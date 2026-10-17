Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class trnMahiatCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM trnMahiatTbl  WHERE (ID <> 1) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     *  FROM trnMahiatTbl  WHERE (ID <> 1)  AND (CodeClosed = 1)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM trnMahiatTbl  WHERE  (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM trnMahiatTbl  WHERE (CodeClosed = 0) AND (ID <> 1) AND (ID = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     *   FROM trnMahiatTbl  WHERE (ID <> 1) AND (ID = " & i & ") "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
