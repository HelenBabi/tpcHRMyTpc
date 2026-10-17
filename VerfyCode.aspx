<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerfyCode.aspx.vb" Inherits="VerfyCode" %>
<!DOCTYPE html>

<html lang="en" dir="rtl">
<head>
<%--	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">--%>
     <link rel="icon" type="image/png" href="images/Logo-site.png" />
	<title>پتروشیمی من</title>
	<link rel="stylesheet" href="assets/styles/style.min.css">
     <link rel="icon" type="image/png" href="images/Logo-site.png" />
	<!-- Waves Effect -->
	<link rel="stylesheet" href="assets/plugin/waves/waves.min.css">

	<!-- RTL -->
	<link rel="stylesheet" href="assets/styles/style-rtl.min.css">
    <style type="text/css">

    .r5 {
  border-bottom-left-radius:5px 5px;
  border-bottom-right-radius:5px 5px;
  border-top-left-radius:5px 5px;
  border-top-right-radius:5px 5px;
    color: #003366;
}
.txt_
{
     border-color: #9a9a9a; border-width: 1px; border-style: solid; background-color: #ebebeb;
     padding:0px 5px 0px 0px;
     margin:0px;
     color:Black;
     text-align:right;
    }
    </style>
</head>

<body>

<div id="single-wrapper">
    
      <form id="form1" runat="server" class="frm-single" defaultbutton="btnVerify" defaultfocus="txtCode">
			<div class="inside">
			<div class="title">تایید کد</div>
			<!-- /.title -->
			<div class="frm-title">سامانه پتروشیمی من</div>
			<!-- /.frm-title -->
			<div class="frm-input">
                  <div style="width:300px; margin:auto; padding-top:50px;">
            <asp:Label ID="Label1" runat="server" ForeColor="Red" />
          
           
        </div>
                <h3>
                  <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
          </h3>
                      <asp:TextBox ID="txtCode" runat="server" class="frm-inp" EnableTheming="True" TabIndex="1" placeholder="کد ارسال شده به موبایلتان را وارد نمایید" TextMode="Number" 
         ></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="شماره موبایل باید وارد شود" ControlToValidate="txtCode" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </div>
			<!-- /.frm-input -->
			<div class="frm-input">
                
                
                       
			<!-- /.frm-input -->
		
			  <asp:Button ID="btnVerify" runat="server" Text="تأیید" OnClick="btnVerify_Click" CssClass="btn btn-success btn-sm" />
   			<div class="row small-spacing">
				<div class="col-sm-12">
					<!-- /.txt-login-with -->
				</div>
				<!-- /.col-sm-12 -->
				<!-- /.col-sm-6 -->
				<div class="col-sm-6"></div>
			
			</div>
			<!-- /.row -->
			<div class="frm-footer">
              	 <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/login_2.aspx">بازگشت به صفحه اصلی</asp:HyperLink>


			</div>
             
		</div>
		<!-- .inside -->
	</form>
	<!-- /.frm-single -->
</div><!--/#single-wrapper -->


<%--	<script src="assets/scripts/jquery.min.js"></script>
	<script src="assets/scripts/modernizr.min.js"></script>
	<script src="assets/plugin/bootstrap/js/bootstrap.min.js"></script>
	<script src="assets/plugin/nprogress/nprogress.js"></script>
	<script src="assets/plugin/waves/waves.min.js"></script>

	<script src="assets/scripts/main.min.js"></script>--%>
</body>
</html>
