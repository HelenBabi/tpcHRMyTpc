Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsMaxOpid
    Public Function GetMaxOpid() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "		SELECT  MAX(opid) as maxxx FROM  tblOpId "

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function


End Class
