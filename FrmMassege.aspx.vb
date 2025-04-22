Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class FrmMassege

    Inherits System.Web.UI.Page
    Dim dt2 As DataTable
    Dim datS As String = ""
    Dim dateE As String = ""
    Dim datS1 As String = ""
    Dim dateE1 As String = ""
    Dim warning_pic As String = ""
    Dim date_jari As String = ClsDateTime.GetDateNow
    Protected Sub grdJari_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(grdJari.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image1"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
        If e.DataColumn.Name = "attach_" Then
            Dim hyperlink_ As HyperLink = TryCast(grdJari.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "HyperLink1"), HyperLink)
            If hyperlink_.NavigateUrl = "" Then
                hyperlink_.Visible = False
            End If

            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub

    'Protected Sub grdMySend_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles grdMySend.AfterPerformCallback
    '    load_grid_MySend()
    'End Sub

    Protected Sub grdMySend_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(grdMySend.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
        If e.DataColumn.Name = "attach_" Then
            Dim hyperlink_ As HyperLink = TryCast(grdMySend.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "hiperMySend"), HyperLink)
            If hyperlink_.NavigateUrl = "" Then
                hyperlink_.Visible = False
            End If

            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub
   
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

      
        Call save_final()


        txtNote.Text = ""
        txtsubject.Text = ""
      

    End Sub

    Private Sub save_final()
        hdn_Final_key.Value = ClsGetMaxId.GetMaxId("tblMessage", "id_Msg")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        Dim showall As Byte
        If CheckBox1.Checked = True Then
            showall = 1
        End If
        cmm.CommandText = "PrcMassege"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@ShowAll", SqlDbType.Int, 1)
        cmm.Parameters("@ShowAll").Value = showall

        cmm.Parameters.Add("@mozu", SqlDbType.VarChar, 150)
        cmm.Parameters("@mozu").Value = txtsubject.Text


        cmm.Parameters.Add("@note", SqlDbType.NVarChar, 250)
        cmm.Parameters("@note").Value = txtNote.Text

        cmm.Parameters.Add("@file_url", SqlDbType.VarChar, 150)
        If FileUpload1.HasFile Then
            cmm.Parameters("@file_url").Value = "images/msg/" & hdn_Final_key.Value & "_" & FileUpload1.PostedFile.FileName
        Else
            cmm.Parameters("@file_url").Value = ""
        End If

        cmm.Parameters.Add("@id_Msg", SqlDbType.VarChar, 3)
        cmm.Parameters("@id_Msg").Value = hdn_Final_key.Value

        cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
        cmm.Parameters("@sender").Value = Session("UserId")

        cmm.Parameters.Add("@reciver", SqlDbType.Int, 10)
        cmm.Parameters("@reciver").Value = cmbreciver.SelectedValue

        cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
        cmm.Parameters("@userid").Value = Session("UserId")

        'cmm.Parameters.Add("@CodeClosed", SqlDbType.Bit, 1)
        'cmm.Parameters("@CodeClosed").Value = ChbActiveCode.Checked

        cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
        cmm.Parameters("@type_").Value = 1
        'If hdn_Final_key.Value = "del" Then
        '    cmm.Parameters("@type_").Value = 3
        'ElseIf hdn_Final_key.Value = "edit" Then
        '    cmm.Parameters("@type_").Value = 2
        'ElseIf hdn_Final_key.Value = "new" Then
        '    cmm.Parameters("@type_").Value = 1
        'End If

        cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
        cmm.Parameters("@pm").Direction = ParameterDirection.Output

        cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
        cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
        cmm.Connection = cnn

        Dim result As Integer
        cmm.ExecuteNonQuery()
        result = cmm.Parameters("@ResultCode").Value
        If result = 0 Then
            msg_box("  پیام جدید با شماره ردیف  " & hdn_Final_key.Value & "  ارسال شد  ")
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
            Try
                If FileUpload1.HasFile Then

                    Dim masir As String = HttpContext.Current.Server.MapPath("~/images/msg/")



                    Dim filname As String = hdn_Final_key.Value & "_" & FileUpload1.PostedFile.FileName
                    FileUpload1.PostedFile.SaveAs(masir & filname)
                End If


            Catch ex As Exception

            End Try
        Else
            msg_box(cmm.Parameters("@pm").Value)
        End If

        load_grid_MySend()
        load_grid_jari()
    End Sub

    Private Sub load_users_reciver()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = " select UserCode,UserLastName +' '+ UserFamili as nam from tblUsers where SysUser=0 and codeclosed=0 order by UserFamili "
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbreciver.DataSource = dt
        cmbreciver.DataValueField = "UserCode"
        cmbreciver.DataTextField = "nam"
        cmbreciver.DataBind()

    End Sub

    Protected Sub grdJari_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles grdJari.AfterPerformCallback
        'If Me.IsCallback Then
        load_grid_jari()
        grdJari.ExpandAll()

        load_grid_MySend()
        'End If
    End Sub


    Protected Sub grddtl1_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("id_job").ToString



        Dim sqlstr As String
        sqlstr = " SELECT     tblJobDetail.date_sabt + ' ' + tblJobDetail.time_sabt as zaman, tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
