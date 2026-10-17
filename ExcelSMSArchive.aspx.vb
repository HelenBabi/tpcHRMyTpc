Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.Globalization
Imports System.IO

Partial Class ExcelSMSArchive
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadArchive()
        End If
    End Sub

    ' ============================================================
    ' متد کمکی
    ' ============================================================
    Private Function GetDataTable(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Try
            Dim paramDict As New Dictionary(Of String, Object)()
            For Each p As SqlParameter In parameters
                paramDict.Add(p.ParameterName, p.Value)
            Next
            Return ClsDataTable.GetDataTable(sql, paramDict)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' ============================================================
    ' کلاس وضعیت
    ' ============================================================
    Protected Function GetStatusClass(ByVal status As Object) As String
        Try
            Dim statusStr As String = status.ToString()

            Select Case statusStr
                Case "OK"
                    Return "arc-badge-ok"
                Case Else
                    Return "arc-badge-failed"
            End Select
        Catch ex As Exception
            Return "arc-badge-failed"
        End Try
    End Function

    ' ============================================================
    ' بارگذاری آرشیو
    ' ============================================================
    Private Sub LoadArchive()
        Try
            Dim sql As New StringBuilder()

            sql.AppendLine("SELECT ")
            sql.AppendLine("    ID, BatchID, PersonCode, FirstName, LastName, Mobile, ")
            sql.AppendLine("    SmsText, SmsFinalText, SendDate, SendDatePersian, SendTime, ")
            sql.AppendLine("    Status, StatusText, SmsID, ErrorMessage, CreatedBy ")
            sql.AppendLine("FROM ExcelSMSArchive ")
            sql.AppendLine("WHERE 1=1 ")

            ' ====== فیلتر تاریخ ======
            Dim dateFrom As String = txtDateFrom.Text.Trim()
            Dim dateTo As String = txtDateTo.Text.Trim()

            If Not String.IsNullOrEmpty(dateFrom) Then
                sql.AppendLine("    AND SendDatePersian >= @DateFrom ")
            End If

            If Not String.IsNullOrEmpty(dateTo) Then
                sql.AppendLine("    AND SendDatePersian <= @DateTo ")
            End If

            ' ====== فیلتر موبایل ======
            Dim mobile As String = txtMobile.Text.Trim()
            If Not String.IsNullOrEmpty(mobile) Then
                sql.AppendLine("    AND Mobile LIKE N'%' + @Mobile + N'%' ")
            End If

            ' ====== فیلتر کد پرسنلی ======
            Dim personCode As String = txtPersonCode.Text.Trim()
            If Not String.IsNullOrEmpty(personCode) Then
                sql.AppendLine("    AND PersonCode LIKE N'%' + @PersonCode + N'%' ")
            End If

            sql.AppendLine("ORDER BY SendDate DESC, ID DESC")

            ' ====== پارامترها ======
            Dim params As New List(Of SqlParameter)

            If Not String.IsNullOrEmpty(dateFrom) Then
                params.Add(New SqlParameter("@DateFrom", dateFrom))
            End If

            If Not String.IsNullOrEmpty(dateTo) Then
                params.Add(New SqlParameter("@DateTo", dateTo))
            End If

            If Not String.IsNullOrEmpty(mobile) Then
                params.Add(New SqlParameter("@Mobile", mobile))
            End If

            If Not String.IsNullOrEmpty(personCode) Then
                params.Add(New SqlParameter("@PersonCode", personCode))
            End If

            Dim dt As DataTable = GetDataTable(sql.ToString(), params.ToArray())

            ' ====== اضافه کردن شماره ردیف ======
            dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            ' ====== آمار ======
            UpdateSummary(dt)

            ' ====== اتصال به GridView ======
            gvArchive.DataSource = dt
            gvArchive.DataBind()

            pnlSummary.Visible = True

            If dt.Rows.Count = 0 Then
                ShowMessage("ℹ️ هیچ رکوردی در آرشیو یافت نشد", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری آرشیو: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' به‌روزرسانی آمار
    ' ============================================================
    Private Sub UpdateSummary(ByVal dt As DataTable)
        Try
            Dim totalCount As Integer = dt.Rows.Count
            Dim successCount As Integer = 0
            Dim failedCount As Integer = 0
            Dim batchList As New HashSet(Of String)()

            For Each row As DataRow In dt.Rows
                If row("Status").ToString() = "OK" Then
                    successCount += 1
                Else
                    failedCount += 1
                End If

                If row("BatchID") IsNot DBNull.Value Then
                    batchList.Add(row("BatchID").ToString())
                End If
            Next

            lblTotalCount.Text = totalCount.ToString()
            lblSuccessCount.Text = successCount.ToString()
            lblFailedCount.Text = failedCount.ToString()
            lblBatchCount.Text = batchList.Count.ToString()

        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' جستجو
    ' ============================================================
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        gvArchive.PageIndex = 0
        LoadArchive()
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvArchive_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvArchive.PageIndexChanging
        gvArchive.PageIndex = e.NewPageIndex
        LoadArchive()
    End Sub

    ' ============================================================
    ' خروجی اکسل
    ' ============================================================
    Protected Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim sql As New StringBuilder()

            sql.AppendLine("SELECT ")
            sql.AppendLine("    SendDatePersian AS [تاریخ ارسال], ")
            sql.AppendLine("    SendTime AS [ساعت], ")
            sql.AppendLine("    FirstName AS [نام], ")
            sql.AppendLine("    LastName AS [نام خانوادگی], ")
            sql.AppendLine("    PersonCode AS [کد پرسنلی], ")
            sql.AppendLine("    Mobile AS [موبایل], ")
            sql.AppendLine("    SmsFinalText AS [متن پیامک], ")
            sql.AppendLine("    StatusText AS [وضعیت], ")
            sql.AppendLine("    CreatedBy AS [کاربر] ")
            sql.AppendLine("FROM ExcelSMSArchive ")
            sql.AppendLine("ORDER BY SendDate DESC")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=ExcelSMSArchive.xls")
            Response.Charset = "utf-8"
            Response.ContentType = "application/vnd.ms-excel"

            Dim sb As New StringBuilder()

            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>")
            sb.Append("<head><meta charset='utf-8' />")
            sb.Append("<style>")
            sb.Append("table { border-collapse: collapse; direction: rtl; font-family: Tahoma; }")
            sb.Append("th { background: #2c3e50; color: white; padding: 8px 12px; border: 1px solid #ddd; text-align: center; }")
            sb.Append("td { padding: 8px 12px; border: 1px solid #ddd; text-align: center; }")
            sb.Append("</style>")
            sb.Append("</head><body>")

            sb.Append("<h3>آرشیو پیامک‌های گروهی</h3>")
            sb.Append("<table>")

            sb.Append("<tr>")
            For Each col As DataColumn In dt.Columns
                sb.Append("<th>" & col.ColumnName & "</th>")
            Next
            sb.Append("</tr>")

            For Each row As DataRow In dt.Rows
                sb.Append("<tr>")
                For Each col As DataColumn In dt.Columns
                    sb.Append("<td>" & row(col.ColumnName).ToString() & "</td>")
                Next
                sb.Append("</tr>")
            Next

            sb.Append("</table>")
            sb.Append("</body></html>")

            Dim utf8Bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sb.ToString())
            Response.BinaryWrite(utf8Bytes)
            Response.End()

        Catch ex As Exception
            ShowMessage("خطا در خروجی اکسل: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("ExcelSMS.aspx")
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
                divMessage.Attributes("class") = "arc-alert arc-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                divMessage.Attributes("class") = "arc-alert arc-alert-danger"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                divMessage.Attributes("class") = "arc-alert arc-alert-info"
        End Select
    End Sub

End Class