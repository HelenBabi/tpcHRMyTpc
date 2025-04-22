Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Partial Class trnTechersDetailFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            load_cmbGender()
            load_cmbType()
            load_grid_FAKTOR()
            load_grid_Service()
            load_grid_Skil()
        End If

        loadDetail()
    End Sub

    Private Sub loadDetail()
        Dim dt As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "select * from trnTechersVW WHERE        (techID = " & Session("techID") & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            nam.Text = dt.Rows(0).Item("nam").ToString
            family.Text = dt.Rows(0).Item("family").ToString
            mahalEshtegal.Text = dt.Rows(0).Item("mahalEshtegal").ToString
            semat.Text = dt.Rows(0).Item("semat").ToString
            father.Text = dt.Rows(0).Item("father").ToString
            shsh.Text = dt.Rows(0).Item("shsh").ToString
            codemeli.Text = dt.Rows(0).Item("codemeli").ToString
            tarixStexdam.Text = dt.Rows(0).Item("tarixStexdam").ToString
            sabege.Text = dt.Rows(0).Item("sabege").ToString
            omur.Text = dt.Rows(0).Item("omur").ToString
            Unit.Text = dt.Rows(0).Item("Unit").ToString
            daxeli.Text = dt.Rows(0).Item("daxeli").ToString
            mobil.Text = dt.Rows(0).Item("mobil").ToString
            madrak.Text = dt.Rows(0).Item("madrak").ToString
            reshte.Text = dt.Rows(0).Item("reshte").ToString
            gerayesh.Text = dt.Rows(0).Item("gerayesh").ToString
            uni.Text = dt.Rows(0).Item("uni").ToString
            tarixTavalod.Text = dt.Rows(0).Item("tarixTavalod").ToString
            moadel.Text = dt.Rows(0).Item("moadel").ToString
            moaref.Text = dt.Rows(0).Item("moaref").ToString
            maharatha.Text = dt.Rows(0).Item("maharatha").ToString
            notee.Text = dt.Rows(0).Item("notee").ToString

            jens.SelectedValue = dt.Rows(0).Item("genderTypeID").ToString
            cmbTechType.SelectedValue = dt.Rows(0).Item("techTypeID").ToString
            sherkati.Text = dt.Rows(0).Item("textt").ToString
            codePersoneli.Text = dt.Rows(0).Item("codePersoneli").ToString
        End If
    End Sub

    Private Sub load_cmbType()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) techTypeID, techTypename FROM            trnTecherTypeTbl"
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbTechType.DataSource = dt
        cmbTechType.DataTextField = "techTypename"
        cmbTechType.DataValueField = "techTypeID"
        cmbTechType.DataBind()

    End Sub
    Private Sub load_cmbGender()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) idd, jens FROM            jensiat"
        dt = ClsDataTable.GetDataTable(sqlstr)
        jens.DataSource = dt
        jens.DataTextField = "jens"
        jens.DataValueField = "idd"
        jens.DataBind()

    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    
    
    Protected Sub btnSabtFile_Click(sender As Object, e As EventArgs) Handles btnSabtFile.Click
        'If cmbRunevesht.Value = 0 Then
        '    msg_box("لطفا بعد از مشخص نمودن گیرنده ارجاع فایل ضمیمه را اضافه نمایید")
        '    cmbRunevesht.Focus()
        '    Exit Sub
        'Else
        If FileUpload.HasFile = False Then
            msg_box("فایل انتخاب نشده است")
            Exit Sub
        Else
            save_file()




        End If

        'End If

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


            cmm.CommandText = "trnTechersFileInsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Session("techID")


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
            cmm.Parameters("@sharh").Value = txtFileNote.Text


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
                load_grid_FAKTOR()
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
    Private Sub load_grid_FAKTOR()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT       documentsTbl.docType, documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name, documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid, " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib,  " & _
                         "personel.nam + ' ' + personel.family AS karbar  " & _
                           " FROM            documentsTbl INNER JOIN  " & _
                         " personel ON documentsTbl.userid = personel.ID  " & _
        " WHERE(documentsTbl.fileID = " & Session("techID") & ") and documentsTbl.docType = 18 " & _
                       "ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

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

    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnTecherArzyabiPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@techID", SqlDbType.Int, 6)
            cmm.Parameters("@techID").Value = Session("techID")


            cmm.Parameters.Add("@dore", SqlDbType.NVarChar, 50)
            cmm.Parameters("@dore").Value = dore.Text
           
            cmm.Parameters.Add("@nomre", SqlDbType.Int, 5)
            cmm.Parameters("@nomre").Value = Val(nomre.Text)

            cmm.Parameters.Add("@userID", SqlDbType.Int, 5)
            cmm.Parameters("@userID").Value = Session("userid")

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
        ' GridView2.Visible = True
        Dim sqlstr As String = ""
        sqlstr = "SELECT       * from trnTechersDtlTbl WHERE techID = " & Session("techID") & " "


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

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("trnBrowsTechersFrm.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("trnBrowsTechersFrm.aspx")
    End Sub
 

    Private Sub load_grid_Skil()
        ' GridView2.Visible = True
        Dim sqlstr As String = ""
        sqlstr = "SELECT       * from trnTechersSkilTbl WHERE techID = " & Session("techID") & " "


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
            'Else
            'GridView2.Visible = False

        End If


    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "trnTecherSkilPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@techID", SqlDbType.Int, 6)
            cmm.Parameters("@techID").Value = Session("techID")


            cmm.Parameters.Add("@dore", SqlDbType.NVarChar, 50)
            cmm.Parameters("@dore").Value = TextBox1.Text

            cmm.Parameters.Add("@nomre", SqlDbType.Int, 5)
            cmm.Parameters("@nomre").Value = Val(TextBox2.Text)

            cmm.Parameters.Add("@userID", SqlDbType.Int, 5)
            cmm.Parameters("@userID").Value = Session("userid")

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
                load_grid_Skil()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try
    End Sub

End Class
