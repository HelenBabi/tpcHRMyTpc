Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Security.Principal
Imports System.Configuration
Imports CrystalDecisions.Web


Partial Class SelectPersonnel
    Inherits System.Web.UI.Page
    Private Const PrintedListKey As String = "PrintedCardList"

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        SaveSelectedCodes() '← حتماً ذخیره شود قبل از چاپ

        Dim selectedCodes = SelectedPersonnel
        If selectedCodes.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('لطفاً حداقل یک نفر را انتخاب کنید');", True)
            Return
        End If

        ' بازیابی اطلاعات افراد انتخاب‌شده از دیتابیس
        Dim inClause As String = String.Join(",", selectedCodes.Select(Function(x) "'" & x & "'"))
        Dim sql As String = "SELECT * FROM overhaulHerastVW WHERE codePersoneli IN (" & inClause & ")"
        Dim dtAll As DataTable = ClsDataTable.GetDataTable(sql)

        Session("CardDataTable") = dtAll
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "popup", "window.open('PrintCards.aspx','_blank','width=800,height=600');", True)
    End Sub




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SelectedPersonnel = New List(Of String)()
            BindGrid()
        Else
            SaveSelectedCodes()
        End If
    End Sub


    Private Sub BindGrid()
        'Dim baseSql As String = "SELECT * FROM overhaulHerastVW WHERE codePersoneli IS NOT NULL ORDER BY codePersoneli"

        ' اگر چیزی وارد جستجو شده باشد
        'If Not String.IsNullOrEmpty(txtSearch.Text.Trim()) Then
        '    Dim keyword As String = txtSearch.Text.Trim().Replace("'", "''")
        '    baseSql &= " AND (fulname LIKE N'%" & keyword & "%' OR codePersoneli LIKE N'%" & keyword & "%')"

        'End If
        Dim baseSql As String = "SELECT * FROM overhaulHerastVW WHERE codePersoneli IS NOT NULL"

        If Not String.IsNullOrEmpty(txtSearch.Text.Trim()) Then
            Dim keyword As String = txtSearch.Text.Trim().Replace("'", "''")
            baseSql &= " AND (fulname LIKE N'%" & keyword & "%' OR codePersoneli LIKE N'%" & keyword & "%')"
        End If

        baseSql &= " ORDER BY codePersoneli"

        Dim dt As DataTable = ClsDataTable.GetDataTable(baseSql)
        lblTotalCount.Text = "تعداد کل: " & dt.Rows.Count.ToString() & " نفر"

        gridPersonnel.DataSource = dt
        gridPersonnel.DataBind()
    End Sub
   
    Protected Sub gridPersonnel_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridPersonnel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chk As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing Then
                Dim code As String = gridPersonnel.DataKeys(e.Row.RowIndex)("codePersoneli").ToString()
                chk.Checked = SelectedPersonnel.Contains(code)
            End If

            ' کد قبلی شما برای imgPhoto:
            Dim img As Image = CType(e.Row.FindControl("imgPhoto"), Image)
            If img IsNot Nothing Then
                Dim nationality As String = gridPersonnel.DataKeys(e.Row.RowIndex)("Nationality").ToString()
                Dim relativeUrl As String = "~/Attachment/" & nationality & "/PersonalPic.jpg"
                Dim physicalPath As String = Server.MapPath(relativeUrl)
                If IO.File.Exists(physicalPath) Then
                    img.ImageUrl = ResolveUrl(relativeUrl)
                Else
                    img.ImageUrl = ResolveUrl("~/Attachment/default.png")
                End If
            End If
        End If
    End Sub


    Private Sub RestoreSelections()
        Dim selectedList As List(Of String) = TryCast(Session("SelectedPersonnel"), List(Of String))
        If selectedList Is Nothing Then Exit Sub

        For Each row As GridViewRow In gridPersonnel.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing Then
                Dim code As String = gridPersonnel.DataKeys(row.RowIndex)("codePersoneli").ToString()
                chk.Checked = selectedList.Contains(code)
            End If
        Next
    End Sub

    Protected Sub gridPersonnel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        SaveSelectedCodes()
        gridPersonnel.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SaveSelectedCodes()
        gridPersonnel.PageIndex = 0
        BindGrid()
    End Sub


    Private Sub SaveSelectedCodes()
        For Each row As GridViewRow In gridPersonnel.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing Then
                Dim code As String = gridPersonnel.DataKeys(row.RowIndex)("codePersoneli").ToString()
                If chk.Checked Then
                    If Not SelectedPersonnel.Contains(code) Then
                        SelectedPersonnel.Add(code)
                    End If
                Else
                    If SelectedPersonnel.Contains(code) Then
                        SelectedPersonnel.Remove(code)
                    End If
                End If
            End If
        Next
    End Sub



    Private Property SelectedPersonnel As List(Of String)
        Get
            If Session("SelectedPersonnel") Is Nothing Then
                Session("SelectedPersonnel") = New List(Of String)()
            End If
            Return CType(Session("SelectedPersonnel"), List(Of String))
        End Get
        Set(value As List(Of String))
            Session("SelectedPersonnel") = value
        End Set
    End Property

   
    Private Property CurrentBatchIndex As Integer
        Get
            If Session("CurrentBatchIndex") Is Nothing Then
                Session("CurrentBatchIndex") = 0
            End If
            Return Convert.ToInt32(Session("CurrentBatchIndex"))
        End Get
        Set(value As Integer)
            Session("CurrentBatchIndex") = value
        End Set
    End Property
    Protected Sub btnSelectFirst80_Click(sender As Object, e As EventArgs) Handles btnSelectFirst80.Click
        CurrentBatchIndex = 0
        SelectBatch(CurrentBatchIndex)
    End Sub

   

    Protected Sub btnSelectNext80_Click(sender As Object, e As EventArgs) Handles btnSelectNext80.Click
        CurrentBatchIndex += 1
        SelectBatch(CurrentBatchIndex)
    End Sub

    Private Sub SelectBatch(batchIndex As Integer)
        If batchIndex < 1 Then batchIndex = 1
        CurrentBatchIndex = batchIndex

        Dim offset As Integer = (batchIndex - 1) * 80

        Dim sql As String = String.Format("Select codePersoneli     FROM (SELECT codePersoneli, ROW_NUMBER() OVER (ORDER BY codePersoneli) AS RowNum     FROM overhaulHerastVW " & _
          " WHERE codePersoneli Is Not NULL     ) AS Temp     WHERE RowNum > {0} AND RowNum <= {1}", offset, offset + 80)
        Dim dt As DataTable = ClsDataTable.GetDataTable(sql)
        ' انتخاب‌ها جایگزین قبلی نشوند، به لیست اضافه شوند:
    For Each row As DataRow In dt.Rows
        Dim code As String = row("codePersoneli").ToString()
        If Not SelectedPersonnel.Contains(code) Then
            SelectedPersonnel.Add(code)
                    End If
    Next

                ' محاسبه PageIndex برای آخرین رکورد دسته:
    If dt.Rows.Count > 0 Then
        Dim lastCode As String = dt.Rows(dt.Rows.Count - 1)("codePersoneli").ToString()
        Dim indexInAllData As Integer = GetIndexOfCode(lastCode)
        If indexInAllData >= 0 Then
            Dim pageSize As Integer = gridPersonnel.PageSize
            gridPersonnel.PageIndex = indexInAllData \ pageSize
                    End If
                End If

    BindGrid()
    End Sub

    Private Function GetIndexOfCode(code As String) As Integer
        Dim baseSql As String = "SELECT codePersoneli FROM overhaulHerastVW WHERE codePersoneli IS NOT NULL ORDER BY codePersoneli"
        Dim dt As DataTable = ClsDataTable.GetDataTable(baseSql)
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("codePersoneli").ToString() = code Then
                Return i
            End If
        Next
        Return -1
    End Function


End Class


