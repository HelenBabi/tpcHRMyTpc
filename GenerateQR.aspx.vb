
Partial Class GenerateQR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnQR_Click()
        Dim token = Guid.NewGuid()
        ' ذخیره در EvaluationSessions با انقضا (مثلاً 30 دقیقه)
        ' ساخت لینک:
        Dim url = "https://site.com/Evaluation/Evaluation.aspx?token=" & token.ToString()
        ' تولید QR (QRCoder)
    End Sub

End Class
