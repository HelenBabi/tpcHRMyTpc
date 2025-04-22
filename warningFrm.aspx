<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="warningFrm.aspx.vb" Inherits="warningFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<h3>فرم پروفایل</h3>--%>
<div class="alert alert-success">
 <%--<h3>این صفحه درحال بروزرسانی است</h3>--%>
    <%--<h4>لطفا  تماس نگیرید،درصورت آماده شدن فرم دریافت اطلاعات، حتما اطلاع رسانی خواهد شد</h4>--%>
  <h4>نظر شما باموفقیت ثبت گردید.</h4>
    
 <%--   <p>
        <a asp-controller="Account" asp-action="Login">ورود به سایت</a>
    </p>--%>
</div>
        <div class="row">
             <div class="col-md-4">
                 </div>
			<div class="col-md-8">
               
              
                 
                 <asp:Button ID="btnWork" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text=" برگشت" />
                
             			    			</div>
              
      </div>
</asp:Content>

