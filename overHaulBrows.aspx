<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overHaulBrows.aspx.vb" Inherits="overHaulBrows" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
		
          <div class="col-md-3">

                </div>
           <div class="col-md-3">
                     <asp:Label ID="lblTotal" runat="server" Visible="true"></asp:Label>
                               </div>
          <div class="col-md-3">
<asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                </div>
                 
                   <div class="col-md-3">
<asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                </div>
          </div>

    
     <div class="row">
			<div class="col-md-12">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="Id" Width="100%">

                        <settings showfilterrow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="120000" />

<Settings ShowFilterRow="True" ShowGroupPanel="True" showhorizontalscrollbar="True"></Settings>

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True" showdetailbuttons="False"></SettingsDetail>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                        <Columns>
                                <dx:GridViewDataTextColumn Caption="کدپیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                               
                          
                              <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="نام خانوداگی" FieldName="LastName" VisibleIndex="2">
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="شماره پدر" FieldName="FatherName" VisibleIndex="3" >
                               
                            </dx:GridViewDataTextColumn>
                            
                           
                                                 <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" VisibleIndex="4" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="shSh" VisibleIndex="5">
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirthDay" VisibleIndex="6" >
                               
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="شماره کارت بانکی" FieldName="BankCardNo" VisibleIndex="7" >
                               
                            </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="8" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="9" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره موبایل" FieldName="Mobile" VisibleIndex="10" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تلفن ثابت" FieldName="Tell" VisibleIndex="11" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پستی" FieldName="PostalCode" VisibleIndex="12">
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مدرک" VisibleIndex="13" FieldName="madrak" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="رشته" FieldName="FieldOfStudy" VisibleIndex="14" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="وضعیت تاهل" FieldName="tahol" VisibleIndex="15" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نظام وظیفه" FieldName="xedmat" VisibleIndex="16" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="17" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="IsLastOverhaul" VisibleIndex="18" >
                               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" VisibleIndex="19" >
                               
                            </dx:GridViewDataTextColumn>
                             
                                                 


                                <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" VisibleIndex="20">
                                </dx:GridViewDataTextColumn>
                             
                                                 


                        </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settingspager pagesize="15">
                        </settingspager>

                        <settings showfilterrow="True" showgrouppanel="True" ShowHeaderFilterButton="True" ShowHorizontalScrollBar="True" ShowFilterBar="Visible"  />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

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



     
</asp:Content>

