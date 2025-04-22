<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmNetworkHardWare.aspx.vb" Inherits="frmNetworkHardWare" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="div_page" dir="rtl">
 <div class="div_headp">
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
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" />
  </div>
        <div class="div_line">
            <div class="div_nev">
                اموال Rack</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_SystemCode" runat="server" AutoCompleteType="HomePhone" 
                    BackColor="#FFFFCC" HorizontalAlign="Left" MaxLength="6" Width="170px" 
                    Enabled="False" CssClass="txt_ltr r5 h25">
                    <validationsettings enablecustomvalidation="True" 
                        errortext="6 رقمي باشد">
                        <regularexpression errortext="شماره اموال بايد 6 رقمي باشد" 
                            validationexpression="\d{6}" />
<RegularExpression ErrorText="اموال 6 رقمي بايدباشد" ValidationExpression="\d{6}"></RegularExpression>
                    </validationsettings>
                </dx:ASPxTextBox>
            
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemcode" runat="server" 
                    ControlToValidate="txt_SystemCode" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="b"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                         
                
                
                ساختمان</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbBulding" runat="server" Width="190px" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname2" runat="server" 
                    ControlToValidate="cmbBulding" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="b" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                
                        <asp:Button ID="Button1" runat="server" Text="ايجاد نام Rack" 
                    ValidationGroup="b" Font-Bold="True" Width="90px" Enabled="False" />
                
            </div>
            <div class="div_txt">
                <asp:TextBox ID="txt_ComputerName" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ltr r5 h25" Enabled="False" 
                    ForeColor="Red"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname9" runat="server" 
                    ControlToValidate="txt_ComputerName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
        
        <div class="div_nev">
                   توضيحات</div>

        <div id="Divnote" style="   float:right;
          width:800px;
          text-align:left;
          direction:rtl;
          vertical-align:middle;
          line-height:25px;
          height:25px;">
                <asp:TextBox ID="TxtNote" runat="server" Width="780px" 
                    ValidationGroup="a" Enabled="False"></asp:TextBox>
           </div>

           <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
    

         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>
                <div class="div_line">
            <div class="div_nev">
                لیست قطعات</div>
            <div class="div_txt">
                                            
                <asp:DropDownList ID="Cmbg" runat="server" AutoPostBack="True" 
                    CssClass="txt_ r5" Enabled="False" Height="30px" TabIndex="10" Width="190px">
                </asp:DropDownList>
                                            
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname6" runat="server" 
                    ControlToValidate="Cmbg" ErrorMessage="*" ForeColor="Red" InitialValue="0" 
                    ValidationGroup="c"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                مدل</div>
            <div class="div_txt">
                <asp:DropDownList ID="Cmbgd" runat="server" CssClass="txt_ r5" Enabled="False" 
                    Height="30px" TabIndex="11" Width="190px">
                </asp:DropDownList>
            </div>
            <div class="div_val">
        
                <asp:RequiredFieldValidator ID="vald_Systemname7" runat="server" 
                    ControlToValidate="Cmbgd" ErrorMessage="*" ForeColor="Red" InitialValue="0" 
                    ValidationGroup="c"></asp:RequiredFieldValidator>
        
            </div>
            <div class="div_nev">
                    </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
        
           <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
    <div class="div_line">
            <div class="div_nev">
                اموال قطعه</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_AmvalTmp" runat="server" CssClass="txt_ltr r5 h25" 
                    Enabled="False" Width="190px">
                    <masksettings mask="000000" />
                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                <asp:Button ID="Button2" runat="server" Enabled="False" Font-Bold="True" 
                    Text="ايجاد نام قطعه" ValidationGroup="b" Width="90px" />
            </div>
            <div class="div_txt">
                <asp:TextBox ID="txt_Serial" runat="server" CssClass="txt_ltr r5 h25" 
                    Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                <asp:Button ID="btnIn" runat="server" CssClass="btn r5" Enabled="False" 
                    Text="ثبت موقت" ValidationGroup="c" Width="90px" />
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>

         
           <div id="grdGate" style=" 
            width:948px;
          height:auto;
          text-align:center;
          vertical-align:middle;

          direction:ltr;">

                                   
                                   
                                            <asp:HiddenField ID="hdn_tmp" runat="server" />
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                Font-Size="10pt" style="color: #000000" TabIndex="12" Width="948px" 
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                                CellPadding="3">
                                                <Columns>
                                                    <asp:BoundField DataField="Tmp_id" HeaderText="#">
                                                    <ItemStyle ForeColor="White" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SystemCode" HeaderText="کد سیستم" Visible="False" />
                                                    <asp:BoundField DataField="BaseSystemHDtlCode" HeaderText="کد سخت افزار" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="BaseModelCode" HeaderText="کد مدل" Visible="False" />
                                                    <asp:BoundField DataField="Serial" HeaderText="نام قطعه">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="id_rec_note" HeaderText="شماره اموال" />
                                                    <asp:BoundField DataField="BaseModelName" HeaderText="مدل" />
                                                    <asp:BoundField DataField="BaseSystemHDtlNmae" HeaderText="قطعه جدید" />
                                                    <asp:ButtonField CommandName="del" Text="حذف" />
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
    
        </div>
    </div>
    <br />

    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            
    </asp:Content>

