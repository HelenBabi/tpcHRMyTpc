<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Default6.aspx.vb" Inherits="Default6" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>نمودار Chart.js</title>
       <script src="Scripts/chart.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <canvas id="myChart" width="600" height="300"></canvas>
            <asp:HiddenField ID="HiddenChartData" runat="server" />
        </div>

        <script>
            window.onload = function () {
                var ctx = document.getElementById('myChart').getContext('2d');
                var chartData = JSON.parse('<%= HiddenChartData.Value %>');

                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: chartData.labels,
                        datasets: [{
                            label: 'میزان فروش',
                            data: chartData.data,
                            backgroundColor: 'rgba(75, 192, 192, 0.5)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            };
        </script>
    </form>
</body>
</html>
