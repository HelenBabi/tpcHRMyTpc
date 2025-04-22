Imports System.Data
Imports System.Data.SqlClient
Partial Class employTakafolFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            load_nesbat()
        End If
        load_grid()
    End Sub
    Private Sub load_nesbat()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     ID, Name         FROM Relations WHERE     (CodeClosed = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        nesbat.DataSource = dt
        nesbat.DataValueField = "ID"
        nesbat.DataTextField = "Name"
        nesbat.DataBind()
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("employTakafolFrm.aspx", Val(Session("UserId")))
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
            nam.Enabled = True
            famili.Enabled = True
            codpersoneli.Enabled = True
          
            tarixetebar.Enabled = True
            tarixTavlod.Enabled = True
            nesbat.Enabled = True
           
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

            ASPxGrvHead.Enabled = True

            ChbActiveCode.Enabled = False
            nam.Enabled = False
            famili.Enabled = False
            codpersoneli.Enabled = False
          
            tarixetebar.Enabled = False
            tarixTavlod.Enabled = False
            nesbat.Enabled = False
          
        End If

    End Sub

    Private Sub clear_form()
        codTxt.Text = ""
        nam.Text = ""
        famili.Text = ""
        codpersoneli.Text = ""
     
        tarixetebar.Text = ""
        tarixTavlod.Text = ""
        nesbat.SelectedValue = 0
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
        codTxt.Text = ClsGetMaxId.GetMaxId("Sponsorship", "id")
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
        Dim ClsB_ As New employTakafolCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New employTakafolCls
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            codTxt.Text = dt.Rows(0).Item("ID").ToString
            nam.Text = dt.Rows(0).Item("fstName").ToString
            famili.Text = dt.Rows(0).Item("name").ToString
            codpersoneli.Text = dt.Rows(0).Item("empID").ToString
            tarixetebar.Text = dt.Rows(0).Item("credit").ToString
            tarixTavlod.Text = dt.Rows(0).Item("birthday").ToString
            nesbat.SelectedValue = dt.Rows(0).Item("relation").ToString

            ChbActiveCode.Checked = dt.Rows(0).Item("IsActive").ToString
        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then codTxt.Text = ClsGetMaxId.GetMaxId("Sponsorship", "id")
        'Dim perid As Integer=
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "employTakafolPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@id", SqlDbType.Int)
            cmm.Parameters("@id").Value = codTxt.Text

            cmm.Parameters.Add("@empID", SqlDbType.NVarChar, 15)
            cmm.Parameters("@empID").Value = codpersoneli.Text

           

            cmm.Parameters.Add("@fstName", SqlDbType.NVarChar, 50)
            cmm.Parameters("@fstName").Value = nam.Text

            cmm.Parameters.Add("@name", SqlDbType.NVarChar, 50)
            cmm.Parameters("@name").Value = famili.Text

            cmm.Parameters.Add("@relation", SqlDbType.Int)
            cmm.Parameters("@relation").Value = nesbat.SelectedValue


            cmm.Parameters.Add("@credit", SqlDbType.NVarChar, 10)
            cmm.Parameters("@credit").Value = tarixetebar.Text

            cmm.Parameters.Add("@birthday", SqlDbType.NVarChar, 10)
            cmm.Parameters("@birthday").Value = tarixTavlod.Text

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@IsActive", SqlDbType.Bit, 1)
            cmm.Parameters("@IsActive").Value = ChbActiveCode.Checked

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
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub




    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")

    End Sub
End Class
