<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="telPersonalsEditFrm.aspx.vb" Inherits="telPersonalsEditFrm" %>

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
             <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="شماره پرونده"></asp:Label>
              <asp:TextBox ID="txtCode" runat="server" Enabled="False" 
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="نام"></asp:Label>
               <asp:TextBox ID="txtPcUserName" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>
           <div class="col-md-4">
               <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="نام خانوادگی"></asp:Label>
               <asp:TextBox ID="txtPcUserFamili" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>

    </div>
      <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="کد ملی"></asp:Label>
              <asp:TextBox ID="codemelli" runat="server" Enabled="False" 
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="کد پرسنلی"></asp:Label>
               <asp:TextBox ID="txtCodePerson" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>
           <div class="col-md-4">
               <asp:Label ID="Label6" runat="server" CssClass="control-label" Text="نام پدر"></asp:Label>
               <asp:TextBox ID="father" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>

    </div>
        <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="تلفن داخلی "></asp:Label>
              <asp:TextBox ID="teldaxele" runat="server" Enabled="False" 
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="تلفن منزل"></asp:Label>
               <asp:TextBox ID="telmanzel" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>
           <div class="col-md-4">
               <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="موبایل"></asp:Label>
               <asp:TextBox ID="mobile" runat="server"  Enabled="False" 
                    CssClass="form-control"></asp:TextBox>
        </div>

    </div>
    <br />
       <div class="row">
        <div class="col-md-12">
            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="Id"  
                    EnableCallBacks="False" OnHtmlDataCellPrepared="ASPxGrvHead_HtmlDataCellPrepared" ClientIDMode="AutoID" Width="100%">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="10" Visible="False">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="0" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
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
                        <dx:GridViewDataTextColumn Caption="تلفن منزل" FieldName="telmanzel" VisibleIndex="3" Width="5%">
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
                        <dx:GridViewDataTextColumn Caption="کدپرسنلی" FieldName="codePersoneli" 
                            VisibleIndex="6" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره پرونده" FieldName="Id" 
                            VisibleIndex="7" Width="5%">
                              <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("Id")%>'></asp:LinkButton>
                            </dataitemtemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Name="photo" 
            ShowInCustomizationForm="True" VisibleIndex="8" Width="15%">
            <DataItemTemplate>
                <asp:Image ID="Image5" runat="server" Height="96px" Width="64px" />
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center" font-bold="False">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="9" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <SettingsPager PageSize="25">
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

