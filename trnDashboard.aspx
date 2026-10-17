<%@ Page Title="داشبورد آموزش" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDashboard.aspx.vb" Inherits="trnDashboard" %>
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
       

			<div class="col-md-12">
               
             
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                
                     
             			    			</div>
              
      </div>

      <div class="row">
			<div class="col-md-6">

               	<asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="از تاريخ"></asp:Label>
              <input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/>
			</div>
           <div class="col-md-6">
               <asp:CheckBoxList ID="CheckBoxListMahiat" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
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
			 	
                  <asp:Label ID="lblCnt" runat="server" CssClass="control-label" BackColor="#FFFFCC" ForeColor="Black"></asp:Label>
                   	<asp:Label ID="lblTotal" runat="server" CssClass="control-label" BackColor="#FFFF99" ForeColor="Black"></asp:Label>
            
		</div>
    </div>
    <br />
     <div class="row">
         	<div class="col-md-4">
                 </div>
			<div class="col-md-4">
                <asp:Button ID="BtnPrint" runat="server" Text="چاپ نمودار" 
                            CssClass="btn btn-primary btn-custom" />
                  </div>
                <div class="col-md-4">
                  <asp:Button ID="ibtnExitt" runat="server" class="btn btn-warning btn-sm" Text="برگشت" />
                </div>
                
         </div>
         <br />
      <div class="row">
			<div class="col-md-12">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="idd" Width="100%">

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settings showfilterrow="True" showgrouppanel="True" ShowHeaderFilterButton="True" />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail ShowDetailButtons="False" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="120000" />

                        <settings showfilterrow="True" />

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="1%">
                                <clearfilterbutton visible="True">
                                </clearfilterbutton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="سال" FieldName="sal" 
                                ShowInCustomizationForm="True" VisibleIndex="6" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ماه" FieldName="mah" 
                                VisibleIndex="5" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ماهیت" FieldName="mahiat" 
                                VisibleIndex="4" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نفرساعت" FieldName="saat" 
                                VisibleIndex="3" Width="8%">
                            </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn Width="1%" Caption="*" VisibleIndex="10" Visible="False" FieldName="idd">
    <cellstyle horizontalalign="Right">
    </cellstyle>
</dx:GridViewDataTextColumn>
                        </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                        <settingspager pagesize="20">
                        </settingspager>

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

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
