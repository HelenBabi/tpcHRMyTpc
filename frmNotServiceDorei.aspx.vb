Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class frmNotServiceDorei
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then

        End If

        load_grid()
    End Sub

    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("SystemCode").ToString

        Dim ClsB_ As New ClsBrowsSystems
        Dim dt2 As New DataTable
        dt2 = ClsB_.GetAllBrowsSystems(idd)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub


    Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("SystemHDtlStatus"))
        If value = 0 Then
            e.Row.ForeColor = Drawing.Color.Green
            '   e.Row.BackColor = Drawing.Color.Aquamarine
        Else
            e.Row.ForeColor = Drawing.Color.Red
            '   e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
        End If
    End Sub

    'Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim idd As Integer
    '    Dim grid As ASPxGridView = CType(sender, ASPxGridView)
    '    idd = grid.GetMasterRowFieldValues("WorkId").ToString

    '    Dim sqlstr As String

    '    Dim dt As New DataTable
    '    sqlstr = ("seect * from tblWorkDtl where WorkId= " & idd & "")
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    grid.DataSource = dt
    '    grid.DataBind()

    'End Sub







    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("WorkTypeCode"))
    '    If value = 1 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LightGreen
    '    ElseIf value = 2 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
    '    ElseIf value = 3 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.Aquamarine

    '    ElseIf value = 4 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.BurlyWood
    '    End If
    'End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmNotServiceDorei.aspx", Val(Session("UserId")))

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
        Dim dtSistem As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     tblSystem.SystemCode, tblHolder.HolderName + ' ' + tblHolder.HolderFamili AS karbar, tblBuilding.BuildingName, tblUnits.UnitsName, " & _
        " tblSystem.SystemRoom, tblSystem.SystemPhone  " & _
                     " FROM      tblSystem INNER JOIN  " & _
                      " tblBuilding ON tblSystem.BuildingCode = tblBuilding.BuildingCode INNER JOIN  " & _
                      " tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN  " & _
                      " tblHolder ON tblSystem.HolderCode = tblHolder.HolderCode  " & _
               " WHERE     (tblSystem.SystemCode NOT IN  " & _
                         " (SELECT     SystemCode  " & _
        " FROM tblWorkHdr       WHERE     (WorkTypeCode = 4))) "


        dtSistem = ClsDataTable.GetDataTable(sqlstr)
        grdNotDprei.DataSource = dtSistem
        grdNotDprei.DataBind()
        lblCnt.Text = dtSistem.Rows.Count
    End Sub



    'Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
    '    If e.CommandArgs.CommandName = "select" Then
    '        Dim ClsB As New ClsBrowsWorks
    '        Dim dt As New DataTable
    '        dt = ClsB.GetBrowsWorks(e.CommandArgs.CommandArgument.ToString(), hdn_deactivedcode.Value)
    '        txt_az.Text = dt.Rows(0).Item("BrowsWorksCode").ToString
    '        txt_ta.Text = dt.Rows(0).Item("BrowsWorksName").ToString
    '        ChbActiveCode.Checked = dt.Rows(0).Item("CodeClosed").ToString
    '    End If
    'End Sub




    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
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
End Class
