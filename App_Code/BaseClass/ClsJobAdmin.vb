Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsJob
    Public Function GetAllJob(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblJob.id_job, tblJob.subject, tblJob.olaviat, tblJob.date_start, tblJob.time_start, tblJob.date_end, tblJob.time_end, tblJob.darsad, tblJob.note, " & _
                      " tblJob.file_url, tblJob.sender, tblJob.reciver, tblJob.stat, tblJob.ShowAll, tblJob.opid, tblJob.CodeClosed, tblJob.ArcTypeCode, " & _
                      " tblUsers.UserFamili + ' ' + tblUsers.UserLastName AS sender_, tblUsers_1.UserFamili + ' ' + tblUsers_1.UserLastName AS ricever_ " & _
                     "FROM     tblJob INNER JOIN        tblUsers ON tblJob.sender = tblUsers.UserCode INNER JOIN   tblUsers AS tblUsers_1 ON tblJob.reciver = tblUsers_1.UserCode " & _
        " WHERE (tblJob.CodeClosed = 0) order by tblJob.id_job DESC"
        If deactive = True Then
            sqlstr = "SELECT     tblJob.id_job, tblJob.subject, tblJob.olaviat, tblJob.date_start, tblJob.time_start, tblJob.date_end, tblJob.time_end, tblJob.darsad, tblJob.note, " & _
                         " tblJob.file_url, tblJob.sender, tblJob.reciver, tblJob.stat, tblJob.ShowAll, tblJob.opid, tblJob.CodeClosed, tblJob.ArcTypeCode, " & _
                         " tblUsers.UserFamili + ' ' + tblUsers.UserLastName AS sender_, tblUsers_1.UserFamili + ' ' + tblUsers_1.UserLastName AS ricever_ " & _
                        "FROM     tblJob INNER JOIN        tblUsers ON tblJob.sender = tblUsers.UserCode INNER JOIN   tblUsers AS tblUsers_1 ON tblJob.reciver = tblUsers_1.UserCode " & _
                " WHERE (tblJob.CodeClosed <> 0) order by tblJob.id_job DESC "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllJobDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     id_job, subject, olaviat, date_start, time_start, date_end, time_end, darsad, note, file_url, sender, reciver, stat, ShowAll, opid, CodeClosed, ArcTypeCode " & _
          "FROM         tblJob  WHERE  (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetJob(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    id_job, subject, olaviat, date_start, time_start, date_end, time_end, darsad, note, file_url, sender, reciver, stat, ShowAll, opid, CodeClosed, ArcTypeCode " & _
    " FROM         tblJob WHERE (CodeClosed = 0) AND (id_job <> 0) AND (id_job = " & i & ") "
        If deactive = True Then
            sqlstr = "SELECT     id_job, subject, olaviat, date_start, time_start, date_end, time_end, darsad, note, file_url, sender, reciver, stat, ShowAll, opid, CodeClosed, ArcTypeCode " & _
            " FROM         tblJob WHERE (id_job <> 0) AND (id_job = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function



End Class
