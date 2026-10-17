<%@ Page Title="گزارش آزمون" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="ExamReport.aspx.vb" Inherits="ExamReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .report-container { max-width: 1000px; margin: 0 auto; padding: 20px; }
        .report-header {
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            color: white;
            padding: 25px 30px;
            border-radius: 15px;
            margin-bottom: 30px;
        }
        .report-header h2 { margin: 0; font-size: 24px; }
        .report-header p { margin: 10px 0 0; opacity: 0.9; }
        
        .detail-section {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
            border-right: 4px solid #1e3c72;
        }
        .detail-section h6 {
            color: #1e3c72;
            font-weight: bold;
            margin-bottom: 15px;
            border-bottom: 2px solid #e0e0e0;
            padding-bottom: 10px;
        }
        .detail-row {
            display: flex;
            padding: 6px 0;
            border-bottom: 1px dashed #e0e0e0;
        }
        .detail-row:last-child { border-bottom: none; }
        .detail-label { width: 130px; font-weight: 600; color: #555; }
        .detail-value { flex: 1; color: #333; }
        
        .national-code {
            font-family: 'Courier New', monospace;
            font-weight: 600;
            letter-spacing: 2px;
            color: #1e3c72;
            direction: ltr;
            font-size: 14px;
        }
        
        .answer-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }
        .answer-table th {
            background: #1e3c72;
            color: white;
            padding: 10px 12px;
            text-align: center;
        }
        .answer-table td {
            padding: 8px 12px;
            border-bottom: 1px solid #e0e0e0;
            text-align: center;
        }
        .answer-table tr:hover { background: #f0f8ff; }
        .answer-correct { background: #d4edda; color: #155724; font-weight: bold; }
        .answer-wrong { background: #f8d7da; color: #721c24; font-weight: bold; }
        
        .badge { display: inline-block; padding: 5px 12px; border-radius: 20px; font-size: 12px; }
        .bg-success { background: #d4edda; color: #155724; }
        .bg-danger { background: #f8d7da; color: #721c24; }
        .bg-warning { background: #fff3cd; color: #856404; }
        
        .alert {
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
        }
        .alert-danger { background: #f8d7da; color: #721c24; border: 2px solid #dc3545; }
        
        .btn-back {
            background: #6c757d;
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.3s;
        }
        .btn-back:hover { background: #5a6268; color: white; }
        
        .text-muted { color: #6c757d; }
        .mt-3 { margin-top: 15px; }
        .mt-4 { margin-top: 20px; }
        
        @media (max-width: 768px) {
            .detail-row { flex-direction: column; }
            .detail-label { width: auto; }
            .report-container { padding: 10px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="report-container">
        
        <div class="report-header">
            <h2>📊 گزارش کامل آزمون و ارزیابی</h2>
            <p><asp:Label ID="lblCourseTitle" runat="server" Text="-" /></p>
        </div>
        
        <!-- ============================================================
        اطلاعات پرسنل و دوره
        ============================================================ -->
        <asp:Panel ID="pnlInfo" runat="server" Visible="false">
            
            <div class="detail-section">
                <h6><i class="mdi mdi-account"></i> اطلاعات پرسنل و دوره</h6>
                <div class="detail-row">
                    <span class="detail-label">پرسنل:</span>
                    <span class="detail-value"><asp:Label ID="lblPersonnel" runat="server" /></span>
                </div>
                <div class="detail-row">
                    <span class="detail-label">کد پرسنلی:</span>
                    <span class="detail-value"><asp:Label ID="lblCode" runat="server" /></span>
                </div>
                <div class="detail-row">
                    <span class="detail-label">کد ملی:</span>
                    <span class="detail-value"><asp:Label ID="lblNationalCode" runat="server" CssClass="national-code" /></span>
                </div>
                <div class="detail-row">
                    <span class="detail-label">واحد:</span>
                    <span class="detail-value"><asp:Label ID="lblUnit" runat="server" /></span>
                </div>
                <div class="detail-row">
                    <span class="detail-label">دوره:</span>
                    <span class="detail-value"><asp:Label ID="lblCourse" runat="server" /></span>
                </div>
                <div class="detail-row">
                    <span class="detail-label">تاریخ ثبت‌نام:</span>
                    <span class="detail-value"><asp:Label ID="lblRegDate" runat="server" /></span>
                </div>
                <div class="detail-row">
                    <span class="detail-label">وضعیت تایید:</span>
                    <span class="detail-value"><asp:Label ID="lblStatus" runat="server" /></span>
                </div>
            </div>
            
            <!-- ============================================================
            نتیجه آزمون
            ============================================================ -->
            <div class="detail-section">
                <h6><i class="mdi mdi-clipboard-check"></i> نتیجه آزمون</h6>
                
                <asp:Panel ID="pnlExam" runat="server" Visible="false">
                    <div class="detail-row">
                        <span class="detail-label">نمره:</span>
                        <span class="detail-value"><strong><asp:Label ID="lblScore" runat="server" /></strong></span>
                    </div>
                    <div class="detail-row">
                        <span class="detail-label">پاسخ صحیح:</span>
                        <span class="detail-value"><asp:Label ID="lblCorrect" runat="server" /></span>
                    </div>
                    <div class="detail-row">
                        <span class="detail-label">وضعیت:</span>
                        <span class="detail-value"><asp:Label ID="lblExamStatus" runat="server" /></span>
                    </div>
                    <div class="detail-row">
                        <span class="detail-label">تاریخ آزمون:</span>
                        <span class="detail-value"><asp:Label ID="lblExamDate" runat="server" /></span>
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="pnlNoExam" runat="server" Visible="false">
                    <div class="text-muted">⏳ این پرسنل هنوز در آزمون شرکت نکرده است.</div>
                </asp:Panel>
            </div>
            
            <!-- ============================================================
            پاسخ‌های داده شده
            ============================================================ -->
            <div class="detail-section">
                <h6><i class="mdi mdi-format-list-bulleted"></i> پاسخ‌های داده شده</h6>
                
                <asp:Panel ID="pnlAnswers" runat="server">
                    <asp:Repeater ID="rptAnswers" runat="server">
                        <HeaderTemplate>
                            <table class="answer-table">
                                <thead>
                                    <tr>
                                        <th style="width:50px;">#</th>
                                        <th style="text-align:right;">سوال</th>
                                        <th>پاسخ</th>
                                        <th style="width:100px;">وضعیت</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td style="text-align:right;"><%# Eval("QuestionText") %></td>
                                <td><%# Eval("AnswerText") %></td>
                                <td class='<%# IIf(Convert.ToBoolean(Eval("IsCorrect")), "answer-correct", "answer-wrong") %>'>
                                    <%# IIf(Convert.ToBoolean(Eval("IsCorrect")), "✅ صحیح", "❌ غلط") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
            
            <!-- ============================================================
            وضعیت ارزیابی
            ============================================================ -->
            <div class="detail-section">
                <h6><i class="mdi mdi-star"></i> وضعیت ارزیابی</h6>
                
                <asp:Panel ID="pnlEval" runat="server" Visible="false">
                    <div class="detail-row">
                        <span class="detail-label">وضعیت:</span>
                        <span class="detail-value"><span class="badge bg-success">✅ ارزیابی شده</span></span>
                    </div>
                    <div class="detail-row">
                        <span class="detail-label">تاریخ ارزیابی:</span>
                        <span class="detail-value"><asp:Label ID="lblEvalDate" runat="server" /></span>
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="pnlNoEval" runat="server" Visible="false">
                    <div class="text-muted">⏳ این پرسنل هنوز ارزیابی را انجام نداده است.</div>
                </asp:Panel>
            </div>
            
            <!-- ============================================================
            دکمه بازگشت
            ============================================================ -->
            <div class="mt-4">
                <asp:LinkButton ID="btnBack" runat="server" CssClass="btn-back" OnClick="btnBack_Click">
                    🔙 بازگشت به مدیریت ثبت‌نام‌ها
                </asp:LinkButton>
            </div>
            
        </asp:Panel>
        
        <!-- ============================================================
        پیام خطا
        ============================================================ -->
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
            <asp:Label ID="lblError" runat="server" />
        </asp:Panel>
        
    </div>
    
</asp:Content>