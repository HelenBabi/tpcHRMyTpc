Imports System.Data
Imports System.Data.SqlClient

Partial Class frmTicketSysInfo

    Inherits System.Web.UI.Page
    Dim dtaspx As New DataTable


    Private Sub find_pc()
        Dim sqlwhere As Integer = Request.QueryString("PwhereSTR")
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(sqlwhere)
        txt_SystemCode.Text = sqlwhere
        txt_holder.Text = dt.Rows(0).Item("nam").ToString
        txt_bulding.Text = dt.Rows(0).Item("BuildingName").ToString
        txt_SystemTabage.Text = dt.Rows(0).Item("SystemTabage").ToString
        txt_Systemunit.Text = dt.Rows(0).Item("UnitsName").ToString
        txt_SystemPhone.Text = dt.Rows(0).Item("SystemPhone").ToString
        txt_SystemRoom.Text = dt.Rows(0).Item("SystemRoom").ToString
        txt_SystemRoomNum.Text = dt.Rows(0).Item("SystemRoomNum").ToString
        txt_ComputerName.Text = dt.Rows(0).Item("ComputerName").ToString
        txtNote.Text = dt.Rows(0).Item("tozihat").ToString

        If dt.Rows(0).Item("upAntivirus").ToString = True Then
            chkAntivirus.Checked = True
        End If
        If dt.Rows(0).Item("joinDomain").ToString = True Then
            chkjoinDomain.Checked = True
        End If
        If dt.Rows(0).Item("pxe").ToString = True Then
            Chkpxe.Checked = True
        End If

        If dt.Rows(0).Item("pollicy").ToString = True Then
            chkPlicy.Checked = True
        End If
        If dt.Rows(0).Item("cd").ToString = True Then
            chkcd.Checked = True
        End If
        If dt.Rows(0).Item("usb").ToString = True Then
            chkusb.Checked = True
        End If
        If dt.Rows(0).Item("modem").ToString = True Then
            chkmodem.Checked = True
        End If
        If dt.Rows(0).Item("fdd").ToString = True Then
            chkfdd.Checked = True
        End If
        load_grid_hardware()
        load_grid_DomainUsers()
        load_grid_software()
        load_gridDrive()
        service_dorei()
        ' txt_vaziat.Text = dt.Rows(0).Item("CodeClosed").ToString
        Button1.Enabled = True

        If dt.Rows(0).Item("CodeClosed").ToString = True Then

            txt_vaziat.Text = "رده خارج"
        Else
            txt_vaziat.Text = "فعال"
        End If

    End Sub
    Private Sub service_dorei()
        Dim sqlstr As String

        sqlstr = "SELECT Max(tblWorkHdr.WorkDateS) AS Expr2    FROM tblWorkHdr " & _
            " WHERE(tblWorkHdr.SystemCode = " & txt_SystemCode.Text & ") and tblWorkHdr.WorkTypeCode=4 "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            txt_dateServiceDorei.Text = dt.Rows(0).Item("Expr2").ToString

        End If

    End Sub
    Private Sub load_grid_software()
        Dim sqlstr As String
        sqlstr = "SELECT     tblSystemSDtl.SystemCode, tblBaseSystemSDtl.BaseSystemSDtlNmae , tblBaseSystemSDtl.BaseSystemSDtlCode" & _
    " FROM    tblBaseSystemSDtl INNER JOIN    tblSystemSDtl ON tblBaseSystemSDtl.BaseSystemSDtlCode = tblSystemSDtl.BaseSystemSDtlCode " & _
        " WHERE(tblSystemSDtl.SystemCode = " & txt_SystemCode.Text & ")"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdsoftware.DataSource = dt
        grdsoftware.DataBind()

    End Sub
    Private Sub load_grid_DomainUsers()
        Dim sqlstr As String
        sqlstr = "SELECT     tblPcDomainUserDtl.systemCode, tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS nam, tblUsers.UserName_, tblUsers.UserEmail " & _
               " FROM         tblPcDomainUserDtl INNER JOIN    tblUsers ON tblPcDomainUserDtl.PcUserCode = tblUsers.UserCode " & _
                 " WHERE     (tblPcDomainUserDtl.systemCode = " & Val(txt_SystemCode.Text) & ")"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdDomainUsers.DataSource = dt
        grdDomainUsers.DataBind()

    End Sub

    Private Sub load_grid_hardware()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dtaspx = ClsB_.GetAllBrowsSystems(txt_SystemCode.Text)
        If dtaspx.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dtaspx
            ASPxGrvHead.DataBind()
        Else
            '   msg_box("اين شماره اموال داراي قطعه نمي باشد")
        End If

    End Sub

 


    Private Sub clear_form()
        txt_bulding.Text = ""
        txt_ComputerName.Text = ""
        txt_dateServiceDorei.Text = ""
        txt_holder.Text = ""
        txt_SystemPhone.Text = ""
        txt_SystemRoom.Text = ""
        txt_SystemRoomNum.Text = ""
        txt_SystemTabage.Text = ""
        txt_Systemunit.Text = ""
        txt_vaziat.Text = ""
        txtNote.Text = ""
        grdDomainUsers.DataBind()
        grdDrive.DataBind()
        grdsoftware.DataBind()
        ASPxGrvHead.DataBind()

    End Sub


    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("SystemHDtlStatus"))
        If value = 0 Then
            e.Row.ForeColor = Drawing.Color.Green
            '   e.Row.BackColor = Drawing.Color.Aquamarine
        Else
            e.Row.ForeColor = Drawing.Color.Red
            '   e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
        End If
    End Sub



    Private Sub load_gridDrive()
        Dim sqlstr As String
        sqlstr = "SELECT     tblSystemDriveDtl.idd, tblBaseDriveType.dirveTyp, tblBaseDrive.driveName, tblSystemDriveDtl.capacity_, tblSystemDriveDtl.used_, tblSystemDriveDtl.free_, " & _
        " tblSystemDriveDtl.os FROM         tblBaseDrive INNER JOIN " & _
                      " tblSystemDriveDtl ON tblBaseDrive.driveCode = tblSystemDriveDtl.driveCode INNER JOIN " & _
                      " tblBaseDriveType ON tblSystemDriveDtl.driveType = tblBaseDriveType.idd " & _
       " WHERE(tblSystemDriveDtl.systemCode = " & txt_SystemCode.Text & ") order by tblBaseDrive.driveCode"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdDrive.DataSource = dt
        grdDrive.DataBind()

    End Sub



    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim furl As String = "frmSabeghe.aspx?PwhereSTR=" & Val(txt_SystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=400,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        find_pc()
    End Sub
End Class
