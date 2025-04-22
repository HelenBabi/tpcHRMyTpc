Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class radifCls
    Public Shared Function GetMaxId(ByVal tarix As String, ByVal resturan As Integer) As Int64
        Dim cnn As New SqlConnection
        Dim cmm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim max As Int64
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim sqlstr As String = "SELECT        MAX(radif) AS radiff " & _
        " FROM resrevationTbl GROUP BY tarix, resturanID,stat " & _
         " HAVING        (tarix = '" & tarix & "') AND (resturanID = " & resturan & ") AND (stat = 1 ) "
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        If dt.Rows.Count = 0 Then
            max = 1
        Else
            max = Val(dt.Rows(0).Item(0).ToString) + 1
        End If
        cnn.Close()
        cnn.Dispose()
        dt.Dispose()
        Return max

    End Function
End Class