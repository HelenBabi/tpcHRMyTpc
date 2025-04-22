<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="FrmMassege.aspx.vb" Inherits="FrmMassege" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>صصصصصصصصصصصصصصصصصصصص</title>
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
    <%--    <script language="javascript" type="text/javascript">

        function openNewWin(url) {

            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');

            x.focus();

        }

</script>--%>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="position:relative; top:-26px; left: 1px; direction:rtl; ">

    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientIDMode="AutoID" Width="930px" 
        style="margin-top: 0px" TabIndex="3">
        <TabPages>
            <dx:TabPage Text="پیام های دريافتي من">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx1:ASPxGridView ID="grdJari" runat="server" AutoGenerateColumns="False" 
                            ClientIDMode="AutoID" EnableCallBacks="False" Width="928px"  OnHtmlDataCellPrepared="grdJari_HtmlDataCellPrepared" 
                            PreviewFieldName="note" KeyFieldName="id_Msg">
                            <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                            <Columns>
                                <dx1:GridViewDataTextColumn Caption="ردیف" FieldName="id_Msg" 
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="30px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn Caption="موضوع" FieldName="mozu" 
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn Caption="تاریخ ارسال" FieldName="date_start" 
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn Caption="فرستنده" FieldName="sender_" 
                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="150px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn Name="attach_" ShowInCustomizationForm="True" 
                                    VisibleIndex="8" Width="25px">
                                    <DataItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" 
                                            NavigateUrl='<%# eval("file_url") %>' Target="_blank">فایل</asp:HyperLink>
                                    </DataItemTemplate>
                                    <HeaderCaptionTemplate>
                                        <asp:Image ID="Image3" runat="server" Height="24px" 
                                            ImageUrl="~/images/design_images/attach.png" Width="24px" />
                                    </HeaderCaptionTemplate>
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn FieldName="date_end" ShowInCustomizationForm="True" 
                                    Visible="False" VisibleIndex="10" Name="date_end">
                                </dx1:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowFocusedRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                            <SettingsPager PageSize="15">
                            </SettingsPager>
                            <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowPreview="True" />
                            <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />

