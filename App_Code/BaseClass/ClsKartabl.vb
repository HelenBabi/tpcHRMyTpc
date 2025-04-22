Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsKartabl
    Public Function GetActive() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     tblVisitTemp.KartablCode, LTRIM(RTRIM(tblVisitTemp.pelak_no1)) + '  ' + LTRIM(RTRIM(STR(tblVisitTemp.pelak_no2))) AS shahrbani1, " & _
                      " LTRIM(RTRIM(STR(tblVisitTemp.pelak_no3))) + '  ' + LTRIM(RTRIM(tblVisitTemp.pelak_no4)) + ' ' + LTRIM(RTRIM(STR(tblVisitTemp.pelak_no5))) AS shahrbani2,  " & _
        " tblVisitTemp.VisitHeramNo, tblVisitTemp.HeramColorCode, tblVisitTemp.VisitKiloMetr, tblVisitTemp.KindCarCode, tblVisitTemp.VisitDate, tblVisitTemp.VisitTime, " & _
        " tblVisitTemp.KartablStatus, tblVisitTemp.opid, tblVisitTemp.ArcTypeCode, tblHeramColor.HeramColorName, tblKindCar.KindCarName " & _
" FROM         tblVisitTemp INNER JOIN " & _
                      " tblHeramColor ON tblVisitTemp.HeramColorCode = tblHeramColor.HeramColorCode INNER JOIN " & _
                      " tblKindCar ON tblVisitTemp.KindCarCode = tblKindCar.KindCarCode " & _
" WHERE     (tblVisitTemp.KartablStatus = 0) "
        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function
End Class
