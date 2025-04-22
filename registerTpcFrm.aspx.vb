Imports System.Data
Imports System.Data.SqlClient
Partial Class registerTpcFrm
    Inherits System.Web.UI.Page
    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("registerTpcFrm.aspx", Val(Session("UserId")))
        If meno_arr(0) = False Then
            Response.Redirect("login_2.aspx")
        End If
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT        TOP (200) idReg, nam, lname, fatherName, codeMelli, shsh, mosahebe, natijeMosahebe, tarixMosahebe, tozihat " & _
       " FROM registerTbl WHERE        (idReg = " & Val(idReg.Text) & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            nam.Text = dt.Rows(0).Item("nam").ToString
            lname.Text = dt.Rows(0).Item("lname").ToString
            fatherName.Text = dt.Rows(0).Item("fatherName").ToString
            codeMelli.Text = dt.Rows(0).Item("codeMelli").ToString
            shsh.Text = dt.Rows(0).Item("shsh").ToString
            tozihat.Text = dt.Rows(0).Item("tozihat").ToString
            chkMosahebe.Checked = dt.Rows(0).Item("mosahebe").ToString
            cmbNatije.SelectedValue = dt.Rows(0).Item("natijeMosahebe").ToString
            tarixMosahebe.Text = dt.Rows(0).Item("tarixMosahebe").ToString
        Else
            msg_box("یافت نشد")
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

    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        save_final()
    End Sub
    Private Sub save_final()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            'cmm.CommandText = "serviceInsPrc"
            cmm.CommandText = "registerMosahebePrc"
            cmm.CommandType = CommandType.StoredProcedure



            cmm.Parameters.Add("@idReg", SqlDbType.Int, 5)
            cmm.Parameters("@idReg").Value = Val(idReg.Text)

            Dim valll As Integer = 0
            If chkMosahebe.Checked = True Then
                valll = 1
            End If

            cmm.Parameters.Add("@mosahebe", SqlDbType.Int, 5)
            cmm.Parameters("@mosahebe").Value = valll


            cmm.Parameters.Add("@natijeMosahebe", SqlDbType.Int, 5)
            cmm.Parameters("@natijeMosahebe").Value = cmbNatije.SelectedValue

            cmm.Parameters.Add("@tarixMosahebe", SqlDbType.VarChar, 50)
            cmm.Parameters("@tarixMosahebe").Value = tarixMosahebe.Text

            cmm.Parameters.Add("@tozihat", SqlDbType.NVarChar, 10000)
            cmm.Parameters("@tozihat").Value = tozihat.Text

           

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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            meno_acc()
            loadCmbNatije()
        End If
    End Sub
    Private Sub loadCmbNatije()



        Dim sqlstr1 As String = ""

        sqlstr1 = "SELECT        TOP (200) natijeID, natije, stat " & _
        "FROM registerNatijeTbl         WHERE(stat = 1)"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr1)
        cmbNatije.DataSource = dt
        cmbNatije.DataValueField = "natijeID"
        cmbNatije.DataTextField = "natije"
        cmbNatije.DataBind()


    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
