<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="kartexTOkartexFrm.aspx.vb" Inherits="kartexTOkartexFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="container">

           
                  <div class="alert-success">
                             
                               <p> <asp:Label ID="lblPayam" runat="server" Text="فرم انتقال سوابق آموزشی" ></asp:Label>
                                 
</p>
                          </div>
               </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="lbl1" runat="server" Text="کد پرسنلی قدیم" CssClass="control-label"></asp:Label>
              <asp:TextBox ID="txt_CurentPass" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="form-control" TextMode="Number"></asp:TextBox>
               <asp:RequiredFieldValidator ID="vald_Passname" runat="server" 
                    ControlToValidate="txt_NewPass1" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
   
              
        </div>
         <div class="col-md-4">

        </div>
         <div class="col-md-4">

        </div>
    </div>
     <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="کد پرسنلی جدید" CssClass="control-label"></asp:Label>
              <asp:TextBox ID="txt_NewPass1" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="form-control" TextMode="Number"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_NewPass1" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
         <div class="col-md-4">

        </div>
         <div class="col-md-4">

        </div>
    </div>
     <div class="row">
           <div class="col-md-4">

        </div>
        <div class="col-md-4">
             <asp:Button ID="Button1" class="btn-m btn-success waves-effect waves-light" runat="server" Text="ذخیره" Width="100px" ValidationGroup="a" />
        </div>
         <div class="col-md-4">
             <asp:Button ID="Button2" class="btn-md btn-warning waves-effect waves-light" runat="server" Text="برگشت" Width="100px" />
                       
               
              
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
      <div class="row">
        <div class="col-md-12">

        </div>

    </div>

        
</asp:Content>

