Imports System.Data
Imports System.Data.SqlClient
Partial Class frmTicketUserEdit
    Inherits System.Web.UI.Page

    Private Sub save_final()
       
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand


        cmm.CommandText = "Prcuserinfo"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@loc", SqlDbType.NVarChar, 100)
        cmm.Parameters("@loc").Value = txtLocation.Text

        cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 100)
        cmm.Parameters("@nam").Value = txtName.Text

        cmm.Parameters.Add("@UserFamili", SqlDbType.NVarChar, 100)
        cmm.Parameters("@UserFamili").Value = txtlName.Text


        cmm.Parameters.Add("@codePesonel", SqlDbType.Int, 6)
        cmm.Parameters("@codePesonel").Value = txtCodepersonel.Text

        cmm.Parameters.Add("@telfon", SqlDbType.VarChar, 50)
        cmm.Parameters("@telfon").Value = txtTell.Text

        cmm.Parameters.Add("@semat", SqlDbType.VarChar, 50)
        cmm.Parameters("@semat").Value = txtSemat.Text


        cmm.Parameters.Add("@uemail", SqlDbType.VarChar, 50)
        cmm.Parameters("@uemail").Value = txtPcUserEmail.Text


        cmm.Parameters.Add("@Mobil", SqlDbType.VarChar, 50)
        cmm.Parameters("@Mobil").Value = txtMobile.Text

        cmm.Parameters.Add("@genderr", SqlDbType.SmallInt)
        cmm.Parameters("@genderr").Value = cmbGender.SelectedValue

        cmm.Parameters.Add("@smss", SqlDbType.Bit)
        cmm.Parameters("@smss").Value = 0

        cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
        cmm.Parameters("@userid").Value = Session("UserId")

        cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
        cmm.Parameters("@pm").Direction = ParameterDirection.Output

        cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
        cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
        cmm.Connection = cnn

        Dim result As Integer
        cmm.ExecuteNonQuery()
        result = cmm.Parameters("@ResultCode").Value
        If result = 0 Then
            msg_box(" اطلاعات پروفایل شما ویرایش شد ")
            Session("lname") = txtName.Text
            Session("fname") = txtlName.Text
            Session("namnull") = "noo"
        Else
            msg_box(cmm.Parameters("@pm").Value)
        End If


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




    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            load_userinfo()
        End If


    End Sub
    Private Sub load_userinfo()
        Dim dt As DataTable
        Dim Clsuserinfo As New user_login

        dt = Clsuserinfo.GetUserinfo(Session("userid"))
        txtCodepersonel.Text = dt.Rows(0).Item("codePersoneli").ToString
        txtlName.Text = dt.Rows(0).Item("family").ToString
        txtMobile.Text = dt.Rows(0).Item("mobile").ToString
        txtName.Text = dt.Rows(0).Item("nam").ToString
        txtPcUserEmail.Text = dt.Rows(0).Item("emaill").ToString
        'txtSemat.Text = dt.Rows(0).Item("semat").ToString
        txtTell.Text = dt.Rows(0).Item("teldaxeli").ToString

        cmbGender.SelectedValue = dt.Rows(0).Item("jens")
      
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Call save_final()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
       
            Response.Redirect("index.aspx")

    End Sub
End Class
