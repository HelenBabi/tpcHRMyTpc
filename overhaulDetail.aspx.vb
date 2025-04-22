Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports System.IO.Compression
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports Ionic.Zip



Partial Class overhaulDetail
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable

  


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadItems()
        PersonImage.ImageUrl = "~/Attachment/" & Nationality.Text & "/PersonalPic.jpg"

        If Not Page.IsPostBack Then
            LoadImages()
        End If

        'gridErja()
        notes()
        'load_grid_FAKTOR()
        If Me.IsPostBack = False Then

            loadItems()
            loadcmbParaf()
            loadComboRuneveshtTyp()
        End If
        If Session("from") = "user" Then
            'btnWork.Visible = False
            BtnCancel.Visible = False

            'CheckFori.Enabled = False
            btnOk.Visible = False

            'cmbParaf.Visible = False
            'btnParaf.Visible = False
            CheckBoxRunevesht.Visible = False
        ElseIf Session("from") = "admin" Then
            btnOk.Visible = True

            'cmbParaf.Visible = True
            'btnParaf.Visible = True
            CheckBoxRunevesht.Visible = True

        ElseIf Session("from") = "reciver" Then
            btnOk.Visible = False

            'cmbParaf.Visible = False
            'btnParaf.Visible = False
            CheckBoxRunevesht.Visible = True
            btnOk.Visible = False
            'btnWork.Visible = False
            BtnCancel.Visible = False
        End If



    End Sub
    Private Sub LoadImages()
        Dim folder = "~/Attachment/" & Nationality.Text & "/"
        Dim folderPath = Server.MapPath(folder)
        Dim files = Directory.GetFiles(folderPath).Take(6).ToList()

        Dim images = files.Select(Function(f) New With {
            Key .Url = ResolveUrl(folder & Path.GetFileName(f)),
            Key .Name = Path.GetFileName(f),
            Key .Nat = Nationality.Text
        }).ToList()

        RepeaterImages.DataSource = images
        RepeaterImages.DataBind()
    End Sub

    Protected Sub btnDownloadZip_Click(sender As Object, e As EventArgs) Handles btnDownloadZip.Click
        Dim folderPath As String = Server.MapPath("~/Attachment/" & Nationality.Text & "/")
        Dim files = Directory.GetFiles(folderPath).Take(6).ToList() ' همون فایل‌هایی که توی صفحه نشون دادی

        Response.Clear()
        Response.BufferOutput = False
        Response.ContentType = "application/zip"
        Response.AddHeader("content-disposition", "attachment; filename=" & Nationality.Text & ".zip")

        Using zip As New Ionic.Zip.ZipFile()
            For Each path In files
                If File.Exists(path) Then
                    zip.AddFile(path, "") ' بدون پوشه داخلی
                End If
            Next
            zip.Save(Response.OutputStream)
        End Using

        Response.Flush()
        Response.End()
    End Sub


    Sub gridErja()
        Dim dt2 As New DataTable

        Dim sqlstr As String = ""
        If Session("from") = "user" Then
            sqlstr = " SELECT        girande,tarix, saat, matnErja, erja, aslRuneveshtType, actionId, nam + ' ' + family AS ferestande, runeveshtTypeValue " & _
   "FROM sugReciverVW WHERE        (sender = " & Session("userID") & ") AND (laterID = " & Session("sugID") & ") "
        ElseIf Session("from") = "admin" Then
            sqlstr = " SELECT        girande,tarix, saat, matnErja, erja, aslRuneveshtType, actionId, nam + ' ' + family AS ferestande, runeveshtTypeValue " & _
  "FROM sugReciverVW WHERE        (laterID = " & Session("sugID") & ") "
        ElseIf Session("from") = "reciver" Then
            sqlstr = " SELECT        girande,tarix, saat, matnErja, erja, aslRuneveshtType, actionId, nam + ' ' + family AS ferestande, runeveshtTypeValue " & _
     "FROM sugReciverVW WHERE        (reciverID = " & Session("userID") & ") AND (laterID = " & Session("sugID") & ") "
        End If




        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            'PanelErja.Visible = True
            GridViewErja.DataSource = dt2
            GridViewErja.DataBind()
        End If


    End Sub
    Sub notes()
        Dim dt2 As New DataTable
        Dim sqlstr As String = " SELECT       ID, codePersoneli, headd, IdOverhaul, notee, note_id, REPLACE([pic ], ' ', '') AS picc from overhaulCommentVW WHERE        (IdOverhaul = " & Session("idOver") & ")  ORDER BY note_id DESC"



        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            DataList1.DataSource = dt2
            DataList1.DataBind()
        End If


    End Sub
    Protected Sub DataList1_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemCreated
        For i As Integer = 0 To DataList1.Items.Count - 1
            '  Dim labelcode As new Label = DataList5 .Items(i).FindControl("Label8")
            Dim labelcode As Label = DirectCast(DataList1.Items(i).FindControl("lblUsrTyp"), Label)
            If Val(labelcode.Text) = 100 Then
                DataList1.Items(i).BackColor = Drawing.Color.LightYellow

            ElseIf Val(labelcode.Text) = 0 Then
                DataList1.Items(i).BackColor = Drawing.Color.Khaki
                'DataList1.Items(i).Font.Bold = True
            ElseIf Val(labelcode.Text) = 2 Then
                DataList1.Items(i).BackColor = Drawing.Color.MediumAquamarine
            ElseIf Val(labelcode.Text) = 1 Then
                DataList1.Items(i).BackColor = Drawing.Color.AntiqueWhite
            ElseIf Val(labelcode.Text) = 5 Then
                DataList1.Items(i).BackColor = Drawing.Color.MistyRose
            ElseIf Val(labelcode.Text) = 6 Then
                DataList1.Items(i).BackColor = Drawing.Color.Aquamarine
            End If
        Next
    End Sub
    Private Sub save_final()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "overhaulCommentPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@sugID", SqlDbType.Int)
            cmm.Parameters("@sugID").Value = Id.Text

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@notee").Value = txtNazar.Text

            cmm.Parameters.Add("@PerID", SqlDbType.Int, 10)
            cmm.Parameters("@PerID").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            'Dim frm As String = ""

            'If Session("modir") = True Then
            '    frm = "modir"
            'Else
            '    frm = Session("from")
            'End If


            'cmm.Parameters.Add("@userType", SqlDbType.VarChar, 7)
            'cmm.Parameters("@userType").Value = frm




            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                'msg_box("  ثبت  شد ")


            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub



    Private Sub loadComboRuneveshtTyp()
        Dim sqlstr As String = "SELECT        TOP (200) runevestTypeID, runeveshtTypeValue, runeveshtTypeStat, visibile " & _
       " FROM laterRuneveshtTypeTbl WHERE        (visibile = 1)"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbruneveshtType.DataSource = dt
        CmbruneveshtType.DataTextField = "runeveshtTypeValue"
        CmbruneveshtType.DataValueField = "runevestTypeID"
        CmbruneveshtType.DataBind()


    End Sub


    Private Sub loadcmbParaf()
        Dim dtg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT idd,valuee FROM tblTicketTemplate WHERE     (CodeClosed = 0) and userId= " & Session("userid") & ""
        dtg = ClsDataTable.GetDataTable(sqlstr)

        'cmbParaf.DataSource = dtg
        'cmbParaf.DataTextField = "valuee"
        'cmbParaf.DataValueField = "idd"
        'cmbParaf.DataBind()

    End Sub



    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub

    Private Sub loadItems()
        Dim dt As DataTable

        Dim sqlstrr As String = "SELECT *  FROM   overhaulBrowsVW WHERE        (Id = " & Session("idOver") & ")"
        dt = ClsDataTable.GetDataTable(sqlstrr)
        If dt.Rows.Count > 0 Then

            Id.Text = dt.Rows(0).Item("Id")
            LastName.Text = dt.Rows(0).Item("LastName")
            FirstName.Text = dt.Rows(0).Item("FirstName")
            FatherName.Text = dt.Rows(0).Item("FatherName")
            Nationality.Text = dt.Rows(0).Item("Nationality")
            shSh.Text = dt.Rows(0).Item("shSh")
            BirthDay.Text = dt.Rows(0).Item("BirthDay")
            BankCardNo.Text = dt.Rows(0).Item("BankCardNo").ToString()
            IBAN.Text = dt.Rows(0).Item("IBAN").ToString()

            SSN.Text = dt.Rows(0).Item("SSN").ToString()
            Mobile.Text = dt.Rows(0).Item("Mobile")
            Tell.Text = dt.Rows(0).Item("Tell")
            PostalCode.Text = dt.Rows(0).Item("PostalCode")
            Address.Text = dt.Rows(0).Item("Address")
            Degree.Text = dt.Rows(0).Item("madrak")
            Major.Text = dt.Rows(0).Item("FieldOfStudy")
            'State.Text = dt.Rows(0).Item("IsLastOverhaul")
            tahol.Text = dt.Rows(0).Item("tahol")

            xedmat.Text = dt.Rows(0).Item("xedmat")
            isargari.Text = dt.Rows(0).Item("isargari")




        End If
    End Sub








    'Protected Sub btnWork_Click(sender As Object, e As EventArgs) Handles btnWork.Click
    '    Dim sqlstr, sqlstr2 As String

    '    sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 4  WHERE (sugID = " & Session("sugID") & ") "
    '    Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

    '    Dim payam As String = ""


    '    payam = txtNazar.Text & " نیاز به توضیحات بیشتر"


    '    sqlstr2 = "INSERT INTO    sugCommentTbl ( sugID, notee, PerID, zaman) " & _
    '                " VALUES     (" & Session("sugID") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
    '    upStat(sqlstr)
    '    upStat(sqlstr2)
    '    notes()
    'End Sub

    'Protected Sub btnParaf_Click(sender As Object, e As EventArgs) Handles btnParaf.Click
    '    'If cmbParaf.SelectedIndex < 0 Then
    '    '    msg_box("لطفا پاراف خود را تنظیم نمایید")
    '    '    Exit Sub
    '    'End If
    '    'txtNazar.Text = txtNazar.Text & " " & cmbParaf.SelectedItem.ToString
    'End Sub



    Protected Sub btnNazar_Click(sender As Object, e As EventArgs) Handles btnNazar.Click
        save_final()
        notes()
        txtNazar.Text = ""
    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
        Session("idOver") = 0
        'If Session("from") = "user" Then
        '    Response.Redirect("sugMyWorkTableFrm.aspx")
        'ElseIf Session("from") = "admin" Then
        '    Response.Redirect("sugWorkTableAllFrm.aspx")

        'ElseIf Session("from") = "reciver" Then
        '    Response.Redirect("sugWorkTableFrm.aspx")
        'End If
        Response.Redirect("overhaulBrowseAdmin.aspx")


    End Sub





    Protected Sub CheckBoxRunevesht_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRunevesht.CheckedChanged
        If CheckBoxRunevesht.Checked = True Then
            PanelRunevest.Visible = True

            load_cmbRuneveshtUsers()

            'PanelAttach.Visible = True
            'btnPuyesh.Visible = True


        Else
            PanelRunevest.Visible = False
            'PanelAttach.Visible = False
            'btnPuyesh.Visible = False

        End If
    End Sub
    Private Sub load_cmbRuneveshtUsers()
        Dim dtm As New DataTable
        Dim sqlstr As String = ""

        If Session("from") = "admin" Then
            sqlstr = "SELECT        nam + ' ' + family +  ' - ' + postname AS karbar, ID " & _
        "FROM personel WHERE        (CodeClosed = 0) AND (modir = 1)  ORDER BY family"
        Else
            sqlstr = "SELECT        nam + ' ' + family +  ' - ' + postname AS karbar, ID " & _
      "FROM personel WHERE        (CodeClosed = 0) AND (UnitsCode = " & Session("unitID") & ")  ORDER BY family"
        End If

        dtm = ClsDataTable.GetDataTable(sqlstr)
        cmbRunevesht.DataSource = dtm
        cmbRunevesht.TextField = "karbar"
        cmbRunevesht.ValueField = "ID"
        cmbRunevesht.DataBind()

    End Sub

    Protected Sub btnSaveFinal_Click(sender As Object, e As EventArgs) Handles btnSaveFinal.Click
        If cmbRunevesht.Value = 0 Then
            cmbRunevesht.Focus()
            msg_box("لطفا گیرنده را انتخاب نمایید")

            Exit Sub
        Else

            If Val(hdn_tmp.Value) = 0 Then
                save_Erja2()

            Else
                save_Erja()
            End If



            Dim payam As String = ""
            If Session("from") = "admin" Then
                payam = txtNazar.Text & " پیشنهاد ارجاع شد به کمیته مربوطه"
            ElseIf Session("from") = "reciver" Then
                payam = txtNazar.Text & " پیشنهاد ارجاع شد به کارشناس مربوطه"
            End If

            Dim sqlstr As String = ""

            Dim sqlstr2 As String = ""
            Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

            If Session("from") = "user" Then
                sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 8 WHERE (sugID = " & Session("sugID") & ") "


            End If




            If Session("from") = "admin" Then
                sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 9 WHERE (sugID = " & Session("sugID") & ") "

            End If

            If Session("from") = "reciver" And Session("modir") = True Then
                sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 10 WHERE (sugID = " & Session("sugID") & ") "
            End If

            If Session("from") = "reciver" And Session("modir") = False Then
                sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 11 WHERE (sugID = " & Session("sugID") & ") "
            End If





            sqlstr2 = "INSERT INTO    sugCommentTbl ( sugID, notee, PerID, zaman) " & _
                        " VALUES     (" & Session("sugID") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
            upStat(sqlstr)
            upStat(sqlstr2)
            notes()

        End If
    End Sub
    Private Sub save_Erja2()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterSaveErja2Prc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Session("sugID")

            cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            cmm.Parameters("@sender").Value = Session("UserId")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" پیشنهاد باموفقیت ارجاع داده شد ")
                '  Call save_()
                btnSaveFinal.Visible = False
                PanelRunevest.Enabled = False
                txtNazar.Enabled = False
                btnNazar.Enabled = False
                'btnParaf.Enabled = False
                btnOk.Enabled = False
                'btnWork.Enabled = False
                BtnCancel.Enabled = False

                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub save_Erja()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterSaveErjaPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Session("sugID")

            'cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            'cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            'cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            'cmm.Parameters("@sender").Value = Session("UserId")

            'cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            'cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" پیشنهاد باموفقیت ارجاع داده شد ")
                '  Call save_()
                btnSaveFinal.Visible = False
                PanelRunevest.Enabled = False
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        Dim sqlstr, sqlstr2, sqlstr3 As String

        sqlstr = "  UPDATE RegisterOverhauls  SET   State = 5  WHERE (Id = " & Session("idOver") & ") "
        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""


        payam = txtNazar.Text & "  عدم تایید"


        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"

        sqlstr3 = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
          "VALUES     ('" & Mobile.Text & "', N'تایید نشد', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"
        upStat(sqlstr)
        upStat(sqlstr2)
        upStat(sqlstr3)
        notes()

    End Sub

    Protected Sub btnRunevesht_Click(sender As Object, e As EventArgs) Handles btnRunevesht.Click
        If cmbRunevesht.Value = 0 Then
            cmbRunevesht.Focus()
            msg_box("لطفا گیرنده  را انتخاب نمایید")

            Exit Sub
        Else
            If Val(hdn_tmp.Value) = 0 Then
                hdn_tmp.Value = ClsGetMaxId.GetMaxId("laterRuneveshtTempTbl", "tempID")
            End If
            save_tmp(True, False)
            GridView2.Visible = True
            load_grid_tmp()

        End If

    End Sub
    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)



        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterErjatempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Session("sugID")

            cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            cmm.Parameters("@sender").Value = Session("UserId")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@runevestTypeID", SqlDbType.Int, 10)
            cmm.Parameters("@runevestTypeID").Value = CmbruneveshtType.SelectedValue

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

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub load_grid_tmp()
        Dim sqlstr As String = " SELECT   * from laterRuneveshtTempGridVW where tempID = " & hdn_tmp.Value & ""
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            btnSaveFinal.Visible = True
            GridView2.DataSource = dt
            GridView2.DataBind()
        Else
            btnSaveFinal.Visible = False
            GridView2.Visible = False
        End If

    End Sub
    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from laterRuneveshtTempTbl where idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
    End Sub





    Protected Sub CheckBoxAttach_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAttach.CheckedChanged
        If CheckBoxAttach.Checked = True Then
            'UpdatePanel1.Visible = True
            PanelAttach.Visible = True




            'btnSaveFinal.Visible = True
        Else
            PanelAttach.Visible = False
            'UpdatePanel1.Visible = False
            'btnPuyesh.Visible = False

        End If
    End Sub



    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim sqlstr, sqlstr2 As String

        sqlstr = "  UPDATE RegisterOverhauls  SET   State = 1  WHERE (Id = " & Session("idOver") & ") "
        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""


        payam = txtNazar.Text & "  تایید شد"


        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
        upStat(sqlstr)
        upStat(sqlstr2)
        notes()
    End Sub
End Class
