Imports System.Data
Imports System.Data.SqlClient
Partial Class registerReportFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_gridAll()
    End Sub
    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT * from amarReshteVW"
        dt = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()

        ' CntTyp()
        'Dim cntt As Integer = dt.Rows.Count
        'lblTotal.Text = "تعداد  = " & cntt
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
