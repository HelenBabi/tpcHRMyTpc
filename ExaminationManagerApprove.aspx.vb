Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization
Imports System.Collections.Generic

Partial Class ExaminationManagerApprove
    Inherits System.Web.UI.Page

    Private _personCode As String = ""
    Private _messageType As String = "info"
    Private _managerUnitCode As String = ""
    Private _managerUnitName As String = ""

    ' ============================================================
    ' مقدار ثابت: این فرم مخصوص رییس واحد است → ApproveBy = 2
    ' ============================================================
    Private ReadOnly Property ApproveByValue As Integer
        Get
            Return 2
        End Get
    End Property

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
            LoadUnitCompletionStatus()
            LoadRequests()
        End If
    End Sub

    ' ============================================================
    ' متد کمکی GetDataTable
    ' ============================================================
    Private Function GetDataTable(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Dim dt As New DataTable()
        Using cnn As New SqlConnection(ClsConnection.GetConn)
            Using cmd As New SqlCommand(sql, cnn)
                For Each p As SqlParameter In parameters
                    cmd.Parameters.Add(p)
                Next
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' ============================================================
    ' بارگذاری درخواست‌ها (فقط ApproveBy = 2)
    ' ============================================================
    Private Sub LoadRequests()
        Try
            Dim sqlstr As String = "SELECT " &
                "    r.ID, " &
                "    r.PersonCode, " &
                "    r.FullName, " &
                "    r.UnitName, " &
                "    r.ExaminationDate, " &
                "    dbo.FormatDateFa(r.ExaminationDate) AS PersianDate, " &
                "    r.StatusCode, " &
                "    r.StatusDescription, " &
                "    r.CreateDate, " &
                "    CASE r.StatusCode " &
                "        WHEN 1 THEN N'در انتظار تایید رییس واحد' " &
                "        WHEN 2 THEN N'تایید رییس واحد' " &
                "        WHEN 3 THEN N'در انتظار تایید کارشناس بهداشت' " &
                "        WHEN 4 THEN N'تایید نهایی' " &
                "        WHEN 6 THEN N'رد شده' " &
                "        WHEN 7 THEN N'انجام شده' " &
                "        ELSE N'نامشخص' " &
                "    END AS StatusText, " &
                "    CASE r.StatusCode " &
                "        WHEN 1 THEN 'pending' " &
                "        WHEN 2 THEN 'approved' " &
                "        WHEN 3 THEN 'pending' " &
                "        WHEN 4 THEN 'approved' " &
                "        WHEN 6 THEN 'rejected' " &
                "        WHEN 7 THEN 'done' " &
                "        ELSE 'unknown' " &
                "    END AS StatusClass " &
                "FROM exmReservationRequests r " &
                "INNER JOIN exmUnits u ON r.UnitID = u.ID " &
                "WHERE r.SupervisorCode = @ManagerCode " &
                "  AND r.StatusCode = 1 " &
                "  AND ISNULL(u.ApproveBy, 1) = @ApproveBy " &
                "ORDER BY r.CreateDate DESC"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.AddWithValue("@ManagerCode", _personCode)
                    cmd.Parameters.AddWithValue("@ApproveBy", ApproveByValue)
                    cmd.CommandTimeout = 60
                    cnn.Open()

                    Dim dt As New DataTable()
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)

                    If dt.Columns.Contains("RowNumber") = False Then
                        dt.Columns.Add("RowNumber", GetType(Integer))
                    End If

                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("RowNumber") = i + 1
                    Next

                    gvRequests.DataSource = dt
                    gvRequests.DataBind()

                    If dt.Rows.Count = 0 Then
                        ShowMessage("ℹ️ هیچ درخواستی برای تایید وجود ندارد", "info")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری وضعیت تکمیل رزرو واحد
    ' ============================================================
    Private Sub LoadUnitCompletionStatus()
        Try
            If String.IsNullOrEmpty(_personCode) Then
                pnlUnitStatus.Visible = False
                Return
            End If

            Dim sqlUnit As String =
            "SELECT TOP 1 p.UnitsCode, u.UnitsName " &
            "FROM personel p " &
            "LEFT JOIN tblUnits u ON p.UnitsCode = u.UnitsCode " &
            "WHERE p.codePersoneli = @PersonCode"

            Dim dtUnit As DataTable = GetDataTable(sqlUnit, New SqlParameter("@PersonCode", _personCode))

            If dtUnit Is Nothing OrElse dtUnit.Rows.Count = 0 Then
                pnlUnitStatus.Visible = False
                Return
            End If

            If Not dtUnit.Columns.Contains("UnitsCode") OrElse Not dtUnit.Columns.Contains("UnitsName") Then
                pnlUnitStatus.Visible = False
                Return
            End If

            _managerUnitCode = If(dtUnit.Rows(0)("UnitsCode") Is DBNull.Value, "", dtUnit.Rows(0)("UnitsCode").ToString().Trim())
            _managerUnitName = If(dtUnit.Rows(0)("UnitsName") Is DBNull.Value, "نامشخص", dtUnit.Rows(0)("UnitsName").ToString())

            If String.IsNullOrEmpty(_managerUnitCode) Then
                pnlUnitStatus.Visible = False
                Return
            End If

            Dim sqlTotal As String =
            "SELECT COUNT(*) FROM personel WHERE UnitsCode = @UnitCode"

            Dim totalPersonnel As Integer = 0
            Dim dtTotal As DataTable = GetDataTable(sqlTotal, New SqlParameter("@UnitCode", _managerUnitCode))

            If dtTotal IsNot Nothing AndAlso dtTotal.Rows.Count > 0 AndAlso dtTotal.Rows(0)(0) IsNot DBNull.Value Then
                totalPersonnel = Convert.ToInt32(dtTotal.Rows(0)(0))
            End If

            Dim sqlReserved As String =
            "SELECT COUNT(DISTINCT r.PersonCode) " &
            "FROM exmReservationRequests r " &
            "INNER JOIN personel p ON r.PersonCode = p.codePersoneli " &
            "WHERE p.UnitsCode = @UnitCode " &
            "  AND r.StatusCode NOT IN (6, 7,8)"

            Dim reservedCount As Integer = 0
            Dim dtReserved As DataTable = GetDataTable(sqlReserved, New SqlParameter("@UnitCode", _managerUnitCode))

            If dtReserved IsNot Nothing AndAlso dtReserved.Rows.Count > 0 AndAlso dtReserved.Rows(0)(0) IsNot DBNull.Value Then
                reservedCount = Convert.ToInt32(dtReserved.Rows(0)(0))
            End If

            Dim percent As Double = 0
            If totalPersonnel > 0 Then
                percent = Math.Round((CDbl(reservedCount) / CDbl(totalPersonnel)) * 100, 1)
            End If

            lblUnitName.Text = _managerUnitName
            lblTotalPersonnel.Text = totalPersonnel.ToString()
            lblReservedCount.Text = reservedCount.ToString()
            lblNotReservedCount.Text = (totalPersonnel - reservedCount).ToString()
            lblCompletionPercent.Text = percent.ToString("0.0") & "%"

            Dim badgeClass As String = "percent-badge"
            If percent < 30 Then
                badgeClass = "percent-badge low"
            ElseIf percent < 70 Then
                badgeClass = "percent-badge medium"
            End If
            lblCompletionPercent.CssClass = badgeClass

            ' ===== استفاده از Attributes به‌جای Style (رفع خطای Index) =====
            If progressBarFill IsNot Nothing Then
                progressBarFill.Attributes("style") = "width: " & percent.ToString("0.0") & "%;"
            End If

            Dim sqlNotReserved As String =
            "SELECT p.codePersoneli, p.nam + ' ' + p.family AS FullName, ISNULL(p.mobile, '') AS Mobile " &
            "FROM personel p " &
            "WHERE p.UnitsCode = @UnitCode " &
            "  AND p.codePersoneli NOT IN ( " &
            "      SELECT DISTINCT r.PersonCode FROM exmReservationRequests r " &
            "      WHERE r.StatusCode NOT IN (6, 7,8) " &
            "  ) " &
            "ORDER BY p.family, p.nam"

            Dim dtNotReserved As DataTable = GetDataTable(sqlNotReserved, New SqlParameter("@UnitCode", _managerUnitCode))

            If dtNotReserved IsNot Nothing AndAlso dtNotReserved.Rows.Count > 0 Then
                rptNotReserved.DataSource = dtNotReserved
                rptNotReserved.DataBind()
                pnlNotReservedList.Visible = True
                lblNotReservedEmpty.Visible = False
            Else
                pnlNotReservedList.Visible = False
                lblNotReservedEmpty.Visible = True
            End If

            pnlUnitStatus.Visible = True

        Catch ex As Exception
            pnlUnitStatus.Visible = False
            ShowMessage("⚠️ خطا در بارگذاری آمار واحد: " & ex.Message, "warning")
        End Try
    End Sub

    ' ============================================================
    ' RowDataBound
    ' ============================================================
    Protected Sub gvRequests_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim statusCode As Integer = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "StatusCode"))

                Dim btnApprove As Button = CType(e.Row.FindControl("btnApprove"), Button)
                Dim btnReject As Button = CType(e.Row.FindControl("btnReject"), Button)
                Dim txtComment As TextBox = CType(e.Row.FindControl("txtComment"), TextBox)
                Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)

                If statusCode <> 1 Then
                    If btnApprove IsNot Nothing Then btnApprove.Visible = False
                    If btnReject IsNot Nothing Then btnReject.Visible = False
                    If txtComment IsNot Nothing Then txtComment.Visible = False
                    If chkSelect IsNot Nothing Then chkSelect.Enabled = False
                End If
            Catch
            End Try
        End If
    End Sub

    ' ============================================================
    ' تایید/رد تکی (RowCommand) - ✅ اصلاح‌شده
    ' ============================================================
    Protected Sub gvRequests_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvRequests.RowCommand
        Try
            If e.CommandName <> "Approve" AndAlso e.CommandName <> "Reject" Then
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
                ShowMessage("❌ اطلاعات ردیف معتبر نیست. لطفاً صفحه را رفرش کنید.", "danger")
                Return
            End If

            If row.RowIndex < 0 OrElse row.RowIndex >= gvRequests.DataKeys.Count Then
                ShowMessage("❌ ایندکس ردیف معتبر نیست.", "danger")
                Return
            End If

            Dim key As Object = gvRequests.DataKeys(row.RowIndex).Value
            If key Is Nothing Then
                ShowMessage("❌ شناسه درخواست پیدا نشد.", "danger")
                Return
            End If

            Dim requestID As Integer = Convert.ToInt32(key)

            Dim comment As String = ""
            Dim txtComment As TextBox = TryCast(row.FindControl("txtComment"), TextBox)
            If txtComment IsNot Nothing Then
                comment = txtComment.Text.Trim()
            End If

            ProcessRequest(requestID, e.CommandName, comment)

            ' ===== پاک کردن پیام قبلی و بارگذاری مجدد =====
            pnlMessage.Visible = False

            LoadUnitCompletionStatus()
            LoadRequests()

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
            Dim lastError As String = ""

            For Each requestID As Integer In selectedIDs
                Try
                    ProcessRequest(requestID, "Approve", bulkComment)
                    successCount += 1
                Catch ex As Exception
                    errorCount += 1
                    lastError = ex.Message
                End Try
            Next

            If errorCount = 0 Then
                ShowMessage("✅ " & successCount & " درخواست با موفقیت تایید شد", "success")
            Else
                ShowMessage("✅ " & successCount & " درخواست تایید شد | ❌ " & errorCount & " درخواست خطا داشت" & If(lastError <> "", " - " & lastError, ""), "warning")
            End If

            txtBulkComment.Text = ""
            LoadUnitCompletionStatus()
            LoadRequests()

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
            Dim lastError As String = ""

            For Each requestID As Integer In selectedIDs
                Try
                    ProcessRequest(requestID, "Reject", bulkComment)
                    successCount += 1
                Catch ex As Exception
                    errorCount += 1
                    lastError = ex.Message
                End Try
            Next

            If errorCount = 0 Then
                ShowMessage("✅ " & successCount & " درخواست با موفقیت رد شد", "success")
            Else
                ShowMessage("✅ " & successCount & " درخواست رد شد | ❌ " & errorCount & " درخواست خطا داشت" & If(lastError <> "", " - " & lastError, ""), "warning")
            End If

            txtBulkComment.Text = ""
            LoadUnitCompletionStatus()
            LoadRequests()

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دریافت ID درخواست‌های انتخاب شده - ✅ اصلاح‌شده
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
        Catch ex As Exception
            ' در صورت خطا، لیست خالی برگردان
        End Try

        Return selectedIDs
    End Function

    ' ============================================================
    ' پردازش درخواست (تایید یا رد) - مخصوص رییس واحد
    ' ============================================================
    Private Sub ProcessRequest(ByVal requestID As Integer, ByVal action As String, ByVal comment As String)
        If requestID <= 0 Then
            Throw New Exception("شناسه درخواست معتبر نیست.")
        End If

        Dim personCode As String = ""
        If Session("codePersoneli") IsNot Nothing Then
            personCode = Session("codePersoneli").ToString().Trim()
        End If

        If String.IsNullOrEmpty(personCode) Then
            Throw New Exception("کد پرسنلی رییس واحد یافت نشد")
        End If

        Dim rejectDesc As String = "رد شده توسط رییس واحد"
        Dim approveDesc As String = "در انتظار تایید کارشناس بهداشت"

        Using cnn As New SqlConnection(ClsConnection.GetConn)
            cnn.Open()

            Using trans As SqlTransaction = cnn.BeginTransaction()
                Try
                    If action = "Reject" Then
                        Dim updateSql As String =
                            "UPDATE r " &
                            "SET r.StatusCode = 6, " &
                            "    r.StatusDescription = @RejectDesc, " &
                            "    r.SupervisorComment = @Comment, " &
                            "    r.SupervisorRejectDate = GETDATE(), " &
                            "    r.IsDone = 1, " &
                            "    r.CurrentLevel = 0, " &
                            "    r.UpdateDate = GETDATE() " &
                            "FROM exmReservationRequests r " &
                            "INNER JOIN exmUnits u ON r.UnitID = u.ID " &
                            "WHERE r.ID = @RequestID " &
                            "  AND r.SupervisorCode = @ManagerCode " &
                            "  AND r.StatusCode = 1 " &
                            "  AND ISNULL(u.ApproveBy, 1) = @ApproveBy"

                        Using cmd As New SqlCommand(updateSql, cnn, trans)
                            cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = requestID
                            cmd.Parameters.Add("@ManagerCode", SqlDbType.NVarChar, 50).Value = personCode
                            cmd.Parameters.Add("@ApproveBy", SqlDbType.TinyInt).Value = ApproveByValue
                            cmd.Parameters.Add("@RejectDesc", SqlDbType.NVarChar, 200).Value = rejectDesc

                            If String.IsNullOrEmpty(comment) Then
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, -1).Value = DBNull.Value
                            Else
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, -1).Value = comment
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
                        Dim updateSql As String =
                            "UPDATE r " &
                            "SET r.StatusCode = 3, " &
                            "    r.StatusDescription = @ApproveDesc, " &
                            "    r.CurrentLevel = 2, " &
                            "    r.SupervisorComment = @Comment, " &
                            "    r.SupervisorApproveDate = GETDATE(), " &
                            "    r.UpdateDate = GETDATE() " &
                            "FROM exmReservationRequests r " &
                            "INNER JOIN exmUnits u ON r.UnitID = u.ID " &
                            "WHERE r.ID = @RequestID " &
                            "  AND r.SupervisorCode = @ManagerCode " &
                            "  AND r.StatusCode = 1 " &
                            "  AND ISNULL(u.ApproveBy, 1) = @ApproveBy"

                        Using cmd As New SqlCommand(updateSql, cnn, trans)
                            cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = requestID
                            cmd.Parameters.Add("@ManagerCode", SqlDbType.NVarChar, 50).Value = personCode
                            cmd.Parameters.Add("@ApproveBy", SqlDbType.TinyInt).Value = ApproveByValue
                            cmd.Parameters.Add("@ApproveDesc", SqlDbType.NVarChar, 200).Value = approveDesc

                            If String.IsNullOrEmpty(comment) Then
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, -1).Value = DBNull.Value
                            Else
                                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, -1).Value = comment
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
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvRequests_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvRequests.PageIndexChanging
        gvRequests.PageIndex = e.NewPageIndex
        LoadRequests()
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/index.aspx")
    End Sub

    ' ============================================================
    ' خروجی Excel
    ' ============================================================
    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim sqlUnit As String =
            "SELECT TOP 1 p.UnitsCode, u.UnitsName " &
            "FROM personel p " &
            "LEFT JOIN tblUnits u ON p.UnitsCode = u.UnitsCode " &
            "WHERE p.codePersoneli = @PersonCode"

            Dim dtUnit As DataTable = GetDataTable(sqlUnit, New SqlParameter("@PersonCode", _personCode))

            If dtUnit Is Nothing OrElse dtUnit.Rows.Count = 0 Then
                ShowMessage("⚠️ واحد شما یافت نشد.", "warning")
                Return
            End If

            Dim unitCode As String = If(dtUnit.Rows(0)("UnitsCode") Is DBNull.Value, "", dtUnit.Rows(0)("UnitsCode").ToString().Trim())
            Dim unitName As String = If(dtUnit.Rows(0)("UnitsName") Is DBNull.Value, "نامشخص", dtUnit.Rows(0)("UnitsName").ToString())

            If String.IsNullOrEmpty(unitCode) Then
                ShowMessage("⚠️ کد واحد شما یافت نشد.", "warning")
                Return
            End If

            Dim sqlAll As String =
            "SELECT " &
            "    p.codePersoneli AS [کد پرسنلی], " &
            "    p.nam + ' ' + p.family AS [نام و نام خانوادگی], " &
            "    ISNULL(p.mobile, '') AS [موبایل], " &
            "    CASE WHEN EXISTS ( " &
            "        SELECT 1 FROM exmReservationRequests r " &
            "        WHERE r.PersonCode = p.codePersoneli " &
            "          AND r.StatusCode NOT IN (6, 7,8) " &
            "    ) THEN N'✅ رزرو کرده' ELSE N'⏳ رزرو نکرده' END AS [وضعیت رزرو], " &
            "    ISNULL(( " &
            "        SELECT TOP 1 dbo.FormatDateFa(r.ExaminationDate) " &
            "        FROM exmReservationRequests r " &
            "        WHERE r.PersonCode = p.codePersoneli " &
            "          AND r.StatusCode NOT IN (6, 7,8) " &
            "        ORDER BY r.CreateDate DESC " &
            "    ), N'-') AS [تاریخ معاینه] " &
            "FROM personel p " &
            "WHERE p.UnitsCode = @UnitCode " &
            "ORDER BY CASE WHEN EXISTS ( " &
            "        SELECT 1 FROM exmReservationRequests r " &
            "        WHERE r.PersonCode = p.codePersoneli " &
            "          AND r.StatusCode NOT IN (6, 7,8) " &
            "    ) THEN 1 ELSE 0 END, p.family, p.nam"

            Dim dtAll As DataTable = GetDataTable(sqlAll, New SqlParameter("@UnitCode", unitCode))

            If dtAll Is Nothing OrElse dtAll.Rows.Count = 0 Then
                ShowMessage("⚠️ هیچ پرسنلی در واحد شما یافت نشد.", "warning")
                Return
            End If

            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.UTF8
            Response.AddHeader("content-disposition", "attachment;filename=ManagerUnitReservationStatus_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xls")
            Response.ContentType = "application/vnd.ms-excel"

            Dim sb As New StringBuilder()

            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml' dir='rtl'>")
            sb.Append("<head>")
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />")
            sb.Append("<style>")
            sb.Append("table { border-collapse: collapse; width: 100%; font-family: Tahoma, Arial; direction: rtl; }")
            sb.Append("th { background: #880e4f; color: #fff; padding: 10px; border: 1px solid #ccc; text-align: right; font-size: 13px; }")
            sb.Append("td { padding: 8px; border: 1px solid #ddd; text-align: right; font-size: 12px; }")
            sb.Append("tr:nth-child(even) { background: #f8f9fa; }")
            sb.Append(".header { text-align: center; font-size: 18px; font-weight: bold; padding: 15px; background: #fce4ec; }")
            sb.Append(".sub-header { text-align: center; font-size: 14px; padding: 10px; background: #f8bbd0; }")
            sb.Append(".reserved { background: #d4edda !important; color: #155724; font-weight: bold; }")
            sb.Append(".not-reserved { background: #fff3cd !important; color: #856404; font-weight: bold; }")
            sb.Append("</style>")
            sb.Append("</head>")
            sb.Append("<body>")

            sb.Append("<div class='header'>📊 گزارش وضعیت رزرو معاینه - رییس واحد</div>")
            sb.Append("<div class='sub-header'>")
            sb.Append("واحد: <b>" & unitName & "</b> | ")
            sb.Append("تاریخ گزارش: <b>" & FormatDateFaPublic(DateTime.Now) & "</b> | ")
            sb.Append("تعداد کل پرسنل: <b>" & dtAll.Rows.Count.ToString() & "</b>")
            sb.Append("</div>")

            sb.Append("<br/>")
            sb.Append("<table>")
            sb.Append("<thead><tr>")
            For Each col As DataColumn In dtAll.Columns
                sb.Append("<th>" & col.ColumnName & "</th>")
            Next
            sb.Append("</tr></thead><tbody>")

            For Each dr As DataRow In dtAll.Rows
                sb.Append("<tr>")
                For Each col As DataColumn In dtAll.Columns
                    Dim cellValue As String = If(dr(col) Is DBNull.Value, "-", dr(col).ToString())
                    Dim cellClass As String = ""
                    If col.ColumnName = "وضعیت رزرو" Then
                        If cellValue.Contains("رزرو کرده") Then
                            cellClass = " class='reserved'"
                        Else
                            cellClass = " class='not-reserved'"
                        End If
                    End If
                    sb.Append("<td" & cellClass & ">" & Server.HtmlEncode(cellValue) & "</td>")
                Next
                sb.Append("</tr>")
            Next

            sb.Append("</tbody></table>")
            sb.Append("</body></html>")

            Response.Write(sb.ToString())
            Response.Flush()
            Response.End()

        Catch ex As Exception
            ShowMessage("❌ خطا در تولید Excel: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' فرمت تاریخ فارسی
    ' ============================================================
    Private Function FormatDateFaPublic(ByVal dt As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString("0000") & "/" &
               pc.GetMonth(dt).ToString("00") & "/" &
               pc.GetDayOfMonth(dt).ToString("00")
        Catch
            Return dt.ToString("yyyy/MM/dd")
        End Try
    End Function

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
            Case "danger"
                lblMessageIcon.Text = "❌"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
            Case Else
                lblMessageIcon.Text = "ℹ️"
        End Select

        pnlMessage.Visible = True
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class