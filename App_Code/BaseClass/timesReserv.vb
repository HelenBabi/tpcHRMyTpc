Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class timesReserv
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     barnameTbl.CodeClosed,refahiLocationTbl.locationValue, barnameTbl.timeID, barnameTbl.valuee, barnameTbl.resturanID " & _
                   " FROM         refahiLocationTbl INNER JOIN " & _
                     " barnameTbl ON refahiLocationTbl.locID = barnameTbl.resturanID   WHERE (barnameTbl.timeID <> 0) AND (barnameTbl.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT    barnameTbl.CodeClosed, refahiLocationTbl.locationValue, barnameTbl.timeID, barnameTbl.valuee, barnameTbl.resturanID " & _
                     " FROM         refahiLocationTbl INNER JOIN " & _
                     " barnameTbl ON refahiLocationTbl.locID = barnameTbl.resturanID    WHERE (barnameTbl.timeID <> 0)  AND (barnameTbl.CodeClosed = 1)  "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     barnameTbl.CodeClosed,refahiLocationTbl.locationValue, barnameTbl.timeID, barnameTbl.valuee, barnameTbl.resturanID " & _
                  " FROM         refahiLocationTbl INNER JOIN " & _
                      " barnameTbl ON refahiLocationTbl.locID = barnameTbl.resturanID  FROM barnameTbl  WHERE  (barnameTbl.CodeClosed = 0)  "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     barnameTbl.CodeClosed,refahiLocationTbl.locationValue, barnameTbl.timeID, barnameTbl.valuee, barnameTbl.resturanID " & _
               " FROM         refahiLocationTbl INNER JOIN " & _
                     " barnameTbl ON refahiLocationTbl.locID = barnameTbl.resturanID     WHERE (barnameTbl.CodeClosed = 0) AND (barnameTbl.timeID <> 0) AND (barnameTbl.timeID = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT    barnameTbl.CodeClosed, refahiLocationTbl.locationValue, barnameTbl.timeID, barnameTbl.valuee, barnameTbl.resturanID " & _
                     " FROM         refahiLocationTbl INNER JOIN" & _
                     "  barnameTbl ON refahiLocationTbl.locID = barnameTbl.resturanID     WHERE (barnameTbl.timeID <> 0) AND (barnameTbl.timeID = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
