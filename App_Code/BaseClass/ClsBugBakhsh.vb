Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBugBakhsh
    Public Function GetAllBugBakhsh(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblBugBakhsh.BugBakhshCode, tblBugBakhsh.BugCode, tblBugBakhsh.BakhshCode, tblBugBakhsh.CodeClosed, tblBugBakhsh.opid, tblBugBakhsh.ArcTypeCode, " & _
        " tblBug.BugName, tblBakhsh.BakhshName   FROM         tblBugBakhsh INNER JOIN " & _
                      " tblBug ON tblBugBakhsh.BugCode = tblBug.BugCode INNER JOIN  tblBakhsh ON tblBugBakhsh.BakhshCode = tblBakhsh.BakhshCode  WHERE  (tblBugBakhsh.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     tblBugBakhsh.BugBakhshCode, tblBugBakhsh.BugCode, tblBugBakhsh.BakhshCode, tblBugBakhsh.CodeClosed, tblBugBakhsh.opid, tblBugBakhsh.ArcTypeCode, " & _
        " tblBug.BugName, tblBakhsh.BakhshName   FROM         tblBugBakhsh INNER JOIN " & _
                      " tblBug ON tblBugBakhsh.BugCode = tblBug.BugCode INNER JOIN  tblBakhsh ON tblBugBakhsh.BakhshCode = tblBakhsh.BakhshCode"

        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBugBakhsh(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblBugBakhsh.BugBakhshCode, tblBugBakhsh.BugCode, tblBugBakhsh.BakhshCode, tblBugBakhsh.CodeClosed, tblBugBakhsh.opid, tblBugBakhsh.ArcTypeCode, " & _
        " tblBug.BugName, tblBakhsh.BakhshName   FROM         tblBugBakhsh INNER JOIN " & _
                      " tblBug ON tblBugBakhsh.BugCode = tblBug.BugCode INNER JOIN  tblBakhsh ON tblBugBakhsh.BakhshCode = tblBakhsh.BakhshCode  WHERE  (tblBugBakhsh.CodeClosed = 0) and (BugBakhshCode =" & i & ")"

        If deactive = True Then
            sqlstr = "SELECT     tblBugBakhsh.BugBakhshCode, tblBugBakhsh.BugCode, tblBugBakhsh.BakhshCode, tblBugBakhsh.CodeClosed, tblBugBakhsh.opid, tblBugBakhsh.ArcTypeCode, " & _
        " tblBug.BugName, tblBakhsh.BakhshName   FROM         tblBugBakhsh INNER JOIN " & _
                      " tblBug ON tblBugBakhsh.BugCode = tblBug.BugCode INNER JOIN  tblBakhsh ON tblBugBakhsh.BakhshCode = tblBakhsh.BakhshCode  WHERE  (BugBakhshCode =" & i & ")"

        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
