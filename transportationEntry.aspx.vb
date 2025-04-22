Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.XtraCharts

Partial Class transportationEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

            'load_gridAll()
        Else


        End If
        load_grid()
        BindQuata()
    End Sub

    Private Sub BindQuata()

        Dim dt3 As New DataTable
   

        Dim sqlstr As String = "select  dbo.ServiceRegNotEntryCount() as tedadNo,dbo.ServiceRegEntryCount() as tedad,dbo.GetPersonelCount() as perCount"

        dt3 = ClsDataTable.GetDataTable(sqlstr)

        'Chart1.DataSource = dt3
        'Chart1.Series("Series1").XValueMember = "perCount"
        'Chart1.Series("Series1").YValueMembers = "tedad"

        ''Chart1.Series("Series1").Palette.p()
        ''Chart1.Series("Series2").XValueMember = "tedad"
        ''Chart1.Series("Series2").YValueMembers = "tedad"
        ''Chart1.Series("Series3").XValueMember = "tedadNo"
        ''Chart1.Series("Series3").YValueMembers = "tedadNo"
        'Chart1.DataBind()
        Dim kol As Integer = dt3.Rows(0).Item("perCount").ToString
        Dim regcount As Integer = dt3.Rows(0).Item("tedad").ToString
        Dim regNocount As Integer = dt3.Rows(0).Item("tedadNo").ToString

        Dim dt As New DataTable
        dt.Columns.Add("Status")
        dt.Columns.Add("Total")

        'dt.Rows.Add("Success", kol)
        dt.Rows.Add("ثبت نام کرده", regcount)
        dt.Rows.Add("ثبت نام نگرده", regNocount)

        Chart1.DataSource = dt
        Chart1.Series("Series1").XValueMember = "Status"
        Chart1.Series("Series1").YValueMembers = "Total"
        Chart1.DataBind()

    End Sub

 


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("transportationEntry.aspx", Val(Session("UserId")))
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


    Private Sub load_grid()


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from ServiceEntryVW "
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
        lblCnt.Text = dt.Rows.Count

        'Chart1.DataSource = dt

        'Chart1.Series("Series1").XValueMember = "codePersoneli"
        'Chart1.Series("Series1").YValueMembers = dt.Rows.Count
        'Chart1.Series("Series2").YValueMember = "bagi"
        'Chart1.DataBind()

    End Sub








    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub







End Class
