<%@ WebHandler Language="VB" Class="DownloadMyChildDoc" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class DownloadMyChildDoc : Implements IHttpHandler, IRequiresSessionState

    Private Const UPLOAD_FOLDER As String = "~/Uploads/ChildRequests/"

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            ' ============================================================
            ' ۱. بررسی Session
            ' ============================================================
            Dim personCode As String = ""
            If context.Session IsNot Nothing AndAlso context.Session("codePersoneli") IsNot Nothing Then
                personCode = context.Session("codePersoneli").ToString().Trim()
            End If

            If String.IsNullOrEmpty(personCode) Then
                WriteError(context, 401, "دسترسی غیرمجاز - لطفاً وارد شوید")
                Return
            End If

            ' ============================================================
            ' ۲. پارامترها
            ' ============================================================
            Dim requestID As Integer = 0
            Integer.TryParse(context.Request.QueryString("id"), requestID)
            If requestID <= 0 Then
                WriteError(context, 400, "شناسه نامعتبر")
                Return
            End If

            Dim docType As String = If(context.Request.QueryString("type"), "").ToString().ToLower().Trim()
            Dim mode As String = If(context.Request.QueryString("mode"), "view").ToString().ToLower().Trim()

            ' ============================================================
            ' ۳. نام ستون بر اساس نوع (شامل olympiad)
            ' ============================================================
            Dim columnName As String = ""
            Select Case docType
                Case "transcript" : columnName = "TranscriptFileName"
                Case "extra" : columnName = "ExtraDocumentFileName"
                Case "birthcert" : columnName = "BirthCertificateFileName"
                Case "nationalcard" : columnName = "NationalCardFileName"
                Case "studentcard" : columnName = "StudentCardFileName"
                Case "olympiad" : columnName = "OlympiadDocFileName"
                Case Else
                    WriteError(context, 400, "نوع سند نامعتبر: " & docType)
                    Return
            End Select

            ' ============================================================
            ' ۴. خواندن نام فایل
            ' ============================================================
            Dim sql As String = "SELECT " & columnName & " FROM exmChildRequests WHERE ID = @ID AND PersonCode = @PersonCode"
            Dim fileName As String = ""

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.AddWithValue("@ID", requestID)
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    cnn.Open()
                    Dim result As Object = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        fileName = result.ToString().Trim()
                    End If
                End Using
            End Using

            If String.IsNullOrEmpty(fileName) Then
                WriteError(context, 404, "فایل یافت نشد (id=" & requestID & ", type=" & docType & ")")
                Return
            End If

            ' ============================================================
            ' ۵. بررسی فایل فیزیکی
            ' ============================================================
            Dim filePath As String = context.Server.MapPath(UPLOAD_FOLDER) & fileName
            If Not File.Exists(filePath) Then
                WriteError(context, 404, "فایل روی سرور موجود نیست: " & fileName)
                Return
            End If

            ' ============================================================
            ' ۶. ارسال فایل
            ' ============================================================
            Dim fInfo As New FileInfo(filePath)
            Dim encodedName As String = Uri.EscapeDataString(Path.GetFileName(fileName))

            context.Response.Clear()
            context.Response.Buffer = True
            context.Response.ContentType = GetContentType(fileName)
            context.Response.AddHeader("Content-Length", fInfo.Length.ToString())
            context.Response.AddHeader("Content-Disposition",
                If(mode = "download", "attachment", "inline") & "; filename*=UTF-8''" & encodedName)

            context.Response.TransmitFile(filePath)
            context.Response.Flush()
            context.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            WriteError(context, 500, "خطا: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ✅ خطای خوانا
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

    Private Function GetContentType(ByVal fileName As String) As String
        Select Case Path.GetExtension(fileName).ToLower()
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