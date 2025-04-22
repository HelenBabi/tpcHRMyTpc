Imports System.Data
Imports System.Data.SqlClient
Partial Class hDataEntryFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            load_cmbMadrak()

        End If
    End Sub

    Private Sub load_cmbMadrak()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        idMadrak, madarkVal, sen         FROM registerMadrakTbl "
        dt = ClsDataTable.GetDataTable(sqlstr)
        madrak.DataSource = dt
        madrak.DataTextField = "madarkVal"
        madrak.DataValueField = "idMadrak"
        madrak.DataBind()

    End Sub
    Private Sub load_cmbReshte()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        idReshte, reshte        FROM registerReshteTbl " & _
               " WHERE        (madrakID = " & madrak.SelectedValue & ") AND (stat = 1)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        reshte.DataSource = dt
        reshte.DataTextField = "reshte"
        reshte.DataValueField = "idReshte"
        reshte.DataBind()

    End Sub
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub



   
    Private Sub saveFinal()
        HiddenField1.Value = 0
        HiddenField1.Value = ClsGetMaxId.GetMaxId("registerTbl", "idReg")

        'If hdn_Final_key.Value = "new" Then txtTicketId.Text = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "hDataEntryPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@idReg", SqlDbType.Int, 10)
            cmm.Parameters("@idReg").Value = HiddenField1.Value

            cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
            cmm.Parameters("@madrak").Value = madrak.SelectedItem.Text

            cmm.Parameters.Add("@reshte", SqlDbType.NVarChar, 50)
            cmm.Parameters("@reshte").Value = reshte.SelectedItem.Text


            cmm.Parameters.Add("@codePosti", SqlDbType.VarChar, 50)
            cmm.Parameters("@codePosti").Value = codePosti.Text

            cmm.Parameters.Add("@moafitType", SqlDbType.NVarChar, 50)
            cmm.Parameters("@moafitType").Value = ""



            cmm.Parameters.Add("@address", SqlDbType.NVarChar, 250)
            cmm.Parameters("@address").Value = address.Text

            cmm.Parameters.Add("@mail", SqlDbType.VarChar, 50)
            cmm.Parameters("@mail").Value = Mail.Text

            cmm.Parameters.Add("@mobil", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mobil").Value = mobil.Text

            cmm.Parameters.Add("@tell", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tell").Value = tell.Text

            'cmm.Parameters.Add("@salMadrak", SqlDbType.NVarChar, 50)
            'cmm.Parameters("@salMadrak").Value = salMadrak.Text

            'cmm.Parameters.Add("@moadel", SqlDbType.VarChar, 50)
            'cmm.Parameters("@moadel").Value = moadel.Text

            'cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
            'cmm.Parameters("@madrak").Value = madrak.SelectedValue
            cmm.Parameters.Add("@xedmat", SqlDbType.NVarChar, 50)
            If jens.SelectedValue = 1 Then
                cmm.Parameters("@xedmat").Value = xedmat.SelectedItem.Text
            Else
                cmm.Parameters("@xedmat").Value = ""
            End If



            cmm.Parameters.Add("@tahol", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tahol").Value = tahol.SelectedValue


            cmm.Parameters.Add("@jens", SqlDbType.NVarChar, 50)
            cmm.Parameters("@jens").Value = jens.SelectedItem.Text

            Dim tavalod As String = sal.SelectedValue + "/" + mah.SelectedValue + "/" + ruz.SelectedValue
            cmm.Parameters.Add("@tarixTavalod", SqlDbType.VarChar, 50)
            cmm.Parameters("@tarixTavalod").Value = tavalod

            cmm.Parameters.Add("@tabeiat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tabeiat").Value = "ایرانی"

            cmm.Parameters.Add("@shsh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shsh").Value = shsh.Text


            cmm.Parameters.Add("@codeMelli", SqlDbType.VarChar, 50)
            cmm.Parameters("@codeMelli").Value = codeMelli.Text

            cmm.Parameters.Add("@fatherName", SqlDbType.NVarChar, 50)
            cmm.Parameters("@fatherName").Value = fatherName.Text

            cmm.Parameters.Add("@lname", SqlDbType.NVarChar, 50)
            cmm.Parameters("@lname").Value = lname.Text

            cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nam").Value = nam.Text


            cmm.Parameters.Add("@gerayeshTahsili", SqlDbType.NVarChar, 50)
            cmm.Parameters("@gerayeshTahsili").Value = gerayeshTahsili.Text

            'cmm.Parameters.Add("@salMadrak", SqlDbType.NVarChar, 50)
            'cmm.Parameters("@salMadrak").Value = salMadrak.Text

            Dim moaadel As String = moadel2.Text + "/" + moadel1.Text
            cmm.Parameters.Add("@moadel", SqlDbType.VarChar, 50)
            cmm.Parameters("@moadel").Value = moaadel

            'cmm.Parameters.Add("@tahsilUni", SqlDbType.NVarChar, 50)
            'cmm.Parameters("@tahsilUni").Value = ""

            cmm.Parameters.Add("@madrakuni", SqlDbType.NVarChar, 50)
            cmm.Parameters("@madrakuni").Value = madrakuni.Text


            cmm.Parameters.Add("@maharatha", SqlDbType.NVarChar, 500)
            cmm.Parameters("@maharatha").Value = maharatha.Text

            cmm.Parameters.Add("@moaref", SqlDbType.NVarChar, 50)
            cmm.Parameters("@moaref").Value = moaref.Text

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar, 4000)
            cmm.Parameters("@notee").Value = notee.Text


            cmm.Parameters.Add("@shohada", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shohada").Value = shohada.SelectedItem.Text



            cmm.Parameters.Add("@sanatNaft", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sanatNaft").Value = nesbatRadio.SelectedItem.Text

            cmm.Parameters.Add("@nesbat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nesbat").Value = nesbatCmb.SelectedValue

            cmm.Parameters.Add("@codePerson", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codePerson").Value = persenliTxt.Text


            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  درخواست شما با شماره پیگیری    " & HiddenField1.Value & "   ثبت شد")
                'Session("message") = "  درخواست با شماره پیگیری    " & tiketid & "   به واحد کامپیوتر ارسال شد"
                'Session("regID") = HiddenField1.Value
                'Response.Redirect("tpcRegisterStep3.aspx")
                'Response.Redirect("messageFrm.aspx")
                msg_box("  درخواست  با شماره پرونده    " & HiddenField1.Value & "   ثبت شد")


            Else
                'msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub madrak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles madrak.SelectedIndexChanged
        load_cmbReshte()

    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If Len(codeMelli.Text) > 10 Or Len(codeMelli.Text) < 10 Then
            msg_box(" کدملی باید 10رقم باشد")
            codeMelli.Focus()
            Exit Sub
        End If
     
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT        codeMelli         FROM registerTbl " & _
                          "WHERE        (codeMelli = '" & codeMelli.Text & "') and (stat = 1)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            msg_box("با این کد ملی قبلا ثبت نام شده است")
            Exit Sub
        Else
            saveFinal()
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub nesbatRadio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles nesbatRadio.SelectedIndexChanged
        If nesbatRadio.SelectedValue = 1 Then
            nesbatLbl.Visible = True
            nesbatCmb.Visible = True
            perseneliLbl.Visible = True
            persenliTxt.Visible = True

        Else
            nesbatLbl.Visible = False
            nesbatCmb.Visible = False
            perseneliLbl.Visible = False
            persenliTxt.Visible = False
            persenliTxt.Text = ""
            nesbatCmb.SelectedValue = 0
        End If
    End Sub
End Class
