Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors


Partial Class EditHistory
    Inherits System.Web.UI.Page
    Dim warning_pic As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()
            load_Sazman()
            load_cmbShogl()

        End If
        If Me.IsCallback = False Then
            load_grid()
        End If

    End Sub

    Private Sub load_Sazman()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM   sazman order by sazmanName"
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbShoghl.DataSource = dt
        cmbShoghl.DataValueField = "id"
        cmbShoghl.DataTextField = "sazmanName"
        cmbShoghl.DataBind()

    End Sub
    Private Sub load_cmbShogl()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM   mashaghel order by postname"
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbSazman.DataSource = dt
        cmbSazman.DataValueField = "id"
        cmbSazman.DataTextField = "postname"
        cmbSazman.DataBind()

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

            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            ibtnDeleted.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False
            ibtnExit.Enabled = False
            codepersonel.Enabled = True

            enddate.Enabled = True

            startdate.Enabled = True

            modat.Enabled = True
  
            codepersonel.Enabled = True

            enddate.Enabled = True

            cmbShoghl.Enabled = True
            cmbSazman.Enabled = True

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
          
        Else
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

            codepersonel.Enabled = False
           
            enddate.Enabled = False

            startdate.Enabled = False
            modat.Enabled = False

            codepersonel.Enabled = False

            enddate.Enabled = False

            cmbShoghl.Enabled = False
            cmbSazman.Enabled = False

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
          
        End If

    End Sub

    Private Sub clear_form()


        codepersonel.Text = ""
     
        enddate.Text = ""


        startdate.Text = ""
        modat.Text = ""



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
        idd.Text = ClsGetMaxId.GetMaxId("tblJob", "id_job")
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
        Dim sqlstr As String
        sqlstr = "SELECT    personel.codePersoneli, savabeg.id, savabeg.tarixshoru, savabeg.tarixpayan, CAST(savabeg.karkardSal AS CHAR) + '-' + CAST(savabeg.karkardMah  AS CHAR) + '-' + CAST(savabeg.karkardDay   AS CHAR) AS karkard, " & _
                 "  CAST(savabeg.sabegeSal  AS CHAR) + '-' + CAST(savabeg.sabegeMah  AS CHAR) + '-' + CAST(savabeg.sabegeDay  AS CHAR) AS sabege, savabeg.tozih, mashaghel.postname, sazman.sazmanName " & _
              " FROM         savabeg INNER JOIN " & _
                  " mashaghel ON savabeg.onvan = mashaghel.id INNER JOIN " & _
                  " personel ON savabeg.perID = personel.ID INNER JOIN " & _
                  " sazman ON savabeg.sazman = sazman.id "
        '"  WHERE(personel.codePersoneli = " & codepersonel.Text & ") order by savabeg.tarixshoru"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ClsJob
            Dim dt As New DataTable
            Dim sqlstr As String = "SELECT     personel.nam + ' ' + personel.family AS nam, personel.codePersoneli, savabeg.*, savabeg.id AS idd,CAST(savabeg.karkardSal AS CHAR) + '-' + CAST(savabeg.karkardMah  AS CHAR) + '-' + CAST(savabeg.karkardDay   AS CHAR) AS karkard, CAST(savabeg.sabegeSal  AS CHAR) + '-' + CAST(savabeg.sabegeMah  AS CHAR) + '-' + CAST(savabeg.sabegeDay  AS CHAR) AS sabege" & _
                      " FROM         personel INNER JOIN " & _
                     " savabeg ON personel.ID = savabeg.perID " & _
                  " WHERE     (savabeg.id = " & e.CommandArgs.CommandArgument.ToString() & ")"
            ' dt = ClsB.GetJob(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            dt = ClsDataTable.GetDataTable(sqlstr)

            idd.Text = dt.Rows(0).Item("idd").ToString
            codepersonel.Text = dt.Rows(0).Item("codePersoneli").ToString
            startdate.Text = dt.Rows(0).Item("tarixshoru").ToString
            enddate.Text = dt.Rows(0).Item("tarixpayan").ToString
            modat.Text = dt.Rows(0).Item("time_end").ToString
            sabege.Text = dt.Rows(0).Item("sabege").ToString
            modat.Text = dt.Rows(0).Item("karkard").ToString
            txtTozih.Text = dt.Rows(0).Item("tozih").ToString
            cmbSazman.SelectedValue = dt.Rows(0).Item("sazman").ToString
            cmbShoghl.SelectedValue = dt.Rows(0).Item("onvan").ToString
        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then idd.Text = ClsGetMaxId.GetMaxId("tblJob", "id_job")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        cmm.CommandText = "PrcJobAdmin"
        cmm.CommandType = CommandType.StoredProcedure

       

      
        cmm.Parameters.Add("@subject", SqlDbType.VarChar, 150)
        cmm.Parameters("@subject").Value = codepersonel.Text

       

        cmm.Parameters.Add("@date_start", SqlDbType.VarChar, 10)
        cmm.Parameters("@date_start").Value = startdate.Text

       
        cmm.Parameters.Add("@date_end", SqlDbType.VarChar, 10)
        cmm.Parameters("@date_end").Value = enddate.Text

        cmm.Parameters.Add("@time_end", SqlDbType.VarChar, 6)
        cmm.Parameters("@time_end").Value = modat.Text

        cmm.Parameters.Add("@note", SqlDbType.NVarChar, 250)
        cmm.Parameters("@note").Value = txtTozih.Text

        cmm.Parameters.Add("@sender", SqlDbType.Int, 5)
        cmm.Parameters("@sender").Value = cmbSazman.SelectedValue

        cmm.Parameters.Add("@reciver", SqlDbType.Int, 5)
        cmm.Parameters("@reciver").Value = cmbShoghl.SelectedValue



        cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
        cmm.Parameters("@userid").Value = Session("UserId")

      

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

   

End Class
