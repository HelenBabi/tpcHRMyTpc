<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmPcEditHardware.aspx.vb" Inherits="frmPcEditHardware" %>

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
            <td style="text-align: center" dir="ltr">
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
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        Enabled="False" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" />
                </td>
        </tr>
        <tr>
            <td>

    <table class="style1" style="width: 600px" align="center">
        <tr>
            <td style="height: 30px" colspan="2">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9">
                <asp:TextBox ID="txt_bulding" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </td>
            <td class="style10" style="width: 50px; height: 30px">
                

                
                
                
                ساختمان</td>
            <td class="style2" style="width: 50px; height: 30px">




                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
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
                &nbsp;</td>
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
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                <asp:TextBox ID="txt_ComputerName" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False" ForeColor="Red"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                نام كامپيوتر</td>
        </tr>
        
        <tr>
       
            <td style="width: 250px; height: 30px">
        
                &nbsp;</td>
            <td colspan="3" dir="rtl">
        
                                     <asp:Button ID="btnOut" runat="server" CssClass="btn r5" Text="حذف" 
                                        Width="50px" Visible="False" />
                                  
                                </td>
            <td class="style2" style="height: 30px" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Cmbsystemg" runat="server" CssClass="txt_ r5" 
                                        Height="30px" TabIndex="9" Width="194px" Visible="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
          
            <td class="style7" style="text-align: left; color: #000000;">
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="قطعات  ثبت شده براي سيستم" Visible="False" 
                    Width="120px"></asp:Label>
&nbsp; </td>
        </tr>
        
        <tr>
       
            <td style="width: 250px; height: 30px">
        
                                    <asp:Button ID="btnIn" runat="server" CssClass="btn r5" 
                    Text="اضافه" Width="40px" Visible="False" />
                                </td>
            <td colspan="3" dir="rtl">
        
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Cmbgd" runat="server" CssClass="txt_ r5" Height="30px" 
                                                TabIndex="11" Visible="False" Width="194px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
            <td class="style2" style="height: 30px" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Cmbg" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Height="30px" TabIndex="10" 
                                        Width="194px" Visible="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
            
            <td class="style7" style="text-align: left; color: #000000;">
                <asp:Label ID="Label3" runat="server" Text="قطعات جديد" Visible="False"></asp:Label>
            </td>
        </tr>
        
       <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9" dir="rtl">
        
                                    <asp:TextBox ID="txt_Serial" runat="server" 
                    CssClass="txt_ltr r5 h25" ValidationGroup="a" Visible="False" Width="190px"></asp:TextBox>
                                </td>
            <td class="style10" style="width: 50px; height: 30px">
        
                                    <asp:Label ID="lblSerialGate" runat="server" 
                                        Text="سريال قطعه                                   " Visible="False" 
                                        Width="80px"></asp:Label>
                                </td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                                    <dx:ASPxTextBox ID="txt_AmvalTmp" runat="server" 
                    CssClass="txt_ltr r5 h25" Visible="False" Width="190px">
                                        <masksettings mask="000000" />
                                        <validationsettings display="None">
                                        </validationsettings>
                                    </dx:ASPxTextBox>
            </td>
            <td class="style8" style="text-align: left;">
                                    <asp:Label ID="lblAmvalGate" runat="server" 
                    Text="اموال قطعه" Visible="False" 
                                        Width="113px"></asp:Label>
            </td>
        </tr>

       <tr>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px">
                &nbsp;</td>
            <td style="width: 250px; height: 30px" class="style9" dir="rtl">
        
                                    <asp:TextBox ID="txt_SystemTP" runat="server" 
                    CssClass="txt_ltr r5 h25" ValidationGroup="a" Visible="False" Width="190px"></asp:TextBox>
                                </td>
            <td class="style10" style="width: 50px; height: 30px">
        
                                    <asp:Label ID="lblTP" runat="server" Text="تي پي قطعه" Visible="False" 
                                        Width="80px"></asp:Label>
                                </td>
            <td class="style2" style="width: 50px; height: 30px">
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                                    <asp:TextBox ID="txt_Mac" runat="server" 
                    CssClass="txt_ltr r5 h25" Visible="False" Width="190px"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                                    <asp:Label ID="lblMac" runat="server" Text="مك آدرس" Visible="False" 
                                        Width="113px"></asp:Label>
            </td>
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
                &nbsp;</td>
            <td class="style2" style="width: 200px; height: 30px; text-align: right;">
                                    <asp:TextBox ID="txt_Garanty" runat="server" 
                    CssClass="txt_ltr r5 h25" ValidationGroup="a" Visible="False" Width="190px"></asp:TextBox>
            </td>
            <td class="style8" style="text-align: left;">
                                    <asp:Label ID="lblGaranti" runat="server" 
                    Text="تاريخ گارانتي" Visible="False" 
                                        Width="113px"></asp:Label>
            </td>
        </tr>

        <tr>
       
            <td style="height: 30px" colspan="7">
        
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

