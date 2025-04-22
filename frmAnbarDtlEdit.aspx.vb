Imports System.Data
Imports System.Data.SqlClient

Partial Class frmAnbarDtlEdit
    Inherits System.Web.UI.Page
    Dim datS As String = ""
    Dim datS1 As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("super_vizer") = False Then
        '    meno_acc()
        'End If

        load_grid()
        If Me.IsPostBack = False Then

            load_cmbAnbar()
            load_cmb()
            load_cmbg()
            load_cmbmodel()
            load_holder()
            load_Bulding()
            load_Units()
        End If



    End Sub


#Region "All pages"

    Private Sub meno_acc()
        Dim meno_arr(10) As Boolean
        ' meno_arr = ClsAccounting.meno_acc(Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), Val(Session("UserId")))
        meno_arr = ClsAccounting.meno_acc("frmAnbarDtl.aspx", Val(Session("UserId")))
        ibtnAdd.Visible = meno_arr(1)
        ibtnEdit.Visible = meno_arr(2)
        ibtnSave.Visible = meno_arr(3)
        ibtnSaveNew.Visible = meno_arr(4)
        ibtnCancel.Visible = meno_arr(5)
        ibtnDelete.Visible = meno_arr(6)
        ibtnDeleted.Visible = meno_arr(7)
        ibtnHistory.Visible = meno_arr(8)
        ibtnDeactived.Visible = meno_arr(9)
        ibtnPrint.Visible = meno_arr(10)
        ibtnExit.Visible = meno_arr(11)
    End Sub

    Private Sub enable_disable()
        If ibtnSave.Enabled = False Then
            ibtnSave.Enabled = True
            ibtnSaveNew.Enabled = True
            ibtnCancel.Enabled = True
            'txt_SystemCode.Enabled = True
            ibtnEdit.Enabled = False
            ibtnDelete.Enabled = False
            ibtnAdd.Enabled = False
            ibtnDeleted.Enabled = False
            ibtnHistory.Enabled = False
            ibtnDeactived.Enabled = False
            ibtnPrint.Enabled = False
            ibtnExit.Enabled = False
            'GridView2.DataBind()
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

            cmbBulding.Enabled = True
            'cmbHolder.Enabled = True
            '' txt_SystemCode.Enabled = True
            'txt_SystemPhone.Enabled = True
            'txt_SystemRoom.Enabled = True
            txt_Garanty.Enabled = True
            cmbUnit.Enabled = True
            ' txt_SystemRoomNum.Enabled = True
            txt_SystemTabage.Enabled = True
            ' Button1.Enabled = True
            'txt_SystemRoomNum.Enabled = True

            Cmbg.Enabled = True
            Cmbgd.Enabled = True
            txt_AmvalTmp.Enabled = True
            txt_Serial.Enabled = True
            txt_Mrs.Enabled = True
            txt_Garanty.Enabled = True
            txt_waranti.Enabled = True
            'btnIn.Enabled = True
            '   txt_SystemTPP.Enabled = True
            txt_sgati.Enabled = True
            '  chkAntivirus.Enabled = True
            '  chkjoinDomain.Enabled = True
            ' chkPXE.Enabled = True
            txtNote.Enabled = True
            ' txt_tarixarid.Enabled = True
            cmbType.Enabled = True
            txt_tedad.Enabled = True
            CmbAnbar.Enabled = True
        Else
            CmbAnbar.Enabled = False
            txt_tedad.Enabled = False
            cmbType.Enabled = False
            ' txt_tarixarid.Enabled = False
            'GridView2.DataBind()
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

            cmbBulding.Enabled = False
            '  cmbHolder.Enabled = False
            'txt_SystemCode.Enabled = False
            '  txt_SystemPhone.Enabled = False
            '  txt_SystemRoom.Enabled = False
            txt_Garanty.Enabled = False
            cmbUnit.Enabled = False
            txt_SystemTabage.Enabled = False
            'Button1.Enabled = False
            ' txt_SystemRoomNum.Enabled = False
            ' txt_SystemCode.Enabled = False

            Cmbg.Enabled = False
            Cmbgd.Enabled = False
            txt_AmvalTmp.Enabled = False
            txt_Serial.Enabled = False
            txt_Mrs.Enabled = False
            txt_Garanty.Enabled = False
            txt_waranti.Enabled = False
            'btnIn.Enabled = False
            '  txt_SystemTPP.Enabled = False
            txt_sgati.Enabled = False
            '  chkAntivirus.Enabled = False
            '  chkjoinDomain.Enabled = False
            '  chkPXE.Enabled = False
            txtNote.Enabled = False
        End If

    End Sub

    Private Sub clear_form()
        ' txt_SystemCode.Text = ""
        cmbBulding.SelectedValue = 0
        txt_SystemTabage.Text = ""
        'cmbUnit.SelectedValue = 0

        '  cmbHolder.SelectedValue = 0
        'txt_SystemCode.Enabled = False
        '  txt_SystemPhone.Text = ""
        ' txt_SystemRoom.Text = ""
        txt_Garanty.Text = ""
        ' cmbUnit.SelectedValue = 0
        txt_SystemTabage.Text = ""
        ' txt_SystemRoomNum.Text = ""
        ' txt_ComputerName.Text = ""

        txt_AmvalTmp.Text = ""
        txt_Serial.Text = ""
        txt_Mrs.Text = ""
        'txt_SystemTPP.Text = ""
        txt_waranti.Text = ""
        TxtNote.Text = ""

        'chkAntivirus.Checked = False
        ' chkjoinDomain.Checked = False
        ' chkPXE.Checked = False
        ' txt_tarixarid.Text = ""
        txt_sgati.Text = ""
        txt_tedad.Text = 0
        'txt_GateName.Text = ""
        'CmbAnbar.SelectedValue = 0

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
#Region "load fitment"

    Private Function fillDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     BaseSystemHDtlCode, BaseSystemHDtlNmae, CodeClosed, opid, ArcTypeCode  FROM tblBaseSystemHDtl WHERE     (CodeClosed = 0) AND (BaseSystemHDtlCode <> 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function


#End Region


#Region "meno button"
    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSave.Click

        If txt_tedad.Text = " " Or txt_tedad.Text = 0 Then
            msg_box("تعداد حتما بايد وارد شود")
            Exit Sub
        End If

        If cmbType.SelectedValue <> 1 Then
            If Val(chek_tedad.Rows(0).Item("tedad").ToString) < Val(txt_tedad.Text) Then
                msg_box("از اين كالا فقط " & Val(chek_tedad.Rows(0).Item("tedad").ToString) & " عدد موجود است ")
                lblTotal.Visible = True
                lblTotal.Text = "موجودي = " & Val(chek_tedad.Rows(0).Item("tedad").ToString)
                Exit Sub
            Else
                Call save_final()
                lblTotal.Visible = False
                clear_form()
            End If
        Else
            Call save_final()
            lblTotal.Visible = False
            clear_form()
        End If



        'Call chek_systemCode()
        ' chkAntivirus.Checked = False
        '  chkjoinDomain.Checked = False
        ' chkPXE.Checked = False

    End Sub

    Private Function chek_tedad() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT  SUM(dtltedad) AS tedad   FROM tblDtlAnbar WHERE (tblDtlAnbar.kodeGate = " & Cmbg.SelectedValue & ") And (tblDtlAnbar.kodeModel = " & Cmbgd.SelectedValue & ") And (tblDtlAnbar.idAnbar = " & CmbAnbar.SelectedValue & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)

        Return dt
    End Function




    'Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
    '    New_()

    'End Sub

    Private Sub New_()
        hdn_Final_key.Value = "new"
        clear_form()
        'txt_SystemCode.Text = ClsGetMaxId.GetMaxId("tblSystem", "SystemCode")
        enable_disable()
    End Sub
    Private Sub save_()
        enable_disable()
        clear_form()
        hdn_Final_key.Value = 0
    End Sub

    Protected Sub ibtnSaveNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSaveNew.Click
        If txt_tedad.Text = " " Or txt_tedad.Text = 0 Then
            msg_box("تعداد حتما بايد وارد شود")
            Exit Sub
        End If
        If cmbType.SelectedValue = 1 And chek_tedad.Rows(0).Item("tedad") < Val(txt_tedad.Text) Then
            msg_box("از اين كالا فقط " & chek_tedad.Rows(0).Item("tedad") & " عدد موجود است ")
            lblTotal.Visible = True
            lblTotal.Text = "موجودي = " & chek_tedad.Rows(0).Item("tedad")
            Exit Sub
        Else
            Call save_final()
            lblTotal.Visible = False
        End If

        Call New_()
    End Sub

    Protected Sub ibtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEdit.Click
        Call edit_()
    End Sub
    Private Sub edit_()
        enable_disable()
        hdn_Final_key.Value = "edit"
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
        hdn_Final_key.Value = "del"
        Call save_final()
        enable_disable()
        clear_form()
    End Sub


    'Private Sub Deactived_()
    '    If hdn_deactivedcode.Value = "false" Then
    '        hdn_deactivedcode.Value = "true"
    '    Else
    '        hdn_deactivedcode.Value = "false"
    '    End If
    '    load_grid()
    'End Sub

#End Region


    Private Sub load_grid()
        Dim ClsB_ As New ClsAnbarEdit
        Dim dt As New DataTable
        dt = ClsB_.GetAllUnits
        grdFarei.DataSource = dt
        grdFarei.DataBind()

    End Sub



    Protected Sub ASPxGrvHead_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles grdFarei.RowCommand
        If e.CommandArgs.CommandName = "select" Then
            Dim ClsB As New ClsAnbarEdit
            Dim dt As New DataTable
            dt = ClsB.GetUnits(e.CommandArgs.CommandArgument.ToString())
            ' txt_SystemCode.Text = ""
            cmbBulding.SelectedValue = dt.Rows(0).Item("saxteman").ToString
            txt_SystemTabage.Text = dt.Rows(0).Item("tabage").ToString
            txtidkala.Text = dt.Rows(0).Item("IddtlKala").ToString
            txt_Garanty.Text = dt.Rows(0).Item("tarixgaranti").ToString
            txt_Tarixx.Text = dt.Rows(0).Item("WorkDateE").ToString
            txt_AmvalTmp.Text = dt.Rows(0).Item("amval").ToString
            txt_Serial.Text = dt.Rows(0).Item("serial").ToString
            txt_Mrs.Text = dt.Rows(0).Item("mrs").ToString

            txt_waranti.Text = dt.Rows(0).Item("tarixwaranti").ToString
            txtNote.Text = dt.Rows(0).Item("WorkNote").ToString


            txt_sgati.Text = dt.Rows(0).Item("tarixskati").ToString
            txt_tedad.Text = dt.Rows(0).Item("dtltedad").ToString

            CmbAnbar.SelectedValue = dt.Rows(0).Item("idAnbar").ToString
            Cmbg.SelectedValue = dt.Rows(0).Item("kodeGate").ToString
            Cmbgd.SelectedValue = dt.Rows(0).Item("kodeModel").ToString
            cmbType.SelectedValue = dt.Rows(0).Item("inout").ToString
            cmbStock.SelectedValue = dt.Rows(0).Item("stock").ToString

        End If
    End Sub


    Private Sub save_final()




        Dim cnn As New SqlConnection
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim cmm As New SqlCommand

        cmm.CommandText = "prcDtlAnbarEdit"
        cmm.CommandType = CommandType.StoredProcedure

        cmm.Parameters.Add("@IddtlKala", SqlDbType.Int, 6)
        cmm.Parameters("@IddtlKala").Value = txtidkala.Text

        cmm.Parameters.Add("@type_", SqlDbType.TinyInt, 1)
        If hdn_Final_key.Value = "del" Then
            cmm.Parameters("@type_").Value = 3
        ElseIf hdn_Final_key.Value = "edit" Then
            cmm.Parameters("@type_").Value = 2
            'ElseIf hdn_Final_key.Value = "new" Then
            '    cmm.Parameters("@type_").Value = 1
        End If


        cmm.Parameters.Add("@idAnbar", SqlDbType.SmallInt)
        cmm.Parameters("@idAnbar").Value = CmbAnbar.SelectedValue

        cmm.Parameters.Add("@kodeGate", SqlDbType.SmallInt)
        cmm.Parameters("@kodeGate").Value = Cmbg.SelectedValue

        cmm.Parameters.Add("@kodeModel", SqlDbType.Int)
        cmm.Parameters("@kodeModel").Value = Cmbgd.SelectedValue

        cmm.Parameters.Add("@amval", SqlDbType.Int)
        cmm.Parameters("@amval").Value = Val(txt_AmvalTmp.Text)

        cmm.Parameters.Add("@serial", SqlDbType.NVarChar, 100)
        cmm.Parameters("@serial").Value = txt_Serial.Text

        cmm.Parameters.Add("@mrs", SqlDbType.NVarChar, 100)
        cmm.Parameters("@mrs").Value = txt_Mrs.Text

        cmm.Parameters.Add("@tarixXarid", SqlDbType.VarChar, 10)
        cmm.Parameters("@tarixXarid").Value = txt_Tarixx.Text

        cmm.Parameters.Add("@tarixgaranti", SqlDbType.VarChar, 10)
        cmm.Parameters("@tarixgaranti").Value = txt_Garanty.Text

        cmm.Parameters.Add("@tarixwaranti", SqlDbType.VarChar, 10)
        cmm.Parameters("@tarixwaranti").Value = txt_waranti.Text

        cmm.Parameters.Add("@tarixskati", SqlDbType.VarChar, 10)
        cmm.Parameters("@tarixskati").Value = txt_sgati.Text

        cmm.Parameters.Add("@saxteman", SqlDbType.Int)
        cmm.Parameters("@saxteman").Value = cmbBulding.SelectedValue


        cmm.Parameters.Add("@tabage", SqlDbType.SmallInt)
        cmm.Parameters("@tabage").Value = Val(txt_SystemTabage.Text)

        cmm.Parameters.Add("@stock", SqlDbType.SmallInt)
        cmm.Parameters("@stock").Value = cmbStock.SelectedValue

        cmm.Parameters.Add("@vahed", SqlDbType.Int)
        cmm.Parameters("@vahed").Value = cmbUnit.SelectedValue

        cmm.Parameters.Add("@nameGate", SqlDbType.VarChar, 50)
        cmm.Parameters("@nameGate").Value = " "

        Dim tp As Integer = 0
        If cmbType.SelectedValue = 1 Then
            tp = 1
        Else
            tp = -1
        End If
        cmm.Parameters.Add("@TypeCode", SqlDbType.SmallInt)
        cmm.Parameters("@TypeCode").Value = tp

        cmm.Parameters.Add("@inOut", SqlDbType.SmallInt)
        cmm.Parameters("@inOut").Value = Val(cmbType.SelectedValue)

        cmm.Parameters.Add("@dtltedad", SqlDbType.Int)
        cmm.Parameters("@dtltedad").Value = Val(txt_tedad.Text)

        cmm.Parameters.Add("@WorkNote", SqlDbType.NVarChar, 2000)
        cmm.Parameters("@WorkNote").Value = txtNote.Text

        cmm.Parameters.Add("@userid", SqlDbType.Int, 10)
        cmm.Parameters("@userid").Value = Session("UserId")

        cmm.Parameters.Add("@pm", SqlDbType.NVarChar, 500)
        cmm.Parameters("@pm").Direction = ParameterDirection.Output

        cmm.Parameters.Add("@ResultCode", SqlDbType.TinyInt, 3)
        cmm.Parameters("@ResultCode").Direction = ParameterDirection.Output
        cmm.Connection = cnn


        Dim result As Integer
        cmm.ExecuteNonQuery()
        result = cmm.Parameters("@ResultCode").Value
        If result = 0 Then
            '      msg_box("Computer:  " & txt_ComputerName.Text & " Succecfull ")
            msg_box("  عملیات با موفقیت انجام شد ")
            ' Call save_()
        Else
            msg_box(cmm.Parameters("@pm").Value)
        End If

    End Sub


    Private Sub load_holder()
        Dim dt As New DataTable
        Dim cls_ As New ClsHolder
        dt = cls_.GetAllHolderDDL
        'cmbHolder.DataSource = dt
        'cmbHolder.DataValueField = "HolderCode"
        'cmbHolder.DataTextField = "names"
        'cmbHolder.DataBind()

    End Sub
    Private Sub load_Bulding()
        Dim dt As New DataTable
        Dim cls_ As New ClsBuilding
        dt = cls_.GetAllBuildingDDL
        cmbBulding.DataSource = dt
        cmbBulding.DataValueField = "BuildingCode"
        cmbBulding.DataTextField = "BuildingName"
        cmbBulding.DataBind()

    End Sub
    Private Sub load_Units()
        Dim dt As New DataTable
        Dim cls_ As New ClsUnits
        dt = cls_.GetAllUnitsDDL
        cmbUnit.DataSource = dt
        cmbUnit.DataValueField = "UnitsCode"
        cmbUnit.DataTextField = "UnitsName"
        cmbUnit.DataBind()

    End Sub



    Protected Sub Cmbg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmbg.SelectedIndexChanged
        'ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & a & "','" & b & "')</script>")
        load_cmbg()

    End Sub

    Private Sub load_cmbmodel()
        Dim dtmg As New DataTable

        Dim sqlstr = " SELECT        tblBaseModel.BaseModelName, tblBaseModel.BaseModelCode, tblBaseSystemHDtl.BaseSystemHDtlCode " & _
                 " FROM            tblBaseModel INNER JOIN " & _
                          " tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
        " WHERE(tblBaseModel.CodeClosed = 0)  " & _
           " ORDER BY tblBaseModel.BaseModelName"
        dtmg = ClsDataTable.GetDataTable(sqlstr)
        Cmbgd.DataSource = dtmg
        Cmbgd.DataTextField = "BaseModelName"
        Cmbgd.DataValueField = "BaseModelCode"
        Cmbgd.DataBind()
    End Sub

    Private Sub load_cmbg()
        Dim dtmg As New DataTable
      
        Dim sqlstr = " SELECT        tblBaseModel.BaseModelName, tblBaseModel.BaseModelCode, tblBaseSystemHDtl.BaseSystemHDtlCode " & _
                 " FROM            tblBaseModel INNER JOIN " & _
                          " tblBaseSystemHDtl ON tblBaseModel.BaseSystemHDtlCode = tblBaseSystemHDtl.BaseSystemHDtlCode " & _
        " WHERE(tblBaseModel.CodeClosed = 0) And (tblBaseSystemHDtl.BaseSystemHDtlCode = " & Cmbg.SelectedValue & ") " & _
           " ORDER BY tblBaseModel.BaseModelName"
        dtmg = ClsDataTable.GetDataTable(sqlstr)
        Cmbgd.DataSource = dtmg
        Cmbgd.DataTextField = "BaseModelName"
        Cmbgd.DataValueField = "BaseModelCode"
        Cmbgd.DataBind()
    End Sub

    Private Sub load_cmb()
        Dim dtg, dtm, dtmg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT        BaseSystemHDtlNmae, BaseSystemHDtlCode         FROM tblBaseSystemHDtl " & _
        " WHERE(CodeClosed = 0) ORDER BY BaseSystemHDtlNmae"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        Cmbg.DataSource = dtm
        Cmbg.DataTextField = "BaseSystemHDtlNmae"
        Cmbg.DataValueField = "BaseSystemHDtlCode"
        Cmbg.DataBind()

    End Sub

    Private Sub load_cmbAnbar()
        Dim dtg, dtm, dtmg As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     idAnbar, nameAnabr, stat " & _
        " FROM tblAnbar WHERE     (stat = 1)"
        dtm = ClsDataTable.GetDataTable(sqlstr)
        CmbAnbar.DataSource = dtm
        CmbAnbar.DataTextField = "nameAnabr"
        CmbAnbar.DataValueField = "idAnbar"
        CmbAnbar.DataBind()

    End Sub
    Protected Sub ibtnExit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnExit.Click

        If Session("UserType") = "admn" Then
            Response.Redirect("frmWorkTableAdmin.aspx")
        ElseIf Session("UserType") = "worker" Then
            Response.Redirect("index.aspx")
        ElseIf Session("UserType") = "user" Then
            Response.Redirect("frmWorkTableUsers.aspx")
        End If
    End Sub



    Protected Sub gen_comName()
        Dim comname As String
        Dim dt As New DataTable
        Dim clsB_ As New ClsBuilding
        dt = clsB_.GetBuilding(cmbBulding.SelectedValue, True)
        If dt.Rows.Count = 0 Then
            msg_box("لطفا ساختمان را انتخاب كنيد")
            Exit Sub
        End If
        comname = txt_AmvalTmp.Text & "-" & dt.Rows(0).Item("BuildingEngName") & "-" & "F" & txt_SystemTabage.Text
        ' txt_GateName.Text = comname
    End Sub





    Protected Sub ibtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAdd.Click
        New_()
    End Sub



    Protected Sub CmbAnbar_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbAnbar.SelectedIndexChanged

        load_cmb()
        load_cmbg()

    End Sub


End Class
