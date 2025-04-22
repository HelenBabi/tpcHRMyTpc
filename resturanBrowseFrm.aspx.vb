Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Globalization

Partial Class resturanBrowseFrm
    Inherits System.Web.UI.Page
   

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            ' meno_acc()

            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow()
        Else
            'loadgrdNow()

        End If

    End Sub









    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("DeviceCode"))
    '    If value = 1 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.AliceBlue

    '    ElseIf value = 2 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.LightBlue

    '    ElseIf value = 3 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.LightCoral

    '    ElseIf value = 3 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.LightGreen
    '    End If


    'End Sub
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
    Private Sub loadgrdNow()
        Dim dt5 As DataTable
        Dim sqlstr As String = "select * from resturanRepNowVW "
        dt5 = ClsDataTable.GetDataTableBoad5(sqlstr)
        ASPxGrvHead.DataSource = dt5
        ASPxGrvHead.DataBind()

        Dim cntt As Integer = dt5.Rows.Count
        lblTotal.Text = "جمع كل = " & cntt
        'CntTypnow()

        Dim dtg As New DataTable

        Dim sqlstrg As String = "select * from resturanCountRepVW "
        dtg = ClsDataTable.GetDataTableBoad5(sqlstrg)
        If dtg.Rows.Count > 0 Then
            GridView3.Visible = True
            GridView3.DataSource = dtg
            GridView3.DataBind()
        End If

    End Sub

    Private Sub load_grid()
        Dim dt2, dt1, dt3 As New DataTable
        Dim datS, dateE, datS1, dateE1 As String


        datS = Request.Form("datepicker")
        dateE = Request.Form("datepicker2")
        lblDateS.Text = datS
        lbldateE.Text = dateE


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
        sqlstr22 = "select * from resturanRepVW WHERE        (Date Between '" & datS1 & "' And '" & dateE1 & "')"
        dt1 = ClsDataTable.GetDataTableBoad5(sqlstr22)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()
        'CntTyp()
        Dim cntt As Integer = dt1.Rows.Count
        lblTotal.Text = "تعداد كل = " & cntt

        'Dim dtg As New DataTable
        'Dim sqlstrg As String = "SELECT    * from figuresVW where (Date Between '" & ClsDateTime.ShamsiToMiladi(datS1) & "' And '" & ClsDateTime.ShamsiToMiladi(dateE1) & "') ORDER BY Date"
        'dtg = ClsDataTable.GetDataTableBoad5(sqlstrg)
        'If dtg.Rows.Count > 0 Then
        '    GridView3.Visible = True
        '    GridView3.DataSource = dtg
        '    GridView3.DataBind()
        'End If
    End Sub


    Private Function ShamsiToMiladi(shamsi As String) As DateTime
        Dim parts() As String = shamsi.Split("/"c)
        If parts.Length <> 3 Then
            Throw New FormatException("فرمت صحیح نیست. فرمت باید به صورت yyyy/MM/dd باشد.")
        End If

        Dim year As Integer = Integer.Parse(parts(0))
        Dim month As Integer = Integer.Parse(parts(1))
        Dim day As Integer = Integer.Parse(parts(2))

        Dim pc As New PersianCalendar()
        Return pc.ToDateTime(year, month, day, 0, 0, 0, 0)
    End Function
    


    




    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        loadgrdNow()
    End Sub

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
        'ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    End Sub


    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click



    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As EventArgs) Handles ibtnExit.Click

        Response.Redirect("index.aspx")

    End Sub
End Class
