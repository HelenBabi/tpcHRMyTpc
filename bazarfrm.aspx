<%@ Page Language="VB" AutoEventWireup="false" CodeFile="bazarfrm.aspx.vb" Inherits="bazarfrm" %>
<link href="assets/styles/style.min.css" rel="stylesheet" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div style="margin:0px; width:auto;text-align:center; >
        <table class="auto-style1">
            <tr>
                <td class="auto-style3" style="background-color: #FFFFFF" >
                    <table class="auto-style7">
                        <tr>
                            <td class="auto-style8">
                                <asp:Button ID="Button2" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #FFFFFF" Text="ثبت نام" />
&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #FFFFFF" Text=" آگهی جدید" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #FFFFFF" Text="صفحه اصلی" />
                            </td>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logo.jpg" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <table class="auto-style1">
                        <tr>
                            <td 
                style="VERTICAL-ALIGN: top; TEXT-ALIGN: center; background-color: #E4E4E4;" class="auto-style6">
                                
                         
                              </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF; text-align: center;">
                    <span class="auto-style9">Copyright © 2014 <strong>Tabriz Petrochemical Company</strong> All rights reserved. 
                        Designer:R.Hajalizade</span>
                    &nbsp;</td><br />
  
    </div>



        <div class="container">
 <div class="row">
    <div class="col-md-12" >
  <asp:Label ID="Label1" runat="server" Text="تعداد" CssClass="control-label"></asp:Label>
          <asp:DataList ID="DataList1" runat="server" BorderColor="#8DC3F0" 
                    BorderStyle="Solid" CellSpacing="1" Font-Bold="False" Font-Names="A1Tahoma" 
                    Font-Overline="False" Font-Size="Small" ForeColor="Black" Height="360px" 
                    RepeatColumns="3" RepeatDirection="Horizontal" style="text-align: center" 
                    Width="16px">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="153px" 
                            ImageUrl='<%# Eval("filname", "images\bazar\{0}")%>' 
                            PostBackUrl='<%# String.Format("Detail.aspx?id={0}", Eval("idd")) %>' 
                            Width="243px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                            style="color: #666666; font-weight: 700; background-color: #CCCCCC;" 
                            Text='<%# eval("mozu") %>' Width="228px"></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>
        
    </div>

           </div>
            <div class="row">
    <div class="col-md-12" >

    </div>

           </div>
        </div>
   
    </form>
</body>
</html>
