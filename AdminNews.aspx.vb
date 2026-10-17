Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Partial Class AdminNews
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadNews()
        End If
    End Sub

    ' ============================================================
    ' متد کمکی برای اجرای کوئری با پارامتر
    ' ============================================================
    Private Function GetDataTable(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Try
            Dim paramDict As New Dictionary(Of String, Object)()
            For Each p As SqlParameter In parameters
                paramDict.Add(p.ParameterName, p.Value)
            Next
            Return ClsDataTable.GetDataTable(sql, paramDict)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' ============================================================
    ' خلاصه کردن متن (برای جدول)
    ' ============================================================
    Protected Function GetShortText(ByVal text As Object) As String
        Try
            If text Is Nothing OrElse text Is DBNull.Value Then
                Return ""
            End If

            Dim fullText As String = text.ToString()

            ' ====== حذف تگ‌های HTML ======
            fullText = Regex.Replace(fullText, "<[^>]+>", " ")

            ' ====== حذف فاصله‌های اضافی ======
            fullText = Regex.Replace(fullText, "\s+", " ").Trim()

            ' ====== محدود کردن طول ======
            If fullText.Length > 100 Then
                Return fullText.Substring(0, 100) & "..."
            End If

            Return fullText
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' بارگذاری لیست اطلاعیه‌ها
    ' ============================================================
    Private Sub LoadNews()
        Try
            Dim sql As String = "SELECT ID, NewsTitle, NewsText, ISNULL(NewsIcon, N'📢') AS NewsIcon, " &
                "IsActive, SortOrder, " &
                "dbo.FormatDateFa(CreatedDate) AS PersianDate " &
                "FROM SystemNews " &
                "ORDER BY SortOrder ASC, CreatedDate DESC"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            gvNews.DataSource = dt
            gvNews.DataBind()

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ذخیره اطلاعیه (افزودن یا ویرایش)
    ' ============================================================
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim title As String = txtTitle.Text.Trim()
            Dim text As String = txtText.Text.Trim()
            Dim icon As String = txtIcon.Text.Trim()
            Dim sortOrder As Integer = 0
            Dim isActive As Boolean = chkIsActive.Checked
            Dim editID As Integer = Convert.ToInt32(hfEditID.Value)

            ' ====== اعتبارسنجی ======
            If String.IsNullOrEmpty(title) Then
                ShowMessage("عنوان اطلاعیه را وارد کنید", "danger")
                Return
            End If

            If String.IsNullOrEmpty(text) Then
                ShowMessage("متن اطلاعیه را وارد کنید", "danger")
                Return
            End If

            ' ====== آیکون پیش‌فرض ======
            If String.IsNullOrEmpty(icon) Then
                icon = "📢"
            End If

            ' ====== ترتیب ======
            Integer.TryParse(txtSortOrder.Text.Trim(), sortOrder)

            Using conn As New SqlConnection(ClsConnection.GetConn)
                conn.Open()

                If editID = 0 Then
                    ' ====== درج جدید ======
                    Dim sql As String = "INSERT INTO SystemNews (NewsTitle, NewsText, NewsIcon, IsActive, SortOrder, CreatedDate, CreatedBy) " &
                        "VALUES (@Title, @Text, @Icon, @IsActive, @SortOrder, GETDATE(), @CreatedBy)"

                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Title", title)
                        cmd.Parameters.AddWithValue("@Text", text)
                        cmd.Parameters.AddWithValue("@Icon", icon)
                        cmd.Parameters.AddWithValue("@IsActive", isActive)
                        cmd.Parameters.AddWithValue("@SortOrder", sortOrder)
                        cmd.Parameters.AddWithValue("@CreatedBy", If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), "admin"))
                        cmd.ExecuteNonQuery()
                    End Using

                    ShowMessage("اطلاعیه با موفقیت اضافه شد", "success")

                Else
                    ' ====== ویرایش ======
                    Dim sql As String = "UPDATE SystemNews SET " &
                        "NewsTitle = @Title, " &
                        "NewsText = @Text, " &
                        "NewsIcon = @Icon, " &
                        "IsActive = @IsActive, " &
                        "SortOrder = @SortOrder, " &
                        "UpdatedDate = GETDATE() " &
                        "WHERE ID = @ID"

                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Title", title)
                        cmd.Parameters.AddWithValue("@Text", text)
                        cmd.Parameters.AddWithValue("@Icon", icon)
                        cmd.Parameters.AddWithValue("@IsActive", isActive)
                        cmd.Parameters.AddWithValue("@SortOrder", sortOrder)
                        cmd.Parameters.AddWithValue("@ID", editID)
                        cmd.ExecuteNonQuery()
                    End Using

                    ShowMessage("اطلاعیه با موفقیت ویرایش شد", "success")
                End If
            End Using

            ClearForm()
            LoadNews()

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' دستورات GridView
    ' ============================================================
    Protected Sub gvNews_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)

            If e.CommandName = "EditNews" Then
                LoadForEdit(id)
            ElseIf e.CommandName = "ToggleNews" Then
                ToggleActive(id)
            ElseIf e.CommandName = "DeleteNews" Then
                DeleteNews(id)
            End If
        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' بارگذاری برای ویرایش
    ' ============================================================
    Private Sub LoadForEdit(id As Integer)
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Dim sql As String = "SELECT * FROM SystemNews WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            hfEditID.Value = id.ToString()
                            txtTitle.Text = reader("NewsTitle").ToString()
                            txtText.Text = reader("NewsText").ToString()
                            txtIcon.Text = reader("NewsIcon").ToString()

                            If reader("SortOrder") IsNot DBNull.Value Then
                                txtSortOrder.Text = reader("SortOrder").ToString()
                            Else
                                txtSortOrder.Text = "0"
                            End If

                            chkIsActive.Checked = Convert.ToBoolean(reader("IsActive"))

                            lblFormTitle.Text = "✏️ ویرایش اطلاعیه"
                        End If
                    End Using
                End Using
            End Using

            ShowMessage("در حالت ویرایش - تغییرات را اعمال کنید", "warning")

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' فعال/غیرفعال کردن اطلاعیه
    ' ============================================================
    Private Sub ToggleActive(id As Integer)
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Dim sql As String = "UPDATE SystemNews SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END, UpdatedDate = GETDATE() WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowMessage("وضعیت با موفقیت تغییر کرد", "success")
            LoadNews()

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' حذف اطلاعیه
    ' ============================================================
    Private Sub DeleteNews(id As Integer)
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Dim sql As String = "DELETE FROM SystemNews WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ShowMessage("اطلاعیه با موفقیت حذف شد", "success")
            LoadNews()

        Catch ex As Exception
            ShowMessage("خطا در حذف: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' انصراف
    ' ============================================================
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
    End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    ' ============================================================
    ' پاکسازی فرم
    ' ============================================================
    Private Sub ClearForm()
        txtTitle.Text = ""
        txtText.Text = ""
        txtIcon.Text = ""
        txtSortOrder.Text = "0"
        chkIsActive.Checked = True
        hfEditID.Value = "0"
        lblFormTitle.Text = "➕ افزودن اطلاعیه جدید"
        pnlMessage.Visible = False
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(msg As String, type As String)
        pnlMessage.Visible = True
        lblMessage.Text = msg
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅ "
                divMessage.Attributes("class") = "alert alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌ "
                divMessage.Attributes("class") = "alert alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️ "
                divMessage.Attributes("class") = "alert alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️ "
                divMessage.Attributes("class") = "alert alert-info"
        End Select
    End Sub

End Class