Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
imports System.Globalization
Imports System

Partial Class vaccinationPersonel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        If Me.IsPostBack = False Then

            loadCmbCityToSiteRuzkar8()
            load_grid_Service()

        End If
        'chekSahm()
        cheksumm()
    End Sub
    Private Sub chekSahm()
        Dim dts As New DataTable

        Dim sqlstrs As String = ""
        sqlstrs = "SELECT    zarfiat   FROM tblTour WHERE (idTour = " & citytToSiteNoRuzkar.SelectedValue & ")  "
        dts = ClsDataTable.GetDataTable(sqlstrs)
        If dts.Rows.Count > 0 Then
            lblzarf.Text = dts.Rows(0).Item("zarfiat").ToString
        End If
    End Sub
    Private Sub cheksumm()
        Dim dts As New DataTable
        lblTedad.Text = ""
        Dim sqlstrs As String = ""
        sqlstrs = "SELECT        id_tour, SUM(tedad) AS summ        FROM tblTourRegist " & _
       " GROUP BY id_tour         HAVING(id_tour = " & citytToSiteNoRuzkar.SelectedValue & ")"
        dts = ClsDataTable.GetDataTable(sqlstrs)
        If dts.Rows.Count > 0 Then
            lblTedad.Text = " تعداد ثبت نامی در این رشته  " & dts.Rows(0).Item("summ").ToString
            'lblBagi.Text = " ظرفیت باقیمانده  " & Val(lblzarf.Text) - Val(lblTedad.Text)
        Else
            lblTedad.Text = " تعداد ثبت نامی در این رشته  " & 0
        End If
    End Sub
    Private Sub loadCmbCityToSiteRuzkar8()

        Dim dtm As New DataTable
        Dim sqlstr As String
     


        sqlstr = "SELECT   magsad,  idTour, CodeClosed, zarfiat " & _
        "FROM         tblTour WHERE     (CodeClosed = 0) AND (zarfiat > dbo.GetZarfiyatTur(idTour)) "
        dtm = ClsDataTable.GetDataTable(sqlstr)
        If dtm.Rows.Count > 0 Then
            citytToSiteNoRuzkar.DataSource = dtm
            citytToSiteNoRuzkar.DataTextField = "magsad"
            citytToSiteNoRuzkar.DataValueField = "idTour"
            citytToSiteNoRuzkar.DataBind()

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
            sqlstr = "SELECT     sum(tedad) as tedadKol   FROM tblTourRegist WHERE (tblTourRegist.id_Tour = " & citytToSiteNoRuzkar.SelectedValue & ")  "
            dt2 = ClsDataTable.GetDataTable(sqlstr)
            Dim tedad As Integer = 0
            If dt2.Rows.Count > 0 Then
                tedad = Val(dt2.Rows(0).Item("tedadKol").ToString)
            End If


            Dim sqlstrZarf As String = ""
            sqlstrZarf = "SELECT    zarfiat   FROM tblTour WHERE (idTour = " & citytToSiteNoRuzkar.SelectedValue & ")  "
            dt5 = ClsDataTable.GetDataTable(sqlstrZarf)

            Dim zarfTur As Integer = Val(dt5.Rows(0).Item("zarfiat").ToString)



            If tedad + Val(txtTedad.Text) > zarfTur Then
                msg_box("  متاسفنه ظرفیت تکمیل شد")
                Exit Sub
            End If

            Dim sqlstrr As String = ""
            sqlstrr = "SELECT        idd, tarix, stat " & _
