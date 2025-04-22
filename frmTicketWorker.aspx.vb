Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.Drawing


Partial Class frmTicketWorker
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable

    Sub notes()
        Dim dt2 As New DataTable
        Dim sqlstr As String = " SELECT tblUsers.UserCode,tblUsers.UsersAdmin as admn, tblUsers.WorkType as typ, tblUsers.UserPic  as axurl, tblGenderTyp.genValue + ' ' + tblUsers.UserLastName + ' ' + tblUsers.UserFamili + ' ' + tblUsers.semat + ' ' + tblHerjaDtl.date_time AS headd, tblHerjaDtl.notee " & _
                     " FROM         tblHerjaDtl INNER JOIN       tblUsers ON tblHerjaDtl.user_id = tblUsers.UserCode INNER JOIN " & _
                      " tblGenderTyp ON tblUsers.genderr = tblGenderTyp.genId         WHERE(tblHerjaDtl.erja_id = " & Session("tiketId") & ") ORDER BY tblHerjaDtl.note_id DESC"


        dt2 = ClsDataTable.GetDataTableHD(sqlstr)
        DataList1.DataSource = dt2
        DataList1.DataBind()
        HiddenFieldUserID.Value = dt2.Rows(0).Item("UserCode").ToString

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
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketComment"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))

            cmm.Parameters.Add("@notee ", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@notee ").Value = txtNazar.Text

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = HiddenFieldUserID.Value
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session("WorkType") = 9 Or Session("WorkType") = 2 Then
            avgTxt.Visible = True

        End If
        If Session("tiketId") = 0 Then
            txtNazar.Visible = False
            btnNazar.Visible = False
            'CheckFori.Visible = False
            btnWork.Visible = False
            btnEtmam.Visible = False
            btnEntegal.Visible = False
            cmbWorkTyp.Visible = False
            btnSabege.Visible = False
            btnBrwsSys.Visible = False
            imgKartabl.Visible = False
            btnmojavez.Visible = False
            lblComName.Visible = False
            lblUserNam.Visible = False
            txtComNam.Visible = False
            txtUserNam.Visible = False
            cmbParaf.Visible = False
            btnParaf.Visible = False
            txtRamz.Visible = False
            lblRamz.Visible = False
            'btnRemot.Visible = False
            btnping.Visible = False
            txtPing.Visible = False
            'CheckFori.Enabled = False
            CheckEgdam.Visible = False

        Else


            notes()
            If Me.IsPostBack = False Then

                loadItems()
                loadcmbParaf()
            End If
            If Session("fromTyp") = "rpt" Or Session("fromTyp") = "prt" Then
                btnWork.Visible = False
                btnEtmam.Visible = False
                btnEntegal.Visible = False
                cmbWorkTyp.Visible = False
                btnNazar.Visible = False
                txtNazar.Visible = False
                imgKartabl.Visible = False
                CheckEgdam.Enabled = False
                'CheckFori.Enabled = False
                btnmojavez.Visible = False
                cmbParaf.Visible = False
                btnParaf.Visible = False
            ElseIf Session("fromTyp") = "worker" Then
                txtComNam.Enabled = True
                txtUserNam.Enabled = True

                imgKartabl.Visible = False

            ElseIf Session("fromTyp") = "usr" Then
                btnWork.Visible = False
                btnEtmam.Visible = False
                btnEntegal.Visible = False
                cmbWorkTyp.Visible = False
                btnSabege.Visible = False
                btnBrwsSys.Visible = False
                imgKartabl.Visible = False
                btnmojavez.Visible = False
                lblComName.Visible = False
                lblUserNam.Visible = False
                txtComNam.Visible = False
                txtUserNam.Visible = False
                cmbParaf.Visible = False
                btnParaf.Visible = False
                txtRamz.Visible = False
                lblRamz.Visible = False
                'btnRemot.Visible = False
                btnping.Visible = False
                txtPing.Visible = False
                'CheckFori.Enabled = False
                CheckEgdam.Visible = False
            ElseIf Session("fromTyp") = "admn" Then
                btnWork.Visible = False
                btnEtmam.Visible = False
                imgKartabl.Visible = False
                btnmojavez.Visible = False
                CheckEgdam.Visible = False
                txtComNam.Visible = False
                lblComName.Visible = False
                txtUserNam.Visible = False
                lblUserNam.Visible = False
                txtRamz.Visible = False
                lblRamz.Visible = False

            ElseIf Session("fromTyp") = "admnAll" Or Session("fromTyp") = "admnEntegal" Then
                btnWork.Visible = False
                btnEtmam.Visible = False
                btnEntegal.Visible = False
                cmbWorkTyp.Visible = False
                'CheckFori.Enabled = False
                'If Session("stat") = 2 Then
                '    btnNazar.Visible = False
                '    txtNazar.Visible = False
                '    cmbParaf.Visible = False
                '    btnParaf.Visible = False
                'End If
                imgKartabl.Visible = True
                btnmojavez.Visible = False
                CheckEgdam.Enabled = False
                'CheckFori.Enabled = False

            ElseIf Session("fromTyp") = "tiketMande" Then
                btnEtmam.Visible = False
                btnEntegal.Visible = False
                cmbWorkTyp.Visible = False
                'CheckFori.Enabled = False
                'If Session("stat") = 2 Then
                '    btnNazar.Visible = False
                '    txtNazar.Visible = False
                '    cmbParaf.Visible = False
                '    btnParaf.Visible = False
                'End If
                imgKartabl.Visible = True
                btnmojavez.Visible = False
                CheckEgdam.Enabled = False
                'CheckFori.Enabled = False
                btnParaf.Visible = False
                cmbParaf.Visible = False
                txtNazar.Visible = False
                btnNazar.Visible = False

            End If


        End If





    End Sub

    Private Sub loadcmbParaf()
        Dim dtg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT idd,valuee FROM tblTicketTemplate WHERE     (CodeClosed = 0) and userId= " & Session("userid") & ""
        dtg = ClsDataTable.GetDataTableHD(sqlstr)

        cmbParaf.DataSource = dtg
        cmbParaf.DataTextField = "valuee"
        cmbParaf.DataValueField = "idd"
        cmbParaf.DataBind()

    End Sub






    Private Sub entegal()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()
        Dim typcod As Integer = cmbWorkTyp.SelectedValue
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketEntegal"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))

            cmm.Parameters.Add("@typvalue ", SqlDbType.NVarChar, 10)
            cmm.Parameters("@typvalue ").Value = cmbWorkTyp.SelectedItem.ToString

            cmm.Parameters.Add("@typ", SqlDbType.Int, 10)
            cmm.Parameters("@typ").Value = cmbWorkTyp.SelectedValue

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@fori", SqlDbType.Bit, 1)
            cmm.Parameters("@fori").Value = 0


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

                '   msg_box("  درخواست شماره  " + Session("tiketId") + "  به واحد   " + cmbWorkTyp.SelectedItem.ToString + "   ارجا شد  ")
                '  lblhoshdar.Text = "  درخواست شماره  " + Session("tiketId") + "  به واحد   " + cmbWorkTyp.SelectedItem.ToString + "   ارجا شد  "
                ' Response.Redirect("frmWorkTable.aspx")
                notes()

            Else
                lblhoshdar.Text = cmm.Parameters("@pm").Value
                'msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            lblhoshdar.Text = ex.Message.ToString
            '   MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub entegal2()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()
        Dim typcod As Integer = cmbWorkTyp.SelectedValue
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketEntegal2"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))

            cmm.Parameters.Add("@typvalue ", SqlDbType.NVarChar, 10)
            cmm.Parameters("@typvalue ").Value = cmbWorkTyp.SelectedItem.ToString

            cmm.Parameters.Add("@typ", SqlDbType.Int, 10)
            cmm.Parameters("@typ").Value = Session("WorkType")

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            ''''''''''''''''''''''''''''''
            cmm.Parameters.Add("@mot", SqlDbType.Int, 1)
            If Session("fromTyp") = "motfrege" Then

                cmm.Parameters("@mot").Value = 1
            Else

                cmm.Parameters("@mot").Value = 0

            End If



            '''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@fori", SqlDbType.Bit, 1)
            cmm.Parameters("@fori").Value = 0


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

                '   msg_box("  درخواست شماره  " + Session("tiketId") + "  به واحد   " + cmbWorkTyp.SelectedItem.ToString + "   ارجا شد  ")
                '  lblhoshdar.Text = "  درخواست شماره  " + Session("tiketId") + "  به واحد   " + cmbWorkTyp.SelectedItem.ToString + "   ارجا شد  "
                ' Response.Redirect("frmWorkTable.aspx")
                notes()

            Else
                lblhoshdar.Text = cmm.Parameters("@pm").Value
                'msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            lblhoshdar.Text = ex.Message.ToString
            '   MsgBox(ex.Message.ToString)
        End Try

    End Sub


    'Private Sub chekwork()
    '    Dim sqlstr2, sqlstr3 As String
    '    sqlstr2 = "select erja_id from tblWorkHdr where erja_id= " & txtTicketId.Text & ""

    '    Dim dt5 As New DataTable
    '    dt5 = ClsDataTable.GetDataTableHD(sqlstr2)
    '    If dt5.Rows.Count = 0 Then
    '        sqlstr3 = " INSERT INTO tblWorkHdr (SystemCode, erja_id, WorkDateS, WorkTimeS, WorkerCodeP, WorkerCodeS, TypeOpCode, WorkTypeCode, WorkDateE, WorkTimeE, WorkNote, opid) " & _
    '              " VALUES    (" & txtSystemCode.Text & ", " & txtTicketId.Text & ", '" & dtTicket.Rows(0).Item("date_s") & "', '" & dtTicket.Rows(0).Item("TIME_S") & "', " & Session("userid") & ", " & Session("userid") & ", 0, " & dtTicket.Rows(0).Item("Typestr") & ", '" & ClsDateTime.GetDateNow & "', '" & ClsDateTime.GetTimeNow & "', 'بدون ثبت ریزکار،درخواست بسته شده است', 0)"
    '        upStat(sqlstr3)
    '    End If
    'End Sub


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




    'Protected Sub btnParaf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnParaf.Click
    '    If cmbParaf.SelectedIndex < 0 Then
    '        msg_box("از منوی تعاریف پایه، پاراف خود را تنظیم نمایید")
    '        Exit Sub
    '    End If
    '    txtNazar.Text = txtNazar.Text & " " & cmbParaf.SelectedItem.ToString
    'End Sub



    Protected Sub btnmojavez_Click(sender As Object, e As EventArgs) Handles btnmojavez.Click
        Dim sqlstr, sqlstr2 As String

        sqlstr = "  UPDATE tblHerja  SET   stat_ = 6 , mojavez = 1  " & _
                              " WHERE (erja_id = " & Session("tiketId") & ") "
        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""

        If hdnType.Value = 1 Then
            payam = txtNazar.Text & "  به مجوز نیاز دارد و بعد از تایید مسئول/سرپرست سخت افزار قابل انجام می باشد "
        Else
            payam = txtNazar.Text & "  به مجوز نیاز دارد و بعد از تایید رئیس واحد کامپیوتر قابل انجام می باشد "
        End If


        sqlstr2 = "INSERT INTO    tblHerjaDtl ( erja_id, notee, user_id, date_time) " & _
                    " VALUES     (" & Session("tiketId") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
        upStat(sqlstr)
        upStat(sqlstr2)
        notes()
        txtNazar.Text = ""
    End Sub




    Private Sub loadItems()
        Dim ClsTicketWorker As New ClsTicket
        dtTicket = ClsTicketWorker.GetBrowsTicketWorker(Session("tiketId"))
        txtTicketId.Text = Session("tiketId")
        txtSystemCode.Text = dtTicket.Rows(0).Item("systemcode")
        txtTelfon.Text = dtTicket.Rows(0).Item("telfon")
        Session("systemcode") = dtTicket.Rows(0).Item("systemcode")
        'Session("stat") = dtTicket.Rows(0).Item("stat_")
        txtComNam.Text = dtTicket.Rows(0).Item("ComputerName")
        txtUserNam.Text = dtTicket.Rows(0).Item("UserName_")
        txtRamz.Text = dtTicket.Rows(0).Item("ramz")
        typ.Value = dtTicket.Rows(0).Item("Typestr").ToString
        'CheckFori.Checked = dtTicket.Rows(0).Item("fori").ToString
        tim.Value = dtTicket.Rows(0).Item("TIME_S").ToString
        dat.Value = dtTicket.Rows(0).Item("date_s").ToString
        hdnType.Value = dtTicket.Rows(0).Item("Typestr").ToString
        txt_holder.Text = dtTicket.Rows(0).Item("holder").ToString
        If dtTicket.Rows(0).Item("stat_").ToString = 3 Then
            CheckEgdam.Checked = True
        End If
        tarixUpInfo.Text = dtTicket.Rows(0).Item("zamanUp").ToString
        If tarixUpInfo.Text = " " Then
            tarixUpInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("red")

        End If
        If dtTicket.Rows(0).Item("blok") = True Then
            msg_box("این سیستم در لیست سیاه است")
            txtNazar.Visible = False
            btnNazar.Visible = False
            'CheckFori.Visible = False
            btnWork.Visible = False
            btnEtmam.Visible = False
            btnEntegal.Visible = False
            cmbWorkTyp.Visible = False
            btnSabege.Visible = False
            btnBrwsSys.Visible = False
            imgKartabl.Visible = False
            btnmojavez.Visible = False
            lblComName.Visible = False
            lblUserNam.Visible = False
            txtComNam.Visible = False
            txtUserNam.Visible = False
            cmbParaf.Visible = False
            btnParaf.Visible = False
            txtRamz.Visible = False
            lblRamz.Visible = False
            'btnRemot.Visible = False
            btnping.Visible = False
            txtPing.Visible = False
            'CheckFori.Enabled = False
            CheckEgdam.Visible = False
        End If
    End Sub

    Private Sub etmamm()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim typcod As Integer = cmbWorkTyp.SelectedValue
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketEtmam"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))

            cmm.Parameters.Add("@Typestr", SqlDbType.Int, 5)
            cmm.Parameters("@Typestr").Value = typ.Value

            cmm.Parameters.Add("@WorkTimeS", SqlDbType.VarChar, 10)
            cmm.Parameters("@WorkTimeS").Value = tim.Value

            cmm.Parameters.Add("@WorkDateS", SqlDbType.VarChar, 10)
            cmm.Parameters("@WorkDateS").Value = dat.Value

            cmm.Parameters.Add("@systemCode", SqlDbType.Int, 10)
            cmm.Parameters("@systemCode").Value = txtSystemCode.Text

            cmm.Parameters.Add("@avgTime", SqlDbType.Int, 5)
            cmm.Parameters("@avgTime").Value = Val(avgTxt.Text)

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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

                '  msg_box(" ثبت شد  ")
                ' Response.Redirect("frmWorkTable.aspx")
                notes()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Protected Sub btnSabege_Click(sender As Object, e As System.EventArgs) Handles btnSabege.Click
        Dim furl As String = "frmSabeghe.aspx?PwhereSTR=" & Val(txtSystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=400,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub btnBrwsSys_Click(sender As Object, e As System.EventArgs) Handles btnBrwsSys.Click
        Dim furl As String = "frmTicketSysInfo.aspx?PwhereSTR=" & Val(txtSystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=800,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub btnEtmam_Click(sender As Object, e As EventArgs) Handles btnEtmam.Click

        If Session("WorkType") = 9 Or Session("WorkType") = 2 Then
            If avgTxt.Text = "" Then
                msg_box("لطفا میانگین زمان اتمام را وارد نمایید")
                avgTxt.BackColor = Drawing.Color.Yellow

                avgTxt.Focus()
            Else
                etmamm()
                btnEtmam.Enabled = False
            End If

        Else
            entegal2()
            btnEtmam.Enabled = False
        End If





        'chekwork()
    End Sub

    Protected Sub btnWork_Click(sender As Object, e As EventArgs) Handles btnWork.Click
        Response.Redirect("frmTicketSystemSof.aspx")
    End Sub

    Protected Sub btnEntegal_Click(sender As Object, e As EventArgs) Handles btnEntegal.Click
        entegal()
        'If CheckFori.Checked = True Then
        '    Dim dtt As New DataTable
        '    Dim sqlstr As String = "SELECT     UserMobile    FROM tblUsers " & _
        '         " WHERE     (WorkType = " & cmbWorkTyp.SelectedValue & ") AND (smss = 1) AND (UserMobile <> '') AND (CodeClosed = 0) "
        '    dtt = ClsDataTable.GetDataTableHD(sqlstr)
        '    If dtt.Rows.Count > 0 Then
        '        For i = 0 To dtt.Rows.Count - 1
        '            mobile.Text = dtt.Rows(i).Item("UserMobile").ToString
        '            sendSms()
        '        Next i

        '    End If

        'End If
    End Sub

    Protected Sub btnping_Click(sender As Object, e As EventArgs) Handles btnping.Click
        Try

            txtPing.Text = ""
            ' lblStatus.Text = ""
            Dim ping As Ping = New Ping
            Dim pingreply As PingReply = ping.Send(txtComNam.Text)
            '  Dim pingreply As PingReply = ping.Send("pm")
            ' If pingreply.Status = True Then
            txtPing.Text = pingreply.Address.ToString


        Catch ex As Exception
            'msg_box(ex.Message)
            txtPing.Text = "پینگ نمی شود"
        End Try




  
    'Protected Sub btnRemot_Click(sender As Object, e As EventArgs) Handles btnRemot.Click
    '    'Process.Start("Cmd.exe", "/C shutdown.exe \\RemoteComputerName/r")
    '    Process.Start("mstsc.exe")
    'End Sub

    End Sub
    Private Sub sendSms()

        Dim matn As String = " درخواست " & txtTicketId.Text & " نوع فوری به کارتابل شما ارسال شد "
        Dim usernam As String = ConfigurationManager.AppSettings("userS")
        Dim pass As String = ConfigurationManager.AppSettings("pasS")
        Dim nos As String = ConfigurationManager.AppSettings("noS")


        'Dim furl As String = "http://www.afe.ir/Url/SendSMS?Username=" & usernam & "&Password=" & pass & "&Number=" & nos & "&Mobile=" & mobile.Text & "&SMS=" & matn & ""
        ''ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=200,width=300,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
        'Server.Transfer(furl)
    End Sub

    Protected Sub CheckEgdam_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEgdam.CheckedChanged
        If CheckEgdam.Checked = True Then

            txtNazar.Text = "در دست اقدام"
            save_final()
            notes()
            txtNazar.Text = ""

            Dim sqlstrEgdam As String = " UPDATE    tblHerja    set stat_ = 3  WHERE     (erja_id = " & Session("tiketId") & ")"
            upStat(sqlstrEgdam)
            'Dim sqlstrinsert = "INSERT INTO tblHerjaDtl   ( erja_id, notee, user_id, date_time) " & _
            '          " VALUES     (" & Session("tiketId") & ", 'در دست اقدام', " & Session("userID") & ", '" & ClsDateTime.GetDateNow & "' + ' ' + '" & ClsDateTime.GetTimeNow & "' )"
            'upStat(sqlstrinsert)
            'notes()

        Else
            Dim sqlstrEgdam As String = " UPDATE    tblHerja    set stat_ = 4  WHERE     (erja_id = " & Session("tiketId") & ")"
            upStat(sqlstrEgdam)

        End If
    End Sub

    Protected Sub btnParaf_Click(sender As Object, e As EventArgs) Handles btnParaf.Click
        If cmbParaf.SelectedIndex < 0 Then
            msg_box("از منوی تعاریف پایه، پاراف خود را تنظیم نمایید")
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
        Session("tiketId") = 0

        Response.Redirect("frmWorkTableUsers.aspx")

    End Sub

    Protected Sub imgKartabl_Click(sender As Object, e As EventArgs) Handles imgKartabl.Click
        Session("tiketId") = 0
        If Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")
        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        ElseIf Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")
        End If
    End Sub

  
End Class
