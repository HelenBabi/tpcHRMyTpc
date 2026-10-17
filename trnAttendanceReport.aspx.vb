Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Globalization

Partial Class trnAttendanceReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            LoadCourses()

        End If
    End Sub



#Region "بارگذاری داده‌ها"

    Private Sub LoadCourses()
        Try
            Dim sqlstr As String = "SELECT idd, onvan FROM trnBarnameRiziTbl ORDER BY idd DESC"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            cmbCourses.DataSource = dt
            cmbCourses.DataTextField = "onvan"
            cmbCourses.DataValueField = "idd"
            cmbCourses.DataBind()

            cmbCourses.Items.Insert(0, New ListItem("-- انتخاب دوره --", "0"))

        Catch ex As Exception
            lblMessage.Text = "خطا در بارگذاری دوره‌ها: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub



    Protected Sub btnShowReport_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadReport()
    End Sub

    Private Sub LoadReport()
        Try
            Dim courseId As String = cmbCourses.SelectedValue
        

    

            If courseId = "0" Then
                lblMessage.Text = "لطفاً یک دوره را انتخاب کنید."
                lblMessage.ForeColor = System.Drawing.Color.Red
                Return
            End If

            ' ============================================================
            ' کوئری گزارش حضور و غیاب - اصلاح شده
            ' ============================================================
            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    ROW_NUMBER() OVER (ORDER BY p.codepersoneli) AS RowNumber,")
            sql.AppendLine("    p.codepersoneli AS PersonCode,")
            sql.AppendLine("    p.nam + ' ' + p.family AS FullName,")
            sql.AppendLine("    u.UnitsName AS Unit,")
            sql.AppendLine("    ISNULL(o.omur, N'تعیین نشده') AS Position,")
            sql.AppendLine("    CONVERT(VARCHAR(10), a.CheckDate, 111) AS CheckDate,")
            sql.AppendLine("    a.CheckInTime,")
            sql.AppendLine("    a.CheckOutTime,")
            sql.AppendLine("    CASE ")
            sql.AppendLine("        WHEN a.CheckOutTime IS NOT NULL AND a.CheckInTime IS NOT NULL THEN ")
            sql.AppendLine("            CONVERT(VARCHAR(8), ")
            sql.AppendLine("                DATEADD(SECOND, ")
            sql.AppendLine("                    DATEDIFF(SECOND, ")
            sql.AppendLine("                        CAST(a.CheckInTime AS datetime), ")
            sql.AppendLine("                        CAST(a.CheckOutTime AS datetime) ")
            sql.AppendLine("                    ), ")
            sql.AppendLine("                    '00:00:00' ")
            sql.AppendLine("                ), ")
            sql.AppendLine("                108 ")
            sql.AppendLine("            ) ")
            sql.AppendLine("        ELSE NULL ")
            sql.AppendLine("    END AS Duration")
            sql.AppendLine("FROM (")
            sql.AppendLine("    SELECT DISTINCT PersonCode")
            sql.AppendLine("    FROM trnAttendanceTbl")
            sql.AppendLine("    WHERE CourseID = @CourseId")
            sql.AppendLine(") coursePersonnel")
            sql.AppendLine("INNER JOIN personel p ON coursePersonnel.PersonCode = p.codepersoneli")
            sql.AppendLine("LEFT JOIN (")
            sql.AppendLine("    SELECT")
            sql.AppendLine("        PersonCode,")
            sql.AppendLine("        CheckDate,")
            sql.AppendLine("        MIN(CheckInTime) AS CheckInTime,")
            sql.AppendLine("        MAX(CheckOutTime) AS CheckOutTime")
            sql.AppendLine("    FROM trnAttendanceTbl")
            sql.AppendLine("    WHERE CheckType = 'IN'")
            sql.AppendLine("    AND CourseID = @CourseId")
            sql.AppendLine("    GROUP BY PersonCode, CheckDate")
            sql.AppendLine(") a ON p.codepersoneli = a.PersonCode")
            sql.AppendLine("LEFT JOIN tblUnits u ON p.UnitsCode = u.UnitsCode")
            sql.AppendLine("LEFT JOIN omurTbl o ON p.omurID = o.omurID")
            sql.AppendLine("ORDER BY p.codepersoneli")

            Dim params As New Dictionary(Of String, Object)()
            params.Add("@CourseId", courseId)
      
            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString(), params)

            ' ============================================================
            ' تبدیل تاریخ به شمسی در کد (برای نمایش در GridView)
            ' ============================================================
            If dt.Rows.Count > 0 Then
                ' اضافه کردن ستون تاریخ شمسی
                dt.Columns.Add("PersianDate", GetType(String))

                For Each row As DataRow In dt.Rows
                    Dim checkDate As Object = row("CheckDate")
                    If checkDate IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(checkDate.ToString()) Then
                        Try
                            Dim dtDate As DateTime = Convert.ToDateTime(checkDate)
                            row("PersianDate") = ConvertGregorianToJalali(dtDate)
                        Catch
                            row("PersianDate") = row("CheckDate").ToString()
                        End Try
                    Else
                        row("PersianDate") = ""
                    End If
                Next

                ' حذف ستون CheckDate و جایگزینی با PersianDate
                dt.Columns.Remove("CheckDate")
                dt.Columns("PersianDate").ColumnName = "CheckDate"

                grdAttendance.DataSource = dt
                grdAttendance.DataBind()

                Dim total As Integer = dt.Rows.Count
                Dim present As Integer = 0
                Dim inClass As Integer = 0

                For Each row As DataRow In dt.Rows
                    Dim checkOut As Object = row("CheckOutTime")
                    Dim checkIn As Object = row("CheckInTime")

                    If checkOut IsNot DBNull.Value AndAlso checkIn IsNot DBNull.Value Then
                        present += 1
                    ElseIf checkIn IsNot DBNull.Value AndAlso checkOut Is DBNull.Value Then
                        inClass += 1
                    End If
                Next

                lblTotalCount.InnerText = total.ToString()
                lblPresentCount.InnerText = present.ToString()
                lblLateCount.InnerText = inClass.ToString()

                divTable.Visible = True
                divSummary.Visible = True
                divNoData.Visible = False
                lblMessage.Text = ""
            Else
                divTable.Visible = False
                divSummary.Visible = False
                divNoData.Visible = True
                lblMessage.Text = "هیچ داده‌ای برای این دوره یافت نشد."
                lblMessage.ForeColor = System.Drawing.Color.Orange
            End If

        Catch ex As Exception
            lblMessage.Text = "خطا در بارگذاری گزارش: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub
    ' ============================================================
    ' تبدیل تاریخ میلادی به شمسی
    ' ============================================================
    Private Function ConvertGregorianToJalali(ByVal gregorianDate As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Dim year As Integer = pc.GetYear(gregorianDate)
            Dim month As Integer = pc.GetMonth(gregorianDate)
            Dim day As Integer = pc.GetDayOfMonth(gregorianDate)
            Return year.ToString() & "/" & month.ToString().PadLeft(2, "0"c) & "/" & day.ToString().PadLeft(2, "0"c)
        Catch ex As Exception
            Return gregorianDate.ToString("yyyy/MM/dd")
        End Try
    End Function
    Private Sub ShowSummary(ByVal dt As DataTable)
        Try
            Dim total As Integer = dt.Rows.Count
            Dim present As Integer = 0
            Dim absent As Integer = 0
            Dim inClass As Integer = 0

            For Each row As DataRow In dt.Rows
                Dim status As String = row("Status").ToString()
                Select Case status
                    Case "حاضر"
                        present += 1
                    Case "در کلاس"
                        inClass += 1
                    Case "غایب"
                        absent += 1
                End Select
            Next

            lblTotalCount.InnerText = total.ToString()
            lblPresentCount.InnerText = present.ToString()

            lblLateCount.InnerText = inClass.ToString()

        Catch ex As Exception
        End Try
    End Sub

#End Region
    Protected Function GetStatusIcon(ByVal checkIn As Object, ByVal checkOut As Object) As String
        If checkIn IsNot DBNull.Value AndAlso checkOut IsNot DBNull.Value Then
            Return "✅ حاضر"
        ElseIf checkIn IsNot DBNull.Value AndAlso checkOut Is DBNull.Value Then
            Return "⏳ در کلاس"
        Else
            Return "❌ غایب"
        End If
    End Function
#Region "رویدادهای GridView"

    Protected Sub grdAttendance_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        grdAttendance.PageIndex = e.NewPageIndex
        LoadReport()
    End Sub

    Protected Function GetStatusClass(ByVal status As Object) As String
        Dim statusText As String = status.ToString()
        Select Case statusText
            Case "حاضر"
                Return "status-present"
            Case "در کلاس"
                Return "status-warning"
            Case "غایب"
                Return "status-absent"
            Case Else
                Return ""
        End Select
    End Function

#End Region

#Region "خروجی Excel"

    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            LoadReport()

            If grdAttendance.Rows.Count = 0 Then
                lblMessage.Text = "هیچ داده‌ای برای خروجی وجود ندارد."
                lblMessage.ForeColor = System.Drawing.Color.Red
                Return
            End If

            ' ============================================
            ' دریافت نام دوره از کمبوباکس
            ' ============================================
            Dim courseName As String = cmbCourses.SelectedItem.Text

            ' حذف کاراکترهای غیرمجاز از نام دوره
            Dim invalidChars As Char() = System.IO.Path.GetInvalidFileNameChars()
            For Each c As Char In invalidChars
                courseName = courseName.Replace(c.ToString(), "")
            Next

            ' اگر نام دوره خالی شد، از کد دوره استفاده کن
            If String.IsNullOrEmpty(courseName) Then
                courseName = "دوره_" & cmbCourses.SelectedValue
            End If

            ' ============================================
            ' ساخت نام فایل
            ' ============================================
            Dim fileName As String = "گزارش_حضور_" & courseName & ".xls"

            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)
            Response.Charset = "utf-8"
            Response.ContentEncoding = System.Text.Encoding.UTF8

            Dim sw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)

            sw.WriteLine("<html>")
            sw.WriteLine("<head>")
            sw.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>")
            sw.WriteLine("<style>")
            sw.WriteLine("body { font-family: 'B Nazanin', 'Tahoma', 'Arial', sans-serif; direction: rtl; }")
            sw.WriteLine("h2 { text-align: center; color: #333; }")
            sw.WriteLine("table { border-collapse: collapse; width: 100%; font-family: 'B Nazanin', 'Tahoma', sans-serif; }")
            sw.WriteLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: center; font-family: 'B Nazanin', 'Tahoma', sans-serif; }")
            sw.WriteLine("th { background-color: #4CAF50; color: white; font-weight: bold; }")
            sw.WriteLine("tr:nth-child(even) { background-color: #f2f2f2; }")
            sw.WriteLine("</style>")
            sw.WriteLine("</head>")
            sw.WriteLine("<body>")

            sw.WriteLine("<h2>📊 گزارش حضور و غیاب</h2>")
            sw.WriteLine("<h3 style='text-align:center;'>" & cmbCourses.SelectedItem.Text & "</h3>")

            sw.WriteLine("<p style='text-align:center;'>")
   

            grdAttendance.RenderControl(hw)

            sw.WriteLine("</body>")
            sw.WriteLine("</html>")

            Response.Write(sw.ToString())
            Response.End()

        Catch ex As Exception
            lblMessage.Text = "خطا در خروجی Excel: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub


    ' ============================================
    ' متد کمکی برای فرمت اعداد فارسی
    ' ============================================
    Private Function ConvertToPersianNumber(ByVal number As String) As String
        Try
            Dim persianDigits As String() = {"۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"}
            Dim result As String = number
            For i As Integer = 0 To 9
                result = result.Replace(i.ToString(), persianDigits(i))
            Next
            Return result
        Catch
            Return number
        End Try
    End Function
    ' ============================================
    ' متد VerifyRenderingInServerForm (برای اجازه رندر)
    ' ============================================
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' این متد برای جلوگیری از خطای رندر در خروجی اکسل لازم است
    End Sub

    Private Function GetCellText(ByVal cell As TableCell) As String
        Try
            If cell.Controls.Count > 0 Then
                For Each ctrl As Control In cell.Controls
                    If TypeOf ctrl Is Literal Then
                        Return DirectCast(ctrl, Literal).Text
                    ElseIf TypeOf ctrl Is Label Then
                        Return DirectCast(ctrl, Label).Text
                    ElseIf TypeOf ctrl Is DataBoundLiteralControl Then
                        Return DirectCast(ctrl, DataBoundLiteralControl).Text
                    End If
                Next
            End If
            Return cell.Text
        Catch
            Return cell.Text
        End Try
    End Function
    ' ============================================================
    ' دکمه برگشت به صفحه اصلی
    ' ============================================================
    Protected Sub btnBackToHome_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Response.Redirect("~/index.aspx")
        Catch ex As Exception
            lblMessage.Text = "خطا در بازگشت به صفحه اصلی: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub
#End Region

End Class