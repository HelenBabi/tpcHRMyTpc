Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Diagnostics
Imports DevExpress.Web.ASPxGridView


Partial Class bootTicketSubmit
    Inherits System.Web.UI.Page
    Dim dtSistem As New DataTable
    Dim az As String = ClsDateTime.GetDateNow
    Dim ta As String = ClsDateTime.GetDateNow
    Dim datS, dateE, datS1, dateE1 As String

    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

       
        listeEntezar()


        If Me.IsPostBack = False Then
            load_grid()
            'load_checkbox()
        End If

    End Sub
    Protected Sub grdFarei_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idd As Integer
        Dim grid As ASPxGridView = CType(sender, ASPxGridView)
        idd = grid.GetMasterRowFieldValues("SystemCode").ToString

        Dim ClsB_ As New ClsBrowsSystems
        Dim dt2 As New DataTable
        dt2 = ClsB_.GetAllBrowsSystemsDetail(idd)
        grid.DataSource = dt2
        'grid.DataBind()

    End Sub


    Protected Sub grdFarei_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("SystemHDtlStatus"))
        Dim gateID As Integer = CDec(e.GetValue("BaseSystemHDtlCode"))
        If gateID = 30 Or gateID = 28 Or gateID = 31 Then
            e.Row.BackColor = Drawing.Color.Yellow

        End If

        If value = 0 Then
            'e.Row.ForeColor = Drawing.Color.Green
            '   e.Row.BackColor = Drawing.Color.Aquamarine
        Else
            e.Row.BackColor = Drawing.Color.PaleVioletRed
            '   e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
        End If
    End Sub

    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then

            txtSystemCode.Text = e.CommandArgs.CommandArgument

        End If

        If e.CommandArgs.CommandName = "mogh" Then
          

            Dim sqlstr As String

            sqlstr = "  UPDATE tblSystem  SET   variance = 1  " & _
                                  " WHERE (SystemCode  = " & e.CommandArgs.CommandArgument & ")  "

            upStat(sqlstr)
            msg_box("مغایرت ثبت شد")
        End If

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
            cnn.close()
        End Try

    End Sub
    Private Sub load_grid()
        Dim dtSistem As DataTable
        Dim clssistem_ As New ClsSystem
        dtSistem = clssistem_.getallsystemfillgrdDomain(Session("codePersoneli"))
      
        If dtSistem.Rows.Count > 0 Then
            ASPxGrvHead.DataSource = dtSistem
            ASPxGrvHead.DataBind()
            txtSystemCode.Text = dtSistem.Rows(0).Item("SystemCode")
            Dim syccode As Integer = dtSistem.Rows(0).Item("SystemCode")
            HiddenFieldUserID.Value = dtSistem.Rows(0).Item("UserCode")
        Else
            msg_box("برای شمادسترسی به کامپیوتری ثبت نشده است،  با واحد کامپیوتر تماس بگیرید")
            'Label2.Text = "برای شمادسترسی به کامپیوتری ثبت نشده است ، با واحد کامپیوتر تماس بگیرید"

        End If

    End Sub



#Region "All pages"



    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

