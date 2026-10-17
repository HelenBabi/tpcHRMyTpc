Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Partial Class trnEvaluationReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                LoadCourses()
                LoadTeachers()
                LoadEvaluations()
                LoadSummary()
            Catch ex As Exception
                lblTotalEvaluations.InnerText = "خطا: " & ex.Message
            End Try
        End If
    End Sub

#Region "بارگذاری داده‌ها"

    Private Sub LoadCourses()
        Try
            Dim sqlstr As String = "SELECT idd, onvan FROM trnBarnameRiziTbl WHERE idd > 0 ORDER BY onvan"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            ddlCourse.DataSource = dt
            ddlCourse.DataTextField = "onvan"
            ddlCourse.DataValueField = "idd"
            ddlCourse.DataBind()

            ddlCourse.Items.Insert(0, New ListItem("همه دوره‌ها", "0"))
        Catch ex As Exception
            ' خطا نادیده گرفته شود
        End Try
    End Sub

    Private Sub LoadTeachers()
        Try
            ' استفاده از COLLATE برای نمایش درست فارسی
            Dim sqlstr As String = "SELECT DISTINCT " & _
                                   "    c.modares COLLATE Persian_100_CI_AS AS modares " & _
                                   "FROM trnBarnameRiziTbl c " & _
                                   "WHERE c.modares IS NOT NULL AND c.modares <> '' " & _
                                   "ORDER BY c.modares COLLATE Persian_100_CI_AS"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            ddlTeacher.DataSource = dt
            ddlTeacher.DataTextField = "modares"
            ddlTeacher.DataValueField = "modares"
            ddlTeacher.DataBind()

            ddlTeacher.Items.Insert(0, New ListItem("همه مدرسین", "0"))
        Catch ex As Exception
            ' خطا نادیده گرفته شود
        End Try
    End Sub

    Private Sub LoadEvaluations()
        Try
            Dim sqlstr As String = BuildQuery()
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            ' اضافه کردن شماره ردیف
            dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            gvEvaluations.DataSource = dt
            gvEvaluations.DataBind()

            ' به‌روزرسانی خلاصه
            LoadSummary()

        Catch ex As Exception
            lblTotalEvaluations.InnerText = "خطا: " & ex.Message
        End Try
    End Sub

    Private Function BuildQuery() As String
        Dim query As New StringBuilder()

        query.AppendLine("SELECT ")
        query.AppendLine("    e.ID,")
        query.AppendLine("    e.PersonCode,")
        ' استفاده از nam و family برای نام کامل
        query.AppendLine("    ISNULL(p.nam + ' ' + p.family, CAST(e.PersonCode AS NVARCHAR(50))) AS PersonName,")
        query.AppendLine("    ISNULL(c.onvan, 'دوره حذف شده') AS CourseName,")
        ' استفاده از COLLATE برای نمایش درست فارسی
        query.AppendLine("    ISNULL(c.modares COLLATE Persian_100_CI_AS, 'نامشخص') AS TeacherName,")
        query.AppendLine("    e.TotalScore,")
        query.AppendLine("    e.Eval1, e.Eval2, e.Eval3, e.Eval4, e.Eval5, e.Eval6, e.Eval7, e.Eval8,")
        query.AppendLine("    e.Suggestions,")
        query.AppendLine("    e.CreateDate,")
        query.AppendLine("    dbo.ConvertGregorianToJalali(e.CreateDate) AS PersianDate")
        query.AppendLine("FROM trnCourseEvaluationTbl e")
        query.AppendLine("LEFT JOIN personel p ON e.PersonCode = p.codePersoneli")
        query.AppendLine("LEFT JOIN trnBarnameRiziTbl c ON e.CourseID = c.idd")
        query.AppendLine("WHERE 1=1")

        ' فیلتر بر اساس دوره
        If ddlCourse.SelectedValue <> "0" Then
            query.AppendLine("    AND e.CourseID = " & ddlCourse.SelectedValue)
        End If

        ' فیلتر بر اساس مدرس - استفاده از COLLATE
        If ddlTeacher.SelectedValue <> "0" Then
            query.AppendLine("    AND c.modares COLLATE Persian_100_CI_AS = N'" & ddlTeacher.SelectedValue.Replace("'", "''") & "'")
        End If

        ' فیلتر بر اساس تاریخ
        If Not String.IsNullOrEmpty(txtDateFrom.Text) Then
            query.AppendLine("    AND CAST(e.CreateDate AS DATE) >= '" & txtDateFrom.Text & "'")
        End If
        If Not String.IsNullOrEmpty(txtDateTo.Text) Then
            query.AppendLine("    AND CAST(e.CreateDate AS DATE) <= '" & txtDateTo.Text & "'")
        End If

        query.AppendLine("ORDER BY e.CreateDate DESC")

        Return query.ToString()
    End Function

    Private Sub LoadSummary()
        Try
            Dim sqlstr As String = "SELECT " & _
                                   "    COUNT(*) AS TotalEvaluations, " & _
                                   "    ISNULL(AVG(CAST(TotalScore AS FLOAT)), 0) AS AverageScore, " & _
                                   "    SUM(CASE WHEN TotalScore >= 80 THEN 1 ELSE 0 END) AS HighScore, " & _
                                   "    SUM(CASE WHEN TotalScore < 40 THEN 1 ELSE 0 END) AS LowScore, " & _
                                   "    COUNT(DISTINCT CourseID) AS TotalCourses " & _
                                   "FROM trnCourseEvaluationTbl"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                lblTotalEvaluations.InnerText = row("TotalEvaluations").ToString()

                Dim avgScore As Decimal = 0
                If Not IsDBNull(row("AverageScore")) Then
                    avgScore = Convert.ToDecimal(row("AverageScore"))
                End If
                lblAverageScore.InnerText = avgScore.ToString("F1")

                lblHighScore.InnerText = row("HighScore").ToString()
                lblLowScore.InnerText = row("LowScore").ToString()
                lblTotalCourses.InnerText = row("TotalCourses").ToString()
            End If
        Catch ex As Exception
            lblTotalEvaluations.InnerText = "خطا در محاسبه آمار"
        End Try
    End Sub

