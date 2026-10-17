Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO

Partial Class ChildRequest
    Inherits System.Web.UI.Page

    Private Const ACADEMIC_YEAR As String = "1405-1404"
    Private Const TOLERANCE_VALUE As Decimal = 0.5D
    Private Const UPLOAD_FOLDER As String = "~/Uploads/ChildRequests/"
    Private Const TEMP_FOLDER As String = "~/Uploads/Temp/"
    Private Const MAX_FILE_SIZE As Integer = 2097152
    Private Shared ReadOnly ALLOWED_EXTENSIONS As String() = {".jpg", ".jpeg", ".png", ".pdf"}
    Private Shared ReadOnly INVALID_FILENAME_CHARS As Char() = {"\"c, "/"c, ":"c, "*"c, "?"c, """"c, "<"c, ">"c, "|"c, " "c, Chr(9)}

    Private _personCode As String = ""
    Private _fullName As String = ""
    Private _unitName As String = ""
    Private _mobile As String = ""

    Private Property RequestSubmitted As Boolean
        Get
            If ViewState("RequestSubmitted") Is Nothing Then Return False
            Return CBool(ViewState("RequestSubmitted"))
        End Get
        Set(value As Boolean)
            ViewState("RequestSubmitted") = value
        End Set
    End Property

    ' ============================================================
    ' ✅ توابع کمکی برای نمایش/مخفی کردن پنل با CSS Class
    ' ============================================================
    Private Sub ShowPanel(ByVal pnl As Panel, ByVal baseClass As String)
        If pnl Is Nothing Then Return
        pnl.CssClass = baseClass
    End Sub

    Private Sub HidePanel(ByVal pnl As Panel, ByVal baseClass As String)
        If pnl Is Nothing Then Return
        pnl.CssClass = baseClass & " chr-hidden"
    End Sub

    ' ============================================================
    ' Page Lifecycle
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("codePersoneli") Is Nothing Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        _personCode = Session("codePersoneli").ToString().Trim()
        _fullName = If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), "")
        _mobile = If(Session("mobile") IsNot Nothing, Session("mobile").ToString(), "")
        _unitName = GetUnitNameByPersonCode(_personCode)

        If Not IsPostBack Then
            LoadDeadline()
            LoadChildren()
            LoadHistory()
        End If
    End Sub

    Private Function GetUnitNameByPersonCode(ByVal personCode As String) As String
        Try
            If String.IsNullOrEmpty(personCode) Then Return ""
            Dim sql As String =
                "SELECT omurTbl.omur " &
                "FROM omurTbl " &
                "INNER JOIN personel ON omurTbl.omurID = personel.omurID " &
                "WHERE personel.codePersoneli = @Code"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@Code", personCode))
            If dt.Rows.Count > 0 AndAlso dt.Rows(0)("omur") IsNot DBNull.Value Then
                Return dt.Rows(0)("omur").ToString().Trim()
            End If
            Return ""
        Catch
            Return ""
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

    Private Function GenerateFileName(ByVal personCode As String, ByVal childName As String,
                                       ByVal docType As String, ByVal ext As String) As String
        Try
            Dim safeChildName As String = SanitizeFileName(childName)
            Dim safeDocType As String = SanitizeFileName(docType)
            Dim pc As New PersianCalendar()
            Dim now As DateTime = DateTime.Now
            Dim persianDate As String = pc.GetYear(now).ToString("0000") &
                                        pc.GetMonth(now).ToString("00") &
                                        pc.GetDayOfMonth(now).ToString("00")
            Dim timePart As String = now.ToString("HHmmss")
            Dim fileName As String = personCode & "_" & safeChildName & "_" & safeDocType & "_" &
                                     persianDate & "-" & timePart & ext
            If fileName.Length > 200 Then fileName = fileName.Substring(0, 195) & ext
            Return fileName
        Catch
            Return personCode & "_" & Guid.NewGuid().ToString("N") & ext
        End Try
    End Function

    Private Function SanitizeFileName(ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return "-"
        Dim result As String = value.Trim()
        For Each c As Char In INVALID_FILENAME_CHARS
            result = result.Replace(c, "-"c)
        Next
        Return result
    End Function

    Private Sub ClearTempFiles()
        Session.Remove("TempTranscript_" & _personCode & "_Path")
        Session.Remove("TempTranscript_" & _personCode & "_Name")
        Session.Remove("TempExtraDoc_" & _personCode & "_Path")
        Session.Remove("TempExtraDoc_" & _personCode & "_Name")
        ' ✅ مدارک جدید
        Session.Remove("TempBirthCert_" & _personCode & "_Path")
        Session.Remove("TempBirthCert_" & _personCode & "_Name")
        Session.Remove("TempNationalCard_" & _personCode & "_Path")
        Session.Remove("TempNationalCard_" & _personCode & "_Name")
        Session.Remove("TempStudentCard_" & _personCode & "_Path")
        Session.Remove("TempStudentCard_" & _personCode & "_Name")
    End Sub

    Private Sub LoadDeadline()
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT TOP 1 DeadlineDate FROM exmChildSettings WHERE AcademicYear = @Year AND IsActive = 1",
                New SqlParameter("@Year", ACADEMIC_YEAR))

            If dt.Rows.Count = 0 OrElse dt.Rows(0)("DeadlineDate") Is DBNull.Value Then
                pnlDeadline.Visible = False
                Return
            End If

            Dim deadline As DateTime = Convert.ToDateTime(dt.Rows(0)("DeadlineDate"))
            lblDeadlineDate.Text = GetPersianDate(deadline)

            Dim daysLeft As Integer = (deadline.Date - DateTime.Now.Date).Days

            If daysLeft < 0 Then
                lblDeadlineStatus.Text = "⛔ مهلت به پایان رسیده است"
                lblDeadlineStatus.Style("color") = "#e74c3c"
                btnSubmit.Enabled = False
            ElseIf daysLeft = 0 Then
                lblDeadlineStatus.Text = "⚠️ آخرین روز مهلت ارسال"
                lblDeadlineStatus.Style("color") = "#f39c12"
            Else
                lblDeadlineStatus.Text = "✅ " & daysLeft & " روز باقی‌مانده"
                lblDeadlineStatus.Style("color") = "#27ae60"
            End If
        Catch
            pnlDeadline.Visible = False
        End Try
    End Sub

    Private Sub LoadChildren()
        Try
            Dim editingRequestID As Integer = 0
            Integer.TryParse(hfEditingRequestID.Value, editingRequestID)

            Dim sql As String =
                "SELECT s.ID AS SponsorshipID, " &
                "       s.fstName + ' ' + s.Name AS ChildName, " &
                "       ISNULL(s.codeMeli, '') AS NationalCode, " &
                "       ISNULL(s.birthday, '') AS BirthDate, " &
                "       s.relation AS RelationID, " &
                "       s.empID AS OwnerEmpID, " &
                "       ISNULL((SELECT COUNT(*) FROM exmChildRequests cr " &
                "               WHERE cr.PersonCode = s.empID " &
                "                 AND cr.SponsorshipID = s.ID " &
                "                 AND cr.AcademicYear = @Year " &
                "                 AND cr.StatusCode NOT IN (4, 5) " &
                "                 AND cr.ID <> @EditingID), 0) AS HasExistingRequest " &
                "FROM Sponsorship s " &
                "WHERE s.empID = @EmpID AND s.relation IN (1, 2) " &
                "ORDER BY s.birthday DESC"

            Dim dt As DataTable = GetDataTable(sql,
                New SqlParameter("@EmpID", _personCode),
                New SqlParameter("@Year", ACADEMIC_YEAR),
                New SqlParameter("@EditingID", editingRequestID))

            If dt.Rows.Count = 0 Then
                pnlChildren.Visible = False
                pnlNoChildren.Visible = True
                btnSubmit.Enabled = False
                Return
            End If

            dt.Columns.Add("IsSelected", GetType(Boolean))
            dt.Columns.Add("IsEditing", GetType(Boolean))

            Dim selectedID As Integer = 0
            Integer.TryParse(hfSelectedChildID.Value, selectedID)

            Dim hasAnyAvailable As Boolean = False

            For Each row As DataRow In dt.Rows
                Dim hasExisting As Boolean = (Convert.ToInt32(row("HasExistingRequest")) > 0)
                row("IsSelected") = (Convert.ToInt32(row("SponsorshipID")) = selectedID)
                row("IsEditing") = (editingRequestID > 0 AndAlso Convert.ToInt32(row("SponsorshipID")) = selectedID)

                If Not hasExisting Then hasAnyAvailable = True
            Next

            rptChildren.DataSource = dt
            rptChildren.DataBind()

            pnlChildren.Visible = True
            pnlNoChildren.Visible = False

            btnSubmit.Enabled = hasAnyAvailable

        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری فرزندان: " & ex.Message)
        End Try
    End Sub

    Protected Sub rptChildren_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then Return
            Dim drv = TryCast(e.Item.DataItem, DataRowView)
            If drv Is Nothing Then Return

            Dim hasExisting As Boolean = (Convert.ToInt32(drv("HasExistingRequest")) > 0)
            Dim isSelected As Boolean = Convert.ToBoolean(drv("IsSelected"))
            Dim isEditing As Boolean = Convert.ToBoolean(drv("IsEditing"))

            Dim lnkChild = TryCast(e.Item.FindControl("lnkChild"), LinkButton)
            If lnkChild IsNot Nothing Then
                If hasExisting Then
                    lnkChild.CssClass = "chr-child-card disabled"
                    lnkChild.Enabled = False
                ElseIf isEditing Then
                    lnkChild.CssClass = "chr-child-card editing-card"
                ElseIf isSelected Then
                    lnkChild.CssClass = "chr-child-card selected"
                Else
                    lnkChild.CssClass = "chr-child-card"
                End If
            End If

            Dim spnIcon = TryCast(e.Item.FindControl("spnIcon"), System.Web.UI.HtmlControls.HtmlGenericControl)
            If spnIcon IsNot Nothing Then
                Dim relationID As Integer = Convert.ToInt32(drv("RelationID"))
                spnIcon.InnerText = If(relationID = 1, "👦", If(relationID = 2, "👧", "👶"))
            End If

            Dim lnkEditChild = TryCast(e.Item.FindControl("lnkEditChild"), LinkButton)
            If lnkEditChild IsNot Nothing Then
                Dim ownerEmpID As String = If(drv("OwnerEmpID") Is DBNull.Value, "", drv("OwnerEmpID").ToString().Trim())
                lnkEditChild.Visible = (ownerEmpID = _personCode)
            End If
        Catch
        End Try
    End Sub

    Protected Sub rptChildren_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "SelectChild" Then
                If hfEditingRequestID.Value <> "0" AndAlso hfEditingRequestID.Value <> "" Then
                    ShowToast("warning", "در حالت ویرایش درخواست نمی‌توانید فرزند را تغییر دهید")
                    Return
                End If

                ClearTempFiles()
                hfSelectedChildID.Value = e.CommandArgument.ToString()
                LoadChildren()
                ResetEducationForm()

                ShowPanel(pnlEducation, "chr-section education")
                ShowPanel(pnlDocuments, "chr-section documents")

            ElseIf e.CommandName = "EditChild" Then
                Dim childID As Integer = 0
                If Not Integer.TryParse(e.CommandArgument.ToString(), childID) OrElse childID = 0 Then
                    ShowToast("error", "شناسه فرزند نامعتبر است")
                    Return
                End If

                LoadChildForEdit(childID)
            End If

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    Private Sub ResetEducationForm()
        HidePanel(pnlConditionHint, "chr-condition-hint")
        HidePanel(pnlPrimaryBox, "chr-primary-box")
        HidePanel(pnlExtraDocument, "chr-extra-doc-panel")
        HidePanel(pnlTranscriptUpload, "")
        HidePanel(pnlUniversity, "")
        HidePanel(pnlSchool, "")
        HidePanel(pnlStudentCard, "chr-student-card-panel")
        HidePanel(pnlRankInput, "")
        HidePanel(pnlWithoutExamReason, "")
        HidePanel(pnlSampadDetails, "")
        ShowPanel(pnlAverageRow, "")

        ddlChildType.SelectedValue = ""
        ddlEducationLevel.Items.Clear()
        ddlEducationLevel.Items.Add(New ListItem("-- ابتدا نوع تحصیل را انتخاب کنید --", ""))
        txtAverage.Text = ""
        txtUniversityName.Text = ""
        ddlUniversityType.SelectedValue = ""
        txtUniversityPhone.Text = ""
        txtStudentNumber.Text = ""
        txtRank.Text = ""
        ddlAdmissionType.SelectedValue = ""
        ddlWithoutExamReason.SelectedValue = ""
        txtSchoolName.Text = ""
        ddlSchoolType.SelectedValue = ""
        txtSchoolPhone.Text = ""
        chkSampad.Checked = False
        txtSampadSchoolName.Text = ""
        txtSampadStudentNumber.Text = ""

        hfRequiresExtraDoc.Value = "0"
        hfExtraDocType.Value = ""
        hfTranscriptUploaded.Value = "0"
        hfExtraDocUploaded.Value = "0"
        hfBirthCertUploaded.Value = "0"
        hfNationalCardUploaded.Value = "0"
        hfStudentCardUploaded.Value = "0"
        hfSampadSelected.Value = "0"

        lblTranscriptStatus.Text = ""
        lblExtraDocStatus.Text = ""
        lblBirthCertStatus.Text = ""
        lblNationalCardStatus.Text = ""
        lblStudentCardStatus.Text = ""

        pnlOldTranscript.Visible = False
        pnlOldExtraDoc.Visible = False
        pnlOldBirthCert.Visible = False
        pnlOldNationalCard.Visible = False
        pnlOldStudentCard.Visible = False
    End Sub

    ' ============================================================
    ' Child: Add New
    ' ============================================================
    Protected Sub lnkShowAddChild_Click(ByVal sender As Object, ByVal e As EventArgs)
        HidePanel(pnlEditChild, "chr-add-child-panel")
        ShowPanel(pnlAddChild, "chr-add-child-panel")
        txtNewChildFirstName.Text = ""
        txtNewChildLastName.Text = ""
        txtNewChildNationalCode.Text = ""
        txtNewChildBirthDate.Text = ""
        ddlNewChildGender.SelectedValue = ""
    End Sub

    Protected Sub lnkCloseAddChild_Click(ByVal sender As Object, ByVal e As EventArgs)
        HidePanel(pnlAddChild, "chr-add-child-panel")
        txtNewChildFirstName.Text = ""
        txtNewChildLastName.Text = ""
        txtNewChildNationalCode.Text = ""
        txtNewChildBirthDate.Text = ""
        ddlNewChildGender.SelectedValue = ""
    End Sub

    Protected Sub btnSaveNewChild_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim firstName As String = txtNewChildFirstName.Text.Trim()
            Dim lastName As String = txtNewChildLastName.Text.Trim()
            Dim nationalCode As String = txtNewChildNationalCode.Text.Trim()
            Dim birthDate As String = txtNewChildBirthDate.Text.Trim()
            Dim genderValue As String = ddlNewChildGender.SelectedValue

            If String.IsNullOrEmpty(firstName) Then
                ShowModalAlert("warning", "نام الزامی", "لطفاً نام فرزند را وارد کنید.")
                Return
            End If

            If String.IsNullOrEmpty(lastName) Then
                ShowModalAlert("warning", "نام خانوادگی الزامی", "لطفاً نام خانوادگی فرزند را وارد کنید.")
                Return
            End If

            If String.IsNullOrEmpty(genderValue) Then
                ShowModalAlert("warning", "جنسیت الزامی", "لطفاً جنسیت فرزند را انتخاب کنید.")
                Return
            End If

            If String.IsNullOrEmpty(nationalCode) Then
                ShowModalAlert("warning", "کد ملی الزامی", "لطفاً کد ملی فرزند را وارد کنید.")
                Return
            End If

            If nationalCode.Length <> 10 OrElse Not IsNumeric(nationalCode) Then
                ShowModalAlert("warning", "کد ملی نامعتبر", "کد ملی باید 10 رقم عددی باشد.")
                Return
            End If

            Dim dtDup As DataTable = GetDataTable(
                "SELECT COUNT(*) FROM Sponsorship WHERE codeMeli = @Code",
                New SqlParameter("@Code", nationalCode))

            If Convert.ToInt32(dtDup.Rows(0)(0)) > 0 Then
                ShowModalAlert("warning", "کد ملی تکراری",
                    "این کد ملی قبلاً در سیستم ثبت شده است. اگر فرزند شماست، با پشتیبانی تماس بگیرید.")
                Return
            End If

            If String.IsNullOrEmpty(birthDate) Then
                ShowModalAlert("warning", "تاریخ تولد الزامی", "لطفاً تاریخ تولد فرزند را وارد کنید (مثال: 1390/05/15).")
                Return
            End If

            Dim personId As Integer = 0
            If Session("userid") IsNot Nothing Then
                Integer.TryParse(Session("userid").ToString(), personId)
            End If

            If personId = 0 Then
                ShowModalAlert("error", "خطا", "اطلاعات پرسنلی شما در سیستم یافت نشد. لطفاً مجدداً وارد سیستم شوید.")
                Return
            End If

            Dim relationID As Integer = Convert.ToInt32(genderValue)

            Dim sqlInsert As String =
                "INSERT INTO Sponsorship " &
                "(empID, fstName, Name, relation, birthday, credit, logUID, IsActive, perId, codeMeli, editt) " &
                "VALUES " &
                "(@empID, @fstName, @Name, @relation, @birthday, NULL, @logUID, 0, @perId, @codeMeli, 0); " &
                "SELECT SCOPE_IDENTITY();"

            Dim newID As Object = ExecuteScalar(sqlInsert,
                New SqlParameter("@empID", _personCode),
                New SqlParameter("@fstName", firstName),
                New SqlParameter("@Name", lastName),
                New SqlParameter("@relation", relationID),
                New SqlParameter("@birthday", birthDate),
                New SqlParameter("@logUID", _personCode),
                New SqlParameter("@perId", personId),
                New SqlParameter("@codeMeli", nationalCode))

            If newID IsNot Nothing Then
                HidePanel(pnlAddChild, "chr-add-child-panel")
                txtNewChildFirstName.Text = ""
                txtNewChildLastName.Text = ""
                txtNewChildNationalCode.Text = ""
                txtNewChildBirthDate.Text = ""
                ddlNewChildGender.SelectedValue = ""

                LoadChildren()

                ShowToast("success", "فرزند جدید با موفقیت ثبت شد. حالا می‌توانید آن را انتخاب کنید.")

                Dim script As String = "setTimeout(function(){ var el = document.querySelector('.chr-section.child'); if(el) el.scrollIntoView({behavior:'smooth', block:'start'}); }, 300);"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "scrollToChild_" & Guid.NewGuid().ToString("N"), script, True)
            Else
                ShowModalAlert("error", "خطا", "ثبت فرزند انجام نشد. لطفاً دوباره تلاش کنید.")
            End If

        Catch ex As Exception
            ShowModalAlert("error", "خطا در ثبت", ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' Child: Edit
    ' ============================================================
    Private Sub LoadChildForEdit(ByVal childID As Integer)
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT ID, fstName, Name, relation, birthday, codeMeli " &
                "FROM Sponsorship " &
                "WHERE ID = @ID AND empID = @EmpID AND relation IN (1, 2)",
                New SqlParameter("@ID", childID),
                New SqlParameter("@EmpID", _personCode))

            If dt.Rows.Count = 0 Then
                ShowModalAlert("error", "دسترسی غیرمجاز", "این فرزند متعلق به شما نیست یا یافت نشد.")
                Return
            End If

            Dim row As DataRow = dt.Rows(0)

            hfEditingChildID.Value = childID.ToString()

            txtEditChildFirstName.Text = If(IsDBNull(row("fstName")), "", row("fstName").ToString().Trim())
            txtEditChildLastName.Text = If(IsDBNull(row("Name")), "", row("Name").ToString().Trim())
            txtEditChildBirthDate.Text = If(IsDBNull(row("birthday")), "", row("birthday").ToString().Trim())
            txtEditChildNationalCode.Text = If(IsDBNull(row("codeMeli")), "", row("codeMeli").ToString().Trim())

            Dim relID As Integer = If(IsDBNull(row("relation")), 0, Convert.ToInt32(row("relation")))
            ddlEditChildGender.SelectedValue = If(relID > 0, relID.ToString(), "")

            HidePanel(pnlAddChild, "chr-add-child-panel")
            ShowPanel(pnlEditChild, "chr-add-child-panel")

            Dim script As String = "setTimeout(function(){ var el = document.getElementById('" & pnlEditChild.ClientID & "'); if(el) el.scrollIntoView({behavior:'smooth', block:'start'}); }, 300);"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "scrollToEditChild_" & Guid.NewGuid().ToString("N"), script, True)

        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری فرزند: " & ex.Message)
        End Try
    End Sub

    Protected Sub lnkCloseEditChild_Click(ByVal sender As Object, ByVal e As EventArgs)
        HidePanel(pnlEditChild, "chr-add-child-panel")
        hfEditingChildID.Value = "0"
        txtEditChildFirstName.Text = ""
        txtEditChildLastName.Text = ""
        txtEditChildNationalCode.Text = ""
        txtEditChildBirthDate.Text = ""
        ddlEditChildGender.SelectedValue = ""
    End Sub

    Protected Sub btnSaveEditChild_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim childID As Integer = 0
            Integer.TryParse(hfEditingChildID.Value, childID)

            If childID = 0 Then
                ShowModalAlert("error", "خطا", "شناسه فرزند معتبر نیست.")
                Return
            End If

            Dim firstName As String = txtEditChildFirstName.Text.Trim()
            Dim lastName As String = txtEditChildLastName.Text.Trim()
            Dim nationalCode As String = txtEditChildNationalCode.Text.Trim()
            Dim birthDate As String = txtEditChildBirthDate.Text.Trim()
            Dim genderValue As String = ddlEditChildGender.SelectedValue

            If String.IsNullOrEmpty(firstName) Then
                ShowModalAlert("warning", "نام الزامی", "لطفاً نام فرزند را وارد کنید.")
                Return
            End If

            If String.IsNullOrEmpty(lastName) Then
                ShowModalAlert("warning", "نام خانوادگی الزامی", "لطفاً نام خانوادگی فرزند را وارد کنید.")
                Return
            End If

            If String.IsNullOrEmpty(genderValue) Then
                ShowModalAlert("warning", "جنسیت الزامی", "لطفاً جنسیت فرزند را انتخاب کنید.")
                Return
            End If

            If String.IsNullOrEmpty(nationalCode) Then
                ShowModalAlert("warning", "کد ملی الزامی", "لطفاً کد ملی فرزند را وارد کنید.")
                Return
            End If

            If nationalCode.Length <> 10 OrElse Not IsNumeric(nationalCode) Then
                ShowModalAlert("warning", "کد ملی نامعتبر", "کد ملی باید 10 رقم عددی باشد.")
                Return
            End If

            Dim dtDup As DataTable = GetDataTable(
                "SELECT COUNT(*) FROM Sponsorship WHERE codeMeli = @Code AND ID <> @ID",
                New SqlParameter("@Code", nationalCode),
                New SqlParameter("@ID", childID))

            If Convert.ToInt32(dtDup.Rows(0)(0)) > 0 Then
                ShowModalAlert("warning", "کد ملی تکراری",
                    "این کد ملی قبلاً برای فرزند دیگری ثبت شده است.")
                Return
            End If

            If String.IsNullOrEmpty(birthDate) Then
                ShowModalAlert("warning", "تاریخ تولد الزامی", "لطفاً تاریخ تولد فرزند را وارد کنید (مثال: 1390/05/15).")
                Return
            End If

            Dim dtCheck As DataTable = GetDataTable(
                "SELECT COUNT(*) FROM Sponsorship WHERE ID = @ID AND empID = @EmpID",
                New SqlParameter("@ID", childID),
                New SqlParameter("@EmpID", _personCode))

            If Convert.ToInt32(dtCheck.Rows(0)(0)) = 0 Then
                ShowModalAlert("error", "دسترسی غیرمجاز", "این فرزند متعلق به شما نیست.")
                Return
            End If

            Dim relationID As Integer = Convert.ToInt32(genderValue)

            Dim affected As Integer = ExecuteNonQuery(
                "UPDATE Sponsorship SET " &
                "  fstName = @fstName, Name = @Name, relation = @relation, " &
                "  birthday = @birthday, codeMeli = @codeMeli " &
                "WHERE ID = @ID AND empID = @EmpID",
                New SqlParameter("@fstName", firstName),
                New SqlParameter("@Name", lastName),
                New SqlParameter("@relation", relationID),
                New SqlParameter("@birthday", birthDate),
                New SqlParameter("@codeMeli", nationalCode),
                New SqlParameter("@ID", childID),
                New SqlParameter("@EmpID", _personCode))

            If affected > 0 Then
                HidePanel(pnlEditChild, "chr-add-child-panel")
                hfEditingChildID.Value = "0"
                txtEditChildFirstName.Text = ""
                txtEditChildLastName.Text = ""
                txtEditChildNationalCode.Text = ""
                txtEditChildBirthDate.Text = ""
                ddlEditChildGender.SelectedValue = ""

                LoadChildren()

                ShowToast("success", "اطلاعات فرزند با موفقیت ویرایش شد.")

                Dim script As String = "setTimeout(function(){ var el = document.querySelector('.chr-section.child'); if(el) el.scrollIntoView({behavior:'smooth', block:'start'}); }, 300);"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "scrollAfterEdit_" & Guid.NewGuid().ToString("N"), script, True)
            Else
                ShowModalAlert("error", "خطا", "ویرایش انجام نشد. لطفاً دوباره تلاش کنید.")
            End If

        Catch ex As Exception
            ShowModalAlert("error", "خطا در ویرایش", ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' Events: DropDownList — نوع تحصیل
    ' ============================================================
    Protected Sub ddlChildType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            HidePanel(pnlConditionHint, "chr-condition-hint")
            HidePanel(pnlPrimaryBox, "chr-primary-box")
            HidePanel(pnlExtraDocument, "chr-extra-doc-panel")
            HidePanel(pnlTranscriptUpload, "")
            HidePanel(pnlUniversity, "")
            HidePanel(pnlSchool, "")
            HidePanel(pnlStudentCard, "chr-student-card-panel")
            HidePanel(pnlRankInput, "")
            HidePanel(pnlWithoutExamReason, "")

            ddlEducationLevel.Items.Clear()

            If String.IsNullOrEmpty(ddlChildType.SelectedValue) Then
                ddlEducationLevel.Items.Add(New ListItem("-- ابتدا نوع تحصیل را انتخاب کنید --", ""))
                Return
            End If

            Dim isStudent As Boolean = (ddlChildType.SelectedValue = "دانشجو")

            If isStudent Then
                ' ✅ دانشجو: دانشگاه + کارت دانشجویی
                ShowPanel(pnlUniversity, "")
                ShowPanel(pnlStudentCard, "chr-student-card-panel")
                HidePanel(pnlSchool, "")
                HidePanel(pnlTranscriptUpload, "")
            Else
                ' ✅ دانش‌آموز: مدرسه + کارنامه
                ShowPanel(pnlSchool, "")
                ShowPanel(pnlTranscriptUpload, "")
                HidePanel(pnlUniversity, "")
                HidePanel(pnlStudentCard, "chr-student-card-panel")
            End If

            LoadEducationLevels(ddlChildType.SelectedValue)

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadEducationLevels(ByVal childType As String)
        Try
            Dim sql As String =
                "SELECT ID, EducationLevel, Condition, ISNULL(MinAverage, 0) AS MinAverage, RowNumber " &
                "FROM exmChildConditions WHERE IsActive = 1 "

            If childType = "دانش‌آموز" Then
                sql &= "AND RowNumber BETWEEN 1 AND 12 "
            ElseIf childType = "دانشجو" Then
                sql &= "AND RowNumber BETWEEN 13 AND 25 "
            End If

            sql &= "ORDER BY SortOrder"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            ddlEducationLevel.Items.Clear()
            ddlEducationLevel.Items.Add(New ListItem("-- انتخاب کنید --", ""))

            For Each row As DataRow In dt.Rows
                Dim item As New ListItem(row("EducationLevel").ToString(), row("ID").ToString())
                item.Attributes.Add("data-rownumber", row("RowNumber").ToString())
                ddlEducationLevel.Items.Add(item)
            Next
        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری مقاطع: " & ex.Message)
        End Try
    End Sub

    Protected Sub ddlEducationLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            HidePanel(pnlConditionHint, "chr-condition-hint")
            HidePanel(pnlPrimaryBox, "chr-primary-box")
            HidePanel(pnlExtraDocument, "chr-extra-doc-panel")

            If String.IsNullOrEmpty(ddlEducationLevel.SelectedValue) Then Return

            Dim levelID As Integer = Convert.ToInt32(ddlEducationLevel.SelectedValue)
            Dim dt As DataTable = GetDataTable(
                "SELECT Condition, MinAverage, RowNumber FROM exmChildConditions WHERE ID = @ID",
                New SqlParameter("@ID", levelID))

            If dt.Rows.Count = 0 Then Return

            Dim rowNumber As Integer = Convert.ToInt32(dt.Rows(0)("RowNumber"))
            Dim condition As String = dt.Rows(0)("Condition").ToString()
            Dim isPrimary As Boolean = (rowNumber = 1 OrElse rowNumber = 2)

            If isPrimary Then
                ShowPanel(pnlPrimaryBox, "chr-primary-box")
                HidePanel(pnlAverageRow, "")
                HidePanel(pnlConditionHint, "chr-condition-hint")
                txtAverage.Text = ""
                ShowPanel(pnlTranscriptUpload, "")
                Return
            End If

            ShowPanel(pnlAverageRow, "")
            lblCondition.Text = condition
            ShowPanel(pnlConditionHint, "chr-condition-hint")

            Dim isStudent As Boolean = (ddlChildType.SelectedValue = "دانشجو")
            If Not isStudent Then
                ShowPanel(pnlTranscriptUpload, "")
            Else
                HidePanel(pnlTranscriptUpload, "")
            End If

            CheckExtraDocumentRequirement(condition)

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ نوع ورود به دانشگاه: با آزمون / بدون آزمون
    ' ============================================================
    Protected Sub ddlAdmissionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            HidePanel(pnlRankInput, "")
            HidePanel(pnlWithoutExamReason, "")

            If ddlAdmissionType.SelectedValue = "WithExam" Then
                ShowPanel(pnlRankInput, "")
                ddlWithoutExamReason.SelectedValue = ""
            ElseIf ddlAdmissionType.SelectedValue = "WithoutExam" Then
                ShowPanel(pnlWithoutExamReason, "")
                txtRank.Text = ""
            End If
        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ چک‌باکس سمپاد
    ' ============================================================
    Protected Sub chkSampad_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If chkSampad.Checked Then
                ShowPanel(pnlSampadDetails, "")
                hfSampadSelected.Value = "1"
                ' اگر سمپاد انتخاب شد، نوع مدرسه به سمپاد تغییر کند
                If ddlSchoolType.Items.FindByValue("سمپاد") IsNot Nothing Then
                    ddlSchoolType.SelectedValue = "سمپاد"
                End If
                ' مدرک استعدادهای درخشان الزامی می‌شود
                hfRequiresExtraDoc.Value = "1"
                hfExtraDocType.Value = "استعدادهای درخشان"
                lblExtraDocTitle.Text = "📑 مدرک قبولی در سمپاد"
                lblExtraDocName.Text = "کارت ورود به مدرسه سمپاد"
                lblExtraDocDescription.Text = "لطفاً تصویر کارت ورود یا معرفی‌نامه مدرسه سمپاد را بارگذاری نمایید."
                ShowPanel(pnlExtraDocument, "chr-extra-doc-panel")
            Else
                HidePanel(pnlSampadDetails, "")
                hfSampadSelected.Value = "0"
                txtSampadSchoolName.Text = ""
                txtSampadStudentNumber.Text = ""
                ' اگر شرط دیگری برای مدرک اضافی وجود ندارد، پنهان شود
                If String.IsNullOrEmpty(hfExtraDocType.Value) OrElse hfExtraDocType.Value = "استعدادهای درخشان" Then
                    hfRequiresExtraDoc.Value = "0"
                    hfExtraDocType.Value = ""
                    HidePanel(pnlExtraDocument, "chr-extra-doc-panel")
                End If
            End If
        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckExtraDocumentRequirement(ByVal condition As String)
        Dim conditionLower As String = If(condition, "").ToLower().Trim()
        Dim isStudent As Boolean = (ddlChildType.SelectedValue = "دانشجو")

        Dim requires As Boolean = False
        Dim title As String = ""
        Dim docName As String = ""
        Dim desc As String = ""
        Dim docType As String = ""

        If isStudent Then
            requires = True
            title = "📑 گواهی پذیرش دانشگاه"
            docName = "گواهی پذیرش دانشگاه"
            desc = "لطفاً تصویر گواهی پذیرش دانشگاه (کارت دانشجویی، نامه پذیرش یا کارنامه کنکور) را بارگذاری نمایید."
            docType = "پذیرش دانشگاه"
        Else
            If conditionLower.Contains("پذیرش در دانشگاه") OrElse conditionLower.Contains("ورودی مقطع") Then
                requires = True
                title = "📑 مدرک پذیرش در دانشگاه"
                docName = "گواهی پذیرش دانشگاه"
                desc = "لطفاً تصویر گواهی پذیرش دانشگاه را بارگذاری نمایید."
                docType = "پذیرش دانشگاه"
            ElseIf conditionLower.Contains("بدون آزمون") Then
                requires = True
                title = "📑 مدرک پذیرش بدون آزمون"
                docName = "گواهی پذیرش بدون آزمون"
                desc = "لطفاً تصویر گواهی پذیرش بدون آزمون را بارگذاری نمایید."
                docType = "بدون آزمون"
            ElseIf conditionLower.Contains("رتبه") Then
                requires = True
                title = "📑 کارنامه آزمون سراسری"
                docName = "کارنامه آزمون سراسری"
                desc = "لطفاً تصویر کارنامه آزمون سراسری را بارگذاری نمایید."
                docType = "رتبه آزمون"
            ElseIf conditionLower.Contains("استعداد") OrElse conditionLower.Contains("تیزهوشان") OrElse conditionLower.Contains("درخشان") Then
                If conditionLower.Contains("ورودی دوره") Then
                    requires = True
                    title = "📑 مدرک ورود به مدرسه استعدادهای درخشان"
                    docName = "کارت ورود به مدرسه تیزهوشان"
                    desc = "لطفاً تصویر کارت ورود یا معرفی‌نامه مدرسه استعدادهای درخشان را بارگذاری نمایید."
                    docType = "استعدادهای درخشان"
                End If
            End If
        End If

        ' اگر سمپاد انتخاب شده، مدرک استعدادهای درخشان الزامی است
        If hfSampadSelected.Value = "1" Then
            requires = True
            title = "📑 مدرک قبولی در سمپاد"
            docName = "کارت ورود به مدرسه سمپاد"
            desc = "لطفاً تصویر کارت ورود یا معرفی‌نامه مدرسه سمپاد را بارگذاری نمایید."
            docType = "استعدادهای درخشان"
        End If

        If requires Then
            hfRequiresExtraDoc.Value = "1"
            hfExtraDocType.Value = docType
            lblExtraDocTitle.Text = title
            lblExtraDocName.Text = docName
            lblExtraDocDescription.Text = desc
            ShowPanel(pnlExtraDocument, "chr-extra-doc-panel")
            lblExtraDocStatus.Text = ""
        Else
            hfRequiresExtraDoc.Value = "0"
            hfExtraDocType.Value = ""
            HidePanel(pnlExtraDocument, "chr-extra-doc-panel")
        End If
    End Sub

    ' ============================================================
    ' Upload Handlers — عمومی
    ' ============================================================
    Protected Sub btnUploadTranscript_Click(ByVal sender As Object, ByVal e As EventArgs)
        HandleFileUpload(fuTranscript, "TempTranscript_" & _personCode, lblTranscriptStatus, "کارنامه", hfTranscriptUploaded)
    End Sub

    Protected Sub btnUploadExtraDoc_Click(ByVal sender As Object, ByVal e As EventArgs)
        HandleFileUpload(fuExtraDocument, "TempExtraDoc_" & _personCode, lblExtraDocStatus, "مدرک", hfExtraDocUploaded)
    End Sub

    ' ✅ Handler های جدید
    Protected Sub btnUploadBirthCert_Click(ByVal sender As Object, ByVal e As EventArgs)
        HandleFileUpload(fuBirthCertificate, "TempBirthCert_" & _personCode, lblBirthCertStatus, "شناسنامه", hfBirthCertUploaded)
    End Sub

    Protected Sub btnUploadNationalCard_Click(ByVal sender As Object, ByVal e As EventArgs)
        HandleFileUpload(fuNationalCard, "TempNationalCard_" & _personCode, lblNationalCardStatus, "کارت ملی", hfNationalCardUploaded)
    End Sub

    Protected Sub btnUploadStudentCard_Click(ByVal sender As Object, ByVal e As EventArgs)
        HandleFileUpload(fuStudentCard, "TempStudentCard_" & _personCode, lblStudentCardStatus, "کارت دانشجویی", hfStudentCardUploaded)
    End Sub

    Private Sub HandleFileUpload(ByVal fileUpload As FileUpload, ByVal sessionKey As String,
                                  ByVal statusLabel As Label, ByVal docLabel As String,
                                  ByVal uploadedFlag As HiddenField)
        Try
            If Not fileUpload.HasFile Then
                statusLabel.Text = "❌ لطفاً یک فایل انتخاب کنید"
                statusLabel.CssClass = "upload-status error"
                ShowToast("warning", "لطفاً یک فایل انتخاب کنید")
                Return
            End If

            If fileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                statusLabel.Text = "❌ حجم فایل بیشتر از 2 مگابایت است"
                statusLabel.CssClass = "upload-status error"
                ShowToast("error", "حجم فایل بیشتر از 2 مگابایت است")
                Return
            End If

            Dim ext As String = Path.GetExtension(fileUpload.FileName).ToLower()
            If Not ALLOWED_EXTENSIONS.Contains(ext) Then
                statusLabel.Text = "❌ فرمت مجاز نیست (فقط JPG، PNG، PDF)"
                statusLabel.CssClass = "upload-status error"
                ShowToast("error", "فرمت فایل مجاز نیست. فقط JPG، PNG یا PDF")
                Return
            End If

            Dim tempFolderPath As String = Server.MapPath(TEMP_FOLDER)
            If Not Directory.Exists(tempFolderPath) Then Directory.CreateDirectory(tempFolderPath)

            Dim tempFileName As String = "tmp_" & Guid.NewGuid().ToString("N") & ext
            Dim tempFilePath As String = Path.Combine(tempFolderPath, tempFileName)

            fileUpload.SaveAs(tempFilePath)

            Session(sessionKey & "_Path") = tempFilePath
            Session(sessionKey & "_Name") = fileUpload.FileName

            If uploadedFlag IsNot Nothing Then uploadedFlag.Value = "1"

            statusLabel.Text = "✅ فایل بارگذاری شد: " & fileUpload.FileName
            statusLabel.CssClass = "upload-status"
            ShowToast("success", docLabel & " با موفقیت بارگذاری شد")

        Catch ex As Exception
            statusLabel.Text = "❌ خطا در بارگذاری: " & ex.Message
            statusLabel.CssClass = "upload-status error"
            ShowToast("error", "خطا در بارگذاری: " & ex.Message)
        End Try
    End Sub

    Private Function MoveFileFromTemp(ByVal sessionKey As String, ByVal childName As String, ByVal docType As String) As String
        Try
            If Session(sessionKey & "_Path") Is Nothing Then Return ""
            Dim currentPath As String = Session(sessionKey & "_Path").ToString()
            If Not File.Exists(currentPath) Then
                Session.Remove(sessionKey & "_Path")
                Session.Remove(sessionKey & "_Name")
                Return ""
            End If

            Dim originalName As String = ""
            If Session(sessionKey & "_Name") IsNot Nothing Then
                originalName = Session(sessionKey & "_Name").ToString()
            End If

            Dim ext As String = Path.GetExtension(originalName).ToLower()
            If String.IsNullOrEmpty(ext) Then ext = Path.GetExtension(currentPath).ToLower()

            Dim newFileName As String = GenerateFileName(_personCode, childName, docType, ext)
            Dim folderPath As String = Server.MapPath(UPLOAD_FOLDER)

            If Not Directory.Exists(folderPath) Then Directory.CreateDirectory(folderPath)

            Dim finalPath As String = Path.Combine(folderPath, newFileName)

            If currentPath <> finalPath Then
                If File.Exists(finalPath) Then File.Delete(finalPath)
                File.Copy(currentPath, finalPath, True)
                Try
                    File.Delete(currentPath)
                Catch
                End Try
            End If

            Session.Remove(sessionKey & "_Path")
            Session.Remove(sessionKey & "_Name")

            If File.Exists(finalPath) Then Return newFileName
            Return ""

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("MoveFileFromTemp Error: " & ex.Message)
            Session.Remove(sessionKey & "_Path")
            Session.Remove(sessionKey & "_Name")
            Return ""
        End Try
    End Function

    ' ============================================================
    ' History
    ' ============================================================
    Protected Sub rptHistory_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then Return
            Dim drv = TryCast(e.Item.DataItem, DataRowView)
            If drv Is Nothing Then Return

            Dim statusCode As Integer = Convert.ToInt32(drv("StatusCode"))

            Dim lnkEdit = TryCast(e.Item.FindControl("lnkEdit"), LinkButton)
            If lnkEdit IsNot Nothing Then
                lnkEdit.Visible = (statusCode = 2)
            End If

            Dim pnlReason = TryCast(e.Item.FindControl("pnlReason"), Panel)
            If pnlReason IsNot Nothing Then
                If drv.Row.Table.Columns.Contains("HasReason") AndAlso Not IsDBNull(drv("HasReason")) Then
                    pnlReason.Visible = Convert.ToBoolean(drv("HasReason"))
                Else
                    pnlReason.Visible = False
                End If
            End If

        Catch
        End Try
    End Sub

    Protected Sub rptHistory_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        Try
            If e.CommandName <> "EditRequest" Then Return

            Dim requestID As Integer = 0
            If Not Integer.TryParse(e.CommandArgument.ToString(), requestID) OrElse requestID = 0 Then
                ShowToast("error", "شناسه درخواست نامعتبر است")
                Return
            End If

            LoadRequestForEdit(requestID)

        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری درخواست: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadRequestForEdit(ByVal requestID As Integer)
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT * FROM exmChildRequests WHERE ID = @ID AND PersonCode = @PersonCode AND StatusCode = 2",
                New SqlParameter("@ID", requestID),
                New SqlParameter("@PersonCode", _personCode))

            If dt.Rows.Count = 0 Then
                ShowToast("error", "درخواست یافت نشد یا قابل ویرایش نیست")
                Return
            End If

            Dim row As DataRow = dt.Rows(0)

            hfEditingRequestID.Value = requestID.ToString()
            hfSelectedChildID.Value = row("SponsorshipID").ToString()

            lblEditMode.Visible = True
            pnlEditBanner.Visible = True
            lblEditChildName.Text = row("ChildFullName").ToString()

            Dim reasonFromHistory As String = ""
            Dim commentFromHistory As String = ""
            Dim reviewerFromHistory As String = ""

            Dim dtHistory As DataTable = GetDataTable(
                "SELECT TOP 1 StatusDescription, ReviewerComment, ReviewerName " &
                "FROM exmChildRequestsHistory WHERE RequestID = @RequestID AND StatusCode = 2 " &
                "ORDER BY CreateDate DESC",
                New SqlParameter("@RequestID", requestID))

            If dtHistory.Rows.Count > 0 Then
                If dtHistory.Rows(0)("StatusDescription") IsNot DBNull.Value Then
                    reasonFromHistory = dtHistory.Rows(0)("StatusDescription").ToString().Trim()
                End If
                If dtHistory.Rows(0)("ReviewerComment") IsNot DBNull.Value Then
                    commentFromHistory = dtHistory.Rows(0)("ReviewerComment").ToString().Trim()
                End If
                If dtHistory.Rows(0)("ReviewerName") IsNot DBNull.Value Then
                    reviewerFromHistory = dtHistory.Rows(0)("ReviewerName").ToString().Trim()
                End If
            End If

            If String.IsNullOrEmpty(reasonFromHistory) Then
                If Not IsDBNull(row("StatusDescription")) Then
                    reasonFromHistory = row("StatusDescription").ToString().Trim()
                End If
            End If

            If String.IsNullOrEmpty(commentFromHistory) Then
                If Not IsDBNull(row("ReviewerComment")) Then
                    commentFromHistory = row("ReviewerComment").ToString().Trim()
                End If
            End If

            Dim fullReason As String = Server.HtmlEncode(reasonFromHistory)

            If Not String.IsNullOrEmpty(commentFromHistory) AndAlso commentFromHistory <> reasonFromHistory Then
                fullReason &= "<br/><span style='font-size:12px;color:#7f8c8d;margin-top:8px;display:block;'>"
                fullReason &= "📝 توضیحات تکمیلی: " & Server.HtmlEncode(commentFromHistory)
                fullReason &= "</span>"
            End If

            If Not String.IsNullOrEmpty(reviewerFromHistory) Then
                fullReason &= "<br/><span style='font-size:11px;color:#95a5a6;margin-top:6px;display:block;'>"
                fullReason &= "👤 کارشناس: " & Server.HtmlEncode(reviewerFromHistory)
                fullReason &= "</span>"
            End If

            If String.IsNullOrEmpty(fullReason) Then
                fullReason = "دلیل توسط کارشناس ذکر نشده است."
            End If

            lblEditReason.Text = fullReason

            LoadEditHistory(requestID)

            btnSubmit.Text = "💾 ذخیره تغییرات"
            btnSubmit.CssClass = "chr-btn chr-btn-submit edit-mode"

            ShowPanel(pnlEducation, "chr-section education")
            ShowPanel(pnlDocuments, "chr-section documents")
            ShowPanel(pnlAverageRow, "")

            If Not IsDBNull(row("ChildType")) Then ddlChildType.SelectedValue = row("ChildType").ToString()

            If Not String.IsNullOrEmpty(ddlChildType.SelectedValue) Then
                Dim isStudent As Boolean = (ddlChildType.SelectedValue = "دانشجو")
                If isStudent Then
                    ShowPanel(pnlUniversity, "")
                    ShowPanel(pnlStudentCard, "chr-student-card-panel")
                    HidePanel(pnlSchool, "")
                    HidePanel(pnlTranscriptUpload, "")
                Else
                    ShowPanel(pnlSchool, "")
                    ShowPanel(pnlTranscriptUpload, "")
                    HidePanel(pnlUniversity, "")
                    HidePanel(pnlStudentCard, "chr-student-card-panel")
                End If

                LoadEducationLevels(ddlChildType.SelectedValue)
                If Not IsDBNull(row("EducationLevelID")) Then
                    Dim levelID As String = row("EducationLevelID").ToString()
                    If ddlEducationLevel.Items.FindByValue(levelID) IsNot Nothing Then
                        ddlEducationLevel.SelectedValue = levelID
                    End If
                End If
            End If

            If Not IsDBNull(row("Average")) Then
                txtAverage.Text = Convert.ToDecimal(row("Average")).ToString("0.00")
            End If

            ' ✅ فیلدهای جدید
            If dt.Columns.Contains("SchoolName") AndAlso Not IsDBNull(row("SchoolName")) Then txtSchoolName.Text = row("SchoolName").ToString()
            If dt.Columns.Contains("SchoolType") AndAlso Not IsDBNull(row("SchoolType")) Then ddlSchoolType.SelectedValue = row("SchoolType").ToString()
            If dt.Columns.Contains("SchoolPhone") AndAlso Not IsDBNull(row("SchoolPhone")) Then txtSchoolPhone.Text = row("SchoolPhone").ToString()

            If dt.Columns.Contains("UniversityName") AndAlso Not IsDBNull(row("UniversityName")) Then txtUniversityName.Text = row("UniversityName").ToString()
            If dt.Columns.Contains("UniversityType") AndAlso Not IsDBNull(row("UniversityType")) Then ddlUniversityType.SelectedValue = row("UniversityType").ToString()
            If dt.Columns.Contains("UniversityPhone") AndAlso Not IsDBNull(row("UniversityPhone")) Then txtUniversityPhone.Text = row("UniversityPhone").ToString()
            If dt.Columns.Contains("StudentNumber") AndAlso Not IsDBNull(row("StudentNumber")) Then txtStudentNumber.Text = row("StudentNumber").ToString()

            If dt.Columns.Contains("AdmissionType") AndAlso Not IsDBNull(row("AdmissionType")) Then
                ddlAdmissionType.SelectedValue = row("AdmissionType").ToString()
                If ddlAdmissionType.SelectedValue = "WithExam" Then
                    ShowPanel(pnlRankInput, "")
                    HidePanel(pnlWithoutExamReason, "")
                ElseIf ddlAdmissionType.SelectedValue = "WithoutExam" Then
                    HidePanel(pnlRankInput, "")
                    ShowPanel(pnlWithoutExamReason, "")
                End If
            End If

            If dt.Columns.Contains("WithoutExamReason") AndAlso Not IsDBNull(row("WithoutExamReason")) Then
                ddlWithoutExamReason.SelectedValue = row("WithoutExamReason").ToString()
            End If

            If dt.Columns.Contains("RankInExam") AndAlso Not IsDBNull(row("RankInExam")) Then txtRank.Text = row("RankInExam").ToString()

            If dt.Columns.Contains("IsSampad") AndAlso Not IsDBNull(row("IsSampad")) Then
                chkSampad.Checked = Convert.ToBoolean(row("IsSampad"))
                If chkSampad.Checked Then
                    ShowPanel(pnlSampadDetails, "")
                    hfSampadSelected.Value = "1"
                End If
            End If

            If dt.Columns.Contains("SampadSchoolName") AndAlso Not IsDBNull(row("SampadSchoolName")) Then txtSampadSchoolName.Text = row("SampadSchoolName").ToString()
            If dt.Columns.Contains("SampadStudentNumber") AndAlso Not IsDBNull(row("SampadStudentNumber")) Then txtSampadStudentNumber.Text = row("SampadStudentNumber").ToString()

            ' ✅ فایل‌های قدیمی
            Dim transcriptFile As String = If(IsDBNull(row("TranscriptFileName")), "", row("TranscriptFileName").ToString())
            Dim extraDocFile As String = If(IsDBNull(row("ExtraDocumentFileName")), "", row("ExtraDocumentFileName").ToString())

            If Not String.IsNullOrEmpty(transcriptFile) Then
                pnlOldTranscript.Visible = True
                lblOldTranscriptName.Text = transcriptFile
                lblTranscriptStatus.Text = "✅ فایل فعلی موجود است. برای تغییر، فایل جدید بارگذاری کنید."
                lblTranscriptStatus.CssClass = "upload-status"
                hfTranscriptUploaded.Value = "1"
                lnkViewOldTranscript.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=transcript&mode=view"
                lnkDownloadOldTranscript.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=transcript&mode=download"
            End If

            If Not String.IsNullOrEmpty(extraDocFile) Then
                pnlOldExtraDoc.Visible = True
                lblOldExtraDocName.Text = extraDocFile
                lblExtraDocStatus.Text = "✅ فایل فعلی موجود است. برای تغییر، فایل جدید بارگذاری کنید."
                lblExtraDocStatus.CssClass = "upload-status"
                hfExtraDocUploaded.Value = "1"
                lnkViewOldExtra.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=extra&mode=view"
                lnkDownloadOldExtra.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=extra&mode=download"
            End If

            ' ✅ فایل‌های هویتی
            If dt.Columns.Contains("BirthCertificateFileName") AndAlso Not IsDBNull(row("BirthCertificateFileName")) Then
                Dim fn As String = row("BirthCertificateFileName").ToString()
                If Not String.IsNullOrEmpty(fn) Then
                    pnlOldBirthCert.Visible = True
                    lblOldBirthCertName.Text = fn
                    lblBirthCertStatus.Text = "✅ فایل فعلی موجود است."
                    lblBirthCertStatus.CssClass = "upload-status"
                    hfBirthCertUploaded.Value = "1"
                    lnkViewOldBirthCert.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=birthcert&mode=view"
                    lnkDownloadOldBirthCert.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=birthcert&mode=download"
                End If
            End If

            If dt.Columns.Contains("NationalCardFileName") AndAlso Not IsDBNull(row("NationalCardFileName")) Then
                Dim fn As String = row("NationalCardFileName").ToString()
                If Not String.IsNullOrEmpty(fn) Then
                    pnlOldNationalCard.Visible = True
                    lblOldNationalCardName.Text = fn
                    lblNationalCardStatus.Text = "✅ فایل فعلی موجود است."
                    lblNationalCardStatus.CssClass = "upload-status"
                    hfNationalCardUploaded.Value = "1"
                    lnkViewOldNationalCard.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=nationalcard&mode=view"
                    lnkDownloadOldNationalCard.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=nationalcard&mode=download"
                End If
            End If

            If dt.Columns.Contains("StudentCardFileName") AndAlso Not IsDBNull(row("StudentCardFileName")) Then
                Dim fn As String = row("StudentCardFileName").ToString()
                If Not String.IsNullOrEmpty(fn) Then
                    pnlOldStudentCard.Visible = True
                    lblOldStudentCardName.Text = fn
                    lblStudentCardStatus.Text = "✅ فایل فعلی موجود است."
                    lblStudentCardStatus.CssClass = "upload-status"
                    hfStudentCardUploaded.Value = "1"
                    lnkViewOldStudentCard.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=studentcard&mode=view"
                    lnkDownloadOldStudentCard.HRef = ResolveUrl("~/DownloadMyChildDoc.ashx") & "?id=" & requestID & "&type=studentcard&mode=download"
                End If
            End If

            Dim extraDocType As String = If(IsDBNull(row("ExtraDocumentType")), "", row("ExtraDocumentType").ToString())
            If Not String.IsNullOrEmpty(extraDocType) Then
                hfRequiresExtraDoc.Value = "1"
                hfExtraDocType.Value = extraDocType
                ShowPanel(pnlExtraDocument, "chr-extra-doc-panel")
                lblExtraDocTitle.Text = "📑 " & extraDocType
                lblExtraDocName.Text = extraDocType
                lblExtraDocDescription.Text = "لطفاً در صورت نیاز فایل جدید بارگذاری کنید."
            End If

            Dim levelIDForCheck As Integer = 0
            If Not IsDBNull(row("EducationLevelID")) Then
                Integer.TryParse(row("EducationLevelID").ToString(), levelIDForCheck)
            End If

            If levelIDForCheck > 0 Then
                Dim dtLevel As DataTable = GetDataTable(
                    "SELECT RowNumber FROM exmChildConditions WHERE ID = @ID",
                    New SqlParameter("@ID", levelIDForCheck))

                If dtLevel.Rows.Count > 0 Then
                    Dim rowNum As Integer = Convert.ToInt32(dtLevel.Rows(0)("RowNumber"))
                    Dim isPrimary As Boolean = (rowNum = 1 OrElse rowNum = 2)

                    If isPrimary Then
                        ShowPanel(pnlPrimaryBox, "chr-primary-box")
                        HidePanel(pnlAverageRow, "")
                        txtAverage.Text = ""
                        ShowPanel(pnlTranscriptUpload, "")
                    Else
                        HidePanel(pnlPrimaryBox, "chr-primary-box")
                        ShowPanel(pnlAverageRow, "")

                        If ddlChildType.SelectedValue = "دانشجو" Then
                            ShowPanel(pnlUniversity, "")
                            ShowPanel(pnlStudentCard, "chr-student-card-panel")
                            HidePanel(pnlTranscriptUpload, "")
                        Else
                            ShowPanel(pnlTranscriptUpload, "")
                        End If
                    End If
                End If
            End If

            LoadChildren()

            Dim script As String = "setTimeout(function(){ var el = document.querySelector('.chr-edit-banner'); if(el) el.scrollIntoView({behavior:'smooth', block:'start'}); }, 500);"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "scrollToEdit_" & Guid.NewGuid().ToString("N"), script, True)

        Catch ex As Exception
            ShowToast("error", "خطا در بارگذاری درخواست: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        ResetAfterSubmit()
        ShowToast("info", "ویرایش لغو شد")
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If RequestSubmitted Then Return
            If Not ValidateForm() Then Return
            RequestSubmitted = True
            SaveRequest()
        Catch ex As Exception
            RequestSubmitted = False
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ اعتبارسنجی فرم — با مدارک اجباری جدید
    ' ============================================================
    Private Function ValidateForm() As Boolean
        Dim isEditMode As Boolean = (hfEditingRequestID.Value <> "0" AndAlso hfEditingRequestID.Value <> "")

        If String.IsNullOrEmpty(hfSelectedChildID.Value) OrElse hfSelectedChildID.Value = "0" Then
            ShowModalAlert("warning", "انتخاب فرزند", "لطفاً یک فرزند را از لیست انتخاب کنید.")
            Return False
        End If

        If String.IsNullOrEmpty(ddlChildType.SelectedValue) Then
            ShowModalAlert("warning", "نوع تحصیل", "لطفاً نوع تحصیل را انتخاب کنید.")
            Return False
        End If

        If String.IsNullOrEmpty(ddlEducationLevel.SelectedValue) Then
            ShowModalAlert("warning", "مقطع تحصیلی", "لطفاً مقطع تحصیلی را انتخاب کنید.")
            Return False
        End If

        ' ✅ بررسی مدارک هویتی اجباری
        Dim hasBirthCert As Boolean = (Session("TempBirthCert_" & _personCode & "_Path") IsNot Nothing) OrElse (hfBirthCertUploaded.Value = "1")
        If Not hasBirthCert Then
            ShowModalAlert("warning", "شناسنامه الزامی",
                "لطفاً تصویر صفحه اول شناسنامه فرزند را در بخش مدارک هویتی بارگذاری کنید.")
            Return False
        End If

        Dim hasNationalCard As Boolean = (Session("TempNationalCard_" & _personCode & "_Path") IsNot Nothing) OrElse (hfNationalCardUploaded.Value = "1")
        If Not hasNationalCard Then
            ShowModalAlert("warning", "کارت ملی الزامی",
                "لطفاً تصویر کارت ملی فرزند را در بخش مدارک هویتی بارگذاری کنید.")
            Return False
        End If

        Dim isStudent As Boolean = (ddlChildType.SelectedValue = "دانشجو")

        ' ✅ بررسی کارت دانشجویی برای دانشجو
        If isStudent Then
            Dim hasStudentCard As Boolean = (Session("TempStudentCard_" & _personCode & "_Path") IsNot Nothing) OrElse (hfStudentCardUploaded.Value = "1")
            If Not hasStudentCard Then
                ShowModalAlert("warning", "کارت دانشجویی الزامی",
                    "لطفاً تصویر کارت دانشجویی را در بخش مدارک بارگذاری کنید.")
                Return False
            End If

            ' ✅ اطلاعات دانشگاه اجباری
            If String.IsNullOrEmpty(txtUniversityName.Text.Trim()) Then
                ShowModalAlert("warning", "نام دانشگاه الزامی", "لطفاً نام دانشگاه/مؤسسه را وارد کنید.")
                Return False
            End If

            If String.IsNullOrEmpty(ddlUniversityType.SelectedValue) Then
                ShowModalAlert("warning", "نوع دانشگاه الزامی", "لطفاً نوع دانشگاه/مؤسسه را انتخاب کنید.")
                Return False
            End If

            If String.IsNullOrEmpty(txtUniversityPhone.Text.Trim()) Then
                ShowModalAlert("warning", "شماره تماس دانشگاه الزامی", "لطفاً شماره تماس دانشگاه را وارد کنید.")
                Return False
            End If

            If String.IsNullOrEmpty(txtStudentNumber.Text.Trim()) Then
                ShowModalAlert("warning", "شماره دانشجویی الزامی", "لطفاً شماره دانشجویی را وارد کنید.")
                Return False
            End If

            If String.IsNullOrEmpty(ddlAdmissionType.SelectedValue) Then
                ShowModalAlert("warning", "نوع ورود به دانشگاه الزامی",
                    "لطفاً نوع ورود به دانشگاه (با آزمون / بدون آزمون) را انتخاب کنید.")
                Return False
            End If

            If ddlAdmissionType.SelectedValue = "WithExam" Then
                If String.IsNullOrEmpty(txtRank.Text.Trim()) Then
                    ShowModalAlert("warning", "رتبه آزمون الزامی",
                        "لطفاً رتبه آزمون سراسری را وارد کنید.")
                    Return False
                End If
            ElseIf ddlAdmissionType.SelectedValue = "WithoutExam" Then
                If String.IsNullOrEmpty(ddlWithoutExamReason.SelectedValue) Then
                    ShowModalAlert("warning", "دلیل پذیرش بدون آزمون الزامی",
                        "لطفاً دلیل پذیرش بدون آزمون را انتخاب کنید.")
                    Return False
                End If
            End If
        Else
            ' ✅ اطلاعات مدرسه اجباری برای دانش‌آموز
            If String.IsNullOrEmpty(txtSchoolName.Text.Trim()) Then
                ShowModalAlert("warning", "نام مدرسه الزامی", "لطفاً نام مدرسه را وارد کنید.")
                Return False
            End If

            If String.IsNullOrEmpty(ddlSchoolType.SelectedValue) Then
                ShowModalAlert("warning", "نوع مدرسه الزامی", "لطفاً نوع مدرسه را انتخاب کنید.")
                Return False
            End If

            If String.IsNullOrEmpty(txtSchoolPhone.Text.Trim()) Then
                ShowModalAlert("warning", "شماره تماس مدرسه الزامی", "لطفاً شماره تماس مدرسه را وارد کنید.")
                Return False
            End If

            ' ✅ بررسی کارنامه برای دانش‌آموز
            Dim hasTranscript As Boolean = (Session("TempTranscript_" & _personCode & "_Path") IsNot Nothing) OrElse (hfTranscriptUploaded.Value = "1")
            If Not hasTranscript Then
                ShowModalAlert("warning", "کارنامه الزامی",
                    "لطفاً تصویر کارنامه دانش‌آموز را در بخش مدارک بارگذاری کنید.")
                Return False
            End If
        End If

        If Not isEditMode Then
            Dim sponsorshipID As Integer = Convert.ToInt32(hfSelectedChildID.Value)
            Dim dtCheck As DataTable = GetDataTable(
                "SELECT COUNT(*) FROM exmChildRequests " &
                "WHERE PersonCode = @Code AND SponsorshipID = @SponsorID " &
                "  AND AcademicYear = @Year AND StatusCode NOT IN (4, 5)",
                New SqlParameter("@Code", _personCode),
                New SqlParameter("@SponsorID", sponsorshipID),
                New SqlParameter("@Year", ACADEMIC_YEAR))

            If Convert.ToInt32(dtCheck.Rows(0)(0)) > 0 Then
                ShowModalAlert("warning", "قبلاً ثبت شده", "برای این فرزند در سال تحصیلی جاری قبلاً درخواست ثبت شده است.")
                Return False
            End If
        End If

        Dim isPrimary As Boolean = (pnlPrimaryBox.CssClass.IndexOf("chr-hidden") = -1) AndAlso (pnlPrimaryBox.CssClass.IndexOf("chr-primary-box") >= 0)

        If Not isPrimary Then
            Dim avg As Decimal = 0
            If Not Decimal.TryParse(txtAverage.Text.Trim(), avg) Then
                ShowModalAlert("warning", "معدل", "لطفاً معدل را به‌درستی وارد کنید (مثال: 18.50)")
                Return False
            End If
            If avg < 0 OrElse avg > 20 Then
                ShowModalAlert("warning", "معدل", "معدل باید بین 0 تا 20 باشد.")
                Return False
            End If

            Dim minAvg As Decimal = 0
            Dim warningMsg As String = ""
            If Not CheckAverageWithTolerance(Convert.ToInt32(ddlEducationLevel.SelectedValue), avg, minAvg, warningMsg) Then
                ShowModalAlert("error", "معدل کمتر از حد نصاب",
                    "معدل شما (" & avg.ToString("0.00") & ") کمتر از حد نصاب (" & minAvg.ToString("0.00") & ") است.")
                Return False
            End If
        End If

        If hfRequiresExtraDoc.Value = "1" Then
            Dim hasOldExtra As Boolean = (hfExtraDocUploaded.Value = "1")
            Dim hasNewExtra As Boolean = (Session("TempExtraDoc_" & _personCode & "_Path") IsNot Nothing)
            If Not hasOldExtra AndAlso Not hasNewExtra Then
                ShowModalAlert("warning", "مدرک اضافی",
                    "لطفاً مدرک اضافی مورد نیاز («" & hfExtraDocType.Value & "») را بارگذاری کنید.")
                Return False
            End If
        End If

        Return True
    End Function

    Private Function CheckAverageWithTolerance(ByVal levelID As Integer, ByVal average As Decimal,
                                                ByRef minAverage As Decimal,
                                                ByRef warningMessage As String) As Boolean
        Try
            warningMessage = ""
            minAverage = 0
            Dim dt As DataTable = GetDataTable(
                "SELECT MinAverage FROM exmChildConditions WHERE ID = @ID",
                New SqlParameter("@ID", levelID))
            If dt.Rows.Count = 0 Then Return True
            If dt.Rows(0)("MinAverage") Is DBNull.Value Then Return True
            minAverage = Convert.ToDecimal(dt.Rows(0)("MinAverage"))
            If average >= minAverage Then Return True
            Dim allowedMin As Decimal = minAverage - TOLERANCE_VALUE
            If average >= allowedMin Then
                warningMessage = "ارفاق نیم نمره"
                Return True
            End If
            warningMessage = "کمتر از حد مجاز"
            Return False
        Catch
            Return True
        End Try
    End Function

    ' ============================================================
    ' ✅ ذخیره درخواست — با فیلدهای جدید
    ' ============================================================
    Private Sub SaveRequest()
        Try
            Dim isStudent As Boolean = (ddlChildType.SelectedValue = "دانشجو")
            Dim sponsorshipID As Integer = Convert.ToInt32(hfSelectedChildID.Value)
            Dim isEditMode As Boolean = (hfEditingRequestID.Value <> "0" AndAlso hfEditingRequestID.Value <> "")
            Dim isPrimary As Boolean = (pnlPrimaryBox.CssClass.IndexOf("chr-hidden") = -1) AndAlso (pnlPrimaryBox.CssClass.IndexOf("chr-primary-box") >= 0)

            Dim dtChild As DataTable = GetDataTable(
                "SELECT s.fstName + ' ' + s.Name AS ChildName, " &
                "ISNULL(s.codeMeli, '') AS NationalCode, " &
                "ISNULL(s.birthday, '') AS BirthDate, s.relation AS RelationID " &
                "FROM Sponsorship s WHERE s.ID = @ID",
                New SqlParameter("@ID", sponsorshipID))

            If dtChild.Rows.Count = 0 Then
                ShowModalAlert("error", "خطا", "فرزند یافت نشد")
                RequestSubmitted = False
                Return
            End If

            Dim childRow As DataRow = dtChild.Rows(0)
            Dim childName As String = childRow("ChildName").ToString()
            Dim nationalCode As String = childRow("NationalCode").ToString()
            Dim birthDate As String = childRow("BirthDate").ToString()
            Dim relationID As Integer = Convert.ToInt32(childRow("RelationID"))
            Dim gender As String = If(relationID = 1, "پسر", "دختر")

            Dim transcriptFile As String = ""
            Dim extraDocFile As String = ""
            Dim birthCertFile As String = ""
            Dim nationalCardFile As String = ""
            Dim studentCardFile As String = ""

            ' ✅ بازیابی فایل‌های قدیمی در حالت ویرایش
            If isEditMode Then
                Dim editID As Integer = Convert.ToInt32(hfEditingRequestID.Value)
                Dim dtOld As DataTable = GetDataTable(
                    "SELECT TranscriptFileName, ExtraDocumentFileName, BirthCertificateFileName, " &
                    "       NationalCardFileName, StudentCardFileName " &
                    "FROM exmChildRequests WHERE ID = @ID",
                    New SqlParameter("@ID", editID))
                If dtOld.Rows.Count > 0 Then
                    If dtOld.Rows(0)("TranscriptFileName") IsNot DBNull.Value Then transcriptFile = dtOld.Rows(0)("TranscriptFileName").ToString()
                    If dtOld.Rows(0)("ExtraDocumentFileName") IsNot DBNull.Value Then extraDocFile = dtOld.Rows(0)("ExtraDocumentFileName").ToString()
                    If dtOld.Columns.Contains("BirthCertificateFileName") AndAlso dtOld.Rows(0)("BirthCertificateFileName") IsNot DBNull.Value Then
                        birthCertFile = dtOld.Rows(0)("BirthCertificateFileName").ToString()
                    End If
                    If dtOld.Columns.Contains("NationalCardFileName") AndAlso dtOld.Rows(0)("NationalCardFileName") IsNot DBNull.Value Then
                        nationalCardFile = dtOld.Rows(0)("NationalCardFileName").ToString()
                    End If
                    If dtOld.Columns.Contains("StudentCardFileName") AndAlso dtOld.Rows(0)("StudentCardFileName") IsNot DBNull.Value Then
                        studentCardFile = dtOld.Rows(0)("StudentCardFileName").ToString()
                    End If
                End If
            End If

            ' ✅ انتقال فایل‌های جدید از Temp
            If Session("TempBirthCert_" & _personCode & "_Path") IsNot Nothing Then
                Dim newFile As String = MoveFileFromTemp("TempBirthCert_" & _personCode, childName, "شناسنامه")
                If Not String.IsNullOrEmpty(newFile) Then birthCertFile = newFile
            End If

            If Session("TempNationalCard_" & _personCode & "_Path") IsNot Nothing Then
                Dim newFile As String = MoveFileFromTemp("TempNationalCard_" & _personCode, childName, "کارت-ملی")
                If Not String.IsNullOrEmpty(newFile) Then nationalCardFile = newFile
            End If

            If isStudent AndAlso Session("TempStudentCard_" & _personCode & "_Path") IsNot Nothing Then
                Dim newFile As String = MoveFileFromTemp("TempStudentCard_" & _personCode, childName, "کارت-دانشجویی")
                If Not String.IsNullOrEmpty(newFile) Then studentCardFile = newFile
            End If

            If Not isStudent AndAlso Session("TempTranscript_" & _personCode & "_Path") IsNot Nothing Then
                Dim newFile As String = MoveFileFromTemp("TempTranscript_" & _personCode, childName, "کارنامه")
                If Not String.IsNullOrEmpty(newFile) Then transcriptFile = newFile
            End If

            Dim docTypeForName As String = hfExtraDocType.Value
            If String.IsNullOrEmpty(docTypeForName) Then docTypeForName = "مدرک-اضافی"

            If Session("TempExtraDoc_" & _personCode & "_Path") IsNot Nothing Then
                Dim newExtra As String = MoveFileFromTemp("TempExtraDoc_" & _personCode, childName, docTypeForName)
                If Not String.IsNullOrEmpty(newExtra) Then extraDocFile = newExtra
            End If

            Dim avg As Object = DBNull.Value
            Dim toleranceNote As String = ""

            If Not isPrimary Then
                Dim avgValue As Decimal = Decimal.Parse(txtAverage.Text.Trim())
                avg = avgValue
                Dim minAvg As Decimal = 0
                Dim warningMsg As String = ""
                Dim isOk As Boolean = CheckAverageWithTolerance(
                    Convert.ToInt32(ddlEducationLevel.SelectedValue), avgValue, minAvg, warningMsg)
                If isOk AndAlso avgValue < minAvg Then
                    toleranceNote = "⚠️ ارفاق نیم نمره - معدل: " & avgValue.ToString("0.00") &
                                    " / حد نصاب: " & minAvg.ToString("0.00")
                End If
            Else
                toleranceNote = "📘 مقطع ابتدایی - ارزیابی از طریق کارنامه"
            End If

            Dim newRequestID As String = ""

            If isEditMode Then
                Dim editID As Integer = Convert.ToInt32(hfEditingRequestID.Value)
                Dim sqlUpdate As String =
                    "UPDATE exmChildRequests SET " &
                    "  FullName = @FullName, UnitName = @UnitName, Mobile = @Mobile, " &
                    "  ChildFullName = @ChildFullName, ChildNationalCode = @ChildNationalCode, " &
                    "  ChildBirthDate = @ChildBirthDate, ChildGender = @ChildGender, " &
                    "  ChildType = @ChildType, EducationLevel = @EducationLevel, EducationLevelID = @EducationLevelID, " &
                    "  Average = @Average, " &
                    "  SchoolName = @SchoolName, SchoolType = @SchoolType, SchoolPhone = @SchoolPhone, " &
                    "  UniversityName = @UniversityName, UniversityType = @UniversityType, UniversityPhone = @UniversityPhone, " &
                    "  StudentNumber = @StudentNumber, AdmissionType = @AdmissionType, WithoutExamReason = @WithoutExamReason, " &
                    "  RankInExam = @RankInExam, " &
                    "  IsSampad = @IsSampad, SampadSchoolName = @SampadSchoolName, SampadStudentNumber = @SampadStudentNumber, " &
                    "  TranscriptFileName = @TranscriptFile, TranscriptFilePath = @TranscriptPath, " &
                    "  ExtraDocumentFileName = @ExtraDocFile, ExtraDocumentFilePath = @ExtraDocPath, " &
                    "  ExtraDocumentType = @ExtraDocType, " &
                    "  BirthCertificateFileName = @BirthCertFile, BirthCertificateFilePath = @BirthCertPath, " &
                    "  NationalCardFileName = @NationalCardFile, NationalCardFilePath = @NationalCardPath, " &
                    "  StudentCardFileName = @StudentCardFile, StudentCardFilePath = @StudentCardPath, " &
                    "  StatusCode = 1, StatusDescription = N'اصلاح شده - در انتظار بررسی روابط عمومی', " &
                    "  ReviewerComment = @ReviewerComment, ReviewDate = NULL, ReviewerCode = NULL, ReviewerName = NULL " &
                    "WHERE ID = @ID"

                Dim affected As Integer = ExecuteNonQuery(sqlUpdate,
                    New SqlParameter("@FullName", _fullName),
                    New SqlParameter("@UnitName", _unitName),
                    New SqlParameter("@Mobile", _mobile),
                    New SqlParameter("@ChildFullName", childName),
                    New SqlParameter("@ChildNationalCode", nationalCode),
                    New SqlParameter("@ChildBirthDate", birthDate),
                    New SqlParameter("@ChildGender", gender),
                    New SqlParameter("@ChildType", ddlChildType.SelectedValue),
                    New SqlParameter("@EducationLevel", ddlEducationLevel.SelectedItem.Text),
                    New SqlParameter("@EducationLevelID", ddlEducationLevel.SelectedValue),
                    New SqlParameter("@Average", avg),
                    New SqlParameter("@SchoolName", ToDbNull(txtSchoolName.Text.Trim())),
                    New SqlParameter("@SchoolType", ToDbNull(ddlSchoolType.SelectedValue)),
                    New SqlParameter("@SchoolPhone", ToDbNull(txtSchoolPhone.Text.Trim())),
                    New SqlParameter("@UniversityName", ToDbNull(txtUniversityName.Text.Trim())),
                    New SqlParameter("@UniversityType", ToDbNull(ddlUniversityType.SelectedValue)),
                    New SqlParameter("@UniversityPhone", ToDbNull(txtUniversityPhone.Text.Trim())),
                    New SqlParameter("@StudentNumber", ToDbNull(txtStudentNumber.Text.Trim())),
                    New SqlParameter("@AdmissionType", ToDbNull(ddlAdmissionType.SelectedValue)),
                    New SqlParameter("@WithoutExamReason", ToDbNull(ddlWithoutExamReason.SelectedValue)),
                    New SqlParameter("@RankInExam", ToDbNullInt(txtRank.Text.Trim())),
                    New SqlParameter("@IsSampad", chkSampad.Checked),
                    New SqlParameter("@SampadSchoolName", ToDbNull(txtSampadSchoolName.Text.Trim())),
                    New SqlParameter("@SampadStudentNumber", ToDbNull(txtSampadStudentNumber.Text.Trim())),
                    New SqlParameter("@TranscriptFile", ToDbNull(transcriptFile)),
                    New SqlParameter("@TranscriptPath", ToDbNull(transcriptFile)),
                    New SqlParameter("@ExtraDocFile", ToDbNull(extraDocFile)),
                    New SqlParameter("@ExtraDocPath", ToDbNull(extraDocFile)),
                    New SqlParameter("@ExtraDocType", ToDbNull(hfExtraDocType.Value)),
                    New SqlParameter("@BirthCertFile", ToDbNull(birthCertFile)),
                    New SqlParameter("@BirthCertPath", ToDbNull(birthCertFile)),
                    New SqlParameter("@NationalCardFile", ToDbNull(nationalCardFile)),
                    New SqlParameter("@NationalCardPath", ToDbNull(nationalCardFile)),
                    New SqlParameter("@StudentCardFile", ToDbNull(studentCardFile)),
                    New SqlParameter("@StudentCardPath", ToDbNull(studentCardFile)),
                    New SqlParameter("@ReviewerComment", ToDbNull(toleranceNote)),
                    New SqlParameter("@ID", editID))

                If affected > 0 Then
                    newRequestID = editID.ToString()
                    ShowEditSuccessModal(newRequestID)
                Else
                    ShowModalAlert("error", "خطا", "به‌روزرسانی انجام نشد.")
                    RequestSubmitted = False
                    Return
                End If
            Else
                Dim sqlInsert As String =
                    "INSERT INTO exmChildRequests (" &
                    "PersonCode, FullName, UnitName, Mobile, " &
                    "SponsorshipID, ChildFullName, ChildNationalCode, ChildBirthDate, ChildGender, " &
                    "ChildType, EducationLevel, EducationLevelID, Average, " &
                    "SchoolName, SchoolType, SchoolPhone, " &
                    "UniversityName, UniversityType, UniversityPhone, StudentNumber, " &
                    "AdmissionType, WithoutExamReason, RankInExam, " &
                    "IsSampad, SampadSchoolName, SampadStudentNumber, " &
                    "AcademicYear, " &
                    "TranscriptFileName, TranscriptFilePath, " &
                    "ExtraDocumentFileName, ExtraDocumentFilePath, ExtraDocumentType, " &
                    "BirthCertificateFileName, BirthCertificateFilePath, " &
                    "NationalCardFileName, NationalCardFilePath, " &
                    "StudentCardFileName, StudentCardFilePath, " &
                    "StatusCode, StatusDescription, ReviewerComment, CreateDate, CreateUserID" &
                    ") VALUES (" &
                    "@PersonCode, @FullName, @UnitName, @Mobile, " &
                    "@SponsorshipID, @ChildFullName, @ChildNationalCode, @ChildBirthDate, @ChildGender, " &
                    "@ChildType, @EducationLevel, @EducationLevelID, @Average, " &
                    "@SchoolName, @SchoolType, @SchoolPhone, " &
                    "@UniversityName, @UniversityType, @UniversityPhone, @StudentNumber, " &
                    "@AdmissionType, @WithoutExamReason, @RankInExam, " &
                    "@IsSampad, @SampadSchoolName, @SampadStudentNumber, " &
                    "@AcademicYear, " &
                    "@TranscriptFile, @TranscriptPath, " &
                    "@ExtraDocFile, @ExtraDocPath, @ExtraDocType, " &
                    "@BirthCertFile, @BirthCertPath, " &
                    "@NationalCardFile, @NationalCardPath, " &
                    "@StudentCardFile, @StudentCardPath, " &
                    "@StatusCode, @StatusDescription, @ReviewerComment, GETDATE(), @CreateUserID" &
                    "); SELECT SCOPE_IDENTITY();"

                Dim newID As Object = ExecuteScalar(sqlInsert,
                    New SqlParameter("@PersonCode", _personCode),
                    New SqlParameter("@FullName", _fullName),
                    New SqlParameter("@UnitName", _unitName),
                    New SqlParameter("@Mobile", _mobile),
                    New SqlParameter("@SponsorshipID", sponsorshipID),
                    New SqlParameter("@ChildFullName", childName),
                    New SqlParameter("@ChildNationalCode", nationalCode),
                    New SqlParameter("@ChildBirthDate", birthDate),
                    New SqlParameter("@ChildGender", gender),
                    New SqlParameter("@ChildType", ddlChildType.SelectedValue),
                    New SqlParameter("@EducationLevel", ddlEducationLevel.SelectedItem.Text),
                    New SqlParameter("@EducationLevelID", ddlEducationLevel.SelectedValue),
                    New SqlParameter("@Average", avg),
                    New SqlParameter("@SchoolName", ToDbNull(txtSchoolName.Text.Trim())),
                    New SqlParameter("@SchoolType", ToDbNull(ddlSchoolType.SelectedValue)),
                    New SqlParameter("@SchoolPhone", ToDbNull(txtSchoolPhone.Text.Trim())),
                    New SqlParameter("@UniversityName", ToDbNull(txtUniversityName.Text.Trim())),
                    New SqlParameter("@UniversityType", ToDbNull(ddlUniversityType.SelectedValue)),
                    New SqlParameter("@UniversityPhone", ToDbNull(txtUniversityPhone.Text.Trim())),
                    New SqlParameter("@StudentNumber", ToDbNull(txtStudentNumber.Text.Trim())),
                    New SqlParameter("@AdmissionType", ToDbNull(ddlAdmissionType.SelectedValue)),
                    New SqlParameter("@WithoutExamReason", ToDbNull(ddlWithoutExamReason.SelectedValue)),
                    New SqlParameter("@RankInExam", ToDbNullInt(txtRank.Text.Trim())),
                    New SqlParameter("@IsSampad", chkSampad.Checked),
                    New SqlParameter("@SampadSchoolName", ToDbNull(txtSampadSchoolName.Text.Trim())),
                    New SqlParameter("@SampadStudentNumber", ToDbNull(txtSampadStudentNumber.Text.Trim())),
                    New SqlParameter("@AcademicYear", ACADEMIC_YEAR),
                    New SqlParameter("@TranscriptFile", ToDbNull(transcriptFile)),
                    New SqlParameter("@TranscriptPath", ToDbNull(transcriptFile)),
                    New SqlParameter("@ExtraDocFile", ToDbNull(extraDocFile)),
                    New SqlParameter("@ExtraDocPath", ToDbNull(extraDocFile)),
                    New SqlParameter("@ExtraDocType", ToDbNull(hfExtraDocType.Value)),
                    New SqlParameter("@BirthCertFile", ToDbNull(birthCertFile)),
                    New SqlParameter("@BirthCertPath", ToDbNull(birthCertFile)),
                    New SqlParameter("@NationalCardFile", ToDbNull(nationalCardFile)),
                    New SqlParameter("@NationalCardPath", ToDbNull(nationalCardFile)),
                    New SqlParameter("@StudentCardFile", ToDbNull(studentCardFile)),
                    New SqlParameter("@StudentCardPath", ToDbNull(studentCardFile)),
                    New SqlParameter("@StatusCode", 1),
                    New SqlParameter("@StatusDescription", "در انتظار بررسی روابط عمومی"),
                    New SqlParameter("@ReviewerComment", ToDbNull(toleranceNote)),
                    New SqlParameter("@CreateUserID", _personCode))

                newRequestID = newID.ToString()
                ShowSuccessModal(newRequestID)
            End If

            ResetAfterSubmit()

        Catch ex As Exception
            RequestSubmitted = False
            ShowModalAlert("error", "خطا در ذخیره", ex.Message)
        End Try
    End Sub

    Private Function ToDbNull(ByVal value As String) As Object
        If String.IsNullOrEmpty(value) Then Return DBNull.Value
        Return value
    End Function

    Private Function ToDbNullInt(ByVal value As String) As Object
        Dim result As Integer
        If Integer.TryParse(value, result) Then Return result
        Return DBNull.Value
    End Function

    Private Sub ResetAfterSubmit()
        lblTranscriptStatus.Text = ""
        lblExtraDocStatus.Text = ""
        lblBirthCertStatus.Text = ""
        lblNationalCardStatus.Text = ""
        lblStudentCardStatus.Text = ""
        HidePanel(pnlEducation, "chr-section education")
        HidePanel(pnlDocuments, "chr-section documents")
        pnlEditBanner.Visible = False
        pnlEditHistory.Visible = False
        HidePanel(pnlAddChild, "chr-add-child-panel")
        HidePanel(pnlEditChild, "chr-add-child-panel")
        lblEditMode.Visible = False
        pnlOldTranscript.Visible = False
        pnlOldExtraDoc.Visible = False
        pnlOldBirthCert.Visible = False
        pnlOldNationalCard.Visible = False
        pnlOldStudentCard.Visible = False

        hfSelectedChildID.Value = "0"
        hfEditingRequestID.Value = "0"
        hfEditingChildID.Value = "0"
        hfRequiresExtraDoc.Value = "0"
        hfExtraDocType.Value = ""
        hfTranscriptUploaded.Value = "0"
        hfExtraDocUploaded.Value = "0"
        hfBirthCertUploaded.Value = "0"
        hfNationalCardUploaded.Value = "0"
        hfStudentCardUploaded.Value = "0"
        hfSampadSelected.Value = "0"

        btnSubmit.Text = "📤 ارسال نهایی"
        btnSubmit.CssClass = "chr-btn chr-btn-submit"

        RequestSubmitted = False
        ClearTempFiles()

        LoadChildren()
        LoadHistory()

        upMain.Update()
        upHistory.Update()
    End Sub

    Private Sub LoadEditHistory(ByVal requestID As Integer)
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT StatusCode, StatusDescription, ReviewerName, ReviewerCode, ReviewerComment, ReviewDate, CreateDate " &
                "FROM exmChildRequestsHistory WHERE RequestID = @RequestID ORDER BY CreateDate DESC",
                New SqlParameter("@RequestID", requestID))

            If dt.Rows.Count > 0 Then
                pnlEditHistory.Visible = True
                rptEditHistory.DataSource = dt
                rptEditHistory.DataBind()
            Else
                pnlEditHistory.Visible = False
            End If
        Catch
            pnlEditHistory.Visible = False
        End Try
    End Sub

    Protected Function GetHistoryStatusText(ByVal statusCode As Object) As String
        Try
            Dim code As Integer = Convert.ToInt32(statusCode)
            Select Case code
                Case 1 : Return "⏳ در انتظار بررسی"
                Case 2 : Return "📝 نیاز به اصلاح"
                Case 3 : Return "✅ تایید شده"
                Case 4 : Return "❌ رد شده"
                Case 5 : Return "🚫 انصراف"
                Case Else : Return "نامشخص"
            End Select
        Catch
            Return "نامشخص"
        End Try
    End Function

    Protected Function GetHistoryDateFa(ByVal reviewDate As Object, ByVal createDate As Object) As String
        Try
            If reviewDate IsNot Nothing AndAlso Not IsDBNull(reviewDate) Then
                Return GetPersianDateTime(Convert.ToDateTime(reviewDate))
            ElseIf createDate IsNot Nothing AndAlso Not IsDBNull(createDate) Then
                Return GetPersianDateTime(Convert.ToDateTime(createDate))
            Else
                Return "-"
            End If
        Catch
            Return "-"
        End Try
    End Function

    Protected Function GetHistoryReviewerName(ByVal name As Object, ByVal code As Object) As String
        Try
            Dim n As String = If(name Is Nothing OrElse IsDBNull(name), "", name.ToString().Trim())
            Dim c As String = If(code Is Nothing OrElse IsDBNull(code), "", code.ToString().Trim())
            If Not String.IsNullOrEmpty(n) AndAlso Not String.IsNullOrEmpty(c) Then
                Return n & " (" & c & ")"
            ElseIf Not String.IsNullOrEmpty(n) Then
                Return n
            ElseIf Not String.IsNullOrEmpty(c) Then
                Return c
            Else
                Return "—"
            End If
        Catch
            Return "—"
        End Try
    End Function

    Protected Function GetHistoryReasonText(ByVal reason As Object) As String
        Try
            If reason Is Nothing OrElse IsDBNull(reason) Then Return "—"
            Dim r As String = reason.ToString().Trim()
            If String.IsNullOrEmpty(r) Then Return "—"
            Return Server.HtmlEncode(r)
        Catch
            Return "—"
        End Try
    End Function

    Protected Function GetHistoryCommentText(ByVal comment As Object) As String
        Try
            If comment Is Nothing OrElse IsDBNull(comment) Then Return "—"
            Dim c As String = comment.ToString().Trim()
            If String.IsNullOrEmpty(c) Then Return "—"
            Return Server.HtmlEncode(c)
        Catch
            Return "—"
        End Try
    End Function

    Protected Function GetHistoryItemClass(ByVal statusCode As Object) As String
        Try
            Dim code As Integer = Convert.ToInt32(statusCode)
            Select Case code
                Case 2 : Return "chr-history-item needs-edit"
                Case 3 : Return "chr-history-item approved"
                Case 4 : Return "chr-history-item rejected"
                Case Else : Return "chr-history-item"
            End Select
        Catch
            Return "chr-history-item"
        End Try
    End Function

    Protected Function GetHistoryReasonClass(ByVal statusCode As Object) As String
        Try
            Dim code As Integer = Convert.ToInt32(statusCode)
            Select Case code
                Case 4 : Return "chr-history-reason for-rejected"
                Case Else : Return "chr-history-reason"
            End Select
        Catch
            Return "chr-history-reason"
        End Try
    End Function

    Protected Function GetHistoryReasonLabel(ByVal statusCode As Object) As String
        Try
            Dim code As Integer = Convert.ToInt32(statusCode)
            Select Case code
                Case 2 : Return "📋 دلیل نیاز به اصلاح:"
                Case 4 : Return "❌ دلیل عدم تایید:"
                Case Else : Return "📋 دلیل:"
            End Select
        Catch
            Return "📋 دلیل:"
        End Try
    End Function

    Private Function GetPersianDateTime(ByVal dt As DateTime) As String
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

    Private Function GetPersianDate(ByVal dt As DateTime) As String
        Try
            Dim pc As New PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" &
                   pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" &
                   pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c)
        Catch
            Return dt.ToString("yyyy/MM/dd")
        End Try
    End Function

    Private Sub ShowSuccessModal(ByVal requestID As String)
        Dim body As New System.Text.StringBuilder()
        body.Append("<div style='text-align:center;direction:rtl;'>")
        body.Append("<p style='font-size:15px;color:#2c3e50;margin-bottom:15px;font-weight:600;'>درخواست شما با موفقیت ثبت شد</p>")
        body.Append("<div style='background:#e8f4fd;padding:12px 20px;border-radius:10px;margin:15px 0;'>")
        body.Append("<p style='font-size:13px;color:#0c5460;margin:0;'><strong>کد پیگیری:</strong> " & requestID & "</p></div>")
        body.Append("<div style='background:#fff3cd;padding:12px 15px;border-radius:10px;margin:15px 0;'>")
        body.Append("<p style='font-size:12px;color:#856404;margin:0;'>📋 درخواست شما به واحد روابط عمومی ارسال شد.</p>")
        body.Append("</div></div>")

        Dim safeBody As String = body.ToString().Replace("'", "\'").Replace(vbCr, "").Replace(vbLf, " ")
        Dim script As String = "showModal('success', '✅ ثبت موفق', '" & safeBody & "', 'متوجه شدم');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "successMsg_" & Guid.NewGuid().ToString("N"), script, True)
    End Sub

    Private Sub ShowEditSuccessModal(ByVal requestID As String)
        Dim body As New System.Text.StringBuilder()
        body.Append("<div style='text-align:center;direction:rtl;'>")
        body.Append("<p style='font-size:15px;color:#2c3e50;margin-bottom:15px;font-weight:600;'>✅ درخواست شما با موفقیت اصلاح شد</p>")
        body.Append("<div style='background:#e8f4fd;padding:12px 20px;border-radius:10px;margin:15px 0;'>")
        body.Append("<p style='font-size:13px;color:#0c5460;margin:0;'><strong>کد پیگیری:</strong> " & requestID & "</p></div>")
        body.Append("<div style='background:#fff3cd;padding:12px 15px;border-radius:10px;margin:15px 0;'>")
        body.Append("<p style='font-size:12px;color:#856404;margin:0;'>📋 درخواست اصلاح شده مجدداً به واحد روابط عمومی ارسال شد.</p>")
        body.Append("</div></div>")

        Dim safeBody As String = body.ToString().Replace("'", "\'").Replace(vbCr, "").Replace(vbLf, " ")
        Dim script As String = "showModal('success', '✅ اصلاح موفق', '" & safeBody & "', 'متوجه شدم');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "editSuccess_" & Guid.NewGuid().ToString("N"), script, True)
    End Sub

    Private Sub LoadHistory()
        Try
            Dim sql As String =
                "SELECT TOP 10 cr.ID, cr.ChildFullName, cr.AcademicYear, cr.EducationLevel, cr.StatusCode, " &
                "       cr.StatusDescription AS CurrentReason, " &
                "       cr.ReviewDate, cr.CreateDate, " &
                "       (SELECT TOP 1 h.StatusDescription " &
                "        FROM exmChildRequestsHistory h " &
                "        WHERE h.RequestID = cr.ID " &
                "        ORDER BY h.CreateDate DESC) AS LastHistoryReason, " &
                "       (SELECT TOP 1 h.ReviewerComment " &
                "        FROM exmChildRequestsHistory h " &
                "        WHERE h.RequestID = cr.ID " &
                "        ORDER BY h.CreateDate DESC) AS LastHistoryComment, " &
                "       (SELECT TOP 1 h.ReviewerName " &
                "        FROM exmChildRequestsHistory h " &
                "        WHERE h.RequestID = cr.ID " &
                "        ORDER BY h.CreateDate DESC) AS LastReviewerName, " &
                "CASE cr.StatusCode " &
                "    WHEN 1 THEN N'⏳ در انتظار بررسی' " &
                "    WHEN 2 THEN N'📝 نیاز به اصلاح' " &
                "    WHEN 3 THEN N'✅ تایید شده' " &
                "    WHEN 4 THEN N'❌ رد شده' " &
                "    WHEN 5 THEN N'🚫 انصراف' " &
                "    ELSE N'نامشخص' END AS StatusText " &
                "FROM exmChildRequests cr WHERE cr.PersonCode = @Code ORDER BY cr.CreateDate DESC"

            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@Code", _personCode))

            If dt.Rows.Count > 0 Then
                If Not dt.Columns.Contains("ReasonDisplay") Then
                    dt.Columns.Add("ReasonDisplay", GetType(String))
                End If
                If Not dt.Columns.Contains("HasReason") Then
                    dt.Columns.Add("HasReason", GetType(Boolean))
                End If

                For Each row As DataRow In dt.Rows
                    Dim statusCode As Integer = Convert.ToInt32(row("StatusCode"))
                    Dim currentReason As String = ""
                    Dim historyReason As String = ""
                    Dim historyComment As String = ""
                    Dim reviewerName As String = ""

                    If row("CurrentReason") IsNot DBNull.Value Then currentReason = row("CurrentReason").ToString().Trim()
                    If row("LastHistoryReason") IsNot DBNull.Value Then historyReason = row("LastHistoryReason").ToString().Trim()
                    If row("LastHistoryComment") IsNot DBNull.Value Then historyComment = row("LastHistoryComment").ToString().Trim()
                    If row("LastReviewerName") IsNot DBNull.Value Then reviewerName = row("LastReviewerName").ToString().Trim()

                    If statusCode = 2 OrElse statusCode = 4 Then
                        Dim reason As String = ""
                        If Not String.IsNullOrEmpty(historyReason) Then
                            reason = historyReason
                        ElseIf Not String.IsNullOrEmpty(currentReason) Then
                            reason = currentReason
                        End If

                        If Not String.IsNullOrEmpty(historyComment) AndAlso historyComment <> reason Then
                            If Not String.IsNullOrEmpty(reason) Then
                                reason &= " <br/><span style='color:#7f8c8d;'>📝 " & Server.HtmlEncode(historyComment) & "</span>"
                            Else
                                reason = historyComment
                            End If
                        End If

                        If Not String.IsNullOrEmpty(reviewerName) Then
                            reason &= " <span style='color:#95a5a6;font-size:11px;'>| 👤 " & Server.HtmlEncode(reviewerName) & "</span>"
                        End If

                        If String.IsNullOrEmpty(reason) Then reason = "دلیل توسط کارشناس ذکر نشده است."

                        row("ReasonDisplay") = reason
                        row("HasReason") = True
                    Else
                        row("ReasonDisplay") = ""
                        row("HasReason") = False
                    End If
                Next

                rptHistory.DataSource = dt
                rptHistory.DataBind()
                pnlHistory.Visible = True
                pnlNoHistory.Visible = False
            Else
                pnlHistory.Visible = False
                pnlNoHistory.Visible = True
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("LoadHistory Error: " & ex.Message)
            pnlHistory.Visible = False
            pnlNoHistory.Visible = True
        End Try
    End Sub

    Private Sub ShowToast(ByVal type As String, ByVal message As String)
        Try
            Dim toastType As String = If(type = "danger", "error", type)
            Dim safeMsg As String = SanitizeJsString(message)
            Dim script As String = "showToast('" & toastType & "', '" & safeMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast_" & Guid.NewGuid().ToString("N"), script, True)
        Catch
        End Try
    End Sub

    Private Sub ShowModalAlert(ByVal type As String, ByVal title As String, ByVal message As String)
        Try
            Dim modalType As String = If(type = "danger", "error", type)
            Dim safeTitle As String = SanitizeJsString(title)
            Dim safeMsg As String = SanitizeJsString(message)
            Dim script As String = "showModal('" & modalType & "', '" & safeTitle & "', '" & safeMsg & "', 'متوجه شدم');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "modal_" & Guid.NewGuid().ToString("N"), script, True)
        Catch
        End Try
    End Sub

    Private Function SanitizeJsString(ByVal value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""
        Dim result As String = value
        result = result.Replace("\", "\\").Replace("'", "\'").Replace("""", "\""")
        result = result.Replace(vbCr, "").Replace(vbLf, " ").Replace(vbTab, " ")
        Return result
    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/index.aspx")
    End Sub

End Class