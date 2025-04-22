Imports System.Data
Imports System.Data.SqlClient
Partial Class resturan22
    Inherits System.Web.UI.Page



    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim sqlstr As String = ""
        Dim dt3 As New DataTable

        sqlstr = "SELECT TOP (10) CardId, Date, DeviceCode, person, Id " & _
          " FROM resturanVW         WHERE(DeviceCode = 4) ORDER BY Id DESC"
        dt3 = ClsDataTable.GetDataTableBoad5(sqlstr)
        If dt3.Rows.Count > 0 Then
            DataList1.DataSource = dt3
            DataList1.DataBind()
        End If

    End Sub
End Class
