Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Partial Class tpcRegisterStepEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Session("EOK") Is Nothing Then
        '    Response.Redirect("tpcRegisterEdit.aspx")
        'End If
        If Me.IsPostBack = False Then
            'load_cmbDocumentType()
            loadItems()
        End If
    End Sub
    Private Sub loadItems()
        Dim dti As New DataTable
        Dim sqlstri As String
        sqlstri = "SELECT        *         FROM registerVW         WHERE(idReg = " & Session("idReg") & ")"
        dti = ClsDataTable.GetDataTable(sqlstri)
        If dti.Rows.Count > 0 Then
            nam.Text = dti.Rows(0).Item("nam").ToString

            lname.Text = dti.Rows(0).Item("lname").ToString

            fatherName.Text = dti.Rows(0).Item("fatherName").ToString

            codeMelli.Text = dti.Rows(0).Item("codeMelli").ToString

            shsh.Text = dti.Rows(0).Item("shsh").ToString

            tabeiat.Text = dti.Rows(0).Item("tabeiat").ToString

            tarixtavlodtxt.Text = dti.Rows(0).Item("tarixTavalod").ToString
          
            jens.Text = dti.Rows(0).Item("jens").ToString

            tahol.Text = dti.Rows(0).Item("tahol").ToString

            xedmat.Text = dti.Rows(0).Item("xedmat").ToString

            madrakTxt.Text = dti.Rows(0).Item("madrak").ToString

            tell.Text = dti.Rows(0).Item("tell").ToString

            mobil.Text = dti.Rows(0).Item("mobil").ToString

            Mail.Text = dti.Rows(0).Item("mail").ToString

            address.Text = dti.Rows(0).Item("address").ToString

            shohada.Text = dti.Rows(0).Item("shohada").ToString

            moafiatTypeCmb.Text = dti.Rows(0).Item("moafitType").ToString

            nesbatRadio.Text = dti.Rows(0).Item("sanatNaft").ToString

            nesbatCmb.Text = dti.Rows(0).Item("nesbat").ToString

            persenliTxt.Text = dti.Rows(0).Item("codePerson").ToString

            codeMelli.Text = dti.Rows(0).Item("codeMelli").ToString

            shsh.Text = dti.Rows(0).Item("shsh").ToString

            codePosti.Text = dti.Rows(0).Item("codePosti").ToString
            reqIDtxt.Text = Session("idReg")
            reqTypetxt.Text = dti.Rows(0).Item("typeVlaue").ToString
            zamanTxt.Text = dti.Rows(0).Item("regDate").ToString

            notee.Text = (dti.Rows(0).Item("tozihat").ToString)

            If xedmat.Text = "معافیت" Then
                moafiatTypeLbl.Visible = True
                moafiatTypeCmb.Visible = True

            Else
                moafiatTypeLbl.Visible = False
                moafiatTypeCmb.Visible = False
            End If

            If nesbatRadio.Text = "بلی" Then
                nesbatLbl.Visible = True
                nesbatCmb.Visible = True
                perseneliLbl.Visible = True
                persenliTxt.Visible = True

            Else
                nesbatLbl.Visible = False
                nesbatCmb.Visible = False
                perseneliLbl.Visible = False
                persenliTxt.Visible = False


            End If

            If jens.Text = "زن" Then
                xedmat.Visible = False
                lblNezam.Visible = False
                moafiatTypeCmb.Visible = False
                moafiatTypeLbl.Visible = False

            Else
                xedmat.Visible = True
                lblNezam.Visible = True
            End If

            maharatha.Text = dti.Rows(0).Item("maharatha").ToString

            gerayeshTahsili.Text = dti.Rows(0).Item("gerayeshTahsili").ToString

            salMadrak.Text = dti.Rows(0).Item("salMadrak").ToString



            moadel1.Text = dti.Rows(0).Item("moadel").ToString

            reshte.Text = dti.Rows(0).Item("reshte").ToString

            madrakuni.Text = dti.Rows(0).Item("madrakuni").ToString
            load_grid_sabege()
            load_grid_sabege2()
            load_grid_FAKTOR()
        End If
    End Sub
    Private Sub load_grid_FAKTOR()
        GridView2.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT        id, content_name, content_file, fileID, typeVlue " & _
             "FROM            documentsVW   WHERE   (fileID = " & Session("idReg") & ")   "

        dtan = ClsDataTable.GetDataTable(sqlstr)
        GridView2.DataSource = dtan
        GridView2.DataBind()

    End Sub
    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(GridView2.DataKeys(gvrow.RowIndex).Value.ToString())

        Dim sqlstrd As String
        Dim dtd As New DataTable
        sqlstrd = "select  id, content_name, content_file  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)
        If dtd.Rows.Count > 0 Then
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("content_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("content_file"), Byte()))
            Response.[End]()

        End If
        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()



    End Sub
    Private Sub load_grid_sabege()
        ASPxGrvHead0.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT        idDtl, sherkat, modat, tempID, idReg,semat " & _
        "FROM registerSherkatTbl WHERE       (idReg = " & Session("idReg") & ")"

        dtan = ClsDataTable.GetDataTable(sqlstr)
        If dtan.Rows.Count > 0 Then
            ASPxGrvHead0.DataSource = dtan
            ASPxGrvHead0.DataBind()
            lblSabegeee.Visible = True
        End If

    End Sub
    Private Sub load_grid_sabege2()
        GridView1.Visible = True

        Dim dtan As DataTable
        Dim sqlstr As String = ""

        sqlstr = "SELECT        idDtl, sherkat, modat, tempID, idReg,semat " & _
        "FROM registerPetroTbl WHERE       (idReg = " & Session("idReg") & ")"

        dtan = ClsDataTable.GetDataTable(sqlstr)
        If dtan.Rows.Count > 0 Then
            GridView1.DataSource = dtan
            GridView1.DataBind()
            lblPetrooo.Visible = True
        End If

    End Sub
    'Private Sub load_cmbDocumentType()
    '    Dim dt As New DataTable
    '    Dim sqlstr As String
    '    sqlstr = "SELECT        idMadrak, madarkVal, sen         FROM registerMadrakTbl         WHERE(stat = 1)"
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    madrak.DataSource = dt
    '    madrak.DataTextField = "madarkVal"
    '    madrak.DataValueField = "idMadrak"
    '    madrak.DataBind()

    'End Sub
    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    'Protected Sub btnPage1_Click(sender As Object, e As EventArgs) Handles btnPage1.Click
    '    chekSen()

    '    If Len(codeMelli.Text) > 10 Or Len(codeMelli.Text) < 10 Then
    '        msg_box(" کدملی باید 10رقم باشد")
    '        codeMelli.Focus()
    '        Exit Sub
    '    End If
    '    If Len(codePosti.Text) > 10 Or Len(codePosti.Text) < 10 Then
    '        msg_box(" کدپستی باید 10رقم باشد")
    '        codePosti.Focus()
    '        Exit Sub
    '    End If

    '    saveFinal()


    'End Sub

    Private Sub saveFinal()


        'If hdn_Final_key.Value = "new" Then txtTicketId.Text = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "registerEditStep2Prc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@idReg", SqlDbType.Int, 10)
            cmm.Parameters("@idReg").Value = Session("idReg")

            cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
            cmm.Parameters("@madrak").Value = madrakTxt.Text

            cmm.Parameters.Add("@codePosti", SqlDbType.VarChar, 50)
            cmm.Parameters("@codePosti").Value = codePosti.Text

            cmm.Parameters.Add("@codePerson", SqlDbType.NVarChar, 50)
            cmm.Parameters("@codePerson").Value = persenliTxt.Text

            cmm.Parameters.Add("@shohada", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shohada").Value = shohada.Text

            cmm.Parameters.Add("@moafitType", SqlDbType.NVarChar, 50)
            cmm.Parameters("@moafitType").Value = moafiatTypeCmb.Text

            cmm.Parameters.Add("@sanatNaft", SqlDbType.NVarChar, 50)
            cmm.Parameters("@sanatNaft").Value = nesbatRadio.Text

            cmm.Parameters.Add("@nesbat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nesbat").Value = nesbatCmb.Text

            cmm.Parameters.Add("@address", SqlDbType.NVarChar, 250)
            cmm.Parameters("@address").Value = address.Text

            cmm.Parameters.Add("@mail", SqlDbType.VarChar, 50)
            cmm.Parameters("@mail").Value = Mail.Text

            cmm.Parameters.Add("@mobil", SqlDbType.NVarChar, 50)
            cmm.Parameters("@mobil").Value = mobil.Text

            cmm.Parameters.Add("@tell", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tell").Value = tell.Text

            'cmm.Parameters.Add("@salMadrak", SqlDbType.NVarChar, 50)
            'cmm.Parameters("@salMadrak").Value = salMadrak.Text

            'cmm.Parameters.Add("@moadel", SqlDbType.VarChar, 50)
            'cmm.Parameters("@moadel").Value = moadel.Text

            'cmm.Parameters.Add("@madrak", SqlDbType.NVarChar, 50)
            'cmm.Parameters("@madrak").Value = madrak.SelectedValue

            cmm.Parameters.Add("@xedmat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@xedmat").Value = xedmat.Text

            cmm.Parameters.Add("@tahol", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tahol").Value = tahol.Text


            cmm.Parameters.Add("@jens", SqlDbType.NVarChar, 50)
            cmm.Parameters("@jens").Value = jens.Text


            cmm.Parameters.Add("@tarixTavalod", SqlDbType.VarChar, 50)
            cmm.Parameters("@tarixTavalod").Value = tarixtavlodtxt.Text

            cmm.Parameters.Add("@tabeiat", SqlDbType.NVarChar, 50)
            cmm.Parameters("@tabeiat").Value = tabeiat.Text

            cmm.Parameters.Add("@shsh", SqlDbType.NVarChar, 50)
            cmm.Parameters("@shsh").Value = shsh.Text


            cmm.Parameters.Add("@codeMelli", SqlDbType.VarChar, 50)
            cmm.Parameters("@codeMelli").Value = codeMelli.Text

            cmm.Parameters.Add("@fatherName", SqlDbType.NVarChar, 50)
            cmm.Parameters("@fatherName").Value = fatherName.Text

            cmm.Parameters.Add("@lname", SqlDbType.NVarChar, 50)
            cmm.Parameters("@lname").Value = lname.Text

            cmm.Parameters.Add("@nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@nam").Value = nam.Text

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  درخواست شما با شماره پیگیری    " & HiddenField1.Value & "   ثبت شد")
                'Session("message") = "  درخواست با شماره پیگیری    " & tiketid & "   به واحد کامپیوتر ارسال شد"

                Response.Redirect("tpcRegisterStep3Edit.aspx")
                'Response.Redirect("messageFrm.aspx")



            Else
                'msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
        End Try
    End Sub

    'Protected Sub xedmat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles xedmat.SelectedIndexChanged
    '    If xedmat.SelectedValue = 2 Then
    '        moafiatTypeLbl.Visible = True
    '        moafiatTypeCmb.Visible = True
    '    Else
    '        moafiatTypeLbl.Visible = False
    '        moafiatTypeCmb.Visible = False
    '    End If
    'End Sub
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        'Dim message As String = ddlFruits.SelectedItem.Text & " - " & ddlFruits.SelectedItem.Value
        'ScriptManager.RegisterStartupScript(CType(sender, Control), Me.GetType(), "alert", "alert('" & message & "');", True)
        If xedmat.Text = "معافیت" Then
            moafiatTypeLbl.Visible = True
            moafiatTypeCmb.Visible = True

        Else
            moafiatTypeLbl.Visible = False
            moafiatTypeCmb.Visible = False
        End If

        If nesbatRadio.Text = "بلی" Then
            nesbatLbl.Visible = True
            nesbatCmb.Visible = True
            perseneliLbl.Visible = True
            persenliTxt.Visible = True

        Else
            nesbatLbl.Visible = False
            nesbatCmb.Visible = False
            perseneliLbl.Visible = False
            persenliTxt.Visible = False
            persenliTxt.Text = ""
            nesbatCmb.Text = ""
        End If

        If jens.Text = "زن" Then
            xedmat.Visible = False
            lblNezam.Visible = False
            moafiatTypeCmb.Visible = False
            moafiatTypeLbl.Visible = False

        Else
            xedmat.Visible = True
            lblNezam.Visible = True
        End If
    End Sub


    'Private Sub chekSen()
    '    Dim saljari As String = Left(ClsDateTime.GetDateNow, 4)
    '    'Dim salreg As Integer = sal.SelectedValue
    '    Dim salreg As Integer = 1399
    '    Dim dts As New DataTable
    '    Dim sqlstrs As String = "SELECT        sen         FROM registerMadrakTbl " & _
    '                      "WHERE         (madarkVal = N'" & madrak.SelectedItem.Text & "') "
    '    dts = ClsDataTable.GetDataTable(sqlstrs)
    '    Dim senMadrak As String = dts.Rows(0).Item("sen").ToString

    '    Dim senPerson As Integer = Val(saljari) - Val(salreg)
    '    If Val(senPerson) > Val(senMadrak) Then
    '        msg_box(" برای مدرک " & madrak.SelectedItem.Text & " حداکثر سن " & senMadrak & " می باشد ")
    '        'btnPage1.Visible = False
    '        'Panel1.Visible = False

    '        'lblMadrak.Text = " برای مدرک " & madrak.SelectedItem.Text & " حداکثر سن " & senMadrak & " می باشد "
    '        'lblMadrak.ForeColor = Drawing.Color.Red
    '    Else
    '        'btnPage1.Visible = True
    '        'lblMadrak.Text = ""
    '        'Panel1.Visible = True
    '    End If
    'End Sub

    'Protected Sub madrak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles madrak.SelectedIndexChanged
    '    If madrak.SelectedValue = 0 Then
    '        'lblMadrak.Text = ""
    '        Exit Sub
    '    Else
    '        chekSen()
    '    End If

    'End Sub



    'Protected Sub sal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles sal.SelectedIndexChanged
    '    If sal.SelectedValue = 0 Then

    '        Exit Sub
    '    Else
    '        chekSen()
    '    End If
    'End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("frmBrowsWorks.aspx")
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("frmBrowsWorks.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("http://192.168.9.18:89/loginTpcRegisterEdit.aspx")
    End Sub
End Class
