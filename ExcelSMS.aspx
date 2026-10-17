<%@ Page Title="ارسال پیامک گروهی از اکسل" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExcelSMS.aspx.vb" Inherits="ExcelSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .excel-container {
            max-width: 1100px;
            margin: 30px auto;
            padding: 25px 30px;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.08);
            direction: rtl;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .excel-title {
            font-size: 22px;
            font-weight: 700;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e9ecef;
        }
        
        /* ============================================ */
        /* کارت‌ها */
        /* ============================================ */
        .excel-card {
            background: #f8f9fa;
            border-radius: 12px;
            padding: 20px 25px;
            margin-bottom: 20px;
            border-right: 4px solid #3498db;
        }
        
        .excel-card h4 {
            color: #2c3e50;
            margin: 0 0 15px 0;
            font-size: 15px;
            font-weight: 600;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        
        .excel-form-group {
            margin-bottom: 18px;
        }
        
        .excel-form-group label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 8px;
            font-size: 14px;
        }
        
        .excel-form-control {
            width: 100%;
            padding: 10px 15px;
            border: 1.5px solid #ddd;
            border-radius: 8px;
            font-family: inherit;
            font-size: 14px;
            box-sizing: border-box;
            transition: all 0.2s;
        }
        
        .excel-form-control:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        
        textarea.excel-form-control {
            resize: vertical;
            min-height: 100px;
            line-height: 1.8;
        }
        
        /* ============================================ */
        /* دکمه‌ها */
        /* ============================================ */
        .excel-btn {
            padding: 10px 25px;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            font-family: inherit;
            display: inline-flex;
            align-items: center;
            gap: 8px;
        }
        
        .excel-btn:hover:not(:disabled) {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(0,0,0,0.15);
        }
        
        .excel-btn:disabled {
            opacity: 0.6;
            cursor: not-allowed;
        }
        
        .excel-btn-primary { background: #3498db; color: white; }
        .excel-btn-primary:hover:not(:disabled) { background: #2980b9; }
        
        .excel-btn-success { background: #27ae60; color: white; }
        .excel-btn-success:hover:not(:disabled) { background: #219a52; }
        
        .excel-btn-warning { background: #f39c12; color: white; }
        .excel-btn-warning:hover:not(:disabled) { background: #e67e22; }
        
        .excel-btn-danger { background: #e74c3c; color: white; }
        .excel-btn-danger:hover:not(:disabled) { background: #c0392b; }
        
        .excel-btn-info { background: #17a2b8; color: white; }
        .excel-btn-info:hover:not(:disabled) { background: #138496; }
        
        .excel-btn-secondary { background: #95a5a6; color: white; }
        .excel-btn-secondary:hover:not(:disabled) { background: #7f8c8d; }
        
        /* ============================================ */
        /* هشدارها */
        /* ============================================ */
        .excel-alert {
            padding: 12px 18px;
            border-radius: 8px;
            margin: 15px 0;
            font-size: 14px;
            display: flex;
            align-items: flex-start;
            gap: 10px;
            line-height: 1.7;
        }
        
        .excel-alert-info { background: #d1ecf1; color: #0c5460; border-right: 4px solid #17a2b8; }
        .excel-alert-success { background: #d4edda; color: #155724; border-right: 4px solid #27ae60; }
        .excel-alert-danger { background: #f8d7da; color: #721c24; border-right: 4px solid #e74c3c; }
        .excel-alert-warning { background: #fff3cd; color: #856404; border-right: 4px solid #f39c12; }
        
        .excel-alert .alert-icon { font-size: 20px; flex-shrink: 0; }
        
        /* ============================================ */
        /* نوار پیشرفت */
        /* ============================================ */
        .excel-progress {
            width: 100%;
            height: 25px;
            background: #e9ecef;
            border-radius: 12px;
            overflow: hidden;
            margin: 15px 0;
            display: none;
        }
        
        .excel-progress.show {
            display: block;
        }
        
        .excel-progress-bar {
            height: 100%;
            background: linear-gradient(90deg, #3498db, #27ae60);
            border-radius: 12px;
            transition: width 0.3s;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 12px;
            font-weight: 700;
        }
        
        /* ============================================ */
        /* جدول */
        /* ============================================ */
        .excel-table-responsive {
            overflow-x: auto;
            max-height: 400px;
            overflow-y: auto;
            border: 1px solid #e9ecef;
            border-radius: 8px;
        }
        
        .excel-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            text-align: right;
        }
        
        .excel-table th {
            background: #2c3e50;
            color: white;
            padding: 10px 12px;
            text-align: center;
            font-weight: 600;
            position: sticky;
            top: 0;
            z-index: 1;
        }
        
        .excel-table td {
            padding: 10px 12px;
            border-bottom: 1px solid #e9ecef;
            text-align: center;
            vertical-align: middle;
        }
        
        .excel-table tr:hover {
            background: #f8f9fa;
        }
        
        .excel-table .national-code {
            font-family: 'Courier New', monospace;
            font-weight: 600;
            direction: ltr;
        }
        
        .badge-valid { background: #d4edda; color: #155724; padding: 3px 10px; border-radius: 12px; font-size: 11px; font-weight: 600; }
        .badge-invalid { background: #f8d7da; color: #721c24; padding: 3px 10px; border-radius: 12px; font-size: 11px; font-weight: 600; }
        
        /* ============================================ */
        /* آمار */
        /* ============================================ */
        .excel-summary {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
            margin-bottom: 20px;
        }
        
        .excel-summary-item {
            background: white;
            padding: 15px 20px;
            border-radius: 10px;
            border: 1px solid #e9ecef;
            flex: 1;
            min-width: 150px;
            text-align: center;
            border-top: 4px solid #3498db;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        
        .excel-summary-item .number {
            font-size: 28px;
            font-weight: 700;
            color: #2c3e50;
            margin-bottom: 5px;
        }
        
        .excel-summary-item .label {
            font-size: 12px;
            color: #6c757d;
            font-weight: 600;
        }
        
        .excel-summary-item.blue { border-top-color: #3498db; }
        .excel-summary-item.green { border-top-color: #27ae60; }
        .excel-summary-item.orange { border-top-color: #f39c12; }
        .excel-summary-item.red { border-top-color: #e74c3c; }
        
        /* ============================================ */
        /* آپلود */
        /* ============================================ */
        .excel-upload-area {
            border: 2px dashed #3498db;
            border-radius: 12px;
            padding: 30px;
            text-align: center;
            background: #e8f4fd;
            cursor: pointer;
            transition: all 0.3s;
            margin-bottom: 15px;
        }
        
        .excel-upload-area:hover {
            background: #d1ecf1;
            border-color: #2980b9;
        }
        
        .excel-upload-area .icon {
            font-size: 50px;
            display: block;
            margin-bottom: 10px;
        }
        
        .excel-upload-area .title {
            font-size: 16px;
            font-weight: 700;
            color: #2c3e50;
            margin-bottom: 8px;
        }
        
        .excel-upload-area .subtitle {
            font-size: 13px;
            color: #6c757d;
        }
        
        .excel-file-input {
            display: none;
        }
        
        .excel-file-name {
            display: inline-block;
            padding: 8px 15px;
            background: #27ae60;
            color: white;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            margin-top: 10px;
        }
        
        /* ============================================ */
        /* لودینگ */
        /* ============================================ */
        .excel-loading {
            display: none;
            text-align: center;
            padding: 30px;
        }
        
        .excel-loading.show { display: block; }
        
        .excel-loading .spinner {
            width: 50px;
            height: 50px;
            border: 5px solid #e0e0e0;
            border-top-color: #3498db;
            border-radius: 50%;
            animation: spin 0.8s linear infinite;
            margin: 0 auto 15px;
        }
        
        @keyframes spin { to { transform: rotate(360deg); } }
        
        .excel-loading p {
            font-size: 14px;
            color: #6c757d;
            margin: 0;
        }
        
        /* ============================================ */
        /* ریسپانسیو */
        /* ============================================ */
        @media (max-width: 768px) {
            .excel-container { padding: 15px; margin: 10px; }
            .excel-summary { flex-direction: column; }
            .excel-btn { width: 100%; justify-content: center; margin-bottom: 5px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="excel-container">
        
        <div class="excel-title">📊 ارسال پیامک گروهی از فایل اکسل</div>
        
        <!-- ============================================ -->
        <!-- پیام -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="excel-alert">
                <span class="alert-icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- مرحله 1: آپلود فایل -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlUpload" runat="server">
            <div class="excel-card">
                <h4>📁 مرحله ۱: آپلود فایل اکسل</h4>
                
                <div class="excel-alert excel-alert-info">
                    <span class="alert-icon">ℹ️</span>
                    <div>
                        فایل اکسل باید شامل <strong>4 ستون</strong> باشد:
                        <br />
                        <strong>نام</strong> | <strong>نام خانوادگی</strong> | <strong>کد پرسنلی</strong> | <strong>شماره موبایل</strong>
                        <br />
                        <small style="color:#6c757d;">سطر اول به عنوان هدر در نظر گرفته می‌شود.</small>
                    </div>
                </div>
                
                <div class="excel-form-group">
                    <label>متن پیامک:</label>
                    <asp:TextBox ID="txtSmsText" runat="server" CssClass="excel-form-control" 
                        TextMode="MultiLine" Rows="4" 
                        placeholder="متن پیامک خود را وارد کنید..." />
                    <small style="color:#6c757d; font-size:12px;">
                        💡 می‌توانید از متغیرهای <code>{نام}</code> و <code>{نام‌خانوادگی}</code> استفاده کنید.
                    </small>
                </div>
                
                <div class="excel-form-group">
                    <label>انتخاب فایل اکسل:</label>
                    
                    <div class="excel-upload-area" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();">
                        <span class="icon">📊</span>
                        <div class="title">فایل اکسل را انتخاب کنید</div>
                        <div class="subtitle">فرمت‌های مجاز: .xls, .xlsx - حداکثر 10 مگابایت</div>
                    </div>
                    
                    <asp:FileUpload ID="fileUpload" runat="server" 
                        CssClass="excel-file-input" 
                        accept=".xls,.xlsx" 
                        onchange="showFileName(this);" />
                    
                    <div style="text-align: center;">
                        <span id="fileName" class="excel-file-name" style="display:none;"></span>
                    </div>
                </div>
                
                <div style="text-align: center;">
                    <asp:Button ID="btnUpload" runat="server" Text="📤 آپلود و پیش‌نمایش" 
                        CssClass="excel-btn excel-btn-primary" 
                        OnClick="btnUpload_Click" 
                        OnClientClick="showLoading();" />
                </div>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- لودینگ -->
        <!-- ============================================ -->
        <div id="excelLoading" class="excel-loading">
            <div class="spinner"></div>
            <p>⏳ در حال پردازش فایل اکسل... لطفاً شکیبا باشید</p>
        </div>
        
        <!-- ============================================ -->
        <!-- مرحله 2: پیش‌نمایش -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlPreview" runat="server" Visible="false">
            
            <div class="excel-card">
                <h4>👁️ مرحله ۲: پیش‌نمایش داده‌ها</h4>
                
                <div class="excel-summary">
                    <div class="excel-summary-item blue">
                        <div class="number"><asp:Label ID="lblTotalRows" runat="server" Text="0" /></div>
                        <div class="label">کل رکوردها</div>
                    </div>
                    <div class="excel-summary-item green">
                        <div class="number"><asp:Label ID="lblValidRows" runat="server" Text="0" /></div>
                        <div class="label">رکوردهای معتبر</div>
                    </div>
                    <div class="excel-summary-item red">
                        <div class="number"><asp:Label ID="lblInvalidRows" runat="server" Text="0" /></div>
                        <div class="label">رکوردهای نامعتبر</div>
                    </div>
                    <div class="excel-summary-item orange">
                        <div class="number"><asp:Label ID="lblDuplicateRows" runat="server" Text="0" /></div>
                        <div class="label">تکراری‌ها</div>
                    </div>
                </div>
                
                <div class="excel-alert excel-alert-warning">
                    <span class="alert-icon">⚠️</span>
                    <div>
                        <strong>توجه:</strong> فقط رکوردهای معتبر (با شماره موبایل صحیح) ارسال خواهند شد.
                        <br />
                        <span style="color:#e74c3c;">کد پرسنلی و شماره موبایل تکراری حذف می‌شوند.</span>
                    </div>
                </div>
                
                <div class="excel-table-responsive">
                    <asp:GridView ID="gvPreview" runat="server" 
                        AutoGenerateColumns="False" 
                        CssClass="excel-table"
                        AllowPaging="False"
                        EmptyDataText="هیچ داده‌ای برای نمایش وجود ندارد">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                            <asp:BoundField DataField="FirstName" HeaderText="نام" />
                            <asp:BoundField DataField="LastName" HeaderText="نام خانوادگی" />
                            <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
                            <asp:BoundField DataField="Mobile" HeaderText="موبایل" 
                                ItemStyle-CssClass="national-code" />
                            <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <span class='<%# GetStatusBadgeClass(Eval("Status")) %>'>
                                        <%# Eval("StatusText") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                
                <div style="text-align: center; margin-top: 20px;">
                    <asp:Button ID="btnSend" runat="server" Text="📤 ارسال پیامک‌ها" 
                        CssClass="excel-btn excel-btn-success" 
                        OnClick="btnSend_Click" 
                        OnClientClick="return confirm('آیا از ارسال پیامک به این تعداد رکورد مطمئن هستید؟');" />
                    
                    <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" 
                        CssClass="excel-btn excel-btn-secondary" 
                        OnClick="btnCancel_Click" />
                </div>
            </div>
        </asp:Panel>
        
        <!-- ============================================ -->
        <!-- مرحله 3: نتیجه -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <div class="excel-card">
                <h4>✅ مرحله ۳: نتیجه ارسال</h4>
                
                <div class="excel-summary">
                    <div class="excel-summary-item green">
                        <div class="number"><asp:Label ID="lblSentCount" runat="server" Text="0" /></div>
                        <div class="label">ارسال شده</div>
                    </div>
                    <div class="excel-summary-item red">
                        <div class="number"><asp:Label ID="lblFailedCount" runat="server" Text="0" /></div>
                        <div class="label">ناموفق</div>
                    </div>
                </div>
                
                <div style="text-align: center; margin-top: 20px;">
                    <asp:Button ID="btnNewUpload" runat="server" Text="📁 آپلود فایل جدید" 
                        CssClass="excel-btn excel-btn-primary" 
                        OnClick="btnNewUpload_Click" />
                </div>
            </div>
        </asp:Panel>
        
    </div>
    
    <script type="text/javascript">
        // ============================================
        // نمایش نام فایل انتخاب شده
        // ============================================
        function showFileName(input) {
            var fileNameSpan = document.getElementById('fileName');
            if (input.files && input.files.length > 0) {
                fileNameSpan.textContent = '📄 ' + input.files[0].name;
                fileNameSpan.style.display = 'inline-block';
            } else {
                fileNameSpan.style.display = 'none';
            }
        }

        // ============================================
        // نمایش لودینگ
        // ============================================
        function showLoading() {
            var loading = document.getElementById('excelLoading');
            if (loading) {
                loading.classList.add('show');
            }
        }

        function hideLoading() {
            var loading = document.getElementById('excelLoading');
            if (loading) {
                loading.classList.remove('show');
            }
        }

        // ============================================
        // بارگذاری اولیه
        // ============================================
        window.addEventListener('load', function () {
            hideLoading();
        });
    </script>
</asp:Content>