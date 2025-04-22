Imports System.Data
Imports System.Data.SqlClient

Partial Class frmBrowsSysFull
    Inherits System.Web.UI.Page
    Dim dtaspx As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If
        If Me.IsPostBack = True Then
            loadGrid()
            loadGridTakafol()

        Else
            Image1.ImageUrl = "images/person_pic/person.ico"
        End If

        load_Units()
        load_nostexdam()
        load_ellatfalli()
    End Sub

    Private Sub load_Units()
        Dim dt As New DataTable
        Dim cls_ As New ClsUnits
        dt = cls_.GetAllUnitsDDL
        CmbUnit.DataSource = dt
        CmbUnit.DataValueField = "UnitsCode"
        CmbUnit.DataTextField = "UnitsName"
        CmbUnit.DataBind()

    End Sub
    

    Private Sub load_nostexdam()
        Dim dt As New DataTable
        Dim cls_ As New ClsNoStexdam
        dt = cls_.GetAllUnitsDDL
        noeestexdam.DataSource = dt
        noeestexdam.DataValueField = "id"
        noeestexdam.DataTextField = "value"
        noeestexdam.DataBind()

    End Sub
    Private Sub load_ellatfalli()
        Dim dt As New DataTable
        Dim cls_ As New ClsBuilding
        dt = cls_.GetAllBuildingDDL
        ellat.DataSource = dt
        ellat.DataValueField = "BuildingCode"
        ellat.DataTextField = "BuildingName"
        ellat.DataBind()

    End Sub
