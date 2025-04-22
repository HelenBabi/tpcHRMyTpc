Imports System.Data
Imports System.Data.SqlClient
Partial Class resortRegisterFrm
    Inherits System.Web.UI.Page
    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            load_ellat()
            'load_Seporde()
        End If

        'load_Mizban()
        load_Mojavez()
    End Sub
    Private Sub load_Mojavez()
        Dim dtt As New DataTable
        Dim sqlstrr As String = ""
        sqlstrr = "SELECT        isResort     FROM personel WHERE        (ID = " & Session("userid") & ") AND (isResort = 1) "
        dtt = ClsDataTable.GetDataTable(sqlstrr)
        If dtt.Rows.Count > 0 Then
            Panel1.Enabled = True
        Else
            msg_box("شما مجوز درخواست مهمان ندارید در صورت نیاز با واحد حراست تماس بگبرید")
            btnBrwsSys.Visible = False
        End If

    End Sub

    Private Sub load_ellat()
        Dim dt As New DataTable
        Dim cls_e As New ClsEllat
        dt = cls_e.GetAllEllatDDL
        CmbEllat.DataSource = dt
        CmbEllat.DataValueField = "EllatCode"
        CmbEllat.DataTextField = "ellatName"
        CmbEllat.DataBind()

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
    Private Sub save_final()


        Dim Resortid As Integer = ClsGetMaxId.GetMaxIdHerast("tblResort", "ResortID")

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHerasat
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "ResortRegisterPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@notee", SqlDbType.NVarChar)
            cmm.Parameters("@notee").Value = txtNotee.Text

            cmm.Parameters.Add("@mojavezNo", SqlDbType.VarChar, 50)
            cmm.Parameters("@mojavezNo").Value = ""


            cmm.Parameters.Add("@otag", SqlDbType.NVarChar, 50)
            cmm.Parameters("@otag").Value = txtotag.Text


            cmm.Parameters.Add("@unitee", SqlDbType.SmallInt)
            cmm.Parameters("@unitee").Value = Session("unitID")

            cmm.Parameters.Add("@mizban", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mizban").Value = Session("lname") & " " & Session("fname")

            cmm.Parameters.Add("@ellat", SqlDbType.SmallInt)
            cmm.Parameters("@ellat").Value = CmbEllat.SelectedValue

            cmm.Parameters.Add("@codeCart", SqlDbType.VarChar, 50)
            cmm.Parameters("@codeCart").Value = ""

            cmm.Parameters.Add("@sepordeTyp", SqlDbType.SmallInt)
            cmm.Parameters("@sepordeTyp").Value = 0

            cmm.Parameters.Add("@ResortID", SqlDbType.Int, 10)
            cmm.Parameters("@ResortID").Value = Resortid

            cmm.Parameters.Add("@MizbanId", SqlDbType.Int, 10)
            cmm.Parameters("@MizbanId").Value = Session("userid")

            cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nam").Value = nam.Text

            cmm.Parameters.Add("@lnam", SqlDbType.NVarChar, 150)
            cmm.Parameters("@lnam").Value = lnam.Text

            cmm.Parameters.Add("@genderr", SqlDbType.SmallInt)
            cmm.Parameters("@genderr").Value = jens.SelectedValue

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = 0

            Dim dats As String = ""
            datS = Request.Form("datepicker")

            If Len(dats) < 10 Then
                dats = ClsDateTime.setDate(dats)
            End If
            cmm.Parameters.Add("@tarix", SqlDbType.VarChar, 10)
            cmm.Parameters("@tarix").Value = dats
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" ثبت شد ")

                'Dim sourceDir As String = HttpContext.Current.Server.MapPath("~/images")
                'Dim backupDir As String = HttpContext.Current.Server.MapPath("~/images/Resort/")
                'File.Copy(Path.Combine(sourceDir, "11.jpg"), Path.Combine(backupDir, Resortid & ".jpg"))
                ' btnsave.Enabled = False
                'Dim furl As String = "frmPrint3.aspx?PwhereSTR=" & Resortid & ""
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=900,width=450,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub

    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        save_final()

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
End Class
