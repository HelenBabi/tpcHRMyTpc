Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
Imports System.Globalization
Imports System
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class trnDashboardRep2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        If Me.IsPostBack = False Then
            sal.Text = ClsDateTime.getSal
            load_grid()
            load_Mahiat()
        End If
        ' برای ماه
        If mah.Items.FindByValue("") Is Nothing Then
            mah.Items.Insert(0, New ListItem("همه", ""))
        End If

    End Sub

    Private Sub load_Mahiat()
        Dim dtuser As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "SELECT        ID, mahiat, CodeClosed, opid, ArcTypeCode         FROM trnMahiatTbl                   WHERE        (CodeClosed = 0)"
        dtuser = ClsDataTable.GetDataTable(sqlstr)
        mahiat.DataSource = dtuser
        mahiat.DataValueField = "ID"
        mahiat.DataTextField = "mahiat"

        mahiat.DataBind()
        mahiat.Items.Insert(0, New ListItem("همه", ""))
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

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub


    Protected Sub lnkDelFile_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(GridView2.DataKeys(gvrow.RowIndex).Value.ToString())

        Dim sqlstr As String = "select idd from trnDashboardTbl where idd= " & fileid & " "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            Dim cnn As New SqlConnection
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            Try
                Dim cmm As New SqlCommand

                cmm.CommandText = "trnEbtal2Prc"
                cmm.CommandType = CommandType.StoredProcedure

                cmm.Parameters.Add("@idd", SqlDbType.Int, 5)
                cmm.Parameters("@idd").Value = fileid


                cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
                cmm.Parameters("@pm").Direction = ParameterDirection.Output

                cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
                cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
                cmm.Connection = cnn

                Dim result As Integer
                cmm.ExecuteNonQuery()
                result = cmm.Parameters("@ResultCode").Value
                If result = 0 Then
                    msg_box("  حذف شد ")
                    load_grid()
                Else
                    msg_box(cmm.Parameters("@pm").Value)
                End If
            Catch ex As Exception
                msg_box(ex.Message.ToString)
            End Try
            load_grid()
        End If



    End Sub


    Private Sub load_grid()
        GridView2.Visible = True
        Dim sqlstr As String = ""

        sqlstr = "SELECT       * from trnDashboardVW ORDER BY idd DESC "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView2.DataSource = dt
            GridView2.DataBind()
        End If


    End Sub

    'Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
    '    Try
    '        Dim sql As String = "SELECT      *  FROM            trnDashboardEntry2VW "
    '        Dim dt = ClsDataTable.GetDataTable(sql)
    '        'Dim dt As DataTable = TryCast(Session("CardDataTable"), DataTable)
    '        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
    '            Response.Write("داده‌ای برای نمایش وجود ندارد.")
    '            Return
    '        End If

    '        Dim rpt As New ReportDocument()
    '        rpt.Load(Server.MapPath("~/Reports/turDashboardMonths.rpt"))
    '        rpt.SetDataSource(dt)

    '        rpt.DataSourceConnections.Clear()

    '        ' صادر کردن به Word و ارسال به Response
    '        Dim stream As System.IO.Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
    '        Response.Clear()
    '        Response.Buffer = True
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("Content-Disposition", "inline; filename=accountSettlement.pdf")
    '        Dim buffer As Byte() = New Byte(stream.Length - 1) {}
    '        stream.Read(buffer, 0, buffer.Length)
    '        Response.OutputStream.Write(buffer, 0, buffer.Length)
    '        Response.Flush()
    '        Response.End()

    '        ' آزادسازی منابع
    '        rpt.Close()
    '        rpt.Dispose()
    '    Catch ex As Exception
    '        Response.Write("خطا در نمودار ساعت آموزش: " & ex.Message)
    '    End Try
    'End Sub
    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            Dim selectedSal As String = sal.Text.Trim()
            Dim selectedMah As String = mah.SelectedValue
            Dim selectedMahiat As String = mahiat.SelectedValue

            Dim sql As String = "SELECT * FROM trnDashboardVW WHERE 1=1"

            If Not String.IsNullOrEmpty(selectedSal) Then
                sql &= " AND sal = " & selectedSal
            End If

            If Not String.IsNullOrEmpty(selectedMah) Then
                ' اگر کاربر گزینه‌ای غیر از "همه" انتخاب کرده باشد
                sql &= " AND Mah <= " & selectedMah
            End If

            If Not String.IsNullOrEmpty(selectedMahiat) Then
                sql &= " AND ID = " & selectedMahiat
            End If

            Dim dt = ClsDataTable.GetDataTable(sql)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Response.Write("داده‌ای برای نمایش وجود ندارد.")
                Return
            End If

            ' بارگذاری گزارش
            Dim rpt As New ReportDocument()
            rpt.Load(Server.MapPath("~/Reports/trnDashboards.rpt"))
            rpt.SetDataSource(dt)
            rpt.DataSourceConnections.Clear()

            ' خروجی به PDF
            Dim stream As System.IO.Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "inline; filename=dashboardReport.pdf")
            Dim buffer As Byte() = New Byte(stream.Length - 1) {}
            stream.Read(buffer, 0, buffer.Length)
            Response.OutputStream.Write(buffer, 0, buffer.Length)
            Response.Flush()
            Response.End()

            rpt.Close()
            rpt.Dispose()
        Catch ex As Exception
            Response.Write("خطا در تولید گزارش: " & ex.Message)
        End Try
    End Sub



End Class
