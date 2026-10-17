Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Ionic.Zip
Imports System.Text

Partial Class trnCertificateFrm
    Inherits System.Web.UI.Page

    ' کلاس اطلاعات فایل
    Public Class FileItemInfo
        Public Property Url As String
        Public Property Name As String
        Public Property FileName As String
        Public Property Caption As String
        Public Property Nat As String
        Public Property FullPath As String
        Public Property IsImage As Boolean
        Public Property Extension As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            meno_acc()
            LoadCourses()  ' بارگذاری دوره‌ها با چک‌باکس

            ' اگر کد پرسنل در Session وجود داشت نمایش بده
            If Session("codepersonel") IsNot Nothing Then
                txtCodePersonel.Text = Session("codepersonel").ToString()
                LoadPerson()
            End If
        End If
    End Sub

    ' ============================================================
    ' بارگذاری دوره‌های آموزشی با چک‌باکس (چند ردیفه)
    ' ============================================================
    ' ============================================================
    ' بارگذاری دوره‌های آموزشی با چک‌باکس (چند ردیفه) - اصلاح شده
    ' ============================================================
    Private Sub LoadCourses()
        Try
            ' پاک کردن آیتم‌های قبلی
            chkCourses.Items.Clear()

            ' اگر کد پرسنل خالی است، خارج شو
            If String.IsNullOrEmpty(txtCodePersonel.Text) Then
                lblMessage.Text = "لطفاً ابتدا کد پرسنل را جستجو کنید"
                lblMessage.ForeColor = Drawing.Color.Orange
                Exit Sub
            End If

            ' دریافت داده از دیتابیس - مرتب‌سازی بر اساس نام دوره
            Dim sqlstr As String = "SELECT idd, dorehayeGozrande FROM trnPersonelDoreTbl WHERE codePersoneli = " & txtCodePersonel.Text & " ORDER BY dorehayeGozrande"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim item As New ListItem()
                    item.Text = row("dorehayeGozrande").ToString().Trim()
                    item.Value = row("idd").ToString()
                    chkCourses.Items.Add(item)
                Next

                ' تنظیم تعداد ستون‌ها بر اساس تعداد دوره‌ها
                Dim totalItems As Integer = chkCourses.Items.Count
                Select Case totalItems
                    Case 1 To 4
                        chkCourses.RepeatColumns = 1
                    Case 5 To 8
                        chkCourses.RepeatColumns = 2
                    Case 9 To 16
                        chkCourses.RepeatColumns = 3
                    Case Else
                        chkCourses.RepeatColumns = 4
                End Select

                ' تنظیم RepeatLayout به Flow برای انعطاف‌پذیری بیشتر
                chkCourses.RepeatLayout = Web.UI.WebControls.RepeatLayout.Flow
                chkCourses.RepeatDirection = Web.UI.WebControls.RepeatDirection.Horizontal

                lblMessage.Text = "✅ " & chkCourses.Items.Count & " دوره یافت شد"
                lblMessage.ForeColor = Drawing.Color.Green
            Else
                lblMessage.Text = "⚠️ هیچ دوره‌ای برای این پرسنل یافت نشد"
                lblMessage.ForeColor = Drawing.Color.Orange
            End If

        Catch ex As Exception
            msg_box("خطا در بارگذاری دوره‌ها: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' دریافت لیست دوره‌های انتخاب شده
    ' ============================================================
    Private Function GetSelectedCourses() As List(Of String)
        Dim selectedCourses As New List(Of String)

        For Each item As ListItem In chkCourses.Items
            If item.Selected Then
                selectedCourses.Add(item.Value)  ' یا item.Text بسته به نیاز
            End If
        Next

        Return selectedCourses
    End Function

    ' ============================================================
    ' دریافت نام دوره‌های انتخاب شده به صورت رشته
    ' ============================================================
    Private Function GetSelectedCourseNames() As String
        Dim names As New List(Of String)

        For Each item As ListItem In chkCourses.Items
            If item.Selected Then
                names.Add(item.Text)
            End If
        Next

        Return String.Join(" - ", names)
    End Function

    ' ============================================================
    ' سایر توابع
    ' ============================================================

    ' کوتاه کردن نام فایل
    Public Function TruncateFileName(ByVal fileName As String, ByVal maxLength As Integer) As String
        If String.IsNullOrEmpty(fileName) Then Return ""
        If fileName.Length <= maxLength Then
            Return fileName
        End If
        Return fileName.Substring(0, maxLength - 3) & "..."
    End Function

    ' پاک کردن لیست فایل‌ها
    Private Sub ClearFilesList()
        pnlEmpty.Visible = True
        RepeaterImages.DataSource = Nothing
        RepeaterImages.DataBind()
    End Sub

    ' بررسی دسترسی
    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("trnCertificateFrm.aspx", Val(Session("UserId")))
        If meno_arr(0) = False Then
            Response.Redirect("login_2.aspx")
        End If
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

    ' جستجوی پرسنل
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtCodePersonel.Text.Trim() = "" Then
            msg_box("لطفاً کد پرسنلی را وارد کنید")
            txtCodePersonel.Focus()
            Exit Sub
        End If

        ' ذخیره در Session
        Session("codepersonel") = txtCodePersonel.Text.Trim()

        ' بارگذاری اطلاعات
        LoadPerson()
    End Sub

    ' بارگذاری اطلاعات پرسنل
    Private Sub LoadPerson()
        Try
            Dim codePersonel As Integer = Val(txtCodePersonel.Text)

            If codePersonel = 0 Then
                msg_box("کد پرسنلی معتبر نیست")
                Exit Sub
            End If

            ' استفاده از جدول personel
            Dim sqlstr As String = "SELECT nam, family, codeMelli FROM personel WHERE codepersoneli = " & codePersonel

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                txtFullName.Text = row("nam").ToString() & " " & row("family").ToString()
                txtCodeMeli.Text = row("codeMelli").ToString()

                ' بارگذاری دوره‌ها و فایل‌ها
                LoadCourses()
                LoadImages()
            Else
                msg_box("کد پرسنلی یافت نشد")
                txtFullName.Text = ""
                txtCodeMeli.Text = ""
                ClearFilesList()
                chkCourses.Items.Clear()
            End If

        Catch ex As Exception
            msg_box("خطا: " & ex.Message)
        End Try
    End Sub

    ' بارگذاری تصاویر و فایل‌ها
    Private Sub LoadImages()
        If String.IsNullOrEmpty(txtCodeMeli.Text) Then
            ClearFilesList()
            Exit Sub
        End If

        Dim folderPath As String = Server.MapPath("~/Attachment/TRN/" & txtCodeMeli.Text & "/")

        If Not Directory.Exists(folderPath) Then
            pnlEmpty.Visible = True
            RepeaterImages.DataSource = Nothing
            RepeaterImages.DataBind()
            Exit Sub
        End If

        Dim files = Directory.GetFiles(folderPath).ToList()

        If files.Count = 0 Then
            pnlEmpty.Visible = True
            RepeaterImages.DataSource = Nothing
            RepeaterImages.DataBind()
            Exit Sub
        End If

        pnlEmpty.Visible = False

        ' ساخت لیست فایل‌ها
        Dim imageList As New List(Of FileItemInfo)

        For Each file In files
            Dim fileName = Path.GetFileName(file)
            Dim extension = Path.GetExtension(file).ToLower()
            Dim isImage As Boolean = (extension = ".jpg" OrElse extension = ".jpeg" OrElse _
                                      extension = ".png" OrElse extension = ".gif" OrElse _
                                      extension = ".bmp" OrElse extension = ".webp")

            Dim item As New FileItemInfo With {
                .Url = ResolveUrl("~/Attachment/TRN/" & txtCodeMeli.Text & "/" & fileName),
                .Name = fileName,
                .FileName = fileName,
                .Caption = fileName,
                .Nat = txtCodeMeli.Text,
                .FullPath = file,
                .IsImage = isImage,
                .Extension = extension.ToUpper()
            }
            imageList.Add(item)
        Next

        RepeaterImages.DataSource = imageList
        RepeaterImages.DataBind()
    End Sub

    ' آپلود فایل
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        ' بررسی کد ملی
        If String.IsNullOrEmpty(txtCodeMeli.Text) Then
            msg_box("لطفاً ابتدا کد پرسنل معتبر جستجو کنید")
            Exit Sub
        End If

        ' دریافت دوره‌های انتخاب شده
        Dim selectedCourses As List(Of String) = GetSelectedCourses()
        If selectedCourses.Count = 0 Then
            msg_box("لطفاً حداقل یک دوره را انتخاب کنید")
            Exit Sub
        End If

        Try
            ' بررسی انتخاب فایل
            If Not FileUploadHealth.HasFile Then
                msg_box("لطفاً فایل مورد نظر را انتخاب کنید")
                Exit Sub
            End If

            ' بررسی حجم فایل (حداکثر 5 مگابایت)
            If FileUploadHealth.PostedFile.ContentLength > 5 * 1024 * 1024 Then
                msg_box("حجم فایل نباید بیشتر از 5 مگابایت باشد")
                Exit Sub
            End If

            ' بررسی نوع فایل
            Dim extension = Path.GetExtension(FileUploadHealth.FileName).ToLower()
            Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".zip", ".rar"}

            If Not allowedExtensions.Contains(extension) Then
                msg_box("نوع فایل مجاز نیست. فایل‌های مجاز: " & String.Join(", ", allowedExtensions))
                Exit Sub
            End If

            ' مسیر پوشه
            Dim folderPath As String = Server.MapPath("~/Attachment/TRN/" & txtCodeMeli.Text & "/")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            ' ایجاد نام فایل با شامل دوره‌های انتخاب شده
            Dim courseNames As String = GetSelectedCourseNames()

            ' حذف کاراکترهای غیرمجاز از نام فایل
            Dim safeName As String = courseNames
            For Each ch In Path.GetInvalidFileNameChars()
                safeName = safeName.Replace(ch, "_")
            Next

            ' محدود کردن طول نام فایل
            If safeName.Length > 100 Then
                safeName = safeName.Substring(0, 100)
            End If

            Dim fileName As String = safeName & extension
            Dim fullPath = Path.Combine(folderPath, fileName)

            ' بررسی وجود فایل تکراری
            If File.Exists(fullPath) Then
                Dim counter As Integer = 1
                Dim newFileName As String = Path.GetFileNameWithoutExtension(fileName) & "_" & counter & extension
                Dim newFullPath As String = Path.Combine(folderPath, newFileName)
                While File.Exists(newFullPath)
                    counter += 1
                    newFileName = Path.GetFileNameWithoutExtension(fileName) & "_" & counter & extension
                    newFullPath = Path.Combine(folderPath, newFileName)
                End While
                fullPath = newFullPath
            End If

            ' ذخیره فایل
            FileUploadHealth.SaveAs(fullPath)

            ' ذخیره در دیتابیس (اختیاری)
            SaveFileToDatabase(fileName, fullPath, selectedCourses)

            msg_box("✅ فایل با موفقیت آپلود شد")
            LoadImages()

            ' ریست کردن انتخاب‌ها
            For Each item As ListItem In chkCourses.Items
                item.Selected = False
            Next

            lblMessage.Text = "✅ فایل با موفقیت آپلود شد"
            lblMessage.ForeColor = Drawing.Color.Green

        Catch ex As Exception
            msg_box("خطا در آپلود فایل: " & ex.Message)
        End Try
    End Sub

    ' ذخیره اطلاعات فایل در دیتابیس (اختیاری)
    Private Sub SaveFileToDatabase(fileName As String, fullPath As String, selectedCourses As List(Of String))
        Try
            ' این تابع را بر اساس ساختار دیتابیس خود پیاده‌سازی کنید
            ' مثال:
            Dim courseIds As String = String.Join(",", selectedCourses)
            Dim courseNames As String = GetSelectedCourseNames()

            Dim sql As New StringBuilder()
            sql.AppendLine("INSERT INTO trnCertificateTbl (")
            sql.AppendLine("    CodePersoneli, CodeMelli, FileName, FilePath, CourseIds, CourseNames, UploadDate")
            sql.AppendLine(") VALUES (")
            sql.AppendLine("    @CodePersoneli, @CodeMelli, @FileName, @FilePath, @CourseIds, @CourseNames, GETDATE()")
            sql.AppendLine(")")

            ' استفاده از پارامترها برای امنیت
            ' ... کد اجرای دستور SQL با پارامترها

        Catch ex As Exception
            ' خطا را ثبت کنید ولی ادامه دهید
            LogError("خطا در ذخیره اطلاعات فایل: " & ex.Message)
        End Try
    End Sub

    ' ثبت خطا (اختیاری)
    Private Sub LogError(message As String)
        Try
            Dim logPath As String = Server.MapPath("~/Logs/ErrorLog.txt")
            Dim logDir As String = Path.GetDirectoryName(logPath)
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            Using writer As New StreamWriter(logPath, True)
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - " & message)
            End Using
        Catch
            ' خطا در ثبت خطا را نادیده بگیر
        End Try
    End Sub

    ' حذف فایل
    Protected Sub RepeaterImages_ItemCommand(sender As Object, e As RepeaterCommandEventArgs) Handles RepeaterImages.ItemCommand
        If e.CommandName = "DeleteFile" Then
            Dim filePath As String = e.CommandArgument.ToString()

            Try
                If File.Exists(filePath) Then
                    Dim fileName = Path.GetFileName(filePath)
                    File.Delete(filePath)
                    msg_box("✅ فایل " & fileName & " با موفقیت حذف شد")
                    LoadImages()
                Else
                    msg_box("⚠️ فایل مورد نظر وجود ندارد")
                End If
            Catch ex As Exception
                msg_box("خطا در حذف فایل: " & ex.Message)
            End Try
        End If
    End Sub

    ' دانلود ZIP
    Protected Sub btnDownloadZip_Click(sender As Object, e As EventArgs) Handles btnDownloadZip.Click
        If String.IsNullOrEmpty(txtCodeMeli.Text) Then
            msg_box("لطفاً ابتدا کد پرسنل معتبر جستجو کنید")
            Return
        End If

        Dim folderPath As String = Server.MapPath("~/Attachment/TRN/" & txtCodeMeli.Text & "/")

        If Not Directory.Exists(folderPath) Then
            msg_box("پوشه مورد نظر وجود ندارد")
            Return
        End If

        Dim files = Directory.GetFiles(folderPath).ToList()

        If files.Count = 0 Then
            msg_box("هیچ فایلی برای دانلود وجود ندارد")
            Return
        End If

        Response.Clear()
        Response.BufferOutput = False
        Response.ContentType = "application/zip"
        Response.AddHeader("content-disposition", "attachment; filename=" & txtCodeMeli.Text & "_" & DateTime.Now.ToString("yyyyMMdd") & ".zip")

        Using zip As New ZipFile()
            For Each path In files
                If File.Exists(path) Then
                    zip.AddFile(path, "")
                End If
            Next
            zip.Save(Response.OutputStream)
        End Using

        Response.Flush()
        Response.End()
    End Sub

End Class