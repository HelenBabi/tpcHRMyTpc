Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors


Partial Class frmJobAdmin
    Inherits System.Web.UI.Page
    Dim warning_pic As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()
            load_users_reciver()
            load_users_sender()

        End If
        If Me.IsCallback = False Then
            load_grid()
        End If

    End Sub
    Protected Sub grddtlok_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim dt2 As DataTable
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("id_job").ToString



        Dim sqlstr As String
        sqlstr = " SELECT    tblJobDetail.date_sabt + ' ' + tblJobDetail.time_sabt as zaman, tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
"FROM         tblJob INNER JOIN " & _
 "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
"WHERE     (tblJob.id_job = " & idd & ")"
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub
    Protected Sub ASPxGrvHead_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(ASPxGrvHead.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
        If e.DataColumn.Name = "attach_" Then
            Dim hyperlink_ As HyperLink = TryCast(ASPxGrvHead.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "hiperMySend"), HyperLink)
            If hyperlink_.NavigateUrl = "" Then
                hyperlink_.Visible = False
            End If

            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub



    Protected Sub ASPxGrvHead_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        Dim date_jari As String = ClsDateTime.GetDateNow

        Dim date_job As String = CStr(e.GetValue("date_end"))

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("olaviat"))
        If value = 0 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PapayaWhip
        Else
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Azure

        End If
        'Dim date_jari As String = ClsDateTime.GetDateNow
        Dim jobStat As Integer = CStr(e.GetValue("stat"))
        If jobStat = 0 And date_job > date_jari Then
            warning_pic = "images/design_images/view_refresh.png"

        ElseIf jobStat = 0 And date_job = date_jari Then
            warning_pic = "images/design_images/view_refresh.png"
        ElseIf date_job < date_jari And jobStat = 0 Then
            warning_pic = "images/design_images/orangerefresh.png"
        ElseIf jobStat = 1 Then
            warning_pic = "images/design_images/icontexto_webdev_ok.png"
        ElseIf jobStat = 2 Then
            warning_pic = "images/design_images/unread.png"


        End If

    End Sub
    'Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
    '    load_grid()
    'End Sub
    Private Sub load_users_reciver()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = " select UserCode,UserLastName +' '+ UserFamili as nam from tblUsers where SysUser=0 and codeclosed=0 order by UserFamili "
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbreciver.DataSource = dt
        cmbreciver.DataValueField = "UserCode"
        cmbreciver.DataTextField = "nam"
        cmbreciver.DataBind()

    End Sub
    Private Sub load_users_sender()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = " select UserCode,UserLastName +' '+ UserFamili as nam from tblUsers where SysUser=0 and codeclosed=0 order by UserFamili "
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbSender.DataSource = dt
        cmbSender.DataValueField = "UserCode"
        cmbSender.DataTextField = "nam"
        cmbSender.DataBind()

    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmJobAdmin.aspx", Val(Session("UserId")))
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
            ChbActiveCode.Enabled = True
            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            ibtnDeleted.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False
            ibtnExit.Enabled = False
            txtsubject.Enabled = True

            txtRadif.Enabled = True

            txtTimeS.Enabled = True

            txtDateE.Enabled = True

            cmbolaviat.Enabled = True

            txtDateS.Enabled = True

            txtTimeE.Enabled = True
            txtDarsad.Enabled = True

            txtsubject.Enabled = True
            cmbolaviat.Enabled = True
            txtDateE.Enabled = True
            checkShowAll.Enabled = True
            cmbreciver.Enabled = True
            cmbSender.Enabled = True
            ChbActiveCode.Enabled = True
            txtTozih.Enabled = True

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
            ASPxGrvHead.Enabled = False
            cmbolaviat.Enabled = True
            ChbActiveCode.Enabled = True
            txtRadif.Enabled = False
        Else
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False
            ChbActiveCode.Enabled = False
            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True
            ibtnDeleted.Enabled = True
            ibtnHistory.Enabled = True
            ibtnDeactived.Enabled = True
            ibtnPrint.Enabled = True
            ibtnExit.Enabled = True


            txtsubject.Enabled = False
            txtRadif.Enabled = False
            txtTimeS.Enabled = False
            txtDateE.Enabled = False
            cmbolaviat.Enabled = False
            txtDateS.Enabled = False
            txtTimeE.Enabled = False
            txtDarsad.Enabled = False
            txtsubject.Enabled = False
            cmbolaviat.Enabled = False
            txtDateE.Enabled = False
            checkShowAll.Enabled = False
            cmbreciver.Enabled = False
            cmbSender.Enabled = False
            ChbActiveCode.Enabled = False
            txtTozih.Enabled = False

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
            cmbolaviat.Enabled = False
            ChbActiveCode.Enabled = False
            ' txt_BakhshCode.Enabled = True
        End If

    End Sub

    Private Sub clear_form()
        txtRadif.Text = ""

        txtsubject.Text = ""
        txtRadif.Text = ""
        txtTimeS.Text = ""
        txtDateE.Text = ""

        txtDarsad.Text = ""
        txtDateS.Text = ""
        txtTimeE.Text = ""

        ChbActiveCode.Checked = False

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
    End Sub

    Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
    End Sub

    Private Sub New_()
        hdn_Final_key.Value = "new"
        clear_form()
        txtRadif.Text = ClsGetMaxId.GetMaxId("tblJob", "id_job")
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

    Protected Sub ibtnDeactived_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDeactived.Click
        Call Deactived_()
    End Sub
    Private Sub Deactived_()
        If hdn_deactivedcode.Value = "false" Then
            hdn_deactivedcode.Value = "true"
        Else
            hdn_deactivedcode.Value = "false"
        End If
        load_grid()
    End Sub

