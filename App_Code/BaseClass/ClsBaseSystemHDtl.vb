Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBaseSystemHDtl
    Public Function GetAllBaseSystemHDtl(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed  FROM tblBaseSystemHDtl  WHERE (BaseSystemHDtlCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed  FROM tblBaseSystemHDtl  WHERE (BaseSystemHDtlCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseSystemHDtlDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed  FROM tblBaseSystemHDtl  WHERE  (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseSystemHDtl(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed   FROM tblBaseSystemHDtl  WHERE (CodeClosed = 0) AND (BaseSystemHDtlCode <> 0) AND (BaseSystemHDtlCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed   FROM tblBaseSystemHDtl  WHERE (BaseSystemHDtlCode <> 0) AND (BaseSystemHDtlCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
