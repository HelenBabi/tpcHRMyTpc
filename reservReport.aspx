<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="reservReport.aspx.vb" Inherits="reservReport" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js-persian-cal.min.js" type="text/javascript"></script>
    <link href="js-persian-cal.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function openNewWin(inp1, inp2) {
            var div_ = document.getElementById("pcal1");
            div_.setAttribute('value', inp1);
            var div_ = document.getElementById("pcal2");
            div_.setAttribute('value', inp2);
            //            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');

            //            div_.focus();

        }

</script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
			<div class="col-md-2">

                </div>
          <div class="col-md-2">
<asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                </div>
          <div class="col-md-2">
<asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">
                     <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                </div>

          </div>

     <div class="row">
			<div class="col-md-4">
  <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" >
                 <asp:ListItem Value="0">گزارش کلی</asp:ListItem>
                 <asp:ListItem Value="1">استخر آقایان</asp:ListItem>
                 <asp:ListItem Value="2">استخر بانوان</asp:ListItem>
             </asp:RadioButtonList>
                </div>
         <div class="col-md-4">
             <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="tarix" HeaderText="تاریخ">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="locationValue" HeaderText="محل">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="menu" HeaderText="تایم" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tedad" HeaderText="تعداد">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                    </Columns>
                                                    <RowStyle Font-Size="Medium" />
                                                </asp:GridView>
                </div>
          <div class="col-md-4">

                </div>
         </div>

    <div class="row">
			<div class="col-md-4">
                <asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="از مورخ"></asp:Label>
<input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/>
                </div>
          <div class="col-md-4">

                </div>
         <div class="col-md-4">
           
                </div>
         </div>
     <div class="row">
			<div class="col-md-4">
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="تا مورخ"></asp:Label>
                <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly" 
                    __designer:mapid="16d"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                </div>
          <div class="col-md-8">
 <asp:Label ID="lblTotal" runat="server" style="color: #FF6600"></asp:Label>
             
  <asp:Label ID="lblCnt" runat="server" Text=""></asp:Label>
                </div>
       
         </div>
     <div class="row">
         	<div class="col-md-12">
                 <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="reservID" Width="50%">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" />

                        <settings showfilterrow="True" showgrouppanel="True" />
                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" ExportMode="All" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                        <Columns>
                            <dx:GridViewDataTextColumn Caption="زمان" FieldName="menu" VisibleIndex="8" Width="30%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="محل" FieldName="locationValue" VisibleIndex="7" Width="5%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نسبت" FieldName="nesbat" VisibleIndex="6" Width="5%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="همراه" FieldName="hamrah" VisibleIndex="5" Width="5%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت رزرو" FieldName="saat" VisibleIndex="4" Width="3%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="tarix" Caption="تاریخ رزرو" VisibleIndex="3" Width="11%">
    <HeaderStyle HorizontalAlign="Center" />
</dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="11%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام و نام خانوادگی کارمند" FieldName="person" ShowInCustomizationForm="True" VisibleIndex="1" Width="25%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="-" FieldName="reservID" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="5%" Visible="False">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" 
                                        CommandArgument='<%# Eval("reservID")%>' Font-Bold="True" 
                                        Text='انتخاب'></asp:LinkButton>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="White" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                        <settingspager pagesize="20">
                        </settingspager>

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True" showdetailbuttons="False"></SettingsDetail>

                        <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                    </dx:ASPxGridView>
                 </div>
          </div>
     <asp:Label ID="Label2" runat="server" ></asp:Label>
                 <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>    
       

        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

  
</asp:Content>

