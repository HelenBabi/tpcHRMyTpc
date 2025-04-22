Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration


Partial Class messageFrm
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load



        notes()

      



    End Sub
    
    Sub notes()
        Dim dt2 As New DataTable
        Dim sqlstr As String = " SELECT       ID, codePersoneli, headd, sugID, notee, note_id, REPLACE([pic ], ' ', '') AS picc from messagedtlVW   ORDER BY note_id DESC"



        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            DataList1.DataSource = dt2
            DataList1.DataBind()
        End If


    End Sub
    Protected Sub DataList1_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemCreated
        For i As Integer = 0 To DataList1.Items.Count - 1
            '  Dim labelcode As new Label = DataList5 .Items(i).FindControl("Label8")
            Dim labelcode As Label = DirectCast(DataList1.Items(i).FindControl("lblUsrTyp"), Label)
            If Val(labelcode.Text) = 100 Then
                DataList1.Items(i).BackColor = Drawing.Color.LightYellow

            ElseIf Val(labelcode.Text) = 0 Then
                DataList1.Items(i).BackColor = Drawing.Color.Khaki
                'DataList1.Items(i).Font.Bold = True
            ElseIf Val(labelcode.Text) = 2 Then
                DataList1.Items(i).BackColor = Drawing.Color.MediumAquamarine
            ElseIf Val(labelcode.Text) = 1 Then
                DataList1.Items(i).BackColor = Drawing.Color.AntiqueWhite
            ElseIf Val(labelcode.Text) = 5 Then
                DataList1.Items(i).BackColor = Drawing.Color.MistyRose
            ElseIf Val(labelcode.Text) = 6 Then
                DataList1.Items(i).BackColor = Drawing.Color.Aquamarine
            End If
        Next
    End Sub
    Private Sub save_final()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "messageCommentPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@sugID", SqlDbType.Int)
            cmm.Parameters("@sugID").Value = 0

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@notee").Value = txtNazar.Text

            cmm.Parameters.Add("@PerID", SqlDbType.Int, 10)
            cmm.Parameters("@PerID").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


      


            cmm.Parameters.Add("@userType", SqlDbType.VarChar, 7)
            cmm.Parameters("@userType").Value = 1




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



 

    Private Sub loadcmbParaf()
        Dim dtg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT idd,valuee FROM tblTicketTemplate WHERE     (CodeClosed = 0) and userId= " & Session("userid") & ""
        dtg = ClsDataTable.GetDataTable(sqlstr)

        cmbParaf.DataSource = dtg
        cmbParaf.DataTextField = "valuee"
        cmbParaf.DataValueField = "idd"
        cmbParaf.DataBind()

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





    'Protected Sub btnmojavez_Click(sender As Object, e As EventArgs) Handles btnEntegal.Click
    '    Dim sqlstr, sqlstr2 As String

    '    sqlstr = "  UPDATE tblHerja  SET   stat_ = 6 , mojavez = 1  " & _
    '                          " WHERE (erja_id = " & Session("tiketId") & ") "
    '    Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

    '    Dim payam As String = ""

    '    If hdnType.Value = 1 Then
    '        payam = txtNazar.Text & "  به مجوز نیاز دارد و بعد از تایید مسئول/سرپرست سخت افزار قابل انجام می باشد "
    '    Else
    '        payam = txtNazar.Text & "  به مجوز نیاز دارد و بعد از تایید رئیس واحد کامپیوتر قابل انجام می باشد "
    '    End If


    '    sqlstr2 = "INSERT INTO    tblHerjaDtl ( erja_id, notee, user_id, date_time) " & _
    '                " VALUES     (" & Session("tiketId") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
    '    upStat(sqlstr)
    '    upStat(sqlstr2)
    '    notes()
    '    txtNazar.Text = ""
    'End Sub




    




    



    

    Protected Sub btnParaf_Click(sender As Object, e As EventArgs) Handles btnParaf.Click
        If cmbParaf.SelectedIndex < 0 Then
            msg_box("لطفا پاراف خود را تنظیم نمایید")
            Exit Sub
        End If
        txtNazar.Text = txtNazar.Text & " " & cmbParaf.SelectedItem.ToString
    End Sub



    Protected Sub btnNazar_Click(sender As Object, e As EventArgs) Handles btnNazar.Click
        save_final()
        notes()
        txtNazar.Text = ""
    End Sub



    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
      
        Response.Redirect("index.aspx")
    


    End Sub


    


    
    

    
    
    

    

    
    
    
    

    
    
    

    



End Class
