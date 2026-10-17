Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

Partial Class DownloadFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim fileId As String = Request.QueryString("id")

        If String.IsNullOrEmpty(fileId) Then
            lblMessage.Text = "❌ خطا: شناسه فایل یافت نشد"
            Return
        End If

        Try
            Dim sqlstr As String = "SELECT id, content_name, content_file FROM documentsTbl WHERE id = " & fileId
            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim fileName As String = dt.Rows(0).Item("content_name").ToString()
                Dim fileContent As Byte() = DirectCast(dt.Rows(0).Item("content_file"), Byte())

                ' افزایش تعداد دانلود
                Try
                    Dim updateSql As String = "UPDATE documentsTbl SET bazdid = bazdid + 1 WHERE id = " & fileId
                    upStat(updateSql)
                Catch
                End Try

                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & fileName & """")
                Response.BinaryWrite(fileContent)
                Response.Flush()
                Response.End()
            Else
                lblMessage.Text = "⚠️ فایل مورد نظر پیدا نشد."
            End If
        Catch ex As Exception
            lblMessage.Text = "❌ خطا: " & ex.Message
        End Try
    End Sub

    Private Sub upStat(ByVal sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
        Catch
        Finally
            cnn.Close()
        End Try
    End Sub
End Class