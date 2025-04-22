Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class turMessageCls
    Public Function GetAllTicketTemplate(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, valuee, CodeClosed  FROM turMessageTbl  WHERE (idd <> 0) AND (CodeClosed = 0)  "
        If deactive = True Then
            sqlstr = "SELECT     idd, valuee, CodeClosed  FROM turMessageTbl  WHERE (idd <> 0)  "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllTicketTemplateDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, valuee, CodeClosed  FROM turMessageTbl  WHERE  (CodeClosed = 0) order by valuee "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetTicketTemplate(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, valuee, CodeClosed   FROM turMessageTbl  WHERE (CodeClosed = 0) AND (idd <> 0) "
        If deactive = True Then
            sqlstr = "SELECT     idd, valuee, CodeClosed   FROM turMessageTbl  WHERE (idd <> 0) "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
