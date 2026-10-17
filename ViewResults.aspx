<%@ Page Title="نتایج آزمایش" Language="VB" MasterPageFile="~/dabir.master" 
    AutoEventWireup="false" CodeFile="ViewResults.aspx.vb" Inherits="ViewResults" 
    Culture="auto" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body { background: #f5f7fb; }
        .card-year { cursor: pointer; }
        .dir-note { font-size: .9rem; color: #6c757d; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container py-4">
        <div class="row mb-3">
            <div class="col-12 text-center">
                <h2 class="mb-0"> مشاهده نتایج آزمایشات</h2>
                <p class="dir-note">
                    <%--فایل‌های PDF نتایج در پوشه 
                    <strong>~/App_Data/Results</strong> قرار می‌گیرند. 
                    نام فایل باید شامل شماره ملی باشد (مثال: ..._1372079785_....pdf)--%>
                </p>
            </div>
        </div>
        <br />
        <asp:Literal ID="ltResults" runat="server"></asp:Literal>

        <div class="row mt-5">
            <%--<div class="col-12 text-center text-muted">
                <small>نسخه آزمایشی - برای محیط تولید احراز هویت اضافه شود.</small>
            </div>--%>
        </div>
    </div>
</asp:Content>
