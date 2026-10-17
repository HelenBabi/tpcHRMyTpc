<%@ Page Title="ثبت درخواست فرزندان ممتاز" Language="VB" MasterPageFile="~/dabir.master" 
    AutoEventWireup="false" CodeFile="ChildRequest.aspx.vb" Inherits="ChildRequest" 
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .chr-container { max-width: 1100px; margin: 30px auto; padding: 25px 30px; background: #fff; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; font-family: 'Segoe UI', Tahoma, sans-serif; }
        .chr-title { font-size: 22px; font-weight: 700; color: #2c3e50; text-align: center; margin-bottom: 20px; padding-bottom: 15px; border-bottom: 2px solid #e9ecef; }
        .chr-deadline-box { background: linear-gradient(135deg, #e8f4fd, #d1ecf1); border: 2px solid #3498db; border-radius: 10px; padding: 15px 20px; margin-bottom: 20px; text-align: center; }
        .chr-deadline-box .icon { font-size: 28px; display: block; margin-bottom: 5px; }
        .chr-deadline-box .title { color: #0c5460; font-size: 14px; font-weight: 600; }
        .chr-deadline-box .date { color: #e74c3c; font-size: 20px; font-weight: bold; direction: ltr; display: inline-block; }
        .chr-deadline-box .status { color: #27ae60; font-size: 13px; display: block; margin-top: 5px; }
        .chr-section { background: #f8f9fa; border-radius: 10px; padding: 18px 20px; margin-bottom: 18px; border-right: 4px solid #3498db; }
        .chr-section.child { border-right-color: #e91e63; background: #fce4ec; }
        .chr-section.education { border-right-color: #f39c12; background: #fef9e7; }
        .chr-section.documents { border-right-color: #27ae60; background: #e8f8f0; }
        .chr-section-title { font-size: 15px; font-weight: 700; color: #2c3e50; margin-bottom: 15px; padding-bottom: 8px; border-bottom: 1px dashed #dee2e6; position: relative; }
        .chr-child-list { display: grid; grid-template-columns: repeat(auto-fill, minmax(280px, 1fr)); gap: 12px; margin-top: 10px; }
        .chr-child-card { background: #fff; border: 2px solid #e0e0e0; border-radius: 10px; padding: 12px 15px; cursor: pointer; transition: all 0.2s; position: relative; text-align: center; display: block; text-decoration: none; color: inherit; }
        .chr-child-card:hover { border-color: #e91e63; transform: translateY(-2px); box-shadow: 0 4px 12px rgba(233,30,99,0.15); text-decoration: none; color: inherit; }
        .chr-child-card.selected { border-color: #e91e63; background: #fce4ec; box-shadow: 0 4px 12px rgba(233,30,99,0.3); }
        .chr-child-card.disabled { background: #f8f9fa; border-color: #dee2e6; opacity: 0.7; cursor: not-allowed; }
        .chr-child-card.disabled::after { content: "⛔ قبلاً ثبت شده"; position: absolute; top: 8px; left: 8px; background: #e74c3c; color: #fff; padding: 3px 10px; border-radius: 12px; font-size: 10px; font-weight: 600; }
        .chr-child-card.editing-card { border-color: #f39c12; background: #fff8e1; }
        .chr-child-card.editing-card::before { content: "✏️ در حال ویرایش"; position: absolute; top: 8px; left: 8px; background: #f39c12; color: #fff; padding: 3px 10px; border-radius: 12px; font-size: 10px; font-weight: 600; }
        .chr-child-card .child-icon { font-size: 32px; margin-bottom: 5px; display: block; }
        .chr-child-card .child-name { font-size: 15px; font-weight: 700; color: #2c3e50; margin-bottom: 5px; }
        .chr-child-card .child-info { font-size: 12px; color: #6c757d; line-height: 1.6; }
        .chr-form-group { margin-bottom: 15px; }
        .chr-form-group label { display: block; font-weight: 600; color: #2c3e50; margin-bottom: 6px; font-size: 13px; }
        .chr-form-group label .required { color: #e74c3c; }
        .chr-input, .chr-select { width: 100%; padding: 9px 12px; border: 1.5px solid #e0e0e0; border-radius: 8px; font-size: 13px; font-family: inherit; box-sizing: border-box; transition: all 0.2s; }
        .chr-input:focus, .chr-select:focus { border-color: #3498db; outline: none; box-shadow: 0 0 0 3px rgba(52,152,219,0.15); }

        .chr-condition-hint { background: linear-gradient(135deg, #fff3cd, #ffe8a1); border-right: 4px solid #f39c12; padding: 15px 20px; border-radius: 8px; font-size: 15px; color: #856404; margin-top: 12px; display: flex; align-items: center; gap: 12px; }
        .chr-condition-hint .condition-icon { font-size: 28px; flex-shrink: 0; }
        .chr-condition-hint .label { font-weight: 600; font-size: 13px; color: #856404; display: block; margin-bottom: 5px; }
        .chr-condition-hint .cond-value { font-weight: 800; color: #e74c3c; font-size: 20px; background: #fff; padding: 6px 18px; border-radius: 8px; border: 2px solid #e74c3c; display: inline-block; }

        .chr-primary-box { background: #e8f4fd; border: 2px dashed #3498db; border-radius: 10px; padding: 15px 20px; font-size: 13px; color: #0c5460; line-height: 1.9; }
        .chr-primary-box .title { font-weight: 700; font-size: 14px; color: #0c5460; margin-bottom: 8px; }

        .chr-edit-banner { background: linear-gradient(135deg, #ff6b6b 0%, #ee5a52 100%); color: #fff; padding: 20px 25px; border-radius: 12px; margin-bottom: 20px; display: flex; align-items: flex-start; gap: 15px; box-shadow: 0 8px 25px rgba(238, 90, 82, 0.4); border: 3px solid #fff; animation: pulseWarning 2s ease-in-out infinite; flex-wrap: wrap; }
        @keyframes pulseWarning { 0%, 100% { box-shadow: 0 8px 25px rgba(238, 90, 82, 0.4); } 50% { box-shadow: 0 8px 35px rgba(238, 90, 82, 0.7); } }
        .chr-edit-banner .icon { font-size: 42px; flex-shrink: 0; animation: shake 1.5s ease-in-out infinite; }
        @keyframes shake { 0%, 100% { transform: rotate(0deg); } 25% { transform: rotate(-8deg); } 75% { transform: rotate(8deg); } }
        .chr-edit-banner .info { flex: 1; min-width: 250px; }
        .chr-edit-banner .info .title { font-size: 18px; font-weight: 800; margin-bottom: 8px; text-shadow: 0 2px 4px rgba(0,0,0,0.2); }
        .chr-edit-banner .info .desc { font-size: 13px; line-height: 1.8; opacity: 0.95; }
        .chr-edit-banner .reason-box { background: rgba(255,255,255,0.95); color: #c0392b; padding: 15px 20px; border-radius: 10px; margin-top: 12px; border-right: 5px solid #c0392b; line-height: 1.9; box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .chr-edit-banner .reason-box .reason-label { display: block; font-size: 13px; color: #e74c3c; font-weight: 800; margin-bottom: 8px; letter-spacing: 0.5px; }
        .chr-edit-banner .reason-box .reason-text { display: block; font-size: 15px; color: #2c3e50; font-weight: 700; line-height: 2; word-wrap: break-word; }
        .chr-edit-banner .cancel-btn { padding: 10px 22px; background: rgba(255,255,255,0.95); color: #c0392b; border: none; border-radius: 8px; cursor: pointer; font-family: inherit; font-weight: 700; font-size: 13px; transition: all 0.2s; align-self: flex-start; }
        .chr-edit-banner .cancel-btn:hover { background: #fff; transform: scale(1.05); }

        .chr-history-panel { background: #f4ecf7; border-right: 4px solid #9b59b6; border-radius: 10px; padding: 15px 20px; margin-bottom: 20px; }
        .chr-history-panel .panel-title { font-size: 14px; font-weight: 700; color: #6c3483; margin-bottom: 12px; padding-bottom: 8px; border-bottom: 1px dashed #9b59b6; }
        .chr-history-card { background: #fff; padding: 12px 15px; border-radius: 8px; border-right: 4px solid #9b59b6; margin-bottom: 10px; }
        .chr-history-card .chr-hist-header { display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; gap: 8px; margin-bottom: 8px; }
        .chr-history-card .chr-hist-date { font-size: 11px; color: #6c757d; }
        .chr-history-card .chr-hist-row { font-size: 12px; color: #2c3e50; margin-bottom: 4px; }
        .chr-history-card .chr-hist-row strong { color: #2c3e50; }
        .chr-history-card .chr-hist-comment { font-size: 11px; color: #6c757d; }

        /* ===== Upload ===== */
        .chr-upload-box { background: #fff; border: 2px dashed #bdc3c7; border-radius: 10px; padding: 20px; text-align: center; margin-bottom: 15px; }
        .chr-upload-box.required-extra { border-color: #e67e22; background: #fff8e1; border-style: solid; }
        .chr-upload-box .upload-icon { font-size: 36px; margin-bottom: 8px; display: block; }
        .chr-upload-box .upload-title { font-size: 14px; font-weight: 700; color: #2c3e50; margin-bottom: 6px; }
        .chr-upload-box .upload-hint { font-size: 12px; color: #6c757d; margin-bottom: 10px; }
        .chr-upload-box .upload-status { font-size: 13px; color: #27ae60; font-weight: 600; margin-top: 8px; display: block; }
        .chr-upload-box .upload-status.error { color: #e74c3c; }

        .chr-old-file-box { background: #e8f8f0; padding: 12px 15px; border-radius: 8px; margin-bottom: 12px; border: 2px solid #27ae60; text-align: right; }
        .chr-old-file-box .lbl { font-size: 11px; color: #6c757d; display: block; margin-bottom: 6px; font-weight: 600; }
        .chr-old-file-box .fname { font-size: 13px; color: #27ae60; font-weight: 700; word-break: break-all; display: block; margin-bottom: 10px; }
        .chr-old-file-box .file-actions { display: flex; gap: 8px; flex-wrap: wrap; }

        .chr-btn-view-file { padding: 8px 18px; background: #3498db; color: #fff !important; border: none; border-radius: 6px; font-family: inherit; font-size: 12px; font-weight: 600; text-decoration: none !important; display: inline-block; transition: all 0.2s; cursor: pointer; }
        .chr-btn-view-file:hover { background: #2980b9; transform: translateY(-2px); color: #fff !important; }
        .chr-btn-download-file { padding: 8px 18px; background: #27ae60; color: #fff !important; border: none; border-radius: 6px; font-family: inherit; font-size: 12px; font-weight: 600; text-decoration: none !important; display: inline-block; transition: all 0.2s; cursor: pointer; }
        .chr-btn-download-file:hover { background: #229954; transform: translateY(-2px); color: #fff !important; }

        .chr-upload-btn { margin-top: 8px; padding: 8px 20px; background: #3498db; color: #fff; border: none; border-radius: 6px; cursor: pointer; font-family: inherit; font-size: 13px; }
        .chr-upload-btn.orange { background: #e67e22; }
        .chr-upload-btn.pink { background: #e91e63; }
        .chr-upload-btn.purple { background: #9c27b0; }

        .chr-file-input {
            width: 100%;
            padding: 10px;
            border: 2px solid #3498db;
            border-radius: 8px;
            background: #fff;
            font-family: inherit;
            font-size: 13px;
            box-sizing: border-box;
            cursor: pointer;
            margin-top: 8px;
            display: block;
        }
        .chr-file-input:hover { border-color: #2980b9; }
        .chr-file-input:focus { outline: none; box-shadow: 0 0 0 3px rgba(52,152,219,0.2); }

        .chr-selected-file-info { background: #e8f4f8; border-radius: 8px; padding: 10px 15px; margin-top: 10px; font-size: 12px; color: #555; text-align: right; border-right: 3px solid #3498db; }
        .chr-selected-file-info .fname { font-weight: 700; color: #0c5460; }

        .chr-extra-doc-panel { background: linear-gradient(135deg, #fff8e1, #ffe8a1); border: 2px solid #e67e22; border-radius: 10px; padding: 15px 20px; margin-top: 15px; }
        .chr-extra-doc-panel .panel-title { font-size: 14px; font-weight: 700; color: #a04000; margin-bottom: 10px; }

        /* ✅ پنل مدارک هویتی (شناسنامه و کارت ملی) */
        .chr-identity-panel { background: linear-gradient(135deg, #fce4ec, #f8bbd0); border: 2px solid #e91e63; border-radius: 10px; padding: 15px 20px; margin-bottom: 15px; }
        .chr-identity-panel .panel-title { font-size: 14px; font-weight: 700; color: #880e4f; margin-bottom: 10px; }
        .chr-identity-panel .chr-upload-box { border-color: #e91e63; }

        .chr-student-card-panel { background: linear-gradient(135deg, #f3e5f5, #e1bee7); border: 2px solid #9c27b0; border-radius: 10px; padding: 15px 20px; margin-top: 15px; }
        .chr-student-card-panel .chr-upload-box { border-color: #9c27b0; }

        .chr-btn-group { display: flex; gap: 10px; justify-content: center; margin-top: 25px; padding-top: 20px; border-top: 2px solid #e9ecef; flex-wrap: wrap; }
        .chr-btn { padding: 12px 40px; border: none; border-radius: 25px; font-size: 14px; font-weight: 600; cursor: pointer; font-family: inherit; min-width: 160px; transition: all 0.2s; }
        .chr-btn:hover:not(:disabled) { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .chr-btn-submit { background: #27ae60; color: #fff; }
        .chr-btn-submit.edit-mode { background: #f39c12; }
        .chr-btn-submit:disabled { background: #bdc3c7; cursor: not-allowed; opacity: 0.6; }
        .chr-btn-cancel { background: #95a5a6; color: #fff; }

        .chr-history { padding-top: 20px; border-top: 2px solid #e9ecef; }
        .chr-history h4 { color: #2c3e50; margin-bottom: 12px; font-size: 15px; }
        .chr-status-badge { padding: 4px 12px; border-radius: 12px; font-size: 12px; font-weight: 600; }
        .chr-status-1 { background: #fff3cd; color: #856404; }
        .chr-status-2 { background: #fff3cd; color: #856404; }
        .chr-status-3 { background: #d4edda; color: #155724; }
        .chr-status-4 { background: #f8d7da; color: #721c24; }
        .chr-status-5 { background: #e2e3e5; color: #383d41; }
        .chr-btn-edit { padding: 6px 16px; background: #f39c12; color: #fff; border: none; border-radius: 15px; font-size: 12px; font-weight: 600; cursor: pointer; font-family: inherit; transition: all 0.2s; text-decoration: none; display: inline-block; }
        .chr-btn-edit:hover { background: #e67e22; transform: scale(1.05); color: #fff; text-decoration: none; }

        .chr-history-item { background: #f8f9fa; padding: 14px 18px; border-radius: 10px; margin-bottom: 12px; border-right: 4px solid #3498db; font-size: 13px; transition: all 0.2s; display: block; }
        .chr-history-item:hover { box-shadow: 0 3px 10px rgba(0,0,0,0.08); }
        .chr-history-item.needs-edit { border-right-color: #f39c12; background: #fff8e1; }
        .chr-history-item.rejected { border-right-color: #e74c3c; background: #fff5f5; }
        .chr-history-item.approved { border-right-color: #27ae60; background: #e8f8f0; }
        .chr-history-top { display: flex; justify-content: space-between; align-items: center; gap: 10px; flex-wrap: wrap; }
        .chr-history-top .chr-history-info { flex: 1; min-width: 200px; }
        .chr-history-top .chr-history-actions { display: flex; gap: 8px; align-items: center; }

        .chr-history-reason { background: #fff5f5; border-right: 4px solid #e74c3c; border-radius: 8px; padding: 10px 14px; margin-top: 12px; font-size: 12px; line-height: 1.9; text-align: right; box-shadow: 0 2px 6px rgba(231, 76, 60, 0.1); }
        .chr-history-reason .reason-label { display: block; font-size: 11px; color: #e74c3c; font-weight: 800; margin-bottom: 5px; letter-spacing: 0.3px; }
        .chr-history-reason .reason-text { display: block; font-size: 13px; color: #2c3e50; font-weight: 600; line-height: 1.8; word-wrap: break-word; }
        .chr-history-reason.for-rejected { background: #f8d7da; border-right-color: #c0392b; }
        .chr-history-reason.for-rejected .reason-label { color: #c0392b; }

        .chr-btn-add-child { float: left; padding: 6px 16px; background: linear-gradient(135deg, #27ae60, #2ecc71); color: #fff !important; border: none; border-radius: 20px; font-size: 12px; font-weight: 700; text-decoration: none !important; display: inline-block; transition: all 0.2s; cursor: pointer; font-family: inherit; }
        .chr-btn-add-child:hover { background: linear-gradient(135deg, #229954, #27ae60); transform: translateY(-2px); box-shadow: 0 4px 12px rgba(39,174,96,0.3); color: #fff !important; text-decoration: none !important; }

        .chr-add-child-panel { background: linear-gradient(135deg, #e8f8f0, #d4edda); border: 2px solid #27ae60; border-radius: 12px; padding: 18px 22px; margin-bottom: 18px; animation: slideDown 0.3s ease; }
        @keyframes slideDown { from { opacity: 0; transform: translateY(-10px); } to { opacity: 1; transform: translateY(0); } }
        .chr-add-child-header { display: flex; align-items: center; gap: 10px; margin-bottom: 15px; padding-bottom: 10px; border-bottom: 1px dashed #27ae60; }
        .chr-add-child-header .icon { font-size: 22px; }
        .chr-add-child-header .title { font-size: 15px; font-weight: 700; color: #155724; flex: 1; }
        .chr-add-child-close { background: rgba(231,76,60,0.9); color: #fff !important; border: none; border-radius: 50%; width: 30px; height: 30px; font-size: 14px; font-weight: 700; cursor: pointer; text-decoration: none !important; display: inline-flex; align-items: center; justify-content: center; transition: all 0.2s; }
        .chr-add-child-close:hover { background: #c0392b; transform: scale(1.1); }
        .chr-add-child-body { background: #fff; border-radius: 10px; padding: 15px; }
        .chr-add-child-actions { display: flex; gap: 10px; justify-content: flex-end; margin-top: 12px; padding-top: 12px; border-top: 1px solid #e9ecef; }
        .chr-btn-save-child { padding: 10px 30px; background: linear-gradient(135deg, #27ae60, #2ecc71); color: #fff; border: none; border-radius: 8px; font-family: inherit; font-size: 13px; font-weight: 700; cursor: pointer; transition: all 0.2s; }
        .chr-btn-save-child:hover { background: #229954; transform: translateY(-2px); box-shadow: 0 4px 12px rgba(39,174,96,0.4); }
        .chr-btn-cancel-child { padding: 10px 30px; background: #95a5a6; color: #fff; border: none; border-radius: 8px; font-family: inherit; font-size: 13px; font-weight: 700; cursor: pointer; transition: all 0.2s; }
        .chr-btn-cancel-child:hover { background: #7f8c8d; transform: translateY(-2px); }

        .chr-child-hint { background: linear-gradient(135deg, #fff3cd, #ffe8a1); border-right: 5px solid #f39c12; border-radius: 10px; padding: 14px 18px; margin-bottom: 15px; display: flex; align-items: flex-start; gap: 12px; font-size: 13px; color: #856404; line-height: 1.9; box-shadow: 0 2px 8px rgba(243,156,18,0.15); }
        .chr-child-hint .hint-icon { font-size: 26px; flex-shrink: 0; animation: pulseIcon 2s ease-in-out infinite; }
        @keyframes pulseIcon { 0%, 100% { transform: scale(1); } 50% { transform: scale(1.15); } }
        .chr-child-hint .hint-text { flex: 1; }
        .chr-child-hint .hint-text strong { color: #e67e22; font-weight: 800; }
        .chr-child-hint .hint-text .small { display: block; margin-top: 6px; font-size: 12px; color: #a04000; }

        .chr-no-children-info { background: linear-gradient(135deg, #e8f4fd, #d1ecf1); border: 2px solid #3498db; border-radius: 10px; padding: 20px; text-align: center; }
        .chr-no-children-info .icon { font-size: 40px; display: block; margin-bottom: 10px; }
        .chr-no-children-info p { color: #0c5460; font-size: 14px; margin: 5px 0; line-height: 1.8; }
        .chr-no-children-info .sub { color: #6c757d; font-size: 12px; }

        .chr-child-card-wrapper { position: relative; display: block; }
        .chr-child-card-wrapper .chr-child-card { width: 100%; box-sizing: border-box; padding-top: 42px; }
        .chr-btn-edit-child { position: absolute; top: 8px; left: 8px; padding: 5px 14px; background: linear-gradient(135deg, #f39c12, #e67e22); color: #fff !important; border: 2px solid #fff; border-radius: 15px; font-size: 11px; font-weight: 700; cursor: pointer; font-family: inherit; text-decoration: none !important; display: inline-flex; align-items: center; gap: 4px; box-shadow: 0 3px 8px rgba(243,156,18,0.4); transition: all 0.2s; z-index: 5; white-space: nowrap; }
        .chr-btn-edit-child:hover { background: linear-gradient(135deg, #e67e22, #d35400); transform: scale(1.05); box-shadow: 0 4px 14px rgba(243,156,18,0.6); color: #fff !important; text-decoration: none !important; }

        .chr-hidden { display: none !important; }

        /* ===== Modal ===== */
        .chr-modal-overlay { display: none; position: fixed; top: 0; left: 0; right: 0; bottom: 0; width: 100vw; height: 100vh; background: rgba(0,0,0,0.7); z-index: 2147483647; margin: 0; padding: 0; direction: rtl; text-align: center; overflow: auto; }
        .chr-modal-overlay.show { display: block; }
        .chr-modal-overlay::before { content: ''; display: inline-block; height: 100%; vertical-align: middle; margin-right: -0.25em; }
        .chr-modal { background: #fff; border-radius: 16px; max-width: 500px; width: 90%; box-shadow: 0 20px 60px rgba(0,0,0,0.3); display: inline-block; text-align: right; vertical-align: middle; margin: 30px 0; direction: rtl; }
        .chr-modal-header { padding: 25px; text-align: center; border-radius: 16px 16px 0 0; }
        .chr-modal-header.success { background: linear-gradient(135deg, #27ae60, #2ecc71); color: #fff; }
        .chr-modal-header.error { background: linear-gradient(135deg, #e74c3c, #c0392b); color: #fff; }
        .chr-modal-header.warning { background: linear-gradient(135deg, #f39c12, #e67e22); color: #fff; }
        .chr-modal-header.info { background: linear-gradient(135deg, #3498db, #2980b9); color: #fff; }
        .chr-modal-icon { font-size: 60px; display: block; margin-bottom: 10px; }
        .chr-modal-title { font-size: 20px; font-weight: 700; margin: 0; }
        .chr-modal-body { padding: 20px 25px; color: #2c3e50; font-size: 14px; line-height: 1.9; text-align: center; }
        .chr-modal-footer { padding: 15px 25px 25px; text-align: center; }
        .chr-modal-btn { padding: 10px 40px; border: none; border-radius: 25px; font-size: 14px; font-weight: 600; cursor: pointer; font-family: inherit; color: #fff; }
        .chr-modal-btn.success { background: #27ae60; }
        .chr-modal-btn.error { background: #e74c3c; }
        .chr-modal-btn.warning { background: #f39c12; }
        .chr-modal-btn.info { background: #3498db; }

        .chr-toast-container { position: fixed; top: 80px; left: 50%; transform: translateX(-50%); z-index: 2147483646; direction: rtl; }
        .chr-toast { background: #fff; border-radius: 12px; padding: 15px 25px; margin-bottom: 10px; box-shadow: 0 8px 30px rgba(0,0,0,0.15); display: flex; align-items: center; gap: 12px; min-width: 300px; max-width: 500px; border-right: 5px solid; }
        .chr-toast.success { border-right-color: #27ae60; }
        .chr-toast.error { border-right-color: #e74c3c; }
        .chr-toast.warning { border-right-color: #f39c12; }
        .chr-toast.info { border-right-color: #3498db; }
        .chr-toast-icon { font-size: 24px; }
        .chr-toast-message { font-size: 13px; color: #2c3e50; font-weight: 600; }
        .chr-toast-close { margin-right: auto; cursor: pointer; color: #999; font-size: 18px; }

        .chr-loading { display: none; position: fixed; top: 0; left: 0; right: 0; bottom: 0; background: rgba(255,255,255,0.85); z-index: 2147483645; align-items: center; justify-content: center; }
        .chr-loading.show { display: flex; }
        .chr-spinner { width: 50px; height: 50px; border: 5px solid #e0e0e0; border-top-color: #3498db; border-radius: 50%; animation: chr-spin 0.8s linear infinite; margin: 0 auto 15px; }
        @keyframes chr-spin { to { transform: rotate(360deg); } }
        .chr-loading-text { color: #2c3e50; font-weight: 600; font-size: 14px; }

        @media (max-width: 768px) {
            .chr-container { padding: 15px; margin: 10px; }
            .chr-child-list { grid-template-columns: 1fr; }
            .chr-btn { width: 100%; }
            .chr-condition-hint { flex-direction: column; text-align: center; }
            .chr-history-item { flex-direction: column; align-items: stretch; }
            .chr-edit-banner { flex-direction: column; }
            .chr-child-hint { flex-direction: column; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>

            <div class="chr-container">
                <div class="chr-title">🎓 ثبت درخواست فرزندان ممتاز - سال 1404-1405</div>

                <%-- بنر نیاز به اصلاح --%>
                <asp:Panel ID="pnlEditBanner" runat="server" Visible="false" CssClass="chr-edit-banner">
                    <span class="icon">⚠️</span>
                    <div class="info">
                        <div class="title">🔔 درخواست شما نیاز به اصلاح دارد</div>
                        <div class="desc">
                            درخواست فرزند <asp:Label ID="lblEditChildName" runat="server" Font-Bold="true" Font-Size="Large" /> 
                            توسط کارشناس روابط عمومی بررسی شده و <strong>نیاز به اصلاح</strong> دارد.
                        </div>
                        <div class="reason-box">
                            <span class="reason-label">📋 دلیل نیاز به اصلاح:</span>
                            <div class="reason-text"><asp:Label ID="lblEditReason" runat="server" /></div>
                        </div>
                        <div class="desc" style="margin-top:12px;">
                            لطفاً موارد فوق را بررسی کرده، اطلاعات را اصلاح کنید و مجدداً ارسال نمایید.
                        </div>
                    </div>
                    <asp:Button ID="btnCancelEdit" runat="server" Text="❌ لغو ویرایش" CssClass="cancel-btn" 
                        OnClick="btnCancelEdit_Click" CausesValidation="false" />
                </asp:Panel>

                <%-- تاریخچه بررسی‌ها --%>
                <asp:Panel ID="pnlEditHistory" runat="server" Visible="false" CssClass="chr-history-panel">
                    <div class="panel-title">📋 تاریخچه بررسی‌های این درخواست</div>
                    <asp:Repeater ID="rptEditHistory" runat="server">
                        <ItemTemplate>
                            <div class="chr-history-card">
                                <div class="chr-hist-header">
                                    <span class='chr-status-badge chr-status-<%# Eval("StatusCode") %>'>
                                        <%# GetHistoryStatusText(Eval("StatusCode")) %>
                                    </span>
                                    <span class="chr-hist-date">
                                        <%# GetHistoryDateFa(Eval("ReviewDate"), Eval("CreateDate")) %>
                                    </span>
                                </div>
                                <div class="chr-hist-row">
                                    <strong>👤 کارشناس:</strong> 
                                    <%# GetHistoryReviewerName(Eval("ReviewerName"), Eval("ReviewerCode")) %>
                                </div>
                                <div class="chr-hist-row">
                                    <strong>📋 دلیل:</strong> 
                                    <%# GetHistoryReasonText(Eval("StatusDescription")) %>
                                </div>
                                <div class="chr-hist-comment">
                                    <strong>📝 یادداشت:</strong> 
                                    <%# GetHistoryCommentText(Eval("ReviewerComment")) %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>

                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div id="divMessage" runat="server" style="padding:12px 18px; border-radius:8px; margin-bottom:15px;">
                        <asp:Label ID="lblMessageIcon" runat="server" />
                        <asp:Label ID="lblMessage" runat="server" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlDeadline" runat="server" CssClass="chr-deadline-box">
                    <span class="icon">⏰</span>
                    <div class="title">مهلت ارسال مدارک:</div>
                    <asp:Label ID="lblDeadlineDate" runat="server" CssClass="date" Text="-" />
                    <asp:Label ID="lblDeadlineStatus" runat="server" CssClass="status" />
                </asp:Panel>

                <%-- ✅ بخش فرزندان --%>
                <div class="chr-section child">
                    <div class="chr-section-title">
                        👶 انتخاب فرزند (فقط فرزندان تحت تکفل شما)
                        <asp:LinkButton ID="lnkShowAddChild" runat="server" 
                            CssClass="chr-btn-add-child" 
                            OnClick="lnkShowAddChild_Click" 
                            CausesValidation="false">
                            ➕ افزودن فرزند جدید
                        </asp:LinkButton>
                    </div>

                    <%-- پیام راهنما --%>
                    <div class="chr-child-hint">
                        <span class="hint-icon">💡</span>
                        <div class="hint-text">
                            <strong>توجه:</strong> اگر فرزند شما در لیست زیر وجود ندارد یا اطلاعاتش (نام، کد ملی، تاریخ تولد) ناقص است،
                            لطفاً با کلیک روی دکمه <strong>«➕ افزودن فرزند جدید»</strong> در بالای همین بخش، 
                            فرزند خود را اضافه کنید. برای اصلاح اطلاعات موجود، روی دکمه <strong>✏️ ویرایش</strong> روی هر کارت کلیک کنید.
                            <span class="small">
                                ⚠️ دقت کنید که کد ملی و تاریخ تولد را دقیقاً مطابق شناسنامه وارد کنید.
                            </span>
                        </div>
                    </div>

                    <%-- فرم افزودن فرزند --%>
                    <asp:Panel ID="pnlAddChild" runat="server" CssClass="chr-add-child-panel chr-hidden">
                        <div class="chr-add-child-header">
                            <span class="icon">➕</span>
                            <span class="title">اطلاعات فرزند جدید</span>
                            <asp:LinkButton ID="lnkCloseAddChild" runat="server" CssClass="chr-add-child-close"
                                OnClick="lnkCloseAddChild_Click" CausesValidation="false" Text="✕" />
                        </div>

                        <div class="chr-add-child-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>نام: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtNewChildFirstName" runat="server" CssClass="chr-input" 
                                            placeholder="نام فرزند" MaxLength="50" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>نام خانوادگی: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtNewChildLastName" runat="server" CssClass="chr-input" 
                                            placeholder="نام خانوادگی" MaxLength="50" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>جنسیت: <span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlNewChildGender" runat="server" CssClass="chr-select">
                                            <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                            <asp:ListItem Text="👦 پسر" Value="1" />
                                            <asp:ListItem Text="👧 دختر" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="chr-form-group">
                                        <label>کد ملی: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtNewChildNationalCode" runat="server" CssClass="chr-input" 
                                            placeholder="کد ملی 10 رقمی" MaxLength="10" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="chr-form-group">
                                        <label>تاریخ تولد (مثال: 1390/05/15): <span class="required">*</span></label>
                                        <asp:TextBox ID="txtNewChildBirthDate" runat="server" CssClass="chr-input" 
                                            placeholder="1390/05/15" MaxLength="10" />
                                    </div>
                                </div>
                            </div>

                            <div class="chr-add-child-actions">
                                <asp:Button ID="btnSaveNewChild" runat="server" Text="💾 ثبت فرزند" 
                                    CssClass="chr-btn-save-child" OnClick="btnSaveNewChild_Click" />
                                <asp:Button ID="btnCancelNewChild" runat="server" Text="❌ انصراف" 
                                    CssClass="chr-btn-cancel-child" OnClick="lnkCloseAddChild_Click" 
                                    CausesValidation="false" />
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- فرم ویرایش فرزند --%>
                    <asp:Panel ID="pnlEditChild" runat="server" CssClass="chr-add-child-panel chr-hidden" 
                        style="background: linear-gradient(135deg, #fff8e1, #ffe8a1); border-color: #f39c12;">
                        <div class="chr-add-child-header" style="border-bottom-color: #f39c12;">
                            <span class="icon">✏️</span>
                            <span class="title" style="color: #a04000;">ویرایش اطلاعات فرزند</span>
                            <asp:LinkButton ID="lnkCloseEditChild" runat="server" CssClass="chr-add-child-close"
                                OnClick="lnkCloseEditChild_Click" CausesValidation="false" Text="✕" />
                        </div>

                        <div class="chr-add-child-body">
                            <asp:HiddenField ID="hfEditingChildID" runat="server" Value="0" />

                            <div class="row">
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>نام: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtEditChildFirstName" runat="server" CssClass="chr-input" 
                                            placeholder="نام فرزند" MaxLength="50" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>نام خانوادگی: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtEditChildLastName" runat="server" CssClass="chr-input" 
                                            placeholder="نام خانوادگی" MaxLength="50" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>جنسیت: <span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlEditChildGender" runat="server" CssClass="chr-select">
                                            <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                            <asp:ListItem Text="👦 پسر" Value="1" />
                                            <asp:ListItem Text="👧 دختر" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="chr-form-group">
                                        <label>کد ملی: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtEditChildNationalCode" runat="server" CssClass="chr-input" 
                                            placeholder="کد ملی 10 رقمی" MaxLength="10" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="chr-form-group">
                                        <label>تاریخ تولد (مثال: 1390/05/15): <span class="required">*</span></label>
                                        <asp:TextBox ID="txtEditChildBirthDate" runat="server" CssClass="chr-input" 
                                            placeholder="1390/05/15" MaxLength="10" />
                                    </div>
                                </div>
                            </div>

                            <div class="chr-add-child-actions">
                                <asp:Button ID="btnSaveEditChild" runat="server" Text="💾 ذخیره تغییرات" 
                                    CssClass="chr-btn-save-child" OnClick="btnSaveEditChild_Click" 
                                    style="background: linear-gradient(135deg, #f39c12, #e67e22);" />
                                <asp:Button ID="btnCancelEditChild" runat="server" Text="❌ انصراف" 
                                    CssClass="chr-btn-cancel-child" OnClick="lnkCloseEditChild_Click" 
                                    CausesValidation="false" />
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- لیست فرزندان --%>
                    <asp:Panel ID="pnlChildren" runat="server" Visible="false">
                        <div class="chr-child-list">
                            <asp:Repeater ID="rptChildren" runat="server" OnItemCommand="rptChildren_ItemCommand" OnItemDataBound="rptChildren_ItemDataBound">
                                <ItemTemplate>
                                    <div class="chr-child-card-wrapper">
                                        <asp:LinkButton ID="lnkChild" runat="server" CssClass="chr-child-card" CommandName="SelectChild" CommandArgument='<%# Eval("SponsorshipID") %>'>
                                            <span class="child-icon" id="spnIcon" runat="server">👶</span>
                                            <div class="child-name"><%# Eval("ChildName") %></div>
                                            <div class="child-info">کد ملی: <%# Eval("NationalCode") %><br />تولد: <%# Eval("BirthDate") %></div>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkEditChild" runat="server" CssClass="chr-btn-edit-child"
                                            CommandName="EditChild" CommandArgument='<%# Eval("SponsorshipID") %>' 
                                            CausesValidation="false" ToolTip="ویرایش اطلاعات این فرزند">
                                            ✏️ ویرایش
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>

                    <%-- پیام «هیچ فرزندی نیست» --%>
                    <asp:Panel ID="pnlNoChildren" runat="server" Visible="false" CssClass="chr-no-children-info">
                        <span class="icon">👶</span>
                        <p><strong>هنوز هیچ فرزندی در سیستم ثبت نشده است</strong></p>
                        <p class="sub">می‌توانید با کلیک روی دکمه «➕ افزودن فرزند جدید» در بالای همین بخش، فرزند خود را اضافه کنید.</p>
                    </asp:Panel>
                </div>

                <%-- ============================================================
                     ✅ بخش تحصیلی — با گزینه‌های جدید
                     ============================================================ --%>
                <asp:Panel ID="pnlEducation" runat="server" CssClass="chr-section education chr-hidden">
                    <div class="chr-section-title">
                        📚 اطلاعات تحصیلی
                        <asp:Label ID="lblEditMode" runat="server" Visible="false" 
                            style="background:#f39c12;color:#fff;padding:3px 12px;border-radius:12px;font-size:12px;margin-right:8px;" 
                            Text="✏️ حالت ویرایش" />
                    </div>

                    <%-- ردیف ۱: نوع تحصیل و مقطع --%>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="chr-form-group">
                                <label>نوع تحصیل: <span class="required">*</span></label>
                                <asp:DropDownList ID="ddlChildType" runat="server" CssClass="chr-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlChildType_SelectedIndexChanged">
                                    <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                    <asp:ListItem Text="دانش‌آموز" Value="دانش‌آموز" />
                                    <asp:ListItem Text="دانشجو" Value="دانشجو" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="chr-form-group">
                                <label>مقطع تحصیلی: <span class="required">*</span></label>
                                <asp:DropDownList ID="ddlEducationLevel" runat="server" CssClass="chr-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEducationLevel_SelectedIndexChanged">
                                    <asp:ListItem Text="-- ابتدا نوع تحصیل را انتخاب کنید --" Value="" />
                                </asp:DropDownList>
                                <asp:Panel ID="pnlConditionHint" runat="server" CssClass="chr-condition-hint chr-hidden">
                                    <span class="condition-icon">💡</span>
                                    <div class="condition-content">
                                        <span class="label">شرط این مقطع:</span>
                                        <asp:Label ID="lblCondition" runat="server" CssClass="cond-value" Text="-" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <%-- ✅ ردیف ۲: اطلاعات مدرسه (فقط برای دانش‌آموز) --%>
                    <asp:Panel ID="pnlSchool" runat="server" CssClass="chr-hidden">
                        <div style="background: #fff; border: 2px dashed #3498db; border-radius: 10px; padding: 15px; margin-top: 10px;">
                            <div style="font-size: 14px; font-weight: 700; color: #0c5460; margin-bottom: 12px;">
                                🏫 اطلاعات مدرسه
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>نام مدرسه: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtSchoolName" runat="server" CssClass="chr-input" 
                                            placeholder="مثال: دبیرستان شهید بهشتی" MaxLength="100" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>نوع مدرسه: <span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlSchoolType" runat="server" CssClass="chr-select">
                                            <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                            <asp:ListItem Text="دولتی" Value="دولتی" />
                                            <asp:ListItem Text="غیرانتفاعی" Value="غیرانتفاعی" />
                                            <asp:ListItem Text="نمونه دولتی" Value="نمونه دولتی" />
                                            <asp:ListItem Text="تیزهوشان (سمپاد)" Value="سمپاد" />
                                            <asp:ListItem Text="شاهد" Value="شاهد" />
                                            <asp:ListItem Text="سایر" Value="سایر" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="chr-form-group">
                                        <label>شماره تماس مدرسه: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtSchoolPhone" runat="server" CssClass="chr-input" 
                                            placeholder="مثال: 02112345678" MaxLength="11" />
                                    </div>
                                </div>
                            </div>

                            <%-- ✅ گزینه قبولی در سمپاد --%>
                            <div class="chr-condition-hint" style="background: linear-gradient(135deg, #e8f4fd, #d1ecf1); border-right-color: #3498db; margin-top: 10px;">
                                <span class="condition-icon">🏆</span>
                                <div class="condition-content" style="flex:1;">
                                    <asp:CheckBox ID="chkSampad" runat="server" 
                                        Text=" ✅ قبولی در مدارس استعدادهای درخشان (سمپاد)" 
                                        AutoPostBack="true" OnCheckedChanged="chkSampad_CheckedChanged"
                                        Font-Bold="true" ForeColor="#0c5460" />
                                    <asp:Panel ID="pnlSampadDetails" runat="server" CssClass="chr-hidden" style="margin-top:12px;">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="chr-form-group">
                                                    <label>نام مدرسه سمپاد:</label>
                                                    <asp:TextBox ID="txtSampadSchoolName" runat="server" CssClass="chr-input" 
                                                        placeholder="مثال: دبیرستان فرزانگان" MaxLength="100" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="chr-form-group">
                                                    <label>شماره دانش‌آموزی سمپاد:</label>
                                                    <asp:TextBox ID="txtSampadStudentNumber" runat="server" CssClass="chr-input" 
                                                        placeholder="مثال: 123456" MaxLength="20" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- ✅ ردیف ۳: اطلاعات دانشگاه (فقط برای دانشجو) --%>
                    <asp:Panel ID="pnlUniversity" runat="server" CssClass="chr-hidden">
                        <div style="background: #fff; border: 2px dashed #9c27b0; border-radius: 10px; padding: 15px; margin-top: 10px;">
                            <div style="font-size: 14px; font-weight: 700; color: #6a1b9a; margin-bottom: 12px;">
                                🎓 اطلاعات دانشگاه / مؤسسه
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="chr-form-group">
                                        <label>نام دانشگاه/مؤسسه: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtUniversityName" runat="server" CssClass="chr-input" 
                                            placeholder="مثال: دانشگاه تهران" MaxLength="100" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="chr-form-group">
                                        <label>نوع دانشگاه/مؤسسه: <span class="required">*</span></label>
                                        <asp:DropDownList ID="ddlUniversityType" runat="server" CssClass="chr-select">
                                            <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                            <asp:ListItem Text="دولتی" Value="دولتی" />
                                            <asp:ListItem Text="آزاد" Value="آزاد" />
                                            <asp:ListItem Text="غیرانتفاعی" Value="غیرانتفاعی" />
                                            <asp:ListItem Text="پیام نور" Value="پیام نور" />
                                            <asp:ListItem Text="علمی کاربردی" Value="علمی کاربردی" />
                                            <asp:ListItem Text="مؤسسه آموزش عالی" Value="مؤسسه" />
                                            <asp:ListItem Text="سایر" Value="سایر" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="chr-form-group">
                                        <label>شماره تماس دانشگاه: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtUniversityPhone" runat="server" CssClass="chr-input" 
                                            placeholder="مثال: 02161112345" MaxLength="11" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="chr-form-group">
                                        <label>شماره دانشجویی: <span class="required">*</span></label>
                                        <asp:TextBox ID="txtStudentNumber" runat="server" CssClass="chr-input" 
                                            placeholder="مثال: 401234567" MaxLength="20" />
                                    </div>
                                </div>
                            </div>

                            <%-- ✅ نوع ورود به دانشگاه: با آزمون / بدون آزمون --%>
                            <div style="background: #f3e5f5; border-right: 4px solid #9c27b0; border-radius: 8px; padding: 12px 15px; margin-top: 12px;">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="chr-form-group" style="margin-bottom:0;">
                                            <label>نوع ورود به دانشگاه: <span class="required">*</span></label>
                                            <asp:DropDownList ID="ddlAdmissionType" runat="server" CssClass="chr-select" 
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAdmissionType_SelectedIndexChanged">
                                                <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                                <asp:ListItem Text="🎓 با آزمون سراسری" Value="WithExam" />
                                                <asp:ListItem Text="✅ بدون آزمون (استعدادهای درخشان/المپیاد)" Value="WithoutExam" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Panel ID="pnlRankInput" runat="server" CssClass="chr-hidden">
                                            <div class="chr-form-group" style="margin-bottom:0;">
                                                <label>رتبه آزمون سراسری: <span class="required">*</span></label>
                                                <asp:TextBox ID="txtRank" runat="server" CssClass="chr-input" 
                                                    placeholder="مثال: 150" MaxLength="6" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Panel ID="pnlWithoutExamReason" runat="server" CssClass="chr-hidden">
                                            <div class="chr-form-group" style="margin-bottom:0;">
                                                <label>دلیل پذیرش بدون آزمون: <span class="required">*</span></label>
                                                <asp:DropDownList ID="ddlWithoutExamReason" runat="server" CssClass="chr-select">
                                                    <asp:ListItem Text="-- انتخاب کنید --" Value="" />
                                                    <asp:ListItem Text="المپیاد علمی" Value="المپیاد" />
                                                    <asp:ListItem Text="استعدادهای درخشان" Value="استعداد درخشان" />
                                                    <asp:ListItem Text="معدل بالای 18" Value="معدل بالا" />
                                                    <asp:ListItem Text="سایر" Value="سایر" />
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- پیام ابتدایی --%>
                    <asp:Panel ID="pnlPrimaryBox" runat="server" CssClass="chr-primary-box chr-hidden">
                        <div class="title">📘 توجه برای مقاطع ابتدایی (اول تا ششم):</div>
                        در مقاطع اول تا ششم ابتدایی، نمره عددی داده نمی‌شود و کارنامه به‌صورت <strong>«خیلی خوب با یک خوب»</strong> ارزیابی می‌گردد.
                        <br />
                        لطفاً <strong>تصویر کارنامه</strong> فرزند خود را در بخش مدارک بارگذاری نمایید.
                    </asp:Panel>

                    <%-- معدل --%>
                    <asp:Panel ID="pnlAverageRow" runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="chr-form-group">
                                    <label>معدل کل: <span class="required">*</span></label>
                                    <asp:TextBox ID="txtAverage" runat="server" CssClass="chr-input" placeholder="مثال: 18.50" MaxLength="5" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>

                <%-- ============================================================
                     ✅ بخش مدارک — با مدارک هویتی جدید
                     ============================================================ --%>
                <asp:Panel ID="pnlDocuments" runat="server" CssClass="chr-section documents chr-hidden">
                    <div class="chr-section-title">📎 بارگذاری مدارک مورد نیاز</div>

                    <%-- ✅ پنل مدارک هویتی (شناسنامه و کارت ملی) --%>
                    <div class="chr-identity-panel">
                        <div class="panel-title">🆔 مدارک هویتی الزامی</div>

                        <%-- تصویر صفحه اول شناسنامه --%>
                        <div class="chr-upload-box required-extra" style="border-color: #e91e63;">
                            <span class="upload-icon">📘</span>
                            <div class="upload-title">تصویر صفحه اول شناسنامه فرزند <span style="color:#e74c3c;">*</span></div>
                            <div class="upload-hint">فرمت: JPG، PNG یا PDF - حداکثر 2 مگابایت</div>
                            
                            <asp:Panel ID="pnlOldBirthCert" runat="server" Visible="false" CssClass="chr-old-file-box">
                                <span class="lbl">📁 فایل فعلی در سرور:</span>
                                <span class="fname"><asp:Label ID="lblOldBirthCertName" runat="server" /></span>
                                <div class="file-actions">
                                    <a id="lnkViewOldBirthCert" runat="server" target="_blank" class="chr-btn-view-file">👁️ مشاهده</a>
                                    <a id="lnkDownloadOldBirthCert" runat="server" class="chr-btn-download-file">⬇️ دانلود</a>
                                </div>
                            </asp:Panel>
                            
                            <asp:FileUpload ID="fuBirthCertificate" runat="server" 
                                CssClass="chr-file-input" 
                                onchange="onFileSelected(this, 'birthCertSelectedInfo', 'birthCertSelectedName');" />
                            
                            <div id="birthCertSelectedInfo" class="chr-selected-file-info" style="display:none;">
                                <span>📎 فایل انتخاب شده: </span>
                                <span class="fname" id="birthCertSelectedName"></span>
                            </div>
                            
                            <asp:Button ID="btnUploadBirthCert" runat="server" Text="⬆️ بارگذاری شناسنامه" 
                                OnClick="btnUploadBirthCert_Click" CssClass="chr-upload-btn pink" />
                            <asp:Label ID="lblBirthCertStatus" runat="server" CssClass="upload-status" />
                        </div>

                        <%-- تصویر کارت ملی فرزند --%>
                        <div class="chr-upload-box required-extra" style="border-color: #e91e63; margin-top:15px;">
                            <span class="upload-icon">💳</span>
                            <div class="upload-title">تصویر کارت ملی فرزند <span style="color:#e74c3c;">*</span></div>
                            <div class="upload-hint">فرمت: JPG، PNG یا PDF - حداکثر 2 مگابایت</div>
                            
                            <asp:Panel ID="pnlOldNationalCard" runat="server" Visible="false" CssClass="chr-old-file-box">
                                <span class="lbl">📁 فایل فعلی در سرور:</span>
                                <span class="fname"><asp:Label ID="lblOldNationalCardName" runat="server" /></span>
                                <div class="file-actions">
                                    <a id="lnkViewOldNationalCard" runat="server" target="_blank" class="chr-btn-view-file">👁️ مشاهده</a>
                                    <a id="lnkDownloadOldNationalCard" runat="server" class="chr-btn-download-file">⬇️ دانلود</a>
                                </div>
                            </asp:Panel>
                            
                            <asp:FileUpload ID="fuNationalCard" runat="server" 
                                CssClass="chr-file-input" 
                                onchange="onFileSelected(this, 'nationalCardSelectedInfo', 'nationalCardSelectedName');" />
                            
                            <div id="nationalCardSelectedInfo" class="chr-selected-file-info" style="display:none;">
                                <span>📎 فایل انتخاب شده: </span>
                                <span class="fname" id="nationalCardSelectedName"></span>
                            </div>
                            
                            <asp:Button ID="btnUploadNationalCard" runat="server" Text="⬆️ بارگذاری کارت ملی" 
                                OnClick="btnUploadNationalCard_Click" CssClass="chr-upload-btn pink" />
                            <asp:Label ID="lblNationalCardStatus" runat="server" CssClass="upload-status" />
                        </div>
                    </div>

                    <%-- ✅ کارنامه (فقط برای دانش‌آموز) --%>
                    <asp:Panel ID="pnlTranscriptUpload" runat="server" CssClass="chr-hidden">
                        <div class="chr-upload-box">
                            <span class="upload-icon">📄</span>
                            <div class="upload-title">تصویر کارنامه سال تحصیلی <span style="color:#e74c3c;">*</span></div>
                            <div class="upload-hint">فرمت: JPG، PNG یا PDF - حداکثر 2 مگابایت</div>
                            
                            <asp:Panel ID="pnlOldTranscript" runat="server" Visible="false" CssClass="chr-old-file-box">
                                <span class="lbl">📁 فایل فعلی در سرور:</span>
                                <span class="fname"><asp:Label ID="lblOldTranscriptName" runat="server" /></span>
                                <div class="file-actions">
                                    <a id="lnkViewOldTranscript" runat="server" target="_blank" class="chr-btn-view-file">👁️ مشاهده</a>
                                    <a id="lnkDownloadOldTranscript" runat="server" class="chr-btn-download-file">⬇️ دانلود</a>
                                </div>
                            </asp:Panel>
                            
                            <asp:FileUpload ID="fuTranscript" runat="server" 
                                CssClass="chr-file-input" 
                                onchange="onFileSelected(this, 'transcriptSelectedInfo', 'transcriptSelectedName');" />
                            
                            <div id="transcriptSelectedInfo" class="chr-selected-file-info" style="display:none;">
                                <span>📎 فایل انتخاب شده: </span>
                                <span class="fname" id="transcriptSelectedName"></span>
                            </div>
                            
                            <asp:Button ID="btnUploadTranscript" runat="server" Text="⬆️ بارگذاری کارنامه" 
                                OnClick="btnUploadTranscript_Click" CssClass="chr-upload-btn" />
                            <asp:Label ID="lblTranscriptStatus" runat="server" CssClass="upload-status" />
                        </div>
                    </asp:Panel>

                    <%-- ✅ کارت دانشجویی (فقط برای دانشجو) --%>
                    <asp:Panel ID="pnlStudentCard" runat="server" CssClass="chr-student-card-panel chr-hidden">
                        <div class="panel-title" style="font-size:14px;font-weight:700;color:#6a1b9a;margin-bottom:10px;">
                            🎓 کارت دانشجویی
                        </div>
                        <div class="chr-upload-box required-extra" style="border-color: #9c27b0; background:#f3e5f5;">
                            <span class="upload-icon">🎓</span>
                            <div class="upload-title">تصویر کارت دانشجویی <span style="color:#e74c3c;">*</span></div>
                            <div class="upload-hint">فرمت: JPG، PNG یا PDF - حداکثر 2 مگابایت</div>
                            
                            <asp:Panel ID="pnlOldStudentCard" runat="server" Visible="false" CssClass="chr-old-file-box">
                                <span class="lbl">📁 فایل فعلی در سرور:</span>
                                <span class="fname"><asp:Label ID="lblOldStudentCardName" runat="server" /></span>
                                <div class="file-actions">
                                    <a id="lnkViewOldStudentCard" runat="server" target="_blank" class="chr-btn-view-file">👁️ مشاهده</a>
                                    <a id="lnkDownloadOldStudentCard" runat="server" class="chr-btn-download-file">⬇️ دانلود</a>
                                </div>
                            </asp:Panel>
                            
                            <asp:FileUpload ID="fuStudentCard" runat="server" 
                                CssClass="chr-file-input" 
                                onchange="onFileSelected(this, 'studentCardSelectedInfo', 'studentCardSelectedName');" />
                            
                            <div id="studentCardSelectedInfo" class="chr-selected-file-info" style="display:none;">
                                <span>📎 فایل انتخاب شده: </span>
                                <span class="fname" id="studentCardSelectedName"></span>
                            </div>
                            
                            <asp:Button ID="btnUploadStudentCard" runat="server" Text="⬆️ بارگذاری کارت دانشجویی" 
                                OnClick="btnUploadStudentCard_Click" CssClass="chr-upload-btn purple" />
                            <asp:Label ID="lblStudentCardStatus" runat="server" CssClass="upload-status" />
                        </div>
                    </asp:Panel>

                    <%-- ✅ مدرک اضافی --%>
                    <asp:Panel ID="pnlExtraDocument" runat="server" CssClass="chr-extra-doc-panel chr-hidden">
                        <div class="panel-title">📑 <asp:Label ID="lblExtraDocTitle" runat="server" Text="مدرک اضافی مورد نیاز" /></div>
                        <div style="font-size:13px;color:#a04000;margin-bottom:10px;line-height:1.7;">
                            <asp:Label ID="lblExtraDocDescription" runat="server" />
                        </div>
                        <div class="chr-upload-box required-extra">
                            <span class="upload-icon">📎</span>
                            <div class="upload-title"><asp:Label ID="lblExtraDocName" runat="server" Text="مدرک" /> <span style="color:#e74c3c;">*</span></div>
                            <div class="upload-hint">فرمت: JPG، PNG یا PDF - حداکثر 2 مگابایت</div>
                            
                            <asp:Panel ID="pnlOldExtraDoc" runat="server" Visible="false" CssClass="chr-old-file-box">
                                <span class="lbl">📁 فایل فعلی در سرور:</span>
                                <span class="fname"><asp:Label ID="lblOldExtraDocName" runat="server" /></span>
                                <div class="file-actions">
                                    <a id="lnkViewOldExtra" runat="server" target="_blank" class="chr-btn-view-file">👁️ مشاهده</a>
                                    <a id="lnkDownloadOldExtra" runat="server" class="chr-btn-download-file">⬇️ دانلود</a>
                                </div>
                            </asp:Panel>
                            
                            <asp:FileUpload ID="fuExtraDocument" runat="server" 
                                CssClass="chr-file-input" 
                                onchange="onFileSelected(this, 'extraSelectedInfo', 'extraSelectedName');" />
                            
                            <div id="extraSelectedInfo" class="chr-selected-file-info" style="display:none;">
                                <span>📎 فایل انتخاب شده: </span>
                                <span class="fname" id="extraSelectedName"></span>
                            </div>
                            
                            <asp:Button ID="btnUploadExtraDoc" runat="server" Text="⬆️ بارگذاری مدرک" 
                                OnClick="btnUploadExtraDoc_Click" CssClass="chr-upload-btn orange" />
                            <asp:Label ID="lblExtraDocStatus" runat="server" CssClass="upload-status" />
                        </div>
                    </asp:Panel>
                </asp:Panel>

                <div class="chr-btn-group">
                    <asp:Button ID="btnSubmit" runat="server" Text="📤 ارسال نهایی" 
                        CssClass="chr-btn chr-btn-submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" 
                        CssClass="chr-btn chr-btn-cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>

                <asp:HiddenField ID="hfSelectedChildID" runat="server" Value="0" />
                <asp:HiddenField ID="hfEditingRequestID" runat="server" Value="0" />
                <asp:HiddenField ID="hfRequiresExtraDoc" runat="server" Value="0" />
                <asp:HiddenField ID="hfExtraDocType" runat="server" Value="" />
                <asp:HiddenField ID="hfTranscriptUploaded" runat="server" Value="0" />
                <asp:HiddenField ID="hfExtraDocUploaded" runat="server" Value="0" />
                <asp:HiddenField ID="hfBirthCertUploaded" runat="server" Value="0" />
                <asp:HiddenField ID="hfNationalCardUploaded" runat="server" Value="0" />
                <asp:HiddenField ID="hfStudentCardUploaded" runat="server" Value="0" />
                <asp:HiddenField ID="hfSampadSelected" runat="server" Value="0" />
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlChildType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEducationLevel" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAdmissionType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkSampad" EventName="CheckedChanged" />
            <asp:PostBackTrigger ControlID="btnUploadTranscript" />
            <asp:PostBackTrigger ControlID="btnUploadExtraDoc" />
            <asp:PostBackTrigger ControlID="btnUploadBirthCert" />
            <asp:PostBackTrigger ControlID="btnUploadNationalCard" />
            <asp:PostBackTrigger ControlID="btnUploadStudentCard" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkShowAddChild" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkCloseAddChild" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveNewChild" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelNewChild" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkCloseEditChild" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveEditChild" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelEditChild" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <%-- ✅ History با UpdatePanel جداگانه --%>
    <asp:UpdatePanel ID="upHistory" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="chr-container" style="margin-top:-20px;">
                <div class="chr-history">
                    <h4>📜 درخواست‌های قبلی شما</h4>
                    <asp:Panel ID="pnlHistory" runat="server" Visible="false">
                        <asp:Repeater ID="rptHistory" runat="server" 
                            OnItemCommand="rptHistory_ItemCommand" 
                            OnItemDataBound="rptHistory_ItemDataBound">
                            <ItemTemplate>
                                <div class='chr-history-item <%# GetHistoryItemClass(Eval("StatusCode")) %>'>
                                    
                                    <div class="chr-history-top">
                                        <div class="chr-history-info">
                                            <strong><%# Eval("ChildFullName") %></strong>
                                            <span style="color:#6c757d;margin-right:10px;">
                                                | سال <%# Eval("AcademicYear") %> | <%# Eval("EducationLevel") %>
                                            </span>
                                        </div>
                                        <div class="chr-history-actions">
                                            <span class='chr-status-badge chr-status-<%# Eval("StatusCode") %>'>
                                                <%# Eval("StatusText") %>
                                            </span>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="chr-btn-edit"
                                                CommandName="EditRequest" CommandArgument='<%# Eval("ID") %>' 
                                                CausesValidation="false">
                                                ✏️ اصلاح
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    
                                    <asp:Panel ID="pnlReason" runat="server" Visible="false" 
                                        CssClass='<%# GetHistoryReasonClass(Eval("StatusCode")) %>'>
                                        <span class="reason-label">
                                            <%# GetHistoryReasonLabel(Eval("StatusCode")) %>
                                        </span>
                                        <span class="reason-text"><%# Eval("ReasonDisplay") %></span>
                                    </asp:Panel>
                                    
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <asp:Panel ID="pnlNoHistory" runat="server" Visible="true">
                        <div style="text-align:center;padding:20px;color:#6c757d;">📭 هنوز درخواستی ثبت نکرده‌اید</div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal -->
    <div id="chrModal" class="chr-modal-overlay">
        <div class="chr-modal">
            <div id="chrModalHeader" class="chr-modal-header success">
                <span id="chrModalIcon" class="chr-modal-icon">✅</span>
                <h3 id="chrModalTitle" class="chr-modal-title">موفق</h3>
            </div>
            <div id="chrModalBody" class="chr-modal-body"></div>
            <div class="chr-modal-footer">
                <button id="chrModalBtn" class="chr-modal-btn success" onclick="closeModal();">متوجه شدم</button>
            </div>
        </div>
    </div>

    <div id="chrToastContainer" class="chr-toast-container"></div>

    <div id="chrLoading" class="chr-loading">
        <div class="chr-loading-content">
            <div class="chr-spinner"></div>
            <div class="chr-loading-text">⏳ در حال انجام...</div>
        </div>
    </div>

    <script type="text/javascript">
        function showModal(type, title, bodyHtml, buttonText) {
            var modal = document.getElementById('chrModal');
            var header = document.getElementById('chrModalHeader');
            var icon = document.getElementById('chrModalIcon');
            var titleEl = document.getElementById('chrModalTitle');
            var body = document.getElementById('chrModalBody');
            var btn = document.getElementById('chrModalBtn');
            if (!modal) return;
            if (modal.parentNode !== document.body) document.body.appendChild(modal);
            header.className = 'chr-modal-header ' + type;
            btn.className = 'chr-modal-btn ' + type;
            var icons = { 'success': '✅', 'error': '❌', 'warning': '⚠️', 'info': 'ℹ️' };
            icon.innerText = icons[type] || 'ℹ️';
            titleEl.innerText = title;
            body.innerHTML = bodyHtml;
            btn.innerText = buttonText || 'متوجه شدم';
            modal.classList.add('show');
            document.body.style.overflow = 'hidden';
        }

        function closeModal() {
            var modal = document.getElementById('chrModal');
            if (!modal) return;
            modal.classList.remove('show');
            document.body.style.overflow = '';
        }

        function showToast(type, message, duration) {
            var container = document.getElementById('chrToastContainer');
            if (!container) return;
            if (container.parentNode !== document.body) document.body.appendChild(container);
            var toast = document.createElement('div');
            toast.className = 'chr-toast ' + type;
            var icons = { 'success': '✅', 'error': '❌', 'warning': '⚠️', 'info': 'ℹ️' };
            toast.innerHTML = '<span class="chr-toast-icon">' + (icons[type] || 'ℹ️') + '</span>' +
                             '<span class="chr-toast-message">' + message + '</span>' +
                             '<span class="chr-toast-close" onclick="this.parentElement.remove();">✕</span>';
            container.appendChild(toast);
            if (duration !== 0) {
                setTimeout(function () { if (toast.parentElement) toast.remove(); }, duration || 5000);
            }
        }

        function showLoading() {
            var l = document.getElementById('chrLoading');
            if (l) l.classList.add('show');
        }

        function hideLoading() {
            var l = document.getElementById('chrLoading');
            if (l) l.classList.remove('show');
        }

        // ✅ تابع عمومی برای نمایش اطلاعات فایل انتخاب شده
        function onFileSelected(input, infoId, nameId) {
            var info = document.getElementById(infoId);
            var nameEl = document.getElementById(nameId);
            if (!info || !nameEl) return;
            if (input.files && input.files[0]) {
                var file = input.files[0];
                var fileSize = (file.size / 1024).toFixed(2);
                nameEl.innerHTML = file.name + ' (' + fileSize + ' KB)';
                info.style.display = 'block';
            } else {
                info.style.display = 'none';
            }
        }

        (function () {
            function relocate() {
                try {
                    var modal = document.getElementById('chrModal');
                    var toast = document.getElementById('chrToastContainer');
                    var loading = document.getElementById('chrLoading');
                    if (modal && modal.parentNode !== document.body) document.body.appendChild(modal);
                    if (toast && toast.parentNode !== document.body) document.body.appendChild(toast);
                    if (loading && loading.parentNode !== document.body) document.body.appendChild(loading);
                } catch (e) { console.error(e); }
            }
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', relocate);
            } else { relocate(); }
            if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    relocate();
                    hideLoading();
                });
            }
        })();
    </script>

</asp:Content>