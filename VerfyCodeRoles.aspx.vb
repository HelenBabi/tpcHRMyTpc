Imports System.Data
Imports System.Data.SqlClient
Partial Class VerfyCodeRoles
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Protected Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim code As String = txtCode.Text.Trim()
        Dim mobile As String = Request.QueryString("mobile")
        Dim sqlstrr As String = "SELECT TOP 1 * FROM PasswordResetCodes              WHERE Mobile=N'" & mobile & "' AND VerificationCode=N'" & code & "' AND ExpireTime>GETDATE()              ORDER BY ID DESC"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstrr)

        If dt.Rows.Count = 0 Then
            lblMsg.Text = "کد اشتباه است یا منقضی شده."
            Return
        End If
        txtCode.Visible = False
        btnVerify.Visible = False
        'Dim newPassword As String = Guid.NewGuid().ToString().Substring(0, 8)

       


    End Sub
    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub
End Class