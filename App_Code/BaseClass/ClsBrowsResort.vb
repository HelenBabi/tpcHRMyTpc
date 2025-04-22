Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBrowsResort

    Public Function GetBrowsDriverNotOut() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  tblMavadOut.HavalehGrup, tblMavadOutDtl.idHavalehDtl, tblMavadOutDtl.namDriver, tblMavadOutDtl.shahrbaniNo, tblMavadOutDtl.dateIn + ' ' +  tblMavadOutDtl.timeIn as zamanvrud, tblMavadOutDtl.bargOutNo, " & _
                      " tblMavadOutDtl.megdar, tblMavadOutDtl.sherkatPeymankar, tblMavadOutDtl.tozihat, tblMavadOutDtl.stat,  " & _
                      " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblMavadOut.Havaleh  " & _
                    " FROM         tblMavadOutDtl INNER JOIN  " & _
                     " tblUsers ON tblMavadOutDtl.userId = tblUsers.UserCode INNER JOIN  " & _
                     " tblMavadOut ON tblMavadOutDtl.idHavaleh = tblMavadOut.idHavaleh  " & _
                     " WHERE tblMavadOutDtl.stat = 0 ORDER BY tblMavadOut.Havaleh "

        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetBrowsmavadin() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        tblmavadIn.idd, tblmavadIn.driver, tblmavadIn.GavahinameSh, tblmavadIn.keshandeSh, tblmavadIn.barnameSh, tblmavadIn.vaznXales, " & _
                         " tblmavadIn.vurudTarix + ' ' + tblmavadIn.vurudSat AS tarix, tblmavadIn.tozihat, tblMahmuleTyp.Name,  " & _
                         " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblmavadIn.VaznTaxliye, tblmavadIn.hozurTarix + ' ' + tblmavadIn.hozurSat AS huzur,  " & _
                         " tblmavadIn.xurujTarix + ' ' + tblmavadIn.xurujSat AS xuruj, tblmavadIn.stat,tblmavadIn.vaznXales - tblmavadIn.VaznTaxliye as kasri " & _
