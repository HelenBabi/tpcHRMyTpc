Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO

Partial Class sugEntryFrm
    Inherits System.Web.UI.Page

   
    Private Sub saveSug()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "sugInsPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@subject", SqlDbType.NVarChar, 150)
            cmm.Parameters("@subject").Value = subject.Text

            cmm.Parameters.Add("@sugType", SqlDbType.Int)
            cmm.Parameters("@sugType").Value = Val(sugType.SelectedValue)

            cmm.Parameters.Add("@sugIncome", SqlDbType.Money)
            cmm.Parameters("@sugIncome").Value = Val(sugIncome.Text)

            cmm.Parameters.Add("@realIncom", SqlDbType.Money)
            cmm.Parameters("@realIncom").Value = Val(realIncom.Text)

            cmm.Parameters.Add("@DescStatus", SqlDbType.NVarChar, 5000)
            cmm.Parameters("@DescStatus").Value = DescStatus.Text

            cmm.Parameters.Add("@sugMethod", SqlDbType.NVarChar, 5000)
            cmm.Parameters("@sugMethod").Value = sugMethod.Text

            cmm.Parameters.Add("@benefits", SqlDbType.Money)
            cmm.Parameters("@benefits").Value = Val(benefits.Text)

            cmm.Parameters.Add("@Description", SqlDbType.NVarChar, 5000)
            cmm.Parameters("@Description").Value = Description.Text

            cmm.Parameters.Add("@codePersoneli", SqlDbType.BigInt)
            cmm.Parameters("@codePersoneli").Value = Session("codePersoneli")

            cmm.Parameters.Add("@partners", SqlDbType.NVarChar, 500)
            cmm.Parameters("@partners").Value = partners.Text

            HiddenFieldSugID.Value = ClsGetMaxId.GetMaxId("sugTbl", "sugID")

            cmm.Parameters.Add("@sugID", SqlDbType.Int)
            cmm.Parameters("@sugID").Value = HiddenFieldSugID.Value

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                If FileUpload.HasFile = True Then
                    save_file()
                End If


                msg_box("  پیشنهاد شما با کد  " & HiddenFieldSugID.Value & " ثبت گردید ")
                subject.Text = ""
                sugIncome.Text = ""
                sugType.SelectedValue = 0
                sugIncome.Text = ""
                realIncom.Text = ""
                benefits.Text = ""
                DescStatus.Text = ""
                sugMethod.Text = ""
                Description.Text = ""
                partners.Text = ""

                HiddenFieldSugID.Value = 0
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try
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


            cmm.CommandText = "sugFileInsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = HiddenFieldSugID.Value


            cmm.Parameters.Add("@tartib", SqlDbType.Int)
            cmm.Parameters("@tartib").Value = 0

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

                'UpdatePanel1.Visible = True

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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            loadCombo()
        End If


    End Sub
    Private Sub loadCombo()
        Dim sqlstr As String = "SELECT        TOP (200) sugTypeID, sugTypeName, state " & _
        " FROM sugTypeTbl WHERE        (state = 1)"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        sugType.DataSource = dt
        sugType.DataTextField = "sugTypeName"
        sugType.DataValueField = "sugTypeID"
        sugType.DataBind()


    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        saveSug()
    End Sub
End Class
