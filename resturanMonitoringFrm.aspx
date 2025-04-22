<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="resturanMonitoringFrm.aspx.vb" Inherits="resturanMonitoringFrm" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
		          <div class="col-md-2">

                </div>
          <div class="col-md-4">

                </div>
          <div class="col-md-6">

                <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />

         
                </div>

          </div>

    
     <div class="row">
			<div class="col-md-12">
                 
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">     
  <ContentTemplate>    
      <asp:Timer ID="Timer1" runat="server" Interval="2000" Enabled="True" ontick="Timer1_Tick"></asp:Timer>
            
       <asp:Label ID="Label1" runat="server" Text="رستوران یک غذای با برنج"></asp:Label>
                <asp:DataList ID="DataList1" runat="server"
                                      
                    RepeatColumns="10" RepeatDirection="Horizontal" style="text-align: center">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <asp:Label ID="LabelRadif" runat="server" Text='<%# Eval("radif")%>'></asp:Label>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="150px" 
                            ImageUrl='<%# Eval("codePersoneli", "images\person_pic\{0}")%>' 
                            Width="100px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                       Text='<%# Eval("person")%>' ></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>
    
     <asp:Label ID="Label2" runat="server" Text="رستوران یک غذای خوراک"></asp:Label>
                <asp:DataList ID="DataList2" runat="server"
                                      
                    RepeatColumns="10" RepeatDirection="Horizontal" style="text-align: center">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <asp:Label ID="LabelRadif" runat="server" Text='<%# Eval("radif")%>'></asp:Label>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="150px" 
                            ImageUrl='<%# Eval("codePersoneli", "images\person_pic\{0}")%>' 
                            Width="100px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                       Text='<%# Eval("person")%>' ></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>
     
                  <asp:Label ID="Label3" runat="server" Text="رستوران دو غذای  برنج"></asp:Label>
                <asp:DataList ID="DataList3" runat="server"
                                      
                    RepeatColumns="10" RepeatDirection="Horizontal" style="text-align: center">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <asp:Label ID="LabelRadif" runat="server" Text='<%# Eval("radif2")%>'></asp:Label>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="150px" 
                            ImageUrl='<%# Eval("codePersoneli", "images\person_pic\{0}")%>' 
                            Width="100px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                       Text='<%# Eval("person")%>' ></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>

    

       <asp:Label ID="Label4" runat="server" Text="رستوران دو غذای خوراک"></asp:Label>
                <asp:DataList ID="DataList4" runat="server"
                                      
                    RepeatColumns="10" RepeatDirection="Horizontal" style="text-align: center">
                    <ItemStyle BorderStyle="Outset" />
                    <ItemTemplate>
                        <asp:Label ID="LabelRadif" runat="server" Text='<%# Eval("radif2")%>'></asp:Label>
                        <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="150px" 
                            ImageUrl='<%# Eval("codePersoneli", "images\person_pic\{0}")%>' 
                            Width="100px" />
            <br />
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                       Text='<%# Eval("person")%>' ></asp:HyperLink>
            <br />
                    </ItemTemplate>
                </asp:DataList>
                   </ContentTemplate>
</asp:UpdatePanel>




                </div>
         
          </div>
    <br />
     <div class="row">
			<div class="col-md-6">
                </div>
         <div class="col-md-6">

            

             </div>
         </div>
    
     
    
     
</asp:Content>

