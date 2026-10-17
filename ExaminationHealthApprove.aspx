<%@ Page Title="تایید درخواست‌های معاینه - کارشناس بهداشت"
    Language="VB"
    MasterPageFile="~/dabir.master"
    AutoEventWireup="false"
    CodeFile="ExaminationHealthApprove.aspx.vb"
    Inherits="ExaminationHealthApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .exm-container {
            max-width: 1400px;
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
        .exm-btn-cancel-all { background: #e67e22; color: #ffffff; }
        .exm-btn-cancel-all:hover { background: #d35400; }
        .exm-btn-select-all { background: #3498db; color: #ffffff; }
        .exm-btn-select-all:hover { background: #2980b9; }
        .exm-btn-clear-all { background: #95a5a6; color: #ffffff; }
        .exm-btn-clear-all:hover { background: #7f8c8d; }
        
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
        .exm-toolbar .exm-comment-group input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
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

        .exm-table tr:hover {
            background: #f8f9fa;
        }

        .exm-status-pending {
            background: #fff3cd;
            color: #856404;
            padding: 4px 12px;
            border-radius: 12px;
            display: inline-block;
            font-weight: 600;
        }
        
        .exm-national-code {
            font-family: 'Courier New', monospace;
            font-weight: 600;
            letter-spacing: 2px;
            color: #2c3e50;
            direction: ltr;
            text-align: center;
            font-size: 14px;
        }
        
        .exm-supervisor-comment {
            background: #e8f4fd;
            color: #0c5460;
            padding: 8px 12px;
            border-radius: 8px;
            font-size: 12px;
            line-height: 1.6;
            max-width: 200px;
            word-wrap: break-word;
            border-right: 3px solid #3498db;
            text-align: right;
        }
        .exm-supervisor-comment.empty {
            background: #f8f9fa;
            color: #adb5bd;
            border-right-color: #dee2e6;
            font-style: italic;
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
            margin: 2px;
            transition: all 0.3s;
            font-family: inherit;
        }
        .exm-btn:hover { transform: scale(1.05); }

        .exm-btn-approve {
            background: #27ae60;
            color: white;
        }
        .exm-btn-approve:hover { background: #219a52; }

        .exm-btn-reject {
            background: #e74c3c;
            color: white;
        }
        .exm-btn-reject:hover { background: #c0392b; }

        .exm-btn-cancel {
            background: #e67e22;
            color: white;
        }
        .exm-btn-cancel:hover { background: #d35400; }

        .exm-btn-back {
            background: #9b59b6;
            color: white;
            padding: 10px 30px;
            font-size: 14px;
        }
        .exm-btn-back:hover { background: #8e44ad; }

        .exm-comment {
            width: 180px;
            padding: 6px 10px;
            border: 1px solid #ddd;
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

        .exm-actions {
            display: flex;
            gap: 5px;
            flex-wrap: wrap;
            align-items: center;
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

        .exm-alert-success {
            background: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .exm-alert-danger {
            background: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }

        .exm-alert-warning {
            background: #fff3cd;
            color: #856404;
            border: 1px solid #ffc107;
        }

        .exm-alert-info {
            background: #d1ecf1;
            color: #0c5460;
            border: 1px solid #bee5eb;
        }
        
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
        
        .exm-tabs {
            display: flex;
            gap: 5px;
            margin-bottom: 20px;
            border-bottom: 2px solid #e9ecef;
            flex-wrap: wrap;
        }
        .exm-tab {
            padding: 12px 25px;
            background: #f8f9fa;
            border: none;
            border-radius: 8px 8px 0 0;
            cursor: pointer;
            font-size: 14px;
            font-weight: 600;
            color: #6c757d;
            transition: all 0.3s;
            font-family: inherit;
        }
        .exm-tab:hover {
            background: #e9ecef;
            color: #495057;
        }
        .exm-tab.active {
            background: #3498db;
            color: white;
            box-shadow: 0 -2px 8px rgba(52,152,219,0.3);
        }
        .exm-tab .badge-count {
            display: inline-block;
            background: #ffffff;
            color: #3498db;
            padding: 2px 10px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 700;
            margin-right: 5px;
        }
        .exm-tab.active .badge-count {
            background: rgba(255,255,255,0.3);
            color: white;
        }
        
        .exm-tab-content {
            display: none;
        }
        .exm-tab-content.active {
            display: block;
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
            .exm-tabs { flex-direction: column; }
            .exm-tab { border-radius: 8px; text-align: center; }
        }
    </style>
    
    <script type="text/javascript">
        function selectAllCheckboxes() {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = true;
                }
            }
            updateSelectedCount();
        }

        function clearAllCheckboxes() {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = false;
            }
            updateSelectedCount();
        }

        function updateSelectedCount() {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
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

        function toggleAllCheckboxes(source) {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = source.checked;
                }
            }
            updateSelectedCount();
        }

        function confirmBulkApprove() {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return false;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]:checked');
            if (checkboxes.length === 0) {
                alert('لطفاً حداقل یک درخواست را انتخاب کنید.');
                return false;
            }
            return confirm('آیا از تایید ' + checkboxes.length + ' درخواست انتخاب شده مطمئن هستید؟');
        }

        function confirmBulkReject() {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return false;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]:checked');
            if (checkboxes.length === 0) {
                alert('لطفاً حداقل یک درخواست را انتخاب کنید.');
                return false;
            }
            return confirm('آیا از رد ' + checkboxes.length + ' درخواست انتخاب شده مطمئن هستید؟');
        }

        function confirmBulkCancel() {
            var activeTab = document.querySelector('.exm-tab-content.active');
            if (!activeTab) return false;
            var checkboxes = activeTab.querySelectorAll('.exm-row-checkbox input[type="checkbox"]:checked');
            if (checkboxes.length === 0) {
                alert('لطفاً حداقل یک درخواست را انتخاب کنید.');
                return false;
            }
            return confirm('⚠️ آیا از ابطال ' + checkboxes.length + ' درخواست مطمئن هستید؟\n\n' +
                           'در این حالت، پرسنل می‌تواند مجدداً رزرو کند.');
        }

        function switchTab(tabName) {
            var tabs = document.querySelectorAll('.exm-tab-content');
            for (var i = 0; i < tabs.length; i++) {
                tabs[i].classList.remove('active');
            }
            
            var tabButtons = document.querySelectorAll('.exm-tab');
            for (var i = 0; i < tabButtons.length; i++) {
                tabButtons[i].classList.remove('active');
            }
            
            var activeContent = document.getElementById('tab_' + tabName);
            if (activeContent) activeContent.classList.add('active');
            
            var activeBtn = document.getElementById('btn_tab_' + tabName);
            if (activeBtn) activeBtn.classList.add('active');
            
            updateSelectedCount();
            
            var hfActiveTab = document.getElementById('<%= hfActiveTab.ClientID %>');
            if (hfActiveTab) hfActiveTab.value = tabName;
        }

        window.onload = function () {
            updateSelectedCount();
            
            var hfActiveTab = document.getElementById('<%= hfActiveTab.ClientID %>');
            if (hfActiveTab && hfActiveTab.value) {
                switchTab(hfActiveTab.value);
            } else {
                switchTab('pending');
            }
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
        <div class="exm-title">
            🩺 تایید درخواست‌های معاینه - کارشناس بهداشت
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='exm-alert exm-alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>

        <asp:HiddenField ID="hfActiveTab" runat="server" Value="pending" />

        <div class="exm-tabs">
            <button type="button" id="btn_tab_pending" class="exm-tab active" 
                onclick="switchTab('pending');">
                ⏳ در انتظار تایید
                <span class="badge-count"><asp:Label ID="lblPendingCount" runat="server" Text="0" /></span>
            </button>
            <button type="button" id="btn_tab_approved" class="exm-tab" 
                onclick="switchTab('approved');">
                ✅ تایید شده
                <span class="badge-count"><asp:Label ID="lblApprovedCount" runat="server" Text="0" /></span>
            </button>
        </div>

        <!-- ============================================ -->
        <!-- تب 1: در انتظار تایید -->
        <!-- ============================================ -->
        <div id="tab_pending" class="exm-tab-content active">
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
            </div>

            <div class="exm-table-responsive">
                <asp:GridView ID="gvRequests" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="exm-table"
                    AllowPaging="True"
                    PageSize="15"
                    OnPageIndexChanging="gvRequests_PageIndexChanging"
                    OnRowDataBound="gvRequests_RowDataBound"
                    OnRowCommand="gvRequests_RowCommand"
                    EmptyDataText="هیچ درخواستی در انتظار تایید کارشناس بهداشت نیست"
                    DataKeyNames="ID">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="40">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkSelectAllHeader" 
                                    onclick="toggleAllCheckboxes(this);" 
                                    class="exm-checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" 
                                    CssClass="exm-row-checkbox" 
                                    Enabled='<%# Convert.ToInt32(Eval("StatusCode")) = 3 %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                        <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" ItemStyle-Width="80" />
                        <asp:BoundField DataField="FullName" HeaderText="پرسنل" ItemStyle-Width="120" />
                        <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" ItemStyle-CssClass="exm-national-code" ItemStyle-Width="100" />
                        <asp:BoundField DataField="UnitName" HeaderText="واحد" ItemStyle-Width="100" />
                        
                        <asp:TemplateField HeaderText="توضیحات سرپرست" ItemStyle-Width="180">
                            <ItemTemplate>
                                <div class='<%# GetSupervisorCommentClass(Eval("SupervisorComment")) %>'>
                                    <%# GetSupervisorCommentText(Eval("SupervisorComment")) %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                            <ItemTemplate>
                                <span class="exm-status-pending">
                                    <%# Eval("StatusText") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="320">
                            <ItemTemplate>
                                <div class="exm-actions">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="exm-comment"
                                        TextMode="MultiLine" Rows="2" ToolTip="توضیحات..." />
                                    <asp:Button ID="btnApprove" runat="server" Text="✅ تایید"
                                        CssClass="exm-btn exm-btn-approve" CommandName="Approve" />
                                    <asp:Button ID="btnReject" runat="server" Text="❌ رد"
                                        CssClass="exm-btn exm-btn-reject" CommandName="Reject" />
                                    <asp:Button ID="btnCancel" runat="server" Text="🚫 ابطال"
                                        CssClass="exm-btn exm-btn-cancel" CommandName="CancelRequest"
                                        OnClientClick="return confirm('⚠️ آیا از ابطال این درخواست مطمئن هستید؟\n\nدر این حالت، پرسنل می‌تواند مجدداً رزرو کند.');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="exm-pagination" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>

        <!-- ============================================ -->
        <!-- تب 2: تایید شده -->
        <!-- ============================================ -->
        <div id="tab_approved" class="exm-tab-content">
            <div class="exm-toolbar" style="background:#e8f8f0; border-right-color:#27ae60;">
                <label>عملیات روی درخواست‌های تایید شده:</label>
                
                <span id="selectedCountApproved" class="exm-selected-count" style="background:#27ae60;">
                    تعداد انتخاب شده: 0 نفر
                </span>
                
                <div class="exm-comment-group">
                    <asp:TextBox ID="txtBulkCancelReason" runat="server" 
                        placeholder="دلیل ابطال (اختیاری)..." 
                        MaxLength="500" />
                </div>
                
                <asp:Button ID="btnBulkCancel" runat="server" Text="🚫 ابطال گروهی" 
                    CssClass="exm-btn exm-btn-cancel-all" 
                    OnClick="btnBulkCancel_Click" 
                    OnClientClick="return confirmBulkCancel();" />
                
                <input type="button" value="☑️ انتخاب همه" 
                    class="exm-btn exm-btn-select-all" 
                    onclick="selectAllCheckboxesApproved();" />
                
                <input type="button" value="☐ لغو انتخاب" 
                    class="exm-btn exm-btn-clear-all" 
                    onclick="clearAllCheckboxesApproved();" />
            </div>

            <div class="exm-table-responsive">
                <asp:GridView ID="gvApproved" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="exm-table"
                    AllowPaging="True"
                    PageSize="15"
                    OnPageIndexChanging="gvApproved_PageIndexChanging"
                    OnRowCommand="gvApproved_RowCommand"
                    EmptyDataText="هیچ درخواست تایید شده‌ای وجود ندارد"
                    DataKeyNames="ID">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="40">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkSelectAllHeaderApproved" 
                                    onclick="toggleAllCheckboxesApproved(this);" 
                                    class="exm-checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectApproved" runat="server" 
                                    CssClass="exm-row-checkbox-approved" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                        <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" ItemStyle-Width="80" />
                        <asp:BoundField DataField="FullName" HeaderText="پرسنل" ItemStyle-Width="120" />
                        <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" ItemStyle-CssClass="exm-national-code" ItemStyle-Width="100" />
                        <asp:BoundField DataField="UnitName" HeaderText="واحد" ItemStyle-Width="100" />
                        
                        <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                            <ItemTemplate>
                                <span style="background:#d4edda; color:#155724; padding:4px 12px; border-radius:12px; font-weight:600; display:inline-block;">
                                    ✅ تایید نهایی
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="180">
                            <ItemTemplate>
                                <div class="exm-actions">
                                    <asp:Button ID="btnCancelApproved" runat="server" Text="🚫 ابطال"
                                        CssClass="exm-btn exm-btn-cancel" CommandName="CancelRequest"
                                        OnClientClick="return confirm('⚠️ آیا از ابطال این درخواست مطمئن هستید؟\n\nدر این حالت، پرسنل می‌تواند مجدداً رزرو کند.');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="exm-pagination" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>

        <div style="margin-top:20px;text-align:center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت به صفحه اصلی"
                CssClass="exm-btn exm-btn-back" OnClick="btnBack_Click" />
        </div>
    </div>
    
    <script type="text/javascript">
        function selectAllCheckboxesApproved() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox-approved input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = true;
                }
            }
            updateSelectedCountApproved();
        }

        function clearAllCheckboxesApproved() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox-approved input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = false;
            }
            updateSelectedCountApproved();
        }

        function toggleAllCheckboxesApproved(source) {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox-approved input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = source.checked;
                }
            }
            updateSelectedCountApproved();
        }

        function updateSelectedCountApproved() {
            var checkboxes = document.querySelectorAll('.exm-row-checkbox-approved input[type="checkbox"]');
            var count = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    count++;
                }
            }
            var countElement = document.getElementById('selectedCountApproved');
            if (countElement) {
                countElement.innerHTML = 'تعداد انتخاب شده: ' + count + ' نفر';
            }
        }
    </script>
</asp:Content>