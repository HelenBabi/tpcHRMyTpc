
Partial Class Default4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ds As New DataSet2
        Dim mad As String = ds.personel.dinColumn.ToString()


        MsgBox(mad)

    End Sub
End Class
