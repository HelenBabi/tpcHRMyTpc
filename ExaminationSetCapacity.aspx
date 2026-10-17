<%@ Page Title="تعیین برنامه معاینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExaminationSetCapacity.aspx.vb" Inherits="ExaminationSetCapacity" %>

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
        
        .exm-form-group {
            margin-bottom: 18px;
        }
        .exm-form-group label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 6px;
            font-size: 14px;
        }
        .exm-form-group select, 
        .exm-form-group input[type="text"],
        .exm-form-group input[type="number"] {
            width: 100%;
            padding: 10px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 14px;
            font-family: inherit;
            transition: all 0.2s;
            background: #ffffff;
            box-sizing: border-box;
        }
        .exm-form-group select:focus, 
        .exm-form-group input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        .exm-form-group .hint {
            font-size: 12px;
            color: #6c757d;
            margin-top: 4px;
        }
        .exm-required { color: #e74c3c; }
        
        .exm-date-row {
            display: flex;
            gap: 10px;
            align-items: center;
        }
        .exm-date-row .exm-date-group {
            flex: 1;
        }
        .exm-date-row .exm-date-group select {
            width: 100%;
            padding: 10px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 14px;
            font-family: inherit;
            transition: all 0.2s;
            background: #ffffff;
            box-sizing: border-box;
            text-align: center;
        }
        .exm-date-row .exm-date-group select:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        .exm-date-row .exm-date-separator {
            font-size: 18px;
            font-weight: bold;
            color: #6c757d;
            padding: 0 5px;
        }
        
        .exm-search-box {
            display: flex;
            gap: 10px;
            margin-bottom: 10px;
            align-items: center;
        }
        .exm-search-box input[type="text"] {
            flex: 2;
            padding: 8px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 14px;
            font-family: inherit;
        }
        .exm-search-box input[type="text"]:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        .exm-search-box .exm-btn-search {
            background: #3498db;
            color: #ffffff;
            padding: 8px 20px;
            border: none;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s;
        }
        .exm-search-box .exm-btn-search:hover { background: #2980b9; }
        .exm-search-box .exm-btn-showall {
            background: #6c757d;
            color: #ffffff;
            padding: 8px 20px;
            border: none;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s;
        }
        .exm-search-box .exm-btn-showall:hover { background: #5a6268; }
        
        .exm-units-container {
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            padding: 10px;
            max-height: 400px;
            overflow-y: auto;
            background: #fafafa;
        }
        .exm-unit-item {
            display: flex;
            align-items: center;
            padding: 8px 12px;
            border-bottom: 1px solid #eee;
            transition: background 0.2s;
            gap: 10px;
        }
        .exm-unit-item:hover {
            background: #e8f4fd;
        }
        .exm-unit-item:last-child {
            border-bottom: none;
        }
        .exm-unit-item .exm-unit-name {
            flex: 2;
            font-size: 14px;
            color: #2c3e50;
            text-align: right;
            font-weight: 500;
        }
        .exm-unit-item .exm-unit-capacity {
            flex: 0.8;
            text-align: center;
        }
        .exm-unit-item .exm-unit-capacity input[type="number"] {
            width: 80px;
            padding: 6px 10px;
            border: 1.5px solid #e0e0e0;
            border-radius: 6px;
            font-size: 14px;
            text-align: center;
        }
        .exm-unit-item .exm-unit-capacity input[type="number"]:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        .exm-unit-item .exm-unit-deadline {
            flex: 1;
            text-align: center;
        }
        .exm-unit-item .exm-unit-deadline input[type="text"] {
            width: 100px;
            padding: 6px 8px;
            border: 1.5px solid #e0e0e0;
            border-radius: 6px;
            font-size: 13px;
            text-align: center;
            font-family: inherit;
            background: #f8f9fa;
            direction: ltr;
        }
        .exm-unit-item .exm-unit-deadline input[type="text"]:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        .exm-unit-item .hint-small {
            font-size: 10px;
            color: #6c757d;
            margin-top: 2px;
        }
        .exm-unit-item .exm-unit-status {
            flex: 0.6;
            font-size: 12px;
            font-weight: 600;
            text-align: center;
        }
        .exm-unit-status-active { 
            color: #27ae60; 
            background: #d4edda;
            padding: 2px 10px;
            border-radius: 12px;
        }
        .exm-unit-status-inactive { 
            color: #e74c3c; 
            background: #f8d7da;
            padding: 2px 10px;
            border-radius: 12px;
        }
        
        .exm-unit-details {
            flex: 0.5;
            text-align: center;
        }
        .exm-btn-details { 
            background: #17a2b8; 
            color: #ffffff; 
            padding: 4px 14px;
            border: none;
            border-radius: 15px;
            font-size: 12px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s;
        }
        .exm-btn-details:hover { 
            background: #138496;
            transform: scale(1.05);
        }
        
        .exm-filter-info {
            margin-top: 5px;
            font-size: 12px;
            color: #6c757d;
        }
        
        .exm-btn-group {
            display: flex;
            gap: 10px;
            margin-top: 20px;
            flex-wrap: wrap;
        }
        .exm-btn {
            padding: 10px 30px;
            border: none;
            border-radius: 25px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            flex: 1;
            min-width: 120px;
            text-align: center;
        }
        .exm-btn:hover { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .exm-btn-submit { background: #27ae60; color: #ffffff; }
        .exm-btn-submit:hover { background: #219a52; }
        .exm-btn-submit:disabled { opacity: 0.6; cursor: not-allowed; transform: none; }
        .exm-btn-back { background: #9b59b6; color: #ffffff; }
        .exm-btn-back:hover { background: #8e44ad; }
        .exm-btn-load-existing { background: #9b59b6; color: #ffffff; }
        .exm-btn-load-existing:hover { background: #8e44ad; }
        .exm-btn-manage-units { background: #17a2b8; color: #ffffff; }
        .exm-btn-manage-units:hover { background: #138496; }
        
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
        .exm-alert .icon { font-size: 20px; }
        
        .exm-table-responsive { overflow-x: auto; margin-top: 20px; }
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
        
        .exm-status-active { color: #27ae60; font-weight: 600; }
        .exm-status-inactive { color: #e74c3c; font-weight: 600; }
        
        .exm-btn-sm {
            padding: 4px 14px;
            border: none;
            border-radius: 15px;
            font-size: 12px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s;
        }
        .exm-btn-sm:hover { transform: scale(1.05); }
        .exm-btn-edit { background: #f39c12; color: #ffffff; }
        .exm-btn-edit:hover { background: #e67e22; }
        .exm-btn-delete { background: #e74c3c; color: #ffffff; }
        .exm-btn-delete:hover { background: #c0392b; }
        
        .exm-existing-info {
            background: #e8f4fd;
            padding: 12px 15px;
            border-radius: 8px;
            border-right: 4px solid #3498db;
            margin: 10px 0;
        }
        .exm-existing-info .exm-unit-cap-item {
            display: inline-block;
            background: #ffffff;
            padding: 3px 12px;
            border-radius: 15px;
            margin: 3px 5px;
            font-size: 13px;
            border: 1px solid #ddd;
        }
        
        .exm-total-capacity {
            background: #e8f4fd;
            padding: 8px 15px;
            border-radius: 8px;
            font-weight: 600;
            color: #2c3e50;
            margin-top: 10px;
            text-align: center;
        }
        
        .exm-pagination {
            padding: 10px 0;
            text-align: center;
        }
        
        @media (max-width: 600px) {
            .exm-container { padding: 15px; margin: 10px; }
            .exm-btn { flex: 1 1 100%; }
            .exm-date-row { flex-direction: column; }
            .exm-date-row .exm-date-group { width: 100%; }
            .exm-unit-item { flex-wrap: wrap; }
            .exm-unit-item .exm-unit-name { flex: 1 1 100%; }
            .exm-search-box { flex-wrap: wrap; }
            .exm-search-box input[type="text"] { flex: 1 1 100%; }
        }
    </style>
    
    <script type="text/javascript">
        // تابع محاسبه جمع کل
        function calculateTotal() {
            var total = 0;
            var items = document.querySelectorAll('.exm-unit-item');

            items.forEach(function (item) {
                var input = item.querySelector('input[type="number"]');
                if (input) {
                    var val = parseInt(input.value) || 0;
                    total += val;

                    var statusSpan = item.querySelector('.exm-unit-status');
                    if (statusSpan) {
                        if (val > 0) {
                            statusSpan.className = 'exm-unit-status exm-unit-status-active';
                            statusSpan.textContent = '✓ فعال';
                        } else {
                            statusSpan.className = 'exm-unit-status exm-unit-status-inactive';
                            statusSpan.textContent = '✗ غیرفعال';
                        }
                    }
                }
            });

            var totalSpan = document.getElementById('lblTotalCapacity');
            if (totalSpan) {
                totalSpan.textContent = total;
            }
            return total;
        }

        // تابع اعتبارسنجی قبل از ارسال
        function validateForm() {
            var inputs = document.querySelectorAll('#unitsContainer input[type="number"]');
            var hasValue = false;

            inputs.forEach(function (input) {
                var val = parseInt(input.value) || 0;
                if (val > 0) {
                    hasValue = true;
                }
            });

            if (!hasValue) {
                alert('لطفاً برای حداقل یک واحد ظرفیت بیشتر از صفر وارد کنید.');
                return false;
            }
            return true;
        }

        // بارگذاری اولیه
        document.addEventListener('DOMContentLoaded', function () {
            var inputs = document.querySelectorAll('#unitsContainer input[type="number"]');

            inputs.forEach(function (input) {
                input.addEventListener('input', calculateTotal);
                input.addEventListener('change', calculateTotal);
                input.addEventListener('click', function () {
                    this.select();
                });
            });

            calculateTotal();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
        <div class="exm-title">📅 تعیین برنامه معاینه دوره‌ای</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='exm-alert exm-alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- ============================================================ -->
        <!-- فرم ثبت ظرفیت -->
        <!-- ============================================================ -->
        <div class="exm-form-group">
            <label>📅 تاریخ معاینه (شمسی) <span class="exm-required">*</span></label>
            <div class="exm-date-row">
                <div class="exm-date-group">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" />
                </div>
                <span class="exm-date-separator">/</span>
                <div class="exm-date-group">
                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" />
                </div>
                <span class="exm-date-separator">/</span>
                <div class="exm-date-group">
                    <asp:DropDownList ID="ddlDay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged" />
                </div>
            </div>
            <div class="hint">سال، ماه و روز معاینه را انتخاب کنید (فقط روزهای شنبه، یکشنبه و دوشنبه)</div>
        </div>
        
        <!-- دکمه‌های عملیات -->
        <div style="display: flex; gap: 10px; flex-wrap: wrap; margin-top: 5px;">
            <asp:Button ID="btnLoadExisting" runat="server" Text="📂 بارگذاری ظرفیت موجود" 
                CssClass="exm-btn exm-btn-load-existing" OnClick="btnLoadExisting_Click" 
                Style="flex: 0 0 auto; padding: 8px 25px; border: none; border-radius: 20px; font-size: 13px; font-weight: 600; cursor: pointer; background: #9b59b6; color: #ffffff;" />
            <asp:Button ID="btnManageUnits" runat="server" Text="🏢 مدیریت واحدهای معاینات" 
                CssClass="exm-btn exm-btn-manage-units" OnClick="btnManageUnits_Click" 
                Style="flex: 0 0 auto; padding: 8px 25px; border: none; border-radius: 20px; font-size: 13px; font-weight: 600; cursor: pointer; background: #17a2b8; color: #ffffff;" />
        </div>
        
        <!-- نمایش ظرفیت موجود -->
        <asp:Panel ID="pnlExisting" runat="server" Visible="false" CssClass="exm-existing-info">
            <asp:Label ID="lblExistingInfo" runat="server" />
        </asp:Panel>
        
        <!-- ============================================================ -->
        <!-- تعیین ظرفیت هر واحد با جستجو -->
        <!-- ============================================================ -->
        <div class="exm-form-group">
            <label>🏢 تعیین ظرفیت هر واحد <span class="exm-required">*</span></label>
            
            <!-- جستجوی واحدها -->
            <div class="exm-search-box">
                <asp:TextBox ID="txtSearchCapacity" runat="server" placeholder="جستجوی واحد..." />
                <asp:Button ID="btnSearchCapacity" runat="server" Text="🔍 جستجو" CssClass="exm-btn-search" OnClick="btnSearchCapacity_Click" />
                <asp:Button ID="btnShowAllCapacity" runat="server" Text="📋 نمایش همه" CssClass="exm-btn-showall" OnClick="btnShowAllCapacity_Click" />
            </div>
            
            <div class="exm-units-container" id="unitsContainer">
                <asp:Repeater ID="rptOmurs" runat="server" OnItemDataBound="rptOmurs_ItemDataBound" OnItemCommand="rptOmurs_ItemCommand">
                    <ItemTemplate>
                        <div class="exm-unit-item">
                            <asp:HiddenField ID="hdnOmurID" runat="server" Value='<%# Eval("UnitID") %>' />
                            <asp:Label ID="lblOmurName" runat="server" Text='<%# Eval("UnitName") %>' CssClass="exm-unit-name" />
                            
                            <div class="exm-unit-capacity">
                                <asp:TextBox ID="txtCapacity" runat="server" TextMode="Number" 
                                    Text='0' Min="0" Max="100" Width="80" />
                            </div>
                            
                            <div class="exm-unit-deadline">
                                <asp:TextBox ID="txtDeadline" runat="server" CssClass="exm-deadline-input" 
                                    ReadOnly="true" BackColor="#f8f9fa" placeholder="---" />
                                <div class="hint-small">مهلت رزرو</div>
                            </div>
                            
                            <div class="exm-unit-status">
                                <asp:Label ID="lblStatus" runat="server" 
                                    Text="✗ غیرفعال"
                                    CssClass="exm-unit-status-inactive" />
                            </div>
                            
                            <div class="exm-unit-details">
                                <asp:Button ID="btnDetails" runat="server" Text="📋 جزئیات" 
                                    CssClass="exm-btn-sm exm-btn-details" 
                                    CommandName="Details" 
                                    CommandArgument='<%# Eval("UnitID") %>' />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            
            <!-- نمایش تعداد واحدهای نمایش داده شده -->
            <div class="exm-filter-info">
                <asp:Label ID="lblFilteredCount" runat="server" Text="نمایش همه واحدها" />
            </div>
            
            <div class="hint">ظرفیت هر واحد را وارد کنید. مهلت رزرو از جدول واحدها خوانده می‌شود.</div>
        </div>
        
        <div class="exm-total-capacity">
            جمع کل ظرفیت: <span id="lblTotalCapacity">0</span> نفر
        </div>
        
        <div class="exm-btn-group">
            <asp:Button ID="btnSubmit" runat="server" Text="✅ ثبت ظرفیت واحدها" CssClass="exm-btn exm-btn-submit" OnClick="btnSubmit_Click" OnClientClick="return validateForm();" />
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="exm-btn exm-btn-back" OnClick="btnBack_Click" />
        </div>
        
        <!-- ============================================================ -->
        <!-- جدول ظرفیت‌های تعیین شده -->
        <!-- ============================================================ -->
        <div class="exm-table-responsive">
            <h4 style="color:#2c3e50; margin:30px 0 10px 0;">📋 لیست ظرفیت‌های تعیین شده</h4>
            <asp:GridView ID="gvCapacities" runat="server" AutoGenerateColumns="False" 
                CssClass="exm-table" AllowPaging="True" PageSize="15"
                OnPageIndexChanging="gvCapacities_PageIndexChanging"
                OnRowCommand="gvCapacities_RowCommand"
                EmptyDataText="هیچ ظرفیتی تعیین نشده است"
                DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                    <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" />
                    <asp:BoundField DataField="DayName" HeaderText="روز هفته" />
                    <asp:BoundField DataField="UnitsInfo" HeaderText="واحدها و ظرفیت" />
                    <asp:BoundField DataField="TotalCapacity" HeaderText="جمع کل" />
                    <asp:TemplateField HeaderText="وضعیت">
                        <ItemTemplate>
                            <span class='<%# IIf(Convert.ToBoolean(Eval("IsActive")), "exm-status-active", "exm-status-inactive") %>'>
                                <%# IIf(Convert.ToBoolean(Eval("IsActive")), "✓ فعال", "✗ غیرفعال") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="عملیات">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="✏️ ویرایش" 
                                CssClass="exm-btn-sm exm-btn-edit" 
                                CommandName="EditRow" 
                                CommandArgument='<%# Eval("ID") %>' />
                            <asp:Button ID="btnToggle" runat="server" Text='<%# IIf(Convert.ToBoolean(Eval("IsActive")), "غیرفعال", "فعال") %>' 
                                CssClass="exm-btn-sm exm-btn-delete" 
                                CommandName="ToggleRow" 
                                CommandArgument='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="exm-pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
    
    <asp:HiddenField ID="hdnEditID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnUnitData" runat="server" Value="" />
</asp:Content>