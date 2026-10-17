Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports CrystalDecisions.Web
Imports System.IO

Partial Class herasatCards
    Inherits System.Web.UI.Page

    Private Const PrintedListKey As String = "PrintedCardList"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SelectedPersonnel = New List(Of String)()
            ' LoadUnits()  ← دیگر نیازی نیست
            LoadPersonnelData()
        Else
            SaveSelectedCodes()
        End If
    End Sub

    ' ============================================================
    ' بارگذاری واحدها
    ' ============================================================
    

    ' ============================================================
    ' بارگذاری داده‌های پرسنل
    ' ============================================================
    Private Sub LoadPersonnelData()
        Try
            Dim sql As String = "SELECT ID, codePersoneli, Nationality, fulname, shift, omur, tarixStart, expireDateCard, PhotoPath FROM Personnel_Management WHERE IsActive = 1"

            If Not String.IsNullOrEmpty(txtSearch.Text.Trim()) Then
                Dim keyword As String = txtSearch.Text.Trim().Replace("'", "''")
                sql &= " AND (fulname LIKE N'%" & keyword & "%' OR codePersoneli LIKE N'%" & keyword & "%' OR Nationality LIKE N'%" & keyword & "%')"
            End If

            sql &= " ORDER BY ID DESC"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            ' اضافه کردن شماره ردیف
            dt.Columns.Add("RowNumber", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1
            Next

            gridPersonnel.DataSource = dt
            gridPersonnel.DataBind()

            lblTotalCount.Text = "تعداد کل: " & dt.Rows.Count.ToString() & " نفر"
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری داده‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(message As String, type As String)
        pnlMessage.Visible = True
        lblMessage.Text = message

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

    ' ============================================================
    ' دریافت مقدار از دیتابیس (برای بررسی تکراری)
    ' ============================================================
    Private Function GetScalarValue(sql As String, ParamArray parameters() As SqlParameter) As Object
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, conn)
                    If parameters IsNot Nothing Then
                        For Each p As SqlParameter In parameters
                            cmd.Parameters.Add(p)
                        Next
                    End If
                    conn.Open()
                    Return cmd.ExecuteScalar()
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' ============================================================
    ' اجرای دستور غیرفعال (INSERT, UPDATE, DELETE)
    ' ============================================================
    Private Function ExecuteNonQuery(sql As String, ParamArray parameters() As SqlParameter) As Integer
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, conn)
                    If parameters IsNot Nothing Then
                        For Each p As SqlParameter In parameters
                            cmd.Parameters.Add(p)
                        Next
                    End If
                    conn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' ============================================================
    ' دریافت DataTable با پارامتر
    ' ============================================================
    Private Function GetDataTableWithParams(sql As String, ParamArray parameters() As SqlParameter) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sql, conn)
                    If parameters IsNot Nothing Then
                        For Each p As SqlParameter In parameters
                            cmd.Parameters.Add(p)
                        Next
                    End If
                    conn.Open()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    ' ============================================================
    ' ذخیره پرسنل جدید یا ویرایش
    ' ============================================================
    ' ============================================================
    ' ذخیره پرسنل جدید یا ویرایش (با پشتیبانی از BMP)
    ' ============================================================
    Protected Sub btnSavePersonnel_Click(sender As Object, e As EventArgs) Handles btnSavePersonnel.Click
        Try
            If Not Page.IsValid Then Exit Sub

            Dim editID As Integer = Convert.ToInt32(hfEditID.Value)
            Dim codePersoneli As String = txtCodePersoneli.Text.Trim()
            Dim nationality As String = txtNationality.Text.Trim()
            Dim fulname As String = txtFullName.Text.Trim()
            Dim shift As String = ddlShift.SelectedValue
            Dim omurName As String = txtOmur.Text.Trim()
            Dim tarixStart As String = txtTarixStart.Text.Trim()
            Dim expireDate As String = txtExpireDate.Text.Trim()

            ' بررسی کد ملی تکراری
            Dim checkSql As String = "SELECT COUNT(*) FROM Personnel_Management WHERE Nationality = @Nationality AND IsActive = 1 AND ID != @EditID"
            Dim result As Object = GetScalarValue(checkSql,
                New SqlParameter("@Nationality", nationality),
                New SqlParameter("@EditID", editID))

            If result IsNot Nothing AndAlso Convert.ToInt32(result) > 0 Then
                ShowMessage("کد ملی قبلاً ثبت شده است!", "danger")
                Return
            End If

            ' پردازش تصویر
            Dim photoPath As String = ""
            If fileUpload.HasFile Then
                ' اضافه کردن BMP به فرمت‌های مجاز
                Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp"}
                Dim extension As String = Path.GetExtension(fileUpload.FileName).ToLower()

                If Not allowedExtensions.Contains(extension) Then
                    ShowMessage("فرمت تصویر مجاز نیست (فقط JPG, PNG, GIF, BMP)", "danger")
                    Return
                End If

                If fileUpload.PostedFile.ContentLength > 2 * 1024 * 1024 Then
                    ShowMessage("حجم تصویر نباید بیشتر از 2 مگابایت باشد", "danger")
                    Return
                End If

                ' ذخیره تصویر با نام کد ملی در پوشه Herasat
                Dim folderPath As String = Server.MapPath("~/Herasat/")

                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                ' تبدیل فایل به JPG برای ذخیره (اختیاری)
                ' یا ذخیره با فرمت اصلی
                Dim fileName As String = nationality & ".jpg"  ' ذخیره به صورت JPG
                ' اگر می‌خواهید با فرمت اصلی ذخیره شود:
                ' Dim fileName As String = nationality & extension

                Dim savePath As String = Path.Combine(folderPath, fileName)

                If File.Exists(savePath) Then
                    File.Delete(savePath)
                End If

                ' ذخیره فایل
                fileUpload.SaveAs(savePath)

                ' اگر فایل BMP است، آن را به JPG تبدیل کنید (اختیاری)
                If extension = ".bmp" Then
                    Try
                        Using bmp As New System.Drawing.Bitmap(savePath)
                            Dim jpgPath As String = Path.Combine(folderPath, nationality & ".jpg")
                            bmp.Save(jpgPath, System.Drawing.Imaging.ImageFormat.Jpeg)
                            File.Delete(savePath)  ' حذف فایل BMP اصلی
                        End Using
                    Catch
                        ' اگر تبدیل ناموفق بود، همان BMP باقی می‌ماند
                    End Try
                End If

                photoPath = "~/Herasat/" & fileName
            ElseIf editID = 0 Then
                ShowMessage("لطفاً تصویر را بارگذاری کنید", "danger")
                Return
            Else
                Dim oldSql As String = "SELECT PhotoPath FROM Personnel_Management WHERE ID = @ID"
                Dim dtOld As DataTable = GetDataTableWithParams(oldSql, New SqlParameter("@ID", editID))
                If dtOld.Rows.Count > 0 Then
                    photoPath = dtOld.Rows(0)("PhotoPath").ToString()
                End If
            End If

            ' ذخیره در دیتابیس
            If editID = 0 Then
                Dim insertSql As String = "INSERT INTO Personnel_Management (codePersoneli, Nationality, fulname, shift, omur, tarixStart, expireDateCard, PhotoPath, CreatedDate, UpdatedDate, IsActive) VALUES (@codePersoneli, @Nationality, @fulname, @shift, @omur, @tarixStart, @expireDate, @PhotoPath, GETDATE(), GETDATE(), 1)"

                ExecuteNonQuery(insertSql,
                    New SqlParameter("@codePersoneli", codePersoneli),
                    New SqlParameter("@Nationality", nationality),
                    New SqlParameter("@fulname", fulname),
                    New SqlParameter("@shift", shift),
                    New SqlParameter("@omur", omurName),
                    New SqlParameter("@tarixStart", tarixStart),
                    New SqlParameter("@expireDate", expireDate),
                    New SqlParameter("@PhotoPath", photoPath))

                ShowMessage("پرسنل با موفقیت اضافه شد", "success")
            Else
                Dim updateSql As String = "UPDATE Personnel_Management SET codePersoneli = @codePersoneli, Nationality = @Nationality, fulname = @fulname, shift = @shift, omur = @omur, tarixStart = @tarixStart, expireDateCard = @expireDate, PhotoPath = @PhotoPath, UpdatedDate = GETDATE() WHERE ID = @ID"

                ExecuteNonQuery(updateSql,
                    New SqlParameter("@codePersoneli", codePersoneli),
                    New SqlParameter("@Nationality", nationality),
                    New SqlParameter("@fulname", fulname),
                    New SqlParameter("@shift", shift),
                    New SqlParameter("@omur", omurName),
                    New SqlParameter("@tarixStart", tarixStart),
                    New SqlParameter("@expireDate", expireDate),
                    New SqlParameter("@PhotoPath", photoPath),
                    New SqlParameter("@ID", editID))

                ShowMessage("پرسنل با موفقیت ویرایش شد", "success")
            End If

            ' پاکسازی فرم
            ClearForm()
            hfEditID.Value = "0"
            btnSavePersonnel.Text = "✅ ذخیره"
            formTitle.InnerText = "➕ افزودن پرسنل جدید"

            LoadPersonnelData()

        Catch ex As Exception
            ShowMessage("خطا: " & ex.Message, "danger")
        End Try
    End Sub


    ' ============================================================
    ' ویرایش پرسنل
    ' ============================================================
    Protected Sub gridPersonnel_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "EditPersonnel" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            LoadPersonnelForEdit(id)
        ElseIf e.CommandName = "DeletePersonnel" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            DeletePersonnel(id)
        End If
    End Sub

    Private Sub LoadPersonnelForEdit(id As Integer)
        Try
            Dim sql As String = "SELECT * FROM Personnel_Management WHERE ID = @ID"
            Dim dt As DataTable = GetDataTableWithParams(sql, New SqlParameter("@ID", id))

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                hfEditID.Value = id.ToString()
                txtCodePersoneli.Text = row("codePersoneli").ToString()
                txtNationality.Text = row("Nationality").ToString()
                txtFullName.Text = row("fulname").ToString()

                If ddlShift.Items.FindByValue(row("shift").ToString()) IsNot Nothing Then
                    ddlShift.SelectedValue = row("shift").ToString()
                End If

                ' تنظیم واحد سازمانی از TextBox
                txtOmur.Text = row("omur").ToString()

                txtTarixStart.Text = row("tarixStart").ToString()
                txtExpireDate.Text = row("expireDateCard").ToString()

                btnSavePersonnel.Text = "✏️ بروزرسانی"
                formTitle.InnerText = "✏️ ویرایش پرسنل"

                ShowMessage("در حالت ویرایش - تغییرات را اعمال کنید", "info")

                Dim photoPath As String = row("PhotoPath").ToString()
                If Not String.IsNullOrEmpty(photoPath) Then
                    Dim script As String = "var preview = document.createElement('div'); preview.id='photoPreview'; preview.className='photo-preview'; var img=document.createElement('img'); img.src='" & photoPath & "'; img.style.maxWidth='100%'; img.style.maxHeight='100%'; preview.appendChild(img); document.getElementById('" & fileUpload.ClientID & "').parentNode.appendChild(preview);"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPhoto", script, True)
                End If
            End If
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' حذف کامل پرسنل (به همراه فایل تصویر)
    ' ============================================================
    Private Sub DeletePersonnel(id As Integer)
        Try
            ' ابتدا اطلاعات پرسنل را دریافت کنیم تا مسیر عکس را داشته باشیم
            Dim selectSql As String = "SELECT PhotoPath, Nationality FROM Personnel_Management WHERE ID = @ID"
            Dim dt As DataTable = GetDataTableWithParams(selectSql, New SqlParameter("@ID", id))

            If dt.Rows.Count > 0 Then
                Dim photoPath As String = dt.Rows(0)("PhotoPath").ToString()
                Dim nationality As String = dt.Rows(0)("Nationality").ToString()

                ' حذف رکورد از دیتابیس (حذف کامل، نه غیرفعال کردن)
                Dim deleteSql As String = "DELETE FROM Personnel_Management WHERE ID = @ID"
                ExecuteNonQuery(deleteSql, New SqlParameter("@ID", id))

                ' حذف فایل تصویر از پوشه Herasat
                If Not String.IsNullOrEmpty(photoPath) Then
                    Try
                        Dim physicalPath As String = Server.MapPath(photoPath)
                        If File.Exists(physicalPath) Then
                            File.Delete(physicalPath)
                        End If
                    Catch ex As Exception
                        ' اگر فایل حذف نشد، فقط لاگ کن
                        System.Diagnostics.Debug.WriteLine("خطا در حذف فایل: " & ex.Message)
                    End Try
                End If

                ' همچنین فایل با نام کد ملی را هم بررسی و حذف کن (در صورتی که با نام دیگر ذخیره شده باشد)
                If Not String.IsNullOrEmpty(nationality) Then
                    Try
                        Dim folderPath As String = Server.MapPath("~/Herasat/")
                        Dim filePath As String = Path.Combine(folderPath, nationality & ".jpg")
                        If File.Exists(filePath) Then
                            File.Delete(filePath)
                        End If
                    Catch ex As Exception
                        System.Diagnostics.Debug.WriteLine("خطا در حذف فایل با نام کد ملی: " & ex.Message)
                    End Try
                End If

                ShowMessage("پرسنل با موفقیت حذف شد", "success")
            Else
                ShowMessage("پرسنل یافت نشد", "danger")
            End If

            LoadPersonnelData()
        Catch ex As Exception
            ShowMessage("خطا در حذف: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' انصراف از ویرایش
    ' ============================================================
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        ClearForm()
        hfEditID.Value = "0"
        btnSavePersonnel.Text = "✅ ذخیره"
        formTitle.InnerText = "➕ افزودن پرسنل جدید"
        ShowMessage("", "")
        pnlMessage.Visible = False
    End Sub

    Private Sub ClearForm()
        txtCodePersoneli.Text = ""
        txtNationality.Text = ""
        txtFullName.Text = ""
        ddlShift.SelectedIndex = 0
        txtOmur.Text = ""  ' پاک کردن واحد سازمانی
        txtTarixStart.Text = ""
        txtExpireDate.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "removePreview",
            "var preview = document.getElementById('photoPreview'); if(preview) preview.remove();", True)
    End Sub

    ' ============================================================
    ' جستجو
    ' ============================================================
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SaveSelectedCodes()
        gridPersonnel.PageIndex = 0
        LoadPersonnelData()
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gridPersonnel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridPersonnel.PageIndexChanging
        SaveSelectedCodes()
        gridPersonnel.PageIndex = e.NewPageIndex
        LoadPersonnelData()
    End Sub

    ' ============================================================
    ' RowDataBound برای نمایش عکس و تیک‌ها
    ' ============================================================
    Protected Sub gridPersonnel_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridPersonnel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' تیک‌های انتخاب شده
            Dim chk As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing Then
                Dim code As String = gridPersonnel.DataKeys(e.Row.RowIndex)("codePersoneli").ToString()
                chk.Checked = SelectedPersonnel.Contains(code)
            End If

            ' نمایش عکس
            Dim img As Image = CType(e.Row.FindControl("imgPhoto"), Image)
            If img IsNot Nothing Then
                Dim photoPath As String = gridPersonnel.DataKeys(e.Row.RowIndex)("PhotoPath").ToString()
                If Not String.IsNullOrEmpty(photoPath) Then
                    Dim physicalPath As String = Server.MapPath(photoPath)
                    If File.Exists(physicalPath) Then
                        img.ImageUrl = ResolveUrl(photoPath)
                    Else
                        img.ImageUrl = ResolveUrl("~/Herasat/default.png")
                    End If
                Else
                    img.ImageUrl = ResolveUrl("~/Herasat/default.png")
                End If
            End If
        End If
    End Sub

    ' ============================================================
    ' عملیات انتخاب و چاپ
    ' ============================================================
    Private Sub SaveSelectedCodes()
        For Each row As GridViewRow In gridPersonnel.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing Then
                Dim code As String = gridPersonnel.DataKeys(row.RowIndex)("codePersoneli").ToString()
                If chk.Checked Then
                    If Not SelectedPersonnel.Contains(code) Then
                        SelectedPersonnel.Add(code)
                    End If
                Else
                    If SelectedPersonnel.Contains(code) Then
                        SelectedPersonnel.Remove(code)
                    End If
                End If
            End If
        Next
    End Sub

    Private Property SelectedPersonnel As List(Of String)
        Get
            If Session("SelectedPersonnel") Is Nothing Then
                Session("SelectedPersonnel") = New List(Of String)()
            End If
            Return CType(Session("SelectedPersonnel"), List(Of String))
        End Get
        Set(value As List(Of String))
            Session("SelectedPersonnel") = value
        End Set
    End Property

    Private Property CurrentBatchIndex As Integer
        Get
            If Session("CurrentBatchIndex") Is Nothing Then
                Session("CurrentBatchIndex") = 0
            End If
            Return Convert.ToInt32(Session("CurrentBatchIndex"))
        End Get
        Set(value As Integer)
            Session("CurrentBatchIndex") = value
        End Set
    End Property

    Protected Sub btnSelectFirst80_Click(sender As Object, e As EventArgs) Handles btnSelectFirst80.Click
        CurrentBatchIndex = 0
        SelectBatch(CurrentBatchIndex)
    End Sub

    Protected Sub btnSelectNext80_Click(sender As Object, e As EventArgs) Handles btnSelectNext80.Click
        CurrentBatchIndex += 1
        SelectBatch(CurrentBatchIndex)
    End Sub

    Private Sub SelectBatch(batchIndex As Integer)
        If batchIndex < 1 Then batchIndex = 1
        CurrentBatchIndex = batchIndex

        Dim offset As Integer = (batchIndex - 1) * 80

        Dim sql As String = "SELECT codePersoneli FROM (SELECT codePersoneli, ROW_NUMBER() OVER (ORDER BY ID) AS RowNum FROM Personnel_Management WHERE IsActive = 1) AS Temp WHERE RowNum > " & offset & " AND RowNum <= " & (offset + 80)
        Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

        For Each row As DataRow In dt.Rows
            Dim code As String = row("codePersoneli").ToString()
            If Not SelectedPersonnel.Contains(code) Then
                SelectedPersonnel.Add(code)
            End If
        Next

        LoadPersonnelData()
    End Sub

    ' ============================================================
    ' چاپ کارت‌های پرسنل
    ' ============================================================
    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        SaveSelectedCodes()

        Dim selectedCodes = SelectedPersonnel
        If selectedCodes.Count = 0 Then
            ShowMessage("لطفاً حداقل یک نفر را انتخاب کنید", "danger")
            Return
        End If

        ' ساخت لیست کدها
        Dim inClause As String = String.Join(",", selectedCodes.Select(Function(x) "'" & x & "'"))

        ' کوئری با تمام فیلدهای مورد نیاز
        Dim sql As String = "SELECT codePersoneli, Nationality, fulname, shift, omur, tarixStart, expireDateCard, PhotoPath FROM Personnel_Management WHERE codePersoneli IN (" & inClause & ") AND IsActive = 1"

        Dim dtAll As DataTable = ClsDataTable.GetDataTable(sql)

        ' بررسی داده
        If dtAll Is Nothing OrElse dtAll.Rows.Count = 0 Then
            ShowMessage("هیچ داده‌ای برای چاپ یافت نشد", "danger")
            Return
        End If

        ' ذخیره در Session با نام جدید
        Session("PersonnelCardDataTable") = dtAll

        ' باز کردن پنجره جدید
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "popup", "window.open('PrintPersonnelCards.aspx','_blank','width=800,height=600');", True)
    End Sub
End Class