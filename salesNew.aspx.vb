Imports System.Data
Imports System.Data.SqlClient
Partial Class salesNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
     
        load_grid_tmp()
    End Sub

   


    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        save_final()
     


    End Sub
    Private Sub save_final()

        If FileUpload1.HasFile = False Then
            msg_box("عکس انتخاب نشده است")
            FileUpload1.Focus()
            Exit Sub
        End If
        Dim idd As Integer = ClsGetMaxId.GetMaxId("tblMainPost", "idd")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand


            cmm.CommandText = "BazarNewPostPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@idd", SqlDbType.Int, 5)
            cmm.Parameters("@idd").Value = idd



            cmm.Parameters.Add("@mobilShow", SqlDbType.Bit)
            cmm.Parameters("@mobilShow").Value = CheckBox1.Checked

            cmm.Parameters.Add("@codeuser", SqlDbType.Int, 5)
            cmm.Parameters("@codeuser").Value = Session("userid")

            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 20)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress

            cmm.Parameters.Add("@compName", SqlDbType.VarChar, 20)
            cmm.Parameters("@compName").Value = Request.UserHostName

            cmm.Parameters.Add("@subject", SqlDbType.NVarChar, 150)
            cmm.Parameters("@subject").Value = onvan.Text

            cmm.Parameters.Add("@groupKala", SqlDbType.Int, 5)
            cmm.Parameters("@groupKala").Value = DropDownList1.SelectedValue

            cmm.Parameters.Add("@filname", SqlDbType.NVarChar, 50)
            cmm.Parameters("@filname").Value = idd & ".jpg"


            cmm.Parameters.Add("@file_url", SqlDbType.VarChar, 150)
            If FileUpload1.HasFile Then
                cmm.Parameters("@file_url").Value = "images/bazar/" & idd & ".jpg"
            Else
                cmm.Parameters("@file_url").Value = ""
            End If


            cmm.Parameters.Add("@tozihat", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@tozihat").Value = tozih.Text

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  عملیات با موفقیت انجام شد ")
                load_grid_tmp()

                onvan.Text = ""
                DropDownList1.SelectedValue = 0
                tozih.Text = ""



                Try
                    If FileUpload1.HasFile Then

                        Dim masir As String = HttpContext.Current.Server.MapPath("~/images/bazar/")
                        Dim filname As String = idd & ".jpg"
                        FileUpload1.PostedFile.SaveAs(masir & filname)
                    End If


                Catch ex As Exception

                End Try
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Private Sub load_grid_tmp()
        Dim sqlstr As String = "SELECT        idd, codeuser, mozu, groupKala, file_url, filname, tozihat, saat, tarix, tarixengeza, ipUser, stat, ipp, compName, tarixebtal, saatebtal " & _
        " FROM tblMainPost       WHERE(codeuser = " & Session("userid") & ")  AND (stat = 1) "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

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
    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "dell" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "UPDATE       tblMainPost      set  stat = 0   WHERE idd =" & radif & ""
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
    End Sub




End Class
