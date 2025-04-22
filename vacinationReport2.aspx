<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="vacinationReport2.aspx.vb" Inherits="vacinationReport2" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h5>لیست رزرو واکسیناسیون</h5>
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
               <asp:Label ID="lblCnt" runat="server" Font-Bold="True" style="color: #FF3300"></asp:Label>
                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">
                     <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                </div>

          </div>

     <div class="row">
			<div class="col-md-12">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="reservID" OnHtmlDataCellPrepared="ASPxGrvHead_HtmlDataCellPrepared"  Width="100%">

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                        <settings showfilterrow="True" showgrouppanel="True" ShowHeaderFilterButton="True" ShowHorizontalScrollBar="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settings showfilterrow="True" ShowFilterBar="Visible" />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

<SettingsDetail IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True" showdetailbuttons="False"></SettingsDetail>

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <Columns>
<dx:GridViewDataTextColumn FieldName="value" Width="10%" Caption="نوع قرارداد" VisibleIndex="5">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                ShowInCustomizationForm="True" VisibleIndex="4" Width="10%">
                               
                                                         
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                               
                                                         
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                           
                            <dx:GridViewDataTextColumn Caption="کدپرسنلی" FieldName="codePersoneli" 
                                ShowInCustomizationForm="True" VisibleIndex="3" Width="10%">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                               
                            </dx:GridViewDataTextColumn>
                            
                           
                                                 <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                                ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                               
                                                     <HeaderStyle HorizontalAlign="Center" />
                               
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                               
                                  <HeaderStyle HorizontalAlign="Center" />
                               
                            </dx:GridViewDataTextColumn>
                            
                           
                            <dx:GridViewDataTextColumn Caption="ردیف" FieldName="reservID" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="5%" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" VisibleIndex="6" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ ثبت" FieldName="tarix" VisibleIndex="7">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت ثبت" FieldName="saat" VisibleIndex="8">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Name="photo" 
            ShowInCustomizationForm="True" VisibleIndex="9" Width="50px">
            <DataItemTemplate>
                <asp:Image ID="Image5" runat="server" Height="48px" Width="64px" />
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        </Columns>

                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="120000" />

                        <settingspager pagesize="20">
                        </settingspager>

<Settings ShowFilterRow="True" ShowGroupPanel="True" showhorizontalscrollbar="True"></Settings>

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                                              <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                    </dx:ASPxGridView>
                </div>
         
          </div>
 <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>


        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

