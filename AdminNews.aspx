<%@ Page Title="مدیریت اطلاعیه‌ها و تازه‌ها" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="AdminNews.aspx.vb" Inherits="AdminNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .news-container { 
            max-width: 1100px; 
            margin: 30px auto; 
            padding: 25px; 
            background: white; 
            border-radius: 12px; 
            box-shadow: 0 2px 15px rgba(0,0,0,0.08); 
            direction: rtl; 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
        }
        
        .news-header { 
            background: linear-gradient(135deg, #1e3c72, #2a5298); 
            color: white; 
            padding: 20px; 
            border-radius: 10px; 
            margin-bottom: 25px; 
            text-align: center; 
        }
        .news-header h2 { 
            margin: 0; 
            font-size: 22px; 
        }
        
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
        
        .form-group { 
            margin-bottom: 18px; 
        }
        .form-group label { 
            display: block; 
            font-weight: 600; 
            color: #2c3e50; 
            margin-bottom: 8px; 
            font-size: 14px; 
        }
        .form-control { 
            width: 100%; 
            padding: 10px 15px; 
            border: 1.5px solid #ddd; 
            border-radius: 8px; 
            font-family: inherit; 
            font-size: 14px; 
            box-sizing: border-box; 
            transition: all 0.2s;
        }
        .form-control:focus { 
            border-color: #1e3c72; 
            outline: none; 
            box-shadow: 0 0 0 3px rgba(30,60,114,0.1); 
        }
        
        .btn { 
            padding: 10px 25px; 
            border: none; 
            border-radius: 8px; 
            font-weight: 600; 
            cursor: pointer; 
            font-size: 14px; 
            transition: all 0.2s;
            font-family: inherit;
            text-decoration: none;
            display: inline-block;
        }
        .btn:hover { 
            transform: translateY(-2px); 
            box-shadow: 0 4px 12px rgba(0,0,0,0.15); 
        }
        .btn-primary { background: #1e3c72; color: white; }
        .btn-primary:hover { background: #2a5298; }
        .btn-warning { background: #f39c12; color: white; }
        .btn-warning:hover { background: #e67e22; }
        .btn-danger { background: #e74c3c; color: white; }
        .btn-danger:hover { background: #c0392b; }
        .btn-success { background: #27ae60; color: white; }
        .btn-success:hover { background: #219a52; }
        .btn-secondary { background: #95a5a6; color: white; }
        .btn-secondary:hover { background: #7f8c8d; }
        .btn-info { background: #17a2b8; color: white; }
        .btn-info:hover { background: #138496; }
        .btn-sm { 
            padding: 5px 10px; 
            font-size: 12px; 
            border-radius: 6px;
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
        .table tr:hover { 
            background: #f8f9fa; 
        }
        
        .news-icon-cell {
            font-size: 24px;
            text-align: center;
        }
        
        .news-title-cell {
            font-weight: 600;
            color: #2c3e50;
            text-align: right;
        }
        
        .news-text-cell {
            color: #6c757d;
            text-align: right;
            max-width: 300px;
            word-wrap: break-word;
            font-size: 12px;
            line-height: 1.6;
        }
        
        .badge-active { 
            background: #d4edda; 
            color: #155724; 
            padding: 4px 12px; 
            border-radius: 12px; 
            font-size: 12px; 
            display: inline-block; 
            font-weight: 600;
        }
        .badge-inactive { 
            background: #f8d7da; 
            color: #721c24; 
            padding: 4px 12px; 
            border-radius: 12px; 
            font-size: 12px; 
            display: inline-block; 
            font-weight: 600;
        }
        
        .alert { 
            padding: 12px 18px; 
            border-radius: 8px; 
            margin: 15px 0; 
            display: flex; 
            align-items: center; 
            gap: 10px; 
            font-size: 14px;
        }
        .alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        
        .help-text { 
            color: #6c757d; 
            font-size: 12px; 
            margin-top: 5px; 
            display: block; 
        }
        
        .section-title {
            color: #2c3e50; 
            margin-bottom: 15px; 
            font-size: 16px;
            font-weight: 600;
        }
        
        .empty-data {
            text-align: center;
            padding: 30px;
            color: #6c757d;
        }
        
        .icon-picker {
            display: flex;
            gap: 8px;
            flex-wrap: wrap;
            margin-top: 8px;
        }
        
        .icon-option {
            width: 40px;
            height: 40px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 20px;
            border: 2px solid #ddd;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.2s;
            background: white;
        }
        
        .icon-option:hover {
            border-color: #1e3c72;
            transform: scale(1.1);
        }
        
        .icon-option.selected {
            border-color: #1e3c72;
            background: #e8f4fd;
            box-shadow: 0 0 0 3px rgba(30,60,114,0.15);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="news-container">
        
        <!-- ============================================ -->
        <!-- هدر -->
        <!-- ============================================ -->
        <div class="news-header">
            <h2>📢 مدیریت اطلاعیه‌ها و تازه‌ها</h2>
        </div>

        <!-- ============================================ -->
        <!-- پیام -->
        <!-- ============================================ -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="alert">
                <asp:Label ID="lblMessageIcon" runat="server" />
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>

        <!-- ============================================ -->
        <!-- فرم افزودن/ویرایش -->
        <!-- ============================================ -->
        <div class="form-box">
            <h3>
                <asp:Label ID="lblFormTitle" runat="server" Text="➕ افزودن اطلاعیه جدید" />
            </h3>

            <div class="form-group">
                <label>عنوان اطلاعیه: <span style="color:red;">*</span></label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" 
                    placeholder="مثال: به‌روزرسانی سامانه" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                    ControlToValidate="txtTitle" 
                    ErrorMessage="عنوان اطلاعیه را وارد کنید" 
                    ForeColor="Red" Display="Dynamic" 
                    ValidationGroup="SaveNews"
                    CssClass="help-text" />
            </div>

            <div class="form-group">
                <label>متن اطلاعیه: <span style="color:red;">*</span></label>
                <asp:TextBox ID="txtText" runat="server" CssClass="form-control" 
                    TextMode="MultiLine" Rows="5" 
                    placeholder="متن اطلاعیه را وارد کنید..." />
                <asp:RequiredFieldValidator ID="rfvText" runat="server" 
                    ControlToValidate="txtText" 
                    ErrorMessage="متن اطلاعیه را وارد کنید" 
                    ForeColor="Red" Display="Dynamic" 
                    ValidationGroup="SaveNews"
                    CssClass="help-text" />
            </div>

            <div class="form-group">
                <label>آیکون:</label>
                <asp:TextBox ID="txtIcon" runat="server" CssClass="form-control" 
                    placeholder="📢" MaxLength="10" 
                    style="width:100px;text-align:center;font-size:24px;display:inline-block;" />
                <div class="icon-picker">
                    <div class="icon-option" onclick="setIcon('📢')">📢</div>
                    <div class="icon-option" onclick="setIcon('🆕')">🆕</div>
                    <div class="icon-option" onclick="setIcon('📚')">📚</div>
                    <div class="icon-option" onclick="setIcon('🩺')">🩺</div>
                    <div class="icon-option" onclick="setIcon('⚠️')">⚠️</div>
                    <div class="icon-option" onclick="setIcon('🔔')">🔔</div>
                    <div class="icon-option" onclick="setIcon('ℹ️')">ℹ️</div>
                    <div class="icon-option" onclick="setIcon('✅')">✅</div>
                    <div class="icon-option" onclick="setIcon('📌')">📌</div>
                    <div class="icon-option" onclick="setIcon('🎉')">🎉</div>
                    <div class="icon-option" onclick="setIcon('📊')">📊</div>
                    <div class="icon-option" onclick="setIcon('🛠️')">🛠️</div>
                </div>
                <small class="help-text">آیکون دلخواه یا از لیست انتخاب کنید</small>
            </div>

            <div class="form-group">
                <label>ترتیب نمایش (کمتر = بالاتر):</label>
                <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control" 
                    TextMode="Number" Text="0" 
                    style="width:120px;" />
            </div>

            <div class="form-group">
                <label>
                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                    فعال باشد (نمایش در صفحه لاگین)
                </label>
            </div>

            <asp:HiddenField ID="hfEditID" runat="server" Value="0" />

            <div style="display: flex; gap: 10px; flex-wrap: wrap;">
                <asp:Button ID="btnSave" runat="server" Text="✅ ذخیره" 
                    CssClass="btn btn-success" OnClick="btnSave_Click" 
                    ValidationGroup="SaveNews" />
                <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" 
                    CssClass="btn btn-secondary" OnClick="btnCancel_Click" 
                    CausesValidation="false" />
                <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" 
                    CssClass="btn btn-info" OnClick="btnBack_Click" 
                    CausesValidation="false" />
            </div>
        </div>

        <hr style="margin: 30px 0; border: none; border-top: 2px solid #e9ecef;" />

        <!-- ============================================ -->
        <!-- لیست اطلاعیه‌ها -->
        <!-- ============================================ -->
        <h3 class="section-title">📋 لیست اطلاعیه‌ها</h3>

        <div class="table-responsive">
            <asp:GridView ID="gvNews" runat="server" AutoGenerateColumns="False" 
                CssClass="table"
                OnRowCommand="gvNews_RowCommand" 
                DataKeyNames="ID" 
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                    
                    <asp:TemplateField HeaderText="آیکون" ItemStyle-Width="60">
                        <ItemTemplate>
                            <div class="news-icon-cell"><%# Eval("NewsIcon") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="NewsTitle" HeaderText="عنوان" 
                        ItemStyle-CssClass="news-title-cell" ItemStyle-Width="180" />
                    
                    <asp:TemplateField HeaderText="متن اطلاعیه" ItemStyle-Width="300">
                        <ItemTemplate>
                            <div class="news-text-cell">
                                <%# GetShortText(Eval("NewsText")) %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="SortOrder" HeaderText="ترتیب" ItemStyle-Width="60" />
                    
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                        <ItemTemplate>
                            <span class='<%# IIf(Convert.ToBoolean(Eval("IsActive")), "badge-active", "badge-inactive") %>'>
                                <%# IIf(Convert.ToBoolean(Eval("IsActive")), "✅ فعال", "❌ غیرفعال") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="PersianDate" HeaderText="تاریخ ایجاد" ItemStyle-Width="100" />
                    
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="✏️" 
                                CssClass="btn btn-warning btn-sm" 
                                CommandName="EditNews" CommandArgument='<%# Eval("ID") %>' 
                                ToolTip="ویرایش" 
                                CausesValidation="false"
                                style="margin: 2px;" />
                            <asp:Button ID="btnToggle" runat="server" Text="🔄" 
                                CssClass="btn btn-primary btn-sm" 
                                CommandName="ToggleNews" CommandArgument='<%# Eval("ID") %>' 
                                ToolTip="فعال/غیرفعال" 
                                CausesValidation="false"
                                style="margin: 2px;" />
                            <asp:Button ID="btnDelete" runat="server" Text="🗑️" 
                                CssClass="btn btn-danger btn-sm" 
                                CommandName="DeleteNews" CommandArgument='<%# Eval("ID") %>' 
                                OnClientClick="return confirm('آیا از حذف این اطلاعیه مطمئن هستید؟');" 
                                ToolTip="حذف" 
                                CausesValidation="false"
                                style="margin: 2px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="empty-data">هیچ اطلاعیه‌ای ثبت نشده است</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
    
    <script type="text/javascript">
        function setIcon(icon) {
            var txt = document.getElementById('<%= txtIcon.ClientID %>');
            if (txt) {
                txt.value = icon;
            }

            // حذف کلاس selected از همه
            var options = document.querySelectorAll('.icon-option');
            for (var i = 0; i < options.length; i++) {
                options[i].classList.remove('selected');
            }

            // اضافه کردن کلاس selected به آیکون انتخاب شده
            event.target.classList.add('selected');
        }
    </script>
</asp:Content>