#End Region


   


    Private Sub save_final()

        If txtTelfon.Text = "" Then
            msg_box("تلفن داخلی خود را حتما باید بنویسید  ")
            txtTelfon.Focus()
            Exit Sub
        End If

        If txtRamz.Text = "" Then
            msg_box("رمز ورود به ویندوز را حتما باید وارد کنید ")
            txtRamz.Focus()
            Exit Sub
        End If
        If txt_ans.Text = "" Then
            ' If j = CheckBoxList1.Items.Count And txt = "" Then
            msg_box(" لطفا درخواست خود را  درقسمت توضیحات بنویسید ")
            txt_ans.Focus()
            'CheckBoxList1.Focus()
            Exit Sub
        End If
        ' txt_ans.Text = " ندارد "

        txt_ans.Text = txt_ans.Text


        Dim tiketid As Integer = ClsGetMaxId.GetMaxIdHD("tblHerja", "erja_id")

        'If hdn_Final_key.Value = "new" Then txtTicketId.Text = ClsGetMaxId.GetMaxId("tblHerja", "erja_id")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "prcTicketSubmit"
            cmm.CommandType = CommandType.StoredProcedure

            'cmm.Parameters.Add("@mojavez", SqlDbType.SmallInt)
            'cmm.Parameters("@mojavez").Value = 0

            cmm.Parameters.Add("@erja_id", SqlDbType.Int, 10)
            cmm.Parameters("@erja_id").Value = tiketid

            cmm.Parameters.Add("@buildingcode", SqlDbType.Int, 10)
            cmm.Parameters("@buildingcode").Value = 1

            cmm.Parameters.Add("@SystemCode", SqlDbType.Int, 10)
            cmm.Parameters("@SystemCode").Value = txtSystemCode.Text

            cmm.Parameters.Add("@notee", SqlDbType.NVarChar)
            cmm.Parameters("@notee").Value = txt_ans.Text.Trim
            'Dim txtrais As String = ""
            'If CmbWorkeType.SelectedValue = 1 Then
            '    txtrais = "مسئول سخت افزار واحد کامپیوتر"
            'Else
            '    txtrais = "رئیس واحد کامپیوتر"
            'End If

            'cmm.Parameters.Add("@MojavezNote", SqlDbType.NVarChar)
            'cmm.Parameters("@MojavezNote").Value = "  نیاز به مجوز دارد وبعد از تایید  " & txtrais & " انجام می شوند  "

            cmm.Parameters.Add("@Typestr", SqlDbType.VarChar, 20)
            cmm.Parameters("@Typestr").Value = 9


            cmm.Parameters.Add("@ipp", SqlDbType.VarChar, 50)
            cmm.Parameters("@ipp").Value = Request.UserHostAddress

            cmm.Parameters.Add("@telfon", SqlDbType.VarChar, 50)
            cmm.Parameters("@telfon").Value = txtTelfon.Text

            cmm.Parameters.Add("@Ramz", SqlDbType.NVarChar, 50)
            cmm.Parameters("@Ramz").Value = txtRamz.Text


            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = HiddenFieldUserID.Value
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''
            cmm.Parameters.Add("@showAll", SqlDbType.Int, 1)
            cmm.Parameters("@showAll").Value = 0

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("  درخواست با شماره پیگیری    " & tiketid & "   به واحد کامپیوتر ارسال شد")
                'Session("message") = "  درخواست با شماره پیگیری    " & tiketid & "   به واحد کامپیوتر ارسال شد"
                ' Session("tiketId") = tiketid
                ' Response.Redirect("messageFrm.aspx")
                'Response.Redirect("messageFrm.aspx")
                btnBrwsSys.Enabled = False
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub
    Private Sub CntTyp()
        Dim dt3 As DataTable
        Dim sqlstr, txtcnt As String
        txtcnt = ""
        sqlstr = "SELECT     tblWorkType.WorkTypeCode, tblWorkType.WorkTypeName, COUNT(tblHerja.Typestr) AS CountOfWorkTypeCode " & _
                    "FROM         tblHerja INNER JOIN " & _
                     " tblWorkType ON tblHerja.Typestr = tblWorkType.WorkTypeCode " & _
                     "GROUP BY tblWorkType.WorkTypeCode, tblWorkType.WorkTypeName, tblHerja.stat_ " & _
    "    HAVING(tblHerja.stat_ = 0)"


        dt3 = ClsDataTable.GetDataTableHD(sqlstr)

        Dim count_ As Integer = dt3.Rows.Count
        For i As Integer = 0 To count_ - 1
            txtcnt += dt3.Rows(i).Item("WorkTypeName").ToString
            txtcnt += " = "
            'If id_type = 1 Then
            '    txtcnt += "سرويس در كارگاه="
            'ElseIf id_type = 2 Then
            '    txtcnt += "ريموت="
            'ElseIf id_type = 3 Then
            '    txtcnt += "سرویس در محل="
            'ElseIf id_type = 4 Then
            '    txtcnt += "سرویس دوره ای="
            'End If
            txtcnt += dt3.Rows(i).Item("CountOfWorkTypeCode").ToString
            txtcnt += "  ;  "
        Next
        lblCnt.Text = "لیست انتظار: " & "   " & txtcnt
    End Sub


    Private Sub listeEntezar()
        Dim dt3 As DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     erja_id         FROM tblHerja      WHERE (stat_ <> 2 And stat_ <> 9) "

        dt3 = ClsDataTable.GetDataTableHD(sqlstr)

        lblCnt.Text = "  در حال حاضر  " & "   " & dt3.Rows.Count & "  درخواست، در لیست انتظار می باشد  "
    End Sub
















    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        save_final()
    End Sub

    Protected Sub btnWork_Click(sender As Object, e As EventArgs) Handles btnWork.Click
     
            Response.Redirect("index.aspx")

    End Sub
End Class
