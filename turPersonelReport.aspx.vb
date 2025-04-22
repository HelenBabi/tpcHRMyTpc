Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView


Partial Class turPersonelReport
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("turPersonelReport.aspx", Val(Session("UserId")))
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
  
    'Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
    '    load_grid()

    'End Sub
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
        If value2 = 6 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LimeGreen

        ElseIf value2 = 7 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PaleVioletRed

        ElseIf value2 = 8 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PapayaWhip
        End If

    End Sub
    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand
        Dim sqlstr As String
        If e.CommandArgs.CommandName = "pazir" Then
            sqlstr = "UPDATE     turRefahiRegTbl SET                stat = 6 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            UpdatePanel1.Update()
        End If
     
        If e.CommandArgs.CommandName = "ebtal" Then
            sqlstr = "UPDATE     turRefahiRegTbl SET                stat = 7 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_grid()
            tedadkol()
            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "ebtal2" Then
            sqlstr = "UPDATE     turRefahiRegTbl SET                stat = 8 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
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
            cnn.Close()
        End Try

    End Sub
    Private Sub load_grid()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM turRefahiVW    WHERE  id_tour = " & Val(DropDownList1.SelectedValue) & "   "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()
        tedadkol()

    End Sub

    Private Sub tedadkol()
        Dim dta As DataTable
        Dim sqlstr2 As String
        sqlstr2 = "SELECT    sum(tedad) as tedadkol  " & _
             " FROM turRefahiRegTbl    WHERE  id_tour = " & Val(DropDownList1.SelectedValue) & " and  hazf = 0  "
        dta = ClsDataTable.GetDataTable(sqlstr2)
        If dta.Rows.Count > 0 Then
            'Label3.Text = dta.Rows(0).Item("tedadkol").ToString
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
            'loadGridAll()
            gridKol()
            load_grid()
        End If
        load_grid()
        gridKol()
        'loadGridAll()
    End Sub
    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("codePersoneli").ToString

        'Dim ClsB_ As New ClsBrowsSystems
        'Dim dt2 As New DataTable
        'dt2 = ClsB_.GetAllBrowsSystems(idd)
        'grid.DataSource = dt2
        Dim sqlstr As String

        sqlstr = "SELECT  * from vwTakfol WHERE        (empID = '" & idd & "')"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt


    End Sub
    Private Sub load_cmb()
        Dim dt1 As New Data.DataTable
        sqlstr = " SELECT     magsad , idTour  FROM turRefahiTbl order by idTour Desc "

        dt1 = ClsDataTable.GetDataTable(sqlstr)
        If dt1.Rows.Count > 0 Then
            DropDownList1.DataSource = dt1

            DropDownList1.DataTextField = "magsad"
            DropDownList1.DataValueField = "idTour"
            DropDownList1.DataBind()
        End If


    End Sub





    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        load_grid()
        UpdatePanel2.Update()
    End Sub


    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ' ASPxExporter1.WriteXlsToResponse()
        grvExporter1.FileName = DropDownList1.SelectedItem.Text
        grvExporter1.WriteXlsToResponse()
    End Sub

 





    Private Sub loadGridAll()
        Dim dtanbar As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM turRefahiVW ORDER BY id_tour"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()
        tedadkol()

    End Sub

    Private Sub gridKol()
        Dim dtk As New DataTable

        Dim sqlstrk As String = ""
        sqlstrk = "SELECT     *  FROM turAmarVW ORDER BY idtour DESC"
        dtk = ClsDataTable.GetDataTable(sqlstrk)
        GridView3.DataSource = dtk
        GridView3.DataBind()
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
