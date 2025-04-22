Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics

Partial Class frmTicketWorkHldr
    Inherits System.Web.UI.Page
    Dim dtSistem, dtTicket As New DataTable
    Dim az As String = ClsDateTime.GetDateNow
    Dim ta As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        ' txt_SystemCode.Text = Session("systemcode")
        If Me.IsPostBack = False Then

            load_checkbox()
            'load_gataat()
            'load_cmbModel()
        End If

        loadDtlTicket()
        '  visiblee()
    End Sub




    Private Sub load_checkbox()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = " SELECT     BaseWorkCode, BaseWorkName  FROM tblBaseWork WHERE     (CodeClosed = 0) AND (BaseWorkCode <> 0) AND (WorkTypeCode = " & Val(Session("WorkType")) & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)
        CheckBoxList1.DataSource = dt
        CheckBoxList1.DataTextField = "BaseWorkName"
        CheckBoxList1.DataValueField = "BaseWorkCode"
        CheckBoxList1.DataBind()
        If Val(Session("WorkType")) = 1 Then
            CheckBox1.Visible = True
            'Label2.Visible = True
            'lblNew.Visible = True
            chbox_dorei.Visible = True

        Else
            CheckBox1.Visible = False
            ' CheckBox1.Checked = False
            'Label2.Visible = False
            'lblNew.Visible = False
            chbox_dorei.Visible = False
        End If
    End Sub



#Region "All pages"



    Private Sub enable_disable()
        If ibtnSave.Enabled = False Then
            ibtnSave.Enabled = True

            ibtnDeactived.Enabled = False

            ibtnExit.Enabled = False
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"

            ibtnDeactived.ImageUrl = "~/Icons/reload2.png"

            ''''''''''''''''''''''''''''''''''''''''''''''''
            '  ASPxGrvHead.Enabled = False
            ' txt_SystemCode.Enabled = True



            txt_SystemCode.Enabled = True

            CheckBoxList1.Enabled = True

            chbox_dorei.Enabled = True
            CheckBox1.Enabled = True

            txt_amvaltmp.Enabled = True
            txt_Garanty.Enabled = True
            txt_Mac.Enabled = True
            txt_Serial.Enabled = True
            txt_SystemTP.Enabled = True



        Else
            ibtnSave.Enabled = False

            ibtnDeactived.Enabled = True

            ibtnExit.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"

            ibtnDeactived.ImageUrl = "~/Icons/reload.png"

            ''''''''''''''''''''''''''''''''''''''''''''''''

            txt_SystemCode.Enabled = False




            txt_SystemCode.Enabled = False

            CheckBoxList1.Enabled = False

            chbox_dorei.Enabled = False
            CheckBox1.Enabled = False

            txt_amvaltmp.Enabled = False
            txt_Garanty.Enabled = False
            txt_Mac.Enabled = False
            txt_Serial.Enabled = False
            txt_SystemTP.Enabled = False

            CheckBox1.Visible = False
            Label2.Visible = False
            Cmbsystemg.Visible = False
            btnOut.Visible = False
            lblNew.Visible = False
            Cmbg.Visible = False
            Cmbgd.Visible = False
            btnIn.Visible = False
            txt_amvaltmp.Visible = False
            txt_Garanty.Visible = False
            txt_Mac.Visible = False
            txt_Serial.Visible = False
            txt_SystemTP.Visible = False
            GridView2.Visible = False
            lblAmvalGate.Visible = False
            lblGaranti.Visible = False
            lblMac.Visible = False
            lblSerialGate.Visible = False
            lblTP.Visible = False

        End If

    End Sub

    Private Sub clear_form()
        txt_amvaltmp.Text = ""
        txt_Garanty.Text = ""
        txt_Mac.Text = ""
        txt_Serial.Text = ""
        txt_SystemTP.Text = ""

        txt_SystemCode.Text = ""
        ' txt_WorkDateE.Text = ""
        ' txt_WorkDateS.Text = ""
        ' txt_WorkTimeE.Text = ""
        '  txt_WorkTimeS.Text = ""


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

        Call save_final()
        'fill_date_time()
    End Sub
    Private Sub loadDtlTicket()


        Dim ClsTicketWorker As New ClsTicket
        dtTicket = ClsTicketWorker.GetBrowsTicketWorker(Session("tiketId"))
        txt_SystemCode.Text = dtTicket.Rows(0).Item("SystemCode")

    End Sub


    Private Sub save_()
        enable_disable()
        clear_form()

    End Sub




    Private Sub cancel_()
        enable_disable()
        clear_form()
    End Sub



