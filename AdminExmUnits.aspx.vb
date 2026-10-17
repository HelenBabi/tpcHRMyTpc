Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization

Partial Class AdminExmUnits
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUnits()
        End If
    End Sub

    ' ============================================================
    ' متد کمکی برای اجرای کوئری با پارامتر
    ' ============================================================
    Private Function GetDataTableWithParams(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Try
            Dim paramDict As New Dictionary(Of String, Object)()
            For Each p As SqlParameter In parameters
                paramDict.Add(p.ParameterName, p.Value)
            Next
            Return ClsDataTable.GetDataTable(sql, paramDict)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' ============================================================
    ' تبدیل تاریخ شمسی به میلادی
    ' ============================================================
    Private Function ConvertPersianDateToGregorian(ByVal persianDate As String) As String
        Try
            If String.IsNullOrEmpty(persianDate) Then
                Return ""
            End If

            Dim parts() As String = persianDate.Split("/"c)
            If parts.Length <> 3 Then
                Return ""
            End If

            Dim year As Integer = Convert.ToInt32(parts(0))
            Dim month As Integer = Convert.ToInt32(parts(1))
            Dim day As Integer = Convert.ToInt32(parts(2))

            Dim pc As New PersianCalendar()
            Dim dt As DateTime = pc.ToDateTime(year, month, day, 0, 0, 0, 0)

            Return dt.ToString("yyyy-MM-dd")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' تبدیل تاریخ میلادی به شمسی
    ' ============================================================
    Private Function ConvertGregorianToPersian(ByVal gregorianDate As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Dim year As Integer = pc.GetYear(gregorianDate)
            Dim month As Integer = pc.GetMonth(gregorianDate)
            Dim day As Integer = pc.GetDayOfMonth(gregorianDate)

            Return year.ToString() & "/" & month.ToString().PadLeft(2, "0"c) & "/" & day.ToString().PadLeft(2, "0"c)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' تولید HTML برای نمایش مهلت
    ' ============================================================
    Protected Function GetDeadlineHtml(ByVal deadlineDate As Object, ByVal persianDeadline As Object) As String
        Try
            If deadlineDate Is Nothing OrElse deadlineDate Is DBNull.Value Then
                Return "<span class='deadline-empty'>---</span>"
            End If

            Dim persian As String = ""
            If persianDeadline IsNot Nothing AndAlso persianDeadline IsNot DBNull.Value Then
                persian = persianDeadline.ToString()
            End If

            If String.IsNullOrEmpty(persian) Then
                Return "<span class='deadline-empty'>---</span>"
            End If

            Dim deadline As DateTime = Convert.ToDateTime(deadlineDate)
            Dim today As DateTime = DateTime.Now.Date

            If today > deadline.Date Then
                Return "<span class='deadline-expired'>⏰ " & persian & " (منقضی)</span>"
            Else
                Return "<span class='deadline-badge'>⏰ " & persian & "</span>"
            End If

        Catch ex As Exception
            Return "<span class='deadline-empty'>---</span>"
        End Try
    End Function

    ' ============================================================
    ' تولید HTML برای نمایش نوع تاییدکننده
    ' ============================================================
    Protected Function GetApproveByHtml(ByVal approveBy As Object, ByVal managerName As Object) As String
        Try
            Dim approveByValue As Integer = 1
            If approveBy IsNot Nothing AndAlso approveBy IsNot DBNull.Value Then
                approveByValue = Convert.ToInt32(approveBy)
            End If

            If approveByValue = 2 Then
                Dim managerText As String = ""
                If managerName IsNot Nothing AndAlso managerName IsNot DBNull.Value Then
                    managerText = managerName.ToString()
                End If

                Dim result As String = "<span class='badge-approve-manager'>👔 رییس واحد</span>"
                If Not String.IsNullOrEmpty(managerText) Then
                    result &= "<br /><small style='color:#6c757d; font-size:11px;'>" & managerText & "</small>"
                End If
                Return result
            Else
                Return "<span class='badge-approve-supervisor'>👤 سرپرست</span>"
            End If

        Catch ex As Exception
            Return "<span class='badge-approve-supervisor'>👤 سرپرست</span>"
        End Try
    End Function

    ' ============================================================
    ' بارگذاری لیست واحدها
    ' ============================================================
    Private Sub LoadUnits()
        Try
            Dim sql As String = "SELECT ID, UnitName, ISNULL(UnitCode, '') AS UnitCode, " &
                "DeadlineDate, " &
                "CASE WHEN DeadlineDate IS NOT NULL THEN dbo.FormatDateFa(DeadlineDate) ELSE '' END AS PersianDeadline, " &
                "ISNULL(ApproveBy, 1) AS ApproveBy, " &
                "ISNULL(ManagerCode, '') AS ManagerCode, " &
                "ISNULL(ManagerName, '') AS ManagerName, " &
                "IsActive " &
                "FROM exmUnits ORDER BY UnitName"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            If Not dt.Columns.Contains("RowNumber") Then
                dt.Columns.Add("RowNumber", GetType(Integer))
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            gvUnits.DataSource = dt
            gvUnits.DataBind()

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ذخیره واحد (افزودن یا ویرایش)
    ' ============================================================
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim unitName As String = txtUnitName.Text.Trim()
            Dim unitCode As String = txtUnitCode.Text.Trim()
            Dim deadlinePersian As String = txtDeadline.Text.Trim()
            Dim isActive As Boolean = chkIsActive.Checked
            Dim editID As Integer = Convert.ToInt32(hfEditID.Value)

            ' ====== خواندن فیلدهای جدید ======
            Dim approveBy As Integer = 1
            If rdoApproveByManager.Checked Then
                approveBy = 2
            End If

            Dim managerCode As String = txtManagerCode.Text.Trim()
            Dim managerName As String = txtManagerName.Text.Trim()

            ' ====== اعتبارسنجی ======
            If String.IsNullOrEmpty(unitName) Then
                ShowMessage("نام واحد را وارد کنید", "danger")
                Return
            End If

            ' ====== اعتبارسنجی رییس واحد ======
            If approveBy = 2 AndAlso String.IsNullOrEmpty(managerCode) Then
                ShowMessage("در حالت «تایید توسط رییس واحد»، کد پرسنلی رییس الزامی است.", "danger")
                Return
            End If

            ' ====== تبدیل تاریخ مهلت ======
            Dim deadlineGregorian As Object = DBNull.Value
            If Not String.IsNullOrEmpty(deadlinePersian) Then
                Dim gregorianStr As String = ConvertPersianDateToGregorian(deadlinePersian)
                If Not String.IsNullOrEmpty(gregorianStr) Then
                    deadlineGregorian = gregorianStr
                Else
                    ShowMessage("فرمت تاریخ مهلت صحیح نیست (مثال: 1405/06/30)", "danger")
                    Return
                End If
            End If

            Using conn As New SqlConnection(ClsConnection.GetConn)
                conn.Open()

                If editID = 0 Then
                    ' ====== درج واحد جدید ======
                    Dim sql As String = "INSERT INTO exmUnits " &
                        "(UnitName, UnitCode, DeadlineDate, IsActive, ApproveBy, ManagerCode, ManagerName, CreatedDate, CreatedBy) " &
                        "VALUES " &
                        "(@UnitName, @UnitCode, @DeadlineDate, @IsActive, @ApproveBy, @ManagerCode, @ManagerName, GETDATE(), @CreatedBy)"

                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@UnitName", unitName)
                        cmd.Parameters.AddWithValue("@UnitCode", If(String.IsNullOrEmpty(unitCode), DBNull.Value, unitCode))
                        cmd.Parameters.AddWithValue("@DeadlineDate", deadlineGregorian)
                        cmd.Parameters.AddWithValue("@IsActive", isActive)
                        cmd.Parameters.AddWithValue("@ApproveBy", approveBy)
                        cmd.Parameters.AddWithValue("@ManagerCode", If(String.IsNullOrEmpty(managerCode), DBNull.Value, managerCode))
                        cmd.Parameters.AddWithValue("@ManagerName", If(String.IsNullOrEmpty(managerName), DBNull.Value, managerName))
                        cmd.Parameters.AddWithValue("@CreatedBy", If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), "admin"))
                        cmd.ExecuteNonQuery()
                    End Using

                    ShowMessage("واحد با موفقیت اضافه شد", "success")

                Else
                    ' ====== ویرایش واحد ======
                    Dim sql As String = "UPDATE exmUnits SET " &
                        "UnitName = @UnitName, " &
                        "UnitCode = @UnitCode, " &
                        "DeadlineDate = @DeadlineDate, " &
                        "IsActive = @IsActive, " &
                        "ApproveBy = @ApproveBy, " &
                        "ManagerCode = @ManagerCode, " &
                        "ManagerName = @ManagerName, " &
                        "UpdatedDate = GETDATE() " &
                        "WHERE ID = @ID"

                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@UnitName", unitName)
                        cmd.Parameters.AddWithValue("@UnitCode", If(String.IsNullOrEmpty(unitCode), DBNull.Value, unitCode))
                        cmd.Parameters.AddWithValue("@DeadlineDate", deadlineGregorian)
                        cmd.Parameters.AddWithValue("@IsActive", isActive)
                        cmd.Parameters.AddWithValue("@ApproveBy", approveBy)
                        cmd.Parameters.AddWithValue("@ManagerCode", If(String.IsNullOrEmpty(managerCode), DBNull.Value, managerCode))
                        cmd.Parameters.AddWithValue("@ManagerName", If(String.IsNullOrEmpty(managerName), DBNull.Value, managerName))
                        cmd.Parameters.AddWithValue("@ID", editID)
                        cmd.ExecuteNonQuery()
                    End Using

                    ShowMessage("واحد با موفقیت ویرایش شد", "success")
                End If
            End Using

            ClearForm()
            LoadUnits()

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دستورات GridView
    ' ============================================================
    Protected Sub gvUnits_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)

            If e.CommandName = "EditUnit" Then
                LoadForEdit(id)
            ElseIf e.CommandName = "ToggleUnit" Then
                ToggleActive(id)
            ElseIf e.CommandName = "DeleteUnit" Then
                DeleteUnit(id)
            End If
        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری برای ویرایش
    ' ============================================================
    Private Sub LoadForEdit(id As Integer)
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Dim sql As String = "SELECT * FROM exmUnits WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            hfEditID.Value = id.ToString()
                            txtUnitName.Text = reader("UnitName").ToString()
                            txtUnitCode.Text = reader("UnitCode").ToString()

                            ' ====== تاریخ مهلت ======
                            If reader("DeadlineDate") IsNot DBNull.Value Then
                                Dim deadlineDate As DateTime = Convert.ToDateTime(reader("DeadlineDate"))
                                txtDeadline.Text = ConvertGregorianToPersian(deadlineDate)
                            Else
                                txtDeadline.Text = ""
                            End If

                            ' ====== ApproveBy ======
                            Dim approveBy As Integer = 1
                            If reader("ApproveBy") IsNot DBNull.Value Then
                                approveBy = Convert.ToInt32(reader("ApproveBy"))
                            End If

                            If approveBy = 2 Then
                                rdoApproveByManager.Checked = True
                                rdoApproveBySupervisor.Checked = False
                            Else
                                rdoApproveBySupervisor.Checked = True
                                rdoApproveByManager.Checked = False
                            End If

                            ' ====== ManagerCode و ManagerName ======
                            If reader("ManagerCode") IsNot DBNull.Value Then
                                txtManagerCode.Text = reader("ManagerCode").ToString()
                            Else
                                txtManagerCode.Text = ""
                            End If

                            If reader("ManagerName") IsNot DBNull.Value Then
                                txtManagerName.Text = reader("ManagerName").ToString()
                            Else
                                txtManagerName.Text = ""
                            End If

                            chkIsActive.Checked = Convert.ToBoolean(reader("IsActive"))

                            lblFormTitle.Text = "✏️ ویرایش واحد"
                        End If
                    End Using
                End Using
            End Using

            ' ====== به‌روزرسانی پنل رییس ======
            Dim script As String = "toggleManagerPanel();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toggleManagerPanelScript", script, True)

            ShowMessage("در حالت ویرایش - تغییرات را اعمال کنید", "warning")

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' فعال/غیرفعال کردن واحد
    ' ============================================================
    Private Sub ToggleActive(id As Integer)
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Dim sql As String = "UPDATE exmUnits SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END, UpdatedDate = GETDATE() WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowMessage("وضعیت با موفقیت تغییر کرد", "success")
            LoadUnits()

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' حذف واحد
    ' ============================================================
    Private Sub DeleteUnit(id As Integer)
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                conn.Open()

                ' ====== بررسی استفاده در ظرفیت‌ها ======
                Dim checkSql As String = "SELECT COUNT(*) FROM exmUnitCapacity WHERE UnitID = @ID"
                Using cmd As New SqlCommand(checkSql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count > 0 Then
                        ShowMessage("این واحد در ظرفیت‌ها استفاده شده و قابل حذف نیست. ابتدا ظرفیت‌های مربوطه را حذف کنید.", "danger")
                        Return
                    End If
                End Using

                ' ====== بررسی استفاده در درخواست‌ها ======
                Dim checkReqSql As String = "SELECT COUNT(*) FROM exmReservationRequests WHERE UnitID = @ID"
                Using cmd As New SqlCommand(checkReqSql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If count > 0 Then
                        ShowMessage("این واحد در درخواست‌های رزرو استفاده شده و قابل حذف نیست.", "danger")
                        Return
                    End If
                End Using

                ' ====== حذف واحد ======
                Dim sql As String = "DELETE FROM exmUnits WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowMessage("واحد با موفقیت حذف شد", "success")
            LoadUnits()

        Catch ex As Exception
            ShowMessage("خطا در حذف: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' انصراف
    ' ============================================================
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("ExaminationSetCapacity.aspx")
    End Sub

    ' ============================================================
    ' پاکسازی فرم
    ' ============================================================
    Private Sub ClearForm()
        txtUnitName.Text = ""
        txtUnitCode.Text = ""
        txtDeadline.Text = ""
        txtManagerCode.Text = ""
        txtManagerName.Text = ""
        chkIsActive.Checked = True
        rdoApproveBySupervisor.Checked = True
        rdoApproveByManager.Checked = False
        hfEditID.Value = "0"
        lblFormTitle.Text = "➕ افزودن واحد جدید"
        pnlMessage.Visible = False

        ' ====== مخفی کردن پنل رییس ======
        Dim script As String = "toggleManagerPanel();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toggleManagerPanelScript", script, True)
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(msg As String, type As String)
        pnlMessage.Visible = True
        lblMessage.Text = msg
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅ "
                divMessage.Attributes("class") = "alert alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌ "
                divMessage.Attributes("class") = "alert alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️ "
                divMessage.Attributes("class") = "alert alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️ "
                divMessage.Attributes("class") = "alert alert-info"
        End Select
    End Sub

End Class