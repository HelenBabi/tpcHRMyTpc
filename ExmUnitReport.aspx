<%@ Page Title="گزارش آماری واحدها" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExmUnitReport.aspx.vb" Inherits="ExmUnitReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .rpt-container {
            max-width: 1400px;
            margin: 30px auto;
            padding: 25px 30px;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.08);
            direction: rtl;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .rpt-title {
            font-size: 22px;
            font-weight: 700;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e9ecef;
        }
        
        /* ============================================ */
        /* فیلترها */
        /* ============================================ */
        .rpt-filter {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 25px;
            display: flex;
            align-items: flex-end;
            gap: 15px;
            flex-wrap: wrap;
            border-right: 4px solid #3498db;
        }
        .rpt-filter-group {
            flex: 1;
            min-width: 200px;
        }
        .rpt-filter-group label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 6px;
            font-size: 13px;
        }
        .rpt-filter-group select,
        .rpt-filter-group input {
            width: 100%;
            padding: 9px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 13px;
            font-family: inherit;
            background: #ffffff;
            box-sizing: border-box;
        }
        .rpt-filter-group select:focus,
        .rpt-filter-group input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        
        .rpt-btn {
            padding: 9px 25px;
            border: none;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            font-family: inherit;
            white-space: nowrap;
        }
        .rpt-btn:hover { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .rpt-btn-search { background: #3498db; color: #ffffff; }
        .rpt-btn-search:hover { background: #2980b9; }
        .rpt-btn-excel { background: #27ae60; color: #ffffff; }
        .rpt-btn-excel:hover { background: #219a52; }
        .rpt-btn-back { background: #9b59b6; color: #ffffff; }
        .rpt-btn-back:hover { background: #8e44ad; }
        
        /* ============================================ */
        /* کارت‌های آماری */
        /* ============================================ */
        .rpt-summary {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
            margin-bottom: 25px;
        }
        .rpt-summary-item {
            background: #ffffff;
            padding: 15px 20px;
            border-radius: 10px;
            border: 1px solid #e9ecef;
            flex: 1;
            min-width: 150px;
            text-align: center;
            border-top: 4px solid #3498db;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        .rpt-summary-item .number {
            font-size: 28px;
            font-weight: 700;
            color: #2c3e50;
            margin-bottom: 5px;
        }
        .rpt-summary-item .label {
            font-size: 12px;
            color: #6c757d;
            font-weight: 600;
        }
        .rpt-summary-item.blue { border-top-color: #3498db; }
        .rpt-summary-item.green { border-top-color: #27ae60; }
        .rpt-summary-item.orange { border-top-color: #f39c12; }
        .rpt-summary-item.red { border-top-color: #e74c3c; }
        .rpt-summary-item.purple { border-top-color: #9b59b6; }
        
        /* ============================================ */
        /* جدول */
        /* ============================================ */
        .rpt-table-responsive {
            overflow-x: auto;
            margin-top: 20px;
        }
        .rpt-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            text-align: right;
        }
        .rpt-table th {
            background: #2c3e50;
            color: #ffffff;
            padding: 10px 12px;
            white-space: nowrap;
            font-weight: 600;
            text-align: center;
        }
        .rpt-table td {
            padding: 10px 12px;
            border-bottom: 1px solid #e9ecef;
            vertical-align: middle;
            text-align: center;
        }
        .rpt-table tr:hover { background: #f8f9fa; }
        
        .badge-active {
            background: #d4edda;
            color: #155724;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        .badge-inactive {
            background: #f8d7da;
            color: #721c24;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        .badge-full {
            background: #fff3cd;
            color: #856404;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        
        .capacity-bar {
            display: inline-block;
            width: 100px;
            height: 20px;
            background: #e9ecef;
            border-radius: 10px;
            overflow: hidden;
            position: relative;
        }
        .capacity-bar-fill {
            height: 100%;
            background: linear-gradient(90deg, #27ae60, #2ecc71);
            border-radius: 10px;
            transition: width 0.3s;
        }
        .capacity-bar-fill.warning {
            background: linear-gradient(90deg, #f39c12, #f1c40f);
        }
        .capacity-bar-fill.danger {
            background: linear-gradient(90deg, #e74c3c, #c0392b);
        }
        .capacity-bar-text {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 11px;
            font-weight: 700;
            color: #2c3e50;
        }
        
        /* ============================================ */
        /* هشدار */
        /* ============================================ */
        .rpt-alert {
            padding: 12px 18px;
            border-radius: 8px;
            margin: 15px 0;
            font-size: 14px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .rpt-alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .rpt-alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .rpt-alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .rpt-alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        
        .rpt-pagination {
            padding: 10px 0;
            text-align: center;
        }
        .rpt-pagination a, .rpt-pagination span {
            padding: 5px 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin: 0 2px;
            text-decoration: none;
            color: #2c3e50;
        }
        .rpt-pagination a:hover {
            background: #3498db;
            color: #ffffff;
            border-color: #3498db;
        }
        .rpt-pagination span {
            background: #3498db;
            color: #ffffff;
            border-color: #3498db;
        }
        
        /* ============================================ */
        /* ✅ ماتریس (تاریخ × واحد) */
        /* ============================================ */
        .matrix-panel {
            background: #ffffff;
            padding: 20px;
            border-radius: 12px;
            margin-top: 30px;
            box-shadow: 0 2px 12px rgba(0,0,0,0.08);
            border-top: 4px solid #f39c12;
        }
        .matrix-title {
            color: #2c3e50;
            border-bottom: 2px solid #f39c12;
            padding-bottom: 10px;
            margin-bottom: 20px;
            font-size: 16px;
            font-weight: 700;
        }
        .matrix-table {
            width: 100%;
            border-collapse: collapse;
            direction: rtl;
            font-family: Tahoma, Arial;
            font-size: 13px;
        }
        .matrix-table th {
            padding: 10px 8px;
            border: 1px solid #ddd;
            text-align: center;
            font-weight: 600;
            white-space: nowrap;
        }
        .matrix-table td {
            padding: 8px;
            border: 1px solid #ddd;
            text-align: center;
            vertical-align: middle;
        }
        .matrix-table .date-cell {
            background: #fff3cd;
            font-weight: 700;
            color: #856404;
            vertical-align: middle;
        }
        .matrix-table .row-num-cell {
            background: #f5f5f5;
            font-weight: 700;
            color: #555;
            vertical-align: middle;
        }
        .matrix-table .assigned-row {
            background: #e8f4fd;
        }
        .matrix-table .reserved-row {
            background: #d4edda;
        }
        .matrix-table .total-col {
            background: #fdebd0;
            font-weight: 700;
            color: #a04000;
        }
        .matrix-table .stat-badge {
            padding: 4px 12px;
            border-radius: 12px;
            font-size: 12px;
            display: inline-block;
            font-weight: 600;
        }
        .matrix-table .badge-assigned {
            background: #3498db;
            color: white;
        }
        .matrix-table .badge-reserved {
            background: #27ae60;
            color: white;
        }
        .matrix-table tr:hover {
            background: #f8f9fa;
        }
        
        @media (max-width: 768px) {
            .rpt-container { padding: 15px; margin: 10px; }
            .rpt-filter { flex-direction: column; align-items: stretch; }
            .rpt-table { font-size: 11px; }
            .rpt-table th, .rpt-table td { padding: 5px 8px; }
            .rpt-summary { flex-direction: column; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="rpt-container">
        <div class="rpt-title">📊 گزارش آماری واحدهای معاینات</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="rpt-alert">
                <asp:Label ID="lblMessageIcon" runat="server" />
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- فیلترها -->
        <!-- ============================================ -->
        <div class="rpt-filter">
            <div class="rpt-filter-group">
                <label>🏢 انتخاب واحد:</label>
                <asp:DropDownList ID="ddlUnit" runat="server">
                    <asp:ListItem Text="-- همه واحدها --" Value="0" />
                </asp:DropDownList>
            </div>
            
            <div class="rpt-filter-group">
                <label>📅 از تاریخ معاینه:</label>
                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="1405/01/01" MaxLength="10" />
            </div>
            
            <div class="rpt-filter-group">
                <label>📅 تا تاریخ معاینه:</label>
                <asp:TextBox ID="txtDateTo" runat="server" placeholder="1405/12/29" MaxLength="10" />
            </div>
            
            <div class="rpt-filter-group">
                <label>وضعیت:</label>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="همه" Value="0" />
                    <asp:ListItem Text="فعال" Value="1" />
                    <asp:ListItem Text="غیرفعال" Value="2" />
                </asp:DropDownList>
            </div>
            
            <asp:Button ID="btnSearch" runat="server" Text="🔍 جستجو" 
                CssClass="rpt-btn rpt-btn-search" OnClick="btnSearch_Click" />
            
            <asp:Button ID="btnExportExcel" runat="server" Text="📊 خروجی اکسل" 
                CssClass="rpt-btn rpt-btn-excel" OnClick="btnExportExcel_Click" />
        </div>
        
        <!-- ============================================ -->
        <!-- کارت‌های آماری -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlSummary" runat="server" Visible="false">
            <div class="rpt-summary">
                <div class="rpt-summary-item blue">
                    <div class="number"><asp:Label ID="lblTotalUnits" runat="server" Text="0" /></div>
                    <div class="label">تعداد واحدها</div>
                </div>
                <div class="rpt-summary-item green">
                    <div class="number"><asp:Label ID="lblTotalCapacity" runat="server" Text="0" /></div>
                    <div class="label">کل ظرفیت</div>
                </div>
                <div class="rpt-summary-item orange">
                    <div class="number"><asp:Label ID="lblTotalReserved" runat="server" Text="0" /></div>
                    <div class="label">رزرو شده</div>
                </div>
                <div class="rpt-summary-item purple">
                    <div class="number"><asp:Label ID="lblTotalAvailable" runat="server" Text="0" /></div>
                    <div class="label">ظرفیت باقیمانده</div>
                </div>
                <div class="rpt-summary-item red">
                    <div class="number"><asp:Label ID="lblFullUnits" runat="server" Text="0" /></div>
                    <div class="label">واحدهای تکمیل</div>
                </div>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- جدول گزارش اصلی -->
        <!-- ============================================ -->
        <div class="rpt-table-responsive">
            <asp:GridView ID="gvReport" runat="server" 
                AutoGenerateColumns="False" 
                CssClass="rpt-table" 
                AllowPaging="True" 
                PageSize="20"
                OnPageIndexChanging="gvReport_PageIndexChanging"
                OnRowCommand="gvReport_RowCommand"
                EmptyDataText="هیچ داده‌ای برای نمایش وجود ندارد"
                DataKeyNames="UnitID">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                    <asp:BoundField DataField="UnitName" HeaderText="نام واحد" ItemStyle-Width="180" />
                    
                    <asp:TemplateField HeaderText="وضعیت واحد" ItemStyle-Width="90">
                        <ItemTemplate>
                            <span class='<%# IIf(Convert.ToBoolean(Eval("IsActive")), "badge-active", "badge-inactive") %>'>
                                <%# IIf(Convert.ToBoolean(Eval("IsActive")), "✓ فعال", "✗ غیرفعال") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="TotalCapacity" HeaderText="کل ظرفیت" ItemStyle-Width="80" />
                    <asp:BoundField DataField="TotalReserved" HeaderText="رزرو شده" ItemStyle-Width="80" />
                    <asp:BoundField DataField="TotalAvailable" HeaderText="باقیمانده" ItemStyle-Width="80" />
                    
                    <asp:TemplateField HeaderText="درصد تکمیل" ItemStyle-Width="150">
                        <ItemTemplate>
                            <div class="capacity-bar">
                                <div class='<%# GetCapacityBarClass(Eval("FillPercent")) %>' 
                                    style='<%# "width:" & Eval("FillPercent") & "%" %>'></div>
                                <div class="capacity-bar-text"><%# Eval("FillPercent") %>%</div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="ExamDates" HeaderText="تاریخ‌های معاینه" ItemStyle-Width="150" />
                    
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="90">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDetails" runat="server" 
                                CommandName="ShowDetails" 
                                CommandArgument='<%# Eval("UnitID") %>'
                                CssClass="rpt-btn rpt-btn-search" 
                                style="padding:5px 12px;font-size:11px;text-decoration:none;display:inline-block;">
                                📋 جزئیات
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="rpt-pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        
        <!-- ============================================ -->
        <!-- ✅ گزارش ماتریسی: تاریخ × واحد -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlMatrix" runat="server" Visible="false" CssClass="matrix-panel">
            
            <div class="matrix-title">
                📊 گزارش تفکیکی تاریخ × واحد
                <span style="font-size:12px; color:#6c757d; font-weight:normal; margin-right:15px;">
                    (تعیین شده = ظرفیت تعریف‌شده | رزرو شده = تعداد رزرو فعال)
                </span>
            </div>

            <div class="rpt-table-responsive">
                <table class="matrix-table">
                    <thead>
                        <tr>
                            <th rowspan="2" style="background:#2c3e50;color:white;">#</th>
                            <th rowspan="2" style="background:#2c3e50;color:white;">تاریخ معاینه</th>
                            <th rowspan="2" style="background:#2c3e50;color:white;">نوع آمار</th>
                            <asp:Repeater ID="rptMatrixHeader" runat="server">
                                <ItemTemplate>
                                    <th style="background:#f39c12;color:white;"><%# Eval("UnitName") %></th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <th style="background:#e74c3c;color:white;">جمع کل</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptMatrixBody" runat="server">
                            <ItemTemplate>
                                <%# GetMatrixRowHtml(Container.DataItem, "") %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
        
        <!-- دکمه بازگشت -->
        <div style="margin-top: 25px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" 
                CssClass="rpt-btn rpt-btn-back" OnClick="btnBack_Click" 
                style="padding:10px 30px;font-size:14px;" />
        </div>
    </div>
</asp:Content>