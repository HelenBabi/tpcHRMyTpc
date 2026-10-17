Imports System.Data
Imports System.Data.SqlClient

Partial Class TestAttachments
    Inherits System.Web.UI.Page

    Protected Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim courseId As String = txtCourseId.Text.Trim()

        If String.IsNullOrEmpty(courseId) Then
            ShowResult("❌ لطفاً Course ID را وارد کنید", "error")
            Return
        End If

        Try
            Dim result As New StringBuilder()
            result.AppendLine("<h3>🔍 تست با Course ID: " & courseId & "</h3>")

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                cnn.Open()
                result.AppendLine("<p class='success'>✅ اتصال به دیتابیس موفق</p>")

                ' تست 1: شمارش کل رکوردها
                Dim sqlstr As String = "SELECT COUNT(1) FROM documentsTbl WHERE fileID = @CourseId AND content_file IS NOT NULL"
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.AddWithValue("@CourseId", courseId)
                    Dim totalCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    result.AppendLine("<p>📊 تعداد کل رکوردها با fileID = " & courseId & ": <strong>" & totalCount & "</strong></p>")
                End Using

                ' تست 2: شمارش رکوردهای دارای فایل
                Dim fileSql As String = "SELECT COUNT(*) FROM documentsTbl WHERE fileID = @CourseId AND content_file IS NOT NULL"
                Using cmd As New SqlCommand(fileSql, cnn)
                    cmd.Parameters.AddWithValue("@CourseId", courseId)
                    Dim fileCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    result.AppendLine("<p>📎 تعداد رکوردهای دارای فایل: <strong>" & fileCount & "</strong></p>")
                End Using

                ' تست 3: نمایش همه رکوردها
                Dim dataSql As String = "SELECT id, fileID, content_name, size, mdate, sharh, " & _
                                       "CASE WHEN content_file IS NULL THEN 'NULL' ELSE 'دارد' END as HasFile " & _
                                       "FROM documentsTbl WHERE fileID = @CourseId"
                Using cmd As New SqlCommand(dataSql, cnn)
                    cmd.Parameters.AddWithValue("@CourseId", courseId)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        gvData.DataSource = dt
                        gvData.DataBind()
                        divData.Visible = True
                        result.AppendLine("<p class='success'>✅ " & dt.Rows.Count & " رکورد پیدا شد</p>")
                    Else
                        divData.Visible = False
                        result.AppendLine("<p class='error'>❌ هیچ رکوردی با این fileID پیدا نشد</p>")
                    End If
                End Using

            End Using

            ShowResult(result.ToString(), "info")

        Catch ex As Exception
            ShowResult("❌ خطا: " & ex.Message & "<br/>" & ex.StackTrace, "error")
        End Try
    End Sub

    Protected Sub btnTestAll_Click(sender As Object, e As EventArgs) Handles btnTestAll.Click
        Try
            Using cnn As New SqlConnection(ClsConnection.GetConn)
                cnn.Open()

                Dim sql As String = "SELECT DISTINCT fileID, COUNT(*) as Count, " & _
                                   "SUM(CASE WHEN content_file IS NOT NULL THEN 1 ELSE 0 END) as HasFile " & _
                                   "FROM documentsTbl GROUP BY fileID ORDER BY fileID"

                Using cmd As New SqlCommand(sql, cnn)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        gvData.DataSource = dt
                        gvData.DataBind()
                        divData.Visible = True
                        ShowResult("✅ " & dt.Rows.Count & " دوره مختلف در documentsTbl وجود دارد", "success")
                    Else
                        ShowResult("❌ هیچ داده‌ای در documentsTbl وجود ندارد", "error")
                        divData.Visible = False
                    End If
                End Using
            End Using
        Catch ex As Exception
            ShowResult("❌ خطا: " & ex.Message, "error")
        End Try
    End Sub

    Private Sub ShowResult(ByVal message As String, ByVal type As String)
        divResult.InnerHtml = message
        divResult.Attributes("class") = "result " & type
        divResult.Visible = True
    End Sub

End Class