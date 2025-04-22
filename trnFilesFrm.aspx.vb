Imports System.Data
Imports System.Data.SqlClient
Partial Class trnFilesFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_DataList()
    End Sub
    Private Sub load_DataList()
        Dim sqlstr As String
        'Request.QueryString("id")
        sqlstr = "SELECT        TOP (200) UnitsCode, UnitsName, CodeClosed, opid, ArcTypeCode " &
        " FROM tblUnits  WHERE        (CodeClosed = 0) AND (UnitsCode <> 0)"
      
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt
        DataList1.DataBind()
    End Sub
End Class
