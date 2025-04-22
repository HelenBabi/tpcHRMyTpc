Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class sportSettingCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        magsad, zarfiat, idTour,CodeClosed,sahm  FROM            tblTour  WHERE (idTour <> 0) AND (CodeClosed = 0)  order by idTour Desc "
        If deactive = True Then
            sqlstr = "SELECT        magsad, zarfiat, idTour,CodeClosed,sahm   FROM            tblTour  WHERE (idTour <> 0)  and (CodeClosed = 1) order by idTour Desc "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        magsad, zarfiat, idTour,CodeClosed,sahm   FROM            tblTour  WHERE  (CodeClosed = 0) order by idTour Desc "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        magsad, zarfiat, idTour,CodeClosed,sahm   FROM            tblTour  WHERE (CodeClosed = 0) AND (idTour <> 0) AND (idTour = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT        magsad, zarfiat, idTour,CodeClosed,sahm   FROM            tblTour  WHERE  (idTour = " & i & ") and (CodeClosed = 1)"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
