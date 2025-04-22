Imports System.Data
Imports System.Data.SqlClient
Partial Class transportationPersonel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then


            load_PerInfo()
            loadCmbCityToSiteRuzkar8()
            loadCmbSiteToCityRuzkar16()
            'loadCmbMotfarege()
            loadCmbCityToSite19Nobatkar()
            loadCmbsiteToCity8Nobatkar()

            load_grid_Service()



        End If

    End Sub

    Private Sub load_PerInfo()

        Dim sqlstrrr As String = " SELECT        ruzkarShabkar " & _
       " FROM personel    where    (ID = " & Session("UserId") & ") "

        Dim dttt As New DataTable
        dttt = ClsDataTable.GetDataTable(sqlstrrr)
        If dttt.Rows.Count > 0 Then
            cmbRuzShabKar.SelectedValue = dttt.Rows(0).Item("ruzkarShabkar").ToString
        End If

    End Sub

    Private Sub loadCmbCityToSiteRuzkar8()



        Dim sqlstr1 As String = ""

        sqlstr1 = "SELECT        masirID, cod, masir, shiftType, citySiteType " & _
        " FROM serviceListTbl WHERE        (shiftType = 1) AND (citySiteType = 1)" & _
          " ORDER BY cod"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr1)
        citytToSiteNoRuzkar.DataSource = dt
        citytToSiteNoRuzkar.DataValueField = "masirID"
        citytToSiteNoRuzkar.DataTextField = "masir"
        citytToSiteNoRuzkar.DataBind()


    End Sub
    Private Sub loadCmbSiteToCityRuzkar16()



        Dim sqlstr2 As String = ""

        sqlstr2 = "SELECT        masirID, cod, masir, shiftType, citySiteType " & _
        " FROM serviceListTbl WHERE        (shiftType = 1) AND (citySiteType = 2)" & _
          " ORDER BY cod"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr2)
        siteToCityNoRuzkar16.DataSource = dt
        If dt.Rows.Count > 0 Then
            siteToCityNoRuzkar16.DataValueField = "masirID"
            siteToCityNoRuzkar16.DataTextField = "masir"
            siteToCityNoRuzkar16.DataBind()

        End If


    End Sub
    Private Sub loadCmbCityToSite19Nobatkar()



        Dim sqlstr19 As String = ""

        sqlstr19 = "SELECT        masirID, cod, masir, shiftType, citySiteType " & _
        " FROM serviceListTbl WHERE        (shiftType = 2) AND (citySiteType = 1)" & _
          " ORDER BY cod"

        Dim dt9 As New DataTable
        dt9 = ClsDataTable.GetDataTable(sqlstr19)
        citytToSiteNoShift19.DataSource = dt9
        citytToSiteNoShift19.DataValueField = "masirID"
        citytToSiteNoShift19.DataTextField = "masir"
        citytToSiteNoShift19.DataBind()


    End Sub
    Private Sub loadCmbsiteToCity8Nobatkar()



        Dim sqlstr19 As String = ""

        sqlstr19 = "SELECT        masirID, cod, masir, shiftType, citySiteType " & _
        " FROM serviceListTbl WHERE        (shiftType = 2) AND (citySiteType = 2)" & _
          " ORDER BY cod"

        Dim dt9 As New DataTable
        dt9 = ClsDataTable.GetDataTable(sqlstr19)
        siteToCityNoShift8.DataSource = dt9
        siteToCityNoShift8.DataValueField = "masirID"
        siteToCityNoShift8.DataTextField = "masir"
        siteToCityNoShift8.DataBind()


    End Sub
    'Private Sub loadCmbMotfarege()



    '    Dim sqlstr1m As String = ""

    '    sqlstr1m = "SELECT        masirID, cod, masir, shiftType, citySiteType " & _
    '    " FROM serviceListTbl WHERE        (shiftType = 4) AND (stat = 1)" & _
    '      " ORDER BY cod"

    '    Dim dtm As New DataTable
    '    dtm = ClsDataTable.GetDataTable(sqlstr1m)
    '    cmbmotafarage.DataSource = dtm
    '    cmbmotafarage.DataValueField = "masirID"
    '    cmbmotafarage.DataTextField = "masir"
    '    cmbmotafarage.DataBind()


    'End Sub
    
  
    

    

   
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
        If citytToSiteNoRuzkar.SelectedValue <> 0 Then
            save_tmp(citytToSiteNoRuzkar.SelectedValue)
            GridView2.Visible = True
            load_grid_tmp()
        End If


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
                Panel4.Visible = False
                '  Call save_()
                ' load_grid()
                'load_grid_Service()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub upRuzShab()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "upRuzShabPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@ruzkarShabkar", SqlDbType.Int, 5)
            cmm.Parameters("@ruzkarShabkar").Value = cmbRuzShabKar.SelectedValue

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
                'load_grid_Service()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try
    End Sub
    Private Sub load_grid_tmp()
        GridView2.Visible = True

        Dim sqlstr As String = ""
        sqlstr = "SELECT        TOP (200) dbo.servicePersonalTemp.idDtl, dbo.servicePersonalTemp.perID, dbo.servicePersonalTemp.serviceID, dbo.servicePersonalTemp.tempID, dbo.serviceListTbl.masir, dbo.serviceTypeTbl.typevalue, " & _
      "  dbo.citySiteTypeTbl.citySiteTypeValue" & _
              " FROM            dbo.servicePersonalTemp INNER JOIN" & _
                        " dbo.serviceListTbl ON dbo.servicePersonalTemp.serviceID = dbo.serviceListTbl.masirID INNER JOIN" & _
                        " dbo.serviceTypeTbl ON dbo.serviceListTbl.shiftType = dbo.serviceTypeTbl.typeID INNER JOIN" & _
                        " dbo.citySiteTypeTbl ON dbo.serviceListTbl.citySiteType = dbo.citySiteTypeTbl.citySiteTypeID" & _
       " WHERE(dbo.servicePersonalTemp.perID = " & Session("UserId") & ") "
        'AND (tempID = " & Val(hdn_tmp.Value) & ")
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub
    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from servicePersonalTemp where idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
            load_grid_Service()
        End If
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView1.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from servicePersonalTemp where idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_Service()
        End If
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If siteToCityNoRuzkar16.SelectedValue <> 173 Then
            save_tmp(siteToCityNoRuzkar16.SelectedValue)
            GridView2.Visible = True
            load_grid_tmp()
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If citytToSiteNoShift19.SelectedValue <> 172 Then
            save_tmp(citytToSiteNoShift19.SelectedValue)
            GridView2.Visible = True
            load_grid_tmp()
        End If
    End Sub



    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If siteToCityNoShift8.SelectedValue <> 170 Then
            save_tmp(siteToCityNoShift8.SelectedValue)
            GridView2.Visible = True
            load_grid_tmp()
            load_grid_Service()
        End If
    End Sub
    Private Sub load_grid_Service()
        GridView2.Visible = True
        Dim sqlstr As String = ""
        sqlstr = "SELECT        TOP (200) dbo.servicePersonalTemp.idDtl, dbo.servicePersonalTemp.perID, dbo.servicePersonalTemp.serviceID, dbo.servicePersonalTemp.tempID, dbo.serviceListTbl.masir, dbo.serviceTypeTbl.typevalue, " & _
      "  dbo.citySiteTypeTbl.citySiteTypeValue" & _
              " FROM            dbo.servicePersonalTemp INNER JOIN" & _
                        " dbo.serviceListTbl ON dbo.servicePersonalTemp.serviceID = dbo.serviceListTbl.masirID INNER JOIN" & _
                        " dbo.serviceTypeTbl ON dbo.serviceListTbl.shiftType = dbo.serviceTypeTbl.typeID INNER JOIN" & _
                        " dbo.citySiteTypeTbl ON dbo.serviceListTbl.citySiteType = dbo.citySiteTypeTbl.citySiteTypeID" & _
       " WHERE(dbo.servicePersonalTemp.perID = " & Session("UserId") & ") "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub

   

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then

            Panel12.Enabled = False

            loadCmbCityToSiteRuzkar8()
            loadCmbSiteToCityRuzkar16()
            'loadCmbMotfarege()
            loadCmbCityToSite19Nobatkar()
            loadCmbsiteToCity8Nobatkar()
            save_NoService()
        Else

            Panel12.Enabled = True
        End If

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

    Protected Sub cmbRuzShabKar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRuzShabKar.SelectedIndexChanged
        If cmbRuzShabKar.SelectedValue = 0 Then
        Else
            upRuzShab()
        End If

    End Sub
End Class
