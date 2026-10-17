Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.Globalization

Partial Class ExmUnitDetails
    Inherits System.Web.UI.Page

    Private _unitID As Integer = 0
    Private _messageType As String = "info"
    Private _unitPersonelCode As String = ""   ' کد واحدی که در جدول personel استفاده می‌شود

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' ====== دریافت UnitID از QueryString ======
            If Request.QueryString("UnitID") IsNot Nothing Then
                Integer.TryParse(Request.QueryString("UnitID").ToString(), _unitID)
            End If

            If _unitID <= 0 Then
                ShowMessage("❌ شناسه واحد نامعتبر است", "warning")
                pnlUnitInfo.Visible = False
                Return
            End If

            If Not IsPostBack Then
                LoadUnitInfo()
                LoadUnitCapacities()
                LoadPersonnel()
                LoadUnitCompletionStatus()
            End If

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' متد کمکی برای اجرای کوئری با پارامتر
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
    ' بارگذاری اطلاعات واحد
    ' ============================================================
    Private Sub LoadUnitInfo()
        Try
            Dim sql As String = "SELECT ID, UnitName, IsActive FROM exmUnits WHERE ID = @UnitID"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", _unitID))

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                lblUnitName.Text = row("UnitName").ToString()

                If Convert.ToBoolean(row("IsActive")) Then
                    lblStatus.Text = "<span style='color:#27ae60;font-weight:bold;'>✓ فعال</span>"
                Else
                    lblStatus.Text = "<span style='color:#e74c3c;font-weight:bold;'>✗ غیرفعال</span>"
                End If

                pnlUnitInfo.Visible = True
            Else
                ShowMessage("❌ واحد مورد نظر یافت نشد", "warning")
                pnlUnitInfo.Visible = False
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات واحد: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری ظرفیت‌های واحد
    ' ============================================================
    Private Sub LoadUnitCapacities()
        Try
            Dim sql As String = "SELECT " &
                "    d.ExaminationDate, " &
                "    dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, " &
                "    CASE DATEPART(dw, d.ExaminationDate) " &
                "        WHEN 7 THEN N'شنبه' " &
                "        WHEN 1 THEN N'یکشنبه' " &
                "        WHEN 2 THEN N'دوشنبه' " &
                "        WHEN 3 THEN N'سه‌شنبه' " &
                "        WHEN 4 THEN N'چهارشنبه' " &
                "        WHEN 5 THEN N'پنج‌شنبه' " &
                "        WHEN 6 THEN N'جمعه' " &
                "    END AS DayName, " &
                "    uc.Capacity, " &
                "    ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE r.ExaminationDate = d.ExaminationDate AND r.UnitID = @UnitID AND r.StatusCode NOT IN (6, 7)), 0) AS ReservedCount, " &
                "    uc.Capacity - ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE r.ExaminationDate = d.ExaminationDate AND r.UnitID = @UnitID AND r.StatusCode NOT IN (6, 7)), 0) AS AvailableCapacity " &
                "FROM exmDailyCapacity d " &
                "INNER JOIN exmUnitCapacity uc ON d.ID = uc.CapacityID " &
                "WHERE uc.UnitID = @UnitID " &
                "ORDER BY d.ExaminationDate DESC"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", _unitID))

            gvCapacities.DataSource = dt
            gvCapacities.DataBind()

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری ظرفیت‌ها: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری لیست پرسنل رزرو کننده
    ' ============================================================
    Private Sub LoadPersonnel()
        Try
            Dim sql As String = "SELECT " &
                "    r.ID, " &
                "    r.PersonCode, " &
                "    r.FullName, " &
                "    ISNULL(p.codeMelli, N'') AS NationalCode, " &
                "    ISNULL(r.Mobile, N'') AS Mobile, " &
                "    dbo.FormatDateFa(r.ExaminationDate) AS PersianDate, " &
                "    r.ExaminationDate AS GregorianDate, " &
                "    dbo.FormatDateFa(r.CreateDate) AS RequestDate, " &
                "    r.StatusCode, " &
           "CASE StatusCode " &
