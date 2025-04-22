<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmChart.aspx.vb" Inherits="frmChart" %>

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
    <div class="div_page" dir="rtl">
<div class="div_headpage" dir="rtl">
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" 
                        Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
</div>
 
        <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:Label ID="lblDateS" runat="server" Font-Bold="True" style="color: #FF3300" 
                    Visible="False"></asp:Label>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                    </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                از تاريخ</div>
            <div class="div_txt">
                <input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/></div>
                    <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                    </div>
            <div class="div_txt">
            
                <asp:Label ID="lblDateE" runat="server" Font-Bold="True" style="color: #FF3300" 
                    Visible="False" Width="5px"></asp:Label>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تا تاريخ</div>
            <div class="div_txt">
                <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly" 
                    __designer:mapid="16d"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_grid">
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        ClientIDMode="AutoID" Width="930px">
                        <TabPages>
                            <dx:TabPage Text="گزارش درقالب نمودار">
                                <ContentCollection>
                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" 
                                            AppearanceName="In A Fog" ClientIDMode="AutoID" Height="450px" 
                                            Width="928px" PaletteName="Palette 1" SeriesSorting="Descending">
                                            <diagramserializable>
                                                <cc1:XYDiagram>
                                                    <axisx visibleinpanesserializable="-1"><label angle="-90" /><range sidemarginsenabled="True" /></axisx>
                                                    <axisy visibleinpanesserializable="-1"><range sidemarginsenabled="True" /></axisy>
                                                    <panes><cc1:XYDiagramPane Name="Pane 1" PaneID="0" Visible="False"></cc1:XYDiagramPane></panes>
                                                </cc1:XYDiagram>
                                            </diagramserializable>
                                            <fillstyle>
                                                <optionsserializable><cc1:SolidFillOptions /></optionsserializable>
                                            </fillstyle>
                                            <legend antialiasing="True">
<Border Visible="False"></Border>

<Shadow Visible="True"></Shadow>
                                            </legend>
                                            <seriesserializable>
                                                <cc1:Series ArgumentDataMember="BuildingName" Name="سرويس در كارگاه" 
                                                    ValueDataMembersSerializable="ss">
                                                    <viewserializable><cc1:StackedBarSeriesView Color="250, 192, 192, 255" Transparency="5"></cc1:StackedBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:StackedBarSeriesLabel Visible="False"><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                                <cc1:Series ArgumentDataMember="BuildingName" Name="ريموت" 
                                                    ValueDataMembersSerializable="rr">
                                                    <viewserializable><cc1:StackedBarSeriesView></cc1:StackedBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:StackedBarSeriesLabel Visible="False"><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                                <cc1:Series ArgumentDataMember="BuildingName" Name="سرویس در محل" 
                                                    ValueDataMembersSerializable="mahal">
                                                    <viewserializable><cc1:StackedBarSeriesView></cc1:StackedBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:StackedBarSeriesLabel Visible="False"><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                                <cc1:Series ArgumentDataMember="BuildingName" Name="سرویس دوره ای" 
                                                    ValueDataMembersSerializable="dorei">
                                                    <viewserializable><cc1:StackedBarSeriesView></cc1:StackedBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:StackedBarSeriesLabel Visible="False"><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                                <cc1:Series ArgumentDataMember="BuildingName" Name="شبكه" 
                                                    ValueDataMembersSerializable="tshbk">
                                                    <viewserializable><cc1:StackedBarSeriesView></cc1:StackedBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:StackedBarSeriesLabel Visible="False"><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                                <cc1:Series ArgumentDataMember="BuildingName" Name="Passive" 
                                                    ValueDataMembersSerializable="pass">
                                                    <viewserializable><cc1:StackedBarSeriesView Color="0, 192, 192"></cc1:StackedBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:StackedBarSeriesLabel><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                                <cc1:Series ArgumentDataMember="saxteman" Name="تعداد سيستم" 
                                                    ValueDataMembersSerializable="tedadsystem" Visible="False">
                                                    <viewserializable><cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView></viewserializable>
                                                    <labelserializable><cc1:SideBySideBarSeriesLabel LineVisible="True"><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:SideBySideBarSeriesLabel></labelserializable>
                                                    <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                    <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                                </cc1:Series>
                                            </seriesserializable>
                                            <seriestemplate seriespointssorting="Descending" 
                                                seriespointssortingkey="Value_1">
                                                <viewserializable><cc1:StackedBarSeriesView></cc1:StackedBarSeriesView></viewserializable>
                                                <labelserializable><cc1:StackedBarSeriesLabel><fillstyle><optionsserializable><cc1:SolidFillOptions /></optionsserializable></fillstyle></cc1:StackedBarSeriesLabel></labelserializable>
                                                <pointoptionsserializable><cc1:PointOptions></cc1:PointOptions></pointoptionsserializable>
                                                <legendpointoptionsserializable><cc1:PointOptions></cc1:PointOptions></legendpointoptionsserializable>
                                            </seriestemplate>
                                            <titles>
                                                <cc1:ChartTitle Font="Tahoma, 10pt" 
                                                    Text="نمودار سرويس هاي انجام شده به تفكيك ساختمان ها" TextColor="Teal" />
                                            </titles>
                                            <palettewrappers>
                                                <dxchartsui:PaletteWrapper Name="Palette 1" ScaleMode="Repeat">
                                                    <palette><cc1:PaletteEntry Color="132, 189, 253" Color2="132, 189, 253" /><cc1:PaletteEntry Color="163, 224, 160" Color2="163, 224, 160" /><cc1:PaletteEntry Color="217, 168, 205" Color2="217, 168, 205" /><cc1:PaletteEntry Color="247, 253, 130" Color2="247, 253, 130" /><cc1:PaletteEntry Color="233, 128, 22" Color2="233, 128, 22" /><cc1:PaletteEntry Color="Silver" Color2="Silver" /></palette>
                                                </dxchartsui:PaletteWrapper>
                                            </palettewrappers>
                                        </dxchartsui:WebChartControl>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="گزارش در قالب جدول">
                                <ContentCollection>
                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="grdServic" runat="server" AutoGenerateColumns="False" 
                                            ClientIDMode="AutoID" KeyFieldName="BuildingCode" Width="400px">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="نام ساختمان" FieldName="BuildingName" 
                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad" 
                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsPager PageSize="20">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" />
                                            <SettingsDetail IsDetailGrid="True" ShowDetailRow="True" 
                                                AllowOnlyOneMasterRowExpanded="True" />

<Settings ShowFilterRow="True"></Settings>

<SettingsDetail ShowDetailRow="True" IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                                        KeyFieldName="WorkTypeCode" 
                                    OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" ClientIDMode="AutoID" Width="400px">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="نوع سرويس" FieldName="WorkTypeName" 
                                                                VisibleIndex="0">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="تعداد" FieldName="tedad2" VisibleIndex="1">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <cellstyle horizontalalign="Center">
                                                                </cellstyle>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <settingspager pagesize="20" visible="False">
                                                        </settingspager>
                                                        <settings showfilterrow="True" />
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="تعداد ثبت نام">
                                <contentcollection>
                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    </dx:ContentControl>
                                </contentcollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                    <br />
    	<script type="text/javascript">
    	    var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
    	    //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

    	    //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
    	    //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
        </div>
            
    
</div>
</asp:Content>

