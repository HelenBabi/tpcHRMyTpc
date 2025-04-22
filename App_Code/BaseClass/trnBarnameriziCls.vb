Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class trnBarnameriziCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM trnBarnameRiziTbl  WHERE (idd <> 0) AND (CodeClosed = 0) order by idd Desc "
        If deactive = True Then
            sqlstr = "SELECT     *  FROM trnBarnameRiziTbl   WHERE (idd <> 0) AND (CodeClosed = 1) order by idd Desc"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM trnBarnameRiziTbl   WHERE  (CodeClosed = 0) order by idd Desc "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM trnBarnameRiziTbl  WHERE (CodeClosed = 0) AND (idd <> 0) AND (idd = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT    *   FROM trnBarnameRiziTbl  WHERE (idd <> 0) AND (idd = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetDocument(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, tblUnits.UnitsName, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                     "FROM            tblUnits INNER JOIN " & _
                            " documentsTbl ON tblUnits.UnitsCode = documentsTbl.UnitsCode  WHERE (documentsTbl.CodeClosed = 0) AND (documentsTbl.id <> 0) AND (documentsTbl.id = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, tblUnits.UnitsName, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                    "FROM            tblUnits INNER JOIN " & _
                           " documentsTbl ON tblUnits.UnitsCode = documentsTbl.UnitsCode  WHERE (documentsTbl.CodeClosed = 1) AND (documentsTbl.id <> 0) AND (documentsTbl.id = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllDocuments(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, tblUnits.UnitsName, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                 "FROM            tblUnits INNER JOIN " & _
                        " documentsTbl ON tblUnits.UnitsCode = documentsTbl.UnitsCode " & _
          "WHERE        (documentsTbl.CodeClosed = 0) AND (documentsTbl.docType = 10)"
        If deactive = True Then
            sqlstr = "SELECT        documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, tblUnits.UnitsName, documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                  "FROM            tblUnits INNER JOIN " & _
                         " documentsTbl ON tblUnits.UnitsCode = documentsTbl.UnitsCode " & _
           "WHERE        (documentsTbl.CodeClosed = 1) AND (documentsTbl.docType = 10)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetPersonelDore(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM trnPersonelDoreTbl  WHERE (CodeClosed = 0) AND (idd <> 0) AND (idd = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT    *   FROM trnPersonelDoreTbl  WHERE (idd <> 0) AND (idd = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetPersonelDoreAll(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *   FROM trnPersonelDoreTbl  WHERE (idd <> 0) AND (CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     *  FROM trnPersonelDoreTbl   WHERE (idd <> 0) AND (CodeClosed = 1)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
