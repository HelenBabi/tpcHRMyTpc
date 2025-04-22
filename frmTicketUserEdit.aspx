<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmTicketUserEdit.aspx.vb" Inherits="frmTicketUserEdit" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style1
        {
            color: #3366CC;
        }
        .style2
        {
            float: right;
            width: 105px;
            height: 25px;
            text-align: left;
            vertical-align: middle;
            line-height: 25px;
            margin-left: 1px;
            color: #000000;
        }
        .dxeTextBox .dxeEditArea
{
    background-color: white;
}

.dxeEditArea 
{
	font-family: Tahoma;
	font-size: 9pt;
	border: 1px solid #A0A0A0;
}
        .style5
        {
            width: 100%;
        }
        .style6
        {
            color: #6699FF;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <table class="style5">
        <tr>
            <td class="style6">
       
                .&nbsp;لطفا اطلاعات خود را تکمیل و ذخیره کنید،برای استفاده از برنامه تکمیل موارد 
                ستاره دار ضروری است</td>
        </tr>
        <tr>
            <td class="style6">
       
                &nbsp;</td>
        </tr>
    </table>
    
    
    <div class="div_line">
        <div class="div_nev">
            نام</div>
        <div class="div_txt">
            <asp:TextBox ID="txtName" runat="server" Width="190px" CssClass="txt_ r5 h25"></asp:TextBox>
        </div>
        <div class="div_val">
            <asp:RequiredFieldValidator ID="vald_Holdercode6" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" CssClass="style1"></asp:RequiredFieldValidator>
        </div>
        <div class="div_nev">
            نام خانوادگی</div>
        <div class="div_txt">
            <asp:TextBox ID="txtlName" runat="server" Width="190px" CssClass="txt_ r5 h25"></asp:TextBox>
        </div>
        <div class="div_val">
            <asp:RequiredFieldValidator ID="vald_Holdercode7" runat="server" 
                    ControlToValidate="txtlName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" CssClass="style1"></asp:RequiredFieldValidator>
        </div>
        <div class="div_nev">
        </div>
        <div class="div_txt">
        </div>
        <div class="div_val">
        </div>
    </div>
    <div class="div_line">
        <div class="div_nev">
                سمت</div>
        <div class="div_txt">
            <asp:TextBox ID="txtSemat" runat="server" Width="190px" 
                    CssClass="txt_ r5 h25" ToolTip="سمت سازمانی خود را بنویسید"></asp:TextBox>
        </div>
        <div class="div_val">
        </div>
        <div class="div_nev">
                كد پرسنل</div>
        <div class="div_txt">
            <asp:TextBox ID="txtCodepersonel" runat="server" Width="190px" 
                    CssClass="txt_ r5 h25" Enabled="False"></asp:TextBox>
        </div>
        <div class="div_val">
            <asp:RequiredFieldValidator ID="vald_Holdercode0" runat="server" 
                    ControlToValidate="txtCodepersonel" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" CssClass="style1"></asp:RequiredFieldValidator>
        </div>
        <div class="div_nev">
        </div>
        <div class="div_txt">
        </div>
        <div class="div_val">
        </div>
    </div>
    <div class="div_line">
        <div class="div_nev">
            تلفن داخلی</div>
            <span class="style1">
        <div class="div_txt">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtTell" runat="server" Width="190px" 
    CssClass="txt_ r5 h25"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="div_val">
            <asp:RequiredFieldValidator ID="vald_Holdercode1" runat="server" 
                    ControlToValidate="txtTell" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" CssClass="style1"></asp:RequiredFieldValidator>
        </div>
        <div class="style2">
            موبایل</div>
        <div class="div_txt">
            <span class="style1">
                    <asp:TextBox ID="txtMobile" runat="server" Width="190px" 
    CssClass="txt_ r5 h25" ToolTip="شماره موبایل خود را طبق نمونه با صفر بنویسید"></asp:TextBox>
    </span>
        </div>
        <div class="div_val">
            <span class="style1">
            <asp:RequiredFieldValidator ID="vald_Holdercode4" runat="server" 
                    ControlToValidate="txtMobile" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" CssClass="style1"></asp:RequiredFieldValidator>
    </span>
        </div>
        <div class="div_nev">
            0914900....</div>
        <div class="div_txt">
        </div>
        <div class="div_val">
        </div>
    </div>
    <div class="div_line">
        <div class="div_nev">
            ایمیل<span class="style1">
            </span>شرکتی</div>
        <div class="div_txt">
            <span class="style1">
            <asp:TextBox ID="txtPcUserEmail" runat="server" Width="190px" 
                    CssClass="txt_ r5 h25" ToolTip="اگر ایمیل شرکتی دارید بنویسد"></asp:TextBox>
    </span>
        </div>
        <div class="div_val">
        </div>
        <div class="div_nev">
            جنسیت</div>
        <div class="div_txt">
            <span class="style1">
            <asp:DropDownList ID="cmbGender" runat="server" CssClass="txt_ r5 h25" 
                Width="100px">
                <asp:ListItem Value="0">مرد</asp:ListItem>
                <asp:ListItem Value="1">زن</asp:ListItem>
            </asp:DropDownList>
    </span>
        </div>
        <div class="div_val">
        </div>
        <div class="div_nev">
        </div>
        <div class="div_txt">
        </div>
        <div class="div_val">
        </div>
    </div>
    <div class="div_line">
        <div class="div_nev">
            <asp:Label ID="lblLocatin" runat="server" Text="وضعیت"></asp:Label>
        </div>
        <div class="div_txt">
            <span class="style1">
            <asp:TextBox ID="txtLocation" runat="server" Width="190px" 
                    CssClass="txt_ r5 h25"></asp:TextBox>
    </span>
        </div>
        <div class="div_val">
        </div>
        <div class="div_nev">
            </div>
        <div class="div_txt">
        </div>
        <div class="div_val">
        </div>
        <div class="div_nev">
        </div>
        <div class="div_txt">
        </div>
        <div class="div_val">
        </div>
    </div>
    <table class="style5">
        <tr>
            <td>
                
                
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                
                
                            &nbsp;</td>
        </tr>
        <tr>
            <td>
                
                
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/Icons/bargasht.png" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icons/save.png" 
                                ValidationGroup="a" />
                
                
                        </td>
        </tr>
    </table>
    </span>
</asp:Content>

