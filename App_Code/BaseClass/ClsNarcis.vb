Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsNarcis
    Public Function GetAllNarcis(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     CdCode, SystemCode, HolderName, HolderPhone, GenuineCode, FareiCode,ip, Serial, CodeClosed, opid, ArcTypeCode     FROM tblNarcis"
        If deactive = True Then
            sqlstr = "SELECT     CdCode, SystemCode, HolderName, HolderPhone, GenuineCode, FareiCode, Serial,ip, CodeClosed, opid, ArcTypeCode " & _
                     " FROM         tblNarcis   WHERE (CodeClosed <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllNarcisDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     CdCode, SystemCode, HolderName, HolderPhone, GenuineCode, FareiCode, Serial,ip, CodeClosed, opid, ArcTypeCode " & _
                  " FROM         tblNarcis  WHERE  (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetNarcis(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     CdCode, SystemCode, HolderName, HolderPhone, GenuineCode, FareiCode, Serial,ip, CodeClosed, opid, ArcTypeCode " & _
               " FROM         tblNarcis  WHERE (CodeClosed = 0) AND (CdCode <> 0) AND (CdCode = " & i & ") "
        If deactive = True Then
            sqlstr = "SELECT   CdCode, SystemCode, HolderName, HolderPhone, GenuineCode, FareiCode, Serial,ip, CodeClosed, opid, ArcTypeCode " & _
                    " FROM         tblNarcis  WHERE (CdCode <> 0) AND (CdCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

  
    
End Class
