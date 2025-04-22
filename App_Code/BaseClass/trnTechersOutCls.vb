Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class trnTechersOutCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM trnTechersOutTbl  WHERE (techID <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     *  FROM trnTechersOutTbl  WHERE (techID <> 0) AND (CodeClosed = 1)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM trnTechersOutTbl  WHERE  (CodeClosed = 0) order by UnitsName "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM trnTechersOutTbl  WHERE (CodeClosed = 0) AND (techID <> 0) AND (techID = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     *   FROM trnTechersOutTbl  WHERE (techID <> 0) AND (techID = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
