Imports System.Net
Imports System.IO
Imports System.Text

Partial Class SendSMS
    Inherits System.Web.UI.Page

    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Dim mobilesText As String = txtMobiles.Text.Trim().Replace(Environment.NewLine, ",")
        Dim message As String = txtMessage.Text.Trim()

        If mobilesText = "" Or message = "" Then
            lblResult.Text = "لطفاً شماره و متن پیام را وارد کنید."
            lblResult.ForeColor = Drawing.Color.Red
            Return
        End If

        Dim numbers As String() = mobilesText.Split(","c)
        Dim successCount As Integer = 0

        For Each number In numbers
            If SendSMS_AFE(number.Trim(), message) Then
                successCount += 1
            End If
        Next

        lblResult.Text = "تعداد پیامک‌های ارسال‌شده: " & successCount
        lblResult.ForeColor = Drawing.Color.Green
    End Sub

    Private Function SendSMS_AFE(mobile As String, messageText As String) As Boolean
        Try


            Dim username As String = "farzad.baradaran@gmail.com"
            Dim password As String = "Tpc#2922"
            Dim fromLine As String = "30007957951793"
            Dim url As String = "https://panel.afe.ir/Api/SendSMS.ashx"

            Dim postData As String = "username=" & username &
                                     "&password=" & password &
                                     "&from=" & fromLine &
                                     "&to=" & mobile &
                                     "&text=" & messageText

            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length

            Using stream As Stream = request.GetRequestStream()
                stream.Write(byteArray, 0, byteArray.Length)
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim result As String = reader.ReadToEnd()
                    Return Not result.ToLower().Contains("error")
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function
End Class
