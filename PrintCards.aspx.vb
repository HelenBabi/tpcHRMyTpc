Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data

Partial Class PrintCards
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExportReportToWord()
        End If
    End Sub

    Private Sub ExportReportToWord()
        Dim dt As DataTable = TryCast(Session("CardDataTable"), DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Response.Write("داده‌ای برای نمایش وجود ندارد.")
            Return
        End If

        Dim rpt As New ReportDocument()

        rpt.Load(Server.MapPath("~/Reports/OverhaulHrCard.rpt"))

        rpt.SetDataSource(dt)
        rpt.DataSourceConnections.Clear()

        ' این متد خودش فرمت Word را تنظیم و ارسال می‌کند:
        rpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "Cards")

        ' بعد از ExportToHttpResponse نیازی به Flush/End نیست، اما اگر دوست دارید:
        Response.Flush()
        Response.End()

        rpt.Close()
        rpt.Dispose()
    End Sub
End Class
