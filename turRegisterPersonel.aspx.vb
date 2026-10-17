Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
imports System.Globalization
Imports System

Partial Class turRegisterPersonel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        If Me.IsPostBack = False Then

            loadCmbCityToSiteRuzkar8()
            load_grid_Service()

        End If
        'LblTakafolCount.Visible = True

        'LblTakafolCount.Text = GetTakafol()
        loadMsg()

    End Sub
    Private Sub loadMsg()
        Dim dtm As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        TOP (200) idd, valuee        FROM turMessageTbl WHERE        (CodeClosed = 0)"
        'AND (zarfiat > dbo.GetZarfiyatTur(idTour)) "
        dtm = ClsDataTable.GetDataTable(sqlstr)
        If dtm.Rows.Count > 0 Then
            lblMsg.Text = dtm.Rows(0).Item("valuee").ToString.Replace(vbCrLf, "<br/>")
        Else
            lblMsg.Text = "-"
        End If

    End Sub
    Private Sub loadCmbCityToSiteRuzkar8()

        Dim dtm As New DataTable
        Dim sqlstr As String



        sqlstr = "SELECT   magsad,  idTour, CodeClosed, zarfiat " & _
        "FROM         turRefahiTbl WHERE     (CodeClosed = 0)"
        'AND (zarfiat > dbo.GetZarfiyatTur(idTour)) "
        dtm = ClsDataTable.GetDataTable(sqlstr)
        If dtm.Rows.Count > 0 Then
            CmbTur.DataSource = dtm
            CmbTur.TextField = "magsad"
            CmbTur.ValueField = "idTour"
            CmbTur.DataBind()
            Dim i As Integer = 0

            'For index = 0 To dtm.Rows.Count - 1
            '    citytToSiteNoRuzkar.SelectedItem.Value.=(System.Drawing.Color.FromKnownColor(0), 0)
            'Next index


        Else
            'lblPayam.Text = "فعلا برنامه ای برای رزرو وجود ندارد"
            BtnAdd.Visible = False
            txtTedad.Enabled = False
            tozih.Enabled = False
            GridView2.Enabled = False

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
        If CmbTur.Value <> 0 Then

            


            Dim sqlstrr As String = ""
            sqlstrr = "SELECT        turRefahiRegTbl.codePersonel, turRefahiRegTbl.stat " & _
                   "FROM            turRefahiRegTbl INNER JOIN " & _
                        " turRefahiTbl ON turRefahiRegTbl.id_tour = turRefahiTbl.idTour " & _
                 "WHERE        (turRefahiRegTbl.codePersonel = " & Session("codePersoneli") & ") AND (turRefahiTbl.CodeClosed = 0) AND (turRefahiRegTbl.stat = 5) OR " & _
                        " (turRefahiRegTbl.stat = 6)"

            Dim dtt As New DataTable
            dtt = ClsDataTable.GetDataTable(sqlstrr)
            If dtt.Rows.Count >= 3 Then

                msg_box("شما 3بار رزرو کرده اید")
                Exit Sub

            Else
                If Val(txtTedad.Text) > Val(GetTakafol()) Then
                    msg_box("تعداد مجاز نمی باشد")
                    Exit Sub
                Else
                    save_final()
                End If
            End If



        End If

    End Sub

    
    Private Sub save_final()
        Dim tedad As Integer = 0
        tedad = Val(txtTedad.Text)
        If tedad < 1 Then
            msg_box("  تعداد نمی تواند کوچکتر از یک باشد ")
            txtTedad.Focus()
            Exit Sub

        End If
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "turRefahiPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@id_tour", SqlDbType.Int, 6)
            cmm.Parameters("@id_tour").Value = CmbTur.Value


            cmm.Parameters.Add("@tedad", SqlDbType.Int, 6)
            cmm.Parameters("@tedad").Value = tedad

            cmm.Parameters.Add("@codePersonel", SqlDbType.VarChar, 10)
            cmm.Parameters("@codePersonel").Value = Session("codePersoneli")

            cmm.Parameters.Add("@codeKuhnavardi", SqlDbType.Int, 10)
            cmm.Parameters("@codeKuhnavardi").Value = 0

            cmm.Parameters.Add("@namee", SqlDbType.NVarChar, 100)
            cmm.Parameters("@namee").Value = 0

            cmm.Parameters.Add("@famili", SqlDbType.NVarChar, 150)
            cmm.Parameters("@famili").Value = 0

            cmm.Parameters.Add("@mahalekar", SqlDbType.NVarChar, 100)
            cmm.Parameters("@mahalekar").Value = 0

            cmm.Parameters.Add("@telfonDaxeli", SqlDbType.VarChar, 50)
            cmm.Parameters("@telfonDaxeli").Value = 0

            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 50)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress


            cmm.Parameters.Add("@mobilee", SqlDbType.VarChar, 50)
            cmm.Parameters("@mobilee").Value = 0

            cmm.Parameters.Add("@namehamrah", SqlDbType.NVarChar, 100)
            cmm.Parameters("@namehamrah").Value = 0

            cmm.Parameters.Add("@nesbat", SqlDbType.NVarChar, 100)
            cmm.Parameters("@nesbat").Value = 0

            cmm.Parameters.Add("@tarix", SqlDbType.VarChar, 50)
            cmm.Parameters("@tarix").Value = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow


            cmm.Parameters.Add("@start", SqlDbType.VarChar, 10)
            cmm.Parameters("@start").Value = Left(DateTime.Now, 10).ToString.Trim

            cmm.Parameters.Add("@tozih", SqlDbType.NVarChar, 1000)
            cmm.Parameters("@tozih").Value = tozih.Text


            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output


            cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
            cmm.Parameters("@type_").Value = 1


            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  ثبت شد ")
                load_grid_Service()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub


    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
         
            Dim cnn As New SqlConnection
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            Try
                Dim cmm As New SqlCommand

                cmm.CommandText = "turRefahiEbtalPrc"
                cmm.CommandType = CommandType.StoredProcedure

                Dim radif As Int64 = e.CommandArgument
                radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)


                cmm.Parameters.Add("@idd", SqlDbType.Int, 5)
                cmm.Parameters("@idd").Value = radif

                cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 50)
                cmm.Parameters("@ipp").Value = Request.UserHostAddress



                cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
                cmm.Parameters("@pm").Direction = ParameterDirection.Output

                cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
                cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
                cmm.Connection = cnn

                Dim result As Integer
                cmm.ExecuteNonQuery()
                result = cmm.Parameters("@ResultCode").Value
                If result = 0 Then
                    msg_box("  ابطال شد ")
                    load_grid_Service()
                Else
                    msg_box(cmm.Parameters("@pm").Value)
                End If
            Catch ex As Exception
                msg_box(ex.Message.ToString)
            End Try
            load_grid_Service()
        End If
    End Sub



    Protected Sub lnkDelFile_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(GridView2.DataKeys(gvrow.RowIndex).Value.ToString())

        Dim sqlstr As String = "select idd from turGridVW where idd= " & fileid & " and stat = 5"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            Dim cnn As New SqlConnection
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            Try
                Dim cmm As New SqlCommand

                cmm.CommandText = "turRefahiEbtalPrc"
                cmm.CommandType = CommandType.StoredProcedure

                cmm.Parameters.Add("@idd", SqlDbType.Int, 5)
                cmm.Parameters("@idd").Value = fileid

                cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 50)
                cmm.Parameters("@ipp").Value = Request.UserHostAddress

                cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
                cmm.Parameters("@pm").Direction = ParameterDirection.Output

                cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
                cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
                cmm.Connection = cnn

                Dim result As Integer
                cmm.ExecuteNonQuery()
                result = cmm.Parameters("@ResultCode").Value
                If result = 0 Then
                    msg_box("  ابطال شد ")
                    load_grid_Service()
                Else
                    msg_box(cmm.Parameters("@pm").Value)
                End If
            Catch ex As Exception
                msg_box(ex.Message.ToString)
            End Try
            load_grid_Service()
        End If



    End Sub


    Private Sub load_grid_Service()
        GridView2.Visible = True
        Dim sqlstr As String = ""
        '        sqlstr = "SELECT        TOP (200) turRegistStatTbl.idd, turRegistStatTbl.id_tour, turRefahiTbl.magsad, turRegistStatTbl.tedad, turRegistStatTbl.statValue " & _
        '               " FROM            turRegistStatTbl INNER JOIN " & _
        '                        " turRefahiTbl ON turRegistStatTbl.id_tour = turRefahiTbl.idTour INNER JOIN " & _
        '                        " turRegistStatTbl ON turRegistStatTbl.stat = turRegistStatTbl.statID " & _
        '       " WHERE(turRegistStatTbl.codePersonel = " & Session("codePersoneli") & ") " & _
        '" ORDER BY turRegistStatTbl.idd DESC"

        sqlstr = "SELECT       * from turGridVW WHERE (codePersonel = " & Session("codePersoneli") & ") and   (stat <> 17) ORDER BY idd DESC"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView2.DataSource = dt
            GridView2.DataBind()
        End If


    End Sub

    Private Function GetTakafol() As Integer
        Dim res As Integer

        Dim sqlstr As String = "SELECT sponsorshipNum  FROM personel  WHERE (codePersoneli  = " & Session("codePersoneli").ToString & ") "
        'AND (IsActive = 1) "
        'AND (credit > N'" & tarix & "')"
        res = Val(ClsDataTable.GetCommandScalar(sqlstr))
        res += 1
        Return res
    End Function


    


   
End Class
