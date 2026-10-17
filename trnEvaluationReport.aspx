<%@ Page Title="گزارش ارزیابی دوره‌های آموزشی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnEvaluationReport.aspx.vb" Inherits="trnEvaluationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .report-container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: white;
            border-radius: 15px;
            box-shadow: 0 2px 20px rgba(0,0,0,0.1);
        }
        .report-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
            padding-bottom: 15px;
            border-bottom: 2px solid #e0e0e0;
        }
        .report-header h2 {
            color: #2c3e50;
            margin: 0;
        }
        .report-header .btn-back {
            background: #9b59b6;
            color: white;
            padding: 10px 25px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        .report-header .btn-back:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
            background: #8e44ad;
        }
        
        .filter-section {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 20px;
        }
        .filter-section table {
            width: 100%;
        }
        .filter-section td {
            padding: 5px 10px;
        }
        .filter-section label {
            font-weight: bold;
            color: #2c3e50;
        }
        .filter-section select, .filter-section input {
            padding: 8px 12px;
            border: 1px solid #ddd;
            border-radius: 8px;
            font-size: 14px;
            width: 100%;
        }
        .filter-section select:focus, .filter-section input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 5px rgba(52,152,219,0.3);
        }
        .filter-section .btn-filter {
            background: #3498db;
            color: white;
            padding: 8px 30px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s;
        }
        .filter-section .btn-filter:hover {
            background: #2980b9;
            transform: scale(1.05);
        }
        .filter-section .btn-reset {
            background: #95a5a6;
            color: white;
            padding: 8px 30px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s;
            margin-right: 5px;
        }
        .filter-section .btn-reset:hover {
            background: #7f8c8d;
            transform: scale(1.05);
        }
        
        .report-grid {
            overflow-x: auto;
        }
        .report-grid table {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
            text-align: right;
        }
        .report-grid th {
            background: #2c3e50;
            color: white;
            padding: 12px 10px;
            border: 1px solid #2c3e50;
            white-space: nowrap;
        }
        .report-grid td {
            padding: 10px;
            border: 1px solid #ddd;
            vertical-align: middle;
        }
        .report-grid tr:nth-child(even) {
            background: #f8f9fa;
        }
        .report-grid tr:hover {
            background: #e8f4fd;
        }
        .report-grid .text-center {
            text-align: center;
        }
        .report-grid .score-high {
            color: #27ae60;
            font-weight: bold;
        }
        .report-grid .score-medium {
            color: #f39c12;
            font-weight: bold;
        }
        .report-grid .score-low {
            color: #e74c3c;
            font-weight: bold;
        }
        
        .summary-box {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            margin: 20px 0;
        }
        .summary-item {
            flex: 1;
            min-width: 150px;
            background: #f8f9fa;
            padding: 15px;
            border-radius: 10px;
            text-align: center;
            border-right: 4px solid #3498db;
        }
        .summary-item .number {
            font-size: 24px;
            font-weight: bold;
            color: #2c3e50;
        }
        .summary-item .label {
            font-size: 12px;
            color: #888;
            margin-top: 5px;
        }
        .summary-item.blue { border-right-color: #3498db; }
        .summary-item.green { border-right-color: #27ae60; }
        .summary-item.orange { border-right-color: #f39c12; }
        .summary-item.red { border-right-color: #e74c3c; }
        .summary-item.purple { border-right-color: #9b59b6; }
        
        .status-badge {
            display: inline-block;
            padding: 3px 10px;
            border-radius: 15px;
            font-size: 12px;
            font-weight: bold;
        }
        .status-badge.high { background: #d4edda; color: #155724; }
        .status-badge.medium { background: #fff3cd; color: #856404; }
        .status-badge.low { background: #f8d7da; color: #721c24; }
        
        .chart-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            margin: 20px 0;
        }
        .chart-box {
            flex: 1;
            min-width: 300px;
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
        }
        .chart-box h4 {
            margin: 0 0 15px 0;
            color: #2c3e50;
            text-align: center;
        }
        .bar-chart {
            display: flex;
            align-items: flex-end;
            height: 200px;
            gap: 10px;
            justify-content: center;
            padding-top: 10px;
        }
        .bar-item {
            display: flex;
            flex-direction: column;
            align-items: center;
            flex: 1;
        }
        .bar-item .bar {
            width: 30px;
            background: #3498db;
            border-radius: 5px 5px 0 0;
            transition: height 0.5s;
            min-height: 5px;
        }
        .bar-item .bar-label {
            margin-top: 5px;
            font-size: 12px;
            color: #888;
        }
        .bar-item .bar-value {
            font-size: 12px;
            font-weight: bold;
            margin-bottom: 3px;
        }
        
        .pagination {
            display: flex;
            justify-content: center;
            gap: 5px;
            margin-top: 20px;
        }
        .pagination a, .pagination span {
            padding: 8px 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            text-decoration: none;
            color: #2c3e50;
            font-size: 14px;
            transition: all 0.3s;
        }
        .pagination a:hover {
            background: #3498db;
            color: white;
            border-color: #3498db;
        }
        .pagination .active {
            background: #3498db;
            color: white;
            border-color: #3498db;
        }
        
        .no-data {
            text-align: center;
            padding: 50px 20px;
            color: #888;
            font-size: 16px;
        }
        .no-data .icon {
            font-size: 48px;
            margin-bottom: 15px;
        }
        
        .tooltip {
            position: relative;
            cursor: pointer;
        }
        .tooltip .tooltip-text {
            visibility: hidden;
            width: 200px;
            background: #2c3e50;
            color: white;
            text-align: right;
            padding: 10px;
            border-radius: 8px;
            position: absolute;
            z-index: 1;
            bottom: 125%;
            left: 50%;
            transform: translateX(-50%);
            opacity: 0;
            transition: opacity 0.3s;
            font-size: 12px;
            font-weight: normal;
        }
        .tooltip:hover .tooltip-text {
            visibility: visible;
            opacity: 1;
        }
        
        @media print {
            .filter-section, .btn-back, .pagination {
                display: none !important;
            }
            .report-container {
                box-shadow: none;
                margin: 0;
                padding: 10px;
            }
        }
        
        @media (max-width: 768px) {
            .report-header {
                flex-direction: column;
                gap: 10px;
            }
            .filter-section td {
                display: block;
                width: 100%;
            }
            .summary-item {
                min-width: 100px;
            }
            .report-grid table {
                font-size: 12px;
            }
            .report-grid th, .report-grid td {
                padding: 6px 5px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="report-container">
        <!-- هدر گزارش -->
        <div class="report-header">
            <h2>📊 گزارش ارزیابی دوره‌های آموزشی</h2>
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت به صفحه اصلی" 
                CssClass="btn-back" OnClick="btnBack_Click" />
        </div>

        <!-- بخش فیلتر -->
        <div class="filter-section">
            <table>
                <tr>
                    <td style="width: 20%;">
                        <label>عنوان دوره:</label>
                        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="همه دوره‌ها" Value="0" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%;">
                        <label>مدرس:</label>
                        <asp:DropDownList ID="ddlTeacher" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="همه مدرسین" Value="0" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%;">
                        <label>از تاریخ:</label>
                        <asp:TextBox ID="txtDateFrom" runat="server" TextMode="Date" />
                    </td>
                    <td style="width: 20%;">
                        <label>تا تاریخ:</label>
                        <asp:TextBox ID="txtDateTo" runat="server" TextMode="Date" />
                    </td>
                    <td style="width: 20%;">
                        <label>&nbsp;</label>
                        <div>
                            <asp:Button ID="btnFilter" runat="server" Text="🔍 اعمال فیلتر" 
                                CssClass="btn-filter" OnClick="btnFilter_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="🔄重置" 
                                CssClass="btn-reset" OnClick="btnReset_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <!-- خلاصه آماری -->
        <div class="summary-box" id="divSummary" runat="server">
            <div class="summary-item blue">
                <div class="number" id="lblTotalEvaluations" runat="server">0</div>
                <div class="label">تعداد کل ارزیابی‌ها</div>
            </div>
            <div class="summary-item green">
                <div class="number" id="lblAverageScore" runat="server">0</div>
                <div class="label">میانگین امتیاز</div>
            </div>
            <div class="summary-item orange">
                <div class="number" id="lblHighScore" runat="server">0</div>
                <div class="label">ارزیابی‌های عالی (≥ 80)</div>
            </div>
            <div class="summary-item red">
                <div class="number" id="lblLowScore" runat="server">0</div>
                <div class="label">ارزیابی‌های ضعیف (&lt; 40)</div>
            </div>
            <div class="summary-item purple">
                <div class="number" id="lblTotalCourses" runat="server">0</div>
                <div class="label">تعداد دوره‌های ارزیابی شده</div>
            </div>
        </div>

        <!-- نمودارها -->
        <div class="chart-container" id="divCharts" runat="server" visible="false">
            <div class="chart-box">
                <h4>📊 توزیع امتیازات</h4>
                <div class="bar-chart" id="barChart" runat="server">
                    <!-- بارهای نمودار توسط کد پشت صحنه تولید می‌شود -->
                </div>
            </div>
        </div>

        <!-- جدول گزارش -->
        <div class="report-grid">
            <asp:GridView ID="gvEvaluations" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" PageSize="20" CssClass="table"
                OnPageIndexChanging="gvEvaluations_PageIndexChanging"
                EmptyDataText="هیچ ارزیابی ثبت نشده است.">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="50" />
                    <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
                    <asp:BoundField DataField="PersonName" HeaderText="نام پرسنل" />
                    <asp:BoundField DataField="CourseName" HeaderText="عنوان دوره" />
                    <asp:BoundField DataField="TeacherName" HeaderText="مدرس" />
                    <asp:BoundField DataField="TotalScore" HeaderText="امتیاز کل" 
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="وضعیت">
                        <ItemTemplate>
                            <span class='status-badge <%# GetStatusClass(Eval("TotalScore")) %>'>
                                <%# GetStatusText(Eval("TotalScore")) %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="جزئیات">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDetails" runat="server" Text="🔍 مشاهده" 
                                CommandArgument='<%# Eval("ID") %>' 
                                OnClick="lnkDetails_Click" CssClass="btn-details" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="تاریخ ارزیابی">
    <ItemTemplate>
        <%# FormatPersianDate(Eval("CreateDate")) %>
    </ItemTemplate>
</asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <!-- Modal برای نمایش جزئیات -->
    <div id="modalDetails" class="modal" style="display:none; position:fixed; z-index:1000; left:0; top:0; width:100%; height:100%; overflow:auto; background:rgba(0,0,0,0.5);">
        <div style="background:white; margin:5% auto; padding:30px; border-radius:15px; max-width:700px; position:relative; direction:rtl;">
            <span onclick="closeModal()" style="position:absolute; left:20px; top:15px; font-size:28px; font-weight:bold; cursor:pointer; color:#aaa;">&times;</span>
            <h3 style="color:#2c3e50; margin-bottom:20px;">📋 جزئیات ارزیابی</h3>
            <div id="modalContent" runat="server">
                <!-- محتوا توسط کد پشت صحنه پر می‌شود -->
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal() {
            document.getElementById('modalDetails').style.display = 'block';
        }

        function closeModal() {
            document.getElementById('modalDetails').style.display = 'none';
        }

        // بستن مودال با کلیک خارج از آن
        window.onclick = function (event) {
            var modal = document.getElementById('modalDetails');
            if (event.target == modal) {
                modal.style.display = 'none';
            }
        }
    </script>
</asp:Content>