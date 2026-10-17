Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class EditFormAccess
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

    Private Property FrmID As Integer
        Get
            If ViewState("FrmID") Is Nothing Then Return 0
            Return CInt(ViewState("FrmID"))
        End Get
        Set(value As Integer)
            ViewState("FrmID") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            meno_acc()

            If Request.QueryString("UserCode") IsNot Nothing Then
                UserCode = CInt(Request.QueryString("UserCode"))
            End If
            If Request.QueryString("FrmID") IsNot Nothing Then
                FrmID = CInt(Request.QueryString("FrmID"))
            End If

            If UserCode = 0 OrElse FrmID = 0 Then
                Response.Redirect("FormAccess.aspx")
                Return
            End If

            LoadUserInfo()
            LoadAccessData()
        End If
    End Sub

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("EditFormAccess.aspx", Val(Session("UserId")))
    End Sub

    ' ================== بارگذاری اطلاعات کاربر ==================
    Private Sub LoadUserInfo()
        ' ✅ جدول Personel
        Dim sqlstr As String =
            "SELECT ISNULL(nam,'') + ' ' + ISNULL(family,'') AS FullName, " &
            "ISNULL(codePersoneli,0) AS PersonnelCode " &
            "FROM Personel WHERE ID=" & UserCode

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            lblUser.Text = dt.Rows(0)("FullName").ToString() &
                           " (کد: " & dt.Rows(0)("PersonnelCode").ToString() & ")"
        End If
    End Sub

    ' ================== بارگذاری داده‌های دسترسی ==================
    Private Sub LoadAccessData()
        Dim sqlstr As String =
            "SELECT f.FrmCaption, f.FrmName, " &
            "ISNULL(fa.FormAccess,0) AS FormAccess, " &
            "ISNULL(fa.AllowNew,0) AS AllowNew, " &
            "ISNULL(fa.AllowEdit,0) AS AllowEdit, " &
            "ISNULL(fa.AllowDelete,0) AS AllowDelete, " &
            "ISNULL(fa.AllowPrint,0) AS AllowPrint, " &
            "ISNULL(fa.AllowViewOldData,0) AS AllowViewOldData, " &
            "ISNULL(fa.AllowEditDate,0) AS AllowEditDate, " &
            "ISNULL(fa.PrintAfterSave,0) AS PrintAfterSave, " &
            "ISNULL(fa.AllowDeleted,0) AS AllowDeleted, " &
            "ISNULL(fa.Allowclosed,0) AS Allowclosed, " &
            "ISNULL(fa.AllowHistory,0) AS AllowHistory " &
            "FROM tblForm f " &
            "LEFT JOIN tblFormAcc fa ON fa.FrmID = f.FrmID AND fa.UserCode=" & UserCode & " " &
            "WHERE f.FrmID = " & FrmID

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            lblFormName.Text = dr("FrmCaption").ToString() & " (" & dr("FrmName").ToString() & ")"
            lblFormInfo.Text = dr("FrmCaption").ToString() & " (" & dr("FrmName").ToString() & ")"

            chkFormAccess.Checked = CBool(dr("FormAccess"))
            chkAllowNew.Checked = CBool(dr("AllowNew"))
            chkAllowEdit.Checked = CBool(dr("AllowEdit"))
            chkAllowDelete.Checked = CBool(dr("AllowDelete"))
            chkAllowPrint.Checked = CBool(dr("AllowPrint"))
            chkAllowViewOldData.Checked = CBool(dr("AllowViewOldData"))
            chkAllowEditDate.Checked = CBool(dr("AllowEditDate"))
            chkPrintAfterSave.Checked = CBool(dr("PrintAfterSave"))
            chkAllowDeleted.Checked = CBool(dr("AllowDeleted"))
            chkAllowclosed.Checked = CBool(dr("Allowclosed"))
            chkAllowHistory.Checked = CBool(dr("AllowHistory"))
        End If
    End Sub

    ' ================== دکمه ذخیره ==================
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            UpdateFormAccessWithTransaction(
                UserCode, FrmID,
                chkFormAccess.Checked,
                chkAllowNew.Checked,
                chkAllowEdit.Checked,
                chkAllowDelete.Checked,
                chkAllowPrint.Checked,
                chkAllowViewOldData.Checked,
                chkAllowEditDate.Checked,
                chkPrintAfterSave.Checked,
                chkAllowDeleted.Checked,
                chkAllowclosed.Checked,
                chkAllowHistory.Checked)

            Response.Redirect("FormAccess.aspx?UserCode=" & UserCode)

        Catch ex As Exception
            lblMsg.Text = "خطا در ویرایش: " & ex.Message
        End Try
    End Sub

    ' ================== ویرایش با SqlTransaction ==================
    Private Sub UpdateFormAccessWithTransaction(
        ByVal userCode As Integer,
        ByVal frmId As Integer,
        ByVal formAccess As Boolean,
        ByVal allowNew As Boolean,
        ByVal allowEdit As Boolean,
        ByVal allowDelete As Boolean,
        ByVal allowPrint As Boolean,
        ByVal allowViewOldData As Boolean,
        ByVal allowEditDate As Boolean,
        ByVal printAfterSave As Boolean,
        ByVal allowDeleted As Boolean,
        ByVal allowClosed As Boolean,
        ByVal allowHistory As Boolean)

        Using cnn As New SqlConnection(ClsConnection.GetConn)
            cnn.Open()

            Using trans As SqlTransaction = cnn.BeginTransaction()
                Try
                    Dim checkSql As String =
                        "SELECT COUNT(*) FROM tblFormAcc " &
                        "WHERE UserCode = @UserCode AND FrmID = @FrmID"

                    Dim exists As Integer = 0
                    Using cmd As New SqlCommand(checkSql, cnn, trans)
                        cmd.Parameters.Add("@UserCode", SqlDbType.Int).Value = userCode
                        cmd.Parameters.Add("@FrmID", SqlDbType.Int).Value = frmId
                        exists = CInt(cmd.ExecuteScalar())
                    End Using

                    If exists = 0 Then
                        Throw New Exception("رکورد دسترسی برای ویرایش پیدا نشد.")
                    End If

                    Dim updateSql As String =
                        "UPDATE tblFormAcc " &
                        "SET FormAccess = @FormAccess, " &
                        "    AllowNew = @AllowNew, " &
                        "    AllowEdit = @AllowEdit, " &
                        "    AllowDelete = @AllowDelete, " &
                        "    AllowPrint = @AllowPrint, " &
                        "    AllowViewOldData = @AllowViewOldData, " &
                        "    AllowEditDate = @AllowEditDate, " &
                        "    PrintAfterSave = @PrintAfterSave, " &
                        "    AllowDeleted = @AllowDeleted, " &
                        "    Allowclosed = @Allowclosed, " &
                        "    AllowHistory = @AllowHistory " &
                        "WHERE UserCode = @UserCode " &
                        "  AND FrmID = @FrmID"

                    Using cmd As New SqlCommand(updateSql, cnn, trans)
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
                            Throw New Exception("خطا در ویرایش دسترسی.")
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

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("FormAccess.aspx?UserCode=" & UserCode)
    End Sub

End Class