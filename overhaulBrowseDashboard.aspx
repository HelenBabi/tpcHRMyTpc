<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overhaulBrowseDashboard.aspx.vb" Inherits="overhaulBrowseDashboard" %>
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

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                        

   
 <div class="row">
           
            <div class="col-md-4">
                <asp:Button ID="Button2" runat="server" Text="بازگشت" CssClass="btn btn-warning btn-sm float-end" />

            </div>
             <div class="col-md-4">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="GridWorkTable">
                </dx:ASPxGridViewExporter>
            </div>
             <div class="col-md-4">    
                 <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
            </div>
        </div>
      
 <div class="row">
           
            <div class="col-md-12">
                   
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientIDMode="AutoID" Width="100%" 
        style="margin-top: 0px" TabIndex="3">
             <TabPages>
                 <dx:TabPage Name="trn" Text="آمار روزانه">
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

<div class="table-responsive shadow-sm p-3 mb-5 bg-white rounded" style="width:100%">
             <dx:ASPxGridView ID="GridWorkTable" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="tarix"  Width="100%" 
                    ClientIDMode="AutoID">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowGroupPanel="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                
                <Columns>
                     <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" 
                            UnboundType="Integer" VisibleIndex="0">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="senderr" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="گیرنده" FieldName="reciverr" VisibleIndex="1" />
                     <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" ShowInCustomizationForm="True" VisibleIndex="3">
                     </dx:GridViewDataTextColumn>
                </Columns>

                <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

            </dx:ASPxGridView>
        </div>
                                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <dx:TabPage Text="آمار چارت" Name="b">
                            <activetabstyle font-bold="True">
                            </activetabstyle>
                            <contentcollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <div class="row">
			<div class="col-md-12">
                
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="chartId"  Width="100%"
                    ClientIDMode="AutoID">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowGroupPanel="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                
                <Columns>

                    
                      <dx:GridViewDataTextColumn Caption="امور" FieldName="senderr" 
                            UnboundType="Integer" VisibleIndex="0">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="واحد" FieldName="unit" 
                            UnboundType="Integer" VisibleIndex="1">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="چارت" FieldName="Title" 
                            UnboundType="Integer" VisibleIndex="2">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" 
                           
                            UnboundType="Integer" VisibleIndex="3" Width="150px">
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="شبکار/روزکار" FieldName="shift" VisibleIndex="4" />
                     <dx:GridViewDataTextColumn Caption="مبلغ نفرساعت" FieldName="cahrtPrice" ShowInCustomizationForm="True" VisibleIndex="5">
                     </dx:GridViewDataTextColumn>
                </Columns>

                <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

            </dx:ASPxGridView>
                
                
                
                </div>
         
          </div>
                                </dx:ContentControl>
                            </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="آمار نفرات" Name="c">
                            <activetabstyle font-bold="True">
                            </activetabstyle>
                            <contentcollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <div class="row">
			<div class="col-md-12">
                
                <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="Id"  Width="100%" 
                    ClientIDMode="AutoID">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowGroupPanel="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                
                <Columns>
                       <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" ShowInCustomizationForm="True" VisibleIndex="0" >
                                       <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" ShowInCustomizationForm="True" VisibleIndex="1">
                          </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" ShowInCustomizationForm="True" VisibleIndex="2">
                           </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" ShowInCustomizationForm="True" VisibleIndex="3">
                     </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" ShowInCustomizationForm="True" VisibleIndex="4">
                                               </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="تاریخ شروع بکار" FieldName="tarixStart" ShowInCustomizationForm="True" VisibleIndex="5">
                     </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statTitle" ShowInCustomizationForm="True" VisibleIndex="6">
                     </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="امور" FieldName="omur" 
                            UnboundType="Integer" VisibleIndex="6">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="واحد" FieldName="unit" 
                            UnboundType="Integer" VisibleIndex="7">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="چارت" FieldName="Title" 
                            UnboundType="Integer" VisibleIndex="8">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       
                    <dx:GridViewDataTextColumn Caption="شبکار/روزکار" FieldName="shift" VisibleIndex="9" />
                     <dx:GridViewDataTextColumn Caption="مبلغ نفرساعت" FieldName="cahrtPrice" ShowInCustomizationForm="True" VisibleIndex="10">
                     </dx:GridViewDataTextColumn>
                   
                  
                   
                     <dx:GridViewDataTextColumn Caption="تاریخ چارت" FieldName="tarix" ShowInCustomizationForm="True" VisibleIndex="11">
                           </dx:GridViewDataTextColumn>
                                             
                </Columns>

                <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

            </dx:ASPxGridView>
                
                
                
                </div>
         
          </div>
                                </dx:ContentControl>
                            </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="آمار وضعیت" Name="d">
                            <activetabstyle font-bold="True">
                            </activetabstyle>
                            <contentcollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <div class="row">
			<div class="col-md-12">
                
                <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="Id"  Width="100%" 
                    ClientIDMode="AutoID">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowGroupPanel="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                
                <Columns>

                              <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" 
                            UnboundType="Integer" VisibleIndex="0">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" 
                            UnboundType="Integer" VisibleIndex="1">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>                      
                                                            
                </Columns>

                <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

            </dx:ASPxGridView>
                
                
                
                </div>
         
          </div>
                                </dx:ContentControl>
                            </contentcollection>
                 </dx:TabPage>
             </TabPages>
           
    </dx:ASPxPageControl>
           


                </div>
     </div>
   
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


