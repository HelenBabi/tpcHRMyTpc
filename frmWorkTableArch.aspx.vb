Imports System.Data
Imports System.Data.SqlClient
Partial Class frmWorkTableArch
    Inherits System.Web.UI.Page
    Dim dtworker, dtusers As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_.aspx")
        End If
        load_grid_workers()

    End Sub

    Private Sub load_grid_workers()

        Dim ClsB_ As New ClsBrowsWorks

        dtworker = ClsB_.GetBrowsTicketsArch(Session("WorkType"))

        GridKartabl.DataSource = dtworker
        GridKartabl.DataBind()
        Dim cntt As Integer = dtworker.Rows.Count
        'lblTotal.Text = "تعداد كل = " & cntt
    End Sub





    Protected Sub GridKartabl_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridKartabl.RowCommand
        Session("tiketId") = e.CommandArgs.CommandArgument
        Response.Redirect("frmTicketWorker.aspx")
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        If Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")
        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        ElseIf Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")
        End If
    End Sub

  
End Class
