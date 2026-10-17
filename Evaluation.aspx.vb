Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class Evaluation
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn
    Private UserID As Integer

    ' ============================================================
    ' کلاس‌های مدل
    ' ============================================================
    Public Class Question
        Public Property QuestionID As Integer
        Public Property QuestionText As String
        Public Property Description As String
        Public Property Options As List(Of OptionItem)
        Public Property QuestionType As String
        Public Property IsRequired As Boolean
    End Class

    Public Class OptionItem
        Public Property Value As String
        Public Property Text As String
    End Class

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If

        UserID = Convert.ToInt32(Session("userid"))

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("RegID")) Then
                Dim regID As Integer = Convert.ToInt32(Request.QueryString("RegID"))
                hfRegistrationID.Value = regID.ToString()
                LoadEvaluationData(regID)
            Else
                ShowMessageBox("لینک ارزیابی نامعتبر است.", "error")
            End If
        End If
    End Sub

    ' ============================================================
    ' نمایش پیام با MessageBox جاوااسکریپت
    ' ============================================================
    Private Sub ShowMessageBox(message As String, type As String)
        Dim icon As String = ""
        Select Case type
            Case "success"
                icon = "✅ "
            Case "error"
                icon = "❌ "
            Case "warning"
                icon = "⚠️ "
            Case Else
                icon = "ℹ️ "
        End Select

        Dim script As String = "alert('" & icon & message.Replace("'", "\'") & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", script, True)
    End Sub

    ' ============================================================
    ' تابع کمکی برای دریافت GroupName
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
    ' بارگذاری اطلاعات ارزیابی
    ' ============================================================
    Private Sub LoadEvaluationData(registrationID As Integer)
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT cr.RegistrationID, cr.CourseID, cr.UserID, " &
                           "ISNULL(ec.Title, 'عنوان دوره مشخص نیست') AS CourseTitle, " &
                           "ec.StartDate, " &
                           "ISNULL(p.nam, 'نامشخص') AS nam, " &
                           "ISNULL(p.family, 'نامشخص') AS family, " &
                           "ISNULL(p.codePersoneli, 'نامشخص') AS codePersoneli " &
                           "FROM trn_CourseRegistrations cr " &
                           "LEFT JOIN trn_EducationalCourses ec ON cr.CourseID = ec.CourseID " &
                           "LEFT JOIN personel p ON cr.UserID = p.ID " &
                           "WHERE cr.RegistrationID = @RegistrationID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            hfCourseID.Value = reader("CourseID").ToString()
                            hfUserID.Value = reader("UserID").ToString()

                            lblCourseTitle.Text = reader("CourseTitle").ToString()
                            lblInstructor.Text = "---"

                            If Not IsDBNull(reader("StartDate")) AndAlso reader("StartDate") IsNot Nothing Then
                                Try
                                    lblCourseDate.Text = Convert.ToDateTime(reader("StartDate")).ToString("yyyy/MM/dd")
                                Catch
                                    lblCourseDate.Text = "تاریخ نامشخص"
                                End Try
                            Else
                                lblCourseDate.Text = "تاریخ نامشخص"
                            End If

                            Dim firstName As String = reader("nam").ToString()
                            Dim lastName As String = reader("family").ToString()
                            lblPersonnelName.Text = firstName & " " & lastName
                        Else
                            ShowMessageBox("ثبت‌نام مورد نظر یافت نشد.", "error")
                            btnSubmit.Enabled = False
                            Return
                        End If
                    End Using
                End Using
            End Using

            LoadQuestions()
            CheckIfEvaluated()

        Catch ex As Exception
            ShowMessageBox("خطا در بارگذاری اطلاعات: " & ex.Message, "error")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری سوالات از دیتابیس
    ' ============================================================
    Private Sub LoadQuestions()
        Try
            Dim questions As New List(Of Question)()
            Dim courseID As Integer = Convert.ToInt32(hfCourseID.Value)

            If courseID > 0 Then
                Using conn As New SqlConnection(ConnectionString)
                    Dim query = "SELECT QuestionID, CourseID, QuestionText, " &
                               "QuestionType, IsRequired, [Order], IsActive, CreatedDate " &
                               "FROM trn_EvaluationQuestions " &
                               "WHERE CourseID = @CourseID AND IsActive = 1 " &
                               "ORDER BY [Order]"

                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@CourseID", courseID)
                        conn.Open()
                        Using reader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim q As New Question()
                                q.QuestionID = Convert.ToInt32(reader("QuestionID"))
                                q.QuestionText = reader("QuestionText").ToString()
                                q.Description = ""
                                q.QuestionType = reader("QuestionType").ToString()
                                q.IsRequired = Convert.ToBoolean(reader("IsRequired"))
                                q.Options = New List(Of OptionItem)()

                                LoadQuestionOptions(q.QuestionID, q.Options)
                                questions.Add(q)
                            End While
                        End Using
                    End Using
                End Using
            End If

            If questions.Count = 0 Then
                questions = GetDefaultQuestions()
            End If

            rptQuestions.DataSource = questions
            rptQuestions.DataBind()

        Catch ex As Exception
            ShowMessageBox("خطا در بارگذاری سوالات: " & ex.Message, "error")
            rptQuestions.DataSource = GetDefaultQuestions()
            rptQuestions.DataBind()
        End Try
    End Sub

    ' ============================================================
    ' دریافت گزینه‌های سوال از دیتابیس
    ' ============================================================
    Private Sub LoadQuestionOptions(questionID As Integer, options As List(Of OptionItem))
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT OptionValue, OptionText FROM trn_QuestionOptions " &
                           "WHERE QuestionID = @QuestionID ORDER BY SortOrder"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@QuestionID", questionID)
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            options.Add(New OptionItem() With {
                                .Value = reader("OptionValue").ToString(),
                                .Text = reader("OptionText").ToString()
                            })
                        End While
                    End Using
                End Using
            End Using

            If options.Count = 0 Then
                AddDefaultOptions(options)
            End If

        Catch ex As Exception
            AddDefaultOptions(options)
        End Try
    End Sub

    ' ============================================================
    ' گزینه‌های پیش‌فرض
    ' ============================================================
    Private Sub AddDefaultOptions(options As List(Of OptionItem))
        options.Clear()
        options.Add(New OptionItem() With {.Value = "1", .Text = "خیلی ضعیف"})
        options.Add(New OptionItem() With {.Value = "2", .Text = "ضعیف"})
        options.Add(New OptionItem() With {.Value = "3", .Text = "متوسط"})
        options.Add(New OptionItem() With {.Value = "4", .Text = "خوب"})
        options.Add(New OptionItem() With {.Value = "5", .Text = "عالی"})
    End Sub

    ' ============================================================
    ' سوالات پیش‌فرض
    ' ============================================================
    Private Function GetDefaultQuestions() As List(Of Question)
        Dim questions As New List(Of Question)()

        Dim q1 As New Question()
        q1.QuestionID = 1
        q1.QuestionText = "میزان رضایت شما از محتوای دوره چقدر است؟"
        q1.Description = "لطفاً بر اساس کیفیت مطالب و سرفصل‌های ارائه شده پاسخ دهید."
        q1.QuestionType = "MultipleChoice"
        q1.IsRequired = True
        q1.Options = New List(Of OptionItem)()
        q1.Options.Add(New OptionItem() With {.Value = "1", .Text = "خیلی کم"})
        q1.Options.Add(New OptionItem() With {.Value = "2", .Text = "کم"})
        q1.Options.Add(New OptionItem() With {.Value = "3", .Text = "متوسط"})
        q1.Options.Add(New OptionItem() With {.Value = "4", .Text = "زیاد"})
        q1.Options.Add(New OptionItem() With {.Value = "5", .Text = "خیلی زیاد"})
        questions.Add(q1)

        Dim q2 As New Question()
        q2.QuestionID = 2
        q2.QuestionText = "میزان تسلط مدرس بر موضوعات دوره چگونه بود؟"
        q2.Description = "لطفاً بر اساس دانش و توانایی مدرس در انتقال مطالب پاسخ دهید."
        q2.QuestionType = "MultipleChoice"
        q2.IsRequired = True
        q2.Options = New List(Of OptionItem)()
        q2.Options.Add(New OptionItem() With {.Value = "1", .Text = "خیلی ضعیف"})
        q2.Options.Add(New OptionItem() With {.Value = "2", .Text = "ضعیف"})
        q2.Options.Add(New OptionItem() With {.Value = "3", .Text = "متوسط"})
        q2.Options.Add(New OptionItem() With {.Value = "4", .Text = "خوب"})
        q2.Options.Add(New OptionItem() With {.Value = "5", .Text = "عالی"})
        questions.Add(q2)

        Dim q3 As New Question()
        q3.QuestionID = 3
        q3.QuestionText = "آیا دوره برای نیازهای شغلی شما مفید بود؟"
        q3.Description = "لطفاً بر اساس کاربرد مطالب در محیط کار خود پاسخ دهید."
        q3.QuestionType = "MultipleChoice"
        q3.IsRequired = True
        q3.Options = New List(Of OptionItem)()
        q3.Options.Add(New OptionItem() With {.Value = "1", .Text = "کاملاً بی‌فایده"})
        q3.Options.Add(New OptionItem() With {.Value = "2", .Text = "کم مفید"})
        q3.Options.Add(New OptionItem() With {.Value = "3", .Text = "تا حدودی مفید"})
        q3.Options.Add(New OptionItem() With {.Value = "4", .Text = "مفید"})
        q3.Options.Add(New OptionItem() With {.Value = "5", .Text = "بسیار مفید"})
        questions.Add(q3)

        Dim q4 As New Question()
        q4.QuestionID = 4
        q4.QuestionText = "کیفیت امکانات و تجهیزات برگزاری دوره را چگونه ارزیابی می‌کنید؟"
        q4.Description = "امکاناتی مانند سالن، پروژکتور، اینترنت، و غیره."
        q4.QuestionType = "MultipleChoice"
        q4.IsRequired = True
        q4.Options = New List(Of OptionItem)()
        q4.Options.Add(New OptionItem() With {.Value = "1", .Text = "بسیار ضعیف"})
        q4.Options.Add(New OptionItem() With {.Value = "2", .Text = "ضعیف"})
        q4.Options.Add(New OptionItem() With {.Value = "3", .Text = "متوسط"})
        q4.Options.Add(New OptionItem() With {.Value = "4", .Text = "خوب"})
        q4.Options.Add(New OptionItem() With {.Value = "5", .Text = "عالی"})
        questions.Add(q4)

        Dim q5 As New Question()
        q5.QuestionID = 5
        q5.QuestionText = "آیا این دوره را به همکاران خود توصیه می‌کنید؟"
        q5.Description = "لطفاً بر اساس تجربه خود پاسخ دهید."
        q5.QuestionType = "MultipleChoice"
        q5.IsRequired = True
        q5.Options = New List(Of OptionItem)()
        q5.Options.Add(New OptionItem() With {.Value = "1", .Text = "کاملاً مخالف"})
        q5.Options.Add(New OptionItem() With {.Value = "2", .Text = "مخالف"})
        q5.Options.Add(New OptionItem() With {.Value = "3", .Text = "نظری ندارم"})
        q5.Options.Add(New OptionItem() With {.Value = "4", .Text = "موافق"})
        q5.Options.Add(New OptionItem() With {.Value = "5", .Text = "کاملاً موافق"})
        questions.Add(q5)

        Return questions
    End Function

    ' ============================================================
    ' بررسی ارزیابی قبلی
    ' ============================================================
    Private Sub CheckIfEvaluated()
        Try
            Dim regID As Integer = Convert.ToInt32(hfRegistrationID.Value)

            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT COUNT(*) FROM trn_EvaluationResults " &
                           "WHERE RegistrationID = @RegistrationID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", regID)
                    conn.Open()
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count > 0 Then
                        ShowMessageBox("شما قبلاً این دوره را ارزیابی کرده‌اید.", "warning")
                        btnSubmit.Enabled = False
                        btnSubmit.Text = "✓ قبلاً ارسال شده"

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
                        txtComments.Enabled = False
                    End If
                End Using
            End Using

        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' رویدادهای Repeater
    ' ============================================================
    Protected Sub rptQuestions_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim question As Question = DirectCast(e.Item.DataItem, Question)
            Dim rptOptions As Repeater = DirectCast(e.Item.FindControl("rptOptions"), Repeater)

            If rptOptions IsNot Nothing AndAlso question IsNot Nothing Then
                rptOptions.DataSource = question.Options
                rptOptions.DataBind()
            End If
        End If
    End Sub

    Protected Sub rptOptions_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rb As RadioButton = DirectCast(e.Item.FindControl("rbOption"), RadioButton)
            Dim lblOption As HtmlGenericControl = DirectCast(e.Item.FindControl("lblOption"), HtmlGenericControl)
            Dim optionItem As OptionItem = DirectCast(e.Item.DataItem, OptionItem)

            If rb IsNot Nothing AndAlso optionItem IsNot Nothing Then
                ' ذخیره مقدار در Attributes
                rb.Attributes.Add("data-value", optionItem.Value)

                ' اگر برچسب وجود ندارد، از Text استفاده کن
                If lblOption IsNot Nothing Then
                    lblOption.InnerText = optionItem.Text
                End If
            End If
        End If
    End Sub

    ' ============================================================
    ' دکمه تست
    ' ============================================================
    Protected Sub btnTest_Click(sender As Object, e As EventArgs)
        ShowMessageBox("✅ این یک پیام تست است! دکمه ارسال کار می‌کند.", "success")
    End Sub

    ' ============================================================
    ' ارسال ارزیابی
    ' ============================================================
    ' ============================================================
    ' ارسال ارزیابی - اصلاح شده با روش جدید جمع‌آوری پاسخ‌ها
    ' ============================================================
    ' ============================================================
    ' ارسال ارزیابی - با دیباگ
    ' ============================================================
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim regID As Integer = Convert.ToInt32(hfRegistrationID.Value)

            If regID = 0 Then
                ShowMessageBox("شناسه ثبت‌نام نامعتبر است.", "error")
                Return
            End If

            If UserID = 0 Then
                ShowMessageBox("شناسه کاربر نامعتبر است.", "error")
                Return
            End If

            If CheckIfAlreadyEvaluated(regID) Then
                ShowMessageBox("شما قبلاً این دوره را ارزیابی کرده‌اید.", "warning")
                Return
            End If

            Dim totalQuestions As Integer = 0
            Dim answeredQuestions As Integer = 0
            Dim answers As New Dictionary(Of Integer, String)()
            Dim isValid As Boolean = True
            Dim questionIndex As Integer = 0
            Dim debugMessage As String = ""

            For Each item As RepeaterItem In rptQuestions.Items
                If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                    totalQuestions += 1
                    questionIndex += 1

                    ' دریافت اطلاعات سوال
                    Dim question As Question = DirectCast(item.DataItem, Question)
                    Dim questionID As Integer = 0
                    If question IsNot Nothing Then
                        questionID = question.QuestionID
                    End If

                    ' دیباگ - نمایش QuestionID
                    debugMessage &= "سوال " & questionIndex & " - QuestionID: " & questionID & vbCrLf

                    ' پیدا کردن RadioButton انتخاب شده
                    Dim selectedValue As String = ""
                    Dim rptOptions As Repeater = DirectCast(item.FindControl("rptOptions"), Repeater)

                    If rptOptions IsNot Nothing Then
                        For Each optItem As RepeaterItem In rptOptions.Items
                            Dim rb As RadioButton = DirectCast(optItem.FindControl("rbOption"), RadioButton)
                            If rb IsNot Nothing AndAlso rb.Checked Then
                                selectedValue = rb.Attributes("data-value")
                                If String.IsNullOrEmpty(selectedValue) Then
                                    selectedValue = rb.Text
                                End If
                                Exit For
                            End If
                        Next
                    End If

                    If Not String.IsNullOrEmpty(selectedValue) Then
                        answeredQuestions += 1
                        debugMessage &= "  - پاسخ: " & selectedValue & vbCrLf
                    End If

                    If String.IsNullOrEmpty(selectedValue) Then
                        isValid = False
                        ShowMessageBox("لطفاً به سوال شماره " & questionIndex & " پاسخ دهید.", "warning")
                        Exit For
                    End If

                    ' =============================================
                    ' 🛠️ اصلاح: اگر questionID = 0 بود، از questionIndex استفاده کن
                    ' =============================================
                    If questionID > 0 Then
                        answers.Add(questionID, selectedValue)
                    Else
                        ' اگر QuestionID صفر بود، از Index استفاده کن
                        answers.Add(questionIndex, selectedValue)
                        debugMessage &= "  - ⚠️ QuestionID صفر بود، از Index " & questionIndex & " استفاده شد" & vbCrLf
                    End If
                End If
            Next

            ' نمایش دیباگ
            ShowMessageBox("📊 تعداد کل: " & totalQuestions & " - پاسخ داده شده: " & answeredQuestions & vbCrLf & debugMessage, "info")

            If Not isValid Then
                Return
            End If

            If answers.Count = 0 Then
                ShowMessageBox("⚠️ هیچ پاسخی ثبت نشد! با وجود اینکه پاسخ داده‌اید.", "warning")
                Return
            End If

            ' 5. ذخیره در دیتابیس
            Dim saveCount As Integer = 0

            Try
                Using conn As New SqlConnection(ConnectionString)
                    conn.Open()

                    Dim tableCheck = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'trn_EvaluationResults'"
                    Using cmd As New SqlCommand(tableCheck, conn)
                        Dim exists As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                        If exists = 0 Then
                            ShowMessageBox("جدول trn_EvaluationResults در دیتابیس وجود ندارد!", "error")
                            Return
                        End If
                    End Using

                    Using trans = conn.BeginTransaction()
                        Try
                            For Each answer In answers
                                Dim query = "INSERT INTO trn_EvaluationResults " &
                                           "(RegistrationID, QuestionID, AnswerValue, EvaluationDate, UserID) " &
                                           "VALUES (@RegistrationID, @QuestionID, @AnswerValue, GETDATE(), @UserID)"

                                Using cmd As New SqlCommand(query, conn, trans)
                                    cmd.Parameters.AddWithValue("@RegistrationID", regID)
                                    cmd.Parameters.AddWithValue("@QuestionID", answer.Key)
                                    cmd.Parameters.AddWithValue("@AnswerValue", answer.Value)
                                    cmd.Parameters.AddWithValue("@UserID", UserID)
                                    saveCount += cmd.ExecuteNonQuery()
                                End Using
                            Next

                            If Not String.IsNullOrEmpty(txtComments.Text.Trim()) Then
                                Dim query = "INSERT INTO trn_EvaluationComments " &
                                           "(RegistrationID, Comment, CommentDate, UserID) " &
                                           "VALUES (@RegistrationID, @Comment, GETDATE(), @UserID)"

                                Using cmd As New SqlCommand(query, conn, trans)
                                    cmd.Parameters.AddWithValue("@RegistrationID", regID)
                                    cmd.Parameters.AddWithValue("@Comment", txtComments.Text.Trim())
                                    cmd.Parameters.AddWithValue("@UserID", UserID)
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If

                            Dim updateQuery = "UPDATE trn_CourseRegistrations SET " &
                                             "IsEvaluated = 1, EvaluationDate = GETDATE() " &
                                             "WHERE RegistrationID = @RegistrationID"

                            Using cmd As New SqlCommand(updateQuery, conn, trans)
                                cmd.Parameters.AddWithValue("@RegistrationID", regID)
                                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                                If rowsAffected = 0 Then
                                    Throw New Exception("ثبت‌نام با کد " & regID & " یافت نشد!")
                                End If
                            End Using

                            trans.Commit()

                            ShowMessageBox("✅ ارزیابی شما با موفقیت ثبت شد! (" & saveCount & " پاسخ ذخیره شد)", "success")

                            btnSubmit.Enabled = False
                            btnSubmit.Text = "✓ ارسال شده"

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
                            txtComments.Enabled = False

                        Catch ex As Exception
                            trans.Rollback()
                            Throw
                        End Try
                    End Using
                End Using

            Catch ex As Exception
                ShowMessageBox("❌ خطا در ذخیره: " & ex.Message, "error")
            End Try

        Catch ex As Exception
            ShowMessageBox("❌ خطا: " & ex.Message, "error")
        End Try
    End Sub

    ' ============================================================
    ' بررسی ارسال قبلی
    ' ============================================================
    Private Function CheckIfAlreadyEvaluated(registrationID As Integer) As Boolean
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT COUNT(*) FROM trn_EvaluationResults " &
                           "WHERE RegistrationID = @RegistrationID"

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
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("MyCourses.aspx")
    End Sub

End Class