#End Region

#Region "رویدادها"

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilter.Click
        LoadEvaluations()
        LoadSummary()
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        ddlCourse.SelectedValue = "0"
        ddlTeacher.SelectedValue = "0"
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        LoadEvaluations()
        LoadSummary()
    End Sub

    Protected Sub gvEvaluations_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvEvaluations.PageIndexChanging
        gvEvaluations.PageIndex = e.NewPageIndex
        LoadEvaluations()
    End Sub

    Protected Sub lnkDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lnk As LinkButton = DirectCast(sender, LinkButton)
            Dim evalId As Integer = Convert.ToInt32(lnk.CommandArgument)

            Dim sqlstr As String = "SELECT " & _
                                   "    e.*, " & _
                                   "    ISNULL(p.nam + ' ' + p.family, CAST(e.PersonCode AS NVARCHAR(50))) AS PersonName, " & _
                                   "    ISNULL(c.onvan, 'دوره حذف شده') AS CourseName, " & _
                                   "    ISNULL(c.modares COLLATE Persian_100_CI_AS, 'نامشخص') AS TeacherName, " & _
                                   "    dbo.ConvertGregorianToJalali(e.CreateDate) AS PersianDate " & _
                                   "FROM trnCourseEvaluationTbl e " & _
                                   "LEFT JOIN personel p ON e.PersonCode = p.codePersoneli " & _
                                   "LEFT JOIN trnBarnameRiziTbl c ON e.CourseID = c.idd " & _
                                   "WHERE e.ID = " & evalId

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim sb As New StringBuilder()

                sb.AppendLine("<div style='direction:rtl;'>")
                sb.AppendLine("<table style='width:100%; border-collapse:collapse;'>")
                sb.AppendLine("<tr><td style='padding:8px; font-weight:bold;'>کد پرسنلی:</td><td style='padding:8px;'>" & row("PersonCode").ToString() & "</td></tr>")
                sb.AppendLine("<tr><td style='padding:8px; font-weight:bold;'>نام پرسنل:</td><td style='padding:8px;'>" & row("PersonName").ToString() & "</td></tr>")
                sb.AppendLine("<tr><td style='padding:8px; font-weight:bold;'>عنوان دوره:</td><td style='padding:8px;'>" & row("CourseName").ToString() & "</td></tr>")
                sb.AppendLine("<tr><td style='padding:8px; font-weight:bold;'>مدرس:</td><td style='padding:8px;'>" & row("TeacherName").ToString() & "</td></tr>")
                sb.AppendLine("<tr><td style='padding:8px; font-weight:bold;'>تاریخ ارزیابی:</td><td style='padding:8px;'>" & row("PersianDate").ToString() & "</td></tr>")
                sb.AppendLine("</table>")

                sb.AppendLine("<hr style='margin:15px 0;' />")
                sb.AppendLine("<h4 style='color:#2c3e50;'>امتیازات:</h4>")
                sb.AppendLine("<table style='width:100%; border-collapse:collapse;'>")
                sb.AppendLine("<tr style='background:#f8f9fa;'>")
                sb.AppendLine("<th style='padding:8px; border:1px solid #ddd; text-align:center;'>سوال</th>")
                sb.AppendLine("<th style='padding:8px; border:1px solid #ddd; text-align:center;'>امتیاز</th>")
                sb.AppendLine("<th style='padding:8px; border:1px solid #ddd; text-align:center;'>حداکثر</th>")
                sb.AppendLine("</tr>")

                Dim evalQuestions As String() = {"تناسب دوره با نیاز شغلی", "ارزش کاربردی مطالب", "افزایش مهارت‌های شغلی", "تنظیم محتوا براساس نیاز", "دانش فنی مدرس", "مشارکت دهی و پاسخگویی", "قدرت بیان و تسلط", "اشتراک تجارب"}
                Dim evalMax As Integer() = {10, 10, 15, 15, 10, 10, 15, 15}
                Dim evalValues As Integer() = {
                    Convert.ToInt32(row("Eval1")),
                    Convert.ToInt32(row("Eval2")),
                    Convert.ToInt32(row("Eval3")),
                    Convert.ToInt32(row("Eval4")),
                    Convert.ToInt32(row("Eval5")),
                    Convert.ToInt32(row("Eval6")),
                    Convert.ToInt32(row("Eval7")),
                    Convert.ToInt32(row("Eval8"))
                }

                For i As Integer = 0 To 7
                    sb.AppendLine("<tr>")
                    sb.AppendLine("<td style='padding:8px; border:1px solid #ddd;'>" & (i + 1) & ". " & evalQuestions(i) & "</td>")
                    sb.AppendLine("<td style='padding:8px; border:1px solid #ddd; text-align:center; font-weight:bold; color:#3498db;'>" & evalValues(i) & "</td>")
                    sb.AppendLine("<td style='padding:8px; border:1px solid #ddd; text-align:center;'>" & evalMax(i) & "</td>")
                    sb.AppendLine("</tr>")
                Next

                sb.AppendLine("<tr style='background:#e8f4fd;'>")
                sb.AppendLine("<td style='padding:8px; border:1px solid #ddd; font-weight:bold;'>جمع کل</td>")
                sb.AppendLine("<td style='padding:8px; border:1px solid #ddd; text-align:center; font-weight:bold; font-size:18px; color:#27ae60;'>" & row("TotalScore").ToString() & "</td>")
                sb.AppendLine("<td style='padding:8px; border:1px solid #ddd; text-align:center;'>90</td>")
                sb.AppendLine("</tr>")
                sb.AppendLine("</table>")

                If Not String.IsNullOrEmpty(row("Suggestions").ToString()) Then
                    sb.AppendLine("<hr style='margin:15px 0;' />")
                    sb.AppendLine("<h4 style='color:#2c3e50;'>💡 پیشنهادات:</h4>")
                    sb.AppendLine("<p style='background:#f8f9fa; padding:10px; border-radius:8px;'>" & row("Suggestions").ToString() & "</p>")
                End If

                sb.AppendLine("</div>")

                modalContent.InnerHtml = sb.ToString()

                ' نمایش مودال با جاوااسکریپت
                ClientScript.RegisterStartupScript(Me.GetType(), "OpenModal", "openModal();", True)
            End If

        Catch ex As Exception
            modalContent.InnerHtml = "خطا در نمایش جزئیات: " & ex.Message
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/index.aspx")
    End Sub

