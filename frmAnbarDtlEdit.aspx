<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmAnbarDtlEdit.aspx.vb" Inherits="frmAnbarDtlEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
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
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" Visible="False" />
  </div>
                <script src="js-persian-cal.min.js" type="text/javascript"></script>
    <link href="js-persian-cal.css" rel="stylesheet" type="text/css" />
    
              <script language="javascript" type="text/javascript">

                  function openNewWin(inp1, inp2) {
                      var div_ = document.getElementById("pcal1");
                      div_.setAttribute('value', inp1);
                      var div_ = document.getElementById("pcal2");
                      div_.setAttribute('value', inp2);
                      //            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');

                      //            div_.focus();

                  }

</script>

              <div class="div_line">
            <div class="div_nev">
                ليست انبار</div>
            <div class="div_txt">
                <asp:DropDownList ID="CmbAnbar" runat="server" AutoPostBack="True" 
                    CssClass="txt_ r5" Height="30px" TabIndex="10" Width="190px" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname12" runat="server" 
                    ControlToValidate="CmbAnbar" ErrorMessage="*" ForeColor="Red" InitialValue="0" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                کد تراکنش</div>
            <div class="div_txt">
                <asp:TextBox ID="txtidkala" runat="server" CssClass="txt_ltr r5 h25" Enabled="False" ValidationGroup="a" Width="180px"></asp:TextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                لیست قطعات</div>
            <div class="div_txt">
                                            
                <asp:DropDownList ID="Cmbg" runat="server" AutoPostBack="True" 
                    CssClass="txt_ r5" Height="30px" TabIndex="10" Width="190px" Enabled="False">
                </asp:DropDownList>
                                            
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname6" runat="server" 
                    ControlToValidate="Cmbg" ErrorMessage="*" ForeColor="Red" InitialValue="0" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                مدل</div>
            <div class="div_txt">
                <asp:DropDownList ID="Cmbgd" runat="server" CssClass="txt_ r5" 
                    Height="30px" TabIndex="11" Width="190px" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
        
                <asp:RequiredFieldValidator ID="vald_Systemname7" runat="server" 
                    ControlToValidate="Cmbgd" ErrorMessage="*" ForeColor="Red" InitialValue="0" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
        
            </div>
            <div class="div_nev">
                تاريخ&nbsp;ورود/خروج</div>
            <div class="div_txt">
                &nbsp;<asp:TextBox ID="txt_Tarixx" runat="server" CssClass="txt_ltr r5 h25" 
                    ValidationGroup="a" Width="180px" Enabled="False"></asp:TextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                اموال قطعه</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_AmvalTmp" runat="server" CssClass="txt_ltr r5 h25" 
                    Width="190px" Enabled="False">
                    <masksettings mask="000000" />
                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                                    سریال قطعه</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_Serial" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ltr r5 h25" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                    MRS</div>
