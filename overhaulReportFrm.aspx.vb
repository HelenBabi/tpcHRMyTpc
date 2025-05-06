Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView


Partial Class overhaulReportFrm
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("overhaulReportFrm.aspx", Val(Session("UserId")))
        'ibtnAdd.Visible = meno_arr(1)
        'ibtnEdit.Visible = meno_arr(2)
        'ibtnSave.Visible = meno_arr(3)
        'ibtnSaveNew.Visible = meno_arr(4)
        'ibtnCancel.Visible = meno_arr(5)
        'ibtnDelete.Visible = meno_arr(6)
        'ibtnDeleted.Visible = meno_arr(7)
        'ibtnHistory.Visible = meno_arr(8)
        'ibtnDeactived.Visible = meno_arr(9)
        'ibtnPrint.Visible = meno_arr(10)
        'ibtnExit.Visible = meno_arr(11)
    End Sub

    Protected Sub ASPxGridView2_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo1" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView2.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image51"), Image)
            iamgegrdst.ImageUrl = warning_pic

        End If
    End Sub
    Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("Nationality"))
        'warning_pic = "images/Nationality/" & value & ".jpg"
        'warning_pic = "images/Icons/loading_transparent.gif"



        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        

    End Sub
    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo2" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image52"), Image)
            iamgegrdst.ImageUrl = warning_pic

        End If
    End Sub

    Protected Sub gridPaziresh_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView2.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        'Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        'warning_pic = "images/person_pic/" & value & ".jpg"




        'If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
        '    Return
        'End If
        'Dim value2 As Decimal = CDec(e.GetValue("teacherScore"))
        'If value2 = 0 Then
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = Drawing.Color.Orange


        'Else
        '    e.Row.ForeColor = Drawing.Color.Black
        '    e.Row.BackColor = Drawing.Color.LimeGreen

        'End If

    End Sub


    Protected Sub ASPxGridView1_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If



    End Sub

    Protected Sub ASPxGridView2_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView2.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If



    End Sub
    Protected Sub ASPxGridView3_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView3.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub ASPxGridView4_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView4.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub ASPxGridView5_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView5.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView6_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView6.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView7_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView7.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView8_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView8.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView9_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView9.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView10_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView10.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
            msg_box("ثبت شد")

        Catch ex As Exception
            msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub
    Private Sub load_grid()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW"
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dt
        ASPxGridView1.DataBind()

        ' CntTyp()


    End Sub
    Private Sub load_gridPaziresh()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 13 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView2.DataSource = dt
            ASPxGridView2.DataBind()

        End If



    End Sub
    Private Sub load_gridEtmam()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 1 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView9.DataSource = dt
            ASPxGridView9.DataBind()

        End If



    End Sub
    Private Sub load_gridPazireshNO()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = -13 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView3.DataSource = dt
            ASPxGridView3.DataBind()

        End If
    End Sub
    Private Sub load_gridPazireshVahedha()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = -19 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView4.DataSource = dt
            ASPxGridView4.DataBind()

        End If
    End Sub
    Private Sub load_gridPazireshVahedhaNo()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 199 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView5.DataSource = dt
            ASPxGridView5.DataBind()

        End If
    End Sub
    Private Sub load_gridNoBarnamerizi()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 200 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView6.DataSource = dt
            ASPxGridView6.DataBind()

        End If
    End Sub

    Private Sub load_gridBarnameriziToUnits()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 19 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView7.DataSource = dt
            ASPxGridView7.DataBind()

        End If
    End Sub
    Private Sub load_gridHerast()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 6 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView8.DataSource = dt
            ASPxGridView8.DataBind()

        End If
    End Sub
    Private Sub load_gridHerastToBarnamerizi()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 33 "
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " where state = 1"
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            ASPxGridView10.DataSource = dt
            ASPxGridView10.DataBind()

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        meno_acc()
        

       


        If Me.IsPostBack = False Then
            If Session("omurID") = 19 Then
                ASPxPageControl1.ActiveTabIndex = 1
            End If

            load_grid()
            load_gridPaziresh()
            load_gridPazireshNO()
            load_gridPazireshVahedha()
            load_gridPazireshVahedhaNo()
            load_gridNoBarnamerizi()
            load_gridBarnameriziToUnits()
            load_gridHerast()
            load_gridEtmam()
            load_gridHerastToBarnamerizi()
        End If
        'If Me.IsCallback Then
        '    loadgridall()
        'End If
        load_gridPaziresh()
        load_grid()
        load_gridPazireshNO()
        load_gridPazireshVahedha()
        load_gridPazireshVahedhaNo()
        load_gridNoBarnamerizi()
        load_gridBarnameriziToUnits()
        load_gridHerast()
        load_gridEtmam()
        load_gridHerastToBarnamerizi()
    End Sub



    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub




    Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
        load_grid()

    End Sub
    Protected Sub gridPaziresh_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView2.AfterPerformCallback
        load_gridPaziresh()

    End Sub



   
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
      
        Select Case ASPxPageControl1.ActiveTabIndex
            Case 0
                grvExporter1.GridViewID = ASPxGridView1.UniqueID
                grvExporter1.FileName = "لیست کلی"
                grvExporter1.WriteXlsToResponse()
            Case 1
                grvExporter1.GridViewID = ASPxGridView2.UniqueID
                grvExporter1.FileName = "تایید پیمانکار"
                grvExporter1.WriteXlsToResponse()
            Case 2
                grvExporter1.GridViewID = ASPxGridView3.UniqueID
                grvExporter1.FileName = "عدم تایید پیمانکار"
                grvExporter1.WriteXlsToResponse()
            Case 3
                grvExporter1.GridViewID = ASPxGridView4.UniqueID
                grvExporter1.FileName = "عدم تایید برنامه ریزی"
                grvExporter1.WriteXlsToResponse()
            Case 4
                grvExporter1.GridViewID = ASPxGridView5.UniqueID
                grvExporter1.FileName = "ارجا به واحدها توسط برنامه ریزی"
                grvExporter1.WriteXlsToResponse()
            Case 5
                grvExporter1.GridViewID = ASPxGridView6.UniqueID
                grvExporter1.FileName = "ارجاع به برنامه ریزی توسط واحدها"
                grvExporter1.WriteXlsToResponse()
            Case 6
                grvExporter1.GridViewID = ASPxGridView7.UniqueID
                grvExporter1.FileName = "ارسال به HSE"
                grvExporter1.WriteXlsToResponse()
            Case 7
                grvExporter1.GridViewID = ASPxGridView8.UniqueID
                grvExporter1.FileName = "ارسال به حراست"
                grvExporter1.WriteXlsToResponse()
            Case 9
                grvExporter1.GridViewID = ASPxGridView9.UniqueID
                grvExporter1.FileName = "لیست اتمام"
                grvExporter1.WriteXlsToResponse()
            Case 8
                grvExporter1.GridViewID = ASPxGridView10.UniqueID
                grvExporter1.FileName = "ارجاع به برنامه ریزی توسط حراست"
                grvExporter1.WriteXlsToResponse()
        End Select
    End Sub
End Class
