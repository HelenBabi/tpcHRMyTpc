Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBrowsWorks
    Public Function GetAllBrowsWorks(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblWorkHdr.erja_id, tblWorkHdr.WorkId, tblWorkHdr.SystemCode, tblWorkType.WorkTypeName, tblWorkType.WorkTypeCode, tblWorkHdr.WorkDateS, " & _
                    "  tblWorkHdr.WorkTimeS, tblWorkHdr.WorkDateE, tblWorkHdr.WorkTimeE, tblWorkHdr.WorkNote,  " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS serviskar, tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS ferestande " & _
                      "FROM         tblUsers AS tblUsers_1 INNER JOIN " & _
                     " tblHerja ON tblUsers_1.UserCode = tblHerja.user_id RIGHT OUTER JOIN " & _
                     " tblWorkHdr INNER JOIN " & _
                     " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode INNER JOIN " & _
                     " tblUsers ON tblWorkHdr.WorkerCodeS = tblUsers.UserCode ON tblHerja.erja_id = tblWorkHdr.erja_id " & _
                      " WHERE     (tblWorkHdr.WorkDateE BETWEEN '" & az & "' AND '" & taa & "')"

        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetBrowsRequest(ByVal PcUserCode As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        tblHerja.erja_id, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.date_E + ' ' + tblHerja.TIME_E AS zamanEtmam,  " & _
       " tblHerja.stat_, tblHerjaStatus.name_hvaz, tblHerja.user_id, tblUsers.Codepersonel " & _
              " FROM            tblHerja INNER JOIN " & _
                        " tblHerjaStatus ON tblHerja.stat_ = tblHerjaStatus.id_hvaz INNER JOIN " & _
                        " tblUsers ON tblHerja.user_id = tblUsers.UserCode " & _
                     " WHERE        (tblUsers.Codepersonel = '" & PcUserCode & "')  " & _
                          "ORDER BY tblHerja.erja_id DESC"
        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function GetAdminRequest() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblHerja.erja_id, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.date_E + ' ' + tblHerja.TIME_E AS zamanEtmam, tblHerja.stat_, " & _
                      " tblHerjaStatus.name_hvaz, tblHerja.user_id, tblWorkType.WorkTypeName, tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS ferestande, tblUsers.UserName_,  " & _
        " tblHerja.mojavez, tblHerja.Typestr  FROM         tblHerja INNER JOIN " & _
          "            tblHerjaStatus ON tblHerja.stat_ = tblHerjaStatus.id_hvaz INNER JOIN " & _
           "           tblWorkType ON tblHerja.Typestr = tblWorkType.WorkTypeCode INNER JOIN " & _
            "          tblUsers ON tblHerja.user_id = tblUsers.UserCode " & _
      " WHERE   (tblHerja.stat_ = 6) AND   (tblHerja.mojavez = 1) and (tblHerja.Typestr <> '1')  " & _
    " ORDER BY tblHerja.erja_id DESC"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAdminRequestSaxtafzar() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblHerja.erja_id, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.date_E + ' ' + tblHerja.TIME_E AS zamanEtmam, tblHerja.stat_, " & _
                      " tblHerjaStatus.name_hvaz, tblHerja.user_id, tblWorkType.WorkTypeName, tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS ferestande, tblUsers.UserName_,  " & _
        " tblHerja.mojavez, tblHerja.Typestr  FROM         tblHerja INNER JOIN " & _
          "            tblHerjaStatus ON tblHerja.stat_ = tblHerjaStatus.id_hvaz INNER JOIN " & _
           "           tblWorkType ON tblHerja.Typestr = tblWorkType.WorkTypeCode INNER JOIN " & _
            "          tblUsers ON tblHerja.user_id = tblUsers.UserCode " & _
      " WHERE     (tblHerja.stat_ = 6) AND (tblHerja.mojavez = 1) and (tblHerja.Typestr = '1') " & _
    " ORDER BY tblHerja.erja_id DESC"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAdminRequestAll() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   tblHerja.date_s,  tblHerja.erja_id, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.date_E + ' ' + tblHerja.TIME_E AS zamanEtmam, tblHerja.stat_, " & _
                      " tblHerjaStatus.name_hvaz, tblHerja.user_id, tblWorkType.WorkTypeName, tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS ferestande, tblUsers.UserName_,  " & _
        " tblHerja.mojavez, tblHerja.Typestr  FROM         tblHerja INNER JOIN " & _
          "            tblHerjaStatus ON tblHerja.stat_ = tblHerjaStatus.id_hvaz INNER JOIN " & _
           "           tblWorkType ON tblHerja.Typestr = tblWorkType.WorkTypeCode INNER JOIN " & _
            "          tblUsers ON tblHerja.user_id = tblUsers.UserCode " & _
      " WHERE     (tblHerja.stat_ <> 9) " & _
    " ORDER BY tblHerja.erja_id DESC"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBrowsRequestArch(ByVal PcUserCode As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      tblHerja.erja_id, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.date_E + ' ' + tblHerja.TIME_E AS zamanEtmam," & _
        "  tblHerja.stat_, tblHerjaStatus .name_hvaz, tblHerja.user_id " & _
          " FROM         tblHerja INNER JOIN      tblHerjaStatus  ON tblHerja.stat_ = tblHerjaStatus .id_hvaz " & _
        " WHERE(tblHerja.user_id = " & PcUserCode & ") and tblHerja.archivee= 1 ORDER BY tblHerja.erja_id DESC"
        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBrowsTickets(ByVal typUser As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblHerja.mojavez, tblHerja.stat_, tblHerja.erja_id, tblHerja.Typestr, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.telfon, tblUsers.UserName_, " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS ferestande, tblHerja.ipp, tblSystem.ComputerName, tblHerjaStatus.name_hvaz " & _
                       " FROM         tblHerja INNER JOIN     tblUsers ON tblHerja.user_id = tblUsers.UserCode INNER JOIN " & _
                      " tblSystem ON tblHerja.systemcode = tblSystem.SystemCode INNER JOIN " & _
                      " tblHerjaStatus ON tblHerja.stat_ = tblHerjaStatus.id_hvaz " & _
                " WHERE     (tblHerja.Typestr = " & typUser & ") AND (tblHerja.stat_ <> 2) AND (tblHerja.stat_ <> 9) and tblHerja.archivee= 0 " & _
                " ORDER BY tblHerja.erja_id"
   
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetBrowsTicketsArchWorker(ByVal typUser As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblHerja.mojavez, tblHerja.stat_, tblHerja.erja_id, tblHerja.Typestr, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.telfon, tblUsers.UserName_, " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS ferestande, tblHerja.ipp, tblSystem.ComputerName, tblHerjaStatus.name_hvaz " & _
                       " FROM         tblHerja INNER JOIN     tblUsers ON tblHerja.user_id = tblUsers.UserCode INNER JOIN " & _
                      " tblSystem ON tblHerja.systemcode = tblSystem.SystemCode INNER JOIN " & _
                      " tblHerjaStatus ON tblHerja.stat_ = tblHerjaStatus.id_hvaz " & _
                " WHERE     (tblHerja.Typestr = " & typUser & ") AND (tblHerja.stat_ <> 2) AND (tblHerja.stat_ <> 9) and tblHerja.archivee= 1 " & _
                " ORDER BY tblHerja.erja_id"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetBrowsTicketsArch(ByVal typUser As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblHerja.erja_id, tblHerja.Typestr, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal, tblHerja.telfon, tblUsers.UserName_, " & _
                      " tblUsers.UserLastName + tblUsers.UserFamili AS ferestande, tblHerja.ipp   FROM         tblHerja INNER JOIN " & _
                      " tblUsers ON tblHerja.user_id = tblUsers.UserCode " & _
                         " WHERE     (tblHerja.Typestr = '" & typUser & "')  and tblHerja.stat_ = 2 ORDER BY tblHerja.erja_id"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBrowsWorksCode(ByVal sqlwhere As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblWorkHdr.WorkId, tblWorkHdr.SystemCode, tblHolder.HolderFamili, tblWorkType.WorkTypeName, tblWorkType.WorkTypeCode, " & _
                      " tblWorkHdr.WorkDateS, tblWorkHdr.WorkTimeS, tblWorkHdr.WorkDateE, tblWorkHdr.WorkTimeE, tblWorkHdr.WorkNote,  " & _
                      " tblUsers.UserFamili AS pasoxgoo, tblUsers_1.UserFamili AS serviskar FROM         tblSystem INNER JOIN " & _
                      " tblHolder ON tblSystem.HolderCode = tblHolder.HolderCode INNER JOIN " & _
                      " tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode INNER JOIN " & _
                      " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode INNER JOIN " & _
                      " tblUsers ON tblWorkHdr.WorkerCodeP = tblUsers.UserCode INNER JOIN " & _
                      " tblUsers AS tblUsers_1 ON tblWorkHdr.WorkerCodeS = tblUsers_1.UserCode " & _
                       " WHERE     tblWorkHdr.SystemCode=" & sqlwhere & ""

        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBrowsWorksSabege(ByVal sqlwhere As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        'sqlstr = "SELECT    tblWorkHdr. erja_id, tblWorkHdr.WorkId, tblWorkHdr.SystemCode, tblHolder.HolderFamili, tblWorkType.WorkTypeName, tblWorkType.WorkTypeCode, " & _
        '              " tblWorkHdr.WorkDateS, tblWorkHdr.WorkTimeS, tblWorkHdr.WorkDateE, tblWorkHdr.WorkTimeE, tblWorkHdr.WorkNote,  " & _
        '              " tblUsers.UserFamili AS pasoxgoo, tblUsers_1.UserFamili AS serviskar FROM         tblSystem INNER JOIN " & _
        '              " tblHolder ON tblSystem.HolderCode = tblHolder.HolderCode INNER JOIN " & _
        '              " tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode INNER JOIN " & _
        '              " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode INNER JOIN " & _
        '              " tblUsers ON tblWorkHdr.WorkerCodeP = tblUsers.UserCode INNER JOIN " & _
        '              " tblUsers AS tblUsers_1 ON tblWorkHdr.WorkerCodeS = tblUsers_1.UserCode " & _
        '               " WHERE     tblWorkHdr.SystemCode=" & sqlwhere & " order by WorkDateS desc"


        sqlstr = "SELECT     tblWorkHdr.erja_id, tblWorkHdr.WorkId, tblWorkHdr.SystemCode, tblWorkType.WorkTypeName, tblWorkType.WorkTypeCode, tblWorkHdr.WorkDateS, " & _
                    "  tblWorkHdr.WorkTimeS, tblWorkHdr.WorkDateE, tblWorkHdr.WorkTimeE, tblWorkHdr.WorkNote,  " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS serviskar, tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS ferestande " & _
                      "FROM         tblUsers AS tblUsers_1 INNER JOIN " & _
                     " tblHerja ON tblUsers_1.UserCode = tblHerja.user_id RIGHT OUTER JOIN " & _
                     " tblWorkHdr INNER JOIN " & _
                     " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode INNER JOIN " & _
                     " tblUsers ON tblWorkHdr.WorkerCodeS = tblUsers.UserCode ON tblHerja.erja_id = tblWorkHdr.erja_id " & _
                       " WHERE     tblWorkHdr.SystemCode=" & sqlwhere & " order by WorkDateS desc"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllBrowsWorksDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      tblWorkHdr.SystemCode, tblWorkHdr.WorkDateS, tblWorkHdr.WorkTimeS, tblWorkHdr.WorkDateE, tblWorkHdr.WorkTimeE, " & _
                       " tblWorkHdr.WorkNote, tblWorker.WorkerName, tblWorker.WorkerFamili, tblWorker.WorkerName AS nams, tblWorker.WorkerFamili AS fnams, " & _
          " tblBaseWork.BaseWorkCode, tblBaseWork.BaseWorkName FROM         tblWorkHdr INNER JOIN  tblWorkDtl ON tblWorkHdr.WorkId = tblWorkDtl.WorkId INNER JOIN " & _
                        " tblBaseWork ON tblWorkDtl.BaseWorkCode = tblBaseWork.BaseWorkCode INNER JOIN  tblWorker ON tblWorkHdr.WorkerCodeP = tblWorker.WorkerCode INNER JOIN " & _
                        " tblWorker AS tblWorker_1 ON tblWorkHdr.WorkerCodeS = tblWorker_1.WorkerCode WHERE     (tblWorkHdr.WorkDateS = '1391/07/12')"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetBrowsWorks(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblWorkHdr.SystemCode, tblWorkHdr.WorkDateS, tblWorkHdr.WorkTimeS, tblWorkHdr.WorkDateE, tblWorkHdr.WorkTimeE, " & _
                     " tblWorkHdr.WorkNote, tblWorker.WorkerName, tblWorker.WorkerFamili, tblWorker.WorkerName AS nams, tblWorker.WorkerFamili AS fnams, " & _
        " tblBaseWork.BaseWorkCode, tblBaseWork.BaseWorkName FROM         tblWorkHdr INNER JOIN  tblWorkDtl ON tblWorkHdr.WorkId = tblWorkDtl.WorkId INNER JOIN " & _
                      " tblBaseWork ON tblWorkDtl.BaseWorkCode = tblBaseWork.BaseWorkCode INNER JOIN  tblWorker ON tblWorkHdr.WorkerCodeP = tblWorker.WorkerCode INNER JOIN " & _
                      " tblWorker AS tblWorker_1 ON tblWorkHdr.WorkerCodeS = tblWorker_1.WorkerCode WHERE     (tblWorkHdr.WorkDateS = '1391/07/12')"

        'sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed   FROM tblBrowsWorks  WHERE (CodeClosed = 0) AND (BrowsWorksCode <> 0) AND (BrowsWorksCode = " & i & ")"
        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed   FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) AND (BrowsWorksCode = " & i & ")"
        'End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