#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmBrowsSysFull.aspx", Val(Session("UserId")))
     
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





    

    Private Sub find_pc()

        Dim ClsB_ As New ClsBrowsSystems
        Dim dt As New DataTable
        dt = ClsB_.GetSystems(Val(codepersonel.Text))

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("rack").ToString = True Then
                msg_box("اين شماره اموال براي " & " Rack  " & "ثبت شده است")
                Exit Sub
            End If
            TextBox20.Text = dt.Rows(0).Item("nam").ToString
            TextBox2.Text = dt.Rows(0).Item("BuildingName").ToString
            TextBox9.Text = dt.Rows(0).Item("SystemTabage").ToString
            TextBox5.Text = dt.Rows(0).Item("UnitsName").ToString

            TextBox11.Text = dt.Rows(0).Item("SystemRoom").ToString
            TextBox1.Text = dt.Rows(0).Item("SystemRoomNum").ToString


            ' txt_vaziat.Text = dt.Rows(0).Item("CodeClosed").ToString
            'Button1.Enabled = True
        Else
            msg_box("سيستمي با اين شماره اموال موجود نيست")
            TextBox2.Text = ""
            TextBox9.Text = ""
            TextBox5.Text = ""
            TextBox20.Text = ""
            TextBox11.Text = ""
            TextBox1.Text = ""


            DataGridView1.DataBind()
        

            'Button1.Enabled = False
            Exit Sub
        End If
        If dt.Rows(0).Item("CodeClosed").ToString = True Then
            msg_box("اين سيستم رده خارج شده است")

        Else

        End If

    End Sub

  

    

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
       
        If codepersonel.Text = "" Then
            msg_box("کد پرسنلی را وارد کنید")

            codepersonel.Focus()
            Exit Sub
        End If
        lblellat.Visible = False
        tarixDisabled.Visible = False
        lblellattarix.Visible = False
        ellat.Visible = False

        clearform()

        loadperson()


    End Sub
    Private Sub clearform()
        Image1.ImageUrl = "images/person_pic/person.ico"
        salkar.Text = 0
        mahkar.Text = 0
        ruzkar.Text = 0
        salsabege.Text = 0
        mahsabege.Text = 0
        ruzsabege.Text = 0
        salzian.Text = 0
        mahzian.Text = 0
        ruzzian1.Text = 0
        salsaxtno.Text = 0
        mahsaxno.Text = 0
        ruzsaxtno.Text = 0

        salsaxtsher.Text = 0
        mahsaxtsher.Text = 0
        ruzsaxtsher.Text = 0
        Panel4.Visible = True
        Panel5.Visible = True
        Panel6.Visible = True
        Panel7.Visible = True
        Panel8.Visible = True

        adres.Text = ""
        TextBox1.Text = ""
        semat.Text = ""
        TextBox11.Text = ""
        TextBox20.Text = ""
        TextBox2.Text = ""
        TextBox9.Text = ""
        TextBox5.Text = ""
        codemelli.Text = ""
        father.Text = ""
        shsh.Text = ""
        mahalesodur.Text = ""
        shserial.Text = ""
        vaziatsh.SelectedIndex = 0
        mahaletavalod.Text = ""
        tarixtavalod.Text = ""
        noeestexdam.SelectedValue = 0
        ' ellat.SelectedValue = 0
        tarixstexdam.Text = ""
        vaziatathol.SelectedIndex = 0
        teldaxele.Text = ""
        telmanzel.Text = ""
        mobile.Text = ""
        mazhab.SelectedIndex = 0
        madrak.Text = ""
        shgozarname.Text = ""
        din.SelectedIndex = 0
        laptopBtn.Enabled = False
        xodroBtn.Enabled = False
        mihmanBtn.Enabled = False
    End Sub
    Private Sub chekMihman()
        Dim dsh2 As New DataTable

        Dim sqlstr As String

        sqlstr = "SELECT  mizbanId from tblResort where mizbanId = " & perid.Text & " "
        dsh2 = ClsDataTable.GetDataTableHerasat(sqlstr)
        If dsh2.Rows.Count > 0 Then
            mihmanBtn.Enabled = True
            mihmanBtn.Text = dsh2.Rows.Count & " " & "نفر ملاقات کننده دارد"
            mihmanBtn.Font.Bold = True
        Else
            mihmanBtn.Enabled = False
            mihmanBtn.Text = "ملاقات کننده ندارد"
            mihmanBtn.Font.Bold = False
        End If


    End Sub

    Private Sub chekxodro()
        Dim dsh2 As New DataTable

        Dim sqlstr As String

        sqlstr = "SELECT  perid from xodroMojavez where perid = " & perid.Text & " "
        dsh2 = ClsDataTable.GetDataTableHerasat(sqlstr)
        If dsh2.Rows.Count > 0 Then
            xodroBtn.Enabled = True
            xodroBtn.Text = "مجوز خودرو دارد"
            xodroBtn.Font.Bold = True
        Else
            xodroBtn.Enabled = False
            xodroBtn.Text = "مجوز خودرو ندارد"
            xodroBtn.Font.Bold = False
        End If


    End Sub

    Private Sub cheklaptop()
        Dim dsh2 As New DataTable

        Dim sqlstr As String

        sqlstr = "SELECT  * from labtop where codepersoneli = " & codepersonel.Text & " "
        dsh2 = ClsDataTable.GetDataTableHerasat(sqlstr)
        If dsh2.Rows.Count > 0 Then
            laptopBtn.Enabled = True
            laptopBtn.Text = "مجوز لپ تاپ دارد"
            laptopBtn.Font.Bold = True
        Else
            laptopBtn.Enabled = False
            laptopBtn.Text = "مجوز لپ تاپ ندارد"
            laptopBtn.Font.Bold = False
        End If


    End Sub

    Private Sub loadperson()
        Dim dsh2 As New DataTable

        Dim sqlstr As String

        sqlstr = "SELECT     personel.*, tblUnits.UnitsName  " & _
                   " FROM         personel INNER JOIN " & _
                      "tblUnits ON personel.UnitsCode = tblUnits.UnitsCode " & _
                          " WHERE     (personel.codepersoneli = " & codepersonel.Text & ") "
        dsh2 = ClsDataTable.GetDataTable(sqlstr)
        If dsh2.Rows.Count > 0 Then
            Image1.ImageUrl = "images/person_pic/" & codepersonel.Text & ".jpg"
            TextBox2.Text = dsh2.Rows(0).Item("nam").ToString & " " & dsh2.Rows(0).Item("family").ToString
            TextBox5.Text = dsh2.Rows(0).Item("tarixestexdam").ToString
            TextBox9.Text = dsh2.Rows(0).Item("postname").ToString
            TextBox20.Text = dsh2.Rows(0).Item("codebime").ToString
            TextBox11.Text = dsh2.Rows(0).Item("madrak").ToString
            TextBox1.Text = dsh2.Rows(0).Item("reshte").ToString
            perid.Text = dsh2.Rows(0).Item("ID").ToString

            CmbUnit.SelectedValue = dsh2.Rows(0).Item("UnitsCode").ToString
            'End If
            codemelli.Text = dsh2.Rows(0).Item("codeMelli").ToString
            father.Text = dsh2.Rows(0).Item("father").ToString
            shsh.Text = dsh2.Rows(0).Item("shsh").ToString
            mahalesodur.Text = dsh2.Rows(0).Item("mahalesodur").ToString
            shserial.Text = dsh2.Rows(0).Item("shserial").ToString

            mahaletavalod.Text = dsh2.Rows(0).Item("mahaletavalod").ToString
            tarixtavalod.Text = dsh2.Rows(0).Item("tarixtavalod").ToString


            tarixstexdam.Text = dsh2.Rows(0).Item("tarixestexdam").ToString
            vaziatathol.SelectedValue = dsh2.Rows(0).Item("vaziattahol").ToString
            teldaxele.Text = dsh2.Rows(0).Item("teldaxeli").ToString
            telmanzel.Text = dsh2.Rows(0).Item("telmanzel").ToString
            mobile.Text = dsh2.Rows(0).Item("mobile").ToString
            mazhab.SelectedValue = dsh2.Rows(0).Item("mazhab").ToString
            madrak.Text = dsh2.Rows(0).Item("madrak").ToString
            shgozarname.Text = dsh2.Rows(0).Item("shgozarname").ToString
            din.SelectedValue = dsh2.Rows(0).Item("din").ToString
            semat.Text = dsh2.Rows(0).Item("postname").ToString

            adres.Text = dsh2.Rows(0).Item("adres").ToString

            noeestexdam.SelectedValue = dsh2.Rows(0).Item("typ").ToString

            If dsh2.Rows(0).Item("CodeClosed").ToString = True Then
                msg_box("اين پرسنل غیرفعال شده است")
                lblellat.Visible = True
                tarixDisabled.Visible = True
                lblellattarix.Visible = True
                ellat.Visible = True

                tarixDisabled.Text = dsh2.Rows(0).Item("tarixDisable").ToString
                ellat.SelectedValue = dsh2.Rows(0).Item("ellatevaziat").ToString
            End If





            loadGrid()
            loadGridTakafol()
            loadsabege()
            loadzian()
            loadkarkard()
            loadziansherkati()
            loadziansherkatiNO()
            cheklaptop()
            chekMihman()
            chekxodro()
        Else

            msg_box("کد پرسنلی مورد نظر در پایگاه داده موجود نمی باشد")
            clearform()
        End If
    End Sub




    Private Sub loadGrid()



        Dim sqlstr As String

        sqlstr = "SELECT     savabeg.id, savabeg.tarixshoru, savabeg.tarixpayan, savabeg.karkardSal + '-' + savabeg.karkardMah + '-' + savabeg.karkardDay AS karkard, " & _
                     " savabeg.sabegeSal + '-' + savabeg.sabegeMah + '-' + savabeg.sabegeDay AS sabege, savabeg.tozih, mashaghel.postname, sazman.sazmanName,  " & _
                     " saxtzian.value, mashaghel.saxt , sherkatiType.value AS sherkati  " & _
                    " FROM         savabeg INNER JOIN " & _
                     " mashaghel ON savabeg.onvan = mashaghel.id INNER JOIN " & _
                     " personel ON savabeg.perID = personel.ID INNER JOIN " & _
                     " sazman ON savabeg.sazman = sazman.id INNER JOIN " & _
                     " saxtzian ON mashaghel.saxt = saxtzian.id  INNER JOIN " & _
                     "  sherkatiType ON sazman.sherkati = sherkatiType.id " & _
                     " WHERE(personel.codePersoneli = " & codepersonel.Text & ") " & _
                    " ORDER BY savabeg.tarixshoru"

        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        DataGridView1.DataSource = dt
        DataGridView1.DataBind()




    End Sub
    Private Sub loadGridTakafol()



        Dim sqlstr As String

        sqlstr = "SELECT     id,empID, fstName, Name, birthday, credit, perid, Expr1 " & _
       " FROM vwTakfol WHERE     (empID = '" & codepersonel.Text & "')"


        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable(sqlstr)
        takafolGRD.DataSource = dt
        takafolGRD.DataBind()




    End Sub
    Private Sub loadkarkard()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountKar(perID) AS sal, dbo.GetMounthsCountKar(perID) AS mah, dbo.GetDaysCountKar(perID) AS ruz " & _
       " FROM savabeg         WHERE(perid = " & perid.Text & ")"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salkar.Text = dts.Rows(0).Item("sal").ToString
            mahkar.Text = dts.Rows(0).Item("mah").ToString
            ruzkar.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub

    Private Sub loadsabege()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCount(perID) AS sal, dbo.GetMounthsCount(perID) AS mah, dbo.GetDaysCount(perID) AS ruz " & _
       " FROM savabeg         WHERE(perid = " & perid.Text & ")"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salsabege.Text = dts.Rows(0).Item("sal").ToString
            mahsabege.Text = dts.Rows(0).Item("mah").ToString
            ruzsabege.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub
    Private Sub loadzian()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountSaxt(savabeg.perID) AS sal, dbo.GetMounthsCountSaxt(savabeg.perID) AS mah, dbo.GetDaysCountSaxt(savabeg.perID) AS ruz " & _
      " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
           " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salzian.Text = dts.Rows(0).Item("sal").ToString
            mahzian.Text = dts.Rows(0).Item("mah").ToString
            ruzzian1.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub

    Private Sub loadziansherkati()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountSaxtsherkati(savabeg.perID) AS sal, dbo.GetMounthsCountSaxtsherkati(savabeg.perID) AS mah, dbo.GetDaysCountSaxtSherkati(savabeg.perID) AS ruz " & _
      " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
           " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salsaxtsher.Text = dts.Rows(0).Item("sal").ToString
            mahsaxtsher.Text = dts.Rows(0).Item("mah").ToString
            ruzsaxtsher.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub

    Private Sub loadziansherkatiNO()


        Dim dts As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT     dbo.GetYearsCountSaxtsherkatiNo(savabeg.perID) AS sal, dbo.GetMounthsCountSaxtsherkatiNo(savabeg.perID) AS mah, dbo.GetDaysCountSaxtSherkatiNo(savabeg.perID) AS ruz " & _
      " FROM         savabeg INNER JOIN    mashaghel ON savabeg.onvan = mashaghel.id " & _
           " WHERE(savabeg.perID = " & perid.Text & ") And (mashaghel.saxt = 1)"

        dts = ClsDataTable.GetDataTable(sqlstr)
        If dts.Rows.Count > 0 Then
            salsaxtno.Text = dts.Rows(0).Item("sal").ToString
            mahsaxno.Text = dts.Rows(0).Item("mah").ToString
            ruzsaxtno.Text = dts.Rows(0).Item("ruz").ToString
        End If


    End Sub













    Private Sub clear_form()
        TextBox2.Text = ""

        TextBox20.Text = ""

        TextBox11.Text = ""
        TextBox1.Text = ""
        TextBox9.Text = ""
        TextBox5.Text = ""

        DataGridView1.DataBind()

    End Sub


    'Protected Sub ASPxGrvHead_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles DataGridView1.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim value As Decimal = CDec(e.GetValue("SystemHDtlStatus"))
    '    If value = 0 Then
    '        e.Row.ForeColor = Drawing.Color.Green
    '        '   e.Row.BackColor = Drawing.Color.Aquamarine
    '    Else
    '        e.Row.ForeColor = Drawing.Color.Red
    '        '   e.Row.BackColor = Drawing.Color.LightGoldenrodYellow
    '    End If
    'End Sub


    Protected Sub mihmanBtn_Click(sender As Object, e As EventArgs) Handles mihmanBtn.Click
        Dim furl As String = "personelResort.aspx?PwhereSTR=" & Val(perid.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=600,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub laptopBtn_Click(sender As Object, e As EventArgs) Handles laptopBtn.Click
        Dim furl As String = "personelLapTop.aspx?PwhereSTR=" & Val(codepersonel.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=350,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub

    Protected Sub xodroBtn_Click(sender As Object, e As EventArgs) Handles xodroBtn.Click

        Dim furl As String = "personelXodro.aspx?PwhereSTR=" & Val(perid.Text) & ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", "window.open( '" + furl + "', null, 'height=250,width=970,status=yes,toolbar=yes,menubar=no,location=no,zoom=100%');", True)
    End Sub
End Class
