Imports System.Data
Imports System.Data.SqlClient

Partial Class frmPersonals
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("super_vizer") = False Then
            meno_acc()
        End If
        If Me.IsPostBack = False Then
            '  Session("UserId") = 1

            load_Units()
            load_nostexdam()
            load_ellatfalli()
            load_Vahed()
        End If
        load_grid()

    End Sub

    Private Sub load_Units()
        Dim dt As New DataTable
        Dim cls_ As New ClsUnits
        dt = cls_.GetAllUnitsDDL
        CmbUnit.DataSource = dt
        CmbUnit.DataValueField = "UnitsCode"
        CmbUnit.DataTextField = "UnitsName"
        CmbUnit.DataBind()

    End Sub
    Private Sub load_ellatfalli()
        Dim dt As New DataTable
        Dim cls_ As New ClsBuilding
        dt = cls_.GetAllBuildingDDL
        ellat.DataSource = dt
        ellat.DataValueField = "BuildingCode"
        ellat.DataTextField = "BuildingName"
        ellat.DataBind()

    End Sub

    Private Sub load_nostexdam()
        Dim dt As New DataTable
        Dim cls_ As New ClsNoStexdam
        dt = cls_.GetAllUnitsDDL
        noeestexdam.DataSource = dt
        noeestexdam.DataValueField = "id"
        noeestexdam.DataTextField = "value"
        noeestexdam.DataBind()

    End Sub

#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmPersonals.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
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
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True
            ChbActiveCode.Enabled = True
            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            'ibtnExitttt.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False

            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew.png"
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
            txtCodePerson.Enabled = True
            ChbActiveCode.Enabled = True
            CmbUnit.Enabled = True
            'txtPcUser.Enabled = False
            txtPcUserFamili.Enabled = True
            txtPcUserName.Enabled = True

            codemelli.Enabled = True

            father.Enabled = True
            shsh.Enabled = True
            mahalesodur.Enabled = True
            shserial.Enabled = True
            vaziatsh.Enabled = True
            mahaletavalod.Enabled = True
            tarixtavalod.Enabled = True
            noeestexdam.Enabled = True
            ellat.Enabled = True
            tarixstexdam.Enabled = True
            vaziatathol.Enabled = True
            teldaxele.Enabled = True
            telmanzel.Enabled = True
            mobile.Enabled = True
            mazhab.Enabled = True
            madrak.Enabled = True
            shgozarname.Enabled = True
            din.Enabled = True
            'semat.Enabled = True
            adres.Enabled = True
            CmbVahed.Enabled = True
           shHesab.Enabled = True
     shBime.Enabled = True
            codePosti.Enabled = True
            reshte.Enabled = True
            chart.Enabled = True
            sarparast.Enabled = True

        Else
            adres.Enabled = False
            CmbUnit.Enabled = False
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False
            ChbActiveCode.Enabled = False
            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True

            ibtnHistory.Enabled = True
            ibtnDeactived.Enabled = True
            ibtnPrint.Enabled = True
            'ibtnExitttt.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew2.png"
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
            txtCodePerson.Enabled = False
            ChbActiveCode.Enabled = False

            txtPcUserFamili.Enabled = False
            txtPcUserName.Enabled = False
            codemelli.Enabled = False

            father.Enabled = False
            shsh.Enabled = False
            mahalesodur.Enabled = False
            shserial.Enabled = False
            vaziatsh.Enabled = False
            mahaletavalod.Enabled = False
            tarixtavalod.Enabled = False
            noeestexdam.Enabled = False
            ellat.Enabled = False
            tarixstexdam.Enabled = False
            vaziatathol.Enabled = False
            teldaxele.Enabled = False
            telmanzel.Enabled = False
            mobile.Enabled = False
            mazhab.Enabled = False
            madrak.Enabled = False
            shgozarname.Enabled = False
            din.Enabled = False
            'semat.Enabled = False
            CmbVahed.Enabled = False

            shHesab.Enabled = False
            shBime.Enabled = False
            codePosti.Enabled = False
            reshte.Enabled = False
            chart.Enabled = False
            sarparast.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        adres.Text = ""
        tarixDisabled.Text = ""
        'semat.Text = ""
        txtCodePerson.Text = ""
        ChbActiveCode.Checked = False
        txtPcUserName.Text = ""
        txtPcUserFamili.Text = ""
        codemelli.Text = ""
        father.Text = ""
        shsh.Text = ""
        mahalesodur.Text = ""
        shserial.Text = ""
        vaziatsh.SelectedIndex = 0
        mahaletavalod.Text = ""
        tarixtavalod.Text = ""
        noeestexdam.SelectedValue = 0
        ellat.SelectedValue = 0
        tarixstexdam.Text = ""
        vaziatathol.SelectedIndex = 0
        teldaxele.Text = ""
        telmanzel.Text = ""
        mobile.Text = ""
        mazhab.SelectedIndex = 0
        madrak.Text = ""
        shgozarname.Text = ""
        din.SelectedIndex = 0
        shHesab.Text = ""

        shBime.Text = ""
        codePosti.Text = ""

        reshte.Text = ""
        chart.Text = ""
        sarparast.Text = ""

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
        txtCode.Text = ClsGetMaxId.GetMaxId("personel", "id")
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
            lblellat.Visible = True
            tarixDisabled.Visible = True
            lblellattarix.Visible = True
            ellat.Visible = True
        Else
            hdn_deactivedcode.Value = "false"
            lblellat.Visible = False
            tarixDisabled.Visible = False
            lblellattarix.Visible = False
            ellat.Visible = False
        End If
        load_grid()
    End Sub

