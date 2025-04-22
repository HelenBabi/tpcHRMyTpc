Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsDataTable
    Public Shared Function GetDataTable(ByVal sqlstr As String) As DataTable
        Dim cnn As New SqlConnection
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        cnn.Close()
        cnn.Dispose()
        Return dt
    End Function
    Public Shared Function GetDataTableHerasat(ByVal sqlstr As String) As DataTable
        Dim cnn As New SqlConnection
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        cnn.ConnectionString = ClsConnection.GetConnHerasat
        cnn.Open()
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        cnn.Close()
        cnn.Dispose()
        Return dt

    End Function
    Public Shared Function GetDataTableAbzar(ByVal sqlstr As String) As DataTable
        Dim cnn As New SqlConnection
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        cnn.ConnectionString = ClsConnection.GetConnAbzart
        cnn.Open()
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        cnn.Close()
        cnn.Dispose()
        Return dt

    End Function
    Public Shared Function GetDataTableHD(ByVal sqlstr As String) As DataTable
        Dim cnn As New SqlConnection
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        cnn.ConnectionString = ClsConnection.GetConnHD
        cnn.Open()
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        cnn.Close()
        cnn.Dispose()
        Return dt

    End Function
    Public Shared Function GetCommandScalar(ByVal sqlstr As String) As String
        Dim cnn As New SqlConnection
        Dim res As String = ""

        Try
            cnn.ConnectionString = ClsConnection.GetConn
            cnn.Open()
            Dim cmd As New SqlCommand(sqlstr, cnn)
            res = cmd.ExecuteScalar()

            cnn.Close()
            cnn.Dispose()
        Catch ex As Exception
            res = "-1"
        End Try

        Return res

    End Function
    Public Shared Function GetDataTableBoad5(ByVal sqlstr As String) As DataTable
        Dim cnn As New SqlConnection
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        cnn.ConnectionString = ClsConnection.GetConnBoad5
        cnn.Open()
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)
        cnn.Close()
        cnn.Dispose()
        Return dt

    End Function
End Class
