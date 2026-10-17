Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb

Partial Class ExamUpload
    Inherits System.Web.UI.Page

    Private ConnectionString As String = ClsConnection.GetConn
    Private UserID As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("userid") Is Nothing Then
                Response.Redirect("login_2.aspx")
            End If

            UserID = Convert.ToInt32(Session("userid"))

            If Not IsPostBack Then
                LoadCourses()
            End If
        Catch ex As Exception
            ShowAlert("خطا در بارگذاری صفحه: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadCourses()
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "SELECT CourseID, Title FROM trn_EducationalCourses WHERE IsActive = 1 ORDER BY Title"
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        ddlCourse.DataSource = dt
                        ddlCourse.DataTextField = "Title"
                        ddlCourse.DataValueField = "CourseID"
                        ddlCourse.DataBind()
                        ddlCourse.Items.Insert(0, New ListItem("-- انتخاب دوره --", "0"))
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowAlert("خطا در بارگذاری دوره‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دانلود قالب اکسل (با استفاده از HTML Table به جای EPPlus)
    ' ============================================================
    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs)
        Try
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=ExamQuestions_Template.xls")

            Dim html As New StringBuilder()
            html.AppendLine("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>")
            html.AppendLine("<head><meta charset='utf-8'><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>سوالات</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>")
            html.AppendLine("<body>")
            html.AppendLine("<table border='1'>")

            ' Header
            html.AppendLine("<tr>")
            html.AppendLine("<th style='background-color:#d9e1f2;font-weight:bold;'>QuestionText</th>")
            html.AppendLine("<th style='background-color:#d9e1f2;font-weight:bold;'>QuestionType</th>")
            html.AppendLine("<th style='background-color:#d9e1f2;font-weight:bold;'>Score</th>")
            html.AppendLine("<th style='background-color:#d9e1f2;font-weight:bold;'>Options</th>")
            html.AppendLine("<th style='background-color:#d9e1f2;font-weight:bold;'>CorrectAnswer</th>")
            html.AppendLine("</tr>")

            ' داده‌های نمونه
            Dim sampleData As String(,) = {
                {"ASP.NET Core در کدام پلتفرم اجرا می‌شود؟", "MultipleChoice", "2", ".NET Framework,.NET Core (Cross-platform),Java,PHP", ".NET Core (Cross-platform)"},
                {"کدام یک از موارد زیر یک فریم‌ورک سمت کلاینت است؟", "MultipleChoice", "2", "Angular,ASP.NET Core,Node.js,Django", "Angular"},
                {"کدام یک برای ارتباط با دیتابیس در ASP.NET Core استفاده می‌شود？", "MultipleChoice", "2", "Entity Framework Core,Dapper,NHibernate,ADO.NET", "Entity Framework Core"}
            }

            For i As Integer = 0 To sampleData.GetUpperBound(0)
                html.AppendLine("<tr>")
                html.AppendLine("<td>" & sampleData(i, 0) & "</td>")
                html.AppendLine("<td>" & sampleData(i, 1) & "</td>")
                html.AppendLine("<td>" & sampleData(i, 2) & "</td>")
                html.AppendLine("<td>" & sampleData(i, 3) & "</td>")
                html.AppendLine("<td>" & sampleData(i, 4) & "</td>")
                html.AppendLine("</tr>")
            Next

            html.AppendLine("</table>")
            html.AppendLine("</body></html>")

            Response.Write(html.ToString())
            Response.End()

        Catch ex As Exception
            ShowAlert("خطا در دانلود قالب: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری فایل اکسل (با استفاده از OleDb)
    ' ============================================================
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try

            Dim courseID As Integer = Convert.ToInt32(ddlCourse.SelectedValue)

            If courseID = 0 Then
                ShowAlert("لطفاً یک دوره را انتخاب کنید.", "warning")
                Exit Sub
            End If

            If Not fuExcel.HasFile Then
                ShowAlert("لطفاً یک فایل اکسل انتخاب کنید.", "warning")
                Exit Sub
            End If

            Dim extension As String = Path.GetExtension(fuExcel.FileName).ToLower()

            If extension <> ".xlsx" AndAlso extension <> ".xls" Then
                ShowAlert("فرمت فایل باید xls یا xlsx باشد.", "warning")
                Exit Sub
            End If

            If fuExcel.PostedFile.ContentLength > 5242880 Then
                ShowAlert("حجم فایل نباید بیشتر از 5 مگابایت باشد.", "warning")
                Exit Sub
            End If

            ' ذخیره موقت با حفظ پسوند
            Dim tempPath As String = Path.Combine( _
                Path.GetTempPath(), _
                Guid.NewGuid().ToString() & extension)

            fuExcel.SaveAs(tempPath)

            Try

                Dim dt As DataTable = ReadExcelFile(tempPath)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    ShowAlert("هیچ داده‌ای در فایل یافت نشد.", "warning")
                    Exit Sub
                End If

                DeleteExistingQuestions(courseID)

                Dim totalQuestions As Integer = 0
                Dim totalOptions As Integer = 0

                For Each row As DataRow In dt.Rows

                    If row.ItemArray.Length < 5 Then Continue For

                    Dim questionText As String = row(0).ToString.Trim
                    Dim questionType As String = row(1).ToString.Trim
                    Dim score As Integer = 1

                    Integer.TryParse(row(2).ToString, score)

                    Dim optionsText As String = row(3).ToString.Trim
                    Dim correctAnswer As String = row(4).ToString.Trim

                    If String.IsNullOrWhiteSpace(questionText) Then
                        Continue For
                    End If

                    Dim questionID As Integer =
                        SaveQuestion(courseID,
                                     questionText,
                                     questionType,
                                     score,
                                     totalQuestions + 1)

                    If questionID <= 0 Then
                        Throw New Exception("خطا در ذخیره سوال: " & questionText)
                    End If

                    totalQuestions += 1

                    If Not String.IsNullOrEmpty(optionsText) Then

                        Dim options() As String = optionsText.Split(","c)

                        For i As Integer = 0 To options.Length - 1

                            Dim optionText As String = options(i).Trim()

                            If optionText <> "" Then

                                Dim isCorrect As Boolean =
                                    optionText.Equals(correctAnswer,
                                                      StringComparison.OrdinalIgnoreCase)

                                If SaveOption(questionID,
                                              optionText,
                                              i + 1,
                                              isCorrect) Then

                                    totalOptions += 1

                                End If

                            End If

                        Next

                    End If

                Next

                pnlResult.Visible = True

                lblResult.Text =
                    "✅ بارگذاری با موفقیت انجام شد<br/>" &
                    "📝 تعداد سوالات: <b>" & totalQuestions & "</b><br/>" &
                    "🔘 تعداد گزینه‌ها: <b>" & totalOptions & "</b><br/>" &
                    "📚 دوره: <b>" & ddlCourse.SelectedItem.Text & "</b>"

                lblResult.CssClass = "success"

                ShowAlert("بارگذاری با موفقیت انجام شد.", "success")

            Catch ex As Exception

                ShowAlert("خطا در پردازش فایل: " & ex.ToString(), "danger")

            Finally

                If File.Exists(tempPath) Then
                    File.Delete(tempPath)
                End If

            End Try

        Catch ex As Exception

            ShowAlert("خطای سیستم: " & ex.ToString(), "danger")

        End Try

    End Sub

    ' ============================================================
    ' خواندن فایل اکسل با OleDb
    ' ============================================================
    Private Function ReadExcelFile(filePath As String) As DataTable
        Dim dt As New DataTable()
        Try
            Dim connString As String = ""
            Dim extension = Path.GetExtension(filePath).ToLower()

            If extension = ".xlsx" Then
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties='Excel 12.0 Xml;HDR=YES;'"
            Else
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties='Excel 8.0;HDR=YES;'"
            End If

            Using conn As New OleDbConnection(connString)
                conn.Open()
                Dim cmd = New OleDbCommand("SELECT * FROM [سوالات$]", conn)
                Using adapter As New OleDbDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using

            Return dt
        Catch ex As Exception
            ' اگر Sheet با نام "سوالات" پیدا نشد، سعی کنید Sheet اول را بخوانید
            Try
                Dim connString As String = ""
                Dim extension = Path.GetExtension(filePath).ToLower()

                If extension = ".xlsx" Then
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties='Excel 12.0 Xml;HDR=YES;'"
                Else
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties='Excel 8.0;HDR=YES;'"
                End If

                Using conn As New OleDbConnection(connString)
                    conn.Open()
                    ' دریافت نام اولین Sheet
                    Dim dtSchema As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    Dim sheetName As String = dtSchema.Rows(0)("TABLE_NAME").ToString()

                    Dim cmd = New OleDbCommand("SELECT * FROM [" & sheetName & "]", conn)
                    Using adapter As New OleDbDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using

                Return dt
            Catch ex2 As Exception
                Throw New Exception("خطا در خواندن فایل اکسل: " & ex2.Message)
            End Try
        End Try
    End Function

    ' ============================================================
    ' حذف سوالات قبلی
    ' ============================================================
    Private Sub DeleteExistingQuestions(courseID As Integer)
        Try
            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        ' حذف گزینه‌ها
                        Dim deleteOptions = "DELETE FROM trn_ExamOptions WHERE QuestionID IN (SELECT QuestionID FROM trn_ExamQuestions WHERE CourseID = @CourseID)"
                        Using cmd As New SqlCommand(deleteOptions, conn, trans)
                            cmd.Parameters.AddWithValue("@CourseID", courseID)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' حذف سوالات
                        Dim deleteQuestions = "DELETE FROM trn_ExamQuestions WHERE CourseID = @CourseID"
                        Using cmd As New SqlCommand(deleteQuestions, conn, trans)
                            cmd.Parameters.AddWithValue("@CourseID", courseID)
                            cmd.ExecuteNonQuery()
                        End Using

                        trans.Commit()
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("خطا در حذف سوالات قبلی: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ذخیره سوال
    ' ============================================================
    Private Function SaveQuestion(courseID As Integer, questionText As String, questionType As String, score As Integer, order As Integer) As Integer
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "INSERT INTO trn_ExamQuestions (CourseID, QuestionText, QuestionType, Score, [Order], IsActive) " &
                           "VALUES (@CourseID, @QuestionText, @QuestionType, @Score, @Order, 1); SELECT SCOPE_IDENTITY()"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseID", courseID)
                    cmd.Parameters.AddWithValue("@QuestionText", questionText)
                    cmd.Parameters.AddWithValue("@QuestionType", If(String.IsNullOrEmpty(questionType), "MultipleChoice", questionType))
                    cmd.Parameters.AddWithValue("@Score", If(score = 0, 1, score))
                    cmd.Parameters.AddWithValue("@Order", order)
                    conn.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' ============================================================
    ' ذخیره گزینه
    ' ============================================================
    Private Function SaveOption(questionID As Integer, optionText As String, order As Integer, isCorrect As Boolean) As Boolean
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim query = "INSERT INTO trn_ExamOptions (QuestionID, OptionText, [Order], IsCorrect) " &
                           "VALUES (@QuestionID, @OptionText, @Order, @IsCorrect)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@QuestionID", questionID)
                    cmd.Parameters.AddWithValue("@OptionText", optionText)
                    cmd.Parameters.AddWithValue("@Order", order)
                    cmd.Parameters.AddWithValue("@IsCorrect", isCorrect)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' نمایش نتیجه
    ' ============================================================
    Private Sub ShowResult(message As String, type As String)
        pnlResult.Visible = True
        lblResult.Text = message
        Select Case type
            Case "success"
                pnlResult.CssClass = "result-box"
                lblResult.CssClass = "success"
            Case "danger"
                pnlResult.CssClass = "result-box"
                lblResult.CssClass = "error"
            Case "warning"
                pnlResult.CssClass = "result-box"
                lblResult.CssClass = "warning"
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
            Case "warning"
                pnlAlert.CssClass = "alert alert-warning alert-dismissible fade show"
            Case Else
                pnlAlert.CssClass = "alert alert-info alert-dismissible fade show"
        End Select
    End Sub

End Class