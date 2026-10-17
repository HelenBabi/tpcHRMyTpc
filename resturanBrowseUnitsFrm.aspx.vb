Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Globalization

Partial Class resturanBrowseUnitsFrm
    Inherits System.Web.UI.Page
    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String
    Dim dt2, dt1, dt3 As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow(a, b)
        Else
            load_grid()

        End If

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("resturanBrowseFrm.aspx", Val(Session("UserId")))

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
        Dim sqlstr As String = "select * from resturanUnitsVW WHERE        (SelectedDate Between '" & dat1 & "' And '" & dat2 & "') ORDER BY SelectedDate"
        dt5 = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt5
        ASPxGrvHead.DataBind()


        'GridView3.Visible = True
        'Dim dtg As New DataTable

        'Dim sqlstrg As String = "select * from dashboardVW WHERE        (SelectedDate Between '" & dat1 & "' And '" & dat2 & "') ORDER BY SelectedDate"
        'dtg = ClsDataTable.GetDataTableBoad5(sqlstrg)
        'If dtg.Rows.Count > 0 Then

        '    GridView3.DataSource = dtg
        '    GridView3.DataBind()
        'End If

        'Dim sqlstrCount As String = "SELECT        SUM(quantity) AS summ, SelectedDate         FROM dashboardVW " & _
        '         "GROUP BY SelectedDate HAVING         (SelectedDate Between N'" & dat1 & "' And N'" & dat2 & "') ORDER BY SelectedDate"
        'Dim dts As New DataTable
        'dts = ClsDataTable.GetDataTableBoad5(sqlstrCount)
        'If dts.Rows.Count > 0 Then

        '    Dim cntt As Integer = dts.Rows(0).Item("summ").ToString()
        '    lblTotal.Text = "جمع كل = " & cntt

        'End If


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
        If lblDateS.Text = "" Or lbldateE.Text = "" Then
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

        Dim sqlstr22 As String = ""
        sqlstr22 = "select * from resturanUnitsVW WHERE        (SelectedDate Between '" & datS1 & "' And '" & dateE1 & "')"
        dt1 = ClsDataTable.GetDataTable(sqlstr22)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()
        'CntTyp()


        'Dim dtgg As New DataTable

        'Dim sqlstrgg As String = "select * from dashboardVW WHERE        (SelectedDate Between '" & datS1 & "' And '" & dateE1 & "') ORDER BY SelectedDate"
        'dtgg = ClsDataTable.GetDataTableBoad5(sqlstrgg)
        'If dtgg.Rows.Count > 0 Then
        '    GridView3.Visible = True
        '    GridView3.DataSource = dtgg
        '    GridView3.DataBind()
        'End If

        'Dim sqlstrCount As String = "SELECT        SUM(quantity) AS summ, SelectedDate         FROM dashboardVW " & _
        '    "GROUP BY SelectedDate HAVING         (SelectedDate Between N'" & datS1 & "' And N'" & dateE1 & "') "
        'Dim dts As New DataTable
        'dts = ClsDataTable.GetDataTableBoad5(sqlstrCount)


        'If dts.Rows.Count > 0 Then
        '    Dim cntt As Integer = 0
        '    For i = 0 To dts.Rows.Count - 1
        '        cntt += dts.Rows(i).Item("summ").ToString()
        '    Next i

        '    lblTotal.Text = "جمع كل = " & cntt

        'End If
    End Sub




    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub
    'Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
    '    loadgrdNow()
    'End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
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


    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")


    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As EventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")

    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
