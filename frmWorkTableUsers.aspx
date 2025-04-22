<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmWorkTableUsers.aspx.vb" Inherits="frmWorkTableUsers" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <div class="row">
			<div class="col-md-12">         
                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="صفحه کارتابل فن آوری اطلاعات"></asp:Label>
                       <asp:HiddenField ID="HiddenFieldUserID" runat="server" Value="0" />
                       <dx:ASPxGridView ID="GridKartabl" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="erja_id"  OnHtmlDataCellPrepared="GridKartabl_HtmlDataCellPrepared" 
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
    <Columns>
        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="70px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="مشاهده" 
                    ToolTip="مشاهده اطلاعات کلی درخواست"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
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
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان ارسال" FieldName="zamanErsal" 
                            ShowInCustomizationForm="True" VisibleIndex="2" 
            Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان اتمام" FieldName="zamanEtmam" 
            ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="name_hvaz" 
            ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Name="photo" 
            ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
            <DataItemTemplate>
                <asp:Image ID="Image5" runat="server" Height="24px" Width="24px" />
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="stat_" FieldName="stat_" 
            ShowInCustomizationForm="True" VisibleIndex="7" Visible="False" 
            Width="0px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="8" Width="50px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnEbtal" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' CommandName='<%# "Ebtall" %>' 
                    style="color: #333333; " Text="ابطال؟" 
                    ToolTip="اگر ابطال را کلیک کنید درخواست مربوطه حذف می شود"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
         <dx:GridViewDataTextColumn VisibleIndex="8" Width="50px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' CommandName='<%# "nazar"%>' 
                    style="color: #333333; " Text="ثبت نظرسنجی" 
                    ToolTip="در این قسمت می توانید نمره رضایت خود را از نحوه انجام کار ثبت نمایید"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
    </Columns>
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
            </div>
             <div class="col-md-4">
                
                 
            </div>
        </div>
 
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


