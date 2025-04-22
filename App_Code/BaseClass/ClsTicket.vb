Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsTicket

    Public Function GetBrowsRequest(ByVal PcUserCode As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      tblHerja.erja_id, tblHerja.systemcode, tblHerja.date_s + ' ' + tblHerja.TIME_S AS zamanErsal," & _
        "  tblHerja.stat_, tblHerjaStatus .name_hvaz, tblHerja.user_id " & _
          " FROM         tblHerja INNER JOIN      tblHerjaStatus  ON tblHerja.stat_ = tblHerjaStatus .id_hvaz " & _
        " WHERE(tblHerja.user_id = " & PcUserCode & ")  ORDER BY tblHerja.erja_id DESC"
        'If deactive = True Then
        '    sqlstr = "SELECT     BrowsWorksCode, BrowsWorksName, CodeClosed  FROM tblBrowsWorks  WHERE (BrowsWorksCode <> 0) "
        'End If

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function GetBrowsTicketWorker(ByVal erjaID As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   tblSystem.blok,tblSystem.tarixInfoUp + ' ' +  tblSystem.saatInfoUp as zamanUp, tblHerja.fori, tblHerja.Ramz, tblHerja.erja_id, tblHerja.buildingcode, tblHerja.Typestr, tblHerja.systemcode, tblHerja.user_id, tblHerja.date_s,  " & _
                     " tblHerja.TIME_S, tblHerja.telfon, tblHerja.stat_, tblUsers.UserName_, tblSystem.ComputerName,  " & _
                     " tblHolder.HolderName + ' ' + tblHolder.HolderFamili AS holder " & _
                   " FROM         tblHerja INNER JOIN " & _
                     " tblUsers ON tblHerja.user_id = tblUsers.UserCode INNER JOIN " & _
                     " tblSystem ON tblHerja.systemcode = tblSystem.SystemCode INNER JOIN " & _
                    "  tblHolder ON tblSystem.HolderCode = tblHolder.HolderCode " & _
                            " WHERE(tblHerja.erja_id = " & erjaID & ")"
        'End If

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function

 
End Class
