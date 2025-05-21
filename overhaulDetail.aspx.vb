Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports System.IO.Compression
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports Ionic.Zip
Imports System.Drawing
Imports iTextSharp.text.pdf
Imports iTextSharp.text


Partial Class overhaulDetail
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable
    Dim sat As String = ClsDateTime.GetTimeNow
    Dim tarix As String = ClsDateTime.GetDateNow
    Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

    Protected fullName As String = "علی رضایی"
    Protected personnelCode As String = "123456"
    Protected position As String = "کارشناس فنی"
    Protected photoPath As String = "/Attachment/1363722026/PersonalPic.jpg"
    Protected expirationDate As String = "1404/01/02"
    Protected signaturePath As String = "/images/signature.jpg"

 


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
      
        If Session("sms") = "True" Then

            PanelSms.Visible = True

        End If


        If Session("erja") = "True" Then
            PanelRunevest.Visible = True
            CmbruneveshtType.Visible = True
            lblruneveshtType.Visible = True
            GridViewErja.Visible = True

        Else
            CmbruneveshtType.Visible = False
            lblruneveshtType.Visible = False
            'GridViewErja.Visible = False
            GridViewErja.Columns.Item(2).Visible = False
            PanelRunevest.Visible = False

        End If

        If Session("ok") = "True" Then

            btnOk.Visible = True
        End If

        If Session("calncel") = "True" Then
            BtnCancel.Visible = True


        End If

       

        If Session("omurID") = 19 Then

            Dim sqlstre As String = "SELECT       Id  FROM            overhaulAllVW  WHERE        ((State < 0 )  or  (State = 106) or  (State = 103)  or  (State = 102)  or  (State = 101)   or  (State = 100) or  (State = 199) or  (State = 200) or  (State = 6) or  (State = 33) or (State = 13) or (State = 15)) AND (Id = " & Session("idOver") & ") "
            Dim dte As New DataTable
            dte = ClsDataTable.GetDataTable(sqlstre)
            If dte.Rows.Count > 0 Then
                PanelRunevest.Visible = False
                PanelSms.Visible = False

                BtnCancel.Visible = False
                btnOk.Visible = False

            End If
        End If

        If Session("omurID") = 19 Then

            Dim sqlstre As String = "SELECT       Id  FROM            overhaulAllVW  WHERE        ( (State = 200)) AND (Id = " & Session("idOver") & ") "
            Dim dte As New DataTable
            dte = ClsDataTable.GetDataTable(sqlstre)
            If dte.Rows.Count > 0 Then
                PanelRunevest.Visible = False
                PanelSms.Visible = False

                BtnCancel.Visible = False
                btnOk.Visible = True

            End If
        End If
        If Session("omurID") = 19 Then

            Dim sqlstre As String = "SELECT       Id  FROM            overhaulAllVW  WHERE        ( (State = 19)) AND (Id = " & Session("idOver") & ") "
            Dim dte As New DataTable
            dte = ClsDataTable.GetDataTable(sqlstre)
            If dte.Rows.Count > 0 Then
                PanelRunevest.Visible = True
                PanelSms.Visible = True

                BtnCancel.Visible = False
                btnOk.Visible = False

            End If
        End If

        If Session("omurID") = 100 Then
            PanelMadarek.Visible=True 
            Dim sqlstre As String = "SELECT       Id  FROM            overhaulAllVW  WHERE        ((State < 0 ) or (State = 106) or (State = 103) or (State = 19) or (State = 102) or (State = 101) or  (State = 199) or  (State = 200) or  (State = 6) or  (State = 33) or (State = 13) or (State = 15)) AND (Id = " & Session("idOver") & ") "
            Dim dte As New DataTable
            dte = ClsDataTable.GetDataTable(sqlstre)
            If dte.Rows.Count > 0 Then
                PanelRunevest.Visible = False
                PanelSms.Visible = False

                BtnCancel.Visible = False
                btnOk.Visible = False

            End If
        End If
        If Session("omurID") = 100 Then

            Dim sqlstre As String = "SELECT       Id  FROM            overhaulAllVW  WHERE        ( (State = 100)) AND (Id = " & Session("idOver") & ") "
            Dim dte As New DataTable
            dte = ClsDataTable.GetDataTable(sqlstre)
            If dte.Rows.Count > 0 Then
                PanelRunevest.Visible = False
                PanelSms.Visible = False

                BtnCancel.Visible = False
                btnOk.Visible = False
                PanelFileUpload.Visible = True
                'btnHse.Visible = True

            End If
        End If

        If Session("omurID") = 6 Then
            gridHseLoad()
            PanelRunevest.Visible = False
                PanelSms.Visible = False
            PanelAttach.Visible = False

            'BtnCancel.Visible = False
                btnOk.Visible = False
            PanelFileUpload.Visible = False
            btnHse.Visible = False
            PanelHse.Visible = True


        End If

        If Session("omurID") = 15 Then

            PanelRunevest.Visible = False
            PanelSms.Visible = False
            'BtnCancel.Visible = False
            'btnOk.Visible = True
            PanelFileUpload.Visible = False
            btnHse.Visible = False
            PanelHse.Visible = False
            PanelEmeni.Visible = True

        End If

        If Session("omurID") = 33 Then

            PanelRunevest.Visible = False
            PanelSms.Visible = False

            BtnCancel.Visible = True
            btnOk.Visible = True
            PanelFileUpload.Visible = False
            btnHse.Visible = False
            PanelHse.Visible = False
            PanelEmeni.Visible = False
            PanelHerasat.Visible = False


        End If
        If Session("omurID") = 101 Then

            PanelRunevest.Visible = False
            PanelSms.Visible = False

            BtnCancel.Visible = False
            btnOk.Visible = True
            PanelFileUpload.Visible = False
            btnHse.Visible = False
            PanelHse.Visible = False
            PanelEmeni.Visible = False
            PanelHerasat.Visible = False
            PanelHR.Visible = True


        End If
        
        If Session("omurID") = 102 Then

            PanelRunevest.Visible = False
            PanelSms.Visible = False

            BtnCancel.Visible = False
            btnOk.Visible = True
            PanelFileUpload.Visible = False
            btnHse.Visible = False
            PanelHse.Visible = False
            PanelEmeni.Visible = False
            PanelHerasat.Visible = False
            PanelHR.Visible = False
            PanelHerasat.Visible = True


        End If
        If Session("omurID") = 103 Then

            PanelRunevest.Visible = False
            PanelSms.Visible = False

            BtnCancel.Visible = True
            btnOk.Visible = True
            PanelFileUpload.Visible = False
            btnHse.Visible = False
            PanelHse.Visible = False
            PanelEmeni.Visible = False
            PanelHerasat.Visible = False
            PanelHR.Visible = False
            PanelHerasat.Visible = False
            PanelDate.Visible = True

        End If
        loadItems()
        'PersonImage.ImageUrl = "~/Attachment/" & Nationality.Text & "/PersonalPic.jpg"

        If Not Page.IsPostBack Then

            txtDate.Text = ClsDateTime.GetDateNow()
            mosahebeDate.Text = ClsDateTime.GetDateNow()
            expiretxt.Text = ClsDateTime.GetDateNow()

            txtDateKelas.Text = ClsDateTime.GetDateNow()
            'Dim Strh As String = "SELECT        sender, omurId, overhaulId, kartablStat         FROM overhaulKartablTbl " & _
            '         " WHERE(sender = 100) AND (kartablStat = 1) AND (omurId = 6) AND (overhaulId = " & Session("idOver") & ")"

            'Dim dtth As New DataTable


            If Session("erja") = "True" Then
                Dim kartal As String = "SELECT        sender, omurId, overhaulId, kartablStat         FROM overhaulKartablTbl " & _
                                 " WHERE  (kartablStat = 1) AND (omurId = " & Session("omurID") & ") AND (overhaulId = " & Session("idOver") & ")"
                Dim dtk As New DataTable
                dtk = ClsDataTable.GetDataTable(kartal)

                Dim senderr As Integer = 0

                If dtk.Rows.Count > 0 Then

                    senderr = dtk.Rows(0).Item("sender").ToString()
                    cmbRunevesht.SelectedValue = senderr
                End If
            End If


            gridMadrakLoad()
            LoadImages()
            loadItems()
            loadcmbParaf()

            'PanelRunevest.Visible = True

            load_cmbRuneveshtUsers()
            loadComboRuneveshtTyp()



        End If

        gridErja()
        notes()
        If Session("omurID") <> 19 Then
            cmbRunevesht.Enabled = False

        End If

    End Sub

    'Private Sub overHulSms()
    '    Dim sqlstr As String = "SELECT        idd, valuee, omurID, CodeClosed         FROM overhaulSmsTbl         WHERE(omurID = " & Session("omurID") & ")"
    '    Dim dt As New DataTable
    '    dt = ClsDataTable.GetDataTable(sqlstr)

    '    If dt.Rows.Count > 0 Then
    '        txtSms.Text = dt.Rows.Item("valuee").ToString()
    '    End If
    'End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

        Dim sqlstr, sqlstr2, sqlstr3, sqlstr4, sqlstr5, sqlstr6, sqlstr7, str As String
        Dim payam As String = ""

        payam = txtNazar.Text & "  تایید شد"

        str = "SELECT        TOP (200) Id, sender, action, reciver, finish         FROM workTableGuidTbl " & _
                  " WHERE(sender = " & Session("omurID") & ") AND (action = 1)"

        Dim dtt As New DataTable
        dtt = ClsDataTable.GetDataTable(str)


        Dim reciver As Integer = 0

        If dtt.Rows.Count > 0 Then

            reciver = dtt.Rows(0).Item("reciver").ToString()
        End If

        sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
                "VALUES        (" & Id.Text & ", " & Session("omurID") & ", " & reciver & ", '" & tarix & "', '" & sat & "', N'" & payam & "')"

        sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") "

        sqlstr6 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & "," & reciver & "," & Session("idOver") & ", 1)"

        sqlstr = "  UPDATE RegisterOverhauls  SET   State = " & reciver & "  WHERE (Id = " & Session("idOver") & ") "


        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"


        upStat(sqlstr4)
        upStat(sqlstr)
        upStat(sqlstr2)
        upStat(sqlstr3)

        upStat(sqlstr5)
        upStat(sqlstr6)
        upStat(sqlstr7)

        msg_box("تایید شد")
        notes()
        loadItems()
        gridErja()

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        Dim sqlstr, sqlstr2, sqlstr3, sqlstr4, sqlstr5, sqlstr6, sqlstr7, str, sqlstr9 As String



        Dim payam As String = ""


        payam = txtNazar.Text & "عدم تایید "

        str = "SELECT        TOP (200) Id, sender, action, reciver, finish         FROM workTableGuidTbl " & _
                  " WHERE(sender = " & Session("omurID") & ") AND (action = 0)"

        Dim dtt As New DataTable
        dtt = ClsDataTable.GetDataTable(str)
        Dim reciver As Integer = 0

        If dtt.Rows.Count > 0 Then

            reciver = dtt.Rows(0).Item("reciver").ToString()
        End If

        sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
                "VALUES        (" & Id.Text & ", " & Session("omurID") & ", " & reciver & ", '" & tarix & "', '" & sat & "', N'" & payam & "')"

        sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") and (omurId = " & Session("omurID") & ")"

        sqlstr6 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & "," & reciver & "," & Session("idOver") & ", 1)"

        Dim stat As String = "-" & Session("omurID")

        sqlstr = "  UPDATE RegisterOverhauls  SET   State = " & stat & "  WHERE (Id = " & Session("idOver") & ") "


        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"


        If Session("omurID") = 6 Then

            Dim smstext1 As String = "با سلام،به علت عدم تایید واحد HSE امکان همکاری با شما مقدور نمیباشد.پیمانکار اورهال پتروشیمی تبریز"

            sqlstr9 = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
              "VALUES     ('" & Mobile.Text & "', N'" & smstext1 & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"

            upStat(sqlstr9)

        ElseIf Session("omurID") = 15 Then

            Dim smstext2 As String = "با سلام،به علت عدم تایید واحد ایمنی امکان همکاری با شما مقدور نمیباشد.پیمانکار اورهال پتروشیمی تبریز"

            sqlstr9 = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
              "VALUES     ('" & Mobile.Text & "', N'" & smstext2 & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"
            upStat(sqlstr9)
        End If

        upStat(sqlstr4)
        upStat(sqlstr)
        upStat(sqlstr2)
        upStat(sqlstr3)

        upStat(sqlstr5)
        upStat(sqlstr6)
        upStat(sqlstr7)

        msg_box("عدم تایید ")
        notes()
        loadItems()
        gridErja()


    End Sub

    Private Sub LoadImages()
        Dim folder = "~/Attachment/" & Nationality.Text & "/"
        ' Dim folder = "~/Attachment/1372079786/"
        Dim folderPath = Server.MapPath(folder)
        Dim files = Directory.GetFiles(folderPath).Take(10).ToList()

        Dim images = files.Select(Function(f) New With {
            Key .Url = ResolveUrl(folder & Path.GetFileName(f)),
            Key .Name = Path.GetFileName(f),
            Key .Nat = Nationality.Text
        }).ToList()

        RepeaterImages.DataSource = images
        RepeaterImages.DataBind()
    End Sub

    Protected Sub btnDownloadZip_Click(sender As Object, e As EventArgs) Handles btnDownloadZip.Click
        Dim folderPath As String = Server.MapPath("~/Attachment/" & Nationality.Text & "/")
        Dim files = Directory.GetFiles(folderPath).Take(10).ToList()

        Response.Clear()
        Response.BufferOutput = False
        Response.ContentType = "application/zip"
        Response.AddHeader("content-disposition", "attachment; filename=" & Nationality.Text & ".zip")

        Using zip As New Ionic.Zip.ZipFile()
            For Each path In files
                If File.Exists(path) Then
                    zip.AddFile(path, "") ' بدون پوشه داخلی
                End If
            Next
            zip.Save(Response.OutputStream)
        End Using

        Response.Flush()
        Response.End()
    End Sub



    Sub notes()
        Dim dt2 As New DataTable
        Dim sqlstr As String = " SELECT       ID, codePersoneli, headd, IdOverhaul, notee, note_id, REPLACE([pic ], ' ', '') AS picc from overhaulCommentVW WHERE        (IdOverhaul = " & Session("idOver") & ")  ORDER BY note_id DESC"

        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            DataList1.DataSource = dt2
            DataList1.DataBind()
        End If


    End Sub
    Protected Sub DataList1_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemCreated
        For i As Integer = 0 To DataList1.Items.Count - 1
            '  Dim labelcode As new Label = DataList5 .Items(i).FindControl("Label8")
            Dim labelcode As Label = DirectCast(DataList1.Items(i).FindControl("lblUsrTyp"), Label)
            If Val(labelcode.Text) = 100 Then
                DataList1.Items(i).BackColor = Drawing.Color.LightYellow

            ElseIf Val(labelcode.Text) = 0 Then
                DataList1.Items(i).BackColor = Drawing.Color.Khaki
                'DataList1.Items(i).Font.Bold = True
            ElseIf Val(labelcode.Text) = 2 Then
                DataList1.Items(i).BackColor = Drawing.Color.MediumAquamarine
            ElseIf Val(labelcode.Text) = 1 Then
                DataList1.Items(i).BackColor = Drawing.Color.AntiqueWhite
            ElseIf Val(labelcode.Text) = 5 Then
                DataList1.Items(i).BackColor = Drawing.Color.MistyRose
            ElseIf Val(labelcode.Text) = 6 Then
                DataList1.Items(i).BackColor = Drawing.Color.Aquamarine
            End If
        Next
    End Sub
    Private Sub save_final()

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "overhaulCommentPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@sugID", SqlDbType.Int)
            cmm.Parameters("@sugID").Value = Id.Text

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@notee").Value = txtNazar.Text

            cmm.Parameters.Add("@PerID", SqlDbType.Int, 10)
            cmm.Parameters("@PerID").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            'Dim frm As String = ""

            'If Session("modir") = True Then
            '    frm = "modir"
            'Else
            '    frm = Session("from")
            'End If


            'cmm.Parameters.Add("@userType", SqlDbType.VarChar, 7)
            'cmm.Parameters("@userType").Value = frm




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


            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

    Private Sub loadComboRuneveshtTyp()
        Dim dtm As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "SELECT        Title + N' دستمزد ساعتی '+ FORMAT(Price, 'N0') + N' ریال ' AS cahrt, chartId " & _
     "FROM overhaulChartTbl WHERE        (isActive  = 1)   ORDER BY chartId"
        'End If

        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbruneveshtType.DataSource = dtm
        CmbruneveshtType.TextField = "cahrt"
        CmbruneveshtType.ValueField = "chartId"
        CmbruneveshtType.DataBind()
    End Sub

    Private Sub load_cmbRuneveshtUsers()
        Dim dtm As New DataTable
        Dim sqlstr As String = ""

        'If Session("from") = "admin" Then
        sqlstr = "SELECT     omurID, omur         FROM omurTbl " & _
                  " WHERE     (overhaul = 1) AND (CodeClosed = 0) ORDER BY omur"
        'Else
        '      sqlstr = "SELECT        nam + ' ' + family +  ' - ' + postname AS karbar, ID " & _
        '"FROM omurTbl WHERE        (CodeClosed = 0) AND (overhaul = 1)  ORDER BY family"
        'End If

        dtm = ClsDataTable.GetDataTable(sqlstr)
        cmbRunevesht.DataSource = dtm
        cmbRunevesht.DataTextField = "omur"
        cmbRunevesht.DataValueField = "omurID"
        cmbRunevesht.DataBind()

    End Sub

    Sub gridErja()
        Dim dt2 As New DataTable

        Dim sqlstr As String = ""
        'If Session("from") = "user" Then
        '         sqlstr = " SELECT        girande,tarix, saat, matnErja, erja, aslRuneveshtType, actionId, nam + ' ' + family AS ferestande, runeveshtTypeValue " & _
        '"FROM sugReciverVW WHERE        (sender = " & Session("userID") & ") AND (laterID = " & Session("sugID") & ") "
        '     ElseIf Session("from") = "admin" Then
        sqlstr = " SELECT        girande,tarix, saat, matnErja, ferestande,FormattedNumber " & _
