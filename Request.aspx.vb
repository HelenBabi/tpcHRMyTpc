Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Partial Class Examination_Request
    Inherits System.Web.UI.Page

    Private _personCode As String = ""
    Private _messageType As String = "info"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            _personCode = Session("codePersoneli").ToString()

            If String.IsNullOrEmpty(_personCode) Then
                Response.Redirect("~/Login.aspx")
                Return
            End If

            LoadPersonInfo()
            LoadAvailableDates()
            LoadRequestHistory()
        End If
    End Sub

#Region "بارگذاری اطلاعات"

    Private Sub LoadPersonInfo()
        Try
            Dim sqlstr As String = "SELECT TOP 1 " & _
                                   "    Code, " & _
                                   "    Name + ' ' + Family AS FullName, " & _
                                   "    UnitName, " & _
                                   "    Post, " & _
                                   "    ParentCode, " & _
                                   "    ParentName + ' ' + ParentFamily AS SupervisorName " & _
                                   "FROM vwRaya " & _
                                   "WHERE Code = @PersonCode"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.AddWithValue("@PersonCode", _personCode)
                    cnn.Open()

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            lblPersonCode.Text = reader("Code").ToString()
                            lblFullName.Text = reader("FullName").ToString()
                            lblUnitName.Text = reader("UnitName").ToString()
                            lblPosition.Text = reader("Post").ToString()
                            lblSupervisor.Text = reader("SupervisorName").ToString()

                            hdnPersonCode.Value = reader("Code").ToString()
                            hdnSupervisorCode.Value = reader("ParentCode").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری اطلاعات: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadAvailableDates()
        Try
            Dim sqlstr As String = "SELECT " & _
                                   "    ExaminationDate, " & _
                                   "    dbo.ConvertGregorianToJalali(ExaminationDate) AS PersianDate, " & _
                                   "    Capacity, " & _
                                   "    (Capacity - ISNULL((SELECT COUNT(*) FROM exmReservationRequests " & _
                                   "                        WHERE ExaminationDate = c.ExaminationDate " & _
                                   "                        AND StatusCode NOT IN (6, 7)), 0)) AS Available, " & _
                                   "    ISNULL(cent.CenterName, 'مرکز بهداشت') AS CenterName " & _
                                   "FROM exmDailyCapacity c " & _
                                   "LEFT JOIN exmExaminationCenters cent ON c.CenterID = cent.ID " & _
                                   "WHERE ExaminationDate >= CAST(GETDATE() AS DATE) " & _
                                   "AND IsActive = 1 " & _
                                   "AND (Capacity - ISNULL((SELECT COUNT(*) FROM exmReservationRequests " & _
                                   "                        WHERE ExaminationDate = c.ExaminationDate " & _
                                   "                        AND StatusCode NOT IN (6, 7)), 0)) > 0 " & _
                                   "ORDER BY ExaminationDate"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            ddlExaminationDate.DataSource = dt
            ddlExaminationDate.DataTextField = "PersianDate"
            ddlExaminationDate.DataValueField = "ExaminationDate"
            ddlExaminationDate.DataBind()

            ddlExaminationDate.Items.Insert(0, New ListItem("-- انتخاب کنید --", ""))

            If ddlExaminationDate.Items.Count <= 1 Then
                ShowMessage("⚠️ در حال حاضر هیچ تاریخی با ظرفیت خالی وجود ندارد. لطفاً بعداً مراجعه کنید.", "warning")
                btnSubmit.Enabled = False
            End If
        Catch ex As Exception
            ShowMessage("خطا در بارگذاری تاریخ‌ها: " & ex.Message, "danger")
        End Try
    End Sub

    Private Sub LoadRequestHistory()
        Try
            Dim sqlstr As String = "SELECT " & _
                                   "    ID, " & _
                                   "    ExaminationDate, " & _
                                   "    dbo.ConvertGregorianToJalali(ExaminationDate) AS PersianDate, " & _
                                   "    StatusCode, " & _
                                   "    CASE StatusCode " & _
                                   "        WHEN 1 THEN 'در انتظار تایید سرپرست' " & _
                                   "        WHEN 2 THEN 'تایید سرپرست' " & _
                                   "        WHEN 3 THEN 'در انتظار تایید رییس' " & _
                                   "        WHEN 4 THEN 'تایید نهایی' " & _
                                   "        WHEN 6 THEN 'رد شده' " & _
                                   "        WHEN 7 THEN 'انجام شده' " & _
                                   "    END AS StatusText, " & _
                                   "    CASE StatusCode " & _
                                   "        WHEN 1 THEN 'pending' " & _
                                   "        WHEN 2 THEN 'pending' " & _
                                   "        WHEN 3 THEN 'pending' " & _
                                   "        WHEN 4 THEN 'approved' " & _
                                   "        WHEN 6 THEN 'rejected' " & _
                                   "        WHEN 7 THEN 'done' " & _
                                   "    END AS StatusClass, " & _
                                   "    SupervisorName, " & _
                                   "    ManagerName, " & _
                                   "    CreateDate, " & _
                                   "    dbo.ConvertGregorianToJalali(CreateDate) AS PersianCreateDate " & _
                                   "FROM exmReservationRequests " & _
                                   "WHERE PersonCode = @PersonCode " & _
                                   "ORDER BY CreateDate DESC"

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand(sqlstr, cnn)
                    cmd.Parameters.AddWithValue("@PersonCode", _personCode)
                    cnn.Open()

                    Dim dt As New DataTable()
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        rptHistory.DataSource = dt
                        rptHistory.DataBind()
                        pnlHistory.Visible = True
                        pnlNoHistory.Visible = False
                    Else
                        pnlHistory.Visible = False
                        pnlNoHistory.Visible = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            pnlHistory.Visible = False
            pnlNoHistory.Visible = True
        End Try
    End Sub

