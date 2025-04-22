<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDoreByDeviceBrowsFrm.aspx.vb" Inherits="trnDoreByDeviceBrowsFrm" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
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
      <div class="row">
			<div class="col-md-2">

                </div>
          <div class="col-md-2">
<asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                </div>
          <div class="col-md-2">
<asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">
                     <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                </div>

          </div>

  <%--  <div class="row">
			<div class="col-md-4">
                <asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="از مورخ"></asp:Label>
<input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/>
                </div>
          <div class="col-md-4">

                </div>
         <div class="col-md-4">

                </div>
         </div>
     <div class="row">
			<div class="col-md-4">
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="تا مورخ"></asp:Label>
                <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly" 
                    __designer:mapid="16d"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                </div>
          <div class="col-md-4">
              <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="نمایش همه رکوردها" />

                </div>
         <div class="col-md-4">
              <asp:Label ID="lblCnt" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTotal" runat="server" style="color: #FF6600"></asp:Label>
                </div>
         </div>--%>
       <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientIDMode="AutoID" Width="100%" 
        style="margin-top: 0px" TabIndex="3">
             <TabPages>
                 <dx:TabPage Text="پرسنل در حین اجرای دوره" Name="a">
                     <activetabstyle font-bold="True">
                     </activetabstyle>
                     <tabstyle width="20%">
                     </tabstyle>
                     <ContentCollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
<div class="row">
			<div class="col-md-12">
                
                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" KeyFieldName="codePersoneli" Width="90%" >
                    <styles>
                        <detailrow>
                        </detailrow>
                    </styles>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" />

                        <settings showfilterrow="True" showgrouppanel="True" />
                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" ExportMode="Expanded" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewDataTextColumn Caption="عنوان دوره" FieldName="onvan" ShowInCustomizationForm="True" VisibleIndex="0" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" ShowInCustomizationForm="True" VisibleIndex="1" Width="8%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="2" Width="8%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" VisibleIndex="3" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدت زمان مورد نیاز برای این دوره/ساعت" FieldName="saatNiaz" VisibleIndex="4" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدت زمان حضور پرسنل در این دوره/ساعت" FieldName="jam" VisibleIndex="5" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="محل برگزاری" FieldName="makan" ShowInCustomizationForm="True" VisibleIndex="6" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره دستگاه" FieldName="deviceID" ShowInCustomizationForm="True" VisibleIndex="7" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" ShowInCustomizationForm="True" VisibleIndex="8" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <settingsbehavior allowfocusedrow="True" />

                    <settingspager pagesize="30">
                    </settingspager>

                    <settings showfilterrow="True" showgrouppanel="True" />
                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingscookies enabled="True" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" showdetailrow="True" />

                    <templates>
                        <detailrow>
                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="idd" Width="100%" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect">

                        <settings showfilterrow="True" ShowGroupPanel="True" showhorizontalscrollbar="True"  />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settings showfilterrow="True" ShowGroupPanel="True" showfilterrowmenu="True" />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" />

<Settings ShowFilterRow="True" ShowGroupPanel="True" showfilterbar="Visible" showheaderfilterbutton="True" showhorizontalscrollbar="True"></Settings>

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                        <Columns>
                              <dx:GridViewDataTextColumn Caption="کد تراکنش" FieldName="idd" ShowInCustomizationForm="True" VisibleIndex="1">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" VisibleIndex="5">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="ساعت ورود" FieldName="StartTime" VisibleIndex="6">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="ساعت خروج" FieldName="endTime" VisibleIndex="7" >
                               
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                           
                                                 <dx:GridViewDataTextColumn Caption="شماره دستگاه" FieldName="deviceID" VisibleIndex="8" >
                               
                                                     <HeaderStyle HorizontalAlign="Center" />
                                                     <cellstyle horizontalalign="Center">
                                                     </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مکان" FieldName="makan" VisibleIndex="10">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                             
                        </Columns>

