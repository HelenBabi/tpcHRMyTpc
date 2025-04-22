Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class productDeliverFrm
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

            load_gridAll()
        Else
            load_gridAll()
            'load_grid()
        End If


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


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("productDeliverFrm.aspx", Val(Session("UserId")))
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


    

    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_gridAll()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

  





   
    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from kalaPersonelReportVW WHERE      stat = 0 "
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        ' CntTyp()
        'Dim cntt As Integer = dt.Rows.Count
        'lblTotal.Text = "تعداد  = " & cntt
    End Sub



    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("perID").ToString



        Dim sqlstr As String = ""
        sqlstr = "SELECT         kalaValue, sizeValue, tarix, saat, idDtl, perID,statValue,stat,tarixThavil " & _
       " FROM kalaPersonelVW  WHERE       (perID = " & idd & ")  "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt
        'grid.DataBind()



    End Sub


    Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat"))
        If value = 0 Then
            'e.Row.ForeColor = Drawing.Color.ligh
            '   e.Row.BackColor = Drawing.Color.Aquamarine
        Else
            'e.Row.ForeColor = Drawing.Color.Green
            e.Row.BackColor = Drawing.Color.LightGreen
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
