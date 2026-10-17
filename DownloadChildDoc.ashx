<%@ WebHandler Language="VB" Class="DownloadChildDoc" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class DownloadChildDoc : Implements IHttpHandler, IRequiresSessionState

    Private Const UPLOAD_FOLDER As String = "~/Uploads/ChildRequests/"
    Private Const FALLBACK_CONN As String = "Data Source=.;Initial Catalog=YOUR_DB;Integrated Security=True"

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            context.Response.Clear()
            context.Response.ContentType = "text/plain; charset=utf-8"

            ' ============================================================
            ' ۱. بررسی Session
            ' ============================================================
            If context.Session Is Nothing Then
                context.Response.StatusCode = 401
                context.Response.Write("خطا: Session در دسترس نیست")
                Return
            End If

            Dim personCode As String = ""
            If context.Session("codePersoneli") IsNot Nothing Then
                personCode = context.Session("codePersoneli").ToString().Trim()
            End If

            If String.IsNullOrEmpty(personCode) Then
                context.Response.StatusCode = 401
                context.Response.Write("خطا: کاربر وارد سیستم نشده است")
                Return
            End If

            ' ============================================================
            ' ۲. دریافت پارامترها با بررسی Null
            ' ============================================================
            Dim idStr As String = ""
            If context.Request.QueryString("id") IsNot Nothing Then
                idStr = context.Request.QueryString("id").ToString()
            End If

            Dim requestID As Integer = 0
            If Not Integer.TryParse(idStr, requestID) OrElse requestID = 0 Then
                context.Response.StatusCode = 400
                context.Response.Write("خطا: شناسه نامعتبر است")
                Return
            End If

            Dim docType As String = ""
            If context.Request.QueryString("type") IsNot Nothing Then
                docType = context.Request.QueryString("type").ToString().ToLower().Trim()
            End If

            Dim mode As String = "view"
            If context.Request.QueryString("mode") IsNot Nothing Then
                mode = context.Request.QueryString("mode").ToString().ToLower().Trim()
            End If

            ' ============================================================
            ' ۳. بررسی نقش روابط عمومی
            ' ============================================================
            Dim isPR As Boolean = False
            Try
                isPR = IsPublicRelationsUser(personCode)
            Catch ex As Exception
                ' اگر خطا داد، فقط به عنوان کاربر عادی در نظر بگیر
                isPR = False
            End Try

            ' ============================================================
            ' ۴. خواندن نام فایل از دیتابیس
            ' ============================================================
            Dim fileName As String = GetFileNameFromDb(requestID, personCode, isPR, docType)

            If String.IsNullOrEmpty(fileName) Then
                context.Response.StatusCode = 404
                context.Response.Write("خطا: فایل مورد نظر یافت نشد")
                Return
            End If

            ' ============================================================
            ' ۵. بررسی وجود فایل روی سرور
            ' ============================================================
            Dim folderPath As String = context.Server.MapPath(UPLOAD_FOLDER)
            If folderPath Is Nothing Then
                context.Response.StatusCode = 500
                context.Response.Write("خطا: مسیر پوشه یافت نشد")
                Return
            End If

            Dim filePath As String = Path.Combine(folderPath, fileName)

            If Not File.Exists(filePath) Then
                context.Response.StatusCode = 404
                context.Response.Write("خطا: فایل روی سرور موجود نیست: " & fileName)
                Return
            End If

            ' ============================================================
            ' ۶. ارسال فایل
            ' ============================================================
            context.Response.Clear()
            context.Response.ContentType = GetContentType(fileName)
            context.Response.AddHeader("Content-Disposition",
                If(mode = "download",
                   "attachment; filename=" & Path.GetFileName(fileName),
                   "inline; filename=" & Path.GetFileName(fileName)))
            context.Response.AddHeader("Content-Length", New FileInfo(filePath).Length.ToString())

            context.Response.TransmitFile(filePath)
            context.Response.Flush()
            context.Response.SuppressContent = True
            context.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            ' ✅ لاگ کردن خطا با جزئیات کامل
            Dim errorMsg As String = "خطا در DownloadChildDoc: " & ex.Message

            If ex.InnerException IsNot Nothing Then
                errorMsg &= " | Inner: " & ex.InnerException.Message
            End If

            errorMsg &= " | StackTrace: " & ex.StackTrace

            Try
                Dim logPath As String = context.Server.MapPath("~/App_Data/download_errors.log")
                Dim logDir As String = Path.GetDirectoryName(logPath)
                If Not Directory.Exists(logDir) Then Directory.CreateDirectory(logDir)
                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - " & errorMsg & Environment.NewLine)
            Catch
            End Try

            context.Response.StatusCode = 500
            context.Response.Write("خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ خواندن نام فایل از دیتابیس با بررسی همه جانبه
    ' ============================================================
    Private Function GetFileNameFromDb(ByVal requestID As Integer, ByVal personCode As String,
                                        ByVal isPR As Boolean, ByVal docType As String) As String
        Dim fileName As String = ""

        ' ✅ ستون مورد نظر بر اساس نوع سند
        Dim columnName As String = ""
        Select Case docType
            Case "transcript" : columnName = "TranscriptFileName"
            Case "extra" : columnName = "ExtraDocumentFileName"
            Case "birthcert" : columnName = "BirthCertificateFileName"
            Case "nationalcard" : columnName = "NationalCardFileName"
            Case "studentcard" : columnName = "StudentCardFileName"
            Case Else
                Return ""
        End Select

        ' ✅ ساخت کوئری
        Dim sql As String = "SELECT " & columnName & " FROM exmChildRequests WHERE ID = @ID"
        If Not isPR Then
            sql &= " AND PersonCode = @PersonCode"
        End If

        ' ✅ دریافت Connection String با بررسی خطا
        Dim connStr As String = ""
        Try
            connStr = ClsConnection.GetConn
        Catch ex As Exception
            ' اگر ClsConnection در دسترس نبود، از Connection String جایگزین استفاده کن
            connStr = FALLBACK_CONN
        End Try

        If String.IsNullOrEmpty(connStr) Then
            Throw New Exception("Connection String خالی است")
        End If

        Using cnn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(sql, cnn)
                cmd.Parameters.AddWithValue("@ID", requestID)
                If Not isPR Then
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                End If

                cnn.Open()
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    fileName = result.ToString().Trim()
                End If
            End Using
        End Using

        Return fileName
    End Function

    ' ============================================================
    ' ✅ بررسی نقش روابط عمومی
    ' ============================================================
    Private Function IsPublicRelationsUser(ByVal personCode As String) As Boolean
        Try
            Dim connStr As String = ""
            Try
                connStr = ClsConnection.GetConn
            Catch
                connStr = FALLBACK_CONN
            End Try

            If String.IsNullOrEmpty(connStr) Then Return False

            Dim sql As String =
                "SELECT COUNT(*) FROM personel p " &
                "INNER JOIN omurTbl o ON p.omurID = o.omurID " &
                "WHERE p.codePersoneli = @Code AND (o.omur LIKE N'%روابط عمومی%' OR o.omur LIKE N'%روابط عمومي%')"

            Using cnn As New SqlConnection(connStr)
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.AddWithValue("@Code", personCode)
                    cnn.Open()
                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return Convert.ToInt32(result) > 0
                    End If
                End Using
            End Using

            Return False
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' ✅ تعیین Content-Type
    ' ============================================================
    Private Function GetContentType(ByVal fileName As String) As String
        If String.IsNullOrEmpty(fileName) Then Return "application/octet-stream"

        Dim ext As String = Path.GetExtension(fileName)
        If String.IsNullOrEmpty(ext) Then Return "application/octet-stream"

        Select Case ext.ToLower()
            Case ".jpg", ".jpeg" : Return "image/jpeg"
            Case ".png" : Return "image/png"
            Case ".gif" : Return "image/gif"
            Case ".pdf" : Return "application/pdf"
            Case Else : Return "application/octet-stream"
        End Select
    End Function

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class