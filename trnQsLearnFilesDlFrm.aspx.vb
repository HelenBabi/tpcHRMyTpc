Imports System.Data
Imports System.Data.SqlClient
Partial Class trnQsLearnFilesDlFrm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGridviewData()

        End If
    End Sub
    Private Sub BindGridviewData()

        Dim dt3 As New DataTable
        Dim sqlstr As String
        ' sqlstr = " SELECT         id, content_name, date, time, size, userid, " & _
        '"  bazdid, sharh, ip, computernam, mdate " & _
        '         " FROM              documentsTbl  where UnitsCode =  " & Request.QueryString("id") & ""
        sqlstr = "SELECT      documentsTbl.date,documentsTbl.bazdid, documentsTbl.size, documentsTbl.id, documentsTbl.content_name, documentsTbl.sharh, trnBarnameRiziTbl.onvan as UnitsName " & _
                         ", documentsTbl.UnitsCode, documentsTbl.CodeClosed " & _
                     "FROM            documentsTbl INNER JOIN " & _
                            " trnBarnameRiziTbl   ON documentsTbl.UnitsCode = trnBarnameRiziTbl.idd  WHERE (documentsTbl.docType = 17) AND (documentsTbl.UnitsCode = " & Session("doreID") & ") and (documentsTbl.mozuID = " & Request.QueryString("id") & ")"
        dt3 = ClsDataTable.GetDataTable(sqlstr)
        If dt3.Rows.Count > 0 Then
            gvDetails.DataSource = dt3
            gvDetails.DataBind()

        End If


    End Sub

    ' This button click event is used to download files from gridview
    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(gvDetails.DataKeys(gvrow.RowIndex).Value.ToString())
        'Dim sqlstrbazdid As String = "UPDATE       documentsTbl SET                bazdid = bazdid + 1  WHERE        (id = " & fileid & ")"
        'upStat(sqlstrbazdid)
        BindGridviewData()
        Dim sqlstrd As String = ""
        Dim dtd As New DataTable
        sqlstrd = "select  id, content_name, content_file  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)



        '''''''''''''''''''''''''''''''''

        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()
        If dtd.Rows.Count > 0 Then

            Dim sqlstrbazdid As String = "UPDATE       documentsTbl SET                bazdid = bazdid + 1  WHERE        (id = " & fileid & ")"
            upStat(sqlstrbazdid)

            Dim sqlStrIns As String = "INSERT INTO trnDownloadHisTbl     (CodePersoneli, ip, tarix, fileID) " & _
                             " VALUES        (" & Session("codePersoneli") & ", '" & Request.UserHostAddress & "' , '" & ClsDateTime.GetDateNow() & "', " & fileid & ")"
            upStat(sqlStrIns)

            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("content_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("content_file"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این موضوع فایل سند ضمیمه نشده است")
        End If

    End Sub

    Protected Sub lnkDownload2_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(gvDetails.DataKeys(gvrow.RowIndex).Value.ToString())

        BindGridviewData()
        Dim sqlstrd As String = ""
        Dim dtd As New DataTable
        sqlstrd = "select  id, voice_name, voiceFile  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)



        '''''''''''''''''''''''''''''''''

        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()
        If dtd.Rows.Count > 0 Then
            Dim sqlstrbazdid As String = "UPDATE       documentsTbl SET                bazdid = bazdid + 1  WHERE        (id = " & fileid & ")"
            upStat(sqlstrbazdid)

            Dim sqlStrIns As String = "INSERT INTO trnDownloadHisTbl     (CodePersoneli, ip, tarix, fileID) " & _
                             " VALUES        (" & Session("codePersoneli") & ", '" & Request.UserHostAddress & "' , '" & ClsDateTime.GetDateNow() & "', " & fileid & ")"
            upStat(sqlStrIns)

            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("voice_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("voiceFile"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این موضوع فایل صوتی ضمیمه نشده است")
        End If

    End Sub

    Protected Sub lnkDownload3_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
        Dim fileid As Integer = Convert.ToInt32(gvDetails.DataKeys(gvrow.RowIndex).Value.ToString())

        BindGridviewData()
        Dim sqlstrd As String = ""
        Dim dtd As New DataTable
        sqlstrd = "select  id, video_name, videoFile  from documentsTbl where id=" & fileid & ""
        dtd = ClsDataTable.GetDataTable(sqlstrd)



        '''''''''''''''''''''''''''''''''

        'Response.ContentType = dtd.Rows(0).Item("content_type").ToString()
        If dtd.Rows.Count > 0 Then

            Dim sqlstrbazdid As String = "UPDATE       documentsTbl SET                bazdid = bazdid + 1  WHERE        (id = " & fileid & ")"
            upStat(sqlstrbazdid)

            Dim sqlStrIns As String = "INSERT INTO trnDownloadHisTbl     (CodePersoneli, ip, tarix, fileID) " & _
                             " VALUES        (" & Session("codePersoneli") & ", '" & Request.UserHostAddress & "' , '" & ClsDateTime.GetDateNow() & "', " & fileid & ")"
            upStat(sqlStrIns)

            Response.AddHeader("Content-Disposition", "attachment;filename=""" & Convert.ToString(dtd.Rows(0).Item("video_name")) & """")
            Response.BinaryWrite(DirectCast(dtd.Rows(0).Item("videoFile"), Byte()))
            Response.[End]()
        Else
            msg_box("برای این موضوع فایل ویدیو ضمیمه نشده است")
        End If

    End Sub
    Private Sub upStat(sqlstr2 As String)
        Dim cnn As New SqlConnection
        Try
            Dim cmm As SqlCommand

            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            cmm = New SqlCommand(sqlstr2, cnn)
            cmm.ExecuteNonQuery()

        Catch ex As Exception
            'msg_box(ex.Message.ToString)
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
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub




    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("trnQsEntryOneSessionFrm.aspx")
    End Sub
End Class
