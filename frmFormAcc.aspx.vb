Imports System.Data
Imports System.Data.SqlClient

Partial Class frmFormAcc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        If Me.IsPostBack = False Then
            load_users_reciver()
            load_FrmUsers()
            load_Forms()
        End If

     

    End Sub
    Private Sub load_Forms()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT        tblForm.FrmID, tblForm.FrmName, tblForm.FrmCaption " & _
           " FROM            tblForm INNER JOIN   tblFormAcc ON tblForm.FrmID = tblFormAcc.FrmID  " & _
                  " WHERE  (tblForm.Frm_level > 1) "
        dt = ClsDataTable.GetDataTable(sqlstr)

        CmbForms.DataSource = dt
        CmbForms.DataTextField = "FrmCaption"
        CmbForms.DataValueField = "FrmID"
        CmbForms.DataBind()

    End Sub


    Private Sub load_FrmUsers()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT    tblForm.FrmID, tblForm.FrmName, tblForm.FrmCaption " & _
           " FROM            tblForm INNER JOIN   tblFormAcc ON tblForm.FrmID = tblFormAcc.FrmID  " & _
                  " WHERE  (tblForm.Frm_level > 1) AND (tblFormAcc.UserCode = " & cmbreciver.SelectedValue & ") "
        dt = ClsDataTable.GetDataTable(sqlstr)


        CmbFormUser.DataSource = dt
        CmbFormUser.DataTextField = "FrmCaption"
        CmbFormUser.DataValueField = "FrmID"
        CmbFormUser.DataBind()

    End Sub
    Private Sub load_users_reciver()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = " select UserCode,UserLastName +' '+ UserFamili as nam from tblUsers where SysUser=1 and codeclosed=0 order by UserFamili "
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbreciver.DataSource = dt
        cmbreciver.DataValueField = "UserCode"
        cmbreciver.DataTextField = "nam"
        cmbreciver.DataBind()

    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmPcEditHardware.aspx", Val(Session("UserId")))
    
    End Sub

 

    Private Sub clear_form()
       
        CmbFormUser.DataBind()
        GridView2.DataBind()



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
#Region "load fitment"

    Private Function fillDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     PcUserCode, PcUserIdDomain, CodeClosed, opid, ArcTypeCode  FROM tblBaseDomainUser WHERE     (CodeClosed = 0) AND (BaseSystemHDtlCode <> 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function


#End Region


#Region "meno button"
   

   

#End Region





    Private Sub save_final()
        '  If hdn_Final_key.Value = "new" Then txt_BakhshCode.Text = ClsGetMaxId.GetMaxId("tblBakhsh", "BakhshCode")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcEditPcHardware"
            cmm.CommandType = CommandType.StoredProcedure


            'cmm.Parameters.Add("@FrmID", SqlDbType.Int, 10)
            'cmm.Parameters("@FrmID").Value = txt_FrmID.Text



            cmm.Parameters.Add("@TypeOpCode", SqlDbType.SmallInt, 6)
            cmm.Parameters("@TypeOpCode").Value = 0


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")


            cmm.Parameters.Add("@has_g", SqlDbType.Int, 10)
            cmm.Parameters("@has_g").Value = hdn_tmp.Value


            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  عملیات با موفقیت انجام شد ")
                'Call save_()

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub





    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)
        Dim note As String
        Dim basesys As Int64
        Dim baseMod As Int64
        ' Dim id_rec As Int64


        If INOUT = False Then
            note = CmbFormUser.SelectedItem.Text
            basesys = 0
            baseMod = 0
        Else
            note = ""
            basesys = Val(CmbForms.SelectedValue)
            'baseMod = Val(Cmbgd.SelectedValue)
        End If


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTmpFrmAcc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@TmpInOutId", SqlDbType.Int, 10)
            cmm.Parameters("@TmpInOutId").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@id_rec", SqlDbType.Int, 10)
            cmm.Parameters("@id_rec").Value = Val(CmbFormUser.SelectedValue)

            cmm.Parameters.Add("@formId", SqlDbType.SmallInt, 6)
            cmm.Parameters("@formId").Value = basesys

            cmm.Parameters.Add("@id_rec_note", SqlDbType.NVarChar, 200)
            cmm.Parameters("@id_rec_note").Value = note

            cmm.Parameters.Add("@UserCode", SqlDbType.Int, 10)
            cmm.Parameters("@UserCode").Value = Session("UserId")

            cmm.Parameters.Add("@INOUT", SqlDbType.Bit, 1)
            cmm.Parameters("@INOUT").Value = INOUT

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  عملیات با موفقیت انجام شد ")
                '  Call save_()
                ' load_grid()

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub
    Protected Sub btnOut_Click(sender As Object, e As System.EventArgs) Handles btnOut.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTempFrmAcc", "TmpInOutId")
        End If
        save_tmp(False, False)
        GridView2.Visible = True
        load_grid_tmp()
    End Sub
    Private Sub load_grid_tmp()
        Dim sqlstr As String = "SELECT      tblTempFrmAcc.id_rec_note,  tblTmpInOutType.INOUTNOTE, tblTempFrmAcc.Tmp_id, tblTempFrmAcc.TmpInOutId, tblTempFrmAcc.id_rec, tblTempFrmAcc.FrmID, tblTempFrmAcc.INOUT, tblTempFrmAcc.UserCode, tblForm.FrmCaption " & _
  " FROM            tblTmpInOutType INNER JOIN " & _
                        " tblTempFrmAcc ON tblTmpInOutType.INOUT = tblTempFrmAcc.INOUT INNER JOIN " & _
                        " tblForm ON tblTempFrmAcc.FrmID = tblForm.FrmID " & _
" WHERE     (tblTempFrmAcc.UserCode = " & Session("UserId") & ") AND (tblTempFrmAcc.TmpInOutId = " & Val(hdn_tmp.Value) & ") "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub


    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from tblTempFrmAcc where Tmp_id=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
    End Sub



    Protected Sub btnIn_Click(sender As Object, e As System.EventArgs) Handles btnIn.Click
        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblTempFrmAcc", "TmpInOutId")
        End If
        save_tmp(True, False)
        GridView2.Visible = True
        load_grid_tmp()
    End Sub

    Protected Sub cmbreciver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbreciver.SelectedIndexChanged
        load_FrmUsers()
    End Sub
End Class
