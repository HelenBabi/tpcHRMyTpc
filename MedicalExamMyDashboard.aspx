
<%@ Page Title="پرونده سلامت من" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="MedicalExamMyDashboard.aspx.vb" Inherits="MedicalExamMyDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="<%= ResolveUrl("~/Scripts/Chart.min.js") %>" type="text/javascript"></script>

    <style type="text/css">
        .my-medical {
            direction: rtl;
            font-family: Tahoma;
            padding: 20px;
            color: #334155;
        }

        .my-medical-title {
            font-size: 22px;
            font-weight: bold;
            color: #176B87;
            margin-bottom: 20px;
        }

        .my-card {
            background: #fff;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            padding: 18px;
            margin-bottom: 18px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }

        .my-card-title {
            font-size: 16px;
            font-weight: bold;
            color: #176B87;
            margin-bottom: 18px;
        }

        .my-info {
            display: inline-block;
            width: 23%;
            margin-bottom: 15px;
            vertical-align: top;
        }

        .my-label {
            color: #777;
            font-size: 12px;
            display: block;
            margin-bottom: 7px;
        }

        .my-value {
            font-size: 14px;
            font-weight: bold;
            color: #333;
        }

        .my-grid {
            width: 100%;
            border-collapse: collapse;
            min-width: 950px;
            font-size: 12px;
        }

        .my-grid th {
            background: #176B87;
            color: #fff;
            padding: 10px;
            text-align: center;
            white-space: nowrap;
        }

        .my-grid td {
            padding: 9px;
            border: 1px solid #e5e7eb;
            text-align: center;
        }

        .my-grid tr:nth-child(even) {
            background: #f5f9fa;
        }

        .my-button {
            background: #64748b;
            color: #fff;
            border: none;
            border-radius: 6px;
            padding: 10px 22px;
            cursor: pointer;
            font-family: Tahoma;
            margin-bottom: 15px;
        }

        .my-button:hover {
            background: #475569;
        }

        .my-message {
            color: #b91c1c;
            font-weight: bold;
            display: block;
            padding: 12px 0;
        }

        .my-chart-grid {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 18px;
        }

        .my-chart-box {
            min-width: 0;
            height: 300px;
            position: relative;
        }

        .my-recommendation {
            border-right: 4px solid #176B87;
            background: #f5f9fa;
            border-radius: 5px;
            padding: 13px;
            margin-bottom: 12px;
        }

        .my-recommendation-title {
            font-weight: bold;
            color: #176B87;
            margin-bottom: 8px;
        }

        .my-recommendation-date {
            color: #888;
            font-size: 11px;
            margin-top: 8px;
        }

        .my-empty {
            color: #777;
            padding: 15px;
            text-align: center;
        }

        @media screen and (max-width: 768px) {
            .my-medical {
                padding: 10px;
            }

            .my-info {
                width: 48%;
            }

            .my-chart-grid {
                grid-template-columns: 1fr;
            }

            .my-chart-box {
                height: 260px;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="my-medical">

        <asp:Button ID="btnBack" runat="server"
            Text="بازگشت به صفحه اصلی"
            CssClass="my-button"
            OnClick="btnBack_Click" />

        <div class="my-medical-title">
            پرونده سلامت من
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="my-message"></asp:Label>

        <asp:Panel ID="pnlPersonnel" runat="server" Visible="false">

            <!-- اطلاعات پرسنلی -->
            <div class="my-card">
                <div class="my-card-title">اطلاعات پرسنلی</div>

                <div class="my-info">
                    <span class="my-label">نام و نام خانوادگی</span>
                    <asp:Label ID="lblFullName" runat="server" CssClass="my-value"></asp:Label>
                </div>

                <div class="my-info">
                    <span class="my-label">شماره پرسنلی</span>
                    <asp:Label ID="lblPersonnelCode" runat="server" CssClass="my-value"></asp:Label>
                </div>

                <div class="my-info">
                    <span class="my-label">واحد سازمانی</span>
                    <asp:Label ID="lblUnit" runat="server" CssClass="my-value"></asp:Label>
                </div>

                <div class="my-info">
                    <span class="my-label">سمت</span>
                    <asp:Label ID="lblPost" runat="server" CssClass="my-value"></asp:Label>
                </div>
            </div>

            <!-- آخرین وضعیت سلامت -->
            <div class="my-card">
                <div class="my-card-title">آخرین وضعیت سلامت</div>

                <div class="my-info">
                    <span class="my-label">وزن</span>
                    <asp:Label ID="lblWeight" runat="server" CssClass="my-value"></asp:Label>
                </div>

                <div class="my-info">
                    <span class="my-label">شاخص توده بدنی (BMI)</span>
                    <asp:Label ID="lblBMI" runat="server" CssClass="my-value"></asp:Label>
                </div>

                <div class="my-info">
                    <span class="my-label">فشار خون</span>
                    <asp:Label ID="lblBloodPressure" runat="server" CssClass="my-value"></asp:Label>
                </div>

                <div class="my-info">
                    <span class="my-label">تاریخ آخرین معاینه</span>
                    <asp:Label ID="lblLastExam" runat="server" CssClass="my-value"></asp:Label>
                </div>
            </div>

            <!-- نمودارها -->
            <div class="my-card">
                <div class="my-card-title">نمودار روند وضعیت سلامت</div>

                <div class="my-chart-grid">

                    <div class="my-card">
                        <div class="my-card-title">وزن و شاخص توده بدنی</div>
                        <div class="my-chart-box">
                            <canvas id="chartWeightBMI"></canvas>
                        </div>
                    </div>

                    <div class="my-card">
                        <div class="my-card-title">قند خون</div>
                        <div class="my-chart-box">
                            <canvas id="chartGlucose"></canvas>
                        </div>
                    </div>

                    <div class="my-card">
                        <div class="my-card-title">چربی خون</div>
                        <div class="my-chart-box">
                            <canvas id="chartLipids"></canvas>
                        </div>
                    </div>

                    <div class="my-card">
                        <div class="my-card-title">فشار خون</div>
                        <div class="my-chart-box">
                            <canvas id="chartBloodPressure"></canvas>
                        </div>
                    </div>

                    <div class="my-card">
                        <div class="my-card-title">آزمایش‌های کبد و کلیه</div>
                        <div class="my-chart-box">
                            <canvas id="chartLiverKidney"></canvas>
                        </div>
                    </div>

                    <div class="my-card">
                        <div class="my-card-title">شمارش سلول‌های خونی (CBC)</div>
                        <div class="my-chart-box">
                            <canvas id="chartCBC"></canvas>
                        </div>
                    </div>

                </div>
            </div>

            <!-- توصیه های پزشکی -->
            <div class="my-card">
                <div class="my-card-title">توصیه‌ها و نظرات پزشکی</div>

                <asp:Repeater ID="rptRecommendations" runat="server">
                    <ItemTemplate>
                        <div class="my-recommendation">
                            <div class="my-recommendation-title">
                                <%# Server.HtmlEncode(Convert.ToString(Eval("Title"))) %>
                            </div>

                            <div>
                                <%# Server.HtmlEncode(Convert.ToString(Eval("Body"))).Replace(vbCrLf, "<br />").Replace(vbLf, "<br />") %>
                            </div>

                            <div class="my-recommendation-date">
                                تاریخ: <%# Server.HtmlEncode(Convert.ToString(Eval("Date"))) %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Panel ID="pnlNoRecommendations" runat="server" Visible="false" CssClass="my-empty">
                    تاکنون توصیه یا نظر پزشکی برای شما ثبت نشده است.
                </asp:Panel>
            </div>

            <!-- سوابق -->
            <div class="my-card">
                <div class="my-card-title">سوابق معاینات و آزمایش‌های پزشکی</div>

                <div style="overflow-x:auto;">
                    <asp:GridView ID="gvHistory" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="my-grid"
                        EmptyDataText="سابقه معاینه‌ای برای شما ثبت نشده است."
                        GridLines="None">

                        <Columns>
                            <asp:BoundField DataField="VisitDate" HeaderText="تاریخ مراجعه" />
                            <asp:BoundField DataField="ExamType" HeaderText="نوع معاینه" />
                            <asp:BoundField DataField="LabDate" HeaderText="تاریخ آزمایش" />
                            <asp:BoundField DataField="Weight" HeaderText="وزن" />
                            <asp:BoundField DataField="BMI" HeaderText="BMI" />
                            <asp:BoundField DataField="Systolic" HeaderText="فشار بالا" />
                            <asp:BoundField DataField="Diastolic" HeaderText="فشار پایین" />
                            <asp:BoundField DataField="Chol" HeaderText="کلسترول" />
                            <asp:BoundField DataField="TG" HeaderText="تری‌گلیسیرید" />
                            <asp:BoundField DataField="HDL" HeaderText="HDL" />
                            <asp:BoundField DataField="LDL" HeaderText="LDL" />
                            <asp:BoundField DataField="FBS" HeaderText="قند خون" />
                            <asp:BoundField DataField="HbA1C" HeaderText="HbA1C" />
                            <asp:BoundField DataField="Recommendation" HeaderText="توصیه پزشکی" />
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

            <asp:Literal ID="litChartData" runat="server"></asp:Literal>

        </asp:Panel>

    </div>

    <script type="text/javascript">

        function drawMyChart(canvasId, chartData) {

            if (typeof Chart === "undefined") {
                return;
            }

            if (!chartData || !chartData.labels || chartData.labels.length === 0) {
                return;
            }

            var canvas = document.getElementById(canvasId);

            if (!canvas) {
                return;
            }

            var colors = [
                "#176B87",
                "#E67E22",
                "#27AE60",
                "#8E44AD",
                "#C0392B"
            ];

            var datasets = [];

            for (var i = 0; i < chartData.datasets.length; i++) {

                datasets.push({
                    label: chartData.datasets[i].label,
                    data: chartData.datasets[i].data,
                    borderColor: colors[i % colors.length],
                    backgroundColor: colors[i % colors.length],
                    fill: false,
                    lineTension: 0.15,
                    borderWidth: 2,
                    pointRadius: 4,
                    spanGaps: true
                });

            }

            new Chart(canvas.getContext("2d"), {
                type: "line",
                data: {
                    labels: chartData.labels,
                    datasets: datasets
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    legend: {
                        position: "bottom",
                        labels: {
                            fontFamily: "Tahoma",
                            fontSize: 11
                        }
                    },
                    tooltips: {
                        mode: "index",
                        intersect: false,
                        titleFontFamily: "Tahoma",
                        bodyFontFamily: "Tahoma"
                    },
                    scales: {
                        xAxes: [{
                            ticks: {
                                fontFamily: "Tahoma",
                                autoSkip: true,
                                maxTicksLimit: 8
                            }
                        }],
                        yAxes: [{
                            ticks: {
                                beginAtZero: false,
                                fontFamily: "Tahoma"
                            }
                        }]
                    }
                }
            });

        }

        function drawAllMyCharts() {

            if (typeof Chart === "undefined") {
                console.log("Chart.js بارگذاری نشده است.");
                return;
            }

            if (typeof medicalCharts === "undefined") {
                console.log("اطلاعات نمودارها دریافت نشده است.");
                return;
            }

            drawMyChart("chartWeightBMI", medicalCharts.weight);
            drawMyChart("chartGlucose", medicalCharts.glucose);
            drawMyChart("chartLipids", medicalCharts.lipids);
            drawMyChart("chartBloodPressure", medicalCharts.pressure);
            drawMyChart("chartLiverKidney", medicalCharts.liver);
            drawMyChart("chartCBC", medicalCharts.cbc);

        }

        if (document.readyState === "loading") {
            document.addEventListener("DOMContentLoaded", drawAllMyCharts);
        } else {
            drawAllMyCharts();
        }

    </script>

</asp:Content>
