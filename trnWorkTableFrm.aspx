<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnWorkTableFrm.aspx.vb" Inherits="trnWorkTableFrm" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <div class="row">
			<div class="col-md-12">         
                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="پیشنهاد های دریافتی من"></asp:Label>
                    
                       <dx:ASPxGridView ID="trnGridWorkTable" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="idd"   
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

    <SettingsPager PageSize="17">
    </SettingsPager>

    <Settings ShowFilterRow="True" />

</dx:ASPxGridView> 
              
             </div> 
                  </div>          

   
  <br />
 <div class="row">
           
            <div class="col-md-4">
                
            </div>
             <div class="col-md-4">
                 <asp:Button ID="Button2" runat="server" Text="برگشت" class="btn btn-warning btn-sm waves-effect waves-light"   /> 
                 <asp:TreeView ID="TreeView1" runat="server">
                     <Nodes>
                         <asp:TreeNode Text="New Node" Value="New Node">
                             <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                         </asp:TreeNode>
                     </Nodes>
                 </asp:TreeView>
            </div>
             <div class="col-md-4">
                
                 
            </div>
        </div>   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


