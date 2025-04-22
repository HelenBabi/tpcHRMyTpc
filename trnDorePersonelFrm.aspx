<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDorePersonelFrm.aspx.vb" Inherits="trnDorePersonelFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
       <div class="row">
			  <div class="col-md-12">
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
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
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
          <div class="col-md-4">
              <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="نمایش همه رکوردها" Visible="False" />

                </div>
         <div class="col-md-4">
              <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTotal" runat="server" style="color: #FF6600"></asp:Label>
                </div>
         </div>
      <div class="row">
			  <div class="col-md-12">
 <asp:Label ID="lblCnt" runat="server" Font-Bold="True" style="color: #FF3300"></asp:Label>
                  </div>
           </div>
     <div class="row">
			  <div class="col-md-12">
                  <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="codePersoneli"  Width="100%" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<Settings ShowFilterRow="True" showgrouppanel="True"></Settings>

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
            Width="5%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("codePersoneli")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="مشاهده"></asp:LinkButton>
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
                            VisibleIndex="3" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" 
                            VisibleIndex="4" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="reshte" Caption="رشته" 
                            VisibleIndex="5" Width="5%">
    <HeaderStyle HorizontalAlign="Center" />
    <cellstyle horizontalalign="Center">
    </cellstyle>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="پست سازمانی" FieldName="postname" VisibleIndex="8" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="هماهنگی اجرا" FieldName="hamahangiEjra" 
                            VisibleIndex="6" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" VisibleIndex="7" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra" VisibleIndex="9" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" VisibleIndex="10" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="tarixPayan" VisibleIndex="11" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="عنوان" FieldName="onvan" VisibleIndex="12" Width="2%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ" VisibleIndex="13" Width="1%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساعت" FieldName="saat" VisibleIndex="14" Width="1%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="دوره های گذرانده" FieldName="dorehayeGozrande" VisibleIndex="15" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نحوه ثبت" FieldName="typeName" VisibleIndex="16" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" VisibleIndex="17" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" VisibleIndex="18" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="30">
                    </settingspager>

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

