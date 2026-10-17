Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports OfficeOpenXml

Partial Class ExcelSMS
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("ExcelSMS_Data") = Nothing
        End If
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        pnlMessage.Visible = True
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
                divMessage.Attributes("class") = "excel-alert excel-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                divMessage.Attributes("class") = "excel-alert excel-alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
                divMessage.Attributes("class") = "excel-alert excel-alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                divMessage.Attributes("class") = "excel-alert excel-alert-info"
        End Select
    End Sub

    ' ============================================================
    ' اعتبارسنجی شماره موبایل
    ' ============================================================
    Private Function IsValidMobile(ByVal mobile As String) As Boolean
        Try
            If String.IsNullOrEmpty(mobile) Then
                Return False
            End If

            mobile = mobile.Trim().Replace(" ", "").Replace("-", "")
            mobile = ConvertToEnglishDigits(mobile)

            If mobile.StartsWith("09") AndAlso mobile.Length = 11 AndAlso IsNumeric(mobile) Then
                Return True
            End If

            If mobile.StartsWith("9") AndAlso mobile.Length = 10 AndAlso IsNumeric(mobile) Then
                Return True
            End If

            If mobile.StartsWith("+98") AndAlso mobile.Length = 13 AndAlso IsNumeric(mobile.Substring(1)) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' ============================================================
    ' نرمال‌سازی شماره موبایل
    ' ============================================================
    Private Function NormalizeMobile(ByVal mobile As String) As String
        Try
            If String.IsNullOrEmpty(mobile) Then
                Return ""
            End If

            mobile = mobile.Trim().Replace(" ", "").Replace("-", "")
            mobile = ConvertToEnglishDigits(mobile)

            If mobile.StartsWith("+98") Then
                mobile = "0" & mobile.Substring(3)
            End If

            If mobile.StartsWith("98") AndAlso mobile.Length = 12 Then
                mobile = "0" & mobile.Substring(2)
            End If

            If mobile.StartsWith("9") AndAlso mobile.Length = 10 Then
                mobile = "0" & mobile
            End If

            Return mobile
        Catch ex As Exception
            Return mobile
        End Try
    End Function

    ' ============================================================
    ' تبدیل اعداد فارسی به انگلیسی
    ' ============================================================
    Private Function ConvertToEnglishDigits(ByVal input As String) As String
        Try
            If String.IsNullOrEmpty(input) Then
                Return ""
            End If

            Dim persianDigits As String() = {"۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"}
            Dim arabicDigits As String() = {"٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩"}
            Dim englishDigits As String() = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}

            For i As Integer = 0 To 9
                input = input.Replace(persianDigits(i), englishDigits(i))
                input = input.Replace(arabicDigits(i), englishDigits(i))
            Next

            Return input
        Catch ex As Exception
            Return input
        End Try
    End Function

    ' ============================================================
    ' کلاس وضعیت
    ' ============================================================
    Protected Function GetStatusBadgeClass(ByVal status As Object) As String
        Try
            Dim statusStr As String = status.ToString()

            Select Case statusStr
                Case "OK"
                    Return "badge-valid"
                Case Else
                    Return "badge-invalid"
            End Select
        Catch ex As Exception
            Return "badge-invalid"
        End Try
    End Function

    ' ============================================================
    ' خواندن فایل اکسل با EPPlus (بدون نیاز به Office)
    ' ============================================================
    Private Function ReadExcelFile(ByVal filePath As String) As DataTable
        Dim dt As New DataTable()

        Try
            ' ====== ایجاد DataTable ======
            dt.Columns.Add("RowNumber", GetType(Integer))
            dt.Columns.Add("FirstName", GetType(String))
            dt.Columns.Add("LastName", GetType(String))
            dt.Columns.Add("PersonCode", GetType(String))
            dt.Columns.Add("Mobile", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("StatusText", GetType(String))

            ' ====== خواندن فایل با EPPlus ======
            Dim fileInfo As New FileInfo(filePath)

            Using package As New ExcelPackage(fileInfo)
                If package.Workbook.Worksheets.Count = 0 Then
                    Throw New Exception("فایل اکسل هیچ شیتی ندارد")
                End If

                Dim ws As ExcelWorksheet = package.Workbook.Worksheets(1)

                If ws.Dimension Is Nothing Then
                    Throw New Exception("فایل اکسل خالی است")
                End If

                Dim rowCount As Integer = ws.Dimension.End.Row
                Dim colCount As Integer = ws.Dimension.End.Column

                ' ====== بررسی حداقل ستون‌ها ======
                If colCount < 4 Then
                    Throw New Exception("فایل اکسل باید حداقل 4 ستون داشته باشد")
                End If

                ' ====== خواندن داده‌ها (از سطر 2 به بعد) ======
                Dim rowNumber As Integer = 0

                For i As Integer = 2 To rowCount
                    Try
                        ' ====== خواندن مقادیر ======
                        Dim firstName As String = GetCellValue(ws, i, 1)
                        Dim lastName As String = GetCellValue(ws, i, 2)
                        Dim personCode As String = GetCellValue(ws, i, 3)
                        Dim mobile As String = GetCellValue(ws, i, 4)

                        ' ====== اگر همه خالی بودن، رد کن ======
                        If String.IsNullOrEmpty(firstName) AndAlso
                           String.IsNullOrEmpty(lastName) AndAlso
                           String.IsNullOrEmpty(personCode) AndAlso
                           String.IsNullOrEmpty(mobile) Then
                            Continue For
                        End If

                        rowNumber += 1

                        ' ====== نرمال‌سازی ======
                        mobile = NormalizeMobile(mobile)
                        personCode = ConvertToEnglishDigits(personCode).Trim()

                        ' ====== بررسی اعتبار ======
                        Dim status As String = "OK"
                        Dim statusText As String = "✅ معتبر"

                        If String.IsNullOrEmpty(firstName) Then
                            status = "INVALID"
                            statusText = "❌ نام خالی"
                        ElseIf String.IsNullOrEmpty(lastName) Then
                            status = "INVALID"
                            statusText = "❌ نام خانوادگی خالی"
                        ElseIf String.IsNullOrEmpty(personCode) Then
                            status = "INVALID"
                            statusText = "❌ کد پرسنلی خالی"
                        ElseIf Not IsValidMobile(mobile) Then
                            status = "INVALID"
                            statusText = "❌ موبایل نامعتبر"
                        End If

                        ' ====== اضافه کردن به DataTable ======
                        Dim row As DataRow = dt.NewRow()
                        row("RowNumber") = rowNumber
                        row("FirstName") = firstName.Trim()
                        row("LastName") = lastName.Trim()
                        row("PersonCode") = personCode
                        row("Mobile") = mobile
                        row("Status") = status
                        row("StatusText") = statusText

                        dt.Rows.Add(row)

                    Catch ex As Exception
                        Continue For
                    End Try
                Next
            End Using

            Return dt

        Catch ex As Exception
            Throw New Exception("خطا در خواندن فایل اکسل: " & ex.Message)
        End Try
    End Function

    ' ============================================================
    ' خواندن مقدار سلول با EPPlus
    ' ============================================================
    Private Function GetCellValue(ByVal ws As ExcelWorksheet, ByVal row As Integer, ByVal col As Integer) As String
        Try
            Dim cell As ExcelRange = ws.Cells(row, col)

            If cell.Value Is Nothing Then
                Return ""
            End If

            Return cell.Value.ToString().Trim()

        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' حذف رکوردهای تکراری و نامعتبر
    ' ============================================================
    Private Function RemoveDuplicates(ByVal dt As DataTable) As DataTable
        Try
            Dim dtResult As DataTable = dt.Clone()

            Dim seenPersonCodes As New HashSet(Of String)()
            Dim seenMobiles As New HashSet(Of String)()

            For Each row As DataRow In dt.Rows

                If row("Status").ToString() <> "OK" Then
                    Continue For
                End If

                Dim personCode As String = row("PersonCode").ToString().Trim()
                Dim mobile As String = row("Mobile").ToString().Trim()

                If seenPersonCodes.Contains(personCode) OrElse seenMobiles.Contains(mobile) Then
                    row("Status") = "DUPLICATE"
                    row("StatusText") = "⚠️ تکراری"
                    Continue For
                End If

                seenPersonCodes.Add(personCode)
                seenMobiles.Add(mobile)

                dtResult.ImportRow(row)
            Next

            Return dtResult

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' ============================================================
    ' مرحله 1: آپلود فایل
    ' ============================================================
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            ' ====== اعتبارسنجی فایل ======
            If Not fileUpload.HasFile Then
                ShowMessage("لطفاً یک فایل اکسل انتخاب کنید", "danger")
                Return
            End If

            ' ====== اعتبارسنجی متن پیامک ======
            If String.IsNullOrEmpty(txtSmsText.Text.Trim()) Then
                ShowMessage("لطفاً متن پیامک را وارد کنید", "danger")
                Return
            End If

            ' ====== بررسی فرمت فایل ======
            Dim extension As String = Path.GetExtension(fileUpload.FileName).ToLower()
            If extension <> ".xls" AndAlso extension <> ".xlsx" Then
                ShowMessage("فقط فایل‌های اکسل (xls یا xlsx) مجاز هستند", "danger")
                Return
            End If

            ' ====== بررسی حجم فایل ======
            If fileUpload.PostedFile.ContentLength > 10 * 1024 * 1024 Then
                ShowMessage("حجم فایل نباید بیشتر از 10 مگابایت باشد", "danger")
                Return
            End If

            ' ====== ذخیره فایل موقت ======
            Dim tempFolder As String = Server.MapPath("~/Temp/")
            If Not Directory.Exists(tempFolder) Then
                Directory.CreateDirectory(tempFolder)
            End If

            Dim tempFileName As String = Guid.NewGuid().ToString() & extension
            Dim tempFilePath As String = Path.Combine(tempFolder, tempFileName)

            fileUpload.SaveAs(tempFilePath)

            ' ====== خواندن فایل اکسل ======
            Dim dt As DataTable = ReadExcelFile(tempFilePath)

            ' ====== حذف فایل موقت ======
            Try
                File.Delete(tempFilePath)
            Catch
            End Try

            ' ====== بررسی خالی بودن ======
            If dt.Rows.Count = 0 Then
                ShowMessage("فایل اکسل خالی است یا فرمت آن صحیح نیست", "danger")
                Return
            End If

            ' ====== حذف تکراری‌ها ======
            Dim dtFinal As DataTable = RemoveDuplicates(dt)

            ' ====== ذخیره در Session ======
            Session("ExcelSMS_Data") = dtFinal
            Session("ExcelSMS_SmsText") = txtSmsText.Text.Trim()

            ' ====== نمایش پیش‌نمایش ======
            ShowPreview(dtFinal)

            ' ====== مخفی کردن مرحله 1 ======
            pnlUpload.Visible = False

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' نمایش پیش‌نمایش
    ' ============================================================
    Private Sub ShowPreview(ByVal dt As DataTable)
        Try
            Dim totalRows As Integer = dt.Rows.Count
            Dim validRows As Integer = 0
            Dim invalidRows As Integer = 0
            Dim duplicateRows As Integer = 0

            For Each row As DataRow In dt.Rows
                Select Case row("Status").ToString()
                    Case "OK"
                        validRows += 1
                    Case "INVALID"
                        invalidRows += 1
                    Case "DUPLICATE"
                        duplicateRows += 1
                End Select
            Next

            lblTotalRows.Text = totalRows.ToString()
            lblValidRows.Text = validRows.ToString()
            lblInvalidRows.Text = invalidRows.ToString()
            lblDuplicateRows.Text = duplicateRows.ToString()

            gvPreview.DataSource = dt
            gvPreview.DataBind()

            pnlPreview.Visible = True

        Catch ex As Exception
            ShowMessage("خطا در نمایش پیش‌نمایش: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' مرحله 2: ارسال پیامک‌ها
    ' ============================================================
    ' ============================================================
    ' مرحله 2: ارسال پیامک‌ها و ذخیره در آرشیو
    ' ============================================================
    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            Dim dt As DataTable = TryCast(Session("ExcelSMS_Data"), DataTable)
            Dim smsText As String = ""
            If Session("ExcelSMS_SmsText") IsNot Nothing Then
                smsText = Session("ExcelSMS_SmsText").ToString()
            End If

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                ShowMessage("داده‌ای برای ارسال وجود ندارد", "danger")
                Return
            End If

            If String.IsNullOrEmpty(smsText) Then
                ShowMessage("متن پیامک خالی است", "danger")
                Return
            End If

            ' ====== شناسه یکتای این ارسال (Batch) ======
            Dim batchID As Guid = Guid.NewGuid()

            Dim sentCount As Integer = 0
            Dim failedCount As Integer = 0
            Dim archivedCount As Integer = 0

            ' ====== اطلاعات کاربر ======
            Dim createdBy As String = "admin"
            If Session("FullName") IsNot Nothing Then
                createdBy = Session("FullName").ToString()
            End If

            ' ====== تاریخ و ساعت شمسی ======
            Dim sendDate As DateTime = DateTime.Now
            Dim sendDatePersian As String = GetPersianDate(sendDate)
            Dim sendTime As String = sendDate.ToString("HH:mm:ss")

            Using conn As New SqlConnection(ClsConnection.GetConn)
                conn.Open()

                Using transaction As SqlTransaction = conn.BeginTransaction()
                    Try
                        For Each row As DataRow In dt.Rows
                            ' ====== فقط رکوردهای معتبر ======
                            If row("Status").ToString() <> "OK" Then
                                Continue For
                            End If

                            Dim mobile As String = row("Mobile").ToString().Trim()
                            Dim firstName As String = row("FirstName").ToString().Trim()
                            Dim lastName As String = row("LastName").ToString().Trim()
                            Dim personCode As String = row("PersonCode").ToString().Trim()

                            ' ====== جایگزینی متغیرها در متن پیامک ======
                            Dim finalSmsText As String = smsText
                            finalSmsText = finalSmsText.Replace("{نام}", firstName)
                            finalSmsText = finalSmsText.Replace("{نام‌خانوادگی}", lastName)
                            finalSmsText = finalSmsText.Replace("{کدپرسنلی}", personCode)
                            finalSmsText = finalSmsText.Replace("{موبایل}", mobile)

                            Dim smsID As Integer = 0
                            Dim errorMsg As String = ""

                            Try
                                ' ====== 1. درج در جدول smsTbl (برای ارسال) ======
                                Dim insertSmsSql As String =
                                    "INSERT INTO smsTbl (mobile, message, tarix, saat, stat) " &
                                    "VALUES (@Mobile, @Message, @Tarix, @Saat, 0); " &
                                    "SELECT SCOPE_IDENTITY();"

                                Using cmd As New SqlCommand(insertSmsSql, conn, transaction)
                                    cmd.Parameters.AddWithValue("@Mobile", mobile)
                                    cmd.Parameters.AddWithValue("@Message", finalSmsText)
                                    cmd.Parameters.AddWithValue("@Tarix", sendDatePersian)
                                    cmd.Parameters.AddWithValue("@Saat", sendTime)
                                    smsID = Convert.ToInt32(cmd.ExecuteScalar())
                                End Using

                                sentCount += 1

                                ' ====== 2. درج در جدول آرشیو ======
                                Dim insertArchiveSql As String =
                                    "INSERT INTO ExcelSMSArchive (" &
                                    "BatchID, PersonCode, FirstName, LastName, Mobile, " &
                                    "SmsText, SmsFinalText, SendDate, SendDatePersian, SendTime, " &
                                    "Status, StatusText, SmsID, CreatedBy) " &
                                    "VALUES (" &
                                    "@BatchID, @PersonCode, @FirstName, @LastName, @Mobile, " &
                                    "@SmsText, @SmsFinalText, @SendDate, @SendDatePersian, @SendTime, " &
                                    "@Status, @StatusText, @SmsID, @CreatedBy)"

                                Using cmd As New SqlCommand(insertArchiveSql, conn, transaction)
                                    cmd.Parameters.AddWithValue("@BatchID", batchID)
                                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                                    cmd.Parameters.AddWithValue("@FirstName", firstName)
                                    cmd.Parameters.AddWithValue("@LastName", lastName)
                                    cmd.Parameters.AddWithValue("@Mobile", mobile)
                                    cmd.Parameters.AddWithValue("@SmsText", smsText)
                                    cmd.Parameters.AddWithValue("@SmsFinalText", finalSmsText)
                                    cmd.Parameters.AddWithValue("@SendDate", sendDate)
                                    cmd.Parameters.AddWithValue("@SendDatePersian", sendDatePersian)
                                    cmd.Parameters.AddWithValue("@SendTime", sendTime)
                                    cmd.Parameters.AddWithValue("@Status", "OK")
                                    cmd.Parameters.AddWithValue("@StatusText", "✅ ارسال موفق")
                                    cmd.Parameters.AddWithValue("@SmsID", If(smsID > 0, smsID, DBNull.Value))
                                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy)
                                    cmd.ExecuteNonQuery()
                                End Using

                                archivedCount += 1

                            Catch ex As Exception
                                failedCount += 1
                                errorMsg = ex.Message

                                ' ====== درج خطا در آرشیو ======
                                Try
                                    Dim insertArchiveSql As String =
                                        "INSERT INTO ExcelSMSArchive (" &
                                        "BatchID, PersonCode, FirstName, LastName, Mobile, " &
                                        "SmsText, SmsFinalText, SendDate, SendDatePersian, SendTime, " &
                                        "Status, StatusText, ErrorMessage, CreatedBy) " &
                                        "VALUES (" &
                                        "@BatchID, @PersonCode, @FirstName, @LastName, @Mobile, " &
                                        "@SmsText, @SmsFinalText, @SendDate, @SendDatePersian, @SendTime, " &
                                        "@Status, @StatusText, @ErrorMessage, @CreatedBy)"

                                    Using cmd As New SqlCommand(insertArchiveSql, conn, transaction)
                                        cmd.Parameters.AddWithValue("@BatchID", batchID)
                                        cmd.Parameters.AddWithValue("@PersonCode", personCode)
                                        cmd.Parameters.AddWithValue("@FirstName", firstName)
                                        cmd.Parameters.AddWithValue("@LastName", lastName)
                                        cmd.Parameters.AddWithValue("@Mobile", mobile)
                                        cmd.Parameters.AddWithValue("@SmsText", smsText)
                                        cmd.Parameters.AddWithValue("@SmsFinalText", finalSmsText)
                                        cmd.Parameters.AddWithValue("@SendDate", sendDate)
                                        cmd.Parameters.AddWithValue("@SendDatePersian", sendDatePersian)
                                        cmd.Parameters.AddWithValue("@SendTime", sendTime)
                                        cmd.Parameters.AddWithValue("@Status", "FAILED")
                                        cmd.Parameters.AddWithValue("@StatusText", "❌ خطا در ارسال")
                                        cmd.Parameters.AddWithValue("@ErrorMessage", errorMsg)
                                        cmd.Parameters.AddWithValue("@CreatedBy", createdBy)
                                        cmd.ExecuteNonQuery()
                                    End Using
                                Catch
                                End Try
                            End Try
                        Next

                        transaction.Commit()

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

            ' ====== نمایش نتیجه ======
            lblSentCount.Text = sentCount.ToString()
            lblFailedCount.Text = failedCount.ToString()

            pnlUpload.Visible = False
            pnlPreview.Visible = False
            pnlResult.Visible = True

            ShowMessage("✅ " & sentCount & " پیامک با موفقیت ارسال شد | 📦 " & archivedCount & " رکورد آرشیو شد", "success")

            ' ====== پاکسازی Session ======
            Session("ExcelSMS_Data") = Nothing
            Session("ExcelSMS_SmsText") = Nothing

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' تبدیل تاریخ به شمسی
    ' ============================================================
    Private Function GetPersianDate(ByVal dt As DateTime) As String
        Try
            Dim pc As New System.Globalization.PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" & pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" & pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c)
        Catch ex As Exception
            Return dt.ToString("yyyy/MM/dd")
        End Try
    End Function

    ' ============================================================
    ' انصراف
    ' ============================================================
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Session("ExcelSMS_Data") = Nothing
        Session("ExcelSMS_SmsText") = Nothing

        pnlUpload.Visible = True
        pnlPreview.Visible = False
        pnlResult.Visible = False

        txtSmsText.Text = ""
        pnlMessage.Visible = False
    End Sub

    ' ============================================================
    ' آپلود فایل جدید
    ' ============================================================
    Protected Sub btnNewUpload_Click(sender As Object, e As EventArgs) Handles btnNewUpload.Click
        Session("ExcelSMS_Data") = Nothing
        Session("ExcelSMS_SmsText") = Nothing

        pnlUpload.Visible = True
        pnlPreview.Visible = False
        pnlResult.Visible = False

        txtSmsText.Text = ""
        pnlMessage.Visible = False
    End Sub

End Class