<%@ Page Title="چاپ QR Code حضور و غیاب" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnPrintQRCode.aspx.vb" Inherits="trnPrintQRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .print-container {
            max-width: 800px;
            margin: 20px auto;
            padding: 20px;
            background: white;
            border-radius: 15px;
            box-shadow: 0 2px 20px rgba(0,0,0,0.1);
        }
        .print-header {
            background: linear-gradient(135deg, #9b59b6 0%, #8e44ad 100%);
            color: white;
            padding: 15px 20px;
            border-radius: 10px 10px 0 0;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
        }
        .print-body {
            padding: 25px;
            text-align: center;
        }
        .course-info {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 10px;
            text-align: right;
            margin-bottom: 20px;
        }
        .course-info .row {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
            padding: 5px 0;
        }
        .course-info .label {
            font-weight: bold;
            color: #555;
        }
        .qr-box {
            border: 3px dashed #ddd;
            border-radius: 15px;
            padding: 30px;
            margin: 20px 0;
            background: #fafafa;
        }
        .qr-box img {
            max-width: 350px;
            max-height: 350px;
            border: 3px solid #ddd;
            border-radius: 15px;
            padding: 15px;
            background: white;
        }
        .btn-group {
            display: flex;
            gap: 10px;
            justify-content: center;
            flex-wrap: wrap;
            margin-top: 20px;
        }
        .btn-print {
            padding: 12px 30px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            font-size: 16px;
            transition: all 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        .btn-print:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
        }
        .btn-primary { background: #3498db; color: white; }
        .btn-success { background: #27ae60; color: white; }
        .btn-warning { background: #f39c12; color: white; }
        .btn-danger { background: #e74c3c; color: white; }
        .btn-back { background: #95a5a6; color: white; }
        .btn-scanner { background: #e67e22; color: white; }
        .btn-scanner:hover { background: #d35400; }
        
        .info-box {
            background: #d1ecf1;
            border: 1px solid #bee5eb;
            border-radius: 10px;
            padding: 15px;
            text-align: right;
            margin: 15px 0;
        }
        .info-box .label { font-weight: bold; color: #0c5460; }
        
        .instructions {
            background: #fff3cd;
            border: 1px solid #ffc107;
            border-radius: 10px;
            padding: 15px;
            text-align: right;
            margin: 15px 0;
        }
        .instructions ul {
            padding-right: 20px;
            margin: 10px 0;
        }
        .instructions li {
            padding: 5px 0;
        }
        
        /* ✅ استایل فیلد انتخاب دوره - افزایش عرض */
        .form-control {
            padding: 10px 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            background: white;
        }
        .filter-row {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
            align-items: center;
            justify-content: center;
        }
        
        /* ✅ عرض لیست دوره را بزرگتر می‌کنیم */
        #cmbCourses {
            width: 100% !important;
            max-width: 450px !important;
            min-width: 300px !important;
            padding: 10px 15px !important;
            font-size: 14px !important;
        }
        
        /* برای نمایش کامل عنوان دوره در لیست */
        .filter-row .form-control {
            width: 100%;
            max-width: 450px;
            min-width: 300px;
        }
        
        /* استایل آیتم‌های لیست */
        #cmbCourses option {
            padding: 8px 10px;
            font-size: 14px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        
        @media print {
            .no-print { display: none !important; }
            .print-container { box-shadow: none; border: none; }
            .qr-box { border: 1px solid #ddd; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="print-container">
        <div class="print-header">
            📱 چاپ QR Code حضور و غیاب
        </div>
        
        <div class="print-body">
            
            <!-- انتخاب دوره -->
            <div class="course-info no-print">
                <h4 style="margin-top: 0; color: #2c3e50;">انتخاب دوره آموزشی</h4>
                <div class="filter-row">
                    <asp:DropDownList ID="cmbCourses" runat="server" CssClass="form-control" Height="50px">
                    </asp:DropDownList>
                    <asp:Button ID="btnShowQR" runat="server" Text="نمایش QR Code" CssClass="btn-print btn-primary" OnClick="btnShowQR_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="🔄 بروزرسانی" CssClass="btn-print btn-warning" OnClick="btnRefresh_Click" />
                </div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style="display: block; margin-top: 10px;"></asp:Label>
            </div>

            <!-- اطلاعات دوره -->
            <div class="course-info" id="divCourseInfo" runat="server" visible="false">
                <h3 id="lblCourseTitle" runat="server" style="color: #2c3e50; text-align: center; margin-bottom: 15px;"></h3>
                <div class="row">
                    <span><span class="label">📅 تاریخ شروع:</span> <span id="lblStartDate" runat="server"></span></span>
                    <span><span class="label">📅 تاریخ اتمام:</span> <span id="lblEndDate" runat="server"></span></span>
                </div>
                <div class="row">
                    <span><span class="label">📍 مکان:</span> <span id="lblLocation" runat="server"></span></span>
                    <span><span class="label">👨‍🏫 مدرس:</span> <span id="lblTeacher" runat="server"></span></span>
                </div>
                <div class="row">
                    <span><span class="label">🕐 ساعت:</span> <span id="lblTime" runat="server"></span></span>
                    <span><span class="label">🔢 کد دوره:</span> <span id="lblCourseId" runat="server" style="font-weight: bold; color: #e74c3c;"></span></span>
                </div>
            </div>

            <!-- QR Code -->
            <div class="qr-box" id="divQRCode" runat="server" visible="false">
                <asp:Image ID="imgQRCode" runat="server" />
                <div style="margin-top: 15px; font-size: 14px; color: #888;">
                    <span id="lblQRText" runat="server"></span>
                </div>
            </div>

            <!-- لینک اسکن -->
            <div class="info-box" id="divLink" runat="server" visible="false">
                <span class="label">🔗 لینک اسکن QR Code:</span>
                <br />
                <asp:Label ID="lblQRUrl" runat="server" ForeColor="#0c5460" Font-Bold="true" Style="word-break: break-all; display: block; margin-top: 5px;"></asp:Label>
                <br />
                <asp:HyperLink ID="hlScanLink" runat="server" 
                    CssClass="btn-print btn-scanner" 
                    Target="_blank"
                    style="display: inline-block; text-decoration: none;">
                    📱 رفتن به صفحه ثبت حضور و ارزیابی
                </asp:HyperLink>
            </div>

            <!-- دستورالعمل استفاده -->
            <div class="instructions" id="divInstructions" runat="server" visible="false">
                <h4 style="margin-top: 0; color: #856404;">📌 دستورالعمل استفاده</h4>
                <ul>
                    <li>✅ پرسنل با اسکن این کد در کلاس حضور خود را ثبت می‌کنند</li>
                    <li>✅ هر پرسنل باید یک بار برای <strong>ورود</strong> و یک بار برای <strong>خروج</strong> اسکن کند</li>
                    <li>✅ زمان ورود و خروج به صورت خودکار ثبت می‌شود</li>
                    <li>✅ برای اسکن کد از نرم‌افزارهای اسکن QR Code استفاده کنید</li>
                </ul>
            </div>

            <!-- دکمه‌های اقدام -->
            <div class="btn-group no-print" id="divButtons" runat="server" visible="false">
                <asp:LinkButton ID="btnPrint" runat="server" CssClass="btn-print btn-primary" OnClick="btnPrint_Click">
                    🖨️ چاپ QR Code
                </asp:LinkButton>
                <asp:LinkButton ID="btnDownload" runat="server" CssClass="btn-print btn-success" OnClick="btnDownload_Click">
                    📥 دانلود QR Code
                </asp:LinkButton>
                <asp:LinkButton ID="btnPrintInstructions" runat="server" CssClass="btn-print btn-warning" OnClick="btnPrintInstructions_Click">
                    📄 چاپ با دستورالعمل
                </asp:LinkButton>
                <asp:LinkButton ID="btnBack" runat="server" CssClass="btn-print btn-back" OnClick="btnBack_Click">
                    ↩ بازگشت
                </asp:LinkButton>
            </div>

            <!-- Hidden Fields -->
            <asp:HiddenField ID="hdnCourseId" runat="server" Value="" />
            <asp:HiddenField ID="hdnCourseName" runat="server" Value="" />
        </div>
    </div>
</asp:Content>