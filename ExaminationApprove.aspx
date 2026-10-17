<%@ Page Title="تایید درخواست‌های معاینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExaminationApprove.aspx.vb" Inherits="ExaminationApprove" %>

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
        
        .exm-unit-status-box {
            background: linear-gradient(135deg, #e8f4fd 0%, #d1ecf1 100%);
            border: 2px solid #3498db;
            border-radius: 12px;
            padding: 20px 25px;
            margin-bottom: 20px;
        }
        .exm-unit-status-box .status-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 15px;
            padding-bottom: 12px;
            border-bottom: 2px dashed #3498db;
        }
        .exm-unit-status-box .status-title {
            font-size: 16px;
            font-weight: 700;
            color: #0c5460;
        }
        .exm-unit-status-box .status-title .unit-name {
            color: #e74c3c;
            font-size: 18px;
        }
        .exm-unit-status-box .percent-badge {
            background: #27ae60;
            color: #fff;
            padding: 8px 20px;
            border-radius: 20px;
            font-size: 18px;
            font-weight: 800;
            box-shadow: 0 4px 10px rgba(39,174,96,0.3);
            display: inline-block;
        }
        .exm-unit-status-box .percent-badge.low {
            background: #e74c3c;
            box-shadow: 0 4px 10px rgba(231,76,60,0.3);
        }
        .exm-unit-status-box .percent-badge.medium {
            background: #f39c12;
            box-shadow: 0 4px 10px rgba(243,156,18,0.3);
        }

        .exm-status-stats {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
            gap: 12px;
            margin-bottom: 15px;
        }
        .exm-stat-item {
            background: #ffffff;
            border-radius: 10px;
            padding: 12px 15px;
            text-align: center;
            border-right: 4px solid #3498db;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        .exm-stat-item .stat-number {
            font-size: 22px;
            font-weight: 800;
            color: #2c3e50;
        }
        .exm-stat-item .stat-label {
            font-size: 12px;
            color: #6c757d;
            margin-top: 3px;
        }
        .exm-stat-item.reserved { border-right-color: #27ae60; }
        .exm-stat-item.not-reserved { border-right-color: #e74c3c; }
        .exm-stat-item.total { border-right-color: #3498db; }

        .exm-progress-bar {
            width: 100%;
            height: 14px;
            background: #e9ecef;
            border-radius: 7px;
            overflow: hidden;
            margin-bottom: 15px;
            direction: ltr;
        }
        .exm-progress-bar-fill {
            height: 100%;
            background: linear-gradient(90deg, #27ae60, #2ecc71);
            border-radius: 7px;
            transition: width 0.6s ease;
            width: 0%;
        }

        .exm-not-reserved-list {
            background: #fff5f5;
            border: 1px dashed #e74c3c;
            border-radius: 10px;
            padding: 12px 15px;
            margin-top: 10px;
        }
        .exm-not-reserved-list .list-title {
            font-size: 13px;
            font-weight: 700;
            color: #721c24;
            margin-bottom: 8px;
        }
        .exm-not-reserved-list .person-chip {
            display: inline-block;
            background: #ffffff;
            border: 1px solid #f5c6cb;
            color: #721c24;
            padding: 4px 10px;
            border-radius: 12px;
            font-size: 12px;
            margin: 3px;
            font-weight: 600;
        }
        .exm-not-reserved-list .person-chip .code {
            color: #adb5bd;
            font-size: 11px;
            margin-right: 4px;
        }
        .exm-not-reserved-empty {
            background: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
            border-radius: 8px;
            padding: 10px 15px;
            font-size: 13px;
            text-align: center;
            font-weight: 600;
            display: block;
        }
        
        .exm-toolbar {
            background: #e8f4fd;
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 15px;
            flex-wrap: wrap;
            border-right: 4px solid #3498db;
        }
        .exm-toolbar label {
            font-weight: 600;
            color: #2c3e50;
            font-size: 14px;
        }
        .exm-toolbar .exm-btn {
            padding: 8px 25px;
            border: none;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            margin: 0;
            font-family: inherit;
        }
        .exm-toolbar .exm-btn:hover { transform: scale(1.05); }
        .exm-btn-approve-all { background: #27ae60; color: #ffffff; }
        .exm-btn-approve-all:hover { background: #219a52; }
        .exm-btn-reject-all { background: #e74c3c; color: #ffffff; }
        .exm-btn-reject-all:hover { background: #c0392b; }
        .exm-btn-select-all { background: #3498db; color: #ffffff; }
        .exm-btn-select-all:hover { background: #2980b9; }
        .exm-btn-clear-all { background: #95a5a6; color: #ffffff; }
        .exm-btn-clear-all:hover { background: #7f8c8d; }
        .exm-btn-excel { background: #217346; color: #ffffff; }
        .exm-btn-excel:hover { background: #1a5c38; }
        
        .exm-toolbar .exm-comment-group {
            flex: 1;
            min-width: 200px;
        }
        .exm-toolbar .exm-comment-group input {
            width: 100%;
            padding: 8px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 6px;
            font-size: 13px;
            font-family: inherit;
        }
        
        .exm-selected-count {
            background: #3498db;
            color: white;
            padding: 5px 15px;
            border-radius: 15px;
            font-size: 13px;
            font-weight: 600;
            white-space: nowrap;
        }
        
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
        .exm-table .exm-status-pending { 
            background: #fff3cd; 
            color: #856404; 
            padding: 3px 12px; 
            border-radius: 12px;
            display: inline-block;
            font-weight: 600;
        }
        .exm-table .exm-status-approved { 
            background: #d4edda; 
            color: #155724; 
            padding: 3px 12px; 
            border-radius: 12px;
            display: inline-block;
            font-weight: 600;
        }
        .exm-table .exm-status-rejected { 
            background: #f8d7da; 
            color: #721c24; 
            padding: 3px 12px; 
            border-radius: 12px;
            display: inline-block;
            font-weight: 600;
        }
        .exm-table .exm-status-done { 
            background: #cce5ff; 
            color: #004085; 
            padding: 3px 12px; 
            border-radius: 12px;
            display: inline-block;
            font-weight: 600;
        }
        
        .exm-checkbox {
            width: 18px;
            height: 18px;
            cursor: pointer;
        }
        
        .exm-btn {
            padding: 6px 18px;
            border: none;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            margin: 2px;
            font-family: inherit;
        }
        .exm-btn:hover { transform: scale(1.05); }
        .exm-btn-approve { background: #27ae60; color: #ffffff; }
        .exm-btn-approve:hover { background: #219a52; }
        .exm-btn-reject { background: #e74c3c; color: #ffffff; }
        .exm-btn-reject:hover { background: #c0392b; }
        .exm-btn-back { 
            background: #9b59b6; 
            color: #ffffff; 
            padding: 10px 30px;
            font-size: 14px;
        }
        .exm-btn-back:hover { background: #8e44ad; }
        
        .exm-comment {
            width: 180px;
            padding: 6px 10px;
            border: 1.5px solid #e0e0e0;
            border-radius: 6px;
            font-size: 12px;
            font-family: inherit;
            min-height: 32px;
        }
        .exm-comment:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        
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
        
        .exm-actions {
            display: flex;
            gap: 5px;
            flex-wrap: wrap;
            align-items: center;
        }
        
        @media (max-width: 768px) {
            .exm-container { padding: 15px; margin: 10px; }
            .exm-table { font-size: 11px; }
            .exm-table th, .exm-table td { padding: 5px 8px; }
            .exm-comment { width: 100%; }
            .exm-actions { flex-direction: column; }
            .exm-btn { width: 100%; text-align: center; }
            .exm-toolbar { flex-direction: column; align-items: stretch; }
            .exm-selected-count { text-align: center; }
            .exm-status-stats { grid-template-columns: 1fr; }
        }
    </style>
    
    <script type="text/javascript">
        function updateSelectedCount() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            var count = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    count++;
                }
            }
            var countElement = document.getElementById('selectedCount');
            if (countElement) {
                countElement.innerHTML = 'تعداد انتخاب شده: ' + count + ' نفر';
            }
        }

        function selectAllCheckboxes() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = true;
                }
            }
            updateSelectedCount();
        }

        function clearAllCheckboxes() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = false;
            }
            updateSelectedCount();
        }

        function toggleAllCheckboxes(source) {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = source.checked;
                }
            }
            updateSelectedCount();
        }

        function confirmBulkApprove() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox input[type="checkbox"]:checked');
            if (checkboxes.length === 0) {
                alert('لطفاً حداقل یک درخواست را انتخاب کنید.');
                return false;
            }
            return confirm('آیا از تایید ' + checkboxes.length + ' درخواست انتخاب شده مطمئن هستید؟');
        }

        function confirmBulkReject() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox input[type="checkbox"]:checked');
            if (checkboxes.length === 0) {
                alert('لطفاً حداقل یک درخواست را انتخاب کنید.');
                return false;
            }
            return confirm('آیا از رد ' + checkboxes.length + ' درخواست انتخاب شده مطمئن هستید؟');
        }

        function exportToExcel() {
            document.getElementById('<%= btnExportExcel.ClientID %>').click();
        }

        window.onload = function () {
            updateSelectedCount();
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
      <div class="exm-title">
            <%= If(Request.QueryString("role") = "Manager", "✅ تایید درخواست‌های معاینه - رییس واحد", "✅ تایید درخواست‌های معاینه - سرپرست") %>
        </div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='exm-alert exm-alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- باکس وضعیت تکمیل رزرو واحد -->
        <asp:Panel ID="pnlUnitStatus" runat="server" Visible="false" CssClass="exm-unit-status-box">
            
            <div class="status-header">
                <div class="status-title">
                    📊 وضعیت تکمیل رزرو واحد: 
                    <span class="unit-name"><asp:Label ID="lblUnitName" runat="server" /></span>
                </div>
                <div>
                    <asp:Label ID="lblCompletionPercent" runat="server" CssClass="percent-badge" Text="0%" />
                </div>
            </div>
            
            <div class="exm-status-stats">
                <div class="exm-stat-item total">
                    <div class="stat-number"><asp:Label ID="lblTotalPersonnel" runat="server" Text="0" /></div>
                    <div class="stat-label">👥 کل پرسنل واحد</div>
                </div>
                <div class="exm-stat-item reserved">
                    <div class="stat-number"><asp:Label ID="lblReservedCount" runat="server" Text="0" /></div>
                    <div class="stat-label">✅ رزرو کرده</div>
                </div>
                <div class="exm-stat-item not-reserved">
                    <div class="stat-number"><asp:Label ID="lblNotReservedCount" runat="server" Text="0" /></div>
                    <div class="stat-label">⏳ رزرو نکرده</div>
                </div>
            </div>
            
            <div class="exm-progress-bar">
                <div id="progressBarFill" runat="server" class="exm-progress-bar-fill"></div>
            </div>
            
            <asp:Panel ID="pnlNotReservedList" runat="server" Visible="false" CssClass="exm-not-reserved-list">
                <div class="list-title">⚠️ پرسنل زیر هنوز رزرو نکرده‌اند:</div>
                <asp:Repeater ID="rptNotReserved" runat="server">
                    <ItemTemplate>
                        <span class="person-chip">
                            <span class="code"><%# Eval("codePersoneli") %></span>
                            <%# Eval("FullName") %>
                        </span>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            
            <asp:Label ID="lblNotReservedEmpty" runat="server" Visible="false" 
                CssClass="exm-not-reserved-empty" 
                Text="🎉 همه پرسنل واحد شما رزرو کرده‌اند!" />
            
        </asp:Panel>
        
        <!-- نوار ابزار تایید گروهی -->
        <div class="exm-toolbar">
            <label>عملیات گروهی:</label>
            
            <span id="selectedCount" class="exm-selected-count">تعداد انتخاب شده: 0 نفر</span>
            
            <div class="exm-comment-group">
                <asp:TextBox ID="txtBulkComment" runat="server" 
                    placeholder="توضیحات گروهی (اختیاری)..." 
                    MaxLength="500" />
            </div>
            
            <asp:Button ID="btnBulkApprove" runat="server" Text="✅ تایید گروهی" 
                CssClass="exm-btn exm-btn-approve-all" 
                OnClick="btnBulkApprove_Click" 
                OnClientClick="return confirmBulkApprove();" />
            
            <asp:Button ID="btnBulkReject" runat="server" Text="❌ رد گروهی" 
                CssClass="exm-btn exm-btn-reject-all" 
                OnClick="btnBulkReject_Click" 
                OnClientClick="return confirmBulkReject();" />
            
            <input type="button" value="☑️ انتخاب همه" 
                class="exm-btn exm-btn-select-all" 
                onclick="selectAllCheckboxes();" />
            
            <input type="button" value="☐ لغو انتخاب" 
                class="exm-btn exm-btn-clear-all" 
                onclick="clearAllCheckboxes();" />

            <!-- دکمه خروجی Excel -->
            <asp:Button ID="btnExportExcel" runat="server" Text="📊 خروجی Excel" 
                CssClass="exm-btn exm-btn-excel" 
                OnClick="btnExportExcel_Click" />
        </div>
        
        <!-- جدول درخواست‌ها -->
        <div class="exm-table-responsive">
            <asp:GridView ID="gvRequests" runat="server" 
                AutoGenerateColumns="False" 
                CssClass="exm-table" 
                AllowPaging="True" 
                PageSize="15"
                OnPageIndexChanging="gvRequests_PageIndexChanging"
                OnRowDataBound="gvRequests_RowDataBound"
                OnRowCommand="gvRequests_RowCommand"
                EmptyDataText="هیچ درخواستی برای تایید وجود ندارد"
                DataKeyNames="ID">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="40">
                        <HeaderTemplate>
                            <input type="checkbox" id="chkSelectAllHeader" 
                                onclick="toggleAllCheckboxes(this);" 
                                style="width:18px;height:18px;cursor:pointer;" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" 
                                CssClass="exm-row-checkbox" 
                                Enabled='<%# Convert.ToInt32(Eval("StatusCode")) = 1 %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                    <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" ItemStyle-Width="80" />
                    <asp:BoundField DataField="FullName" HeaderText="پرسنل" ItemStyle-Width="120" />
                    <asp:BoundField DataField="UnitName" HeaderText="واحد" ItemStyle-Width="120" />
                    
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                        <ItemTemplate>
                            <span class="exm-status-<%# Eval("StatusClass") %>">
                                <%# Eval("StatusText") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="300">
                        <ItemTemplate>
                            <div class="exm-actions">
                                <asp:TextBox ID="txtComment" runat="server" CssClass="exm-comment" 
                                    placeholder="توضیحات..." TextMode="MultiLine" Rows="2" />
                                <asp:Button ID="btnApprove" runat="server" Text="✅ تایید" 
                                    CssClass="exm-btn exm-btn-approve" CommandName="Approve" />
                                <asp:Button ID="btnReject" runat="server" Text="❌ رد" 
                                    CssClass="exm-btn exm-btn-reject" CommandName="Reject" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="exm-pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        
        <div style="margin-top: 20px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت به صفحه اصلی" 
                CssClass="exm-btn exm-btn-back" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>