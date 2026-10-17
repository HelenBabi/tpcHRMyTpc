<%@ Page Title="مدیریت مبالغ مقاطع تحصیلی" Language="VB" MasterPageFile="~/dabir.master" 
    AutoEventWireup="false" CodeFile="ChildAmountManagement.aspx.vb" Inherits="ChildAmountManagement" 
    EnableEventValidation="false" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .am-container { max-width: 1300px; margin: 25px auto; padding: 20px 25px; background: #fff; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; font-family: 'Segoe UI', Tahoma, sans-serif; }

        .am-header-bar { display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px; padding-bottom: 15px; border-bottom: 2px solid #e9ecef; flex-wrap: wrap; gap: 10px; }
        .am-title { font-size: 22px; font-weight: 700; color: #2c3e50; margin: 0; }
        .am-header-actions { display: flex; gap: 8px; flex-wrap: wrap; }
        .am-back-btn { padding: 10px 22px; background: #6c757d; color: #fff; border: none; border-radius: 8px; cursor: pointer; font-family: inherit; font-size: 13px; font-weight: 600; text-decoration: none; display: inline-flex; align-items: center; gap: 6px; }
        .am-back-btn:hover { background: #5a6268; color: #fff; text-decoration: none; }

        .am-tabs { display: flex; gap: 0; margin-bottom: 0; border-bottom: 2px solid #3498db; }
        .am-tab { padding: 12px 30px; background: #f8f9fa; border: 2px solid #e0e0e0; border-bottom: none; border-radius: 10px 10px 0 0; cursor: pointer; font-family: inherit; font-size: 14px; font-weight: 600; color: #6c757d; margin-bottom: -2px; transition: all 0.2s; display: inline-flex; align-items: center; gap: 8px; }
        .am-tab:hover { background: #e8f4fd; color: #2980b9; }
        .am-tab.active { background: #fff; color: #2c3e50; border-color: #3498db; border-bottom: 2px solid #fff; font-weight: 700; }
        .am-tab-badge { background: #3498db; color: #fff; padding: 2px 10px; border-radius: 12px; font-size: 11px; font-weight: 700; }
        .am-tab.active .am-tab-badge { background: #2c3e50; }
        .am-tab.archive-tab.active { border-color: #9b59b6; }
        .am-tab.archive-tab.active .am-tab-badge { background: #9b59b6; }
        .am-tab-content { display: none; padding-top: 20px; }
        .am-tab-content.active { display: block; }

        .am-version-info { background: linear-gradient(135deg, #e8f4fd, #d1ecf1); border: 2px solid #3498db; border-radius: 10px; padding: 15px 20px; margin-bottom: 20px; display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; gap: 12px; }
        .am-version-info .info-item { display: flex; flex-direction: column; gap: 4px; }
        .am-version-info .info-item .lbl { font-size: 11px; color: #0c5460; font-weight: 600; }
        .am-version-info .info-item .val { font-size: 15px; color: #2c3e50; font-weight: 700; }
        .am-version-info .version-badge { background: #3498db; color: #fff; padding: 5px 15px; border-radius: 20px; font-size: 13px; font-weight: 700; }

        .am-table-wrap { overflow-x: auto; border-radius: 10px; border: 1px solid #e0e0e0; background: #fff; }
        .am-table { width: 100%; border-collapse: collapse; font-size: 13px; }
        .am-table thead { background: #2c3e50; color: #fff; }
        .am-table thead th { padding: 12px 10px; text-align: right; font-weight: 600; font-size: 12px; white-space: nowrap; }
        .am-table tbody tr { border-bottom: 1px solid #e9ecef; transition: all 0.2s; }
        .am-table tbody tr:hover { background: #f8f9fa; }
        .am-table tbody tr.changed { background: #fff8e1 !important; border-right: 4px solid #f39c12; }
        .am-table tbody td { padding: 10px; vertical-align: middle; }
        .am-table .col-row { width: 60px; text-align: center; font-weight: 700; color: #3498db; }
        .am-table .col-level { min-width: 200px; font-weight: 600; color: #2c3e50; }
        .am-table .col-type { width: 100px; text-align: center; }
        .am-table .col-amount { width: 200px; }
        .am-table .col-actions { width: 120px; text-align: center; }

        .am-type-badge { padding: 3px 10px; border-radius: 12px; font-size: 11px; font-weight: 600; display: inline-block; }
        .am-type-student { background: #d1ecf1; color: #0c5460; }
        .am-type-university { background: #f3e5f5; color: #6a1b9a; }

        .am-amount-display { font-size: 14px; font-weight: 700; color: #27ae60; text-align: left; direction: ltr; display: inline-block; }
        .am-amount-empty { color: #bdc3c7; font-style: italic; }

        .am-btn { padding: 6px 14px; border: none; border-radius: 6px; font-size: 12px; font-weight: 600; cursor: pointer; font-family: inherit; text-decoration: none; display: inline-block; }
        .am-btn-edit { background: #3498db; color: #fff; }
        .am-btn-edit:hover { background: #2980b9; }

        .am-actions-bar { display: flex; gap: 10px; justify-content: center; margin-top: 25px; padding-top: 20px; border-top: 2px solid #e9ecef; flex-wrap: wrap; }
        .am-btn-primary { padding: 12px 40px; background: #27ae60; color: #fff; border: none; border-radius: 25px; font-size: 14px; font-weight: 600; cursor: pointer; font-family: inherit; transition: all 0.2s; min-width: 160px; }
        .am-btn-primary:hover:not(:disabled) { background: #229954; transform: translateY(-2px); box-shadow: 0 4px 12px rgba(39,174,96,0.3); }
        .am-btn-secondary { padding: 12px 40px; background: #95a5a6; color: #fff; border: none; border-radius: 25px; font-size: 14px; font-weight: 600; cursor: pointer; font-family: inherit; transition: all 0.2s; min-width: 160px; }
        .am-btn-secondary:hover { background: #7f8c8d; transform: translateY(-2px); }

        .am-pending-changes { background: linear-gradient(135deg, #fff3cd, #ffe8a1); border: 2px solid #f39c12; border-radius: 10px; padding: 12px 20px; margin-top: 15px; display: flex; align-items: center; gap: 12px; font-size: 13px; color: #856404; font-weight: 600; }
        .am-pending-changes .icon { font-size: 22px; }
        .am-pending-changes .count { background: #f39c12; color: #fff; padding: 2px 10px; border-radius: 12px; font-weight: 700; }

        .am-archive-panel { background: #f4ecf7; border-right: 4px solid #9b59b6; border-radius: 10px; padding: 15px 20px; margin-bottom: 15px; }
        .am-archive-header { display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; gap: 10px; margin-bottom: 12px; padding-bottom: 10px; border-bottom: 1px dashed #9b59b6; }
        .am-archive-title { font-size: 15px; font-weight: 700; color: #6c3483; }
        .am-archive-meta { font-size: 12px; color: #6c757d; }

        .am-empty { text-align: center; padding: 40px 20px; color: #6c757d; font-size: 14px; }
        .am-empty .icon { font-size: 50px; display: block; margin-bottom: 10px; }

        /* ============================================================
           ✅ Modal — روش جدید: بدون position:fixed
           ============================================================ */
        .am-modal-overlay {
            display: none;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            min-height: 100%;
            background: rgba(0, 0, 0, 0.8);
            z-index: 2147483647;
            padding: 50px 20px;
            box-sizing: border-box;
        }
        .am-modal-overlay.show {
            display: block;
        }
        .am-modal {
            background: #fff;
            border-radius: 14px;
            max-width: 520px;
            width: 100%;
            margin: 0 auto;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.4);
            direction: rtl;
            text-align: right;
            position: relative;
        }
        .am-modal-header { padding: 18px 25px; background: linear-gradient(135deg, #2c3e50, #34495e); color: #fff; border-radius: 14px 14px 0 0; }
        .am-modal-header h3 { margin: 0; font-size: 17px; font-weight: 700; }
        .am-modal-body { padding: 20px 25px; }
        .am-modal-footer { padding: 15px 25px 20px; border-top: 1px solid #e9ecef; background: #f8f9fa; border-radius: 0 0 14px 14px; display: flex; justify-content: flex-end; gap: 10px; flex-wrap: wrap; }

        .am-form-group { margin-bottom: 15px; }
        .am-form-group label { display: block; font-weight: 600; color: #2c3e50; margin-bottom: 6px; font-size: 13px; }
        .am-form-group label .required { color: #e74c3c; }
        .am-form-input { width: 100%; padding: 10px 12px; border: 1.5px solid #e0e0e0; border-radius: 8px; font-size: 13px; font-family: inherit; box-sizing: border-box; }
        .am-form-input:focus { border-color: #3498db; outline: none; box-shadow: 0 0 0 3px rgba(52,152,219,0.15); }
        .am-form-textarea { width: 100%; padding: 10px 12px; border: 1.5px solid #e0e0e0; border-radius: 8px; font-size: 13px; font-family: inherit; box-sizing: border-box; resize: vertical; min-height: 70px; }

        .am-modal-btn { padding: 10px 30px; border: none; border-radius: 8px; font-size: 13px; font-weight: 700; cursor: pointer; font-family: inherit; text-decoration: none; display: inline-block; }
        .am-modal-btn-save { background: #27ae60; color: #fff !important; }
        .am-modal-btn-save:hover { background: #229954; color: #fff !important; text-decoration: none; }
        .am-modal-btn-cancel { background: #95a5a6; color: #fff !important; }
        .am-modal-btn-cancel:hover { background: #7f8c8d; color: #fff !important; text-decoration: none; }

        .am-toast-container { position: fixed; top: 80px; left: 50%; transform: translateX(-50%); z-index: 2147483647; direction: rtl; }
        .am-toast { background: #fff; border-radius: 12px; padding: 15px 25px; margin-bottom: 10px; box-shadow: 0 8px 30px rgba(0,0,0,0.15); display: flex; align-items: center; gap: 12px; min-width: 300px; max-width: 500px; border-right: 5px solid; }
        .am-toast.success { border-right-color: #27ae60; }
        .am-toast.error { border-right-color: #e74c3c; }
        .am-toast.warning { border-right-color: #f39c12; }
        .am-toast.info { border-right-color: #3498db; }
        .am-toast-icon { font-size: 24px; }
        .am-toast-message { font-size: 13px; color: #2c3e50; font-weight: 600; }

        .am-alert-overlay {
            display: none;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            min-height: 100%;
            background: rgba(0, 0, 0, 0.7);
            z-index: 2147483647;
            padding: 50px 20px;
            box-sizing: border-box;
        }
        .am-alert-overlay.show { display: block; }
        .am-alert { background: #fff; border-radius: 14px; max-width: 450px; width: 100%; margin: 0 auto; text-align: center; overflow: hidden; box-shadow: 0 20px 60px rgba(0,0,0,0.3); }
        .am-alert-header { padding: 22px; color: #fff; }
        .am-alert-header.success { background: linear-gradient(135deg, #27ae60, #2ecc71); }
        .am-alert-header.error { background: linear-gradient(135deg, #e74c3c, #c0392b); }
        .am-alert-header.warning { background: linear-gradient(135deg, #f39c12, #e67e22); }
        .am-alert-header.info { background: linear-gradient(135deg, #3498db, #2980b9); }
        .am-alert-icon { font-size: 50px; display: block; margin-bottom: 8px; }
        .am-alert-title { font-size: 18px; font-weight: 700; margin: 0; }
        .am-alert-body { padding: 20px 25px; color: #2c3e50; font-size: 14px; line-height: 1.8; }
        .am-alert-footer { padding: 0 25px 22px; }
        .am-alert-btn { padding: 10px 35px; border: none; border-radius: 25px; font-size: 14px; font-weight: 600; cursor: pointer; font-family: inherit; color: #fff; background: #3498db; }

        @media (max-width: 768px) {
            .am-container { padding: 15px; margin: 10px; }
            .am-actions-bar { flex-direction: column; }
            .am-btn-primary, .am-btn-secondary { width: 100%; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>

            <div class="am-container">
                <div class="am-header-bar">
                    <h2 class="am-title">💰 مدیریت مبالغ مقاطع تحصیلی — واحد روابط عمومی</h2>
                    <div class="am-header-actions">
                        <asp:LinkButton ID="lnkBackToHome" runat="server" CssClass="am-back-btn" 
                            OnClick="lnkBackToHome_Click" CausesValidation="false">🏠 بازگشت</asp:LinkButton>
                    </div>
                </div>

                <div class="am-tabs">
                    <button type="button" id="btn_tab_active" class="am-tab active" onclick="switchTab('active');">
                        📋 مبالغ فعال
                    </button>
                    <button type="button" id="btn_tab_archive" class="am-tab archive-tab" onclick="switchTab('archive');">
                        📦 آرشیو نسخه‌ها
                        <span class="am-tab-badge"><asp:Label ID="lblArchiveCount" runat="server" Text="0" /></span>
                    </button>
                </div>

                <asp:HiddenField ID="hfActiveTab" runat="server" Value="active" />
                <asp:HiddenField ID="hfPendingChanges" runat="server" Value="" />

                <%-- تب ۱ --%>
                <div id="tab_active" class="am-tab-content active">

                    <div class="am-version-info">
                        <div class="info-item">
                            <span class="lbl">📅 سال تحصیلی</span>
                            <span class="val"><asp:Label ID="lblCurrentYear" runat="server" Text="-" /></span>
                        </div>
                        <div class="info-item">
                            <span class="lbl">🔢 نسخه فعلی</span>
                            <span class="version-badge">نسخه <asp:Label ID="lblCurrentVersion" runat="server" Text="-" /></span>
                        </div>
                        <div class="info-item">
                            <span class="lbl">👤 آخرین ویرایش‌کننده</span>
                            <span class="val"><asp:Label ID="lblLastEditor" runat="server" Text="-" /></span>
                        </div>
                        <div class="info-item">
                            <span class="lbl">🕐 تاریخ آخرین ویرایش</span>
                            <span class="val"><asp:Label ID="lblLastEditDate" runat="server" Text="-" /></span>
                        </div>
                    </div>

                    <asp:Panel ID="pnlAmounts" runat="server" Visible="false">
                        <div class="am-table-wrap">
                            <asp:Repeater ID="rptAmounts" runat="server" OnItemCommand="rptAmounts_ItemCommand">
                                <HeaderTemplate>
                                    <table class="am-table">
                                        <thead>
                                            <tr>
                                                <th class="col-row">#</th>
                                                <th class="col-level">مقطع تحصیلی</th>
                                                <th class="col-type">نوع</th>
                                                <th class="col-amount">مبلغ (ریال)</th>
                                                <th class="col-amount">مبلغ به حروف</th>
                                                <th class="col-actions">عملیات</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id='row_<%# Eval("ID") %>' class='<%# If(Convert.ToBoolean(Eval("HasPendingChange")), "changed", "") %>'>
                                        <td class="col-row"><%# Eval("RowNumber") %></td>
                                        <td class="col-level"><%# Eval("EducationLevel") %></td>
                                        <td class="col-type">
                                            <span class='am-type-badge <%# If(Convert.ToInt32(Eval("RowNumber")) <= 12, "am-type-student", "am-type-university") %>'>
                                                <%# If(Convert.ToInt32(Eval("RowNumber")) <= 12, "دانش‌آموز", "دانشجو") %>
                                            </span>
                                        </td>
                                        <td class="col-amount">
                                            <asp:Label ID="lblAmountDisplay" runat="server" CssClass="am-amount-display" 
                                                Text='<%# GetAmountDisplay(Eval("DisplayAmount")) %>' />
                                        </td>
                                        <td class="col-amount" style="font-size:11px;color:#6c757d;">
                                            <%# GetAmountWords(Eval("DisplayAmount")) %>
                                        </td>
                                        <td class="col-actions">
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="am-btn am-btn-edit"
                                                CommandName="EditAmount" CommandArgument='<%# Eval("ID") %>' 
                                                CausesValidation="false">✏️ ویرایش</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></tbody></table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlNoAmounts" runat="server" Visible="false">
                        <div class="am-empty">
                            <span class="icon">📭</span>
                            هیچ مقطعی در سیستم تعریف نشده است.
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlPendingChanges" runat="server" Visible="false" CssClass="am-pending-changes">
                        <span class="icon">⚠️</span>
                        <span>تغییرات ذخیره‌نشده: <span class="count"><asp:Label ID="lblPendingCount" runat="server" Text="0" /></span> مورد</span>
                    </asp:Panel>

                    <div class="am-actions-bar">
                        <asp:Button ID="btnSaveAll" runat="server" Text="💾 ذخیره همه تغییرات" 
                            CssClass="am-btn-primary" OnClick="btnSaveAll_Click" 
                            OnClientClick="return confirmSaveAll();" />
                        <asp:Button ID="btnRefresh" runat="server" Text="🔄 بازنشانی" 
                            CssClass="am-btn-secondary" OnClick="btnRefresh_Click" CausesValidation="false" />
                    </div>
                </div>

                <%-- تب ۲ --%>
                <div id="tab_archive" class="am-tab-content">

                    <asp:Panel ID="pnlArchive" runat="server" Visible="false">
                        <asp:Repeater ID="rptArchive" runat="server" OnItemCommand="rptArchive_ItemCommand">
                            <ItemTemplate>
                                <div class="am-archive-panel">
                                    <div class="am-archive-header">
                                        <div>
                                            <div class="am-archive-title">
                                                📦 نسخه <%# Eval("VersionNumber") %> — <%# Eval("Title") %>
                                            </div>
                                            <div class="am-archive-meta">
                                                👤 <%# Eval("CreatedByName") %> | 
                                                🕐 <%# GetPersianDateTime(Eval("CreatedDate")) %> | 
                                                📝 <%# Eval("Description") %>
                                            </div>
                                        </div>
                                        <asp:LinkButton ID="lnkViewVersion" runat="server" CssClass="am-btn am-btn-edit"
                                            CommandName="ViewVersion" CommandArgument='<%# Eval("ID") %>' 
                                            CausesValidation="false">
                                            👁️ مشاهده
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>

                    <asp:Panel ID="pnlNoArchive" runat="server" Visible="false">
                        <div class="am-empty">
                            <span class="icon">📦</span>
                            هنوز هیچ نسخه‌ای آرشیو نشده است.
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlVersionDetail" runat="server" Visible="false" 
                        style="margin-top:20px;padding-top:20px;border-top:2px solid #e9ecef;">
                        <h3 style="font-size:16px;color:#6c3483;margin:0 0 15px 0;">
                            📋 جزئیات نسخه <asp:Label ID="lblVersionDetailNumber" runat="server" /> — 
                            <asp:Label ID="lblVersionDetailTitle" runat="server" />
                        </h3>

                        <div class="am-table-wrap">
                            <asp:Repeater ID="rptVersionDetail" runat="server">
                                <HeaderTemplate>
                                    <table class="am-table">
                                        <thead>
                                            <tr>
                                                <th class="col-row">#</th>
                                                <th class="col-level">مقطع تحصیلی</th>
                                                <th class="col-amount">مبلغ قبلی (ریال)</th>
                                                <th class="col-amount">مبلغ جدید (ریال)</th>
                                                <th class="col-amount">تغییر</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="col-row"><%# Eval("RowNumber") %></td>
                                        <td class="col-level"><%# Eval("EducationLevel") %></td>
                                        <td class="col-amount" style="color:#e74c3c;">
                                            <%# GetAmountDisplay(Eval("PreviousAmount")) %>
                                        </td>
                                        <td class="col-amount" style="color:#27ae60;font-weight:700;">
                                            <%# GetAmountDisplay(Eval("Amount")) %>
                                        </td>
                                        <td class="col-amount" style="font-weight:700;">
                                            <%# GetDiffDisplay(Eval("PreviousAmount"), Eval("Amount")) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></tbody></table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                </div>

            </div>

            <%-- ============================================================
                 ✅ Modal — داخل ContentTemplate (روش جدید)
                 ============================================================ --%>
            <div id="amEditModal" class="am-modal-overlay" style="display:none;">
                <div class="am-modal">
                    <div class="am-modal-header">
                        <h3>✏️ ویرایش مبلغ مقطع</h3>
                    </div>
                    <div class="am-modal-body">
                        <div class="am-form-group">
                            <label>مقطع تحصیلی:</label>
                            <div id="spanEditLevelName" 
                                style="display:block;padding:10px 12px;background:#f8f9fa;border:1px solid #e0e0e0;border-radius:8px;font-weight:700;color:#2c3e50;">
                                —
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label>مبلغ قبلی (ریال):</label>
                            <div id="spanEditPreviousAmount" 
                                style="display:block;padding:10px 12px;background:#fff5f5;border:1px solid #f8d7da;border-radius:8px;font-weight:700;color:#e74c3c;direction:ltr;text-align:left;">
                                —
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label>مبلغ جدید (ریال): <span class="required">*</span></label>
                            <input type="text" id="inputNewAmount" class="am-form-input" 
                                placeholder="مثال: 18000000" maxlength="15"
                                oninput="formatAmount(this);" />
                            <span style="font-size:11px;color:#6c757d;display:block;margin-top:5px;">
                                💡 فقط عدد وارد کنید (بدون کاما)
                            </span>
                        </div>

                        <div class="am-form-group">
                            <label>دلیل تغییر (اختیاری):</label>
                            <textarea id="inputReason" class="am-form-textarea" rows="2" 
                                placeholder="مثال: افزایش سالانه طبق مصوبه..."></textarea>
                        </div>

                        <div id="divPreview" style="display:none;background:#e8f8f0;border:2px solid #27ae60;border-radius:8px;padding:12px;margin-top:10px;">
                            <span style="font-size:11px;color:#155724;font-weight:600;display:block;margin-bottom:5px;">
                                پیش‌نمایش مبلغ جدید:
                            </span>
                            <span id="spanPreviewAmount" style="font-size:20px;color:#27ae60;font-weight:800;"></span>
                            <span id="spanPreviewWords" style="display:block;font-size:11px;color:#6c757d;margin-top:5px;"></span>
                        </div>
                    </div>
                    <div class="am-modal-footer">
                        <button type="button" class="am-modal-btn am-modal-btn-cancel" 
                            onclick="cancelEdit();">❌ انصراف</button>
                        
                        <button type="button" class="am-modal-btn am-modal-btn-save" 
                            onclick="saveEdit();">💾 ثبت تغییر</button>
                    </div>
                </div>
            </div>

            <%-- HiddenFields --%>
            <asp:HiddenField ID="hfOpenEditModal" runat="server" Value="0" />
            <asp:HiddenField ID="hfCloseEditModal" runat="server" Value="0" />
            <asp:HiddenField ID="hfEditLevelName" runat="server" Value="" />
            <asp:HiddenField ID="hfEditPreviousAmount" runat="server" Value="" />
            <asp:HiddenField ID="hfEditConditionID" runat="server" Value="" />
            <asp:HiddenField ID="hfShowAlert" runat="server" Value="0" />
            <asp:HiddenField ID="hfAlertType" runat="server" Value="" />
            <asp:HiddenField ID="hfAlertTitle" runat="server" Value="" />
            <asp:HiddenField ID="hfAlertMessage" runat="server" Value="" />
            <asp:HiddenField ID="hfShowToast" runat="server" Value="0" />
            <asp:HiddenField ID="hfToastType" runat="server" Value="" />
            <asp:HiddenField ID="hfToastMessage" runat="server" Value="" />

            <%-- Hidden button برای PostBack از JavaScript --%>
            <div style="display:none;">
                <asp:Button ID="btnSaveEditHidden" runat="server" Text="" OnClick="btnSaveEdit_Click" />
                <asp:Button ID="btnCancelEditHidden" runat="server" Text="" OnClick="btnCancelEdit_Click" />
            </div>

            <%-- داده‌های پنهان --%>
            <input type="hidden" id="dataConditionID" name="dataConditionID" value="" />
            <input type="hidden" id="dataAmount" name="dataAmount" value="" />
            <input type="hidden" id="dataReason" name="dataReason" value="" />

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveEditHidden" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelEditHidden" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveAll" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <%-- Alert و Toast --%>
    <div id="amAlert" class="am-alert-overlay">
        <div class="am-alert">
            <div id="amAlertHeader" class="am-alert-header info">
                <span id="amAlertIcon" class="am-alert-icon">ℹ️</span>
                <h3 id="amAlertTitle" class="am-alert-title">اطلاع</h3>
            </div>
            <div id="amAlertBody" class="am-alert-body"></div>
            <div class="am-alert-footer">
                <button type="button" class="am-alert-btn" onclick="closeAlert();">متوجه شدم</button>
            </div>
        </div>
    </div>

    <div id="amToastContainer" class="am-toast-container"></div>

    <script type="text/javascript">
        // ============================================================
        // Tab
        // ============================================================
        function switchTab(tabName) {
            var tabs = document.querySelectorAll('.am-tab-content');
            for (var i = 0; i < tabs.length; i++) tabs[i].classList.remove('active');
            var tabButtons = document.querySelectorAll('.am-tab');
            for (var i = 0; i < tabButtons.length; i++) tabButtons[i].classList.remove('active');
            var activeContent = document.getElementById('tab_' + tabName);
            if (activeContent) activeContent.classList.add('active');
            var activeBtn = document.getElementById('btn_tab_' + tabName);
            if (activeBtn) activeBtn.classList.add('active');
            var hf = document.getElementById('<%= hfActiveTab.ClientID %>');
            if (hf) hf.value = tabName;
        }

        // ============================================================
        // ✅ باز/بستن Modal (روش جدید — بدون انتقال)
        // ============================================================
        function openEditModal() {
            var modal = document.getElementById('amEditModal');
            if (!modal) return;

            // پر کردن مقادیر از HiddenField
            try {
                var hfLevel = document.getElementById('<%= hfEditLevelName.ClientID %>');
                var hfPrevAmount = document.getElementById('<%= hfEditPreviousAmount.ClientID %>');
                var hfCondID = document.getElementById('<%= hfEditConditionID.ClientID %>');

                var spanLevel = document.getElementById('spanEditLevelName');
                var spanPrevAmount = document.getElementById('spanEditPreviousAmount');

                if (hfLevel && spanLevel) spanLevel.innerText = hfLevel.value || '—';
                if (hfPrevAmount && spanPrevAmount) spanPrevAmount.innerText = hfPrevAmount.value || '—';

                var dataCondID = document.getElementById('dataConditionID');
                if (dataCondID && hfCondID) dataCondID.value = hfCondID.value || '';
            } catch (e) { console.error('openEditModal:', e); }

            modal.style.display = 'block';
            modal.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }

        function closeEditModal() {
            var modal = document.getElementById('amEditModal');
            if (!modal) return;
            modal.style.display = 'none';

            // پاک کردن فیلدها
            var inputAmount = document.getElementById('inputNewAmount');
            if (inputAmount) inputAmount.value = '';
            var inputReason = document.getElementById('inputReason');
            if (inputReason) inputReason.value = '';
            var divPreview = document.getElementById('divPreview');
            if (divPreview) divPreview.style.display = 'none';
        }

        // ============================================================
        // ✅ دکمه انصراف
        // ============================================================
        function cancelEdit() {
            try {
                // بستن فوری Modal
                closeEditModal();

                // PostBack برای پاک کردن Session
                var btn = document.getElementById('<%= btnCancelEditHidden.ClientID %>');
                if (btn) {
                    btn.click();
                } else {
                    __doPostBack('<%= btnCancelEditHidden.UniqueID %>', '');
                }
            } catch (e) {
                console.error('cancelEdit:', e);
                closeEditModal();
            }
        }

        // ============================================================
        // ✅ دکمه ثبت
        // ============================================================
        function saveEdit() {
            try {
                var inputAmount = document.getElementById('inputNewAmount');
                if (!inputAmount) {
                    showToast('warning', 'مبلغ یافت نشد');
                    return;
                }

                inputAmount.value = inputAmount.value.replace(/[^\d]/g, '');

                if (!inputAmount.value) {
                    showToast('warning', 'لطفاً مبلغ جدید را وارد کنید');
                    return;
                }

                // پر کردن hidden data
                var dataAmount = document.getElementById('dataAmount');
                if (dataAmount) dataAmount.value = inputAmount.value;

                var dataReason = document.getElementById('dataReason');
                var inputReason = document.getElementById('inputReason');
                if (dataReason && inputReason) dataReason.value = inputReason.value || '';

                var dataCondID = document.getElementById('dataConditionID');
                var hfCondID = document.getElementById('<%= hfEditConditionID.ClientID %>');
                if (dataCondID && hfCondID) dataCondID.value = hfCondID.value || '';

                // بستن فوری Modal (حتی قبل از PostBack)
                closeEditModal();

                // PostBack
                var btn = document.getElementById('<%= btnSaveEditHidden.ClientID %>');
                if (btn) {
                    btn.click();
                } else {
                    __doPostBack('<%= btnSaveEditHidden.UniqueID %>', '');
                }
            } catch (e) {
                console.error('saveEdit:', e);
                closeEditModal();
            }
        }

        // ============================================================
        // Alert
        // ============================================================
        function showAlert(type, title, bodyHtml) {
            var alert = document.getElementById('amAlert');
            var header = document.getElementById('amAlertHeader');
            var icon = document.getElementById('amAlertIcon');
            var titleEl = document.getElementById('amAlertTitle');
            var body = document.getElementById('amAlertBody');
            if (!alert) return;

            header.className = 'am-alert-header ' + type;
            var icons = { 'success': '✅', 'error': '❌', 'warning': '⚠️', 'info': 'ℹ️' };
            icon.innerText = icons[type] || 'ℹ️';
            titleEl.innerText = title;
            body.innerHTML = bodyHtml;
            alert.style.display = 'block';
        }

        function closeAlert() {
            var alert = document.getElementById('amAlert');
            if (alert) alert.style.display = 'none';
        }

        // ============================================================
        // Toast
        // ============================================================
        function showToast(type, message, duration) {
            var container = document.getElementById('amToastContainer');
            if (!container) return;

            var toast = document.createElement('div');
            toast.className = 'am-toast ' + type;
            var icons = { 'success': '✅', 'error': '❌', 'warning': '⚠️', 'info': 'ℹ️' };
            toast.innerHTML = '<span class="am-toast-icon">' + (icons[type] || 'ℹ️') + '</span>' +
                             '<span class="am-toast-message">' + message + '</span>';
            container.appendChild(toast);
            setTimeout(function () { if (toast.parentElement) toast.remove(); }, duration || 4000);
        }

        // ============================================================
        // فرمت کردن مبلغ
        // ============================================================
        function formatAmount(input) {
            var value = input.value.replace(/[^\d]/g, '');
            if (value) {
                input.value = value;
                var amount = parseInt(value, 10);
                if (amount > 0) {
                    var divPreview = document.getElementById('divPreview');
                    var spanPreviewAmount = document.getElementById('spanPreviewAmount');
                    var spanPreviewWords = document.getElementById('spanPreviewWords');
                    if (divPreview) divPreview.style.display = 'block';
                    if (spanPreviewAmount) spanPreviewAmount.innerText = amount.toLocaleString('en-US') + ' ریال';
                    if (spanPreviewWords) spanPreviewWords.innerText = numberToPersianWords(amount) + ' ریال';
                } else {
                    var divPreview2 = document.getElementById('divPreview');
                    if (divPreview2) divPreview2.style.display = 'none';
                }
            } else {
                var divPreview3 = document.getElementById('divPreview');
                if (divPreview3) divPreview3.style.display = 'none';
            }
        }

        // ============================================================
        // تبدیل عدد به حروف فارسی
        // ============================================================
        function numberToPersianWords(num) {
            if (num === 0) return 'صفر';

            var yekan = ['', 'یک', 'دو', 'سه', 'چهار', 'پنج', 'شش', 'هفت', 'هشت', 'نه'];
            var dahgan = ['', '', 'بیست', 'سی', 'چهل', 'پنجاه', 'شصت', 'هفتاد', 'هشتاد', 'نود'];
            var dahYek = ['ده', 'یازده', 'دوازده', 'سیزده', 'چهارده', 'پانزده', 'شانزده', 'هفده', 'هجده', 'نوزده'];
            var sadgan = ['', 'صد', 'دویست', 'سیصد', 'چهارصد', 'پانصد', 'ششصد', 'هفتصد', 'هشتصد', 'نهصد'];
            var scales = ['', ' هزار', ' میلیون', ' میلیارد', ' بیلیون'];

            var result = '';
            var scaleIndex = 0;

            while (num > 0) {
                var part = num % 1000;
                num = Math.floor(num / 1000);

                if (part > 0) {
                    var partText = '';
                    var s = Math.floor(part / 100);
                    if (s > 0) partText += sadgan[s];

                    var remainder = part % 100;
                    if (remainder > 0) {
                        if (partText !== '') partText += ' و ';
                        if (remainder < 10) {
                            partText += yekan[remainder];
                        } else if (remainder < 20) {
                            partText += dahYek[remainder - 10];
                        } else {
                            var d = Math.floor(remainder / 10);
                            var y = remainder % 10;
                            partText += dahgan[d];
                            if (y > 0) partText += ' و ' + yekan[y];
                        }
                    }
                    if (result !== '') {
                        result = partText + scales[scaleIndex] + ' و ' + result;
                    } else {
                        result = partText + scales[scaleIndex];
                    }
                }
                scaleIndex++;
            }
            return result.trim();
        }

        // ============================================================
        // تأیید ذخیره همه
        // ============================================================
        function confirmSaveAll() {
            var pending = document.getElementById('<%= hfPendingChanges.ClientID %>');
            if (!pending || !pending.value) {
                showToast('warning', 'هیچ تغییر جدیدی برای ذخیره وجود ندارد');
                return false;
            }
            var count = pending.value.split('|').length;
            return confirm('آیا از ذخیره ' + count + ' تغییر مطمئن هستید؟\n\n(نسخه قبلی به‌صورت خودکار آرشیو می‌شود)');
        }

        // ============================================================
        // Escape
        // ============================================================
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') { closeAlert(); closeEditModal(); }
        });

        // ============================================================
        // ✅ بعد از PostBack
        // ============================================================
        function afterPostBackHandler() {
            try {
                // ✅ بررسی باز کردن Modal
                var hfOpen = document.getElementById('<%= hfOpenEditModal.ClientID %>');
                if (hfOpen && hfOpen.value === '1') {
                    openEditModal();
                    hfOpen.value = '0';
                }

                // ✅ بستن Modal اگر لازم است
                var hfClose = document.getElementById('<%= hfCloseEditModal.ClientID %>');
                if (hfClose && hfClose.value === '1') {
                    closeEditModal();
                    hfClose.value = '0';
                }

                // ✅ نمایش Alert
                var hfShowAlert = document.getElementById('<%= hfShowAlert.ClientID %>');
                if (hfShowAlert && hfShowAlert.value === '1') {
                    var type = document.getElementById('<%= hfAlertType.ClientID %>').value;
                    var title = document.getElementById('<%= hfAlertTitle.ClientID %>').value;
                    var msg = document.getElementById('<%= hfAlertMessage.ClientID %>').value;
                    showAlert(type, title, msg);
                    hfShowAlert.value = '0';
                }

                // ✅ نمایش Toast
                var hfShowToast = document.getElementById('<%= hfShowToast.ClientID %>');
                if (hfShowToast && hfShowToast.value === '1') {
                    var tType = document.getElementById('<%= hfToastType.ClientID %>').value;
                    var tMsg = document.getElementById('<%= hfToastMessage.ClientID %>').value;
                    showToast(tType, tMsg);
                    hfShowToast.value = '0';
                }
            } catch (e) { console.error('afterPostBack:', e); }
        }

        if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                afterPostBackHandler();
            });
        }

        (function () {
            function onLoad() {
                afterPostBackHandler();
            }
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', onLoad);
            } else { onLoad(); }
        })();
    </script>

</asp:Content>