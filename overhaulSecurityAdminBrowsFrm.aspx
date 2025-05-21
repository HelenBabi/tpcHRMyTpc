<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overhaulSecurityAdminBrowsFrm.aspx.vb" Inherits="overhaulSecurityAdminBrowsFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<style>
    @page { size: A4; margin: 1cm; }
    .page {
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-template-rows: repeat(2, auto);
        gap: 0.4cm;
        page-break-after: always;
    }
    .card {
        width: 4.8cm;
        height: 7.6cm;
        border: 1px solid black;
        padding: 0.2cm;
        box-sizing: border-box;
        font-family: Tahoma;
    }
    .photo {
        width: 2.5cm;
        height: 3.2cm;
        object-fit: cover;
        margin: 0.2cm auto;
        border: 1px solid #000;
    }
    .info { font-size: 10px; text-align: right; }
    .signature img { width: 2.5cm; }
    .back { font-size: 10px; direction: rtl; line-height: 1.5; }
</style>

     
 <div class="row">
           
            <div class="col-md-4">
                <asp:Button ID="Button2" runat="server" Text="بازگشت" CssClass="btn btn-warning btn-sm float-end" />
                <asp:Button ID="btnPrintSelected" runat="server" Text="چاپ کارت‌ها" OnClientClick="getSelectedRows(); return false;" CssClass="btn btn-primary" />

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
                <h5 class="text-center">لیست ثبت نام</h5>
            </div>
        </div>

        <!-- دیو برای جدول -->
        <div class="table-responsive shadow-sm p-3 mb-5 bg-white rounded" style="width:100%">
             <dx:ASPxGridView ID="GridWorkTable" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="Id"  Width="100%" 
                    ClientIDMode="AutoID"
                 >
                  <ClientSideEvents SelectionChanged="function(s, e) {
        var selectedKeys = s.GetSelectedKeysOnPage();
        PageMethods.SendSelected(selectedKeys, function() {
            window.open('PrintCards.aspx', '_blank');
        });
    }" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" />

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
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" Visible="False" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" Visible="False" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" Visible="False" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" Visible="False" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" Visible="False" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" Visible="False" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23" Visible="False">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24" Visible="False">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25" Visible="False">
                     </dx:GridViewDataTextColumn>
                </Columns>

                <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowGroupPanel="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

            </dx:ASPxGridView>
        </div>
    </div>
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


