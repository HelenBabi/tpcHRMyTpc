Imports Microsoft.VisualBasic

Public Class ClsConnection
    Public Shared Function GetConn() As String
        '  Dim myserver_address As String = "172.16.1.15\refah"
        'Dim myserver_address As String = ".\refahi"
        Dim myserver_address As String = ConfigurationManager.AppSettings("myserver_address")
        Dim mydatabase As String = ConfigurationManager.AppSettings("mydatabase")
        Dim myuser As String = ConfigurationManager.AppSettings("myuser")
        ' Dim mypassword As String = "123321"
        Dim mypassword As String = ConfigurationManager.AppSettings("mypassword")
        Dim connectString As String = "Data Source=" & myserver_address & ";Initial Catalog=" & mydatabase & ";Persist Security Info=True;User ID=" & myuser & _
        ";Password=" & mypassword & ";"
        Return (connectString)
    End Function
    Public Shared Function GetConnHerasat() As String
        '  Dim myserver_address As String = "172.16.1.15\refah"
        'Dim myserver_address As String = ".\refahi"
        Dim myserver_address As String = ConfigurationManager.AppSettings("myserver_address_herasat")
        Dim mydatabase As String = ConfigurationManager.AppSettings("mydatabase_herasat")
        Dim myuser As String = ConfigurationManager.AppSettings("myuser")
        ' Dim mypassword As String = "123321"
        Dim mypassword As String = ConfigurationManager.AppSettings("mypassword")
        Dim connectString As String = "Data Source=" & myserver_address & ";Initial Catalog=" & mydatabase & ";Persist Security Info=True;User ID=" & myuser & _
        ";Password=" & mypassword & ";"
        Return (connectString)
    End Function

    Public Shared Function GetConnAbzart() As String
        '  Dim myserver_address As String = "172.16.1.15\refah"
        'Dim myserver_address As String = ".\refahi"
        Dim myserver_address As String = ConfigurationManager.AppSettings("myserver_address_herasat")
        Dim mydatabase_abzar As String = ConfigurationManager.AppSettings("mydatabase_abzar")
        Dim myuser As String = ConfigurationManager.AppSettings("myuser")
        ' Dim mypassword As String = "123321"
        Dim mypassword As String = ConfigurationManager.AppSettings("mypassword")
        Dim connectString As String = "Data Source=" & myserver_address & ";Initial Catalog=" & mydatabase_abzar & ";Persist Security Info=True;User ID=" & myuser & _
        ";Password=" & mypassword & ";"
        Return (connectString)
    End Function

    Public Shared Function GetConnHD() As String
        '  Dim myserver_address As String = "172.16.1.15\refah"
        'Dim myserver_address As String = ".\refahi"
        Dim myserver_address As String = ConfigurationManager.AppSettings("myserver_address_herasat")
        Dim mydatabase_abzar As String = ConfigurationManager.AppSettings("mydatabase_HD")
        Dim myuser As String = ConfigurationManager.AppSettings("myuser")
        ' Dim mypassword As String = "123321"
        Dim mypassword As String = ConfigurationManager.AppSettings("mypassword")
        Dim connectString As String = "Data Source=" & myserver_address & ";Initial Catalog=" & mydatabase_abzar & ";Persist Security Info=True;User ID=" & myuser & _
        ";Password=" & mypassword & ";"
        Return (connectString)
    End Function


    Public Shared Function GetConnBoad5() As String
        '  Dim myserver_address As String = "172.16.1.15\refah"
        'Dim myserver_address As String = ".\refahi"
        Dim myserver_addressBoad5 As String = ConfigurationManager.AppSettings("server_address_Boad5")
        Dim mydatabase_Boad5 As String = ConfigurationManager.AppSettings("database_Boad5")
        Dim myuser As String = ConfigurationManager.AppSettings("userBoad5")
        ' Dim mypassword As String = "123321"
        Dim password_Boad5 As String = ConfigurationManager.AppSettings("password_Boad5")
        Dim connectString As String = "Data Source=" & myserver_addressBoad5 & ";Initial Catalog=" & mydatabase_Boad5 & ";Persist Security Info=True;User ID=" & myuser & _
        ";Password=" & password_Boad5 & ";"
        Return (connectString)
    End Function
End Class
