Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
imports System.Globalization
Imports System

Partial Class certifiDeskPersonal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        If Session("contratTyp") = "2" Or Session("contratTyp") = "4" Or Session("contratTyp") = "5" Then


        Else
            msg_box("فعلا قرارداد رسمی و مدت معین و موقت مجاز به درخواست گواهی حقوق می باشند")
            lblHoshdar.Text = "فعلا قرارداد رسمی و مدت معین و موقت مجاز به درخواست گواهی حقوق می باشند"
            Exit Sub

        End If
        If Me.IsPostBack = False Then
            gavahiTypeLoad()
            bankLoad()


        End If
        loadGridSabege()

    End Sub

    Protected Sub gridSabege_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles gridSabege.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat"))


        If value = 6 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.Yellow
            'lblHoshdar.Visible = True

        End If


    End Sub

    Private Sub loadGridSabege()



        Dim sqlstr As String = ""

        sqlstr = "SELECT     * from sabegeVW  WHERE person_cod = " & Session("codePersoneli") & " "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        gridSabege.DataSource = dt
        gridSabege.DataBind()
        
        For i As Integer = 0 To Val(dt.Rows.Count) - 1
            If Val(dt.Rows(i).Item("stat").ToString) = 5 Then
                msg_box("شما گواهی برگشت از بانک دارید")
                lblHoshdar.Text = "شما گواهی برگشت از بانک دارید"
                btnSave.Visible = False
            End If
        Next i


        Dim sqlstr2 As String = ""

        sqlstr2 = "SELECT     * from sabegeVW  WHERE person_cod = " & Session("codePersoneli") & " and stat = 1  "

        Dim dt2 As New DataTable
        dt2 = ClsDataTable.GetDataTable(sqlstr2)
        If Session("contratTyp") = 2 And dt2.Rows.Count >= 6 Then
            msg_box("سقف مجاز برای شما پر شده است")
            lblHoshdar.Text = "سقف مجاز برای شما پر شده است"
            btnSave.Visible = False
        ElseIf Session("contratTyp") <> 2 And dt2.Rows.Count >= 3 Then
            msg_box("سقف مجاز برای شما پر شده است")
            lblHoshdar.Text = "سقف مجاز برای شما پر شده است"
            btnSave.Visible = False

        End If
        Dim sqlstr3 As String = ""

        sqlstr3 = "SELECT     * from sabegeVW  WHERE person_cod = " & Session("codePersoneli") & " and stat = 6  "

        Dim dt3 As New DataTable
        dt3 = ClsDataTable.GetDataTable(sqlstr3)
        If dt3.Rows.Count > 0 Then
            msg_box("شما یک درخواست گواهی در کارتابل منابع انسانی دارید")
            lblHoshdar.Text = "شما یک درخواست گواهی در کارتابل منابع انسانی دارید"
            btnSave.Visible = False
            Exit Sub

        End If
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
       
        If Len(codeMeli.Text) <> 10 Then

            msg_box("کد ملی باید 10 رقم باشد")
            Exit Sub

      
        End If

        If Session("contratTyp") = "2" Or Session("contratTyp") = "4" Or Session("contratTyp") = "5" Then
            save_final()
        Else

            msg_box("فعلا قرارداد رسمی و مدت معین و موقت مجاز به درخواست گواهی حقوق می باشند")
            lblHoshdar.Text = "فعلا قرارداد رسمی و مدت معین و موقت مجاز به درخواست گواهی حقوق می باشند"
            Exit Sub

        End If

    End Sub


    Private Sub save_final()
        hdn_Final_key.Value = ClsGetMaxId.GetMaxId("gavahi", "new_later_num")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "newGavahiPersonelPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@new_later_num", SqlDbType.Int, 6)
            cmm.Parameters("@new_later_num").Value = Val(hdn_Final_key.Value)


            cmm.Parameters.Add("@person_cod", SqlDbType.Int, 6)
            cmm.Parameters("@person_cod").Value = Session("codePersoneli")

            cmm.Parameters.Add("@certifi_typ", SqlDbType.Int, 5)
            cmm.Parameters("@certifi_typ").Value = Val(CmbGavahiType.SelectedValue)

            cmm.Parameters.Add("@moassese", SqlDbType.NVarChar, 150)
            cmm.Parameters("@moassese").Value = txtSazman.Text

            cmm.Parameters.Add("@motafarege_nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@motafarege_nam").Value = girande.Text & " " & codeMeli.Text

            cmm.Parameters.Add("@bank_nam", SqlDbType.NVarChar, 50)
            cmm.Parameters.Add("@bank_id", SqlDbType.Int, 5)
            cmm.Parameters.Add("@shobe_id", SqlDbType.Int, 5)
            cmm.Parameters.Add("@shobe_nam", SqlDbType.NVarChar, 50)

            If CmbGavahiType.SelectedValue = 6 Or CmbGavahiType.SelectedValue = 8 Or CmbGavahiType.SelectedValue = 10 Then

                cmm.Parameters("@bank_nam").Value = ""
                cmm.Parameters("@shobe_nam").Value = ""
                cmm.Parameters("@bank_id").Value = 1
                cmm.Parameters("@shobe_id").Value = 1

            Else

                cmm.Parameters("@bank_nam").Value = cmbBank.SelectedItem.ToString
                cmm.Parameters("@shobe_nam").Value = cmbShobe.SelectedItem.ToString
                cmm.Parameters("@bank_id").Value = Val(cmbBank.Value)
                cmm.Parameters("@shobe_id").Value = Val(cmbShobe.Value)
            End If

            cmm.Parameters.Add("@reg_user_id", SqlDbType.Int, 5)
            cmm.Parameters("@reg_user_id").Value = Session("userid")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then

                loadGridSabege()
                msg_box("  ثبت شد ")
                lblHoshdar.Text = "درخواست گواهی حقوق به موفقیت ثبت شد"
                btnSave.Visible = False
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub



    Private Sub gavahiTypeLoad()


        Dim sqlstr As String

        sqlstr = "SELECT        gavahi_typ_id, gavahi_typ, report " & _
       " FROM gavahi_type " & _
            "WHERE        (stat = 1) AND (gavahi_typ_id = 9 OR  gavahi_typ_id = 10)  "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbGavahiType.DataSource = dt
        CmbGavahiType.DataValueField = "gavahi_typ_id"
        CmbGavahiType.DataTextField = "gavahi_typ"
        CmbGavahiType.DataBind()
        bankLoad()

        'ibtnSave.Visible = True
    End Sub



    Private Sub bankLoad()

        CmbBank.Enabled = True


        Dim sqlstr As String

        sqlstr = "SELECT     * from bank_  WHERE   (CodeClosed = 0) ORDER BY status"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbBank.DataSource = dt
        cmbBank.ValueField = "bank_id"
        cmbBank.TextField = "bank_name"
        CmbBank.DataBind()
        'shobeLoad()
    End Sub

    Private Sub shobeLoad2()

        CmbShobe.Enabled = True


        Dim sqlstr As String

        sqlstr = "SELECT         shobe_id, shobe_nam, bank_id " & _
       " FROM shobe "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        CmbShobe.DataSource = dt
        cmbShobe.ValueField = "shobe_id"
        cmbShobe.TextField = "shobe_nam"
        CmbShobe.DataBind()




    End Sub
    Protected Sub CmbBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBank.SelectedIndexChanged



        Dim sqlstr As String

        sqlstr = "SELECT         shobe_id, shobe_nam, bank_id " & _
       " FROM shobe WHERE        (bank_id = " & cmbBank.Value & ") and (CodeClosed = 0)"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbShobe.DataSource = dt
        cmbShobe.ValueField = "shobe_id"
        cmbShobe.TextField = "shobe_nam"
        cmbShobe.DataBind()
    End Sub

    Protected Sub CmbGavahiType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGavahiType.SelectedIndexChanged
        If CmbGavahiType.SelectedValue = 10 Then
            cmbBank.Visible = False
            cmbShobe.Visible = False
            lblbank.Visible = False
            lblShobe.Visible = False

            lblSazman.Visible = True
            txtSazman.Visible = True
        Else
            cmbBank.Visible = True
            cmbShobe.Visible = True
            lblbank.Visible = True
            lblShobe.Visible = True

            lblSazman.Visible = False
            txtSazman.Visible = False

        End If
    End Sub

End Class
