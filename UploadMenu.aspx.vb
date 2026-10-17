Imports System
Imports System.IO
Imports System.Drawing
Imports System.Globalization
Imports System.Web.UI

Partial Class UploadMenu
    Inherits Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindMonths()
        End If
    End Sub

    Private Sub BindMonths()
        ddlMonth.Items.Clear()
        Dim monthNames() As String = {"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                                      "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"}

        For i As Integer = 1 To 12
            ddlMonth.Items.Add(New ListItem(monthNames(i - 1), i.ToString()))
        Next
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not fuMenu.HasFile Then
            lblResult.Text = "⚠ لطفاً یک فایل تصویر انتخاب کنید."
            lblResult.CssClass = "text-danger fw-bold"
            Return
        End If

        Try
            Dim ext As String = Path.GetExtension(fuMenu.FileName).ToLower()
            If ext <> ".jpg" AndAlso ext <> ".jpeg" AndAlso ext <> ".png" Then
                lblResult.Text = "فقط فایل‌های تصویری JPG یا PNG مجاز هستند."
                lblResult.CssClass = "text-danger fw-bold"
                Return
            End If

            ' مسیر ذخیره
            Dim folderPath As String = Server.MapPath("~/menuImages/")
            If Not Directory.Exists(folderPath) Then Directory.CreateDirectory(folderPath)

            ' نام فایل فقط بر اساس ماه
            Dim fileName As String = "menu_" & ddlMonth.SelectedValue & ext
            Dim filePath As String = Path.Combine(folderPath, fileName)

            ' تغییر اندازه تصویر به حداکثر 1200px عرض
            Using originalImg As Image = Image.FromStream(fuMenu.PostedFile.InputStream)
                Using resizedImg As Image = ResizeImage(originalImg, 1200)
                    resizedImg.Save(filePath, Imaging.ImageFormat.Jpeg)
                End Using
            End Using

            lblResult.Text = "✅ تصویر با موفقیت آپلود و ذخیره شد."
            lblResult.CssClass = "text-success fw-bold"

            ' نمایش پیش‌نمایش تصویر
            imgPreview.ImageUrl = "~/menuImages/" & fileName
            previewDiv.Visible = True

        Catch ex As Exception
            lblResult.Text = "❌ خطا در آپلود: " & ex.Message
            lblResult.CssClass = "text-danger fw-bold"
        End Try
    End Sub

    ' دکمه برگشت به صفحه اصلی
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx") ' مسیر صفحه اصلی پروژه
    End Sub

    ' تابع تغییر اندازه تصویر
    Private Function ResizeImage(img As Image, maxWidth As Integer) As Image
        If img.Width <= maxWidth Then
            Return CType(img.Clone(), Image)
        End If

        Dim ratio As Double = img.Height / img.Width
        Dim newHeight As Integer = CInt(maxWidth * ratio)
        Dim newImg As New Bitmap(maxWidth, newHeight)
        Using g As Graphics = Graphics.FromImage(newImg)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(img, 0, 0, maxWidth, newHeight)
        End Using
        Return newImg
    End Function

End Class
