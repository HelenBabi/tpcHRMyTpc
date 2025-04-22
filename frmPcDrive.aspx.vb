Imports System.Data
Imports System.Data.SqlClient

Partial Class frmPcDrive
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1

            ' meno_acc()
        End If
        If Me.IsPostBack = False Then
            load_cmb()
            load_cmbDriveType()
        End If

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmPcDrive.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)
        ibtnDeleted.Visible = meno_arr(7)
        ibtnHistory.Visible = meno_arr(8)
        ibtnDeactived.Visible = meno_arr(9)
        ibtnPrint.Visible = meno_arr(10)
        ibtnExit.Visible = meno_arr(11)
    End Sub

    Private Sub enable_disable()
        If ibtnSave.Enabled = False Then
            ibtnSave.Enabled = True
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True

            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            ibtnDeleted.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False
            ibtnExit.Enabled = False
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel.png"

            ibtnEdit.ImageUrl = "~/Icons/edit2.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete2.png"
            ibtnAdd.ImageUrl = "~/Icons/Add2.png"
            ibtnDeleted.ImageUrl = "~/Icons/deleted2.png"
            ibtnHistory.ImageUrl = "~/Icons/history2.png"
            ibtnDeactived.ImageUrl = "~/Icons/reload2.png"
            ibtnPrint.ImageUrl = "~/Icons/Print2.png"
            ''''''''''''''''''''''''''''''''''''''''''''''''


            txt_SystemCode.Enabled = True
            txt_SystemPhone.Enabled = True
            txt_SystemRoom.Enabled = True



        Else
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False

            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True
            ibtnDeleted.Enabled = True
            ibtnHistory.Enabled = True
            ibtnDeactived.Enabled = True
            ibtnPrint.Enabled = True
            ibtnExit.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew2.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel2.png"

            ibtnEdit.ImageUrl = "~/Icons/edit.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete.png"
            ibtnAdd.ImageUrl = "~/Icons/Add.png"
            ibtnDeleted.ImageUrl = "~/Icons/deleted.png"
            ibtnHistory.ImageUrl = "~/Icons/history.png"
            ibtnDeactived.ImageUrl = "~/Icons/reload.png"
            ibtnPrint.ImageUrl = "~/Icons/Print.png"
            ''''''''''''''''''''''''''''''''''''''''''''''''

            txt_SystemCode.Enabled = False
            txt_SystemPhone.Enabled = False
            txt_SystemRoom.Enabled = False


            ' txt_BakhshCode.Enabled = True
        End If

    End Sub
    Private Sub clear_form()
        txt_bulding.Text = ""
        txt_ComputerName.Text = ""
        txt_holder.Text = ""
        txt_SystemPhone.Text = ""
        txt_SystemRoom.Text = ""
        txt_SystemRoomNum.Text = ""
        txt_SystemTabage.Text = ""
        txt_Systemunit.Text = ""
        grdDrive.DataBind()
        GridView2.DataBind()

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
#Region "load fitment"

    Private Function fillDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     PcUserCode, PcUserIdDomain, CodeClosed, opid, ArcTypeCode  FROM tblBaseDomainUser WHERE     (CodeClosed = 0) AND (BaseSystemHDtlCode <> 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    'Private Sub loadddlcompany()
    '    Dim i As Integer
    '    Dim editRow As GridViewRow
    '    Dim ddlkharidd As New DropDownList
    '    Dim dt As New DataTable

    '    Dim sqlstr As String

    '    For i = 0 To GridView1.Rows.Count - 1
    '        Try
    '            editRow = GridView1.Rows(i)
    '            ddlkharidd = editRow.FindControl("ddl_note")
    '            ' sqlstr = "SELECT FitmentCode, FitmentNoteCode, FitmentNote, CodeClosed FROM tblBaseFitmentNote WHERE (FitmentCode = " & GridView1.Rows(i).Cells(0).Text & ") AND (CodeClosed = 0)"
    '            sqlstr = "SELECT     BaseModelCode, BaseSystemHDtlCode, BaseModelName, CodeClosed, opid, ArcTypeCode  FROM tblBaseModel WHERE     (CodeClosed = 0) AND (BaseModelCode <> 0) AND (BaseSystemHDtlCode = " & GridView1.Rows(i).Cells(0).Text & ")"
    '            dt = ClsDataTable.GetDataTable(sqlstr)
    '            If dt.Rows.Count <> 0 Then
    '                ddlkharidd.Items.Add("---")
    '                ddlkharidd.DataSource = dt
    '                ddlkharidd.DataTextField = "BaseModelName"
    '                ddlkharidd.DataValueField = "BaseModelCode"
    '                ddlkharidd.DataBind()
    '                ddlkharidd.Items.Add("---")
    '                ddlkharidd.SelectedValue = "---"
    '                dt.Clear()
    '            End If
    '        Catch ex As Exception
    '        End Try
    '    Next
    'End Sub

    'Private Sub fill_visit_info(ByVal visitcod As Int64)

    '    Dim i As Integer
    '    Dim editRow As GridViewRow
    '    Dim ddl As New DropDownList
    '    Dim chb As New CheckBox

    '    Dim dt As New DataTable

    '    Dim sqlstr As String

    '    For i = 0 To GridView1.Rows.Count - 1
    '        Try
    '            editRow = GridView1.Rows(i)
    '            ddl = editRow.FindControl("ddl_note")
    '            chb = editRow.FindControl("chb_YesNo")
    '            sqlstr = "SELECT  VisitCode, FitmentCode, FitmentYesNo, FitmentNoteCode   FROM tblFitment  WHERE     (VisitCode = " & visitcod & ") AND (FitmentCode = " & GridView1.Rows(i).Cells(0).Text & ")"
    '            dt = ClsDataTable.GetDataTable(sqlstr)
    '            If dt.Rows.Count <> 0 Then
    '                ddl.SelectedValue = dt.Rows(0).Item("FitmentNoteCode").ToString
    '                chb.Checked = dt.Rows(0).Item("FitmentYesNo").ToString
    '                dt.Clear()
    '            End If
    '        Catch ex As Exception
    '        End Try
    '    Next
    'End Sub
#End Region


#Region "meno button"
    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSave.Click
        Call save_final()
        load_gridDrive()
        GridView2.DataBind()
    End Sub

    'Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
    '    New_()

    'End Sub

    Private Sub New_()
        ' hdn_Final_key.Value = "new"
        clear_form()
        'txt_SystemCode.Text = ClsGetMaxId.GetMaxId("tblSystem", "SystemCode")
        enable_disable()
    End Sub
    Private Sub save_()
        enable_disable()
        clear_form()
        hdn_Final_key.Value = 0
    End Sub

    Protected Sub ibtnSaveNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSaveNew.Click
        Call save_final()
        Call New_()
    End Sub

    Protected Sub ibtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEdit.Click
        Call edit_()
    End Sub
    Private Sub edit_()
        enable_disable()
        hdn_Final_key.Value = "edit"
    End Sub
    Private Sub cancel_()
        enable_disable()
        clear_form()
    End Sub

    Protected Sub ibtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnCancel.Click
        Call cancel_()
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDelete.Click
        Call dell_()
    End Sub
    Private Sub dell_()
        hdn_Final_key.Value = "del"
        Call save_final()
        enable_disable()
        clear_form()
    End Sub


    'Private Sub Deactived_()
    '    If hdn_deactivedcode.Value = "false" Then
    '        hdn_deactivedcode.Value = "true"
    '    Else
    '        hdn_deactivedcode.Value = "false"
    '    End If
    '    load_grid()
    'End Sub

#End Region





    'Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
    '    If e.CommandArgs.CommandName = "select" Then
    '        Dim ClsB As New ClsSystem
    '        Dim dt As New DataTable
    '        dt = ClsB.GetSystem(e.CommandArgs.CommandArgument, False)

    '        ' txt_SystemName.Text = dt.Rows(0).Item("SystemName").ToString
    '        'ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
    '    End If
    'End Sub


    Private Sub save_final()
        'If hdn_Final_key.Value = "new" Then txt_SystemCode.Text = ClsGetMaxId.GetMaxId("tblSystem", "SystemCode")

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        cmm.CommandText = "prcPcDriveDtl"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@TmpInOutId", SqlDbType.Int, 10)
        cmm.Parameters("@TmpInOutId").Value = Val(hdn_tmp.Value)


        'cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 6)
        'cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text


        cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
        cmm.Parameters("@userid").Value = Session("UserId")

        cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
        cmm.Parameters("@pm").Direction = ParameterDirection.Output

        cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
        cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
        cmm.Connection = cnn


        Dim result As Integer
        cmm.ExecuteNonQuery()
        result = cmm.Parameters("@ResultCode").Value
        If result = 0 Then
            msg_box("  عملیات با موفقیت انجام شد ")
            ' Call save_()
            ' load_grid()
        Else
            msg_box(cmm.Parameters("@pm").Value)
        End If

    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del2" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from tblTmpDrive where Tmp_id=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
    End Sub



    Protected Sub btnIn_Click(sender As Object, e As System.EventArgs) Handles btnIn.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTmpDrive", "TmpInOutId")
        End If
        save_tmp(True, False)
        GridView2.Visible = True
        load_grid_tmp()
        ChkOS.Checked = False
        txt_Capacity.Text = ""
        txt_free.Text = ""
    End Sub


    Private Sub load_grid_tmp()
        Dim sqlstr As String = "SELECT    tblTmpDrive.Tmp_id, tblBaseDriveType.dirveTyp, tblBaseDrive.driveName, tblTmpDrive.os, tblTmpDrive.free_, tblTmpDrive.used_, tblTmpDrive.capacity_  " & _
    " FROM         tblBaseDrive INNER JOIN     tblTmpDrive ON tblBaseDrive.driveCode = tblTmpDrive.driveCode INNER JOIN " & _
      "                tblBaseDriveType ON tblTmpDrive.driveType = tblBaseDriveType.idd " & _
" WHERE     (tblTmpDrive.UserCode = " & Session("UserId") & ") AND (tblTmpDrive.TmpInOutId = " & Val(hdn_tmp.Value) & ") "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub

    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)

        'Dim freee As Integer
        'freee = Val(txt_Capacity.Text) - Val(txt_Used.Text)


        Dim usedd As Integer
        usedd = Val(txt_Capacity.Text) - Val(txt_free.Text)


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTmpDrive"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@driveCode", SqlDbType.Int)
            cmm.Parameters("@driveCode").Value = Val(CmbDrivelist.SelectedValue)

            cmm.Parameters.Add("@driveType", SqlDbType.Int)
            cmm.Parameters("@driveType").Value = Val(CmbDriveType.SelectedValue)

            cmm.Parameters.Add("@capacity_", SqlDbType.Int)
            cmm.Parameters("@capacity_").Value = Val(txt_Capacity.Text)

            cmm.Parameters.Add("@used_", SqlDbType.Int)
            cmm.Parameters("@used_").Value = usedd

            cmm.Parameters.Add("@free_", SqlDbType.Int)
            cmm.Parameters("@free_").Value = Val(txt_free.Text)


            cmm.Parameters.Add("@oss", SqlDbType.Bit)
            cmm.Parameters("@oss").Value = ChkOS.Checked

            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

            cmm.Parameters.Add("@TmpInOutId", SqlDbType.Int, 10)
            cmm.Parameters("@TmpInOutId").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@UserCode", SqlDbType.Int, 10)
            cmm.Parameters("@UserCode").Value = Session("UserId")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  عملیات با موفقیت انجام شد ")
                '  Call save_()
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    '    Protected Sub Cmbg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDomainUser.SelectedIndexChanged
    '        Dim dtmg As New DataTable
    '        Dim sqlstr = "SELECT     tblBaseModel.BaseModelName, tblBaseModel.BaseModelCode " & _
    '" FROM         tblBaseModel INNER JOIN " & _
    '"                    tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
    '" WHERE     (tblBaseModel.CodeClosed = 0) AND (tblBaseModel.BaseSystemHDtlCode = " & CmbDomainUser.SelectedValue & ")"
    '        dtmg = ClsDataTable.GetDataTable(sqlstr)
    '        Cmbgd.DataSource = dtmg
    '        Cmbgd.DataTextField = "BaseModelName"
    '        Cmbgd.DataValueField = "BaseModelCode"
    '        Cmbgd.DataBind()

    '    End Sub

    Private Sub load_cmb()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     driveCode, driveName  FROM         tblBaseDrive WHERE     (stat = 1) ORDER BY driveCode"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbDrivelist.DataSource = dtm
        CmbDrivelist.DataTextField = "driveName"
        CmbDrivelist.DataValueField = "driveCode"
        CmbDrivelist.DataBind()

    End Sub

    Private Sub load_cmbDriveType()
        Dim dtg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idd, dirveTyp   FROM tblBaseDriveType WHERE     (CodeClosed = 0)"
        dtg = ClsDataTable.GetDataTable(sqlstr)
        CmbDriveType.DataSource = dtg
        CmbDriveType.DataTextField = "dirveTyp"
        CmbDriveType.DataValueField = "idd"
        CmbDriveType.DataBind()

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

    Private Sub find_pc()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(Val(txt_SystemCode.Text))
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("rack").ToString = True Then
                msg_box("اين شماره اموال براي " & " Rack  " & "ثبت شده است")
                Exit Sub
            End If
            txt_holder.Text = dt.Rows(0).Item("HolderFamili").ToString
            txt_bulding.Text = dt.Rows(0).Item("BuildingName").ToString
            txt_SystemTabage.Text = dt.Rows(0).Item("SystemTabage").ToString
            txt_Systemunit.Text = dt.Rows(0).Item("UnitsName").ToString
            txt_SystemPhone.Text = dt.Rows(0).Item("SystemPhone").ToString
            txt_SystemRoom.Text = dt.Rows(0).Item("SystemRoom").ToString
            txt_SystemRoomNum.Text = dt.Rows(0).Item("SystemRoomNum").ToString
            txt_ComputerName.Text = dt.Rows(0).Item("ComputerName").ToString
            load_gridDrive()

            ibtnSave.Enabled = True
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True
            btnIn.Enabled = True
            CmbDrivelist.Enabled = True

            If dt.Rows(0).Item("CodeClosed").ToString = True Then
                msg_box("اين سيستم رده خارج شده است")
                ibtnSave.Enabled = False
                ibtnSaveNew.Enabled = False
                ibtnCancel.Enabled = False
                btnIn.Enabled = False
                CmbDrivelist.Enabled = False
                Exit Sub

            End If

        Else
            msg_box("سيستمي با اين شماره اموال موجود نيست")
            txt_bulding.Text = ""
            txt_SystemTabage.Text = ""
            txt_Systemunit.Text = ""
            txt_holder.Text = ""
            txt_SystemRoom.Text = ""
            txt_SystemRoomNum.Text = ""
            txt_SystemPhone.Text = ""
            txt_ComputerName.Text = ""
            grdDrive.DataBind()
        End If

    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        clear_form()
        find_pc()

    End Sub
    Private Sub load_gridDrive()
        Dim sqlstr As String
        sqlstr = "SELECT     tblSystemDriveDtl.idd, tblBaseDriveType.dirveTyp, tblBaseDrive.driveName, tblSystemDriveDtl.capacity_, tblSystemDriveDtl.used_, tblSystemDriveDtl.free_, " & _
        " tblSystemDriveDtl.os FROM         tblBaseDrive INNER JOIN " & _
                      " tblSystemDriveDtl ON tblBaseDrive.driveCode = tblSystemDriveDtl.driveCode INNER JOIN " & _
                      " tblBaseDriveType ON tblSystemDriveDtl.driveType = tblBaseDriveType.idd " & _
       " WHERE(tblSystemDriveDtl.systemCode = " & Val(txt_SystemCode.Text) & ") order by tblBaseDrive.driveCode"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdDrive.DataSource = dt
        grdDrive.DataBind()

    End Sub

    Protected Sub grdDrive_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDrive.RowCommand
        If e.CommandName = "dell" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(grdDrive.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from tblSystemDriveDtl where idd=" & radif & " and SystemCode= " & Val(txt_SystemCode.Text) & ""
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_gridDrive()
        End If
    End Sub

   
    
End Class
