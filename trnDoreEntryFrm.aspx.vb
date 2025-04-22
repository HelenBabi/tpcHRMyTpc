Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO

Partial Class trnDoreEntryFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()
        End If
        load_grid()
        'load_grid_File()
    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnDoreEntryFrm.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)

        ibtnDeactived.Visible = meno_arr(9)

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

            ChbActiveCode.Enabled = True
            idd.Enabled = False
            onvan.Enabled = True
            dorehayeGozrande.Enabled = True
            mojriDore.Enabled = True
            mahalEjra.Enabled = True
            saat.Enabled = True
            RUZ.Enabled = True
            hamahangiEjra.Enabled = True
           
            tarixPayan.Enabled = True
            tarixShoru.Enabled = True
            codePersoneli.Enabled = True
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

            onvan.Enabled = False
            dorehayeGozrande.Enabled = False
            mojriDore.Enabled = False
            mahalEjra.Enabled = False
            saat.Enabled = False
            RUZ.Enabled = False
            hamahangiEjra.Enabled = False

            tarixPayan.Enabled = False
            tarixShoru.Enabled = False
            codePersoneli.Enabled = False

        End If

    End Sub

    Private Sub clear_form2()

        'dorehayeGozrande.Text = ""
        'mojriDore.Text = ""
        'mahalEjra.Text = ""
        'saat.Text = ""
        'RUZ.Text = ""
        'hamahangiEjra.Text = ""
        codePersoneli.Text = ""
        'tarixPayan.Text = ""
        'tarixShoru.Text = ""
    End Sub
    Private Sub clear_form()

        dorehayeGozrande.Text = ""
        mojriDore.Text = ""
        mahalEjra.Text = ""
        saat.Text = ""
        RUZ.Text = ""
        hamahangiEjra.Text = ""
        codePersoneli.Text = ""
        tarixPayan.Text = ""
        tarixShoru.Text = ""
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
        idd.Text = ClsGetMaxId.GetMaxId("trnPersonelDoreTbl", "idd")
        enable_disable()
    End Sub
    Private Sub save_()
        enable_disable()
        clear_form2()
        hdn_Final_key.Value = 0
    End Sub

    Protected Sub ibtnSaveNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSaveNew.Click
        Call save_final()
        hdn_Final_key.Value = "new"
        clear_form2()
        idd.Text = ClsGetMaxId.GetMaxId("trnPersonelDoreTbl", "idd")
        enable_disable()
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
        Dim ClsB_ As New trnBarnameriziCls
        Dim dt As New DataTable
        dt = ClsB_.GetPersonelDoreAll(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New trnBarnameriziCls
            Dim dt As New DataTable
            dt = ClsB.GetPersonelDore(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            idd.Text = dt.Rows(0).Item("idd").ToString
            onvan.SelectedItem.Text = dt.Rows(0).Item("onvan").ToString
            dorehayeGozrande.Text = dt.Rows(0).Item("dorehayeGozrande").ToString
            mojriDore.Text = dt.Rows(0).Item("mojriDore").ToString
            mahalEjra.Text = dt.Rows(0).Item("mahalEjra").ToString
            saat.Text = dt.Rows(0).Item("saat").ToString
            RUZ.Text = dt.Rows(0).Item("RUZ").ToString
            hamahangiEjra.Text = dt.Rows(0).Item("hamahangiEjra").ToString

            tarixPayan.Text = dt.Rows(0).Item("tarixPayan").ToString
            tarixShoru.Text = dt.Rows(0).Item("tarixShoru").ToString
            codePersoneli.Text = dt.Rows(0).Item("codePersoneli").ToString
        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then idd.Text = ClsGetMaxId.GetMaxId("trnPersonelDoreTbl", "idd")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnDorePersonelPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@idd", SqlDbType.Int, 6)
            cmm.Parameters("@idd").Value = idd.Text

            cmm.Parameters.Add("@codePersoneli", SqlDbType.BigInt, 6)
            cmm.Parameters("@codePersoneli").Value = codePersoneli.Text


            cmm.Parameters.Add("@onvan", SqlDbType.NVarChar, 250)
            cmm.Parameters("@onvan").Value = onvan.SelectedItem.Text

            cmm.Parameters.Add("@dorehayeGozrande", SqlDbType.NVarChar, 500)
            cmm.Parameters("@dorehayeGozrande").Value = dorehayeGozrande.Text

            cmm.Parameters.Add("@mojriDore", SqlDbType.NVarChar, 500)
            cmm.Parameters("@mojriDore").Value = mojriDore.Text

            cmm.Parameters.Add("@mahalEjra", SqlDbType.NVarChar, 500)
            cmm.Parameters("@mahalEjra").Value = mahalEjra.Text

            cmm.Parameters.Add("@saat", SqlDbType.Int, 5)
            cmm.Parameters("@saat").Value = Val(saat.Text)

            cmm.Parameters.Add("@RUZ", SqlDbType.Int, 5)
            cmm.Parameters("@RUZ").Value = Val(RUZ.Text)

            cmm.Parameters.Add("@hamahangiEjra", SqlDbType.NVarChar, 500)
            cmm.Parameters("@hamahangiEjra").Value = hamahangiEjra.Text

            cmm.Parameters.Add("@userid", SqlDbType.Int)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@tarixPayan", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixPayan").Value = tarixPayan.Text

            cmm.Parameters.Add("@tarixShoru", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixShoru").Value = tarixShoru.Text


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

                Call save_()
               
                msg_box("  عملیات با موفقیت انجام شد ")
                load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub




    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")

    End Sub




End Class
