Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsVisitor
    Public Function GetAllVisitor(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     VisitorCode, VisitorName,VisitorFamili, CodeClosed  FROM tblVisitor  WHERE (VisitorCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     VisitorCode, VisitorName,VisitorFamili, CodeClosed  FROM tblVisitor  WHERE (VisitorCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetVisitor(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     VisitorCode, VisitorName,VisitorFamili, CodeClosed   FROM tblVisitor  WHERE (CodeClosed = 0) AND (VisitorCode <> 0) AND (VisitorCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     VisitorCode, VisitorName,VisitorFamili, CodeClosed   FROM tblVisitor  WHERE (VisitorCode <> 0) AND (VisitorCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
