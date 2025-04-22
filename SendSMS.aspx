<%@ Page Language="vb" AutoEventWireup="false" CodeFile="SendSMS.aspx.vb" Inherits="SendSMS" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ارسال پیامک ساده</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 400px; margin: 0 auto; padding-top: 50px;">
            <h3>ارسال پیامک ساده (AFE.ir)</h3>

            <asp:Label ID="Label1" runat="server" Text="شماره‌ها (با , جدا کن):" />
            <asp:TextBox ID="txtMobiles" runat="server" TextMode="MultiLine" Rows="2" Width="100%" />

            <br /><br />
            <asp:Label ID="Label2" runat="server" Text="متن پیام:" />
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" Width="100%" />

            <br /><br />
            <asp:Button ID="btnSend" runat="server" Text="ارسال پیامک" OnClick="btnSend_Click" />

            <br /><br />
            <asp:Label ID="lblResult" runat="server" ForeColor="Green" />
        </div>
    </form>
</body>
</html>
