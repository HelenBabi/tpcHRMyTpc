Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class menuCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gKarmand), 1), '.00', '') as gkarmand, menuTbl.zarfiat,menuTbl.menuID, menuTbl.menu, menuTbl.resturanID, menuTbl.CodeClosed, menuTbl.opid, menuTbl.ArcTypeCode, refahiLocationTbl.locationValue " & _
           " FROM            menuTbl INNER JOIN " & _
                         " refahiLocationTbl ON menuTbl.resturanID = refahiLocationTbl.locID  WHERE (menuTbl.menuID <> 0) AND (menuTbl.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT        REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gKarmand), 1), '.00', '') as gkarmand, menuTbl.zarfiat, menuTbl.menuID, menuTbl.menu, menuTbl.resturanID, REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gimat), 1), '.00', '') as gimat, menuTbl.CodeClosed, menuTbl.opid, menuTbl.ArcTypeCode, refahiLocationTbl.locationValue " & _
   " FROM            menuTbl INNER JOIN " & _
                 " refahiLocationTbl ON menuTbl.resturanID = refahiLocationTbl.locID  WHERE  (menuTbl.CodeClosed = 1) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT         menuTbl.zarfiat,menuTbl.menuID, menuTbl.menu, menuTbl.resturanID, menuTbl.gimat, menuTbl.CodeClosed, menuTbl.opid, menuTbl.ArcTypeCode, refahiLocationTbl.locationValue " & _
     " FROM            menuTbl INNER JOIN " & _
                   " refahiLocationTbl ON menuTbl.resturanID = refahiLocationTbl.locID   WHERE (menuTbl.menuID <> 0) AND (menuTbl.CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gKarmand), 1), '.00', '') as gKarmand, menuTbl.zarfiat,menuTbl.menuID, menuTbl.menu, menuTbl.resturanID, REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gimat), 1), '.00', '') as gimat, menuTbl.CodeClosed, menuTbl.opid, menuTbl.ArcTypeCode, refahiLocationTbl.locationValue, IsFood, IsYaraneh, TypeFoodID " & _
        " FROM            menuTbl INNER JOIN " & _
                  " refahiLocationTbl ON menuTbl.resturanID = refahiLocationTbl.locID   WHERE (menuTbl.menuID = " & i & ") AND (menuTbl.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT      REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gKarmand), 1), '.00', '') as gKarmand, menuTbl.zarfiat,menuTbl.menuID, menuTbl.menu, menuTbl.resturanID, REPLACE(CONVERT(varchar, CONVERT(Money,menuTbl.gimat), 1), '.00', '') as gimat, menuTbl.CodeClosed, menuTbl.opid, menuTbl.ArcTypeCode, refahiLocationTbl.locationValue, IsFood, IsYaraneh, TypeFoodID " & _
          " FROM            menuTbl INNER JOIN " & _
                    " refahiLocationTbl ON menuTbl.resturanID = refahiLocationTbl.locID   WHERE (menuTbl.menuID = " & i & ") AND (menuTbl.menuID <> 0) AND (menuTbl.CodeClosed = 1) "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
