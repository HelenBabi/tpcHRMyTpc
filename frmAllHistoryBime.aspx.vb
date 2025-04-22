Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class frmAllHistoryBime
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then

        End If

        load_grid()
    End Sub

    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("codePersoneli").ToString

        'Dim ClsB_ As New ClsBrowsSystems
        'Dim dt2 As New DataTable
        'dt2 = ClsB_.GetAllBrowsSystems(idd)
        'grid.DataSource = dt2
        Dim sqlstr As String

        sqlstr = "SELECT     personel.codePersoneli,savabeg.id, savabeg.tarixshoru, savabeg.tarixpayan, savabeg.karkardSal + '-' + savabeg.karkardMah + '-' + savabeg.karkardDay AS karkard, " & _
                     " savabeg.sabegeSal + '-' + savabeg.sabegeMah + '-' + savabeg.sabegeDay AS sabege, savabeg.tozih, mashaghel.postname, sazman.sazmanName,  " & _
                     " saxtzian.value, mashaghel.saxt , sherkatiType.value AS sherkati  " & _
                    " FROM         savabeg INNER JOIN " & _
                     " mashaghel ON savabeg.onvan = mashaghel.id INNER JOIN " & _
                     " personel ON savabeg.perID = personel.ID INNER JOIN " & _
                     " sazman ON savabeg.sazman = sazman.id INNER JOIN " & _
                     " saxtzian ON mashaghel.saxt = saxtzian.id  INNER JOIN " & _
                     "  sherkatiType ON sazman.sherkati = sherkatiType.id " & _
                     " WHERE(personel.codePersoneli = " & idd & ") " & _
                    " ORDER BY savabeg.tarixshoru"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt
        'grid.DataBind()



    End Sub


    Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("saxt"))
        If value = 0 Then
            e.Row.ForeColor = Drawing.Color.Red
            '   e.Row.BackColor = Drawing.Color.Aquamarine
        Else
            e.Row.ForeColor = Drawing.Color.Blue
            '   e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
        End If
    End Sub

    'Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim idd As Integer
    '    Dim grid As ASPxGridView = CType(sender, ASPxGridView)
    '    idd = grid.GetMasterRowFieldValues("WorkId").ToString

    '    Dim sqlstr As String

    '    Dim dt As New DataTable
    '    sqlstr = ("seect * from tblWorkDtl where WorkId= " & idd & "")
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    grid.DataSource = dt
    '    grid.DataBind()

    'End Sub







    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("WorkTypeCode"))
    '    If value = 1 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LightGreen
    '    ElseIf value = 2 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
    '    ElseIf value = 3 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.Aquamarine

    '    ElseIf value = 4 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.BurlyWood
    '    End If
    'End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrowsAllSystem.aspx", Val(Session("UserId")))

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
        Dim sqlstr As String

        sqlstr = "SELECT  * from vwReportKoli"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
        lblCnt.Text = dt.Rows.Count
    End Sub



    'Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
    '    If e.CommandArgs.CommandName = "select" Then
    '        Dim ClsB As New ClsBrowsWorks
    '        Dim dt As New DataTable
    '        dt = ClsB.GetBrowsWorks(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
    '        txt_az.Text = dt.Rows(0).Item("BrowsWorksCode").ToString
    '        txt_ta.Text = dt.Rows(0).Item("BrowsWorksName").ToString
    '        ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
    '    End If
    'End Sub




    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")

    End Sub
End Class
