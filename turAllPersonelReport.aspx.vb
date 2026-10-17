Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView


Partial Class turAllPersonelReport
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("turAllPersonelReport.aspx", Val(Session("UserId")))
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
    Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
        loadGridAll()
    End Sub

    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic

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
        If value2 = 6 Or value2 = 17 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightGreen

        ElseIf value2 = 7 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightCoral

        ElseIf value2 = 8 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PapayaWhip
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
        If Not IsPostBack Then
            loadGridAll
        End If

        'loadGridAll()
    End Sub
    'Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim idd As Integer
    '    Dim grid As ASPxGridView = CType(sender, ASPxGridView)
    '    idd = grid.GetMasterRowFieldValues("codePersoneli").ToString

    '    'Dim ClsB_ As New ClsBrowsSystems
    '    'Dim dt2 As New DataTable
    '    'dt2 = ClsB_.GetAllBrowsSystems(idd)
    '    'grid.DataSource = dt2
    '    Dim sqlstr As String

    '    sqlstr = "SELECT  * from vwTakfol WHERE        (empID = '" & idd & "')"


    '    Dim dt As New DataTable
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    grid.DataSource = dt


    'End Sub
   




    

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        ' اطمینان از پر بودن گرید
  
        ' بارگزاری مجدد اطلاعات گرید قبل از اکسپورت
        loadGridAll()

        ' تنظیمات اکسپورت
        grvExporter1.GridViewID = ASPxGridView1.ID

        grvExporter1.WriteXlsToResponse(New DevExpress.XtraPrinting.XlsExportOptions() With {
            .ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFile
        })


    End Sub

    Private Sub loadGridAll()
        Dim dtanbar As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM turRefahiVW ORDER BY idd Desc"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()


    End Sub

   

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        Response.Redirect("index.aspx")
    End Sub


    'Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    '    If CheckBox1.Checked Then
    '        loadGridAll()
    '    Else
    '        load_grid()
    '    End If
    '    'UpdatePanel2.Update()
    'End Sub

End Class
