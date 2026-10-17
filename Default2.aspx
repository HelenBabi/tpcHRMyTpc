<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .fp-container {
            max-width: 500px;
            margin: 40px auto;
            padding: 0;
            background: #ffffff;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.15);
            overflow: hidden;
            direction: rtl;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        /* ============================================ */
        /* هدر */
        /* ============================================ */
        .fp-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px 25px;
            text-align: center;
            position: relative;
        }
        
        .fp-header .icon-circle {
            width: 70px;
            height: 70px;
            background: rgba(255,255,255,0.2);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 15px;
            font-size: 35px;
            animation: pulse 2s infinite;
        }
        
        @keyframes pulse {
            0% { transform: scale(1); box-shadow: 0 0 0 0 rgba(255,255,255,0.4); }
            70% { transform: scale(1.05); box-shadow: 0 0 0 15px rgba(255,255,255,0); }
            100% { transform: scale(1); box-shadow: 0 0 0 0 rgba(255,255,255,0); }
        }
        
        .fp-header h2 {
            margin: 0;
            font-size: 22px;
            font-weight: 700;
        }
        
        .fp-header p {
            margin: 8px 0 0;
            font-size: 13px;
            opacity: 0.9;
        }
        
        /* ============================================ */
        /* مراحل */
        /* ============================================ */
        .fp-steps {
            display: flex;
            justify-content: center;
            gap: 0;
            padding: 20px 25px 0;
            background: #f8f9fa;
        }
        
        .fp-step {
            flex: 1;
            text-align: center;
            position: relative;
            padding-bottom: 15px;
        }
        
        .fp-step::before {
            content: '';
            position: absolute;
            top: 18px;
            right: 50%;
            width: 100%;
            height: 2px;
            background: #e0e0e0;
            z-index: 0;
        }
        
        .fp-step:first-child::before {
            display: none;
        }
        
        .fp-step.active::before,
        .fp-step.completed::before {
            background: #667eea;
        }
        
        .fp-step .step-circle {
            width: 36px;
            height: 36px;
            border-radius: 50%;
            background: #e0e0e0;
            color: #999;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 8px;
            font-weight: 700;
            font-size: 14px;
            position: relative;
            z-index: 1;
            transition: all 0.3s;
        }
        
        .fp-step.active .step-circle {
            background: #667eea;
            color: white;
            box-shadow: 0 0 0 5px rgba(102,126,234,0.2);
        }
        
        .fp-step.completed .step-circle {
            background: #27ae60;
            color: white;
        }
        
        .fp-step .step-label {
            font-size: 11px;
            color: #999;
            font-weight: 600;
        }
        
        .fp-step.active .step-label {
            color: #667eea;
        }
        
        .fp-step.completed .step-label {
            color: #27ae60;
        }
        
        /* ============================================ */
        /* بدنه فرم */
        /* ============================================ */
        .fp-body {
            padding: 30px 35px 35px;
        }
        
        .fp-panel {
            animation: fadeIn 0.4s ease;
        }
        
        @keyframes fadeIn {
            from { opacity: 0; transform: translateX(-20px); }
            to { opacity: 1; transform: translateX(0); }
        }
        
        .fp-form-group {
            margin-bottom: 20px;
        }
        
        .fp-form-group label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 8px;
            font-size: 14px;
        }
        
        .fp-form-group .input-icon {
            position: relative;
        }
        
        .fp-form-group .input-icon .icon-emoji {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 18px;
            z-index: 1;
        }
        
        .fp-inp {
            width: 100%;
            padding: 12px 45px 12px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            font-size: 14px;
            font-family: inherit;
            transition: all 0.3s;
            background: #f8f9fa;
            box-sizing: border-box;
            text-align: right;
        }
        
        .fp-inp:focus {
            border-color: #667eea;
            outline: none;
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
            background: white;
        }
        
        .fp-inp.code-input {
            text-align: center;
            font-size: 24px;
            letter-spacing: 10px;
            font-weight: 700;
            direction: ltr;
            padding-right: 15px;
        }
        
        .fp-inp.code-input::placeholder {
            letter-spacing: 2px;
            font-size: 14px;
            font-weight: normal;
        }
        
        .fp-btn {
            width: 100%;
            padding: 14px 20px;
            border: none;
            border-radius: 12px;
            font-size: 15px;
            font-weight: 700;
            cursor: pointer;
            transition: all 0.3s;
            font-family: inherit;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
        }
        
        .fp-btn-primary {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }
        
        .fp-btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(102,126,234,0.3);
        }
        
        .fp-btn-primary:disabled {
            opacity: 0.6;
            cursor: not-allowed;
            transform: none;
        }
        
        .fp-btn-success {
            background: linear-gradient(135deg, #27ae60 0%, #219a52 100%);
            color: white;
        }
        
        .fp-btn-success:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(39,174,96,0.3);
        }
        
        .fp-btn-outline {
            background: white;
            color: #667eea;
            border: 2px solid #667eea;
        }
        
        .fp-btn-outline:hover {
            background: #667eea;
            color: white;
        }
        
        /* ============================================ */
        /* هشدارها */
        /* ============================================ */
        .fp-alert {
            padding: 14px 18px;
            border-radius: 12px;
            margin-bottom: 20px;
            font-size: 13px;
            display: flex;
            align-items: flex-start;
            gap: 10px;
            line-height: 1.7;
        }
        
        .fp-alert-info {
            background: #d1ecf1;
            color: #0c5460;
            border-right: 4px solid #17a2b8;
        }
        
        .fp-alert-success {
            background: #d4edda;
            color: #155724;
            border-right: 4px solid #27ae60;
        }
        
        .fp-alert-danger {
            background: #f8d7da;
            color: #721c24;
            border-right: 4px solid #e74c3c;
        }
        
        .fp-alert-warning {
            background: #fff3cd;
            color: #856404;
            border-right: 4px solid #f39c12;
        }
        
        .fp-alert .alert-icon {
            font-size: 20px;
            flex-shrink: 0;
        }
        
        /* ============================================ */
        /* تایمر */
        /* ============================================ */
        .fp-timer {
            text-align: center;
            margin: 15px 0;
            padding: 12px;
            background: #f8f9fa;
            border-radius: 10px;
            border: 1px dashed #ddd;
        }
        
        .fp-timer .timer-text {
            font-size: 13px;
            color: #6c757d;
        }
        
        .fp-timer .timer-count {
            font-size: 22px;
            font-weight: 700;
            color: #e74c3c;
            direction: ltr;
            display: inline-block;
            margin-right: 5px;
        }
        
        .fp-timer.expired .timer-count {
            color: #999;
        }
        
        /* ============================================ */
        /* اطلاعات موبایل */
        /* ============================================ */
        .fp-mobile-info {
            background: #e8f4fd;
            border: 2px solid #3498db;
            border-radius: 12px;
            padding: 15px 20px;
            text-align: center;
            margin-bottom: 20px;
        }
        
        .fp-mobile-info .icon {
            font-size: 30px;
            display: block;
            margin-bottom: 8px;
        }
        
        .fp-mobile-info .title {
            font-size: 13px;
            color: #0c5460;
            font-weight: 600;
            margin-bottom: 5px;
        }
        
        .fp-mobile-info .number {
            font-size: 20px;
            font-weight: 700;
            color: #2c3e50;
            direction: ltr;
            letter-spacing: 2px;
            font-family: 'Courier New', monospace;
        }
        
        /* ============================================ */
        /* لینک‌ها */
        /* ============================================ */
        .fp-links {
            text-align: center;
            margin-top: 25px;
            padding-top: 20px;
            border-top: 1px solid #e9ecef;
        }
        
        .fp-links a {
            color: #667eea;
            text-decoration: none;
            font-size: 13px;
            margin: 0 10px;
            transition: all 0.3s;
        }
        
        .fp-links a:hover {
            color: #764ba2;
            text-decoration: underline;
        }
        
        /* ============================================ */
        /* لودینگ */
        /* ============================================ */
        .fp-loading {
            display: none;
            text-align: center;
            padding: 20px;
        }
        
        .fp-loading.show {
            display: block;
        }
        
        .fp-loading .spinner {
            width: 40px;
            height: 40px;
            border: 4px solid #e0e0e0;
            border-top-color: #667eea;
            border-radius: 50%;
            animation: spin 0.8s linear infinite;
            margin: 0 auto 10px;
        }
        
        @keyframes spin {
            to { transform: rotate(360deg); }
        }
        
        .fp-loading p {
            font-size: 13px;
            color: #6c757d;
            margin: 0;
        }
        
        /* ============================================ */
        /* ریسپانسیو */
        /* ============================================ */
        @media (max-width: 576px) {
            .fp-container {
                margin: 20px 10px;
                border-radius: 15px;
            }
            
            .fp-header {
                padding: 25px 20px;
            }
            
            .fp-header .icon-circle {
                width: 60px;
                height: 60px;
                font-size: 30px;
            }
            
            .fp-header h2 {
                font-size: 18px;
            }
            
            .fp-body {
                padding: 25px 20px;
            }
            
            .fp-step .step-label {
                font-size: 10px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="fp-container">
        
        <!-- ============================================ -->
        <!-- هدر -->
        <!-- ============================================ -->
        <div class="fp-header">
            <div class="icon-circle">🔐</div>
            <h2>بازیابی رمز عبور</h2>
            <p>سامانه پتروشیمی من</p>
        </div>
        
        <!-- ============================================ -->
        <!-- مراحل -->
        <!-- ============================================ -->
        <div class="fp-steps">
            <div id="step1" runat="server" class="fp-step active">
                <div class="step-circle">۱</div>
                <div class="step-label">کد پرسنلی</div>
            </div>
            <div id="step2" runat="server" class="fp-step">
                <div class="step-circle">۲</div>
                <div class="step-label">کد تایید</div>
            </div>
            <div id="step3" runat="server" class="fp-step">
                <div class="step-circle">۳</div>
                <div class="step-label">رمز جدید</div>
            </div>
        </div>
        
        <!-- ============================================ -->
        <!-- بدنه -->
        <!-- ============================================ -->
        <div class="fp-body">
            
            <!-- پیام -->
            <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                <div id="divMessage" runat="server" class="fp-alert">
                    <span class="alert-icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                    <asp:Label ID="lblMessage" runat="server" />
                </div>
            </asp:Panel>
            
            <!-- ============================================ -->
            <!-- مرحله 1: کد پرسنلی -->
            <!-- ============================================ -->
            <asp:Panel ID="pnlStep1" runat="server" CssClass="fp-panel">
                <div class="fp-alert fp-alert-info">
                    <span class="alert-icon">ℹ️</span>
                    <div>
                        برای بازیابی رمز عبور، ابتدا <strong>کد پرسنلی</strong> خود را وارد کنید.
                        کد تایید به شماره موبایل ثبت‌شده در سامانه ارسال خواهد شد.
                    </div>
                </div>
                
                <div class="fp-form-group">
                    <label>کد پرسنلی:</label>
                    <div class="input-icon">
                        <span class="icon-emoji">🆔</span>
                        <asp:TextBox ID="txtPersonCode" runat="server" CssClass="fp-inp" 
                            placeholder="کد پرسنلی خود را وارد کنید" 
                            TextMode="Number" MaxLength="20" />
                    </div>
                </div>
                
                <asp:Button ID="btnSendCode" runat="server" Text="📱 ارسال کد تایید" 
                    CssClass="fp-btn fp-btn-primary" OnClick="btnSendCode_Click" />
            </asp:Panel>
            
            <!-- ============================================ -->
            <!-- مرحله 2: کد تایید -->
            <!-- ============================================ -->
            <asp:Panel ID="pnlStep2" runat="server" CssClass="fp-panel" Visible="false">
                
                <div class="fp-mobile-info">
                    <span class="icon">📱</span>
                    <div class="title">کد تایید ارسال شد به شماره:</div>
                    <div class="number"><asp:Label ID="lblMaskedMobile" runat="server" /></div>
                </div>
                
                <div class="fp-form-group">
                    <label>کد تایید 5 رقمی:</label>
                    <asp:TextBox ID="txtVerifyCode" runat="server" CssClass="fp-inp code-input" 
                        placeholder="کد 5 رقمی" 
                        MaxLength="5" TextMode="Number" />
                </div>
                
                <!-- تایمر -->
                <div id="divTimer" class="fp-timer">
                    <span class="timer-text">⏰ زمان باقیمانده:</span>
                    <span class="timer-count" id="timerCount">02:00</span>
                </div>
                
                <div style="display: flex; gap: 10px;">
                    <asp:Button ID="btnVerifyCode" runat="server" Text="✅ تایید کد" 
                        CssClass="fp-btn fp-btn-success" OnClick="btnVerifyCode_Click" 
                        style="flex: 2;" />
                    
                    <asp:Button ID="btnResendCode" runat="server" Text="🔄 ارسال مجدد" 
                        CssClass="fp-btn fp-btn-outline" OnClick="btnResendCode_Click" 
                        CausesValidation="false"
                        style="flex: 1;" />
                </div>
            </asp:Panel>
            
            <!-- ============================================ -->
            <!-- مرحله 3: رمز جدید -->
            <!-- ============================================ -->
            <asp:Panel ID="pnlStep3" runat="server" CssClass="fp-panel" Visible="false">
                
                <div class="fp-alert fp-alert-success">
                    <span class="alert-icon">✅</span>
                    <div>
                        هویت شما تایید شد. لطفاً <strong>رمز عبور جدید</strong> خود را وارد کنید.
                    </div>
                </div>
                
                <div class="fp-form-group">
                    <label>رمز عبور جدید:</label>
                    <div class="input-icon">
                        <span class="icon-emoji">🔒</span>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="fp-inp" 
                            TextMode="Password" 
                            placeholder="رمز جدید (حداقل 4 کاراکتر)" />
                    </div>
                </div>
                
                <div class="fp-form-group">
                    <label>تکرار رمز عبور جدید:</label>
                    <div class="input-icon">
                        <span class="icon-emoji">🔐</span>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="fp-inp" 
                            TextMode="Password" 
                            placeholder="تکرار رمز جدید" />
                    </div>
                </div>
                
                <div class="fp-alert fp-alert-warning">
                    <span class="alert-icon">⚠️</span>
                    <div style="font-size:12px;">
                        رمز عبور باید حداقل <strong>4 کاراکتر</strong> داشته باشد.
                    </div>
                </div>
                
                <asp:Button ID="btnChangePassword" runat="server" Text="💾 تغییر رمز عبور" 
                    CssClass="fp-btn fp-btn-success" OnClick="btnChangePassword_Click" />
            </asp:Panel>
            
            <!-- ============================================ -->
            <!-- لودینگ -->
            <!-- ============================================ -->
            <div id="fpLoading" class="fp-loading">
                <div class="spinner"></div>
                <p>لطفاً شکیبا باشید...</p>
            </div>
            
            <!-- ============================================ -->
            <!-- لینک‌ها -->
            <!-- ============================================ -->
            <div class="fp-links">
                <a href="Login_3.aspx">🔙 بازگشت به صفحه ورود</a>
                <a href="index.aspx">🏠 صفحه اصلی</a>
            </div>
        </div>
    </div>
    
    <!-- ============================================ -->
    <!-- اسکریپت تایمر -->
    <!-- ============================================ -->
    <script type="text/javascript">
        var timerInterval = null;
        var timeLeft = 120; // 2 دقیقه

        function startTimer() {
            timeLeft = 120;
            updateTimerDisplay();

            if (timerInterval) {
                clearInterval(timerInterval);
            }

            timerInterval = setInterval(function () {
                timeLeft--;
                updateTimerDisplay();

                if (timeLeft <= 0) {
                    clearInterval(timerInterval);
                    timerInterval = null;

                    var timerDiv = document.getElementById('divTimer');
                    if (timerDiv) {
                        timerDiv.classList.add('expired');
                        timerDiv.innerHTML = '<span class="timer-text">⏰ زمان کد تایید به پایان رسید. لطفاً کد جدید دریافت کنید.</span>';
                    }
                }
            }, 1000);
        }

        function updateTimerDisplay() {
            var minutes = Math.floor(timeLeft / 60);
            var seconds = timeLeft % 60;

            var display = (minutes < 10 ? '0' : '') + minutes + ':' + (seconds < 10 ? '0' : '') + seconds;

            var timerCount = document.getElementById('timerCount');
            if (timerCount) {
                timerCount.textContent = display;
            }
        }

        function stopTimer() {
            if (timerInterval) {
                clearInterval(timerInterval);
                timerInterval = null;
            }
        }

        // ============================================
        // نمایش لودینگ
        // ============================================
        function showLoading() {
            var loading = document.getElementById('fpLoading');
            if (loading) {
                loading.classList.add('show');
            }
        }

        function hideLoading() {
            var loading = document.getElementById('fpLoading');
            if (loading) {
                loading.classList.remove('show');
            }
        }
    </script>
</asp:Content>