#End Region

#Region "توابع کمکی"

    Public Function GetStatusClass(ByVal score As Object) As String
        Dim totalScore As Integer = Convert.ToInt32(score)
        If totalScore >= 80 Then
            Return "high"
        ElseIf totalScore >= 60 Then
            Return "medium"
        Else
            Return "low"
        End If
    End Function

    Public Function GetStatusText(ByVal score As Object) As String
        Dim totalScore As Integer = Convert.ToInt32(score)
        If totalScore >= 80 Then
            Return "عالی"
        ElseIf totalScore >= 60 Then
            Return "متوسط"
        Else
            Return "ضعیف"
        End If
    End Function

    ''' <summary>
    ''' تبدیل تاریخ میلادی به شمسی با استفاده از تابع دیتابیس
    ''' </summary>
    Protected Function FormatPersianDate(ByVal dateValue As Object) As String
        Try
            If dateValue Is Nothing OrElse IsDBNull(dateValue) Then
                Return ""
            End If

            Dim dt As DateTime = Convert.ToDateTime(dateValue)

            ' استفاده از تابع دیتابیس برای تبدیل
            Dim sqlstr As String = "SELECT dbo.ConvertGregorianToJalali(@DateValue) AS PersianDate"
            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.AddWithValue("@DateValue", dt)
                    cnn.Open()
                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return result.ToString()
                    End If
                End Using
            End Using

            ' در صورت خطا، تاریخ میلادی برگردانده شود
            Return dt.ToString("yyyy/MM/dd HH:mm")
        Catch ex As Exception
            Return dateValue.ToString()
        End Try
    End Function

#End Region

End Class