Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Globalization

Partial Class ChildAmountManagement
    Inherits System.Web.UI.Page

    Private Const ACADEMIC_YEAR As String = "1405-1404"
    Private Const STORAGE_KEY As String = "PendingAmountChanges"

    Private _personCode As String = ""
    Private _fullName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("codePersoneli") Is Nothing Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        _personCode = Session("codePersoneli").ToString().Trim()
        _fullName = If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), "")

        If Not IsPublicRelationsUser() Then
            Response.Redirect("~/index.aspx")
            Return
        End If

        If Not IsPostBack Then
            LoadCurrentVersionInfo()
            LoadAmounts()
            LoadArchive()

            Dim changes As Dictionary(Of Integer, Decimal) = GetPendingChanges()
            If changes.Count > 0 Then
                hfPendingChanges.Value = SerializeChanges(changes)
                pnlPendingChanges.Visible = True
                lblPendingCount.Text = changes.Count.ToString()
            End If
        End If

        ' ============================================================
        ' ✅ مدیریت وضعیت Modal
        ' ============================================================

        ' ✅ اولویت ۱: بررسی سیگنال بستن از Session
        If Session("CloseEditModal") IsNot Nothing AndAlso Session("CloseEditModal").ToString() = "1" Then
            hfCloseEditModal.Value = "1"
            hfOpenEditModal.Value = "0"
            Session.Remove("CloseEditModal")
            System.Diagnostics.Debug.WriteLine(">>> Page_Load: Should close modal")
        End If
        ' ✅ اولویت ۲: اگر rptAmounts_ItemCommand اجرا شده (hfOpen=1)، آن را حفظ کن
        ' (هیچ کاری نکن — hfOpen از قبل توسط rptAmounts_ItemCommand ست شده)
        ' ✅ اولویت ۳: اگر نه، همه چیز صفر
        ' (در IsPostBack های دیگر)

        ' سیگنال نمایش Toast
        If Session("ShowEditToast") IsNot Nothing AndAlso Session("ShowEditToast").ToString() = "1" Then
            hfShowToast.Value = "1"
            hfToastType.Value = "success"
            hfToastMessage.Value = If(Session("EditToastMessage") IsNot Nothing, Session("EditToastMessage").ToString(), "")
            Session.Remove("ShowEditToast")
            Session.Remove("EditToastMessage")
        End If
    End Sub

    Private Function IsPublicRelationsUser() As Boolean
        Try
            Dim sql As String =
                "SELECT COUNT(*) FROM personel p " &
                "INNER JOIN omurTbl o ON p.omurID = o.omurID " &
                "WHERE p.codePersoneli = @Code AND (o.omur LIKE N'%روابط عمومی%' OR o.omur LIKE N'%روابط عمومي%')"
            Dim result As Object = ExecuteScalar(sql, New SqlParameter("@Code", _personCode))
            Return Convert.ToInt32(result) > 0
        Catch
            Return False
        End Try
    End Function

    Private Function GetDataTable(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Dim paramDict As New Dictionary(Of String, Object)()
        For Each p As SqlParameter In parameters
            paramDict.Add(p.ParameterName, p.Value)
        Next
        Return ClsDataTable.GetDataTable(sql, paramDict)
    End Function

    Private Function ExecuteScalar(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As Object
        Using cnn As New SqlConnection(ClsConnection.GetConn)
            Using cmd As New SqlCommand(sql, cnn)
                For Each p As SqlParameter In parameters
                    cmd.Parameters.Add(p)
                Next
                cnn.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Private Function ExecuteNonQuery(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As Integer
        Using cnn As New SqlConnection(ClsConnection.GetConn)
            Using cmd As New SqlCommand(sql, cnn)
                For Each p As SqlParameter In parameters
                    cmd.Parameters.Add(p)
                Next
                cnn.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Private Sub LoadCurrentVersionInfo()
        Try
            lblCurrentYear.Text = ACADEMIC_YEAR

            Dim dt As DataTable = GetDataTable(
                "SELECT TOP 1 VersionNumber, CreatedByName, CreatedDate " &
                "FROM exmChildAmountVersions " &
                "WHERE AcademicYear = @Year AND IsActive = 1 " &
                "ORDER BY VersionNumber DESC",
                New SqlParameter("@Year", ACADEMIC_YEAR))

            If dt.Rows.Count > 0 Then
                lblCurrentVersion.Text = dt.Rows(0)("VersionNumber").ToString()
                lblLastEditor.Text = If(IsDBNull(dt.Rows(0)("CreatedByName")), "-", dt.Rows(0)("CreatedByName").ToString())
                lblLastEditDate.Text = If(IsDBNull(dt.Rows(0)("CreatedDate")), "-", GetPersianDateTime(Convert.ToDateTime(dt.Rows(0)("CreatedDate"))))
            Else
                lblCurrentVersion.Text = "1"
                lblLastEditor.Text = "—"
                lblLastEditDate.Text = "—"
            End If
        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری اطلاعات نسخه: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadAmounts()
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT ID, RowNumber, EducationLevel, ISNULL(Amount, 0) AS Amount " &
                "FROM exmChildConditions " &
                "WHERE IsActive = 1 " &
                "ORDER BY RowNumber")

            If dt.Rows.Count = 0 Then
                pnlAmounts.Visible = False
                pnlNoAmounts.Visible = True
                Return
            End If

            Dim pendingChanges As Dictionary(Of Integer, Decimal) = GetPendingChanges()

            dt.Columns.Add("HasPendingChange", GetType(Boolean))
            dt.Columns.Add("DisplayAmount", GetType(Decimal))

            For Each row As DataRow In dt.Rows
                Dim id As Integer = Convert.ToInt32(row("ID"))
                Dim displayAmount As Decimal = 0

                If Not IsDBNull(row("Amount")) Then
                    Decimal.TryParse(row("Amount").ToString(), displayAmount)
                End If

                If pendingChanges.ContainsKey(id) Then
                    row("HasPendingChange") = True
                    row("DisplayAmount") = pendingChanges(id)
                Else
                    row("HasPendingChange") = False
                    row("DisplayAmount") = displayAmount
                End If
            Next

            rptAmounts.DataSource = dt
            rptAmounts.DataBind()

            pnlAmounts.Visible = True
            pnlNoAmounts.Visible = False

            If pendingChanges.Count > 0 Then
                pnlPendingChanges.Visible = True
                lblPendingCount.Text = pendingChanges.Count.ToString()
            Else
                pnlPendingChanges.Visible = False
            End If

        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری مبالغ: " & ex.Message)
        End Try
    End Sub

    Private Function GetPendingChanges() As Dictionary(Of Integer, Decimal)
        Dim changes As New Dictionary(Of Integer, Decimal)()
        Try
            If Session(STORAGE_KEY) IsNot Nothing Then
                Dim stored As String = Session(STORAGE_KEY).ToString()
                If Not String.IsNullOrEmpty(stored) Then
                    For Each item As String In stored.Split("|"c)
                        Dim parts As String() = item.Split(":"c)
                        If parts.Length = 2 Then
                            Dim id As Integer = 0
                            Dim amount As Decimal = 0
                            If Integer.TryParse(parts(0), id) AndAlso _
                               Decimal.TryParse(parts(1), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, amount) Then
                                changes(id) = amount
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("GetPendingChanges Error: " & ex.Message)
        End Try
        Return changes
    End Function

    Private Function SerializeChanges(ByVal changes As Dictionary(Of Integer, Decimal)) As String
        Dim sb As New StringBuilder()
        For Each kvp As KeyValuePair(Of Integer, Decimal) In changes
            If sb.Length > 0 Then sb.Append("|")
            sb.Append(kvp.Key.ToString() & ":" & kvp.Value.ToString(System.Globalization.CultureInfo.InvariantCulture))
        Next
        Return sb.ToString()
    End Function

    Private Sub SavePendingChanges(ByVal changes As Dictionary(Of Integer, Decimal))
        Try
            Dim serialized As String = SerializeChanges(changes)
            Session(STORAGE_KEY) = serialized
            hfPendingChanges.Value = serialized
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("SavePendingChanges Error: " & ex.Message)
        End Try
    End Sub

    Protected Function GetAmountDisplay(ByVal amount As Object) As String
        Try
            If amount Is Nothing OrElse IsDBNull(amount) Then
                Return "<span class=""am-amount-empty"">تعیین نشده</span>"
            End If
            Dim val As Decimal = 0
            If Decimal.TryParse(amount.ToString(), val) AndAlso val > 0 Then
                Return val.ToString("#,##0")
            End If
            Return "<span class=""am-amount-empty"">تعیین نشده</span>"
        Catch
            Return "-"
        End Try
    End Function

    Protected Function GetAmountWords(ByVal amount As Object) As String
        Try
            If amount Is Nothing OrElse IsDBNull(amount) Then Return "—"
            Dim val As Decimal = 0
            If Decimal.TryParse(amount.ToString(), val) AndAlso val > 0 Then
                Return ConvertNumberToPersianWords(val)
            End If
            Return "—"
        Catch
            Return "—"
        End Try
    End Function

    Protected Function GetDiffDisplay(ByVal prev As Object, ByVal curr As Object) As String
        Try
            Dim p As Decimal = 0, c As Decimal = 0
            If prev IsNot Nothing AndAlso Not IsDBNull(prev) Then Decimal.TryParse(prev.ToString(), p)
            If curr IsNot Nothing AndAlso Not IsDBNull(curr) Then Decimal.TryParse(curr.ToString(), c)

            Dim diff As Decimal = c - p
            If diff > 0 Then
                Return "<span style='color:#27ae60;'>▲ +" & diff.ToString("#,##0") & "</span>"
            ElseIf diff < 0 Then
                Return "<span style='color:#e74c3c;'>▼ " & diff.ToString("#,##0") & "</span>"
            Else
                Return "<span style='color:#6c757d;'>— بدون تغییر</span>"
            End If
        Catch
            Return "-"
        End Try
    End Function

    Private Sub LoadArchive()
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT ID, VersionNumber, Title, Description, CreatedByName, CreatedDate " &
                "FROM exmChildAmountVersions " &
                "WHERE AcademicYear = @Year AND IsActive = 1 " &
                "ORDER BY VersionNumber DESC",
                New SqlParameter("@Year", ACADEMIC_YEAR))

            lblArchiveCount.Text = dt.Rows.Count.ToString()

            If dt.Rows.Count > 0 Then
                rptArchive.DataSource = dt
                rptArchive.DataBind()
                pnlArchive.Visible = True
                pnlNoArchive.Visible = False
            Else
                pnlArchive.Visible = False
                pnlNoArchive.Visible = True
            End If
        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری آرشیو: " & ex.Message)
        End Try
    End Sub

    Protected Sub rptArchive_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        Try
            If e.CommandName <> "ViewVersion" Then Return

            Dim versionID As Integer = 0
            If Not Integer.TryParse(e.CommandArgument.ToString(), versionID) Then Return

            LoadVersionDetail(versionID)
            upMain.Update()
        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری نسخه: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadVersionDetail(ByVal versionID As Integer)
        Try
            Dim dtVersion As DataTable = GetDataTable(
                "SELECT VersionNumber, Title FROM exmChildAmountVersions WHERE ID = @ID",
                New SqlParameter("@ID", versionID))

            If dtVersion.Rows.Count = 0 Then Return

            lblVersionDetailNumber.Text = dtVersion.Rows(0)("VersionNumber").ToString()
            lblVersionDetailTitle.Text = dtVersion.Rows(0)("Title").ToString()

            Dim dt As DataTable = GetDataTable(
                "SELECT a.ConditionID, a.EducationLevel, a.Amount, a.PreviousAmount, " &
                "       ISNULL(c.RowNumber, 0) AS RowNumber " &
                "FROM exmChildAmountArchive a " &
                "LEFT JOIN exmChildConditions c ON a.ConditionID = c.ID " &
                "WHERE a.VersionNumber = @VersionNumber AND a.AcademicYear = @Year " &
                "ORDER BY c.RowNumber",
                New SqlParameter("@VersionNumber", dtVersion.Rows(0)("VersionNumber")),
                New SqlParameter("@Year", ACADEMIC_YEAR))

            rptVersionDetail.DataSource = dt
            rptVersionDetail.DataBind()

            pnlVersionDetail.Visible = True

        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری جزئیات: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ رخداد: کلیک روی ✏️ ویرایش
    ' ============================================================
    Protected Sub rptAmounts_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        Try
            If e.CommandName <> "EditAmount" Then Return

            Dim conditionID As Integer = 0
            Integer.TryParse(e.CommandArgument.ToString(), conditionID)

            If conditionID = 0 Then
                ShowAlert("error", "خطا", "شناسه معتبر نیست.")
                Return
            End If

            Dim dt As DataTable = GetDataTable(
                "SELECT ID, EducationLevel, ISNULL(Amount, 0) AS Amount FROM exmChildConditions WHERE ID = @ID",
                New SqlParameter("@ID", conditionID))

            If dt.Rows.Count = 0 Then
                ShowAlert("error", "خطا", "مقطع یافت نشد.")
                Return
            End If

            Dim row As DataRow = dt.Rows(0)
            Dim currentAmount As Decimal = 0
            If Not IsDBNull(row("Amount")) Then Decimal.TryParse(row("Amount").ToString(), currentAmount)

            Dim changes As Dictionary(Of Integer, Decimal) = GetPendingChanges()
            If changes.ContainsKey(conditionID) Then
                currentAmount = changes(conditionID)
            End If

            Dim levelName As String = row("EducationLevel").ToString()
            Dim prevAmountText As String = If(currentAmount > 0, currentAmount.ToString("#,##0") & " ریال", "تعیین نشده")

            Session("EditingConditionID") = conditionID
            Session("EditingLevelName") = levelName
            Session("EditingPreviousAmount") = prevAmountText

            hfEditConditionID.Value = conditionID.ToString()
            hfEditLevelName.Value = levelName
            hfEditPreviousAmount.Value = prevAmountText

            hfOpenEditModal.Value = "1"

            upMain.Update()

        Catch ex As Exception
            ShowAlert("error", "خطا", ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ رخداد: کلیک روی «💾 ثبت تغییر»
    ' ============================================================
    Protected Sub btnSaveEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' ✅ خواندن شناسه
            Dim conditionID As Integer = 0

            If Request.Form("dataConditionID") IsNot Nothing Then
                Integer.TryParse(Request.Form("dataConditionID").ToString(), conditionID)
            End If

            If conditionID = 0 AndAlso Session("EditingConditionID") IsNot Nothing Then
                Integer.TryParse(Session("EditingConditionID").ToString(), conditionID)
            End If

            If conditionID = 0 AndAlso Not String.IsNullOrEmpty(hfEditConditionID.Value) Then
                Integer.TryParse(hfEditConditionID.Value, conditionID)
            End If

            If conditionID = 0 Then
                ShowAlert("error", "خطا", "شناسه مقطع معتبر نیست.")
                Return
            End If

            ' ✅ خواندن مبلغ
            Dim amountText As String = ""

            If Request.Form("dataAmount") IsNot Nothing Then
                amountText = Request.Form("dataAmount").ToString().Trim()
            End If

            amountText = amountText.Replace(",", "").Replace("،", "").Replace(" ", "").Replace(".", "")

            If String.IsNullOrEmpty(amountText) Then
                ShowAlert("warning", "مبلغ الزامی", "لطفاً مبلغ جدید را وارد کنید.")
                Return
            End If

            Dim newAmount As Decimal = 0
            If Not Decimal.TryParse(amountText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, newAmount) Then
                ShowAlert("warning", "مبلغ نامعتبر", "لطفاً مبلغ را به‌صورت عددی وارد کنید.")
                Return
            End If

            If newAmount < 0 Then
                ShowAlert("warning", "مبلغ نامعتبر", "مبلغ نمی‌تواند منفی باشد.")
                Return
            End If

            ' ✅ ذخیره در Session
            Dim changes As Dictionary(Of Integer, Decimal) = GetPendingChanges()
            changes(conditionID) = newAmount
            SavePendingChanges(changes)

            LoadAmounts()

            ' ✅✅✅ سیگنال بستن Modal از طریق Session
            Session("CloseEditModal") = "1"
            Session("ShowEditToast") = "1"
            Session("EditToastMessage") = "تغییر مبلغ در لیست انتظار قرار گرفت. برای اعمال، دکمه «ذخیره همه تغییرات» را بزنید."

            ' ✅ پاک کردن Session مرتبط
            Session.Remove("EditingConditionID")
            Session.Remove("EditingLevelName")
            Session.Remove("EditingPreviousAmount")

            upMain.Update()

        Catch ex As Exception
            ShowAlert("error", "خطا", ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ رخداد: کلیک روی «❌ انصراف»
    ' ============================================================
    Protected Sub btnCancelEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' ✅✅✅ سیگنال بستن Modal از طریق Session
            Session("CloseEditModal") = "1"

            ' ✅ پاک کردن Session مرتبط
            Session.Remove("EditingConditionID")
            Session.Remove("EditingLevelName")
            Session.Remove("EditingPreviousAmount")

            upMain.Update()

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ رخداد: کلیک روی «💾 ذخیره همه تغییرات»
    ' ============================================================
    Protected Sub btnSaveAll_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim changes As Dictionary(Of Integer, Decimal) = GetPendingChanges()

            If changes.Count = 0 Then
                ShowToast("warning", "هیچ تغییر جدیدی برای ذخیره وجود ندارد.")
                upMain.Update()
                Return
            End If

            Dim currentAmounts As New Dictionary(Of Integer, Decimal)()
            Dim levels As New Dictionary(Of Integer, String)()

            Dim ids As String = String.Join(",", changes.Keys.ToArray())
            Dim dtCurrent As DataTable = GetDataTable(
                "SELECT ID, EducationLevel, ISNULL(Amount, 0) AS Amount " &
                "FROM exmChildConditions WHERE ID IN (" & ids & ")")

            For Each row As DataRow In dtCurrent.Rows
                Dim id As Integer = Convert.ToInt32(row("ID"))
                Dim amt As Decimal = 0
                If Not IsDBNull(row("Amount")) Then Decimal.TryParse(row("Amount").ToString(), amt)
                currentAmounts(id) = amt
                levels(id) = row("EducationLevel").ToString()
            Next

            Dim newVersionNumber As Integer = GetNextVersionNumber()
            Dim versionID As Integer = InsertVersion(newVersionNumber, changes.Count)

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                cnn.Open()
                Using transaction As SqlTransaction = cnn.BeginTransaction()
                    Try
                        For Each kvp As KeyValuePair(Of Integer, Decimal) In changes
                            Dim conditionID As Integer = kvp.Key
                            Dim newAmount As Decimal = kvp.Value
                            Dim prevAmount As Decimal = If(currentAmounts.ContainsKey(conditionID), currentAmounts(conditionID), 0)
                            Dim levelName As String = If(levels.ContainsKey(conditionID), levels(conditionID), "")

                            Using cmdArc As New SqlCommand(
                                "INSERT INTO exmChildAmountArchive " &
                                "(VersionNumber, AcademicYear, ConditionID, EducationLevel, Amount, PreviousAmount, " &
                                " ChangeType, ChangedBy, ChangedByName, ChangeDate, IsActiveVersion) " &
                                "VALUES (@VersionNumber, @Year, @ConditionID, @Level, @Amount, @PreviousAmount, " &
                                " 'Update', @ChangedBy, @ChangedByName, GETDATE(), 1)", cnn, transaction)

                                cmdArc.Parameters.AddWithValue("@VersionNumber", newVersionNumber)
                                cmdArc.Parameters.AddWithValue("@Year", ACADEMIC_YEAR)
                                cmdArc.Parameters.AddWithValue("@ConditionID", conditionID)
                                cmdArc.Parameters.AddWithValue("@Level", levelName)
                                cmdArc.Parameters.AddWithValue("@Amount", newAmount)
                                cmdArc.Parameters.AddWithValue("@PreviousAmount", prevAmount)
                                cmdArc.Parameters.AddWithValue("@ChangedBy", _personCode)
                                cmdArc.Parameters.AddWithValue("@ChangedByName", _fullName)
                                cmdArc.ExecuteNonQuery()
                            End Using

                            Using cmdUpd As New SqlCommand(
                                "UPDATE exmChildConditions SET Amount = @Amount WHERE ID = @ID", cnn, transaction)
                                cmdUpd.Parameters.AddWithValue("@Amount", newAmount)
                                cmdUpd.Parameters.AddWithValue("@ID", conditionID)
                                cmdUpd.ExecuteNonQuery()
                            End Using
                        Next

                        transaction.Commit()
                    Catch
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            Session.Remove(STORAGE_KEY)
            hfPendingChanges.Value = ""

            LoadCurrentVersionInfo()
            LoadAmounts()
            LoadArchive()

            ShowAlert("success", "✅ ذخیره موفق",
                "تغییرات با موفقیت ذخیره شد.<br/><br/>" &
                "📦 <strong>نسخه " & newVersionNumber & "</strong> ساخته شد.<br/>" &
                "👤 کاربر: " & _fullName & "<br/>" &
                "🕐 تاریخ: " & GetPersianDateTime(DateTime.Now))

            upMain.Update()

        Catch ex As Exception
            ShowAlert("error", "خطا در ذخیره", ex.Message)
            upMain.Update()
        End Try
    End Sub

    Private Function GetNextVersionNumber() As Integer
        Try
            Dim result As Object = ExecuteScalar(
                "SELECT ISNULL(MAX(VersionNumber), 0) + 1 FROM exmChildAmountVersions WHERE AcademicYear = @Year",
                New SqlParameter("@Year", ACADEMIC_YEAR))
            Return Convert.ToInt32(result)
        Catch
            Return 1
        End Try
    End Function

    Private Function InsertVersion(ByVal versionNumber As Integer, ByVal itemCount As Integer) As Integer
        Try
            Dim sql As String =
                "INSERT INTO exmChildAmountVersions " &
                "(VersionNumber, AcademicYear, Title, Description, CreatedBy, CreatedByName, IsActive) " &
                "VALUES (@VersionNumber, @Year, @Title, @Description, @CreatedBy, @CreatedByName, 1); " &
                "SELECT SCOPE_IDENTITY();"

            Dim result As Object = ExecuteScalar(sql,
                New SqlParameter("@VersionNumber", versionNumber),
                New SqlParameter("@Year", ACADEMIC_YEAR),
                New SqlParameter("@Title", "نسخه " & versionNumber & " — " & GetPersianDate(DateTime.Now)),
                New SqlParameter("@Description", "تغییر " & itemCount & " مبلغ"),
                New SqlParameter("@CreatedBy", _personCode),
                New SqlParameter("@CreatedByName", _fullName))

            Return Convert.ToInt32(result)
        Catch
            Return 0
        End Try
    End Function

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session(STORAGE_KEY) IsNot Nothing Then
                Session.Remove(STORAGE_KEY)
                hfPendingChanges.Value = ""
                ShowToast("info", "تغییرات در انتظار پاک شد.")
            End If

            LoadCurrentVersionInfo()
            LoadAmounts()
            LoadArchive()

            upMain.Update()

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    Protected Sub lnkBackToHome_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/index.aspx")
    End Sub

    Private Function ConvertNumberToPersianWords(ByVal number As Decimal) As String
        Try
            Dim n As Long = CLng(Math.Floor(number))
            If n = 0 Then Return "صفر"

            Dim yekan As String() = {"", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه"}
            Dim dahgan As String() = {"", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود"}
            Dim dahYek As String() = {"ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده"}
            Dim sadgan As String() = {"", "صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد"}
            Dim scales As String() = {"", " هزار", " میلیون", " میلیارد", " بیلیون"}

            Dim result As String = ""
            Dim scaleIndex As Integer = 0

            While n > 0
                Dim part As Integer = CInt(n Mod 1000)
                n = n \ 1000

                If part > 0 Then
                    Dim partText As String = ""

                    Dim s As Integer = part \ 100
                    If s > 0 Then partText &= sadgan(s)

                    Dim remainder As Integer = part Mod 100
                    If remainder > 0 Then
                        If partText <> "" Then partText &= " و "

                        If remainder < 10 Then
                            partText &= yekan(remainder)
                        ElseIf remainder < 20 Then
                            partText &= dahYek(remainder - 10)
                        Else
                            Dim d As Integer = remainder \ 10
                            Dim y As Integer = remainder Mod 10
                            partText &= dahgan(d)
                            If y > 0 Then partText &= " و " & yekan(y)
                        End If
                    End If

                    If result <> "" Then
                        result = partText & scales(scaleIndex) & " و " & result
                    Else
                        result = partText & scales(scaleIndex)
                    End If
                End If

                scaleIndex += 1
            End While

            Return result.Trim()
        Catch
            Return ""
        End Try
    End Function

    Protected Function GetPersianDateTime(ByVal dt As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" &
                   pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" &
                   pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c) & " - " &
                   dt.ToString("HH:mm")
        Catch
            Return dt.ToString("yyyy/MM/dd HH:mm")
        End Try
    End Function

    Protected Function GetPersianDate(ByVal dt As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" &
                   pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" &
                   pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c)
        Catch
            Return dt.ToString("yyyy/MM/dd")
        End Try
    End Function

    Private Sub ShowToast(ByVal type As String, ByVal message As String)
        hfShowToast.Value = "1"
        hfToastType.Value = type
        hfToastMessage.Value = message
    End Sub

    Private Sub ShowAlert(ByVal type As String, ByVal title As String, ByVal message As String)
        hfShowAlert.Value = "1"
        hfAlertType.Value = type
        hfAlertTitle.Value = title
        hfAlertMessage.Value = message
    End Sub

End Class