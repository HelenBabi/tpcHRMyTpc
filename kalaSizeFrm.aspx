<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="kalaSizeFrm.aspx.vb" Inherits="kalaSizeFrm" %>

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
        <div class="row">
			<div class="col-md-12">
    <table class="style1" style="width: 600px" dir="ltr" >
        <tr>
            <td style="text-align: right">
               
                 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
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

    <table class="style1" style="width: 600px" dir="ltr">
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitscode0" runat="server" 
                    ControlToValidate="CmbHardware" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                                        <asp:DropDownList ID="CmbHardware" runat="server" AutoPostBack="True" 
                                            CssClass="txt_ r5" Height="30px" TabIndex="1" 
                    Width="220px" Enabled="False">
                                        </asp:DropDownList>
            </td>
            <td class="style7" style="text-align: left;">
                کالا</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitscode" runat="server" 
                    ControlToValidate="txt_sizeID" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_sizeID" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
               کد &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_unitsname" runat="server" 
                    ControlToValidate="txt_shobe_nam" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_shobe_nam" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                اندازه</td>
        </tr>
        <tr>
            <td style="height: 30px" colspan="3">
                <asp:TextBox ID="txt_SizeNote" runat="server" Width="100%" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </td>
            <td class="style7" style="text-align: left;">
                توضیحات</td>
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
                    KeyFieldName="sizeID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="1" Width="1%">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد " FieldName="sizeID" 
                            ShowInCustomizationForm="True" VisibleIndex="6" Width="5%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("sizeID") %>' 
                                    CommandName='<%# "select" %>' Text='<%# eval("sizeID") %>'></asp:LinkButton>
                            </dataitemtemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="اندازه" FieldName="sizeValue" VisibleIndex="5" Width="5%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="sizeNote" VisibleIndex="4" Width="15%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کالا " FieldName="kalaValue" 
                            VisibleIndex="3" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ستادی/عملیاتی" FieldName="typeValue" 
                            VisibleIndex="2" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="0" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
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
                </div>
            </div>
</asp:Content>

