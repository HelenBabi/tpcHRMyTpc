Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class addedvalueCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      idd, arzeshValue, az, ta, state  FROM         arzeshAfzudeTbl"
        'If deactive = True Then
        '    sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM tblUnits  WHERE (UnitsCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      idd, arzeshValue, az, ta, state  FROM         arzeshAfzudeTbl order by idd "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      idd, arzeshValue, az, ta, state  FROM         arzeshAfzudeTbl  WHERE  (idd = " & i & ")"
        'If deactive = True Then
        '    sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM tblUnits  WHERE (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
        'End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
