<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnBrowsTechersFrm.aspx.vb" Inherits="trnBrowsTechersFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
     <br />

   <div class="row">

     
                        
			<div class="col-md-8">
               
                <asp:Panel ID="Panel2" runat="server" DefaultButton="ImageButton2" >
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="جستجو بر اساس همه فیلدها"></asp:Label>
          <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" placeholder="برای جستجو قسمتی از نام و نام خانوادگی یا تخصص و یا سابقه را بنویسید و Enter بزنید " ></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                    </asp:Panel>
                </div>
             			<div class="col-md-4">
 <%--<dx:ASPxComboBox ID="CmbPersonel" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains"  CssClass="btn btn-default dropdown-toggle" Width="100%">
                                            </dx:ASPxComboBox>--%>
                </div>
               </div>
    <br />
      <div class="row">
			  <div class="col-md-12">
 <asp:Label ID="lblCnt" runat="server" Font-Bold="True" style="color: #FF3300"></asp:Label>
                  </div>
           </div>
     <div class="row">
			  <div class="col-md-12">
                  <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="techID"  Width="100%" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True" showgrouppanel="True"></Settings>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowFilterBar="Visible" VerticalScrollableHeight="500" />

                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                      
                        <dx:GridViewDataTextColumn Caption="ش پرونده" FieldName="techID" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("techID")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("techID")%>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="1">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                            VisibleIndex="2">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نوع مدرس" FieldName="techTypename" 
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل اشتغال" FieldName="mahalEshtegal" VisibleIndex="4" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="سمت" FieldName="semat" VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="father" 
                            VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ش ش" FieldName="shsh" 
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="codemeli" Caption="کد ملی" 
                            VisibleIndex="7">
    <cellstyle horizontalalign="Center">
    </cellstyle>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ استخدام" FieldName="tarixStexdam" ShowInCustomizationForm="True" VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="سابقه" FieldName="sabege" VisibleIndex="9">
                        </dx:GridViewDataTextColumn>
                      
                        <dx:GridViewDataTextColumn Caption="تلفن داخلی" FieldName="daxeli" VisibleIndex="12">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobil" VisibleIndex="13">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="14">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="15">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="دانشگاه" FieldName="uni" VisibleIndex="17">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="tarixTavalod" VisibleIndex="16">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="معدل" FieldName="moadel" VisibleIndex="18">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="معرف" FieldName="moaref" VisibleIndex="19">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تدریس شرکتی" FieldName="textt" VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="12000" />

                    <settingspager pagesize="30">
                    </settingspager>

                    <Settings ShowFilterRow="True" ShowVerticalScrollBar="True" VerticalScrollableHeight="700" UseFixedTableLayout="True" />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded"></SettingsDetail>

                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                      <templates>
                          <detailrow>
                              
                              <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="dtlID" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" Width="100%">
                                  <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                  <SettingsBehavior AllowFocusedRow="True" />
                                  <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                  <SettingsBehavior AllowFocusedRow="True" />
                                  <Settings />
                                  <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                  <settingsbehavior allowfocusedrow="True" />
                                  <Settings showfilterrow="True" showgrouppanel="True" />
                                  <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                  <settingsdetail showdetailrow="True" />
                                  <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                  <Columns>
                                      <dx:GridViewDataTextColumn Caption="نام دوره" FieldName="dore" VisibleIndex="0">
                                      </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn Caption="نمره ارزیابی" FieldName="nomre" VisibleIndex="1">
                                      </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn FieldName="techID" Visible="False" VisibleIndex="2">
                                      </dx:GridViewDataTextColumn>
                                  </Columns>
                                  <SettingsBehavior AllowFocusedRow="True" />
                                  <settingspager pagesize="30">
                                  </settingspager>
                                  <settings showfilterrow="True" ShowHorizontalScrollBar="True" />
                                  <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                  <settingsdetail allowonlyonemasterrowexpanded="True" exportmode="Expanded" showdetailbuttons="False" />
                                  <styles>
                                      <detailrow backcolor="#CCFFCC">
                                      </detailrow>
                                  </styles>
                              </dx:ASPxGridView>
                              
                          </detailrow>
                      </templates>
                </dx:ASPxGridView>
                  </div>
           </div>
     <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
</asp:Content>

