Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsEllat
    Public Function GetAllEllat(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     ellatCode, EllatName, CodeClosed  FROM tblEllat  WHERE (EllatCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     EllatCode, EllatName, CodeClosed  FROM tblEllat  WHERE (EllatCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function
    Public Function GetAllEllatDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      ellatCode, ellatName, CodeClosed  FROM tblEllatType  WHERE  (CodeClosed = 0) order by ellatCode "
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function

    Public Function GetEllat(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     EllatCode, EllatName, CodeClosed   FROM tblEllat  WHERE (CodeClosed = 0) AND (EllatCode <> 0) AND (EllatCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     EllatCode, EllatName, CodeClosed   FROM tblEllat  WHERE (EllatCode <> 0) AND (EllatCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function

End Class
