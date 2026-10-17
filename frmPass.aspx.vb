Imports System.Data
Imports System.Data.SqlClient

Partial Class frmPass
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' بررسی نشست کاربر
            If Session("userid") Is Nothing Then
                Response.Redirect("login.aspx")
            End If
        End If
    End Sub

    ' ذخیره رمز جدید
    Private Sub SavePassword()
        Try
            ' اعتبارسنجی سمت سرور
            If String.IsNullOrEmpty(txt_CurentPass.Text) Then
                msg_box("لطفاً رمز فعلی را وارد کنید")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txt_NewPass1.Text) Then
                msg_box("لطفاً رمز جدید را وارد کنید")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txt_NewPass2.Text) Then
                msg_box("لطفاً تکرار رمز جدید را وارد کنید")
                Exit Sub
            End If

            If txt_NewPass1.Text <> txt_NewPass2.Text Then
                msg_box("رمز جدید و تکرار آن مطابقت ندارند")
                Exit Sub
            End If

            If txt_NewPass1.Text.Length < 4 Then
                msg_box("رمز جدید باید حداقل ۴ کاراکتر باشد")
                Exit Sub
            End If

            ' بررسی رمز فعلی
            Dim userId As String = Session("userid").ToString()
            Dim sqlCheck As String = "SELECT id FROM personel WHERE ID = " & userId & " AND pass = '" & txt_CurentPass.Text & "'"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlCheck)

            If dt.Rows.Count > 0 Then
                ' به‌روزرسانی رمز جدید
                Dim sqlUpdate As String = "UPDATE personel SET pass = '" & txt_NewPass2.Text & "' WHERE ID = " & userId

                ' اجرای دستور UPDATE با استفاده از SqlCommand
                Dim cnn As New SqlConnection(ClsConnection.GetConn())
                cnn.Open()
                Dim cmm As New SqlCommand(sqlUpdate, cnn)
                cmm.ExecuteNonQuery()
                cnn.Close()

                msg_box("✅ رمز عبور با موفقیت تغییر یافت")

                ' پاک کردن فیلدها
                txt_CurentPass.Text = ""
                txt_NewPass1.Text = ""
                txt_NewPass2.Text = ""
            Else
                msg_box("❌ رمز فعلی اشتباه است")
            End If

        Catch ex As Exception
            msg_box("خطا در تغییر رمز: " & ex.Message)
        End Try
    End Sub

    ' دکمه ذخیره
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SavePassword()
    End Sub

    ' دکمه برگشت
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    ' نمایش پیغام
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
    End Sub

End Class