<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="telPersonalsFrm.aspx.vb" Inherits="telPersonalsFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
 
                   
                  
            <asp:ImageButton ID="ImageButton1" runat="server" Height="48px" ImageUrl="~/Icons/deleted.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
        </div>

    </div>
    <br />

   <div class="row">

     
                        
			<div class="col-md-8">
               
                <asp:Panel ID="Panel2" runat="server" DefaultButton="ImageButton2" >
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="جستجو بر اساس همه فیلدها"></asp:Label>
          <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" placeholder="برای جستجو قسمتی از نام و نام خانوادگی یا تلفن داخلی و منزل یا امور/واحد یا موبایل یا کد پرسنلی یا سمت را بنویسید و Enter بزنید " ></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                    </asp:Panel>
                </div>
             			<div class="col-md-4">
 <%--<dx:ASPxComboBox ID="CmbPersonel" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains"  CssClass="btn btn-default dropdown-toggle" Width="100%">
                                            </dx:ASPxComboBox>--%>
                </div>
               </div>
    <br />
       <div class="row">
        <div class="col-md-12">
            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="codePersoneli"  
                    EnableCallBacks="False" OnHtmlDataCellPrepared="ASPxGrvHead_HtmlDataCellPrepared" ClientIDMode="AutoID" Width="100%">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="0" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگي" FieldName="family" 
                            VisibleIndex="1" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن داخلی" FieldName="teldaxeli" VisibleIndex="2" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن منزل" FieldName="telmanzel" VisibleIndex="3" Width="10%">
                                                      <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" 
                            ShowInCustomizationForm="True" VisibleIndex="5" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" 
                            VisibleIndex="6" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="codePersoneli" ShowInCustomizationForm="True" Width="5%" Caption="کدپرسنلی" VisibleIndex="7">

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="سمت" FieldName="postname" VisibleIndex="8" Width="10%">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <cellstyle horizontalalign="Center">
                                 </cellstyle>
                        </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Name="photo" 
            ShowInCustomizationForm="True" VisibleIndex="9" Width="15%">
            
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
                                 <dataitemtemplate>
                                     <asp:Image ID="Image5" runat="server" Height="96px" Width="64px" />
                                 </dataitemtemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <cellstyle font-bold="False" horizontalalign="Center">
                                 </cellstyle>
        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <SettingsPager PageSize="20">
                    </SettingsPager>

                    <Settings ShowFilterRow="True" />

                </dx:ASPxGridView>
        </div>

    </div>
    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
 <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />

   
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
</asp:Content>

