Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class index
    Inherits System.Web.UI.Page

    Private Const ACADEMIC_YEAR As String = "1405-1404"

    Private _personCode As String = ""
    Private _fullName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("codePersoneli") Is Nothing Then
            Response.Redirect("~/Login_2.aspx")
            Return
        End If

        _personCode = Session("codePersoneli").ToString().Trim()
        _fullName = If(Session("FullName") IsNot Nothing, Session("FullName").ToString(), "")

        If Not IsPostBack Then
            ' نام کاربر برای خوش‌آمدگویی
            Dim displayName As String = _fullName
            If String.IsNullOrEmpty(displayName) AndAlso Session("fname") IsNot Nothing Then
                displayName = Session("fname").ToString()
            End If
            If String.IsNullOrEmpty(displayName) Then
                displayName = "همکار گرامی"
            End If
            lblUserName.Text = Server.HtmlEncode(displayName)

            ' ✅ تاریخ شمسی امروز
            Try
                Dim dds As New ClsDateTime()
                lblDate.Text = dds.getdatee
            Catch
                lblDate.Text = ""
            End Try

            ' آمار اعلان‌ها
            LoadUserStats()
        End If
    End Sub

    Private Sub LoadUserStats()
        ' تعداد اعلان‌های خوانده‌نشده
        Try
            Dim sqlNoti As String =
                "SELECT COUNT(*) FROM tblJobResiverTemp " &
                "WHERE show = 0 AND reciverID = @UserID"
            Dim notiCount As Integer = 0
            If Session("UserId") IsNot Nothing Then
                Dim result As Object = ExecuteScalar(sqlNoti,
                    New SqlParameter("@UserID", Session("UserId")))
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Integer.TryParse(result.ToString(), notiCount)
                End If
            End If
            lblNotificationCount.Text = notiCount.ToString()
        Catch
            lblNotificationCount.Text = "0"
        End Try
    End Sub

    Private Function ExecuteScalar(ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As Object
        Using cnn As New SqlConnection(ClsConnection.GetConn)
            Using cmd As New SqlCommand(sql, cnn)
                For Each p As SqlParameter In parameters
                    cmd.Parameters.Add(p)
                Next
                cnn.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

End Class