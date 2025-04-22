Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class reservReportCls
    Public Function GetAllBrowsWorks(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        *  FROM            reservReportVW where tarix between '" & az & "' and '" & taa & "' and  (stat = 1)"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    


    Public Function GetAllMyReserv(ByVal perid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        *  FROM            reservReportVW where  perID = " & perid & ""

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetResturanID(ByVal az As String, ByVal taa As String, ByVal resturanID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        *  FROM            reservReportVW where tarix between '" & az & "' and '" & taa & "' and resturanID = " & resturanID & " and  (stat = 1) "

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetPerID(ByVal perID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String

        sqlstr = "SELECT        *  FROM            vwReservationAll where perID = " & perID & "  order By reservID Desc"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetAllMenu(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
      

        sqlstr = "SELECT        *  FROM            reportResturanVW where tarix between '" & az & "' and '" & taa & "'"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetReservPaymentAll(ByVal az As String, ByVal taa As String) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String


        sqlstr = "SELECT        *  FROM            reservPaymentVW where tarix between '" & az & "' and '" & taa & "'"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
