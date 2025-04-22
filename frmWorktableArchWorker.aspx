<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmWorktableArchWorker.aspx.vb" Inherits="frmWorktableArchWorker" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            color: #666666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div_headp">
            <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" ImageUrl="~/Icons/Exit.png"
                ToolTip="خروج" Width="48px" />
            <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" ImageUrl="~/Icons/Print.png"
                ToolTip="چاپ" Width="48px" ValidationGroup="b" />
            <asp:ImageButton ID="ibtnDeactived" runat="server" Height="48px" ImageUrl="~/Icons/reload.png"
                ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
            <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" ImageUrl="~/Icons/history.png"
                ToolTip="تاریخچه تغییرات" Width="48px" ValidationGroup="b" />
        </div>
<table class="style1">
        <tr>
            <td>
          
                        

                  <asp:Label ID="lblCnt0" runat="server" 
                      style="font-weight: 700; " CssClass="style2"></asp:Label>
          
                        

                  <asp:Label ID="lblCntrch" runat="server" 
                      style="font-weight: 700; " CssClass="style2"></asp:Label>
          
                     

   
            </td>
        </tr>
    </table>
     
                 <div class="div_grid">
          <dx:ASPxGridView ID="GridKartabl" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="erja_id" 
                     OnHtmlDataCellPrepared="GridKartabl_HtmlDataCellPrepared" 
                    EnableCallBacks="False" ClientIDMode="AutoID">
    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
    </ClientSideEvents>
    <Columns>
        <dx:GridViewDataTextColumn Caption="شماره درخواست" FieldName="erja_id" 
                            VisibleIndex="0" ShowInCustomizationForm="True" 
            Width="40px">
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
                    style="font-weight: 700; color: #000000" CommandName='<%# "mosh" %>'></asp:LinkButton>
            </dataitemtemplate>
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
        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="ferestande" 
            ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName_" 
            ShowInCustomizationForm="True" VisibleIndex="4" Width="120px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="telfon" 
            ShowInCustomizationForm="True" VisibleIndex="5" Width="50px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="نام کامپیوتر" FieldName="ComputerName" VisibleIndex="6" 
            Width="120px">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn 
            VisibleIndex="7" Width="50px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnArchive" runat="server" 
                    CommandArgument='<%# eval("erja_id") %>' CommandName='<%# "archiveee" %>' 
                    style="color: #333333" Text="برگردان به کارتابل؟" 
                    
                    ToolTip="اگر آرشیو را کلیک کنید درخواست مربوطه به آرشیو کارتابل منتقل می شود"></asp:LinkButton>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="8" 
            Caption="وضعیت" FieldName="name_hvaz">
</dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Name="photo" VisibleIndex="9">
            <DataItemTemplate>
                <asp:Image ID="Image5" runat="server" Height="24px" Width="24px" />
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="stat_" FieldName="stat_" Visible="False" 
            VisibleIndex="10">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True" />
    <SettingsBehavior AllowFocusedRow="True">
    </SettingsBehavior>
    <SettingsPager PageSize="17">
    </SettingsPager>
    <Settings ShowFilterRow="True" />
    <Settings ShowFilterRow="True">
    </Settings>
</dx:ASPxGridView>
         

                   </div>     
                   

</asp:Content>

