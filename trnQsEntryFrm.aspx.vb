Imports System.Data
Imports System.Data.SqlClient
Partial Class trnQsEntryFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            CmbUnit.Enabled = False
            DropDownList1.Enabled = False
            soalTxt.Enabled = False


            load_cmbDore()
            load_items()
            'load_grid_Sabage()
            load_items()




        End If

    End Sub

    Private Sub load_items()

        Dim sqlstrH As String = "SELECT        trnDoreReqisterTbl.idd,trnBarnameRiziTbl.stat  " & _
                   "FROM            trnDoreReqisterTbl INNER JOIN " & _
                        " trnBarnameRiziTbl ON trnDoreReqisterTbl.id_tour = trnBarnameRiziTbl.idd " & _
                        " INNER JOIN                         trnQsBaseTbl ON trnBarnameRiziTbl.idd = trnQsBaseTbl.doreID " & _
                 "WHERE(trnDoreReqisterTbl.hazf = 0) And (trnDoreReqisterTbl.codePersonel = " & Session("codePersoneli") & ") And (trnBarnameRiziTbl.hozuri = 2) and (trnQsBaseTbl.qsType = 1)"
        'And (trnBarnameRiziTbl.stat <> 13)"
        Dim dtH As New DataTable
        dtH = ClsDataTable.GetDataTable(sqlstrH)
        If dtH.Rows.Count > 0 Then
            If dtH.Rows(0).Item("stat").ToString = 13 Then
                msg_box("دوره به اتمام رسیده است")
                Panel1.Enabled = False

                'Exit Sub

            End If

            Dim dtm As New DataTable


            Dim sqlstr As String
            sqlstr = "SELECT        *       FROM trnQsdetailVW WHERE        (codePersonel = " & Session("codePersoneli") & ") and (qsType = 1)  "
            ' AND (qsType = 1) "

            dtm = ClsDataTable.GetDataTable(sqlstr)
            Dim cnt As Integer = dtm.Rows.Count
            If cnt > 0 Then
                Dim dtkk As New DataTable
                Dim sqlstkk As String
                sqlstkk = "SELECT        *       FROM trnQsResponseTbl WHERE        (codePersoneli = " & Session("codePersoneli") & ") "
                dtkk = ClsDataTable.GetDataTable(sqlstkk)
                Dim conter As Integer = 1
                If dtkk.Rows.Count > 0 Then
                    conter = dtkk.Rows.Count + 1
                End If
                lblSoal.Text = " سوال " & conter & " از  " & dtH.Rows.Count

                CmbUnit.SelectedValue = dtm.Rows(0).Item("doreID").ToString
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
                sqlstk = "SELECT        *       FROM trnQsWorkBookVW WHERE        (codePersoneli = " & Session("codePersoneli") & ") "
                dtk = ClsDataTable.GetDataTable(sqlstk)

                'Panel1.Visible = False
                Button1.Visible = False
                lblPayam.Visible = True

                Dim jamBarom As Integer = dtH.Rows.Count
                Dim nomre As Integer = dtk.Rows.Count

                lblPayam.Text = "شما  سوالات  را پاسخ داده اید،"
                lblPayam.Text = lblPayam.Text & " نمره = " & nomre & " از  " & jamBarom



                Dim siDarsad As Integer = jamBarom * 30 / 100
                Dim panjahDarsad As Integer = jamBarom * 50 / 100

                lblPayam2.Visible = True
                If nomre <= siDarsad Then

                    lblPayam2.Text = "سطح شما C ارزیابی شد "
                    btnC.Visible = True

                End If

                If nomre >= panjahDarsad Then

                    lblPayam2.Text = "سطح شما B ارزیابی شد"
                    btnB.Visible = True

                End If

                If nomre > panjahDarsad Then

                    lblPayam2.Text = "سطح شما A ارزیابی شد"
                    btnA.Visible = True
                End If

            End If


        Else
            msg_box("شما در دوره غیرحضوری ثبت نام نکرده اید")
            Panel1.Visible = False
            Exit Sub
        End If

    End Sub

    Private Sub load_cmbDore()
        Dim dtm As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        idd, onvan       FROM trnBarnameRiziTbl"
        'WHERE        (hozuri = 0) AND (CodeClosed = 0)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbUnit.DataSource = dtm
        If dtm.Rows.Count > 0 Then
            CmbUnit.DataTextField = "onvan"
            CmbUnit.DataValueField = "idd"
            CmbUnit.DataBind()

        End If


    End Sub







    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
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
        'load_items()
        Response.Redirect("trnQsEntryFrm.aspx")



    End Sub

   

End Class
