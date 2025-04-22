<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmWorkHldr.aspx.vb" Inherits="frmWorkHldr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <div class="div_page" dir="rtl">
    <style type="text/css">
        .style13
        {
            width: 760px;
        }
        .style14
        {
            width: 590px;
        }
    </style>

    <style type="text/css">
        .style1
        {
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div_page" dir="rtl">
<div class="div_headp">
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
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" TabIndex="14" />
                    <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
  </div>
        <div class="div_line">
            <div class="div_nev">
                شماره اموال</div>
            <div class="div_txt">
               
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_SystemCode" runat="server" CssClass="txt_ r5" 
                            Enabled="False" ValidationGroup="a" Width="190px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                            BehaviorID="AutoCompleteEx" CompletionInterval="1000" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                            CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                            DelimiterCharacters=";, :" EnableCaching="true" MinimumPrefixLength="1" 
                            ServiceMethod="GetCompletionList" ServicePath="AutoComplete.asmx" 
                            TargetControlID="txt_SystemCode">
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
                    </ContentTemplate>
    </asp:UpdatePanel>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrcode" runat="server" 
                    ControlToValidate="txt_SystemCode" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">




    <asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="images/design_images/kfilereplace.png" />




<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        TargetControlID="ImageButton1" PopupControlID="Panel1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
                نوع سرويس</div>
            <div class="div_txt">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="CmbWorkeType" runat="server" AutoPostBack="True" 
                                    CssClass="txt_ r5" Height="30px" TabIndex="1" Width="194px">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </div>
            <div class="div_val">
              
                <asp:RequiredFieldValidator ID="vald_WorkeType" runat="server" 
                    ControlToValidate="CmbWorkeType" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                تاریخ شروع</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_WorkDateS" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" TabIndex="2" Enabled="False"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname" runat="server" 
                    ControlToValidate="txt_WorkDateS" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                ساعت شروع</div>
            <div class="div_txt">
                        <asp:TextBox ID="txt_WorkTimeS" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" TabIndex="3"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname0" runat="server" 
                    ControlToValidate="txt_WorkTimeS" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                تاریخ&nbsp; اتمام</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_WorkDateE" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5" TabIndex="4"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname2" runat="server" 
                    ControlToValidate="txt_WorkDateE" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                ساعت اتمام</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_WorkTimeE" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" TabIndex="5"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrname1" runat="server" 
                    ControlToValidate="txt_WorkTimeE" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                پاسخگو</div>
            <div class="div_txt">
                <asp:DropDownList ID="CmbWorkerCodeP" runat="server" Height="30px" 
                    Width="194px" CssClass="txt_ r5" TabIndex="6">
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
                    CssClass="txt_ r5" TabIndex="7">
                </asp:DropDownList>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Workers" runat="server" 
                    ControlToValidate="CmbWorkerCodeS" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
          
          <div id="dalil" style=" width:948px;
          height:25px;  direction:rtl; ">
           <div class="div_nev">
               دليل انجام سرويس</div>
                <asp:TextBox ID="txtWorkDalil" runat="server" Width="500px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5" Height="25px"></asp:TextBox>
  </div>    
    <div id="karha" style=" width:948px;
          height:auto;">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="txt_ r5" 
                                            RepeatColumns="5" style="color: #FF3300" TextAlign="Left">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
        </div>  
         <div class="div_line">
            <div class="div_nev">
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" TabIndex="8" Text="تعویض قطعه" TextAlign="Left" />
            </div>
            <div class="div_txt">
                                    <asp:CheckBox ID="chbox_dorei" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" TabIndex="8" Text="سرویس دوره ای" TextAlign="Left" 
                                        Visible="False" Width="100px" />
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                                    <asp:HiddenField ID="hdn_tmp" runat="server" />
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
                    <div class="div_val">
            </div>
        </div>

         <div class="div_line">
            <div class="div_nev">
                                    <asp:Label ID="Label2" runat="server" Text="قطعات  سيستم" Visible="False" 
                                        Width="100px"></asp:Label>
            </div>
            <div class="div_txt">
                                    <asp:DropDownList ID="Cmbsystemg" runat="server" CssClass="txt_ r5" 
                                        Height="30px" TabIndex="9" Visible="False" 
                    Width="190px">
                                    </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                                     <asp:Button ID="btnOut" runat="server" CssClass="btn r5" Text="حذف" 
                                        Visible="False" Width="50px" />
                                  
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
                                    <asp:Label ID="lblNew" runat="server" 
                    Text="ليست قطعات جديد" Visible="False" 
                                        Width="100px"></asp:Label>
            </div>
            <div class="div_txt">
                                    <asp:DropDownList ID="Cmbg" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Height="30px" TabIndex="10" Visible="False" 
                                        Width="190px">
                                    </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                                    <asp:DropDownList ID="Cmbgd" runat="server" 
                    CssClass="txt_ r5" Height="30px" 
                                        TabIndex="11" Visible="False" Width="194px">
                                    </asp:DropDownList>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                                    <asp:Button ID="btnIn" runat="server" CssClass="btn r5" Text="ثبت موقت" 
                                        Visible="False" Width="100px" />
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
        <div class="div_grid">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="940px">
                                        <Columns>
                                            <asp:BoundField DataField="Tmp_id" HeaderText="#" />
                                            <asp:BoundField DataField="SystemCode" HeaderText="کد سیستم" Visible="False" />
                                            <asp:BoundField DataField="BaseSystemHDtlCode" HeaderText="کد سخت افزار" 
                                                Visible="False" />
                                            <asp:BoundField DataField="BaseModelCode" HeaderText="کد مدل" Visible="False" />
                                            <asp:BoundField DataField="INOUTNOTE" HeaderText="نوع" />
                                            <asp:BoundField DataField="id_rec_note" HeaderText="قطعه حذف شده" />
                                            <asp:BoundField DataField="BaseSystemHDtlNmae" HeaderText="قطعه جدید" />
                                            <asp:BoundField DataField="BaseModelName" HeaderText="مدل" />
                                            <asp:ButtonField CommandName="del" Text="حذف" />
                                        </Columns>
                                    </asp:GridView>
        </div>
        <div id="tozihat" style=" width:948px;
          height:auto;">
          <div class="div_nev">
              توضيحات</div>
                <asp:TextBox ID="txt_WorkTozih" runat="server" Width="700px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
          </div>
           <div class="div_grid">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False" 
                    EnableCallBacks="False" KeyFieldName="SystemCode" Width="100%" 
                    ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="شماره اموال" FieldName="SystemCode" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("SystemCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("SystemCode") %>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="HolderName" 
                            VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="HolderFamili" 
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساختمان" FieldName="BuildingName" 
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="واحد" FieldName="UnitsName" 
                            VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="اتاق" FieldName="SystemRoom" 
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="SystemPhone" 
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowFilterRow="True" />
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
    

    </asp:Content>

