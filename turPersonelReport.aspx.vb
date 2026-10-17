Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Text

Partial Class turPersonelReport
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("turPersonelReport.aspx", Val(Session("UserId")))
        'ibtnAdd.Visible = meno_arr(1)
        'ibtnEdit.Visible = meno_arr(2)
        'ibtnSave.Visible = meno_arr(3)
        'ibtnSaveNew.Visible = meno_arr(4)
        'ibtnCancel.Visible = meno_arr(5)
        'ibtnDelete.Visible = meno_arr(6)
        'ibtnDeleted.Visible = meno_arr(7)
        'ibtnHistory.Visible = meno_arr(8)
        'ibtnDeactived.Visible = meno_arr(9)
        'ibtnPrint.Visible = meno_arr(10)
        'ibtnExit.Visible = meno_arr(11)
    End Sub
    Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
        load_grid()
    End Sub
    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic

        End If
    End Sub
  
    'Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
    '    load_grid()

    'End Sub
    Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        warning_pic = "images/person_pic/" & value & ".jpg"
        'warning_pic = "images/Icons/loading_transparent.gif"



        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value2 As Decimal = CDec(e.GetValue("stat"))
        If value2 = 6 or value2 = 17 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightGreen

        ElseIf value2 = 7 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightCoral

        ElseIf value2 = 8 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PapayaWhip
        End If

    End Sub
    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand
        Dim sqlstr As String
        If e.CommandArgs.CommandName = "pazir" Then
            sqlstr = "UPDATE     turRefahiRegTbl SET                stat = 17 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            'UpdatePanel1.Update()
            Dim index As Integer = e.VisibleIndex
            Dim mobileNumber As String = ASPxGridView1.GetRowValues(index, "mobile").ToString()

            Dim smstext1 As String = "با سلام، " & vbCrLf & _
"جنابعالی برنده " & DropDownList1.SelectedItem.Text & " شده‌اید. نیازی به مراجعه حضوری به واحد رفاهیات نمی‌باشد. در زمان مقتضی، هماهنگی‌های لازم با شما انجام خواهد شد."

            Dim sqlstrs As String = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _        "VALUES     ('" & mobileNumber & "', N'" & smstext1 & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"              upStat(sqlstrs)
        End If
     
        If e.CommandArgs.CommandName = "ebtal" Then
            sqlstr = "UPDATE     turRefahiRegTbl SET                stat = 7 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            'UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "ebtal2" Then
            sqlstr = "UPDATE     turRefahiRegTbl SET                stat = 8 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            'UpdatePanel1.Update()
        End If
        'If e.CommandArgs.CommandName = "sabege" Then
        '    Dim index As Integer = e.VisibleIndex
        '    Dim codepersoneli As Integer = ASPxGridView1.GetRowValues(index, "codePersoneli").ToString()
        '    loadSabge(codepersoneli)
        '    'UpdatePanel1.Update()
        'End If
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
    Private Sub load_grid()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM turRefahiVW    WHERE  id_tour = " & Val(DropDownList1.SelectedValue) & "   "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()
        'tedadkol()

    End Sub

    Private Sub tedadkol()
        Dim dta As DataTable
        Dim sqlstr2 As String
        sqlstr2 = "SELECT    sum(tedad) as tedadkol  " & _
             " FROM turRefahiRegTbl    WHERE  id_tour = " & Val(DropDownList1.SelectedValue) & " and  hazf = 0  "
        dta = ClsDataTable.GetDataTable(sqlstr2)
        If dta.Rows.Count > 0 Then
            'Label3.Text = dta.Rows(0).Item("tedadkol").ToString
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        meno_acc()
        If Not IsPostBack Then
            load_cmb()
            gridKol()
            load_grid()

           
        End If
 
        'If Request("__EVENTTARGET") = "ShowHistory" Then
        '    Dim codePersoneli As String = Request("__EVENTARGUMENT")
        '    ShowPersonelHistory(codePersoneli)
        'End If
        If Request("__EVENTTARGET") = "ShowHistory" Then
            'Dim code As String = Request("__EVENTARGUMENT")
            ' دریافت اطلاعات سابقه بر اساس code
          Dim codePersoneli As String = Request("__EVENTARGUMENT")
            ShowPersonelHistory(codePersoneli)
            ' باز کردن پاپ‌آپ در سمت کلاینت
            ClientScript.RegisterStartupScript(Me.GetType(), "showPopup", "popupHistory.Show();", True)
        End If

    End Sub


    Private Sub ShowPersonelHistory(codePersoneli As String)
        Dim dt As DataTable = GetSabegeByCode(codePersoneli)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            litHistory.Text = "<div class='alert alert-warning'>سابقه‌ای برای این پرسنل یافت نشد.</div>"
            PopupHistory.ShowOnPageLoad = True
            Return
        End If
        Dim fullName As String = dt.Rows(0)("nam") & " " & dt.Rows(0)("family")
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<div class='accordion' id='historyAccordion'>")
        sb.AppendLine("<h4 class='text-center mb-3'><strong> " & fullName & "</strong></h4>")
        sb.AppendLine("<h4 class='text-center mb-3'><strong>کدپرسنلی : " & codePersoneli & "</strong></h4>")
        sb.AppendLine("<div class='accordion' id='historyAccordion'>")
        Dim index As Integer = 0
       For Each row As DataRow In dt.Rows
            sb.AppendLine("<div class='accordion-item'>")
            sb.AppendLine("    <h4 class='accordion-header' id='heading" & index & "'>")
            sb.AppendLine("        <button class='accordion-button " & If(index > 0, "collapsed", "") & "' type='button' data-bs-toggle='collapse' data-bs-target='#collapse" & index & "' aria-expanded='" & If(index = 0, "true", "false") & "' aria-controls='collapse" & index & "'>")
            sb.AppendLine("           " & row("magsad"))
            sb.AppendLine("        </button>")
            sb.AppendLine("    </h4>")
            sb.AppendLine("    <div id='collapse" & index & "' class='accordion-collapse collapse " & If(index = 0, "show", "") & "' aria-labelledby='heading" & index & "' data-bs-parent='#historyAccordion'>")
            sb.AppendLine("        <div class='accordion-body'>")
            'sb.AppendLine("            <strong>شرح:</strong> " & row("tozih") & "<br />")
            'sb.AppendLine("            <strong>امتیاز:</strong> " & row("emtiaz") & "")
            sb.AppendLine("        </div>")
            sb.AppendLine("    </div>")
            sb.AppendLine("</div>")
            index += 1
        Next



        sb.Append("</div>")
        litHistory.Text = sb.ToString()
        PopupHistory.ShowOnPageLoad = True
