Imports System.Net
Imports System.IO
Imports System.Web
Partial Class Default5
    Inherits System.Web.UI.Page


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Dim username As String = "farzad.baradaran@gmail.com" ' ← نام کاربری AFE
        Dim password As String = "Tpc#2922" ' ← رمز عبور AFE
        Dim number As String = "30007957951793"          ' ← شماره فرستنده
        Dim toNumber As String = txtNumber.Text.Trim()
        Dim message As String = HttpUtility.UrlEncode(txtMessage.Text.Trim())

        ' تعریف url اینجا هست 👇
        Dim url As String = "http://www.afe.ir/Url/SendSMS?Username=" & username & _
                         "&Password=" & password & _
                         "&Number=" & number & _
                         "&Mobile=" & toNumber & _
                         "&Message=" & message


        Try
            Dim request As WebRequest = WebRequest.Create(url)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()

            reader.Close()
            response.Close()

            lblResult.Text = "پاسخ سرور: " & responseFromServer
        Catch ex As Exception
            lblResult.ForeColor = Drawing.Color.Red
            lblResult.Text = "خطا در ارسال پیامک: " & ex.Message
        End Try
    End Sub
End Class
