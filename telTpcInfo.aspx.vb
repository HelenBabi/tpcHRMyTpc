Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Class telTpcInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_grid()
    End Sub
    Private Sub load_grid()
        Dim sqlstr As String

        sqlstr = "SELECT * from perInfoVW"




        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridKartabl.DataSource = dt
        GridKartabl.DataBind()

    End Sub
End Class
