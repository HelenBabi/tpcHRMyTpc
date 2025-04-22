Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class trnWorkTableFrm

    Inherits System.Web.UI.Page

    Private Sub loadGrid()
        Dim sqlstr As String = "SELECT  * FROM chartTbl"

        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            trnGridWorkTable.DataSource = dt
            trnGridWorkTable.DataBind()
        End If





        TreeView1.DataSource = dt

        Dim trnd As New TreeNode

        trnd.Text = dt.Rows(0).Item("chartName")
        trnd.Value = dt.Rows(0).Item("chartID")

        trnd.PopulateOnDemand = True

        trnd.SelectAction = TreeNodeSelectAction.Expand


        'TreeView1.chil.Add(trnd)







        TreeView1.DataBind()




    End Sub

    


    Protected Sub GridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles trnGridWorkTable.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("idd") = e.CommandArgs.CommandArgument
            Session("from") = "reciver"
            Response.Redirect("trnDoreDetailFrm.aspx")
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
