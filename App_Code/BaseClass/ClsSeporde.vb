Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsSeporde
    Public Function GetAllSeporde(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SepordeCode, SepordeName, CodeClosed  FROM tblSeporde  WHERE (SepordeCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     SepordeCode, SepordeName, CodeClosed  FROM tblSeporde  WHERE (SepordeCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function
    Public Function GetAllSepordeDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SepordeCode, Sepordevalue, CodeClosed  FROM tblSepordeType  WHERE  (CodeClosed = 0) order by Sepordevalue "
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function
    Public Function GetAllSepordeDDLCMB(ByVal nn As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SepordeCode, Sepordevalue, CodeClosed  FROM tblSepordeType  WHERE  SepordeCode = " & nn & "  "
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function
    Public Function GetSeporde(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SepordeCode, SepordeName, CodeClosed   FROM tblSeporde  WHERE (CodeClosed = 0) AND (SepordeCode <> 0) AND (SepordeCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     SepordeCode, SepordeName, CodeClosed   FROM tblSeporde  WHERE (SepordeCode <> 0) AND (SepordeCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function

End Class
