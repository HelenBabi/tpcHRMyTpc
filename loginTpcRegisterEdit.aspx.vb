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
Partial Class loginTpcRegisterEdit
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
    

    Private Sub editLogin()

        Dim Clsuser_ As New user_login
        Dim dt As New DataTable


        dt = Clsuser_.GetUserok(1, 1)
        If dt.Rows.Count > 0 Then
            Session("EOK") = "1"
            Session("idReg") = dt.Rows(0).Item("idReg").ToString
            Response.Redirect("tpcRegisterStepEdit.aspx")
        Else
            msg_box("یافت نشد")
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
