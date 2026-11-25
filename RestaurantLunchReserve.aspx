
<%@ Page Title="رزرو هفتگی ناهار" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="RestaurantLunchReserve.aspx.vb" Inherits="RestaurantLunchReserve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .lunch-container {
            direction: rtl;
            max-width: 1250px;
            margin: 20px auto;
            padding: 15px;
            font-family: Tahoma, Arial;
            color: #26364b;
        }

        .lunch-header {
            background: linear-gradient(135deg, #1769aa, #2386c8);
            color: #fff;
            padding: 22px;
            border-radius: 16px;
            margin-bottom: 15px;
            box-shadow: 0 5px 15px rgba(25, 105, 170, .15);
        }

        .lunch-header h2 {
            margin: 0 0 8px 0;
            font-size: 20px;
        }

        .lunch-header p {
            margin: 0;
            font-size: 12px;
            opacity: .95;
            line-height: 2;
        }

        .lunch-guide {
            background: #eef7ff;
            border: 1px solid #cfe7fa;
            border-radius: 12px;
            padding: 13px 15px;
            margin-bottom: 18px;
            font-size: 12px;
            line-height: 2.2;
            color: #245b83;
        }

        .lunch-guide strong {
            color: #1266a5;
        }

        .lunch-calendar {
            display: grid;
            grid-template-columns: repeat(4, minmax(0, 1fr));
            gap: 14px;
        }

        .lunch-day {
            background: #fff;
            border: 1px solid #e1e8f0;
            border-radius: 15px;
            padding: 13px;
            box-shadow: 0 3px 12px rgba(30, 60, 90, .07);
            min-width: 0;
            transition: .2s;
        }

        .lunch-day:hover {
            box-shadow: 0 6px 18px rgba(30, 60, 90, .12);
        }

        .lunch-day.today-day {
            background: #f4f6f8;
            border-color: #d9e0e7;
        }

        .lunch-day.holiday-day {
            background: #fff8f7;
            border-color: #f1d2ce;
        }

        .lunch-day-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 8px;
            margin-bottom: 13px;
        }

        .lunch-day-title {
            font-size: 14px;
            font-weight: bold;
            color: #183d68;
        }

        .lunch-date {
            font-size: 11px;
            color: #356b9b;
            background: #edf5ff;
            padding: 5px 9px;
            border-radius: 20px;
            white-space: nowrap;
        }

        .holiday-day .lunch-date {
            color: #b34b42;
            background: #fde9e6;
        }

        .today-badge {
            display: inline-block;
            margin-right: 5px;
            padding: 3px 7px;
            border-radius: 12px;
            color: #fff;
            background: #778899;
            font-size: 10px;
        }

        .food-caption {
            font-size: 12px;
            color: #61748b;
            font-weight: bold;
            margin: 8px 0 10px;
        }

        /* ظاهر گزینه‌های انتخاب غذا */
        .food-options {
            display: block;
            width: 100%;
        }

        .food-options input[type=radio] {
            position: absolute;
            opacity: 0;
            width: 1px;
            height: 1px;
            margin: 0;
        }

        .food-options label {
            display: block;
            cursor: pointer;
            position: relative;
            background: #f6f8fb;
            border: 1px solid #e3eaf2;
            border-radius: 10px;
            padding: 12px 38px 12px 10px;
            margin: 0 0 9px 0;
            color: #344b63;
            font-size: 12px;
            line-height: 1.9;
            transition: all .2s ease;
        }

        .food-options label:before {
            content: "";
            position: absolute;
            right: 13px;
            top: 50%;
            transform: translateY(-50%);
            width: 15px;
            height: 15px;
            border: 2px solid #9aabba;
            border-radius: 50%;
            background: #fff;
            box-sizing: border-box;
        }

        .food-options input[type=radio]:checked + label {
            background: #eaf5ff;
            border: 2px solid #2184cc;
            color: #075b9d;
            font-weight: bold;
            padding-top: 11px;
            padding-bottom: 11px;
            box-shadow: 0 3px 9px rgba(33, 132, 204, .13);
        }

        .food-options input[type=radio]:checked + label:before {
            border: 5px solid #2184cc;
            background: #fff;
        }

        .food-options input[type=radio]:focus + label {
            outline: 2px solid #8bc5f1;
            outline-offset: 2px;
        }

        .food-options input[type=radio]:disabled + label {
            cursor: not-allowed;
            opacity: .55;
        }

        .holiday-day .food-options input[type=radio]:checked + label {
            background: #fff0ed;
            border-color: #db766b;
            color: #a7443b;
        }

        .holiday-day .food-options input[type=radio]:checked + label:before {
            border-color: #db766b;
        }

        .lunch-day-status {
            display: block;
            margin-top: 10px;
            padding: 8px 9px;
            border-radius: 8px;
            background: #f3f6f9;
            color: #77889a;
            font-size: 10px;
            line-height: 1.9;
        }

        .holiday-day .lunch-day-status {
            background: #fff0ed;
            color: #a85b52;
        }

        .lunch-message {
            display: block;
            margin: 15px 0;
            padding: 14px 16px;
            border-radius: 11px;
            font-size: 13px;
            line-height: 2;
        }

        .lunch-message.success {
            background: #eaf8ef;
            border: 1px solid #b9e6c7;
            color: #18733b;
        }

        .lunch-message.error {
            background: #fff0ef;
            border: 1px solid #f1c6c2;
            color: #b52e27;
        }

        .lunch-save-area {
            text-align: center;
            margin: 22px 0 10px;
        }

        .lunch-save-button {
            border: 0;
            border-radius: 10px;
            padding: 13px 38px;
            background: linear-gradient(135deg, #16834b, #20a65e);
            color: #fff;
            font-family: Tahoma;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
            box-shadow: 0 4px 12px rgba(32, 166, 94, .2);
        }

        .lunch-save-button:hover {
            background: linear-gradient(135deg, #116d3d, #188c4e);
        }

        .lunch-save-button:disabled {
            opacity: .5;
            cursor: not-allowed;
        }

        @media (max-width: 1000px) {
            .lunch-calendar {
                grid-template-columns: repeat(3, minmax(0, 1fr));
            }
        }

        @media (max-width: 700px) {
            .lunch-calendar {
                grid-template-columns: repeat(2, minmax(0, 1fr));
                gap: 10px;
            }

            .lunch-container {
                padding: 8px;
            }

            .lunch-day {
                padding: 10px;
            }
        }

        @media (max-width: 420px) {
            .lunch-calendar {
                grid-template-columns: 1fr;
            }

            .lunch-day {
                padding: 13px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="lunch-container">

        <div class="lunch-header">
            <h2>🍽 رزرو هفتگی ناهار</h2>
            <p>
                غذای موردنظر خود را برای روزهای آینده انتخاب کنید.
                امکان ویرایش رزرو تا پایان روز قبل وجود دارد.
            </p>
        </div>

        <div class="lunch-guide">
            <strong>راهنمای انتخاب:</strong>
            روی کادر غذای موردنظر کلیک کنید تا انتخاب شود.
            کادر آبی نشان‌دهنده انتخاب شماست.
            <br />
            در روزهای عادی باید غذا یا مرخصی را انتخاب کنید؛
            در پنج‌شنبه و جمعه انتخاب غذا اختیاری است.
        </div>

        <asp:Label ID="lblMessage" runat="server"
            EnableViewState="false" />

        <div class="lunch-calendar">

            <asp:Repeater ID="rptWeek" runat="server"
                OnItemDataBound="rptWeek_ItemDataBound">

                <ItemTemplate>

                    <asp:Panel ID="pnlDay" runat="server"
                        CssClass="lunch-day">

                        <asp:HiddenField ID="hfDate" runat="server"
                            Value='<%# Eval("MenuDateIso") %>' />

                        <div class="lunch-day-header">

                            <div class="lunch-day-title">
                                <%# Eval("WeekdayName") %>
                                <asp:Label ID="lblTodayBadge" runat="server"
                                    CssClass="today-badge"
                                    Text="امروز" Visible="false" />
                            </div>

                            <div class="lunch-date">
                                <%# Eval("DateText") %>
                            </div>

                        </div>

                        <div class="food-caption">
                            غذای ناهار موردنظر:
                        </div>

                        <asp:RadioButtonList ID="rblFood" runat="server"
                            CssClass="food-options"
                            RepeatDirection="Vertical"
                            RepeatLayout="Flow">
                        </asp:RadioButtonList>

                        <asp:Label ID="lblDayStatus" runat="server"
                            CssClass="lunch-day-status" />

                    </asp:Panel>

                </ItemTemplate>

            </asp:Repeater>

        </div>

        <div class="lunch-save-area">

            <asp:Button ID="btnSave" runat="server"
                Text="ثبت نهایی رزروها"
                CssClass="lunch-save-button"
                OnClick="btnSave_Click"
                CausesValidation="false" />

        </div>

    </div>

</asp:Content>
