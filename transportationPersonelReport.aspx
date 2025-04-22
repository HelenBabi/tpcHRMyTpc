<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="transportationPersonelReport.aspx.vb" Inherits="transportationPersonelReport" %>

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
			<div class="col-md-6">
               
                <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
              
                
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

                
               
                  
			</div>
              
      </div>

     <div class="row">
     <div class="col-md-3">
				<asp:Label ID="Label8" runat="server" CssClass="control-label" Text="نوع سرویس "></asp:Label>
                          <asp:DropDownList ID="CmbAnbar" runat="server" AutoPostBack="True" 
                    CssClass="btn btn-default dropdown-toggle" >
                </asp:DropDownList>
			</div>
           <div class="col-md-3">
                 </div>
         <div class="col-md-3">

             
			</div>
          <div class="col-md-3">
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="نمایش همه رکوردها" Visible="False" />

             
			</div>
         </div>
    <br />

    
     <div class="row">
     <div class="col-md-12">
         <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="masirID" Width="100%">

                        <styles>
                            <detailrow >
                            </detailrow>
                        </styles>

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True" ShowGroupPanel="True" showheaderfilterbutton="True" showhorizontalscrollbar="True" showtitlepanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True" exportmode="Expanded"></SettingsDetail>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <Columns>
<dx:GridViewDataTextColumn FieldName="cod" Width="3%" Caption="شماره " VisibleIndex="0">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مسیر" FieldName="masir" 
                                VisibleIndex="1" Width="50%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع" FieldName="citySiteTypeValue" 
                                ShowInCustomizationForm="True" VisibleIndex="2" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="روزکار/نوبتکار" FieldName="typevalue" VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" 
                                VisibleIndex="4" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="carTypeName" VisibleIndex="5" Caption="نوع ماشین">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="capasity" VisibleIndex="6" Caption="ظرفیت">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="masirID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="citySiteTypeID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                            </dx:GridViewDataTextColumn>
                        </Columns>

                        <settingsbehavior allowfocusedrow="True" />

                        <settingspager pagesize="25">
                        </settingspager>

                        <settings showfilterrow="True" showgrouppanel="True" />
                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" ExportMode="Expanded" />

                        <templates>
                            <detailrow>
                                <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" EnableCallBacks="False" 
                                    OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" 
                                    KeyFieldName="idDtl" ClientIDMode="AutoID">

      
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />

      
                                <settingsbehavior allowfocusedrow="True" />

      
                                    <settingsbehavior allowfocusedrow="True" />

      
                                    <settingsbehavior allowfocusedrow="True" />

      
                                    <settings gridlines="Horizontal" showgroupbuttons="False" showfilterrow="True" />

      
                                    <settings gridlines="Horizontal" showgroupbuttons="False" showfilterrow="True" />

      
                                    <settings gridlines="Horizontal" 
                                        showgroupbuttons="False" showfilterrow="True" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <SettingsBehavior AllowFocusedRow="True" />

      
                                    <settings showfilterrow="True" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                    <settings showfilterrow="True" />

      
                                <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                    <settings gridlines="Horizontal" showgroupbuttons="False" />

      
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />

      
                                    <settings gridlines="Horizontal" 
                                        showgroupbuttons="False" showfilterrow="True" />

      
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />

      
                                    <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />

      
                                    <settings gridlines="Horizontal" 
                                        showgroupbuttons="False" ShowFooter="True" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    
                                    <settingsbehavior allowfocusedrow="True" />

      
                                    <settings gridlines="Horizontal" 
                                        showgroupbuttons="False" showfilterrow="True" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settings showfilterrow="True" gridlines="Horizontal" showgroupbuttons="False" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />
                                <Settings ShowFilterRow="True" gridlines="Horizontal" showgroupbuttons="False" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />

      
                                <settings gridlines="Horizontal" showgroupbuttons="False" showfilterrow="True" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />

      
                                <settings showfilterrow="True" gridlines="Horizontal" showgroupbuttons="False" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <settingsbehavior allowfocusedrow="True" />

      
                                <settings gridlines="Horizontal" 
                                    showgroupbuttons="False" ShowFilterRow="True" />

      
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="idDtl" 
                                            VisibleIndex="0" Caption="کد" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="pers" ShowInCustomizationForm="True" VisibleIndex="1" Caption="پرسنل">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="تاریخ ثبت" FieldName="tarix" 
                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn 
                                            FieldName="saat" VisibleIndex="3" 
                                            Caption="ساعت ثبت">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="typevalue" VisibleIndex="4">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settingspager pagesize="25" alwaysshowpager="True" mode="ShowAllRecords">
                                    </settingspager>

      
                                    <settings showfilterrow="True" gridlines="Horizontal" showgroupbuttons="False" />

      
                                </dx:ASPxGridView>
                            </detailrow>
                        </templates>
                    </dx:ASPxGridView>
 </div>
        
          </div>

     <div class="row">
     <div class="col-md-12">
         <dxchartsui:WebChartControl runat="server" Width="1500px" Height="800px" PaletteName="Palette 1" AppearanceName="In A Fog" SeriesSorting="Descending" ClientIDMode="AutoID" ID="WebChartControl1"><DiagramSerializable>
