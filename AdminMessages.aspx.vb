Imports System.Data
Imports System.Data.SqlClient

Partial Class AdminMessages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadMessages()
        End If
    End Sub

    ' ============================================================
    ' بارگذاری لیست پیغام‌ها
    ' ============================================================
    Private Sub LoadMessages()
        Try
            Dim sql As String = "SELECT ID, MessageTitle, MessageText, IsActive, CreatedDate FROM SystemMessages ORDER BY CreatedDate DESC"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            gvMessages.DataSource = dt
            gvMessages.DataBind()
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ذخیره پیغام (افزودن یا ویرایش)
    ' ============================================================
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim title As String = txtTitle.Text.Trim()
            Dim message As String = txtMessage.Text.Trim()
            Dim isActive As Boolean = chkIsActive.Checked
            Dim editID As Integer = Convert.ToInt32(hfEditID.Value)

            If String.IsNullOrEmpty(message) Then
                ShowMessage("متن پیغام را وارد کنید", "danger")
                Return
            End If

            Using conn As New SqlConnection(ClsConnection.GetConn)
                conn.Open()

                If editID = 0 Then
                    ' درج پیغام جدید
                    Dim sql As String = "INSERT INTO SystemMessages (MessageTitle, MessageText, IsActive, CreatedDate, CreatedBy) VALUES (@Title, @Text, @IsActive, GETDATE(), @CreatedBy)"
                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Title", If(String.IsNullOrEmpty(title), "پیغام مدیر سیستم", title))
                        cmd.Parameters.AddWithValue("@Text", message)
                        cmd.Parameters.AddWithValue("@IsActive", isActive)
                        cmd.Parameters.AddWithValue("@CreatedBy", Session("FullName"))
                        cmd.ExecuteNonQuery()
                    End Using
                    ShowMessage("پیغام با موفقیت اضافه شد", "success")
                Else
                    ' ویرایش پیغام
                    Dim sql As String = "UPDATE SystemMessages SET MessageTitle = @Title, MessageText = @Text, IsActive = @IsActive, UpdatedDate = GETDATE() WHERE ID = @ID"
                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Title", If(String.IsNullOrEmpty(title), "پیغام مدیر سیستم", title))
                        cmd.Parameters.AddWithValue("@Text", message)
                        cmd.Parameters.AddWithValue("@IsActive", isActive)
                        cmd.Parameters.AddWithValue("@ID", editID)
                        cmd.ExecuteNonQuery()
                    End Using
                    ShowMessage("پیغام با موفقیت ویرایش شد", "success")
                End If
            End Using

            ClearForm()
            LoadMessages()
        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' عملیات روی پیغام‌ها
    ' ============================================================
    Protected Sub gvMessages_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)

            If e.CommandName = "EditMsg" Then
                LoadForEdit(id)
            ElseIf e.CommandName = "ToggleMsg" Then
                ToggleActive(id)
            ElseIf e.CommandName = "DeleteMsg" Then
                DeleteMessage(id)
            End If
        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadForEdit(id As Integer)
        Using conn As New SqlConnection(ClsConnection.GetConn)
            Dim sql As String = "SELECT * FROM SystemMessages WHERE ID = @ID"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ID", id)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        hfEditID.Value = id.ToString()
                        txtTitle.Text = reader("MessageTitle").ToString()
                        txtMessage.Text = reader("MessageText").ToString()
                        chkIsActive.Checked = Convert.ToBoolean(reader("IsActive"))
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub ToggleActive(id As Integer)
        Using conn As New SqlConnection(ClsConnection.GetConn)
            Dim sql As String = "UPDATE SystemMessages SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END, UpdatedDate = GETDATE() WHERE ID = @ID"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ID", id)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
        ShowMessage("وضعیت پیغام تغییر کرد", "success")
        LoadMessages()
    End Sub

    Private Sub DeleteMessage(id As Integer)
        Using conn As New SqlConnection(ClsConnection.GetConn)
            Dim sql As String = "DELETE FROM SystemMessages WHERE ID = @ID"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ID", id)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
        ShowMessage("پیغام حذف شد", "success")
        LoadMessages()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
        Response.Redirect("index.aspx")
    End Sub

    Private Sub ClearForm()
        txtTitle.Text = ""
        txtMessage.Text = ""
        chkIsActive.Checked = True
        hfEditID.Value = "0"
    End Sub

    Private Sub ShowMessage(msg As String, type As String)
        pnlMessage.Visible = True
        lblMessage.Text = msg
        divMessage.Attributes("class") = "alert alert-" & type
    End Sub

End Class