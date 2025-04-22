Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class frmWorkHldr
    Inherits System.Web.UI.Page
    Dim dtSistem As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clssistem_ As New ClsSystem
        dtSistem = clssistem_.getallsystemfillgrd()
        ASPxGrvHead.DataSource = dtSistem
        ASPxGrvHead.DataBind()

        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            '  Session("UserId") = 1
            ' meno_acc()
            load_worker()
            load_work_type()
            load_checkbox()
            fill_date_time()
            CmbWorkerCodeP.SelectedValue = Session("WorkerCode")
            CmbWorkerCodeS.SelectedValue = Session("WorkerCode")
            CmbWorkeType.SelectedValue = Session("WorkType")
        End If
        'load_grid()
    End Sub

    Private Sub load_worker()
        Dim dt As New DataTable
        Dim cls_ As New ClsWorker
        dt = cls_.GetAllWorkerDDL
        CmbWorkerCodeP.DataSource = dt
        CmbWorkerCodeP.DataValueField = "WorkerCode"
        CmbWorkerCodeP.DataTextField = "names"
        CmbWorkerCodeP.DataBind()

        CmbWorkerCodeS.DataSource = dt
        CmbWorkerCodeS.DataValueField = "WorkerCode"
        CmbWorkerCodeS.DataTextField = "names"
        CmbWorkerCodeS.DataBind()

    End Sub
    Private Sub load_work_type()
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT     WorkTypeCode, WorkTypeName   FROM  tblWorkType"
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbWorkeType.DataSource = dt
        CmbWorkeType.DataValueField = "WorkTypeCode"
        CmbWorkeType.DataTextField = "WorkTypeName"
        CmbWorkeType.DataBind()
    End Sub

    Protected Sub ASPxGrvHead_RowCommand1(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ClsSystem
            Dim dt As New DataTable

            dt = ClsB.getsystemgrd(e.CommandArgs.CommandArgument.ToString())
            txt_SystemCode.Text = dt.Rows(0).Item("SystemCode").ToString()
            UpdatePanel2.Update()
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Private Sub load_checkbox()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = " SELECT     BaseWorkCode, BaseWorkName  FROM tblBaseWork WHERE     (CodeClosed = 0) AND (BaseWorkCode <> 0) AND (WorkTypeCode = " & CmbWorkeType.SelectedValue & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)
        CheckBoxList1.DataSource = dt
        CheckBoxList1.DataTextField = "BaseWorkName"
        CheckBoxList1.DataValueField = "BaseWorkCode"
        CheckBoxList1.DataBind()
        If CmbWorkeType.SelectedValue = 1 Then
            CheckBox1.Visible = True
            'Label2.Visible = True
            'lblNew.Visible = True
            chbox_dorei.Visible = True

        Else
            CheckBox1.Visible = False
            CheckBox1.Checked = False
            'Label2.Visible = False
            'lblNew.Visible = False
            chbox_dorei.Visible = False
        End If
    End Sub

    Private Sub fill_date_time()
        txt_WorkDateS.Text = ClsDateTime.GetDateNow
        txt_WorkDateE.Text = ClsDateTime.GetDateNow
        txt_WorkTimeE.Text = ClsDateTime.GetTimeNow
        txt_WorkTimeS.Text = ClsDateTime.GetTimeNow
    End Sub

