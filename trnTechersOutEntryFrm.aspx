<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnTechersOutEntryFrm.aspx.vb" Inherits="trnTechersOutEntryFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
 </div>
     <div class="row">
        <div class="col-md-12">
 
                      <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
 <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
                                    
                                  <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
         
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                                                ValidationGroup="b" />
                <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
               <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
        </div>

    </div>
    <br />
    <asp:Panel ID="Panel1" runat="server" Enabled="False">
    <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label4" runat="server" class="label-control" Text="نام موسسه"></asp:Label>
            <asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="nam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                   
        </div>
        <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" class="label-control" Text="کد ملی"></asp:Label>
            <asp:TextBox ID="codemeli" runat="server" placeholder="کدملی بدون خط تیره"
                    ValidationGroup="a"  class="form-control" MaxLength="10" TextMode="Number" ></asp:TextBox>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="codemeli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
        
         <div class="col-md-4">
               <asp:Label ID="Label6" runat="server" class="label-control" Text="کداقتصادی"></asp:Label>
            <asp:TextBox ID="codeEgtesadi" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="codeEgtesadi" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
        </div>
     <div class="row">
         <div class="col-md-4">
              <asp:Label ID="Label5" runat="server" class="label-control" Text="شناسه ملی"></asp:Label>
            <asp:TextBox ID="shenasemeli" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="shenasemeli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label8" runat="server" class="label-control" Text="شماره ثبت"></asp:Label>
            <asp:TextBox ID="sabtNo" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="sabtNo" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label9" runat="server" class="label-control" Text="کد پستی"></asp:Label>
            <asp:TextBox ID="codePosti" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="codePosti" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
        </div>

    <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label24" runat="server" class="label-control" Text="شماره تماس"></asp:Label>
                            <asp:TextBox ID="tel" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                       
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label25" runat="server" class="label-control" Text="فاکس"></asp:Label>
                      <asp:TextBox ID="fax" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      
        </div>
         <div class="col-md-4">
            <asp:Label ID="nesbatLbl" runat="server" class="label-control" Text="محل اقامت قانونی موسسه" ></asp:Label>
                                     
                 <asp:TextBox ID="mahaleEgamat" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        
                                                                                  
                                         </div> 
      
      
        </div>
  
     <div class="row">
     <div class="col-md-4">
            <asp:Label ID="Label10" runat="server" class="label-control" Text="نوع مالکیت" ></asp:Label>
                                     
                    <asp:DropDownList ID="malekiatType" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                      
                        <asp:ListItem>خصوصی</asp:ListItem>
                        <asp:ListItem>دولتی</asp:ListItem>
                                      
                  </asp:DropDownList>
                     
                                                                                  
                                         </div> 
      
       <div class="col-md-4">
            <asp:Label ID="Label11" runat="server" class="label-control" Text="شماره حساب" ></asp:Label>
                                     
                 <asp:TextBox ID="shHesab" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      
                                                                                  
                                         </div> 
      <div class="col-md-4">
            <asp:Label ID="Label12" runat="server" class="label-control" Text="شماره شبا" ></asp:Label>
                                     
                 <asp:TextBox ID="shShaba" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                     
                                                                                  
                                         </div> 
        </div>

     <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label13" runat="server" class="label-control" Text="بانک"></asp:Label>
            <asp:TextBox ID="bank"   runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
         
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label14" runat="server" class="label-control" Text="شعبه"></asp:Label>
            <asp:TextBox ID="shobe"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label15" runat="server" class="label-control" Text="کد شعبه"></asp:Label>
            <asp:TextBox ID="codeShobe"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
        </div>
        </div>

 
    
     <div class="row">
		<br />
          <div class="col-md-4">
                <asp:Label ID="Label2" runat="server" class="label-control" Text="نام موسسه صاحب حساب"></asp:Label>
            <asp:TextBox ID="namMoseseSahebHesab"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label1" runat="server" class="label-control" Text="نام مدیرعامل"></asp:Label>
                                                
                              <asp:TextBox ID="modirAmel"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                                            
                                         
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label3" runat="server" class="label-control" Text="حسن همکاری"></asp:Label>
                  <asp:TextBox ID="hosnHamkari"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                </div>
         

          </div>
      
     
     <div class="row">
        <div class="col-md-12">
              <asp:Label ID="Label21" runat="server" class="label-control" Text="نتایج حاصل از ارزیابی اولیه تامین کنندگان"></asp:Label>
            <asp:TextBox ID="natayejArzyabiTamin"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         </div>
     <div class="row">
        <div class="col-md-9">
              <asp:Label ID="Label23" runat="server" class="label-control" Text="نتایج حاصل ازارزیابی اثربخشی دوره های آموزشی برگزار شده  قبلی"></asp:Label>
            <asp:TextBox ID="natayejArzyabiGabli"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         
           <div class="col-md-3">
    <asp:CheckBox ID="ChbActiveCode" runat="server" Text="غیرفعال؟" />
               </div>
         </div>
</asp:Panel>
    <br />
      <div class="row">
            <div class="col-md-12">
              
               <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="techID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="ش پرونده" FieldName="techID" 
                            ShowInCustomizationForm="True" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("techID") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("techID") %>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام موسسه" FieldName="nam" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد ملی موسسه" FieldName="codemeli" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد اقتصادی" FieldName="codeEgtesadi" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شناسه ملی" FieldName="shenasemeli" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره ثبت" FieldName="sabtNo" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="7">
                        </dx:GridViewDataCheckColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>
        </div>
             
               
     
       
        </div>
      <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
      <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
</asp:Content>

