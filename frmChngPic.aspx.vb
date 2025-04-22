Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls


Partial Class frmChngPic
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("WorkType") = 50 Then

            ibtnExit.Visible = False
        End If

        If Me.IsPostBack = False Then
            Session("UserId") = Session("userid")
            ' meno_acc()
        End If

    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmChngPic.aspx", Val(Session("UserId")))
        'ibtnAdd.Visible = meno_arr(1)
        'ibtnEdit.Visible = meno_arr(2)
        'ibtnSave.Visible = meno_arr(3)
        'ibtnSaveNew.Visible = meno_arr(4)
        'ibtnCancel.Visible = meno_arr(5)
        'ibtnDelete.Visible = meno_arr(6)
        'ibtnDeleted.Visible = meno_arr(7)
        'ibtnHistory.Visible = meno_arr(8)
        'ibtnDeactived.Visible = meno_arr(9)
        'ibtnPrint.Visible = meno_arr(10)
        'ibtnExit.Visible = meno_arr(11)
    End Sub

    Private Sub enable_disable()
        If ibtnSave.Enabled = False Then
            ibtnSave.Enabled = True
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True

            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            ibtnDeleted.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False
            ibtnExit.Enabled = False
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel.png"

            ibtnEdit.ImageUrl = "~/Icons/edit2.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete2.png"
            ibtnAdd.ImageUrl = "~/Icons/Add2.png"
            ibtnDeleted.ImageUrl = "~/Icons/deleted2.png"
            ibtnHistory.ImageUrl = "~/Icons/history2.png"
            ibtnDeactived.ImageUrl = "~/Icons/reload2.png"
            ibtnPrint.ImageUrl = "~/Icons/Print2.png"
            ''''''''''''''''''''''''''''''''''''''''''''''''

        Else
            ibtnSave.Enabled = False
            ibtnSaveNew.Enabled = False
            ibtnCancel.Enabled = False

            ibtnEdit.Enabled = True
            ibtnDelete.Enabled = True
            ibtnAdd.Enabled = True
            ibtnDeleted.Enabled = True
            ibtnHistory.Enabled = True
            ibtnDeactived.Enabled = True
            ibtnPrint.Enabled = True
            ibtnExit.Enabled = True
            '''''''''''''''''''''''''''''''''''''''''
            ibtnSave.ImageUrl = "~/Icons/save2.png"
            ibtnSaveNew.ImageUrl = "~/Icons/savenew2.png"
            ibtnCancel.ImageUrl = "~/Icons/Cancel2.png"

            ibtnEdit.ImageUrl = "~/Icons/edit.png"
            ibtnDelete.ImageUrl = "~/Icons/Delete.png"
            ibtnAdd.ImageUrl = "~/Icons/Add.png"
            ibtnDeleted.ImageUrl = "~/Icons/deleted.png"
            ibtnHistory.ImageUrl = "~/Icons/history.png"
            ibtnDeactived.ImageUrl = "~/Icons/reload.png"
            ibtnPrint.ImageUrl = "~/Icons/Print.png"
            ''''''''''''''''''''''''''''''''''''''''''''''''



            ' txt_BakhshCode.Enabled = True
        End If

    End Sub

    Private Sub clear_form()
        'txt_CurentPass.Text = ""
        'txt_NewPass1.Text = ""
        'txt_NewPass2.Text = ""

    End Sub

    Private Sub msg_box(ByVal msg As String)
        Dim sb As New StringBuilder
        sb.Append("alert('")
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", sb.ToString(), True)
        ' ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" + sb.ToString + "');", True)
    End Sub

#End Region


#Region "meno button"
    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSave.Click
        If Page.IsValid Then
            Call save_final()

        End If


    End Sub


    Private Sub save_()
        enable_disable()
        clear_form()

    End Sub

    Protected Sub ibtnSaveNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSaveNew.Click
        Call save_final()

    End Sub

    Protected Sub ibtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEdit.Click
        Call edit_()
    End Sub
    Private Sub edit_()
        enable_disable()
    End Sub
    Private Sub cancel_()
        enable_disable()
        clear_form()
    End Sub

    Protected Sub ibtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnCancel.Click
        Call cancel_()
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDelete.Click
        Call dell_()
    End Sub
    Private Sub dell_()

        Call save_final()
        enable_disable()
        clear_form()
    End Sub



#End Region








    Private Sub save_final()
        Dim sqlstr As String

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Dim size As Integer = FileUpload1.PostedFile.ContentLength
        size = size / 1024
        'This is where I'd like to perform the file size check 


        If FileUpload1.HasFile = False Then

            msg_box("لطفا فایل را انتخاب کنید")
           

        Else
            If size > 100 Then
                msg_box("حجم فایل بیشتر از صد کیلو بایت است")
                FileUpload1.Focus()
                Exit Sub
            End If
            Dim masir As String = HttpContext.Current.Server.MapPath("~/images/person_pic/")

            FileUpload1.PostedFile.SaveAs(masir & Session("codePersoneli") & ".jpg")

            Dim filname As String = "images/person_pic/" & Session("codePersoneli") & ".jpg"
            sqlstr = "UPDATE    personel  SET   UserPic = '" & filname & "'" & _
          " WHERE(ID = " & Session("userid") & ")"
            cmm = New SqlCommand(sqlstr, cnn)
            cmm.ExecuteNonQuery()
            msg_box("عكس جديد ثبت شد")
            cnn.Close()

        End If

    End Sub

    Private Sub var(p1 As Object)
        Throw New NotImplementedException
    End Sub

    Private Function FileExtension() As Object
        Throw New NotImplementedException
    End Function





    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

     
            Response.Redirect("index.aspx")
    
    End Sub

    'Protected Sub CustomValidator1_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
    '    Dim myPostedFile As HttpPostedFile = FileUpload1.PostedFile
    '    If myPostedFile IsNot Nothing AndAlso myPostedFile.ContentLength > 0 Then
    '        Dim finfo As New FileInfo(myPostedFile.FileName)
    '        Dim fileExtension As String = finfo.Extension.ToLower()
    '        If fileExtension = ".gif" OrElse fileExtension = ".jpg" OrElse fileExtension = ".jpeg" OrElse fileExtension = ".png" Then
    '            args.IsValid = True
    '        Else
    '            args.IsValid = False
    '        End If
    '    End If
    'End Sub
End Class
