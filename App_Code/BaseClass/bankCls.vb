Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class bankCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM bank_  WHERE (bank_id <> 1) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     *  FROM bank_  WHERE (bank_id <> 1)  AND (CodeClosed = 1)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM bank_  WHERE  (CodeClosed = 0) order by UnitsName "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM bank_  WHERE (CodeClosed = 0) AND (bank_id <> 1) AND (bank_id = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     *   FROM bank_  WHERE (bank_id <> 1) AND (bank_id = " & i & ") "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
