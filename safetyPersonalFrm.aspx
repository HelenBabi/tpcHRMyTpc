<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="safetyPersonalFrm.aspx.vb" Inherits="safetyPersonalFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
           
                 
                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                          <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                             <div class="row">
                                        <div class="col-md-6">
            
             <asp:Label ID="Label11" runat="server"  class="control-label" Text="پرسنل"></asp:Label>
            
     
   <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        TargetControlID="holderTxt" PopupControlID="Panel11" BackgroundCssClass="modalBackground" CancelControlID="Image4">
        </cc1:ModalPopupExtender>
                             <asp:TextBox ID="holderTxt" runat="server" CssClass="form-control" 
                    ValidationGroup="a"  ></asp:TextBox>
        <asp:Label ID="holderId" runat="server" Visible="false" ></asp:Label>
      
        
             </div>



              			
             <div class="col-md-3">
  <asp:Button ID="btnSabege" runat="server" class="btn-primary" Text="سوابق پرسنل" />
                </div>
                                 <div class="col-md-3">
              
               
                               </div>
                      </div>
                
         
        <br />

                  <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="lblKala" runat="server" class="control-label"  Text=" انتخاب کالا"></asp:Label>
                
                     <dx:ASPxComboBox ID="kalaCmb" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains"  CssClass="btn btn-default dropdown-toggle" Width="100%">
                                            </dx:ASPxComboBox>
                               
                               </div>
                     
                       <div class="col-md-4">
                <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="تعداد"></asp:Label>
             <asp:TextBox ID="txt_tedad" CssClass="form-control" runat="server" BackColor="#FFFF99" TextMode="Number" Width="100px" Height="40px">1</asp:TextBox>
            
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_tedad" ErrorMessage="تعداد خارج از رنج" MaximumValue="100000" MinimumValue="1" ValidationGroup="a" ForeColor="Red" Type="Integer"></asp:RangeValidator>
             </div>
                          <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" Class="control-label" Text="توضیحات"></asp:Label>
             <asp:TextBox ID="tozihat" CssClass="form-control" runat="server"  Height="40px"></asp:TextBox>
                             
                               </div>
                      </div>
                          <br />

                  <div class="row">
              
			   <div class="col-md-3">
              
               
                               </div>
                         <div class="col-md-3">
              
               
                               </div>
                         <div class="col-md-3">
               <asp:Button ID="Button1" runat="server" class="btn btn-success btn-sm waves-effect waves-light"  Text="ثبت" />
               
                               </div>
                         <div class="col-md-3">
              
               
                               </div>
                      </div>
                       <br />
              



                                <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="lblSabege" runat="server" CssClass="control-label" Text="سوابق " Visible="false"></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="idDtl" Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%" ForeColor="Black" GridLines="Vertical">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:BoundField DataField="idDtl" HeaderText="#">
                                                        <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="karbar" HeaderText="پرسنل" />
                                                        <asp:BoundField DataField="kalaValue" HeaderText="کالا" />
                                                       
                                                        <asp:BoundField DataField="tarix" HeaderText="تاریخ " >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tedad" HeaderText="تعداد">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="تاریخ تحویل" DataField="tarixThavil" Visible="False" />
                                                        <asp:BoundField DataField="statValue" HeaderText="وضعیت" Visible="False" />
                                                             <asp:BoundField DataField="tozihat" HeaderText="توضیحات" />
                                                             <asp:ButtonField CommandName="del" Text="حذف" >
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
         </ContentTemplate>
                </asp:UpdatePanel>

                        </asp:Panel>
             
                 <br />
                  <div class="row">
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idDtl" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="kalaValue" HeaderText="کالا" />
                                            <asp:BoundField DataField="sizeValue" HeaderText="سایز" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ " />
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت" />
                                            <asp:BoundField DataField="tozihat" HeaderText="توضیحات" />
                                            <asp:ButtonField CommandName="del" Text="ابطال" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle Font-Size="Medium" ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                               </div>
                      </div>
           
    <br />
     <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="Panel11" runat="server" Width="60%" Height="100%">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server"  CssClass="modalPopup" >
    
            <ContentTemplate>
                <asp:Image ID="Image4" runat="server" 
                    ImageUrl="~/images/design_images/dialog_close.png" />
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" KeyFieldName="codePersoneli" Width="100%"  
                    ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="کد پرسنلی" FieldName="codePersoneli" UnboundType="Integer" VisibleIndex="0">
                            <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("codePersoneli")%>' CommandName='<%# "select" %>' Text='<%# Eval("codePersoneli")%>'></asp:LinkButton>
                            </dataitemtemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="teldaxeli" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="سمت" FieldName="postname" VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                                            </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                </dx:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </asp:Panel>
        </div>
    </div>
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

