Imports System.Data
Imports System.Data.SqlClient
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim dt3 As New DataTable
    Dim sqlstr As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
        " FROM tblMainPost WHERE        (stat = 1) ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
     " FROM tblMainPost WHERE        (stat = 1) AND (groupKala = 4)  ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
 " FROM tblMainPost WHERE        (stat = 1) AND (groupKala = 2)  ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
      " FROM tblMainPost WHERE        (stat = 1) ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
  " FROM tblMainPost WHERE        (stat = 1) AND (groupKala = 1)  ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
" FROM tblMainPost WHERE        (stat = 1) AND (groupKala = 3)  ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
" FROM tblMainPost WHERE        (stat = 1) AND (groupKala = 5)  ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        sqlstr = "SELECT        idd, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal, codeuser " & _
" FROM tblMainPost WHERE        (stat = 1) AND (groupKala = 6)  ORDER BY idd DESC "
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt3
        DataList1.DataBind()
        Label1.Text = "تعداد:" & dt3.Rows.Count
    End Sub
End Class
