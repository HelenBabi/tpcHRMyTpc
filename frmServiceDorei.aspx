<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmServiceDorei.aspx.vb" Inherits="frmServiceDorei" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script src="js-persian-cal.min.js" type="text/javascript"></script>
    <link href="js-persian-cal.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function openNewWin(inp1, inp2) {
            var div_ = document.getElementById("pcal1");
            div_.setAttribute('value', inp1);
            var div_ = document.getElementById("pcal2");
            div_.setAttribute('value', inp2);
            //            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');

            //            div_.focus();

        }

</script>
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
                &nbsp;از تاریخ</div>
            <div class="div_txt">
            
               <input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                </div>
            <div class="div_txt">
            
               &nbsp;<asp:Label ID="lblDateE" runat="server" Font-Bold="True" style="color: #FF3300" 
                    Visible="False"></asp:Label>
                <asp:Label ID="lblDateS" runat="server" Font-Bold="True" style="color: #FF3300" 
                    Visible="False"></asp:Label>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                سرويس شده ها</div>
            <div class="div_txt">
                <asp:Label ID="lblCnt" runat="server" Font-Bold="True" style="color: #FF3300"></asp:Label>
                </div>
                    <div class="div_val">
            </div>
        </div>
           <div class="div_line">
            <div class="div_nev">
                تا تاریخ</div>
            <div class="div_txt">
                <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly" 
                    __designer:mapid="16d"/></div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                سرويس نشده ها</div>
            <div class="div_txt">
                <asp:Label ID="lblCntNotDorei" runat="server" Font-Bold="True" 
                    style="color: #FF3300"></asp:Label>
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_grid">
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        ClientIDMode="AutoID" Width="930px">
                        <TabPages>
                            <dx:TabPage Text="سیستم هایی که سرویس دوره ای شده اند">
                                <ContentCollection>
                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                                            ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="SystemCode" 
                                            Width="928px">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                                                    ShowInCustomizationForm="True" UnboundType="Integer" VisibleIndex="0" 
                                                    Width="80px">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" 
                                                            CommandArgument='<%# eval("SystemCode") %>' CommandName='<%# "select" %>' 
                                                            Text='<%# eval("SystemCode") %>'></asp:LinkButton>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="نام كاربر" FieldName="karbar" 
                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="120px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="تاريخ سرويس" FieldName="MaxOfWorkDateE" 
                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ساختمان" FieldName="BuildingName" 
                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="80px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" 
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="80px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="اتاق" FieldName="SystemRoom" 
                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="تلفن" FieldName="SystemPhone" 
                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="50px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="سرويسكار" FieldName="serviskar" 
                                                    ShowInCustomizationForm="True" VisibleIndex="7" Width="100px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="توضيحات" FieldName="WorkNote" 
                                                    ShowInCustomizationForm="True" VisibleIndex="8" Width="240px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPager PageSize="25">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" />
                                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                            <Styles>
                                                <DetailRow BackColor="#CCFFCC">
                                                </DetailRow>
                                            </Styles>
                                            <Templates>
                                                <DetailRow>
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
                                                            <dx:GridViewDataTextColumn Caption="نام قطعه" FieldName="BaseSystemHDtlNmae" 
                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Right">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="مدل" FieldName="BaseModelName" 
                                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="15%">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="اموال قطعه" FieldName="AmvalCode" 
                                                                VisibleIndex="3" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="TP شماره" FieldName="SystemHDtlTP" 
                                                                VisibleIndex="4" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="سريال" FieldName="SystemHDtlSerial" 
                                                                VisibleIndex="5" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption=" تاریخ نصب قطعه" 
                                                                FieldName="SystemHDtlDateIn" VisibleIndex="6" Width="10%">
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="تاریخ حذف قطعه" 
                                                                FieldName="SystemHDtlDateOut" VisibleIndex="7" Width="10%">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Right">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="تاریخ گارانتی" 
                                                                FieldName="SystemHDtlGarantyDate" ShowInCustomizationForm="True" 
                                                                VisibleIndex="8" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="مك آدرس" FieldName="SystemHDtlMac" 
                                                                VisibleIndex="9" Width="15%">
                                                                <editcellstyle horizontalalign="Justify">
                                                                </editcellstyle>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Left">
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
                                                </DetailRow>
                                            </Templates>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="سیستم هایی که سرویس دوره ای نشده اند">
                                <ContentCollection>
                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="grdNotDprei" runat="server" AutoGenerateColumns="False" 
                                            ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="SystemCode" 
                                            Width="925px">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                                                    ShowInCustomizationForm="True" UnboundType="Integer" VisibleIndex="0" 
                                                    Width="70px">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" 
                                                            CommandArgument='<%# eval("SystemCode") %>' CommandName='<%# "select" %>' 
                                                            Text='<%# eval("SystemCode") %>'></asp:LinkButton>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="نام كاربر" FieldName="karbar" 
                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ساختمان" FieldName="BuildingName" 
                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" 
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="اتاق" FieldName="SystemRoom" 
                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="تلفن" FieldName="SystemPhone" 
                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPager PageSize="30">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" />
                                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                            <Styles>
                                                <DetailRow BackColor="#CCFFCC">
                                                </DetailRow>
                                            </Styles>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView ID="grdFarei0" runat="server" AutoGenerateColumns="False" 
                                                        ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="SystemCode" 
                                                        OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" 
                                                        OnHtmlRowPrepared="grdFarei_HtmlRowPrepared" Width="930px">
                                                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="*" FieldName="SystemHDtlStatus" 
                                                                Visible="False" VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="نام قطعه" FieldName="BaseSystemHDtlNmae" 
                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Right">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="مدل" FieldName="BaseModelName" 
                                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="اموال قطعه" FieldName="AmvalCode" 
                                                                VisibleIndex="3" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="TP شماره" FieldName="SystemHDtlTP" 
                                                                VisibleIndex="4" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="سريال" FieldName="SystemHDtlSerial" 
                                                                VisibleIndex="5" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption=" تاریخ نصب قطعه" 
                                                                FieldName="SystemHDtlDateIn" VisibleIndex="6" Width="10%">
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="تاریخ حذف قطعه" 
                                                                FieldName="SystemHDtlDateOut" VisibleIndex="7" Width="10%">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Right">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="تاریخ گارانتی" 
                                                                FieldName="SystemHDtlGarantyDate" ShowInCustomizationForm="True" 
                                                                VisibleIndex="8" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="MacAddres" FieldName="SystemHDtlMac" 
                                                                VisibleIndex="9" Width="10%">
                                                                <editcellstyle horizontalalign="Justify">
                                                                </editcellstyle>
                                                                <cellstyle horizontalalign="Justify">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <settingsbehavior allowfocusedrow="True" />
                                                        <settingsbehavior allowfocusedrow="True" />
                                                        <settingspager pagesize="20">
                                                        </settingspager>
                                                        <settings showfilterrow="True" />
                                                        <settings showfilterrow="True" />
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                    <br />
    	<script type="text/javascript">
    	    var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
    	    //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

    	    //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
    	    //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
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

