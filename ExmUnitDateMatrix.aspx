<%@ Page Title="گزارش تفصیلی تاریخ × واحد" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExmUnitDateMatrix.aspx.vb" Inherits="ExmUnitDateMatrix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /* ============================================ */
        /* کانتینر اصلی */
        /* ============================================ */
        .mx-container { max-width: 1600px; margin: 25px auto; padding: 25px; background: #fff; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; font-family: 'Segoe UI', Tahoma, sans-serif; }
        .mx-title { font-size: 22px; font-weight: 700; color: #2c3e50; text-align: center; margin-bottom: 20px; padding-bottom: 15px; border-bottom: 2px solid #e9ecef; }
        
        /* ============================================ */
        /* فیلترها */
        /* ============================================ */
        .mx-filter { background: #f8f9fa; padding: 18px; border-radius: 10px; margin-bottom: 22px; display: flex; align-items: flex-end; gap: 12px; flex-wrap: wrap; border-right: 4px solid #3498db; }
        .mx-filter-group { flex: 1; min-width: 180px; }
        .mx-filter-group label { display: block; font-weight: 600; color: #2c3e50; margin-bottom: 6px; font-size: 13px; }
        .mx-filter-group input, .mx-filter-group select { width: 100%; padding: 9px 12px; border: 1.5px solid #e0e0e0; border-radius: 8px; font-size: 13px; box-sizing: border-box; font-family: inherit; }
        .mx-filter-group input:focus, .mx-filter-group select:focus { border-color: #3498db; outline: none; box-shadow: 0 0 0 3px rgba(52,152,219,0.15); }
        
        .mx-btn { padding: 9px 22px; border: none; border-radius: 8px; font-size: 13px; font-weight: 600; cursor: pointer; font-family: inherit; transition: all 0.3s; white-space: nowrap; }
        .mx-btn:hover { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .mx-btn-search { background: #3498db; color: #fff; }
        .mx-btn-excel { background: #27ae60; color: #fff; }
        .mx-btn-back { background: #9b59b6; color: #fff; }
        
        /* ============================================ */
        /* پنل کنترل سلامت ظرفیت */
        /* ============================================ */
        .audit-panel { background: #ffffff; padding: 20px; border-radius: 12px; margin-bottom: 25px; box-shadow: 0 2px 12px rgba(0,0,0,0.08); border-top: 4px solid #16a085; }
        .audit-title { color: #2c3e50; border-bottom: 2px solid #16a085; padding-bottom: 10px; margin-bottom: 15px; font-size: 16px; font-weight: 700; }
        .audit-table { width: 100%; border-collapse: collapse; font-size: 13px; }
        .audit-table th { background: #16a085; color: #fff; padding: 9px 8px; text-align: center; font-weight: 600; white-space: nowrap; }
        .audit-table td { padding: 8px; border-bottom: 1px solid #eee; text-align: center; vertical-align: middle; }
        .audit-table tr:hover td { background: #e8f8f5; }
        
        .audit-low     { background: #f8d7da; color: #721c24; padding: 3px 10px; border-radius: 12px; font-size: 12px; font-weight: 700; display: inline-block; }
        .audit-exact   { background: #d4edda; color: #155724; padding: 3px 10px; border-radius: 12px; font-size: 12px; font-weight: 700; display: inline-block; }
        .audit-high    { background: #fff3cd; color: #856404; padding: 3px 10px; border-radius: 12px; font-size: 12px; font-weight: 700; display: inline-block; }
        .audit-ok      { background: #d1ecf1; color: #0c5460; padding: 3px 10px; border-radius: 12px; font-size: 12px; font-weight: 700; display: inline-block; }
        .audit-unknown { background: #e9ecef; color: #6c757d; padding: 3px 10px; border-radius: 12px; font-size: 12px; display: inline-block; }
        
        .audit-legend { display: flex; gap: 18px; flex-wrap: wrap; font-size: 12px; margin-top: 12px; padding: 10px; background: #f8f9fa; border-radius: 8px; }
        
        /* ============================================ */
        /* ماتریس */
        /* ============================================ */
        .mx-table-responsive { overflow-x: auto; margin-top: 15px; }
        .mx-table { width: 100%; border-collapse: collapse; font-size: 12px; direction: rtl; }
        .mx-table th { background: #2c3e50; color: #fff; padding: 9px 6px; border: 1px solid #34495e; text-align: center; font-weight: 600; white-space: nowrap; position: sticky; top: 0; z-index: 5; }
        .mx-table td { padding: 7px 6px; border: 1px solid #ddd; text-align: center; vertical-align: middle; }
        .mx-table tr:hover td { background: #f1f8ff; }
        
        .mx-table .col-unit { background: #34495e; color: #ecf0f1; font-size: 11px; line-height: 1.3; }
        .mx-table .col-unit a { color: #fff; text-decoration: none; }
        .mx-table .col-unit a:hover { text-decoration: underline; }
        .mx-table .col-unit .unit-personnel { display: block; font-size: 10px; color: #bdc3c7; font-weight: normal; margin-top: 2px; }
        
        .mx-table .col-total { background: #e74c3c; color: #fff; }
        .mx-table .row-total { background: #fdebd0; font-weight: 700; color: #a04000; }
        
        .mx-table .date-cell { background: #fff3cd; font-weight: 700; color: #856404; white-space: nowrap; }
        .mx-table .date-cell a { color: #856404; text-decoration: none; display: block; padding: 2px 4px; }
        .mx-table .date-cell a:hover { background: #ffeaa7; border-radius: 4px; text-decoration: underline; }
        
        /* ============================================ */
        /* سلول فشرده ماتریس + Tooltip سفارشی */
        /* ============================================ */
        .mx-table td.mx-compact { padding: 3px 2px; vertical-align: middle; min-width: 48px; position: relative; }
        
        .mx-cell-compact { display: flex; flex-direction: column; align-items: center; justify-content: center; padding: 2px 4px; border-radius: 6px; line-height: 1.1; transition: all 0.15s; cursor: help; position: relative; }
        .mx-cell-compact:hover { background: #f1f8ff; transform: scale(1.05); box-shadow: 0 2px 6px rgba(0,0,0,0.1); z-index: 10; }
        .mx-cell-compact .res-num { font-size: 15px; font-weight: 700; color: #2c3e50; }
        .mx-cell-compact .cap-num { font-size: 9px; color: #95a5a6; font-weight: 500; margin-top: 1px; }
        .mx-cell-compact .mini-bar { width: 34px; height: 3px; background: #e9ecef; border-radius: 2px; margin-top: 3px; overflow: hidden; }
        .mx-cell-compact .mini-bar-fill { height: 100%; border-radius: 2px; transition: width 0.3s; background: #27ae60; }
        .mx-cell-compact .mini-bar-fill.warning { background: #f39c12; }
        .mx-cell-compact .mini-bar-fill.danger  { background: #e74c3c; }
        .mx-cell-compact .mini-bar-fill.over    { background: #c0392b; }
        
        .mx-cell-compact.is-empty .res-num { color: #c0c4c8; font-weight: 400; font-size: 13px; }
        .mx-cell-compact.is-empty .cap-num { color: #d0d4d8; }
        .mx-cell-compact.is-full { background: #fdeaea; border: 1px solid #f5c6cb; }
        .mx-cell-compact.is-full .res-num { color: #c0392b; }
        
        /* ============================================ */
        /* ✅ هشدار رزرو بیش از ظرفیت */
        /* ============================================ */
        .mx-cell-compact.is-over { background: #fff3cd; border: 2px solid #e74c3c; }
        .mx-cell-compact.is-over .res-num { color: #c0392b; font-weight: 800; }
        .mx-cell-compact.is-over .cap-num { color: #c0392b; font-weight: 700; }
        .mx-cell-compact.is-over::after { content: "⚠️"; position: absolute; top: -6px; left: -6px; font-size: 10px; background: #fff; border-radius: 50%; width: 14px; height: 14px; line-height: 14px; text-align: center; box-shadow: 0 1px 3px rgba(0,0,0,0.2); }
        
        /* ============================================ */
        /* ✅ Tooltip سفارشی */
        /* ============================================ */
        .mx-cell-compact .mx-tip { display: none; position: absolute; bottom: calc(100% + 8px); left: 50%; transform: translateX(-50%); background: #2c3e50; color: #fff; padding: 8px 12px; border-radius: 8px; font-size: 11px; font-weight: 500; white-space: nowrap; z-index: 1000; box-shadow: 0 4px 12px rgba(0,0,0,0.25); line-height: 1.6; direction: rtl; text-align: right; pointer-events: none; }
        .mx-cell-compact .mx-tip::after { content: ""; position: absolute; top: 100%; left: 50%; transform: translateX(-50%); border: 6px solid transparent; border-top-color: #2c3e50; }
        .mx-cell-compact .mx-tip .tip-row { display: flex; align-items: center; gap: 6px; padding: 1px 0; }
        .mx-cell-compact .mx-tip .tip-warn { color: #f39c12; font-weight: 700; border-top: 1px solid #34495e; padding-top: 4px; margin-top: 4px; }
        .mx-cell-compact:hover .mx-tip { display: block; }
        
        /* ============================================ */
        /* پنل‌های جزئیات */
        /* ============================================ */
        .mx-detail { margin-top: 30px; background: #ffffff; border-radius: 12px; box-shadow: 0 2px 12px rgba(0,0,0,0.08); border-top: 4px solid #f39c12; padding: 20px; scroll-margin-top: 20px; }
        .mx-detail-title { font-size: 16px; font-weight: 700; color: #2c3e50; padding-bottom: 10px; margin-bottom: 15px; border-bottom: 2px solid #f39c12; }
        .mx-detail-table { width: 100%; border-collapse: collapse; font-size: 13px; }
        .mx-detail-table th { background: #f39c12; color: #fff; padding: 9px 10px; text-align: center; }
        .mx-detail-table td { padding: 8px 10px; border-bottom: 1px solid #eee; text-align: center; }
        .mx-detail-table tr:hover { background: #fef5e7; }
        
        /* ============================================ */
        /* ✅ هدر خلاصه تعداد پرسنل */
        /* ============================================ */
        .mx-detail-summary {
            background: linear-gradient(135deg, #f39c12, #e67e22);
            color: #fff;
            padding: 12px 20px;
            border-radius: 8px;
            margin-bottom: 15px;
            font-size: 14px;
            font-weight: 600;
            box-shadow: 0 2px 8px rgba(243,156,18,0.3);
        }
        .mx-detail-summary .count-number {
            font-size: 22px;
            font-weight: 800;
            margin: 0 4px;
            color: #fff;
        }
        .mx-detail-summary .count-unit {
            display: inline-block;
            background: rgba(255,255,255,0.2);
            padding: 2px 8px;
            border-radius: 10px;
            margin-right: 8px;
            font-size: 11px;
            font-weight: 500;
        }
        
        .mx-pagination { padding: 10px 0; text-align: center; }
        .mx-pagination a, .mx-pagination span { padding: 5px 12px; border: 1px solid #ddd; border-radius: 5px; margin: 0 2px; text-decoration: none; color: #2c3e50; display: inline-block; }
        .mx-pagination a:hover { background: #3498db; color: #fff; border-color: #3498db; }
        .mx-pagination span { background: #3498db; color: #fff; border-color: #3498db; }
        
        /* ============================================ */
        /* نوار درصد */
        /* ============================================ */
        .fill-bar { height: 100%; border-radius: 8px; background: linear-gradient(90deg,#27ae60,#2ecc71); }
        .fill-bar.warning { background: linear-gradient(90deg,#f39c12,#f1c40f); }
        .fill-bar.danger  { background: linear-gradient(90deg,#e74c3c,#c0392b); }
        
        /* ============================================ */
        /* هشدارها */
        /* ============================================ */
        .mx-alert { padding: 12px 18px; border-radius: 8px; margin: 12px 0; font-size: 14px; }
        .mx-alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .mx-alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .mx-alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        
        /* ============================================ */
        /* راهنما */
        /* ============================================ */
        .mx-legend { display: flex; gap: 18px; flex-wrap: wrap; font-size: 12px; margin-bottom: 12px; padding: 10px; background: #f8f9fa; border-radius: 8px; }
        .mx-legend span { display: inline-flex; align-items: center; gap: 6px; }
        .mx-legend .box { width: 16px; height: 16px; border-radius: 4px; display: inline-block; }
        
        /* ============================================ */
        /* ریسپانسیو */
        /* ============================================ */
        @media (max-width: 768px) {
            .mx-container { padding: 12px; margin: 8px; }
            .mx-filter { flex-direction: column; align-items: stretch; }
            .mx-table { font-size: 10px; }
            .audit-table { font-size: 11px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mx-container">
        <div class="mx-title">📊 گزارش تفصیلی تاریخ × واحد (با کنترل سلامت ظرفیت)</div>
        
        <!-- پیام‌ها -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="mx-alert">
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- فیلترها -->
        <!-- ============================================ -->
        <div class="mx-filter">
            <div class="mx-filter-group">
                <label>📅 از تاریخ معاینه:</label>
                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="1405/01/01" MaxLength="10" />
            </div>
            <div class="mx-filter-group">
                <label>📅 تا تاریخ معاینه:</label>
                <asp:TextBox ID="txtDateTo" runat="server" placeholder="1405/12/29" MaxLength="10" />
            </div>
            <div class="mx-filter-group">
                <label>🏢 واحد:</label>
                <asp:DropDownList ID="ddlUnit" runat="server">
                    <asp:ListItem Text="-- همه واحدها --" Value="0" />
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="🔍 نمایش" CssClass="mx-btn mx-btn-search" OnClick="btnSearch_Click" />
            <asp:Button ID="btnExportExcel" runat="server" Text="📊 خروجی اکسل" CssClass="mx-btn mx-btn-excel" OnClick="btnExportExcel_Click" />
        </div>
        
        <!-- ============================================ -->
        <!-- پنل کنترل سلامت ظرفیت vs تعداد پرسنل -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlCapacityAudit" runat="server" Visible="false" CssClass="audit-panel">
            <div class="audit-title">
                🏢 کنترل سلامت ظرفیت واحدها
                <span style="font-size:12px; color:#6c757d; font-weight:normal; margin-right:15px;">
                    (مقایسه ظرفیت کل تعریف‌شده با تعداد پرسنل فعال)
                </span>
            </div>

            <div class="mx-table-responsive">
                <asp:GridView ID="gvCapacityAudit" runat="server" AutoGenerateColumns="False"
                    CssClass="audit-table" EmptyDataText="داده‌ای برای نمایش وجود ندارد"
                    AllowPaging="True" PageSize="30" OnPageIndexChanging="gvCapacityAudit_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="35" />
                        <asp:BoundField DataField="UnitName" HeaderText="واحد" ItemStyle-Width="140" />
                        <asp:BoundField DataField="PersonnelCount" HeaderText="تعداد پرسنل" ItemStyle-Width="90" />
                        <asp:BoundField DataField="TotalCapacity" HeaderText="ظرفیت کل تعریف‌شده" ItemStyle-Width="120" />
                        <asp:BoundField DataField="DateCount" HeaderText="تعداد تاریخ معاینه" ItemStyle-Width="100" />
                        <asp:BoundField DataField="AvgPerDate" HeaderText="میانگین هر تاریخ" ItemStyle-Width="100" />
                        <asp:BoundField DataField="TotalReserved" HeaderText="رزرو فعال" ItemStyle-Width="80" />
                        <asp:TemplateField HeaderText="اختلاف" ItemStyle-Width="140">
                            <ItemTemplate>
                                <span style="font-size:11px; color:#6c757d;"><%# Eval("DiffText") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="110">
                            <ItemTemplate>
                                <span class='<%# Eval("StatusClass") %>'><%# Eval("Status") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="mx-pagination" HorizontalAlign="Center" />
                </asp:GridView>
            </div>

            <div class="audit-legend">
                <span><b>🟢 دقیق:</b> ظرفیت کل = تعداد پرسنل</span>
                <span><b>🔴 کم:</b> ظرفیت کل &lt; تعداد پرسنل (بعضی پرسنل جا نمی‌شوند!)</span>
                <span><b>🟡 زیاد:</b> ظرفیت کل &gt; ۲ برابر پرسنل (هدر رفتن ظرفیت)</span>
                <span><b>🔵 متعادل:</b> جای انعطاف دارد</span>
                <span><b>⚫ بدون تاریخ:</b> هیچ تاریخ معاینه‌ای تعریف نشده</span>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- راهنمای ماتریس -->
        <!-- ============================================ -->
        <div class="mx-legend">
            <span><span class="box" style="background:#f1f8ff;"></span> رزرو دارد ولی جا هست</span>
            <span><span class="box" style="background:#fdeaea;"></span> تکمیل (رزرو = ظرفیت)</span>
            <span><span class="box" style="background:#fff3cd;border:2px solid #e74c3c;"></span> ⚠️ رزرو بیش از ظرفیت</span>
            <span><span class="box" style="background:#fff3cd;"></span> تاریخ (کلیک = جزئیات پرسنل)</span>
            <span><span class="box" style="background:#34495e;"></span> واحد (کلیک = جزئیات واحد)</span>
            <span>💡 روی <b>تاریخ</b> یا <b>واحد</b> کلیک کنید</span>
        </div>
        
        <!-- ============================================ -->
        <!-- ماتریس تاریخ × واحد -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlMatrix" runat="server" Visible="false">
            <div class="mx-table-responsive">
                <table class="mx-table">
                    <thead>
                        <tr>
                            <th style="background:#2c3e50;">#</th>
                            <th style="background:#2c3e50;">تاریخ معاینه</th>
                            <asp:Repeater ID="rptHeader" runat="server">
                                <ItemTemplate>
                                    <th class="col-unit">
                                        <a href='<%# "ExmUnitDateMatrix.aspx?UnitID=" & Eval("ID") & "&DateFrom=" & Server.UrlEncode(Request("DateFrom")) & "&DateTo=" & Server.UrlEncode(Request("DateTo")) & "#pnlUnitDetail" %>'
                                           title="کلیک برای جزئیات واحد">
                                            <%# Eval("UnitName") %>
                                        </a>
                                        <span class="unit-personnel">👥 <%# Eval("PersonnelCount") %> پرسنل</span>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <th class="col-total">جمع سطر (تاریخ)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptBody" runat="server">
                            <ItemTemplate>
                                <%# GetMatrixRowHtml(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <asp:Repeater ID="rptFooter" runat="server">
                            <ItemTemplate>
                                <%# GetMatrixFooterHtml(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tfoot>
                </table>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- ✅ پنل جزئیات پرسنل (کلیک روی تاریخ) -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlDateDetail" runat="server" Visible="false" CssClass="mx-detail" ClientIDMode="Static">
            <div class="mx-detail-title">
                👥 پرسنل رزروکننده در تاریخ <asp:Label ID="lblDetailDate" runat="server" ForeColor="#e74c3c" />
                <span style="font-size:12px; color:#6c757d; font-weight:normal; margin-right:15px;">
                    (تفکیک بر اساس واحد)
                </span>
            </div>
            
            <!-- ✅ هدر خلاصه تعداد -->
            <div id="divDetailSummary" runat="server" class="mx-detail-summary" visible="false">
                <asp:Label ID="lblDetailCount" runat="server" />
            </div>
            
            <div class="mx-table-responsive">
                <asp:GridView ID="gvDateDetail" runat="server" AutoGenerateColumns="False" 
                    CssClass="mx-detail-table" EmptyDataText="هیچ پرسنلی در این تاریخ رزرو نکرده است"
                    AllowPaging="True" PageSize="30" OnPageIndexChanging="gvDateDetail_PageIndexChanging"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UnitName" HeaderText="واحد" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" ItemStyle-Width="90" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" ItemStyle-Width="170" />
                        <asp:BoundField DataField="Position" HeaderText="سمت" ItemStyle-Width="110" />
                        <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Mobile" HeaderText="موبایل" ItemStyle-Width="110" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="RequestDateFa" HeaderText="تاریخ رزرو" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="StatusCodeFa" HeaderText="وضعیت" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle BackColor="#f39c12" ForeColor="White" Font-Bold="True" />
                    <RowStyle BorderColor="#eee" BorderWidth="1px" BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="#fef9f3" />
                    <PagerStyle CssClass="mx-pagination" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- پنل جزئیات واحد (کلیک روی واحد) -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlUnitDetail" runat="server" Visible="false" CssClass="mx-detail" style="border-top-color:#3498db;" ClientIDMode="Static">
            <div class="mx-detail-title" style="border-bottom-color:#3498db;">
                🏢 جزئیات واحد <asp:Label ID="lblDetailUnit" runat="server" ForeColor="#2980b9" />
                <span style="font-size:12px; color:#6c757d; font-weight:normal; margin-right:15px;">
                    (ظرفیت و رزرو در هر تاریخ)
                </span>
            </div>
            
            <div class="mx-table-responsive">
                <asp:GridView ID="gvUnitDetail" runat="server" AutoGenerateColumns="False" 
                    CssClass="mx-detail-table" EmptyDataText="هیچ رکوردی برای این واحد یافت نشد"
                    AllowPaging="True" PageSize="25" OnPageIndexChanging="gvUnitDetail_PageIndexChanging">
                    <HeaderStyle BackColor="#3498db" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="40" />
                        <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" ItemStyle-Width="110" />
                        <asp:BoundField DataField="Capacity" HeaderText="ظرفیت" ItemStyle-Width="80" />
                        <asp:BoundField DataField="Reserved" HeaderText="رزرو شده" ItemStyle-Width="80" />
                        <asp:BoundField DataField="Available" HeaderText="باقیمانده" ItemStyle-Width="80" />
                        <asp:TemplateField HeaderText="درصد تکمیل" ItemStyle-Width="130">
                            <ItemTemplate>
                                <div style="background:#e9ecef;border-radius:8px;height:16px;width:100px;position:relative;overflow:hidden;display:inline-block;">
                                    <div class='<%# GetFillBarClass(Eval("FillPercent")) %>' 
                                        style='<%# "width:" & Eval("FillPercent") & "%" %>'></div>
                                    <div style="position:absolute;top:0;left:0;right:0;bottom:0;text-align:center;font-size:10px;font-weight:700;line-height:16px;color:#2c3e50;">
                                        <%# Eval("FillPercent") %>%
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="mx-pagination" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- دکمه بازگشت -->
        <!-- ============================================ -->
        <div style="margin-top: 25px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="mx-btn mx-btn-back" 
                OnClick="btnBack_Click" style="padding:10px 30px;font-size:14px;" />
        </div>
    </div>

    <!-- ============================================ -->
    <!-- ✅ اسکرول خودکار به پنل جزئیات -->
    <!-- ============================================ -->
    <script type="text/javascript">
        (function () {
            function scrollToDetailPanel() {
                var url = window.location.href;
                var target = null;

                // اگر DetailDate در URL هست → به پنل پرسنل برو
                if (url.indexOf('DetailDate=') > -1) {
                    target = document.getElementById('pnlDateDetail');
                }
                    // اگر UnitID در URL هست (و صفر نیست) → به پنل واحد برو
                else if (url.indexOf('UnitID=') > -1 && url.indexOf('UnitID=0') === -1) {
                    target = document.getElementById('pnlUnitDetail');
                }

                if (target && target.offsetParent !== null) {
                    // اسکرول نرم به پنل
                    setTimeout(function () {
                        target.scrollIntoView({ behavior: 'smooth', block: 'start' });
                    }, 100);
                }
            }

            // بعد از لود کامل صفحه
            if (document.readyState === 'complete') {
                scrollToDetailPanel();
            } else {
                window.addEventListener('load', scrollToDetailPanel);
            }
        })();
    </script>
</asp:Content>