<div id="txtMaac" style="   float:right;
          width:200px;
          text-align:left;
          direction:ltr;
          vertical-align:middle;
          line-height:25px;
          height:25px;">
                <asp:TextBox ID="txt_Mrs" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ltr r5 h25" Enabled="False"></asp:TextBox>
           </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                تاريخ گارانتي</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_Garanty" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ltr r5 h25" 
                    MaxLength="10" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                                    تاریخ وارانتي</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_waranti" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ltr r5 h25" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
        
                              
                                            تاريخ اسقاطي
                                    
            </div>
            <div class="div_txt">
                <asp:TextBox ID="txt_sgati" runat="server" CssClass="txt_ltr r5 h25" 
                    ValidationGroup="a" Width="190px" Enabled="False"></asp:TextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                ساختمان</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbBulding" runat="server" Width="190px" 
                    ValidationGroup="e" CssClass="txt_ r5" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Systemname9" runat="server" 
                    ControlToValidate="cmbBulding" ErrorMessage="*" ForeColor="Red" 
                    InitialValue="0" ValidationGroup="e"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                طبقه</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_SystemTabage" runat="server" CssClass="txt_ltr r5 h25" 
                    Width="190px" Enabled="False">
                    <masksettings mask="&lt;0..5&gt;" />
                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                واحد</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbUnit" runat="server" Width="190px" CssClass="txt_ r5" Enabled="False">
                </asp:DropDownList>
            </div>
                    <div class="div_val">
                        <asp:RequiredFieldValidator ID="vald_Systemname10" runat="server" 
                            ControlToValidate="cmbUnit" ErrorMessage="*" ForeColor="Red" 
                            ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                نوع تراکنش</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbType" runat="server" Width="190px" CssClass="txt_ r5" Font-Bold="True" Font-Size="12pt" Enabled="False">
                    <asp:ListItem Value="1">ورود به انبار</asp:ListItem>
                    <asp:ListItem Value="2">خروج از انبار</asp:ListItem>
                    <asp:ListItem Value="3">رده خارج</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                وضعیت</div>
            <div class="div_txt">
                <asp:DropDownList ID="cmbStock" runat="server" CssClass="txt_ r5" Font-Bold="True" Font-Size="12pt" Width="190px" Enabled="False">
                    <asp:ListItem Value="0">نو</asp:ListItem>
                    <asp:ListItem Value="1">دست دوم</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تعداد</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_tedad" runat="server" CssClass="txt_ltr r5 h25" 
                    Text="0" Width="100px" BackColor="#FFFF99" Enabled="False">
                    <masksettings mask="000000" />
                    <MaskSettings Mask="000000000" />
                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
                    <div class="div_val">
                        <asp:RequiredFieldValidator ID="vald_Systemname11" runat="server" 
                            ControlToValidate="txt_tedad" ErrorMessage="*" ForeColor="Red" InitialValue="0" 
                            ValidationGroup="a"></asp:RequiredFieldValidator>
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

                        <div class="div_grid" dir="rtl">
                            <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="IddtlKala">
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settings gridlines="Horizontal" showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <SettingsBehavior AllowFocusedRow="True" />
                                <Settings ShowFilterRow="True" />
                                <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settings showfilterrow="True" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settings gridlines="Horizontal" showgroupbuttons="False" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                    showgroupbuttons="False" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <settingsbehavior allowfocusedrow="True" />
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                                <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="کد تراکنش" FieldName="IddtlKala" VisibleIndex="0">
                                        <dataitemtemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# eval("IddtlKala") %>' CommandName='<%# "select" %>' Text='<%# eval("IddtlKala") %>'></asp:LinkButton>
                                        </dataitemtemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاريخ" FieldName="WorkDateE" VisibleIndex="1" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Left">
                                        </cellstyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="اموال" FieldName="amval" ShowInCustomizationForm="True" VisibleIndex="2" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="سريال" FieldName="serial" VisibleIndex="3" Width="100px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="mrs" FieldName="mrs" VisibleIndex="4" Width="100px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاريخ گارانتي" FieldName="tarixgaranti" VisibleIndex="5" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Left">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاريخ وارانتي" FieldName="tarixwaranti" VisibleIndex="6" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تاريخ اسقاطي" FieldName="tarixskati" VisibleIndex="7" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ساختمان" FieldName="BuildingName" VisibleIndex="9" Width="50px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="نام قطعه" FieldName="BaseSystemHDtlNmae" VisibleIndex="13" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Left">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="مدل" FieldName="BaseModelName" VisibleIndex="11">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="تعداد" FieldName="dtltedad" VisibleIndex="14" Width="10px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="توضيحات" FieldName="WorkNote" VisibleIndex="15" Width="190px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Right">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="TypeCode" FieldName="TypeCode" Visible="False" VisibleIndex="17">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ثبت کننده" FieldName="karbar" VisibleIndex="16" Width="100px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <cellstyle horizontalalign="Center">
                                        </cellstyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="نوع تراکنش" FieldName="type" VisibleIndex="18" Width="50px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="وضعیت " FieldName="value" VisibleIndex="19" Width="50px">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <settingsbehavior allowfocusedrow="True" />
                                <settingspager alwaysshowpager="True" mode="ShowAllRecords" pagesize="25">
                                </settingspager>
                                <settings gridlines="Horizontal" showfilterrow="True" 
                                        showgroupbuttons="False" />
                            </dx:ASPxGridView>
                            </div>
        
                
                
                <div class="div_line">
            <div class="div_nev">
                    </div>
            <div class="div_txt">
                <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:Label ID="lblTotal" runat="server" style="color: #FF3300" Text="Label" 
                    Visible="False"></asp:Label>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                
    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            
            </div>
                    <div class="div_val">
            </div>
        </div>
        
        

    

         
    
        </div>
    </div>
    <br />
    <script type="text/javascript">
        var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
        var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
        //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

        //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
        //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
    </asp:Content>

