Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class FormAccess
    Inherits System.Web.UI.Page

    Private Property SelectedUserCode As Integer
        Get
            If ViewState("SelectedUserCode") Is Nothing Then Return 0
            Return CInt(ViewState("SelectedUserCode"))
        End Get
        Set(value As Integer)
            ViewState("SelectedUserCode") = value
        End Set
    End Property

    Private Property SelectedUserName As String
        Get
            If ViewState("SelectedUserName") Is Nothing Then Return ""
            Return ViewState("SelectedUserName").ToString()
        End Get
        Set(value As String)
            ViewState("SelectedUserName") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            meno_acc()

            If Request.QueryString("UserCode") IsNot Nothing Then
                SelectedUserCode = CInt(Request.QueryString("UserCode"))
                LoadSelectedUser()
            End If

            BindPersonel()
        End If
    End Sub

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("FormAccess.aspx", Val(Session("UserId")))
    End Sub

    Private Sub BindPersonel()
        Dim search As String = txtSearch.Text.Trim()

        Dim sqlstr As String =
            "SELECT ID, " &
            "ISNULL(codePersoneli,0) AS PersonnelCode, " &
            "ISNULL(nam,'') + ' ' + ISNULL(family,'') AS FullName, " &
            "ISNULL(postname,'') AS PostName " &
            "FROM Personel " &
            "WHERE ISNULL(CodeClosed,0) = 0 "

        If search <> "" Then
            Dim s As String = search.Replace("'", "''")
            sqlstr &= "AND (nam LIKE N'%" & s & "%' " &
                      "OR family LIKE N'%" & s & "%' " &
                      "OR codePersoneli LIKE N'%" & s & "%' " &
                      "OR postname LIKE N'%" & s & "%') "
        End If

        sqlstr &= "ORDER BY family, nam"

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        gvPersonel.DataSource = dt
        gvPersonel.DataBind()

        lblCount.Text = "(" & dt.Rows.Count & " نفر)"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        gvPersonel.PageIndex = 0
        BindPersonel()
    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        gvPersonel.PageIndex = 0
        BindPersonel()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        txtSearch.Text = ""
        gvPersonel.PageIndex = 0
        BindPersonel()
    End Sub

    Protected Sub gvPersonel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvPersonel.PageIndex = e.NewPageIndex
        BindPersonel()
    End Sub

    Protected Sub gvPersonel_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim id As Integer = CInt(gvPersonel.DataKeys(e.Row.RowIndex).Value)
            If id = SelectedUserCode Then
                e.Row.CssClass = "selected-row"
            End If
        End If
    End Sub

    Protected Sub gvPersonel_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim userId As Integer = CInt(e.CommandArgument)

        Select Case e.CommandName

            Case "SelectPersonel"
                SelectedUserCode = userId
                LoadSelectedUser()
                BindPersonel()

            Case "GiveAccess"
                Response.Redirect("AddFormAccess.aspx?UserCode=" & userId)

            Case "TakeAccess"
                Response.Redirect("EditFormAccess.aspx?UserCode=" & userId & "&Mode=Take")

        End Select
    End Sub

    Private Sub LoadSelectedUser()
        Dim sqlstr As String =
            "SELECT ISNULL(nam,'') + ' ' + ISNULL(family,'') AS FullName, " &
            "ISNULL(codePersoneli,0) AS PersonnelCode " &
            "FROM Personel WHERE ID=" & SelectedUserCode

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            SelectedUserName = dt.Rows(0)("FullName").ToString() &
                               " (کد: " & dt.Rows(0)("PersonnelCode").ToString() & ")"
        End If

        lblSelectedUser.Text = SelectedUserName
        pnlAccess.Visible = True
        pnlEmpty.Visible = False

        BindUserAccess()
    End Sub

    Private Sub BindUserAccess()
        Dim sqlstr As String =
            "SELECT fa.FrmID, f.FrmName, f.FrmCaption, " &
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
            "FROM tblFormAcc fa " &
            "INNER JOIN tblForm f ON f.FrmID = fa.FrmID " &
            "WHERE fa.UserCode = " & SelectedUserCode & " " &
            "ORDER BY f.Frm_sort"

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)
        gvAccess.DataSource = dt
        gvAccess.DataBind()
    End Sub

    Protected Function GetIconClass(value As Object) As String
        If value IsNot Nothing AndAlso value IsNot DBNull.Value AndAlso CBool(value) Then
            Return "icon-yes"
        End If
        Return "icon-no"
    End Function

    Protected Function GetIconText(value As Object) As String
        If value IsNot Nothing AndAlso value IsNot DBNull.Value AndAlso CBool(value) Then
            Return "✓"
        End If
        Return "✗"
    End Function

    Protected Function GetAccessCount(userId As Object) As String
        If userId Is Nothing OrElse userId Is DBNull.Value Then Return "0"

        Dim sqlstr As String =
            "SELECT COUNT(*) AS Cnt FROM tblFormAcc WHERE UserCode=" & CInt(userId)

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Cnt").ToString()
        End If
        Return "0"
    End Function

    Protected Function GetAccessBadgeClass(userId As Object) As String
        Dim count As Integer = 0

        If userId IsNot Nothing AndAlso userId IsNot DBNull.Value Then
            Dim sqlstr As String =
                "SELECT COUNT(*) AS Cnt FROM tblFormAcc WHERE UserCode=" & CInt(userId)

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                count = CInt(dt.Rows(0)("Cnt"))
            End If
        End If

        If count = 0 Then Return "badge-access badge-access-zero"
        If count <= 5 Then Return "badge-access badge-access-low"
        If count <= 15 Then Return "badge-access badge-access-mid"
        Return "badge-access badge-access-high"
    End Function

    Protected Sub btnAddNew_Click(sender As Object, e As EventArgs)
        Response.Redirect("AddFormAccess.aspx?UserCode=" & SelectedUserCode)
    End Sub

    Protected Sub btnBulkAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("BulkAddAccess.aspx?UserCode=" & SelectedUserCode)
    End Sub

    Protected Sub gvAccess_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim frmId As Integer = CInt(e.CommandArgument)

        Select Case e.CommandName

            Case "EditAccess"
                Response.Redirect("EditFormAccess.aspx?UserCode=" & SelectedUserCode & "&FrmID=" & frmId)

            Case "DeleteAccess"
                Try
                    DeleteAccess(SelectedUserCode, frmId)
                    BindUserAccess()
                    BindPersonel()
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err",
                        "alert('خطا در گرفتن دسترسی: " & ex.Message.Replace("'", "\'") & "');", True)
                End Try

        End Select
    End Sub

    Private Sub DeleteAccess(ByVal userCode As Integer, ByVal frmId As Integer)
        Using cnn As New SqlConnection(ClsConnection.GetConn)
            cnn.Open()

            Using trans As SqlTransaction = cnn.BeginTransaction()
                Try
                    Dim deleteSql As String =
                        "DELETE FROM tblFormAcc " &
                        "WHERE UserCode = @UserCode " &
                        "  AND FrmID = @FrmID"

                    Using cmd As New SqlCommand(deleteSql, cnn, trans)
                        cmd.Parameters.Add("@UserCode", SqlDbType.Int).Value = userCode
                        cmd.Parameters.Add("@FrmID", SqlDbType.Int).Value = frmId

                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected = 0 Then
                            Throw New Exception("رکورد دسترسی پیدا نشد.")
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

End Class