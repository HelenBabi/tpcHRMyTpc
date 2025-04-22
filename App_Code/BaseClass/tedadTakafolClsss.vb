Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class tedadTakafolClsss
    Public Shared Function GetCount(ByVal codepersoneli As Integer) As Int64
        Dim cnn As New SqlConnection
        Dim cmm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim takafolCnt As Int64
        cnn.ConnectionString = ClsConnection.GetConn
        cnn.Open()
        Dim sqlstr As String = "SELECT     ID, empID, fstName, Name, relation, birthday, credit, logUID " & _
        " FROM Sponsorship WHERE     (empID = '" & codepersoneli & "')"
        da = New SqlDataAdapter(sqlstr, cnn)
        da.Fill(dt)

        takafolCnt = dt.Rows.Count + 1
        cnn.Close()
        cnn.Dispose()
        dt.Dispose()
        Return takafolCnt

    End Function


End Class
