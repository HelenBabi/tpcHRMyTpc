Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO

Partial Class trnBarnameRiziFrm
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
        meno_arr = ClsAccounting.meno_acc("trnBarnameRiziFrm.aspx", Val(Session("UserId")))
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
            vahedha.Enabled = True
            tarixShuru.Enabled = True
            tarixEtmam.Enabled = True
            saat.Enabled = True
            makan.Enabled = True
            modares.Enabled = True
            PanelFile.Enabled = True
            FileUpload.Enabled = True
            saatNiaz.Enabled = True
            cmbDeviceID.Enabled = True
            ruz.Enabled = True
            faniCmb.Enabled = True
            zarfiat.Enabled = True
            statCmb.Enabled = True
            tozih.Enabled = True
            cmbHozuri.Enabled = True
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
            vahedha.Enabled = False
            tarixShuru.Enabled = False
            tarixEtmam.Enabled = False
            saat.Enabled = False
            makan.Enabled = False
            modares.Enabled = False
            PanelFile.Enabled = False
            ' txt_BakhshCode.Enabled = True
            FileUpload.Enabled = False
            saatNiaz.Enabled = False
            cmbDeviceID.Enabled = False
            ruz.Enabled = False
            faniCmb.Enabled = False
            zarfiat.Enabled = False
            statCmb.Enabled = False
            tozih.Enabled = False
            cmbHozuri.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        onvan.Text = ""
        vahedha.Text = ""
        tarixShuru.Text = ""
        tarixEtmam.Text = ""
        saat.Text = ""
        makan.Text = ""
        modares.Text = ""
        ChbActiveCode.Checked = False
        sharhFile.Text = ""
        saatNiaz.Text = 0
        ruz.Text = ""
        zarfiat.Text = 0
        tozih.Text = ""
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
        idd.Text = ClsGetMaxId.GetMaxId("trnBarnameRiziTbl", "idd")
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
        Dim ClsB_ As New trnBarnameriziCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New trnBarnameriziCls
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            idd.Text = dt.Rows(0).Item("idd").ToString
            onvan.Text = dt.Rows(0).Item("onvan").ToString
            vahedha.Text = dt.Rows(0).Item("vahedha").ToString
            tarixShuru.Text = dt.Rows(0).Item("tarixShuru").ToString
            tarixEtmam.Text = dt.Rows(0).Item("tarixEtmam").ToString
            saat.Text = dt.Rows(0).Item("saat").ToString
            makan.Text = dt.Rows(0).Item("makan").ToString
            modares.Text = dt.Rows(0).Item("modares").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
            saatNiaz.Text = dt.Rows(0).Item("saatNiaz").ToString
            cmbDeviceID.SelectedValue = dt.Rows(0).Item("deviceID").ToString
            ruz.Text = dt.Rows(0).Item("ruz").ToString
            faniCmb.SelectedValue = dt.Rows(0).Item("fani").ToString
            zarfiat.Text = dt.Rows(0).Item("zarfiat").ToString
            statCmb.SelectedValue = dt.Rows(0).Item("stat").ToString
            tozih.Text = dt.Rows(0).Item("tozih").ToString
            cmbHozuri.SelectedValue = dt.Rows(0).Item("hozuri").ToString
        End If
    End Sub


    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then idd.Text = ClsGetMaxId.GetMaxId("trnBarnameRiziTbl", "idd")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnBarnameRiziprc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@idd", SqlDbType.Int, 6)
            cmm.Parameters("@idd").Value = idd.Text

            cmm.Parameters.Add("@onvan", SqlDbType.NVarChar, 250)
            cmm.Parameters("@onvan").Value = onvan.Text

            cmm.Parameters.Add("@vahedha", SqlDbType.NVarChar, 500)
            cmm.Parameters("@vahedha").Value = vahedha.Text

            cmm.Parameters.Add("@tarixShuru", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixShuru").Value = tarixShuru.Text

            cmm.Parameters.Add("@tarixEtmam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixEtmam").Value = tarixEtmam.Text

            cmm.Parameters.Add("@saat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@saat").Value = saat.Text

            cmm.Parameters.Add("@makan", SqlDbType.NVarChar, 50)
            cmm.Parameters("@makan").Value = makan.Text

            cmm.Parameters.Add("@modares", SqlDbType.NVarChar, 50)
            cmm.Parameters("@modares").Value = modares.Text

            cmm.Parameters.Add("@userid", SqlDbType.Int)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@saatNiaz", SqlDbType.Int)
            cmm.Parameters("@saatNiaz").Value = Val(saatNiaz.Text)

            cmm.Parameters.Add("@deviceID", SqlDbType.Int)
            cmm.Parameters("@deviceID").Value = cmbDeviceID.SelectedValue

            cmm.Parameters.Add("@fani", SqlDbType.NVarChar, 50)
            cmm.Parameters("@fani").Value = faniCmb.SelectedValue

            cmm.Parameters.Add("@hozuri", SqlDbType.NVarChar, 50)
            cmm.Parameters("@hozuri").Value = cmbHozuri.SelectedValue

            cmm.Parameters.Add("@ruz", SqlDbType.Int)
            cmm.Parameters("@ruz").Value = Val(ruz.Text)

            cmm.Parameters.Add("@tozih", SqlDbType.NVarChar, 500)
            cmm.Parameters("@tozih").Value = tozih.Text

            cmm.Parameters.Add("@CodeClosed", SqlDbType.Bit, 1)
            cmm.Parameters("@CodeClosed").Value = ChbActiveCode.Checked

            cmm.Parameters.Add("@zarfiat", SqlDbType.Int)
            cmm.Parameters("@zarfiat").Value = Val(zarfiat.Text)

            cmm.Parameters.Add("@stat", SqlDbType.Int)
            cmm.Parameters("@stat").Value = Val(statCmb.SelectedValue)

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
                If FileUpload.HasFile = True Then
                    trnFilePrc()
                End If
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
#Region "filee"
    Private Sub trnFilePrc()

        Dim size As Integer = FileUpload.PostedFile.ContentLength
        size = size / 1024
        'This is where I'd like to perform the file size check 


        Dim filePath As String = FileUpload.PostedFile.FileName

        Dim filename As String = Path.GetFileName(filePath)

        Dim ext As String = Path.GetExtension(filename)


        Dim fs As Stream = FileUpload.PostedFile.InputStream

        Dim br As New BinaryReader(fs)

        Dim bytes As Byte() = br.ReadBytes(fs.Length)

        'insert the file into database


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand


            cmm.CommandText = "trnFilePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Val(idd.Text)


            cmm.Parameters.Add("@tartib", SqlDbType.Int)
            cmm.Parameters("@tartib").Value = 0

            cmm.Parameters.Add("@codeuser", SqlDbType.Int, 5)
            cmm.Parameters("@codeuser").Value = Session("userid")

            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 20)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress

            cmm.Parameters.Add("@compName", SqlDbType.VarChar, 20)
            cmm.Parameters("@compName").Value = 0

            cmm.Parameters.Add("@size", SqlDbType.Int)
            cmm.Parameters("@size").Value = size

            cmm.Parameters.Add("@Name", SqlDbType.NVarChar, 150)
            cmm.Parameters("@Name").Value = filename

            cmm.Parameters.Add("@sharh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sharh").Value = sharhFile.Text


            cmm.Parameters.Add("@Data", SqlDbType.Binary)
            cmm.Parameters("@Data").Value = bytes

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                'msg_box("  فایل ثبت شد ")
                'BindGridviewData()

                Try

                Catch ex As Exception
                    msg_box(ex.Message.ToString)
                End Try
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try



    End Sub
    Private Sub load_grid_File()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT     documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name,  documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid,  " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib, " & _
                         "tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar " & _
