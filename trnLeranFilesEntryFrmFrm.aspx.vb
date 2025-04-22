Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO

Partial Class trnLeranFilesEntryFrmFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("super_vizer") = False Then
            meno_acc()
        End If
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()
            load_Units()
        End If
        load_grid()
        'load_grid_File()
    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnLeranFilesEntryFrmFrm.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)

        ibtnDeactived.Visible = meno_arr(9)

        'ibtnExit.Visible = meno_arr(11)
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
            sharhFile.Enabled = True
            CmbUnit.Enabled = True
          
            PanelFile.Enabled = True
            FileUpload.Enabled = True
            FileUploadVedio.Enabled = True
            FileUploadVoice.Enabled = True

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

            sharhFile.Enabled = False
          
            PanelFile.Enabled = False
            ' txt_BakhshCode.Enabled = True
            FileUpload.Enabled = False
            FileUploadVedio.Enabled = False
            FileUploadVoice.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        sharhFile.Text = ""
        'CmbUnit.SelectedValue = 0
      
        ChbActiveCode.Checked = False
        sharhFile.Text = ""
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
        idd.Text = ClsGetMaxId.GetMaxId("documentsTbl", "id")
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

    Private Sub load_Units()
        Dim dt As New DataTable
        Dim cls_ As New trnSubjectFilesCls
        dt = cls_.GetAllUnitsDDL
        CmbUnit.DataSource = dt
        CmbUnit.DataValueField = "UnitsCode"
        CmbUnit.DataTextField = "UnitsName"
        CmbUnit.DataBind()

    End Sub
    Private Sub load_grid()
        Dim ClsB_ As New trnSubjectFilesCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnitsDocuments(hdn_deactivedcode.Value)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New trnBarnameriziCls
            Dim dt As New DataTable
            dt = ClsB.GetDocument(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            idd.Text = dt.Rows(0).Item("id").ToString
            sharhFile.Text = dt.Rows(0).Item("sharh").ToString
            CmbUnit.Text = dt.Rows(0).Item("UnitsCode").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
        End If
    End Sub


    Private Sub save_final()
        'If hdn_Final_key.Value = "new" Then idd.Text = ClsGetMaxId.GetMaxId("documentsTbl", "id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand


        Dim size As Integer = FileUpload.PostedFile.ContentLength
        size = size / 1024


        Dim filePath As String = FileUpload.PostedFile.FileName

        Dim filename As String = Path.GetFileName(filePath)

        Dim ext As String = Path.GetExtension(filename)


        Dim fs As Stream = FileUpload.PostedFile.InputStream

        Dim br As New BinaryReader(fs)

        Dim bytes As Byte() = br.ReadBytes(fs.Length)

        'insert the file into database

        Dim filePath2 As String = FileUploadVoice.PostedFile.FileName
        Dim filename2 As String = Path.GetFileName(filePath2)

        Dim filePath3 As String = FileUploadVedio.PostedFile.FileName()
        Dim filename3 As String = Path.GetFileName(filePath3)

        Dim fs2 As Stream = FileUploadVoice.PostedFile.InputStream
        Dim br2 As New BinaryReader(fs2)
        Dim bytes2 As Byte() = br2.ReadBytes(fs2.Length)

        Dim fs3 As Stream = FileUploadVedio.PostedFile.InputStream
        Dim br3 As New BinaryReader(fs3)
        Dim bytes3 As Byte() = br3.ReadBytes(fs3.Length)




        Try

            cmm.CommandText = "trnFileDLPrc"
            cmm.CommandType = CommandType.StoredProcedure

            idd.Text = ClsGetMaxId.GetMaxId("documentsTbl", "id")
            cmm.Parameters.Add("@idd", SqlDbType.Int)
            cmm.Parameters("@idd").Value = idd.Text

            cmm.Parameters.Add("@UnitsCode", SqlDbType.Int)
            cmm.Parameters("@UnitsCode").Value = CmbUnit.SelectedValue

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

            cmm.Parameters.Add("@voice_name", SqlDbType.NVarChar, 150)
            cmm.Parameters("@voice_name").Value = filename2

            cmm.Parameters.Add("@video_name", SqlDbType.NVarChar, 150)
            cmm.Parameters("@video_name").Value = filename3


            cmm.Parameters.Add("@sharh", SqlDbType.NVarChar, 150)
            cmm.Parameters("@sharh").Value = sharhFile.Text


            cmm.Parameters.Add("@Data", SqlDbType.Binary)
            cmm.Parameters("@Data").Value = bytes


            cmm.Parameters.Add("@DataVoice", SqlDbType.Binary)
            cmm.Parameters("@DataVoice").Value = bytes2

            cmm.Parameters.Add("@DataVideo", SqlDbType.Binary)
            cmm.Parameters("@DataVideo").Value = bytes3

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
