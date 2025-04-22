Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView


Partial Class vaccinationPersonelReport
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("vaccinationPersonelReport.aspx", Val(Session("UserId")))
        'ibtnAdd.Visible = meno_arr(1)
        'ibtnEdit.Visible = meno_arr(2)
        'ibtnSave.Visible = meno_arr(3)
        'ibtnSaveNew.Visible = meno_arr(4)
        'ibtnCancel.Visible = meno_arr(5)
        'ibtnDelete.Visible = meno_arr(6)
        'ibtnDeleted.Visible = meno_arr(7)
        'ibtnHistory.Visible = meno_arr(8)
        'ibtnDeactived.Visible = meno_arr(9)
        'ibtnPrint.Visible = meno_arr(10)
        'ibtnExit.Visible = meno_arr(11)
    End Sub

    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub
    Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        warning_pic = "images/person_pic/" & value & ".jpg"
        'warning_pic = "images/Icons/loading_transparent.gif"



        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value2 As Decimal = CDec(e.GetValue("stat"))
        If value2 = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LimeGreen

        ElseIf value2 = 3 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PaleVioletRed


        End If

    End Sub
    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand
        Dim sqlstr As String
        If e.CommandArgs.CommandName = "dell" Then
            sqlstr = "UPDATE     tblTourRegist SET                stat = 2 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "geybat" Then
            sqlstr = "UPDATE     tblTourRegist SET                stat = 3 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "ebtal" Then
            sqlstr = "delete from tblTourRegist where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            UpdatePanel1.Update()
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
    Private Sub load_grid()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM vacinaciunVW    WHERE  id_tour = " & DropDownList1.SelectedValue & "   "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()
        tedadkol()

    End Sub

    Private Sub tedadkol()
        Dim dta As DataTable
        Dim sqlstr2 As String
        sqlstr2 = "SELECT    sum(tedad) as tedadkol  " & _
             " FROM tblTourRegist    WHERE  id_tour = " & DropDownList1.SelectedValue & " and  hazf = 0  "
        dta = ClsDataTable.GetDataTable(sqlstr2)
        If dta.Rows.Count > 0 Then
            Label3.Text = dta.Rows(0).Item("tedadkol").ToString
        End If

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        meno_acc()
        If Me.IsPostBack = False Then

            load_cmb()
        End If
        If Me.IsCallback Then
            load_grid()
        End If

        load_grid()
    End Sub

    Private Sub load_cmb()
        Dim dt1 As New Data.DataTable
        sqlstr = " SELECT     magsad , idTour  FROM tblTour WHERE        (CodeClosed = 0) ORDER BY idTour DESC "
        dt1 = ClsDataTable.GetDataTable(sqlstr)
        DropDownList1.DataSource = dt1
        DropDownList1.DataTextField = "magsad"
        DropDownList1.DataValueField = "idTour"
        DropDownList1.DataBind()

    End Sub





    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        load_grid()
        UpdatePanel2.Update()
    End Sub


    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ' ASPxExporter1.WriteXlsToResponse()
        ASPxGridViewExporter1.WriteXlsToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub







    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged



        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM vacinaciunVW    "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()
        tedadkol()


    End Sub
End Class
