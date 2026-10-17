<%@ Page Title="ویرایش دسترسی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="EditFormAccess.aspx.vb" Inherits="EditFormAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .chk-row {
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 6px;
            margin-bottom: 8px;
            background-color: #f9f9f9;
            transition: background-color .2s;
        }
        .chk-row:hover { background-color: #eef7ff; }
        .chk-row input[type="checkbox"] { margin-left: 5px; }
        .chk-label { cursor: pointer; font-weight: normal; }
        .card-header h5 { margin: 0; }
        .form-label { font-weight: bold; }

        .alert-info-user {
            background-color: #d1ecf1;
            border-right: 4px solid #17a2b8;
            padding: 10px 15px;
            border-radius: 4px;
            margin-bottom: 15px;
        }

        .alert-form-info {
            background-color: #fff3cd;
            border-right: 4px solid #ffc107;
            padding: 10px 15px;
            border-radius: 4px;
            margin-bottom: 15px;
        }

        .form-badge {
            display: inline-block;
            background-color: #007bff;
            color: white;
            padding: 3px 10px;
            border-radius: 12px;
            font-size: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <div class="card shadow">

            <!-- هدر کارت -->
            <div class="card-header bg-primary text-white">
                <h5>
                    ✏️ ویرایش دسترسی -
                    <asp:Label ID="lblFormName" runat="server" Font-Bold="true"></asp:Label>
                </h5>
            </div>

            <div class="card-body">

                <!-- اطلاعات کاربر -->
                <div class="alert-info-user">
                    <span>👤</span>
                    <strong>کاربر:</strong>
                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                </div>

                <!-- اطلاعات فرم -->
                <div class="alert-form-info">
                    <span>📋</span>
                    <strong>فرم:</strong>
                    <asp:Label ID="lblFormInfo" runat="server"></asp:Label>
                </div>

                <hr />
                <h6>🔧 سطوح دسترسی:</h6>

                <!-- چک‌باکس‌ها -->
                <div class="row">

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkFormAccess" runat="server"
                                Text=" دسترسی به فرم" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowNew" runat="server"
                                Text=" اجازه درج جدید" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowEdit" runat="server"
                                Text=" اجازه ویرایش" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowDelete" runat="server"
                                Text=" اجازه حذف" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowPrint" runat="server"
                                Text=" اجازه چاپ" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowViewOldData" runat="server"
                                Text=" مشاهده اطلاعات قدیمی" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowEditDate" runat="server"
                                Text=" اجازه ویرایش تاریخ" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkPrintAfterSave" runat="server"
                                Text=" چاپ پس از ذخیره" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowDeleted" runat="server"
                                Text=" اجازه حذف رکورد" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowclosed" runat="server"
                                Text=" اجازه بستن" CssClass="chk-label" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowHistory" runat="server"
                                Text=" مشاهده تاریخچه" CssClass="chk-label" />
                        </div>
                    </div>

                </div>

                <hr />

                <!-- دکمه‌ها -->
                <div class="mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="💾 ذخیره تغییرات"
                        CssClass="btn btn-success" OnClick="btnSave_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="🔙 بازگشت"
                        CssClass="btn btn-secondary" OnClick="btnCancel_Click"
                        CausesValidation="false" />
                </div>

                <br />

                <!-- پیام -->
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger font-weight-bold"></asp:Label>

            </div>
        </div>
    </div>

</asp:Content>