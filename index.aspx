<%@ Page Title="داشبورد اصلی" Language="VB" MasterPageFile="~/dabir.master" 
    AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /* ============================================================
           ✅ استایل صفحه داشبورد — هماهنگ با مستر پیج
           ============================================================ */
        .dash-wrap { 
            direction: rtl;
            padding: 24px; /* ✅ پدینگ برای جلوگیری از چسبیدن کارت‌ها به لبه */
        }

        /* Welcome */
        .dash-welcome {
            background: linear-gradient(135deg, #0f766e 0%, #14b8a6 60%, #2dd4bf 100%);
            border-radius: 18px;
            padding: 30px 35px;
            color: #fff;
            position: relative;
            overflow: hidden;
            margin-bottom: 24px;
            box-shadow: 0 12px 30px rgba(15, 118, 110, 0.25);
        }
        .dash-welcome::before {
            content: '';
            position: absolute;
            top: -80px;
            left: -80px;
            width: 280px;
            height: 280px;
            background: radial-gradient(circle, rgba(255,255,255,0.18) 0%, transparent 70%);
            border-radius: 50%;
            pointer-events: none;
        }
        .dash-welcome::after {
            content: '';
            position: absolute;
            bottom: -100px;
            right: -50px;
            width: 320px;
            height: 320px;
            background: radial-gradient(circle, rgba(255,255,255,0.12) 0%, transparent 70%);
            border-radius: 50%;
            pointer-events: none;
        }
        .dash-welcome-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 20px;
            flex-wrap: wrap;
            position: relative;
            z-index: 1;
        }
        .dash-welcome-text { flex: 1; min-width: 260px; }
        .dash-welcome .greeting {
            font-size: 26px;
            font-weight: 800;
            margin: 0 0 8px 0;
            text-shadow: 0 2px 4px rgba(0,0,0,0.15);
        }
        .dash-welcome .subtitle {
            font-size: 14px;
            color: rgba(255,255,255,0.92);
            margin: 0;
            line-height: 1.9;
            font-weight: 500;
        }
        .dash-welcome .clock-box {
            background: rgba(255,255,255,0.15);
            backdrop-filter: blur(10px);
            -webkit-backdrop-filter: blur(10px);
            border: 1px solid rgba(255,255,255,0.25);
            border-radius: 14px;
            padding: 16px 22px;
            text-align: center;
            min-width: 200px;
        }
        .dash-welcome .clock-time {
            font-size: 32px;
            font-weight: 800;
            letter-spacing: 1px;
            font-variant-numeric: tabular-nums;
            margin-bottom: 4px;
            display: block;
        }
        .dash-welcome .clock-date {
            font-size: 12px;
            color: rgba(255,255,255,0.85);
            font-weight: 500;
        }

        /* Stats row */
        .dash-stats {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
            gap: 16px;
            margin-bottom: 24px;
        }
        .dash-stat {
            background: #fff;
            border-radius: 14px;
            padding: 20px 22px;
            box-shadow: 0 2px 12px rgba(15, 23, 42, 0.06);
            border: 1px solid #e2e8f0;
            display: flex;
            align-items: center;
            gap: 16px;
            transition: all 0.25s ease;
            position: relative;
            overflow: hidden;
        }
        .dash-stat::after {
            content: '';
            position: absolute;
            top: 0;
            right: 0;
            width: 4px;
            height: 100%;
            background: var(--stat-color, #0f766e);
            transition: width 0.25s;
        }
        .dash-stat:hover {
            transform: translateY(-4px);
            box-shadow: 0 12px 30px rgba(15, 23, 42, 0.12);
        }
        .dash-stat:hover::after { width: 6px; }
        .dash-stat-icon {
            width: 54px;
            height: 54px;
            border-radius: 14px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 26px;
            flex-shrink: 0;
            background: var(--stat-bg, #ccfbf1);
            color: var(--stat-color, #0f766e);
        }
        .dash-stat-body { flex: 1; min-width: 0; }
        .dash-stat-value {
            font-size: 26px;
            font-weight: 800;
            color: #0f172a;
            line-height: 1.1;
            margin: 0 0 4px 0;
        }
        .dash-stat-label {
            font-size: 12px;
            color: #64748b;
            font-weight: 600;
            margin: 0;
        }
        .dash-stat.red { --stat-color: #ef4444; --stat-bg: #fee2e2; }

        /* Section header */
        .dash-section-head {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 0 16px 0;
            padding-bottom: 10px;
            border-bottom: 2px solid #e2e8f0;
        }
        .dash-section-title {
            font-size: 17px;
            font-weight: 800;
            color: #0f172a;
            margin: 0;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .dash-section-title i {
            font-size: 22px;
            color: #0f766e;
        }

        /* Quick shortcuts */
        .dash-shortcuts {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
            gap: 14px;
            margin-bottom: 24px;
        }
        .dash-shortcut {
            background: #fff;
            border-radius: 14px;
            padding: 20px 16px;
            text-align: center;
            text-decoration: none !important;
            color: #1e293b;
            border: 1px solid #e2e8f0;
            box-shadow: 0 2px 8px rgba(15, 23, 42, 0.04);
            transition: all 0.25s ease;
            position: relative;
            overflow: hidden;
        }
        .dash-shortcut:hover {
            transform: translateY(-5px);
            border-color: #14b8a6;
            box-shadow: 0 12px 30px rgba(20, 184, 166, 0.18);
            color: #0f766e;
            text-decoration: none !important;
        }
        .dash-shortcut-icon {
            width: 56px;
            height: 56px;
            border-radius: 16px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 28px;
            margin-bottom: 12px;
            background: #f0fdfa;
            color: #0f766e;
            transition: all 0.25s;
        }
        .dash-shortcut:hover .dash-shortcut-icon {
            background: #ccfbf1;
            transform: scale(1.1) rotate(-5deg);
        }
        .dash-shortcut-title {
            font-size: 13.5px;
            font-weight: 700;
            margin: 0 0 4px 0;
            color: #1e293b;
        }
        .dash-shortcut-desc {
            font-size: 11.5px;
            color: #64748b;
            margin: 0;
            line-height: 1.5;
        }

        /* Two column layout */
        .dash-grid {
            display: grid;
            grid-template-columns: 2fr 1fr;
            gap: 20px;
            margin-bottom: 24px;
        }
        @media (max-width: 991px) {
            .dash-grid { grid-template-columns: 1fr; }
        }

        /* Card */
        .dash-card {
            background: #fff;
            border-radius: 14px;
            padding: 22px 24px;
            border: 1px solid #e2e8f0;
            box-shadow: 0 2px 12px rgba(15, 23, 42, 0.05);
        }
        .dash-card-head {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 16px;
            padding-bottom: 12px;
            border-bottom: 1px dashed #e2e8f0;
        }
        .dash-card-title {
            font-size: 15px;
            font-weight: 800;
            color: #0f172a;
            margin: 0;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .dash-card-title i {
            color: #0f766e;
            font-size: 20px;
        }
        .dash-card-badge {
            background: #fef3c7;
            color: #b45309;
            padding: 3px 10px;
            border-radius: 999px;
            font-size: 11px;
            font-weight: 700;
        }

        /* Announcements */
        .dash-notice {
            display: flex;
            align-items: flex-start;
            gap: 12px;
            padding: 12px 14px;
            border-radius: 10px;
            background: #f8fafc;
            margin-bottom: 10px;
            border-right: 4px solid #0f766e;
            transition: all 0.2s;
        }
        .dash-notice:hover {
            background: #f0fdfa;
            border-right-color: #14b8a6;
        }
        .dash-notice-icon {
            font-size: 22px;
            flex-shrink: 0;
            margin-top: 1px;
        }
        .dash-notice-body { flex: 1; min-width: 0; }
        .dash-notice-title {
            font-size: 13px;
            font-weight: 700;
            color: #1e293b;
            margin: 0 0 3px 0;
            line-height: 1.5;
        }
        .dash-notice-meta {
            font-size: 11px;
            color: #94a3b8;
            margin: 0;
        }
        .dash-notice.blue { border-right-color: #3b82f6; }
        .dash-notice.blue:hover { background: #eff6ff; border-right-color: #2563eb; }
        .dash-notice.red { border-right-color: #ef4444; }
        .dash-notice.red:hover { background: #fef2f2; border-right-color: #dc2626; }

        /* Support card */
        .dash-support {
            background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%);
            border-radius: 14px;
            padding: 24px;
            color: #fff;
            position: relative;
            overflow: hidden;
        }
        .dash-support::before {
            content: '';
            position: absolute;
            top: -50px;
            right: -50px;
            width: 180px;
            height: 180px;
            background: radial-gradient(circle, rgba(20, 184, 166, 0.3) 0%, transparent 70%);
            border-radius: 50%;
        }
        .dash-support-title {
            font-size: 16px;
            font-weight: 800;
            margin: 0 0 8px 0;
            position: relative;
            z-index: 1;
        }
        .dash-support-text {
            font-size: 12.5px;
            color: rgba(255,255,255,0.75);
            line-height: 1.9;
            margin: 0 0 16px 0;
            position: relative;
            z-index: 1;
        }
        .dash-support-actions {
            display: flex;
            gap: 8px;
            flex-wrap: wrap;
            position: relative;
            z-index: 1;
        }
        .dash-support-btn {
            padding: 9px 16px;
            border-radius: 10px;
            font-size: 12.5px;
            font-weight: 700;
            text-decoration: none !important;
            display: inline-flex;
            align-items: center;
            gap: 6px;
            transition: all 0.2s;
            border: none;
            cursor: pointer;
            font-family: inherit;
        }
        .dash-support-btn.primary {
            background: #14b8a6;
            color: #fff;
        }
        .dash-support-btn.primary:hover {
            background: #0f766e;
            color: #fff;
            transform: translateY(-2px);
        }
        .dash-support-btn.ghost {
            background: rgba(255,255,255,0.1);
            color: #fff;
            border: 1px solid rgba(255,255,255,0.2);
        }
        .dash-support-btn.ghost:hover {
            background: rgba(255,255,255,0.2);
            color: #fff;
            transform: translateY(-2px);
        }

        /* Tips */
        .dash-tip {
            background: #fffbeb;
            border: 1px solid #fde68a;
            border-radius: 12px;
            padding: 16px 18px;
            display: flex;
            align-items: flex-start;
            gap: 12px;
            color: #78350f;
            font-size: 12.5px;
            line-height: 1.9;
            margin-top: 24px;
        }
        .dash-tip i {
            font-size: 22px;
            color: #d97706;
            flex-shrink: 0;
        }
        .dash-tip strong { color: #92400e; }

        /* Welcome section spacing */
        .dash-section { margin-bottom: 24px; }

        /* Animation */
        @keyframes dashFadeUp {
            from { opacity: 0; transform: translateY(12px); }
            to   { opacity: 1; transform: translateY(0); }
        }
        .dash-anim { animation: dashFadeUp 0.45s ease both; }
        .dash-anim.d1 { animation-delay: 0.05s; }
        .dash-anim.d2 { animation-delay: 0.12s; }
        .dash-anim.d3 { animation-delay: 0.2s; }
        .dash-anim.d4 { animation-delay: 0.28s; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="dash-wrap">

        <%-- ============================================================
             ✅ خوش‌آمدگویی + ساعت زنده + تاریخ شمسی
             ============================================================ --%>
        <div class="dash-welcome dash-anim">
            <div class="dash-welcome-row">
                <div class="dash-welcome-text">
                    <h1 class="greeting">
                        👋 سلام، <asp:Label ID="lblUserName" runat="server" Text="" />
                    </h1>
                    <p class="subtitle">
                        به سامانه یکپارچه پتروشیمی تبریز خوش آمدید.<br />
                        از منوی سمت راست می‌توانید به تمام بخش‌های سیستم دسترسی داشته باشید.
                    </p>
                </div>
                <div class="clock-box">
                    <span id="liveClock" class="clock-time">00:00:00</span>
                    <span class="clock-date">
                        <asp:Label ID="lblDate" runat="server" /> <!-- ✅ تاریخ برگشت -->
                    </span>
                </div>
            </div>
        </div>

        <%-- ============================================================
             ✅ کارت‌های آماری (فقط اعلان‌های خوانده‌نشده)
             ============================================================ --%>
        <div class="dash-stats dash-anim d1">

            <a href="#" class="dash-stat red" style="text-decoration:none;">
                <div class="dash-stat-icon">
                    <i class="mdi mdi-bell-ring-outline"></i>
                </div>
                <div class="dash-stat-body">
                    <p class="dash-stat-value"><asp:Label ID="lblNotificationCount" runat="server" Text="0" /></p>
                    <p class="dash-stat-label">اعلان‌های خوانده‌نشده</p>
                </div>
            </a>

        </div>

        <%-- ============================================================
             ✅ دسترسی سریع
             ============================================================ --%>
        <div class="dash-section dash-anim d2">
            <div class="dash-section-head">
                <h2 class="dash-section-title">
                    <i class="mdi mdi-lightning-bolt"></i>
                    دسترسی سریع
                </h2>
            </div>
            <div class="dash-shortcuts">
                <a href="personelProfileFrm.aspx" class="dash-shortcut">
                    <div class="dash-shortcut-icon">
                        <i class="mdi mdi-account-box-outline"></i>
                    </div>
                    <h3 class="dash-shortcut-title">پروفایل شخصی</h3>
                    <p class="dash-shortcut-desc">مشاهده و ویرایش اطلاعات فردی</p>
                </a>

                <a href="frmPass.aspx" class="dash-shortcut">
                    <div class="dash-shortcut-icon">
                        <i class="mdi mdi-lock-reset"></i>
                    </div>
                    <h3 class="dash-shortcut-title">تغییر رمز عبور</h3>
                    <p class="dash-shortcut-desc">به‌روزرسانی رمز ورود به سیستم</p>
                </a>

                <a href="frmChngPic.aspx" class="dash-shortcut">
                    <div class="dash-shortcut-icon">
                        <i class="mdi mdi-image-edit-outline"></i>
                    </div>
                    <h3 class="dash-shortcut-title">تغییر تصویر پروفایل</h3>
                    <p class="dash-shortcut-desc">بارگذاری عکس جدید برای پروفایل</p>
                </a>
            </div>
        </div>

        <%-- ============================================================
             ✅ دو ستون: اطلاعیه‌ها + پشتیبانی
             ============================================================ --%>
        <div class="dash-grid dash-anim d3">

            <%-- اطلاعیه‌ها --%>
            <div class="dash-card">
                <div class="dash-card-head">
                    <h2 class="dash-card-title">
                        <i class="mdi mdi-bullhorn-variant-outline"></i>
                        اطلاعیه‌های اخیر
                    </h2>
                    <span class="dash-card-badge">جدید</span>
                </div>

           

                <div class="dash-notice red">
                    <span class="dash-notice-icon">⚠️</span>
                    <div class="dash-notice-body">
                        <p class="dash-notice-title">
                            از به اشتراک‌گذاری رمز عبور خود با دیگران خودداری کنید.
                        </p>
                        <p class="dash-notice-meta">۲ هفته پیش · امنیت اطلاعات</p>
                    </div>
                </div>

            </div>

            <%-- پشتیبانی + راهنما --%>
            <div>
                <div class="dash-support">
                    <h3 class="dash-support-title">🎧 نیاز به کمک دارید؟</h3>
                    <p class="dash-support-text">
                        تیم پشتیبانی پتروشیمی تبریز در ساعات اداری آماده پاسخگویی به سوالات شماست.
                    </p>
                    <div class="dash-support-actions">
                        <a href="tel:04133286380" class="dash-support-btn primary">
                            <i class="mdi mdi-phone"></i> تماس با پشتیبانی
                        </a>
                        <a href="#" class="dash-support-btn ghost">
                            <i class="mdi mdi-email-outline"></i> ارسال ایمیل
                        </a>
                    </div>
                </div>

                <div class="dash-tip">
                    <i class="mdi mdi-lightbulb-on-outline"></i>
                    <div>
                        <strong>نکته:</strong> برای دسترسی سریع‌تر به بخش‌های پرکاربرد، از منوی کناری استفاده کنید. تمام منوها بر اساس سطح دسترسی شما نمایش داده می‌شوند.
                    </div>
                </div>
            </div>

        </div>

    </div>

    <script type="text/javascript">
        // ============================================================
        // ✅ ساعت زنده در کارت خوش‌آمدگویی
        // ============================================================
        (function () {
            function pad(n) { return n < 10 ? '0' + n : '' + n; }

            function updateClock() {
                var el = document.getElementById('liveClock');
                if (!el) return;
                var now = new Date();
                el.textContent = pad(now.getHours()) + ':' + pad(now.getMinutes()) + ':' + pad(now.getSeconds());
            }

            updateClock();
            setInterval(updateClock, 1000);
        })();
    </script>

</asp:Content>