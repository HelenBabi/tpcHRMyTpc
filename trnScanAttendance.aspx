<%@ Page Title="ثبت حضور با QR Code" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnScanAttendance.aspx.vb" Inherits="trnScanAttendance" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .scan-container {
            max-width: 700px;
            margin: 40px auto;
            padding: 30px;
            background: white;
            border-radius: 15px;
            box-shadow: 0 2px 20px rgba(0,0,0,0.1);
            text-align: center;
        }
        .scan-icon { font-size: 64px; margin-bottom: 15px; }
        .scan-title { font-size: 22px; font-weight: bold; color: #2c3e50; margin-bottom: 20px; }
        .scan-status {
            padding: 15px;
            border-radius: 10px;
            margin: 15px 0;
            font-size: 16px;
            font-weight: bold;
        }
        .scan-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .scan-error { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .scan-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .scan-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .info-box {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 10px;
            margin: 10px 0;
            text-align: right;
        }
        .info-box h4 { margin: 0 0 10px 0; color: #2c3e50; }
        .info-row {
            display: flex;
            justify-content: space-between;
            padding: 6px 0;
            border-bottom: 1px solid #eee;
        }
        .info-row:last-child { border-bottom: none; }
        .info-label { font-weight: bold; color: #555; }
        .info-value { color: #2c3e50; }
        
        .btn-scan {
            padding: 12px 30px;
            font-size: 16px;
            border: none;
            border-radius: 30px;
            cursor: pointer;
            transition: all 0.3s;
            margin: 5px;
            text-decoration: none;
            display: inline-block;
        }
        .btn-scan:hover { transform: scale(1.05); box-shadow: 0 4px 15px rgba(0,0,0,0.2); }
        .btn-checkin { background: #27ae60; color: white; }
        .btn-checkout { background: #e74c3c; color: white; }
        .btn-home { 
            background: #9b59b6; 
            color: white; 
            padding: 12px 40px;
            font-size: 16px;
            border: none;
            border-radius: 30px;
            cursor: pointer;
            transition: all 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        .btn-home:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
            background: #8e44ad;
        }
        .btn-attachment {
            background: #9b59b6;
            color: white;
            padding: 12px 30px;
            font-size: 16px;
            border: none;
            border-radius: 30px;
            cursor: pointer;
            transition: all 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        .btn-attachment:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
            background: #8e44ad;
        }
        .btn-download {
            background: #27ae60;
            color: white;
            padding: 5px 20px;
            font-size: 13px;
            border: none;
            border-radius: 20px;
            cursor: pointer;
            transition: all 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        .btn-download:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
        }
        .btn-download-all {
            background: #9b59b6;
            color: white;
            padding: 10px 30px;
            font-size: 14px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            transition: all 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        .btn-download-all:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
        }
        .time-display { font-size: 14px; color: #888; margin-top: 10px; }
        
        .warning-box {
            background: #fff3cd;
            border: 2px solid #ffc107;
            border-radius: 10px;
            padding: 15px;
            margin: 15px 0;
            text-align: right;
        }
        .warning-box h4 { color: #856404; margin-top: 0; }
        .warning-box p { font-size: 13px; color: #856404; margin: 5px 0; }
        
        /* بخش فایل‌های ضمیمه */
        .attachments-section {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }
        .attachments-section h3 {
            border-bottom: 2px solid #9b59b6;
            padding-bottom: 10px;
            color: #2c3e50;
            margin-bottom: 15px;
            text-align: right;
        }
        .attachment-item {
            background: #f8f9fa;
            padding: 12px 15px;
            margin-bottom: 8px;
            border-radius: 8px;
            border-right: 4px solid #9b59b6;
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            text-align: right;
        }
        .attachment-item:hover {
            background: #e8f5e9;
        }
        .attachment-info {
            display: flex;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
        }
        .attachment-icon { font-size: 20px; }
        .attachment-name { font-weight: bold; color: #2c3e50; }
        .attachment-size { color: #888; font-size: 12px; }
        .attachment-date { color: #999; font-size: 11px; }
        .no-attachment {
            text-align: center;
            color: #888;
            padding: 15px;
            background: #f8f9fa;
            border-radius: 8px;
        }
        .loading-attachments {
            text-align: center;
            padding: 20px;
            display: none;
        }
        .loading-attachments span {
            font-size: 20px;
        }
        .loading-attachments .loading-text {
            font-size: 16px;
            color: #888;
            margin-right: 10px;
        }
        
        /* فرم ارزیابی */
        .evaluation-form {
            margin-top: 25px;
            padding-top: 25px;
            border-top: 2px solid #e0e0e0;
            text-align: right;
        }
        .evaluation-form h3 {
            color: #2c3e50;
            margin-bottom: 20px;
            font-size: 18px;
        }
        .eval-item {
            background: #f8f9fa;
            padding: 12px 15px;
            margin-bottom: 10px;
            border-radius: 8px;
            border-right: 4px solid #3498db;
            text-align: right;
        }
        .eval-item label {
            font-weight: bold;
            color: #2c3e50;
            display: block;
            margin-bottom: 5px;
            font-size: 14px;
        }
        .eval-item .range-container {
            display: flex;
            align-items: center;
            gap: 10px;
            justify-content: flex-start;
        }
        .eval-item .range-container input[type="range"] {
            flex: 1;
            max-width: 300px;
            direction: ltr;
            cursor: pointer;
        }
        .eval-item .range-value {
            font-weight: bold;
            color: #3498db;
            min-width: 30px;
            font-size: 16px;
        }
        .eval-item .range-labels {
            display: flex;
            justify-content: space-between;
            font-size: 11px;
            color: #888;
            max-width: 300px;
            margin-top: 2px;
        }
        .eval-item .range-labels span:first-child { color: #27ae60; }
        .eval-item .range-labels span:last-child { color: #e74c3c; }
        .eval-item .range-labels span:nth-child(2) { color: #f39c12; }
        
        .eval-score {
            background: #e8f4fd;
            padding: 10px 15px;
            border-radius: 8px;
            margin: 15px 0;
            text-align: center;
        }
        .eval-score strong {
            font-size: 16px;
            color: #2c3e50;
        }
        .eval-score span {
            font-size: 20px;
            font-weight: bold;
            color: #3498db;
        }
        
        .eval-suggestions textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 8px;
            font-family: inherit;
            font-size: 14px;
            resize: vertical;
            min-height: 60px;
            text-align: right;
        }
        .eval-suggestions textarea:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 5px rgba(52,152,219,0.3);
        }
        .eval-suggestions label {
            font-weight: bold;
            display: block;
            margin-bottom: 5px;
            text-align: right;
            color: #2c3e50;
        }
        
        .footer-text {
            margin-top: 20px;
            font-size: 12px;
            color: #999;
        }
        
        .eval-submit-area {
            margin-top: 15px;
            display: flex;
            justify-content: center;
            gap: 10px;
            flex-wrap: wrap;
        }
        
        .btn-eval-submit {
            background: #27ae60;
            color: white;
            padding: 12px 40px;
            font-size: 16px;
            border: none;
            border-radius: 30px;
            cursor: pointer;
            transition: all 0.3s;
        }
        .btn-eval-submit:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
            background: #219a52;
        }
        .btn-eval-submit:disabled {
            opacity: 0.6;
            cursor: not-allowed;
        }
        
        .eval-already-done {
            background: #d4edda;
            padding: 15px;
            border-radius: 8px;
            color: #155724;
            border: 1px solid #c3e6cb;
            text-align: center;
        }
        .eval-already-done .checkmark {
            font-size: 30px;
        }
        .eval-already-done p {
            margin: 5px 0;
        }
        .eval-already-done .btn-home {
            padding: 10px 30px;
            font-size: 14px;
        }
        
        .text-right { text-align: right; }
        .text-center { text-align: center; }
        .mt-10 { margin-top: 10px; }
        .mb-10 { margin-bottom: 10px; }
        .btn-back-top {
            text-align: left;
            margin-bottom: 15px;
        }
        .btn-back-top .btn-home {
            padding: 8px 20px;
            font-size: 14px;
        }
        
        @media (max-width: 600px) {
            .attachment-item {
                flex-direction: column;
                align-items: stretch;
                gap: 10px;
            }
            .attachment-info {
                justify-content: center;
            }
            .btn-download {
                text-align: center;
            }
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="scan-container">
        <div class="scan-icon">📱</div>
        <div class="scan-title">ثبت حضور با QR Code</div>
        
        <div id="divStatus" runat="server" class="scan-status">در حال بارگذاری...</div>

        <!-- اطلاعات پرسنل -->
        <div id="divPersonInfo" runat="server" class="info-box" visible="false">
            <h4>👤 اطلاعات پرسنل</h4>
            <div class="info-row">
                <span class="info-label">کد پرسنلی:</span>
                <span class="info-value" id="lblCode" runat="server"></span>
            </div>
            <div class="info-row">
                <span class="info-label">نام و نام خانوادگی:</span>
                <span class="info-value" id="lblName" runat="server"></span>
            </div>
        </div>

        <!-- اطلاعات دوره -->
        <div id="divCourseInfo" runat="server" class="info-box" visible="false">
            <h4>🎓 اطلاعات دوره</h4>
            <div class="info-row">
                <span class="info-label">عنوان دوره:</span>
                <span class="info-value" id="lblCourseName" runat="server"></span>
            </div>
            <div class="info-row">
                <span class="info-label">تاریخ:</span>
                <span class="info-value" id="lblCourseDate" runat="server"></span>
            </div>
            <div class="info-row">
                <span class="info-label">مکان:</span>
                <span class="info-value" id="lblCourseLocation" runat="server"></span>
            </div>
            <div class="info-row">
                <span class="info-label">مدرس:</span>
                <span class="info-value" id="lblCourseTeacher" runat="server"></span>
            </div>
        </div>

        <!-- هشدار امنیتی -->
        <div class="warning-box">
            <h4>⚠️ نکته امنیتی</h4>
            <p>
                فقط خودتان می‌توانید حضور خود را ثبت کنید. 
                کد پرسنلی شما: <strong id="lblPersonCodeDisplay" runat="server"></strong>
            </p>
        </div>

        <!-- دکمه‌های ورود و خروج -->
        <!-- دکمه‌های ورود و خروج - تغییر یافته -->
<div id="divAction" runat="server" visible="false" style="margin-top: 20px;">
    <asp:Button ID="btnCheckIn" runat="server" Text="✅ ثبت حضور" 
        CssClass="btn-scan btn-checkin" 
        OnClick="btnCheckIn_Click" 
        OnClientClick="disableButton(this);" 
        UseSubmitBehavior="false" 
        style="font-size: 18px; padding: 15px 50px;" />
    <br />
    <div class="time-display" id="lblCurrentTime" runat="server"></div>
</div>

        <!-- دکمه مشاهده فایل‌های ضمیمه -->
        <div id="divAttachmentButton" runat="server" visible="false" style="margin-top: 20px;">
            <asp:Button ID="btnShowAttachments" runat="server" 
                CssClass="btn-attachment" 
                OnClick="btnShowAttachments_Click" />
        </div>

        <!-- بخش فایل‌های ضمیمه (بارگذاری با UpdatePanel) -->
        <asp:UpdatePanel ID="upAttachments" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnShowAttachments" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div id="divAttachments" runat="server" class="attachments-section" visible="false">
                    
                    <!-- نشانگر بارگذاری -->
                    <div id="divLoading" runat="server" class="loading-attachments">
                        <span>⏳</span>
                        <span class="loading-text">در حال بارگذاری فایل‌ها...</span>
                    </div>
                    
                    <h3>📎 فایل‌های ضمیمه دوره</h3>
                    
                    <<asp:Repeater ID="rptAttachments" runat="server">
    <ItemTemplate>
        <div class="attachment-item">
            <div class="attachment-info">
                <span class="attachment-icon"><%# GetFileIcon(Eval("content_name")) %></span>
                <span class="attachment-name"><%# Eval("content_name") %></span>
                <span class="attachment-size">(حجم: <%# FormatFileSize(Eval("size")) %>)</span>
                <span class="attachment-date">تاریخ: <%# Convert.ToDateTime(Eval("mdate")).ToString("yyyy/MM/dd") %></span>
            </div>
            <a href="DownloadFile.aspx?id=<%# Eval("id") %>" class="btn-download" target="_blank">
                📥 دانلود
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>
                    
                    <!-- پیغام عدم وجود فایل -->
                    <div id="divNoAttachment" runat="server" class="no-attachment">
                        ⚠️ هیچ فایل ضمیمه‌ای برای این دوره وجود ندارد.
                    </div>
                    
                    <!-- دکمه دانلود همه فایل‌ها -->
                    <div style="text-align: center; margin-top: 15px;">
                        <asp:LinkButton ID="btnDownloadAllAttachments" runat="server" 
                            CssClass="btn-download-all" 
                            OnClick="btnDownloadAllAttachments_Click"
                            Visible="false">
                            📦 دانلود همه فایل‌ها (ZIP)
                        </asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- فرم ارزیابی -->
        <div id="divEvaluation" runat="server" class="evaluation-form" visible="false">
            <div class="btn-back-top">
                <asp:Button ID="btnBackTop" runat="server" Text="🏠 بازگشت به صفحه اصلی" 
                    CssClass="btn-home" 
                    OnClick="btnBackToHome_Click" />
            </div>

            <h3>📝 فرم ارزیابی دوره آموزشی</h3>
            
            <!-- سوال 1 -->
            <div class="eval-item">
                <label>1. میزان تناسب دوره آموزشی با نیاز آموزشی و مسئولیت شغلی شما</label>
                <div class="range-container">
                    <input type="range" id="eval1" runat="server" min="2" max="10" step="1" value="6" 
                           oninput="updateRangeValue(this, 'val1')" 
                           onchange="updateRangeValue(this, 'val1')" />
                    <span class="range-value" id="val1">6</span>
                </div>
                <div class="range-labels">
                    <span>عالی (10)</span>
                    <span>متوسط</span>
                    <span>ضعیف (2)</span>
                </div>
            </div>

            <!-- سوال 2 -->
            <div class="eval-item">
                <label>2. ارزش کاربردی یا میزان عملی بودن مطالب ارائه شده</label>
                <div class="range-container">
                    <input type="range" id="eval2" runat="server" min="2" max="10" step="1" value="6" 
                           oninput="updateRangeValue(this, 'val2')" 
                           onchange="updateRangeValue(this, 'val2')" />
                    <span class="range-value" id="val2">6</span>
                </div>
                <div class="range-labels">
                    <span>عالی (10)</span>
                    <span>متوسط</span>
                    <span>ضعیف (2)</span>
                </div>
            </div>

            <!-- سوال 3 -->
            <div class="eval-item">
                <label>3. میزان تاثیر دوره آموزش در افزایش مهارت های شغلی</label>
                <div class="range-container">
                    <input type="range" id="eval3" runat="server" min="3" max="15" step="1" value="9" 
                           oninput="updateRangeValue(this, 'val3')" 
                           onchange="updateRangeValue(this, 'val3')" />
                    <span class="range-value" id="val3">9</span>
                </div>
                <div class="range-labels">
                    <span>عالی (15)</span>
                    <span>متوسط</span>
                    <span>ضعیف (3)</span>
                </div>
            </div>

            <!-- سوال 4 -->
            <div class="eval-item">
                <label>4. تنظیم محتوای دوره براساس پاسخگوئی به نیازهای محیط کار و نیازهای فراگیر</label>
                <div class="range-container">
                    <input type="range" id="eval4" runat="server" min="3" max="15" step="1" value="9" 
                           oninput="updateRangeValue(this, 'val4')" 
                           onchange="updateRangeValue(this, 'val4')" />
                    <span class="range-value" id="val4">9</span>
                </div>
                <div class="range-labels">
                    <span>عالی (15)</span>
                    <span>متوسط</span>
                    <span>ضعیف (3)</span>
                </div>
            </div>

            <!-- سوال 5 -->
            <div class="eval-item">
                <label>5. میزان اطلاعات و دانش مدرس دوره در رابطه با محتوای فنی و علمی مرتبط با دوره آموزشی</label>
                <div class="range-container">
                    <input type="range" id="eval5" runat="server" min="2" max="10" step="1" value="6" 
                           oninput="updateRangeValue(this, 'val5')" 
                           onchange="updateRangeValue(this, 'val5')" />
                    <span class="range-value" id="val5">6</span>
                </div>
                <div class="range-labels">
                    <span>عالی (10)</span>
                    <span>متوسط</span>
                    <span>ضعیف (2)</span>
                </div>
            </div>

            <!-- سوال 6 -->
            <div class="eval-item">
                <label>6. میزان توانایی مدرس در مشارکت دهی و پاسخگویی به سوالات و ارائه راهکارهای منطقی و عملی</label>
                <div class="range-container">
                    <input type="range" id="eval6" runat="server" min="2" max="10" step="1" value="6" 
                           oninput="updateRangeValue(this, 'val6')" 
                           onchange="updateRangeValue(this, 'val6')" />
                    <span class="range-value" id="val6">6</span>
                </div>
                <div class="range-labels">
                    <span>عالی (10)</span>
                    <span>متوسط</span>
                    <span>ضعیف (2)</span>
                </div>
            </div>

            <!-- سوال 7 -->
            <div class="eval-item">
                <label>7. قدرت بیان و انتقال مطلب و تسلط مدرس دوره آموزشی بر موضوع تدریس</label>
                <div class="range-container">
                    <input type="range" id="eval7" runat="server" min="3" max="15" step="1" value="9" 
                           oninput="updateRangeValue(this, 'val7')" 
                           onchange="updateRangeValue(this, 'val7')" />
                    <span class="range-value" id="val7">9</span>
                </div>
                <div class="range-labels">
                    <span>عالی (15)</span>
                    <span>متوسط</span>
                    <span>ضعیف (3)</span>
                </div>
            </div>

            <!-- سوال 8 -->
            <div class="eval-item">
                <label>8. میزان اشتراک گذاری تجارب فنی توسط مدرس درطول دوره آموزشی و راهنمایی مراحل اجرای کار</label>
                <div class="range-container">
                    <input type="range" id="eval8" runat="server" min="3" max="15" step="1" value="9" 
                           oninput="updateRangeValue(this, 'val8')" 
                           onchange="updateRangeValue(this, 'val8')" />
                    <span class="range-value" id="val8">9</span>
                </div>
                <div class="range-labels">
                    <span>عالی (15)</span>
                    <span>متوسط</span>
                    <span>ضعیف (3)</span>
                </div>
            </div>

            <!-- جمع نمره -->
            <div class="eval-score">
                <strong>جمع نمره ارزیابی مدرس:</strong> 
                <span id="lblTotalScoreDisplay">0</span>
            </div>

            <!-- پیشنهادات -->
            <div class="eval-suggestions">
                <label>💡 پیشنهادات:</label>
                <textarea id="txtSuggestions" runat="server" placeholder="پیشنهادات خود را برای بهبود دوره وارد کنید..." rows="3"></textarea>
            </div>

            <!-- دکمه‌ها -->
            <div class="eval-submit-area">
                <asp:Button ID="btnSubmitEvaluation" runat="server" Text="📊 ثبت ارزیابی" 
                    CssClass="btn-eval-submit" 
                    OnClick="btnSubmitEvaluation_Click" 
                    OnClientClick="return validateEvaluation();" />
                
                <asp:Button ID="btnBackFromEvaluation" runat="server" Text="🏠 بازگشت به صفحه اصلی" 
                    CssClass="btn-home" 
                    OnClick="btnBackToHome_Click" />
            </div>
        </div>

        <!-- نمایش ارزیابی قبلی -->
        <div id="divEvaluationDone" runat="server" class="eval-already-done" visible="false">
            <div class="checkmark">✅</div>
            <p>شما قبلاً این دوره را ارزیابی کرده‌اید.</p>
            <p style="font-size:14px; margin-top:5px;">امتیاز شما: <strong id="lblPreviousScore" runat="server"></strong></p>
            
            <div style="margin-top: 15px;">
                <asp:Button ID="btnBackFromDone" runat="server" Text="🏠 بازگشت به صفحه اصلی" 
                    CssClass="btn-home" 
                    OnClick="btnBackToHome_Click" />
            </div>
        </div>

        <!-- دکمه بازگشت -->
        <div id="divBack" runat="server" visible="false" style="margin-top: 20px;">
            <asp:Button ID="btnBackToHome" runat="server" Text="🏠 بازگشت به صفحه اصلی" 
                CssClass="btn-scan btn-home" 
                OnClick="btnBackToHome_Click" />
        </div>
        
        <div class="footer-text">
            <span id="lblVersion" runat="server">نسخه 1.0</span>
        </div>
    </div>

    <asp:HiddenField ID="hdnPersonCode" runat="server" Value="" />
    <asp:HiddenField ID="hdnCourseId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAttendanceId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEvaluationId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnHasAttachments" runat="server" Value="0" />
    
    <script type="text/javascript">
        function disableButton(btn) {
            btn.disabled = true;
            btn.value = "⏳ در حال ثبت...";
            return true;
        }

        function updateRangeValue(range, displayId) {
            var display = document.getElementById(displayId);
            if (display) {
                display.textContent = range.value;
            }
            calculateTotal();
        }

        function calculateTotal() {
            var total = 0;
            var values = document.querySelectorAll('.range-value');
            values.forEach(function (el) {
                var val = parseInt(el.textContent);
                if (!isNaN(val)) {
                    total += val;
                }
            });
            var totalDisplay = document.getElementById('lblTotalScoreDisplay');
            if (totalDisplay) {
                totalDisplay.textContent = total;
            }
            return total;
        }

        function validateEvaluation() {
            calculateTotal();
            return true;
        }

        document.addEventListener('DOMContentLoaded', function () {
            calculateTotal();
        });
    </script>
</asp:Content>