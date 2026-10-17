Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Web

Partial Class light_login2
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' ================================================
        ' ذخیره ReturnUrl در Session اگر در QueryString وجود دارد
        ' ================================================
        Dim returnUrl As String = Request.QueryString("ReturnUrl")
        If Not String.IsNullOrEmpty(returnUrl) Then
            Session("ReturnUrl") = returnUrl
        End If

        ' ================================================
        ' اگر قبلاً لاگین کرده، به صفحه قبلی یا صفحه اصلی هدایت شود
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
            divReturnInfo.Attributes("class") = "return-info show"
        End If

        ' ================================================
        ' بارگذاری پیغام فعال مدیر سیستم (فقط در اولین بارگذاری)
        ' ================================================
        If Not IsPostBack Then
            LoadSystemMessage()
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
                litMessageTitle.Text = dtMsg.Rows(0)("MessageTitle").ToString().Trim()
                litSystemMessage.Text = dtMsg.Rows(0)("MessageText").ToString()
            End If
        Catch ex As Exception
            LogError("LoadSystemMessage: " & ex.Message)
        End Try
    End Sub

    ' ================================================
    ' پاکسازی HTML از فاصله‌های اضافی
    ' ================================================
    Private Function CleanHtml(ByVal html As String) As String
        If String.IsNullOrEmpty(html) Then Return ""

        ' حذف newline و tab
        html = html.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        html = html.Replace(Chr(9), " ")

        ' حذف فاصله‌های اضافی با Regex
        html = Regex.Replace(html, "\s+", " ")           ' چند فاصله → یک فاصله
        html = Regex.Replace(html, ">\s+<", "><")        ' فاصله بین تگ‌ها
        html = Regex.Replace(html, "\s+>", ">")          ' فاصله قبل از >
        html = Regex.Replace(html, "<\s+", "<")          ' فاصله بعد از <

        Return html.Trim()
    End Function

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
                ' بررسی وجود ReturnUrl در Session
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
    ' تابع بررسی URL معتبر (امنیتی)
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
    ' ثبت خطا در فایل لاگ
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
            ' خطا را نادیده بگیر
        End Try
    End Sub

End Class