Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text
Imports System.Configuration

Partial Class ExmChangeReservationDate
    Inherits System.Web.UI.Page

    ' ============================================================
    ' Page Load
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadReservations()
            LoadAvailableDates()
            pnlChange.Visible = False

            ' === ✅ خواندن پیام موفقیت از Session (بعد از Redirect) ===
            If Session("ExmChangeDateSuccessMsg") IsNot Nothing Then
                Dim successMsg As String = Session("ExmChangeDateSuccessMsg").ToString()
                If Not String.IsNullOrEmpty(successMsg) Then
                    ShowMessage(successMsg, "success")
                    Session("ExmChangeDateSuccessMsg") = Nothing
                End If
            End If
        End If
    End Sub

    ' ============================================================
    ' اجرای دستور دیتابیس
    ' ============================================================
    Private Function ExecuteNonQueryLocal(ByVal sql As String) As Integer
        Try
            Dim cnn As New System.Data.SqlClient.SqlConnection()
            cnn.ConnectionString = ClsConnection.GetConn
            Using cnn
                Using cmd As New System.Data.SqlClient.SqlCommand(sql, cnn)
                    cnn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("خطا در اجرای دستور دیتابیس: " & ex.Message)
        End Try
    End Function

    ' ============================================================
    ' ✅ بارگذاری لیست رزروهای فعال
    ' ============================================================
    Private Sub LoadReservations()
        Try
            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    r.ID AS ReservationID, ")
            sql.AppendLine("    ISNULL(r.PersonCode,'') AS PersonCode, ")
            sql.AppendLine("    ISNULL(r.FullName,'') AS FullName, ")
            sql.AppendLine("    ISNULL(r.NationalCode,'') AS NationalCode, ")
            sql.AppendLine("    ISNULL(r.UnitName,'-') AS UnitName, ")
            sql.AppendLine("    dbo.FormatDateFa(r.ExaminationDate) AS CurrentDateFa, ")
            sql.AppendLine("    CONVERT(varchar(10), r.ExaminationDate, 120) AS CurrentDate, ")
            sql.AppendLine("    ISNULL(r.StatusDescription,'') AS StatusDescription ")
            sql.AppendLine("FROM exmReservationRequests r ")
            sql.AppendLine("WHERE r.StatusCode NOT IN (6,7,8) ")
            sql.AppendLine("ORDER BY r.ExaminationDate DESC, r.FullName")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            If Not dt.Columns.Contains("RowNumber") Then dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            ViewState("Reservations") = dt
            gvReservations.DataSource = dt
            gvReservations.DataBind()
            lblCount.Text = dt.Rows.Count.ToString()

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری رزروها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ بارگذاری تاریخ‌های دارای ظرفیت خالی (برای dropdown تاریخ جدید)
    ' ============================================================
    Private Sub LoadAvailableDates()
        Try
            Dim selectedDate As String = ddlNewDate.SelectedValue
            ddlNewDate.Items.Clear()

            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT d.ID, d.ExaminationDate, dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, ")
            sql.AppendLine("       ISNULL(SUM(uc.Capacity), 0) AS TotalCapacity, ")
            sql.AppendLine("       ISNULL((SELECT COUNT(*) FROM exmReservationRequests r ")
            sql.AppendLine("               WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, d.ExaminationDate) ")
            sql.AppendLine("                 AND r.StatusCode NOT IN (6,7,8)), 0) AS TotalReserved ")
            sql.AppendLine("FROM exmDailyCapacity d ")
            sql.AppendLine("LEFT JOIN exmUnitCapacity uc ON uc.CapacityID = d.ID ")
            sql.AppendLine("WHERE d.IsActive = 1 ")
            sql.AppendLine("  AND d.ExaminationDate >= CAST(GETDATE() AS DATE) ")
            sql.AppendLine("GROUP BY d.ID, d.ExaminationDate ")
            sql.AppendLine("HAVING ISNULL(SUM(uc.Capacity), 0) > 0 ")
            sql.AppendLine("ORDER BY d.ExaminationDate")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            ddlNewDate.Items.Add(New ListItem("-- انتخاب تاریخ جدید --", "0"))

            If dt.Rows.Count = 0 Then
                lblCapacityInfo.Text = "<span class='capacity-box cap-danger'>هیچ تاریخ فعالی وجود ندارد</span>"
                Return
            End If

            For Each row As DataRow In dt.Rows
                Dim dailyID As String = row("ID").ToString()
                Dim persianDate As String = row("PersianDate").ToString()
                Dim cap As Integer = Convert.ToInt32(row("TotalCapacity"))
                Dim res As Integer = Convert.ToInt32(row("TotalReserved"))
                Dim avail As Integer = cap - res
                If avail < 0 Then avail = 0

                Dim txt As String = persianDate & " (کل: " & cap & " | رزرو: " & res & " | خالی: " & avail & ")"
                ddlNewDate.Items.Add(New ListItem(txt, dailyID))

                ViewState("Cap_" & dailyID) = cap
                ViewState("Res_" & dailyID) = res
            Next

            ' برگرداندن انتخاب قبلی
            If Not String.IsNullOrEmpty(selectedDate) AndAlso selectedDate <> "0" Then
                Try
                    Dim selItem As ListItem = ddlNewDate.Items.FindByValue(selectedDate)
                    If selItem IsNot Nothing Then
                        ddlNewDate.ClearSelection()
                        selItem.Selected = True
                    End If
                Catch
                End Try
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری تاریخ‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' جستجو در رزروها
    ' ============================================================
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Dim searchTerm As String = txtSearch.Text.Trim()
            If String.IsNullOrEmpty(searchTerm) Then
                ShowMessage("لطفاً عبارت جستجو را وارد کنید", "warning")
                Return
            End If

            Dim dt As DataTable = CType(ViewState("Reservations"), DataTable)
            If dt Is Nothing Then
                LoadReservations()
                dt = CType(ViewState("Reservations"), DataTable)
                If dt Is Nothing Then Return
            End If

            Dim safeTerm As String = searchTerm.Replace("'", "''").Replace("[", "").Replace("]", "")

            Dim filter As String = "FullName LIKE '%" & safeTerm & "%' " &
                                   "OR NationalCode LIKE '%" & safeTerm & "%' " &
                                   "OR PersonCode LIKE '%" & safeTerm & "%'"

            Dim dv As New DataView(dt)
            dv.RowFilter = filter

            Dim dtFiltered As DataTable = dv.ToTable()
            If Not dtFiltered.Columns.Contains("RowNumber") Then dtFiltered.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dtFiltered.Rows.Count - 1
                dtFiltered.Rows(i)("RowNumber") = i + 1
            Next

            gvReservations.DataSource = dtFiltered
            gvReservations.DataBind()
            lblCount.Text = dtFiltered.Rows.Count.ToString()

            If dtFiltered.Rows.Count = 0 Then
                ShowMessage("ℹ️ هیچ رزروی با این عبارت یافت نشد", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در جستجو: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' نمایش همه
    ' ============================================================
    Protected Sub btnShowAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowAll.Click
        txtSearch.Text = ""
        LoadReservations()
        pnlChange.Visible = False
    End Sub

    ' ============================================================
    ' انتخاب رزرو → نمایش پنل تغییر تاریخ
    ' ============================================================
    'Protected Sub gvReservations_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvReservations.RowCommand
    '    Try
    '        If e.CommandName = "SelectReservation" Then
    '            Dim reservationID As Integer = Convert.ToInt32(e.CommandArgument)

    '            Dim dt As DataTable = CType(ViewState("Reservations"), DataTable)
    '            If dt Is Nothing Then Return

    '            Dim found As DataRow = Nothing
    '            For Each row As DataRow In dt.Rows
    '                If Convert.ToInt32(row("ReservationID")) = reservationID Then
    '                    found = row
    '                    Exit For
    '                End If
    '            Next

    '            If found Is Nothing Then
    '                ShowMessage("رزرو یافت نشد", "danger")
    '                Return
    '            End If

    '            ViewState("SelectedReservationID") = reservationID
    '            ViewState("SelectedOldDate") = found("CurrentDate").ToString()

    '            lblSelectedPerson.Text = found("FullName").ToString() & " (" & found("NationalCode").ToString() & ")"
    '            lblCurrentDate.Text = found("CurrentDateFa").ToString()

    '            ' پاک کردن انتخاب تاریخ جدید و توضیح
    '            ddlNewDate.ClearSelection()
    '            If ddlNewDate.Items.Count > 0 Then
    '                ddlNewDate.Items(0).Selected = True
    '            End If
    '            txtComment.Text = ""
    '            lblCapacityInfo.Text = ""

    '            pnlChange.Visible = True

    '            ' اسکرول به پنل (اختیاری)
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "scrollToChange",
    '                "document.getElementById('" & pnlChange.ClientID & "').scrollIntoView({behavior:'smooth'});", True)
    '        End If

    '    Catch ex As Exception
    '        ShowMessage("خطا: " & ex.Message, "danger")
    '    End Try
    'End Sub
    Protected Sub gvReservations_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvReservations.RowCommand
        Try
            ' === ✅ حذف رزرو ===
            If e.CommandName = "DeleteReservation" Then
                Dim reservationID As Integer = Convert.ToInt32(e.CommandArgument)

                ' گرفتن اطلاعات رزرو برای پیام
                Dim dt As DataTable = CType(ViewState("Reservations"), DataTable)
                Dim personName As String = ""
                Dim dateFa As String = ""
                If dt IsNot Nothing Then
                    For Each row As DataRow In dt.Rows
                        If Convert.ToInt32(row("ReservationID")) = reservationID Then
                            personName = row("FullName").ToString()
                            dateFa = row("CurrentDateFa").ToString()
                            Exit For
                        End If
                    Next
                End If

                ' ✅ حذف کامل از دیتابیس
                Dim sqlDelete As String = "DELETE FROM exmReservationRequests WHERE ID = " & reservationID
                ExecuteNonQueryLocal(sqlDelete)

                ' پیام موفقیت + Redirect (همان الگوی تغییر تاریخ)
                Session("ExmChangeDateSuccessMsg") = "🗑 رزرو «" & personName & "» در تاریخ " & dateFa & " با موفقیت حذف شد."
                Response.Redirect("ExmChangeReservationDate.aspx", False)
                Context.ApplicationInstance.CompleteRequest()
                Return
            End If

            ' === ادامه کد تغییر تاریخ (که قبلاً داشتید) ===
            If e.CommandName = "SelectReservation" Then
                Dim reservationID As Integer = Convert.ToInt32(e.CommandArgument)

                Dim dt As DataTable = CType(ViewState("Reservations"), DataTable)
                If dt Is Nothing Then Return

                Dim found As DataRow = Nothing
                For Each row As DataRow In dt.Rows
                    If Convert.ToInt32(row("ReservationID")) = reservationID Then
                        found = row
                        Exit For
                    End If
                Next

                If found Is Nothing Then
                    ShowMessage("رزرو یافت نشد", "danger")
                    Return
                End If

                ViewState("SelectedReservationID") = reservationID
                ViewState("SelectedOldDate") = found("CurrentDate").ToString()

                lblSelectedPerson.Text = found("FullName").ToString() & " (" & found("NationalCode").ToString() & ")"
                lblCurrentDate.Text = found("CurrentDateFa").ToString()

                ddlNewDate.ClearSelection()
                If ddlNewDate.Items.Count > 0 Then
                    ddlNewDate.Items(0).Selected = True
                End If
                txtComment.Text = ""
                lblCapacityInfo.Text = ""

                pnlChange.Visible = True

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "scrollToChange",
                    "document.getElementById('" & pnlChange.ClientID & "').scrollIntoView({behavior:'smooth'});", True)
            End If

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub
    ' ============================================================
    ' تغییر تاریخ جدید → نمایش ظرفیت
    ' ============================================================
    Protected Sub ddlNewDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNewDate.SelectedIndexChanged
        Try
            Dim dailyID As Integer = Convert.ToInt32(ddlNewDate.SelectedValue)
            If dailyID = 0 Then
                lblCapacityInfo.Text = ""
                Return
            End If

            Dim cap As Integer = 0
            Dim res As Integer = 0
            If ViewState("Cap_" & dailyID.ToString()) IsNot Nothing Then
                cap = Convert.ToInt32(ViewState("Cap_" & dailyID.ToString()))
            End If
            If ViewState("Res_" & dailyID.ToString()) IsNot Nothing Then
                res = Convert.ToInt32(ViewState("Res_" & dailyID.ToString()))
            End If
            Dim avail As Integer = cap - res
            If avail < 0 Then avail = 0

            Dim boxClass As String = "cap-ok"
            If avail = 0 Then
                boxClass = "cap-full"
            ElseIf avail < 3 Then
                boxClass = "cap-warn"
            End If

            lblCapacityInfo.Text = "<span class='capacity-box " & boxClass & "'>" &
                                    "کل ظرفیت: " & cap & " | رزرو: " & res & " | " &
                                    "جا: " & avail & "</span>"

            If avail = 0 Then
                lblCapacityInfo.Text &= " <span style='color:#c00;font-weight:bold;'>⚠️ این تاریخ پر است!</span>"
            End If

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ ثبت تغییر تاریخ
    ' ============================================================
    Protected Sub btnSubmitChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmitChange.Click
        Try
            Dim reservationID As Integer = 0
            If ViewState("SelectedReservationID") IsNot Nothing Then
                reservationID = Convert.ToInt32(ViewState("SelectedReservationID"))
            End If

            If reservationID = 0 Then
                ShowMessage("لطفاً ابتدا یک رزرو را انتخاب کنید", "warning")
                Return
            End If

            Dim newDailyID As Integer = Convert.ToInt32(ddlNewDate.SelectedValue)
            If newDailyID = 0 Then
                ShowMessage("لطفاً تاریخ جدید را انتخاب کنید", "warning")
                Return
            End If

            ' گرفتن تاریخ جدید
            Dim sqlDate As String = "SELECT ExaminationDate FROM exmDailyCapacity WHERE ID = " & newDailyID
            Dim dtDate As DataTable = ClsDataTable.GetDataTable(sqlDate)
            If dtDate.Rows.Count = 0 Then
                ShowMessage("❌ تاریخ جدید یافت نشد", "danger")
                Return
            End If
            Dim newExamDateObj As DateTime = Convert.ToDateTime(dtDate.Rows(0)("ExaminationDate"))
            Dim newExamDate As String = newExamDateObj.ToString("yyyy-MM-dd")

            Dim oldDate As String = ""
            If ViewState("SelectedOldDate") IsNot Nothing Then
                oldDate = ViewState("SelectedOldDate").ToString()
            End If

            If oldDate = newExamDate Then
                ShowMessage("تاریخ جدید با تاریخ فعلی یکسان است", "warning")
                Return
            End If

            ' چک ظرفیت
            Dim checkCapSql As New StringBuilder()
            checkCapSql.AppendLine("SELECT ISNULL(SUM(uc.Capacity),0) AS TotalCap, ")
            checkCapSql.AppendLine("       ISNULL((SELECT COUNT(*) FROM exmReservationRequests r ")
            checkCapSql.AppendLine("               WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, d.ExaminationDate) ")
            checkCapSql.AppendLine("                 AND r.StatusCode NOT IN (6,7,8)), 0) AS Reserved ")
            checkCapSql.AppendLine("FROM exmDailyCapacity d ")
            checkCapSql.AppendLine("LEFT JOIN exmUnitCapacity uc ON uc.CapacityID = d.ID ")
            checkCapSql.AppendLine("WHERE d.ID = " & newDailyID)
            checkCapSql.AppendLine("GROUP BY d.ID, d.ExaminationDate")

            Dim dtCap As DataTable = ClsDataTable.GetDataTable(checkCapSql.ToString())
            If dtCap.Rows.Count > 0 Then
                Dim cap As Integer = Convert.ToInt32(dtCap.Rows(0)("TotalCap"))
                Dim res As Integer = Convert.ToInt32(dtCap.Rows(0)("Reserved"))
                If (cap - res) <= 0 Then
                    ShowMessage("❌ ظرفیت تاریخ جدید پر است. امکان تغییر وجود ندارد.", "danger")
                    Return
                End If
            End If

            Dim comment As String = txtComment.Text.Trim().Replace("'", "''")
            Dim currentUser As String = "HealthExpert"
            Try
                If Session("Username") IsNot Nothing Then
                    currentUser = Session("Username").ToString()
                End If
            Catch
            End Try
            Dim safeUser As String = currentUser.Replace("'", "''")

            ' ✅ UPDATE — StatusDescription دست نمی‌خورد
            Dim sqlUpdate As New StringBuilder()
            sqlUpdate.AppendLine("UPDATE exmReservationRequests SET ")
            sqlUpdate.AppendLine("    ExaminationDate = '" & newExamDate & "', ")
            sqlUpdate.AppendLine("    PersonnelDescription = ISNULL(PersonnelDescription,'') + ")
            sqlUpdate.AppendLine("        CASE WHEN ISNULL(PersonnelDescription,'') = '' THEN '' ELSE ' | ' END + ")
            sqlUpdate.AppendLine("        N'تغییر تاریخ از " & oldDate & " به " & newExamDate & " توسط " & safeUser & "' + ")
            sqlUpdate.AppendLine("        CASE WHEN '" & comment & "' = '' THEN '' ELSE N' - توضیح: " & comment & "' END ")
            sqlUpdate.AppendLine("WHERE ID = " & reservationID)

            ExecuteNonQueryLocal(sqlUpdate.ToString())

            ' تاریخچه (اختیاری)
            Try
                Dim sqlHistory As New StringBuilder()
                sqlHistory.AppendLine("INSERT INTO exmReservationDateChanges ")
                sqlHistory.AppendLine("    (ReservationID, OldDate, NewDate, ChangedBy, ChangeDate, Comment) ")
                sqlHistory.AppendLine("VALUES ( ")
                sqlHistory.AppendLine("    " & reservationID & ", ")
                sqlHistory.AppendLine("    '" & oldDate & "', ")
                sqlHistory.AppendLine("    '" & newExamDate & "', ")
                sqlHistory.AppendLine("    N'" & safeUser & "', ")
                sqlHistory.AppendLine("    GETDATE(), ")
                sqlHistory.AppendLine("    N'" & comment & "' ")
                sqlHistory.AppendLine(")")
                ExecuteNonQueryLocal(sqlHistory.ToString())
            Catch
            End Try

            ' === ✅ ذخیره پیام موفقیت در Session و Redirect ===
            Session("ExmChangeDateSuccessMsg") = "✅ تاریخ معاینه «" & lblSelectedPerson.Text & "» با موفقیت تغییر کرد."
            Response.Redirect("ExmChangeReservationDate.aspx", False)
            Context.ApplicationInstance.CompleteRequest()
            Return

        Catch ex As Exception
            ShowMessage("خطا در ثبت تغییر: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' انصراف از تغییر
    ' ============================================================
    Protected Sub btnCancelChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelChange.Click
        pnlChange.Visible = False
        ViewState("SelectedReservationID") = Nothing
        ViewState("SelectedOldDate") = Nothing
        ddlNewDate.ClearSelection()
        txtComment.Text = ""
        lblCapacityInfo.Text = ""
        ShowMessage("عملیات لغو شد", "info")
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvReservations_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvReservations.PageIndexChanging
        gvReservations.PageIndex = e.NewPageIndex
        Dim dt As DataTable = CType(ViewState("Reservations"), DataTable)
        If dt IsNot Nothing Then
            gvReservations.DataSource = dt
            gvReservations.DataBind()
        End If
    End Sub

    ' ============================================================
    ' RowDataBound — رنگ‌آمیزی رزروهای امروز/فردا (اختیاری)
    ' ============================================================
    Protected Sub gvReservations_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvReservations.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' می‌توانید اینجا شرط خاصی بگذارید، مثلاً اگر تاریخ فعلی امروز است، ردیف قرمز شود
            Try
                Dim currentDateStr As String = DataBinder.Eval(e.Row.DataItem, "CurrentDate").ToString()
                Dim currentDate As DateTime = Convert.ToDateTime(currentDateStr)
                If currentDate.Date = DateTime.Today Then
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 235, 235)
                End If
            Catch
            End Try
        End If
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        pnlMessage.Visible = True
        Select Case type
            Case "success"
                divMessage.Attributes("class") = "mr-alert mr-alert-success"
                lblMessage.Text = "✅ " & message
            Case "danger"
                divMessage.Attributes("class") = "mr-alert mr-alert-danger"
                lblMessage.Text = "❌ " & message
            Case "warning"
                divMessage.Attributes("class") = "mr-alert mr-alert-warning"
                lblMessage.Text = "⚠️ " & message
            Case Else
                divMessage.Attributes("class") = "mr-alert mr-alert-info"
                lblMessage.Text = "ℹ️ " & message
        End Select
    End Sub

End Class