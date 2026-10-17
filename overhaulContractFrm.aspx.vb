Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports CrystalDecisions.Web


Partial Class overhaulContractFrm
    Inherits System.Web.UI.Page
    Private Const PrintedListKey As String = "PrintedCardList"

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ' 1. جمع‌آوری داده‌ها
        Dim cardList As New List(Of CardDataHr)
        For Each row As GridViewRow In gridPersonnel.Rows
            Dim chk = CType(row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing AndAlso chk.Checked Then
                Dim dk = gridPersonnel.DataKeys(row.RowIndex)
                cardList.Add(New CardDataHr With {
                                   .shift = dk("shift").ToString(),
                                       .tarixStart = dk("tarixStart").ToString(),
                .Address = dk("Address").ToString(),
                .BankCardNo = dk("BankCardNo").ToString(),
                                   .BirthDay = dk("BirthDay").ToString(),
                                   .chart = dk("chart").ToString(),
                                   .city = dk("city").ToString(),
                                   .codePersoneli = dk("codePersoneli").ToString(),
                                   .FatherName = dk("FatherName").ToString(),
                                   .FirstName = dk("FirstName").ToString(),
                                   .IBAN = dk("IBAN").ToString(),
                                   .LastName = dk("LastName").ToString(),
                                   .madrak = dk("madrak").ToString(),
                                   .MilitaryName = dk("MilitaryName").ToString(),
                                   .Nationality = dk("Nationality").ToString(),
                                   .Price = dk("Price").ToString(),
                                   .province = dk("province").ToString(),
                                   .reshte = dk("reshte").ToString(),
                                   .shSh = dk("shSh").ToString(),
                                   .SSN = dk("SSN").ToString()
                                                               })
            End If
        Next
        If cardList.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('لطفاً حداقل یک نفر را انتخاب کنید');", True)
            Return
        End If
        ' بعد از این خط:
        ' If cardList.Count = 0 Then ...

        ' تبدیل List(Of CardData) به DataTable

    Dim dt As New DataTable()
      dt.Columns.Add("reshte", GetType(String))

     dt.Columns.Add("BankCardNo", GetType(String))
        dt.Columns.Add("IBAN", GetType(String))
        dt.Columns.Add("SSN", GetType(String))
        dt.Columns.Add("Address", GetType(String))
        dt.Columns.Add("chart", GetType(String))
        dt.Columns.Add("Price", GetType(String))

        dt.Columns.Add("FirstName", GetType(String))
        dt.Columns.Add("LastName", GetType(String))
        dt.Columns.Add("FatherName", GetType(String))
        dt.Columns.Add("Nationality", GetType(String))
        dt.Columns.Add("shSh", GetType(String))
        dt.Columns.Add("BirthDay", GetType(String))


        dt.Columns.Add("province", GetType(String))
        dt.Columns.Add("codePersoneli", GetType(String))
        dt.Columns.Add("city", GetType(String))
        dt.Columns.Add("Shift", GetType(String))
        dt.Columns.Add("MilitaryName", GetType(String))
        dt.Columns.Add("madrak", GetType(String))
        dt.Columns.Add("tarixStart", GetType(String))


        For Each c As CardDataHr In cardList
            dt.Rows.Add(c.Address, c.codePersoneli, c.BankCardNo, c.BirthDay, c.chart, c.city, c.FatherName, c.FirstName, c.IBAN, c.LastName, c.madrak, c.MilitaryName, c.Nationality, c.Price, c.province, c.reshte, c.shift, c.shSh, c.SSN, c.tarixStart)
        Next

        Session("CardDataTable") = dt
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openPopup",
            "window.open('PrintCardsHr.aspx','_blank','width=800,height=600');", True)

        '' ذخیره لیست کد پرسنلی چاپ‌شده در Session
        'Dim printedIds = cardList.Select(Function(c) c.PersonnelCode).ToList()
        'Session(PrintedListKey) = printedIds

        '' فعال کردن دکمه علامت‌گذاری
        'btnMarkPrinted.Enabled = True

        '' فرض می‌کنیم rptCards.rpt داخل پوشه Reports است
        'Dim rpt As New ReportDocument()
        'rpt.Load(Server.MapPath("~/Reports/OverhaulHrCard.rpt"))
        'rpt.SetDataSource(dt)

        '' تنظیم مسیر پوشه‌ی موقت اگر لازم است
        'rpt.DataSourceConnections.Clear()

        '' صادر کردن به PDF و ارسال به Response
        'Dim stream As System.IO.Stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("Content-Disposition", "inline; filename=Cards.pdf")
        'Dim buffer As Byte() = New Byte(stream.Length - 1) {}
        'stream.Read(buffer, 0, buffer.Length)
        'Response.OutputStream.Write(buffer, 0, buffer.Length)
        'Response.Flush()
        'Response.End()

        '' آزادسازی منابع
        'rpt.Close()
        'rpt.Dispose()


    End Sub
    Protected Sub btnMarkPrinted_Click(sender As Object, e As EventArgs) Handles btnMarkPrinted.Click
        Dim printedIds = TryCast(Session(PrintedListKey), List(Of String))
        If printedIds Is Nothing OrElse printedIds.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('هیچ رکوردی برای علامت‌گذاری وجود ندارد');", True)
            Return
        End If

        ' ساختن یک رشته مانند ('P001','P005',...)
        Dim inClause As String = String.Join(",", printedIds.Select(Function(id) "'" & id & "'"))



        ' مثال: به‌روزرسانی وضعیت در جدول هدف (مثلاً PrintedCardsLog)
        Dim updateSql As String = " INSERT INTO overhaulKartablTbl                          (sender, omurId, overhaulId, kartablStat) VALUES        (102, 103, 1, 1)"

        ' اجرای فرمان SQL
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("YourConnStr").ConnectionString)
            Using cmd As New SqlCommand(updateSql, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' پاک کردن Session و غیرفعال کردن دکمه
        Session.Remove(PrintedListKey)
        btnMarkPrinted.Enabled = False

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('وضعیت رکوردها با موفقیت به‌روز شد');", True)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Dim baseSql As String = "SELECT * FROM overhaulHerastVW WHERE codePersoneli IS NOT NULL ORDER BY codePersoneli"

        ' اگر چیزی وارد جستجو شده باشد
        If Not String.IsNullOrEmpty(txtSearch.Text.Trim()) Then
            Dim keyword As String = txtSearch.Text.Trim().Replace("'", "''")
            baseSql &= " AND (fulname LIKE N'%" & keyword & "%' OR codePersoneli LIKE N'%" & keyword & "%')"

        End If

        Dim dt As DataTable = ClsDataTable.GetDataTable(baseSql)
        lblTotalCount.Text = "تعداد کل: " & dt.Rows.Count.ToString() & " نفر"

        gridPersonnel.DataSource = dt
        gridPersonnel.DataBind()
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        gridPersonnel.PageIndex = 0 ' بازگشت به صفحه اول هنگام جستجو
        BindGrid()
    End Sub




    'Protected Sub gridPersonnel_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gridPersonnel.RowCreated
    '    If e.Row.RowType = DataControlRowType.Pager Then
    '        Dim ul As New System.Web.UI.HtmlControls.HtmlGenericControl("ul")
    '        ul.Attributes("class") = "pagination justify-content-center"

    '        For Each ctrl As Control In e.Row.Controls(0).Controls
    '            Dim li As New System.Web.UI.HtmlControls.HtmlGenericControl("li")
    '            li.Attributes("class") = "page-item"

    '            If TypeOf ctrl Is LinkButton Then
    '                Dim link As LinkButton = CType(ctrl, LinkButton)
    '                Dim a As New System.Web.UI.HtmlControls.HtmlAnchor()
    '                a.HRef = "javascript:__doPostBack('" & link.UniqueID & "','')"
    '                a.InnerText = link.Text
    '                a.Attributes("class") = "page-link"
    '                li.Controls.Add(a)
    '            ElseIf TypeOf ctrl Is LiteralControl Then
    '                Dim lit As LiteralControl = CType(ctrl, LiteralControl)
    '                Dim span As New System.Web.UI.HtmlControls.HtmlGenericControl("span")
    '                span.InnerText = lit.Text
    '                span.Attributes("class") = "page-link disabled"
    '                li.Controls.Add(span)
    '            End If

    '            ul.Controls.Add(li)
    '        Next

    '        e.Row.Controls(0).Controls.Clear()
    '        e.Row.Controls(0).Controls.Add(ul)
    '    End If
    'End Sub

    Protected Sub gridPersonnel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gridPersonnel.PageIndex = e.NewPageIndex
        BindGrid() ' یا هر متدی که اطلاعات را بارگذاری می‌کند
    End Sub

    Protected Sub gridPersonnel_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridPersonnel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim img As Image = CType(e.Row.FindControl("imgPhoto"), Image)
            If img IsNot Nothing Then
                Dim nationality As String = gridPersonnel.DataKeys(e.Row.RowIndex)("Nationality").ToString()
                Dim relativeUrl As String = "~/Attachment/" & nationality & "/PersonalPic.jpg"

                Dim physicalPath As String = Server.MapPath(relativeUrl)
                If IO.File.Exists(physicalPath) Then
                    img.ImageUrl = ResolveUrl(relativeUrl)
                Else
                    img.ImageUrl = ResolveUrl("~/Attachment/default.png")
                End If
            End If
        End If
    End Sub


End Class


