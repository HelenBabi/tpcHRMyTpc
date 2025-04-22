Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics

Partial Class frmTicketSubmit
    Inherits System.Web.UI.Page
    Dim dtSistem As New DataTable
    Dim az As String = ClsDateTime.GetDateNow
    Dim ta As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String

    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMohem.Text = "تذکّر مهم : اطلاعات از درايوي كه ويندوز نصب شده است (درايو My Document ، C و Desktop كليه كاربران, به درايوهاي ديگر انتقال داده شود ، در غير اين صورت مسئوليت به عهده كاربر مي باشد )"
        chkIAgree.Text = "اینجانب " & Session("lname") & " " & Session("fname") & " " & "متعهد می شوم که اطلاعات این فرم را بادقت پر کردم و درصورت ازبین رفتن اطلاعات مسئولیت بر عهده خودم می باشد"
        If Session("namnull") = "yess" Then
            msg_box("برای استفاده از سیستم باید اطلاعات پروفایل خود را تکمیل کنید")
            Response.Redirect("frmTicketUserEdit.aspx")
        End If
        'If CmbWorkeType.SelectedValue = 0 Then
        '    Panel2.GroupingText = ""
        '    lblTozihat.Visible = False
        '    txt_ans.Visible = False
        'End If
        listeEntezar()


        If Me.IsPostBack = False Then
            load_grid_Systems()
            'load_checkbox()
        End If

    End Sub
    Private Sub load_grid_Systems()
        Dim clssistem_ As New ClsSystem
        'Dim usr As String = Request.LogonUserIdentity.Name
        'usr = Mid(usr, 6, Len(usr)) & "@tpco.com"


        dtSistem = clssistem_.getallsystemfillgrdDomain(Session("userid"))

        GridUserSystem.DataSource = dtSistem
        GridUserSystem.DataBind()
        If dtSistem.Rows.Count > 0 Then
            txtTelfon.Text = dtSistem.Rows(0).Item("telfon")
            txtSystemCode.Text = dtSistem.Rows(0).Item("SystemCode")
            Dim syccode As Integer = dtSistem.Rows(0).Item("SystemCode")
        Else
            msg_box("برای شمادسترسی به کامپیوتری ثبت نشده است اگر سیستمی در اختیار دارید با واحد کامپیوتر تماس بگیرید")
            Panel1.GroupingText = "برای شمادسترسی به کامپیوتری ثبت نشده است اگر سیستمی در اختیار دارید با واحد کامپیوتر تماس بگیرید"

        End If


    End Sub
    Protected Sub GridUserSystem_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridUserSystem.RowCommand
        If e.CommandName = "dell" Then

            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridUserSystem.Rows(radif).Cells(0).Text.ToString)

            Dim ClsB As New ClsSystem
            Dim dt As New DataTable

            dt = ClsB.getsystemgrd(radif)
            txtSystemCode.Text = dt.Rows(0).Item("SystemCode").ToString()
            'txtTelfon.Text = dt.Rows(0).Item("SystemPhone").ToString()

        End If
    End Sub



#Region "All pages"



    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

