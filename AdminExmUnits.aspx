<%@ Page Title="مدیریت واحدهای معاینات" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="AdminExmUnits.aspx.vb" Inherits="AdminExmUnits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .unit-container { 
            max-width: 1200px; 
            margin: 30px auto; 
            padding: 25px; 
            background: white; 
            border-radius: 12px; 
            box-shadow: 0 2px 15px rgba(0,0,0,0.08); 
            direction: rtl; 
        }
        
        .unit-header { 
            background: linear-gradient(135deg, #1e3c72, #2a5298); 
            color: white; 
            padding: 20px; 
            border-radius: 10px; 
            margin-bottom: 25px; 
            text-align: center; 
        }
        .unit-header h2 { margin: 0; font-size: 22px; }
        
        .form-box {
            background: #f8f9fa; 
            padding: 20px; 
            border-radius: 10px; 
            margin-bottom: 25px;
            border: 1px solid #e9ecef;
        }
        
        .form-box h3 {
            color: #2c3e50; 
            margin-bottom: 15px; 
            font-size: 16px;
            border-bottom: 2px solid #1e3c72;
            padding-bottom: 10px;
        }
        
        .section-title {
            color: #2c3e50; 
            margin-bottom: 15px; 
            font-size: 16px;
            font-weight: 600;
        }
        
        .table-responsive {
            overflow-x: auto;
            margin-top: 20px;
        }
        .table { 
            width: 100%; 
            border-collapse: collapse; 
            font-size: 13px; 
            text-align: right;
            direction: rtl;
        }
        .table th { 
            background: #2c3e50; 
            color: white; 
            padding: 12px; 
            text-align: center; 
            font-weight: 600; 
            white-space: nowrap;
        }
        .table td { 
            padding: 12px; 
            border-bottom: 1px solid #eee; 
            text-align: center; 
            vertical-align: middle; 
        }
        .table tr:hover { background: #f8f9fa; }
        
        .badge-active { 
            background: #d4edda; color: #155724; 
            padding: 4px 12px; border-radius: 12px; 
            font-size: 12px; display: inline-block; font-weight: 600;
        }
        .badge-inactive { 
            background: #f8d7da; color: #721c24; 
            padding: 4px 12px; border-radius: 12px; 
            font-size: 12px; display: inline-block; font-weight: 600;
        }
        
        .deadline-badge {
            background: #fff3cd; color: #856404;
            padding: 4px 10px; border-radius: 12px;
            font-size: 12px; display: inline-block; font-weight: 600;
        }
        .deadline-empty { color: #6c757d; font-size: 12px; }
        .deadline-expired {
            background: #f8d7da; color: #721c24;
            padding: 4px 10px; border-radius: 12px;
            font-size: 12px; display: inline-block; font-weight: 600;
        }
        
        /* ===== بج نوع تاییدکننده ===== */
        .badge-approve-supervisor {
            background: #d1ecf1; color: #0c5460;
            padding: 4px 12px; border-radius: 12px;
            font-size: 12px; display: inline-block; font-weight: 600;
        }
        .badge-approve-manager {
            background: #fce4ec; color: #880e4f;
            padding: 4px 12px; border-radius: 12px;
            font-size: 12px; display: inline-block; font-weight: 600;
        }
        
        .alert { 
            padding: 12px 18px; border-radius: 8px; 
            margin: 15px 0; display: flex; 
            align-items: center; gap: 10px; font-size: 14px;
        }
        .alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        
        .help-text { 
            color: #6c757d; font-size: 12px; 
            margin-top: 5px; display: block; 
        }
        
        .empty-data {
            text-align: center; padding: 30px; color: #6c757d;
        }
        
        /* ===== پنل رییس واحد ===== */
        .manager-panel {
            background: #fce4ec;
            border: 2px dashed #e91e63;
            border-radius: 10px;
            padding: 15px;
            margin-top: 15px;
        }
        .manager-panel .panel-title {
            color: #880e4f;
            font-weight: 700;
            margin-bottom: 10px;
            font-size: 14px;
        }
        
        /* ===== رادیو باتن زیبا ===== */
        .radio-group {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
        }
        .radio-item {
            display: flex;
            align-items: center;
            gap: 6px;
            padding: 8px 16px;
            border: 2px solid #e0e0e0;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.2s;
            background: white;
        }
        .radio-item:hover {
            border-color: #3498db;
            background: #f0f8ff;
        }
        .radio-item input[type="radio"] {
            margin: 0;
            cursor: pointer;
        }
        .radio-item.selected-supervisor {
            border-color: #17a2b8;
            background: #d1ecf1;
        }
        .radio-item.selected-manager {
            border-color: #e91e63;
            background: #fce4ec;
        }
    </style>
    
    <script type="text/javascript">
        function toggleManagerPanel() {
            var rdoSupervisor = document.getElementById('<%= rdoApproveBySupervisor.ClientID %>');
            var rdoManager = document.getElementById('<%= rdoApproveByManager.ClientID %>');
            var pnlManager = document.getElementById('<%= pnlManagerInfo.ClientID %>');
            var lblManagerCode = document.getElementById('<%= lblManagerCodeRequired.ClientID %>');
            
            if (rdoManager && rdoManager.checked) {
                if (pnlManager) pnlManager.style.display = 'block';
                if (lblManagerCode) lblManagerCode.style.display = 'inline';
            } else {
                if (pnlManager) pnlManager.style.display = 'none';
                if (lblManagerCode) lblManagerCode.style.display = 'none';
            }
        }
        
        window.onload = function () {
            toggleManagerPanel();
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="unit-container">
        
        <!-- هدر -->
        <div class="unit-header">
            <h2>🏢 مدیریت واحدهای معاینات</h2>
        </div>

        <!-- پیام -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="alert">
                <asp:Label ID="lblMessageIcon" runat="server" />
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>

        <!-- ============================================ -->
        <!-- فرم افزودن/ویرایش واحد -->
        <!-- ============================================ -->
        <div class="form-box">
            <h3>
                <asp:Label ID="lblFormTitle" runat="server" Text="➕ افزودن واحد جدید" />
            </h3>

            <!-- ردیف اول: نام، کد، مهلت -->
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblUnitName" runat="server" CssClass="control-label" Text="نام واحد: *" />
                        <asp:TextBox ID="txtUnitName" runat="server" CssClass="form-control" 
                            placeholder="مثال: فناوری اطلاعات و ارتباطات" />
                        <asp:RequiredFieldValidator ID="rfvUnitName" runat="server" 
                            ControlToValidate="txtUnitName" 
                            ErrorMessage="نام واحد را وارد کنید" 
                            ForeColor="Red" Display="Dynamic" 
                            ValidationGroup="SaveUnit"
                            CssClass="help-text" />
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblUnitCode" runat="server" CssClass="control-label" Text="کد واحد:" />
                        <asp:TextBox ID="txtUnitCode" runat="server" CssClass="form-control" 
                            placeholder="مثال: 001" />
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblDeadline" runat="server" CssClass="control-label" Text="مهلت رزرو (شمسی):" />
                        <asp:TextBox ID="txtDeadline" runat="server" CssClass="form-control" 
                            placeholder="1405/06/30" MaxLength="10" />
                        <small class="help-text">تاریخ مهلت رزرو برای این واحد</small>
                    </div>
                </div>
            </div>

            <!-- ============================================ -->
            <!-- ردیف جدید: انتخاب نوع تاییدکننده -->
            <!-- ============================================ -->
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12">
                    <div class="form-group">
                        <label style="font-weight:bold; color:#2c3e50; margin-bottom:10px; display:block;">
                            🎯 نوع تاییدکننده درخواست‌ها: *
                        </label>
                        <div class="radio-group">
                            <label class="radio-item" id="lblRdoSupervisor">
                                <asp:RadioButton ID="rdoApproveBySupervisor" runat="server" 
                                    GroupName="ApproveBy" Checked="true"
                                    onclick="toggleManagerPanel();" />
                                <span>👤 تایید توسط <strong>سرپرست</strong></span>
                            </label>
                            
                            <label class="radio-item" id="lblRdoManager">
                                <asp:RadioButton ID="rdoApproveByManager" runat="server" 
                                    GroupName="ApproveBy"
                                    onclick="toggleManagerPanel();" />
                                <span>👔 تایید توسط <strong>رییس واحد</strong></span>
                            </label>
                        </div>
                        <small class="help-text">
                            در صورت انتخاب «رییس واحد»، درخواست‌های پرسنل این واحد به‌جای سرپرست، به رییس واحد ارسال می‌شود.
                        </small>
                    </div>
                </div>
            </div>

            <!-- ============================================ -->
            <!-- پنل رییس واحد (فقط وقتی رییس انتخاب شود) -->
            <!-- ============================================ -->
            <asp:Panel ID="pnlManagerInfo" runat="server" CssClass="manager-panel" style="display:none;">
                <div class="panel-title">👔 اطلاعات رییس واحد</div>
                
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblManagerCode" runat="server" 
                                CssClass="control-label" 
                                Text="کد پرسنلی رییس:" />
                            <asp:Label ID="lblManagerCodeRequired" runat="server" 
                                Text=" *" ForeColor="Red" style="display:none;" />
                            <asp:TextBox ID="txtManagerCode" runat="server" 
                                CssClass="form-control" 
                                placeholder="مثال: 12345" 
                                MaxLength="50" />
                            <small class="help-text">کد پرسنلی رییس واحد را وارد کنید</small>
                        </div>
                    </div>
                    
                    <div class="col-md-8">
                        <div class="form-group">
                            <asp:Label ID="lblManagerName" runat="server" 
                                CssClass="control-label" 
                                Text="نام رییس:" />
                            <asp:TextBox ID="txtManagerName" runat="server" 
                                CssClass="form-control" 
                                placeholder="مثال: احمد رضایی" 
                                MaxLength="150" />
                            <small class="help-text">نام و نام خانوادگی رییس واحد</small>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- ردیف: فعال بودن -->
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                            فعال باشد
                        </label>
                    </div>
                </div>
            </div>

            <!-- ردیف: دکمه‌ها -->
            <div class="row">
                <div class="col-md-12" style="text-align: center; margin-top: 15px;">
                    <asp:HiddenField ID="hfEditID" runat="server" Value="0" />
                    
                    <asp:Button ID="btnSave" runat="server" Text="✅ ذخیره" 
                        CssClass="btn btn-success" OnClick="btnSave_Click" 
                        ValidationGroup="SaveUnit"
                        style="margin: 5px; padding: 8px 30px;" />
                    
                    <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" 
                        CssClass="btn btn-secondary" OnClick="btnCancel_Click" 
                        CausesValidation="false"
                        style="margin: 5px; padding: 8px 30px;" />
                    
                    <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" 
                        CssClass="btn btn-info" OnClick="btnBack_Click" 
                        CausesValidation="false"
                        style="margin: 5px; padding: 8px 30px;" />
                </div>
            </div>
        </div>

        <!-- ============================================ -->
        <!-- لیست واحدها -->
        <!-- ============================================ -->
        <h3 class="section-title">📋 لیست واحدها</h3>

        <div class="table-responsive">
            <asp:GridView ID="gvUnits" runat="server" AutoGenerateColumns="False" 
                CssClass="table"
                OnRowCommand="gvUnits_RowCommand" 
                DataKeyNames="ID" 
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                    <asp:BoundField DataField="UnitName" HeaderText="نام واحد" ItemStyle-Width="200" />
                    <asp:BoundField DataField="UnitCode" HeaderText="کد" ItemStyle-Width="80" />
                    
                    <asp:TemplateField HeaderText="مهلت رزرو" ItemStyle-Width="130">
                        <ItemTemplate>
                            <%# GetDeadlineHtml(Eval("DeadlineDate"), Eval("PersianDeadline")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="نوع تایید" ItemStyle-Width="180">
                        <ItemTemplate>
                            <%# GetApproveByHtml(Eval("ApproveBy"), Eval("ManagerName")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="90">
                        <ItemTemplate>
                            <span class='<%# IIf(Convert.ToBoolean(Eval("IsActive")), "badge-active", "badge-inactive") %>'>
                                <%# IIf(Convert.ToBoolean(Eval("IsActive")), "✅ فعال", "❌ غیرفعال") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="180">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="✏️" 
                                CssClass="btn btn-warning btn-sm" 
                                CommandName="EditUnit" CommandArgument='<%# Eval("ID") %>' 
                                ToolTip="ویرایش" 
                                CausesValidation="false"
                                style="margin: 2px;" />
                            <asp:Button ID="btnToggle" runat="server" Text="🔄" 
                                CssClass="btn btn-primary btn-sm" 
                                CommandName="ToggleUnit" CommandArgument='<%# Eval("ID") %>' 
                                ToolTip="فعال/غیرفعال" 
                                CausesValidation="false"
                                style="margin: 2px;" />
                            <asp:Button ID="btnDelete" runat="server" Text="🗑️" 
                                CssClass="btn btn-danger btn-sm" 
                                CommandName="DeleteUnit" CommandArgument='<%# Eval("ID") %>' 
                                OnClientClick="return confirm('آیا از حذف این واحد مطمئن هستید؟');" 
                                ToolTip="حذف" 
                                CausesValidation="false"
                                style="margin: 2px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="empty-data">هیچ واحدی ثبت نشده است</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>