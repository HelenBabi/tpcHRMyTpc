<%@ Page Title="آرشیو پیامک‌های گروهی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExcelSMSArchive.aspx.vb" Inherits="ExcelSMSArchive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .arc-container {
            max-width: 1300px;
            margin: 30px auto;
            padding: 25px 30px;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.08);
            direction: rtl;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .arc-title {
            font-size: 22px;
            font-weight: 700;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e9ecef;
        }
        
        .arc-filter {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            display: flex;
            align-items: flex-end;
            gap: 15px;
            flex-wrap: wrap;
            border-right: 4px solid #3498db;
        }
        
        .arc-filter-group {
            flex: 1;
            min-width: 180px;
        }
        
        .arc-filter-group label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 6px;
            font-size: 13px;
        }
        
        .arc-filter-group input,
        .arc-filter-group select {
            width: 100%;
            padding: 9px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 13px;
            font-family: inherit;
            box-sizing: border-box;
        }
        
        .arc-btn {
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
        
        .arc-btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(0,0,0,0.15);
        }
        
        .arc-btn-search { background: #3498db; color: white; }
        .arc-btn-excel { background: #27ae60; color: white; }
        .arc-btn-back { background: #9b59b6; color: white; }
        
        .arc-table-responsive {
            overflow-x: auto;
            margin-top: 20px;
            max-height: 600px;
            overflow-y: auto;
            border: 1px solid #e9ecef;
            border-radius: 8px;
        }
        
        .arc-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
            text-align: right;
        }
        
        .arc-table th {
            background: #2c3e50;
            color: white;
            padding: 10px 8px;
            text-align: center;
            font-weight: 600;
            position: sticky;
            top: 0;
            z-index: 1;
            white-space: nowrap;
        }
        
        .arc-table td {
            padding: 8px;
            border-bottom: 1px solid #e9ecef;
            text-align: center;
            vertical-align: middle;
        }
        
        .arc-table tr:hover {
            background: #f8f9fa;
        }
        
        .arc-mobile {
            font-family: 'Courier New', monospace;
            font-weight: 600;
            direction: ltr;
        }
        
        .arc-badge-ok { background: #d4edda; color: #155724; padding: 3px 10px; border-radius: 12px; font-size: 11px; font-weight: 600; }
        .arc-badge-failed { background: #f8d7da; color: #721c24; padding: 3px 10px; border-radius: 12px; font-size: 11px; font-weight: 600; }
        
        .arc-summary {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
            margin-bottom: 20px;
        }
        
        .arc-summary-item {
            background: white;
            padding: 15px 20px;
            border-radius: 10px;
            border: 1px solid #e9ecef;
            flex: 1;
            min-width: 150px;
            text-align: center;
            border-top: 4px solid #3498db;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        
        .arc-summary-item .number {
            font-size: 28px;
            font-weight: 700;
            color: #2c3e50;
            margin-bottom: 5px;
        }
        
        .arc-summary-item .label {
            font-size: 12px;
            color: #6c757d;
            font-weight: 600;
        }
        
        .arc-summary-item.blue { border-top-color: #3498db; }
        .arc-summary-item.green { border-top-color: #27ae60; }
        .arc-summary-item.red { border-top-color: #e74c3c; }
        .arc-summary-item.orange { border-top-color: #f39c12; }
        
        .arc-alert {
            padding: 12px 18px;
            border-radius: 8px;
            margin: 15px 0;
            font-size: 14px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .arc-alert-info { background: #d1ecf1; color: #0c5460; border-right: 4px solid #17a2b8; }
        .arc-alert-success { background: #d4edda; color: #155724; border-right: 4px solid #27ae60; }
        .arc-alert-danger { background: #f8d7da; color: #721c24; border-right: 4px solid #e74c3c; }
        
        @media (max-width: 768px) {
            .arc-container { padding: 15px; margin: 10px; }
            .arc-filter { flex-direction: column; align-items: stretch; }
            .arc-summary { flex-direction: column; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="arc-container">
        <div class="arc-title">📦 آرشیو پیامک‌های گروهی</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="arc-alert">
                <asp:Label ID="lblMessageIcon" runat="server" />
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- فیلترها -->
        <!-- ============================================ -->
        <div class="arc-filter">
            <div class="arc-filter-group">
                <label>از تاریخ (شمسی):</label>
                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="1405/01/01" MaxLength="10" />
            </div>
            
            <div class="arc-filter-group">
                <label>تا تاریخ (شمسی):</label>
                <asp:TextBox ID="txtDateTo" runat="server" placeholder="1405/12/29" MaxLength="10" />
            </div>
            
            <div class="arc-filter-group">
                <label>شماره موبایل:</label>
                <asp:TextBox ID="txtMobile" runat="server" placeholder="0914..." />
            </div>
            
            <div class="arc-filter-group">
                <label>کد پرسنلی:</label>
                <asp:TextBox ID="txtPersonCode" runat="server" placeholder="کد پرسنلی" />
            </div>
            
            <asp:Button ID="btnSearch" runat="server" Text="🔍 جستجو" 
                CssClass="arc-btn arc-btn-search" OnClick="btnSearch_Click" />
            
            <asp:Button ID="btnExportExcel" runat="server" Text="📊 خروجی اکسل" 
                CssClass="arc-btn arc-btn-excel" OnClick="btnExportExcel_Click" />
            
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" 
                CssClass="arc-btn arc-btn-back" OnClick="btnBack_Click" />
        </div>
        
        <!-- ============================================ -->
        <!-- آمار -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlSummary" runat="server" Visible="false">
            <div class="arc-summary">
                <div class="arc-summary-item blue">
                    <div class="number"><asp:Label ID="lblTotalCount" runat="server" Text="0" /></div>
                    <div class="label">کل رکوردها</div>
                </div>
                <div class="arc-summary-item green">
                    <div class="number"><asp:Label ID="lblSuccessCount" runat="server" Text="0" /></div>
                    <div class="label">ارسال موفق</div>
                </div>
                <div class="arc-summary-item red">
                    <div class="number"><asp:Label ID="lblFailedCount" runat="server" Text="0" /></div>
                    <div class="label">ارسال ناموفق</div>
                </div>
                <div class="arc-summary-item orange">
                    <div class="number"><asp:Label ID="lblBatchCount" runat="server" Text="0" /></div>
                    <div class="label">تعداد ارسال‌ها</div>
                </div>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- جدول -->
        <!-- ============================================ -->
        <div class="arc-table-responsive">
            <asp:GridView ID="gvArchive" runat="server" 
                AutoGenerateColumns="False" 
                CssClass="arc-table"
                AllowPaging="True" 
                PageSize="50"
                OnPageIndexChanging="gvArchive_PageIndexChanging"
                EmptyDataText="هیچ رکوردی در آرشیو یافت نشد">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                    <asp:BoundField DataField="SendDatePersian" HeaderText="تاریخ ارسال" ItemStyle-Width="90" />
                    <asp:BoundField DataField="SendTime" HeaderText="ساعت" ItemStyle-Width="70" />
                    <asp:BoundField DataField="FirstName" HeaderText="نام" ItemStyle-Width="100" />
                    <asp:BoundField DataField="LastName" HeaderText="نام خانوادگی" ItemStyle-Width="120" />
                    <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" ItemStyle-Width="80" />
                    <asp:BoundField DataField="Mobile" HeaderText="موبایل" 
                        ItemStyle-CssClass="arc-mobile" ItemStyle-Width="100" />
                    
                    <asp:TemplateField HeaderText="متن پیامک" ItemStyle-Width="300">
                        <ItemTemplate>
                            <div style="text-align:right; font-size:11px; line-height:1.6; max-height:60px; overflow:hidden;">
                                <%# Eval("SmsFinalText") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                        <ItemTemplate>
                            <span class='<%# GetStatusClass(Eval("Status")) %>'>
                                <%# Eval("StatusText") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="CreatedBy" HeaderText="کاربر" ItemStyle-Width="100" />
                </Columns>
                <PagerStyle CssClass="arc-pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>