"FROM            documentsTbl INNER JOIN " & _
                        " tblUsers ON documentsTbl.userid = tblUsers.UserCode " & _
       " WHERE(documentsTbl.fileID = " & Val(idd.Text) & ") " & _
"ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

    End Sub
    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())

        Dim sqlstrd As String
        Dim dtd As New DataTable
        sqlstrd = "select  id, content_name, content_file  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)
        If dtd.Rows.Count > 0 Then
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("content_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("content_file"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این پرونده فایل ضمیمه نشده است")
        End If
        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()



    End Sub


    Protected Sub lnkDel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())
        Dim cnn As New SqlConnection

        '  ins_temp(3, radif)
        Dim cmm As New SqlCommand
        cmm.CommandText = "delete        legalDocuments       WHERE id =" & fileid & ""
        Dim constr As String = ClsConnection.GetConn
        cnn.ConnectionString = constr
        cmm.Connection = cnn

        cnn.Open()
        cmm.ExecuteNonQuery()

        cnn.Close()
        load_grid_File()
    End Sub
#End Region



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnDoreCommitTrnSession"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@idd", SqlDbType.Int, 6)
            cmm.Parameters("@idd").Value = idd.Text
            cmm.Connection = cnn

            'Dim result As Integer
            cmm.ExecuteNonQuery()
            'result = cmm.Parameters("@ResultCode").Value
            'If result = 0 Then

            'Call save_()

            msg_box("  عملیات با موفقیت انجام شد ")

            'Else
            'msg_box(cmm.Parameters("@pm").Value)
            'End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try
    End Sub
End Class
