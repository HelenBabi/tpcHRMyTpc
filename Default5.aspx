<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default5.aspx.vb" Inherits="Default5" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ارسال پیامک با AFE</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="direction:rtl; text-align:right; width:300px; margin:100px auto;">
            <h3>ارسال پیامک</h3>
            شماره گیرنده:<br />
            <asp:TextBox ID="txtNumber" runat="server" Width="100%"></asp:TextBox><br /><br />
            متن پیام:<br />
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox><br /><br />
            <asp:Button ID="btnSend" runat="server" Text="ارسال پیامک" OnClick="btnSend_Click" /><br /><br />
            <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>