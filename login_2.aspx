<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login_2.aspx.vb" Inherits="light_login2" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
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
    
    <form id="form1" runat="server" class="frm-single" defaultbutton="Button1" defaultfocus="TextBox1">
			<div class="inside">
			<div class="title">شرکت پتروشیمی تبریز</div>
			<!-- /.title -->
			<div class="frm-title">سامانه پتروشیمی من</div>
			<!-- /.frm-title -->
			<div class="frm-input">
            <asp:TextBox ID="TextBox1" runat="server" class="frm-inp" EnableTheming="True" TabIndex="1" placeholder="کدپرسنلی" TextMode="Number" 
         ></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="کد پرسنلی باید وارد شود" ControlToValidate="TextBox1" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </div>
			<!-- /.frm-input -->
			<div class="frm-input">
                    <asp:TextBox ID="TextBox2" runat="server"  class="frm-inp" 
                        TextMode="Password" TabIndex="2" placeholder="رمز عبور را اگر تغییر نداده اید کدملی میباشد"
          ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="رمز عبور باید وارد شود" ControlToValidate="TextBox2" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <i class="fa fa-lock frm-ico"></i></div>
			<!-- /.frm-input -->
			<div class="clearfix margin-bottom-20">
			
			</div>
			<!-- /.clearfix -->
		
                <%--<div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                     <cc1:CaptchaControl ID="cptCaptcha" runat="server" 
        CaptchaBackgroundNoise="Low" CaptchaLength="4" 
        CaptchaHeight="60" CaptchaWidth="150" 
        CaptchaLineNoise="None" CaptchaMinTimeout="5" 
        CaptchaMaxTimeout="240" FontColor = "#529E00" CaptchaChars="123456789" Visible="False" />
                  <asp:TextBox ID="txtCaptcha" runat="server" class="input--style-4" placeholder="کد امنیتی" Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="کد امنیتی باید وارد شود" ControlToValidate="txtCaptcha" ForeColor="Red" ValidationGroup="aaa"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate = "txtCaptcha" ValidationGroup="a"></asp:RequiredFieldValidator>
               <asp:Label ID="lblErrorMessage" runat="server" Font-Names = "Arial" Text=""></asp:Label> 
                                      
                                </div>
                            </div>
                        </div>--%>
             <asp:Button ID="Button1" runat="server" Text="ورود" class="frm-submit" OnClick="Button1_Click" ValidationGroup="1"/>

   			<div class="row small-spacing">
				<div class="col-sm-12">
					<!-- /.txt-login-with -->
				</div>
				<!-- /.col-sm-12 -->
				<!-- /.col-sm-6 -->
				<div class="col-sm-6"></div>
				<!-- /.col-sm-6 -->
			</div>
			<!-- /.row -->
			<div class="frm-footer">
                <h5 style="color:red">لطفا از مرورگرهای Firefox یا GoogleChrome استفاده نمایید</h5>


			</div>
                <%----%>
			<!-- /.footer -->
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
