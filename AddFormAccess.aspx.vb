Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class AddFormAccess
    Inherits System.Web.UI.Page

    Private Property UserCode As Integer
        Get
            If ViewState("UserCode") Is Nothing Then Return 0
            Return CInt(ViewState("UserCode"))
        End Get
        Set(value As Integer)
            ViewState("UserCode") = value
        End Set
    End Property

    ' ================== Page Load ==================
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            meno_acc()

            If Request.QueryString("UserCode") IsNot Nothing Then
                UserCode = CInt(Request.QueryString("UserCode"))
            End If

            If UserCode = 0 Then
                Response.Redirect("FormAccess.aspx")
                Return
            End If

            LoadUserInfo()
            LoadAvailableForms()
        End If
    End Sub

    ' ================== منوی دسترسی ==================
    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("AddFormAccess.aspx", Val(Session("UserId")))
    End Sub

    ' ================== بارگذاری اطلاعات کاربر ==================
    Private Sub LoadUserInfo()
        Dim sqlstr As String =
            "SELECT ISNULL(nam,'') + ' ' + ISNULL(family,'') AS FullName, " &
            "ISNULL(codePersoneli,0) AS PersonnelCode " &
            "FROM Personel WHERE ID=" & UserCode

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            Dim fullName As String = dt.Rows(0)("FullName").ToString()
            Dim personnelCode As String = dt.Rows(0)("PersonnelCode").ToString()

            lblUser.Text = fullName
            lblUserInfo.Text = fullName & " (کد: " & personnelCode & ")"
        End If
    End Sub

    ' ================== بارگذاری فرم‌های موجود (سلسله‌مراتبی) ==================
    Private Sub LoadAvailableForms()

        ' ============ دریافت همه فرم‌ها (بدون فیلتر دسترسی) ============
        Dim sqlstrAllForms As String =
            "SELECT FrmID, FrmName, FrmCaption, " &
            "       ISNULL(Frm_level, 0) AS Frm_level, " &
            "       ISNULL(Frm_Child, 0) AS Frm_Child, " &
            "       ISNULL(Frm_sort, 0) AS Frm_sort " &
            "FROM tblForm " &
            "ORDER BY Frm_sort, FrmID"

        Dim dtAllForms As DataTable = ClsDataTable.GetDataTable(sqlstrAllForms)

        ' ============ دریافت فرم‌هایی که کاربر دسترسی دارد ============
        Dim sqlstrUserAccess As String =
            "SELECT FrmID FROM tblFormAcc WHERE UserCode = " & UserCode

        Dim dtUserAccess As DataTable = ClsDataTable.GetDataTable(sqlstrUserAccess)

        ' ساخت لیست ID فرم‌های دارای دسترسی
        Dim accessedIDs As New List(Of Integer)
        For Each row As DataRow In dtUserAccess.Rows
            accessedIDs.Add(CInt(row("FrmID")))
        Next

        ' ============ فیلتر کردن فرم‌های بدون دسترسی ============
        Dim dtAvailable As DataTable = dtAllForms.Clone()
        For Each row As DataRow In dtAllForms.Rows
            Dim frmID As Integer = CInt(row("FrmID"))
            If Not accessedIDs.Contains(frmID) Then
                dtAvailable.ImportRow(row)
            End If
        Next

        ' ============ ساخت DropDownList سلسله‌مراتبی ============
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("FrmID", GetType(Integer))
        dtResult.Columns.Add("DisplayName", GetType(String))

        ' گرفتن لیست همه پدرها (حتی اگر خودشان در دسترسی باشند، فقط برای ساختار)
        Dim allParents As DataRow() = dtAvailable.Select(
            "ISNULL(Frm_Child,0) = 0 OR Frm_level = 1", "Frm_sort, FrmID")

        ' اگر پدری در dtAvailable نیست ولی فرزندش هست، پدر را از dtAllForms بیاور
        Dim addedParentIDs As New List(Of Integer)

        ' ============ مرحله ۱: فرم‌های پدر و فرزندانشان ============
        Dim sqlstrAllParents As String =
            "SELECT FrmID, FrmName, FrmCaption, " &
            "       ISNULL(Frm_level, 0) AS Frm_level, " &
            "       ISNULL(Frm_Child, 0) AS Frm_Child, " &
            "       ISNULL(Frm_sort, 0) AS Frm_sort " &
            "FROM tblForm " &
            "WHERE ISNULL(Frm_Child,0) = 0 OR Frm_level = 1 " &
            "ORDER BY Frm_sort, FrmID"

        Dim dtAllParents As DataTable = ClsDataTable.GetDataTable(sqlstrAllParents)

        For Each parentRow As DataRow In dtAllParents.Rows

            Dim parentID As Integer = CInt(parentRow("FrmID"))
            Dim parentName As String = parentRow("FrmName").ToString()
            Dim parentCaption As String = parentRow("FrmCaption").ToString()

            ' پیدا کردن فرزندان این پدر از فرم‌های بدون دسترسی
            Dim childRows As DataRow() = dtAvailable.Select(
                "ISNULL(Frm_Child,0) = " & parentID, "Frm_sort, FrmID")

            ' اگر پدر در دسترسی‌ها هست ولی فرزند بدون دسترسی دارد، پدر را نمایش بده
            Dim parentHasAccess As Boolean = accessedIDs.Contains(parentID)

            If childRows.Length > 0 OrElse Not parentHasAccess Then

                ' اضافه کردن پدر
                Dim parentText As String = "📁 " & parentCaption & " (" & parentName & ")"
                dtResult.Rows.Add(parentID, parentText)
                addedParentIDs.Add(parentID)

                ' اضافه کردن فرزندان
                For Each childRow As DataRow In childRows
                    Dim childID As Integer = CInt(childRow("FrmID"))
                    Dim childName As String = childRow("FrmName").ToString()
                    Dim childCaption As String = childRow("FrmCaption").ToString()

                    Dim childText As String = "    └─ 📄 " & childCaption & " (" & childName & ")"
                    dtResult.Rows.Add(childID, childText)
                Next
            End If
        Next

        ' ============ مرحله ۲: فرم‌های یتیم (بدون پدر) ============
        For Each row As DataRow In dtAvailable.Rows
            Dim frmID As Integer = CInt(row("FrmID"))
            Dim childOf As Integer = CInt(row("Frm_Child"))

            ' اگر پدرش در لیست اضافه شده، قبلاً اضافه شده
            If childOf > 0 AndAlso addedParentIDs.Contains(childOf) Then
                Continue For
            End If

            ' اگر خودش قبلاً اضافه نشده، اضافه کن
            Dim alreadyAdded As Boolean = False
            For Each dr As DataRow In dtResult.Rows
                If CInt(dr("FrmID")) = frmID Then
                    alreadyAdded = True
                    Exit For
                End If
            Next

            If Not alreadyAdded Then
                Dim orphanName As String = row("FrmName").ToString()
                Dim orphanCaption As String = row("FrmCaption").ToString()

                Dim orphanText As String
                If childOf > 0 Then
                    ' فرزندی که پدرش در دسترسی است ولی خودش نیست
                    orphanText = "    └─ 📄 " & orphanCaption & " (" & orphanName & ")"
                Else
                    ' فرم مستقل
                    orphanText = "📄 " & orphanCaption & " (" & orphanName & ")"
                End If

                dtResult.Rows.Add(frmID, orphanText)
            End If
        Next

        ' ============ Bind به DropDownList ============
        ddlForms.DataSource = dtResult
        ddlForms.DataTextField = "DisplayName"
        ddlForms.DataValueField = "FrmID"
        ddlForms.DataBind()

        If dtResult.Rows.Count > 0 Then
            ddlForms.Items.Insert(0, New ListItem("-- انتخاب کنید --", ""))
        Else
            ddlForms.Items.Insert(0, New ListItem("تمام فرم‌ها قبلاً داده شده است", ""))
            btnSave.Enabled = False
        End If

    End Sub

    ' ================== دکمه ذخیره ==================
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If ddlForms.SelectedValue = "" Then
            lblMsg.Text = "لطفاً یک فرم انتخاب کنید."
            Return
        End If

        Dim frmId As Integer = CInt(ddlForms.SelectedValue)

        Try
            InsertFormAccessWithTransaction( _
                UserCode, frmId, _
                chkFormAccess.Checked, _
                chkAllowNew.Checked, _
                chkAllowEdit.Checked, _
                chkAllowDelete.Checked, _
                chkAllowPrint.Checked, _
                chkAllowViewOldData.Checked, _
                chkAllowEditDate.Checked, _
                chkPrintAfterSave.Checked, _
                chkAllowDeleted.Checked, _
                chkAllowclosed.Checked, _
                chkAllowHistory.Checked)

            Response.Redirect("FormAccess.aspx?UserCode=" & UserCode)

        Catch ex As Exception
            lblMsg.Text = "خطا در ذخیره: " & ex.Message
        End Try
    End Sub

    ' ================== ثبت دسترسی با SqlTransaction ==================
    Private Sub InsertFormAccessWithTransaction( _
        ByVal userCode As Integer, _
        ByVal frmId As Integer, _
        ByVal formAccess As Boolean, _
        ByVal allowNew As Boolean, _
        ByVal allowEdit As Boolean, _
        ByVal allowDelete As Boolean, _
        ByVal allowPrint As Boolean, _
        ByVal allowViewOldData As Boolean, _
        ByVal allowEditDate As Boolean, _
        ByVal printAfterSave As Boolean, _
        ByVal allowDeleted As Boolean, _
        ByVal allowClosed As Boolean, _
        ByVal allowHistory As Boolean)

        Using cnn As New SqlConnection(ClsConnection.GetConn)
            cnn.Open()

            Using trans As SqlTransaction = cnn.BeginTransaction()
                Try

                    ' ============ بررسی وجود قبلی ============
                    Dim checkSql As String =
                        "SELECT COUNT(*) FROM tblFormAcc " &
                        "WHERE UserCode = @UserCode AND FrmID = @FrmID"

                    Dim exists As Integer = 0
                    Using cmd As New SqlCommand(checkSql, cnn, trans)
                        cmd.Parameters.Add("@UserCode", SqlDbType.Int).Value = userCode
                        cmd.Parameters.Add("@FrmID", SqlDbType.Int).Value = frmId
                        exists = CInt(cmd.ExecuteScalar())
                    End Using

                    If exists > 0 Then
                        Throw New Exception("این دسترسی قبلاً برای این کاربر ثبت شده است.")
                    End If

                    ' ============ ثبت دسترسی جدید ============
                    Dim insertSql As String =
                        "INSERT INTO tblFormAcc " &
                        "   (UserCode, FrmID, FormAccess, AllowNew, AllowEdit, " &
                        "    AllowDelete, AllowPrint, AllowViewOldData, AllowEditDate, " &
                        "    PrintAfterSave, AllowDeleted, Allowclosed, AllowHistory) " &
                        "VALUES " &
                        "   (@UserCode, @FrmID, @FormAccess, @AllowNew, @AllowEdit, " &
                        "    @AllowDelete, @AllowPrint, @AllowViewOldData, @AllowEditDate, " &
                        "    @PrintAfterSave, @AllowDeleted, @Allowclosed, @AllowHistory)"

                    Using cmd As New SqlCommand(insertSql, cnn, trans)
                        cmd.Parameters.Add("@UserCode", SqlDbType.Int).Value = userCode
                        cmd.Parameters.Add("@FrmID", SqlDbType.Int).Value = frmId
                        cmd.Parameters.Add("@FormAccess", SqlDbType.Bit).Value = formAccess
                        cmd.Parameters.Add("@AllowNew", SqlDbType.Bit).Value = allowNew
                        cmd.Parameters.Add("@AllowEdit", SqlDbType.Bit).Value = allowEdit
                        cmd.Parameters.Add("@AllowDelete", SqlDbType.Bit).Value = allowDelete
                        cmd.Parameters.Add("@AllowPrint", SqlDbType.Bit).Value = allowPrint
                        cmd.Parameters.Add("@AllowViewOldData", SqlDbType.Bit).Value = allowViewOldData
                        cmd.Parameters.Add("@AllowEditDate", SqlDbType.Bit).Value = allowEditDate
                        cmd.Parameters.Add("@PrintAfterSave", SqlDbType.Bit).Value = printAfterSave
                        cmd.Parameters.Add("@AllowDeleted", SqlDbType.Bit).Value = allowDeleted
                        cmd.Parameters.Add("@Allowclosed", SqlDbType.Bit).Value = allowClosed
                        cmd.Parameters.Add("@AllowHistory", SqlDbType.Bit).Value = allowHistory

                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected = 0 Then
                            Throw New Exception("خطا در ثبت دسترسی.")
                        End If
                    End Using

                    trans.Commit()

                Catch ex As Exception
                    Try
                        trans.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Sub

    ' ================== دکمه انصراف ==================
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("FormAccess.aspx?UserCode=" & UserCode)
    End Sub

End Class