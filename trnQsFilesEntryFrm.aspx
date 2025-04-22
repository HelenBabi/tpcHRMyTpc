<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnQsFilesEntryFrm.aspx.vb" Inherits="trnQsFilesEntryFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div class="row">
        <div class="col-md-12">
              
                 <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
  <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
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
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
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
            
        </div>
           <div class="col-md-4">

        </div>

    </div>
    <br />
    <div class="row">
       <div class="col-md-4">
             <asp:Label ID="Label2" runat="server" class="label-control" Text="دوره"></asp:Label>
            <asp:DropDownList ID="CmbUnit" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                    </asp:DropDownList>
            <asp:RequiredFieldValidator ID="vald_BaseWorkcode5" runat="server" 
                    ControlToValidate="CmbUnit" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
            <div class="col-md-4">
              <asp:Label ID="Label7" runat="server" class="label-control" Text="جلسه"></asp:Label>
            <asp:DropDownList ID="cmbJalase" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False" ValidationGroup="a">
                <asp:ListItem Value="0">انتخاب</asp:ListItem>
                <asp:ListItem Value="1">جلسه اول</asp:ListItem>
                <asp:ListItem Value="2">جلسه دوم</asp:ListItem>
                <asp:ListItem Value="3">جلسه سوم</asp:ListItem>
                <asp:ListItem Value="4">جلسه چهارم</asp:ListItem>
                                    </asp:DropDownList>
            
        </div>
           <div class="col-md-4">
           
        </div>

    </div>
    <div class="row">
       <div class="col-md-4">
            
        </div>
            <div class="col-md-4">
         
        </div>
           <div class="col-md-4">
           
        </div>

    </div>
    <asp:Panel ID="PanelFile" runat="server" GroupingText="ضمایم" Enabled="False" >
      <div class="row">
          
			  <div class="col-md-3">
                    <asp:Label ID="Label4" runat="server" class="label-control" Text="فایل "></asp:Label>
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="400px" Enabled="False" />
                  </div>
          
            <div class="col-md-9">
                <asp:Label ID="Label5" runat="server" class="label-control" Text="توضیحات فایل"></asp:Label>
                  <asp:TextBox ID="sharhFile" runat="server" CssClass="txt_ r5" ValidationGroup="a" Width="100%" ></asp:TextBox>
                  </div>
               
        </div>
    <br />
      
     </asp:Panel>
    <div class="row">
        <div class="col-md-4">
                <asp:Label ID="Label9" runat="server" class="label-control" Text="غیرفعال"></asp:Label>
              <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" />
        </div>
           <div class="col-md-4">

        </div>
           <div class="col-md-4">

        </div>

    </div>
    <br />
    <div class="row">
        <div class="col-md-12">

            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="id"  
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
 <dx:GridViewDataTextColumn Caption="کد" FieldName="id" 
                            ShowInCustomizationForm="True" VisibleIndex="1">
     <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("id")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("id")%>'></asp:LinkButton>
                            </DataItemTemplate>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   <cellstyle horizontalalign="Center">
     </cellstyle>
                                                   </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="sharh" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="دوره" FieldName="onvan" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                                <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="8">
                        </dx:GridViewDataCheckColumn>
                    
                       
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
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

