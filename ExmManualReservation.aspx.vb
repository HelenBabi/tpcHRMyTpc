Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text
Imports System.Configuration

Partial Class ExmManualReservation
    Inherits System.Web.UI.Page

    ' ============================================================
    ' Page Load
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUnits()
            pnlPersonnelList.Visible = False
            pnlSelected.Visible = False

            Dim dtEmpty As New DataTable()
            dtEmpty.Columns.Add("ID", GetType(Integer))
            dtEmpty.Columns.Add("PersonCode", GetType(String))
            dtEmpty.Columns.Add("FullName", GetType(String))
            dtEmpty.Columns.Add("NationalCode", GetType(String))
            dtEmpty.Columns.Add("Position", GetType(String))
            dtEmpty.Columns.Add("Mobile", GetType(String))
            ViewState("SelectedPersonnel") = dtEmpty
        End If

        ' ===== ✅ نمایش پیام موفقیت از Postback قبلی =====
        If ViewState("SuccessMessage") IsNot Nothing Then
            Dim successMsg As String = ViewState("SuccessMessage").ToString()
            If Not String.IsNullOrEmpty(successMsg) Then
                ShowMessage(successMsg, "success")
                ViewState("SuccessMessage") = Nothing
            End If
        End If
    End Sub

    ' ============================================================
    ' ✅ تابع کمکی: اجرای INSERT/UPDATE/DELETE
    '    با استفاده از ClsConnection.GetConn
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
    ' بارگذاری لیست واحدها
    ' ============================================================
    Private Sub LoadUnits()
        Try
            Dim sql As String = "SELECT ID, UnitName FROM exmUnits WHERE IsActive = 1 ORDER BY UnitName"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)
            ddlUnit.Items.Clear()
            ddlUnit.Items.Add(New ListItem("-- انتخاب واحد --", "0"))
            For Each row As DataRow In dt.Rows
                ddlUnit.Items.Add(New ListItem(row("UnitName").ToString(), row("ID").ToString()))
            Next
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری واحدها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' وقتی واحد انتخاب می‌شود → لیست تاریخ‌ها
    ' ============================================================
    Protected Sub ddlUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        Try
            ddlDate.Items.Clear()
            pnlPersonnelList.Visible = False

            Dim unitID As Integer = Convert.ToInt32(ddlUnit.SelectedValue)
            If unitID = 0 Then
                ddlDate.Items.Add(New ListItem("-- ابتدا واحد را انتخاب کنید --", "0"))
                lblCapacityInfo.Text = ""
                Return
            End If

            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT d.ID, d.ExaminationDate, dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, ")
            sql.AppendLine("       ISNULL((SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc WHERE uc.CapacityID = d.ID AND uc.UnitID = " & unitID & "), 0) AS Capacity, ")
            sql.AppendLine("       ISNULL((SELECT COUNT(*) FROM exmReservationRequests r ")
            sql.AppendLine("               WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, d.ExaminationDate) ")
            sql.AppendLine("                 AND r.UnitID = " & unitID & " AND r.StatusCode NOT IN (6,7,8)), 0) AS Reserved ")
            sql.AppendLine("FROM exmDailyCapacity d ")
            sql.AppendLine("WHERE d.IsActive = 1 ")
            sql.AppendLine("  AND EXISTS (SELECT 1 FROM exmUnitCapacity uc2 WHERE uc2.CapacityID = d.ID AND uc2.UnitID = " & unitID & " AND uc2.Capacity > 0) ")
            sql.AppendLine("  AND d.ExaminationDate >= CAST(GETDATE() AS DATE) ")
            sql.AppendLine("ORDER BY d.ExaminationDate")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            If dt.Rows.Count = 0 Then
                ddlDate.Items.Add(New ListItem("-- هیچ تاریخ آینده‌ای برای این واحد تعریف نشده --", "0"))
                lblCapacityInfo.Text = "<span class='capacity-box cap-danger'>هیچ تاریخ فعالی نیست</span>"
                Return
            End If

            ddlDate.Items.Add(New ListItem("-- انتخاب تاریخ --", "0"))
            For Each row As DataRow In dt.Rows
                Dim dailyID As String = row("ID").ToString()
                Dim persianDate As String = row("PersianDate").ToString()
                Dim cap As Integer = Convert.ToInt32(row("Capacity"))
                Dim res As Integer = Convert.ToInt32(row("Reserved"))
                Dim avail As Integer = cap - res
                If avail < 0 Then
                    avail = 0
                End If

                Dim txt As String = persianDate & " (جا: " & avail & " از " & cap & ")"
                ddlDate.Items.Add(New ListItem(txt, dailyID))

                ViewState("Cap_" & dailyID) = cap
                ViewState("Res_" & dailyID) = res
            Next

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری تاریخ‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' وقتی تاریخ انتخاب می‌شود → نمایش ظرفیت + لیست پرسنل
    ' ============================================================
    Protected Sub ddlDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDate.SelectedIndexChanged
        Try
            Dim dailyID As Integer = Convert.ToInt32(ddlDate.SelectedValue)
            If dailyID = 0 Then
                lblCapacityInfo.Text = ""
                pnlPersonnelList.Visible = False
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
            If avail < 0 Then
                avail = 0
            End If

            Dim boxClass As String = "cap-ok"
            If avail = 0 Then
                boxClass = "cap-full"
            ElseIf avail < 3 Then
                boxClass = "cap-warn"
            End If

            lblCapacityInfo.Text = "<span class='capacity-box " & boxClass & "'>" & _
                                    "ظرفیت: " & cap & " | رزرو: " & res & " | " & _
                                    "جا: " & avail & "</span>"

            LoadPersonnelList(dailyID)

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری لیست پرسنلی که در این تاریخ رزرو ندارند
    ' ============================================================
    Private Sub LoadPersonnelList(ByVal dailyID As Integer)
        Try
            Dim unitID As Integer = Convert.ToInt32(ddlUnit.SelectedValue)
            If unitID = 0 OrElse dailyID = 0 Then Return

            Dim sqlDate As String = "SELECT ExaminationDate FROM exmDailyCapacity WHERE ID = " & dailyID
            Dim dtDate As DataTable = ClsDataTable.GetDataTable(sqlDate)
            If dtDate.Rows.Count = 0 Then Return
            Dim examDate As String = Convert.ToDateTime(dtDate.Rows(0)("ExaminationDate")).ToString("yyyy-MM-dd")

            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    p.ID, ")
            sql.AppendLine("    CAST(p.codePersoneli AS NVARCHAR(50)) AS PersonCode, ")
            sql.AppendLine("    (ISNULL(p.nam,'') + ' ' + ISNULL(p.family,'')) AS FullName, ")
            sql.AppendLine("    ISNULL(p.codeMelli,'-') AS NationalCode, ")
            sql.AppendLine("    ISNULL(p.postname,'-') AS Position, ")
            sql.AppendLine("    ISNULL(p.mobile,'-') AS Mobile ")
            sql.AppendLine("FROM personel p ")
            sql.AppendLine("INNER JOIN exmUnits u ON p.UnitsCode = u.OldUnitCode ")
            sql.AppendLine("WHERE u.ID = " & unitID & " ")
            sql.AppendLine("  AND ISNULL(p.CodeClosed, 0) = 0 ")
            sql.AppendLine("  AND NOT EXISTS ( ")
            sql.AppendLine("      SELECT 1 FROM exmReservationRequests r ")
            sql.AppendLine("      WHERE r.PersonCode = CAST(p.codePersoneli AS NVARCHAR(50)) ")
            sql.AppendLine("        AND CONVERT(date, r.ExaminationDate) = CONVERT(date, '" & examDate & "') ")
            sql.AppendLine("        AND r.StatusCode NOT IN (6, 7, 8) ")
            sql.AppendLine("  ) ")
            sql.AppendLine("ORDER BY p.family, p.nam")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            If Not dt.Columns.Contains("RowNumber") Then dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            ViewState("AvailablePersonnel") = dt
            gvPersonnel.DataSource = dt
            gvPersonnel.DataBind()

            lblPersonnelCount.Text = dt.Rows.Count.ToString()
            pnlPersonnelList.Visible = dt.Rows.Count > 0

            If dt.Rows.Count = 0 Then
                ShowMessage("ℹ️ همه پرسنل این واحد در این تاریخ رزرو دارند.", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری لیست پرسنل: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' جستجوی پرسنل
    ' ============================================================
    Protected Sub btnSearchPersonnel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchPersonnel.Click
        Try
            Dim searchTerm As String = txtSearch.Text.Trim()
            If String.IsNullOrEmpty(searchTerm) Then
                ShowMessage("لطفاً عبارت جستجو را وارد کنید", "warning")
                Return
            End If

            Dim dailyID As Integer = Convert.ToInt32(ddlDate.SelectedValue)
            If dailyID = 0 Then
                ShowMessage("لطفاً ابتدا تاریخ را انتخاب کنید", "warning")
                Return
            End If

            Dim dt As DataTable = CType(ViewState("AvailablePersonnel"), DataTable)
            If dt Is Nothing Then
                ShowMessage("لطفاً ابتدا واحد و تاریخ را انتخاب کنید", "warning")
                Return
            End If

            Dim safeTerm As String = searchTerm.Replace("'", "''").Replace("[", "").Replace("]", "")

            Dim filter As String = "PersonCode LIKE '%" & safeTerm & "%' " & _
                                   "OR FullName LIKE '%" & safeTerm & "%' " & _
                                   "OR NationalCode LIKE '%" & safeTerm & "%'"

            Dim dv As New DataView(dt)
            dv.RowFilter = filter

            Dim dtFiltered As DataTable = dv.ToTable()
            If Not dtFiltered.Columns.Contains("RowNumber") Then dtFiltered.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dtFiltered.Rows.Count - 1
                dtFiltered.Rows(i)("RowNumber") = i + 1
            Next

            gvPersonnel.DataSource = dtFiltered
            gvPersonnel.DataBind()

            lblPersonnelCount.Text = dtFiltered.Rows.Count.ToString()
            pnlPersonnelList.Visible = True

            If dtFiltered.Rows.Count = 0 Then
                ShowMessage("ℹ️ هیچ پرسنلی با این عبارت یافت نشد", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در جستجو: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' نمایش همه پرسنل واحد
    ' ============================================================
    Protected Sub btnShowAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowAll.Click
        txtSearch.Text = ""
        Dim dailyID As Integer = Convert.ToInt32(ddlDate.SelectedValue)
        If dailyID > 0 Then
            LoadPersonnelList(dailyID)
        Else
            ShowMessage("لطفاً ابتدا تاریخ را انتخاب کنید", "warning")
        End If
    End Sub

    ' ============================================================
    ' انتخاب/حذف پرسنل
    ' ============================================================
    Protected Sub gvPersonnel_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvPersonnel.RowCommand
        Try
            If e.CommandName = "SelectPerson" Then
                Dim personID As Integer = Convert.ToInt32(e.CommandArgument)

                Dim dtSelected As DataTable = CType(ViewState("SelectedPersonnel"), DataTable)
                If dtSelected Is Nothing Then
                    dtSelected = New DataTable()
                    dtSelected.Columns.Add("ID", GetType(Integer))
                    dtSelected.Columns.Add("PersonCode", GetType(String))
                    dtSelected.Columns.Add("FullName", GetType(String))
                    dtSelected.Columns.Add("NationalCode", GetType(String))
                    dtSelected.Columns.Add("Position", GetType(String))
                    dtSelected.Columns.Add("Mobile", GetType(String))
                End If

                Dim exists As Boolean = False
                For Each r As DataRow In dtSelected.Rows
                    If Convert.ToInt32(r("ID")) = personID Then
                        exists = True
                        Exit For
                    End If
                Next

                If exists Then
                    ShowMessage("این پرسنل قبلاً به سبد اضافه شده است", "warning")
                    Return
                End If

                Dim dtAvailable As DataTable = CType(ViewState("AvailablePersonnel"), DataTable)
                If dtAvailable IsNot Nothing Then
                    For Each row As DataRow In dtAvailable.Rows
                        If Convert.ToInt32(row("ID")) = personID Then
                            Dim newRow As DataRow = dtSelected.NewRow()
                            newRow("ID") = personID
                            newRow("PersonCode") = row("PersonCode").ToString()
                            newRow("FullName") = row("FullName").ToString()
                            newRow("NationalCode") = row("NationalCode").ToString()
                            newRow("Position") = row("Position").ToString()
                            newRow("Mobile") = row("Mobile").ToString()
                            dtSelected.Rows.Add(newRow)
                            Exit For
                        End If
                    Next
                End If

                ViewState("SelectedPersonnel") = dtSelected
                BindSelectedGrid()

                ShowMessage("✅ پرسنل به سبد اضافه شد", "success")
            End If

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gvSelected_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvSelected.RowCommand
        Try
            If e.CommandName = "RemovePerson" Then
                Dim personID As Integer = Convert.ToInt32(e.CommandArgument)
                Dim dtSelected As DataTable = CType(ViewState("SelectedPersonnel"), DataTable)
                If dtSelected IsNot Nothing Then
                    For i As Integer = dtSelected.Rows.Count - 1 To 0 Step -1
                        If Convert.ToInt32(dtSelected.Rows(i)("ID")) = personID Then
                            dtSelected.Rows.RemoveAt(i)
                        End If
                    Next
                    ViewState("SelectedPersonnel") = dtSelected
                    BindSelectedGrid()
                    ShowMessage("پرسنل از سبد حذف شد", "info")
                End If
            End If
        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بایند سبد
    ' ============================================================
    Private Sub BindSelectedGrid()
        Try
            Dim dtSelected As DataTable = CType(ViewState("SelectedPersonnel"), DataTable)
            If dtSelected Is Nothing OrElse dtSelected.Rows.Count = 0 Then
                pnlSelected.Visible = False
                Return
            End If

            Dim dtDisplay As DataTable = dtSelected.Copy()
            If Not dtDisplay.Columns.Contains("RowNumber") Then dtDisplay.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dtDisplay.Rows.Count - 1
                dtDisplay.Rows(i)("RowNumber") = i + 1
            Next

            lblSelectedUnit.Text = ddlUnit.SelectedItem.Text
            lblSelectedDate.Text = ddlDate.SelectedItem.Text

            gvSelected.DataSource = dtDisplay
            gvSelected.DataBind()
            pnlSelected.Visible = True

        Catch
        End Try
    End Sub

    ' ============================================================
    ' حذف کل سبد
    ' ============================================================
    Protected Sub btnClearSelected_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSelected.Click
        Dim dtEmpty As New DataTable()
        dtEmpty.Columns.Add("ID", GetType(Integer))
        dtEmpty.Columns.Add("PersonCode", GetType(String))
        dtEmpty.Columns.Add("FullName", GetType(String))
        dtEmpty.Columns.Add("NationalCode", GetType(String))
        dtEmpty.Columns.Add("Position", GetType(String))
        dtEmpty.Columns.Add("Mobile", GetType(String))
        ViewState("SelectedPersonnel") = dtEmpty
        pnlSelected.Visible = False
        ShowMessage("سبد پاک شد", "info")
    End Sub

    ' ============================================================
    ' ✅ ثبت نهایی (با پیام موفقیت پایدار)
    ' ============================================================
    Protected Sub btnSubmitAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmitAll.Click
        Try
            Dim dailyID As Integer = Convert.ToInt32(ddlDate.SelectedValue)
            Dim unitID As Integer = Convert.ToInt32(ddlUnit.SelectedValue)
            If dailyID = 0 OrElse unitID = 0 Then
                ShowMessage("لطفاً واحد و تاریخ را انتخاب کنید", "warning")
                Return
            End If

            Dim dtSelected As DataTable = CType(ViewState("SelectedPersonnel"), DataTable)
            If dtSelected Is Nothing OrElse dtSelected.Rows.Count = 0 Then
                ShowMessage("سبد خالی است. ابتدا پرسنل اضافه کنید", "warning")
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
            If avail < 0 Then
                avail = 0
            End If

            If dtSelected.Rows.Count > avail Then
                ShowMessage("❌ ظرفیت کافی نیست! فقط " & avail & " جا خالی است ولی " & dtSelected.Rows.Count & " نفر انتخاب شده‌اید.", "danger")
                Return
            End If

            Dim sqlDate As String = "SELECT ExaminationDate FROM exmDailyCapacity WHERE ID = " & dailyID
            Dim dtDate As DataTable = ClsDataTable.GetDataTable(sqlDate)
            If dtDate.Rows.Count = 0 Then
                ShowMessage("❌ تاریخ یافت نشد", "danger")
                Return
            End If
            Dim examDate As String = Convert.ToDateTime(dtDate.Rows(0)("ExaminationDate")).ToString("yyyy-MM-dd")

            Dim sqlUnit As String = "SELECT UnitName FROM exmUnits WHERE ID = " & unitID
            Dim dtUnit As DataTable = ClsDataTable.GetDataTable(sqlUnit)
            Dim unitName As String = ""
            If dtUnit.Rows.Count > 0 Then
                unitName = dtUnit.Rows(0)("UnitName").ToString()
            End If

            Dim comment As String = txtComment.Text.Trim()
            Dim currentUser As String = "HealthExpert"
            Try
                If Session("Username") IsNot Nothing Then
                    currentUser = Session("Username").ToString()
                End If
            Catch
            End Try

            Dim successCount As Integer = 0
            Dim errorCount As Integer = 0
            Dim errorMessages As New StringBuilder()

            For Each row As DataRow In dtSelected.Rows
                Try
                    Dim personCode As String = row("PersonCode").ToString().Replace("'", "''")
                    Dim fullName As String = row("FullName").ToString().Replace("'", "''")
                    Dim nationalCode As String = row("NationalCode").ToString().Replace("'", "''")
                    Dim mobile As String = row("Mobile").ToString().Replace("'", "''")
                    Dim position As String = row("Position").ToString().Replace("'", "''")

                    Dim sqlDup As String = "SELECT COUNT(*) AS Cnt FROM exmReservationRequests " & _
                                           "WHERE PersonCode = '" & personCode & "' " & _
                                           "  AND CONVERT(date, ExaminationDate) = CONVERT(date, '" & examDate & "') " & _
                                           "  AND StatusCode NOT IN (6,7,8)"
                    Dim dtDup As DataTable = ClsDataTable.GetDataTable(sqlDup)
                    If Convert.ToInt32(dtDup.Rows(0)("Cnt")) > 0 Then
                        errorCount += 1
                        errorMessages.AppendLine("⚠️ " & fullName & " قبلاً رزرو دارد")
                        Continue For
                    End If

                    Dim sqlInsert As New StringBuilder()
                    sqlInsert.AppendLine("INSERT INTO exmReservationRequests ( ")
                    sqlInsert.AppendLine("    PersonCode, FullName, NationalCode, Mobile, Position, ")
                    sqlInsert.AppendLine("    UnitID, UnitName, ExaminationDate, ")
                    sqlInsert.AppendLine("    StatusCode, StatusDescription, ")
                    sqlInsert.AppendLine("    CreateUserID, CreateDate, RequestDate, PersonnelDescription ")
                    sqlInsert.AppendLine(") VALUES ( ")
                    sqlInsert.AppendLine("    '" & personCode & "', ")
                    sqlInsert.AppendLine("    N'" & fullName & "', ")
                    sqlInsert.AppendLine("    '" & nationalCode & "', ")
                    sqlInsert.AppendLine("    '" & mobile & "', ")
                    sqlInsert.AppendLine("    N'" & position & "', ")
                    sqlInsert.AppendLine("    " & unitID & ", ")
                    sqlInsert.AppendLine("    N'" & unitName.Replace("'", "''") & "', ")
                    sqlInsert.AppendLine("    '" & examDate & "', ")
                    sqlInsert.AppendLine("    1, ")
                    sqlInsert.AppendLine("    N'ثبت دستی توسط کارشناس بهداشت', ")
                    sqlInsert.AppendLine("    N'" & currentUser.Replace("'", "''") & "', ")
                    sqlInsert.AppendLine("    GETDATE(), ")
                    sqlInsert.AppendLine("    GETDATE(), ")
                    sqlInsert.AppendLine("    N'" & comment.Replace("'", "''") & "' ")
                    sqlInsert.AppendLine(")")

                    ExecuteNonQueryLocal(sqlInsert.ToString())
                    successCount += 1

                Catch innerEx As Exception
                    errorCount += 1
                    errorMessages.AppendLine("❌ " & row("FullName").ToString() & ": " & innerEx.Message)
                End Try
            Next

            If successCount > 0 Then
                ' ===== ۱. پاک کردن سبد =====
                Dim dtEmpty As New DataTable()
                dtEmpty.Columns.Add("ID", GetType(Integer))
                dtEmpty.Columns.Add("PersonCode", GetType(String))
                dtEmpty.Columns.Add("FullName", GetType(String))
                dtEmpty.Columns.Add("NationalCode", GetType(String))
                dtEmpty.Columns.Add("Position", GetType(String))
                dtEmpty.Columns.Add("Mobile", GetType(String))
                ViewState("SelectedPersonnel") = dtEmpty
                pnlSelected.Visible = False
                txtComment.Text = ""

                ' ===== ۲. رفرش لیست تاریخ‌ها (ظرفیت کم شده) =====
                ddlUnit_SelectedIndexChanged(sender, e)

                ' ===== ۳. نمایش پیام موفقیت =====
                Dim msg As String = "✅ " & successCount & " پرسنل با موفقیت ثبت شدند."
                If errorCount > 0 Then
                    msg &= " (خطا: " & errorCount & " نفر)"
                End If
                ShowMessage(msg, "success")

                ' ===== ۴. ذخیره در ViewState برای Postback بعدی =====
                ViewState("SuccessMessage") = msg
            Else
                ShowMessage("❌ هیچ پرسنلی ثبت نشد. " & errorMessages.ToString(), "danger")
            End If

        Catch ex As Exception
            ShowMessage("خطا در ثبت نهایی: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvPersonnel_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvPersonnel.PageIndexChanging
        gvPersonnel.PageIndex = e.NewPageIndex
        Dim dtAvailable As DataTable = CType(ViewState("AvailablePersonnel"), DataTable)
        If dtAvailable IsNot Nothing Then
            gvPersonnel.DataSource = dtAvailable
            gvPersonnel.DataBind()
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