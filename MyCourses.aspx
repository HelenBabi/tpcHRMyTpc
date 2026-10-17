<%@ Page Title="دوره‌های من" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="MyCourses.aspx.vb" Inherits="MyCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-header {
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            color: white;
            padding: 25px 30px;
            border-radius: 15px;
            margin-bottom: 30px;
        }
        .page-header h2 { margin: 0; font-size: 24px; }
        .page-header p { margin: 10px 0 0; opacity: 0.9; }
        
        .filter-section {
            background: #f8f9fa;
            padding: 15px 20px;
            border-radius: 12px;
            border: 1px solid #e0e0e0;
            margin-bottom: 20px;
        }
        .filter-section .form-label { font-weight: 600; color: #333; }
        .filter-section .form-control, .filter-section .form-select {
            border-radius: 8px;
            border: 1px solid #ddd;
            padding: 8px 12px;
        }
        
        .course-card {
            background: #fff;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            border: 1px solid #e0e0e0;
            transition: all 0.3s;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        .course-card:hover { box-shadow: 0 4px 15px rgba(0,0,0,0.1); }
        
        .course-title { font-size: 18px; font-weight: bold; color: #1e3c72; }
        .course-meta { color: #666; font-size: 13px; margin: 5px 0; }
        .course-meta i { margin-left: 5px; color: #1e3c72; }
        
        .status-badge {
            padding: 4px 12px;
            border-radius: 20px;
            font-size: 12px;
            display: inline-block;
        }
        .status-pending { background: #fff3cd; color: #856404; }
        .status-confirmed { background: #d4edda; color: #155724; }
        .status-rejected { background: #f8d7da; color: #721c24; }
        .status-evaluated { background: #cce5ff; color: #004085; }
        
        .btn-evaluate {
            background: linear-gradient(135deg, #28a745, #20c997);
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 8px;
            transition: all 0.3s;
            font-size: 13px;
            cursor: pointer;
        }
        .btn-evaluate:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(40,167,69,0.4);
            color: white;
        }
        
        .btn-register {
            background: #28a745;
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 8px;
            transition: all 0.3s;
            font-size: 13px;
            cursor: pointer;
        }
        .btn-register:hover {
            background: #218838;
            color: white;
            transform: translateY(-2px);
        }
        
        .btn-register-disabled {
            background: #6c757d;
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 8px;
            font-size: 13px;
            cursor: default;
        }
        
        .btn-exam {
            background: linear-gradient(135deg, #ff6b6b, #ee5a24);
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 8px;
            transition: all 0.3s;
            font-size: 13px;
            cursor: pointer;
        }
        .btn-exam:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(238,90,36,0.4);
            color: white;
        }
        
        .btn-exam-done {
            background: #6c757d;
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 8px;
            transition: all 0.3s;
            font-size: 13px;
            cursor: pointer;
        }
        .btn-exam-done:hover {
            background: #5a6268;
            color: white;
            transform: translateY(-2px);
        }
        
        .content-section {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 15px;
            margin-top: 15px;
            border-right: 3px solid #17a2b8;
        }
        .content-section .content-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
            flex-wrap: wrap;
        }
        .content-section .content-title {
            font-weight: bold;
            color: #1e3c72;
            font-size: 14px;
        }
        .content-section .content-progress {
            font-size: 12px;
            color: #666;
        }
        
        .content-item {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 8px 12px;
            background: white;
            border-radius: 8px;
            margin-bottom: 8px;
            border: 1px solid #e0e0e0;
            transition: all 0.3s;
            flex-wrap: wrap;
            gap: 5px;
        }
        .content-item:hover { border-color: #17a2b8; }
        .content-item .content-name {
            display: flex;
            align-items: center;
            gap: 10px;
            font-size: 13px;
            flex: 1;
        }
        .content-item .content-name .icon { font-size: 22px; }
        .content-item .content-status { font-size: 12px; }
        .content-item .content-status .viewed { color: #28a745; font-weight: bold; }
        .content-item .content-status .not-viewed { color: #dc3545; }
        
        .action-buttons {
            display: flex;
            gap: 8px;
            flex-wrap: wrap;
            margin-top: 10px;
        }
        
        .btn-sm {
            padding: 5px 14px;
            font-size: 13px;
            border-radius: 6px;
            border: none;
            cursor: pointer;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            gap: 5px;
        }
        .btn-sm:hover { transform: translateY(-2px); }
        .btn-sm-view { background: #17a2b8; color: white; }
        .btn-sm-view:hover { background: #138496; }
        .btn-sm-download { background: #28a745; color: white; }
        .btn-sm-download:hover { background: #218838; }
        .btn-sm-viewed {
            background: #d4edda;
            color: #155724;
            cursor: default;
        }
        .btn-sm-viewed:hover { transform: none; }
        
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
        
        .row {
            display: flex;
            flex-wrap: wrap;
            margin: 0 -10px;
        }
        .col-md-3 { width: 25%; padding: 0 10px; }
        .col-md-4 { width: 33.333%; padding: 0 10px; }
        .col-md-6 { width: 50%; padding: 0 10px; }
        .col-md-12 { width: 100%; padding: 0 10px; }
        
        .text-end { text-align: left; }
        .mt-2 { margin-top: 10px; }
        .mt-3 { margin-top: 15px; }
        .mb-3 { margin-bottom: 15px; }
        .input-group {
            display: flex;
            align-items: center;
        }
        .input-group .form-control { flex: 1; }
        
        .empty-state {
            text-align: center;
            padding: 60px 20px;
        }
        .empty-state .icon { font-size: 80px; color: #ddd; }
        .empty-state h4 { color: #666; margin-top: 20px; }
        
        @media (max-width: 768px) {
            .col-md-3, .col-md-4, .col-md-6 { width: 100%; }
            .text-end { text-align: right !important; }
            .action-buttons { width: 100%; }
            .action-buttons .btn-sm { flex: 1; }
            .btn-register, .btn-exam, .btn-evaluate { width: 100%; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="container-fluid">
        
        <!-- هدر صفحه -->
        <div class="page-header">
            <h2>📚 دوره‌های من</h2>
            <p>مشاهده محتوای آموزشی، شرکت در آزمون و ارزیابی دوره‌ها</p>
        </div>
        
        <!-- پیام‌ها -->
        <asp:Panel ID="pnlAlert" runat="server" CssClass="alert" role="alert" style="display:none;">
            <button type="button" class="btn-close" onclick="this.parentElement.style.display='none'">&times;</button>
            <asp:Label ID="lblAlertMessage" runat="server" />
        </asp:Panel>
        
        <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <!-- فیلترها -->
                <div class="filter-section">
                    <div class="row">
                        <div class="col-md-4">
                            <label class="form-label">وضعیت</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="all" Text="همه" />
                                <asp:ListItem Value="pending" Text="در انتظار تایید" />
                                <asp:ListItem Value="confirmed" Text="تایید شده" />
                                <asp:ListItem Value="rejected" Text="رد شده" />
                                <asp:ListItem Value="evaluated" Text="ارزیابی شده" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">جستجو</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="جستجوی دوره..." />
                                <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click">
                                    🔍
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">&nbsp;</label>
                            <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-secondary d-block" OnClick="btnRefresh_Click">
                                🔄 بروزرسانی
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                
                <!-- لیست دوره‌ها -->
                <asp:Repeater ID="rptMyCourses" runat="server" OnItemCommand="rptMyCourses_ItemCommand" OnItemDataBound="rptMyCourses_ItemDataBound">
                    <ItemTemplate>
                        <div class="course-card">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- عنوان و اطلاعات دوره -->
                                    <div class="course-title">
                                        📖 <%# Container.DataItem("CourseTitle") %>
                                    </div>
                                    <div class="course-meta">
                                        📅 تاریخ برگزاری: <%# Container.DataItem("StartDate") %> 
                                        تا <%# Container.DataItem("EndDate") %>
                                    </div>
                                    <div class="course-meta">
                                        <i class="mdi mdi-information"></i> <%# Container.DataItem("Description") %>
                                    </div>
                                    <div class="mt-2">
                                        <!-- وضعیت ثبت‌نام -->
                                        <span id="spanStatus" runat="server" visible='<%# Convert.ToBoolean(Container.DataItem("IsRegistered")) %>'>
                                            <span class="status-badge status-<%# GetStatusClass(Container.DataItem("IsConfirmed")) %>">
                                                <%# GetStatusText(Container.DataItem("IsConfirmed")) %>
                                            </span>
                                        </span>
                                        <span id="spanNotRegistered" runat="server" visible='<%# Not Convert.ToBoolean(Container.DataItem("IsRegistered")) %>'>
                                            <span class="status-badge" style="background:#e9ecef;color:#6c757d;">
                                                📝 ثبت‌نام نشده
                                            </span>
                                        </span>
                                        <span id="Span1" class="status-badge status-evaluated" visible='<%# Convert.ToBoolean(Container.DataItem("IsEvaluated")) %>' runat="server">
                                            ✅ ارزیابی شده
                                        </span>
                                    </div>
                                    
                                    <!-- ============================================================
                                    بخش محتوای آموزشی (فقط برای دوره‌های ثبت‌نام شده)
                                    ============================================================ -->
                                    <asp:Panel ID="pnlContent" runat="server" CssClass="content-section" 
                                        Visible='<%# Convert.ToBoolean(Container.DataItem("IsRegistered")) AndAlso Convert.ToBoolean(Container.DataItem("IsConfirmed")) %>'>
                                        
                                        <div class="content-header">
                                            <div class="content-title">📁 محتوای آموزشی</div>
                                            <div class="content-progress">
                                                <asp:Label ID="lblContentProgress" runat="server" Text="0 از 0" />
                                            </div>
                                        </div>
                                        
                                        <asp:Repeater ID="rptContents" runat="server" OnItemCommand="rptContents_ItemCommand">
                                            <ItemTemplate>
                                                <div class="content-item">
                                                    <div class="content-name">
                                                        <span class="icon"><%# GetContentIcon(Container.DataItem("ContentType")) %></span>
                                                        <span><%# Container.DataItem("Title") %></span>
                                                    </div>
                                                    <div class="content-status">
                                                        <span class='<%# IIf(Convert.ToBoolean(Container.DataItem("IsViewed")), "viewed", "not-viewed") %>'>
                                                            <%# IIf(Convert.ToBoolean(Container.DataItem("IsViewed")), "✅ مشاهده شده", "⏳ مشاهده نشده") %>
                                                        </span>
                                                    </div>
                                                    <div class="action-buttons">
                                                        <asp:LinkButton ID="btnViewContent" runat="server" 
                                                            CssClass="btn-sm btn-sm-view" 
                                                            CommandName="ViewContent" 
                                                            CommandArgument='<%# Container.DataItem("ContentID") & "|" & Container.DataItem("ContentType") & "|" & Container.DataItem("FilePath") & "|" & Container.DataItem("VideoURL") & "|" & Container.DataItem("ExternalLink") %>'>
                                                            👁️ مشاهده
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnDownload" runat="server" 
                                                            CssClass="btn-sm btn-sm-download" 
                                                            CommandName="DownloadContent" 
                                                            CommandArgument='<%# Container.DataItem("ContentID") %>'
                                                            Visible='<%# Container.DataItem("ContentType").ToString() = "File" OrElse Container.DataItem("ContentType").ToString() = "PDF" OrElse Container.DataItem("ContentType").ToString() = "PPT" %>'>
                                                            ⬇️ دانلود
                                                        </asp:LinkButton>
                                                        <span id="Span2" class="btn-sm btn-sm-viewed" 
                                                            visible='<%# Convert.ToBoolean(Container.DataItem("IsViewed")) %>' runat="server">
                                                            ✅ مشاهده شد
                                                        </span>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        <asp:Label ID="lblNoContent" runat="server" Text="📭 هیچ محتوایی برای این دوره ثبت نشده است." 
                                            CssClass="text-muted" Visible="false" />
                                    </asp:Panel>
                                    
                                    <!-- ============================================================
                                    دکمه‌های اقدام
                                    ============================================================ -->
                                    <div class="action-buttons">
                                        <!-- ثبت‌نام در دوره -->
                                        <asp:LinkButton ID="btnRegister" runat="server" 
                                            CssClass="btn-register" 
                                            CommandName="Register" 
                                            CommandArgument='<%# Container.DataItem("CourseID") %>'
                                            Visible='<%# Not Convert.ToBoolean(Container.DataItem("IsRegistered")) AndAlso Convert.ToBoolean(Container.DataItem("IsActive")) %>'
                                            OnClientClick="return confirm('آیا از ثبت‌نام در این دوره اطمینان دارید؟');">
                                            📝 ثبت‌نام در دوره
                                        </asp:LinkButton>
                                        
                                        <!-- شرکت در آزمون / مشاهده نتیجه -->
                                        <asp:LinkButton ID="btnExam" runat="server" 
                                            CssClass='<%# IIf(CheckIfExamTaken(Convert.ToInt32(Container.DataItem("RegistrationID"))), "btn-exam-done", "btn-exam") %>'
                                            CommandName="StartExam" 
                                            CommandArgument='<%# Container.DataItem("RegistrationID") %>'
                                            Visible='<%# Convert.ToBoolean(Container.DataItem("IsRegistered")) AndAlso Convert.ToBoolean(Container.DataItem("IsConfirmed")) %>'>
                                            <%# IIf(CheckIfExamTaken(Convert.ToInt32(Container.DataItem("RegistrationID"))), "📊 مشاهده نتیجه", "📝 شرکت در آزمون") %>
                                        </asp:LinkButton>
                                        
                                        <!-- ارزیابی -->
                                        <asp:LinkButton ID="btnEvaluate" runat="server" 
                                            CssClass="btn-evaluate" 
                                            CommandName="Evaluate" 
                                            CommandArgument='<%# Container.DataItem("RegistrationID") %>'
                                            Visible='<%# Convert.ToBoolean(Container.DataItem("IsRegistered")) AndAlso Convert.ToBoolean(Container.DataItem("IsConfirmed")) AndAlso Not Convert.ToBoolean(Container.DataItem("IsEvaluated")) %>'>
                                            ⭐ ارزیابی دوره
                                        </asp:LinkButton>
                                    </div>
                                    
                                    <!-- پیام وضعیت -->
                                    <div class="mt-2 text-muted" style="font-size: 12px;">
                                        <asp:Label ID="lblStatusMessage" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                
                <!-- خالی بودن لیست -->
                <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
                    <div class="empty-state">
                        <div class="icon">📚</div>
                        <h4>هیچ دوره‌ای یافت نشد</h4>
                        <p class="text-muted">در حال حاضر هیچ دوره فعالی وجود ندارد.</p>
                    </div>
                </asp:Panel>
                
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
    
</asp:Content>