<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="productBrowseFrm.aspx.vb" Inherits="productBrowseFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.1.Web, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>

<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.1.Web" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.1" namespace="DevExpress.XtraCharts" tagprefix="cc2" %>

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
<asp:ImageButton ID="ibtnxl" runat="server" Height="24px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="24px" />
                </div>
          <div class="col-md-4">
<asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
          <div class="col-md-6">
                 <asp:Chart ID="Chart1" runat="server" Height="190px" Width="320px">
                    <Series>
                        <asp:Series ChartType="Pie" Legend="Legend1" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1">
                        </asp:Legend>
                        <asp:Legend Name="Legend2">
                        </asp:Legend>
                        <asp:Legend Name="Legend3">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Name="Title1">
                        </asp:Title>
                        <asp:Title Name="Title2">
                        </asp:Title>
                        <asp:Title Name="Title3">
                        </asp:Title>
                    </Titles>
                </asp:Chart>

                <table class="nav-justified">
                    <tr>
                        <td><asp:Label ID="lblKol" runat="server" ></asp:Label></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> <asp:Label ID="lblSabt" runat="server" ></asp:Label></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> <asp:Label ID="lblNosabt" runat="server" ></asp:Label></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                
               
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

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True" showgrouppanel="True" showfilterbar="Visible" showheaderfilterbutton="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" />

                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                  <Columns>
             
                   <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="70px">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("codePersoneli")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="سوابق نفر"></asp:LinkButton>
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
                    style="color: #000000; font-weight: 700;" Text="سوابق واحد"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" 
                            UnboundType="Integer" VisibleIndex="0" Width="5%">
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
                            VisibleIndex="1" Width="5%">
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
                            VisibleIndex="3" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ستادی/عملیاتی" FieldName="typeValue" 
                            VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" ShowInCustomizationForm="True" VisibleIndex="5" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" VisibleIndex="6" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کالا" FieldName="kalaValue" ShowInCustomizationForm="True" VisibleIndex="7" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="sizeValue" Caption="اندازه" 
                            VisibleIndex="8" Width="5%">
    <HeaderStyle HorizontalAlign="Center" />
    <cellstyle horizontalalign="Center">
    </cellstyle>
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="tarix" ShowInCustomizationForm="True" Width="5%" VisibleIndex="10" Caption="تاریخ درخواست">
    <HeaderStyle HorizontalAlign="Center" />
    <cellstyle horizontalalign="Center">
    </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="tarixThavil" VisibleIndex="11" Caption="تاریخ تحویل" Width="5%" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="stat" 
                            VisibleIndex="12" Width="10%" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="perID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                        </dx:GridViewDataTextColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                </dx:ASPxGridView>
                </div>
         
          </div>
    <br />
     <div class="row">
			<div class="col-md-6">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="kalaValue" Width="100%" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True" showgrouppanel="True" showfilterbar="Visible" showheaderfilterbutton="True"></Settings>

                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                  <Columns>
             
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="15%" Caption="کالا" FieldName="kalaValue" UnboundType="Integer">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Right">
            </CellStyle>
        </dx:GridViewDataTextColumn>
             
                   <dx:GridViewDataTextColumn VisibleIndex="1" 
            Width="15%" Caption="اندازه" FieldName="sizeValue">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="100">
                    </settingspager>

                    <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False"></SettingsDetail>

                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                </dx:ASPxGridView>
                </div>
         <div class="col-md-6">

             <asp:ImageButton ID="ibtnxl0" runat="server" Height="24px" ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" Width="24px" />

             </div>
         </div>
    <div class="row">
			<div class="col-md-4">
                <asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="از مورخ" Visible="False"></asp:Label>
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
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="تا مورخ" Visible="False"></asp:Label>
                <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly"  
                    __designer:mapid="16d"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" Visible="False" />
                </div>
          <div class="col-md-4">
              <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="نمایش همه رکوردها" Visible="False" />

                </div>
         <div class="col-md-4">
              <asp:Label ID="lblCnt" runat="server" Text="" Visible="False"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" style="color: #FF6600" Visible="False"></asp:Label>
                </div>
         </div>
 <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
                    GridViewID="ASPxGridView1">
                </dx:ASPxGridViewExporter>
        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

