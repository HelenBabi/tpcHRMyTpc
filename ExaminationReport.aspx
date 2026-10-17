<%@ Page Title="گزارش درخواست‌های معاینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExaminationReport.aspx.vb" Inherits="ExaminationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .exm-container {
            max-width: 1200px;
            margin: 30px auto;
            padding: 25px 30px;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.08);
            direction: rtl;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .exm-title {
            font-size: 22px;
            font-weight: 700;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e9ecef;
        }
        
        .exm-filter {
            background: #f8f9fa;
            padding: 15px 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 15px;
            flex-wrap: wrap;
        }
        .exm-filter label {
            font-weight: 600;
            color: #2c3e50;
            font-size: 14px;
        }
        .exm-filter select, .exm-filter input {
            padding: 8px 15px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 14px;
            font-family: inherit;
            background: #ffffff;
            min-width: 150px;
        }
        .exm-filter select:focus, .exm-filter input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        
        .exm-filter .exm-btn {
            padding: 8px 25px;
            border: none;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            background: #3498db;
            color: #ffffff;
        }
        .exm-filter .exm-btn:hover { background: #2980b9; }
        .exm-filter .exm-btn-excel { background: #27ae60; }
        .exm-filter .exm-btn-excel:hover { background: #219a52; }
        
        .exm-table-responsive {
            overflow-x: auto;
        }
        .exm-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            text-align: right;
        }
        .exm-table th {
            background: #2c3e50;
            color: #ffffff;
            padding: 10px 12px;
            white-space: nowrap;
            font-weight: 600;
        }
        .exm-table td {
            padding: 10px 12px;
            border-bottom: 1px solid #e9ecef;
            vertical-align: middle;
        }
        .exm-table tr:hover { background: #f8f9fa; }
        
        .exm-status-pending { background: #fff3cd; color: #856404; padding: 3px 10px; border-radius: 12px; display: inline-block; font-size: 12px; }
        .exm-status-approved { background: #d4edda; color: #155724; padding: 3px 10px; border-radius: 12px; display: inline-block; font-size: 12px; }
        .exm-status-rejected { background: #f8d7da; color: #721c24; padding: 3px 10px; border-radius: 12px; display: inline-block; font-size: 12px; }
        .exm-status-done { background: #cce5ff; color: #004085; padding: 3px 10px; border-radius: 12px; display: inline-block; font-size: 12px; }
        
        .exm-alert {
            padding: 12px 18px;
            border-radius: 8px;
            margin: 15px 0;
            font-size: 14px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .exm-alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .exm-alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .exm-alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .exm-alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        
        .exm-summary {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
            margin-bottom: 20px;
        }
        .exm-summary-item {
            background: #f8f9fa;
            padding: 10px 20px;
            border-radius: 8px;
            border-right: 4px solid #3498db;
            flex: 1;
            min-width: 150px;
        }
        .exm-summary-item .number {
            font-size: 24px;
            font-weight: 700;
            color: #2c3e50;
        }
        .exm-summary-item .label {
            font-size: 13px;
            color: #6c757d;
        }
        
        .exm-pagination {
            padding: 10px 0;
            text-align: center;
        }
        .exm-pagination a, .exm-pagination span {
            padding: 5px 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin: 0 2px;
            text-decoration: none;
            color: #2c3e50;
        }
        .exm-pagination a:hover {
            background: #3498db;
            color: #ffffff;
            border-color: #3498db;
        }
        .exm-pagination span {
            background: #3498db;
            color: #ffffff;
            border-color: #3498db;
        }
        
        .exm-btn-back {
            background: #9b59b6;
            color: #ffffff;
            padding: 10px 30px;
            border: none;
            border-radius: 25px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
        }
        .exm-btn-back:hover { background: #8e44ad; }
        .exm-comment-cell {
            max-width: 150px;
            word-wrap: break-word;
            font-size: 12px;
            color: #6c757d;
        }
        
        .exm-national-code {
            font-family: 'Courier New', monospace;
            font-weight: 600;
            letter-spacing: 1px;
            color: #2c3e50;
            direction: ltr;
            text-align: center;
        }
        
        .exm-supervisor-name {
            font-weight: 600;
            color: #2c3e50;
            font-size: 13px;
        }
        
        @media (max-width: 768px) {
            .exm-container { padding: 15px; margin: 10px; }
            .exm-table { font-size: 11px; }
            .exm-table th, .exm-table td { padding: 5px 8px; }
            .exm-filter { flex-direction: column; align-items: stretch; }
            .exm-summary { flex-direction: column; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
        <div class="exm-title">📊 گزارش درخواست‌های معاینه</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='exm-alert exm-alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- فیلترها -->
        <div class="exm-filter">
            <div>
                <label>سال شمسی:</label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div>
                <label>وضعیت:</label>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="همه" Value="0" />
                    <asp:ListItem Text="درخواست پرسنل" Value="1" />
                    <asp:ListItem Text="تایید رئیس" Value="2" />
                    <asp:ListItem Text="تایید کارشناس بهداشت" Value="4" />
                    <asp:ListItem Text="رد شده" Value="6" />
                </asp:DropDownList>
            </div>
            <div>
                <label>واحد:</label>
                <asp:DropDownList ID="ddlUnit" runat="server">
                    <asp:ListItem Text="همه" Value="0" />
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="🔍 جستجو" CssClass="exm-btn" OnClick="btnSearch_Click" />
            <asp:Button ID="btnExportExcel" runat="server" Text="📊 خروجی اکسل" CssClass="exm-btn exm-btn-excel" OnClick="btnExportExcel_Click" />
        </div>
        
        <!-- آمار -->
        <div class="exm-summary">
            <div class="exm-summary-item">
                <div class="number"><asp:Label ID="lblTotalRequests" runat="server" Text="0" /></div>
                <div class="label">کل درخواست‌ها</div>
            </div>
            <div class="exm-summary-item" style="border-right-color: #fff3cd;">
                <div class="number"><asp:Label ID="lblPendingRequests" runat="server" Text="0" /></div>
                <div class="label">در انتظار تایید</div>
            </div>
            <div class="exm-summary-item" style="border-right-color: #27ae60;">
                <div class="number"><asp:Label ID="lblApprovedRequests" runat="server" Text="0" /></div>
                <div class="label">تایید شده</div>
            </div>
            <div class="exm-summary-item" style="border-right-color: #e74c3c;">
                <div class="number"><asp:Label ID="lblRejectedRequests" runat="server" Text="0" /></div>
                <div class="label">رد شده</div>
            </div>
        </div>
        
        <!-- جدول گزارش -->
        <div class="exm-table-responsive">
            <asp:GridView ID="gvReport" runat="server" 
                AutoGenerateColumns="False" 
                CssClass="exm-table" 
                AllowPaging="True" 
                PageSize="20"
                OnPageIndexChanging="gvReport_PageIndexChanging"
                EmptyDataText="هیچ درخواستی یافت نشد"
                DataKeyNames="ID"
                ShowHeaderWhenEmpty="True">
               <Columns>
    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
    <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" />
    <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
    <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" ItemStyle-CssClass="exm-national-code" />
    <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" />
    <asp:BoundField DataField="UnitName" HeaderText="واحد" />
    
    <%-- ستون نام سرپرست --%>
    <asp:TemplateField HeaderText="نام سرپرست" ItemStyle-Width="150">
        <ItemTemplate>
            <span class="exm-supervisor-name">
                <%# Eval("SupervisorName") %>
            </span>
        </ItemTemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="وضعیت">
        <ItemTemplate>
            <span class="exm-status-<%# Eval("StatusClass") %>">
                <%# Eval("StatusText") %>
            </span>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="توضیحات رئیس">
        <ItemTemplate>
            <div class="exm-comment-cell">
                <%# Eval("SupervisorComment") %>
            </div>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="توضیحات کارشناس بهداشت">
        <ItemTemplate>
            <div class="exm-comment-cell">
                <%# Eval("HealthExpertComment") %>
            </div>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="RequestDate" HeaderText="تاریخ درخواست" />
    <asp:BoundField DataField="SupervisorApproveDate" HeaderText="تاریخ تایید رئیس" />
    <asp:BoundField DataField="HealthExpertApproveDate" HeaderText="تاریخ تایید کارشناس" />
</Columns>
                <PagerStyle CssClass="exm-pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        
        <div style="margin-top: 20px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="exm-btn-back" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>