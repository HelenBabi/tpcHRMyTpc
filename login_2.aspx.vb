Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Globalization

Partial Class light_login3
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' ====== بررسی پیام موفقیت ======
        If Session("LoginSuccessMessage") IsNot Nothing Then
            pnlSuccessMessage.Visible = True
            lblSuccessMessage.Text = Session("LoginSuccessMessage").ToString()
            Session("LoginSuccessMessage") = Nothing
        End If
        ' ================================================
        ' ذخیره ReturnUrl در Session
        ' ================================================
        Dim returnUrl As String = Request.QueryString("ReturnUrl")
        If Not String.IsNullOrEmpty(returnUrl) Then
            Session("ReturnUrl") = returnUrl
        End If

        ' ================================================
        ' اگر قبلاً لاگین کرده
        ' ================================================
        If Session("userid") IsNot Nothing Then
            Dim savedUrl As String = Session("ReturnUrl")
            If Not String.IsNullOrEmpty(savedUrl) AndAlso IsLocalUrl(savedUrl) Then
                Session("ReturnUrl") = Nothing
                Response.Redirect(savedUrl)
            Else
                Response.Redirect("index.aspx")
            End If
        End If

        ' ================================================
        ' نمایش پیام اگر از صفحه ثبت حضور آمده باشد
        ' ================================================
        Dim returnUrl2 As String = Request.QueryString("ReturnUrl")
        If Not String.IsNullOrEmpty(returnUrl2) AndAlso returnUrl2.ToLower().Contains("trnscanattendance") Then
            divReturnInfo.Attributes("style") = "display:block; background:#d1ecf1; padding:12px 15px; border-radius:10px; margin-bottom:20px; text-align:center; border:2px solid #bee5eb;"
        End If

        ' ================================================
        ' بارگذاری اولیه
        ' ================================================
        If Not IsPostBack Then
            LoadSystemMessage()
            LoadNews()
        End If
    End Sub

    ' ================================================
    ' بارگذاری پیغام فعال مدیر سیستم
    ' ================================================
    ' ================================================
    ' بارگذاری پیغام فعال مدیر سیستم
    ' ================================================
    Private Sub LoadSystemMessage()
        Try
            Dim sql As String = "SELECT TOP 1 MessageTitle, MessageText FROM SystemMessages WHERE IsActive = 1 ORDER BY CreatedDate DESC"
            Dim dtMsg As DataTable = ClsDataTable.GetDataTable(sql)

            If dtMsg IsNot Nothing AndAlso dtMsg.Rows.Count > 0 Then
                ' ====== پر کردن کارت پیغام مدیر ======
                lblAdminMessageTitle.Text = dtMsg.Rows(0)("MessageTitle").ToString().Trim()
                litAdminMessage.Text = dtMsg.Rows(0)("MessageText").ToString()

                ' ====== نمایش کارت ======
                pnlAdminMessage.Visible = True
            Else
                pnlAdminMessage.Visible = False
            End If
        Catch ex As Exception
            LogError("LoadSystemMessage: " & ex.Message)
            pnlAdminMessage.Visible = False
        End Try
    End Sub

    ' ================================================
    ' بارگذاری اطلاعیه‌ها و تازه‌ها
    ' ================================================
    Private Sub LoadNews()
        Try
            Dim sql As String = "SELECT ID, NewsTitle, NewsText, ISNULL(NewsIcon, N'📢') AS NewsIcon, " &
                "dbo.FormatDateFa(CreatedDate) AS PersianDate " &
                "FROM SystemNews " &
                "WHERE IsActive = 1 " &
                "ORDER BY SortOrder ASC, CreatedDate DESC"

            Dim dtNews As DataTable = ClsDataTable.GetDataTable(sql)

            If dtNews IsNot Nothing AndAlso dtNews.Rows.Count > 0 Then
                rptNews.DataSource = dtNews
                rptNews.DataBind()
                pnlNoNews.Visible = False
            Else
                rptNews.DataSource = Nothing
                rptNews.DataBind()
                pnlNoNews.Visible = True
            End If
        Catch ex As Exception
            LogError("LoadNews: " & ex.Message)
            pnlNoNews.Visible = True
        End Try
    End Sub

    ' ================================================
    ' لاگین
    ' ================================================
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Page.IsValid Then
            loginnn()
        End If
    End Sub

    Private Sub loginnn()
        Try
            Dim uname As String = TextBox1.Text.Trim()
            Dim upass As String = TextBox2.Text.Trim()
            Dim Clsuser_ As New user_login

            dt = Clsuser_.GetUserok(uname, upass)

            If dt.Rows.Count > 0 Then
                ' تنظیم سشن‌ها
                Session("userid") = dt.Rows(0).Item("ID").ToString()
                Session("lname") = dt.Rows(0).Item("nam").ToString()
                Session("fname") = dt.Rows(0).Item("family").ToString()
                Session("FullName") = Session("lname") & " " & Session("fname")
                Session("mobile") = dt.Rows(0).Item("mobile").ToString()
                Session("super_vizer") = dt.Rows(0).Item("supervizer")
                Session("omurID") = dt.Rows(0).Item("omurID").ToString()
                Session("unitID") = dt.Rows(0).Item("UnitsCode").ToString()
                Session("contratTyp") = dt.Rows(0).Item("typ").ToString()
                Session("codePersoneli") = dt.Rows(0).Item("codePersoneli").ToString().Trim()
                Session("shift") = dt.Rows(0).Item("shift").ToString()
                Session("modir") = dt.Rows(0).Item("modir").ToString()
                Session("jens") = dt.Rows(0).Item("jens").ToString()
                Session("NationalCode") = dt.Rows(0).Item("codeMelli").ToString()
                Session("erja") = dt.Rows(0).Item("erja").ToString()
                Session("ok") = dt.Rows(0).Item("ok").ToString()
                Session("calncel") = dt.Rows(0).Item("calncel").ToString()
                Session("sms") = dt.Rows(0).Item("sms").ToString()
                Session("LoginMethod") = "Normal"

                ' دریافت سمت کاربر
                Dim sqlstr2 As String = "SELECT chartName FROM chartTbl WHERE codePersoneli = '" & dt.Rows(0).Item("codePersoneli").ToString().Trim() & "'"
                Dim dtp As New DataTable()
                dtp = ClsDataTable.GetDataTable(sqlstr2)

                If dtp.Rows.Count > 0 Then
                    Session("semat") = dtp.Rows(0).Item("chartName").ToString()
                Else
                    Session("semat") = "-"
                End If

                ' ================================================
                ' بررسی ReturnUrl
                ' ================================================
                Dim returnUrl As String = Session("ReturnUrl")

                If Not String.IsNullOrEmpty(returnUrl) AndAlso IsLocalUrl(returnUrl) Then
                    Session("ReturnUrl") = Nothing
                    Response.Redirect(returnUrl, False)
                Else
                    Response.Redirect("index.aspx", False)
                End If

                Context.ApplicationInstance.CompleteRequest()
            Else
                msg_box("نام کاربری یا رمز عبور اشتباه می باشد.")
                ClearForm()
            End If
        Catch ex As Exception
            msg_box("خطا در ارتباط با پایگاه داده. لطفا با پشتیبانی تماس بگیرید.")
            LogError(ex.Message)
        End Try
    End Sub

    ' ================================================
    ' بررسی URL معتبر
    ' ================================================
    Private Function IsLocalUrl(ByVal url As String) As Boolean
        If String.IsNullOrEmpty(url) Then Return False

        If url.StartsWith("/") OrElse url.StartsWith("~/") Then Return True
        If url.StartsWith("./") OrElse url.StartsWith("../") Then Return True

        If url.StartsWith("http") OrElse url.StartsWith("https") Then
            Dim appPath As String = Request.Url.GetLeftPart(UriPartial.Authority)
            Return url.StartsWith(appPath, StringComparison.OrdinalIgnoreCase)
        End If

        Return False
    End Function

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder()
        sb.Append("swal({ title: 'خطا!', text: '")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("', type: 'error', confirmButtonText: 'باشه' });")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
    End Sub

    Private Sub ClearForm()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Focus()
    End Sub

    ' ================================================
    ' ثبت خطا
    ' ================================================
    Private Sub LogError(errorMsg As String)
        Try
            Dim logPath As String = Server.MapPath("~/logs/")
            If Not System.IO.Directory.Exists(logPath) Then
                System.IO.Directory.CreateDirectory(logPath)
            End If
            System.IO.File.AppendAllText(logPath & "error_log.txt",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - " & errorMsg & Environment.NewLine)
        Catch
        End Try
    End Sub

End Class