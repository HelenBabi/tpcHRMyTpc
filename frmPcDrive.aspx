<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmPcDrive.aspx.vb" Inherits="frmPcDrive" %>

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
    
    <table class="style6" style="width: 700px" align="center">
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
                <asp:RequiredFieldValidator ID="vald_Systemname8" runat="server" 
                    ControlToValidate="txt_SystemRoomNum" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
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
                <asp:RequiredFieldValidator ID="vald_Systemname9" runat="server" 
                    ControlToValidate="txt_ComputerName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_ComputerName" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False" ForeColor="Red"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                نام كامپيوتر</td>
        </tr>
        <tr>
            <td style="height: 30px" colspan="7" dir="rtl">
                 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdDrive" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" Font-Size="10pt" style="color: #000000; text-align: center;" 
                            TabIndex="12" Width="519px">
                            <Columns>
                                <asp:BoundField DataField="idd" HeaderText="#" />
                                <asp:BoundField DataField="driveName" HeaderText="درايو" />
                                <asp:BoundField DataField="dirveTyp" HeaderText="نوع" />
                                <asp:BoundField DataField="capacity_" HeaderText="فضاي كل" />
                                <asp:BoundField DataField="used_" HeaderText="پر" />
                                <asp:BoundField DataField="free_" HeaderText="خالي" />
                                <asp:BoundField DataField="os" HeaderText="OS" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
    
            </td>
        </tr>
        
        <tr>
       
            <td style="height: 30px" colspan="7">
        
                <table class="style6">
                    <tr class="style9">
                        <td style="text-align: right" width="51">
                                    <asp:TextBox ID="txt_free" runat="server" CssClass="txt_ r5" ForeColor="Red" 
                                        ValidationGroup="a" Width="50px"></asp:TextBox>
                        </td>
                        <td style="text-align: left" width="40">
                            خالي</td>
                        <td style="text-align: right" width="51">
                                    <asp:TextBox ID="txt_Capacity" runat="server" CssClass="txt_ r5" 
                                        ForeColor="Red" ValidationGroup="a" Width="50px"></asp:TextBox>
                        </td>
                        <td style="text-align: left" width="50">
                            فضاي كل</td>
                        <td style="text-align: right" width="51">
                                            <asp:CheckBox ID="ChkOS" runat="server" style="color: #FF3300" Text="OS?" 
                                                TextAlign="Left" Width="50px" />
                                        </td>
                        <td style="width: 111px; text-align: right" width="70">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="CmbDriveType" runat="server" AutoPostBack="True" 
                                                CssClass="txt_ r5" Height="30px" TabIndex="10" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="CmbDrivelist" runat="server" AutoPostBack="True" 
                                                CssClass="txt_ r5" Height="30px" TabIndex="10" Width="40px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                        </td>
                        <td style="text-align: left" width="30">
                            درايو</td>
                    </tr>
                    <tr class="style9">
                        <td style="text-align: right" width="51">
                                    &nbsp;</td>
                        <td style="text-align: left" width="40">
                            &nbsp;</td>
                        <td style="text-align: right" width="51">
                                    &nbsp;</td>
                        <td style="text-align: left; width: 101px;" width="50" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnIn" runat="server" CssClass="btn r5" Font-Bold="True" 
                                        Text="ثبت موقت" ValidationGroup="b" Width="100px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 111px; text-align: right" width="70">
                                    &nbsp;</td>
                        <td style="text-align: left" width="30">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
       
            <td style= height: 30px; text-align: center;" colspan="7" dir="rtl">
        
                           
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" Font-Size="10pt" ForeColor="#333333" GridLines="None" 
                                style="color: #000000" TabIndex="12" Width="500px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Tmp_id" HeaderText="#">
                                    <ItemStyle ForeColor="White" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SystemCode" HeaderText="کد سیستم" Visible="False" />
                                    <asp:BoundField DataField="driveName" HeaderText="درايو" />
                                    <asp:BoundField DataField="dirveTyp" HeaderText="نوع" />
                                    <asp:BoundField DataField="capacity_" HeaderText="فضاي كل" />
                                    <asp:BoundField DataField="used_" HeaderText="پر" />
                                    <asp:BoundField DataField="free_" HeaderText="خالي" />
                                    <asp:BoundField DataField="os" HeaderText="OS" />
                                    <asp:ButtonField CommandName="del2" Text="حذف" />
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
                            <asp:HiddenField ID="hdn_tmp" runat="server" />
                            <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
               
            </td>
        </tr>
        
        <tr>
       
            <td style="width: 250px; height: 30px">
        
                &nbsp;</td>
            <td colspan="6" dir="rtl">
        
                        &nbsp;</td>
             
        </tr>
        <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="height: 30px" colspan="6">
    
                        &nbsp;</td>
        </tr>
        </table>
                </td>
        </tr>
        
    </table>
            
</asp:Content>
