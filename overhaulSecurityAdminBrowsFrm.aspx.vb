Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports CrystalDecisions.CrystalReports.Engine


Partial Class overhaulSecurityAdminBrowsFrm

    Inherits System.Web.UI.Page
    Dim tarix As String = ClsDateTime.GetDateNow
    Dim saat As String = ClsDateTime.GetDateNow & " " & ClsDateTime.GetTimeNow

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            ' Session("UserId") = 1
            meno_acc()

        End If
        load_gridAll()
    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("overhaulSecurityAdminBrowsFrm.aspx", Val(Session("UserId")))
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

    Protected Sub btnPrintSelected_Click(sender As Object, e As EventArgs) Handles btnPrintSelected.Click
        ' دریافت رکوردهای انتخاب‌شده از ASPxGridView
        Dim selectedIds = GridWorkTable.GetSelectedFieldValues("Id")


        If selectedIds.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('لطفاً حداقل یک نفر را انتخاب کنید');", True)
            Return
        End If

        ' ساخت لیست کارت‌ها
        Dim cardList As New List(Of CardData)()
        For Each id As Object In selectedIds
            ' گرفتن داده‌های پرسنل از پایگاه داده یا منبع اصلی
            Dim personnelId As Integer = Convert.ToInt32(id)
            Dim dr As DataRow = GetPersonnelDataById(personnelId)
            ' تابع دلخواه برای دریافت اطلاعات
            If dr IsNot Nothing Then
                cardList.Add(New CardData With {
                    .FullName = dr("fulname").ToString(),
                    .PersonnelCode = dr("CodePersoneli").ToString(),
                    .PhotoPath = dr("PhotoPath").ToString(),
                    .ExpirationDate = dr("ExpireDateCard").ToString(),
                    .shift = dr("shift").ToString(),
                      .omur = dr("omur").ToString()
                })
            End If
        Next

        'If cardList.Count = 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('لطفاً حداقل یک نفر را انتخاب کنید');", True)
        '    Return
        'End If
        ' بعد از این خط:
        ' If cardList.Count = 0 Then ...

        ' تبدیل List(Of CardData) به DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("fulname", GetType(String))
        dt.Columns.Add("codePersoneli", GetType(String))
        dt.Columns.Add("PhotoPath", GetType(String))
        dt.Columns.Add("Shift", GetType(String))
        dt.Columns.Add("expireDateCard", GetType(String))
        dt.Columns.Add("omur", GetType(String))

        For Each c As CardData In cardList
            dt.Rows.Add(c.FullName, c.PersonnelCode, c.PhotoPath, c.shift, c.ExpirationDate, c.omur)
        Next

        Session("CardDataTable") = dt
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openPopup",
            "window.open('PrintCards.aspx','_blank','width=800,height=600');", True)
    End Sub

    Private Function GetPersonnelDataById(id As Integer) As DataRow
        Dim dt As New DataTable
        Dim sqlstr As String = "SELECT * FROM overhaulHerastVW WHERE omurID = " & Session("omurID") & " AND id = " & id


        dt = ClsDataTable.GetDataTable(sqlstr)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function




    Private Sub load_gridAll()


        Dim dt As New DataTable
        Dim sqlstr As String = ""
        'If Session("unitID") = 19 Or Session("unitID") = 15 Then
        '    sqlstr = "SELECT * from overhaulAllVW"
        'Else
        sqlstr = "SELECT * from  overhaulAllVW WHERE        (codePersoneli IS NOT NULL) "
        'End If


        dt = ClsDataTable.GetDataTable(sqlstr)
        GridWorkTable.DataSource = dt
        GridWorkTable.DataBind()

        ' CntTyp()
        Dim cntt As Integer = dt.Rows.Count

    End Sub
    Protected Sub GridWorkTable_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles GridWorkTable.HtmlRowPrepared
        If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
            Return
        End If
        Dim value As Decimal = CDec(e.GetValue("State"))
        If value = 1 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightGreen
        ElseIf value = 2 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = Drawing.Color.LightYellow
        ElseIf value = 5 Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.BackColor = System.Drawing.Color.LightPink

        End If


    End Sub
    Protected Sub GridWorkTable_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridWorkTable.RowCommand


        If e.CommandArgs.CommandName = "moshahede" Then
            'Session("fromTyp") = "admn"
            Session("idOver") = e.CommandArgs.CommandArgument
            Response.Redirect("overhaulDetail.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ibtnxl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnxl.Click
        grvExporter1.WriteXlsxToResponse()
    End Sub

    Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        Dim selectedIds = GridWorkTable.GetSelectedFieldValues("Id")

        If selectedIds.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('لطفاً حداقل یک نفر را انتخاب کنید');", True)
            Return
        End If

        ' فراخوانی متد آپدیت وضعیت در دیتابیس
        UpdatePrintStatus(selectedIds)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('پرونده های انتخاب شده به برنامه ریزی تعمیرات ارسال گردید');", True)
    End Sub

    Private Sub UpdatePrintStatus(selectedIds As IList)
        Dim idsCsv As String = String.Join(",", selectedIds.Cast(Of Integer)())
        Dim sql As String = "UPDATE RegisterOverhauls SET State = 103 WHERE Id IN (" & idsCsv & ")"
        upStat(sql)

        Dim sqlstr2, sqlstr4, sqlstr6, sqlstr7 As String
        Dim payam As String = "تایید شد"

        For Each id As Object In selectedIds
            ' گرفتن داده‌های پرسنل از پایگاه داده یا منبع اصلی
            Dim personnelId As Integer = Convert.ToInt32(id)
          
                sqlstr7 = "INSERT INTO laterReciverTbl       (laterID, sender, reciverID, tarix, saat, matnErja)" & _
                 "VALUES        (" & personnelId & ", " & Session("omurID") & ", 103 , '" & tarix & "', '" & saat & "', N'" & payam & "')"

                sqlstr4 = " delete overhaulKartablTbl  where (overhaulId = " & personnelId & ") "

                sqlstr6 = "INSERT   INTO          overhaulKartablTbl( sender, omurId, overhaulId, kartablStat)         VALUES(" & Session("omurID") & ",103 ," & personnelId & ", 1)"

                sqlstr2 = "INSERT INTO    overhaulCommentTbl ( IdOverhaul, notee, PerID, zaman) " & _
                            " VALUES     (" & personnelId & ", N'" & payam & "', " & Session("userid") & ", '" & tarix & "')"


                upStat(sqlstr4)

                upStat(sqlstr2)


                upStat(sqlstr6)
                upStat(sqlstr7)

        Next

        load_gridAll()

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

End Class
