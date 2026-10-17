Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Data

' تعریف کلاس
Public Class ChartData
    Public Property labels As List(Of String)
    Public Property data As List(Of Integer)
End Class

Partial Class Default6
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim labels As New List(Of String)()
            Dim values As New List(Of Integer)()

            Try
                Dim sqlstr As String
                sqlstr = "SELECT        SUM(saat) AS summ, dorehayeGozrande                  FROM trnPersonelDoreTbl " & _
               " GROUP BY dorehayeGozrande, tarixShoru HAVING        (tarixShoru BETWEEN N'1403/10/01' AND N'1404/10/01')"
                Dim dtS As New DataTable
                dtS = ClsDataTable.GetDataTable(sqlstr)
                For i = 0 To dtS.Rows.Count - 1
                    labels.Add(dtS.Rows(i).Item("dorehayeGozrande").ToString())
                    values.Add(Convert.ToInt32(dtS.Rows(i).Item("summ").ToString()))
                Next i

            Catch ex As Exception
                Response.Write("خطا در خواندن داده‌ها: " & ex.Message)
            End Try



            ' ایجاد شیء و مقداردهی
            Dim chartData As New ChartData()
            chartData.labels = labels
            chartData.data = values

            ' تبدیل به JSON و ارسال به کلاینت
            HiddenChartData.Value = New JavaScriptSerializer().Serialize(chartData)

        End If
    End Sub

End Class
