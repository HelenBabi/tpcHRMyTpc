Imports System.Data
Imports System.Data.SqlClient
Partial Class tpcRegisterStep1
    Inherits System.Web.UI.Page

    Protected Sub btnPage1_Click(sender As Object, e As EventArgs) Handles btnPage1.Click
        Session("okk") = 1
        Response.Redirect("tpcRegisterStep2.aspx")
    End Sub

    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        Response.Redirect("tpcRegisterEdit.aspx")
    End Sub
End Class
