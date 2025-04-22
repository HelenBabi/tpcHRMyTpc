Imports System.Data
Imports System.Data.SqlClient

Partial Class FrmKartabl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        load_grid()
    End Sub


    Private Sub load_grid()
        Dim ClsB_ As New ClsKartabl
        Dim dt As New DataTable
        dt = ClsB_.GetActive()
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Response.Redirect("FrmVisit1.aspx" & "?id_=" & e.CommandArgs.CommandArgument.ToString())
        End If
    End Sub
End Class
