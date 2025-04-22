Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class shobeCls
    Public Function GetAllBaseModel(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        shobe.shobe_id, shobe.shobe_nam, shobe.bank_id, shobe.CodeClosed, shobe.opid, shobe.ArcTypeCode, bank_.bank_name " & _
                "FROM            shobe INNER JOIN " & _
                        " bank_ ON shobe.bank_id = bank_.bank_id " & _
                      "WHERE        (shobe.CodeClosed = 0) AND (shobe.shobe_id <> 1) " & _
                       "ORDER BY bank_.bank_name, shobe.shobe_nam"
        If deactive = True Then
            sqlstr = "SELECT        shobe.shobe_id, shobe.shobe_nam, shobe.bank_id, shobe.CodeClosed, shobe.opid, shobe.ArcTypeCode, bank_.bank_name " & _
               "FROM            shobe INNER JOIN " & _
                       " bank_ ON shobe.bank_id = bank_.bank_id " & _
                     "WHERE        (shobe.CodeClosed = 1) AND (shobe.shobe_id <> 1) " & _
                      "ORDER BY bank_.bank_name, shobe.shobe_nam"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseModelDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        shobe.shobe_id, shobe.shobe_nam, shobe.bank_id, shobe.CodeClosed, shobe.opid, shobe.ArcTypeCode, bank_.bank_name " & _
                "FROM            shobe INNER JOIN " & _
                        " bank_ ON shobe.bank_id = bank_.bank_id " & _
                      "WHERE        (shobe.CodeClosed = 0) AND (shobe.shobe_id <> 1) " & _
                       "ORDER BY bank_.bank_name, shobe.shobe_nam"
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
