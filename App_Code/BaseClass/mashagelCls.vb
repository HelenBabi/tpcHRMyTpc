Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class mashagelCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     mashaghel.id, mashaghel.postname, mashaghel.saxt, mashaghel.CodeClosed, mashaghel.opid, mashaghel.ArcTypeCode, saxtzian.value " & _
                " FROM         mashaghel INNER JOIN " & _
                     " saxtzian ON mashaghel.saxt = saxtzian.id  WHERE (mashaghel.id <> 0) AND (mashaghel.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT     mashaghel.id, mashaghel.postname, mashaghel.saxt, mashaghel.CodeClosed, mashaghel.opid, mashaghel.ArcTypeCode, saxtzian.value " & _
               " FROM         mashaghel INNER JOIN " & _
                    " saxtzian ON mashaghel.saxt = saxtzian.id  WHERE (mashaghel.id <> 0) AND (mashaghel.CodeClosed = 1)  "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     mashaghel.id, mashaghel.postname, mashaghel.saxt, mashaghel.CodeClosed, mashaghel.opid, mashaghel.ArcTypeCode, saxtzian.value " & _
                " FROM         mashaghel INNER JOIN " & _
                     " saxtzian ON mashaghel.saxt = saxtzian.id  WHERE (mashaghel.CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        ' sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM tblUnits  WHERE (CodeClosed = 0) AND (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
        sqlstr = "SELECT     mashaghel.id, mashaghel.postname, mashaghel.saxt, mashaghel.CodeClosed, mashaghel.opid, mashaghel.ArcTypeCode, saxtzian.value " & _
         " FROM         mashaghel INNER JOIN " & _
              " saxtzian ON mashaghel.saxt = saxtzian.id  WHERE (mashaghel.id = " & i & ") AND (mashaghel.CodeClosed = 0) "
        If deactive = True Then
            ' sqlstr = "SELECT     UnitsCode, UnitsName, CodeClosed   FROM tblUnits  WHERE (UnitsCode <> 0) AND (UnitsCode = " & i & ")"
            sqlstr = "SELECT     mashaghel.id, mashaghel.postname, mashaghel.saxt, mashaghel.CodeClosed, mashaghel.opid, mashaghel.ArcTypeCode, saxtzian.value " & _
        " FROM         mashaghel INNER JOIN " & _
             " saxtzian ON mashaghel.saxt = saxtzian.id  WHERE (mashaghel.id = " & i & ") AND (mashaghel.id <> 0) AND (mashaghel.CodeClosed = 1)  "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
