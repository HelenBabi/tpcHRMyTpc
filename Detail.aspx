<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Detail.aspx.vb" Inherits="Detail" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
        <div class="row">
            <div class="col-md-6">

            </div>
            
              <div class="col-md-6">
<asp:Label ID="Label1" runat="server" Font-Names="Droid Arabic Naskh" Font-Size="25pt" CssClass="control-label"></asp:Label>
            </div>
            </div>
     <div class="row">
            <div class="col-md-4">
                 <%--<asp:Label ID="Label4" runat="server"  CssClass="control-label" Text="نام" >  </asp:Label>--%>
               
              
            </div>
              <div class="col-md-4">
                   

            </div>
              <div class="col-md-4">
                    <asp:TextBox  ID="nam" runat="server" CssClass="control-form" Enabled="false" ></asp:TextBox>
            </div>
            </div>
          <div class="row">
            <div class="col-md-4">
                 <%--<asp:Label ID="Label5" runat="server"   CssClass="control-label" Text="تلفن"></asp:Label>--%>
                  
            </div>
              <div class="col-md-4">
                      
            </div>
              <div class="col-md-4">
                     <asp:TextBox ID="telf" runat="server"  CssClass="control-form" Enabled="false"></asp:TextBox>
            </div>
            </div>
       <div class="row">
            <div class="col-md-4">
                 <%--<asp:Label ID="Label5" runat="server"   CssClass="control-label" Text="تلفن"></asp:Label>--%>
                  
            </div>
              <div class="col-md-4">
                      
            </div>
              <div class="col-md-4">
                     <asp:TextBox ID="mobile" runat="server"  CssClass="control-form" Enabled="false" Visible="false" ></asp:TextBox>
            </div>
            </div>
      <div class="row">
            <div class="col-md-4">
                 <%--<asp:Label ID="Label2" runat="server"   CssClass="control-label" Text="تاریخ"></asp:Label>--%>
                  
            </div>
              <div class="col-md-4">
                      

            </div>
              <div class="col-md-4">
                    <asp:TextBox ID="tarix" runat="server" CssClass="control-form" Enabled="false"></asp:TextBox>
            </div>
            </div>
    <br />
    <div class="row">

        <div class="col-md-6">
              
        <asp:Image ID="Image2" runat="server" Height="300px" Width="400px" />

        </div>
        <div class="col-md-6">
 <asp:TextBox ID="tozih" runat="server" Font-Names="Droid Arabic Naskh" Font-Size="14pt" CssClass="control-form" Width="100%" Enabled="false" TextMode="MultiLine" Height="300px" BorderStyle="Groove"></asp:TextBox>
            
        </div>
    </div>
    
</asp:Content>

