<%@ Page Language="vb" AutoEventWireup="false" CodeFile="test_sso.aspx.vb" Inherits="test_sso" %>

<!DOCTYPE html>
<html>
<head>
    <title>تست SSO</title>
    <style>
        body { font-family: Tahoma; direction: rtl; padding: 20px; }
        .container { max-width: 800px; margin: auto; }
        .btn { 
            padding: 10px 20px; 
            margin: 5px; 
            cursor: pointer;
            background: #667eea;
            color: white;
            border: none;
            border-radius: 5px;
        }
        .btn:hover { background: #764ba2; }
        .result { 
            background: #f0f0f0; 
            padding: 15px; 
            margin: 10px 0; 
            border-radius: 5px;
            min-height: 100px;
        }
        .success { color: green; font-weight: bold; }
        .error { color: red; font-weight: bold; }
        pre {
            background: #fff;
            padding: 10px;
            border-radius: 5px;
            overflow: auto;
            max-height: 300px;
            border: 1px solid #ddd;
            white-space: pre-wrap;
            word-wrap: break-word;
        }
        .info-box {
            background: #d4edda;
            padding: 15px;
            border-radius: 5px;
            margin-top: 10px;
            border: 2px solid #28a745;
        }
        .loading { 
            background: #f0f0f0; 
            padding: 10px; 
            text-align: center;
        }
        .test-buttons {
            margin: 20px 0;
            display: flex;
            flex-wrap: wrap;
            gap: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>🔍 تست مسیرهای SSO</h2>
            <p>برای پیدا کردن آدرس صحیح SSO، روی هر دکمه کلیک کنید</p>
            
            <div class="test-buttons">
                <asp:Button ID="btnTest1" runat="server" Text="تست /login" CssClass="btn" OnClick="btnTest1_Click" />
                <asp:Button ID="btnTest2" runat="server" Text="تست /oauth2/authorize" CssClass="btn" OnClick="btnTest2_Click" />
                <asp:Button ID="btnTest3" runat="server" Text="تست /connect/authorize" CssClass="btn" OnClick="btnTest3_Click" />
                <asp:Button ID="btnTest4" runat="server" Text="تست /authorize" CssClass="btn" OnClick="btnTest4_Click" />
                <asp:Button ID="btnTest5" runat="server" Text="تست Discovery" CssClass="btn" OnClick="btnTest5_Click" />
                <asp:Button ID="btnTest6" runat="server" Text="تست /api/authorize" CssClass="btn" OnClick="btnTest6_Click" />
                <asp:Button ID="btnTest7" runat="server" Text="تست /Account/Login" CssClass="btn" OnClick="btnTest7_Click" />
                <asp:Button ID="btnTest8" runat="server" Text="تست /realms/master" CssClass="btn" OnClick="btnTest8_Click" />
                <asp:Button ID="btnTestAll" runat="server" Text="تست همه" CssClass="btn" OnClick="btnTestAll_Click" Style="background: #28a745;" />
            </div>
            
            <div id="resultDiv" runat="server" class="result">
                <p>⏳ روی یکی از دکمه‌ها کلیک کنید تا نتیجه تست را ببینید</p>
            </div>
            
            <div style="margin-top: 20px; padding: 10px; background: #fff3cd; border-radius: 5px;">
                <h4>💡 راهنمایی:</h4>
                <ul>
                    <li>اگر آدرسی <strong>موفق</strong> بود، آن را در Web.config قرار دهید</li>
                    <li>اگر Discovery موفق بود، می‌توانید از آن استفاده کنید</li>
                    <li>اگر هیچکدام موفق نبود، با پشتیبانی SSO تماس بگیرید</li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>