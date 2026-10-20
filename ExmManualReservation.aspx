<%@ Page Title="ثبت دستی رزرو معاینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExmManualReservation.aspx.vb" Inherits="ExmManualReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .mr-container { max-width: 1400px; margin: 25px auto; padding: 25px; background: #fff; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; font-family: 'Segoe UI', Tahoma, sans-serif; }
        .mr-title { font-size: 22px; font-weight: 700; color: #2c3e50; text-align: center; margin-bottom: 20px; padding-bottom: 15px; border-bottom: 2px solid #e9ecef; }
        
        /* ============================================ */
        /* کارت‌های انتخاب */
        /* ============================================ */
        .mr-card { background: #f8f9fa; padding: 20px; border-radius: 10px; margin-bottom: 20px; border-right: 4px solid #3498db; }
        .mr-card-title { font-size: 15px; font-weight: 700; color: #2c3e50; margin-bottom: 12px; padding-bottom: 8px; border-bottom: 1px solid #ddd; }
        
        .mr-row { display: flex; gap: 12px; flex-wrap: wrap; align-items: flex-end; }
        .mr-group { flex: 1; min-width: 180px; }
        .mr-group label { display: block; font-weight: 600; color: #2c3e50; margin-bottom: 6px; font-size: 13px; }
        .mr-group input, .mr-group select, .mr-group textarea { width: 100%; padding: 9px 12px; border: 1.5px solid #e0e0e0; border-radius: 8px; font-size: 13px; box-sizing: border-box; font-family: inherit; }
        .mr-group input:focus, .mr-group select:focus { border-color: #3498db; outline: none; box-shadow: 0 0 0 3px rgba(52,152,219,0.15); }
        
        .mr-btn { padding: 9px 22px; border: none; border-radius: 8px; font-size: 13px; font-weight: 600; cursor: pointer; font-family: inherit; transition: all 0.3s; white-space: nowrap; }
        .mr-btn:hover { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .mr-btn-search { background: #3498db; color: #fff; }
        .mr-btn-save { background: #27ae60; color: #fff; }
        .mr-btn-cancel { background: #95a5a6; color: #fff; }
        .mr-btn-danger { background: #e74c3c; color: #fff; }
        .mr-btn-back { background: #9b59b6; color: #fff; }
        .mr-btn:disabled { background: #ccc; cursor: not-allowed; transform: none; box-shadow: none; }
        
        /* ============================================ */
        /* هشدارها */
        /* ============================================ */
        .mr-alert { padding: 12px 18px; border-radius: 8px; margin: 12px 0; font-size: 14px; display: flex; align-items: center; gap: 10px; }
        .mr-alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .mr-alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .mr-alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .mr-alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        
        /* ============================================ */
        /* جدول‌ها */
        /* ============================================ */
        .mr-table-responsive { overflow-x: auto; margin-top: 15px; }
        .mr-table { width: 100%; border-collapse: collapse; font-size: 13px; text-align: right; }
        .mr-table th { background: #2c3e50; color: #fff; padding: 10px 8px; white-space: nowrap; text-align: center; }
        .mr-table td { padding: 8px; border-bottom: 1px solid #e9ecef; vertical-align: middle; text-align: center; }
        .mr-table tr:hover td { background: #f1f8ff; }
        .mr-table tr.selected td { background: #d4edda; }
        
        /* ============================================ */
        /* ظرفیت نمایش */
        /* ============================================ */
        .capacity-box { display: inline-block; padding: 4px 10px; border-radius: 12px; font-size: 12px; font-weight: 700; }
        .cap-ok { background: #d4edda; color: #155724; }
        .cap-warn { background: #fff3cd; color: #856404; }
        .cap-danger { background: #f8d7da; color: #721c24; }
        .cap-full { background: #e74c3c; color: #fff; }
        
        /* ============================================ */
        /* کارت خلاصه انتخاب */
        /* ============================================ */
        .mr-summary { background: linear-gradient(135deg, #27ae60, #2ecc71); color: #fff; padding: 15px 20px; border-radius: 10px; margin: 15px 0; font-weight: 600; box-shadow: 0 2px 8px rgba(39,174,96,0.3); }
        .mr-summary .num { font-size: 22px; font-weight: 800; margin: 0 4px; }
        
        /* ============================================ */
        /* تاریخ نمایش */
        /* ============================================ */
        .date-badge { display: inline-block; padding: 3px 10px; border-radius: 12px; background: #e8f4fd; color: #1f5f8b; font-weight: 600; font-size: 12px; margin: 2px; }
        .date-badge.sel { background: #3498db; color: #fff; }
        
        @media (max-width: 768px) {
            .mr-container { padding: 12px; margin: 8px; }
            .mr-row { flex-direction: column; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mr-container">
        <div class="mr-title">📝 ثبت دستی رزرو معاینه (ویژه کارشناس بهداشت)</div>
        
        <!-- پیام‌ها -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="mr-alert">
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- مرحله ۱: انتخاب واحد و تاریخ -->
        <!-- ============================================ -->
        <div class="mr-card">
            <div class="mr-card-title">📌 مرحله ۱: انتخاب واحد و تاریخ معاینه</div>
            <div class="mr-row">
                <div class="mr-group">
                    <label>🏢 واحد:</label>
                    <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="mr-group">
                    <label>📅 تاریخ معاینه:</label>
                    <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged">
                        <asp:ListItem Text="-- ابتدا واحد را انتخاب کنید --" Value="0" />
                    </asp:DropDownList>
                </div>
                <div class="mr-group" style="min-width: 250px;">
                    <label>ظرفیت این تاریخ:</label>
                    <div id="divCapacityInfo" style="padding-top: 10px;">
                        <asp:Label ID="lblCapacityInfo" runat="server" ForeColor="#6c757d" />
                    </div>
                </div>
            </div>
        </div>
        
        <!-- ============================================ -->
        <!-- مرحله ۲: جستجوی پرسنل -->
        <!-- ============================================ -->
        <div class="mr-card" style="border-right-color:#f39c12;">
            <div class="mr-card-title">🔍 مرحله ۲: جستجوی پرسنل واحد</div>
            <div class="mr-row">
                <div class="mr-group">
                    <label>کد پرسنلی / نام خانوادگی / کد ملی:</label>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="مثال: 12345 یا احمدی" />
                </div>
                <asp:Button ID="btnSearchPersonnel" runat="server" Text="🔍 جستجو" CssClass="mr-btn mr-btn-search" OnClick="btnSearchPersonnel_Click" />
                <asp:Button ID="btnShowAll" runat="server" Text="👥 نمایش همه پرسنل واحد" CssClass="mr-btn mr-btn-search" style="background:#16a085;" OnClick="btnShowAll_Click" />
            </div>
            <div style="font-size: 12px; color: #856404; margin-top: 10px;">
                ⚠️ فقط پرسنلی نمایش داده می‌شوند که <b>در این تاریخ (برای این واحد) رزرو ندارند</b>.
            </div>
        </div>
        
        <!-- ============================================ -->
        <!-- جدول پرسنل -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlPersonnelList" runat="server" Visible="false">
            <div class="mr-summary">
                👥 <span class="num"><asp:Label ID="lblPersonnelCount" runat="server" Text="0" /></span> پرسنل بدون رزرو در این واحد یافت شد
            </div>
            <div class="mr-table-responsive">
                <asp:GridView ID="gvPersonnel" runat="server" AutoGenerateColumns="False" 
                    CssClass="mr-table" EmptyDataText="هیچ پرسنلی برای نمایش وجود ندارد"
                    AllowPaging="True" PageSize="25"
                    OnPageIndexChanging="gvPersonnel_PageIndexChanging"
                    OnRowCommand="gvPersonnel_RowCommand"
                    DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="40" />
                        <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" ItemStyle-Width="100" />
                        <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" ItemStyle-Width="180" />
                        <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" ItemStyle-Width="110" />
                        <asp:BoundField DataField="Position" HeaderText="سمت" ItemStyle-Width="130" />
                        <asp:BoundField DataField="Mobile" HeaderText="موبایل" ItemStyle-Width="110" />
                        <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="130">
                            <ItemTemplate>
                                <asp:Button ID="btnSelect" runat="server" 
                                    CommandName="SelectPerson" 
                                    CommandArgument='<%# Eval("ID") %>' 
                                    Text="➕ افزودن" 
                                    CssClass="mr-btn mr-btn-save" 
                                    style="padding:5px 12px;font-size:11px;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- مرحله ۳: سبد انتخاب‌شده‌ها + ثبت نهایی -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlSelected" runat="server" Visible="false" CssClass="mr-card" style="border-right-color:#27ae60;">
            <div class="mr-card-title">✅ مرحله ۳: تأیید نهایی و ثبت</div>
            
            <div style="font-size: 13px; color: #2c3e50; margin-bottom: 12px;">
                📅 تاریخ انتخاب‌شده: <asp:Label ID="lblSelectedDate" runat="server" ForeColor="#e74c3c" Font-Bold="true" />
                &nbsp;|&nbsp; 🏢 واحد: <asp:Label ID="lblSelectedUnit" runat="server" ForeColor="#2980b9" Font-Bold="true" />
            </div>
            
            <div class="mr-table-responsive">
                <asp:GridView ID="gvSelected" runat="server" AutoGenerateColumns="False" 
                    CssClass="mr-table" EmptyDataText="هیچ پرسنلی هنوز انتخاب نشده"
                    OnRowCommand="gvSelected_RowCommand"
                    DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="40" />
                        <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" ItemStyle-Width="100" />
                        <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" ItemStyle-Width="180" />
                        <asp:BoundField DataField="Position" HeaderText="سمت" ItemStyle-Width="130" />
                        <asp:TemplateField HeaderText="حذف" ItemStyle-Width="80">
                            <ItemTemplate>
                                <asp:Button ID="btnRemove" runat="server" 
                                    CommandName="RemovePerson" 
                                    CommandArgument='<%# Eval("ID") %>' 
                                    Text="❌" 
                                    CssClass="mr-btn mr-btn-danger" 
                                    style="padding:5px 10px;font-size:11px;"
                                    OnClientClick="return confirm('حذف شود؟');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            
            <div class="mr-row" style="margin-top: 15px;">
                <div class="mr-group" style="min-width: 300px;">
                    <label>📝 توضیحات (اختیاری):</label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="2" placeholder="دلیل ثبت دستی (مثلاً: بیمار بود، مأموریت داشت و ...)" />
                </div>
                <asp:Button ID="btnSubmitAll" runat="server" Text="✅ ثبت نهایی" CssClass="mr-btn mr-btn-save" 
                    OnClick="btnSubmitAll_Click" OnClientClick="return confirm('ثبت نهایی انجام شود؟');" 
                    style="padding: 11px 30px; font-size: 14px;" />
                <asp:Button ID="btnClearSelected" runat="server" Text="🗑️ پاک کردن سبد" CssClass="mr-btn mr-btn-cancel" 
                    OnClick="btnClearSelected_Click" OnClientClick="return confirm('همه انتخاب‌ها پاک شود؟');" />
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- دکمه بازگشت -->
        <!-- ============================================ -->
        <div style="margin-top: 25px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="mr-btn mr-btn-back" 
                OnClick="btnBack_Click" style="padding:10px 30px;font-size:14px;" />
        </div>
    </div>

    <!-- ============================================ -->
    <!-- ✅ محو شدن خودکار پیام موفقیت بعد از ۵ ثانیه -->
    <!-- ============================================ -->
    <script type="text/javascript">
        window.addEventListener('load', function () {
            var msgPanel = document.getElementById('<%= pnlMessage.ClientID %>');
            if (msgPanel && msgPanel.style.display !== 'none') {
                var msgDiv = msgPanel.querySelector('.mr-alert-success');
                if (msgDiv) {
                    setTimeout(function () {
                        msgPanel.style.transition = 'opacity 1s, max-height 0.5s';
                        msgPanel.style.opacity = '0';
                        setTimeout(function () {
                            msgPanel.style.display = 'none';
                        }, 1000);
                    }, 6000);
                }
            }
        });
    </script>
</asp:Content>