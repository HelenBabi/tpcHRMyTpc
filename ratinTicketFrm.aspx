<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ratinTicketFrm.aspx.vb" Inherits="ratinTicketFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="row">

			<h5>لطفا فرم ارزیابی ذیل را جهت ارایه خدمات بهتر تکمیل فرمایید</h5>

                      </div>
    <br />

      <div class="row">
             <div class="col-md-3">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="شماره درخواست"></asp:Label>
                  </div>
           <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="زمان درخواست"></asp:Label>
               
               
                  </div>
           <div class="col-md-3">
               </div>
           <div class="col-md-3">
               </div>
          </div>
      <div class="row">
             <div class="col-md-3">
                  <asp:TextBox ID="lblErjaID" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
 
                  </div>
           <div class="col-md-3">
           <asp:TextBox ID="lblZaman" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
               
                  </div>
           <div class="col-md-3">
               </div>
           <div class="col-md-3">
               </div>
          </div>
     
     <div class="row">
             <div class="col-md-3">
                 <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="متن درخواست"></asp:Label>
                  </div>
           <div class="col-md-3">
                    
               
               
                  </div>
           <div class="col-md-3">
               </div>
           <div class="col-md-3">
               </div>
          </div>
     <div class="row">
           
           <div class="col-md-12">
                
               <asp:TextBox ID="lblTozih" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
          </div>
    <br />

      <div class="row">

			  <div class="col-md-4">
                   
<asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Bold="True" Font-Size="Large">
    <asp:ListItem Value="0">خیلی ضعیف </asp:ListItem>
    <asp:ListItem Value="1">ضعیف </asp:ListItem>
    <asp:ListItem Value="2">متوسط</asp:ListItem>
    <asp:ListItem Value="3">خوب</asp:ListItem>
    <asp:ListItem Value="4">عالی</asp:ListItem>
                 </asp:RadioButtonList>
                  </div>
             <div class="col-md-4">

                  </div>
          
             <div class="col-md-4">
                  </div>
                      </div>
     <div class="row">
			  <div class="col-md-6">
<asp:TextBox ID="txtNote" runat="server" Height="200px" TextMode="MultiLine" Width="400px" placeholder="لطفا انتقاد یا پیشنهاد و توضیحات خورد را در این قسمت بنویسید" ></asp:TextBox>
                  </div>
           <div class="col-md-6">
               
                  </div>
         </div>
           <br />

         <div class="row">
             <div class="col-md-4">
                 </div>
			<div class="col-md-8">
               
              
                  <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت " />
                 <asp:Button ID="btnWork" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text=" برگشت" />
                
             			    			</div>
              
      </div>
</asp:Content>