"    WHEN 1 THEN N'در انتظار تایید سرپرست' " &
"    WHEN 2 THEN N'تایید سرپرست' " &
"    WHEN 3 THEN N'در انتظار تایید بهداشت' " &
"    WHEN 4 THEN N'تایید نهایی' " &
"    WHEN 6 THEN N'رد شده' " &
"    WHEN 7 THEN N'انجام شده' " &
"    WHEN 8 THEN N'ابطال شده توسط کارشناس' " &
"    ELSE N'نامشخص' " &
"END AS StatusText, " &
"CASE StatusCode " &
"    WHEN 1 THEN 'pending' " &
"    WHEN 2 THEN 'approved' " &
"    WHEN 3 THEN 'pending' " &
"    WHEN 4 THEN 'approved' " &
"    WHEN 6 THEN 'rejected' " &
"    WHEN 7 THEN 'done' " &
"    WHEN 8 THEN 'canceled' " &
"    ELSE 'unknown' " &
"END AS StatusClass " &
                "FROM exmReservationRequests r " &
                "LEFT JOIN personel p ON r.PersonCode = p.codePersoneli " &
                "WHERE r.UnitID = @UnitID " &
                "ORDER BY r.CreateDate DESC"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", _unitID))

            lblTotalReservations.Text = dt.Rows.Count.ToString()

            If dt.Rows.Count > 0 Then
                dt.Columns.Add("RowNumber", GetType(Integer))
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("RowNumber") = i + 1
                Next

                gvPersonnel.DataSource = dt
                gvPersonnel.DataBind()

                pnlPersonnel.Visible = True
                pnlNoPersonnel.Visible = False
            Else
                pnlPersonnel.Visible = False
                pnlNoPersonnel.Visible = True
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری پرسنل: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری آمار تکمیل رزرو پرسنل واحد
    ' ============================================================
    ' ============================================================
    ' بارگذاری آمار تکمیل رزرو پرسنل واحد
    ' ============================================================
    Private Sub LoadUnitCompletionStatus()
        Try
            ' ===== 1. پیدا کردن کد واحد واقعی (UnitCode) از exmUnits =====
            Dim sqlGetUnitCode As String =
            "SELECT UnitCode, UnitName FROM exmUnits WHERE ID = @UnitID"
            Dim dtUnitCode As DataTable = GetDataTable(sqlGetUnitCode, New SqlParameter("@UnitID", _unitID))

            If dtUnitCode.Rows.Count = 0 Then
                ' واحد در exmUnits پیدا نشد
                pnlNotReservedList.Visible = False
                lblNotReservedEmpty.Visible = True
                lblNotReservedEmpty.Text = "⚠️ واحد در سیستم معاینات یافت نشد"
                Return
            End If

            Dim realUnitCode As String = ""
            If dtUnitCode.Rows(0)("UnitCode") IsNot DBNull.Value Then
                realUnitCode = dtUnitCode.Rows(0)("UnitCode").ToString().Trim()
            End If

            If String.IsNullOrEmpty(realUnitCode) Then
                pnlNotReservedList.Visible = False
                lblNotReservedEmpty.Visible = True
                lblNotReservedEmpty.Text = "⚠️ کد واحد در سیستم معاینات ثبت نشده است"
                Return
            End If

            ' ===== 2. تعداد کل پرسنل واحد =====
            Dim sqlTotal As String =
            "SELECT COUNT(*) FROM personel WHERE UnitsCode = @UnitCode"

            Dim totalPersonnel As Integer = 0
            Dim dtTotal As DataTable = GetDataTable(sqlTotal, New SqlParameter("@UnitCode", realUnitCode))

            If dtTotal.Rows.Count > 0 AndAlso dtTotal.Rows(0)(0) IsNot DBNull.Value Then
                totalPersonnel = Convert.ToInt32(dtTotal.Rows(0)(0))
            End If

            ' ===== اگه هیچ پرسنلی در واحد نیست =====
            If totalPersonnel = 0 Then
                lblTotalPersonnel.Text = "0"
                lblReservedCount.Text = "0"
                lblNotReservedCount.Text = "0"
                lblCompletionPercent.Text = "0%"
                progressBarFill.Style("width") = "0%"

                pnlNotReservedList.Visible = False
                lblNotReservedEmpty.Visible = True
                lblNotReservedEmpty.CssClass = "exm-not-reserved-empty"
                lblNotReservedEmpty.Text = "⚠️ هیچ پرسنلی در این واحد ثبت نشده است"
                Return
            End If

            ' ===== 3. تعداد رزروکنندگان =====
            Dim sqlReserved As String =
            "SELECT COUNT(DISTINCT r.PersonCode) " &
            "FROM exmReservationRequests r " &
            "INNER JOIN personel p ON r.PersonCode = p.codePersoneli " &
            "WHERE p.UnitsCode = @UnitCode " &
            "  AND r.StatusCode NOT IN (6, 7)"

            Dim reservedCount As Integer = 0
            Dim dtReserved As DataTable = GetDataTable(sqlReserved, New SqlParameter("@UnitCode", realUnitCode))

            If dtReserved.Rows.Count > 0 AndAlso dtReserved.Rows(0)(0) IsNot DBNull.Value Then
                reservedCount = Convert.ToInt32(dtReserved.Rows(0)(0))
            End If

            ' ===== 4. محاسبه درصد =====
            Dim percent As Double = 0
            If totalPersonnel > 0 Then
                percent = Math.Round((CDbl(reservedCount) / CDbl(totalPersonnel)) * 100, 1)
            End If

            ' ===== 5. نمایش =====
            lblTotalPersonnel.Text = totalPersonnel.ToString()
            lblReservedCount.Text = reservedCount.ToString()
            lblNotReservedCount.Text = (totalPersonnel - reservedCount).ToString()
            lblCompletionPercent.Text = percent.ToString("0.0") & "%"

            progressBarFill.Style("width") = percent.ToString("0.0") & "%"

            ' ===== 6. لیست افرادی که رزرو نکردن =====
            Dim sqlNotReserved As String =
            "SELECT p.codePersoneli, p.nam + ' ' + p.family AS FullName, ISNULL(p.mobile, '') AS Mobile " &
            "FROM personel p " &
            "WHERE p.UnitsCode = @UnitCode " &
            "  AND p.codePersoneli NOT IN ( " &
            "      SELECT DISTINCT r.PersonCode FROM exmReservationRequests r " &
            "      WHERE r.StatusCode NOT IN (6, 7) " &
            "  ) " &
            "ORDER BY p.family, p.nam"

            Dim dtNotReserved As DataTable = GetDataTable(sqlNotReserved, New SqlParameter("@UnitCode", realUnitCode))

            ' ===== بازنشانی استایل پیش‌فرض =====
            lblNotReservedEmpty.CssClass = "exm-not-reserved-empty"
            lblNotReservedEmpty.Style.Clear()

            If dtNotReserved.Rows.Count > 0 Then
                rptNotReserved.DataSource = dtNotReserved
                rptNotReserved.DataBind()
                pnlNotReservedList.Visible = True
                lblNotReservedEmpty.Visible = False
            Else
                pnlNotReservedList.Visible = False
                lblNotReservedEmpty.Visible = True

                ' ===== اگه تعداد رزرو نکرده = 0 ولی رزروکنندگان هم 0 هستن =====
                If reservedCount = 0 Then
                    lblNotReservedEmpty.Text = "⚠️ هیچ رزروی برای این واحد ثبت نشده است"
                    lblNotReservedEmpty.Style("background") = "#fff3cd"
                    lblNotReservedEmpty.Style("color") = "#856404"
                    lblNotReservedEmpty.Style("border-color") = "#ffc107"
                Else
                    lblNotReservedEmpty.Text = "🎉 همه پرسنل این واحد رزرو کرده‌اند!"
                End If
            End If

        Catch ex As Exception
            ShowMessage("⚠️ خطا در بارگذاری آمار واحد: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' کلاس وضعیت ظرفیت
    ' ============================================================
    Protected Function GetCapacityStatusClass(ByVal available As Object, ByVal capacity As Object) As String
        Try
            Dim avail As Integer = Convert.ToInt32(available)
            Dim cap As Integer = Convert.ToInt32(capacity)

            If avail <= 0 Then
                Return "badge-inactive"
            ElseIf avail <= (cap * 0.2) Then
                Return "badge-pending"
            Else
                Return "badge-active"
            End If
        Catch ex As Exception
            Return "badge-active"
        End Try
    End Function

    Protected Function GetCapacityStatusText(ByVal available As Object, ByVal capacity As Object) As String
        Try
            Dim avail As Integer = Convert.ToInt32(available)
            Dim cap As Integer = Convert.ToInt32(capacity)

            If avail <= 0 Then
                Return "✗ تکمیل"
            ElseIf avail <= (cap * 0.2) Then
                Return "⚠️ ظرفیت کم"
            Else
                Return "✓ فعال"
            End If
        Catch ex As Exception
            Return "✓ فعال"
        End Try
    End Function

    ' ============================================================
    ' کلاس وضعیت درخواست
    ' ============================================================
    Protected Function GetStatusClass(ByVal statusCode As Object) As String
        Try
            Dim code As Integer = Convert.ToInt32(statusCode)

            Select Case code
                Case 1, 3
                    Return "badge-pending"
                Case 2, 4, 7
                    Return "badge-approved"
                Case 6
                    Return "badge-rejected"
                Case Else
                    Return "badge-pending"
            End Select
        Catch ex As Exception
            Return "badge-pending"
        End Try
    End Function

    ' ============================================================
    ' صفحه‌بندی پرسنل
    ' ============================================================
    Protected Sub gvPersonnel_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvPersonnel.PageIndexChanging
        gvPersonnel.PageIndex = e.NewPageIndex
        LoadPersonnel()
    End Sub

    ' ============================================================
    ' خروجی اکسل پرسنل (رزروکنندگان + رزرو نکرده‌ها)
    ' ============================================================
    ' ============================================================
    ' خروجی اکسل پرسنل (رزروکنندگان + رزرو نکرده‌ها)
    ' ============================================================
    ' ============================================================
    ' ✅ خروجی اکسل پرسنل (رزروکنندگان + رزرو نکرده‌ها) - اصلاح‌شده
    ' ============================================================
    Protected Sub btnExportPersonnel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportPersonnel.Click
        Try
            ' ===== 1. پیدا کردن کد واحد واقعی =====
            Dim sqlGetUnitCode As String =
            "SELECT UnitCode FROM exmUnits WHERE ID = @UnitID"
            Dim dtUnitCode As DataTable = GetDataTable(sqlGetUnitCode, New SqlParameter("@UnitID", _unitID))

            If dtUnitCode.Rows.Count = 0 OrElse dtUnitCode.Rows(0)("UnitCode") Is DBNull.Value Then
                ShowMessage("⚠️ کد واحد یافت نشد", "warning")
                Return
            End If

            Dim realUnitCode As String = dtUnitCode.Rows(0)("UnitCode").ToString().Trim()

            ' ============================================================
            ' ✅ اصلاح: کوئری با JOIN به‌جای زیرکوئری چندستونه
            ' ============================================================
            Dim sql As New StringBuilder()

            sql.AppendLine("SELECT ")
            sql.AppendLine("    p.codePersoneli AS [کد پرسنلی], ")
            sql.AppendLine("    p.nam + ' ' + p.family AS [نام و نام خانوادگی], ")
            sql.AppendLine("    ISNULL(p.codeMelli, N'') AS [کد ملی], ")
            sql.AppendLine("    ISNULL(p.mobile, N'') AS [موبایل], ")

            ' ===== وضعیت رزرو =====
            sql.AppendLine("    CASE WHEN Res.ID IS NOT NULL THEN N'✅ رزرو کرده' ELSE N'⏳ رزرو نکرده' END AS [وضعیت رزرو], ")

            ' ===== تاریخ معاینه =====
            sql.AppendLine("    ISNULL(dbo.FormatDateFa(Res.ExaminationDate), N'-') AS [تاریخ معاینه], ")

            ' ===== وضعیت درخواست =====
            sql.AppendLine("    CASE ")
            sql.AppendLine("        WHEN Res.StatusCode IS NULL THEN N'-' ")
            sql.AppendLine("        WHEN Res.StatusCode = 1 THEN N'در انتظار تایید سرپرست' ")
            sql.AppendLine("        WHEN Res.StatusCode = 2 THEN N'تایید سرپرست' ")
            sql.AppendLine("        WHEN Res.StatusCode = 3 THEN N'در انتظار تایید بهداشت' ")
            sql.AppendLine("        WHEN Res.StatusCode = 4 THEN N'تایید نهایی' ")
            sql.AppendLine("        WHEN Res.StatusCode = 6 THEN N'رد شده' ")
            sql.AppendLine("        WHEN Res.StatusCode = 7 THEN N'انجام شده' ")
            sql.AppendLine("        WHEN Res.StatusCode = 8 THEN N'ابطال شده توسط کارشناس' ")
            sql.AppendLine("        ELSE N'نامشخص' ")
            sql.AppendLine("    END AS [وضعیت درخواست] ")

            sql.AppendLine("FROM personel p ")

            ' ===== LEFT JOIN به آخرین رزرو فعال هر پرسنل =====
            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT r.PersonCode, r.ExaminationDate, r.StatusCode, ")
            sql.AppendLine("           ROW_NUMBER() OVER (PARTITION BY r.PersonCode ORDER BY r.CreateDate DESC) AS rn ")
            sql.AppendLine("    FROM exmReservationRequests r ")
            sql.AppendLine("    WHERE r.StatusCode NOT IN (6, 7, 8) ")
            sql.AppendLine(") AS Res ON Res.PersonCode = p.codePersoneli AND Res.rn = 1 ")

            sql.AppendLine("WHERE p.UnitsCode = @UnitCode ")
            sql.AppendLine("ORDER BY ")
            sql.AppendLine("    CASE WHEN Res.ID IS NOT NULL THEN 1 ELSE 0 END, ")  ' ← خطا: Res.ID وجود ندارد
            sql.AppendLine("    p.family, p.nam")

            ' ===== اصلاح ORDER BY =====
            sql = New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    p.codePersoneli AS [کد پرسنلی], ")
            sql.AppendLine("    p.nam + ' ' + p.family AS [نام و نام خانوادگی], ")
            sql.AppendLine("    ISNULL(p.codeMelli, N'') AS [کد ملی], ")
            sql.AppendLine("    ISNULL(p.mobile, N'') AS [موبایل], ")
            sql.AppendLine("    CASE WHEN Res.PersonCode IS NOT NULL THEN N'✅ رزرو کرده' ELSE N'⏳ رزرو نکرده' END AS [وضعیت رزرو], ")
            sql.AppendLine("    ISNULL(dbo.FormatDateFa(Res.ExaminationDate), N'-') AS [تاریخ معاینه], ")
            sql.AppendLine("    CASE ")
            sql.AppendLine("        WHEN Res.StatusCode IS NULL THEN N'-' ")
            sql.AppendLine("        WHEN Res.StatusCode = 1 THEN N'در انتظار تایید سرپرست' ")
            sql.AppendLine("        WHEN Res.StatusCode = 2 THEN N'تایید سرپرست' ")
            sql.AppendLine("        WHEN Res.StatusCode = 3 THEN N'در انتظار تایید بهداشت' ")
            sql.AppendLine("        WHEN Res.StatusCode = 4 THEN N'تایید نهایی' ")
            sql.AppendLine("        WHEN Res.StatusCode = 6 THEN N'رد شده' ")
            sql.AppendLine("        WHEN Res.StatusCode = 7 THEN N'انجام شده' ")
            sql.AppendLine("        WHEN Res.StatusCode = 8 THEN N'ابطال شده توسط کارشناس' ")
            sql.AppendLine("        ELSE N'نامشخص' ")
            sql.AppendLine("    END AS [وضعیت درخواست] ")
            sql.AppendLine("FROM personel p ")
            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT r.PersonCode, r.ExaminationDate, r.StatusCode, ")
            sql.AppendLine("           ROW_NUMBER() OVER (PARTITION BY r.PersonCode ORDER BY r.CreateDate DESC) AS rn ")
            sql.AppendLine("    FROM exmReservationRequests r ")
            sql.AppendLine("    WHERE r.StatusCode NOT IN (6, 7, 8) ")
            sql.AppendLine(") AS Res ON Res.PersonCode = p.codePersoneli AND Res.rn = 1 ")
            sql.AppendLine("WHERE p.UnitsCode = @UnitCode ")
            sql.AppendLine("ORDER BY ")
            sql.AppendLine("    CASE WHEN Res.PersonCode IS NOT NULL THEN 1 ELSE 0 END, ")
            sql.AppendLine("    p.family, p.nam")

            Dim dt As DataTable = GetDataTable(sql.ToString(), New SqlParameter("@UnitCode", realUnitCode))

            If dt.Rows.Count = 0 Then
                ShowMessage("⚠️ هیچ داده‌ای برای خروجی وجود ندارد", "warning")
                Return
            End If

            ' ============================================================
            ' ✅ ساخت خروجی Excel با نام فارسی شمسی
            ' ============================================================
            Dim pc As New PersianCalendar()
            Dim now As DateTime = DateTime.Now
            Dim persianDateForFile As String = pc.GetYear(now).ToString() & "-" & _
                                               pc.GetMonth(now).ToString().PadLeft(2, "0"c) & "-" & _
                                               pc.GetDayOfMonth(now).ToString().PadLeft(2, "0"c) & "_" & _
                                               now.Hour.ToString().PadLeft(2, "0"c) & "-" & _
                                               now.Minute.ToString().PadLeft(2, "0"c)

            Dim fileName As String = "PersonnelReservation_" & persianDateForFile & ".xls"

            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.UTF8
            Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
            Response.ContentType = "application/vnd.ms-excel"

            Dim sb As New StringBuilder()

            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml' dir='rtl'>")
            sb.Append("<head>")
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />")
            sb.Append("<style>")
            sb.Append("table { border-collapse: collapse; width: 100%; font-family: Tahoma, Arial; direction: rtl; }")
            sb.Append("th { background-color: #2c3e50; color: #FFFFFF; padding: 10px; border: 1px solid #ccc; text-align: center; font-size: 13px; }")
            sb.Append("td { padding: 8px; border: 1px solid #ddd; text-align: center; font-size: 12px; color: #000000; }")
            sb.Append("tr:nth-child(even) { background-color: #f8f9fa; }")
            sb.Append(".header { text-align: center; font-size: 18px; font-weight: bold; padding: 15px; background-color: #e8f4fd; color: #2c3e50; }")
            sb.Append(".sub-header { text-align: center; font-size: 14px; padding: 10px; background-color: #d1ecf1; }")
            sb.Append(".reserved { background-color: #d4edda; color: #155724; font-weight: bold; }")
            sb.Append(".not-reserved { background-color: #fff3cd; color: #856404; font-weight: bold; }")
            sb.Append("</style>")
            sb.Append("</head>")
            sb.Append("<body>")

            sb.Append("<div class='header'>📊 گزارش وضعیت رزرو پرسنل واحد</div>")
            sb.Append("<div class='sub-header'>")
            sb.Append("واحد: <b>" & Server.HtmlEncode(lblUnitName.Text) & "</b> | ")
            sb.Append("تاریخ گزارش: <b>" & FormatDateFaPublic(DateTime.Now) & "</b> | ")
            sb.Append("تعداد کل پرسنل: <b>" & dt.Rows.Count.ToString() & "</b>")
            sb.Append("</div>")

            sb.Append("<br/>")
            sb.Append("<table>")
            sb.Append("<thead><tr>")
            For Each col As DataColumn In dt.Columns
                sb.Append("<th>" & col.ColumnName & "</th>")
            Next
            sb.Append("</tr></thead><tbody>")

            For Each dr As DataRow In dt.Rows
                sb.Append("<tr>")
                For Each col As DataColumn In dt.Columns
                    Dim cellValue As String = If(dr(col) Is DBNull.Value, "-", dr(col).ToString())
                    Dim cellClass As String = ""

                    If col.ColumnName = "وضعیت رزرو" Then
                        If cellValue.Contains("رزرو کرده") Then
                            cellClass = " class='reserved'"
                        Else
                            cellClass = " class='not-reserved'"
                        End If
                    End If

                    sb.Append("<td" & cellClass & ">" & Server.HtmlEncode(cellValue) & "</td>")
                Next
                sb.Append("</tr>")
            Next

            sb.Append("</tbody></table>")
            sb.Append("</body></html>")

            Response.Write(sb.ToString())
            Response.Flush()
            Response.End()

        Catch ex As Exception
            ShowMessage("خطا در خروجی اکسل: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' متد کمکی فرمت تاریخ فارسی
    ' ============================================================
    Private Function FormatDateFaPublic(ByVal dt As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString("0000") & "/" &
                   pc.GetMonth(dt).ToString("00") & "/" &
                   pc.GetDayOfMonth(dt).ToString("00")
        Catch
            Return dt.ToString("yyyy/MM/dd")
        End Try
    End Function

    ' ============================================================
    ' بازگشت هوشمند
    ' ============================================================
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            'Dim fromPage As String = ""

            '' ====== خواندن از Session ======
            'If Session("ExmUnitDetails_From") IsNot Nothing Then
            '    fromPage = Session("ExmUnitDetails_From").ToString()
            'End If

            ' ====== بررسی و هدایت ======
            If Session("ExmUnitDetails_From") = "2" Then
                Response.Redirect("ExmUnitReport.aspx")
            ElseIf Session("ExmUnitDetails_From") = "1" Then

                Response.Redirect("ExaminationSetCapacity.aspx")
            End If

        Catch ex As Exception
            'Response.Redirect("ExaminationSetCapacity.aspx")
        End Try
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
                lblMessageIcon.Text = "✅ "
                divMessage.Attributes("class") = "alert alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌ "
                divMessage.Attributes("class") = "alert alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️ "
                divMessage.Attributes("class") = "alert alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️ "
                divMessage.Attributes("class") = "alert alert-info"
        End Select
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class