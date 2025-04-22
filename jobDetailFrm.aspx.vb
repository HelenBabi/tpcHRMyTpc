Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO
Partial Class jobDetailFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        loadItems()
        notes()
        load_grid_FAKTOR()
    End Sub
    Private Sub loadItems()

        Dim dt As New DataTable
        Dim sqlstr As String = "select * from knowlegmanagmentTbl WHERE        (kmID = " & Session("knowlegID") & ") "
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then

            kmIDTxt.Text = dt.Rows(0).Item("kmID").ToString
            subject.Text = dt.Rows(0).Item("subject").ToString
            tozihatTxt.Text = dt.Rows(0).Item("note").ToString
            darsadPishTxt.Text = dt.Rows(0).Item("darsad").ToString
        End If
    End Sub
    Sub notes()
        Dim dt2 As New DataTable
        Dim sqlstr As String = "SELECT        notee, headd,REPLACE([pic ], ' ', '') AS picc, kmID, userType " & _
       " FROM jobCommentVW        WHERE(kmID = " & Val(kmIDTxt.Text) & ")"

        dt2 = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt2
        DataList1.DataBind()


    End Sub
    Private Sub load_grid_FAKTOR()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT        documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name, documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid,  " & _
                        " documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib,  " & _
                        " personel.nam + ' ' + personel.family AS karbar " & _
             "FROM            documentsTbl INNER JOIN " & _
                         " personel ON documentsTbl.userid = personel.ID " & _
       " WHERE(documentsTbl.fileID = " & Val(kmIDTxt.Text) & ") AND (docType = 14)  ORDER BY documentsTbl.tartib"

    
        '"ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

    End Sub

    Protected Sub btnNazar_Click(sender As Object, e As EventArgs) Handles btnNazar.Click


   

        If Val(darsadPishTxt.Text) + Val(darsadTxt.Text) > 100 Then
            msg_box("جمع درصد پیشرفت نمی تواند بزرگتر از صد باشد")
            darsadTxt.Focus()

            Exit Sub
        Else
            save_final()
            notes()
            txtNazar.Text = ""
            darsadTxt.Text = ""
        End If



    End Sub
    Private Sub save_final()


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "jobCommentsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@kmID", SqlDbType.Int, 10)
            cmm.Parameters("@kmID").Value = Val(kmIDTxt.Text)

            cmm.Parameters.Add("@notee ", SqlDbType.NVarChar)
            cmm.Parameters("@notee ").Value = txtNazar.Text


            cmm.Parameters.Add("@darsad ", SqlDbType.Int, 5)
            cmm.Parameters("@darsad ").Value = Val(darsadTxt.Text)

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Int16
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                'msg_box("  ثبت  شد ")
                loadItems()

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub


    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
        Session("knowlegID") = 0
        If Session("from") = "mySent" Then
            Response.Redirect("jobMySentBrowseFrm.aspx")
        End If
        If Session("from") = "myJob" Then
            Response.Redirect("jobManagementBrowseFrm.aspx")
        End If

        If Session("from") = "myJobGroup" Then
            Response.Redirect("jobManagementGroupBrowseFrm.aspx")
        End If





    End Sub

    Protected Sub imgKartabl_Click(sender As Object, e As EventArgs) Handles imgKartabl.Click
       
            Response.Redirect("index.aspx")
    
    End Sub

    Protected Sub btnSabtFile_Click(sender As Object, e As EventArgs) Handles btnSabtFile.Click
        If FileUpload.HasFile = False Then
            msg_box("فایل انتخاب نشده است")
            Exit Sub
        Else
            save_file()

            tartib.Text = ""
            sharhFile.Text = ""
            load_grid_FAKTOR()

        End If
    End Sub
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub
    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())

        Dim sqlstrd As String
        Dim dtd As New DataTable
        sqlstrd = "select  id, content_name, content_file  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)
        If dtd.Rows.Count > 0 Then
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("content_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("content_file"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این پرونده فایل ضمیمه نشده است")
        End If
        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()



    End Sub


    Protected Sub lnkDel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())
        Dim cnn As New SqlConnection

        '  ins_temp(3, radif)
        Dim cmm As New SqlCommand
        cmm.CommandText = "delete        legalDocuments       WHERE id =" & fileid & ""
        Dim constr As String = ClsConnection.GetConn
        cnn.ConnectionString = constr
        cmm.Connection = cnn

        cnn.Open()
        cmm.ExecuteNonQuery()

        cnn.Close()
        load_grid_FAKTOR()
    End Sub
    Private Sub save_file()

        Dim size As Integer = FileUpload.PostedFile.ContentLength
        size = size / 1024
        'This is where I'd like to perform the file size check 


        Dim filePath As String = FileUpload.PostedFile.FileName

        Dim filename As String = Path.GetFileName(filePath)

        Dim ext As String = Path.GetExtension(filename)


        Dim fs As Stream = FileUpload.PostedFile.InputStream

        Dim br As New BinaryReader(fs)

        Dim bytes As Byte() = br.ReadBytes(fs.Length)

        'insert the file into database


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand


            cmm.CommandText = "jobFileInsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Val(kmIDTxt.Text)


            cmm.Parameters.Add("@tartib", SqlDbType.Int)
            cmm.Parameters("@tartib").Value = Val(tartib.Text)

            cmm.Parameters.Add("@codeuser", SqlDbType.Int, 5)
            cmm.Parameters("@codeuser").Value = Session("userid")

            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 20)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress

            cmm.Parameters.Add("@compName", SqlDbType.VarChar, 20)
            cmm.Parameters("@compName").Value = 0

            cmm.Parameters.Add("@size", SqlDbType.Int)
            cmm.Parameters("@size").Value = size

            cmm.Parameters.Add("@Name", SqlDbType.NVarChar, 150)
            cmm.Parameters("@Name").Value = filename

            cmm.Parameters.Add("@sharh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sharh").Value = sharhFile.Text


            cmm.Parameters.Add("@Data", SqlDbType.Binary)
            cmm.Parameters("@Data").Value = bytes

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  فایل ثبت شد ")
                'BindGridviewData()

                Try

                Catch ex As Exception
                    msg_box(ex.Message.ToString)
                End Try
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try



    End Sub
End Class
