<%@ Page Title="دوره آموزشی غیرحضوری" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnQsEntryOneSessionFrm.aspx.vb" Inherits="trnQsEntryOneSessionFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        :root {
            --primary-color: #667eea;
            --secondary-color: #764ba2;
            --success-color: #28a745;
            --info-color: #17a2b8;
            --warning-color: #ffc107;
        }
        
        .fade-in {
            animation: fadeIn 0.6s ease-in;
        }
        
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        .page-header {
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            color: white;
            padding: 20px 25px;
            border-radius: 15px;
            margin-bottom: 30px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }
        
        .page-header h2 {
            margin: 0;
            font-weight: 600;
        }
        
        .page-header i {
            margin-left: 10px;
        }
        
        .card-glass {
            background: white;
            border-radius: 20px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.08);
            overflow: hidden;
            margin-bottom: 25px;
        }
        
        .card-header-custom {
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            color: white;
            padding: 15px 20px;
            font-size: 18px;
            font-weight: 600;
        }
        
        .card-header-custom i {
            margin-left: 10px;
        }
        
        .card-body-custom {
            padding: 25px;
        }
        
        .form-label-custom {
            display: block;
            font-weight: 600;
            color: #333;
            margin-bottom: 8px;
        }
        
        .form-label-custom i {
            margin-left: 8px;
            color: var(--primary-color);
        }
        
        .dropdown-custom {
            width: 100%;
            padding: 12px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            font-size: 14px;
            transition: all 0.3s ease;
            background: #f8f9fa;
        }
        
        .dropdown-custom:focus {
            border-color: var(--primary-color);
            outline: none;
            box-shadow: 0 0 0 3px rgba(102,126,234,0.1);
        }
        
        .file-card {
            background: linear-gradient(135deg, #667eea15 0%, #764ba215 100%);
            border-radius: 15px;
            padding: 15px;
            text-align: center;
            transition: all 0.3s ease;
            cursor: pointer;
            border: 2px solid transparent;
            margin: 10px;
            display: inline-block;
            width: 120px;
        }
        
        .file-card:hover {
            transform: translateY(-5px);
            border-color: var(--primary-color);
        }
        
        .file-card img {
            transition: transform 0.3s ease;
        }
        
        .file-card:hover img {
            transform: scale(1.05);
        }
        
        .file-title {
            display: block;
            margin-top: 10px;
            font-weight: 600;
            color: var(--primary-color);
            text-decoration: none;
            font-size: 12px;
        }
        
        .file-title:hover {
            color: var(--secondary-color);
            text-decoration: none;
        }
        
        .btn-start {
            background: linear-gradient(135deg, var(--success-color) 0%, #20c997 100%);
            color: white;
            border: none;
            padding: 14px 35px;
            border-radius: 50px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            width: 100%;
        }
        
        .btn-start:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(40,167,69,0.3);
        }
        
        .btn-back {
            background: linear-gradient(135deg, var(--warning-color) 0%, #ff9800 100%);
            color: #333;
            border: none;
            padding: 10px 25px;
            border-radius: 50px;
            font-weight: 600;
            transition: all 0.3s ease;
        }
        
        .btn-back:hover {
            transform: translateX(-5px);
            box-shadow: 0 5px 15px rgba(255,193,7,0.3);
        }
        
        .alert-success-custom {
            background: #d4edda;
            color: #155724;
            border-right: 4px solid #28a745;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 20px;
        }
        
        .alert-info-custom {
            background: #d1ecf1;
            color: #0c5460;
            border-right: 4px solid #17a2b8;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 20px;
        }
        
        .step-indicator {
            display: flex;
            justify-content: space-between;
            margin-bottom: 30px;
            position: relative;
        }
        
        .step {
            flex: 1;
            text-align: center;
            position: relative;
            z-index: 1;
        }
        
        .step .step-circle {
            width: 40px;
            height: 40px;
            background: white;
            border: 2px solid var(--primary-color);
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            color: var(--primary-color);
        }
        
        .step.active .step-circle {
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            color: white;
            border-color: transparent;
        }
        
        .step.completed .step-circle {
            background: var(--success-color);
            color: white;
            border-color: transparent;
        }
        
        .step-label {
            display: block;
            margin-top: 10px;
            font-size: 12px;
            color: #666;
        }
        
        .step.active .step-label {
            color: var(--primary-color);
            font-weight: 600;
        }
        
        .step-line {
            position: absolute;
            top: 20px;
            left: 0;
            right: 0;
            height: 2px;
            background: #e0e0e0;
            z-index: 0;
        }
        
        @media (max-width: 768px) {
            .card-body-custom {
                padding: 15px;
            }
            .file-card {
                width: 100px;
                margin: 5px;
            }
            .file-card img {
                width: 60px;
                height: 60px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container-fluid fade-in">
        
        <!-- هدر صفحه -->
        <div class="page-header">
            <h2>
                <i class="fas fa-chalkboard-teacher"></i>
                دوره آموزشی غیرحضوری
            </h2>
            <p class="mb-0 mt-2">لطفاً مراحل زیر را به ترتیب طی نمایید</p>
        </div>
        
        <!-- پیام‌ها -->
        <div class="alert-success-custom" id="divSuccess" runat="server" visible="false">
            <i class="fas fa-check-circle"></i>
            <asp:Label ID="lblPayam" runat="server"></asp:Label>
        </div>
        
        <div class="alert-info-custom" id="divInfo" runat="server" visible="false">
            <i class="fas fa-info-circle"></i>
            <asp:Label ID="lblPayam2" runat="server"></asp:Label>
        </div>
        
        <br />
        
        <!-- نشانگر مراحل -->
        <div class="step-indicator">
            <div class="step-line"></div>
            <div class="step" id="step1">
                <div class="step-circle">1</div>
                <span class="step-label">انتخاب دوره</span>
            </div>
            <div class="step" id="step2">
                <div class="step-circle">2</div>
                <span class="step-label">مطالعه فایل‌ها</span>
            </div>
            <div class="step" id="step3">
                <div class="step-circle">3</div>
                <span class="step-label">شرکت در آزمون</span>
            </div>
        </div>
        
        <!-- کارت اصلی -->
        <asp:Panel ID="Panel3" runat="server">
            <div class="card-glass">
                <div class="card-header-custom">
                    <i class="fas fa-graduation-cap"></i>
                    اطلاعات دوره غیرحضوری
                </div>
                <div class="card-body-custom">
                    
                    <div class="form-group">
                        <label class="form-label-custom">
                            <i class="fas fa-book"></i> عنوان دوره آموزشی
                        </label>
                        <asp:DropDownList ID="CmbUnit" runat="server" CssClass="dropdown-custom" AutoPostBack="True" Height="40px">
                        </asp:DropDownList>
                    </div>
                    
                    <hr />
                    
                    <!-- فایل‌های آموزشی -->
                    <div class="card-glass mt-3">
                        <div class="card-header-custom" style="background: linear-gradient(135deg, #17a2b8 0%, #138496 100%);">
                            <i class="fas fa-file-video"></i>
                            مرحله اول - مشاهده فایل‌های آموزشی دوره
                        </div>
                        <div class="card-body-custom">
                            <div class="text-center">
                                <asp:DataList ID="DataList1" runat="server" 
                                    RepeatColumns="4" 
                                    RepeatDirection="Horizontal" 
                                    Width="100%" 
                                    CellPadding="10"
                                    BorderStyle="None"
                                    HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div class="file-card">
                                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                                Height="80px" 
                                                Width="80px" 
                                                ImageUrl="~/Icons/file-64.png" 
                                                PostBackUrl='<%# String.Format("trnQsLearnFilesDlFrm.aspx?id={0}", Eval("jalaseNO"))%>' />
                                            <br />
                                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                                Font-Size="12px" 
                                CssClass="file-title"
                                Text='<%# Eval("jalaseValue")%>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </asp:Panel>
        
        <br />
        
        <!-- مرحله آزمون -->
        <asp:Panel ID="Panel2" runat="server">
            <div class="card-glass">
                <div class="card-header-custom" style="background: linear-gradient(135deg, #28a745 0%, #20c997 100%);">
                    <i class="fas fa-question-circle"></i>
                    مرحله دوم - آزمون ارزیابی
                </div>
                <div class="card-body-custom text-center">
                    <div class="p-3" style="background: #e8f5e9; border-radius: 12px; margin-bottom: 20px;">
                        <i class="fas fa-clock" style="font-size: 24px; color: #28a745;"></i>
                        <p class="mt-2 mb-0">پس از مطالعه کامل فایل‌های آموزشی، در آزمون شرکت نمایید</p>
                        <small class="text-muted">تعداد سوالات: 20 | زمان تقریبی: 30 دقیقه</small>
                    </div>
                    <asp:Button ID="Button1" runat="server" CssClass="btn-start" Text="شروع آزمون" />
                </div>
            </div>
        </asp:Panel>
        
        <br />
        
        <!-- دکمه برگشت -->
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnBack" runat="server" CssClass="btn-back float-left" Text="برگشت" />
            </div>
        </div>
        
    </div>
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    
    <script>
        $(document).ready(function () {
            $('.file-card').hover(
                function () { $(this).find('img').css('transform', 'scale(1.05)'); },
                function () { $(this).find('img').css('transform', 'scale(1)'); }
            );
        });
    </script>

</asp:Content>