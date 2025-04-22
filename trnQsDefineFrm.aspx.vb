Imports System.Data
Imports System.Data.SqlClient
Partial Class trnQsDefineFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then


            load_cmbDore()
            'load_grid_Sabage()

            load_cmbQsTyp()

            load_grid_tmp()

        End If

    End Sub
    Private Sub load_cmbDore()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        idd, onvan       FROM trnBarnameRiziTbl        WHERE        (hozuri = 2) AND (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbUnit.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            CmbUnit.DataTextField = "onvan"
            CmbUnit.DataValueField = "idd"
            CmbUnit.DataBind()

        End If


    End Sub

    Private Sub load_cmbQsTyp()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) qSTypeID, qsTypeValue FROM            trnQsTypeTbl"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        DropDownList1.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            DropDownList1.DataTextField = "qsTypeValue"
            DropDownList1.DataValueField = "qSTypeID"
            DropDownList1.DataBind()

        End If


    End Sub





    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        save_tmp(CmbUnit.SelectedValue, answerChk.Checked, soalTxt.Text, qozineTxt.Text, DropDownList1.SelectedValue)
        GridView1.Visible = True
        load_grid_tmp()
        UpdatePanel5.Update()



    End Sub
    Private Sub save_tmp(ByVal doreID As Integer, ByVal answer As Boolean, ByVal soal As String, ByVal gozine As String, ByVal qsType As Integer)

        If hdn_tmp.Value = 0 Then
            hdn_tmp.Value = ClsGetMaxId.GetMaxId("trnQsBaseTbl", "qsID")
        End If

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "trnQsDefinePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@doreID", SqlDbType.Int, 5)
            cmm.Parameters("@doreID").Value = CmbUnit.SelectedValue

            cmm.Parameters.Add("@answer", SqlDbType.Bit)
            cmm.Parameters("@answer").Value = answer

            cmm.Parameters.Add("@qsID", SqlDbType.Int, 5)
            cmm.Parameters("@qsID").Value = hdn_tmp.Value

            cmm.Parameters.Add("@soal", SqlDbType.NVarChar, 500)
            cmm.Parameters("@soal").Value = soal

            cmm.Parameters.Add("@gozine", SqlDbType.NVarChar, 500)
            cmm.Parameters("@gozine").Value = gozine

            cmm.Parameters.Add("@qsType", SqlDbType.Int, 5)
            cmm.Parameters("@qsType").Value = qsType

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                qozineTxt.Text = ""
                answerChk.Checked = False

                msg_box("  ثبت شد ")
                'load_grid_Sabage()
                GridView1.Visible = True
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Private Sub load_grid_tmp()


        Dim sqlstr As String = ""
        sqlstr = "SELECT        * " & _
       " FROM trnQsVW  WHERE       (doreID  = " & Val(CmbUnit.SelectedValue) & ") and (qsType = " & DropDownList1.SelectedValue & ")  ORDER BY qsType asc"
        'AND (tempID = " & Val(hdn_tmp.Value) & ")
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
        Else
            GridView1.Visible = False
        End If

    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView1.Rows(radif).Cells(1).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from trnQsDtlTbl where   dtlID=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
            'load_grid_Service()
        End If
        If e.CommandName = "sel" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView1.Rows(radif).Cells(1).Text.ToString)
            '  ins_temp(3, radif)
            Dim sqlstr As String = "SELECT        trnQsDtlTbl.qsID, trnQsBaseTbl.qsValue " & _
                       " FROM            trnQsDtlTbl INNER JOIN " & _
                        " trnQsBaseTbl ON trnQsDtlTbl.qsID = trnQsBaseTbl.qsID " & _
                     "  WHERE(trnQsDtlTbl.dtlID = " & radif & ")"
            Dim dt As New DataTable
            dt = ClsDataTable.GetDataTable(sqlstr)
            hdn_tmp.Value = dt.Rows(0).Item("qsID").ToString
            soalTxt.Text = dt.Rows(0).Item("qsValue").ToString
            'CmbUnit.SelectedValue = dt.Rows(0).Item("doreID").ToString
            'UpdatePanel5.Update()

        End If
    End Sub


    Private Sub load_grid_Sabage()

        Dim sqlstr As String = ""
        sqlstr = "SELECT        * " & _
       " FROM trnQsVW  WHERE       (doreID  = " & Val(CmbUnit.SelectedValue) & ")  "
        'AND (tempID = " & Val(hdn_tmp.Value) & ")
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView1.DataSource = dt
            GridView1.DataBind()

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





    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hdn_tmp.Value = 0
        qozineTxt.Text = ""
        soalTxt.Text = ""

    End Sub

    Protected Sub CmbUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbUnit.SelectedIndexChanged
        load_grid_tmp()
    End Sub

   
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        hdn_tmp.Value = 0
        load_grid_tmp()
    End Sub
End Class
