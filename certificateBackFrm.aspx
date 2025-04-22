<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false"
    CodeFile="certificateBackFrm.aspx.vb" Inherits="certificateBackFrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 950px;
        }
    .auto-style1 {
        width: 100%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Expand from 0px to the appropriate size while fading in --%>
    <script language="javascript" type="text/javascript">
        function intFormat(number) {
            var regex = /(\d)((\d{3},?)+)$/;
            number = number.split(',').join('');
            while (regex.test(number)) {
                number = number.replace(regex, '$1,$2');
            }
            return number;
        }
        function numFormat(number) {
            var pointReg = /([\d,\.]*)\.(\d*)$/, f;
            if (pointReg.test(number)) {
                f = RegExp.$2;
                return intFormat(RegExp.$1) + '.' + f;
            }
            return intFormat(number);
        }
    </script>
    <div class="div_page" dir="rtl">
        <div class="div_headp">
            <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" ImageUrl="~/Icons/Exit.png"
                ToolTip="خروج" Width="48px" />
            <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" ImageUrl="~/Icons/Print.png"
                ToolTip="چاپ" Width="48px" ValidationGroup="b" Visible="False" />
            <asp:ImageButton ID="ibtnDeactived" runat="server" Height="48px" ImageUrl="~/Icons/reload.png"
                ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" Visible="False" />
            <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" ImageUrl="~/Icons/history.png"
                ToolTip="سوابق سیستم" Width="48px" ValidationGroup="b" Visible="False" />
            <asp:ImageButton ID="ibtnDeleted" runat="server" Height="48px" ImageUrl="~/Icons/deleted.png"
                ToolTip="لیست کدهای حذف شده" Width="48px" ValidationGroup="b" Visible="False" />
            <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" ImageUrl="~/Icons/Delete.png"
                ToolTip="حذف" Width="48px" ValidationGroup="a" Visible="False" />
            <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" ImageUrl="~/Icons/Cancel2.png"
                ToolTip="انصراف" Width="48px" ValidationGroup="b" Visible="False" />
            <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" ImageUrl="~/Icons/savenew2.png"
                ToolTip="ذخیره و جدید" Width="48px" ValidationGroup="a" Visible="False" />
            <asp:ImageButton ID="ibtnSave" runat="server" Height="48px" ImageUrl="~/Icons/save2.png"
                ToolTip="ذخیره" Width="48px" ValidationGroup="a" Visible="False" />
            <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" ImageUrl="~/Icons/edit.png"
                ToolTip="ویرایش" Width="48px" ValidationGroup="a" Visible="False" />
            <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" ImageUrl="~/Icons/Add.png"
                ToolTip="جدید" Width="48px" ValidationGroup="b" Visible="False" />
        </div>
          <div class="div_headp">
             <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="#FF3300" Text="فرم ثبت ابطال و برگشت ازبانک"></asp:Label>
             </div>
        <div class="div_line">
              
            <div class="div_nev">
                کد پرسنل</div>


            <div class="div_txt">

                                 <asp:Panel ID="Panel3" runat="server" Width="190px" DefaultButton="ImageButton1">
                                     <asp:TextBox ID="codepersonel" 
    runat="server" Width="150px" ValidationGroup="a"
                    CssClass="txt_ r5" MaxLength="6"></asp:TextBox>
                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" 
    runat="server" BehaviorID="AutoCompleteEx"
                    CompletionInterval="1000" CompletionListCssClass="autocomplete_completionListElement"
                    
    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                    CompletionSetCount="20" DelimiterCharacters=";, :" 
    EnableCaching="true" MinimumPrefixLength="1"
                    ServiceMethod="GetCompletionList" 
    ServicePath="AutoComplete.asmx" TargetControlID="codepersonel">
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
                                     <asp:ImageButton ID="ImageButton1" runat="server" 
                                         ImageUrl="images/design_images/kfilereplace.png" Style="height: 22px" 
                                         ValidationGroup="a" />
                                     <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                                         BackgroundCssClass="modalBackground" PopupControlID="Panel1" 
                                         TargetControlID="ImageButton1">
                                     </cc1:ModalPopupExtender>
                                     <asp:RequiredFieldValidator ID="vald_Systemname8" runat="server" 
                                         ControlToValidate="codepersonel" ErrorMessage="*" ForeColor="Red" 
                                         ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                 </asp:Panel>
              
            </div>
            <div class="div_val">
            </div>


            <div class="div_nev">
                نام ونام خانوادگی
            </div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox2" runat="server" Width="190px" ValidationGroup="a" CssClass="txt_ r5"
                    Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                عنوان شغل
            </div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox9" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                تاریخ شروع بکار</div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox5" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                شماره بیمه</div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox20" runat="server" Width="190px" ValidationGroup="a" CssClass="txt_ r5"
                    Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                درجه تحصیلی</div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox11" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                نوع استخدام</div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox1" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                <asp:Label ID="perid" runat="server" Visible="False"></asp:Label>
                </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تعداد گواهی</div>
            <div class="div_txt">
                <asp:TextBox ID="lblCntGavahi" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>
         
       <div id="Div1" style="width: 948px; height: auto; text-align: center; vertical-align: middle;
            direction: rtl;">
            <asp:Panel ID="Panel9" runat="server" GroupingText="سوابق">
            <dx:ASPxGridView ID="gridSabege" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="new_later_num" ClientIDMode="AutoID" Width="100%">
                <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                <Columns>
                    <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="latter_date" VisibleIndex="0"
                        Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="gavahi_typ" ShowInCustomizationForm="True" Width="25%" 
                        Caption="نوع گواهی" VisibleIndex="1">
    <editcellstyle horizontalalign="Justify">
    </editcellstyle>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Justify"></CellStyle>
</dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="bank_nam" ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="شعبه" FieldName="shobe_nam"
                        ShowInCustomizationForm="True" VisibleIndex="3" Width="15%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="سازمان/موسسه" FieldName="moassese"
                        VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="برای" FieldName="motafarege_nam" ShowInCustomizationForm="True" VisibleIndex="5" Width="20%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statt" VisibleIndex="6" ShowInCustomizationForm="True" Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="stat" FieldName="stat" Visible="False" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn VisibleIndex="8" Width="50px" FieldName="new_later_num" Caption="ابطال؟">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnEbtal" runat="server" 
                    CommandArgument='<%# Eval("new_later_num")%>' CommandName='<%# "Ebtall" %>' 
                    style="color: #333333; " Text="ابطال؟" 
                    ></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="9" Width="50px" FieldName="new_later_num" Caption="ثبت برگشت از بانک؟">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" 
                    CommandArgument='<%# Eval("new_later_num")%>' CommandName='<%# "bargasht"%>' 
                    style="color: #333333; " Text="برگشت ازبانک؟" 
                    ></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn VisibleIndex="10" Width="50px" FieldName="new_later_num" Caption="برگشت به صادره؟">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkButton2" runat="server" 
                    CommandArgument='<%# Eval("new_later_num")%>' CommandName='<%# "sodur"%>' 
                    style="color: #333333; " Text="برگشت به صادره؟" 
                    ></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager PageSize="20">
                </SettingsPager>
            </dx:ASPxGridView>
            </asp:Panel>
           </div>
        <div id="grdGate" style="width: 948px; height: auto; text-align: center; vertical-align: middle;
            direction: ltr;">
                        <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
        </div>
        <br />
        <table align="center" cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td style="vertical-align: top">
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel4" runat="server" BorderColor="#00CC99" GroupingText="سابقه کار" Visible="False">
                                    <table class="auto-style1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="dhg" runat="server" Text="سال"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="salkar" runat="server" Text="salkar"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="ماه"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="mahkar" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="روز"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ruzkar" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="Panel7" runat="server" BorderColor="#FFFFCC" GroupingText="سابقه پرداخت بیمه" Visible="False">
                                    <table class="auto-style1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" Text="سال"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="salsabege" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label22" runat="server" Text="ماه"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="mahsabege" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" Text="روز"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ruzsabege" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="Panel8" runat="server" BorderColor="#00CC99" GroupingText=" سختی زیان آورغیرشرکتی" Visible="False">
                                    <table class="auto-style1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text="سال"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="salsaxtsher" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label28" runat="server" Text="ماه"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="mahsaxtsher" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label30" runat="server" Text="روز"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ruzsaxtsher" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="Panel6" runat="server" BorderColor="#00CC99" GroupingText="سختی زیان آور شرکتی" Visible="False">
                                    <table class="auto-style1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Text="سال"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="salsaxtno" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Text="ماه"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="mahsaxno" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" Text="روز"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ruzsaxtno" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="Panel5" runat="server" BorderColor="#00CC99" GroupingText="جمع سابقه سختی زیان آور" Visible="False">
                                    <table class="auto-style1">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="سال"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="salzian" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="ماه"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="mahzian" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="روز"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ruzzian1" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <hr />
    </div>
    <%-- Expand from 0px to the appropriate size while fading in --%>
</asp:Content>
