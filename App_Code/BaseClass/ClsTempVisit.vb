Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data


Public Class ClsTempVisit
    Public Function GetCarInformation(ByVal id As Int64) As DataTable
        Dim dt, dt2 As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT KartablCode, pelak_no1, pelak_no2, pelak_no3, pelak_no4, pelak_no5 FROM tblVisitTemp WHERE (KartablCode = 1) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            sqlstr = " SELECT     shasi_no, pelak_no1, pelak_no2, pelak_no3, pelak_no4, pelak_no5, KindCarCode, dam_no, motor_no, vin_no, EshterakCode, CustomerCode  FROM  tblCar " & _
" WHERE     (pelak_no1 = N'" & dt.Rows(0).Item("pelak_no1").ToString & "') AND (pelak_no2 = " & dt.Rows(0).Item("pelak_no2").ToString & ") AND (pelak_no3 = " & dt.Rows(0).Item("pelak_no3").ToString & ") " & _
" AND (pelak_no4 = N'" & dt.Rows(0).Item("pelak_no4").ToString & "') AND (pelak_no5 = " & dt.Rows(0).Item("pelak_no5").ToString & ") "
            dt2 = ClsDataTable.GetDataTable(sqlstr)
            If dt2.Rows.Count > 0 Then
                Return dt2
            End If
        End If
        Return dt

    End Function

End Class