#End Region

    Protected Sub visiblee()
        'load_checkbox()
        ' UpdatePanel5.Update()
        If Session("workTyp") <> 1 Then
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


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketWork"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@WorkId", SqlDbType.Int, 10)
            cmm.Parameters("@WorkId").Value = 0


            Dim sqlstr As String
            Dim jj As Integer
            Dim dtt As DataTable
            Dim softcode As String = ""
            For jj = 0 To CheckBoxList1.Items.Count - 1
                If CheckBoxList1.Items(jj).Selected = True Then
                    ' txt = txt & CheckBoxList1.Items(j).Value & "#"
                    sqlstr = " SELECT     softCode, BaseWorkCode  FROM tblBaseWork WHERE    BaseWorkCode  = " & CheckBoxList1.Items(jj).Value & ""
                    dtt = ClsDataTable.GetDataTable(sqlstr)
                    If dtt.Rows(0).Item("softCode").ToString <> 0 Then
                        softcode = softcode & dtt.Rows(0).Item("softCode").ToString & "#"
                    End If

                End If

            Next
            'If softcode.Length > 0 Then
            '    softcode = softcode.Substring(0, softcode.Length - 1)
            'End If

            cmm.Parameters.Add("@softcode", SqlDbType.VarChar, 1000)
            cmm.Parameters("@softcode").Value = softcode


            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txt_SystemCode.Text

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = Session("tiketId")

            cmm.Parameters.Add("@WorkerCodeP", SqlDbType.Int, 10)
            cmm.Parameters("@WorkerCodeP").Value = Session("userid")

            cmm.Parameters.Add("@WorkerCodeS", SqlDbType.Int, 10)
            cmm.Parameters("@WorkerCodeS").Value = Session("userid")

            cmm.Parameters.Add("@WorkTimeS", SqlDbType.VarChar, 5)
            cmm.Parameters("@WorkTimeS").Value = dtTicket.Rows(0).Item("TIME_S")


            cmm.Parameters.Add("@WorkDateS", SqlDbType.VarChar, 10)
            cmm.Parameters("@WorkDateS").Value = dtTicket.Rows(0).Item("date_s")
            cmm.Parameters.Add("@TypeOpCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@TypeOpCode").Value = 0


            cmm.Parameters.Add("@WorkTypeCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@WorkTypeCode").Value = Val(Session("WorkType"))

            cmm.Parameters.Add("@WorkNote", SqlDbType.NVarChar, 250)
            cmm.Parameters("@WorkNote").Value = txt_WorkTozih.Text

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim j As Integer
            Dim txtvalue As String = ""
            Dim txt As String = ""
            For j = 0 To CheckBoxList1.Items.Count - 1
                If CheckBoxList1.Items(j).Selected = True Then
                    txt = txt & CheckBoxList1.Items(j).Value & "#"
                    txtvalue = txtvalue & CheckBoxList1.Items(j).Text & " - "
                End If

            Next
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            cmm.Parameters.Add("@notee", SqlDbType.NVarChar)

            cmm.Parameters("@notee").Value = "  کارهای انجام شده:  " & txtvalue + " " + txt_WorkTozih.Text


            cmm.Parameters.Add("@BaseWorkCode", SqlDbType.VarChar, 2000)
            cmm.Parameters("@BaseWorkCode").Value = txt

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)

            cmm.Parameters("@type_").Value = 1

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

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  عملیات با موفقیت انجام شد ")
                ' Call save_()
                ibtnSave.Enabled = False
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try




    End Sub

    Private Sub setupp()
        Dim j As Integer
        Dim txtvalue As String = ""
        Dim txt As String = ""
        For j = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(j).Selected = True Then
                txt = txt & CheckBoxList1.Items(j).Value & "#"
                txtvalue = txtvalue & CheckBoxList1.Items(j).Text & " - "


                Dim sqlstr As String = " SELECT     BaseWorkCode, masir  FROM tblBaseWork WHERE    (BaseWorkCode = " & CheckBoxList1.Items(j).Value & ")"
                Dim dts As DataTable
                dts = ClsDataTable.GetDataTable(sqlstr)
                ' Dim flnam As String = "~/Soft\"

                System.Diagnostics.Process.Start("\\intranet\TPCNET\setup.exe")
                ' System.Diagnostics.Process.Start("d:\Soft\" & dts.Rows(0).Item("masir"))
            End If

        Next
    End Sub
    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Cmbsystemg.Visible = True
            lblNew.Visible = True
            Label2.Visible = True
            btnIn.Visible = True
            btnOut.Visible = True

            txt_amvaltmp.Visible = True
            txt_Garanty.Visible = True
            txt_Mac.Visible = True
            txt_Serial.Visible = True
            txt_SystemTP.Visible = True

            lblAmvalGate.Visible = True
            lblGaranti.Visible = True
            lblMac.Visible = True
            lblSerialGate.Visible = True
            lblTP.Visible = True

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

            txt_amvaltmp.Visible = False
            txt_Garanty.Visible = False
            txt_Mac.Visible = False
            txt_Serial.Visible = False
            txt_SystemTP.Visible = False

            lblAmvalGate.Visible = False
            lblGaranti.Visible = False
            lblMac.Visible = False
            lblSerialGate.Visible = False
            lblTP.Visible = False

        End If
    End Sub
    Sub load_gataat()
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
        UpdatePanel3.Update()
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
            cmm.Parameters("@gateAmval").Value = Val(txt_amvaltmp.Text)

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
                txt_amvaltmp.Text = ""
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
    Protected Sub btnOut_Click(sender As Object, e As System.EventArgs) Handles btnOut.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTmpInOutGate", "TmpInOutId")
        End If
        save_tmp(False, False)
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


    Protected Sub btnEtmam_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEtmam.Click
        etmamm()
        chekwork()
    End Sub
    Private Sub etmamm()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        'Dim typcod As Integer = cmbWorkTyp.SelectedValue
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketEtmam"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))


            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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

                msg_box(" ثبت شد  ")
                ' Response.Redirect("frmWorkTable.aspx")
                ' notes()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub chekwork()
        Dim sqlstr2, sqlstr3 As String
        sqlstr2 = " select erja_id from tblWorkHdr where erja_id= " & Session("tiketId") & ""

        Dim dt5 As New DataTable
        dt5 = ClsDataTable.GetDataTable(sqlstr2)
        If dt5.Rows.Count = 0 Then
            sqlstr3 = " INSERT INTO tblWorkHdr (SystemCode, erja_id, WorkDateS, WorkTimeS, WorkerCodeP, WorkerCodeS, TypeOpCode, WorkTypeCode, WorkDateE, WorkTimeE, WorkNote, opid) " & _
                  " VALUES    (" & txt_SystemCode.Text & ", " & Session("tiketId") & ", '" & dtTicket.Rows(0).Item("date_s") & "', '" & dtTicket.Rows(0).Item("TIME_S") & "', " & Session("userid") & ", " & Session("userid") & ", 0, " & dtTicket.Rows(0).Item("Typestr") & ", '" & ClsDateTime.GetDateNow & "', '" & ClsDateTime.GetTimeNow & "', 'بدون ثبت ریزکار،درخواست بسته شده است', 0)"
            upStat(sqlstr3)
        End If
    End Sub
    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim unixVariables(2) As String
        '  nameLabel.Text = FQDN.Text
        Dim Proc As New System.Diagnostics.Process
        Proc.StartInfo = New ProcessStartInfo("D:\Soft\setup.exe")
        ' Proc.StartInfo.Arguments = "-pw " & UNIXPASSWORD & " " & UNIXUSERNAME & "@" & UNIXCOMPUTERNAME & " ls > output.txt"
        Proc.StartInfo.RedirectStandardInput = True
        Proc.StartInfo.RedirectStandardOutput = True
        Proc.StartInfo.UseShellExecute = False
        Proc.Start()
        ' Pause for script to run
        System.Threading.Thread.Sleep(100)
        Proc.Close()
    End Sub
End Class
