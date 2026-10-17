Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions

Partial Class ForgotPass
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("FP_PersonCode") = Nothing
            Session("FP_Mobile") = Nothing
            Session("FP_VerifyCode") = Nothing
            Session("FP_Verified") = Nothing
            Session("FP_CodeTime") = Nothing
            Session("FP_LastSMSTime") = Nothing
        End If
    End Sub

    ' ============================================================
    ' متد کمکی
    ' ============================================================
    Private Function GetDataTable(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
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
    ' مخفی کردن شماره موبایل
    ' ============================================================
    Private Function MaskMobile(ByVal mobile As String) As String
        Try
            If String.IsNullOrEmpty(mobile) OrElse mobile.Length < 10 Then
                Return mobile
            End If

            If mobile.Length = 11 Then
                Return mobile.Substring(0, 4) & "***" & mobile.Substring(7, 4)
            End If

            Return mobile
        Catch ex As Exception
            Return mobile
        End Try
    End Function

    ' ============================================================
    ' تولید کد تایید 5 رقمی
    ' ============================================================
    Private Function GenerateVerifyCode() As String
        Dim rnd As New Random()
        Return rnd.Next(10000, 99999).ToString()
    End Function

    ' ============================================================
    ' ثبت پیامک در جدول smsTbl
    ' ============================================================
    Private Sub InsertSMS(ByVal mobile As String, ByVal message As String)
        Try
            Dim sql As String = "INSERT INTO smsTbl (mobile, message, tarix, saat, stat) " &
                "VALUES (@Mobile, @Message, @Tarix, @Saat, 0)"

            Dim persianDate As String = GetPersianDate(DateTime.Now)
            Dim timeStr As String = DateTime.Now.ToString("HH:mm:ss")

            Using conn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Mobile", mobile)
                    cmd.Parameters.AddWithValue("@Message", message)
                    cmd.Parameters.AddWithValue("@Tarix", persianDate)
                    cmd.Parameters.AddWithValue("@Saat", timeStr)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            LogError("InsertSMS: " & ex.Message)
            Throw New Exception("خطا در ارسال پیامک: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' تبدیل تاریخ به شمسی
    ' ============================================================
    Private Function GetPersianDate(ByVal dt As DateTime) As String
        Try
            Dim pc As New System.Globalization.PersianCalendar()
            Return pc.GetYear(dt).ToString() & "/" & pc.GetMonth(dt).ToString().PadLeft(2, "0"c) & "/" & pc.GetDayOfMonth(dt).ToString().PadLeft(2, "0"c)
        Catch ex As Exception
            Return dt.ToString("yyyy/MM/dd")
        End Try
    End Function

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        pnlMessage.Visible = True
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
                divMessage.Attributes("class") = "fp-alert fp-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                divMessage.Attributes("class") = "fp-alert fp-alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
                divMessage.Attributes("class") = "fp-alert fp-alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                divMessage.Attributes("class") = "fp-alert fp-alert-info"
        End Select
    End Sub

    ' ============================================================
    ' ثبت خطا
    ' ============================================================
    Private Sub LogError(ByVal errorMsg As String)
        Try
            Dim logPath As String = Server.MapPath("~/logs/")
            If Not System.IO.Directory.Exists(logPath) Then
                System.IO.Directory.CreateDirectory(logPath)
            End If
            System.IO.File.AppendAllText(logPath & "error_log.txt",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - ForgotPass: " & errorMsg & Environment.NewLine)
        Catch
        End Try
    End Sub

    ' ============================================================
    ' مرحله 1: ارسال کد تایید
    ' ============================================================
    Protected Sub btnSendCode_Click(sender As Object, e As EventArgs) Handles btnSendCode.Click
        Try
            btnSendCode.Enabled = False
            btnSendCode.Text = "⏳ در حال ارسال..."

            If Session("FP_LastSMSTime") IsNot Nothing Then
                Dim lastTime As DateTime = Convert.ToDateTime(Session("FP_LastSMSTime"))
                If DateTime.Now.Subtract(lastTime).TotalSeconds < 30 Then
                    Dim remaining As Integer = 30 - CInt(DateTime.Now.Subtract(lastTime).TotalSeconds)
                    ShowMessage("لطفاً " & remaining & " ثانیه دیگر صبر کنید", "warning")
                    Return
                End If
            End If

            Dim personCode As String = txtPersonCode.Text.Trim()

            If String.IsNullOrEmpty(personCode) Then
                ShowMessage("لطفاً کد پرسنلی خود را وارد کنید", "danger")
                Return
            End If

            Dim sql As String = "SELECT codePersoneli, nam, family, mobile FROM personel WHERE codePersoneli = @PersonCode"
            Dim dt As DataTable = GetDataTable(sql, New SqlParameter("@PersonCode", personCode))

            If dt.Rows.Count = 0 Then
                ShowMessage("کد پرسنلی در سامانه یافت نشد", "danger")
                Return
            End If

            Dim row As DataRow = dt.Rows(0)
            Dim mobile As String = ""

            If row("mobile") IsNot DBNull.Value Then
                mobile = row("mobile").ToString().Trim()
            End If

            If String.IsNullOrEmpty(mobile) Then
                ShowMessage("شماره موبایلی برای این کد پرسنلی ثبت نشده است. لطفاً با پشتیبانی تماس بگیرید.", "danger")
                Return
            End If

            Dim verifyCode As String = GenerateVerifyCode()

            ' ============================================================
            ' متن پیامک کوتاه (کاهش هزینه)
            ' ============================================================
            Dim smsText As String = "کد بازیابی رمز: " & verifyCode & Environment.NewLine & _
                "اعتبار: 2 دقیقه"

            InsertSMS(mobile, smsText)

            Session("FP_LastSMSTime") = DateTime.Now
            Session("FP_PersonCode") = personCode
            Session("FP_Mobile") = mobile
            Session("FP_VerifyCode") = verifyCode
            Session("FP_CodeTime") = DateTime.Now
            Session("FP_Verified") = False

            lblMaskedMobile.Text = MaskMobile(mobile)

            pnlStep1.Visible = False
            pnlStep2.Visible = True
            pnlStep3.Visible = False

            step1.Attributes("class") = "fp-step completed"
            step2.Attributes("class") = "fp-step active"
            step3.Attributes("class") = "fp-step"

            ShowMessage("کد تایید به شماره " & MaskMobile(mobile) & " ارسال شد", "success")

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "startTimer",
                "setTimeout(function(){ startTimer(); }, 100);", True)

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        Finally
            btnSendCode.Enabled = True
            btnSendCode.Text = "📱 ارسال کد تایید"
        End Try
    End Sub

    ' ============================================================
    ' مرحله 2: تایید کد
    ' ============================================================
    Protected Sub btnVerifyCode_Click(sender As Object, e As EventArgs) Handles btnVerifyCode.Click
        Try
            btnVerifyCode.Enabled = False
            btnVerifyCode.Text = "⏳ در حال بررسی..."

            Dim enteredCode As String = txtVerifyCode.Text.Trim()

            If String.IsNullOrEmpty(enteredCode) Then
                ShowMessage("لطفاً کد تایید را وارد کنید", "danger")
                Return
            End If

            If enteredCode.Length <> 5 Then
                ShowMessage("کد تایید باید 5 رقم باشد", "danger")
                Return
            End If

            If Session("FP_VerifyCode") Is Nothing Then
                ShowMessage("لطفاً مجدداً کد تایید دریافت کنید", "danger")
                Return
            End If

            Dim savedCode As String = Session("FP_VerifyCode").ToString()

            If Session("FP_CodeTime") IsNot Nothing Then
                Dim codeTime As DateTime = Convert.ToDateTime(Session("FP_CodeTime"))
                If DateTime.Now.Subtract(codeTime).TotalMinutes > 2 Then
                    ShowMessage("کد تایید منقضی شده است. لطفاً کد جدید دریافت کنید", "warning")
                    Return
                End If
            End If

            If enteredCode <> savedCode Then
                ShowMessage("کد تایید وارد شده صحیح نیست", "danger")
                txtVerifyCode.Text = ""
                Return
            End If

            Session("FP_Verified") = True

            pnlStep1.Visible = False
            pnlStep2.Visible = False
            pnlStep3.Visible = True

            step1.Attributes("class") = "fp-step completed"
            step2.Attributes("class") = "fp-step completed"
            step3.Attributes("class") = "fp-step active"

            ShowMessage("هویت شما تایید شد. لطفاً رمز عبور جدید را وارد کنید", "success")

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "stopTimer", "stopTimer();", True)

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        Finally
            btnVerifyCode.Enabled = True
            btnVerifyCode.Text = "✅ تایید کد"
        End Try
    End Sub

    ' ============================================================
    ' ارسال مجدد کد
    ' ============================================================
    Protected Sub btnResendCode_Click(sender As Object, e As EventArgs) Handles btnResendCode.Click
        Try
            btnResendCode.Enabled = False
            btnResendCode.Text = "⏳ در حال ارسال..."

            If Session("FP_LastSMSTime") IsNot Nothing Then
                Dim lastTime As DateTime = Convert.ToDateTime(Session("FP_LastSMSTime"))
                If DateTime.Now.Subtract(lastTime).TotalSeconds < 30 Then
                    Dim remaining As Integer = 30 - CInt(DateTime.Now.Subtract(lastTime).TotalSeconds)
                    ShowMessage("لطفاً " & remaining & " ثانیه دیگر صبر کنید", "warning")
                    Return
                End If
            End If

            If Session("FP_PersonCode") Is Nothing OrElse Session("FP_Mobile") Is Nothing Then
                ShowMessage("لطفاً مجدداً کد پرسنلی را وارد کنید", "danger")
                Return
            End If

            Dim mobile As String = Session("FP_Mobile").ToString()

            Dim verifyCode As String = GenerateVerifyCode()

            ' ============================================================
            ' متن پیامک کوتاه (کاهش هزینه)
            ' ============================================================
            Dim smsText As String = "کد بازیابی رمز: " & verifyCode & Environment.NewLine & _
                "اعتبار: 2 دقیقه"

            InsertSMS(mobile, smsText)

            Session("FP_LastSMSTime") = DateTime.Now
            Session("FP_VerifyCode") = verifyCode
            Session("FP_CodeTime") = DateTime.Now

            ShowMessage("کد تایید جدید به شماره " & MaskMobile(mobile) & " ارسال شد", "success")

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "startTimer",
                "setTimeout(function(){ startTimer(); }, 100);", True)

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        Finally
            btnResendCode.Enabled = True
            btnResendCode.Text = "🔄 ارسال مجدد"
        End Try
    End Sub

    ' ============================================================
    ' بازگشت به مرحله 1
    ' ============================================================
    Protected Sub btnBackToStep1_Click(sender As Object, e As EventArgs) Handles btnBackToStep1.Click
        Session("FP_PersonCode") = Nothing
        Session("FP_Mobile") = Nothing
        Session("FP_VerifyCode") = Nothing
        Session("FP_Verified") = Nothing
        Session("FP_CodeTime") = Nothing
        Session("FP_LastSMSTime") = Nothing

        pnlStep1.Visible = True
        pnlStep2.Visible = False
        pnlStep3.Visible = False

        step1.Attributes("class") = "fp-step active"
        step2.Attributes("class") = "fp-step"
        step3.Attributes("class") = "fp-step"

        txtPersonCode.Text = ""
        txtVerifyCode.Text = ""

        pnlMessage.Visible = False

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "stopTimer", "stopTimer();", True)
    End Sub

    ' ============================================================
    ' مرحله 3: تغییر رمز عبور
    ' ============================================================
    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Try
            ' ====== غیرفعال کردن دکمه ======
            btnChangePassword.Enabled = False
            btnChangePassword.Text = "⏳ در حال ذخیره..."

            If Session("FP_Verified") Is Nothing OrElse Convert.ToBoolean(Session("FP_Verified")) = False Then
                ShowMessage("لطفاً ابتدا هویت خود را تایید کنید", "danger")
                Return
            End If

            Dim newPassword As String = txtNewPassword.Text.Trim()
            Dim confirmPassword As String = txtConfirmPassword.Text.Trim()

            If String.IsNullOrEmpty(newPassword) Then
                ShowMessage("لطفاً رمز عبور جدید را وارد کنید", "danger")
                Return
            End If

            If newPassword.Length < 4 Then
                ShowMessage("رمز عبور باید حداقل 4 کاراکتر باشد", "danger")
                Return
            End If

            If newPassword <> confirmPassword Then
                ShowMessage("رمز عبور و تکرار آن مطابقت ندارند", "danger")
                Return
            End If

            Dim personCode As String = ""
            If Session("FP_PersonCode") IsNot Nothing Then
                personCode = Session("FP_PersonCode").ToString()
            End If

            If String.IsNullOrEmpty(personCode) Then
                ShowMessage("خطا در شناسایی کاربر. لطفاً مجدداً تلاش کنید", "danger")
                Return
            End If

            ' ============================================================
            ' به‌روزرسانی رمز عبور
            ' ============================================================
            Dim sql As String = "UPDATE personel SET pass = @NewPass WHERE codePersoneli = @PersonCode"

            Using conn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@NewPass", newPassword)
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    conn.Open()

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected = 0 Then
                        ShowMessage("خطا در تغییر رمز عبور. لطفاً با پشتیبانی تماس بگیرید", "danger")
                        Return
                    End If
                End Using
            End Using

            ' ============================================================
            ' پیامک تایید با رمز جدید (متن کوتاه)
            ' ============================================================
            Try
                Dim mobile As String = ""
                If Session("FP_Mobile") IsNot Nothing Then
                    mobile = Session("FP_Mobile").ToString()
                End If

                If Not String.IsNullOrEmpty(mobile) Then
                    ' ====== متن کوتاه با رمز جدید ======
                    Dim smsText As String = "رمز جدید: " & newPassword & Environment.NewLine & _
                        "سامانه پتروشیمی من"

                    InsertSMS(mobile, smsText)
                End If
            Catch ex As Exception
                LogError("SMS Confirm: " & ex.Message)
            End Try

            ' ====== پاکسازی Session ======
            Session("FP_PersonCode") = Nothing
            Session("FP_Mobile") = Nothing
            Session("FP_VerifyCode") = Nothing
            Session("FP_Verified") = Nothing
            Session("FP_CodeTime") = Nothing
            Session("FP_LastSMSTime") = Nothing

            ' ====== ذخیره پیام موفقیت برای صفحه لاگین ======
            Session("LoginSuccessMessage") = "رمز عبور شما با موفقیت تغییر یافت. لطفاً با رمز جدید وارد شوید."

            ' ====== هدایت مستقیم به Login_2.aspx ======
            Response.Redirect("Login_2.aspx")

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")

            ' ====== فعال کردن مجدد دکمه در صورت خطا ======
            btnChangePassword.Enabled = True
            btnChangePassword.Text = "💾 تغییر رمز عبور"
        End Try
    End Sub

    ' ============================================================
    ' انصراف و بازگشت به ورود
    ' ============================================================
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Session("FP_PersonCode") = Nothing
        Session("FP_Mobile") = Nothing
        Session("FP_VerifyCode") = Nothing
        Session("FP_Verified") = Nothing
        Session("FP_CodeTime") = Nothing
        Session("FP_LastSMSTime") = Nothing

        Response.Redirect("Login_2.aspx")
    End Sub

End Class