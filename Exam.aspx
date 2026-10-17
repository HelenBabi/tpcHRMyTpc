<%@ Page Title="آزمون دوره آموزشی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="true" CodeFile="Exam.aspx.vb" Inherits="Exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .exam-container {
            max-width: 900px;
            margin: 0 auto;
            padding: 20px;
        }
        
        .exam-header {
            background: linear-gradient(135deg, #dc3545, #c82333);
            color: white;
            padding: 30px;
            border-radius: 15px 15px 0 0;
            text-align: center;
        }
        
        .exam-header h2 {
            margin: 0;
            font-size: 24px;
        }
        
        .exam-header p {
            margin: 10px 0 0;
            opacity: 0.9;
        }
        
        .exam-body {
            background: #fff;
            padding: 30px;
            border-radius: 0 0 15px 15px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }
        
        .exam-info {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 30px;
            border-right: 4px solid #dc3545;
        }
        
        .exam-info .info-row {
            display: flex;
            margin: 5px 0;
            padding: 5px 0;
            border-bottom: 1px dashed #e0e0e0;
        }
        
        .exam-info .info-row:last-child {
            border-bottom: none;
        }
        
        .exam-info .info-label {
            font-weight: bold;
            width: 120px;
            color: #555;
        }
        
        .exam-info .info-value {
            flex: 1;
            color: #333;
        }
        
        .timer-container {
            background: #fff3cd;
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            border: 2px solid #ffc107;
            text-align: center;
        }
        
        .timer-container .timer {
            font-size: 32px;
            font-weight: bold;
            color: #dc3545;
            font-family: monospace;
        }
        
        .timer-container .timer-label {
            font-size: 14px;
            color: #666;
        }
        
        .question-container {
            background: #fff;
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            transition: all 0.3s;
        }
        
        .question-container:hover {
            border-color: #1e3c72;
        }
        
        .question-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;
        }
        
        .question-number {
            font-weight: bold;
            color: #1e3c72;
            font-size: 16px;
        }
        
        .question-score {
            background: #1e3c72;
            color: white;
            padding: 3px 12px;
            border-radius: 20px;
            font-size: 12px;
        }
        
        .question-text {
            font-size: 16px;
            margin-bottom: 15px;
            line-height: 1.6;
        }
        
        /* ============================================================
        استایل گزینه‌ها با استفاده از کلاس‌های بوت‌استرپ
        ============================================================ */
        .options-container .form-check {
            padding: 8px 15px;
            margin-bottom: 5px;
            border-radius: 8px;
            border: 2px solid #e0e0e0;
            transition: all 0.3s;
            background: #fafafa;
            cursor: pointer;
        }
        
        .options-container .form-check:hover {
            background: #e8f4f8;
            border-color: #1e3c72;
        }
        
        .options-container .form-check.selected {
            background: #d4edda;
            border-color: #28a745;
        }
        
        .options-container .form-check.wrong {
            background: #f8d7da;
            border-color: #dc3545;
        }
        
        .options-container .form-check-input {
            margin-top: 0.1rem;
            margin-left: 10px;
            accent-color: #1e3c72;
            width: 18px;
            height: 18px;
            float: right;
        }
        
        .options-container .form-check-label {
            font-size: 15px;
            cursor: pointer;
            display: inline-block;
            padding-right: 5px;
        }
        
        .exam-actions {
            margin-top: 30px;
            padding-top: 20px;
            border-top: 2px solid #eee;
            display: flex;
            gap: 15px;
            justify-content: center;
            flex-wrap: wrap;
        }
        
        .btn-submit-exam {
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
        
        .btn-submit-exam:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(40,167,69,0.4);
        }
        
        .btn-submit-exam:disabled {
            opacity: 0.6;
            cursor: not-allowed;
            transform: none;
        }
        
        .btn-cancel-exam {
            background: #6c757d;
            color: white;
            border: none;
            padding: 12px 30px;
            border-radius: 10px;
            font-size: 16px;
            cursor: pointer;
            transition: all 0.3s;
        }
        
        .btn-cancel-exam:hover {
            background: #5a6268;
        }
        
        .alert {
            padding: 15px 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            display: none;
        }
        
        .alert-success { background: #d4edda; color: #155724; border: 2px solid #28a745; }
        .alert-danger { background: #f8d7da; color: #721c24; border: 2px solid #dc3545; }
        .alert-warning { background: #fff3cd; color: #856404; border: 2px solid #ffc107; }
        .alert-info { background: #d1ecf1; color: #0c5460; border: 2px solid #17a2b8; }
        
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
        
        .btn-close:hover { opacity: 1; }
        
        .result-box {
            text-align: center;
            padding: 40px 20px;
        }
        
        .result-box .score {
            font-size: 60px;
            font-weight: bold;
        }
        
        .result-box .score.passed { color: #28a745; }
        .result-box .score.failed { color: #dc3545; }
        
        .result-box .result-detail {
            font-size: 16px;
            margin: 10px 0;
        }
        
        .row {
            display: flex;
            flex-wrap: wrap;
            margin: 0 -10px;
        }
        
        .col-md-6 { width: 50%; padding: 0 10px; }
        .col-md-12 { width: 100%; padding: 0 10px; }
        
        .text-end { text-align: left; }
        .text-center { text-align: center; }
        .mt-3 { margin-top: 15px; }
        .mb-3 { margin-bottom: 15px; }
        .mb-4 { margin-bottom: 20px; }
        
        @media (max-width: 768px) {
            .col-md-6 { width: 100%; }
            .exam-body { padding: 15px; }
            .timer-container .timer { font-size: 24px; }
            .options-container .form-check {
                padding: 10px 12px;
                font-size: 14px;
            }
            .options-container .form-check-label {
                font-size: 14px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="exam-container">
        
        <!-- هدر -->
        <div class="exam-header">
            <h2>📝 آزمون دوره آموزشی</h2>
            <p><asp:Label ID="lblCourseTitle" runat="server" Text="-" /></p>
        </div>
        
        <!-- بدنه -->
        <div class="exam-body">
            
            <!-- پیام‌ها -->
            <asp:Panel ID="pnlAlert" runat="server" CssClass="alert" role="alert" style="display:none;">
                <button type="button" class="btn-close" onclick="this.parentElement.style.display='none'">&times;</button>
                <asp:Label ID="lblAlertMessage" runat="server" />
            </asp:Panel>
            
            <!-- اطلاعات آزمون -->
            <div class="exam-info">
                <div class="info-row">
                    <div class="info-label">عنوان دوره:</div>
                    <div class="info-value"><asp:Label ID="lblCourseTitleInfo" runat="server" Text="-" /></div>
                </div>
                <div class="info-row">
                    <div class="info-label">پرسنل:</div>
                    <div class="info-value"><asp:Label ID="lblPersonnelName" runat="server" Text="-" /></div>
                </div>
                <div class="info-row">
                    <div class="info-label">تعداد سوالات:</div>
                    <div class="info-value"><asp:Label ID="lblTotalQuestions" runat="server" Text="0" /></div>
                </div>
                <div class="info-row">
                    <div class="info-label">نمره قبولی:</div>
                    <div class="info-value"><asp:Label ID="lblMinPassScore" runat="server" Text="60" /> از 100</div>
                </div>
            </div>
            
            <!-- تایمر -->
            <div class="timer-container" id="timerContainer" runat="server">
                <div class="timer-label">⏱️ زمان باقی مانده</div>
                <div class="timer" id="timerDisplay">00:00</div>
            </div>
            
            <!-- سوالات آزمون -->
            <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
                <ItemTemplate>
                    <div class="question-container">
                        <div class="question-header">
                            <span class="question-number">سوال <%# Container.ItemIndex + 1 %></span>
                            <span class="question-score">نمره: <%# Eval("Score") %></span>
                        </div>
                        <div class="question-text">
                            <%# Eval("QuestionText") %>
                        </div>
                        <div class="options-container">
                            <asp:Repeater ID="rptOptions" runat="server">
                                <ItemTemplate>
                                    <div class="form-check">
                                        <asp:RadioButton ID="rbOption" runat="server" 
                                            CssClass="form-check-input"
                                            GroupName='<%# "q" & GetGroupName(Container) %>'
                                            onchange="checkAnswer(this)" />
                                        <asp:HiddenField ID="hfOptionID" runat="server" Value='<%# Eval("OptionID") %>' />
                                        <label class="form-check-label" for="rbOption">
                                            <%# Eval("OptionText") %>
                                        </label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <!-- دکمه‌ها -->
            <div class="exam-actions">
                <asp:Button ID="btnSubmitExam" runat="server" Text="✅ ارسال پاسخ‌ها" 
                    CssClass="btn-submit-exam" OnClick="btnSubmitExam_Click" 
                    OnClientClick="return confirm('آیا از ارسال پاسخ‌ها اطمینان دارید؟');" />
                <asp:Button ID="btnCancelExam" runat="server" Text="❌ انصراف" 
                    CssClass="btn-cancel-exam" OnClick="btnCancelExam_Click" />
            </div>
            
            <!-- نتیجه آزمون -->
            <asp:Panel ID="pnlResult" runat="server" Visible="false" CssClass="result-box">
                <div class="row">
                    <div class="col-md-12">
                        <div class="score">
                            <asp:Label ID="lblResultScore" runat="server" />
                        </div>
                        <div class="result-detail">
                            <asp:Label ID="lblResultMessage" runat="server" />
                        </div>
                        <div class="result-detail">
                            تعداد پاسخ‌های صحیح: <asp:Label ID="lblCorrectCount" runat="server" />
                            از <asp:Label ID="lblTotalCount" runat="server" />
                        </div>
                        <div class="mt-3">
                            <asp:LinkButton ID="btnBackToCourses" runat="server" 
                                CssClass="btn btn-primary" PostBackUrl="~/MyCourses.aspx">
                                🔙 بازگشت به دوره‌های من
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            
            <!-- اطلاعات مخفی -->
            <asp:HiddenField ID="hfRegistrationID" runat="server" Value="0" />
            <asp:HiddenField ID="hfCourseID" runat="server" Value="0" />
            <asp:HiddenField ID="hfExamID" runat="server" Value="0" />
            <asp:HiddenField ID="hfTimeRemaining" runat="server" Value="0" />
            
        </div>
    </div>
    
    <script type="text/javascript">
        // ============================================================
        // تابع GetGroupName برای جاوااسکریپت (برای رفع خطای ItemIndex)
        // ============================================================
        function getGroupName(container) {
            return container.id;
        }

        // ============================================================
        // تایمر آزمون
        // ============================================================
        var totalSeconds = parseInt(document.getElementById('<%= hfTimeRemaining.ClientID %>').value);
        var timerInterval = null;

        function startTimer() {
            if (totalSeconds <= 0) {
                document.getElementById('timerDisplay').innerHTML = '00:00';
                return;
            }

            timerInterval = setInterval(function () {
                totalSeconds--;
                updateTimerDisplay();

                if (totalSeconds <= 0) {
                    clearInterval(timerInterval);
                    document.getElementById('timerDisplay').innerHTML = '⏰ زمان تمام شد!';
                    document.getElementById('<%= btnSubmitExam.ClientID %>').click();
                }
            }, 1000);
        }

        function updateTimerDisplay() {
            var minutes = Math.floor(totalSeconds / 60);
            var seconds = totalSeconds % 60;
            document.getElementById('timerDisplay').innerHTML =
                String(minutes).padStart(2, '0') + ':' + String(seconds).padStart(2, '0');
        }

        // ============================================================
        // برجسته کردن گزینه انتخاب شده
        // ============================================================
        function checkAnswer(radio) {
            var div = radio.closest('.form-check');
            var siblings = div.closest('.options-container').querySelectorAll('.form-check');
            siblings.forEach(function (sib) {
                sib.classList.remove('selected');
            });
            if (radio.checked) {
                div.classList.add('selected');
            }
        }

        // ============================================================
        // اجرا بعد از بارگذاری صفحه
        // ============================================================
        document.addEventListener('DOMContentLoaded', function () {
            startTimer();
        });

        // برای جلوگیری از خروج از صفحه با دکمه بازگشت
        window.addEventListener('beforeunload', function (e) {
            if (document.getElementById('<%= btnSubmitExam.ClientID %>').disabled == false) {
                e.preventDefault();
                e.returnValue = 'آیا از خروج از صفحه اطمینان دارید؟ پاسخ‌های شما ذخیره نخواهد شد.';
            }
        });
    </script>
    
</asp:Content>