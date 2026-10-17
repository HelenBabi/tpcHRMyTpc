Imports System.Data
Imports System.Data.SqlClient


Partial Class resturanMonitoringFrm
    Inherits System.Web.UI.Page
 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        meno_acc()
  
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        resturan1()
        resturan12()
        resturan2()
        resturan22()
    End Sub
#Region "All pages"
    Private Sub resturan1()
        Dim sqlstr As String = ""
        Dim dtb As New DataTable
        sqlstr = "SELECT TOP (10) CardId, Date, DeviceCode, person, Id " & _
        " FROM resturanVW         WHERE(DeviceCode = 1) ORDER BY Id DESC"
        dtb = ClsDataTable.GetDataTableBoad5(sqlstr)
        If dtb.Rows.Count > 0 Then
            Label1.Visible = True
            DataList1.DataSource = dtb
            DataList1.DataBind()
        Else
            Label1.Visible = False
        End If
    End Sub
    Private Sub resturan12()
       Dim sqlstr As String = ""
        Dim dt3 As New DataTable

        sqlstr = "SELECT TOP (10) CardId, Date, DeviceCode, person, Id " & _
          " FROM resturanVW         WHERE(DeviceCode = 2) ORDER BY Id DESC"
        dt3 = ClsDataTable.GetDataTableBoad5(sqlstr)
        If dt3.Rows.Count > 0 Then
            Label2.Visible = True
            DataList2.DataSource = dt3
            DataList2.DataBind()
        Else
            Label2.Visible = False
        End If
    End Sub
    Private Sub resturan2()
        Dim sqlstr As String = ""
        Dim dt3 As New DataTable

        sqlstr = "SELECT TOP (10) CardId, Date, DeviceCode, person, Id " & _
          " FROM resturanVW         WHERE(DeviceCode = 3) ORDER BY Id DESC"
        dt3 = ClsDataTable.GetDataTableBoad5(sqlstr)
        If dt3.Rows.Count > 0 Then
            Label3.Visible = True
            DataList3.DataSource = dt3
            DataList3.DataBind()
        Else
            Label3.Visible = False
        End If
    End Sub
    Private Sub resturan22()
      Dim sqlstr As String = ""
        Dim dt3 As New DataTable

        sqlstr = "SELECT TOP (10) CardId, Date, DeviceCode, person, Id " & _
          " FROM resturanVW         WHERE(DeviceCode = 4) ORDER BY Id DESC"
        dt3 = ClsDataTable.GetDataTableBoad5(sqlstr)
        If dt3.Rows.Count > 0 Then
            Label4.Visible = True
            DataList4.DataSource = dt3
            DataList4.DataBind()
        Else
            Label4.Visible = False
        End If
    End Sub
    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("resturanMonitoringFrm.aspx", Val(Session("UserId")))
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
  
    


    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub


End Class
