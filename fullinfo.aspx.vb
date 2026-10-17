Imports System.Data
Imports System.IO

Partial Class fullinfo
    Inherits System.Web.UI.Page
    Dim _source As String = ""


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'دریافت منبع

            _source = Request.QueryString("source")

            ' نمایش یا مخفی کردن دکمه برگشت به ثبت‌نام‌ها
            btnBackToRegistrations.Visible = (_source = "registrations")

            ' بررسی وجود ID در QueryString
            If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                Dim personID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                ' دریافت کد پرسنلی بر اساس ID
                LoadPersonByID(personID)
            ElseIf Session("codepersonel") IsNot Nothing Then
                txtCode.Text = Session("codepersonel").ToString()
                LoadData()
            End If
        Else
            ' در PostBack داده‌ها را از Session بازیابی کنید
            If Session("TrainingDataFullInfo") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(Session("TrainingDataFullInfo"), DataTable)
                grdTraining.DataSource = dt
                grdTraining.DataBind()
            End If
        End If
    End Sub

    ' ============================================================
    ' بارگذاری اطلاعات بر اساس ID (برای مشاهده از صفحه ثبت‌نام)
    ' ============================================================
    Private Sub LoadPersonByID(personID As Integer)
        Try
            If personID = 0 Then
                ShowMessage("شناسه پرسنل معتبر نیست")
                pnlInfo.Visible = False
                Exit Sub
            End If

            ' دریافت کد پرسنلی بر اساس ID
            Dim sql As String = "SELECT codepersoneli FROM personel WHERE ID = " & personID
            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            If dt.Rows.Count = 0 Then
                ShowMessage("پرسنلی با این شناسه یافت نشد")
                pnlInfo.Visible = False
                Exit Sub
            End If

            Dim codePersonel As String = dt.Rows(0)("codepersoneli").ToString()
            txtCode.Text = codePersonel
            LoadData()

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message)
            pnlInfo.Visible = False
        End Try
    End Sub

    ' برگشت به صفحه قبل
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' اگر از صفحه ثبت‌نام آمده، به آن برگردد
        If _source = "registrations" Then
            Response.Redirect("CourseRegistrations.aspx")
        Else
            Response.Redirect("index.aspx")
        End If
    End Sub

    ' برگشت به صفحه مدیریت ثبت‌نام‌ها
    Protected Sub btnBackToRegistrations_Click(sender As Object, e As EventArgs) Handles btnBackToRegistrations.Click
        Response.Redirect("CourseRegistrations.aspx")
    End Sub

    ' جستجو
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtCode.Text.Trim() = "" Then
            ShowMessage("لطفاً کد پرسنلی را وارد کنید")
            Exit Sub
        End If
        LoadData()
    End Sub

    ' بارگذاری اصلی اطلاعات
    Private Sub LoadData()
        Try
            Dim codePersonel As Integer = Val(txtCode.Text)

            If codePersonel = 0 Then
                ShowMessage("کد پرسنلی معتبر نیست")
                pnlInfo.Visible = False
                Exit Sub
            End If

            ' دریافت اطلاعات پرسنل
            Dim sqlPerson As String = "SELECT * FROM personel WHERE codepersoneli = " & codePersonel
            Dim dtPerson As DataTable = ClsDataTable.GetDataTable(sqlPerson)

            If dtPerson.Rows.Count = 0 Then
                ShowMessage("پرسنلی با این کد یافت نشد")
                pnlInfo.Visible = False
                Exit Sub
            End If

            Dim row As DataRow = dtPerson.Rows(0)
            Dim perId As Integer = Val(row("ID").ToString())
            Dim codeMeli As String = row("codeMelli").ToString()

            ' نمایش اطلاعات پایه
            lblCode.Text = codePersonel.ToString()
            lblName.Text = row("nam").ToString() & " " & row("family").ToString()
            lblCodeMeli.Text = codeMeli
            lblBime.Text = row("codebime").ToString()
            lblDateEmploy.Text = row("tarixestexdam").ToString()
            lblPost.Text = row("postname").ToString()
            lblMadrak.Text = row("madrak").ToString()
            lblReshte.Text = row("reshte").ToString()

            ' بارگذاری عکس
            LoadImage(codePersonel)

            ' بارگذاری سوابق آموزشی
            LoadTrainingRecords(codePersonel)

            ' بارگذاری مدارک
            LoadFiles(codeMeli)

            ' بارگذاری آمار
            LoadStatistics(perId)

            ' نمایش پنل
            pnlInfo.Visible = True

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message)
            pnlInfo.Visible = False
        End Try
    End Sub

    ' بارگذاری عکس
    Private Sub LoadImage(codePersonel As Integer)
        Try
            Dim imgPath As String = Server.MapPath("~/images/person_pic/" & codePersonel & ".jpg")
            If File.Exists(imgPath) Then
                imgPerson.ImageUrl = "~/images/person_pic/" & codePersonel & ".jpg"
            Else
                imgPerson.ImageUrl = "~/images/person_pic/person.ico"
            End If
        Catch
            imgPerson.ImageUrl = "~/images/person_pic/person.ico"
        End Try
    End Sub

    ' بارگذاری سوابق آموزشی با DevExpress GridView
    Private Sub LoadTrainingRecords(codePersonel As Integer)
        Try
            Dim sql As String = "SELECT idd, onvan, dorehayeGozrande, mojriDore, mahalEjra, " &
                               "tarixShoru, tarixPayan, RUZ, saat FROM trnPersonelDoreTbl " &
                               "WHERE codePersoneli = " & codePersonel & " ORDER BY tarixShoru DESC"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            ' ذخیره DataTable در Session برای استفاده در PostBack
            Session("TrainingDataFullInfo") = dt

            grdTraining.DataSource = dt
            grdTraining.DataBind()

        Catch ex As Exception
            'grdTraining.DataSource = Nothing
            'grdTraining.DataBind()
            'Session("TrainingDataFullInfo") = Nothing
        End Try
    End Sub

    'Private Sub LoadTrainingRecords(codePersonel As Integer)
    '    Try
    '        Dim sql As String = "SELECT onvan, dorehayeGozrande, mojriDore, mahalEjra, " &
    '                           "tarixShoru, tarixPayan FROM trnPersonelDoreTbl " &
    '                           "WHERE codePersoneli = " & codePersonel & " ORDER BY tarixShoru DESC"

    '        Dim dt As DataTable = ClsDataTable.GetDataTable(sql)
    '        grdTraining.DataSource = dt
    '        grdTraining.DataBind()
    '    Catch ex As Exception
    '        grdTraining.DataSource = Nothing
    '        grdTraining.DataBind()
    '    End Try
    'End Sub

    ' رویداد تغییر صفحه Grid
    Protected Sub grdTraining_PageIndexChanged(sender As Object, e As EventArgs) Handles grdTraining.PageIndexChanged
        ' بارگذاری مجدد داده‌ها از Session
        If Session("TrainingDataFullInfo") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(Session("TrainingDataFullInfo"), DataTable)
            grdTraining.DataSource = dt
            grdTraining.DataBind()
        Else
            ' اگر داده‌ها در Session نبود، دوباره بارگذاری کن
            If Not String.IsNullOrEmpty(txtCode.Text) Then
                LoadTrainingRecords(Val(txtCode.Text))
            End If
        End If
    End Sub

    ' رویداد DataBinding Grid
    Protected Sub grdTraining_DataBinding(sender As Object, e As EventArgs) Handles grdTraining.DataBinding
        ' اگر داده‌ها خالی بودند، از Session بارگذاری کن
        If grdTraining.DataSource Is Nothing Then
            If Session("TrainingDataFullInfo") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(Session("TrainingDataFullInfo"), DataTable)
                grdTraining.DataSource = dt
            ElseIf Not String.IsNullOrEmpty(txtCode.Text) Then
                LoadTrainingRecords(Val(txtCode.Text))
            End If
        End If
    End Sub

    ' بارگذاری مدارک آپلود شده
    Private Sub LoadFiles(codeMeli As String)
        Try
            If String.IsNullOrEmpty(codeMeli) Then
                rptFiles.Visible = False
                lblNoFiles.Visible = True
                Return
            End If

            Dim folderPath As String = Server.MapPath("~/Attachment/TRN/" & codeMeli & "/")

            If Not Directory.Exists(folderPath) Then
                rptFiles.Visible = False
                lblNoFiles.Visible = True
                Return
            End If

            Dim files = Directory.GetFiles(folderPath).ToList()

            If files.Count = 0 Then
                rptFiles.Visible = False
                lblNoFiles.Visible = True
                Return
            End If

            Dim fileList As New List(Of Object)
            For Each file In files
                Dim fileName = Path.GetFileName(file)
                Dim ext = Path.GetExtension(file).ToLower()
                Dim isImage As Boolean = (ext = ".jpg" OrElse ext = ".jpeg" OrElse ext = ".png" OrElse ext = ".gif" OrElse ext = ".bmp" OrElse ext = ".webp")

                fileList.Add(New With {
                    .Url = ResolveUrl("~/Attachment/TRN/" & codeMeli & "/" & fileName),
                    .FileName = fileName,
                    .IsImage = isImage
                })
            Next

            rptFiles.DataSource = fileList
            rptFiles.DataBind()
            rptFiles.Visible = True
            lblNoFiles.Visible = False

        Catch ex As Exception
            rptFiles.Visible = False
            lblNoFiles.Visible = True
        End Try
    End Sub

    ' بارگذاری آمار سوابق
    Private Sub LoadStatistics(perId As Integer)
        Try
            ' سابقه کار از جدول savabeg
            Dim sqlKar As String = "SELECT " &
                                   "ISNULL(SUM(CAST(karkardSal AS INT)), 0) AS sal, " &
                                   "ISNULL(SUM(CAST(karkardMah AS INT)), 0) AS mah, " &
                                   "ISNULL(SUM(CAST(karkardDay AS INT)), 0) AS ruz " &
                                   "FROM savabeg WHERE perID = " & perId

            Dim dtKar As DataTable = ClsDataTable.GetDataTable(sqlKar)
            If dtKar.Rows.Count > 0 Then
                Dim totalMonths As Integer = Val(dtKar.Rows(0)("mah").ToString())
                Dim totalYears As Integer = Val(dtKar.Rows(0)("sal").ToString())
                Dim totalDays As Integer = Val(dtKar.Rows(0)("ruz").ToString())

                totalMonths += totalYears * 12
                totalYears = totalMonths \ 12
                totalMonths = totalMonths Mod 12

                lblSalKar.Text = totalYears.ToString()
                lblMahKar.Text = totalMonths.ToString()
                lblRuzKar.Text = totalDays.ToString()
            End If

            ' سختی کار
            Dim sqlSakhti As String = "SELECT " &
                                      "ISNULL(SUM(CAST(s.karkardSal AS INT)), 0) AS sal, " &
                                      "ISNULL(SUM(CAST(s.karkardMah AS INT)), 0) AS mah, " &
                                      "ISNULL(SUM(CAST(s.karkardDay AS INT)), 0) AS ruz " &
                                      "FROM savabeg s " &
                                      "INNER JOIN mashaghel m ON s.onvan = m.id " &
                                      "WHERE s.perID = " & perId & " AND m.saxt = 1"

            Dim dtSakhti As DataTable = ClsDataTable.GetDataTable(sqlSakhti)
            If dtSakhti.Rows.Count > 0 Then
                Dim totalMonths As Integer = Val(dtSakhti.Rows(0)("mah").ToString())
                Dim totalYears As Integer = Val(dtSakhti.Rows(0)("sal").ToString())

                totalMonths += totalYears * 12
                totalYears = totalMonths \ 12

                lblSalSakhti.Text = totalYears.ToString()
            End If

        Catch ex As Exception
            lblSalKar.Text = "0"
            lblMahKar.Text = "0"
            lblRuzKar.Text = "0"
            lblSalSakhti.Text = "0"
        End Try
    End Sub

    ' کوتاه کردن متن
    Public Function CutText(text As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(text) Then Return ""
        If text.Length <= maxLen Then Return text
        Return text.Substring(0, maxLen - 3) & "..."
    End Function

    ' نمایش پیغام
    Private Sub ShowMessage(msg As String)
        Dim script As String = "alert('" & msg.Replace("'", " ") & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", script, True)
    End Sub

End Class