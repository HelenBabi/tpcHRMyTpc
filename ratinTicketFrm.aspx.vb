Imports System.Data
Imports System.Data.SqlClient
Partial Class ratinTicketFrm
    Inherits System.Web.UI.Page

    'Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged
    '    btnBrwsSys.Visible = True

    '    If RadioButtonList1.SelectedValue = 1 Or RadioButtonList1.SelectedValue = 2 Or RadioButtonList1.SelectedValue = 3 Then
    '        txtNote.Visible = True
    '    Else
    '        txtNote.Visible = False

    '    End If
    'End Sub
    
    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        If RadioButtonList1.SelectedValue.ToString = "" Then

            msg_box("لطفا یکی از گزینه های ارزیابی را انتخاب فرمایید")
            RadioButtonList1.Focus()
            Exit Sub

        ElseIf RadioButtonList1.SelectedValue = 0 Or RadioButtonList1.SelectedValue = 1 And txtNote.Text = "" Then

            msg_box("لطفا توضیحات لازم برای گزینه های ارزیابی انتخاب شده را بنویسید")
            txtNote.Focus()

            Exit Sub
        Else

            nazar()
        End If

    End Sub
    Private Sub nazar()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()
        'Dim typcod As Integer = cmbWorkTyp.SelectedValue
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "ratinPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@ratin", SqlDbType.NVarChar, 5)
            cmm.Parameters("@ratin").Value = RadioButtonList1.SelectedItem.ToString

            cmm.Parameters.Add("@ratinValue", SqlDbType.Int, 5)
            cmm.Parameters("@ratinValue").Value = Val(RadioButtonList1.SelectedValue)


            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar)
            cmm.Parameters("@notee").Value = txtNote.Text.Trim

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("useridd")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                'msg_box("  ثبت  شد ")
                Response.Redirect("warningFrm.aspx")
              
            Else
                msg_box(cmm.Parameters("@pm").Value)
                'msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
            ' MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub
    Protected Sub btnWork_Click(sender As Object, e As EventArgs) Handles btnWork.Click
        'If Session("ratin") = "no" Then
        '    Response.Redirect("login_2.aspx")
        'End If


        'If Session("UserType") = "user" Then
        Response.Redirect("frmWorkTableUsers.aspx")

        'ElseIf Session("UserType") = "admn" Then
        '    Response.Redirect("frmWorkTableAdmin.aspx")

        'ElseIf Session("UserType") = "worker" Then
        '    Response.Redirect("index.aspx")
        'End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblErjaID.Text = Session("tiketId")
        Dim sqlstrN As String = "SELECT        TOP (1) erja_id, notee, user_id, date_time" & _
        " FROM tblHerjaDtl WHERE        (erja_id = " & Session("tiketId") & ")"
        Dim dtN As New DataTable
        dtN = ClsDataTable.GetDataTableHD(sqlstrN)
        If dtN.Rows.Count > 0 Then
            lblTozih.Text = dtN.Rows(0).Item("notee").ToString
            lblZaman.Text = dtN.Rows(0).Item("date_time").ToString
        End If
    End Sub
End Class
