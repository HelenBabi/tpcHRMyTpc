Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class index
    Inherits System.Web.UI.Page
    Dim dtworker, dtusers As New DataTable
    Dim warning_pic As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        'load_grid_workers()
        'contt()
        '  conttArch()
        loadGridTrn()
        'loadGridSug()
        load_gridJob()

    End Sub
    Private Sub loadGridTrn()
        Dim sqlstr As String = "SELECT  * FROM trnDoreReqisterVW WHERE        (personelAdmin = " & Session("codePersoneli") & ") AND (stat = 10)"
        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            trnGridWorkTable.DataSource = dt
            trnGridWorkTable.DataBind()
        End If



    End Sub
    Protected Sub trnGridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles trnGridWorkTable.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("idd") = e.CommandArgs.CommandArgument
            Session("from") = "reciver"
            Response.Redirect("trnDoreDetailFrm.aspx")
        End If

      
    End Sub
    'Protected Sub sugGridWorkTable_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles sugGridWorkTable.HtmlRowPrepared

    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If



    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value2 As Decimal = CDec(e.GetValue("sugStatusID"))
    '    If value2 = 1 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.Orange


    '    ElseIf value2 = 8 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.PaleVioletRed

    '    ElseIf value2 = 10 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.Yellow
    '    ElseIf value2 = 11 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.Orange
    '    ElseIf value2 = 2 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.LightGreen
    '    ElseIf value2 = 3 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.Red
    '    End If
    'End Sub
    Protected Sub trnGridWorkTable_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles trnGridWorkTable.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If



        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value2 As Decimal = CDec(e.GetValue("stat"))
        If value2 = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.Orange


        ElseIf value2 = 8 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PaleVioletRed

        ElseIf value2 = 10 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Yellow
        ElseIf value2 = 11 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Orange
        ElseIf value2 = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightGreen
        ElseIf value2 = 3 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Red
        End If
    End Sub
    'Private Sub loadGridSug()
    '    Dim sqlstr As String = ""
    '    'If Session("modir") = True Then
    '    sqlstr = "SELECT  * FROM sugWorktableReciverVW WHERE        (reciverID = " & Session("userid") & ") "
    '    'Else
    '    '    sqlstr = "SELECT  * FROM sugWorktableReciverVW WHERE        (reciverID = " & Session("userid") & ") and        (sugStatusID = 11)"
    '    'End If


    '    Dim dt As New DataTable

    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    If dt.Rows.Count > 0 Then
    '        sugGridWorkTable.DataSource = dt
    '        sugGridWorkTable.DataBind()
    '    End If

    'End Sub
    'Protected Sub sugGridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles sugGridWorkTable.RowCommand
    '    If e.CommandArgs.CommandName = "moshahede" Then
    '        Session("sugID") = e.CommandArgs.CommandArgument
    '        Session("from") = "reciver"
    '        Response.Redirect("sugDetailConversationFrm.aspx")
    '    End If

    'End Sub

    Private Sub load_gridJob()

        Dim dt As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "SELECT   * from jobJariVW WHERE        (reciverID = " & Session("userid") & ") ORDER BY kmID DESC"

        dt = ClsDataTable.GetDataTable(sqlstr)

        jobGrid.DataSource = dt
        jobGrid.DataBind()
    End Sub



    Protected Sub jobGrid_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles jobGrid.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Dim ts As Integer = e.CommandArgs.CommandArgument.ToString()

            Session("knowlegID") = e.CommandArgs.CommandArgument.ToString()
            Session("from") = "myJob"
            Response.Redirect("jobDetailFrm.aspx")
        End If


    End Sub



   
  
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub
    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConnHD
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub
End Class
