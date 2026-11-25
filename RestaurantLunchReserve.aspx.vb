
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Web.UI.WebControls

Partial Class RestaurantLunchReserve
    Inherits System.Web.UI.Page

    Private Const MenuUrl As String =
        "https://portal.tpco.ir/food-menu/data/menu.json"

    Private Const LeaveValue As String = "__LEAVE__"

    Private _savedReservations As Dictionary(Of DateTime, String)
    Private _closedWeekdays As HashSet(Of Integer)

    Protected Sub Page_Load(ByVal sender As Object,
                            ByVal e As EventArgs) Handles Me.Load

        If Session("userid") Is Nothing Then
            lblMessage.CssClass = "lunch-message error"
            lblMessage.Text = "شناسه کاربر یافت نشد. لطفاً مجدداً وارد سامانه شوید."
            btnSave.Enabled = False
            Return
        End If

        If Not IsPostBack Then
            LoadWeek()
        End If

    End Sub

    '====================================================
    ' بارگذاری امروز و 7 روز آینده
    '====================================================
    Private Sub LoadWeek()

        Try

            lblMessage.Text = ""

            _savedReservations = GetSavedReservations()
            _closedWeekdays = GetClosedWeekdays()

            Dim menu As Dictionary(Of DateTime, LunchMenuDay) =
                GetMenuFromJson()

            Dim today As DateTime = DateTime.Today
            Dim weekList As New List(Of LunchMenuDay)

            For i As Integer = 0 To 7

                Dim currentDate As DateTime = today.AddDays(i)
                Dim menuDay As LunchMenuDay

                If menu.ContainsKey(currentDate.Date) Then

                    menuDay = menu(currentDate.Date)

                    menuDay.MenuDate = currentDate.Date
                    menuDay.MenuDateIso = currentDate.ToString(
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture)

                    menuDay.DateText = GetPersianDate(currentDate)

                    menuDay.IsReservable =
                        currentDate.Date > today AndAlso
                        Not _closedWeekdays.Contains(
                            CInt(currentDate.DayOfWeek))

                Else

                    menuDay = New LunchMenuDay()

                    menuDay.MenuDate = currentDate.Date
                    menuDay.MenuDateIso = currentDate.ToString(
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture)

                    menuDay.DateText = GetPersianDate(currentDate)
                    menuDay.WeekdayName = GetWeekdayName(currentDate)
                    menuDay.IsReservable = False
                    menuDay.Foods = New List(Of String)()

                End If

                weekList.Add(menuDay)

            Next

            rptWeek.DataSource = weekList
            rptWeek.DataBind()

        Catch ex As Exception

            lblMessage.CssClass = "lunch-message error"
            lblMessage.Text =
                "خطا در دریافت منوی غذا: " &
                Server.HtmlEncode(ex.Message)

        End Try

    End Sub

    '====================================================
    ' دریافت منوی غذا از JSON
    '====================================================
    Private Function GetMenuFromJson() As Dictionary(Of DateTime, LunchMenuDay)

        Dim result As New Dictionary(Of DateTime, LunchMenuDay)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Using wc As New WebClient()

            wc.Encoding = Encoding.UTF8

            Dim json As String = wc.DownloadString(MenuUrl)

            Dim serializer As New JavaScriptSerializer()
            serializer.MaxJsonLength = Integer.MaxValue

            Dim rows As Object() =
                serializer.Deserialize(Of Object())(json)

            If rows Is Nothing Then Return result

            Dim pc As New PersianCalendar()

            For Each rowObject As Object In rows

                Dim cells As Object() = TryCast(rowObject, Object())

                If cells Is Nothing OrElse cells.Length < 9 Then
                    Continue For
                End If

                Try

                    Dim dateParts As String() =
                        Convert.ToString(cells(0)).Replace("-", "/").Split("/"c)

                    If dateParts.Length <> 3 Then Continue For

                    Dim jy As Integer = Integer.Parse(dateParts(0))
                    Dim jm As Integer = Integer.Parse(dateParts(1))
                    Dim jd As Integer = Integer.Parse(dateParts(2))

                    Dim menuDate As DateTime =
                        pc.ToDateTime(jy, jm, jd, 0, 0, 0, 0)

                    Dim foods As New List(Of String)

                    ' فقط غذای برنجی و خوراک ناهار
                    AddFood(foods, cells(4))
                    AddFood(foods, cells(7))

                    ' خانه cells(8) غذای شام است و نمایش داده نمی‌شود.

                    Dim menuDay As New LunchMenuDay()

                    menuDay.MenuDate = menuDate.Date
                    menuDay.DateText = GetPersianDate(menuDate)
                    menuDay.WeekdayName = GetWeekdayName(menuDate)
                    menuDay.Foods = foods

                    If Not result.ContainsKey(menuDate.Date) Then
                        result.Add(menuDate.Date, menuDay)
                    End If

                Catch
                    ' رکورد نامعتبر نادیده گرفته می‌شود
                End Try

            Next

        End Using

        Return result

    End Function

    Private Sub AddFood(ByVal foods As List(Of String),
                        ByVal value As Object)

        Dim foodName As String = Convert.ToString(value).Trim()

        If foodName <> "" AndAlso
           foodName <> "-" AndAlso
           Not foods.Contains(foodName) Then

            foods.Add(foodName)

        End If

    End Sub

    '====================================================
    ' تنظیم گزینه‌های هر روز
    '====================================================
    Protected Sub rptWeek_ItemDataBound(
        ByVal sender As Object,
        ByVal e As RepeaterItemEventArgs)

        If e.Item.ItemType <> ListItemType.Item AndAlso
           e.Item.ItemType <> ListItemType.AlternatingItem Then
            Return
        End If

        Dim day As LunchMenuDay =
            DirectCast(e.Item.DataItem, LunchMenuDay)

        Dim rbl As RadioButtonList =
            DirectCast(e.Item.FindControl("rblFood"), RadioButtonList)

        Dim lblStatus As Label =
            DirectCast(e.Item.FindControl("lblDayStatus"), Label)

        Dim pnlDay As Panel =
            DirectCast(e.Item.FindControl("pnlDay"), Panel)

        Dim lblToday As Label =
            DirectCast(e.Item.FindControl("lblTodayBadge"), Label)

        Dim isToday As Boolean =
            day.MenuDate.Date = DateTime.Today

        Dim isHoliday As Boolean =
            day.MenuDate.DayOfWeek = DayOfWeek.Thursday OrElse
            day.MenuDate.DayOfWeek = DayOfWeek.Friday

        ' ظاهر کارت
        If isToday Then

            pnlDay.CssClass = "lunch-day today-day"
            lblToday.Visible = True

        ElseIf isHoliday Then

            pnlDay.CssClass = "lunch-day holiday-day"

        Else

            pnlDay.CssClass = "lunch-day"

        End If

        rbl.Items.Clear()

        Dim savedValue As String = ""

        If _savedReservations IsNot Nothing AndAlso
           _savedReservations.ContainsKey(day.MenuDate.Date) Then

            savedValue = _savedReservations(day.MenuDate.Date)

        End If

        ' گزینه‌های غذا
        For i As Integer = 0 To day.Foods.Count - 1

            Dim food As String = day.Foods(i)
            Dim displayText As String = food

            If i = 0 Then
                displayText = "🍚 غذای برنجی: " & food
            ElseIf i = 1 Then
                displayText = "🥘 غذای خوراک: " & food
            End If

            rbl.Items.Add(New ListItem(displayText, food))

        Next

        ' پنج‌شنبه و جمعه گزینه مرخصی ندارند
        If Not isHoliday Then
            rbl.Items.Add(New ListItem("🏖 مرخصی هستم", LeaveValue))
        End If

        ' انتخاب رزرو قبلی؛ گزینه اضافی و تکراری ایجاد نمی‌کنیم
        If savedValue <> "" Then

            If savedValue = "مرخصی" Then

                If Not isHoliday AndAlso
                   rbl.Items.FindByValue(LeaveValue) IsNot Nothing Then

                    rbl.SelectedValue = LeaveValue

                End If

            Else

                If rbl.Items.FindByValue(savedValue) IsNot Nothing Then
                    rbl.SelectedValue = savedValue
                End If

            End If

        End If

        ' وضعیت روز
        If day.Foods Is Nothing OrElse day.Foods.Count = 0 Then

            rbl.Enabled = False
            lblStatus.Text = "منوی غذا برای این روز در دسترس نیست."

        ElseIf isToday Then

            rbl.Enabled = False
            lblStatus.Text = "امروز؛ امکان ثبت یا ویرایش رزرو وجود ندارد."

        ElseIf Not day.IsReservable Then

            rbl.Enabled = False
            lblStatus.Text = "رزرو این روز توسط مدیریت بسته شده است."

        ElseIf isHoliday Then

            rbl.Enabled = True
            lblStatus.Text = "انتخاب غذا در این روز اختیاری است."

        Else

            rbl.Enabled = True

            If savedValue <> "" Then
                lblStatus.Text = "رزرو قبلی شما ثبت شده است."
            Else
                lblStatus.Text = "لطفاً یکی از گزینه‌ها را انتخاب کنید."
            End If

        End If

    End Sub

    '====================================================
    ' ثبت رزروها
    '====================================================
    Protected Sub btnSave_Click(ByVal sender As Object,
                                ByVal e As EventArgs)

        Try

            If Session("userid") Is Nothing Then
                Throw New Exception("شناسه کاربر معتبر نیست.")
            End If

            Dim userId As String =
                Convert.ToString(Session("userid"))

            Dim menu As Dictionary(Of DateTime, LunchMenuDay) =
                GetMenuFromJson()

            Dim closedDays As HashSet(Of Integer) =
                GetClosedWeekdays()

            Dim today As DateTime = DateTime.Today
            Dim weekStart As DateTime = today
            Dim weekEnd As DateTime = today.AddDays(7)

            Dim reservationCount As Integer = 0
            Dim deletedCount As Integer = 0

            Using conn As New SqlConnection(ClsConnection.GetConn())

                conn.Open()

                Using tran As SqlTransaction = conn.BeginTransaction()

                    Try

                        For Each repeaterItem As RepeaterItem In rptWeek.Items

                            Dim hf As HiddenField =
                                DirectCast(repeaterItem.FindControl("hfDate"), HiddenField)

                            Dim rbl As RadioButtonList =
                                DirectCast(repeaterItem.FindControl("rblFood"), RadioButtonList)

                            Dim dateValue As DateTime

                            If Not DateTime.TryParseExact(
                                hf.Value,
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                dateValue) Then

                                Throw New Exception("تاریخ یکی از روزها معتبر نیست.")

                            End If

                            dateValue = dateValue.Date

                            ' فقط امروز تا 7 روز آینده
                            If dateValue < weekStart OrElse dateValue > weekEnd Then
                                Throw New Exception("تاریخ رزرو خارج از محدوده مجاز است.")
                            End If

                            ' امروز قابل ویرایش نیست
                            If dateValue <= today Then
                                Continue For
                            End If

                            ' روز بسته توسط مدیریت
                            If closedDays.Contains(CInt(dateValue.DayOfWeek)) Then
                                Continue For
                            End If

                            ' روز بدون منو
                            If Not menu.ContainsKey(dateValue) Then
                                Continue For
                            End If

                            If menu(dateValue).Foods Is Nothing OrElse
                               menu(dateValue).Foods.Count = 0 Then
                                Continue For
                            End If

                            Dim isHoliday As Boolean =
                                dateValue.DayOfWeek = DayOfWeek.Thursday OrElse
                                dateValue.DayOfWeek = DayOfWeek.Friday

                            Dim selectedValue As String = rbl.SelectedValue

                            ' در پنج‌شنبه و جمعه انتخاب اجباری نیست
                            If selectedValue = "" Then

                                If isHoliday Then

                                    ' اگر قبلاً رزرو ثبت شده، با خالی گذاشتن حذف شود
                                    DeleteReservation(conn, tran, userId, dateValue)
                                    deletedCount += 1

                                    Continue For

                                Else

                                    Throw New Exception(
                                        "لطفاً برای تاریخ " &
                                        GetPersianDate(dateValue) &
                                        " غذا یا مرخصی را انتخاب کنید.")

                                End If

                            End If

                            Dim isLeave As Boolean =
                                (selectedValue = LeaveValue)

                            Dim selectedFood As String = ""

                            If isLeave Then

                                If isHoliday Then
                                    Throw New Exception(
                                        "برای پنج‌شنبه و جمعه امکان انتخاب مرخصی وجود ندارد.")
                                End If

                                selectedFood = "مرخصی"

                            Else

                                If Not menu(dateValue).Foods.Contains(selectedValue) Then

                                    Throw New Exception(
                                        "غذای انتخاب‌شده برای تاریخ " &
                                        GetPersianDate(dateValue) &
                                        " معتبر نیست.")

                                End If

                                selectedFood = selectedValue

                            End If

                            SaveReservation(
                                conn,
                                tran,
                                userId,
                                dateValue,
                                selectedFood,
                                isLeave)

                            reservationCount += 1

                        Next

                        tran.Commit()

                    Catch
                        tran.Rollback()
                        Throw
                    End Try

                End Using

            End Using

            ' نمایش مجدد اطلاعات پس از ذخیره
            LoadWeek()

            lblMessage.CssClass = "lunch-message success"

            lblMessage.Text =
                "✓ عملیات با موفقیت انجام شد!" &
                "<br/>رزروهای ناهار شما با موفقیت ثبت یا ویرایش شدند." &
                "<br/>تعداد رزروهای ثبت یا ویرایش‌شده: " &
                reservationCount.ToString() &
                "<br/>تعداد رزروهای حذف‌شده: " &
                deletedCount.ToString()

        Catch ex As Exception

            lblMessage.CssClass = "lunch-message error"

            lblMessage.Text =
                "✕ عملیات انجام نشد!" &
                "<br/>" &
                Server.HtmlEncode(ex.Message)

        End Try

    End Sub

    '====================================================
    ' ذخیره یا ویرایش رزرو
    '====================================================
    Private Sub SaveReservation(
        ByVal conn As SqlConnection,
        ByVal tran As SqlTransaction,
        ByVal userId As String,
        ByVal menuDate As DateTime,
        ByVal selectedFood As String,
        ByVal isLeave As Boolean)

        Dim sql As String =
            "IF EXISTS (" &
            " SELECT 1 FROM dbo.RestaurantLunchReservations " &
            " WITH (UPDLOCK, HOLDLOCK) " &
            " WHERE UserID=@UserID AND MenuDate=@MenuDate " &
            ") " &
            "BEGIN " &
            " UPDATE dbo.RestaurantLunchReservations " &
            " SET SelectedFood=@SelectedFood, " &
            " IsLeave=@IsLeave, ModifiedDate=GETDATE() " &
            " WHERE UserID=@UserID AND MenuDate=@MenuDate " &
            "END " &
            "ELSE " &
            "BEGIN " &
            " INSERT INTO dbo.RestaurantLunchReservations " &
            " (UserID, MenuDate, SelectedFood, IsLeave) " &
            " VALUES (@UserID, @MenuDate, @SelectedFood, @IsLeave) " &
            "END"

        Using cmd As New SqlCommand(sql, conn, tran)

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userId
            cmd.Parameters.Add("@MenuDate", SqlDbType.Date).Value = menuDate.Date

            cmd.Parameters.Add("@SelectedFood", SqlDbType.NVarChar, 200).Value =
                selectedFood

            cmd.Parameters.Add("@IsLeave", SqlDbType.Bit).Value = isLeave

            cmd.ExecuteNonQuery()

        End Using

    End Sub

    '====================================================
    ' حذف رزرو قبلی؛ برای پنج‌شنبه و جمعه بدون انتخاب
    '====================================================
    Private Sub DeleteReservation(
        ByVal conn As SqlConnection,
        ByVal tran As SqlTransaction,
        ByVal userId As String,
        ByVal menuDate As DateTime)

        Dim sql As String =
            "DELETE FROM dbo.RestaurantLunchReservations " &
            "WHERE UserID=@UserID AND MenuDate=@MenuDate"

        Using cmd As New SqlCommand(sql, conn, tran)

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userId
            cmd.Parameters.Add("@MenuDate", SqlDbType.Date).Value = menuDate.Date

            cmd.ExecuteNonQuery()

        End Using

    End Sub

    '====================================================
    ' دریافت رزروهای قبلی کاربر
    '====================================================
    Private Function GetSavedReservations() As Dictionary(Of DateTime, String)

        Dim result As New Dictionary(Of DateTime, String)

        Dim userId As String = Convert.ToString(Session("userid"))

        Using conn As New SqlConnection(ClsConnection.GetConn())

            conn.Open()

            Dim sql As String =
                "SELECT MenuDate, SelectedFood, IsLeave " &
                "FROM dbo.RestaurantLunchReservations " &
                "WHERE UserID=@UserID"

            Using cmd As New SqlCommand(sql, conn)

                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userId

                Using rd As SqlDataReader = cmd.ExecuteReader()

                    While rd.Read()

                        Dim menuDate As DateTime =
                            Convert.ToDateTime(rd("MenuDate")).Date

                        Dim selectedFood As String

                        If Convert.ToBoolean(rd("IsLeave")) Then
                            selectedFood = "مرخصی"
                        Else
                            selectedFood = Convert.ToString(rd("SelectedFood"))
                        End If

                        result(menuDate) = selectedFood

                    End While

                End Using

            End Using

        End Using

        Return result

    End Function

    '====================================================
    ' دریافت روزهای بسته‌شده توسط مدیریت
    '====================================================
    Private Function GetClosedWeekdays() As HashSet(Of Integer)

        Dim result As New HashSet(Of Integer)

        Using conn As New SqlConnection(ClsConnection.GetConn())

            conn.Open()

            Dim sql As String =
                "SELECT WeekdayNo FROM dbo.RestaurantWeekdaySettings " &
                "WHERE IsOpen=0"

            Using cmd As New SqlCommand(sql, conn)

                Using rd As SqlDataReader = cmd.ExecuteReader()

                    While rd.Read()
                        result.Add(Convert.ToInt32(rd("WeekdayNo")))
                    End While

                End Using

            End Using

        End Using

        Return result

    End Function

    '====================================================
    ' تبدیل تاریخ میلادی به شمسی
    '====================================================
    Private Function GetPersianDate(ByVal value As DateTime) As String

        Dim pc As New PersianCalendar()

        Return String.Format(
            "{0:0000}/{1:00}/{2:00}",
            pc.GetYear(value),
            pc.GetMonth(value),
            pc.GetDayOfMonth(value))

    End Function

    '====================================================
    ' نام روز هفته
    '====================================================
    Private Function GetWeekdayName(ByVal value As DateTime) As String

        Select Case value.DayOfWeek

            Case DayOfWeek.Saturday
                Return "شنبه"

            Case DayOfWeek.Sunday
                Return "یکشنبه"

            Case DayOfWeek.Monday
                Return "دوشنبه"

            Case DayOfWeek.Tuesday
                Return "سه‌شنبه"

            Case DayOfWeek.Wednesday
                Return "چهارشنبه"

            Case DayOfWeek.Thursday
                Return "پنج‌شنبه"

            Case Else
                Return "جمعه"

        End Select

    End Function

    '====================================================
    ' مدل اطلاعات هر روز
    '====================================================
    Public Class LunchMenuDay

        Public Property MenuDate As DateTime
        Public Property MenuDateIso As String
        Public Property DateText As String
        Public Property WeekdayName As String
        Public Property Foods As List(Of String)
        Public Property IsReservable As Boolean

    End Class

End Class
