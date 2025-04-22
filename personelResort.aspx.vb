Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Class personelResort
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_grid()

    End Sub

    Private Sub load_grid()
        Dim sqlwhere As Integer = Request.QueryString("PwhereSTR")
        Dim sqlstr As String
        Dim dt1 As New DataTable
        sqlstr = "SELECT     tblResort.ResortID, tblResort.nam + ' ' + tblResort.lnam AS moraje, tblResort.datee + ' ' + tblResort.timee as zamansbat, tblResort.codeCart, tblResort.mizban, " & _
                    "   tblResort.otag, tblResort.mojavezNo, tblResort.notee, tblResort.dateEnd + ' ' + tblResort.timeEnd as zamanEnd,  " & _
                     " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblSepordeType.Sepordevalue, tblResort.stat,  " & _
                      " tblUnits.UnitsName, tblGenderTyp.genValue, tblEllatType.ellatName, " & _
                      " tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS outKarbar " & _
                      " FROM         tblResort INNER JOIN " & _
                      " tblUsers ON tblResort.userId = tblUsers.UserCode INNER JOIN " & _
                       " tblSepordeType ON tblResort.sepordeTyp = tblSepordeType.SepordeCode INNER JOIN " & _
                     " tblUnits ON tblResort.unitee = tblUnits.UnitsCode INNER JOIN " & _
                      " tblGenderTyp ON tblResort.genderr = tblGenderTyp.genId INNER JOIN " & _
                      " tblEllatType ON tblResort.ellat = tblEllatType.ellatCode INNER JOIN " & _
                      " tblUsers AS tblUsers_1 ON tblResort.outUserId = tblUsers_1.UserCode " & _
                     " WHERE     (tblResort.mizbanId = " & sqlwhere & " ) order by tblResort.ResortID Desc "
        dt1 = ClsDataTable.GetDataTableHerasat(sqlstr)
        ASPxGrvHead.DataSource = dt1
        ASPxGrvHead.DataBind()

        'Dim cntt As Integer = dt1.Rows.Count
        'lblTotal.Text = "تعداد  = " & cntt
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsToResponse()
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand
        Session("ResortID") = e.CommandArgs.CommandArgument
        'Session("fromTyp") = "rpt"
        'Response.Redirect("frmResortOut.aspx")
        Dim furl As String = "frmPrint3.aspx?PwhereSTR=" & Session("ResortID") & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=700,width=450,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)


    End Sub
    Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LimeGreen

        ElseIf value = 0 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.PaleVioletRed


        End If


    End Sub

End Class
