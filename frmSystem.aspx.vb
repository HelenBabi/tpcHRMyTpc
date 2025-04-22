Imports System.Data
Imports System.Data.SqlClient

Partial Class frmSystem
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            '  Session("UserId") = 1

            ' meno_acc()
        End If
        If Me.IsPostBack = False Then
            load_cmb()
            load_holder()
            load_Bulding()
            load_Units()
            load_systemType()

        End If

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("FrmSystem.aspx", Val(Session("UserId")))
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
            txt_SystemCode.Enabled = True
            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            ibtnDeleted.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False
            ibtnExit.Enabled = False
            GridView2.DataBind()
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

            cmbBulding.Enabled = True
            cmbHolder.Enabled = True
            ' txt_SystemCode.Enabled = True
            txt_SystemPhone.Enabled = True
            txt_SystemRoom.Enabled = True
            txt_SystemTP.Enabled = True
            cmbUnit.Enabled = True
            txt_SystemRoomNum.Enabled = True
            txt_SystemTabage.Enabled = True
            Button1.Enabled = True
            txt_SystemRoomNum.Enabled = True

            Cmbg.Enabled = True
            Cmbgd.Enabled = True
            txt_AmvalTmp.Enabled = True
            txt_Serial.Enabled = True
            txt_Mac.Enabled = True
            txt_SystemTP.Enabled = True
            txt_Garanty.Enabled = True
            btnIn.Enabled = True
            txt_SystemTPP.Enabled = True
            txt_IP.Enabled = True
            chkAntivirus.Enabled = True
            chkjoinDomain.Enabled = True
            chkPXE.Enabled = True
            txtNote.Enabled = True
            cmbSystemTyp.Enabled = True
        Else
            cmbSystemTyp.Enabled = False
            GridView2.DataBind()
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

            cmbBulding.Enabled = False
            cmbHolder.Enabled = False
            'txt_SystemCode.Enabled = False
            txt_SystemPhone.Enabled = False
            txt_SystemRoom.Enabled = False
            txt_SystemTP.Enabled = False
            cmbUnit.Enabled = False
            txt_SystemTabage.Enabled = False
            Button1.Enabled = False
            txt_SystemRoomNum.Enabled = False
            txt_SystemCode.Enabled = False

            Cmbg.Enabled = False
            Cmbgd.Enabled = False
            txt_AmvalTmp.Enabled = False
            txt_Serial.Enabled = False
            txt_Mac.Enabled = False
            txt_SystemTP.Enabled = False
            txt_Garanty.Enabled = False
            btnIn.Enabled = False
            txt_SystemTPP.Enabled = False
            txt_IP.Enabled = False
            chkAntivirus.Enabled = False
            chkjoinDomain.Enabled = False
            chkPXE.Enabled = False
            TxtNote.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        txt_SystemCode.Text = ""
        cmbBulding.SelectedValue = 0
        txt_SystemTabage.Text = ""
        cmbUnit.SelectedValue = 0

        cmbHolder.SelectedValue = 0
        'txt_SystemCode.Enabled = False
        txt_SystemPhone.Text = ""
        txt_SystemRoom.Text = ""
        txt_SystemTP.Text = ""
        cmbUnit.SelectedValue = 0
        txt_SystemTabage.Text = ""
        txt_SystemRoomNum.Text = ""
        txt_ComputerName.Text = ""

        txt_AmvalTmp.Text = ""
        txt_Serial.Text = ""
        txt_Mac.Text = ""
        txt_SystemTPP.Text = ""
        txt_Garanty.Text = ""
        TxtNote.Text = ""

        chkAntivirus.Checked = False
        chkjoinDomain.Checked = False
        chkPXE.Checked = False


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
        sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed, opid, ArcTypeCode  FROM tblBaseSystemHDtl WHERE     (CodeClosed = 0) AND (BaseSystemHDtlCode <> 0)"
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
        Call chek_systemCode()
        chkAntivirus.Checked = False
        chkjoinDomain.Checked = False
        chkPXE.Checked = False
    End Sub
    Private Sub chek_systemCode()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(Val(txt_SystemCode.Text))
        If dt.Rows.Count > 0 Then
            msg_box(" بااين شماره قبلا سيستم تعريف شده است")
            Exit Sub
        Else
            Call save_final()
            
        End If

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


    Private Sub load_grid()
        Dim ClsB_ As New ClsSystem
        Dim dt As New DataTable
        'dt = ClsB_.GetAllSystem(hdn_deactivedcode.Value)
        
    End Sub



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
        gen_comName()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        
        cmm.CommandText = "prcNewSystem"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@tozihat", SqlDbType.NVarChar, 200)
        cmm.Parameters("@tozihat").Value = txtNote.Text

        cmm.Parameters.Add("@upAntivirus", SqlDbType.Bit, 1)
        cmm.Parameters("@upAntivirus").Value = chkAntivirus.Checked

        cmm.Parameters.Add("@joinDoamin", SqlDbType.Bit, 1)
        cmm.Parameters("@joinDoamin").Value = chkjoinDomain.Checked

        cmm.Parameters.Add("@pxe", SqlDbType.Bit, 1)
        cmm.Parameters("@pxe").Value = chkPXE.Checked

        cmm.Parameters.Add("@ComputerName", SqlDbType.VarChar, 20)
        cmm.Parameters("@ComputerName").Value = txt_ComputerName.Text

        cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 6)
        cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

        cmm.Parameters.Add("@SystemType", SqlDbType.Int, 6)
        cmm.Parameters("@SystemType").Value = cmbSystemTyp.SelectedValue

        'cmm.Parameters.Add("@SystemTP", SqlDbType.Int, 10)
        'cmm.Parameters("@SystemTP").Value = 0

        cmm.Parameters.Add("@BuildingCode", SqlDbType.Int, 6)
        cmm.Parameters("@BuildingCode").Value = Val(cmbBulding.SelectedValue)

        cmm.Parameters.Add("@UnitsCode", SqlDbType.Int, 6)
        cmm.Parameters("@UnitsCode").Value = Val(cmbUnit.SelectedValue)

        cmm.Parameters.Add("@tabage", SqlDbType.Int, 6)
        cmm.Parameters("@tabage").Value = Val(txt_SystemTabage.Text)

        cmm.Parameters.Add("@SystemRoom", SqlDbType.NVarChar, 50)
        cmm.Parameters("@SystemRoom").Value = txt_SystemRoom.Text

        cmm.Parameters.Add("@SystemRoomNum", SqlDbType.Int, 6)
        cmm.Parameters("@SystemRoomNum").Value = Val(txt_SystemRoomNum.Text)

        cmm.Parameters.Add("@HolderCode", SqlDbType.Int, 6)
        cmm.Parameters("@HolderCode").Value = Val(cmbHolder.SelectedValue)

        cmm.Parameters.Add("@SystemPhone", SqlDbType.VarChar, 20)
        cmm.Parameters("@SystemPhone").Value = txt_SystemPhone.Text

        cmm.Parameters.Add("@SystemTPP", SqlDbType.VarChar, 50)
        cmm.Parameters("@SystemTPP").Value = txt_SystemTPP.Text

        cmm.Parameters.Add("@TmpInOutId", SqlDbType.Int, 10)
        cmm.Parameters("@TmpInOutId").Value = Val(hdn_tmp.Value)
  


        cmm.Parameters.Add("@has_g", SqlDbType.Int, 10)

        cmm.Parameters("@has_g").Value = hdn_tmp.Value

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
            msg_box("Computer:  " & txt_ComputerName.Text & " Succecfull ")
              Call save_()
        Else
            msg_box(cmm.Parameters("@pm").Value)
        End If

    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from tblTmpGateSystem where Tmp_id=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
    End Sub
    Private Sub load_holder()
        Dim dt As New DataTable
        Dim cls_ As New ClsHolder
        dt = cls_.GetAllHolderDDL
        cmbHolder.DataSource = dt
        cmbHolder.DataValueField = "HolderCode"
        cmbHolder.DataTextField = "names"
        cmbHolder.DataBind()

    End Sub
    Private Sub load_Bulding()
        Dim dt As New DataTable
        Dim cls_ As New ClsBuilding
        dt = cls_.GetAllBuildingDDL
        cmbBulding.DataSource = dt
        cmbBulding.DataValueField = "BuildingCode"
        cmbBulding.DataTextField = "BuildingName"
        cmbBulding.DataBind()

    End Sub
    Private Sub load_Units()
        Dim dt As New DataTable
        Dim cls_ As New ClsUnits
        dt = cls_.GetAllUnitsDDL
        cmbUnit.DataSource = dt
        cmbUnit.DataValueField = "UnitsCode"
        cmbUnit.DataTextField = "UnitsName"
        cmbUnit.DataBind()

    End Sub

    Private Sub load_systemType()
        Dim dt As New DataTable
        Dim cls_ As New ClsSystemType
        dt = cls_.GetAllSystemTypeDDL
        cmbSystemTyp.DataSource = dt
        cmbSystemTyp.DataValueField = "SystemTypCode"
        cmbSystemTyp.DataTextField = "SystemName"
        cmbSystemTyp.DataBind()

    End Sub
    Protected Sub btnIn_Click(sender As Object, e As System.EventArgs) Handles btnIn.Click
      
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTmpGateSystem", "TmpInOutId")
        End If
        save_tmp(True, False)
        GridView2.Visible = True
        load_grid_tmp()

        txt_AmvalTmp.Text = ""
        txt_Serial.Text = ""
        txt_Garanty.Text = ""
        txt_IP.Text = ""
        txt_Mac.Text = ""
        txt_SystemTP.Text = ""

    End Sub


    Private Sub load_grid_tmp()
        Dim sqlstr As String = "SELECT    IPGate,TPGate,GarantyDate,Serial,Tmp_id,tblTmpGateSystem.Tmp_id, tblTmpGateSystem.TmpInOutId, tblTmpGateSystem.id_rec, tblTmpGateSystem.SystemCode, tblTmpGateSystem.BaseSystemHDtlCode, " & _
      " tblTmpGateSystem.BaseModelCode,tblTmpGateSystem.macGate, tblTmpGateSystem.UserCode, tblTmpGateSystem.INOUT, tblTmpGateSystem.id_rec_note, tblTmpInOutType.INOUTNOTE, " & _
      " tblBaseSystemHDtl.BaseSystemHDtlNmae, tblBaseModel.BaseModelName " & _
