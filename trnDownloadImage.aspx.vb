Imports System.IO

Partial Class trnDownloadImage

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim nat As String = System.IO.Path.GetFileName(Request.QueryString("nat"))
        Dim file As String = System.IO.Path.GetFileName(Request.QueryString("file"))


        Dim path As String = Server.MapPath("~/Attachment/TRN/" & nat & "/" & file)

        If String.IsNullOrEmpty(nat) OrElse String.IsNullOrEmpty(file) Then
            Response.StatusCode = 400
            Response.End()
        End If

        If Not System.IO.File.Exists(path) Then
            Response.StatusCode = 404
            Response.End()
        End If

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & file)
        Response.WriteFile(path)
        Response.End()
    End Sub

End Class
