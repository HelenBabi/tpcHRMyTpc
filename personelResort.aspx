<%@ Page Language="VB" AutoEventWireup="false" CodeFile="personelResort.aspx.vb" Inherits="personelResort" %>

<!DOCTYPE html>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 51px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="ResortID" Width="100%">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="ملاقات شونده" FieldName="mizban" 
                                ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="زمان خروج" FieldName="zamanEnd" 
                                VisibleIndex="3" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="زمان ورود" FieldName="zamansbat" 
                                VisibleIndex="2" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مراجعه کننده" FieldName="moraje" 
                                VisibleIndex="1" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره " FieldName="ResortID" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="50px">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" 
                                        CommandArgument='<%# eval("ResortID") %>' Font-Bold="True" 
                                        Text='<%# eval("ResortID") %>'></asp:LinkButton>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="*" FieldName="stat" 
                                ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع سپرده" FieldName="Sepordevalue" 
                                ShowInCustomizationForm="True" VisibleIndex="8" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره کارت" FieldName="codeCart" 
                                ShowInCustomizationForm="True" VisibleIndex="9" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" 
                                ShowInCustomizationForm="True" VisibleIndex="12" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="otag" 
                                ShowInCustomizationForm="True" VisibleIndex="14" Width="70px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="همراه" FieldName="notee" VisibleIndex="10" 
                                Width="70px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" ثبت کننده" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="15" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ثبت کننده خروج" FieldName="outKarbar" 
                                VisibleIndex="17" Width="50px">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />

                        <settingspager pagesize="20">
                        </settingspager>

                    <Settings ShowFilterRow="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail />

                        <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td>
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>
