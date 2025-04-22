Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

       load_checkbox2()
        If Me.IsPostBack = False Then

            load_users_reciver()
            load_checkbox()
        End If


    End Sub

    Private Sub load_checkbox2()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT        tblForm.FrmID, tblForm.FrmName, tblForm.FrmCaption " & _
           " FROM            tblForm INNER JOIN   tblFormAcc ON tblForm.FrmID = tblFormAcc.FrmID  " & _
                  " WHERE  (tblForm.Frm_level > 1) "
        dt = ClsDataTable.GetDataTable(sqlstr)



        ListBox3.DataSource = dt
        ListBox3.DataTextField = "FrmCaption"
        ListBox3.DataValueField = "FrmID"
        ListBox3.DataBind()

    End Sub


    Private Sub load_checkbox()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT        tblForm.FrmID, tblForm.FrmName, tblForm.FrmCaption " & _
           " FROM            tblForm INNER JOIN   tblFormAcc ON tblForm.FrmID = tblFormAcc.FrmID  " & _
                  " WHERE  (tblForm.Frm_level > 1) AND (tblFormAcc.UserCode = " & cmbreciver.SelectedValue & ") "
        dt = ClsDataTable.GetDataTable(sqlstr)
      


        ListBox1.DataSource = dt
        ListBox1.DataTextField = "FrmCaption"
        ListBox1.DataValueField = "FrmID"
        ListBox1.DataBind()

    End Sub

  
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    Private Sub load_users_reciver()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = " select UserCode,UserLastName +' '+ UserFamili as nam from tblUsers where SysUser=1 and codeclosed=0 order by UserFamili "
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbreciver.DataSource = dt
        cmbreciver.DataValueField = "UserCode"
        cmbreciver.DataTextField = "nam"
        cmbreciver.DataBind()

    End Sub

    Protected Sub cmbreciver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbreciver.SelectedIndexChanged
        load_checkbox()
    End Sub

   
End Class
