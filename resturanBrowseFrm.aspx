<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="resturanBrowseFrm.aspx.vb" Inherits="resturanBrowseFrm" %>

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
       

			<div class="col-md-6">
               
             
                       <asp:Button ID="ibtnExit" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                   <!-- دکمه خروجی اکسل -->

                
             			    			</div>
          <div class="col-md-6">
              <asp:Button ID="btnExportExcel" runat="server" 
    Text="📊 خروجی اکسل" 
    CssClass="btn btn-success" 
    OnClick="btnExportExcel_Click"
    Width="200px" Height="32px" />
              </div>
              
      </div>

      <div class="row">
			<div class="col-md-6">

               	<asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="از تاريخ"></asp:Label>
              <input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/>
			</div>
           <div class="col-md-6">
                       <asp:Button ID="Button1" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
               </div>
          	</div>
			    <div class="row">			
            <div class="col-md-6">
				<asp:Label ID="Label1" runat="server" CssClass="control-label" Text="تا تاريخ"></asp:Label>
                 <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly" 
                    __designer:mapid="16d"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                
			</div>

              <div class="col-md-6">
			 	<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="divicename" HeaderText="نوع">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="quantity" HeaderText="تعداد" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="tarix" HeaderText="تاریخ" />
                                                       
                                                    </Columns>
                                                    <RowStyle Font-Size="Medium" />
                                                </asp:GridView>
                  <asp:Label ID="lblCnt" runat="server" CssClass="control-label" BackColor="#FFFFCC" ForeColor="Black"></asp:Label>
                   	<asp:Label ID="lblTotal" runat="server" CssClass="control-label" BackColor="#FFFF99" ForeColor="Black"></asp:Label>
            
		</div>
    </div>
    <br />
     <div class="row">
			<div class="col-md-12">

                </div>
         </div>
         <br />
      <div class="row">
			<div class="col-md-12">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="CardId" Width="100%">

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settings showfilterrow="True" showgrouppanel="True" ShowHeaderFilterButton="True" />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail ShowDetailButtons="False" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="1%">
                                <clearfilterbutton visible="True">
                                </clearfilterbutton>
                            </dx:GridViewCommandColumn>
<dx:GridViewDataTextColumn Width="5%" Caption="وعده غذایی" VisibleIndex="8" Visible="False">
    <HeaderStyle HorizontalAlign="Center" />
    <cellstyle horizontalalign="Center">
    </cellstyle>
</dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع غذا" FieldName="divicename" 
                                ShowInCustomizationForm="True" VisibleIndex="6" Width="8%">
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="رستوران" FieldName="resturantName" 
                                VisibleIndex="5" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت" FieldName="Time" 
                                VisibleIndex="4" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" 
                                VisibleIndex="3" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" 
                                VisibleIndex="2" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام" FieldName="Name" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="8%">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="CardId" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="8%">
                             
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="*" FieldName="deviceId" 
                                VisibleIndex="10" Width="1%" Visible="False">
                            </dx:GridViewDataTextColumn>
                        </Columns>

                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="120000" />

                        <settingspager pagesize="20">
                        </settingspager>

                        <settings showfilterrow="True" />

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" />

                        <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                    </dx:ASPxGridView>

</div>
          </div>


      <div class="row">
			<div class="col-md-12">
                 <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>

</div>
          </div>
    
     <script type="text/javascript">
         var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
         var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
         //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

         //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
         //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
</asp:Content>

