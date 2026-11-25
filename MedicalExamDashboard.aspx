
<%@ Page Title="داشبورد معاینات پزشکی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="MedicalExamDashboard.aspx.vb" Inherits="MedicalExamDashboard" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">


<script src="<%= ResolveUrl("~/Scripts/Chart.min.js") %>" type="text/javascript"></script>


    <style type="text/css">

        .med-dashboard {
            direction: rtl;
            font-family: Tahoma;
            padding: 15px;
        }

        .med-title {
            font-size: 20px;
            font-weight: bold;
            color: #245579;
            margin-bottom: 20px;
        }

        .med-toolbar {
            margin-bottom: 15px;
        }

        .med-back-button {
            display: inline-block;
            padding: 9px 20px;
            background: #647987;
            color: white !important;
            text-decoration: none;
            border-radius: 5px;
            font-family: Tahoma;
            font-size: 13px;
        }

        .med-back-button:hover {
            background: #465c6b;
            text-decoration: none;
        }

        .med-search {
            background: #f5f8fb;
            border: 1px solid #dce5ed;
            border-radius: 8px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .med-input {
            width: 260px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-family: Tahoma;
        }

        .med-button {
            padding: 9px 22px;
            background: #2475a8;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-family: Tahoma;
        }

        .med-card {
            border: 1px solid #e0e6ec;
            border-radius: 8px;
            padding: 15px;
            background: white;
            margin-bottom: 15px;
        }

        .med-card-title {
            font-weight: bold;
            color: #245579;
            margin-bottom: 15px;
            border-bottom: 1px solid #e8edf2;
            padding-bottom: 10px;
        }

        .med-info {
            display: inline-block;
            width: 24%;
            margin-bottom: 15px;
            vertical-align: top;
        }

        .med-label {
            display: block;
            color: #777;
            font-size: 12px;
            margin-bottom: 5px;
        }

        .med-value {
            font-weight: bold;
            color: #333;
        }

        .med-grid {
            width: 100%;
            border-collapse: collapse;
            font-family: Tahoma;
            font-size: 12px;
        }

        .med-grid th {
            background: #edf3f8;
            color: #315b79;
            padding: 10px;
            border: 1px solid #dce5ed;
        }

        .med-grid td {
            padding: 9px;
            border: 1px solid #e3e8ed;
            text-align: center;
        }

        .med-grid tr:nth-child(even) {
            background: #f8fafc;
        }

        .med-message {
            display: block;
            margin: 12px 0;
            font-weight: bold;
        }

        .med-charts {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 15px;
        }

        .med-chart-card {
            border: 1px solid #e0e6ec;
            border-radius: 8px;
            padding: 15px;
            background: #fff;
            min-width: 0;
        }

        .med-chart-title {
            font-weight: bold;
            color: #245579;
            margin-bottom: 15px;
            text-align: right;
        }

        .med-chart-container {
            position: relative;
            height: 280px;
            width: 100%;
        }

        .med-recommendation {
            border-right: 4px solid #2475a8;
            background: #f5f8fb;
            padding: 12px;
            margin-bottom: 10px;
            border-radius: 5px;
            line-height: 2;
            overflow-wrap: break-word;
        }

        .med-recommendation-title {
            font-weight: bold;
            color: #245579;
        }

        .med-recommendation-date {
            color: #888;
            font-size: 11px;
            margin-top: 5px;
        }

        @media(max-width:768px) {

            .med-info {
                width: 48%;
            }

            .med-input {
                width: 100%;
                margin-bottom: 10px;
            }

            .med-charts {
                grid-template-columns: 1fr;
            }

        }

    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="med-dashboard">

        <div class="med-toolbar">

            <asp:HyperLink ID="lnkBack" runat="server"
                NavigateUrl="~/index.aspx"
                CssClass="med-back-button">
                &#8592; بازگشت به صفحه اصلی
            </asp:HyperLink>

        </div>


        <div class="med-title">
            پرونده سلامت و نتایج معاینات پزشکی پرسنل
        </div>


        <!-- جستجوی پرسنل -->

        <div class="med-search">

            <asp:Label ID="lblSearch" runat="server"
                Text="جستجوی پرسنل:"
                Font-Bold="true" />

            &nbsp;

            <asp:TextBox ID="txtSearch" runat="server"
                CssClass="med-input"
                placeholder="شماره پرسنلی یا نام و نام خانوادگی" />

            <asp:Button ID="btnSearch" runat="server"
                Text="جستجو"
                CssClass="med-button"
                OnClick="btnSearch_Click" />

            <asp:Label ID="lblMessage" runat="server"
                CssClass="med-message" />

        </div>


        <asp:Panel ID="pnlPersonnel" runat="server" Visible="false">


            <!-- مشخصات پرسنل -->

            <div class="med-card">

                <div class="med-card-title">
                    مشخصات پرسنل
                </div>

                <div class="med-info">
                    <span class="med-label">نام و نام خانوادگی</span>
                    <asp:Label ID="lblFullName" runat="server" CssClass="med-value" />
                </div>

                <div class="med-info">
                    <span class="med-label">شماره پرسنلی</span>
                    <asp:Label ID="lblPersonnelCode" runat="server" CssClass="med-value" />
                </div>

                <div class="med-info">
                    <span class="med-label">واحد سازمانی</span>
                    <asp:Label ID="lblUnit" runat="server" CssClass="med-value" />
                </div>

                <div class="med-info">
                    <span class="med-label">عنوان سمت</span>
                    <asp:Label ID="lblPost" runat="server" CssClass="med-value" />
                </div>

            </div>


            <!-- آخرین شاخص ها -->

            <div class="med-card">

                <div class="med-card-title">
                    آخرین شاخص‌های سلامت
                </div>

                <div class="med-info">
                    <span class="med-label">وزن</span>
                    <asp:Label ID="lblWeight" runat="server" CssClass="med-value" />
                </div>

                <div class="med-info">
                    <span class="med-label">BMI</span>
                    <asp:Label ID="lblBMI" runat="server" CssClass="med-value" />
                </div>

                <div class="med-info">
                    <span class="med-label">فشار خون</span>
                    <asp:Label ID="lblBloodPressure" runat="server" CssClass="med-value" />
                </div>

                <div class="med-info">
                    <span class="med-label">تاریخ آخرین معاینه</span>
                    <asp:Label ID="lblLastExam" runat="server" CssClass="med-value" />
                </div>

            </div>


            <!-- نمودارهای سلامت -->

            <div class="med-card">

                <div class="med-card-title">
                    نمودار روند تغییرات شاخص‌های سلامت
                </div>

                <div class="med-charts">

                    <div class="med-chart-card">

                        <div class="med-chart-title">
                            روند وزن و شاخص توده بدنی
                        </div>

                        <div class="med-chart-container">
                            <canvas id="chartWeightBMI"></canvas>
                        </div>

                    </div>


                    <div class="med-chart-card">

                        <div class="med-chart-title">
                            روند قند خون
                        </div>

                        <div class="med-chart-container">
                            <canvas id="chartGlucose"></canvas>
                        </div>

                    </div>


                    <div class="med-chart-card">

                        <div class="med-chart-title">
                            روند چربی خون
                        </div>

                        <div class="med-chart-container">
                            <canvas id="chartLipids"></canvas>
                        </div>

                    </div>


                    <div class="med-chart-card">

                        <div class="med-chart-title">
                            روند فشار خون
                        </div>

                        <div class="med-chart-container">
                            <canvas id="chartBloodPressure"></canvas>
                        </div>

                    </div>


                    <div class="med-chart-card">

                        <div class="med-chart-title">
                            شاخص‌های کبد و کلیه
                        </div>

                        <div class="med-chart-container">
                            <canvas id="chartLiverKidney"></canvas>
                        </div>

                    </div>


                    <div class="med-chart-card">

                        <div class="med-chart-title">
                            شمارش سلول‌های خونی
                        </div>

                        <div class="med-chart-container">
                            <canvas id="chartCBC"></canvas>
                        </div>

                    </div>

                </div>

            </div>


            <!-- توصیه های پزشکی -->

            <div class="med-card">

                <div class="med-card-title">
                    توصیه‌ها و پیگیری‌های پزشکی
                </div>

                <asp:Repeater ID="rptRecommendations" runat="server">

                    <ItemTemplate>

                        <div class="med-recommendation">

                            <div class="med-recommendation-title">
                                <%# Eval("Title") %>
                            </div>

                            <div>
                                <%# Eval("Body") %>
                            </div>

                            <div class="med-recommendation-date">
                                تاریخ: <%# Eval("Date") %>
                            </div>

                        </div>

                    </ItemTemplate>

                </asp:Repeater>

                <asp:Label ID="lblNoRecommendation" runat="server"
                    Text="توصیه پزشکی برای این پرسنل ثبت نشده است."
                    Visible="false"
                    ForeColor="#888" />

            </div>


            <!-- سوابق آزمایش ها -->

            <div class="med-card">

                <div class="med-card-title">
                    سوابق آزمایش‌ها و معاینات
                </div>

                <div style="overflow-x:auto;">

                    <asp:GridView ID="gvHistory" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="med-grid"
                        EmptyDataText="سابقه‌ای برای این پرسنل ثبت نشده است.">

                        <Columns>

                            <asp:BoundField DataField="VisitDate" HeaderText="تاریخ مراجعه" />
                            <asp:BoundField DataField="ExamType" HeaderText="نوع معاینه" />
                            <asp:BoundField DataField="LabDate" HeaderText="تاریخ آزمایش" />

                            <asp:BoundField DataField="FBS" HeaderText="قند خون" />
                            <asp:BoundField DataField="HbA1C" HeaderText="HbA1C" />

                            <asp:BoundField DataField="Chol" HeaderText="کلسترول" />
                            <asp:BoundField DataField="TG" HeaderText="تری‌گلیسیرید" />
                            <asp:BoundField DataField="HDL" HeaderText="HDL" />
                            <asp:BoundField DataField="LDL" HeaderText="LDL" />

                            <asp:BoundField DataField="BMI" HeaderText="BMI" />

                            <asp:BoundField DataField="Recommendation" HeaderText="توصیه پزشکی" />

                        </Columns>

                    </asp:GridView>

                </div>

            </div>


            <!-- داده های نمودار -->

            <asp:Literal ID="litChartData" runat="server" />


            <!-- رسم نمودارها -->

            <script type="text/javascript">

                (function () {

                    if (typeof Chart === "undefined" || typeof medicalCharts === "undefined") {
                        return;
                    }

                    Chart.defaults.global.defaultFontFamily = "Tahoma";
                    Chart.defaults.global.defaultFontSize = 11;

                    var colors = [
                        "#2475a8",
                        "#e67e22",
                        "#27ae60",
                        "#8e44ad",
                        "#c0392b"
                    ];

                    function drawMedicalChart(canvasId, chartData) {

                        var canvas = document.getElementById(canvasId);

                        if (!canvas || !chartData) {
                            return;
                        }

                        for (var i = 0; i < chartData.datasets.length; i++) {

                            chartData.datasets[i].borderColor = colors[i % colors.length];
                            chartData.datasets[i].backgroundColor = colors[i % colors.length];
                            chartData.datasets[i].borderWidth = 2;
                            chartData.datasets[i].pointRadius = 3;
                            chartData.datasets[i].pointHoverRadius = 5;

                        }

                        new Chart(canvas.getContext("2d"), {

                            type: "line",

                            data: chartData,

                            options: {

                                responsive: true,

                                maintainAspectRatio: false,

                                legend: {
                                    display: true,
                                    position: "bottom",
                                    rtl: true,
                                    labels: {
                                        fontFamily: "Tahoma",
                                        padding: 15
                                    }
                                },

                                tooltips: {
                                    enabled: true,
                                    rtl: true,
                                    titleFontFamily: "Tahoma",
                                    bodyFontFamily: "Tahoma"
                                },

                                hover: {
                                    mode: "nearest",
                                    intersect: true
                                },

                                scales: {

                                    xAxes: [{
                                        display: true,
                                        ticks: {
                                            fontFamily: "Tahoma",
                                            autoSkip: true,
                                            maxRotation: 45,
                                            minRotation: 0
                                        },
                                        scaleLabel: {
                                            display: true,
                                            labelString: "تاریخ",
                                            fontFamily: "Tahoma"
                                        }
                                    }],

                                    yAxes: [{
                                        display: true,
                                        ticks: {
                                            fontFamily: "Tahoma"
                                        },
                                        scaleLabel: {
                                            display: true,
                                            labelString: "مقدار",
                                            fontFamily: "Tahoma"
                                        }
                                    }]

                                }

                            }

                        });

                    }


                    drawMedicalChart("chartWeightBMI", medicalCharts.weight);

                    drawMedicalChart("chartGlucose", medicalCharts.glucose);

                    drawMedicalChart("chartLipids", medicalCharts.lipids);

                    drawMedicalChart("chartBloodPressure", medicalCharts.pressure);

                    drawMedicalChart("chartLiverKidney", medicalCharts.liver);

                    drawMedicalChart("chartCBC", medicalCharts.cbc);

                })();

            </script>

        </asp:Panel>

    </div>

</asp:Content>
