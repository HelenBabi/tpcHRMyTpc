Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class telPersonalsFrm
    Inherits System.Web.UI.Page
    Dim warning_pic As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        meno_acc()
        'End If
        If Me.IsPostBack = False Then
            '  Session("UserId") = 1
            'load_cmbPersonel()
            'load_Units()
            'load_nostexdam()
            'load_ellatfalli()

        End If
        load_grid()

    End Sub

  
    '    Private Sub load_cmbPersonel()
    '        Dim dtmm As New DataTable

    '        Dim sqlstr As String
    '        sqlstr = "SELECT        TOP (5000) personel.ID, " & _
    '                        " personel.nam + ' ' + personel.family + ' ' + personel.postname + ' ' + personel.codeMelli + ' ' + + ' ' + personel.teldaxeli + ' ' + personel.telmanzel + ' ' + personel.mobile " & _
    '                         " AS pers " & _
    '"FROM            personel INNER JOIN " & _
    '                         "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
    '                        " omurTbl ON personel.omurID = omurTbl.omurID " & _
    '"WHERE        (personel.ID <> 0) AND (personel.CodeClosed = 0)"
    '        dtmm = ClsDataTable.GetDataTable(sqlstr)
    '        CmbPersonel.DataSource = dtmm
    '        CmbPersonel.TextField = "pers"
    '        CmbPersonel.ValueField = "Id"
    '        CmbPersonel.DataBind()

    '    End Sub


    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("telPersonalsFrm.aspx", Val(Session("UserId")))
        'ibtnAdd.Visible = meno_arr(1)
        'ibtnEdit.Visible = meno_arr(2)
        'ibtnSave.Visible = meno_arr(3)
        'ibtnSaveNew.Visible = meno_arr(4)
        'ibtnCancel.Visible = meno_arr(5)
        'ibtnDelete.Visible = meno_arr(6)
        ''ibtnExitttt.Visible = meno_arr(7)
        'ibtnHistory.Visible = meno_arr(8)
        'ibtnDeactived.Visible = meno_arr(9)
        'ibtnPrint.Visible = meno_arr(10)
        'ibtnExitttt.Visible = meno_arr(11)
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



    Private Sub load_grid()
        Dim ClsB_ As New ClsPersonal
        Dim dt As New DataTable
        dt = ClsB_.GetAllBaseDomainUserVW(0)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
    End Sub



    Protected Sub ASPxGrvHead_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo" Then
            Dim iamgegrdst As Image = TryCast(ASPxGrvHead.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image5"), Image)
            iamgegrdst.ImageUrl = warning_pic
            '  textBox.Text = Convert.ToString(e.CellValue)
        End If
    End Sub
    Protected Sub ASPxGrvHead_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        warning_pic = "images/person_pic/" & value & ".jpg"
        'warning_pic = "images/Icons/loading_transparent.gif"




    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("index.aspx")
    End Sub



    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        BindGridviewData()
        subject.Focus()

    End Sub
    Private Sub BindGridviewData()

        Dim dt3 As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      personel.postname ,personel.CodeClosed, personel.nam, personel.family, personel.codePersoneli, personel.codeMelli, personel.teldaxeli, personel.telmanzel, personel.mobile,  " & _
        "tblUnits.UnitsName, omurTbl.omur " & _
   "FROM            personel INNER JOIN " & _
                        " tblUnits ON personel.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                        " omurTbl ON personel.omurID = omurTbl.omurID " & _
"WHERE        (personel.CodeClosed = 0) and        (personel.nam LIKE N'%" & subject.Text & "%') OR " & _
                         "(personel.family LIKE N'%" & subject.Text & "%') OR " & _
                        " (personel.teldaxeli LIKE N'%" & subject.Text & "%') OR " & _
                        " (personel.telmanzel LIKE N'%" & subject.Text & "%') OR " & _
                        " (personel.mobile LIKE N'%" & subject.Text & "%') OR " & _
                        " (tblUnits.UnitsName LIKE N'%" & subject.Text & "%') OR " & _
                                " (omurTbl.omur LIKE N'%" & subject.Text & "%') Or " & _
                        " (personel.codePersoneli LIKE '%" & subject.Text & "%') or " & _
         " (personel.postname LIKE '%" & subject.Text & "%')"

        dt3 = ClsDataTable.GetDataTable(sqlstr)
        If dt3.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dt3
            ASPxGrvHead.DataBind()

        End If


    End Sub
    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        BindGridviewData()
    End Sub
End Class
