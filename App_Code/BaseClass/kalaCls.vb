Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class kalaCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) kalaTbl.kalaID, kalaTbl.kalaValue, kalaTbl.setadi, kalaTbl.stat, kalaTbl.CodeClosed, kalaTbl.opid, kalaTbl.ArcTypeCode, personelTypeTbl.typeValue  " & _
          "FROM            kalaTbl INNER JOIN " & _
                        " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  WHERE        (CodeClosed = 0) AND (kalaID <> 0) "
        If deactive = True Then
            sqlstr = "SELECT        TOP (200) kalaTbl.kalaID, kalaTbl.kalaValue, kalaTbl.setadi, kalaTbl.stat, kalaTbl.CodeClosed, kalaTbl.opid, kalaTbl.ArcTypeCode, personelTypeTbl.typeValue  " & _
          "FROM            kalaTbl INNER JOIN " & _
                        " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  WHERE        (CodeClosed = 1) AND (kalaID <> 0) "
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
        sqlstr = "SELECT        TOP (200) kalaTbl.kalaID, kalaTbl.kalaValue, kalaTbl.setadi, kalaTbl.stat, kalaTbl.CodeClosed, kalaTbl.opid, kalaTbl.ArcTypeCode, personelTypeTbl.typeValue  " & _
    "FROM            kalaTbl INNER JOIN " & _
                  " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  WHERE        (CodeClosed = 0) AND (kalaID =" & i & " ) "
        If deactive = True Then
            sqlstr = "SELECT        TOP (200) kalaTbl.kalaID, kalaTbl.kalaValue, kalaTbl.setadi, kalaTbl.stat, kalaTbl.CodeClosed, kalaTbl.opid, kalaTbl.ArcTypeCode, personelTypeTbl.typeValue  " & _
"FROM            kalaTbl INNER JOIN " & _
              " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID  WHERE        (CodeClosed = 1) AND (kalaID =" & i & " ) "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetBaseModel(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) kalaTbl.kalaID, kalaTbl.kalaValue, kalaTbl.setadi, kalaTbl.stat, kalaTbl.CodeClosed, kalaTbl.opid, kalaTbl.ArcTypeCode, personelTypeTbl.typeValue " & _
      " FROM            kalaTbl INNER JOIN " & _
                        " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID " & _
                      "WHERE        (kalaTbl.kalaID = " & i & ") AND (kalaTbl.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT        TOP (200) kalaTbl.kalaID, kalaTbl.kalaValue, kalaTbl.setadi, kalaTbl.stat, kalaTbl.CodeClosed, kalaTbl.opid, kalaTbl.ArcTypeCode, personelTypeTbl.typeValue " & _
    " FROM            kalaTbl INNER JOIN " & _
                      " personelTypeTbl ON kalaTbl.setadi = personelTypeTbl.typeID " & _
                    "WHERE        (kalaTbl.kalaID = " & i & ") AND (kalaTbl.CodeClosed = 1) "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
