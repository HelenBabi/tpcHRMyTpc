Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class frmAnbarSabege
    Inherits System.Web.UI.Page
    Dim dt2, dt1, dt3 As New DataTable
    Dim datS, dateE, datS1, dateE1 As String

    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        load_grid()



    End Sub

 

   
    Private Sub load_grid()
        Dim dt2 As DataTable
        Dim sqlwhere As Integer = Request.QueryString("PwhereSTR")
        Dim sqlstr As String


        sqlstr = " SELECT       * from kalaPersonelVW WHERE        (perID = " & sqlwhere & ")  order by tarix desc "
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        grdFarei.DataSource = dt2
        grdFarei.DataBind()
    End Sub
   


    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub






End Class
