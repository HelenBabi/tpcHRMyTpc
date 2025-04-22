Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class trnSubjectFilesCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM trnSubjectsTbl  WHERE (UnitsCode <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM trnSubjectsTbl  WHERE (UnitsCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed  FROM trnSubjectsTbl  WHERE  (CodeClosed = 0) order by UnitsName "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM trnSubjectsTbl  WHERE (CodeClosed = 0) AND (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM trnSubjectsTbl  WHERE (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDocuments(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnSubjectsTbl.UnitsName, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                      "FROM            trnSubjectsTbl INNER JOIN " & _
                             " documentsTbl ON trnSubjectsTbl.UnitsCode = documentsTbl.UnitsCode  WHERE (documentsTbl.id <> 0) AND (documentsTbl.CodeClosed = 0) AND (docType = 10) "
        If deactive = True Then
            sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnSubjectsTbl.UnitsName, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                       "FROM            trnSubjectsTbl INNER JOIN " & _
                              " documentsTbl ON trnSubjectsTbl.UnitsCode = documentsTbl.UnitsCode  WHERE (documentsTbl.id <> 0) AND (docType = 10)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDocumentsgHozuri(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnBarnameRiziTbl.onvan, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                      "FROM            trnBarnameRiziTbl INNER JOIN " & _
                             " documentsTbl ON trnBarnameRiziTbl.idd = documentsTbl.UnitsCode  WHERE (documentsTbl.id <> 0) AND (documentsTbl.CodeClosed = 0) AND (docType = 17) "
        If deactive = True Then
            sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnBarnameRiziTbl.onvan, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                       "FROM            trnBarnameRiziTbl INNER JOIN " & _
                              " documentsTbl ON trnBarnameRiziTbl.idd = documentsTbl.UnitsCode  WHERE (documentsTbl.id <> 0) AND (docType = 17)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDocumentgHozuri(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       documentsTbl.mozuID, documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnBarnameRiziTbl.onvan,trnBarnameRiziTbl.idd, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                      "FROM            trnBarnameRiziTbl INNER JOIN " & _
                             " documentsTbl ON trnBarnameRiziTbl.idd = documentsTbl.UnitsCode  WHERE  (documentsTbl.id = " & i & ") AND (documentsTbl.CodeClosed = 0) AND (docType = 17) "
        If deactive = True Then
            sqlstr = "SELECT       documentsTbl.mozuID, documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnBarnameRiziTbl.onvan,trnBarnameRiziTbl.idd, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                       "FROM            trnBarnameRiziTbl INNER JOIN " & _
                              " documentsTbl ON trnBarnameRiziTbl.idd = documentsTbl.UnitsCode  WHERE (documentsTbl.id = " & i & ") AND (docType = 17)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
