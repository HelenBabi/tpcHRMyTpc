<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="jobManagementGroupBrowseFrm.aspx.vb" Inherits="jobManagementGroupBrowseFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
     
     
     <div class="row">
		  
              <div class="col-md-6">
				<asp:Label ID="Label2" runat="server" CssClass="control-label" Text=" کارهای گروهی دریافتی من"></asp:Label>
        
                          
             
         </div>
          <div class="col-md-6">
 	
              <asp:Button ID="imgKartabl" runat="server" class="btn btn-danger btn-bordered waves-effect waves-light" Text="برگشت به کارتابل" />
              </div>
         
    </div>
           <div class="row">
			<div class="col-md-6">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButton1" >
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="جستجو بر اساس موضوع"></asp:Label>
          <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ToolTip="قسمتی از موضع مورد نظر را بنویسید " ></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                    </asp:Panel>
                </div>
             			<div class="col-md-6">

                </div>
               </div>
 
     <div class="row">
			<div class="col-md-12">

                		     <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="kmID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="7%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("kmID")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="مشاهده"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1">
                            <clearfilterbutton visible="True">
                            </clearfilterbutton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد" FieldName="kmID" 
                            ShowInCustomizationForm="True" VisibleIndex="2" >
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="karbar" VisibleIndex="4">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="date_start" VisibleIndex="5">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="date_end" VisibleIndex="6">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="الویت" FieldName="valuee" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="olaviat" Visible="False">
        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="درصد پیشرفت" FieldName="darsad" VisibleIndex="9">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="نوع کار" FieldName="jobTypeValue" VisibleIndex="10">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" />
                </dx:ASPxGridView>

                </div>
         </div>
     <div class="row">
			<div class="col-md-12">

                		     <dx:ASPxGridView ID="gridFind" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="kmID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="7%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh0" runat="server" 
                    CommandArgument='<%# Eval("kmID")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="مشاهده"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1">
                            <clearfilterbutton visible="True">
                            </clearfilterbutton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد" FieldName="kmID" 
                            ShowInCustomizationForm="True" VisibleIndex="2" >
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="karbar" VisibleIndex="4">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="date_start" VisibleIndex="5">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="date_end" VisibleIndex="6">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="الویت" FieldName="valuee" VisibleIndex="7">
                               <HeaderStyle HorizontalAlign="Center" />
                               <cellstyle horizontalalign="Center">
                               </cellstyle>
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="درصد پیشرفت" FieldName="darsad" VisibleIndex="9">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="نوع کار" FieldName="jobTypeValue" VisibleIndex="10">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="olaviat" Visible="False">
        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" />
                </dx:ASPxGridView>

                </div>
         </div>

            <div class="row">
			<div class="col-md-12">
                </div>
              </div>
 

</asp:Content>

