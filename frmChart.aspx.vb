Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Web.Management.WebManagementEvent

Partial Class frmChart
    Inherits System.Web.UI.Page
    Public datS, dateE, datS1, dateE1 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then

            'Dim a As String = ClsDateTime.GetDateNow
            'Dim b As String = ClsDateTime.GetDateNow
            'ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")


            load_grd()
        End If
        'grdsaxteman_load()


        'load_kargah()
        'load_remot()
        'load_mahal()
        'load_dorei()
        'load_shabake()
        'load_tedadsystem()
    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmChart.aspx", Val(Session("UserId")))

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

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        'grvExporter1.WriteXlsToResponse()

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

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

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
        load_kargah2()
        load_remot2()
        load_mahal2()
        load_dorei2()
        load_grd2()
        load_shabake2()
        load_Passive()
        ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    End Sub
    Private Sub load_shabake2()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS tshbk, tblBuilding.BuildingName " & _
                 " FROM         tblBuilding INNER JOIN " & _
                  "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                   "   tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
                   " GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName, tblWorkHdr.WorkDateE " & _
                   " HAVING      (tblWorkHdr.WorkTypeCode = 5) AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "') "
        Dim dtShbk2 As New DataTable
        dtShbk2 = ClsDataTable.GetDataTable(sqlstr)

        WebChartControl1.Series.Item(4).DataSource = dtShbk2
        WebChartControl1.DataBind()
    End Sub

    Private Sub load_kargah2()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS ss, tblBuilding.BuildingName " & _
                 " FROM         tblBuilding INNER JOIN " & _
                  "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                   "   tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
                   " GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName, tblWorkHdr.WorkDateE " & _
                   " HAVING      (tblWorkHdr.WorkTypeCode = 1) AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "') "
        Dim dtS As New DataTable
        dtS = ClsDataTable.GetDataTable(sqlstr)

        WebChartControl1.Series.Item(0).DataSource = dtS
        WebChartControl1.DataBind()
    End Sub

    Private Sub load_remot2()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS rr, tblBuilding.BuildingName " & _
                 " FROM         tblBuilding INNER JOIN " & _
                  "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                   "   tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
                   " GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName, tblWorkHdr.WorkDateE " & _
                   " HAVING      (tblWorkHdr.WorkTypeCode = 2) AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "') "
        Dim dtR As New DataTable
        dtR = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(1).DataSource = dtR
        WebChartControl1.DataBind()
    End Sub

    Private Sub load_mahal2()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS mahal, tblBuilding.BuildingName " & _
                  " FROM         tblBuilding INNER JOIN " & _
                   "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                    "   tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
                    " GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName, tblWorkHdr.WorkDateE " & _
                    " HAVING      (tblWorkHdr.WorkTypeCode = 3) AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "') "
        Dim dtm As New DataTable
        dtm = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(2).DataSource = dtm
        WebChartControl1.DataBind()
    End Sub
    Private Sub load_Passive()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS pass, tblBuilding.BuildingName " & _
                 " FROM         tblBuilding INNER JOIN " & _
                  "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                   "   tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
                   " GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName, tblWorkHdr.WorkDateE " & _
                   " HAVING      (tblWorkHdr.WorkTypeCode = 6) AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "') "
        Dim dtdo As New DataTable
        dtdo = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(5).DataSource = dtdo
        WebChartControl1.DataBind()

    End Sub
    Private Sub load_dorei2()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS dorei, tblBuilding.BuildingName " & _
                 " FROM         tblBuilding INNER JOIN " & _
                  "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                   "   tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
                   " GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName, tblWorkHdr.WorkDateE " & _
                   " HAVING      (tblWorkHdr.WorkTypeCode = 4) AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "') "
        Dim dtdo As New DataTable
        dtdo = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(3).DataSource = dtdo
        WebChartControl1.DataBind()

    End Sub

    Private Sub load_grd()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS tedad, tblBuilding.BuildingName, tblBuilding.BuildingCode " & _
"FROM         tblBuilding INNER JOIN " & _
 "                     tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
  "                    tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
"GROUP BY tblBuilding.BuildingName, tblBuilding.BuildingCode"
        Dim dtS As New DataTable
        dtS = ClsDataTable.GetDataTable(sqlstr)
        grdServic.DataSource = dtS
        grdServic.DataBind()
    End Sub
    Private Sub load_grd2()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS tedad, tblBuilding.BuildingName, tblBuilding.BuildingCode " & _
                    " FROM         tblBuilding INNER JOIN " & _
                      " tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                      " tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
