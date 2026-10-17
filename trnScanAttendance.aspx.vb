Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports System.Web.UI.WebControls

Partial Class trnScanAttendance
    Inherits System.Web.UI.Page

    ' ============================================
    ' تابع برای دریافت رشته اتصال با Timeout بالا
    ' ============================================
    Private Function GetConnectionStringWithTimeout() As String
        ' دریافت رشته اتصال اصلی از کلاس ClsConnection
        Dim connString As String = ClsConnection.GetConn

        ' اگر رشته اتصال شامل Timeout نبود، آن را اضافه کن
        If Not connString.ToLower().Contains("connection timeout") Then
            ' اگر رشته با ; ختم نشده بود، ; اضافه کن
            If Not connString.EndsWith(";") Then
                connString &= ";"
            End If
            connString &= "Connection Timeout=120;"
        End If

        Return connString
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblCurrentTime.InnerText = "🕐 زمان فعلی: " & DateTime.Now.ToString("HH:mm:ss")

            Dim courseId As String = Request.QueryString("courseId")

            If String.IsNullOrEmpty(courseId) Then
                ShowStatus("❌ خطا: اطلاعات دوره یافت نشد", "scan-error")
                divBack.Visible = True
                btnBackToHome.Visible = True
                Exit Sub
            End If

            ' ذخیره در Session
            Session("doreID") = courseId
            hdnCourseId.Value = courseId

            If Session("codePersoneli") Is Nothing Then
                ShowStatus("⚠️ لطفاً ابتدا وارد سیستم شوید", "scan-error")
                divBack.Visible = True
                btnBackToHome.Visible = True
                Exit Sub
            End If

            LoadPersonInfoFromSession()
            LoadCourseInfo(courseId)
            LoadAttendanceStatus()
            CheckEvaluationStatus()

            ' بررسی وجود فایل‌های ضمیمه
            CheckAttachmentsExist()
        End If
    End Sub

#Region "بارگذاری اطلاعات"

    Private Sub LoadPersonInfoFromSession()
        Try
            Dim personCode As String = Session("codePersoneli").ToString()
            Dim fullName As String = Session("FullName").ToString()

            hdnPersonCode.Value = personCode
            lblCode.InnerText = personCode
            lblName.InnerText = fullName
            lblPersonCodeDisplay.InnerText = personCode

            divPersonInfo.Visible = True

        Catch ex As Exception
            ShowStatus("❌ خطا در بارگذاری اطلاعات: " & ex.Message, "scan-error")
            divBack.Visible = True
            btnBackToHome.Visible = True
        End Try
    End Sub

    Private Sub LoadCourseInfo(ByVal courseId As String)
        Try
            Dim sqlstr As String = "SELECT onvan, tarixShuru, tarixEtmam, makan, modares FROM trnBarnameRiziTbl WHERE idd = " & courseId
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                lblCourseName.InnerText = row("onvan").ToString()
                lblCourseDate.InnerText = row("tarixShuru").ToString() & " - " & row("tarixEtmam").ToString()
                lblCourseLocation.InnerText = row("makan").ToString()
                lblCourseTeacher.InnerText = row("modares").ToString()
                divCourseInfo.Visible = True
            Else
                ShowStatus("⚠️ دوره آموزشی یافت نشد", "scan-error")
                divBack.Visible = True
                btnBackToHome.Visible = True
            End If
        Catch ex As Exception
            ShowStatus("❌ خطا در بارگذاری اطلاعات دوره: " & ex.Message, "scan-error")
        End Try
    End Sub

#End Region