#End Region
    Protected Sub CmbWorkeType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbWorkeType.SelectedIndexChanged
        'txt_ans.Visible = True
        'load_checkbox()

        'UpdatePanel8.Update()
        If CmbWorkeType.SelectedValue = 1 Then
            lblMohem.Visible = True
            'lblMohem0.Visible = True
            'lblMohem1.Visible = True
            txt_mohem.Visible = True
            txt_Iradat.Visible = True
            chkIAgree.Visible = True
            Panel3.Visible = True
            Panel4.Visible = True
            Panel2.Visible = False

        Else
            lblMohem.Visible = False
            'lblMohem0.Visible = False
            'lblMohem1.Visible = False
            txt_mohem.Visible = False
            txt_Iradat.Visible = False
            chkIAgree.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel2.Visible = True
            txt_ans.Visible = True
        End If
        UpdatePanel10.Update()
        UpdatePanel11.Update()
        UpdatePanel12.Update()
        UpdatePanel13.Update()
        'Panel2.DataBind()
    End Sub

    'Private Sub load_checkbox()
    '    'Dim clsgetdata_ As New ClsDataTable

    '    Dim dt As New DataTable
    '    Dim sqlstr As String = " SELECT     BaseWorkCode, BaseWorkName, mojavez  FROM tblTicketWorks WHERE     (CodeClosed = 0) AND (BaseWorkCode <> 0) and WorkTypeCode= " & CmbWorkeType.SelectedValue & ""
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    CheckBoxList1.DataSource = dt
    '    CheckBoxList1.DataTextField = "BaseWorkName"
    '    CheckBoxList1.DataValueField = "mojavez"
    '    CheckBoxList1.DataBind()
    '    ' UpdatePanel5.Update()

    'End Sub


    'Public Function load_checkboxMojavez(ByVal moj As String) As String
    '    'Dim clsgetdata_ As New ClsDataTable
    '    Dim dt As New DataTable
    '    Dim sqlstr As String = " SELECT     BaseWorkCode, BaseWorkName, mojavez  FROM tblTicketWorks WHERE     (CodeClosed = 0) AND (BaseWorkCode <> 0) and WorkTypeCode= " & CmbWorkeType.SelectedValue & ""
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    CheckBoxList1.DataSource = dt
    '    CheckBoxList1.DataTextField = "BaseWorkName"
    '    CheckBoxList1.DataValueField = "mojavez"
    '    CheckBoxList1.DataBind()

    '    Dim ii As Integer
    '    Dim txtmojvaz As String = ""
    '    For ii = 0 To CheckBoxList1.Items.Count - 1
    '        If CheckBoxList1.Items(ii).Value = True Then
    '            txtmojvaz = txtmojvaz & CheckBoxList1.Items(ii).Text & " ; "

    '        End If

    '    Next
    '    Return txtmojvaz
    'End Function



    Private Sub save_final()

        If CmbWorkeType.SelectedValue = 0 Then

            Panel2.GroupingText = ""

            msg_box(" نوع سرویس را انتخاب کنید")
            CmbWorkeType.Focus()
            Exit Sub
        End If
        'Dim mojavez As Integer = 0

        'Dim txtmojavez As String = ""
        'Dim j As Integer
        'Dim txt As String = ""
        'For j = 0 To CheckBoxList1.Items.Count - 1

        '    If CheckBoxList1.Items(j).Selected = True Then
        '        txt = txt & CheckBoxList1.Items(j).Text & " ; "

        '        If CheckBoxList1.Items(j).Value = True Then
        '            txtmojavez = txtmojavez & CheckBoxList1.Items(j).Text & " ; "
        '            mojavez = 1
        '        End If
        '    End If
        'Next
        If txtRamz.Text = "" Then
            msg_box("رمز ورود به ویندوز را حتما باید وارد کنید ")
            txtRamz.Focus()
            Exit Sub
        End If
        If CmbWorkeType.SelectedValue <> 1 And txt_ans.Text = "" Then
            ' If j = CheckBoxList1.Items.Count And txt = "" Then
            msg_box(" لطفا درخواست خود را  درقسمت توضیحات بنویسید ")
            txt_ans.Focus()
            'CheckBoxList1.Focus()
            Exit Sub
        End If
        ' txt_ans.Text = " ندارد "
        ' End If
        'If txt_ans.Text <> "" And (j = CheckBoxList1.Items.Count And txt = "") Then
        '    txt = "از لیست انتخاب نشده است"
        'End If
        If CmbWorkeType.SelectedValue = 1 Then
            If txt_mohem.Text = "" Then
                msg_box("قسمت اطلاعات مهم را حتما باید پر کنید")
                txt_mohem.Focus()
                Exit Sub
            End If
            If txt_Iradat.Text = "" Then
                msg_box("قسمت شرح ایرادات را حتما باید پر کنید")
                txt_Iradat.Focus()
                Exit Sub
            End If
            If chkIAgree.Checked = False Then
                msg_box("تعهد نامه را بایدبخوانید و تیک بزنید")
                chkIAgree.Focus()
                Exit Sub

            End If

            Dim txtMohem As String = "اطلاعات مهم سیستم :" & txt_mohem.Text & " - "
            Dim txtIradat As String = "مشکل سیستم :" & txt_Iradat.Text
            txt_ans.Text = txt_ans.Text & " - " & txtMohem & " " & txtIradat '& "- رمز:" & txtRamz.Text
        Else
            txt_ans.Text = txt_ans.Text '& "- رمز:" & txtRamz.Text
        End If

        Dim tiketid As Integer = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")

        'If hdn_Final_key.Value = "new" Then txtTicketId.Text = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketSubmit"
            cmm.CommandType = CommandType.StoredProcedure

            'cmm.Parameters.Add("@mojavez", SqlDbType.SmallInt)
            'cmm.Parameters("@mojavez").Value = 0

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = tiketid

            cmm.Parameters.Add("@buildingcode", SqlDbType.Int, 10)
            cmm.Parameters("@buildingcode").Value = 1

            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txtSystemCode.Text

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar)
            cmm.Parameters("@notee").Value = txt_ans.Text.Trim
            'Dim txtrais As String = ""
            'If CmbWorkeType.SelectedValue = 1 Then
            '    txtrais = "مسئول سخت افزار واحد کامپیوتر"
            'Else
            '    txtrais = "رئیس واحد کامپیوتر"
            'End If

            'cmm.Parameters.Add("@MojavezNote", SqlDbType.NVarChar)
            'cmm.Parameters("@MojavezNote").Value = "  نیاز به مجوز دارد وبعد از تایید  " & txtrais & " انجام می شوند  "

            cmm.Parameters.Add("@Typestr", SqlDbType.VarChar, 20)
            cmm.Parameters("@Typestr").Value = CmbWorkeType.SelectedValue


            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 50)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress

            cmm.Parameters.Add("@telfon", SqlDbType.VarChar, 50)
            cmm.Parameters("@telfon").Value = txtTelfon.Text

            cmm.Parameters.Add("@Ramz", SqlDbType.NVarChar, 50)
            cmm.Parameters("@Ramz").Value = txtRamz.Text


            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  درخواست با شماره پیگیری    " & tiketid & "   به واحد کامپیوتر ارسال شد")
                ' Response.Redirect("frmWorkTableUsers.aspx")
                ImageButton2.Enabled = False
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub
    Private Sub CntTyp()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     tblWorkType.WorkTypeCode, tblWorkType.WorkTypeName, COUNT(tblHerja.Typestr) AS CountOfWorkTypeCode " & _
                    "FROM         tblHerja INNER JOIN " & _
                     " tblWorkType ON tblHerja.Typestr = tblWorkType.WorkTypeCode " & _
                     "GROUP BY tblWorkType.WorkTypeCode, tblWorkType.WorkTypeName, tblHerja.stat_ " & _
    "    HAVING(tblHerja.stat_ = 0)"


        dt3 = ClsDataTable.GetDataTable(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count
        For i As Integer = 0 To count_ - 1
            txtcnt += dt3.Rows(i).Item("WorkTypeName").ToString
            txtcnt += " = "
            'If id_type = 1 Then
            '    txtcnt += "سرويس در كارگاه="
            'ElseIf id_type = 2 Then
            '    txtcnt += "ريموت="
            'ElseIf id_type = 3 Then
            '    txtcnt += "سرویس در محل="
            'ElseIf id_type = 4 Then
            '    txtcnt += "سرویس دوره ای="
            'End If
            txtcnt += dt3.Rows(i).Item("CountOfWorkTypeCode").ToString
            txtcnt += "  ;  "
        Next
        lblCnt.Text = "لیست انتظار: " & "   " & txtcnt
    End Sub


    Private Sub listeEntezar()
        Dim dt3 As DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     erja_id         FROM tblHerja      WHERE (stat_ <> 2 And stat_ <> 9) "

        dt3 = ClsDataTable.GetDataTable(sqlstr)

        lblCnt.Text = "  در حال حاضر  " & "   " & dt3.Rows.Count & "  درخواست، در لیست انتظار می باشد  "
    End Sub






    'Protected Sub CheckBoxList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxList1.SelectedIndexChanged
    '    Dim j As Integer
    '    Dim txt As String = ""
    '    For j = 0 To CheckBoxList1.Items.Count - 1
    '        If CheckBoxList1.Items(j).Selected = True Then
    '            txt = txt & CheckBoxList1.Items(j).Text & " و "

    '        End If

    '    Next
    '    txt_ans.Text = txt
    'End Sub




    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        save_final()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'Response.Redirect("frmWorkTableUsers.aspx")
        If Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")

        ElseIf Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")

        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        End If
    End Sub









End Class
