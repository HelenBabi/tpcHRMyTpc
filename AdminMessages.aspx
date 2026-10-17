<%@ Page Title="مدیریت پیغام‌های سیستم" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="AdminMessages.aspx.vb" Inherits="AdminMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .msg-container { max-width: 900px; margin: 30px auto; padding: 25px; background: white; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; }
        .msg-header { background: linear-gradient(135deg, #1e3c72, #2a5298); color: white; padding: 20px; border-radius: 10px; margin-bottom: 25px; text-align: center; }
        .msg-header h2 { margin: 0; font-size: 22px; }
        .form-group { margin-bottom: 20px; }
        .form-group label { display: block; font-weight: 600; color: #2c3e50; margin-bottom: 8px; }
        .form-control { width: 100%; padding: 10px 15px; border: 1.5px solid #ddd; border-radius: 8px; font-family: inherit; font-size: 14px; }
        .form-control:focus { border-color: #1e3c72; outline: none; box-shadow: 0 0 0 3px rgba(30,60,114,0.1); }
        .btn { padding: 10px 25px; border: none; border-radius: 8px; font-weight: 600; cursor: pointer; font-size: 14px; }
        .btn-primary { background: #1e3c72; color: white; }
        .btn-primary:hover { background: #2a5298; }
        .btn-warning { background: #f39c12; color: white; }
        .btn-warning:hover { background: #e08e0b; }
        .btn-danger { background: #e74c3c; color: white; }
        .btn-danger:hover { background: #c0392b; }
        .btn-secondary { background: #95a5a6; color: white; }
        .table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        .table th { background: #2c3e50; color: white; padding: 12px; text-align: right; }
        .table td { padding: 12px; border-bottom: 1px solid #eee; text-align: right; }
        .table tr:hover { background: #f8f9fa; }
        .badge-active { background: #d4edda; color: #155724; padding: 4px 12px; border-radius: 12px; font-size: 12px; }
        .badge-inactive { background: #f8d7da; color: #721c24; padding: 4px 12px; border-radius: 12px; font-size: 12px; }
        .alert { padding: 12px 18px; border-radius: 8px; margin: 15px 0; }
        .alert-success { background: #d4edda; color: #155724; }
        .alert-danger { background: #f8d7da; color: #721c24; }
        .help-text { color: #6c757d; font-size: 12px; margin-top: 5px; display: block; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="msg-container">
        <div class="msg-header">
            <h2>📢 مدیریت پیغام‌های سیستم</h2>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="alert">
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>

        <!-- فرم افزودن/ویرایش پیغام -->
        <div class="form-group">
            <label>عنوان پیغام:</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="مثال: اطلاعیه مهم" />
        </div>

        <div class="form-group">
            <label>متن پیغام:</label>
            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10" 
                placeholder="متن پیغام خود را وارد کنید... (برای خط جدید Enter بزنید)" />
            <small class="help-text">💡 برای شکستن خط، از کلید Enter استفاده کنید</small>
        </div>

        <div class="form-group">
            <label>
                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                فعال باشد (نمایش به کاربران)
            </label>
        </div>

        <asp:HiddenField ID="hfEditID" runat="server" Value="0" />

        <div style="text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text="✅ ذخیره پیغام" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
        </div>

        <hr style="margin: 30px 0;" />

        <!-- لیست پیغام‌ها -->
        <h3>📋 لیست پیغام‌ها</h3>
        <asp:GridView ID="gvMessages" runat="server" AutoGenerateColumns="False" CssClass="table" 
            OnRowCommand="gvMessages_RowCommand" DataKeyNames="ID" EmptyDataText="هیچ پیغامی ثبت نشده است">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                <asp:BoundField DataField="MessageTitle" HeaderText="عنوان" />
                <asp:BoundField DataField="MessageText" HeaderText="متن پیغام" />
                <asp:TemplateField HeaderText="وضعیت">
                    <ItemTemplate>
                        <span class='<%# IIf(Convert.ToBoolean(Eval("IsActive")), "badge-active", "badge-inactive") %>'>
                            <%# IIf(Convert.ToBoolean(Eval("IsActive")), "✅ فعال", "❌ غیرفعال") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="تاریخ ایجاد" DataFormatString="{0:yyyy/MM/dd HH:mm}" />
                <asp:TemplateField HeaderText="عملیات">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="✏️" CssClass="btn btn-warning" 
                            CommandName="EditMsg" CommandArgument='<%# Eval("ID") %>' ToolTip="ویرایش" />
                        <asp:Button ID="btnToggle" runat="server" Text="🔄" CssClass="btn btn-primary" 
                            CommandName="ToggleMsg" CommandArgument='<%# Eval("ID") %>' ToolTip="فعال/غیرفعال" />
                        <asp:Button ID="btnDelete" runat="server" Text="🗑️" CssClass="btn btn-danger" 
                            CommandName="DeleteMsg" CommandArgument='<%# Eval("ID") %>' 
                            OnClientClick="return confirm('آیا از حذف مطمئن هستید؟');" ToolTip="حذف" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>