" FROM            tblmavadIn INNER JOIN " & _
                        " tblMahmuleTyp ON tblmavadIn.mahmule = tblMahmuleTyp.Code INNER JOIN " & _
                        " tblUsers ON tblmavadIn.userVrud = tblUsers.UserCode order by tblmavadIn.idd DESC"

        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetBrowsDriverIn(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String

        'sqlstr = "SELECT     tblMavadOut.HavalehGrup, tblMavadOutDtl.idHavalehDtl, tblMavadOutDtl.namDriver, tblMavadOutDtl.shahrbaniNo,  " & _
        '              " tblMavadOutDtl.dateIn + ' ' + tblMavadOutDtl.timeIn AS zamanvrud, tblMavadOutDtl.bargOutNo, tblMavadOutDtl.dateOut + ' ' + tblMavadOutDtl.timeOut AS zamanXruj,  " & _
        '              " tblMavadOutDtl.megdar, tblMavadOutDtl.sherkatPeymankar, tblMavadOutDtl.tozihat, tblMavadOutDtl.stat, tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, " & _
        '               " tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS karbarOut, tblMavadOut.Havaleh " & _
        '             " FROM         tblMavadOutDtl INNER JOIN " & _
        '              " tblUsers ON tblMavadOutDtl.userId = tblUsers.UserCode INNER JOIN " & _
        '              " tblMavadOut ON tblMavadOutDtl.idHavaleh = tblMavadOut.idHavaleh INNER JOIN " & _
        '              " tblUsers AS tblUsers_1 ON tblMavadOutDtl.outuserId = tblUsers_1.UserCode " & _
        '            " where tblMavadOutDtl.dateIn BETWEEN '" & az & "' and '" & taa & "' ORDER BY tblMavadOut.Havaleh "
        sqlstr = "SELECT   * from mavadoutDtlGridVW where dateIn BETWEEN '" & az & "' and '" & taa & "' ORDER BY Havaleh "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetAllBrowsWorks(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblResort.ResortID, tblResort.nam + ' ' + tblResort.lnam AS moraje, tblResort.datee + ' ' + tblResort.timee as zamansbat, tblResort.codeCart, tblResort.mizban, " & _
                    "   tblResort.otag, tblResort.mojavezNo, tblResort.notee, tblResort.dateEnd + ' ' + tblResort.timeEnd as zamanEnd,  " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblSepordeType.Sepordevalue, tblResort.stat,  " & _
                      " tblUnits.UnitsName, tblGenderTyp.genValue, tblEllatType.ellatName, " & _
                      " tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS outKarbar " & _
                      " FROM         tblResort INNER JOIN " & _
                      " tblUsers ON tblResort.userId = tblUsers.UserCode INNER JOIN " & _
                       " tblSepordeType ON tblResort.sepordeTyp = tblSepordeType.SepordeCode INNER JOIN " & _
                     " tblUnits ON tblResort.unitee = tblUnits.UnitsCode INNER JOIN " & _
                      " tblGenderTyp ON tblResort.genderr = tblGenderTyp.genId INNER JOIN " & _
                      " tblEllatType ON tblResort.ellat = tblEllatType.ellatCode INNER JOIN " & _
                      " tblUsers AS tblUsers_1 ON tblResort.outUserId = tblUsers_1.UserCode " & _
                     " WHERE     (tblResort.datee BETWEEN '" & az & "' AND '" & taa & "')"

        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function

    Public Function GetResort(ByVal ResortId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblResort.ResortID, tblGenderTyp.genValue+' ' + tblResort.nam + ' ' +  tblResort.lnam as moraje , tblResort.datee + '   ' + tblResort.timee as zaman, tblResort.codeCart, tblResort.mizban, " & _
                      "tblResort.otag, tblResort.mojavezNo, tblResort.notee, tblResort.dateEnd + '   ' + tblResort.timeEnd as zamanEnd,  " & _
                      "tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar,  tblSepordeType.Sepordevalue, tblResort.stat,  " & _
        " tblUnits.UnitsName, tblEllatType.ellatName " & _
"FROM         tblResort INNER JOIN " & _
                     " tblUsers ON tblResort.userId = tblUsers.UserCode INNER JOIN " & _
                       "tblSepordeType ON tblResort.sepordeTyp = tblSepordeType.SepordeCode INNER JOIN " & _
                      "tblUnits ON tblResort.unitee = tblUnits.UnitsCode INNER JOIN " & _
                      "tblGenderTyp ON tblResort.genderr = tblGenderTyp.genId INNER JOIN " & _
                      "tblEllatType ON tblResort.ellat = tblEllatType.ellatCode " & _
        " WHERE tblResort.ResortID = " & ResortId & ""

        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        Return dt
    End Function

    Public Function GetNotOut() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblResort.ResortID, tblResort.nam + ' ' + tblResort.lnam AS moraje, tblResort.datee, tblResort.timee, tblResort.codeCart, tblResort.mizban, " & _
                    "  tblResort.otag, tblResort.mojavezNo, tblResort.notee, tblResort.dateEnd, tblResort.timeEnd,  " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblSepordeType.Sepordevalue, tblResort.stat,  " & _
                      " tblUnits.UnitsName, tblGenderTyp.genValue, tblEllatType.ellatName, " & _
                      " tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS outKarbar " & _
                      " FROM         tblResort INNER JOIN " & _
                      " tblUsers ON tblResort.userId = tblUsers.UserCode INNER JOIN " & _
                    " tblSepordeType ON tblResort.sepordeTyp = tblSepordeType.SepordeCode INNER JOIN " & _
                     " tblUnits ON tblResort.unitee = tblUnits.UnitsCode INNER JOIN " & _
                       " tblGenderTyp ON tblResort.genderr = tblGenderTyp.genId INNER JOIN " & _
                      " tblEllatType ON tblResort.ellat = tblEllatType.ellatCode INNER JOIN " & _
                      " tblUsers AS tblUsers_1 ON tblResort.outUserId = tblUsers_1.UserCode " & _
                     " WHERE     tblResort.stat = 0 or tblResort.stat = 4"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt

    End Function

    Public Function GetMangeHvalehdtl(ByVal i As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  tblMavadOutDtl.dateOut, tblMavadOutDtl.timeOut ,tblMavadOut.idHavaleh, tblMavadOutDtl.idHavalehDtl, tblMavadOutDtl.namDriver, tblMavadOutDtl.shahrbaniNo, tblMavadOutDtl.dateIn ,  tblMavadOutDtl.timeIn , tblMavadOutDtl.bargOutNo, " & _
                        " tblMavadOutDtl.megdar, tblMavadOutDtl.sherkatPeymankar, tblMavadOutDtl.tozihat, tblMavadOutDtl.stat,tblMavadOutDtl.magsad, tblMavadOutDtl.tranzitNO, tblMavadOutDtl.gomrok,  " & _
                        " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblMavadOut.Havaleh  " & _
                      " FROM         tblMavadOutDtl INNER JOIN  " & _
                       " tblUsers ON tblMavadOutDtl.userId = tblUsers.UserCode INNER JOIN  " & _
                       " tblMavadOut ON tblMavadOutDtl.idHavaleh = tblMavadOut.idHavaleh  " & _
                       " WHERE tblMavadOutDtl.idHavalehDtl = " & i & " ORDER BY tblMavadOut.Havaleh "

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetMangeHvalehdtlAll() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  tblMavadOutDtl.dateOut + ' ' + tblMavadOutDtl.timeOut as zamanxruj , tblMavadOut.HavalehGrup, tblMavadOutDtl.idHavalehDtl, tblMavadOutDtl.namDriver, tblMavadOutDtl.shahrbaniNo, tblMavadOutDtl.dateIn + ' ' +  tblMavadOutDtl.timeIn as zamanvrud, tblMavadOutDtl.bargOutNo, " & _
                      " tblMavadOutDtl.megdar, tblMavadOutDtl.sherkatPeymankar, tblMavadOutDtl.tozihat, tblMavadOutDtl.stat,  " & _
                      " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblMavadOut.Havaleh  " & _
                    " FROM         tblMavadOutDtl INNER JOIN  " & _
                     " tblUsers ON tblMavadOutDtl.userId = tblUsers.UserCode INNER JOIN  " & _
                     " tblMavadOut ON tblMavadOutDtl.idHavaleh = tblMavadOut.idHavaleh  " & _
                     "  ORDER BY tblMavadOut.Havaleh "

        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
