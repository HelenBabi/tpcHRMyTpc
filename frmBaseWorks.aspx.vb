Imports System.Data
Imports System.Data.SqlClient

Partial Class frmBaseWorks
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            '  Session("UserId") = 1

            load_work_type()
            ' meno_acc()
        End If
        load_grid()

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


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBaseWorks.aspx", Val(Session("UserId")))
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
            txt_BaseWorkName.Enabled = True
            ChbActiveCode.Enabled = True
            txt_BaseWorkCode.Enabled = False
            CmbWorkeType.Enabled = True
            txtMasir.Enabled = True
            chass.Enabled = True
        Else
            chass.Enabled = False
            ChbActiveCode.Enabled = False
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
            txt_BaseWorkName.Enabled = False
            txtMasir.Enabled = False
            CmbWorkeType.Enabled = False
            ChbActiveCode.Enabled = False
            ' txt_BakhshCode.Enabled = True
        End If

    End Sub

    Private Sub clear_form()
        txt_BaseWorkCode.Text = ""
        txt_BaseWorkName.Text = ""
        txtMasir.Text = ""
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
        txt_BaseWorkCode.Text = ClsGetMaxId.GetMaxId("tblBaseWork", "BaseWorkCode")
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
        CmbWorkeType.Enabled = True
    End Sub

    Protected Sub ibtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEdit.Click

        Call edit_()
    End Sub
    Private Sub edit_()
        enable_disable()
        CmbWorkeType.Enabled = False
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
        CmbWorkeType.Enabled = True
    End Sub
    Private Sub dell_()
        Dim dt As DataTable
        Dim sqlstr As String
        sqlstr = " select BaseWorkCode from tblworkdtl where  BaseWorkCode=  " & Val(txt_BaseWorkCode.Text) & ""
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            msg_box("اين كد نمي تواند حذف شود")
            Exit Sub
        End If
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
        Dim ClsB_ As New ClsBaseWork
        Dim dt As New DataTable
        dt = ClsB_.GetAllBaseWork(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ClsBaseWork
            Dim dt As New DataTable
            dt = ClsB.GetBaseWork(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            txt_BaseWorkCode.Text = dt.Rows(0).Item("BaseWorkCode").ToString
            txt_BaseWorkName.Text = dt.Rows(0).Item("BaseWorkName").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
            chass.Checked = dt.Rows(0).Item("showAssistant").ToString
            txtMasir.Text = dt.Rows(0).Item("masir").ToString
            CmbWorkeType.SelectedValue = dt.Rows(0).Item("WorkTypeCode").ToString
        End If
    End Sub
    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("WorkTypeCode"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Green
            '   e.Row.BackColor = Drawing.Color.LightGreen
        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Magenta
            ' e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
        ElseIf value = 3 Then
            e.Row.ForeColor = Drawing.Color.Blue

            ' e.Row.BackColor = Drawing.Color.Aquamarine

        ElseIf value = 4 Then
            e.Row.ForeColor = Drawing.Color.Brown
            '  e.Row.BackColor = Drawing.Color.BurlyWood
        End If


    End Sub

    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then txt_BaseWorkCode.Text = ClsGetMaxId.GetMaxId("tblBaseWork", "BaseWorkCode")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        cmm.CommandText = "PrcBaseWork"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@showAssistant", SqlDbType.Bit, 1)
        If chass.Checked = True Then
            cmm.Parameters("@showAssistant").Value = 1
        Else
            cmm.Parameters("@showAssistant").Value = 0
        End If



        cmm.Parameters.Add("@WorkTypeCode", SqlDbType.Int, 2)
        cmm.Parameters("@WorkTypeCode").Value = Val(CmbWorkeType.SelectedValue)

        cmm.Parameters.Add("@BaseWorkCode", SqlDbType.Int, 6)
        cmm.Parameters("@BaseWorkCode").Value = txt_BaseWorkCode.Text

        cmm.Parameters.Add("@BaseWorkName", SqlDbType.NVarChar, 100)
        cmm.Parameters("@BaseWorkName").Value = txt_BaseWorkName.Text

        cmm.Parameters.Add("@masir", SqlDbType.VarChar, 1000)
        cmm.Parameters("@masir").Value = txtMasir.Text

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



End Class
