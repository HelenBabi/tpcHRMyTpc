Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports CrystalDecisions.CrystalReports.Engine

Partial Class overhaulSecurityAdminBrowsFrm

    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

        End If
        load_gridAll()
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("overhaulSecurityAdminBrowsFrm.aspx", Val(Session("UserId")))
        If meno_arr(0) = False Then
            Response.Redirect("login_2.aspx")
        End If
    End Sub


    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

#End Region

    Protected Sub btnPrintSelected_Click(sender As Object, e As EventArgs) Handles btnPrintSelected.Click
        ' دریافت رکوردهای انتخاب‌شده از ASPxGridView
        Dim selectedIds = GridWorkTable.GetSelectedFieldValues("Id")

        If selectedIds.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('لطفاً حداقل یک نفر را انتخاب کنید');", True)
            Return
        End If

        ' ساخت لیست کارت‌ها
        Dim cardList As New List(Of CardData)()
        For Each id As Object In selectedIds
            ' گرفتن داده‌های پرسنل از پایگاه داده یا منبع اصلی
            Dim dr As DataRow = GetPersonnelDataById(id) ' تابع دلخواه برای دریافت اطلاعات
            If dr IsNot Nothing Then
                cardList.Add(New CardData With {
                    .FullName = dr("fulname").ToString(),
                    .PersonnelCode = dr("CodePersoneli").ToString(),
                    .PhotoPath = dr("PhotoPath").ToString(),
                    .ExpirationDate = dr("ExpireDateCard").ToString(),
                    .shift = dr("shift").ToString()
                })
            End If
        Next

        ' تبدیل لیست به DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("fulname")
        dt.Columns.Add("CodePersoneli")
        dt.Columns.Add("PhotoPath")
        dt.Columns.Add("ExpireDateCard")
        dt.Columns.Add("shift")

        For Each item In cardList
            dt.Rows.Add(item.FullName, item.PersonnelCode, item.PhotoPath, item.ExpirationDate, item.shift)
        Next

        ' بارگذاری گزارش و ارسال به خروجی
        Dim rpt As New ReportDocument()
        rpt.Load(Server.MapPath("~/Reports/OverhaulHrCard.rpt"))
        rpt.SetDataSource(dt)

        Dim stream As System.IO.Stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "inline; filename=Cards.pdf")
        stream.CopyTo(Response.OutputStream)
        Response.Flush()
        Response.End()

        rpt.Close()
        rpt.Dispose()
    End Sub

    Private Function GetPersonnelDataById(id As Integer) As DataRow
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function



    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulAllVW where omurID = " & Session("omurID") & " "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        GridWorkTable.DataSource = dt
        GridWorkTable.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub
    Protected Sub GridWorkTable_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridWorkTable.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("State"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightGreen
        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightYellow
        ElseIf value = 5 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightPink

        End If


    End Sub
    Protected Sub GridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridWorkTable.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub
End Class