#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmWorkHldr.aspx", Val(Session("UserId")))
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
            '  ASPxGrvHead.Enabled = False
            ' txt_SystemCode.Enabled = True
            txt_WorkDateE.Enabled = True
            txt_WorkDateS.Enabled = True
            txt_WorkTimeE.Enabled = True
            txt_WorkTimeS.Enabled = True
            txt_WorkTozih.Enabled = True
            txtWorkDalil.Enabled = True
            txt_SystemCode.Enabled = True
            CmbWorkerCodeP.Enabled = True
            CmbWorkerCodeS.Enabled = True
            CmbWorkeType.Enabled = True
            CheckBoxList1.Enabled = True

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
            ASPxGrvHead.Enabled = True
            txt_SystemCode.Enabled = False
            txt_WorkDateE.Enabled = False
            txt_WorkDateS.Enabled = False
            txt_WorkTimeE.Enabled = False
            txt_WorkTimeS.Enabled = False
            txt_WorkTozih.Enabled = False
            txtWorkDalil.Enabled = False
            CmbWorkerCodeP.Enabled = False
            txt_SystemCode.Enabled = False
            CmbWorkerCodeS.Enabled = False
            CmbWorkeType.Enabled = False
            CheckBoxList1.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        txt_SystemCode.Text = ""
        txt_WorkDateE.Text = ""
        txt_WorkDateS.Text = ""
        txt_WorkTimeE.Text = ""
        txt_WorkTimeS.Text = ""
        txt_WorkTozih.Text = ""
        txtWorkDalil.Text = ""
        CmbWorkerCodeP.SelectedValue = Session("WorkerCode")
        CmbWorkerCodeS.SelectedValue = Session("WorkerCode")
        CmbWorkeType.SelectedValue = Session("WorkType")
        hdn_tmp.Value = ""
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


#Region "meno button"
    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSave.Click
        Call save_final()
        fill_date_time()
    End Sub

    Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
        fill_date_time()

    End Sub

    Private Sub New_()
        hdn_Final_key.Value = "new"
        clear_form()
        ' txt_UnitsCode.Text = ClsGetMaxId.GetMaxId("tblUnits", "UnitsCode")
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
        fill_date_time()
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

    Protected Sub ibtnDeactived_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDeactived.Click
        Call Deactived_()
    End Sub
    Private Sub Deactived_()
        If hdn_deactivedcode.Value = "false" Then
            hdn_deactivedcode.Value = "true"
        Else
            hdn_deactivedcode.Value = "false"
        End If
        'load_grid()
    End Sub
#End Region

    Protected Sub CmbWorkeType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbWorkeType.SelectedIndexChanged
        load_checkbox()
        UpdatePanel6.Update()
        If CmbWorkeType.SelectedValue <> 1 Then
            Label2.Visible = False
            lblNew.Visible = False
            Cmbg.Visible = False
            Cmbgd.Visible = False
            Cmbsystemg.Visible = False
            btnIn.Visible = False
            btnOut.Visible = False
            GridView2.Visible = False
        End If
    End Sub

    Private Sub save_final()
        '  If hdn_Final_key.Value = "new" Then txt_BakhshCode.Text = ClsGetMaxId.GetMaxId("tblBakhsh", "BakhshCode")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcWork"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@WorkId", SqlDbType.Int, 10)
            cmm.Parameters("@WorkId").Value = 0

            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

            cmm.Parameters.Add("@WorkerCodeP", SqlDbType.Int, 10)
            cmm.Parameters("@WorkerCodeP").Value = CmbWorkerCodeP.SelectedValue

            cmm.Parameters.Add("@WorkerCodeS", SqlDbType.Int, 10)
            cmm.Parameters("@WorkerCodeS").Value = CmbWorkerCodeS.SelectedValue

            cmm.Parameters.Add("@WorkTimeS", SqlDbType.VarChar, 5)
            cmm.Parameters("@WorkTimeS").Value = txt_WorkTimeS.Text

            cmm.Parameters.Add("@WorkTimeE", SqlDbType.VarChar, 5)
            cmm.Parameters("@WorkTimeE").Value = txt_WorkTimeE.Text

            cmm.Parameters.Add("@WorkDateE", SqlDbType.VarChar, 10)
            cmm.Parameters("@WorkDateE").Value = txt_WorkDateE.Text

            cmm.Parameters.Add("@WorkDateS", SqlDbType.VarChar, 10)
            cmm.Parameters("@WorkDateS").Value = txt_WorkDateS.Text

            cmm.Parameters.Add("@TypeOpCode", SqlDbType.SmallInt, 6)
