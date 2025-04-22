Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.Drawing


Partial Class trnSessionScoreFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadDetail()

    End Sub
    Sub loadDetail()
        Dim dt2 As New DataTable
        Dim sqlstr As String = "select * from trnDoreReqisterVW where idd = " & Session("Classidd") & ""
        dt2 = ClsDataTable.GetDataTable(sqlstr)
        codePersonel.Text = dt2.Rows(0).Item("codePersonel").ToString
        nam.Text = dt2.Rows(0).Item("nam").ToString
        family.Text = dt2.Rows(0).Item("family").ToString
        typevalue.Text = dt2.Rows(0).Item("typevalue").ToString
        onvan.Text = dt2.Rows(0).Item("onvan").ToString
        makan.Text = dt2.Rows(0).Item("makan").ToString
        tarixShuru.Text = dt2.Rows(0).Item("tarixShuru").ToString
        tarixEtmam.Text = dt2.Rows(0).Item("tarixEtmam").ToString
        modares.Text = dt2.Rows(0).Item("modares").ToString


    End Sub

    Private Sub save_final()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketComment"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = (Session("tiketId"))

        
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


            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
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




    'Protected Sub btnParaf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnParaf.Click
    '    If cmbParaf.SelectedIndex < 0 Then
    '        msg_box("از منوی تعاریف پایه، پاراف خود را تنظیم نمایید")
    '        Exit Sub
    '    End If
    '    txtNazar.Text = txtNazar.Text & " " & cmbParaf.SelectedItem.ToString
    'End Sub







    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("trnRegisterReportTotalFrm.aspx")
    End Sub
End Class
