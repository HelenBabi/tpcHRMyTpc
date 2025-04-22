Imports System.Web.UI.WebControls.TreeView
Imports System.Data
Imports System.Data.SqlClient
Partial Class login_
    Inherits System.Web.UI.Page
    Dim dt As New DataTable


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim uname As String
        Dim upass As String
        Dim Clsuser_ As New user_login

        Dim unam As String = LCase((TextBox1.Text))
        dt = Clsuser_.GetUserok(unam, TextBox2.Text)
        If dt.Rows.Count > 0 Then
            uname = dt.Rows(0).Item("UserName_").ToString.Trim
            upass = dt.Rows(0).Item("UserPass_").ToString.Trim
            If unam = uname And TextBox2.Text = upass Then
                Session("userid") = dt.Rows(0).Item("UserCode").ToString
                Session("lname") = dt.Rows(0).Item("UserLastName").ToString
                Session("fname") = dt.Rows(0).Item("UserFamili").ToString
                Session("ax") = dt.Rows(0).Item("UserPic").ToString
                Session("WorkerCode") = Val(dt.Rows(0).Item("WorkerCode").ToString)
                Session("WorkType") = dt.Rows(0).Item("WorkType").ToString

                'Dim sqlstr2 As String
                'sqlstr2 = "  UPDATE tblUsers  SET             oline  = 1 " & _
                '                    "WHERE(UserCode = " & Session("userid") & ")"
                'upStat(sqlstr2)

                'If dt.Rows(0).Item("UserLastName").ToString = "" Or dt.Rows(0).Item("UserFamili").ToString = "" Then
                '    Session("namnull") = "yess"
                '    Response.Redirect("frmTicketUserEdit.aspx")
                'End If

                'If dt.Rows(0).Item("WorkType").ToString = "100" Then
                '    Session("UserType") = "user"
                '    Response.Redirect("frmWorkTableUsers.aspx")

                'ElseIf dt.Rows(0).Item("UsersAdmin") = True Then
                '    Session("UserType") = "admn"
                '    Response.Redirect("frmWorkTableAdmin.aspx")


                'ElseIf dt.Rows(0).Item("UserSuperVizer") = True Then
                '    Session("UserType") = "worker"
                '    Response.Redirect("index.aspx")

                'End If
                Response.Redirect("index.aspx")
            End If
           
        Else
            msg_box("نام کاربری یا رمز عبور اشتباه می باشد.")
        End If


    End Sub
    Private Sub upStat(sqlstr2 As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr2, cnn)
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

   

    'Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    TextBox1.Attributes.Add("onkeyup", "toLowercase()")
    'End Sub
End Class
