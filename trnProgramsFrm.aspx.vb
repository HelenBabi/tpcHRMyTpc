Imports System.Data
Imports System.Data.SqlClient
Partial Class trnProgramsFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_grid_File
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

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
    Protected Sub ASPxGrvHead0_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles ASPxGrvHead0.RowDataBound


        If (DataBinder.Eval(e.Row.DataItem, "stat")) = 12 Then
            e.Row.BackColor = System.Drawing.Color.LightGreen
        ElseIf (DataBinder.Eval(e.Row.DataItem, "stat")) = 13 Then
            e.Row.BackColor = System.Drawing.Color.LightSalmon
        ElseIf (DataBinder.Eval(e.Row.DataItem, "stat")) = 15 Then
            e.Row.BackColor = System.Drawing.Color.LightGray
        ElseIf (DataBinder.Eval(e.Row.DataItem, "stat")) = 9 Then
            e.Row.BackColor = System.Drawing.Color.LightYellow
        End If

        'If (DataBinder.Eval(e.Row.DataItem, "lnkDownload")) <> "" Then
        '    e.Row.BackColor = System.Drawing.Color.Red
        'End If

    End Sub

            'Protected Sub ASPxGrvHead0_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead0.HtmlRowPrepared
            '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            '        Return
            '    End If
            '    Dim value As String = CDec(e.GetValue("tarixEtmam"))
            '    If value < ClsDateTime.GetDateNow() Then

            '        e.Row.ForeColor = Drawing.Color.Black

            '        e.Row.BackColor = Drawing.Color.OrangeRed


            '    End If


            'End Sub


    Private Sub load_grid_File()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT * from trnDoreGridVW ORDER BY stat"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

    End Sub
    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)

        Dim fileid As Integer = 0
        If ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString() <> "" Then
            fileid = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())
        End If


        Dim sqlstrd As String
        Dim dtd As New DataTable
        sqlstrd = "select  id, content_name, content_file  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)
        If dtd.Rows.Count > 0 Then
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("content_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("content_file"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این برنامه فایل ضمیمه نشده است")
        End If
        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()



    End Sub
End Class
