Imports System.Data
Imports System.Data.SqlClient
Partial Class bazarfrm
    Inherits System.Web.UI.Page
    Dim dt3 As New DataTable
    Dim sqlstr As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
      " FROM tblMainPost WHERE        (stat = 1) ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

  
End Class