<SettingsBehavior AllowFocusedRow="True" autofilterrowinputdelay="120000"></SettingsBehavior>

                        <settingspager pagesize="15">
                        </settingspager>

                    <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" showdetailbuttons="False"></SettingsDetail>

                                              <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                    </dx:ASPxGridView>
                        </detailrow>
                    </templates>
                </dx:ASPxGridView>
                
                </div>
         
          </div>
                                   </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>

                        <dx:TabPage Text="پرسنل اتمام دوره" Name="b">
                            <activetabstyle font-bold="True">
                            </activetabstyle>
                            <contentcollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <div class="row">
			<div class="col-md-12">
                
                <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" KeyFieldName="codePersoneli" Width="90%" >
                    <styles>
                        <detailrow>
                        </detailrow>
                    </styles>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" />

                        <settings showfilterrow="True" showgrouppanel="True" />
                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" ExportMode="Expanded" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewDataTextColumn Caption="عنوان دوره" FieldName="onvan" ShowInCustomizationForm="True" VisibleIndex="0" Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" ShowInCustomizationForm="True" VisibleIndex="1" Width="8%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="2" Width="8%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" VisibleIndex="3" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدت زمان مورد نیاز برای این دوره/ساعت" FieldName="saatNiaz" VisibleIndex="4" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدت زمان حضور پرسنل در این دوره/ساعت" FieldName="jam" VisibleIndex="5" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل برگزاری" FieldName="makan" ShowInCustomizationForm="True" VisibleIndex="6" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره دستگاه" FieldName="deviceID" ShowInCustomizationForm="True" VisibleIndex="7" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" ShowInCustomizationForm="True" VisibleIndex="8" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <settingsbehavior allowfocusedrow="True" />

                    <settingspager pagesize="30">
                    </settingspager>

                    <settings showfilterrow="True" showgrouppanel="True" />
                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingscookies enabled="True" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" showdetailrow="True" />

                    <templates>
                        <detailrow>
                            <dx:ASPxGridView ID="ASPxGridViewEtmam" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="idd" Width="100%" OnBeforePerformDataSelect="ASPxGridViewEtmam_BeforePerformDataSelect">

                        <settings showfilterrow="True" ShowGroupPanel="True" showhorizontalscrollbar="True"  />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settings showfilterrow="True" ShowGroupPanel="True" showfilterrowmenu="True" />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" />

<Settings ShowFilterRow="True" ShowGroupPanel="True" showfilterbar="Visible" showheaderfilterbutton="True" showhorizontalscrollbar="True"></Settings>

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                        <Columns>
                              <dx:GridViewDataTextColumn Caption="کد تراکنش" FieldName="idd" ShowInCustomizationForm="True" VisibleIndex="1">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" VisibleIndex="5">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="ساعت ورود" FieldName="StartTime" VisibleIndex="6">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="ساعت خروج" FieldName="endTime" VisibleIndex="7" >
                               
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                           
                                                 <dx:GridViewDataTextColumn Caption="شماره دستگاه" FieldName="deviceID" VisibleIndex="8" >
                               
                                                     <HeaderStyle HorizontalAlign="Center" />
                                                     <cellstyle horizontalalign="Center">
                                                     </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مکان" FieldName="makan" VisibleIndex="10">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                             
                        </Columns>

<SettingsBehavior AllowFocusedRow="True" autofilterrowinputdelay="120000"></SettingsBehavior>

                        <settingspager pagesize="15">
                        </settingspager>

                    <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" showdetailbuttons="False"></SettingsDetail>

                                              <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                    </dx:ASPxGridView>
                        </detailrow>
                    </templates>
                </dx:ASPxGridView>
                
                </div>
         
          </div>
                                </dx:ContentControl>
                            </contentcollection>
                 </dx:TabPage>

                        </TabPages>
             <contentstyle backcolor="#D9E1EC">
             </contentstyle>
    </dx:ASPxPageControl>

     
 <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>

     <dx:ASPxGridViewExporter ID="ASPxGridViewExporterEtmam" runat="server" 
                    GridViewID="ASPxGridView1">
                </dx:ASPxGridViewExporter>
        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

