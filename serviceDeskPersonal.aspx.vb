Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
imports System.Globalization
Imports System

Partial Class serviceDeskPersonal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        If Me.IsPostBack = False Then

            loadCmbCityToSiteRuzkar8()
            load_grid_Service()
            loadCmbtime()
        End If
        chekSahm()
        cheksumm()
    End Sub
    Private Sub chekSahm()
        Dim dts As New DataTable

        Dim sqlstrs As String = ""
        sqlstrs = "SELECT    zarfiat   FROM serviceDeskTbl WHERE (idTour = " & Val(citytToSiteNoRuzkar.SelectedValue) & ")  "
        dts = ClsDataTable.GetDataTable(sqlstrs)
        If dts.Rows.Count > 0 Then
            lblzarf.Text = dts.Rows(0).Item("zarfiat").ToString
        End If
    End Sub
    Private Sub cheksumm()
        Dim dts As New DataTable
        lblTedad.Text = ""
        Dim sqlstrs As String = ""
        sqlstrs = "SELECT        id_tour        FROM serviceDeskDtlTbl " & _
       " where (id_tour = " & Val(citytToSiteNoRuzkar.SelectedValue) & ") and (hazf = 0) "
        dts = ClsDataTable.GetDataTable(sqlstrs)
        If dts.Rows.Count > 0 Then
            lblTedad.Text = " تعداد رزرو در این زمان  " & dts.Rows.Count
            lblBagi.Text = " ظرفیت باقیمانده  " & Val(lblzarf.Text) - dts.Rows.Count & " نفر "
        Else
            lblTedad.Text = " تعداد رزرو در این زمان  " & 0
            lblBagi.Text = " ظرفیت باقیمانده  " & Val(lblzarf.Text) & " نفر "
        End If
    End Sub
    Private Sub loadCmbCityToSiteRuzkar8()

        Dim dtm As New DataTable
        Dim sqlstr As String
      
        sqlstr = "SELECT   magsad,zarfiat,  idTour " & _
        "FROM         serviceDeskTbl WHERE     (CodeClosed = 0) AND (zarfiat > dbo.GetZarfiyatServiceDesk(idTour)) "
        dtm = ClsDataTable.GetDataTable(sqlstr)
        If dtm.Rows.Count > 0 Then
            citytToSiteNoRuzkar.DataSource = dtm
            citytToSiteNoRuzkar.DataTextField = "magsad"
            citytToSiteNoRuzkar.DataValueField = "idTour"
            citytToSiteNoRuzkar.DataBind()
        Else
            Dim sqlstr2 As String = "SELECT        TOP (1) idTour, magsad " & _
     " FROM serviceDeskTbl  ORDER BY idTour DESC"
            Dim dt2 As New DataTable
            dt2 = ClsDataTable.GetDataTable(sqlstr2)
            lblzarf.Visible = True
            lblzarf.Text = "ظرفیت  رزرو تا " & dt2.Rows(0).Item("magsad").ToString & " تکمیل میباشد "
            msg_box("ظرفیت  رزرو تا " & dt2.Rows(0).Item("magsad").ToString & " تکمیل میباشد ")
            citytToSiteNoRuzkar.Enabled = False
            timeCmb.Enabled = False
            tozih.Enabled = False
            BtnAdd.Visible = False
        End If




    End Sub
    Private Sub loadCmbtime()

        Dim dtm As New DataTable
        Dim sqlstr As String



        sqlstr = "SELECT        TOP (20) timeID, startTime + ' ' + endTime as saat" & _
       " FROM serviseDeskTimeTbl" & _
                    " WHERE        (stat = 1) AND (timeID NOT IN" & _
                            " (SELECT        timeID " & _
       " FROM serviceDeskDtlTbl WHERE           (id_tour = " & Val(citytToSiteNoRuzkar.SelectedValue) & ") AND (hazf = 0) ))"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        If dtm.Rows.Count > 0 Then
            timeCmb.DataSource = dtm
            timeCmb.DataTextField = "saat"
            timeCmb.DataValueField = "timeID"
            timeCmb.DataBind()

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
        If citytToSiteNoRuzkar.SelectedValue <> 0 Then
            'save_tmp(citytToSiteNoRuzkar.SelectedValue)
            'GridView2.Visible = True
            'load_grid_tmp()





            Dim dt2, dt5, dtSahm As New DataTable
            Dim sqlstr As String = ""
            sqlstr = "SELECT     tedad   FROM serviceDeskDtlTbl WHERE (serviceDeskDtlTbl.id_Tour = " & Val(citytToSiteNoRuzkar.SelectedValue) & ") and        (hazf = 0) "
            dt2 = ClsDataTable.GetDataTable(sqlstr)
            Dim tedad As Integer = 0
            If dt2.Rows.Count > 0 Then
                tedad = dt2.Rows.Count
            End If


            Dim sqlstrZarf As String = ""
            sqlstrZarf = "SELECT    zarfiat   FROM serviceDeskTbl WHERE (idTour = " & Val(citytToSiteNoRuzkar.SelectedValue) & ")  "
            dt5 = ClsDataTable.GetDataTable(sqlstrZarf)

            Dim zarfTur As Integer = Val(dt5.Rows(0).Item("zarfiat").ToString)



            If tedad > zarfTur Then
                msg_box("  متاسفنه ظرفیت تکمیل شد")
                Exit Sub
            End If

            Dim sqlstrr As String = ""
            sqlstrr = "SELECT        idd, tarix, stat " & _
