Imports System.Data
Imports System.Data.SqlClient

Partial Class frmBrowsSysFullTicket
    Inherits System.Web.UI.Page
    Dim dtaspx As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        find_pc()

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrowsSysFull.aspx", Val(Session("UserId")))

    End Sub



    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

#End Region




    Private Sub find_pc()
        Dim sqlwhere As Integer = Request.QueryString("PwhereSTR")
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(sqlwhere)

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
            msg_box("اين سيستم رده خارج شده است")
            txt_vaziat.Text = "رده خارج"
        Else
            txt_vaziat.Text = "فعال"
        End If

    End Sub
    Private Sub service_dorei()
        Dim sqlstr As String

        sqlstr = "SELECT Max(tblWorkHdr.WorkDateS) AS Expr2    FROM tblWorkHdr " & _
            " WHERE(tblWorkHdr.SystemCode = " & 399222 & ") and tblWorkHdr.WorkTypeCode=4 "

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
        " WHERE(tblSystemSDtl.SystemCode = " & 399222 & ")"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdsoftware.DataSource = dt
        grdsoftware.DataBind()

    End Sub
    Private Sub load_grid_DomainUsers()
        Dim sqlstr As String
        sqlstr = "SELECT     tblBaseDomainUser.PcUserName + ' ' + tblBaseDomainUser.PcUserFamili as nam, tblBaseDomainUser.PcUserIdDomain, " & _
        " tblBaseDomainUser.PcUserEmail, tblPcDomainUserDtl.systemCode  FROM   tblBaseDomainUser INNER JOIN " & _
                      " tblPcDomainUserDtl ON tblBaseDomainUser.PcUserCode = tblPcDomainUserDtl.PcUserCode " & _
        "WHERE(tblPcDomainUserDtl.systemCode = " & 399222 & ")"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdDomainUsers.DataSource = dt
        grdDomainUsers.DataBind()

    End Sub

    Private Sub load_grid_hardware()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dtaspx = ClsB_.GetAllBrowsSystems(399222)
        If dtaspx.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dtaspx
            ASPxGrvHead.DataBind()
        Else
            '   msg_box("اين شماره اموال داراي قطعه نمي باشد")
        End If

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        clear_form()
        chkAntivirus.Checked = False
        chkjoinDomain.Checked = False
        Chkpxe.Checked = False
        chkPlicy.Checked = False
        chkcd.Checked = False
        chkusb.Checked = False
        chkfdd.Checked = False
        chkmodem.Checked = False
        find_pc()
        'If ASPxGrvHead.
    End Sub



    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        If Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")
        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        ElseIf Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")
        End If
    End Sub

    Protected Sub ASPxGrvHead_BeforePerformDataSelect(sender As Object, e As System.EventArgs) Handles ASPxGrvHead.BeforePerformDataSelect

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

    Protected Sub ibtnPrint_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnPrint.Click
        Session("RpName") = "report_browsSysFull"
        Dim furl As String = "frmPgReport.aspx?PwhereSTR=" + " WHERE tblSystem.SystemCode= " & txt_SystemCode.Text & "&PwhereSTR2=" & "and tblSystemHDtl.SystemCode=" & txt_SystemCode.Text
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=700,width=850,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Private Sub load_gridDrive()
        Dim sqlstr As String
        sqlstr = "SELECT     tblSystemDriveDtl.idd, tblBaseDriveType.dirveTyp, tblBaseDrive.driveName, tblSystemDriveDtl.capacity_, tblSystemDriveDtl.used_, tblSystemDriveDtl.free_, " & _
        " tblSystemDriveDtl.os FROM         tblBaseDrive INNER JOIN " & _
                      " tblSystemDriveDtl ON tblBaseDrive.driveCode = tblSystemDriveDtl.driveCode INNER JOIN " & _
                      " tblBaseDriveType ON tblSystemDriveDtl.driveType = tblBaseDriveType.idd " & _
       " WHERE(tblSystemDriveDtl.systemCode = " & 399222 & ") order by tblBaseDrive.driveCode"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdDrive.DataSource = dt
        grdDrive.DataBind()

    End Sub


    Protected Sub ibtnHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnHistory.Click
        'Session("RpName") = "report_browsSysFull"
        Dim furl As String = "frmSabeghe.aspx?PwhereSTR=" & Val(txt_SystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=400,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim furl As String = "frmSabeghe.aspx?PwhereSTR=" & Val(txt_SystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=400,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub
End Class
