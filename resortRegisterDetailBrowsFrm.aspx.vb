Imports System.Data
Imports System.Data.SqlClient
Partial Class resortRegisterDetailBrowsFrm
    Inherits System.Web.UI.Page
    Dim a As String = ClsDateTime.GetDateNow
    Dim b As String = ClsDateTime.GetDateNow
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("fromTyp") = "rpt" Then
            btnBrwsSys.Visible = False
        End If
        Image1.ImageUrl = "images/Resort/" & Session("ResortID") & ".jpg"
        If Me.IsPostBack = False Then
            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
            load_ellat()
            load_Seporde()
            loadDetails()
            load_grid_FAKTOR()
        End If

        'load_Mizban()

    End Sub

    Private Sub loadDetails()
        Dim sqlstrr As String = ""
        sqlstrr = "SELECT        ResortID, nam, lnam, genderr, datee + ' ' + timee AS tarixVorud, sepordeTyp, codeCart, ellat, MizbanId, mizban, unitee, otag, mojavezNo, notee, stat, userId, dateEnd + ' ' + timeEnd AS tarixXuruj2, outUserId, sherkat, vahed, " & _
                       "  onvanshoghli, cartType,codeMelli, tarix, TarixXurujUser + ' ' + SaatXurujUser AS tarixXuruj1 , TarixVurudUser + ' ' + SaatVurudUser as vurudUserSaat FROM            tblResort WHERE        (ResortID = " & Session("ResortID") & ") "
        Dim dtt As New DataTable
        dtt = ClsDataTable.GetDataTableHerasat(sqlstrr)
        If dtt.Rows.Count > 0 Then
            txtNotee.Text = dtt.Rows(0).Item("notee").ToString

            txtCodemojavez.Text = dtt.Rows(0).Item("mojavezNo").ToString

            txtotag.Text = dtt.Rows(0).Item("otag").ToString
            nam.Text = dtt.Rows(0).Item("nam").ToString
            lnam.Text = dtt.Rows(0).Item("lnam").ToString
            CmbEllat.SelectedValue = dtt.Rows(0).Item("ellat").ToString

            txtShomarecart.Text = dtt.Rows(0).Item("codeCart").ToString
            mizabnTxtt.Text = dtt.Rows(0).Item("mizban").ToString
            CmbSeporedeTyp.SelectedValue = dtt.Rows(0).Item("sepordeTyp").ToString
            jens.SelectedValue = dtt.Rows(0).Item("genderr").ToString
            tarix.Text = dtt.Rows(0).Item("tarix").ToString
            tarixVorud.Text = dtt.Rows(0).Item("tarixVorud").ToString
            tarixXuruj1.Text = dtt.Rows(0).Item("tarixXuruj1").ToString
            tarixXuruj2.Text = dtt.Rows(0).Item("tarixXuruj2").ToString
            vurudUser.Text = dtt.Rows(0).Item("vurudUserSaat").ToString
            codeMelli.Text = dtt.Rows(0).Item("codeMelli").ToString
        End If

    End Sub
    Private Sub load_Seporde()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     SepordeCode, Sepordevalue, CodeClosed  FROM tblSepordeType  WHERE  (CodeClosed = 0) order by Sepordevalue "
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        CmbSeporedeTyp.DataSource = dt
        CmbSeporedeTyp.DataValueField = "SepordeCode"
        CmbSeporedeTyp.DataTextField = "Sepordevalue"
        CmbSeporedeTyp.DataBind()

    End Sub

    Private Sub load_ellat()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      ellatCode, ellatName, CodeClosed  FROM tblEllatType  WHERE  (CodeClosed = 0) order by ellatCode "
        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
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


        '   Dim Resortid As Integer = ClsGetMaxId.GetMaxId("tblResort", "ResortID")

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketEtmam"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@ResortID", SqlDbType.Int, 10)
            cmm.Parameters("@ResortID").Value = Session("ResortID")

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("ثبت خروج با موفقیت انجام شد")
                ' Response.Redirect("frmWorkTableUsers.aspx")
                btnBrwsSys.Enabled = False
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
   
        Response.Redirect("frmBrowsResort.aspx")

    End Sub


    Private Sub load_grid_FAKTOR()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT     documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name,  documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid,  " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib, " & _
                         "tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar " & _
"FROM            documentsTbl INNER JOIN " & _
                        " tblUsers ON documentsTbl.userid = tblUsers.UserCode " & _
       " WHERE(documentsTbl.fileID = " & Session("ResortID") & ") " & _
"ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTableHerasat(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

    End Sub

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())

        Dim sqlstrd As String
        Dim dtd As New DataTable
        sqlstrd = "select  id, content_name, content_file  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTableHerasat(sqlstrd)
        If dtd.Rows.Count > 0 Then
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("content_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("content_file"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این پرونده فایل ضمیمه نشده است")
        End If
        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()



    End Sub


    Protected Sub lnkDel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())
        Dim cnn As New SqlConnection

        '  ins_temp(3, radif)
        Dim cmm As New SqlCommand
        cmm.CommandText = "delete        documentsTbl       WHERE id =" & fileid & ""
        Dim constr As String = ClsConnection.GetConnHerasat
        cnn.ConnectionString = constr
        cmm.Connection = cnn

        cnn.Open()
        cmm.ExecuteNonQuery()

        cnn.Close()
        load_grid_FAKTOR()
    End Sub


End Class
