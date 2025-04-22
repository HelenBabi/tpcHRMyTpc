Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsSystemType
    Public Function GetAllSystemType(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SystemTypCode, SystemName, CodeClosed  FROM tblBaseSystemType  WHERE (SystemTypCode <> 0) AND (CodeClosed = 0) order by SystemTypCode"
        If deactive = True Then
            sqlstr = "SELECT     SystemTypCode, SystemName, CodeClosed  FROM tblBaseSystemType  WHERE (SystemTypCode <> 0) order by SystemTypCode "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllSystemTypeDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SystemTypCode, SystemName, CodeClosed  FROM tblBaseSystemType  WHERE  (CodeClosed = 0) order by SystemTypCode "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetSystemType(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SystemTypCode, SystemName, CodeClosed   FROM tblBaseSystemType  WHERE (CodeClosed = 0) AND (SystemTypCode <> 0) AND (SystemTypCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     SystemTypCode, SystemName, CodeClosed   FROM tblBaseSystemType  WHERE (SystemTypCode <> 0) AND (SystemTypCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
