Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports DevExpress.Web.ASPxGridView

Partial Class trnDoreByDeviceBrowsFrm
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

            'ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            'loadgrdNow(a, b)
            ''load_gridAll()
            load_gridAll()
            load_gridAllEtmam()
        Else
            'load_gridAll()
            'load_grid()
        End If
        load_gridAll()
        load_gridAllEtmam()
    End Sub


   



    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim saatNiaz As Decimal = CDec(e.GetValue("saatNiaz"))
    '    Dim jam As Decimal = CDec(e.GetValue("jam"))
    '    If jam < saatNiaz Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LightCoral

    '        'ElseIf value = 2 Then
    '        '    e.Row.ForeColor = Drawing.Color.Black
    '        '    e.Row.BackColor = System.Drawing.Color.PaleVioletRed


    '    End If


    'End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnDoreByDeviceBrowsFrm.aspx", Val(Session("UserId")))
        If meno_arr(0) = False Then
            Response.Redirect("login_2.aspx")
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

#End Region
    'Private Sub loadgrdNow(ByVal dat1 As String, ByVal dat2 As String)


    '    Dim dt As New DataTable
    '    Dim sqlstr As String
    '    sqlstr = "SELECT * from trnDoreByDeviceVW  WHERE     (tarix BETWEEN '" & dat1 & "' AND '" & dat2 & "')"
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    ASPxGrvHead.DataSource = dt
    '    ASPxGrvHead.DataBind()

    '    Dim cntt As Integer = dt.Rows.Count
    '    lblTotal.Text = "تعداد  = " & cntt
    '    ' CntTypnow()
    'End Sub

    'Private Sub load_grid()
    '    datS = Request.Form("datepicker")
    '    dateE = Request.Form("datepicker2")
    '    lblDateS.Text = datS
    '    lbldateE.Text = dateE


    '    'If lbldateE.Text = "" Or lblDateS.Text = "" Then
    '    '    msg_box("تاریخ شروع و تاریخ اتمام را وارد كنيد")

    '    '    Exit Sub
    '    'End If
    '    If lblDateS.Text = "" Then
    '        Exit Sub
    '    End If
    '    If Len(lblDateS.Text) < 10 Then
    '        datS1 = ClsDateTime.setDate(datS)
    '    End If

    '    If Len(lbldateE.Text) < 10 Then
    '        dateE1 = ClsDateTime.setDate(dateE)
    '    End If

    '    If Len(lblDateS.Text) = 10 Then
    '        datS1 = datS
    '    End If

    '    If Len(lbldateE.Text) = 10 Then
    '        dateE1 = dateE
    '    End If


    '    Dim dt As New DataTable
    '    Dim sqlstr As String
    '    sqlstr = "SELECT * from trnDoreByDeviceVW  WHERE     (tarix BETWEEN '" & datS1 & "' AND '" & dateE1 & "')"
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    ASPxGrvHead.DataSource = dt
    '    ASPxGrvHead.DataBind()

    '    ' CntTyp()
    '    Dim cntt As Integer = dt.Rows.Count
    '    lblTotal.Text = "تعداد  = " & cntt
    'End Sub








    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_gridAll()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click

        If ASPxPageControl1.TabPages.Item(0).IsActive = True Then
            grvExporter1.WriteXlsxToResponse()
        Else
            ASPxGridViewExporterEtmam.WriteXlsxToResponse()

        End If


    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub



    'Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click


    '    load_grid()
    '    'If datS1 > dateE1 Then
    '    '    msg_box("تاریخ شروع نمی تواند بزرگتر از تاریخ اتمام باشد")
    '    '    ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    '    '    Exit Sub
    '    'End If
    '    ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    'End Sub


    'Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    '    If CheckBox1.Checked = True Then
    '        load_gridAll()
    '    Else
    '        load_grid()
    '    End If


    'End Sub
    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      tarix, doreID, codePersoneli, onvan, SUM(modat) / 60 AS jam, saatNiaz, nam, family, makan,deviceID " & _
       " FROM trnDoreByDeviceVW " & _
                " GROUP BY doreID,codePersoneli, onvan, saatNiaz, nam, family, makan,deviceID,tarix  "
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dt
            ASPxGrvHead.DataBind()

            ' CntTyp()

        End If

        'lblTotal.Text = "تعداد  = " & cntt
    End Sub
    Private Sub load_gridAllEtmam()


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       tarix,doreID, codePersoneli, onvan, SUM(modat) / 60 AS jam, saatNiaz, nam, family, makan,deviceID  " & _
       " FROM trnDoreByDeviceArcVW " & _
                " GROUP BY doreID,codePersoneli, onvan, saatNiaz, nam, family, makan,deviceID,tarix  "
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView1.DataSource = dt
            ASPxGridView1.DataBind()

            ' CntTyp()

        End If

        'lblTotal.Text = "تعداد  = " & cntt
    End Sub
    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt2 As DataTable
        'Dim gate As 
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        HiddenField1.Value = grid.GetMasterRowFieldValues("codePersoneli").ToString
        Dim doreID As Integer = grid.GetMasterRowFieldValues("doreID").ToString
        ', modeel
        Dim sqlstr As String
        'AND (kodeModel = " & modeel & ")

        sqlstr = " SELECT       * from trnDoreByDeviceVW WHERE        (codePersoneli = " & HiddenField1.Value & ")  and (doreID = " & doreID & ") "
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub
    Protected Sub ASPxGridViewEtmam_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt2 As DataTable
        'Dim gate As 
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        Dim codePersoneli As Long = grid.GetMasterRowFieldValues("codePersoneli").ToString
        Dim doreID As Integer = grid.GetMasterRowFieldValues("doreID").ToString
        ', modeel
        Dim sqlstr As String
        'AND (kodeModel = " & modeel & ")

        sqlstr = " SELECT       * from trnDoreByDeviceArcVW WHERE        (codePersoneli = " & codePersoneli & ")  and (doreID = " & doreID & ") "
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub
End Class
