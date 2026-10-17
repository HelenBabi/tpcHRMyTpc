<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login_1.aspx.vb" Inherits="light_login2" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>

<html lang="fa" dir="rtl">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="description" content="سامانه پتروشیمی من">
    <meta name="author" content="Petrochimi Tabriz">
    <link rel="icon" type="image/png" href="images/Logo-site.png" />
    <title>پتروشیمی من | ورود به سامانه</title>
    
    <!-- Bootstrap 5 CSS (محلی) -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    
    <!-- Font Awesome محلی -->
    <link rel="stylesheet" href="css/fontawesome.min.css">
    
    <!-- SweetAlert2 (محلی) -->
    <link rel="stylesheet" href="css/sweetalert2.min.css" />

    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            font-family: 'Tahoma', 'IranSans', 'Segoe UI', sans-serif;
            min-height: 100vh;
        }
        
        #single-wrapper {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }
        
        .frm-single {
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            overflow: hidden;
            transition: transform 0.3s ease;
            max-width: 500px;
            width: 100%;
        }
        
        .frm-single:hover {
            transform: translateY(-5px);
        }
        
        .inside {
            padding: 40px 30px;
        }
        
        .title {
            font-size: 20px;
            font-weight: bold;
            color: #667eea;
            text-align: center;
            margin-bottom: 10px;
            border-bottom: 2px solid #667eea;
            display: inline-block;
            width: auto;
            padding-bottom: 5px;
        }
        
        .frm-title {
            font-size: 28px;
            font-weight: 600;
            color: #333;
            text-align: center;
            margin: 20px 0 30px;
            position: relative;
        }
        
        .frm-title::after {
            content: '';
            display: block;
            width: 50px;
            height: 3px;
            background: linear-gradient(90deg, #667eea, #764ba2);
            margin: 10px auto 0;
            border-radius: 3px;
        }
        
        .frm-input {
            margin-bottom: 25px;
            position: relative;
        }
        
        .frm-inp {
            width: 100%;
            padding: 12px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            font-size: 14px;
            transition: all 0.3s ease;
            background: #f8f9fa;
            font-family: inherit;
            text-align: right;
        }
        
        .frm-inp:focus {
            border-color: #667eea;
            outline: none;
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
            background: white;
        }
        
        .frm-inp:hover {
            border-color: #764ba2;
        }
        
        .frm-submit {
            width: 100%;
            padding: 14px 20px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white !important;
            border: none;
            border-radius: 12px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-top: 10px;
            display: block;
            text-align: center;
        }
        
        .frm-submit:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(102,126,234,0.3);
            background: linear-gradient(135deg, #5a67d8 0%, #6b46a0 100%);
            color: white !important;
        }
        
        .frm-submit:active {
            transform: translateY(0);
        }
        
        .frm-submit:disabled {
            opacity: 0.6;
            cursor: not-allowed;
            transform: none;
        }

        /* استایل دکمه SSO */
        .sso-btn {
            width: 100%;
            padding: 14px 20px;
            background: #4285f4;
            color: white !important;
            border: none;
            border-radius: 12px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-top: 10px;
            display: block;
            text-align: center;
            text-decoration: none;
        }
        
        .sso-btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(66, 133, 244, 0.3);
            background: #3367d6;
            color: white !important;
        }
        
        .sso-btn:active {
            transform: translateY(0);
        }
        
        .divider {
            display: flex;
            align-items: center;
            text-align: center;
            margin: 20px 0;
        }
        
        .divider::before,
        .divider::after {
            content: '';
            flex: 1;
            border-bottom: 1px solid #e0e0e0;
        }
        
        .divider::before {
            margin-left: 15px;
        }
        
        .divider::after {
            margin-right: 15px;
        }
        
        .divider span {
            color: #999;
            font-size: 13px;
            white-space: nowrap;
        }
        
        .frm-footer {
            text-align: center;
            margin-top: 25px;
            padding-top: 20px;
            border-top: 1px solid #e0e0e0;
        }
        
        .frm-footer h5 {
            color: #e74c3c;
            font-size: 13px;
            margin: 0;
        }
        
        .validation-error {
            color: #e74c3c;
            font-size: 12px;
            display: block;
            margin-top: 5px;
            text-align: right;
        }
        
        .forgot-link {
            text-align: center;
            margin-top: 20px;
        }
        
        .forgot-link a {
            color: #667eea;
            text-decoration: none;
            font-size: 13px;
            transition: color 0.3s ease;
        }
        
        .forgot-link a:hover {
            color: #764ba2;
            text-decoration: underline;
        }
        
        .input-icon {
            position: relative;
        }
        
        .input-icon .icon-emoji {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 18px;
            z-index: 1;
        }
        
        .input-icon .frm-inp {
            padding-right: 45px;
            padding-left: 15px;
        }
        
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
        
        .inside {
            animation: fadeInUp 0.6s ease;
        }
        
        /* استایل پیام ورود برای ثبت حضور */
        .return-info {
            background: #d1ecf1;
            padding: 12px 15px;
            border-radius: 10px;
            margin-bottom: 20px;
            text-align: center;
            border: 2px solid #bee5eb;
            display: none;
        }
        
        .return-info.show {
            display: block;
        }
        
        .return-info span {
            font-size: 15px;
            color: #0c5460;
            font-weight: bold;
        }
        
        .return-info .icon {
            font-size: 20px;
            margin-left: 8px;
        }
        
        @media (max-width: 576px) {
            .inside {
                padding: 30px 20px;
            }
            
            .frm-title {
                font-size: 24px;
            }
            
            .title {
                font-size: 18px;
            }
            
            .frm-submit {
                padding: 12px 20px;
                font-size: 14px;
            }
        }
        
        input[type="submit"], input[type="button"], button {
            -webkit-appearance: none;
            appearance: none;
        }
        
        .loading {
            position: relative;
            pointer-events: none;
        }
        
        .loading::after {
            content: '';
            position: absolute;
            width: 20px;
            height: 20px;
            top: 50%;
            left: 20px;
            margin-top: -10px;
            border: 2px solid white;
            border-radius: 50%;
            border-top-color: transparent;
            animation: spin 0.6s linear infinite;
        }
        
        @keyframes spin {
            to { transform: rotate(360deg); }
        }
        
        .fa, .fas, .far, .fab {
            font-family: 'Segoe UI', 'Tahoma', sans-serif;
        }
        
        .fa-industry:before { content: "🏭"; }
        .fa-user-circle:before { content: "👤"; }
        .fa-id-card:before { content: "🆔"; }
        .fa-lock:before { content: "🔒"; }
        .fa-key:before { content: "🔑"; }
        .fa-phone-alt:before { content: "📞"; }
        .fa-google:before { content: "G"; font-weight: bold; font-size: 18px; }
        
        .sso-btn .btn-icon {
            margin-left: 10px;
        }
        
        /* ============================================ */
        /* استایل SweetAlert2 سفارشی - سایز درصدی */
        /* ============================================ */
        .swal2-popup.swal-rtl {
            width: 85% !important;
            max-width: 700px !important;
            min-height: 50vh !important;
            padding: 4% !important;
            border-radius: 1.5rem !important;
            direction: rtl !important;
            font-family: 'Tahoma', 'IranSans', sans-serif !important;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3) !important;
        }

        .swal2-popup.swal-rtl .swal2-title {
            font-size: 1.6rem !important;
            font-weight: bold !important;
            color: #2c3e50 !important;
            padding: 2% 0 !important;
            border-bottom: 2px solid #667eea !important;
            padding-bottom: 2% !important;
        }

        .swal2-popup.swal-rtl .swal2-html-container {
            font-size: 1.1rem !important;
            line-height: 2 !important;
            color: #444 !important;
            padding: 3% 2% !important;
            text-align: right !important;
            max-height: 35vh !important;
            overflow-y: auto !important;
        }

        .swal2-popup.swal-rtl .swal2-confirm {
            font-size: 1rem !important;
            padding: 2% 6% !important;
            border-radius: 0.8rem !important;
            font-weight: bold !important;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%) !important;
            transition: all 0.3s ease !important;
        }

        .swal2-popup.swal-rtl .swal2-confirm:hover {
            transform: translateY(-2px) !important;
            box-shadow: 0 8px 20px rgba(102,126,234,0.4) !important;
        }

        .swal2-popup.swal-rtl .swal2-icon {
            width: 15% !important;
            height: auto !important;
            min-width: 60px !important;
            min-height: 60px !important;
            margin: 2% auto !important;
        }

        .swal2-popup.swal-rtl .swal2-icon .swal2-icon-content {
            font-size: 2.5rem !important;
        }

        /* اسکرول‌بار زیبا برای متن پیغام */
        .swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar {
            width: 0.5rem;
        }

        .swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar-track {
            background: #f1f1f1;
            border-radius: 0.5rem;
        }

        .swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar-thumb {
            background: #667eea;
            border-radius: 0.5rem;
        }

        .swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar-thumb:hover {
            background: #5a67d8;
        }

        /* برای موبایل */
        @media (max-width: 576px) {
            .swal2-popup.swal-rtl {
                width: 92% !important;
                min-height: 40vh !important;
                padding: 5% !important;
            }
            
            .swal2-popup.swal-rtl .swal2-title {
                font-size: 1.2rem !important;
            }
            
            .swal2-popup.swal-rtl .swal2-html-container {
                font-size: 0.95rem !important;
                max-height: 30vh !important;
            }
            
            .swal2-popup.swal-rtl .swal2-confirm {
                font-size: 0.9rem !important;
                padding: 3% 8% !important;
            }
        }
        /* جاستیفای کردن متن پیغام */
