<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmEditWorkHdr.aspx.vb" Inherits="frmEditWorkHdr" %>

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
<div class="div_headp" dir="rtl">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
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
                        ValidationGroup="a" TabIndex="17" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" TabIndex="16" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
</div> 
        <div class="div_line">
            <div class="div_nev">
                کد کار</div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                شماره اموال</div>
            <div class="div_txt">
     <asp:TextBox ID="txt_SystemCode" runat="server" Width="186px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5" 
            MaxLength="6"></asp:TextBox>
                      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
    EnableCaching="true"
  BehaviorID="AutoCompleteEx"
  MinimumPrefixLength="1"
  TargetControlID="txt_SystemCode"
  ServicePath="AutoComplete.asmx"
  ServiceMethod="GetCompletionList" 
  CompletionInterval="1000"  
  CompletionSetCount="20"
  CompletionListCssClass="autocomplete_completionListElement"
  CompletionListItemCssClass="autocomplete_listItem"
  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
  DelimiterCharacters=";, :"
  >
  <Animations>
  <OnShow>
  <Sequence>
  <%-- Make the completion list transparent and then show it --%>
  <OpacityAction Opacity="0" />
  <HideAction Visible="true" />

  <%--Cache the original size of the completion list the first time
    the animation is played and then set it to zero --%>
  <ScriptAction Script="// Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
  <%-- Expand from 0px to the appropriate size while fading in --%>
  <Parallel Duration=".4">
  <FadeIn />
  <Length PropertyKey="height" StartValue="0" 
	EndValueScript="$find('AutoCompleteEx')._height" />
  </Parallel>
  </Sequence>
  </OnShow>
  <OnHide>
  <%-- Collapse down to 0px and fade out --%>
  <Parallel Duration=".4">
  <FadeOut />
  <Length PropertyKey="height" StartValueScript=
	"$find('AutoCompleteEx')._height" EndValue="0" />
  </Parallel>
  </OnHide>
  </Animations>
    </cc1:AutoCompleteExtender>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                نوع سرويس</div>
            <div class="div_txt">
                                <asp:DropDownList ID="CmbWorkeType" runat="server" AutoPostBack="True" 
                                    CssClass="txt_ r5" Height="30px" TabIndex="1" Width="194px" 
                                    Enabled="False">
                                </asp:DropDownList>
            </div>
                    <div class="div_val">
              
                <asp:RequiredFieldValidator ID="vald_WorkeType1" runat="server" 
                    ControlToValidate="CmbWorkeType" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>

                <div class="div_line">
            <div class="div_nev">
                تاریخ شروع</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_WorkDateS" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ltr r5 h25" TabIndex="2" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrcode" runat="server" 
                    ControlToValidate="txt_SystemCode" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                ساعت شروع</div>
            <div class="div_txt">
                        <dx:ASPxTextBox ID="txt_WorkTimeS" runat="server" CssClass="txt_ltr r5 h25" 
                            Enabled="False" TabIndex="3" Width="170px">
                            <masksettings mask="HH:mm" />
                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname3" runat="server" 
                    ControlToValidate="txt_WorkTimeS" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                تاریخ&nbsp; اتمام</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_WorkDateE" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ltr r5 h25" TabIndex="4"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname2" runat="server" 
                    ControlToValidate="txt_WorkDateE" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                ساعت اتمام</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_WorkTimeE" runat="server" CssClass="txt_ltr r5 h25" 
                    Enabled="False" TabIndex="5" Width="170px">
                    <masksettings mask="HH:mm" />
                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname1" runat="server" 
                    ControlToValidate="txt_WorkTimeE" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                پاسخگو</div>
            <div class="div_txt">
                <asp:DropDownList ID="CmbWorkerCodeP" runat="server" Height="30px" 
                    Width="194px" CssClass="txt_ r5" TabIndex="6" Enabled="False">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Worker" runat="server" 
                    ControlToValidate="CmbWorkerCodeP" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                سرویسکار</div>
            <div class="div_txt">
                <asp:DropDownList ID="CmbWorkerCodeS" runat="server" Width="190px" 
                    CssClass="txt_ r5" TabIndex="7" Enabled="False">
                </asp:DropDownList>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Workers" runat="server" 
                    ControlToValidate="CmbWorkerCodeS" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                دلیل 
                انجام كار</div>
            <div id="txtDalil" style="   float:right;
                  width:843px;
          text-align:right;
          direction:rtl;
          vertical-align:middle;
          line-height:25px;
          height:25px;">
              
                <asp:TextBox ID="txtWorkDalil" runat="server" Width="700px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5" TabIndex="8"></asp:TextBox>
              
            </div>
          
        </div>
                <div class="div_line">
            <div class="div_nev">
                ريز سرويس</div>
           <div id="RizService" style="   float:right;
                  width:843px;
          text-align:right;
          direction:rtl;
          vertical-align:middle;
          line-height:25px;
           height:auto;">
                        <asp:CheckBoxList ID="CheckBox1" runat="server" RepeatColumns="5" 
                            style="color: #FF3300" TextAlign="Left" CssClass="txt_ r5" 
                    TabIndex="9">
                        </asp:CheckBoxList>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                توضیحات</div>
          <div id="tozihat" style="   float:right;
                  width:843px;
          text-align:right;
          direction:rtl;
          vertical-align:middle;
          line-height:25px;
           height:auto;">
    
                <asp:TextBox ID="txt_WorkTozih" runat="server" Width="700px" 
                    ValidationGroup="a" Height="25px" TextMode="MultiLine" CssClass="txt_ r5" 
                    TabIndex="13"></asp:TextBox>
    
            </div>
        </div>
                <div class="div_grid">
            
                    <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                        ClientIDMode="AutoID" KeyFieldName="WorkId" Width="948px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                        <clientsideevents focusedrowchanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="توضيحات" FieldName="WorkNote" 
                                VisibleIndex="9" Width="380px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرويسكار" FieldName="serviskar" 
                                ShowInCustomizationForm="True" VisibleIndex="8" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="پاسخگو" FieldName="pasoxgoo" 
                                VisibleIndex="7" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت اتمام" FieldName="WorkTimeE" 
                                VisibleIndex="6" Width="50px">
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاريخ اتمام" FieldName="WorkDateE" 
                                VisibleIndex="5" Width="50px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ساعت شروع" FieldName="WorkTimeS" 
                                VisibleIndex="4" Width="50px">
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاريخ شروع" FieldName="WorkDateS" 
                                ShowInCustomizationForm="True" VisibleIndex="3" Width="50px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع سرویس" FieldName="WorkTypeName" 
                                ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام کاربر" FieldName="HolderFamili" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="100px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="50px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="*" FieldName="WorkTypeCode" Visible="False" 
                                VisibleIndex="10">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                        <settingspager pagesize="20">
                        </settingspager>

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

                        <settings showfilterrow="True" showgrouppanel="True" />
                        <settingstext grouppanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />
                    <settingsdetail showdetailrow="True" />

                        <settingsdetail allowonlyonemasterrowexpanded="True" isdetailgrid="True" 
                            showdetailrow="True" />

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>

<SettingsDetail IsDetailGrid="True" AllowOnlyOneMasterRowExpanded="True" showdetailbuttons="False"></SettingsDetail>

                        <styles>
                            <detailrow backcolor="#CCFFCC">
                            </detailrow>
                        </styles>
                        <templates>
                            <detailrow>
                        <br />  <br />
                              <br />  <br />
                            </detailrow>
                        </templates>
                    </dx:ASPxGridView>
            
                    </div>
                    <div class="div_line">
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
                <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
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

