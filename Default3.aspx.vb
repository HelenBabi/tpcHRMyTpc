Imports System.Management
Imports System.Diagnostics

Partial Class Default3
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

      
    End Sub

 
   
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Process.Start("Cmd.exe", "/C shutdown.exe \\RemoteComputerName/r")
        Process.Start("mstsc.exe")
    End Sub
End Class