"FROM overhalErjatVW WHERE        (laterID = " & Id.Text & ") "
        '   ElseIf Session("from") = "reciver" Then
        '       sqlstr = " SELECT        girande,tarix, saat, matnErja, erja, aslRuneveshtType, actionId, nam + ' ' + family AS ferestande, runeveshtTypeValue " & _
        '"FROM sugReciverVW WHERE        (reciverID = " & Session("userID") & ") AND (laterID = " & Session("sugID") & ") "
        'End If




        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            'PanelErja.Visible = True
            GridViewErja.DataSource = dt2
            GridViewErja.DataBind()
        End If


    End Sub
    Private Sub gridHseLoad()
        Dim dt2 As New DataTable

        Dim sqlstr As String = ""

        sqlstr = "SELECT        TOP (200) tarix, COUNT(shiftID) AS countt, shifName         FROM overhaulHseKelasTbl GROUP BY tarix, shifName "


        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            'PanelErja.Visible = True
            GridHse.DataSource = dt2
            GridHse.DataBind()
        End If


    End Sub
    Private Sub loadcmbParaf()
        Dim dtg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT idd,valuee FROM tblTicketTemplate WHERE     (CodeClosed = 0) and userId= " & Session("userid") & ""
        dtg = ClsDataTable.GetDataTable(sqlstr)

        'cmbParaf.DataSource = dtg
        'cmbParaf.DataTextField = "valuee"
        'cmbParaf.DataValueField = "idd"
        'cmbParaf.DataBind()

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

    Private Sub loadItems()
        Dim dt As DataTable

        Dim sqlstrr As String = "SELECT *  FROM   overhaulAllVW WHERE        (Id = " & Session("idOver") & ")"
        dt = ClsDataTable.GetDataTable(sqlstrr)
        If dt.Rows.Count > 0 Then

            Id.Text = dt.Rows(0).Item("Id")
            LastName.Text = dt.Rows(0).Item("LastName")
            FirstName.Text = dt.Rows(0).Item("FirstName")
            FatherName.Text = dt.Rows(0).Item("FatherName")
            Nationality.Text = dt.Rows(0).Item("Nationality")
            ShSh.Text = dt.Rows(0).Item("shSh")
            BirthDay.Text = dt.Rows(0).Item("BirthDay")
            BankCardNo.Text = dt.Rows(0).Item("BankCardNo").ToString()
            IBAN.Text = dt.Rows(0).Item("IBAN").ToString()

            SSN.Text = dt.Rows(0).Item("SSN").ToString()
            Mobile.Text = dt.Rows(0).Item("Mobile")
            Tell.Text = dt.Rows(0).Item("Tell")
            PostalCode.Text = dt.Rows(0).Item("PostalCode")
            Address.Text = dt.Rows(0).Item("Address")
            Degree.Text = dt.Rows(0).Item("madrak")
            Major.Text = dt.Rows(0).Item("reshte")
            'State.Text = dt.Rows(0).Item("IsLastOverhaul")
            tahol.Text = dt.Rows(0).Item("martialStatus")

            xedmat.Text = dt.Rows(0).Item("MilitaryName")
            isargari.Text = dt.Rows(0).Item("isargari")
            IsLastOverhaul.Text = dt.Rows(0).Item("overhalHistory").ToString()
            JobPosition.Text = dt.Rows(0).Item("JobPosition").ToString()
            bime.Text = dt.Rows(0).Item("bime").ToString()
            province.Text = dt.Rows(0).Item("province").ToString()
            city.Text = dt.Rows(0).Item("city").ToString()
            'oldPersoelCode.Text = "-"
            EducationalOrientation.Text = dt.Rows(0).Item("EducationalOrientation").ToString()
            lblStatus.Text = dt.Rows(0).Item("Title").ToString()

            fullName = FirstName.Text & " " & LastName.Text
            personnelCode = dt.Rows(0).Item("codePersoneli").ToString()
            position = dt.Rows(0).Item("chart").ToString()

            photoPath = ResolveUrl("~/Attachment/" & Nationality.Text & "/PersonalPic.jpg")
            signaturePath = ResolveUrl("~/images/signature.jpg")

            lblShift.Text = dt.Rows(0).Item("shift").ToString()
            expirationDate = dt.Rows(0).Item("expireDateCard").ToString()
          


        End If
    End Sub




    Protected Sub btnNazar_Click(sender As Object, e As EventArgs) Handles btnNazar.Click
        save_final()
        notes()
        txtNazar.Text = ""
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
        Session("idOver") = 0
        'If Session("fromTyp") = "sms" Then
        '    Response.Redirect("overhaulBrowseSms.aspx")
        'End If

        If Session("omurID") = 13 Or Session("omurID") = 19 Or Session("omurID") = 100 Then
            Response.Redirect("overhaulReportFrm.aspx")
        Else
            Response.Redirect("overhaulBrowseAdmin.aspx")
        End If

    End Sub

    Protected Sub btnSaveFinal_Click(sender As Object, e As EventArgs) Handles btnSaveFinal.Click
        If cmbRunevesht.SelectedValue = 0 Then
            cmbRunevesht.Focus()
            msg_box("لطفا گیرنده را انتخاب نمایید")

            Exit Sub
        Else

            'If Val(hdn_tmp.Value) = 0 Then
            save_Erja2()

            'End If

            Dim payam As String = ""

            payam = txtNazar.Text & " پرونده ارجاع شد به " & cmbRunevesht.SelectedItem.Text & " "
            'End If

            Dim sqlstr As String = ""

            Dim sqlstr2 As String = ""

            Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

            'sqlstr = "  UPDATE RegisterOverhauls  SET   State = " & cmbRunevesht.Value & " WHERE (Id = " & Session("idOver") & ") "


            If Session("omurID") = 19 Then
                sqlstr = "  UPDATE RegisterOverhauls  SET   State = 199 WHERE (Id = " & Session("idOver") & ") "
            Else
                sqlstr = "  UPDATE RegisterOverhauls  SET   State = 200 WHERE (Id = " & Session("idOver") & ") "
            End If

            'If Session("omurID") = 13 Then
            '    sqlstr = "  UPDATE RegisterOverhauls  SET   State = 9  WHERE (Id = " & Session("idOver") & ") "

            'End If

            sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                        " VALUES     (" & Id.Text & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"

            If Session("omurID") = 19 Then
                Dim sqlstrSms As String = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
                      "VALUES     ('" & Mobile.Text & "', N'" & txtSms.Text & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"
                upStat(sqlstrSms)
            End If


            upStat(sqlstr)
            upStat(sqlstr2)

            loadItems()
            notes()
            gridErja()
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
            cmm.Parameters("@laterID").Value = Id.Text

            cmm.Parameters.Add("@reciverID", SqlDbType.Int, 5)
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.SelectedValue)

            cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            cmm.Parameters("@sender").Value = Session("omurID")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@chartID", SqlDbType.Int, 5)
            cmm.Parameters("@chartID").Value = Val(CmbruneveshtType.Value)

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
                msg_box(" پرونده باموفقیت ارجاع داده شد ")
                '  Call save_()
                btnSaveFinal.Visible = False
                PanelRunevest.Enabled = False
                txtNazar.Enabled = False
                btnNazar.Enabled = False
                'btnParaf.Enabled = False
                btnOk.Enabled = False
                'btnWork.Enabled = False
                BtnCancel.Enabled = False

                'load_grid()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub




    Protected Sub btnRunevesht_Click(sender As Object, e As EventArgs) Handles btnRunevesht.Click
        


        If Session("omurID") <> 19 And Val(CmbruneveshtType.Value) = 0 Then

            'Dim sqlstr4 As String = "SELECT        laterID  FROM            laterReciverTbl WHERE        (laterID = " & Session("idOver") & ")"
            'Dim dt4 As New DataTable
            'dt4 = ClsDataTable.GetDataTable(sqlstr4)
            'If dt4.Rows.Count < 3 Then
            msg_box("لطفا چارت پیشنهادی  را انتخاب نمایید")
            CmbruneveshtType.Focus()
            Exit Sub
            'End If



        End If

        'If Val(cmbRunevesht.SelectedValue) <> 0 Then
        '    Dim capacity As Integer = 0
        '    Dim countt As Integer = 0

        '    If Session("omurID") = 19 Then
        '        capacity = GetCapacityByCode(cmbRunevesht.SelectedValue)

        '        countt = GetCountByCode(cmbRunevesht.SelectedValue)
        '    Else
        '        capacity = GetCapacityByCode(Session("omurID"))

        '        countt = GetCountByCode(Session("omurID"))
        '    End If



        '    If countt >= capacity Then
        '        msg_box("ظرفیت این واحد به اتمام رسیده است")
        '        CmbruneveshtType.Focus()
        '        Exit Sub
        '    End If



        'End If



        If cmbRunevesht.SelectedValue = 0 Then
            cmbRunevesht.Focus()
            msg_box("لطفا گیرنده  را انتخاب نمایید")

            Exit Sub
        Else
            If Val(hdn_tmp.Value) = 0 Then
                hdn_tmp.Value = ClsGetMaxId.GetMaxId("laterRuneveshtTempTbl", "tempID")
            End If
            save_tmp(True, False)
            GridView2.Visible = True
            load_grid_tmp()

        End If

    End Sub

    Function GetCapacityByCode(code As String) As Integer
        Dim sqlstrc As String = "SELECT        omurID, cpasity         FROM omurTbl WHERE        (overhaul = 1)"
        Dim dtc As New DataTable
        dtc = ClsDataTable.GetDataTable(sqlstrc)

        For Each row As DataRow In dtc.Rows
            If row("omurID").ToString().Trim() = code.Trim() Then
                Return Convert.ToInt32(row("cpasity"))
            End If
        Next
        Return -1 ' اگر کدی پیدا نشد
    End Function

    Function GetCountByCode(code As String) As Integer
        Dim sqlstrc As String = "SELECT        COUNT(reciverID) AS countt, reciverID        FROM laterReciverTbl         WHERE(sender = 19) GROUP BY reciverID"
        Dim dtc As New DataTable
        dtc = ClsDataTable.GetDataTable(sqlstrc)

        For Each row As DataRow In dtc.Rows
            If row("reciverID").ToString().Trim() = code.Trim() Then
                Return Convert.ToInt32(row("countt"))
            End If
        Next
        Return 0 ' اگر کدی پیدا نشد
    End Function
    Function GetSumMosahebeByCode(code As String, tarix As String) As Integer
        Dim sqlstrc As String = "SELECT        shiftID, COUNT(shiftID) AS countt, tarix         FROM overhaulMosahebeDateTbl GROUP BY shiftID, tarix"
        Dim dtc As New DataTable
        dtc = ClsDataTable.GetDataTable(sqlstrc)

        For Each row As DataRow In dtc.Rows
            If row("shiftID").ToString().Trim() = code.Trim() And row("tarix").ToString().Trim() = tarix.Trim() Then
                Return Convert.ToInt32(row("countt"))
            End If
        Next
        Return 0 ' اگر کدی پیدا نشد
    End Function

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
            cmm.Parameters("@reciverID").Value = Val(cmbRunevesht.SelectedValue)

            cmm.Parameters.Add("@laterID", SqlDbType.Int)
            cmm.Parameters("@laterID").Value = Id.Text

            cmm.Parameters.Add("@sender", SqlDbType.Int, 10)
            cmm.Parameters("@sender").Value = Session("omurID")

            cmm.Parameters.Add("@matnErja", SqlDbType.NVarChar, 2000)
            cmm.Parameters("@matnErja").Value = txtMatnErja.Text

            cmm.Parameters.Add("@runevestTypeID", SqlDbType.Int, 10)
            cmm.Parameters("@runevestTypeID").Value = Val(CmbruneveshtType.Value)

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
        If dt.Rows.Count > 0 Then
            btnSaveFinal.Visible = True
            GridView2.DataSource = dt
            GridView2.DataBind()
        Else
            btnSaveFinal.Visible = False
            GridView2.Visible = False
        End If

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

    Protected Sub CheckBoxAttach_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAttach.CheckedChanged
        If CheckBoxAttach.Checked = True Then
            'UpdatePanel1.Visible = True
            PanelAttach.Visible = True


            'btnSaveFinal.Visible = True
        Else
            PanelAttach.Visible = False
            'UpdatePanel1.Visible = False
            'btnPuyesh.Visible = False

        End If
    End Sub


   



    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim messages As New List(Of String)
        Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".pdf"}
        Dim maxSize As Integer = 1048576 ' 1MB

        Try
            ' مسیر پوشه ذخیره‌سازی
            Dim folderPath As String = Server.MapPath("~/Attachment/" & Nationality.Text & "/")
            If Not System.IO.Directory.Exists(folderPath) Then
                System.IO.Directory.CreateDirectory(folderPath)
            End If

            ' آپلود کارت سلامت
            If FileUploadHealth.HasFile Then
                Dim ext1 As String = System.IO.Path.GetExtension(FileUploadHealth.FileName).ToLower()
                If Not allowedExtensions.Contains(ext1) Then
                    messages.Add("فرمت فایل کارت سلامت مجاز نیست.")
                    msg_box("فرمت فایل کارت سلامت مجاز نیست.")
                    Exit Sub
                ElseIf FileUploadHealth.PostedFile.ContentLength > maxSize Then
                    messages.Add("حجم فایل کارت سلامت نباید بیشتر از 1 مگابایت باشد.")
                Else
                    Dim filename1 As String = Session("omurID") & "_health" & ext1
                    FileUploadHealth.SaveAs(System.IO.Path.Combine(folderPath, filename1))
                    messages.Add("فایل کارت سلامت با موفقیت آپلود شد.")
                End If
            Else
                messages.Add("لطفاً فایل کارت سلامت را انتخاب کنید.")
                msg_box("لطفاً فایل کارت سلامت را انتخاب کنید.")
                Exit Sub
            End If

            ' آپلود برگه معاینه
            'If FileUploadCheckup.HasFile Then
            Dim ext2 As String = System.IO.Path.GetExtension(FileUploadCheckup.FileName).ToLower()
            If Not allowedExtensions.Contains(ext2) Then
                messages.Add("فرمت فایل معاینه مجاز نیست.")
            ElseIf FileUploadCheckup.PostedFile.ContentLength > maxSize Then
                messages.Add("حجم فایل معاینه نباید بیشتر از 1 مگابایت باشد.")
            Else
                Dim filename2 As String = Session("omurID") & "_checkup" & ext2
                FileUploadCheckup.SaveAs(System.IO.Path.Combine(folderPath, filename2))
                messages.Add("فایل معاینه با موفقیت آپلود شد.")
            End If
            'Else
            '    messages.Add("لطفاً فایل معاینه را انتخاب کنید.")
            'End If

            lblMessage.Text = "<div class='alert alert-info'>" & String.Join("<br/>", messages) & "</div>"
            msg_box("ذخیره شد")
            notes()
            loadItems()
            gridErja()
            LoadImages()

            btnHse.Visible = True
        Catch ex As Exception
            lblMessage.Text = "<div class='alert alert-danger'>خطا در آپلود فایل‌ها: " & ex.Message & "</div>"
        End Try
    End Sub



    Protected Sub btnSms_Click(sender As Object, e As EventArgs) Handles btnSms.Click
        If GetSumMosahebeByCode(cmbMosahebeDate.SelectedValue, mosahebeDate.Text) >= 150 Then
            msg_box("ظرفیت این ساعت تکمیل است=150")
            Exit Sub
        End If

        txtSms.Text = "لطفاً در تاریخ " & mosahebeDate.Text & " " & "جهت مصاحبه اورهال، به پیمانکار پترو صنعت جنوب مستقر در درب حراست پتروشیمی تبریز از ساعت" & cmbMosahebeDate.SelectedItem.Text & " " & "مراجعه فرمایید.مدارک مورد نیاز شامل کارت ملی، شناسنامه، عدم سوءپیشینه و ۲ قطعه عکس را به همراه داشته باشید"

        Dim sqlstrdel As String = "DELETE  FROM            overhaulMosahebeDateTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)

        Dim sqlstr As String = "INSERT INTO overhaulMosahebeDateTbl     (IdOver, tarix,shiftID,shifName)  VALUES        (" & Id.Text & ", '" & mosahebeDate.Text & "'," & cmbMosahebeDate.SelectedValue & ",N'" & cmbMosahebeDate.SelectedItem.Text & "')"
        upStat(sqlstr)


        Dim sqlstr5 As String

        Dim payam As String = "تاریخ مصاحبه: " & " " & mosahebeDate.Text & " ساعت:" & " " & cmbMosahebeDate.SelectedItem.Text



        sqlstr5 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"




        upStat(sqlstr5)

        notes()
        loadItems()
        gridErja()



    End Sub

    Protected Sub btnHse_Click(sender As Object, e As EventArgs) Handles btnHse.Click
        Dim sqlstr, sqlstr2, sqlstr3, sqlstr4, sqlstr5, sqlstr6, sqlstr7, str As String
        Dim payam As String = ""

        payam = txtNazar.Text & "پرونده ارجاع شد به HSE"

        'str = "SELECT        TOP (200) Id, sender, action, reciver, finish         FROM workTableGuidTbl " & _
        '          " WHERE(sender = " & Session("omurID") & ") AND (action = 1)"

        'Dim dtt As New DataTable
        'dtt = ClsDataTable.GetDataTable(str)


        'Dim reciver As Integer = 0

        'If dtt.Rows.Count > 0 Then

        '    reciver = dtt.Rows(0).Item("reciver").ToString()
        'End If

        sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
                "VALUES        (" & Id.Text & ", " & Session("omurID") & ", " & 6 & ", '" & tarix & "', '" & sat & "', N'" & payam & "')"

        sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") "

        sqlstr6 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & "," & 6 & "," & Session("idOver") & ", 1)"

        sqlstr = "  UPDATE RegisterOverhauls  SET   State = " & 6 & "  WHERE (Id = " & Session("idOver") & ") "


        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"


        upStat(sqlstr4)
        upStat(sqlstr)
        upStat(sqlstr2)
        upStat(sqlstr3)

        upStat(sqlstr5)
        upStat(sqlstr6)
        upStat(sqlstr7)

        msg_box("ارسال شد")
        notes()
        loadItems()
        gridErja()
    End Sub
    Function GetSumHseByCode(code As String, tarix As String) As Integer
        Dim sqlstrc As String = "SELECT        shiftID, COUNT(shiftID) AS countt, tarix         FROM overhaulHseKelasTbl GROUP BY shiftID, tarix"
        Dim dtc As New DataTable
        dtc = ClsDataTable.GetDataTable(sqlstrc)

        For Each row As DataRow In dtc.Rows
            If row("shiftID").ToString().Trim() = code.Trim() And row("tarix").ToString().Trim() = tarix.Trim() Then
                Return Convert.ToInt32(row("countt"))
            End If
        Next
        Return 0 ' اگر کدی پیدا نشد
    End Function
    Protected Sub BtnHseSave_Click(sender As Object, e As EventArgs) Handles BtnHseSave.Click

        If GetSumHseByCode(cmbKelas.SelectedValue, txtDateKelas.Text) >= 100 Then
            msg_box("ظرفیت این ساعت تکمیل است=150")
            Exit Sub
        End If


        Dim sqlstrdel As String = "DELETE  FROM            overhaulHseKelasTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)

        Dim sqlstr As String = "INSERT INTO overhaulHseKelasTbl      (IdOver, tarix,shiftID,shifName)  VALUES        (" & Id.Text & ", '" & txtDateKelas.Text & "'," & cmbKelas.SelectedValue & ",N'" & cmbKelas.SelectedItem.Text & "')"
        upStat(sqlstr)


        Dim sqlstr1, sqlstr2, sqlstr3, sqlstr4, sqlstr5

        Dim payam As String = "لطفا در تاریخ: " & " " & txtDateKelas.Text & " " & cmbKelas.SelectedItem.Text & " " & "جهت تکمیل فرم حراست و شرکت در کلاس به پتروشیمی تبریز مراجعه نمایید.ضمنا قبل از مراجعه نسبت به ویرایش نهایی اطلاعات و بارگذاری مدارک لازم اقدام نمایید و اصل مدارک ثبت نامی و یک قطعه عکس به همراه داشته باشید"

        sqlstr1 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
               "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 15 , '" & tarix & "', '" & sat & "', N'تایید شد')"

        sqlstr2 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") and (omurId = " & Session("omurID") & ")"

        sqlstr3 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & ",15," & Session("idOver") & ", 1)"

        sqlstr4 = "  UPDATE RegisterOverhauls  SET   State = 15  WHERE (Id = " & Session("idOver") & ") "


        sqlstr5 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"


        Dim sqlstrSms As String = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
                     "VALUES     ('" & Mobile.Text & "', N'" & payam & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"
        upStat(sqlstrSms)

        upStat(sqlstr1)
        upStat(sqlstr2)
        upStat(sqlstr3)
        upStat(sqlstr4)
        upStat(sqlstr5)

        notes()
        loadItems()
        gridErja()

        msg_box("پرونده به ایمنی ارسال شد")
    End Sub

    Protected Sub btnEmeni_Click(sender As Object, e As EventArgs) Handles btnEmeni.Click
        Dim sqlstrdel As String = "DELETE  FROM            overhaulNomreTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)

        Dim sqlstr As String = "INSERT INTO overhaulNomreTbl     (IdOver, tarix,nomre)  VALUES        (" & Id.Text & ", '" & txtDate.Text & "'," & txtNomre.Text & ")"
        upStat(sqlstr)


        Dim sqlstr1 As String

        Dim payam As String = "نمره ایمنی: " & " " & txtNomre.Text

        'sqlstr1 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
        '       "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 33 , '" & tarix & "', '" & sat & "', N'" & payam & "')"

        'sqlstr2 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") and (omurId = " & Session("omurID") & ")"

        'sqlstr3 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & ",33," & Session("idOver") & ", 1)"

        'sqlstr4 = "  UPDATE RegisterOverhauls  SET   State = 33  WHERE (Id = " & Session("idOver") & ") "


        sqlstr1 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"



        upStat(sqlstr1)

        notes()
        loadItems()
        gridErja()

        msg_box("نمره ثبت شد")
    End Sub





    Protected Sub BtnHr_Click(sender As Object, e As EventArgs) Handles BtnHr.Click
        Dim sqlstrdel As String = "DELETE  FROM            overhaulCodepersoneliTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)

        Dim sqlstr As String = "INSERT INTO overhaulCodepersoneliTbl     (IdOver, codePersoneli)  VALUES        (" & Id.Text & ", " & codepersoneliTxt.Text & ")"
        upStat(sqlstr)


        Dim sqlstr1 As String

        Dim payam As String = "کد پرسنلی: " & " " & codepersoneliTxt.Text


        sqlstr1 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"



        upStat(sqlstr1)

        notes()
        loadItems()
        gridErja()

        msg_box("کد پرسنلی ثبت شد")
    End Sub

    Protected Sub btnExpire_Click(sender As Object, e As EventArgs) Handles btnExpire.Click
        Dim sqlstrdel As String = "DELETE  FROM            overhaulHerasatTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)

        Dim sqlstr As String = "INSERT INTO overhaulHerasatTbl     (IdOver, tarix)  VALUES        (" & Id.Text & ", '" & expiretxt.Text & "')"
        upStat(sqlstr)


        Dim sqlstr1 As String

        Dim payam As String = "تاریخ انقضا: " & " " & expiretxt.Text


        sqlstr1 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"



        upStat(sqlstr1)

        notes()
        loadItems()
        gridErja()

        msg_box("تاریخ انقضا ثبت شد")
    End Sub

    Protected Sub CheckBoxMadarek_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMadarek.CheckedChanged
        Dim sqlstrdel As String = "DELETE  FROM            overhalMadrakNagesTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)
        gridMadrakLoad()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sqlstr As String = "INSERT INTO overhalMadrakNagesTbl     (IdOver, tarix, madrakID, madrakName, saat,userID)  VALUES        (" & Id.Text & ", '" & tarix & "'," & cmbmadarek.SelectedValue & ",N'" & cmbmadarek.SelectedItem.Text & "','" & saat & "'," & Session("userid") & ")"
        upStat(sqlstr)
        gridMadrakLoad()


    End Sub
    Private Sub gridMadrakLoad()
        Dim dt2 As New DataTable

        Dim sqlstr As String = ""

        sqlstr = " SELECT        idDtl, IdOver, tarix, madrakID, madrakName, saat " & _
