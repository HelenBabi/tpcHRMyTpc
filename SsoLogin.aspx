<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SsoLogin.aspx.vb" Inherits="SsoLogin" %>
<!DOCTYPE html>

<html lang="fa" dir="rtl">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ورود با SSO</title>
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <style>
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'Tahoma', 'IranSans', sans-serif;
        }
        .login-box {
            background: white;
            border-radius: 20px;
            padding: 40px;
            max-width: 450px;
            width: 100%;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
        }
        .login-title {
            text-align: center;
            font-size: 24px;
            font-weight: bold;
            color: #333;
            margin-bottom: 30px;
        }
        .login-title::after {
            content: '';
            display: block;
            width: 50px;
            height: 3px;
            background: linear-gradient(90deg, #667eea, #764ba2);
            margin: 10px auto 0;
            border-radius: 3px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-control {
            border-radius: 12px;
            padding: 12px 15px;
            border: 2px solid #e0e0e0;
            transition: all 0.3s ease;
            text-align: right;
        }
        .form-control:focus {
            border-color: #667eea;
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
        }
        .btn-login {
            width: 100%;
            padding: 14px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            border-radius: 12px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .btn-login:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(102,126,234,0.3);
        }
        
        /* استایل دکمه کد پرسنلی */
        .btn-personnel {
            width: 100%;
            padding: 14px;
            background: #28a745;
            color: white;
            border: none;
            border-radius: 12px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-top: 10px;
        }
        .btn-personnel:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(40, 167, 69, 0.3);
            background: #218838;
        }
        
        .divider {
            display: flex;
            align-items: center;
            text-align: center;
            margin: 25px 0;
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
        
        .error-message {
            color: #e74c3c;
            font-size: 13px;
            margin-top: 10px;
            text-align: center;
        }
        .success-message {
            color: #28a745;
            font-size: 13px;
            margin-top: 10px;
            text-align: center;
        }
        .info-text {
            text-align: center;
            color: #666;
            font-size: 14px;
            margin-top: 10px;
        }
        .user-info {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 10px 15px;
            margin: 10px 0 15px 0;
            text-align: center;
            border-right: 3px solid #667eea;
        }
        .user-info .label {
            color: #666;
            font-size: 11px;
            display: block;
        }
        .user-info .value {
            color: #333;
            font-size: 14px;
            font-weight: bold;
        }
        .hint-text {
            color: #999;
            font-size: 11px;
            text-align: center;
            margin-top: 5px;
        }
    </style>
</head>
<body>
    <div class="login-box">
        <div class="login-title">🔑 ورود با SSO</div>
        
        <form id="form1" runat="server">
            <!-- نمایش نام کاربری ویندوز (فقط برای اطلاع) -->
            <div class="user-info">
                <span class="label">نام کاربری ویندوز فعلی</span>
                <span class="value" id="lblWindowsUser" runat="server">-</span>
            </div>
            
            <!-- فیلد نام کاربری (کد پرسنلی) با مقدار پیش‌فرض -->
            <div class="form-group">
                <asp:TextBox ID="txtPersonnelCode" runat="server" CssClass="form-control" 
                    placeholder="نام کاربری ویندوز" TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPersonnelCode" ErrorMessage="نام کاربری ویندوز را وارد کنید" 
                    ForeColor="Red" Display="Dynamic" ValidationGroup="sso"></asp:RequiredFieldValidator>
                <div class="hint-text">* نام کاربری ویندوز به صورت پیش‌فرض وارد شده، قابل ویرایش است</div>
            </div>
            
            <!-- فیلد رمز عبور -->
            <div class="form-group">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" 
                    placeholder="رمز عبور" TextMode="Password" TabIndex="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtPassword" ErrorMessage="رمز عبور را وارد کنید" 
                    ForeColor="Red" Display="Dynamic" ValidationGroup="sso"></asp:RequiredFieldValidator>
            </div>
            
            <!-- دکمه ورود با SSO -->
            <div class="form-group">
                <asp:Button ID="btnSSOLogin" runat="server" Text="🔑 ورود با SSO" 
                    CssClass="btn-login" OnClick="btnSSOLogin_Click" ValidationGroup="sso" />
            </div>
            
            <!-- جداکننده -->
            <div class="divider">
                <span>یا</span>
            </div>
            
            <!-- دکمه ورود با کد پرسنلی (هدایت به login_2.aspx) -->
            <div class="form-group">
                <asp:Button ID="btnPersonnelLogin" runat="server" Text="👤 ورود با کد پرسنلی (صفحه دیگر)" 
                    CssClass="btn-personnel" OnClick="btnPersonnelLogin_Click" />
            </div>
            
            <div id="lblMessage" runat="server" class="error-message"></div>
            <div id="lblSuccess" runat="server" class="success-message"></div>
            
            <div class="info-text">
                <small>برای ورود با SSO، نام کاربری و رمز عبور خود را وارد کنید</small>
            </div>
        </form>
    </div>
</body>
</html>