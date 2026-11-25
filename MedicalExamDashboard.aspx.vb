
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Web.Script.Serialization

Partial Class MedicalExamDashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            meno_acc()
        End If

        load_grid()
    End Sub

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("MedicalExamDashboard.aspx", Val(Session("UserId")))
    End Sub

    Private Sub load_grid()
        pnlPersonnel.Visible = False

        If txtSearch.Text.Trim() <> "" Then
            SearchPersonnel(txtSearch.Text.Trim())
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        load_grid()
    End Sub

    Private Sub SearchPersonnel(ByVal searchValue As String)

        Dim dtPersonnel As New DataTable()

        Dim sql As String = "SELECT TOP 1 p.codePersoneli, p.nam, p.family, p.UnitsCode, p.postname, u.UnitsName FROM dbo.personel p LEFT JOIN dbo.tblUnits u ON p.UnitsCode = u.UnitsCode WHERE p.CodeClosed = 0 AND (CONVERT(NVARCHAR(30), p.codePersoneli) = @Search OR p.nam LIKE @LikeSearch OR p.family LIKE @LikeSearch OR (p.nam + N' ' + p.family) LIKE @LikeSearch) ORDER BY p.codePersoneli"

        Using cnn As New SqlConnection(ClsConnection.GetConn())
            Using cmd As New SqlCommand(sql, cnn)

                cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 30).Value = searchValue
                cmd.Parameters.Add("@LikeSearch", SqlDbType.NVarChar, 150).Value = "%" & searchValue & "%"

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtPersonnel)
                End Using

            End Using
        End Using

        If dtPersonnel.Rows.Count = 0 Then
            lblMessage.Text = "پرسنلی با مشخصات واردشده پیدا نشد."
            lblMessage.ForeColor = Drawing.Color.Red
            pnlPersonnel.Visible = False
            Return
        End If

        Dim pRow As DataRow = dtPersonnel.Rows(0)
        Dim personnelCode As String = Convert.ToString(pRow("codePersoneli"))

        lblFullName.Text = Server.HtmlEncode(Convert.ToString(pRow("nam")) & " " & Convert.ToString(pRow("family")))
        lblPersonnelCode.Text = Server.HtmlEncode(personnelCode)

        If IsDBNull(pRow("UnitsName")) Then
            lblUnit.Text = "-"
        Else
            lblUnit.Text = Server.HtmlEncode(Convert.ToString(pRow("UnitsName")))
        End If

        If IsDBNull(pRow("postname")) Then
            lblPost.Text = "-"
        Else
            lblPost.Text = Server.HtmlEncode(Convert.ToString(pRow("postname")))
        End If

        LoadMedicalHistory(personnelCode)

        pnlPersonnel.Visible = True
        lblMessage.Text = ""

    End Sub

    Private Sub LoadMedicalHistory(ByVal personnelCode As String)

        Dim dt As New DataTable()

        Dim sql As String = "SELECT ID, C002 AS VisitDate, C006 AS ExamType, C038 AS LabDate, C031 AS Weight, C033 AS BMI, C034 AS Systolic, C035 AS Diastolic, C039 AS Chol, C040 AS TG, C041 AS HDL, C042 AS LDL, C043 AS FBS, C044 AS HbA1C, C045 AS BUN, C046 AS Creat, C048 AS AST, C049 AS ALT, C050 AS ALP, C051 AS RBC, C052 AS WBC, C053 AS Hb, C054 AS Hct, C055 AS Plt, C087 AS NutritionRecommendation, C093 AS FinalOpinion, C094 AS Recommendation, C095 AS FinalConditions FROM dbo.MedicalExamRaw WHERE LTRIM(RTRIM(C008)) = @Code ORDER BY C002 DESC, ID DESC"

        Using cnn As New SqlConnection(ClsConnection.GetConn())
            Using cmd As New SqlCommand(sql, cnn)

                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = personnelCode

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

            End Using
        End Using

        gvHistory.DataSource = dt
        gvHistory.DataBind()

        SetLatestValues(dt)
        LoadRecommendations(dt)
        BuildMedicalCharts(dt)

    End Sub

    Private Sub SetLatestValues(ByVal dt As DataTable)

        lblWeight.Text = "-"
        lblBMI.Text = "-"
        lblBloodPressure.Text = "-"
        lblLastExam.Text = "-"

        If dt.Rows.Count = 0 Then Return

        For Each row As DataRow In dt.Rows

            If lblWeight.Text = "-" AndAlso HasValue(row("Weight")) Then
                lblWeight.Text = Server.HtmlEncode(Convert.ToString(row("Weight")))
            End If

            If lblBMI.Text = "-" AndAlso HasValue(row("BMI")) Then
                lblBMI.Text = Server.HtmlEncode(Convert.ToString(row("BMI")))
            End If

            If lblBloodPressure.Text = "-" AndAlso HasValue(row("Systolic")) AndAlso HasValue(row("Diastolic")) Then
                lblBloodPressure.Text = Server.HtmlEncode(Convert.ToString(row("Systolic")) & " / " & Convert.ToString(row("Diastolic")))
            End If

            If lblLastExam.Text = "-" AndAlso HasValue(row("VisitDate")) Then
                lblLastExam.Text = Server.HtmlEncode(Convert.ToString(row("VisitDate")))
            End If

        Next

    End Sub

    Private Function HasValue(ByVal value As Object) As Boolean

        If value Is Nothing OrElse IsDBNull(value) Then Return False

        Return Convert.ToString(value).Trim() <> ""

    End Function

    Private Sub LoadRecommendations(ByVal dt As DataTable)

        Dim rec As New DataTable()

        rec.Columns.Add("Title")
        rec.Columns.Add("Body")
        rec.Columns.Add("Date")

        For Each r As DataRow In dt.Rows

            Dim visitDate As String = Convert.ToString(r("VisitDate"))

            AddRecommendation(rec, "توصیه تغذیه‌ای", r("NutritionRecommendation"), visitDate)
            AddRecommendation(rec, "نظر نهایی پزشکی", r("FinalOpinion"), visitDate)
            AddRecommendation(rec, "توصیه پزشکی", r("Recommendation"), visitDate)
            AddRecommendation(rec, "شرایط و توضیحات نظر نهایی", r("FinalConditions"), visitDate)

        Next

        rptRecommendations.DataSource = rec
        rptRecommendations.DataBind()

        lblNoRecommendation.Visible = (rec.Rows.Count = 0)

    End Sub

    Private Sub AddRecommendation(ByVal dt As DataTable, ByVal title As String, ByVal value As Object, ByVal visitDate As String)

        If value Is Nothing OrElse IsDBNull(value) Then Return

        Dim body As String = Convert.ToString(value).Trim()

        If body = "" Then Return

        Dim r As DataRow = dt.NewRow()

        r("Title") = title
        r("Body") = Server.HtmlEncode(body)
        r("Date") = Server.HtmlEncode(visitDate)

        dt.Rows.Add(r)

    End Sub

    Private Sub BuildMedicalCharts(ByVal dt As DataTable)

        Dim charts As New Dictionary(Of String, Object)()

        charts("weight") = CreateChartData(dt, "VisitDate", New String() {"Weight", "BMI"}, New String() {"وزن", "شاخص توده بدنی"})
        charts("glucose") = CreateChartData(dt, "LabDate", New String() {"FBS", "HbA1C"}, New String() {"قند خون ناشتا", "هموگلوبین A1C"})
        charts("lipids") = CreateChartData(dt, "LabDate", New String() {"Chol", "TG", "HDL", "LDL"}, New String() {"کلسترول", "تری‌گلیسیرید", "HDL", "LDL"})
        charts("pressure") = CreateChartData(dt, "VisitDate", New String() {"Systolic", "Diastolic"}, New String() {"فشار سیستول", "فشار دیاستول"})
        charts("liver") = CreateChartData(dt, "LabDate", New String() {"AST", "ALT", "ALP", "BUN", "Creat"}, New String() {"AST", "ALT", "ALP", "BUN", "کراتینین"})
        charts("cbc") = CreateChartData(dt, "LabDate", New String() {"RBC", "WBC", "Hb", "Hct", "Plt"}, New String() {"RBC", "WBC", "هموگلوبین", "هماتوکریت", "پلاکت"})

        Dim serializer As New JavaScriptSerializer()
        serializer.MaxJsonLength = Integer.MaxValue

        litChartData.Text = "<script type='text/javascript'>var medicalCharts = " & serializer.Serialize(charts) & ";</script>"

    End Sub

    Private Function CreateChartData(ByVal dt As DataTable, ByVal dateColumn As String, ByVal columns As String(), ByVal titles As String()) As Object

        Dim labels As New List(Of String)()
        Dim datasets As New List(Of Object)()

        Dim rows() As DataRow = dt.Select(dateColumn & " IS NOT NULL AND " & dateColumn & " <> ''", dateColumn & " ASC, ID ASC")

        For Each row As DataRow In rows
            labels.Add(Convert.ToString(row(dateColumn)).Trim())
        Next

        For colIndex As Integer = 0 To columns.Length - 1

            Dim values As New List(Of Object)()

            For Each row As DataRow In rows
                values.Add(GetNumericValue(row(columns(colIndex))))
            Next

            Dim item As New Dictionary(Of String, Object)()

            item("label") = titles(colIndex)
            item("data") = values
            item("fill") = False
            item("lineTension") = 0.2
            item("spanGaps") = True

            datasets.Add(item)

        Next

        Dim result As New Dictionary(Of String, Object)()

        result("labels") = labels
        result("datasets") = datasets

        Return result

    End Function

    Private Function GetNumericValue(ByVal value As Object) As Object

        If value Is Nothing OrElse IsDBNull(value) Then Return Nothing

        Dim s As String = Convert.ToString(value).Trim()

        If s = "" Then Return Nothing

        s = s.Replace("٬", "")
        s = s.Replace(",", "")
        s = s.Replace("٫", ".")

        s = s.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2")
        s = s.Replace("۳", "3").Replace("۴", "4").Replace("۵", "5")
        s = s.Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9")

        s = s.Replace("٠", "0").Replace("١", "1").Replace("٢", "2")
        s = s.Replace("٣", "3").Replace("٤", "4").Replace("٥", "5")
        s = s.Replace("٦", "6").Replace("٧", "7").Replace("٨", "8").Replace("٩", "9")

        Dim number As Double

        If Double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, number) Then
            Return number
        End If

        Return Nothing

    End Function

End Class
