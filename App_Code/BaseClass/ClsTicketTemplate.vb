Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsTicketTemplate
    Public Function GetAllTicketTemplate(ByVal deactive As Boolean, ByVal usrid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, valuee, CodeClosed  FROM tblTicketTemplate  WHERE (idd <> 0) AND (CodeClosed = 0)  and userId=" & usrid & " "
        If deactive = True Then
            sqlstr = "SELECT     idd, valuee, CodeClosed  FROM tblTicketTemplate  WHERE (idd <> 0) and userId=" & usrid & " "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllTicketTemplateDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, valuee, CodeClosed  FROM tblTicketTemplate  WHERE  (CodeClosed = 0) order by valuee "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetTicketTemplate(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, valuee, CodeClosed   FROM tblTicketTemplate  WHERE (CodeClosed = 0) AND (idd <> 0) AND (idd = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     idd, valuee, CodeClosed   FROM tblTicketTemplate  WHERE (idd <> 0) AND (idd = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
