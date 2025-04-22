<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false"
    CodeFile="fullinfo.aspx.vb" Inherits="fullinfo" %>

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
        .auto-style2 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Expand from 0px to the appropriate size while fading in --%>
    <div class="div_page" dir="rtl">
        <table class="auto-style1">
            <tr>
                <td>

                    <table class="auto-style1">
                        <tr>
                            <td>

                <asp:Label ID="perid" runat="server" Visible="False"></asp:Label>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                    <asp:ImageButton ID="ImageButton2" runat="server" Height="28px" ImageUrl="~/Icons/deleted.png" ToolTip="برگشت به صفحه اصلی" Width="28px" />

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="mihmanBtn" runat="server" Text="ملاقات کننده" Height="25px" Enabled="False" Font-Bold="False" />
                                <asp:Button ID="laptopBtn" runat="server" Text="مجوز لپ تاپ" Height="25px" Width="100px" Enabled="False" Font-Bold="False" />

                                <asp:Button ID="xodroBtn" runat="server" Text="مجوز خودرو" Height="25px" Width="100px" Enabled="False" Font-Bold="False" />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">

                            </td>
                            <td class="auto-style2">

                            </td>
                            <td class="auto-style2">

                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td>
                    &nbsp;</td>
                <td>

                    <asp:Image ID="Image1" runat="server" CssClass="r5" Height="110px" Width="90px" />
                            </td>
            </tr>
            </table>
        <div class="div_line">
            <div class="div_nev">
                کد پرسنل</div>


            <div class="div_txt">

                                 <asp:Panel ID="Panel3" runat="server" Width="190px" DefaultButton="ImageButton1">
                                     <asp:TextBox ID="codepersonel" 
    runat="server" Width="150px" ValidationGroup="a"
                    CssClass="txt_ r5" MaxLength="6" Enabled="False"></asp:TextBox>
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
                                         ValidationGroup="a" Visible="False" />
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
                رشته تحصیلی</div>
            <div class="div_txt">
                <asp:TextBox ID="TextBox1" runat="server" Width="190px" ValidationGroup="a"
                    CssClass="txt_ r5" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                کدملی</div>
            <div class="div_txt">
                        <asp:TextBox ID="codemelli" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                نام پدر</div>
            <div class="div_txt">
                        <asp:TextBox ID="father" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
        </div>

        <div class="div_line">
            <div class="div_nev">
                شماره شناسنامه </div>
                    <div class="div_txt">
                        <asp:TextBox ID="shsh" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        محل صدور</div>
                    <div class="div_txt">
                        <asp:TextBox ID="mahalesodur" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        شماره سریال ش</div>
                    <div class="div_txt">
                        <asp:TextBox ID="shserial" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                وضعیت شناسنامه</div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="vaziatsh" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>اصل</asp:ListItem>
                                        <asp:ListItem>المثنی</asp:ListItem>
                                    </asp:DropDownList>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        محل تولد</div>
                    <div class="div_txt">
                        <asp:TextBox ID="mahaletavalod" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                <div class="div_val">
                    </div>
                    <div class="div_nev">
                        تاریخ تولد</div>
                    <div class="div_txt">
                        <asp:TextBox ID="tarixtavalod" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                نوع استخدام</div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="noeestexdam" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                    </asp:DropDownList>
                    </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode2" runat="server" 
                    ControlToValidate="noeestexdam" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_nev">
                        امور</div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="CmbUnit" runat="server" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                    </asp:DropDownList>
                    </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode5" runat="server" 
                    ControlToValidate="CmbUnit" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_nev">
                        واحد</div>
                    <div class="div_txt">
                       
                                    <asp:DropDownList ID="CmbVahed" runat="server" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                    </asp:DropDownList>
                       
                    </div>
                    <div class="div_val">
                    </div>
        </div>
         <div class="div_line">
            <div class="div_nev">
                سمت</div>
               <div class="div_txt">
                    <asp:TextBox ID="semat" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
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
                تاریخ استخدام</div>
                    <div class="div_txt">
                        <asp:TextBox ID="tarixstexdam" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        مدرک تحصیلی</div>
                    <div class="div_txt">
                        <asp:TextBox ID="madrak" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        وضعیت تاهل
                    </div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="vaziatathol" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="متاهل">متاهل</asp:ListItem>
                                        <asp:ListItem Value="مجرد">مجرد</asp:ListItem>
                                        <asp:ListItem Value="مطعلقه">مطعلقه</asp:ListItem>
                                    </asp:DropDownList>
                    </div>
                    <div class="div_val">
                    </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                تلفن داخلی</div>
                    <div class="div_txt">
                        <asp:TextBox ID="teldaxele" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        تلفن منزل</div>
                    <div class="div_txt">
                        <asp:TextBox ID="telmanzel" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        موبایل</div>
                    <div class="div_txt">
                        <asp:TextBox ID="mobile" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                دین</div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="din" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>اسلام</asp:ListItem>
                                        <asp:ListItem>مسیحی</asp:ListItem>
                                    </asp:DropDownList>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        مذهب</div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="mazhab" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>شیعه</asp:ListItem>
                                        <asp:ListItem>سنی</asp:ListItem>
                                    </asp:DropDownList>
                    </div>
                <div class="div_val">
                    </div>
                    <div class="div_nev">
                        شماره گذرنامه</div>
                    <div class="div_txt">
                        <asp:TextBox ID="shgozarname" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
        </div>
        <div class="div_line">
                <div class="div_nev">
                         آدرس</div>
            <div class="div_txtnote">

                        <asp:TextBox ID="adres" runat="server" Width="700px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>

            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                &nbsp;</div>
                    <div class="div_txt">

                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        <asp:Label ID="lblellat" runat="server" Text="علت غیرفعالی" Visible="False" ForeColor="Red"></asp:Label>
                        </div>
                    <div class="div_txt">
                                    <asp:DropDownList ID="ellat" runat="server" AutoPostBack="True" 
                                        CssClass="txt_ r5" Font-Bold="True" Height="25px" TabIndex="10" 
                                        Width="190px" Enabled="False" Visible="False">
                                    </asp:DropDownList>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        <asp:Label ID="lblellattarix" runat="server" Text="تاریخ غیرفعالی" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="div_txt">

                        <asp:TextBox ID="tarixDisabled" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="txt_ r5" Visible="False"></asp:TextBox>

                    </div>
                    <div class="div_val">
                    </div>
        </div>
          <div id="Div2" style="width: 948px; height: auto; text-align: center; vertical-align: middle;
            direction: ltr;">
            <asp:Panel ID="Panel1" runat="server" GroupingText="سوابق آموزشی">
                <dx:ASPxGridView ID="trnGrid" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="idd" ClientIDMode="AutoID" Width="930px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="عنوان" FieldName="onvan" VisibleIndex="0" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="dorehayeGozrande" 
                        Caption="دوره های گذرانده" VisibleIndex="1" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra"
                        VisibleIndex="3" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ"
                        VisibleIndex="4" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="saat" Caption="ساعت"
                        VisibleIndex="5" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="هماهنگی اجرا" FieldName="hamahangiEjra" VisibleIndex="6" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="idd" FieldName="idd" Visible="False" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" VisibleIndex="8" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="tarixPayan" VisibleIndex="9" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                </dx:ASPxGridView>
            </asp:Panel>
        </div>
        <br />
        <div id="grdGate" style="width: 948px; height: auto; text-align: center; vertical-align: middle;
            direction: ltr;">
            <asp:Panel ID="Panel9" runat="server" GroupingText="سوابق پرسنل">
                <dx:ASPxGridView ID="DataGridView1" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="id" ClientIDMode="AutoID" Width="930px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="نوع" FieldName="sherkati" VisibleIndex="0"
                        Width="12%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="value" ShowInCustomizationForm="True" Width="15%" 
                        Caption="سختی زیان آور" VisibleIndex="1">
                            <editcellstyle horizontalalign="Justify">
                            </editcellstyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Justify">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="عنوان شغل" FieldName="postname"
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="5%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ خاتمه" FieldName="tarixpayan"
                        VisibleIndex="3" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixshoru"
                        VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="sazmanName" Width="5%" Caption="نام سازمان"
                        VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                </dx:ASPxGridView>
            </asp:Panel>
        </div>
        <br />
        <div id="Div1" style="width: 948px; height: auto; text-align: center; vertical-align: middle;
            direction: ltr;">
            <asp:Panel ID="Panel111" runat="server" GroupingText="افراد تحت تکفل">
                <dx:ASPxGridView ID="takafolGRD" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="id" ClientIDMode="AutoID" Width="930px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="نسبت" FieldName="Expr1" VisibleIndex="2"
                        Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="birthday" Width="10%" 
                        Caption="تاریخ تولد" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Name"
                        ShowInCustomizationForm="True" VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="fstName"
                        VisibleIndex="5" Width="5%" ShowInCustomizationForm="True">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                </dx:ASPxGridView>
            </asp:Panel>
        </div>
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
                                            <td class="auto-style2">
                                                <asp:Label ID="Label4" runat="server" Text="ماه"></asp:Label>
                                            </td>
                                            <td class="auto-style2">
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
