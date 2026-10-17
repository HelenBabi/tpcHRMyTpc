<%@ Page Title="مدیریت دسترسی کاربران به فرم‌ها" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="FormAccess.aspx.vb" Inherits="FormAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-title {
            color: #2c3e50;
            border-bottom: 3px solid #3498db;
            padding-bottom: 10px;
            margin-bottom: 20px;
            font-weight: bold;
        }

        .card-personel {
            border: none;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            overflow: hidden;
        }

        .card-header-gradient {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 12px 18px;
            font-weight: bold;
            font-size: 15px;
        }

        .card-header-success {
            background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
            color: white;
            padding: 12px 18px;
            font-weight: bold;
            font-size: 15px;
        }

        .search-box {
            position: relative;
            margin-bottom: 15px;
        }

        .search-box input {
            border-radius: 25px;
            padding: 10px 45px 10px 20px;
            border: 2px solid #e0e0e0;
            transition: all 0.3s;
            font-size: 14px;
        }

        .search-box input:focus {
            border-color: #667eea;
            box-shadow: 0 0 8px rgba(102, 126, 234, 0.3);
            outline: none;
        }

        .search-box .search-icon {
            position: absolute;
            left: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #999;
            font-size: 18px;
        }

        .btn-clear {
            background-color: #6c757d;
            color: white;
            border-radius: 25px;
            padding: 8px 18px;
            border: none;
            font-size: 13px;
        }

        .btn-clear:hover { background-color: #5a6268; color: white; }

        .grid-personel {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            font-size: 13px;
            border-radius: 8px;
            overflow: hidden;
        }

        .grid-personel th {
            background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
            color: white;
            padding: 12px 8px;
            text-align: center;
            font-weight: 600;
            border: none;
        }

        .grid-personel td {
            padding: 10px 8px;
            border-bottom: 1px solid #f0f0f0;
            text-align: center;
            vertical-align: middle;
        }

        .grid-personel tr {
            background-color: white;
            transition: background-color 0.2s;
        }

        .grid-personel tr:hover {
            background-color: #f8f9ff;
        }

        .grid-personel tr.selected-row {
            background-color: #fff8e1 !important;
            border-right: 4px solid #ffc107;
        }

        .personel-name {
            text-align: right !important;
            font-weight: 500;
            color: #2c3e50;
        }

        .personel-code {
            color: #6c757d;
            font-size: 12px;
        }

        .badge-access {
            display: inline-block;
            min-width: 30px;
            padding: 4px 10px;
            border-radius: 12px;
            font-weight: bold;
            font-size: 12px;
            color: white;
        }

        .badge-access-zero { background-color: #95a5a6; }
        .badge-access-low  { background-color: #3498db; }
        .badge-access-mid  { background-color: #f39c12; }
        .badge-access-high { background-color: #27ae60; }

        .action-buttons {
            display: flex;
            gap: 5px;
            justify-content: center;
        }

        .btn-action {
            padding: 6px 12px;
            border-radius: 6px;
            font-size: 12px;
            font-weight: 600;
            border: none;
            cursor: pointer;
            transition: transform 0.15s, box-shadow 0.15s;
            color: white;
            text-decoration: none;
            display: inline-block;
        }

        .btn-action:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
            color: white;
        }

        .btn-give {
            background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
        }

        .btn-take {
            background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%);
        }

        .btn-view {
            background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
        }

        .grid-access {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

        .grid-access th {
            background-color: #17a2b8;
            color: white;
            padding: 8px 6px;
            text-align: center;
            border: 1px solid #ddd;
            font-size: 12px;
        }

        .grid-access td {
            padding: 6px;
            border: 1px solid #e0e0e0;
            text-align: center;
        }

        .grid-access tr:hover { background-color: #f0f8ff; }

        .icon-yes { color: #28a745; font-weight: bold; font-size: 15px; }
        .icon-no  { color: #dc3545; font-size: 15px; }

        .info-bar {
            background-color: #e8f4f8;
            border-right: 4px solid #17a2b8;
            padding: 10px 15px;
            border-radius: 6px;
            margin-bottom: 15px;
            font-size: 13px;
        }

        .grid-personel .pagination {
            background-color: #f8f9fa;
            padding: 8px;
        }

        .grid-personel .pagination a,
        .grid-personel .pagination span {
            padding: 5px 10px;
            margin: 2px;
            border-radius: 4px;
            text-decoration: none;
            display: inline-block;
        }

        .grid-personel .pagination a {
            background-color: white;
            color: #4facfe;
            border: 1px solid #4facfe;
        }

        .grid-personel .pagination a:hover {
            background-color: #4facfe;
            color: white;
        }

        .grid-personel .pagination span {
            background-color: #4facfe;
            color: white;
            font-weight: bold;
        }
    </style>

    <script type="text/javascript">
        function confirmTake() {
            return confirm('آیا از گرفتن این دسترسی مطمئن هستید؟');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid mt-3">

        <h4 class="page-title">🔐 مدیریت دسترسی کاربران به فرم‌ها</h4>

        <div class="row">

            <!-- ============ ستون چپ: لیست پرسنل (بزرگ) ============ -->
            <div class="col-md-7">
                <div class="card card-personel">

                    <div class="card-header-gradient">
                        👥 لیست پرسنل
                        <span style="float:left; font-size:12px; opacity:0.85;">
                            <asp:Label ID="lblCount" runat="server"></asp:Label>
                        </span>
                    </div>

                    <div class="card-body">

                        <div class="search-box">
                            <asp:TextBox ID="txtSearch" runat="server"
                                CssClass="form-control"
                                placeholder="جستجو: نام، فامیل، کد پرسنلی، پست..."
                                AutoPostBack="true"
                                OnTextChanged="txtSearch_TextChanged">
                            </asp:TextBox>
                            <span class="search-icon">🔍</span>
                        </div>

                        <div style="text-align:center; margin-bottom:12px;">
                            <asp:Button ID="btnSearch" runat="server" Text="🔍 جستجو"
                                CssClass="btn btn-sm btn-primary" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="✖ پاک کردن"
                                CssClass="btn btn-sm btn-clear" OnClick="btnClear_Click" />
                        </div>

                        <div style="max-height: 700px; overflow-y: auto; border: 1px solid #e0e0e0; border-radius: 8px;">
                            <asp:GridView ID="gvPersonel" runat="server"
                                CssClass="grid-personel"
                                AutoGenerateColumns="false"
                                DataKeyNames="ID"
                                OnRowCommand="gvPersonel_RowCommand"
                                OnRowDataBound="gvPersonel_RowDataBound"
                                EmptyDataText="هیچ پرسنلی یافت نشد."
                                ShowHeaderWhenEmpty="true"
                                AllowPaging="true"
                                PageSize="25"
                                OnPageIndexChanging="gvPersonel_PageIndexChanging"
                                GridLines="None">

                                <Columns>

                                    <asp:TemplateField HeaderText="نام و فامیل">
                                        <ItemTemplate>
                                            <div class="personel-name">
                                                👤 <%# Eval("FullName") %>
                                                <br />
                                                <span class="personel-code">
                                                    کد: <%# Eval("PersonnelCode") %>
                                                    <%# If(Eval("PostName").ToString() <> "", " | " & Eval("PostName"), "") %>
                                                </span>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="دسترسی" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <span class='<%# GetAccessBadgeClass(Eval("ID")) %>'>
                                                <%# GetAccessCount(Eval("ID")) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <div class="action-buttons">

                                                <asp:LinkButton ID="lnkGive" runat="server"
                                                    CssClass="btn-action btn-give"
                                                    CommandName="GiveAccess"
                                                    CommandArgument='<%# Eval("ID") %>'
                                                    ToolTip="دادن دسترسی جدید">
                                                    ➕ دادن
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkTake" runat="server"
                                                    CssClass="btn-action btn-take"
                                                    CommandName="TakeAccess"
                                                    CommandArgument='<%# Eval("ID") %>'
                                                    OnClientClick="return confirmTake();"
                                                    ToolTip="گرفتن دسترسی‌ها">
                                                    ➖ گرفتن
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkView" runat="server"
                                                    CssClass="btn-action btn-view"
                                                    CommandName="SelectPersonel"
                                                    CommandArgument='<%# Eval("ID") %>'
                                                    ToolTip="مشاهده دسترسی‌ها">
                                                    👁️ نمایش
                                                </asp:LinkButton>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="pagination" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>

            <!-- ============ ستون راست: دسترسی‌های پرسنل انتخاب شده ============ -->
            <div class="col-md-5">

                <asp:Panel ID="pnlAccess" runat="server" Visible="false">
                    <div class="card card-personel">

                        <div class="card-header-success">
                            🛡️ دسترسی‌های:
                            <asp:Label ID="lblSelectedUser" runat="server" Font-Bold="true"></asp:Label>
                        </div>

                        <div class="card-body">

                            <div class="info-bar">
                                💡 برای ویرایش یا گرفتن دسترسی، از دکمه‌های ✏️ و 🗑️ استفاده کنید.
                            </div>

                            <div class="mb-3">
                                <asp:Button ID="btnAddNew" runat="server"
                                    Text="➕ دادن دسترسی جدید"
                                    CssClass="btn btn-success btn-sm"
                                    OnClick="btnAddNew_Click" />

                                <asp:Button ID="btnBulkAdd" runat="server"
                                    Text="📦 دادن گروهی"
                                    CssClass="btn btn-info btn-sm"
                                    OnClick="btnBulkAdd_Click" />
                            </div>

                            <div style="max-height: 600px; overflow-y: auto;">
                                <asp:GridView ID="gvAccess" runat="server"
                                    CssClass="grid-access"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="FrmID"
                                    OnRowCommand="gvAccess_RowCommand"
                                    EmptyDataText="هیچ دسترسی ثبت نشده است.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30px">
                                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FrmName" HeaderText="نام فرم" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="FrmCaption" HeaderText="عنوان فرم" />

                                        <asp:TemplateField HeaderText="✓" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <span class='<%# GetIconClass(Eval("FormAccess")) %>'>
                                                    <%# GetIconText(Eval("FormAccess")) %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="درج" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <span class='<%# GetIconClass(Eval("AllowNew")) %>'>
                                                    <%# GetIconText(Eval("AllowNew")) %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="50px">
                                            <ItemTemplate>
                                                <span class='<%# GetIconClass(Eval("AllowEdit")) %>'>
                                                    <%# GetIconText(Eval("AllowEdit")) %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="حذف" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <span class='<%# GetIconClass(Eval("AllowDelete")) %>'>
                                                    <%# GetIconText(Eval("AllowDelete")) %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="چاپ" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <span class='<%# GetIconClass(Eval("AllowPrint")) %>'>
                                                    <%# GetIconText(Eval("AllowPrint")) %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="90px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server"
                                                    CssClass="btn btn-warning btn-sm"
                                                    CommandName="EditAccess"
                                                    CommandArgument='<%# Eval("FrmID") %>'
                                                    ToolTip="ویرایش دسترسی"
                                                    Style="padding:2px 6px; font-size:11px;">✏️</asp:LinkButton>

                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                    CssClass="btn btn-danger btn-sm"
                                                    CommandName="DeleteAccess"
                                                    CommandArgument='<%# Eval("FrmID") %>'
                                                    OnClientClick="return confirm('آیا از گرفتن این دسترسی مطمئن هستید؟');"
                                                    ToolTip="گرفتن دسترسی"
                                                    Style="padding:2px 6px; font-size:11px;">🗑️</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </asp:Panel>

                <!-- پنل خالی: طراحی ساده‌تر -->
                <asp:Panel ID="pnlEmpty" runat="server">
                    <div class="card card-personel">
                        <div class="card-header-gradient" style="background: linear-gradient(135deg, #95a5a6 0%, #7f8c8d 100%);">
                            🛡️ دسترسی‌های پرسنل
                        </div>
                        <div class="card-body" style="text-align:center; padding: 40px 20px; color: #95a5a6;">
                            <div style="font-size: 40px; margin-bottom: 10px; opacity: 0.5;">👆</div>
                            <p style="margin: 0;">برای مشاهده دسترسی‌ها، روی دکمه <strong style="color:#4facfe;">👁️ نمایش</strong> کلیک کنید</p>
                        </div>
                    </div>
                </asp:Panel>

            </div>
        </div>
    </div>

</asp:Content>