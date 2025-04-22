Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBaseModel
    Public Function GetAllBaseModel(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      tblBaseModel.BaseModelCode, tblBaseModel.BaseModelName, tblBaseModel.CodeClosed, tblBaseModel.opid, tblBaseModel.ArcTypeCode, " & _
        " tblBaseSystemHDtl.BaseSystemHDtlNmae FROM      tblBaseModel INNER JOIN " & _
                      "tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode  WHERE (tblBaseModel.BaseModelCode <> 0) AND (tblBaseModel.CodeClosed = 0)  ORDER BY tblBaseSystemHDtl.BaseSystemHDtlNmae "
        If deactive = True Then
            sqlstr = "SELECT        tblBaseModel.BaseModelCode, tblBaseModel.BaseModelName, tblBaseModel.CodeClosed, tblBaseModel.opid, tblBaseModel.ArcTypeCode, " & _
            " tblBaseSystemHDtl.BaseSystemHDtlNmae FROM         tblBaseModel INNER JOIN " & _
                      "tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode  WHERE (tblBaseModel.BaseModelCode <> 0) ORDER BY tblBaseSystemHDtl.BaseSystemHDtlNmae "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseModelDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseModelCode, BaseModelName, CodeClosed  FROM tblBaseModel  WHERE  (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseModel(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemHDtlCode,BaseModelCode, BaseModelName, CodeClosed   FROM tblBaseModel  WHERE (CodeClosed = 0) AND (BaseModelCode <> 0) AND (BaseModelCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     BaseSystemHDtlCode,BaseModelCode, BaseModelName, CodeClosed   FROM tblBaseModel  WHERE (BaseModelCode <> 0) AND (BaseModelCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
