<%@ Page Title="جزئیات واحد" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ExmUnitDetails.aspx.vb" Inherits="ExmUnitDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .unit-container {
            max-width: 1200px;
            margin: 30px auto;
            padding: 25px 30px;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.08);
            direction: rtl;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .unit-title {
            font-size: 22px;
            font-weight: 700;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e9ecef;
        }
        .unit-info-card {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 15px 20px;
            margin-bottom: 20px;
            border: 1px solid #e9ecef;
        }
        .unit-info-card h4 {
            color: #2c3e50;
            margin: 0 0 12px 0;
            font-size: 15px;
            font-weight: 600;
            border-bottom: 2px solid #3498db;
            padding-bottom: 8px;
        }
        .unit-info-row {
            display: flex;
            justify-content: space-between;
            padding: 6px 0;
            border-bottom: 1px dashed #e9ecef;
            font-size: 14px;
        }
        .unit-info-row:last-child { border-bottom: none; }
        .unit-info-label { color: #6c757d; font-weight: 400; }
        .unit-info-value { color: #2c3e50; font-weight: 500; }
        
        /* ============================================ */
        /* جدول */
        /* ============================================ */
        .unit-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            text-align: right;
            margin-top: 10px;
        }
        .unit-table th {
            background: #2c3e50;
            color: #ffffff;
            padding: 10px 12px;
            font-weight: 600;
            text-align: center;
            white-space: nowrap;
        }
        .unit-table td {
            padding: 10px 12px;
            border-bottom: 1px solid #e9ecef;
            vertical-align: middle;
            text-align: center;
        }
        .unit-table tr:hover { background: #f8f9fa; }
        
        .national-code {
            font-family: 'Courier New', monospace;
            font-weight: 600;
            letter-spacing: 1px;
            color: #2c3e50;
            direction: ltr;
            text-align: center;
        }
        
        .badge-active {
            background: #d4edda;
            color: #155724;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        .badge-inactive {
            background: #f8d7da;
            color: #721c24;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        .badge-pending {
            background: #fff3cd;
            color: #856404;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        .badge-approved {
            background: #d4edda;
            color: #155724;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        .badge-rejected {
            background: #f8d7da;
            color: #721c24;
            padding: 3px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }
        
        /* ============================================ */
        /* دکمه‌ها */
        /* ============================================ */
        .btn-back {
            background: #9b59b6;
            color: #ffffff;
            padding: 10px 30px;
            border: none;
            border-radius: 25px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            margin-top: 20px;
            font-family: inherit;
        }
        .btn-back:hover { background: #8e44ad; transform: translateY(-2px); }
        
        .btn-excel {
            background: #27ae60;
            color: #ffffff;
            padding: 8px 20px;
            border: none;
            border-radius: 8px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            font-family: inherit;
        }
        .btn-excel:hover { background: #219a52; transform: translateY(-2px); }
        
        /* ============================================ */
        /* هشدار */
        /* ============================================ */
        .alert {
            padding: 12px 18px;
            border-radius: 8px;
            margin: 15px 0;
            font-size: 14px;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        
        .no-data {
            text-align: center;
            padding: 30px;
            color: #6c757d;
        }
        .no-data .icon { font-size: 40px; margin-bottom: 10px; display: block; }
        
        .table-header-bar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
            flex-wrap: wrap;
            gap: 10px;
        }
        .table-header-bar h4 {
            margin: 0;
            color: #2c3e50;
            font-size: 15px;
            font-weight: 600;
        }
        
        /* ============================================ */
        /* آمار تکمیل رزرو */
        /* ============================================ */
        .exm-status-stats {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(140px, 1fr));
            gap: 12px;
            margin-bottom: 15px;
        }
        .exm-stat-item {
            background: #ffffff;
            border-radius: 10px;
            padding: 12px 15px;
            text-align: center;
            border-right: 4px solid #3498db;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        .exm-stat-item .stat-number {
            font-size: 22px;
            font-weight: 800;
            color: #2c3e50;
        }
        .exm-stat-item .stat-label {
            font-size: 12px;
            color: #6c757d;
            margin-top: 3px;
        }
        .exm-stat-item.reserved { border-right-color: #27ae60; }
        .exm-stat-item.not-reserved { border-right-color: #e74c3c; }
        .exm-stat-item.total { border-right-color: #3498db; }

        .exm-progress-bar {
            width: 100%;
            height: 14px;
            background: #e9ecef;
            border-radius: 7px;
            overflow: hidden;
            margin-bottom: 15px;
            direction: ltr;
        }
        .exm-progress-bar-fill {
            height: 100%;
            background: linear-gradient(90deg, #27ae60, #2ecc71);
            border-radius: 7px;
            transition: width 0.6s ease;
            width: 0%;
        }

        .exm-not-reserved-list {
            background: #fff5f5;
            border: 1px dashed #e74c3c;
            border-radius: 10px;
            padding: 12px 15px;
            margin-top: 10px;
            max-height: 250px;
            overflow-y: auto;
        }
        .exm-not-reserved-list .list-title {
            font-size: 13px;
            font-weight: 700;
            color: #721c24;
            margin-bottom: 8px;
        }
        .exm-not-reserved-list .person-chip {
            display: inline-block;
            background: #ffffff;
            border: 1px solid #f5c6cb;
            color: #721c24;
            padding: 4px 10px;
            border-radius: 12px;
            font-size: 12px;
            margin: 3px;
            font-weight: 600;
        }
        .exm-not-reserved-list .person-chip .code {
            color: #adb5bd;
            font-size: 11px;
            margin-right: 4px;
        }
        .exm-not-reserved-empty {
            background: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
            border-radius: 8px;
            padding: 10px 15px;
            font-size: 13px;
            text-align: center;
            font-weight: 600;
            display: block;
            margin-top: 10px;
        }
        
        @media (max-width: 768px) {
            .unit-container { padding: 15px; margin: 10px; }
            .unit-table { font-size: 11px; }
            .unit-table th, .unit-table td { padding: 5px 8px; }
            .exm-status-stats { grid-template-columns: 1fr; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="unit-container">
        <div class="unit-title">📋 جزئیات واحد</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="alert">
                <asp:Label ID="lblMessageIcon" runat="server" />
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <asp:Panel ID="pnlUnitInfo" runat="server" Visible="false">
            
            <!-- ============================================ -->
            <!-- اطلاعات واحد -->
            <!-- ============================================ -->
            <div class="unit-info-card">
                <h4>🏢 اطلاعات واحد</h4>
                <div class="unit-info-row">
                    <span class="unit-info-label">نام واحد</span>
                    <span class="unit-info-value"><asp:Label ID="lblUnitName" runat="server" /></span>
                </div>
                <div class="unit-info-row">
                    <span class="unit-info-label">وضعیت</span>
                    <span class="unit-info-value"><asp:Label ID="lblStatus" runat="server" /></span>
                </div>
                <div class="unit-info-row">
                    <span class="unit-info-label">تعداد کل رزروها</span>
                    <span class="unit-info-value"><asp:Label ID="lblTotalReservations" runat="server" Text="0" /></span>
                </div>
            </div>
            
            <!-- ============================================ -->
            <!-- آمار تکمیل رزرو واحد -->
            <!-- ============================================ -->
            <div class="unit-info-card">
                <h4>📊 آمار تکمیل رزرو پرسنل واحد</h4>
                
                <div class="exm-status-stats">
                    <div class="exm-stat-item total">
                        <div class="stat-number"><asp:Label ID="lblTotalPersonnel" runat="server" Text="0" /></div>
                        <div class="stat-label">👥 کل پرسنل واحد</div>
                    </div>
                    <div class="exm-stat-item reserved">
                        <div class="stat-number"><asp:Label ID="lblReservedCount" runat="server" Text="0" /></div>
                        <div class="stat-label">✅ رزرو کرده</div>
                    </div>
                    <div class="exm-stat-item not-reserved">
                        <div class="stat-number"><asp:Label ID="lblNotReservedCount" runat="server" Text="0" /></div>
                        <div class="stat-label">⏳ رزرو نکرده</div>
                    </div>
                    <div class="exm-stat-item">
                        <div class="stat-number"><asp:Label ID="lblCompletionPercent" runat="server" Text="0%" /></div>
                        <div class="stat-label">📈 درصد تکمیل</div>
                    </div>
                </div>
                
                <!-- نوار پیشرفت -->
                <div class="exm-progress-bar">
                    <div id="progressBarFill" runat="server" class="exm-progress-bar-fill"></div>
                </div>
                
                <!-- لیست افرادی که رزرو نکردن -->
                <asp:Panel ID="pnlNotReservedList" runat="server" Visible="false" CssClass="exm-not-reserved-list">
                    <div class="list-title">⚠️ پرسنل زیر هنوز رزرو نکرده‌اند:</div>
                    <asp:Repeater ID="rptNotReserved" runat="server">
                        <ItemTemplate>
                            <span class="person-chip">
                                <span class="code"><%# Eval("codePersoneli") %></span>
                                <%# Eval("FullName") %>
                            </span>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                
                <asp:Label ID="lblNotReservedEmpty" runat="server" Visible="false" 
                    CssClass="exm-not-reserved-empty" 
                    Text="🎉 همه پرسنل این واحد رزرو کرده‌اند!" />
            </div>
            
            <!-- ============================================ -->
            <!-- تاریخ‌های معاینه و ظرفیت -->
            <!-- ============================================ -->
            <div class="unit-info-card">
                <h4>📅 تاریخ‌های معاینه و ظرفیت</h4>
                <asp:GridView ID="gvCapacities" runat="server" AutoGenerateColumns="False" 
                    CssClass="unit-table" EmptyDataText="هیچ ظرفیتی برای این واحد ثبت نشده است">
                    <Columns>
                        <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" />
                        <asp:BoundField DataField="DayName" HeaderText="روز هفته" />
                        <asp:BoundField DataField="Capacity" HeaderText="ظرفیت" />
                        <asp:BoundField DataField="ReservedCount" HeaderText="رزرو شده" />
                        <asp:BoundField DataField="AvailableCapacity" HeaderText="ظرفیت باقیمانده" />
                        <asp:TemplateField HeaderText="وضعیت">
                            <ItemTemplate>
                                <span class='<%# GetCapacityStatusClass(Eval("AvailableCapacity"), Eval("Capacity")) %>'>
                                    <%# GetCapacityStatusText(Eval("AvailableCapacity"), Eval("Capacity")) %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            
            <!-- ============================================ -->
            <!-- لیست پرسنل رزرو کننده -->
            <!-- ============================================ -->
            <div class="unit-info-card">
                <div class="table-header-bar">
                    <h4>👥 پرسنل رزرو کننده</h4>
                    <asp:Button ID="btnExportPersonnel" runat="server" Text="📊 خروجی اکسل" 
                        CssClass="btn-excel" OnClick="btnExportPersonnel_Click" />
                </div>
                
                <asp:Panel ID="pnlPersonnel" runat="server" Visible="false">
                    <asp:GridView ID="gvPersonnel" runat="server" AutoGenerateColumns="False" 
                        CssClass="unit-table" AllowPaging="True" PageSize="15"
                        OnPageIndexChanging="gvPersonnel_PageIndexChanging"
                        EmptyDataText="هیچ پرسنلی از این واحد رزرو نکرده است">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                            <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" ItemStyle-Width="80" />
                            <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" ItemStyle-Width="150" />
                            <asp:BoundField DataField="NationalCode" HeaderText="کد ملی" 
                                ItemStyle-CssClass="national-code" ItemStyle-Width="110" />
                            <asp:BoundField DataField="Mobile" HeaderText="موبایل" ItemStyle-Width="100" />
                            <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" ItemStyle-Width="90" />
                            
                            <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="130">
                                <ItemTemplate>
                                    <span class='<%# GetStatusClass(Eval("StatusCode")) %>'>
                                        <%# Eval("StatusText") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="RequestDate" HeaderText="تاریخ درخواست" ItemStyle-Width="100" />
                        </Columns>
                        <PagerStyle CssClass="rpt-pagination" HorizontalAlign="Center" />
                    </asp:GridView>
                </asp:Panel>
                
                <asp:Panel ID="pnlNoPersonnel" runat="server" Visible="true">
                    <div class="no-data">
                        <span class="icon">📭</span>
                        <p>هیچ پرسنلی از این واحد رزرو نکرده است</p>
                    </div>
                </asp:Panel>
            </div>
            
            <!-- ============================================ -->
            <!-- دکمه بازگشت -->
            <!-- ============================================ -->
            <div style="text-align: center;">
                <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" 
                    CssClass="btn-back" OnClick="btnBack_Click" />
            </div>
        </asp:Panel>
    </div>
    
    <!-- ============================================ -->
    <!-- HiddenField برای ذخیره صفحه مبدا -->
    <!-- ============================================ -->
    <asp:HiddenField ID="hdnReturnUrl" runat="server" Value="" EnableViewState="true" />
</asp:Content>