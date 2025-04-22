Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsTicketBaseWork
    Public Function GetAllBaseWork(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   tblTicketWorks.mojavez, tblTicketWorks.BaseWorkCode, tblTicketWorks.WorkTypeCode, tblTicketWorks.BaseWorkName, tblTicketWorks.CodeClosed, " & _
        " tblWorkType.WorkTypeName  FROM         tblTicketWorks INNER JOIN " & _
        " tblWorkType ON tblTicketWorks.WorkTypeCode = tblWorkType.WorkTypeCode " & _
 " WHERE     (tblTicketWorks.BaseWorkCode <> 0) order by tblTicketWorks.WorkTypeCode"
        If deactive = True Then
            sqlstr = "SELECT    tblTicketWorks.mojavez, tblTicketWorks.BaseWorkCode, tblTicketWorks.WorkTypeCode, tblTicketWorks.BaseWorkName, tblTicketWorks.CodeClosed, " & _
        " tblWorkType.WorkTypeName  FROM         tblTicketWorks INNER JOIN " & _
        " tblWorkType ON tblTicketWorks.WorkTypeCode = tblWorkType.WorkTypeCode " & _
 " WHERE     (tblTicketWorks.BaseWorkCode <> 0) order by tblTicketWorks.WorkTypeCode "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseWorkDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblTicketWorks.mojavez, tblTicketWorks.BaseWorkCode, tblTicketWorks.WorkTypeCode, tblTicketWorks.BaseWorkName, tblTicketWorks.CodeClosed, " & _
        " tblWorkType.WorkTypeName  FROM         tblTicketWorks INNER JOIN " & _
        " tblWorkType ON tblTicketWorks.WorkTypeCode = tblWorkType.WorkTypeCode " & _
 " WHERE     (tblTicketWorks.CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseWork(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblTicketWorks.mojavez, BaseWorkCode, WorkTypeCode, BaseWorkName, CodeClosed   FROM tblTicketWorks  WHERE (CodeClosed = 0) AND (BaseWorkCode <> 0) AND (BaseWorkCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT    tblTicketWorks.mojavez, BaseWorkCode, WorkTypeCode, BaseWorkName, CodeClosed   FROM tblTicketWorks  WHERE (BaseWorkCode <> 0) AND (BaseWorkCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
