<%@ Page Title="مدیریت ثبت‌نام‌ها" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="CourseRegistrations.aspx.vb" Inherits="CourseRegistrations" %>

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
        
        .table-responsive { overflow-x: auto; }
        .table { width: 100%; border-collapse: collapse; font-size: 14px; }
        .table th, .table td { padding: 10px 12px; border: 1px solid #e0e0e0; text-align: center; vertical-align: middle; }
        .table th { background: #1e3c72; color: white; font-weight: 600; }
        .table tr:nth-child(even) { background: #f8f9fa; }
        .table tr:hover { background: #e8f0fe; }
        
        .status-badge {
            padding: 4px 12px;
            border-radius: 20px;
            font-size: 12px;
            display: inline-block;
        }
        .status-pending { background: #fff3cd; color: #856404; }
        .status-confirmed { background: #d4edda; color: #155724; }
        .status-rejected { background: #f8d7da; color: #721c24; }
        
        .btn-confirm {
            background: #28a745;
            border: none;
            color: #fff;
            padding: 5px 15px;
            border-radius: 4px;
            transition: all 0.3s;
            font-size: 12px;
            cursor: pointer;
        }
        .btn-confirm:hover { background: #218838; }
        
        .btn-reject {
            background: #dc3545;
            border: none;
            color: #fff;
            padding: 5px 15px;
            border-radius: 4px;
            transition: all 0.3s;
            font-size: 12px;
            cursor: pointer;
        }
        .btn-reject:hover { background: #c82333; }
        
        .btn-view {
            background: #17a2b8;
            border: none;
            color: #fff;
            padding: 5px 12px;
            border-radius: 4px;
            transition: all 0.3s;
            font-size: 12px;
            cursor: pointer;
        }
        .btn-view:hover { background: #138496; }
        
        .btn-details {
            background: #6c757d;
            border: none;
            color: #fff;
            padding: 5px 12px;
            border-radius: 4px;
            transition: all 0.3s;
            font-size: 12px;
            cursor: pointer;
        }
        .btn-details:hover { background: #5a6268; }
        
        .btn-pending {
            background: #ffc107;
            border: none;
            color: #000;
            padding: 5px 12px;
            border-radius: 4px;
            font-size: 12px;
            cursor: default;
        }
        
        .badge { display: inline-block; padding: 3px 8px; border-radius: 12px; font-size: 11px; }
        .bg-success { background: #d4edda; color: #155724; }
        .bg-warning { background: #fff3cd; color: #856404; }
        .bg-danger { background: #f8d7da; color: #721c24; }
        .bg-info { background: #cce5ff; color: #004085; }
        
        .summary-box {
            background: #fff;
            border-radius: 12px;
            padding: 15px;
            border: 1px solid #e0e0e0;
            text-align: center;
        }
        .summary-box .number { font-size: 24px; font-weight: bold; color: #1e3c72; }
        .summary-box .label { color: #666; font-size: 12px; margin-top: 3px; }
        
        .row { display: flex; flex-wrap: wrap; margin: 0 -10px; }
        .col-md-2 { width: 16.666%; padding: 0 10px; }
        .col-md-3 { width: 25%; padding: 0 10px; }
        .col-md-4 { width: 33.333%; padding: 0 10px; }
        .col-md-6 { width: 50%; padding: 0 10px; }
        .col-md-12 { width: 100%; padding: 0 10px; }
        
        .text-end { text-align: left; }
        .mt-2 { margin-top: 10px; }
        .mt-3 { margin-top: 15px; }
        .mb-3 { margin-bottom: 15px; }
        .mb-4 { margin-bottom: 20px; }
        
        .input-group { display: flex; align-items: center; }
        .input-group .form-control { flex: 1; }
        
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
        .btn-close { float: left; background: none; border: none; font-size: 24px; cursor: pointer; color: #000; opacity: 0.5; padding: 0 10px; }
        .btn-close:hover { opacity: 1; }
        
        .btn-primary-custom {
            background: #1e3c72;
            border: none;
            color: #fff;
            padding: 8px 20px;
            border-radius: 8px;
            transition: all 0.3s;
            cursor: pointer;
        }
        .btn-primary-custom:hover { background: #2a5298; color: #fff; }
        
        .pagination { display: flex; justify-content: center; gap: 5px; margin-top: 15px; }
        .pagination a, .pagination span {
            padding: 8px 14px;
            border: 1px solid #ddd;
            border-radius: 4px;
            text-decoration: none;
            color: #1e3c72;
        }
        .pagination a:hover { background: #f0f0f0; }
        .pagination .active { background: #1e3c72; color: white; border-color: #1e3c72; }
        
        .empty-state { text-align: center; padding: 40px 20px; }
        .empty-state i { font-size: 60px; color: #ddd; }
        .empty-state h4 { color: #666; margin-top: 15px; }
        
        @media (max-width: 768px) {
            .col-md-2, .col-md-3, .col-md-4, .col-md-6 { width: 100%; }
            .text-end { text-align: right !important; }
            .table th, .table td { padding: 5px 8px; font-size: 12px; }
            .btn-confirm, .btn-reject, .btn-view, .btn-details { width: 100%; margin: 2px 0; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container-fluid">
        
        <!-- هدر صفحه -->
        <div class="page-header">
            <h2><i class="mdi mdi-account-multiple"></i> مدیریت ثبت‌نام‌ها</h2>
            <p>تایید یا رد ثبت‌نام پرسنل و مشاهده وضعیت آزمون و ارزیابی</p>
        </div>
        
        <!-- پیام‌ها -->
        <asp:Panel ID="pnlAlert" runat="server" CssClass="alert" role="alert" style="display:none;">
            <button type="button" class="btn-close" onclick="this.parentElement.style.display='none'">&times;</button>
            <asp:Label ID="lblAlertMessage" runat="server" />
        </asp:Panel>
        
        <!-- فیلترها -->
        <div class="filter-section">
            <div class="row">
                <div class="col-md-3">
                    <label class="form-label">انتخاب دوره</label>
                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="-- همه دوره‌ها --" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label class="form-label">وضعیت تایید</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <asp:ListItem Value="all" Text="همه" />
                        <asp:ListItem Value="pending" Text="در انتظار تایید" />
                        <asp:ListItem Value="confirmed" Text="تایید شده" />
                        <asp:ListItem Value="rejected" Text="رد شده" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label class="form-label">وضعیت آزمون</label>
                    <asp:DropDownList ID="ddlExamStatus" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlExamStatus_SelectedIndexChanged">
                        <asp:ListItem Value="all" Text="همه" />
                        <asp:ListItem Value="passed" Text="قبول شده" />
                        <asp:ListItem Value="failed" Text="رد شده" />
                        <asp:ListItem Value="not_taken" Text="شرکت نکرده" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label class="form-label">وضعیت ارزیابی</label>
                    <asp:DropDownList ID="ddlEvalStatus" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEvalStatus_SelectedIndexChanged">
                        <asp:ListItem Value="all" Text="همه" />
                        <asp:ListItem Value="done" Text="انجام شده" />
                        <asp:ListItem Value="pending" Text="انجام نشده" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="کد پرسنلی یا نام..." />
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary-custom" OnClick="btnSearch_Click">
                            <i class="mdi mdi-magnify"></i> جستجو
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-6 text-end">
                    <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-primary-custom" OnClick="btnRefresh_Click">
                        <i class="mdi mdi-refresh"></i> بروزرسانی
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        
        <!-- خلاصه آمار -->
        <div class="row mb-4">
            <div class="col-md-2">
                <div class="summary-box total">
                    <div class="number"><asp:Label ID="lblTotal" runat="server" Text="0" /></div>
                    <div class="label">کل ثبت‌نام‌ها</div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="summary-box pending">
                    <div class="number"><asp:Label ID="lblPending" runat="server" Text="0" /></div>
                    <div class="label">در انتظار تایید</div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="summary-box confirmed">
                    <div class="number"><asp:Label ID="lblConfirmed" runat="server" Text="0" /></div>
                    <div class="label">تایید شده</div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="summary-box rejected">
                    <div class="number"><asp:Label ID="lblRejected" runat="server" Text="0" /></div>
                    <div class="label">رد شده</div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="summary-box" style="border-color:#28a745;">
                    <div class="number" style="color:#28a745;"><asp:Label ID="lblExamPassed" runat="server" Text="0" /></div>
                    <div class="label">قبول شده در آزمون</div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="summary-box" style="border-color:#17a2b8;">
                    <div class="number" style="color:#17a2b8;"><asp:Label ID="lblEvaluated" runat="server" Text="0" /></div>
                    <div class="label">ارزیابی شده</div>
                </div>
            </div>
        </div>
        
        <!-- جدول ثبت‌نام‌ها -->
        <div class="table-responsive">
            <asp:GridView ID="gvRegistrations" runat="server" 
                AutoGenerateColumns="False"
                CssClass="table table-hover"
                GridLines="None"
                AllowPaging="True"
                PageSize="10"
                OnPageIndexChanging="gvRegistrations_PageIndexChanging"
                OnRowCommand="gvRegistrations_RowCommand"
                OnRowDataBound="gvRegistrations_RowDataBound"
                PagerSettings-Mode="NumericFirstLast"
                PagerSettings-PageButtonCount="5"
                PagerStyle-CssClass="pagination"
                PagerStyle-HorizontalAlign="Center">
                
                <Columns>
                    <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="40px">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="پرسنل">
                        <ItemTemplate>
                            <%# GetFullName(Eval("nam"), Eval("family")) %>
                            <br />
                            <small class="text-muted"><%# Eval("codePersoneli") %></small>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="واحد">
                        <ItemTemplate>
                            <%# Eval("UnitsName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="دوره">
                        <ItemTemplate>
                            <%# Eval("CourseTitle") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="تاریخ ثبت‌نام" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("RegistrationDate")).ToString("yyyy/MM/dd HH:mm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="وضعیت تایید" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <span class="status-badge status-<%# GetStatusClass(Eval("IsConfirmed")) %>">
                                <%# GetStatusText(Eval("IsConfirmed")) %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="محتوا" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <span class="badge <%# IIf(Convert.ToBoolean(Eval("AllViewed")), "bg-success", "bg-warning") %>">
                                <%# IIf(Convert.ToBoolean(Eval("AllViewed")), "مشاهده", "مشاهده نشده") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="آزمون" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <%# GetExamStatusText(Eval("ExamPassed"), Eval("ExamTaken")) %>
                            <br />
                            <small><%# IIf(Convert.ToBoolean(Eval("ExamTaken")), "نمره: " & Eval("ExamScore") & "%", "") %></small>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="ارزیابی" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <span class="badge <%# IIf(Convert.ToBoolean(Eval("IsEvaluated")), "bg-success", "bg-warning") %>">
                                <%# IIf(Convert.ToBoolean(Eval("IsEvaluated")), "انجام شده", "انجام نشده") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="180px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnConfirm" runat="server" 
                                CssClass="btn-confirm" 
                                CommandName="Confirm" 
                                CommandArgument='<%# Eval("RegistrationID") %>'
                                Visible='<%# Not Convert.ToBoolean(Eval("IsConfirmed")) %>'
                                OnClientClick="return confirm('آیا از تایید این ثبت‌نام اطمینان دارید؟');">
                                <i class="mdi mdi-check"></i> تایید
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnReject" runat="server" 
                                CssClass="btn-reject" 
                                CommandName="Reject" 
                                CommandArgument='<%# Eval("RegistrationID") %>'
                                Visible='<%# Not Convert.ToBoolean(Eval("IsConfirmed")) %>'
                                OnClientClick="return confirm('آیا از رد این ثبت‌نام اطمینان دارید؟');">
                                <i class="mdi mdi-close"></i> رد
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnView" runat="server" 
                                CssClass="btn-view" 
                                CommandName="ViewUser" 
                                CommandArgument='<%# Eval("UserID") %>'>
                                <i class="mdi mdi-eye"></i> مشاهده
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnDetails" runat="server" 
                                CssClass="btn-details" 
                                CommandName="ViewDetails" 
                                CommandArgument='<%# Eval("RegistrationID") %>'
                                Visible='<%# Convert.ToBoolean(Eval("IsConfirmed")) %>'>
                                <i class="mdi mdi-file-document"></i> جزئیات
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        
        <!-- نمایش پیام خالی بودن -->
        <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
            <div class="empty-state">
                <i class="mdi mdi-account-multiple"></i>
                <h4>هیچ ثبت‌نامی یافت نشد</h4>
                <p class="text-muted">در حال حاضر هیچ درخواست ثبت‌نامی وجود ندارد.</p>
            </div>
        </asp:Panel>
        
    </div>
    
</asp:Content>