" GROUP BY tblBuilding.BuildingName, tblBuilding.BuildingCode, tblWorkHdr.WorkDateE " & _
" HAVING      (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "')"
        Dim dtS As New DataTable
        dtS = ClsDataTable.GetDataTable(sqlstr)
        grdServic.DataSource = dtS
        grdServic.DataBind()
    End Sub
 
    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim dt2 As DataTable
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("BuildingCode").ToString

        Dim sqlstr As String
        Dim tst As String = dateE1

        If dateE1 = "" Then

            sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS tedad2, tblBuilding.BuildingCode, tblWorkType.WorkTypeName " & _
             " FROM         tblBuilding INNER JOIN " & _
              "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
               "  tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode INNER JOIN " & _
                " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode " & _
" GROUP BY tblBuilding.BuildingCode, tblWorkType.WorkTypeName  HAVING      (tblBuilding.BuildingCode = " & idd & ") "

            dt2 = ClsDataTable.GetDataTable(sqlstr)
            grid.DataSource = dt2
        End If

        If dateE1 <> "" Then

            sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS tedad2, tblBuilding.BuildingCode, tblWorkType.WorkTypeName " & _
                 " FROM         tblBuilding INNER JOIN " & _
                  "    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                   "  tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode INNER JOIN " & _
                    " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode " & _
    " GROUP BY tblBuilding.BuildingCode, tblWorkType.WorkTypeName,tblWorkHdr.WorkDateE " & _
    "  HAVING      (tblBuilding.BuildingCode = " & idd & ") AND (tblWorkHdr.WorkDateE >= '" & datS1 & "' AND tblWorkHdr.WorkDateE <= '" & dateE1 & "')"
            dt2 = ClsDataTable.GetDataTable(sqlstr)
            grid.DataSource = dt2
        End If


        'grid.DataBind()

    End Sub

    Private Sub load_kargah()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS ss, tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
"FROM         tblBuilding INNER JOIN " & _
 "                     tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
  "                    tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
"GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
 "       HAVING(tblWorkHdr.WorkTypeCode = 1)"
        Dim dtS As New DataTable
        dtS = ClsDataTable.GetDataTable(sqlstr)

        WebChartControl1.Series.Item(0).DataSource = dtS
        WebChartControl1.DataBind()
    End Sub

    Private Sub load_remot()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS rr, tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
"FROM         tblBuilding INNER JOIN " & _
 "                     tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
  "                    tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
"GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
 "       HAVING(tblWorkHdr.WorkTypeCode = 2)"
        Dim dtR As New DataTable
        dtR = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(1).DataSource = dtR
        WebChartControl1.DataBind()
    End Sub

    Private Sub load_mahal()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS mahal, tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
"FROM         tblBuilding INNER JOIN " & _
 "                     tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
  "                    tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
"GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
 "       HAVING(tblWorkHdr.WorkTypeCode = 3)"
        Dim dtm As New DataTable
        dtm = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(2).DataSource = dtm
        WebChartControl1.DataBind()
    End Sub

    Private Sub load_dorei()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS dorei, tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
"FROM         tblBuilding INNER JOIN " & _
 "                     tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
  "                    tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
"GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
 "       HAVING(tblWorkHdr.WorkTypeCode = 4)"
        Dim dtdo As New DataTable
        dtdo = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(3).DataSource = dtdo
        WebChartControl1.DataBind()

    End Sub

    Private Sub load_shabake()
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblWorkHdr.WorkTypeCode) AS tshbk, tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
"FROM         tblBuilding INNER JOIN " & _
 "                     tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
  "                    tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode " & _
"GROUP BY tblWorkHdr.WorkTypeCode, tblBuilding.BuildingName " & _
 "       HAVING(tblWorkHdr.WorkTypeCode = 5)"
        Dim dtshbk As New DataTable
        dtshbk = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(4).DataSource = dtshbk
        WebChartControl1.DataBind()

    End Sub
    
    Private Sub load_tedadsystem()
        Dim sqlstr As String
        sqlstr = "SELECT     tblBuilding.BuildingName AS saxteman, COUNT(tblBuilding.BuildingCode) AS tedadsystem " & _
                  " FROM         tblBuilding INNER JOIN " & _
                    "  tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode " & _
        " WHERE(tblSystem.CodeClosed = 0) " & _
" GROUP BY tblBuilding.BuildingName"
        Dim dtkol As New DataTable
        dtkol = ClsDataTable.GetDataTable(sqlstr)

        'WebChartControl1.DataSource = dt
        'WebChartControl1.DataSource = dtR

        WebChartControl1.Series.Item(5).DataSource = dtkol
        WebChartControl1.DataBind()



    End Sub
End Class
