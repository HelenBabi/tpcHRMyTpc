Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class sugWorkTableAllFrm

    Inherits System.Web.UI.Page

    Private Sub loadGrid()
        Dim sqlstr As String = "SELECT        * " & _
        " FROM sugGrideUserVW order by sugID Desc"
        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridWorkTable.DataSource = dt
            GridWorkTable.DataBind()
        End If



    End Sub

    Protected Sub GridWorkTable_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridWorkTable.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
      


        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value2 As Decimal = CDec(e.GetValue("sugStatusID"))
        If value2 = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.Orange


        ElseIf value2 = 8 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PaleVioletRed

        ElseIf value2 = 10 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Yellow
        ElseIf value2 = 11 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Orange
        ElseIf value2 = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightGreen
        ElseIf value2 = 3 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Red
        End If

    End Sub


    Protected Sub GridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridWorkTable.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("sugID") = e.CommandArgs.CommandArgument
            Session("from") = "admin"
            Response.Redirect("sugDetailConversationFrm.aspx")
        End If

        'If e.CommandArgs.CommandName = "archiveee" Then

        '    Dim sqlstr As String

        '    sqlstr = "  UPDATE tblHerja SET             archivee = 1 " & _
        '                          "WHERE(erja_id = " & e.CommandArgs.CommandArgument & ")"
        '    upStat(sqlstr)
        '    load_grid_workers()

        '    conttArch()
        'End If
    End Sub



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadGrid()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
