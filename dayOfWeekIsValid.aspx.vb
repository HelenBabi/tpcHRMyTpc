Imports System.Data
Imports System.Data.SqlClient

Partial Class dayOfWeekIsValid
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            load_Resturan()

            load_Timesall()
        End If
     
        load_grid()
    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("dayOfWeekIsValid.aspx", Val(Session("UserId")))
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

            ChbActiveCode.Enabled = True
            idd.Enabled = False
            resturanCmb.Enabled = True
            saatCmb.Enabled = True
            ruzcmb.Enabled = True
        Else

            resturanCmb.Enabled = False
            saatCmb.Enabled = False
            ruzcmb.Enabled = False
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
            ChbActiveCode.Enabled = False
            ' txt_BakhshCode.Enabled = True
        End If

    End Sub

    Private Sub clear_form()
        resturanCmb.SelectedValue = 1
        'saatCmb.SelectedValue = 0
        ruzcmb.SelectedValue = 0
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
        idd.Text = ClsGetMaxId.GetMaxId("Hafteh", "HaftehID")
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

    Private Sub load_Times()
        Dim dt As New DataTable
        Dim roozHafteh As Integer
        roozHafteh = ClsDateTime.GetRoozHafteAdad()

        Dim sqlstr As String
        sqlstr = " SELECT timeID, valuee  FROM  barnameTbl " & _
        " WHERE (CodeClosed = 0)  and (resturanID = " & resturanCmb.SelectedValue & ")  "

        dt = ClsDataTable.GetDataTable(sqlstr)
       
        saatCmb.DataSource = dt
        saatCmb.DataValueField = "timeID"
        saatCmb.DataTextField = "valuee"
        saatCmb.DataBind()
    End Sub


    Private Sub load_Timesall()
        Dim dt As New DataTable
        Dim roozHafteh As Integer
        roozHafteh = ClsDateTime.GetRoozHafteAdad()

        Dim sqlstr As String
        sqlstr = " SELECT timeID, valuee  FROM  barnameTbl " & _
        " WHERE (CodeClosed = 0)  "

        dt = ClsDataTable.GetDataTable(sqlstr)

        saatCmb.DataSource = dt
        saatCmb.DataValueField = "timeID"
        saatCmb.DataTextField = "valuee"
        saatCmb.DataBind()
    End Sub


    Private Sub load_Resturan()
        Dim dt As New DataTable

        Dim sqlstr As String
       sqlstr = "SELECT        locID, locationValue, stat        FROM refahiLocationTbl WHERE        (stat = 1) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        resturanCmb.DataSource = dt
        resturanCmb.DataValueField = "locID"
        resturanCmb.DataTextField = "locationValue"
        resturanCmb.DataBind()
    End Sub
    Private Sub load_grid()
        Dim ClsB_ As New ayamCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ayamCls
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            idd.Text = dt.Rows(0).Item("HaftehID").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("IsActive").ToString
            ruzcmb.SelectedValue = dt.Rows(0).Item("HaftehRooz").ToString
            resturanCmb.SelectedValue = dt.Rows(0).Item("resturanID").ToString
            saatCmb.SelectedValue = dt.Rows(0).Item("timeID").ToString

        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then idd.Text = ClsGetMaxId.GetMaxId("Hafteh", "HaftehID")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "ayamPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@HaftehID", SqlDbType.Int, 6)
            cmm.Parameters("@HaftehID").Value = idd.Text

            cmm.Parameters.Add("@HaftehRooz", SqlDbType.Int, 5)
            cmm.Parameters("@HaftehRooz").Value = ruzcmb.SelectedValue


            cmm.Parameters.Add("@timeID", SqlDbType.Int, 5)
            cmm.Parameters("@timeID").Value = saatCmb.SelectedValue

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


    Protected Sub resturanCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles resturanCmb.SelectedIndexChanged
        load_Times()
    End Sub

    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("resturanID"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightSeaGreen

        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightGreen

        End If


    End Sub
End Class
