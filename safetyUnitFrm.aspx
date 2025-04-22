<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="safetyUnitFrm.aspx.vb" Inherits="safetyUnitFrm" %>

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
                                        <div class="col-md-4">
            
             <asp:Label ID="Label11" runat="server"   class="control-label" Text="واحد"></asp:Label>
            
     <dx:ASPxComboBox ID="CmbUnit" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains"  CssClass="btn btn-default dropdown-toggle" Width="100%">
                                            </dx:ASPxComboBox>
   
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Icons/refresh.png" Width="25px" Height="25px" ValidationGroup="b" ToolTip="برای مشاهده سوابق واحد کلیک نمایید" />
        
             </div>
<div class="col-md-4">
  <asp:Button ID="btnSabege" runat="server" class="btn-primary" Text="سوابق واحد" />
                </div>


              			<div class="col-md-4">
          
               
                               </div>
             
                      </div>
                
         
        <br />

                  <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="lblKala" runat="server"  CssClass="control-label"   Text=" انتخاب کالا"></asp:Label>
                     

                <dx:ASPxComboBox ID="kalaCmb" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains"  CssClass="btn btn-default dropdown-toggle" Width="100%">
                                            </dx:ASPxComboBox>
                               
                               </div>
                  
                       <div class="col-md-4">
                <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="تعداد"></asp:Label>
             <asp:TextBox ID="txt_tedad" CssClass="form-control" runat="server" BackColor="#FFFF99" TextMode="Number" Width="100px" Height="40px">1</asp:TextBox>
            
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_tedad" ErrorMessage="تعداد خارج از رنج" MaximumValue="100000" MinimumValue="1" ValidationGroup="a" ForeColor="Red" Type="Integer"></asp:RangeValidator>
                
             </div>
                             <div class="col-md-4">
                    <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="توضیحات"></asp:Label>
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
                                                        <asp:BoundField DataField="omur" HeaderText="واحد" />
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