9:          cmm.Parameters("@TypeOpCode").Value = 0

            cmm.Parameters.Add("@WorkTypeCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@WorkTypeCode").Value = CmbWorkeType.SelectedValue

            cmm.Parameters.Add("@WorkNote", SqlDbType.VarChar, 10)
            cmm.Parameters("@WorkNote").Value = txt_WorkTozih.Text

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim j As Integer
            Dim txt As String = ""
            For j = 0 To CheckBoxList1.Items.Count - 1
                If CheckBoxList1.Items(j).Selected = True Then
                    txt = txt & CheckBoxList1.Items(j).Value & "#"
                End If
                   
            Next
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@BaseWorkCode", SqlDbType.VarChar, 2000)
            cmm.Parameters("@BaseWorkCode").Value = txt

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
            If hdn_Final_key.Value = "del" Then
                cmm.Parameters("@type_").Value = 3
            ElseIf hdn_Final_key.Value = "edit" Then
                cmm.Parameters("@type_").Value = 2
            ElseIf hdn_Final_key.Value = "new" Then
                cmm.Parameters("@type_").Value = 1
            End If
            cmm.Parameters.Add("@has_g", SqlDbType.Int, 10)
            If CheckBox1.Checked = True Then
                cmm.Parameters("@has_g").Value = hdn_tmp.Value
            Else
                cmm.Parameters("@has_g").Value = 0
            End If
            cmm.Parameters.Add("@has_servis", SqlDbType.Bit, 1)
            If chbox_dorei.Checked = True Then
                cmm.Parameters("@has_servis").Value = True
            Else
                cmm.Parameters("@has_servis").Value = False
            End If
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
                Call save_()
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Cmbsystemg.Visible = True
            lblNew.Visible = True
            Label2.Visible = True
            btnIn.Visible = True
            btnOut.Visible = True

            Cmbg.Visible = True
            Cmbgd.Visible = True

            load_gataat()

        Else
            Cmbsystemg.Visible = False
            Cmbg.Visible = False
            Cmbgd.Visible = False
            Label2.Visible = False
            lblNew.Visible = False
            btnIn.Visible = False
            btnOut.Visible = False
            GridView2.Visible = False
        End If
    End Sub
    Sub load_gataat()
        If txt_SystemCode.Text = "" Then
            msg_box("لطفا کد سیستم را وارد کنید")
        Else
            Dim dtg, dtm, dtmg As New DataTable
            Dim sqlstr As String = " SELECT     tblSystemHDtl.id_rec, tblBaseSystemHDtl.BaseSystemHDtlNmae + ' ' + tblBaseModel.BaseModelName AS sname " & _
" FROM         tblSystemHDtl INNER JOIN  " & _
   "                   tblBaseModel ON tblSystemHDtl.BaseModelCode = tblBaseModel.BaseModelCode INNER JOIN " & _
  "                    tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
" WHERE     (tblSystemHDtl.SystemCode = " & Val(txt_SystemCode.Text) & ") AND (tblSystemHDtl.SystemHDtlStatus = 0)"
            dtg = ClsDataTable.GetDataTable(sqlstr)
            Cmbsystemg.DataSource = dtg
            Cmbsystemg.DataTextField = "sname"
            Cmbsystemg.DataValueField = "id_rec"
            Cmbsystemg.DataBind()
            sqlstr = "SELECT BaseSystemHDtlNmae, BaseSystemHDtlCode FROM tblBaseSystemHDtl WHERE     (CodeClosed = 0)"
            dtm = ClsDataTable.GetDataTable(sqlstr)
            Cmbg.DataSource = dtm
            Cmbg.DataTextField = "BaseSystemHDtlNmae"
            Cmbg.DataValueField = "BaseSystemHDtlCode"
            Cmbg.DataBind()

        End If
    End Sub

    Protected Sub Cmbg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmbg.SelectedIndexChanged
        Dim dtmg As New DataTable
        Dim sqlstr = "SELECT     tblBaseModel.BaseModelName, tblBaseModel.BaseModelCode " & _
