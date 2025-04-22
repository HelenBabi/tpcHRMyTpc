<%@ Page Title="" Language="VB" MasterPageFile="~/home.master" AutoEventWireup="false" CodeFile="FrmKartabl.aspx.vb" Inherits="FrmKartabl" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <dx:ASPxGridView ID="ASPxGrvHead" runat="server" Width="710px" 
                    AutoGenerateColumns="False" ClientIDMode="AutoID" 
                    KeyFieldName="KartablCode"  
                    EnableCallBacks="False" 
                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue">
        <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
        <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
        </ClientSideEvents>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="پلاک" 
                            FieldName="shahrbani2" VisibleIndex="1">
                <PropertiesTextEdit>
                    <Style HorizontalAlign="Left" VerticalAlign="Middle">
                    </Style>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="پلاک" FieldName="shahrbani1" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                <PropertiesTextEdit>
                    <Style HorizontalAlign="Left">
                    </Style>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="کد" FieldName="KartablCode" 
                            ShowInCustomizationForm="True" VisibleIndex="4">
                <DataItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("KartablCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("KartablCode") %>'></asp:LinkButton>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="رنگ هرم" FieldName="HeramColorName" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="نوع ماشین" FieldName="KindCarName" 
                VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="کیلومتر" FieldName="VisitKiloMetr" 
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="VisitDate" 
                VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="ساعت" FieldName="VisitTime" 
                VisibleIndex="8">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="هرم" FieldName="VisitHeramNo" 
                VisibleIndex="9">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" />
        <SettingsBehavior AllowFocusedRow="True">
        </SettingsBehavior>
        <Settings ShowFilterRow="True" />
        <SettingsPager>
            <FirstPageButton Text="اولین صفحه" Visible="True">
            </FirstPageButton>
            <LastPageButton Text="آخرین صفحه" Visible="True">
            </LastPageButton>
            <NextPageButton Text="بعدی">
            </NextPageButton>
            <PrevPageButton Text="قبلی">
            </PrevPageButton>
        </SettingsPager>

<Settings ShowFilterRow="True"></Settings>

        <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
            <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
            </LoadingPanelOnStatusBar>
            <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
            </LoadingPanel>
        </Images>
        <ImagesFilterControl>
            <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
            </LoadingPanel>
        </ImagesFilterControl>
        <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                        CssPostfix="Office2003Blue">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
            <LoadingPanel ImageSpacing="10px">
            </LoadingPanel>
        </Styles>
        <StylesEditors>
            <ProgressBar Height="25px">
            </ProgressBar>
        </StylesEditors>
    </dx:ASPxGridView>
</asp:Content>

