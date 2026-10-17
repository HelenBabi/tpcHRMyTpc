<%@ Page Title="بارگذاری سوالات آزمون" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="ExamUpload.aspx.vb" Inherits="ExamUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .upload-container {
            max-width: 900px;
            margin: 0 auto;
            padding: 20px;
        }
        .page-header {
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            color: white;
            padding: 25px 30px;
            border-radius: 15px;
            margin-bottom: 30px;
        }
        .page-header h2 { margin: 0; font-size: 24px; }
        .page-header p { margin: 10px 0 0; opacity: 0.9; }
        
        .upload-area {
            border: 3px dashed #ddd;
            padding: 60px 20px;
            text-align: center;
            border-radius: 15px;
            transition: all 0.3s;
            cursor: pointer;
            background: #fafafa;
        }
        .upload-area:hover {
            border-color: #1e3c72;
            background: #f0f8ff;
        }
        .upload-area i {
            font-size: 64px;
            color: #1e3c72;
        }
        .upload-area h4 { margin-top: 15px; color: #333; }
        .upload-area .text-muted { color: #999; }
        
        .template-download {
            background: #f8f9fa;
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            border: 1px solid #e0e0e0;
        }
        .btn-template {
            background: #28a745;
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 8px;
            transition: all 0.3s;
            cursor: pointer;
        }
        .btn-template:hover { background: #218838; color: white; }
        
        .result-box {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-top: 20px;
            border: 1px solid #e0e0e0;
        }
        .result-box .success { color: #28a745; }
        .result-box .error { color: #dc3545; }
        .result-box .warning { color: #ffc107; }
        
        .btn-upload {
            background: #1e3c72;
            color: white;
            border: none;
            padding: 12px 40px;
            border-radius: 8px;
            transition: all 0.3s;
            font-size: 16px;
            cursor: pointer;
            margin-top: 15px;
        }
        .btn-upload:hover { background: #2a5298; color: white; }
        
        .alert {
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            display: none;
        }
        .alert-success { background: #d4edda; color: #155724; border: 2px solid #28a745; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 2px solid #dc3545; }
        .alert-warning { background: #fff3cd; color: #856404; border: 2px solid #ffc107; }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 2px solid #17a2b8; }
        .btn-close {
            float: left;
            background: none;
            border: none;
            font-size: 24px;
            cursor: pointer;
            color: #000;
            opacity: 0.5;
            padding: 0 10px;
        }
        .btn-close:hover { opacity: 1; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="upload-container">
        
        <div class="page-header">
            <h2>📤 بارگذاری سوالات آزمون</h2>
            <p>بارگذاری سوالات آزمون از طریق فایل اکسل</p>
        </div>
        
        <!-- پیام‌ها -->
        <asp:Panel ID="pnlAlert" runat="server" CssClass="alert" role="alert" style="display:none;">
            <button type="button" class="btn-close" onclick="this.parentElement.style.display='none'">&times;</button>
            <asp:Label ID="lblAlertMessage" runat="server" />
        </asp:Panel>
        
        <!-- انتخاب دوره -->
        <div class="form-group mb-3">
            <label class="form-label">انتخاب دوره <span class="text-danger">*</span></label>
            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" style="width:100%;padding:10px;">
                <asp:ListItem Value="0" Text="-- انتخاب دوره --" />
            </asp:DropDownList>
        </div>
        
        <!-- دانلود قالب -->
        <div class="template-download">
            <div class="row">
                <div class="col-md-8">
                    <h5><i class="mdi mdi-file-excel"></i> قالب فایل اکسل</h5>
                    <p class="text-muted">قالب استاندارد سوالات آزمون را دانلود کنید و بر اساس آن سوالات خود را طراحی کنید.</p>
                </div>
                <div class="col-md-4 text-end">
                    <asp:LinkButton ID="btnDownloadTemplate" runat="server" 
                        CssClass="btn-template" OnClick="btnDownloadTemplate_Click">
                        <i class="mdi mdi-download"></i> دانلود قالب
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        
        <!-- آپلود فایل -->
        <div class="upload-area" id="dropZone">
            <i class="mdi mdi-cloud-upload"></i>
            <h4>فایل اکسل را اینجا بکشید و رها کنید</h4>
            <p class="text-muted">یا کلیک کنید تا فایل انتخاب کنید</p>
            <asp:FileUpload ID="fuExcel" runat="server" CssClass="d-none" />
            <asp:Button ID="btnUpload" runat="server" Text="📤 بارگذاری فایل" 
                CssClass="btn-upload" OnClick="btnUpload_Click" />
            <br />
            <small class="text-muted">فایل‌های مجاز: .xlsx, .xls | حداکثر حجم: ۵ مگابایت</small>
        </div>
        
        <!-- نتیجه بارگذاری -->
        <asp:Panel ID="pnlResult" runat="server" Visible="false" CssClass="result-box">
            <asp:Label ID="lblResult" runat="server" />
        </asp:Panel>
        
    </div>
    
    <script type="text/javascript">
        var dropZone = document.getElementById('dropZone');
        var fileInput = document.getElementById('<%= fuExcel.ClientID %>');
        var btnUpload = document.getElementById('<%= btnUpload.ClientID %>');

        if (dropZone && fileInput) {
            dropZone.addEventListener('click', function () {
                fileInput.click();
            });
            dropZone.addEventListener('dragover', function (e) {
                e.preventDefault();
                this.style.borderColor = '#1e3c72';
                this.style.background = '#e8f0fe';
            });
            dropZone.addEventListener('dragleave', function (e) {
                e.preventDefault();
                this.style.borderColor = '#ddd';
                this.style.background = 'transparent';
            });
            dropZone.addEventListener('drop', function (e) {
                e.preventDefault();
                this.style.borderColor = '#ddd';
                this.style.background = 'transparent';
                if (e.dataTransfer.files.length) {
                    fileInput.files = e.dataTransfer.files;
                    if (btnUpload) btnUpload.click();
                }
            });
            fileInput.addEventListener('change', function () {
                if (this.files.length && btnUpload) btnUpload.click();
            });
        }
    </script>
    
</asp:Content>