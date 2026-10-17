Imports System.Data
Imports System.Data.SqlClient

Partial Class ExamReport
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("RegID")) Then
                Dim registrationID As Integer = Convert.ToInt32(Request.QueryString("RegID"))
                LoadReport(registrationID)
            Else
                pnlInfo.Visible = False
                pnlError.Visible = True
                lblError.Text = "❌ شناسه ثبت‌نام ارسال نشده است."
            End If
        End If
    End Sub

    ' ============================================================
    ' بارگذاری گزارش
    ' ============================================================
    Private Sub LoadReport(registrationID As Integer)
        Try
            Dim dt As New DataTable()
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT " &
                           "cr.RegistrationID, cr.CourseID, cr.UserID, cr.RegistrationDate, " &
                           "cr.IsConfirmed, cr.ConfirmedDate, cr.IsEvaluated, cr.EvaluationDate, " &
                           "p.nam, p.family, p.codePersoneli, " &
                           "p.codeMelli AS NationalCode, " &
                           "u.UnitsName, " &
                           "ec.Title AS CourseTitle, " &
                           "er.Score AS ExamScore, er.CorrectCount, er.TotalCount, er.IsPassed, er.ExamDate, " &
                           "eq.QuestionText, eo.OptionText AS AnswerText, " &
                           "CASE WHEN eo.IsCorrect = 1 THEN 1 ELSE 0 END AS IsCorrect " &
                           "FROM trn_CourseRegistrations cr " &
                           "INNER JOIN personel p ON cr.UserID = p.ID " &
                           "INNER JOIN tblUnits u ON p.UnitsCode = u.UnitsCode " &
                           "INNER JOIN trn_EducationalCourses ec ON cr.CourseID = ec.CourseID " &
                           "LEFT JOIN trn_ExamResults er ON cr.RegistrationID = er.RegistrationID " &
                           "LEFT JOIN trn_ExamAnswerDetails ed ON er.ResultID = ed.ResultID " &
                           "LEFT JOIN trn_ExamQuestions eq ON ed.QuestionID = eq.QuestionID " &
                           "LEFT JOIN trn_ExamOptions eo ON ed.OptionID = eo.OptionID " &
                           "WHERE cr.RegistrationID = @RegistrationID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                ' اطلاعات پرسنل و دوره
                lblPersonnel.Text = row("nam").ToString() & " " & row("family").ToString()
                lblCode.Text = row("codePersoneli").ToString()
                lblNationalCode.Text = row("NationalCode").ToString()
                lblUnit.Text = row("UnitsName").ToString()
                lblCourse.Text = row("CourseTitle").ToString()
                lblCourseTitle.Text = row("CourseTitle").ToString()
                lblRegDate.Text = Convert.ToDateTime(row("RegistrationDate")).ToString("yyyy/MM/dd HH:mm")
                lblStatus.Text = GetStatusText(row("IsConfirmed"))

                ' نتیجه آزمون
                If Not IsDBNull(row("ExamScore")) AndAlso row("ExamScore") IsNot Nothing Then
                    pnlExam.Visible = True
                    pnlNoExam.Visible = False

                    lblScore.Text = row("ExamScore").ToString() & "%"
                    lblCorrect.Text = row("CorrectCount").ToString() & " از " & row("TotalCount").ToString()
                    lblExamDate.Text = Convert.ToDateTime(row("ExamDate")).ToString("yyyy/MM/dd HH:mm")

                    If Convert.ToBoolean(row("IsPassed")) Then
                        lblExamStatus.Text = "<span class='badge bg-success'>✅ قبول شده</span>"
                    Else
                        lblExamStatus.Text = "<span class='badge bg-danger'>❌ رد شده</span>"
                    End If

                    ' پاسخ‌های داده شده
                    Dim answerRows As DataRow() = dt.Select("QuestionText IS NOT NULL")
                    If answerRows.Length > 0 Then
                        Dim answerDt As DataTable = dt.Clone()
                        For Each dr As DataRow In answerRows
                            answerDt.ImportRow(dr)
                        Next
                        rptAnswers.DataSource = answerDt
                        rptAnswers.DataBind()
                    End If
                Else
                    pnlExam.Visible = False
                    pnlNoExam.Visible = True
                End If

                ' وضعیت ارزیابی
                If Convert.ToBoolean(row("IsEvaluated")) Then
                    pnlEval.Visible = True
                    pnlNoEval.Visible = False
                    lblEvalDate.Text = Convert.ToDateTime(row("EvaluationDate")).ToString("yyyy/MM/dd HH:mm")
                Else
                    pnlEval.Visible = False
                    pnlNoEval.Visible = True
                End If

                pnlInfo.Visible = True
                pnlError.Visible = False
            Else
                pnlInfo.Visible = False
                pnlError.Visible = True
                lblError.Text = "❌ اطلاعاتی برای این ثبت‌نام یافت نشد."
            End If

        Catch ex As Exception
            pnlInfo.Visible = False
            pnlError.Visible = True
            lblError.Text = "❌ خطا در بارگذاری اطلاعات: " & ex.Message
        End Try
    End Sub

    ' ============================================================
    ' برگشت به صفحه قبل
    ' ============================================================
    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("index.aspx")
    End Sub

    ' ============================================================
    ' توابع کمکی
    ' ============================================================
    Private Function GetStatusText(isConfirmed As Object) As String
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

End Class