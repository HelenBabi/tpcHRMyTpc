Imports System.Data
Imports System.Data.SqlClient
Partial Class trnTechersOutEntryFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
      
        load_grid()

    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnTechersOutEntryFrm.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)

        ibtnDeactived.Visible = meno_arr(9)

    End Sub

    Private Sub enable_disable()
        If ibtnSave.Enabled = False Then
            ibtnSave.Enabled = True
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True

            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False

            ibtnDeactived.Enabled = False
            ibtnExit.Enabled = False
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel.png"

            ibtnEdit.ImageUrl = "~/Icons/edit2.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete2.png"
            ibtnAdd.ImageUrl = "~/Icons/Add2.png"

            ibtnDeactived.ImageUrl = "~/Icons/reload2.png"

            ''''''''''''''''''''''''''''''''''''''''''''''''
            ASPxGrvHead.Enabled = False
            Panel1.Enabled = True



        Else
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False
            ChbActiveCode.Enabled = False
            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True

            ibtnDeactived.Enabled = True

            ibtnExit.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew2.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel2.png"

            ibtnEdit.ImageUrl = "~/Icons/edit.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete.png"
            ibtnAdd.ImageUrl = "~/Icons/Add.png"

            ibtnDeactived.ImageUrl = "~/Icons/reload.png"

            ''''''''''''''''''''''''''''''''''''''''''''''''
            ASPxGrvHead.Enabled = True

            ChbActiveCode.Enabled = False
            ' txt_BakhshCode.Enabled = True
            Panel1.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        'Panel1.Dispose()

        ChbActiveCode.Checked = False

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
        'txt_UnitsCode.Text = ClsGetMaxId.GetMaxId("tblUnits", "UnitsCode")
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
        clear_form()

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
        Dim ClsB_ As New trnTechersOutCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub




    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then
            HiddenField1.Value = ClsGetMaxId.GetMaxId("trnTechersOutCls", "techID")
        End If



        'If hdn_Final_key.Value = "new" Then txtTicketId.Text = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "trnTechersOutPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@typ", SqlDbType.Int, 5)
            If hdn_Final_key.Value = "del" Then
                cmm.Parameters("@typ").Value = 3
            ElseIf hdn_Final_key.Value = "edit" Then
                cmm.Parameters("@typ").Value = 2
            ElseIf hdn_Final_key.Value = "new" Then
                cmm.Parameters("@typ").Value = 1
            End If

            cmm.Parameters.Add("@techID", SqlDbType.Int, 10)
            cmm.Parameters("@techID").Value = HiddenField1.Value

            cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nam").Value = nam.Text

            cmm.Parameters.Add("@codemeli", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codemeli").Value = codemeli.Text

            cmm.Parameters.Add("@codeEgtesadi", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codeEgtesadi").Value = codeEgtesadi.Text

            cmm.Parameters.Add("@shenasemeli", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shenasemeli").Value = shenasemeli.Text

            cmm.Parameters.Add("@sabtNo", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sabtNo").Value = sabtNo.Text

            cmm.Parameters.Add("@codePosti", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codePosti").Value = codePosti.Text

            cmm.Parameters.Add("@tel", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tel").Value = tel.Text

            cmm.Parameters.Add("@fax", SqlDbType.NVarChar, 50)
            cmm.Parameters("@fax").Value = fax.Text

            cmm.Parameters.Add("@mahaleEgamat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mahaleEgamat").Value = mahaleEgamat.Text

            cmm.Parameters.Add("@malekiatType", SqlDbType.NVarChar, 50)
            cmm.Parameters("@malekiatType").Value = malekiatType.SelectedItem.Value

            cmm.Parameters.Add("@shHesab", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shHesab").Value = shHesab.Text

            cmm.Parameters.Add("@shShaba", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shShaba").Value = shShaba.Text

            cmm.Parameters.Add("@bank", SqlDbType.NVarChar, 50)
            cmm.Parameters("@bank").Value = bank.Text

            cmm.Parameters.Add("@shobe", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shobe").Value = shobe.Text

            cmm.Parameters.Add("@codeShobe", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codeShobe").Value = codeShobe.Text

            cmm.Parameters.Add("@namMoseseSahebHesab", SqlDbType.NVarChar, 50)
            cmm.Parameters("@namMoseseSahebHesab").Value = namMoseseSahebHesab.Text

            cmm.Parameters.Add("@modirAmel", SqlDbType.NVarChar, 50)
            cmm.Parameters("@modirAmel").Value = modirAmel.Text

            cmm.Parameters.Add("@hosnHamkari", SqlDbType.NVarChar, 50)
            cmm.Parameters("@hosnHamkari").Value = hosnHamkari.Text


            cmm.Parameters.Add("@natayejArzyabiTamin", SqlDbType.NVarChar, 500)
            cmm.Parameters("@natayejArzyabiTamin").Value = natayejArzyabiTamin.Text

            cmm.Parameters.Add("@natayejArzyabiGabli", SqlDbType.NVarChar, 500)
            cmm.Parameters("@natayejArzyabiGabli").Value = natayejArzyabiGabli.Text


            cmm.Parameters.Add("@CodeClosed", SqlDbType.Bit, 1)
            cmm.Parameters("@CodeClosed").Value = ChbActiveCode.Checked



            cmm.Parameters.Add("@userid", SqlDbType.Int, 5)
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
                Call save_()
                load_grid()

                msg_box(" عملیات با موفقیت انجام شد")

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New trnTechersOutCls
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)

            HiddenField1.Value = dt.Rows(0).Item("techID").ToString
            nam.Text = dt.Rows(0).Item("nam").ToString
            codemeli.Text = dt.Rows(0).Item("codemeli").ToString
            codeEgtesadi.Text = dt.Rows(0).Item("codeEgtesadi").ToString
            shenasemeli.Text = dt.Rows(0).Item("shenasemeli").ToString
            sabtNo.Text = dt.Rows(0).Item("sabtNo").ToString
            codePosti.Text = dt.Rows(0).Item("codePosti").ToString
            tel.Text = dt.Rows(0).Item("tel").ToString
            fax.Text = dt.Rows(0).Item("fax").ToString
            mahaleEgamat.Text = dt.Rows(0).Item("mahaleEgamat").ToString
            malekiatType.SelectedItem.Text = dt.Rows(0).Item("malekiatType").ToString
            shHesab.Text = dt.Rows(0).Item("shHesab").ToString
            shShaba.Text = dt.Rows(0).Item("shShaba").ToString
            bank.Text = dt.Rows(0).Item("bank").ToString
            shobe.Text = dt.Rows(0).Item("shobe").ToString
            codeShobe.Text = dt.Rows(0).Item("codeShobe").ToString
            namMoseseSahebHesab.Text = dt.Rows(0).Item("namMoseseSahebHesab").ToString
            modirAmel.Text = dt.Rows(0).Item("modirAmel").ToString
            hosnHamkari.Text = dt.Rows(0).Item("hosnHamkari").ToString
            natayejArzyabiTamin.Text = dt.Rows(0).Item("natayejArzyabiTamin").ToString
            natayejArzyabiGabli.Text = dt.Rows(0).Item("natayejArzyabiGabli").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
        End If
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
