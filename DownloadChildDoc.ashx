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
            ' ============================================================
            ' ۱. بررسی Session
            ' ============================================================
            If context.Session Is Nothing Then
                WriteError(context, 401, "Session در دسترس نیست")
                Return
            End If

            Dim personCode As String = ""
            If context.Session("codePersoneli") IsNot Nothing Then
                personCode = context.Session("codePersoneli").ToString().Trim()
            End If

            If String.IsNullOrEmpty(personCode) Then
                WriteError(context, 401, "کاربر وارد سیستم نشده است")
                Return
            End If

            ' ============================================================
            ' ۲. پارامترها
            ' ============================================================
            Dim idStr As String = If(context.Request.QueryString("id"), "").ToString()
            Dim requestID As Integer = 0
            If Not Integer.TryParse(idStr, requestID) OrElse requestID <= 0 Then
                WriteError(context, 400, "شناسه نامعتبر است")
                Return
            End If

            Dim docType As String = If(context.Request.QueryString("type"), "").ToString().ToLower().Trim()
            Dim mode As String = If(context.Request.QueryString("mode"), "view").ToString().ToLower().Trim()

            ' ============================================================
            ' ۳. نقش روابط عمومی
            ' ============================================================
            Dim isPR As Boolean = IsPublicRelationsUser(personCode)

            ' ============================================================
            ' ۴. نام فایل از دیتابیس
            ' ============================================================
            Dim fileName As String = GetFileNameFromDb(requestID, personCode, isPR, docType)

            If String.IsNullOrEmpty(fileName) Then
                WriteError(context, 404, "فایل مورد نظر در دیتابیس یافت نشد (type=" & docType & ")")
                Return
            End If

            ' ============================================================
            ' ۵. بررسی فایل فیزیکی
            ' ============================================================
            Dim folderPath As String = context.Server.MapPath(UPLOAD_FOLDER)
            Dim filePath As String = Path.Combine(folderPath, fileName)

            If Not File.Exists(filePath) Then
                WriteError(context, 404, "فایل روی سرور موجود نیست: " & fileName)
                Return
            End If

            ' ============================================================
            ' ۶. ارسال فایل (بدون SuppressContent)
            ' ============================================================
            Dim fInfo As New FileInfo(filePath)

            context.Response.Clear()
            context.Response.Buffer = True
            context.Response.ContentType = GetContentType(fileName)
            context.Response.AddHeader("Content-Length", fInfo.Length.ToString())

            Dim encodedName As String = Uri.EscapeDataString(Path.GetFileName(fileName))
            Dim dispType As String = If(mode = "download", "attachment", "inline")
            context.Response.AddHeader("Content-Disposition",
                dispType & "; filename=""" & Path.GetFileName(fileName).Replace("""", "") & """; filename*=UTF-8''" & encodedName)

            context.Response.TransmitFile(filePath)
            context.Response.Flush()
            context.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            LogError(context, ex)
            WriteError(context, 500, ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ خواندن نام فایل از دیتابیس — با پشتیبانی از همه انواع
    ' ============================================================
    Private Function GetFileNameFromDb(ByVal requestID As Integer, ByVal personCode As String,
                                        ByVal isPR As Boolean, ByVal docType As String) As String
        Dim columnName As String = ""
        Select Case docType
            Case "transcript" : columnName = "TranscriptFileName"
            Case "extra" : columnName = "ExtraDocumentFileName"
            Case "birthcert" : columnName = "BirthCertificateFileName"
            Case "nationalcard" : columnName = "NationalCardFileName"
            Case "studentcard" : columnName = "StudentCardFileName"
            Case "olympiad" : columnName = "OlympiadDocFileName"     ' ✅ اضافه شد
            Case Else
                Return ""
        End Select

        Dim sql As String = "SELECT " & columnName & " FROM exmChildRequests WHERE ID = @ID"
        If Not isPR Then
            sql &= " AND PersonCode = @PersonCode"
        End If

        Dim connStr As String = GetConnStr()
        If String.IsNullOrEmpty(connStr) Then Throw New Exception("Connection String خالی است")

        Using cnn As New SqlConnection(connStr)
            Using cmd As New SqlCommand(sql, cnn)
                cmd.Parameters.AddWithValue("@ID", requestID)
                If Not isPR Then cmd.Parameters.AddWithValue("@PersonCode", personCode)

                cnn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString().Trim()
                End If
            End Using
        End Using

        Return ""
    End Function

    ' ============================================================
    ' ✅ بررسی نقش روابط عمومی
    ' ============================================================
    Private Function IsPublicRelationsUser(ByVal personCode As String) As Boolean
        Try
            Dim connStr As String = GetConnStr()
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
                    Return result IsNot Nothing AndAlso Not IsDBNull(result) AndAlso Convert.ToInt32(result) > 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ============================================================
    ' ✅ Connection String
    ' ============================================================
    Private Function GetConnStr() As String
        Try
            Return ClsConnection.GetConn
        Catch
            Return FALLBACK_CONN
        End Try
    End Function

    ' ============================================================
    ' ✅ Content-Type
    ' ============================================================
    Private Function GetContentType(ByVal fileName As String) As String
        If String.IsNullOrEmpty(fileName) Then Return "application/octet-stream"
        Select Case Path.GetExtension(fileName).ToLower()
            Case ".jpg", ".jpeg" : Return "image/jpeg"
            Case ".png" : Return "image/png"
            Case ".gif" : Return "image/gif"
            Case ".bmp" : Return "image/bmp"
            Case ".pdf" : Return "application/pdf"
            Case Else : Return "application/octet-stream"
        End Select
    End Function

    ' ============================================================
    ' ✅ خطا و لاگ
    ' ============================================================
    Private Sub WriteError(ByVal context As HttpContext, ByVal code As Integer, ByVal message As String)
        Try
            context.Response.Clear()
            context.Response.StatusCode = code
            context.Response.ContentType = "text/plain; charset=utf-8"
            context.Response.Write(message)
        Catch
        End Try
    End Sub

    Private Sub LogError(ByVal context As HttpContext, ByVal ex As Exception)
        Try
            Dim logPath As String = context.Server.MapPath("~/App_Data/download_errors.log")
            Dim logDir As String = Path.GetDirectoryName(logPath)
            If Not Directory.Exists(logDir) Then Directory.CreateDirectory(logDir)
            Dim msg As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - " & ex.Message
            If ex.InnerException IsNot Nothing Then msg &= " | Inner: " & ex.InnerException.Message
            msg &= " | " & ex.StackTrace
            File.AppendAllText(logPath, msg & Environment.NewLine)
        Catch
        End Try
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class