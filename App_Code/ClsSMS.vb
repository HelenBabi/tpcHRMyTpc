Imports System.Net
Imports System.Web

Public Class ClsSMS
    Public Shared Sub SendSMS(mobile As String, message As String)
        Dim username As String = "your_username"
        Dim password As String = "your_password"
        Dim senderNumber As String = "your_sender_number"

        'Dim apiUrl As String = $"https://panel.afe.ir/UrlService/sendSMS.ashx" &
        '    $"?username={HttpUtility.UrlEncode(username)}" &
        '    $"&password={HttpUtility.UrlEncode(password)}" &
        '    $"&from={senderNumber}&to={mobile}" &
        '    $"&text={HttpUtility.UrlEncode(message)}"

        'Using client As New WebClient()
        '    client.Encoding = Text.Encoding.UTF8
        '    client.DownloadString(apiUrl)
        'End Using
    End Sub
End Class