" FROM serviceDeskDtlTbl " & _
   " WHERE        (codePersonel = " & Session("codePersoneli") & ")  AND (hazf <> 1)"

            Dim dtr As New DataTable
            dtr = ClsDataTable.GetDataTable(sqlstrr)
            If dtr.Rows.Count > 0 Then
                msg_box("بیشتر از یکبار  نمی توانید رزرو نمایید")
                Exit Sub

            End If
            If Len(tozih.Text) > 50 Then
                msg_box("تعداد کارکتر توضیحات بیشتر از 50 است")
                tozih.Focus()
                Exit Sub

            End If

            save_final()
        End If

    End Sub

    
    Private Sub save_final()
       
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "servicedeskPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@id_tour", SqlDbType.Int, 6)
            cmm.Parameters("@id_tour").Value = Val(citytToSiteNoRuzkar.SelectedValue)


            cmm.Parameters.Add("@timeID", SqlDbType.Int, 6)
            cmm.Parameters("@timeID").Value = timeCmb.SelectedValue

            cmm.Parameters.Add("@codePersonel", SqlDbType.VarChar, 10)
            cmm.Parameters("@codePersonel").Value = Session("codePersoneli")

         

            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 50)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress

            cmm.Parameters.Add("@tozih", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tozih").Value = tozih.Text

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
                loadCmbtime()
                chekSahm()
                cheksumm()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub



    Private Sub load_grid_Service()
        GridView2.Visible = True
        Dim sqlstr As String = ""
        sqlstr = "SELECT       * from serviceDeskGridVW WHERE codePersonel = " & Session("codePersoneli") & " "


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            'GridView2.Visible = True
            GridView2.DataSource = dt
            GridView2.DataBind()
            'Else
            'GridView2.Visible = False

        End If


    End Sub




    


    Protected Sub citytToSiteNoRuzkar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles citytToSiteNoRuzkar.SelectedIndexChanged
        'chekSahm()

        cheksumm()
        loadCmbtime()

    End Sub

    Protected Sub lnkDelFile_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "serviceDeskEbtalPrc"
            cmm.CommandType = CommandType.StoredProcedure

            Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
            Dim fileid As Integer = Convert.ToInt32(GridView2.DataKeys(gvrow.RowIndex).Value.ToString())


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
                chekSahm()
                cheksumm()
                loadCmbtime()

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try
        load_grid_Service()

    End Sub
End Class
