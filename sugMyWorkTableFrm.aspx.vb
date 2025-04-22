Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class sugMyWorkTableFrm

    Inherits System.Web.UI.Page

    Private Sub loadGrid()
        Dim sqlstr As String = "SELECT        * " & _
        " FROM sugGrideUserVW WHERE        (codePersoneli = " & Session("codePersoneli") & ")"
        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridWorkTable.DataSource = dt
            GridWorkTable.DataBind()
        End If



    End Sub
    Protected Sub GridWorkTable_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridWorkTable.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If




        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value2 As Decimal = CDec(e.GetValue("sugStatusID"))
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



    Protected Sub GridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridWorkTable.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("sugID") = e.CommandArgs.CommandArgument
            Session("from") = "user"
            Response.Redirect("sugDetailConversationFrm.aspx")
        End If

        If e.CommandArgs.CommandName = "Ebtall" Then
            Dim dt3 As New DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     sugID       FROM sugTbl " & _
                  " WHERE     (sugStatusID = 1 )  and (sugID = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTable(sqlstr2)
            If dt3.Rows.Count = 0 Then
                msg_box("فقط پیشنهادهای های در وضعیت بررسی را می توانید ابطال کنید")
                Exit Sub
            End If

            Dim sqlstr As String

            sqlstr = "  UPDATE sugTbl  SET   sugStatusID = 5  " & _
                                  " WHERE (sugID = " & e.CommandArgs.CommandArgument & ")  "

            upStat(sqlstr)
            loadGrid()
         
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

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.close()
        End Try

    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadGrid()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
