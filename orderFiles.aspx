<%@ Page Language="VB" AutoEventWireup="false" CodeFile="orderFiles.aspx.vb" Inherits="orderFiles" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="dabir.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>  Orderfiles</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 711px;
        }
        </style>
</head>
<body bgcolor="White">
    <form id="form1" runat="server">

    
    <table class="style1">
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                <asp:DropDownList ID="cmbfiletyp" runat="server">
                    <asp:ListItem Value="0">درخواست</asp:ListItem>
                    <asp:ListItem Value="1">پیش فاکتور</asp:ListItem>
                    <asp:ListItem Value="2">فیش واریزی</asp:ListItem>
                    <asp:ListItem Value="3">نامه مالی</asp:ListItem>
                    <asp:ListItem Value="4">حواله</asp:ListItem>
                    <asp:ListItem Value="5">سایر</asp:ListItem>
                </asp:DropDownList>
    
                
                <asp:FileUpload runat="server" CssClass="txt_ r5 h25" Width="500px" 
                    ID="FileUpload1"></asp:FileUpload>
                                            
                
                                               </td>
            <td>
                <asp:Label ID="Label1" runat="server" ForeColor="Black" Text=":فایل ضمیه"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                <asp:Button ID="Button1" runat="server" Text="ذخیره" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                <asp:DataList ID="DataList1" runat="server" style="margin: 8px;" Width="700px" 
                    DataKeyField="OrderID">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" class="style1" 
                            style="padding-top:5px;line-height:30px;">
                            <tr>
                                <td>
                                    <div style=" text-align:right;direction:rtl; width: 638px;">
                                        <div style="float:right ;width:556px;">
                                            <asp:Label ID="Label4" runat="server" ForeColor="Black" 
                                                Text='<%# eval("noeFile") %>' Width="250px"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" 
                                                CommandArgument='<%# eval("idd") %>' CommandName="dell" Font-Bold="True">حذف</asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                        <div style="text-align: left; float: left; width: 70px; background-color: #008F00; border-top: 1px solid #00CC00; border-right: 1px solid #008F00; border-bottom: 1px solid #008F00; border-left: 1px solid #00CC00; background: url('http://localhost:4261/artan/image/design_images/bg-download.png') repeat-x 50px center;">
                                            <div style="border: 1px solid #fff; background: url('http://localhost:4261/artan/image/design_images/icon-download.png') no-repeat 50px center; padding: 0px 10px 0px 10px;">
                                                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Black" 
                                                    NavigateUrl='<%# eval("FilesMasir") %>' Target="_blank">مشاهده</asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td rowspan="2">
                                    <div style="background-image: url('http://localhost:4261/artan/image/design_images/Document-Blank-icon.png'); background-repeat: no-repeat; height: 48px; width: 48px;">
                                    </div>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="text-align:right;direction:rtl;  width: 638px;">
                                        <div style="color:Black;float:right;width:30px;">
                                            تاریخ
                                        </div>
                                        <div style="color:Black;float:right;width:70px;">
                                            <asp:Label ID="Label6" runat="server" ForeColor="#999999" 
                                                Text='<%# eval("date_s") %>'></asp:Label>
                                        </div>
                                        <div style="color:Black;float:right;width:40px;">
                                            ساعت
                                        </div>
                                        <div style="color:Black;float:right;width:100px;">
                                            <asp:Label ID="Label3" runat="server" ForeColor="#999999" 
                                                Text='<%# eval("time_s") %>'></asp:Label>
                                        </div>
                                        <div style="color:Black;float:left;width:100px;text-align:left; direction:ltr;">
                                            <asp:Label ID="Label5" runat="server" Text='<%# eval("FilesName") %>' 
                                                ForeColor="#FF9900"></asp:Label>
                                        </div>
                                        <div style="color:Black;float:left;width:80px;text-align:left;direction:rtl;">
                                            
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <div style="margin:8px;margin-top:0px;height:1px;background-color:#bfbebe;">
                        </div>
                    </SeparatorTemplate>
                </asp:DataList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    
    </form>
</body>
</html>
