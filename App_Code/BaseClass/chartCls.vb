Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class chartCls
    Public Function GetAllBaseDomainUser(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from chartVW                 WHERE    (chartID <> 0) AND (CodeClosed = 0)"
        If deactive = True Then
            sqlstr = "SELECT *  from chartVW  WHERE     (chartID <> 0) AND (CodeClosed <> 0)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseDomainUserDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from chartTbl  WHERE     (chartID <> 0) AND (CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseDomainUser(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  * from chartTbl  WHERE     (chartID = " & i & ") AND (CodeClosed = 0)"
        If deactive = True Then
            sqlstr = "SELECT   * from chartTbl  WHERE     (chartID = " & i & ") AND (CodeClosed <> 0)"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
