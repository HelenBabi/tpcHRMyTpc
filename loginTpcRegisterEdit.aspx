<%@ Page Language="VB" AutoEventWireup="false" CodeFile="loginTpcRegisterEdit.aspx.vb" Inherits="loginTpcRegisterEdit" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>

<html dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="UTF-8"/>
    	<title>پتروشیمی من</title>


	<!-- Waves Effect -->

	<!-- RTL -->


    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content="Colorlib Templates"/>


     
          
     <!-- Vendor CSS-->
    <link href="vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all"/>
    <link href="vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all"/>
    <!-- Font special for pages-->
  

    <!-- Vendor CSS-->
    <link href="vendor/select2/select2.min.css" rel="stylesheet" media="all"/>
    <link href="vendor/datepicker/daterangepicker.css" rel="stylesheet" media="all"/>

    <!-- Main CSS-->
    <link href="css/main.css" rel="stylesheet" media="all"/>
    
</head>
<body>
    <form id="form1" runat="server">
         <div id="single-wrapper">
    
   
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
	
		
               <div class="row row-space">
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
                   <asp:Button ID="Button1" runat="server" Text="Button" />
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
			<div class="frm-footer">IT DEPARTMENT
                <h5 style="color:red">لطفا از مرورگرهای Firefox یا GoogleChrome استفاده نمایید</h5>


			</div>
            
			<!-- /.footer -->
		</div>
		<!-- .inside -->
	</form>
	<!-- /.frm-single -->
</div>      
             <script src="vendor/jquery/jquery.min.js"></script>
    <!-- Vendor JS-->
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/datepicker/moment.min.js"></script>
    <script src="vendor/datepicker/daterangepicker.js"></script>

    <!-- Main JS-->
    <script src="js/global.js"></script> 
    </form>
</body>
</html>
