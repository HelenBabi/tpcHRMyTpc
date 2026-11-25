
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class RestaurantLunchReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd")
            txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd")

            BindReport()

        End If

    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)

        BindReport()

    End Sub
    '========================================
    ' اتصال دیتابیس و اجرای ExecuteScalar
    '========================================

    Private Function ExecuteScalar(ByVal sql As String,
                                   ByVal ParamArray parameters() As SqlParameter) As Object

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

    '========================================
    ' تبدیل تاریخ
    '========================================

    Private Function GetDateValue(ByVal value As String) As DateTime

        Dim result As DateTime

        If Not DateTime.TryParseExact(
            value,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            result) Then

            Throw New Exception("تاریخ وارد شده معتبر نیست.")

        End If

        Return result.Date

    End Function

    Private Sub ValidateDateRange()

        Dim fromDate As DateTime = GetDateValue(txtFromDate.Text)
        Dim toDate As DateTime = GetDateValue(txtToDate.Text)

        If fromDate > toDate Then
            Throw New Exception("تاریخ شروع نمی‌تواند از تاریخ پایان بزرگ‌تر باشد.")
        End If

    End Sub

    '========================================
    ' نمایش پیام
    '========================================

    Private Sub ShowMessage(ByVal message As String)

        lblMessage.Text = Server.HtmlEncode(message)
        lblMessage.Visible = True

    End Sub

    '========================================
    ' نمایش گزارش اصلی
    '========================================

    Private Sub BindReport()

        Try

            lblMessage.Visible = False

            ValidateDateRange()

            Dim fromDate As DateTime = GetDateValue(txtFromDate.Text)
            Dim toDate As DateTime = GetDateValue(txtToDate.Text)

            Dim dt As New DataTable()

            Using cnn As New SqlConnection(ClsConnection.GetConn)

                Dim sql As String =
      "SELECT " &
      " R.MenuDate, " &
      " dbo.FormatDateFa(CONVERT(DATE, R.MenuDate)) AS MenuDateFa, " &
      " R.SelectedFood, " &
      " COUNT(*) AS ReserveCount " &
      " FROM dbo.RestaurantLunchReservations R " &
      " WHERE R.MenuDate >= @FromDate " &
      " AND R.MenuDate < DATEADD(DAY, 1, @ToDate) " &
      " AND ISNULL(R.IsLeave, 0) = 0 " &
      " AND ISNULL(R.SelectedFood, '') <> '' " &
      " GROUP BY R.MenuDate, R.SelectedFood " &
      " ORDER BY R.MenuDate, R.SelectedFood"

                Using cmd As New SqlCommand(sql, cnn)

                    cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate
                    cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using

                End Using

            End Using

            gvSummary.DataSource = dt
            gvSummary.DataBind()

            Dim totalReservations As Integer = 0

            For Each row As DataRow In dt.Rows
                totalReservations += Convert.ToInt32(row("ReserveCount"))
            Next

            lblTotalReservations.Text = totalReservations.ToString("N0")

            lblTotalLeaves.Text = GetTotalLeaves(fromDate, toDate).ToString("N0")

            ViewState("SummaryData") = dt

        Catch ex As Exception

            ShowMessage(ex.Message)

        End Try

    End Sub

    '========================================
    ' تعداد مرخصی ها
    '========================================

    Private Function GetTotalLeaves(ByVal fromDate As DateTime,
                                    ByVal toDate As DateTime) As Integer

        Dim sql As String =
            "SELECT COUNT(*) " &
            "FROM dbo.RestaurantLunchReservations " &
            "WHERE MenuDate >= @FromDate " &
            "AND MenuDate < DATEADD(DAY, 1, @ToDate) " &
            "AND ISNULL(IsLeave, 0) = 1"

        Dim result As Object = ExecuteScalar(
            sql,
            New SqlParameter("@FromDate", SqlDbType.Date) With {.Value = fromDate},
            New SqlParameter("@ToDate", SqlDbType.Date) With {.Value = toDate}
        )

        If result Is Nothing OrElse IsDBNull(result) Then
            Return 0
        End If

        Return Convert.ToInt32(result)

    End Function

    '========================================
    ' کلیک روی مشاهده جزئیات
    '========================================

    Protected Sub gvSummary_RowCommand(ByVal sender As Object,
                                       ByVal e As GridViewCommandEventArgs)

        If e.CommandName = "ShowDetails" Then

            Try

                Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

                Dim selectedDate As DateTime =
                    Convert.ToDateTime(
                        gvSummary.DataKeys(rowIndex).Values("MenuDate")
                    )

                Dim selectedFood As String =
                    Convert.ToString(
                        gvSummary.DataKeys(rowIndex).Values("SelectedFood")
                    )

                hfSelectedDate.Value = selectedDate.ToString("yyyy-MM-dd")
                hfSelectedFood.Value = selectedFood

                BindDetails(selectedDate, selectedFood)

                Dim persianDate As String = ""

                Using cnn As New SqlConnection(ClsConnection.GetConn)

                    Dim sqlDate As String =
                        "SELECT dbo.FormatDateFa(@MenuDate)"

                    Using cmd As New SqlCommand(sqlDate, cnn)

                        cmd.Parameters.Add("@MenuDate", SqlDbType.Date).Value = selectedDate.Date

                        cnn.Open()

                        persianDate = Convert.ToString(cmd.ExecuteScalar())

                    End Using

                End Using

                lblDetailsTitle.Text =
                    Server.HtmlEncode(selectedFood & " - " & persianDate)

                ScriptManager.RegisterStartupScript(
                    Me,
                    Me.GetType(),
                    "OpenDetails",
                    "openDetailsModal();",
                    True
                )

            Catch ex As Exception

                ShowMessage(ex.Message)

            End Try

        End If

    End Sub

    '========================================
    ' دریافت جزئیات پرسنل
    '========================================

    Private Sub BindDetails(ByVal selectedDate As DateTime,
                            ByVal selectedFood As String)

        Dim dt As New DataTable()

        Using cnn As New SqlConnection(ClsConnection.GetConn)

            Dim sql As String =
                "SELECT " &
                " P.codePersoneli, " &
                " P.nam + N' ' + P.family AS FullName, " &
                " P.mobile, " &
                " P.postname, " &
                " O.omur AS OmurName, " &
                " U.UnitsName " &
                " FROM dbo.RestaurantLunchReservations R " &
                " INNER JOIN dbo.personel P ON P.ID = R.UserID " &
                " LEFT JOIN dbo.omurTbl O ON O.omurID = P.omurID " &
                " LEFT JOIN dbo.tblUnits U ON U.UnitsCode = P.UnitsCode " &
                " WHERE R.MenuDate >= @MenuDate " &
                " AND R.MenuDate < DATEADD(DAY, 1, @MenuDate) " &
                " AND R.SelectedFood = @SelectedFood " &
                " AND ISNULL(R.IsLeave, 0) = 0 " &
                " ORDER BY P.codePersoneli"

            Using cmd As New SqlCommand(sql, cnn)

                cmd.Parameters.Add("@MenuDate", SqlDbType.Date).Value = selectedDate.Date

                cmd.Parameters.Add("@SelectedFood", SqlDbType.NVarChar, 255).Value =
                    selectedFood

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

            End Using

        End Using

        gvDetails.DataSource = dt
        gvDetails.DataBind()

        ViewState("DetailsData") = dt

    End Sub

    '========================================
    ' دریافت اطلاعات گزارش برای اکسل
    '========================================

    Private Function GetSummaryExportData(ByVal fromDate As DateTime,
                                          ByVal toDate As DateTime) As DataTable

        Dim dt As New DataTable()

        Using cnn As New SqlConnection(ClsConnection.GetConn)

            Dim sql As String =
     "SELECT " &
     " dbo.FormatDateFa(CONVERT(DATE, R.MenuDate)) AS [تاریخ شمسی], " &
     " R.SelectedFood AS [نام غذا], " &
     " COUNT(*) AS [تعداد رزرو] " &
     " FROM dbo.RestaurantLunchReservations R " &
     " WHERE R.MenuDate >= @FromDate " &
     " AND R.MenuDate < DATEADD(DAY, 1, @ToDate) " &
     " AND ISNULL(R.IsLeave, 0) = 0 " &
     " AND ISNULL(R.SelectedFood, '') <> '' " &
     " GROUP BY R.MenuDate, R.SelectedFood " &
     " ORDER BY R.MenuDate, R.SelectedFood"
            Using cmd As New SqlCommand(sql, cnn)

                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

            End Using

        End Using

        Return dt

    End Function

    '========================================
    ' اطلاعات جزئیات برای اکسل
    '========================================

    Private Function GetDetailsExportData(ByVal selectedDate As DateTime,
                                          ByVal selectedFood As String) As DataTable

        Dim dt As New DataTable()

        Using cnn As New SqlConnection(ClsConnection.GetConn)

            Dim sql As String =
                "SELECT " &
                " P.codePersoneli AS [کد پرسنلی], " &
                " P.nam + N' ' + P.family AS [نام و نام خانوادگی], " &
                " P.postname AS [سمت], " &
                " O.omur AS [امور پرسنل], " &
                " U.UnitsName AS [واحد پرسنلی], " &
                " P.mobile AS [شماره همراه] " &
                " FROM dbo.RestaurantLunchReservations R " &
                " INNER JOIN dbo.personel P ON P.ID = R.UserID " &
                " LEFT JOIN dbo.omurTbl O ON O.omurID = P.omurID " &
                " LEFT JOIN dbo.tblUnits U ON U.UnitsCode = P.UnitsCode " &
                " WHERE R.MenuDate >= @MenuDate " &
                " AND R.MenuDate < DATEADD(DAY, 1, @MenuDate) " &
                " AND R.SelectedFood = @SelectedFood " &
                " AND ISNULL(R.IsLeave, 0) = 0 " &
                " ORDER BY P.codePersoneli"

            Using cmd As New SqlCommand(sql, cnn)

                cmd.Parameters.Add("@MenuDate", SqlDbType.Date).Value = selectedDate.Date

                cmd.Parameters.Add("@SelectedFood", SqlDbType.NVarChar, 255).Value =
                    selectedFood

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

            End Using

        End Using

        Return dt

    End Function

    '========================================
    ' خروجی اکسل گزارش اصلی
    '========================================

    Protected Sub btnExportSummary_Click(ByVal sender As Object,
                                         ByVal e As EventArgs)

        Try

            ValidateDateRange()

            Dim fromDate As DateTime = GetDateValue(txtFromDate.Text)
            Dim toDate As DateTime = GetDateValue(txtToDate.Text)

            Dim dt As DataTable = GetSummaryExportData(fromDate, toDate)

            ExportDataTableToExcel(
                dt,
                "RestaurantLunchReport_" &
                DateTime.Now.ToString("yyyyMMdd_HHmm") & ".xls"
            )

        Catch ex As Exception

            ShowMessage(ex.Message)

        End Try

    End Sub

    '========================================
    ' خروجی اکسل جزئیات پرسنل
    '========================================

    Protected Sub btnExportDetails_Click(ByVal sender As Object,
                                         ByVal e As EventArgs)

        Try

            If String.IsNullOrEmpty(hfSelectedDate.Value) OrElse
               String.IsNullOrEmpty(hfSelectedFood.Value) Then

                Throw New Exception("ابتدا یک غذا را از گزارش انتخاب کنید.")

            End If

            Dim selectedDate As DateTime = GetDateValue(hfSelectedDate.Value)
            Dim selectedFood As String = hfSelectedFood.Value

            Dim dt As DataTable =
                GetDetailsExportData(selectedDate, selectedFood)

            ExportDataTableToExcel(
                dt,
                "RestaurantEmployees_" &
                selectedDate.ToString("yyyyMMdd") & ".xls"
            )

        Catch ex As Exception

            ShowMessage(ex.Message)

        End Try

    End Sub

    '========================================
    ' تولید فایل Excel
    '========================================

    Private Sub ExportDataTableToExcel(ByVal dt As DataTable,
                                       ByVal fileName As String)

        Dim html As New StringBuilder()

        html.Append("<html><head>")
        html.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />")
        html.Append("</head>")
        html.Append("<body dir='rtl'>")

        html.Append("<table border='1' cellpadding='6' cellspacing='0'>")

        html.Append("<tr style='background-color:#eaf0f7;font-weight:bold;'>")

        For Each col As DataColumn In dt.Columns

            html.Append("<th>")
            html.Append(Server.HtmlEncode(col.ColumnName))
            html.Append("</th>")

        Next

        html.Append("</tr>")

        For Each row As DataRow In dt.Rows

            html.Append("<tr>")

            For Each col As DataColumn In dt.Columns

                html.Append("<td>")

                If Not IsDBNull(row(col)) Then
                    html.Append(Server.HtmlEncode(Convert.ToString(row(col))))
                End If

                html.Append("</td>")

            Next

            html.Append("</tr>")

        Next

        html.Append("</table>")
        html.Append("</body></html>")

        Response.Clear()
        Response.Buffer = True
        Response.Charset = "utf-8"
        Response.ContentEncoding = Encoding.UTF8
        Response.BinaryWrite(Encoding.UTF8.GetPreamble())

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader(
            "Content-Disposition",
            "attachment;filename=" & fileName
        )

        Response.Write(html.ToString())
        Response.Flush()

        HttpContext.Current.ApplicationInstance.CompleteRequest()

    End Sub

End Class
