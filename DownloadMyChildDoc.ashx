<%@ WebHandler Language="VB" Class="DownloadMyChildDoc" %>

Imports System
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class DownloadMyChildDoc : Implements IHttpHandler

    Private Const UPLOAD_FOLDER As String = "~/Uploads/ChildRequests/"

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim personCode As String = ""
            If context.Session("codePersoneli") IsNot Nothing Then
                personCode = context.Session("codePersoneli").ToString().Trim()
            End If

            If String.IsNullOrEmpty(personCode) Then
                context.Response.StatusCode = 401
                context.Response.Write("دسترسی غیرمجاز")
                Return
            End If

            Dim requestID As Integer = 0
            Integer.TryParse(context.Request.QueryString("id"), requestID)
            If requestID = 0 Then
                context.Response.StatusCode = 400
                context.Response.Write("شناسه نامعتبر")
                Return
            End If

            Dim docType As String = If(context.Request.QueryString("type"), "").ToLower().Trim()
            Dim mode As String = If(context.Request.QueryString("mode"), "view").ToLower().Trim()

            Dim sql As String =
                "SELECT TranscriptFileName, ExtraDocumentFileName, " &
                "       BirthCertificateFileName, NationalCardFileName, StudentCardFileName " &
                "FROM exmChildRequests " &
                "WHERE ID = @ID AND PersonCode = @PersonCode"

            Dim fileName As String = ""

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.AddWithValue("@ID", requestID)
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    cnn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Select Case docType
                                Case "transcript"
                                    If Not IsDBNull(reader("TranscriptFileName")) Then fileName = reader("TranscriptFileName").ToString()
                                Case "extra"
                                    If Not IsDBNull(reader("ExtraDocumentFileName")) Then fileName = reader("ExtraDocumentFileName").ToString()
                                Case "birthcert"
                                    If Not IsDBNull(reader("BirthCertificateFileName")) Then fileName = reader("BirthCertificateFileName").ToString()
                                Case "nationalcard"
                                    If Not IsDBNull(reader("NationalCardFileName")) Then fileName = reader("NationalCardFileName").ToString()
                                Case "studentcard"
                                    If Not IsDBNull(reader("StudentCardFileName")) Then fileName = reader("StudentCardFileName").ToString()
                            End Select
                        End If
                    End Using
                End Using
            End Using

            If String.IsNullOrEmpty(fileName) Then
                context.Response.StatusCode = 404
                context.Response.Write("فایل یافت نشد")
                Return
            End If

            Dim filePath As String = context.Server.MapPath(UPLOAD_FOLDER & fileName)

            If Not File.Exists(filePath) Then
                context.Response.StatusCode = 404
                context.Response.Write("فایل روی سرور موجود نیست")
                Return
            End If

            context.Response.Clear()
            context.Response.ContentType = GetContentType(fileName)
            context.Response.AddHeader("Content-Disposition",
                If(mode = "download", "attachment; filename=" & Path.GetFileName(fileName), "inline; filename=" & Path.GetFileName(fileName)))

            context.Response.TransmitFile(filePath)
            context.Response.End()

        Catch ex As Exception
            context.Response.StatusCode = 500
            context.Response.Write("خطا: " & ex.Message)
        End Try
    End Sub

    Private Function GetContentType(ByVal fileName As String) As String
        Dim ext As String = Path.GetExtension(fileName).ToLower()
        Select Case ext
            Case ".jpg", ".jpeg" : Return "image/jpeg"
            Case ".png" : Return "image/png"
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