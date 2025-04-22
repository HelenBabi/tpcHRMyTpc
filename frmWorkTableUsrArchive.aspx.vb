Imports System.Data
Imports System.Data.SqlClient
Partial Class frmWorkTableUsrArchive
    Inherits System.Web.UI.Page
    Dim dtworker, dtusers As New DataTable




    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_.aspx")
        End If
        load_grid_users()

        conttArch()
    End Sub


    Private Sub load_grid_users()


        Dim ClsB_ As New ClsBrowsWorks
        'Dim dt1 As New DataTable
        dtusers = ClsB_.GetBrowsRequestArch(Session("userid"))
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
        If e.CommandArgs.CommandName = "entexab" Then
            'Session("tiketId") = e.CommandArgs.CommandArgument
            'Response.Redirect("frmTicketWorker.aspx")
            Session("fromTyp") = "usr"
            Session("tiketId") = e.CommandArgs.CommandArgument
            Response.Redirect("frmTicketWorker.aspx")
        End If
        If e.CommandArgs.CommandName = "kartabll" Then


            Dim sqlstr As String

            sqlstr = "  UPDATE tblHerja SET             archivee = 0 " & _
                                  "WHERE(erja_id = " & e.CommandArgs.CommandArgument & ")"

            upStat(sqlstr)
            load_grid_users()
            conttArch()
        End If
    End Sub

    Private Sub conttArch()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     erja_id       FROM tblHerja " & _
              " WHERE     (archivee = 1) AND (user_id = " & Session("userid") & ") "


        dt3 = ClsDataTable.GetDataTable(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count

        lblCnt.Text = "تعداد موجود درآرشیو کارتابل: " & "   " & count_
    End Sub



    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
            'msg_box("ثبت شد")
            'load_grid_jari()
            'load_grid_ok()
            'load_grid_MySend()
            'load_grid_okall()
        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.close()
        End Try

    End Sub
End Class
