<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="sugConversationFrm.aspx.vb" Inherits="sugConversationFrm" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="fc-view-container">
        <div class="alert-info">
<div class="row">
           
            
             <div class="col-md-8">
                 <asp:Label ID="Label1" runat="server" class="control-label" Text="موضوع"></asp:Label>
                    <asp:TextBox ID="subject" class="form-control" runat="server"></asp:TextBox>
                
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label3" runat="server" class="control-label" Text="نوع پیشنهاد"></asp:Label>
                 <asp:DropDownList ID="sugType"  CssClass="form-control" Height="40px" runat="server">
                 </asp:DropDownList>
            </div>
        </div>
            <div class="row">
           
            <div class="col-md-4">
                 <asp:Label ID="Label4" runat="server" class="control-label" Text="پیش بینی هزینه اجرایی"></asp:Label>
                    <asp:TextBox ID="sugIncome" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label5" runat="server" class="control-label" Text="پیش بینی درآمد اجرایی"></asp:Label>
                    <asp:TextBox ID="realIncom" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label8" runat="server" class="control-label" Text="منافع حاصل از پیشنهاد"></asp:Label>
                    <asp:TextBox ID="benefits" class="form-control" runat="server"></asp:TextBox>
                 
            </div>
        </div>
        </div>
        
    
     <div class="row">
           
          
        </div>
    
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label6" runat="server" class="control-label" Text="شرح وضعیت موجود"></asp:Label>
                    <asp:TextBox ID="DescStatus" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
                    <asp:Label ID="Label7" runat="server" class="control-label" Text="شرح روش پیشنهادی"></asp:Label>
                    <asp:TextBox ID="sugMethod" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label9" runat="server" class="control-label" Text="شرح "></asp:Label>
                    <asp:TextBox ID="Description" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    </div> 
</asp:Content>

