Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms

Public Class ClsWorker
    
    Public Function GetAllWorker(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     WorkerCode, WorkerName, WorkerFamili , CodeClosed  FROM tblWorker  WHERE (WorkerCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     WorkerCode, WorkerName, CodeClosed  FROM tblWorker  WHERE (WorkerCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetAllWorkerDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT     WorkerCode, WorkerName + ' ' + WorkerFamili AS names, CodeClosed  FROM tblWorker  WHERE (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetWorker(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     WorkerCode, WorkerName, WorkerFamili , CodeClosed  FROM tblWorker  WHERE (CodeClosed = 0) AND (WorkerCode <> 0) AND (WorkerCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     WorkerCode, WorkerName, CodeClosed  FROM tblWorker  WHERE (WorkerCode <> 0) AND (WorkerCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class