" FROM         tblTmpGateSystem INNER JOIN " & _
                    " tblTmpInOutType ON tblTmpGateSystem.INOUT = tblTmpInOutType.INOUT INNER JOIN " & _
                    " tblBaseModel ON tblTmpGateSystem.BaseModelCode = tblBaseModel.BaseModelCode AND  " & _
                    " tblTmpGateSystem.BaseSystemHDtlCode = tblBaseModel.BaseSystemHDtlCode INNER JOIN " & _
                    " tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
" WHERE     (tblTmpGateSystem.UserCode = " & Session("UserId") & ") AND (tblTmpGateSystem.TmpInOutId = " & Val(hdn_tmp.Value) & ") "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub

    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)
        Dim note As String
        Dim basesys As Int64
        Dim baseMod As Int64
        ' Dim id_rec As Int64


        note = Val(txt_AmvalTmp.Text)
            basesys = Val(Cmbg.SelectedValue)
            baseMod = Val(Cmbgd.SelectedValue)


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTmpGateSystem"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@MacAdres", SqlDbType.VarChar, 50)
            cmm.Parameters("@MacAdres").Value = txt_Mac.Text

            cmm.Parameters.Add("@ipGate", SqlDbType.VarChar, 50)
            cmm.Parameters("@ipGate").Value = txt_IP.Text

            cmm.Parameters.Add("@SystemHDtlSerial", SqlDbType.VarChar, 15)
            cmm.Parameters("@SystemHDtlSerial").Value = txt_Serial.Text

            cmm.Parameters.Add("@SystemHDtlTP", SqlDbType.VarChar, 15)
            cmm.Parameters("@SystemHDtlTP").Value = txt_SystemTP.Text

            'cmm.Parameters.Add("@AmvalCode", SqlDbType.Int, 15)
            'cmm.Parameters("@AmvalCode").Value = Val(txt_AmvalTmp.Text)

            cmm.Parameters.Add("@SystemHDtlGarantyDate", SqlDbType.VarChar, 15)
            cmm.Parameters("@SystemHDtlGarantyDate").Value = txt_Garanty.Text

            cmm.Parameters.Add("@TmpInOutId", SqlDbType.Int, 10)
            cmm.Parameters("@TmpInOutId").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@id_rec", SqlDbType.Int, 10)
            cmm.Parameters("@id_rec").Value = 0

            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

            cmm.Parameters.Add("@BaseSystemHDtlCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@BaseSystemHDtlCode").Value = basesys

            cmm.Parameters.Add("@BaseModelCode", SqlDbType.Int, 10)
            cmm.Parameters("@BaseModelCode").Value = baseMod

            cmm.Parameters.Add("@id_rec_note", SqlDbType.Int, 6)
            cmm.Parameters("@id_rec_note").Value = note

            cmm.Parameters.Add("@UserCode", SqlDbType.Int, 10)
            cmm.Parameters("@UserCode").Value = Session("UserId")

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
    Protected Sub Cmbg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmbg.SelectedIndexChanged
        Dim dtmg As New DataTable
        Dim sqlstr = "SELECT     tblBaseModel.BaseModelName, tblBaseModel.BaseModelCode " & _
