Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data


Public Class ClsKindCar

    Public Function GetAllKindCar(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     KindCarCode, KindCarName, CodeClosed  FROM tblKindCar  WHERE (KindCarCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     KindCarCode, KindCarName, CodeClosed  FROM tblKindCar  WHERE (KindCarCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllKindCarDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     KindCarCode, KindCarName, CodeClosed   FROM tblKindCar  WHERE     (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetKindCar(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     KindCarCode, KindCarName, CodeClosed   FROM tblKindCar  WHERE (CodeClosed = 0) AND (KindCarCode <> 0) AND (KindCarCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     KindCarCode, KindCarName, CodeClosed   FROM tblKindCar  WHERE (KindCarCode <> 0) AND (KindCarCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
