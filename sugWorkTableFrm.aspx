<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="sugWorkTableFrm.aspx.vb" Inherits="sugWorkTableFrm" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <div class="row">
			<div class="col-md-12">         
                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="پیشنهاد های دریافتی من"></asp:Label>
                    
                       <dx:ASPxGridView ID="GridWorkTable" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="sugID"   
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">

    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

    <Settings ShowFilterRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

    <Columns>
            <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="5%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("sugID")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="جزئیات"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="کد پیشنهاد" FieldName="sugID"
                            VisibleIndex="0" ShowInCustomizationForm="True" 
            Width="5%">
        
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                            VisibleIndex="1" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="ferestande" VisibleIndex="3" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
            </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="جهت" FieldName="runeveshtTypeValue" 
                            VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" 
                            VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساعت" FieldName="saat" 
                            VisibleIndex="5" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
      
        
            <dx:GridViewDataTextColumn Caption="متن ارجاع" FieldName="matnErja" VisibleIndex="6" Width="15%">
                <HeaderStyle HorizontalAlign="Center" />
                <cellstyle horizontalalign="Center">
                </cellstyle>
            </dx:GridViewDataTextColumn>
      
        
            <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statusValue" VisibleIndex="7" Width="5%">
                <HeaderStyle HorizontalAlign="Center" />
                <cellstyle horizontalalign="Center">
                </cellstyle>
            </dx:GridViewDataTextColumn>
      
        
    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

    <SettingsPager PageSize="17">
    </SettingsPager>

<Settings ShowFilterRow="True"></Settings>

</dx:ASPxGridView> 
              
             </div> 
                  </div>          

   
  <br />
 <div class="row">
           
            <div class="col-md-4">
                
            </div>
             <div class="col-md-4">
                 <asp:Button ID="Button2" runat="server" Text="برگشت" class="btn btn-warning btn-sm waves-effect waves-light"   /> 
            </div>
             <div class="col-md-4">
                
                 
            </div>
        </div>   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


