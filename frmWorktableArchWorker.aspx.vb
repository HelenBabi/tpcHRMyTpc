Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class frmWorktableArchWorker
    Inherits System.Web.UI.Page
    Dim dtworker, dtusers As New DataTable
    Dim warning_pic As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_.aspx")
        End If
        load_grid_workers()
        'contt()

    End Sub
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
        End If

    End Sub


    'Private Sub contt()
    '    Dim dt3 As DataTable
    '    Dim sqlstr, txtcnt As String
    '    txtcnt = ""
    '    sqlstr = " SELECT     erja_id, Typestr   FROM tblHerja  WHERE     (Typestr = '" & Session("WorkType") & "') AND (stat_ <> 2) AND (stat_ <> 9)   AND (mojavez = 0) "


    '    dt3 = ClsDataTable.GetDataTable(sqlstr)

    '    Dim count_ As Integer = dt3.Rows.Count

    '    lblCnt0.Text = "تعداد موجود در کارتابل = " & "   " & count_
    'End Sub
    Private Sub load_grid_workers()

        Dim ClsB_ As New ClsBrowsWorks

        dtworker = ClsB_.GetBrowsTicketsArchWorker(Session("WorkType"))

        GridKartabl.DataSource = dtworker
        GridKartabl.DataBind()
        Dim cntt As Integer = dtworker.Rows.Count
        lblCnt0.Text = "تعداد   = " & cntt
    End Sub


    Protected Sub GridKartabl_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridKartabl.RowCommand
        If e.CommandArgs.CommandName = "mosh" Then
            Session("tiketId") = e.CommandArgs.CommandArgument
            Session("fromTyp") = "worker"
            Response.Redirect("frmTicketWorker.aspx")
        End If

        If e.CommandArgs.CommandName = "archiveee" Then

            Dim sqlstr As String

            sqlstr = "  UPDATE tblHerja SET             archivee = 0 " & _
                                  "WHERE(erja_id = " & e.CommandArgs.CommandArgument & ")"
            upStat(sqlstr)
            load_grid_workers()


        End If
    End Sub
    'Private Sub conttArch()
    '    Dim dt3 As DataTable
    '    Dim sqlstr, txtcnt As String
    '    txtcnt = ""
    '    sqlstr = "SELECT     erja_id       FROM tblHerja " & _
    '          " WHERE     (archivee = 1) AND (tblHerja.Typestr = " & Session("WorkType") & ")  AND stat_<>9 "


    '    dt3 = ClsDataTable.GetDataTable(sqlstr)

    '    Dim count_ As Integer = dt3.Rows.Count

    '    lblCntrch.Text = " آرشیو کارتابل=  " & "   " & count_
    'End Sub
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

    Protected Sub ibtnDeactived_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnDeactived.Click
        load_grid_workers()
    End Sub
End Class
