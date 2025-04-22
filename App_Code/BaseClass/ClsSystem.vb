Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsSystem
    Public Function GetAllSystem(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT   tblSystem.hassas, tblHolder.personCode, tblHolder.HolderName, tblHolder.HolderFamili, tblUnits.UnitsName, tblBuilding.BuildingName, tblSystem.SystemRoom, tblSystem.SystemPhone, tblSystem.DoreDate, " & _
        " tblSystem.SystemCode FROM         tblHolder INNER JOIN " & _
                      " tblSystem ON tblHolder.HolderCode = tblSystem.HolderCode INNER JOIN " & _
                             " tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                      " tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode   WHERE (tblSystem.SystemCode <> 0) AND (tblSystem.CodeClosed = 0)  AND (tblSystem.Rack = 0) "
        If deactive = True Then
            sqlstr = "SELECT     tblSystem.hassas,tblHolder.personCode, tblHolder.HolderName, tblHolder.HolderFamili, tblUnits.UnitsName, tblBuilding.BuildingName, tblSystem.SystemRoom, tblSystem.SystemPhone, tblSystem.DoreDate, " & _
        " tblSystem.SystemCode FROM         tblHolder INNER JOIN " & _
                      " tblSystem ON tblHolder.HolderCode = tblSystem.HolderCode INNER JOIN " & _
                             " tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                      " tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode   WHERE (tblSystem.SystemCode <> 0) "
        End If

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function GetAllSystemDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblHolder.personCode,tblHolder.HolderName, tblHolder.HolderFamili, tblUnits.UnitsName, tblBuilding.BuildingName, tblSystem.SystemRoom, tblSystem.SystemPhone, tblSystem.DoreDate, " & _
        " tblSystem.SystemCode FROM         tblHolder INNER JOIN " & _
                      " tblSystem ON tblHolder.HolderCode = tblSystem.HolderCode INNER JOIN " & _
                             " tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                      " tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode   WHERE  (tblSystem.CodeClosed = 0) "
        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function

    Public Function GetSystem(ByVal i As Int64, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  tblSystem.hassas,SystemTP, UnitsCode, BuildingCode, SystemRoom, SystemPhone, HolderCode, CodeClosed, SystemCode, SystemTabage, " & _
        " SystemRoomNum, ComputerName, pollicy, tozihat,upAntivirus, joinDomain, pxe, usb, cd, modem, typ, fdd,vdi,companyID,omurID,blok FROM         tblSystem WHERE (CodeClosed = 0) AND (SystemCode <> 0) AND (SystemCode = " & i & ")"
        If deactive = True Then
            sqlstr = "SELECT  tblSystem.hassas,SystemTP, UnitsCode, BuildingCode, SystemRoom, SystemPhone, HolderCode, CodeClosed, SystemCode, SystemTabage, " & _
        " SystemRoomNum, ComputerName, pollicy, tozihat,upAntivirus, joinDomain, pxe, usb, cd, modem, fdd, typ, vdi,companyID,omurID,blok   FROM         tblSystem   WHERE (SystemCode <> 0) AND (SystemCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function getallsystemfillgrd() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from browseAllSystemVW "
        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function getallHolders() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        HolderCode, HolderName, HolderFamili, personCode " & _
        " FROM tblHolder         WHERE(CodeClosed = 0) and (HolderCode <> 0 ) " & _
" ORDER BY HolderFamili, HolderName"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function getallsystemfillgrdDomain(ByVal PCuserCode As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr = "SELECT * from userSystemsVW " & _
                           " WHERE(Codepersonel = '" & PCuserCode & "') "

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function getallsystemfillgrdRack() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblBuilding.BuildingName, tblSystem.DoreDate, tblSystem.pollicy, tblSystem.SystemCode, tblSystem.CodeClosed, tblSystem.ComputerName,   tblSystem.tozihat " & _
                  " FROM         tblSystem INNER JOIN     tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode " & _
              " WHERE(tblSystem.CodeClosed = 0) And (tblSystem.rack = 1) ORDER BY tblSystem.tarix DESC"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function getsystemgrd(ByVal i As Int64) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT    tblSystem.HolderCode, tblHolder.personCode,tblHolder.HolderName, tblHolder.HolderFamili, tblUnits.UnitsName, tblBuilding.BuildingName, tblSystem.SystemRoom, tblSystem.SystemPhone, tblSystem.DoreDate, " & _
        " tblSystem.SystemCode FROM         tblHolder INNER JOIN " & _
                      " tblSystem ON tblHolder.HolderCode = tblSystem.HolderCode INNER JOIN " & _
                             " tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                      " tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode where tblSystem.SystemCode=" & i & ""

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function
    Public Function getHolder(ByVal i As Int64) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        * " & _
        " FROM perInfoVW         WHERE (codePersoneli = " & i & ") ORDER BY family"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function getUser(ByVal i As Int64) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT UserCode  FROM tblUsers         WHERE UserCode =" & i & " "
        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function

    Public Function getallsystemfillgrdArc(ByVal SystemCode As Int64) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblSystemArc.SystemRoom, tblSystemArc.SystemPhone, tblSystemArc.SystemCode, tblSystemArc.SystemTabage, tblSystemArc.SystemRoomNum, " & _
                      "tblSystemArc.ComputerName, tblSystemArc.tozihat, tblSystemArc.tarixEdit, tblSystemArc.saatEdit,  " & _
                     " tblHolder.HolderName + ' ' + tblHolder.HolderFamili AS holder, tblBuilding.BuildingName, tblBaseSystemType.SystemName,  " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblUnits.UnitsName " & _
                        "FROM         tblSystemArc INNER JOIN " & _
                      "tblHolder ON tblSystemArc.HolderCode = tblHolder.HolderCode INNER JOIN " & _
                      "tblBuilding ON tblSystemArc.BuildingCode = tblBuilding.BuildingCode INNER JOIN " & _
                      "tblBaseSystemType ON tblSystemArc.typ = tblBaseSystemType.SystemTypCode INNER JOIN " & _
                     " tblOpId ON tblSystemArc.opid = tblOpId.opid INNER JOIN " & _
                    "  tblUsers ON tblOpId.UserCode = tblUsers.UserCode    INNER JOIN " & _
                      "tblUnits ON tblSystemArc.UnitsCode = tblUnits.UnitsCode " & _
                     " WHERE     (tblSystemArc.SystemCode = " & SystemCode & ")  " & _
    " ORDER BY tblSystemArc.tarixEdit DESC"

        dt = ClsDataTable.GetDataTableHD(sqlstr)
        Return dt
    End Function

End Class
