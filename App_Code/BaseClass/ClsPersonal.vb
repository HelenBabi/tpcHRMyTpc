Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsPersonal
    Public Function GetAllBaseDomainUser(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     personel.postname ,personel.codeMelli, personel.Id, personel.UnitsCode, personel.nam, personel.family, personel.codePersoneli, teldaxeli, telmanzel, mobile, " & _
                     " personel.CodeClosed, personel.opid, personel.ArcTypeCode, tblUnits.UnitsName, omurTbl.omur  " & _
                    " FROM         personel INNER JOIN " & _
                       "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode  INNER JOIN" & _
                       "  omurTbl ON personel.omurID = omurTbl.omurID" & _
                      " WHERE     (personel.id <> 0) AND (personel.CodeClosed = 0) "
        If deactive = True Then
            sqlstr = "SELECT    personel.postname , personel.codeMelli, personel.Id, personel.UnitsCode, personel.nam, personel.family, personel.codePersoneli,teldaxeli, telmanzel, mobile,  " & _
                     " personel.CodeClosed, personel.opid, personel.ArcTypeCode, tblUnits.UnitsName, omurTbl.omur  " & _
                    " FROM         personel INNER JOIN " & _
                       "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode  INNER JOIN " & _
                       "  omurTbl ON personel.omurID = omurTbl.omurID" & _
                      " WHERE     (personel.id <> 0) AND (personel.CodeClosed <> 0) "
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseDomainUserVW(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     personel.postname ,personel.codeMelli, personel.Id, personel.UnitsCode, personel.nam, personel.family, personel.codePersoneli, teldaxeli, telmanzel, mobile, " & _
                     " personel.CodeClosed, personel.opid, personel.ArcTypeCode, tblUnits.UnitsName, omurTbl.omur  " & _
                    " FROM         personel INNER JOIN " & _
                       "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode  INNER JOIN" & _
                       "  omurTbl ON personel.omurID = omurTbl.omurID" & _
                      " WHERE     (personel.id <> 0) AND (personel.CodeClosed = 0) AND (personel.teldaxeli <> '0')"
        If deactive = True Then
            sqlstr = "SELECT    personel.postname , personel.codeMelli, personel.Id, personel.UnitsCode, personel.nam, personel.family, personel.codePersoneli,teldaxeli, telmanzel, mobile,  " & _
                     " personel.CodeClosed, personel.opid, personel.ArcTypeCode, tblUnits.UnitsName, omurTbl.omur  " & _
                    " FROM         personel INNER JOIN " & _
                       "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode  INNER JOIN " & _
                       "  omurTbl ON personel.omurID = omurTbl.omurID" & _
                      " WHERE     (personel.id <> 0) AND (personel.CodeClosed <> 0) AND (personel.teldaxeli <> '0')"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBaseDomainUserDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     personel.*, tblUnits.UnitsName  " & _
                   " FROM         personel INNER JOIN " & _
                      "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode " & _
                     " WHERE     (personel.id <> 0) AND (personel.CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBaseDomainUser(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        personel.ID, personel.nam, personel.family, personel.madrak, personel.codePersoneli, personel.reshte, personel.tarixestexdam, personel.codebime,   " & _
       " personel.postname, personel.typ, personel.UnitsCode, personel.codeMelli, personel.father, personel.shsh, personel.mahalesodur, personel.shserial,  " & _
       " personel.vaziatsh, personel.mahaletavalod, personel.tarixtavalod, personel.ellatevaziat, personel.vaziattahol, personel.teldaxeli, personel.telmanzel, " & _
      "  personel.mobile, personel.din, personel.mazhab, personel.shgozarname, personel.opid, personel.ArcTypeCode, personel.CodeClosed, personel.adres, " &
       " personel.tarixDisable, personel.jens, personel.userType, personel.UserPass, personel.isResort, personel.mozd, personel.serviceNo, personel.UserPic, " & _
      "  personel.supervizer, personel.emaill, personel.pass, personel.ruzkarShabkar, personel.setadiAmaliati, personel.omurID, personel.sponsorshipNum, " & _
      " personel.shift, personel.modir, personel.codePosti, personel.shHesab, personel.tarixSodur, personel.tarixEzdevaj, personel.sabegeNezam, " & _
       "personel.daneshgah, personel.moadel, personel.tarixMadrak, personel.shahrTahsil, personel.esargariTyp, personel.nesbatEsargari, personel.tarixSarbazi, " & _
      " personel.moafitType, personel.sanatNaft, personel.nesbatNaft, personel.codePersonNaft, personel.xedmatType, personel.tarixSarbaziShuru, " & _
       "personel.shMosalsalsh, personel.NoeIsargari, tblUnits.UnitsName, chartTbl.chartName, chartTbl.personelAdmin  " & _
           "  from   personel INNER JOIN  " & _
                       "       tblUnits ON personel.UnitsCode = tblUnits.UnitsCode LEFT OUTER JOIN  " & _
                        " chartTbl ON personel.codePersoneli = chartTbl.codePersoneli  " & _
          "WHERE (personel.ID = " & i & ") And (personel.CodeClosed = 0)"
        If deactive = True Then
            sqlstr = "SELECT        personel.ID, personel.nam, personel.family, personel.madrak, personel.codePersoneli, personel.reshte, personel.tarixestexdam, personel.codebime,   " & _
           " personel.postname, personel.typ, personel.UnitsCode, personel.codeMelli, personel.father, personel.shsh, personel.mahalesodur, personel.shserial,  " & _
           " personel.vaziatsh, personel.mahaletavalod, personel.tarixtavalod, personel.ellatevaziat, personel.vaziattahol, personel.teldaxeli, personel.telmanzel, " & _
          "  personel.mobile, personel.din, personel.mazhab, personel.shgozarname, personel.opid, personel.ArcTypeCode, personel.CodeClosed, personel.adres, " &
           " personel.tarixDisable, personel.jens, personel.userType, personel.UserPass, personel.isResort, personel.mozd, personel.serviceNo, personel.UserPic, " & _
          "  personel.supervizer, personel.emaill, personel.pass, personel.ruzkarShabkar, personel.setadiAmaliati, personel.omurID, personel.sponsorshipNum, " & _
          " personel.shift, personel.modir, personel.codePosti, personel.shHesab, personel.tarixSodur, personel.tarixEzdevaj, personel.sabegeNezam, " & _
           "personel.daneshgah, personel.moadel, personel.tarixMadrak, personel.shahrTahsil, personel.esargariTyp, personel.nesbatEsargari, personel.tarixSarbazi, " & _
          " personel.moafitType, personel.sanatNaft, personel.nesbatNaft, personel.codePersonNaft, personel.xedmatType, personel.tarixSarbaziShuru, " & _
           "personel.shMosalsalsh, personel.NoeIsargari, tblUnits.UnitsName, chartTbl.chartName, chartTbl.personelAdmin  " & _
               "   from  personel INNER JOIN  " & _
                            "       tblUnits ON personel.UnitsCode = tblUnits.UnitsCode LEFT OUTER JOIN  " & _
                        " chartTbl ON personel.codePersoneli = chartTbl.codePersoneli  " &
              "WHERE (personel.ID = " & i & ") And (personel.CodeClosed <> 0)"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
