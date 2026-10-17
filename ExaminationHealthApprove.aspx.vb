Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections.Generic

Partial Class ExaminationHealthApprove
    Inherits System.Web.UI.Page

    Private _personCode As String = ""
    Private _messageType As String = "info"

    ' ============================================================
    ' ثابت‌های StatusCode
    ' ============================================================
    Private Const STATUS_PENDING_SUPERVISOR As Integer = 1
    Private Const STATUS_APPROVED_SUPERVISOR As Integer = 2
    Private Const STATUS_PENDING_HEALTH As Integer = 3
    Private Const STATUS_FINAL_APPROVED As Integer = 4
    Private Const STATUS_REJECTED As Integer = 6
    Private Const STATUS_DONE As Integer = 7
    Private Const STATUS_CANCELED As Integer = 8

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("codePersoneli") Is Nothing Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        _personCode = Session("codePersoneli").ToString().Trim()

        If String.IsNullOrEmpty(_personCode) Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        If Not IsPostBack Then
            LoadAllData()
        End If
    End Sub

    ' ============================================================
    ' بارگذاری همه داده‌ها
    ' ============================================================
    Private Sub LoadAllData()
        LoadRequests()
        LoadApprovedRequests()
    End Sub

    ' ============================================================
    ' توابع کمکی نمایش توضیحات سرپرست
    ' ============================================================
    Protected Function GetSupervisorCommentClass(ByVal comment As Object) As String
        If comment Is Nothing OrElse comment Is DBNull.Value OrElse String.IsNullOrEmpty(comment.ToString().Trim()) Then
            Return "exm-supervisor-comment empty"
        End If
        Return "exm-supervisor-comment"
    End Function

    Protected Function GetSupervisorCommentText(ByVal comment As Object) As String
        If comment Is Nothing OrElse comment Is DBNull.Value OrElse String.IsNullOrEmpty(comment.ToString().Trim()) Then
            Return "بدون توضیح"
        End If
        Return Server.HtmlEncode(comment.ToString())
    End Function

    ' ============================================================
    ' بارگذاری درخواست‌های در انتظار تایید کارشناس بهداشت
    ' ============================================================
    Private Sub LoadRequests()
        Try
            Dim sqlstr As String =
                "SELECT " &
                "    r.ID, " &
                "    r.PersonCode, " &
                "    r.FullName, " &
                "    ISNULL(p.codeMelli, N'') AS NationalCode, " &
                "    r.UnitName, " &
                "    r.Position, " &
                "    r.Mobile, " &
                "    r.ExaminationDate, " &
                "    dbo.FormatDateFa(r.ExaminationDate) AS PersianDate, " &
                "    r.CenterID, " &
                "    r.StatusCode, " &
                "    r.StatusDescription, " &
                "    r.SupervisorName, " &
                "    ISNULL(r.SupervisorComment, N'') AS SupervisorComment, " &
                "    r.SupervisorApproveDate, " &
                "    r.CreateDate, " &
                "    N'در انتظار تایید کارشناس بهداشت' AS StatusText " &
                "FROM exmReservationRequests r " &
                "LEFT JOIN personel p ON r.PersonCode = p.codePersoneli " &
                "WHERE r.StatusCode = 3 " &
                "  AND r.CurrentLevel = 2 " &
                "ORDER BY r.CreateDate DESC"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.CommandTimeout = 60
                    cnn.Open()

                    Dim dt As New DataTable()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                    If Not dt.Columns.Contains("RowNumber") Then
                        dt.Columns.Add("RowNumber", GetType(Integer))
                    End If

                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("RowNumber") = i + 1
                    Next

                    gvRequests.DataSource = dt
                    gvRequests.DataBind()

                    lblPendingCount.Text = dt.Rows.Count.ToString()

                    If dt.Rows.Count = 0 Then
                        ShowMessage("ℹ️ هیچ درخواستی در انتظار تایید کارشناس بهداشت وجود ندارد", "info")
                    End If
                End Using
            End Using

        Catch ex As Exception
            ShowMessage("❌ خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری درخواست‌های تایید شده (برای امکان ابطال)
    ' ============================================================
    Private Sub LoadApprovedRequests()
        Try
            Dim sqlstr As String =
                "SELECT " &
                "    r.ID, " &
                "    r.PersonCode, " &
                "    r.FullName, " &
                "    ISNULL(p.codeMelli, N'') AS NationalCode, " &
                "    r.UnitName, " &
                "    r.ExaminationDate, " &
                "    dbo.FormatDateFa(r.ExaminationDate) AS PersianDate, " &
                "    r.StatusCode, " &
                "    r.StatusDescription, " &
                "    ISNULL(r.HealthExpertComment, N'') AS HealthExpertComment, " &
                "    r.HealthExpertApproveDate, " &
                "    r.CreateDate " &
                "FROM exmReservationRequests r " &
                "LEFT JOIN personel p ON r.PersonCode = p.codePersoneli " &
                "WHERE r.StatusCode = 4 " &
                "ORDER BY r.HealthExpertApproveDate DESC, r.CreateDate DESC"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.CommandTimeout = 60
                    cnn.Open()

                    Dim dt As New DataTable()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                    If Not dt.Columns.Contains("RowNumber") Then
                        dt.Columns.Add("RowNumber", GetType(Integer))
                    End If

                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("RowNumber") = i + 1
                    Next

                    gvApproved.DataSource = dt
                    gvApproved.DataBind()

                    lblApprovedCount.Text = dt.Rows.Count.ToString()

                End Using
            End Using

        Catch ex As Exception
            ShowMessage("❌ خطا در بارگذاری درخواست‌های تایید شده: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' RowDataBound برای درخواست‌های در انتظار
    ' ============================================================
    Protected Sub gvRequests_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim statusCode As Integer = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "StatusCode"))

            Dim btnApprove As Button = TryCast(e.Row.FindControl("btnApprove"), Button)
            Dim btnReject As Button = TryCast(e.Row.FindControl("btnReject"), Button)
            Dim btnCancel As Button = TryCast(e.Row.FindControl("btnCancel"), Button)
            Dim txtComment As TextBox = TryCast(e.Row.FindControl("txtComment"), TextBox)
            Dim chkSelect As CheckBox = TryCast(e.Row.FindControl("chkSelect"), CheckBox)

            If statusCode <> 3 Then
                If btnApprove IsNot Nothing Then btnApprove.Visible = False
                If btnReject IsNot Nothing Then btnReject.Visible = False
                If btnCancel IsNot Nothing Then btnCancel.Visible = False
                If txtComment IsNot Nothing Then txtComment.Visible = False
                If chkSelect IsNot Nothing Then chkSelect.Enabled = False
            End If
        End If
    End Sub

    ' ============================================================
    ' RowCommand درخواست‌های در انتظار (تایید/رد/ابطال)
    ' ✅ اصلاح شده: CancelRequest به جای Cancel
    ' ============================================================
    Protected Sub gvRequests_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvRequests.RowCommand
        Try
            If e.CommandName <> "Approve" AndAlso e.CommandName <> "Reject" AndAlso e.CommandName <> "CancelRequest" Then
                Return
            End If

            Dim button As Button = TryCast(e.CommandSource, Button)
            If button Is Nothing Then
                ShowMessage("❌ دکمه عملیات پیدا نشد.", "danger")
                Return
            End If

            Dim row As GridViewRow = TryCast(button.NamingContainer, GridViewRow)
            If row Is Nothing Then
                ShowMessage("❌ ردیف درخواست پیدا نشد.", "danger")
                Return
            End If

            ' ===== بررسی امن DataKeys =====
            If gvRequests.DataKeys Is Nothing OrElse gvRequests.DataKeys.Count = 0 Then
                ShowMessage("❌ اطلاعات ردیف معتبر نیست.", "danger")
                Return
            End If

            If row.RowIndex < 0 OrElse row.RowIndex >= gvRequests.DataKeys.Count Then
                ShowMessage("❌ ایندکس ردیف معتبر نیست.", "danger")
                Return
            End If

            Dim dataKey As Object = gvRequests.DataKeys(row.RowIndex).Value
            If dataKey Is Nothing Then
                ShowMessage("❌ شناسه درخواست پیدا نشد.", "danger")
                Return
            End If

            Dim requestID As Integer = Convert.ToInt32(dataKey)

            Dim comment As String = ""
            Dim txtComment As TextBox = TryCast(row.FindControl("txtComment"), TextBox)
            If txtComment IsNot Nothing Then
                comment = txtComment.Text.Trim()
            End If

            ' ✅ اصلاح: بررسی CancelRequest
            If e.CommandName = "CancelRequest" Then
                CancelRequest(requestID, comment)
            Else
                ProcessRequest(requestID, e.CommandName, comment)
            End If

            LoadAllData()

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' RowCommand درخواست‌های تایید شده (فقط ابطال)
    ' ✅ اصلاح شده: CancelRequest به جای Cancel
    ' ============================================================
    Protected Sub gvApproved_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvApproved.RowCommand
        Try
            If e.CommandName <> "CancelRequest" Then
                Return
            End If

            Dim button As Button = TryCast(e.CommandSource, Button)
            If button Is Nothing Then
                ShowMessage("❌ دکمه عملیات پیدا نشد.", "danger")
                Return
            End If

            Dim row As GridViewRow = TryCast(button.NamingContainer, GridViewRow)
            If row Is Nothing Then
                ShowMessage("❌ ردیف درخواست پیدا نشد.", "danger")
                Return
            End If

            If gvApproved.DataKeys Is Nothing OrElse gvApproved.DataKeys.Count = 0 Then
                ShowMessage("❌ اطلاعات ردیف معتبر نیست.", "danger")
                Return
            End If

            If row.RowIndex < 0 OrElse row.RowIndex >= gvApproved.DataKeys.Count Then
                ShowMessage("❌ ایندکس ردیف معتبر نیست.", "danger")
                Return
            End If

            Dim dataKey As Object = gvApproved.DataKeys(row.RowIndex).Value
            If dataKey Is Nothing Then
                ShowMessage("❌ شناسه درخواست پیدا نشد.", "danger")
                Return
            End If

            Dim requestID As Integer = Convert.ToInt32(dataKey)

            CancelRequest(requestID, "ابطال توسط کارشناس بهداشت")

            LoadAllData()

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' تایید گروهی
    ' ============================================================
    Protected Sub btnBulkApprove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBulkApprove.Click
        Try
            Dim selectedIDs As List(Of Integer) = GetSelectedRequestIDs()

            If selectedIDs.Count = 0 Then
                ShowMessage("⚠️ لطفاً حداقل یک درخواست را انتخاب کنید.", "warning")
                Return
            End If

            Dim bulkComment As String = txtBulkComment.Text.Trim()
            Dim successCount As Integer = 0
            Dim errorCount As Integer = 0

            For Each requestID As Integer In selectedIDs
                Try
                    ProcessRequest(requestID, "Approve", bulkComment)
                    successCount += 1
                Catch ex As Exception
                    errorCount += 1
                End Try
            Next

            If errorCount = 0 Then
                ShowMessage("✅ " & successCount & " درخواست با موفقیت تایید شد", "success")
            Else
                ShowMessage("✅ " & successCount & " درخواست تایید شد | ❌ " & errorCount & " درخواست خطا داشت", "warning")
            End If

            txtBulkComment.Text = ""
            LoadAllData()

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' رد گروهی
    ' ============================================================
    Protected Sub btnBulkReject_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBulkReject.Click
        Try
            Dim selectedIDs As List(Of Integer) = GetSelectedRequestIDs()

            If selectedIDs.Count = 0 Then
                ShowMessage("⚠️ لطفاً حداقل یک درخواست را انتخاب کنید.", "warning")
                Return
            End If

            Dim bulkComment As String = txtBulkComment.Text.Trim()
            Dim successCount As Integer = 0
            Dim errorCount As Integer = 0

            For Each requestID As Integer In selectedIDs
                Try
                    ProcessRequest(requestID, "Reject", bulkComment)
                    successCount += 1
                Catch ex As Exception
                    errorCount += 1
                End Try
            Next

            If errorCount = 0 Then
                ShowMessage("✅ " & successCount & " درخواست با موفقیت رد شد", "success")
            Else
                ShowMessage("✅ " & successCount & " درخواست رد شد | ❌ " & errorCount & " درخواست خطا داشت", "warning")
            End If

            txtBulkComment.Text = ""
            LoadAllData()

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ابطال گروهی (روی درخواست‌های تایید شده)
    ' ============================================================
    Protected Sub btnBulkCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBulkCancel.Click
        Try
            Dim selectedIDs As List(Of Integer) = GetSelectedApprovedIDs()

            If selectedIDs.Count = 0 Then
                ShowMessage("⚠️ لطفاً حداقل یک درخواست را انتخاب کنید.", "warning")
                Return
            End If

            Dim bulkComment As String = txtBulkCancelReason.Text.Trim()
            If String.IsNullOrEmpty(bulkComment) Then
                bulkComment = "ابطال گروهی توسط کارشناس بهداشت"
            End If

            Dim successCount As Integer = 0
            Dim errorCount As Integer = 0

            For Each requestID As Integer In selectedIDs
                Try
                    CancelRequest(requestID, bulkComment)
                    successCount += 1
                Catch ex As Exception
                    errorCount += 1
                End Try
            Next

            If errorCount = 0 Then
                ShowMessage("✅ " & successCount & " درخواست با موفقیت ابطال شد", "success")
            Else
                ShowMessage("✅ " & successCount & " درخواست ابطال شد | ❌ " & errorCount & " درخواست خطا داشت", "warning")
            End If

            txtBulkCancelReason.Text = ""
            LoadAllData()

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دریافت ID درخواست‌های انتخاب شده (تب در انتظار)
    ' ============================================================
    Private Function GetSelectedRequestIDs() As List(Of Integer)
        Dim selectedIDs As New List(Of Integer)()

        Try
            If gvRequests.DataKeys Is Nothing OrElse gvRequests.DataKeys.Count = 0 Then
                Return selectedIDs
            End If

            For Each row As GridViewRow In gvRequests.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkSelect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                    If chkSelect IsNot Nothing AndAlso chkSelect.Checked AndAlso chkSelect.Enabled Then
                        If row.RowIndex >= 0 AndAlso row.RowIndex < gvRequests.DataKeys.Count Then
                            Dim key As Object = gvRequests.DataKeys(row.RowIndex).Value
                            If key IsNot Nothing Then
                                selectedIDs.Add(Convert.ToInt32(key))
                            End If
                        End If
                    End If
                End If
            Next
        Catch
        End Try

        Return selectedIDs
    End Function

    ' ============================================================
    ' دریافت ID درخواست‌های انتخاب شده (تب تایید شده)
    ' ============================================================
    Private Function GetSelectedApprovedIDs() As List(Of Integer)
        Dim selectedIDs As New List(Of Integer)()

        Try
            If gvApproved.DataKeys Is Nothing OrElse gvApproved.DataKeys.Count = 0 Then
                Return selectedIDs
            End If

            For Each row As GridViewRow In gvApproved.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkSelect As CheckBox = CType(row.FindControl("chkSelectApproved"), CheckBox)
                    If chkSelect IsNot Nothing AndAlso chkSelect.Checked Then
                        If row.RowIndex >= 0 AndAlso row.RowIndex < gvApproved.DataKeys.Count Then
                            Dim key As Object = gvApproved.DataKeys(row.RowIndex).Value
                            If key IsNot Nothing Then
                                selectedIDs.Add(Convert.ToInt32(key))
                            End If
                        End If
                    End If
                End If
            Next
        Catch
        End Try

        Return selectedIDs
    End Function

    ' ============================================================
    ' پردازش درخواست (تایید یا رد)
    ' ============================================================
    Private Sub ProcessRequest(ByVal requestID As Integer, ByVal action As String, ByVal comment As String)
        If requestID <= 0 Then
            Throw New Exception("شناسه درخواست معتبر نیست.")
        End If

        Using cnn As New SqlConnection(ClsConnection.GetConn)
            cnn.Open()

            Using trans As SqlTransaction = cnn.BeginTransaction()
                Try
                    If action = "Reject" Then
                        Dim sql As String =
                            "UPDATE exmReservationRequests " &
                            "SET " &
                            "    StatusCode = 6, " &
                            "    StatusDescription = N'رد شده توسط کارشناس بهداشت', " &
                            "    HealthExpertComment = @Comment, " &
                            "    HealthExpertRejectDate = GETDATE(), " &
                            "    IsDone = 1, " &
                            "    CurrentLevel = 0, " &
                            "    UpdateDate = GETDATE() " &
                            "WHERE ID = @RequestID " &
                            "  AND StatusCode = 3 " &
                            "  AND CurrentLevel = 2"

                        Using cmd As New SqlCommand(sql, cnn, trans)
                            cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = requestID

                            If String.IsNullOrWhiteSpace(comment) Then
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = DBNull.Value
                            Else
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = comment
                            End If

                            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                            If rowsAffected = 0 Then
                                Throw New Exception("درخواست پیدا نشد یا قبلاً بررسی شده است.")
                            End If
                        End Using

                        trans.Commit()
                        Return
                    End If

                    If action = "Approve" Then
                        Dim sql As String =
                            "UPDATE exmReservationRequests " &
                            "SET " &
                            "    StatusCode = 4, " &
                            "    StatusDescription = N'تایید نهایی', " &
                            "    CurrentLevel = 0, " &
                            "    HealthExpertComment = @Comment, " &
                            "    HealthExpertApproveDate = GETDATE(), " &
                            "    IsDone = 1, " &
                            "    UpdateDate = GETDATE() " &
                            "WHERE ID = @RequestID " &
                            "  AND StatusCode = 3 " &
                            "  AND CurrentLevel = 2"

                        Using cmd As New SqlCommand(sql, cnn, trans)
                            cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = requestID

                            If String.IsNullOrWhiteSpace(comment) Then
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = DBNull.Value
                            Else
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = comment
                            End If

                            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                            If rowsAffected = 0 Then
                                Throw New Exception("درخواست تایید نشد. ممکن است قبلاً بررسی شده باشد.")
                            End If
                        End Using

                        trans.Commit()
                        Return
                    End If

                Catch ex As Exception
                    Try
                        trans.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Sub

    ' ============================================================
    ' ابطال درخواست (غیرفعال کردن - Soft Delete)
    ' ============================================================
    Private Sub CancelRequest(ByVal requestID As Integer, ByVal reason As String)
        If requestID <= 0 Then
            Throw New Exception("شناسه درخواست معتبر نیست.")
        End If

        Dim personCode As String = _personCode
        If String.IsNullOrEmpty(personCode) Then
            personCode = "system"
        End If

        If String.IsNullOrEmpty(reason) Then
            reason = "ابطال توسط کارشناس بهداشت"
        End If

        Using cnn As New SqlConnection(ClsConnection.GetConn)
            cnn.Open()

            Using trans As SqlTransaction = cnn.BeginTransaction()
                Try
                    ' ====== بررسی وضعیت فعلی ======
                    Dim checkSql As String =
                        "SELECT StatusCode FROM exmReservationRequests " &
                        "WHERE ID = @RequestID"

                    Dim currentStatus As Integer = 0
                    Using cmd As New SqlCommand(checkSql, cnn, trans)
                        cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = requestID
                        Dim result As Object = cmd.ExecuteScalar()
                        If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                            currentStatus = Convert.ToInt32(result)
                        End If
                    End Using

                    If currentStatus = 0 Then
                        Throw New Exception("درخواست پیدا نشد.")
                    End If

                    If currentStatus <> 3 AndAlso currentStatus <> 4 Then
                        Throw New Exception("فقط درخواست‌های در انتظار یا تایید نهایی قابل ابطال هستند.")
                    End If

                    ' ====== ابطال: StatusCode = 8 ======
                    Dim sql As String =
                        "UPDATE exmReservationRequests " &
                        "SET " &
                        "    StatusCode = 8, " &
                        "    StatusDescription = N'ابطال شده توسط کارشناس بهداشت', " &
                        "    IsDone = 1, " &
                        "    CurrentLevel = 0, " &
                        "    CancelReason = @Reason, " &
                        "    CancelDate = GETDATE(), " &
                        "    CancelBy = @CancelBy, " &
                        "    UpdateDate = GETDATE() " &
                        "WHERE ID = @RequestID"

                    Using cmd As New SqlCommand(sql, cnn, trans)
                        cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = requestID
                        cmd.Parameters.Add("@Reason", SqlDbType.NVarChar, 500).Value = reason
                        cmd.Parameters.Add("@CancelBy", SqlDbType.NVarChar, 50).Value = personCode

                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected = 0 Then
                            Throw New Exception("ابطال انجام نشد.")
                        End If
                    End Using

                    trans.Commit()

                Catch ex As Exception
                    Try
                        trans.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvRequests_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvRequests.PageIndexChanging
        gvRequests.PageIndex = e.NewPageIndex
        LoadRequests()
    End Sub

    Protected Sub gvApproved_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvApproved.PageIndexChanging
        gvApproved.PageIndex = e.NewPageIndex
        LoadApprovedRequests()
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/index.aspx")
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
                pnlMessage.CssClass = "exm-alert exm-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                pnlMessage.CssClass = "exm-alert exm-alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
                pnlMessage.CssClass = "exm-alert exm-alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                pnlMessage.CssClass = "exm-alert exm-alert-info"
        End Select

        pnlMessage.Visible = True
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class