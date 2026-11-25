Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System.Globalization
Imports OfficeOpenXml


Partial Class MedicalExamImport
    Inherits System.Web.UI.Page

    Private Const ColumnCount As Integer = 95

    Private Function GetConnectionString() As String
        Return ClsConnection.GetConn()
    End Function


    Protected Sub btnImport_Click(ByVal sender As Object,
                                  ByVal e As EventArgs)

        lblMessage.Text = ""

        If Not fuExcel.HasFile Then
            ShowError("لطفاً فایل اکسل را انتخاب کنید.")
            Return
        End If

        If Not chkReplace.Checked Then
            ShowError("لطفاً تأیید جایگزینی اطلاعات را فعال کنید.")
            Return
        End If

        Dim ext As String =
            IO.Path.GetExtension(fuExcel.FileName).ToLowerInvariant()

        If ext <> ".xlsx" Then
            ShowError("فقط فایل اکسل با پسوند xlsx مجاز است.")
            Return
        End If

        If fuExcel.PostedFile.ContentLength > 20 * 1024 * 1024 Then
            ShowError("حجم فایل نباید بیشتر از ۲۰ مگابایت باشد.")
            Return
        End If

        Dim tempFile As String = Nothing

        Try

            Dim folder As String =
                Server.MapPath("~/App_Data/MedicalImport/")

            If Not IO.Directory.Exists(folder) Then
                IO.Directory.CreateDirectory(folder)
            End If

            tempFile = IO.Path.Combine(
                folder,
                Guid.NewGuid().ToString("N") & ".xlsx"
            )

            ' ذخیره موقت فایل اکسل
            fuExcel.SaveAs(tempFile)

            ' =====================================
            ' خواندن فایل اکسل
            ' =====================================

            Dim excelData As DataTable =
                ReadExcelFile(tempFile)

            ' بررسی خالی نبودن فایل
            If excelData.Rows.Count = 0 Then
                Throw New Exception(
                    "فایل اکسل فاقد اطلاعات یا سرستون است."
                )
            End If

            ' بررسی تعداد ستون‌ها
            If excelData.Columns.Count <> ColumnCount Then
                Throw New Exception(
                    "تعداد ستون‌های فایل معتبر نیست. " &
                    "تعداد مورد انتظار: ۹۵، تعداد فایل: " &
                    excelData.Columns.Count.ToString()
                )
            End If

            ' =====================================
            ' بررسی سرستون‌های مهم
            ' =====================================

            CheckHeader(excelData, 0, "شماره پرونده")
            CheckHeader(excelData, 1, "تاریخ مراجعه")
            CheckHeader(excelData, 7, "شماره پرسنلی")
            CheckHeader(excelData, 37, "تاریخ انجام آزمایش")
            CheckHeader(excelData, 42, "FBS")
            CheckHeader(excelData, 43, "HbA1C")
            CheckHeader(excelData, 93, "توصیه های پزشکی")

            ' =====================================
            ' آماده‌سازی اطلاعات
            ' =====================================

            Dim importTable As DataTable =
                PrepareImportTable(excelData)

            If importTable.Rows.Count = 0 Then
                Throw New Exception(
                    "هیچ رکورد معتبری برای انتقال وجود ندارد."
                )
            End If

            ' =====================================
            ' اتصال به SQL Server
            ' =====================================

            Using cnn As New SqlConnection(GetConnectionString())

                cnn.Open()

                ' ساخت جدول موقت
                Dim createSql As String =
                    BuildStageTableSql()

                Using cmd As New SqlCommand(createSql, cnn)
                    cmd.CommandTimeout = 120
                    cmd.ExecuteNonQuery()
                End Using

                ' =====================================
                ' انتقال اطلاعات به جدول موقت
                ' =====================================

                Using bulk As New SqlBulkCopy(cnn)

                    bulk.DestinationTableName = "#MedicalExamStage"
                    bulk.BatchSize = 1000
                    bulk.BulkCopyTimeout = 300

                    For i As Integer = 1 To ColumnCount

                        Dim colName As String =
                            "C" & i.ToString("000")

                        bulk.ColumnMappings.Add(
                            colName,
                            colName
                        )

                    Next

                    bulk.WriteToServer(importTable)

                End Using

                ' =====================================
                ' کنترل تعداد رکوردها
                ' =====================================

                Dim stageCount As Integer

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) FROM #MedicalExamStage", cnn)

                    stageCount = Convert.ToInt32(
                        cmd.ExecuteScalar()
                    )

                End Using

                If stageCount <> importTable.Rows.Count Then
                    Throw New Exception(
                        "تعداد رکوردهای منتقل‌شده صحیح نیست."
                    )
                End If

                ' =====================================
                ' جایگزینی اطلاعات قبلی
                ' =====================================

                Using trans As SqlTransaction = cnn.BeginTransaction()

                    Try

                        Dim insertColumns As String =
                            BuildColumnList()

                        Dim sql As String =
                            "DELETE FROM dbo.MedicalExamRaw; " &
                            "INSERT INTO dbo.MedicalExamRaw (" &
                            insertColumns & ") " &
                            "SELECT " & insertColumns &
                            " FROM #MedicalExamStage; " &
                            "DELETE FROM dbo.MedicalExamColumnMap;"

                        Using cmd As New SqlCommand(sql, cnn, trans)

                            cmd.CommandTimeout = 300
                            cmd.ExecuteNonQuery()

                        End Using

                        ' ثبت نام ستون‌ها
                        For i As Integer = 0 To ColumnCount - 1

                            Using cmd As New SqlCommand(
                                "INSERT INTO dbo.MedicalExamColumnMap " &
                                "(ColumnNo,ColumnName) VALUES (@No,@Name)",
                                cnn, trans)

                                cmd.Parameters.Add(
                                    "@No", SqlDbType.Int
                                ).Value = i + 1

                                cmd.Parameters.Add(
                                    "@Name", SqlDbType.NVarChar, 255
                                ).Value =
                                    Convert.ToString(excelData.Rows(0)(i))

                                cmd.ExecuteNonQuery()

                            End Using

                        Next

                        ' ثبت گزارش بارگذاری
                        Dim importedBy As String =
                            Convert.ToString(Session("UserId"))

                        Using cmd As New SqlCommand(
                            "INSERT INTO dbo.MedicalExamImportLog " &
                            "(FileName,RecordCount,ImportedBy,Status) " &
                            "VALUES (@File,@Count,@User,N'موفق')",
                            cnn, trans)

                            cmd.Parameters.Add(
                                "@File", SqlDbType.NVarChar, 255
                            ).Value = IO.Path.GetFileName(
                                fuExcel.FileName
                            )

                            cmd.Parameters.Add(
                                "@Count", SqlDbType.Int
                            ).Value = stageCount

                            cmd.Parameters.Add(
                                "@User", SqlDbType.NVarChar, 100
                            ).Value = importedBy

                            cmd.ExecuteNonQuery()

                        End Using

                        trans.Commit()

                    Catch

                        trans.Rollback()
                        Throw

                    End Try

                End Using

                ' =====================================
                ' پیام موفقیت
                ' =====================================

                ShowSuccess(
                    "اطلاعات با موفقیت جایگزین شد." &
                    "<br/>تعداد رکوردهای ثبت‌شده: " &
                    stageCount.ToString("N0") &
                    "<br/>تعداد ستون‌ها: ۹۵" &
                    "<br/>زمان بارگذاری: " &
                    DateTime.Now.ToString("HH:mm:ss")
                )

            End Using

        Catch ex As Exception

            ShowError(
                "خطا در بارگذاری اطلاعات: " &
                Server.HtmlEncode(ex.Message)
            )

        Finally

            If Not String.IsNullOrEmpty(tempFile) Then

                Try
                    If IO.File.Exists(tempFile) Then
                        IO.File.Delete(tempFile)
                    End If
                Catch
                End Try

            End If

        End Try

    End Sub



    Private Function ReadExcelFile(ByVal filePath As String) As DataTable

        Dim dt As New DataTable()

        Using package As New ExcelPackage(New FileInfo(filePath))

            If package.Workbook.Worksheets.Count = 0 Then
                Throw New Exception("فایل اکسل فاقد شیت اطلاعاتی است.")
            End If

            Dim ws As ExcelWorksheet =
                package.Workbook.Worksheets(1)

            If ws.Dimension Is Nothing Then
                Throw New Exception("فایل اکسل خالی است.")
            End If

            Dim rowCount As Integer = ws.Dimension.End.Row
            Dim colCount As Integer = ws.Dimension.End.Column

            For i As Integer = 1 To colCount
                dt.Columns.Add("Column" & i.ToString("000"))
            Next

            For r As Integer = 1 To rowCount

                Dim dr As DataRow = dt.NewRow()

                For c As Integer = 1 To colCount

                    Dim value As Object = ws.Cells(r, c).Value

                    If value Is Nothing Then
                        dr(c - 1) = DBNull.Value
                    Else
                        dr(c - 1) = Convert.ToString(
                            value,
                            Globalization.CultureInfo.InvariantCulture
                        )
                    End If

                Next

                dt.Rows.Add(dr)

            Next

        End Using

        Return dt

    End Function


    Private Sub CheckHeader(
        ByVal dt As DataTable,
        ByVal index As Integer,
        ByVal expected As String
    )

        Dim actual As String =
            Convert.ToString(dt.Rows(0)(index)).Trim()

        If actual <> expected Then

            Throw New Exception(
                "سرستون شماره " & (index + 1).ToString() &
                " معتبر نیست. مقدار مورد انتظار: " &
                expected & " - مقدار فایل: " & actual
            )

        End If

    End Sub

    Private Function PrepareImportTable(
        ByVal source As DataTable
    ) As DataTable

        Dim result As New DataTable()

        For i As Integer = 1 To ColumnCount

            result.Columns.Add(
                "C" & i.ToString("000"),
                GetType(String)
            )

        Next

        ' ردیف اول اکسل سرستون است
        For r As Integer = 1 To source.Rows.Count - 1

            Dim sourceRow As DataRow = source.Rows(r)

            Dim allEmpty As Boolean = True

            For c As Integer = 0 To ColumnCount - 1

                If Not IsDBNull(sourceRow(c)) AndAlso
                   Convert.ToString(sourceRow(c)).Trim() <> "" Then

                    allEmpty = False
                    Exit For

                End If

            Next

            If allEmpty Then
                Continue For
            End If

            Dim caseNo As String =
                Convert.ToString(sourceRow(0)).Trim()

            Dim employeeCode As String =
                Convert.ToString(sourceRow(7)).Trim()

            ' اگر شماره پرونده یا شماره پرسنلی خالی بود، ردیف نادیده گرفته شود
            If caseNo = "" OrElse employeeCode = "" Then
                Continue For
            End If

            Dim newRow As DataRow = result.NewRow()

            For c As Integer = 0 To ColumnCount - 1

                If IsDBNull(sourceRow(c)) Then

                    newRow(c) = DBNull.Value

                Else

                    newRow(c) =
                        Convert.ToString(
                            sourceRow(c),
                            CultureInfo.InvariantCulture
                        ).Trim()

                End If

            Next

            result.Rows.Add(newRow)

        Next

        Return result

    End Function

    Private Function BuildStageTableSql() As String

        Dim sql As String =
            "IF OBJECT_ID('tempdb..#MedicalExamStage') IS NOT NULL " &
            "DROP TABLE #MedicalExamStage; " &
            "CREATE TABLE #MedicalExamStage ("

        For i As Integer = 1 To ColumnCount

            If i > 1 Then sql &= ","

            sql &= "[C" & i.ToString("000") &
                   "] NVARCHAR(MAX) NULL"

        Next

        sql &= ");"

        Return sql

    End Function

    Private Function BuildColumnList() As String

        Dim names As New List(Of String)()

        For i As Integer = 1 To ColumnCount

            names.Add("[C" & i.ToString("000") & "]")

        Next

        Return String.Join(",", names.ToArray())

    End Function

    Private Sub ShowSuccess(ByVal message As String)

        lblMessage.CssClass = "result-box success-box"

        lblMessage.Text =
            "<div>" & message & "</div>"

    End Sub

    Private Sub ShowError(ByVal message As String)

        lblMessage.CssClass = "result-box error-box"

        lblMessage.Text =
            "<div>" & message & "</div>"

    End Sub

End Class
