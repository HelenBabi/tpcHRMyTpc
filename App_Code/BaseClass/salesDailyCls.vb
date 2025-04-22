Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class salesDailyCls
    Public Function GetSales(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        *  FROM            salesDailyPayReportVW where tarix between '" & az & "' and '" & taa & "'"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    
End Class