#Region "بررسی و بارگذاری فایل‌های ضمیمه"

    ' ============================================
    ' بررسی وجود فایل‌های ضمیمه (با Timeout بالا)
    ' ============================================
    Private Sub CheckAttachmentsExist()
        Try
            Dim doreID As Object = Session("doreID")

            If doreID Is Nothing Then
                divAttachmentButton.Visible = False
                hdnHasAttachments.Value = "0"
                Exit Sub
            End If

            Dim doreIDInt As Integer = Convert.ToInt32(doreID)



            Dim dt3 As New DataTable
            Dim sqlstr As String
         
            sqlstr = "SELECT      TOP (1) 1 " & _
                         "FROM           documentsTbl   WHERE (documentsTbl.CodeClosed = 0) AND (documentsTbl.fileID  = " & doreIDInt & ")"
            dt3 = ClsDataTable.GetDataTable(sqlstr)
            If dt3.Rows.Count > 0 Then


                divAttachmentButton.Visible = True
                btnShowAttachments.Text = "📎 مشاهده فایل‌های ضمیمه دوره"
                hdnHasAttachments.Value = "1"
                ShowStatus("✅ فایل‌های ضمیمه برای این دوره وجود دارد.", "scan-success")

            Else

                divAttachmentButton.Visible = False
                hdnHasAttachments.Value = "0"
                ShowStatus("ℹ️ هیچ فایل ضمیمه‌ای برای این دوره یافت نشد.", "scan-warning")

            End If

            '    ' استفاده از رشته اتصال با Timeout بالا
            '    Using cnn As New SqlConnection(GetConnectionStringWithTimeout())
            '        cnn.Open()

            '        ' کوئری بسیار ساده و سریع با TOP 1
            '        Dim sqlstr As String = "SELECT TOP 1 1 FROM documentsTbl WITH (NOLOCK) WHERE fileID = @FileID AND content_file IS NOT NULL"

            '        Using cmd As New SqlCommand(sqlstr, cnn)
            '            cmd.CommandTimeout = 120
            '            cmd.Parameters.AddWithValue("@FileID", doreIDInt)
            '            Dim result As Object = cmd.ExecuteScalar()
            '            Dim hasFile As Boolean = (result IsNot Nothing AndAlso result.ToString() = "1")

            '            If hasFile Then
            '                divAttachmentButton.Visible = True
            '                btnShowAttachments.Text = "📎 مشاهده فایل‌های ضمیمه دوره"
            '                hdnHasAttachments.Value = "1"
            '                ShowStatus("✅ فایل‌های ضمیمه برای این دوره وجود دارد.", "scan-success")
            '            Else
            '                divAttachmentButton.Visible = False
            '                hdnHasAttachments.Value = "0"
            '                ShowStatus("ℹ️ هیچ فایل ضمیمه‌ای برای این دوره یافت نشد.", "scan-warning")
            '            End If
            '        End Using
            '    End Using
        Catch ex As Exception
            divAttachmentButton.Visible = False
            hdnHasAttachments.Value = "0"
            ShowStatus("❌ خطا در بررسی فایل‌ها: " & ex.Message, "scan-error")
        End Try
    End Sub

    ' ============================================
    ' بارگذاری فایل‌های ضمیمه (با کلیک روی دکمه)
    ' ============================================
    Protected Sub btnShowAttachments_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowAttachments.Click
        Try
            Dim doreID As Object = Session("doreID")

            If doreID Is Nothing Then
                ShowStatus("❌ خطا: اطلاعات دوره یافت نشد", "scan-error")
                Exit Sub
            End If

            Dim doreIDInt As Integer = Convert.ToInt32(doreID)

            ' نمایش نشانگر بارگذاری
            divLoading.Visible = True
            divAttachments.Visible = True

            Dim dt As New DataTable()

            ' استفاده از ClsDataTable.GetDataTable (مشابه فرم دیگر)
            Dim sqlstr As String = "SELECT id, content_name, size, mdate, sharh " & _
                                  "FROM documentsTbl " & _
                                  "WHERE fileID = " & doreIDInt & " AND content_file IS NOT NULL " & _
                                  "ORDER BY id"

            dt = ClsDataTable.GetDataTable(sqlstr)

            ' مخفی کردن نشانگر بارگذاری
            divLoading.Visible = False

            If dt.Rows.Count > 0 Then
                rptAttachments.DataSource = dt
                rptAttachments.DataBind()
                divAttachments.Visible = True
                divNoAttachment.Visible = False
                btnDownloadAllAttachments.Visible = (dt.Rows.Count > 1)

                ' مخفی کردن دکمه نمایش فایل‌ها
                divAttachmentButton.Visible = False

                ShowStatus("✅ " & dt.Rows.Count & " فایل ضمیمه برای این دوره یافت شد.", "scan-success")
            Else
                divAttachments.Visible = True
                divNoAttachment.Visible = True
                btnDownloadAllAttachments.Visible = False
                ShowStatus("ℹ️ هیچ فایل ضمیمه‌ای برای این دوره یافت نشد.", "scan-warning")
            End If

        Catch ex As Exception
            divLoading.Visible = False
            ShowStatus("❌ خطا در نمایش فایل‌ها: " & ex.Message, "scan-error")
        End Try
    End Sub

