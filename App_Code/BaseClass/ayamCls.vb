Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ayamCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        'sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM tblUnits  WHERE (UnitsCode <> 0) AND (CodeClosed = 0) "
        sqlstr = "SELECT     Hafteh.HaftehID, Hafteh.HaftehRooz, Hafteh.timeID, Hafteh.IsActive, ayamTbl.ruz, barnameTbl.valuee, barnameTbl.resturanID,  " & _
        " refahiLocationTbl.locationValue  " & _
          " FROM         Hafteh INNER JOIN  " & _
                     " barnameTbl ON Hafteh.timeID = barnameTbl.timeID INNER JOIN  " & _
                     " ayamTbl ON Hafteh.HaftehRooz = ayamTbl.idd INNER JOIN  " & _
                     " refahiLocationTbl ON barnameTbl.resturanID = refahiLocationTbl.locID  " & _
       " WHERE (Hafteh.IsActive = 1) and (Hafteh.HaftehID <> 0)"
        If deactive = True Then
            sqlstr = "SELECT     Hafteh.HaftehID, Hafteh.HaftehRooz, Hafteh.timeID, Hafteh.IsActive, ayamTbl.ruz, barnameTbl.valuee, barnameTbl.resturanID,  " & _
             " refahiLocationTbl.locationValue  " & _
               " FROM         Hafteh INNER JOIN  " & _
                          " barnameTbl ON Hafteh.timeID = barnameTbl.timeID INNER JOIN  " & _
                          " ayamTbl ON Hafteh.HaftehRooz = ayamTbl.idd INNER JOIN  " & _
                          " refahiLocationTbl ON barnameTbl.resturanID = refahiLocationTbl.locID  " & _
            " WHERE (Hafteh.IsActive = 0) and (Hafteh.HaftehID <> 0)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        'sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM tblUnits  WHERE  (CodeClosed = 0) order by UnitsName "
        sqlstr = "SELECT     Hafteh.HaftehID, Hafteh.HaftehRooz, Hafteh.timeID, Hafteh.IsActive, ayamTbl.ruz, barnameTbl.valuee, barnameTbl.resturanID,  " & _
        " refahiLocationTbl.locationValue  " & _
          " FROM         Hafteh INNER JOIN  " & _
                     " barnameTbl ON Hafteh.timeID = barnameTbl.timeID INNER JOIN  " & _
                     " ayamTbl ON Hafteh.HaftehRooz = ayamTbl.idd INNER JOIN  " & _
                     " refahiLocationTbl ON barnameTbl.resturanID = refahiLocationTbl.locID  " & _
       " WHERE (Hafteh.IsActive = 1) and (Hafteh.HaftehID <> 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        'sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM tblUnits  WHERE (CodeClosed = 0) AND (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
        sqlstr = "SELECT     Hafteh.HaftehID, Hafteh.HaftehRooz, Hafteh.timeID, Hafteh.IsActive, ayamTbl.ruz, barnameTbl.valuee, barnameTbl.resturanID,  " & _
        " refahiLocationTbl.locationValue  " & _
          " FROM         Hafteh INNER JOIN  " & _
                     " barnameTbl ON Hafteh.timeID = barnameTbl.timeID INNER JOIN  " & _
                     " ayamTbl ON Hafteh.HaftehRooz = ayamTbl.idd INNER JOIN  " & _
                     " refahiLocationTbl ON barnameTbl.resturanID = refahiLocationTbl.locID  " & _
       " WHERE (Hafteh.IsActive = 1) and (Hafteh.HaftehID = " & i & ")"
        If deactive = True Then
            'sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM tblUnits  WHERE (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
            sqlstr = "SELECT     Hafteh.HaftehID, Hafteh.HaftehRooz, Hafteh.timeID, Hafteh.IsActive, ayamTbl.ruz, barnameTbl.valuee, barnameTbl.resturanID,  " & _
       " refahiLocationTbl.locationValue  " & _
         " FROM         Hafteh INNER JOIN  " & _
                    " barnameTbl ON Hafteh.timeID = barnameTbl.timeID INNER JOIN  " & _
                    " ayamTbl ON Hafteh.HaftehRooz = ayamTbl.idd INNER JOIN  " & _
                    " refahiLocationTbl ON barnameTbl.resturanID = refahiLocationTbl.locID  " & _
      " WHERE (Hafteh.IsActive = 0) and (Hafteh.HaftehID = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
