Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Partial Class resturan1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
       
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim sqlstr As String = ""
        Dim dtb As New DataTable
       
        sqlstr = "SELECT TOP (10) CardId, Date, DeviceCode, person, Id " & _
        " FROM resturanVW         WHERE(DeviceCode = 1) ORDER BY Id DESC"
        dtb = ClsDataTable.GetDataTableBoad5(sqlstr)
        If dtb.Rows.Count > 0 Then
            DataList1.DataSource = dtb
            DataList1.DataBind()
        End If

    End Sub
End Class
