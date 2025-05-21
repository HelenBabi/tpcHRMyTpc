<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overhaulBrowseSms.aspx.vb" Inherits="overhaulBrowseSms" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


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

   <div class="container mt-3" style="width:100%">
        <div class="row">
            <div class="col-md-12">
                <!-- عنوان یا هر محتویات دیگر داخل صفحه -->
                <h5 class="text-center">لیست پیامک های ارسالی</h5>
            </div>
        </div>

        <!-- دیو برای جدول -->
        <div class="table-responsive shadow-sm p-3 mb-5 bg-white rounded" style="width:100%">
             <dx:ASPxGridView ID="GridWorkTable" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="Id"  Width="100%" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                
                <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowGroupPanel="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="5">
                     </dx:GridViewDataTextColumn>
                    
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="6">
                     </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="8"></dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                    <dx:GridViewDataTextColumn Caption="متن پیامک" FieldName="message" VisibleIndex="9" Width="60%" />
                     <dx:GridViewDataTextColumn Caption="وضعیت ارسال" FieldName="stat" VisibleIndex="10">
                     </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="تاریخ ارسال" FieldName="tarixSms" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="ساعت ارسال" FieldName="saat" VisibleIndex="12" />
                </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

            </dx:ASPxGridView>
        </div>
    </div>
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


