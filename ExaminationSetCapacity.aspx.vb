Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Collections.Generic
Imports System.Globalization

Partial Class ExaminationSetCapacity
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"
    Private _unitCapacities As New Dictionary(Of Integer, Integer)()
    Private _unitDeadlines As New Dictionary(Of Integer, String)()

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("super_vizer") IsNot Nothing AndAlso Session("super_vizer") = False Then
                meno_acc()
            End If

            LoadYears()
            LoadMonths()
            LoadDays()
            LoadCapacities()
            SetDefaultDate()

            ' ====== اول مهلت‌ها از exmUnits لود بشه ======
            LoadUnitDeadlines()

            ' ====== بعد ظرفیت‌های تاریخ فعلی ======
            LoadExistingCapacities()

            ' ====== در نهایت واحدها با مقادیر لود بشن ======
            LoadUnitsForCapacity()

        Else
            If ViewState("UnitCapacities") IsNot Nothing Then
                _unitCapacities = DirectCast(ViewState("UnitCapacities"), Dictionary(Of Integer, Integer))
            End If

            If ViewState("UnitDeadlines") IsNot Nothing Then
                _unitDeadlines = DirectCast(ViewState("UnitDeadlines"), Dictionary(Of Integer, String))
            End If
        End If
    End Sub

    ' ============================================================
    ' دسترسی منو
    ' ============================================================
    Private Sub meno_acc()
        Try
            Dim meno_arr(10) As Boolean
            meno_arr = ClsAccounting.meno_acc("ExaminationSetCapacity.aspx", Val(Session("UserId")))

            btnSubmit.Visible = meno_arr(3)
            btnBack.Visible = True
            btnLoadExisting.Visible = meno_arr(3)
            btnManageUnits.Visible = meno_arr(3)
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' متد کمکی برای اجرای کوئری با پارامتر
    ' ============================================================
    Private Function GetDataTableWithParams(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
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
    ' تبدیل میلادی به شمسی
    ' ============================================================
    Private Function ConvertGregorianToJalali(ByVal gregorianDate As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Dim year As Integer = pc.GetYear(gregorianDate)
            Dim month As Integer = pc.GetMonth(gregorianDate)
            Dim day As Integer = pc.GetDayOfMonth(gregorianDate)

            Return year.ToString() & "/" & month.ToString().PadLeft(2, "0"c) & "/" & day.ToString().PadLeft(2, "0"c)
        Catch ex As Exception
            Return gregorianDate.ToString("yyyy-MM-dd")
        End Try
    End Function

    ' ============================================================
    ' تبدیل شمسی به میلادی
    ' ============================================================
    Private Function ConvertPersianToGregorian(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer) As DateTime?
        Try
            If year < 1300 OrElse year > 1500 Then Return Nothing
            If month < 1 OrElse month > 12 Then Return Nothing
            If day < 1 OrElse day > 31 Then Return Nothing

            Dim pc As New PersianCalendar()
            Dim dt As DateTime

            Try
                dt = pc.ToDateTime(year, month, day, 0, 0, 0, 0)
            Catch ex As ArgumentOutOfRangeException
                Return Nothing
            End Try

            Dim dayOfWeek As Integer = CInt(dt.DayOfWeek)
            If dayOfWeek <> 6 AndAlso dayOfWeek <> 0 AndAlso dayOfWeek <> 1 Then
                Return Nothing
            End If

            Return dt.Date
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' ============================================================
    ' دریافت نام واحد
    ' ============================================================
    Private Function GetUnitName(ByVal unitID As Integer) As String
        Try
            If unitID <= 0 Then
                Return "نامشخص"
            End If

            Dim sql As String = "SELECT UnitName FROM exmUnits WHERE ID = @UnitID"
            Dim dt As DataTable = GetDataTableWithParams(sql, New SqlParameter("@UnitID", unitID))

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0).ToString()
            End If

            Return "واحد " & unitID.ToString()
        Catch ex As Exception
            Return "واحد " & unitID.ToString()
        End Try
    End Function

    ' ============================================================
    ' بارگذاری مهلت‌های همه واحدها از exmUnits
    ' ============================================================
    Private Sub LoadUnitDeadlines()
        Try
            Dim sql As String = "SELECT ID, CASE WHEN DeadlineDate IS NOT NULL THEN dbo.FormatDateFa(DeadlineDate) ELSE '' END AS PersianDeadline FROM exmUnits WHERE IsActive = 1"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            _unitDeadlines = New Dictionary(Of Integer, String)()

            For Each row As DataRow In dt.Rows
                Dim unitID As Integer = Convert.ToInt32(row("ID"))
                Dim deadline As String = ""

                If row("PersianDeadline") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(row("PersianDeadline").ToString()) Then
                    deadline = row("PersianDeadline").ToString()
                End If

                _unitDeadlines(unitID) = deadline
            Next

            ViewState("UnitDeadlines") = _unitDeadlines

        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری ظرفیت‌های تاریخ انتخاب شده
    ' ============================================================
    Private Sub LoadExistingCapacities()
        Try
            Dim yearText As String = ddlYear.SelectedValue
            Dim monthText As String = ddlMonth.SelectedValue
            Dim dayText As String = ddlDay.SelectedValue

            If String.IsNullOrEmpty(yearText) OrElse String.IsNullOrEmpty(monthText) OrElse String.IsNullOrEmpty(dayText) Then
                pnlExisting.Visible = False
                _unitCapacities = New Dictionary(Of Integer, Integer)()
                Return
            End If

            Dim yearInt As Integer = Convert.ToInt32(yearText)
            Dim monthInt As Integer = Convert.ToInt32(monthText)
            Dim dayInt As Integer = Convert.ToInt32(dayText)

            Dim examinationDate As DateTime? = ConvertPersianToGregorian(yearInt, monthInt, dayInt)

            If Not examinationDate.HasValue Then
                pnlExisting.Visible = False
                _unitCapacities = New Dictionary(Of Integer, Integer)()
                Return
            End If

            ' ====== دریافت ظرفیت‌ها برای این تاریخ ======
            Dim sql As String = "SELECT " &
                "    uc.UnitID, " &
                "    u.UnitName, " &
                "    uc.Capacity " &
                "FROM exmUnitCapacity uc " &
                "INNER JOIN exmUnits u ON uc.UnitID = u.ID " &
                "INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID " &
                "WHERE d.ExaminationDate = @ExaminationDate AND d.IsActive = 1 " &
                "ORDER BY u.UnitName"

            Dim dt As DataTable = GetDataTableWithParams(sql, New SqlParameter("@ExaminationDate", examinationDate.Value.Date))

            ' ====== ذخیره در دیکشنری ======
            _unitCapacities = New Dictionary(Of Integer, Integer)()

            For Each row As DataRow In dt.Rows
                Dim unitID As Integer = Convert.ToInt32(row("UnitID"))
                Dim capacity As Integer = Convert.ToInt32(row("Capacity"))
                _unitCapacities(unitID) = capacity
            Next

            ViewState("UnitCapacities") = _unitCapacities

            ' ====== نمایش خلاصه ======
            If dt.Rows.Count > 0 Then
                Dim sb As New StringBuilder()
                sb.AppendLine("ظرفیت برای این تاریخ قبلاً تعیین شده است:")
                sb.AppendLine("<br />")

                For Each row As DataRow In dt.Rows
                    Dim unitID As Integer = Convert.ToInt32(row("UnitID"))
                    Dim unitName As String = row("UnitName").ToString()
                    Dim capacity As Integer = Convert.ToInt32(row("Capacity"))
                    Dim persianDeadline As String = ""

                    If _unitDeadlines.ContainsKey(unitID) Then
                        persianDeadline = _unitDeadlines(unitID)
                    End If

                    sb.AppendLine("<span class='exm-unit-cap-item'><strong>" & unitName & "</strong>: " & capacity & " نفر")

                    If Not String.IsNullOrEmpty(persianDeadline) Then
                        sb.AppendLine(" <span style='color:#e74c3c;'>(مهلت: " & persianDeadline & ")</span>")
                    End If

                    sb.AppendLine("</span> ")
                Next

                lblExistingInfo.Text = sb.ToString()
                pnlExisting.Visible = True
            Else
                pnlExisting.Visible = False
            End If

        Catch ex As Exception
            pnlExisting.Visible = False
        End Try
    End Sub

    ' ============================================================
    ' تغییر سال
    ' ============================================================
    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadDays()
        pnlExisting.Visible = False
        LoadExistingCapacities()
        LoadUnitsForCapacity()
    End Sub

    ' ============================================================
    ' تغییر ماه
    ' ============================================================
    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        LoadDays()
        pnlExisting.Visible = False
        LoadExistingCapacities()
        LoadUnitsForCapacity()
    End Sub

    ' ============================================================
    ' تغییر روز
    ' ============================================================
    Protected Sub ddlDay_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDay.SelectedIndexChanged
        pnlExisting.Visible = False
        LoadExistingCapacities()
        LoadUnitsForCapacity()
    End Sub

    ' ============================================================
    ' تاریخ پیش فرض
    ' ============================================================
    Private Sub SetDefaultDate()
        Try
            Dim pc As New PersianCalendar()
            Dim now As DateTime = DateTime.Now

            Dim currentYear As Integer = pc.GetYear(now)
            Dim currentMonth As Integer = pc.GetMonth(now)

            If ddlYear.Items.FindByValue(currentYear.ToString()) IsNot Nothing Then
                ddlYear.SelectedValue = currentYear.ToString()
            End If

            If ddlMonth.Items.FindByValue(currentMonth.ToString()) IsNot Nothing Then
                ddlMonth.SelectedValue = currentMonth.ToString()
            End If

            LoadDays()

            Dim currentDate As DateTime = now
            Dim dayOfWeek As Integer = CInt(currentDate.DayOfWeek)

            While dayOfWeek <> 6 AndAlso dayOfWeek <> 0 AndAlso dayOfWeek <> 1
                currentDate = currentDate.AddDays(1)
                dayOfWeek = CInt(currentDate.DayOfWeek)
            End While

            Dim pc2 As New PersianCalendar()
            Dim targetYear As Integer = pc2.GetYear(currentDate)
            Dim targetMonth As Integer = pc2.GetMonth(currentDate)
            Dim targetDay As Integer = pc2.GetDayOfMonth(currentDate)

            If ddlYear.Items.FindByValue(targetYear.ToString()) IsNot Nothing Then
                ddlYear.SelectedValue = targetYear.ToString()
            End If

            If ddlMonth.Items.FindByValue(targetMonth.ToString()) IsNot Nothing Then
                ddlMonth.SelectedValue = targetMonth.ToString()
            End If

            LoadDays()

            If ddlDay.Items.FindByValue(targetDay.ToString()) IsNot Nothing Then
                ddlDay.SelectedValue = targetDay.ToString()
            End If
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری سال‌ها
    ' ============================================================
    Private Sub LoadYears()
        Try
            Dim pc As New PersianCalendar()
            Dim currentYear As Integer = pc.GetYear(DateTime.Now)

            ddlYear.Items.Clear()
            ddlYear.Items.Add(New ListItem("-- سال --", ""))

            For i As Integer = currentYear - 5 To currentYear + 5
                ddlYear.Items.Add(New ListItem(i.ToString(), i.ToString()))
            Next
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری ماه‌ها
    ' ============================================================
    Private Sub LoadMonths()
        Try
            ddlMonth.Items.Clear()
            ddlMonth.Items.Add(New ListItem("-- ماه --", ""))
            ddlMonth.Items.Add(New ListItem("فروردین", "1"))
            ddlMonth.Items.Add(New ListItem("اردیبهشت", "2"))
            ddlMonth.Items.Add(New ListItem("خرداد", "3"))
            ddlMonth.Items.Add(New ListItem("تیر", "4"))
            ddlMonth.Items.Add(New ListItem("مرداد", "5"))
            ddlMonth.Items.Add(New ListItem("شهریور", "6"))
            ddlMonth.Items.Add(New ListItem("مهر", "7"))
            ddlMonth.Items.Add(New ListItem("آبان", "8"))
            ddlMonth.Items.Add(New ListItem("آذر", "9"))
            ddlMonth.Items.Add(New ListItem("دی", "10"))
            ddlMonth.Items.Add(New ListItem("بهمن", "11"))
            ddlMonth.Items.Add(New ListItem("اسفند", "12"))
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری روزها
    ' ============================================================
    Private Sub LoadDays()
        Try
            ddlDay.Items.Clear()
            ddlDay.Items.Add(New ListItem("-- روز --", ""))

            Dim pc As New PersianCalendar()
            Dim currentYear As Integer = pc.GetYear(DateTime.Now)
            Dim currentMonth As Integer = pc.GetMonth(DateTime.Now)

            If Not String.IsNullOrEmpty(ddlYear.SelectedValue) Then
                currentYear = Convert.ToInt32(ddlYear.SelectedValue)
            End If

            If Not String.IsNullOrEmpty(ddlMonth.SelectedValue) Then
                currentMonth = Convert.ToInt32(ddlMonth.SelectedValue)
            End If

            For day As Integer = 1 To 31
                Try
                    Dim dt As DateTime = pc.ToDateTime(currentYear, currentMonth, day, 0, 0, 0, 0)
                    Dim dayOfWeek As Integer = CInt(dt.DayOfWeek)

                    If dayOfWeek = 6 OrElse dayOfWeek = 0 OrElse dayOfWeek = 1 Then
                        ddlDay.Items.Add(New ListItem(day.ToString(), day.ToString()))
                    End If
                Catch ex As ArgumentOutOfRangeException
                    Continue For
                End Try
            Next
        Catch ex As Exception
            ddlDay.Items.Clear()
            ddlDay.Items.Add(New ListItem("-- روز --", ""))
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری واحدها (با مهلت از exmUnits)
    ' ============================================================
    Private Sub LoadUnitsForCapacity(Optional ByVal searchText As String = "")
        Try
            Dim sqlstr As String = "SELECT ID, UnitName FROM exmUnits WHERE IsActive = 1"

            If Not String.IsNullOrEmpty(searchText) Then
                sqlstr &= " AND UnitName LIKE N'%' + @SearchText + N'%'"
            End If

            sqlstr &= " ORDER BY UnitName"

            Dim dt As DataTable

            If Not String.IsNullOrEmpty(searchText) Then
                Dim params As New Dictionary(Of String, Object)
                params.Add("@SearchText", searchText)
                dt = ClsDataTable.GetDataTable(sqlstr, params)
            Else
                dt = ClsDataTable.GetDataTable(sqlstr)
            End If

            Dim dtUnits As New DataTable()
            dtUnits.Columns.Add("UnitID", GetType(Integer))
            dtUnits.Columns.Add("UnitName", GetType(String))
            dtUnits.Columns.Add("CapacityValue", GetType(Integer))
            dtUnits.Columns.Add("DeadlineValue", GetType(String))

            For Each row As DataRow In dt.Rows
                Dim unitID As Integer = Convert.ToInt32(row("ID"))
                Dim capacity As Integer = 0
                Dim deadline As String = ""

                ' ====== مقدار ظرفیت ======
                If _unitCapacities IsNot Nothing AndAlso _unitCapacities.ContainsKey(unitID) Then
                    capacity = _unitCapacities(unitID)
                End If

                ' ====== مقدار مهلت از exmUnits ======
                If _unitDeadlines IsNot Nothing AndAlso _unitDeadlines.ContainsKey(unitID) Then
                    deadline = _unitDeadlines(unitID)
                End If

                dtUnits.Rows.Add(unitID, row("UnitName"), capacity, deadline)
            Next

            rptOmurs.DataSource = dtUnits
            rptOmurs.DataBind()

            lblFilteredCount.Text = "نمایش " & dtUnits.Rows.Count & " واحد"

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری واحدها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' جستجوی واحد
    ' ============================================================
    Protected Sub btnSearchCapacity_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchCapacity.Click
        LoadUnitsForCapacity(txtSearchCapacity.Text.Trim())
    End Sub

    ' ============================================================
    ' نمایش همه واحدها
    ' ============================================================
    Protected Sub btnShowAllCapacity_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowAllCapacity.Click
        txtSearchCapacity.Text = ""
        LoadUnitsForCapacity()
    End Sub

    ' ============================================================
    ' مدیریت واحدها
    ' ============================================================
    Protected Sub btnManageUnits_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnManageUnits.Click
        Response.Redirect("AdminExmUnits.aspx")
    End Sub

    ' ============================================================
    ' بارگذاری ظرفیت‌ها
    ' ============================================================
    Private Sub LoadCapacities()
        Try
            Dim sqlstr As String = "SELECT TOP 100 " &
                "d.ID, " &
                "d.ExaminationDate AS GregorianDate, " &
                "dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, " &
                "CASE DATEPART(dw,d.ExaminationDate) " &
                "WHEN 7 THEN N'شنبه' " &
                "WHEN 1 THEN N'یکشنبه' " &
                "WHEN 2 THEN N'دوشنبه' " &
                "WHEN 3 THEN N'سه‌شنبه' " &
                "WHEN 4 THEN N'چهارشنبه' " &
                "WHEN 5 THEN N'پنج‌شنبه' " &
                "WHEN 6 THEN N'جمعه' " &
                "END AS DayName, " &
                "ISNULL((SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc WHERE uc.CapacityID=d.ID),0) AS TotalCapacity, " &
                "d.IsActive, " &
                "d.CreateDate " &
                "FROM exmDailyCapacity d " &
                "WHERE d.IsActive=1 " &
                "ORDER BY d.ExaminationDate DESC"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                dt.Columns.Add("RowNumber", GetType(Integer))
                dt.Columns.Add("UnitsInfo", GetType(String))

                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("RowNumber") = i + 1
                    Dim id As Integer = Convert.ToInt32(dt.Rows(i)("ID"))
                    dt.Rows(i)("UnitsInfo") = GetUnitsInfo(id)
                Next

                gvCapacities.DataSource = dt
                gvCapacities.DataBind()
            Else
                gvCapacities.DataSource = Nothing
                gvCapacities.DataBind()
                ShowMessage("هیچ ظرفیتی تعیین نشده است", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' اطلاعات واحدها
    ' ============================================================
    Private Function GetUnitsInfo(ByVal capacityID As Integer) As String
        Try
            Dim sqlstr As String = "SELECT STUFF((" &
                "SELECT N'، ' + u.UnitName + N': ' + " &
                "CAST(uc.Capacity AS NVARCHAR(10)) + " &
                "CASE WHEN u.DeadlineDate IS NOT NULL THEN N' (مهلت: ' + dbo.FormatDateFa(u.DeadlineDate) + N')' ELSE N'' END " &
                "FROM exmUnitCapacity uc " &
                "INNER JOIN exmUnits u " &
                "ON uc.UnitID=u.ID " &
                "WHERE uc.CapacityID=@CapacityID " &
                "FOR XML PATH(''), TYPE" &
                ").value('.','NVARCHAR(MAX)'),1,2,'')"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.Add("@CapacityID", SqlDbType.Int).Value = capacityID

                    cnn.Open()
                    Dim result As Object = cmd.ExecuteScalar()

                    If result Is Nothing OrElse IsDBNull(result) Then
                        Return ""
                    End If

                    Return result.ToString()
                End Using
            End Using

        Catch ex As Exception
            Return "خطا در دریافت اطلاعات"
        End Try
    End Function

    ' ============================================================
    ' بارگذاری ظرفیت موجود (دکمه)
    ' ============================================================
    Protected Sub btnLoadExisting_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadExisting.Click
        Try
            LoadUnitDeadlines()
            LoadExistingCapacities()
            LoadUnitsForCapacity()

            If pnlExisting.Visible Then
                ShowMessage("ظرفیت و مهلت‌های قبلی بارگذاری شد", "info")
            Else
                ShowMessage("برای این تاریخ ظرفیتی تعیین نشده است", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری ظرفیت: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ثبت ظرفیت
    ' ============================================================
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            lblMessage.Text = ""
            pnlMessage.Visible = False
            btnSubmit.Enabled = False
            btnSubmit.Text = "⏳ در حال ثبت..."

            Dim yearText As String = ddlYear.SelectedValue.Trim()
            Dim monthText As String = ddlMonth.SelectedValue.Trim()
            Dim dayText As String = ddlDay.SelectedValue.Trim()

            If String.IsNullOrEmpty(yearText) OrElse String.IsNullOrEmpty(monthText) OrElse String.IsNullOrEmpty(dayText) Then
                ShowMessage("لطفاً تاریخ را کامل انتخاب کنید.", "warning")
                Exit Sub
            End If

            Dim yearInt As Integer
            Dim monthInt As Integer
            Dim dayInt As Integer

            If Not Integer.TryParse(yearText, yearInt) Then
                ShowMessage("سال نامعتبر است.", "warning")
                Exit Sub
            End If

            If Not Integer.TryParse(monthText, monthInt) Then
                ShowMessage("ماه نامعتبر است.", "warning")
                Exit Sub
            End If

            If Not Integer.TryParse(dayText, dayInt) Then
                ShowMessage("روز نامعتبر است.", "warning")
                Exit Sub
            End If

            Dim examinationDate As DateTime? = ConvertPersianToGregorian(yearInt, monthInt, dayInt)

            If Not examinationDate.HasValue Then
                ShowMessage("تاریخ انتخاب شده معتبر نیست.", "warning")
                Exit Sub
            End If

            ' ====== ظرفیت واحدها ======
            Dim unitCapacityList As New List(Of String)

            For Each item As RepeaterItem In rptOmurs.Items
                Dim txt As TextBox = CType(item.FindControl("txtCapacity"), TextBox)
                Dim hdn As HiddenField = CType(item.FindControl("hdnOmurID"), HiddenField)

                If txt Is Nothing OrElse hdn Is Nothing Then
                    Continue For
                End If

                Dim value As String = txt.Text.Trim()
                If String.IsNullOrEmpty(value) Then
                    Continue For
                End If

                Dim capacity As Integer
                If Not Integer.TryParse(value, capacity) Then
                    Continue For
                End If

                If capacity > 0 Then
                    unitCapacityList.Add(hdn.Value & ":" & capacity)
                End If
            Next

            If unitCapacityList.Count = 0 Then
                ShowMessage("لطفاً برای حداقل یک واحد ظرفیت بیشتر از صفر وارد کنید.", "warning")
                Exit Sub
            End If

            Dim selectedUnits As String = String.Join(",", unitCapacityList.ToArray())

            Dim capacityID As Integer = 0
            Dim message As String = ""

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                cnn.Open()

                Using cmd As New SqlCommand("sp_AddUnitCapacity", cnn)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add("@ExaminationDate", SqlDbType.Date).Value = examinationDate.Value.Date
                    cmd.Parameters.Add("@UnitCapacities", SqlDbType.NVarChar, -1).Value = selectedUnits
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session("UserID"))

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            If Not IsDBNull(dr("Message")) Then
                                message = dr("Message").ToString()
                            End If

                            If Not IsDBNull(dr("CapacityID")) Then
                                capacityID = Convert.ToInt32(dr("CapacityID"))
                            End If
                        End If
                    End Using
                End Using
            End Using

            If capacityID = 0 Then
                ShowMessage("ثبت ظرفیت با خطا مواجه شد: " & message, "danger")
                Exit Sub
            End If

            Dim persianDate As String = ConvertGregorianToJalali(examinationDate.Value)

            If String.IsNullOrEmpty(message) Then
                message = "ظرفیت با موفقیت برای تاریخ " & persianDate & " ثبت شد."
            Else
                message = message.Replace(examinationDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), persianDate)
            End If

            ShowMessage(message, "success")

            ' ====== بارگذاری مجدد ======
            LoadCapacities()
            LoadUnitDeadlines()
            LoadExistingCapacities()
            LoadUnitsForCapacity()
            pnlExisting.Visible = False

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        Finally
            btnSubmit.Enabled = True
            btnSubmit.Text = "✅ ثبت ظرفیت واحدها"
        End Try
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/index.aspx")
    End Sub

    ' ============================================================
    ' صفحه‌بندی GridView
    ' ============================================================
    Protected Sub gvCapacities_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvCapacities.PageIndexChanging
        gvCapacities.PageIndex = e.NewPageIndex
        LoadCapacities()
    End Sub

    ' ============================================================
    ' دستورات GridView
    ' ============================================================
    Protected Sub gvCapacities_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvCapacities.RowCommand
        Try
            If e.CommandName = "EditRow" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)

                Dim sqlstr As String = "SELECT ExaminationDate FROM exmDailyCapacity WHERE ID=@ID"

                Using cnn As New SqlConnection(ClsConnection.GetConn)
                    Using cmd As New SqlCommand(sqlstr, cnn)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id

                        cnn.Open()
                        Dim examDate As Object = cmd.ExecuteScalar()

                        If examDate IsNot Nothing AndAlso Not IsDBNull(examDate) Then
                            Dim gregorianDate As DateTime = Convert.ToDateTime(examDate)
                            Dim persianDate As String = ConvertGregorianToJalali(gregorianDate)

                            Dim pc As New PersianCalendar()
                            Dim persianYear As Integer = pc.GetYear(gregorianDate)
                            Dim persianMonth As Integer = pc.GetMonth(gregorianDate)
                            Dim persianDay As Integer = pc.GetDayOfMonth(gregorianDate)

                            ddlYear.SelectedValue = persianYear.ToString()
                            ddlMonth.SelectedValue = persianMonth.ToString()
                            LoadDays()
                            ddlDay.SelectedValue = persianDay.ToString()

                            hdnEditID.Value = id.ToString()

                            ' بارگذاری مهلت‌ها و ظرفیت‌ها
                            LoadUnitDeadlines()
                            LoadExistingCapacities()
                            LoadUnitsForCapacity()

                            ShowMessage("اطلاعات برای تاریخ " & persianDate & " بارگذاری شد.", "info")
                        End If
                    End Using
                End Using

            ElseIf e.CommandName = "ToggleRow" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)

                Dim sqlstr As String = "UPDATE exmDailyCapacity SET IsActive = CASE WHEN IsActive=1 THEN 0 ELSE 1 END WHERE ID=@ID"

                Using cnn As New SqlConnection(ClsConnection.GetConn)
                    Using cmd As New SqlCommand(sqlstr, cnn)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                        cnn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ShowMessage("وضعیت با موفقیت تغییر کرد", "success")
                LoadCapacities()
            End If

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' Repeater ItemDataBound
    ' ============================================================
    Protected Sub rptOmurs_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then

                Dim txtCapacity As TextBox = CType(e.Item.FindControl("txtCapacity"), TextBox)
                Dim txtDeadline As TextBox = CType(e.Item.FindControl("txtDeadline"), TextBox)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim btnDetails As Button = CType(e.Item.FindControl("btnDetails"), Button)
                Dim hdnOmurID As HiddenField = CType(e.Item.FindControl("hdnOmurID"), HiddenField)

                ' ====== پر کردن مقادیر از DataItem ======
                Dim dataItem As DataRowView = TryCast(e.Item.DataItem, DataRowView)
                If dataItem IsNot Nothing Then
                    If txtCapacity IsNot Nothing Then
                        txtCapacity.Text = dataItem("CapacityValue").ToString()
                    End If

                    If txtDeadline IsNot Nothing Then
                        Dim deadline As String = dataItem("DeadlineValue").ToString()
                        If Not String.IsNullOrEmpty(deadline) Then
                            txtDeadline.Text = deadline
                        Else
                            txtDeadline.Text = "---"
                        End If
                    End If
                End If

                ' ====== تنظیم وضعیت ======
                If txtCapacity IsNot Nothing AndAlso lblStatus IsNot Nothing Then
                    Dim capacity As Integer = 0
                    Integer.TryParse(txtCapacity.Text, capacity)

                    If capacity > 0 Then
                        lblStatus.Text = "✓ فعال"
                        lblStatus.CssClass = "exm-unit-status-active"
                    Else
                        lblStatus.Text = "✗ غیرفعال"
                        lblStatus.CssClass = "exm-unit-status-inactive"
                    End If
                End If

                ' ====== تنظیم CommandArgument ======
                If btnDetails IsNot Nothing AndAlso hdnOmurID IsNot Nothing Then
                    btnDetails.CommandArgument = hdnOmurID.Value
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' Repeater Command
    ' ============================================================
    Protected Sub rptOmurs_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs) Handles rptOmurs.ItemCommand
        Try
            If e.CommandName = "Details" Then
                Dim unitID As Integer = Convert.ToInt32(e.CommandArgument)

                ' ====== ذخیره صفحه مبدا در Session ======
                Session("ExmUnitDetails_From") = "1"

                ' ====== هدایت به صفحه جزئیات ======
                Response.Redirect("ExmUnitDetails.aspx?UnitID=" & unitID)
            End If
        Catch ex As Exception
            ShowMessage("خطا در نمایش جزئیات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
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

    ' ============================================================
    ' کلاس Alert
    ' ============================================================
    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class