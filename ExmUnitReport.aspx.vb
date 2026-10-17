Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text
Imports System.IO

Partial Class ExmUnitReport
    Inherits System.Web.UI.Page

    Private _messageType As String = "info"

    ' ============================================================
    ' بارگذاری صفحه
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUnits()
            LoadReport()
            LoadMatrixReport()
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
    ' تبدیل تاریخ شمسی به میلادی
    ' ============================================================
    Private Function ConvertPersianDateToGregorian(ByVal persianDate As String) As String
        Try
            If String.IsNullOrEmpty(persianDate) Then
                Return ""
            End If

            Dim parts() As String = persianDate.Split("/"c)
            If parts.Length <> 3 Then
                Return ""
            End If

            Dim year As Integer = Convert.ToInt32(parts(0))
            Dim month As Integer = Convert.ToInt32(parts(1))
            Dim day As Integer = Convert.ToInt32(parts(2))

            Dim pc As New PersianCalendar()
            Dim dt As DateTime = pc.ToDateTime(year, month, day, 0, 0, 0, 0)

            Return dt.ToString("yyyy-MM-dd")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' بارگذاری لیست واحدها
    ' ============================================================
    Private Sub LoadUnits()
        Try
            Dim sql As String = "SELECT ID, UnitName FROM exmUnits ORDER BY UnitName"
            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            ddlUnit.Items.Clear()
            ddlUnit.Items.Add(New ListItem("-- همه واحدها --", "0"))

            For Each row As DataRow In dt.Rows
                ddlUnit.Items.Add(New ListItem(row("UnitName").ToString(), row("ID").ToString()))
            Next

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری واحدها: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ بارگذاری گزارش اصلی (اصلاح‌شده)
    ' ============================================================
    Private Sub LoadReport()
        Try
            Dim unitFilter As String = ddlUnit.SelectedValue
            Dim statusFilter As String = ddlStatus.SelectedValue
            Dim dateFrom As String = txtDateFrom.Text.Trim()
            Dim dateTo As String = txtDateTo.Text.Trim()

            Dim dateFromGregorian As String = ""
            Dim dateToGregorian As String = ""

            If Not String.IsNullOrEmpty(dateFrom) Then
                dateFromGregorian = ConvertPersianDateToGregorian(dateFrom)
            End If

            If Not String.IsNullOrEmpty(dateTo) Then
                dateToGregorian = ConvertPersianDateToGregorian(dateTo)
            End If

            Dim sql As New StringBuilder()

            ' ============================================================
            ' ✅ اصلاح: ظرفیت و رزرو فقط در بازه تاریخ فیلتر
            ' ============================================================
            sql.AppendLine("SELECT ")
            sql.AppendLine("    u.ID AS UnitID, ")
            sql.AppendLine("    u.UnitName, ")
            sql.AppendLine("    u.IsActive, ")
            sql.AppendLine("    ISNULL(TotalCap.TotalCapacity, 0) AS TotalCapacity, ")
            sql.AppendLine("    ISNULL(TotalRes.TotalReserved, 0) AS TotalReserved, ")
            sql.AppendLine("    CASE WHEN ISNULL(TotalCap.TotalCapacity, 0) - ISNULL(TotalRes.TotalReserved, 0) < 0 ")
            sql.AppendLine("         THEN 0 ")
            sql.AppendLine("         ELSE ISNULL(TotalCap.TotalCapacity, 0) - ISNULL(TotalRes.TotalReserved, 0) ")
            sql.AppendLine("    END AS TotalAvailable ")
            sql.AppendLine("FROM exmUnits u ")

            ' ===== زیرکوئری ظرفیت =====
            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT uc.UnitID, SUM(uc.Capacity) AS TotalCapacity ")
            sql.AppendLine("    FROM exmUnitCapacity uc ")
            sql.AppendLine("    INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID ")
            sql.AppendLine("    WHERE d.IsActive = 1 ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sql.AppendLine("        AND d.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sql.AppendLine("        AND d.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sql.AppendLine("    GROUP BY uc.UnitID ")
            sql.AppendLine(") AS TotalCap ON u.ID = TotalCap.UnitID ")

            ' ===== زیرکوئری رزرو =====
            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT r.UnitID, COUNT(*) AS TotalReserved ")
            sql.AppendLine("    FROM exmReservationRequests r ")
            sql.AppendLine("    WHERE r.StatusCode NOT IN (6, 7, 8) ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sql.AppendLine("        AND r.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sql.AppendLine("        AND r.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sql.AppendLine("    GROUP BY r.UnitID ")
            sql.AppendLine(") AS TotalRes ON u.ID = TotalRes.UnitID ")
            sql.AppendLine("WHERE 1=1 ")

            If unitFilter <> "0" Then
                sql.AppendLine("    AND u.ID = " & unitFilter)
            End If

            If statusFilter = "1" Then
                sql.AppendLine("    AND u.IsActive = 1")
            ElseIf statusFilter = "2" Then
                sql.AppendLine("    AND u.IsActive = 0")
            End If

            sql.AppendLine("ORDER BY u.UnitName ASC")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            ' ===== اضافه کردن ستون‌های محاسباتی =====
            If Not dt.Columns.Contains("RowNumber") Then
                dt.Columns.Add("RowNumber", GetType(Integer))
            End If
            If Not dt.Columns.Contains("FillPercent") Then
                dt.Columns.Add("FillPercent", GetType(Integer))
            End If
            If Not dt.Columns.Contains("ExamDates") Then
                dt.Columns.Add("ExamDates", GetType(String))
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("RowNumber") = i + 1

                Dim totalCap As Integer = 0
                Dim totalRes As Integer = 0

                If dt.Rows(i)("TotalCapacity") IsNot DBNull.Value Then
                    totalCap = Convert.ToInt32(dt.Rows(i)("TotalCapacity"))
                End If

                If dt.Rows(i)("TotalReserved") IsNot DBNull.Value Then
                    totalRes = Convert.ToInt32(dt.Rows(i)("TotalReserved"))
                End If

                ' ===== محاسبه درصد =====
                Dim fillPercent As Integer = 0
                If totalCap > 0 Then
                    fillPercent = CInt((CDbl(totalRes) / CDbl(totalCap)) * 100)
                End If

                ' ===== محدود کردن به 0-100 =====
                If fillPercent > 100 Then fillPercent = 100
                If fillPercent < 0 Then fillPercent = 0

                dt.Rows(i)("FillPercent") = fillPercent

                ' ===== باقیمانده امن =====
                Dim available As Integer = totalCap - totalRes
                If available < 0 Then available = 0

                dt.Rows(i)("TotalAvailable") = available

                Dim unitID As Integer = Convert.ToInt32(dt.Rows(i)("UnitID"))
                dt.Rows(i)("ExamDates") = GetUnitExamDates(unitID, dateFromGregorian, dateToGregorian)
            Next

            gvReport.DataSource = dt
            gvReport.DataBind()

            UpdateSummary(dt)

            If dt.Rows.Count = 0 Then
                ShowMessage("ℹ️ هیچ داده‌ای برای نمایش وجود ندارد", "info")
                pnlSummary.Visible = False
            Else
                pnlSummary.Visible = True
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری گزارش: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ بارگذاری گزارش ماتریسی (تاریخ × واحد)
    ' ============================================================
    ' ============================================================
    ' ✅ بارگذاری گزارش ماتریسی (تاریخ × واحد) - فقط واحدهای فعال
    ' ============================================================
    Private Sub LoadMatrixReport()
        Try
            Dim dateFrom As String = txtDateFrom.Text.Trim()
            Dim dateTo As String = txtDateTo.Text.Trim()

            Dim dateFromGregorian As String = ""
            Dim dateToGregorian As String = ""

            If Not String.IsNullOrEmpty(dateFrom) Then
                dateFromGregorian = ConvertPersianDateToGregorian(dateFrom)
            End If

            If Not String.IsNullOrEmpty(dateTo) Then
                dateToGregorian = ConvertPersianDateToGregorian(dateTo)
            End If

            ' ============================================================
            ' مرحله ۱: پیدا کردن واحدهایی که در بازه تاریخ درگیر هستند
            ' (یا ظرفیت دارند یا رزرو دارند)
            ' ============================================================
            Dim sqlActiveUnits As New StringBuilder()
            sqlActiveUnits.AppendLine("SELECT DISTINCT u.ID, u.UnitName ")
            sqlActiveUnits.AppendLine("FROM exmUnits u ")
            sqlActiveUnits.AppendLine("WHERE u.IsActive = 1 ")
            sqlActiveUnits.AppendLine("  AND ( ")
            sqlActiveUnits.AppendLine("      EXISTS ( ")
            sqlActiveUnits.AppendLine("          SELECT 1 FROM exmUnitCapacity uc ")
            sqlActiveUnits.AppendLine("          INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID ")
            sqlActiveUnits.AppendLine("          WHERE uc.UnitID = u.ID ")
            sqlActiveUnits.AppendLine("            AND uc.Capacity > 0 ")
            sqlActiveUnits.AppendLine("            AND d.IsActive = 1 ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sqlActiveUnits.AppendLine("            AND d.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sqlActiveUnits.AppendLine("            AND d.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sqlActiveUnits.AppendLine("      ) ")
            sqlActiveUnits.AppendLine("      OR ")
            sqlActiveUnits.AppendLine("      EXISTS ( ")
            sqlActiveUnits.AppendLine("          SELECT 1 FROM exmReservationRequests r ")
            sqlActiveUnits.AppendLine("          WHERE r.UnitID = u.ID ")
            sqlActiveUnits.AppendLine("            AND r.StatusCode NOT IN (6, 7, 8) ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sqlActiveUnits.AppendLine("            AND r.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sqlActiveUnits.AppendLine("            AND r.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sqlActiveUnits.AppendLine("      ) ")
            sqlActiveUnits.AppendLine("  ) ")
            sqlActiveUnits.AppendLine("ORDER BY u.UnitName")

            Dim dtUnits As DataTable = ClsDataTable.GetDataTable(sqlActiveUnits.ToString())

            If dtUnits.Rows.Count = 0 Then
                pnlMatrix.Visible = False
                Return
            End If

            ' ============================================================
            ' مرحله ۲: کوئری ماتریسی
            ' ============================================================
            Dim sql As New StringBuilder()

            sql.AppendLine("SELECT ")
            sql.AppendLine("    d.ExaminationDate, ")
            sql.AppendLine("    dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, ")

            ' ===== ستون‌های داینامیک برای واحدهای فعال =====
            For Each unitRow As DataRow In dtUnits.Rows
                Dim unitID As Integer = Convert.ToInt32(unitRow("ID"))

                Dim aliasCapacity As String = "Unit_" & unitID & "_Capacity"
                sql.AppendLine("    ISNULL(( ")
                sql.AppendLine("        SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc ")
                sql.AppendLine("        WHERE uc.CapacityID = d.ID AND uc.UnitID = " & unitID)
                sql.AppendLine("    ), 0) AS " & aliasCapacity & ", ")

                Dim aliasReserved As String = "Unit_" & unitID & "_Reserved"
                sql.AppendLine("    ISNULL(( ")
                sql.AppendLine("        SELECT COUNT(*) FROM exmReservationRequests r ")
                sql.AppendLine("        WHERE r.ExaminationDate = d.ExaminationDate ")
                sql.AppendLine("          AND r.UnitID = " & unitID)
                sql.AppendLine("          AND r.StatusCode NOT IN (6, 7, 8) ")
                sql.AppendLine("    ), 0) AS " & aliasReserved & ", ")
            Next

            ' ===== جمع کل =====
            sql.AppendLine("    ISNULL(( ")
            sql.AppendLine("        SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc ")
            sql.AppendLine("        WHERE uc.CapacityID = d.ID ")
            sql.AppendLine("    ), 0) AS TotalCapacity, ")
            sql.AppendLine("    ISNULL(( ")
            sql.AppendLine("        SELECT COUNT(*) FROM exmReservationRequests r ")
            sql.AppendLine("        WHERE r.ExaminationDate = d.ExaminationDate ")
            sql.AppendLine("          AND r.StatusCode NOT IN (6, 7, 8) ")
            sql.AppendLine("    ), 0) AS TotalReserved ")

            sql.AppendLine("FROM exmDailyCapacity d ")
            sql.AppendLine("WHERE d.IsActive = 1 ")

            ' ===== فقط تاریخ‌هایی که حداقل یک واحد درگیر دارد =====
            sql.AppendLine("  AND ( ")
            sql.AppendLine("      EXISTS ( ")
            sql.AppendLine("          SELECT 1 FROM exmUnitCapacity uc2 ")
            sql.AppendLine("          INNER JOIN exmUnits u2 ON u2.ID = uc2.UnitID ")
            sql.AppendLine("          WHERE uc2.CapacityID = d.ID ")
            sql.AppendLine("            AND uc2.Capacity > 0 ")
            sql.AppendLine("            AND u2.IsActive = 1 ")
            sql.AppendLine("      ) ")
            sql.AppendLine("      OR ")
            sql.AppendLine("      EXISTS ( ")
            sql.AppendLine("          SELECT 1 FROM exmReservationRequests r2 ")
            sql.AppendLine("          WHERE r2.ExaminationDate = d.ExaminationDate ")
            sql.AppendLine("            AND r2.StatusCode NOT IN (6, 7, 8) ")
            sql.AppendLine("      ) ")
            sql.AppendLine("  ) ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sql.AppendLine("    AND d.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sql.AppendLine("    AND d.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sql.AppendLine("ORDER BY d.ExaminationDate")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            If dt.Rows.Count = 0 Then
                pnlMatrix.Visible = False
                Return
            End If

            ' ============================================================
            ' مرحله ۳: ساخت DataTable نهایی
            ' ============================================================
            Dim dtMatrix As New DataTable()
            dtMatrix.Columns.Add("RowNumber", GetType(Integer))
            dtMatrix.Columns.Add("PersianDate", GetType(String))
            dtMatrix.Columns.Add("StatsType", GetType(String))

            For Each unitRow As DataRow In dtUnits.Rows
                dtMatrix.Columns.Add("Unit_" & unitRow("ID").ToString(), GetType(String))
            Next
            dtMatrix.Columns.Add("Total", GetType(String))

            ' ===== پر کردن =====
            Dim rowNum As Integer = 1
            For Each dataRow As DataRow In dt.Rows

                ' ===== ردیف تعیین شده =====
                Dim rowAssigned As DataRow = dtMatrix.NewRow()
                rowAssigned("RowNumber") = rowNum
                rowAssigned("PersianDate") = dataRow("PersianDate").ToString()
                rowAssigned("StatsType") = "تعیین شده"

                For Each unitRow As DataRow In dtUnits.Rows
                    Dim unitID As Integer = Convert.ToInt32(unitRow("ID"))
                    Dim colName As String = "Unit_" & unitID.ToString()
                    Dim srcCol As String = "Unit_" & unitID.ToString() & "_Capacity"

                    Dim val As Integer = 0
                    If dataRow.Table.Columns.Contains(srcCol) AndAlso dataRow(srcCol) IsNot DBNull.Value Then
                        val = Convert.ToInt32(dataRow(srcCol))
                    End If

                    ' ===== اگر صفر بود، خالی بگذار =====
                    If val = 0 Then
                        rowAssigned(colName) = "-"
                    Else
                        rowAssigned(colName) = val.ToString()
                    End If
                Next

                Dim totalCap As Integer = Convert.ToInt32(dataRow("TotalCapacity"))
                rowAssigned("Total") = If(totalCap = 0, "-", totalCap.ToString())
                dtMatrix.Rows.Add(rowAssigned)

                ' ===== ردیف رزرو شده =====
                Dim rowReserved As DataRow = dtMatrix.NewRow()
                rowReserved("RowNumber") = rowNum
                rowReserved("PersianDate") = ""
                rowReserved("StatsType") = "رزرو شده"

                For Each unitRow As DataRow In dtUnits.Rows
                    Dim unitID As Integer = Convert.ToInt32(unitRow("ID"))
                    Dim colName As String = "Unit_" & unitID.ToString()
                    Dim srcCol As String = "Unit_" & unitID.ToString() & "_Reserved"

                    Dim val As Integer = 0
                    If dataRow.Table.Columns.Contains(srcCol) AndAlso dataRow(srcCol) IsNot DBNull.Value Then
                        val = Convert.ToInt32(dataRow(srcCol))
                    End If

                    If val = 0 Then
                        rowReserved(colName) = "-"
                    Else
                        rowReserved(colName) = val.ToString()
                    End If
                Next

                Dim totalRes As Integer = Convert.ToInt32(dataRow("TotalReserved"))
                rowReserved("Total") = If(totalRes = 0, "-", totalRes.ToString())
                dtMatrix.Rows.Add(rowReserved)

                rowNum += 1
            Next

            ' ============================================================
            ' مرحله ۴: حذف ستون‌هایی که در همه ردیف‌ها خالی هستند
            ' ============================================================
            Dim columnsToRemove As New List(Of String)()

            For Each unitRow As DataRow In dtUnits.Rows
                Dim colName As String = "Unit_" & unitRow("ID").ToString()
                Dim hasData As Boolean = False

                For Each matrixRow As DataRow In dtMatrix.Rows
                    If matrixRow(colName).ToString() <> "-" AndAlso matrixRow(colName).ToString() <> "" Then
                        hasData = True
                        Exit For
                    End If
                Next

                If Not hasData Then
                    columnsToRemove.Add(colName)
                End If
            Next

            ' ===== حذف ستون‌های خالی از DataTable =====
            For Each colName As String In columnsToRemove
                If dtMatrix.Columns.Contains(colName) Then
                    dtMatrix.Columns.Remove(colName)
                End If
            Next

            ' ===== حذف واحدهای خالی از dtUnits برای هدر =====
            Dim dtUnitsFiltered As DataTable = dtUnits.Clone()
            For Each unitRow As DataRow In dtUnits.Rows
                Dim colName As String = "Unit_" & unitRow("ID").ToString()
                If dtMatrix.Columns.Contains(colName) Then
                    dtUnitsFiltered.ImportRow(unitRow)
                End If
            Next

            ' ============================================================
            ' مرحله ۵: ذخیره و بایند
            ' ============================================================
            ViewState("MatrixData") = dtMatrix
            ViewState("MatrixUnits") = dtUnitsFiltered

            rptMatrixHeader.DataSource = dtUnitsFiltered
            rptMatrixHeader.DataBind()

            rptMatrixBody.DataSource = dtMatrix
            rptMatrixBody.DataBind()

            If dtMatrix.Rows.Count = 0 Then
                pnlMatrix.Visible = False
            Else
                pnlMatrix.Visible = True
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری گزارش ماتریسی: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ تولید HTML ردیف‌های ماتریس
    ' ============================================================
    Protected Function GetMatrixRowHtml(ByVal dataItem As Object, ByVal rowType As String) As String
        Try
            Dim drv As DataRowView = CType(dataItem, DataRowView)

            Dim rowNumber As String = drv("RowNumber").ToString()
            Dim persianDate As String = drv("PersianDate").ToString()
            Dim statsType As String = drv("StatsType").ToString()

            Dim dtUnits As DataTable = CType(ViewState("MatrixUnits"), DataTable)
            If dtUnits Is Nothing Then Return ""

            Dim sb As New StringBuilder()

            ' ===== ردیف اول (تعیین شده) با rowspan =====
            If statsType = "تعیین شده" Then
                sb.Append("<tr class='assigned-row'>")
                sb.Append("<td rowspan='2' style='background:#fff3cd;font-weight:700;color:#856404;vertical-align:middle;text-align:center;'>" & rowNumber & "</td>")
                sb.Append("<td rowspan='2' class='date-cell'>" & persianDate & "</td>")
                sb.Append("<td><span class='stat-badge badge-assigned'>" & statsType & "</span></td>")
            Else
                sb.Append("<tr class='reserved-row'>")
                sb.Append("<td><span class='stat-badge badge-reserved'>" & statsType & "</span></td>")
            End If

            ' ===== ستون‌های واحدها =====
            For Each unitRow As DataRow In dtUnits.Rows
                Dim unitID As String = unitRow("ID").ToString()
                Dim colName As String = "Unit_" & unitID

                Dim cellValue As String = "0"
                If drv.DataView.Table.Columns.Contains(colName) Then
                    cellValue = drv(colName).ToString()
                End If

                sb.Append("<td>" & cellValue & "</td>")
            Next

            ' ===== ستون جمع کل =====
            Dim totalValue As String = "0"
            If drv.DataView.Table.Columns.Contains("Total") Then
                totalValue = drv("Total").ToString()
            End If
            sb.Append("<td class='total-col'>" & totalValue & "</td>")

            sb.Append("</tr>")

            Return sb.ToString()

        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' RowCommand
    ' ============================================================
    Protected Sub gvReport_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvReport.RowCommand
        Try
            If e.CommandName = "ShowDetails" Then
                Dim unitID As Integer = Convert.ToInt32(e.CommandArgument)
                Session("ExmUnitDetails_From") = "2"
                Response.Redirect("ExmUnitDetails.aspx?UnitID=" & unitID)
            End If
        Catch ex As Exception
            ShowMessage("خطا در نمایش جزئیات: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ دریافت تاریخ‌های معاینه واحد (اصلاح‌شده)
    ' ============================================================
    Private Function GetUnitExamDates(ByVal unitID As Integer, _
                                       ByVal dateFromGregorian As String, _
                                       ByVal dateToGregorian As String) As String
        Try
            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT TOP 3 dbo.FormatDateFa(d.ExaminationDate) AS PersianDate ")
            sql.AppendLine("FROM exmDailyCapacity d ")
            sql.AppendLine("INNER JOIN exmUnitCapacity uc ON d.ID = uc.CapacityID ")
            sql.AppendLine("WHERE uc.UnitID = " & unitID & " ")
            sql.AppendLine("  AND d.IsActive = 1 ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sql.AppendLine("  AND d.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sql.AppendLine("  AND d.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sql.AppendLine("ORDER BY d.ExaminationDate DESC")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            Dim dates As New List(Of String)()
            For Each row As DataRow In dt.Rows
                dates.Add(row("PersianDate").ToString())
            Next

            If dates.Count = 0 Then
                Return "-"
            End If

            Return String.Join(" | ", dates.ToArray())
        Catch ex As Exception
            Return "-"
        End Try
    End Function

    ' ============================================================
    ' ✅ به‌روزرسانی کارت‌های آماری (اصلاح‌شده)
    ' ============================================================
    Private Sub UpdateSummary(ByVal dt As DataTable)
        Try
            Dim totalUnits As Integer = dt.Rows.Count
            Dim totalCapacity As Integer = 0
            Dim totalReserved As Integer = 0
            Dim totalAvailable As Integer = 0
            Dim fullUnits As Integer = 0

            For Each row As DataRow In dt.Rows
                Dim cap As Integer = 0
                Dim res As Integer = 0

                If row("TotalCapacity") IsNot DBNull.Value Then
                    cap = Convert.ToInt32(row("TotalCapacity"))
                End If

                If row("TotalReserved") IsNot DBNull.Value Then
                    res = Convert.ToInt32(row("TotalReserved"))
                End If

                totalCapacity += cap
                totalReserved += res

                ' ===== باقیمانده امن =====
                Dim available As Integer = cap - res
                If available < 0 Then available = 0
                totalAvailable += available

                ' ===== شمارش واحدهای پر =====
                If cap > 0 AndAlso res >= cap Then
                    fullUnits += 1
                End If
            Next

            lblTotalUnits.Text = totalUnits.ToString()
            lblTotalCapacity.Text = totalCapacity.ToString()
            lblTotalReserved.Text = totalReserved.ToString()
            lblTotalAvailable.Text = totalAvailable.ToString()
            lblFullUnits.Text = fullUnits.ToString()

        Catch ex As Exception
        End Try
    End Sub

    ' ============================================================
    ' کلاس نوار ظرفیت
    ' ============================================================
    Protected Function GetCapacityBarClass(ByVal fillPercent As Object) As String
        Try
            Dim percent As Integer = Convert.ToInt32(fillPercent)

            If percent >= 90 Then
                Return "capacity-bar-fill danger"
            ElseIf percent >= 70 Then
                Return "capacity-bar-fill warning"
            Else
                Return "capacity-bar-fill"
            End If
        Catch ex As Exception
            Return "capacity-bar-fill"
        End Try
    End Function

    ' ============================================================
    ' جستجو
    ' ============================================================
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        gvReport.PageIndex = 0
        LoadReport()
        LoadMatrixReport()
    End Sub

    ' ============================================================
    ' صفحه‌بندی
    ' ============================================================
    Protected Sub gvReport_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvReport.PageIndexChanging
        gvReport.PageIndex = e.NewPageIndex
        LoadReport()
    End Sub

    ' ============================================================
    ' خروجی اکسل
    ' ============================================================
    'Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportExcel.Click
    '    Try
    '        Dim sql As New StringBuilder()

    '        sql.AppendLine("SELECT ")
    '        sql.AppendLine("    u.UnitName AS [نام واحد], ")
    '        sql.AppendLine("    CASE WHEN u.IsActive = 1 THEN N'فعال' ELSE N'غیرفعال' END AS [وضعیت], ")
    '        sql.AppendLine("    ISNULL(SUM(uc.Capacity), 0) AS [کل ظرفیت], ")
    '        sql.AppendLine("    ISNULL(( ")
    '        sql.AppendLine("        SELECT COUNT(*) FROM exmReservationRequests r ")
    '        sql.AppendLine("        WHERE r.UnitID = u.ID ")
    '        sql.AppendLine("          AND r.StatusCode NOT IN (6, 7, 8) ")
    '        sql.AppendLine("    ), 0) AS [رزرو شده], ")
    '        sql.AppendLine("    ISNULL(SUM(uc.Capacity), 0) - ISNULL(( ")
    '        sql.AppendLine("        SELECT COUNT(*) FROM exmReservationRequests r ")
    '        sql.AppendLine("        WHERE r.UnitID = u.ID ")
    '        sql.AppendLine("          AND r.StatusCode NOT IN (6, 7, 8) ")
    '        sql.AppendLine("    ), 0) AS [باقیمانده] ")
    '        sql.AppendLine("FROM exmUnits u ")
    '        sql.AppendLine("LEFT JOIN exmUnitCapacity uc ON u.ID = uc.UnitID ")
    '        sql.AppendLine("GROUP BY u.ID, u.UnitName, u.IsActive ")
    '        sql.AppendLine("ORDER BY u.UnitName")

    '        Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

    '        Response.Clear()
    '        Response.Buffer = True
    '        Response.AddHeader("content-disposition", "attachment;filename=ExmUnitReport.xls")
    '        Response.Charset = "utf-8"
    '        Response.ContentType = "application/vnd.ms-excel"

    '        Dim sb As New StringBuilder()

    '        sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>")
    '        sb.Append("<head><meta charset='utf-8' />")
    '        sb.Append("<style>")
    '        sb.Append("table { border-collapse: collapse; direction: rtl; font-family: Tahoma; }")
    '        sb.Append("th { background: #2c3e50; color: white; padding: 8px 12px; border: 1px solid #ddd; text-align: center; }")
    '        sb.Append("td { padding: 8px 12px; border: 1px solid #ddd; text-align: center; }")
    '        sb.Append("</style>")
    '        sb.Append("</head><body>")

    '        sb.Append("<h3>گزارش آماری واحدهای معاینات</h3>")
    '        sb.Append("<table>")

    '        sb.Append("<tr>")
    '        For Each col As DataColumn In dt.Columns
    '            sb.Append("<th>" & col.ColumnName & "</th>")
    '        Next
    '        sb.Append("</tr>")

    '        For Each row As DataRow In dt.Rows
    '            sb.Append("<tr>")
    '            For Each col As DataColumn In dt.Columns
    '                sb.Append("<td>" & row(col.ColumnName).ToString() & "</td>")
    '            Next
    '            sb.Append("</tr>")
    '        Next

    '        sb.Append("</table>")
    '        sb.Append("</body></html>")

    '        Dim utf8Bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sb.ToString())
    '        Response.BinaryWrite(utf8Bytes)
    '        Response.End()

    '    Catch ex As Exception
    '        ShowMessage("خطا در خروجی اکسل: " & ex.Message, "danger")
    '    End Try
    'End Sub

    ' ============================================================
    ' بازگشت
    ' ============================================================

    ' ============================================================
    ' ✅ خروجی اکسل با دو شیت: کلی + ماتریسی
    ' ============================================================
    ' ============================================================
    ' ✅ خروجی اکسل با دو شیت: کلی + ماتریسی (اصلاح‌شده)
    ' ============================================================
    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim dateFrom As String = txtDateFrom.Text.Trim()
            Dim dateTo As String = txtDateTo.Text.Trim()

            Dim dateFromGregorian As String = ""
            Dim dateToGregorian As String = ""

            If Not String.IsNullOrEmpty(dateFrom) Then
                dateFromGregorian = ConvertPersianDateToGregorian(dateFrom)
            End If

            If Not String.IsNullOrEmpty(dateTo) Then
                dateToGregorian = ConvertPersianDateToGregorian(dateTo)
            End If

            ' ============================================================
            ' ساخت شیت ۱: گزارش آماری کلی
            ' ============================================================
            Dim sqlReport As New StringBuilder()
            sqlReport.AppendLine("SELECT ")
            sqlReport.AppendLine("    u.UnitName AS [نام واحد], ")
            sqlReport.AppendLine("    CASE WHEN u.IsActive = 1 THEN N'فعال' ELSE N'غیرفعال' END AS [وضعیت], ")
            sqlReport.AppendLine("    ISNULL(TotalCap.TotalCapacity, 0) AS [کل ظرفیت], ")
            sqlReport.AppendLine("    ISNULL(TotalRes.TotalReserved, 0) AS [رزرو شده], ")
            sqlReport.AppendLine("    CASE WHEN ISNULL(TotalCap.TotalCapacity, 0) - ISNULL(TotalRes.TotalReserved, 0) < 0 ")
            sqlReport.AppendLine("         THEN 0 ")
            sqlReport.AppendLine("         ELSE ISNULL(TotalCap.TotalCapacity, 0) - ISNULL(TotalRes.TotalReserved, 0) ")
            sqlReport.AppendLine("    END AS [باقیمانده] ")
            sqlReport.AppendLine("FROM exmUnits u ")
            sqlReport.AppendLine("LEFT JOIN ( ")
            sqlReport.AppendLine("    SELECT uc.UnitID, SUM(uc.Capacity) AS TotalCapacity ")
            sqlReport.AppendLine("    FROM exmUnitCapacity uc ")
            sqlReport.AppendLine("    INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID ")
            sqlReport.AppendLine("    WHERE d.IsActive = 1 ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sqlReport.AppendLine("      AND d.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sqlReport.AppendLine("      AND d.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sqlReport.AppendLine("    GROUP BY uc.UnitID ")
            sqlReport.AppendLine(") TotalCap ON u.ID = TotalCap.UnitID ")
            sqlReport.AppendLine("LEFT JOIN ( ")
            sqlReport.AppendLine("    SELECT r.UnitID, COUNT(*) AS TotalReserved ")
            sqlReport.AppendLine("    FROM exmReservationRequests r ")
            sqlReport.AppendLine("    WHERE r.StatusCode NOT IN (6, 7, 8) ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sqlReport.AppendLine("      AND r.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sqlReport.AppendLine("      AND r.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sqlReport.AppendLine("    GROUP BY r.UnitID ")
            sqlReport.AppendLine(") TotalRes ON u.ID = TotalRes.UnitID ")
            sqlReport.AppendLine("ORDER BY u.UnitName")

            Dim dtReport As DataTable = ClsDataTable.GetDataTable(sqlReport.ToString())

            ' ============================================================
            ' ساخت شیت ۲: گزارش ماتریسی
            ' ============================================================
            Dim sqlActiveUnits As New StringBuilder()
            sqlActiveUnits.AppendLine("SELECT DISTINCT u.ID, u.UnitName ")
            sqlActiveUnits.AppendLine("FROM exmUnits u ")
            sqlActiveUnits.AppendLine("WHERE u.IsActive = 1 ")
            sqlActiveUnits.AppendLine("  AND ( ")
            sqlActiveUnits.AppendLine("      EXISTS ( ")
            sqlActiveUnits.AppendLine("          SELECT 1 FROM exmUnitCapacity uc ")
            sqlActiveUnits.AppendLine("          INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID ")
            sqlActiveUnits.AppendLine("          WHERE uc.UnitID = u.ID AND uc.Capacity > 0 AND d.IsActive = 1 ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sqlActiveUnits.AppendLine("            AND d.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sqlActiveUnits.AppendLine("            AND d.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sqlActiveUnits.AppendLine("      ) ")
            sqlActiveUnits.AppendLine("      OR ")
            sqlActiveUnits.AppendLine("      EXISTS ( ")
            sqlActiveUnits.AppendLine("          SELECT 1 FROM exmReservationRequests r ")
            sqlActiveUnits.AppendLine("          WHERE r.UnitID = u.ID AND r.StatusCode NOT IN (6, 7, 8) ")

            If Not String.IsNullOrEmpty(dateFromGregorian) Then
                sqlActiveUnits.AppendLine("            AND r.ExaminationDate >= '" & dateFromGregorian & "'")
            End If

            If Not String.IsNullOrEmpty(dateToGregorian) Then
                sqlActiveUnits.AppendLine("            AND r.ExaminationDate <= '" & dateToGregorian & "'")
            End If

            sqlActiveUnits.AppendLine("      ) ")
            sqlActiveUnits.AppendLine("  ) ")
            sqlActiveUnits.AppendLine("ORDER BY u.UnitName")

            Dim dtUnits As DataTable = ClsDataTable.GetDataTable(sqlActiveUnits.ToString())

            Dim dtMatrixDisplay As New DataTable()

            If dtUnits.Rows.Count > 0 Then

                Dim sqlMatrix As New StringBuilder()
                sqlMatrix.AppendLine("SELECT ")
                sqlMatrix.AppendLine("    d.ExaminationDate, ")
                sqlMatrix.AppendLine("    dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, ")

                For Each unitRow As DataRow In dtUnits.Rows
                    Dim unitID As Integer = Convert.ToInt32(unitRow("ID"))

                    sqlMatrix.AppendLine("    ISNULL(( ")
                    sqlMatrix.AppendLine("        SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc ")
                    sqlMatrix.AppendLine("        WHERE uc.CapacityID = d.ID AND uc.UnitID = " & unitID)
                    sqlMatrix.AppendLine("    ), 0) AS Unit_" & unitID & "_Capacity, ")

                    sqlMatrix.AppendLine("    ISNULL(( ")
                    sqlMatrix.AppendLine("        SELECT COUNT(*) FROM exmReservationRequests r ")
                    sqlMatrix.AppendLine("        WHERE r.ExaminationDate = d.ExaminationDate ")
                    sqlMatrix.AppendLine("          AND r.UnitID = " & unitID)
                    sqlMatrix.AppendLine("          AND r.StatusCode NOT IN (6, 7, 8) ")
                    sqlMatrix.AppendLine("    ), 0) AS Unit_" & unitID & "_Reserved, ")
                Next

                sqlMatrix.AppendLine("    ISNULL((SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc WHERE uc.CapacityID = d.ID), 0) AS TotalCapacity, ")
                sqlMatrix.AppendLine("    ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE r.ExaminationDate = d.ExaminationDate AND r.StatusCode NOT IN (6, 7, 8)), 0) AS TotalReserved ")

                sqlMatrix.AppendLine("FROM exmDailyCapacity d ")
                sqlMatrix.AppendLine("WHERE d.IsActive = 1 ")
                sqlMatrix.AppendLine("  AND ( ")
                sqlMatrix.AppendLine("      EXISTS ( ")
                sqlMatrix.AppendLine("          SELECT 1 FROM exmUnitCapacity uc2 ")
                sqlMatrix.AppendLine("          INNER JOIN exmUnits u2 ON u2.ID = uc2.UnitID ")
                sqlMatrix.AppendLine("          WHERE uc2.CapacityID = d.ID AND uc2.Capacity > 0 AND u2.IsActive = 1 ")
                sqlMatrix.AppendLine("      ) ")
                sqlMatrix.AppendLine("      OR ")
                sqlMatrix.AppendLine("      EXISTS ( ")
                sqlMatrix.AppendLine("          SELECT 1 FROM exmReservationRequests r2 ")
                sqlMatrix.AppendLine("          WHERE r2.ExaminationDate = d.ExaminationDate ")
                sqlMatrix.AppendLine("            AND r2.StatusCode NOT IN (6, 7, 8) ")
                sqlMatrix.AppendLine("      ) ")
                sqlMatrix.AppendLine("  ) ")

                If Not String.IsNullOrEmpty(dateFromGregorian) Then
                    sqlMatrix.AppendLine("    AND d.ExaminationDate >= '" & dateFromGregorian & "'")
                End If

                If Not String.IsNullOrEmpty(dateToGregorian) Then
                    sqlMatrix.AppendLine("    AND d.ExaminationDate <= '" & dateToGregorian & "'")
                End If

                sqlMatrix.AppendLine("ORDER BY d.ExaminationDate")

                Dim dtRaw As DataTable = ClsDataTable.GetDataTable(sqlMatrix.ToString())

                dtMatrixDisplay.Columns.Add("ردیف", GetType(Integer))
                dtMatrixDisplay.Columns.Add("تاریخ معاینه", GetType(String))
                dtMatrixDisplay.Columns.Add("نوع آمار", GetType(String))

                For Each unitRow As DataRow In dtUnits.Rows
                    dtMatrixDisplay.Columns.Add(unitRow("UnitName").ToString(), GetType(String))
                Next
                dtMatrixDisplay.Columns.Add("جمع کل", GetType(String))

                Dim rowNum As Integer = 1
                For Each dataRow As DataRow In dtRaw.Rows

                    ' ===== ردیف تعیین شده =====
                    Dim rowAssigned As DataRow = dtMatrixDisplay.NewRow()
                    rowAssigned("ردیف") = rowNum
                    rowAssigned("تاریخ معاینه") = dataRow("PersianDate").ToString()
                    rowAssigned("نوع آمار") = "تعیین شده"

                    For Each unitRow As DataRow In dtUnits.Rows
                        Dim unitID As Integer = Convert.ToInt32(unitRow("ID"))
                        Dim colName As String = unitRow("UnitName").ToString()
                        Dim srcCol As String = "Unit_" & unitID & "_Capacity"

                        Dim val As Integer = 0
                        If dataRow.Table.Columns.Contains(srcCol) Then
                            val = Convert.ToInt32(dataRow(srcCol))
                        End If

                        rowAssigned(colName) = If(val = 0, "-", val.ToString())
                    Next

                    Dim totalCap As Integer = Convert.ToInt32(dataRow("TotalCapacity"))
                    rowAssigned("جمع کل") = If(totalCap = 0, "-", totalCap.ToString())
                    dtMatrixDisplay.Rows.Add(rowAssigned)

                    ' ===== ردیف رزرو شده =====
                    Dim rowReserved As DataRow = dtMatrixDisplay.NewRow()
                    rowReserved("ردیف") = rowNum
                    rowReserved("تاریخ معاینه") = ""
                    rowReserved("نوع آمار") = "رزرو شده"

                    For Each unitRow As DataRow In dtUnits.Rows
                        Dim unitID As Integer = Convert.ToInt32(unitRow("ID"))
                        Dim colName As String = unitRow("UnitName").ToString()
                        Dim srcCol As String = "Unit_" & unitID & "_Reserved"

                        Dim val As Integer = 0
                        If dataRow.Table.Columns.Contains(srcCol) Then
                            val = Convert.ToInt32(dataRow(srcCol))
                        End If

                        rowReserved(colName) = If(val = 0, "-", val.ToString())
                    Next

                    Dim totalRes As Integer = Convert.ToInt32(dataRow("TotalReserved"))
                    rowReserved("جمع کل") = If(totalRes = 0, "-", totalRes.ToString())
                    dtMatrixDisplay.Rows.Add(rowReserved)

                    rowNum += 1
                Next

                ' ===== حذف ستون‌های خالی =====
                Dim colsToRemove As New List(Of String)()
                For Each unitRow As DataRow In dtUnits.Rows
                    Dim colName As String = unitRow("UnitName").ToString()
                    Dim hasData As Boolean = False

                    For Each matrixRow As DataRow In dtMatrixDisplay.Rows
                        If matrixRow(colName).ToString() <> "-" AndAlso matrixRow(colName).ToString() <> "" Then
                            hasData = True
                            Exit For
                        End If
                    Next

                    If Not hasData Then
                        colsToRemove.Add(colName)
                    End If
                Next

                For Each colName As String In colsToRemove
                    If dtMatrixDisplay.Columns.Contains(colName) Then
                        dtMatrixDisplay.Columns.Remove(colName)
                    End If
                Next

            End If

            ' ============================================================
            ' ✅ نام فایل با تاریخ شمسی
            ' ============================================================
            Dim pc As New PersianCalendar()
            Dim now As DateTime = DateTime.Now
            Dim persianDateForFile As String = pc.GetYear(now).ToString() & "-" & _
                                               pc.GetMonth(now).ToString().PadLeft(2, "0"c) & "-" & _
                                               pc.GetDayOfMonth(now).ToString().PadLeft(2, "0"c) & "_" & _
                                               now.Hour.ToString().PadLeft(2, "0"c) & "-" & _
                                               now.Minute.ToString().PadLeft(2, "0"c)

            Dim fileName As String = "Gozaresh_ExmUnits_" & persianDateForFile & ".xls"

            ' ============================================================
            ' ساخت فایل Excel (HTML-based)
            ' ============================================================
            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
            Response.ContentType = "application/vnd.ms-excel"

            Dim sb As New StringBuilder()

            sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' ")
            sb.Append("xmlns:x='urn:schemas-microsoft-com:office:excel' ")
            sb.Append("xmlns='http://www.w3.org/TR/REC-html40'>")
            sb.Append("<head>")
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />")
            sb.Append("<!--[if gte mso 9]><xml>")
            sb.Append("<x:ExcelWorkbook><x:ExcelWorksheets>")

            ' ===== شیت ۱ =====
            sb.Append("<x:ExcelWorksheet>")
            sb.Append("<x:Name>گزارش کلی</x:Name>")
            sb.Append("<x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions>")
            sb.Append("</x:ExcelWorksheet>")

            ' ===== شیت ۲ =====
            If dtMatrixDisplay.Columns.Count > 0 Then
                sb.Append("<x:ExcelWorksheet>")
                sb.Append("<x:Name>گزارش ماتریسی</x:Name>")
                sb.Append("<x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions>")
                sb.Append("</x:ExcelWorksheet>")
            End If

            sb.Append("</x:ExcelWorksheets></x:ExcelWorkbook>")
            sb.Append("</xml><![endif]-->")

            ' ============================================================
            ' ✅ CSS اصلاح‌شده: نوع آمار پررنگ
            ' ============================================================
            sb.Append("<style type='text/css'>")
            sb.Append("table { border-collapse: collapse; direction: rtl; font-family: Tahoma; }")
            sb.Append("th { background-color: #2c3e50; color: #FFFFFF; padding: 8px 12px; border: 1px solid #ddd; text-align: center; font-weight: bold; font-size: 12pt; }")
            sb.Append("td { padding: 8px 12px; border: 1px solid #ddd; text-align: center; font-size: 11pt; color: #000000; }")
            sb.Append(".title { font-size: 16pt; font-weight: bold; text-align: center; padding: 12px; background-color: #f0f0f0; color: #2c3e50; }")
            sb.Append(".subtitle { font-size: 11pt; text-align: center; color: #666666; padding: 8px; }")
            sb.Append(".date-cell { background-color: #FFF3CD; font-weight: bold; color: #856404; }")
            sb.Append(".assigned-row { background-color: #E8F4FD; }")
            sb.Append(".reserved-row { background-color: #D4EDDA; }")
            sb.Append(".total-col { background-color: #FDEBD0; font-weight: bold; color: #A04000; }")
            sb.Append(".stats-assigned { background-color: #3498DB; color: #FFFFFF; font-weight: bold; }")
            sb.Append(".stats-reserved { background-color: #27AE60; color: #FFFFFF; font-weight: bold; }")
            sb.Append("</style>")
            sb.Append("</head>")
            sb.Append("<body>")

            ' ============================================================
            ' شیت ۱: گزارش کلی
            ' ============================================================
            sb.Append("<div class='title'>📊 گزارش آماری واحدهای معاینات</div>")
            sb.Append("<br/>")
            sb.Append("<table>")

            ' ===== هدر =====
            sb.Append("<tr>")
            For Each col As DataColumn In dtReport.Columns
                sb.Append("<th>" & col.ColumnName & "</th>")
            Next
            sb.Append("</tr>")

            ' ===== داده‌ها =====
            For Each row As DataRow In dtReport.Rows
                sb.Append("<tr>")
                For Each col As DataColumn In dtReport.Columns
                    Dim val As String = If(row(col.ColumnName) Is DBNull.Value, "-", row(col.ColumnName).ToString())
                    sb.Append("<td>" & Server.HtmlEncode(val) & "</td>")
                Next
                sb.Append("</tr>")
            Next

            sb.Append("</table>")

            ' ============================================================
            ' شیت ۲: گزارش ماتریسی
            ' ============================================================
            If dtMatrixDisplay.Columns.Count > 0 Then

                sb.Append("<br pagebreak='true' />")
                sb.Append("<div class='title'>📊 گزارش تفکیکی تاریخ × واحد</div>")
                sb.Append("<div class='subtitle'>")
                sb.Append("تعیین شده = ظرفیت تعریف‌شده | رزرو شده = تعداد رزرو فعال | - = خالی")
                sb.Append("</div>")
                sb.Append("<br/>")
                sb.Append("<table>")

                ' ===== هدر =====
                sb.Append("<tr>")
                For Each col As DataColumn In dtMatrixDisplay.Columns
                    sb.Append("<th>" & col.ColumnName & "</th>")
                Next
                sb.Append("</tr>")

                ' ===== داده‌ها =====
                For Each row As DataRow In dtMatrixDisplay.Rows
                    Dim rowClass As String = ""
                    If row("نوع آمار").ToString() = "تعیین شده" Then
                        rowClass = " class='assigned-row'"
                    ElseIf row("نوع آمار").ToString() = "رزرو شده" Then
                        rowClass = " class='reserved-row'"
                    End If

                    sb.Append("<tr" & rowClass & ">")

                    For Each col As DataColumn In dtMatrixDisplay.Columns
                        Dim val As String = If(row(col.ColumnName) Is DBNull.Value, "-", row(col.ColumnName).ToString())
                        Dim cellClass As String = ""
                        Dim displayVal As String = ""

                        ' ============================================================
                        ' ✅ نوع آمار: پررنگ و بدون span
                        ' ============================================================
                        If col.ColumnName = "نوع آمار" Then
                            If val = "تعیین شده" Then
                                cellClass = " class='stats-assigned'"
                            ElseIf val = "رزرو شده" Then
                                cellClass = " class='stats-reserved'"
                            End If
                            displayVal = val  ' ← فقط متن ساده، بدون span

                            ' ============================================================
                            ' تاریخ: زرد پررنگ
                            ' ============================================================
                        ElseIf col.ColumnName = "تاریخ معاینه" AndAlso Not String.IsNullOrEmpty(val) Then
                            cellClass = " class='date-cell'"
                            displayVal = Server.HtmlEncode(val)

                            ' ============================================================
                            ' جمع کل: نارنجی
                            ' ============================================================
                        ElseIf col.ColumnName = "جمع کل" Then
                            cellClass = " class='total-col'"
                            displayVal = Server.HtmlEncode(val)

                            ' ============================================================
                            ' بقیه: عادی
                            ' ============================================================
                        Else
                            displayVal = Server.HtmlEncode(val)
                        End If

                        sb.Append("<td" & cellClass & ">" & displayVal & "</td>")
                    Next

                    sb.Append("</tr>")
                Next

                sb.Append("</table>")
            End If

            sb.Append("</body></html>")

            Dim utf8Bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sb.ToString())
            Response.BinaryWrite(utf8Bytes)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            ShowMessage("خطا در خروجی اکسل: " & ex.Message, "danger")
        End Try
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("ExaminationSetCapacity.aspx")
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        pnlMessage.Visible = True
        lblMessage.Text = message
        _messageType = type

        Select Case type
            Case "success"
                lblMessageIcon.Text = "✅ "
                divMessage.Attributes("class") = "rpt-alert rpt-alert-success"
            Case "danger"
                lblMessageIcon.Text = "❌ "
                divMessage.Attributes("class") = "rpt-alert rpt-alert-danger"
            Case "warning"
                lblMessageIcon.Text = "⚠️ "
                divMessage.Attributes("class") = "rpt-alert rpt-alert-warning"
            Case Else
                lblMessageIcon.Text = "ℹ️ "
                divMessage.Attributes("class") = "rpt-alert rpt-alert-info"
        End Select
    End Sub

    Public Function GetAlertClass() As String
        Return _messageType
    End Function

End Class