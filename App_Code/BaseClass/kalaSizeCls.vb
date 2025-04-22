Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class kalaSizeCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) kalaSizeTbl.sizeID, kalaSizeTbl.kalaID, kalaSizeTbl.sizeValue, kalaSizeTbl.sizeNote, kalaSizeTbl.stat, kalaTbl.kalaValue, kalaSizeTbl.CodeClosed, personelTypeTbl.typeValue  " & _
        "FROM            kalaSizeTbl INNER JOIN  " & _
                        " kalaTbl ON kalaSizeTbl.kalaID = kalaTbl.kalaID INNER JOIN  " & _
                        " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  " & _
                   "WHERE(kalaSizeTbl.CodeClosed = 0) And (kalaSizeTbl.sizeID <> 0)"
        If deactive = True Then
            sqlstr = "SELECT        TOP (200) kalaSizeTbl.sizeID, kalaSizeTbl.kalaID, kalaSizeTbl.sizeValue, kalaSizeTbl.sizeNote, kalaSizeTbl.stat, kalaTbl.kalaValue, kalaSizeTbl.CodeClosed, personelTypeTbl.typeValue  " & _
         "FROM            kalaSizeTbl INNER JOIN  " & _
                         " kalaTbl ON kalaSizeTbl.kalaID = kalaTbl.kalaID INNER JOIN  " & _
                         " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  " & _
                    "WHERE(kalaSizeTbl.CodeClosed = 1) And (kalaSizeTbl.sizeID <> 0)"
        End If

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

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) kalaSizeTbl.sizeID, kalaSizeTbl.kalaID, kalaSizeTbl.sizeValue, kalaSizeTbl.sizeNote, kalaSizeTbl.stat, kalaTbl.kalaValue, kalaSizeTbl.CodeClosed, personelTypeTbl.typeValue  " & _
   "FROM            kalaSizeTbl INNER JOIN  " & _
                   " kalaTbl ON kalaSizeTbl.kalaID = kalaTbl.kalaID INNER JOIN  " & _
                   " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  " & _
              "WHERE(kalaSizeTbl.CodeClosed = 0) And (kalaSizeTbl.sizeID = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT        TOP (200) kalaSizeTbl.sizeID, kalaSizeTbl.kalaID, kalaSizeTbl.sizeValue, kalaSizeTbl.sizeNote, kalaSizeTbl.stat, kalaTbl.kalaValue, kalaSizeTbl.CodeClosed, personelTypeTbl.typeValue  " & _
      "FROM            kalaSizeTbl INNER JOIN  " & _
                      " kalaTbl ON kalaSizeTbl.kalaID = kalaTbl.kalaID INNER JOIN  " & _
                      " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  " & _
                 "WHERE(kalaSizeTbl.CodeClosed = 1) And (kalaSizeTbl.sizeID = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
