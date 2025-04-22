Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class chartMgrFrm
    Inherits System.Web.UI.Page
    Dim warning_pic As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        meno_acc()
        'End If
        If Me.IsPostBack = False Then
            '  Session("UserId") = 1

            'load_Units()
            'load_nostexdam()
            'load_ellatfalli()
            BindGridviewData()
        End If
        'load_grid()
        BindGridviewData()
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        BindGridviewData()
        subject.Focus()

    End Sub
    Private Sub BindGridviewData()

        Dim dt3 As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       chartTbl.chartID, chartTbl.chartName, chartTbl.codePersoneli, chartTbl.personelAdmin ,personel.CodeClosed, personel.nam, personel.family,  personel.codeMelli, personel.teldaxeli, personel.telmanzel, personel.mobile,  " & _
        "tblUnits.UnitsName, omurTbl.omur " & _
   "FROM          " & _
   "chartTbl INNER JOIN " & _
                         "personel ON chartTbl.codePersoneli = personel.codePersoneli " & _
 " INNER JOIN " & _
                        " tblUnits ON personel.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                        " omurTbl ON personel.omurID = omurTbl.omurID " & _
"WHERE        (personel.CodeClosed = 0) and        (personel.nam LIKE N'%" & subject.Text & "%') OR " & _
                         "(personel.family LIKE N'%" & subject.Text & "%') OR " & _
                        " (personel.teldaxeli LIKE N'%" & subject.Text & "%') OR " & _
                        " (personel.telmanzel LIKE N'%" & subject.Text & "%') OR " & _
                        " (personel.mobile LIKE N'%" & subject.Text & "%') OR " & _
                        " (tblUnits.UnitsName LIKE N'%" & subject.Text & "%') OR " & _
                                " (omurTbl.omur LIKE N'%" & subject.Text & "%') Or " & _
                        " (chartTbl.codePersoneli LIKE '%" & subject.Text & "%') or " & _
         " (chartTbl.chartName LIKE '%" & subject.Text & "%')"

        dt3 = ClsDataTable.GetDataTable(sqlstr)
        If dt3.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dt3
            ASPxGrvHead.DataBind()

        End If


    End Sub
    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        BindGridviewData()
    End Sub
    Protected Sub ASPxGrvDtl_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("personelAdmin").ToString

        'Dim ClsB_ As New ClsBrowsSystems
        'Dim dt2 As New DataTable
        'dt2 = ClsB_.GetAllBrowsSystems(idd)
        'grid.DataSource = dt2
        Dim sqlstr As String

        sqlstr = "SELECT  * from chartSarparastVW WHERE        (codePersoneli = " & idd & ")"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt
        'grid.DataBind()



    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("chartMgrFrm.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        'ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)
        'ibtnExitttt.Visible = meno_arr(7)
        ibtnHistory.Visible = meno_arr(8)
        ibtnDeactived.Visible = meno_arr(9)
        ibtnPrint.Visible = meno_arr(10)
        'ibtnExitttt.Visible = meno_arr(11)
    End Sub

    Private Sub enable_disable()
        If ibtnSave.Enabled = False Then
            ibtnSave.Enabled = True
            'ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True
            'ChbActiveCode.Enabled = True
            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            'ibtnExitttt.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False

            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"
            'ibtnSaveNew.ImageUrl = "~/Icons/savenew.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel.png"

            ibtnEdit.ImageUrl = "~/Icons/edit2.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete2.png"
            ibtnAdd.ImageUrl = "~/Icons/Add2.png"
            'ibtnExitttt.ImageUrl = "~/Icons/deleted2.png"
            ibtnHistory.ImageUrl = "~/Icons/history2.png"
            ibtnDeactived.ImageUrl = "~/Icons/reload2.png"
            ibtnPrint.ImageUrl = "~/Icons/Print2.png"
            ''''''''''''''''''''''''''''''''''''''''''''''''
            ASPxGrvHead.Enabled = False

            chartID.Enabled = False
            chartName.Enabled = True
            codePersoneli.Enabled = True
            personelAdmin.Enabled = True


        Else
            'adres.Enabled = False
            'CmbUnit.Enabled = False
            ibtnSave.Enabled = False
            'ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False
            'ChbActiveCode.Enabled = False
            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True

            ibtnHistory.Enabled = True
            ibtnDeactived.Enabled = True
            ibtnPrint.Enabled = True
            'ibtnExitttt.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"
            'ibtnSaveNew.ImageUrl = "~/Icons/savenew2.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel2.png"

            ibtnEdit.ImageUrl = "~/Icons/edit.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete.png"
            ibtnAdd.ImageUrl = "~/Icons/Add.png"
            'ibtnExitttt.ImageUrl = "~/Icons/deleted.png"
            ibtnHistory.ImageUrl = "~/Icons/history.png"
            ibtnDeactived.ImageUrl = "~/Icons/reload.png"
            ibtnPrint.ImageUrl = "~/Icons/Print.png"
            ''''''''''''''''''''''''''''''''''''''''''''''''
            ASPxGrvHead.Enabled = True
            chartID.Enabled = False
            chartName.Enabled = False
            codePersoneli.Enabled = False
            personelAdmin.Enabled = False

            'txtPcUserFamili.Enabled = False
            'txtPcUserName.Enabled = False
            'codemelli.Enabled = False

            'father.Enabled = False

            'teldaxele.Enabled = False
            'telmanzel.Enabled = False
            'mobile.Enabled = False

        End If

    End Sub

    Private Sub clear_form()

        'chartID.Text = ""

        chartName.Text = ""
        codePersoneli.Text = ""
        personelAdmin.Text = ""


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
        'If txtCodePerson.Text = "" Then
        '    msg_box("باید آی دی کاربر را وارد کنید")
        '    txtCodePerson.Focus()
        '    Exit Sub

        'End If
        Call save_final()

    End Sub

    Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
    End Sub

    Private Sub New_()
        hdn_Final_key.Value = "new"
        clear_form()
        chartID.Text = ClsGetMaxId.GetMaxId("chartTbl", "chartID")
        enable_disable()
    End Sub
    Private Sub save_()
        enable_disable()
        clear_form()
        hdn_Final_key.Value = 0
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
        'Dim dt As DataTable
        'Dim sqlstr As String
        'sqlstr = " select PcMizbanId from tblPcDomainUserDtl where  PcMizbanId=  " & Val(txtPcUser.Text) & ""
        'dt = ClsDataTable.GetDataTable(sqlstr)
        'If dt.Rows.Count > 0 Then
        '    msg_box("اين كداستفاده شده است و نمي تواند حذف شود")
        '    Exit Sub
        'End If
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
            'lblellat.Visible = True
            'tarixDisabled.Visible = True
            'lblellattarix.Visible = True
            'ellat.Visible = True
        Else
            hdn_deactivedcode.Value = "false"
            'lblellat.Visible = False
            'tarixDisabled.Visible = False
            'lblellattarix.Visible = False
            'ellat.Visible = False
        End If
        load_grid()
    End Sub

#End Region


    Private Sub load_grid()
        Dim ClsB_ As New chartCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllBaseDomainUser(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New chartCls
            Dim dt As New DataTable
            dt = ClsB.GetBaseDomainUser(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            'txtCode.Text = dt.Rows(0).Item("ID").ToString
            'txtPcUserName.Text = dt.Rows(0).Item("nam").ToString
            'txtPcUserFamili.Text = dt.Rows(0).Item("family").ToString
            'txtCodePerson.Text = dt.Rows(0).Item("codepersoneli").ToString
            chartID.Text = dt.Rows(0).Item("chartID").ToString
            chartName.Text = dt.Rows(0).Item("chartName").ToString
            codePersoneli.Text = dt.Rows(0).Item("codePersoneli").ToString
            personelAdmin.Text = dt.Rows(0).Item("personelAdmin").ToString

        End If
    End Sub


    Private Sub save_final()
        'If hdn_Final_key.Value = "new" Then txtCode.Text = ClsGetMaxId.GetMaxId("personel", "id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        cmm.CommandText = "chartMgrPrc"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@chartID", SqlDbType.Int, 6)
        cmm.Parameters("@chartID").Value = Val(chartID.Text)

        cmm.Parameters.Add("@chartName", SqlDbType.NVarChar, 100)
        cmm.Parameters("@chartName").Value = chartName.Text

        cmm.Parameters.Add("@codePersoneli", SqlDbType.BigInt, 6)
        cmm.Parameters("@codePersoneli").Value = codePersoneli.Text

        cmm.Parameters.Add("@personelAdmin", SqlDbType.BigInt, 6)
        cmm.Parameters("@personelAdmin").Value = personelAdmin.Text

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



    Protected Sub ASPxGrvHead_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(ASPxGrvHead.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub
    Protected Sub ASPxGrvHead_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        warning_pic = "images/person_pic/" & value & ".jpg"
    End Sub

    Protected Sub ASPxGrvDtl_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst2 As Image = TryCast(ASPxGrvHead.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image9"), Image)
            iamgegrdst2.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub
    'Protected Sub ASPxGrvDtl_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvDtl.HtmlRowPrepared

    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
    '    warning_pic = "images/person_pic/" & value & ".jpg"
    'End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("index.aspx")
    End Sub



    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub
End Class
