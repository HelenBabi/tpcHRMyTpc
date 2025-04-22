Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration

Partial Class certificateBackFrm
    Inherits System.Web.UI.Page
    Dim dtaspx As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        meno_acc()

        If Me.IsCallback = True Then
            loadGridSabege()
        End If


    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("certificateBackFrm.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)
        ibtnDeleted.Visible = meno_arr(7)
        ibtnHistory.Visible = meno_arr(8)
        ibtnDeactived.Visible = meno_arr(9)
        ibtnPrint.Visible = meno_arr(10)
        'ibtnExit.Visible = meno_arr(11)
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

        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightSalmon
        End If


    End Sub









    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

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

        'salsaxtsher.Text = 0
        'mahsaxtsher.Text = 0
        'ruzsaxtsher.Text = 0
        'Panel4.Visible = True
        'Panel5.Visible = True
        'Panel6.Visible = True
        'Panel7.Visible = True
        'Panel8.Visible = True





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
            perid.Text = dsh2.Rows(0).Item("ID").ToString

            loadGridSabege()

            loadsabege()
            loadzian()
            loadkarkard()
            loadziansherkati()
            loadziansherkatiNO()

        

        Else
            'MsgBox("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
            msg_box("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
        End If
    End Sub



    Private Sub loadGridSabege()



        Dim sqlstr As String

        sqlstr = "SELECT     * from sabegeVW  WHERE person_cod = " & Val(codepersonel.Text) & "  "

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


    End Sub


    Private Sub loadkarkard()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountKar(perID) AS sal, dbo.GetMounthsCountKar(perID) AS mah, dbo.GetDaysCountKar(perID) AS ruz " & _
       " FROM savabeg         WHERE(perid = " & perid.Text & ")"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salkar.Text = dts.Rows(0).Item("sal").ToString
            mahkar.Text = dts.Rows(0).Item("mah").ToString
            ruzkar.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub

    Private Sub loadsabege()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCount(perID) AS sal, dbo.GetMounthsCount(perID) AS mah, dbo.GetDaysCount(perID) AS ruz " & _
       " FROM savabeg         WHERE(perid = " & perid.Text & ")"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salsabege.Text = dts.Rows(0).Item("sal").ToString
            mahsabege.Text = dts.Rows(0).Item("mah").ToString
            ruzsabege.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub
    Private Sub loadzian()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountSaxt(savabeg.perID) AS sal, dbo.GetMounthsCountSaxt(savabeg.perID) AS mah, dbo.GetDaysCountSaxt(savabeg.perID) AS ruz " & _
      " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
           " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salzian.Text = dts.Rows(0).Item("sal").ToString
            mahzian.Text = dts.Rows(0).Item("mah").ToString
            ruzzian1.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub

    Private Sub loadziansherkati()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountSaxtsherkati(savabeg.perID) AS sal, dbo.GetMounthsCountSaxtsherkati(savabeg.perID) AS mah, dbo.GetDaysCountSaxtSherkati(savabeg.perID) AS ruz " & _
      " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
           " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salsaxtsher.Text = dts.Rows(0).Item("sal").ToString
            mahsaxtsher.Text = dts.Rows(0).Item("mah").ToString
            ruzsaxtsher.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub

    Private Sub loadziansherkatiNO()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountSaxtsherkatiNo(savabeg.perID) AS sal, dbo.GetMounthsCountSaxtsherkatiNo(savabeg.perID) AS mah, dbo.GetDaysCountSaxtSherkatiNo(savabeg.perID) AS ruz " & _
      " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
           " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salsaxtno.Text = dts.Rows(0).Item("sal").ToString
            mahsaxno.Text = dts.Rows(0).Item("mah").ToString
            ruzsaxtno.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub











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
        Dim filename As String = "gavahi.rpt"
        crystalReport.Load(Server.MapPath("~/reports/Cr1.rpt"))


        Dim sqlstr As String = " SELECT   * from perInfoGavahiVW WHERE        (new_later_num = " & hdn_Final_key.Value & ") "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        Dim jens As String = "آقا"
        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("jens").ToString = 2 Then
                jens = "خانم"

            End If

            Dim nam As String = TextBox2.Text

            crystalReport.SetParameterValue("jens", jens)

            crystalReport.SetParameterValue("nam", dt.Rows(0).Item("nam").ToString)
            crystalReport.SetParameterValue("family", dt.Rows(0).Item("family").ToString)

            crystalReport.SetParameterValue("father", dt.Rows(0).Item("father").ToString)
            crystalReport.SetParameterValue("shsh", dt.Rows(0).Item("shsh").ToString)

            crystalReport.SetParameterValue("mahalesodur", dt.Rows(0).Item("mahalesodur").ToString)
            crystalReport.SetParameterValue("codeMelli", dt.Rows(0).Item("codeMelli").ToString)

            crystalReport.SetParameterValue("mozd", dt.Rows(0).Item("naxales").ToString)


            crystalReport.SetParameterValue("bank", dt.Rows(0).Item("bank_nam").ToString)
            crystalReport.SetParameterValue("shobe", dt.Rows(0).Item("shobe_nam").ToString)

            crystalReport.SetParameterValue("contartType", dt.Rows(0).Item("value").ToString)

            crystalReport.SetParameterValue("tarix", dt.Rows(0).Item("latter_date").ToString)

            Dim sqlstrm As String = " SELECT   * from managerTbl WHERE       stat =1  "
            Dim dtm As New DataTable
            dtm = ClsDataTable.GetDataTable(sqlstrm)
            crystalReport.SetParameterValue("manager", dtm.Rows(0).Item("mangerName").ToString)
            crystalReport.SetParameterValue("managerPost", dtm.Rows(0).Item("managerPost").ToString)


            Dim formatType As ExportFormatType = ExportFormatType.NoFormat
            formatType = ExportFormatType.WordForWindows
            crystalReport.ExportToHttpResponse(formatType, Response, True, "Crystal")
            Response.End()



        End If
        'crystalReport.SetDataSource(dt)
    End Sub


   
    Protected Sub gridSabege_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles gridSabege.RowCommand

        Dim tim As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        If e.CommandArgs.CommandName = "Ebtall" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     new_later_num       FROM gavahi " & _
                  " WHERE     (stat = 1 or stat = 5 )  and (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTable(sqlstr2)
            If dt3.Rows.Count = 0 Then
                msg_box("فقط گواهی های در وضعیت صادر شده و برگشت ازبانک را می توانید ابطال کنید")
                Exit Sub
            End If

            Dim sqlstr As String

            sqlstr = "  UPDATE gavahi  SET   stat= 2  ,  ebtal_user = " & Session("userid") & ", ebtal_date = N'" & tim & "'" & _
                                  " WHERE (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            upStat(sqlstr)
            msg_box("ابطال شد")
            loadGridSabege()
        End If

        If e.CommandArgs.CommandName = "bargasht" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     new_later_num       FROM gavahi " & _
                  " WHERE     (stat = 1 or stat = 2 )   and (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTable(sqlstr2)
            If dt3.Rows.Count = 0 Then
                msg_box("فقط گواهی های در وضعیت صادر شده و ابطال را می توانید برگشت ازبانک کنید")
                Exit Sub
            End If

            Dim sqlstr As String

            sqlstr = "  UPDATE gavahi  SET   stat= 5 ,  ebtal_user = " & Session("userid") & ", ebtal_date = N'" & tim & "'" & _
                                  " WHERE (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            upStat(sqlstr)
            msg_box("برگشت از باانک ثبت شد")
            loadGridSabege()
        End If
        If e.CommandArgs.CommandName = "sodur" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     new_later_num       FROM gavahi " & _
                  " WHERE     (stat = 5 or stat = 2 )   and (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTable(sqlstr2)
            If dt3.Rows.Count = 0 Then
                msg_box("فقط گواهی های در وضعیت برگشت ازبانک و ابطال را می توانید به حالت صادره تغییر دهید")
                Exit Sub
            End If

            Dim sqlstr As String

            sqlstr = "  UPDATE gavahi  SET   stat= 1  ,  ebtal_user = " & Session("userid") & ", ebtal_date = N'" & tim & "'" & _
                                  " WHERE (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            upStat(sqlstr)
            msg_box("وضعیت گواهی از برگشت ازبانک به صادره تغییریافت")
            loadGridSabege()
        End If
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
            cnn.close()
        End Try

    End Sub
End Class