"FROM overhalMadrakNagesTbl WHERE        (IdOver = " & Id.Text & ") "

        dt2 = ClsDataTable.GetDataTable(sqlstr)
        If dt2.Rows.Count > 0 Then
            'PanelErja.Visible = True
            gridMadrak.DataSource = dt2
            gridMadrak.DataBind()
        End If


    End Sub
    Protected Sub gridMadrak_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridMadrak.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(gridMadrak.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = "delete from overhalMadrakNagesTbl where idDtl=" & radif
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            gridMadrakLoad()
        End If
    End Sub
    Protected Sub BtnStartWork_Click(sender As Object, e As EventArgs) Handles BtnStartWork.Click

        Dim sqlstrdel As String = "DELETE  FROM            overhaulStartDateTbl  WHERE        (IdOver = " & Id.Text & ")"
        upStat(sqlstrdel)

        Dim sqlstr As String = "INSERT INTO overhaulStartDateTbl     (IdOver, tarix)  VALUES        (" & Id.Text & ", '" & txtDate.Text & "')"
        upStat(sqlstr)


        Dim sqlstr1 As String = ""

        Dim payam As String = "با سلام لطفا در تاریخ: " & " " & txtDate.Text & " " & " از ساعت ٧.٣٠ (روزکار) ١٩.٣٠ (شبکار) با ثبت چهره در پتروشیمی تبریز شروع به کار نمایید."

        Dim sqlstr9 As String = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
             "VALUES     ('" & Mobile.Text & "', N'" & payam & "', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"

        upStat(sqlstr9)


        sqlstr1 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"


        upStat(sqlstr1)

        notes()
        loadItems()
        gridErja()

        msg_box("تاریخ شروع به کار ثبت شد")



    End Sub
End Class



