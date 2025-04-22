Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBakhsh
    Public Function GetAllBakhsh(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BakhshCode, BakhshName, CodeClosed  FROM tblBakhsh  WHERE (BakhshCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     BakhshCode, BakhshName, CodeClosed  FROM tblBakhsh  WHERE (BakhshCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetAllBakhshDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BakhshCode, BakhshName, CodeClosed  FROM tblBakhsh  WHERE (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBakhsh(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BakhshCode, BakhshName, CodeClosed  FROM tblBakhsh  WHERE (CodeClosed = 0) AND (BakhshCode <> 0) AND (BakhshCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     BakhshCode, BakhshName, CodeClosed  FROM tblBakhsh  WHERE (BakhshCode <> 0) AND (BakhshCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
