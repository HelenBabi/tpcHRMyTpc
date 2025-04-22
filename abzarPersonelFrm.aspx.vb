Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO

Partial Class abzarPersonelFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
  
        If Me.IsPostBack = False Then
            find_pc()

        End If


    End Sub
    Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grdFarei.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("inout"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PaleGreen

        ElseIf value = 2 Then

            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PeachPuff
        ElseIf value = 3 Then

            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Plum
        End If


    End Sub





    Private Sub find_pc()




            Dim sqlstr As String = "SELECT        * " & _
       " FROM vwAnbarDtl WHERE        (codePersoneli = " & Session("codePersoneli") & ")"
            Dim dt As New DataTable
        dt = ClsDataTable.GetDataTableAbzar(sqlstr)
        If dt.Rows.Count > 0 Then
            'namTxt.Text = dt.Rows(0).Item("holder").ToString
            'unitTxt.Text = dt.Rows(0).Item("UnitsName").ToString
            'omurTxt.Text = dt.Rows(0).Item("omur").ToString
            load_gridAll()

        Else

            'namTxt.Text = ""
            'unitTxt.Text = ""
            'omurTxt.Text = ""
            grdFarei.DataBind()
            'sadereTxt.Text = ""
            'varedeTxt.Text = ""
            'bagitxt.Text = ""
            'Exit Sub
        End If








    End Sub


    Private Sub gridSadereVarede()
        Dim sqlstr As String = "SELECT        TOP (200)  kartex " & _
        " FROM kartexPersonelVW GROUP BY codePersoneli, kartex         HAVING(codePersoneli = " & Session("codePersoneli") & ")"

        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTableAbzar(sqlstr)

        If dt.Rows.Count > 0 Then
            Dim s As Integer = 0
            'Dim sad As Integer = 0
            'Dim varr As Integer = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                s = s + Val(dt.Rows(i).Item("kartex").ToString())
                'sad = sad + Val(dt.Rows(i).Item("sadere").ToString())
                'varr = varr + Val(dt.Rows(i).Item("varede").ToString())
            Next i



            'varedeTxt.Text = varr
            'sadereTxt.Text = sad
            'bagitxt.Text = s
        End If



    End Sub

    Private Sub load_gridAll()



        Dim sqlstra As String = ""

        Dim dta As New DataTable

        sqlstra = " SELECT       * from kartexPersonelVW where codePersoneli = " & Val(Session("codePersoneli")) & " and typ <> 10 and  (kartex > 0)"

        dta = ClsDataTable.GetDataTableAbzar(sqlstra)

        grdFarei.DataSource = dta

        grdFarei.DataBind()
        'gridSadereVarede()
        'Dim cntt As Integer = dta.Rows.Count

    End Sub

    Protected Sub btnWork_Click(sender As Object, e As EventArgs) Handles btnWork.Click
   
            Response.Redirect("index.aspx")
    
    End Sub


 


    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            TryCast(e.Row.FindControl("lblRowNumber"), Label).Text = (e.Row.RowIndex + 1).ToString()
        End If
    End Sub

   

    Protected Sub grdFarei_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles grdFarei.AfterPerformCallback
        load_gridAll()

    End Sub
End Class
