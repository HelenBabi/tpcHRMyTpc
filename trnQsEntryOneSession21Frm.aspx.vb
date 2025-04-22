Imports System.Data
Imports System.Data.SqlClient
Partial Class trnQsEntryOneSession21Frm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            'CmbUnit.Enabled = False



            load_cmbDore()


            'load_cmbQsType()
            'load_grid_Sabage()
            'load_items()
            loadDataList()

            Session("doreID") = CmbUnit.SelectedValue


        End If

    End Sub
    Private Sub loadDataList()
        Dim sqlstr As String
        'Request.QueryString("id")
        sqlstr = "SELECT       *  from trnQsTypeVW where doreID = " & Session("doreID") & ""

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt
        DataList1.DataBind()
    End Sub
    

    Private Sub load_cmbDore()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        *        FROM trnQsCmbDoreVW WHERE        (codePersonel = " & Session("codePersoneli") & ")"
        'WHERE        (hozuri = 0) AND (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbUnit.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            CmbUnit.DataTextField = "onvan"
            CmbUnit.DataValueField = "idd"
            CmbUnit.DataBind()

        End If


    End Sub








    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("trnQsEntryOneSessionFrm.aspx")
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











  
End Class
