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


Partial Class SelectPersonnel
    Inherits System.Web.UI.Page
    Private Const PrintedListKey As String = "PrintedCardList"

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ' 1. جمع‌آوری داده‌ها
        Dim cardList As New List(Of CardData)
        For Each row As GridViewRow In gridPersonnel.Rows
            Dim chk = CType(row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing AndAlso chk.Checked Then
                Dim dk = gridPersonnel.DataKeys(row.RowIndex)
                cardList.Add(New CardData With {
                    .FullName = dk("fulname").ToString(),
                    .PersonnelCode = dk("codePersoneli").ToString(),
                    .PhotoPath = dk("PhotoPath").ToString(),
                .ExpirationDate = dk("expireDateCard").ToString(),
                .shift = dk("shift").ToString(),
                    .omur = dk("omur").ToString()
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
        dt.Columns.Add("fulname", GetType(String))
        dt.Columns.Add("codePersoneli", GetType(String))
        dt.Columns.Add("PhotoPath", GetType(String))
        dt.Columns.Add("Shift", GetType(String))
        dt.Columns.Add("expireDateCard", GetType(String))
        dt.Columns.Add("omur", GetType(String))

        For Each c As CardData In cardList
            dt.Rows.Add(c.FullName, c.PersonnelCode, c.PhotoPath, c.shift, c.ExpirationDate, c.omur)
        Next

        'Session("CardDataTable") = dt
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openPopup",
        '    "window.open('PrintCards.aspx','_blank','width=800,height=600');", True)

        ' ذخیره لیست کد پرسنلی چاپ‌شده در Session
        Dim printedIds = cardList.Select(Function(c) c.PersonnelCode).ToList()
        Session(PrintedListKey) = printedIds

        ' فعال کردن دکمه علامت‌گذاری
        btnMarkPrinted.Enabled = True

        ' فرض می‌کنیم rptCards.rpt داخل پوشه Reports است
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
        Dim sqlstr As String = "SELECT * FROM overhaulHerastVW WHERE omurID = " & Session("omurID")
        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)
        gridPersonnel.DataSource = dt
        gridPersonnel.DataBind()
    End Sub

    Protected Sub gridPersonnel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridPersonnel.PageIndexChanging
        gridPersonnel.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub gridPersonnel_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gridPersonnel.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim ul As New System.Web.UI.HtmlControls.HtmlGenericControl("ul")
            ul.Attributes("class") = "pagination justify-content-center"

            For Each ctrl As Control In e.Row.Controls(0).Controls
                Dim li As New System.Web.UI.HtmlControls.HtmlGenericControl("li")
                li.Attributes("class") = "page-item"

                If TypeOf ctrl Is LinkButton Then
                    Dim link As LinkButton = CType(ctrl, LinkButton)
                    Dim a As New System.Web.UI.HtmlControls.HtmlAnchor()
                    a.HRef = "javascript:__doPostBack('" & link.UniqueID & "','')"
                    a.InnerText = link.Text
                    a.Attributes("class") = "page-link"
                    li.Controls.Add(a)
                ElseIf TypeOf ctrl Is LiteralControl Then
                    Dim lit As LiteralControl = CType(ctrl, LiteralControl)
                    Dim span As New System.Web.UI.HtmlControls.HtmlGenericControl("span")
                    span.InnerText = lit.Text
                    span.Attributes("class") = "page-link disabled"
                    li.Controls.Add(span)
                End If

                ul.Controls.Add(li)
            Next

            e.Row.Controls(0).Controls.Clear()
            e.Row.Controls(0).Controls.Add(ul)
        End If
    End Sub



End Class


