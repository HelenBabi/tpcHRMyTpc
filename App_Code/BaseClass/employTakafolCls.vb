Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class employTakafolCls
    Public Function GetAllUnits(ByVal codepersoneli As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from vwTakfol   WHERE     (codePersoneli = " & codepersoneli & ") "


        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllUnitsDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     Sponsorship.ID, Sponsorship.empID, Sponsorship.fstName, Sponsorship.Name, Sponsorship.relation, Sponsorship.birthday, Sponsorship.credit,  " & _
                   "  Sponsorship.IsActive, Sponsorship.perId, Relations.Name AS nesbatt " & _
                   " FROM         Sponsorship INNER JOIN " & _
                    " Relations ON Sponsorship.relation = Relations.ID " & _
                    " ORDER BY Sponsorship.empID "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetUnits(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     Sponsorship.ID, Sponsorship.empID, Sponsorship.fstName, Sponsorship.Name, Sponsorship.relation, Sponsorship.birthday, Sponsorship.credit,  " & _
                    "  Sponsorship.IsActive, Sponsorship.perId, Relations.Name AS nesbatt " & _
                    " FROM         Sponsorship INNER JOIN " & _
                     " Relations ON Sponsorship.relation = Relations.ID " & _
               "     WHERE  (Sponsorship.IsActive = 1)  AND  (Sponsorship.ID = " & i & ") "
        If deactive = True Then
            sqlstr = "SELECT     Sponsorship.ID, Sponsorship.empID, Sponsorship.fstName, Sponsorship.Name, Sponsorship.relation, Sponsorship.birthday, Sponsorship.credit,  " & _
                               "  Sponsorship.IsActive, Sponsorship.perId, Relations.Name AS nesbatt " & _
                               " FROM         Sponsorship INNER JOIN " & _
                                " Relations ON Sponsorship.relation = Relations.ID " & _
                          "     WHERE  (Sponsorship.IsActive = 0)  AND  (Sponsorship.ID = " & i & ") "
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
