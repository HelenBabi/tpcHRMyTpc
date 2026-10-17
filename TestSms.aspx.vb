Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports System.Text

Partial Class TestSms
    Inherits System.Web.UI.Page

    Protected Sub btnSend_Click(sender As Object, e As EventArgs)
        Try
            lblResult.Text = "در حال ارسال پیامک... لطفاً صبر کنید."
            lblResult.ForeColor = System.Drawing.Color.Blue

            SendExaminationReminders()

            lblResult.Text = "✅ پیامک‌های یادآوری با موفقیت ارسال شدند."
            lblResult.ForeColor = System.Drawing.Color.Green

        Catch ex As Exception
            lblResult.Text = "❌ خطا: " & ex.Message
            lblResult.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

    Private Sub SendExaminationReminders()
        Dim connectionString As String = "Data Source=.;Initial Catalog=bimeDB;User ID=sa;Password=as12AS!@;"

        ' ====== کوئری با JOIN به جدول personel برای دریافت شماره موبایل ======
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
                    Dim count As Integer = 0
                    While reader.Read()
                        count += 1
                        Dim id As Integer = Convert.ToInt32(reader("ID"))
                        Dim personCode As String = reader("PersonCode").ToString()
                        Dim name As String = reader("FullName").ToString()
                        Dim mobile As String = reader("Mobile").ToString()
                        Dim persianDate As String = reader("PersianDate").ToString()

                        ' ====== نمایش در صفحه ======
                        lblResult.Text &= "<br />📌 " & count & ". " & name & " - " & mobile

                        ' ====== بررسی وجود شماره موبایل ======
                        If String.IsNullOrEmpty(mobile) OrElse mobile.Length < 11 Then
                            lblResult.Text &= " ⚠️ شماره موبایل نامعتبر!"
                            Continue While
                        End If

                        Dim message As String = "سلام " & name & " عزیز، فردا (" & persianDate & ") نوبت معاینه دوره‌ای شماست."

                        If SendSms(mobile, message) Then
                            UpdateReminderStatus(id, connectionString)
                            lblResult.Text &= " ✅ ارسال شد."
                        Else
                            lblResult.Text &= " ❌ خطا در ارسال."
                        End If
                    End While

                    If count = 0 Then
                        lblResult.Text &= "<br />⚠️ هیچ رکوردی برای ارسال پیامک یافت نشد."
                    End If
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
            request.Timeout = 30000

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Dim result As String = reader.ReadToEnd()
                    Return result.Contains("OK") OrElse result.Contains("موفق")
                End Using
            End Using

        Catch ex As Exception
            lblResult.Text &= "<br />❌ خطا در ارسال پیامک: " & ex.Message
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

End Class