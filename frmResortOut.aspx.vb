Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO

Partial Class frmResortOut

    Inherits System.Web.UI.Page
    Dim dt1 As New DataTable

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("fromTyp") = "rpt" Then
            'btnsave.Visible = False
            Label2.Visible = False
        End If
        Image1.ImageUrl = "images/Resort/" & Session("ResortID") & ".jpg"
        load_Noemoraje()
        txtResortId.Text = Session("ResortID")

    End Sub


    Private Sub load_Noemoraje()
        Dim ResortId As Integer = Session("ResortID")
        Dim dt1 As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "SELECT     tblResort.ResortID, tblGenderTyp.genValue+' ' + tblResort.nam + ' ' +  tblResort.lnam as moraje , tblResort.datee + '   ' + tblResort.timee as zaman, tblResort.codeCart, tblResort.mizban, " & _
                     "tblResort.otag, tblResort.mojavezNo, tblResort.notee, tblResort.dateEnd + '   ' + tblResort.timeEnd as zamanEnd,  " & _
                     "tblUsers.UserLastName + ' ' + tblUsers.UserFamili AS karbar,  tblSepordeType.Sepordevalue, tblResort.stat,  " & _
       " tblUnits.UnitsName, tblEllatType.ellatName " & _
"FROM         tblResort INNER JOIN " & _
                    " tblUsers ON tblResort.userId = tblUsers.UserCode INNER JOIN " & _
                      "tblSepordeType ON tblResort.sepordeTyp = tblSepordeType.SepordeCode INNER JOIN " & _
                     "tblUnits ON tblResort.unitee = tblUnits.UnitsCode INNER JOIN " & _
                     "tblGenderTyp ON tblResort.genderr = tblGenderTyp.genId INNER JOIN " & _
                     "tblEllatType ON tblResort.ellat = tblEllatType.ellatCode " & _
       " WHERE tblResort.ResortID = " & ResortId & ""
        dt1 = ClsDataTable.GetDataTableHerasat(sqlstr)

        txtCodemojavez.Text = dt1.Rows(0).Item("mojavezNo")
        txtLnam.Text = dt1.Rows(0).Item("moraje")
        txtMizban.Text = dt1.Rows(0).Item("mizban")
        txtNotee.Text = dt1.Rows(0).Item("notee")
        txtotag.Text = dt1.Rows(0).Item("otag")
        txtShomarecart.Text = dt1.Rows(0).Item("codeCart")
        txtellt.Text = dt1.Rows(0).Item("ellatName")
        txtunit.Text = dt1.Rows(0).Item("UnitsName")
        txtsepordetyp.Text = dt1.Rows(0).Item("Sepordevalue")
        txtzaman.Text = dt1.Rows(0).Item("zaman")
        txtzamanXuruj.Text = dt1.Rows(0).Item("zamanEnd")
    End Sub
#Region "All pages"

  



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








    Private Sub save_final()


        '   Dim Resortid As Integer = ClsGetMaxId.GetMaxId("tblResort", "ResortID")

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConnHerasat
        cnn.Open()

        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "resortEndUserPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@ResortID", SqlDbType.Int, 10)
            cmm.Parameters("@ResortID").Value = txtResortId.Text

            cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
            cmm.Parameters("@userid").Value = Session("userid")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("ثبت خروج با موفقیت انجام شد")
                ' Response.Redirect("frmWorkTableUsers.aspx")
                btnBrwsSys.Enabled = False
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub

   




   

    
    Protected Sub btnPrint_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrint.Click
        Dim furl As String = "frmPrint3.aspx?PwhereSTR=" & Session("ResortID") & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=900,width=450,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub btnBrwsSys_Click(sender As Object, e As EventArgs) Handles btnBrwsSys.Click
        save_final()
    End Sub

    Protected Sub btnBack0_Click(sender As Object, e As EventArgs) Handles btnBack0.Click
        If Session("fromTyp") = "regOut" Then
            Response.Redirect("resortRegisterBrowseFrm.aspx")
        Else
            Response.Redirect("index.aspx")
        End If
    End Sub
End Class
