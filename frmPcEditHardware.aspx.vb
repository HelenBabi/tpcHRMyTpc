Imports System.Data
Imports System.Data.SqlClient

Partial Class frmPcEditHardware
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


        End If

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmPcEditHardware.aspx", Val(Session("UserId")))
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
          

            ' txt_BakhshCode.Enabled = True
        End If

    End Sub

    Private Sub clear_form()
        txt_SystemCode.Text = ""
        txt_bulding.Text = ""
        txt_ComputerName.Text = ""
        txt_holder.Text = ""
        txt_SystemPhone.Text = ""
        txt_SystemRoom.Text = ""
        txt_SystemRoomNum.Text = ""
        txt_SystemTabage.Text = ""
        txt_Systemunit.Text = ""
        Cmbsystemg.DataBind()
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
        '  If hdn_Final_key.Value = "new" Then txt_BakhshCode.Text = ClsGetMaxId.GetMaxId("tblBakhsh", "BakhshCode")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcEditPcHardware"
            cmm.CommandType = CommandType.StoredProcedure

           
            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

          

            cmm.Parameters.Add("@TypeOpCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@TypeOpCode").Value = 0

           
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
         
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")

         
            cmm.Parameters.Add("@has_g", SqlDbType.Int, 10)
            cmm.Parameters("@has_g").Value = hdn_tmp.Value
            

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

    Private Sub find_pc()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(Val(txt_SystemCode.Text))
        If dt.Rows.Count > 0 Then

            txt_holder.Text = dt.Rows(0).Item("HolderFamili").ToString
            txt_bulding.Text = dt.Rows(0).Item("BuildingName").ToString
            txt_SystemTabage.Text = dt.Rows(0).Item("SystemTabage").ToString
            txt_Systemunit.Text = dt.Rows(0).Item("UnitsName").ToString
            txt_SystemPhone.Text = dt.Rows(0).Item("SystemPhone").ToString
            txt_SystemRoom.Text = dt.Rows(0).Item("SystemRoom").ToString
            txt_SystemRoomNum.Text = dt.Rows(0).Item("SystemRoomNum").ToString
            txt_ComputerName.Text = dt.Rows(0).Item("ComputerName").ToString
           
            ibtnSave.Enabled = True
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True

            Cmbg.Visible = True
            Cmbgd.Visible = True
            btnIn.Visible = True
            btnOut.Visible = True
            GridView2.Visible = True
            Label2.Visible = True
            Label3.Visible = True
            Cmbsystemg.Visible = True

            txt_AmvalTmp.Visible = True
            txt_Garanty.Visible = True
            txt_Mac.Visible = True
            txt_Serial.Visible = True
            txt_SystemTP.Visible = True

            lblAmvalGate.Visible = True
            lblGaranti.Visible = True
            lblMac.Visible = True
            lblSerialGate.Visible = True
            lblTP.Visible = True

            load_gataat()

            If dt.Rows(0).Item("CodeClosed").ToString = True Then
                msg_box("اين سيستم رده خارج شده است")
                ibtnSave.Enabled = False
                ibtnSaveNew.Enabled = False
                ibtnCancel.Enabled = False
             
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
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False
        End If

    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        find_pc()

    End Sub
  
    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)
        Dim note As String
        Dim basesys As Int64
        Dim baseMod As Int64
        ' Dim id_rec As Int64


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

            cmm.Parameters.Add("@gateAmval", SqlDbType.Int, 10)
            cmm.Parameters("@gateAmval").Value = Val(txt_AmvalTmp.Text)

            cmm.Parameters.Add("@gateSerial", SqlDbType.VarChar, 50)
            cmm.Parameters("@gateSerial").Value = txt_Serial.Text

            cmm.Parameters.Add("@gateMac", SqlDbType.VarChar, 50)
            cmm.Parameters("@gateMac").Value = txt_Mac.Text

            cmm.Parameters.Add("@gateTp", SqlDbType.VarChar, 50)
            cmm.Parameters("@gateTp").Value = txt_SystemTP.Text


            cmm.Parameters.Add("@gateGranty", SqlDbType.VarChar, 50)
            cmm.Parameters("@gateGranty").Value = txt_Garanty.Text

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
            cmm.Parameters("@UserCode").Value = Session("UserId")

            cmm.Parameters.Add("@INOUT", SqlDbType.Bit, 1)
            cmm.Parameters("@INOUT").Value = INOUT


            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  عملیات با موفقیت انجام شد ")
                '  Call save_()
                ' load_grid()
                txt_AmvalTmp.Text = ""
                txt_Garanty.Text = ""
                txt_Mac.Text = ""
                txt_Serial.Text = ""
                txt_SystemTP.Text = ""
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
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
    Sub load_gataat()
        If txt_SystemCode.Text = "" Then
            msg_box("لطفا کد سیستم را وارد کنید")
        Else
            Dim dtg, dtm, dtmg As New DataTable
            Dim sqlstr As String = " SELECT     tblSystemHDtl.id_rec, tblBaseSystemHDtl.BaseSystemHDtlNmae + ' ' + tblBaseModel.BaseModelName AS sname " & _
" FROM         tblSystemHDtl INNER JOIN  " & _
   "                   tblBaseModel ON tblSystemHDtl.BaseModelCode = tblBaseModel.BaseModelCode INNER JOIN " & _
  "                    tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
" WHERE     (tblSystemHDtl.SystemCode = " & Val(txt_SystemCode.Text) & ") "
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

    Protected Sub btnIn_Click(sender As Object, e As System.EventArgs) Handles btnIn.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTmpInOutGate", "TmpInOutId")
        End If
        save_tmp(True, False)
        GridView2.Visible = True
        load_grid_tmp()
    End Sub

    Protected Sub ibtnDeactived_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnDeactived.Click

    End Sub

    Protected Sub ibtnAdd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
    End Sub
End Class
