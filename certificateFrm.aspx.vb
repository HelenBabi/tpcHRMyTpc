Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration

Partial Class certificateFrm
    Inherits System.Web.UI.Page
    Dim dtaspx As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        meno_acc()
        'End If
        If Me.IsPostBack = False Then

        End If


    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("certificateFrm.aspx", Val(Session("UserId")))
        If meno_arr(0) = False Then
            Response.Redirect("login_2.aspx")
        End If
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

    Protected Sub gridSabege_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles gridSabege.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat"))


        If value = 5 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightPink
            'lblHoshdar.Visible = True

        End If


    End Sub









    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ibtnSave.Visible = True
        lblHoshdar.Text = "فرم ثبت گواهی"
        If codepersonel.Text = "" Then
            msg_box("کد پرسنلی را وارد کنید")

            codepersonel.Focus()
            Exit Sub
        End If

        'salkar.Text = 0
        'mahkar.Text = 0
        'ruzkar.Text = 0
        'salsabege.Text = 0
        'mahsabege.Text = 0
        'ruzsabege.Text = 0
        'salzian.Text = 0
        'mahzian.Text = 0
        'ruzzian1.Text = 0
        'salsaxtno.Text = 0
        'mahsaxno.Text = 0
        'ruzsaxtno.Text = 0
        txtMozd.Text = ""
        'salsaxtsher.Text = 0
        'mahsaxtsher.Text = 0
        'ruzsaxtsher.Text = 0
        'Panel4.Visible = True
        'Panel5.Visible = True
        'Panel6.Visible = True
        'Panel7.Visible = True
        'Panel8.Visible = True

        lblbank.Visible = True
        lblShobe.Visible = True
        CmbBank.Visible = True
        CmbShobe.Visible = True

        txtGirande.Text = ""
        txtSazman.Text = ""

        lblSazman.Visible = False
        txtSazman.Visible = False


        loadperson()



    End Sub




    Private Sub loadperson()
        Dim dsh2 As New DataTable

        Dim sqlstr As String

        sqlstr = "select * from perInfoVW where codePersoneli = " & Val(codepersonel.Text) & " "
        dsh2 = ClsDataTable.GetDataTable(sqlstr)
        If dsh2.Rows.Count > 0 Then

            TextBox2.Text = dsh2.Rows(0).Item("nam").ToString & " " & dsh2.Rows(0).Item("family").ToString
            TextBox5.Text = dsh2.Rows(0).Item("tarixestexdam").ToString
            TextBox9.Text = dsh2.Rows(0).Item("postname").ToString
            TextBox20.Text = dsh2.Rows(0).Item("codebime").ToString
            TextBox11.Text = dsh2.Rows(0).Item("madrak").ToString
            TextBox1.Text = dsh2.Rows(0).Item("value").ToString
            lblHoshdar.Text = "فرم ثبت گواهی"
            perid.Text = dsh2.Rows(0).Item("ID").ToString
            txtMozd.Text = dsh2.Rows(0).Item("mozd").ToString
            loadGridSabege()
            cmbShobe.Enabled = True
            'loadsabege()
            'loadzian()
            'loadkarkard()
            'loadziansherkati()
            'loadziansherkatiNO()
            hdnType.Value = dsh2.Rows(0).Item("typ").ToString
            gavahiTypeLoad()

            bankLoad()

            'cmbBank. = 8
            shobeLoad2()
            'cmbShobe.Value = 3
            If dsh2.Rows(0).Item("CodeClosed").ToString = True Then
                msg_box("پرسنل مربوطه بازنشسته می باشد")
                lblHoshdar.Text = "پرسنل مربوطه بازنشسته می باشد"

            End If
        Else
            'MsgBox("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
            msg_box("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
        End If
    End Sub




    Private Sub bankLoad()

        CmbBank.Enabled = True


        Dim sqlstr As String

        sqlstr = "SELECT     * from bank_  WHERE   (CodeClosed = 0) ORDER BY status"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbBank.DataSource = dt
        cmbBank.ValueField = "bank_id"
        cmbBank.TextField = "bank_name"
        CmbBank.DataBind()
        'shobeLoad()
    End Sub

    Private Sub shobeLoad2()

        CmbShobe.Enabled = True


        Dim sqlstr As String

        sqlstr = "SELECT         shobe_id, shobe_nam, bank_id " & _
       " FROM shobe "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbShobe.DataSource = dt
        cmbShobe.ValueField = "shobe_id"
        cmbShobe.TextField = "shobe_nam"
        CmbShobe.DataBind()




    End Sub

    Private Sub loadGridSabege()



        Dim sqlstr As String

        sqlstr = "SELECT     * from sabegeVW  WHERE person_cod = " & codepersonel.Text & " "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        gridSabege.DataSource = dt
        gridSabege.DataBind()
        lblCntGavahi.Text = dt.Rows.Count
        If Val(lblCntGavahi.Text) > 2 Then
            lblCntGavahi.BackColor = Drawing.Color.Red
        ElseIf Val(lblCntGavahi.Text) > 1 Then
            lblCntGavahi.BackColor = Drawing.Color.Yellow
        ElseIf Val(lblCntGavahi.Text) = 1 Then
            lblCntGavahi.BackColor = Drawing.Color.GreenYellow
        End If
        For i As Integer = 0 To Val(lblCntGavahi.Text) - 1
            If Val(dt.Rows(i).Item("stat").ToString) = 5 Then
                msg_box("این پرسنل گواهی برگشت از بانک دارد")
                lblHoshdar.Text = "این پرسنل گواهی برگشت از بانک دارد"
                ibtnSave.Visible = False
            End If
        Next i


    End Sub


    'Private Sub loadkarkard()


    '    Dim dts As New DataTable
    '    Dim sqlstr As String

    '    sqlstr = "SELECT     dbo.GetYearsCountKar(perID) AS sal, dbo.GetMounthsCountKar(perID) AS mah, dbo.GetDaysCountKar(perID) AS ruz " & _
    '   " FROM savabeg         WHERE(perid = " & perid.Text & ")"

    '    dts = ClsDataTable.GetDataTable(sqlstr)
    '    If dts.Rows.Count > 0 Then
    '        salkar.Text = dts.Rows(0).Item("sal").ToString
    '        mahkar.Text = dts.Rows(0).Item("mah").ToString
    '        ruzkar.Text = dts.Rows(0).Item("ruz").ToString
    '    End If


    'End Sub

    'Private Sub loadsabege()


    '    Dim dts As New DataTable
    '    Dim sqlstr As String

    '    sqlstr = "SELECT     dbo.GetYearsCount(perID) AS sal, dbo.GetMounthsCount(perID) AS mah, dbo.GetDaysCount(perID) AS ruz " & _
    '   " FROM savabeg         WHERE(perid = " & perid.Text & ")"

    '    dts = ClsDataTable.GetDataTable(sqlstr)
    '    If dts.Rows.Count > 0 Then
    '        salsabege.Text = dts.Rows(0).Item("sal").ToString
    '        mahsabege.Text = dts.Rows(0).Item("mah").ToString
    '        ruzsabege.Text = dts.Rows(0).Item("ruz").ToString
    '    End If


    'End Sub
    'Private Sub loadzian()


    '    Dim dts As New DataTable
    '    Dim sqlstr As String

    '    sqlstr = "SELECT     dbo.GetYearsCountSaxt(savabeg.perID) AS sal, dbo.GetMounthsCountSaxt(savabeg.perID) AS mah, dbo.GetDaysCountSaxt(savabeg.perID) AS ruz " & _
    '  " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
    '       " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

    '    dts = ClsDataTable.GetDataTable(sqlstr)
    '    If dts.Rows.Count > 0 Then
    '        salzian.Text = dts.Rows(0).Item("sal").ToString
    '        mahzian.Text = dts.Rows(0).Item("mah").ToString
    '        ruzzian1.Text = dts.Rows(0).Item("ruz").ToString
    '    End If


    'End Sub

    'Private Sub loadziansherkati()


    '    Dim dts As New DataTable
    '    Dim sqlstr As String

    '    sqlstr = "SELECT     dbo.GetYearsCountSaxtsherkati(savabeg.perID) AS sal, dbo.GetMounthsCountSaxtsherkati(savabeg.perID) AS mah, dbo.GetDaysCountSaxtSherkati(savabeg.perID) AS ruz " & _
    '  " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
    '       " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

    '    dts = ClsDataTable.GetDataTable(sqlstr)
    '    If dts.Rows.Count > 0 Then
    '        salsaxtsher.Text = dts.Rows(0).Item("sal").ToString
    '        mahsaxtsher.Text = dts.Rows(0).Item("mah").ToString
    '        ruzsaxtsher.Text = dts.Rows(0).Item("ruz").ToString
    '    End If


    'End Sub

    'Private Sub loadziansherkatiNO()


    '    Dim dts As New DataTable
    '    Dim sqlstr As String

    '    sqlstr = "SELECT     dbo.GetYearsCountSaxtsherkatiNo(savabeg.perID) AS sal, dbo.GetMounthsCountSaxtsherkatiNo(savabeg.perID) AS mah, dbo.GetDaysCountSaxtSherkatiNo(savabeg.perID) AS ruz " & _
    '  " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
    '       " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

    '    dts = ClsDataTable.GetDataTable(sqlstr)
    '    If dts.Rows.Count > 0 Then
    '        salsaxtno.Text = dts.Rows(0).Item("sal").ToString
    '        mahsaxno.Text = dts.Rows(0).Item("mah").ToString
    '        ruzsaxtno.Text = dts.Rows(0).Item("ruz").ToString
    '    End If


    'End Sub











    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")


    End Sub


    Private Sub clear_form()
        TextBox2.Text = ""

        TextBox20.Text = ""

        TextBox11.Text = ""
        TextBox1.Text = ""
        TextBox9.Text = ""
        TextBox5.Text = ""

        gridSabege.DataBind()

    End Sub




    Protected Sub ibtnPrint_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnPrint.Click
        printt()

    End Sub
    Private Sub printt()
        Dim crystalReport As New ReportDocument()

        'Dim masir As String = Application.StartupPath()

        'tarixJari = ClsDateTime.GetDateNow
        'az = reportDayli.az.Text
        'ta = reportDayli.ta.Text
        'Dim resnam As String = "'"
        'If userType = 1 Then
        '    resnam = "فاکتور رستوران"
        'Else
        '    resnam = "فاکتور کافه سنتی"
        'End If



        Dim sqlstr As String = " SELECT   * from perInfoGavahiVW WHERE        (new_later_num = " & hdn_Final_key.Value & ") "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        Dim jens As String = "آقای"
        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("jens").ToString = 2 Then
                jens = "خانم"

            End If


            crystalReport.Load(Server.MapPath("~/reports/" & dt.Rows(0).Item("report").ToString))


            Dim nam As String = TextBox2.Text

            crystalReport.SetParameterValue("jens", jens)

            crystalReport.SetParameterValue("nam", dt.Rows(0).Item("nam").ToString)
            crystalReport.SetParameterValue("family", dt.Rows(0).Item("family").ToString)

            crystalReport.SetParameterValue("father", dt.Rows(0).Item("father").ToString)
            crystalReport.SetParameterValue("shsh", dt.Rows(0).Item("shsh").ToString)

            crystalReport.SetParameterValue("mahalesodur", dt.Rows(0).Item("mahalesodur").ToString)
            crystalReport.SetParameterValue("codeMelli", dt.Rows(0).Item("codeMelli").ToString)

            crystalReport.SetParameterValue("mozd", dt.Rows(0).Item("naxales").ToString)

            crystalReport.SetParameterValue("moasese", dt.Rows(0).Item("moassese").ToString)

            crystalReport.SetParameterValue("bank", dt.Rows(0).Item("bank_nam").ToString)
            crystalReport.SetParameterValue("shobe", dt.Rows(0).Item("shobe_nam").ToString)

            crystalReport.SetParameterValue("contartType", dt.Rows(0).Item("value").ToString)

            crystalReport.SetParameterValue("codePersoneli", codepersonel.Text)

            crystalReport.SetParameterValue("tarix", dt.Rows(0).Item("latter_date").ToString)

            Dim sqlstrm As String = " SELECT   * from managerTbl WHERE        (CodeClosed = 0)  "
            Dim dtm As New DataTable
            dtm = ClsDataTable.GetDataTable(sqlstrm)
            crystalReport.SetParameterValue("manager", dtm.Rows(0).Item("mangerName").ToString)
            crystalReport.SetParameterValue("managerPost", dtm.Rows(0).Item("managerPost").ToString)


            Dim formatType As ExportFormatType = ExportFormatType.NoFormat
            formatType = ExportFormatType.WordForWindows
            crystalReport.ExportToHttpResponse(formatType, Response, True, "Crystal")
            Response.End()
            hdn_Final_key.Value = 0
        End If
        'crystalReport.SetDataSource(dt)
    End Sub

    Protected Sub CmbBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBank.SelectedIndexChanged



        Dim sqlstr As String

        sqlstr = "SELECT         shobe_id, shobe_nam, bank_id " & _
       " FROM shobe WHERE        (bank_id = " & cmbBank.Value & ") and (CodeClosed = 0)"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbShobe.DataSource = dt
        cmbShobe.ValueField = "shobe_id"
        cmbShobe.TextField = "shobe_nam"
        cmbShobe.DataBind()
    End Sub

    Protected Sub CmbGavahiType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGavahiType.SelectedIndexChanged
        If CmbGavahiType.SelectedValue = 10 Then
            cmbBank.Visible = False
            cmbShobe.Visible = False
            lblbank.Visible = False
            lblShobe.Visible = False

            lblSazman.Visible = True
            txtSazman.Visible = True
        Else
            cmbBank.Visible = True
            cmbShobe.Visible = True
            lblbank.Visible = True
            lblShobe.Visible = True

            lblSazman.Visible = False
            txtSazman.Visible = False

        End If
    End Sub

    Private Sub gavahiTypeLoad()


        Dim sqlstr As String

        sqlstr = "SELECT        gavahi_typ_id, gavahi_typ, report " & _
       " FROM gavahi_type " & _
            "WHERE        (stat = 1) AND (gavahi_typ_id = 9 OR  gavahi_typ_id = 10)  "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbGavahiType.DataSource = dt
        CmbGavahiType.DataValueField = "gavahi_typ_id"
        CmbGavahiType.DataTextField = "gavahi_typ"
        CmbGavahiType.DataBind()
        bankLoad()

        'ibtnSave.Visible = True
    End Sub


    Protected Sub ibtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnSave.Click
        If txtGirande.Text = "" Then
            msg_box("لطفا نام گیرنده وام را بنویسید")
            txtGirande.BackColor = Drawing.Color.LightYellow
            txtGirande.Focus()
            Exit Sub
        Else
            save_final()
        End If

    End Sub
    Private Sub save_final()
        hdn_Final_key.Value = ClsGetMaxId.GetMaxId("gavahi", "new_later_num")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "newGavahiPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@new_later_num", SqlDbType.Int, 6)
            cmm.Parameters("@new_later_num").Value = Val(hdn_Final_key.Value)

            cmm.Parameters.Add("@certifi_typ", SqlDbType.Int, 5)
            cmm.Parameters("@certifi_typ").Value = Val(CmbGavahiType.SelectedValue)

            cmm.Parameters.Add("@person_cod", SqlDbType.Int, 10)
            cmm.Parameters("@person_cod").Value = Val(codepersonel.Text)



            cmm.Parameters.Add("@reg_user_id", SqlDbType.Int, 5)
            cmm.Parameters("@reg_user_id").Value = Val(Session("userid"))

            cmm.Parameters.Add("@moassese", SqlDbType.NVarChar, 150)
            cmm.Parameters("@moassese").Value = txtSazman.Text

            cmm.Parameters.Add("@motafarege_nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@motafarege_nam").Value = txtGirande.Text

            cmm.Parameters.Add("@naxales", SqlDbType.NVarChar, 50)
            cmm.Parameters("@naxales").Value = txtMozd.Text


            If CmbGavahiType.SelectedValue = 6 Or CmbGavahiType.SelectedValue = 8 Or CmbGavahiType.SelectedValue = 10 Then
                cmm.Parameters.Add("@bank_nam", SqlDbType.NVarChar, 50)
                cmm.Parameters("@bank_nam").Value = ""

                cmm.Parameters.Add("@shobe_nam", SqlDbType.NVarChar, 50)
                cmm.Parameters("@shobe_nam").Value = ""


                cmm.Parameters.Add("@bank_id", SqlDbType.Int, 5)
                cmm.Parameters("@bank_id").Value = 1

                cmm.Parameters.Add("@shobe_id", SqlDbType.Int, 5)
                cmm.Parameters("@shobe_id").Value = 1

            Else
                cmm.Parameters.Add("@bank_nam", SqlDbType.NVarChar, 50)
                cmm.Parameters("@bank_nam").Value = cmbBank.SelectedItem.ToString

                cmm.Parameters.Add("@shobe_nam", SqlDbType.NVarChar, 50)
                cmm.Parameters("@shobe_nam").Value = cmbShobe.SelectedItem.ToString


                cmm.Parameters.Add("@bank_id", SqlDbType.Int, 5)
                cmm.Parameters("@bank_id").Value = Val(cmbBank.Value)

                cmm.Parameters.Add("@shobe_id", SqlDbType.Int, 5)
                cmm.Parameters("@shobe_id").Value = Val(cmbShobe.Value)
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
                'msg_box("  ثبت شد ")
                'lblHoshdar.Text = "ثبت شد"
                loadGridSabege()
                printt()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
End Class
