Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Imports System.IO

Partial Class PrintPersonnelCards
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExportReportToWord()
        End If
    End Sub

    Private Sub ExportReportToWord()
        Try
            Dim dt As DataTable = TryCast(Session("PersonnelCardDataTable"), DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Response.Write("داده‌ای برای نمایش وجود ندارد.")
                Return
            End If

            ' بررسی وجود فایل گزارش
            Dim reportPath As String = Server.MapPath("~/Reports/herasatCard.rpt")
            If Not File.Exists(reportPath) Then
                Response.Write("فایل گزارش وجود ندارد: " & reportPath)
                Return
            End If

            Dim rpt As New ReportDocument()
            rpt.Load(reportPath)
            rpt.SetDataSource(dt)
            rpt.DataSourceConnections.Clear()

            ' صادر کردن به PDF و ارسال به Response
            Dim stream As System.IO.Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "inline; filename=PersonnelCards.pdf")

            Dim buffer As Byte() = New Byte(stream.Length - 1) {}
            stream.Read(buffer, 0, buffer.Length)
            Response.OutputStream.Write(buffer, 0, buffer.Length)
            Response.Flush()
            Response.End()

            ' آزادسازی منابع
            rpt.Close()
            rpt.Dispose()
        Catch ex As Exception
            Response.Clear()
            Response.Write("خطا در تولید گزارش: " & ex.Message)
        End Try
    End Sub

End Class