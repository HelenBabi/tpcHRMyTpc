<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDoreEntryFrm.aspx.vb" Inherits="trnDoreEntryFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
              
                 <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
  <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" />
 <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                 <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                                
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
                   <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
               
              
         
        </div>

    </div>
      <div class="row">
        
           <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" class="label-control" Text="کد"></asp:Label>
            <asp:TextBox ID="idd" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="idd" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
          <div class="col-md-4">
             <asp:Label ID="Label4" runat="server" class="label-control" Text="عنوان"></asp:Label>
             <asp:DropDownList ID="onvan" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12" Enabled="False">
                                   <asp:ListItem Value="0">فنی</asp:ListItem>
                                   <asp:ListItem Value="90">غیرفنی</asp:ListItem>
                                   <asp:ListItem Value="92">كامپیوتر</asp:ListItem>
                                   <asp:ListItem Value="93">سمینار</asp:ListItem>
                                   <asp:ListItem Value="95">زبان</asp:ListItem>
                  <asp:ListItem Value="95">بازدید</asp:ListItem>
                                   </asp:DropDownList>
        </div>
           <div class="col-md-4">
                <asp:Label ID="Label12" runat="server" class="label-control" Text="عنوان دوره"></asp:Label>
            <asp:TextBox ID="dorehayeGozrande" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="dorehayeGozrande" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>

    </div>
    <div class="row">
       <div class="col-md-4">
             <asp:Label ID="Label2" runat="server" class="label-control" Text="مجری دوره"></asp:Label>
            <asp:TextBox ID="mojriDore" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="mojriDore" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
            <div class="col-md-4">
             <asp:Label ID="Label3" runat="server" class="label-control" Text="محل اجرا"></asp:Label>
            <asp:TextBox ID="mahalEjra" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="mahalEjra" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
             <asp:Label ID="Label5" runat="server" class="label-control" Text="روز"></asp:Label>
            <asp:TextBox ID="RUZ" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="RUZ" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>

    </div>
    <div class="row">
       <div class="col-md-4">
             <asp:Label ID="Label6" runat="server" class="label-control" Text="ساعت "></asp:Label>
            <asp:TextBox ID="saat" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="saat" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
            <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" class="label-control" Text="هماهنگی اجرا"></asp:Label>
            <asp:TextBox ID="hamahangiEjra" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="hamahangiEjra" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
             <asp:Label ID="Label8" runat="server" class="label-control" Text="تاریخ شروع"></asp:Label>
            <asp:TextBox ID="tarixShoru" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="tarixShoru" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>

    </div>
    
    <div class="row">
        <div class="col-md-4">
                <asp:Label ID="Label9" runat="server" class="label-control" Text="تاریخ پایان"></asp:Label>
             <asp:TextBox ID="tarixPayan" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tarixPayan" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
   <asp:Label ID="Label10" runat="server" class="label-control" Text="کد پرسنلی"></asp:Label>
             <asp:TextBox ID="codePersoneli" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="codePersoneli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
                 <asp:Label ID="Label11" runat="server" class="label-control" Text="فعال/غیرفعال"></asp:Label>
              <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" />
        </div>

    </div>
    <br />
    <div class="row">
        <div class="col-md-12">

            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="idd"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
 <dx:GridViewDataTextColumn Caption="کد" FieldName="idd" 
                            ShowInCustomizationForm="True" VisibleIndex="1">
     <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("idd")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("idd")%>'></asp:LinkButton>
                            </DataItemTemplate>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   <cellstyle horizontalalign="Center">
     </cellstyle>
                                                   </dx:GridViewDataTextColumn>
                        
                         <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="عنوان" FieldName="onvan" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="عنوان دوره" FieldName="dorehayeGozrande" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                                <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" 
                            ShowInCustomizationForm="True" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="تاریخ اتمام" FieldName="tarixPayan" 
                            ShowInCustomizationForm="True" VisibleIndex="5">
                              <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" 
                            ShowInCustomizationForm="True" VisibleIndex="6">
                              <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra" 
                            ShowInCustomizationForm="True" VisibleIndex="7">
                              <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ" 
                            ShowInCustomizationForm="True" VisibleIndex="7">
                               <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساعت " FieldName="saat" VisibleIndex="9">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="8" Visible="False">
                        </dx:GridViewDataCheckColumn>
                    
                       
                        <dx:GridViewDataTextColumn Caption="هماهنگی اجرا" FieldName="hamahangiEjra" VisibleIndex="10">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    
                       
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
       </div>
    </div>
    
      <div class="row">
        <div class="col-md-4">

        </div>
           <div class="col-md-4">
 <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
        </div>
           <div class="col-md-4">
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
        </div>

    </div>
    
</asp:Content>

