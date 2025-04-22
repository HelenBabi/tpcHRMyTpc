Imports System.Data
Imports System.Data.SqlClient

Partial Class kartexTOkartexFrm
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            Session("UserId") = Session("userid")
            ' meno_acc()
        End If

    End Sub














    Private Sub save_final()
        Dim sqlstr As String

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        'Dim Clsuser_ As New user_login
        'dt = Clsuser_.GetUserok(Session("UserName"), txt_CurentPass.Text)

      
        sqlstr = "SELECT   *   FROM trnPersonelDoreTbl " & _
       " WHERE  codePersoneli = " & txt_CurentPass.Text & " "
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            sqlstr = "UPDATE    trnPersonelDoreTbl  SET   codePersoneli = " & txt_NewPass1.Text & "" & _
           " WHERE  codePersoneli = " & txt_CurentPass.Text & " "
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
            msg_box("سوابق با موفقیت به کد پرسنلی جدید انتقال شد")
            cnn.Close()
        Else
            msg_box("با این کد پرسنلی سوابق موجود نیست")
            Exit Sub
        End If


    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
       
            Response.Redirect("index.aspx")
    
    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        save_final()
    End Sub
End Class