#End Region

#Region "دانلود فایل‌های ضمیمه"
    ' ============================================
    ' دانلود فایل با استفاده از Button (به جای LinkButton)
    ' ============================================
    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btn As Button = DirectCast(sender, Button)
            Dim fileId As Integer = Convert.ToInt32(btn.CommandArgument)

            ' استفاده از روشی که در فرم دیگر کار می‌کند
            Dim sqlstr As String = "SELECT id, content_name, content_file FROM documentsTbl WHERE id = " & fileId
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim fileName As String = dt.Rows(0).Item("content_name").ToString()
                Dim fileContent As Byte() = DirectCast(dt.Rows(0).Item("content_file"), Byte())

                ' افزایش تعداد دانلود
                Try
                    Dim updateSql As String = "UPDATE documentsTbl SET bazdid = bazdid + 1 WHERE id = " & fileId
                    upStat(updateSql)
                Catch
                End Try

                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & fileName & """")
                Response.BinaryWrite(fileContent)
                Response.Flush()
                Response.End()
            Else
                ShowStatus("⚠️ فایل مورد نظر پیدا نشد.", "scan-warning")
            End If
        Catch ex As Exception
            ShowStatus("❌ خطا در دانلود فایل: " & ex.Message, "scan-error")
        End Try
    End Sub

    Private Sub upStat(ByVal sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
        Catch ex As Exception
            ' خطا را نادیده بگیر
        Finally
            cnn.Close()
        End Try
    End Sub
    ' ============================================
    ' دانلود یک فایل ضمیمه
    ' ============================================
    

    ' ============================================
    ' دانلود همه فایل‌ها به صورت ZIP
    ' ============================================
    Protected Sub btnDownloadAllAttachments_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim doreID As Object = Session("doreID")

            If doreID Is Nothing Then
                ShowStatus("❌ خطا: اطلاعات دوره یافت نشد", "scan-error")
                Exit Sub
            End If

            Dim doreIDInt As Integer = Convert.ToInt32(doreID)
            Dim files As New List(Of Tuple(Of String, Byte()))()

            Using cnn As New SqlConnection(GetConnectionStringWithTimeout())
                cnn.Open()

                Dim sqlstr As String = "SELECT id, content_name, content_file FROM documentsTbl WITH (NOLOCK) WHERE fileID = @FileID AND content_file IS NOT NULL"
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.CommandTimeout = 180
                    cmd.Parameters.AddWithValue("@FileID", doreIDInt)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim fileName As String = reader("content_name").ToString()
                            Dim fileData As Byte() = DirectCast(reader("content_file"), Byte())
                            If fileData IsNot Nothing AndAlso fileData.Length > 0 Then
                                files.Add(Tuple.Create(fileName, fileData))
                            End If
                        End While
                    End Using
                End Using
            End Using

            If files.Count = 0 Then
                ShowStatus("⚠️ هیچ فایل ضمیمه‌ای برای دانلود وجود ندارد.", "scan-warning")
                Return
            End If

            If files.Count = 1 Then
                ' اگر فقط یک فایل وجود دارد
                Dim file As Tuple(Of String, Byte()) = files(0)
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & file.Item1 & """")
                Response.BinaryWrite(file.Item2)
                Response.Flush()
                Response.End()
            Else
                ' چند فایل به صورت ZIP
                DownloadFilesAsZip(files, "Course_" & doreIDInt & "_Attachments.zip")
            End If
        Catch ex As Exception
            ShowStatus("❌ خطا در دانلود فایل‌ها: " & ex.Message, "scan-error")
        End Try
    End Sub

    Private Sub DownloadFilesAsZip(ByVal files As List(Of Tuple(Of String, Byte())), ByVal zipFileName As String)
        Try
            Using ms As New MemoryStream()
                Using zip As New Ionic.Zip.ZipFile()
                    For Each file In files
                        zip.AddEntry(file.Item1, file.Item2)
                    Next
                    zip.Save(ms)
                End Using

                Dim zipData As Byte() = ms.ToArray()
                Response.Clear()
                Response.ContentType = "application/zip"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & zipFileName & """")
                Response.BinaryWrite(zipData)
                Response.Flush()
                Response.End()
            End Using
        Catch ex As Exception
            ShowStatus("❌ خطا در ایجاد فایل ZIP: " & ex.Message, "scan-error")
        End Try
    End Sub

#End Region

#Region "بررسی وضعیت حضور"

    Private Sub LoadAttendanceStatus()
        Try
            Dim personCode As String = hdnPersonCode.Value
            Dim courseId As String = hdnCourseId.Value

            If String.IsNullOrEmpty(personCode) OrElse String.IsNullOrEmpty(courseId) Then
                Exit Sub
            End If

            Using cnn As New SqlConnection(GetConnectionStringWithTimeout())
                cnn.Open()

                ' بررسی آیا کاربر قبلاً حضور ثبت کرده است
                Dim sqlstr As String = "SELECT TOP 1 ID, CheckType, CheckInTime " &
                                  "FROM trnAttendanceTbl " &
                                  "WHERE PersonCode = @PersonCode " &
                                  "AND CourseID = @CourseID " &
                                  "AND CheckType = 'IN' " &
                                  "AND CAST(CheckDate AS DATE) = CAST(GETDATE() AS DATE) " &
                                  "ORDER BY ID DESC"

                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.CommandTimeout = 60
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    cmd.Parameters.AddWithValue("@CourseID", courseId)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' قبلاً حضور ثبت شده است
                            Dim attendanceId As Integer = Convert.ToInt32(reader("ID"))
                            hdnAttendanceId.Value = attendanceId.ToString()

                            ShowStatus("✅ حضور شما با موفقیت ثبت شده است. (ساعت: " & reader("CheckInTime").ToString() & ")", "scan-success")
                            'btnCheckIn.Visible = False
                            btnCheckIn.Enabled = False


                            btnCheckIn.Visible = True

                            btnCheckIn.Text = "✅ حضور ثبت شده است"

                            ShowStatus("✅ حضور شما با موفقیت ثبت شده است. (ساعت: " & reader("CheckInTime").ToString() & ")", "scan-success")
                            divAction.Visible = True

                            ' نمایش فرم ارزیابی
                            divEvaluation.Visible = True
                        Else
                            ' هنوز حضور ثبت نشده است
                            ShowStatus("📝 برای ثبت حضور در دوره، دکمه زیر را کلیک کنید.", "scan-info")
                            btnCheckIn.Visible = True
                            btnCheckIn.Enabled = True
                            btnCheckIn.Text = "✅ ثبت حضور"
                            divAction.Visible = True
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ShowStatus("❌ خطا در بررسی وضعیت حضور: " & ex.Message, "scan-error")
        End Try
    End Sub

#End Region

#Region "ثبت ورود و خروج"

    Protected Sub btnCheckIn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheckIn.Click
        Try
            btnCheckIn.Enabled = False
            btnCheckIn.Text = "⏳ در حال ثبت..."

            Dim personCode As String = hdnPersonCode.Value
            Dim courseId As String = hdnCourseId.Value
            Dim userId As String = Session("userid").ToString()

            If String.IsNullOrEmpty(personCode) OrElse String.IsNullOrEmpty(courseId) Then
                ShowStatus("❌ خطا: اطلاعات ناقص است", "scan-error")
                btnCheckIn.Enabled = True
                btnCheckIn.Text = "✅ ثبت حضور"
                Exit Sub
            End If

            Using cnn As New SqlConnection(GetConnectionStringWithTimeout())
                cnn.Open()
                Using transaction As SqlTransaction = cnn.BeginTransaction()
                    Try
                        ' بررسی اینکه آیا قبلاً حضور ثبت شده است
                        Dim checkSql As String = "SELECT COUNT(*) FROM trnAttendanceTbl WITH (UPDLOCK) " &
                                           "WHERE PersonCode = @PersonCode " &
                                           "AND CourseID = @CourseID " &
                                           "AND CheckType = 'IN' " &
                                           "AND CAST(CheckDate AS DATE) = CAST(GETDATE() AS DATE)"

                        Using cmdCheck As New SqlCommand(checkSql, cnn, transaction)
                            cmdCheck.CommandTimeout = 60
                            cmdCheck.Parameters.AddWithValue("@PersonCode", personCode)
                            cmdCheck.Parameters.AddWithValue("@CourseID", courseId)

                            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                            If count > 0 Then
                                ShowStatus("✅ شما  حضور خود را ثبت کردید.", "scan-success")
                                btnCheckIn.Visible = False
                                btnCheckIn.Enabled = False
                                btnCheckIn.Text = "✅ ثبت حضور"
                                transaction.Commit()
                                Exit Sub
                            End If
                        End Using

                        Dim now As DateTime = DateTime.Now

                        ' ثبت حضور (فقط ورود)
                        Dim insertSql As String = "INSERT INTO trnAttendanceTbl " &
                                             "(PersonCode, CourseID, CheckType, CheckDate, CheckTime, CheckDateTime, CheckInTime, UserID, CreateDate) " &
                                             "VALUES (@PersonCode, @CourseID, 'IN', @CheckDate, @CheckTime, @CheckDateTime, @CheckInTime, @UserID, @CreateDate)"

                        Using cmdInsert As New SqlCommand(insertSql, cnn, transaction)
                            cmdInsert.CommandTimeout = 60
                            cmdInsert.Parameters.AddWithValue("@PersonCode", personCode)
                            cmdInsert.Parameters.AddWithValue("@CourseID", courseId)
                            cmdInsert.Parameters.AddWithValue("@CheckDate", now.Date)
                            cmdInsert.Parameters.AddWithValue("@CheckTime", now.ToString("HH:mm:ss"))
                            cmdInsert.Parameters.AddWithValue("@CheckDateTime", now)
                            cmdInsert.Parameters.AddWithValue("@CheckInTime", now.ToString("HH:mm:ss"))
                            cmdInsert.Parameters.AddWithValue("@UserID", userId)
                            cmdInsert.Parameters.AddWithValue("@CreateDate", now)

                            Dim rowsAffected As Integer = cmdInsert.ExecuteNonQuery()

                            If rowsAffected > 0 Then
                                Using cmdGetId As New SqlCommand("SELECT SCOPE_IDENTITY()", cnn, transaction)
                                    cmdGetId.CommandTimeout = 60
                                    Dim newId As Object = cmdGetId.ExecuteScalar()
                                    If newId IsNot Nothing Then
                                        hdnAttendanceId.Value = newId.ToString()
                                    End If
                                End Using

                                transaction.Commit()

                                ShowStatus("✅ حضور شما با موفقیت ثبت شد. ساعت: " & now.ToString("HH:mm:ss"), "scan-success")
                                btnCheckIn.Visible = False
                                btnCheckIn.Enabled = False
                                btnCheckIn.Text = "✅ حضور ثبت شد"
                                lblCurrentTime.InnerText = "🕐 زمان ثبت حضور: " & now.ToString("HH:mm:ss")

                                ' نمایش فرم ارزیابی
                                divEvaluation.Visible = True
                            Else
                                transaction.Rollback()
                                ShowStatus("❌ ثبت حضور انجام نشد.", "scan-error")
                                btnCheckIn.Enabled = True
                                btnCheckIn.Text = "✅ ثبت حضور"
                            End If
                        End Using

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using

        Catch ex As Exception
            ShowStatus("❌ خطا: " & ex.Message, "scan-error")
            btnCheckIn.Enabled = True
            btnCheckIn.Text = "✅ ثبت حضور"
        End Try
    End Sub



#End Region

#Region "بررسی و ثبت ارزیابی"

    Private Sub CheckEvaluationStatus()
        Try
            Dim personCode As String = hdnPersonCode.Value
            Dim courseId As String = hdnCourseId.Value

            If String.IsNullOrEmpty(personCode) OrElse String.IsNullOrEmpty(courseId) Then
                Exit Sub
            End If

            Using cnn As New SqlConnection(GetConnectionStringWithTimeout())
                Dim sqlstr As String = "SELECT ID, TotalScore FROM trnCourseEvaluationTbl WHERE PersonCode = @PersonCode AND CourseID = @CourseID"
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.CommandTimeout = 60
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    cmd.Parameters.AddWithValue("@CourseID", courseId)
                    cnn.Open()

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            divEvaluation.Visible = False
                            divEvaluationDone.Visible = True
                            lblPreviousScore.InnerText = reader("TotalScore").ToString()
                            hdnEvaluationId.Value = reader("ID").ToString()
                        Else
                            divEvaluation.Visible = True
                            divEvaluationDone.Visible = False
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            divEvaluation.Visible = True
            divEvaluationDone.Visible = False
        End Try
    End Sub

    Protected Sub btnSubmitEvaluation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmitEvaluation.Click
        Try
            btnSubmitEvaluation.Enabled = False
            btnSubmitEvaluation.Text = "⏳ در حال ثبت..."

            Dim personCode As String = hdnPersonCode.Value
            Dim courseId As String = hdnCourseId.Value
            Dim userId As String = Session("userid").ToString()

            If String.IsNullOrEmpty(personCode) OrElse String.IsNullOrEmpty(courseId) Then
                ShowStatus("❌ خطا: اطلاعات ناقص است", "scan-error")
                btnSubmitEvaluation.Enabled = True
                btnSubmitEvaluation.Text = "📊 ثبت ارزیابی"
                Exit Sub
            End If

            Dim score1 As Integer = GetRangeValue(eval1.Value)
            Dim score2 As Integer = GetRangeValue(eval2.Value)
            Dim score3 As Integer = GetRangeValue(eval3.Value)
            Dim score4 As Integer = GetRangeValue(eval4.Value)
            Dim score5 As Integer = GetRangeValue(eval5.Value)
            Dim score6 As Integer = GetRangeValue(eval6.Value)
            Dim score7 As Integer = GetRangeValue(eval7.Value)
            Dim score8 As Integer = GetRangeValue(eval8.Value)

            Dim totalScore As Integer = score1 + score2 + score3 + score4 + score5 + score6 + score7 + score8
            Dim suggestions As String = txtSuggestions.Value

            Using cnn As New SqlConnection(GetConnectionStringWithTimeout())
                cnn.Open()

                Dim sqlstr As String = "INSERT INTO trnCourseEvaluationTbl " & _
                                      "(PersonCode, CourseID, Eval1, Eval2, Eval3, Eval4, Eval5, Eval6, Eval7, Eval8, TotalScore, Suggestions, UserID, CreateDate) " & _
                                      "VALUES (@PersonCode, @CourseID, @Eval1, @Eval2, @Eval3, @Eval4, @Eval5, @Eval6, @Eval7, @Eval8, @TotalScore, @Suggestions, @UserID, @CreateDate)"

                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.CommandTimeout = 60
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    cmd.Parameters.AddWithValue("@CourseID", courseId)
                    cmd.Parameters.AddWithValue("@Eval1", score1)
                    cmd.Parameters.AddWithValue("@Eval2", score2)
                    cmd.Parameters.AddWithValue("@Eval3", score3)
                    cmd.Parameters.AddWithValue("@Eval4", score4)
                    cmd.Parameters.AddWithValue("@Eval5", score5)
                    cmd.Parameters.AddWithValue("@Eval6", score6)
                    cmd.Parameters.AddWithValue("@Eval7", score7)
                    cmd.Parameters.AddWithValue("@Eval8", score8)
                    cmd.Parameters.AddWithValue("@TotalScore", totalScore)
                    cmd.Parameters.AddWithValue("@Suggestions", suggestions)
                    cmd.Parameters.AddWithValue("@UserID", userId)
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        ShowStatus("✅ ارزیابی شما با موفقیت ثبت شد. متشکریم!", "scan-success")
                        divEvaluation.Visible = False
                        divEvaluationDone.Visible = True
                        lblPreviousScore.InnerText = totalScore.ToString()
                    Else
                        ShowStatus("❌ خطا در ثبت ارزیابی", "scan-error")
                    End If
                End Using
            End Using

        Catch ex As Exception
            ShowStatus("❌ خطا در ثبت ارزیابی: " & ex.Message, "scan-error")
        Finally
            btnSubmitEvaluation.Enabled = True
            btnSubmitEvaluation.Text = "📊 ثبت ارزیابی"
        End Try
    End Sub

    Private Function GetRangeValue(ByVal value As String) As Integer
        Dim result As Integer = 0
        Integer.TryParse(value, result)
        Return result
    End Function

#End Region

#Region "توابع کمکی"

    Private Sub ShowStatus(ByVal message As String, ByVal cssClass As String)
        divStatus.InnerHtml = message
        divStatus.Attributes("class") = "scan-status " & cssClass
        divStatus.Visible = True
    End Sub

    Protected Sub btnBackToHome_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBackToHome.Click
        Response.Redirect("~/index.aspx")
    End Sub

    ' ============================================
    ' توابع کمکی برای نمایش در Repeater
    ' ============================================
    Public Function GetFileIcon(ByVal fileName As Object) As String
        If fileName Is Nothing OrElse String.IsNullOrEmpty(fileName.ToString()) Then
            Return "📄"
        End If

        Dim name As String = fileName.ToString().ToLower()
        If name.EndsWith(".pdf") Then Return "📕"
        If name.EndsWith(".doc") OrElse name.EndsWith(".docx") Then Return "📘"
        If name.EndsWith(".xls") OrElse name.EndsWith(".xlsx") Then Return "📊"
        If name.EndsWith(".ppt") OrElse name.EndsWith(".pptx") Then Return "📙"
        If name.EndsWith(".jpg") OrElse name.EndsWith(".jpeg") OrElse name.EndsWith(".png") OrElse name.EndsWith(".gif") OrElse name.EndsWith(".bmp") Then Return "🖼️"
        If name.EndsWith(".mp4") OrElse name.EndsWith(".avi") OrElse name.EndsWith(".mkv") OrElse name.EndsWith(".mov") OrElse name.EndsWith(".wmv") Then Return "🎬"
        If name.EndsWith(".mp3") OrElse name.EndsWith(".wav") OrElse name.EndsWith(".aac") OrElse name.EndsWith(".flac") Then Return "🎵"
        If name.EndsWith(".zip") OrElse name.EndsWith(".rar") OrElse name.EndsWith(".7z") Then Return "📦"
        If name.EndsWith(".exe") OrElse name.EndsWith(".msi") Then Return "⚙️"
        If name.EndsWith(".txt") Then Return "📃"
        Return "📄"
    End Function

    Public Function FormatFileSize(ByVal size As Object) As String
        If size Is Nothing OrElse Convert.ToInt64(size) = 0 Then
            Return "0 B"
        End If

        Dim bytes As Long = Convert.ToInt64(size)
        Dim sizes As String() = {"B", "KB", "MB", "GB", "TB"}
        Dim order As Integer = 0
        While bytes >= 1024 AndAlso order < sizes.Length - 1
            order += 1
            bytes = bytes / 1024
        End While

        Return String.Format("{0:0.##} {1}", bytes, sizes(order))
    End Function

#End Region

End Class