<%@ Page Title="دوره‌های غیرحضوری" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnSessionOfflinePersonal.aspx.vb" Inherits="trnSessionOfflinePersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .page-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 20px;
            border-radius: 15px;
            margin-bottom: 25px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }
        
        .page-header h3 {
            margin: 0;
            font-weight: 600;
        }
        
        .page-header i {
            margin-left: 10px;
        }
        
        /* آیکون‌های جایگزین با ایموجی */
        .icon-emoji {
            font-size: 18px;
            display: inline-block;
        }
        
        .card-custom {
            background: white;
            border-radius: 15px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.08);
            padding: 25px;
            margin-bottom: 25px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }
        
        .card-custom:hover {
            box-shadow: 0 8px 25px rgba(0,0,0,0.15);
        }
        
        .card-title-custom {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 12px 20px;
            border-radius: 10px;
            margin: -25px -25px 20px -25px;
            font-size: 18px;
            font-weight: 600;
        }
        
        .card-title-custom i {
            margin-left: 8px;
        }
        
        .info-box {
            background: #f8f9fa;
            border-right: 4px solid #667eea;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
        }
        
        .info-box-success {
            background: #d4edda;
            border-right-color: #28a745;
            color: #155724;
        }
        
        .info-box-danger {
            background: #f8d7da;
            border-right-color: #dc3545;
            color: #721c24;
        }
        
        .info-box-warning {
            background: #fff3cd;
            border-right-color: #ffc107;
            color: #856404;
        }
        
        .form-label-custom {
            font-weight: 600;
            color: #333;
            margin-bottom: 8px;
            display: block;
        }
        
        .dropdown-custom {
            width: 100%;
            padding: 10px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 10px;
            background: #f8f9fa;
            font-size: 14px;
            transition: all 0.3s ease;
        }
        
        .dropdown-custom:focus {
            border-color: #667eea;
            outline: none;
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
            background: white;
        }
        
        .badge-custom {
            display: inline-block;
            padding: 8px 15px;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 600;
            margin: 5px;
        }
        
        .badge-primary {
            background: #667eea;
            color: white;
        }
        
        .badge-success {
            background: #28a745;
            color: white;
        }
        
        .badge-warning {
            background: #ffc107;
            color: #333;
        }
        
        .badge-danger {
            background: #dc3545;
            color: white;
        }
        
        .btn-custom {
            padding: 10px 25px;
            border-radius: 25px;
            font-weight: 600;
            transition: all 0.3s ease;
        }
        
        .btn-custom:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }
        
        .gridview-custom {
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }
        
        .gridview-custom table {
            width: 100%;
            border-collapse: collapse;
        }
        
        .gridview-custom th {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 12px;
            font-weight: 600;
            text-align: center;
        }
        
        .gridview-custom td {
            padding: 10px;
            text-align: center;
            border-bottom: 1px solid #e0e0e0;
        }
        
        .gridview-custom tr:hover {
            background: #f8f9fa;
        }
        
        .status-approved {
            color: #28a745;
            font-weight: 600;
        }
        
        .status-pending {
            color: #ffc107;
            font-weight: 600;
        }
        
        .status-rejected {
            color: #dc3545;
            font-weight: 600;
        }
        
        .link-cancel {
            background: #dc3545;
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            text-decoration: none;
            font-size: 12px;
            transition: all 0.3s ease;
            display: inline-block;
        }
        
        .link-cancel:hover {
            background: #c82333;
            color: white;
            text-decoration: none;
            transform: scale(1.05);
        }
        
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        .fade-in {
            animation: fadeIn 0.5s ease;
        }
        
        /* استایل برای آیکون‌های جایگزین */
        .fa, .fas, .far, .fab {
            font-family: 'Segoe UI', 'Tahoma', sans-serif;
        }
        
        /* ایموجی‌های جایگزین برای Font Awesome */
        .fa-graduation-cap:before { content: "🎓"; }
        .fa-clipboard-list:before { content: "📋"; }
        .fa-info-circle:before { content: "ℹ️"; }
        .fa-exclamation-triangle:before { content: "⚠️"; }
        .fa-book-open:before { content: "📖"; }
        .fa-users:before { content: "👥"; }
        .fa-list-alt:before { content: "📃"; }
        .fa-times:before { content: "✖"; }
        .fa-inbox:before { content: "📥"; }
        
        @media (max-width: 768px) {
            .card-custom {
                padding: 15px;
            }
            
            .btn-custom {
                width: 100%;
                margin-top: 10px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container-fluid fade-in">
        
        <!-- هدر صفحه -->
        <div class="page-header">
            <h3>
                <span class="icon-emoji">🎓</span>
                درخواست شرکت در دوره آموزشی غیرحضوری
            </h3>
        </div>
        
        <!-- کارت اصلی درخواست -->
        <div class="card-custom">
            <div class="card-title-custom">
                <span class="icon-emoji">📋</span>
                فرم ثبت درخواست دوره غیرحضوری
            </div>
            
            <!-- اطلاعات راهنما -->
            <div class="info-box info-box-success">
                <span class="icon-emoji">ℹ️</span>
                <strong>نحوه ثبت درخواست:</strong><br />
                پس از ثبت درخواست، به کارتابل سرپرست مربوطه ارسال می‌گردد تا میزان تأثیرگذاری دوره را تعیین نمایند.
                در صورت تأیید، پس از شرکت در آزمون تعیین سطح و با توجه به ظرفیت دوره، برای شرکت دعوت خواهید شد.
            </div>
            
            <div class="info-box info-box-warning">
                <span class="icon-emoji">⚠️</span>
                <strong>نکته مهم:</strong><br />
                جهت اطلاع از نتیجه درخواست خود می‌توانید به بخش 
                <strong>آموزش / ثبت نام دوره‌های آموزشی / لیست درخواست‌های من / نتیجه</strong> مراجعه نمایید.
            </div>
            
            <br />
            
            <!-- انتخاب دوره -->
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label class="form-label-custom">
                            <span class="icon-emoji">📖</span> انتخاب دوره آموزشی
                        </label>
                        <asp:DropDownList ID="citytToSiteNoRuzkar" runat="server" 
                            CssClass="dropdown-custom" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            
            <!-- نمایش وضعیت ظرفیت -->
            <div class="row mt-3">
                <div class="col-md-6">
                    <div class="info-box" style="background: #e3f2fd;">
                        <span class="icon-emoji">👥</span>
                        <strong>وضعیت ثبت‌نام:</strong><br />
                        <asp:Label ID="lblBagi" runat="server" Text="2" Font-Bold="True" ForeColor="#1565C0"></asp:Label>
                        <asp:Label ID="lblTedad" runat="server" Text="0" Font-Bold="True" ForeColor="#1565C0" Visible="false"></asp:Label>
                        <asp:Label ID="lblzarf" runat="server" Font-Bold="True" ForeColor="#1565C0" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- فضای خالی برای اطلاعات بیشتر -->
                </div>
            </div>
            
            <br />
            
            <!-- دکمه ثبت نام -->
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnAdd" runat="server" Text="ثبت درخواست شرکت در دوره" 
                        CssClass="btn btn-custom btn-success" 
                        Style="background: linear-gradient(135deg, #28a745 0%, #20c997 100%); color: white; padding: 12px 40px; font-size: 16px; border: none; cursor: pointer;"
                        ValidationGroup="1" />
                </div>
            </div>
            
            <asp:HiddenField ID="HiddenFieldSabege" runat="server" />
        </div>
        
        <!-- لیست دوره‌های ثبت نام شده -->
        <div class="card-custom">
            <div class="card-title-custom">
                <span class="icon-emoji">📃</span>
                لیست دوره‌های غیرحضوری من
            </div>
            
            <div class="table-responsive gridview-custom">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    Font-Size="13px" TabIndex="12" Width="100%" 
                    DataKeyNames="idd" BorderStyle="None" GridLines="None"
                    CssClass="table table-hover">
                    
                    <AlternatingRowStyle BackColor="#F8F9FA" />
                    
                    <Columns>
                        <asp:BoundField DataField="idd" HeaderText="#" HeaderStyle-Width="50px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="onvan" HeaderText="عنوان دوره">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="tarixShuru" HeaderText="از تاریخ">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="tarixEtmam" HeaderText="تا تاریخ">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="tarix" HeaderText="تاریخ ثبت نام">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="statValue" HeaderText="وضعیت">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" CssClass="status-pending" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="sarparast" HeaderText="سرپرست مربوطه">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="arzyabiAdmin" HeaderText="نتیجه">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="scoreval" HeaderText="تعیین سطح">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="notee" HeaderText="توضیحات آموزش">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="عملیات" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelFile" runat="server" 
                                    CssClass="link-cancel"
                                    OnClientClick="return confirmCancelEvent()"  
                                    OnClick="lnkDelFile_Click">
                                    <span class="icon-emoji">✖</span> ابطال
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    
                    <HeaderStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="grid-row" />
                    <EmptyDataTemplate>
                        <div class="text-center p-5">
                            <span class="icon-emoji" style="font-size: 48px; color: #ccc;">📥</span>
                            <p class="mt-3">هیچ دوره‌ای ثبت نام نشده است</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
        
        <!-- دکمه برگشت -->
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnBack" runat="server" 
                    CssClass="btn btn-warning btn-custom" 
                    Text="بازگشت به صفحه قبل"
                    Style="background: linear-gradient(135deg, #ffc107 0%, #ff9800 100%); color: #333; padding: 10px 30px; border: none; cursor: pointer;" />
            </div>
        </div>
        
        <!-- فیلدهای مخفی -->
        <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
        
    </div>
    
    <br />

<%--    <script type="text/javascript">
        // تابع تایید ابطال با confirm معمولی (بدون SweetAlert)
        function confirmCancelEvent() {
            Swal.fire({
                title: 'تایید ابطال',
                text: 'آیا مطمئن هستید که می‌خواهید این درخواست را ابطال کنید؟',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'بله، ابطال شود',
                cancelButtonText: 'خیر، انصراف'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack('ctl00$ContentPlaceHolder1$GridView2$ctl02$lnkdelFile', '');
        }
        });
        return false;
        }
    </script>--%>

</asp:Content>