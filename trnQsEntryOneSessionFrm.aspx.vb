Imports System.Data
Imports System.Data.SqlClient
Partial Class trnQsEntryOneSessionFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            'CmbUnit.Enabled = False
  


            load_cmbDore()

            load_items()
            'load_cmbQsType()
            'load_grid_Sabage()
            'load_items()
            loadDataList()

            Session("doreID") = CmbUnit.SelectedValue


        End If

    End Sub
    Private Sub loadDataList()
        Dim sqlstr As String
        'Request.QueryString("id")
        sqlstr = "SELECT       trnQsFileJalaseTbl.jalaseNO,trnQsFileJalaseTbl.jalaseValue, hrDocsDB.dbo.documentsTbl.UnitsCode   " & _
       "FROM            trnQsFileJalaseTbl INNER JOIN  " & _
                        " hrDocsDB.dbo.documentsTbl ON trnQsFileJalaseTbl.jalaseNO = hrDocsDB.dbo.documentsTbl.mozuID INNER JOIN  " & _
                        " trnBarnameRiziTbl ON hrDocsDB.dbo.documentsTbl.UnitsCode = trnBarnameRiziTbl.idd  " & _
          "WHERE        (hrDocsDB.dbo.documentsTbl.docType = 17) AND (hrDocsDB.dbo.documentsTbl.UnitsCode = " & Val(CmbUnit.SelectedValue) & ")  " & _
"GROUP BY trnQsFileJalaseTbl.jalaseValue, hrDocsDB.dbo.documentsTbl.UnitsCode,trnQsFileJalaseTbl.jalaseNO"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        DataList1.DataSource = dt
        DataList1.DataBind()
    End Sub
    Private Sub load_items()

        Dim sqlstrH As String = "SELECT        trnDoreReqisterTbl.idd,trnBarnameRiziTbl.stat, trnQsBaseTbl.doreID  " & _
                   "FROM            trnDoreReqisterTbl INNER JOIN " & _
                        " trnBarnameRiziTbl ON trnDoreReqisterTbl.id_tour = trnBarnameRiziTbl.idd " & _
                        " INNER JOIN                         trnQsBaseTbl ON trnBarnameRiziTbl.idd = trnQsBaseTbl.doreID " & _
                 "WHERE(trnDoreReqisterTbl.hazf = 0) And (trnDoreReqisterTbl.codePersonel = " & Session("codePersoneli") & ") And (trnBarnameRiziTbl.hozuri = 2) and (trnQsBaseTbl.qsType = 5) and (trnQsBaseTbl.doreID = " & Val(CmbUnit.SelectedValue) & ")"
        'And (trnBarnameRiziTbl.stat <> 13)"
        Dim dtH As New DataTable
        dtH = ClsDataTable.GetDataTable(sqlstrH)
        If dtH.Rows.Count > 0 Then
            If dtH.Rows(0).Item("stat").ToString = 13 Then
                msg_box("دوره به اتمام رسیده است")
                Panel3.Enabled = False

                'Exit Sub

            End If
            Panel3.Visible = True
            Dim dtm As New DataTable


            Dim sqlstr As String
            sqlstr = "SELECT        *       FROM trnQsdetailVW WHERE        (codePersonel = " & Session("codePersoneli") & ") and (qsType = 5) and (doreID = " & CmbUnit.SelectedValue & ") ORDER BY doreID DESC  "
            ' AND (qsType = 1) "

            dtm = ClsDataTable.GetDataTable(sqlstr)
            Dim cnt As Integer = dtm.Rows.Count
            If cnt > 0 Then
                Panel3.Visible = True

                Dim dtkk As New DataTable
                Dim sqlstkk As String
                sqlstkk = "SELECT        *       FROM trnQsResponseVW WHERE        (codePersoneli = " & Session("codePersoneli") & ") and (doreID = " & CmbUnit.SelectedValue & ") "
                dtkk = ClsDataTable.GetDataTable(sqlstkk)
                Dim conter As Integer = 1
                If dtkk.Rows.Count > 0 Then
                    conter = dtkk.Rows.Count + 1
                End If
                



            Else
           

            End If


        Else
            'msg_box("شما در دوره غیرحضوری ثبت نام نکرده اید")
            Panel3.Visible = False
            Button1.Visible = False

            Exit Sub
        End If

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








    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
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





 



    Protected Sub CmbUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbUnit.SelectedIndexChanged
        load_items()
        'UpdatePanel5.Update()
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("trnQsEntryOneSession21Frm.aspx")
    End Sub
End Class
