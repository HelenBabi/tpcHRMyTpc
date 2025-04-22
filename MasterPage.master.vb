
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Response.Redirect("Default.aspx")
    End Sub

   

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("http://192.168.9.18:81")
    End Sub
End Class

