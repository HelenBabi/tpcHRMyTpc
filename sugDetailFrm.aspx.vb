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

Partial Class sugDetailFrm
    Inherits System.Web.UI.Page
    Dim dtSistem As New DataTable
    Dim az As String = ClsDateTime.GetDateNow
    Dim ta As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String

    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sugID") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
        loadItems()
        'load_gridGirandeFerestande()
        loadSendReciverDetail()
        loadComboTyp()
        upReadDateTime()

        'If Me.IsPostBack = False Then
        '    load_cmbUsers()
        '    load_cmbRuneveshtUsers()
        'End If
    End Sub
    Private Sub loadComboTyp()
        Dim sqlstr As String = "SELECT        TOP (200) sugTypeID, sugTypeName, state " & _
        " FROM sugTypeTbl WHERE        (state = 1)"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        sugType.DataSource = dt
        sugType.DataTextField = "sugTypeName"
        sugType.DataValueField = "sugTypeID"
        sugType.DataBind()


    End Sub
    Private Sub load_cmbRuneveshtUsers()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        UserFamili + ' ' + UserLastName +  ' - ' + semat AS karbar, UserCode " & _
        "FROM tblUsers ORDER BY UserFamili"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        cmbRunevesht.DataSource = dtm
        cmbRunevesht.TextField = "karbar"
        cmbRunevesht.ValueField = "UserCode"
        cmbRunevesht.DataBind()

    End Sub
 
    Private Sub loadSendReciverDetail()
        Dim dt As DataTable
        Dim sqlstrr As String = ""
        If Session("fromTyp") = "daryafti" Then
            btnPayanGardesh.Visible = True
            'sqlstrr = "select  * from laterReciverTbl WHERE        (id = " & Val(HdnId.Value) & ") AND (reciverID = " & Session("userID") & ") AND   (erja = 1)"

        End If
        'sqlstrr = "select  * from laterReciverTbl WHERE        (laterID = " & Val(Val(HdnId.Value)) & ") AND  (erja = 1)"
        'dt = ClsDataTable.GetDataTable(sqlstrr)
        'If dt.Rows.Count > 0 Then

        '    btnErajaat.Visible = True
        '    txtMatnErjaa.Visible = True
        '    lblMatnErja.Visible = True
        '    txtMatnErjaa.Text = dt.Rows(0).Item("matnErja")

        'End If

    End Sub
    Private Sub loadItems()
        Dim dt As DataTable
        HdnId.Value = Session("sugID")
        Dim sqlstrr As String = "SELECT *  FROM   sugGrideUserVW WHERE        (sugID = " & HdnId.Value & ")"
        dt = ClsDataTable.GetDataTable(sqlstrr)
        If dt.Rows.Count > 0 Then

            subject.Text = dt.Rows(0).Item("subject")
            sugType.SelectedValue = dt.Rows(0).Item("sugType")
            sugIncome.Text = dt.Rows(0).Item("sugIncome")
            realIncom.Text = dt.Rows(0).Item("realIncom")
            DescStatus.Text = dt.Rows(0).Item("DescStatus")
            sugMethod.Text = dt.Rows(0).Item("sugMethod")
            benefits.Text = dt.Rows(0).Item("benefits")
            Description.Text = dt.Rows(0).Item("Description")

        End If

    End Sub
    Private Sub load_gridGirandeFerestande()
        Dim dtSi As DataTable

        Dim sqlstrr As String = "select  * from laterRuneveshtGridVW WHERE        (id = " & Val(HdnId.Value) & ") "
        dtSi = ClsDataTable.GetDataTable(sqlstrr)

        GridGirande.DataSource = dtSi
        GridGirande.DataBind()

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

    Private Sub load_grid_FAKTOR()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT     documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name,  documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid,  " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib, " & _
                         "tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar " & _
"FROM            documentsTbl INNER JOIN " & _
                        " tblUsers ON documentsTbl.userid = tblUsers.UserCode " & _
       " WHERE(documentsTbl.fileID = " & Val(Val(HdnId.Value)) & ") " & _
"ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        ASPxGrvHead0.DataSource = dtan
        ASPxGrvHead0.DataBind()

    End Sub









    Protected Sub imgKartabl_Click(sender As Object, e As EventArgs) Handles imgKartabl.Click
        HdnId.Value = 0
        Response.Redirect("index.aspx")
    End Sub

    Private Sub printt()
        Dim crystalReport As New ReportDocument()

        Dim sqlstrHamesh As String = " SELECT   * from laterHameshVW WHERE        (erja_id  = " & Val(Val(HdnId.Value)) & ") "
        Dim dth As New DataTable
        dth = ClsDataTable.GetDataTable(sqlstrHamesh)

        Dim sqlstrRunevesht As String = " SELECT   * from laterRuneveshtReportVW WHERE        (laterID  = " & Val(Val(HdnId.Value)) & ") "
        Dim dtr As New DataTable
        dtr = ClsDataTable.GetDataTable(sqlstrRunevesht)

        Dim sqlstr As String = " SELECT   * from laterPrintVW WHERE        (laterID  = " & Val(Val(HdnId.Value)) & ") "
        Dim dt As New DataTable

        dt = ClsDataTable.GetDataTable(sqlstr)

        Dim jens As String = "جناب آقای"
        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("genderr").ToString = 1 Then
                jens = "سرکارخانم"

            End If


            crystalReport.Load(Server.MapPath("~/reports/laterA4.rpt"))


            Dim girande As String = dt.Rows(0).Item("girande").ToString
            Dim ferestande As String = dt.Rows(0).Item("ferestande").ToString
            Dim mozu As String = " موضوع: " & dt.Rows(0).Item("mozu").ToString
            Dim matn As String = " احتراما " & dt.Rows(0).Item("matn").ToString
            Dim postGirande As String = dt.Rows(0).Item("postGirande").ToString
            Dim postferestande As String = dt.Rows(0).Item("postferestande").ToString
            Dim laterDate As String = dt.Rows(0).Item("laterDate").ToString
            Dim laterID As String = dt.Rows(0).Item("laterID").ToString
            crystalReport.SetParameterValue("jens", jens)


            crystalReport.SetParameterValue("axxMasir", Server.MapPath("~/images/emza/" & dt.Rows(0).Item("Codepersonel").ToString & ".jpg"))


            crystalReport.SetParameterValue("girande", girande)
            crystalReport.SetParameterValue("ferestande", ferestande)

            crystalReport.SetParameterValue("mozu", mozu)
            crystalReport.SetParameterValue("matn", matn)

            crystalReport.SetParameterValue("postGirande", postGirande)
            crystalReport.SetParameterValue("postferestande", postferestande)

            crystalReport.SetParameterValue("laterDate", laterDate)
            crystalReport.SetParameterValue("laterID", laterID)

            Dim runevesht As String = ""
            'If dtr.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtr.Rows.Count - 1
            '        runevesht = runevesht + " " + dtr.Rows(i).Item("runevesht").ToString
            '    Next i
            'End If
            crystalReport.SetParameterValue("runevesht", runevesht)


            Dim hamesh As String = ""
            'If dth.Rows.Count > 0 Then
            '    For i As Integer = 0 To dth.Rows.Count - 1
            '        hamesh = hamesh + " " + dth.Rows(i).Item("hamesh").ToString
            '    Next i
            'End If
            crystalReport.SetParameterValue("hamesh", hamesh)

            Dim formatType As ExportFormatType = ExportFormatType.NoFormat


            formatType = ExportFormatType.WordForWindows
            crystalReport.ExportToHttpResponse(formatType, Response, True, "Crystal")

            Response.End()

        End If
        'crystalReport.SetDataSource(dt)
    End Sub


    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        'upMatn()
        printt()
    End Sub
    Private Sub payanGradesh()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterPayanGardeshPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@id", SqlDbType.Int)
            cmm.Parameters("@id").Value = Val(HdnId.Value)

            cmm.Parameters.Add("@userID", SqlDbType.Int)
            cmm.Parameters("@userID").Value = Session("userID")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" پایان گردش انجام شد و نامه مربوطه از کارتابل جاری به کارتابل نامه های دریافتی انتقال یافت ")
                '  Call save_()
                btnPayanGardesh.Visible = False
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Protected Sub btnPuyesh_Click(sender As Object, e As EventArgs) Handles btnPuyesh.Click
        'Timer1.Enabled = True
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterScanPrc"
            cmm.CommandType = CommandType.StoredProcedure


            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Val(Val(HdnId.Value))

            cmm.Parameters.Add("@ip", SqlDbType.VarChar, 50)
            cmm.Parameters("@ip").Value = Request.UserHostAddress

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

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub


    'Protected Sub lnkDel_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
    '    Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
    '    Dim fileid As Integer = Convert.ToInt32(ASPxGrvHead0.DataKeys(gvrow.RowIndex).Value.ToString())
    '    Dim cnn As New SqlConnection

    '    '  ins_temp(3, radif)
    '    Dim cmm As New SqlCommand
    '    cmm.CommandText = "delete        legalDocuments       WHERE id =" & fileid & ""
    '    Dim constr As String = ClsConnection.GetConn
    '    cnn.ConnectionString = constr
    '    cmm.Connection = cnn

    '    cnn.Open()
    '    cmm.ExecuteNonQuery()

    '    cnn.Close()
    '    load_grid_FAKTOR()
    'End Sub
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


            cmm.CommandText = "knowlegNewFilePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Val(Val(HdnId.Value))


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
            cmm.Parameters("@sharh").Value = ""


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

    Protected Sub btnSabtFile_Click(sender As Object, e As EventArgs) Handles btnSabtFile.Click
        If cmbRunevesht.Value = 0 Then
            msg_box("لطفا بعد از مشخص نمودن گیرنده ارجاع فایل ضمیمه را اضافه نمایید")
            cmbRunevesht.Focus()
            Exit Sub
        Else
            If FileUpload.HasFile = False Then
                msg_box("فایل انتخاب نشده است")
                Exit Sub
            Else
                save_file()


                load_grid_FAKTOR()

            End If

        End If


    End Sub






    Private Sub upReadDateTime()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterUpReadTimePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@id", SqlDbType.Int)
            cmm.Parameters("@id").Value = Val(HdnId.Value)

            cmm.Parameters.Add("@userID", SqlDbType.Int)
            cmm.Parameters("@userID").Value = Session("userID")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                'msg_box(" نامه باموفقیت ارسال شد ")
                '  Call save_()

                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Private Sub save_tmp(ByVal INOUT As Boolean, ByVal IsDel As Boolean)



        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterErjatempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Val(Val(HdnId.Value))

            cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            cmm.Parameters("@sender").Value = Session("UserId")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  عملیات با موفقیت انجام شد ")
                '  Call save_()
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub load_grid_tmp()
        Dim sqlstr As String = " SELECT   * from laterRuneveshtTempGridVW where tempID = " & hdn_tmp.Value & ""
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub
    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridView2.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from laterRuneveshtTempTbl where idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_tmp()
        End If
    End Sub

    Protected Sub btnSaveFinal_Click(sender As Object, e As EventArgs) Handles btnSaveFinal.Click
        If cmbRunevesht.Value = 0 Then
            cmbRunevesht.Focus()
            msg_box("لطفا گیرنده نامه را انتخاب نمایید")

            Exit Sub
        Else

            If Val(hdn_tmp.Value) = 0 Then
                save_Erja2()

            Else
                save_Erja()
            End If
        End If
    End Sub
    Private Sub save_Erja2()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterSaveErja2Prc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Val(Val(HdnId.Value))

            cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            cmm.Parameters("@sender").Value = Session("UserId")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" نامه باموفقیت ارجاع داده شد ")
                '  Call save_()
                btnSaveFinal.Visible = False
                PanelRunevest.Enabled = False
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub save_Erja()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "laterSaveErjaPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Val(Val(HdnId.Value))

            'cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            'cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.Value)

            'cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            'cmm.Parameters("@sender").Value = Session("UserId")

            'cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            'cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@tempID", SqlDbType.Int, 5)
            cmm.Parameters("@tempID").Value = Val(hdn_tmp.Value)

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box(" نامه باموفقیت ارجاع داده شد ")
                '  Call save_()
                btnSaveFinal.Visible = False
                PanelRunevest.Enabled = False
                ' load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Protected Sub btnErajaat_Click(sender As Object, e As EventArgs) Handles btnErajaat.Click
        Dim furl As String = "laterErajjatFrm.aspx?PwhereSTR=" & Val(Val(HdnId.Value)) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=500,width=1000,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Dim furl As String = "laterHistoryFrm.aspx?PwhereSTR=" & Val(Val(HdnId.Value)) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=500,width=1000,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub btnPayanGardesh_Click(sender As Object, e As EventArgs) Handles btnPayanGardesh.Click
        payanGradesh()
    End Sub

    Protected Sub CheckBoxAttach_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAttach.CheckedChanged
        If CheckBoxAttach.Checked = True Then
            'UpdatePanel1.Visible = True
            load_grid_FAKTOR()


            'btnSaveFinal.Visible = True
        Else
            'UpdatePanel1.Visible = False
            'btnPuyesh.Visible = False

        End If
    End Sub

    Protected Sub CheckBoxRunevesht_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRunevesht.CheckedChanged
        If CheckBoxRunevesht.Checked = True Then
            PanelRunevest.Visible = True

            load_cmbRuneveshtUsers()

            PanelAttach.Visible = True
            btnPuyesh.Visible = True


        Else
            PanelRunevest.Visible = False
            PanelAttach.Visible = False
            btnPuyesh.Visible = False

        End If
    End Sub

    Protected Sub btnRunevesht_Click(sender As Object, e As EventArgs) Handles btnRunevesht.Click
        If cmbRunevesht.Value = 0 Then
            cmbRunevesht.Focus()
            msg_box("لطفا گیرنده نامه را انتخاب نمایید")

            Exit Sub
        Else
            If Val(hdn_tmp.Value) = 0 Then
                hdn_tmp.Value = ClsGetMaxId.GetMaxId("laterRuneveshtTempTbl", "tempID")
            End If
            save_tmp(True, False)
            GridView2.Visible = True
            load_grid_tmp()
            btnSaveFinal.Visible = True
        End If

    End Sub
End Class

