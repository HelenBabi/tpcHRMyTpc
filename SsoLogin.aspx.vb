Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Configuration
Imports System.Security.Principal

Partial Class SsoLogin
    Inherits System.Web.UI.Page

    ' تعریف متغیرها
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' اگر قبلاً لاگین کرده، به صفحه اصلی هدایت شود
        If Session("userid") IsNot Nothing Then
            Response.Redirect("index.aspx")
        End If

        ' دریافت نام کاربری ویندوز و قرار دادن در TextBox به صورت پیش‌فرض
        If Not IsPostBack Then
            SetDefaultUsername()
        End If
    End Sub

    ' تابع تنظیم نام کاربری پیش‌فرض
    Private Sub SetDefaultUsername()
        Try
            Dim windowsUser As String = ""

            ' دریافت نام کاربری ویندوز از سیستم
            If User.Identity.IsAuthenticated Then
                windowsUser = User.Identity.Name

                ' حذف دامنه (Domain\Username)
                If windowsUser.Contains("\") Then
                    windowsUser = windowsUser.Substring(windowsUser.IndexOf("\") + 1)
                End If
            End If

            ' اگر نام کاربری ویندوز وجود داشت، در TextBox قرار بده
            If Not String.IsNullOrEmpty(windowsUser) Then
                txtPersonnelCode.Text = windowsUser
                lblWindowsUser.InnerText = windowsUser
            Else
                lblWindowsUser.InnerText = "ویندوز Auth فعال نیست"
                txtPersonnelCode.Text = ""
            End If

        Catch ex As Exception
            lblWindowsUser.InnerText = "خطا در دریافت نام کاربری"
            LogError("Error getting Windows user: " & ex.Message)
        End Try
    End Sub

    ' تابع CheckSSO
    Private Function CheckSSO(userName As String, password As String) As String
        Try
            ' خواندن URL از web.config
            Dim url As String = ConfigurationManager.AppSettings("SSOApiUrl")

            ' اگر URL خالی بود، از مقدار پیش‌فرض استفاده کن
            If String.IsNullOrEmpty(url) Then
                url = "http://192.168.34.118:9090/api/Sso/token"
            End If

            ' خواندن API Key از web.config
            Dim apiKey As String = ConfigurationManager.AppSettings("SSOApiKey")
            If String.IsNullOrEmpty(apiKey) Then
                apiKey = "BNTJ1ENFaSHpJHGMkWFi1eS98Fb89mQkF9s4-XyHgXc"
            End If

            Dim request As HttpWebRequest =
                CType(WebRequest.Create(url), HttpWebRequest)

            request.Method = "POST"
            request.ContentType = "application/json"
            request.Timeout = 30000

            Dim json As String =
                "{""apiKey"":""" & apiKey & """," &
                """userName"":""" & userName & """," &
                """password"":""" & password & """}"

            Dim bytes As Byte() = Encoding.UTF8.GetBytes(json)

            Using stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            Dim response As HttpWebResponse =
                CType(request.GetResponse(), HttpWebResponse)

            Using reader As New StreamReader(response.GetResponseStream())
                Dim result As String = reader.ReadToEnd()
                Dim serializer As New JavaScriptSerializer()
                Dim obj = serializer.Deserialize(Of Dictionary(Of String, Object))(result)

                If Convert.ToBoolean(obj("isSuccess")) Then
                    Dim data = CType(obj("data"), Dictionary(Of String, Object))
                    Session("SSOToken") = data("accessToken").ToString()
                    Return data("emplyeeId").ToString()
                End If
            End Using

        Catch ex As WebException
            LogError("WebException in CheckSSO: " & ex.Message)
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim errorResponse As String = reader.ReadToEnd()
                    LogError("Error Response: " & errorResponse)
                End Using
            End If
            Return ""

        Catch ex As Exception
            LogError("Exception in CheckSSO: " & ex.ToString())
            Return ""
        End Try

        Return ""
    End Function

    ' تابع ورود با SSO
    Protected Sub btnSSOLogin_Click(sender As Object, e As EventArgs) Handles btnSSOLogin.Click
        If Page.IsValid Then
            Try
                ' غیرفعال کردن دکمه
                btnSSOLogin.Enabled = False
                btnSSOLogin.Text = "در حال ورود..."

                Dim personnelCode As String = txtPersonnelCode.Text.Trim()
                Dim password As String = txtPassword.Text.Trim()

                ' اعتبارسنجی
                If String.IsNullOrEmpty(personnelCode) Then
                    lblMessage.InnerText = "لطفاً کد پرسنلی یا نام کاربری را وارد کنید"
                    txtPersonnelCode.Focus()
                    btnSSOLogin.Enabled = True
                    btnSSOLogin.Text = "🔑 ورود با SSO"
                    Exit Sub
                End If

                If String.IsNullOrEmpty(password) Then
                    lblMessage.InnerText = "لطفاً رمز عبور را وارد کنید"
                    txtPassword.Focus()
                    btnSSOLogin.Enabled = True
                    btnSSOLogin.Text = "🔑 ورود با SSO"
                    Exit Sub
                End If

                ' بررسی SSO
                Dim employeeId As String = CheckSSO(personnelCode, password)

                If employeeId <> "" Then
                    ' ورود موفق
                    LoginUser(employeeId)
                Else
                    lblMessage.InnerText = "کد پرسنلی یا رمز عبور اشتباه است"
                    LogError("SSO login failed for user: " & personnelCode)
                    btnSSOLogin.Enabled = True
                    btnSSOLogin.Text = "🔑 ورود با SSO"
                End If

            Catch ex As Exception
                lblMessage.InnerText = "خطا در ورود. لطفاً مجدداً تلاش کنید."
                LogError("SSO Login Error: " & ex.Message)
                btnSSOLogin.Enabled = True
                btnSSOLogin.Text = "🔑 ورود با SSO"
            End Try
        End If
    End Sub

    ' تابع ورود با کد پرسنلی (هدایت به login_2.aspx)
    Protected Sub btnPersonnelLogin_Click(sender As Object, e As EventArgs) Handles btnPersonnelLogin.Click
        ' هدایت به صفحه ورود با کد پرسنلی
        Response.Redirect("~/login_2.aspx")
    End Sub

    ' تابع ورود کاربر
    Private Sub LoginUser(employeeId As String)
        Try
            Dim Clsuser_ As New user_login()
            dt = Clsuser_.GetUserokCodePersoneli(employeeId)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' تنظیم Session ها
                Session("fromLogin") = "1"
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
                Session("LoginMethod") = "SSO"

                ' دریافت سمت کاربر
                Try
                    Dim sqlstr2 As String = "SELECT chartName FROM chartTbl WHERE codePersoneli = '" & employeeId & "'"
                    Dim dtp As New DataTable()
                    dtp = ClsDataTable.GetDataTable(sqlstr2)

                    If dtp.Rows.Count > 0 Then
                        Session("semat") = dtp.Rows(0).Item("chartName").ToString()
                    Else
                        Session("semat") = "-"
                    End If
                Catch ex As Exception
                    Session("semat") = "-"
                    LogError("Error getting user position: " & ex.Message)
                End Try

                ' نمایش پیام موفقیت
                lblSuccess.InnerText = "ورود موفق! در حال انتقال به صفحه اصلی..."

                ' هدایت به صفحه اصلی بعد از 2 ثانیه
                Dim script As String = "setTimeout(function(){ window.location='index.aspx'; }, 2000);"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Redirect", script, True)

            Else
                lblMessage.InnerText = "کاربر با این کد پرسنلی در سیستم یافت نشد"
                LogError("User not found: " & employeeId)
                btnSSOLogin.Enabled = True
                btnSSOLogin.Text = "🔑 ورود با SSO"
            End If

        Catch ex As Exception
            lblMessage.InnerText = "خطا در ورود. لطفاً مجدداً تلاش کنید."
            LogError("LoginUser Error: " & ex.Message)
            btnSSOLogin.Enabled = True
            btnSSOLogin.Text = "🔑 ورود با SSO"
        End Try
    End Sub

    ' تابع لاگ خطا
    Private Sub LogError(errorMsg As String)
        Try
            Dim logPath As String = Server.MapPath("~/logs/")
            If Not System.IO.Directory.Exists(logPath) Then
                System.IO.Directory.CreateDirectory(logPath)
            End If

            System.IO.File.AppendAllText(logPath & "sso_error_log.txt",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - " & errorMsg & Environment.NewLine)
        Catch
            ' خطا را نادیده بگیر
        End Try
    End Sub

End Class