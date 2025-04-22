Imports System.Data
Imports System.Data.SqlClient
Partial Class jobManagementBrowseFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
     

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()
            load_grid()
            upStat()
            'load_gridBySubject()
        Else

        End If
        If Me.IsCallback = True Then
            load_gridBySubject()

            '    '    loadGridno()
            '    '    loadGridStok()
            '    '    loadGridSAll()

            '    '    'load_cmbAnbar()
        End If

        'load_gridUsers()
    End Sub
    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_gridBySubject()
    End Sub

    Private Sub upStat()
        Dim sqlstr As String
        'sqlstr = "UPDATE tblJob SET             stat =1 " & _
        '                         " WHERE(reciver = " & Session("UserId") & " and stat=0)"
        sqlstr = "UPDATE       tblJobResiverTemp  " & _
        "set show = 1 WHERE        (reciverID = " & Session("UserId") & ") AND (show = 0)"
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            cnn.Close()
        End Try
    End Sub



    Private Sub load_grid()

        Dim dt As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "SELECT   * from jobJariVW WHERE        (reciverID = " & Session("userid") & ") ORDER BY kmID DESC"

        dt = ClsDataTable.GetDataTable(sqlstr)

        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub

  

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Dim ts As Integer = e.CommandArgs.CommandArgument.ToString()

            Session("knowlegID") = e.CommandArgs.CommandArgument.ToString()
            Session("from") = "myJob"
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


        Dim dttt As New DataTable
        Dim sqlstrr As String = ""
        sqlstrr = "SELECT   * from jobJariVW WHERE     (subject  LIKE N'%" & subject.Text & "%') and  (reciverID = " & Session("userid") & ")"


        dttt = ClsDataTable.GetDataTable(sqlstrr)
        ASPxGrvHead.DataSource = dttt
        ASPxGrvHead.DataBind()
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
