<%@ Page Title="مدیریت دوره‌های آموزشی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="CourseManagement.aspx.vb" Inherits="CourseManagement" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* استایل‌های عمومی */
        .form-section {
            background: #fff;
            padding: 25px;
            border-radius: 12px;
            margin-bottom: 20px;
            border: 1px solid #e0e0e0;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        .form-section .form-label {
            font-weight: 600;
            color: #333;
        }
        .form-section .form-control, .form-section .form-select {
            border-radius: 8px;
            border: 1px solid #ddd;
            padding: 8px 12px;
            transition: all 0.3s;
        }
        .form-section .form-control:focus, .form-section .form-select:focus {
            border-color: #1e3c72;
            box-shadow: 0 0 0 0.2rem rgba(30,60,114,0.25);
        }
        
        .course-selector {
            background: #f8f9fa;
            padding: 15px 20px;
            border-radius: 8px;
            border: 1px solid #e0e0e0;
            margin-bottom: 20px;
        }
        .course-selector .form-label {
            font-weight: 600;
            color: #1e3c72;
            display: block;
            margin-bottom: 5px;
        }
        
        .btn-success-custom {
            background: #28a745;
            border: none;
            color: #fff;
            padding: 10px 30px;
            border-radius: 8px;
            transition: all 0.3s;
            font-weight: 600;
        }
        .btn-success-custom:hover {
            background: #218838;
            color: #fff;
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(40,167,69,0.3);
        }
        .btn-secondary {
            background: #6c757d;
            border: none;
            color: #fff;
            padding: 10px 30px;
            border-radius: 8px;
        }
        .btn-secondary:hover {
            background: #5a6268;
            color: #fff;
        }
        .btn-primary-custom {
            background: #1e3c72;
            border: none;
            color: #fff;
            padding: 8px 20px;
            border-radius: 8px;
            transition: all 0.3s;
        }
        .btn-primary-custom:hover {
            background: #2a5298;
            color: #fff;
        }
        .btn-info-custom {
            background: #17a2b8;
            border: none;
            color: #fff;
            padding: 10px 30px;
            border-radius: 8px;
        }
        .btn-info-custom:hover {
            background: #138496;
            color: #fff;
        }
        .btn-danger-custom {
            background: #dc3545;
            border: none;
            color: #fff;
            padding: 5px 10px;
            border-radius: 4px;
        }
        .btn-danger-custom:hover {
            background: #c82333;
            color: #fff;
        }
        
        .text-danger { color: #dc3545; }
        .text-muted { color: #6c757d; font-size: 12px; }
        .text-success { color: #28a745; }
        
        .form-check { padding-right: 25px; }
        .form-check-input { margin-right: -20px; margin-top: 5px; }
        .form-check-label { margin-right: 5px; font-weight: 500; }
        
        .mt-3 { margin-top: 15px; }
        .mt-4 { margin-top: 20px; }
        .mb-3 { margin-bottom: 15px; }
        
        .row { display: flex; flex-wrap: wrap; margin: 0 -10px; }
        .col-md-4 { width: 33.333%; padding: 0 10px; }
        .col-md-6 { width: 50%; padding: 0 10px; }
        .col-md-8 { width: 66.666%; padding: 0 10px; }
        .col-md-12 { width: 100%; padding: 0 10px; }
        
        .mdi { margin-left: 5px; }
        hr { margin: 20px 0; border: 0; border-top: 1px solid #e0e0e0; }
        
        .nav-tabs .nav-link { 
            color: #333; 
            border: none; 
            padding: 12px 20px; 
            border-radius: 8px 8px 0 0; 
            transition: all 0.3s; 
            cursor: pointer; 
            background: transparent;
            text-decoration: none;
            display: inline-block;
        }
        .nav-tabs .nav-link:hover { background: #f0f0f0; color: #1e3c72; }
        .nav-tabs .nav-link.active { background: #1e3c72; color: #fff; border: none; }
        .nav-tabs .nav-link i { margin-left: 8px; }
        
        .table-responsive { overflow-x: auto; }
        .table th, .table td { padding: 12px; vertical-align: middle; }
        .table-striped tbody tr:nth-of-type(odd) { background-color: rgba(0,0,0,0.02); }
        .table-hover tbody tr:hover { background-color: rgba(30,60,114,0.05); }
        
        .status-badge { padding: 4px 12px; border-radius: 20px; font-size: 12px; display: inline-block; }
        .status-active { background: #d4edda; color: #155724; }
        .status-inactive { background: #f8d7da; color: #721c24; }
        .status-pending { background: #fff3cd; color: #856404; }
        
        .alert { 
            padding: 15px 20px; 
            border-radius: 10px; 
            margin-bottom: 15px;
            animation: slideDown 0.5s ease-out;
            font-weight: bold;
            font-size: 15px;
            display: none;
        }
        .alert-success { 
            background: #d4edda; 
            color: #155724; 
            border: 2px solid #28a745;
            box-shadow: 0 4px 15px rgba(40,167,69,0.2);
        }
        .alert-danger { 
            background: #f8d7da; 
            color: #721c24; 
            border: 2px solid #dc3545;
            box-shadow: 0 4px 15px rgba(220,53,69,0.2);
        }
        .alert-warning { 
            background: #fff3cd; 
            color: #856404; 
            border: 2px solid #ffc107;
            box-shadow: 0 4px 15px rgba(255,193,7,0.2);
        }
        .alert-info { 
            background: #d1ecf1; 
            color: #0c5460; 
            border: 2px solid #17a2b8;
            box-shadow: 0 4px 15px rgba(23,162,184,0.2);
        }
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
        
        .file-upload-area {
            border: 2px dashed #ddd;
            padding: 40px;
            text-align: center;
            border-radius: 12px;
            transition: all 0.3s;
            cursor: pointer;
        }
        .file-upload-area:hover {
            border-color: #1e3c72;
            background: #f8f9fa;
        }
        .file-upload-area i {
            font-size: 48px;
            color: #1e3c72;
        }
        .uploaded-file {
            background: #e8f0fe;
            padding: 10px 15px;
            border-radius: 8px;
            margin: 5px 0;
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
        }
        
        .question-item {
            background: #fff;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            padding: 15px;
            margin-bottom: 10px;
        }
        .question-item .question-text {
            font-weight: 600;
            color: #1e3c72;
        }
        
        .badge {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            display: inline-block;
        }
        .badge-primary { background: #1e3c72; color: #fff; }
        .badge-success { background: #28a745; color: #fff; }
        .badge-danger { background: #dc3545; color: #fff; }
        .badge-warning { background: #ffc107; color: #000; }
        .badge-info { background: #17a2b8; color: #fff; }
        
        .float-end { float: left; }
        .text-end { text-align: left; }
        
        .btn-action { 
            padding: 5px 10px; 
            border-radius: 4px; 
            margin: 2px; 
            border: none; 
            color: #fff;
            display: inline-block;
        }
        .btn-action i { margin-left: 3px; }
        .input-group { display: flex; align-items: center; }
        .input-group .form-control { flex: 1; }
        
        .page-title {
            color: #1e3c72;
            font-weight: bold;
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 3px solid #1e3c72;
        }
        
        .update-progress {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: rgba(0,0,0,0.7);
            color: #fff;
            padding: 20px 40px;
            border-radius: 10px;
            z-index: 9999;
            display: none;
        }
        .update-progress .spinner {
            border: 4px solid #f3f3f3;
            border-top: 4px solid #1e3c72;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1s linear infinite;
            margin: 0 auto 10px;
        }
        
        .text-large-textarea {
            font-size: 16px !important;
            padding: 15px !important;
            line-height: 2 !important;
            min-height: 120px !important;
        }
        
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        @keyframes slideDown {
            from { opacity: 0; transform: translateY(-30px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        @media (max-width: 768px) {
            .col-md-4, .col-md-6, .col-md-8 { width: 100%; }
            .text-end { text-align: right !important; }
            .nav-tabs .nav-link { padding: 8px 12px; font-size: 13px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="update-progress" style="display:block;">
                <div class="spinner"></div>
                <div style="text-align:center;">در حال بارگذاری...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-md-12">
                <h2 class="page-title">
                    <i class="mdi mdi-book-open-page-variant"></i> مدیریت دوره‌های آموزشی
                </h2>
                <p class="text-muted">ایجاد، ویرایش و مدیریت دوره‌های آموزشی</p>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <!-- پیام‌های هشدار -->
                <asp:Panel ID="pnlAlert" runat="server" CssClass="alert" role="alert" style="display:none;">
                    <button type="button" class="btn-close" onclick="this.parentElement.style.display='none'">&times;</button>
                    <asp:Label ID="lblAlertMessage" runat="server" />
                </asp:Panel>

                <!-- تب‌ها -->
                <ul class="nav nav-tabs mb-3" id="courseTabs" role="tablist">
                    <li class="nav-item">
                        <asp:LinkButton ID="tabList" runat="server" CssClass="nav-link active" OnClick="tab_Click" CommandArgument="list">
                            <i class="mdi mdi-format-list-bulleted"></i> لیست دوره‌ها
                        </asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="tabNew" runat="server" CssClass="nav-link" OnClick="tab_Click" CommandArgument="new">
                            <i class="mdi mdi-plus-circle"></i> دوره جدید
                        </asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="tabFiles" runat="server" CssClass="nav-link" OnClick="tab_Click" CommandArgument="files">
                            <i class="mdi mdi-file-upload"></i> فایل‌ها
                        </asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="tabEvaluation" runat="server" CssClass="nav-link" OnClick="tab_Click" CommandArgument="evaluation">
                            <i class="mdi mdi-check-circle"></i> سوالات ارزیابی
                        </asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="tabExam" runat="server" CssClass="nav-link" OnClick="tab_Click" CommandArgument="exam">
                            <i class="mdi mdi-pencil"></i> سوالات آزمون
                        </asp:LinkButton>
                    </li>
                </ul>

                <div class="tab-content">
                    
                    <!-- ============================================================ -->
                    <!-- TAB 1: لیست دوره‌ها -->
                    <!-- ============================================================ -->
                    <asp:Panel ID="pnlList" runat="server" CssClass="tab-pane active" style="display:block;">
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="جستجوی دوره..." />
                                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary-custom" OnClick="btnSearch_Click">
                                        <i class="mdi mdi-magnify"></i> جستجو
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-6 text-end">
                                <asp:LinkButton ID="btnNewCourse" runat="server" CssClass="btn btn-success-custom" OnClick="tab_Click" CommandArgument="new">
                                    <i class="mdi mdi-plus"></i> دوره جدید
                                </asp:LinkButton>
                            </div>
                        </div>
                        
                        <div class="table-responsive">
                            <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-hover table-striped" 
                                OnRowCommand="gvCourses_RowCommand"
                                OnRowDataBound="gvCourses_RowDataBound"
                                GridLines="None"
                                DataKeyNames="CourseID">
                                <Columns>
                                    <asp:BoundField DataField="CourseID" HeaderText="کد" />
                                    <asp:BoundField DataField="Title" HeaderText="عنوان دوره" />
                                    <asp:BoundField DataField="StartDate" HeaderText="تاریخ شروع" />
                                    <asp:BoundField DataField="EndDate" HeaderText="تاریخ پایان" />
                                    <asp:TemplateField HeaderText="وضعیت">
                                        <ItemTemplate>
                                            <span class='status-badge status-<%# Eval("StatusClass") %>'>
                                                <%# Eval("StatusText") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عملیات">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary-custom btn-action" 
                                                CommandName="EditCourse" CommandArgument='<%# Eval("CourseID") %>'>
                                                <i class="mdi mdi-pencil"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger-custom btn-action"
                                                CommandName="DeleteCourse" CommandArgument='<%# Eval("CourseID") %>'
                                                OnClientClick="return confirm('آیا از حذف این دوره اطمینان دارید؟');">
                                                <i class="mdi mdi-delete"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnSelectCourse" runat="server" CssClass="btn btn-success-custom btn-action"
                                                CommandName="SelectCourse" CommandArgument='<%# Eval("CourseID") %>'>
                                                <i class="mdi mdi-check"></i> انتخاب
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <asp:Label ID="lblRecordCount" runat="server" CssClass="text-muted" />
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ============================================================ -->
                    <!-- TAB 2: دوره جدید -->
                    <!-- ============================================================ -->
                    <asp:Panel ID="pnlNew" runat="server" CssClass="tab-pane" style="display:none;">
                        <div class="form-section">
                            <h5 class="mb-3"><i class="mdi mdi-plus-circle"></i> اطلاعات دوره جدید</h5>
                            
                            <div class="row">
                                <div class="col-md-8 mb-3">
                                    <label class="form-label">عنوان دوره <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="200" placeholder="عنوان دوره را وارد کنید" />
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                                        ControlToValidate="txtTitle" 
                                        ErrorMessage="عنوان دوره الزامی است"
                                        CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-md-4 mb-3">
                                    <label class="form-label">وضعیت</label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="فعال" />
                                        <asp:ListItem Value="0" Text="غیرفعال" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 mb-3">
                                    <label class="form-label">توضیحات دوره</label>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" 
                                        TextMode="MultiLine" Rows="4" placeholder="توضیحات دوره را وارد کنید" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <label class="form-label">تاریخ شروع <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date" />
                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server"
                                        ControlToValidate="txtStartDate"
                                        ErrorMessage="تاریخ شروع الزامی است"
                                        CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-md-4 mb-3">
                                    <label class="form-label">تاریخ پایان <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date" />
                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server"
                                        ControlToValidate="txtEndDate"
                                        ErrorMessage="تاریخ پایان الزامی است"
                                        CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-md-4 mb-3">
                                    <label class="form-label">نمره قبولی</label>
                                    <asp:TextBox ID="txtMinPassScore" runat="server" CssClass="form-control" Text="60" />
                                    <small class="text-muted">حداقل نمره برای قبولی (پیش‌فرض: 60)</small>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <div class="form-check">
                                        <asp:CheckBox ID="chkHasEvaluation" runat="server" CssClass="form-check-input" />
                                        <label class="form-check-label">دارای ارزیابی</label>
                                        <br />
                                        <small class="text-muted">ارزیابی قبل از آزمون الزامی است</small>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <div class="form-check">
                                        <asp:CheckBox ID="chkHasExam" runat="server" CssClass="form-check-input" OnCheckedChanged="chkHasExam_CheckedChanged" AutoPostBack="true" />
                                        <label class="form-check-label">دارای آزمون</label>
                                        <br />
                                        <small class="text-muted">آزمون نهایی دوره</small>
                                    </div>
                                </div>
                            </div>

                            <div id="examSettings" runat="server" style="display:none;">
                                <hr />
                                <h6 class="mb-3"><i class="mdi mdi-clock"></i> تنظیمات آزمون</h6>
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label">زمان شروع آزمون</label>
                                        <asp:TextBox ID="txtExamStart" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label">زمان پایان آزمون</label>
                                        <asp:TextBox ID="txtExamEnd" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label">مدت زمان (دقیقه)</label>
                                        <asp:TextBox ID="txtExamDuration" runat="server" CssClass="form-control" Text="30" />
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-3" id="divSaveStatus" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblSaveStatus" runat="server" CssClass="text-success" Font-Bold="true" Font-Size="16px" />
                                </div>
                            </div>

                            <div class="row mt-4">
                                <div class="col-md-12">
                                    <asp:Button ID="btnSaveCourse" runat="server" Text="ذخیره دوره" 
                                        CssClass="btn btn-success-custom" OnClick="btnSaveCourse_Click" />
                                    
                                    <asp:Button ID="btnResetCourse" runat="server" Text="انصراف" 
                                        CssClass="btn btn-secondary" OnClick="btnResetCourse_Click" 
                                        CausesValidation="false" />
                                    
                                    <asp:LinkButton ID="btnBackToList" runat="server" CssClass="btn btn-info-custom" 
                                        OnClick="tab_Click" CommandArgument="list" CausesValidation="false">
                                        <i class="mdi mdi-arrow-right"></i> بازگشت به لیست
                                    </asp:LinkButton>
                                    
                                    <asp:HiddenField ID="hfCourseID" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ============================================================ -->
                    <!-- TAB 3: فایل‌ها -->
                    <!-- ============================================================ -->
                    <asp:Panel ID="pnlFiles" runat="server" CssClass="tab-pane" style="display:none;">
                        <div class="course-selector">
                            <div class="row">
                                <div class="col-md-8">
                                    <label class="form-label"><i class="mdi mdi-book-open"></i> انتخاب دوره:</label>
                                    <asp:DropDownList ID="ddlFileCourse" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFileCourse_SelectedIndexChanged" style="width:100%;min-width:200px;">
                                        <asp:ListItem Value="0" Text="-- انتخاب دوره --" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4" style="display:flex;align-items:flex-end;">
                                    <asp:LinkButton ID="btnRefreshFiles" runat="server" CssClass="btn btn-primary-custom" OnClick="btnRefreshFiles_Click">
                                        <i class="mdi mdi-refresh"></i> بروزرسانی
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="mt-2">
                                <asp:Label ID="lblSelectedFileCourse" runat="server" CssClass="text-muted" />
                            </div>
                        </div>

                        <div class="form-section">
                            <h5 class="mb-3"><i class="mdi mdi-file-upload"></i> بارگذاری فایل‌های دوره</h5>
                            
                            <asp:Label ID="lblFilesMessage" runat="server" CssClass="text-muted" />
                            
                            <div class="file-upload-area" id="dropZone">
                                <i class="mdi mdi-cloud-upload"></i>
                                <h5>فایل را اینجا بکشید و رها کنید</h5>
                                <p class="text-muted">یا کلیک کنید تا فایل انتخاب کنید</p>
                                <asp:FileUpload ID="fuCourseFile" runat="server" CssClass="d-none" />
                                <asp:Button ID="btnUploadFile" runat="server" Text="بارگذاری فایل" 
                                    CssClass="btn btn-primary-custom mt-3" OnClick="btnUploadFile_Click" />
                                <br />
                                <small class="text-muted">حداکثر حجم: 50 مگابایت | انواع مجاز: PDF, DOC, DOCX, PPT, PPTX, XLS, XLSX, MP4, AVI, MKV, JPG, PNG</small>
                            </div>

                            <div class="mt-4">
                                <h6>فایل‌های بارگذاری شده</h6>
                                <asp:Repeater ID="rptFiles" runat="server" OnItemCommand="rptFiles_ItemCommand">
                                    <ItemTemplate>
                                        <div class="uploaded-file">
                                            <span class="file-name">
                                                <i class="mdi mdi-file"></i>
                                                <%# Eval("FileName") %>
                                            </span>
                                            <span class="file-size">
                                                <%# FormatFileSize(Eval("FileSize")) %>
                                            </span>
                                            <span class="file-date">
                                                <%# Convert.ToDateTime(Eval("UploadDate")).ToString("yyyy/MM/dd HH:mm") %>
                                            </span>
                                            <div>
                                                <a href='<%# ResolveUrl(Eval("FilePath").ToString()) %>' target="_blank" class="btn btn-info-custom btn-action">
                                                    <i class="mdi mdi-eye"></i>
                                                </a>
                                                <asp:LinkButton ID="btnDeleteFile" runat="server"
                                                    CssClass="btn btn-danger-custom btn-action"
                                                    CommandName="DeleteFile" CommandArgument='<%# Eval("FileID") %>'
                                                    OnClientClick="return confirm('آیا از حذف این فایل اطمینان دارید؟');">
                                                    <i class="mdi mdi-delete"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ============================================================ -->
                    <!-- TAB 4: سوالات ارزیابی -->
                    <!-- ============================================================ -->
                    <asp:Panel ID="pnlEvaluation" runat="server" CssClass="tab-pane" style="display:none;">
                        <div class="course-selector">
                            <div class="row">
                                <div class="col-md-8">
                                    <label class="form-label"><i class="mdi mdi-book-open"></i> انتخاب دوره:</label>
                                    <asp:DropDownList ID="ddlEvalCourse" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEvalCourse_SelectedIndexChanged" style="width:100%;min-width:200px;">
                                        <asp:ListItem Value="0" Text="-- انتخاب دوره --" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4" style="display:flex;align-items:flex-end;">
                                    <asp:LinkButton ID="btnRefreshEval" runat="server" CssClass="btn btn-primary-custom" OnClick="btnRefreshEval_Click">
                                        <i class="mdi mdi-refresh"></i> بروزرسانی
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="mt-2">
                                <asp:Label ID="lblSelectedEvalCourse" runat="server" CssClass="text-muted" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-section">
                                    <h5 class="mb-3"><i class="mdi mdi-check-circle"></i> لیست سوالات ارزیابی</h5>
                                    <asp:Label ID="lblEvalMessage" runat="server" CssClass="text-muted" />
                                    <asp:Repeater ID="rptEvaluationQuestions" runat="server" OnItemCommand="rptEvaluationQuestions_ItemCommand">
                                        <ItemTemplate>
                                            <div class="question-item">
                                                <div class="question-text">
                                                    <%# Container.ItemIndex + 1 %> - <%# Eval("QuestionText") %>
                                                    <span class="badge badge-danger float-end">
                                                        <%# IIf(Eval("IsRequired"), "اجباری", "اختیاری") %>
                                                    </span>
                                                </div>
                                                <div class="mt-2">
                                                    <asp:Label ID="lblOptions" runat="server" Text='<%# Eval("Options") %>' />
                                                </div>
                                                <div class="mt-2">
                                                    <asp:LinkButton ID="btnDeleteEvalQuestion" runat="server"
                                                        CssClass="btn btn-danger-custom btn-action"
                                                        CommandName="DeleteEvalQuestion" CommandArgument='<%# Eval("QuestionID") %>'
                                                        OnClientClick="return confirm('آیا از حذف این سوال اطمینان دارید؟');">
                                                        <i class="mdi mdi-delete"></i> حذف
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-section">
                                    <h5 class="mb-3"><i class="mdi mdi-plus"></i> افزودن سوال ارزیابی</h5>
                                    <div class="mb-3">
                                        <label class="form-label">متن سوال <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtEvalQuestion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                                    </div>
                                    <div class="mb-3">
                                        <label class="form-label">نوع سوال</label>
                                        <asp:DropDownList ID="ddlEvalType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="MultipleChoice" Text="چند گزینه‌ای" />
                                            <asp:ListItem Value="Rating" Text="امتیازی" />
                                            <asp:ListItem Value="Text" Text="متن باز" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="mb-3">
                                        <div class="form-check">
                                            <asp:CheckBox ID="chkEvalRequired" runat="server" CssClass="form-check-input" Checked="true" />
                                            <label class="form-check-label">اجباری</label>
                                        </div>
                                    </div>
                                    <div class="mb-3">
                                        <label class="form-label">گزینه‌ها (هر خط یک گزینه)</label>
                                        <asp:TextBox ID="txtEvalOptions" runat="server" CssClass="form-control"
                                            TextMode="MultiLine" Rows="4" 
                                            placeholder="گزینه 1&#10;گزینه 2&#10;گزینه 3" />
                                    </div>
                                    <asp:Button ID="btnAddEvalQuestion" runat="server" Text="افزودن سوال" 
                                        CssClass="btn btn-primary-custom" OnClick="btnAddEvalQuestion_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- ============================================================ -->
                    <!-- TAB 5: سوالات آزمون -->
                    <!-- ============================================================ -->
                    <asp:Panel ID="pnlExam" runat="server" CssClass="tab-pane" style="display:none;">
                        <div class="course-selector">
                            <div class="row">
                                <div class="col-md-8">
                                    <label class="form-label"><i class="mdi mdi-book-open"></i> انتخاب دوره:</label>
                                    <asp:DropDownList ID="ddlExamCourse" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamCourse_SelectedIndexChanged" style="width:100%;min-width:200px;">
                                        <asp:ListItem Value="0" Text="-- انتخاب دوره --" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4" style="display:flex;align-items:flex-end;">
                                    <asp:LinkButton ID="btnRefreshExam" runat="server" CssClass="btn btn-primary-custom" OnClick="btnRefreshExam_Click">
                                        <i class="mdi mdi-refresh"></i> بروزرسانی
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="mt-2">
                                <asp:Label ID="lblSelectedExamCourse" runat="server" CssClass="text-muted" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-section">
                                    <h5 class="mb-3"><i class="mdi mdi-pencil"></i> لیست سوالات آزمون</h5>
                                    <asp:Label ID="lblExamMessage" runat="server" CssClass="text-muted" />
                                    <asp:Repeater ID="rptExamQuestions" runat="server" OnItemCommand="rptExamQuestions_ItemCommand">
                                        <ItemTemplate>
                                            <div class="question-item">
                                                <div class="question-text">
                                                    <%# Container.ItemIndex + 1 %> - <%# Eval("QuestionText") %>
                                                    <span class="badge badge-primary float-end">نمره: <%# Eval("Score") %></span>
                                                </div>
                                                <div class="mt-2">
                                                    <asp:Label ID="lblExamOptions" runat="server" Text='<%# Eval("Options") %>' />
                                                </div>
                                                <div class="mt-2">
                                                    <asp:LinkButton ID="btnDeleteExamQuestion" runat="server"
                                                        CssClass="btn btn-danger-custom btn-action"
                                                        CommandName="DeleteExamQuestion" CommandArgument='<%# Eval("QuestionID") %>'
                                                        OnClientClick="return confirm('آیا از حذف این سوال اطمینان دارید؟');">
                                                        <i class="mdi mdi-delete"></i> حذف
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-section">
                                    <h5 class="mb-3"><i class="mdi mdi-plus"></i> افزودن سوال آزمون</h5>
                                    
                                    <div class="mb-3">
                                        <label class="form-label">متن سوال <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtExamQuestion" runat="server" CssClass="form-control text-large-textarea" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <label class="form-label">نوع سوال</label>
                                            <asp:DropDownList ID="ddlExamType" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="MultipleChoice" Text="چند گزینه‌ای" />
                                                <asp:ListItem Value="TrueFalse" Text="صحیح/غلط" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label class="form-label">نمره</label>
                                            <asp:TextBox ID="txtExamScore" runat="server" CssClass="form-control" Text="1" />
                                        </div>
                                    </div>
                                    
                                    <hr />
                                    
                                    <h6 class="mb-2">افزودن گزینه‌ها</h6>
                                    <div class="row mb-2">
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtOptionText" runat="server" CssClass="form-control" placeholder="متن گزینه" />
                                        </div>
                                        <div class="col-md-3" style="padding-top: 5px;">
                                            <div class="form-check">
                                                <asp:CheckBox ID="chkCorrectOption" runat="server" CssClass="form-check-input" />
                                                <label class="form-check-label" style="font-size: 12px;">پاسخ صحیح</label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAddOption" runat="server" Text="افزودن" CssClass="btn btn-success btn-sm w-100" OnClick="btnAddOption_Click" />
                                        </div>
                                    </div>
                                    
                                    <div class="text-end mb-2">
                                        <asp:LinkButton ID="btnClearOptions" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnClearOptions_Click" CausesValidation="false">
                                            <i class="mdi mdi-delete"></i> پاک کردن همه
                                        </asp:LinkButton>
                                    </div>
                                    
                                    <asp:GridView ID="gvOptions" runat="server" AutoGenerateColumns="False" 
                                        CssClass="table table-bordered table-sm" OnRowCommand="gvOptions_RowCommand"
                                        EnableViewState="true">
                                        <Columns>
                                            <asp:BoundField DataField="RowNo" HeaderText="#" />
                                            <asp:BoundField DataField="OptionText" HeaderText="متن گزینه" />
                                            <asp:CheckBoxField DataField="IsCorrect" HeaderText="صحیح" />
                                            <asp:TemplateField HeaderText="عملیات">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDeleteOption" runat="server" 
                                                        Text="حذف" CssClass="btn btn-danger btn-sm"
                                                        CommandName="DeleteOption" CommandArgument='<%# Eval("RowNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    
                                    <hr />
                                    
                                    <asp:Button ID="btnAddExamQuestion" runat="server" Text="ذخیره سوال" 
                                        CssClass="btn btn-primary-custom w-100" OnClick="btnAddExamQuestion_Click" />
                                    
                                    <small class="text-muted mt-2 d-block">
                                        <i class="mdi mdi-information"></i> 
                                        برای افزودن سوال، ابتدا گزینه‌ها را اضافه کرده و حداقل یک گزینه را به عنوان پاسخ صحیح انتخاب کنید.
                                    </small>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUploadFile" />
                <asp:PostBackTrigger ControlID="btnSaveCourse" />
                <asp:PostBackTrigger ControlID="btnAddExamQuestion" />
                <asp:PostBackTrigger ControlID="btnAddEvalQuestion" />
               
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        // تابع برای نمایش/مخفی کردن تنظیمات آزمون
        function toggleExamSettings() {
            var chkExam = document.getElementById('<%= chkHasExam.ClientID %>');
            var examSettings = document.getElementById('examSettings');
            if (chkExam && examSettings) {
                if (chkExam.checked) {
                    examSettings.style.display = 'block';
                } else {
                    examSettings.style.display = 'none';
                }
            }
        }

        // تابع برای اجرا بعد از هر PostBack (برای UpdatePanel)
        function pageLoad() {
            toggleExamSettings();

            var chkExam = document.getElementById('<%= chkHasExam.ClientID %>');
            if (chkExam) {
                chkExam.removeEventListener('change', toggleExamSettings);
                chkExam.addEventListener('change', function () {
                    toggleExamSettings();
                });
            }
        }

        // اجرا بعد از بارگذاری کامل صفحه
        document.addEventListener('DOMContentLoaded', function () {
            toggleExamSettings();

            var chkExam = document.getElementById('<%= chkHasExam.ClientID %>');
            if (chkExam) {
                chkExam.addEventListener('change', function () {
                    toggleExamSettings();
                });
            }

            // آپلود فایل با Drag & Drop
            var dropZone = document.getElementById('dropZone');
            var fileInput = document.getElementById('<%= fuCourseFile.ClientID %>');
            var btnUpload = document.getElementById('<%= btnUploadFile.ClientID %>');

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
        });
    </script>
</asp:Content>