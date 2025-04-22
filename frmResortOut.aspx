<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmResortOut.aspx.vb" Inherits="frmResortOut" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style1
        {
            width: 84%;
        }
            
        .style7
        {
        height: 30px;
        text-align: center;
        width: 249px;
        color: #000000;
    }
    .style11
    {
        width: 726px;
    }
        .style23
        {
            text-align: right;
        }
        .style24
        {
            text-align: right;
            width: 401px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" class="style1" style="width: 600px" dir="ltr">
                <tr>
                    <td class="style11">
                        <table align="center" class="style1" style="width: 600px">
                            <tr>
                                <td class="style23">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    <asp:Label ID="Label2" runat="server" style="color: #FF3300; font-weight: 700;" 
                                        Text="فرم ثبت خروج"></asp:Label>
                                </td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtResortId" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    شماره</td>
                            </tr>
                            <tr>
                                <td class="style23" rowspan="6">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right; color: #3366FF;">
                                    چاپ<asp:ImageButton ID="btnPrint" runat="server" Height="40px" 
                                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ مجدد کارت تردد" Width="30px" Visible="False" />
                                </td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtLnam" runat="server" BackColor="#FFFFCC" CssClass="txt_ r5" 
                                        Enabled="False" Font-Bold="True" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    نام و نام خانوادگی</td>
                            </tr>
                            <tr>
                                <td class="style24" rowspan="6" style="text-align: right;">
                                    <asp:Image ID="Image1" runat="server" Height="150px" Width="200px" />
                                </td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtzaman" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    زمان ورود</td>
                            </tr>
                            <tr>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtzamanXuruj" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    زمان خروج</td>
                            </tr>
                            <tr>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtsepordetyp" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    نوع سپرده</td>
                            </tr>
                            <tr>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtShomarecart" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    شماره کارت</td>
                            </tr>
                            <tr>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtellt" runat="server" BackColor="#FFFFCC" CssClass="txt_ r5" 
                                        Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    علت ملاقات</td>
                            </tr>
                            <tr>
                                <td class="style23">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtMizban" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    ملاقات شونده</td>
                            </tr>
                            <tr>
                                <td class="style23">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtunit" runat="server" BackColor="#FFFFCC" CssClass="txt_ r5" 
                                        Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    واحد</td>
                            </tr>
                            <tr>
                                <td class="style23">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtotag" runat="server" BackColor="#FFFFCC" CssClass="txt_ r5" 
                                        Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    موبایل</td>
                            </tr>
                            <tr>
                                <td class="style23">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    &nbsp;</td>
                                <td class="style24" style="text-align: right;">
                                    <asp:TextBox ID="txtCodemojavez" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    سریال لب تاپ</td>
                            </tr>
                            <tr>
                                <td class="style23" colspan="3">
                                    <asp:TextBox ID="txtNotee" runat="server" BackColor="#FFFFCC" 
                                        CssClass="txt_ r5" Enabled="False" Height="50px" TextMode="MultiLine" 
                                        ToolTip="توضیحات خورد را در این قسمت بنویسید" Width="450px"></asp:TextBox>
                                </td>
                                <td class="style7" style="text-align: left;">
                                    همراه</td>
                            </tr>
                            <tr>
                                <td class="style23" colspan="3">&nbsp;</td>
                                <td class="style7" style="text-align: left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style23" colspan="3">
                                    <asp:Button ID="btnBack0" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                                    <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت خروج" ValidationGroup="a" />
                                </td>
                                <td class="style7" style="text-align: left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 30px">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

