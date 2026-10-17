Imports System
Imports System.Net.Http
Imports System.Web.UI
Imports System.Web.UI.HtmlControls

Public Class test_sso
    Inherits System.Web.UI.Page

    ' حذف تعریف مجدد - این کنترل در aspx تعریف شده است
    ' Protected resultDiv As HtmlGenericControl

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' این متد برای بارگذاری صفحه است
    End Sub

    Protected Sub btnTest1_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/login", "/login")
    End Sub

    Protected Sub btnTest2_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/oauth2/authorize", "/oauth2/authorize")
    End Sub

    Protected Sub btnTest3_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/connect/authorize", "/connect/authorize")
    End Sub

    Protected Sub btnTest4_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/authorize", "/authorize")
    End Sub

    Protected Sub btnTest5_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/.well-known/openid-configuration", ".well-known/openid-configuration")
    End Sub

    Protected Sub btnTest6_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/api/authorize", "/api/authorize")
    End Sub

    Protected Sub btnTest7_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/Account/Login", "/Account/Login")
    End Sub

    Protected Sub btnTest8_Click(sender As Object, e As EventArgs)
        TestUrl("https://sso.tpco.ir/realms/master/protocol/openid-connect/auth", "/realms/master")
    End Sub

    Protected Sub btnTestAll_Click(sender As Object, e As EventArgs)
        Dim result As String = "<h3>📊 نتایج تست همه آدرس‌ها:</h3>"
        Dim urls As New Dictionary(Of String, String) From {
            {"/login", "https://sso.tpco.ir/login"},
            {"/oauth2/authorize", "https://sso.tpco.ir/oauth2/authorize"},
            {"/connect/authorize", "https://sso.tpco.ir/connect/authorize"},
            {"/authorize", "https://sso.tpco.ir/authorize"},
            {"/.well-known/openid-configuration", "https://sso.tpco.ir/.well-known/openid-configuration"},
            {"/api/authorize", "https://sso.tpco.ir/api/authorize"},
            {"/Account/Login", "https://sso.tpco.ir/Account/Login"},
            {"/realms/master", "https://sso.tpco.ir/realms/master/protocol/openid-connect/auth"}
        }

        Using client As New HttpClient()
            client.Timeout = TimeSpan.FromSeconds(5)

            For Each item As KeyValuePair(Of String, String) In urls
                Try
                    Dim response As HttpResponseMessage = client.GetAsync(item.Value).Result
                    Dim status As String = If(response.IsSuccessStatusCode,
                        "<span class='success'>✅ موفق</span>",
                        "<span class='error'>❌ ناموفق</span>")

                    result += String.Format(
                        "<p><strong>{0}</strong><br><code style='background:#eee;padding:5px;display:block;word-break:break-all;'>{1}</code><br>{2} - کد وضعیت: {3}</p>",
                        item.Key,
                        item.Value,
                        status,
                        response.StatusCode
                    )

                    ' اگر Discovery بود و موفق شد
                    If item.Key = "/.well-known/openid-configuration" AndAlso response.IsSuccessStatusCode Then
                        Dim content As String = response.Content.ReadAsStringAsync().Result

                        ' استخراج آدرس authorize از JSON
                        Try
                            Dim authStart As Integer = content.IndexOf("authorization_endpoint")
                            If authStart > 0 Then
                                Dim authEnd As Integer = content.IndexOf(",", authStart)
                                If authEnd < 0 Then authEnd = content.Length - 1
                                Dim authPart As String = content.Substring(authStart, authEnd - authStart)
                                authPart = authPart.Replace("""", "").Replace("authorization_endpoint:", "").Trim()

                                result += "<div class='info-box'>"
                                result += "<strong>🎯 آدرس Authorization پیدا شد:</strong><br>"
                                result += "<code style='background:#fff;padding:5px;display:block;word-break:break-all;'>" & authPart & "</code>"
                                result += "<p>✅ این آدرس را در Web.config به عنوان SsoAuthorizeUrl قرار دهید.</p>"
                                result += "</div>"
                            End If
                        Catch
                        End Try
                    End If

                Catch ex As Exception
                    result += String.Format(
                        "<p><strong>{0}</strong><br><code style='background:#eee;padding:5px;display:block;word-break:break-all;'>{1}</code><br><span class='error'>❌ خطا: {2}</span></p>",
                        item.Key,
                        item.Value,
                        ex.Message
                    )
                End Try
            Next
        End Using

        resultDiv.InnerHtml = result
    End Sub

    Private Sub TestUrl(url As String, name As String)
        Try
            ' نمایش در حال بارگذاری
            resultDiv.InnerHtml = "<div class='loading'>⏳ در حال بررسی آدرس: " & name & "</div>"

            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(10)

                Try
                    Dim response As HttpResponseMessage = client.GetAsync(url).Result

                    ' ساخت نتیجه
                    Dim resultHtml As String = ""

                    If response.IsSuccessStatusCode Then
                        resultHtml = "<h3>✅ نتیجه تست: موفق</h3>"
                        resultHtml += "<p><strong>آدرس تست شده:</strong><br>"
                        resultHtml += "<code style='background:#eee;padding:5px;display:block;word-break:break-all;'>" & url & "</code></p>"
                        resultHtml += "<p><strong>وضعیت:</strong> <span class='success'>" & response.StatusCode & " - " & response.ReasonPhrase & "</span></p>"
                        resultHtml += "<p><strong>نتیجه:</strong> ✅ این آدرس معتبر است! می‌توانید از آن استفاده کنید.</p>"

                        ' خواندن محتوای پاسخ
                        Dim content As String = response.Content.ReadAsStringAsync().Result

                        ' اگر Discovery بود، آدرس authorize را استخراج کن
                        If name = ".well-known/openid-configuration" Then
                            Try
                                Dim authStart As Integer = content.IndexOf("authorization_endpoint")
                                If authStart > 0 Then
                                    Dim authEnd As Integer = content.IndexOf(",", authStart)
                                    If authEnd < 0 Then authEnd = content.Length - 1
                                    Dim authPart As String = content.Substring(authStart, authEnd - authStart)
                                    authPart = authPart.Replace("""", "").Replace("authorization_endpoint:", "").Trim()
                                    resultHtml += "<div class='info-box'>"
                                    resultHtml += "<strong>🎯 آدرس Authorization پیدا شد:</strong><br>"
                                    resultHtml += "<code style='background:#fff;padding:5px;display:block;word-break:break-all;'>" & authPart & "</code>"
                                    resultHtml += "<p>✅ این آدرس را در Web.config به عنوان SsoAuthorizeUrl قرار دهید.</p>"
                                    resultHtml += "</div>"
                                End If
                            Catch
                            End Try
                        End If

                        ' نمایش محتوای پاسخ
                        resultHtml += "<p><strong>محتوای پاسخ (بخشی از آن):</strong></p>"
                        If content.Length > 1000 Then
                            content = content.Substring(0, 1000) & "..."
                        End If
                        resultHtml += "<pre>" & Server.HtmlEncode(content) & "</pre>"

                    Else
                        resultHtml = "<h3>❌ نتیجه تست: ناموفق</h3>"
                        resultHtml += "<p><strong>آدرس تست شده:</strong><br>"
                        resultHtml += "<code style='background:#eee;padding:5px;display:block;word-break:break-all;'>" & url & "</code></p>"
                        resultHtml += "<p><strong>وضعیت:</strong> <span class='error'>" & response.StatusCode & " - " & response.ReasonPhrase & "</span></p>"
                        resultHtml += "<p><strong>نتیجه:</strong> ❌ این آدرس معتبر نیست. آدرس دیگری را تست کنید.</p>"
                    End If

                    resultDiv.InnerHtml = resultHtml

                Catch ex As Exception
                    resultDiv.InnerHtml = "<h3>❌ خطا در تست آدرس</h3>" &
                        "<p><strong>آدرس تست شده:</strong><br><code style='background:#eee;padding:5px;display:block;word-break:break-all;'>" & url & "</code></p>" &
                        "<p><strong>خطا:</strong> " & ex.Message & "</p>" &
                        "<p><strong>نتیجه:</strong> ❌ این آدرس قابل دسترسی نیست.</p>"
                End Try
            End Using

        Catch ex As Exception
            resultDiv.InnerHtml = "<h3>❌ خطا</h3>" &
                "<p><strong>خطا:</strong> " & ex.Message & "</p>"
        End Try
    End Sub
End Class