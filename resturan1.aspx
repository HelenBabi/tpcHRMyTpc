<%@ Page Language="VB" AutoEventWireup="false" CodeFile="resturan1.aspx.vb" Inherits="resturan1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> رستوران یک برنج</title>
</head>
<body>

    <form id="form1" runat="server">
        <div style="text-align:center">
            <h2 style="text-align:center">رستوران یک </h2>
            <h3 style="text-align:center">غذای با برنج</h3>
        </div>
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="direction:ltr; width:100%; height:auto;" >
 
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">     
  <ContentTemplate>    
      <asp:Timer ID="Timer1" runat="server" Interval="2000" Enabled="True" ontick="Timer1_Tick"></asp:Timer>
     
                <asp:DataList ID="DataList1" runat="server" BorderColor="#8DC3F0" 
                    BorderStyle="Solid" CellSpacing="1" Font-Bold="False" Font-Names="A1Tahoma" 
                    Font-Overline="False" Font-Size="Small" ForeColor="Black" 
                    RepeatColumns="5" RepeatDirection="Horizontal" style="text-align: center">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="400px" 
                            ImageUrl='<%# Eval("CardId", "images\person_pic\{0}" + ".jpg")%>' 
                            Width="300px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="true" Font-Size="14"
                            style="color: #666666; font-weight: 700; " 
                            Text='<%# Eval("person")%>' Width="228px"></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>
                
                   </ContentTemplate>
</asp:UpdatePanel>
   
    </div>
        
       

    </form>
 
   
</body>
</html>
