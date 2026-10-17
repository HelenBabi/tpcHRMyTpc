<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="productPersonelUnit.aspx.vb" Inherits="productPersonelUnit" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                             <div class="row">
              <div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" class="label-control" Text="پرسنل "></asp:Label>
                                     <dx:ASPxComboBox ID="cmbPersonel" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" CssClass="txt_ r5" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains"  Width="100%" >
                                            </dx:ASPxComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbRuzShabKar" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
			<div class="col-md-4">
                 <asp:Label ID="Label2" runat="server" class="label-control" Text="نوع پرسنل"></asp:Label>
                      <asp:DropDownList ID="cmbRuzShabKar" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   <asp:ListItem Value="0">انتخاب</asp:ListItem>
                                   <asp:ListItem Value="1">عملیاتی</asp:ListItem>
                                   <asp:ListItem Value="2">ستادی</asp:ListItem>
                                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbRuzShabKar" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
             	<div class="col-md-4">
                     </div>
                      </div>

                 <br />
                  <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="lblKala" runat="server" class="label-control" Text=" انتخاب کالا"></asp:Label>
                      <asp:DropDownList ID="kalaCmb" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                               
                               </div>
                      <div class="col-md-4">
                 <asp:Label ID="lblSize" runat="server" class="label-control" Text=" انتخاب اندازه"></asp:Label>
                      <asp:DropDownList ID="sizeCmb" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                             
                               </div>
                         <div class="col-md-4">
                             <asp:Button ID="BtnAdd" runat="server" Text="ثبت" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a"  />
                             </div>
                      </div>
                          </asp:Panel>
    <br />
             <asp:Panel ID="Panel12" runat="server" BorderColor="Black" BorderStyle="Solid" >
             
            <div class="row">
                                       	<div class="col-md-10">
                                               
                                                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="codePersoneli" Width="100%">
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <styles>
                                                        <detailrow>
                                                        </detailrow>
                                                    </styles>
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <settings showfilterrow="True" showgrouppanel="True" />
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <settings showfilterrow="True" showhorizontalscrollbar="True" />
                                                    <settingsdetail showdetailrow="True" allowonlyonemasterrowexpanded="True" exportmode="Expanded" />
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
                                                        <dx:GridViewDataTextColumn Caption="ستادی/عملیاتی" FieldName="typeValue" VisibleIndex="4" Width="10%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <cellstyle horizontalalign="Center">
                                                            </cellstyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="4" Width="10%">
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
                                                    <settings showfilterrow="True" />
                                                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                                    <settingsdetail showdetailrow="True" />
                                                    <templates>
                                                        <detailrow>
                                                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="gr_" EnableCallBacks="False" KeyFieldName="perID" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect" OnHtmlRowPrepared="grdFarei_HtmlRowPrepared" Width="100%">
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
                                                                    <dx:GridViewDataTextColumn Caption="تاریخ تحویل" FieldName="tarixThavil" VisibleIndex="3" Width="5%" Visible="False">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <cellstyle horizontalalign="Center">
                                                                        </cellstyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True" VisibleIndex="4" Width="5%" Visible="False">
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
              <br />
     <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" Visible="false">
                           <div class="row">
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idDtl" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical">
                                                   <AlternatingRowStyle BackColor="#CCCCCC" />
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
                                            <asp:ButtonField CommandName="del" Text="ابطال" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="Medium" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                               </div>
                      </div>         
            </asp:Panel>
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
 
</asp:Content>