.swal2-popup.swal-rtl .swal2-html-container {
    text-align: justify !important;
    text-justify: inter-word !important;
    direction: rtl !important;
    line-height: 2 !important;
    font-size: 1rem !important;
    padding: 3% 2% !important;
}
/* ============================================ */
/* استایل SweetAlert2 سفارشی - با جاستیفای */
/* ============================================ */
.swal2-popup.swal-rtl {
    width: 85% !important;
    max-width: 700px !important;
    min-height: 50vh !important;
    padding: 4% !important;
    border-radius: 1.5rem !important;
    direction: rtl !important;
    font-family: 'Tahoma', 'IranSans', sans-serif !important;
    box-shadow: 0 20px 60px rgba(0,0,0,0.3) !important;
}

.swal2-popup.swal-rtl .swal2-title {
    font-size: 1.6rem !important;
    font-weight: bold !important;
    color: #2c3e50 !important;
    padding: 2% 0 !important;
    border-bottom: 2px solid #667eea !important;
    padding-bottom: 2% !important;
    text-align: center !important;
}

/* ============================================ */
/* متن پیغام با جاستیفای */
/* ============================================ */
.swal2-popup.swal-rtl .swal2-html-container {
    font-size: 1rem !important;
    line-height: 2 !important;
    color: #444 !important;
    padding: 3% 2% !important;
    text-align: justify !important;        /* ← جاستیفای */
    text-justify: inter-word !important;   /* ← فاصله بین کلمات */
    direction: rtl !important;
    max-height: 35vh !important;
    overflow-y: auto !important;
}

