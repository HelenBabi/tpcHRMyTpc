Imports System.Data
Imports System.Data.SqlClient
Partial Class transportationPersonelMyCar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            loadDetals()
        End If

    End Sub
    Private Sub loadDetals()
        Dim dt3 As New DataTable
        Dim sqlstr3 As String = ""
        sqlstr3 = "SELECT        *         FROM shaxsicarTbl WHERE        (perID = " & Session("userid") & ")"
        dt3 = ClsDataTable.GetDataTable(sqlstr3)
        If dt3.Rows.Count > 0 Then
        

            carTypee.Text = dt3.Rows(0).Item("carType").ToString
            carPelak2.Text = Right(dt3.Rows(0).Item("carPelak").ToString, 2)
            carPelak3.Text = Mid(dt3.Rows(0).Item("carPelak").ToString, 9, 3)
            carPelakHarf.Text = Mid(dt3.Rows(0).Item("carPelak").ToString, 13, 1)
            carPelakiran.Text = Mid(dt3.Rows(0).Item("carPelak").ToString, 6, 2)


        End If
    End Sub
    Private Sub save_final()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            'cmm.CommandText = "serviceInsPrc"
            cmm.CommandText = "shaxsicarInsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@personelID", SqlDbType.Int, 5)
            cmm.Parameters("@personelID").Value = Session("UserId")

            'cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            'cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)


            cmm.Parameters.Add("@carType", SqlDbType.NVarChar, 50)
            cmm.Parameters("@carType").Value = carTypee.Text

            cmm.Parameters.Add("@carPelak", SqlDbType.NVarChar, 50)
            cmm.Parameters("@carPelak").Value = "ایران" & carPelakiran.Text & "-" & carPelak3.Text & "-" & carPelakHarf.Text & "-" & carPelak2.Text

            ' cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
            'If hdn_Final_key.Value = "edit" Then
            '     cmm.Parameters("@type_").Value = 2
            ' Else
            '     cmm.Parameters("@type_").Value = 1
            ' End If


            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" اطلاعات ذخیره شد ")
                'btnBrwsSys.Visible = False
                'If hdn_Final_key.Value = "edit" Then
                '    btnBrwsSys.Visible = True
                'End If
                'btnBack.Visible = True



            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
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
    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        save_final()
    End Sub


    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
