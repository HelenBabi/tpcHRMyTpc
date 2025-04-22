Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Imports System.Globalization

Partial Class orderFiles
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            load_files()


        End If



    End Sub
    Protected Sub DataList1_ItemCommand(sender As Object, e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        If e.CommandName = "dell" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            'radif = Val(DataList1.Items(radif).ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete    OrderFiles  WHERE     idd =" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_files()

        End If


    End Sub

    Sub load_files()


        Dim sqlstr As String
        sqlstr = " SELECT     idd, FilesName, FilesMasir, date_s, time_s, OrderID, noeFile, noeFileCode " & _
"        FROM OrderFiles  WHERE     OrderID =" & Request.QueryString("PwhereSTR") & " order by noeFileCode"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)

        DataList1.DataSource = dt
        DataList1.DataBind()


    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrsWorksSabege.aspx", Val(Session("UserId")))

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

#End Region








    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        save_final()
    End Sub

    Private Sub save_final()
        If FileUpload1.HasFile = False Then
            msg_box("فایل انتخاب نشده است")
            FileUpload1.Focus()
            Exit Sub
        End If
        Dim OrderID As Integer = Request.QueryString("PwhereSTR")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcOrderFiles"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@noeFile", SqlDbType.NVarChar, 50)
            cmm.Parameters("@noeFile").Value = cmbfiletyp.SelectedItem.ToString

            cmm.Parameters.Add("@noeFileCode", SqlDbType.SmallInt)
            cmm.Parameters("@noeFileCode").Value = cmbfiletyp.SelectedValue
            cmm.Parameters.Add("@OrderID", SqlDbType.Int, 10)
            cmm.Parameters("@OrderID").Value = OrderID

            cmm.Parameters.Add("@FilesName", SqlDbType.NVarChar, 50)
            cmm.Parameters("@FilesName").Value = OrderID & "_" & cmbfiletyp.SelectedValue & "_" & FileUpload1.PostedFile.FileName

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            cmm.Parameters.Add("@FilesMasir", SqlDbType.VarChar, 150)
            If FileUpload1.HasFile Then
                cmm.Parameters("@FilesMasir").Value = "images/attach_files/Orders/" & OrderID & "_" & cmbfiletyp.SelectedValue & "_" & FileUpload1.PostedFile.FileName
            Else
                cmm.Parameters("@FilesMasir").Value = ""
            End If

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  ok")
                load_files()

                Try
                    If FileUpload1.HasFile Then

                        Dim masir As String = HttpContext.Current.Server.MapPath("~/images/attach_files/Orders/")
                        Dim filname As String = OrderID & "_" & cmbfiletyp.SelectedValue & "_" & FileUpload1.PostedFile.FileName
                        FileUpload1.PostedFile.SaveAs(masir & filname)
                    End If


                Catch ex As Exception

                End Try
            Else
                msg_box(cmm.Parameters("@pm").Value)

            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub

   
End Class
