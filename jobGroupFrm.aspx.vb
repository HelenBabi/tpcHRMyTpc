Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports DevExpress.Web
Imports System.Linq
Imports System.Web.UI.WebControls
Partial Class jobGroupFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            load_users()
        End If
        load_grid()
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("jobFrm.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)

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

            ibtnExit.Enabled = False
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel.png"

            ibtnEdit.ImageUrl = "~/Icons/edit2.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete2.png"
            ibtnAdd.ImageUrl = "~/Icons/Add2.png"

            ''''''''''''''''''''''''''''''''''''''''''''''''
            ASPxGrvHead.Enabled = False

            ChbActiveCode.Enabled = True
            kmIDTxt.Enabled = False
            subject.Enabled = True
            showAll.Enabled = True
            'tozihat.Enabled = True
            olvaitCmb.Enabled = True
            darsad.Enabled = True
            daneshChek.Enabled = True
            smsChek.Enabled = True
            noteTxt.Enabled = True
            cmbRunevesht.Enabled = True
            btnRunevesht.Enabled = True
            PanelAttach.Enabled = True
            FileUpload.Enabled = True
            matnErja.Enabled = True
            GridView2.Enabled = True
        Else
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False
            ChbActiveCode.Enabled = False
            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True

            ibtnExit.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew2.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel2.png"

            ibtnEdit.ImageUrl = "~/Icons/edit.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete.png"
            ibtnAdd.ImageUrl = "~/Icons/Add.png"

            ASPxGrvHead.Enabled = True

            ChbActiveCode.Enabled = False
            ' txt_BakhshCode.Enabled = True
            showAll.Enabled = False
            'tozihat.Enabled = False
            olvaitCmb.Enabled = False
            darsad.Enabled = False
            daneshChek.Enabled = False
            smsChek.Enabled = False
            noteTxt.Enabled = False
            cmbRunevesht.Enabled = False
            btnRunevesht.Enabled = False
            PanelAttach.Enabled = False
            FileUpload.Enabled = False
            matnErja.Enabled = False
            GridView2.Enabled = False

        End If

    End Sub

    Private Sub clear_form()
        kmIDTxt.Text = ""
        subject.Enabled = False
        ChbActiveCode.Checked = False
        showAll.Checked = False
        subject.Text = ""
        ChbActiveCode.Checked = False
        showAll.Checked = False
        olvaitCmb.SelectedValue = 0
        '            date_start()
        ', , date_end, 
        darsad.Text = 0
        smsChek.Checked = False
        noteTxt.Text = ""
        daneshChek.Checked = False
        'tozihat.Text = ""
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

    Private Sub load_users()
        Dim dtuser As New DataTable

        Dim sqlstr As String = "SELECT        TOP (200) WorkTypeCode, WorkTypeName " & _
       " FROM tblWorkType WHERE        (WorkTypeCode <> 0) AND (WorkTypeCode <> 100)"
        dtuser = ClsDataTable.GetDataTable(sqlstr)
        cmbRunevesht.DataSource = dtuser
        cmbRunevesht.ValueField = "WorkTypeCode"
        cmbRunevesht.TextField = "WorkTypeName"
        cmbRunevesht.DataBind()

    End Sub
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
        kmIDTxt.Text = ClsGetMaxId.GetMaxId("knowlegmanagmentTbl", "kmID")
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
        Dim ClsB_ As New knowMangementCls
        Dim dt As New DataTable
        dt = ClsB_.GetAllJobs(hdn_deactivedcode.Value, Session("userid"))
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New knowMangementCls
            Dim dt As New DataTable
            dt = ClsB.GetJobs(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            kmIDTxt.Text = dt.Rows(0).Item("kmID").ToString
            subject.Text = dt.Rows(0).Item("subject").ToString
            ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
            showAll.Checked = dt.Rows(0).Item("showAll").ToString
            olvaitCmb.SelectedValue = dt.Rows(0).Item("olaviat").ToString
            '            date_start()
            ', , date_end, 
            darsad.Text = dt.Rows(0).Item("olaviat").ToString
            smsChek.Checked = dt.Rows(0).Item("smss").ToString
            noteTxt.Text = dt.Rows(0).Item("note").ToString
            daneshChek.Checked = dt.Rows(0).Item("daneshStat").ToString
            load_grid_reciver()
            load_grid_FAKTOR()
            'cmbworkTyp.SelectedValue = dt.Rows(0).Item("typ").ToString
        End If
    End Sub
    Private Sub load_grid_reciver()
        Dim sqlstr As String = " SELECT   * from jobGridGroupVW where jobID = " & Val(kmIDTxt.Text) & ""
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub

    Private Sub save_final()
        If hdn_Final_key.Value = "new" Then kmIDTxt.Text = ClsGetMaxId.GetMaxId("knowlegmanagmentTbl", "kmID")

        Dim datS As String = Request.Form("datepicker")
        Dim dateE As String = Request.Form("datepicker2")
        If hdn_Final_key.Value <> "del" Then

            If datS = "" Or dateE = "" Then
                msg_box("تاریخ شروع و تاریخ اتمام را وارد كنيد")

                Exit Sub
            End If
            If Len(datS) < 10 Then
                datS = ClsDateTime.setDate(datS)
            End If

            If Len(dateE) < 10 Then
                dateE = ClsDateTime.setDate(dateE)
            End If


            If datS > dateE Then
                msg_box("مهلت نامعتبر می باشد")

                Exit Sub
            End If

        End If


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "jobsGroupPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@kmID", SqlDbType.Int, 6)
            cmm.Parameters("@kmID").Value = kmIDTxt.Text

            cmm.Parameters.Add("@workType", SqlDbType.Int, 6)
            cmm.Parameters("@workType").Value = 0

            cmm.Parameters.Add("@subject", SqlDbType.NVarChar, 100)
            cmm.Parameters("@subject").Value = subject.Text

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("UserId")

            cmm.Parameters.Add("@CodeClosed", SqlDbType.Bit, 1)
            cmm.Parameters("@CodeClosed").Value = ChbActiveCode.Checked

            cmm.Parameters.Add("@showAll", SqlDbType.Bit, 1)
            cmm.Parameters("@showAll").Value = showAll.Checked

            cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
            If hdn_Final_key.Value = "del" Then
                cmm.Parameters("@type_").Value = 3

            ElseIf hdn_Final_key.Value = "edit" Then
                cmm.Parameters("@type_").Value = 2


            ElseIf hdn_Final_key.Value = "new" Then
                cmm.Parameters("@type_").Value = 1



            End If
            cmm.Parameters.Add("@darsad", SqlDbType.Int, 6)
            cmm.Parameters("@darsad").Value = darsad.Text

            cmm.Parameters.Add("@note", SqlDbType.NVarChar, 500)
            cmm.Parameters("@note").Value = noteTxt.Text


            cmm.Parameters.Add("@olaviat", SqlDbType.SmallInt, 2)
            cmm.Parameters("@olaviat").Value = olvaitCmb.SelectedValue


            cmm.Parameters.Add("@date_start", SqlDbType.VarChar, 10)
            cmm.Parameters("@date_start").Value = datS

            cmm.Parameters.Add("@date_end", SqlDbType.VarChar, 10)
            cmm.Parameters("@date_end").Value = dateE

            cmm.Parameters.Add("@smss", SqlDbType.Bit, 1)
            cmm.Parameters("@smss").Value = smsChek.Checked
            cmm.Parameters.Add("@daneshStat", SqlDbType.Int, 1)
            If daneshChek.Checked = True Then

                cmm.Parameters("@daneshStat").Value = 1
            Else
                cmm.Parameters("@daneshStat").Value = 0
            End If

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)


            cmm.Parameters.Add("@groupUserType", SqlDbType.Int, 5)
            cmm.Parameters("@groupUserType").Value = 1

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
                hdn_tmp.Value = 0
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


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

    Protected Sub btnRunevesht_Click(sender As Object, e As EventArgs) Handles btnRunevesht.Click
        If cmbRunevesht.Value = 0 Then
            msg_box("لطفا گیرنده کار را انتخاب نمایید")
            cmbRunevesht.Focus()
            Exit Sub
        Else
            If Val(hdn_tmp.Value) = 0 Then
                hdn_tmp.Value = ClsGetMaxId.GetMaxId("tblJobResiverTemp", "tempID")
            End If
            save_tmp(True, False)
            GridView2.Visible = True
            'load_grid_tmp()
            load_grid_reciver()

            'btnSave.Visible = True
            'btnBack.Visible = True
            'load_cmbRuneveshtUsers()

        End If
    End Sub
    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)

        'Dim sqlstr As String = "SELECT        UserCode, genderr         FROM tblUsers         WHERE(UserCode = " & Val(cmbRunevesht.Value) & ")"
        'Dim dtt As New DataTable
        'dtt = ClsDataTable.GetDataTable(sqlstr)
        'Dim sex As Integer = dtt.Rows(0).Item("genderr").ToString

        'Dim sexx As String = "آقای"
        'If sex = 1 Then

        '    sexx = "خانم"

        'End If

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "jobReciverGroupTempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            cmm.Parameters.Add("@jobID", SqlDbType.Int)
            cmm.Parameters("@jobID").Value = Val(kmIDTxt.Text)

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@sender", SqlDbType.Int, 5)
            cmm.Parameters("@sender").Value = Session("UserId")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 1000)
            cmm.Parameters("@matnErja").Value = "بخش " & cmbRunevesht.SelectedItem.Text & " لطفا " & matnErja.Text

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  عملیات با موفقیت انجام شد ")
                '  Call save_()
                ' load_grid()
                matnErja.Text = ""
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub load_grid_tmp()
        Dim sqlstr As String = " SELECT   * from jobGridGroupTempVW where tempID = " & hdn_tmp.Value & ""
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
            cmm.CommandText = "delete from tblJobResiverGroupTemp where idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()

            'If hdn_Final_key.Value = "edit" Then
            load_grid_reciver()
            'End If


            'load_grid_tmp()

        End If
    End Sub

    Protected Sub btnSabtFile_Click(sender As Object, e As EventArgs) Handles btnSabtFile.Click
        If FileUpload.HasFile = False Then
            msg_box("فایل انتخاب نشده است")
            Exit Sub
        Else
            save_file()


            load_grid_FAKTOR()

        End If
    End Sub
    Private Sub save_file()

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


            cmm.CommandText = "jobNewFilePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Val(kmIDTxt.Text)


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
            cmm.Parameters("@sharh").Value = ""


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
    Private Sub load_grid_FAKTOR()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT     documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name,  documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid,  " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib, " & _
                         "tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar " & _
