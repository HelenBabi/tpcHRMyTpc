Imports System
Imports System.Collections
Imports System.Linq
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Linq
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AutoComplete
    Inherits System.Web.Services.WebService
    Dim cn As New SqlClient.SqlConnection()

    <WebMethod()> _
    Public Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer) As Integer()
        Dim dt As New DataTable
        dt = ClsDataTable.GetDataTable("select * from personel Where codePersoneli like '%" & prefixText & "%'")
        Dim txtItems As New List(Of Integer)
        Dim dbValues As String
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                ''String From DataBase(dbValues)
                dbValues = row("codePersoneli")
                'dbValues = dbValues.ToLower()
                txtItems.Add(Val(dbValues))
            Next
        End If

        Return txtItems.ToArray()
    End Function

    Private Function Val(dbValues As String) As Integer
        Throw New NotImplementedException
    End Function

End Class