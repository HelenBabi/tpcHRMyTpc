Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsHolder
    Public Function GetAllHolder(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     personCode,HolderCode,HolderName, HolderFamili, CodeClosed, opid, ArcTypeCode  FROM tblHolder  WHERE (HolderCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     personCode,HolderCode,HolderName, HolderFamili, CodeClosed, opid, ArcTypeCode  FROM tblHolder  WHERE (HolderCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllHolderDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     personCode,HolderCode, HolderFamili + ' ' + HolderName + ' ' + personCode as names, CodeClosed, opid, ArcTypeCode  FROM tblHolder  WHERE  (CodeClosed = 0) ORDER BY HolderFamili "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetHolder(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     personCode,HolderCode,HolderName, HolderFamili, CodeClosed, opid, ArcTypeCode   FROM tblHolder  WHERE (CodeClosed = 0) AND (HolderCode <> 0) AND (HolderCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     personCode,HolderCode,HolderName, HolderFamili, CodeClosed, opid, ArcTypeCode   FROM tblHolder  WHERE (HolderCode <> 0) AND (HolderCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
