<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tpcRegisterEdit.aspx.vb" Inherits="tpcRegisterEdit" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>

<html dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Colorlib Templates">
    <meta name="author" content="Colorlib">
    <meta name="keywords" content="Colorlib Templates">
      <title>Tpc HR</title>
	<link rel="stylesheet" href="assets/styles/style.min.css">

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
    
    <form id="form1" runat="server" class="frm-single" defaultbutton="reqIDtxt" defaultfocus="reqIDtxt">
			<div class="inside">
			<div class="title">شرکت پتروشیمی تبریز</div>
			<!-- /.title -->
			<div class="frm-title">سامانه پتروشیمی من</div>
			<!-- /.frm-title -->
			<div class="frm-input">
            <asp:TextBox ID="TextBox1" runat="server" class="frm-inp" EnableTheming="True" TabIndex="1" placeholder="کدپرسنلی" TextMode="Number" 
         ></asp:TextBox>
                        </div>
			<!-- /.frm-input -->
			<div class="frm-input">
                                   <asp:Label ID="Label16" runat="server" class="label" Text="کد پرسنلی"></asp:Label>
                                             <asp:TextBox ID="reqIDtxt" runat="server" placeholder=" "
                    ValidationGroup="a"  class="input--style-4" TextMode="Number"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="کد پرسنلی" ControlToValidate="reqIDtxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                

                                </div>
                            </div>
                        </div>
   <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                   <asp:Label ID="Label1" runat="server" class="label" Text="رمز عبور"></asp:Label>
                                             <asp:TextBox ID="codeMelliTxt" runat="server" placeholder=" "
                    ValidationGroup="a"  class="input--style-4" TextMode="Number"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="کلمه عبور" ControlToValidate="codeMelliTxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                

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
</body>
</html>
