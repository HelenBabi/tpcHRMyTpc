<%@ Page Title="جزئیات بخش" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="OmurDetails.aspx.vb" Inherits="OmurDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .detail-container { max-width: 900px; margin: 20px auto; padding: 20px; direction: rtl; font-family: Tahoma; }
        .detail-title { font-size: 20px; font-weight: bold; color: #2c3e50; border-bottom: 2px solid #3498db; padding-bottom: 10px; margin-bottom: 20px; }
        .info-box { background: #f8f9fa; padding: 15px; border-radius: 8px; margin-bottom: 20px; }
        .info-row { display: flex; justify-content: space-between; padding: 5px 0; border-bottom: 1px dashed #ddd; }
        .info-label { color: #6c757d; }
        .info-value { font-weight: bold; color: #2c3e50; }
        .table { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .table th { background: #2c3e50; color: white; padding: 8px 12px; text-align: right; }
        .table td { padding: 8px 12px; border-bottom: 1px solid #ddd; }
        .table tr:hover { background: #f8f9fa; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="detail-container">
        <div class="detail-title">📋 جزئیات بخش</div>
        
        <div class="info-box">
            <div class="info-row">
                <span class="info-label">نام بخش:</span>
                <span class="info-value"><asp:Label ID="lblOmurName" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">تعداد پرسنل:</span>
                <span class="info-value"><asp:Label ID="lblPersonCount" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">کل ظرفیت تعیین شده:</span>
                <span class="info-value"><asp:Label ID="lblTotalCapacity" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">تعداد تخصیص داده شده:</span>
                <span class="info-value"><asp:Label ID="lblAssignedCount" runat="server" /></span>
            </div>
            <div class="info-row">
                <span class="info-label">وضعیت:</span>
                <span class="info-value"><asp:Label ID="lblStatus" runat="server" /></span>
            </div>
        </div>
        
        <h4>📅 تاریخ‌های معاینه این بخش</h4>
        <asp:GridView ID="gvDates" runat="server" CssClass="table" AutoGenerateColumns="False" EmptyDataText="هیچ تاریخی ثبت نشده است">
            <Columns>
                <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" />
                <asp:BoundField DataField="DayName" HeaderText="روز هفته" />
                <asp:BoundField DataField="Capacity" HeaderText="ظرفیت" />
                <asp:BoundField DataField="Reserved" HeaderText="تعداد رزرو" />
            </Columns>
        </asp:GridView>
        
        <h4>👥 لیست پرسنل این بخش</h4>
        <asp:GridView ID="gvPersons" runat="server" CssClass="table" AutoGenerateColumns="False" EmptyDataText="هیچ پرسنلی یافت نشد">
            <Columns>
                <asp:BoundField DataField="PersonCode" HeaderText="کد پرسنلی" />
                <asp:BoundField DataField="FullName" HeaderText="نام و نام خانوادگی" />
                <asp:BoundField DataField="Status" HeaderText="وضعیت رزرو" />
            </Columns>
        </asp:GridView>
        
        <div style="margin-top: 20px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت" CssClass="exm-btn exm-btn-back" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>