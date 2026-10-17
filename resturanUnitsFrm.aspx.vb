Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data
Imports System.Web.UI.HtmlControls

Partial Class resturanUnitsFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim pc As New PersianCalendar()
            Dim thisYear As Integer = pc.GetYear(DateTime.Now)

            ' سال‌ها
            For y As Integer = thisYear To thisYear + 2
                ddlYear.Items.Add(New ListItem(y.ToString(), y.ToString()))
            Next
            ddlYear.SelectedValue = thisYear.ToString()

            ' ماه‌ها
            Dim months() As String = {"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                                      "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"}
            For i As Integer = 1 To 12
                ddlMonth.Items.Add(New ListItem(months(i - 1), i.ToString()))
            Next
            ddlMonth.SelectedValue = pc.GetMonth(DateTime.Now).ToString()

            BindCalendar()
        End If
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        BindCalendar()
    End Sub

    Private Sub BindCalendar()
        Dim pc As New PersianCalendar()
        Dim year As Integer = Integer.Parse(ddlYear.SelectedValue)
        Dim month As Integer = Integer.Parse(ddlMonth.SelectedValue)
        Dim daysInMonth As Integer = pc.GetDaysInMonth(year, month)

        Dim dt As New DataTable()
        dt.Columns.Add("DayText")
        dt.Columns.Add("FullDate")
        dt.Columns.Add("IsSelected", GetType(Boolean))
        dt.Columns.Add("IsEditable", GetType(Boolean))

        ' --- انتخاب‌های قبلی از دیتابیس
        Dim selectedDates As New List(Of String)
        Using conn As New SqlConnection(ClsConnection.GetConn())
            conn.Open()
            Dim sql As String = "SELECT SelectedDate FROM resturanUserDates WHERE UserID=" & Session("userid") &
                                " AND SelectedDate LIKE '" & year & "/" & month.ToString("00") & "/%'"
            Dim cmd As New SqlCommand(sql, conn)
            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            While rdr.Read()
                selectedDates.Add(rdr("SelectedDate").ToString())
            End While
        End Using

        ' روزهای هفته فارسی
        Dim persianDays As String() = {"یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"}
        Dim monthsName As String() = {"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                                      "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"}

        Dim today As DateTime = DateTime.Now

        For d As Integer = 1 To daysInMonth
            Dim thisDate As DateTime = pc.ToDateTime(year, month, d, 0, 0, 0, 0)
            Dim dbDate As String = String.Format("{0:0000}/{1:00}/{2:00}", year, month, d)
            Dim persianDayName As String = persianDays(CInt(thisDate.DayOfWeek))
            'Dim displayText As String = persianDayName & " " & d.ToString("00") & " " & monthsName(month - 1) & " " & year.ToString()
            Dim displayText As String = persianDayName & " " & d.ToString("00")
            Dim isSelected As Boolean = selectedDates.Contains(dbDate)
            Dim isEditable As Boolean = (thisDate > today.Date)
            dt.Rows.Add(displayText, dbDate, isSelected, isEditable)
        Next

        dlDays.DataSource = dt
        dlDays.DataBind()

        ' ---- نمایش عکس منو ----
        Dim imgPath As String = "~/menuImages/menu_" & month & ".jpg"
        lblMonthName.Text = monthsName(month - 1)
        Dim fullPath As String = Server.MapPath(imgPath)

        If System.IO.File.Exists(fullPath) Then
            imgMenuThumb.ImageUrl = imgPath
            imgMenuThumb.Attributes("data-img") = ResolveUrl(imgPath)
            lblModalMonth.Text = monthsName(month - 1)
            menuSection.Visible = True

            ' نمایش خودکار مدال بعد از بارگذاری
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowMenuModal",
                "setTimeout(function() { showMenuModal(); }, 1000);", True)
        Else
            menuSection.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim year As Integer = Integer.Parse(ddlYear.SelectedValue)
        Dim month As Integer = Integer.Parse(ddlMonth.SelectedValue)

        ' حذف انتخاب‌های قبلی برای همین ماه
        Dim delSql As String = "DELETE FROM resturanUserDates WHERE UserID=" & Session("userid") &
                               " AND SelectedDate LIKE '" & year & "/" & month.ToString("00") & "/%'"
        upStat(delSql)

        ' ذخیره جدیدها
        For Each item As DataListItem In dlDays.Items
            Dim chk As CheckBox = CType(item.FindControl("chkDay"), CheckBox)
            If chk IsNot Nothing AndAlso chk.Checked AndAlso chk.Enabled Then
                Dim selectedDate As String = chk.ToolTip
                Dim sql As String = "INSERT INTO resturanUserDates(UserID, SelectedDate) VALUES(" &
                                    Session("userid") & ", '" & selectedDate & "')"
                upStat(sql)
            End If
        Next

        lblMessage.Text = "✅ تاریخ‌های انتخابی با موفقیت ذخیره شد."
    End Sub

    Private Sub upStat(sqlstr As String)
        Using cnn As New SqlConnection(ClsConnection.GetConn())
            Try
                cnn.Open()
                Dim cmm As New SqlCommand(sqlstr, cnn)
                cmm.ExecuteNonQuery()
            Catch ex As Exception
                ' در صورت نیاز لاگ کن
            End Try
        End Using
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
