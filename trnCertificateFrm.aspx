<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false"
    CodeFile="trnCertificateFrm.aspx.vb" Inherits="trnCertificateFrm" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .div_page {
            background: #f5f7fa;
            padding: 25px;
            border-radius: 15px;
            direction: rtl;
        }
        .card-shadow {
            background: white;
            border-radius: 15px;
            margin-bottom: 25px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
            overflow: hidden;
        }
        .card-header-custom {
            background: linear-gradient(135deg, #3498db 0%, #2980b9 100%);
            color: white;
            padding: 15px 20px;
            font-weight: bold;
            font-size: 16px;
        }
        .card-header-success {
            background: linear-gradient(135deg, #27ae60 0%, #229954 100%);
        }
        .card-header-warning {
            background: linear-gradient(135deg, #f39c12 0%, #e67e22 100%);
        }
        .card-body {
            padding: 20px;
        }
        .info-row {
            display: flex;
            margin-bottom: 15px;
            padding: 8px 0;
            border-bottom: 1px solid #f0f0f0;
        }
        .info-label {
            width: 130px;
            font-weight: bold;
            color: #555;
        }
        .info-value {
            flex: 1;
            color: #333;
        }
        .gallery-card {
            border: 1px solid #dee2e6;
            border-radius: 12px;
            padding: 15px;
            margin-bottom: 20px;
            background: white;
            transition: all 0.3s ease;
            text-align: center;
        }
        .gallery-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
        }
        .gallery-img {
            width: 100%;
            height: 150px;
            object-fit: cover;
            border-radius: 8px;
            margin-bottom: 10px;
        }
        .file-icon {
            width: 100%;
            height: 150px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: #f8f9fa;
            border-radius: 8px;
            margin-bottom: 10px;
            font-size: 48px;
        }
        .file-name {
            font-size: 12px;
            font-weight: 600;
            color: #2c3e50;
            margin: 10px 0;
            word-break: break-all;
        }
        .btn-group-custom {
            display: flex;
            gap: 8px;
            margin-top: 10px;
        }
        .btn-download {
            background: #3498db;
            color: white;
            border: none;
            padding: 6px 12px;
            border-radius: 20px;
            cursor: pointer;
            font-size: 12px;
            text-decoration: none;
            display: inline-block;
            flex: 1;
            text-align: center;
        }
        .btn-download:hover {
            background: #2980b9;
            color: white;
            text-decoration: none;
        }
        .btn-delete {
            background: #e74c3c;
            color: white;
            border: none;
            padding: 6px 12px;
            border-radius: 20px;
            cursor: pointer;
            font-size: 12px;
            text-decoration: none;
            display: inline-block;
            flex: 1;
            text-align: center;
        }
        .btn-delete:hover {
            background: #c0392b;
            color: white;
            text-decoration: none;
        }
        .btn-success {
            background: #27ae60;
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 25px;
            cursor: pointer;
        }
        .btn-success:hover {
            background: #219a52;
        }
        .btn-primary {
            background: #3498db;
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 25px;
            cursor: pointer;
        }
        .btn-primary:hover {
            background: #2980b9;
        }
        .upload-area {
            border: 2px dashed #bdc3c7;
            border-radius: 12px;
            padding: 30px;
            text-align: center;
            background: #f8f9fa;
        }
        .flex-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }
        .flex-item {
            flex: 0 0 calc(25% - 20px);
            min-width: 200px;
        }

        /* ============================================= */
        /* استایل چک‌باکس‌های چند ردیفه و کوچک */
        /* ============================================= */
/* ============================================= */
/* استایل چک‌باکس‌های چند ردیفه - نسخه اصلاح شده */
/* ============================================= */
.course-checkbox-container {
    background: #f8f9fa;
    border: 1px solid #dee2e6;
    border-radius: 8px;
    padding: 10px;
    max-height: 280px;
    overflow-y: auto;
    overflow-x: hidden;
}

.course-checkbox-container::-webkit-scrollbar {
    width: 6px;
}

.course-checkbox-container::-webkit-scrollbar-track {
    background: #f1f1f1;
    border-radius: 10px;
}

.course-checkbox-container::-webkit-scrollbar-thumb {
    background: #888;
    border-radius: 10px;
}

.course-checkbox-container::-webkit-scrollbar-thumb:hover {
    background: #555;
}

/* استایل اصلی چک‌باکس‌ها */
.course-checkbox-list {
    display: flex;
    flex-wrap: wrap;
    gap: 4px 8px;
    width: 100%;
    padding: 0;
    margin: 0;
    list-style: none;
}

/* هر آیتم چک‌باکس */
.course-checkbox-list li {
    display: inline-block !important;
    flex: 0 0 calc(25% - 8px) !important;
    min-width: 150px;
    max-width: 100%;
    margin: 0 !important;
    padding: 0 !important;
    list-style: none !important;
    box-sizing: border-box;
}

/* لیبل داخل چک‌باکس */
.course-checkbox-list label {
    display: flex !important;
    align-items: center !important;
    padding: 4px 10px !important;
    margin: 0 !important;
    background: white !important;
    border: 1px solid #e0e0e0 !important;
    border-radius: 4px !important;
    font-size: 12px !important;
    cursor: pointer !important;
    transition: all 0.2s !important;
    white-space: nowrap !important;
    min-height: 28px !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    width: 100% !important;
    box-sizing: border-box !important;
}

.course-checkbox-list label:hover {
    background: #e8f4f8 !important;
    border-color: #3498db !important;
    transform: translateY(-1px);
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

/* ورودی چک‌باکس */
.course-checkbox-list input[type="checkbox"] {
    margin-left: 6px !important;
    margin-right: 2px !important;
    transform: scale(0.8) !important;
    cursor: pointer !important;
    flex-shrink: 0 !important;
    position: relative !important;
    top: 0 !important;
}

/* حالت انتخاب شده */
.course-checkbox-list input[type="checkbox"]:checked + label {
    background: #d4edda !important;
    border-color: #28a745 !important;
    font-weight: bold !important;
}

.course-checkbox-list input[type="checkbox"]:checked + label::after {
    content: " ✓" !important;
    color: #28a745 !important;
    font-weight: bold !important;
}

/* ============================================= */
/* واکنش‌گرا (Responsive) */
/* ============================================= */

/* 4 ستون در صفحه‌های بزرگ */
@media (min-width: 1200px) {
    .course-checkbox-list li {
        flex: 0 0 calc(25% - 8px) !important;
        min-width: 180px;
    }
}

/* 3 ستون در تبلت بزرگ */
@media (max-width: 1199px) and (min-width: 992px) {
    .course-checkbox-list li {
        flex: 0 0 calc(33.33% - 8px) !important;
        min-width: 150px;
    }
}

/* 3 ستون در تبلت */
@media (max-width: 991px) and (min-width: 768px) {
    .course-checkbox-list li {
        flex: 0 0 calc(33.33% - 8px) !important;
        min-width: 130px;
    }
}

/* 2 ستون در موبایل بزرگ */
@media (max-width: 767px) and (min-width: 481px) {
    .course-checkbox-list li {
        flex: 0 0 calc(50% - 8px) !important;
        min-width: 120px;
    }
    .course-checkbox-list label {
        font-size: 11px !important;
        padding: 3px 8px !important;
        min-height: 24px !important;
    }
}

/* 1 ستون در موبایل کوچک */
@media (max-width: 480px) {
    .course-checkbox-list li {
        flex: 0 0 100% !important;
        min-width: 80px;
    }
    .course-checkbox-list label {
        font-size: 11px !important;
        padding: 3px 8px !important;
        min-height: 24px !important;
    }
}
    </style>
    
    <script type="text/javascript">
        // تایید حذف فایل
        function confirmDeleteFile(fileName) {
            return confirm("آیا از حذف فایل '" + fileName + "' اطمینان دارید؟\nاین عملیات غیرقابل بازگشت است.");
        }

        // جستجوی دوره‌ها - نسخه اصلاح شده
        function filterCourses() {
            var input = document.getElementById('txtSearchCourse');
            if (!input) return;

            var filter = input.value.toLowerCase().trim();
            var items = document.querySelectorAll('.course-checkbox-list li');
            var visibleCount = 0;

            items.forEach(function (item) {
                var label = item.querySelector('label');
                if (!label) return;

                var text = label.textContent.toLowerCase();
                var match = text.indexOf(filter) > -1;
                item.style.display = match ? '' : 'none';
                if (match) visibleCount++;
            });

            // به‌روزرسانی تعداد
            var countDisplay = document.getElementById('filteredCount');
            if (countDisplay) {
                countDisplay.textContent = '📊 ' + visibleCount + ' دوره یافت شد';
            }
        }

        // به‌روزرسانی تعداد انتخاب‌ها - نسخه اصلاح شده
        function updateSelectedCount() {
            var checkboxes = document.querySelectorAll('.course-checkbox-list input[type="checkbox"]');
            var checked = 0;
            var total = checkboxes.length;

            checkboxes.forEach(function (cb) {
                if (cb.checked) checked++;
            });

            var countDisplay = document.getElementById('selectedCount');
            if (countDisplay) {
                countDisplay.textContent = '✅ تعداد دوره‌های انتخاب شده: ' + checked + ' از ' + total;
            }
        }

        // انتخاب/عدم انتخاب همه - نسخه اصلاح شده
        function selectAllCourses(select) {
            var checkboxes = document.querySelectorAll('.course-checkbox-list input[type="checkbox"]');
            checkboxes.forEach(function (cb) {
                cb.checked = select;
            });
            updateSelectedCount();
        }

        // رویدادها پس از بارگذاری صفحه
        document.addEventListener('DOMContentLoaded', function () {
            // به‌روزرسانی تعداد
            setTimeout(updateSelectedCount, 100);

            // اضافه کردن رویداد change به چک‌باکس‌ها
            document.querySelectorAll('.course-checkbox-list input[type="checkbox"]').forEach(function (cb) {
                cb.addEventListener('change', updateSelectedCount);
            });
        });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="div_page">
        
        <!-- ======================================== -->
        <!-- فرم اطلاعات پرسنلی -->
        <!-- ======================================== -->
        <div class="card-shadow">
            <div class="card-header-custom">
                👤 اطلاعات پرسنلی
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label style="font-weight: bold; margin-bottom: 5px; display: block;">کد پرسنلی :</label>
                            <asp:Panel ID="PanelSearch" runat="server" DefaultButton="btnSearch">
                                <div style="display: flex; gap: 8px;">
                                    <asp:TextBox ID="txtCodePersonel" runat="server" CssClass="form-control" 
                                        style="flex: 1; padding: 8px; border: 1px solid #ddd; border-radius: 5px;" 
                                        MaxLength="6"></asp:TextBox>
                                    <asp:LinkButton ID="btnSearch" runat="server" 
                                        style="background: #3498db; color: white; padding: 8px 20px; border-radius: 5px; text-decoration: none; border: none; cursor: pointer;" 
                                        OnClick="btnSearch_Click">
                                        🔍 جستجو
                                    </asp:LinkButton>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label style="font-weight: bold; margin-bottom: 5px; display: block;">نام و نام خانوادگی :</label>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" 
                                style="width: 100%; padding: 8px; border: 1px solid #ddd; border-radius: 5px; background: #f5f5f5;" 
                                Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label style="font-weight: bold; margin-bottom: 5px; display: block;">کد ملی :</label>
                            <asp:TextBox ID="txtCodeMeli" runat="server" CssClass="form-control" 
                                style="width: 100%; padding: 8px; border: 1px solid #ddd; border-radius: 5px; background: #f5f5f5;" 
                                Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- ======================================== -->
        <!-- مدارک و ضمایم -->
        <!-- ======================================== -->
        <div class="card-shadow">
            <div class="card-header-custom card-header-success">
                📎 ضمایم و مدارک
            </div>
            <div class="card-body">
                
                <!-- نمایش فایل‌ها -->
                <div class="flex-container">
                    <asp:Repeater ID="RepeaterImages" runat="server" OnItemCommand="RepeaterImages_ItemCommand">
                        <ItemTemplate>
                            <div class="flex-item">
                                <div class="gallery-card">
                                    <%# If(Eval("IsImage"), 
                                        String.Format("<img src='{0}' class='gallery-img' alt='مدرک' />", Eval("Url")), 
                                        String.Format("<div class='file-icon'>📄 {0}</div>", Eval("Extension"))) %>
                                    
                                    <div class="file-name" title="<%# Eval("FileName") %>">
                                        📄 <%# TruncateFileName(Eval("FileName").ToString(), 30) %>
                                    </div>
                                    
                                    <div class="btn-group-custom">
                                        <a href='<%# "trnDownloadImage.aspx?nat=" & Eval("Nat") & "&file=" & Server.UrlEncode(Eval("Name").ToString()) %>' 
                                           class="btn-download">
                                            📥 دانلود
                                        </a>
                                        <asp:LinkButton ID="btnDelete" runat="server" 
                                            CssClass="btn-delete"
                                            CommandName="DeleteFile"
                                            CommandArgument='<%# Eval("FullPath") %>'
                                            OnClientClick='<%# "return confirmDeleteFile(""" & Eval("FileName").ToString().Replace("\", "\\").Replace("""", "\""") & """);" %>'>
                                            🗑️ حذف
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                
                <!-- پیغام خالی بودن لیست -->
                <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
                    <div class="alert alert-info">
                        ℹ️ هیچ فایلی در این پوشه وجود ندارد
                    </div>
                </asp:Panel>
                
                <!-- دکمه دانلود همه فایل‌ها -->
                <div style="text-align: left; margin-top: 20px;">
                    <asp:Button ID="btnDownloadZip" runat="server" Text="📦 دانلود همه فایل‌ها (ZIP)" 
                        CssClass="btn-success" OnClick="btnDownloadZip_Click" />
                </div>
            </div>
        </div>
        
        <!-- ======================================== -->
        <!-- انتخاب چند دوره با چک‌باکس -->
        <!-- ======================================== -->
        <!-- ======================================== -->
<!-- انتخاب چند دوره با چک‌باکس - نسخه اصلاح شده -->
<!-- ======================================== -->
<div class="card-shadow">
    <div class="card-header-custom card-header-warning">
        🎓 انتخاب دوره‌های آموزشی
    </div>
    <div class="card-body">
        <div class="form-group">
            <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 10px; flex-wrap: wrap; gap: 10px;">
                <label style="font-weight: bold; margin: 0;">دوره‌های آموزشی را انتخاب کنید :</label>
                <div style="display: flex; gap: 8px; flex-wrap: wrap;">
                    <button type="button" class="btn-success" style="padding: 4px 15px; font-size: 12px; border: none; border-radius: 15px; cursor: pointer;" onclick="selectAllCourses(true)">
                        ✅ انتخاب همه
                    </button>
                    <button type="button" class="btn-delete" style="padding: 4px 15px; font-size: 12px; border: none; border-radius: 15px; cursor: pointer;" onclick="selectAllCourses(false)">
                        ❌ لغو همه
                    </button>
                </div>
            </div>
            
            <!-- جستجوی دوره‌ها -->
            <input type="text" id="txtSearchCourse" class="search-box" 
                   placeholder="🔍 جستجوی دوره ..." onkeyup="filterCourses()" />
            
            <!-- محفظه چک‌باکس‌ها با اسکرول -->
            <div class="course-checkbox-container">
                <asp:CheckBoxList ID="chkCourses" runat="server" 
                    CssClass="course-checkbox-list"
                    RepeatDirection="Horizontal"
                    RepeatLayout="Flow"
                    CellPadding="0"
                    CellSpacing="0">
                </asp:CheckBoxList>
            </div>
            
            <!-- نمایش تعداد انتخاب‌ها -->
            <div style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px; flex-wrap: wrap; gap: 8px;">
                <div id="selectedCount" class="selected-count">
                    ✅ تعداد دوره‌های انتخاب شده: 0
                </div>
                <div id="filteredCount" class="selected-count" style="background: #fff3cd; border-color: #ffc107;">
                    📊 0 دوره یافت شد
                </div>
            </div>
            
            <!-- پیغام -->
            <asp:Label ID="lblMessage" runat="server" 
                style="display: block; margin-top: 10px; font-size: 12px; color: #666;"></asp:Label>
        </div>
    </div>
</div>
        
        <!-- ======================================== -->
        <!-- بارگذاری فایل جدید -->
        <!-- ======================================== -->
        <div class="card-shadow">
            <div class="card-header-custom">
                📤 بارگذاری مدرک جدید
            </div>
            <div class="card-body">
                <div class="upload-area">
                    <div style="font-size: 48px; margin-bottom: 10px;">📁</div>
                    
                    <div style="margin-bottom: 15px;">
                        <asp:FileUpload ID="FileUploadHealth" runat="server" 
                            style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 5px;" />
                    </div>
                    
                    <div style="display: flex; gap: 10px; justify-content: center; flex-wrap: wrap;">
                        <asp:Button ID="btnUpload" runat="server" Text="📤 ثبت فایل" CssClass="btn-primary" 
                            OnClick="btnUpload_Click" />
                    </div>
                    
                    <asp:Label ID="lblUploadMessage" runat="server" 
                        style="display: block; margin-top: 10px; font-size: 12px; color: #666;"></asp:Label>
                </div>
            </div>
        </div>
        
    </div>
    
</asp:Content>