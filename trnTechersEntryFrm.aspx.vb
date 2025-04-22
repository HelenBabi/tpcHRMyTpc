Imports System.Data
Imports System.Data.SqlClient
Partial Class trnTechersEntryFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            load_cmbGender()
            load_cmbType()
        End If
        load_grid()

    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnTechersEntryFrm.aspx", Val(Session("UserId")))
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
        Dim ClsB_ As New trnTechersCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub


    Private Sub load_cmbType()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) techTypeID, techTypename FROM            trnTecherTypeTbl"
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbTechType.DataSource = dt
        cmbTechType.DataTextField = "techTypename"
        cmbTechType.DataValueField = "techTypeID"
        cmbTechType.DataBind()

    End Sub
    Private Sub load_cmbGender()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) idd, jens FROM            jensiat"
        dt = ClsDataTable.GetDataTable(sqlstr)
        jens.DataSource = dt
        jens.DataTextField = "jens"
        jens.DataValueField = "idd"
        jens.DataBind()

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
            HiddenField1.Value = ClsGetMaxId.GetMaxId("trnTechersTbl", "techID")
        End If



        'If hdn_Final_key.Value = "new" Then txtTicketId.Text = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "trnTecherssPrc"
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

            Dim moaadel As String = moadel2.Text + "/" + moadel1.Text
            cmm.Parameters.Add("@moadel", SqlDbType.VarChar, 50)
            cmm.Parameters("@moadel").Value = moaadel

            cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nam").Value = nam.Text

            cmm.Parameters.Add("@family", SqlDbType.NVarChar, 50)
            cmm.Parameters("@family").Value = family.Text

            cmm.Parameters.Add("@mahalEshtegal", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mahalEshtegal").Value = mahalEshtegal.Text

            cmm.Parameters.Add("@semat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@semat").Value = semat.Text

            cmm.Parameters.Add("@father", SqlDbType.NVarChar, 50)
            cmm.Parameters("@father").Value = father.Text

            cmm.Parameters.Add("@shsh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shsh").Value = shsh.Text

            cmm.Parameters.Add("@codemeli", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codemeli").Value = codemeli.Text

            cmm.Parameters.Add("@tarixStexdam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixStexdam").Value = tarixStexdam.Text

            cmm.Parameters.Add("@sabege", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sabege").Value = sabege.Text

            cmm.Parameters.Add("@omur", SqlDbType.NVarChar, 50)
            cmm.Parameters("@omur").Value = omur.Text

            cmm.Parameters.Add("@Unit", SqlDbType.NVarChar, 50)
            cmm.Parameters("@Unit").Value = Unit.Text

            cmm.Parameters.Add("@mobil", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mobil").Value = mobil.Text

            cmm.Parameters.Add("@daxeli", SqlDbType.NVarChar, 50)
            cmm.Parameters("@daxeli").Value = daxeli.Text

            cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
            cmm.Parameters("@madrak").Value = madrak.Text

            cmm.Parameters.Add("@reshte", SqlDbType.NVarChar, 50)
            cmm.Parameters("@reshte").Value = reshte.Text

            cmm.Parameters.Add("@gerayesh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@gerayesh").Value = gerayesh.Text

            cmm.Parameters.Add("@uni", SqlDbType.NVarChar, 50)
            cmm.Parameters("@uni").Value = uni.Text

            cmm.Parameters.Add("@tarixTavalod", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixTavalod").Value = tarixTavalod.Text

            cmm.Parameters.Add("@moaref", SqlDbType.NVarChar, 50)
            cmm.Parameters("@moaref").Value = moaref.Text

            cmm.Parameters.Add("@maharatha", SqlDbType.NVarChar, 500)
            cmm.Parameters("@maharatha").Value = maharatha.Text

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar, 500)
            cmm.Parameters("@notee").Value = notee.Text

            cmm.Parameters.Add("@adres", SqlDbType.NVarChar, 500)
            cmm.Parameters("@adres").Value = address.Text

            cmm.Parameters.Add("@genderTypeID", SqlDbType.Int, 5)
            cmm.Parameters("@genderTypeID").Value = jens.SelectedValue

            cmm.Parameters.Add("@codePersoneli", SqlDbType.Int, 5)
            cmm.Parameters("@codePersoneli").Value = codePersoneli.Text

            cmm.Parameters.Add("@CodeClosed", SqlDbType.Bit, 1)
            cmm.Parameters("@CodeClosed").Value = ChbActiveCode.Checked

            cmm.Parameters.Add("@sherkati", SqlDbType.Bit, 1)
            cmm.Parameters("@sherkati").Value = CheckBox1.Checked

            cmm.Parameters.Add("@userid", SqlDbType.Int, 5)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@techTypeID", SqlDbType.Int, 5)
            cmm.Parameters("@techTypeID").Value = cmbTechType.SelectedValue

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
            Dim ClsB As New trnTechersCls
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            HiddenField1.Value = dt.Rows(0).Item("techID").ToString
            nam.Text = dt.Rows(0).Item("nam").ToString
            family.Text = dt.Rows(0).Item("family").ToString
            mahalEshtegal.Text = dt.Rows(0).Item("mahalEshtegal").ToString
            semat.Text = dt.Rows(0).Item("semat").ToString
            father.Text = dt.Rows(0).Item("father").ToString
            shsh.Text = dt.Rows(0).Item("shsh").ToString
            codemeli.Text = dt.Rows(0).Item("codemeli").ToString
            tarixStexdam.Text = dt.Rows(0).Item("tarixStexdam").ToString
            sabege.Text = dt.Rows(0).Item("sabege").ToString
            omur.Text = dt.Rows(0).Item("omur").ToString
            Unit.Text = dt.Rows(0).Item("Unit").ToString
            daxeli.Text = dt.Rows(0).Item("daxeli").ToString
            mobil.Text = dt.Rows(0).Item("mobil").ToString
            madrak.Text = dt.Rows(0).Item("madrak").ToString
            reshte.Text = dt.Rows(0).Item("reshte").ToString
            gerayesh.Text = dt.Rows(0).Item("gerayesh").ToString
            uni.Text = dt.Rows(0).Item("uni").ToString
            tarixTavalod.Text = dt.Rows(0).Item("tarixTavalod").ToString
            moadel1.Text = dt.Rows(0).Item("moadel").ToString
            moaref.Text = dt.Rows(0).Item("moaref").ToString
            maharatha.Text = dt.Rows(0).Item("maharatha").ToString
            notee.Text = dt.Rows(0).Item("notee").ToString
            jens.SelectedValue = dt.Rows(0).Item("genderTypeID").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
            cmbTechType.SelectedValue = dt.Rows(0).Item("techTypeID").ToString
            CheckBox1.Checked = dt.Rows(0).Item("sherkati").ToString
            codePersoneli.Text = dt.Rows(0).Item("codePersoneli").ToString
        End If
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
