<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login_2.aspx.vb" Inherits="light_login3" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>

<html lang="fa" dir="rtl">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="description" content="سامانه پتروشیمی من">
    <meta name="author" content="Petrochimi Tabriz">
    <link rel="icon" type="image/png" href="images/Logo-site.png" />
    <title>ورود به سامانه | پتروشیمی من</title>

    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/fontawesome.min.css">
    <link rel="stylesheet" href="css/sweetalert2.min.css" />

    <style type="text/css">
        :root {
            --lp-primary: #0f766e;
            --lp-primary-dark: #115e59;
            --lp-primary-light: #14b8a6;
            --lp-accent: #2dd4bf;
            --lp-bg: #f1f5f9;
            --lp-text: #1e293b;
            --lp-text-muted: #64748b;
            --lp-border: #e2e8f0;
            --lp-radius: 18px;
            --lp-shadow: 0 10px 40px rgba(15, 23, 42, 0.08);
            --lp-shadow-lg: 0 20px 60px rgba(15, 23, 42, 0.15);
        }

        * { margin: 0; padding: 0; box-sizing: border-box; }

        body {
            font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
            min-height: 100vh;
            background: #f1f5f9;
            position: relative;
            overflow-x: hidden;
        }
        /* پس‌زمینه تزئینی */
        body::before {
            content: '';
            position: fixed;
            top: -40%;
            right: -20%;
            width: 900px;
            height: 900px;
            background: radial-gradient(circle, rgba(20, 184, 166, 0.18) 0%, transparent 60%);
            border-radius: 50%;
            pointer-events: none;
            z-index: 0;
        }
        body::after {
            content: '';
            position: fixed;
            bottom: -30%;
            left: -15%;
            width: 700px;
            height: 700px;
            background: radial-gradient(circle, rgba(15, 118, 110, 0.12) 0%, transparent 60%);
            border-radius: 50%;
            pointer-events: none;
            z-index: 0;
        }

        #main-wrapper {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 30px 20px;
            position: relative;
            z-index: 1;
        }

        .main-container {
            display: flex;
            gap: 28px;
            max-width: 1180px;
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
            background: #fff;
            border-radius: var(--lp-radius);
            box-shadow: var(--lp-shadow-lg);
            overflow: hidden;
            transition: transform 0.3s ease;
            border: 1px solid var(--lp-border);
            position: relative;
        }
        .login-column::before {
            content: '';
            position: absolute;
            top: 0; left: 0; right: 0;
            height: 5px;
            background: linear-gradient(90deg, #0f766e, #14b8a6, #2dd4bf);
        }

        .login-column:hover { transform: translateY(-5px); }

        .login-inside {
            padding: 38px 30px 32px;
            animation: fadeInUp 0.6s ease;
        }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(24px); }
            to   { opacity: 1; transform: translateY(0); }
        }

        .brand-chip {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            background: #f0fdfa;
            color: var(--lp-primary-dark);
            padding: 6px 14px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 700;
            border: 1px solid #99f6e4;
            margin-bottom: 18px;
        }

        .frm-title {
            font-size: 24px;
            font-weight: 800;
            color: #0f172a;
            text-align: center;
            margin: 4px 0 6px;
            position: relative;
        }
        .frm-subtitle {
            font-size: 13px;
            color: var(--lp-text-muted);
            text-align: center;
            margin: 0 0 26px;
            line-height: 1.7;
            font-weight: 500;
        }

        .frm-input { margin-bottom: 18px; position: relative; }

        .frm-inp {
            width: 100%;
            padding: 13px 44px 13px 15px;
            border: 2px solid var(--lp-border);
            border-radius: 12px;
            font-size: 14px;
            transition: all 0.25s ease;
            background: #f8fafc;
            font-family: inherit;
            text-align: right;
            color: var(--lp-text);
        }
        .frm-inp::placeholder { color: #94a3b8; }
        .frm-inp:focus {
            border-color: var(--lp-primary-light);
            outline: none;
            box-shadow: 0 0 0 4px rgba(20, 184, 166, 0.15);
            background: #fff;
        }
        .frm-inp:hover { border-color: #94a3b8; }

        .input-icon .icon-emoji {
            position: absolute;
            right: 14px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 17px;
            z-index: 1;
            opacity: 0.75;
        }

        .frm-submit {
            width: 100%;
            padding: 14px 20px;
            background: linear-gradient(135deg, #0f766e 0%, #14b8a6 100%);
            color: #fff !important;
            border: none;
            border-radius: 12px;
            font-size: 15px;
            font-weight: 800;
            cursor: pointer;
            transition: all 0.25s ease;
            margin-top: 6px;
            display: block;
            text-align: center;
            box-shadow: 0 8px 20px rgba(15, 118, 110, 0.25);
        }
        .frm-submit:hover {
            transform: translateY(-2px);
            box-shadow: 0 12px 30px rgba(15, 118, 110, 0.35);
            background: linear-gradient(135deg, #115e59 0%, #0f766e 100%);
            color: #fff !important;
        }
        .frm-submit:disabled { opacity: 0.6; cursor: not-allowed; transform: none; }

        .sso-btn {
            width: 100%;
            padding: 13px 20px;
            background: #f1f5f9;
            color: var(--lp-primary-dark) !important;
            border: 2px solid #cbd5e1;
            border-radius: 12px;
            font-size: 14px;
            font-weight: 700;
            cursor: pointer;
            transition: all 0.25s ease;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            text-decoration: none !important;
        }
        .sso-btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 20px rgba(15, 23, 42, 0.1);
            background: #fff;
            border-color: var(--lp-primary-light);
            color: var(--lp-primary) !important;
        }

        .divider {
            display: flex;
            align-items: center;
            text-align: center;
            margin: 22px 0;
        }
        .divider::before, .divider::after {
            content: '';
            flex: 1;
            border-bottom: 1px solid var(--lp-border);
        }
        .divider::before { margin-left: 12px; }
        .divider::after { margin-right: 12px; }
        .divider span { color: #94a3b8; font-size: 12px; white-space: nowrap; font-weight: 600; }

        .forgot-link { text-align: center; margin-top: 18px; }
        .forgot-link a {
            color: var(--lp-primary);
            text-decoration: none;
            font-size: 13px;
            font-weight: 600;
            transition: all 0.2s ease;
            display: inline-flex;
            align-items: center;
            gap: 5px;
        }
        .forgot-link a:hover {
            color: var(--lp-primary-dark);
            text-decoration: none;
            gap: 8px;
        }

        .frm-footer {
            text-align: center;
            margin-top: 24px;
            padding-top: 18px;
            border-top: 1px dashed var(--lp-border);
        }
        .frm-footer h5 {
            color: #64748b;
            font-size: 12px;
            margin: 0;
            font-weight: 500;
            line-height: 1.7;
        }
        .frm-footer h5 strong { color: var(--lp-primary); }

        .validation-error {
            color: #ef4444;
            font-size: 11.5px;
            display: block;
            margin-top: 5px;
            text-align: right;
            font-weight: 600;
        }

        /* ============================================ */
        /* پیام‌ها */
        /* ============================================ */
        .lp-alert {
            padding: 12px 16px;
            border-radius: 10px;
            margin-bottom: 18px;
            display: flex;
            align-items: flex-start;
            gap: 10px;
            font-size: 13px;
            line-height: 1.8;
            border: 1px solid;
            font-weight: 600;
        }
        .lp-alert.success { background: #dcfce7; color: #166534; border-color: #86efac; }
        .lp-alert.info    { background: #dbeafe; color: #1e40af; border-color: #93c5fd; }
        .lp-alert.warning { background: #fef3c7; color: #92400e; border-color: #fcd34d; }
        .lp-alert.error   { background: #fee2e2; color: #991b1b; border-color: #fca5a5; }
        .lp-alert .icon { font-size: 18px; flex-shrink: 0; margin-top: 1px; }

        /* ============================================ */
        /* ستون چپ - اطلاعیه‌ها و تازه‌ها */
        /* ============================================ */
        .news-column {
            flex: 1;
            min-width: 400px;
            display: flex;
            flex-direction: column;
            gap: 22px;
        }

        /* ============================================ */
        /* کارت پیغام مدیر */
        /* ============================================ */
        .admin-message-card {
            background: linear-gradient(135deg, #fffbeb 0%, #fef3c7 100%);
            border-radius: var(--lp-radius);
            box-shadow: var(--lp-shadow-lg);
            overflow: hidden;
            border: 1px solid #fde68a;
            position: relative;
            animation: pulseGlow 2s infinite;
        }
        @keyframes pulseGlow {
            0%   { box-shadow: var(--lp-shadow-lg), 0 0 0 0 rgba(245, 158, 11, 0.35); }
            70%  { box-shadow: var(--lp-shadow-lg), 0 0 0 14px rgba(245, 158, 11, 0); }
            100% { box-shadow: var(--lp-shadow-lg), 0 0 0 0 rgba(245, 158, 11, 0); }
        }

        .admin-message-header {
            background: linear-gradient(135deg, #f59e0b 0%, #d97706 100%);
            color: #fff;
            padding: 15px 22px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .admin-message-header .icon {
            font-size: 22px;
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
            font-size: 16px;
            font-weight: 800;
        }

        .admin-message-body {
            padding: 18px 22px;
            max-height: 280px;
            overflow-y: auto;
            background: #fff;
        }
        .admin-message-body::-webkit-scrollbar { width: 6px; }
        .admin-message-body::-webkit-scrollbar-track { background: #f8fafc; border-radius: 3px; }
        .admin-message-body::-webkit-scrollbar-thumb { background: #f59e0b; border-radius: 3px; }

        .admin-message-title {
            font-size: 14px;
            font-weight: 800;
            color: #92400e;
            margin-bottom: 12px;
            padding-bottom: 10px;
            border-bottom: 2px dashed #fcd34d;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .admin-message-text {
            font-size: 13.5px;
            color: #78350f;
            line-height: 2;
            text-align: justify;
            text-justify: inter-word;
        }
        .admin-message-text p { margin-bottom: 10px; }
        .admin-message-text strong { color: #dc2626; }
        .admin-message-text ol,
        .admin-message-text ul {
            padding-right: 24px;
            margin: 10px 0;
        }
        .admin-message-text li { margin-bottom: 8px; }

        /* ============================================ */
        /* کارت تازه‌ها */
        /* ============================================ */
        .news-card {
            background: #fff;
            border-radius: var(--lp-radius);
            box-shadow: var(--lp-shadow-lg);
            overflow: hidden;
            flex: 1;
            display: flex;
            flex-direction: column;
            border: 1px solid var(--lp-border);
        }

        .news-header {
            background: linear-gradient(135deg, #0f766e 0%, #14b8a6 100%);
            color: #fff;
            padding: 18px 22px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .news-header .icon { font-size: 22px; }
        .news-header h3 {
            margin: 0;
            font-size: 17px;
            font-weight: 800;
        }

        .news-body {
            padding: 20px 22px;
            overflow-y: auto;
            max-height: 500px;
            flex: 1;
        }
        .news-body::-webkit-scrollbar { width: 6px; }
        .news-body::-webkit-scrollbar-track { background: #f1f5f9; border-radius: 3px; }
        .news-body::-webkit-scrollbar-thumb { background: #14b8a6; border-radius: 3px; }

        .news-item {
            display: flex;
            gap: 14px;
            padding: 14px;
            border-radius: 12px;
            margin-bottom: 12px;
            background: #f8fafc;
            border-right: 4px solid #14b8a6;
            transition: all 0.25s ease;
        }
        .news-item:hover {
            background: #f0fdfa;
            transform: translateX(-5px);
            box-shadow: 0 8px 20px rgba(20, 184, 166, 0.12);
        }
        .news-item:last-child { margin-bottom: 0; }

        .news-item .news-icon {
            font-size: 26px;
            flex-shrink: 0;
            width: 44px;
            height: 44px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.06);
        }
        .news-item .news-content { flex: 1; min-width: 0; }
        .news-item .news-title {
            font-size: 13.5px;
            font-weight: 800;
            color: #0f172a;
            margin-bottom: 5px;
            line-height: 1.6;
        }
        .news-item .news-text {
            font-size: 12.5px;
            color: #64748b;
            line-height: 1.8;
            text-align: justify;
        }
        .news-item .news-date {
            font-size: 11px;
            color: #94a3b8;
            margin-top: 6px;
            display: block;
            font-weight: 600;
        }

        .no-news {
            text-align: center;
            padding: 40px 20px;
            color: #94a3b8;
        }
        .no-news .icon {
            font-size: 48px;
            display: block;
            margin-bottom: 12px;
            opacity: 0.6;
        }
        .no-news p { font-size: 13px; margin: 0; font-weight: 600; }

        /* ============================================ */
        /* لودینگ */
        /* ============================================ */
        .loading { position: relative; pointer-events: none; }
        .loading::after {
            content: '';
            position: absolute;
            width: 18px;
            height: 18px;
            top: 50%;
            left: 20px;
            margin-top: -9px;
            border: 2px solid #fff;
            border-radius: 50%;
            border-top-color: transparent;
            animation: spin 0.6s linear infinite;
        }
        @keyframes spin { to { transform: rotate(360deg); } }

        /* ============================================ */
        /* ریسپانسیو */
        /* ============================================ */
        @media (max-width: 992px) {
            .main-container { flex-direction: column-reverse; }
            .login-column { flex: 0 0 100%; max-width: 100%; }
            .news-column { min-width: 100%; }
        }
        @media (max-width: 576px) {
            #main-wrapper { padding: 15px; }
            .login-inside { padding: 30px 22px 26px; }
            .frm-title { font-size: 20px; }
            .news-header { padding: 15px 18px; }
            .news-header h3 { font-size: 15px; }
            .news-body { padding: 15px 16px; }
            .admin-message-header { padding: 13px 18px; }
            .admin-message-header h3 { font-size: 14px; }
            .admin-message-body { padding: 14px 16px; }
        }

        input[type="submit"], input[type="button"], button {
            -webkit-appearance: none;
            appearance: none;
        }
    </style>

</head>

<body>

<div id="main-wrapper">
    <div class="main-container">

        <!-- ============================================ -->
        <!-- ستون راست - فرم لاگین -->
        <!-- ============================================ -->
        <div class="login-column">
            <form id="form1" runat="server" class="login-form" defaultbutton="Button1" defaultfocus="TextBox1">
                <div class="login-inside">

                    <!-- پیام موفقیت تغییر رمز -->
                    <asp:Panel ID="pnlSuccessMessage" runat="server" Visible="false" CssClass="lp-alert success">
                        <span class="icon">✅</span>
                        <asp:Label ID="lblSuccessMessage" runat="server" />
                    </asp:Panel>

                    <div id="divReturnInfo" runat="server" style="display:none;">
                        <div class="lp-alert info">
                            <span class="icon">📱</span>
                            <span>برای ثبت حضور در دوره آموزشی، لطفاً وارد شوید</span>
                        </div>
                    </div>

                    <div class="brand-chip">
                        🏭 شرکت پتروشیمی تبریز
                    </div>

                    <div class="frm-title">ورود به سامانه</div>
                    <p class="frm-subtitle">برای ادامه، کد پرسنلی و رمز عبور خود را وارد کنید</p>

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

                    <div class="d-grid gap-2 mt-3">
                        <asp:Button ID="Button1" runat="server" Text="ورود به سامانه"
                            CssClass="frm-submit" OnClick="Button1_Click" ValidationGroup="1" />
                    </div>

                    <div class="divider">
                        <span>یا</span>
                    </div>

                    <div class="d-grid gap-2">
                        <a href="https://sso.tpco.ir/Sso/Portal?applicationName=MyTpc" class="sso-btn">
                            <i class="fa fa-sign-in-alt"></i>
                            ورود با SSO
                        </a>
                    </div>

                    <div class="forgot-link">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgotPass.aspx">
                            <span class="icon-emoji">🔑</span> رمز عبور خود را فراموش کرده‌اید؟
                        </asp:HyperLink>
                    </div>

                    <div class="frm-footer">
                        <h5>
                            📞 نیاز به راهنمایی دارید؟ با شماره <strong>33286380</strong> تماس بگیرید
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

            <!-- کارت پیغام مدیر -->
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

            <!-- کارت تازه‌ها -->
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

<!-- ============================================ -->
<!-- اسکریپت پیام موفقیت -->
<!-- ============================================ -->
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        var successMsg = '<%= If(Session("LoginSuccessMessage") IsNot Nothing, Session("LoginSuccessMessage").ToString().Replace("'", "\'"), "") %>';

        if (successMsg && successMsg.trim() !== '') {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '✅ موفق',
                    text: successMsg,
                    icon: 'success',
                    confirmButtonText: 'متوجه شدم',
                    confirmButtonColor: '#0f766e',
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