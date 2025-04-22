<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmJobAdmin.aspx.vb" Inherits="frmJobAdmin" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
.dxgvControl,
.dxgvDisabled
{
	border: Solid 1px #9F9F9F;
	font: 11px Tahoma;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvGroupPanel
{
	white-space: nowrap;
	font-size: 9pt;
	
	background-color: #EDEDED;
	color: #8D8D8D;
	border-bottom: Solid 1px #9F9F9F;
	padding: 7px 4px 8px 6px;
}

.dxgvTable
{
	background-color: White;
	border: 0;
	border-collapse: separate!important;
	overflow: hidden;
	font: 9pt Tahoma;
	color: Black;
}

.dxgvHeader {
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px 5px 6px;
	border: Solid 1px #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;	
}

.dxGridView_gvExpandedButton {
    background-position: -36px -18px;
    width: 9px;
    height: 10px;
}

.dxGridView_gvCollapsedButton,
.dxGridView_gvExpandedButton,
.dxGridView_gvDetailCollapsedButton,
.dxGridView_gvDetailExpandedButton,
.dxGridView_gvFilterRowButton,
.dxGridView_gvHeaderFilter,
.dxGridView_gvHeaderFilterActive,
.dxGridView_gvHeaderSortDown,
.dxGridView_gvHeaderSortUp,
.dxGridView_gvDragAndDropArrowDown,
.dxGridView_gvDragAndDropArrowUp,
.dxGridView_gvDragAndDropHideColumn,
.dxGridView_gvParentGroupRows,

.dxgvFilterRow 
{
	background-color: #E7E7E7;
}
.dxgvFocusedRow
{
	background-color: #8D8D8D;
    color: White;
}

.dxGridView_gvDetailCollapsedButton {
    background-position: -53px 0px;
    width: 9px;
    height: 10px;
}

.dxpControl
{
	font: 9pt Tahoma;
	color: black;
}
.dxpSummary
{
	font: 9pt Tahoma;
	color: black;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 1px 4px 0px 4px;
}
.dxpDisabled
{
	color: #acacac;
	border-color: #808080;
	cursor: default;
}

.dxpDisabledButton
{
	font: 9pt Tahoma;
	color: black;
	text-decoration: none;
}
.dxpButton
{
	font: 9pt Tahoma;
	color: #394EA2;
	text-decoration: underline;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	
}

.dxWeb_pPrevDisabled {
    background-position: -105px -25px;
    width: 16px;
    height: 17px;
}

.dxWeb_rpHeaderTopLeftCorner,
.dxWeb_rpHeaderTopRightCorner,
.dxWeb_rpBottomLeftCorner,
.dxWeb_rpBottomRightCorner,
.dxWeb_rpTopLeftCorner,
.dxWeb_rpTopRightCorner,
.dxWeb_rpGroupBoxBottomLeftCorner,
.dxWeb_rpGroupBoxBottomRightCorner,
.dxWeb_rpGroupBoxTopLeftCorner,
.dxWeb_rpGroupBoxTopRightCorner,
.dxWeb_mHorizontalPopOut,
.dxWeb_mVerticalPopOut,
.dxWeb_mSubMenuItem,
.dxWeb_mSubMenuItemChecked,
.dxWeb_nbCollapse,
.dxWeb_nbExpand,
.dxWeb_splVSeparator,
.dxWeb_splVSeparatorHover,
.dxWeb_splHSeparator,
.dxWeb_splHSeparatorHover,
.dxWeb_splVCollapseBackwardButton,
.dxWeb_splVCollapseBackwardButtonHover,
.dxWeb_splHCollapseBackwardButton,
.dxWeb_splHCollapseBackwardButtonHover,
.dxWeb_splVCollapseForwardButton,
.dxWeb_splVCollapseForwardButtonHover,
.dxWeb_splHCollapseForwardButton,
.dxWeb_splHCollapseForwardButtonHover,
.dxWeb_pcCloseButton,
.dxWeb_pcSizeGrip,
.dxWeb_pAll,
.dxWeb_pAllDisabled,
.dxWeb_pPrev,
.dxWeb_pPrevDisabled,
.dxWeb_pNext,
.dxWeb_pNextDisabled,
.dxWeb_pLast,
.dxWeb_pLastDisabled,
.dxWeb_pFirst,


.dxpCurrentPageNumber
{
	font: 9pt Tahoma;
	color: black;
	font-weight: bold;
	text-decoration: none;
	padding: 1px 3px 0px 3px;
}
.dxpPageNumber
{
	font: 9pt Tahoma;
	color: #394EA2;
	text-decoration: underline;
	text-align: center;
	vertical-align: middle;
	padding: 1px 5px 0px 5px;
}

.dxWeb_pNextDisabled {
    background-position: -81px -25px;
    width: 16px;
    height: 17px;
}

        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div_page" dir="rtl">
                    
  <div class="div_headp">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnExit0" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اكسل" Width="48px" 
                        ValidationGroup="b" />
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
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
    </div>
        <div class="div_line">
            <div class="div_nev">
                رديف</div>
            <div class="div_txt">
                <asp:TextBox ID="txtRadif" runat="server" Width="100px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                موضوع</div>
            <div class="div_txt">
                <asp:TextBox ID="txtsubject" runat="server" Enabled="False" Width="180px" 
                    CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                اولويت</div>
            <div class="div_txt">
                <asp:DropDownList runat="server" CssClass="txt_ r5 h25" Width="100px" 
                    ID="cmbolaviat" Enabled="False">
                    <asp:ListItem Value="0">بالا</asp:ListItem>
                    <asp:ListItem Value="1">معمولی</asp:ListItem>
                </asp:DropDownList>
            </div>
                    <div class="div_val">
            </div>
        </div>

                <div class="div_line">
            <div class="div_nev">
                تاريخ شروع</div>
            <div class="div_txt">
                <asp:TextBox ID="txtDateS" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ltr r5 h25" 
                    MaxLength="10"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                ساعت شروع</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txtTimeS" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="100px" Enabled="False">
                    <masksettings mask="000-000-000-000" />
<MaskSettings Mask="HH:mm"></MaskSettings>

                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                        درصد</div>
            <div class="div_txt">
                        <dx:ASPxTextBox ID="txtDarsad" runat="server" CssClass="txt_ltr r5 h25" 
                    Enabled="False" Width="100px">
                            <masksettings mask="000.00.000.000" />
<MaskSettings Mask="&lt;0..100&gt;"></MaskSettings>

                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                تاريخ انقضا</div>
                    <div class="div_txt">
                <asp:TextBox ID="txtDateE" runat="server" Enabled="False" MaxLength="10" 
                    Width="190px" CssClass="txt_ltr r5 h25"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        ساعت انقضا </div>
                    <div class="div_txt">
                        <dx:ASPxTextBox ID="txtTimeE" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="100px" Enabled="False">
                            <masksettings mask="000-000-000-000" />
                            <MaskSettings Mask="HH:mm">
                            </MaskSettings>
                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        نمايش براي همه</div>
                    <div class="div_txt">
                        <asp:CheckBox ID="checkShowAll" runat="server" Enabled="False" />
                    </div>
                    <div class="div_val">
                    </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                فرستنده</div>
            <div class="div_txt">
                <asp:DropDownList runat="server" CssClass="txt_ r5" Width="190px" 
                    ID="cmbSender" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                گيرنده</div>
            <div class="div_txt">
                <asp:DropDownList runat="server" CssClass="txt_ r5" Width="190px" 
                    ID="cmbreciver" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                        <asp:CheckBox ID="ChbActiveCode" runat="server" Text="غیر فعال" 
                            Enabled="False" TextAlign="Left" />
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
        <div class="div_nev">
                توضيحات</div>

                <div id="tozih" style=" float:right;
          width:800px;
          height:25px;
          text-align:right;
          vertical-align:middle;
          direction:rtl;">

                        <asp:TextBox ID="txtTozih" runat="server" Enabled="False" Width="750px" 
                            CssClass="txt_ r5"></asp:TextBox>

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

           <div class="div_grid">     


                    <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" CssClass="gr_" EnableCallBacks="False" 
                        KeyFieldName="id_job" OnHtmlDataCellPrepared="ASPxGrvHead_HtmlDataCellPrepared" 
                        PreviewFieldName="note" Width="930px">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                        <Columns>
                            <dx:GridViewDataTextColumn Name="attach_" ShowInCustomizationForm="True" 
                                VisibleIndex="11" Width="10px">
                                <dataitemtemplate>
                                    <asp:HyperLink ID="hiperMySend" runat="server" 
                                        NavigateUrl='<%# eval("file_url") %>' Target="_blank">فایل</asp:HyperLink>
                                </dataitemtemplate>
                                <headercaptiontemplate>
                                    <asp:Image ID="img_ok1" runat="server" Height="24px" 
                                        ImageUrl="~/images/design_images/attach.png" Width="24px" />
                                </headercaptiontemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ردیف" FieldName="id_job" VisibleIndex="1" 
                                Width="40px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataImageColumn Caption="وضعیت" Name="photo" 
                                ShowInCustomizationForm="True" VisibleIndex="8" Width="25px">
                                <dataitemtemplate>
                                    <asp:Image ID="Image5" runat="server" Height="24px" Width="24px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataImageColumn>
                            <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="sender_" 
                                ShowInCustomizationForm="True" VisibleIndex="7" Width="125px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="گیرنده" FieldName="ricever_" 
                                ShowInCustomizationForm="True" VisibleIndex="6" Width="125px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="پیشرفت" FieldName="darsad" 
                                ShowInCustomizationForm="True" VisibleIndex="5" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ اتمام" FieldName="date_end" 
                                ShowInCustomizationForm="True" VisibleIndex="4" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="date_start" 
                                ShowInCustomizationForm="True" VisibleIndex="3" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                                ShowInCustomizationForm="True" VisibleIndex="2" Width="180px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="انتخاب" FieldName="id_job" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="20px">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                        CommandArgument='<%# eval("id_job") %>' CommandName='<%# "select" %>' 
                                        Text='<%# "انتخاب" %>'></asp:LinkButton>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="olaviat" FieldName="olaviat" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" Width="0px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="date_end" Name="date_end" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="9" Width="0px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn Caption="غیرفعال" FieldName="CodeClosed" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="12" Width="0px">
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataTextColumn FieldName="stat" Visible="False" VisibleIndex="13" 
                                Width="0px">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <SettingsPager PageSize="15">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowPreview="True" />
                        <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                        <settingsdetail allowonlyonemasterrowexpanded="True" showdetailrow="True" />
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                        <Styles>
                            <DetailRow BackColor="#CCFFCC">
                            </DetailRow>
                        </Styles>
                        <templates>
                            <detailrow>
                                <dx:ASPxGridView ID="grddtlok" runat="server" AutoGenerateColumns="False" 
                                    ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="id_dtl" 
                                    OnBeforePerformDataSelect="grddtlok_BeforePerformDataSelect" Width="850px">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="detail_job" 
                                            VisibleIndex="0" Width="748px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Right">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id_dtl" Visible="False" VisibleIndex="1" 
                                            Width="2px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="زمان ثبت" FieldName="zaman" 
                                            VisibleIndex="2" Width="100px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settingspager pagesize="25" visible="False">
                                    </settingspager>
                                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                    <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                </dx:ASPxGridView>
                            </detailrow>
                        </templates>
                    </dx:ASPxGridView>


            </div>
     <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server">
                </dx:ASPxGridViewExporter>
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
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </div>
                    <div class="div_val">
            </div>
        </div>
              
    </div>
</asp:Content>

