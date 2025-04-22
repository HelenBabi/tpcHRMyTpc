<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style8 {
            width: 726px;
        }
*
{
    font-size:12px;
    color: #000000;
}

        .auto-style10 {
            width: 3px;
        }
        .auto-style12 {
        width: 125px;
    }
    .auto-style13 {
        color: #666666;
        width: 125px;
    }
    .auto-style14 {
        width: 657px;
        text-align: left;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
<table class="auto-style7"  >
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="تعداد"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style14" dir="rtl">
                <asp:DataList ID="DataList1" runat="server" BorderColor="#8DC3F0" 
                    BorderStyle="Solid" CellSpacing="1" Font-Bold="False" Font-Names="A1Tahoma" 
                    Font-Overline="False" Font-Size="Small" ForeColor="Black" Height="360px" 
                    RepeatColumns="3" RepeatDirection="Horizontal" style="text-align: center" 
                    Width="16px">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="153px" 
                            ImageUrl='<%# Eval("filname", "~\images\bazar\{0}")%>' 
                            PostBackUrl='<%# String.Format("Detail.aspx?id={0}", Eval("idd")) %>' 
                            Width="243px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                            style="color: #666666; font-weight: 700; background-color: #CCCCCC;" 
                            Text='<%# eval("mozu") %>' Width="228px"></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>
                </td>
            <td class="auto-style10" style="text-align: justify; VERTICAL-ALIGN: top;  clip: rect(0px, auto, auto, auto);"> 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <table class="auto-style7">
                    <tr>
                        <td class="auto-style12">
                            <asp:Button ID="Button1" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033;" Text="همهٔ آگهی‌ها" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">
                            <asp:Button ID="Button2" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033;" Text="املاک" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Button ID="Button3" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033;" Text="وسایل نقلیه" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">
                            <asp:Button ID="Button4" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033; text-align: center;" Text="لوازم خانگی" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="auto-style12">
                            <asp:Button ID="Button5" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033;" Text="کامپیوتر" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">
                            <asp:Button ID="Button6" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033;" Text="خدمات فنی" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">
                            <asp:Button ID="Button7" runat="server" BorderStyle="None" Font-Names="Droid Arabic Naskh" Font-Size="16pt" ForeColor="#999999" style="background-color: #E4E4E4; color: #990033;" Text="موبایل" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>
            </div>
        </div>

    </div>
    
    </asp:Content>