#End Region

#Region "رویدادها"

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            btnSubmit.Enabled = False
            btnSubmit.Text = "⏳ در حال ثبت..."

            Dim personCode As String = hdnPersonCode.Value
            Dim examinationDate As String = ddlExaminationDate.SelectedValue

            If String.IsNullOrEmpty(personCode) Then
                ShowMessage("کد پرسنلی یافت نشد", "danger")
                Return
            End If

            If String.IsNullOrEmpty(examinationDate) Then
                ShowMessage("لطفاً تاریخ معاینه را انتخاب کنید", "warning")
                btnSubmit.Enabled = True
                btnSubmit.Text = "📤 ثبت درخواست"
                Return
            End If

            Dim userID As String = Session("userid").ToString()

            Using cnn As New SqlConnection(ClsConnection.GetConn)
                Using cmd As New SqlCommand("sp_AddReservationRequest", cnn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@PersonCode", personCode)
                    cmd.Parameters.AddWithValue("@ExaminationDate", examinationDate)
                    cmd.Parameters.AddWithValue("@CenterID", DBNull.Value)
                    cmd.Parameters.AddWithValue("@UserID", userID)

                    cnn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        Dim requestID As Integer = Convert.ToInt32(reader("RequestID"))
                        Dim message As String = reader("Message").ToString()
                        Dim supervisorName As String = reader("SupervisorName").ToString()

                        ShowMessage("✅ " & message & " (شماره پیگیری: " & requestID & ")", "success")
                        btnSubmit.Enabled = False
                        btnSubmit.Text = "✅ ثبت شد"

                        ' بارگذاری مجدد تاریخچه
                        LoadRequestHistory()
                    End If
                    reader.Close()
                End Using
            End Using

        Catch ex As Exception
            ShowMessage("❌ خطا: " & ex.Message, "danger")
            btnSubmit.Enabled = True
            btnSubmit.Text = "📤 ثبت درخواست"
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Default.aspx")
    End Sub

#End Region

#Region "توابع کمکی"

    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅"
                pnlMessage.CssClass = "alert alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌"
                pnlMessage.CssClass = "alert alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️"
                pnlMessage.CssClass = "alert alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️"
                pnlMessage.CssClass = "alert alert-info"
        End Select

        pnlMessage.Visible = True
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

#End Region

End Class