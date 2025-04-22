Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsSoft
    Public Function GetAllSoft(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemSDtlCode, BaseSystemSDtlNmae, CodeClosed  FROM tblBaseSystemSDtl  WHERE (BaseSystemSDtlCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     BaseSystemSDtlCode, BaseSystemSDtlNmae, CodeClosed  FROM tblBaseSystemSDtl  WHERE (BaseSystemSDtlCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllSoftDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemSDtlCode, BaseSystemSDtlNmae, CodeClosed  FROM tblBaseSystemSDtl  WHERE  (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetSoft(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemSDtlCode, BaseSystemSDtlNmae, CodeClosed   FROM tblBaseSystemSDtl  WHERE (CodeClosed = 0) AND (BaseSystemSDtlCode <> 0) AND (BaseSystemSDtlCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     BaseSystemSDtlCode, BaseSystemSDtlNmae, CodeClosed   FROM tblBaseSystemSDtl  WHERE (BaseSystemSDtlCode <> 0) AND (BaseSystemSDtlCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
