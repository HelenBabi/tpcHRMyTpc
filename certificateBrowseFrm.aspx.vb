Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration

Partial Class certificateBrowseFrm
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow(a, b)
        Else
            load_grid()

        End If
        'If Me.IsCallback = True Then
        '    ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
        '    loadgrdNow(a, b)
        'End If

    End Sub
   

    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
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
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("certificateBrowseFrm.aspx", Val(Session("UserId")))
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
    Private Sub loadgrdNow(ByVal dat1 As String, ByVal dat2 As String)



        'If lblDateS.Text = "" Then
        '    Exit Sub
        'End If
       
        Dim sqlstr As String

        sqlstr = "SELECT     * from perInfoGavahiVW  WHERE   (latter_date BETWEEN '" & dat1 & "' AND '" & dat2 & "')"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        Dim cntt As Integer = dt.Rows.Count
        lblTotal.Text = "تعداد كل = " & cntt

    End Sub

    Private Sub load_grid()

        datS = Request.Form("datepicker")
        dateE = Request.Form("datepicker2")
        lblDateS.Text = datS
        lbldateE.Text = dateE


        'If lbldateE.Text = "" Or lblDateS.Text = "" Then
        '    msg_box("تاریخ شروع و تاریخ اتمام را وارد كنيد")

        '    Exit Sub
        'End If
        If lblDateS.Text = "" Or lbldateE.Text = "" Then
            Exit Sub
        End If
        If Len(lblDateS.Text) < 10 Then
            datS1 = ClsDateTime.setDate(datS)
        End If

        If Len(lbldateE.Text) < 10 Then
            dateE1 = ClsDateTime.setDate(dateE)
        End If

        If Len(lblDateS.Text) = 10 Then
            datS1 = datS
        End If

        If Len(lbldateE.Text) = 10 Then
            dateE1 = dateE
        End If


        Dim sqlstr As String

        sqlstr = "SELECT     * from perInfoGavahiRepVW WHERE        (latter_date BETWEEN '" & datS1 & "' AND '" & dateE1 & "')"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
        'CntTyp()
        Dim cntt As Integer = dt1.Rows.Count
        lblTotal.Text = "تعداد كل = " & cntt
    End Sub
   
    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

       
            Response.Redirect("index.aspx")
       

    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click


        load_grid()
        'If datS1 > dateE1 Then
        '    msg_box("تاریخ شروع نمی تواند بزرگتر از تاریخ اتمام باشد")
        '    ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
        '    Exit Sub
        'End If
        ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            'Dim ClsB As New managerCls
            'Dim dt As New DataTable
            'dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
            HiddenField1.Value = e.CommandArgs.CommandArgument.ToString()

        End If
    End Sub
    Private Sub printtt()
        Dim crystalReport As New ReportDocument()

       
        Dim sqlstr As String = " SELECT   * from perInfoGavahiVW WHERE        (new_later_num = " & HiddenField1.Value & ") "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        Dim jens As String = "آقای"
        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("jens").ToString = 2 Then
                jens = "خانم"

            End If


            crystalReport.Load(Server.MapPath("~/reports/" & dt.Rows(0).Item("report").ToString))


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

            crystalReport.SetParameterValue("codePersoneli", dt.Rows(0).Item("codePersoneli").ToString)

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

    Protected Sub ibtnPrint_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnPrint.Click
        If HiddenField1.Value = 0 Then
            Exit Sub
        Else

            printtt()
        End If

    End Sub
End Class
