Imports System.Data
Imports System.Data.SqlClient

Partial Class Exam
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn
    Private UserID As Integer
    Private RegistrationID As Integer
    Private CourseID As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If

        UserID = Convert.ToInt32(Session("userid"))

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("RegID")) Then
                RegistrationID = Convert.ToInt32(Request.QueryString("RegID"))
                hfRegistrationID.Value = RegistrationID.ToString()
                LoadExamData()
                LoadQuestions()

                ' ============================================================
                ' بررسی اینکه قبلاً آزمون داده شده
                ' ============================================================
                If CheckIfExamTaken(RegistrationID) Then
                    ShowAlert("⚠️ شما قبلاً در این آزمون شرکت کرده‌اید.", "warning")
                    btnSubmitExam.Enabled = False
                    btnSubmitExam.Text = "✓ قبلاً شرکت کرده‌اید"

                    ' غیرفعال کردن همه کنترل‌ها
                    For Each item As RepeaterItem In rptQuestions.Items
                        Dim rptOptions As Repeater = DirectCast(item.FindControl("rptOptions"), Repeater)
                        If rptOptions IsNot Nothing Then
                            For Each optItem As RepeaterItem In rptOptions.Items
                                Dim rb As RadioButton = DirectCast(optItem.FindControl("rbOption"), RadioButton)
                                If rb IsNot Nothing Then
                                    rb.Enabled = False
                                End If
                            Next
                        End If
                    Next

                    ' نمایش نتیجه قبلی
                    ShowPreviousResult()
                End If
            Else
                ShowAlert("لینک آزمون نامعتبر است.", "danger")
            End If
        End If
    End Sub

    ' ============================================================
    ' تابع کمکی برای دریافت GroupName (رفع خطای ItemIndex)
    ' ============================================================
    Public Function GetGroupName(container As Control) As String
        Try
            Dim item As RepeaterItem = Nothing
            Dim current As Control = container

            While current IsNot Nothing
                If TypeOf current Is RepeaterItem Then
                    item = DirectCast(current, RepeaterItem)
                    Exit While
                End If
                current = current.Parent
            End While

            If item IsNot Nothing Then
                Return "q" & item.ItemIndex
            End If
        Catch
        End Try

        Return "q0"
    End Function

    ' ============================================================
    ' بارگذاری اطلاعات آزمون
    ' ============================================================
    Private Sub LoadExamData()
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT cr.CourseID, cr.UserID, ec.Title AS CourseTitle, " &
                           "ec.MinPassScore, " &
                           "p.nam, p.family " &
                           "FROM trn_CourseRegistrations cr " &
                           "INNER JOIN trn_EducationalCourses ec ON cr.CourseID = ec.CourseID " &
                           "INNER JOIN personel p ON cr.UserID = p.ID " &
                           "WHERE cr.RegistrationID = @RegistrationID AND cr.UserID = @UserID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            CourseID = Convert.ToInt32(reader("CourseID"))
                            hfCourseID.Value = CourseID.ToString()

                            lblCourseTitle.Text = reader("CourseTitle").ToString()
                            lblCourseTitleInfo.Text = reader("CourseTitle").ToString()
                            lblPersonnelName.Text = reader("nam").ToString() & " " & reader("family").ToString()
                            lblMinPassScore.Text = reader("MinPassScore").ToString()
                        Else
                            ShowAlert("ثبت‌نام مورد نظر یافت نشد.", "danger")
                            btnSubmitExam.Enabled = False
                            Return
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری سوالات آزمون
    ' ============================================================
    Private Sub LoadQuestions()
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT q.QuestionID, q.QuestionText, q.QuestionType, q.Score, " &
                           "o.OptionID, o.OptionText, o.IsCorrect " &
                           "FROM trn_ExamQuestions q " &
                           "LEFT JOIN trn_ExamOptions o ON q.QuestionID = o.QuestionID " &
                           "WHERE q.CourseID = @CourseID AND q.IsActive = 1 " &
                           "ORDER BY q.[Order], o.[Order]"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", CourseID)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            ' گروه‌بندی سوالات
                            Dim questions = dt.AsEnumerable() _
                                .GroupBy(Function(r) r.Field(Of Integer)("QuestionID")) _
                                .Select(Function(g) New With {
                                    .QuestionID = g.Key,
                                    .QuestionText = g.First().Field(Of String)("QuestionText"),
                                    .QuestionType = g.First().Field(Of String)("QuestionType"),
                                    .Score = g.First().Field(Of Integer)("Score"),
                                    .Options = g.Select(Function(r) New With {
                                        .OptionID = r.Field(Of Integer)("OptionID"),
                                        .OptionText = r.Field(Of String)("OptionText"),
                                        .IsCorrect = r.Field(Of Boolean)("IsCorrect")
                                    }).ToList()
                                }).ToList()

                            rptQuestions.DataSource = questions
                            rptQuestions.DataBind()

                            lblTotalQuestions.Text = questions.Count.ToString()

                            ' تنظیم تایمر (30 دقیقه)
                            hfTimeRemaining.Value = "1800"
                        Else
                            ShowAlert("هیچ سوالی برای این آزمون تعریف نشده است.", "warning")
                            btnSubmitExam.Enabled = False
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ShowAlert("خطا در بارگذاری سوالات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' رویداد ItemDataBound برای Repeater سوالات
    ' ============================================================
    Protected Sub rptQuestions_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim question = e.Item.DataItem
            Dim rptOptions As Repeater = DirectCast(e.Item.FindControl("rptOptions"), Repeater)

            If rptOptions IsNot Nothing AndAlso question IsNot Nothing Then
                Dim options = question.GetType().GetProperty("Options").GetValue(question, Nothing)
                rptOptions.DataSource = options
                rptOptions.DataBind()
            End If
        End If
    End Sub

    ' ============================================================
    ' بررسی اینکه قبلاً آزمون داده شده است
    ' ============================================================
    Private Sub CheckAlreadyTaken()
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT COUNT(*) FROM trn_ExamResults WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                    conn.Open()
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count > 0 Then
                        ShowAlert("شما قبلاً در این آزمون شرکت کرده‌اید.", "warning")
                        btnSubmitExam.Enabled = False
                        btnSubmitExam.Text = "✓ قبلاً شرکت کرده‌اید"

                        ' غیرفعال کردن همه کنترل‌ها
                        For Each item As RepeaterItem In rptQuestions.Items
                            Dim rptOptions As Repeater = DirectCast(item.FindControl("rptOptions"), Repeater)
                            If rptOptions IsNot Nothing Then
                                For Each optItem As RepeaterItem In rptOptions.Items
                                    Dim rb As RadioButton = DirectCast(optItem.FindControl("rbOption"), RadioButton)
                                    If rb IsNot Nothing Then
                                        rb.Enabled = False
                                    End If
                                Next
                            End If
                        Next

                        ' نمایش نتیجه قبلی
                        ShowPreviousResult()
                    End If
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' نمایش نتیجه قبلی
    ' ============================================================
    Private Sub ShowPreviousResult()
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT Score, CorrectCount, TotalCount, IsPassed FROM trn_ExamResults " &
                           "WHERE RegistrationID = @RegistrationID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", RegistrationID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim score As Integer = Convert.ToInt32(reader("Score"))
                            Dim isPassed As Boolean = Convert.ToBoolean(reader("IsPassed"))

                            lblResultScore.Text = score & "%"
                            If isPassed Then
                                lblResultScore.CssClass = "score passed"
                            Else
                                lblResultScore.CssClass = "score failed"
                            End If

                            lblCorrectCount.Text = reader("CorrectCount").ToString()
                            lblTotalCount.Text = reader("TotalCount").ToString()

                            If isPassed Then
                                lblResultMessage.Text = "✅ قبول شدید! تبریک می‌گوییم."
                            Else
                                lblResultMessage.Text = "❌ متأسفانه قبول نشدید. لطفاً با مدیر آموزش تماس بگیرید."
                            End If

                            pnlResult.Visible = True
                            btnSubmitExam.Visible = False
                            btnCancelExam.Visible = False
                            timerContainer.Visible = False
                        End If
                    End Using
                End Using
            End Using
        Catch
        End Try
    End Sub

    ' ============================================================
    ' ارسال آزمون
    ' ============================================================
    Protected Sub btnSubmitExam_Click(sender As Object, e As EventArgs)
        Try
            ' ============================================================
            ' 1. بررسی اینکه قبلاً آزمون داده نشده باشد
            ' ============================================================
            Dim regID As Integer = Convert.ToInt32(hfRegistrationID.Value)
            If CheckIfExamTaken(regID) Then
                ShowAlert("⚠️ شما قبلاً در این آزمون شرکت کرده‌اید.", "warning")
                btnSubmitExam.Enabled = False
                btnSubmitExam.Text = "✓ قبلاً شرکت کرده‌اید"
                Return
            End If

            ' ============================================================
            ' 2. جمع‌آوری پاسخ‌ها
            ' ============================================================
            Dim answers As New Dictionary(Of Integer, Integer)()
            Dim totalCorrect As Integer = 0
            Dim totalQuestions As Integer = 0
            Dim totalScore As Integer = 0
            Dim earnedScore As Integer = 0

            For Each item As RepeaterItem In rptQuestions.Items
                If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                    totalQuestions += 1

                    Dim hfQuestionID As HiddenField = DirectCast(item.FindControl("hfQuestionID"), HiddenField)
                    Dim rptOptions As Repeater = DirectCast(item.FindControl("rptOptions"), Repeater)

                    If hfQuestionID IsNot Nothing AndAlso rptOptions IsNot Nothing Then
                        Dim questionID As Integer = Convert.ToInt32(hfQuestionID.Value)
                        Dim selectedOptionID As Integer = 0

                        ' پیدا کردن پاسخ انتخاب شده
                        For Each optItem As RepeaterItem In rptOptions.Items
                            Dim rb As RadioButton = DirectCast(optItem.FindControl("rbOption"), RadioButton)
                            If rb IsNot Nothing AndAlso rb.Checked Then
                                Dim hfOptionID As HiddenField = DirectCast(optItem.FindControl("hfOptionID"), HiddenField)
                                If hfOptionID IsNot Nothing Then
                                    selectedOptionID = Convert.ToInt32(hfOptionID.Value)
                                End If
                                Exit For
                            End If
                        Next

                        If selectedOptionID > 0 Then
                            answers.Add(questionID, selectedOptionID)

                            ' بررسی صحیح بودن پاسخ
                            Using conn As New SqlConnection(ConnectionString)
                                Dim query = "SELECT IsCorrect, Score FROM trn_ExamOptions o " &
                                           "INNER JOIN trn_ExamQuestions q ON o.QuestionID = q.QuestionID " &
                                           "WHERE o.OptionID = @OptionID"
                                Using cmd As New SqlCommand(query, conn)
                                    cmd.Parameters.AddWithValue("@OptionID", selectedOptionID)
                                    conn.Open()
                                    Using reader = cmd.ExecuteReader()
                                        If reader.Read() Then
                                            Dim isCorrect As Boolean = Convert.ToBoolean(reader("IsCorrect"))
                                            Dim score As Integer = Convert.ToInt32(reader("Score"))
                                            totalScore += score
                                            If isCorrect Then
                                                totalCorrect += 1
                                                earnedScore += score
                                            End If
                                        End If
                                    End Using
                                End Using
                            End Using
                        End If
                    End If
                End If
            Next

            ' اگر به هیچ سوالی پاسخ داده نشده باشد
            If answers.Count = 0 Then
                ShowAlert("⚠️ شما به هیچ سوالی پاسخ نداده‌اید!", "warning")
                Return
            End If

            ' ============================================================
            ' 3. محاسبه نمره نهایی
            ' ============================================================
            Dim minPassScore As Integer = Convert.ToInt32(lblMinPassScore.Text)
            Dim finalScore As Integer = 0

            If totalScore > 0 Then
                finalScore = CInt(Math.Round((earnedScore / totalScore) * 100))
            End If

            Dim isPassed As Boolean = (finalScore >= minPassScore)

            ' ============================================================
            ' 4. ذخیره در دیتابیس (با تراکنش)
            ' ============================================================
            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        ' 4-1. ذخیره نتایج
                        Dim query = "INSERT INTO trn_ExamResults " &
                                   "(RegistrationID, Score, CorrectCount, TotalCount, IsPassed, ExamDate) " &
                                   "VALUES (@RegistrationID, @Score, @CorrectCount, @TotalCount, @IsPassed, GETDATE()); " &
                                   "SELECT SCOPE_IDENTITY()"

                        Dim resultID As Integer
                        Using cmd As New SqlCommand(query, conn, trans)
                            cmd.Parameters.AddWithValue("@RegistrationID", regID)
                            cmd.Parameters.AddWithValue("@Score", finalScore)
                            cmd.Parameters.AddWithValue("@CorrectCount", totalCorrect)
                            cmd.Parameters.AddWithValue("@TotalCount", totalQuestions)
                            cmd.Parameters.AddWithValue("@IsPassed", isPassed)
                            resultID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' 4-2. ذخیره پاسخ‌ها
                        For Each answer In answers
                            Dim detailQuery = "INSERT INTO trn_ExamAnswerDetails " &
                                             "(ResultID, QuestionID, OptionID) " &
                                             "VALUES (@ResultID, @QuestionID, @OptionID)"
                            Using cmd As New SqlCommand(detailQuery, conn, trans)
                                cmd.Parameters.AddWithValue("@ResultID", resultID)
                                cmd.Parameters.AddWithValue("@QuestionID", answer.Key)
                                cmd.Parameters.AddWithValue("@OptionID", answer.Value)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

            ' ============================================================
            ' 5. نمایش نتیجه
            ' ============================================================
            lblResultScore.Text = finalScore & "%"
            If isPassed Then
                lblResultScore.CssClass = "score passed"
                lblResultMessage.Text = "✅ قبول شدید! تبریک می‌گوییم. نمره شما " & finalScore & "% است."
            Else
                lblResultScore.CssClass = "score failed"
                lblResultMessage.Text = "❌ متأسفانه قبول نشدید. نمره شما " & finalScore & "% است (حداقل قبولی: " & minPassScore & "%). لطفاً با مدیر آموزش تماس بگیرید."
            End If

            lblCorrectCount.Text = totalCorrect.ToString()
            lblTotalCount.Text = totalQuestions.ToString()

            pnlResult.Visible = True
            btnSubmitExam.Visible = False
            btnCancelExam.Visible = False
            timerContainer.Visible = False

            ' غیرفعال کردن همه کنترل‌ها
            For Each item As RepeaterItem In rptQuestions.Items
                Dim rptOptions As Repeater = DirectCast(item.FindControl("rptOptions"), Repeater)
                If rptOptions IsNot Nothing Then
                    For Each optItem As RepeaterItem In rptOptions.Items
                        Dim rb As RadioButton = DirectCast(optItem.FindControl("rbOption"), RadioButton)
                        If rb IsNot Nothing Then
                            rb.Enabled = False
                        End If
                    Next
                End If
            Next

            ' ============================================================
            ' 6. غیرفعال کردن دکمه ارسال برای همیشه
            ' ============================================================
            btnSubmitExam.Enabled = False
            btnSubmitExam.Text = "✓ ارسال شده"

        Catch ex As Exception
            ShowAlert("❌ خطا در ثبت پاسخ‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بررسی اینکه قبلاً آزمون داده شده است
    ' ============================================================
    Private Function CheckIfExamTaken(registrationID As Integer) As Boolean
        Try
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
    ' انصراف
    ' ============================================================
    Protected Sub btnCancelExam_Click(sender As Object, e As EventArgs)
        Response.Redirect("MyCourses.aspx")
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
            Case "warning"
                pnlAlert.CssClass = "alert alert-warning alert-dismissible fade show"
            Case Else
                pnlAlert.CssClass = "alert alert-info alert-dismissible fade show"
        End Select
    End Sub

End Class