<Settings ShowFilterRow="True" ShowGroupPanel="True" ShowPreview="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                            <Styles>
                                <DetailRow BackColor="#CCFFCC">
                                </DetailRow>
                            </Styles>
                        </dx1:ASPxGridView>
                        <cc1:ModalPopupExtender ID="grdJari_ModalPopupExtender" runat="server" TargetControlID="LinkButton5" BackgroundCssClass="modalBackground1"
                            DynamicServicePath="" Enabled="True"  PopupControlID="panel1" CancelControlID="Image4">
                        </cc1:ModalPopupExtender>
                        <asp:LinkButton ID="LinkButton5" runat="server" ></asp:LinkButton>
                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="پیام های ارسال شده من">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <dx1:ASPxGridView ID="grdMySend" runat="server" AutoGenerateColumns="False" 
                                    ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="id_Msg" 
                                    OnHtmlDataCellPrepared="grdMySend_HtmlDataCellPrepared" PreviewFieldName="note" 
                                    Width="928px">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                                    <Columns>
                                        <dx1:GridViewDataTextColumn Caption="ردیف" FieldName="id_Msg" 
                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="30px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="موضوع" FieldName="mozu" 
                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="تاریخ ارسال" FieldName="date_start" 
                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="تاریخ دریافت" FieldName="date_start" 
                                            ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="گیرنده" FieldName="reciver_" 
                                            ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Name="attach_" ShowInCustomizationForm="True" 
                                            VisibleIndex="8">
                                            <DataItemTemplate>
                                                <asp:HyperLink ID="hiperMySend" runat="server" 
                                                    NavigateUrl='<%# eval("file_url") %>' Target="_blank">فایل</asp:HyperLink>
                                            </DataItemTemplate>
                                            <HeaderCaptionTemplate>
                                                <asp:Image ID="img_ok1" runat="server" Height="24px" 
                                                    ImageUrl="~/images/design_images/attach.png" Width="24px" />
                                            </HeaderCaptionTemplate>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn FieldName="date_start" Name="date_end" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn FieldName="stat" ShowInCustomizationForm="True" 
                                            Visible="False" VisibleIndex="11">
                                        </dx1:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowPreview="True" />
                                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" />
                                    <Styles>
                                        <DetailRow BackColor="#CCFFCC">
                                        </DetailRow>
                                    </Styles>
                                </dx1:ASPxGridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="ارسال پیام جدید">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <div class="div_line1">
            <div class="div_nev1">
                عنوان</div>
            <div class="div_txt1">
                <asp:TextBox ID="txtsubject" runat="server" Width="190px" 
                    CssClass="txt_ r5 h25"></asp:TextBox>
            </div>
            <div class="div_val1">
                <asp:RequiredFieldValidator ID="vald_Holdercode" runat="server" 
                    ControlToValidate="txtsubject" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev1">
            </div>
            <div class="div_txt1">
                <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                &nbsp;</div>
            <div class="div_val1">
            </div>
            <div class="div_nev1">
                <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
            </div>
            <div class="div_txt1">
                &nbsp;</div>
                    <div class="div_val1">
            </div>
        </div>
        <div class="div_line1">
            <div class="div_nev1">
                :گیرنده</div>
            <div class="div_txt1">
                <asp:DropDownList ID="cmbreciver" runat="server" CssClass="txt_ r5 h25" 
                    Width="190px">
                </asp:DropDownList>
            </div>
            <div class="div_val1">
            </div>
            <div class="div_nev1">
                </div>
            <div class="div_txt1">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="نمايش براي همه بعداز اتمام" />
            </div>
            <div class="div_val1">
            </div>
            <div class="div_nev1">
            </div>
            <div class="div_txt1">
            </div>
                    <div class="div_val1">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">

                      فايل ضميمه</div>
                       <div class="div_txtnote">
                           <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txt_ r5 h25" 
                               Width="500px" />
                        </div>
                        </div>
                        <div class="div_line">
            <div class="div_nev">

                      توضيحات</div>
                       <div class="div_txtnote">
                <asp:TextBox ID="txtNote" runat="server" Width="830px" 
                    ValidationGroup="a" CssClass="txt_ r5"></asp:TextBox>
                        </div>
                        </div>

                <div class="div_line1">
            <div class="div_nev1">
                </div>
            <div class="div_txt1">
            </div>
            <div class="div_val1">
            </div>
            <div class="div_nev1">
                <asp:Button ID="Button1" runat="server" CssClass="btn r5 " Font-Bold="False" 
                    Font-Size="12pt" Height="25px" Text="ثبت" ValidationGroup="a" Width="80px" />
            </div>
            <div class="div_txt1">
            </div>
            <div class="div_val1">
            </div>
            <div class="div_nev1">
            </div>
            <div class="div_txt1">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </div>
                    <div class="div_val1">
            </div>
        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <br />
    	<script type="text/javascript">
    	    var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
    	    //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

//    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
//    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
    
      <asp:Panel ID="Panel1" runat="server">
      <div class ="div_line ">
        <asp:Image ID="Image4" runat="server" 
            ImageUrl="~/images/design_images/dialog_close.png" />
    </div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
    <div class="div_grid" style="direction:rtl;">
                        <dx1:ASPxGridView runat="server" EnableCallBacks="False" 
            KeyFieldName="id_dtl" PreviewFieldName="detail_job" AutoGenerateColumns="False" 
            Width="928px" ClientIDMode="AutoID" ID="grddtl" 
            OnHtmlDataCellPrepared="grdJari_HtmlDataCellPrepared">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                            <Columns>
                                <dx1:GridViewDataTextColumn VisibleIndex="0" Width="828px">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="LinkButton6" runat="server" 
                                            CommandArgument='<%# eval("id_dtl") %>' CommandName='<%# "delete" %>' 
                                            ForeColor="Red">حذف</asp:LinkButton>
                                    </DataItemTemplate>
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn FieldName="id_dtl" Visible="False" VisibleIndex="1">
                                </dx1:GridViewDataTextColumn>
                                <dx1:GridViewDataTextColumn Caption="زمان ثبت" FieldName="zaman" 
                                    VisibleIndex="2" Width="100px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx1:GridViewDataTextColumn>
                            </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager PageSize="25"></SettingsPager>

<Settings ShowPreview="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>
</dx1:ASPxGridView>

    </div>
          <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
    <br />
    <div class="div_line">
            <div class="div_nev">

                      درج توضيحات</div>
                       <div class="div_txtnote">
                <asp:TextBox ID="txtNote0" runat="server" Width="830px" 
                    ValidationGroup="a" CssClass="txt_ r5"></asp:TextBox>
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
                <asp:Button ID="Button2" runat="server" CssClass="btn r5" Text="ثبت" 
                    Width="80px" />
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
        </ContentTemplate>
            </asp:UpdatePanel>
             </asp:Panel>     
             </div>
</asp:Content>

