Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports DevExpress.Web
Imports System.Linq
Imports System.Web.UI.WebControls
Partial Class tpcRegisterEdit
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Session("okk") Is Nothing Then
        '    Response.Redirect("tpcRegisterStep1.aspx")
        'End If
        'hdn_tmp.Value = Session("regID")
        'If Me.IsPostBack = False Then
        '    load_cmbDocumentType()
        'End If
    End Sub
    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click

        cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim())

        If cptCaptcha.UserValidated Then

            loginnn()

        Else
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "کد امنیتی بدرستی وارد نشده است"
        End If
    End Sub
    
    Private Sub loginnn()
        Dim uname As String
        Dim upass As String
        Dim Clsuser_ As New user_login
        Dim dt As New DataTable

        Dim unam As String = LCase((reqIDtxt.Text))
        dt = Clsuser_.GetUserok(unam, codeMelliTxt.Text)
        If dt.Rows.Count > 0 Then
            uname = dt.Rows(0).Item("codePersoneli").ToString.Trim
            upass = dt.Rows(0).Item("pass").ToString.Trim
            If unam = uname And codeMelliTxt.Text = upass Then
                Session("userid") = dt.Rows(0).Item("ID").ToString
                Session("lname") = dt.Rows(0).Item("nam").ToString
                Session("fname") = dt.Rows(0).Item("family").ToString
                'Session("ax") = "images/person_pic/" & Session("codePersoneli") & ".jpg"
                Session("super_vizer") = dt.Rows(0).Item("supervizer")
                Session("unitID") = dt.Rows(0).Item("UnitsCode").ToString

                Session("contratTyp") = dt.Rows(0).Item("typ").ToString
                Session("codePersoneli") = dt.Rows(0).Item("codePersoneli").ToString.Trim
                Session("shift") = dt.Rows(0).Item("shift").ToString
                Session("modir") = dt.Rows(0).Item("isResort").ToString
                Session("jens") = dt.Rows(0).Item("jens").ToString

                'Dim sqlstr2 As String
                'sqlstr2 = "  UPDATE tblUsers  SET             oline  = 1 " & _
                '                    "WHERE(UserCode = " & Session("userid") & ")"
                'upStat(sqlstr2)



                'If dt.Rows(0).Item("serviceNo") = 0 Then

                '    Response.Redirect("transportationPersonel.aspx")

                'Else
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
