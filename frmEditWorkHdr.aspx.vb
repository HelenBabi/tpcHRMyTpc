Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class frmEditWorkHdr
    Inherits System.Web.UI.Page
    Dim dtSistem As New DataTable
    Dim dt2, dt1, dt3 As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim clssistem_ As New ClsSystem
        'dtSistem = clssistem_.getallsystemfillgrd()
        'ASPxGrvHead.DataSource = dtSistem
        'ASPxGrvHead.DataBind()

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
            'enable_disable()

        End If

        load_grid()
    End Sub
     

    Private Sub load_grid()
        Dim ClsB_ As New ClsBrowsWorks
        Dim az, ta As String
        az = ClsDateTime.GetDateNow
        ta = ClsDateTime.GetDateNow
        'Dim dt1 As New DataTable
        dt1 = ClsB_.GetAllBrowsWorks(az, ta)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()

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

    'Protected Sub ASPxGrvHead_RowCommand1(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
    '    If e.CommandArgs.CommandName = "select" Then
    '        Dim ClsB As New ClsSystem
    '        Dim dt As New DataTable

    '        dt = ClsB.getsystemgrd(e.CommandArgs.CommandArgument.ToString())
    '        txt_SystemCode.Text = dt.Rows(0).Item("SystemCode").ToString()
    '        UpdatePanel2.Update()
    '        ModalPopupExtender1.Hide()
    '    End If
    'End Sub
    Private Sub load_checkbox()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = " SELECT     BaseWorkCode, BaseWorkName  FROM tblBaseWork WHERE     (CodeClosed = 0) AND (BaseWorkCode <> 0) AND (WorkTypeCode = " & CmbWorkeType.SelectedValue & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)
        CheckBox1.DataSource = dt
        CheckBox1.DataTextField = "BaseWorkName"
        CheckBox1.DataValueField = "BaseWorkCode"
        CheckBox1.DataBind()
        'If CmbWorkeType.SelectedValue = 1 Then
        '    CheckBox1.Visible = True
        '    'Label2.Visible = True
        '    'lblNew.Visible = True
        '    chbox_dorei.Visible = True

        'Else
        '    CheckBox1.Visible = False
        '    CheckBox1.Checked = False
        '    'Label2.Visible = False
        '    'lblNew.Visible = False
        '    chbox_dorei.Visible = False
        'End If
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
        meno_arr = ClsAccounting.meno_acc("frmEditWorkHdr.aspx", Val(Session("UserId")))
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
            CheckBox1.Enabled = True
            txt_WorkTozih.Enabled = True
            '  chbox_dorei.Enabled = True
            CheckBox1.Enabled = True
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
            CheckBox1.Enabled = False
            txt_WorkTozih.Enabled = False
            ' chbox_dorei.Enabled = False
            CheckBox1.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        txt_SystemCode.Text = ""
        ' txt_WorkDateE.Text = ""
        ' txt_WorkDateS.Text = ""
        ' txt_WorkTimeE.Text = ""
        '  txt_WorkTimeS.Text = ""
        txt_WorkTozih.Text = ""
        txtWorkDalil.Text = ""
        CmbWorkerCodeP.SelectedValue = Session("WorkerCode")
        CmbWorkerCodeS.SelectedValue = Session("WorkerCode")
        CmbWorkeType.SelectedValue = 0
        hdn_tmp.Value = ""
    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

#End Region


#Region "meno button"
    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSave.Click
        chek_systemCode()


        'fill_date_time()
    End Sub
    Private Sub chek_systemCode()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(Val(txt_SystemCode.Text))
        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("CodeClosed").ToString = True Then
                msg_box("اين سيستم رده خارج شده است")
                ibtnSave.Enabled = False
                ibtnAdd.Enabled = True
                Exit Sub
            End If
            Call save_final()
        Else
            msg_box("سيستمي بااين شماره اموال موجود نيست")
            Exit Sub
        End If

    End Sub
    Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
        'fill_date_time()
        Call load_checkbox()
    End Sub

    Private Sub New_()
        hdn_tmp.Value = "new"
        clear_form()
        ' txt_UnitsCode.Text = ClsGetMaxId.GetMaxId("tblUnits", "UnitsCode")
        enable_disable()
    End Sub
    Private Sub save_()
        enable_disable()
        clear_form()
        hdn_tmp.Value = 0
    End Sub

    Protected Sub ibtnSaveNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSaveNew.Click
        Call save_final()
        Call New_()
        'fill_date_time()
        Call load_checkbox()
    End Sub

    Protected Sub ibtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEdit.Click
        Call edit_()
    End Sub
    Private Sub edit_()
        enable_disable()
        hdn_tmp.Value = "edit"
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
        hdn_tmp.Value = "del"
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

    'Protected Sub CmbWorkeType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbWorkeType.SelectedIndexChanged
    '    load_checkbox()
    '    UpdatePanel5.Update()
    '    If CmbWorkeType.SelectedValue <> 1 Then
    '        Label2.Visible = False
    '        lblNew.Visible = False
    '        Cmbg.Visible = False
    '        Cmbgd.Visible = False
    '        Cmbsystemg.Visible = False
    '        btnIn.Visible = False
    '        btnOut.Visible = False
    '        GridView2.Visible = False
    '    End If
    'End Sub

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

            cmm.Parameters.Add("@WorkNote", SqlDbType.NVarChar, 250)
            cmm.Parameters("@WorkNote").Value = txt_WorkTozih.Text

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim j As Integer
            Dim txt As String = ""
            For j = 0 To CheckBox1.Items.Count - 1
                If CheckBox1.Items(j).Selected = True Then
                    txt = txt & CheckBox1.Items(j).Value & "#"
                End If

            Next
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@BaseWorkCode", SqlDbType.VarChar, 2000)
            cmm.Parameters("@BaseWorkCode").Value = txt

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
            If hdn_tmp.Value = "del" Then
                cmm.Parameters("@type_").Value = 3
            ElseIf hdn_tmp.Value = "edit" Then
                cmm.Parameters("@type_").Value = 2
            ElseIf hdn_tmp.Value = "new" Then
                cmm.Parameters("@type_").Value = 1
            End If
            cmm.Parameters.Add("@has_g", SqlDbType.Int, 10)
            'If CheckBox1.Checked = True Then
            '    cmm.Parameters("@has_g").Value = hdn_tmp.Value
            'Else
            '    cmm.Parameters("@has_g").Value = 0
            'End If
            'cmm.Parameters.Add("@has_servis", SqlDbType.Bit, 1)
            'If chbox_dorei.Checked = True Then
            '    cmm.Parameters("@has_servis").Value = True
            'Else
            '    cmm.Parameters("@has_servis").Value = False
            'End If
            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
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
            MsgBox(ex.Message.ToString)
        End Try


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




    Protected Sub hdn_tmp_ValueChanged(sender As Object, e As System.EventArgs) Handles hdn_tmp.ValueChanged

    End Sub
End Class
