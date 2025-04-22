Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class employeCls
    Public Function GetAllUnits(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      Employ.CodeClosed,Employ.autonum, Employ.ID, Employ.pass, Employ.fstName, Employ.name, Employ.work_no, Employ.mobileNO, Employ.homeNO, Employ.creditDate, " & _
       " Employ.isMariage, Employ.birthday, Type.TypeName " & _
        " FROM         Employ INNER JOIN " & _
                     " Type ON Employ.work_no = Type.TypeID " & _
               "   WHERE (Employ.autonum <> 0) AND (Employ.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT    Employ.CodeClosed,Employ.autonum, Employ.ID, Employ.pass, Employ.fstName, Employ.name, Employ.work_no, Employ.mobileNO, Employ.homeNO, Employ.creditDate, " & _
        " Employ.isMariage, Employ.birthday, Type.TypeName " & _
         " FROM         Employ INNER JOIN " & _
                      " Type ON Employ.work_no = Type.TypeID " & _
                "    WHERE (Employ.autonum <> 0) AND (Employ.CodeClosed = 1)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     Employ.CodeClosed, Employ.autonum, Employ.ID, Employ.pass, Employ.fstName, Employ.name, Employ.work_no, Employ.mobileNO, Employ.homeNO, Employ.creditDate, " & _
      " Employ.isMariage, Employ.birthday, Type.TypeName " & _
       " FROM         Employ INNER JOIN " & _
                    " Type ON Employ.work_no = Type.TypeID " & _
              "    WHERE  (Employ.autonum = 0) order by Employ.work_no "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    Employ.CodeClosed,Employ.autonum, Employ.ID, Employ.pass, Employ.fstName, Employ.name, Employ.work_no, Employ.mobileNO, Employ.homeNO, Employ.creditDate, " & _
      " Employ.isMariage, Employ.birthday, Type.TypeName " & _
       " FROM         Employ INNER JOIN " & _
                    " Type ON Employ.work_no = Type.TypeID " & _
              "     WHERE (Employ.CodeClosed = 0) AND (Employ.autonum <> 0) AND (Employ.autonum = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT     Employ.CodeClosed,Employ.autonum, Employ.ID, Employ.pass, Employ.fstName, Employ.name, Employ.work_no, Employ.mobileNO, Employ.homeNO, Employ.creditDate, " & _
      " Employ.isMariage, Employ.birthday, Type.TypeName " & _
       " FROM         Employ INNER JOIN " & _
                    " Type ON Employ.work_no = Type.TypeID " & _
              "    WHERE (Employ.autonum <> 0) AND (Employ.autonum = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
