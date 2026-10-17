Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class frmChngPic
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' بررسی نشست کاربر
            If Session("userid") Is Nothing Then
                Response.Redirect("login.aspx")
            End If

            ' بارگذاری عکس فعلی
            LoadCurrentImage()
        End If
    End Sub

    ' بارگذاری عکس فعلی پروفایل
    Private Sub LoadCurrentImage()
        Try
            Dim codePersoneli As String = Session("codePersoneli").ToString()
            Dim imagePath As String = Server.MapPath("~/images/person_pic/" & codePersoneli & ".jpg")

            If File.Exists(imagePath) Then
                imgPreview.ImageUrl = "~/images/person_pic/" & codePersoneli & ".jpg"
            Else
                imgPreview.ImageUrl = "~/images/person_pic/person.ico"
            End If
        Catch ex As Exception
            imgPreview.ImageUrl = "~/images/person_pic/person.ico"
        End Try
    End Sub

    ' ذخیره عکس جدید
    Private Sub SaveImage()
        Try
            ' بررسی وجود فایل
            If Not FileUpload1.HasFile Then
                msg_box("❌ لطفاً یک فایل عکس انتخاب کنید")
                Exit Sub
            End If

            ' بررسی فرمت فایل
            Dim extension As String = Path.GetExtension(FileUpload1.FileName).ToLower()
            Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"}

            If Not allowedExtensions.Contains(extension) Then
                msg_box("❌ فقط فایل‌های تصویری مجاز هستند (jpg, jpeg, png, gif, bmp, webp)")
                Exit Sub
            End If

            ' بررسی حجم فایل (حداکثر 2 مگابایت)
            If FileUpload1.PostedFile.ContentLength > 2 * 1024 * 1024 Then
                msg_box("❌ حجم فایل نباید بیشتر از 2 مگابایت باشد")
                Exit Sub
            End If

            ' مسیر ذخیره‌سازی
            Dim savePath As String = Server.MapPath("~/images/person_pic/")

            ' ایجاد پوشه اگر وجود ندارد
            If Not Directory.Exists(savePath) Then
                Directory.CreateDirectory(savePath)
            End If

            ' ذخیره فایل با نام کد پرسنلی
            Dim codePersoneli As String = Session("codePersoneli").ToString()
            Dim fileName As String = codePersoneli & extension
            Dim fullPath As String = Path.Combine(savePath, fileName)

            ' حذف عکس قبلی اگر وجود دارد
            Dim oldFiles = Directory.GetFiles(savePath, codePersoneli & ".*")
            For Each oldFile In oldFiles
                Try
                    File.Delete(oldFile)
                Catch
                End Try
            Next

            ' ذخیره عکس جدید
            FileUpload1.SaveAs(fullPath)

            ' به‌روزرسانی پیش‌نمایش
            imgPreview.ImageUrl = "~/images/person_pic/" & fileName

            msg_box("✅ عکس پروفایل با موفقیت تغییر یافت")

        Catch ex As Exception
            msg_box("❌ خطا در ذخیره عکس: " & ex.Message)
        End Try
    End Sub

    ' اعتبارسنجی سمت سرور
    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            If FileUpload1.HasFile Then
                Dim extension As String = Path.GetExtension(FileUpload1.FileName).ToLower()
                Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"}

                If allowedExtensions.Contains(extension) Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                End If
            Else
                args.IsValid = False
            End If
        Catch
            args.IsValid = False
        End Try
    End Sub

    ' دکمه ذخیره
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            SaveImage()
        End If
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