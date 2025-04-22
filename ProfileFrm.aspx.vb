Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.IO
Partial Class ProfileFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            load_Sazman()
            load_cmbShogl()
            loadPerInfo()
            load_Vahed()
            load_Units()
            load_grid_File()

        End If

    End Sub
    Private Sub load_Sazman()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM   sazman order by sazmanName"
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbSazman.DataSource = dt
        cmbSazman.ValueField = "id"
        cmbSazman.TextField = "sazmanName"
        cmbSazman.DataBind()

    End Sub
    Private Sub load_cmbShogl()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM   mashaghel order by postname"
        dt = ClsDataTable.GetDataTable(sqlstr)
        cmbShoghl.DataSource = dt
        cmbShoghl.ValueField = "id"
        cmbShoghl.TextField = "postname"
        cmbShoghl.DataBind()

    End Sub
    Private Sub loadPerInfo()

        Dim ClsB As New ClsPersonal
        Dim dt As New DataTable
        dt = ClsB.GetBaseDomainUser(Session("userid"), 0)
        If dt.Rows.Count > 0 Then
            loadSavabeg()

            CmbUnit.Value = dt.Rows(0).Item("UnitsCode").ToString


            codeMelli.Text = dt.Rows(0).Item("codeMelli").ToString
            father.Text = dt.Rows(0).Item("father").ToString
            shsh.Text = dt.Rows(0).Item("shsh").ToString
            mahalesodur.Text = dt.Rows(0).Item("mahalesodur").ToString
            shserial.Text = dt.Rows(0).Item("shserial").ToString
            vaziatsh.SelectedValue = dt.Rows(0).Item("vaziatsh").ToString
            mahaletavalod.Text = dt.Rows(0).Item("mahaletavalod").ToString
            tarixtavalod.Text = dt.Rows(0).Item("tarixtavalod").ToString

            vaziatathol.SelectedValue = dt.Rows(0).Item("vaziattahol").ToString
            teldaxele.Text = dt.Rows(0).Item("teldaxeli").ToString
            telmanzel.Text = dt.Rows(0).Item("telmanzel").ToString
            mobile.Text = dt.Rows(0).Item("mobile").ToString

            madrak.Text = dt.Rows(0).Item("madrak").ToString
            shgozarname.Text = dt.Rows(0).Item("shgozarname").ToString
            adres.Text = dt.Rows(0).Item("adres").ToString
            CmbVahed.Value = dt.Rows(0).Item("omurID").ToString

            shHesab.Text = dt.Rows(0).Item("shHesab").ToString
            adres.Text = dt.Rows(0).Item("adres").ToString
            shBime.Text = dt.Rows(0).Item("codebime").ToString
            codePosti.Text = dt.Rows(0).Item("codePosti").ToString

            reshte.Text = dt.Rows(0).Item("reshte").ToString

            tarixEzdevaj.Text = dt.Rows(0).Item("tarixEzdevaj").ToString
            sabegeNezam.Text = dt.Rows(0).Item("sabegeNezam").ToString
            tarixSodur.Text = dt.Rows(0).Item("tarixSodur").ToString

            daneshgah.Text = dt.Rows(0).Item("daneshgah").ToString
            moadel.Text = dt.Rows(0).Item("moadel").ToString
            tarixMadrak.Text = dt.Rows(0).Item("tarixMadrak").ToString
            shahrTahsil.Text = dt.Rows(0).Item("shahrTahsil").ToString
            esargariTyp.SelectedValue = dt.Rows(0).Item("esargariTyp").ToString
            nesbatEsargari.SelectedValue = dt.Rows(0).Item("nesbatEsargari").ToString
            tarixSarbazi.Text = dt.Rows(0).Item("tarixSarbazi").ToString


            moafitType.SelectedItem.Text = dt.Rows(0).Item("moafitType").ToString

            nesbatRadio.SelectedItem.Text = dt.Rows(0).Item("sanatNaft").ToString
            nesbatCmb.SelectedValue = dt.Rows(0).Item("nesbatNaft").ToString
            persenliTxt.Text = dt.Rows(0).Item("codePersonNaft").ToString
            xedmat.SelectedItem.Text = dt.Rows(0).Item("xedmatType").ToString
            tarixSarbaziShuru.Text = dt.Rows(0).Item("tarixSarbaziShuru").ToString
            shMosalsalsh.Text = dt.Rows(0).Item("shMosalsalsh").ToString
            cmbRuzShabKar.SelectedValue = dt.Rows(0).Item("setadiAmaliati").ToString
        End If

    End Sub
    Private Sub loadSavabeg()
        Dim dt As New DataTable

        Dim sqlstr As String = " select * from savabegVW where perID =" & Session("userid") & ""
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            GridViewSavabeg.DataSource = dt
            GridViewSavabeg.DataBind()
        End If


    End Sub
    'Protected Sub GridViewSavabeg_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewSavabeg.RowCommand
    '    If e.CommandName = "del" Then
    '        Dim cnn As New SqlConnection
    '        Dim radif As Int64 = e.CommandArgument
    '        radif = Val(GridViewSavabeg.Rows(radif).Cells(0).Text.ToString)
    '        '  ins_temp(3, radif)
    '        Dim cmm As New SqlCommand
    '        cmm.CommandText = "delete from kalaPersonel where   idDtl=" & radif
    '        Dim constr As String = ClsConnection.GetConn
    '        cnn.ConnectionString = constr
    '        cmm.Connection = cnn
    '        cnn.Open()
    '        cmm.ExecuteNonQuery()
    '        cnn.Close()
    '        loadSavabeg()
    '        'load_grid_Service()
    '    End If
    'End Sub

    Protected Sub lnkDel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(GridViewSavabeg.DataKeys(gvrow.RowIndex).Value.ToString())
        'Dim radif As Int64 = 0
        'radif = Val(GridViewSavabeg.Rows(radif).Cells(0).Text.ToString)
        Dim cnn As New SqlConnection

        '  ins_temp(3, radif)
        Dim cmm As New SqlCommand
        cmm.CommandText = "delete        savabeg       WHERE id =" & fileid & ""
        Dim constr As String = ClsConnection.GetConn
        cnn.ConnectionString = constr
        cmm.Connection = cnn

        cnn.Open()
        cmm.ExecuteNonQuery()

        cnn.Close()
        loadSavabeg()
    End Sub
    Private Sub save_final()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            'cmm.CommandText = "serviceInsPrc"
            cmm.CommandText = "profilePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@Id", SqlDbType.Int)
            cmm.Parameters("@Id").Value = Session("userid")

            cmm.Parameters.Add("@UnitsCode", SqlDbType.Int, 6)
            cmm.Parameters("@UnitsCode").Value = CmbUnit.Value

            cmm.Parameters.Add("@omurID", SqlDbType.Int, 6)
            cmm.Parameters("@omurID").Value = CmbVahed.Value

            cmm.Parameters.Add("@codeMelli", SqlDbType.VarChar, 50)
            cmm.Parameters("@codeMelli").Value = codeMelli.Text

            cmm.Parameters.Add("@shgozarname", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shgozarname").Value = shgozarname.Text

            cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
            cmm.Parameters("@madrak").Value = madrak.Text

            cmm.Parameters.Add("@mobile", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mobile").Value = mobile.Text

            cmm.Parameters.Add("@telmanzel", SqlDbType.NVarChar, 50)
            cmm.Parameters("@telmanzel").Value = telmanzel.Text

            cmm.Parameters.Add("@teldaxeli", SqlDbType.NVarChar, 50)
            cmm.Parameters("@teldaxeli").Value = teldaxele.Text

            cmm.Parameters.Add("@vaziattahol", SqlDbType.NVarChar, 50)
            cmm.Parameters("@vaziattahol").Value = vaziatathol.SelectedValue

            cmm.Parameters.Add("@adres", SqlDbType.NVarChar, 500)
            cmm.Parameters("@adres").Value = adres.Text

            cmm.Parameters.Add("@tarixtavalod", SqlDbType.VarChar, 50)
            cmm.Parameters("@tarixtavalod").Value = tarixtavalod.Text

            cmm.Parameters.Add("@mahaletavalod", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mahaletavalod").Value = mahaletavalod.Text

            cmm.Parameters.Add("@vaziatsh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@vaziatsh").Value = vaziatsh.SelectedValue

            cmm.Parameters.Add("@shserial", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shserial").Value = shserial.Text

            cmm.Parameters.Add("@mahalesodur", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mahalesodur").Value = mahalesodur.Text

            cmm.Parameters.Add("@father", SqlDbType.NVarChar, 50)
            cmm.Parameters("@father").Value = father.Text

            cmm.Parameters.Add("@shsh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shsh").Value = shsh.Text

            cmm.Parameters.Add("@shHesab", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shHesab").Value = shHesab.Text

            cmm.Parameters.Add("@codebime", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codebime").Value = shBime.Text

            cmm.Parameters.Add("@codePosti", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codePosti").Value = codePosti.Text

            cmm.Parameters.Add("@reshte", SqlDbType.NVarChar, 50)
            cmm.Parameters("@reshte").Value = reshte.Text

            cmm.Parameters.Add("@tarixEzdevaj", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixEzdevaj").Value = tarixEzdevaj.Text

            cmm.Parameters.Add("@tarixSodur", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixSodur").Value = tarixSodur.Text

            cmm.Parameters.Add("@sabegeNezam", SqlDbType.NVarChar, 500)
            cmm.Parameters("@sabegeNezam").Value = sabegeNezam.Text

            cmm.Parameters.Add("@daneshgah", SqlDbType.NVarChar, 50)
            cmm.Parameters("@daneshgah").Value = daneshgah.Text

            cmm.Parameters.Add("@moadel", SqlDbType.NVarChar, 50)
            cmm.Parameters("@moadel").Value = moadel.Text

            cmm.Parameters.Add("@tarixMadrak", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixMadrak").Value = tarixMadrak.Text

            cmm.Parameters.Add("@shahrTahsil", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shahrTahsil").Value = shahrTahsil.Text

            cmm.Parameters.Add("@esargariTyp", SqlDbType.NVarChar, 50)
            cmm.Parameters("@esargariTyp").Value = esargariTyp.SelectedValue

            cmm.Parameters.Add("@nesbatEsargari", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nesbatEsargari").Value = nesbatEsargari.SelectedValue

            cmm.Parameters.Add("@tarixSarbazi", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixSarbazi").Value = tarixSarbazi.Text



            cmm.Parameters.Add("@moafitType", SqlDbType.NVarChar, 50)
            cmm.Parameters("@moafitType").Value = moafitType.SelectedItem.Text

            cmm.Parameters.Add("@sanatNaft", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sanatNaft").Value = nesbatRadio.SelectedItem.Text

            cmm.Parameters.Add("@nesbatNaft", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nesbatNaft").Value = nesbatCmb.SelectedValue

            cmm.Parameters.Add("@codePersonNaft", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codePersonNaft").Value = persenliTxt.Text

            cmm.Parameters.Add("@xedmatType", SqlDbType.NVarChar, 50)
            cmm.Parameters("@xedmatType").Value = xedmat.SelectedItem.Text


            cmm.Parameters.Add("@tarixSarbaziShuru", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tarixSarbaziShuru").Value = tarixSarbaziShuru.Text

            cmm.Parameters.Add("@shMosalsalsh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shMosalsalsh").Value = shMosalsalsh.Text

            cmm.Parameters.Add("@setadiAmaliati", SqlDbType.Int, 5)
            cmm.Parameters("@setadiAmaliati").Value = cmbRuzShabKar.SelectedValue


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
                msg_box(" اطلاعات ذخیره شد ")
                loadPerInfo()

                'btnBrwsSys.Visible = False
                'If hdn_Final_key.Value = "edit" Then
                '    btnBrwsSys.Visible = True
                'End If
                'btnBack.Visible = True



            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub
    Private Sub save_file()


        Dim masir As String = HttpContext.Current.Server.MapPath("~/images/attach_files/")



        Dim filname As String = Session("codePersoneli") & "_" & FileUpload.PostedFile.FileName
        FileUpload.PostedFile.SaveAs(masir & filname)


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


            cmm.CommandText = "profilePersonelFileInsPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@fileID", SqlDbType.Int, 5)
            cmm.Parameters("@fileID").Value = Session("userid")


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

            cmm.Parameters.Add("@sharh", SqlDbType.NVarChar, 500)
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
                load_grid_File()
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
    Private Sub load_grid_File()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT       documentsTbl.docType, documentsTbl.sharh, documentsTbl.id, documentsTbl.content_name, documentsTbl.date, documentsTbl.time, documentsTbl.size, documentsTbl.userid, " & _
                         "documentsTbl.ip, documentsTbl.computernam, documentsTbl.mdate, documentsTbl.fileID, documentsTbl.tartib,  " & _
                         "personel.nam + ' ' + personel.family AS karbar  " & _
                           " FROM            documentsTbl INNER JOIN  " & _
                         " personel ON documentsTbl.userid = personel.ID  " & _
        " WHERE(documentsTbl.fileID = " & Session("userid") & ") and documentsTbl.docType = 12 " & _
                       "ORDER BY documentsTbl.tartib"
        dtan = ClsDataTable.GetDataTable(sqlstr)
        If dtan.Rows.Count > 0 Then
            ASPxGrvHead0.DataSource = dtan
            ASPxGrvHead0.DataBind()
        End If


    End Sub
    Protected Sub ASPxGrvHead0_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ASPxGrvHead0.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(ASPxGrvHead0.Rows(radif).Cells(1).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from documentsTbl where id=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            load_grid_File()

        End If
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

    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        save_final()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub
    Private Sub load_Vahed()
        Dim dt As New DataTable
        Dim sqlstrr As String = "SELECT        omurID, omur " & _
       " FROM omurTbl WHERE        (CodeClosed = 0) ORDER BY omur"
        dt = ClsDataTable.GetDataTable(sqlstrr)
        CmbVahed.DataSource = dt
        CmbVahed.ValueField = "omurID"
        CmbVahed.TextField = "omur"
        CmbVahed.DataBind()

    End Sub
    Private Sub load_Units()
        Dim dt As New DataTable
        Dim cls_ As New ClsUnits
        dt = cls_.GetAllUnitsDDL
        CmbUnit.DataSource = dt
        CmbUnit.ValueField = "UnitsCode"
        CmbUnit.TextField = "UnitsName"
        CmbUnit.DataBind()
    End Sub
    Protected Sub nesbatRadio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles nesbatRadio.SelectedIndexChanged
        If nesbatRadio.SelectedValue = 1 Then
            nesbatLbl.Enabled = True
            nesbatCmb.Enabled = True
            perseneliLbl.Enabled = True
            persenliTxt.Enabled = True

        Else
            nesbatLbl.Enabled = False
            nesbatCmb.Enabled = False
            perseneliLbl.Enabled = False
            persenliTxt.Enabled = False
            persenliTxt.Text = ""
            nesbatCmb.SelectedValue = 0
        End If
    End Sub

    Protected Sub xedmat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles xedmat.SelectedIndexChanged
        If xedmat.SelectedValue = 1 Then


            moafitType.SelectedItem.Text = ""



        Else

            moafitType.Enabled = True


        End If
    End Sub
End Class
