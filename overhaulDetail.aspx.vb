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



Partial Class overhaulDetail
    Inherits System.Web.UI.Page
    Dim dtTicket As New DataTable
    Dim sat As String = ClsDateTime.GetTimeNow
    Dim tarix As String = ClsDateTime.GetDateNow

  


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim strs As String = ""
        strs = "SELECT        idDtl, sender, omurId, overhaulId, kartablStat         FROM overhaulKartablTbl " & _
                  " WHERE(omurId = " & Session("omurID") & ")"

        Dim dtts As New DataTable
        dtts = ClsDataTable.GetDataTable(strs)

        Dim senderr As Integer = 0

        If dtts.Rows.Count > 0 Then

            senderr = dtts.Rows(0).Item("sender").ToString()
        End If

        cmbRunevesht.SelectedValue = senderr


        loadItems()
        'PersonImage.ImageUrl = "~/Attachment/" & Nationality.Text & "/PersonalPic.jpg"

        If Not Page.IsPostBack Then
            LoadImages()
            loadItems()
            loadcmbParaf()

            PanelRunevest.Visible = True
            loadComboRuneveshtTyp()
            load_cmbRuneveshtUsers()

        End If

        gridErja()
        notes()


        ' برنامه ریزی
        '       If Session("omurID") = 19 Then
        '           PanelRunevest.Visible = True

        '           ' پیمانکار
        '       ElseIf Session("omurID") = 13 Then

        '           PanelRunevest.Visible = False

        '       Else
        '           ' واحدها
        '           PanelRunevest.Visible = False

        '       End If


        Dim sqlstr As String = "SELECT        omurId, overhaulId " & _
    " FROM overhaulKartablTbl WHERE        (omurId = " & Session("omurID") & ") AND (overhaulId = " & Session("idOver") & ") AND ( kartablStat = 1 )"
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            BtnCancel.Visible = False
            btnOk.Visible = False

            PanelRunevest.Visible = True
            'CmbruneveshtType.Visible = False
            'lblruneveshtType.Visible = False
            'GridView2.Columns.Item(2).Visible = False
        Else

            BtnCancel.Visible = True
            btnOk.Visible = True

            PanelRunevest.Visible = False
        End If
       


        '       Dim sqlstr2 As String = "SELECT        omurId, overhaulId " & _
        '  " FROM overhaulKartablTbl WHERE        (omurId = 13) AND (overhaulId = " & Session("idOver") & ") AND ( kartablStat = 0 )"
        '       Dim dt2 As New DataTable
        '       dt2 = ClsDataTable.GetDataTable(sqlstr2)
        '       If dt2.Rows.Count > 0 Then
        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           'PanelRunevest.Visible = True
        '           PanelRunevest.Visible = False
        '           CmbruneveshtType.Visible = False
        '           lblruneveshtType.Visible = False
        '           GridView2.Columns.Item(2).Visible = False
        '       End If

        '       Dim sqlstr3 As String = "SELECT        omurId, overhaulId " & _
        ' " FROM overhaulKartablTbl WHERE        (omurId = 19) AND (overhaulId = " & Session("idOver") & ") AND ( kartablStat = 1 )"
        '       Dim dt3 As New DataTable
        '       dt3 = ClsDataTable.GetDataTable(sqlstr3)
        '       If dt3.Rows.Count > 0 And Session("omurID") = 19 Then
        '           BtnCancel.Visible = True
        '           btnOk.Visible = True
        '           PanelRunevest.Visible = True
        '       ElseIf dt3.Rows.Count > 0 And Session("omurID") = 13 Then
        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           PanelRunevest.Visible = False
        '       End If

        '       Dim sqlstr6 As String = "SELECT        omurId, overhaulId " & _
        '" FROM overhaulKartablTbl WHERE        (omurId = 19) AND (overhaulId = " & Session("idOver") & ") AND ( kartablStat = 0 )"
        '       Dim dt6 As New DataTable
        '       dt6 = ClsDataTable.GetDataTable(sqlstr6)
        '       If dt6.Rows.Count > 0 And Session("omurID") = 19 Then
        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           PanelRunevest.Visible = True
        '       ElseIf dt6.Rows.Count > 0 And Session("omurID") = 13 Then
        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           PanelRunevest.Visible = False
        '       End If


        '       Dim sqlstr4 As String = "SELECT        laterID  FROM            laterReciverTbl WHERE        (laterID = " & Session("idOver") & ")"
        '       Dim dt4 As New DataTable
        '       dt4 = ClsDataTable.GetDataTable(sqlstr4)
        '       If dt4.Rows.Count > 0 And Session("omurID") = 13 Then
        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           'PanelRunevest.Visible = True
        '       End If

        '       Dim sqlstr5 As String = "SELECT        omurId, overhaulId " & _
        ' " FROM overhaulKartablTbl WHERE        (omurId <> 19) and (omurId <> 13) and (overhaulId = " & Session("idOver") & ") "
        '       Dim dt5 As New DataTable
        '       dt5 = ClsDataTable.GetDataTable(sqlstr5)
        '       If dt5.Rows.Count = 0 And Session("omurID") = 13 Then
        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           PanelRunevest.Visible = False
        '       ElseIf dt5.Rows.Count = 0 And Session("omurID") = 19 Then

        '           BtnCancel.Visible = False
        '           btnOk.Visible = False
        '           PanelRunevest.Visible = True
        '       Else
        '           BtnCancel.Visible = True
        '           btnOk.Visible = True
        '           PanelRunevest.Visible = False
        '       End If
    End Sub
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim sqlstr, sqlstr2, sqlstr3, sqlstr4, sqlstr5, sqlstr6, sqlstr7, str As String

    

        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

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

        sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") and (omurId = " & Session("omurID") & ")"

        sqlstr6 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & "," & reciver & "," & Session("idOver") & ", 1)"

        sqlstr = "  UPDATE RegisterOverhauls  SET   State = " & Session("omurID") & "  WHERE (Id = " & Session("idOver") & ") "


        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"

        ''تایید برنامه ریزی
        'If Session("omurID") = 19 Then
        '    sqlstr = "  UPDATE RegisterOverhauls  SET   State = 2  WHERE (Id = " & Session("idOver") & ") "

        '    sqlstr6 = "INSERT   INTO          overhaulKartablTbl(omurId, overhaulId, kartablStat)         VALUES(13," & Session("idOver") & ", 1)"

        '    sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
        '         "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 13, '" & tarix & "', '" & sat & "', N'" & payam & "')"

        '    'تایید پیمانکار
        'ElseIf Session("omurID") = 13 Then

        '    sqlstr = "  UPDATE RegisterOverhauls  SET   State = 1  WHERE (Id = " & Session("idOver") & ") "

        '    sqlstr3 = "INSERT   INTO          overhaulKartablTbl(omurId, overhaulId, kartablStat)         VALUES(19," & Session("idOver") & ", 1)"

        '    sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
        '           "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 19, '" & tarix & "', '" & sat & "', N'" & payam & "')"

        'Else
        '    'تایید واحدها
        '    sqlstr = "  UPDATE RegisterOverhauls  SET   State = 3  WHERE (Id = " & Session("idOver") & ") "

        '    sqlstr5 = "INSERT   INTO          overhaulKartablTbl(omurId, overhaulId, kartablStat)         VALUES(19," & Session("idOver") & ", 1)"

        '    sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
        '           "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 19, '" & tarix & "', '" & sat & "', N'" & payam & "')"

        'End If




        'sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
        '            " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"



            'Dim sqlstr8 As String = "SELECT       reciverID,sender,laterID  FROM            laterReciverTbl WHERE        (laterID = " & Session("idOver") & ") AND (reciverID = " & Session("omurID") & ")"
            'Dim dt8 As New DataTable
            'dt8 = ClsDataTable.GetDataTable(sqlstr8)
            'Dim reciver As Integer
            'If dt8.Rows.Count > 0 Then
            '    reciver = dt8.Rows(0).Item("sender").ToString()

            'Else
            '    reciver = 19

            'End If

            'sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
            '       "VALUES        (" & Id.Text & ", " & Session("omurID") & ", " & reciver & ", '" & tarix & "', '" & sat & "', N'" & payam & "')"




        'sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") and (omurId = " & Session("omurID") & ")"





            upStat(sqlstr4)
            upStat(sqlstr)
            upStat(sqlstr2)
            upStat(sqlstr3)

            upStat(sqlstr5)
            upStat(sqlstr6)
            upStat(sqlstr7)


            notes()
            loadItems()
            gridErja()

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        Dim sqlstr, sqlstr2, sqlstr3, sqlstr4, sqlstr6, sqlstr9 As String


        Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

        Dim payam As String = ""


        payam = txtNazar.Text & "  عدم تایید"


        ' برنامه ریزی
        If Session("omurID") = 19 Then
            sqlstr = "  UPDATE RegisterOverhauls  SET   State = -2  WHERE (Id = " & Session("idOver") & ") "

            sqlstr6 = "INSERT   INTO          overhaulKartablTbl(omurId, overhaulId, kartablStat)         VALUES(13," & Session("idOver") & ", 0)"

            sqlstr9 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
                 "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 13, '" & tarix & "', '" & sat & "', N'" & payam & "')"

            ' پیمانکار
        ElseIf Session("omurID") = 13 Then

            sqlstr = "  UPDATE RegisterOverhauls  SET   State = -1  WHERE (Id = " & Session("idOver") & ") "


        Else

            sqlstr = "  UPDATE RegisterOverhauls  SET   State = -3  WHERE (Id = " & Session("idOver") & ") "

            sqlstr6 = "INSERT   INTO          overhaulKartablTbl(omurId, overhaulId, kartablStat)         VALUES(19," & Session("idOver") & ", 0)"

            sqlstr9 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
                  "VALUES        (" & Id.Text & ", " & Session("omurID") & ", 19, '" & tarix & "', '" & sat & "', N'" & payam & "')"

            ' واحدها
          

        End If

        sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & Id.Text & ") and (omurId = " & Session("omurID") & ")"




        sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                    " VALUES     (" & Session("idOver") & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"

        sqlstr3 = "INSERT INTO smsTbl                  (mobile, message, tarix, saat)" & _
          "VALUES     ('" & Mobile.Text & "', N'تایید نشد', '" & ClsDateTime.GetDateNow() & "', '" & ClsDateTime.GetTimeNow() & "')"

        upStat(sqlstr4)
        upStat(sqlstr)
        upStat(sqlstr6)
        
        upStat(sqlstr2)
        upStat(sqlstr3)
        upStat(sqlstr9)
        notes()
        loadItems()
        gridErja()
    End Sub

    Private Sub LoadImages()
        Dim folder = "~/Attachment/" & Nationality.Text & "/"
        Dim folderPath = Server.MapPath(folder)
        Dim files = Directory.GetFiles(folderPath).Take(7).ToList()

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
        Dim files = Directory.GetFiles(folderPath).Take(7).ToList()

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
        sqlstr = "SELECT        Title + N' دستمزد روزانه '+ FORMAT(Price, 'N0') + N' ریال ' AS cahrt, chartId " & _
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
            shSh.Text = dt.Rows(0).Item("shSh")
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


        End If
    End Sub


    

    Protected Sub btnNazar_Click(sender As Object, e As EventArgs) Handles btnNazar.Click
        save_final()
        notes()
        txtNazar.Text = ""
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
        Session("idOver") = 0
        'If Session("from") = "user" Then
        '    Response.Redirect("sugMyWorkTableFrm.aspx")
        If Session("omurID") = 13 Then
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

            payam = txtNazar.Text & " پرونده ارجاع شد به " & cmbRunevesht.Text
            'End If

            Dim sqlstr As String = ""

            Dim sqlstr2 As String = ""

            Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

            'sqlstr = "  UPDATE RegisterOverhauls  SET   State = " & cmbRunevesht.Value & " WHERE (Id = " & Session("idOver") & ") "


            If Session("omurID") = 19 Then
                sqlstr = "  UPDATE RegisterOverhauls  SET   State = 199 WHERE (Id = " & Session("idOver") & ") "

            End If

            'If Session("omurID") = 13 Then
            '    sqlstr = "  UPDATE RegisterOverhauls  SET   State = 9  WHERE (Id = " & Session("idOver") & ") "

            'End If

            sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                        " VALUES     (" & Id.Text & ", N'" & payam & "', " & Session("userid") & ", '" & saat & "')"
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

        If Session("omurID") <> 19 And Session("omurID") <> 13 And Val(CmbruneveshtType.Value) = 0 Then

            msg_box("لطفا چارت پیشنهادی  را انتخاب نمایید")
            CmbruneveshtType.Focus()
            Exit Sub
        End If

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

 
End Class
