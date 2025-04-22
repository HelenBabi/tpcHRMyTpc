Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class typeQouataCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM contratType  WHERE (id <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     *  FROM contratType  WHERE (id <> 0)  AND (CodeClosed = 1) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    *  FROM  contratType  WHERE  (CodeClosed = 0) order by value "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM contratType  WHERE (CodeClosed = 0) AND (id <> 0) AND (id = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     *   FROM contratType  WHERE (TypeID <> 0) AND (id = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
