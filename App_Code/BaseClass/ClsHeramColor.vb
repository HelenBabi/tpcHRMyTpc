Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data


Public Class ClsHeramColor
    Public Function GetAllHeramColor(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     HeramColorCode, HeramColorName, CodeClosed  FROM tblHeramColor  WHERE (HeramColorCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     HeramColorCode, HeramColorName, CodeClosed  FROM tblHeramColor  WHERE (HeramColorCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetAllHeramColorDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     HeramColorCode, HeramColorName, CodeClosed  FROM tblHeramColor  WHERE     (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetHeramColor(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     HeramColorCode, HeramColorName, CodeClosed   FROM tblHeramColor  WHERE (CodeClosed = 0) AND (HeramColorCode <> 0) AND (HeramColorCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     HeramColorCode, HeramColorName, CodeClosed   FROM tblHeramColor  WHERE (HeramColorCode <> 0) AND (HeramColorCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