" FROM tblTourRegist " & _
   " WHERE        (codePersonel = " & Session("codePersoneli") & ") AND (tarix >= '" & ClsDateTime.GetDateNow & "')  AND (stat = 1)"

            Dim dtr As New DataTable
            dtr = ClsDataTable.GetDataTable(sqlstrr)
            If dtr.Rows.Count > 0 Then
                msg_box("بیشتر از یکبار در روز نمی توانید رزرو نمایید")
                Exit Sub

            End If


            Dim sqlstrSahm As String = ""
            Dim mah As String = Left(ClsDateTime.GetDateNow(), 7)
            sqlstrSahm = "SELECT         idd             FROM tblTourRegist " & _
                         " WHERE        (mah = '" & mah & "') AND (codePersonel = " & Session("codePersoneli") & ") "
            dtSahm = ClsDataTable.GetDataTable(sqlstrSahm)

            'If dtSahm.Rows.Count >= Val(lblSahm.Text) Then
            '    msg_box("سهمیه شما در این ماه به اتمام رسیده است")
            '    Exit Sub
            'End If


            'Dim flag_rooz_az_mah As Boolean = False

            'Dim startDate As String = ""
            'Dim EndDate As String = ""
            'Dim start1 As Integer = 1
            'Dim End1 As Integer = 7
            'Dim start2 As Integer = 8
            'Dim End2 As Integer = 15
            'Dim sqlstrh As String = ""

            'Dim rooz As Integer = ClsDateTime.getRoozMah()

            Dim x As String
       
            Dim w As String = ""
            Dim dd As New Globalization.GregorianCalendar
            x = dd.GetDayOfWeek(Now)
            Dim start As Date
            Dim today = Date.Now()

            If x = 1 Then
                w = "  دوشنبه "
                start = today.AddDays(-2)
            ElseIf x = 2 Then
                w = "  سه شنبه "
                start = today.AddDays(-3)
            ElseIf x = 3 Then
                w = "  چهارشنبه "
                start = today.AddDays(-4)
            ElseIf x = 4 Then
                w = "  پنج شنبه "
                start = today.AddDays(-5)

            ElseIf x = 5 Then
                w = "  جمعه "
                start = today.AddDays(-6)
            ElseIf x = 6 Then
                w = "  شنبه "
                start = DateTime.Now
            ElseIf x = 0 Then
                w = "  یکشنبه "
                start = today.AddDays(-1)
            End If
            Dim st, too As String
            st = Left(start, 10)
            too = Left(today, 10)

            Dim sqlstrs As String = ""
            sqlstrs = "SELECT        idd, tarixm, stat " & _
     " FROM tblTourRegist " & _
        " WHERE        (codePersonel = " & Session("codePersoneli") & ") AND (tarixm >= '" & st.ToString.Trim & "') AND (tarixm <= '" & too.ToString.Trim & "') "

            ' sqlstrs = "SELECT        TOP (200) codePersonel " & _
            '" FROM tblTourRegist " & _
            '            " WHERE(codePersonel = " & Session("codePersoneli") & ") And (stat <> 4)"

            Dim dtsabtt As New DataTable
            dtsabtt = ClsDataTable.GetDataTable(sqlstrs)
            If dtsabtt.Rows.Count > 1 Then
                msg_box("سهمیه شما در این هفته به اتمام رسیده است")
                Exit Sub

            End If






            save_final()
        End If

    End Sub

    Private Function CreateDate(ruuz As String) As String
        Dim tarix As String = ""
        Dim sal As String = ClsDateTime.getSal()
        Dim mah As String = ClsDateTime.getMah()
        If mah.Length = 1 Then
            mah = "0" & mah
        End If
        tarix = sal + "/" + mah + "/"
        If ruuz.Length = 1 Then
            ruuz = "0" & ruuz
        End If
        tarix = tarix & ruuz
        Return tarix
    End Function
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

            cmm.CommandText = "PrcInsTourRegist"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@id_tour", SqlDbType.Int, 6)
            cmm.Parameters("@id_tour").Value = citytToSiteNoRuzkar.SelectedValue


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


    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from tblTourRegist where  (stat = 1) AND  idd=" & radif
            'sqlstr = "UPDATE     tblTourRegist SET                stat = 4 where idd= " & e.CommandArgs.CommandArgument.ToString() & ""
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()

            load_grid_Service()
        End If
    End Sub

    Private Sub load_grid_Service()
        GridView2.Visible = True
        Dim sqlstr As String = ""
        sqlstr = "SELECT        TOP (200) tblTourRegist.idd, tblTourRegist.id_tour, tblTour.magsad, tblTourRegist.tedad, turRegistStatTbl.statValue " & _
               " FROM            tblTourRegist INNER JOIN " & _
                        " tblTour ON tblTourRegist.id_tour = tblTour.idTour INNER JOIN " & _
                        " turRegistStatTbl ON tblTourRegist.stat = turRegistStatTbl.statID " & _
       " WHERE(tblTourRegist.codePersonel = " & Session("codePersoneli") & ") " & _
" ORDER BY tblTourRegist.idd DESC"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView2.DataSource = dt
            GridView2.DataBind()
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


    Protected Sub citytToSiteNoRuzkar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles citytToSiteNoRuzkar.SelectedIndexChanged
        'chekSahm()

        cheksumm()
    End Sub
End Class
