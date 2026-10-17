Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class MyCourses
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn
    Private UserID As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If

        UserID = Convert.ToInt32(Session("userid"))

        If Not IsPostBack Then
            LoadMyCourses()
        End If
    End Sub

    ' ============================================================
    ' بارگذاری دوره‌ها
    ' ============================================================
    Private Sub LoadMyCourses()
        Try
            Dim status As String = ddlStatus.SelectedValue
            Dim searchText As String = txtSearch.Text.Trim()

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT ec.CourseID, ec.Title AS CourseTitle, " &
                           "dbo.ConvertGregorianToJalali(ec.StartDate) AS StartDate, " &
                           "dbo.ConvertGregorianToJalali(ec.EndDate) AS EndDate, " &
                           "ec.Description, ec.IsActive, " &
                           "ISNULL(cr.RegistrationID, 0) AS RegistrationID, " &
                           "ISNULL(cr.UserID, 0) AS UserID, " &
                           "dbo.ConvertGregorianToJalali(cr.RegistrationDate) AS RegistrationDate, " &
                           "ISNULL(cr.IsConfirmed, 0) AS IsConfirmed, " &
                           "ISNULL(cr.IsEvaluated, 0) AS IsEvaluated, " &
                           "dbo.ConvertGregorianToJalali(cr.ConfirmedDate) AS ConfirmedDate, " &
                           "CASE WHEN cr.RegistrationID IS NOT NULL THEN 1 ELSE 0 END AS IsRegistered " &
                           "FROM trn_EducationalCourses ec " &
                           "LEFT JOIN trn_CourseRegistrations cr ON ec.CourseID = cr.CourseID AND cr.UserID = @UserID AND cr.IsActive = 1 " &
                           "WHERE ec.IsActive = 1 "

                If Not String.IsNullOrEmpty(searchText) Then
                    query += " AND ec.Title LIKE @SearchText "
                End If

                query += " ORDER BY ec.CreatedDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    If Not String.IsNullOrEmpty(searchText) Then
                        cmd.Parameters.AddWithValue("@SearchText", "%" & searchText & "%")
                    End If
                    conn.Open()
                    Dim dt As New DataTable()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using

                    rptMyCourses.DataSource = dt
                    rptMyCourses.DataBind()
                    pnlEmpty.Visible = (dt.Rows.Count = 0)
                End Using
            End Using

        Catch ex As Exception
            ShowAlert("❌ خطا در بارگذاری دوره‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری محتوای دوره
    ' ============================================================
    Private Sub LoadCourseContents(registrationID As Integer, repeater As Repeater, lblProgress As Label, lblNoContent As Label)
        Try
            If registrationID = 0 Then
                repeater.DataSource = Nothing
                repeater.DataBind()
                lblNoContent.Visible = True
                lblProgress.Text = "0 از 0"
                Return
            End If

            Dim courseID As Integer = 0

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT CourseID FROM trn_CourseRegistrations WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        courseID = Convert.ToInt32(result)
                    End If
                End Using
            End Using

            If courseID = 0 Then
                repeater.DataSource = Nothing
                repeater.DataBind()
                lblNoContent.Visible = True
                lblProgress.Text = "0 از 0"
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT c.ContentID, c.CourseID, c.Title, c.Description, " &
                           "c.ContentType, c.FileName, c.FilePath, c.VideoURL, c.ExternalLink, " &
                           "c.[Order], c.IsRequired, " &
                           "CASE WHEN v.ViewID IS NOT NULL THEN 1 ELSE 0 END AS IsViewed " &
                           "FROM trn_CourseContents c " &
                           "LEFT JOIN trn_ContentViews v ON c.ContentID = v.ContentID " &
                           "AND v.UserID = @UserID AND v.RegistrationID = @RegistrationID " &
                           "WHERE c.CourseID = @CourseID AND c.IsActive = 1 " &
                           "ORDER BY c.[Order]"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Dim dt As New DataTable()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using

                    If dt.Rows.Count > 0 Then
                        repeater.DataSource = dt
                        repeater.DataBind()
                        lblNoContent.Visible = False

                        Dim total As Integer = dt.Rows.Count
                        Dim viewed As Integer = 0
                        For Each row As DataRow In dt.Rows
                            If Not IsDBNull(row("IsViewed")) AndAlso Convert.ToInt32(row("IsViewed")) = 1 Then
                                viewed += 1
                            End If
                        Next
                        lblProgress.Text = viewed & " از " & total
                    Else
                        repeater.DataSource = Nothing
                        repeater.DataBind()
                        lblNoContent.Visible = True
                        lblProgress.Text = "0 از 0"
                    End If
                End Using
            End Using

        Catch ex As Exception
            repeater.DataSource = Nothing
            repeater.DataBind()
            lblNoContent.Visible = True
            lblProgress.Text = "0 از 0"
        End Try
    End Sub

    ' ============================================================
    ' بررسی مشاهده همه محتواها
    ' ============================================================
    Private Function CheckAllViewed(registrationID As Integer) As Boolean
        Try
            If registrationID = 0 Then
                Return False
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT " &
                           "COUNT(*) AS Total, " &
                           "SUM(CASE WHEN v.ViewID IS NOT NULL THEN 1 ELSE 0 END) AS Viewed " &
                           "FROM trn_CourseContents c " &
                           "INNER JOIN trn_CourseRegistrations cr ON c.CourseID = cr.CourseID " &
                           "LEFT JOIN trn_ContentViews v ON c.ContentID = v.ContentID " &
                           "AND v.UserID = @UserID AND v.RegistrationID = @RegistrationID " &
                           "WHERE cr.RegistrationID = @RegistrationID AND c.IsActive = 1"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim total As Integer = Convert.ToInt32(reader("Total"))
                            Dim viewed As Integer = Convert.ToInt32(reader("Viewed"))
                            If total = 0 Then
                                Return True
                            End If
                            Return viewed >= total
                        End If
                    End Using
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' بررسی اینکه قبلاً آزمون داده شده - Public برای استفاده در ASPX
    ' ============================================================
    Public Function CheckIfExamTaken(registrationID As Integer) As Boolean
        Try
            If registrationID = 0 Then
                Return False
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT COUNT(*) FROM trn_ExamResults WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' بررسی وجود سوالات آزمون
    ' ============================================================
    Private Function CheckHasExamQuestions(registrationID As Integer) As Boolean
        Try
            If registrationID = 0 Then
                Return False
            End If

            Dim courseID As Integer = 0
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT CourseID FROM trn_CourseRegistrations WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        courseID = Convert.ToInt32(result)
                    End If
                End Using
            End Using

            If courseID = 0 Then
                Return False
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT COUNT(*) FROM trn_ExamQuestions WHERE CourseID = @CourseID AND IsActive = 1"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    conn.Open()
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' نمایش نتیجه قبلی آزمون
    ' ============================================================
    Private Sub ShowPreviousExamResult(registrationID As Integer)
        Try
            If registrationID = 0 Then
                ShowAlert("❌ شناسه ثبت‌نام نامعتبر است.", "warning")
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT Score, CorrectCount, TotalCount, IsPassed FROM trn_ExamResults " &
                           "WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim score As Integer = Convert.ToInt32(reader("Score"))
                            Dim isPassed As Boolean = Convert.ToBoolean(reader("IsPassed"))
                            Dim correctCount As Integer = Convert.ToInt32(reader("CorrectCount"))
                            Dim totalCount As Integer = Convert.ToInt32(reader("TotalCount"))

                            Dim msg As String = "📊 نتیجه آزمون قبلی شما:\n"
                            msg &= "نمره: " & score & "%\n"
                            msg &= "پاسخ صحیح: " & correctCount & " از " & totalCount & "\n"

                            If isPassed Then
                                msg &= "✅ قبول شدید"
                            Else
                                msg &= "❌ قبول نشدید"
                            End If

                            ShowAlert(msg, "info")
                        Else
                            ShowAlert("❌ نتیجه‌ای برای این آزمون یافت نشد.", "warning")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowAlert("خطا در دریافت نتیجه: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ثبت‌نام در دوره
    ' ============================================================
    Private Sub RegisterCourse(courseID As Integer)
        Try
            If courseID = 0 Then
                ShowAlert("❌ شناسه دوره نامعتبر است.", "warning")
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()

                ' بررسی اینکه قبلاً ثبت‌نام نکرده باشد
                Dim checkQuery = "SELECT COUNT(*) FROM trn_CourseRegistrations WHERE CourseID = @CourseID AND UserID = @UserID AND IsActive = 1"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        ShowAlert("⚠️ شما قبلاً در این دوره ثبت‌نام کرده‌اید.", "warning")
                        Return
                    End If
                End Using

                ' ثبت‌نام جدید
                Dim query = "INSERT INTO trn_CourseRegistrations (CourseID, UserID, RegistrationDate, IsActive) " &
                           "VALUES (@CourseID, @UserID, GETDATE(), 1)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowAlert("✅ ثبت‌نام شما با موفقیت انجام شد. منتظر تایید مسئول آموزش باشید.", "success")

            ' بروزرسانی لیست
            LoadMyCourses()
            upMain.Update()

        Catch ex As Exception
            ShowAlert("❌ خطا در ثبت‌نام: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' رویدادهای Repeater اصلی
    ' ============================================================
    Protected Sub rptMyCourses_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' دریافت RegistrationID
            Dim registrationID As Integer = 0
            Dim regIDObj = DataBinder.Eval(e.Item.DataItem, "RegistrationID")
            If regIDObj IsNot Nothing AndAlso Not IsDBNull(regIDObj) Then
                registrationID = Convert.ToInt32(regIDObj)
            End If

            ' دریافت وضعیت‌ها
            Dim isConfirmed As Boolean = False
            Dim isEvaluated As Boolean = False
            Dim isRegistered As Boolean = False
            Dim isActive As Boolean = False

            Dim isConfirmedObj = DataBinder.Eval(e.Item.DataItem, "IsConfirmed")
            If isConfirmedObj IsNot Nothing AndAlso Not IsDBNull(isConfirmedObj) Then
                isConfirmed = Convert.ToBoolean(isConfirmedObj)
            End If

            Dim isEvaluatedObj = DataBinder.Eval(e.Item.DataItem, "IsEvaluated")
            If isEvaluatedObj IsNot Nothing AndAlso Not IsDBNull(isEvaluatedObj) Then
                isEvaluated = Convert.ToBoolean(isEvaluatedObj)
            End If

            Dim isRegisteredObj = DataBinder.Eval(e.Item.DataItem, "IsRegistered")
            If isRegisteredObj IsNot Nothing AndAlso Not IsDBNull(isRegisteredObj) Then
                isRegistered = Convert.ToBoolean(isRegisteredObj)
            End If

            Dim isActiveObj = DataBinder.Eval(e.Item.DataItem, "IsActive")
            If isActiveObj IsNot Nothing AndAlso Not IsDBNull(isActiveObj) Then
                isActive = Convert.ToBoolean(isActiveObj)
            End If

            ' بارگذاری محتواها
            Dim rptContents As Repeater = DirectCast(e.Item.FindControl("rptContents"), Repeater)
            Dim lblProgress As Label = DirectCast(e.Item.FindControl("lblContentProgress"), Label)
            Dim lblNoContent As Label = DirectCast(e.Item.FindControl("lblNoContent"), Label)

            If rptContents IsNot Nothing AndAlso registrationID > 0 Then
                LoadCourseContents(registrationID, rptContents, lblProgress, lblNoContent)
            ElseIf rptContents IsNot Nothing Then
                rptContents.DataSource = Nothing
                rptContents.DataBind()
                lblNoContent.Visible = True
                lblProgress.Text = "0 از 0"
            End If

            ' پیدا کردن دکمه‌ها
            Dim btnRegister As LinkButton = DirectCast(e.Item.FindControl("btnRegister"), LinkButton)
            Dim btnExam As LinkButton = DirectCast(e.Item.FindControl("btnExam"), LinkButton)
            Dim btnEvaluate As LinkButton = DirectCast(e.Item.FindControl("btnEvaluate"), LinkButton)
            Dim lblStatus As Label = DirectCast(e.Item.FindControl("lblStatusMessage"), Label)

            ' بررسی وضعیت آزمون
            Dim examTaken As Boolean = False
            Dim examPassed As Boolean = False

            If registrationID > 0 Then
                Try
                    Using conn As New SqlConnection(ConnectionString)
                        Dim query = "SELECT IsPassed FROM trn_ExamResults WHERE RegistrationID = @RegistrationID"
                        Using cmd As New SqlCommand(query, conn)
                            cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                            conn.Open()
                            Dim result = cmd.ExecuteScalar()
                            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                                examTaken = True
                                examPassed = Convert.ToBoolean(result)
                            End If
                        End Using
                    End Using
                Catch
                End Try
            End If

            ' تنظیم دکمه ثبت‌نام
            If btnRegister IsNot Nothing Then
                btnRegister.Visible = isActive AndAlso Not isRegistered
            End If

            ' تنظیم دکمه آزمون
            If btnExam IsNot Nothing Then
                If isRegistered AndAlso isConfirmed Then
                    If examTaken Then
                        btnExam.Text = "📊 مشاهده نتیجه"
                        btnExam.CssClass = "btn-exam-done"
                    Else
                        btnExam.Text = "📝 شرکت در آزمون"
                        btnExam.CssClass = "btn-exam"
                    End If
                    btnExam.Visible = True
                Else
                    btnExam.Visible = False
                End If
            End If

            ' تنظیم دکمه ارزیابی
            If btnEvaluate IsNot Nothing Then
                btnEvaluate.Visible = isRegistered AndAlso isConfirmed AndAlso Not isEvaluated
            End If

            ' پیام وضعیت
            If lblStatus IsNot Nothing Then
                If Not isRegistered AndAlso isActive Then
                    lblStatus.Text = "📝 برای ثبت‌نام در این دوره کلیک کنید"
                ElseIf isRegistered AndAlso Not isConfirmed Then
                    lblStatus.Text = "⏳ در انتظار تایید توسط مسئول آموزش"
                ElseIf isRegistered AndAlso isConfirmed AndAlso examTaken AndAlso examPassed Then
                    lblStatus.Text = "✅ در آزمون قبول شده‌اید. می‌توانید ارزیابی را انجام دهید."
                ElseIf isRegistered AndAlso isConfirmed AndAlso examTaken AndAlso Not examPassed Then
                    lblStatus.Text = "❌ در آزمون قبول نشده‌اید. اما می‌توانید ارزیابی را انجام دهید."
                ElseIf isRegistered AndAlso isConfirmed AndAlso Not examTaken Then
                    lblStatus.Text = "📝 برای شرکت در آزمون آماده هستید"
                ElseIf isRegistered AndAlso isConfirmed AndAlso isEvaluated Then
                    lblStatus.Text = "✅ دوره کامل شده است."
                Else
                    lblStatus.Text = "📚 محتوای آموزشی را مشاهده کنید"
                End If
            End If
        End If
    End Sub

    Protected Sub rptMyCourses_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Dim commandArg = e.CommandArgument

        Select Case e.CommandName
            Case "Register"
                Dim courseID As Integer = Convert.ToInt32(commandArg)
                RegisterCourse(courseID)

            Case "StartExam"
                Dim registrationID As Integer = Convert.ToInt32(commandArg)
                If CheckIfExamTaken(registrationID) Then
                    ShowPreviousExamResult(registrationID)
                Else
                    Response.Redirect("Exam.aspx?RegID=" & registrationID)
                End If

            Case "Evaluate"
                Dim registrationID As Integer = Convert.ToInt32(commandArg)
                Response.Redirect("Evaluation.aspx?RegID=" & registrationID)
        End Select
    End Sub

    ' ============================================================
    ' رویدادهای Repeater محتوا
    ' ============================================================
    Protected Sub rptContents_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Dim args As String() = e.CommandArgument.ToString().Split("|")
        Dim contentID As Integer = Convert.ToInt32(args(0))

        Select Case e.CommandName
            Case "ViewContent"
                RegisterView(contentID)

                Dim contentType As String = args(1)
                Dim filePath As String = args(2)
                Dim videoURL As String = args(3)
                Dim externalLink As String = args(4)

                Select Case contentType
                    Case "Video"
                        If Not String.IsNullOrEmpty(videoURL) Then
                            Dim script As String = "window.open('" & videoURL & "', '_blank');"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openVideo", script, True)
                        Else
                            ShowAlert("لینک ویدیو موجود نیست.", "warning")
                        End If

                    Case "Link"
                        If Not String.IsNullOrEmpty(externalLink) Then
                            Dim script As String = "window.open('" & externalLink & "', '_blank');"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openLink", script, True)
                        Else
                            ShowAlert("لینک موجود نیست.", "warning")
                        End If

                    Case "File", "PDF", "PPT"
                        ViewFileInBrowser(contentID)
                End Select

                LoadMyCourses()
                upMain.Update()

            Case "DownloadContent"
                DownloadFile(contentID)
        End Select
    End Sub

    ' ============================================================
    ' ثبت مشاهده محتوا
    ' ============================================================
    Private Sub RegisterView(contentID As Integer)
        Try
            Dim registrationID As Integer = 0

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT TOP 1 cr.RegistrationID FROM trn_CourseRegistrations cr " &
                           "INNER JOIN trn_CourseContents c ON c.CourseID = cr.CourseID " &
                           "WHERE c.ContentID = @ContentID AND cr.UserID = @UserID AND cr.IsActive = 1"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ContentID", contentID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        registrationID = Convert.ToInt32(result)
                    Else
                        Return
                    End If
                End Using
            End Using

            If registrationID = 0 Then
                Return
            End If

            Using conn As New SqlConnection(ConnectionString)
                Dim checkQuery = "SELECT COUNT(*) FROM trn_ContentViews " &
                               "WHERE ContentID = @ContentID AND UserID = @UserID AND RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(checkQuery, conn)
                    cmd.Parameters.AddWithValue("@ContentID", contentID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        Return
                    End If
                End Using
            End Using

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "INSERT INTO trn_ContentViews (ContentID, UserID, RegistrationID, ViewDate, IsCompleted) " &
                           "VALUES (@ContentID, @UserID, @RegistrationID, GETDATE(), 1)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ContentID", contentID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error in RegisterView: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' نمایش فایل در مرورگر
    ' ============================================================
    Private Sub ViewFileInBrowser(contentID As Integer)
        Try
            Dim filePath As String = ""

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT FilePath FROM trn_CourseContents WHERE ContentID = @ContentID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ContentID", contentID)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        filePath = result.ToString()
                    End If
                End Using
            End Using

            If String.IsNullOrEmpty(filePath) Then
                ShowAlert("❌ مسیر فایل مشخص نیست.", "warning")
                Return
            End If

            Dim physicalPath As String = Server.MapPath(filePath)

            If Not File.Exists(physicalPath) Then
                ShowAlert("❌ فایل یافت نشد. مسیر: " & physicalPath, "warning")
                Return
            End If

            Dim fileUrl As String = ResolveUrl(filePath)
            Dim script As String = "window.open('" & fileUrl & "', '_blank');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openFile", script, True)

        Catch ex As Exception
            ShowAlert("❌ خطا در نمایش فایل: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دانلود فایل
    ' ============================================================
    Private Sub DownloadFile(contentID As Integer)
        Try
            Dim filePath As String = ""
            Dim fileName As String = ""

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT FilePath, FileName FROM trn_CourseContents WHERE ContentID = @ContentID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ContentID", contentID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            filePath = reader("FilePath").ToString()
                            fileName = reader("FileName").ToString()
                        Else
                            ShowAlert("❌ اطلاعات فایل یافت نشد.", "warning")
                            Return
                        End If
                    End Using
                End Using
            End Using

            If String.IsNullOrEmpty(filePath) Then
                ShowAlert("❌ مسیر فایل مشخص نیست.", "warning")
                Return
            End If

            If String.IsNullOrEmpty(fileName) Then
                fileName = Path.GetFileName(filePath)
            End If

            Dim physicalPath As String = Server.MapPath(filePath)

            If Not File.Exists(physicalPath) Then
                ShowAlert("❌ فایل یافت نشد. مسیر: " & physicalPath, "warning")
                Return
            End If

            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & fileName)
            Response.AppendHeader("Content-Length", New FileInfo(physicalPath).Length.ToString())
            Response.TransmitFile(physicalPath)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            ShowAlert("❌ خطا در دانلود: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دریافت MIME Type
    ' ============================================================
    Private Function GetMimeType(filePath As String) As String
        Dim extension As String = Path.GetExtension(filePath).ToLower()
        Select Case extension
            Case ".pdf" : Return "application/pdf"
            Case ".doc" : Return "application/msword"
            Case ".docx" : Return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            Case ".xls" : Return "application/vnd.ms-excel"
            Case ".xlsx" : Return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Case ".ppt" : Return "application/vnd.ms-powerpoint"
            Case ".pptx" : Return "application/vnd.openxmlformats-officedocument.presentationml.presentation"
            Case ".jpg", ".jpeg" : Return "image/jpeg"
            Case ".png" : Return "image/png"
            Case ".gif" : Return "image/gif"
            Case ".mp4" : Return "video/mp4"
            Case ".avi" : Return "video/x-msvideo"
            Case ".mkv" : Return "video/x-matroska"
            Case ".txt" : Return "text/plain"
            Case ".zip" : Return "application/zip"
            Case ".rar" : Return "application/x-rar-compressed"
            Case Else : Return "application/octet-stream"
        End Select
    End Function

    ' ============================================================
    ' توابع کمکی برای ASPX
    ' ============================================================
    Public Function GetStatusClass(isConfirmed As Object) As String
        If isConfirmed Is Nothing OrElse IsDBNull(isConfirmed) Then
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
        If isConfirmed Is Nothing OrElse IsDBNull(isConfirmed) Then
            Return "در انتظار تایید"
        End If
        Dim status As Integer = Convert.ToInt32(isConfirmed)
        Select Case status
            Case 0 : Return "در انتظار تایید"
            Case 1 : Return "تایید شده ✓"
            Case 2 : Return "رد شده ✗"
            Case Else : Return "نامشخص"
        End Select
    End Function

    Public Function GetContentIcon(contentType As Object) As String
        Dim type As String = contentType.ToString().ToLower()
        Select Case type
            Case "video" : Return "🎬"
            Case "file" : Return "📄"
            Case "pdf" : Return "📕"
            Case "ppt", "pptx" : Return "📊"
            Case "link" : Return "🔗"
            Case "doc", "docx" : Return "📝"
            Case "xls", "xlsx" : Return "📈"
            Case "zip", "rar" : Return "📦"
            Case "jpg", "jpeg", "png", "gif", "bmp" : Return "🖼️"
            Case Else : Return "📁"
        End Select
    End Function

    ' ============================================================
    ' رویدادهای فیلترها
    ' ============================================================
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadMyCourses()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadMyCourses()
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadMyCourses()
        ShowAlert("لیست دوره‌ها بروزرسانی شد.", "info")
    End Sub

    ' ============================================================
    ' نمایش پیام با جاوااسکریپت
    ' ============================================================
    Private Sub ShowAlert(message As String, type As String)
        Dim icon As String = ""
        Select Case type
            Case "success"
                icon = "✅ "
            Case "danger", "error"
                icon = "❌ "
            Case "warning"
                icon = "⚠️ "
            Case Else
                icon = "ℹ️ "
        End Select

        Dim cleanMessage As String = message.Replace("'", "\'").Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        Dim script As String = "alert('" & icon & cleanMessage & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", script, True)
    End Sub

End Class