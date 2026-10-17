<%@ Page Title="برنامه زمانبندی دوره‌های آموزشی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnProgramsFrm.aspx.vb" Inherits="trnProgramsFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        :root {
            --primary: #667eea;
            --secondary: #764ba2;
            --success: #28a745;
            --warning: #ffc107;
            --danger: #dc3545;
            --info: #17a2b8;
            --dark: #343a40;
            --light: #f8f9fa;
        }
        
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        
        /* آیکون‌های جایگزین با ایموجی */
        .icon-emoji {
            font-size: 18px;
            display: inline-block;
        }
        .icon-lg { font-size: 24px; }
        .icon-md { font-size: 20px; }
        
        .page-header {
            background: linear-gradient(135deg, var(--primary) 0%, var(--secondary) 100%);
            color: white;
            padding: 25px 30px;
            border-radius: 20px;
            margin-bottom: 30px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.1);
            position: relative;
            overflow: hidden;
        }
        
        .page-header::before {
            content: '';
            position: absolute;
            top: -50%;
            right: -50%;
            width: 200%;
            height: 200%;
            background: radial-gradient(circle, rgba(255,255,255,0.1) 0%, transparent 70%);
        }
        
        .page-header h2 {
            margin: 0;
            font-weight: bold;
            position: relative;
            z-index: 1;
        }
        
        .page-header p {
            margin: 10px 0 0;
            opacity: 0.9;
            position: relative;
            z-index: 1;
        }
        
        .card-glass {
            background: white;
            border-radius: 20px;
            box-shadow: 0 10px 35px rgba(0,0,0,0.08);
            overflow: hidden;
            transition: all 0.3s ease;
            margin-bottom: 25px;
        }
        
        .card-glass:hover {
            box-shadow: 0 15px 40px rgba(0,0,0,0.12);
        }
        
        .card-header-custom {
            background: linear-gradient(135deg, var(--primary) 0%, var(--secondary) 100%);
            color: white;
            padding: 18px 25px;
            font-size: 18px;
            font-weight: bold;
        }
        
        .card-body-custom {
            padding: 25px;
        }
        
        .grid-container {
            overflow-x: auto;
            border-radius: 15px;
        }
        
        .modern-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }
        
        .modern-grid th {
            background: linear-gradient(135deg, var(--primary) 0%, var(--secondary) 100%);
            color: white;
            padding: 14px 12px;
            text-align: center;
            white-space: nowrap;
        }
        
        .modern-grid td {
            padding: 12px;
            text-align: center;
            border-bottom: 1px solid #e9ecef;
        }
        
        .modern-grid tr:hover td {
            background-color: #f8f9fa;
        }
        
        .status-registering td {
            background: linear-gradient(135deg, #28a74515 0%, #20c99715 100%);
        }
        
        .status-expired td {
            background: linear-gradient(135deg, #dc354515 0%, #ff6b6b15 100%);
        }
        
        .status-cancelled td {
            background: linear-gradient(135deg, #6c757d15 0%, #adb5bd15 100%);
        }
        
        .status-pending td {
            background: linear-gradient(135deg, #ffc10715 0%, #ff980015 100%);
        }
        
        .badge-status {
            display: inline-block;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 11px;
            font-weight: bold;
        }
        
        .badge-success {
            background: linear-gradient(135deg, #28a745, #20c997);
            color: white;
        }
        
        .badge-danger {
            background: linear-gradient(135deg, #dc3545, #ff6b6b);
            color: white;
        }
        
        .badge-secondary {
            background: linear-gradient(135deg, #6c757d, #8a929b);
            color: white;
        }
        
        .badge-warning {
            background: linear-gradient(135deg, #ffc107, #ff9800);
            color: #333;
        }
        
        .btn-download {
            background: linear-gradient(135deg, var(--info), #138496);
            color: white;
            border: none;
            padding: 6px 15px;
            border-radius: 20px;
            font-size: 12px;
            cursor: pointer;
            transition: all 0.3s ease;
            text-decoration: none;
            display: inline-flex;
            align-items: center;
            gap: 5px;
        }
        
        .btn-download:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(23,162,184,0.3);
            color: white;
            text-decoration: none;
        }
        
        .btn-download-disabled {
            background: linear-gradient(135deg, #6c757d, #8a929b);
            color: white;
            border: none;
            padding: 6px 15px;
            border-radius: 20px;
            font-size: 12px;
            text-decoration: none;
            display: inline-flex;
            align-items: center;
            gap: 5px;
            cursor: not-allowed;
            opacity: 0.6;
        }
        
        .btn-back {
            background: linear-gradient(135deg, var(--warning), #ff9800);
            color: #333;
            border: none;
            padding: 10px 30px;
            border-radius: 50px;
            font-weight: bold;
            transition: all 0.3s ease;
            cursor: pointer;
        }
        
        .btn-back:hover {
            transform: translateX(-5px);
            box-shadow: 0 5px 20px rgba(255,193,7,0.4);
            color: #333;
        }
        
        .search-box {
            background: white;
            border-radius: 50px;
            padding: 5px 15px;
            border: 2px solid #e0e0e0;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
        }
        
        .search-box:focus-within {
            border-color: var(--primary);
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
        }
        
        .search-box input {
            border: none;
            padding: 10px;
            width: 250px;
            outline: none;
        }
        
        .stats-row {
            display: flex;
            gap: 20px;
            margin-bottom: 30px;
            flex-wrap: wrap;
        }
        
        .stat-card {
            background: white;
            border-radius: 15px;
            padding: 20px;
            text-align: center;
            box-shadow: 0 5px 15px rgba(0,0,0,0.08);
            transition: all 0.3s ease;
            flex: 1;
            min-width: 150px;
        }
        
        .stat-card:hover {
            transform: translateY(-5px);
        }
        
        .stat-card .count {
            font-size: 28px;
            font-weight: bold;
        }
        
        .stat-card .label {
            color: #666;
            font-size: 13px;
        }
        
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
        
        .fade-in-up {
            animation: fadeInUp 0.5s ease-out;
        }
        
        @media (max-width: 768px) {
            .page-header h2 {
                font-size: 20px;
            }
            .card-body-custom {
                padding: 15px;
            }
            .stats-row {
                flex-direction: column;
            }
            .stat-card {
                min-width: auto;
            }
        }
        
        @media (max-width: 576px) {
            .btn-back {
                width: 100%;
            }
            .search-box {
                width: 100%;
                margin-bottom: 10px;
            }
            .search-box input {
                width: 100%;
            }
        }
        
        .filter-select {
            border-radius: 50px;
            padding: 10px 15px;
            border: 2px solid #e0e0e0;
            background: white;
            outline: none;
        }
        
        .filter-select:focus {
            border-color: var(--primary);
        }
        
        .flex-between {
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 15px;
            margin-bottom: 20px;
        }
        
        .text-left {
            text-align: left;
        }
        
        .text-center {
            text-align: center;
        }
        
        .mt-3 {
            margin-top: 15px;
        }
        
        .mb-4 {
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="fade-in-up">
        
        <div class="page-header">
            <h2>
                <span class="icon-emoji icon-md">📅</span>
                برنامه زمانبندی دوره‌های آموزشی
            </h2>
            <p>
                <span class="icon-emoji">ℹ️</span>
                لیست کامل دوره‌های آموزشی قابل ثبت‌نام
            </p>
        </div>
        
        <div class="card-glass">
            <div class="card-header-custom">
                <span class="icon-emoji icon-md">📋</span>
                لیست دوره‌های آموزشی
            </div>
            <div class="card-body-custom">
                
                <div class="stats-row">
                    <div class="stat-card">
                        <div><span class="icon-emoji icon-lg">🎓</span></div>
                        <div class="count" id="totalCount">0</div>
                        <div class="label">کل دوره‌ها</div>
                    </div>
                    <div class="stat-card">
                        <div><span class="icon-emoji icon-lg">▶️</span></div>
                        <div class="count" id="activeCount">0</div>
                        <div class="label">در حال برگزاری</div>
                    </div>
                    <div class="stat-card">
                        <div><span class="icon-emoji icon-lg">⏰</span></div>
                        <div class="count" id="pendingCount">0</div>
                        <div class="label">در حال ثبت‌نام</div>
                    </div>
                    <div class="stat-card">
                        <div><span class="icon-emoji icon-lg">✅</span></div>
                        <div class="count" id="completedCount">0</div>
                        <div class="label">برگزار شده</div>
                    </div>
                </div>
                
                <div class="flex-between">
                    <div class="search-box">
                        <span class="icon-emoji">🔍</span>
                        <input type="text" id="searchInput" placeholder="جستجو در دوره‌ها...">
                    </div>
                    <div>
                        <select id="statusFilter" class="filter-select">
                            <option value="all">همه وضعیت‌ها</option>
                            <option value="در حال ثبت‌نام">در حال ثبت‌نام</option>
                            <option value="در حال برگزاری">در حال برگزاری</option>
                            <option value="برگزار شده">برگزار شده</option>
                            <option value="لغو شده">لغو شده</option>
                        </select>
                    </div>
                </div>
                
                <div class="grid-container">
                    <asp:GridView ID="ASPxGrvHead0" runat="server" 
                        AutoGenerateColumns="False" 
                        DataKeyNames="id" 
                        Width="100%" 
                        BorderStyle="None" 
                        GridLines="None"
                        CssClass="modern-grid"
                        OnRowDataBound="ASPxGrvHead0_RowDataBound">
                        
                        <AlternatingRowStyle BackColor="#F8F9FA" />
                        
                        <Columns>
                            <asp:BoundField DataField="idd" HeaderText="ردیف">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="onvan" HeaderText="عنوان دوره">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="vahedha" HeaderText="واحدهای شرکت کننده">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="tarixShuru" HeaderText="تاریخ شروع">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="tarixEtmam" HeaderText="تاریخ اتمام">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="saat" HeaderText="ساعت برگزاری">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="makan" HeaderText="مکان برگزاری">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="modares" HeaderText="مدرس">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="فایل آموزشی">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" 
                                        CssClass="btn-download" 
                                        OnClick="lnkDownload_Click"
                                        CommandArgument='<%# Eval("id") %>'>
                                        <span class="icon-emoji">📥</span> دانلود
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="statValue" HeaderText="وضعیت">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="typevalue" HeaderText="نوع دوره">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        
                        <HeaderStyle BackColor="Transparent" Font-Bold="true" ForeColor="White" />
                        
                        <EmptyDataTemplate>
                            <div class="text-center" style="padding: 50px;">
                                <span class="icon-emoji" style="font-size: 48px; color: #ccc;">📭</span>
                                <p style="margin-top: 15px; color: #999;">هیچ دوره آموزشی یافت نشد</p>
                            </div>
                        </EmptyDataTemplate>
                        
                    </asp:GridView>
                </div>
                
            </div>
        </div>
        
        <br />
        
        <div class="row mt-3">
            <div class="col-md-12 text-left">
                <asp:Button ID="btnBack" runat="server" 
                    CssClass="btn-back" 
                    Text="↩️ بازگشت" 
                    ValidationGroup="b" />
            </div>
        </div>
        
    </div>
    
    <!-- اسکریپت‌های محلی (در MasterPage موجود است) -->
    <script type="text/javascript">
        // تابع به‌روزرسانی آمار
        function updateStatsDisplay(total, active, pending, completed) {
            var totalEl = document.getElementById("totalCount");
            var activeEl = document.getElementById("activeCount");
            var pendingEl = document.getElementById("pendingCount");
            var completedEl = document.getElementById("completedCount");

            if (totalEl) totalEl.innerText = total || 0;
            if (activeEl) activeEl.innerText = active || 0;
            if (pendingEl) pendingEl.innerText = pending || 0;
            if (completedEl) completedEl.innerText = completed || 0;
        }

        // تابع فیلتر جدول
        function filterTable() {
            var input = document.getElementById("searchInput");
            if (!input) return;

            var filter = input.value.toUpperCase();
            var statusFilter = document.getElementById("statusFilter");
            var selectedStatus = statusFilter ? statusFilter.value : "all";
            var table = document.querySelector(".modern-grid");

            if (!table) return;

            var rows = table.getElementsByTagName("tr");
            var visibleCount = { total: 0, active: 0, pending: 0, completed: 0 };

            for (var i = 1; i < rows.length; i++) {
                var cells = rows[i].getElementsByTagName("td");
                if (cells.length === 0) continue;

                var titleCell = cells[1];
                var statusCell = cells[9];

                var titleMatch = titleCell && titleCell.textContent.toUpperCase().indexOf(filter) > -1;
                var statusMatch = selectedStatus === "all" || (statusCell && statusCell.textContent.trim() === selectedStatus);

                if (titleMatch && statusMatch) {
                    rows[i].style.display = "";
                    visibleCount.total++;
                    var status = statusCell ? statusCell.textContent.trim() : "";
                    if (status === "در حال برگزاری") visibleCount.active++;
                    else if (status === "در حال ثبت‌نام") visibleCount.pending++;
                    else if (status === "برگزار شده") visibleCount.completed++;
                } else {
                    rows[i].style.display = "none";
                }
            }

            updateStatsDisplay(visibleCount.total, visibleCount.active, visibleCount.pending, visibleCount.completed);
        }

        // بارگذاری اولیه
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', function () {
                document.getElementById("searchInput") && document.getElementById("searchInput").addEventListener("keyup", filterTable);
                document.getElementById("statusFilter") && document.getElementById("statusFilter").addEventListener("change", filterTable);
            });
        } else {
            document.getElementById("searchInput") && document.getElementById("searchInput").addEventListener("keyup", filterTable);
            document.getElementById("statusFilter") && document.getElementById("statusFilter").addEventListener("change", filterTable);
        }
    </script>

</asp:Content>