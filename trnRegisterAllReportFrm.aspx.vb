Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView


Partial Class trnRegisterAllReportFrm
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnRegisterAllReportFrm.aspx", Val(Session("UserId")))
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
        'Dim value2 As Decimal = CDec(e.GetValue("scoreID"))
        'If value2 = 1 Then
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = Drawing.Color.LimeGreen
        'ElseIf value2 = 2 Then
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = Drawing.Color.Orange
        'ElseIf value2 = 3 Then
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = System.Drawing.Color.PaleVioletRed


        'End If

    End Sub
    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo2" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image52"), Image)
            iamgegrdst.ImageUrl = warning_pic

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
            msg_box("ثبت شد")
            UpdatePanel1.Update()
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub
    Private Sub load_grid()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnAllRegisterVW    WHERE  iddd = " & Val(DropDownList1.SelectedValue) & " "
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            ASPxGridView1.Visible = True
            ASPxGridView1.DataSource = dtanbar
            ASPxGridView1.DataBind()
            'tedadkol()
        Else
            ASPxGridView1.Visible = False

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
            load_grid()

        

        End If
        'If Me.IsCallback Then
        '    loadgridall()
        'End If

        load_grid()
    
    End Sub

    Private Sub load_cmb()
        Dim dt1 As New Data.DataTable
        sqlstr = " SELECT        idd, onvan  AS doree " & _
       " FROM trnBarnameRiziTbl WHERE          (hozuri = 1) order by idd desc"

        dt1 = ClsDataTable.GetDataTable(sqlstr)
        DropDownList1.DataSource = dt1
        DropDownList1.DataTextField = "doree"
        DropDownList1.DataValueField = "idd"
        DropDownList1.DataBind()

    End Sub





    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        load_grid()

   

        UpdatePanel2.Update()

    End Sub




    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub




    Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
        load_grid()

    End Sub







    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged



        loadgridall()


    End Sub
    Private Sub loadgridall()
        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW where hazf = 0    "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()
        'tedadkol()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If ASPxPageControl1.TabPages.Item(0).IsActive = True Then
            grvExporter1.FileName = DropDownList1.SelectedItem.Text
            grvExporter1.WriteXlsxToResponse()
        Else
            ASPxGridViewExporterEtmam.FileName = DropDownList1.SelectedItem.Text
            ASPxGridViewExporterEtmam.WriteXlsxToResponse()

        End If
    End Sub

End Class
