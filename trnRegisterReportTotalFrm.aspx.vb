Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView


Partial Class trnRegisterReportTotalFrm
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Dim dt As New DataTable
    Dim warning_pic As String = ""

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnRegisterReportTotalFrm.aspx", Val(Session("UserId")))
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

    Protected Sub gridPaziresh_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)

        If e.DataColumn.Name = "photo1" Then
            Dim iamgegrdst As Image = TryCast(gridPaziresh.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "Image51"), Image)
            iamgegrdst.ImageUrl = warning_pic

        End If
    End Sub
    Protected Sub ASPxGridView1_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        warning_pic = "images/person_pic/" & value & ".jpg"
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

    Protected Sub gridPaziresh_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles gridPaziresh.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("codePersoneli"))
        warning_pic = "images/person_pic/" & value & ".jpg"
        'warning_pic = "images/Icons/loading_transparent.gif"



        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value2 As Decimal = CDec(e.GetValue("teacherScore"))
        If value2 = 0 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.Orange


        Else
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LimeGreen

        End If

    End Sub


    Protected Sub ASPxGridView1_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand


        'Dim sqlstr As String

        If e.CommandArgs.CommandName = "SabtNomre" Then
            'sqlstr = "UPDATE     trnDoreReqisterTbl SET              hazf = 0,  stat = 17,notee= N'' where idd= " & e.CommandArgs.CommandArgument.ToString() & " "
            'upStat(sqlstr)
            'load_grid()

            'UpdatePanel1.Update()
            Session("Classidd") = e.CommandArgs.CommandArgument.ToString()
            Response.Redirect("trnSessionScoreFrm.aspx")

        End If



    End Sub

    Protected Sub gridPaziresh_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles gridPaziresh.RowCommand
        Dim sqlstr As String
        If e.CommandArgs.CommandName = "aa" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                teacherScore = 1,notee= N'' where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()

            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "bb" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                teacherScore = 2,notee= N'' where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "cc" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                teacherScore = 3,notee= N''  where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "PI1" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                teacherScore = 4,notee= N''  where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()
        End If
        If e.CommandArgs.CommandName = "UPI1" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                teacherScore = 5,notee= N''  where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()
        End If

        If e.CommandArgs.CommandName = "ADV" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                teacherScore = 6,notee= N''  where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()
        End If

        If e.CommandArgs.CommandName = "ebtal" Then
            sqlstr = "	Update trnDoreReqisterTbl  set ebtalDateTime= '" & ClsDateTime.GetDateNow() & "' , ebtalIp= '" & Request.UserHostAddress & "'  ,hazf = 1  ,stat = 16 ,notee= N'عدم پذیرش بعلت بالا بودن سابقه' " & _
     " Where idd = " & e.CommandArgs.CommandArgument.ToString() & "  "
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()

        End If

        If e.CommandArgs.CommandName = "okk" Then
            sqlstr = "UPDATE     trnDoreReqisterTbl SET                stat = 17,notee= N'' where idd= " & e.CommandArgs.CommandArgument.ToString() & " "
            upStat(sqlstr)
            load_gridPaziresh()
            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
            UpdatePanel1.Update()
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


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat <> 17 "
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            ASPxGridView1.Visible = True
            ASPxGridView1.DataSource = dtanbar
            ASPxGridView1.DataBind()

        Else
            ASPxGridView1.Visible = False

        End If



    End Sub
    Private Sub load_gridPaziresh()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 ORDER BY teacherScore"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            gridPaziresh.Visible = True
            gridPaziresh.DataSource = dtanbar
            gridPaziresh.DataBind()

        Else
            gridPaziresh.Visible = False

        End If



    End Sub
    Private Sub load_gridA()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 and    scoreID = 1"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            gridA.Visible = True
            gridA.DataSource = dtanbar
            gridA.DataBind()

        Else
            gridA.Visible = False

        End If



    End Sub
    Private Sub load_gridB()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 and    scoreID = 2"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            gridB.Visible = True
            gridB.DataSource = dtanbar
            gridB.DataBind()

        Else
            gridB.Visible = False

        End If



    End Sub
    Private Sub load_gridC()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 and    scoreID = 3"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            gridC.Visible = True
            gridC.DataSource = dtanbar
            gridC.DataBind()

        Else
            gridC.Visible = False

        End If



    End Sub

    Private Sub load_gridPI1()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 and    scoreID = 4"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            ASPxGridViewPI1.Visible = True
            ASPxGridViewPI1.DataSource = dtanbar
            ASPxGridViewPI1.DataBind()

        Else
            ASPxGridViewPI1.Visible = False

        End If



    End Sub
    Private Sub load_gridUPI1()


        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 and    scoreID = 5"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            ASPxGridViewUPI1.Visible = True
            ASPxGridViewUPI1.DataSource = dtanbar
            ASPxGridViewUPI1.DataBind()

        Else
            ASPxGridViewUPI1.Visible = False

        End If



    End Sub

    Private Sub load_gridADV()

        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW    WHERE   stat = 17 and    scoreID = 6"
        'and  hazf = 0 order by scoreID"
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        If dtanbar.Rows.Count > 0 Then
            ASPxGridViewADV.Visible = True
            ASPxGridViewADV.DataSource = dtanbar
            ASPxGridViewADV.DataBind()

        Else
            ASPxGridViewADV.Visible = False

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

            load_grid()

            load_gridPaziresh()




            load_gridA()
            load_gridB()
            load_gridC()
            load_gridADV()
            load_gridPI1()
            load_gridUPI1()
        End If
        'If Me.IsCallback Then
        '    loadgridall()
        'End If
        load_gridPaziresh()
        load_grid()
        load_gridA()
        load_gridB()
        load_gridC()
        load_gridADV()
        load_gridPI1()
        load_gridUPI1()
    End Sub







    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        load_grid()
        load_gridPaziresh()
        load_gridA()
        load_gridB()
        load_gridC()
        load_gridADV()
        load_gridPI1()
        load_gridUPI1()
        UpdatePanel2.Update()

    End Sub




    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub




    Protected Sub ASPxGridView1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridView1.AfterPerformCallback
        load_grid()

    End Sub
    Protected Sub gridPaziresh_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles gridPaziresh.AfterPerformCallback
        load_gridPaziresh()

    End Sub

    Protected Sub gridA_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles gridA.AfterPerformCallback
        load_gridA()

    End Sub
    Protected Sub gridB_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles gridB.AfterPerformCallback
        load_gridB()

    End Sub
    Protected Sub gridC_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles gridC.AfterPerformCallback
        load_gridC()

    End Sub

    Protected Sub gridPI1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridViewPI1.AfterPerformCallback
        load_gridPI1()

    End Sub

    Protected Sub gridUPI1_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridViewUPI1.AfterPerformCallback
        load_gridUPI1()

    End Sub
    Protected Sub gridADV_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGridViewADV.AfterPerformCallback
        load_gridADV()

    End Sub


    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged



        loadgridall()


    End Sub
    Private Sub loadgridall()
        Dim dtanbar As DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  " & _
             " FROM trnDoreReqisterVW where hazf = 0    "
        dtanbar = ClsDataTable.GetDataTable(sqlstr)
        ASPxGridView1.DataSource = dtanbar
        ASPxGridView1.DataBind()

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If ASPxPageControl1.TabPages.Item(0).IsActive = True Then

            grvExporter1.WriteXlsxToResponse()
        ElseIf ASPxPageControl1.TabPages.Item(1).IsActive = True Then

            ASPxGridViewExporterEtmam.WriteXlsxToResponse()

            'ElseIf ASPxPageControl1.TabPages.Item(2).IsActive = True Then
            '    ASPxGridViewADV.WriteXlsxToResponse()

        End If
    End Sub
End Class
