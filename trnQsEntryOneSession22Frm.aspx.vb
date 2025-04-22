Imports System.Data
Imports System.Data.SqlClient
Partial Class trnQsEntryOneSession22Frm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            'CmbUnit.Enabled = False
            DropDownList1.Enabled = False
            soalTxt.Enabled = False


            load_cmbDore()

            load_items()
            'load_cmbQsType()
            'load_grid_Sabage()
            'load_items()


            Session("doreID") = CmbUnit.SelectedValue
            load_cmbQsTyp()

        End If

    End Sub
    Private Sub load_cmbQsTyp()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        TOP (200) qSTypeID, qsTypeValue FROM            trnQsTypeTbl where qSTypeID= " & Request.QueryString("id") & ""
        dtm = ClsDataTable.GetDataTable(sqlstr)
        DropDownList1.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            DropDownList1.DataTextField = "qsTypeValue"
            DropDownList1.DataValueField = "qSTypeID"
            DropDownList1.DataBind()

        End If
    End Sub

    Private Sub load_items()

        Dim sqlstrH As String = "SELECT        trnDoreReqisterTbl.idd,trnBarnameRiziTbl.stat, trnQsBaseTbl.doreID  " & _
                   "FROM            trnDoreReqisterTbl INNER JOIN " & _
                        " trnBarnameRiziTbl ON trnDoreReqisterTbl.id_tour = trnBarnameRiziTbl.idd " & _
                        " INNER JOIN                         trnQsBaseTbl ON trnBarnameRiziTbl.idd = trnQsBaseTbl.doreID " & _
                 "WHERE(trnDoreReqisterTbl.hazf = 0) And (trnDoreReqisterTbl.codePersonel = " & Session("codePersoneli") & ") And (trnBarnameRiziTbl.hozuri = 2) and (trnQsBaseTbl.qsType = " & Request.QueryString("id") & ") and (trnQsBaseTbl.doreID = " & CmbUnit.SelectedValue & ")"
        'And (trnBarnameRiziTbl.stat <> 13)"
        Dim dtH As New DataTable
        dtH = ClsDataTable.GetDataTable(sqlstrH)
        If dtH.Rows.Count > 0 Then
            If dtH.Rows(0).Item("stat").ToString = 13 Then
                msg_box("دوره به اتمام رسیده است")
                Panel1.Enabled = False

                'Exit Sub

            End If
            Panel1.Visible = True
            Dim dtm As New DataTable


            Dim sqlstr As String
            sqlstr = "SELECT        *       FROM trnQsdetailVW WHERE        (codePersonel = " & Session("codePersoneli") & ")  and (doreID = " & CmbUnit.SelectedValue & ") and (qsType = " & Request.QueryString("id") & ") ORDER BY doreID DESC  "
            ' AND (qsType = 1) "

            dtm = ClsDataTable.GetDataTable(sqlstr)
            Dim cnt As Integer = dtm.Rows.Count




            If cnt > 0 Then
                Panel2.Visible = True

                Dim dtkk As New DataTable
                Dim sqlstkk As String
                'sqlstkk = "SELECT        *       FROM trnQsResponseVW WHERE        (codePersoneli = " & Session("codePersoneli") & ") and (doreID = " & CmbUnit.SelectedValue & ") and (qsType = " & Request.QueryString("id") & ")"
                sqlstkk = "SELECT        TOP (200) dbo.trnQsResponseTbl.codePersoneli  " & _
                   "FROM            dbo.trnQsResponseTbl INNER JOIN  " & _
                        " dbo.trnQsBaseTbl ON dbo.trnQsResponseTbl.qsID = dbo.trnQsBaseTbl.qsID  " & _
                    "WHERE        (dbo.trnQsResponseTbl.codePersoneli = " & Session("codePersoneli") & ") AND (dbo.trnQsBaseTbl.doreID = " & CmbUnit.SelectedValue & ") AND (dbo.trnQsBaseTbl.qsType = " & Request.QueryString("id") & ")"
                dtkk = ClsDataTable.GetDataTable(sqlstkk)
                Dim conter As Integer = 1
                If dtkk.Rows.Count > 0 Then
                    conter = dtkk.Rows.Count + 1
                End If

                Dim dtq As New DataTable
                Dim sqlstrq As String = "SELECT        TOP (200) qsID, doreID, qsValue, qsType  " & _
                "FROM trnQsBaseTbl WHERE        (doreID = " & CmbUnit.SelectedValue & ") AND (qsType = " & Request.QueryString("id") & ")"
                dtq = ClsDataTable.GetDataTable(sqlstrq)

                lblSoal.Text = " سوال " & conter & " از  " & dtq.Rows.Count

                'CmbUnit.SelectedValue = dtm.Rows(0).Item("doreID").ToString
                DropDownList1.SelectedValue = dtm.Rows(0).Item("qsType").ToString

                soalTxt.Text = dtm.Rows(0).Item("qsValue").ToString
                hdn_QsID.Value = dtm.Rows(0).Item("qsID").ToString


                Dim dtr As New DataTable
                Dim sqlstrr As String = "SELECT         dtlID, qsID, optionn, answer " & _
               "  FROM trnQsDtlTbl WHERE        (qsID = " & hdn_QsID.Value & ") ORDER BY qsID"
                dtr = ClsDataTable.GetDataTable(sqlstrr)
                RadioButtonList1.DataSource = dtr
                RadioButtonList1.DataTextField = "optionn"
                RadioButtonList1.DataValueField = "dtlID"
                RadioButtonList1.DataBind()

            Else
                'msg_box("شما  سوالات تایین سطح را پاسخ داده اید")
                Dim dtk As New DataTable
                Dim sqlstk As String
                ' sqlstk = "SELECT        *       FROM trnQsWorkBookVW WHERE       (codePersoneli = " & Session("codePersoneli") & ") and (doreID = " & CmbUnit.SelectedValue & ") ORDER BY doreID DESC "
                sqlstk = "select * from trnQsResponse1Vw where  (codePersonel = " & Session("codePersoneli") & ") and (id_tour = " & CmbUnit.SelectedValue & ") and (answer = 1)"
                dtk = ClsDataTable.GetDataTable(sqlstk)

                'Panel1.Visible = False
                Button1.Visible = False
                lblPayam.Visible = True

                Dim jamBarom As Integer = dtH.Rows.Count
                Dim nomre As Integer = dtk.Rows.Count

                'lblPayam.Text = "شما  سوالات  را پاسخ داده اید"

                btnWorkBook.Visible = True


                lblPayam.Text = lblPayam.Text & " شما  " & nomre & "  از  " & jamBarom & " سوال را صحیح جواب داده اید  "
                Panel2.Visible = False

                'btnB.Visible = True

                'Dim siDarsad As Integer = jamBarom * 30 / 100
                'Dim panjahDarsad As Integer = jamBarom * 50 / 100

                'lblPayam2.Visible = True

                'If nomre <= siDarsad Then
                Dim sqlc As String = "UPDATE        trnDoreReqisterTbl SET                teacherScore = " & nomre & "  WHERE        (codePersonel = " & Session("codePersoneli") & ") AND (id_tour = " & CmbUnit.SelectedValue & ")"
                upStat(sqlc)

                '    'lblPayam2.Text = "سطح شما C ارزیابی شد "



                'End If

                'If nomre >= panjahDarsad Then
                '    Dim sqlb As String = "UPDATE        trnDoreReqisterTbl SET                teacherScore = " & nomre & "  WHERE        (codePersonel = " & Session("codePersoneli") & ") AND (id_tour = " & CmbUnit.SelectedValue & ")"
                '    upStat(sqlb)

                '    'lblPayam2.Text = "سطح شما B ارزیابی شد"


                'End If

                'If nomre > panjahDarsad Then
                '    Dim sqla As String = "UPDATE        trnDoreReqisterTbl SET                teacherScore = 7  WHERE        (codePersonel = " & Session("codePersoneli") & ") AND (id_tour = " & CmbUnit.SelectedValue & ")"
                '    upStat(sqla)

                '    'lblPayam2.Text = "سطح شما A ارزیابی شد"

                'End If

            End If


        Else
            'msg_box("شما در دوره غیرحضوری ثبت نام نکرده اید")
            'Panel1.Visible = False
            Exit Sub
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
            msg_box(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try

    End Sub
    Private Sub load_cmbDore()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        *        FROM trnQsCmbDoreVW WHERE        (codePersonel = " & Session("codePersoneli") & ")"
        'WHERE        (hozuri = 0) AND (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbUnit.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            CmbUnit.DataTextField = "onvan"
            CmbUnit.DataValueField = "idd"
            CmbUnit.DataBind()

        End If


    End Sub




    Private Sub load_cmbQsType()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        qsTypeValue, qSTypeID        FROM trnQsDetailVW WHERE        (codePersonel = " & Session("codePersoneli") & ")"
        'WHERE        (hozuri = 0) AND (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbUnit.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            DropDownList1.DataTextField = "qsTypeValue"
            DropDownList1.DataValueField = "qSTypeID"
            DropDownList1.DataBind()

        End If


    End Sub



    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("trnQsEntryOneSession21Frm.aspx")
    End Sub

    Private Sub save_tmp(ByVal doreID As Integer, ByVal responss As Integer)


        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try

            cmm.CommandText = "trnQsResponsePrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@response", SqlDbType.Int, 5)
            cmm.Parameters("@response").Value = responss

            cmm.Parameters.Add("@qsID", SqlDbType.Int, 5)
            cmm.Parameters("@qsID").Value = hdn_QsID.Value

            cmm.Parameters.Add("@codePersoneli", SqlDbType.Int, 5)
            cmm.Parameters("@codePersoneli").Value = Session("codePersoneli")

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
                'load_grid_Sabage()

            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


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





    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        save_tmp(hdn_QsID.Value, RadioButtonList1.SelectedValue)
        load_items()
        'Response.Redirect("trnQsEntryOneSessionFrm.aspx")



    End Sub



    Protected Sub CmbUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbUnit.SelectedIndexChanged
        load_items()
        'UpdatePanel5.Update()
    End Sub

    Protected Sub btnWorkBook_Click(sender As Object, e As EventArgs) Handles btnWorkBook.Click
        If CmbUnit.SelectedValue = 0 Then
            msg_box(" دوره مربوطه را انتخاب نمایید")

            Exit Sub
        Else
            Dim furl As String = "trnQsWorkBookFrm.aspx?PwhereSTR=" & Session("codePersoneli") & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=600,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)

        End If
    End Sub
End Class
