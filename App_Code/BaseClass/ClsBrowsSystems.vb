Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsBrowsSystems
    Public Function GetAllBrowsSystems(ByVal amval As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  tblSystemHDtl.SystemHDtlIp, tblSystemHDtl.SystemHDtlMac, tblSystemHDtl.id_rec, tblSystemHDtl.SystemCode, tblHolder.HolderName, tblHolder.HolderFamili, tblBaseSystemHDtl.BaseSystemHDtlNmae, tblBaseModel.BaseModelName, " & _
       " tblSystem.SystemTabage, tblSystem.SystemRoomNum, tblSystem.ComputerName, tblSystemHDtl.SystemHDtlTP, tblSystemHDtl.SystemHDtlSerial, tblSystemHDtl.SystemHDtlDateIn, tblSystemHDtl.SystemHDtlDateOut,tblSystemHDtl.BaseSystemHDtlCode, " & _
       " tblSystemHDtl.SystemHDtlStatus, tblSystemHDtl.AmvalCode, tblSystemHDtl.SystemHDtlGarantyDate, tblBuilding.BuildingName, tblUnits.UnitsName, tblSystem.SystemRoom, " & _
       " tblSystem.SystemPhone FROM tblBaseModel INNER JOIN  tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode INNER JOIN " & _
                     " tblSystemHDtl ON tblBaseModel.BaseModelCode = tblSystemHDtl.BaseModelCode AND  tblBaseSystemHDtl.BaseSystemHDtlCode = tblSystemHDtl.BaseSystemHDtlCode INNER JOIN " & _
                     " tblSystem ON tblSystemHDtl.SystemCode = tblSystem.SystemCode INNER JOIN  tblHolder ON tblSystem.HolderCode = tblHolder.HolderCode INNER JOIN " & _
                     " tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode INNER JOIN   tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode " & _
       " WHERE tblSystemHDtl.SystemCode = " & amval & " order by tblSystemHDtl.SystemHDtlStatus"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function GetAllBrowsSystemsDetail(ByVal amval As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        *  FROM            detailSystemVW  WHERE        (SystemCode = " & amval & ")"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function GetSystems(ByVal amval As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  tblSystem.port,tblSystem.node, tarixInfoUp, saatInfoUp, tblSystem.SystemTP, tblHolder.HolderCode, tblHolder.HolderName + ' ' + tblHolder.HolderFamili + ' ' + tblHolder.personCode AS nam, tblSystem.upAntivirus, tblSystem.joinDomain, tblSystem.pxe, tblSystem.vdi, " & _
        " tblSystem.CodeClosed, tblSystem.SystemRoom, tblSystem.SystemPhone, tblSystem.SystemTabage, tblSystem.SystemRoomNum, " & _
       "  tblSystem.ComputerName, tblSystem.cd, tblSystem.usb, tblSystem.fdd, tblSystem.modem, tblSystem.rack, tblBuilding.BuildingName, " & _
       " tblBuilding.BuildingEngName, tblUnits.UnitsName, tblHolder.HolderName, tblHolder.HolderFamili, tblSystem.SystemCode, tblSystem.tozihat,tblSystem.blok, " & _
        " tblSystem.pollicy, tblBaseSystemType.SystemName " & _
        " FROM         tblSystem INNER JOIN " & _
          "            tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode INNER JOIN " & _
           "           tblHolder ON tblSystem.HolderCode = tblHolder.HolderCode INNER JOIN " & _
            "          tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
             "         tblBaseSystemType ON tblSystem.typ = tblBaseSystemType.SystemTypCode " & _
        " WHERE(tblSystem.SystemCode = " & amval & ")"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
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

        dt = ClsDataTable.GetDataTableHD(sqlstr)
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
        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function


    Public Function GetSystemsAmval(ByVal amval As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       AmvalCode         FROM tblSystemHDtl         WHERE(AmvalCode = " & amval & ")"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function

End Class
