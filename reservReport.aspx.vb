Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class reservReport
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()
            RadioButtonList1.SelectedValue = 0
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            'loadgrdNowByResturanID(a, b, Session("UserType"))
        Else
            'ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            'loadgrdNowByResturanID(a, b, Session("UserType"))
            load_grid()
        End If
        'If Me.IsCallback = True Then
        '    
        'End If

        ' loadgrdNowByResturanID(a, b, Session("UserType"))
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
    Private Sub loadgrdNowByResturanID(ByVal dat1 As String, ByVal dat2 As String, ByVal resturanID As String)
        Dim dt5 As DataTable
        Dim ClsB_ As New reservReportCls
        If resturanID = 0 Then
            dt5 = ClsB_.GetAllBrowsWorks(dat1, dat2)
        Else
            dt5 = ClsB_.GetResturanID(dat1, dat2, resturanID)
        End If



        ASPxGrvHead.DataSource = dt5
        ASPxGrvHead.DataBind()

        '  Dim cntt As Integer = dt5.Rows.Count
        'lblTotal.Text = "تعداد كل = " & cntt
        'CntTypnow()
    End Sub
    

    Private Sub load_grid()
        datS = Request.Form("datepicker")
        dateE = Request.Form("datepicker2")
        lblDateS.Text = datS
        lbldateE.Text = dateE


        If lbldateE.Text = "" Or lblDateS.Text = "" Then
            msg_box("تاریخ شروع و تاریخ اتمام را وارد كنيد")

            Exit Sub
        End If

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


        Dim sqlstr As String
        If RadioButtonList1.SelectedValue = 0 Then
            sqlstr = "SELECT * from refahiRepVW  WHERE      (tarix >= '" & datS1 & "')  AND   (tarix <= '" & dateE1 & "')  "

        ElseIf RadioButtonList1.SelectedValue = 1 Then
            sqlstr = "SELECT * from refahiRepVW  WHERE      (tarix >= '" & datS1 & "')  AND   (tarix <= '" & dateE1 & "')  AND (locID = 1)  "

        ElseIf RadioButtonList1.SelectedValue = 2 Then
            sqlstr = "SELECT * from refahiRepVW  WHERE      (tarix >= '" & datS1 & "')  AND   (tarix <= '" & dateE1 & "')  AND (locID = 2)  "
        End If


        dt1 = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()
        ' CntTyp()
        Dim sqstrtedad As String = "SELECT        COUNT(locID) AS tedad, locationValue, menu, tarix " & _
       " FROM ebtalGridVW GROUP BY locationValue, menu, tarix " & _
             " HAVING        (tarix >= '" & datS1 & "'    AND tarix <= '" & dateE1 & "')"
        Dim dtted As New DataTable
        dtted = ClsDataTable.GetDataTable(sqstrtedad)
        GridView3.DataSource = dtted
        GridView3.DataBind()


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




    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("reservID").ToString

        Dim sqlstr As String
        If RadioButtonList1.SelectedValue = 0 Then
            sqlstr = "SELECT * from ebtalGridVW WHERE        (reservID = " & idd & ") "

        ElseIf RadioButtonList1.SelectedValue = 1 Then
            sqlstr = "SELECT * from ebtalGridVW WHERE        (reservID = " & idd & ")  AND (locID = 1)"

        ElseIf RadioButtonList1.SelectedValue = 2 Then
            sqlstr = "SELECT * from ebtalGridVW WHERE        (reservID = " & idd & ")  AND (locID = 2)"
        End If
        dt2 = ClsDataTable.GetDataTable(sqlstr)


        Label2.Text = dt1.Rows.Count
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub
End Class
