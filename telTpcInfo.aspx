<%@ Page Language="VB" AutoEventWireup="false" CodeFile="telTpcInfo.aspx.vb" Inherits="telTpcInfo" %>

<!DOCTYPE html>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<link href="assets/styles/style-rtl.css" rel="stylesheet" />
<link href="assets/styles/style-rtl.min.css" rel="stylesheet" />

<link href="assets/styles/style.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <div class="row">

       </div>
        <br />
      <div class="row">
			  <div class="col-md-12">
                  <dx:ASPxGridView ID="GridKartabl" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="codePersoneli" 
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                      <Columns>
                          <dx:GridViewDataTextColumn Caption="codePersoneli" FieldName="codePersoneli" VisibleIndex="0">
                          </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="nam" FieldName="nam" VisibleIndex="1">
                          </dx:GridViewDataTextColumn>
                      </Columns>

    <SettingsBehavior AllowFocusedRow="True" />

    <SettingsPager PageSize="17">
    </SettingsPager>

    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />

              <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

</dx:ASPxGridView>
                  </div>
           </div>

    </div>
    </form>
</body>
</html>
