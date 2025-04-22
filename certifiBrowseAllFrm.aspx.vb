Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class certifiBrowseAllFrm

    Inherits System.Web.UI.Page

    

    Protected Sub ASPxGrvHead_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared

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


      
        End If

    End Sub
    Private Sub load_grid()


        Dim sqlstr As String

        sqlstr = "SELECT     * from perInfoGavahiRepVW WHERE        stat = 6"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
        'CntTyp()
       
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("codePersoneliii") = e.CommandArgs.CommandArgument

            Response.Redirect("certifiDetail.aspx")


        End If
        Dim tim As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        If e.CommandArgs.CommandName = "Ebtall" Then
     

            Dim sqlstr As String

            sqlstr = "  UPDATE gavahi  SET   stat= 2  ,  ebtal_user = " & Session("userid") & ", ebtal_date = N'" & tim & "'" & _
                                  " WHERE (new_later_num = " & e.CommandArgs.CommandArgument & ") "

            upStat(sqlstr)
            msg_box("ابطال شد")
            load_grid()
        End If
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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_grid()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
