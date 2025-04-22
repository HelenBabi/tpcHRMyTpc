<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmWorkTableAdminAll.aspx.vb" Inherits="frmWorkTableAdminAll" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                     
                        

                  <table class="style1">
                      <tr>
                          <td>
          
                        

                              <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
                                  GridViewID="GridKartabl" PaperName="AllTickets">
                              </dx:ASPxGridViewExporter>
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
          
                     

   
                          </td>
                      </tr>
                      <tr>
                          <td>
          
                        

                  <asp:Label ID="lblCnt0" runat="server" 
                      style="font-weight: 700; " CssClass="style2"></asp:Label>
          
                        

                  <asp:Label ID="lblCntrch" runat="server" 
                      style="font-weight: 700; " CssClass="style2" Visible="False"></asp:Label>
          
                     

   
                          </td>
                      </tr>
                  </table>
          
                        

   
  <div class="div_grid">
  
          <dx:ASPxGridView ID="GridKartabl" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="erja_id"  OnHtmlDataCellPrepared="GridKartabl_HtmlDataCellPrepared" 
                    EnableCallBacks="False" ClientIDMode="AutoID">
    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
    <Columns>
        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="70px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="مشاهده"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="شماره درخواست" FieldName="erja_id"
                            VisibleIndex="1" ShowInCustomizationForm="True" 
            Width="50px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="اموال" FieldName="systemcode" 
                            ShowInCustomizationForm="True" VisibleIndex="2" Name="sel" 
            Width="70px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="ferestande" 
            ShowInCustomizationForm="True" VisibleIndex="3" Width="120px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName_" 
            ShowInCustomizationForm="True" VisibleIndex="4" Width="70px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="نوع سرویس" FieldName="WorkTypeName" 
            ShowInCustomizationForm="True" VisibleIndex="5" Width="50px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان ارسال" FieldName="zamanErsal" 
                            ShowInCustomizationForm="True" VisibleIndex="6" 
            Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="زمان اتمام" FieldName="zamanEtmam" 
            ShowInCustomizationForm="True" VisibleIndex="7" Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="name_hvaz" 
            ShowInCustomizationForm="True" VisibleIndex="8" Width="100px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Name="photo" 
            ShowInCustomizationForm="True" VisibleIndex="9" Width="50px">
            <DataItemTemplate>
                <asp:Image ID="Image5" runat="server" Height="24px" Width="24px" />
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="stat_" FieldName="stat_" 
            ShowInCustomizationForm="True" VisibleIndex="10" Visible="False" 
            Width="0px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

    <SettingsPager PageSize="17">
    </SettingsPager>
    <Settings ShowFilterRow="True" ShowGroupPanel="True" />

<Settings ShowFilterRow="True"></Settings>

              <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

</dx:ASPxGridView>
         
      
        </div>
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            color: #000000;
        }
    </style>
</asp:Content>


