<%@ Page Title="ارزیابی دوره آموزشی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="Evaluation.aspx.vb" Inherits="Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .evaluation-container {
            max-width: 900px;
            margin: 0 auto;
            padding: 20px;
        }
        
        .evaluation-header {
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            color: white;
            padding: 30px;
            border-radius: 15px 15px 0 0;
            text-align: center;
        }
        
        .evaluation-header h2 {
            margin: 0;
            font-size: 24px;
        }
        
        .evaluation-header p {
            margin: 10px 0 0;
            opacity: 0.9;
        }
        
        .evaluation-body {
            background: #fff;
            padding: 30px;
            border-radius: 0 0 15px 15px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }
        
        .course-info {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 30px;
            border-right: 4px solid #1e3c72;
        }
        
        .course-info .info-row {
            display: flex;
            margin: 5px 0;
            padding: 5px 0;
            border-bottom: 1px dashed #e0e0e0;
        }
        
        .course-info .info-row:last-child {
            border-bottom: none;
        }
        
        .course-info .info-label {
            font-weight: bold;
            width: 120px;
            color: #555;
        }
        
        .course-info .info-value {
            flex: 1;
            color: #333;
        }
        
        .question-section {
            margin-bottom: 30px;
            padding-bottom: 20px;
            border-bottom: 1px solid #eee;
        }
        
        .question-section:last-child {
            border-bottom: none;
            margin-bottom: 0;
        }
        
        .question-title {
            font-weight: bold;
            color: #1e3c72;
            font-size: 16px;
            margin-bottom: 5px;
        }
        
        .question-title .required {
            color: red;
            margin-right: 5px;
        }
        
        .question-description {
            color: #666;
            font-size: 13px;
            margin-bottom: 10px;
            padding-right: 20px;
        }
        
        .options-group {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            padding: 10px 20px;
        }
        
        .options-group label {
            display: flex;
            align-items: center;
            gap: 8px;
            cursor: pointer;
            padding: 8px 18px;
            border-radius: 8px;
            transition: all 0.3s;
            border: 2px solid #e0e0e0;
            background: #fafafa;
            font-size: 14px;
        }
        
        .options-group label:hover {
            background: #e8f4f8;
            border-color: #1e3c72;
            transform: translateY(-2px);
        }
        
        .options-group label.selected {
            background: #d4edda;
            border-color: #28a745;
        }
        
        .options-group input[type="radio"] {
            accent-color: #1e3c72;
            width: 18px;
            height: 18px;
            margin: 0;
        }
        
        .evaluation-actions {
            margin-top: 30px;
            padding-top: 20px;
            border-top: 2px solid #eee;
            display: flex;
            gap: 15px;
            justify-content: center;
        }
        
        .btn-submit {
            background: linear-gradient(135deg, #28a745, #20c997);
            color: white;
            border: none;
            padding: 12px 40px;
            border-radius: 10px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s;
        }
        
        .btn-submit:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(40, 167, 69, 0.4);
        }
        
        .btn-submit:disabled {
            opacity: 0.6;
            cursor: not-allowed;
            transform: none;
        }
        
        .btn-cancel {
            background: #6c757d;
            color: white;
            border: none;
            padding: 12px 30px;
            border-radius: 10px;
            font-size: 16px;
            cursor: pointer;
            transition: all 0.3s;
        }
        
        .btn-cancel:hover {
            background: #5a6268;
        }
        
        .alert {
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            display: none;
        }
        
        .alert-success {
            background: #d4edda;
            color: #155724;
            border: 2px solid #28a745;
        }
        
        .alert-danger {
            background: #f8d7da;
            color: #721c24;
            border: 2px solid #dc3545;
        }
        
        .alert-warning {
            background: #fff3cd;
            color: #856404;
            border: 2px solid #ffc107;
        }
        
        .alert-info {
            background: #d1ecf1;
            color: #0c5460;
            border: 2px solid #17a2b8;
        }
        
        .btn-close {
            float: left;
            background: none;
            border: none;
            font-size: 24px;
            cursor: pointer;
            color: #000;
            opacity: 0.5;
            padding: 0 10px;
        }
        
        .btn-close:hover {
            opacity: 1;
        }
        
        .progress-bar {
            width: 100%;
            height: 10px;
            background: #e9ecef;
            border-radius: 5px;
            margin: 20px 0;
            overflow: hidden;
        }
        
        .progress-bar .progress {
            height: 100%;
            background: linear-gradient(90deg, #28a745, #20c997);
            border-radius: 5px;
            transition: width 0.5s ease;
            width: 0%;
        }
        
        .progress-text {
            text-align: center;
            font-size: 13px;
            color: #666;
            margin-top: -10px;
            margin-bottom: 10px;
        }
        
        .form-group {
            margin-bottom: 20px;
        }
        
        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }
        
        .form-control {
            width: 100%;
            padding: 10px;
            border-radius: 8px;
            border: 2px solid #ddd;
            transition: border-color 0.3s;
            font-family: inherit;
            font-size: 14px;
        }
        
        .form-control:focus {
            border-color: #1e3c72;
            outline: none;
        }
        
        .form-control:disabled {
            background: #f5f5f5;
            cursor: not-allowed;
        }
        
        .badge {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
        }
        
        .badge-success {
            background: #d4edda;
            color: #155724;
        }
        
        .badge-warning {
            background: #fff3cd;
            color: #856404;
        }
        
        .badge-danger {
            background: #f8d7da;
            color: #721c24;
        }
        
        .mt-3 {
            margin-top: 15px;
        }
        
        .mb-3 {
            margin-bottom: 15px;
        }
        
        @media (max-width: 768px) {
            .evaluation-container {
                padding: 10px;
            }
            
            .options-group {
                flex-direction: column;
                padding: 10px 0;
            }
            
            .options-group label {
                width: 100%;
                justify-content: center;
            }
            
            .evaluation-actions {
                flex-direction: column;
            }
            
            .evaluation-actions button {
                width: 100%;
            }
            
            .course-info .info-row {
                flex-direction: column;
            }
            
            .course-info .info-label {
                width: auto;
            }
            
            .evaluation-body {
                padding: 15px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="evaluation-container">
        
        <!-- هدر -->
        <div class="evaluation-header">
            <h2>📝 فرم ارزیابی دوره آموزشی</h2>
            <p>لطفاً با دقت به سوالات پاسخ دهید</p>
        </div>
        
        <!-- بدنه -->
        <div class="evaluation-body">
            
            <!-- پیام‌ها -->
            <asp:Panel ID="pnlAlert" runat="server" CssClass="alert" role="alert" style="display:none;">
                <button type="button" class="btn-close" onclick="this.parentElement.style.display='none'">&times;</button>
                <asp:Label ID="lblAlertMessage" runat="server" />
            </asp:Panel>
            
            <!-- اطلاعات دوره -->
            <div class="course-info">
                <div class="info-row">
                    <div class="info-label">عنوان دوره:</div>
                    <div class="info-value"><asp:Label ID="lblCourseTitle" runat="server" Text="-" /></div>
                </div>
                <div class="info-row" id="divInstructor" runat="server">
                    <div class="info-label">مدرس:</div>
                    <div class="info-value"><asp:Label ID="lblInstructor" runat="server" Text="-" /></div>
                </div>
                <div class="info-row">
                    <div class="info-label">تاریخ برگزاری:</div>
                    <div class="info-value"><asp:Label ID="lblCourseDate" runat="server" Text="-" /></div>
                </div>
                <div class="info-row">
                    <div class="info-label">پرسنل:</div>
                    <div class="info-value"><asp:Label ID="lblPersonnelName" runat="server" Text="-" /></div>
                </div>
            </div>
            
            <!-- پیشرفت -->
            <div class="progress-bar">
                <div class="progress" id="progressBar"></div>
            </div>
            <div class="progress-text" id="progressText">۰ از ۰ پاسخ داده شده</div>
            
            <!-- سوالات -->
            <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
                <ItemTemplate>
                    <div class="question-section" data-questionid='<%# Eval("QuestionID") %>'>
                        <div class="question-title">
                            سوال <%# Container.ItemIndex + 1 %>: 
                            <%# Eval("QuestionText") %>
                            <span class="required">*</span>
                        </div>
                        <div class="question-description">
                            <%# Eval("Description") %>
                        </div>
                        
                        <div class="options-group">
                       <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
    <ItemTemplate>
        <label>
            <asp:RadioButton ID="rbOption" runat="server" 
                GroupName='<%# "q" & GetGroupName(Container) %>'
                onchange="updateProgress()" />
            <span id="lblOption" runat="server"><%# Eval("Text") %></span>
        </label>
    </ItemTemplate>
</asp:Repeater>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <!-- نظرات تکمیلی -->
            <div class="form-group">
                <label>نظرات تکمیلی شما:</label>
                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" 
                    TextMode="MultiLine" Rows="4" 
                    placeholder="لطفاً نظرات، پیشنهادات یا انتقادات خود را در اینجا بنویسید..." />
            </div>
            
            <!-- دکمه‌ها -->
            <div class="evaluation-actions">
                <asp:Button ID="btnSubmit" runat="server" Text="✅ ارسال ارزیابی" 
                    CssClass="btn-submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف" 
                    CssClass="btn-cancel" OnClick="btnCancel_Click" />
                <!-- برای تست - بعد از تست حذف کنید -->
<div class="mt-3">
    <asp:Button ID="btnTest" runat="server" Text="🧪 تست پیام" 
        CssClass="btn btn-info" OnClick="btnTest_Click" />
</div>
            </div>
            
            <!-- اطلاعات مخفی -->
            <asp:HiddenField ID="hfRegistrationID" runat="server" Value="0" />
            <asp:HiddenField ID="hfCourseID" runat="server" Value="0" />
            <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
            
        </div>
    </div>
    
    <script type="text/javascript">
        function updateProgress() {
            var total = document.querySelectorAll('.question-section').length;
            var answered = 0;

            document.querySelectorAll('.question-section').forEach(function (section) {
                var radios = section.querySelectorAll('input[type="radio"]');
                var checked = false;
                radios.forEach(function (radio) {
                    if (radio.checked) checked = true;
                });
                if (checked) answered++;
            });

            var progress = total > 0 ? (answered / total) * 100 : 0;
            document.getElementById('progressBar').style.width = progress + '%';
            document.getElementById('progressText').innerHTML = answered + ' از ' + total + ' پاسخ داده شده';
        }

        // برجسته کردن گزینه انتخاب شده
        document.addEventListener('DOMContentLoaded', function () {
            document.querySelectorAll('.options-group input[type="radio"]').forEach(function (radio) {
                radio.addEventListener('change', function () {
                    var label = this.closest('label');
                    var siblings = label.closest('.options-group').querySelectorAll('label');
                    siblings.forEach(function (sib) {
                        sib.classList.remove('selected');
                    });
                    if (this.checked) {
                        label.classList.add('selected');
                    }
                });
            });
        });

        // بررسی کامل بودن فرم قبل از ارسال
        function validateForm() {
            var total = document.querySelectorAll('.question-section').length;
            var answered = 0;

            document.querySelectorAll('.question-section').forEach(function (section) {
                var radios = section.querySelectorAll('input[type="radio"]');
                var checked = false;
                radios.forEach(function (radio) {
                    if (radio.checked) checked = true;
                });
                if (checked) answered++;
            });

            if (answered < total) {
                alert('⚠️ لطفاً به همه سوالات پاسخ دهید.\n(' + answered + ' از ' + total + ')');
                return false;
            }
            return confirm('✅ آیا از ارسال ارزیابی خود اطمینان دارید؟');
        }

        // اتصال به دکمه ارسال
        document.addEventListener('DOMContentLoaded', function () {
            var submitBtn = document.getElementById('<%= btnSubmit.ClientID %>');
            if (submitBtn) {
                submitBtn.addEventListener('click', function (e) {
                    if (!validateForm()) {
                        e.preventDefault();
                    }
                });
            }

            // به‌روزرسانی اولیه
            updateProgress();
        });
    </script>
    
</asp:Content>