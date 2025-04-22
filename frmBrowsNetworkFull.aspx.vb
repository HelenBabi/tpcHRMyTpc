Imports System.Data
Imports System.Data.SqlClient

Partial Class frmBrowsNetworkFull
    Inherits System.Web.UI.Page
    Dim dtaspx As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1

            ' meno_acc()
        End If
        'load_grid_hardware()

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrowsNetworkFull.aspx", Val(Session("UserId")))

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







    Private Sub find_pc()

        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(Val(txt_SystemCode.Text))

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("rack").ToString = False Then
                msg_box("اين شماره اموال براي " & " كامپيوتر  " & "ثبت شده است")
                Exit Sub
            End If
            txt_bulding.Text = dt.Rows(0).Item("BuildingName").ToString
            
            txt_ComputerName.Text = dt.Rows(0).Item("ComputerName").ToString
            txtNote.Text = dt.Rows(0).Item("tozihat").ToString

            service_dorei()
            ' txt_vaziat.Text = dt.Rows(0).Item("CodeClosed").ToString
            Button1.Enabled = True
            load_grid_hardware()
        Else
            msg_box(" اين شماره اموال موجود نيست")
            txt_bulding.Text = ""
          
            txt_ComputerName.Text = ""
            txt_dateServiceDorei.Text = ""
            txt_vaziat.Text = ""
            ASPxGrvHead.DataBind()

            Button1.Enabled = False
            Exit Sub
        End If
        If dt.Rows(0).Item("CodeClosed").ToString = True Then
            msg_box("اين رك رده خارج شده است")
            txt_vaziat.Text = "رده خارج"
        Else
            txt_vaziat.Text = "فعال"
        End If

    End Sub
    Private Sub service_dorei()
        Dim sqlstr As String

        sqlstr = "SELECT Max(tblWorkHdr.WorkDateS) AS Expr2    FROM tblWorkHdr " & _
            " WHERE(tblWorkHdr.SystemCode = " & Val(txt_SystemCode.Text) & ") and tblWorkHdr.WorkTypeCode=4 "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            txt_dateServiceDorei.Text = dt.Rows(0).Item("Expr2").ToString

        End If

    End Sub
   

    Private Sub load_grid_hardware()
        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dtaspx = ClsB_.GetAllBrowsSystems(Val(txt_SystemCode.Text))
        If dtaspx.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dtaspx
            ASPxGrvHead.DataBind()
        Else
            '   msg_box("اين شماره اموال داراي قطعه نمي باشد")
        End If

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        clear_form()
       
        find_pc()
        'If ASPxGrvHead.
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

    Protected Sub ASPxGrvHead_BeforePerformDataSelect(sender As Object, e As System.EventArgs) Handles ASPxGrvHead.BeforePerformDataSelect

    End Sub

    Private Sub clear_form()
        txt_bulding.Text = ""
        txt_ComputerName.Text = ""
        txt_dateServiceDorei.Text = ""
      
        txt_vaziat.Text = ""
        txtNote.Text = ""
       
        ASPxGrvHead.DataBind()

    End Sub


    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
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

    Protected Sub ibtnPrint_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnPrint.Click
        Session("RpName") = "report_browsSysFull"
        Dim furl As String = "frmPgReport.aspx?PwhereSTR=" + " WHERE tblSystem.SystemCode= " & txt_SystemCode.Text & "&PwhereSTR2=" & "and tblSystemHDtl.SystemCode=" & txt_SystemCode.Text
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=700,width=850,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    


    Protected Sub ibtnHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnHistory.Click
        'Session("RpName") = "report_browsSysFull"
        Dim furl As String = "frmSabeghe.aspx?PwhereSTR=" & Val(txt_SystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=400,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim furl As String = "frmSabeghe.aspx?PwhereSTR=" & Val(txt_SystemCode.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=400,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub
End Class
