
<%@ Page Title="بارگذاری اطلاعات معاینات پزشکی"
    Language="VB"
    MasterPageFile="~/dabir.master"
    AutoEventWireup="false"
    CodeFile="MedicalExamImport.aspx.vb"
    Inherits="MedicalExamImport" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<style>
    .medical-import {
        direction: rtl;
        text-align: right;
        padding: 20px;
    }

    .import-card {
        background: #fff;
        border: 1px solid #e5e7eb;
        border-radius: 12px;
        padding: 25px;
        margin-bottom: 20px;
        box-shadow: 0 3px 12px rgba(0,0,0,.04);
    }

    .import-title {
        font-size: 20px;
        font-weight: bold;
        color: #164e63;
        margin-bottom: 20px;
    }

    .upload-input {
        display: block;
        width: 100%;
        padding: 14px;
        border: 2px dashed #94a3b8;
        border-radius: 9px;
        background: #f8fafc;
        margin: 15px 0;
    }

    .btn-import {
        background: #0f766e;
        color: white;
        border: none;
        padding: 12px 28px;
        border-radius: 8px;
        cursor: pointer;
        font-size: 14px;
    }

    .btn-import:hover {
        background: #115e59;
    }

    .warning-box {
        background: #fff7ed;
        border: 1px solid #fed7aa;
        padding: 14px;
        border-radius: 8px;
        color: #9a3412;
        line-height: 2;
    }

    .result-box {
        display: block;
        padding: 15px;
        border-radius: 8px;
        margin-top: 15px;
        line-height: 2;
    }

    .success-box {
        background: #ecfdf5;
        color: #047857;
        border: 1px solid #a7f3d0;
    }

    .error-box {
        background: #fef2f2;
        color: #b91c1c;
        border: 1px solid #fecaca;
    }
</style>

<div class="medical-import">

    <div class="import-card">

        <div class="import-title">
            <i class="fa fa-heartbeat"></i>
            مدیریت اطلاعات معاینات پزشکی پرسنل
        </div>

        <p>
            فایل اکسل نتایج معاینات پزشکی را انتخاب کنید.
            اطلاعات قبلی پس از تأیید صحت فایل جدید جایگزین می‌شود.
        </p>

        <div class="warning-box">
            <b>توجه مهم:</b>
            با بارگذاری فایل جدید، تمام اطلاعات قبلی معاینات
            جایگزین خواهند شد. لطفاً از صحت فایل اطمینان حاصل کنید.
        </div>

        <br />

        <asp:FileUpload ID="fuExcel"
            runat="server"
            CssClass="upload-input" />

        <asp:CheckBox ID="chkReplace"
            runat="server"
            Text="از جایگزینی کامل اطلاعات قبلی اطمینان دارم."
        />

        <br /><br />

        <asp:Button ID="btnImport"
            runat="server"
            Text="بارگذاری و جایگزینی اطلاعات"
            CssClass="btn-import"
            OnClick="btnImport_Click"
            OnClientClick="return confirm('تمام اطلاعات قبلی معاینات با فایل جدید جایگزین می‌شود. ادامه می‌دهید؟');" />

        <asp:Label ID="lblMessage"
            runat="server"
            EnableViewState="false" />

    </div>

</div>

</asp:Content>
