Imports System.Data
Imports System.Data.SqlClient
Partial Class trnDoreDetailFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadDetails()
    End Sub
    Private Sub loadDetails()
        Dim dt As New DataTable
        Dim sqlstr As String = "select * from trnDoreReqisterVW where idd = " & Session("idd") & ""
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            subject.Text = dt.Rows(0).Item("onvan").ToString
            karbar.Text = dt.Rows(0).Item("karbar").ToString
            aztarix.Text = dt.Rows(0).Item("tarixShuru").ToString
            tatarix.Text = dt.Rows(0).Item("tarixEtmam").ToString
            modares.Text = dt.Rows(0).Item("modares").ToString
            saat.Text = dt.Rows(0).Item("saat").ToString
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sqlstr2 As String = ""
        Dim sqlstr3 As String = ""
        sqlstr2 = "  UPDATE trnDoreReqisterTbl  SET   stat = 14  WHERE (idd = " & Session("idd") & ") "
        sqlstr3 = "  UPDATE trnDoreReqisterTbl  SET   adminScore = 1  WHERE (idd = " & Session("idd") & ") "
        upStat(sqlstr2)
        upStat(sqlstr3)
        msg_box("عملیات  با موفقیت انجام شد")
     
    End Sub

    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        msg = Replace(msg, "'", " ")
        msg = Replace(msg, """", " ")
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sqlstr2 As String = ""
        Dim sqlstr3 As String = ""
        sqlstr2 = "  UPDATE trnDoreReqisterTbl  SET   stat = 14  WHERE (idd = " & Session("idd") & ") "
        sqlstr3 = "  UPDATE trnDoreReqisterTbl  SET   adminScore = 2  WHERE (idd = " & Session("idd") & ") "
        upStat(sqlstr2)
        upStat(sqlstr3)
        msg_box("عملیات  با موفقیت انجام شد")
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sqlstr2 As String = ""
        Dim sqlstr3 As String = ""
        sqlstr2 = "  UPDATE trnDoreReqisterTbl  SET   stat = 14  WHERE (idd = " & Session("idd") & ") "
        sqlstr3 = "  UPDATE trnDoreReqisterTbl  SET   adminScore = 3  WHERE (idd = " & Session("idd") & ") "
        upStat(sqlstr2)
        upStat(sqlstr3)
        msg_box("عملیات  با موفقیت انجام شد")
    End Sub
End Class
