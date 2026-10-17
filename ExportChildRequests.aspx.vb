Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Text

Partial Class ExportChildRequests
    Inherits System.Web.UI.Page

    Private Const ACADEMIC_YEAR As String = "1405-1404"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' ✅ پاک کردن کامل Response
        Response.Clear()
        Response.ClearHeaders()
        Response.Buffer = True

        Try
            ' ============================================================
            ' ۱. بررسی Session
            ' ============================================================
            If Session("codePersoneli") Is Nothing Then
                Response.Redirect("~/Login_2.aspx")
                Return
            End If

            Dim personCode As String = Session("codePersoneli").ToString().Trim()
            Dim personName As String = If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), personCode)

            ' ============================================================
            ' ۲. بررسی نقش روابط عمومی
            ' ============================================================
            If Not IsPublicRelationsUser(personCode) Then
                Response.ContentType = "text/html; charset=utf-8"
                Response.Write("<html><head><meta charset='utf-8'></head><body style='font-family:Tahoma;direction:rtl;padding:20px;'>")
                Response.Write("<h3 style='color:red;'>دسترسی غیرمجاز — فقط کارشناسان روابط عمومی</h3>")
                Response.Write("</body></html>")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Return
            End If

            ' ============================================================
            ' ۳. دریافت فیلترها از Session
            ' ============================================================
            Dim activeTab As String = If(Session("ExportActiveTab") IsNot Nothing, Session("ExportActiveTab").ToString(), "open")
            Dim statusFilter As String = If(Session("ExportStatusFilter") IsNot Nothing, Session("ExportStatusFilter").ToString(), "0")
            Dim typeFilter As String = If(Session("ExportTypeFilter") IsNot Nothing, Session("ExportTypeFilter").ToString(), "")
            Dim search As String = If(Session("ExportSearch") IsNot Nothing, Session("ExportSearch").ToString(), "")

            ' پاک کردن Session پس از استفاده
            Session.Remove("ExportActiveTab")
            Session.Remove("ExportStatusFilter")
            Session.Remove("ExportTypeFilter")
            Session.Remove("ExportSearch")

            ' ============================================================
            ' ۴. ساخت کوئری
            ' ============================================================
            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    cr.ID AS [کد پیگیری],")
            sql.AppendLine("    cr.PersonCode AS [کد پرسنلی],")
            sql.AppendLine("    cr.FullName AS [نام کارمند],")
            sql.AppendLine("    ISNULL(o.omur, N'-') AS [واحد],")
            sql.AppendLine("    cr.Mobile AS [موبایل],")
            sql.AppendLine("    cr.ChildFullName AS [نام فرزند],")
            sql.AppendLine("    cr.ChildNationalCode AS [کد ملی فرزند],")
            sql.AppendLine("    cr.ChildBirthDate AS [تاریخ تولد],")
            sql.AppendLine("    cr.ChildGender AS [جنسیت],")
            sql.AppendLine("    cr.ChildType AS [نوع تحصیل],")
            sql.AppendLine("    cr.EducationLevel AS [مقطع تحصیلی],")
            sql.AppendLine("    ISNULL(CAST(cr.Average AS NVARCHAR(10)), N'-') AS [معدل],")
            sql.AppendLine("    ISNULL(cr.SchoolName, N'-') AS [نام مدرسه],")
            sql.AppendLine("    ISNULL(cr.SchoolType, N'-') AS [نوع مدرسه],")
            sql.AppendLine("    ISNULL(cr.SchoolPhone, N'-') AS [تلفن مدرسه],")
            sql.AppendLine("    ISNULL(cr.UniversityName, N'-') AS [نام دانشگاه],")
            sql.AppendLine("    ISNULL(cr.UniversityType, N'-') AS [نوع دانشگاه],")
            sql.AppendLine("    ISNULL(cr.UniversityPhone, N'-') AS [تلفن دانشگاه],")
            sql.AppendLine("    ISNULL(cr.StudentNumber, N'-') AS [شماره دانشجویی],")
            sql.AppendLine("    CASE ")
            sql.AppendLine("        WHEN cr.AdmissionType = 'WithExam' THEN N'با آزمون'")
            sql.AppendLine("        WHEN cr.AdmissionType = 'WithoutExam' THEN N'بدون آزمون'")
            sql.AppendLine("        ELSE N'-'")
            sql.AppendLine("    END AS [نوع ورود],")
            sql.AppendLine("    ISNULL(CAST(cr.RankInExam AS NVARCHAR(20)), N'-') AS [رتبه آزمون],")
            sql.AppendLine("    ISNULL(cr.WithoutExamReason, N'-') AS [دلیل بدون آزمون],")
            sql.AppendLine("    CASE WHEN cr.IsSampad = 1 THEN N'بله' ELSE N'خیر' END AS [سمپاد],")
            sql.AppendLine("    ISNULL(cr.SampadSchoolName, N'-') AS [مدرسه سمپاد],")
            sql.AppendLine("    ISNULL(CAST(cc.Amount AS NVARCHAR(30)), N'0') AS [مبلغ مقطع],")
            sql.AppendLine("    CASE cr.StatusCode")
            sql.AppendLine("        WHEN 1 THEN N'در انتظار بررسی'")
            sql.AppendLine("        WHEN 2 THEN N'نیاز به اصلاح'")
            sql.AppendLine("        WHEN 3 THEN N'تایید شده'")
            sql.AppendLine("        WHEN 4 THEN N'رد شده'")
            sql.AppendLine("        WHEN 5 THEN N'انصراف'")
            sql.AppendLine("        ELSE N'نامشخص'")
            sql.AppendLine("    END AS [وضعیت],")
            sql.AppendLine("    ISNULL(cr.StatusDescription, N'-') AS [دلیل/توضیحات],")
            sql.AppendLine("    ISNULL(cr.ReviewerName, N'-') AS [کارشناس],")
            sql.AppendLine("    cr.CreateDate AS [تاریخ ثبت],")
            sql.AppendLine("    cr.ReviewDate AS [تاریخ بررسی]")
            sql.AppendLine("FROM exmChildRequests cr")
            sql.AppendLine("LEFT JOIN exmChildConditions cc ON cr.EducationLevelID = cc.ID")
            sql.AppendLine("LEFT JOIN personel p ON p.codePersoneli = cr.PersonCode")
            sql.AppendLine("LEFT JOIN omurTbl o ON o.omurID = p.omurID")
            sql.AppendLine("WHERE cr.AcademicYear = @Year")

            Dim params As New List(Of SqlParameter) From {
                New SqlParameter("@Year", ACADEMIC_YEAR)
            }

            ' فیلتر وضعیت
            If activeTab = "open" Then
                sql.AppendLine("AND cr.StatusCode IN (1, 2)")
            Else
                sql.AppendLine("AND cr.StatusCode IN (3, 4, 5)")
            End If

            Dim statusInt As Integer = 0
            If Integer.TryParse(statusFilter, statusInt) AndAlso statusInt > 0 Then
                sql.AppendLine("AND cr.StatusCode = @StatusCode")
                params.Add(New SqlParameter("@StatusCode", statusInt))
            End If

            ' فیلتر نوع تحصیل
            If Not String.IsNullOrEmpty(typeFilter) Then
                sql.AppendLine("AND cr.ChildType = @ChildType")
                params.Add(New SqlParameter("@ChildType", typeFilter))
            End If

            ' فیلتر جستجو
            If Not String.IsNullOrEmpty(search) Then
                sql.AppendLine("AND (cr.FullName LIKE @Search OR cr.PersonCode LIKE @Search " &
                               "OR cr.ChildFullName LIKE @Search OR cr.ChildNationalCode LIKE @Search)")
                params.Add(New SqlParameter("@Search", "%" & search & "%"))
            End If

            sql.AppendLine("ORDER BY cr.CreateDate DESC")

            ' ============================================================
            ' ۵. اجرای کوئری
            ' ============================================================
            Dim dt As DataTable = GetDataTable(sql.ToString(), params.ToArray())

            ' ============================================================
            ' ۶. ساخت فایل Excel
            ' ============================================================
            Dim fileName As String = "ChildRequests_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xls"

            Response.Clear()
            Response.ClearHeaders()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.UTF8
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & fileName & """")

            ' ✅ BOM برای UTF-8
            Response.BinaryWrite(Encoding.UTF8.GetPreamble())

            ' ============================================================
            ' ✅ ۷. ساخت HTML با هدر گزارش
            ' ============================================================
            Dim html As New StringBuilder()
            html.AppendLine("<html xmlns:x=""urn:schemas-microsoft-com:office:excel"">")
            html.AppendLine("<head>")
            html.AppendLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
            html.AppendLine("<style>")
            html.AppendLine("    table { border-collapse: collapse; font-family: Tahoma; font-size: 11px; direction: rtl; }")
            html.AppendLine("    th { background: #2c3e50; color: #fff; padding: 8px; border: 1px solid #000; font-weight: bold; text-align: center; }")
            html.AppendLine("    td { padding: 6px 8px; border: 1px solid #999; text-align: right; mso-number-format:'\@'; }")
            html.AppendLine("    tr:nth-child(even) { background: #f8f9fa; }")
            html.AppendLine("    .report-header { text-align: center; font-size: 16px; font-weight: bold; color: #2c3e50; padding: 15px; background: #e8f4fd; border: 2px solid #3498db; }")
            html.AppendLine("    .report-subheader { text-align: center; font-size: 12px; color: #555; padding: 10px; background: #f8f9fa; border: 1px solid #dee2e6; }")
            html.AppendLine("    .report-info { font-size: 11px; color: #555; padding: 8px; background: #fff3cd; border: 1px solid #ffe8a1; text-align: right; }")
            html.AppendLine("    .amount-cell { color: #27ae60; font-weight: bold; background: #e8f8f0; text-align: left; }")
            html.AppendLine("    .status-approved { color: #27ae60; font-weight: bold; }")
            html.AppendLine("    .status-rejected { color: #e74c3c; font-weight: bold; }")
            html.AppendLine("    .status-pending { color: #f39c12; font-weight: bold; }")
            html.AppendLine("</style>")
            html.AppendLine("</head>")
            html.AppendLine("<body>")

            ' ============================================================
            ' ✅ هدر گزارش
            ' ============================================================
            Dim tabTitle As String = If(activeTab = "open", "درخواست‌های باز", "درخواست‌های بررسی‌شده")

            html.AppendLine("<table style='width:100%; margin-bottom:15px; border:none;'>")

            ' ردیف ۱: عنوان اصلی
            html.AppendLine("<tr>")
            html.AppendLine("<td class='report-header' colspan='" & (dt.Columns.Count + 1) & "'>")
            html.AppendLine("📊 گزارش " & tabTitle & " — فرزندان ممتاز")
            html.AppendLine("</td>")
            html.AppendLine("</tr>")

            ' ردیف ۲: سال تحصیلی و تعداد
            html.AppendLine("<tr>")
            html.AppendLine("<td class='report-subheader' colspan='" & (dt.Columns.Count + 1) & "'>")
            html.AppendLine("سال تحصیلی: " & ACADEMIC_YEAR & " &nbsp;|&nbsp; تعداد رکوردها: " & dt.Rows.Count.ToString() & " &nbsp;|&nbsp; واحد روابط عمومی")
            html.AppendLine("</td>")
            html.AppendLine("</tr>")

            ' ردیف ۳: تاریخ و ساعت تولید + کاربر
            html.AppendLine("<tr>")
            html.AppendLine("<td class='report-info' colspan='" & (dt.Columns.Count + 1) & "'>")
            html.AppendLine("🕐 تاریخ و ساعت تولید گزارش: " & GetPersianDateTime(DateTime.Now) &
                            " &nbsp;&nbsp;|&nbsp;&nbsp; " &
                            "👤 تهیه‌کننده: " & Server.HtmlEncode(personName) & " (" & Server.HtmlEncode(personCode) & ")")
            html.AppendLine("</td>")
            html.AppendLine("</tr>")

            ' ردیف ۴: فیلترهای اعمال‌شده (اگر وجود دارند)
            Dim filterText As New StringBuilder()
            If statusInt > 0 Then
                Dim statusText As String = ""
                Select Case statusInt
                    Case 1 : statusText = "در انتظار بررسی"
                    Case 2 : statusText = "نیاز به اصلاح"
                    Case 3 : statusText = "تایید شده"
                    Case 4 : statusText = "رد شده"
                    Case 5 : statusText = "انصراف"
                End Select
                filterText.Append("وضعیت: " & statusText)
            End If
            If Not String.IsNullOrEmpty(typeFilter) Then
                If filterText.Length > 0 Then filterText.Append(" | ")
                filterText.Append("نوع تحصیل: " & typeFilter)
            End If
            If Not String.IsNullOrEmpty(search) Then
                If filterText.Length > 0 Then filterText.Append(" | ")
                filterText.Append("جستجو: " & Server.HtmlEncode(search))
            End If

            If filterText.Length > 0 Then
                html.AppendLine("<tr>")
                html.AppendLine("<td class='report-info' colspan='" & (dt.Columns.Count + 1) & "'>")
                html.AppendLine("🔍 فیلترهای اعمال‌شده: " & filterText.ToString())
                html.AppendLine("</td>")
                html.AppendLine("</tr>")
            End If

            ' ردیف خالی برای فاصله
            html.AppendLine("<tr><td colspan='" & (dt.Columns.Count + 1) & "' style='border:none; height:10px;'></td></tr>")
            html.AppendLine("</table>")

            ' ============================================================
            ' ✅ جدول داده‌ها
            ' ============================================================
            html.AppendLine("<table>")

            ' Header
            html.AppendLine("<thead><tr>")
            For Each col As DataColumn In dt.Columns
                html.AppendLine("<th>" & Server.HtmlEncode(col.ColumnName) & "</th>")
            Next
            html.AppendLine("</tr></thead>")

            ' Body
            html.AppendLine("<tbody>")
            For Each row As DataRow In dt.Rows
                html.AppendLine("<tr>")
                For Each col As DataColumn In dt.Columns
                    Dim val As String = ""
                    Dim cssClass As String = ""

                    If row(col) IsNot DBNull.Value Then
                        If col.ColumnName = "تاریخ ثبت" OrElse col.ColumnName = "تاریخ بررسی" Then
                            Try
                                val = GetPersianDateTime(Convert.ToDateTime(row(col)))
                            Catch
                                val = row(col).ToString()
                            End Try

                        ElseIf col.ColumnName = "مبلغ مقطع" Then
                            ' ✅ فرمت مبلغ با جداکننده هزارگان
                            Dim amount As Decimal = 0
                            If Decimal.TryParse(row(col).ToString(), amount) AndAlso amount > 0 Then
                                val = amount.ToString("#,##0") & " ریال"
                                cssClass = " class='amount-cell'"
                            Else
                                val = "تعیین نشده"
                                cssClass = " class='amount-cell'"
                            End If

                        ElseIf col.ColumnName = "وضعیت" Then
                            val = row(col).ToString()
                            Select Case val
                                Case "تایید شده"
                                    cssClass = " class='status-approved'"
                                Case "رد شده"
                                    cssClass = " class='status-rejected'"
                                Case "در انتظار بررسی", "نیاز به اصلاح"
                                    cssClass = " class='status-pending'"
                            End Select

                        Else
                            val = row(col).ToString()
                        End If
                    End If

                    html.AppendLine("<td" & cssClass & ">" & Server.HtmlEncode(val) & "</td>")
                Next
                html.AppendLine("</tr>")
            Next
            html.AppendLine("</tbody>")

            html.AppendLine("</table>")

            ' ============================================================
            ' ✅ فوتر گزارش
            ' ============================================================
            html.AppendLine("<table style='width:100%; margin-top:15px; border:none;'>")
            html.AppendLine("<tr>")
            html.AppendLine("<td class='report-subheader' colspan='" & (dt.Columns.Count + 1) & "'>")
            html.AppendLine("— پایان گزارش —")
            html.AppendLine("</td>")
            html.AppendLine("</tr>")
            html.AppendLine("</table>")

            html.AppendLine("</body>")
            html.AppendLine("</html>")

            Response.Write(html.ToString())
            Response.Flush()

            ' ✅ پایان تمیز
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Catch ex As Threading.ThreadAbortException
            ' ✅ نادیده گرفتن ThreadAbortException
        Catch ex As Exception
            Try
                Response.Clear()
                Response.ClearHeaders()
                Response.ContentType = "text/html; charset=utf-8"
                Response.Write("<html><head><meta charset='utf-8'></head><body style='font-family:Tahoma;direction:rtl;padding:20px;'>")
                Response.Write("<h2 style='color:red;'>خطا در تولید خروجی Excel</h2>")
                Response.Write("<p><strong>پیام:</strong> " & Server.HtmlEncode(ex.Message) & "</p>")
                If ex.InnerException IsNot Nothing Then
                    Response.Write("<p><strong>جزئیات:</strong> " & Server.HtmlEncode(ex.InnerException.Message) & "</p>")
                End If
                Response.Write("<p><strong>StackTrace:</strong></p>")
                Response.Write("<pre style='background:#f5f5f5;padding:10px;direction:ltr;text-align:left;'>" & Server.HtmlEncode(ex.StackTrace) & "</pre>")
                Response.Write("</body></html>")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Catch
            End Try
        End Try
    End Sub

    ' ============================================================
    ' ✅ بررسی نقش روابط عمومی
    ' ============================================================
    Private Function IsPublicRelationsUser(ByVal personCode As String) As Boolean
        Try
            Dim sql As String =
                "SELECT COUNT(*) FROM personel p " &
                "INNER JOIN omurTbl o ON p.omurID = o.omurID " &
                "WHERE p.codePersoneli = @Code AND (o.omur LIKE N'%روابط عمومی%' OR o.omur LIKE N'%روابط عمومي%')"

            Dim result As Object = ExecuteScalar(sql, New SqlParameter("@Code", personCode))
            Return Convert.ToInt32(result) > 0
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' ✅ توابع کمکی دیتابیس
    ' ============================================================
    Private Function GetDataTable(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Dim paramDict As New Dictionary(Of String, Object)()
        For Each p As SqlParameter In parameters
            paramDict.Add(p.ParameterName, p.Value)
        Next
        Return ClsDataTable.GetDataTable(sql, paramDict)
    End Function

    Private Function ExecuteScalar(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As Object
        Using cnn As New SqlConnection(ClsConnection.GetConn)
            Using cmd As New SqlCommand(sql, cnn)
                For Each p As SqlParameter In parameters
                    cmd.Parameters.Add(p)
                Next
                cnn.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    ' ============================================================
    ' ✅ تبدیل تاریخ به شمسی با ساعت
    ' ============================================================
    Private Function GetPersianDateTime(ByVal dt As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" &
                   pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" &
                   pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c) & " - " &
                   dt.ToString("HH:mm:ss")
        Catch
            Return dt.ToString("yyyy/MM/dd HH:mm:ss")
        End Try
    End Function

End Class