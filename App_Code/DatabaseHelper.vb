Imports System.Data
Imports System.Data.SqlClient

Public Class DbHelper

    ' گرفتن ConnectionString از web.config
    Private Shared Function GetConn() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("MyConn").ConnectionString
    End Function

    ' اجرای دستورات INSERT, UPDATE, DELETE
    Public Shared Sub ExecuteNonQuery(sql As String, Optional parameters As List(Of SqlParameter) = Nothing)
        Using cnn As New SqlConnection(GetConn())
            Using cmd As New SqlCommand(sql, cnn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If
                cnn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' اجرای دستورات SELECT و برگرداندن DataTable
    Public Shared Function GetDataTable(sql As String, Optional parameters As List(Of SqlParameter) = Nothing) As DataTable
        Dim dt As New DataTable()
        Using cnn As New SqlConnection(GetConn())
            Using cmd As New SqlCommand(sql, cnn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' گرفتن مقدار تک مقداری (مثلا Count, Max, Sum)
    Public Shared Function ExecuteScalar(sql As String, Optional parameters As List(Of SqlParameter) = Nothing) As Object
        Using cnn As New SqlConnection(GetConn())
            Using cmd As New SqlCommand(sql, cnn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If
                cnn.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

End Class
