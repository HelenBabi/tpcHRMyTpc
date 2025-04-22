Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class frmWorkTableAdminAll
    Inherits System.Web.UI.Page
    Dim dtworker, dtusers As New DataTable
    Dim warning_pic As String = ""
    Protected Sub GridKartabl_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(GridKartabl.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If

    End Sub
    Protected Sub GridKartabl_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridKartabl.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat_"))
        Dim strval As String = e.GetValue("date_s")
        If strval < ClsDateTime.GetDateNow And value = 5 Or value = 0 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Salmon
            'Else
            '    e.Row.ForeColor = Drawing.Color.Black
            '    e.Row.BackColor = System.Drawing.Color.Azure

        End If


        If value = 0 Then
            warning_pic = "images/Icons/loading_transparent.gif"

        ElseIf value = 3 Then

            warning_pic = "images/Icons/orangerefresh.png"

        ElseIf value = 2 Then

            warning_pic = "images/Icons/ok2.png"

        ElseIf value = 4 Then

            warning_pic = "images/Icons/person.ico"
        ElseIf value = 5 Then

            warning_pic = "images/Icons/user.png"
        ElseIf value = 6 Then

            warning_pic = "images/Icons/system.png"
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_.aspx")
        End If
        load_grid_users()
        contt()
        conttArch()
    End Sub



    Private Sub load_grid_users()
        Dim ClsB_ As New ClsBrowsWorks
        dtusers = ClsB_.GetAdminRequestAll

        GridKartabl.DataSource = dtusers
        GridKartabl.DataBind()


    End Sub
    'Protected Sub GridKartabl_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridKartabl.RowCommand
    '    If e.CommandName = "s" Then
    '        Dim rowindex As Integer = CInt(e.CommandArgument)
    '        Dim row As GridViewRow = GridKartabl.Rows(rowindex)
    '        Dim tiketId As Int64 = Val(row.Cells(0).Text)
    '        Session("tiketId") = tiketId
    '        Response.Redirect("frmTicketWorker.aspx")
    '    End If
    'End Sub


    Protected Sub GridKartabl_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridKartabl.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            Session("fromTyp") = "admnAll"
            Session("tiketId") = e.CommandArgs.CommandArgument
            Response.Redirect("frmTicketWorker.aspx")
        End If
        If e.CommandArgs.CommandName = "archiveee" Then


            Dim sqlstr As String

            sqlstr = "  UPDATE tblHerja SET             archivee = 1 " & _
                                  "WHERE(erja_id = " & e.CommandArgs.CommandArgument & ")"

            upStat(sqlstr)
            load_grid_users()
            contt()
            conttArch()
        End If
        If e.CommandArgs.CommandName = "Ebtall" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     erja_id       FROM tblHerja " & _
                  " WHERE     (stat_ = 0) AND (erja_id = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTable(sqlstr2)
            If dt3.Rows.Count = 0 Then
                msg_box("فقط درخواست های در وضعیت انتظار را می توانید ابطال کنید")
                Exit Sub
            End If

            Dim sqlstr As String

            sqlstr = "  UPDATE tblHerja  SET   stat_ = 9  " & _
                                  " WHERE (erja_id = " & e.CommandArgs.CommandArgument & ") and (stat_= 0)"

            upStat(sqlstr)
            load_grid_users()
            contt()
            conttArch()
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
    Private Sub conttArch()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     erja_id       FROM tblHerja " & _
              " WHERE     (archivee = 1) AND (user_id = " & Session("userid") & ") "


        dt3 = ClsDataTable.GetDataTable(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count

        lblCntrch.Text = " آرشیو کارتابل=  " & "   " & count_
    End Sub

    Private Sub contt()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     erja_id       FROM tblHerja " & _
              " WHERE     (stat_ <> 9)  "


        dt3 = ClsDataTable.GetDataTable(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count

        lblCnt0.Text = "تعداد  = " & "   " & count_
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

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        If Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")
        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        ElseIf Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")
        End If
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        ASPxGridViewExporter1.WriteXlsToResponse()
    End Sub
End Class
