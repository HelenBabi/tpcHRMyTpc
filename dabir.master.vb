
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Partial Class light_MasterPage
    Inherits System.Web.UI.MasterPage
    Dim dt, DT1, dt_sum As New DataTable

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If
        'ltr_family.Text = "اروف حاج علی"
        ltrLevel.Text = Session("semat")
        'ltrMadrak.Text = Session("madrakReshte")
        Dim dds As New ClsDateTime


        imgProfiles.Src = "images/person_pic/" & Session("codePersoneli") & ".jpg"
        ''"../" +
        ltrDate.Text = dds.getdatee



        '''''''''''''''''''''''''''''''load person_detail


        'Image_pp.ImageUrl = Session("ax")
        ltr_family.Text = Session("lname") & " " & Session("fname")
        'Label_fname.Text = Session("fname")
        'Image_pp.Height = 50
        'Image_pp.Width = 50
        ''''''''''''''''''''load menu


        Label1.Text = ""
        'Label2.Text = ""
        Dim ClsMenu_ As New ClsMenu
        dt = ClsMenu_.GetHeadMenu()
        Dim str_smenu As String = ""
        Dim STR_MENU As String = ""
        Dim STR_CTH As String = """"
        Dim sum_unmenu As String

        If dt.Rows.Count > 0 Then

            If Session("super_vizer") = 0 Then
                For I As Integer = 0 To dt.Rows.Count - 1
                    dt_sum = ClsMenu_.GetCountHeadSubs(dt.Rows(I).Item("FrmID"), 0)
                    sum_unmenu = dt_sum.Rows(0).Item("SUM_").ToString
                    If Val(sum_unmenu) > 0 Then

                        STR_MENU += "<ul class='menu js__accordion'>" & _
                                        "<li>" & _
                                            "<a class='waves-effect parent-item js__control' href='#'><i class='menu-icon mdi mdi-cube-outline'></i><span>" + dt.Rows(I).Item("FrmCaption").ToString + "</span><span class='menu-arrow fa fa-angle-down'></span></a>" & _
                                            "<ul class='sub-menu js__content'>"


                        DT1 = ClsMenu_.GetHeadSubs(dt.Rows(I).Item("FrmID"), 0)
                        If DT1.Rows.Count > 0 Then

                            For J As Integer = 0 To DT1.Rows.Count - 1
                                STR_MENU += "<li><a href='" + DT1.Rows(J).Item("FrmName").ToString + "'>" + DT1.Rows(J).Item("FrmCaption").ToString + "</a></li>"


                            Next
                            STR_MENU += "</ul>	</li>"
                            STR_MENU += "</ul>"
                        End If
                    End If
                Next
            End If

            For I As Integer = 0 To dt.Rows.Count - 1
                dt_sum = ClsMenu_.GetCountHeadSubs(dt.Rows(I).Item("FrmID"), Session("userid"))
                sum_unmenu = dt_sum.Rows(0).Item("SUM_").ToString
                If Val(sum_unmenu) > 0 Then

                    STR_MENU += "<ul class='menu js__accordion'>" & _
                                    "<li>" & _
                                        "<a class='waves-effect parent-item js__control' href='#'><i class='menu-icon mdi mdi-cube-outline'></i><span>" + dt.Rows(I).Item("FrmCaption").ToString + "</span><span class='menu-arrow fa fa-angle-down'></span></a>" & _
                                        "<ul class='sub-menu js__content'>"


                    DT1 = ClsMenu_.GetHeadSubs(dt.Rows(I).Item("FrmID"), Session("userid"))
                    If DT1.Rows.Count > 0 Then

                        For J As Integer = 0 To DT1.Rows.Count - 1
                            STR_MENU += "<li><a href='" + DT1.Rows(J).Item("FrmName").ToString + "'>" + DT1.Rows(J).Item("FrmCaption").ToString + "</a></li>"


                        Next
                        STR_MENU += "</ul>	</li>"
                        STR_MENU += "</ul>"
                    End If
                End If
            Next



        End If
        sel_noti()
        Label1.Text = STR_MENU
        'Label2.Text = str_smenu
    End Sub
    Private Sub sel_noti()
        Dim sqlstr As String
        sqlstr = "  select reciverID from tblJobResiverTemp  WHERE        (show = 0) AND (reciverID = " & Session("UserId") & ")  "
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            Imagenoti.Visible = True
        Else
            Imagenoti.Visible = False
        End If
    End Sub
    Protected Sub Imagenoti_Click(sender As Object, e As ImageClickEventArgs) Handles Imagenoti.Click
        Response.Redirect("jobManagementBrowseFrm.aspx")
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs)
        ' پاک کردن تمام سشن‌ها
    

        ' پاک کردن کوکی اگر دارید
        If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
            Dim myCookie As New HttpCookie("ASP.NET_SessionId")
            myCookie.Expires = DateTime.Now.AddDays(-1D)
            Response.Cookies.Add(myCookie)
        End If
        If Session("fromLogin") = "1" Then
            Session.Clear()
            Session.Abandon()
            Response.Redirect("http://192.168.30.10:9090/sso/portal", True)
            Context.ApplicationInstance.CompleteRequest()
        Else
            Session.Clear()
            Session.Abandon()
            Response.Redirect("login_2.aspx", True)
            Context.ApplicationInstance.CompleteRequest()
        End If
        ' هدایت به صفحه لاگین
       
    End Sub

End Class
