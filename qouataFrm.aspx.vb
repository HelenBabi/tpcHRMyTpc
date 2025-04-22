Imports System.Data
Imports System.Data.SqlClient

Partial Class qouataFrm
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
    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("qouataFrm.aspx", Val(Session("UserId")))
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
            TypeName.Enabled = True
            ChbActiveCode.Enabled = True
            TypeID.Enabled = False
            TypeSahm1.Enabled = True
            TypeSahmDateStart1.Enabled = True
            TypeSahmDateEnd1.Enabled = True
            TypeSahm2.Enabled = True
            TypeSahmDateStart2.Enabled = True
            TypeSahmDateEnd2.Enabled = True
            TypeSahm3.Enabled = True
            TypeSahmDateStart3.Enabled = True
            TypeSahmDateEnd3.Enabled = True
            TypeSahm4.Enabled = True
            TypeSahmDateStart4.Enabled = True
            TypeSahmDateEnd4.Enabled = True
            TypeID.Enabled = True
        Else
            TypeID.Enabled = False
            TypeSahm1.Enabled = False
            TypeSahmDateStart1.Enabled = False
            TypeSahmDateEnd1.Enabled = False
            TypeSahm2.Enabled = False
            TypeSahmDateStart2.Enabled = False
            TypeSahmDateEnd2.Enabled = False
            TypeSahm3.Enabled = False
            TypeSahmDateStart3.Enabled = False
            TypeSahmDateEnd3.Enabled = False
            TypeSahm4.Enabled = False
            TypeSahmDateStart4.Enabled = False
            TypeSahmDateEnd4.Enabled = False

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
            TypeName.Enabled = False
            ChbActiveCode.Enabled = False
            ' txt_BakhshCode.Enabled = True
        End If

    End Sub

    Private Sub clear_form()
        TypeID.Text = ""
        TypeName.Text = ""
        ChbActiveCode.Checked = False
        TypeSahm1.Text = ""
        TypeSahmDateStart1.Text = ""
        TypeSahmDateEnd1.Text = ""
        TypeSahm2.Text = ""
        TypeSahmDateStart2.Text = ""
        TypeSahmDateEnd2.Text = ""
        TypeSahm3.Text = ""
        TypeSahmDateStart3.Text = ""
        TypeSahmDateEnd3.Text = ""
        TypeSahm4.Text = ""
        TypeSahmDateStart4.Text = ""
        TypeSahmDateEnd4.Text = ""
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
        ' TypeID.Text = ClsGetMaxId.GetMaxId("tblUnits", "UnitsCode")
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
        Dim ClsB_ As New typeQouataCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New typeQouataCls
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            TypeID.Text = dt.Rows(0).Item("id").ToString
            TypeName.Text = dt.Rows(0).Item("value").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString

            TypeSahm1.Text = dt.Rows(0).Item("TypeSahm1").ToString
            TypeSahmDateStart1.Text = dt.Rows(0).Item("TypeSahmDateStart1").ToString
            TypeSahmDateEnd1.Text = dt.Rows(0).Item("TypeSahmDateEnd1").ToString
            TypeSahm2.Text = dt.Rows(0).Item("TypeSahm2").ToString
            TypeSahmDateStart2.Text = dt.Rows(0).Item("TypeSahmDateStart2").ToString
            TypeSahmDateEnd2.Text = dt.Rows(0).Item("TypeSahmDateEnd2").ToString
            TypeSahm3.Text = dt.Rows(0).Item("TypeSahm3").ToString
            TypeSahmDateStart3.Text = dt.Rows(0).Item("TypeSahmDateStart3").ToString
            TypeSahmDateEnd3.Text = dt.Rows(0).Item("TypeSahmDateEnd3").ToString
            TypeSahm4.Text = dt.Rows(0).Item("TypeSahm4").ToString
            TypeSahmDateStart4.Text = dt.Rows(0).Item("TypeSahmDateStart4").ToString
            TypeSahmDateEnd4.Text = dt.Rows(0).Item("TypeSahmDateEnd4").ToString
        End If
    End Sub


    Private Sub save_final()
        '  If hdn_Final_key.Value = "new" Then TypeID.Text = ClsGetMaxId.GetMaxId("Type", "TypeID")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "typeQouataPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@TypeID", SqlDbType.Int, 5)
            cmm.Parameters("@TypeID").Value = TypeID.Text

            cmm.Parameters.Add("@TypeName", SqlDbType.NVarChar, 100)
            cmm.Parameters("@TypeName").Value = TypeName.Text

            cmm.Parameters.Add("@TypeSahmDateEnd4", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateEnd4").Value = TypeSahmDateEnd4.Text


            cmm.Parameters.Add("@TypeSahmDateStart4", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateStart4").Value = TypeSahmDateStart4.Text

            cmm.Parameters.Add("@TypeSahm4", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahm4").Value = TypeSahm4.Text


            cmm.Parameters.Add("@TypeSahmDateEnd3", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateEnd3").Value = TypeSahmDateEnd3.Text


            cmm.Parameters.Add("@TypeSahmDateStart3", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateStart3").Value = TypeSahmDateStart3.Text

            cmm.Parameters.Add("@TypeSahm3", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahm3").Value = TypeSahm3.Text

            cmm.Parameters.Add("@TypeSahmDateEnd2", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateEnd2").Value = TypeSahmDateEnd2.Text


            cmm.Parameters.Add("@TypeSahmDateStart2", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateStart2").Value = TypeSahmDateStart2.Text

            cmm.Parameters.Add("@TypeSahm2", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahm2").Value = TypeSahm2.Text

            cmm.Parameters.Add("@TypeSahmDateEnd1", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateEnd1").Value = TypeSahmDateEnd1.Text


            cmm.Parameters.Add("@TypeSahmDateStart1", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahmDateStart1").Value = TypeSahmDateStart1.Text

            cmm.Parameters.Add("@TypeSahm1", SqlDbType.Int, 5)
            cmm.Parameters("@TypeSahm1").Value = TypeSahm1.Text

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
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub




    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
      
            Response.Redirect("index.aspx")
     
    End Sub


End Class
