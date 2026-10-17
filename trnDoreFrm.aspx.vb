Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Partial Class trnDoreFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPersonInfo()
            LoadTrainingGrid()
            LoadCertificateFiles()
        Else
            ' در PostBack داده‌ها را از Session بازیابی کنید
            If Session("TrainingData") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(Session("TrainingData"), DataTable)
                trnGrid.DataSource = dt
                trnGrid.DataBind()
                CalculateStatistics(dt)
            End If
        End If
    End Sub

    ' بارگذاری اطلاعات پرسنل
    Private Sub LoadPersonInfo()
        Try
            Dim codePersoneli As String = Session("codePersoneli").ToString()

            If String.IsNullOrEmpty(codePersoneli) Then
                msg_box("کد پرسنلی یافت نشد")
                Exit Sub
            End If

            Dim sqlstr As String = "SELECT postname, madrak, reshte FROM personel WHERE codepersoneli = " & Val(codePersoneli)
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                sematTxt.Text = dt.Rows(0)("postname").ToString()
                madrakTxt.Text = dt.Rows(0)("madrak").ToString()
                reshteTxt.Text = dt.Rows(0)("reshte").ToString()
            End If
        Catch ex As Exception
            msg_box("خطا در بارگذاری اطلاعات: " & ex.Message)
        End Try
    End Sub

    ' بارگذاری سوابق آموزشی
    Private Sub LoadTrainingGrid()
        Try
            Dim codePersoneli As String = Session("codePersoneli").ToString()

            If String.IsNullOrEmpty(codePersoneli) Then
                Exit Sub
            End If

            Dim sqlStr As String = "SELECT * FROM trnPersonelDoreTbl WHERE codePersoneli = " & Val(codePersoneli) & " ORDER BY tarixShoru DESC"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlStr)

            ' ذخیره DataTable در Session
            Session("TrainingData") = dt

            trnGrid.DataSource = dt
            trnGrid.DataBind()

            ' محاسبه آمار
            CalculateStatistics(dt)

        Catch ex As Exception
            trnGrid.DataSource = Nothing
            trnGrid.DataBind()
            Session("TrainingData") = Nothing
        End Try
    End Sub

    ' محاسبه آمار
    Private Sub CalculateStatistics(dt As DataTable)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblTotalCount.Text = dt.Rows.Count.ToString()

            Dim totalHours As Integer = 0
            Dim totalDays As Integer = 0

            For Each row As DataRow In dt.Rows
                If IsNumeric(row("saat")) Then
                    totalHours += Val(row("saat").ToString())
                End If
                If IsNumeric(row("RUZ")) Then
                    totalDays += Val(row("RUZ").ToString())
                End If
            Next

            lblTotalHours.Text = totalHours.ToString()
            lblTotalDays.Text = totalDays.ToString()
        Else
            lblTotalCount.Text = "0"
            lblTotalHours.Text = "0"
            lblTotalDays.Text = "0"
        End If
    End Sub

    ' رویداد تغییر صفحه Grid
    Protected Sub trnGrid_PageIndexChanged(sender As Object, e As EventArgs) Handles trnGrid.PageIndexChanged
        ' بارگذاری مجدد داده‌ها از Session
        If Session("TrainingData") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(Session("TrainingData"), DataTable)
            trnGrid.DataSource = dt
            trnGrid.DataBind()
        Else
            LoadTrainingGrid()
        End If
    End Sub

    ' رویداد DataBinding Grid
    Protected Sub trnGrid_DataBinding(sender As Object, e As EventArgs) Handles trnGrid.DataBinding
        ' اگر داده‌ها خالی بودند، دوباره بارگذاری کنید
        If trnGrid.DataSource Is Nothing Then
            If Session("TrainingData") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(Session("TrainingData"), DataTable)
                trnGrid.DataSource = dt
            Else
                LoadTrainingGrid()
            End If
        End If
    End Sub

    ' بارگذاری مدارک آپلود شده (فقط نمایش، بدون حذف)
    Private Sub LoadCertificateFiles()
        Try
            Dim codePersoneli As String = Session("codePersoneli").ToString()

            If String.IsNullOrEmpty(codePersoneli) Then
                ShowNoFiles()
                Exit Sub
            End If

            ' دریافت کد ملی پرسنل
            Dim sqlMeli As String = "SELECT codeMelli FROM personel WHERE codepersoneli = " & Val(codePersoneli)
            Dim dtMeli As DataTable = ClsDataTable.GetDataTable(sqlMeli)

            If dtMeli.Rows.Count = 0 Then
                ShowNoFiles()
                Exit Sub
            End If

            Dim codeMeli As String = dtMeli.Rows(0)("codeMelli").ToString()
            Dim folderPath As String = Server.MapPath("~/Attachment/TRN/" & codeMeli & "/")

            If Not Directory.Exists(folderPath) Then
                ShowNoFiles()
                Exit Sub
            End If

            Dim files = Directory.GetFiles(folderPath).ToList()

            If files.Count = 0 Then
                ShowNoFiles()
                Exit Sub
            End If

            ' ساخت لیست فایل‌ها
            Dim fileList As New List(Of Object)

            For Each file In files
                Dim fileName = Path.GetFileName(file)
                Dim extension = Path.GetExtension(file).ToLower()
                Dim isImage As Boolean = (extension = ".jpg" OrElse extension = ".jpeg" OrElse _
                                          extension = ".png" OrElse extension = ".gif" OrElse _
                                          extension = ".bmp" OrElse extension = ".webp")

                fileList.Add(New With {
                    .Url = ResolveUrl("~/Attachment/TRN/" & codeMeli & "/" & fileName),
                    .FileName = fileName,
                    .IsImage = isImage
                })
            Next

            rptCertificates.DataSource = fileList
            rptCertificates.DataBind()
            pnlNoFiles.Visible = False
            lblTotalFiles.Text = files.Count.ToString()

        Catch ex As Exception
            ShowNoFiles()
        End Try
    End Sub

    ' نمایش پیام خالی بودن فایل‌ها
    Private Sub ShowNoFiles()
        rptCertificates.DataSource = Nothing
        rptCertificates.DataBind()
        pnlNoFiles.Visible = True
        lblTotalFiles.Text = "0"
    End Sub

    ' کوتاه کردن نام فایل
    Public Function TruncateFileName(fileName As String, maxLength As Integer) As String
        If String.IsNullOrEmpty(fileName) Then Return ""
        If fileName.Length <= maxLength Then Return fileName
        Return fileName.Substring(0, maxLength - 3) & "..."
    End Function

    ' نمایش پیغام
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
    End Sub

    ' دکمه برگشت
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

End Class