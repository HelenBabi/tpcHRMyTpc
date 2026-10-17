Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Partial Class trnProgramsFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_grid_File()
        End If
    End Sub

    Private Sub load_grid_File()
        Try
            ASPxGrvHead0.Visible = True

            Dim dtan As DataTable
            Dim sqlstr As String = "SELECT * FROM trnDoreGridVW ORDER BY stat"
            dtan = ClsDataTable.GetDataTable(sqlstr)

            ASPxGrvHead0.DataSource = dtan
            ASPxGrvHead0.DataBind()

            CalculateStatsFromDataSource(dtan)

        Catch ex As Exception
            msg_box("خطا در بارگذاری اطلاعات: " & ex.Message)
        End Try
    End Sub

    Private Sub CalculateStatsFromDataSource(dt As DataTable)
        Try
            Dim total As Integer = dt.Rows.Count
            Dim activeCount As Integer = 0
            Dim pendingCount As Integer = 0
            Dim completedCount As Integer = 0

            For Each row As DataRow In dt.Rows
                Dim status As String = row("statValue").ToString().Trim()

                If status = "در حال برگزاری" Then
                    activeCount += 1
                ElseIf status = "در حال ثبت‌نام" Then
                    pendingCount += 1
                ElseIf status = "برگزار شده" Then
                    completedCount += 1
                End If
            Next

            Dim script As String = String.Format("updateStatsDisplay({0}, {1}, {2}, {3});", total, activeCount, pendingCount, completedCount)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "stats", script, True)

        Catch ex As Exception
            Dim script As String = "updateStatsDisplay(0, 0, 0, 0);"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "stats", script, True)
        End Try
    End Sub

    Protected Sub ASPxGrvHead0_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles ASPxGrvHead0.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim stat As Object = DataBinder.Eval(e.Row.DataItem, "stat")
            Dim statusValue As String = DataBinder.Eval(e.Row.DataItem, "statValue").ToString()
            Dim doreID As Object = DataBinder.Eval(e.Row.DataItem, "id")

            Dim hasFile As Boolean = CheckFileAttachment(doreID)
            Dim lnkDownload As LinkButton = CType(e.Row.FindControl("lnkDownload"), LinkButton)

            If lnkDownload IsNot Nothing Then
                If Not hasFile Then
                    lnkDownload.Enabled = False
                    lnkDownload.Text = "بدون فایل"
                    lnkDownload.CssClass = "btn-download-disabled"
                    lnkDownload.ToolTip = "برای این دوره فایل ضمیمه وجود ندارد"
                End If
            End If

            If e.Row.Cells.Count > 9 Then
                Select Case stat
                    Case 9
                        e.Row.CssClass = "status-pending"
                        e.Row.Cells(9).Text = "<span class='badge-status badge-warning'>" & statusValue & "</span>"
                    Case 12
                        e.Row.CssClass = "status-registering"
                        e.Row.Cells(9).Text = "<span class='badge-status badge-success'>" & statusValue & "</span>"
                    Case 13
                        e.Row.CssClass = "status-expired"
                        e.Row.Cells(9).Text = "<span class='badge-status badge-secondary'>" & statusValue & "</span>"
                    Case 15
                        e.Row.CssClass = "status-cancelled"
                        e.Row.Cells(9).Text = "<span class='badge-status badge-danger'>" & statusValue & "</span>"
                    Case Else
                        e.Row.Cells(9).Text = "<span class='badge-status'>" & statusValue & "</span>"
                End Select
            End If
        End If
    End Sub

    Private Function CheckFileAttachment(doreID As Object) As Boolean
        Try
            If doreID Is Nothing Then Return False
            Dim sqlstr As String = "SELECT COUNT(*) FROM documentsTbl WHERE id = " & Convert.ToInt32(doreID)
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)
            Return dt.Rows.Count > 0 AndAlso Convert.ToInt32(dt.Rows(0)(0)) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)

            Dim fileid As Integer = 0
            If ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value IsNot Nothing Then
                fileid = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value)
            End If

            Dim sqlstrd As String = "SELECT id, content_name, content_file FROM documentsTbl WHERE id = " & fileid
            Dim dtd As New DataTable
            dtd = ClsDataTable.GetDataTable(sqlstrd)

            If dtd.Rows.Count > 0 Then
                Dim fileName As String = dtd.Rows(0).Item("content_name").ToString()
                Dim fileContent As Byte() = DirectCast(dtd.Rows(0).Item("content_file"), Byte())

                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & fileName & """")
                Response.BinaryWrite(fileContent)
                Response.Flush()
                Response.End()
            Else
                msg_box("این دوره فایل ضمیمه ندارد")
            End If
        Catch ex As Exception
            msg_box("خطا در دانلود فایل: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("Swal.fire({ title: 'توجه', text: '")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("', icon: 'info', confirmButtonText: 'باشه', confirmButtonColor: '#667eea' });")

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
    End Sub

End Class