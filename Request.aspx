<%@ Page Title="ثبت درخواست معاینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="Request.aspx.vb" Inherits="Examination_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .exm-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 25px;
            background: white;
            border-radius: 12px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.08);
        }
        .exm-title {
            font-size: 22px;
            font-weight: bold;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #eee;
        }
        .exm-title i { margin-left: 8px; }
        
        .info-card {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 15px 20px;
            margin-bottom: 20px;
        }
        .info-card h4 {
            color: #2c3e50;
            margin: 0 0 12px 0;
            font-size: 15px;
        }
        .info-row {
            display: flex;
            justify-content: space-between;
            padding: 6px 0;
            border-bottom: 1px dashed #e9ecef;
            font-size: 14px;
        }
        .info-row:last-child { border-bottom: none; }
        .info-label { color: #6c757d; font-weight: normal; }
        .info-value { color: #2c3e50; font-weight: 500; }
        
        .form-group {
            margin-bottom: 18px;
        }
        .form-group label {
            display: block;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 6px;
            font-size: 14px;
        }
        .form-group select, .form-group input, .form-group textarea {
            width: 100%;
            padding: 10px 12px;
            border: 1.5px solid #e0e0e0;
            border-radius: 8px;
            font-size: 14px;
            font-family: inherit;
            transition: all 0.2s;
            background: white;
        }
        .form-group select:focus, .form-group input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 3px rgba(52,152,219,0.15);
        }
        .form-group .hint {
            font-size: 12px;
            color: #6c757d;
            margin-top: 4px;
        }
        
        .btn-group {
            display: flex;
            gap: 10px;
            margin-top: 20px;
            flex-wrap: wrap;
        }
        .btn-group .btn {
            padding: 10px 30px;
            border: none;
            border-radius: 25px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            flex: 1;
            min-width: 120px;
        }
        .btn-group .btn:hover { transform: translateY(-2px); box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
        .btn-submit { background: #27ae60; color: white; }
        .btn-submit:hover { background: #219a52; }
        .btn-submit:disabled { opacity: 0.6; cursor: not-allowed; transform: none !important; }
        .btn-back { background: #9b59b6; color: white; }
        .btn-back:hover { background: #8e44ad; }
        
        .alert {
            padding: 12px 18px;
            border-radius: 8px;
            margin: 15px 0;
            font-size: 14px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .alert .icon { font-size: 20px; }
        
        .status-badge {
            display: inline-block;
            padding: 2px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
        }
        .status-pending { background: #fff3cd; color: #856404; }
        .status-approved { background: #d4edda; color: #155724; }
        .status-rejected { background: #f8d7da; color: #721c24; }
        .status-done { background: #cce5ff; color: #004085; }
        
        .request-history {
            margin-top: 25px;
            padding-top: 20px;
            border-top: 2px solid #eee;
        }
        .request-history h4 {
            color: #2c3e50;
            margin-bottom: 12px;
            font-size: 15px;
        }
        .history-item {
            background: #f8f9fa;
            padding: 10px 15px;
            border-radius: 8px;
            margin-bottom: 8px;
            border-right: 3px solid #3498db;
            font-size: 13px;
        }
        .history-item .date { color: #6c757d; font-size: 12px; }
        .history-item .status { font-weight: 600; }
        
        .no-data {
            text-align: center;
            padding: 30px;
            color: #6c757d;
        }
        .no-data .icon { font-size: 40px; margin-bottom: 10px; }
        
        @media (max-width: 600px) {
            .exm-container { padding: 15px; margin: 15px; }
            .btn-group .btn { flex: 1 1 100%; }
            .info-row { flex-direction: column; padding: 4px 0; }
            .info-label { margin-bottom: 2px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
        <div class="exm-title">📋 ثبت درخواست معاینه دوره‌ای</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='alert alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <!-- اطلاعات پرسنل -->
        <div class="info-card">
            <h4>👤 اطلاعات پرسنل</h4>
            <div class="info-row">
                <span class="info-label">کد پرسنلی</span>
                <span class="info-value"><asp:Label ID="lblPersonCode" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">نام و نام خانوادگی</span>
                <span class="info-value"><asp:Label ID="lblFullName" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">واحد سازمانی</span>
                <span class="info-value"><asp:Label ID="lblUnitName" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">سمت</span>
                <span class="info-value"><asp:Label ID="lblPosition" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">سرپرست</span>
                <span class="info-value"><asp:Label ID="lblSupervisor" runat="server" /></span>
            </div>
        </div>
        
        <!-- فرم ثبت درخواست -->
        <div class="form-group">
            <label>📅 تاریخ معاینه <span style="color:red;">*</span></label>
            <asp:DropDownList ID="ddlExaminationDate" runat="server">
                <asp:ListItem Text="-- انتخاب کنید --" Value="" />
            </asp:DropDownList>
            <div class="hint">تاریخ‌هایی که ظرفیت خالی دارند نمایش داده می‌شوند</div>
        </div>
        
        <div class="form-group">
            <label>💬 توضیحات (اختیاری)</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" placeholder="توضیحات اضافی در صورت نیاز..." />
        </div>
        
        <div class="btn-group">
            <asp:Button ID="btnSubmit" runat="server" Text="📤 ثبت درخواست" CssClass="btn btn-submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="btn btn-back" OnClick="btnBack_Click" />
        </div>
        
        <!-- تاریخچه درخواست‌های قبلی -->
        <div class="request-history">
            <h4>📜 تاریخچه درخواست‌های شما</h4>
            <asp:Panel ID="pnlHistory" runat="server">
                <asp:Repeater ID="rptHistory" runat="server">
                    <ItemTemplate>
                        <div class="history-item">
                            <div>
                                <span class="status"><%# Eval("StatusText") %></span>
                                <span class="date"><%# Eval("PersianDate") %></span>
                            </div>
                            <div style="font-size:12px; color:#6c757d;">
                                سرپرست: <%# Eval("SupervisorName") %> | 
                                وضعیت: <span class="status-badge status-<%# Eval("StatusClass") %>"><%# Eval("StatusText") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="pnlNoHistory" runat="server" Visible="false">
                <div class="no-data">
                    <div class="icon">📭</div>
                    <p>هیچ درخواستی ثبت نکرده‌اید</p>
                </div>
            </asp:Panel>
        </div>
    </div>
    
    <asp:HiddenField ID="hdnPersonCode" runat="server" />
    <asp:HiddenField ID="hdnSupervisorCode" runat="server" />
    <asp:HiddenField ID="hdnManagerCode" runat="server" />
    
    <script type="text/javascript">
        function validateForm() {
            var date = document.getElementById('<%= ddlExaminationDate.ClientID %>');
            if (date.value == '') {
                alert('لطفاً تاریخ معاینه را انتخاب کنید');
                date.focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>