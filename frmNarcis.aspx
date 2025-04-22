<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmNarcis.aspx.vb" Inherits="frmNarcis" %>

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
                    <asp:ImageButton ID="ibtnExit0" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="خروج" Width="48px" 
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
                شماره CD</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_CdCode" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Narciscode" runat="server" 
                    ControlToValidate="txt_CdCode" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                شماره اموال</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_Amval" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="190px" Enabled="False" MaxLength="6">
                    <masksettings mask="000-000-000-000" />
                    <masksettings mask="000000" />

                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Narciscode0" runat="server" 
                    ControlToValidate="txt_Amval" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                نام كاربر</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_HolderName" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ r5"></asp:TextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Narcisname" runat="server" 
                    ControlToValidate="txt_HolderName" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>

                <div class="div_line">
            <div class="div_nev">
                تلفن کاربر</div>
            <div class="div_txt">
                <asp:TextBox ID="txt_Phone" runat="server" Width="190px" 
                    ValidationGroup="a" Enabled="False" CssClass="txt_ltr r5 h25" 
                    MaxLength="4"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                كد فرعي</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_Farei" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="190px" Enabled="False">
                    <masksettings mask="000-000-000-000" />
<MaskSettings Mask="000-000-000-000-000-000-000"></MaskSettings>

                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Narcisname1" runat="server" 
                    ControlToValidate="txt_Farei" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                كد اصلي</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_Genine" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="190px" Enabled="False">
                    <masksettings mask="000-000-000-000" />
<MaskSettings Mask="000-000-000-000"></MaskSettings>

                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
                    <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Narcisname0" runat="server" 
                    ControlToValidate="txt_Genine" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
        </div>
                <div class="div_line">
            <div class="div_nev">
                شماره&nbsp; سریال Cd</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_Serial" runat="server" CssClass="txt_ltr r5 h25" 
                    HorizontalAlign="Left" Width="190px" Enabled="False">
                    <masksettings mask="000-000-000-000" />
<MaskSettings Mask="000-000-000-000-000"></MaskSettings>

                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_Narcisname2" runat="server" 
                    ControlToValidate="txt_Serial" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                آی پی</div>
            <div class="div_txt">
                <dx:ASPxTextBox ID="txt_IP" runat="server" CssClass="txt_ltr r5 h25" 
                    Enabled="False" Width="190px">
                    <masksettings mask="000.00.000.000" />
                    <validationsettings display="None">
                    </validationsettings>
                </dx:ASPxTextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:CheckBox ID="ChbActiveCode" runat="server" Text="غیر فعال" 
                    Visible="False" Enabled="False" />
            </div>
                    <div class="div_val">
            </div>
        </div>
           <div class="grdGate" dir="ltr">     
                <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="CdCode"  
                    EnableCallBacks="False" 
                    Width="948px" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Width="5px" Visible="False">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="1" Width="5px">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn Caption="IP" FieldName="ip" VisibleIndex="2" 
                            Width="100px">
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره سریال " FieldName="Serial" 
                            VisibleIndex="3" Width="150px">
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کد اصلی" FieldName="GenuineCode" 
                            VisibleIndex="4" Width="150px">
                            <EditCellStyle HorizontalAlign="Center">
                            </EditCellStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <editcellstyle horizontalalign="Left">
                            </editcellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="كد فرعي" FieldName="FareiCode" 
                            VisibleIndex="5" Width="200px">
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تلفن" FieldName="HolderPhone" 
                            VisibleIndex="6" Width="30px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام کاربر" FieldName="HolderName" 
                            VisibleIndex="7" Width="100px">
                            <EditCellStyle HorizontalAlign="Right">
                            </EditCellStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره CD" FieldName="CdCode" 
                            ShowInCustomizationForm="True" VisibleIndex="8" Width="10px">
                            <EditCellStyle HorizontalAlign="Center">
                            </EditCellStyle>
                            <dataitemtemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("CdCode") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("CdCode") %>'></asp:LinkButton>
                            </dataitemtemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره اموال " FieldName="SystemCode" 
                            ShowInCustomizationForm="True" VisibleIndex="9" Width="50px">
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <SettingsPager PageSize="25">
                    </SettingsPager>

                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                    <SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد" />

<Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<SettingsText GroupPanel="فيلد مورد نظر را جهت گروهبندي ,در اين محل قرار دهيد"></SettingsText>
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

