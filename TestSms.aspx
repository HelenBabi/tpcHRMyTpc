<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestSms.aspx.vb" Inherits="TestSms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>تست ارسال پیامک</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; direction:rtl; font-family:Tahoma; padding:50px;">
            <h2>🧪 تست ارسال پیامک یادآوری</h2>
            <asp:Button ID="btnSend" runat="server" Text="📨 ارسال پیامک تست" 
                CssClass="btn" OnClick="btnSend_Click" 
                style="padding:10px 30px; font-size:16px; background:#27ae60; color:white; border:none; border-radius:5px; cursor:pointer;" />
            <br /><br />
            <asp:Label ID="lblResult" runat="server" ForeColor="Blue" Font-Size="14px" />
        </div>
    </form>
</body>
</html>