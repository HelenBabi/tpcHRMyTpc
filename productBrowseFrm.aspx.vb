Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class productBrowseFrm
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()

            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow(a, b)
        Else
            load_grid()

        End If
        load_gridAll()
        BindQuata()
    End Sub

    Private Sub BindQuata()

        Dim dt3 As New DataTable


        Dim sqlstr As String = "select  dbo.kalaNotEntryCount() as tedadNo,dbo.kalaEntryCount() as tedad,dbo.GetPersonelCountKala() as perCount"

        dt3 = ClsDataTable.GetDataTable(sqlstr)


        Dim kol As Integer = dt3.Rows(0).Item("perCount").ToString
        Dim regcount As Integer = dt3.Rows(0).Item("tedad").ToString
        'Dim regNocount As Integer = dt3.Rows(0).Item("tedadNo").ToString
        Dim regNocount As Integer = kol - regcount
        lblKol.Text = kol & "  تعداد پرسنل  "

        lblSabt.Text = regcount & "  ثبت نام کرده  "
        lblNosabt.Text = regNocount & "  باقیمانده  "
        Dim dt As New DataTable
        dt.Columns.Add("Status")
        dt.Columns.Add("Total")

        'dt.Rows.Add("Success", kol)
        dt.Rows.Add("ثبت نام کرده", regcount)
        dt.Rows.Add("باقیمانده", regNocount)

        Chart1.DataSource = dt
        Chart1.Series("Series1").XValueMember = "Status"
        Chart1.Series("Series1").YValueMembers = "Total"
        Chart1.DataBind()

    End Sub

    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_gridAll()
    End Sub


    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("perIDD") = e.CommandArgs.CommandArgument
            Response.Redirect("productPersonelDetail.aspx")
        End If
        If e.CommandArgs.CommandName = "moshahedeUnit" Then
            Session("perIDD") = e.CommandArgs.CommandArgument
            Session("unitID") = e.CommandArgs.CommandArgument
            Response.Redirect("productUnitDetail.aspx")
        End If
    End Sub




    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("stat"))
    '    If value = 1 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LimeGreen

    '    ElseIf value = 0 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.PaleVioletRed


    '    End If


    'End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("productBrowseFrm.aspx", Val(Session("UserId")))
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


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from kalaPersonelReportVW  WHERE     (tarix BETWEEN '" & datS1 & "' AND '" & dateE1 & "'  ) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        Dim cntt As Integer = dt.Rows.Count
        lblTotal.Text = "تعداد  = " & cntt
        ' CntTypnow()
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
        If lblDateS.Text = "" Then
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


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from kalaPersonelReportVW  WHERE     (tarix BETWEEN '" & datS1 & "' AND '" & dateE1 & "')"
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        ' CntTyp()
        Dim sqlstrr As String = ""
        Dim dtt As New DataTable
        sqlstrr = "SELECT        kalaTbl.kalaValue, kalaSizeTbl.sizeValue, COUNT(kalaSizeTbl.sizeID) AS tedad " & _
    "FROM            kalaPersonel INNER JOIN " & _
                        " kalaTbl ON kalaPersonel.kalaID = kalaTbl.kalaID INNER JOIN " & _
                        " kalaSizeTbl ON kalaPersonel.sizeID = kalaSizeTbl.sizeID " & _
"WHERE        (kalaPersonel.tarix BETWEEN '" & datS1 & "' AND '" & dateE1 & "') " & _
"GROUP BY kalaTbl.kalaValue, kalaSizeTbl.sizeValue " & _
"ORDER BY kalaTbl.kalaValue, kalaSizeTbl.sizeValue "
        dtt = ClsDataTable.GetDataTable(sqlstrr)
        ASPxGridView1.DataSource = dtt
        ASPxGridView1.DataBind()

        Dim cntt As Integer = dt.Rows.Count
        lblTotal.Text = "تعداد  = " & cntt
    End Sub








    'Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
    '    load_grid()
    'End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()


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


    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            load_gridAll()
        Else
            load_grid()
        End If


    End Sub
    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from kalaPersonelReportVW"
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count
        lblTotal.Text = "تعداد  = " & cntt

        Dim sqlstrr As String = ""
        Dim dtt As New DataTable
        sqlstrr = "SELECT        kalaTbl.kalaValue, kalaSizeTbl.sizeValue, COUNT(kalaSizeTbl.sizeID) AS tedad " & _
    "FROM            kalaPersonel INNER JOIN " & _
                        " kalaTbl ON kalaPersonel.kalaID = kalaTbl.kalaID INNER JOIN " & _
                        " kalaSizeTbl ON kalaPersonel.sizeID = kalaSizeTbl.sizeID " & _
"GROUP BY kalaTbl.kalaValue, kalaSizeTbl.sizeValue " & _
"ORDER BY kalaTbl.kalaValue, kalaSizeTbl.sizeValue "
        dtt = ClsDataTable.GetDataTable(sqlstrr)
        ASPxGridView1.DataSource = dtt
        ASPxGridView1.DataBind()

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ibtnxl0_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl0.Click
        ASPxGridViewExporter1.WriteXlsxToResponse()
    End Sub
End Class
