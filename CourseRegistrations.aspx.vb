Imports System.Data
Imports System.Data.SqlClient

Partial Class CourseRegistrations
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn
    Private UserID As Integer
    Private ReadOnly PageSize As Integer = 10

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If

        UserID = Convert.ToInt32(Session("userid"))

        If Not IsPostBack Then
            ddlStatus.SelectedValue = "all"
            ddlExamStatus.SelectedValue = "all"
            ddlEvalStatus.SelectedValue = "all"

            LoadCourses()
            LoadRegistrations()
        End If
    End Sub

    Private Sub LoadCourses()
        Try
            Dim dt As New DataTable()
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT CourseID, Title FROM trn_EducationalCourses WHERE IsActive = 1 ORDER BY Title"
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            ddlCourse.DataSource = dt
            ddlCourse.DataTextField = "Title"
            ddlCourse.DataValueField = "CourseID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("-- همه دوره‌ها --", "0"))

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری لیست دوره‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadRegistrations()
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlCourse.SelectedValue)
            Dim status As String = ddlStatus.SelectedValue
            Dim examStatus As String = ddlExamStatus.SelectedValue
            Dim evalStatus As String = ddlEvalStatus.SelectedValue
            Dim searchText As String = txtSearch.Text.Trim()

            Dim dt As New DataTable()
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT cr.RegistrationID, cr.CourseID, cr.UserID, cr.RegistrationDate, " &
                           "cr.IsConfirmed, cr.ConfirmedDate, cr.ConfirmedBy, " &
                           "cr.IsEvaluated, cr.EvaluationDate, " &
                           "p.nam, p.family, p.codePersoneli, " &
                           "u.UnitsName, " &
                           "ec.Title AS CourseTitle, " &
                           "CASE WHEN er.ResultID IS NOT NULL THEN 1 ELSE 0 END AS ExamTaken, " &
                           "CASE WHEN er.IsPassed = 1 THEN 1 ELSE 0 END AS ExamPassed, " &
                           "ISNULL(er.Score, 0) AS ExamScore, " &
                           "CASE WHEN v.RegistrationID IS NOT NULL THEN 1 ELSE 0 END AS AllViewed " &
                           "FROM trn_CourseRegistrations cr " &
                           "INNER JOIN personel p ON cr.UserID = p.ID " &
                           "INNER JOIN tblUnits u ON p.UnitsCode = u.UnitsCode " &
                           "INNER JOIN trn_EducationalCourses ec ON cr.CourseID = ec.CourseID " &
                           "LEFT JOIN trn_ExamResults er ON cr.RegistrationID = er.RegistrationID " &
                           "LEFT JOIN (SELECT DISTINCT RegistrationID FROM trn_ContentViews) v ON cr.RegistrationID = v.RegistrationID " &
                           "WHERE cr.IsActive = 1 "

                If courseID > 0 Then
                    query += " AND cr.CourseID = @CourseID "
                End If

                Select Case status
                    Case "pending"
                        query += " AND (cr.IsConfirmed = 0 OR cr.IsConfirmed IS NULL) "
                    Case "confirmed"
                        query += " AND cr.IsConfirmed = 1 "
                    Case "rejected"
                        query += " AND cr.IsConfirmed = 2 "
                End Select

                Select Case examStatus
                    Case "passed"
                        query += " AND er.IsPassed = 1 "
                    Case "failed"
                        query += " AND er.IsPassed = 0 AND er.ResultID IS NOT NULL "
                    Case "not_taken"
                        query += " AND er.ResultID IS NULL "
                End Select

                Select Case evalStatus
                    Case "done"
                        query += " AND cr.IsEvaluated = 1 "
                    Case "pending"
                        query += " AND (cr.IsEvaluated = 0 OR cr.IsEvaluated IS NULL) "
                End Select

                If Not String.IsNullOrEmpty(searchText) Then
                    query += " AND (p.nam LIKE @SearchText OR p.family LIKE @SearchText OR p.codePersoneli LIKE @SearchText) "
                End If

                query += " ORDER BY cr.RegistrationDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    If courseID > 0 Then
                        cmd.Parameters.AddWithValue("@CourseID", courseID)
                    End If
                    If Not String.IsNullOrEmpty(searchText) Then
                        cmd.Parameters.AddWithValue("@SearchText", "%" & searchText & "%")
                    End If
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            gvRegistrations.DataSource = dt
            gvRegistrations.DataBind()
            gvRegistrations.PageSize = PageSize

            pnlEmpty.Visible = (dt.Rows.Count = 0)
            UpdateStatistics()

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری ثبت‌نام‌ها: " & ex.Message, "danger")
            pnlEmpty.Visible = True
        End Try
    End Sub

    Private Sub UpdateStatistics()
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlCourse.SelectedValue)

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT " &
                           "COUNT(cr.RegistrationID) AS Total, " &
                           "SUM(CASE WHEN (cr.IsConfirmed = 0 OR cr.IsConfirmed IS NULL) THEN 1 ELSE 0 END) AS Pending, " &
                           "SUM(CASE WHEN cr.IsConfirmed = 1 THEN 1 ELSE 0 END) AS Confirmed, " &
                           "SUM(CASE WHEN cr.IsConfirmed = 2 THEN 1 ELSE 0 END) AS Rejected, " &
                           "SUM(CASE WHEN er.IsPassed = 1 THEN 1 ELSE 0 END) AS ExamPassed, " &
                           "SUM(CASE WHEN cr.IsEvaluated = 1 THEN 1 ELSE 0 END) AS Evaluated " &
                           "FROM trn_CourseRegistrations cr " &
                           "LEFT JOIN trn_ExamResults er ON cr.RegistrationID = er.RegistrationID " &
                           "WHERE cr.IsActive = 1 "

                If courseID > 0 Then
                    query += " AND cr.CourseID = @CourseID "
                End If

                Using cmd As New SqlCommand(query, conn)
                    If courseID > 0 Then
                        cmd.Parameters.AddWithValue("@CourseID", courseID)
                    End If
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            lblTotal.Text = reader("Total").ToString()
                            lblPending.Text = reader("Pending").ToString()
                            lblConfirmed.Text = reader("Confirmed").ToString()
                            lblRejected.Text = reader("Rejected").ToString()
                            lblExamPassed.Text = reader("ExamPassed").ToString()
                            lblEvaluated.Text = reader("Evaluated").ToString()
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' رویدادهای فیلترها
    ' ============================================================
    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvRegistrations.PageIndex = 0
        LoadRegistrations()
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvRegistrations.PageIndex = 0
        LoadRegistrations()
    End Sub

    Protected Sub ddlExamStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvRegistrations.PageIndex = 0
        LoadRegistrations()
    End Sub

    Protected Sub ddlEvalStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvRegistrations.PageIndex = 0
        LoadRegistrations()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        gvRegistrations.PageIndex = 0
        LoadRegistrations()
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        gvRegistrations.PageIndex = 0
        LoadRegistrations()
        ShowAlert("لیست بروزرسانی شد.", "info")
    End Sub

    ' ============================================================
    ' صفحه‌بندی GridView
    ' ============================================================
    Protected Sub gvRegistrations_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvRegistrations.PageIndex = e.NewPageIndex
        LoadRegistrations()
    End Sub

    Protected Sub gvRegistrations_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' می‌توانید پردازش‌های اضافی روی هر ردیف انجام دهید
        End If
    End Sub

    ' ============================================================
    ' رویدادهای GridView
    ' ============================================================
    Protected Sub gvRegistrations_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim registrationID As Integer = Convert.ToInt32(e.CommandArgument)

        Select Case e.CommandName
            Case "Confirm"
                ConfirmRegistration(registrationID)
            Case "Reject"
                RejectRegistration(registrationID)
            Case "ViewUser"
                Dim userID As Integer = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("fullinfo.aspx?ID=" & userID & "&source=registrations")
            Case "ViewDetails"
                Response.Redirect("ExamReport.aspx?RegID=" & registrationID)
        End Select
    End Sub

    ' ============================================================
    ' تایید و رد ثبت‌نام
    ' ============================================================
    Private Sub ConfirmRegistration(registrationID As Integer)
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "UPDATE trn_CourseRegistrations SET " &
                           "IsConfirmed = 1, ConfirmedDate = GETDATE(), ConfirmedBy = @ConfirmedBy " &
                           "WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    cmd.Parameters.AddWithValue("@ConfirmedBy", UserID)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowAlert("✅ ثبت‌نام با موفقیت تایید شد.", "success")
            LoadRegistrations()

        Catch ex As Exception
            ShowAlert("❌ خطا در تایید ثبت‌نام: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub RejectRegistration(registrationID As Integer)
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "UPDATE trn_CourseRegistrations SET " &
                           "IsConfirmed = 2, ConfirmedDate = GETDATE(), ConfirmedBy = @ConfirmedBy " &
                           "WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    cmd.Parameters.AddWithValue("@ConfirmedBy", UserID)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowAlert("✅ ثبت‌نام با موفقیت رد شد.", "success")
            LoadRegistrations()

        Catch ex As Exception
            ShowAlert("❌ خطا در رد ثبت‌نام: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' توابع کمکی
    ' ============================================================
    Public Function GetFullName(nam As Object, family As Object) As String
        Dim firstName As String = If(nam Is DBNull.Value, "", nam.ToString())
        Dim lastName As String = If(family Is DBNull.Value, "", family.ToString())
        Return (firstName & " " & lastName).Trim()
    End Function

    Public Function GetStatusClass(isConfirmed As Object) As String
        If isConfirmed Is DBNull.Value OrElse isConfirmed Is Nothing Then
            Return "pending"
        End If
        Dim status As Integer = Convert.ToInt32(isConfirmed)
        Select Case status
            Case 0 : Return "pending"
            Case 1 : Return "confirmed"
            Case 2 : Return "rejected"
            Case Else : Return "pending"
        End Select
    End Function

    Public Function GetStatusText(isConfirmed As Object) As String
        If isConfirmed Is DBNull.Value OrElse isConfirmed Is Nothing Then
            Return "در انتظار تایید"
        End If
        Dim status As Integer = Convert.ToInt32(isConfirmed)
        Select Case status
            Case 0 : Return "در انتظار تایید"
            Case 1 : Return "تایید شده"
            Case 2 : Return "رد شده"
            Case Else : Return "نامشخص"
        End Select
    End Function

    Public Function GetActionInfo(isConfirmed As Object, confirmedDate As Object) As String
        If isConfirmed Is DBNull.Value OrElse isConfirmed Is Nothing Then
            Return "نیاز به تایید"
        End If

        Dim status As Integer = Convert.ToInt32(isConfirmed)

        If status = 0 Then
            Return "نیاز به تایید"
        ElseIf status = 1 AndAlso Not IsDBNull(confirmedDate) Then
            Return "تایید در: " & Convert.ToDateTime(confirmedDate).ToString("yyyy/MM/dd HH:mm")
        ElseIf status = 2 AndAlso Not IsDBNull(confirmedDate) Then
            Return "رد در: " & Convert.ToDateTime(confirmedDate).ToString("yyyy/MM/dd HH:mm")
        Else
            Return ""
        End If
    End Function

    Public Function GetExamStatusText(examPassed As Object, examTaken As Object) As String
        If Convert.ToBoolean(examTaken) Then
            If Convert.ToBoolean(examPassed) Then
                Return "<span class='badge bg-success'>✅ قبول شده</span>"
            Else
                Return "<span class='badge bg-danger'>❌ رد شده</span>"
            End If
        Else
            Return "<span class='badge bg-warning'>⏳ شرکت نکرده</span>"
        End If
    End Function

    Private Sub ShowAlert(message As String, type As String)
        pnlAlert.Visible = True
        pnlAlert.Style.Add("display", "block")
        lblAlertMessage.Text = message

        Select Case type
            Case "success"
                pnlAlert.CssClass = "alert alert-success alert-dismissible fade show"
            Case "danger"
                pnlAlert.CssClass = "alert alert-danger alert-dismissible fade show"
            Case "warning"
                pnlAlert.CssClass = "alert alert-warning alert-dismissible fade show"
            Case Else
                pnlAlert.CssClass = "alert alert-info alert-dismissible fade show"
        End Select
    End Sub

End Class