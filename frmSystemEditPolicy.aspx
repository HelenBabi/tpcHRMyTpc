<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmSystemEditPolicy.aspx.vb" Inherits="frmSystemEditPolicy" %>

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
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
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
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" Visible="False" Enabled="False" />
  </div>
        <div class="div_line">
            <div class="div_nev">
                شماره اموال</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_SystemCode" runat="server" AutoCompleteType="HomePhone" 
                    BackColor="#FFFFCC" HorizontalAlign="Left" MaxLength="6" Width="150px" 
                    Enabled="False" Height="30px">
                    <validationsettings enablecustomvalidation="True" 
                        errortext="6 رقمي باشد">
                        <regularexpression errortext="شماره اموال بايد 6 رقمي باشد" 
                            validationexpression="\d{6}" />
<RegularExpression ErrorText="اموال 6 رقمي بايدباشد" ValidationExpression="\d{6}"></RegularExpression>
                    </validationsettings>
                </dx:ASPxTextBox>
            
            </div>
            <div class="div_val">
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
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                طبقه</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemTabage" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemcode0" runat="server" 
                    ControlToValidate="txt_SystemTabage" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                واحد</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbUnit" runat="server" Width="190px" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname1" runat="server" 
                    ControlToValidate="cmbUnit" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                تحويل گيرنده</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbHolder" runat="server" Width="190px" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname4" runat="server" 
                    ControlToValidate="cmbHolder" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                نام
                اتاق 
            </div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemRoom" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname3" runat="server" 
                    ControlToValidate="txt_SystemRoom" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                شماره اتاق</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemRoomNum" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname8" runat="server" 
                    ControlToValidate="txt_SystemRoomNum" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                تلفن</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemPhone" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname5" runat="server" 
                    ControlToValidate="txt_SystemPhone" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                نام كامپيوتر</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_ComputerName" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False" ForeColor="Red"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname9" runat="server" 
                    ControlToValidate="txt_ComputerName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                تي پي</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_SystemTP" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                رده خارج</div>
            <div class="div_txt">
                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" />
            
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                
                        <asp:Button ID="Button1" runat="server" Text="ايجاد نام كامپيوتر" 
                    ValidationGroup="a" Font-Bold="True" Width="190px" Enabled="False" />
                
            </div>
                    <div class="div_val">
            </div>
        </div>
        <div class="div_line">
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
                <asp:CheckBox ID="chkPXE" runat="server" Text="PXE" TextAlign="Left" 
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
           <div id="grdGate" style=" 
            width:948px;
          height:auto;
          text-align:center;
          vertical-align:middle;

          direction:rtl;">

                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="SystemCode" Width="100%" 
                    ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("SystemCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("SystemCode") %>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="HolderName" 
                            VisibleIndex="1">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="HolderFamili" 
                            VisibleIndex="2">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساختمان" FieldName="BuildingName" 
                            VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" 
                            VisibleIndex="4">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="اتاق" FieldName="SystemRoom" 
                            VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="SystemPhone" 
                            VisibleIndex="6">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="30">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>

<SettingsDetail ShowDetailRow="True" allowonlyonemasterrowexpanded="True" ShowDetailButtons="False"></SettingsDetail>
                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                </dx:ASPxGridView>

        </div>

         <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">

    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />

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

    </div>
    
       
            
    </asp:Content>

