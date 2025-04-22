Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class frmBrsWorksSabege
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    
            load_grid()



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
        Dim value As Decimal = CDec(e.GetValue("WorkTypeCode"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.AliceBlue

        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Azure
        ElseIf value = 3 Then
            e.Row.ForeColor = Drawing.Color.Black

            e.Row.BackColor = Drawing.Color.WhiteSmoke
        ElseIf value = 4 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PapayaWhip

        End If


    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrsWorksSabege.aspx", Val(Session("UserId")))

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
        lblTotal.Text = "تعداد كل = " & cntt
        CntTypnow()
    End Sub

    Private Sub load_grid()

        Dim ClsB_ As New ClsBrowsWorks
        'Dim dt1 As New DataTable
        Dim sqlwhere As Integer = Request.QueryString("PwhereSTR")

        dt1 = ClsB_.GetAllBrowsWorksCode(sqlwhere)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()
        CntTyp()
        Dim cntt As Integer = dt1.Rows.Count
        lblTotal.Text = "تعداد كل = " & cntt
    End Sub
    Private Sub CntTyp()
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT tblWorkHdr.WorkTypeCode, Count(tblWorkHdr.WorkTypeCode) AS CountOfWorkTypeCode, tblWorkType.WorkTypeName" & _
         " FROM tblWorkHdr INNER JOIN tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode " & _
       " WHERE (((tblWorkHdr.WorkDateS) Between '" & datS1 & "' And '" & dateE1 & "'))" & _
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
       " WHERE (((tblWorkHdr.WorkDateS) Between '" & a & "' And '" & b & "'))" & _
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

        If Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")
        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        ElseIf Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")
        End If
    End Sub





End Class
