Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClsMainReports
    Public Function GetSaxteman() As DataTable
        Dim dt As New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT     COUNT(tblBuilding.BuildingName) AS tedad, tblBuilding.BuildingName, tblWorkType.WorkTypeName " & _
" FROM         tblBuilding INNER JOIN    tblSystem ON tblBuilding.BuildingCode = tblSystem.BuildingCode INNER JOIN " & _
                     " tblUnits ON tblSystem.UnitsCode = tblUnits.UnitsCode INNER JOIN " & _
                     " tblWorkHdr ON tblSystem.SystemCode = tblWorkHdr.SystemCode INNER JOIN " & _
                     " tblWorkType ON tblWorkHdr.WorkTypeCode = tblWorkType.WorkTypeCode " & _
                     " GROUP BY tblBuilding.BuildingName, tblWorkType.WorkTypeName " & _
                    "ORDER BY tedad DESC, tblBuilding.BuildingName"

        dt = ClsDataTable.GetDataTable(sqlstr)
        Return dt
    End Function

End Class
