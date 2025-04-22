<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTicketSysInfo.aspx.vb" Inherits="frmTicketSysInfo" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1" Namespace="DevExpress.Web.ASPxGridView"
    TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="dabir.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  
    <div class="div_page" dir="rtl">
        <div class="div_line">
            <div class="div_nev">
                شماره اموال</div>


            <div class="div_txt">

                <asp:TextBox ID="txt_SystemCode" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>

            </div>
            <div class="div_val">
            </div>


            <div class="div_nev">
                ساختمان
            </div>
            <div class="div_txt">
                <asp:TextBox ID="txt_bulding" runat="server" Width="190px" ValidationGroup="a" CssClass="txt_ r5"
                    Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                طبقه
            </div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemTabage" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                واحد</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_Systemunit" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تحويل گيرنده</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_holder" runat="server" Width="190px" ValidationGroup="a" CssClass="txt_ r5"
                    Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                نام اتاق</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemRoom" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                شماره اتاق</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemRoomNum" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تلفن</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemPhone" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                نام كامپيوتر</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_ComputerName" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False" ForeColor="Red"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                &nbsp; سرويس دوره اي</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_dateServiceDorei" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False" ForeColor="Red"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:Button ID="Button1" runat="server" Enabled="False" Font-Bold="True" 
                    ForeColor="Red" style="color: #3333FF" Text="سوابق سيستم" Width="190px" />
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                وضعيت</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_vaziat" runat="server" Width="190px" ValidationGroup="a" CssClass="txt_ r5"
                    Enabled="False" ForeColor="Red"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
        <div class="div_line">
        <div class="div_val">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev2">
                <asp:CheckBox ID="chkPlicy" runat="server" Text="Policy?" 
                    TextAlign="Left" Enabled="False" />
            </div>
            <div class="div_nev2">
                <asp:CheckBox ID="chkAntivirus" runat="server" Text="آنتي ويروس؟" 
                    TextAlign="Left" Enabled="False" />
            </div>
              <div class="div_nev2">
                <asp:CheckBox ID="chkjoinDomain" runat="server" Text="join Domain?" 
                    TextAlign="Left" Enabled="False" />
            </div>
              <div class="div_nev2">
                <asp:CheckBox ID="Chkpxe" runat="server" Text="PXE" TextAlign="Left" 
                    Enabled="False" />
            </div>
              <div class="div_nev2">
                <asp:CheckBox ID="chkusb" runat="server" Text="USB" TextAlign="Left" 
                    Enabled="False" />
            </div>
              <div class="div_nev2">
                <asp:CheckBox ID="chkcd" runat="server" Text="CD ROM" TextAlign="Left" 
                    Enabled="False" />
            </div>
              <div class="div_nev2">
                <asp:CheckBox ID="chkmodem" runat="server" Text="MODEM" TextAlign="Left" 
                    Enabled="False" />
            </div>
             <div class="div_nev2">
                <asp:CheckBox ID="chkfdd" runat="server" Text="FDD" TextAlign="Left" 
                    Enabled="False" />
               </div>
               
                    </div>
                    <div class="div_line">
            <div class="div_nev">

                      توضيحات</div>
                       <div class="div_txtnote">
                <asp:TextBox ID="txtNote" runat="server" Width="830px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
                        </div>
                        </div>
       
        <div id="grdGate" style="width: 948px; height: auto; text-align: center; vertical-align: middle;
            direction: ltr;">
            <dx:aspxgridview ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="id_rec" ClientIDMode="AutoID" Width="930px">
                <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                <Columns>
                    <dx:GridViewDataTextColumn Caption="IP" FieldName="SystemHDtlIp" VisibleIndex="0"
                        Width="12%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Justify">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="SystemHDtlMac" ShowInCustomizationForm="True" Width="15%" 
                        Caption="مك آدرس" VisibleIndex="1">
    <editcellstyle horizontalalign="Justify">
    </editcellstyle>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Justify"></CellStyle>
</dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاریخ گارانتی" FieldName="SystemHDtlGarantyDate"
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="5%">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاریخ حذف قطعه" FieldName="SystemHDtlDateOut"
                        VisibleIndex="3" Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption=" تاریخ نصب قطعه" FieldName="SystemHDtlDateIn"
                        VisibleIndex="4" Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SystemHDtlSerial" Width="5%" Caption="سريال"
                        VisibleIndex="5">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="TP شماره" FieldName="SystemHDtlTP" VisibleIndex="6"
                        Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Justify">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="اموال قطعه" FieldName="AmvalCode" VisibleIndex="7"
                        Width="8%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Justify">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="مدل" FieldName="BaseModelName" ShowInCustomizationForm="True"
                        VisibleIndex="8" Width="15%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام قطعه" FieldName="BaseSystemHDtlNmae" ShowInCustomizationForm="True"
                        VisibleIndex="9" Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="*" FieldName="SystemHDtlStatus" Visible="False"
                        VisibleIndex="10">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager PageSize="20">
                </SettingsPager>
                <Settings ShowFilterRow="True"></Settings>
            </dx:aspxgridview>
        </div>
        <br />
        <table align="center" cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td style="vertical-align: top">
                    <asp:GridView ID="grdsoftware" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                        TabIndex="12" Width="468px" Style="color: #000000; float: right;">
                        <Columns>
                            <asp:BoundField DataField="BaseSystemSDtlNmae" 
                                HeaderText="سيستم عامل و نرم افزارهاي روي سيستم" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td style="vertical-align: top">
                    <asp:GridView ID="grdDomainUsers" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
                        TabIndex="12" Width="468px" Style="color: #000000; float: left;">
                        <Columns>
                            <asp:BoundField DataField="UserName_" HeaderText="كاربران ثبت شده سيستم" />
                            <asp:BoundField DataField="nam" HeaderText="نام" />
                            <asp:BoundField DataField="UserEmail" HeaderText="ايميل" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <hr />
        <table align="center" cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td style="vertical-align: top">
                 
                <asp:GridView ID="grdDrive" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="468px" 
                         style="color: #000000; text-align: center;">
                                        <Columns>
                                            <asp:BoundField DataField="driveName" 
                                                HeaderText="درايو" />
                                            <asp:BoundField DataField="dirveTyp" HeaderText="نوع" />
                                            <asp:BoundField DataField="capacity_" HeaderText="فضاي كل" />
                                            <asp:BoundField HeaderText="پر" DataField="used_" />
                                            <asp:BoundField HeaderText="خالي" DataField="free_" />
                                            <asp:BoundField DataField="os" HeaderText="OS" />
                                        </Columns>
                                    </asp:GridView>
    
                </td>
            </tr>
        </table>
    </div>
    <%-- Expand from 0px to the appropriate size while fading in --%>

    </form>
</body>
</html>