/* ============================================ */
/* جاستیفای برای پاراگراف‌ها و لیست‌ها */
/* ============================================ */
.swal2-popup.swal-rtl .swal2-html-container p,
.swal2-popup.swal-rtl .swal2-html-container div,
.swal2-popup.swal-rtl .swal2-html-container li {
    text-align: justify !important;
    text-justify: inter-word !important;
    direction: rtl !important;
}

/* برای لیست‌های شماره‌دار */
.swal2-popup.swal-rtl .swal2-html-container ol,
.swal2-popup.swal-rtl .swal2-html-container ul {
    padding-right: 25px !important;
    text-align: justify !important;
    direction: rtl !important;
}

.swal2-popup.swal-rtl .swal2-html-container li {
    margin-bottom: 12px !important;
    text-align: justify !important;
    text-justify: inter-word !important;
}

.swal2-popup.swal-rtl .swal2-confirm {
    font-size: 1rem !important;
    padding: 2% 6% !important;
    border-radius: 0.8rem !important;
    font-weight: bold !important;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%) !important;
    transition: all 0.3s ease !important;
}

.swal2-popup.swal-rtl .swal2-confirm:hover {
    transform: translateY(-2px) !important;
    box-shadow: 0 8px 20px rgba(102,126,234,0.4) !important;
}

