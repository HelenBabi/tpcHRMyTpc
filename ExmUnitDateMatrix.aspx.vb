Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text

Partial Class ExmUnitDateMatrix
    Inherits System.Web.UI.Page

    ' ============================================================
    ' Page Load
    ' ============================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUnits()

            Dim dateFrom As String = Request.QueryString("DateFrom")
            Dim dateTo As String = Request.QueryString("DateTo")
            Dim unitID As String = Request.QueryString("UnitID")
            Dim detailDate As String = Request.QueryString("DetailDate")

            If Not String.IsNullOrEmpty(dateFrom) Then txtDateFrom.Text = dateFrom
            If Not String.IsNullOrEmpty(dateTo) Then txtDateTo.Text = dateTo

            LoadMatrix()

            If Not String.IsNullOrEmpty(detailDate) Then
                ViewState("DetailDate") = detailDate
                LoadDateDetail(detailDate)
            End If

            If Not String.IsNullOrEmpty(unitID) AndAlso unitID <> "0" Then
                ViewState("DetailUnitID") = Convert.ToInt32(unitID)
                LoadUnitDetail(Convert.ToInt32(unitID))
            End If
        End If
    End Sub

    ' ============================================================
    ' بارگذاری لیست واحدها
    ' ============================================================
    Private Sub LoadUnits()
        Try
            Dim sql As String = "SELECT ID, UnitName FROM exmUnits WHERE IsActive = 1 ORDER BY UnitName"
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
    ' تبدیل تاریخ شمسی به میلادی
    ' ============================================================
    Private Function ConvertPersianDateToGregorian(ByVal persianDate As String) As String
        Try
            If String.IsNullOrEmpty(persianDate) Then Return ""
            Dim parts() As String = persianDate.Split("/"c)
            If parts.Length <> 3 Then Return ""
            Dim pc As New PersianCalendar()
            Dim dt As DateTime = pc.ToDateTime(Convert.ToInt32(parts(0)), Convert.ToInt32(parts(1)), Convert.ToInt32(parts(2)), 0, 0, 0, 0)
            Return dt.ToString("yyyy-MM-dd")
        Catch
            Return ""
        End Try
    End Function

    ' ============================================================
    ' ✅ تبدیل مطمئن تاریخ به میلادی (yyyy-MM-dd)
    ' ============================================================
    Private Function NormalizeToGregorian(ByVal input As String) As String
        Try
            If String.IsNullOrEmpty(input) Then Return ""

            Dim s As String = input.Trim()
            If s.Contains(" ") Then s = s.Split(" "c)(0)
            If s.Contains("T") Then s = s.Split("T"c)(0)

            Dim d As DateTime
            If DateTime.TryParseExact(s, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Return d.ToString("yyyy-MM-dd")
            End If

            If DateTime.TryParseExact(s, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Return d.ToString("yyyy-MM-dd")
            End If

            Dim parts() As String = s.Split("/"c, "-"c)
            If parts.Length = 3 Then
                Dim y As Integer = Convert.ToInt32(parts(0))
                If y >= 1300 AndAlso y <= 1500 Then
                    Dim pc As New PersianCalendar()
                    Dim g As DateTime = pc.ToDateTime(y, Convert.ToInt32(parts(1)), Convert.ToInt32(parts(2)), 0, 0, 0, 0)
                    Return g.ToString("yyyy-MM-dd")
                End If
            End If

            If DateTime.TryParse(s, d) Then
                Return d.ToString("yyyy-MM-dd")
            End If

            Return ""
        Catch
            Return ""
        End Try
    End Function

    ' ============================================================
    ' ✅ تعداد پرسنل فعال هر واحد (با OldUnitCode)
    ' ============================================================
    Private Function GetUnitPersonnelCounts(ByVal unitIDs As List(Of Integer)) As Dictionary(Of Integer, Integer)
        Dim result As New Dictionary(Of Integer, Integer)()
        Try
            If unitIDs Is Nothing OrElse unitIDs.Count = 0 Then Return result

            Dim idList As String = String.Join(",", unitIDs)

            Dim sql As String = "SELECT u.ID, ISNULL(p.Cnt, 0) AS Cnt " & _
                                "FROM exmUnits u " & _
                                "LEFT JOIN ( " & _
                                "    SELECT UnitsCode, COUNT(*) AS Cnt " & _
                                "    FROM personel " & _
                                "    WHERE ISNULL(CodeClosed, 0) = 0 " & _
                                "    GROUP BY UnitsCode " & _
                                ") p ON p.UnitsCode = u.OldUnitCode " & _
                                "WHERE u.ID IN (" & idList & ")"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql)

            For Each row As DataRow In dt.Rows
                If row("ID") IsNot DBNull.Value Then
                    Dim uid As Integer = Convert.ToInt32(row("ID"))
                    Dim cnt As Integer = 0
                    If row("Cnt") IsNot DBNull.Value Then cnt = Convert.ToInt32(row("Cnt"))
                    result(uid) = cnt
                End If
            Next
        Catch ex As Exception
            ShowMessage("خطا در دریافت تعداد پرسنل: " & ex.Message, "warning")
        End Try
        Return result
    End Function

    ' ============================================================
    ' ✅ بارگذاری ماتریس تاریخ × واحد
    ' ============================================================
    Private Sub LoadMatrix()
        Try
            Dim dateFromG As String = ConvertPersianDateToGregorian(txtDateFrom.Text.Trim())
            Dim dateToG As String = ConvertPersianDateToGregorian(txtDateTo.Text.Trim())
            Dim unitFilter As Integer = Convert.ToInt32(ddlUnit.SelectedValue)

            ' ===== مرحله ۱: واحدهای درگیر در بازه =====
            Dim sqlUnits As New StringBuilder()
            sqlUnits.AppendLine("SELECT DISTINCT u.ID, u.UnitName ")
            sqlUnits.AppendLine("FROM exmUnits u ")
            sqlUnits.AppendLine("WHERE u.IsActive = 1 ")
            If unitFilter > 0 Then sqlUnits.AppendLine("  AND u.ID = " & unitFilter)
            sqlUnits.AppendLine("  AND ( ")
            sqlUnits.AppendLine("      EXISTS (SELECT 1 FROM exmUnitCapacity uc ")
            sqlUnits.AppendLine("              INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID ")
            sqlUnits.AppendLine("              WHERE uc.UnitID = u.ID AND uc.Capacity > 0 AND d.IsActive = 1 ")
            If dateFromG <> "" Then sqlUnits.AppendLine("                AND d.ExaminationDate >= '" & dateFromG & "'")
            If dateToG <> "" Then sqlUnits.AppendLine("                AND d.ExaminationDate <= '" & dateToG & "'")
            sqlUnits.AppendLine("      ) ")
            sqlUnits.AppendLine("      OR ")
            sqlUnits.AppendLine("      EXISTS (SELECT 1 FROM exmReservationRequests r ")
            sqlUnits.AppendLine("              WHERE r.UnitID = u.ID AND r.StatusCode NOT IN (6,7,8) ")
            If dateFromG <> "" Then sqlUnits.AppendLine("                AND r.ExaminationDate >= '" & dateFromG & "'")
            If dateToG <> "" Then sqlUnits.AppendLine("                AND r.ExaminationDate <= '" & dateToG & "'")
            sqlUnits.AppendLine("      ) ")
            sqlUnits.AppendLine("  ) ")
            sqlUnits.AppendLine("ORDER BY u.UnitName")

            Dim dtUnits As DataTable = ClsDataTable.GetDataTable(sqlUnits.ToString())

            If dtUnits.Rows.Count = 0 Then
                pnlMatrix.Visible = False
                pnlCapacityAudit.Visible = False
                ShowMessage("ℹ️ هیچ داده‌ای برای نمایش وجود ندارد", "info")
                Return
            End If

            ' ===== مرحله ۲: کوئری اصلی ماتریس =====
            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT d.ID AS DailyID, d.ExaminationDate, dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, ")

            For Each uRow As DataRow In dtUnits.Rows
                Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                sql.AppendLine("  ISNULL((SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc WHERE uc.CapacityID = d.ID AND uc.UnitID = " & uid & "), 0) AS Unit_" & uid & "_Cap, ")
                sql.AppendLine("  ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, d.ExaminationDate) AND r.UnitID = " & uid & " AND r.StatusCode NOT IN (6,7,8)), 0) AS Unit_" & uid & "_Res, ")
            Next

            sql.AppendLine("  ISNULL((SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc WHERE uc.CapacityID = d.ID), 0) AS RowTotalCap, ")
            sql.AppendLine("  ISNULL((SELECT COUNT(*) FROM exmReservationRequests r WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, d.ExaminationDate) AND r.StatusCode NOT IN (6,7,8)), 0) AS RowTotalRes ")
            sql.AppendLine("FROM exmDailyCapacity d ")
            sql.AppendLine("WHERE d.IsActive = 1 ")
            sql.AppendLine("  AND ( ")
            sql.AppendLine("      EXISTS (SELECT 1 FROM exmUnitCapacity uc2 INNER JOIN exmUnits u2 ON u2.ID = uc2.UnitID ")
            sql.AppendLine("              WHERE uc2.CapacityID = d.ID AND uc2.Capacity > 0 AND u2.IsActive = 1) ")
            sql.AppendLine("      OR ")
            sql.AppendLine("      EXISTS (SELECT 1 FROM exmReservationRequests r2 ")
            sql.AppendLine("              WHERE CONVERT(date, r2.ExaminationDate) = CONVERT(date, d.ExaminationDate) AND r2.StatusCode NOT IN (6,7,8)) ")
            sql.AppendLine("  ) ")
            If dateFromG <> "" Then sql.AppendLine("  AND d.ExaminationDate >= '" & dateFromG & "'")
            If dateToG <> "" Then sql.AppendLine("  AND d.ExaminationDate <= '" & dateToG & "'")
            sql.AppendLine("ORDER BY d.ExaminationDate")

            Dim dtRaw As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            If dtRaw.Rows.Count = 0 Then
                pnlMatrix.Visible = False
                pnlCapacityAudit.Visible = False
                ShowMessage("ℹ️ هیچ تاریخ معاینه‌ای در بازه انتخابی یافت نشد", "info")
                Return
            End If

            ' ===== مرحله ۳: ساخت DataTable ماتریس =====
            Dim dtMatrix As New DataTable()
            dtMatrix.Columns.Add("RowNumber", GetType(Integer))
            dtMatrix.Columns.Add("DailyID", GetType(Integer))
            dtMatrix.Columns.Add("PersianDate", GetType(String))
            dtMatrix.Columns.Add("ExaminationDate", GetType(String))

            For Each uRow As DataRow In dtUnits.Rows
                dtMatrix.Columns.Add("Unit_" & uRow("ID").ToString(), GetType(String))
            Next
            dtMatrix.Columns.Add("RowTotal", GetType(String))

            Dim rowNum As Integer = 1
            For Each dataRow As DataRow In dtRaw.Rows
                Dim dr As DataRow = dtMatrix.NewRow()
                dr("RowNumber") = rowNum
                dr("DailyID") = dataRow("DailyID")
                dr("PersianDate") = dataRow("PersianDate").ToString()

                Dim examDateVal As Object = dataRow("ExaminationDate")
                If examDateVal Is DBNull.Value Then
                    dr("ExaminationDate") = ""
                Else
                    dr("ExaminationDate") = Convert.ToDateTime(examDateVal).ToString("yyyy-MM-dd")
                End If

                For Each uRow As DataRow In dtUnits.Rows
                    Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                    Dim capCol As String = "Unit_" & uid & "_Cap"
                    Dim resCol As String = "Unit_" & uid & "_Res"
                    Dim colName As String = "Unit_" & uid

                    Dim cap As Integer = 0
                    Dim res As Integer = 0
                    If dataRow.Table.Columns.Contains(capCol) AndAlso dataRow(capCol) IsNot DBNull.Value Then cap = Convert.ToInt32(dataRow(capCol))
                    If dataRow.Table.Columns.Contains(resCol) AndAlso dataRow(resCol) IsNot DBNull.Value Then res = Convert.ToInt32(dataRow(resCol))

                    If cap = 0 AndAlso res = 0 Then
                        dr(colName) = "-"
                    Else
                        dr(colName) = cap.ToString() & "/" & res.ToString()
                    End If
                Next

                Dim totalCap As Integer = If(dataRow("RowTotalCap") Is DBNull.Value, 0, Convert.ToInt32(dataRow("RowTotalCap")))
                Dim totalRes As Integer = If(dataRow("RowTotalRes") Is DBNull.Value, 0, Convert.ToInt32(dataRow("RowTotalRes")))
                dr("RowTotal") = totalCap.ToString() & "/" & totalRes.ToString()

                dtMatrix.Rows.Add(dr)
                rowNum += 1
            Next

            ' ===== مرحله ۴: حذف ستون‌های خالی =====
            Dim colsToRemove As New List(Of String)()
            For Each uRow As DataRow In dtUnits.Rows
                Dim colName As String = "Unit_" & uRow("ID").ToString()
                Dim hasData As Boolean = False
                For Each mRow As DataRow In dtMatrix.Rows
                    If mRow(colName).ToString() <> "-" Then
                        hasData = True
                        Exit For
                    End If
                Next
                If Not hasData Then colsToRemove.Add(colName)
            Next
            For Each c As String In colsToRemove
                If dtMatrix.Columns.Contains(c) Then dtMatrix.Columns.Remove(c)
            Next

            Dim dtUnitsFiltered As DataTable = dtUnits.Clone()
            For Each uRow As DataRow In dtUnits.Rows
                If dtMatrix.Columns.Contains("Unit_" & uRow("ID").ToString()) Then
                    dtUnitsFiltered.ImportRow(uRow)
                End If
            Next

            ' ===== مرحله ۵: تعداد پرسنل هر واحد =====
            Dim unitIDs As New List(Of Integer)()
            For Each uRow As DataRow In dtUnitsFiltered.Rows
                unitIDs.Add(Convert.ToInt32(uRow("ID")))
            Next
            Dim personnelCounts As Dictionary(Of Integer, Integer) = GetUnitPersonnelCounts(unitIDs)

            If Not dtUnitsFiltered.Columns.Contains("PersonnelCount") Then
                dtUnitsFiltered.Columns.Add("PersonnelCount", GetType(Integer))
            End If
            For Each uRow As DataRow In dtUnitsFiltered.Rows
                Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                uRow("PersonnelCount") = If(personnelCounts.ContainsKey(uid), personnelCounts(uid), 0)
            Next

            ' ===== مرحله ۶: جمع ستون‌ها =====
            Dim colTotals As New Dictionary(Of Integer, String)()
            For Each uRow As DataRow In dtUnitsFiltered.Rows
                Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                Dim sumCap As Integer = 0
                Dim sumRes As Integer = 0
                For Each r As DataRow In dtRaw.Rows
                    Dim capCol As String = "Unit_" & uid & "_Cap"
                    Dim resCol As String = "Unit_" & uid & "_Res"
                    If r.Table.Columns.Contains(capCol) AndAlso r(capCol) IsNot DBNull.Value Then sumCap += Convert.ToInt32(r(capCol))
                    If r.Table.Columns.Contains(resCol) AndAlso r(resCol) IsNot DBNull.Value Then sumRes += Convert.ToInt32(r(resCol))
                Next
                colTotals(uid) = sumCap.ToString() & "/" & sumRes.ToString()
            Next

            ' ===== مرحله ۷: جمع کل =====
            Dim grandCap As Integer = 0
            Dim grandRes As Integer = 0
            For Each r As DataRow In dtRaw.Rows
                If r("RowTotalCap") IsNot DBNull.Value Then grandCap += Convert.ToInt32(r("RowTotalCap"))
                If r("RowTotalRes") IsNot DBNull.Value Then grandRes += Convert.ToInt32(r("RowTotalRes"))
            Next

            ViewState("MatrixData") = dtMatrix
            ViewState("MatrixUnits") = dtUnitsFiltered
            ViewState("ColTotals") = colTotals
            ViewState("GrandTotal") = grandCap.ToString() & "/" & grandRes.ToString()
            ViewState("PersonnelCounts") = personnelCounts

            rptHeader.DataSource = dtUnitsFiltered
            rptHeader.DataBind()

            rptBody.DataSource = dtMatrix
            rptBody.DataBind()

            ' ===== فوتر =====
            Dim dtFooter As New DataTable()
            dtFooter.Columns.Add("RowLabel", GetType(String))
            dtFooter.Columns.Add("GrandTotal", GetType(String))
            For Each uRow As DataRow In dtUnitsFiltered.Rows
                dtFooter.Columns.Add("Unit_" & uRow("ID").ToString(), GetType(String))
            Next

            Dim footRow As DataRow = dtFooter.NewRow()
            footRow("RowLabel") = "جمع ستون‌ها (هر واحد)"
            footRow("GrandTotal") = grandCap.ToString() & "/" & grandRes.ToString()
            For Each uRow As DataRow In dtUnitsFiltered.Rows
                Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                footRow("Unit_" & uid.ToString()) = If(colTotals.ContainsKey(uid), colTotals(uid), "-")
            Next
            dtFooter.Rows.Add(footRow)

            ViewState("MatrixFooter") = dtFooter
            rptFooter.DataSource = dtFooter
            rptFooter.DataBind()

            pnlMatrix.Visible = True

            ' ===== مرحله ۸: کنترل سلامت ظرفیت =====
            LoadCapacityAudit(dateFromG, dateToG, unitIDs)

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری ماتریس: " & ex.Message, "danger")
            pnlMatrix.Visible = False
            pnlCapacityAudit.Visible = False
        End Try
    End Sub

    ' ============================================================
    ' ✅ کنترل سلامت ظرفیت (فرمول: ظرفیت کل vs تعداد پرسنل)
    ' ============================================================
    Private Sub LoadCapacityAudit(ByVal dateFromG As String, ByVal dateToG As String, _
                                  ByVal unitIDs As List(Of Integer))
        Try
            If unitIDs Is Nothing OrElse unitIDs.Count = 0 Then
                pnlCapacityAudit.Visible = False
                Return
            End If

            Dim idList As String = String.Join(",", unitIDs)

            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    u.ID AS UnitID, ")
            sql.AppendLine("    u.UnitName, ")
            sql.AppendLine("    u.OldUnitCode, ")
            sql.AppendLine("    ISNULL(p.PersonnelCount, 0) AS PersonnelCount, ")
            sql.AppendLine("    ISNULL(c.TotalCapacity, 0) AS TotalCapacity, ")
            sql.AppendLine("    ISNULL(c.DateCount, 0) AS DateCount, ")
            sql.AppendLine("    ISNULL(r.TotalReserved, 0) AS TotalReserved ")
            sql.AppendLine("FROM exmUnits u ")

            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT UnitsCode, COUNT(*) AS PersonnelCount ")
            sql.AppendLine("    FROM personel ")
            sql.AppendLine("    WHERE ISNULL(CodeClosed, 0) = 0 ")
            sql.AppendLine("    GROUP BY UnitsCode ")
            sql.AppendLine(") p ON p.UnitsCode = u.OldUnitCode ")

            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT uc.UnitID, ")
            sql.AppendLine("           SUM(uc.Capacity) AS TotalCapacity, ")
            sql.AppendLine("           COUNT(DISTINCT d.ID) AS DateCount ")
            sql.AppendLine("    FROM exmUnitCapacity uc ")
            sql.AppendLine("    INNER JOIN exmDailyCapacity d ON uc.CapacityID = d.ID ")
            sql.AppendLine("    WHERE d.IsActive = 1 ")
            If dateFromG <> "" Then sql.AppendLine("      AND d.ExaminationDate >= '" & dateFromG & "'")
            If dateToG <> "" Then sql.AppendLine("      AND d.ExaminationDate <= '" & dateToG & "'")
            sql.AppendLine("    GROUP BY uc.UnitID ")
            sql.AppendLine(") c ON c.UnitID = u.ID ")

            sql.AppendLine("LEFT JOIN ( ")
            sql.AppendLine("    SELECT UnitID, COUNT(*) AS TotalReserved ")
            sql.AppendLine("    FROM exmReservationRequests ")
            sql.AppendLine("    WHERE StatusCode NOT IN (6, 7, 8) ")
            If dateFromG <> "" Then sql.AppendLine("      AND ExaminationDate >= '" & dateFromG & "'")
            If dateToG <> "" Then sql.AppendLine("      AND ExaminationDate <= '" & dateToG & "'")
            sql.AppendLine("    GROUP BY UnitID ")
            sql.AppendLine(") r ON r.UnitID = u.ID ")

            sql.AppendLine("WHERE u.ID IN (" & idList & ") ")
            sql.AppendLine("ORDER BY u.UnitName")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            Dim dtAudit As New DataTable()
            dtAudit.Columns.Add("RowNumber", GetType(Integer))
            dtAudit.Columns.Add("UnitID", GetType(Integer))
            dtAudit.Columns.Add("UnitName", GetType(String))
            dtAudit.Columns.Add("PersonnelCount", GetType(Integer))
            dtAudit.Columns.Add("TotalCapacity", GetType(Integer))
            dtAudit.Columns.Add("DateCount", GetType(Integer))
            dtAudit.Columns.Add("AvgPerDate", GetType(Integer))
            dtAudit.Columns.Add("TotalReserved", GetType(Integer))
            dtAudit.Columns.Add("Status", GetType(String))
            dtAudit.Columns.Add("StatusClass", GetType(String))
            dtAudit.Columns.Add("DiffText", GetType(String))

            Dim rowNum As Integer = 1
            For Each row As DataRow In dt.Rows
                Dim personnel As Integer = Convert.ToInt32(row("PersonnelCount"))
                Dim totalCap As Integer = Convert.ToInt32(row("TotalCapacity"))
                Dim dateCount As Integer = Convert.ToInt32(row("DateCount"))
                Dim totalRes As Integer = Convert.ToInt32(row("TotalReserved"))

                Dim avgPerDate As Integer = 0
                If dateCount > 0 Then avgPerDate = CInt(Math.Round(CDbl(totalCap) / CDbl(dateCount)))

                Dim status As String = ""
                Dim statusClass As String = ""
                Dim diffText As String = ""

                If personnel = 0 Then
                    If row("OldUnitCode") Is DBNull.Value Then
                        status = "🆕 واحد جدید"
                        statusClass = "audit-unknown"
                        diffText = "هنوز پرسنلی تخصیص نیافته"
                    Else
                        status = "⚪ پرسنل نامشخص"
                        statusClass = "audit-unknown"
                        diffText = "هیچ پرسنل فعالی ثبت نشده"
                    End If
                ElseIf dateCount = 0 Then
                    status = "⚫ بدون تاریخ"
                    statusClass = "audit-unknown"
                    diffText = "ظرفیتی تعریف نشده"
                ElseIf totalCap < personnel Then
                    status = "🔴 کم"
                    statusClass = "audit-low"
                    diffText = "کمبود " & (personnel - totalCap) & " نفر"
                ElseIf totalCap = personnel Then
                    status = "🟢 دقیق"
                    statusClass = "audit-exact"
                    diffText = "دقیقاً برابر"
                ElseIf totalCap > personnel * 2 Then
                    status = "🟡 زیاد"
                    statusClass = "audit-high"
                    diffText = "مازاد " & (totalCap - personnel) & " نفر"
                Else
                    status = "🔵 متعادل"
                    statusClass = "audit-ok"
                    diffText = "مازاد " & (totalCap - personnel) & " نفر"
                End If

                Dim dr As DataRow = dtAudit.NewRow()
                dr("RowNumber") = rowNum
                dr("UnitID") = Convert.ToInt32(row("UnitID"))
                dr("UnitName") = row("UnitName").ToString()
                dr("PersonnelCount") = personnel
                dr("TotalCapacity") = totalCap
                dr("DateCount") = dateCount
                dr("AvgPerDate") = avgPerDate
                dr("TotalReserved") = totalRes
                dr("Status") = status
                dr("StatusClass") = statusClass
                dr("DiffText") = diffText
                dtAudit.Rows.Add(dr)

                rowNum += 1
            Next

            ViewState("CapacityAudit") = dtAudit
            gvCapacityAudit.DataSource = dtAudit
            gvCapacityAudit.DataBind()
            pnlCapacityAudit.Visible = dtAudit.Rows.Count > 0

        Catch ex As Exception
            ShowMessage("خطا در کنترل سلامت ظرفیت: " & ex.Message, "danger")
            pnlCapacityAudit.Visible = False
        End Try
    End Sub

    ' ============================================================
    ' ✅ تولید HTML ردیف‌های ماتریس
    ' ============================================================
    Protected Function GetMatrixRowHtml(ByVal dataItem As Object) As String
        Try
            Dim drv As DataRowView = CType(dataItem, DataRowView)
            Dim dtUnits As DataTable = CType(ViewState("MatrixUnits"), DataTable)
            Dim personnelCounts As Dictionary(Of Integer, Integer) = CType(ViewState("PersonnelCounts"), Dictionary(Of Integer, Integer))
            If dtUnits Is Nothing Then Return ""

            Dim sb As New StringBuilder()
            Dim rowNum As String = drv("RowNumber").ToString()
            Dim persianDate As String = drv("PersianDate").ToString()
            Dim examDate As String = drv("ExaminationDate").ToString()

            sb.Append("<tr>")
            sb.Append("<td style='background:#f5f5f5;font-weight:700;color:#555;'>" & rowNum & "</td>")

            ' ==== تاریخ: لینک (با #pnlDateDetail برای اسکرول خودکار) ====
            Dim dateUrl As String = "ExmUnitDateMatrix.aspx?DetailDate=" & Server.UrlEncode(examDate) & _
                                    "&DateFrom=" & Server.UrlEncode(txtDateFrom.Text.Trim()) & _
                                    "&DateTo=" & Server.UrlEncode(txtDateTo.Text.Trim()) & _
                                    "#pnlDateDetail"
            sb.Append("<td class='date-cell'>")
            sb.Append("<a href='" & dateUrl & "' title='کلیک برای مشاهده پرسنل رزروکننده در این تاریخ'>")
            sb.Append(persianDate & " 👥")
            sb.Append("</a></td>")

            ' ==== سلول‌های واحد ====
            For Each uRow As DataRow In dtUnits.Rows
                Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                Dim colName As String = "Unit_" & uid.ToString()
                Dim cellVal As String = If(drv.DataView.Table.Columns.Contains(colName), drv(colName).ToString(), "-")

                If cellVal = "-" OrElse Not cellVal.Contains("/") Then
                    sb.Append("<td class='mx-compact' style='color:#ccc;'>—</td>")
                    Continue For
                End If

                Dim parts() As String = cellVal.Split("/"c)
                Dim cap As Integer = Convert.ToInt32(parts(0))
                Dim res As Integer = Convert.ToInt32(parts(1))
                Dim avail As Integer = cap - res
                If avail < 0 Then avail = 0
                Dim pct As Integer = 0
                If cap > 0 Then pct = CInt((CDbl(res) / CDbl(cap)) * 100)
                If pct > 100 Then pct = 100

                Dim stateClass As String = ""
                Dim barClass As String = "mini-bar-fill"
                Dim isOverReserved As Boolean = (res > cap)

                If isOverReserved Then
                    stateClass = " is-over"
                    barClass = "mini-bar-fill over"
                ElseIf cap > 0 AndAlso res >= cap Then
                    stateClass = " is-full"
                    barClass = "mini-bar-fill danger"
                ElseIf pct >= 70 Then
                    barClass = "mini-bar-fill warning"
                End If
                If res = 0 Then stateClass &= " is-empty"

                Dim unitPersonnel As Integer = 0
                If personnelCounts IsNot Nothing AndAlso personnelCounts.ContainsKey(uid) Then
                    unitPersonnel = personnelCounts(uid)
                End If

                Dim coveragePct As Integer = 0
                If unitPersonnel > 0 Then coveragePct = CInt((CDbl(res) / CDbl(unitPersonnel)) * 100)

                ' ============================================================
                ' Tooltip سفارشی
                ' ============================================================
                Dim tipHtml As New StringBuilder()
                tipHtml.Append("<div class='mx-tip'>")
                tipHtml.Append("<div class='tip-row'>📊 ظرفیت: <b>" & cap & "</b> نفر</div>")
                tipHtml.Append("<div class='tip-row'>✅ رزرو: <b>" & res & "</b> نفر</div>")
                tipHtml.Append("<div class='tip-row'>🪑 باقیمانده: <b>" & avail & "</b> نفر</div>")
                tipHtml.Append("<div class='tip-row'>📈 درصد تکمیل: <b>" & pct & "%</b></div>")
                If unitPersonnel > 0 Then
                    tipHtml.Append("<div class='tip-row'>👥 کل پرسنل: <b>" & unitPersonnel & "</b> نفر</div>")
                    tipHtml.Append("<div class='tip-row'>📉 پوشش: <b>" & coveragePct & "%</b></div>")
                End If
                If isOverReserved Then
                    tipHtml.Append("<div class='tip-row tip-warn'>⚠️ رزرو " & (res - cap) & " نفر بیش از ظرفیت!</div>")
                End If
                tipHtml.Append("</div>")

                sb.Append("<td class='mx-compact'>")
                sb.Append("<div class='mx-cell-compact" & stateClass & "'>")
                sb.Append("<span class='res-num'>" & res & "</span>")
                sb.Append("<span class='cap-num'>از " & cap & "</span>")
                sb.Append("<div class='mini-bar'><div class='" & barClass & "' style='width:" & pct & "%'></div></div>")
                sb.Append(tipHtml.ToString())
                sb.Append("</div>")
                sb.Append("</td>")
            Next

            ' ==== جمع سطر ====
            Dim rowTotal As String = If(drv.DataView.Table.Columns.Contains("RowTotal"), drv("RowTotal").ToString(), "-")
            If rowTotal.Contains("/") Then
                Dim p() As String = rowTotal.Split("/"c)
                sb.Append("<td class='row-total' style='white-space:nowrap;'>")
                sb.Append("<b style='font-size:14px;'>" & p(1) & "</b>")
                sb.Append("<span style='font-size:10px;color:#a04000;'> از " & p(0) & "</span>")
                sb.Append("</td>")
            Else
                sb.Append("<td class='row-total'>" & rowTotal & "</td>")
            End If

            sb.Append("</tr>")
            Return sb.ToString()

        Catch ex As Exception
            Return "<tr><td colspan='99'>خطا: " & Server.HtmlEncode(ex.Message) & "</td></tr>"
        End Try
    End Function

    ' ============================================================
    ' ✅ تولید HTML فوتر (جمع ستون‌ها)
    ' ============================================================
    Protected Function GetMatrixFooterHtml(ByVal dataItem As Object) As String
        Try
            Dim drv As DataRowView = CType(dataItem, DataRowView)
            Dim dtUnits As DataTable = CType(ViewState("MatrixUnits"), DataTable)
            If dtUnits Is Nothing Then Return ""

            Dim sb As New StringBuilder()
            sb.Append("<tr style='background:#34495e;color:#fff;font-weight:700;'>")
            sb.Append("<td colspan='2' style='background:#2c3e50;color:#fff;text-align:center;padding:8px;font-size:12px;'>")
            sb.Append("📊 " & drv("RowLabel").ToString())
            sb.Append("</td>")

            For Each uRow As DataRow In dtUnits.Rows
                Dim colName As String = "Unit_" & uRow("ID").ToString()
                Dim val As String = If(drv.DataView.Table.Columns.Contains(colName), drv(colName).ToString(), "-")

                sb.Append("<td style='background:#16a085;color:#fff;font-weight:700;text-align:center;padding:6px 4px;'>")
                If val.Contains("/") Then
                    Dim p() As String = val.Split("/"c)
                    sb.Append("<div style='font-size:15px;line-height:1.1;'>" & p(1) & "</div>")
                    sb.Append("<div style='font-size:9px;opacity:0.85;'>از " & p(0) & "</div>")
                Else
                    sb.Append(val)
                End If
                sb.Append("</td>")
            Next

            Dim grand As String = drv("GrandTotal").ToString()
            sb.Append("<td style='background:#c0392b;color:#fff;font-weight:700;text-align:center;padding:6px 4px;'>")
            If grand.Contains("/") Then
                Dim g() As String = grand.Split("/"c)
                sb.Append("<div style='font-size:15px;'>" & g(1) & "</div>")
                sb.Append("<div style='font-size:9px;opacity:0.85;'>از " & g(0) & "</div>")
            Else
                sb.Append(grand)
            End If
            sb.Append("</td></tr>")

            Return sb.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ============================================================
    ' ✅ بارگذاری جزئیات پرسنل یک تاریخ
    ' ============================================================
    Private Sub LoadDateDetail(ByVal examinationDate As String)
        Try
            If String.IsNullOrEmpty(examinationDate) Then Return

            Dim safeDate As String = NormalizeToGregorian(examinationDate)
            If String.IsNullOrEmpty(safeDate) Then
                ShowMessage("❌ تاریخ ورودی نامعتبر است: " & examinationDate, "danger")
                Return
            End If

            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    ROW_NUMBER() OVER (ORDER BY r.UnitName, r.FullName) AS RowNumber, ")
            sql.AppendLine("    ISNULL(r.UnitName, '-') AS UnitName, ")
            sql.AppendLine("    ISNULL(r.PersonCode, '-') AS PersonCode, ")
            sql.AppendLine("    ISNULL(r.FullName, '-') AS FullName, ")
            sql.AppendLine("    ISNULL(r.NationalCode, '-') AS NationalCode, ")
            sql.AppendLine("    ISNULL(r.Mobile, '-') AS Mobile, ")
            sql.AppendLine("    ISNULL(r.Position, '-') AS Position, ")
            sql.AppendLine("    r.RequestDate, ")
            sql.AppendLine("    r.StatusCode, ")
            sql.AppendLine("    ISNULL(r.StatusDescription, '') AS StatusDescription ")
            sql.AppendLine("FROM exmReservationRequests r ")
            sql.AppendLine("WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, '" & safeDate & "') ")
            sql.AppendLine("  AND r.StatusCode NOT IN (6, 7, 8) ")
            sql.AppendLine("ORDER BY r.UnitName, r.FullName")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            Dim persianDate As String = "-"
            Try
                Dim d As DateTime = DateTime.ParseExact(safeDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                Dim pc As New PersianCalendar()
                persianDate = pc.GetYear(d).ToString() & "/" & _
                              pc.GetMonth(d).ToString().PadLeft(2, "0"c) & "/" & _
                              pc.GetDayOfMonth(d).ToString().PadLeft(2, "0"c)
            Catch
                persianDate = safeDate
            End Try

            lblDetailDate.Text = persianDate

            If Not dt.Columns.Contains("RequestDateFa") Then dt.Columns.Add("RequestDateFa", GetType(String))
            If Not dt.Columns.Contains("StatusCodeFa") Then dt.Columns.Add("StatusCodeFa", GetType(String))

            For Each row As DataRow In dt.Rows
                Try
                    If row("RequestDate") IsNot DBNull.Value Then
                        Dim rd As DateTime = Convert.ToDateTime(row("RequestDate"))
                        Dim pc2 As New PersianCalendar()
                        row("RequestDateFa") = pc2.GetYear(rd).ToString() & "/" & _
                                                pc2.GetMonth(rd).ToString().PadLeft(2, "0"c) & "/" & _
                                                pc2.GetDayOfMonth(rd).ToString().PadLeft(2, "0"c) & " - " & _
                                                rd.ToString("HH:mm")
                    Else
                        row("RequestDateFa") = "-"
                    End If
                Catch
                    row("RequestDateFa") = "-"
                End Try

                Dim sc As Integer = If(row("StatusCode") Is DBNull.Value, 0, Convert.ToInt32(row("StatusCode")))
                Select Case sc
                    Case 0 : row("StatusCodeFa") = "ثبت شده"
                    Case 1 : row("StatusCodeFa") = "تأیید سرپرست"
                    Case 2 : row("StatusCodeFa") = "تأیید مدیر واحد"
                    Case 3 : row("StatusCodeFa") = "تأیید کارشناس بهداشت"
                    Case 4 : row("StatusCodeFa") = "حاضر نشده"
                    Case 5 : row("StatusCodeFa") = "انجام شده"
                    Case 6 : row("StatusCodeFa") = "لغو توسط کاربر"
                    Case 7 : row("StatusCodeFa") = "لغو توسط ادمین"
                    Case 8 : row("StatusCodeFa") = "منقضی شده"
                    Case Else : row("StatusCodeFa") = "کد " & sc
                End Select
            Next

            gvDateDetail.DataSource = dt
            gvDateDetail.DataBind()
            pnlDateDetail.Visible = True

            If dt.Rows.Count = 0 Then
                divDetailSummary.Visible = False
                ShowMessage("ℹ️ هیچ پرسنلی در تاریخ " & persianDate & " رزرو نکرده است", "info")
            Else
                Dim unitStats As New Dictionary(Of String, Integer)()
                For Each row As DataRow In dt.Rows
                    Dim un As String = If(row("UnitName") Is DBNull.Value, "نامشخص", row("UnitName").ToString())
                    If Not unitStats.ContainsKey(un) Then unitStats(un) = 0
                    unitStats(un) += 1
                Next

                Dim unitSummary As String = ""
                For Each kv As KeyValuePair(Of String, Integer) In unitStats
                    unitSummary &= "<span class='count-unit'>" & kv.Key & ": " & kv.Value & "</span>"
                Next

                divDetailSummary.Visible = True
                lblDetailCount.Text = "👥 <span class='count-number'>" & dt.Rows.Count & "</span> پرسنل رزرو کرده‌اند " & unitSummary

                ShowMessage("✅ " & dt.Rows.Count & " پرسنل در تاریخ " & persianDate & " رزرو کرده‌اند", "success")
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری جزئیات تاریخ: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' ✅ بارگذاری جزئیات واحد
    ' ============================================================
    Private Sub LoadUnitDetail(ByVal unitID As Integer)
        Try
            Dim dateFromG As String = ConvertPersianDateToGregorian(txtDateFrom.Text.Trim())
            Dim dateToG As String = ConvertPersianDateToGregorian(txtDateTo.Text.Trim())

            Dim sql As New StringBuilder()
            sql.AppendLine("SELECT ")
            sql.AppendLine("    ROW_NUMBER() OVER (ORDER BY d.ExaminationDate) AS RowNumber, ")
            sql.AppendLine("    dbo.FormatDateFa(d.ExaminationDate) AS PersianDate, ")
            sql.AppendLine("    ISNULL((SELECT SUM(uc.Capacity) FROM exmUnitCapacity uc ")
            sql.AppendLine("            WHERE uc.CapacityID = d.ID AND uc.UnitID = " & unitID & "), 0) AS Capacity, ")
            sql.AppendLine("    ISNULL((SELECT COUNT(*) FROM exmReservationRequests r ")
            sql.AppendLine("            WHERE CONVERT(date, r.ExaminationDate) = CONVERT(date, d.ExaminationDate) AND r.UnitID = " & unitID & " ")
            sql.AppendLine("              AND r.StatusCode NOT IN (6,7,8)), 0) AS Reserved ")
            sql.AppendLine("FROM exmDailyCapacity d ")
            sql.AppendLine("WHERE d.IsActive = 1 ")
            sql.AppendLine("  AND EXISTS (SELECT 1 FROM exmUnitCapacity uc2 ")
            sql.AppendLine("              WHERE uc2.CapacityID = d.ID AND uc2.UnitID = " & unitID & ") ")
            If dateFromG <> "" Then sql.AppendLine("  AND d.ExaminationDate >= '" & dateFromG & "'")
            If dateToG <> "" Then sql.AppendLine("  AND d.ExaminationDate <= '" & dateToG & "'")
            sql.AppendLine("ORDER BY d.ExaminationDate")

            Dim dt As DataTable = ClsDataTable.GetDataTable(sql.ToString())

            If Not dt.Columns.Contains("Available") Then dt.Columns.Add("Available", GetType(Integer))
            If Not dt.Columns.Contains("FillPercent") Then dt.Columns.Add("FillPercent", GetType(Integer))

            For Each row As DataRow In dt.Rows
                Dim cap As Integer = Convert.ToInt32(row("Capacity"))
                Dim res As Integer = Convert.ToInt32(row("Reserved"))
                Dim avail As Integer = cap - res
                If avail < 0 Then avail = 0
                row("Available") = avail

                Dim pct As Integer = 0
                If cap > 0 Then pct = CInt((CDbl(res) / CDbl(cap)) * 100)
                If pct > 100 Then pct = 100
                If pct < 0 Then pct = 0
                row("FillPercent") = pct
            Next

            Dim dtUnitName As DataTable = ClsDataTable.GetDataTable("SELECT UnitName FROM exmUnits WHERE ID = " & unitID)
            If dtUnitName.Rows.Count > 0 Then
                lblDetailUnit.Text = dtUnitName.Rows(0)("UnitName").ToString()
            Else
                lblDetailUnit.Text = "شناخته نشد"
            End If

            gvUnitDetail.DataSource = dt
            gvUnitDetail.DataBind()
            pnlUnitDetail.Visible = True

            If dt.Rows.Count = 0 Then
                ShowMessage("ℹ️ هیچ تاریخ معاینه‌ای برای این واحد در بازه انتخابی یافت نشد", "info")
            End If

        Catch ex As Exception
            ShowMessage("خطا در بارگذاری جزئیات واحد: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' کلاس نوار درصد
    ' ============================================================
    Protected Function GetFillBarClass(ByVal fillPercent As Object) As String
        Try
            Dim p As Integer = Convert.ToInt32(fillPercent)
            If p >= 90 Then Return "fill-bar danger"
            If p >= 70 Then Return "fill-bar warning"
            Return "fill-bar"
        Catch
            Return "fill-bar"
        End Try
    End Function

    ' ============================================================
    ' رویدادها
    ' ============================================================
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        pnlDateDetail.Visible = False
        pnlUnitDetail.Visible = False
        LoadMatrix()
    End Sub

    Protected Sub gvCapacityAudit_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvCapacityAudit.PageIndexChanging
        gvCapacityAudit.PageIndex = e.NewPageIndex
        Dim dtAudit As DataTable = CType(ViewState("CapacityAudit"), DataTable)
        If dtAudit IsNot Nothing Then
            gvCapacityAudit.DataSource = dtAudit
            gvCapacityAudit.DataBind()
        End If
    End Sub

    Protected Sub gvDateDetail_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvDateDetail.PageIndexChanging
        gvDateDetail.PageIndex = e.NewPageIndex
        Dim detailDate As String = CType(ViewState("DetailDate"), String)
        If String.IsNullOrEmpty(detailDate) Then detailDate = Request.QueryString("DetailDate")
        If Not String.IsNullOrEmpty(detailDate) Then LoadDateDetail(detailDate)
    End Sub

    Protected Sub gvUnitDetail_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvUnitDetail.PageIndexChanging
        gvUnitDetail.PageIndex = e.NewPageIndex
        Dim uid As Integer = 0
        If ViewState("DetailUnitID") IsNot Nothing Then uid = Convert.ToInt32(ViewState("DetailUnitID"))
        If uid = 0 AndAlso Not String.IsNullOrEmpty(Request.QueryString("UnitID")) Then uid = Convert.ToInt32(Request.QueryString("UnitID"))
        If uid > 0 Then LoadUnitDetail(uid)
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    ' ============================================================
    ' ✅ خروجی اکسل (با فرمت متن خالص برای جلوگیری از تبدیل تاریخ)
    ' ============================================================
    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim dtMatrix As DataTable = CType(ViewState("MatrixData"), DataTable)
            Dim dtUnits As DataTable = CType(ViewState("MatrixUnits"), DataTable)
            Dim colTotals As Dictionary(Of Integer, String) = CType(ViewState("ColTotals"), Dictionary(Of Integer, String))
            Dim dtAudit As DataTable = CType(ViewState("CapacityAudit"), DataTable)

            If dtMatrix Is Nothing OrElse dtUnits Is Nothing Then
                ShowMessage("ابتدا گزارش را نمایش دهید", "warning")
                Return
            End If

            Dim sb As New StringBuilder()
            sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' ")
            sb.Append("xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>")
            sb.Append("<head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />")
            sb.Append("<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>")

            If dtAudit IsNot Nothing AndAlso dtAudit.Rows.Count > 0 Then
                sb.Append("<x:ExcelWorksheet><x:Name>کنترل سلامت ظرفیت</x:Name>")
                sb.Append("<x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>")
            End If

            sb.Append("<x:ExcelWorksheet><x:Name>ماتریس تاریخ × واحد</x:Name>")
            sb.Append("<x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>")

            sb.Append("</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->")
            sb.Append("<style>table{border-collapse:collapse;direction:rtl;font-family:Tahoma;} ")
            sb.Append("th{background:#2c3e50;color:#fff;padding:6px;border:1px solid #ccc;font-size:11pt;} ")
            sb.Append("td{padding:5px;border:1px solid #ddd;text-align:center;font-size:10pt;} ")
            sb.Append(".row-total{background:#fdebd0;font-weight:bold;} ")
            sb.Append(".col-total{background:#e74c3c;color:#fff;font-weight:bold;} ")
            sb.Append(".footer{background:#16a085;color:#fff;font-weight:bold;} ")
            sb.Append(".date-cell{background:#fff3cd;font-weight:bold;} ")
            sb.Append(".title{font-size:14pt;font-weight:bold;text-align:center;padding:10px;background:#f0f0f0;} ")
            sb.Append(".status-low{background:#f8d7da;color:#721c24;font-weight:bold;} ")
            sb.Append(".status-exact{background:#d4edda;color:#155724;font-weight:bold;} ")
            sb.Append(".status-high{background:#fff3cd;color:#856404;font-weight:bold;} ")
            sb.Append(".status-ok{background:#d1ecf1;color:#0c5460;font-weight:bold;} ")
            sb.Append("</style></head><body>")

            ' ===== شیت ۱: کنترل سلامت ظرفیت =====
            If dtAudit IsNot Nothing AndAlso dtAudit.Rows.Count > 0 Then
                sb.Append("<div class='title'>🏢 کنترل سلامت ظرفیت واحدها</div>")
                sb.Append("<br/>")
                sb.Append("<table>")
                sb.Append("<tr>")
                sb.Append("<th>#</th><th>واحد</th><th>تعداد پرسنل</th><th>ظرفیت کل</th>")
                sb.Append("<th>تعداد تاریخ</th><th>میانگین هر تاریخ</th><th>رزرو فعال</th>")
                sb.Append("<th>اختلاف</th><th>وضعیت</th>")
                sb.Append("</tr>")

                For Each row As DataRow In dtAudit.Rows
                    Dim st As String = row("Status").ToString()
                    Dim stClass As String = ""

                    If st.Contains("کم") Then
                        stClass = "status-low"
                    ElseIf st.Contains("دقیق") Then
                        stClass = "status-exact"
                    ElseIf st.Contains("زیاد") Then
                        stClass = "status-high"
                    ElseIf st.Contains("متعادل") Then
                        stClass = "status-ok"
                    End If

                    sb.Append("<tr>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("RowNumber").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("UnitName").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("PersonnelCount").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("TotalCapacity").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("DateCount").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("AvgPerDate").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("TotalReserved").ToString() & "</td>")
                    sb.Append("<td style='mso-number-format:\@;'>" & row("DiffText").ToString() & "</td>")
                    sb.Append("<td class='" & stClass & "' style='mso-number-format:\@;'>" & st & "</td>")
                    sb.Append("</tr>")
                Next
                sb.Append("</table>")
                sb.Append("<br pagebreak='true' />")
            End If

            ' ===== شیت ۲: ماتریس تاریخ × واحد =====
            sb.Append("<div class='title'>📊 ماتریس تاریخ × واحد</div>")
            sb.Append("<br/>")
            sb.Append("<table>")
            sb.Append("<tr><th>#</th><th>تاریخ معاینه</th>")
            For Each uRow As DataRow In dtUnits.Rows
                sb.Append("<th>" & uRow("UnitName").ToString() & "</th>")
            Next
            sb.Append("<th class='col-total'>جمع سطر</th></tr>")

            For Each row As DataRow In dtMatrix.Rows
                sb.Append("<tr>")
                sb.Append("<td style='mso-number-format:\@;'>" & row("RowNumber").ToString() & "</td>")
                sb.Append("<td class='date-cell' style='mso-number-format:\@;'>" & row("PersianDate").ToString() & "</td>")

                For Each uRow As DataRow In dtUnits.Rows
                    Dim colName As String = "Unit_" & uRow("ID").ToString()
                    Dim val As String = If(row.Table.Columns.Contains(colName), row(colName).ToString(), "-")
                    Dim displayVal As String = val
                    If val.Contains("/") Then
                        Dim parts() As String = val.Split("/"c)
                        If parts.Length = 2 Then
                            displayVal = parts(0) & " از " & parts(1)
                        End If
                    End If
                    sb.Append("<td style='mso-number-format:\@;'>" & displayVal & "</td>")
                Next

                Dim rowTotalVal As String = row("RowTotal").ToString()
                If rowTotalVal.Contains("/") Then
                    Dim pTot() As String = rowTotalVal.Split("/"c)
                    If pTot.Length = 2 Then
                        rowTotalVal = pTot(0) & " از " & pTot(1)
                    End If
                End If
                sb.Append("<td class='row-total' style='mso-number-format:\@;'>" & rowTotalVal & "</td>")
                sb.Append("</tr>")
            Next

            ' ===== فوتر =====
            sb.Append("<tr><td colspan='2' class='footer' style='mso-number-format:\@;'>جمع ستون‌ها</td>")
            For Each uRow As DataRow In dtUnits.Rows
                Dim uid As Integer = Convert.ToInt32(uRow("ID"))
                Dim val As String = If(colTotals IsNot Nothing AndAlso colTotals.ContainsKey(uid), colTotals(uid), "-")
                If val.Contains("/") Then
                    Dim pF() As String = val.Split("/"c)
                    If pF.Length = 2 Then
                        val = pF(0) & " از " & pF(1)
                    End If
                End If
                sb.Append("<td class='footer' style='mso-number-format:\@;'>" & val & "</td>")
            Next

            Dim grandVal As String = ViewState("GrandTotal").ToString()
            If grandVal.Contains("/") Then
                Dim pG() As String = grandVal.Split("/"c)
                If pG.Length = 2 Then
                    grandVal = pG(0) & " از " & pG(1)
                End If
            End If
            sb.Append("<td class='footer' style='mso-number-format:\@;'>" & grandVal & "</td></tr>")
            sb.Append("</table></body></html>")

            Dim pc As New PersianCalendar()
            Dim now As DateTime = DateTime.Now
            Dim fileDate As String = pc.GetYear(now).ToString() & "-" & pc.GetMonth(now).ToString().PadLeft(2, "0"c) & "-" & pc.GetDayOfMonth(now).ToString().PadLeft(2, "0"c)
            Dim fileName As String = "MatrixReport_" & fileDate & ".xls"

            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
            Response.ContentType = "application/vnd.ms-excel"
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetBytes(sb.ToString()))
            Response.Flush()
            Response.End()

        Catch ex As Exception
            ShowMessage("خطا در خروجی اکسل: " & ex.Message, "danger")
        End Try
    End Sub

    ' ============================================================
    ' نمایش پیام
    ' ============================================================
    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        pnlMessage.Visible = True
        Select Case type
            Case "success"
                divMessage.Attributes("class") = "mx-alert mx-alert-info"
                lblMessage.Text = "✅ " & message
            Case "danger"
                divMessage.Attributes("class") = "mx-alert mx-alert-danger"
                lblMessage.Text = "❌ " & message
            Case "warning"
                divMessage.Attributes("class") = "mx-alert mx-alert-warning"
                lblMessage.Text = "⚠️ " & message
            Case Else
                divMessage.Attributes("class") = "mx-alert mx-alert-info"
                lblMessage.Text = "ℹ️ " & message
        End Select
    End Sub

End Class