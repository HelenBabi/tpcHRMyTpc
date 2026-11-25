
<%@ Page Title="گزارش رزرو غذای رستوران"
    Language="VB"
    MasterPageFile="~/dabir.master"
    AutoEventWireup="false"
    CodeFile="RestaurantLunchReport.aspx.vb"
    Inherits="RestaurantLunchReport"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .rl-container {
            direction: rtl;
            font-family: Tahoma;
            padding: 15px;
            color: #333;
        }

        .rl-title {
            background: #263b5e;
            color: white;
            padding: 16px;
            border-radius: 7px;
            margin-bottom: 18px;
            font-size: 18px;
            font-weight: bold;
        }

        .rl-box {
            background: white;
            padding: 18px;
            border-radius: 7px;
            margin-bottom: 18px;
            box-shadow: 0 2px 8px rgba(0,0,0,.06);
        }

        .rl-filter {
            display: flex;
            flex-wrap: wrap;
            align-items: end;
            gap: 12px;
        }

        .rl-filter-item {
            min-width: 170px;
        }

        .rl-filter-item label {
            display: block;
            margin-bottom: 7px;
            font-weight: bold;
            font-size: 13px;
        }

        .rl-input {
            width: 100%;
            box-sizing: border-box;
            padding: 9px;
            border: 1px solid #d5dbe3;
            border-radius: 5px;
            font-family: Tahoma;
        }

        .rl-btn {
            border: 0;
            border-radius: 5px;
            padding: 10px 16px;
            cursor: pointer;
            font-family: Tahoma;
            font-size: 13px;
            color: white;
        }

        .rl-search { background: #2878c7; }
        .rl-excel { background: #16834b; }
        .rl-close { background: #777; }

        .rl-grid {
            width: 100%;
            border-collapse: collapse;
            font-family: Tahoma;
            font-size: 12px;
        }

        .rl-grid th {
            background: #eaf0f7;
            color: #263b5e;
            padding: 11px 7px;
            border: 1px solid #dce2e9;
        }

        .rl-grid td {
            padding: 10px 7px;
            border: 1px solid #e1e5ea;
            text-align: center;
        }

        .rl-grid tr:nth-child(even) {
            background: #fafbfd;
        }

        .rl-grid a {
            color: #1769aa;
            font-weight: bold;
            text-decoration: none;
        }

        .rl-summary {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            margin-bottom: 18px;
        }

        .rl-summary-item {
            background: #f0f5fa;
            border-radius: 7px;
            padding: 13px 18px;
            min-width: 150px;
        }

        .rl-summary-item span {
            display: block;
            color: #687789;
            font-size: 12px;
            margin-bottom: 7px;
        }

        .rl-summary-item strong {
            font-size: 20px;
            color: #263b5e;
        }

        .rl-message {
            display: block;
            padding: 12px;
            margin-bottom: 15px;
            border-radius: 5px;
            background: #fff4db;
            color: #795600;
        }

        .rl-modal {
            display: none;
            position: fixed;
            z-index: 99999;
            right: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,.55);
            overflow-y: auto;
        }

        .rl-modal-content {
            background: white;
            width: 94%;
            max-width: 1150px;
            margin: 4% auto;
            border-radius: 8px;
            padding: 20px;
            box-sizing: border-box;
        }

        .rl-modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #eee;
            padding-bottom: 13px;
            margin-bottom: 15px;
        }

        .rl-table-scroll {
            overflow-x: auto;
        }

        @media print {
            .rl-filter, .rl-actions {
                display: none !important;
            }
        }
    </style>

    <script type="text/javascript">

        function openDetailsModal() {
            document.getElementById('<%= pnlDetailsModal.ClientID %>').style.display = 'block';
        }

        function closeDetailsModal() {
            document.getElementById('<%= pnlDetailsModal.ClientID %>').style.display = 'none';
        }

        window.addEventListener('click', function (event) {
            var modal = document.getElementById('<%= pnlDetailsModal.ClientID %>');
            if (event.target === modal) {
                closeDetailsModal();
            }
        });

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="rl-container">

        <div class="rl-title">
            گزارش رزرو غذای رستوران
        </div>

        <asp:Label ID="lblMessage" runat="server"
            CssClass="rl-message" Visible="false"></asp:Label>

        <div class="rl-box">

            <div class="rl-filter">

                <div class="rl-filter-item">
                    <label>از تاریخ</label>
                    <asp:TextBox ID="txtFromDate" runat="server"
                        TextMode="Date" CssClass="rl-input">
                    </asp:TextBox>
                </div>

                <div class="rl-filter-item">
                    <label>تا تاریخ</label>
                    <asp:TextBox ID="txtToDate" runat="server"
                        TextMode="Date" CssClass="rl-input">
                    </asp:TextBox>
                </div>

                <div class="rl-filter-item">
                    <asp:Button ID="btnSearch" runat="server"
                        Text="نمایش گزارش"
                        CssClass="rl-btn rl-search"
                        OnClick="btnSearch_Click" />
                </div>

                <div class="rl-filter-item">
                    <asp:Button ID="btnExportSummary" runat="server"
                        Text="خروجی Excel گزارش"
                        CssClass="rl-btn rl-excel"
                        OnClick="btnExportSummary_Click" />
                </div>

            </div>

        </div>

        <div class="rl-box">

            <div class="rl-summary">

                <div class="rl-summary-item">
                    <span>مجموع رزرو غذا</span>
                    <strong>
                        <asp:Label ID="lblTotalReservations" runat="server" Text="0" />
                    </strong>
                </div>

                <div class="rl-summary-item">
                    <span>مجموع مرخصی</span>
                    <strong>
                        <asp:Label ID="lblTotalLeaves" runat="server" Text="0" />
                    </strong>
                </div>

            </div>

            <div class="rl-table-scroll">

                <asp:GridView ID="gvSummary" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="rl-grid"
                    GridLines="None"
                    DataKeyNames="MenuDate,SelectedFood"
                    OnRowCommand="gvSummary_RowCommand"
                    EmptyDataText="در بازه انتخاب شده اطلاعاتی ثبت نشده است.">

                    <Columns>

                     <asp:BoundField DataField="MenuDateFa"
    HeaderText="تاریخ" />

                        <asp:BoundField DataField="SelectedFood"
                            HeaderText="نام غذا" />

                        <asp:BoundField DataField="ReserveCount"
                            HeaderText="تعداد رزرو"
                            DataFormatString="{0:N0}" />

                        <asp:TemplateField HeaderText="جزئیات">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDetails" runat="server"
                                    Text="مشاهده افراد"
                                    CommandName="ShowDetails"
                                    CommandArgument="<%# Container.DataItemIndex %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

        <asp:HiddenField ID="hfSelectedDate" runat="server" />
        <asp:HiddenField ID="hfSelectedFood" runat="server" />

        <asp:Panel ID="pnlDetailsModal" runat="server"
            CssClass="rl-modal">

            <div class="rl-modal-content">

                <div class="rl-modal-header">

                    <h3>
                        جزئیات رزرو:
                        <asp:Label ID="lblDetailsTitle" runat="server" />
                    </h3>

                    <button type="button" class="rl-btn rl-close"
                        onclick="closeDetailsModal()">
                        بستن
                    </button>

                </div>

                <div class="rl-actions" style="margin-bottom:15px;">

                    <asp:Button ID="btnExportDetails" runat="server"
                        Text="خروجی Excel اسامی پرسنل"
                        CssClass="rl-btn rl-excel"
                        OnClick="btnExportDetails_Click" />

                </div>

                <div class="rl-table-scroll">

                    <asp:GridView ID="gvDetails" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="rl-grid"
                        GridLines="None"
                        EmptyDataText="فردی برای این غذا ثبت نشده است.">

                        <Columns>

                            <asp:BoundField DataField="codePersoneli"
                                HeaderText="کد پرسنلی" />

                            <asp:BoundField DataField="FullName"
                                HeaderText="نام و نام خانوادگی" />

                            <asp:BoundField DataField="postname"
                                HeaderText="سمت" />

                            <asp:BoundField DataField="OmurName"
                                HeaderText="امور پرسنل" />

                            <asp:BoundField DataField="UnitsName"
                                HeaderText="واحد پرسنلی" />

                         

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </asp:Panel>

    </div>

</asp:Content>
