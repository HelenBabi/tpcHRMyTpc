Imports System.Data
Partial Class home
    Inherits System.Web.UI.MasterPage

    Dim dt, DT1, dt_sum As New DataTable

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''Session("userid") = 1
        ' ''''''''''''''''''''''''''''''''''''''''''''''''''load personal detail
        'If Session("userid") Is Nothing Then
        '    Response.Redirect("login_.aspx")
        'Else
        '    If Session("ax") = "" Then
        '        persinpic.ImageUrl = "images/design_images/person_pic/user.png"
        '    Else
        '        persinpic.ImageUrl = Session("ax")
        '    End If
        '    Label_lname.Text = Session("lname")
        '    Label_fname1.Text = Session("fname")
        'End If
        ' '''''''''''''''''''''''''''''''''''''''load menu
        'Label1.Text = ""
        'Label2.Text = ""
        'Dim ClsMenu_ As New ClsMenu
        'dt = ClsMenu_.GetHeadMenu()
        'Dim str_smenu As String = ""
        'Dim STR_MENU As String = ""
        'Dim STR_CTH As String = """"
        'Dim sum_unmenu As String
        'If dt.Rows.Count > 0 Then
        '    STR_MENU += "<ul class=" & STR_CTH & "menu" & STR_CTH & ">"
        '    For I As Integer = 0 To dt.Rows.Count - 1
        '        dt_sum = ClsMenu_.GetCountHeadSubs(dt.Rows(I).Item("FrmID"), Session("userid"))
        '        sum_unmenu = dt_sum.Rows(0).Item("SUM_").ToString
        '        If Val(sum_unmenu) > 0 Then
        '            STR_MENU += "<li><a id=" & STR_CTH & "m" & dt.Rows(I).Item("FrmID") & STR_CTH & " class=" & STR_CTH & "drophome" & STR_CTH & _
        '                "  href=" & STR_CTH & dt.Rows(I).Item("FrmName").ToString & STR_CTH & ">" & _
        '                              "<img src=" & STR_CTH & "images/menu_img/" & dt.Rows(I).Item("FrmIcon").ToString & STR_CTH & " width=" &
        '                          STR_CTH & "64" & STR_CTH & " height=" & STR_CTH & "64" & STR_CTH & "/><br/><span>" & dt.Rows(I).Item("FrmCaption").ToString & "" & _
        '                          "</span></a></li>"
        '            DT1 = ClsMenu_.GetHeadSubs(dt.Rows(I).Item("FrmID"), Session("userid"))
        '            If DT1.Rows.Count > 0 Then
        '                str_smenu += "<div class=" & STR_CTH & "m" & dt.Rows(I).Item("FrmID") & " submenu" & STR_CTH & "><ul class=" & STR_CTH & "subdrophome" & STR_CTH & ">"
        '                For J As Integer = 0 To DT1.Rows.Count - 1
        '                    str_smenu += "<li><a class=" & STR_CTH & "subs" & STR_CTH & " href=" & STR_CTH & DT1.Rows(J).Item("FrmName").ToString & STR_CTH & ">" &
        '                        DT1.Rows(J).Item("FrmCaption").ToString & _
        '                    "</a></li>"
        '                Next
        '                str_smenu += "</ul></div>"
        '            End If
        '            'STR_MENU += "</li>"
        '        End If
        '    Next
        '    STR_MENU += "</ul>"
        'End If
        'Label1.Text = STR_MENU
        'Label2.Text = str_smenu
        ''        <div class="menu1 submenu">
        ''<ul  class="subdrophome">
        ''<li >
        ''<a  class"subs" href="http://bazar.ir/cat,1/الکترونیک_و_دیجیتال/">الکترونیک و دیجیتال</a>
        ''</li>
        ''<li>
        ''<a  class"subs" href="http://bazar.ir/cat,2/کتاب/">کتاب</a>
        ''</li>
        ''</ul>
        ''</div>
        ''<div class="menu2 submenu">
        ''<ul  class="subdrophome">
        ''<li >
        ''<a  class"subs" href="http://bazar.ir/cat,1/الکترونیک_و_دیجیتال/"> و دیجیتال</a>
        ''</li>
        ''<li >
        ''<a  class"subs" href="http://bazar.ir/cat,2/کتاب/">کتاب</a>
        ''</li>
        ''</ul>
        ''</div>
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("frmPass.aspx")
    End Sub

    Protected Sub lnbexit0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnbexit0.Click
        Response.Redirect("frmChngPic.aspx")
    End Sub

    Protected Sub lnbexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnbexit.Click

    End Sub
End Class

