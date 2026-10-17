<%@ Page Title="گزارش حضور و غیاب" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnAttendanceReport.aspx.vb" Inherits="trnAttendanceReport" %>

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
            background: linear-gradient(135deg, #2c3e50 0%, #3498db 100%);
            color: white;
            padding: 15px 20px;
            border-radius: 10px 10px 0 0;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
        }
        .report-body {
            padding: 25px;
        }
        .filter-box {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 20px;
            border: 1px solid #ddd;
        }
        .filter-row {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
            align-items: flex-end;
        }
        .filter-group {
            flex: 1;
            min-width: 200px;
        }
        .filter-group label {
            display: block;
            font-weight: bold;
            color: #555;
            margin-bottom: 5px;
            font-size: 13px;
        }
        .filter-group .form-control {
            width: 100%;
            padding: 8px 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            background: white;
        }
        .btn-report {
            padding: 10px 30px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s;
            color: white;
        }
        .btn-report:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
        }
        .btn-primary { background: #3498db; }
        .btn-success { background: #27ae60; }
        .btn-warning { background: #f39c12; }
        .btn-danger { background: #e74c3c; }
        .btn-back { background: #95a5a6; }
        
        .table-container {
            overflow-x: auto;
            margin-top: 20px;
        }
        .table-report {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
        }
        .table-report th {
            background: #2c3e50;
            color: white;
            padding: 12px 15px;
            text-align: center;
            white-space: nowrap;
        }
        .table-report td {
            padding: 10px 15px;
            border-bottom: 1px solid #eee;
            text-align: center;
        }
        .table-report tr:hover {
            background: #f5f5f5;
        }
        .table-report .status-present {
            color: #27ae60;
            font-weight: bold;
        }
        .table-report .status-absent {
            color: #e74c3c;
            font-weight: bold;
        }
        .table-report .status-late {
            color: #f39c12;
            font-weight: bold;
        }
        
        .summary-box {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
            margin-bottom: 20px;
            padding: 15px;
            background: #f8f9fa;
            border-radius: 10px;
            border: 1px solid #ddd;
        }
        .summary-item {
            flex: 1;
            min-width: 150px;
            text-align: center;
            padding: 10px;
            background: white;
            border-radius: 8px;
            box-shadow: 0 1px 5px rgba(0,0,0,0.1);
        }
        .summary-item .number {
            font-size: 28px;
            font-weight: bold;
        }
        .summary-item .label {
            font-size: 13px;
            color: #888;
        }
        .summary-item.present .number { color: #27ae60; }
        .summary-item.absent .number { color: #e74c3c; }
        .summary-item.late .number { color: #f39c12; }
        .summary-item.total .number { color: #3498db; }
        
        .no-data {
            text-align: center;
            padding: 40px;
            color: #999;
            font-size: 16px;
        }

        .status-present {
    color: #27ae60;
    font-weight: bold;
    background: #d4edda;
    padding: 3px 12px;
    border-radius: 15px;
    display: inline-block;
}
.status-absent {
    color: #e74c3c;
    font-weight: bold;
    background: #f8d7da;
    padding: 3px 12px;
    border-radius: 15px;
    display: inline-block;
}
.status-warning {
    color: #f39c12;
    font-weight: bold;
    background: #fff3cd;
    padding: 3px 12px;
    border-radius: 15px;
    display: inline-block;
}
        
        @media print {
            .no-print { display: none !important; }
            .report-container { box-shadow: none; border: 1px solid #ddd; }
            .filter-box { background: none; border: 1px solid #ddd; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="report-container">
        <div class="report-header">
            📊 گزارش حضور و غیاب پرسنل
        </div>
        
        <div class="report-body">
            
            <!-- فیلترها -->
            <div class="filter-box no-print">
                <div class="filter-row">
                    <div class="filter-group">
                        <label>📚 دوره آموزشی</label>
                        <asp:DropDownList ID="cmbCourses" runat="server" CssClass="form-control" Height="45px">
                        </asp:DropDownList>
                    </div>
                
                    <div class="filter-group" style="flex: 0 0 auto;">
                        <asp:Button ID="btnShowReport" runat="server" Text="📊 نمایش گزارش" CssClass="btn-report btn-primary" OnClick="btnShowReport_Click" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="📥 خروجی Excel" CssClass="btn-report btn-success" OnClick="btnExportExcel_Click" />
                       
                        <asp:Button ID="btnBackToHome" runat="server" CssClass="btn-report btn-warning" 
            Text="🏠 بازگشت به صفحه اصلی" OnClick="btnBackToHome_Click" 
            style="margin-right: 10px;" />
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style="display: block; margin-top: 10px;"></asp:Label>
            </div>

            <!-- خلاصه آمار -->
            <!-- خلاصه آمار -->
<div id="divSummary" runat="server" visible="false" class="summary-box">
    <div class="summary-item total">
        <div class="number" id="lblTotalCount" runat="server">0</div>
        <div class="label">👥 کل پرسنل</div>
    </div>
    <div class="summary-item present">
        <div class="number" id="lblPresentCount" runat="server">0</div>
        <div class="label">✅ حاضر (ورود و خروج)</div>
    </div>
    <div class="summary-item late">
        <div class="number" id="lblLateCount" runat="server">0</div>
        <div class="label">⏳ در کلاس (فقط ورود)</div>
    </div>
</div>

<!-- جدول گزارش -->
<div id="div1" runat="server" visible="false" class="table-container">
    <asp:GridView ID="GridView1" runat="server" 
        CssClass="table-report"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="20"
        OnPageIndexChanging="grdAttendance_PageIndexChanging"
        GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="40px" />
            </asp:TemplateField>
            <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
            <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" />
            <asp:BoundField DataField="Unit" HeaderText="واحد" />
            <asp:BoundField DataField="Position" HeaderText="سمت" />
            <asp:BoundField DataField="CheckDate" HeaderText="تاریخ" />
            <asp:BoundField DataField="CheckInTime" HeaderText="زمان ورود" />
            <asp:BoundField DataField="CheckOutTime" HeaderText="زمان خروج" />
            <asp:BoundField DataField="Duration" HeaderText="مدت حضور" />
        </Columns>
        <PagerStyle CssClass="pager" HorizontalAlign="Center" />
    </asp:GridView>
</div>

            <!-- جدول گزارش -->
            <div id="divTable" runat="server" visible="false" class="table-container">
                <asp:GridView ID="grdAttendance" runat="server" 
    CssClass="table-report"
    AutoGenerateColumns="false"
    AllowPaging="true"
    PageSize="20"
    OnPageIndexChanging="grdAttendance_PageIndexChanging"
    GridLines="None">
    <Columns>
        <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <ItemStyle Width="40px" />
        </asp:TemplateField>
        <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
        <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" />
        <asp:BoundField DataField="Unit" HeaderText="واحد" />
        <asp:BoundField DataField="Position" HeaderText="امور" />
       <asp:BoundField DataField="CheckDate" HeaderText="تاریخ" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false" />
        <asp:BoundField DataField="CheckInTime" HeaderText="زمان ورود" />
        <asp:BoundField DataField="CheckOutTime" HeaderText="زمان خروج" />
        <asp:BoundField DataField="Duration" HeaderText="مدت حضور" />
       <asp:TemplateField HeaderText="وضعیت">
    <ItemTemplate>
        <%# GetStatusIcon(Eval("CheckInTime"), Eval("CheckOutTime")) %>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
    <PagerStyle CssClass="pager" HorizontalAlign="Center" />
</asp:GridView>
            </div>

            <!-- پیام عدم وجود داده -->
            <div id="divNoData" runat="server" visible="false" class="no-data">
                📭 هیچ داده‌ای برای نمایش وجود ندارد.
            </div>
        </div>
    </div>
</asp:Content>