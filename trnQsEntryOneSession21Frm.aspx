<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnQsEntryOneSession21Frm.aspx.vb" Inherits="trnQsEntryOneSession21Frm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
           <div class="container">

           
                  <div class="alert-success">
                             
                               <h3> <asp:Label ID="lblPayam" runat="server" Text="" Visible="False"></asp:Label>
                                 
</h3>
                          </div>
               <div class="alert-info">
                             
                               <h3> <asp:Label ID="lblPayam2" runat="server" Text="" Visible="False"></asp:Label>
                                 
</h3>
                          </div>
               </div>
    

           
    <br />
     
    <div class="row">
        <div class="col-md-12">
              <asp:Panel ID="Panel3" runat="server" GroupingText="دوره غیرحضوری- مشاهده سوال های دوره">
                      <div class="row">
                                        <div class="col-md-6">
            
             <asp:Label ID="Label11" runat="server"  class="control-label" Text="دوره"></asp:Label>
            
                                            <asp:DropDownList ID="CmbUnit" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px"></asp:DropDownList>
        
             </div>

<div class="col-md-3">
    

    </div>

              			<div class="col-md-3">
                            
             
                               </div>
             
                      </div>
                  <hr />
      <div class="row">
			<div class="col-md-12">
              
                <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="A1Tahoma" Font-Overline="False" Font-Size="Small" GridLines="Both" Height="60px" RepeatColumns="8" RepeatDirection="Horizontal" style="text-align: center; margin-left: 1px; background-color: #339966;" Width="100%">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BorderStyle="Outset" ForeColor="#000066" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="96px" ImageUrl="~/Icons/question-mark-48.png" PostBackUrl='<%# String.Format("trnQsEntryOneSession22Frm.aspx?id={0}", Eval("qSTypeID"))%>' style="background-color: #339966" Width="96px" />
            <br />
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Size="13pt" style="color: #FFFFFF; background-color: #339966;" Text='<%# Eval("qsTypeValue")%>' Width="110px"></asp:HyperLink>
            <br />
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>

                </div>
         
          </div>
               </asp:Panel>

        </div>

    </div>
      <div class="row">
		
                <div class="col-md-4">
                  
                </div>
                <div class="col-md-4">
               
                </div>
                  <div class="col-md-4">
                       
                </div>
          
                </div>

          <br />

             <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">

                </div>
                <div class="col-md-3">

                </div>
                  <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm " Text="برگشت" />
                </div>
                </div>
       

                                   
</asp:Content>