"FROM         tblJob INNER JOIN " & _
 "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
"WHERE     (tblJob.id_job = " & idd & ")"
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub


    '    Protected Sub grddtlok0_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Dim idd As Integer
    '        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
    '        idd = grid.GetMasterRowFieldValues("id_job").ToString



    '        Dim sqlstr As String
    '        sqlstr = " SELECT     tblJobDetail.date_sabt + ' ' + tblJobDetail.time_sabt as zaman, tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
    '"FROM         tblJob INNER JOIN " & _
    ' "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
    '"WHERE     (tblJob.id_job = " & idd & ")"
    '        dt2 = ClsDataTable.GetDataTable(sqlstr)
    '        grid.DataSource = dt2
    '        'grid.DataBind()

    '    End Sub


    Protected Sub grddtlok_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("id_job").ToString



        Dim sqlstr As String
        sqlstr = " SELECT    tblJobDetail.date_sabt + ' ' + tblJobDetail.time_sabt as zaman, tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
"FROM         tblJob INNER JOIN " & _
 "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
"WHERE     (tblJob.id_job = " & idd & ")"
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub
    Protected Sub grddtlokall_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("id_job").ToString



        Dim sqlstr As String
        sqlstr = " SELECT    tblJobDetail.date_sabt + ' ' + tblJobDetail.time_sabt as zaman, tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
"FROM         tblJob INNER JOIN " & _
 "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
"WHERE     (tblJob.id_job = " & idd & ")"
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub
    'Protected Sub grdJari_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grdJari.HtmlRowPrepared



    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("olaviat"))
    '    If value = 0 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.PapayaWhip
    '    Else
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.Azure

    '    End If


    '    Dim date_job As String = CStr(e.GetValue("date_end"))
    '    If date_job > date_jari Then
    '        warning_pic = "images/design_images/view_refresh.png"
    '    Else
    '        warning_pic = "images/design_images/error.png"

    '    End If

    'End Sub
    
   
    


    Private Sub load_grid_MySend()
        Dim sqlstr As String
        sqlstr = " SELECT     tblUsers_1.UserFamili + ' ' + tblUsers_1.UserLastName AS reciver_, tblUsers.UserFamili + ' ' + tblUsers.UserLastName AS sender_, tblMessage.id_Msg, " & _
       " tblMessage.mozu, tblMessage.date_start, tblMessage.time_start, tblMessage.note, tblMessage.file_url, tblMessage.sender, tblMessage.reciver, tblMessage.stat, " & _
       " tblMessage.ShowAll FROM         tblMessage INNER JOIN " & _
               "      tblUsers AS tblUsers_1 ON tblMessage.reciver = tblUsers_1.UserCode INNER JOIN " & _
                "     tblUsers ON tblMessage.sender = tblUsers.UserCode " & _
                 "    WHERE     (tblMessage.sender = " & Session("UserId") & ") AND (tblMessage.stat = 0) " & _
               " ORDER BY tblMessage.id_Msg DESC"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdMySend.DataSource = dt
        grdMySend.DataBind()

    End Sub


    Private Sub load_grid_jari()
        Dim sqlstr As String
        sqlstr = " SELECT     tblUsers_1.UserFamili + ' ' + tblUsers_1.UserLastName AS reciver_, tblUsers.UserFamili + ' ' + tblUsers.UserLastName AS sender_, tblMessage.id_Msg, " & _
        " tblMessage.mozu, tblMessage.date_start, tblMessage.time_start, tblMessage.note, tblMessage.file_url, tblMessage.sender, tblMessage.reciver, tblMessage.stat, " & _
        " tblMessage.ShowAll FROM         tblMessage INNER JOIN " & _
                "      tblUsers AS tblUsers_1 ON tblMessage.reciver = tblUsers_1.UserCode INNER JOIN " & _
                 "     tblUsers ON tblMessage.sender = tblUsers.UserCode " & _
                  "    WHERE     (tblMessage.reciver = " & Session("UserId") & ") AND (tblMessage.stat = 0) " & _
                " ORDER BY tblMessage.id_Msg DESC"



        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grdJari.DataSource = dt
        grdJari.DataBind()

    End Sub
  
   
    Private Sub load_grid_dtl(id_j As Integer)
        Dim sqlstr As String
        sqlstr = " SELECT     tblJobDetail.date_sabt + ' ' + tblJobDetail.time_sabt as zaman, tblJobDetail.id_dtl,tblJob.subject, tblJobDetail.detail_job, tblJobDetail.id_job_ " & _
"FROM         tblJob INNER JOIN " & _
 "                     tblJobDetail ON tblJob.id_job = tblJobDetail.id_job_ " & _
