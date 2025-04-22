<%@ WebHandler Language="VB" Class="FileVB" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration

Public Class FileVB : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As Integer = Integer.Parse(context.Request.QueryString("id"))
        Dim bytes As Byte()
        Dim contentType As String
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim name As String
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT content_name, content_file, docType FROM documentsTbl WHERE Id=@Id"
                cmd.Parameters.AddWithValue("@Id", id)
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    bytes = DirectCast(sdr("Data"), Byte())
                    contentType = sdr("ContentType").ToString()
                    name = sdr("Name").ToString()
                End Using
                con.Close()
            End Using
        End Using
        context.Response.Clear()
        context.Response.Buffer = True
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name)
        context.Response.ContentType = contentType
        context.Response.BinaryWrite(bytes)
        context.Response.End()
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class