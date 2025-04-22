Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsAnbarEdit
    Public Function GetAllUnits() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   * from vwAnbarDtl "


        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM tblUnits  WHERE  (CodeClosed = 0) order by UnitsName "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    *  FROM vwAnbarDtl  WHERE (IddtlKala = " & i & ")"
       
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
