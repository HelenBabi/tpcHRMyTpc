Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBaseWork
    Public Function GetAllBaseWork(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblBaseWork.masir, tblBaseWork.BaseWorkCode, tblBaseWork.WorkTypeCode, tblBaseWork.BaseWorkName, tblBaseWork.CodeClosed, " & _
        " tblWorkType.WorkTypeName, tblBaseWork.showAssistant  FROM         tblBaseWork INNER JOIN " & _
        " tblWorkType ON tblBaseWork.WorkTypeCode = tblWorkType.WorkTypeCode " & _
 " WHERE     (tblBaseWork.BaseWorkCode <> 0) order by tblBaseWork.WorkTypeCode"
        If deactive = True Then
            sqlstr = "SELECT     tblBaseWork.masir,tblBaseWork.BaseWorkCode, tblBaseWork.WorkTypeCode, tblBaseWork.BaseWorkName, tblBaseWork.CodeClosed, " & _
        " tblWorkType.WorkTypeName, tblBaseWork.showAssistant  FROM         tblBaseWork INNER JOIN " & _
        " tblWorkType ON tblBaseWork.WorkTypeCode = tblWorkType.WorkTypeCode " & _
 " WHERE     (tblBaseWork.BaseWorkCode <> 0) order by tblBaseWork.WorkTypeCode "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseWorkDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblBaseWork.BaseWorkCode, tblBaseWork.WorkTypeCode, tblBaseWork.BaseWorkName, tblBaseWork.CodeClosed, " & _
        " tblWorkType.WorkTypeName, tblBaseWork.showAssistant  FROM         tblBaseWork INNER JOIN " & _
        " tblWorkType ON tblBaseWork.WorkTypeCode = tblWorkType.WorkTypeCode " & _
 " WHERE     (tblBaseWork.CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseWork(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    masir, BaseWorkCode, WorkTypeCode, BaseWorkName, CodeClosed, tblBaseWork.showAssistant   FROM tblBaseWork  WHERE (CodeClosed = 0) AND (BaseWorkCode <> 0) AND (BaseWorkCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     masir,BaseWorkCode, WorkTypeCode, BaseWorkName, CodeClosed, tblBaseWork.showAssistant   FROM tblBaseWork  WHERE (BaseWorkCode <> 0) AND (BaseWorkCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
