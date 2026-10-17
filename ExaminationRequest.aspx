<%@ Page Title="ثبت درخواست معاینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExaminationRequest.aspx.vb" Inherits="ExaminationRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .exm-container {
            max-width: 1100px;
            margin: 30px auto 30px auto;
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
            margin-bottom: 15px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e9ecef;
        }
        
        .exm-btn-help {
            background: #17a2b8;
            color: #ffffff;
            padding: 8px 20px;
            border: none;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            font-family: inherit;
        }
        .exm-btn-help:hover {
            background: #138496;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(23, 162, 184, 0.3);
        }
        
        /* ============================================ */
        /* باکس مهلت رزرو */
        /* ============================================ */
        .exm-deadline-box {
            background: #e8f4fd;
            border: 2px solid #3498db;
            border-radius: 12px;
            padding: 15px 20px;
            margin-bottom: 20px;
            text-align: center;
        }
        .exm-deadline-box .deadline-icon {
            font-size: 30px;
            display: block;
            margin-bottom: 5px;
        }
        .exm-deadline-box .deadline-title {
            color: #0c5460;
            font-size: 14px;
            font-weight: 600;
            margin-bottom: 5px;
        }
        .exm-deadline-box .deadline-date {
            color: #e74c3c;
            font-size: 20px;
            font-weight: bold;
            direction: ltr;
            display: inline-block;
        }
        .exm-deadline-box .deadline-expired {
            color: #e74c3c;
            font-size: 14px;
            font-weight: bold;
            display: block;
            margin-top: 5px;
        }
        .exm-deadline-box .deadline-ok {
            color: #27ae60;
            font-size: 13px;
            display: block;
            margin-top: 5px;
        }
        .exm-deadline-box.expired {
            background: #f8d7da;
            border-color: #e74c3c;
        }
        .exm-deadline-box.expired .deadline-title {
            color: #721c24;
        }
        
        /* ============================================ */
        /* هشدار مهم */
        /* ============================================ */
        .exm-warning-box {
            background: #fff3cd;
            border: 2px solid #ffc107;
            border-radius: 12px;
            padding: 15px 20px;
            margin-bottom: 20px;
            text-align: center;
        }
        .exm-warning-box .warning-icon {
            font-size: 30px;
            display: block;
            margin-bottom: 5px;
        }
        .exm-warning-box .warning-title {
            color: #856404;
            font-size: 15px;
            font-weight: 700;
            margin-bottom: 5px;
        }
        .exm-warning-box .warning-text {
            color: #856404;
            font-size: 13px;
            line-height: 1.8;
        }
        .exm-warning-box .warning-text strong {
            color: #e74c3c;
        }
        
        .exm-form-group {
            margin-bottom: 18px;
        }
        .exm-form-group > label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 10px;
            font-size: 15px;
        }
        
        /* ============================================ */
        /* کارت‌های تاریخ */
        /* ============================================ */
        .exm-dates-container {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(230px, 1fr));
            gap: 15px;
            margin-top: 10px;
            padding: 5px;
        }

        .exm-date-card {
            background: #ffffff;
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            padding: 15px;
            cursor: pointer;
            transition: all 0.3s;
            position: relative;
            text-align: center;
        }

        .exm-date-card:hover:not(.full) {
            border-color: #3498db;
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(52,152,219,0.2);
        }

        .exm-date-card.selected {
            border-color: #27ae60;
            background: #e8f8f0;
            box-shadow: 0 5px 15px rgba(39,174,96,0.3);
        }

        .exm-date-card.full {
            background: #f8d7da;
            border-color: #e74c3c;
            cursor: not-allowed;
            opacity: 0.75;
        }

        .exm-date-header {
            border-bottom: 2px solid #e9ecef;
            padding-bottom: 10px;
            margin-bottom: 10px;
        }

        .exm-date-day {
            font-size: 13px;
            color: #6c757d;
            margin-bottom: 5px;
            font-weight: 600;
        }

        .exm-date-value {
            font-size: 18px;
            font-weight: 700;
            color: #2c3e50;
            direction: ltr;
        }

        .exm-date-capacity {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 5px 0;
            font-size: 12px;
        }

        .capacity-label {
            color: #6c757d;
        }

        .capacity-value {
            font-weight: 700;
            color: #27ae60;
        }

        .exm-date-card.full .capacity-value {
            color: #e74c3c;
        }

        .exm-date-units {
            margin: 8px 0;
            padding: 8px 0;
            border-top: 1px dashed #e9ecef;
            border-bottom: 1px dashed #e9ecef;
            max-height: 80px;
            overflow-y: auto;
        }

        .exm-date-units .unit-item {
            display: inline-block;
            background: #e8f4fd;
            color: #0c5460;
            padding: 2px 8px;
            border-radius: 10px;
            font-size: 10px;
            margin: 2px;
            font-weight: 600;
        }

        .exm-date-units .unit-item.full {
            background: #f8d7da;
            color: #721c24;
            text-decoration: line-through;
        }

        .exm-date-status {
            margin-top: 8px;
        }

        .badge-full {
            background: #e74c3c;
            color: white;
            padding: 4px 12px;
            border-radius: 12px;
            font-size: 11px;
            font-weight: 700;
            display: inline-block;
        }

        .badge-available {
            background: #27ae60;
            color: white;
            padding: 4px 12px;
            border-radius: 12px;
            font-size: 11px;
            font-weight: 700;
            display: inline-block;
        }

        /* ============================================ */
        /* اطلاعات تاریخ انتخاب شده */
        /* ============================================ */
        .exm-selected-date-info {
            margin: 15px 0;
        }

        .selected-date-info {
            background: #d4edda;
            border: 2px solid #27ae60;
            border-radius: 10px;
            padding: 12px 20px;
            display: flex;
            align-items: center;
            gap: 10px;
            font-size: 14px;
        }

        .selected-date-info .icon {
            font-size: 20px;
        }

        .selected-date-info strong {
            color: #27ae60;
            font-size: 16px;
        }

        /* ============================================ */
        /* دکمه‌های ثبت و بازگشت - بعد از تقویم */
        /* ============================================ */
        .exm-btn-group {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            position: relative;
            margin-top: 25px;
            margin-bottom: 10px;
            padding: 15px 0;
            background: transparent;
            border-top: 2px solid #e9ecef;
            direction: rtl;
            justify-content: center;
            z-index: 1;
        }
        
        .exm-btn {
            padding: 12px 40px;
            border: none;
            border-radius: 25px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            min-width: 180px;
            text-align: center;
            font-family: inherit;
        }
        .exm-btn:hover { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .exm-btn-submit { background: #27ae60; color: #ffffff; }
        .exm-btn-submit:hover { background: #219a52; }
        .exm-btn-submit:disabled { opacity: 0.6; cursor: not-allowed; transform: none !important; background: #95a5a6; }
        .exm-btn-back { background: #9b59b6; color: #ffffff; }
        .exm-btn-back:hover { background: #8e44ad; }
        
        /* ============================================ */
        /* هشدارها */
        /* ============================================ */
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
        
        /* ============================================ */
        /* تاریخچه */
        /* ============================================ */
        .exm-history {
            margin-top: 25px;
            padding-top: 20px;
            border-top: 2px solid #e9ecef;
        }
        .exm-history h4 {
            color: #2c3e50;
            margin-bottom: 12px;
            font-size: 15px;
            font-weight: 600;
        }
        .exm-history-item {
            background: #f8f9fa;
            padding: 12px 15px;
            border-radius: 10px;
            margin-bottom: 10px;
            border-right: 3px solid #3498db;
            font-size: 13px;
        }
        .exm-history-item .exm-history-date { 
            color: #6c757d; 
            font-size: 12px;
        }
        .exm-history-item .exm-history-status { 
            font-weight: 700; 
            color: #2c3e50;
            font-size: 13px;
        }
        .exm-history-item .exm-history-status-row {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 8px;
            flex-wrap: wrap;
            margin-bottom: 10px;
            padding-bottom: 8px;
            border-bottom: 1px dashed #dee2e6;
        }
        
        /* باکس برجسته تاریخ معاینه */
        .exm-history-item .exm-history-exam-date {
            background: #fff3cd;
            border: 1px dashed #ffc107;
            border-radius: 8px;
            padding: 8px 14px;
            margin: 8px 0;
            font-size: 13px;
            color: #856404;
            display: flex;
            align-items: center;
            gap: 8px;
            flex-wrap: wrap;
        }
        .exm-history-item .exm-history-exam-date .label {
            color: #856404;
            font-size: 13px;
            font-weight: 600;
        }
        .exm-history-item .exm-history-exam-date .value {
            font-weight: 900;
            color: #e74c3c;
            font-size: 16px;
            direction: ltr;
            letter-spacing: 0.5px;
            background: #ffffff;
            padding: 3px 12px;
            border-radius: 6px;
            border: 1px solid #e74c3c;
        }
        
        .exm-history-item .exm-history-supervisor {
            margin-top: 8px;
            font-size: 13px;
            color: #495057;
            display: flex;
            align-items: center;
            gap: 8px;
            flex-wrap: wrap;
        }
        
        .exm-status-badge {
            display: inline-block;
            padding: 3px 14px;
            border-radius: 15px;
            font-size: 12px;
            font-weight: 600;
        }
        .exm-status-pending { background: #fff3cd; color: #856404; }
        .exm-status-approved { background: #d4edda; color: #155724; }
        .exm-status-rejected { background: #f8d7da; color: #721c24; }
        .exm-status-done { background: #cce5ff; color: #004085; }
        
        .exm-no-data {
            text-align: center;
            padding: 30px;
            color: #6c757d;
        }
        .exm-no-data .icon { font-size: 40px; margin-bottom: 10px; display: block; }
        
        /* ============================================ */
        /* لودینگ Overlay */
        /* ============================================ */
        .exm-loading-overlay {
            display: none;
            position: fixed !important;
            top: 0 !important;
            left: 0 !important;
            right: 0 !important;
            bottom: 0 !important;
            width: 100vw !important;
            height: 100vh !important;
            background: rgba(0, 0, 0, 0.7) !important;
            z-index: 999999 !important;
            justify-content: center !important;
            align-items: center !important;
            flex-direction: column !important;
            margin: 0 !important;
            padding: 0 !important;
            direction: rtl !important;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif !important;
        }

        .exm-loading-overlay.show {
            display: flex !important;
        }

        .exm-loading-content {
            background: #ffffff;
            padding: 35px 45px;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.4);
            text-align: center;
            min-width: 380px;
            max-width: 90%;
            animation: exm-fadeIn 0.3s ease;
        }

        @keyframes exm-fadeIn {
            from { opacity: 0; transform: scale(0.9); }
            to { opacity: 1; transform: scale(1); }
        }

        .exm-loading-spinner {
            width: 70px;
            height: 70px;
            border: 6px solid #e0e0e0;
            border-top-color: #3498db;
            border-radius: 50%;
            animation: exm-spin 0.8s linear infinite;
            margin: 0 auto 20px auto;
        }

        @keyframes exm-spin {
            to { transform: rotate(360deg); }
        }

        .exm-loading-text {
            color: #2c3e50;
            font-size: 17px;
            font-weight: 700;
            text-align: center;
            margin-bottom: 8px;
        }

        .exm-loading-subtext {
            color: #6c757d;
            font-size: 13px;
            text-align: center;
            margin-bottom: 20px;
        }

        .exm-loading-progress {
            width: 100%;
            height: 6px;
            background: #e9ecef;
            border-radius: 3px;
            overflow: hidden;
            margin-bottom: 20px;
            direction: ltr;
        }

        .exm-loading-progress-bar {
            width: 0%;
            height: 100%;
            background: linear-gradient(90deg, #3498db, #27ae60);
            border-radius: 3px;
            animation: exm-progress 3s ease-in-out infinite;
        }

        @keyframes exm-progress {
            0% { width: 0%; }
            50% { width: 70%; }
            100% { width: 100%; }
        }

        .exm-loading-steps {
            display: flex;
            justify-content: space-between;
            gap: 10px;
            flex-wrap: wrap;
            direction: rtl;
        }

        .exm-step {
            font-size: 11px;
            color: #adb5bd;
            padding: 5px 12px;
            border-radius: 12px;
            background: #f8f9fa;
            transition: all 0.3s;
            flex: 1;
            text-align: center;
            white-space: nowrap;
        }

        .exm-step.active {
            background: #d4edda;
            color: #155724;
            font-weight: 600;
        }

        .exm-btn-loading {
            position: relative;
            pointer-events: none;
            opacity: 0.8;
        }

        .exm-btn-loading::after {
            content: '';
            position: absolute;
            width: 16px;
            height: 16px;
            top: 50%;
            right: 15px;
            margin-top: -8px;
            border: 2px solid #ffffff;
            border-radius: 50%;
            border-top-color: transparent;
            animation: exm-spin 0.6s linear infinite;
        }
        
        /* ============================================ */
        /* استایل SweetAlert */
        /* ============================================ */
        .success-popup {
            width: 450px !important;
            max-width: 90% !important;
            padding: 30px 25px !important;
            border-radius: 20px !important;
            direction: rtl !important;
            font-family: 'Tahoma', 'IranSans', sans-serif !important;
        }

        .success-popup .swal2-icon {
            width: 80px !important;
            height: 80px !important;
            margin: 10px auto 20px auto !important;
            border-width: 4px !important;
        }

        .success-popup .swal2-icon .swal2-icon-content {
            font-size: 60px !important;
        }

        .success-popup .swal2-title {
            font-size: 22px !important;
            font-weight: bold !important;
            color: #27ae60 !important;
            padding: 0 0 15px 0 !important;
        }

        .success-popup .swal2-html-container {
            font-size: 14px !important;
            line-height: 2 !important;
            color: #444 !important;
            text-align: center !important;
            padding: 10px 5px !important;
        }

        .success-popup .swal2-confirm {
            font-size: 15px !important;
            padding: 12px 40px !important;
            border-radius: 25px !important;
            background: linear-gradient(135deg, #27ae60 0%, #219a52 100%) !important;
            font-weight: bold !important;
            box-shadow: 0 5px 15px rgba(39, 174, 96, 0.4) !important;
        }
        
        .help-popup {
            width: 450px !important;
            max-width: 90% !important;
            padding: 20px !important;
            border-radius: 12px !important;
            direction: rtl !important;
            font-family: 'Tahoma', 'IranSans', sans-serif !important;
        }
        .help-popup .swal2-title {
            font-size: 18px !important;
            font-weight: bold !important;
            color: #2c3e50 !important;
            padding-bottom: 15px !important;
            border-bottom: 2px solid #17a2b8 !important;
        }
        .help-popup .swal2-html-container {
            font-size: 13px !important;
            line-height: 1.8 !important;
            color: #444 !important;
            text-align: right !important;
            padding: 15px 5px !important;
        }
        .help-popup .swal2-confirm {
            font-size: 13px !important;
            padding: 8px 30px !important;
            border-radius: 8px !important;
            background: #17a2b8 !important;
        }
        
        @media (max-width: 600px) {
            .exm-container { padding: 15px; margin: 10px 10px 30px 10px; }
            .exm-btn { flex: 1 1 100%; min-width: auto; }
            .exm-warning-box { padding: 12px; }
            .exm-deadline-box { padding: 12px; }
            .exm-loading-content { padding: 30px 25px; min-width: 280px; }
            .exm-dates-container { grid-template-columns: 1fr; }
            .exm-btn-group { padding: 10px 0; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
        <div class="exm-title">📋 ثبت درخواست معاینه دوره‌ای</div>
        
        <!-- دکمه راهنما -->
        <div style="text-align: left; margin-bottom: 15px;">
            <asp:Button ID="btnHelp" runat="server" Text="❓ راهنما" 
                CssClass="exm-btn-help" OnClientClick="showHelp(); return false;" />
        </div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='exm-alert exm-alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- باکس مهلت رزرو -->
        <asp:Panel ID="pnlDeadline" runat="server" Visible="false" CssClass="exm-deadline-box">
            <span class="deadline-icon">⏰</span>
            <div class="deadline-title">مهلت رزرو برای واحد شما:</div>
            <asp:Label ID="lblDeadlineDate" runat="server" CssClass="deadline-date" />
            <asp:Label ID="lblDeadlineStatus" runat="server" />
        </asp:Panel>
        
       <!-- هشدار مهم -->
<div class="exm-warning-box">
    <span class="warning-icon">⚠️</span>
    <div class="warning-title">توجه مهم</div>
    <div class="warning-text">
        همکاران محترم لطفا به نکات ذیل توجه فرمائید:
    </div>
    
    <div style="background:#fff; border:1px dashed #e74c3c; border-radius:8px; padding:10px 15px; margin-top:12px; color:#e74c3c; font-weight:bold; font-size:13px; line-height:1.8;">
        <ol style="margin:0; padding-right:20px;">
            <li>تاریخ انتخابی به منزله <strong>تاریخ نهایی</strong> بوده و <strong>امکان تغییر یا ابطال وجود ندارد</strong>.</li>
            <li>توجه نمایید که هر پرسنل تنها <strong>یک بار در سال</strong> مجاز به رزرو معاینه دوره‌ای می‌باشد.</li>
            <li>نسبت به پیگیری تائید نهایی تاریخ انتخابی توسط رئیس مربوطه اقدام نمایید و در صورت عدم تائید، تاریخ انتخابی به منزله تاریخ معاینه شما محسوب نمی‌شود.</li>
        </ol>
    </div>
</div>
        
        <!-- ============================================ -->
        <!-- انتخاب تاریخ معاینه (کارت‌های تاریخ) -->
        <!-- ============================================ -->
        <div class="exm-form-group">
            <label>📅 انتخاب تاریخ معاینه <span class="exm-required">*</span></label>
            
            <!-- نمایش کارت‌ها -->
            <div class="exm-dates-container" id="datesContainer">
                <asp:Repeater ID="rptDates" runat="server" 
                    OnItemDataBound="rptDates_ItemDataBound">
                    <ItemTemplate>
                        <div class='<%# GetDateCardClass(Eval("IsFull")) %>' 
                             data-date='<%# Eval("GregorianDate") %>'
                             data-persian='<%# Eval("PersianDateShort") %>'
                             onclick='<%# IIf(Convert.ToBoolean(Eval("IsFull")), "", "selectDate(this, """ & Eval("GregorianDate") & """, """ & Eval("PersianDateShort") & """);") %>'>
                            
                            <div class="exm-date-header">
                                <div class="exm-date-day"><%# Eval("DayName") %></div>
                                <div class="exm-date-value"><%# Eval("PersianDateShort") %></div>
                            </div>
                            
                            <div class="exm-date-capacity">
                                <span class="capacity-label">ظرفیت خالی:</span>
                                <span class="capacity-value"><%# Eval("TotalAvailable") %> نفر</span>
                            </div>
                            
                            <div class="exm-date-units">
                                <%# GetUnitsListHtml(Eval("UnitsList")) %>
                            </div>
                            
                            <div class="exm-date-status">
                                <span class='<%# GetStatusBadgeClass(Eval("IsFull")) %>'>
                                    <%# IIf(Convert.ToBoolean(Eval("IsFull")), "❌ تکمیل ظرفیت", "✅ قابل انتخاب") %>
                                </span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            
            <asp:HiddenField ID="hdnSelectedDate" runat="server" Value="" />
            <asp:HiddenField ID="hdnSelectedPersianDate" runat="server" Value="" />
            
            <asp:Panel ID="pnlNoDates" runat="server" Visible="false">
                <div class="exm-alert exm-alert-warning">
                    <span class="icon">⚠️</span>
                    <span>هیچ تاریخی برای معاینه تعریف نشده است.</span>
                </div>
            </asp:Panel>
        </div>

        <!-- نمایش اطلاعات تاریخ انتخاب شده -->
        <asp:Panel ID="pnlSelectedDate" runat="server" Visible="false" CssClass="exm-selected-date-info">
            <div class="selected-date-info">
                <span class="icon">✅</span>
                <span class="text">تاریخ انتخابی شما:</span>
                <strong class="date"><asp:Label ID="lblSelectedDate" runat="server" /></strong>
            </div>
        </asp:Panel>

        <!-- ============================================ -->
        <!-- دکمه‌های ثبت و بازگشت - بعد از تقویم، قبل از تاریخچه -->
        <!-- ============================================ -->
        <div class="exm-btn-group">
            <asp:Button ID="btnSubmit" runat="server" Text="📤 ثبت درخواست" CssClass="exm-btn exm-btn-submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="exm-btn exm-btn-back" OnClick="btnBack_Click" />
        </div>
        
        <!-- تاریخچه درخواست‌های قبلی -->
        <div class="exm-history">
            <h4>📜 تاریخچه درخواست‌های شما</h4>
            <asp:Panel ID="pnlHistory" runat="server" Visible="false">
                <asp:Repeater ID="rptHistory" runat="server">
    <ItemTemplate>
        <div class="exm-history-item">
            <!-- ردیف اول: وضعیت + تاریخ ثبت -->
         <%--   <div class="exm-history-status-row">
                <span class="exm-history-status"><%# Eval("StatusText") %></span>
                <%--<span class="exm-history-date"><%# Eval("CreatePersianDate") %></span>--%>
            <%--</div>--%>

            <!-- تاریخ معاینه بولد و برجسته -->
            <div class="exm-history-exam-date">
                <span class="label">📅 تاریخ معاینه شما:</span>
                <span class="value"><%# Eval("ExamPersianDateShort") %></span>
            </div>

            <!-- وضعیت -->
            <div class="exm-history-supervisor">
                وضعیت: 
                <span class="exm-status-badge exm-status-<%# Eval("StatusClass") %>">
                    <%# Eval("StatusText") %>
                </span>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="pnlNoHistory" runat="server" Visible="true">
                <div class="exm-no-data">
                    <span class="icon">📭</span>
                    <p>هیچ درخواستی ثبت نکرده‌اید</p>
                </div>
            </asp:Panel>
        </div>
    </div>
    
    <!-- ============================================ -->
    <!-- لودینگ Overlay -->
    <!-- ============================================ -->
    <div id="loadingOverlay" class="exm-loading-overlay">
        <div class="exm-loading-content">
            <div class="exm-loading-spinner"></div>
            <div class="exm-loading-text">⏳ در حال بررسی از سامانه جامع منابع انسانی...</div>
            <div class="exm-loading-subtext">لطفاً صفحه را نبندید</div>
            <div class="exm-loading-progress">
                <div class="exm-loading-progress-bar"></div>
            </div>
            <div class="exm-loading-steps">
                <span class="exm-step active">✓ بررسی ظرفیت</span>
                <span class="exm-step">⏳ دریافت اطلاعات سرپرست</span>
                <span class="exm-step">⏳ ثبت نهایی</span>
            </div>
        </div>
    </div>
    
    <asp:HiddenField ID="hdnPersonCode" runat="server" />
    <asp:HiddenField ID="hdnSupervisorCode" runat="server" />
    <asp:HiddenField ID="hdnOmurID" runat="server" />
    <asp:HiddenField ID="hdnMobile" runat="server" />
    <asp:HiddenField ID="hdnUnitCode" runat="server" />
    <asp:HiddenField ID="hdnUnitID" runat="server" />
    
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var overlay = document.getElementById('loadingOverlay');
            if (overlay && overlay.parentNode !== document.body) {
                document.body.appendChild(overlay);
            }
        });

        function showLoading() {
            var overlay = document.getElementById('loadingOverlay');
            if (overlay) {
                overlay.classList.add('show');
            }
        }

        function hideLoading() {
            var overlay = document.getElementById('loadingOverlay');
            if (overlay) {
                overlay.classList.remove('show');
            }
        }

        function selectDate(cardElement, gregorianDate, persianDate) {
            var allCards = document.querySelectorAll('.exm-date-card');
            for (var i = 0; i < allCards.length; i++) {
                allCards[i].classList.remove('selected');
            }

            cardElement.classList.add('selected');

            var hdnDate = document.getElementById('<%= hdnSelectedDate.ClientID %>');
            var hdnPersianDate = document.getElementById('<%= hdnSelectedPersianDate.ClientID %>');

            if (hdnDate) hdnDate.value = gregorianDate;
            if (hdnPersianDate) hdnPersianDate.value = persianDate;

            var pnlSelectedDate = document.getElementById('<%= pnlSelectedDate.ClientID %>');
            var lblSelectedDate = document.getElementById('<%= lblSelectedDate.ClientID %>');

            if (pnlSelectedDate) {
                pnlSelectedDate.style.display = 'block';
            }

            if (lblSelectedDate) {
                lblSelectedDate.textContent = persianDate;
            }
        }

        // ============================================
        // نمایش پیام موفقیت
        // ============================================
        function showSuccessMessage(requestID) {
            var successContent =
                '<div style="text-align:center; direction:rtl;">' +
                '<p style="font-size:15px; color:#2c3e50; margin-bottom:15px;">' +
                'درخواست معاینه شما با موفقیت ثبت شد' +
                '</p>' +
                '<div style="background:#e8f4fd; padding:12px 20px; border-radius:10px; margin:15px 0;">' +
                '<p style="font-size:13px; color:#0c5460; margin:0;">' +
                '<strong>شماره پیگیری:</strong> ' + requestID +
                '</p>' +
                '</div>' +
                '<div style="background:#fff3cd; padding:12px 15px; border-radius:10px; margin:15px 0;">' +
                '<p style="font-size:13px; color:#856404; margin:0; line-height:1.8;">' +
                '📋 درخواست شما برای بررسی به <strong>سرپرست</strong> ارسال شد.' +
                '</p>' +
                '</div>' +
                '<p style="font-size:12px; color:#e74c3c; margin-top:15px;">' +
                '⚠️ پس از تایید سرپرست، امکان تغییر یا ابطال وجود ندارد.' +
                '</p>' +
                '</div>';

            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '✅ ثبت موفق',
                    html: successContent,
                    icon: 'success',
                    confirmButtonText: 'متوجه شدم',
                    confirmButtonColor: '#27ae60',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: { popup: 'success-popup' }
                }).then(function () {
                    // ❌ قبلاً: window.location.reload();
                    // ✅ حالا: فقط اسکرول به تاریخچه (اختیاری)
                    var historySection = document.querySelector('.exm-history');
                    if (historySection) {
                        historySection.scrollIntoView({ behavior: 'smooth', block: 'start' });
                    }
                });
            } else {
                alert('✅ درخواست شما با موفقیت ثبت شد\nشماره پیگیری: ' + requestID);
                var historySection = document.querySelector('.exm-history');
                if (historySection) {
                    historySection.scrollIntoView({ behavior: 'smooth', block: 'start' });
                }
            }
        }

        // ============================================
        // راهنما
        // ============================================
        function showHelp() {
            var helpContent =
                '<div style="text-align:right; direction:rtl;">' +
                '<p style="margin-bottom:10px;"><strong>📌 راهنمای ثبت درخواست معاینه:</strong></p>' +
                '<ol style="padding-right:20px; margin:0; line-height:1.8;">' +
                '<li>مهلت رزرو واحد خود را در بالای فرم بررسی کنید.</li>' +
                '<li>از کارت‌های تاریخ، تاریخ مورد نظر خود را انتخاب کنید.</li>' +
                '<li>ظرفیت واحد خود را در کارت تاریخ بررسی کنید.</li>' +
                '<li>دکمه «ثبت درخواست» را بزنید.</li>' +
                '<li>پس از ثبت، درخواست به سرپرست ارسال می‌شود.</li>' +
                '</ol>' +
                '<p style="margin-top:15px; color:#e74c3c; font-weight:bold;">' +
                '⚠️ توجه: تاریخ انتخابی به منزله تاریخ نهایی بوده و امکان تغییر یا ابطال وجود ندارد.' +
                '</p>' +
                '<p style="margin-top:10px; color:#856404; background:#fff3cd; padding:8px; border-radius:6px;">' +
                '📅 تاریخ‌های نمایش داده شده فقط روزهای <strong>شنبه، یکشنبه و دوشنبه</strong> هستند.' +
                '</p>' +
                '</div>';

            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '❓ راهنمای ثبت درخواست',
                    html: helpContent,
                    icon: 'info',
                    confirmButtonText: 'متوجه شدم',
                    confirmButtonColor: '#17a2b8',
                    customClass: {
                        popup: 'help-popup'
                    }
                });
            } else {
                alert('راهنما:\n\n1. مهلت رزرو واحد خود را بررسی کنید\n2. تاریخ معاینه را انتخاب کنید\n3. ظرفیت واحد خود را بررسی کنید\n4. دکمه ثبت را بزنید\n\n⚠️ پس از رزرو امکان تغییر وجود ندارد.');
            }
        }

        // ============================================
        // اتصال رویدادها
        // ============================================
        document.addEventListener('DOMContentLoaded', function () {
            var btnSubmit = document.getElementById('<%= btnSubmit.ClientID %>');

            if (btnSubmit) {
                btnSubmit.addEventListener('click', function (e) {
                    // ===== بررسی انتخاب تاریخ قبل از شروع لودینگ =====
                    var hdnDate = document.getElementById('<%= hdnSelectedDate.ClientID %>');
                    if (!hdnDate || !hdnDate.value || hdnDate.value.trim() === '') {
                        e.preventDefault();
                        e.stopPropagation();
                        if (e.stopImmediatePropagation) e.stopImmediatePropagation();

                        if (typeof Swal !== 'undefined') {
                            Swal.fire({
                                title: '⚠️ تاریخ انتخاب نشده',
                                html: '<div style="text-align:center; direction:rtl; font-size:14px; line-height:1.9;">' +
                                      'لطفاً ابتدا از کارت‌های تقویم، <strong style="color:#e74c3c;">یک تاریخ معاینه</strong> انتخاب کنید.' +
                                      '</div>',
                                icon: 'warning',
                                confirmButtonText: 'متوجه شدم',
                                confirmButtonColor: '#f39c12',
                                customClass: { popup: 'help-popup' }
                            });
                        } else {
                            alert('⚠️ لطفاً ابتدا یک تاریخ معاینه انتخاب کنید.');
                        }
                        return false;
                    }
                    // ===== پایان بررسی =====

                    if (typeof Page_ClientValidate === 'function') {
                        if (Page_ClientValidate()) {
                            showLoading();
                            this.classList.add('exm-btn-loading');
                            this.value = '⏳ در حال ثبت...';
                        }
                    } else {
                        showLoading();
                        this.classList.add('exm-btn-loading');
                        this.value = '⏳ در حال ثبت...';
                    }
                });
                }

            hideLoading();
        });

            window.addEventListener('load', function () {
                hideLoading();
            });
    </script>
</asp:Content>