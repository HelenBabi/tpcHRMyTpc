Imports System
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.UI
Imports System.Web.Script.Serialization
Imports System.Configuration
Imports System.Text
Imports System.Data

Public Class sso
    Inherits System.Web.UI.Page

    Protected Async Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' جلوگیری از اجرای مجدد در PostBack
        If Not IsPostBack Then
            Try
                ' دریافت کد از SSO
                Dim code As String = Request.QueryString("code")

                If String.IsNullOrEmpty(code) Then
                    Session("Error") = "کد احراز هویت دریافت نشد."
                    Response.Redirect("login_2.aspx")
                    Return
                End If

                ' دریافت تنظیمات از Web.config
                Dim tokenUrl As String = ConfigurationManager.AppSettings("SsoTokenUrl")
                Dim clientName As String = ConfigurationManager.AppSettings("SsoClientName")

                If String.IsNullOrEmpty(tokenUrl) OrElse String.IsNullOrEmpty(clientName) Then
                    Session("Error") = "تنظیمات SSO کامل نیست."
                    Response.Redirect("login_2.aspx")
                    Return
                End If

                ' ارسال درخواست به SSO
                Using httpClient As New HttpClient()
                    ' ساخت داده‌های درخواست
                    Dim requestData As New Dictionary(Of String, String) From {
                        {"Code", code},
                        {"ClientName", clientName}
                    }

                    ' تبدیل به JSON
                    Dim serializer As New JavaScriptSerializer()
                    Dim jsonData As String = serializer.Serialize(requestData)
                    Dim jsonContent As New StringContent(jsonData, Encoding.UTF8, "application/json")

                    ' ارسال درخواست به SSO
                    Dim httpResponse As HttpResponseMessage = Await httpClient.PostAsync(tokenUrl, jsonContent).ConfigureAwait(False)

                    ' بررسی پاسخ
                    If Not httpResponse.IsSuccessStatusCode Then
                        Session("Error") = "تبدیل کد به توکن ناموفق بود."
                        Response.Redirect("login_2.aspx")
                        Return
                    End If

                    ' خواندن پاسخ
                    Dim jsonResponse As String = Await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(False)

                    ' تبدیل پاسخ به شیء
                    Dim envelope As SsoTokenEnvelope = serializer.Deserialize(Of SsoTokenEnvelope)(jsonResponse)

                    ' بررسی اعتبار پاسخ
                    If envelope Is Nothing OrElse Not envelope.IsSuccess OrElse envelope.Data Is Nothing Then
                        Dim errorMsg As String = If(envelope IsNot Nothing, envelope.Message, "پاسخ نامعتبر")
                        Session("Error") = errorMsg
                        Response.Redirect("login_2.aspx")
                        Return
                    End If

                    ' ================================================
                    ' دریافت شماره موبایل از SSO
                    ' ================================================
                    Dim mobile As String = envelope.Data.Mobile

                    ' ================================================
                    ' پیدا کردن کد پرسنلی بر اساس شماره موبایل
                    ' ================================================
                    'Dim employeeId As String = GetEmployeeIdByMobile(mobile)
                    LoginUser(mobile)
                   

                End Using

            Catch ex As Exception
                Session("Error") = "خطا در ارتباط با SSO: " & ex.Message
                Response.Redirect("login_2.aspx")
            End Try
        End If
    End Sub

    ' ================================================
    ' تابع پیدا کردن کد پرسنلی بر اساس شماره موبایل
    ' ================================================
    Private Function GetEmployeeIdByMobile(mobile As String) As String
        Try
            ' استفاده از کلاس user_login برای پیدا کردن کاربر
            Dim clsUser As New user_login()
            Dim dt As DataTable = clsUser.GetUserByMobile(mobile)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("codePersoneli").ToString().Trim()
            End If
            Return ""
        Catch ex As Exception
            LogError("GetEmployeeIdByMobile Error: " & ex.Message)
            Return ""
        End Try
    End Function

    ' ================================================
    ' تابع ورود کاربر با کد پرسنلی (مشابه loginnn شما)
    ' ================================================
    Private Sub LoginUser(employeeId As String)
        Try
            Dim Clsuser_ As New user_login()
            Dim dt As New DataTable

            ' دریافت اطلاعات کاربر بر اساس کد پرسنلی
            dt = Clsuser_.GetUserByMobile(employeeId)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' ================================================
                ' تنظیم Session ها (مطابق با loginnn شما)
                ' ================================================
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

                ' ================================================
                ' دریافت سمت کاربر
                ' ================================================
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

                ' ================================================
                ' هدایت به صفحه اصلی
                ' ================================================
                Response.Redirect("index.aspx")

            Else
                ' کاربر پیدا نشد
                Session("Error") = "کاربر با کد پرسنلی " & employeeId & " در سیستم یافت نشد."
                Response.Redirect("login_2.aspx")
            End If

        Catch ex As Exception
            Session("Error") = "خطا در ورود: " & ex.Message
            Response.Redirect("login_2.aspx")
        End Try
    End Sub

    ' ================================================
    ' تابع ثبت خطا (اختیاری)
    ' ================================================
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

' ================================================
' کلاس‌های مدل برای پاسخ SSO
' ================================================
Public Class SsoTokenData
    Public Property Mobile As String
    Public Property Token As String
    Public Property FullName As String
    Public Property Email As String
    Public Property NationalCode As String
End Class

Public Class SsoTokenEnvelope
    Public Property IsSuccess As Boolean
    Public Property Message As String
    Public Property Data As SsoTokenData
End Class