<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="bootTicketSubmit.aspx.vb" Inherits="bootTicketSubmit" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

   <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
       <div class="row">
			<div class="col-md-12">
                               <h4>لطفا کلید + را کلیک کنید تا قطعات سخت افزاری سیستم شما مشاهده شود</h4>
                                              <h4><< خواهشمند است در صورت عدم انطباق مشخصات تجهیزات( شماره اموال کیبورد، مانیتور ، پرینتر) ، گزینه مغایرت را کلیک نموده و توضیحات مربوطه را جهت بررسی ذکر نمایید.>></h4>
                <br />
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="SystemCode" Width="100%" 
                    ClientIDMode="AutoID">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True" autoexpandallgroups="True"></SettingsBehavior>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowGroupButtons="False" ShowHeaderFilterBlankItems="False" />

                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                    <settingsdetail showdetailrow="True" ExportMode="Expanded" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="5%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("SystemCode")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="انتخاب"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center" backcolor="#00CC66">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                                               <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تحویل گیرنده سیستم" FieldName="nam" 
                            VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="BuildingName" ShowInCustomizationForm="True" Caption="ساختمان" 
                            VisibleIndex="3" Width="10%">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" 
                            VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="اتاق" FieldName="SystemRoom" 
                            VisibleIndex="5" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="SystemPhone" 
                            VisibleIndex="6" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نوع سیستم" FieldName="SystemName" 
                            VisibleIndex="7" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="TP" FieldName="SystemTP" VisibleIndex="8" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="10" 
            Width="5%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" 
                    CommandArgument='<%# Eval("SystemCode")%>' CommandName='<%# "mogh"%>' 
                    style="color: #000000; font-weight: 700;" Text="مغایرت"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center" backcolor="#FF3300">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="30" visible="False">
                    </settingspager>

                    <Settings />

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" exportmode="All"></SettingsDetail>

                                       <templates>
                        <detailrow>
                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" 
                                ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="SystemCode" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect"
                               OnHtmlRowPrepared="grdFarei_HtmlRowPrepared" Width="100%">
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Visible="False">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="*" FieldName="SystemHDtlStatus" 
                                        VisibleIndex="0" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="نام قطعه" 
                                        FieldName="BaseSystemHDtlNmae" ShowInCustomizationForm="True" 
                                        VisibleIndex="1" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="مدل" 
                                        FieldName="BaseModelName" VisibleIndex="2" Width="20%" 
                                        ShowInCustomizationForm="True">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="اموال قطعه" 
                                        FieldName="AmvalCode" VisibleIndex="3" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="TP شماره" FieldName="SystemHDtlTP" 
                                        VisibleIndex="4" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="سريال" FieldName="SystemHDtlSerial" 
                                        VisibleIndex="5" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption=" تاریخ نصب قطعه" FieldName="SystemHDtlDateIn" 
                                        VisibleIndex="6" Width="10%">
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ حذف قطعه" FieldName="SystemHDtlDateOut" 
                                        VisibleIndex="7" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاریخ گارانتی" FieldName="SystemHDtlGarantyDate" 
                                        ShowInCustomizationForm="True" VisibleIndex="8" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                 
                                    <dx:GridViewDataTextColumn FieldName="BaseSystemHDtlCode" Visible="False" VisibleIndex="9">
                                    </dx:GridViewDataTextColumn>
                                 
                                </Columns>
                                <settingsbehavior allowfocusedrow="True" />
                                <settingspager pagesize="20" visible="False">
                                </settingspager>
                            </dx:ASPxGridView>
                        </detailrow>
                    </templates>
                </dx:ASPxGridView>
                 </div>      
                
                                                     
                                    
                
        </div>
       <br />

       <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="شماره اموال"></asp:Label>

				<asp:TextBox ID="txtSystemCode" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label5" runat="server" CssClass="control-label" Text="تلفن داخلی"></asp:Label>
                <asp:TextBox ID="txtTelfon" runat="server" placeholder="تلفن داخلی را در این قسمت بنویسید" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
			</div>

              <div class="col-md-4">
				<asp:Label ID="Label6" runat="server" CssClass="control-label" Text=" رمز ورود به ویندوز"></asp:Label>
                <asp:TextBox ID="txtRamz" runat="server"  placeholder="رمز ورود به ویندوز را در این قسمت بنویسید" 
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
			</div>
            		</div>

       <div class="row">
			<div class="col-md-12">    
                         <asp:Label ID="Label1" runat="server" CssClass="control-label" Text=" توضیحات"></asp:Label>
                <asp:TextBox ID="txt_ans" runat="server" placeholder="درخواست خود را در این قسمت بنویسید" Height="88px" 
                                            TextMode="MultiLine" 
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>            
       </div>
           </div>
                <br />

         <div class="row">
             <div class="col-md-4">
                 </div>
			<div class="col-md-8">
               
              
                  <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت و ارسال" />
                 <asp:Button ID="btnWork" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text=" برگشت " />
                
             			    			<asp:HiddenField ID="HiddenFieldUserID" runat="server" Value="0" />
                
             			    			</div>
              
      </div>
       <br />

        <div class="row">
			<div class="col-md-12">         
                      <asp:Label ID="lblCnt" runat="server" Text="Label"></asp:Label>
                </div>
        </div>
                        
                
           
             
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


