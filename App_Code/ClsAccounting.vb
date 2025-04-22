Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsAccounting
    Public Shared Function meno_acc(ByVal form_name As String, ByVal UserID As String) As Boolean()
        Dim cnn As New SqlConnection
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim i As Byte

        Dim meno_arr(11) As Boolean
        Dim flag As Boolean = False
        Dim sqlstr As String
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        sqlstr = " SELECT     tblFormAcc.AllowNew, tblFormAcc.AllowEdit, tblFormAcc.AllowDelete, tblFormAcc.AllowPrint,AllowDeleted,Allowclosed,AllowHistory " & _
" FROM         tblFormAcc INNER JOIN " & _
 "                     tblForm ON tblFormAcc.FrmID = tblForm.FrmID " & _
" WHERE     (tblFormAcc.usercode =" & UserID & ") AND (tblForm.FrmName = '" & form_name.Trim & "')"
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        cnn.Close()
        For i = 0 To 11
            meno_arr(i) = False
        Next
        If dt.Rows.Count > 0 Then
            meno_arr(0) = True
            If dt.Rows(0).Item("AllowNew") = True Then
                meno_arr(1) = True
            End If
            If dt.Rows(0).Item("AllowEdit") = True Then
                meno_arr(2) = True
            End If
            If dt.Rows(0).Item("AllowDelete") = True Then
                meno_arr(6) = True
            End If
            If dt.Rows(0).Item("AllowPrint") = True Then
                meno_arr(10) = True
            End If
            If dt.Rows(0).Item("AllowDeleted") = True Then
                meno_arr(7) = True
            End If
            If dt.Rows(0).Item("AllowHistory") = True Then
                meno_arr(8) = True
            End If
            If dt.Rows(0).Item("Allowclosed") = True Then
                meno_arr(9) = True
            End If

            If dt.Rows(0).Item("AllowNew") = True Or dt.Rows(0).Item("AllowEdit") = True Or dt.Rows(0).Item("AllowDelete") = True Then
                '''''save
                meno_arr(3) = True
                ''''save new
                meno_arr(4) = True
                ''''cancel
                meno_arr(5) = True
            End If

            'If SaveTlStp.Visible = True Then
            '    cancelTlStp.Visible = True
            'End If
        Else
            meno_arr(0) = False
        End If
        Return meno_arr
    End Function
End Class
