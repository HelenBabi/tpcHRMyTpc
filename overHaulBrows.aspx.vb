Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class overHaulBrows
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()


            'loadgrdNow(a, b)

        

        End If
        load_gridAll()

    End Sub


    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
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
        meno_arr = ClsAccounting.meno_acc("overHaulBrows.aspx", Val(Session("UserId")))
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
   

    








    'Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
    '    load_gridAll()
    'End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub



   

    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        If Session("unitID") = 19 Then
            sqlstr = "SELECT * from overhaulBrowsVW"
        Else
            sqlstr = "SELECT * from overhaulBrowsVW omurID = " & Session("omurID") & " and kartablStat = 0 and State = 1"
        End If

        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count
        lblTotal.Text = "تعداد  = " & cntt
    End Sub
End Class
