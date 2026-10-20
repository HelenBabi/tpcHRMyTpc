<%@ Page Title="بررسی درخواست‌های فرزندان ممتاز" Language="VB" MasterPageFile="~/dabir.master" 
    AutoEventWireup="false" CodeFile="ChildRequestReview.aspx.vb" Inherits="ChildRequestReview" 
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .rv-container { max-width: 1400px; margin: 25px auto; padding: 20px 25px; background: #fff; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; font-family: 'Segoe UI', Tahoma, sans-serif; }
        .rv-header-bar { display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px; padding-bottom: 15px; border-bottom: 2px solid #e9ecef; flex-wrap: wrap; gap: 10px; }
        .rv-title { font-size: 22px; font-weight: 700; color: #2c3e50; margin: 0; }
        .rv-header-actions { display: flex; gap: 8px; flex-wrap: wrap; }
        .rv-back-btn { padding: 10px 22px; background: #6c757d; color: #fff; border: none; border-radius: 8px; cursor: pointer; font-family: inherit; font-size: 13px; font-weight: 600; text-decoration: none; display: inline-flex; align-items: center; gap: 6px; }
        .rv-back-btn:hover { background: #5a6268; color: #fff; text-decoration: none; }
        .rv-export-btn { padding: 10px 22px; background: #27ae60; color: #fff; border: none; border-radius: 8px; cursor: pointer; font-family: inherit; font-size: 13px; font-weight: 600; text-decoration: none; display: inline-flex; align-items: center; gap: 6px; }
        .rv-export-btn:hover { background: #229954; color: #fff; text-decoration: none; }

        .rv-tabs { display: flex; gap: 0; margin-bottom: 0; border-bottom: 2px solid #3498db; }
        .rv-tab { padding: 12px 30px; background: #f8f9fa; border: 2px solid #e0e0e0; border-bottom: none; border-radius: 10px 10px 0 0; cursor: pointer; font-family: inherit; font-size: 14px; font-weight: 600; color: #6c757d; margin-bottom: -2px; transition: all 0.2s; display: inline-flex; align-items: center; gap: 8px; }
        .rv-tab:hover { background: #e8f4fd; color: #2980b9; }
        .rv-tab.active { background: #fff; color: #2c3e50; border-color: #3498db; border-bottom: 2px solid #fff; font-weight: 700; }
        .rv-tab-badge { background: #3498db; color: #fff; padding: 2px 10px; border-radius: 12px; font-size: 11px; font-weight: 700; }
        .rv-tab.active .rv-tab-badge { background: #2c3e50; }
        .rv-tab.closed-tab.active { border-color: #27ae60; }
        .rv-tab.closed-tab.active .rv-tab-badge { background: #27ae60; }

        .rv-tab-content { display: none; padding-top: 20px; }
        .rv-tab-content.active { display: block; }

        .rv-filter-box { background: linear-gradient(135deg, #e8f4fd, #d1ecf1); border: 2px solid #3498db; border-radius: 10px; padding: 15px 20px; margin-bottom: 20px; }
        .rv-filter-row { display: flex; gap: 12px; flex-wrap: wrap; align-items: flex-end; }
        .rv-filter-item { flex: 1; min-width: 180px; }
        .rv-filter-item label { display: block; font-weight: 600; color: #0c5460; margin-bottom: 6px; font-size: 12px; }
        .rv-input, .rv-select { width: 100%; padding: 8px 12px; border: 1.5px solid #b8daff; border-radius: 8px; font-size: 13px; font-family: inherit; box-sizing: border-box; background: #fff; }
        .rv-input:focus, .rv-select:focus { border-color: #3498db; outline: none; box-shadow: 0 0 0 3px rgba(52,152,219,0.15); }
        .rv-filter-btn { padding: 9px 25px; background: #3498db; color: #fff; border: none; border-radius: 8px; cursor: pointer; font-family: inherit; font-size: 13px; font-weight: 600; }
        .rv-filter-btn:hover { background: #2980b9; }

        .rv-table-wrap { overflow-x: auto; border-radius: 0 10px 10px 10px; border: 1px solid #e0e0e0; background: #fff; }
        .rv-table { width: 100%; border-collapse: collapse; font-size: 13px; }
        .rv-table thead { background: #2c3e50; color: #fff; }
        .rv-table thead th { padding: 12px 10px; text-align: right; font-weight: 600; font-size: 12px; white-space: nowrap; }
        .rv-table tbody tr { border-bottom: 1px solid #e9ecef; }
        .rv-table tbody tr:hover { background: #f8f9fa; }
        .rv-table tbody td { padding: 10px; vertical-align: middle; }
        .rv-table .col-id { width: 60px; text-align: center; font-weight: 700; color: #3498db; }
        .rv-table .col-name { font-weight: 600; color: #2c3e50; }
        .rv-table .col-actions { width: 130px; text-align: center; }
        .rv-table .col-amount { text-align: center; font-weight: 700; color: #27ae60; }
        .rv-table .col-contract { font-size: 12px; color: #6c757d; }

        .rv-status-badge { padding: 4px 12px; border-radius: 12px; font-size: 11px; font-weight: 600; white-space: nowrap; display: inline-block; }
        .rv-status-1 { background: #fff3cd; color: #856404; }
        .rv-status-2 { background: #d1ecf1; color: #0c5460; }
        .rv-status-3 { background: #d4edda; color: #155724; }
        .rv-status-4 { background: #f8d7da; color: #721c24; }
        .rv-status-5 { background: #e2e3e5; color: #383d41; }

        .rv-btn { padding: 6px 14px; border: none; border-radius: 6px; font-size: 12px; font-weight: 600; cursor: pointer; font-family: inherit; text-decoration: none; display: inline-block; }
        .rv-btn-view { background: #3498db; color: #fff; }
        .rv-btn-view:hover { background: #2980b9; color: #fff; text-decoration: none; }

        .rv-empty { text-align: center; padding: 40px 20px; color: #6c757d; font-size: 14px; }
        .rv-empty .icon { font-size: 50px; display: block; margin-bottom: 10px; }

        .rv-modal-overlay { display: none; position: fixed; top: 0 !important; left: 0 !important; right: 0 !important; bottom: 0 !important; width: 100vw !important; height: 100vh !important; background: rgba(0,0,0,0.75); z-index: 2147483647 !important; margin: 0 !important; padding: 0 !important; box-sizing: border-box; direction: ltr; overflow: hidden; }
        .rv-modal-overlay.show { display: block !important; }

        .rv-modal { background: #fff; border-radius: 0; box-shadow: none; display: flex; flex-direction: column; text-align: right; direction: rtl; overflow: hidden; box-sizing: border-box; position: absolute; top: 0; bottom: 0; left: 0; right: 0; width: 100%; height: 100%; max-width: 100%; margin: 0; }

        .rv-modal-header { padding: 18px 25px; background: linear-gradient(135deg, #2c3e50, #34495e); color: #fff; display: flex; justify-content: space-between; align-items: center; flex-shrink: 0; }
        .rv-modal-header h3 { margin: 0; font-size: 17px; font-weight: 700; }
        .rv-modal-close { background: none; border: none; color: #fff; font-size: 24px; cursor: pointer; line-height: 1; }

        .rv-modal-body { padding: 25px 30px; flex: 1 1 auto; overflow-y: auto; box-sizing: border-box; min-height: 0; }

        .rv-modal-footer { padding: 15px 25px 18px; border-top: 1px solid #e9ecef; background: #f8f9fa; flex-shrink: 0; display: flex; justify-content: space-between; align-items: center; gap: 10px; flex-wrap: wrap; box-sizing: border-box; }
        .rv-modal-footer-actions { display: flex; gap: 10px; flex-wrap: wrap; }

        .rv-detail-section { background: #f8f9fa; border-radius: 10px; padding: 15px 20px; margin-bottom: 15px; border-right: 4px solid #3498db; }
        .rv-detail-section.documents { border-right-color: #27ae60; background: #e8f8f0; }
        .rv-detail-section.identity { border-right-color: #e91e63; background: #fce4ec; }
        .rv-detail-section.school { border-right-color: #3498db; background: #e8f4fd; }
        .rv-detail-section.university { border-right-color: #9c27b0; background: #f3e5f5; }
        .rv-detail-section.sampad { border-right-color: #f39c12; background: #fef9e7; }
        .rv-detail-section.olympiad { border-right-color: #4caf50; background: #e8f5e9; }
        .rv-detail-section.exceptional { border-right-color: #e91e63; background: #fce4ec; }
        .rv-detail-section.contract { border-right-color: #3498db; background: #e8f4fd; }
        .rv-detail-section h4 { font-size: 14px; font-weight: 700; color: #2c3e50; margin: 0 0 12px 0; padding-bottom: 8px; border-bottom: 1px dashed #dee2e6; }
        .rv-detail-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(240px, 1fr)); gap: 12px; }
        .rv-detail-item { background: #fff; padding: 10px 14px; border-radius: 8px; border: 1px solid #e9ecef; }
        .rv-detail-item .lbl { font-size: 11px; color: #6c757d; font-weight: 600; display: block; margin-bottom: 4px; }
        .rv-detail-item .val { font-size: 13px; color: #2c3e50; font-weight: 600; word-break: break-word; }

        /* ============================================================
           ✅ بخش مبلغ — طراحی برجسته و شکیل
           ============================================================ */
        .rv-detail-section.amount { 
            border-right: none; 
            background: linear-gradient(135deg, #1e8449 0%, #27ae60 50%, #2ecc71 100%);
            padding: 0;
            border-radius: 14px;
            box-shadow: 0 8px 25px rgba(39, 174, 96, 0.35);
            overflow: hidden;
            position: relative;
        }
        .rv-detail-section.amount::before {
            content: '';
            position: absolute;
            top: -50%;
            right: -50%;
            width: 200%;
            height: 200%;
            background: radial-gradient(circle, rgba(255,255,255,0.15) 0%, transparent 70%);
            pointer-events: none;
        }
        .rv-detail-section.amount h4 {
            background: rgba(0,0,0,0.15);
            color: #fff;
            border-bottom: none;
            margin: 0;
            padding: 12px 20px;
            font-size: 14px;
            font-weight: 700;
            text-shadow: 0 2px 4px rgba(0,0,0,0.2);
            position: relative;
            z-index: 1;
        }

        .rv-amount-display { 
            padding: 25px 20px; 
            text-align: center; 
            position: relative;
            z-index: 1;
        }
        .rv-amount-display .amount-level-badge {
            display: inline-block;
            background: rgba(255,255,255,0.95);
            color: #1e8449;
            padding: 6px 20px;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 800;
            margin-bottom: 15px;
            box-shadow: 0 3px 10px rgba(0,0,0,0.15);
        }
        .rv-amount-display .amount-value-box {
            background: rgba(255,255,255,0.98);
            border-radius: 14px;
            padding: 20px 30px;
            display: inline-block;
            min-width: 280px;
            box-shadow: 0 6px 20px rgba(0,0,0,0.2);
            border: 3px solid #fff;
            position: relative;
        }
        .rv-amount-display .amount-value-box::after {
            content: '';
            position: absolute;
            bottom: -3px;
            right: 15%;
            left: 15%;
            height: 4px;
            background: linear-gradient(90deg, transparent, #27ae60, transparent);
            border-radius: 2px;
        }
        .rv-amount-display .amount-value { 
            font-size: 34px; 
            color: #1e8449; 
            font-weight: 900; 
            display: block; 
            line-height: 1.2;
            letter-spacing: -0.5px;
            text-shadow: 0 1px 2px rgba(0,0,0,0.1);
        }
        .rv-amount-display .amount-unit {
            font-size: 15px;
            color: #27ae60;
            font-weight: 700;
            display: block;
            margin-top: 5px;
        }
        .rv-amount-display .amount-words {
            font-size: 12px;
            color: #6c757d;
            display: block;
            margin-top: 8px;
            padding-top: 8px;
            border-top: 1px dashed #dee2e6;
            font-weight: 600;
        }
        .rv-amount-display .amount-hint {
            display: block;
            margin-top: 15px;
            font-size: 11px;
            color: rgba(255,255,255,0.95);
            background: rgba(0,0,0,0.2);
            padding: 8px 15px;
            border-radius: 20px;
            display: inline-block;
            font-weight: 600;
        }

        /* حالت خالی */
        .rv-amount-empty { 
            background: linear-gradient(135deg, #fff3cd, #ffe8a1); 
            border: 2px dashed #f39c12; 
            border-radius: 12px; 
            padding: 20px; 
            text-align: center; 
            color: #856404; 
            font-size: 13px; 
            font-weight: 600; 
            margin-top: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 10px;
        }
        .rv-amount-empty .empty-icon {
            font-size: 28px;
            animation: pulseIcon 2s ease-in-out infinite;
        }
        @keyframes pulseIcon {
            0%, 100% { transform: scale(1); }
            50% { transform: scale(1.15); }
        }

        .rv-doc-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(280px, 1fr)); gap: 15px; }
        .rv-doc-card { background: #fff; border: 2px solid #e0e0e0; border-radius: 10px; padding: 15px; text-align: center; }
        .rv-doc-card.missing { border-color: #e74c3c; background: #fff5f5; }
        .rv-doc-card.identity-card { border-color: #e91e63; }
        .rv-doc-card.identity-card.missing { border-color: #e74c3c; background: #fff5f5; }
        .rv-doc-card .doc-icon { font-size: 40px; display: block; margin-bottom: 8px; }
        .rv-doc-card .doc-title { font-size: 13px; font-weight: 700; color: #2c3e50; margin-bottom: 6px; }
        .rv-doc-card .doc-name { font-size: 11px; color: #6c757d; margin-bottom: 10px; word-break: break-all; }
        .rv-doc-actions { display: flex; gap: 8px; justify-content: center; flex-wrap: wrap; }
        .rv-doc-btn { padding: 7px 15px; border: none; border-radius: 6px; font-size: 12px; font-weight: 600; cursor: pointer; font-family: inherit; text-decoration: none; display: inline-block; }
        .rv-doc-btn-view { background: #3498db; color: #fff; }
        .rv-doc-btn-view:hover { background: #2980b9; color: #fff; text-decoration: none; }
        .rv-doc-btn-download { background: #27ae60; color: #fff; }
        .rv-doc-btn-download:hover { background: #229954; color: #fff; text-decoration: none; }

        .rv-review-form { background: #fff; border: 2px solid #f39c12; border-radius: 10px; padding: 15px 20px; margin-top: 15px; }
        .rv-form-group { margin-bottom: 15px; }
        .rv-form-group label { display: block; font-weight: 600; color: #2c3e50; margin-bottom: 6px; font-size: 13px; }
        .rv-textarea { width: 100%; padding: 10px 12px; border: 1.5px solid #e0e0e0; border-radius: 8px; font-size: 13px; font-family: inherit; box-sizing: border-box; resize: vertical; min-height: 70px; }
        .rv-textarea:focus { border-color: #f39c12; outline: none; box-shadow: 0 0 0 3px rgba(243,156,18,0.15); }

        .rv-reason-chips { display: flex; flex-wrap: wrap; gap: 8px; margin-bottom: 10px; }
        .rv-chip { padding: 7px 14px; background: #fff; border: 2px solid #e0e0e0; border-radius: 20px; font-size: 12px; cursor: pointer; transition: all 0.2s; font-family: inherit; color: #2c3e50; font-weight: 600; }
        .rv-chip:hover { border-color: #e74c3c; background: #fff5f5; }
        .rv-chip.selected { background: #e74c3c; color: #fff; border-color: #e74c3c; }
        .rv-chip.reject-chip:hover { border-color: #c0392b; background: #f8d7da; }
        .rv-chip.reject-chip.selected { background: #c0392b; color: #fff; border-color: #c0392b; }
        .rv-chip.correction-chip:hover { border-color: #3498db; background: #e8f4fd; }
        .rv-chip.correction-chip.selected { background: #3498db; color: #fff; border-color: #3498db; }

        .rv-action-btn { padding: 10px 25px; border: none; border-radius: 8px; font-size: 13px; font-weight: 700; cursor: pointer; font-family: inherit; }
        .rv-action-btn:hover { transform: translateY(-1px); box-shadow: 0 4px 10px rgba(0,0,0,0.15); }
        .rv-action-approve { background: #27ae60; color: #fff; }
        .rv-action-reject { background: #e74c3c; color: #fff; }
        .rv-action-correction { background: #3498db; color: #fff; }
        .rv-action-cancel { background: #95a5a6; color: #fff; }

        .rv-toast-container { position: fixed; top: 80px; left: 50%; transform: translateX(-50%); z-index: 2147483646 !important; direction: rtl; }
        .rv-toast { background: #fff; border-radius: 12px; padding: 15px 25px; margin-bottom: 10px; box-shadow: 0 8px 30px rgba(0,0,0,0.15); display: flex; align-items: center; gap: 12px; min-width: 300px; max-width: 500px; border-right: 5px solid; }
        .rv-toast.success { border-right-color: #27ae60; }
        .rv-toast.error { border-right-color: #e74c3c; }
        .rv-toast.warning { border-right-color: #f39c12; }
        .rv-toast.info { border-right-color: #3498db; }
        .rv-toast-icon { font-size: 24px; }
        .rv-toast-message { font-size: 13px; color: #2c3e50; font-weight: 600; }

        .rv-alert-overlay { display: none; position: fixed; top: 0 !important; left: 0 !important; right: 0 !important; bottom: 0 !important; width: 100vw !important; height: 100vh !important; background: rgba(0,0,0,0.7); z-index: 2147483647 !important; text-align: center; }
        .rv-alert-overlay.show { display: block !important; }
        .rv-alert-overlay::before { content: ''; display: inline-block; height: 100%; vertical-align: middle; margin-right: -0.25em; }
        .rv-alert { background: #fff; border-radius: 14px; max-width: 450px; width: 90%; display: inline-block; text-align: center; vertical-align: middle; overflow: hidden; }
        .rv-alert-header { padding: 22px; color: #fff; }
        .rv-alert-header.success { background: linear-gradient(135deg, #27ae60, #2ecc71); }
        .rv-alert-header.error { background: linear-gradient(135deg, #e74c3c, #c0392b); }
        .rv-alert-header.warning { background: linear-gradient(135deg, #f39c12, #e67e22); }
        .rv-alert-header.info { background: linear-gradient(135deg, #3498db, #2980b9); }
        .rv-alert-icon { font-size: 50px; display: block; margin-bottom: 8px; }
        .rv-alert-title { font-size: 18px; font-weight: 700; margin: 0; }
        .rv-alert-body { padding: 20px 25px; color: #2c3e50; font-size: 14px; line-height: 1.8; }
        .rv-alert-footer { padding: 0 25px 22px; }
        .rv-alert-btn { padding: 10px 35px; border: none; border-radius: 25px; font-size: 14px; font-weight: 600; cursor: pointer; font-family: inherit; color: #fff; background: #3498db; }

        .rv-history-item { background: #fff; padding: 12px 15px; border-radius: 8px; border-right: 4px solid #9b59b6; margin-bottom: 10px; }
        .rv-history-item .rv-hist-header { display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; gap: 8px; margin-bottom: 8px; }
        .rv-history-item .rv-hist-date { font-size: 11px; color: #6c757d; }
        .rv-history-item .rv-hist-row { font-size: 12px; color: #2c3e50; margin-bottom: 4px; }
        .rv-history-item .rv-hist-row strong { color: #2c3e50; }
        .rv-history-item .rv-hist-comment { font-size: 11px; color: #6c757d; }

        /* ✅ هشدار عدم تطابق شرایط */
        .rv-condition-alert { background: linear-gradient(135deg, #f8d7da, #f5c6cb); border: 2px solid #e74c3c; border-radius: 10px; padding: 15px 20px; margin-bottom: 15px; display: flex; align-items: flex-start; gap: 12px; }
        .rv-condition-alert .alert-icon { font-size: 28px; flex-shrink: 0; }
        .rv-condition-alert .alert-content { flex: 1; }
        .rv-condition-alert .alert-title { font-size: 14px; font-weight: 700; color: #721c24; margin-bottom: 5px; }
        .rv-condition-alert .alert-text { font-size: 12px; color: #721c24; line-height: 1.8; }

        @media (max-width: 768px) {
            .rv-modal-body { padding: 15px; }
            .rv-detail-grid { grid-template-columns: 1fr; }
            .rv-doc-grid { grid-template-columns: 1fr; }
            .rv-header-bar { flex-direction: column; align-items: stretch; }
            .rv-amount-display .amount-value { font-size: 26px; }
            .rv-amount-display .amount-value-box { min-width: 240px; padding: 15px 20px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="rv-container">
        <div class="rv-header-bar">
            <h2 class="rv-title">📋 بررسی درخواست‌های فرزندان ممتاز — کارشناس روابط عمومی</h2>
            <div class="rv-header-actions">
                <asp:LinkButton ID="lnkExportExcel" runat="server" CssClass="rv-export-btn" 
                    OnClick="lnkExportExcel_Click" CausesValidation="false"
                    OnClientClick="return prepareExport();">📊 خروجی Excel</asp:LinkButton>
                <asp:LinkButton ID="lnkBackToHome" runat="server" CssClass="rv-back-btn" 
                    OnClick="lnkBackToHome_Click" CausesValidation="false">🏠 بازگشت به صفحه اصلی</asp:LinkButton>
            </div>
        </div>

        <div class="rv-tabs">
            <button type="button" id="btn_tab_open" class="rv-tab active" onclick="switchTab('open');">
                📂 درخواست‌های باز
                <span class="rv-tab-badge"><asp:Label ID="lblTabOpenCount" runat="server" Text="0" /></span>
            </button>
            <button type="button" id="btn_tab_closed" class="rv-tab closed-tab" onclick="switchTab('closed');">
                ✅ بررسی‌شده‌ها
                <span class="rv-tab-badge"><asp:Label ID="lblTabClosedCount" runat="server" Text="0" /></span>
            </button>
        </div>

        <asp:HiddenField ID="hfActiveTab" runat="server" Value="open" />

        <%-- تب ۱: درخواست‌های باز --%>
        <div id="tab_open" class="rv-tab-content active">
            <div class="rv-filter-box">
                <div class="rv-filter-row">
                    <div class="rv-filter-item">
                        <label>🔍 جستجو</label>
                        <asp:TextBox ID="txtSearchOpen" runat="server" CssClass="rv-input" placeholder="نام کارمند / نام فرزند / کد ملی" />
                    </div>
                    <div class="rv-filter-item">
                        <label>وضعیت</label>
                        <asp:DropDownList ID="ddlStatusOpen" runat="server" CssClass="rv-select">
                            <asp:ListItem Text="همه" Value="0" />
                            <asp:ListItem Text="⏳ در انتظار بررسی" Value="1" />
                            <asp:ListItem Text="📝 نیاز به اصلاح" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="rv-filter-item">
                        <label>نوع تحصیل</label>
                        <asp:DropDownList ID="ddlTypeOpen" runat="server" CssClass="rv-select">
                            <asp:ListItem Text="همه" Value="" />
                            <asp:ListItem Text="دانش‌آموز" Value="دانش‌آموز" />
                            <asp:ListItem Text="دانشجو" Value="دانشجو" />
                        </asp:DropDownList>
                    </div>
                    <div class="rv-filter-item" style="flex:0;">
                        <asp:Button ID="btnFilterOpen" runat="server" Text="🔎 اعمال فیلتر" CssClass="rv-filter-btn" OnClick="btnFilterOpen_Click" />
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlTableOpen" runat="server" Visible="false">
                <div class="rv-table-wrap">
                    <asp:Repeater ID="rptOpen" runat="server" OnItemCommand="rptOpen_ItemCommand">
    <HeaderTemplate>
        <table class="rv-table">
            <thead>
                <tr>
                    <th class="col-id">#</th>
                    <th>نام کارمند</th>
                    <th>نوع قرارداد</th>
                    <th>نام فرزند</th>
                    <th>کد ملی فرزند</th>
                    <th>نوع</th>
                    <th>مقطع</th>
                    <th>معدل</th>
                    <th>مدرک اضافی</th>
                    <th class="col-amount">مبلغ (ریال)</th>
                    <th>وضعیت</th>
                    <th>تاریخ ثبت</th>
                    <th class="col-actions">عملیات</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class="col-id"><%# DataBinder.Eval(Container.DataItem, "ID") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "FullName") %><br /><small style="color:#6c757d;"><%# DataBinder.Eval(Container.DataItem, "PersonCode") %></small></td>
            <td class="col-contract"><%# DataBinder.Eval(Container.DataItem, "ContractTypeName") %></td>
            <td class="col-name"><%# DataBinder.Eval(Container.DataItem, "ChildFullName") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "ChildNationalCode") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "ChildType") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "EducationLevel") %></td>
            <td style="text-align:center;"><%# DataBinder.Eval(Container.DataItem, "AverageDisplay") %></td>
            <td style="text-align:center;"><%# DataBinder.Eval(Container.DataItem, "ExtraDocDisplay") %></td>
            <td class="col-amount"><%# DataBinder.Eval(Container.DataItem, "AmountDisplay") %></td>
            <td><span class='rv-status-badge rv-status-<%# DataBinder.Eval(Container.DataItem, "StatusCode") %>'><%# DataBinder.Eval(Container.DataItem, "StatusText") %></span></td>
            <td><%# DataBinder.Eval(Container.DataItem, "CreateDatePersian") %></td>
            <td class="col-actions">
                <asp:LinkButton ID="lnkView" runat="server" CssClass="rv-btn rv-btn-view" 
                    CommandName="ViewRequest" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CausesValidation="false">👁️ مشاهده</asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate></tbody></table></FooterTemplate>
</asp:Repeater>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlEmptyOpen" runat="server" Visible="false">
                <div class="rv-empty"><span class="icon">📭</span>هیچ درخواستی با این فیلترها یافت نشد.</div>
            </asp:Panel>
        </div>

        <%-- تب ۲: بررسی‌شده‌ها --%>
        <div id="tab_closed" class="rv-tab-content">
            <div class="rv-filter-box" style="background:linear-gradient(135deg,#e8f8f0,#d4edda);border-color:#27ae60;">
                <div class="rv-filter-row">
                    <div class="rv-filter-item">
                        <label>🔍 جستجو</label>
                        <asp:TextBox ID="txtSearchClosed" runat="server" CssClass="rv-input" placeholder="نام کارمند / نام فرزند / کد ملی" />
                    </div>
                    <div class="rv-filter-item">
                        <label>وضعیت</label>
                        <asp:DropDownList ID="ddlStatusClosed" runat="server" CssClass="rv-select">
                            <asp:ListItem Text="همه" Value="0" />
                            <asp:ListItem Text="✅ تایید شده" Value="3" />
                            <asp:ListItem Text="❌ رد شده" Value="4" />
                            <asp:ListItem Text="🚫 انصراف" Value="5" />
                        </asp:DropDownList>
                    </div>
                    <div class="rv-filter-item">
                        <label>نوع تحصیل</label>
                        <asp:DropDownList ID="ddlTypeClosed" runat="server" CssClass="rv-select">
                            <asp:ListItem Text="همه" Value="" />
                            <asp:ListItem Text="دانش‌آموز" Value="دانش‌آموز" />
                            <asp:ListItem Text="دانشجو" Value="دانشجو" />
                        </asp:DropDownList>
                    </div>
                    <div class="rv-filter-item" style="flex:0;">
                        <asp:Button ID="btnFilterClosed" runat="server" Text="🔎 اعمال فیلتر" CssClass="rv-filter-btn" OnClick="btnFilterClosed_Click" />
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlTableClosed" runat="server" Visible="false">
                <div class="rv-table-wrap">
                    <asp:Repeater ID="rptClosed" runat="server" OnItemCommand="rptClosed_ItemCommand">
    <HeaderTemplate>
        <table class="rv-table">
            <thead>
                <tr>
                    <th class="col-id">#</th>
                    <th>نام کارمند</th>
                    <th>نوع قرارداد</th>
                    <th>نام فرزند</th>
                    <th>کد ملی فرزند</th>
                    <th>نوع</th>
                    <th>مقطع</th>
                    <th class="col-amount">مبلغ (ریال)</th>
                    <th>وضعیت</th>
                    <th>تاریخ ثبت</th>
                    <th class="col-actions">عملیات</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class="col-id"><%# DataBinder.Eval(Container.DataItem, "ID") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "FullName") %><br /><small style="color:#6c757d;"><%# DataBinder.Eval(Container.DataItem, "PersonCode") %></small></td>
            <td class="col-contract"><%# DataBinder.Eval(Container.DataItem, "ContractTypeName") %></td>
            <td class="col-name"><%# DataBinder.Eval(Container.DataItem, "ChildFullName") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "ChildNationalCode") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "ChildType") %></td>
            <td><%# DataBinder.Eval(Container.DataItem, "EducationLevel") %></td>
            <td class="col-amount"><%# DataBinder.Eval(Container.DataItem, "AmountDisplay") %></td>
            <td><span class='rv-status-badge rv-status-<%# DataBinder.Eval(Container.DataItem, "StatusCode") %>'><%# DataBinder.Eval(Container.DataItem, "StatusText") %></span></td>
            <td><%# DataBinder.Eval(Container.DataItem, "CreateDatePersian") %></td>
            <td class="col-actions">
                <asp:LinkButton ID="lnkView" runat="server" CssClass="rv-btn rv-btn-view" 
                    CommandName="ViewRequest" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CausesValidation="false">👁️ مشاهده</asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate></tbody></table></FooterTemplate>
</asp:Repeater>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlEmptyClosed" runat="server" Visible="false">
                <div class="rv-empty"><span class="icon">📭</span>هیچ درخواستی با این فیلترها یافت نشد.</div>
            </asp:Panel>
        </div>
    </div>

    <%-- ============================================================
         Modal Detail — جزئیات کامل درخواست
         ============================================================ --%>
    <div id="rvDetailModal" class="rv-modal-overlay">
        <div class="rv-modal">
            <div class="rv-modal-header">
                <h3>📄 جزئیات درخواست <span id="rvModalID"></span></h3>
                <button type="button" class="rv-modal-close" onclick="closeDetailModal();">✕</button>
            </div>
            <div class="rv-modal-body">
                <asp:Panel ID="pnlDetail" runat="server" Visible="false">

                    <%-- ✅ هشدار عدم تطابق شرایط --%>
                    <asp:Panel ID="pnlConditionAlert" runat="server" Visible="false" CssClass="rv-condition-alert">
                        <span class="alert-icon">⚠️</span>
                        <div class="alert-content">
                            <div class="alert-title">توجه: عدم تطابق شرایط دانش‌آموز/دانشجو با مقطع انتخابی</div>
                            <div class="alert-text">
                                <asp:Label ID="lblConditionAlertText" runat="server" />
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- ✅ اطلاعات کارمند --%>
                    <div class="rv-detail-section">
                        <h4>👤 اطلاعات کارمند</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">نام و نام خانوادگی</span><span class="val"><asp:Label ID="lblD_FullName" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">کد پرسنلی</span><span class="val"><asp:Label ID="lblD_PersonCode" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">واحد</span><span class="val"><asp:Label ID="lblD_UnitName" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">موبایل</span><span class="val"><asp:Label ID="lblD_Mobile" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">نوع قرارداد</span><span class="val"><asp:Label ID="lblD_ContractType" runat="server" /></span></div>
                        </div>
                    </div>

                    <%-- ✅ اطلاعات فرزند --%>
                    <div class="rv-detail-section">
                        <h4>👶 اطلاعات فرزند</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">نام فرزند</span><span class="val"><asp:Label ID="lblD_ChildName" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">کد ملی</span><span class="val"><asp:Label ID="lblD_ChildNationalCode" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">تاریخ تولد</span><span class="val"><asp:Label ID="lblD_ChildBirthDate" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">جنسیت</span><span class="val"><asp:Label ID="lblD_ChildGender" runat="server" /></span></div>
                        </div>
                    </div>

                    <%-- ✅ اطلاعات تحصیلی --%>
                    <div class="rv-detail-section">
                        <h4>📚 اطلاعات تحصیلی</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">نوع تحصیل</span><span class="val"><asp:Label ID="lblD_ChildType" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">مقطع تحصیلی</span><span class="val"><asp:Label ID="lblD_EducationLevel" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">معدل کل</span><span class="val"><asp:Label ID="lblD_Average" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">سال تحصیلی</span><span class="val"><asp:Label ID="lblD_AcademicYear" runat="server" /></span></div>
                        </div>
                    </div>

                    <%-- ✅ اطلاعات مدرسه (دانش‌آموز) --%>
                    <asp:Panel ID="pnlDetailSchool" runat="server" Visible="false" CssClass="rv-detail-section school">
                        <h4>🏫 اطلاعات مدرسه</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">نام مدرسه</span><span class="val"><asp:Label ID="lblD_SchoolName" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">نوع مدرسه</span><span class="val"><asp:Label ID="lblD_SchoolType" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">شماره تماس مدرسه</span><span class="val"><asp:Label ID="lblD_SchoolPhone" runat="server" /></span></div>
                        </div>
                    </asp:Panel>

                    <%-- ✅ اطلاعات سمپاد --%>
                    <asp:Panel ID="pnlDetailSampad" runat="server" Visible="false" CssClass="rv-detail-section sampad">
                        <h4>🏆 قبولی در سمپاد</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">وضعیت</span><span class="val">✅ بله - قبولی در مدارس استعدادهای درخشان</span></div>
                            <div class="rv-detail-item"><span class="lbl">نام مدرسه سمپاد</span><span class="val"><asp:Label ID="lblD_SampadSchoolName" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">شماره دانش‌آموزی سمپاد</span><span class="val"><asp:Label ID="lblD_SampadStudentNumber" runat="server" /></span></div>
                        </div>
                    </asp:Panel>

                    <%-- ✅ اطلاعات المپیاد (جدید) --%>
                    <asp:Panel ID="pnlDetailOlympiad" runat="server" Visible="false" CssClass="rv-detail-section olympiad">
                        <h4>🏅 قبولی در المپیاد علمی</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">وضعیت</span><span class="val"><asp:Label ID="lblD_OlympiadStatus" runat="server" Text="✅ بله - قبولی در المپیاد علمی" /></span></div>
                        </div>
                        <asp:Panel ID="pnlDocOlympiad" runat="server" Visible="false" style="margin-top:15px;">
                            <div class="rv-doc-card">
                                <span class="doc-icon">📜</span>
                                <div class="doc-title">تصویر کارنامه/مدرک قبولی المپیاد</div>
                                <div class="doc-name"><asp:Label ID="lblDocOlympiadName" runat="server" /></div>
                                <div class="rv-doc-actions">
                                    <a id="lnkViewOlympiad" runat="server" target="_blank" class="rv-doc-btn rv-doc-btn-view">👁️ مشاهده</a>
                                    <a id="lnkDownloadOlympiad" runat="server" class="rv-doc-btn rv-doc-btn-download">⬇️ دانلود</a>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                    <%-- ✅ اطلاعات استثنایی (جدید) --%>
                    <asp:Panel ID="pnlDetailExceptional" runat="server" Visible="false" CssClass="rv-detail-section exceptional">
                        <h4>🌟 درخواست استثنایی</h4>
                        <div style="font-size:13px;color:#880e4f;line-height:1.8;">
                            ✅ این درخواست به‌عنوان <strong>«درخواست استثنایی»</strong> ثبت شده است و نیاز به بررسی ویژه توسط روابط عمومی دارد.
                        </div>
                    </asp:Panel>

                    <%-- ✅ اطلاعات دانشگاه (دانشجو) --%>
                    <asp:Panel ID="pnlDetailUniversity" runat="server" Visible="false" CssClass="rv-detail-section university">
                        <h4>🎓 اطلاعات دانشگاه / مؤسسه</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">نام دانشگاه/مؤسسه</span><span class="val"><asp:Label ID="lblD_UniversityName" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">نوع دانشگاه/مؤسسه</span><span class="val"><asp:Label ID="lblD_UniversityType" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">شماره تماس دانشگاه</span><span class="val"><asp:Label ID="lblD_UniversityPhone" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">شماره دانشجویی</span><span class="val"><asp:Label ID="lblD_StudentNumber" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">نوع ورود به دانشگاه</span><span class="val"><asp:Label ID="lblD_AdmissionType" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">رتبه آزمون</span><span class="val"><asp:Label ID="lblD_RankInExam" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">دلیل پذیرش بدون آزمون</span><span class="val"><asp:Label ID="lblD_WithoutExamReason" runat="server" /></span></div>
                        </div>
                    </asp:Panel>

                    <%-- ============================================================
                         ✅ مبلغ مقطع — طراحی برجسته و شکیل
                         ============================================================ --%>
                    <asp:Panel ID="pnlDetailAmount" runat="server" Visible="false" CssClass="rv-detail-section amount">
                        <h4>💰 مبلغ قابل پرداخت برای این مقطع</h4>
                        <div class="rv-amount-display">
                            <div class="amount-level-badge">
                                🎓 مقطع: <asp:Label ID="lblAmountLevel" runat="server" />
                            </div>
                            <div class="amount-value-box">
                                <asp:Label ID="lblAmountValue" runat="server" CssClass="amount-value" />
                                <span class="amount-unit">ریال</span>
                                <span class="amount-words">
                                    <asp:Label ID="lblAmountWords" runat="server" />
                                </span>
                            </div>
                            <span class="amount-hint">💡 این مبلغ فقط برای همکاران روابط عمومی قابل مشاهده است.</span>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlDetailAmountEmpty" runat="server" Visible="false">
                        <div class="rv-amount-empty">
                            <span class="empty-icon">⚠️</span>
                            <span>مبلغ این مقطع در سیستم تعریف نشده است. لطفاً با مدیر سیستم تماس بگیرید.</span>
                        </div>
                    </asp:Panel>

                    <%-- ✅ مدارک هویتی --%>
                    <div class="rv-detail-section identity">
                        <h4>🆔 مدارک هویتی</h4>
                        <div class="rv-doc-grid">
                            <%-- شناسنامه --%>
                            <asp:Panel ID="pnlDocBirthCert" runat="server" Visible="false">
                                <div class="rv-doc-card identity-card">
                                    <span class="doc-icon">📘</span>
                                    <div class="doc-title">تصویر صفحه اول شناسنامه</div>
                                    <div class="doc-name"><asp:Label ID="lblDocBirthCertName" runat="server" /></div>
                                    <div class="rv-doc-actions">
                                        <a id="lnkViewBirthCert" runat="server" target="_blank" class="rv-doc-btn rv-doc-btn-view">👁️ مشاهده</a>
                                        <a id="lnkDownloadBirthCert" runat="server" class="rv-doc-btn rv-doc-btn-download">⬇️ دانلود</a>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDocBirthCertMissing" runat="server" Visible="false">
                                <div class="rv-doc-card identity-card missing">
                                    <span class="doc-icon">❌</span>
                                    <div class="doc-title">شناسنامه پیوست نشده</div>
                                    <div class="doc-name">این مدرک اجباری است</div>
                                </div>
                            </asp:Panel>

                            <%-- کارت ملی --%>
                            <asp:Panel ID="pnlDocNationalCard" runat="server" Visible="false">
                                <div class="rv-doc-card identity-card">
                                    <span class="doc-icon">💳</span>
                                    <div class="doc-title">تصویر کارت ملی فرزند</div>
                                    <div class="doc-name"><asp:Label ID="lblDocNationalCardName" runat="server" /></div>
                                    <div class="rv-doc-actions">
                                        <a id="lnkViewNationalCard" runat="server" target="_blank" class="rv-doc-btn rv-doc-btn-view">👁️ مشاهده</a>
                                        <a id="lnkDownloadNationalCard" runat="server" class="rv-doc-btn rv-doc-btn-download">⬇️ دانلود</a>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDocNationalCardMissing" runat="server" Visible="false">
                                <div class="rv-doc-card identity-card missing">
                                    <span class="doc-icon">❌</span>
                                    <div class="doc-title">کارت ملی پیوست نشده</div>
                                    <div class="doc-name">این مدرک اجباری است</div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <%-- ✅ مدارک تحصیلی --%>
                    <div class="rv-detail-section documents">
                        <h4>📎 مدارک تحصیلی</h4>
                        <div class="rv-doc-grid">
                            <%-- کارنامه --%>
                            <asp:Panel ID="pnlDocTranscript" runat="server" Visible="false">
                                <div class="rv-doc-card">
                                    <span class="doc-icon">📄</span>
                                    <div class="doc-title">تصویر کارنامه</div>
                                    <div class="doc-name"><asp:Label ID="lblDocTranscriptName" runat="server" /></div>
                                    <div class="rv-doc-actions">
                                        <a id="lnkViewTranscript" runat="server" target="_blank" class="rv-doc-btn rv-doc-btn-view">👁️ مشاهده</a>
                                        <a id="lnkDownloadTranscript" runat="server" class="rv-doc-btn rv-doc-btn-download">⬇️ دانلود</a>
                                    </div>
                                </div>
                            </asp:Panel>

                            <%-- کارت دانشجویی --%>
                            <asp:Panel ID="pnlDocStudentCard" runat="server" Visible="false">
                                <div class="rv-doc-card">
                                    <span class="doc-icon">🎓</span>
                                    <div class="doc-title">تصویر کارت دانشجویی</div>
                                    <div class="doc-name"><asp:Label ID="lblDocStudentCardName" runat="server" /></div>
                                    <div class="rv-doc-actions">
                                        <a id="lnkViewStudentCard" runat="server" target="_blank" class="rv-doc-btn rv-doc-btn-view">👁️ مشاهده</a>
                                        <a id="lnkDownloadStudentCard" runat="server" class="rv-doc-btn rv-doc-btn-download">⬇️ دانلود</a>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDocStudentCardMissing" runat="server" Visible="false">
                                <div class="rv-doc-card missing">
                                    <span class="doc-icon">❌</span>
                                    <div class="doc-title">کارت دانشجویی پیوست نشده</div>
                                    <div class="doc-name">برای دانشجویان اجباری است</div>
                                </div>
                            </asp:Panel>

                            <%-- مدرک اضافی --%>
                            <asp:Panel ID="pnlDocExtra" runat="server" Visible="false">
                                <div class="rv-doc-card">
                                    <span class="doc-icon">📑</span>
                                    <div class="doc-title"><asp:Label ID="lblDocExtraTitle" runat="server" Text="مدرک اضافی" /></div>
                                    <div class="doc-name"><asp:Label ID="lblDocExtraName" runat="server" /></div>
                                    <div class="rv-doc-actions">
                                        <a id="lnkViewExtra" runat="server" target="_blank" class="rv-doc-btn rv-doc-btn-view">👁️ مشاهده</a>
                                        <a id="lnkDownloadExtra" runat="server" class="rv-doc-btn rv-doc-btn-download">⬇️ دانلود</a>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDocNone" runat="server" Visible="false">
                                <div class="rv-doc-card missing">
                                    <span class="doc-icon">⚠️</span>
                                    <div class="doc-title">مدرکی پیوست نشده</div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <%-- آخرین بررسی --%>
                    <asp:Panel ID="pnlPreviousReview" runat="server" Visible="false" CssClass="rv-detail-section" 
                        style="border-right-color:#f39c12;background:#fef9e7;">
                        <h4>📝 آخرین بررسی کارشناس</h4>
                        <div class="rv-detail-grid">
                            <div class="rv-detail-item"><span class="lbl">وضعیت فعلی</span><span class="val"><asp:Label ID="lblPrevStatus" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">نام کارشناس</span><span class="val"><asp:Label ID="lblPrevReviewer" runat="server" /></span></div>
                            <div class="rv-detail-item"><span class="lbl">تاریخ بررسی</span><span class="val"><asp:Label ID="lblPrevReviewDate" runat="server" /></span></div>
                            <div class="rv-detail-item" style="grid-column:1/-1;"><span class="lbl">دلیل / توضیحات</span><span class="val"><asp:Label ID="lblPrevReason" runat="server" /></span></div>
                            <div class="rv-detail-item" style="grid-column:1/-1;"><span class="lbl">یادداشت داخلی</span><span class="val"><asp:Label ID="lblPrevComment" runat="server" /></span></div>
                        </div>
                    </asp:Panel>

                    <%-- تاریخچه بررسی‌ها --%>
                    <asp:Panel ID="pnlHistoryReview" runat="server" Visible="false" CssClass="rv-detail-section" 
                        style="border-right-color:#9b59b6;background:#f4ecf7;">
                        <h4>📋 تاریخچه بررسی‌ها</h4>
                        <asp:Repeater ID="rptReviewHistory" runat="server">
    <ItemTemplate>
        <div class="rv-history-item">
            <div class="rv-hist-header">
                <span class='rv-status-badge rv-status-<%# DataBinder.Eval(Container.DataItem, "StatusCode") %>'>
                    <%# GetStatusText(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "StatusCode"))) %>
                </span>
                <span class="rv-hist-date">
                    <%# GetHistoryDate(DataBinder.Eval(Container.DataItem, "ReviewDate"), DataBinder.Eval(Container.DataItem, "CreateDate")) %>
                </span>
            </div>
            <div class="rv-hist-row">
                <strong>👤 کارشناس:</strong> 
                <%# GetHistoryReviewer(DataBinder.Eval(Container.DataItem, "ReviewerName"), DataBinder.Eval(Container.DataItem, "ReviewerCode")) %>
            </div>
            <div class="rv-hist-row">
                <strong>📋 دلیل:</strong> 
                <%# GetHistoryReason(DataBinder.Eval(Container.DataItem, "StatusDescription")) %>
            </div>
            <div class="rv-hist-comment">
                <strong>📝 یادداشت:</strong> 
                <%# GetHistoryComment(DataBinder.Eval(Container.DataItem, "ReviewerComment")) %>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
                    </asp:Panel>

                    <%-- فرم بررسی --%>
                    <asp:Panel ID="pnlReviewForm" runat="server" CssClass="rv-review-form">
                        <h4 style="font-size:14px;font-weight:700;color:#a04000;margin:0 0 12px 0;padding-bottom:8px;border-bottom:1px dashed #f39c12;">
                            ✍️ ثبت نظر کارشناس
                        </h4>
                        <div class="rv-form-group">
                            <label>دلیل (برای رد یا نیاز به اصلاح):</label>
                            <div class="rv-reason-chips">
                                <button type="button" class="rv-chip reject-chip" onclick="selectReason(this, 'عدم تطابق شرایط دانش‌آموز/دانشجو با مقطع انتخابی');">⚠️ عدم تطابق شرایط</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'معدل کمتر از حد نصاب');">معدل کمتر از حد نصاب</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'کارنامه ناخوانا / نامعتبر');">کارنامه ناخوانا</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'عدم تطابق مقطع تحصیلی');">عدم تطابق مقطع</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'مدرک پذیرش دانشگاه ناقص');">مدرک دانشگاه ناقص</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'رتبه آزمون نامعتبر');">رتبه نامعتبر</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'شناسنامه یا کارت ملی پیوست نشده');">مدارک هویتی ناقص</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'کارت دانشجویی پیوست نشده');">کارت دانشجویی ناقص</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'مدرک المپیاد پیوست نشده');">مدرک المپیاد ناقص</button>
                                <button type="button" class="rv-chip" onclick="selectReason(this, 'سایر (توضیح دهید)');">سایر</button>
                            </div>
                            <asp:TextBox ID="txtReviewReason" runat="server" CssClass="rv-textarea" 
                                TextMode="MultiLine" Rows="3" placeholder="دلیل را انتخاب کنید یا بنویسید..."
                                oninput="syncHiddenReason(this);" />
                        </div>
                        <div class="rv-form-group">
                            <label>یادداشت داخلی (اختیاری):</label>
                            <asp:TextBox ID="txtReviewerComment" runat="server" CssClass="rv-textarea" 
                                TextMode="MultiLine" Rows="2" placeholder="یادداشت داخلی..."
                                oninput="syncHiddenComment(this);" />
                        </div>
                    </asp:Panel>

                </asp:Panel>
                <asp:Panel ID="pnlDetailEmpty" runat="server" Visible="true">
                    <div class="rv-empty"><span class="icon">📄</span>درخواستی انتخاب نشده است.</div>
                </asp:Panel>
            </div>
            
            <div class="rv-modal-footer">
                <asp:LinkButton ID="btnCloseFooter" runat="server" CssClass="rv-action-btn rv-action-cancel"
                    OnClientClick="closeDetailModal(); return false;" CausesValidation="false" Text="بستن" />
                <asp:Panel ID="pnlReviewButtons" runat="server" CssClass="rv-modal-footer-actions">
                    <asp:LinkButton ID="btnCorrection" runat="server" CssClass="rv-action-btn rv-action-correction"
                        OnClick="btnCorrection_Click" CausesValidation="false" Text="📝 نیاز به اصلاح"
                        OnClientClick="syncAllHidden();" />
                    <asp:LinkButton ID="btnReject" runat="server" CssClass="rv-action-btn rv-action-reject"
                        OnClick="btnReject_Click" CausesValidation="false" Text="❌ عدم تایید"
                        OnClientClick="syncAllHidden();" />
                    <asp:LinkButton ID="btnApprove" runat="server" CssClass="rv-action-btn rv-action-approve"
                        OnClick="btnApprove_Click" CausesValidation="false" Text="✅ تایید"
                        OnClientClick="syncAllHidden();" />
                </asp:Panel>
            </div>
        </div>
    </div>

    <%-- Alert --%>
    <div id="rvAlert" class="rv-alert-overlay">
        <div class="rv-alert">
            <div id="rvAlertHeader" class="rv-alert-header info">
                <span id="rvAlertIcon" class="rv-alert-icon">ℹ️</span>
                <h3 id="rvAlertTitle" class="rv-alert-title">اطلاع</h3>
            </div>
            <div id="rvAlertBody" class="rv-alert-body"></div>
            <div class="rv-alert-footer">
                <button type="button" class="rv-alert-btn" onclick="closeAlert();">متوجه شدم</button>
            </div>
        </div>
    </div>

    <%-- Toast --%>
    <div id="rvToastContainer" class="rv-toast-container"></div>

    <%-- HiddenFields --%>
    <asp:HiddenField ID="hfSelectedRequestID" runat="server" Value="0" />
    <asp:HiddenField ID="hfShowModal" runat="server" Value="0" />
    <asp:HiddenField ID="hfReviewReason" runat="server" Value="" />
    <asp:HiddenField ID="hfReviewerComment" runat="server" Value="" />

    <script type="text/javascript">
        // ============================================================
        // انتقال Modal/Alert/Toast به body
        // ============================================================
        (function () {
            function relocateToBody() {
                try {
                    var ids = ['rvDetailModal', 'rvAlert', 'rvToastContainer'];
                    for (var i = 0; i < ids.length; i++) {
                        var el = document.getElementById(ids[i]);
                        if (el && el.parentNode && el.parentNode !== document.body) {
                            document.body.appendChild(el);
                        }
                    }
                } catch (e) { console.error('relocate error:', e); }
            }
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', relocateToBody);
            } else { relocateToBody(); }
        })();

        // ============================================================
        // sync توابع HiddenField
        // ============================================================
        function syncHiddenReason(el) {
            try {
                var hf = document.getElementById('<%= hfReviewReason.ClientID %>');
                if (hf) hf.value = el.value || '';
            } catch (e) { }
        }
        function syncHiddenComment(el) {
            try {
                var hf = document.getElementById('<%= hfReviewerComment.ClientID %>');
                if (hf) hf.value = el.value || '';
            } catch (e) { }
        }
        function syncAllHidden() {
            try {
                var txtR = document.getElementById('<%= txtReviewReason.ClientID %>');
                var hfR = document.getElementById('<%= hfReviewReason.ClientID %>');
                if (txtR && hfR) hfR.value = txtR.value || '';

                var txtC = document.getElementById('<%= txtReviewerComment.ClientID %>');
                var hfC = document.getElementById('<%= hfReviewerComment.ClientID %>');
                if (txtC && hfC) hfC.value = txtC.value || '';
            } catch (e) { }
            return true;
        }

        // ============================================================
        // ✅ آماده‌سازی برای خروجی Excel
        // ============================================================
        function prepareExport() {
            try {
                closeDetailModal();

                var hfShow = document.getElementById('<%= hfShowModal.ClientID %>');
                if (hfShow) hfShow.value = '0';

                try { sessionStorage.setItem('exportInProgress', '1'); } catch (e) { }

                return true;
            } catch (e) {
                console.error('prepareExport error:', e);
                return true;
            }
        }

        // ============================================================
        // Tab
        // ============================================================
        function switchTab(tabName) {
            var tabs = document.querySelectorAll('.rv-tab-content');
            for (var i = 0; i < tabs.length; i++) tabs[i].classList.remove('active');
            var tabButtons = document.querySelectorAll('.rv-tab');
            for (var i = 0; i < tabButtons.length; i++) tabButtons[i].classList.remove('active');
            var activeContent = document.getElementById('tab_' + tabName);
            if (activeContent) activeContent.classList.add('active');
            var activeBtn = document.getElementById('btn_tab_' + tabName);
            if (activeBtn) activeBtn.classList.add('active');
            var hf = document.getElementById('<%= hfActiveTab.ClientID %>');
            if (hf) hf.value = tabName;
        }

        // ============================================================
        // Modal
        // ============================================================
        function openDetailModal(requestID) {
            var modal = document.getElementById('rvDetailModal');
            if (!modal) return;
            if (modal.parentNode !== document.body) {
                document.body.appendChild(modal);
            }
            var idSpan = document.getElementById('rvModalID');
            if (idSpan && requestID) idSpan.innerText = '#' + requestID;
            modal.classList.add('show');
            document.body.style.overflow = 'hidden';
            try {
                var txtR = document.getElementById('<%= txtReviewReason.ClientID %>');
                var hfR = document.getElementById('<%= hfReviewReason.ClientID %>');
                if (txtR && hfR) hfR.value = txtR.value || '';

                var txtC = document.getElementById('<%= txtReviewerComment.ClientID %>');
                var hfC = document.getElementById('<%= hfReviewerComment.ClientID %>');
                if (txtC && hfC) hfC.value = txtC.value || '';
            } catch (e) { }
        }

        function closeDetailModal() {
            var modal = document.getElementById('rvDetailModal');
            if (!modal) return;
            modal.classList.remove('show');
            document.body.style.overflow = '';
        }

        // ============================================================
        // Alert
        // ============================================================
        function showAlert(type, title, bodyHtml) {
            var alert = document.getElementById('rvAlert');
            var header = document.getElementById('rvAlertHeader');
            var icon = document.getElementById('rvAlertIcon');
            var titleEl = document.getElementById('rvAlertTitle');
            var body = document.getElementById('rvAlertBody');
            if (!alert) return;
            if (alert.parentNode !== document.body) {
                document.body.appendChild(alert);
            }
            header.className = 'rv-alert-header ' + type;
            var icons = { 'success': '✅', 'error': '❌', 'warning': '⚠️', 'info': 'ℹ️' };
            icon.innerText = icons[type] || 'ℹ️';
            titleEl.innerText = title;
            body.innerHTML = bodyHtml;
            alert.classList.add('show');
        }

        function closeAlert() {
            var alert = document.getElementById('rvAlert');
            if (alert) alert.classList.remove('show');
        }

        // ============================================================
        // Toast
        // ============================================================
        function showToast(type, message, duration) {
            var container = document.getElementById('rvToastContainer');
            if (!container) return;
            if (container.parentNode !== document.body) {
                document.body.appendChild(container);
            }
            var toast = document.createElement('div');
            toast.className = 'rv-toast ' + type;
            var icons = { 'success': '✅', 'error': '❌', 'warning': '⚠️', 'info': 'ℹ️' };
            toast.innerHTML = '<span class="rv-toast-icon">' + (icons[type] || 'ℹ️') + '</span>' +
                             '<span class="rv-toast-message">' + message + '</span>';
            container.appendChild(toast);
            setTimeout(function () { if (toast.parentElement) toast.remove(); }, duration || 5000);
        }

        // ============================================================
        // Reason Chips
        // ============================================================
        function selectReason(btn, text) {
            var textarea = document.getElementById('<%= txtReviewReason.ClientID %>');
            var hfReason = document.getElementById('<%= hfReviewReason.ClientID %>');
            if (!textarea) return;
            var wasSelected = btn.classList.contains('selected');
            var allChips = document.querySelectorAll('.rv-chip');
            for (var i = 0; i < allChips.length; i++) allChips[i].classList.remove('selected');
            if (!wasSelected) {
                btn.classList.add('selected');
                textarea.value = text;
                if (hfReason) hfReason.value = text;
            } else {
                textarea.value = '';
                if (hfReason) hfReason.value = '';
            }
        }

        // ============================================================
        // Escape
        // ============================================================
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') { closeAlert(); closeDetailModal(); }
        });

        // ============================================================
        // باز کردن Modal بعد از PostBack
        // ============================================================
        (function () {
            function tryOpenModal() {
                try {
                    var exportFlag = null;
                    try { exportFlag = sessionStorage.getItem('exportInProgress'); } catch (e) { }

                    if (exportFlag === '1') {
                        try { sessionStorage.removeItem('exportInProgress'); } catch (e) { }
                        var hfShowReset = document.getElementById('<%= hfShowModal.ClientID %>');
                        if (hfShowReset) hfShowReset.value = '0';
                        return;
                    }

                    var hfShow = document.getElementById('<%= hfShowModal.ClientID %>');
                    var hfID = document.getElementById('<%= hfSelectedRequestID.ClientID %>');
                    if (hfShow && hfShow.value === '1') {
                        openDetailModal(hfID ? hfID.value : '');
                    }
                } catch (e) {
                    console.error('tryOpenModal error:', e);
                }
            }
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', tryOpenModal);
            } else { tryOpenModal(); }
        })();

        // ============================================================
        // انتخاب تب فعال
        // ============================================================
        (function () {
            function onLoad() {
                var hf = document.getElementById('<%= hfActiveTab.ClientID %>');
                if (hf && hf.value) switchTab(hf.value);
            }
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', onLoad);
            } else { onLoad(); }
        })();

        // ============================================================
        // بعد از هر PostBack
        // ============================================================
        if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                try {
                    var ids = ['rvDetailModal', 'rvAlert', 'rvToastContainer'];
                    for (var i = 0; i < ids.length; i++) {
                        var el = document.getElementById(ids[i]);
                        if (el && el.parentNode && el.parentNode !== document.body) {
                            document.body.appendChild(el);
                        }
                    }
                } catch (e) { }
            });
        }
    </script>

</asp:Content>