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

        ' تنظیم مسیر پوشه‌ی موقت اگر لازم است
        rpt.DataSourceConnections.Clear()

        ' صادر کردن به PDF و ارسال به Response
        Dim stream As System.IO.Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "inline; filename=Cards.pdf")
        Dim buffer As Byte() = New Byte(stream.Length - 1) {}
        stream.Read(buffer, 0, buffer.Length)
        Response.OutputStream.Write(buffer, 0, buffer.Length)
        Response.Flush()
        Response.End()

        ' آزادسازی منابع
        rpt.Close()
        rpt.Dispose()
    End Sub
End Class
