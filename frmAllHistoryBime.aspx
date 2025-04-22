<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmAllHistoryBime.aspx.vb" Inherits="frmAllHistoryBime" %>

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
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
</div>
 
  <div class="div_line">
            <div class="div_nev">
                تعداد
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
                    EnableCallBacks="False" KeyFieldName="codePersoneli" Width="940px" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True" showgrouppanel="True"></Settings>

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded"></SettingsDetail>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" 
                            UnboundType="Integer" VisibleIndex="0" Width="50px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("codePersoneli") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("codePersoneli") %>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="1" Width="100px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                            VisibleIndex="2" Width="120px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" 
                            VisibleIndex="3" Width="50px">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="سال بیمه" FieldName="salBime" 
                            VisibleIndex="4" Width="30px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="mahBime" ShowInCustomizationForm="True" Caption="ماه بیمه" 
                            VisibleIndex="5" Width="100px">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
    <cellstyle horizontalalign="Center">
    </cellstyle>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روزبیمه" FieldName="ruzBime" 
                            VisibleIndex="6" Width="100px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="سال شرکتی" FieldName="salSaxtSherkati" VisibleIndex="7">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ماه شرکتی" FieldName="mahSaxtSherkati" VisibleIndex="8">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز شرکتی" FieldName="ruzSaxtSherkati" VisibleIndex="9">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="سال غیرشرکتی" FieldName="salGeyeSherkati" VisibleIndex="10">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ماه غیرشرکتی" FieldName="mahGeyeSherkati" VisibleIndex="11">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز غیرشرکتی" FieldName="ruzGeyeSherkati" VisibleIndex="12">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="30">
                    </settingspager>

                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />

                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                    <templates>
                        <detailrow>
                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                    ClientIDMode="AutoID" CssClass="gr_" EnableCallBacks="False" 
                                    OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect"  
                                    OnHtmlRowPrepared="grdFarei_HtmlRowPrepared"  Width="955px" 
                                    KeyFieldName="codePersoneli">
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="نوع" FieldName="sherkati" 
                                        VisibleIndex="0" Width="12%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Justify">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="سختی زیان آور" 
                                        FieldName="value" ShowInCustomizationForm="True" 
                                        VisibleIndex="1" Width="15%">
                                        <editcellstyle horizontalalign="Justify">
                                        </editcellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Justify">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="عنوان شغل" 
                                        FieldName="postname" VisibleIndex="2" Width="5%" 
                                        ShowInCustomizationForm="True">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ خاتمه" 
                                        FieldName="tarixpayan" VisibleIndex="3" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixshoru" 
                                        VisibleIndex="4" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="نام سازمان" FieldName="sazmanName" 
                                        VisibleIndex="5" Width="5%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <settingsbehavior allowfocusedrow="True" />
                                <settingspager pagesize="20">
                                </settingspager>
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

