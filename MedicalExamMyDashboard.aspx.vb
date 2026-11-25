
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Web.Script.Serialization

Partial Class MedicalExamMyDashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then

            meno_acc()

            Dim personnelCode As String = ""

            If Session("codePersoneli") IsNot Nothing Then
                personnelCode = Convert.ToString(Session("codePersoneli")).Trim()
            End If

            If personnelCode = "" Then
                lblMessage.Text = "کد پرسنلی شما در اطلاعات ورود ثبت نشده است."
                pnlPersonnel.Visible = False
                Return
            End If

            SearchPersonnel(personnelCode)

        End If

    End Sub

    Private Sub meno_acc()

        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("MedicalExamMyDashboard.aspx", Val(Session("UserId")))

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs)

        Response.Redirect("~/index.aspx")

    End Sub

    '========================================
    ' دریافت اطلاعات پرسنلی
    '========================================
    Private Sub SearchPersonnel(ByVal personnelCode As String)

        Dim codeValue As Long

        If Not Long.TryParse(personnelCode, codeValue) Then
            lblMessage.Text = "کد پرسنلی ثبت‌شده برای شما معتبر نیست."
            pnlPersonnel.Visible = False
            Return
        End If

        Dim dtPersonnel As New DataTable()

        Dim sql As String = "SELECT TOP 1 p.codePersoneli, p.nam, p.family, p.postname, u.UnitsName " & _
                            "FROM dbo.personel p " & _
                            "LEFT JOIN dbo.tblUnits u ON p.UnitsCode = u.UnitsCode " & _
                            "WHERE p.CodeClosed = 0 AND p.codePersoneli = @Code"

        Using cnn As New SqlConnection(ClsConnection.GetConn())

            Using cmd As New SqlCommand(sql, cnn)

                cmd.Parameters.Add("@Code", SqlDbType.BigInt).Value = codeValue

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtPersonnel)
                End Using

            End Using

        End Using

        If dtPersonnel.Rows.Count = 0 Then
            lblMessage.Text = "اطلاعات پرسنلی شما در سیستم پیدا نشد."
            pnlPersonnel.Visible = False
            Return
        End If

        Dim pRow As DataRow = dtPersonnel.Rows(0)

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

    '========================================
    ' دریافت سوابق پزشکی
    '========================================
    Private Sub LoadMedicalHistory(ByVal personnelCode As String)

        Dim dt As New DataTable()

        Dim sql As String = "SELECT " & _
                            "ID, " & _
                            "C002 AS VisitDate, " & _
                            "C006 AS ExamType, " & _
                            "C038 AS LabDate, " & _
                            "C031 AS Weight, " & _
                            "C033 AS BMI, " & _
                            "C034 AS Systolic, " & _
                            "C035 AS Diastolic, " & _
                            "C039 AS Chol, " & _
                            "C040 AS TG, " & _
                            "C041 AS HDL, " & _
                            "C042 AS LDL, " & _
                            "C043 AS FBS, " & _
                            "C044 AS HbA1C, " & _
                            "C045 AS BUN, " & _
                            "C046 AS Creat, " & _
                            "C048 AS AST, " & _
                            "C049 AS ALT, " & _
                            "C050 AS ALP, " & _
                            "C051 AS RBC, " & _
                            "C052 AS WBC, " & _
                            "C053 AS Hb, " & _
                            "C054 AS Hct, " & _
                            "C055 AS Plt, " & _
                            "C087 AS NutritionRecommendation, " & _
                            "C093 AS FinalOpinion, " & _
                            "C094 AS Recommendation, " & _
                            "C095 AS FinalConditions " & _
                            "FROM dbo.MedicalExamRaw " & _
                            "WHERE LTRIM(RTRIM(C008)) = @Code " & _
                            "ORDER BY C002 DESC, ID DESC"

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

    '========================================
    ' آخرین مقادیر سلامت
    '========================================
    Private Sub SetLatestValues(ByVal dt As DataTable)

        lblWeight.Text = "-"
        lblBMI.Text = "-"
        lblBloodPressure.Text = "-"
        lblLastExam.Text = "-"

        If dt.Rows.Count = 0 Then
            Return
        End If

        For Each row As DataRow In dt.Rows

            If lblWeight.Text = "-" Then
                If HasValue(row("Weight")) Then
                    lblWeight.Text = Server.HtmlEncode(Convert.ToString(row("Weight")))
                End If
            End If

            If lblBMI.Text = "-" Then
                If HasValue(row("BMI")) Then
                    lblBMI.Text = Server.HtmlEncode(Convert.ToString(row("BMI")))
                End If
            End If

            If lblBloodPressure.Text = "-" Then

                If HasValue(row("Systolic")) AndAlso HasValue(row("Diastolic")) Then

                    lblBloodPressure.Text = Server.HtmlEncode( _
                        Convert.ToString(row("Systolic")) & " / " & Convert.ToString(row("Diastolic")))

                End If

            End If

            If lblLastExam.Text = "-" Then
                If HasValue(row("VisitDate")) Then
                    lblLastExam.Text = Server.HtmlEncode(Convert.ToString(row("VisitDate")))
                End If
            End If

            If lblWeight.Text <> "-" AndAlso _
               lblBMI.Text <> "-" AndAlso _
               lblBloodPressure.Text <> "-" AndAlso _
               lblLastExam.Text <> "-" Then

                Exit For

            End If

        Next

    End Sub

    Private Function HasValue(ByVal value As Object) As Boolean

        If value Is Nothing OrElse IsDBNull(value) Then
            Return False
        End If

        Return Convert.ToString(value).Trim() <> ""

    End Function

    '========================================
    ' نمایش توصیه های پزشکی
    '========================================
    Private Sub LoadRecommendations(ByVal dt As DataTable)

        Dim dtRecommendations As New DataTable()

        dtRecommendations.Columns.Add("Title", GetType(String))
        dtRecommendations.Columns.Add("Body", GetType(String))
        dtRecommendations.Columns.Add("Date", GetType(String))

        For Each row As DataRow In dt.Rows

            Dim visitDate As String = Convert.ToString(row("VisitDate"))

            If HasValue(row("NutritionRecommendation")) Then
                AddRecommendation(dtRecommendations, "توصیه تغذیه", row("NutritionRecommendation"), visitDate)
            End If

            If HasValue(row("FinalOpinion")) Then
                AddRecommendation(dtRecommendations, "نظر نهایی پزشک", row("FinalOpinion"), visitDate)
            End If

            If HasValue(row("Recommendation")) Then
                AddRecommendation(dtRecommendations, "توصیه پزشکی", row("Recommendation"), visitDate)
            End If

            If HasValue(row("FinalConditions")) Then
                AddRecommendation(dtRecommendations, "شرایط و توضیحات نهایی", row("FinalConditions"), visitDate)
            End If

        Next

        rptRecommendations.DataSource = dtRecommendations
        rptRecommendations.DataBind()

        pnlNoRecommendations.Visible = (dtRecommendations.Rows.Count = 0)

    End Sub

    Private Sub AddRecommendation(ByVal dt As DataTable, _
                                  ByVal title As String, _
                                  ByVal body As Object, _
                                  ByVal visitDate As String)

        Dim newRow As DataRow = dt.NewRow()

        newRow("Title") = title
        newRow("Body") = Convert.ToString(body)
        newRow("Date") = visitDate

        dt.Rows.Add(newRow)

    End Sub

    '========================================
    ' ساخت اطلاعات نمودارها
    '========================================
    Private Sub BuildMedicalCharts(ByVal dt As DataTable)

        Dim charts As New Dictionary(Of String, Object)()

        charts.Add("weight", CreateChartData(dt, "VisitDate", _
            New String() {"Weight", "BMI"}, _
            New String() {"وزن", "BMI"}))

        charts.Add("glucose", CreateChartData(dt, "LabDate", _
            New String() {"FBS", "HbA1C"}, _
            New String() {"قند ناشتا (FBS)", "HbA1C"}))

        charts.Add("lipids", CreateChartData(dt, "LabDate", _
            New String() {"Chol", "TG", "HDL", "LDL"}, _
            New String() {"کلسترول", "تری‌گلیسیرید", "HDL", "LDL"}))

        charts.Add("pressure", CreateChartData(dt, "VisitDate", _
            New String() {"Systolic", "Diastolic"}, _
            New String() {"فشار سیستولیک", "فشار دیاستولیک"}))

        charts.Add("liver", CreateChartData(dt, "LabDate", _
            New String() {"AST", "ALT", "ALP", "BUN", "Creat"}, _
            New String() {"AST", "ALT", "ALP", "BUN", "Creat"}))

        charts.Add("cbc", CreateChartData(dt, "LabDate", _
            New String() {"RBC", "WBC", "Hb", "Hct", "Plt"}, _
            New String() {"RBC", "WBC", "Hb", "Hct", "Plt"}))

        Dim serializer As New JavaScriptSerializer()
        serializer.MaxJsonLength = Integer.MaxValue

        Dim json As String = serializer.Serialize(charts)

        litChartData.Text = "<script type=""text/javascript"">var medicalCharts = " & json & ";</script>"

    End Sub

    '========================================
    ' ساخت یک نمودار با چند شاخص
    '========================================
    Private Function CreateChartData(ByVal dt As DataTable, _
                                     ByVal dateColumn As String, _
                                     ByVal fields As String(), _
                                     ByVal titles As String()) As Dictionary(Of String, Object)

        Dim result As New Dictionary(Of String, Object)()

        Dim labels As New List(Of String)()
        Dim datasets As New List(Of Object)()

        Dim sortedRows As DataTable = dt.Clone()

        If dt.Rows.Count > 0 Then

            Dim view As New DataView(dt)
            view.Sort = dateColumn & " ASC, ID ASC"
            sortedRows = view.ToTable()

        End If

        Dim validRows As New List(Of DataRow)()

        For Each row As DataRow In sortedRows.Rows

            If HasValue(row(dateColumn)) Then
                validRows.Add(row)
                labels.Add(Convert.ToString(row(dateColumn)).Trim())
            End If

        Next

        For i As Integer = 0 To fields.Length - 1

            Dim values As New List(Of Object)()

            For Each row As DataRow In validRows

                values.Add(GetNumericValue(row(fields(i))))

            Next

            Dim dataset As New Dictionary(Of String, Object)()

            dataset.Add("label", titles(i))
            dataset.Add("data", values)

            datasets.Add(dataset)

        Next

        result.Add("labels", labels)
        result.Add("datasets", datasets)

        Return result

    End Function

    '========================================
    ' تبدیل اعداد فارسی و رشته به عدد
    '========================================
    Private Function GetNumericValue(ByVal value As Object) As Object

        If value Is Nothing OrElse IsDBNull(value) Then
            Return Nothing
        End If

        Dim s As String = Convert.ToString(value).Trim()

        If s = "" Then
            Return Nothing
        End If

        s = s.Replace("۰", "0")
        s = s.Replace("۱", "1")
        s = s.Replace("۲", "2")
        s = s.Replace("۳", "3")
        s = s.Replace("۴", "4")
        s = s.Replace("۵", "5")
        s = s.Replace("۶", "6")
        s = s.Replace("۷", "7")
        s = s.Replace("۸", "8")
        s = s.Replace("۹", "9")

        s = s.Replace("٠", "0")
        s = s.Replace("١", "1")
        s = s.Replace("٢", "2")
        s = s.Replace("٣", "3")
        s = s.Replace("٤", "4")
        s = s.Replace("٥", "5")
        s = s.Replace("٦", "6")
        s = s.Replace("٧", "7")
        s = s.Replace("٨", "8")
        s = s.Replace("٩", "9")

        s = s.Replace("٬", "")
        s = s.Replace(",", "")
        s = s.Replace("٫", ".")

        Dim numberValue As Double

        If Double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, numberValue) Then
            Return numberValue
        End If

        Return Nothing

    End Function

End Class
