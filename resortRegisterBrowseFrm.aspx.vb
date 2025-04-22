Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class resortRegisterBrowseFrm
    Inherits System.Web.UI.Page
  
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            'meno_acc()





        End If

        load_Mojavez()
        load_grid()
    End Sub

    Private Sub load_Mojavez()
        Dim dtt As New DataTable
        Dim sqlstrr As String = ""
        sqlstrr = "SELECT        isResort     FROM personel WHERE        (ID = " & Session("userid") & ") AND (isResort = 1) "
        dtt = ClsDataTable.GetDataTable(sqlstrr)
        If dtt.Rows.Count > 0 Then
            'Panel1.Enabled = True
        Else
            msg_box("شما مجوز درخواست مهمان ندارید در صورت نیاز با واحد حراست تماس بگبرید")
            ASPxGrvHead.Enabled = False
            ibtnxl.Enabled = False
        End If

    End Sub
    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles ASPxGrvHead.RowCommand

        If e.CommandArgs.CommandName = "xuruj" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     ResortID       FROM tblResort " & _
                  " WHERE     (stat = 3)  and (ResortID = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTableHerasat(sqlstr2)
            If dt3.Rows.Count > 0 Then
                msg_box("قبل از ثبت ورود توسط حراست شما نمی توانید  خروج ثبت کنید ")
                Exit Sub
            Else
                Session("fromTyp") = "regOut"
                Session("ResortID") = e.CommandArgs.CommandArgument
                Response.Redirect("resortRegisterDetail2Frm.aspx")
            End If


        End If
        If e.CommandArgs.CommandName = "vurud" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     ResortID       FROM tblResort " & _
                  " WHERE     (stat = 3)  and (ResortID = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTableHerasat(sqlstr2)
            If dt3.Rows.Count > 0 Then
                msg_box("قبل از ثبت ورود توسط حراست شما نمی توانید ورود  ثبت کنید ")
                Exit Sub
            Else
                Session("fromTyp") = "regin"
                Session("ResortID") = e.CommandArgs.CommandArgument
                Response.Redirect("resortRegisterDetail2Frm.aspx")
            End If


        End If
        If e.CommandArgs.CommandName = "Ebtall" Then
            Dim dt3 As DataTable
            Dim sqlstr2 As String

            sqlstr2 = "SELECT     ResortID       FROM tblResort " & _
                  " WHERE     (stat <> 3)  and (ResortID = " & e.CommandArgs.CommandArgument & ") "

            dt3 = ClsDataTable.GetDataTableHerasat(sqlstr2)
            If dt3.Rows.Count > 0 Then
                msg_box("ابطلال در این مرحله نامعتبر می باشد ")
                Exit Sub
            Else
                Dim sqlstr As String

                sqlstr = "  UPDATE tblResort  SET   stat = 6  " & _
                                      " WHERE (ResortID = " & e.CommandArgs.CommandArgument & ")  "

                upStat(sqlstr)
                load_grid()
            End If

           
        End If
    End Sub

    Private Sub upStat(sqlstr As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConnHerasat
            cnn.Open()
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
        Finally
            cnn.close()
        End Try

    End Sub

    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles ASPxGrvHead.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("stat"))
    '    If value = 1 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = Drawing.Color.LimeGreen

    '    ElseIf value = 0 Then
    '        e.Row.ForeColor = Drawing.Color.Black
    '        e.Row.BackColor = System.Drawing.Color.PaleVioletRed


    '    End If


    'End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("resortRegisterBrowseFrm.aspx", Val(Session("UserId")))
        If meno_arr(0) = False Then
            Response.Redirect("login_2.aspx")
        End If
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

#End Region


    
    

    Private Sub load_grid()
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        MizbanId, ResortID, moraje, datee, timee, codeCart, mizban, otag, mojavezNo, notee, dateEnd, timeEnd, karbar, Sepordevalue, stat, UnitsName, genValue, ellatName, outKarbar,statVal,tarix " & _
       " FROM morajeinVW " & _
              " WHERE        (MizbanId = " & Session("userid") & ") "



        'sqlstr = "SELECT     tblResort.MizbanId, tblResort.ResortID, tblResort.nam + ' ' + tblResort.lnam AS moraje, tblResort.datee, tblResort.timee, tblResort.codeCart, tblResort.mizban, " & _
        '            "  tblResort.otag, tblResort.mojavezNo, tblResort.notee, tblResort.dateEnd, tblResort.timeEnd,  " & _
        '             " tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar, tblSepordeType.Sepordevalue, tblResort.stat,  " & _
        '              " tblUnits.UnitsName, tblGenderTyp.genValue, tblEllatType.ellatName, " & _
        '              " tblUsers_1.UserLastName + ' ' + tblUsers_1.UserFamili AS outKarbar " & _
        '              " FROM         tblResort INNER JOIN " & _
        '              " tblUsers ON tblResort.userId = tblUsers.UserCode INNER JOIN " & _
        '            " tblSepordeType ON tblResort.sepordeTyp = tblSepordeType.SepordeCode INNER JOIN " & _
        '             " tblUnits ON tblResort.unitee = tblUnits.UnitsCode INNER JOIN " & _
        '               " tblGenderTyp ON tblResort.genderr = tblGenderTyp.genId INNER JOIN " & _
        '              " tblEllatType ON tblResort.ellat = tblEllatType.ellatCode INNER JOIN " & _
        '              " tblUsers AS tblUsers_1 ON tblResort.outUserId = tblUsers_1.UserCode " & _
        '             " WHERE  tblResort.MizbanId = " & Session("userid") & " and  tblResort.stat = 4  or tblResort.stat = 3   "

        dt = ClsDataTable.GetDataTableHerasat(sqlstr)
        ASPxGrvHead.DataSource = dt
        ASPxGrvHead.DataBind()
        ' CntTyp()
      
    End Sub
    




    Protected Sub ASPxGrvHead_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles ASPxGrvHead.AfterPerformCallback
        load_grid()
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click
        Response.Redirect("index.aspx")

    End Sub




End Class
