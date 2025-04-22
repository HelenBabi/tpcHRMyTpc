<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmBrowsAllRack.aspx.vb" Inherits="frmBrowsAllRack" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div_page" dir="rtl">
<div class="div_headpage" dir="rtl">
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
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
</div>
 
  <div class="div_line">
            <div class="div_nev">
                تعداد كل&nbsp;
            </div>
            <div class="div_txt">
                <asp:Label ID="lblCnt" runat="server" Font-Bold="True" style="color: #FF3300"></asp:Label>
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

                <div class="div_grid">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="SystemCode" Width="940px" 
                    ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                            UnboundType="Integer" VisibleIndex="0" Width="50px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("SystemCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("SystemCode") %>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="BuildingName" ShowInCustomizationForm="True" Caption="ساختمان" 
                            VisibleIndex="1" Width="150px">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام Rack" FieldName="ComputerName" 
                            VisibleIndex="2" Width="200px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="توضيحات" FieldName="tozihat" 
                            ShowInCustomizationForm="True" VisibleIndex="3" Width="500px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="30">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
                    <settingsdetail showdetailrow="True" />

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True"></SettingsDetail>
                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                    <templates>
                        <detailrow>
                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="SystemCode" 
                                OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" 
                                OnHtmlRowPrepared="grdFarei_HtmlRowPrepared" Width="930px">
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="*" FieldName="SystemHDtlStatus" 
                                        Visible="False" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="قطعه" FieldName="BaseSystemHDtlNmae" 
                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="مدل" FieldName="BaseModelName" 
                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="30%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="اموال قطعه" FieldName="AmvalCode" 
                                        VisibleIndex="3" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="نام قطعه" FieldName="SystemHDtlSerial" 
                                        VisibleIndex="5" Width="20%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption=" تاریخ نصب قطعه" 
                                        FieldName="SystemHDtlDateIn" VisibleIndex="6" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ حذف قطعه" 
                                        FieldName="SystemHDtlDateOut" VisibleIndex="7" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingspager pagesize="20">
                                </settingspager>
                                <settings showfilterrow="True" />
                                <settings showfilterrow="True" />
                                <settings showfilterrow="True" />
                            </dx:ASPxGridView>
                        </detailrow>
                    </templates>
                </dx:ASPxGridView>
        </div>
            <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
    
</asp:Content>

