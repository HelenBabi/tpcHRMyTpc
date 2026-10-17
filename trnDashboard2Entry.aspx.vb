Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
Imports System.Globalization
Imports System
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class trnDashboard2Entry
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

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        save_final()
    End Sub


    Private Sub save_final()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnDashboard2Prc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@sal", SqlDbType.Int, 5)
            cmm.Parameters("@sal").Value = Val(sal.Text)

            cmm.Parameters.Add("@mah", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mah").Value = mah.SelectedValue

            cmm.Parameters.Add("@mahiat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mahiat").Value = mahiat.SelectedValue

            cmm.Parameters.Add("@saat", SqlDbType.Int, 5)
            cmm.Parameters("@saat").Value = Val(saat.Text)

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  ثبت شد ")
                load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


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

        sqlstr = "SELECT       * from trnDashboardEntry2VW ORDER BY idd DESC "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView2.DataSource = dt
            GridView2.DataBind()
        End If


    End Sub

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            Dim sql As String = "SELECT      *  FROM            trnDashboardEntry2VW "
            Dim dt = ClsDataTable.GetDataTable(sql)
            'Dim dt As DataTable = TryCast(Session("CardDataTable"), DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Response.Write("داده‌ای برای نمایش وجود ندارد.")
                Return
            End If

            Dim rpt As New ReportDocument()
            rpt.Load(Server.MapPath("~/Reports/turDashboardMonths.rpt"))
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
End Class
