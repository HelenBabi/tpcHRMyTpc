<%@ Page Title="آپلود منوی رستوران" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="UploadMenu.aspx.vb" Inherits="UploadMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-5 mb-5">

    <div class="card shadow-lg border-0 rounded-4 mx-auto" style="max-width: 600px;">
        <div class="card-header bg-primary text-white fw-bold fs-5">
            🍽️ فرم آپلود منوی ماهانه
        </div>
        <br />
        <div class="card-body">

            <!-- انتخاب ماه -->
            <div class="mb-3">
                <label for="ddlMonth" class="form-label fw-bold">ماه مربوطه:</label>
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select w-auto d-inline-block">
                </asp:DropDownList>
            </div>

            <!-- انتخاب فایل -->
            <div class="mb-3">
                <label class="form-label fw-bold">انتخاب تصویر منو:</label>
                <asp:FileUpload ID="fuMenu" runat="server" CssClass="form-control" />
            </div>
            <br />
            <!-- دکمه‌ها -->
            <div class="mb-3">
                <asp:Button ID="btnUpload" runat="server" Text="آپلود تصویر" CssClass="btn btn-success mt-2 px-4 fw-bold" />
                                <asp:Button ID="btnBack" runat="server" Text="🔙 بازگشت به صفحه اصلی" CssClass="btn btn-secondary mt-2 px-4 fw-bold ms-2" />
            </div>

            <!-- نتیجه -->
            <asp:Label ID="lblResult" runat="server" CssClass="d-block mt-3 fw-bold"></asp:Label>

            <!-- پیش‌نمایش تصویر آپلودشده -->
            <div id="previewDiv" runat="server" class="mt-4 text-center" Visible="False">
                <h6 class="fw-bold text-primary mb-2">پیش‌نمایش تصویر آپلودشده:</h6>
                <asp:Image ID="imgPreview" runat="server" CssClass="img-fluid rounded-4 shadow-sm" />
            </div>

        </div>
    </div>

</div>

<style>
.card {
    max-width: 600px;
    margin: 0 auto;
}
.img-fluid {
    max-width: 100%;
    height: auto;
    border: 2px solid #dee2e6;
}
</style>

</asp:Content>
