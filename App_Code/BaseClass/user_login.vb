Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class user_login
    Public Function GetUserok(ByVal name_ As String, ByVal pass_ As String) As DataTable

        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      mobile, codeMelli,madrak + ' ' + reshte as madrakReshte, postname,ID, nam, family,UserPic,codePersoneli,pass,serviceNo,supervizer,UnitsCode,typ,shift,isResort,jens,modir,omurID,erja,ok,sms,calncel FROM personel  WHERE        (codePersoneli = " & name_ & ") AND (pass = N'" & pass_ & "') AND (CodeClosed = 0)"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetUserokCodePersoneli(ByVal name_ As String) As DataTable

        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      mobile, codeMelli,madrak + ' ' + reshte as madrakReshte, postname,ID, nam, family,UserPic,codePersoneli,pass,serviceNo,supervizer,UnitsCode,typ,shift,isResort,jens,modir,omurID,erja,ok,sms,calncel FROM personel  WHERE        (codePersoneli = " & name_ & ") AND   (CodeClosed = 0)"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetUserinfo(ByVal iduser As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     *  FROM  personel  WHERE     (ID = " & iduser & ") AND (CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetUserByMobile(ByVal name_ As String) As DataTable

        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT      mobile, codeMelli,madrak + ' ' + reshte as madrakReshte, postname,ID, nam, family,UserPic,codePersoneli,pass,serviceNo,supervizer,UnitsCode,typ,shift,isResort,jens,modir,omurID,erja,ok,sms,calncel FROM personel  WHERE        (mobile = '" & name_ & "') AND   (CodeClosed = 0)"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
