<%@ Page Language="VB" AutoEventWireup="false" 
    CodeFile="ExmChangeReservationDate.aspx.vb" 
    Inherits="ExmChangeReservationDate" 
    MasterPageFile="~/dabir.master" 
    Title="تغییر تاریخ معاینه پرسنل"
    MaintainScrollPositionOnPostBack="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mr-container { max-width: 1100px; margin: 0 auto; background: #fff; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,.08); padding: 20px; }
        .mr-header { border-bottom: 2px solid #007bff; padding-bottom: 10px; margin-bottom: 20px; }
        .mr-header h2 { margin: 0; color: #007bff; }
        .mr-alert { padding: 12px 16px; border-radius: 6px; margin-bottom: 16px; font-weight: bold; }
        .mr-alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .mr-alert-danger  { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .mr-alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffeeba; }
        .mr-alert-info    { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .mr-row { display: flex; gap: 12px; flex-wrap: wrap; align-items: flex-end; margin-bottom: 16px; }
        .mr-field { flex: 1; min-width: 180px; }
        .mr-field label { display: block; font-weight: bold; margin-bottom: 4px; color: #333; }
        .mr-field input[type=text], .mr-field select, .mr-field textarea {
            width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box;
        }
        .mr-btn { padding: 9px 18px; border: none; border-radius: 4px; cursor: pointer; font-weight: bold; }
        .mr-btn-primary { background: #007bff; color: #fff; }
        .mr-btn-success { background: #28a745; color: #fff; }
        .mr-btn-secondary { background: #6c757d; color: #fff; }
        .mr-btn-warning { background: #ffc107; color: #212529; }
        .mr-btn:disabled { opacity: .6; cursor: not-allowed; }
        .mr-grid { width: 100%; border-collapse: collapse; margin-top: 12px; }
        .mr-grid th { background: #007bff; color: #fff; padding: 8px; text-align: right; font-size: 13px; }
        .mr-grid td { padding: 8px; border-bottom: 1px solid #eee; font-size: 13px; }
        .mr-grid tr:hover { background: #f1f7ff; }
        .mr-section { border: 1px solid #ddd; border-radius: 6px; padding: 16px; margin-top: 16px; background: #fafbfc; }
        .mr-section h3 { margin-top: 0; color: #007bff; }
        .capacity-box { display: inline-block; padding: 6px 12px; border-radius: 4px; font-weight: bold; }
        .cap-ok    { background: #d4edda; color: #155724; }
        .cap-warn  { background: #fff3cd; color: #856404; }
        .cap-full  { background: #f8d7da; color: #721c24; }
        .cap-danger{ background: #f8d7da; color: #721c24; }
        .pager { margin-top: 10px; }
        .pager a, .pager span { display: inline-block; padding: 4px 10px; margin: 0 2px; border: 1px solid #ddd; border-radius: 4px; text-decoration: none; color: #333; }
        .pager span { background: #007bff; color: #fff; border-color: #007bff; }
        .mr-btn-danger { background: #dc3545; color: #fff; }
.mr-btn-danger:hover { background: #c82333; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mr-container">
        <div class="mr-header">
            <h2>🔄 تغییر تاریخ معاینه پرسنل</h2>
            <p style="margin:6px 0 0;color:#666;font-size:13px;">جستجو و انتخاب رزرو فعال، سپس تعیین تاریخ جدید</p>
        </div>

        <!-- پیام -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="mr-alert mr-alert-info">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <!-- جستجو -->
        <div class="mr-row">
            <div class="mr-field" style="flex:3;">
                <label>🔍 جستجو (نام / کد ملی / کد پرسنلی)</label>
                <asp:TextBox ID="txtSearch" runat="server" placeholder="عبارت جستجو..."></asp:TextBox>
            </div>
            <div class="mr-field" style="flex:0 0 auto;">
                <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="mr-btn mr-btn-primary" OnClick="btnSearch_Click" />
            </div>
            <div class="mr-field" style="flex:0 0 auto;">
                <asp:Button ID="btnShowAll" runat="server" Text="نمایش همه" CssClass="mr-btn mr-btn-secondary" OnClick="btnShowAll_Click" />
            </div>
        </div>

        <!-- لیست رزروهای فعال -->
        <div class="mr-section">
            <h3>📋 رزروهای فعال (<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label> مورد)</h3>

            <asp:GridView ID="gvReservations" runat="server"
                AutoGenerateColumns="false"
                CssClass="mr-grid"
                AllowPaging="true"
                PageSize="15"
                OnPageIndexChanging="gvReservations_PageIndexChanging"
                OnRowCommand="gvReservations_RowCommand"
                OnRowDataBound="gvReservations_RowDataBound"
                EmptyDataText="هیچ رزرو فعالی یافت نشد."
                GridLines="None">
                <PagerStyle CssClass="pager" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="#" />
                    <asp:BoundField DataField="FullName" HeaderText="نام پرسنل" />
                    <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" />
                    <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
                    <asp:BoundField DataField="UnitName" HeaderText="واحد" />
                    <asp:BoundField DataField="CurrentDateFa" HeaderText="تاریخ فعلی" />
                    <asp:BoundField DataField="StatusDescription" HeaderText="وضعیت" />
                    <asp:TemplateField HeaderText="عملیات">
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server"
                                Text="تغییر تاریخ"
                                CssClass="mr-btn mr-btn-warning"
                                CommandName="SelectReservation"
                                CommandArgument='<%# Eval("ReservationID") %>' />
                        </ItemTemplate>
            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="حذف">
    <ItemTemplate>
        <asp:Button ID="btnDelete" runat="server"
            Text="🗑 حذف"
            CssClass="mr-btn mr-btn-danger"
            CommandName="DeleteReservation"
            CommandArgument='<%# Eval("ReservationID") %>'
            OnClientClick="return confirm('آیا از حذف این رزرو مطمئن هستید؟ این عمل قابل بازگشت نیست.');" />
    </ItemTemplate>
</asp:TemplateField>
                  
                </Columns>
            </asp:GridView>
        </div>

        <!-- پنل تغییر تاریخ -->
        <asp:Panel ID="pnlChange" runat="server" Visible="false" CssClass="mr-section" style="border-color:#ffc107;background:#fffef5;">
            <h3>✏️ تغییر تاریخ برای: <asp:Label ID="lblSelectedPerson" runat="server" ForeColor="#e65100"></asp:Label></h3>

            <div class="mr-row">
                <div class="mr-field">
                    <label>تاریخ فعلی</label>
                    <asp:Label ID="lblCurrentDate" runat="server" Font-Bold="true" ForeColor="#c00"></asp:Label>
                </div>
                <div class="mr-field">
                    <label>تاریخ جدید *</label>
                    <asp:DropDownList ID="ddlNewDate" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlNewDate_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="mr-row">
                <div class="mr-field" style="flex:3;">
                    <label>توضیح (اختیاری)</label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="2" placeholder="دلیل تغییر تاریخ..."></asp:TextBox>
                </div>
            </div>

            <div style="margin-bottom:16px;">
                <asp:Label ID="lblCapacityInfo" runat="server"></asp:Label>
            </div>

            <div class="mr-row">
                <asp:Button ID="btnSubmitChange" runat="server" Text="✅ ثبت تغییر تاریخ"
                    CssClass="mr-btn mr-btn-success" OnClick="btnSubmitChange_Click" />
                <asp:Button ID="btnCancelChange" runat="server" Text="انصراف"
                    CssClass="mr-btn mr-btn-secondary" OnClick="btnCancelChange_Click" CausesValidation="false" />
            </div>
        </asp:Panel>

        <!-- بازگشت -->
        <div style="margin-top:24px;text-align:left;">
            <asp:Button ID="btnBack" runat="server" Text="↩ بازگشت" CssClass="mr-btn mr-btn-secondary" OnClick="btnBack_Click" CausesValidation="false" />
        </div>
    </div>
</asp:Content>