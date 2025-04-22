Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data


Public Class ClsMenu
    Public Function GetHeadMenu() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     FrmID AS sum_, FrmID, FrmName, FrmCaption, Frm_level, Frm_Child, Frm_sort, FrmIcon " & _
        " FROM tblForm WHERE     (Frm_level = 1) " & _
        " ORDER BY tblForm.Frm_sort "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetCountHeadSubs(ByVal idparent As Integer, ByVal userid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblForm.FrmID) AS sum_ FROM         tblForm INNER JOIN " & _
                      " tblFormAcc ON tblForm.FrmID = tblFormAcc.FrmID " & _
" WHERE     (tblFormAcc.UserCode = " & userid & ") AND (tblFormAcc.FormAccess = 1) and (Frm_Child=" & idparent & ")"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetHeadSubs(ByVal idparent As Integer, ByVal userid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblForm.FrmName, tblForm.FrmCaption, tblForm.Frm_level, tblForm.Frm_Child, tblForm.Frm_sort, tblForm.FrmIcon " & _
" FROM         tblForm INNER JOIN " & _
                  " tblFormAcc ON tblForm.FrmID = tblFormAcc.FrmID " & _
"WHERE     (tblFormAcc.UserCode = " & userid & ") AND (tblFormAcc.FormAccess = 1) and (Frm_Child=" & idparent & ")" & _
" ORDER BY tblForm.Frm_sort "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
