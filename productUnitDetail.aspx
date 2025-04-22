<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="productUnitDetail.aspx.vb" Inherits="productUnitDetail" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
             <asp:Panel ID="Panel12" runat="server"  >
                       
                  <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="سوابق دریافت کالای واحد">
            <div class="row">
                                       	<div class="col-md-10">
                                              
                                                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="codePersoneli" Width="100%">
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <settings showfilterrow="True" ShowHorizontalScrollBar="True" />
                                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                                    <settingsbehavior allowfocusedrow="True" />
                                                    <Settings ShowFilterRow="True" />
                                                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                                    <settingsdetail showdetailrow="True" />
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" UnboundType="Integer" VisibleIndex="0" Width="10%">
                                                            <dataitemtemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# eval("codePersoneli") %>' CommandName='<%# "select" %>' Text='<%# eval("codePersoneli") %>'></asp:LinkButton>
                                                            </dataitemtemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <cellstyle horizontalalign="Center">
                                                            </cellstyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="1" Width="10%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <cellstyle horizontalalign="Right">
                                                            </cellstyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" VisibleIndex="2" Width="10%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <cellstyle horizontalalign="Right">
                                                            </cellstyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ستادی/عملیاتی" FieldName="typeValue" VisibleIndex="3" Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="4" Width="10%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <cellstyle horizontalalign="Center">
                                                            </cellstyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="UnitsName" VisibleIndex="5" Width="15%" Caption="امور">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <cellstyle horizontalalign="Center">
                                                            </cellstyle>
                                                        </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" VisibleIndex="6" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="stat" Visible="False" VisibleIndex="6" Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="perID" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <settingspager pagesize="30">
                                                    </settingspager>
                                                    <Settings ShowFilterRow="True" showgrouppanel="True" />
                                                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                                    <settingsdetail showdetailrow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" />
                                                    <styles>
                                                        <detailrow backcolor="#CCFFCC">
                                                        </detailrow>
                                                    </styles>
                                                    <templates>
                                                        <detailrow>
                                                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="gr_" EnableCallBacks="False" KeyFieldName="perID" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect"  Width="100%">
                                                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="کالا" FieldName="kalaValue" VisibleIndex="0" Width="15%">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <cellstyle horizontalalign="Right">
                                                                        </cellstyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="اندازه" FieldName="sizeValue" ShowInCustomizationForm="True" VisibleIndex="1" Width="5%">
                                                                        <editcellstyle horizontalalign="Justify">
                                                                        </editcellstyle>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <cellstyle horizontalalign="Center">
                                                                        </cellstyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="tarix" ShowInCustomizationForm="True" VisibleIndex="2" Width="5%">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <cellstyle horizontalalign="Center">
                                                                        </cellstyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="تاریخ تحویل" FieldName="tarixThavil" VisibleIndex="3" Width="5%">
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
                 	 <div class="col-md-2">
<asp:ImageButton ID="ibtnxl" runat="server" Height="24px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="24px" />
                </div>
                      </div>
              
                       </asp:Panel> 
             
               <br />
     <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="کالاهای درخواستی">

<div class="row">
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="4" DataKeyNames="idDtl" ForeColor="#333333" GridLines="None">
                                                   <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="nam" HeaderText="نام" />
                                            <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                                            <asp:BoundField DataField="codePersoneli" HeaderText="کد پرسنلی" />
                                            <asp:BoundField DataField="value" HeaderText="نوع قراداد">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="kalaValue" HeaderText="کالا" />
                                            <asp:BoundField DataField="sizeValue" HeaderText="سایز" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ درخواست" />
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت" />
                                            <asp:ButtonField CommandName="deliver" Text="تحویل" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" BackColor="#00FFCC" />

                                            </asp:ButtonField>
                                        </Columns>
                                                   <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="Medium" BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                               </div>
                      </div>
          <br />
            </asp:Panel>
                                   
            </asp:Panel>
    <br />
     
          <br />

             <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
       
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
     <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead" Landscape="True" PaperName="TPC">
                </dx:ASPxGridViewExporter>
</asp:Content>