"FROM            documentsTbl INNER JOIN " & _
                        " tblUsers ON documentsTbl.userid = tblUsers.UserCode " & _
       " WHERE(documentsTbl.fileID = " & Val(kmIDTxt.Text) & ") " & _
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
        cmm.CommandText = "delete        documentsTbl       WHERE id =" & fileid & ""
        Dim constr As String = ClsConnection.GetConn
        cnn.ConnectionString = constr
        cmm.Connection = cnn

        cnn.Open()
        cmm.ExecuteNonQuery()

        'Dim cmm2 As New SqlCommand
        'cmm2.CommandText = "delete from laterRuneveshtTempTbl where id=" & fileid
        'cmm2.Connection = cnn
        'cmm2.ExecuteNonQuery()


        cnn.Close()
        load_grid_FAKTOR()
        load_grid_reciver()
    End Sub



    Protected Sub cmbRunevesht_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRunevesht.SelectedIndexChanged
        loadUsers()
    End Sub
    Private Sub loadUsers()
        Dim sqlstr As String = "SELECT        TOP (200) tblWorkType.WorkTypeCode, tblUsers.UserLastName + ' ' + tblUsers.UserFamili + ' ' + tblUsers.semat  AS headd, tblUsers.UserPic  as axurl " & _
                     " FROM            tblWorkType INNER JOIN     tblUsers ON tblWorkType.WorkTypeCode = tblUsers.WorkType " & _
                " WHERE(tblWorkType.WorkTypeCode <> 100 And tblWorkType.WorkTypeCode <> 0 And tblWorkType.WorkTypeCode = " & cmbRunevesht.Value & ") And (tblUsers.CodeClosed = 0) AND (tblUsers.UsersAdmin <> 1)"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        cmbRunevesht.ToolTip = (dt.Rows(i).Item("UserLastName").ToString())

        '    Next i

        'End If

        DataList1.DataSource = dt
        DataList1.DataBind()

    End Sub
End Class
