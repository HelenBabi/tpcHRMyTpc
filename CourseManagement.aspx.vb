Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Partial Class CourseManagement
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("userid") Is Nothing Then
                Response.Redirect("login_2.aspx")
            End If

            txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
            txtEndDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd")

            LoadCourseDropdowns()
            ShowTab("list")

            ' پاک کردن Session مربوط به گزینه‌های آزمون در ابتدای کار
            Session.Remove("ExamOptions")
        End If
    End Sub

    ' ============================================================
    ' رویداد تغییر وضعیت چک‌باکس دارای آزمون
    ' ============================================================
    Protected Sub chkHasExam_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasExam.CheckedChanged
        If chkHasExam.Checked Then
            examSettings.Style.Add("display", "block")
        Else
            examSettings.Style.Add("display", "none")
        End If
        UpdatePanel1.Update()
    End Sub

    ' ============================================================
    ' بارگذاری لیست دوره‌ها در DropDownList ها
    ' ============================================================
    Private Sub LoadCourseDropdowns()
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

            Dim dropdowns As DropDownList() = {ddlFileCourse, ddlEvalCourse, ddlExamCourse}

            For Each ddl In dropdowns
                ddl.DataSource = dt
                ddl.DataTextField = "Title"
                ddl.DataValueField = "CourseID"
                ddl.DataBind()

                Dim defaultItem As New ListItem("-- انتخاب دوره --", "0")
                ddl.Items.Insert(0, defaultItem)
            Next

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری لیست دوره‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' مدیریت تب‌ها
    ' ============================================================
    Protected Sub tab_Click(sender As Object, e As EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim tabName As String = lb.CommandArgument
        ShowTab(tabName)
    End Sub

    Private Sub ShowTab(tabName As String)
        pnlList.Visible = False
        pnlNew.Visible = False
        pnlFiles.Visible = False
        pnlEvaluation.Visible = False
        pnlExam.Visible = False

        pnlList.Style.Add("display", "none")
        pnlNew.Style.Add("display", "none")
        pnlFiles.Style.Add("display", "none")
        pnlEvaluation.Style.Add("display", "none")
        pnlExam.Style.Add("display", "none")

        tabList.CssClass = "nav-link"
        tabNew.CssClass = "nav-link"
        tabFiles.CssClass = "nav-link"
        tabEvaluation.CssClass = "nav-link"
        tabExam.CssClass = "nav-link"

        Select Case tabName
            Case "list"
                pnlList.Visible = True
                pnlList.Style.Add("display", "block")
                tabList.CssClass = "nav-link active"
                LoadCourses()

            Case "new"
                pnlNew.Visible = True
                pnlNew.Style.Add("display", "block")
                tabNew.CssClass = "nav-link active"

                If hfCourseID.Value = "0" Then
                    btnSaveCourse.Text = "ذخیره دوره"
                    txtTitle.Text = ""
                    txtDescription.Text = ""
                    txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
                    txtEndDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd")
                    chkHasEvaluation.Checked = False
                    chkHasExam.Checked = False
                    txtExamStart.Text = ""
                    txtExamEnd.Text = ""
                    txtExamDuration.Text = ""
                    txtMinPassScore.Text = "60"
                    divSaveStatus.Visible = False
                    lblSaveStatus.Text = ""
                    examSettings.Style.Add("display", "none")
                End If

            Case "files"
                pnlFiles.Visible = True
                pnlFiles.Style.Add("display", "block")
                tabFiles.CssClass = "nav-link active"
                LoadFiles()

            Case "evaluation"
                pnlEvaluation.Visible = True
                pnlEvaluation.Style.Add("display", "block")
                tabEvaluation.CssClass = "nav-link active"
                LoadEvaluationQuestions()

            Case "exam"
                pnlExam.Visible = True
                pnlExam.Style.Add("display", "block")
                tabExam.CssClass = "nav-link active"
                LoadExamQuestions()
        End Select
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowAlert(message As String, type As String)
        pnlAlert.Visible = True
        pnlAlert.Style.Add("display", "block")
        lblAlertMessage.Text = message

        Select Case type
            Case "success"
                pnlAlert.CssClass = "alert alert-success alert-dismissible fade show"
            Case "danger"
                pnlAlert.CssClass = "alert alert-danger alert-dismissible fade show"
                lblAlertMessage.Font.Bold = True
            Case "warning"
                pnlAlert.CssClass = "alert alert-warning alert-dismissible fade show"
            Case Else
                pnlAlert.CssClass = "alert alert-info alert-dismissible fade show"
        End Select

        Dim script As String = "window.scrollTo(0, 0);"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "ScrollToTop", script, True)
    End Sub

    ' ============================================================
    ' بارگذاری لیست دوره‌ها
    ' ============================================================
    Private Sub LoadCourses()
        Try
            Dim dt As New DataTable()
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT ec.CourseID, ec.Title, ec.Description, " &
                           "dbo.ConvertGregorianToJalali(ec.StartDate) AS StartDate, " &
                           "dbo.ConvertGregorianToJalali(ec.EndDate) AS EndDate, " &
                           "ec.HasEvaluation, ec.HasExam, ec.ExamStartTime, ec.ExamEndTime, ec.ExamDuration, " &
                           "ec.MinPassScore, ec.IsActive, ec.CreatedDate, " &
                           "CASE " &
                           "  WHEN GETDATE() < ec.StartDate THEN N'آینده' " &
                           "  WHEN GETDATE() BETWEEN ec.StartDate AND ec.EndDate THEN N'در حال برگزاری' " &
                           "  ELSE N'به پایان رسیده' " &
                           "END AS StatusText, " &
                           "CASE " &
                           "  WHEN GETDATE() < ec.StartDate THEN 'status-pending' " &
                           "  WHEN GETDATE() BETWEEN ec.StartDate AND ec.EndDate THEN 'status-active' " &
                           "  ELSE 'status-inactive' " &
                           "END AS StatusClass " &
                           "FROM trn_EducationalCourses ec " &
                           "WHERE ec.IsActive = 1 " &
                           "ORDER BY ec.CreatedDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            If dt.Rows.Count > 0 Then
                gvCourses.DataSource = dt
                gvCourses.DataBind()
                lblRecordCount.Text = "تعداد کل: " & dt.Rows.Count & " دوره"
            Else
                gvCourses.DataSource = Nothing
                gvCourses.DataBind()
                lblRecordCount.Text = "هیچ دوره‌ای یافت نشد."
            End If

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری دوره‌ها: " & ex.Message, "danger")
            gvCourses.DataSource = Nothing
            gvCourses.DataBind()
            lblRecordCount.Text = "خطا در بارگذاری داده‌ها"
        End Try
    End Sub

    ' ============================================================
    ' جستجو
    ' ============================================================
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim searchText As String = txtSearch.Text.Trim()
            Dim dt As New DataTable()

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT ec.CourseID, ec.Title, ec.Description, " &
                           "dbo.ConvertGregorianToJalali(ec.StartDate) AS StartDate, " &
                           "dbo.ConvertGregorianToJalali(ec.EndDate) AS EndDate, " &
                           "ec.HasEvaluation, ec.HasExam, ec.ExamStartTime, ec.ExamEndTime, ec.ExamDuration, " &
                           "ec.MinPassScore, ec.IsActive, ec.CreatedDate, " &
                           "CASE " &
                           "  WHEN GETDATE() < ec.StartDate THEN 'آینده' " &
                           "  WHEN GETDATE() BETWEEN ec.StartDate AND ec.EndDate THEN 'در حال برگزاری' " &
                           "  ELSE 'به پایان رسیده' " &
                           "END AS StatusText, " &
                           "CASE " &
                           "  WHEN GETDATE() < ec.StartDate THEN 'status-pending' " &
                           "  WHEN GETDATE() BETWEEN ec.StartDate AND ec.EndDate THEN 'status-active' " &
                           "  ELSE 'status-inactive' " &
                           "END AS StatusClass " &
                           "FROM trn_EducationalCourses ec " &
                           "WHERE ec.IsActive = 1 "

                If Not String.IsNullOrEmpty(searchText) Then
                    query += " AND (ec.Title LIKE @SearchText OR ec.Description LIKE @SearchText) "
                End If

                query += " ORDER BY ec.CreatedDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(searchText) Then
                        cmd.Parameters.AddWithValue("@SearchText", "%" & searchText & "%")
                    End If
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            If dt.Rows.Count > 0 Then
                gvCourses.DataSource = dt
                gvCourses.DataBind()
                lblRecordCount.Text = "تعداد نتایج: " & dt.Rows.Count & " دوره"
            Else
                gvCourses.DataSource = Nothing
                gvCourses.DataBind()
                lblRecordCount.Text = "نتیجه‌ای یافت نشد."
            End If

        Catch ex As Exception
            ShowAlert("خطا در جستجو: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' رویدادهای GridView دوره‌ها
    ' ============================================================
    Protected Sub gvCourses_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCourses.RowCommand
        Dim courseID As Integer = Convert.ToInt32(e.CommandArgument)

        Select Case e.CommandName
            Case "EditCourse"
                EditCourse(courseID)
            Case "DeleteCourse"
                DeleteCourse(courseID)
            Case "SelectCourse"
                SelectCourse(courseID)
        End Select
    End Sub

    Private Sub SelectCourse(courseID As Integer)
        Try
            Dim courseTitle As String = ""
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT Title FROM trn_EducationalCourses WHERE CourseID = @CourseID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    courseTitle = cmd.ExecuteScalar().ToString()
                End Using
            End Using

            Dim dropdowns As DropDownList() = {ddlFileCourse, ddlEvalCourse, ddlExamCourse}
            For Each ddl In dropdowns
                ddl.SelectedValue = courseID.ToString()
            Next

            ShowAlert("✅ دوره '" & courseTitle & "' انتخاب شد.", "success")

            LoadFiles()
            LoadEvaluationQuestions()
            LoadExamQuestions()

        Catch ex As Exception
            ShowAlert("خطا در انتخاب دوره: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ذخیره دوره
    ' ============================================================
    Protected Sub btnSaveCourse_Click(sender As Object, e As EventArgs) Handles btnSaveCourse.Click
        Try
            If String.IsNullOrEmpty(txtTitle.Text.Trim()) Then
                ShowAlert("❌ عنوان دوره الزامی است.", "warning")
                Return
            End If

            If String.IsNullOrEmpty(txtStartDate.Text) OrElse String.IsNullOrEmpty(txtEndDate.Text) Then
                ShowAlert("❌ تاریخ شروع و پایان الزامی است.", "warning")
                Return
            End If

            Dim startDate = Convert.ToDateTime(txtStartDate.Text)
            Dim endDate = Convert.ToDateTime(txtEndDate.Text)

            If startDate > endDate Then
                ShowAlert("❌ تاریخ شروع نمی‌تواند از تاریخ پایان بزرگتر باشد.", "warning")
                Return
            End If

            Dim courseID As Integer = Convert.ToInt32(hfCourseID.Value)
            Dim isNew As Boolean = (courseID = 0)

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()

                If isNew Then
                    Dim query = "INSERT INTO trn_EducationalCourses " &
                               "(Title, Description, StartDate, EndDate, HasEvaluation, HasExam, " &
                               "ExamStartTime, ExamEndTime, ExamDuration, MinPassScore, CreatedBy) " &
                               "VALUES (@Title, @Description, @StartDate, @EndDate, @HasEvaluation, @HasExam, " &
                               "@ExamStartTime, @ExamEndTime, @ExamDuration, @MinPassScore, @CreatedBy); " &
                               "SELECT SCOPE_IDENTITY()"

                    Using cmd As New SqlCommand(query, conn)
                        AddCourseParameters(cmd)
                        courseID = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    ShowAlert("✅ دوره با موفقیت ایجاد شد. کد: " & courseID, "success")
                    hfCourseID.Value = courseID.ToString()
                    btnSaveCourse.Text = "ویرایش دوره"

                    divSaveStatus.Visible = True
                    lblSaveStatus.Text = "✅ دوره با کد " & courseID & " با موفقیت ذخیره شد."

                    LoadCourseDropdowns()
                    LoadCourses()

                Else
                    Dim query = "UPDATE trn_EducationalCourses SET " &
                               "Title = @Title, Description = @Description, StartDate = @StartDate, " &
                               "EndDate = @EndDate, HasEvaluation = @HasEvaluation, HasExam = @HasExam, " &
                               "ExamStartTime = @ExamStartTime, ExamEndTime = @ExamEndTime, " &
                               "ExamDuration = @ExamDuration, MinPassScore = @MinPassScore, " &
                               "ModifiedDate = GETDATE(), ModifiedBy = @ModifiedBy " &
                               "WHERE CourseID = @CourseID"

                    Using cmd As New SqlCommand(query, conn)
                        AddCourseParameters(cmd)
                        cmd.Parameters.AddWithValue("@CourseID", courseID)
                        cmd.Parameters.AddWithValue("@ModifiedBy", Session("userid"))
                        cmd.ExecuteNonQuery()
                    End Using

                    ShowAlert("✅ دوره با موفقیت ویرایش شد.", "success")
                    divSaveStatus.Visible = True
                    lblSaveStatus.Text = "✅ دوره با کد " & courseID & " با موفقیت ویرایش شد."
                    LoadCourses()
                    LoadCourseDropdowns()
                End If
            End Using

        Catch ex As Exception
            ShowAlert("❌ خطا در ذخیره دوره: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub AddCourseParameters(cmd As SqlCommand)
        cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim())

        If String.IsNullOrEmpty(txtDescription.Text) Then
            cmd.Parameters.AddWithValue("@Description", DBNull.Value)
        Else
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text)
        End If

        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text))
        cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtEndDate.Text))
        cmd.Parameters.AddWithValue("@HasEvaluation", chkHasEvaluation.Checked)
        cmd.Parameters.AddWithValue("@HasExam", chkHasExam.Checked)

        If String.IsNullOrEmpty(txtExamStart.Text) Then
            cmd.Parameters.AddWithValue("@ExamStartTime", DBNull.Value)
        Else
            cmd.Parameters.AddWithValue("@ExamStartTime", Convert.ToDateTime(txtExamStart.Text))
        End If

        If String.IsNullOrEmpty(txtExamEnd.Text) Then
            cmd.Parameters.AddWithValue("@ExamEndTime", DBNull.Value)
        Else
            cmd.Parameters.AddWithValue("@ExamEndTime", Convert.ToDateTime(txtExamEnd.Text))
        End If

        If String.IsNullOrEmpty(txtExamDuration.Text) Then
            cmd.Parameters.AddWithValue("@ExamDuration", DBNull.Value)
        Else
            cmd.Parameters.AddWithValue("@ExamDuration", Convert.ToInt32(txtExamDuration.Text))
        End If

        cmd.Parameters.AddWithValue("@MinPassScore", Convert.ToInt32(txtMinPassScore.Text))
        cmd.Parameters.AddWithValue("@CreatedBy", Session("userid"))
    End Sub

    Protected Sub btnResetCourse_Click(sender As Object, e As EventArgs) Handles btnResetCourse.Click
        hfCourseID.Value = "0"
        txtTitle.Text = ""
        txtDescription.Text = ""
        txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
        txtEndDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd")
        chkHasEvaluation.Checked = False
        chkHasExam.Checked = False
        txtExamStart.Text = ""
        txtExamEnd.Text = ""
        txtExamDuration.Text = ""
        txtMinPassScore.Text = "60"
        btnSaveCourse.Text = "ذخیره دوره"
        divSaveStatus.Visible = False
        lblSaveStatus.Text = ""
        examSettings.Style.Add("display", "none")
        ShowAlert("فرم بازنشانی شد.", "info")
    End Sub

    Private Sub EditCourse(courseID As Integer)
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT *, " &
                           "dbo.ConvertGregorianToJalali(StartDate) AS PersianStartDate, " &
                           "dbo.ConvertGregorianToJalali(EndDate) AS PersianEndDate " &
                           "FROM trn_EducationalCourses WHERE CourseID = @CourseID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            hfCourseID.Value = courseID.ToString()
                            txtTitle.Text = reader("Title").ToString()
                            txtDescription.Text = reader("Description").ToString()
                            txtStartDate.Text = Convert.ToDateTime(reader("StartDate")).ToString("yyyy-MM-dd")
                            txtEndDate.Text = Convert.ToDateTime(reader("EndDate")).ToString("yyyy-MM-dd")
                            chkHasEvaluation.Checked = Convert.ToBoolean(reader("HasEvaluation"))
                            chkHasExam.Checked = Convert.ToBoolean(reader("HasExam"))

                            If Not IsDBNull(reader("ExamStartTime")) Then
                                txtExamStart.Text = Convert.ToDateTime(reader("ExamStartTime")).ToString("yyyy-MM-ddTHH:mm")
                            End If

                            If Not IsDBNull(reader("ExamEndTime")) Then
                                txtExamEnd.Text = Convert.ToDateTime(reader("ExamEndTime")).ToString("yyyy-MM-ddTHH:mm")
                            End If

                            txtExamDuration.Text = If(IsDBNull(reader("ExamDuration")), "", reader("ExamDuration").ToString())
                            txtMinPassScore.Text = reader("MinPassScore").ToString()
                            btnSaveCourse.Text = "ویرایش دوره"
                            divSaveStatus.Visible = False
                            lblSaveStatus.Text = ""

                            If chkHasExam.Checked Then
                                examSettings.Style.Add("display", "block")
                            Else
                                examSettings.Style.Add("display", "none")
                            End If

                            ShowTab("new")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowAlert("خطا در ویرایش دوره: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub DeleteCourse(courseID As Integer)
        Try
            Dim errorMessages As New List(Of String)()
            Dim hasDependency As Boolean = False

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()

                ' 1. بررسی ثبت‌نام‌ها
                Dim checkQuery = "SELECT COUNT(*) FROM trn_CourseRegistrations WHERE CourseID = @CourseID AND IsActive = 1"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        errorMessages.Add("📝 " & count & " ثبت‌نام فعال")
                        hasDependency = True
                    End If
                End Using

                ' 2. بررسی محتوای آموزشی
                checkQuery = "SELECT COUNT(*) FROM trn_CourseContents WHERE CourseID = @CourseID AND IsActive = 1"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        errorMessages.Add("📁 " & count & " فایل محتوایی")
                        hasDependency = True
                    End If
                End Using

                ' 3. بررسی سوالات ارزیابی
                checkQuery = "SELECT COUNT(*) FROM trn_EvaluationQuestions WHERE CourseID = @CourseID AND IsActive = 1"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        errorMessages.Add("❓ " & count & " سوال ارزیابی")
                        hasDependency = True
                    End If
                End Using

                ' 4. بررسی سوالات آزمون
                checkQuery = "SELECT COUNT(*) FROM trn_ExamQuestions WHERE CourseID = @CourseID AND IsActive = 1"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        errorMessages.Add("📝 " & count & " سوال آزمون")
                        hasDependency = True
                    End If
                End Using

                ' 5. بررسی نتایج آزمون
                checkQuery = "SELECT COUNT(*) FROM trn_ExamResults er " &
                            "INNER JOIN trn_CourseRegistrations cr ON er.RegistrationID = cr.RegistrationID " &
                            "WHERE cr.CourseID = @CourseID"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        errorMessages.Add("📊 " & count & " نتیجه آزمون")
                        hasDependency = True
                    End If
                End Using

                ' 6. بررسی نتایج ارزیابی
                checkQuery = "SELECT COUNT(*) FROM trn_EvaluationResults er " &
                            "INNER JOIN trn_CourseRegistrations cr ON er.RegistrationID = cr.RegistrationID " &
                            "WHERE cr.CourseID = @CourseID"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        errorMessages.Add("⭐ " & count & " نتیجه ارزیابی")
                        hasDependency = True
                    End If
                End Using

            End Using

            If hasDependency Then
                Dim msg As String = "❌ این دوره دارای وابستگی‌های زیر است:<br/>"
                msg &= String.Join("<br/>", errorMessages)
                msg &= "<br/><br/>لطفاً ابتدا موارد فوق را حذف کنید."
                ShowAlert(msg, "danger")
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "UPDATE trn_EducationalCourses SET IsActive = 0 WHERE CourseID = @CourseID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowAlert("✅ دوره با موفقیت حذف شد.", "success")
            LoadCourses()
            LoadCourseDropdowns()

        Catch ex As Exception
            ShowAlert("❌ خطا در حذف دوره: " & ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gvCourses_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCourses.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btnDelete As LinkButton = e.Row.FindControl("btnDelete")
            If btnDelete IsNot Nothing Then
                btnDelete.OnClientClick = "return confirm('آیا از حذف این دوره اطمینان دارید؟');"
            End If
        End If
    End Sub

    ' ============================================================
    ' مدیریت فایل‌ها
    ' ============================================================
    Private Sub LoadFiles()
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlFileCourse.SelectedValue)

            If courseID = 0 Then
                rptFiles.DataSource = Nothing
                rptFiles.DataBind()
                lblFilesMessage.Text = "لطفاً یک دوره را انتخاب کنید."
                lblSelectedFileCourse.Text = "هیچ دوره‌ای انتخاب نشده است."
                Return
            End If

            Dim courseTitle As String = ddlFileCourse.SelectedItem.Text
            lblSelectedFileCourse.Text = "دوره انتخاب شده: " & courseTitle

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT * FROM trn_CourseFiles WHERE CourseID = @CourseID AND IsActive = 1 ORDER BY UploadDate DESC"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            rptFiles.DataSource = dt
                            rptFiles.DataBind()
                            lblFilesMessage.Text = "تعداد فایل‌ها: " & dt.Rows.Count
                        Else
                            rptFiles.DataSource = Nothing
                            rptFiles.DataBind()
                            lblFilesMessage.Text = "هیچ فایلی برای این دوره بارگذاری نشده است."
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowAlert("خطا در بارگذاری فایل‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    Protected Sub ddlFileCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFileCourse.SelectedIndexChanged
        LoadFiles()
    End Sub

    Protected Sub btnRefreshFiles_Click(sender As Object, e As EventArgs) Handles btnRefreshFiles.Click
        LoadFiles()
        ShowAlert("لیست فایل‌ها بروزرسانی شد.", "info")
    End Sub

    Protected Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlFileCourse.SelectedValue)

            If courseID = 0 Then
                ShowAlert("❌ لطفاً ابتدا یک دوره را انتخاب کنید.", "warning")
                Return
            End If

            If Not fuCourseFile.HasFile Then
                ShowAlert("❌ لطفاً یک فایل انتخاب کنید.", "warning")
                Return
            End If

            If fuCourseFile.PostedFile.ContentLength > 52428800 Then
                ShowAlert("❌ حجم فایل بیش از حد مجاز است (حداکثر 50 مگابایت).", "warning")
                Return
            End If

            Dim fileName = Path.GetFileName(fuCourseFile.FileName)
            Dim fileExtension = Path.GetExtension(fileName).ToLower()
            Dim fileSize As Integer = fuCourseFile.PostedFile.ContentLength

            Dim contentType As String = GetContentType(fileExtension)

            Dim uploadFolder As String = "~/Uploads/Courses/" & courseID & "/"
            Dim physicalPath As String = Server.MapPath(uploadFolder)

            If Not Directory.Exists(physicalPath) Then
                Directory.CreateDirectory(physicalPath)
            End If

            Dim uniqueFileName As String = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") & "_" & fileName
            Dim fullPath As String = Path.Combine(physicalPath, uniqueFileName)
            Dim dbPath As String = uploadFolder & uniqueFileName

            fuCourseFile.SaveAs(fullPath)

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        Dim query1 = "INSERT INTO trn_CourseFiles (CourseID, FileName, FilePath, FileType, FileSize, UploadedBy, UploadDate) " &
                                   "VALUES (@CourseID, @FileName, @FilePath, @FileType, @FileSize, @UploadedBy, GETDATE())"
                        Using cmd As New SqlCommand(query1, conn, trans)
                            cmd.Parameters.AddWithValue("@CourseID", courseID)
                            cmd.Parameters.AddWithValue("@FileName", fileName)
                            cmd.Parameters.AddWithValue("@FilePath", dbPath)
                            cmd.Parameters.AddWithValue("@FileType", fileExtension.TrimStart("."))
                            cmd.Parameters.AddWithValue("@FileSize", fileSize)
                            cmd.Parameters.AddWithValue("@UploadedBy", Session("userid"))
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim query2 = "INSERT INTO trn_CourseContents (CourseID, Title, Description, ContentType, FileName, FilePath, [Order], IsActive, CreatedDate) " &
                                   "VALUES (@CourseID, @Title, @Description, @ContentType, @FileName, @FilePath, @Order, 1, GETDATE())"
                        Using cmd As New SqlCommand(query2, conn, trans)
                            cmd.Parameters.AddWithValue("@CourseID", courseID)
                            cmd.Parameters.AddWithValue("@Title", Path.GetFileNameWithoutExtension(fileName))
                            cmd.Parameters.AddWithValue("@Description", "فایل آموزشی دوره")
                            cmd.Parameters.AddWithValue("@ContentType", contentType)
                            cmd.Parameters.AddWithValue("@FileName", uniqueFileName)
                            cmd.Parameters.AddWithValue("@FilePath", dbPath)
                            cmd.Parameters.AddWithValue("@Order", GetNextOrder(courseID))
                            cmd.ExecuteNonQuery()
                        End Using

                        trans.Commit()

                        ShowAlert("✅ فایل '" & fileName & "' با موفقیت بارگذاری شد.", "success")
                        LoadFiles()

                    Catch ex As Exception
                        trans.Rollback()
                        If File.Exists(fullPath) Then
                            File.Delete(fullPath)
                        End If
                        Throw ex
                    End Try
                End Using
            End Using

        Catch ex As Exception
            ShowAlert("❌ خطا در بارگذاری فایل: " & ex.Message, "danger")
        End Try
    End Sub

    Private Function GetContentType(extension As String) As String
        Select Case extension.ToLower()
            Case ".pdf" : Return "PDF"
            Case ".ppt", ".pptx" : Return "PPT"
            Case ".mp4", ".avi", ".mkv", ".wmv", ".flv", ".mov" : Return "Video"
            Case ".doc", ".docx", ".xls", ".xlsx", ".txt", ".zip", ".rar" : Return "File"
            Case ".jpg", ".jpeg", ".png", ".gif", ".bmp" : Return "File"
            Case Else : Return "File"
        End Select
    End Function

    Private Function GetNextOrder(courseID As Integer) As Integer
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT ISNULL(MAX([Order]), 0) + 1 FROM trn_CourseContents WHERE CourseID = @CourseID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch
            Return 1
        End Try
    End Function

    Protected Sub rptFiles_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptFiles.ItemCommand
        If e.CommandName = "DeleteFile" Then
            Try
                Dim fileID As Integer = Convert.ToInt32(e.CommandArgument)

                Dim filePath As String = ""
                Using conn As New SqlConnection(ConnectionString)
                    Dim query = "SELECT FilePath FROM trn_CourseFiles WHERE FileID = @FileID"
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@FileID", fileID)
                        conn.Open()
                        filePath = cmd.ExecuteScalar().ToString()
                    End Using
                End Using

                If Not String.IsNullOrEmpty(filePath) Then
                    Dim physicalPath As String = Server.MapPath(filePath)
                    If File.Exists(physicalPath) Then
                        File.Delete(physicalPath)
                    End If
                End If

                Using conn As New SqlConnection(ConnectionString)
                    Dim query = "DELETE FROM trn_CourseFiles WHERE FileID = @FileID"
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@FileID", fileID)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ShowAlert("✅ فایل با موفقیت حذف شد.", "success")
                LoadFiles()

            Catch ex As Exception
                ShowAlert("❌ خطا در حذف فایل: " & ex.Message, "danger")
            End Try
        End If
    End Sub

    Public Function FormatFileSize(fileSize As Object) As String
        If fileSize Is DBNull.Value Then Return "نامشخص"
        Dim size As Long = Convert.ToInt64(fileSize)
        If size < 1024 Then Return size & " B"
        If size < 1048576 Then Return (size / 1024).ToString("F1") & " KB"
        If size < 1073741824 Then Return (size / 1048576).ToString("F1") & " MB"
        Return (size / 1073741824).ToString("F1") & " GB"
    End Function

    ' ============================================================
    ' مدیریت سوالات ارزیابی
    ' ============================================================
    Private Sub LoadEvaluationQuestions()
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlEvalCourse.SelectedValue)

            If courseID = 0 Then
                rptEvaluationQuestions.DataSource = Nothing
                rptEvaluationQuestions.DataBind()
                lblEvalMessage.Text = "لطفاً یک دوره را انتخاب کنید."
                lblSelectedEvalCourse.Text = "هیچ دوره‌ای انتخاب نشده است."
                Return
            End If

            Dim courseTitle As String = ddlEvalCourse.SelectedItem.Text
            lblSelectedEvalCourse.Text = "دوره انتخاب شده: " & courseTitle

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT q.*, " &
                           "STUFF((SELECT ', ' + OptionText + CASE WHEN IsCorrect = 1 THEN ' (صحیح)' ELSE '' END " &
                           "FROM trn_EvaluationOptions WHERE QuestionID = q.QuestionID FOR XML PATH('')), 1, 2, '') AS Options " &
                           "FROM trn_EvaluationQuestions q " &
                           "WHERE q.CourseID = @CourseID AND q.IsActive = 1 " &
                           "ORDER BY q.[Order]"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            rptEvaluationQuestions.DataSource = dt
                            rptEvaluationQuestions.DataBind()
                            lblEvalMessage.Text = "تعداد سوالات ارزیابی: " & dt.Rows.Count
                        Else
                            rptEvaluationQuestions.DataSource = Nothing
                            rptEvaluationQuestions.DataBind()
                            lblEvalMessage.Text = "هیچ سوال ارزیابی برای این دوره تعریف نشده است."
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowAlert("خطا در بارگذاری سوالات ارزیابی: " & ex.Message, "danger")
        End Try
    End Sub

    Protected Sub ddlEvalCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEvalCourse.SelectedIndexChanged
        LoadEvaluationQuestions()
    End Sub

    Protected Sub btnRefreshEval_Click(sender As Object, e As EventArgs) Handles btnRefreshEval.Click
        LoadEvaluationQuestions()
        ShowAlert("لیست سوالات ارزیابی بروزرسانی شد.", "info")
    End Sub

    Protected Sub btnAddEvalQuestion_Click(sender As Object, e As EventArgs) Handles btnAddEvalQuestion.Click
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlEvalCourse.SelectedValue)

            If courseID = 0 Then
                ShowAlert("❌ لطفاً ابتدا یک دوره را انتخاب کنید.", "warning")
                Return
            End If

            If String.IsNullOrEmpty(txtEvalQuestion.Text.Trim()) Then
                ShowAlert("❌ متن سوال الزامی است.", "warning")
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        Dim query = "INSERT INTO trn_EvaluationQuestions (CourseID, QuestionText, QuestionType, IsRequired) " &
                                   "VALUES (@CourseID, @QuestionText, @QuestionType, @IsRequired); SELECT SCOPE_IDENTITY()"

                        Dim questionID As Integer
                        Using cmd As New SqlCommand(query, conn, trans)
                            cmd.Parameters.AddWithValue("@CourseID", courseID)
                            cmd.Parameters.AddWithValue("@QuestionText", txtEvalQuestion.Text.Trim())
                            cmd.Parameters.AddWithValue("@QuestionType", ddlEvalType.SelectedValue)
                            cmd.Parameters.AddWithValue("@IsRequired", chkEvalRequired.Checked)
                            questionID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        If ddlEvalType.SelectedValue = "MultipleChoice" AndAlso Not String.IsNullOrEmpty(txtEvalOptions.Text) Then
                            Dim options As String() = txtEvalOptions.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                            For i As Integer = 0 To options.Length - 1
                                Dim optQuery = "INSERT INTO trn_EvaluationOptions (QuestionID, OptionText, [Order]) " &
                                              "VALUES (@QuestionID, @OptionText, @Order)"
                                Using optCmd As New SqlCommand(optQuery, conn, trans)
                                    optCmd.Parameters.AddWithValue("@QuestionID", questionID)
                                    optCmd.Parameters.AddWithValue("@OptionText", options(i).Trim())
                                    optCmd.Parameters.AddWithValue("@Order", i + 1)
                                    optCmd.ExecuteNonQuery()
                                End Using
                            Next
                        End If

                        trans.Commit()
                        ShowAlert("✅ سوال ارزیابی با موفقیت افزوده شد.", "success")
                        txtEvalQuestion.Text = ""
                        txtEvalOptions.Text = ""
                        LoadEvaluationQuestions()

                    Catch ex As Exception
                        trans.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ShowAlert("❌ خطا در افزودن سوال: " & ex.Message, "danger")
        End Try
    End Sub

    ' حذف سوال ارزیابی
    Protected Sub rptEvaluationQuestions_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptEvaluationQuestions.ItemCommand
        If e.CommandName = "DeleteEvalQuestion" Then
            Try
                Dim questionID As Integer = Convert.ToInt32(e.CommandArgument)

                Using conn As New SqlConnection(ConnectionString)
                    conn.Open()
                    Using trans = conn.BeginTransaction()
                        Try
                            ' حذف گزینه‌های سوال
                            Dim optQuery = "DELETE FROM trn_EvaluationOptions WHERE QuestionID = @QuestionID"
                            Using cmd As New SqlCommand(optQuery, conn, trans)
                                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                                cmd.ExecuteNonQuery()
                            End Using

                            ' غیرفعال کردن سوال
                            Dim query = "UPDATE trn_EvaluationQuestions SET IsActive = 0 WHERE QuestionID = @QuestionID"
                            Using cmd As New SqlCommand(query, conn, trans)
                                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                                cmd.ExecuteNonQuery()
                            End Using

                            trans.Commit()
                            ShowAlert("✅ سوال ارزیابی با موفقیت حذف شد.", "success")
                            LoadEvaluationQuestions()

                        Catch ex As Exception
                            trans.Rollback()
                            Throw ex
                        End Try
                    End Using
                End Using

            Catch ex As Exception
                ShowAlert("❌ خطا در حذف سوال: " & ex.Message, "danger")
            End Try
        End If
    End Sub

    ' ============================================================
    ' مدیریت سوالات آزمون با استفاده از Session و GridView
    ' ============================================================

    ' ایجاد جدول موقت برای ذخیره گزینه‌ها در Session
    Private Function CreateOptionTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("RowNo", GetType(Integer))
        dt.Columns.Add("OptionText", GetType(String))
        dt.Columns.Add("IsCorrect", GetType(Boolean))
        Return dt
    End Function

    ' بارگذاری گزینه‌ها از Session
    Private Sub LoadOptionsFromSession()
        Dim dt As DataTable = Session("ExamOptions")
        If dt IsNot Nothing Then
            gvOptions.DataSource = dt
            gvOptions.DataBind()
        Else
            gvOptions.DataSource = Nothing
            gvOptions.DataBind()
        End If
    End Sub

    Private Sub LoadExamQuestions()
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlExamCourse.SelectedValue)

            If courseID = 0 Then
                rptExamQuestions.DataSource = Nothing
                rptExamQuestions.DataBind()
                lblExamMessage.Text = "لطفاً یک دوره را انتخاب کنید."
                lblSelectedExamCourse.Text = "هیچ دوره‌ای انتخاب نشده است."
                Return
            End If

            Dim courseTitle As String = ddlExamCourse.SelectedItem.Text
            lblSelectedExamCourse.Text = "دوره انتخاب شده: " & courseTitle

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT q.*, " &
                           "STUFF((SELECT ', ' + OptionText + CASE WHEN IsCorrect = 1 THEN ' (صحیح)' ELSE '' END " &
                           "FROM trn_ExamOptions WHERE QuestionID = q.QuestionID FOR XML PATH('')), 1, 2, '') AS Options " &
                           "FROM trn_ExamQuestions q " &
                           "WHERE q.CourseID = @CourseID AND q.IsActive = 1 " &
                           "ORDER BY q.[Order]"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            rptExamQuestions.DataSource = dt
                            rptExamQuestions.DataBind()
                            lblExamMessage.Text = "تعداد سوالات آزمون: " & dt.Rows.Count
                        Else
                            rptExamQuestions.DataSource = Nothing
                            rptExamQuestions.DataBind()
                            lblExamMessage.Text = "هیچ سوال آزمون برای این دوره تعریف نشده است."
                        End If
                    End Using
                End Using
            End Using

            ' بارگذاری گزینه‌های Session در GridView
            LoadOptionsFromSession()

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری سوالات آزمون: " & ex.Message, "danger")
        End Try
    End Sub

    Protected Sub ddlExamCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamCourse.SelectedIndexChanged
        ' پاک کردن Session هنگام تغییر دوره
        Session.Remove("ExamOptions")
        LoadExamQuestions()
    End Sub

    Protected Sub btnRefreshExam_Click(sender As Object, e As EventArgs) Handles btnRefreshExam.Click
        LoadExamQuestions()
        ShowAlert("لیست سوالات آزمون بروزرسانی شد.", "info")
    End Sub

    ' افزودن گزینه جدید
    Protected Sub btnAddOption_Click(sender As Object, e As EventArgs) Handles btnAddOption.Click
        Try
            If String.IsNullOrEmpty(txtOptionText.Text.Trim()) Then
                ShowAlert("❌ متن گزینه را وارد کنید.", "warning")
                Return
            End If

            Dim dt As DataTable = Session("ExamOptions")

            If dt Is Nothing Then
                dt = CreateOptionTable()
            End If

            ' بررسی تکراری نبودن گزینه
            For Each row As DataRow In dt.Rows
                If row("OptionText").ToString().Trim() = txtOptionText.Text.Trim() Then
                    ShowAlert("❌ این گزینه قبلاً اضافه شده است.", "warning")
                    Return
                End If
            Next

            Dim newRow = dt.NewRow()
            newRow("RowNo") = dt.Rows.Count + 1
            newRow("OptionText") = txtOptionText.Text.Trim()
            newRow("IsCorrect") = chkCorrectOption.Checked

            dt.Rows.Add(newRow)
            Session("ExamOptions") = dt

            ' بارگذاری مجدد GridView
            gvOptions.DataSource = dt
            gvOptions.DataBind()

            ' پاک کردن فیلدها
            txtOptionText.Text = ""
            chkCorrectOption.Checked = False

            ShowAlert("✅ گزینه با موفقیت اضافه شد.", "success")

        Catch ex As Exception
            ShowAlert("❌ خطا در افزودن گزینه: " & ex.Message, "danger")
        End Try
    End Sub

    ' حذف گزینه از Session
    Protected Sub gvOptions_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvOptions.RowCommand
        If e.CommandName = "DeleteOption" Then
            Try
                Dim rowNo As Integer = Convert.ToInt32(e.CommandArgument)
                Dim dt As DataTable = Session("ExamOptions")

                If dt IsNot Nothing Then
                    For Each row As DataRow In dt.Rows
                        If Convert.ToInt32(row("RowNo")) = rowNo Then
                            row.Delete()
                            Exit For
                        End If
                    Next

                    dt.AcceptChanges()

                    ' بازسازی شماره ردیف‌ها
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("RowNo") = i + 1
                    Next

                    Session("ExamOptions") = dt

                    gvOptions.DataSource = dt
                    gvOptions.DataBind()

                    ShowAlert("✅ گزینه با موفقیت حذف شد.", "success")
                End If

            Catch ex As Exception
                ShowAlert("❌ خطا در حذف گزینه: " & ex.Message, "danger")
            End Try
        End If
    End Sub

    ' پاک کردن تمام گزینه‌ها
    Protected Sub btnClearOptions_Click(sender As Object, e As EventArgs) Handles btnClearOptions.Click
        Session.Remove("ExamOptions")
        gvOptions.DataSource = Nothing
        gvOptions.DataBind()
        ShowAlert("✅ تمام گزینه‌ها پاک شدند.", "info")
    End Sub

    ' افزودن سوال آزمون با گزینه‌های ذخیره شده در Session
    Protected Sub btnAddExamQuestion_Click(sender As Object, e As EventArgs) Handles btnAddExamQuestion.Click
        Try
            Dim courseID As Integer = Convert.ToInt32(ddlExamCourse.SelectedValue)

            If courseID = 0 Then
                ShowAlert("❌ لطفاً ابتدا یک دوره را انتخاب کنید.", "warning")
                Return
            End If

            If String.IsNullOrEmpty(txtExamQuestion.Text.Trim()) Then
                ShowAlert("❌ متن سوال الزامی است.", "warning")
                Return
            End If

            ' بررسی وجود گزینه‌ها
            Dim dtOptions As DataTable = Session("ExamOptions")
            If dtOptions Is Nothing OrElse dtOptions.Rows.Count = 0 Then
                ShowAlert("❌ لطفاً حداقل یک گزینه برای سوال وارد کنید.", "warning")
                Return
            End If

            ' بررسی وجود حداقل یک گزینه صحیح
            Dim hasCorrect As Boolean = False
            For Each row As DataRow In dtOptions.Rows
                If Convert.ToBoolean(row("IsCorrect")) Then
                    hasCorrect = True
                    Exit For
                End If
            Next

            If Not hasCorrect Then
                ShowAlert("❌ لطفاً حداقل یک گزینه را به عنوان پاسخ صحیح انتخاب کنید.", "warning")
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        ' 1. درج سوال
                        Dim query = "INSERT INTO trn_ExamQuestions (CourseID, QuestionText, QuestionType, Score) " &
                                   "VALUES (@CourseID, @QuestionText, @QuestionType, @Score); SELECT SCOPE_IDENTITY()"

                        Dim questionID As Integer
                        Using cmd As New SqlCommand(query, conn, trans)
                            cmd.Parameters.AddWithValue("@CourseID", courseID)
                            cmd.Parameters.AddWithValue("@QuestionText", txtExamQuestion.Text.Trim())
                            cmd.Parameters.AddWithValue("@QuestionType", ddlExamType.SelectedValue)
                            cmd.Parameters.AddWithValue("@Score", Convert.ToInt32(txtExamScore.Text))
                            questionID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' 2. درج گزینه‌ها از Session
                        For Each row As DataRow In dtOptions.Rows
                            Dim optQuery = "INSERT INTO trn_ExamOptions (QuestionID, OptionText, [Order], IsCorrect) " &
                                          "VALUES (@QuestionID, @OptionText, @Order, @IsCorrect)"
                            Using optCmd As New SqlCommand(optQuery, conn, trans)
                                optCmd.Parameters.AddWithValue("@QuestionID", questionID)
                                optCmd.Parameters.AddWithValue("@OptionText", row("OptionText").ToString())
                                optCmd.Parameters.AddWithValue("@Order", Convert.ToInt32(row("RowNo")))
                                optCmd.Parameters.AddWithValue("@IsCorrect", Convert.ToBoolean(row("IsCorrect")))
                                optCmd.ExecuteNonQuery()
                            End Using
                        Next

                        trans.Commit()

                        ' پاک کردن Session و فرم
                        Session.Remove("ExamOptions")
                        gvOptions.DataSource = Nothing
                        gvOptions.DataBind()

                        txtExamQuestion.Text = ""
                        txtExamScore.Text = "1"
                        txtOptionText.Text = ""
                        chkCorrectOption.Checked = False

                        ShowAlert("✅ سوال آزمون با موفقیت افزوده شد.", "success")
                        LoadExamQuestions()

                    Catch ex As Exception
                        trans.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

        Catch ex As Exception
            ShowAlert("❌ خطا در افزودن سوال: " & ex.Message, "danger")
        End Try
    End Sub

    ' حذف سوال آزمون
    Protected Sub rptExamQuestions_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptExamQuestions.ItemCommand
        If e.CommandName = "DeleteExamQuestion" Then
            Try
                Dim questionID As Integer = Convert.ToInt32(e.CommandArgument)

                Using conn As New SqlConnection(ConnectionString)
                    conn.Open()
                    Using trans = conn.BeginTransaction()
                        Try
                            ' حذف گزینه‌های سوال
                            Dim optQuery = "DELETE FROM trn_ExamOptions WHERE QuestionID = @QuestionID"
                            Using cmd As New SqlCommand(optQuery, conn, trans)
                                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                                cmd.ExecuteNonQuery()
                            End Using

                            ' غیرفعال کردن سوال
                            Dim query = "UPDATE trn_ExamQuestions SET IsActive = 0 WHERE QuestionID = @QuestionID"
                            Using cmd As New SqlCommand(query, conn, trans)
                                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                                cmd.ExecuteNonQuery()
                            End Using

                            trans.Commit()
                            ShowAlert("✅ سوال آزمون با موفقیت حذف شد.", "success")
                            LoadExamQuestions()

                        Catch ex As Exception
                            trans.Rollback()
                            Throw ex
                        End Try
                    End Using
                End Using

            Catch ex As Exception
                ShowAlert("❌ خطا در حذف سوال: " & ex.Message, "danger")
            End Try
        End If
    End Sub

End Class