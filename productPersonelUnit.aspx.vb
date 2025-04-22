Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class productPersonelUnit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            'load_cmbSize()
            load_PerInfo()
            load_cmbPersonel()
            load_grid_Sabage()
            load_cmbkala()
            load_grid_tmp()
            'load_cmbSize()
        End If

    End Sub

    Private Sub load_cmbPersonel()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      personn, ID " & _
      "  FROM personelKalaReqVW         WHERE(UnitsCode = " & Session("unitID") & ") "


        dtm = ClsDataTable.GetDataTable(sqlstr)
        cmbPersonel.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            cmbPersonel.TextField = "personn"
            cmbPersonel.ValueField = "ID"
            cmbPersonel.DataBind()

        End If
     


    End Sub
    Private Sub load_cmbkala()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        kalaID, kalaValue, setadi         FROM kalaTbl         WHERE(stat = 1) and (setadi = " & cmbRuzShabKar.SelectedValue & ") AND (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        kalaCmb.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            kalaCmb.DataTextField = "kalaValue"
            kalaCmb.DataValueField = "kalaID"
            kalaCmb.DataBind()
            load_cmbSize()
        End If


    End Sub
    Private Sub load_cmbSize()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT         sizeID, kalaID, sizeValue + '  ' + sizeNote as sizee, stat" & _
      "  FROM kalaSizeTbl         WHERE(kalaID = " & kalaCmb.SelectedValue & ") AND (CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            sizeCmb.DataSource = dt
            sizeCmb.DataTextField = "sizee"
            sizeCmb.DataValueField = "sizeID"
            sizeCmb.DataBind()

        End If

    End Sub
    Private Sub load_PerInfo()

        Dim sqlstrrr As String = " SELECT        setadiAmaliati " & _
       " FROM personel    where    (ID = " & Session("UserId") & ") "

        Dim dttt As New DataTable
        dttt = ClsDataTable.GetDataTable(sqlstrrr)
        If dttt.Rows.Count > 0 Then
            cmbRuzShabKar.SelectedValue = dttt.Rows(0).Item("setadiAmaliati").ToString
        End If

    End Sub




    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        save_tmp(kalaCmb.SelectedValue, sizeCmb.SelectedValue)
        GridView2.Visible = True
        load_grid_Sabage()
        'load_grid_tmp()



    End Sub
    Private Sub save_tmp(ByVal kalaID As Integer, ByVal SizeID As Integer)

        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("kalaPersonel", "tempID")
        End If

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "kalaInsTempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@kalaID", SqlDbType.Int, 5)
            cmm.Parameters("@kalaID").Value = kalaID

            cmm.Parameters.Add("@sizeID", SqlDbType.Int, 5)
            cmm.Parameters("@sizeID").Value = SizeID

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@perID", SqlDbType.Int, 5)
            cmm.Parameters("@perID").Value = cmbPersonel.Value

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
                upRuzShab()
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

            cmm.CommandText = "upsetadiAmaliatiPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@setadiAmaliati", SqlDbType.Int, 5)
            cmm.Parameters("@setadiAmaliati").Value = cmbRuzShabKar.SelectedValue

            cmm.Parameters.Add("@perID", SqlDbType.Int, 5)
            cmm.Parameters("@perID").Value = cmbPersonel.Value

   
            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then

                'msg_box("  ثبت شد ")
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
        sqlstr = "SELECT        nam, family,codePersoneli,kalaValue, sizeValue, tarix, saat, idDtl, perID,statValue,value " & _
       " FROM kalaPersonelVW  WHERE       (UnitsCode = " & Session("unitID") & ")  and (stat = 0)"
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
            cmm.CommandText = "delete from kalaPersonel where stat = 0  and idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
            'load_grid_Service()
        End If
    End Sub


    Private Sub load_grid_Sabage()

        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        TOP (100) PERCENT codePersoneli, typeValue, value, nam, family, perID " & _
                "FROM            dbo.kalaPersonelReportVW AS kalaPersonelReportVW_1 " & _
          "GROUP BY codePersoneli, typeValue, value, nam, family, UnitsCode, stat, perID " & _
        "HAVING        (UnitsCode = " & Session("unitID") & ") AND (stat = 1)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            'lblSabege.Visible = True
            ASPxGrvHead.DataSource = dt
            ASPxGrvHead.DataBind()
        End If




    End Sub






    Protected Sub cmbRuzShabKar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRuzShabKar.SelectedIndexChanged
        If cmbRuzShabKar.SelectedValue = 0 Then
            lblKala.Visible = False
            lblSize.Visible = False
            kalaCmb.Visible = False
            sizeCmb.Visible = False
            BtnAdd.Visible = False

        Else
            'upRuzShab()
            lblKala.Visible = True
            lblSize.Visible = True
            kalaCmb.Visible = True
            sizeCmb.Visible = True
            BtnAdd.Visible = True

            load_cmbkala()

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

    Protected Sub kalaCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles kalaCmb.SelectedIndexChanged
        load_cmbSize()
    End Sub

    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("perID").ToString



        Dim sqlstr As String = ""
        sqlstr = "SELECT        tarixThavil, kalaValue, sizeValue, tarix, saat, idDtl, perID,statValue,stat " & _
       " FROM kalaPersonelVW  WHERE       (perID = " & idd & ")  "
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        grid.DataSource = dt
        'grid.DataBind()



    End Sub


    Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("stat"))
        If value = 5 Then
            'e.Row.ForeColor = Drawing.Color.ligh
            '   e.Row.BackColor = Drawing.Color.Aquamarine
        Else
            'e.Row.ForeColor = Drawing.Color.Green
            'e.Row.BackColor = Drawing.Color.LightGreen
        End If
    End Sub

   
    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub
End Class
