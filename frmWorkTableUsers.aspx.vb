Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class frmWorkTableUsers
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
        'If value = 0 Then
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = System.Drawing.Color.PapayaWhip
        'Else
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = System.Drawing.Color.Azure

        'End If


        If value = 0 Then
            warning_pic = "images/Icons/loading_transparent.gif"

        ElseIf value = 1 Then

            warning_pic = "images/Icons/orangerefresh.png"

        ElseIf value = 3 Then

            warning_pic = "images/Icons/loading_transparent.gif"


        ElseIf value = 2 Then

            warning_pic = "images/Icons/ok2.png"

        ElseIf value = 4 Then

            warning_pic = "images/Icons/person.ico"
        ElseIf value = 5 Then

            warning_pic = "images/Icons/user.png"
        ElseIf value = 6 Then

            warning_pic = "images/Icons/system.png"
        ElseIf value = 9 Then

            warning_pic = "Icons/exit5.png"
        End If


    End Sub
   
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        load_grid_users()
        contt()
        conttArch()
    End Sub

  

    Private Sub load_grid_users()


        Dim ClsB_ As New ClsBrowsWorks
        'Dim dt1 As New DataTable
        dtusers = ClsB_.GetBrowsRequest(Session("codePersoneli"))
        If dtusers.Rows.Count > 0 Then
            GridKartabl.DataSource = dtusers
            GridKartabl.DataBind()

            HiddenFieldUserID.Value = dtusers.Rows(0).Item("user_id").ToString()
        End If


    End Sub
    

    Protected Sub GridKartabl_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridKartabl.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            Session("fromTyp") = "usr"
            Session("tiketId") = e.CommandArgs.CommandArgument
            Response.Redirect("frmTicketWorker.aspx")
        End If
        If e.CommandArgs.CommandName = "nazar" Then

            Session("tiketId") = e.CommandArgs.CommandArgument
            Session("useridd") = HiddenFieldUserID.Value

            Response.Redirect("ratinTicketFrm.aspx")
        End If
        
        If e.CommandArgs.CommandName = "Ebtall" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     erja_id       FROM tblHerja " & _
                  " WHERE     (stat_ = 0 or stat_ = 6 )  and (erja_id = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTableHD(sqlstr2)
            If dt3.Rows.Count = 0 Then
                msg_box("فقط درخواست های در وضعیت انتظار را می توانید ابطال کنید")
                Exit Sub
            End If

            Dim sqlstr As String

            sqlstr = "  UPDATE tblHerja  SET   stat_ = 9  " & _
                                  " WHERE (erja_id = " & e.CommandArgs.CommandArgument & ") and (stat_ = 0  or stat_ = 6 ) "

            upStat(sqlstr)
            load_grid_users()
            contt()
            conttArch()
        End If
    End Sub
    
    Private Sub conttArch()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     erja_id       FROM tblHerja " & _
              " WHERE     (archivee = 1) AND (user_id = " & Session("userid") & ")  AND stat_<>9 "


        dt3 = ClsDataTable.GetDataTableHD(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count

        'lblCntrch.Text = " آرشیو کارتابل=  " & "   " & count_
    End Sub

    Private Sub contt()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     erja_id       FROM tblHerja " & _
              " WHERE     (archivee = 0) AND (user_id = " & Session("userid") & ") AND stat_<>9 "


        dt3 = ClsDataTable.GetDataTableHD(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count

        'lblCnt0.Text = "تعداد موجود در کارتابل = " & "   " & count_
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
            cnn.close()
        End Try

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