End Sub

    Private Function GetSabegeByCode(codePersoneli As String) As DataTable
        Dim sql As String = "SELECT magsad,nam,family FROM turGridVW WHERE        (codePersonel = " & codePersoneli & ") AND (stat = 17) or (stat = 6) ORDER BY idd"
        Dim dt As New DataTable()
        dt = ClsDataTable.GetDataTable(sql)
        Return dt
    End Function


    'Private Sub loadSabge(ByVal codepersoneli As Integer)
    '    Dim sql As String = "SELECT magsad FROM turGridVW WHERE        (codePersonel = " & codePersoneli & ") AND (stat = 17)"
    '    Dim dt As New DataTable()
    '    dt = ClsDataTable.GetDataTable(sql)
    '    ASPxGrvHead.DataSource = dt
    '    ASPxGrvHead.DataBind()
    'End Sub
    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("codePersoneli").ToString

        'Dim ClsB_ As New ClsBrowsSystems
        'Dim dt2 As New DataTable
        'dt2 = ClsB_.GetAllBrowsSystems(idd)
        'grid.DataSource = dt2
        Dim sqlstr As String

        sqlstr = "SELECT  * from vwTakfol WHERE        (empID = '" & idd & "')"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt


    End Sub
    Private Sub load_cmb()
        Dim dt1 As New Data.DataTable
        sqlstr = " SELECT     magsad , idTour  FROM turRefahiTbl order by idTour Desc "

        dt1 = ClsDataTable.GetDataTable(sqlstr)
        If dt1.Rows.Count > 0 Then
            DropDownList1.DataSource = dt1

            DropDownList1.DataTextField = "magsad"
            DropDownList1.DataValueField = "idTour"
            DropDownList1.DataBind()
        End If


    End Sub





    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        load_grid()
        'UpdatePanel2.Update()
    End Sub


    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        ' اطمینان از پر بودن گرید
        If DropDownList1.SelectedValue Is Nothing OrElse DropDownList1.SelectedValue = "" Then
            msg_box("لطفاً یک تور را انتخاب کنید.")
            Return
        End If

        ' بارگزاری مجدد اطلاعات گرید قبل از اکسپورت
        load_grid()

        ' تنظیمات اکسپورت
        grvExporter1.GridViewID = ASPxGridView1.ID
        grvExporter1.FileName = DropDownList1.SelectedItem.Text.Trim()

        grvExporter1.WriteXlsToResponse(New DevExpress.XtraPrinting.XlsExportOptions() With {
            .ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFile
        })


    End Sub

    'Private Sub loadGridAll()
    '    Dim dtanbar As New DataTable

    '    Dim sqlstr As String
    '    sqlstr = "SELECT     *  FROM turRefahiVW ORDER BY id_tour"
    '    dtanbar = ClsDataTable.GetDataTable(sqlstr)
    '    ASPxGridView1.DataSource = dtanbar
    '    ASPxGridView1.DataBind()
    '    tedadkol()

    'End Sub

    Private Sub gridKol()
        Dim dtk As New DataTable

        Dim sqlstrk As String = ""
        sqlstrk = "SELECT     *  FROM turAmarVW ORDER BY idtour DESC"
        dtk = ClsDataTable.GetDataTable(sqlstrk)
        ASPxGridView2.DataSource = dtk
        ASPxGridView2.DataBind()
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ASPxGridView2_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView2.AfterPerformCallback
        gridKol()
    End Sub
    'Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    '    If CheckBox1.Checked Then
    '        loadGridAll()
    '    Else
    '        load_grid()
    '    End If
    '    'UpdatePanel2.Update()
    'End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
    

        ' بارگزاری مجدد اطلاعات گرید قبل از اکسپورت
        gridKol()

        ' تنظیمات اکسپورت
        ASPxGridViewExporter1.GridViewID = ASPxGridView2.ID
        ASPxGridViewExporter1.FileName = "آمارکلی"

        ASPxGridViewExporter1.WriteXlsToResponse(New DevExpress.XtraPrinting.XlsExportOptions() With {
            .ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFile
        })

    End Sub
End Class
