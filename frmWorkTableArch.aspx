<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmWorkTableArch.aspx.vb" Inherits="frmWorkTableArch" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="div_headp" dir="rtl">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
</div>
       
  <div class="div_grid">
          <dx:ASPxGridView ID="GridKartabl" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="erja_id"  
                    EnableCallBacks="False" ClientIDMode="AutoID">
    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
    </ClientSideEvents>
    <Columns>
        <dx:GridViewDataTextColumn Caption="شماره درخواست" FieldName="erja_id" 
                            VisibleIndex="0" ShowInCustomizationForm="True" 
            Width="50px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="اموال" FieldName="systemcode" 
                            ShowInCustomizationForm="True" VisibleIndex="1" Name="sel" 
            Width="70px">
            <dataitemtemplate>
                <asp:LinkButton ID="LinkButton2" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' Text='<%# eval("systemcode") %>'></asp:LinkButton>
            </dataitemtemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Right">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان ارسال" FieldName="zamanErsal" 
                            ShowInCustomizationForm="True" VisibleIndex="2" 
            Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="ferestande" 
            ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName_" 
            ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="telfon" 
            ShowInCustomizationForm="True" VisibleIndex="5" Width="50px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="IP" FieldName="ipp" VisibleIndex="6" 
            Width="100px">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True" />
    <SettingsBehavior AllowFocusedRow="True">
    </SettingsBehavior>
    <SettingsPager PageSize="25">
    </SettingsPager>
    <Settings ShowFilterRow="True" />
    <Settings ShowFilterRow="True">
    </Settings>
</dx:ASPxGridView>
         
         </div>       
                   
</asp:Content>

