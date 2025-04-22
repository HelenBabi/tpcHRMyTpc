Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization.PersianCalendar
Imports System
Imports System.Linq
Imports System.Configuration


Partial Class refahiReserve
    Inherits System.Web.UI.Page

    Dim tarix As String = ClsDateTime.GetDateNow
    Dim times As Integer = ClsDateTime.GetSaat()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("login_2.aspx")
        End If

     

        If Me.IsPostBack = True Then
            Exit Sub
        End If


        Dim sqlstrSS As String
        sqlstrSS = "SELECT     TOP (1) startTime, stopTime,CodeClosed  FROM startStopTbl "
        Dim dtss As New DataTable
        dtss = ClsDataTable.GetDataTable(sqlstrSS)
        Dim startt As Integer = dtss.Rows(0).Item("startTime").ToString
        Dim stopp As Integer = dtss.Rows(0).Item("stopTime").ToString
        If dtss.Rows(0).Item("CodeClosed") = 0 Then

            If times >= startt And times <= stopp Then
                load_Resturan()
                ' Dim quta As Integer = GetTakafol() 'tedadTakafolcls.GetCount(Session("codePersoneli"))
                ' tedad.Text = GetTakafol()

               
                Dim x As String
                Dim w As String = ""
                Dim dd As New Globalization.PersianCalendar
                x = dd.GetDayOfWeek(Now)

                Dim today = Date.Now()

                If x = 1 Then
                    w = "  دوشنبه "
                    msg_box("  سیستم رزرواسیون غیرفعال است")
                    hoshdarLbl.Text = "  سیستم رزرواسیون غیرفعال است"
                    Panel1.Enabled = False
                    Exit Sub

               
                ElseIf x = 2 Then
                    w = "  سه شنبه "
                    msg_box("سه شنبه سیستم رزرواسیون غیرفعال است")
                    hoshdarLbl.Text = "سه شنبه سیستم رزرواسیون غیرفعال است"
                    Panel1.Enabled = False
                    Exit Sub
                ElseIf x = 3 Then
                    w = "  چهارشنبه "
                    If Session("contratTyp") <> 2 Or Session("shift") = 1 Then
                        msg_box("  سیستم رزرواسیون غیرفعال است")
                        hoshdarLbl.Text = "  سیستم رزرواسیون غیرفعال است"
                        Panel1.Enabled = False
                        Exit Sub
                   
                    End If
                ElseIf x = 4 Then
                    w = "  پنج شنبه "
                    If Session("contratTyp") = 2 Or Session("contratTyp") = 5 Then


                        Panel1.Enabled = True
                        checkQouata()
                        loadGridEbtal()
                    Else
                        msg_box("امروز برای پرسنل رسمی  و قرارداد معین  امکان رزرو مقدور میباشد")
                        hoshdarLbl.Text = "امروز برای پرسنل رسمی  و قرارداد معین  امکان رزرو مقدور میباشد"
                        Panel1.Enabled = False
                        Exit Sub
                    End If
                ElseIf x = 5 Then
                    w = "  جمعه "
                    msg_box("جمعه سیستم رزرواسیون غیرفعال است")
                    hoshdarLbl.Text = "جمعه سیستم رزرواسیون غیرفعال است"
                    Panel1.Enabled = False
                    Exit Sub
                ElseIf x = 6 Then
                    w = "  شنبه "

                    'If Session("modir") = True Then


                    '    Panel1.Enabled = True
                    '    resturanCmb.SelectedValue = 1
                    '    load_Menu()
                    '    'cmbTakafol.Visible = False
                    '    cmbTakafol.Visible = False
                    '    btnTemp.Visible = False
                    '    Label5.Visible = False
                    '    resturanCmb.Enabled = False
                    '    checkQouata()
                    '    loadGridEbtal()
                    'Else
                    '    msg_box("امروز برای مدیران امکان رزرو مقدور میباشد")
                    '    hoshdarLbl.Text = "امروز برای مدیران امکان رزرو مقدور میباشد"
                    '    Panel1.Enabled = False
                    '    Exit Sub
                    'End If
                    msg_box("  سیستم رزرواسیون غیرفعال است")
                    hoshdarLbl.Text = "  سیستم رزرواسیون غیرفعال است"
                    Panel1.Enabled = False
                    Exit Sub
                ElseIf x = 0 Then
                    w = "  یکشنبه "
                    If Session("contratTyp") = 2 Or Session("contratTyp") = 5 Then


                        Panel1.Enabled = True
                        checkQouata()
                        loadGridEbtal()
                    Else
                        msg_box("امروز برای پرسنل رسمی  و قرارداد معین  امکان رزرو مقدور میباشد")
                        hoshdarLbl.Text = "امروز برای پرسنل رسمی  و قرارداد معین  امکان رزرو مقدور میباشد"
                        Panel1.Enabled = False
                        Exit Sub
                    End If
                  
                End If
            Else
                hoshdarLbl.Text = "زمان رزرو از ساعت" & startt & " الی " & stopp + 1 & " میباشد "

                msg_box("زمان رزرو از ساعت" & startt & " الی " & stopp + 1 & " میباشد ")
                Panel1.Enabled = False
                Exit Sub
            End If

        Else
            msg_box("سیستم رزرو غیرفعال میباشد")
            hoshdarLbl.Text = "سیستم رزرو غیرفعال میباشد"
            Panel1.Enabled = False
            Exit Sub
        End If


        '-------------------------------------------------------

     


    



    End Sub
  


    Private Sub load_Resturan()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT        locID, locationValue, stat        FROM refahiLocationTbl WHERE        (stat = 1) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        resturanCmb.DataSource = dt
        resturanCmb.DataValueField = "locID"
        resturanCmb.DataTextField = "locationValue"
        resturanCmb.DataBind()
    End Sub

    'Private Sub load_Menu()
    '    Dim dt As New DataTable

    '    Dim sqlstr As String
    '    sqlstr = "SELECT         menuID, menu, resturanID, gimat, zarfiat, CodeClosed, opid, ArcTypeCode " & _
    '    " FROM menuTbl WHERE      (CodeClosed = 0) AND  (resturanID = " & resturanCmb.SelectedValue & ") "
    '    dt = ClsDataTable.GetDataTable(sqlstr)
    '    menuCmb.DataSource = dt
    '    menuCmb.DataValueField = "menuID"
    '    menuCmb.DataTextField = "menu"
    '    menuCmb.DataBind()
    'End Sub

    Private Sub load_Menu()
        Dim dt As New DataTable

        Dim sqlstr As String
        sqlstr = "SELECT menuID, menu     FROM menuTbl " & _
                "WHERE (resturanID = " & resturanCmb.SelectedValue & ") AND (zarfiat > dbo.GetZarfiyatRooz(menuID)) AND (CodeClosed = 0) "

        dt = ClsDataTable.GetDataTable(sqlstr)
        menuCmb.DataSource = dt
        menuCmb.DataValueField = "menuID"
        menuCmb.DataTextField = "menu"
        menuCmb.DataBind()
    End Sub

    Private Sub load_Times()
        Dim dt As New DataTable
        Dim roozHafteh As Integer
        roozHafteh = ClsDateTime.GetRoozHafteAdad()

        Dim sqlstr As String
        sqlstr = String.Format(" SELECT timeID, valuee  FROM  barnameTbl " & _
        " WHERE (CodeClosed = 0) AND (resturanID = {0}) AND (dbo.GetTimeValid(timeID, {1}) = 1)  " & _
           " ORDER BY timeID ", resturanCmb.SelectedValue, roozHafteh)

        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count = 0 And resturanCmb.SelectedValue <> 0 Then
            btnTemp.Visible = False

            hoshdarLbl.Text = "امروز  " & resturanCmb.SelectedItem.Text & " فعال نمیباشد "


        End If
        'saatCmb.DataSource = dt
        'saatCmb.DataValueField = "timeID"
        'saatCmb.DataTextField = "valuee"
        'saatCmb.DataBind()
    End Sub
    Private Sub checkQouata()
        Dim dt, dt2, dt3 As New DataTable

        ' Dim takafol As Integer = tedadTakafolcls.GetCount(Session("codePersoneli"))

        '  Dim qouata As Integer = qouataCls.GetQouata(Session("contratTyp"))
        Dim sqlstr, sqlstr2 As String
        Dim rooz As Integer = ClsDateTime.getRoozMah()
        Dim flag_rooz_az_mah As Boolean = False





        Dim sqlstr3 As String = "SELECT     reservID      FROM resrevationTbl " & _
      " WHERE     (tarix = '" & tarix & "') AND (stat =1) AND (perID = " & Session("UserId") & ")"
        dt3 = ClsDataTable.GetDataTable(sqlstr3)
        If dt3.Rows.Count > 0 Then
            Panel1.Enabled = False
            'grdReserve.Enabled = False
            hoshdarLbl.Text = "  امروز رزرو کرده اید"
            msg_box("  امروز رزرو کرده اید")
            btnReserve.Visible = False
            Panel1.Enabled = False
            Exit Sub
        End If



        sqlstr = String.Format("SELECT  id, value, TypeSahm1, TypeSahmDateStart1, TypeSahmDateEnd1, TypeSahm2, TypeSahmDateStart2, TypeSahmDateEnd2, TypeSahm3, " & _
        " TypeSahmDateStart3, TypeSahmDateEnd3, TypeSahm4, TypeSahmDateStart4, TypeSahmDateEnd4 " & _
        " FROM contratType WHERE (id = {0})", Session("contratTyp"))



        '     sqlstr = "SELECT     reservID      FROM resrevationTbl " & _
        '  " WHERE     (mah = " & mah & ") AND (stat <>0) AND (perID = " & Session("UserId") & ")"

        '     sqlstr2 = "SELECT     reservID      FROM resrevationTbl " & _
        '" WHERE     (tarix = '" & tarix & "') AND (stat =1) AND (perID = " & Session("UserId") & ")"

        dt = ClsDataTable.GetDataTable(sqlstr)


        Dim startDate As String = ""
        Dim EndDate As String = ""

        Dim sahm As Integer = dt.Rows(0).Item("TypeSahm1").ToString
        Dim start1 As Integer = dt.Rows(0).Item("TypeSahmDateStart1").ToString
        Dim End1 As Integer = dt.Rows(0).Item("TypeSahmDateEnd1").ToString


        If (rooz >= start1 And rooz <= End1) Then
            flag_rooz_az_mah = True
            startDate = CreateDate(start1)
            EndDate = CreateDate(End1)
        End If


        If (flag_rooz_az_mah = False) Then
            sahm = dt.Rows(0).Item("TypeSahm2").ToString
            Dim start2 As Integer = dt.Rows(0).Item("TypeSahmDateStart2").ToString
            Dim End2 As Integer = dt.Rows(0).Item("TypeSahmDateEnd2").ToString
            If (rooz >= start2 And rooz <= End2) Then
                flag_rooz_az_mah = True
                startDate = CreateDate(start2)
                EndDate = CreateDate(End2)
            End If
        End If

        If (flag_rooz_az_mah = False) Then
            sahm = dt.Rows(0).Item("TypeSahm3").ToString
            Dim start3 As Integer = dt.Rows(0).Item("TypeSahmDateStart3").ToString
            Dim End3 As Integer = dt.Rows(0).Item("TypeSahmDateEnd3").ToString
            If (rooz >= start3 And rooz <= End3) Then
                flag_rooz_az_mah = True
                startDate = CreateDate(start3)
                EndDate = CreateDate(End3)
            End If
        End If

        If (flag_rooz_az_mah = False) Then
            sahm = dt.Rows(0).Item("TypeSahm4").ToString
            Dim start4 As Integer = dt.Rows(0).Item("TypeSahmDateStart4").ToString
            Dim End4 As Integer = dt.Rows(0).Item("TypeSahmDateEnd4").ToString
            If (rooz >= start4 And rooz <= End4) Then
                flag_rooz_az_mah = True
                startDate = CreateDate(start4)
                EndDate = CreateDate(End4)
            End If
        End If


        If (flag_rooz_az_mah = False) Then
            Panel1.Enabled = False
            hoshdarLbl.Text = "امروز مجاز به رزرو نمیباشید"
            Exit Sub
        End If

        '        sqlstr2 = String.Format("SELECT reservID  " & _
        '       " FROM resrevationTbl " & _
        '" WHERE (perID = {0}) AND (stat <> 0) AND (tarix >= '{1}' AND tarix <= '{2}')", Session("UserId"), startDate, EndDate)

        sqlstr2 = String.Format("SELECT COALESCE(SUM(dbo.GetSahmReserv(dbo.GetCountFood(reservID), dbo.GetTakafol(reservID))),0) as SUMMM " & _
       " FROM resrevationTbl " & _
" WHERE (perID = {0}) AND (stat <> 0) AND (tarix >= '{1}') AND (tarix <= '{2}')", Session("UserId"), startDate, EndDate)


        Dim sqlster As String = "SELECT        TOP (200) tarix         FROM resrevationTbl " & _
                 " WHERE        (perID = " & Session("UserId") & ") AND (tarix >= '" & startDate & "') AND (tarix <= '" & EndDate & "') AND (stat = 1)"
        Dim dttt As New DataTable
        dttt = ClsDataTable.GetDataTable(sqlster)
        Dim tedadSahmEstef As Integer
        tedadSahmEstef = dttt.Rows.Count


        'dt2 = ClsDataTable.GetDataTable(sqlstr2)

        Dim sum As Integer = Val(ClsDataTable.GetCommandScalar(sqlstr2))

        sahmieLbl.Text = sahm
        estfadeLbl.Text = sum
        bagiLbl.Text = Val(sahmieLbl.Text) - Val(tedadSahmEstef)
        LblTakafolCount.Text = GetTakafol()
        LblMaxGaza.Text = Val(bagiLbl.Text) * Val(LblTakafolCount.Text)
        HdnMaxGaza.Value = Val(bagiLbl.Text) * Val(LblTakafolCount.Text)
        If tedadSahmEstef >= sahm Then
            Panel1.Enabled = False
            hoshdarLbl.Text = "سهمیه شما در این ماه تمام شده است"
            Exit Sub
            'load_gridreserveInMonth()
        End If



      

    End Sub

    Private Function CreateDate(ruuz As String) As String
        Dim tarix As String = ""
        Dim sal As String = ClsDateTime.getSal()
        Dim mah As String = ClsDateTime.getMah()
        If mah.Length = 1 Then
            mah = "0" & mah
        End If
        tarix = sal + "/" + mah + "/"
        If ruuz.Length = 1 Then
            ruuz = "0" & ruuz
        End If
        tarix = tarix & ruuz
        Return tarix
    End Function

    Private Function GetTakafol() As Integer
        Dim res As Integer

        Dim sqlstr As String = "SELECT COUNT(*)  FROM Sponsorship  WHERE (empID = N'" + Session("codePersoneli").ToString + "') AND (IsActive = 1) "
        'AND (credit > N'" & tarix & "')"
        res = Val(ClsDataTable.GetCommandScalar(sqlstr))
        res += 1
        Return res
    End Function

    Private Function GetCountReservByFoodType(tempID As Integer, FoodTypeID As Integer) As Integer
        Dim res As Integer

        Dim sqlstr As String = String.Format("SELECT COALESCE (SUM(reservDtlTmpTbl.tedad), 0) " & _
        " FROM  reservDtlTmpTbl INNER JOIN       menuTbl ON reservDtlTmpTbl.menuID = menuTbl.menuID " & _
            " WHERE (reservDtlTmpTbl.tempID = {0}) AND (menuTbl.TypeFoodID = {1}) ", tempID, FoodTypeID)
        res = Val(ClsDataTable.GetCommandScalar(sqlstr))
        Return res
    End Function

    Private Function GetIsYaraneh(FoodId As Integer) As Boolean
        Dim res As Boolean

        Dim sqlstr As String = "SELECT IsYaraneh   FROM menuTbl  WHERE (menuID = " & FoodId & ")"
        res = ClsDataTable.GetCommandScalar(sqlstr)

        Return res

    End Function

    Private Function GetFoodType(FoodId As Integer) As Integer
        Dim res As Integer

        Dim sqlstr As String = "SELECT TypeFoodID   FROM menuTbl  WHERE (menuID = " & FoodId & ")"
        res = ClsDataTable.GetCommandScalar(sqlstr)
        Return res

    End Function

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

    Protected Sub btnTemp_Click(sender As Object, e As EventArgs) Handles btnTemp.Click
        
        saveTemp(Val(cmbTakafol.SelectedValue))

    End Sub
    Private Sub saveTemp(relid As Int16)
        If tedad.Text = 0 Or tedad.Text < 0 Then
            hoshdarLbl.Text = "تعداد نمی تواند صفر یا کوچکتر از صفر باشد"
            Exit Sub


        Else
            If Val(tedad.Text) > Val(LblTakafolCount.Text) Then
                hoshdarLbl.Text = "تعداد نمی تواند بیشتر از سهمیه باشد"
                msg_box("تعداد نمی تواند بیشتر از سهمیه باشد")
                Exit Sub
            End If
            hoshdarLbl.Text = ""

            If Val(hdn_tmp.Value) = 0 Then
                hdn_tmp.Value = ClsGetMaxId.GetMaxId("reservDtlTmpTbl", "tempID")
            End If


            Dim foodType As Integer = GetFoodType(menuCmb.SelectedValue)
            Dim isyaraneh As Boolean = GetIsYaraneh(menuCmb.SelectedValue)

            If isyaraneh = True Then
                Dim countOld As Integer = GetCountReservByFoodType(Val(hdn_tmp.Value), foodType)
                Dim countnew As Integer = countOld + Val(tedad.Text)

                If countnew > Val(HdnMaxGaza.Value) Then
                    hoshdarLbl.Text = "سقف سهمیه پر میباشد." & vbLf & "ظرفیت مجاز شما :" & HdnMaxGaza.Value
                    Exit Sub
                End If
            End If

            save_tmp(isyaraneh, relid)
            GridTemp.Visible = True
            load_grid_tmp()
            btnReserve.Visible = True

        End If

    End Sub
    Private Sub load_grid_tmp()

        Dim sqlstr As String = "SELECT locationValue,IDD, tedad, tempID, userID, menu,takafol,nesbat" & _
 " from tempMenuReservVW " & _
                  " WHERE     (userID = " & Session("UserId") & ") AND (tempID = " & Val(hdn_tmp.Value) & ") "

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then


            GridTemp.DataSource = dt
            GridTemp.DataBind()

            'Dim sumj, arzesh, gimatGaza As Integer
            'sumj = 0
            'For i = 0 To dt.Rows.Count - 1
            '    sumj = sumj + Val(dt.Rows(i).Item("baha").ToString())
            '    arzesh = arzesh + Val(dt.Rows(i).Item("arzeshafzude").ToString())
            '    gimatGaza = gimatGaza + Val(dt.Rows(i).Item("gimatGaza").ToString())
            'Next

       
        Else
            btnReserve.Visible = False
            GridTemp.Visible = False
            'PanelTempGimat.Visible = False
        End If
      
    End Sub

    Private Sub save_tmp(isyaraneh As Boolean, relID As Int32)
        If hdn_Final_key.Value = 0 Then
            hdn_Final_key.Value = ClsGetMaxId.GetMaxId("resrevationTbl", "reservID")
        End If

        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand
        Try
            cmm.CommandText = "reserveTempPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@reservID", SqlDbType.Int)
            cmm.Parameters("@reservID").Value = hdn_Final_key.Value

            cmm.Parameters.Add("@tempID", SqlDbType.Int)
            cmm.Parameters("@tempID").Value = hdn_tmp.Value

            cmm.Parameters.Add("@resturanID", SqlDbType.Int)
            cmm.Parameters("@resturanID").Value = resturanCmb.SelectedValue

            cmm.Parameters.Add("@menuID", SqlDbType.Int)
            cmm.Parameters("@menuID").Value = Val(menuCmb.SelectedValue)

            cmm.Parameters.Add("@tedad", SqlDbType.Int)
            cmm.Parameters("@tedad").Value = Val(tedad.Text)

            cmm.Parameters.Add("@userID", SqlDbType.Int, 5)
            cmm.Parameters("@userID").Value = Session("UserId")

            cmm.Parameters.Add("@relationID", SqlDbType.Int, 5)
            cmm.Parameters("@relationID").Value = relID


            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                ' msg_box("  عملیات با موفقیت انجام شد ")
                btnReserve.Visible = True
                'resturanCmb.Enabled = False
                'PanelTempGimat.Visible = True
            Else
                ' msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            If isyaraneh = True Then
                HdnCountGazaSef.Value = Val(HdnCountGazaSef.Value) - Val(tedad.Text)
            End If

            ' msg_box(ex.Message.ToString)
        End Try


    End Sub

    Protected Sub btnReserve_Click(sender As Object, e As EventArgs) Handles btnReserve.Click
        Dim sqlstrSS As String
        sqlstrSS = "SELECT     TOP (1) startTime, stopTime  FROM startStopTbl WHERE     (CodeClosed = 0) "
        Dim dtss As New DataTable
        dtss = ClsDataTable.GetDataTable(sqlstrSS)
        Dim startt As Integer = dtss.Rows(0).Item("startTime").ToString
        Dim stopp As Integer = dtss.Rows(0).Item("stopTime").ToString
        If times >= startt And times <= stopp Then
            Call save_final()

            btnReserve.Visible = False
        Else
            msg_box("وقت رزرو تمام شده است")
        End If

    End Sub

  
    Private Sub save_final()

        Dim radif As Integer = radifCls.GetMaxId(tarix, resturanCmb.SelectedValue)
        hdn_Final_key.Value = ClsGetMaxId.GetMaxId("resrevationTbl", "reservID")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "reserveResturanPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@userID", SqlDbType.Int, 10)
            cmm.Parameters("@userID").Value = Session("UserId")

            cmm.Parameters.Add("@tempID", SqlDbType.Int)
            cmm.Parameters("@tempID").Value = hdn_tmp.Value

            cmm.Parameters.Add("@reservID", SqlDbType.Int)
            cmm.Parameters("@reservID").Value = hdn_Final_key.Value

            cmm.Parameters.Add("@radif", SqlDbType.Int)
            cmm.Parameters("@radif").Value = radif

            cmm.Parameters.Add("@resturanID", SqlDbType.Int)
            cmm.Parameters("@resturanID").Value = resturanCmb.SelectedValue

            cmm.Parameters.Add("@barnameID", SqlDbType.Int, 5)
            cmm.Parameters("@barnameID").Value = 0
            'date_p.GetMonth(Now)
            'Dim date_p As New Globalization.PersianCalendar
            Dim clsDt As New ClsDateTime

            Dim mah As Integer = clsDt.getmonthNow


            cmm.Parameters.Add("@mah", SqlDbType.Int)

            cmm.Parameters("@mah").Value = mah

            cmm.Parameters.Add("@perID", SqlDbType.Int)
            cmm.Parameters("@perID").Value = Session("UserId")

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Parameters.Add("@maxResCode", SqlDbType.Int, 10)
            cmm.Parameters("@maxResCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            'Dim maxCode As String = cmm.Parameters("@maxResCode").Value
            Dim pm As String = cmm.Parameters("@pm").Value
            If pm.Length > 0 Then
                msg_box("قبلا یکبار ذخیره شده است")
                hoshdarLbl.Text = "قبلا یکبار ذخیره شده است"
            Else
                If result = 0 Then
                    'msg_box("ردیف " & radif & " در " & resturanCmb.SelectedItem.Text & " برای شما رزرو شد ")
                    'hoshdarLbl.Text = "ردیف " & radif & "   رزرو شد "
                    msg_box(" رزرو شد ")
                    hoshdarLbl.Text = " رزرو شد "
                    Panel1.Enabled = False
                    btnReserve.Visible = False
                    btnTemp.Visible = False
                    GridTemp.Enabled = False

                    hiddel()
                    checkQouata()
                    'checkQouata2()
                    loadGridEbtal()
                Else
                    msg_box(cmm.Parameters("@pm").Value)
                End If
            End If


        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub resturanCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles resturanCmb.SelectedIndexChanged
        load_Times()
        load_Menu()
        loadTakafol()
    End Sub
    Private Sub loadTakafol()
        Dim dtt As New DataTable
        Dim sqlstrt As String = ""
        If resturanCmb.SelectedValue = 1 Then
            sqlstrt = "SELECT        TOP (200) Sponsorship.empID, Sponsorship.fstName + ' ' + Sponsorship.Name AS takafol, Relations.Name AS nesbat, Sponsorship.relation, Sponsorship.relation, Sponsorship.ID " & _
         " FROM            Sponsorship INNER JOIN                          Relations ON Sponsorship.relation = Relations.ID " & _
                         "WHERE        (Sponsorship.empID = '" & Session("codePersoneli") & "')  AND (Sponsorship.relation = 1 OR       Sponsorship.relation = 5 OR  Sponsorship.relation = 7 OR  Sponsorship.relation = 8)"
        ElseIf resturanCmb.SelectedValue = 2 Then

            sqlstrt = "SELECT        TOP (200) Sponsorship.empID, Sponsorship.fstName + ' ' + Sponsorship.Name AS takafol, Relations.Name AS nesbat, Sponsorship.relation, Sponsorship.relation, Sponsorship.ID " & _
      " FROM            Sponsorship INNER JOIN                          Relations ON Sponsorship.relation = Relations.ID " & _
                      "WHERE        (Sponsorship.empID = '" & Session("codePersoneli") & "')  AND (Sponsorship.relation = 2 OR       Sponsorship.relation = 3 OR  Sponsorship.relation = 4 OR  Sponsorship.relation = 4)"
        Else
            sqlstrt = "SELECT        TOP (200) Sponsorship.empID, Sponsorship.fstName + ' ' + Sponsorship.Name AS takafol, Relations.Name AS nesbat, Sponsorship.relation, Sponsorship.relation, Sponsorship.ID " & _
        " FROM            Sponsorship INNER JOIN                          Relations ON Sponsorship.relation = Relations.ID " & _
                        "WHERE        (Sponsorship.empID = '10000') "
        End If

     
            dtt = ClsDataTable.GetDataTable(sqlstrt)
            cmbTakafol.DataSource = dtt
        If dtt.Rows.Count > 0 Then
            cmbTakafol.Visible = True
            btnTemp.Visible = True
            Label5.Visible = True
            cmbTakafol.DataValueField = "ID"
            cmbTakafol.DataTextField = "takafol"
            cmbTakafol.DataBind()
        Else
            cmbTakafol.Visible = False
            btnTemp.Visible = False
            Label5.Visible = False

        End If






    End Sub
    Private Sub hiddel()
        GridTemp.Rows.Item(5).Visible = False

    End Sub
    Protected Sub GridTemp_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridTemp.RowCommand
        If e.CommandName = "del" Then
            Dim cnn As New SqlConnection
            Dim radif As Int64 = e.CommandArgument
            radif = Val(GridTemp.Rows(radif).Cells(0).Text.ToString)
            '  ins_temp(3, radif)
            Dim cmm As New SqlCommand
            cmm.CommandText = " delete   reservDtlTmpTbl      WHERE   (IDD = " & radif & ")"
            Dim constr As String = ClsConnection.GetConn
            cnn.ConnectionString = constr
            cmm.Connection = cnn
            cnn.Open()
            cmm.ExecuteNonQuery()
            cnn.Close()
            'PanelGimat.Visible = False
            '  msg_box("ردیف مورد نظر حذف شد")
            hoshdarLbl.Text = "ردیف مورد نظر حذف شد"

            load_grid_tmp()

        End If
    End Sub


    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("index.aspx")
    End Sub

    
    Private Sub loadGridEbtal()
        Dim sqlstr As String
        Dim tarix As String = ClsDateTime.GetDateNow
        ' sqlstr = "SELECT    * from reserveVW where perID = " & Session("UserId") & " and tarix = '" & tarix & "'"
        '        sqlstr = "SELECT        resrevationTbl.reservID, resrevationTbl.resturanID, resrevationTbl.barnameID, resrevationTbl.perID, resrevationTbl.tarix, resrevationTbl.saat, resrevationTbl.mah, " & _
        '        " resrevationTbl.stat, resrevationTbl.radif, barnameTbl.valuee, refahiLocationTbl.locationValue " & _
        '" FROM            resrevationTbl INNER JOIN " & _
        '                         " refahiLocationTbl ON resrevationTbl.resturanID = refahiLocationTbl.locID INNER JOIN " & _
        '                        " barnameTbl ON resrevationTbl.barnameID = barnameTbl.timeID " & _
        '          " WHERE        (resrevationTbl.stat = 1) AND (resrevationTbl.perID = " & Session("UserId") & ") AND (resrevationTbl.tarix = '" & tarix & "') "
        sqlstr = "select * from ebtalGridVW WHERE        (tarix = '" & tarix & "') AND (perID = " & Session("UserId") & ")"
        Dim dtt As New DataTable
        'If dtt.Rows.Count > 0 Then
        dtt = ClsDataTable.GetDataTable(sqlstr)
        If dtt.Rows.Count > 0 Then
            GridTemp.Visible = True
            Button1.Visible = True

            grdReserve.DataSource = dtt
            grdReserve.DataBind()

           
            Dim resevID As Integer = dtt.Rows(0).Item("reservID").ToString
            HiddenField1.Value = resevID

        Else
            'hoshdarLbl.Text = "امروز رزروی ندارید"
            grdReserve.Visible = False
            GridTemp.Visible = False
            Button1.Visible = False

            load_Resturan()

            checkQouata()

        End If

    End Sub

    Private Sub ebtal()
        'If hdn_Final_key.Value = "new" Then txt_UnitsCode.Text = ClsGetMaxId.GetMaxId("tblUnits", "UnitsCode")
        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Try
            Dim cmm As New SqlCommand

            cmm.CommandText = "reservEbtalPrc"
            cmm.CommandType = CommandType.StoredProcedure

            cmm.Parameters.Add("@reservID", SqlDbType.Int)
            cmm.Parameters("@reservID").Value = HiddenField1.Value

            cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
            cmm.Parameters("@pm").Direction = ParameterDirection.Output

            cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
            cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
            cmm.Connection = cnn

            Dim result As Integer
            cmm.ExecuteNonQuery()
            result = cmm.Parameters("@ResultCode").Value
            If result = 0 Then
                msg_box("رزرو ابطال شد")
                hoshdarLbl.Text = "رزرو ابطال شد"
                Button1.Enabled = False
               
            Else
                msg_box(cmm.Parameters("@pm").Value)
            End If
        Catch ex As Exception
            msg_box(ex.Message.ToString)
        End Try


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
       
        ebtal()
        loadGridEbtal()
    End Sub

  

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Session("jens") = 1 And resturanCmb.SelectedValue = 2 Then
            msg_box("شما مجاز به استفاده از استخر بانوان نمیباشد")
            'resturanCmb.SelectedValue = 1
            resturanCmb.Focus()

            Exit Sub
        ElseIf Session("jens") = 2 And resturanCmb.SelectedValue = 1 Then
            msg_box("شما مجاز به استفاده از استخر آقایان نمیباشد")
            'resturanCmb.SelectedValue = 2 And resturanCmb.SelectedValue = 1
            resturanCmb.Focus()
            Exit Sub
        Else
            saveTemp(0)
        End If

    End Sub

    
End Class
