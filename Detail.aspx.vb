Imports System.Data
Imports System.Data.SqlClient
Partial Class Detail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadItems()
        ' Image2.ImageUrl = ("~\attach_files\" & Request.QueryString("id") & ".jpg")
        Image2.ImageUrl = "images/bazar/" & Request.QueryString("id") & ".jpg"
    End Sub


    Private Sub loadItems()
        Dim dt3 As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       * from bazarVW WHERE        (idd = " & Request.QueryString("id") & ")"
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        Label1.Text = dt3.Rows(0).Item("mozu").ToString
        nam.Text = dt3.Rows(0).Item("namm").ToString
        telf.Text = dt3.Rows(0).Item("teldaxeli").ToString
        tozih.Text = dt3.Rows(0).Item("tozihat").ToString
        tarix.Text = dt3.Rows(0).Item("tarix").ToString
        If dt3.Rows(0).Item("mobilShow").ToString = True Then
            mobile.Visible = True
            mobile.Text = dt3.Rows(0).Item("mobile").ToString
        End If
    End Sub
End Class
