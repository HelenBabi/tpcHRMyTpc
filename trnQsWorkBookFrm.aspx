<%@ Page Language="VB" AutoEventWireup="false" CodeFile="trnQsWorkBookFrm.aspx.vb" Inherits="trnQsWorkBookFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="dabir.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>کارنامه</title>
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
                        ValidationGroup="b" Visible="False" />
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
                      </div>
        </div>
        <div class="div_grid">
                    <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                    ClientIDMode="AutoID" CssClass="gr_" 
                                      Width="100%" 
                                    KeyFieldName="qsID">
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <settings showfilterrow="True" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <settings gridlines="Horizontal" showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <settings gridlines="Horizontal" showfilterrow="True" 
                                    showgroupbuttons="False" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <settingsbehavior allowfocusedrow="True" />
                        <settings gridlines="Horizontal" showgroupbuttons="False" ShowGroupPanel="True" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />

                        <settings showfilterrow="True" />
                                         
                       <Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <SettingsBehavior AllowFocusedRow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" ShowHeaderFilterButton="True" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" ShowHeaderFilterBlankItems="False" ShowHeaderFilterButton="True" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="5%">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="دوره" FieldName="onvan" VisibleIndex="1">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="qsTypeValue" 
                                            VisibleIndex="2" Caption="بخش">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="qsValue" ShowInCustomizationForm="True" Width="40%" Caption="سوال" VisibleIndex="3">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="جواب درست" FieldName="optionn" VisibleIndex="4" Width="30%" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ju" ShowInCustomizationForm="True" VisibleIndex="5" Caption="جواب شما" Width="30%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                     
                        <settingsbehavior allowfocusedrow="True" />

                        <settingspager pagesize="20">
                        </settingspager>

                        <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                          <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
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
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" GridViewID="grdFarei">
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
