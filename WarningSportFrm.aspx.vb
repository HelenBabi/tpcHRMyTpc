
Partial Class WarningSportFrm
    Inherits System.Web.UI.Page

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click

        Response.Redirect("vaccinationPersonel.aspx")
    End Sub
End Class
