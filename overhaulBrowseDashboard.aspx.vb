Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Partial Class overhaulBrowseDashboard

    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()
            load_gridAll()
            load_gridAmarChart()
            load_gridAmarChartPersonel()
            load_gridAmarChartStat()
        End If
        load_gridAll()
        load_gridAmarChart()
        load_gridAmarChartPersonel()
        load_gridAmarChartStat()
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("overhaulBrowseDashboard.aspx", Val(Session("UserId")))
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
    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulDashboardVW  ORDER BY tarix DESC"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        GridWorkTable.DataSource = dt
        GridWorkTable.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub
    Private Sub load_gridAmarChart()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulDahsboardChartVW   ORDER BY tedad DESC"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dt
        ASPxGridView1.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub
    Private Sub load_gridAmarChartPersonel()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulPersonelChartVW   "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView2.DataSource = dt
        ASPxGridView2.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub
    Private Sub load_gridAmarChartStat()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT        TOP (200) COUNT(overhaulStatusTbl.Id) AS tedad, overhaulStatusTbl.Title, overhaulStatusTbl.Id " & _
                 " FROM            overhaulStatusTbl INNER JOIN                          overhaulDB1.dbo.RegisterOverhauls ON overhaulStatusTbl.Id = overhaulDB1.dbo.RegisterOverhauls.State" & _
                 " GROUP BY overhaulStatusTbl.Title, overhaulStatusTbl.Id ORDER BY tedad DESC"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView3.DataSource = dt
        ASPxGridView3.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        Select Case ASPxPageControl1.ActiveTabIndex
            Case 0
                grvExporter1.GridViewID = GridWorkTable.UniqueID
                grvExporter1.FileName = "آمار روزانه"
                grvExporter1.WriteXlsToResponse()
            Case 1
                grvExporter1.GridViewID = ASPxGridView1.UniqueID
                grvExporter1.FileName = "آمار چارت"
                grvExporter1.WriteXlsToResponse()
            Case 2
                grvExporter1.GridViewID = ASPxGridView2.UniqueID
                grvExporter1.FileName = "آمار نفرات"
                grvExporter1.WriteXlsToResponse()
            Case 3
                grvExporter1.GridViewID = ASPxGridView3.UniqueID
                grvExporter1.FileName = "آمار وضعیت"
                grvExporter1.WriteXlsToResponse()
        End Select

    End Sub

  


End Class
