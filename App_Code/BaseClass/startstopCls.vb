Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class startstopCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      idd, startTime, stopTime, CodeClosed, opid, ArcTypeCode FROM startStopTbl WHERE (idd <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT       idd, startTime, stopTime, CodeClosed, opid, ArcTypeCode FROM startStopTbl  WHERE (idd <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      idd, startTime, stopTime, CodeClosed, opid, ArcTypeCode FROM startStopTbl  WHERE  (CodeClosed = 0)  "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, startTime, stopTime, CodeClosed, opid, ArcTypeCode FROM startStopTbl   WHERE (CodeClosed = 0) AND (idd <> 0) AND (idd = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT      idd, startTime, stopTime, CodeClosed, opid, ArcTypeCode FROM startStopTbl  WHERE (idd <> 0) AND (idd = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
