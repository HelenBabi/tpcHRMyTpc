<%@ Page Title="افزودن دسترسی جدید" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="AddFormAccess.aspx.vb" Inherits="AddFormAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .card-custom {
            border: none;
            border-radius: 12px;
            box-shadow: 0 2px 12px rgba(0,0,0,0.08);
            overflow: hidden;
        }

        .card-header-warning {
            background: linear-gradient(135deg, #f2994a 0%, #f2c94c 100%);
            color: white;
            padding: 15px 20px;
            font-weight: bold;
            font-size: 16px;
        }

        .info-user-bar {
            background: linear-gradient(135deg, #e8f4f8 0%, #d1ecf1 100%);
            border-right: 4px solid #17a2b8;
            padding: 12px 18px;
            border-radius: 8px;
            margin-bottom: 20px;
            font-size: 14px;
        }

        .form-label-bold {
            font-weight: bold;
            color: #2c3e50;
            margin-bottom: 8px;
            display: block;
        }

        .ddl-forms {
            border-radius: 8px;
            border: 2px solid #e0e0e0;
            padding: 8px 12px;
            font-size: 14px;
            transition: all 0.3s;
        }

        .ddl-forms:focus {
            border-color: #f2994a;
            box-shadow: 0 0 8px rgba(242, 153, 74, 0.3);
            outline: none;
        }

        .access-section-title {
            color: #2c3e50;
            border-bottom: 2px solid #f2994a;
            padding-bottom: 6px;
            margin: 20px 0 15px 0;
            font-weight: bold;
        }

        .chk-row {
            padding: 10px 12px;
            border: 2px solid #e0e0e0;
            border-radius: 8px;
            margin-bottom: 8px;
            background-color: #fafafa;
            transition: all 0.2s;
            cursor: pointer;
        }

        .chk-row:hover {
            background-color: #fff8ee;
            border-color: #f2c94c;
        }

        .chk-row input[type="checkbox"] {
            margin-left: 8px;
            transform: scale(1.2);
            cursor: pointer;
        }

        .chk-row label {
            cursor: pointer;
            font-weight: normal;
            margin: 0;
        }

        .btn-save-custom {
            background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 8px;
            font-weight: bold;
            font-size: 14px;
            transition: transform 0.2s;
        }

        .btn-save-custom:hover {
            transform: translateY(-2px);
            color: white;
            box-shadow: 0 4px 10px rgba(17, 153, 142, 0.3);
        }

        .btn-cancel-custom {
            background: linear-gradient(135deg, #bdc3c7 0%, #95a5a6 100%);
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 8px;
            font-weight: bold;
            font-size: 14px;
        }

        .btn-cancel-custom:hover {
            color: white;
            box-shadow: 0 4px 10px rgba(149, 165, 166, 0.3);
        }

        .alert-msg {
            padding: 10px 15px;
            border-radius: 8px;
            margin-top: 15px;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <div class="card card-custom">

            <div class="card-header-warning">
                ➕ افزودن دسترسی جدید برای:
                <asp:Label ID="lblUser" runat="server" Font-Bold="true"></asp:Label>
            </div>

            <div class="card-body" style="padding: 25px;">

                <div class="info-user-bar">
                    <span style="font-size: 18px;">👤</span>
                    <strong>کاربر انتخاب شده:</strong>
                    <asp:Label ID="lblUserInfo" runat="server"></asp:Label>
                </div>

                <div class="row">
                    <div class="col-md-8">
                        <label class="form-label-bold">📋 فرم مورد نظر:</label>
                        <asp:DropDownList ID="ddlForms" runat="server"
                            CssClass="form-control ddl-forms"
                            AutoPostBack="false" Height="40px">
                        </asp:DropDownList>
                    </div>
                </div>

                <h5 class="access-section-title">🔧 سطوح دسترسی:</h5>

                <div class="row">

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkFormAccess" runat="server"
                                Text=" دسترسی به فرم" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowNew" runat="server"
                                Text=" اجازه درج جدید" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowEdit" runat="server"
                                Text=" اجازه ویرایش" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowDelete" runat="server"
                                Text=" اجازه حذف" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowPrint" runat="server"
                                Text=" اجازه چاپ" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowViewOldData" runat="server"
                                Text=" مشاهده اطلاعات قدیمی" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowEditDate" runat="server"
                                Text=" اجازه ویرایش تاریخ" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkPrintAfterSave" runat="server"
                                Text=" چاپ پس از ذخیره" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowDeleted" runat="server"
                                Text=" اجازه حذف رکورد" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowclosed" runat="server"
                                Text=" اجازه بستن" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="chk-row">
                            <asp:CheckBox ID="chkAllowHistory" runat="server"
                                Text=" مشاهده تاریخچه" />
                        </div>
                    </div>

                </div>

                <hr style="margin: 25px 0;" />

                <div>
                    <asp:Button ID="btnSave" runat="server" Text="💾 ذخیره"
                        CssClass="btn-save-custom" OnClick="btnSave_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="❌ انصراف"
                        CssClass="btn-cancel-custom" OnClick="btnCancel_Click"
                        CausesValidation="false" />
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="alert-msg text-danger"></asp:Label>

            </div>
        </div>
    </div>

</asp:Content>