Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsGetMaxId
    Public Shared Function GetMaxId(ByVal tblname As String, ByVal fieldname As String) As Int64
        Dim cnn As New SqlConnection
        Dim cmm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim max As Int64
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim sqlstr As String = "SELECT Max(" & fieldname & ") AS counts FROM " & tblname
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
    Public Shared Function GetMaxIdHerast(ByVal tblname As String, ByVal fieldname As String) As Int64
        Dim cnn As New SqlConnection
        Dim cmm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim max As Int64
        cnn.ConnectionString = ClsConnection.GetConnHerasat
        cnn.Open()
        Dim sqlstr As String = "SELECT Max(" & fieldname & ") AS counts FROM " & tblname
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

    Public Shared Function GetMaxIdHD(ByVal tblname As String, ByVal fieldname As String) As Int64
        Dim cnn As New SqlConnection
        Dim cmm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim max As Int64
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()
        Dim sqlstr As String = "SELECT Max(" & fieldname & ") AS counts FROM " & tblname
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
