<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="qouataFrm.aspx.vb" Inherits="qouataFrm" %>

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
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 31px;
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
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeleted" runat="server" Height="48px" 
                        ImageUrl="~/Icons/deleted.png" ToolTip="لیست کدهای حذف شده" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
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
            <td style="text-align: center">
                    <table class="auto-style1" style="color: #000000" dir="ltr">
                        <tr>
                            <td>
                <asp:RequiredFieldValidator ID="vald_unitsname" runat="server" 
                    ControlToValidate="TypeName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                            <td dir="rtl">
                <asp:TextBox ID="TypeName" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                            </td>
                            <td>نام گروه</td>
                            <td>
                <asp:RequiredFieldValidator ID="vald_unitscode" runat="server" 
                    ControlToValidate="TypeID" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                <asp:TextBox ID="TypeID" runat="server" Width="100px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                            </td>
                            <td>کد</td>
                        </tr>
                    </table>
                </td>
        </tr>
        <tr>
            <td>

    <table class="style1" style="width: 600px" align="center">
        <tr>
            <td style="height: 30px" colspan="4">
                <table class="auto-style1" style="color: #000000">
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                <asp:TextBox ID="TypeSahmDateEnd1" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>پایان</td>
                        <td>
                <asp:TextBox ID="TypeSahmDateStart1" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>شروع</td>
                        <td>&nbsp;</td>
                        <td>
                <asp:TextBox ID="TypeSahm1" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>تعداد سهمیه1</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                <asp:TextBox ID="TypeSahmDateEnd2" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>پایان</td>
                        <td>
                <asp:TextBox ID="TypeSahmDateStart2" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>شروع</td>
                        <td>&nbsp;</td>
                        <td>
                <asp:TextBox ID="TypeSahm2" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>تعداد سهمیه2</td>
                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                        <td class="auto-style2">
                <asp:TextBox ID="TypeSahmDateEnd3" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td class="auto-style2">پایان</td>
                        <td class="auto-style2">
                <asp:TextBox ID="TypeSahmDateStart3" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td class="auto-style2">شروع</td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2">
                <asp:TextBox ID="TypeSahm3" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td class="auto-style2">تعداد سهمیه3</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                <asp:TextBox ID="TypeSahmDateEnd4" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>پایان</td>
                        <td>
                <asp:TextBox ID="TypeSahmDateStart4" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>شروع</td>
                        <td>&nbsp;</td>
                        <td>
                <asp:TextBox ID="TypeSahm4" runat="server" Width="50px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                        </td>
                        <td>تعداد سهمیه4</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
            </td>
            <td class="style7" style="text-align: left;">
                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" Text="غیرفعال؟" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 30px" dir="rtl">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="id"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="کد " FieldName="id" 
                            ShowInCustomizationForm="True" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("id")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("id")%>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام گروه" VisibleIndex="1" FieldName="value">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد سهمیه1" VisibleIndex="1" FieldName="TypeSahm1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شروع" VisibleIndex="1" FieldName="TypeSahmDateStart1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="پایان" VisibleIndex="2" FieldName="TypeSahmDateEnd1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد سهمیه2" VisibleIndex="3" FieldName="TypeSahm2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شروع" VisibleIndex="4" FieldName="TypeSahmDateStart2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="پایان" VisibleIndex="5" FieldName="TypeSahmDateEnd2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد سهمیه3" VisibleIndex="6" FieldName="TypeSahm3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شروع" VisibleIndex="7" FieldName="TypeSahmDateStart3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="پایان" VisibleIndex="8" FieldName="TypeSahmDateEnd3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد سهمیه4" ShowInCustomizationForm="True" VisibleIndex="9" FieldName="TypeSahm4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شروع" VisibleIndex="11" FieldName="TypeSahmDateStart4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="پایان" VisibleIndex="12" FieldName="TypeSahmDateEnd4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="14">
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewCommandColumn VisibleIndex="15">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
                </td>
        </tr>
    </table>
</asp:Content>

