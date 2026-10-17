<%@ Application Language="VB" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>

<script runat="server">

    Private Shared _timer As System.Threading.Timer
    Private Shared _isRunning As Boolean = False

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' اجرا هر شب ساعت 20:00 (8 شب)
        Dim now As DateTime = DateTime.Now
        Dim nextRun As DateTime = New DateTime(now.Year, now.Month, now.Day, 20, 0, 0)
        If now > nextRun Then
            nextRun = nextRun.AddDays(1)
        End If

        Dim dueTime As Integer = CInt((nextRun - now).TotalMilliseconds)
        Dim period As Integer = 86400000 ' 24 ساعت

        _timer = New System.Threading.Timer(AddressOf SendReminders, Nothing, dueTime, period)
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        If _timer IsNot Nothing Then
            _timer.Dispose()
        End If
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends
    End Sub

    Private Sub SendReminders(state As Object)
        If _isRunning Then Return
        _isRunning = True

        Try
            SendExaminationReminders()
        Catch ex As Exception
            ' ثبت خطا
        Finally
            _isRunning = False
        End Try
    End Sub

    Private Sub SendExaminationReminders()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("YourConnectionString").ConnectionString

        ' ====== کوئری با JOIN به جدول personel ======
        Dim query As String = "SELECT " &
                              "    r.ID, " &
                              "    r.PersonCode, " &
                              "    r.FullName, " &
                              "    p.mobile AS Mobile, " &
                              "    dbo.ConvertGregorianToJalali(r.ExaminationDate) AS PersianDate " &
                              "FROM exmReservationRequests r " &
                              "INNER JOIN personel p ON r.PersonCode = p.codePersoneli " &
                              "WHERE r.StatusCode = 4 " &
                              "AND r.IsDone = 0 " &
                              "AND r.ReminderSent = 0 " &
                              "AND r.ExaminationDate = DATEADD(DAY, 1, CAST(GETDATE() AS DATE))"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim mobile As String = reader("Mobile").ToString()
                        Dim name As String = reader("FullName").ToString()
                        Dim persianDate As String = reader("PersianDate").ToString()
                        Dim id As Integer = Convert.ToInt32(reader("ID"))

                        If Not String.IsNullOrEmpty(mobile) AndAlso mobile.Length >= 11 Then
                            Dim message As String = "سلام " & name & " عزیز، فردا (" & persianDate & ") نوبت معاینه دوره‌ای شماست."

                            If SendSms(mobile, message) Then
                                UpdateReminderStatus(id, connectionString)
                            End If
                        End If
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Function SendSms(mobile As String, message As String) As Boolean
        Try
            Dim username As String = "farzad.baradaran@gmail.com"
            Dim password As String = "Tpc#2922"
            Dim fromNumber As String = "30007957951793"

            Dim url As String = "http://www.afe.ir/WebService/V4/BoxService.asmx/SendSms?" &
                               "username=" & username &
                               "&password=" & password &
                               "&number=" & fromNumber &
                               "&mobiles=" & mobile &
                               "&text=" & message &
                               "&type=1&time=0"

            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"
            request.ContentType = "text/xml; charset=utf-8"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Dim result As String = reader.ReadToEnd()
                    Return result.Contains("OK") OrElse result.Contains("موفق")
                End Using
            End Using

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub UpdateReminderStatus(id As Integer, connectionString As String)
        Dim updateQuery As String = "UPDATE exmReservationRequests SET ReminderSent = 1 WHERE ID = @ID"
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(updateQuery, connection)
                command.Parameters.AddWithValue("@ID", id)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

</script>