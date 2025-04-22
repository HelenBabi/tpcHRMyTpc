Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Class productUnitDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            'load_cmbSize()


            load_grid_Sabage()

            load_grid_tmp()
            'load_cmbSize()
        End If

    End Sub

    Private Sub load_grid_Sabage()

        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        TOP (100) PERCENT codePersoneli, typeValue, value, nam, family, perID,UnitsName,omur " & _
                "FROM            dbo.kalaPersonelReportVW AS kalaPersonelReportVW_1 " & _
          "GROUP BY codePersoneli, typeValue, value, nam, family, UnitsCode, stat, perID,UnitsName,omur " & _
        "HAVING        (UnitsCode = " & Session("unitID") & ") AND (stat = 1)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            'lblSabege.Visible = True
            ASPxGrvHead.DataSource = dt
            ASPxGrvHead.DataBind()
        End If




    End Sub
    '    Private Sub load_grid_PerInfoo()

    '        Dim sqlstr As String = ""
    '        sqlstr = "SELECT        TOP (200) dbo.personel.nam + ' ' + dbo.personel.family AS person, dbo.personel.codePersoneli, dbo.personelTypeTbl.typeValue, dbo.contratType.value, dbo.tblUnits.UnitsName " & _
    '"FROM            dbo.personel INNER JOIN " & _
    '                         "dbo.personelTypeTbl ON dbo.personel.setadiAmaliati = dbo.personelTypeTbl.typeID INNER JOIN " & _
    '                       "  dbo.contratType ON dbo.personel.typ = dbo.contratType.id INNER JOIN " & _
    '                      "   dbo.tblUnits ON dbo.personel.UnitsCode = dbo.tblUnits.UnitsCode " & _
    '      "  WHERE(dbo.personel.codePersoneli = " & Session("perIDD") & ")"
    '        Dim dt As New DataTable
    '        dt = ClsDataTable.GetDataTable(sqlstr)
    '        If dt.Rows.Count > 0 Then
    '            GridView3.Visible = True
    '            GridView3.DataSource = dt
    '            GridView3.DataBind()
    '        End If


    '    End Sub




    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("productBrowseFrm.aspx")
    End Sub



    Private Sub load_grid_tmp()
        GridView2.Visible = False
        Panel3.Visible = False

        Dim sqlstr As String = ""
        sqlstr = "SELECT         kalaValue, sizeValue, tarix, saat, idDtl, perID,statValue,codePersoneli, UnitsCode, nam, family,value " & _
       " FROM kalaPersonelVW       WHERE(UnitsCode  = " & Session("unitID") & ") and (stat = 0)"
        'AND (tempID = " & Val(hdn_tmp.Value) & ")
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            Panel3.Visible = True
            GridView2.Visible = True

            GridView2.DataSource = dt
            GridView2.DataBind()

        End If

    End Sub
    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "deliver" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "UPDATE       kalaPersonel SET stat=1,  tarixThavil = '" & ClsDateTime.GetDateNow & "', tahvilUser = " & Session("UserId") & " where stat = 0  and idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
            load_grid_Sabage()
            'load_grid_Service()
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
    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub
End Class