<cc2:XYDiagram>
<AxisX VisibleInPanesSerializable="-1">
<Label Angle="-90"></Label>

<Range SideMarginsEnabled="True"></Range>
</AxisX>

<AxisY VisibleInPanesSerializable="-1">
<Range SideMarginsEnabled="True"></Range>
</AxisY>
<Panes>
<cc2:XYDiagramPane PaneID="0" Visible="False" Name="Pane 1"></cc2:XYDiagramPane>
</Panes>
</cc2:XYDiagram>
</DiagramSerializable>

<FillStyle><OptionsSerializable>
<cc2:SolidFillOptions></cc2:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

<Legend Antialiasing="True">
<Border Visible="False"></Border>

<Shadow Visible="True"></Shadow>
</Legend>
<SeriesSerializable>
    <cc2:Series ArgumentDataMember="masir" Name="مسیر سرویس" ValueDataMembersSerializable="ss">
        <viewserializable>
            <cc2:StackedBarSeriesView>
            </cc2:StackedBarSeriesView>
        </viewserializable>
        <labelserializable>
            <cc2:StackedBarSeriesLabel>
                <fillstyle>
                    <optionsserializable>
                        <cc2:SolidFillOptions />
                    </optionsserializable>
                </fillstyle>
            </cc2:StackedBarSeriesLabel>
        </labelserializable>
        <pointoptionsserializable>
            <cc2:PointOptions>
            </cc2:PointOptions>
        </pointoptionsserializable>
        <legendpointoptionsserializable>
            <cc2:PointOptions>
            </cc2:PointOptions>
        </legendpointoptionsserializable>
    </cc2:Series>
</SeriesSerializable>

<SeriesTemplate SeriesPointsSorting="Descending" SeriesPointsSortingKey="Value_1"><ViewSerializable>
<cc2:StackedBarSeriesView></cc2:StackedBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<cc2:StackedBarSeriesLabel>
<FillStyle><OptionsSerializable>
<cc2:SolidFillOptions></cc2:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
</cc2:StackedBarSeriesLabel>
</LabelSerializable>
<PointOptionsSerializable>
<cc2:PointOptions></cc2:PointOptions>
</PointOptionsSerializable>
<LegendPointOptionsSerializable>
<cc2:PointOptions></cc2:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>
<Titles>
<cc2:ChartTitle Text="نمودار سرویس ایاب ذهاب" Font="Tahoma, 10pt" TextColor="Teal"></cc2:ChartTitle>
</Titles>
<PaletteWrappers>
<dxchartsui:PaletteWrapper Name="Palette 1" ScaleMode="Repeat"><Palette>
<cc2:PaletteEntry Color="132, 189, 253" Color2="132, 189, 253"></cc2:PaletteEntry>
<cc2:PaletteEntry Color="163, 224, 160" Color2="163, 224, 160"></cc2:PaletteEntry>
<cc2:PaletteEntry Color="217, 168, 205" Color2="217, 168, 205"></cc2:PaletteEntry>
<cc2:PaletteEntry Color="247, 253, 130" Color2="247, 253, 130"></cc2:PaletteEntry>
<cc2:PaletteEntry Color="233, 128, 22" Color2="233, 128, 22"></cc2:PaletteEntry>
<cc2:PaletteEntry Color="Silver" Color2="Silver"></cc2:PaletteEntry>
</Palette>
</dxchartsui:PaletteWrapper>
</PaletteWrappers>
</dxchartsui:WebChartControl>

 </div>
          </div>
    <div class="row">
     <div class="col-md-12">
         
         </div>
        </div>
  <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
     
</asp:Content>

