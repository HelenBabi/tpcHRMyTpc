Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System
Imports System.Linq
Imports Ionic.Zip   ' برای دانلود همه (zip)

Partial Class ViewResults
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim download As String = Request.QueryString("download")
        If Not String.IsNullOrEmpty(download) Then
            ServeFile(download)
            Return
        End If

        If Not IsPostBack Then
            Dim nid As String = TryCast(Session("NationalCode"), String)
            nid = NormalizeDigits(nid)
            If Not String.IsNullOrEmpty(nid) Then
                ShowResults(nid)
            Else
                ltResults.Text = "<div class='alert alert-warning'>کد ملی در سشن پیدا نشد.</div>"
            End If

            If Request.QueryString("downloadall") = "1" Then
                DownloadAll(nid)
                Return
            End If
        End If
    End Sub

    Private Sub DownloadAll(nid As String)
        Dim resultsDir As String = Server.MapPath("~/App_Data/Results")
        nid = NormalizeDigits(nid)
        Dim files = Directory.GetFiles(resultsDir, "*" & nid & "*.*", SearchOption.AllDirectories)

        If files.Length = 0 Then
            Response.Clear()
            Response.ContentType = "text/plain; charset=utf-8"
            Response.Write("فایلی یافت نشد.")
            Response.End()
            Return
        End If

        Response.Clear()
        Response.BufferOutput = False
        Response.ContentType = "application/zip"
        Response.AddHeader("content-disposition", "attachment; filename=" & nid & "_AllResults.zip")

        Using zip As New ZipFile()
            For Each path In files
                If File.Exists(path) Then
                    Dim relPath As String = path.Replace(resultsDir & "\", "")
                    Dim folderInZip As String = System.IO.Path.GetDirectoryName(relPath)
                    If String.IsNullOrEmpty(folderInZip) Then folderInZip = ""
                    zip.AddFile(path, folderInZip)

                End If
            Next
            zip.Save(Response.OutputStream)
        End Using

        Response.Flush()
        Response.End()
    End Sub



    Private Sub ShowResults(nid As String)
        ' ---------- ۱) نرمال‌سازی کد ملی ----------
        nid = NormalizeDigits(nid)
        nid = nid.Replace(" ", "").Replace("-", "").Replace(vbTab, "").Trim()

        If nid.Length = 0 Then
            ltResults.Text = "<div class='alert alert-danger'>شماره ملی وارد نشده است.</div>"
            Return
        End If

        ' اگر می‌خواهی اعتبارسنجی هم فعال باشد، خط زیر را از حالت کامنت خارج کن
        ' If Not Regex.IsMatch(nid, "^\d{8,10}$") Then
        '     ltResults.Text = "<div class='alert alert-danger'>شماره ملی نامعتبر است.</div>"
        '     Return
        ' End If

        ' ---------- ۲) بررسی وجود پوشه ----------
        Dim resultsDir As String = Server.MapPath("~/App_Data/Results")
        If Not Directory.Exists(resultsDir) Then
            ltResults.Text = "<div class='alert alert-danger'>پوشه نتایج پیدا نشد.</div>"
            Return
        End If

        ' ---------- ۳) جستجوی دستی (راه‌حل اصلی مشکل) ----------
        Dim files As String()
        Try
            files = Directory.EnumerateFiles(resultsDir, "*", SearchOption.AllDirectories) _
                             .Where(Function(f) Path.GetFileName(f).IndexOf(nid, StringComparison.OrdinalIgnoreCase) >= 0) _
                             .OrderBy(Function(f) Path.GetFileName(f), StringComparer.OrdinalIgnoreCase) _
                             .ToArray()
        Catch ex As Exception
            ltResults.Text = "<div class='alert alert-danger'>خطا در خواندن فایل‌ها: " & HttpUtility.HtmlEncode(ex.Message) & "</div>"
            Return
        End Try

        If files.Length = 0 Then
            ltResults.Text = "<div class='alert alert-info'>فایلی برای شما یافت نشد.</div>"
            Return
        End If

        ' ---------- ۴) ساخت HTML ----------
        Dim sb As New StringBuilder(files.Length * 512)
        sb.AppendLine("<link href='css2/all.min.css' rel='stylesheet'>")
        sb.AppendLine("<div class='row'>")

        Dim colors() As String = {"primary", "success", "warning", "danger", "info", "secondary", "dark"}
        Dim icons() As String = {"fa-calendar", "fa-book", "fa-file-alt", "fa-file-pdf", "fa-star", "fa-check", "fa-circle"}

        Dim idx As Integer = 0
        For Each f As String In files
            ' مسیر نسبی با / برای لینک
            Dim relPath As String = f.Substring(resultsDir.Length).TrimStart("\"c).Replace("\"c, "/"c)
            Dim fname As String = Path.GetFileName(f)

            ' نام پوشه (اگر فایل در ریشه باشد، "-" نمایش داده می‌شود)
            Dim folderName As String = Path.GetDirectoryName(relPath)
            If String.IsNullOrEmpty(folderName) Then folderName = "-"
            folderName = folderName.Replace("\"c, "/"c)

            Dim color As String = colors(idx Mod colors.Length)
            Dim icon As String = icons(idx Mod icons.Length)
            idx += 1

            Dim fileUrl As String = "?download=" & Server.UrlEncode(relPath)

            ' HtmlEncode برای جلوگیری از XSS و خرابی HTML
            Dim safeFname As String = HttpUtility.HtmlEncode(fname)
            Dim safeFolder As String = HttpUtility.HtmlEncode(folderName)

            sb.AppendLine("<div class='col-sm-6 col-md-4 col-lg-3 mb-3'>")
            sb.AppendLine("<div class='card h-100 border-" & color & "'>")
            sb.AppendLine("<div class='card-header bg-" & color & " text-white'><i class='fas " & icon & " me-2'></i>" & safeFolder & "</div>")
            sb.AppendLine("<div class='card-body d-flex flex-column'>")
            sb.AppendLine("<p class='card-text text-truncate' title='" & safeFname & "'>" & safeFname & "</p>")
            sb.AppendLine("<div class='mt-auto'>")
            sb.AppendLine("<a class='btn btn-sm btn-" & color & " me-2' href='" & fileUrl & "'>دانلود</a>")
            sb.AppendLine("<a class='btn btn-sm btn-outline-" & color & "' target='_blank' href='" & fileUrl & "&view=1'>پیش‌نمایش</a>")
            sb.AppendLine("</div></div></div></div>")
        Next

        sb.AppendLine("</div>")
        sb.AppendLine("<br />")
        sb.AppendLine("<div class='row mt-4'>")
        sb.AppendLine("  <div class='col-12 text-center'>")
        sb.AppendLine("    <a class='btn btn-success me-2' href='?downloadall=1&amp;nid=" & Server.UrlEncode(nid) & "'>دانلود همه</a>")
        sb.AppendLine("    <a class='btn btn-secondary' href='Index.aspx'>بازگشت</a>")
        sb.AppendLine("  </div>")
        sb.AppendLine("</div>")

        ltResults.Text = sb.ToString()
    End Sub


    ''' <summary>
    ''' تبدیل اعداد فارسی و عربی به لاتین و حذف کاراکترهای کنترلی
    ''' </summary>
    Private Function NormalizeDigits(input As String) As String
        If String.IsNullOrEmpty(input) Then Return String.Empty

        Dim sb As New StringBuilder(input.Length)
        For Each c As Char In input
            Select Case c
                Case "۰"c To "۹"c   ' اعداد فارسی
                    sb.Append(ChrW(AscW(c) - AscW("۰"c) + AscW("0"c)))
                Case "٠"c To "٩"c   ' اعداد عربی
                    sb.Append(ChrW(AscW(c) - AscW("٠"c) + AscW("0"c)))
                Case ControlChars.NullChar, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                    ' حذف کاراکترهای کنترلی
                Case Else
                    sb.Append(c)
            End Select
        Next
        Return sb.ToString()
    End Function

    Private Sub ServeFile(fileName As String)
        Dim resultsDir As String = Server.MapPath("~/App_Data/Results")
        ' بازسازی مسیر ویندوزی
        Dim relPath = fileName.Replace("/", "\")
        Dim full = Path.Combine(resultsDir, relPath)

        If Not File.Exists(full) Then
            Response.Clear()
            Response.ContentType = "text/plain; charset=utf-8"
            Response.Write("فایل پیدا نشد: " & full)
            Response.End()
            Return
        End If

        Dim viewOnly = Not String.IsNullOrEmpty(Request.QueryString("view"))
        Dim ext = Path.GetExtension(full).ToLower()
        Dim contentType As String = "application/octet-stream"
        If ext = ".pdf" Then contentType = "application/pdf"
        If ext = ".jpg" Or ext = ".jpeg" Then contentType = "image/jpeg"
        If ext = ".png" Then contentType = "image/png"

        Response.Clear()
        Response.ContentType = contentType
        If Not viewOnly Then
            Response.AddHeader("Content-Disposition", "attachment; filename=" & Server.UrlPathEncode(Path.GetFileName(full)))
        End If
        Response.TransmitFile(full)
        Response.End()
    End Sub
End Class