" FROM         tblBaseModel INNER JOIN " & _
"                    tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
" WHERE     (tblBaseModel.CodeClosed = 0) AND (tblBaseModel.BaseSystemHDtlCode = " & Cmbg.SelectedValue & ")"
        dtmg = ClsDataTable.GetDataTable(sqlstr)
        Cmbgd.DataSource = dtmg
        Cmbgd.DataTextField = "BaseModelName"
        Cmbgd.DataValueField = "BaseModelCode"
        Cmbgd.DataBind()
    End Sub

    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)
        Dim note As String
        Dim basesys As Int64
        Dim baseMod As Int64
        '  Dim id_rec As Int64


        If INOUT = False Then
            note = Cmbsystemg.SelectedItem.Text
            basesys = 0
            baseMod = 0
        Else
            note = ""
            basesys = Val(Cmbg.SelectedValue)
            baseMod = Val(Cmbgd.SelectedValue)
        End If


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTmpInOutGate"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@TmpInOutId", SqlDbType.Int, 10)
            cmm.Parameters("@TmpInOutId").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@id_rec", SqlDbType.Int, 10)
            cmm.Parameters("@id_rec").Value = Val(Cmbsystemg.SelectedValue)

            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

            cmm.Parameters.Add("@BaseSystemHDtlCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@BaseSystemHDtlCode").Value = basesys

            cmm.Parameters.Add("@BaseModelCode", SqlDbType.Int, 10)
            cmm.Parameters("@BaseModelCode").Value = baseMod

            cmm.Parameters.Add("@id_rec_note", SqlDbType.NVarChar, 200)
            cmm.Parameters("@id_rec_note").Value = note

            cmm.Parameters.Add("@UserCode", SqlDbType.Int, 10)
            cmm.Parameters("@UserCode").Value = 1

            cmm.Parameters.Add("@INOUT", SqlDbType.Bit, 1)
            cmm.Parameters("@INOUT").Value = INOUT


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

    Protected Sub btnOut_Click(sender As Object, e As System.EventArgs) Handles btnOut.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTmpInOutGate", "TmpInOutId")
        End If
        save_tmp(False, False)
        GridView2.Visible = True
        load_grid_tmp()
    End Sub


    Private Sub load_grid_tmp()
        Dim sqlstr As String = "SELECT     Tmp_id,tblTmpInOutGate.Tmp_id, tblTmpInOutGate.TmpInOutId, tblTmpInOutGate.id_rec, tblTmpInOutGate.SystemCode, tblTmpInOutGate.BaseSystemHDtlCode, " & _
        " tblTmpInOutGate.BaseModelCode, tblTmpInOutGate.UserCode, tblTmpInOutGate.INOUT, tblTmpInOutGate.id_rec_note, tblTmpInOutType.INOUTNOTE, " & _
        " tblBaseSystemHDtl.BaseSystemHDtlNmae, tblBaseModel.BaseModelName " & _
" FROM         tblTmpInOutGate INNER JOIN " & _
                      " tblTmpInOutType ON tblTmpInOutGate.INOUT = tblTmpInOutType.INOUT INNER JOIN " & _
                      " tblBaseModel ON tblTmpInOutGate.BaseModelCode = tblBaseModel.BaseModelCode AND  " & _
                      " tblTmpInOutGate.BaseSystemHDtlCode = tblBaseModel.BaseSystemHDtlCode INNER JOIN " & _
                      " tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
" WHERE     (tblTmpInOutGate.UserCode = " & Session("UserId") & ") AND (tblTmpInOutGate.TmpInOutId = " & Val(hdn_tmp.Value) & ") "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub

    Protected Sub btnIn_Click(sender As Object, e As System.EventArgs) Handles btnIn.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTmpInOutGate", "TmpInOutId")
        End If
        save_tmp(True, False)
        GridView2.Visible = True
        load_grid_tmp()
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from tblTmpInOutGate where Tmp_id=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
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

  
   
End Class
