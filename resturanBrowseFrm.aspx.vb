Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Globalization
Imports System.IO
Imports System.Text

Partial Class resturanBrowseFrm
    Inherits System.Web.UI.Page
    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String
    Dim dt2, dt1, dt3 As New DataTable
    Dim dtExportCache As DataTable ' کش کردن داده‌ها برای خروجی اکسل

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            meno_acc()
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            loadgrdNow(a, b)
        Else
            load_grid()
        End If
    End Sub

#Region "All pages"
    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        meno_arr = ClsAccounting.meno_acc("resturanBrowseFrm.aspx", Val(Session("UserId")))
    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
    End Sub
#End Region

    Private Sub loadgrdNow(ByVal dat1 As String, ByVal dat2 As String)
        Dim dt5 As DataTable
        Dim sqlstr As String = "select * from resturanRepNowVW WHERE (tarix Between '" & dat1 & "' And '" & dat2 & "') ORDER BY tarix"
        dt5 = ClsDataTable.GetDataTableBoad5(sqlstr)
        ASPxGrvHead.DataSource = dt5
        ASPxGrvHead.DataBind()

        ' کش کردن داده‌ها برای خروجی اکسل
        dtExportCache = dt5.Copy()

        GridView3.Visible = True
        Dim dtg As New DataTable
        Dim sqlstrg As String = "select * from dashboardVW WHERE (tarix Between '" & dat1 & "' And '" & dat2 & "') ORDER BY tarix"
        dtg = ClsDataTable.GetDataTableBoad5(sqlstrg)
        If dtg.Rows.Count > 0 Then
            GridView3.DataSource = dtg
            GridView3.DataBind()
        End If

        Dim sqlstrCount As String = "SELECT SUM(quantity) AS summ, tarix FROM dashboardVW " & _
                 "GROUP BY tarix HAVING (tarix Between N'" & dat1 & "' And N'" & dat2 & "') ORDER BY tarix"
        Dim dts As New DataTable
        dts = ClsDataTable.GetDataTableBoad5(sqlstrCount)
        If dts.Rows.Count > 0 Then
            Dim cntt As Integer = dts.Rows(0).Item("summ").ToString()
            lblTotal.Text = "جمع كل = " & cntt
        End If
    End Sub

    Private Sub load_grid()
        datS = Request.Form("datepicker")
        dateE = Request.Form("datepicker2")
        lblDateS.Text = datS
        lbldateE.Text = dateE

        If lblDateS.Text = "" Or lbldateE.Text = "" Then
            Exit Sub
        End If

        If Len(lblDateS.Text) < 10 Then
            datS1 = ClsDateTime.setDate(datS)
        End If

        If Len(lbldateE.Text) < 10 Then
            dateE1 = ClsDateTime.setDate(dateE)
        End If

        If Len(lblDateS.Text) = 10 Then
            datS1 = datS
        End If

        If Len(lbldateE.Text) = 10 Then
            dateE1 = dateE
        End If

        Dim sqlstr22 As String = ""
        sqlstr22 = "select * from resturanRepVW WHERE (tarix Between '" & datS1 & "' And '" & dateE1 & "')"
        dt1 = ClsDataTable.GetDataTableBoad5(sqlstr22)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()

        ' کش کردن داده‌ها برای خروجی اکسل
        dtExportCache = dt1.Copy()

        Dim dtgg As New DataTable
        Dim sqlstrgg As String = "select * from dashboardVW WHERE (tarix Between '" & datS1 & "' And '" & dateE1 & "') ORDER BY tarix"
        dtgg = ClsDataTable.GetDataTableBoad5(sqlstrgg)
        If dtgg.Rows.Count > 0 Then
            GridView3.Visible = True
            GridView3.DataSource = dtgg
            GridView3.DataBind()
        End If

        Dim sqlstrCount As String = "SELECT SUM(quantity) AS summ, tarix FROM dashboardVW " & _
            "GROUP BY tarix HAVING (tarix Between N'" & datS1 & "' And N'" & dateE1 & "') "
        Dim dts As New DataTable
        dts = ClsDataTable.GetDataTableBoad5(sqlstrCount)

        If dts.Rows.Count > 0 Then
            Dim cntt As Integer = 0
            For i = 0 To dts.Rows.Count - 1
                cntt += dts.Rows(i).Item("summ").ToString()
            Next i
            lblTotal.Text = "جمع كل = " & cntt
        End If
    End Sub

    ' ============================================================
    ' خروجی اکسل بهینه با سرعت بالا
    ' ============================================================
    ' ============================================================
    ' خروجی اکسل فوق‌سریع با StringBuilder
    ' ============================================================
    Protected Sub btnExportExcel_Click(sender As Object, e As EventArgs)
        Try
            Dim dtExport As DataTable = dtExportCache

            If dtExport Is Nothing OrElse dtExport.Rows.Count = 0 Then
                dtExport = TryCast(ASPxGrvHead.DataSource, DataTable)
            End If

            If dtExport Is Nothing OrElse dtExport.Rows.Count = 0 Then
                msg_box("داده‌ای برای خروجی اکسل وجود ندارد")
                Return
            End If

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=RestaurantReport.xls")
            Response.Charset = "utf-8"
            Response.ContentType = "application/vnd.ms-excel"

            Dim sb As New StringBuilder()

            ' هدر جدول (نام ستون‌های فارسی)
            sb.Append("<table border='1' cellpadding='5' style='font-family:Tahoma;font-size:12px;direction:rtl;'>")
            sb.Append("<tr style='background-color:#2c3e50;color:white;font-weight:bold;'>")
            sb.Append("<th>کد پرسنلی</th>")
            sb.Append("<th>نام</th>")
            sb.Append("<th>نام خانوادگی</th>")
            sb.Append("<th>تاریخ</th>")
            sb.Append("<th>ساعت</th>")
            sb.Append("<th>رستوران</th>")
            sb.Append("<th>نوع غذا</th>")
            sb.Append("</tr>")

            ' داده‌ها
            For Each row As DataRow In dtExport.Rows
                sb.Append("<tr>")
                sb.Append("<td>" & row("CardId").ToString() & "</td>")
                sb.Append("<td>" & row("Name").ToString() & "</td>")
                sb.Append("<td>" & row("Family").ToString() & "</td>")
                sb.Append("<td>" & row("tarix").ToString() & "</td>")
                sb.Append("<td>" & row("Time").ToString() & "</td>")
                sb.Append("<td>" & row("resturantName").ToString() & "</td>")
                sb.Append("<td>" & row("divicename").ToString() & "</td>")
                sb.Append("</tr>")
            Next

            sb.Append("</table>")

            Dim utf8Bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sb.ToString())
            Response.BinaryWrite(utf8Bytes)
            Response.End()

        Catch ex As Exception
            msg_box("خطا در خروجی اکسل: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' تبدیل نام ستون به فارسی
    ' ============================================================
    Private Function GetPersianColumnName(columnName As String) As String
        Select Case columnName.ToLower()
            Case "cardid"
                Return "کد پرسنلی"
            Case "name"
                Return "نام"
            Case "family"
                Return "نام خانوادگی"
            Case "tarix"
                Return "تاریخ"
            Case "time"
                Return "ساعت"
            Case "resturantname"
                Return "رستوران"
            Case "divicename"
                Return "نوع غذا"
            Case "deviceid"
                Return "کد دستگاه"
            Case "quantity"
                Return "تعداد"
            Case "summ"
                Return "مجموع"
            Case Else
                Return columnName
        End Select
    End Function

    ' ============================================================
    ' متد مورد نیاز برای RenderControl
    ' ============================================================
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' این متد برای جلوگیری از خطای RenderControl در خروجی اکسل مورد نیاز است
    End Sub

    ' ============================================================
    ' رویدادهای موجود
    ' ============================================================
    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub

    

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        load_grid()
        ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & datS & "','" & dateE & "')</script>")
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As EventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("index.aspx")
    End Sub

End Class