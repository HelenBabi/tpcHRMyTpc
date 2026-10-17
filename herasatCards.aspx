<%@ Page Title="مدیریت پرسنل - کارت‌های قرنطینه" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="herasatCards.aspx.vb" Inherits="herasatCards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-header {
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            color: white;
            padding: 15px 20px;
            border-radius: 10px 10px 0 0;
        }
        .card-header h5 { margin: 0; font-weight: bold; }
        .card-body { padding: 20px; background: #f8f9fa; border-radius: 0 0 10px 10px; }
        .form-control { border-radius: 8px; border: 1.5px solid #ddd; }
        .form-control:focus { border-color: #1e3c72; box-shadow: 0 0 0 3px rgba(30, 60, 114, 0.15); }
        .btn { border-radius: 8px; padding: 8px 25px; font-weight: 600; }
        .btn-success { background: #27ae60; border: none; }
        .btn-success:hover { background: #219a52; }
        .btn-warning { background: #f39c12; border: none; color: white; }
        .btn-warning:hover { background: #e08e0b; color: white; }
        .btn-danger { background: #e74c3c; border: none; }
        .btn-danger:hover { background: #c0392b; }
        .btn-secondary { background: #95a5a6; border: none; }
        .btn-secondary:hover { background: #7f8c8d; }
        .table { font-size: 13px; text-align: right; }
        .table th { background: #2c3e50; color: white; text-align: center; vertical-align: middle; }
        .table td { vertical-align: middle; text-align: center; }
        .table-striped tbody tr:nth-of-type(odd) { background: #f8f9fa; }
        .table-hover tbody tr:hover { background: #e8f4f8; }
        .pagination-container { padding: 10px 0; }
        .pagination-container a, .pagination-container span { 
            padding: 5px 12px; border: 1px solid #ddd; margin: 0 2px; border-radius: 5px; 
            color: #2c3e50; text-decoration: none;
        }
        .pagination-container a:hover { background: #1e3c72; color: white; border-color: #1e3c72; }
        .pagination-container span { background: #1e3c72; color: white; border-color: #1e3c72; }
        .alert { padding: 10px 15px; border-radius: 8px; margin: 10px 0; }
        .alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .mt-2 { margin-top: 10px; }
        .mt-3 { margin-top: 15px; }
        .mt-4 { margin-top: 20px; }
        .mb-2 { margin-bottom: 10px; }
        .mb-3 { margin-bottom: 15px; }
        .row { display: flex; flex-wrap: wrap; margin: 0 -10px; }
        .col-md-3 { flex: 0 0 25%; max-width: 25%; padding: 0 10px; box-sizing: border-box; }
        .col-md-12 { flex: 0 0 100%; max-width: 100%; padding: 0 10px; box-sizing: border-box; }
        .text-center { text-align: center; }
        .text-danger { color: #e74c3c; }
        .text-success { color: #27ae60; }
        label { font-weight: 600; color: #2c3e50; margin-bottom: 5px; display: block; }
        .form-group { margin-bottom: 15px; }
        .photo-preview { 
            width: 100px; height: 120px; border: 2px dashed #ddd; 
            border-radius: 10px; display: flex; align-items: center; 
            justify-content: center; margin: 10px auto;
        }
        .photo-preview img { max-width: 100%; max-height: 100%; border-radius: 8px; }
        @media (max-width: 768px) {
            .col-md-3 { flex: 0 0 100%; max-width: 100%; }
            .table { font-size: 11px; }
        }
        .toolbar {
            background: #ecf0f1;
            padding: 10px 15px;
            border-radius: 8px;
            margin-bottom: 15px;
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 10px;
        }
        .toolbar .btn { padding: 5px 15px; font-size: 12px; }
        .toolbar .form-control { width: 200px; display: inline-block; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid" style="max-width: 1400px; margin: 0 auto; padding: 20px;">
        
        <!-- عنوان صفحه -->
        <div class="card-header" style="border-radius: 10px; margin-bottom: 20px;">
            <h5>📋 مدیریت پرسنل - کارت‌های متفرقه</h5>
        </div>

        <!-- ============================================
        بخش افزودن/ویرایش پرسنل
        ============================================ -->
       <!-- ============================================
بخش افزودن/ویرایش پرسنل
============================================ -->
<div class="card mt-3">
    <div class="card-header">
        <h5 id="formTitle" runat="server">➕ افزودن پرسنل جدید</h5>
    </div>
    <div class="card-body">
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div id="divMessage" runat="server" class="alert">
                <asp:Label ID="lblMessageIcon" runat="server" />
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>کد پرسنلی:</label>
                    <asp:TextBox ID="txtCodePersoneli" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvCode" runat="server" 
                        ControlToValidate="txtCodePersoneli" 
                        ErrorMessage="کد پرسنلی الزامی است" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revCode" runat="server" 
                        ControlToValidate="txtCodePersoneli" 
                        ValidationExpression="^\d+$" 
                        ErrorMessage="کد پرسنلی باید عدد باشد" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>کد ملی:</label>
                    <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvNational" runat="server" 
                        ControlToValidate="txtNationality" 
                        ErrorMessage="کد ملی الزامی است" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revNational" runat="server" 
                        ControlToValidate="txtNationality" 
                        ValidationExpression="^\d{10}$" 
                        ErrorMessage="کد ملی باید 10 رقم باشد" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>نام و نام خانوادگی:</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" 
                        ControlToValidate="txtFullName" 
                        ErrorMessage="نام الزامی است" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>شیفت:</label>
                    <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control" Height="40px">
                        <asp:ListItem Text="انتخاب کنید" Value="" />
                        <asp:ListItem Text="روزکار" Value="روزکار" />
                        <asp:ListItem Text="شبکار" Value="شبکار" />
                        <asp:ListItem Text="چرخشی" Value="چرخشی" />
                        <asp:ListItem Text="اداری" Value="اداری" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>واحد سازمانی:</label>
                    <!-- تغییر از DropDownList به TextBox -->
                    <asp:TextBox ID="txtOmur" runat="server" CssClass="form-control" placeholder="مثال: مالی، منابع انسانی، ..." />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>تاریخ شروع بکار:</label>
                    <asp:TextBox ID="txtTarixStart" runat="server" CssClass="form-control" placeholder="1400/01/01" />
                    <asp:RegularExpressionValidator ID="revDate" runat="server" 
                        ControlToValidate="txtTarixStart" 
                        ValidationExpression="^\d{4}/\d{2}/\d{2}$" 
                        ErrorMessage="فرمت تاریخ صحیح نیست (مثال: 1400/01/01)" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>تاریخ انقضای کارت:</label>
                    <asp:TextBox ID="txtExpireDate" runat="server" CssClass="form-control" placeholder="1404/01/01" />
                    <asp:RegularExpressionValidator ID="revExpire" runat="server" 
                        ControlToValidate="txtExpireDate" 
                        ValidationExpression="^\d{4}/\d{2}/\d{2}$" 
                        ErrorMessage="فرمت تاریخ صحیح نیست (مثال: 1404/01/01)" 
                        ForeColor="Red" ValidationGroup="Personnel" Display="Dynamic" />
                </div>
            </div>
        <div class="col-md-3">
    <div class="form-group">
        <label>بارگذاری تصویر:</label>
        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
        <small class="text-muted">فرمت‌های مجاز: JPG, PNG, GIF, BMP - حداکثر 2 مگابایت</small>
    </div>
</div>
        </div>
        
        <div class="row mt-2">
            <div class="col-md-12 text-center">
                <asp:HiddenField ID="hfEditID" runat="server" Value="0" />
                <asp:Button ID="btnSavePersonnel" runat="server" Text="✅ ذخیره" 
                    CssClass="btn btn-success" OnClick="btnSavePersonnel_Click" 
                    ValidationGroup="Personnel" />
                <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" 
                    CssClass="btn btn-secondary" OnClick="btnCancel_Click" 
                    CausesValidation="false" />
            </div>
        </div>
    </div>
</div>

        <!-- ============================================
        نوار ابزار جستجو و انتخاب
        ============================================ -->
        <div class="toolbar">
            <div class="form-group" style="margin-bottom:0; display:flex; align-items:center; gap:10px; flex-wrap:wrap;">
              <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ToolTip="جستجو..." />
                <asp:Button ID="btnSearch" runat="server" Text="🔍 جستجو" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSelectFirst80" runat="server" Text="📋 انتخاب ۸۰ نفر اول" CssClass="btn btn-info" OnClick="btnSelectFirst80_Click" />
                <asp:Button ID="btnSelectNext80" runat="server" Text="📋 انتخاب ۸۰ نفر بعدی" CssClass="btn btn-info" OnClick="btnSelectNext80_Click" />
                <asp:Button ID="btnPrint" runat="server" Text="🖨️ چاپ کارت‌ها" CssClass="btn btn-success" OnClick="btnPrint_Click" />
                <asp:Label ID="lblTotalCount" runat="server" CssClass="text-muted" Text="تعداد کل: 0 نفر" />
            </div>
        </div>

        <!-- ============================================
        GridView نمایش پرسنل
        ============================================ -->
        <div class="table-responsive">
            <asp:GridView ID="gridPersonnel" runat="server" 
                CssClass="table table-striped table-bordered table-hover"
                PagerStyle-CssClass="pagination-container justify-content-center"
                AllowPaging="True" PageSize="20"
                PagerSettings-Mode="Numeric"
                OnPageIndexChanging="gridPersonnel_PageIndexChanging"
                OnRowCommand="gridPersonnel_RowCommand"
                OnRowDataBound="gridPersonnel_RowDataBound"
                DataKeyNames="ID,codePersoneli,Nationality,PhotoPath"
                AutoGenerateColumns="False"
                ClientIDMode="Static">

                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                    <asp:BoundField DataField="codePersoneli" HeaderText="کد پرسنلی" />
                    <asp:BoundField DataField="Nationality" HeaderText="کد ملی" />
                    <asp:BoundField DataField="fulname" HeaderText="نام و نام خانوادگی" />
                    <asp:BoundField DataField="shift" HeaderText="شیفت" />
                    <asp:BoundField DataField="omur" HeaderText="واحد" />
                    <asp:BoundField DataField="tarixStart" HeaderText="تاریخ شروع" />
                    <asp:BoundField DataField="expireDateCard" HeaderText="تاریخ انقضا" />
                    
                    <asp:TemplateField HeaderText="عکس" ItemStyle-Width="80">
                        <ItemTemplate>
                            <asp:Image ID="imgPhoto" runat="server" Width="60px" Height="75px" 
                                       Style="border:1px solid #ccc; border-radius:5px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="✏️" CssClass="btn btn-sm btn-warning" 
                                CommandName="EditPersonnel" CommandArgument='<%# Eval("ID") %>' ToolTip="ویرایش" />
                            <asp:Button ID="btnDelete" runat="server" Text="🗑️" CssClass="btn btn-sm btn-danger" 
                                CommandName="DeletePersonnel" CommandArgument='<%# Eval("ID") %>' 
                                OnClientClick="return confirm('آیا از حذف این پرسنل مطمئن هستید؟');" ToolTip="حذف" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="انتخاب" ItemStyle-Width="60">
                        <HeaderTemplate>
                            <input type="checkbox" id="chkSelectAll" onclick="toggleAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="rowCheck" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <PagerStyle CssClass="pagination-container justify-content-center" />
                <EmptyDataTemplate>
                    <div class="alert alert-info text-center">هیچ پرسنلی یافت نشد</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <!-- ============================================
    اسکریپت‌های جاوااسکریپت
    ============================================ -->
    <script type="text/javascript">
        function toggleAll(source) {
            var checkboxes = document.querySelectorAll('.rowCheck');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }
        }

        // نمایش پیش‌نمایش تصویر
        document.getElementById('<%= fileUpload.ClientID %>').addEventListener('change', function(e) {
            var file = this.files[0];
            if (file) {
                var reader = new FileReader();
                reader.onload = function(e) {
                    var preview = document.getElementById('photoPreview');
                    if (!preview) {
                        preview = document.createElement('div');
                        preview.id = 'photoPreview';
                        preview.className = 'photo-preview';
                        var img = document.createElement('img');
                        img.id = 'previewImg';
                        preview.appendChild(img);
                        this.parentNode.appendChild(preview);
                    }
                    document.getElementById('previewImg').src = e.target.result;
                }.bind(this);
                reader.readAsDataURL(file);
            }
        });
    </script>
    <script>
    document.getElementById('<%= txtSearch.ClientID %>').placeholder = 'جستجو...';
</script>
</asp:Content>