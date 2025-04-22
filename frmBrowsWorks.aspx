<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmBrowsWorks.aspx.vb" Inherits="frmBrowsWorks" %>

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
              <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="نمایش همه رکوردها" />

                </div>
         <div class="col-md-4">
              <asp:Label ID="lblCnt" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTotal" runat="server" style="color: #FF6600"></asp:Label>
                </div>
         </div>
     <div class="row">
			<div class="col-md-12">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="idReg" Width="100%">

<Settings ShowFilterRow="True" ShowGroupPanel="True" showhorizontalscrollbar="True"></Settings>

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />

                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="120000" />

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />

                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                        <settings showfilterrow="True" showgrouppanel="True" ShowHeaderFilterButton="True" ShowHorizontalScrollBar="True" ShowFilterBar="Visible"  />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

                    <settingsdetail showdetailrow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                        <Columns>
                              <dx:GridViewDataTextColumn Caption="نتیجه مصاحبه" FieldName="natije" VisibleIndex="29">
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="تاریخ مصاحبه" FieldName="tarixMosahebe" VisibleIndex="30">
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="tozihat" VisibleIndex="31">
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="نحوه ثبت نام" FieldName="typeVlaue" 
                                ShowInCustomizationForm="True" VisibleIndex="27" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                           
                                                 <dx:GridViewDataTextColumn Caption="کد پستی" FieldName="codePosti" 
                                ShowInCustomizationForm="True" VisibleIndex="27" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مهارتها" FieldName="maharatha" 
                                ShowInCustomizationForm="True" VisibleIndex="26">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنلی صنعت نفت" FieldName="codePerson" 
                                ShowInCustomizationForm="True" VisibleIndex="25" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="نسبت صنعت نفت" FieldName="nesbat" 
                                ShowInCustomizationForm="True" VisibleIndex="24" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="جز وابستگان صنعت نفت" FieldName="sanatNaft" 
                                ShowInCustomizationForm="True" VisibleIndex="23" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع معافیت" FieldName="moafitType" 
                                ShowInCustomizationForm="True" VisibleIndex="22" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="وضعیت ایثارگری" FieldName="shohada" 
                                ShowInCustomizationForm="True" VisibleIndex="21" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="regDate" 
                                ShowInCustomizationForm="True" VisibleIndex="20" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="آدرس" FieldName="address" 
                                ShowInCustomizationForm="True" VisibleIndex="19"  Visible="False">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ایمیل" FieldName="mail" 
                                ShowInCustomizationForm="True" VisibleIndex="18" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobil" 
                                ShowInCustomizationForm="True" VisibleIndex="17" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تلفن" FieldName="tell" 
                                ShowInCustomizationForm="True" VisibleIndex="16" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سال اخذ مدرک" FieldName="salMadrak" 
                                ShowInCustomizationForm="True" VisibleIndex="15" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="معدل" FieldName="moadel" 
                                ShowInCustomizationForm="True" VisibleIndex="14" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="دانشگاه محل اخذ مدرک" FieldName="madrakuni" 
                                ShowInCustomizationForm="True" VisibleIndex="13" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="گرایش تحصیلی" FieldName="gerayeshTahsili" 
                                ShowInCustomizationForm="True" VisibleIndex="13" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="رشته تحصیلی" FieldName="reshte" 
                                ShowInCustomizationForm="True" VisibleIndex="12" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="آخرین مدرک تحصیلی" FieldName="madrak" 
                                ShowInCustomizationForm="True" VisibleIndex="11" Width="15%">
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="وضعیت نظام وظیفه" FieldName="xedmat" 
                                ShowInCustomizationForm="True" VisibleIndex="10" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="وضعیت تاهل" FieldName="tahol" 
                                VisibleIndex="9" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="جنسیت" FieldName="jens" 
                                ShowInCustomizationForm="True" VisibleIndex="8" >
                                                               <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="tarixTavalod" 
                                VisibleIndex="7" >
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تابعیت" FieldName="tabeiat" 
                                VisibleIndex="6" >
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="shsh" 
                                VisibleIndex="5" >
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" 
                                ShowInCustomizationForm="True" VisibleIndex="4" >
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="fatherName" 
                                ShowInCustomizationForm="True" VisibleIndex="3" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="lname" 
                                VisibleIndex="2">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                                ShowInCustomizationForm="True" VisibleIndex="1" >
                               
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره پرونده" FieldName="idReg" 
                                ShowInCustomizationForm="True" VisibleIndex="0" >
                                                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" 
                                        CommandArgument='<%# Eval("idReg")%>' Font-Bold="True" 
                                        Text='<%# Eval("idReg")%>' CommandName='<%# "moshahede"%>'></asp:LinkButton>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                             
                              <dx:GridViewDataTextColumn Caption="معرف" FieldName="moaref" VisibleIndex="28">
                              </dx:GridViewDataTextColumn>
                             
                        </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                        <settingspager pagesize="15">
                        </settingspager>

                        <settings showfilterrow="True" />

                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

<SettingsDetail IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True" showdetailbuttons="False"></SettingsDetail>

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

