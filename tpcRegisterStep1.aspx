<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tpcRegisterStep1.aspx.vb" Inherits="tpcRegisterStep1" %>

<!DOCTYPE html>

<html dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Colorlib Templates">
    <meta name="author" content="Colorlib">
    <meta name="keywords" content="Colorlib Templates">
       <title>سامانه درخواست اشتغال</title>
          
     <!-- Vendor CSS-->
    <link href="vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all">
    <link href="vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all">
    <!-- Font special for pages-->
  

    <!-- Vendor CSS-->
    <link href="vendor/select2/select2.min.css" rel="stylesheet" media="all">
    <link href="vendor/datepicker/daterangepicker.css" rel="stylesheet" media="all">

    <!-- Main CSS-->
    <link href="css/main.css" rel="stylesheet" media="all">
    <link href="css/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
   <div class="page-wrapper bg-gra-01 p-t-130 p-b-100 font-poppins">
        <div class="wrapper wrapper--w680">
              <div id="dvv" style=" 
             width:680;
                          height:100px;
          background-image:url(images/bg-head-02.jpg);
          -webkit-box-shadow: 0px 8px 20px 0px rgba(0, 0, 0, 0.15);
  -moz-box-shadow: 0px 8px 20px 0px rgba(0, 0, 0, 0.15);
  box-shadow: 0px 8px 20px 0px rgba(0, 0, 0, 0.15);
   padding-top: 210px;
        "></div>
            <div class="card card-4">
                                 <div class="card-body">
                                     <h6 class="title"> گام اول - قوانین و مقررات </h6>
                                                                       <h6 class="title2">تکمیل فرم های ذیل به هیچ وجه به منزله جذب و استخدام اینجانب در شرکت پتروشیمی تبریز و زیر مجموعه های آن چه به صورت رسمی، قراردادی و چه به صورت پیمانکاری، روزمزدی و غیره نبوده و صرفاً جهت آگاهی و تکمیل بانک اطلاعاتی مدیریت منابع انسانی می باشد.</h6>
                                                                                            <h6 class="title2">فایل تصویر کارت ملی و مدرک تحصیلی و عکس شخص متقاضی و شناسنامه با فرمتjpgحداکثر حجم فایل 100 کیلو بایت  برای ثبت نام الزامی می باشد</h6>
                                         <h6 class="title2">دریافت شماره پرونده بمنزله اتمام ثبت نام می باشد</h6>
                                                                           </div>
                 <div class="p-t-20">
                          
                   <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>
                <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                    
             <asp:Button ID="btnPage1" runat="server" class="btn btn--radius-2 btn--blue" Text="ثبت نام" ValidationGroup="a"  />
                                    
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                       <asp:Button ID="btnBrwsSys" runat="server"  Text="ویرایش " ValidationGroup="a" CssClass="btn btn--radius btn--green"    />
                                      
                                </div>
                            </div>
                        </div>
                </div>
            
            </div>
       </div>
    </form>
</body>
</html>
