Imports System.Data
Imports System.Data.SqlClient
Partial Class vaccinationPersonel2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then


            load_grid_Service()

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

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        'save_tmp(citytToSiteNoRuzkar.SelectedValue)
        'GridView2.Visible = True
        'load_grid_tmp()
        'Dim dt2, dt5 As New DataTable
        'Dim sqlstr As String = ""
        'sqlstr = "SELECT     sum(tedad) as tedadKol   FROM tblTourRegist WHERE (tblTourRegist.id_Tour = " & citytToSiteNoRuzkar.SelectedValue & ")  "
        'dt2 = ClsDataTable.GetDataTable(sqlstr)
        'Dim tedad As Integer = 0
        'If dt2.Rows.Count > 0 Then
        '    tedad = Val(dt2.Rows(0).Item("tedadKol").ToString)
        'End If


        'Dim sqlstrZarf As String = ""
        'sqlstrZarf = "SELECT    zarfiat   FROM tblTour WHERE (idTour = " & citytToSiteNoRuzkar.SelectedValue & ")  "
        'dt5 = ClsDataTable.GetDataTable(sqlstrZarf)

        'Dim zarfTur As Integer = Val(dt5.Rows(0).Item("zarfiat").ToString)



        'If tedad + Val(txtTedad.Text) > zarfTur Then
        '    msg_box("  متاسفنه ظرفیت تکمیل شد")
        '    Exit Sub
        'End If
        'If zarf + txtTedad.Text > Session("zarfiat") Then
        '    msg_box(" تعداد شما بیشتر از ظرفیت زمان است")
        '    Exit Sub
        'End If

        'If dt2.Rows(0).Item("codeKuhnavardi") = txtcodekuh.Text Then
        '    msg_box("شما یکبار ثبت نام کرده اید")
        '    Exit Sub
        'End If
        'sqlstr = "SELECT     *   FROM tblTourRegist WHERE id_Tour = " & citytToSiteNoRuzkar.SelectedValue & " "
        'dt5 = ClsDataTable.GetDataTable(sqlstr)
        'If dt5.Rows.Count > 0 Then
        '    msg_box("شما یکبار ثبت نام کرده اید")
        '    Exit Sub
        'End If

        save_final()


    End Sub
    Private Sub save_final()

        Dim tedad As Integer = 0
        tedad = Val(txtTedad.Text)
        If tedad < 1 Then
            msg_box("  تعداد نمی تواند کوچکتر از یک باشد ")
            txtTedad.Focus()
            Exit Sub

        End If
        hdn_Final_key.Value = ClsGetMaxId.GetMaxId("vaccinationReservTbl", "reservID")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "vaccinationReservPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@reservID", SqlDbType.Int, 5)
            cmm.Parameters("@reservID").Value = hdn_Final_key.Value

            cmm.Parameters.Add("@tedad", SqlDbType.Int, 5)
            cmm.Parameters("@tedad").Value = Val(txtTedad.Text)

            cmm.Parameters.Add("@perID", SqlDbType.Int, 5)
            cmm.Parameters("@perID").Value = Session("userid")

            cmm.Parameters.Add("@mobile", SqlDbType.VarChar, 15)
            cmm.Parameters("@mobile").Value = mobileTxt.Text

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output


            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" ردیف  " & hdn_Final_key.Value & " برای شما رزرو شد ")
                load_grid_Service()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub


    Private Sub save_tmp(ByVal masirID As Integer)

        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("servicePersonalTemp", "tempID")
        End If

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "serviceInsTempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@serviceID", SqlDbType.Int, 5)
            cmm.Parameters("@serviceID").Value = masirID

      

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@perID", SqlDbType.Int, 5)
            cmm.Parameters("@perID").Value = Session("UserId")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then

                msg_box("  ثبت شد ")

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub


    'Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
    '    If e.CommandName = "del" Then
    '        Dim cnn As New SqlConnection
    '        Dim radif As Int64 = e.CommandArgument
    '        radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
    '        '  ins_temp(3, radif)
    '        Dim cmm As New SqlCommand
    '        cmm.CommandText = "delete from tblTourRegist where idd=" & radif
    '        Dim constr As String = ClsConnection.GetConn
    '        cnn.ConnectionString = constr
    '        cmm.Connection = cnn
    '        cnn.Open()
    '        cmm.ExecuteNonQuery()
    '        cnn.Close()

    '        load_grid_Service()
    '    End If
    'End Sub

    Private Sub load_grid_Service()
        GridView2.Visible = True
        Dim sqlstr As String = "SELECT        TOP (200) vaccinationReservTbl.tedad, vaccinationReservTbl.reservID, vaccinationReservTbl.perID, personel.nam + ' ' +  personel.family as nam, personel.codePersoneli, personel.mobile  " & _
                         " FROM            vaccinationReservTbl INNER JOIN " & _
                         " personel ON vaccinationReservTbl.perID = personel.ID " & _
                          " WHERE        (personel.codePersoneli = " & Session("codePersoneli") & ")"
   
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub




    Private Sub save_NoService()


        hdn_tmp.Value = 0

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "serviceInsNoPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@perID", SqlDbType.Int, 5)
            cmm.Parameters("@perID").Value = Session("UserId")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then

                msg_box("  ثبت شد ")
                '  Call save_()
                ' load_grid()
                load_grid_Service()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub


End Class
