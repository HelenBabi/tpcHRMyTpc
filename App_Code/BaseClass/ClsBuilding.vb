Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBuilding
    Public Function GetAllBuilding(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BuildingCode, BuildingName, BuildingEngName, CodeClosed, opid, ArcTypeCode  FROM tblBuilding  WHERE (BuildingCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     BuildingCode, BuildingName, BuildingEngName, CodeClosed, opid, ArcTypeCode  FROM tblBuilding  WHERE (BuildingCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBuildingDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BuildingCode, BuildingName, BuildingEngName, CodeClosed, opid, ArcTypeCode  FROM tblBuilding  WHERE  (CodeClosed = 0) order by BuildingName "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBuilding(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BuildingCode, BuildingName, BuildingEngName, CodeClosed, opid, ArcTypeCode   FROM tblBuilding  WHERE (CodeClosed = 0) AND (BuildingCode <> 0) AND (BuildingCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     BuildingCode, BuildingName, BuildingEngName, CodeClosed, opid, ArcTypeCode   FROM tblBuilding  WHERE (BuildingCode <> 0) AND (BuildingCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
