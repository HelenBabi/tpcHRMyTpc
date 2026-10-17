Imports System.Data.SqlClient
Imports System.Data

Partial Class AcceptRules
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlVerify.Visible = False
            lblInfo.Text = ""
            Session("CodeSent") = False ' کنترل ارسال کد
        End If
    End Sub

    ' دکمه تایید و ارسال کد
    Protected Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click

        If Session("userid") Is Nothing Then
            lblInfo.Text = "Session خالی است. لطفاً دوباره وارد شوید."
            lblInfo.CssClass = "text-danger"
            Return
        End If

        Dim personnelId As Integer = Session("userid")

        ' چک اگر قبلاً ثبت کرده
        Dim sqlCheck As String = "SELECT COUNT(*) FROM tblAcceptRules WHERE PersonnelId=" & personnelId
        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlCheck)
        If dt.Rows.Count > 0 AndAlso Convert.ToInt32(dt.Rows(0)(0)) > 0 Then
            lblInfo.Text = "❌ شما قبلاً ثبت کرده‌اید."
            msg_box("❌ شما قبلاً ثبت کرده‌اید.")
            lblInfo.CssClass = "text-danger"
            btnAccept.Visible = False
            pnlVerify.Visible = False
            Return
        End If

        ' فقط اگر هنوز کدی ارسال نشده باشد
        If Session("CodeSent") Is Nothing OrElse Session("CodeSent") = False Then
            SendVerificationCode()
            Session("CodeSent") = True
        End If

    End Sub
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub
    ' ارسال مجدد کد
    Protected Sub btnResend_Click(sender As Object, e As EventArgs) Handles btnResend.Click
        SendVerificationCode()
    End Sub

    ' تولید و ارسال کد تایید
    Private Sub SendVerificationCode()
        Dim mobile As String = Session("mobile").ToString()
        Dim cleanMobile As String = New String(mobile.Where(AddressOf Char.IsDigit).ToArray())
        If Not cleanMobile.StartsWith("0") Then cleanMobile = "0" & cleanMobile

        ' تولید کد ۶ رقمی
        Dim code As String = New Random().Next(100000, 999999).ToString()
        Dim expire As DateTime = DateTime.Now.AddMinutes(5)

        ' ذخیره در جدول PasswordResetCodes
        Dim sql As String = "INSERT INTO PasswordResetCodes (Mobile, VerificationCode, ExpireTime) VALUES ('" & cleanMobile & "', '" & code & "', '" & expire & "')"
        UpStat(sql)

        ' ذخیره پیام در جدول smsTbl
        Dim sqlSms As String = "INSERT INTO smsTbl (mobile, message, tarix, saat) VALUES ('" & cleanMobile & "', N'" & code & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"
        UpStat(sqlSms)

        lblInfo.Text = "کد تایید ارسال شد."
        lblInfo.CssClass = "text-info"

        btnAccept.Visible = False
        pnlVerify.Visible = True

        ' شروع تایمر جاوااسکریپت
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "timer", "startTimer(300);", True)
    End Sub

    ' تایید کد و ثبت نهایی
    Protected Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim codeInput As String = txtCode.Text.Trim()
        Dim personnelId As Integer = Session("userid")
        Dim mobile As String = Session("mobile").ToString()
        Dim cleanMobile As String = New String(mobile.Where(AddressOf Char.IsDigit).ToArray())
        If Not cleanMobile.StartsWith("0") Then cleanMobile = "0" & cleanMobile

        ' چک اگر قبلاً ثبت کرده
        Dim sqlCheckFinal As String = "SELECT COUNT(*) FROM tblAcceptRules WHERE PersonnelId=" & personnelId
        Dim dtCheck As DataTable = ClsDataTable.GetDataTable(sqlCheckFinal)
        If dtCheck.Rows.Count > 0 AndAlso Convert.ToInt32(dtCheck.Rows(0)(0)) > 0 Then
            lblInfo.Text = "❌ شما قبلاً ثبت نام کرده‌اید."
            lblInfo.CssClass = "text-danger"
            btnVerify.Visible = False
            pnlVerify.Visible = False
            Return
        End If

        ' بررسی کد تایید
        Dim sqlCode As String = "SELECT TOP 1 VerificationCode, ExpireTime FROM PasswordResetCodes WHERE Mobile='" & cleanMobile & "' ORDER BY ExpireTime DESC"
        Dim dtCode As DataTable = ClsDataTable.GetDataTable(sqlCode)

        If dtCode.Rows.Count = 0 Then
            lblInfo.Text = "❌ کد تایید پیدا نشد."
            lblInfo.CssClass = "text-danger"
            Return
        End If

        Dim correctCode As String = dtCode.Rows(0)("VerificationCode").ToString()
        Dim expireTime As DateTime = Convert.ToDateTime(dtCode.Rows(0)("ExpireTime"))

        If DateTime.Now > expireTime Then
            lblInfo.Text = "⏰ کد تایید منقضی شده است. لطفاً مجدد درخواست کنید."
            lblInfo.CssClass = "text-danger"
            btnResend.Visible = True
            Return
        End If

        If codeInput <> correctCode Then
            lblInfo.Text = "❌ کد وارد شده اشتباه است."
            lblInfo.CssClass = "text-danger"
            Return
        End If

        ' ثبت در tblAcceptRules
        Dim sqlInsert As String = "INSERT INTO tblAcceptRules (PersonnelId, AcceptDate) VALUES (" & personnelId & ", GETDATE())"
        UpStat(sqlInsert)

        msg_box("✅ ثبت نام با موفقیت انجام شد.")
        lblInfo.CssClass = "text-success"

        ' غیر فعال کردن دکمه‌ها و پاک کردن Session
        btnVerify.Visible = False
        txtCode.Visible = False
        btnResend.Visible = False
        Session("CodeSent") = Nothing
    End Sub


    ' اجرای دستورات SQL
    Private Sub UpStat(sqlstr As String)
        Using cnn As New SqlConnection(ClsConnection.GetConn())
            Using cmm As New SqlCommand(sqlstr, cnn)
                cnn.Open()
                cmm.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
