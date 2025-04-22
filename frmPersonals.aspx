<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmPersonals.aspx.vb" Inherits="frmPersonals" %>

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

                    <asp:ImageButton ID="ImageButton1" runat="server" Height="48px" ImageUrl="~/Icons/deleted.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
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
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" />
                    <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />

</div>
 <div class="div_line">
            <div class="div_nev">
                &nbsp;شماره پرونده</div>
            <div class="div_txt">
                                        <asp:TextBox ID="txtCode" runat="server" Width="190px" Enabled="False" 
                                            CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode" runat="server" 
                    ControlToValidate="txtCode" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                نام </div>
            <div class="div_txt">
                <asp:TextBox ID="txtPcUserName" runat="server" Width="190px" Enabled="False" 
                    CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode0" runat="server" 
                    ControlToValidate="txtPcUserName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                نام خانوادگي</div>
            <div class="div_txt">
                <asp:TextBox ID="txtPcUserFamili" runat="server" Width="190px" Enabled="False" 
                    CssClass="txt_ r5"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode1" runat="server" 
                    ControlToValidate="txtPcUserFamili" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                کد پرسنلی</div>
                    <div class="div_txt">
                        <asp:TextBox ID="txtCodePerson" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                    </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode3" runat="server" 
                    ControlToValidate="txtCodePerson" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
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
                </div>
               <div class="div_txt">
                    
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
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode6" runat="server" 
                    ControlToValidate="madrak" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
              <div class="div_nev">
                  رشته تحصیلی</div>
               <div class="div_txt">
                        <asp:TextBox ID="reshte" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
                   </div>
              <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode7" runat="server" 
                    ControlToValidate="reshte" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
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
                        </div>
                    <div class="div_txt">
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
                  شماره حساب</div>
               <div class="div_txt">

                        <asp:TextBox ID="shHesab" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>

                   </div>
               <div class="div_val">
                    </div>
                <div class="div_nev">
                  شماره بیمه</div>
               <div class="div_txt">

                        <asp:TextBox ID="shBime" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>

                   </div>
               <div class="div_val">
                    </div>
                <div class="div_nev">
                    کدپستی</div>
               <div class="div_txt">

                        <asp:TextBox ID="codePosti" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>

                   </div>
               <div class="div_val">
                    </div>
              </div>
         <div class="div_line">
                <div class="div_nev">
                  چارت</div>
               <div class="div_txt">

                        <asp:TextBox ID="chart" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>

                   </div>
               <div class="div_val">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="chart" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" ></asp:RequiredFieldValidator>
                    </div>
                <div class="div_nev">
                  کدپ سرپرست</div>
               <div class="div_txt">

                        <asp:TextBox ID="sarparast" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>

                   </div>
               <div class="div_val">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="sarparast" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" ></asp:RequiredFieldValidator>
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
                غیرفعال؟ </div>
                    <div class="div_txt">

                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" AutoPostBack="True" />

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
        
                       <div id="grdd" style=" width:948px;
          text-align:center;
          direction:rtl;
          vertical-align:middle;
                     height:auto;" dir="rtl">
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="Id"  
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="900px">

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="8" Visible="False">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="شماره پرونده" FieldName="Id" 
                            VisibleIndex="0" Width="50px">
                            <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("Id")%>'></asp:LinkButton>
                            </dataitemtemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            VisibleIndex="1" Width="150px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگي" FieldName="family" 
                            VisibleIndex="2" Width="200px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کدپرسنلی" FieldName="codePersoneli" 
                            VisibleIndex="3" Width="100px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" 
                            ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="7" Width="50px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <SettingsPager PageSize="25">
                    </SettingsPager>

<Settings ShowFilterRow="True"></Settings>

                </dx:ASPxGridView>
            </div>


         
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
 <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />

   
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>


   
               
   
  
    </div>
</asp:Content>

