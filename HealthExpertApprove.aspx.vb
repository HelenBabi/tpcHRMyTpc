Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization
Imports System.Diagnostics

Partial Class HealthExpertApprove
    Inherits System.Web.UI.Page

    Private _personCode As String = ""
    Private _messageType As String = "info"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            _personCode = Session("codePersoneli").ToString()

            If String.IsNullOrEmpty(_personCode) Then
                Response.Redirect("~/Login_2.aspx")
                Return
            End If

            LoadRequests()
        End If
    End Sub

    Private Sub LoadRequests()
        Try
            ' ====== فقط درخواست‌هایی که در انتظار تایید کارشناس بهداشت هستند ======
            Dim sqlstr As String = "SELECT " &
                                   "    r.ID, " &
                                   "    r.PersonCode, " &
                                   "    r.FullName, " &
                                   "    r.UnitName, " &
                                   "    r.ExaminationDate, " &
                                   "    dbo.ConvertGregorianToJalali(r.ExaminationDate) AS PersianDate, " &
                                   "    r.StatusCode, " &
                                   "    r.StatusDescription, " &
                                   "    r.CreateDate, " &
                                   "    CASE r.StatusCode " &
                                   "        WHEN 3 THEN N'در انتظار تایید کارشناس بهداشت' " &
                                   "        ELSE N'نامشخص' " &
                                   "    END AS StatusText " &
                                   "FROM exmReservationRequests r " &
                                   "WHERE r.ManagerCode = @PersonCode " &
                                   "  AND r.CurrentLevel = 2 " &
                                   "  AND r.StatusCode = 3 " &
                                   "ORDER BY r.CreateDate DESC"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.AddWithValue("@PersonCode", _personCode)
                    cmd.CommandTimeout = 60
                    cnn.Open()

                    Dim dt As New DataTable()
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)

                    dt.Columns.Add("RowNumber", GetType(Integer))
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("RowNumber") = i + 1
                    Next

                    gvRequests.DataSource = dt
                    gvRequests.DataBind()

                    If dt.Rows.Count = 0 Then
                        ShowMessage("ℹ️ هیچ درخواستی برای تایید وجود ندارد", "info")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    Protected Sub gvRequests_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim statusCode As Integer = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "StatusCode"))
            Dim btnApprove As Button = CType(e.Row.FindControl("btnApprove"), Button)
            Dim btnReject As Button = CType(e.Row.FindControl("btnReject"), Button)
            Dim txtComment As TextBox = CType(e.Row.FindControl("txtComment"), TextBox)

            ' فقط وضعیت 3 (در انتظار تایید کارشناس بهداشت) قابل تایید/رد است
            If statusCode <> 3 Then
                If btnApprove IsNot Nothing Then btnApprove.Visible = False
                If btnReject IsNot Nothing Then btnReject.Visible = False
                If txtComment IsNot Nothing Then txtComment.Visible = False
            End If
        End If
    End Sub

    Protected Sub gvRequests_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvRequests.RowCommand
        Try
            Dim button As Button = CType(e.CommandSource, Button)
            Dim row As GridViewRow = CType(button.NamingContainer, GridViewRow)
            Dim requestID As Integer = Convert.ToInt32(gvRequests.DataKeys(row.RowIndex).Value)

            Dim comment As String = ""
            Dim txtComment As TextBox = CType(row.FindControl("txtComment"), TextBox)
            If txtComment IsNot Nothing Then
                comment = txtComment.Text
            End If

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                cnn.Open()
                Using trans As SqlTransaction = cnn.BeginTransaction()
                    Try
                        If e.CommandName = "Reject" Then
                            ' رد توسط کارشناس بهداشت
                            Dim updateSql As String = "UPDATE exmReservationRequests " &
                                                       "SET StatusCode = 6, " &
                                                       "    StatusDescription = N'رد شده توسط کارشناس بهداشت', " &
                                                       "    HealthExpertComment = @Comment, " &
                                                       "    HealthExpertRejectDate = GETDATE(), " &
                                                       "    IsDone = 1, " &
                                                       "    UpdateDate = GETDATE() " &
                                                       "WHERE ID = @RequestID"

                            Using cmd As New SqlCommand(updateSql, cnn, trans)
                                cmd.Parameters.AddWithValue("@RequestID", requestID)
                                cmd.Parameters.AddWithValue("@Comment", comment)
                                cmd.ExecuteNonQuery()
                            End Using

                            trans.Commit()
                            ShowMessage("✅ درخواست با موفقیت رد شد", "success")
                            LoadRequests()
                            Return
                        End If

                        If e.CommandName = "Approve" Then
                            ' ====== تایید نهایی توسط کارشناس بهداشت ======
                            Dim updateSql As String = "UPDATE exmReservationRequests " &
                                                       "SET StatusCode = 4, " &
                                                       "    StatusDescription = N'تایید نهایی', " &
                                                       "    CurrentLevel = 3, " &
                                                       "    HealthExpertComment = @Comment, " &
                                                       "    HealthExpertApproveDate = GETDATE(), " &
                                                       "    IsDone = 1, " &
                                                       "    UpdateDate = GETDATE() " &
                                                       "WHERE ID = @RequestID"

                            Using cmd As New SqlCommand(updateSql, cnn, trans)
                                cmd.Parameters.AddWithValue("@RequestID", requestID)
                                cmd.Parameters.AddWithValue("@Comment", comment)
                                cmd.ExecuteNonQuery()
                            End Using

                            ' ====== درج پیامک در جدول smsTbl ======
                            InsertSmsReminder(requestID, cnn, trans)

                            trans.Commit()
                            ShowMessage("✅ درخواست با موفقیت تایید نهایی شد", "success")
                            LoadRequests()
                        End If

                    Catch ex As Exception
                        trans.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub InsertSmsReminder(ByVal requestID As Integer, ByVal cnn As SqlConnection, ByVal trans As SqlTransaction)
        Try
            ' دریافت اطلاعات پرسنل و تاریخ معاینه
            Dim sql As String = "SELECT " &
                                "    r.PersonCode, " &
                                "    r.FullName, " &
                                "    p.mobile, " &
                                "    r.ExaminationDate, " &
                                "    dbo.ConvertGregorianToJalali(r.ExaminationDate) AS PersianDate " &
                                "FROM exmReservationRequests r " &
                                "INNER JOIN personel p ON r.PersonCode = p.codePersoneli " &
                                "WHERE r.ID = @RequestID"

            Using cmd As New SqlCommand(sql, cnn, trans)
                cmd.Parameters.AddWithValue("@RequestID", requestID)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim mobile As String = reader("mobile").ToString()
                        Dim fullName As String = reader("FullName").ToString()
                        Dim persianDate As String = reader("PersianDate").ToString()
                        Dim examDate As DateTime = Convert.ToDateTime(reader("ExaminationDate"))

                        ' بررسی اینکه آیا فردا تاریخ معاینه است
                        Dim tomorrow As DateTime = DateTime.Now.AddDays(1).Date
                        If examDate.Date = tomorrow.Date Then
                            ' ساخت متن پیامک
                            Dim smsText As String = "سلام " & fullName & " همکار محترم،" & vbCrLf &
                                                    "فردا (" & persianDate & ") نوبت معاینه دوره‌ای شماست." & vbCrLf &
                                                    "لطفاً در ساعت مقرر در مرکز بهداشت حاضر باشید."

                            ' درج در جدول smsTbl
                            Dim insertSql As String = "INSERT INTO smsTbl (mobile, message, tarix, saat, stat) " &
                                                      "VALUES (@Mobile, @Message, @Tarix, @Saat, 0)"

                            Using insertCmd As New SqlCommand(insertSql, cnn, trans)
                                insertCmd.Parameters.AddWithValue("@Mobile", mobile)
                                insertCmd.Parameters.AddWithValue("@Message", smsText)
                                insertCmd.Parameters.AddWithValue("@Tarix", persianDate)
                                insertCmd.Parameters.AddWithValue("@Saat", DateTime.Now.ToString("HH:mm"))
                                insertCmd.ExecuteNonQuery()

                                EventLog.WriteEntry("HealthExpertApprove", "پیامک یادآوری برای " & fullName & " درج شد", EventLogEntryType.Information)
                            End Using
                        End If
                    End If
                End Using
            End Using

        Catch ex As Exception
            ' ثبت خطا در لاگ اما اجازه ادامه بده
            EventLog.WriteEntry("HealthExpertApprove", "خطا در درج پیامک: " & ex.Message, EventLogEntryType.Error)
        End Try
    End Sub

    Protected Sub gvRequests_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvRequests.PageIndexChanging
        gvRequests.PageIndex = e.NewPageIndex
        LoadRequests()
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/index.aspx")
    End Sub

    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
                pnlMessage.CssClass = "exm-alert exm-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                pnlMessage.CssClass = "exm-alert exm-alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
                pnlMessage.CssClass = "exm-alert exm-alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                pnlMessage.CssClass = "exm-alert exm-alert-info"
        End Select

        pnlMessage.Visible = True
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class