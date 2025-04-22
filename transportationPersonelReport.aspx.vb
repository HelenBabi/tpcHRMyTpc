Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class transportationPersonelReport
    Inherits System.Web.UI.Page

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
          If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()
            meno_acc()
            load_cmbAnbar()
        End If
        If Me.IsCallback Then
            load_gridTyp()
            'load_cmbAnbar()
        End If
        'load_cmbAnbar()
        load_gridTyp()
        'load_grid()
        load_chart()
        BindQuata()
    End Sub
    Private Sub BindQuata()

        Dim dt3 As New DataTable


        Dim sqlstr As String = "select  dbo.ServiceRegNotEntryCount() as tedadNo,dbo.ServiceRegEntryCount() as tedad,dbo.GetPersonelCount() as perCount"

        dt3 = ClsDataTable.GetDataTable(sqlstr)


        Dim kol As Integer = dt3.Rows(0).Item("perCount").ToString
        Dim regcount As Integer = dt3.Rows(0).Item("tedad").ToString
        Dim regNocount As Integer = dt3.Rows(0).Item("tedadNo").ToString
        lblKol.Text = kol & "کل پرسنل"

        lblSabt.Text = regcount & "ثبت نام کرده"
        lblNosabt.Text = regNocount & "باقیمانده"
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
    Private Sub load_chart()
        Dim sqlstr As String
        sqlstr = "SELECT        tedad AS ss, masir FROM            serviceAmarVW WHERE        (shiftType = " & CmbAnbar.SelectedValue & ") ORDER BY citySiteTypeID"
        Dim dtS As New DataTable
        dtS = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.Series.Item(0).DataSource = dtS

        WebChartControl1.Series.Item(0).DataSource = dtS
        WebChartControl1.DataBind()
    End Sub

    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt2 As DataTable
        Dim ServID As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)

        ServID = grid.GetMasterRowFieldValues("masirID").ToString

        Dim sqlstr As String


        sqlstr = " SELECT       * from serviceAmarDetailsVW WHERE        (serviceID = " & ServID & ")  "
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub

    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("citySiteTypeID"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.Azure

        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Aquamarine
        ElseIf value = 3 Then
            e.Row.ForeColor = Drawing.Color.Black

            e.Row.BackColor = Drawing.Color.WhiteSmoke
        ElseIf value = 4 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PapayaWhip
        ElseIf value = 5 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Beige

        End If


    End Sub

 

    'Protected Sub grdFarei_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

    '    Dim warning_pic As String = ""
    '    Dim jobStat As Integer = CStr(e.GetValue("TypeCode"))
    '    If jobStat = 1 Then
    '        warning_pic = "images/design_images/view_refresh.png"
    '    ElseIf jobStat = 2 Then
    '        warning_pic = "images/design_images/view_refresh.png"
    '    End If


    '    If e.DataColumn.Name = "photo" Then
    '        Dim iamgegrdst As Image = TryCast(ASPxGrvHead.FindDetailRowTemplateControl(e.VisibleIndex, "Image5"), Image)
    '        iamgegrdst.ImageUrl = warning_pic
    '        '  textBox.Text = Convert.ToString(e.CellValue)
    '    End If

    'End Sub

#Region "All pages"


    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("transportationPersonelReport.aspx", Val(Session("UserId")))
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


    Private Sub load_grid()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        'sqlstr = "SELECT       tblDtlAnbar.idAnbar, tblDtlAnbar.kodeGate, tblDtlAnbar.kodeModel, SUM(tblDtlAnbar.dtltedad * tblDtlAnbar.TypeCode) AS tedad, " & _
        ' "tblBaseSystemHDtl.BaseSystemHDtlNmae, tblBaseModel.BaseModelName, tblAnbar.nameAnabr " & _
        '      " FROM            tblDtlAnbar INNER JOIN " & _
        '                " tblBaseSystemHDtl ON tblDtlAnbar.kodeGate = tblBaseSystemHDtl.BaseSystemHDtlCode INNER JOIN " & _
        '                " tblBaseModel ON tblBaseSystemHDtl.BaseSystemHDtlCode = tblBaseModel.BaseSystemHDtlCode AND  " & _
        '                " tblDtlAnbar.kodeModel = tblBaseModel.BaseModelCode INNER JOIN " & _
        '                " tblAnbar ON tblDtlAnbar.idAnbar = tblAnbar.idAnbar " & _
        '              " GROUP BY tblDtlAnbar.idAnbar, tblDtlAnbar.kodeGate, tblDtlAnbar.kodeModel, tblBaseSystemHDtl.BaseSystemHDtlNmae, tblBaseModel.BaseModelName,  " & _
        '              " tblAnbar.nameAnabr HAVING(tblDtlAnbar.idAnbar = " & CmbAnbar.SelectedValue & ")"
        sqlstr = "select * from serviceAmarVW "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dtanbar
        ASPxGrvHead.DataBind()

    End Sub








    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_gridTyp()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

      
            Response.Redirect("index.aspx")
      
    End Sub


    Private Sub load_cmbAnbar()
        Dim dtg, dtm, dtmg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT         typeID, typevalue " & _
       " FROM serviceTypeTbl where  stat=1 "
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbAnbar.DataSource = dtm
        CmbAnbar.DataTextField = "typevalue"
        CmbAnbar.DataValueField = "typeID"
        CmbAnbar.DataBind()

    End Sub

    Protected Sub CmbAnbar_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbAnbar.SelectedIndexChanged
        If CmbAnbar.SelectedValue = 3 Then
            WebChartControl1.Visible = False
        Else
            WebChartControl1.Visible = True
        End If
        load_gridTyp()
        load_chart()
    End Sub
    Private Sub load_gridTyp()
        'If CmbAnbar.SelectedValue = 0 Then
        '    load_grid()
        'Else

        Dim dtanbar As DataTable

        Dim sqlstr As String

        sqlstr = "select * from serviceAmarVW WHERE        (shiftType = " & CmbAnbar.SelectedValue & ") ORDER BY citySiteTypeID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dtanbar
        ASPxGrvHead.DataBind()
        'End If



    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            load_grid()
        Else
            load_gridTyp()
        End If

    End Sub
End Class
