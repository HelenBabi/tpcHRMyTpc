Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class trnBrowsOutTechersFrm
    Inherits System.Web.UI.Page
    Dim warning_pic As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then

        End If

        load_grid()
    End Sub



    Protected Sub GridKartabl_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then

            Session("techID") = e.CommandArgs.CommandArgument
            Response.Redirect("trnOutTechersDetailFrm.aspx")
        End If
    End Sub

    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("techID").ToString

        Dim sqlstr As String

        sqlstr = "SELECT  * from trnTechersDtlTbl WHERE        (techID = " & idd & ")"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt
        'grid.DataBind()



    End Sub


    'Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("saxt"))
    '    If value = 0 Then
    '        e.Row.ForeColor = Drawing.Color.Red
    '        '   e.Row.BackColor = Drawing.Color.Aquamarine
    '    Else
    '        e.Row.ForeColor = Drawing.Color.Blue
    '        '   e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
    '    End If
    'End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnBrowsTechersFrm.aspx", Val(Session("UserId")))

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

        sqlstr = "SELECT * from trnTechersOutTbl"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
        lblCnt.Text = dt.Rows.Count
    End Sub






    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")

    End Sub





    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        BindGridviewData()
        subject.Focus()

    End Sub
    Private Sub BindGridviewData()

        Dim dt3 As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT *      from trnTechersOutTbl  where  (nam LIKE N'%" & subject.Text & "%') OR " & _
                         "(codemeli LIKE N'%" & subject.Text & "%') OR " & _
                        " (codeEgtesadi LIKE N'%" & subject.Text & "%') OR " & _
                        " (shenasemeli LIKE N'%" & subject.Text & "%') OR " & _
                        " (sabtNo LIKE N'%" & subject.Text & "%') OR " & _
                        " (tel LIKE N'%" & subject.Text & "%') OR " & _
                                " (mahaleEgamat LIKE N'%" & subject.Text & "%') Or " & _
                        " (malekiatType LIKE '%" & subject.Text & "%') or " & _
         " (modirAmel LIKE '%" & subject.Text & "%')"

        dt3 = ClsDataTable.GetDataTable(sqlstr)
        If dt3.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dt3
            ASPxGrvHead.DataBind()
            lblCnt.Text = dt3.Rows.Count
        End If


    End Sub
    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        BindGridviewData()
    End Sub
End Class


