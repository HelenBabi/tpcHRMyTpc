<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login_2.aspx.vb" Inherits="light_login3" %>
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
    <title>پتروشیمی من</title>
    
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/fontawesome.min.css">
    <link rel="stylesheet" href="css/sweetalert2.min.css" />

    <style type="text/css">
        * { margin: 0; padding: 0; box-sizing: border-box; }
        
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            font-family: 'Tahoma', 'IranSans', 'Segoe UI', sans-serif;
            min-height: 100vh;
            padding: 20px;
        }
        
        #main-wrapper {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }
        
        .main-container {
            display: flex;
            gap: 25px;
            max-width: 1200px;
            width: 100%;
            flex-wrap: wrap;
            align-items: stretch;
        }
        
        /* ============================================ */
        /* ستون راست - فرم لاگین */
        /* ============================================ */
        .login-column {
            flex: 0 0 420px;
            max-width: 420px;
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            overflow: hidden;
            transition: transform 0.3s ease;
        }
        
        .login-column:hover { transform: translateY(-5px); }
        
        .login-inside {
            padding: 35px 30px;
            animation: fadeInUp 0.6s ease;
        }
        
        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        .title {
            font-size: 18px;
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
            font-size: 24px;
            font-weight: 600;
            color: #333;
            text-align: center;
            margin: 20px 0 25px;
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
        
        .frm-input { margin-bottom: 20px; position: relative; }
        
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
        
        .frm-inp:hover { border-color: #764ba2; }
        
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
        
        .frm-submit:disabled { opacity: 0.6; cursor: not-allowed; transform: none; }

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
        
        .divider {
            display: flex;
            align-items: center;
            text-align: center;
            margin: 20px 0;
        }
        
        .divider::before, .divider::after {
            content: '';
            flex: 1;
            border-bottom: 1px solid #e0e0e0;
        }
        
        .divider::before { margin-left: 15px; }
        .divider::after { margin-right: 15px; }
        
        .divider span { color: #999; font-size: 13px; white-space: nowrap; }
        
        .frm-footer {
            text-align: center;
            margin-top: 25px;
            padding-top: 20px;
            border-top: 1px solid #e0e0e0;
        }
        
        .frm-footer h5 { color: #e74c3c; font-size: 13px; margin: 0; }
        
        .validation-error {
            color: #e74c3c;
            font-size: 12px;
            display: block;
            margin-top: 5px;
            text-align: right;
        }
        
        .forgot-link { text-align: center; margin-top: 20px; }
        
        .forgot-link a {
            color: #667eea;
            text-decoration: none;
            font-size: 13px;
            transition: color 0.3s ease;
        }
        
        .forgot-link a:hover { color: #764ba2; text-decoration: underline; }
        
        .input-icon { position: relative; }
        
        .input-icon .icon-emoji {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 18px;
            z-index: 1;
        }
        
        .input-icon .frm-inp { padding-right: 45px; padding-left: 15px; }
        
        /* ============================================ */
        /* ستون چپ - اطلاعیه‌ها و تازه‌ها */
        /* ============================================ */
        .news-column {
            flex: 1;
            min-width: 400px;
            display: flex;
            flex-direction: column;
            gap: 20px;
        }
        
        /* ============================================ */
        /* کارت پیغام مدیر (بالای تازه‌ها) */
        /* ============================================ */
        .admin-message-card {
            background: linear-gradient(135deg, #fff3cd 0%, #ffe8a1 100%);
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            overflow: hidden;
            border: 2px solid #ffc107;
            animation: pulseGlow 2s infinite;
        }
        
        @keyframes pulseGlow {
            0% { box-shadow: 0 20px 60px rgba(0,0,0,0.3), 0 0 0 0 rgba(255, 193, 7, 0.4); }
            70% { box-shadow: 0 20px 60px rgba(0,0,0,0.3), 0 0 0 15px rgba(255, 193, 7, 0); }
            100% { box-shadow: 0 20px 60px rgba(0,0,0,0.3), 0 0 0 0 rgba(255, 193, 7, 0); }
        }
        
        .admin-message-header {
            background: linear-gradient(135deg, #f39c12 0%, #e67e22 100%);
            color: white;
            padding: 15px 25px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .admin-message-header .icon {
            font-size: 24px;
            animation: bellRing 2s infinite;
        }
        
        @keyframes bellRing {
            0%, 100% { transform: rotate(0); }
            10%, 30% { transform: rotate(-15deg); }
            20%, 40% { transform: rotate(15deg); }
            50% { transform: rotate(0); }
        }
        
        .admin-message-header h3 {
            margin: 0;
            font-size: 17px;
            font-weight: 600;
        }
        
        .admin-message-body {
            padding: 20px 25px;
            max-height: 300px;
            overflow-y: auto;
        }
        
        .admin-message-body::-webkit-scrollbar { width: 8px; }
        .admin-message-body::-webkit-scrollbar-track { background: rgba(255,255,255,0.3); border-radius: 4px; }
        .admin-message-body::-webkit-scrollbar-thumb { background: #f39c12; border-radius: 4px; }
        
        .admin-message-title {
            font-size: 15px;
            font-weight: 700;
            color: #856404;
            margin-bottom: 12px;
            padding-bottom: 10px;
            border-bottom: 2px dashed #ffc107;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        
        .admin-message-text {
            font-size: 14px;
            color: #856404;
            line-height: 2;
            text-align: justify;
            text-justify: inter-word;
        }
        
        .admin-message-text p {
            margin-bottom: 10px;
        }
        
        .admin-message-text strong {
            color: #e74c3c;
        }
        
        .admin-message-text ol,
        .admin-message-text ul {
            padding-right: 25px;
            margin: 10px 0;
        }
        
        .admin-message-text li {
            margin-bottom: 8px;
        }
        
        /* ============================================ */
        /* کارت تازه‌ها */
        /* ============================================ */
        .news-card {
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            overflow: hidden;
            flex: 1;
            display: flex;
            flex-direction: column;
        }
        
        .news-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 18px 25px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .news-header .icon { font-size: 24px; }
        
        .news-header h3 {
            margin: 0;
            font-size: 18px;
            font-weight: 600;
        }
        
        .news-body {
            padding: 20px 25px;
            overflow-y: auto;
            max-height: 500px;
            flex: 1;
        }
        
        .news-body::-webkit-scrollbar { width: 8px; }
        .news-body::-webkit-scrollbar-track { background: #f1f1f1; border-radius: 4px; }
        .news-body::-webkit-scrollbar-thumb { background: #667eea; border-radius: 4px; }
        
        .news-item {
            display: flex;
            gap: 15px;
            padding: 15px;
            border-radius: 12px;
            margin-bottom: 12px;
            background: #f8f9fa;
            border-right: 4px solid #667eea;
            transition: all 0.3s ease;
        }
        
        .news-item:hover {
            background: #e8f4fd;
            transform: translateX(-5px);
            box-shadow: 0 5px 15px rgba(102,126,234,0.15);
        }
        
        .news-item:last-child { margin-bottom: 0; }
        
        .news-item .news-icon {
            font-size: 30px;
            flex-shrink: 0;
            width: 45px;
            height: 45px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: white;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
        }
        
        .news-item .news-content { flex: 1; }
        
        .news-item .news-title {
            font-size: 14px;
            font-weight: 700;
            color: #2c3e50;
            margin-bottom: 6px;
        }
        
        .news-item .news-text {
            font-size: 13px;
            color: #6c757d;
            line-height: 1.7;
            text-align: justify;
        }
        
        .news-item .news-date {
            font-size: 11px;
            color: #adb5bd;
            margin-top: 6px;
            display: block;
        }
        
        .no-news {
            text-align: center;
            padding: 40px 20px;
            color: #adb5bd;
        }
        
        .no-news .icon {
            font-size: 50px;
            display: block;
            margin-bottom: 15px;
        }
        
        .no-news p { font-size: 14px; margin: 0; }
        
        /* ============================================ */
        /* ریسپانسیو */
        /* ============================================ */
        @media (max-width: 992px) {
            .main-container { flex-direction: column-reverse; }
            .login-column { flex: 0 0 100%; max-width: 100%; }
            .news-column { min-width: 100%; }
        }
        
        @media (max-width: 576px) {
            .login-inside { padding: 25px 20px; }
            .frm-title { font-size: 20px; }
            .title { font-size: 16px; }
            .news-header { padding: 15px 20px; }
            .news-header h3 { font-size: 16px; }
            .news-body { padding: 15px 18px; }
            .admin-message-header { padding: 12px 18px; }
            .admin-message-header h3 { font-size: 15px; }
            .admin-message-body { padding: 15px 18px; }
        }
        
        input[type="submit"], input[type="button"], button {
            -webkit-appearance: none;
            appearance: none;
        }
        
        .loading { position: relative; pointer-events: none; }
        
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
        
        @keyframes spin { to { transform: rotate(360deg); } }
    </style>

</head>

<body>

<div id="main-wrapper">
    <!-- پیام موفقیت تغییر رمز -->
<asp:Panel ID="pnlSuccessMessage" runat="server" Visible="false"
    style="background:#d4edda; color:#155724; padding:12px 18px; border-radius:10px; 
           margin-bottom:20px; border:2px solid #27ae60; text-align:center;">
    <span style="font-size:20px;">✅</span>
    <asp:Label ID="lblSuccessMessage" runat="server"
        style="font-weight:bold; font-size:14px;" />
</asp:Panel>
    <div class="main-container">
        
        <!-- ============================================ -->
        <!-- ستون راست - فرم لاگین -->
        <!-- ============================================ -->
        <div class="login-column">
            <form id="form1" runat="server" class="login-form" defaultbutton="Button1" defaultfocus="TextBox1">
                <div class="login-inside">
                    
                    <div id="divReturnInfo" runat="server" style="display:none;">
                        <div style="background:#d1ecf1; padding:12px 15px; border-radius:10px; margin-bottom:20px; text-align:center; border:2px solid #bee5eb;">
                            <span style="font-size:15px; color:#0c5460; font-weight:bold;">
                                📱 برای ثبت حضور در دوره آموزشی، لطفاً وارد شوید
                            </span>
                        </div>
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

                    <div class="divider">
                        <span>یا</span>
                    </div>

                    <div class="d-grid gap-2">
                        <a href="https://sso.tpco.ir/Sso/Portal?applicationName=MyTpc" class="sso-btn"> ورود با SSO</a>
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
                    
                    <asp:HiddenField ID="hdnReturnUrl" runat="server" />
                    
                </div>
            </form>
        </div>
        
        <!-- ============================================ -->
        <!-- ستون چپ - پیغام مدیر + تازه‌ها -->
        <!-- ============================================ -->
        <div class="news-column">
            
            <!-- ============================================ -->
            <!-- کارت پیغام مدیر (بالای تازه‌ها) -->
            <!-- ============================================ -->
            <asp:Panel ID="pnlAdminMessage" runat="server" Visible="false" CssClass="admin-message-card">
                <div class="admin-message-header">
                    <span class="icon">🔔</span>
                    <h3>پیغام مدیر سیستم</h3>
                </div>
                <div class="admin-message-body">
                    <div class="admin-message-title">
                        <span>📢</span>
                        <asp:Label ID="lblAdminMessageTitle" runat="server" />
                    </div>
                    <div class="admin-message-text">
                        <asp:Literal ID="litAdminMessage" runat="server" />
                    </div>
                </div>
            </asp:Panel>
            
            <!-- ============================================ -->
            <!-- کارت تازه‌ها -->
            <!-- ============================================ -->
            <div class="news-card">
                <div class="news-header">
                    <span class="icon">📢</span>
                    <h3>اطلاعیه‌ها و تازه‌ها</h3>
                </div>
                <div class="news-body">
                    <asp:Repeater ID="rptNews" runat="server">
                        <ItemTemplate>
                            <div class="news-item">
                                <div class="news-icon"><%# Eval("NewsIcon") %></div>
                                <div class="news-content">
                                    <div class="news-title"><%# Eval("NewsTitle") %></div>
                                    <div class="news-text"><%# Eval("NewsText") %></div>
                                    <span class="news-date">
                                        🕐 <%# Eval("PersianDate") %>
                                    </span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <asp:Panel ID="pnlNoNews" runat="server" Visible="false">
                        <div class="no-news">
                            <span class="icon">📭</span>
                            <p>هیچ اطلاعیه‌ای در حال حاضر وجود ندارد</p>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        
    </div>
    
</div>

<!-- ============================================ -->
<!-- اسکریپت‌های محلی -->
<!-- ============================================ -->
<script src="js/jquery-3.6.0.min.js"></script>
<script src="js/bootstrap.bundle.min.js"></script>
<script src="js/sweetalert2.all.min.js"></script>

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
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            // ====== بررسی پیام موفقیت ======
            var successMsg = '<%= If(Session("LoginSuccessMessage") IsNot Nothing, Session("LoginSuccessMessage").ToString().Replace("'", "\'"), "") %>';

        if (successMsg && successMsg.trim() !== '') {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '✅ موفق',
                    text: successMsg,
                    icon: 'success',
                    confirmButtonText: 'متوجه شدم',
                    confirmButtonColor: '#27ae60',
                    customClass: {
                        popup: 'swal-rtl'
                    }
                });
            } else {
                alert(successMsg);
            }
        }
    });
</script>

<%
    ' ====== پاکسازی پیام بعد از نمایش ======
    Session("LoginSuccessMessage") = Nothing
%>
</body>
</html>