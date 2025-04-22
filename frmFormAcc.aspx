<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmFormAcc.aspx.vb" Inherits="frmFormAcc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style1
        {
            width: 84%;
        }
        .style6
        {
            width: 100%;
        }
        .style2
        {
            height: 26px;
            text-align: center;
        }
    
        .style7
        {
            height: 30px;
            text-align: center;
            width: 100px;
        }
        .style8
        {
            height: 30px;
            text-align: center;
            width: 100px;
            color: #000000;
        }
        .style9
        {
            color: #000000;
        }
        .style10
        {
            height: 26px;
            text-align: center;
            color: #000000;
        }
    .dxeTextBox .dxeEditArea
{
    background-color: white;
}

.dxeEditArea 
{
	font-family: Tahoma;
	font-size: 9pt;
	border: 1px solid #A0A0A0;
}
        .style11
        {
            width: 100%;
            padding: 1px 2px;
        }
        .style12
        {
            width: 100%;
            color: #acacac;
            cursor: default;
            border-width: 0px;
            margin-left: 2px;
            margin-right: 1px;
            margin-top: 0px;
            margin-bottom: 1px;
            padding-left: 0px;
            padding-right: 1px;
            padding-top: 0px;
            padding-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1" style="width: 700px" align="center">
        <tr>
            <td>

    <table class="style1" style="width: 600px" align="center">
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9" dir="rtl">
                &nbsp;</td>
            <td class="style10" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:DropDownList ID="cmbreciver" runat="server" AutoPostBack="True" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="style8" style="text-align: left;">
                نام کاربر</td>
        </tr>
        
        <tr>
       
            <td style="width: 250px; height: 30px">
        
                &nbsp;</td>
            <td colspan="3" dir="rtl">
        
                                     <asp:Button ID="btnOut" runat="server" CssClass="btn r5" Text="حذف" 
                                        Width="50px" />
                                  
                                </td>
            <td class="style2" style="height: 30px" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="CmbFormUser" runat="server" CssClass="txt_ r5" 
                                        Height="30px" TabIndex="9" Width="194px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
          
            <td class="style7" style="text-align: left; color: #000000;">
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="فرم هایی که کاربر دسترسی دارد" 
                    Width="120px"></asp:Label>
&nbsp; </td>
        </tr>
        
        <tr>
       
            <td style="width: 250px; height: 30px">
        
                                    &nbsp;</td>
            <td colspan="3" dir="rtl">
        
                                    <asp:Button ID="btnIn" runat="server" CssClass="btn r5" 
                    Text="اضافه" Width="40px" />
                                </td>
            <td class="style2" style="height: 30px" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="CmbForms" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Height="30px" TabIndex="10" 
                                        Width="194px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
            
            <td class="style7" style="text-align: left; color: #000000;">
                <asp:Label ID="Label3" runat="server" Text="فرم های موجود"></asp:Label>
            </td>
        </tr>
        
        <tr>
       
            <td style="height: 30px" colspan="7">
        
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="690px" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Tmp_id" HeaderText="#" />
                                            <asp:BoundField DataField="FrmID" HeaderText="کد فرم" Visible="False" />
                                            <asp:BoundField DataField="id_rec_note" HeaderText="فرم حذف شده" />
                                            <asp:BoundField DataField="FrmCaption" HeaderText="فرم جدید" />
                                            <asp:ButtonField CommandName="del" Text="حذف" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
             
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="height: 30px" colspan="6">
    
                                    <asp:HiddenField ID="hdn_tmp" runat="server" />
    
    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
    
                        </td>
        </tr>
        </table>
                </td>
        </tr>
    </table>
</asp:Content>

