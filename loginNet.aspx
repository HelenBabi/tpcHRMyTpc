<%@ Page Language="VB" AutoEventWireup="false" CodeFile="loginNet.aspx.vb" Inherits="loginNet" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>

<html lang="en" dir="rtl">
<head>
     <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Colorlib Templates">
    <meta name="author" content="Colorlib">
    <meta name="keywords" content="Colorlib Templates">
       <title>پتروشیمی من</title>
          <%--<link rel="stylesheet" href="assets/styles/style.min.css">--%>

	<!-- Waves Effect -->
	<%--<link rel="stylesheet" href="assets/plugin/waves/waves.min.css">--%>

	<!-- RTL -->
<%--	<link rel="stylesheet" href="assets/styles/style-rtl.min.css">--%>
     <!-- Vendor CSS-->
    <link href="vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all">
    <link href="vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all">
    <!-- Font special for pages-->
  

    <!-- Vendor CSS-->
    <link href="vendor/select2/select2.min.css" rel="stylesheet" media="all">
    <link href="vendor/datepicker/daterangepicker.css" rel="stylesheet" media="all">

    <!-- Main CSS-->
    <link href="css2/main.css" rel="stylesheet" media="all">





	<!-- RTL -->

</head>

 
<body>

<div id="single-wrapper">
    
    <form id="form1" runat="server" class="frm-single">
         <div class="page-wrapper bg-gra-01 p-t-130 p-b-100 font-poppins">
        <div class="wrapper wrapper--w680">
            <div class="card card-4">
                <div class="card-body">
                                     <h6 class="title"> شرکت پتروشیمی تبریز </h6>
                                                                       
              
   <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                   <asp:Label ID="Label1" runat="server" class="label" Text="نام کاربری"></asp:Label>
                                             <asp:TextBox ID="TextBox1" runat="server" placeholder=" "
                    ValidationGroup="a"  class="input--style-4"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام کاربری" ControlToValidate="TextBox1" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                                  <div class="input-group">
                                   <asp:Label ID="Label3" runat="server" class="label" Text="کلمه عبور"></asp:Label>
                                             <asp:TextBox ID="TextBox2" runat="server" placeholder=" "
                    ValidationGroup="a"  class="input--style-4" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="کلمه عبور" ControlToValidate="TextBox2" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                           
                        </div>
                    <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                     <cc1:CaptchaControl ID="cptCaptcha" runat="server" 
        CaptchaBackgroundNoise="Low" CaptchaLength="5" 
        CaptchaHeight="60" CaptchaWidth="200" 
        CaptchaLineNoise="None" CaptchaMinTimeout="5" 
        CaptchaMaxTimeout="240" FontColor = "#529E00" CaptchaChars="123456789" />
                  <asp:TextBox ID="txtCaptcha" runat="server" class="input--style-4" placeholder="کد امنیتی"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="کد امنیتی باید وارد شود" ControlToValidate="txtCaptcha" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*Required" ControlToValidate = "txtCaptcha" ValidationGroup="a"></asp:RequiredFieldValidator>
               <asp:Label ID="lblErrorMessage" runat="server" Font-Names = "Arial" Text=""></asp:Label> 
                                      
                                </div>
                            </div>
                        </div>
                    <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                    
                <asp:HiddenField ID="hdn_tmp" runat="server" />
                                    
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                       <asp:Button ID="btnBrwsSys" runat="server" class="btn btn--radius-2 btn--blue" Text="ورود" ValidationGroup="a"   />
                                      
                                </div>
                            </div>
                        </div>


        </div>
                            </div>
              </div>
             </div>      
             <script src="vendor/jquery/jquery.min.js"></script>
    <!-- Vendor JS-->
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/datepicker/moment.min.js"></script>
    <script src="vendor/datepicker/daterangepicker.js"></script>

    <!-- Main JS-->
    <script src="js/global.js"></script> 
    </form>
	<!-- /.frm-single -->
</div><!--/#single-wrapper -->


	<script src="assets/scripts/jquery.min.js"></script>
	<script src="assets/scripts/modernizr.min.js"></script>
	<script src="assets/plugin/bootstrap/js/bootstrap.min.js"></script>
	<script src="assets/plugin/nprogress/nprogress.js"></script>
	<script src="assets/plugin/waves/waves.min.js"></script>

	<script src="assets/scripts/main.min.js"></script>
</body>
</html>
