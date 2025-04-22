<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmDomainUsers.aspx.vb" Inherits="frmDomainUsers" %>

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
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeleted" runat="server" Height="48px" 
                        ImageUrl="~/Icons/deleted.png" ToolTip="لیست کدهای حذف شده" Width="48px" 
                        ValidationGroup="b" />
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
                كد كاربر</div>
            <div class="div_txt">
                                        <asp:TextBox ID="txtPcUser" runat="server" Width="190px" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_BaseWorkcode" runat="server" 
                    ControlToValidate="txtPcUser" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                نام كاربر</div>
            <div class="div_txt">
                <asp:TextBox ID="txtPcUserName" runat="server" Width="190px" Enabled="False"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                نام خانوادگي</div>
            <div class="div_txt">
                <asp:TextBox ID="txtPcUserFamili" runat="server" Width="190px" Enabled="False"></asp:TextBox>
            </div>
                    <div class="div_val">
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                آي دي</div>
                    <div class="div_txt">
                        <asp:TextBox ID="txtPcUserIdDomain" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ltr r5 h25"></asp:TextBox>
                    </div>
                    <div class="div_val">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="txtPcUserIdDomain" ErrorMessage="*" ForeColor="Red" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ValidationGroup="a"></asp:RegularExpressionValidator>
                    </div>
                    <div class="div_nev">
                        ايميل</div>
                    <div class="div_txt">
                        <asp:TextBox ID="txtPcUserEmail" runat="server" Width="190px" Enabled="False" 
                            CssClass="txt_ltr r5 h25"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
                    <div class="div_nev">
                        موبايل</div>
                    <div class="div_txt">
                <asp:TextBox ID="txtPcUserMobile" runat="server" Width="190px" Enabled="False"></asp:TextBox>
                    </div>
                    <div class="div_val">
                    </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                اختيارت ويژه</div>
            <div id="div_txt"  style="float:right;
          width:514px;
          height:25px;
          text-align:right;
          vertical-align:middle;
          line-height:25px; ">
                <asp:TextBox ID="txtpcUserSpcial" runat="server" Width="500px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
           
            <div class="div_nev">
                غیر فعال</div>
            <div class="div_txt">
                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" />
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
                    KeyFieldName="UserCode"  
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="940px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="8" Visible="False">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="كدكاربر" FieldName="UserCode" 
                            VisibleIndex="0" Width="5px">
                            <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("UserCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("UserCode") %>'></asp:LinkButton>
                            </dataitemtemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="UserLastName" 
                            VisibleIndex="1" Width="50px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگي" FieldName="UserFamili" 
                            VisibleIndex="2" Width="150px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="آي دي" FieldName="UserName_" 
                            VisibleIndex="3" Width="100px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ايميل" FieldName="UserEmail" 
                            ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موبايل" FieldName="UserMobile" 
                            ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="اختيارات ويژه" FieldName="UserSpcial" 
                            ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="7" Width="5px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                    <SettingsPager PageSize="25">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" />

<Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>
            </div>


         
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />


   
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
   
  
    </div>
</asp:Content>