.swal2-popup.swal-rtl .swal2-icon {
    width: 15% !important;
    height: auto !important;
    min-width: 60px !important;
    min-height: 60px !important;
    margin: 2% auto !important;
}

.swal2-popup.swal-rtl .swal2-icon .swal2-icon-content {
    font-size: 2.5rem !important;
}

/* اسکرول‌بار */
.swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar {
    width: 0.5rem;
}

.swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar-track {
    background: #f1f1f1;
    border-radius: 0.5rem;
}

.swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar-thumb {
    background: #667eea;
    border-radius: 0.5rem;
}

.swal2-popup.swal-rtl .swal2-html-container::-webkit-scrollbar-thumb:hover {
    background: #5a67d8;
}

/* برای موبایل */
@media (max-width: 576px) {
    .swal2-popup.swal-rtl {
        width: 92% !important;
        min-height: 40vh !important;
        padding: 5% !important;
    }
    
    .swal2-popup.swal-rtl .swal2-title {
        font-size: 1.2rem !important;
    }
    
    .swal2-popup.swal-rtl .swal2-html-container {
        font-size: 0.95rem !important;
        max-height: 30vh !important;
        text-align: justify !important;
    }
    
    .swal2-popup.swal-rtl .swal2-confirm {
        font-size: 0.9rem !important;
        padding: 3% 8% !important;
    }
}
    </style>

</head>

<body>

<div id="single-wrapper">
    
    <form id="form1" runat="server" class="frm-single" defaultbutton="Button1" defaultfocus="TextBox1">
        <div class="inside">
            
            <!-- پیام ورود برای ثبت حضور -->
            <div id="divReturnInfo" runat="server" class="return-info">
                <span>
                    <span class="icon">📱</span>
                    برای ثبت حضور در دوره آموزشی، لطفاً وارد شوید
                </span>
            </div>
            
            <div class="text-center">
                <div class="title d-inline-block">
                    <span class="icon-emoji">🏭</span> شرکت پتروشیمی تبریز
                </div>
            </div>
            
            <div class="frm-title">
                <span class="icon-emoji">👤</span> سامانه پتروشیمی من
            </div>
            
            <div class="frm-input input-icon">
                <span class="icon-emoji">🆔</span>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="frm-inp" TabIndex="1" 
                    placeholder="کد پرسنلی" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ForeColor="Red" ErrorMessage="کد پرسنلی باید وارد شود" 
                    ControlToValidate="TextBox1" ValidationGroup="1"
                    CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            
            <div class="frm-input input-icon">
                <span class="icon-emoji">🔒</span>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="frm-inp" 
                    TextMode="Password" TabIndex="2" 
                    placeholder="رمز ورود (رمز پیش‌فرض: کد ملی)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ForeColor="Red" ErrorMessage="رمز عبور باید وارد شود" 
                    ControlToValidate="TextBox2" ValidationGroup="1"
                    CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            
            <div class="clearfix"></div>
            
            <div class="d-grid gap-2 mt-3">
                <asp:Button ID="Button1" runat="server" Text="ورود به سامانه" 
                    CssClass="frm-submit" OnClick="Button1_Click" ValidationGroup="1"/>
            </div>

            <!-- جداکننده -->
            <div class="divider">
                <span>یا</span>
            </div>

            <!-- دکمه SSO -->
            <div class="d-grid gap-2">
                <a href="https://sso.tpco.ir/Sso/Portal?applicationName=MyTpc" class="btn btn-primary btn-lg" style="padding-left: 2.5rem; padding-right: 2.5rem;color:white;"> ورود با SSO</a>
            </div>
            
            <div class="forgot-link">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgotPass.aspx">
                    <span class="icon-emoji">🔑</span> رمز عبور خود را فراموش کرده‌اید؟
                </asp:HyperLink>
            </div>
            
            <div class="frm-footer">
                <h5>
                    <span class="icon-emoji">📞</span> 
                    نیاز به راهنمایی دارید؟ با شماره 33286380 تماس بگیرید
                </h5>
            </div>
            
            <!-- HiddenField برای پیغام مدیر سیستم -->