#End Region


    Private Sub load_grid()
        Dim ClsB_ As New ClsJob
        Dim dt As New DataTable
        dt = ClsB_.GetAllJob(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ClsJob
            Dim dt As New DataTable
            dt = ClsB.GetJob(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)


            txtRadif.Text = dt.Rows(0).Item("id_job").ToString
            txtsubject.Text = dt.Rows(0).Item("subject").ToString

            txtDateS.Text = dt.Rows(0).Item("date_start").ToString
            txtTimeS.Text = dt.Rows(0).Item("time_start").ToString
            txtDateE.Text = dt.Rows(0).Item("date_end").ToString
            txtTimeE.Text = dt.Rows(0).Item("time_end").ToString
            txtDarsad.Text = dt.Rows(0).Item("darsad").ToString
            If dt.Rows(0).Item("ShowAll").ToString = True Then
                checkShowAll.Checked = True
            Else
                checkShowAll.Checked = False
            End If
            If dt.Rows(0).Item("olaviat").ToString = True Then
                cmbolaviat.SelectedValue = 1
            Else
                cmbolaviat.SelectedValue = 0
            End If
            ' cmbSender.SelectedValue = dt.Rows(0).Item("sender").ToString
            ' cmbreciver.SelectedValue = dt.Rows(0).Item("reciver").ToString
            txtTozih.Text = dt.Rows(0).Item("note").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then txtRadif.Text = ClsGetMaxId.GetMaxId("tblJob", "id_job")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        '@pid_job int,
        '@pSystemCode numeric(6,0),
        '@CodeClosed bit,
        '@userid int ,
        '@pHolderName varchar(20),
        '@pHolderPhone numeric(4,0),
        '@pm nvarchar(500) out,
        '@ResultCode tinyint out,
        '@pGenuineCode  nvarchar(20),
        '@pFareiCode  nvarchar(20),
        '@pSerial  nvarchar(20),
        '@Ip  nvarchar(20),
        '@type_ tinyint
        Dim olavi As Boolean

        If cmbolaviat.SelectedValue = 1 Then
            olavi = True

        ElseIf cmbolaviat.SelectedValue = 0 Then
            olavi = False
        End If
        cmm.CommandText = "PrcJobAdmin"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@ShowAll", SqlDbType.Bit, 1)
        cmm.Parameters("@ShowAll").Value = checkShowAll.Checked

        cmm.Parameters.Add("@id_job", SqlDbType.Int, 6)
        cmm.Parameters("@id_job").Value = Val(txtRadif.Text)

        cmm.Parameters.Add("@subject", SqlDbType.VarChar, 150)
        cmm.Parameters("@subject").Value = txtsubject.Text

        cmm.Parameters.Add("@olaviat", SqlDbType.Bit, 1)
        cmm.Parameters("@olaviat").Value = olavi

        cmm.Parameters.Add("@date_start", SqlDbType.VarChar, 10)
        cmm.Parameters("@date_start").Value = txtDateS.Text

        cmm.Parameters.Add("@time_start", SqlDbType.VarChar, 6)
        cmm.Parameters("@time_start").Value = txtTimeS.Text

        cmm.Parameters.Add("@date_end", SqlDbType.VarChar, 10)
        cmm.Parameters("@date_end").Value = txtDateE.Text

        cmm.Parameters.Add("@time_end", SqlDbType.VarChar, 6)
        cmm.Parameters("@time_end").Value = txtTimeE.Text

        cmm.Parameters.Add("@darsad", SqlDbType.VarChar, 3)
        cmm.Parameters("@darsad").Value = txtDarsad.Text

        cmm.Parameters.Add("@note", SqlDbType.NVarChar, 250)
        cmm.Parameters("@note").Value = txtTozih.Text

        cmm.Parameters.Add("@sender", SqlDbType.Int, 5)
        cmm.Parameters("@sender").Value = cmbSender.SelectedValue

        cmm.Parameters.Add("@reciver", SqlDbType.Int, 5)
        cmm.Parameters("@reciver").Value = cmbreciver.SelectedValue



        cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
        cmm.Parameters("@userid").Value = Session("UserId")

        cmm.Parameters.Add("@CodeClosed", SqlDbType.Bit, 1)
        cmm.Parameters("@CodeClosed").Value = ChbActiveCode.Checked

        cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
        If hdn_Final_key.Value = "del" Then
            cmm.Parameters("@type_").Value = 3
        ElseIf hdn_Final_key.Value = "edit" Then
            cmm.Parameters("@type_").Value = 2
        ElseIf hdn_Final_key.Value = "new" Then
            cmm.Parameters("@type_").Value = 1
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
            load_grid()
        Else
            msg_box(cmm.Parameters("@pm").Value)
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

   
    Protected Sub ibtnExit0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit0.Click
        grvExporter1.WriteXlsToResponse()
    End Sub

    '    Protected Sub ASPxGrvHead_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Dim idd As Integer
    '        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
    '        idd = grid.GetMasterRowFieldValues("id_job").ToString
    '        Dim dt2 As DataTable


    '        Dim sqlstr As String
    '        sqlstr = " SELECT     tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
    '"FROM         tblJob INNER JOIN " & _
    ' "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
    '"WHERE     (tblJob.id_job = " & idd & ")"
    '        dt2 = ClsDataTable.GetDataTable(sqlstr)
    '        grid.DataSource = dt2
    '        'grid.DataBind()

    '    End Sub
    'Protected Sub ASPxGrvHead_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared



    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("olaviat"))
    '    If value = 0 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.PapayaWhip
    '    Else
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.Azure

    '    End If


    'End Sub

End Class
