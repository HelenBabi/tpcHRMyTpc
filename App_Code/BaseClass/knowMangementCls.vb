Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class knowMangementCls
    Public Function GetAllUnits(ByVal deactive As Boolean, ByVal userId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        tblWorkType.WorkTypeName, knowlegmanagmentTbl.CodeClosed, knowlegmanagmentTbl.userID, knowlegmanagmentTbl.kmID,  " & _
       " knowlegmanagmentTbl.subject, knowlegmanagmentTbl.showAll, knowlegmanagmentTbl.typ " & _
                      " FROM            tblWorkType INNER JOIN " & _
                        " knowlegmanagmentTbl ON tblWorkType.WorkTypeCode = knowlegmanagmentTbl.typ " & _
                         "WHERE        (knowlegmanagmentTbl.CodeClosed = 0) AND (knowlegmanagmentTbl.userID = " & userId & ")"
        If deactive = True Then
            sqlstr = "SELECT        tblWorkType.WorkTypeName, knowlegmanagmentTbl.CodeClosed, knowlegmanagmentTbl.userID, knowlegmanagmentTbl.kmID,  " & _
        " knowlegmanagmentTbl.subject, knowlegmanagmentTbl.showAll, knowlegmanagmentTbl.typ " & _
                       " FROM            tblWorkType INNER JOIN " & _
                         " knowlegmanagmentTbl ON tblWorkType.WorkTypeCode = knowlegmanagmentTbl.typ " & _
                          "WHERE        (knowlegmanagmentTbl.CodeClosed = 1) AND (knowlegmanagmentTbl.userID = " & userId & ")"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM knowlegmanagmentTbl  WHERE  (CodeClosed = 0) order by subject "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM knowlegmanagmentTbl  WHERE (CodeClosed = 0) AND (kmID <> 0) AND (kmID = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     *   FROM knowlegmanagmentTbl  WHERE (kmID <> 0) AND (kmID = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
   
    Public Function GetAllUnitsBrowse() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   * from knowlegGridVW ORDER BY kmID DESC"
       
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsBrowseBySubject(ByVal subject As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   * from knowlegGridVW WHERE     (subject  LIKE N'%" & subject & "%') "

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetJobs(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM knowlegmanagmentTbl  WHERE (CodeClosed = 0) AND (kmID <> 0) AND (kmID = " & i & ") And (job = 1)"
        If deactive = True Then
            sqlstr = "SELECT     *   FROM knowlegmanagmentTbl  WHERE (kmID <> 0) AND (kmID = " & i & ") And (job = 1)"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllJobs(ByVal deactive As Boolean, ByVal userId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        CodeClosed, userID, kmID, subject, showAll, typ  " & _
       " FROM knowlegmanagmentTbl  " & _
            "WHERE(CodeClosed = 0) And (userId = " & userId & ") And (job = 1) And (groupUserType = 0)"


        If deactive = True Then
            sqlstr = "SELECT        CodeClosed, userID, kmID, subject, showAll, typ  " & _
   " FROM knowlegmanagmentTbl  " & _
        "WHERE(CodeClosed = 1) And (userId = " & userId & ") And (job = 1) And (groupUserType = 0)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
