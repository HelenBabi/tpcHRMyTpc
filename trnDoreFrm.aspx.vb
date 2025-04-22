Imports System.Data
Imports System.Data.SqlClient
Partial Class trnDoreFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
     gridAmuzesh
        loadperson()
    End Sub

    Private Sub loadperson()
        Dim dsh2 As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT   postname,madrak,reshte from  personel  " & _
                                             " WHERE     codepersoneli = " & Session("codePersoneli") & ""
        dsh2 = ClsDataTable.GetDataTable(sqlstr)
        If dsh2.Rows.Count > 0 Then

            sematTxt.Text = dsh2.Rows(0).Item("postname").ToString

            madrakTxt.Text = dsh2.Rows(0).Item("madrak").ToString
            reshteTxt.Text = dsh2.Rows(0).Item("reshte").ToString
           
        Else
            'MsgBox("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
            msg_box("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
        End If
    End Sub

    Private Sub gridAmuzesh()
        Dim sqlStr As String = "select * from trnPersonelDoreTbl WHERE        (codePersoneli = " & Session("codePersoneli") & ")"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlStr)
        trnGrid.DataSource = dt
        trnGrid.DataBind()
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

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
