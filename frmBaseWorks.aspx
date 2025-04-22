<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmBaseWorks.aspx.vb" Inherits="frmBaseWorks" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: left;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td colspan="3">

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
            <td>&nbsp;</td>
            <td style="text-align: right"><asp:RequiredFieldValidator ID="vald_BaseWorkname0" runat="server" 
                    ControlToValidate="txt_BaseWorkCode" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txt_BaseWorkCode" runat="server" Width="90px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
           
            </td>
            <td style="color: #000000; text-align: left">کد</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode" runat="server" 
                    ControlToValidate="CmbWorkeType" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="CmbWorkeType" runat="server" AutoPostBack="True" 
                                                    CssClass="txt_ r5" Enabled="False" Height="30px" TabIndex="1" Width="194px">
                                                </asp:DropDownList>
                                            </td>
            <td class="auto-style2" style="color: #000000">نوع سرويس</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align: right">
                <asp:RequiredFieldValidator ID="vald_BaseWorkname" runat="server" 
                    ControlToValidate="txt_BaseWorkName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txt_BaseWorkName" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5" Height="25px"></asp:TextBox>
            </td>
            <td class="auto-style2" style="color: #000000">نام کار</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align: left; color: #000000;">
                <asp:TextBox ID="txtMasir" runat="server" Width="859px" 
                    ValidationGroup="a" Enabled="False" Height="25px"></asp:TextBox>
           
            </td>
            <td style="color: #000000; text-align: left">مسیر نصب</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="color: #000000; text-align: right;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chass" runat="server" Enabled="False" Text="نصب خودکار؟" Checked="True" />
            </td>
            <td>
                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" Text="غیر فعال" style="color: #000000" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="BaseWorkCode"  
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="5" Visible="False">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="4" Width="5px">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="نوع سرويس" FieldName="WorkTypeName" 
                            VisibleIndex="3" Width="240px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام كار" FieldName="BaseWorkName" 
                            ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مسیر نصب" FieldName="masir" VisibleIndex="2" Width="300px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Left">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد كار" FieldName="BaseWorkCode" 
                            ShowInCustomizationForm="True" VisibleIndex="0" Width="5px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("BaseWorkCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("BaseWorkCode") %>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <SettingsPager PageSize="25">
                    </SettingsPager>

<Settings ShowFilterRow="True"></Settings>

                </dx:ASPxGridView>


         
            </td>
        </tr>
        <tr>
            <td colspan="3">
    

         
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />


   
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
   
  
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

