<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="menuFrm.aspx.vb" Inherits="menuFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

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
            <td style="text-align: center">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeleted" runat="server" Height="48px" 
                        ImageUrl="~/Icons/deleted.png" ToolTip="لیست کدهای حذف شده" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
                </td>
        </tr>
        <tr>
            <td>

    <table class="style1" style="width: 600px" align="center">
        <tr>
            <td style="width: 250px; height: 30px">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitscode" runat="server" 
                    ControlToValidate="menuID" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="menuID" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                کد </td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitsname" runat="server" 
                    ControlToValidate="menuName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="menuName" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                تایم </td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitsname0" runat="server" 
                    ControlToValidate="resturanCmb" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:DropDownList ID="resturanCmb" runat="server" Height="25px" Width="190px" Enabled="False">
                </asp:DropDownList>
            </td>
            <td class="style7" style="text-align: left;">
                محل</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitsname2" runat="server" 
                    ControlToValidate="zarfiat" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="zarfiat" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                ظرفیت</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server">
                </dx:ASPxGridViewExporter>
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" />
            </td>
            <td class="style7" style="text-align: left;">
                غیر فعال</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 30px">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="menuID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="0">
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="ظرفیت" FieldName="zarfiat" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل" FieldName="locationValue" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تایم" FieldName="menu" ShowInCustomizationForm="True" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد" FieldName="menuID" ShowInCustomizationForm="True" VisibleIndex="7">
                            <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("menuId")%>' CommandName='<%# "select" %>' Text='<%# Eval("menuID")%>'></asp:LinkButton>
                            </dataitemtemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
                </td>
        </tr>
    </table>
</asp:Content>