" FROM         tblBaseModel INNER JOIN " & _
"                    tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
" WHERE     (tblBaseModel.CodeClosed = 0) AND (tblBaseModel.BaseSystemHDtlCode = " & Cmbg.SelectedValue & ") order by BaseModelName"
        dtmg = ClsDataTable.GetDataTable(sqlstr)
        Cmbgd.DataSource = dtmg
        Cmbgd.DataTextField = "BaseModelName"
        Cmbgd.DataValueField = "BaseModelCode"
        Cmbgd.DataBind()

    End Sub

    Private Sub load_cmb()
        Dim dtg, dtm, dtmg As New DataTable
        Dim sqlstr As String 
        sqlstr = "SELECT BaseSystemHDtlNmae, BaseSystemHDtlCode FROM tblBaseSystemHDtl WHERE     (CodeClosed = 0) order by BaseSystemHDtlNmae"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        Cmbg.DataSource = dtm
        Cmbg.DataTextField = "BaseSystemHDtlNmae"
        Cmbg.DataValueField = "BaseSystemHDtlCode"
        Cmbg.DataBind()

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

    

    Protected Sub gen_comName()
        Dim comname As String
        Dim dt As New DataTable
        Dim clsB_ As New ClsBuilding
        dt = clsB_.GetBuilding(cmbBulding.SelectedValue, True)

        comname = txt_SystemCode.Text & dt.Rows(0).Item("BuildingEngName") & "F" & txt_SystemTabage.Text & "-" & txt_SystemRoomNum.Text
        txt_ComputerName.Text = comname
    End Sub

    
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        gen_comName()
    End Sub

    
    Protected Sub ibtnAdd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
    End Sub

    
End Class