#End Region


    Private Sub load_grid()
        Dim ClsB_ As New ClsPersonal
        Dim dt As New DataTable
        dt = ClsB_.GetAllBaseDomainUser(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ClsPersonal
            Dim dt As New DataTable
            dt = ClsB.GetBaseDomainUser(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            txtCode.Text = dt.Rows(0).Item("ID").ToString
            txtPcUserName.Text = dt.Rows(0).Item("nam").ToString
            txtPcUserFamili.Text = dt.Rows(0).Item("family").ToString
            txtCodePerson.Text = dt.Rows(0).Item("codepersoneli").ToString
            CmbUnit.SelectedValue = dt.Rows(0).Item("UnitsCode").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
            'If ChbActiveCode.Checked = False Then
            '    tarixDisabled.Text = "فعال"
            '    ellat.Visible = False
            'Else
            '    tarixDisabled.Text = "غیرفعال"
            '    ellat.Visible = True
            '    'ellat.Text = dt.Rows(0).Item("ellatevaziat").ToString
            'End If
            codemelli.Text = dt.Rows(0).Item("codeMelli").ToString
            father.Text = dt.Rows(0).Item("father").ToString
            shsh.Text = dt.Rows(0).Item("shsh").ToString
            mahalesodur.Text = dt.Rows(0).Item("mahalesodur").ToString
            shserial.Text = dt.Rows(0).Item("shserial").ToString
            vaziatsh.SelectedValue = dt.Rows(0).Item("vaziatsh").ToString
            mahaletavalod.Text = dt.Rows(0).Item("mahaletavalod").ToString
            tarixtavalod.Text = dt.Rows(0).Item("tarixtavalod").ToString
            noeestexdam.SelectedValue = dt.Rows(0).Item("typ").ToString

            tarixstexdam.Text = dt.Rows(0).Item("tarixestexdam").ToString
            vaziatathol.SelectedValue = dt.Rows(0).Item("vaziattahol").ToString
            teldaxele.Text = dt.Rows(0).Item("teldaxeli").ToString
            telmanzel.Text = dt.Rows(0).Item("telmanzel").ToString
            mobile.Text = dt.Rows(0).Item("mobile").ToString
            mazhab.SelectedValue = dt.Rows(0).Item("mazhab").ToString
            madrak.Text = dt.Rows(0).Item("madrak").ToString
            shgozarname.Text = dt.Rows(0).Item("shgozarname").ToString
            din.SelectedValue = dt.Rows(0).Item("din").ToString
            'semat.Text = dt.Rows(0).Item("postname").ToString
            tarixDisabled.Text = dt.Rows(0).Item("tarixDisable").ToString
            ellat.SelectedValue = dt.Rows(0).Item("ellatevaziat").ToString
            adres.Text = dt.Rows(0).Item("adres").ToString
            CmbVahed.SelectedValue = dt.Rows(0).Item("omurID").ToString

            shHesab.Text = dt.Rows(0).Item("shHesab").ToString

            shBime.Text = dt.Rows(0).Item("codebime").ToString
            codePosti.Text = dt.Rows(0).Item("codePosti").ToString

            reshte.Text = dt.Rows(0).Item("reshte").ToString

            chart.Text = dt.Rows(0).Item("chartName").ToString
            sarparast.Text = dt.Rows(0).Item("personelAdmin").ToString
        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then txtCode.Text = ClsGetMaxId.GetMaxId("personel", "id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        cmm.CommandText = "PrcUnitsPersonal"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@id", SqlDbType.Int, 6)
        cmm.Parameters("@id").Value = Val(txtCode.Text)



        cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 50)
        cmm.Parameters("@nam").Value = txtPcUserName.Text

        cmm.Parameters.Add("@family", SqlDbType.NVarChar, 100)
        cmm.Parameters("@family").Value = txtPcUserFamili.Text

        cmm.Parameters.Add("@codepersoneli", SqlDbType.VarChar, 100)
        cmm.Parameters("@codepersoneli").Value = txtCodePerson.Text

        cmm.Parameters.Add("@UnitsCode", SqlDbType.Int, 6)
        cmm.Parameters("@UnitsCode").Value = CmbUnit.SelectedValue

        cmm.Parameters.Add("@omurID", SqlDbType.Int, 6)
        cmm.Parameters("@omurID").Value = CmbVahed.SelectedValue

        cmm.Parameters.Add("@postname", SqlDbType.NVarChar, 50)
        cmm.Parameters("@postname").Value = chart.Text

        cmm.Parameters.Add("@sarparast", SqlDbType.NVarChar, 50)
        cmm.Parameters("@sarparast").Value = sarparast.Text


        cmm.Parameters.Add("@din", SqlDbType.NVarChar, 50)
        cmm.Parameters("@din").Value = din.SelectedValue

        cmm.Parameters.Add("@codeMelli", SqlDbType.VarChar, 50)
        cmm.Parameters("@codeMelli").Value = codemelli.Text

        cmm.Parameters.Add("@shgozarname", SqlDbType.NVarChar, 50)
        cmm.Parameters("@shgozarname").Value = shgozarname.Text

        cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
        cmm.Parameters("@madrak").Value = madrak.Text

        cmm.Parameters.Add("@mazhab", SqlDbType.NVarChar, 50)
        cmm.Parameters("@mazhab").Value = mazhab.SelectedValue

        cmm.Parameters.Add("@mobile", SqlDbType.NVarChar, 50)
        cmm.Parameters("@mobile").Value = mobile.Text

        cmm.Parameters.Add("@telmanzel", SqlDbType.NVarChar, 50)
        cmm.Parameters("@telmanzel").Value = telmanzel.Text

        cmm.Parameters.Add("@teldaxeli", SqlDbType.NVarChar, 50)
        cmm.Parameters("@teldaxeli").Value = teldaxele.Text

        cmm.Parameters.Add("@vaziattahol", SqlDbType.NVarChar, 50)
        cmm.Parameters("@vaziattahol").Value = vaziatathol.SelectedValue

        cmm.Parameters.Add("@tarixestexdam", SqlDbType.VarChar, 50)
        cmm.Parameters("@tarixestexdam").Value = tarixstexdam.Text

        cmm.Parameters.Add("@ellatevaziat", SqlDbType.Int)
        cmm.Parameters("@ellatevaziat").Value = ellat.SelectedValue

        cmm.Parameters.Add("@adres", SqlDbType.NVarChar, 500)
        cmm.Parameters("@adres").Value = adres.Text

        cmm.Parameters.Add("@tarixdisable", SqlDbType.VarChar, 50)
        cmm.Parameters("@tarixdisable").Value = tarixDisabled.Text


        cmm.Parameters.Add("@typ", SqlDbType.SmallInt)
        cmm.Parameters("@typ").Value = noeestexdam.SelectedValue

        cmm.Parameters.Add("@tarixtavalod", SqlDbType.VarChar, 50)
        cmm.Parameters("@tarixtavalod").Value = tarixtavalod.Text

        cmm.Parameters.Add("@mahaletavalod", SqlDbType.NVarChar, 50)
        cmm.Parameters("@mahaletavalod").Value = mahaletavalod.Text

        cmm.Parameters.Add("@vaziatsh", SqlDbType.NVarChar, 50)
        cmm.Parameters("@vaziatsh").Value = vaziatsh.SelectedValue

        cmm.Parameters.Add("@shserial", SqlDbType.NVarChar, 50)
        cmm.Parameters("@shserial").Value = shserial.Text

        cmm.Parameters.Add("@mahalesodur", SqlDbType.NVarChar, 50)
        cmm.Parameters("@mahalesodur").Value = mahalesodur.Text

        cmm.Parameters.Add("@father", SqlDbType.NVarChar, 50)
        cmm.Parameters("@father").Value = father.Text

        cmm.Parameters.Add("@shsh", SqlDbType.NVarChar, 50)
        cmm.Parameters("@shsh").Value = shsh.Text

        cmm.Parameters.Add("@shHesab", SqlDbType.NVarChar, 50)
        cmm.Parameters("@shHesab").Value = shHesab.Text

        cmm.Parameters.Add("@codebime", SqlDbType.NVarChar, 50)
        cmm.Parameters("@codebime").Value = shBime.Text

        cmm.Parameters.Add("@codePosti", SqlDbType.NVarChar, 50)
        cmm.Parameters("@codePosti").Value = codePosti.Text

        cmm.Parameters.Add("@reshte", SqlDbType.NVarChar, 50)
        cmm.Parameters("@reshte").Value = reshte.Text

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

        'If hdn_Final_key.Value = "edit" And ChbActiveCode.Checked = True And ellat.SelectedValue = 0 Then

        '    msg_box("علت غیرفعالی باید مشخص شود")

        '    Exit Sub
        'End If

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






    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ChbActiveCode_CheckedChanged(sender As Object, e As EventArgs) Handles ChbActiveCode.CheckedChanged
        If ChbActiveCode.Checked = True Then
            lblellat.Visible = True
            tarixDisabled.Visible = True
            lblellattarix.Visible = True
            ellat.Visible = True
            ellat.Focus()
        Else
            lblellat.Visible = False
            tarixDisabled.Visible = False
            lblellattarix.Visible = False
            ellat.Visible = False

        End If
    
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub

    
    Private Sub load_Vahed()
        Dim dt As New DataTable
        Dim sqlstrr As String = "SELECT        omurID, omur " & _
       " FROM omurTbl WHERE        (CodeClosed = 0) ORDER BY omur"
        dt = ClsDataTable.GetDataTable(sqlstrr)
        CmbVahed.DataSource = dt
        CmbVahed.DataValueField = "omurID"
        CmbVahed.DataTextField = "omur"
        CmbVahed.DataBind()

    End Sub
End Class
