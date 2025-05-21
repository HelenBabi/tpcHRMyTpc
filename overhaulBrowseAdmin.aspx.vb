Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class overhaulBrowseAdmin

    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

        End If
        load_gridAll()
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("overhaulBrowseAdmin.aspx", Val(Session("UserId")))
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
    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        GridWorkTable.DataSource = dt
        GridWorkTable.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub
    Protected Sub GridWorkTable_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridWorkTable.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("State"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightGreen
        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightYellow
        ElseIf value = 5 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightPink

        End If


    End Sub
    Protected Sub GridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridWorkTable.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub
End Class
