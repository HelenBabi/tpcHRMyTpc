Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBaseDomainUser
    Public Function GetAllBaseDomainUser(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblUsers.CodeClosed, tblUsers.UserCode, tblUsers.UserLastName, tblUsers.UserFamili, tblUsers.UserName_, tblUsers.semat, tblUsers.UserEmail, tblUsers.UserMobile, tblUsers.UserSpcial,  " & _
                        "tblGenderTyp.genValue, tblUsers.smss, tblUsers.telfon FROM         tblUsers INNER JOIN " & _
                      " tblGenderTyp ON tblUsers.genderr = tblGenderTyp.genId " & _
                      " WHERE     (tblUsers.UserCode <> 0) AND (tblUsers.CodeClosed = 0)"
        If deactive = True Then
            sqlstr = "SELECT    tblUsers.CodeClosed, tblUsers.UserCode, tblUsers.UserLastName, tblUsers.UserFamili, tblUsers.UserName_, tblUsers.semat, tblUsers.UserEmail, tblUsers.UserMobile, tblUsers.UserSpcial,  " & _
                        "tblGenderTyp.genValue, tblUsers.smss, tblUsers.telfon FROM         tblUsers INNER JOIN " & _
                      " tblGenderTyp ON tblUsers.genderr = tblGenderTyp.genId " & _
                      " WHERE     (tblUsers.UserCode <> 0) AND (tblUsers.CodeClosed <> 0)"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseDomainUserDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblUsers.CodeClosed, tblUsers.UserCode, tblUsers.UserLastName, tblUsers.UserFamili, tblUsers.UserName_, tblUsers.semat, tblUsers.UserEmail, tblUsers.UserMobile, tblUsers.UserSpcial,  " & _
                        "tblGenderTyp.genValue, tblUsers.smss, tblUsers.telfon FROM         tblUsers INNER JOIN " & _
                      " tblGenderTyp ON tblUsers.genderr = tblGenderTyp.genId " & _
                      " WHERE     (tblUsers.UserCode <> 0) AND (tblUsers.CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseDomainUser(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblUsers.CodeClosed, tblUsers.UserCode, tblUsers.UserLastName, tblUsers.UserFamili, tblUsers.UserName_, tblUsers.semat, tblUsers.UserEmail, tblUsers.UserMobile, tblUsers.UserSpcial,  " & _
                       "tblGenderTyp.genValue, tblUsers.smss, tblUsers.telfon FROM         tblUsers INNER JOIN " & _
                     " tblGenderTyp ON tblUsers.genderr = tblGenderTyp.genId " & _
                     " WHERE     (tblUsers.UserCode = " & i & ") AND (tblUsers.CodeClosed = 0)"
        If deactive = True Then
            sqlstr = "SELECT    tblUsers.CodeClosed, tblUsers.UserCode, tblUsers.UserLastName, tblUsers.UserFamili, tblUsers.UserName_, tblUsers.semat, tblUsers.UserEmail, tblUsers.UserMobile, tblUsers.UserSpcial,  " & _
                          "tblGenderTyp.genValue, tblUsers.smss, tblUsers.telfon FROM         tblUsers INNER JOIN " & _
                        " tblGenderTyp ON tblUsers.genderr = tblGenderTyp.genId " & _
                        " WHERE     (tblUsers.UserCode = " & i & ") AND (tblUsers.CodeClosed <> 0)"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
