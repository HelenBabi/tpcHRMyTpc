Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class resavationBrowse
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()

            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow(a, b)
        Else
            load_grid()

        End If


    End Sub
    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        Session("tiketId") = e.CommandArgs.CommandArgument
        Session("fromTyp") = "rpt"
        Response.Redirect("frmTicketWorker.aspx")
    End Sub

    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("WorkId").ToString

        Dim sqlstr As String

        sqlstr = "SELECT     tblWorkDtl.WorkId, tblWorkDtl.BaseWorkCode, tblWorkDtl.WorkDtlNote, tblBaseWork.BaseWorkName " & _
                 " FROM         tblWorkDtl INNER JOIN    tblBaseWork ON tblWorkDtl.BaseWorkCode = tblBaseWork.BaseWorkCode " & _
                                   " where(WorkId = " & idd & ")"
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub







    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("resturanID"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightSeaGreen

        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightGreen

        End If


    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrowsWorks.aspx", Val(Session("UserId")))

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

#End Region
    Private Sub loadgrdNow(ByVal dat1 As String, ByVal dat2 As String)
        Dim dt5 As DataTable
        Dim ClsB_ As New ClsBrowsWorks
        dt5 = ClsB_.GetAllBrowsWorks(dat1, dat2)
        ASPxGrvHead.DataSource = dt5
        ASPxGrvHead.DataBind()

        Dim cntt As Integer = dt5.Rows.Count
        'lblTotal.Text = "تعداد كل = " & cntt
        'CntTypnow()
    End Sub

    Private Sub load_grid()
        datS = Request.Form("datepicker")
        dateE = Request.Form("datepicker2")
        lblDateS.Text = datS
        lbldateE.Text = dateE


        'If lbldateE.Text = "" Or lblDateS.Text = "" Then
        '    msg_box("تاریخ شروع و تاریخ اتمام را وارد كنيد")

        '    Exit Sub
        'End If

        If Len(lblDateS.Text) < 10 Then
            datS1 = ClsDateTime.setDate(datS)
        End If

        If Len(lbldateE.Text) < 10 Then
            dateE1 = ClsDateTime.setDate(dateE)
        End If

        If Len(lblDateS.Text) = 10 Then
            datS1 = datS
        End If

        If Len(lbldateE.Text) = 10 Then
            dateE1 = dateE
        End If


        Dim ClsB_ As New ClsBrowsWorks
        'Dim dt1 As New DataTable
        dt1 = ClsB_.GetAllBrowsWorks(datS1, dateE1)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()
        ' CntTyp()
        Dim cntt As Integer = dt1.Rows.Count
        lblTotal.Text = "تعداد كل = " & cntt
    End Sub
    Private Sub CntTyp()
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT tblWorkHdr.WorkTypeCode, Count(tblWorkHdr.WorkTypeCode) AS CountOfWorkTypeCode, tblWorkType.WorkTypeName" & _
         " FROM tblWorkHdr INNER JOIN tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode " & _
       " WHERE (((tblWorkHdr.WorkDateE) Between '" & datS1 & "' And '" & dateE1 & "'))" & _
 "GROUP BY tblWorkHdr.WorkTypeCode, tblWorkType.WorkTypeName "


        dt3 = ClsDataTable.GetDataTable(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count
        For i As Integer = 0 To count_ - 1
            txtcnt += dt3.Rows(i).Item("WorkTypeName").ToString
            txtcnt += " = "
            'If id_type = 1 Then
            '    txtcnt += "سرويس در كارگاه="
            'ElseIf id_type = 2 Then
            '    txtcnt += "ريموت="
            'ElseIf id_type = 3 Then
            '    txtcnt += "سرویس در محل="
            'ElseIf id_type = 4 Then
            '    txtcnt += "سرویس دوره ای="
            'End If
            txtcnt += dt3.Rows(i).Item("CountOfWorkTypeCode").ToString
            txtcnt += "  ;      "
        Next
        lblCnt.Text = txtcnt
    End Sub


    Private Sub CntTypnow()
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT tblWorkHdr.WorkTypeCode, Count(tblWorkHdr.WorkTypeCode) AS CountOfWorkTypeCode, tblWorkType.WorkTypeName" & _
         " FROM tblWorkHdr INNER JOIN tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode " & _
       " WHERE (((tblWorkHdr.WorkDateE) Between '" & a & "' And '" & b & "'))" & _
 "GROUP BY tblWorkHdr.WorkTypeCode, tblWorkType.WorkTypeName "


        dt3 = ClsDataTable.GetDataTable(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count
        For i As Integer = 0 To count_ - 1
            txtcnt += dt3.Rows(i).Item("WorkTypeName").ToString
            txtcnt += " = "
            'If id_type = 1 Then
            '    txtcnt += "سرويس در كارگاه="
            'ElseIf id_type = 2 Then
            '    txtcnt += "ريموت="
            'ElseIf id_type = 3 Then
            '    txtcnt += "سرویس در محل="
            'ElseIf id_type = 4 Then
            '    txtcnt += "سرویس دوره ای="
            'End If
            txtcnt += dt3.Rows(i).Item("CountOfWorkTypeCode").ToString
            txtcnt += "  ;      "
        Next
        lblCnt.Text = txtcnt
    End Sub




    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click


        Response.Redirect("index.aspx")


    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click


        load_grid()
        'If datS1 > dateE1 Then
        '    msg_box("تاریخ شروع نمی تواند بزرگتر از تاریخ اتمام باشد")
        '    ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
        '    Exit Sub
        'End If
        ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    End Sub


End Class
