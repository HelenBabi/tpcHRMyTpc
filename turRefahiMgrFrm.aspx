<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="turRefahiMgrFrm.aspx.vb" Inherits="turRefahiMgrFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="container mt-4 p-4 border rounded shadow-sm bg-light" dir="rtl" style="max-width: 700px;">
    <div class="d-flex justify-content-center mb-3 flex-wrap gap-2">
 <asp:ImageButton ID="ibtnAdd" runat="server" ImageUrl="~/Icons/Add.png" ToolTip="جدید" CssClass="img-thumbnail" Width="48px" Height="48px" />
 <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" CssClass="img-thumbnail" Width="48px" Height="48px" />
<asp:ImageButton ID="ibtnSave" runat="server" ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" CssClass="img-thumbnail" Width="48px" Height="48px" Enabled="False" />
 <asp:ImageButton ID="ibtnSaveNew" runat="server" ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" CssClass="img-thumbnail" Width="48px" Height="48px" Enabled="False" />
 <asp:ImageButton ID="ibtnCancel" runat="server" ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" CssClass="img-thumbnail" Width="48px" Height="48px" Enabled="False" />
  <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/Icons/Delete.png" ToolTip="حذف" CssClass="img-thumbnail" Width="48px" Height="48px" />
 <asp:ImageButton ID="ibtnDeactived" runat="server" ImageUrl="~/Icons/reload.png" ToolTip="بازخوانی کدهای غیر فعال" CssClass="img-thumbnail" Width="48px" Height="48px" />
        <asp:ImageButton ID="ibtnExit" runat="server" ImageUrl="~/Icons/Exit.png" ToolTip="خروج" CssClass="img-thumbnail" Width="48px" Height="48px" />
       
    </div>

    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
    <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />

    <div class="mb-3 row">
        <label class="col-sm-2 col-form-label">کد:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txt_idTour" runat="server" CssClass="form-control" Enabled="False" />
        </div>
        <div class="col-sm-2">
           <%-- <asp:RequiredFieldValidator ID="vald_idTour" runat="server" ControlToValidate="txt_idTour" ErrorMessage="*" ForeColor="Red" />--%>
        </div>
    </div>

    <div class="mb-3 row">
        <label class="col-sm-2 col-form-label">زمان:</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txt_magsad" runat="server" CssClass="form-control" Enabled="False" Height="75px" TextMode="MultiLine" Width="100%" />
            <%--<asp:RequiredFieldValidator ID="vald_magsad" runat="server" ControlToValidate="txt_magsad" ErrorMessage="*" ForeColor="Red" />--%>
        </div>
    </div>

    <div class="mb-3 row">
        <label class="col-sm-2 col-form-label">غیر فعال:</label>
        <div class="col-sm-10">
            <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" CssClass="form-check-input" />
        </div>
    </div>

    <div class="mt-4">
        <dx:ASPxGridView ID="ASPxGrvHead" runat="server"
            AutoGenerateColumns="False"
            KeyFieldName="idTour"
            EnableCallBacks="False"
            Width="100%" ClientIDMode="AutoID">
            <ClientSideEvents FocusedRowChanged="function(s, e) { }" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Settings ShowFilterRow="True" />
            <SettingsPager PageSize="25" />
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0">
                    <ClearFilterButton Visible="True" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="کد" FieldName="idTour" VisibleIndex="1">
                    <DataItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"
                            CommandArgument='<%# Eval("idTour") %>'
                            CommandName="select"
                            Text='<%# Eval("idTour") %>'></asp:LinkButton>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="مکان و زمان" FieldName="magsad" VisibleIndex="2" />
                <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" VisibleIndex="3" />
            </Columns>
        </dx:ASPxGridView>
    </div>
</div>


</asp:Content>