<div id="divSystemMessage" style="display:none;">
    <asp:Literal ID="litSystemMessage" runat="server" />
</div>
<div id="divMessageTitle" style="display:none;">
    <asp:Literal ID="litMessageTitle" runat="server" />
</div>
            
            <!-- HiddenField برای ReturnUrl -->
            <asp:HiddenField ID="hdnReturnUrl" runat="server" />
            
        </div>
    </form>
    
</div>

<!-- ============================================ -->
<!-- اسکریپت‌های محلی -->
<!-- ============================================ -->
<script src="js/jquery-3.6.0.min.js"></script>
<script src="js/bootstrap.bundle.min.js"></script>

<!-- SweetAlert2 (محلی) -->
<script src="js/sweetalert2.all.min.js"></script>

<!-- ============================================ -->
<!-- اسکریپت نمایش پیغام مدیر سیستم -->
<!-- ============================================ -->
<script src="js/jquery-3.6.0.min.js"></script>
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        var msgText = '<%= litSystemMessage.Text.Replace("'", "\'").Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ") %>';
        var msgTitle = '<%= litMessageTitle.Text.Replace("'", "\'") %>';

        if (msgText && msgText.trim() !== '' && msgText !== '&nbsp;') {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: msgTitle || '📢 پیغام مدیر سیستم',
                    text: msgText,
                    icon: 'info',
                    confirmButtonText: 'متوجه شدم',
                    confirmButtonColor: '#667eea',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        popup: 'swal-rtl'
                    }
                });
            } else {
                alert((msgTitle || 'پیغام مدیر سیستم') + '\n\n' + msgText);
            }
        }
    });
</script>

<!-- ============================================ -->
<!-- اسکریپت ReturnUrl -->
<!-- ============================================ -->
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        var params = new URLSearchParams(window.location.search);
        var returnUrl = params.get('ReturnUrl');
        if (returnUrl) {
            var hdn = document.getElementById('<%= hdnReturnUrl.ClientID %>');
            if (hdn) {
                hdn.value = returnUrl;
            }
        }
    });
</script>

<!-- ============================================ -->
<!-- اسکریپت دکمه ورود -->
<!-- ============================================ -->
<script type="text/javascript">
    $(document).ready(function () {
        var $btn = $('#<%= Button1.ClientID %>');
        if ($btn.length > 0) {
            $btn.css('display', 'block');
        }

        $('#<%= Button1.ClientID %>').click(function (e) {
            if (typeof Page_ClientValidate === 'function') {
                if (Page_ClientValidate('1')) {
                    $(this).addClass('loading');
                    $(this).val('در حال ورود...');
                }
            } else {
                $(this).addClass('loading');
                $(this).val('در حال ورود...');
            }
        });

        $('.frm-inp').on('input', function () {
            $(this).siblings('.validation-error').hide();
        });
    });

    window.onload = function () {
        var btn = document.getElementById('<%= Button1.ClientID %>');
        if (btn && btn.classList.contains('loading')) {
            btn.classList.remove('loading');
            btn.value = 'ورود به سامانه';
        }
    };
</script>

</body>
</html>