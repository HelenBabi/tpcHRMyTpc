Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsCustomer
    Public Function GetAllCustomer(ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblCustomer.CustomerCode, tblCar.shasi_no, tblCar.pelak_no1, tblCar.pelak_no2, tblCar.pelak_no3, tblCar.pelak_no4, tblCar.pelak_no5, tblCar.KindCarCode, " & _
        " tblCar.dam_no, tblCar.motor_no, tblCar.vin_no, tblCar.opid, tblCar.ArcTypeCode, tblCustomer.CustomerName, tblCustomer.CustomerFamili, " & _
        " tblCustomer.CustomerHomeTel, tblCustomer.CustomerWorkTel, tblCustomer.CustomerMobil, tblCustomer.CustomerAddress, tblCustomer.CodeClosed " & _
" FROM         tblCar INNER JOIN " & _
                      " tblCustomer ON tblCar.CustomerCode = tblCustomer.CustomerCode " & _
" WHERE     (tblCustomer.CustomerCode <> 0) AND (tblCustomer.CodeClosed = 0)"
        If deactive = True Then
            sqlstr = "SELECT     tblCustomer.CustomerCode, tblCar.shasi_no, tblCar.pelak_no1, tblCar.pelak_no2, tblCar.pelak_no3, tblCar.pelak_no4, tblCar.pelak_no5, tblCar.KindCarCode, " & _
       " tblCar.dam_no, tblCar.motor_no, tblCar.vin_no, tblCar.opid, tblCar.ArcTypeCode, tblCustomer.CustomerName, tblCustomer.CustomerFamili, " & _
       " tblCustomer.CustomerHomeTel, tblCustomer.CustomerWorkTel, tblCustomer.CustomerMobil, tblCustomer.CustomerAddress, tblCustomer.CodeClosed " & _
" FROM         tblCar INNER JOIN " & _
                     " tblCustomer ON tblCar.CustomerCode = tblCustomer.CustomerCode " & _
" WHERE     (tblCustomer.CustomerCode <> 0)"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
    Public Function GetAllCustomerDDL() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblCustomer.CustomerCode, tblCar.shasi_no, tblCar.pelak_no1, tblCar.pelak_no2, tblCar.pelak_no3, tblCar.pelak_no4, tblCar.pelak_no5, tblCar.KindCarCode, " & _
        " tblCar.dam_no, tblCar.motor_no, tblCar.vin_no, tblCar.opid, tblCar.ArcTypeCode, tblCustomer.CustomerName, tblCustomer.CustomerFamili, " & _
        " tblCustomer.CustomerHomeTel, tblCustomer.CustomerWorkTel, tblCustomer.CustomerMobil, tblCustomer.CustomerAddress, tblCustomer.CodeClosed " & _
" FROM         tblCar INNER JOIN " & _
                      " tblCustomer ON tblCar.CustomerCode = tblCustomer.CustomerCode " & _
" WHERE  (tblCustomer.CodeClosed = 0)"
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

    Public Function GetCustomer(ByVal i As Integer, ByVal deactive As Boolean) As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblCustomer.CustomerCode, tblCar.shasi_no, tblCar.pelak_no1, tblCar.pelak_no2, tblCar.pelak_no3, tblCar.pelak_no4, tblCar.pelak_no5, tblCar.KindCarCode, " & _
       " tblCar.dam_no, tblCar.motor_no, tblCar.vin_no, tblCar.opid, tblCar.ArcTypeCode, tblCustomer.CustomerName, tblCustomer.CustomerFamili, " & _
       " tblCustomer.CustomerHomeTel, tblCustomer.CustomerWorkTel, tblCustomer.CustomerMobil, tblCustomer.CustomerAddress, tblCustomer.CodeClosed " & _
" FROM         tblCar INNER JOIN " & _
                     " tblCustomer ON tblCar.CustomerCode = tblCustomer.CustomerCode " & _
" WHERE     (tblCustomer.CustomerCode <> 0) AND (tblCustomer.CodeClosed = 0) AND   (tblCustomer.CustomerCode = " & i & ")"

        If deactive = True Then
            sqlstr = "SELECT     tblCustomer.CustomerCode, tblCar.shasi_no, tblCar.pelak_no1, tblCar.pelak_no2, tblCar.pelak_no3, tblCar.pelak_no4, tblCar.pelak_no5, tblCar.KindCarCode, " & _
      " tblCar.dam_no, tblCar.motor_no, tblCar.vin_no, tblCar.opid, tblCar.ArcTypeCode, tblCustomer.CustomerName, tblCustomer.CustomerFamili, " & _
      " tblCustomer.CustomerHomeTel, tblCustomer.CustomerWorkTel, tblCustomer.CustomerMobil, tblCustomer.CustomerAddress, tblCustomer.CodeClosed " & _
" FROM         tblCar INNER JOIN " & _
                    " tblCustomer ON tblCar.CustomerCode = tblCustomer.CustomerCode " & _
" WHERE     (tblCustomer.CustomerCode <> 0)  AND   (tblCustomer.CustomerCode = " & i & ")"
        End If
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
