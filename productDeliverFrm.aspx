<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="productDeliverFrm.aspx.vb" Inherits="productDeliverFrm" %>

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
<asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
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
			<div class="col-md-12">
                  <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="codePersoneli" Width="100%" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />

                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="70px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("codePersoneli")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="تحویل به نفر"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="70px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkButton2" runat="server" 
                    CommandArgument='<%# Eval("UnitsCode")%>' CommandName='<%# "moshahedeUnit"%>' 
                    style="color: #000000; font-weight: 700;" Text="تحویل به واحد"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" 
                            UnboundType="Integer" VisibleIndex="0" Width="10%">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("codePersoneli") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("codePersoneli") %>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="1" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                            VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" 
                            VisibleIndex="3" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ستادی/عملیاتی" FieldName="typeValue" 
                            VisibleIndex="4" Width="10%">
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="tarix" Caption="تاریخ درخواست" 
                            VisibleIndex="5" Width="10%">
    <cellstyle horizontalalign="Center">
    </cellstyle>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" ShowInCustomizationForm="True" VisibleIndex="7" Width="15%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="stat" 
                            VisibleIndex="6" Width="10%" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="perID" Visible="False" VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="30">
                    </settingspager>

<Settings ShowFilterRow="True" showgrouppanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="All"></SettingsDetail>

                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                    <templates>
                        <detailrow>
                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                    ClientIDMode="AutoID" CssClass="gr_" EnableCallBacks="False" 
                                    OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect"  
                                    OnHtmlRowPrepared="grdFarei_HtmlRowPrepared"  Width="100%" 
                                    KeyFieldName="perID">
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="کالا" FieldName="kalaValue" 
                                        VisibleIndex="0" Width="15%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="اندازه" 
                                        FieldName="sizeValue" ShowInCustomizationForm="True" 
                                        VisibleIndex="1" Width="5%">
                                        <editcellstyle horizontalalign="Justify">
                                        </editcellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ درخواست" 
                                        FieldName="tarix" VisibleIndex="2" Width="5%" 
                                        ShowInCustomizationForm="True">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ تحویل" FieldName="tarixThavil" VisibleIndex="3" Width="5%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True" VisibleIndex="4" Width="5%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <settingsbehavior allowfocusedrow="True" />
                                <settingspager pagesize="20">
                                </settingspager>
                            </dx:ASPxGridView>
                        </detailrow>
                    </templates>
                </dx:ASPxGridView>
                </div>
         
          </div>
  <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead" Landscape="True" PaperName="TPC">
                </dx:ASPxGridViewExporter>


        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

