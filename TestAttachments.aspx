<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestAttachments.aspx.vb" Inherits="TestAttachments" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>تست فایل‌های ضمیمه</title>
    <style>
        body { font-family: Tahoma; direction: rtl; padding: 20px; }
        .result { background: #f0f0f0; padding: 15px; margin: 10px 0; border-radius: 5px; }
        .success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .error { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: right; }
        th { background: #f2f2f2; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>🔍 تست فایل‌های ضمیمه</h1>
        
        <div>
            <label>Course ID: </label>
            <asp:TextBox ID="txtCourseId" runat="server" Text="123" Width="100px" />
            <asp:Button ID="btnTest" runat="server" Text="تست" OnClick="btnTest_Click" />
            <asp:Button ID="btnTestAll" runat="server" Text="نمایش همه دوره‌ها" OnClick="btnTestAll_Click" />
        </div>

        <div id="divResult" runat="server" class="result"></div>

        <div id="divData" runat="server" visible="false">
            <h2>نتایج کوئری</h2>
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="True" CssClass="table" />
        </div>
    </form>
</body>
</html>