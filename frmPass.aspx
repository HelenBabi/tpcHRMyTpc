<%@ Page Title="تغییر رمز عبور" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmPass.aspx.vb" Inherits="frmPass" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .password-container {
            min-height: 80vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            padding: 20px;
        }
        .password-card {
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            max-width: 500px;
            width: 100%;
            overflow: hidden;
            animation: fadeInUp 0.5s ease;
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
        .password-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px 25px;
            text-align: center;
        }
        .password-header h3 {
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }
        .password-header p {
            margin: 10px 0 0;
            opacity: 0.9;
            font-size: 13px;
        }
        .password-body {
            padding: 35px 30px;
        }
        .form-group-custom {
            margin-bottom: 25px;
        }
        .form-label-custom {
            display: block;
            font-weight: bold;
            color: #555;
            margin-bottom: 8px;
            font-size: 14px;
        }
        .input-group-custom {
            position: relative;
        }
        .input-icon {
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 18px;
            color: #999;
            z-index: 1;
        }
        .form-control-custom {
            width: 100%;
            padding: 12px 40px 12px 12px;
            border: 2px solid #e9ecef;
            border-radius: 12px;
            font-size: 14px;
            transition: all 0.3s ease;
            background: #f8f9fa;
        }
        .form-control-custom:focus {
            border-color: #667eea;
            outline: none;
            background: white;
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
        }
        .btn-custom {
            width: 48%;
            padding: 12px;
            border: none;
            border-radius: 12px;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .btn-save {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }
        .btn-save:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(102,126,234,0.4);
        }
        .btn-back {
            background: #6c757d;
            color: white;
        }
        .btn-back:hover {
            background: #5a6268;
            transform: translateY(-2px);
        }
        .btn-group-custom {
            display: flex;
            justify-content: space-between;
            gap: 15px;
            margin-top: 30px;
        }
        .password-strength {
            margin-top: 8px;
            font-size: 11px;
        }
        .strength-weak {
            color: #dc3545;
        }
        .strength-medium {
            color: #ffc107;
        }
        .strength-strong {
            color: #28a745;
        }
        .error-message {
            color: #dc3545;
            font-size: 11px;
            margin-top: 5px;
            display: block;
        }
        .lock-icon {
            font-size: 60px;
            margin-bottom: 15px;
        }
        .divider {
            text-align: center;
            margin: 20px 0;
            position: relative;
        }
        .divider::before {
            content: '';
            position: absolute;
            top: 50%;
            left: 0;
            right: 0;
            height: 1px;
            background: #e9ecef;
        }
        .divider span {
            background: white;
            padding: 0 15px;
            position: relative;
            color: #999;
            font-size: 12px;
        }
        @media (max-width: 576px) {
            .password-body {
                padding: 25px 20px;
            }
            .btn-custom {
                width: 100%;
                margin-bottom: 10px;
            }
            .btn-group-custom {
                flex-direction: column;
                gap: 10px;
            }
        }
    </style>
    
    <script type="text/javascript">
        function checkPasswordStrength() {
            var password = document.getElementById('<%= txt_NewPass1.ClientID %>').value;
            var strengthDiv = document.getElementById('passwordStrength');

            if (password.length == 0) {
                strengthDiv.innerHTML = '';
                return;
            }

            var strength = 0;
            if (password.length >= 6) strength++;
            if (password.length >= 8) strength++;
            if (password.match(/[a-z]+/)) strength++;
            if (password.match(/[A-Z]+/)) strength++;
            if (password.match(/[0-9]+/)) strength++;
            if (password.match(/[$@#&!]+/)) strength++;

            var message = '';
            var className = '';

            if (strength <= 2) {
                message = '⚠️ رمز ضعیف';
                className = 'strength-weak';
            } else if (strength <= 4) {
                message = '⚠️ رمز متوسط';
                className = 'strength-medium';
            } else {
                message = '✅ رمز قوی';
                className = 'strength-strong';
            }

            strengthDiv.innerHTML = message;
            strengthDiv.className = 'password-strength ' + className;
        }

        function validatePasswords() {
            var newPass = document.getElementById('<%= txt_NewPass1.ClientID %>').value;
            var confirmPass = document.getElementById('<%= txt_NewPass2.ClientID %>').value;
            var errorDiv = document.getElementById('passwordError');

            if (newPass != confirmPass && confirmPass != '') {
                errorDiv.innerHTML = '❌ رمز جدید و تکرار آن مطابقت ندارند';
                errorDiv.style.display = 'block';
                return false;
            } else {
                errorDiv.innerHTML = '';
                errorDiv.style.display = 'none';
                return true;
            }
        }

        function validateForm() {
            var currentPass = document.getElementById('<%= txt_CurentPass.ClientID %>').value;
            var newPass = document.getElementById('<%= txt_NewPass1.ClientID %>').value;
            var confirmPass = document.getElementById('<%= txt_NewPass2.ClientID %>').value;

            if (currentPass == '') {
                alert('لطفاً رمز فعلی را وارد کنید');
                return false;
            }

            if (newPass == '') {
                alert('لطفاً رمز جدید را وارد کنید');
                return false;
            }

            if (confirmPass == '') {
                alert('لطفاً تکرار رمز جدید را وارد کنید');
                return false;
            }

            if (newPass != confirmPass) {
                alert('رمز جدید و تکرار آن مطابقت ندارند');
                return false;
            }

            if (newPass.length < 4) {
                alert('رمز جدید باید حداقل ۴ کاراکتر باشد');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="password-container" dir="rtl">
        <div class="password-card">
            
            <!-- هدر کارت -->
            <div class="password-header">
                <div class="lock-icon">🔐</div>
                <h3>تغییر رمز عبور</h3>
                <p>برای حفظ امنیت حساب خود، رمز عبور خود را به صورت دوره‌ای تغییر دهید</p>
            </div>
            
            <!-- بدنه فرم -->
            <div class="password-body">
                
                <!-- رمز فعلی -->
                <div class="form-group-custom">
                    <label class="form-label-custom">🔒 رمز فعلی</label>
                    <div class="input-group-custom">
                        <span class="input-icon">🔑</span>
                        <asp:TextBox ID="txt_CurentPass" runat="server" 
                            CssClass="form-control-custom" 
                            TextMode="Password"
                            placeholder="رمز فعلی خود را وارد کنید"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="vald_Passcode" runat="server" 
                        ControlToValidate="txt_CurentPass" 
                        ErrorMessage="* رمز فعلی الزامی است" 
                        ForeColor="Red" 
                        ValidationGroup="a"
                        CssClass="error-message"></asp:RequiredFieldValidator>
                </div>
                
                <div class="divider">
                    <span>رمز جدید</span>
                </div>
                
                <!-- رمز جدید -->
                <div class="form-group-custom">
                    <label class="form-label-custom">✨ رمز جدید</label>
                    <div class="input-group-custom">
                        <span class="input-icon">🔐</span>
                        <asp:TextBox ID="txt_NewPass1" runat="server" 
                            CssClass="form-control-custom" 
                            TextMode="Password"
                            placeholder="رمز جدید را وارد کنید"
                            onkeyup="checkPasswordStrength()"></asp:TextBox>
                    </div>
                    <div id="passwordStrength" class="password-strength"></div>
                    <asp:RequiredFieldValidator ID="vald_Passname" runat="server" 
                        ControlToValidate="txt_NewPass1" 
                        ErrorMessage="* رمز جدید الزامی است" 
                        ForeColor="Red" 
                        ValidationGroup="a"
                        CssClass="error-message"></asp:RequiredFieldValidator>
                </div>
                
                <!-- تکرار رمز جدید -->
                <div class="form-group-custom">
                    <label class="form-label-custom">🔄 تکرار رمز جدید</label>
                    <div class="input-group-custom">
                        <span class="input-icon">✓</span>
                        <asp:TextBox ID="txt_NewPass2" runat="server" 
                            CssClass="form-control-custom" 
                            TextMode="Password"
                            placeholder="رمز جدید را مجدد وارد کنید"
                            onkeyup="validatePasswords()"></asp:TextBox>
                    </div>
                    <div id="passwordError" class="error-message" style="display:none;"></div>
                    <asp:RequiredFieldValidator ID="vald_Passname0" runat="server" 
                        ControlToValidate="txt_NewPass2" 
                        ErrorMessage="* تکرار رمز جدید الزامی است" 
                        ForeColor="Red" 
                        ValidationGroup="a"
                        CssClass="error-message"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cmpPasswords" runat="server" 
                        ControlToCompare="txt_NewPass1"
                        ControlToValidate="txt_NewPass2"
                        ErrorMessage="* رمز جدید و تکرار آن مطابقت ندارند"
                        ForeColor="Red"
                        ValidationGroup="a"
                        CssClass="error-message"></asp:CompareValidator>
                </div>
                
                <!-- دکمه‌ها -->
                <div class="btn-group-custom">
                    <asp:Button ID="btnSave" runat="server" 
                        CssClass="btn-custom btn-save" 
                        Text="💾 ذخیره تغییرات" 
                        ValidationGroup="a"
                        OnClientClick="return validateForm();"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnBack" runat="server" 
                        CssClass="btn-custom btn-back" 
                        Text="🏠 بازگشت به صفحه اصلی" 
                        OnClick="btnBack_Click" />
                </div>
                
                <!-- نکات امنیتی -->
                <div class="divider">
                    <span>نکات امنیتی</span>
                </div>
                <div style="font-size: 11px; color: #999; text-align: center; margin-top: 15px;">
                    <p style="margin: 5px 0;">• رمز عبور باید حداقل ۴ کاراکتر باشد</p>
                    <p style="margin: 5px 0;">• از ترکیب حروف بزرگ، کوچک و اعداد استفاده کنید</p>
                    <p style="margin: 5px 0;">• از رمزهای تکراری برای چندین سایت استفاده نکنید</p>
                </div>
                
            </div>
            
        </div>
    </div>
    
</asp:Content>