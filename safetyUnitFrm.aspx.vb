Imports System.Data
Imports System.Data.SqlClient
Partial Class safetyUnitFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            'load_cmbSize()
            'load_PerInfo()
            load_cmbUnits()
            load_grid_Sabage()

            'If Session("contratTyp") = 2 Or Session("contratTyp") = 4 Or Session("contratTyp") = 5 Then

            '    BtnAdd.Visible = True
            '    Panel1.Visible = True

            'Else
            '    msg_box("فعلا برای پرسنل رسمی و قرارداد معین و موقت امکان ثبت مقدور میباشد")
            '    BtnAdd.Visible = False
            '    Panel1.Visible = False
            'End If

            load_cmbkala()
            load_grid_tmp()
            'load_cmbSize()
        End If

    End Sub
    Private Sub load_cmbUnits()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       *         FROM omurTbl "
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbUnit.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            CmbUnit.TextField = "omur"
            CmbUnit.ValueField = "omurID"
            CmbUnit.DataBind()

        End If


    End Sub
    Private Sub load_cmbkala()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT       kalaID, CONVERT(VARCHAR(10),kalaID) + ' ' + kalaValue AS kala, setadi        FROM kalaTbl         WHERE (stat = 1) and  (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        kalaCmb.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            kalaCmb.TextField = "kala"
            kalaCmb.ValueField = "kalaID"
            kalaCmb.DataBind()

        End If


    End Sub
  
    'Private Sub load_PerInfo()

    '    Dim sqlstrrr As String = " SELECT        setadiAmaliati " & _
    '   " FROM personel    where    (ID = " & Session("UserId") & ") "

    '    Dim dttt As New DataTable
    '    dttt = ClsDataTable.GetDataTable(sqlstrrr)
    '    If dttt.Rows.Count > 0 Then
    '        cmbRuzShabKar.SelectedValue = dttt.Rows(0).Item("setadiAmaliati").ToString
    '    End If

    'End Sub




    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        save_tmp(kalaCmb.Value, 0, txt_tedad.Text)
        GridView2.Visible = True
        load_grid_tmp()

       





    End Sub
    Private Sub save_tmp(ByVal kalaID As Integer, ByVal SizeID As Integer, ByVal tedad As Integer)

        If Val(hdn_tmp.Value) = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("kalaPersonel", "tempID")
        End If

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "kalaInsUnitTempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@kalaID", SqlDbType.Int, 5)
            cmm.Parameters("@kalaID").Value = kalaID

            cmm.Parameters.Add("@sizeID", SqlDbType.Int, 5)
            cmm.Parameters("@sizeID").Value = SizeID

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@tedad", SqlDbType.Int, 5)
            cmm.Parameters("@tedad").Value = tedad

            cmm.Parameters.Add("@tozihat", SqlDbType.NVarChar, 500)
            cmm.Parameters("@tozihat").Value = tozihat.Text

            cmm.Parameters.Add("@unitID", SqlDbType.Int, 5)
            cmm.Parameters("@unitID").Value = CmbUnit.Value

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
                load_grid_Sabage()
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
        sqlstr = "SELECT        tozihat, kalaValue, sizeValue, tarix, saat, idDtl, perID,statValue " & _
       " FROM kalaUnitsVW  WHERE       (perID = " & Val(CmbUnit.Value) & ")  and (stat = 0)"
        'AND (tempID = " & Val(hdn_tmp.Value) & ")
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView2.DataSource = dt
            GridView2.DataBind()

        End If

    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView1.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from kalaUnits where   idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_Sabage()
            'load_grid_Service()
        End If
    End Sub


    Private Sub load_grid_Sabage()

        Dim sqlstr As String = ""
        sqlstr = "SELECT        tozihat, tedad,omur,kalaValue, sizeValue, tarix, saat, idDtl, perID,statValue,tarixThavil " & _
       " FROM kalaUnitsVW  WHERE       (perID = " & Val(CmbUnit.Value) & ")  and (stat = 1) order by tarix desc "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            lblSabege.Visible = True
            ''GridView2.Visible = True
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
        Else
            GridView1.Visible = False

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

   
  

    Protected Sub btnSabege_Click(sender As Object, e As EventArgs) Handles btnSabege.Click
        If CmbUnit.Value = 0 Then
            msg_box(" واحد مربوطه را انتخاب نمایید")

            Exit Sub
        Else
            Dim furl As String = "saftyUnitSabege.aspx?PwhereSTR=" & CmbUnit.Value & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=600,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)

        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        load_grid_Sabage()
    End Sub
End Class
