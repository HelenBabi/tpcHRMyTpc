Imports System.Data
Imports System.Data.SqlClient
Partial Class jobMySentBrowseFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_grid()
        gridFind.Visible = False
        'load_gridUsers()
    End Sub






    Private Sub load_grid()

        Dim dt As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "SELECT   * from jobMySentVW WHERE        (userID = " & Session("userid") & ") ORDER BY kmID DESC"

        dt = ClsDataTable.GetDataTable(sqlstr)

        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Dim ts As Integer = e.CommandArgs.CommandArgument.ToString()

            Session("knowlegID") = e.CommandArgs.CommandArgument.ToString()
            Session("from") = "mySent"
            Response.Redirect("jobDetailFrm.aspx")

        End If
        'If e.CommandArgs.CommandName = "likee" Then

        '    Dim kmID As Integer = e.CommandArgs.CommandArgument.ToString()
        '    Dim sqlstr As String = "INSERT INTO knowlegmanagmentLikeTbl " & _
        '                   "(kmID, userID,tarix ,saat)             VALUES(" & kmID & ", " & Session("userid") & ",'" & ClsDateTime.GetDateNow & "','" & ClsDateTime.GetTimeNow & "')"
        '    upStat(sqlstr)
        '    load_grid()
        'End If
        'If e.CommandArgs.CommandName = "tesdadLike" Then

        '    Dim furl As String = "knowlegLikes.aspx?PwhereSTR=" & Val(e.CommandArgs.CommandArgument.ToString()) & ""
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=250,width=600,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
        'End If

    End Sub

    Protected Sub gridFind_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles gridFind.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Dim ts As Integer = e.CommandArgs.CommandArgument.ToString()

            Session("knowlegID") = e.CommandArgs.CommandArgument.ToString()
            Session("from") = "mySent"
            Response.Redirect("jobDetailFrm.aspx")
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
            cnn.Close()
        End Try

    End Sub

    Private Sub load_gridBySubject()
        ASPxGrvHead.Visible = False
        gridFind.Visible = True
        Dim dttt As New DataTable
        Dim sqlstrr As String = ""
        sqlstrr = "SELECT   * from jobMySentVW WHERE     (subject  LIKE N'%" & subject.Text & "%') and  (userID = " & Session("userid") & ")"


        dttt = ClsDataTable.GetDataTable(sqlstrr)
        gridFind.DataSource = dttt
        gridFind.DataBind()
    End Sub



    

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        load_gridBySubject()
    End Sub

    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("olaviat"))
        If value = 0 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.AliceBlue

        ElseIf value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Orange
        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black

            e.Row.BackColor = Drawing.Color.OrangeRed


        End If


    End Sub

    Protected Sub imgKartabl_Click(sender As Object, e As EventArgs) Handles imgKartabl.Click

        Response.Redirect("index.aspx")
  
    End Sub
End Class
