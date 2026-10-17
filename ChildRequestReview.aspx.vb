Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO

Partial Class ChildRequestReview
    Inherits System.Web.UI.Page

    Private Const ACADEMIC_YEAR As String = "1405-1404"
    Private Const DOWNLOAD_HANDLER As String = "~/DownloadChildDoc.ashx"

    Private _personCode As String = ""
    Private _fullName As String = ""
    Private _unitName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("codePersoneli") Is Nothing Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        _personCode = Session("codePersoneli").ToString().Trim()
        _fullName = If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), "")
        _unitName = GetUnitNameByPersonCode(_personCode)

        If Not IsPostBack Then
            LoadStats()
            LoadOpenRequests()
            LoadClosedRequests()
            hfShowModal.Value = "0"
            pnlDetail.Visible = False
            pnlDetailEmpty.Visible = True
            pnlReviewButtons.Visible = False
        Else
            If hfShowModal.Value = "1" AndAlso Session("ReviewingRequestID") IsNot Nothing Then
                Dim reviewingID As Integer = 0
                Integer.TryParse(Session("ReviewingRequestID").ToString(), reviewingID)
                If reviewingID > 0 Then
                    LoadRequestDetail(reviewingID)
                End If
            End If
        End If
    End Sub

    ' ============================================================
    ' ✅ بررسی نقش روابط عمومی
    ' ============================================================
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

    Protected Sub lnkBackToHome_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/index.aspx")
    End Sub

    Protected Sub lnkExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' ✅ پاک کردن وضعیت Modal قبل از خروجی
            hfShowModal.Value = "0"
            Session.Remove("ReviewingRequestID")

            pnlDetail.Visible = False
            pnlDetailEmpty.Visible = True
            pnlReviewButtons.Visible = False

            Session("ExportActiveTab") = hfActiveTab.Value
            If hfActiveTab.Value = "open" Then
                Session("ExportStatusFilter") = ddlStatusOpen.SelectedValue
                Session("ExportTypeFilter") = ddlTypeOpen.SelectedValue
                Session("ExportSearch") = txtSearchOpen.Text.Trim()
            Else
                Session("ExportStatusFilter") = ddlStatusClosed.SelectedValue
                Session("ExportTypeFilter") = ddlTypeClosed.SelectedValue
                Session("ExportSearch") = txtSearchClosed.Text.Trim()
            End If

            Dim url As String = ResolveUrl("~/ExportChildRequests.aspx")
            Dim script As String =
                "closeDetailModal(); " &
                "setTimeout(function(){ window.open('" & url & "', '_blank'); }, 200);"

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openExport_" & Guid.NewGuid().ToString("N"), script, True)

        Catch ex As Exception
            ShowToast("error", "خطا در خروجی Excel: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadStats()
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT StatusCode, COUNT(*) AS Cnt FROM exmChildRequests " &
                "WHERE AcademicYear = @Year GROUP BY StatusCode",
                New SqlParameter("@Year", ACADEMIC_YEAR))

            Dim pending As Integer = 0, correction As Integer = 0
            Dim approved As Integer = 0, rejected As Integer = 0

            For Each row As DataRow In dt.Rows
                Dim cnt As Integer = Convert.ToInt32(row("Cnt"))
                Select Case Convert.ToInt32(row("StatusCode"))
                    Case 1 : pending = cnt
                    Case 2 : correction = cnt
                    Case 3 : approved = cnt
                    Case 4 : rejected = cnt
                End Select
            Next

            lblTabOpenCount.Text = (pending + correction).ToString()
            lblTabClosedCount.Text = (approved + rejected).ToString()
        Catch ex As Exception
            ShowToast("error", "خطا در آمار: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ Load Open Requests — با مبلغ
    ' ============================================================
    Private Sub LoadOpenRequests()
        Try
            Dim search As String = txtSearchOpen.Text.Trim()
            Dim statusFilter As Integer = 0
            Integer.TryParse(ddlStatusOpen.SelectedValue, statusFilter)
            Dim typeFilter As String = ddlTypeOpen.SelectedValue

            Dim sql As String =
                "SELECT cr.ID, cr.PersonCode, cr.FullName, cr.ChildFullName, cr.ChildNationalCode, " &
                "       cr.ChildType, cr.EducationLevel, cr.EducationLevelID, cr.Average, " &
                "       cr.ExtraDocumentFileName, cr.StatusCode, cr.CreateDate, " &
                "       ISNULL(cc.Amount, 0) AS Amount, " &
                "       CASE cr.StatusCode " &
                "           WHEN 1 THEN N'⏳ در انتظار بررسی' " &
                "           WHEN 2 THEN N'📝 نیاز به اصلاح' " &
                "           ELSE N'نامشخص' END AS StatusText " &
                "FROM exmChildRequests cr " &
                "LEFT JOIN exmChildConditions cc ON cr.EducationLevelID = cc.ID " &
                "WHERE cr.AcademicYear = @Year AND cr.StatusCode IN (1, 2)"

            Dim params As New List(Of SqlParameter) From {
                New SqlParameter("@Year", ACADEMIC_YEAR)
            }

            If statusFilter > 0 Then
                sql &= " AND cr.StatusCode = @StatusCode"
                params.Add(New SqlParameter("@StatusCode", statusFilter))
            End If

            If Not String.IsNullOrEmpty(typeFilter) Then
                sql &= " AND cr.ChildType = @ChildType"
                params.Add(New SqlParameter("@ChildType", typeFilter))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (cr.FullName LIKE @Search OR cr.PersonCode LIKE @Search " &
                       "OR cr.ChildFullName LIKE @Search OR cr.ChildNationalCode LIKE @Search)"
                params.Add(New SqlParameter("@Search", "%" & search & "%"))
            End If

            sql &= " ORDER BY cr.CreateDate DESC"

            Dim dt As DataTable = GetDataTable(sql, params.ToArray())

            dt.Columns.Add("CreateDatePersian", GetType(String))
            dt.Columns.Add("AverageDisplay", GetType(String))
            dt.Columns.Add("ExtraDocDisplay", GetType(String))
            dt.Columns.Add("AmountDisplay", GetType(String))

            For Each row As DataRow In dt.Rows
                If row("CreateDate") IsNot DBNull.Value Then
                    row("CreateDatePersian") = GetPersianDateTime(Convert.ToDateTime(row("CreateDate")))
                Else
                    row("CreateDatePersian") = "-"
                End If
                row("AverageDisplay") = If(row("Average") Is DBNull.Value, "-", String.Format("{0:0.00}", row("Average")))
                Dim eFile As String = ""
                If row("ExtraDocumentFileName") IsNot DBNull.Value Then eFile = row("ExtraDocumentFileName").ToString().Trim()
                row("ExtraDocDisplay") = If(String.IsNullOrEmpty(eFile), "—", "📎 دارد")

                Dim amount As Decimal = 0
                If row("Amount") IsNot DBNull.Value Then
                    Decimal.TryParse(row("Amount").ToString(), amount)
                End If
                row("AmountDisplay") = If(amount > 0, amount.ToString("#,##0"), "—")
            Next

            If dt.Rows.Count > 0 Then
                rptOpen.DataSource = dt
                rptOpen.DataBind()
                pnlTableOpen.Visible = True
                pnlEmptyOpen.Visible = False
            Else
                pnlTableOpen.Visible = False
                pnlEmptyOpen.Visible = True
            End If
        Catch ex As Exception
            ShowToast("error", "خطا در لیست باز: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ Load Closed Requests — با مبلغ
    ' ============================================================
    Private Sub LoadClosedRequests()
        Try
            Dim search As String = txtSearchClosed.Text.Trim()
            Dim statusFilter As Integer = 0
            Integer.TryParse(ddlStatusClosed.SelectedValue, statusFilter)
            Dim typeFilter As String = ddlTypeClosed.SelectedValue

            Dim sql As String =
                "SELECT cr.ID, cr.PersonCode, cr.FullName, cr.ChildFullName, cr.ChildNationalCode, " &
                "       cr.ChildType, cr.EducationLevel, cr.EducationLevelID, cr.StatusCode, cr.CreateDate, " &
                "       ISNULL(cc.Amount, 0) AS Amount, " &
                "       CASE cr.StatusCode " &
                "           WHEN 3 THEN N'✅ تایید شده' " &
                "           WHEN 4 THEN N'❌ رد شده' " &
                "           WHEN 5 THEN N'🚫 انصراف' " &
                "           ELSE N'نامشخص' END AS StatusText " &
                "FROM exmChildRequests cr " &
                "LEFT JOIN exmChildConditions cc ON cr.EducationLevelID = cc.ID " &
                "WHERE cr.AcademicYear = @Year AND cr.StatusCode IN (3, 4, 5)"

            Dim params As New List(Of SqlParameter) From {
                New SqlParameter("@Year", ACADEMIC_YEAR)
            }

            If statusFilter > 0 Then
                sql &= " AND cr.StatusCode = @StatusCode"
                params.Add(New SqlParameter("@StatusCode", statusFilter))
            End If

            If Not String.IsNullOrEmpty(typeFilter) Then
                sql &= " AND cr.ChildType = @ChildType"
                params.Add(New SqlParameter("@ChildType", typeFilter))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (cr.FullName LIKE @Search OR cr.PersonCode LIKE @Search " &
                       "OR cr.ChildFullName LIKE @Search OR cr.ChildNationalCode LIKE @Search)"
                params.Add(New SqlParameter("@Search", "%" & search & "%"))
            End If

            sql &= " ORDER BY cr.ReviewDate DESC, cr.CreateDate DESC"

            Dim dt As DataTable = GetDataTable(sql, params.ToArray())

            dt.Columns.Add("CreateDatePersian", GetType(String))
            dt.Columns.Add("AmountDisplay", GetType(String))

            For Each row As DataRow In dt.Rows
                If row("CreateDate") IsNot DBNull.Value Then
                    row("CreateDatePersian") = GetPersianDateTime(Convert.ToDateTime(row("CreateDate")))
                Else
                    row("CreateDatePersian") = "-"
                End If

                Dim amount As Decimal = 0
                If row("Amount") IsNot DBNull.Value Then
                    Decimal.TryParse(row("Amount").ToString(), amount)
                End If
                row("AmountDisplay") = If(amount > 0, amount.ToString("#,##0"), "—")
            Next

            If dt.Rows.Count > 0 Then
                rptClosed.DataSource = dt
                rptClosed.DataBind()
                pnlTableClosed.Visible = True
                pnlEmptyClosed.Visible = False
            Else
                pnlTableClosed.Visible = False
                pnlEmptyClosed.Visible = True
            End If
        Catch ex As Exception
            ShowToast("error", "خطا در لیست بسته: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnFilterOpen_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadOpenRequests()
    End Sub

    Protected Sub btnFilterClosed_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadClosedRequests()
    End Sub

    Protected Sub rptOpen_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        HandleViewRequest(e)
    End Sub

    Protected Sub rptClosed_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        HandleViewRequest(e)
    End Sub

    Private Sub HandleViewRequest(ByVal e As RepeaterCommandEventArgs)
        Try
            If e.CommandName <> "ViewRequest" Then Return

            Dim requestID As Integer = 0
            If Not Integer.TryParse(e.CommandArgument.ToString(), requestID) OrElse requestID = 0 Then
                ShowToast("error", "شناسه نامعتبر")
                Return
            End If

            hfSelectedRequestID.Value = requestID.ToString()
            Session("ReviewingRequestID") = requestID

            LoadRequestDetail(requestID)
            hfShowModal.Value = "1"

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ Load Request Detail — با تمام فیلدهای جدید + واحد کارمند
    ' ============================================================
    Private Sub LoadRequestDetail(ByVal requestID As Integer)
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT cr.*, " &
                "       ISNULL(cc.Amount, 0) AS LevelAmount, " &
                "       cc.EducationLevel AS LevelName, " &
                "       ISNULL(o.omur, N'-') AS EmployeeUnitName " &
                "FROM exmChildRequests cr " &
                "LEFT JOIN exmChildConditions cc ON cr.EducationLevelID = cc.ID " &
                "LEFT JOIN personel p ON p.codePersoneli = cr.PersonCode " &
                "LEFT JOIN omurTbl o ON o.omurID = p.omurID " &
                "WHERE cr.ID = @ID",
                New SqlParameter("@ID", requestID))

            If dt.Rows.Count = 0 Then
                pnlDetail.Visible = False
                pnlDetailEmpty.Visible = True
                pnlReviewButtons.Visible = False
                Return
            End If

            Dim row As DataRow = dt.Rows(0)

            ' اطلاعات کارمند
            lblD_FullName.Text = GetSafeString(row, "FullName")
            lblD_PersonCode.Text = GetSafeString(row, "PersonCode")

            ' ✅ واحد کارمند (نه واحد کارشناس)
            lblD_UnitName.Text = GetSafeString(row, "EmployeeUnitName")

            lblD_Mobile.Text = GetSafeString(row, "Mobile")

            ' اطلاعات فرزند
            lblD_ChildName.Text = GetSafeString(row, "ChildFullName")
            lblD_ChildNationalCode.Text = GetSafeString(row, "ChildNationalCode")
            lblD_ChildBirthDate.Text = GetSafeString(row, "ChildBirthDate")
            lblD_ChildGender.Text = GetSafeString(row, "ChildGender")

            ' اطلاعات تحصیلی عمومی
            lblD_ChildType.Text = GetSafeString(row, "ChildType")
            lblD_EducationLevel.Text = GetSafeString(row, "EducationLevel")
            lblD_Average.Text = If(row("Average") Is DBNull.Value, "-", String.Format("{0:0.00}", row("Average")))
            lblD_AcademicYear.Text = GetSafeString(row, "AcademicYear")

            Dim isStudent As Boolean = (GetSafeString(row, "ChildType") = "دانشجو")

            ' ✅ اطلاعات مدرسه
            Dim schoolName As String = GetSafeString(row, "SchoolName")
            Dim schoolType As String = GetSafeString(row, "SchoolType")
            Dim schoolPhone As String = GetSafeString(row, "SchoolPhone")

            If Not isStudent AndAlso (schoolName <> "-" OrElse schoolType <> "-" OrElse schoolPhone <> "-") Then
                pnlDetailSchool.Visible = True
                lblD_SchoolName.Text = schoolName
                lblD_SchoolType.Text = schoolType
                lblD_SchoolPhone.Text = schoolPhone
            Else
                pnlDetailSchool.Visible = False
            End If

            ' ✅ اطلاعات سمپاد
            Dim isSampad As Boolean = False
            If row.Table.Columns.Contains("IsSampad") AndAlso Not IsDBNull(row("IsSampad")) Then
                isSampad = Convert.ToBoolean(row("IsSampad"))
            End If

            If isSampad Then
                pnlDetailSampad.Visible = True
                lblD_SampadSchoolName.Text = GetSafeString(row, "SampadSchoolName")
                lblD_SampadStudentNumber.Text = GetSafeString(row, "SampadStudentNumber")
            Else
                pnlDetailSampad.Visible = False
            End If

            ' ✅ اطلاعات دانشگاه
            If isStudent Then
                pnlDetailUniversity.Visible = True
                lblD_UniversityName.Text = GetSafeString(row, "UniversityName")
                lblD_UniversityType.Text = GetSafeString(row, "UniversityType")
                lblD_UniversityPhone.Text = GetSafeString(row, "UniversityPhone")
                lblD_StudentNumber.Text = GetSafeString(row, "StudentNumber")

                Dim admissionType As String = GetSafeString(row, "AdmissionType")
                If admissionType = "WithExam" Then
                    lblD_AdmissionType.Text = "🎓 با آزمون سراسری"
                ElseIf admissionType = "WithoutExam" Then
                    lblD_AdmissionType.Text = "✅ بدون آزمون"
                Else
                    lblD_AdmissionType.Text = "-"
                End If

                lblD_RankInExam.Text = GetSafeString(row, "RankInExam")
                lblD_WithoutExamReason.Text = GetSafeString(row, "WithoutExamReason")
            Else
                pnlDetailUniversity.Visible = False
            End If

            ' ============================================================
            ' ✅ نمایش مبلغ (فقط برای روابط عمومی) — نسخه بهبود یافته
            ' ============================================================
            If IsPublicRelationsUser() Then
                Dim levelAmount As Decimal = 0
                If row("LevelAmount") IsNot DBNull.Value Then
                    Decimal.TryParse(row("LevelAmount").ToString(), levelAmount)
                End If

                If levelAmount > 0 Then
                    pnlDetailAmount.Visible = True
                    pnlDetailAmountEmpty.Visible = False

                    ' نام مقطع
                    lblAmountLevel.Text = GetSafeString(row, "EducationLevel")

                    ' مبلغ عددی
                    lblAmountValue.Text = levelAmount.ToString("#,##0")

                    ' مبلغ به حروف
                    lblAmountWords.Text = ConvertNumberToPersianWords(levelAmount)
                Else
                    pnlDetailAmount.Visible = False
                    pnlDetailAmountEmpty.Visible = True
                End If
            Else
                pnlDetailAmount.Visible = False
                pnlDetailAmountEmpty.Visible = False
            End If

            ' ✅ مدارک هویتی
            Dim birthCertFile As String = GetSafeString(row, "BirthCertificateFileName")
            Dim nationalCardFile As String = GetSafeString(row, "NationalCardFileName")

            If birthCertFile = "-" Then birthCertFile = ""
            If nationalCardFile = "-" Then nationalCardFile = ""

            If Not String.IsNullOrEmpty(birthCertFile) Then
                pnlDocBirthCert.Visible = True
                pnlDocBirthCertMissing.Visible = False
                lblDocBirthCertName.Text = birthCertFile
                lnkViewBirthCert.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=birthcert&mode=view"
                lnkDownloadBirthCert.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=birthcert&mode=download"
            Else
                pnlDocBirthCert.Visible = False
                pnlDocBirthCertMissing.Visible = True
            End If

            If Not String.IsNullOrEmpty(nationalCardFile) Then
                pnlDocNationalCard.Visible = True
                pnlDocNationalCardMissing.Visible = False
                lblDocNationalCardName.Text = nationalCardFile
                lnkViewNationalCard.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=nationalcard&mode=view"
                lnkDownloadNationalCard.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=nationalcard&mode=download"
            Else
                pnlDocNationalCard.Visible = False
                pnlDocNationalCardMissing.Visible = True
            End If

            ' ✅ مدارک تحصیلی
            Dim transcriptFile As String = GetSafeString(row, "TranscriptFileName")
            Dim extraDocFile As String = GetSafeString(row, "ExtraDocumentFileName")
            Dim extraDocType As String = GetSafeString(row, "ExtraDocumentType")
            Dim studentCardFile As String = GetSafeString(row, "StudentCardFileName")

            If transcriptFile = "-" Then transcriptFile = ""
            If extraDocFile = "-" Then extraDocFile = ""
            If extraDocType = "-" Then extraDocType = ""
            If studentCardFile = "-" Then studentCardFile = ""

            If Not String.IsNullOrEmpty(transcriptFile) Then
                pnlDocTranscript.Visible = True
                lblDocTranscriptName.Text = transcriptFile
                lnkViewTranscript.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=transcript&mode=view"
                lnkDownloadTranscript.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=transcript&mode=download"
            Else
                pnlDocTranscript.Visible = False
            End If

            If isStudent Then
                If Not String.IsNullOrEmpty(studentCardFile) Then
                    pnlDocStudentCard.Visible = True
                    pnlDocStudentCardMissing.Visible = False
                    lblDocStudentCardName.Text = studentCardFile
                    lnkViewStudentCard.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=studentcard&mode=view"
                    lnkDownloadStudentCard.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=studentcard&mode=download"
                Else
                    pnlDocStudentCard.Visible = False
                    pnlDocStudentCardMissing.Visible = True
                End If
            Else
                pnlDocStudentCard.Visible = False
                pnlDocStudentCardMissing.Visible = False
            End If

            If Not String.IsNullOrEmpty(extraDocFile) Then
                pnlDocExtra.Visible = True
                lblDocExtraTitle.Text = If(String.IsNullOrEmpty(extraDocType), "مدرک اضافی", extraDocType)
                lblDocExtraName.Text = extraDocFile
                lnkViewExtra.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=extra&mode=view"
                lnkDownloadExtra.HRef = ResolveUrl(DOWNLOAD_HANDLER) & "?id=" & requestID & "&type=extra&mode=download"
            Else
                pnlDocExtra.Visible = False
            End If

            ' ✅ بررسی عدم تطابق شرایط
            CheckConditionMismatch(requestID, row)

            ' آخرین بررسی
            Dim statusCode As Integer = Convert.ToInt32(row("StatusCode"))
            Dim reviewerCode As String = GetSafeString(row, "ReviewerCode")
            Dim reviewerName As String = GetSafeString(row, "ReviewerName")
            Dim reviewDate As Object = row("ReviewDate")
            Dim reviewerComment As String = GetSafeString(row, "ReviewerComment")

            If statusCode <> 1 Then
                pnlPreviousReview.Visible = True
                lblPrevStatus.Text = GetStatusText(statusCode)
                If reviewerName <> "-" AndAlso Not String.IsNullOrEmpty(reviewerName) Then
                    lblPrevReviewer.Text = reviewerName & " (" & reviewerCode & ")"
                ElseIf reviewerCode <> "-" Then
                    lblPrevReviewer.Text = reviewerCode
                Else
                    lblPrevReviewer.Text = "—"
                End If
                lblPrevReviewDate.Text = If(reviewDate Is DBNull.Value, "-", GetPersianDateTime(Convert.ToDateTime(reviewDate)))
                lblPrevReason.Text = GetSafeString(row, "StatusDescription")
                lblPrevComment.Text = If(reviewerComment = "-", "—", reviewerComment)
            Else
                pnlPreviousReview.Visible = False
            End If

            LoadReviewHistory(requestID)

            Dim canReview As Boolean = (statusCode = 1 OrElse statusCode = 2)
            pnlReviewForm.Visible = canReview
            pnlReviewButtons.Visible = canReview

            If canReview AndAlso Not IsPostBack Then
                txtReviewReason.Text = ""
                txtReviewerComment.Text = ""
                hfReviewReason.Value = ""
                hfReviewerComment.Value = ""
            End If

            pnlDetail.Visible = True
            pnlDetailEmpty.Visible = False

        Catch ex As Exception
            ShowToast("error", "خطا: " & ex.Message)
            pnlDetail.Visible = False
            pnlDetailEmpty.Visible = True
            pnlReviewButtons.Visible = False
        End Try
    End Sub

    ' ============================================================
    ' ✅ بررسی عدم تطابق شرایط
    ' ============================================================
    Private Sub CheckConditionMismatch(ByVal requestID As Integer, ByVal row As DataRow)
        Try
            Dim educationLevelID As Integer = 0
            If row.Table.Columns.Contains("EducationLevelID") AndAlso Not IsDBNull(row("EducationLevelID")) Then
                Integer.TryParse(row("EducationLevelID").ToString(), educationLevelID)
            End If

            If educationLevelID = 0 Then
                pnlConditionAlert.Visible = False
                Return
            End If

            Dim dtCondition As DataTable = GetDataTable(
                "SELECT Condition, MinAverage FROM exmChildConditions WHERE ID = @ID",
                New SqlParameter("@ID", educationLevelID))

            If dtCondition.Rows.Count = 0 Then
                pnlConditionAlert.Visible = False
                Return
            End If

            Dim condition As String = If(IsDBNull(dtCondition.Rows(0)("Condition")), "", dtCondition.Rows(0)("Condition").ToString().Trim())
            Dim minAverage As Decimal = 0
            If Not IsDBNull(dtCondition.Rows(0)("MinAverage")) Then
                Decimal.TryParse(dtCondition.Rows(0)("MinAverage").ToString(), minAverage)
            End If

            Dim average As Decimal = 0
            If Not IsDBNull(row("Average")) Then
                Decimal.TryParse(row("Average").ToString(), average)
            End If

            Dim mismatchMessages As New List(Of String)()

            If minAverage > 0 AndAlso average > 0 AndAlso average < (minAverage - 0.5D) Then
                mismatchMessages.Add("❌ معدل (" & average.ToString("0.00") & ") کمتر از حد نصاب (" & minAverage.ToString("0.00") & ") است.")
            End If

            If Not String.IsNullOrEmpty(condition) Then
                Dim conditionLower As String = condition.ToLower()
                If conditionLower.Contains("بدون آزمون") Then
                    Dim admissionType As String = GetSafeString(row, "AdmissionType")
                    If admissionType <> "WithoutExam" Then
                        mismatchMessages.Add("❌ این مقطع نیاز به پذیرش «بدون آزمون» دارد، اما نوع ورود ثبت‌شده «با آزمون» است.")
                    End If
                End If
                If conditionLower.Contains("رتبه") Then
                    Dim rank As String = GetSafeString(row, "RankInExam")
                    If rank = "-" OrElse String.IsNullOrEmpty(rank) Then
                        mismatchMessages.Add("❌ این مقطع نیاز به «رتبه آزمون» دارد، اما رتبه ثبت نشده است.")
                    End If
                End If
                If conditionLower.Contains("استعداد") OrElse conditionLower.Contains("تیزهوشان") OrElse conditionLower.Contains("درخشان") Then
                    Dim isSampad As Boolean = False
                    If row.Table.Columns.Contains("IsSampad") AndAlso Not IsDBNull(row("IsSampad")) Then
                        isSampad = Convert.ToBoolean(row("IsSampad"))
                    End If
                    If Not isSampad Then
                        mismatchMessages.Add("❌ این مقطع مخصوص مدارس استعدادهای درخشان است، اما گزینه «قبولی در سمپاد» انتخاب نشده است.")
                    End If
                End If
            End If

            If mismatchMessages.Count > 0 Then
                pnlConditionAlert.Visible = True
                lblConditionAlertText.Text = String.Join("<br/>", mismatchMessages.ToArray())
            Else
                pnlConditionAlert.Visible = False
            End If

        Catch ex As Exception
            pnlConditionAlert.Visible = False
        End Try
    End Sub

    Private Sub LoadReviewHistory(ByVal requestID As Integer)
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT StatusCode, StatusDescription, ReviewerName, ReviewerCode, ReviewerComment, ReviewDate, CreateDate " &
                "FROM exmChildRequestsHistory WHERE RequestID = @RequestID ORDER BY CreateDate DESC",
                New SqlParameter("@RequestID", requestID))

            If dt.Rows.Count > 0 Then
                pnlHistoryReview.Visible = True
                rptReviewHistory.DataSource = dt
                rptReviewHistory.DataBind()
            Else
                pnlHistoryReview.Visible = False
            End If
        Catch ex As Exception
            pnlHistoryReview.Visible = False
        End Try
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As EventArgs)
        ProcessReview(3, "✅ تایید شده", False)
    End Sub

    Protected Sub btnReject_Click(ByVal sender As Object, ByVal e As EventArgs)
        ProcessReview(4, "❌ رد شده", True)
    End Sub

    Protected Sub btnCorrection_Click(ByVal sender As Object, ByVal e As EventArgs)
        ProcessReview(2, "📝 نیاز به اصلاح", True)
    End Sub

    Private Function GetReviewReasonValue() As String
        Dim val As String = hfReviewReason.Value
        If Not String.IsNullOrEmpty(val) Then Return val.Trim()

        If txtReviewReason IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtReviewReason.Text) Then
            Return txtReviewReason.Text.Trim()
        End If

        For Each key As String In Request.Form.AllKeys
            If key IsNot Nothing AndAlso key.Contains("txtReviewReason") Then
                Dim posted As String = Request.Form(key)
                If posted IsNot Nothing AndAlso posted.Trim() <> "" Then
                    Return posted.Trim()
                End If
            End If
        Next

        Return ""
    End Function

    Private Function GetReviewCommentValue() As String
        Dim val As String = hfReviewerComment.Value
        If Not String.IsNullOrEmpty(val) Then Return val.Trim()

        If txtReviewerComment IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtReviewerComment.Text) Then
            Return txtReviewerComment.Text.Trim()
        End If

        For Each key As String In Request.Form.AllKeys
            If key IsNot Nothing AndAlso key.Contains("txtReviewerComment") Then
                Dim posted As String = Request.Form(key)
                If posted IsNot Nothing AndAlso posted.Trim() <> "" Then
                    Return posted.Trim()
                End If
            End If
        Next

        Return ""
    End Function

    Private Sub ProcessReview(ByVal newStatusCode As Integer, ByVal statusText As String, ByVal requireReason As Boolean)
        Try
            Dim requestID As Integer = 0
            If Session("ReviewingRequestID") IsNot Nothing Then
                Integer.TryParse(Session("ReviewingRequestID").ToString(), requestID)
            End If
            If requestID = 0 Then Integer.TryParse(hfSelectedRequestID.Value, requestID)

            If requestID = 0 Then
                ShowAlert("error", "خطا", "درخواستی انتخاب نشده.")
                Return
            End If

            Dim reason As String = GetReviewReasonValue()
            Dim comment As String = GetReviewCommentValue()

            If requireReason AndAlso String.IsNullOrEmpty(reason) Then
                ShowAlert("warning", "دلیل الزامی", "لطفاً دلیل را وارد کنید.")
                Return
            End If

            If String.IsNullOrEmpty(reason) Then reason = "تایید شد"

            Dim sql As String =
                "UPDATE exmChildRequests SET " &
                "StatusCode = @StatusCode, StatusDescription = @StatusDescription, " &
                "ReviewerCode = @ReviewerCode, ReviewerName = @ReviewerName, " &
                "ReviewDate = GETDATE(), ReviewerComment = @ReviewerComment " &
                "WHERE ID = @ID"

            Dim affected As Integer = ExecuteNonQuery(sql,
                New SqlParameter("@StatusCode", newStatusCode),
                New SqlParameter("@StatusDescription", reason),
                New SqlParameter("@ReviewerCode", _personCode),
                New SqlParameter("@ReviewerName", _fullName),
                New SqlParameter("@ReviewerComment", If(String.IsNullOrEmpty(comment), DBNull.Value, CObj(comment))),
                New SqlParameter("@ID", requestID))

            If affected > 0 Then
                SaveReviewHistory(requestID, newStatusCode, reason, comment)

                hfShowModal.Value = "0"
                hfReviewReason.Value = ""
                hfReviewerComment.Value = ""
                Session.Remove("ReviewingRequestID")
                pnlDetail.Visible = False
                pnlDetailEmpty.Visible = True
                pnlReviewButtons.Visible = False

                Dim script As String = "closeDetailModal(); showToast('success', 'وضعیت با موفقیت به «" & SanitizeJsString(statusText) & "» تغییر یافت');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "done_" & Guid.NewGuid().ToString("N"), script, True)

                LoadStats()
                LoadOpenRequests()
                LoadClosedRequests()
            Else
                ShowAlert("error", "خطا", "به‌روزرسانی انجام نشد.")
            End If

        Catch ex As Exception
            ShowAlert("error", "خطا", ex.Message)
        End Try
    End Sub

    Private Sub SaveReviewHistory(ByVal requestID As Integer, ByVal statusCode As Integer,
                                   ByVal reason As String, ByVal comment As String)
        Try
            ExecuteNonQuery(
                "INSERT INTO exmChildRequestsHistory " &
                "(RequestID, StatusCode, StatusDescription, ReviewerCode, ReviewerName, ReviewerComment, ReviewDate) " &
                "VALUES (@RequestID, @StatusCode, @StatusDescription, @ReviewerCode, @ReviewerName, @ReviewerComment, GETDATE())",
                New SqlParameter("@RequestID", requestID),
                New SqlParameter("@StatusCode", statusCode),
                New SqlParameter("@StatusDescription", If(String.IsNullOrEmpty(reason), DBNull.Value, CObj(reason))),
                New SqlParameter("@ReviewerCode", _personCode),
                New SqlParameter("@ReviewerName", _fullName),
                New SqlParameter("@ReviewerComment", If(String.IsNullOrEmpty(comment), DBNull.Value, CObj(comment))))
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("SaveReviewHistory Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ تبدیل عدد به حروف فارسی
    ' ============================================================
    Private Function ConvertNumberToPersianWords(ByVal number As Decimal) As String
        Try
            Dim n As Long = CLng(Math.Floor(number))
            If n = 0 Then Return "صفر ریال"

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

            Return result.Trim() & " ریال"
        Catch
            Return ""
        End Try
    End Function

    Private Function GetSafeString(ByVal row As DataRow, ByVal colName As String) As String
        Try
            If Not row.Table.Columns.Contains(colName) Then Return "-"
            If row(colName) Is DBNull.Value Then Return "-"
            Dim val As String = row(colName).ToString().Trim()
            If String.IsNullOrEmpty(val) Then Return "-"
            Return val
        Catch
            Return "-"
        End Try
    End Function

    Protected Function GetStatusText(ByVal statusCode As Integer) As String
        Select Case statusCode
            Case 1 : Return "⏳ در انتظار بررسی"
            Case 2 : Return "📝 نیاز به اصلاح"
            Case 3 : Return "✅ تایید شده"
            Case 4 : Return "❌ رد شده"
            Case 5 : Return "🚫 انصراف"
            Case Else : Return "نامشخص"
        End Select
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

    Protected Function GetHistoryDate(ByVal reviewDate As Object, ByVal createDate As Object) As String
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

    Protected Function GetHistoryReviewer(ByVal name As Object, ByVal code As Object) As String
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

    Protected Function GetHistoryReason(ByVal reason As Object) As String
        Try
            If reason Is Nothing OrElse IsDBNull(reason) Then Return "—"
            Dim r As String = reason.ToString().Trim()
            If String.IsNullOrEmpty(r) Then Return "—"
            Return Server.HtmlEncode(r)
        Catch
            Return "—"
        End Try
    End Function

    Protected Function GetHistoryComment(ByVal comment As Object) As String
        Try
            If comment Is Nothing OrElse IsDBNull(comment) Then Return "—"
            Dim c As String = comment.ToString().Trim()
            If String.IsNullOrEmpty(c) Then Return "—"
            Return Server.HtmlEncode(c)
        Catch
            Return "—"
        End Try
    End Function

    Private Sub ShowToast(ByVal type As String, ByVal message As String)
        Try
            Dim safeMsg As String = SanitizeJsString(message)
            Dim script As String = "showToast('" & type & "', '" & safeMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast_" & Guid.NewGuid().ToString("N"), script, True)
        Catch
        End Try
    End Sub

    Private Sub ShowAlert(ByVal type As String, ByVal title As String, ByVal message As String)
        Try
            Dim safeTitle As String = SanitizeJsString(title)
            Dim safeMsg As String = SanitizeJsString(message)
            Dim script As String = "showAlert('" & type & "', '" & safeTitle & "', '" & safeMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert_" & Guid.NewGuid().ToString("N"), script, True)
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

End Class