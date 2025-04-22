<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="SystemSoft.aspx.vb" Inherits="SystemSoft" %>
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
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style6" style="width: 710px" align="center">
        <tr>
            <td style="text-align: center">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" 
                        Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDeleted" runat="server" Height="48px" 
                        ImageUrl="~/Icons/deleted.png" ToolTip="لیست کدهای حذف شده" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" Visible="False" />
                </td>
        </tr>
        <tr>
            <td>

    <table class="style1" style="width: 600px" align="center">
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9">
                <asp:TextBox ID="txt_bulding" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style10" style="width: 50px; height: 30px">
                

                
                
                
                ساختمان</td>
            <td class="style2" style="height: 30px" colspan="2">




                <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButton1">
                    <asp:RequiredFieldValidator ID="vald_Systemname12" runat="server" 
                        ControlToValidate="txt_SystemCode" ErrorMessage="*" ForeColor="Red" 
                        InitialValue="0" ValidationGroup="a"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="images/design_images/kfilereplace.png" ValidationGroup="a" />
                    <asp:RequiredFieldValidator ID="vald_Systemname11" runat="server" 
                        ControlToValidate="txt_SystemCode" ErrorMessage="*" ForeColor="Red" 
                        ValidationGroup="b"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txt_SystemCode" runat="server" Width="150px" 
                    ValidationGroup="a" CssClass="txt_ r5" MaxLength="6"></asp:TextBox>
                </asp:Panel>




            </td>
            <td class="style8" style="text-align: left;">
                شماره اموال</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" dir="rtl">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9">
                <asp:TextBox ID="txt_Systemunit" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style10" style="width: 50px; height: 30px">
                واحد</td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;" 
                dir="rtl">
                <asp:TextBox ID="txt_SystemTabage" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                طبقه</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" dir="rtl">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9">
                <asp:TextBox ID="txt_SystemRoom" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style10" style="width: 50px; height: 30px">
                نام
                اتاق </td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_holder" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                تحويل گيرنده</td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9">
                <asp:TextBox ID="txt_SystemPhone" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style10" style="width: 50px; height: 30px">
                تلفن</td>
            <td class="style2" style="width: 50px; height: 30px">
                
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_SystemRoomNum" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                شماره اتاق</td>
        </tr>
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
                
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_ComputerName" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False" ForeColor="Red"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                نام كامپيوتر</td>
        </tr>
        <tr>
            <td class="style11">
                </td>
            <td class="style11" colspan="6" dir="rtl">
    
                                    <asp:GridView ID="GridSoft" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="500px" 
                    style="color: #000000" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                                        BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField="BaseSystemSDtlCode" HeaderText="#" />
                                            <asp:BoundField DataField="BaseSystemSDtlNmae" 
                                                HeaderText="نرم افزارهاي ثبت شده روي سيستم" />
                                            <asp:ButtonField CommandName="dell" Text="حذف" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
    
                        </td>

        </tr>
        <tr>
       
            <td style="width: 250px; height: 30px">
        
                &nbsp;</td>
            <td colspan="3" dir="rtl">
        
                                    <asp:Button ID="btnIn" runat="server" CssClass="btn r5" 
                    Text="ثبت موقت" Width="100px" ValidationGroup="b" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                </td>
            <td class="style2" style="width: 50px; height: 30px">
                <asp:RequiredFieldValidator ID="vald_Systemname10" runat="server" 
                    ControlToValidate="Cmbsoftware" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="b" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" 
                style="width: 200px; height: 30px; text-align: right; color: #000000;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Cmbsoftware" 
    runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Height="30px" TabIndex="10" 
                                        Width="190px" 
    ValidationGroup="b">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
             
            <td class="style7" style="text-align: left; color: #000000;">
                ليست </td>
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="table-layout: fixed;" colspan="6" dir="rtl">
    
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="500px" style="color: #000000" 
                                        CssClass="gr_" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Tmp_id" HeaderText="#" >
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BaseSystemSDtlNmae" HeaderText="نرم افزار" />
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
            <td style="height: 30px" colspan="7">
                34<asp:HiddenField ID="hdn_tmp" runat="server" />
    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </td>
        </tr>
        </table>
                </td>
        </tr>
    </table>
</asp:Content>

