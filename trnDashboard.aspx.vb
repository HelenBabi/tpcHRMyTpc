Imports System.Data
Imports System.Web.Script.Serialization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class trnDashboard
    Inherits System.Web.UI.Page
    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String
    Dim dt2, dt1, dt3 As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()
            load_checkbox()
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow(a, b)
        Else
            load_grid()

        End If

    End Sub

    Private Sub load_checkbox()
        'Dim clsgetdata_ As New ClsDataTable
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT        ID, mahiat, CodeClosed, opid, ArcTypeCode         FROM trnMahiatTbl WHERE        (CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        CheckBoxListMahiat.DataSource = dt
        CheckBoxListMahiat.DataTextField = "mahiat"
        CheckBoxListMahiat.DataValueField = "ID"
        CheckBoxListMahiat.DataBind()
      
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("trnDashboard.aspx", Val(Session("UserId")))

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
    Private Sub loadgrdNow(ByVal dat1 As String, ByVal dat2 As String)
        Dim dt5 As DataTable
        Dim sqlstr As String = "select * from trnDashboardVW WHERE        (tarix Between '" & dat1 & "' And '" & dat2 & "') ORDER BY tarix"
        dt5 = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt5
        ASPxGrvHead.DataBind()

        'CntTypnow()
    
    End Sub

    Private Sub load_grid()
        datS = Request.Form("datepicker")
        dateE = Request.Form("datepicker2")
        lblDateS.Text = datS
        lbldateE.Text = dateE


        'If lbldateE.Text = "" Or lblDateS.Text = "" Then
        '    msg_box("تاریخ شروع و تاریخ اتمام را وارد كنيد")

        '    Exit Sub
        'End If
        If lblDateS.Text = "" Or lbldateE.Text = "" Then
            Exit Sub
        End If
        If Len(lblDateS.Text) < 10 Then
            datS1 = ClsDateTime.setDate(datS)
        End If

        If Len(lbldateE.Text) < 10 Then
            dateE1 = ClsDateTime.setDate(dateE)
        End If

        If Len(lblDateS.Text) = 10 Then
            datS1 = datS
        End If

        If Len(lbldateE.Text) = 10 Then
            dateE1 = dateE
        End If

        Dim sqlstr22 As String = ""
        sqlstr22 = "select * from trnDashboardVW WHERE        (tarix Between '" & datS1 & "' And '" & dateE1 & "')"
        dt1 = ClsDataTable.GetDataTable(sqlstr22)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()
        'CntTyp()


       

       
    End Sub


    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            Dim sql As String = "SELECT      *  FROM            trnDashboardVW WHERE (tarix Between '" & datS1 & "' And '" & dateE1 & "')"
            Dim dt = ClsDataTable.GetDataTable(sql)
            'Dim dt As DataTable = TryCast(Session("CardDataTable"), DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Response.Write("داده‌ای برای نمایش وجود ندارد.")
                Return
            End If

            Dim rpt As New ReportDocument()
            rpt.Load(Server.MapPath("~/Reports/trnDashboards.rpt"))
            rpt.SetDataSource(dt)

            rpt.DataSourceConnections.Clear()

            ' صادر کردن به Word و ارسال به Response
            Dim stream As System.IO.Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "inline; filename=accountSettlement.pdf")
            Dim buffer As Byte() = New Byte(stream.Length - 1) {}
            stream.Read(buffer, 0, buffer.Length)
            Response.OutputStream.Write(buffer, 0, buffer.Length)
            Response.Flush()
            Response.End()

            ' آزادسازی منابع
            rpt.Close()
            rpt.Dispose()
        Catch ex As Exception
            Response.Write("خطا در نمودار ساعت آموزش: " & ex.Message)
        End Try
    End Sub

    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub
    'Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
    '    loadgrdNow()
    'End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click


        load_grid()
        'If datS1 > dateE1 Then
        '    msg_box("تاریخ شروع نمی تواند بزرگتر از تاریخ اتمام باشد")
        '    ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
        '    Exit Sub
        'End If
        ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    End Sub



    Protected Sub ibtnExit_Click(sender As Object, e As EventArgs) Handles ibtnExitt.Click

        Response.Redirect("index.aspx")

    End Sub
End Class