Imports System.Configuration
imports  Stimulsoft.Report
Imports Stimulsoft.Report.Components
Imports Stimulsoft.Report.Web
Partial Class frmPgReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim RpName As String = Session("RpName")
        RpName += ".mrt"
        Dim report As New StiReport()
        report.Load(Server.MapPath("~/Report_File/" + RpName))
        report.Compile()
        If Not Me.IsPostBack Then
            Try
                Select Case RpName
                    Case "report_browsSysFull.mrt"

                        Dim sqlwhere As String = Request.QueryString("PwhereSTR")
                        Dim sqlwhere2 As String = Request.QueryString("PwhereSTR2")
                        report("sqlwhere") = "'" + sqlwhere + "'"
                        report("sqlwhere2") = "'" + sqlwhere2 + "'"
                End Select
                If (RpName <> String.Empty) Then
                    report.Dictionary.Databases.Clear()
                    report.Dictionary.Databases.Add(New Stimulsoft.Report.Dictionary.StiSqlDatabase("SQL2008", ConfigurationManager.ConnectionStrings(1).ConnectionString))
                    report.Render(False)
                    StiWebViewer_repo.Report = report
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub
End Class