"WHERE     (tblJob.id_job = " & id_j & ")"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grddtl.DataSource = dt
        grddtl.DataBind()

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_.aspx")
        End If
        upStat()
        If Me.IsPostBack = False Then

            load_users_reciver()
            load_grid_jari()
          
            load_grid_MySend()


            Dim a As String = ClsDateTime.GetDateNow
            Dim b As String = ClsDateTime.GetDateNow
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
           
        End If
        'If Me.IsCallback = True Then
        '    
        'End If

        load_grid_MySend()
        'load_grid_jari()

    End Sub

    Private Sub upStat()
        Dim sqlstr As String
        sqlstr = "  UPDATE  tblMessage SET      stat =0 " & _
                                 " WHERE(reciver = " & Session("UserId") & " and stat=2)"

        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            cnn.Close()
        End Try
    End Sub



    '    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
    'sender.
    '    End Sub

    Protected Sub grdJari_RowCommand(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles grdJari.RowCommand

        ''msg_box(e.CommandArgs.CommandArgument)
        ''msg_box(e.VisibleIndex.ToString())
        ''If e.DataColumn.Name = "photo" Then
        ''    Dim iamgegrdst As Image = TryCast(grdJari.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image1"), Image)
        ''    iamgegrdst.ImageUrl = warning_pic
        ''    '  textBox.Text = Convert.ToString(e.CellValue)
        ''End If
        ''Dim a As TextBox
        '' Dim txt_ As New ASPxTextBox
        'Dim txt_ As ASPxTextBox = grdJari.FindRowTemplateControl(e.VisibleIndex, "ASPxTextBox1")

        ''DIM   textBox As ASPxTextBox = ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "ASPxTextBox1") as ASPxTextBox;
        ''txt_ = TryCast(grdJari.FindEditRowCellTemplateControl(
        'msg_box(txt_.Text)

        If e.CommandArgs.CommandName = "select" Then
            Dim iamgegrdst As ASPxTextBox = TryCast(grdJari.FindRowCellTemplateControl(e.VisibleIndex, grdJari.Columns("darsad"), "ASPxTextBox1"), ASPxTextBox)
            'msg_box(iamgegrdst.Text)
            '  textBox.Text = Convert.ToString(e.CellValue)
            Dim sqlstr As String
            If iamgegrdst.Text = 100 Then
                sqlstr = "  UPDATE tblJob SET   stat=1 , darsad = '" & Val(iamgegrdst.Text) & "' " & _
                                    "WHERE(id_job = " & e.CommandArgs.CommandArgument & ")"

            Else
                sqlstr = "  UPDATE tblJob SET             darsad = '" & Val(iamgegrdst.Text) & "' " & _
                                  "WHERE(id_job = " & e.CommandArgs.CommandArgument & ")"
            End If

            updarsad(sqlstr)
        ElseIf e.CommandArgs.CommandName = "sabtnote" Then
            Label2.Text = e.CommandArgs.CommandArgument
            'Dim url As String = HttpContext.Current.Server.MapPath("~/frmHolder.aspx")
            'Dim a As String = HttpContext.Current.Request.Url.ToString
            'a = a.Substring(0, a.LastIndexOf("/") + 1)
            'a += "frmHolder.aspx"
            ''Dim masir As String = HttpContext.Current.Server.MapPath("~/images/attach_files/")
            'ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "')</script>")
            load_grid_dtl(Label2.Text)
            grdJari_ModalPopupExtender.Show()

        End If


    End Sub
    Private Sub updarsad(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
            msg_box("ثبت شد")
            load_grid_jari()

            load_grid_MySend()

        Catch ex As Exception
            msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub

    Protected Sub LinkButton5_Click(sender As Object, e As System.EventArgs)
        grdJari_ModalPopupExtender.Show()
    End Sub
    Protected Sub grddtl_RowCommand(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles grddtl.RowCommand
        If e.CommandArgs.CommandName = "delete" Then
            Dim sqlstr As String
            Dim cnn As New SqlConnection

            Dim cmm As SqlCommand
            sqlstr = " delete tblJobDetail where id_dtl=" & e.CommandArgs.CommandArgument & ""

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
            load_grid_dtl(Label2.Text)
            cnn.Close()
        End If




    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        If txtNote0.Text = "" Then
            ' msg_box("لطفا توضی")
            Exit Sub
        End If
        Dim sqlstr As String
        Dim cnn As New SqlConnection
        Dim tim As String = ClsDateTime.GetTimeNow
        Dim cmm As SqlCommand

        sqlstr = "INSERT       INTO tblJobDetail (id_job_, detail_job, date_sabt, time_sabt) " & _
               " VALUES     (" & Val(Label2.Text) & ", '" & txtNote0.Text & "' , '" & date_jari & "' , '" & tim & "')"

        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        cmm = New SqlCommand(sqlstr, cnn)
        cmm.ExecuteNonQuery()
        cnn.Close()
        load_grid_dtl(Label2.Text)
        txtNote0.Text = ""
    End Sub

    Private Function datepicker() As Object
        Throw New NotImplementedException
    End Function


End Class
