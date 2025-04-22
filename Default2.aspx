<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style6
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style6">
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                <asp:DropDownList ID="cmbreciver" runat="server" AutoPostBack="True" Width="300px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                <table class="style6">
                    <tr>
                        <td>
                            <asp:ListBox ID="ListBox3" runat="server" Height="672px" style="text-align: right" Width="176px"></asp:ListBox>
                        </td>
                        <td>
                            <asp:ListBox ID="ListBox1" runat="server" Height="345px" style="text-align: right" Width="176px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
        
                                    <asp:Button ID="btnIn" runat="server" CssClass="btn r5" 
                    Text="اضافه" Width="40px" />
                                </td>
                        <td>
        
                                     <asp:Button ID="btnOut" runat="server" CssClass="btn r5" Text="حذف" 
                                        Width="50px" />
                                  
                                </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
        
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="690px" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" Visible="False">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Tmp_id" HeaderText="#" />
                                            <asp:BoundField DataField="SystemCode" HeaderText="کد سیستم" Visible="False" />
                                            <asp:BoundField DataField="INOUTNOTE" HeaderText="نوع" />
                                            <asp:BoundField DataField="id_rec_note" HeaderText="قطعه حذف شده" />
                                            <asp:BoundField DataField="BaseModelName" HeaderText="مدل" />
                                            <asp:BoundField DataField="BaseSystemHDtlNmae" HeaderText="قطعه جدید" />
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
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

