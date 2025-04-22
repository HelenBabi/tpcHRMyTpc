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


Partial Class sugDetailConversationFrm
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        gridErja()
        notes()
        load_grid_FAKTOR()
        If Me.IsPostBack = False Then
            loadComboTyp()
            loadItems()
            loadcmbParaf()
            loadComboRuneveshtTyp()
        End If
        If Session("from") = "user" Then
            btnWork.Visible = False
            btnEtmam.Visible = False



            'CheckFori.Enabled = False
            btnBrwsSys.Visible = False

            cmbParaf.Visible = False
            btnParaf.Visible = False
            CheckBoxRunevesht.Visible = False
        ElseIf Session("from") = "admin" Then
            btnBrwsSys.Visible = True

            cmbParaf.Visible = True
            btnParaf.Visible = True
            CheckBoxRunevesht.Visible = True

        ElseIf Session("from") = "reciver" Then
            btnBrwsSys.Visible = False

            cmbParaf.Visible = False
            btnParaf.Visible = False
            CheckBoxRunevesht.Visible = True
            btnBrwsSys.Visible = False
            btnWork.Visible = False
            btnEtmam.Visible = False
        End If



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
            PanelErja.Visible = True
            GridViewErja.DataSource = dt2
            GridViewErja.DataBind()
        End If


    End Sub
    Sub notes()
        Dim dt2 As New DataTable
        Dim sqlstr As String = " SELECT       ID, codePersoneli, headd, sugID, notee, note_id, REPLACE([pic ], ' ', '') AS picc from sugCommentVW WHERE        (sugID = " & Session("sugID") & ")  ORDER BY note_id DESC"



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
            cmm.CommandText = "sugCommentPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@sugID", SqlDbType.Int)
            cmm.Parameters("@sugID").Value = Session("sugID")

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@notee").Value = txtNazar.Text

            cmm.Parameters.Add("@PerID", SqlDbType.Int, 10)
            cmm.Parameters("@PerID").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Dim frm As String = ""

            If Session("modir") = True Then
                frm = "modir"
            Else
                frm = Session("from")
            End If


            cmm.Parameters.Add("@userType", SqlDbType.VarChar, 7)
            cmm.Parameters("@userType").Value = frm




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
    Private Sub loadComboTyp()
        Dim sqlstr As String = "SELECT        TOP (200) sugTypeID, sugTypeName, state " & _
        " FROM sugTypeTbl WHERE        (state = 1)"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        sugType.DataSource = dt
        sugType.DataTextField = "sugTypeName"
        sugType.DataValueField = "sugTypeID"
        sugType.DataBind()


    End Sub

    Private Sub loadcmbParaf()
        Dim dtg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT idd,valuee FROM tblTicketTemplate WHERE     (CodeClosed = 0) and userId= " & Session("userid") & ""
        dtg = ClsDataTable.GetDataTable(sqlstr)

        cmbParaf.DataSource = dtg
        cmbParaf.DataTextField = "valuee"
        cmbParaf.DataValueField = "idd"
        cmbParaf.DataBind()

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





    'Protected Sub btnmojavez_Click(sender As Object, e As EventArgs) Handles btnEntegal.Click
    '    Dim sqlstr, sqlstr2 As String

    '    sqlstr = "  UPDATE tblHerja  SET   stat_ = 6 , mojavez = 1  " & _
    '                          " WHERE (erja_id = " & Session("tiketId") & ") "
    '    Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

    '    Dim payam As String = ""

    '    If hdnType.Value = 1 Then
    '        payam = txtNazar.Text & "  به مجوز نیاز دارد و بعد از تایید مسئول/سرپرست سخت افزار قابل انجام می باشد "
    '    Else
    '        payam = txtNazar.Text & "  به مجوز نیاز دارد و بعد از تایید رئیس واحد کامپیوتر قابل انجام می باشد "
    '    End If


    '    sqlstr2 = "INSERT INTO    tblHerjaDtl ( erja_id, notee, user_id, date_time) " & _
    '                " VALUES     (" & Session("tiketId") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
    '    upStat(sqlstr)
    '    upStat(sqlstr2)
    '    notes()
    '    txtNazar.Text = ""
    'End Sub




    Private Sub loadItems()
        Dim dt As DataTable

        Dim sqlstrr As String = "SELECT *  FROM   sugGrideUserVW WHERE        (sugID = " & Session("sugID") & ")"
        dt = ClsDataTable.GetDataTable(sqlstrr)
        If dt.Rows.Count > 0 Then

            subject.Text = dt.Rows(0).Item("subject")
            sugType.SelectedValue = dt.Rows(0).Item("sugType")
            sugIncome.Text = dt.Rows(0).Item("sugIncome")
            realIncom.Text = dt.Rows(0).Item("realIncom")
            DescStatus.Text = dt.Rows(0).Item("DescStatus")
            sugMethod.Text = dt.Rows(0).Item("sugMethod")
            benefits.Text = dt.Rows(0).Item("benefits")
            Description.Text = dt.Rows(0).Item("Description")
            pishnahaddahande.Text = dt.Rows(0).Item("karbar")
            partners.Text = dt.Rows(0).Item("partners")
        End If
    End Sub

    
   

    Protected Sub btnBrwsSys_Click(sender As Object, e As System.EventArgs) Handles btnBrwsSys.Click
        Dim sqlstr, sqlstr2 As String

        sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 2  WHERE (sugID = " & Session("sugID") & ") "
        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""


        payam = txtNazar.Text & " پیشنهاد تایید شد"
      

        sqlstr2 = "INSERT INTO    sugCommentTbl ( sugID, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("sugID") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
        upStat(sqlstr)
        upStat(sqlstr2)
        notes()


    End Sub

    

    Protected Sub btnWork_Click(sender As Object, e As EventArgs) Handles btnWork.Click
        Dim sqlstr, sqlstr2 As String

        sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 4  WHERE (sugID = " & Session("sugID") & ") "
        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""


        payam = txtNazar.Text & " نیاز به توضیحات بیشتر"


        sqlstr2 = "INSERT INTO    sugCommentTbl ( sugID, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("sugID") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
        upStat(sqlstr)
        upStat(sqlstr2)
        notes()
    End Sub

    Protected Sub btnParaf_Click(sender As Object, e As EventArgs) Handles btnParaf.Click
        If cmbParaf.SelectedIndex < 0 Then
            msg_box("لطفا پاراف خود را تنظیم نمایید")
            Exit Sub
        End If
        txtNazar.Text = txtNazar.Text & " " & cmbParaf.SelectedItem.ToString
    End Sub



    Protected Sub btnNazar_Click(sender As Object, e As EventArgs) Handles btnNazar.Click
        save_final()
        notes()
        txtNazar.Text = ""
    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
        Session("sugID") = 0
        If Session("from") = "user" Then
            Response.Redirect("sugMyWorkTableFrm.aspx")
        ElseIf Session("from") = "admin" Then
            Response.Redirect("sugWorkTableAllFrm.aspx")

        ElseIf Session("from") = "reciver" Then
            Response.Redirect("sugWorkTableFrm.aspx")
        End If



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
                btnParaf.Enabled = False
                btnBrwsSys.Enabled = False
                btnWork.Enabled = False
                btnEtmam.Enabled = False

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

    Protected Sub btnEtmam_Click(sender As Object, e As EventArgs) Handles btnEtmam.Click
        Dim sqlstr, sqlstr2 As String

        sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 3  WHERE (sugID = " & Session("sugID") & ") "
        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""


        payam = txtNazar.Text & " پیشنهاد تایید نمی شود"


        sqlstr2 = "INSERT INTO    sugCommentTbl ( sugID, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("sugID") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
        upStat(sqlstr)
        upStat(sqlstr2)
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

    Protected Sub btnSabtFile_Click(sender As Object, e As EventArgs) Handles btnSabtFile.Click
        'If cmbRunevesht.Value = 0 Then
        '    msg_box("لطفا بعد از مشخص نمودن گیرنده ارجاع فایل ضمیمه را اضافه نمایید")
        '    cmbRunevesht.Focus()
        '    Exit Sub
        'Else
        If FileUpload.HasFile = False Then
            msg_box("فایل انتخاب نشده است")
            Exit Sub
        Else
            save_file()




        End If

        'End If

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


            cmm.CommandText = "sugFileInsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Session("sugID")


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

                'UpdatePanel1.Visible = True
                load_grid_FAKTOR()
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

        sqlstr = "SELECT       documentsTbl.docType, documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name, documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid, " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib,  " & _
                         "personel.nam + ' ' + personel.family AS karbar  " & _
                           " FROM            documentsTbl INNER JOIN  " & _
                         " personel ON documentsTbl.userid = personel.ID  " & _
        " WHERE(documentsTbl.fileID = " & Session("sugID") & ") and documentsTbl.docType = 11 " & _
                       "ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

    End Sub

    Protected Sub CheckBoxAttach_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAttach.CheckedChanged
        If CheckBoxAttach.Checked = True Then
            'UpdatePanel1.Visible = True
            PanelAttach.Visible = True

            load_grid_FAKTOR()


            'btnSaveFinal.Visible = True
        Else
            PanelAttach.Visible = False
            'UpdatePanel1.Visible = False
            'btnPuyesh.Visible = False

        End If
    End Sub

   
    
End Class
