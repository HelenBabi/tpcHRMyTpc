<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="protectionCard.aspx.vb" Inherits="protectionCard" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
 
                   
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                  
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                                  <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
         
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                                                ValidationGroup="b" />
                <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
            <asp:ImageButton ID="ImageButton1" runat="server" Height="48px" ImageUrl="~/Icons/deleted.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
        </div>

    </div>
    <br />

     <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="کد چارت"></asp:Label>
              <asp:TextBox ID="chartID" runat="server" Enabled="False" 
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-8">
                  <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="چارت"></asp:Label>
               <asp:TextBox ID="chartName" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>
          

    </div>
      <div class="row">
        <div class="col-md-4">
               <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="کد پرسنلی"></asp:Label>
               <asp:TextBox ID="codePersoneli" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label5" runat="server" CssClass="control-label" Text=" کد پرسنلی سرپرست"></asp:Label>
               <asp:TextBox ID="personelAdmin" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>
          
           <div class="col-md-4">
             <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" Text="غیرفعال؟" />
        </div>

    </div>
        
    <br />
     <div class="row">

     
                        
			<div class="col-md-10">
               
                <asp:Panel ID="Panel2" runat="server" DefaultButton="ImageButton2" >
                <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="جستجو بر اساس همه فیلدها"></asp:Label>
          <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" placeholder="برای جستجو قسمتی از نام و نام خانوادگی یا تلفن داخلی و  یا امور/واحد یا موبایل یا کد پرسنلی یا سمت را بنویسید و Enter بزنید " ></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                    </asp:Panel>
                </div>
             			<div class="col-md-2">

                </div>
               </div>
    <br />
       <div class="row">
        <div class="col-md-12">
            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="codePersoneli" OnHtmlDataCellPrepared="ASPxGrvHead_HtmlDataCellPrepared" Width="100%" 
                    ClientIDMode="AutoID">

             <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True" showgrouppanel="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded"></SettingsDetail>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Visible="False">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد چارت" FieldName="chartID" 
                            VisibleIndex="1" Width="5%">
                             <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("chartID")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("chartID")%>'></asp:LinkButton>
                            </dataitemtemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="چارت" FieldName="chartName" 
                            VisibleIndex="2" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" 
                            ShowInCustomizationForm="True" VisibleIndex="3" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="4" Width="10%">
                             
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگي" FieldName="family" 
                            VisibleIndex="5" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       
                       
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی سرپرست" FieldName="personelAdmin" VisibleIndex="6" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Name="photo" 
            ShowInCustomizationForm="True" VisibleIndex="7" Width="15%">
            <DataItemTemplate>
                <asp:Image ID="Image5" runat="server" Height="96px" Width="64px" />
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center" font-bold="False">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="8" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <SettingsPager PageSize="25">
                    </SettingsPager>

<Settings ShowFilterRow="True"></Settings>

                    <settingsdetail exportmode="All" showdetailrow="True" IsDetailGrid="True" />
                    <templates>
                        <detailrow>
                            
                                    <detailrow>
                                        <dx:ASPxGridView ID="ASPxGrvDtl" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="chartID" OnBeforePerformDataSelect="ASPxGrvDtl_BeforePerformDataSelect"
                                            OnHtmlDataCellPrepared="ASPxGrvDtl_BeforePerformDataSelect"   Width="100%">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                            <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                            <Columns>
                                                <dx:GridViewCommandColumn Visible="False" VisibleIndex="0">
                                                    <clearfilterbutton visible="True">
                                                    </clearfilterbutton>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="کد چارت" FieldName="chartID" VisibleIndex="1" Width="5%">
                                                    <dataitemtemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("chartID")%>' CommandName='<%# "select" %>' Text='<%# Eval("chartID")%>'></asp:LinkButton>
                                                    </dataitemtemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle horizontalalign="Center">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="چارت" FieldName="chartName" VisibleIndex="2" Width="15%">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle horizontalalign="Center">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" ShowInCustomizationForm="True" VisibleIndex="3" Width="5%">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle horizontalalign="Right">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="4" Width="10%">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle horizontalalign="Center">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="نام خانوادگي" FieldName="family" VisibleIndex="5" Width="15%">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle horizontalalign="Center">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="کد پرسنلی سرپرست" FieldName="personelAdmin" VisibleIndex="6" Width="5%">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle horizontalalign="Center">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="photo" ShowInCustomizationForm="True" VisibleIndex="7" Width="15%">
                                                    <dataitemtemplate>
                                                        <asp:Image ID="Image9" runat="server" Height="96px" Width="64px" />
                                                    </dataitemtemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <cellstyle font-bold="False" horizontalalign="Center">
                                                    </cellstyle>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settingsbehavior allowfocusedrow="True" />
                                            <settingspager pagesize="25">
                                            </settingspager>
                                            <settings showfilterrow="True" />
                                            <settingsdetail exportmode="Expanded" showdetailrow="True" />
                                        </dx:ASPxGridView>
                                    </detailrow>
                                </templates>
                            </dx:ASPxGridView>
                        </detailrow>
                    </templates>

                </dx:ASPxGridView>
        </div>

    </div>
    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
 <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />

   
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
</asp:Content>

