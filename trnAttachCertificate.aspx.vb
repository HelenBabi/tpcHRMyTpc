Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports Ionic.Zip


Partial Class trnAttachCertificate
    Inherits System.Web.UI.Page

    Protected Class CaptionInfo
        Public Property Caption As String
        Public Property Order As Integer
    End Class

    ' ==================== متدهای بارگذاری کمبوباکس (مثل shobeLoad2 شما) ====================

    Private Sub LoadGavahiType()
        'Dim sqlstr As String = "SELECT gavahi_type_id, gavahi_type_name FROM gavahi_type ORDER BY sort_order"
        'Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        'CmbGavahiType.DataSource = dt
        'CmbGavahiType.DataTextField = "gavahi_type_name"
        'CmbGavahiType.DataValueField = "gavahi_type_id"
        'CmbGavahiType.DataBind()

        'CmbGavahiType.Items.Insert(0, New ListItem("-- انتخاب نوع گواهی --", "0"))
    End Sub

    Private Sub LoadBank()
        'Dim sqlstr As String = "SELECT bank_id, bank_nam FROM bank ORDER BY bank_nam"
        'Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        'cmbBank.DataSource = dt
        'cmbBank.DataTextField = "bank_nam"
        'cmbBank.DataValueField = "bank_id"
        'cmbBank.DataBind()

        'cmbBank.Items.Insert(0, New ListItem("-- انتخاب بانک --", "0"))
    End Sub

    ' دقیقاً مثل shobeLoad2 خودتان
    Private Sub shobeLoad2()
        cmbShobe.Enabled = True

        Dim sqlstr As String = "SELECT shobe_id, shobe_nam, bank_id FROM shobe"
        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        cmbShobe.DataSource = dt
        cmbShobe.DataValueField = "shobe_id"
        cmbShobe.DataTextField = "shobe_nam"
        cmbShobe.DataBind()

        cmbShobe.Items.Insert(0, New ListItem("-- انتخاب شعبه --", "0"))
    End Sub

    Private Sub shobeLoadByBank(ByVal bankId As Integer)
        cmbShobe.Enabled = True

        Dim sqlstr As String = "SELECT shobe_id, shobe_nam, bank_id FROM shobe WHERE bank_id = {bankId}"
        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

        cmbShobe.DataSource = dt
        cmbShobe.DataValueField = "shobe_id"
        cmbShobe.DataTextField = "shobe_nam"
        cmbShobe.DataBind()

        cmbShobe.Items.Insert(0, New ListItem("-- انتخاب شعبه --", "0"))
    End Sub

    ' ==================== بارگذاری لیست پرسنل ====================

    Private Sub LoadPersonnelList()
        Dim query As String = "SELECT NationalCode, FullName FROM Personnel ORDER BY FullName"
        Dim dt As DataTable = ClsDataTable.GetDataTable(query)

        ddlPersonnel.DataSource = dt
        ddlPersonnel.DataTextField = "FullName"
        ddlPersonnel.DataValueField = "NationalCode"
        ddlPersonnel.DataBind()
        ddlPersonnel.Items.Insert(0, New ListItem("-- انتخاب پرسنل --", ""))
    End Sub

    ' جستجوی پرسنل
    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        Dim searchTerm As String = txtSearch.Text.Trim()
        Dim query As String = "SELECT NationalCode, FullName FROM Personnel WHERE FullName LIKE '%" & searchTerm & "%' OR NationalCode LIKE '%" & searchTerm & "%' ORDER BY FullName"
        Dim dt As DataTable = ClsDataTable.GetDataTable(query)

        ddlPersonnel.DataSource = dt
        ddlPersonnel.DataTextField = "FullName"
        ddlPersonnel.DataValueField = "NationalCode"
        ddlPersonnel.DataBind()
        ddlPersonnel.Items.Insert(0, New ListItem("-- انتخاب پرسنل --", ""))
    End Sub

    ' ==================== بارگذاری GridView (مثل loadGridSabege خودتان) ====================

    Private Sub loadGridSabege()
        Dim sqlstr As String = "SELECT * FROM sabegeVW WHERE person_cod = " & Session("codePersoneliii") & " ORDER BY reg_date DESC"

        Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)
        gridSabege.DataSource = dt
        gridSabege.DataBind()

        If dt.Rows.Count > 0 Then
            Try
                'pishnahaddahande.Text = dt.Rows(0).Item("personell").ToString()
                'person_cod.Text = dt.Rows(0).Item("person_cod").ToString()
                txtMozd.Text = dt.Rows(0).Item("mozd").ToString()
                'contracttyp.Text = dt.Rows(0).Item("value").ToString()
                hdnType.Value = dt.Rows(0).Item("typ").ToString()
            Catch ex As Exception
                ' بعضی از فیلدها ممکن است وجود نداشته باشند
            End Try
        End If

        ' بارگذاری اطلاعات برای ویرایش (stat = 6)
        Dim sqlstr2 As String = "SELECT moassese, gavahi_typ, bank_id, shobe_id, motafarege_nam, new_later_num " &
                                 "FROM sabegeVW WHERE person_cod = " & Session("codePersoneliii") & " AND stat = 6"

        Dim dt2 As DataTable = ClsDataTable.GetDataTable(sqlstr2)
        If dt2.Rows.Count > 0 Then
            txtGirande.Text = dt2.Rows(0).Item("motafarege_nam").ToString()
            CmbGavahiType.SelectedValue = dt2.Rows(0).Item("gavahi_typ").ToString()
            HiddenFieldlater_num.Value = dt2.Rows(0).Item("new_later_num").ToString()
            txtSazman.Text = dt2.Rows(0).Item("moassese").ToString()

            Dim bankId As Integer = Val(dt2.Rows(0).Item("bank_id").ToString())
            If bankId > 0 Then
                cmbBank.SelectedValue = bankId.ToString()
                shobeLoadByBank(bankId)
                cmbShobe.SelectedValue = dt2.Rows(0).Item("shobe_id").ToString()
            End If
        End If
    End Sub

    ' ==================== متد ثبت (دقیقاً مثل save_final شما) ====================

    Private Sub save_final()
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn()
        cnn.Open()

        Try
            Dim cmm As New SqlCommand
            cmm.CommandText = "newGavahi2Prc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@new_later_num", SqlDbType.Int, 6)
            cmm.Parameters("@new_later_num").Value = Val(HiddenFieldlater_num.Value)

            cmm.Parameters.Add("@certifi_typ", SqlDbType.Int, 5)
            cmm.Parameters("@certifi_typ").Value = Val(CmbGavahiType.SelectedValue)

            cmm.Parameters.Add("@moassese", SqlDbType.NVarChar, 150)
            cmm.Parameters("@moassese").Value = txtSazman.Text

            cmm.Parameters.Add("@motafarege_nam", SqlDbType.NVarChar, 50)
            cmm.Parameters("@motafarege_nam").Value = txtGirande.Text

            cmm.Parameters.Add("@naxales", SqlDbType.NVarChar, 50)
            cmm.Parameters("@naxales").Value = Val(txtMozd.Text)

            ' شرط مانند کد خودتان
            If Val(CmbGavahiType.SelectedValue) = 6 Or Val(CmbGavahiType.SelectedValue) = 8 Or Val(CmbGavahiType.SelectedValue) = 10 Then
                cmm.Parameters.Add("@bank_nam", SqlDbType.NVarChar, 50).Value = ""
                cmm.Parameters.Add("@shobe_nam", SqlDbType.NVarChar, 50).Value = ""
                cmm.Parameters.Add("@bank_id", SqlDbType.Int, 5).Value = 1
                cmm.Parameters.Add("@shobe_id", SqlDbType.Int, 5).Value = 1
            Else
                cmm.Parameters.Add("@bank_nam", SqlDbType.NVarChar, 50).Value = cmbBank.SelectedItem.ToString()
                cmm.Parameters.Add("@shobe_nam", SqlDbType.NVarChar, 50).Value = cmbShobe.SelectedItem.ToString()
                cmm.Parameters.Add("@bank_id", SqlDbType.Int, 5).Value = Val(cmbBank.SelectedValue)
                cmm.Parameters.Add("@shobe_id", SqlDbType.Int, 5).Value = Val(cmbShobe.SelectedValue)
            End If

            cmm.Parameters.Add("@reg_user_id", SqlDbType.Int, 5)
            cmm.Parameters("@reg_user_id").Value = Session("userid")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output

            cmm.Connection = cnn
            cmm.ExecuteNonQuery()

            Dim result As Integer = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("ثبت شد")
                loadGridSabege()
                printt()
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If

        Catch ex As Exception
            msg_box(ex.Message.ToString())
        Finally
            cnn.Close()
        End Try
    End Sub

    ' ==================== نمایش مدارک (مانند کد قبلی) ====================

    Private Sub LoadImages(nationalCode As String)
        Dim folder As String = "~/Attachment/" & nationalCode & "/"
        Dim folderPath As String = Server.MapPath(folder)

        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
            RepeaterImages.DataSource = Nothing
            RepeaterImages.DataBind()
            divNoData.Visible = True
            Exit Sub
        End If

        Dim files = Directory.GetFiles(folderPath).ToList()

        Dim captions As New Dictionary(Of String, CaptionInfo) From {
            {"IdCart.jpg", New CaptionInfo With {.Caption = "کارت ملی", .Order = 2}},
            {"PersonalPic.jpg", New CaptionInfo With {.Caption = "عکس پرسنلی", .Order = 1}},
            {"ShenasOne.jpg", New CaptionInfo With {.Caption = "صفحه اول شناسنامه", .Order = 3}},
            {"ShanasDesc.jpg", New CaptionInfo With {.Caption = "صفحه توضیحات شناسنامه", .Order = 4}},
            {"LastCertificate.jpg", New CaptionInfo With {.Caption = "مدرک تحصیلی", .Order = 5}},
            {"BackgrounRecord.jpg", New CaptionInfo With {.Caption = "گواهی عدم سوءپیشینه", .Order = 6}},
            {"MilitaryCardType.jpg", New CaptionInfo With {.Caption = "گواهی خدمت یا معافیت", .Order = 7}},
            {"MarriagePage.jpg", New CaptionInfo With {.Caption = "صفحه ازدواج شناسنامه", .Order = 10}},
            {"ChildrenPage.jpg", New CaptionInfo With {.Caption = "صفحه فرزندان شناسنامه", .Order = 11}},
            {"100_health.jpg", New CaptionInfo With {.Caption = "مدرک HSE", .Order = 8}},
            {"100_checkup.jpg", New CaptionInfo With {.Caption = "مدرک HSE پشت", .Order = 9}}
        }

        Dim images = files.Select(Function(f)
                                      Dim fileName = Path.GetFileName(f)
                                      Dim info As CaptionInfo = Nothing
                                      If captions.TryGetValue(fileName, info) Then
                                      Else
                                          info = New CaptionInfo With {.Caption = fileName, .Order = 999}
                                      End If
                                      Return New With {
                                          .Url = ResolveUrl(folder & fileName),
                                          .Name = fileName,
                                          .Caption = info.Caption,
                                          .SortOrder = info.Order,
                                          .Nat = nationalCode
                                      }
                                  End Function).OrderBy(Function(x) x.SortOrder).ToList()

        RepeaterImages.DataSource = images
        RepeaterImages.DataBind()
        divNoData.Visible = (images.Count = 0)
    End Sub

    ' ==================== متدهای کمکی شما ====================

    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            cnn.ConnectionString = ClsConnection.GetConn()
            cnn.Open()
            Dim cmm As New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
    End Sub

    Private Sub printt()
        ' کد چاپ شما
    End Sub

    ' ==================== رویدادها ====================

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        
    End Sub

    Protected Sub ddlPersonnel_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(ddlPersonnel.SelectedValue) Then
            Dim nationalCode As String = ddlPersonnel.SelectedValue
            ViewState("CurrentNationalCode") = nationalCode
            LoadImages(nationalCode)
            PanelAttach.Visible = True
            PanelUpload.Visible = True
        Else
            PanelAttach.Visible = False
            PanelUpload.Visible = False
        End If
    End Sub

    Protected Sub CmbGavahiType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' در صورت نیاز
    End Sub

    Protected Sub cmbBank_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim bankId As Integer = Val(cmbBank.SelectedValue)
        If bankId > 0 Then
            shobeLoadByBank(bankId)
        Else
            shobeLoad2()
        End If
    End Sub

    Protected Sub btnSaveGavahi_Click(sender As Object, e As EventArgs)
        save_final()
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        HiddenFieldlater_num.Value = "0"
        txtSazman.Text = ""
        txtGirande.Text = ""
        txtMozd.Text = ""
        CmbGavahiType.SelectedIndex = 0
        cmbBank.SelectedIndex = 0
        shobeLoad2()
    End Sub

    Protected Sub gridSabege_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gridSabege.PageIndex = e.NewPageIndex
        loadGridSabege()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Dim nationalCode As String = ViewState("CurrentNationalCode")

        If String.IsNullOrEmpty(nationalCode) Then
            msg_box("لطفا ابتدا یک پرسنل انتخاب کنید.")
            Return
        End If

        If fileUpload.HasFile Then
            Dim originalFileName As String = Path.GetFileName(fileUpload.FileName)
            Dim extension As String = Path.GetExtension(originalFileName).ToLower()

            Dim finalFileName As String = ""
            If ddlDocumentType.SelectedValue <> "" Then
                finalFileName = ddlDocumentType.SelectedValue
            ElseIf Not String.IsNullOrEmpty(txtCustomCaption.Text) Then
                finalFileName = txtCustomCaption.Text.Replace(" ", "_") & extension
            Else
                finalFileName = originalFileName
            End If

            Dim folder As String = "~/Attachment/" & nationalCode & "/"
            Dim folderPath As String = Server.MapPath(folder)

            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim savePath As String = Path.Combine(folderPath, finalFileName)

            If File.Exists(savePath) Then
                msg_box("فایلی با این نام قبلاً آپلود شده است.")
                Return
            End If

            fileUpload.SaveAs(savePath)
            msg_box("مدرک با موفقیت آپلود شد.")

            ddlDocumentType.SelectedIndex = 0
            txtCustomCaption.Text = ""
            LoadImages(nationalCode)
        Else
            msg_box("لطفاً یک فایل انتخاب کنید.")
        End If
    End Sub

    Protected Sub btnDelete_Command(sender As Object, e As CommandEventArgs)
        Dim fileName As String = e.CommandArgument.ToString()
        Dim nationalCode As String = ViewState("CurrentNationalCode")

        If Not String.IsNullOrEmpty(nationalCode) AndAlso Not String.IsNullOrEmpty(fileName) Then
            Dim folderPath As String = Server.MapPath("~/Attachment/" & nationalCode & "/")
            Dim filePath As String = Path.Combine(folderPath, fileName)

            If File.Exists(filePath) Then
                File.Delete(filePath)
                msg_box("مدرک با موفقیت حذف شد.")
            Else
                msg_box("فایل مورد نظر یافت نشد.")
            End If

            LoadImages(nationalCode)
        End If
    End Sub

    Protected Sub btnDownloadZip_Click(sender As Object, e As EventArgs)
        Dim nationalCode As String = ViewState("CurrentNationalCode")

        If String.IsNullOrEmpty(nationalCode) Then
            msg_box("لطفا ابتدا یک پرسنل انتخاب کنید.")
            Return
        End If

        Dim folderPath As String = Server.MapPath("~/Attachment/" & nationalCode & "/")

        If Not Directory.Exists(folderPath) Then
            msg_box("هیچ مدرکی برای این پرسنل یافت نشد.")
            Return
        End If

        Dim files = Directory.GetFiles(folderPath).ToList()
        If files.Count = 0 Then
            msg_box("هیچ مدرکی برای دانلود وجود ندارد.")
            Return
        End If

        Response.Clear()
        Response.BufferOutput = False
        Response.ContentType = "application/zip"
        Response.AddHeader("content-disposition", "attachment; filename=" & nationalCode & "_Documents.zip")

        Using zip As New ZipFile()
            zip.AlternateEncodingUsage = ZipOption.AsNecessary
            zip.AlternateEncoding = System.Text.Encoding.UTF8
            For Each path In files
                If File.Exists(path) Then
                    zip.AddFile(path, "")
                End If
            Next
            zip.Save(Response.OutputStream)
        End Using

        Response.Flush()
        Response.End()
    End Sub

End Class