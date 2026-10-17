<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnAttachCertificate.aspx.vb" Inherits="trnAttachCertificate" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .rtl { direction: rtl; text-align: right; }
        .document-card { transition: transform 0.2s; }
        .document-card:hover { transform: translateY(-5px); }
        .preview-img { height: 180px; object-fit: cover; cursor: pointer; }
        .upload-area { background-color: #f8f9fa; border: 2px dashed #dee2e6; border-radius: 10px; }
        .file-list { max-height: 500px; overflow-y: auto; }
        .form-group { margin-bottom: 1rem; }

        .btn-delete {
    background-color: #dc3545;
    color: white;
    padding: 5px 10px;
    text-decoration: none;
    border-radius: 3px;
    display: inline-block;
    font-size: 12px;
    cursor: pointer;
    border: none;
}

.btn-delete:hover {
    background-color: #c82333;
    color: white;
    text-decoration: none;
}

.btn-download {
    background-color: #007bff;
    color: white;
    padding: 5px 10px;
    text-decoration: none;
    border-radius: 3px;
    display: inline-block;
    font-size: 12px;
}

.btn-download:hover {
    background-color: #0056b3;
    color: white;
    text-decoration: none;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container-fluid rtl mt-3">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">مدیریت مدارک آموزشی پرسنل</h4>
            </div>
            <div class="card-body">
                
                <!-- بخش انتخاب پرسنل -->
                <div class="row mb-4">
                    <div class="col-md-6">
                        <label class="form-label fw-bold">انتخاب پرسنل:</label>
                        <asp:DropDownList ID="ddlPersonnel" runat="server" CssClass="form-select" 
                            AutoPostBack="True" OnSelectedIndexChanged="ddlPersonnel_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label fw-bold">جستجو:</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" 
                            placeholder="نام یا کد ملی..." AutoPostBack="True" OnTextChanged="txtSearch_TextChanged" />
                    </div>
                </div>

                <!-- اطلاعات پرسنل انتخاب شده -->
                <asp:Panel ID="PanelPersonnelInfo" runat="server" Visible="False" CssClass="alert alert-info mb-4">
                    <asp:Label ID="lblPersonnelInfo" runat="server" Font-Bold="True" />
                </asp:Panel>

                <!-- ========== کمبوباکس‌های بانک و شعبه (به سبک کدهای خودتان) ========== -->
                <div class="row mb-4">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>نوع گواهی:</label>
                            <asp:DropDownList ID="CmbGavahiType" runat="server" CssClass="form-control" 
                                AutoPostBack="True" OnSelectedIndexChanged="CmbGavahiType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>نام بانک:</label>
                            <asp:DropDownList ID="cmbBank" runat="server" CssClass="form-control" 
                                AutoPostBack="True" OnSelectedIndexChanged="cmbBank_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row mb-4">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>نام شعبه:</label>
                            <asp:DropDownList ID="cmbShobe" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>سازمان/موسسه:</label>
                            <asp:TextBox ID="txtSazman" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </div>

                <div class="row mb-4">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>نام گیرنده:</label>
                            <asp:TextBox ID="txtGirande" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>مبلغ:</label>
                            <asp:TextBox ID="txtMozd" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="HiddenFieldlater_num" runat="server" />
                <asp:HiddenField ID="hdnType" runat="server" />

                <!-- دکمه ثبت (مثل save_final شما) -->
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:Button ID="btnSaveGavahi" runat="server" Text="ثبت گواهی" CssClass="btn btn-success" OnClick="btnSaveGavahi_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="جدید" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
                    </div>
                </div>

                <!-- بخش آپلود مدرک -->
                <asp:Panel ID="PanelUpload" runat="server" Visible="False" CssClass="upload-area p-4 mb-4">
                    <h5 class="mb-3">افزودن مدرک جدید</h5>
                    <div class="row">
                        <div class="col-md-4 mb-2">
                            <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-select">
                                <asp:ListItem Value="">-- انتخاب نوع مدرک --</asp:ListItem>
                                <asp:ListItem Value="PersonalPic.jpg">عکس پرسنلی</asp:ListItem>
                                <asp:ListItem Value="IdCart.jpg">کارت ملی</asp:ListItem>
                                <asp:ListItem Value="ShenasOne.jpg">شناسنامه صفحه اول</asp:ListItem>
                                <asp:ListItem Value="ShanasDesc.jpg">شناسنامه توضیحات</asp:ListItem>
                                <asp:ListItem Value="LastCertificate.jpg">مدرک تحصیلی</asp:ListItem>
                                <asp:ListItem Value="BackgrounRecord.jpg">گواهی عدم سوء پیشینه</asp:ListItem>
                                <asp:ListItem Value="MilitaryCardType.jpg">کارت معافیت/خدمت</asp:ListItem>
                                <asp:ListItem Value="MarriagePage.jpg">صفحه ازدواج</asp:ListItem>
                                <asp:ListItem Value="ChildrenPage.jpg">صفحه فرزندان</asp:ListItem>
                                <asp:ListItem Value="100_health.jpg">مدرک HSE</asp:ListItem>
                                <asp:ListItem Value="100_checkup.jpg">مدرک HSE پشت</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4 mb-2">
                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-3 mb-2">
                            <asp:TextBox ID="txtCustomCaption" runat="server" CssClass="form-control" 
                                placeholder="عنوان دلخواه (اختیاری)" />
                        </div>
                        <div class="col-md-1 mb-2">
                            <asp:Button ID="btnUpload" runat="server" Text="آپلود" CssClass="btn btn-primary w-100" 
                                OnClick="btnUpload_Click" />
                        </div>
                    </div>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />
                </asp:Panel>

                <!-- نمایش مدارک -->
                <asp:Panel ID="PanelAttach" runat="server" GroupingText="لیست مدارک" 
                    CssClass="border rounded p-3 mt-3" BackColor="#FFFFCC" Visible="False">
                    
                    <div class="row file-list">
                        <asp:Repeater ID="RepeaterImages" runat="server">
                            <ItemTemplate>
                                <div class="col-md-3 col-sm-6 mb-4">
                                    <div class="card shadow-sm document-card h-100">
                                        <img src='<%# Eval("Url") %>' class="card-img-top preview-img" 
                                            onerror="this.src='../Images/no-image.png'"
                                            onclick='showPreview(this.src)' />
                                        <div class="card-body p-2">
                                            <div class="text-muted mb-2 text-center" style="font-size: 14px; font-weight: bold;">
                                                <%# Eval("SortOrder") %>. <%# Eval("Caption") %>
                                            </div>
                                            <div class="btn-group w-100" role="group">
                                                <a href='<%# "DownloadFile.aspx?file=" & Server.UrlEncode(Eval("Name")) & "&nat=" & Eval("Nat") %>' 
                                                   class="btn btn-sm btn-outline-primary">دانلود</a>
                                                <asp:LinkButton ID="btnDelete" runat="server"
                                                    CommandArgument='<%# Eval("Name") %>'
                                                    OnCommand="btnDelete_Command"
                                                    OnClientClick="return confirm('آیا از حذف این مدرک اطمینان دارید؟');"
                                                    CssClass="btn btn-sm btn-outline-danger">حذف</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="row mt-3">
                        <div class="col-12 text-end">
                            <asp:Button ID="btnDownloadZip" runat="server" Text="دانلود همه مدارک به صورت فشرده" 
                                CssClass="btn btn-success" OnClick="btnDownloadZip_Click" />
                        </div>
                    </div>
                    
                    <div id="divNoData" runat="server" visible="false" class="text-center text-muted">
                        <hr />
                        <span>هیچ مدرکی برای این پرسنل یافت نشد.</span>
                    </div>
                </asp:Panel>

                <!-- GridView برای نمایش سوابق گواهی (مثل gridSabege شما) -->
                <div class="mt-4">
                    <asp:GridView ID="gridSabege" runat="server" CssClass="table table-bordered table-striped"
                        AutoGenerateColumns="True" AllowPaging="True" PageSize="10"
                        OnPageIndexChanging="gridSabege_PageIndexChanging">
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>

    <!-- مودال پیش‌نمایش -->
    <div class="modal fade" id="imageModal" tabindex="-1">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">پیش‌نمایش مدرک</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body text-center">
                    <img id="modalImage" src="" style="max-width: 100%; max-height: 70vh;" />
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function showPreview(src) {
            var modal = new bootstrap.Modal(document.getElementById('imageModal'));
            document.getElementById('modalImage').src = src;
            modal.show();
        }

        document.addEventListener('DOMContentLoaded', function () {
            var ddl = document.getElementById('<%= ddlDocumentType.ClientID %>');
            var txtCustom = document.getElementById('<%= txtCustomCaption.ClientID %>');
            if (ddl) {
                ddl.addEventListener('change', function () {
                    if (this.value !== '') {
                        txtCustom.value = '';
                        txtCustom.placeholder = 'در صورت انتخاب نوع مدرک، نیازی به وارد کردن عنوان نیست';
                    } else {
                        txtCustom.placeholder = 'عنوان دلخواه (اختیاری)';
                    }
                });
            }
        });
    </script>
</asp:Content>



