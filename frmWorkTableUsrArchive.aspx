<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmWorkTableUsrArchive.aspx.vb" Inherits="frmWorkTableUsrArchive" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
                 <tr>
                     <td>
 
             
          
                        

                  <asp:Label ID="lblCnt" runat="server" style="font-weight: 700; color: #000000;"></asp:Label>
          
                     

                     </td>
                 </tr>
             </table>
         <div class="div_grid">
 
             
          
                        

          <dx:ASPxGridView ID="GridKartabl" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="erja_id"  
                    EnableCallBacks="False" ClientIDMode="AutoID">
    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="شماره درخواست" FieldName="erja_id" 
                            VisibleIndex="0" ShowInCustomizationForm="True" 
            Width="50px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="اموال" FieldName="systemcode" 
                            ShowInCustomizationForm="True" VisibleIndex="1" Name="sel" 
            Width="70px">
            <dataitemtemplate>
                <asp:LinkButton ID="LinkButton2" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' Text='<%# eval("systemcode") %>' 
                    CommandName='<%# "entexab" %>'></asp:LinkButton>
            </dataitemtemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Right">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان ارسال" FieldName="zamanErsal" 
                            ShowInCustomizationForm="True" VisibleIndex="2" 
            Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="name_hvaz" 
            ShowInCustomizationForm="True" VisibleIndex="3" Width="50px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان اتمام" FieldName="zamanEtmam" 
            ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="5" Width="100px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnArchive" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' CommandName='<%# "kartabll" %>' 
                    Text="برگرداندن به کارتابل"></asp:LinkButton>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True" />
    <SettingsPager PageSize="20">
    </SettingsPager>
    <Settings ShowFilterRow="True" />
</dx:ASPxGridView>
         
      
        </div>
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>


