<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSabeghe.aspx.vb" Inherits="frmSabeghe" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="dabir.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>سوابق سيستم</title>
</head>
<body bgcolor="White">
    <form id="form1" runat="server">
   <div class="div_page" dir="rtl">
   <div class="div_headp" dir="rtl">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" Visible="False" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" 
                        Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" Visible="False" />
</div>
        <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div id="cntt" style=" 
          float:right;
            width:625px;
          height:auto;
          text-align:left;
          vertical-align:middle;

          direction:rtl;">
                <asp:Label ID="lblCnt" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTotal" runat="server" style="color: #FF6600"></asp:Label>
                      </div>
        </div>
        <div class="div_grid">
                    <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="WorkId" Width="948px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="توضيحات" FieldName="WorkNote" 
                                VisibleIndex="10" Width="180px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرويسكار" FieldName="serviskar" 
                                ShowInCustomizationForm="True" VisibleIndex="9" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت اتمام" FieldName="WorkTimeE" 
                                VisibleIndex="8" Width="50px">
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاريخ اتمام" FieldName="WorkDateE" 
                                VisibleIndex="7" Width="50px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت شروع" FieldName="WorkTimeS" 
                                VisibleIndex="6" Width="50px">
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاريخ شروع" FieldName="WorkDateS" 
                                ShowInCustomizationForm="True" VisibleIndex="5" Width="50px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع سرویس" FieldName="WorkTypeName" 
                                ShowInCustomizationForm="True" VisibleIndex="4" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کاربر" FieldName="ferestande" 
                                VisibleIndex="2" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره درخواست" FieldName="erja_id" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="50px">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" 
                                        CommandArgument='<%# eval("erja_id") %>' Font-Bold="True" 
                                        Text='<%# eval("erja_id") %>'></asp:LinkButton>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="*" FieldName="WorkTypeCode" Visible="False" 
                                VisibleIndex="10">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                        <settingspager pagesize="20">
                        </settingspager>

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settings showfilterrow="True" showgrouppanel="True" />
                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                    <settingsdetail showdetailrow="True" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" />

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                        <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                        <templates>
                            <detailrow>
                                <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                    ClientIDMode="AutoID" CssClass="gr_" EnableCallBacks="False" 
                                    KeyFieldName="WorkId" 
                                    OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" Width="400px">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="WorkId" Visible="False" 
                                            VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="BaseWorkCode" FieldName="BaseWorkCode" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn 
                                            FieldName="BaseWorkName" VisibleIndex="2" Width="500px" 
                                            Caption="ريز سرويس انجام شده :">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                <SettingsBehavior AllowFocusedRow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <Settings ShowFilterRow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settingspager pagesize="25" visible="False">
                                    </settingspager>
                                <settings showfilterrow="True" />
                                <settings gridlines="Horizontal" showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                    showgroupbuttons="False" />
                                    <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                    <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                    <settings gridlines="Horizontal" showgroupbuttons="False" />
                                    <styles>
                                        <alternatingrow backcolor="#33CCCC" horizontalalign="Center">
                                        </alternatingrow>
                                    </styles>
                                </dx:ASPxGridView>
                            </detailrow>
                        </templates>
                    </dx:ASPxGridView>
        </div>
        <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server">
                </dx:ASPxGridViewExporter>
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
   </div>
    </form>
</body>
</html>
