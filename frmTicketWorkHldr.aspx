<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmTicketWorkHldr.aspx.vb" Inherits="frmTicketWorkHldr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div_headp">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/bargasht.png" ToolTip="خروج" Width="48px" />
                                 <asp:ImageButton ID="btnEtmam" runat="server" Height="48px" 
                                     ImageUrl="~/images/Icons/done.png" Width="48px" 
                    meta:resourcekey="btnEtmamResource1" ToolTip="اتمام کار" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Images/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" 
                        Visible="False" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" 
                        ImageUrl="~/Icons/save.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="نصب نرم افزارهای انتخاب شده" Visible="False" />
                    <br />
                 
  </div>
       <table class="style1">
                        <tr>
                            <td>
                            
                                        <table align="center" cellpadding="0" cellspacing="0" class="style13" dir="rtl" 
                                            style="color: #000000; width: 710px;" width="710">
                                            <tr>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td dir="ltr" width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td width="110">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100px" width="50">
                                                    شماره اموال</td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txt_SystemCode" runat="server" CssClass="txt_ r5" 
                                                        Enabled="False" MaxLength="6" ValidationGroup="a" Width="190px"></asp:TextBox>
                                                </td>
                                                <td width="50">
                                                    &nbsp;</td>
                                                <td colspan="2" dir="ltr" style="width: 100px" width="50">
                                                    &nbsp;</td>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                                <td width="110">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="14" style="width: 160px" width="50">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="txt_ r5" 
                                                        RepeatColumns="5" style="color: #3333FF" TabIndex="9" TextAlign="Left">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100px">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                    &nbsp;</td>
                                                <td width="60">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100px" width="50">
                                                    توضيحات</td>
                                                <td colspan="11">
                                                    <asp:TextBox ID="txt_WorkTozih" runat="server" CssClass="txt_ r5" Height="50px" 
                                                        TabIndex="13" TextMode="MultiLine" ValidationGroup="a" Width="550px"></asp:TextBox>
                                                </td>
                                                <td width="110">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="14" dir="ltr">
                     
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table align="right" cellpadding="0" cellspacing="0" class="style14" dir="rtl">
                                                                <tr>
                                                                    <td colspan="7" dir="rtl">
                                                                        &nbsp;</td>
                                                                    <td width="50">
                                                                        &nbsp;</td>
                                                                    <td width="50">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100px">
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                                                            CssClass="txt_ r5" TabIndex="10" Text="تعویض قطعه" TextAlign="Left" 
                                                                            Visible="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chbox_dorei" runat="server" AutoPostBack="True" 
                                                                            CssClass="txt_ r5" Enabled="False" ForeColor="Blue" TabIndex="11" 
                                                                            Text="سرویس دوره ای" TextAlign="Left" Visible="False" Width="150px" />
                                                                    </td>
                                                                    <td style="width: 100px" width="50">
                                                                        &nbsp;</td>
                                                                    <td colspan="3" style="width: 90px">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100px">
                                                                        <asp:Label ID="Label2" runat="server" Text="قطعات فعلي سيستم" Visible="False" 
                                                                            Width="123px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Cmbsystemg" runat="server" CssClass="txt_ r5" 
                                                                            Height="30px" TabIndex="12" Visible="False" Width="194px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 100px" width="50">
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="btnOut" runat="server" CssClass="btn r5" Height="30px" 
                                                                            TabIndex="13" Text="حذف" Visible="False" Width="50px" />
                                                                    </td>
                                                                    <td width="50">
                                                                        &nbsp;</td>
                                                                    <td colspan="2" style="width: 90px" width="50">
                                                                        <asp:HiddenField ID="hdn_tmp" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100px">
                                                                        <asp:Label ID="lblNew" runat="server" Text="ليست قطعات جديد" Visible="False" 
                                                                            Width="113px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="Cmbg" runat="server" AutoPostBack="True" 
                                                                                    CssClass="txt_ r5" Height="30px" TabIndex="14" Visible="False" Width="194px">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="Cmbgd" runat="server" CssClass="txt_ r5" Height="30px" 
                                                                                    TabIndex="11" Visible="False" Width="194px">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td width="60">
                                                                        <asp:Button ID="btnIn" runat="server" CssClass="btn r5" Text="اضافه" 
                                                                            Visible="False" Width="40px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100px">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td colspan="3">
                                                                        &nbsp;</td>
                                                                    <td width="60">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100px">
                                                                        <asp:Label ID="lblAmvalGate" runat="server" Text="اموال قطعه" Visible="False" 
                                                                            Width="113px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_amvaltmp" runat="server" CssClass="txt_ltr r5 h25" 
                                                                            Visible="False" Width="190px"></asp:TextBox>
                                                                    </td>
                                                                    <td colspan="3" dir="ltr">
                                                                        <asp:Label ID="lblSerialGate" runat="server" 
                                                                            Text="سريال قطعه                                   " Visible="False" 
                                                                            Width="113px"></asp:Label>
                                                                    </td>
                                                                    <td width="60">
                                                                        <asp:TextBox ID="txt_Serial" runat="server" CssClass="txt_ltr r5 h25" 
                                                                            ValidationGroup="a" Visible="False" Width="190px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="width: 100px">
                                                                        <asp:Label ID="lblMac" runat="server" Text="مك آدرس" Visible="False" 
                                                                            Width="113px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Mac" runat="server" CssClass="txt_ltr r5 h25" 
                                                                            Visible="False" Width="190px"></asp:TextBox>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="lblTP" runat="server" Text="تي پي قطعه" Visible="False" 
                                                                            Width="113px"></asp:Label>
                                                                    </td>
                                                                    <td width="60">
                                                                        <asp:TextBox ID="txt_SystemTP" runat="server" CssClass="txt_ltr r5 h25" 
                                                                            ValidationGroup="a" Visible="False" Width="190px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px" width="50">
                                                                        <asp:Label ID="lblGaranti" runat="server" Text="تاريخ گارانتي" Visible="False" 
                                                                            Width="113px"></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txt_Garanty" runat="server" CssClass="txt_ltr r5 h25" 
                                                                            ValidationGroup="a" Visible="False" Width="190px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px" width="50">
                                                                        &nbsp;</td>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="7" dir="ltr">
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                                            CellPadding="4" Font-Size="10pt" ForeColor="#333333" GridLines="None" 
                                                                            TabIndex="12" Width="700px">
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
                                                                </caption>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100px" width="50">
                                                    &nbsp;</td>
                                                <td colspan="11">
                                                    &nbsp;</td>
                                                <td width="110">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                                        </td>
                        </tr>
                    </table>
    </asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>


