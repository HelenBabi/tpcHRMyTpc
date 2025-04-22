Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class sugAdminWorkTableFrm

    Inherits System.Web.UI.Page

    Private Sub loadGrid()
        Dim sqlstr As String = "SELECT        sugID, subject, sugType, sugIncome, realIncom, DescStatus, sugMethod, benefits, Description, sugDate, sugTime, codePersoneli, sugTypeName, statusValue " & _
        " FROM sugGrideUserVW "
        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridWorkTable.DataSource = dt
            GridWorkTable.DataBind()
        End If



    End Sub








    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadGrid()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
