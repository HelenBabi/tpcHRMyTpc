<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="GenerateQR.aspx.vb" Inherits="GenerateQR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h3>فعال‌سازی ارزیابی کلاس</h3>
    <asp:Button ID="btnQR" runat="server" Text="تولید QR" OnClick="btnQR_Click"/>
    <br /><br />
    <asp:Image ID="imgQR" runat="server" />
</asp:Content>

