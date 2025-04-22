<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmPass.aspx.vb" Inherits="frmPass" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style1
        {
            width: 84%;
        }
        .style2
        {
            height: 26px;
            text-align: center;
        }
    
        .style7
        {
            height: 30px;
            text-align: center;
            width: 100px;
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table class="style1" style="width: 600px" align="center" dir="ltr">
        <tr>
            <td style="text-align: center; color: #000000;">
                    فرم تغییر رمز</td>
        </tr>
        <tr>
            <td style="text-align: right">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" 
                        Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeleted" runat="server" Height="48px" 
                        ImageUrl="~/Icons/deleted.png" ToolTip="لیست کدهای حذف شده" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" 
                        ImageUrl="~/Icons/save.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                </td>
        </tr>
        <tr>
            <td>

    <table class="style1" style="width: 600px" align="center">
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_Passcode" runat="server" 
                    ControlToValidate="txt_CurentPass" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_CurentPass" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                رمز فعلي</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_NewPass1" ControlToValidate="txt_NewPass2" ErrorMessage="تکرار رمز جدید نامعتبر" ForeColor="Red" ValidationGroup="a"></asp:CompareValidator>
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_Passname" runat="server" 
                    ControlToValidate="txt_NewPass1" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_NewPass1" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                رمز جديد</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_Passname0" runat="server" 
                    ControlToValidate="txt_NewPass2" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_NewPass2" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                تكرار رمز جديد</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                &nbsp;</td>
            <td class="style7" style="text-align: center;">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 30px">
                &nbsp;</td>
        </tr>
    </table>
                </td>
        </tr>
    </table>
</asp:Content>

