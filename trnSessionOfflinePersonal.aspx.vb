Imports System.Data
Imports System.Data.SqlClient
'Imports System.Globalization.PersianCalendar
imports System.Globalization
Imports System

Partial Class trnSessionOfflinePersonal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        If Me.IsPostBack = False Then

            loadCmbCityToSiteRuzkar8()
            load_grid_Service()

        End If
      

    End Sub




    Private Sub loadCmbCityToSiteRuzkar8()

        Dim dtm As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        idd, onvan + ' ' + tozih  AS doree " & _
       " FROM trnBarnameRiziTbl WHERE         (CodeClosed = 0) AND         (hozuri = 2) And (stat <> 13)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        If dtm.Rows.Count > 0 Then
            citytToSiteNoRuzkar.DataSource = dtm
            citytToSiteNoRuzkar.DataTextField = "doree"
            citytToSiteNoRuzkar.DataValueField = "idd"
            citytToSiteNoRuzkar.DataBind()
        Else

            lblzarf.Visible = True
            lblzarf.Text = "فعلا دوره ای جهت ثبت نام وجود ندارد "
            msg_box("فعلا دوره ای جهت ثبت نام وجود ندارد ")
            citytToSiteNoRuzkar.Enabled = False


            BtnAdd.Visible = False
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

      

            Dim sqlstrr As String = ""
            sqlstrr = "SELECT        trnDoreReqisterTbl.idd " & _
                 "FROM            trnDoreReqisterTbl INNER JOIN " & _
                        " trnBarnameRiziTbl ON trnDoreReqisterTbl.id_tour = trnBarnameRiziTbl.idd " & _
                   " WHERE(trnDoreReqisterTbl.codePersonel = " & Session("codePersoneli") & ") And (trnDoreReqisterTbl.hazf = 0) And (trnBarnameRiziTbl.hozuri = 2) "

            Dim dtr As New DataTable
            dtr = ClsDataTable.GetDataTable(sqlstrr)
            If dtr.Rows.Count > 0 Then
                msg_box("بیشتر از یک دوره  نمی توانید شرکت نمایید")
                Exit Sub

            End If

            'If HiddenFieldSabege.Value <= 10 Then
            save_final()
            'Else
            '    msg_box("سابقه شما بیشتر از 10 سال میباشد")
            '    Exit Sub
            '    BtnAdd.Visible = False

            'End If

        End If

    End Sub


    Private Sub save_final()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnDoreRegisterPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@id_tour", SqlDbType.Int, 6)
            cmm.Parameters("@id_tour").Value = Val(citytToSiteNoRuzkar.SelectedValue)



            cmm.Parameters.Add("@codePersonel", SqlDbType.VarChar, 10)
            cmm.Parameters("@codePersonel").Value = Session("codePersoneli")



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
                msg_box("  ثبت شد ")
                load_grid_Service()
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
        sqlstr = "SELECT       * from trnDoreRegisterOfflineVW WHERE codePersonel = " & Session("codePersoneli") & "  And hozuri = 2 "


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView2.Visible = True
            GridView2.DataSource = dt
            GridView2.DataBind()
            'Else
            'GridView2.Visible = False

        End If


    End Sub






    Protected Sub lnkDelFile_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sqlstr As String = ""
        sqlstr = "SELECT       * from trnDoreReqisterTbl WHERE codePersonel = " & Session("codePersoneli") & " and stat = 10"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then


            Dim cnn As New SqlConnection
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            Try
                Dim cmm As New SqlCommand

                cmm.CommandText = "trnRegisterEbtalPrc"
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
                   


                Else
                    msg_box(cmm.Parameters("@pm").Value)
                End If
            Catch ex As Exception
                msg_box(ex.Message.ToString)
            End Try
            load_grid_Service()
        Else
            msg_box("فقط درخواست های در وضعیت ثبت نام قابلیت ابطال دارند")
            Exit Sub

        End If

    End Sub
End Class
