<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="EditHistory.aspx.vb" Inherits="EditHistory" %>

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
                <asp:TextBox ID="idd" runat="server" Width="100px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                کد پرسنل </div>
            <div class="div_txt">
                <asp:TextBox ID="codepersonel" runat="server" Enabled="False" Width="180px" 
                    CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                نام و نام خانوادگی</div>
            <div class="div_txt">
                <asp:TextBox ID="nam" runat="server" Enabled="False" Width="180px" 
                    CssClass="txt_ r5"></asp:TextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>

                <div class="div_line">
            <div class="div_nev">
                محل اشتغال</div>
            <div class="div_txt">
                <asp:DropDownList runat="server" CssClass="txt_ r5" Width="190px" 
                    ID="cmbSazman" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تاريخ شروع</div>
            <div class="div_txt">
                <asp:TextBox ID="startdate" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ltr r5 h25" 
                    MaxLength="10"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                        تاریخ خاتمه</div>
            <div class="div_txt">
                <asp:TextBox ID="enddate" runat="server" Enabled="False" MaxLength="10" 
                    Width="190px" CssClass="txt_ltr r5 h25"></asp:TextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                عنوان شغل</div>
                    <div class="div_txt">
                <asp:DropDownList runat="server" CssClass="txt_ r5" Width="190px" 
                    ID="cmbShoghl" Enabled="False">
                </asp:DropDownList>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        مدت کارکرد</div>
                    <div class="div_txt">
                        <dx:ASPxTextBox ID="modat" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="100px" Enabled="False">
                            <MaskSettings Mask="HH:mm">
                            </MaskSettings>
                            <masksettings mask="00-00-00" />
                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        سابقه بیمه</div>
                    <div class="div_txt">
                        <dx:ASPxTextBox ID="sabege" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="100px" Enabled="False">
                            <MaskSettings Mask="HH:mm">
                            </MaskSettings>
                            <masksettings mask="00-00-00" />
                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
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
                        KeyFieldName="id" 
                        Width="930px">
                        <settingsdetail allowonlyonemasterrowexpanded="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="id" FieldName="id" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <SettingsPager PageSize="15">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowPreview="True" />
                        <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                        <Styles>
                            <DetailRow BackColor="#CCFFCC">
                            </DetailRow>
                        </Styles>
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

