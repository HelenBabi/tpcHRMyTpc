Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization
Imports System.Collections.Generic

Partial Class ExaminationRequest
    Inherits System.Web.UI.Page

    Private _personCode As String = ""
    Private _unitCode As String = ""
    Private _exmUnitID As Integer = 0
    Private _deadlineDate As DateTime? = Nothing
    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("super_vizer") IsNot Nothing AndAlso Session("super_vizer") = False Then
            meno_acc()
        End If

        If Session("codePersoneli") IsNot Nothing Then
            _personCode = Session("codePersoneli").ToString()
        End If

        If Session("unitID") IsNot Nothing Then
            _unitCode = Session("unitID").ToString().Trim()
        End If

        If Not String.IsNullOrEmpty(_unitCode) Then
            _exmUnitID = GetExmUnitIDByUnitCode(_unitCode)
        End If

        If _exmUnitID > 0 Then
            _deadlineDate = GetDeadlineDateForUnit(_exmUnitID)
        End If

        If Not IsPostBack Then
            If String.IsNullOrEmpty(_personCode) Then
                Response.Redirect("~/Login_2.aspx")
                Return
            End If

            ' ============ تاریخچه اول بارگذاری شود ============
            LoadRequestHistory()

            If _exmUnitID <= 0 Then
                btnSubmit.Enabled = False
                Return
            End If

            DisplayDeadline()

            If IsDeadlineExpired() Then
                ShowMessage("⚠️ مهلت رزرو برای واحد شما به پایان رسیده است. امکان ثبت درخواست وجود ندارد. اما می‌توانید تاریخچه درخواست‌های قبلی خود را در پایین صفحه مشاهده کنید.", "danger")
                btnSubmit.Enabled = False
                Return
            End If

            LoadAvailableDates()
        End If
    End Sub

    Private Sub meno_acc()
        Try
            Dim meno_arr(10) As Boolean
            meno_arr = ClsAccounting.meno_acc("ExaminationRequest.aspx", Val(Session("UserId")))

            btnBack.Visible = True
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' متدهای کمکی
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

    Private Function GetExmUnitIDByUnitCode(ByVal unitCode As String) As Integer
        Try
            If String.IsNullOrEmpty(unitCode) Then
                Return 0
            End If

            Dim sql As String = "SELECT ID FROM exmUnits WHERE UnitCode = @UnitCode AND IsActive = 1"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitCode", unitCode))

            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If

            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetDeadlineDateForUnit(ByVal exmUnitID As Integer) As DateTime?
        Try
            If exmUnitID <= 0 Then
                Return Nothing
            End If

            Dim sql As String = "SELECT DeadlineDate FROM exmUnits WHERE ID = @UnitID"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", exmUnitID))

            If dt.Rows.Count > 0 AndAlso dt.Rows(0)("DeadlineDate") IsNot DBNull.Value Then
                Return Convert.ToDateTime(dt.Rows(0)("DeadlineDate"))
            End If

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function IsDeadlineExpired() As Boolean
        Try
            If Not _deadlineDate.HasValue Then
                Return False
            End If

            Dim today As DateTime = DateTime.Now.Date
            Return today > _deadlineDate.Value.Date
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub DisplayDeadline()
        Try
            If Not _deadlineDate.HasValue Then
                pnlDeadline.Visible = False
                Return
            End If

            Dim persianDeadline As String = GetPersianDate(_deadlineDate.Value.ToString("yyyy-MM-dd"))

            lblDeadlineDate.Text = persianDeadline

            If IsDeadlineExpired() Then
                pnlDeadline.CssClass = "exm-deadline-box expired"
                lblDeadlineStatus.Text = "<span class='deadline-expired'>⛔ مهلت به پایان رسیده است</span>"
            Else
                Dim daysLeft As Integer = (_deadlineDate.Value.Date - DateTime.Now.Date).Days
                pnlDeadline.CssClass = "exm-deadline-box"

                If daysLeft = 0 Then
                    lblDeadlineStatus.Text = "<span class='deadline-ok'>⚠️ آخرین روز مهلت رزرو</span>"
                Else
                    lblDeadlineStatus.Text = "<span class='deadline-ok'>✅ " & daysLeft & " روز باقی‌مانده</span>"
                End If
            End If

            pnlDeadline.Visible = True

        Catch ex As Exception
            pnlDeadline.Visible = False
        End Try
    End Sub

    Private Function GetUnitApproveBy(ByVal exmUnitID As Integer) As Integer
        Try
            If exmUnitID <= 0 Then Return 1

            Dim sql As String = "SELECT ISNULL(ApproveBy, 1) AS ApproveBy FROM exmUnits WHERE ID = @UnitID"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", exmUnitID))

            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)("ApproveBy"))
            End If

            Return 1
        Catch ex As Exception
            Return 1
        End Try
    End Function

    Private Function GetSupervisorInfo(ByVal personCode As String) As DataTable
        Try
            Dim sql As String = "SELECT TOP 1 p.codePersoneli AS SupervisorCode, " &
                "       p.nam + ' ' + p.family AS SupervisorName " &
                "FROM vwRaya v " &
                "INNER JOIN personel p ON v.ParentCode = p.codePersoneli " &
                "WHERE v.Code = @PersonCode"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@PersonCode", personCode))

            If dt.Rows.Count > 0 Then
                Return dt
            End If

            Dim sql2 As String = "SELECT TOP 1 ISNULL(supervizer, '') AS SupervisorCode, " &
                "       (SELECT TOP 1 nam + ' ' + family FROM personel WHERE codePersoneli = p.supervizer) AS SupervisorName " &
                "FROM personel p WHERE p.codePersoneli = @PersonCode"

            Return GetDataTable(sql2, New SqlParameter("@PersonCode", personCode))
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Private Function GetUnitManagerInfo(ByVal exmUnitID As Integer) As DataTable
        Try
            Dim sql As String = "SELECT TOP 1 " &
                                "ISNULL(ManagerCode, '') AS ManagerCode, " &
                                "ISNULL(ManagerName, '') AS ManagerName " &
                                "FROM exmUnits WHERE ID = @UnitID"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", exmUnitID))

            If dt.Rows.Count > 0 AndAlso String.IsNullOrEmpty(dt.Rows(0)("ManagerCode").ToString()) Then
                Dim sqlPersonel As String = "SELECT TOP 1 " &
                                            "ISNULL(p.codePersoneli, '') AS ManagerCode, " &
                                            "ISNULL(p.nam + ' ' + p.family, '') AS ManagerName " &
                                            "FROM Personel p " &
                                            "INNER JOIN exmUnits eu ON eu.UnitCode = CAST(p.UnitsCode AS NVARCHAR(50)) " &
                                            "WHERE eu.ID = @UnitID AND p.modir = 1 AND ISNULL(p.CodeClosed,0) = 0"

                Dim dtPersonel As DataTable = GetDataTable(sqlPersonel, New SqlParameter("@UnitID", exmUnitID))

                If dtPersonel.Rows.Count > 0 Then
                    Return dtPersonel
                End If
            End If

            Return dt
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    ' ============================================================
    ' ظرفیت واحد در تاریخ مشخص
    ' ============================================================
    Private Function GetUnitCapacityForDate(ByVal exmUnitID As Integer, ByVal examinationDate As String) As Integer
        Try
            If exmUnitID <= 0 Then
                Return 0
            End If

            Dim sql As String = "SELECT ISNULL(uc.Capacity, 0) - ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE r.ExaminationDate = @ExaminationDate AND r.UnitID = @UnitID AND r.StatusCode NOT IN (6, 7, 8)), 0) AS AvailableUnitCapacity FROM exmDailyCapacity c INNER JOIN exmUnitCapacity uc ON c.ID = uc.CapacityID WHERE c.ExaminationDate = @ExaminationDate AND c.IsActive = 1 AND uc.UnitID = @UnitID"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@ExaminationDate", examinationDate), New SqlParameter("@UnitID", exmUnitID))

            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If
            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function CheckUnitCapacityForDate(ByVal exmUnitID As Integer, ByVal examinationDate As String) As Boolean
        Try
            Return GetUnitCapacityForDate(exmUnitID, examinationDate) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetUnitName(ByVal exmUnitID As Integer) As String
        Try
            If exmUnitID <= 0 Then
                Return "نامشخص"
            End If

            Dim sql As String = "SELECT UnitName FROM exmUnits WHERE ID = @UnitID"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@UnitID", exmUnitID))

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0).ToString()
            End If

            Return "واحد " & exmUnitID.ToString()
        Catch ex As Exception
            Return "واحد " & exmUnitID.ToString()
        End Try
    End Function

    Private Function GetPersianDate(ByVal gregorianDate As String) As String
        Try
            Dim dt As DateTime = Convert.ToDateTime(gregorianDate)
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" & pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" & pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c)
        Catch ex As Exception
            Return gregorianDate
        End Try
    End Function

    Private Function GetPersianYear(ByVal gregorianDate As String) As String
        Try
            Dim dt As DateTime = Convert.ToDateTime(gregorianDate)
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function CheckReservationInYear(ByVal personCode As String, ByVal examinationDate As String, ByRef reservationDate As String) As Boolean
        Try
            Dim sql As String = "SELECT TOP 1 dbo.FormatDateFa(ExaminationDate) AS PersianDate FROM exmReservationRequests WHERE PersonCode = @PersonCode AND YEAR(ExaminationDate) = YEAR(@ExaminationDate) AND StatusCode NOT IN (6, 7, 8) ORDER BY CreateDate DESC"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@PersonCode", personCode), New SqlParameter("@ExaminationDate", examinationDate))

            If dt.Rows.Count > 0 Then
                reservationDate = dt.Rows(0)("PersianDate").ToString()
                Return True
            End If

            reservationDate = ""
            Return False
        Catch ex As Exception
            reservationDate = ""
            Return False
        End Try
    End Function

    ' ============================================================
    ' بارگذاری کارت‌های تاریخ
    ' ✅ فقط ظرفیت واحد کاربر ملاک است
    ' ============================================================
    Private Sub LoadAvailableDates()
        Try
            Dim sqlstr As String = "SELECT " &
                               "    c.ExaminationDate, " &
                               "    dbo.FormatDateFa(c.ExaminationDate) AS PersianDate, " &
                               "    CONVERT(NVARCHAR(10), c.ExaminationDate, 120) AS GregorianDate, " &
                               "    CASE DATEPART(dw, c.ExaminationDate) " &
                               "        WHEN 7 THEN N'شنبه' " &
                               "        WHEN 1 THEN N'یکشنبه' " &
                               "        WHEN 2 THEN N'دوشنبه' " &
                               "        WHEN 3 THEN N'سه‌شنبه' " &
                               "        WHEN 4 THEN N'چهارشنبه' " &
                               "        WHEN 5 THEN N'پنج‌شنبه' " &
                               "        WHEN 6 THEN N'جمعه' " &
                               "    END AS DayName, " &
                               "    ISNULL((SELECT TOP 1 uc.Capacity FROM exmUnitCapacity uc WHERE uc.CapacityID = c.ID AND uc.UnitID = @UnitID), 0) AS MyUnitCapacity, " &
                               "    ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE r.ExaminationDate = c.ExaminationDate AND r.UnitID = @UnitID AND r.StatusCode NOT IN (6, 7, 8)), 0) AS MyUnitReservedCount " &
                               "FROM exmDailyCapacity c " &
                               "WHERE c.ExaminationDate >= CAST(GETDATE() AS DATE) " &
                               "AND c.IsActive = 1 " &
                               "AND DATEPART(dw, c.ExaminationDate) IN (7, 1, 2) " &
                               "ORDER BY c.ExaminationDate"

            Dim dt As DataTable = GetDataTable(sqlstr, New SqlParameter("@UnitID", _exmUnitID))

            If dt.Rows.Count > 0 Then
                If Not dt.Columns.Contains("TotalAvailable") Then
                    dt.Columns.Add("TotalAvailable", GetType(Integer))
                End If
                If Not dt.Columns.Contains("IsFull") Then
                    dt.Columns.Add("IsFull", GetType(Boolean))
                End If
                If Not dt.Columns.Contains("UnitsList") Then
                    dt.Columns.Add("UnitsList", GetType(String))
                End If
                If Not dt.Columns.Contains("PersianDateShort") Then
                    dt.Columns.Add("PersianDateShort", GetType(String))
                End If

                For Each row As DataRow In dt.Rows
                    Dim examDate As String = row("GregorianDate").ToString()

                    ' ====== فقط ظرفیت واحد کاربر ======
                    Dim myUnitCap As Integer = 0
                    Dim myUnitReserved As Integer = 0

                    If row("MyUnitCapacity") IsNot DBNull.Value Then
                        myUnitCap = Convert.ToInt32(row("MyUnitCapacity"))
                    End If
                    If row("MyUnitReservedCount") IsNot DBNull.Value Then
                        myUnitReserved = Convert.ToInt32(row("MyUnitReservedCount"))
                    End If

                    Dim myUnitAvailable As Integer = myUnitCap - myUnitReserved
                    If myUnitAvailable < 0 Then myUnitAvailable = 0

                    ' ====== ✅ فقط اگر واحد کاربر ظرفیت نداشت، غیرفعال شود ======
                    Dim isFull As Boolean = (myUnitAvailable <= 0)

                    row("TotalAvailable") = myUnitAvailable
                    row("IsFull") = isFull

                    ' ====== تاریخ کوتاه ======
                    Dim fullPersian As String = row("PersianDate").ToString()
                    If fullPersian.Length >= 10 Then
                        row("PersianDateShort") = fullPersian.Substring(0, 10)
                    Else
                        row("PersianDateShort") = fullPersian
                    End If

                    row("UnitsList") = GetUnitsListHtmlForDate(examDate)
                Next

                rptDates.DataSource = dt
                rptDates.DataBind()
                pnlNoDates.Visible = False
            Else
                pnlNoDates.Visible = True
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری تاریخ‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دریافت لیست واحدها برای نمایش در کارت
    ' ============================================================
    Private Function GetUnitsListHtmlForDate(ByVal examinationDate As String) As String
        Try
            Dim sql As String = "SELECT u.UnitName, uc.Capacity AS TotalCapacity, " &
                "ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE r.ExaminationDate = @ExaminationDate AND r.UnitID = uc.UnitID AND r.StatusCode NOT IN (6, 7, 8)), 0) AS ReservedCount " &
                "FROM exmDailyCapacity c " &
                "INNER JOIN exmUnitCapacity uc ON c.ID = uc.CapacityID " &
                "INNER JOIN exmUnits u ON uc.UnitID = u.ID " &
                "WHERE c.ExaminationDate = @ExaminationDate AND c.IsActive = 1 AND uc.Capacity > 0 " &
                "ORDER BY u.UnitName"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@ExaminationDate", examinationDate))

            Dim sb As New StringBuilder()

            For Each row As DataRow In dt.Rows
                Dim unitName As String = row("UnitName").ToString()
                Dim totalCap As Integer = Convert.ToInt32(row("TotalCapacity"))
                Dim reservedCount As Integer = Convert.ToInt32(row("ReservedCount"))
                Dim available As Integer = totalCap - reservedCount
                If available < 0 Then available = 0

                Dim cssClass As String = If(available > 0, "unit-item", "unit-item full")

                sb.Append("<span class='" & cssClass & "'>")
                sb.Append(unitName & ": " & available)
                sb.Append("</span>")
            Next

            Return sb.ToString()

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Function GetDateCardClass(ByVal isFull As Object) As String
        Try
            If Convert.ToBoolean(isFull) Then
                Return "exm-date-card full"
            Else
                Return "exm-date-card"
            End If
        Catch ex As Exception
            Return "exm-date-card"
        End Try
    End Function

    Protected Function GetStatusBadgeClass(ByVal isFull As Object) As String
        Try
            If Convert.ToBoolean(isFull) Then
                Return "badge-full"
            Else
                Return "badge-available"
            End If
        Catch ex As Exception
            Return "badge-available"
        End Try
    End Function

    Protected Function GetUnitsListHtml(ByVal unitsList As Object) As String
        Try
            If unitsList Is Nothing OrElse unitsList Is DBNull.Value Then
                Return ""
            End If
            Return unitsList.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub rptDates_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                ' نیازی به کار اضافه نیست
            End If
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری تاریخچه درخواست‌ها
    ' ============================================================
    Private Sub LoadRequestHistory()
        Try
            Dim sql As String = "SELECT TOP 5 ID, " &
                "ExaminationDate, " &
                "dbo.FormatDateFa(ExaminationDate) AS PersianDate, " &
                "dbo.FormatDateFa(ExaminationDate) AS ExamPersianDate, " &
                "dbo.FormatDateFa(CreateDate) AS CreatePersianDate, " &
                "StatusCode, StatusDescription, " &
                "CASE StatusCode " &
                "    WHEN 1 THEN N'در انتظار تایید سرپرست' " &
                "    WHEN 2 THEN N'تایید سرپرست' " &
                "    WHEN 3 THEN N'در انتظار تایید بهداشت' " &
                "    WHEN 4 THEN N'تایید نهایی' " &
                "    WHEN 6 THEN N'رد شده' " &
                "    WHEN 7 THEN N'انجام شده' " &
                "    WHEN 8 THEN N'ابطال شده' " &
                "    ELSE N'نامشخص' " &
                "END AS StatusText, " &
                "CASE StatusCode " &
                "    WHEN 1 THEN 'pending' " &
                "    WHEN 2 THEN 'approved' " &
                "    WHEN 3 THEN 'pending' " &
                "    WHEN 4 THEN 'approved' " &
                "    WHEN 6 THEN 'rejected' " &
                "    WHEN 7 THEN 'done' " &
                "    WHEN 8 THEN 'rejected' " &
                "    ELSE 'unknown' " &
                "END AS StatusClass " &
                "FROM exmReservationRequests " &
                "WHERE PersonCode = @PersonCode " &
                "ORDER BY CreateDate DESC"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@PersonCode", _personCode))

            If dt.Rows.Count > 0 Then
                If Not dt.Columns.Contains("ExamPersianDateShort") Then
                    dt.Columns.Add("ExamPersianDateShort", GetType(String))
                End If

                For Each row As DataRow In dt.Rows
                    Dim fullDate As String = ""
                    If row("ExamPersianDate") IsNot DBNull.Value Then
                        fullDate = row("ExamPersianDate").ToString()
                    End If

                    If fullDate.Length >= 10 Then
                        row("ExamPersianDateShort") = fullDate.Substring(0, 10)
                    Else
                        row("ExamPersianDateShort") = fullDate
                    End If
                Next

                rptHistory.DataSource = dt
                rptHistory.DataBind()
                pnlHistory.Visible = True
                pnlNoHistory.Visible = False
            Else
                pnlHistory.Visible = False
                pnlNoHistory.Visible = True
            End If

        Catch ex As Exception
            pnlHistory.Visible = False
            pnlNoHistory.Visible = True
        End Try
    End Sub

    ' ============================================================
    ' رویداد ثبت درخواست
    ' ============================================================
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            btnSubmit.Enabled = False
            btnSubmit.Text = "⏳ در حال ثبت..."

            If IsDeadlineExpired() Then
                ShowMessage("⚠️ مهلت رزرو برای واحد شما به پایان رسیده است. امکان ثبت درخواست وجود ندارد.", "danger")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            Dim personCode As String = Session("codePersoneli").ToString()
            Dim fullName As String = Session("FullName").ToString()
            Dim mobile As String = If(Session("mobile") IsNot Nothing, Session("mobile").ToString(), "")
            Dim userID As String = Session("userid").ToString()

            If Session("unitID") IsNot Nothing Then
                _unitCode = Session("unitID").ToString().Trim()
            End If

            _exmUnitID = GetExmUnitIDByUnitCode(_unitCode)

            ' ============================================================
            ' بررسی نوع تاییدکننده (سرپرست یا رییس)
            ' ============================================================
            Dim approveBy As Integer = GetUnitApproveBy(_exmUnitID)

            Dim supervisorCode As String = ""
            Dim supervisorName As String = ""

            If approveBy = 2 Then
                ' ====== واحد نیاز به تایید رییس دارد ======
                Try
                    Dim dtManager As DataTable = GetUnitManagerInfo(_exmUnitID)
                    If dtManager.Rows.Count > 0 Then
                        If dtManager.Rows(0)("ManagerCode") IsNot DBNull.Value Then
                            supervisorCode = dtManager.Rows(0)("ManagerCode").ToString()
                        End If
                        If dtManager.Rows(0)("ManagerName") IsNot DBNull.Value Then
                            supervisorName = dtManager.Rows(0)("ManagerName").ToString()
                        End If
                    End If
                Catch ex As Exception
                End Try
            Else
                ' ====== واحد نیاز به تایید سرپرست دارد ======
                Try
                    Dim dtSupervisor As DataTable = GetSupervisorInfo(personCode)
                    If dtSupervisor.Rows.Count > 0 Then
                        If dtSupervisor.Rows(0)("SupervisorCode") IsNot DBNull.Value Then
                            supervisorCode = dtSupervisor.Rows(0)("SupervisorCode").ToString()
                        End If
                        If dtSupervisor.Rows(0)("SupervisorName") IsNot DBNull.Value Then
                            supervisorName = dtSupervisor.Rows(0)("SupervisorName").ToString()
                        End If
                    End If
                Catch ex As Exception
                End Try
            End If

            Dim unitName As String = GetUnitName(_exmUnitID)

            ' ====== دریافت تاریخ از HiddenField ======
            Dim examinationDate As String = hdnSelectedDate.Value.Trim()

            If String.IsNullOrEmpty(examinationDate) Then
                ShowMessage("لطفاً تاریخ معاینه را انتخاب کنید", "warning")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            If String.IsNullOrEmpty(personCode) Then
                ShowMessage("❌ کد پرسنلی یافت نشد", "danger")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            If String.IsNullOrEmpty(fullName) Then
                ShowMessage("❌ اطلاعات پرسنل کامل نیست", "danger")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            If _exmUnitID <= 0 Then
                ShowMessage("❌ واحد سازمانی شما در سیستم معاینات یافت نشد", "danger")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            If Not CheckUnitCapacityForDate(_exmUnitID, examinationDate) Then
                ShowMessage("⚠️ واحد " & unitName & " در تاریخ " & GetPersianDate(examinationDate) & " ظرفیت ندارد.", "warning")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            Dim reservationDate As String = ""
            If CheckReservationInYear(personCode, examinationDate, reservationDate) Then
                ShowMessage("⚠️ شما برای سال جاری (" & GetPersianYear(examinationDate) & ") تاریخ " & reservationDate & " رزرو کردید.", "warning")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
                Return
            End If

            ' ============================================================
            ' تعیین StatusDescription و CurrentLevel
            ' ============================================================
            Dim initialStatusCode As Integer = 1
            Dim initialStatusDesc As String = "در انتظار تایید سرپرست"
            Dim initialCurrentLevel As Integer = 1

            If approveBy = 2 Then
                initialStatusDesc = "در انتظار تایید رییس واحد"
            End If

            Dim newID As Integer = 0

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                cnn.Open()

                Using transaction As SqlTransaction = cnn.BeginTransaction()
                    Try
                        Dim query As String = ""
                        query = query & "INSERT INTO exmReservationRequests ("
                        query = query & "PersonCode, FullName, UnitCode, UnitName, Position, Mobile, "
                        query = query & "ExaminationDate, CenterID, RequestDate, SupervisorCode, SupervisorName, "
                        query = query & "ManagerCode, ManagerName, StatusCode, StatusDescription, "
                        query = query & "UnitID, CreateUserID, CreateDate, "
                        query = query & "IsDone, ReminderSent, CurrentLevel"
                        query = query & ") VALUES ("
                        query = query & "@PersonCode, @FullName, @UnitCode, @UnitName, @Position, @Mobile, "
                        query = query & "@ExaminationDate, @CenterID, @RequestDate, @SupervisorCode, @SupervisorName, "
                        query = query & "@ManagerCode, @ManagerName, @StatusCode, @StatusDescription, "
                        query = query & "@UnitID, @CreateUserID, @CreateDate, "
                        query = query & "@IsDone, @ReminderSent, @CurrentLevel"
                        query = query & "); SELECT SCOPE_IDENTITY();"

                        Using cmd As New SqlCommand(query, cnn, transaction)
                            cmd.CommandTimeout = 120

                            cmd.Parameters.AddWithValue("@PersonCode", personCode)
                            cmd.Parameters.AddWithValue("@FullName", fullName)
                            cmd.Parameters.AddWithValue("@UnitCode", If(String.IsNullOrEmpty(_unitCode), DBNull.Value, _unitCode))
                            cmd.Parameters.AddWithValue("@UnitName", If(String.IsNullOrEmpty(unitName), DBNull.Value, unitName))
                            cmd.Parameters.AddWithValue("@Position", DBNull.Value)
                            cmd.Parameters.AddWithValue("@Mobile", If(String.IsNullOrEmpty(mobile), DBNull.Value, mobile))
                            cmd.Parameters.AddWithValue("@ExaminationDate", Convert.ToDateTime(examinationDate))
                            cmd.Parameters.AddWithValue("@CenterID", DBNull.Value)
                            cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now)
                            cmd.Parameters.AddWithValue("@SupervisorCode", If(String.IsNullOrEmpty(supervisorCode), DBNull.Value, supervisorCode))
                            cmd.Parameters.AddWithValue("@SupervisorName", If(String.IsNullOrEmpty(supervisorName), DBNull.Value, supervisorName))
                            cmd.Parameters.AddWithValue("@ManagerCode", DBNull.Value)
                            cmd.Parameters.AddWithValue("@ManagerName", DBNull.Value)
                            cmd.Parameters.AddWithValue("@StatusCode", initialStatusCode)
                            cmd.Parameters.AddWithValue("@StatusDescription", initialStatusDesc)
                            cmd.Parameters.AddWithValue("@UnitID", _exmUnitID)
                            cmd.Parameters.AddWithValue("@CreateUserID", userID)
                            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now)
                            cmd.Parameters.AddWithValue("@IsDone", False)
                            cmd.Parameters.AddWithValue("@ReminderSent", False)
                            cmd.Parameters.AddWithValue("@CurrentLevel", initialCurrentLevel)

                            newID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

            If newID > 0 Then
                btnSubmit.Enabled = False
                btnSubmit.Text = "✅ ثبت شد"

                LoadRequestHistory()
                LoadAvailableDates()

                hdnSelectedDate.Value = ""
                hdnSelectedPersianDate.Value = ""
                pnlSelectedDate.Visible = False

                Dim successScript As String = "showSuccessMessage('" & newID.ToString() & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "successMessage", successScript, True)
            Else
                ShowMessage("❌ خطا در ثبت درخواست", "danger")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                HideLoading()
            End If

        Catch ex As Exception
            HandleError(ex)
            btnSubmit.Enabled = True
            btnSubmit.Text = "📤 ثبت درخواست"
            HideLoading()
        End Try
    End Sub

    Private Sub HideLoading()
        Dim hideScript As String = "hideLoading();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideLoading", hideScript, True)
    End Sub

    Private Sub HandleError(ByVal ex As Exception)
        Try
            Dim msg As String = ex.Message
            Dim personCode As String = Session("codePersoneli").ToString()
            Dim examinationDate As String = hdnSelectedDate.Value

            If msg.Contains("سال جاری") OrElse msg.Contains("قبلاً") Then
                Dim reservationDate As String = ""
                If Not String.IsNullOrEmpty(personCode) AndAlso Not String.IsNullOrEmpty(examinationDate) Then
                    If CheckReservationInYear(personCode, examinationDate, reservationDate) Then
                        ShowMessage("⚠️ شما برای سال جاری (" & GetPersianYear(examinationDate) & ") قبلاً در تاریخ " & reservationDate & " ثبت نام کرده‌اید. هر پرسنل فقط سالی یکبار می‌تواند ثبت نام کند.", "warning")
                    Else
                        ShowMessage("⚠️ شما در سال جاری قبلاً برای معاینه ثبت نام کرده‌اید. هر پرسنل فقط سالی یکبار می‌تواند ثبت نام کند.", "warning")
                    End If
                Else
                    ShowMessage("⚠️ شما در سال جاری قبلاً برای معاینه ثبت نام کرده‌اید. هر پرسنل فقط سالی یکبار می‌تواند ثبت نام کند.", "warning")
                End If
            ElseIf msg.Contains("ظرفیت") Then
                ShowMessage("⚠️ " & msg, "warning")
            Else
                ShowMessage("❌ خطا: " & msg, "danger")
            End If
        Catch ex2 As Exception
            ShowMessage("❌ خطای سیستمی رخ داده است", "danger")
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/index.aspx")
    End Sub

    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
                pnlMessage.CssClass = "exm-alert exm-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                pnlMessage.CssClass = "exm-alert exm-alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
                pnlMessage.CssClass = "exm-alert exm-alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                pnlMessage.CssClass = "exm-alert exm-alert-info"
        End Select

        pnlMessage.Visible = True
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class