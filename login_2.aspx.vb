Imports System.Data
Imports System.Data.SqlClient
Partial Class light_login2
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'If IsPostBack Then

        '    If Convert.ToString(Request.Form("__EVENTARGUMENT")) = "Submit" Then
        '        MsgBox("okk")
        '    End If
        'End If
    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
     
        'cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim())

        'If cptCaptcha.UserValidated Then

        loginnn()

        'Else
        '    lblErrorMessage.ForeColor = System.Drawing.Color.Red
        '    lblErrorMessage.Text = "کد امنیتی بدرستی وارد نشده است"
        'End If
    End Sub
    Private Sub loginnn()
        Dim uname As String
        Dim upass As String
        Dim Clsuser_ As New user_login

        Dim unam As String = LCase((TextBox1.Text))
        dt = Clsuser_.GetUserok(unam, TextBox2.Text)
        If dt.Rows.Count > 0 Then
            uname = dt.Rows(0).Item("codePersoneli").ToString.Trim
            upass = dt.Rows(0).Item("pass").ToString.Trim
            If unam = uname And TextBox2.Text = upass Then
                Session("userid") = dt.Rows(0).Item("ID").ToString
                Session("lname") = dt.Rows(0).Item("nam").ToString
                Session("fname") = dt.Rows(0).Item("family").ToString
                'Session("ax") = "images/person_pic/" & Session("codePersoneli") & ".jpg"
                Session("super_vizer") = dt.Rows(0).Item("supervizer")
                Session("omurID") = dt.Rows(0).Item("omurID").ToString
                Session("unitID") = dt.Rows(0).Item("UnitsCode").ToString
                Session("contratTyp") = dt.Rows(0).Item("typ").ToString
                Session("codePersoneli") = dt.Rows(0).Item("codePersoneli").ToString.Trim
                Session("shift") = dt.Rows(0).Item("shift").ToString
                Session("modir") = dt.Rows(0).Item("modir").ToString
                Session("jens") = dt.Rows(0).Item("jens").ToString

                Session("erja") = dt.Rows(0).Item("erja").ToString
                Session("ok") = dt.Rows(0).Item("ok").ToString

                Session("calncel") = dt.Rows(0).Item("calncel").ToString

                Session("sms") = dt.Rows(0).Item("sms").ToString

                Dim sqlstr2 As String
                sqlstr2 = "SELECT         chartName     FROM chartTbl WHERE        (codePersoneli = " & dt.Rows(0).Item("codePersoneli").ToString.Trim & ")"
                Dim dtp As New DataTable
                dtp = ClsDataTable.GetDataTable(sqlstr2)
                If dtp.Rows.Count > 0 Then
                    Session("semat") = dtp.Rows(0).Item("chartName").ToString
                Else
                    Session("semat") = "-"
                End If


                Response.Redirect("index.aspx")
                'End If

            End If
        Else
            msg_box("نام کاربری یا رمز عبور اشتباه می باشد.")
        End If
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
