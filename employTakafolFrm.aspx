<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="employTakafolFrm.aspx.vb" Inherits="employTakafolFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style1 {
        width: 100%;
        color: #000000;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1" dir="ltr">
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
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
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="5" dir="rtl">
            <table class="auto-style1" dir="rtl">
                <tr>
                    <td>شماره پرونده</td>
                    <td style="text-align: right">
                        <asp:TextBox ID="codTxt" runat="server" Height="25px" Width="50px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="vald_unitscode" runat="server" 
                    ControlToValidate="codTxt" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>نام</td>
                    <td>
                        <asp:TextBox runat="server" Height="25px" Width="190px" ID="nam" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                <asp:RequiredFieldValidator ID="vald_unitscode0" runat="server" 
                    ControlToValidate="nam" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                    <td>نام خانوادگی</td>
                    <td>
                        <asp:TextBox ID="famili" runat="server" Height="25px" Width="190px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                <asp:RequiredFieldValidator ID="vald_unitscode2" runat="server" 
                    ControlToValidate="famili" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                    <td>کد پرسنلی سرپرست</td>
                    <td>
                        <asp:TextBox ID="codpersoneli" runat="server" Height="25px" Width="190px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                <asp:RequiredFieldValidator ID="vald_unitscode1" runat="server" 
                    ControlToValidate="codpersoneli" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>نسبت</td>
                    <td>
                        <asp:DropDownList ID="nesbat" runat="server" Height="25px" Width="190px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                <asp:RequiredFieldValidator ID="vald_unitscode3" runat="server" 
                    ControlToValidate="nesbat" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td>تاریخ تولد</td>
                    <td>
                        <asp:TextBox ID="tarixTavlod" runat="server" Height="25px" Width="190px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>تاریخ اعتبار</td>
                    <td>
                        <asp:TextBox ID="tarixetebar" runat="server" Height="25px" Width="190px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>تاریخ تولد</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>فعال؟</td>
                    <td>
                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" Checked="True" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="9" dir="ltr">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="ID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataCheckColumn Caption="فعال" FieldName="IsActive" 
                            VisibleIndex="0">
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="شماره پرونده" FieldName="ID" 
                            ShowInCustomizationForm="True" VisibleIndex="11">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("ID")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("ID")%>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="fstName" 
                            ShowInCustomizationForm="True" VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Name" VisibleIndex="9">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="empID" VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نسبت" FieldName="nesbatt" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ اعتبار" FieldName="credit" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ تولد" ExportWidth="1" FieldName="birthday" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

