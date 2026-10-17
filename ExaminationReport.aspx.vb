Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text

Partial Class ExaminationReport
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"
    Private _personCode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("codePersoneli") Is Nothing Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        _personCode = Session("codePersoneli").ToString().Trim()

        If String.IsNullOrEmpty(_personCode) Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        If Not IsPostBack Then
            LoadYears()
            LoadUnits()
            LoadReport()
        End If

    End Sub

    ' ============================================================
    ' بارگذاری سال‌های شمسی
    ' ============================================================
    Private Sub LoadYears()

        Try

            ddlYear.Items.Clear()

            ' سال جاری شمسی
            Dim currentYear As Integer = GetPersianYear(DateTime.Now)

            ' از 10 سال قبل تا سال جاری
            For i As Integer = currentYear - 10 To currentYear
                ddlYear.Items.Add(New ListItem(i.ToString(), i.ToString()))
            Next

            ' گزینه "همه"
            ddlYear.Items.Add(New ListItem("همه", "0"))

            ' انتخاب سال جاری
            ddlYear.SelectedValue = currentYear.ToString()

        Catch ex As Exception

            ShowMessage("❌ خطا در بارگذاری سال‌ها: " & ex.Message, "danger")

        End Try

    End Sub

    ' ============================================================
    ' دریافت سال شمسی
    ' ============================================================
    Private Function GetPersianYear(dateTime As DateTime) As Integer

        Try

            Dim persianCalendar As New System.Globalization.PersianCalendar()
            Return persianCalendar.GetYear(dateTime)

        Catch ex As Exception

            Return dateTime.Year - 621

        End Try

    End Function

    ' ============================================================
    ' تبدیل تاریخ میلادی به شمسی
    ' ============================================================
    Private Function ConvertToPersianDate(dateValue As Object) As String

        If dateValue Is DBNull.Value OrElse dateValue Is Nothing Then
            Return ""
        End If

        Try

            Dim dt As DateTime = Convert.ToDateTime(dateValue)
            Dim persianCalendar As New System.Globalization.PersianCalendar()

            Dim year As Integer = persianCalendar.GetYear(dt)
            Dim month As Integer = persianCalendar.GetMonth(dt)
            Dim day As Integer = persianCalendar.GetDayOfMonth(dt)

            Return String.Format("{0:0000}/{1:00}/{2:00}", year, month, day)

        Catch ex As Exception

            Return ""

        End Try

    End Function

    ' ============================================================
    ' بارگذاری واحدها
    ' ============================================================
    Private Sub LoadUnits()

        Try

            Dim sqlstr As String =
                "SELECT UnitsCode, UnitsName " &
                "FROM tblUnits " &
                "ORDER BY UnitsName"

            Using cnn As New SqlConnection(ClsConnection.GetConn)

                Using cmd As New SqlCommand(sqlstr, cnn)

                    cnn.Open()

                    Using reader As SqlDataReader = cmd.ExecuteReader()

                        ddlUnit.Items.Clear()
                        ddlUnit.Items.Add(New ListItem("همه", "0"))

                        While reader.Read()

                            ddlUnit.Items.Add(
                                New ListItem(
                                    reader("UnitsName").ToString(),
                                    reader("UnitsCode").ToString()
                                )
                            )

                        End While

                    End Using

                End Using

            End Using

        Catch ex As Exception

            ShowMessage("❌ خطا در بارگذاری واحدها: " & ex.Message, "danger")

        End Try

    End Sub

    ' ============================================================
    ' بارگذاری گزارش
    ' ============================================================
    Private Sub LoadReport()

        Try

            Dim yearFilter As String = ddlYear.SelectedValue
            Dim statusFilter As String = ddlStatus.SelectedValue
            Dim unitFilter As String = ddlUnit.SelectedValue

            Dim sqlstr As New StringBuilder()

            sqlstr.AppendLine("SELECT")
            sqlstr.AppendLine("    r.ID,")
            sqlstr.AppendLine("    r.PersonCode,")
            sqlstr.AppendLine("    ISNULL(p.codeMelli, N'') AS NationalCode,")
            sqlstr.AppendLine("    r.FullName,")
            sqlstr.AppendLine("    r.UnitName,")
            sqlstr.AppendLine("    ISNULL(r.SupervisorName, N'-') AS SupervisorName,")   ' ← نام سرپرست
            sqlstr.AppendLine("    r.ExaminationDate,")
            sqlstr.AppendLine("    dbo.FormatDateFa(r.ExaminationDate) AS PersianDate,")
            sqlstr.AppendLine("    r.StatusCode,")
            sqlstr.AppendLine("    r.StatusDescription,")
            sqlstr.AppendLine("    r.SupervisorComment,")
            sqlstr.AppendLine("    r.HealthExpertComment,")
            sqlstr.AppendLine("    r.CreateDate,")
            sqlstr.AppendLine("    r.SupervisorApproveDate AS SupervisorApproveDate_Miladi,")
            sqlstr.AppendLine("    r.HealthExpertApproveDate AS HealthExpertApproveDate_Miladi,")
            sqlstr.AppendLine("    r.CurrentLevel,")
            sqlstr.AppendLine("    CASE")
            sqlstr.AppendLine("        WHEN r.StatusCode = 1 THEN N'درخواست پرسنل'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 2 THEN N'تایید رئیس'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 3 THEN N'در انتظار تایید کارشناس'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 4 THEN N'تایید کارشناس بهداشت'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 6 THEN N'رد شده'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 7 THEN N'انجام شده'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 8 THEN N'ابطال شده توسط کارشناس'")
            sqlstr.AppendLine("        ELSE N'نامشخص'")
            sqlstr.AppendLine("    END AS StatusText,")
            sqlstr.AppendLine("    CASE")
            sqlstr.AppendLine("        WHEN r.StatusCode = 1 THEN 'pending'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 2 THEN 'approved'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 3 THEN 'pending'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 4 THEN 'approved'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 6 THEN 'rejected'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 7 THEN 'done'")
            sqlstr.AppendLine("        WHEN r.StatusCode = 8 THEN 'canceled'")
            sqlstr.AppendLine("        ELSE 'pending'")
            sqlstr.AppendLine("    END AS StatusClass,")
            sqlstr.AppendLine("    dbo.FormatDateFa(r.CreateDate) AS RequestDate")
            sqlstr.AppendLine("FROM exmReservationRequests r")
            sqlstr.AppendLine("LEFT JOIN personel p ON r.PersonCode = p.codePersoneli")
            sqlstr.AppendLine("WHERE 1=1")

            ' فیلتر سال (بر اساس تاریخ شمسی)
            If yearFilter <> "0" Then
                sqlstr.AppendLine("    AND YEAR(r.ExaminationDate) = " & GetGregorianYear(yearFilter))
            End If

            ' فیلتر وضعیت
            If statusFilter <> "0" Then
                sqlstr.AppendLine("    AND r.StatusCode = " & statusFilter)
            End If

            ' فیلتر واحد
            If unitFilter <> "0" Then
                sqlstr.AppendLine("    AND r.UnitCode = " & unitFilter)
            End If

            sqlstr.AppendLine("ORDER BY r.CreateDate DESC")

            Using cnn As New SqlConnection(ClsConnection.GetConn)

                Using cmd As New SqlCommand(sqlstr.ToString(), cnn)

                    cmd.CommandTimeout = 60

                    cnn.Open()

                    Dim dt As New DataTable()

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                    ' اضافه کردن ستون‌های تاریخ شمسی (با نام‌های جدید)
                    dt.Columns.Add("SupervisorApproveDate", GetType(String))
                    dt.Columns.Add("HealthExpertApproveDate", GetType(String))

                    ' تبدیل تاریخ‌ها به شمسی
                    For Each row As DataRow In dt.Rows

                        ' تاریخ تایید رئیس
                        Dim supervisorDate As Object = row("SupervisorApproveDate_Miladi")
                        If supervisorDate IsNot DBNull.Value AndAlso supervisorDate IsNot Nothing Then
                            row("SupervisorApproveDate") = ConvertToPersianDate(supervisorDate)
                        Else
                            row("SupervisorApproveDate") = ""
                        End If

                        ' تاریخ تایید کارشناس
                        Dim healthDate As Object = row("HealthExpertApproveDate_Miladi")
                        If healthDate IsNot DBNull.Value AndAlso healthDate IsNot Nothing Then
                            row("HealthExpertApproveDate") = ConvertToPersianDate(healthDate)
                        Else
                            row("HealthExpertApproveDate") = ""
                        End If

                    Next

                    ' شماره ردیف
                    dt.Columns.Add("RowNumber", GetType(Integer))

                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("RowNumber") = i + 1
                    Next

                    gvReport.DataSource = dt
                    gvReport.DataBind()

                    ' نمایش آمار
                    UpdateStatistics(dt)

                End Using

            End Using

        Catch ex As Exception

            ShowMessage("❌ خطا در بارگذاری گزارش: " & ex.Message, "danger")

        End Try

    End Sub

    ' ============================================================
    ' تبدیل سال شمسی به میلادی
    ' ============================================================
    Private Function GetGregorianYear(persianYear As String) As Integer

        Try

            Dim pYear As Integer = Convert.ToInt32(persianYear)

            ' تبدیل سال شمسی به میلادی با استفاده از تاریخ 1 فروردین
            Dim persianCalendar As New System.Globalization.PersianCalendar()

            Dim persianDate As New DateTime(pYear, 1, 1, persianCalendar)

            Return persianDate.Year

        Catch ex As Exception

            ' اگر تبدیل ناموفق بود، همان سال را برگردان
            Return Convert.ToInt32(persianYear) + 621

        End Try

    End Function

    ' ============================================================
    ' به‌روزرسانی آمار
    ' ============================================================
    Private Sub UpdateStatistics(dt As DataTable)

        Try

            Dim totalCount As Integer = dt.Rows.Count

            Dim pendingCount As Integer = 0
            Dim approvedCount As Integer = 0
            Dim rejectedCount As Integer = 0

            For Each row As DataRow In dt.Rows

                Dim statusCode As Integer = Convert.ToInt32(row("StatusCode"))

                ' در انتظار تایید: وضعیت 1 (درخواست پرسنل) و 3 (در انتظار کارشناس)
                If statusCode = 1 OrElse statusCode = 3 Then
                    pendingCount += 1
                    ' تایید شده: وضعیت 2 (تایید رئیس) و 4 (تایید کارشناس)
                ElseIf statusCode = 2 OrElse statusCode = 4 Then
                    approvedCount += 1
                    ' رد شده: وضعیت 6
                ElseIf statusCode = 6 Then
                    rejectedCount += 1
                End If

            Next

            lblTotalRequests.Text = totalCount.ToString()
            lblPendingRequests.Text = pendingCount.ToString()
            lblApprovedRequests.Text = approvedCount.ToString()
            lblRejectedRequests.Text = rejectedCount.ToString()

        Catch ex As Exception

            ' خطا در آمار را نادیده بگیر

        End Try

    End Sub

    ' ============================================================
    ' تغییر سال
    ' ============================================================
    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadReport()
    End Sub

    ' ============================================================
    ' جستجو
    ' ============================================================
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadReport()
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvReport_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvReport.PageIndex = e.NewPageIndex
        LoadReport()
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/index.aspx")
    End Sub

    ' ============================================================
    ' خروجی اکسل (فقط GridView)
    ' ============================================================
    Protected Sub btnExportExcel_Click(sender As Object, e As EventArgs)

        Try

            ' حذف صفحه‌بندی برای خروجی کامل
            gvReport.AllowPaging = False

            ' بارگذاری مجدد داده‌ها
            LoadReport()

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=ExaminationReport.xls")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"

            ' فقط GridView رو رندر کن
            Dim sw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)

            ' فقط GridView رو رندر کن (نه کل صفحه)
            gvReport.RenderControl(hw)

            ' خروجی رو بنویس
            Response.Write(sw.ToString())
            Response.End()

        Catch ex As Exception

            ShowMessage("❌ خطا در خروجی اکسل: " & ex.Message, "danger")

        End Try

    End Sub

    ' ============================================================
    ' برای رندر کردن GridView در خروجی اکسل
    ' ============================================================
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)

        ' این متد برای جلوگیری از خطای RenderControl در خروجی اکسل مورد نیاز است
        ' کاری انجام نمی‌دهد

    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(message As String, type As String)

        pnlMessage.Visible = True
        lblMessage.Text = message
        _messageType = type

        Select Case type

            Case "success"
                lblMessageIcon.Text = "✅"
                pnlMessage.CssClass = "exm-alert exm-alert-success"

            Case "danger"
                lblMessageIcon.Text = "❌"
                pnlMessage.CssClass = "exm-alert exm-alert-danger"

            Case "warning"
                lblMessageIcon.Text = "⚠️"
                pnlMessage.CssClass = "exm-alert exm-alert-warning"

            Case Else
                lblMessageIcon.Text = "ℹ️"
                pnlMessage.CssClass = "exm-alert exm-alert-info"

        End Select

    End Sub

    ' ============================================================
    ' دریافت کلاس پیام
    ' ============================================================
    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class