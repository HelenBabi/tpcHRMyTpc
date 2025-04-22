<%@ Page Language="VB" AutoEventWireup="false" CodeFile="trnFilesFrm.aspx.vb" Inherits="trnFilesFrm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <asp:Panel ID="Panel1" runat="server" GroupingText="فایل های آموزشی">
      <div class="row">
			<div class="col-md-12">
              
                <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="A1Tahoma" Font-Overline="False" Font-Size="Small" GridLines="Both" Height="65px" RepeatColumns="8" RepeatDirection="Horizontal" style="text-align: center; margin-left: 1px; background-color: #339966;" Width="16px">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BorderStyle="Outset" ForeColor="#000066" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="96px" ImageUrl="~/Images/folder-close-icon.png" PostBackUrl='<%# String.Format("faunitUsers.aspx?id={0}", Eval("UnitsCode"))%>' style="background-color: #339966" Width="96px" />
            <br />
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Size="13pt" style="color: #FFFFFF; background-color: #339966;" Text='<%# Eval("UnitsName")%>' Width="110px"></asp:HyperLink>
            <br />
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>

                </div>
         
          </div>
               </asp:Panel>
    </form>
</body>
</html>
