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
    'Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared

    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("Nationality"))
    '    'warning_pic = "images/Nationality/" & value & ".jpg"
    '    'warning_pic = "images/Icons/loading_transparent.gif"



    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If


    'End Sub
    Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo2" Then
            Dim iamgegrdst As Image = TryCast(ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image52"), Image)
            iamgegrdst.ImageUrl = warning_pic

        End If
    End Sub

    Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        'Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        'warning_pic = "images/person_pic/" & value & ".jpg"




        'If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
        '    Return
        'End If
        Dim value2 As Decimal = CDec(e.GetValue("State"))
        If value2 = 0 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightSkyBlue


        ElseIf value2 = 19 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightGreen
        ElseIf value2 = 199 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightPink
        End If

    End Sub


    'Protected Sub ASPxGridView1_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand
    '    If e.CommandArgs.CommandName = "moshahede" Then
    '        'Session("fromTyp") = "admn"
    '        Session("idOver") = e.CommandArgs.CommandArgument
    '        Response.Redirect("overhaulDetail.aspx")
    '    End If
    'End Sub

    Protected Sub ASPxGridView1_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("idOver") = e.CommandArgs.CommandArgument

            Dim url As String = "overhaulDetail.aspx"
            Dim script As String = "window.open('" & url & "', '_blank');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openNewTab", script, True)
        End If
    End Sub



    Protected Sub ASPxGridView15_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView15.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 14
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView14_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView14.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 13
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView11_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView11.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 7
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If



    End Sub
    Protected Sub ASPxGridView12_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView12.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 9
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView13_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView13.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 6
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView2_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView2.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 1
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If



    End Sub
    Protected Sub ASPxGridView3_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView3.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 2
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub ASPxGridView4_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView4.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 3
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub ASPxGridView5_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView5.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 4
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView6_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView6.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 5
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView7_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView7.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 8
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView8_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView8.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 10
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView9_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView9.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 12
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub
    Protected Sub ASPxGridView10_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView10.RowCommand
        If e.CommandArgs.CommandName = "moshahede" Then
            Session("tab") = 11
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
    Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
        load_grid()

    End Sub
    Private Sub load_grid()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW ORDER BY FirstName"

        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dt
        ASPxGridView1.DataBind()

        ' CntTyp()


    End Sub
    Private Sub load_gridPeymankar()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 100 ORDER BY FirstName"
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView11.DataSource = dt
        ASPxGridView11.DataBind()

        ' CntTyp()


    End Sub
    Private Sub load_gridBarnameEtmam()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 103 ORDER BY FirstName"
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView14.DataSource = dt
        ASPxGridView14.DataBind()

        ' CntTyp()


    End Sub
    Private Sub load_gridPshtibaniEtmam()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulHerastVW  ORDER BY codePersoneli"
        'Else
        '    sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView15.DataSource = dt
        ASPxGridView15.DataBind()

        ' CntTyp()


    End Sub
    Private Sub load_gridImeni()

        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 15 ORDER BY FirstName"
      
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView12.DataSource = dt
        ASPxGridView12.DataBind()
        ' CntTyp()
    End Sub
    Private Sub load_gridUnitsNotOk()

        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state < 0 ORDER BY FirstName"

        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView13.DataSource = dt
        ASPxGridView13.DataBind()
        ' CntTyp()
    End Sub

    Private Sub load_gridPaziresh()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        sqlstr = "SELECT * from overhaulAllVW where state = 19 ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = 102 ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = -100  ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = -19 ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = 199 ORDER BY tarixMosahebe Desc"
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
        sqlstr = "SELECT * from overhaulAllVW where state = 200 ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = 6 ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = 33 ORDER BY FirstName"
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
        sqlstr = "SELECT * from overhaulAllVW where state = 101 ORDER BY FirstName"
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
                'ASPxPageControl1.TabPages(0).Visible = False

            End If
            'If Session("tab") = 5 Then
            '    ASPxPageControl1.ActiveTabIndex = 5
            'End If
            'If Session("tab") = 1 Then
            '    ASPxPageControl1.ActiveTabIndex = 1
            'End If



            Select Case Session("tab")
                Case 0
                    ASPxPageControl1.ActiveTabIndex = 0
                Case 1
                    ASPxPageControl1.ActiveTabIndex = 1
                Case 2
                    ASPxPageControl1.ActiveTabIndex = 2
                Case 3
                    ASPxPageControl1.ActiveTabIndex = 3
                Case 4
                    ASPxPageControl1.ActiveTabIndex = 4
                Case 5
                    ASPxPageControl1.ActiveTabIndex = 5
                Case 6
                    ASPxPageControl1.ActiveTabIndex = 6
                Case 7
                    ASPxPageControl1.ActiveTabIndex = 7
                Case 8
                    ASPxPageControl1.ActiveTabIndex = 8
                Case 9
                    ASPxPageControl1.ActiveTabIndex = 9
                Case 10
                    ASPxPageControl1.ActiveTabIndex = 10
                Case 11
                    ASPxPageControl1.ActiveTabIndex = 11
                Case 12
                    ASPxPageControl1.ActiveTabIndex = 12
                Case 13
                    ASPxPageControl1.ActiveTabIndex = 13
                Case 14
                    ASPxPageControl1.ActiveTabIndex = 14

            End Select


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
            load_gridPeymankar()
            load_gridImeni()
            load_gridUnitsNotOk()
            load_gridBarnameEtmam()
            load_gridPshtibaniEtmam()

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
        load_gridPeymankar()
        load_gridImeni()
        load_gridUnitsNotOk()
        load_gridBarnameEtmam()
        load_gridPshtibaniEtmam()
    End Sub



    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub




 
  



   
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If Session("omurID") <> 100 Then

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
                Case 8
                    grvExporter1.GridViewID = ASPxGridView7.UniqueID
                    grvExporter1.FileName = "ارسال به HSE"
                    grvExporter1.WriteXlsToResponse()
                Case 7
                    grvExporter1.GridViewID = ASPxGridView11.UniqueID
                    grvExporter1.FileName = "پیمانکار"
                    grvExporter1.WriteXlsToResponse()
                Case 12
                    grvExporter1.GridViewID = ASPxGridView9.UniqueID
                    grvExporter1.FileName = "حراست صدور کارت"
                    grvExporter1.WriteXlsToResponse()
                Case 11
                    grvExporter1.GridViewID = ASPxGridView10.UniqueID
                    grvExporter1.FileName = "حضورغیاب"
                    grvExporter1.WriteXlsToResponse()
                Case 10
                    grvExporter1.GridViewID = ASPxGridView8.UniqueID
                    grvExporter1.FileName = "حراست"
                    grvExporter1.WriteXlsToResponse()
                Case 9
                    grvExporter1.GridViewID = ASPxGridView12.UniqueID
                    grvExporter1.FileName = "ارسال به ایمنی"
                    grvExporter1.WriteXlsToResponse()
                Case 6
                    grvExporter1.GridViewID = ASPxGridView13.UniqueID
                    grvExporter1.FileName = "عدم تایید واحدها"
                    grvExporter1.WriteXlsToResponse()
                Case 13
                    grvExporter1.GridViewID = ASPxGridView14.UniqueID
                    grvExporter1.FileName = "برنامه ریزی - شروع بکار"
                    grvExporter1.WriteXlsToResponse()
                Case 14
                    grvExporter1.GridViewID = ASPxGridView15.UniqueID
                    grvExporter1.FileName = "پشتیبانی رفاهی"
                    grvExporter1.WriteXlsToResponse()
            End Select
        End If
    End Sub
End Class
