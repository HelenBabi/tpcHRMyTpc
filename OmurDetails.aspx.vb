Imports System.Data
Imports System.Data.SqlClient

Partial Class OmurDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim omurID As Integer = Convert.ToInt32(Request.QueryString("OmurID"))
            If omurID > 0 Then
                LoadOmurDetails(omurID)
            Else
                Response.Redirect("~/ExaminationSetCapacity.aspx")
            End If
        End If
    End Sub

    Private Sub LoadOmurDetails(ByVal omurID As Integer)
        Try
            ' ====== 1. اطلاعات اصلی بخش ======
            Dim sqlstr As String = "SELECT " &
                                   "    o.omurID, " &
                                   "    o.omur AS OmurName, " &
                                   "    COUNT(p.codePersoneli) AS PersonCount " &
                                   "FROM omurTbl o " &
                                   "LEFT JOIN personel p ON p.omurID = o.omurID AND p.CodeClosed = 0 " &
                                   "WHERE o.omurID = @OmurID " &
                                   "GROUP BY o.omurID, o.omur"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr, New Dictionary(Of String, Object) From {{"@OmurID", omurID}})

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                lblOmurName.Text = row("OmurName").ToString()
                lblPersonCount.Text = row("PersonCount").ToString()
            End If

            ' ====== 2. محاسبه ظرفیت‌ها ======
            Dim capSql As String = "SELECT " &
                                   "    ISNULL((" &
                                   "        SELECT SUM(oc.Capacity) " &
                                   "        FROM exmOmurCapacity oc " &
                                   "        INNER JOIN exmDailyCapacity c ON oc.CapacityID = c.ID " &
                                   "        WHERE oc.OmurID = @OmurID " &
                                   "        AND c.ExaminationDate >= CAST(GETDATE() AS DATE) " &
                                   "        AND c.IsActive = 1" &
                                   "    ), 0) AS TotalCapacity, " &
                                   "    ISNULL((" &
                                   "        SELECT COUNT(*) " &
                                   "        FROM exmReservationRequests r " &
                                   "        INNER JOIN exmDailyCapacity c ON r.ExaminationDate = c.ExaminationDate " &
                                   "        INNER JOIN exmOmurCapacity oc ON c.ID = oc.CapacityID " &
                                   "        WHERE oc.OmurID = @OmurID " &
                                   "        AND r.StatusCode NOT IN (6, 7)" &
                                   "        AND c.ExaminationDate >= CAST(GETDATE() AS DATE)" &
                                   "    ), 0) AS AssignedCount"

            Dim capDt As DataTable = ClsDataTable.GetDataTable(capSql, New Dictionary(Of String, Object) From {{"@OmurID", omurID}})

            If capDt.Rows.Count > 0 Then
                Dim capRow As DataRow = capDt.Rows(0)
                Dim totalCap As Integer = Convert.ToInt32(capRow("TotalCapacity"))
                Dim assigned As Integer = Convert.ToInt32(capRow("AssignedCount"))

                lblTotalCapacity.Text = totalCap.ToString()
                lblAssignedCount.Text = assigned.ToString()
                lblStatus.Text = If(totalCap > 0 AndAlso totalCap <= assigned, "🔴 تکمیل", "🟢 موجود")
            End If

            ' ====== 3. تاریخ‌های معاینه این بخش ======
            Dim dateSql As String = "SELECT " &
                                    "    dbo.ConvertGregorianToJalali(c.ExaminationDate) AS PersianDate, " &
                                    "    CASE DATEPART(dw, c.ExaminationDate) " &
                                    "        WHEN 7 THEN N'شنبه' " &
                                    "        WHEN 1 THEN N'یکشنبه' " &
                                    "        WHEN 2 THEN N'دوشنبه' " &
                                    "        WHEN 3 THEN N'سه‌شنبه' " &
                                    "        WHEN 4 THEN N'چهارشنبه' " &
                                    "        WHEN 5 THEN N'پنج‌شنبه' " &
                                    "        WHEN 6 THEN N'جمعه' " &
                                    "    END AS DayName, " &
                                    "    oc.Capacity, " &
                                    "    ISNULL((" &
                                    "        SELECT COUNT(*) " &
                                    "        FROM exmReservationRequests r " &
                                    "        WHERE r.ExaminationDate = c.ExaminationDate " &
                                    "        AND r.StatusCode NOT IN (6, 7)" &
                                    "    ), 0) AS Reserved " &
                                    "FROM exmOmurCapacity oc " &
                                    "INNER JOIN exmDailyCapacity c ON oc.CapacityID = c.ID " &
                                    "WHERE oc.OmurID = @OmurID " &
                                    "AND c.ExaminationDate >= CAST(GETDATE() AS DATE) " &
                                    "AND c.IsActive = 1 " &
                                    "ORDER BY c.ExaminationDate"

            Dim dateDt As DataTable = ClsDataTable.GetDataTable(dateSql, New Dictionary(Of String, Object) From {{"@OmurID", omurID}})
            gvDates.DataSource = dateDt
            gvDates.DataBind()

            ' ====== 4. پرسنل این بخش ======
            Dim personSql As String = "SELECT " &
                                      "    p.codePersoneli AS PersonCode, " &
                                      "    p.nam + ' ' + p.family AS FullName, " &
                                      "    CASE " &
                                      "        WHEN EXISTS (" &
                                      "            SELECT 1 FROM exmReservationRequests r " &
                                      "            WHERE r.PersonCode = p.codePersoneli " &
                                      "            AND r.StatusCode NOT IN (6, 7)" &
                                      "        ) THEN N'رزرو کرده' " &
                                      "        ELSE N'رزرو نکرده' " &
                                      "    END AS Status " &
                                      "FROM personel p " &
                                      "WHERE p.omurID = @OmurID " &
                                      "AND p.CodeClosed = 0 " &
                                      "ORDER BY p.family"

            Dim personDt As DataTable = ClsDataTable.GetDataTable(personSql, New Dictionary(Of String, Object) From {{"@OmurID", omurID}})
            gvPersons.DataSource = personDt
            gvPersons.DataBind()

        Catch ex As Exception
            Response.Write("خطا: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/ExaminationSetCapacity.aspx")
    End Sub
End Class