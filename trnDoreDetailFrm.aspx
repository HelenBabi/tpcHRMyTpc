<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDoreDetailFrm.aspx.vb" Inherits="trnDoreDetailFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Enabled="False" GroupingText="اطلاعات دوره" BorderStyle="Solid">


<div class="fc-view-container">
        <div class="alert-info">
            <div class="row">
                    
                        <div class="col-md-4">
                 <asp:Label ID="Label12" runat="server" class="control-label" Text="درخواست کننده"></asp:Label>
                <asp:TextBox ID="karbar" class="form-control" runat="server"></asp:TextBox>
            </div>
                <div class="col-md-8">
               
                
            </div>
        </div>
<div class="row">
           
            
             <div class="col-md-8">
                 <asp:Label ID="Label1" runat="server" class="control-label" Text="نام دوره"></asp:Label>
                    <asp:TextBox ID="subject" class="form-control" runat="server"></asp:TextBox>
                
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label2" runat="server" class="control-label" Text="مدرس"></asp:Label>
                 <asp:TextBox ID="modares" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
            <div class="row">
           
            <div class="col-md-4">
                 <asp:Label ID="Label4" runat="server" class="control-label" Text="از تاریخ"></asp:Label>
                    <asp:TextBox ID="aztarix" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label6" runat="server" class="control-label" Text="تا تاریخ"></asp:Label>
                    <asp:TextBox ID="tatarix" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label8" runat="server" class="control-label" Text="ساعت"></asp:Label>
                    <asp:TextBox ID="saat" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()"></asp:TextBox>
                 
            </div>
        </div>
        </div>
        
    
    <br />
    </div>


        
            </asp:Panel>
    <br />
        <div class="alert-success">
                             
                               <p>سرپرست محترم : خواهشمند است جهت تعيين ميزان تاثيرگذاري دوره مذكور در ارتقاء كيفي انجام وظايف شغلي كاركنان‌، در جدول زير بر روي جزئيات كليك كرده و يكي از گزينه هاي ( بالا - متوسط - پايين ) را انتخاب كنيد .  باتشكر
                                 
</p>
                          </div>
   

    <br />



      



      <asp:Panel ID="Panel1" runat="server" GroupingText="میزان تاثیرگذاری بر ارتقای کیفیت وظایف شغلی" BorderStyle="Solid">
          <div class="row">
                  <div class="col-md-3">

                </div>
			<div class="col-md-3">
             
               
                           
                </div>
              
                <div class="col-md-3">
                </div>
                  <div class="col-md-3">

                </div>
                </div>
          <br />
            <div class="row">
                  <div class="col-md-3">

                </div>
			<div class="col-md-3">
           
                 <asp:Button ID="Button1" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="زیاد" />
                           
                </div>
              
                <div class="col-md-3">
                </div>
                  <div class="col-md-3">

                </div>
                </div>
             <br />
           <div class="row">
                  <div class="col-md-3">
                </div>
			<div class="col-md-3">
               <asp:Button ID="Button2" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="متوسط" />
               
                           
                </div>
             
                <div class="col-md-3">
                </div>
                  <div class="col-md-3">

                </div>
                </div>
             <br />
           <div class="row">
                <div class="col-md-3">
                </div>
			<div class="col-md-3">
                 <asp:Button ID="Button3" runat="server" class="btn btn-danger btn-sm waves-effect waves-light" Text="کم" />
               
                           
                </div>
               
                <div class="col-md-3">
                </div>
                  <div class="col-md-3">

                </div>
                </div>
          

    <br />
          </asp:Panel>
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
</asp:Content>

