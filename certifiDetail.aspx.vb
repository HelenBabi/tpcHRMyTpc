Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration


Partial Class certifiDetail
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load






        If Me.IsPostBack = False Then
          
            gavahiTypeLoad()
            bankLoad()

            'cmbBank. = 8
            shobeLoad2()
            loadGridSabege()
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

        Dim sqlstr As String = ""

        sqlstr = "SELECT     * from sabegeVW  WHERE person_cod = " & Session("codePersoneliii") & "  "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        gridSabege.DataSource = dt
        gridSabege.DataBind()

        'For i As Integer = 0 To Val(dt.Rows.Count) - 1
        '    If Val(dt.Rows(i).Item("stat").ToString) = 5 Then
        '        msg_box("پرسنل گواهی برگشت از بانک دارد")

        '    End If
        'Next i

        pishnahaddahande.Text = dt.Rows(0).Item("personell").ToString()

        person_cod.Text = dt.Rows(0).Item("person_cod").ToString()

        txtMozd.Text = dt.Rows(0).Item("mozd").ToString()
        contracttyp.Text = dt.Rows(0).Item("value").ToString()
        hdnType.Value = dt.Rows(0).Item("typ").ToString()
        Dim sqlstr2 As String = ""

        sqlstr2 = "SELECT   moassese,gavahi_typ, bank_id, shobe_id,motafarege_nam,new_later_num from sabegeVW  WHERE person_cod = " & Session("codePersoneliii") & " and stat = 6"

        Dim dt2 As New DataTable
        dt2 = ClsDataTable.GetDataTable(sqlstr2)
        'If Session("contratTyp") = 2 And dt2.Rows.Count >= 6 Then
        '    msg_box("سقف مجاز برای پرسنل پر شده است")

        'ElseIf Session("contratTyp") <> 2 And dt2.Rows.Count >= 3 Then
        '    msg_box("سقف مجاز برای پرسنل پر شده است")
        If dt2.Rows.Count > 0 Then
            txtGirande.Text = dt2.Rows(0).Item("motafarege_nam").ToString()
            CmbGavahiType.SelectedValue = dt2.Rows(0).Item("gavahi_typ").ToString()
            'End If
            HiddenFieldlater_num.Value = dt2.Rows(0).Item("new_later_num").ToString()

            txtSazman.Text = dt2.Rows(0).Item("moassese").ToString()
            cmbBank.Value = dt2.Rows(0).Item("bank_id").ToString()
            cmbShobe.Value = dt2.Rows(0).Item("shobe_id").ToString()


        End If




    End Sub
   
   

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
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

    Private Sub save_final()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "newGavahi2Prc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@new_later_num", SqlDbType.Int, 6)
            cmm.Parameters("@new_later_num").Value = Val(HiddenFieldlater_num.Value)

            cmm.Parameters.Add("@certifi_typ", SqlDbType.Int, 5)
            cmm.Parameters("@certifi_typ").Value = Val(CmbGavahiType.SelectedValue)

            cmm.Parameters.Add("@moassese", SqlDbType.NVarChar, 150)
            cmm.Parameters("@moassese").Value = txtSazman.Text

            cmm.Parameters.Add("@motafarege_nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@motafarege_nam").Value = txtGirande.Text

            cmm.Parameters.Add("@naxales", SqlDbType.NVarChar, 50)
            cmm.Parameters("@naxales").Value = Val(txtMozd.Text)


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

            cmm.Parameters.Add("@reg_user_id", SqlDbType.Int, 5)
            cmm.Parameters("@reg_user_id").Value = Session("userid")

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



        Dim sqlstr As String = " SELECT   * from perInfoGavahiVW WHERE        (new_later_num = " & HiddenFieldlater_num.Value & ") "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        Dim jens As String = "آقای"
        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("jens").ToString = 2 Then
                jens = "خانم"

            End If


            'crystalReport.Load(Server.MapPath("~/reports/" & dt.Rows(0).Item("report").ToString))
            crystalReport.Load(Server.MapPath("~/reports/mostagim22.rpt"))

            Dim nam As String = pishnahaddahande.Text
            Dim girande As String = txtGirande.Text


            crystalReport.SetParameterValue("girande", girande)

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

            crystalReport.SetParameterValue("codePersoneli", Session("codePersoneliii"))

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

    




    Protected Sub CmbBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBank.SelectedIndexChanged



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

    

    

    
    Protected Sub gridSabege_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles gridSabege.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat"))


        If value = 6 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Yellow
            'lblHoshdar.Visible = True

        End If


    End Sub

    
    
    

    
    
    

    
    Protected Sub btnChap_Click(sender As Object, e As EventArgs) Handles btnChap.Click
        printt()
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        save_final()

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
