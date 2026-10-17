<%@ Page Title="سوابق آموزشی" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDoreFrm.aspx.vb" Inherits="trnDoreFrm" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .page-container {
            background: #f5f7fa;
            padding: 25px;
            min-height: 100vh;
        }
        .info-card {
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
        .card-header-info {
            background: linear-gradient(135deg, #17a2b8 0%, #138496 100%);
        }
        .card-header-warning {
            background: linear-gradient(135deg, #f39c12 0%, #e67e22 100%);
        }
        .card-body {
            padding: 20px;
        }
        .info-row {
            margin-bottom: 15px;
        }
        .info-label {
            font-weight: bold;
            color: #555;
            margin-bottom: 5px;
            display: block;
        }
        .info-value {
            color: #333;
            padding: 8px 12px;
            background: #f8f9fa;
            border-radius: 8px;
            border: 1px solid #e9ecef;
        }
        .file-gallery {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            margin-top: 15px;
        }
        .file-item {
            width: 120px;
            text-align: center;
            cursor: pointer;
            transition: transform 0.2s;
        }
        .file-item:hover {
            transform: translateY(-5px);
        }
        .file-item img {
            width: 120px;
            height: 120px;
            object-fit: cover;
            border-radius: 10px;
            border: 2px solid #e9ecef;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        .file-icon {
            width: 120px;
            height: 120px;
            background: #f8f9fa;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 48px;
            border-radius: 10px;
            border: 2px solid #e9ecef;
        }
        .file-name {
            font-size: 11px;
            margin-top: 8px;
            color: #555;
            word-break: break-all;
            line-height: 1.3;
        }
        .btn-custom {
            padding: 8px 25px;
            border-radius: 25px;
            font-size: 14px;
        }
        .empty-files {
            text-align: center;
            padding: 30px;
            background: #f8f9fa;
            border-radius: 10px;
            color: #999;
        }
        .stat-badge {
            display: inline-block;
            padding: 8px 15px;
            background: #e8f4f8;
            color: #3498db;
            border-radius: 25px;
            font-size: 13px;
            margin: 5px;
        }
        .file-item {
    display: flex;
    align-items: center;
    padding: 8px;
    border-bottom: 1px solid #eee;
    max-width: 100%;
}

.file-name {
    flex: 1;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    direction: rtl;
    padding: 0 10px;
}

/* برای نمایش کامل در هاور */
.file-item:hover .file-name {
    white-space: normal;
    word-wrap: break-word;
    background: #f5f5f5;
    position: relative;
    z-index: 10;
}
        
        /* استایل‌های صفحه‌بندی */
        .dxgvPagerButton {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            color: #333;
            padding: 5px 12px;
            margin: 0 2px;
            border-radius: 4px;
            cursor: pointer;
        }
        .dxgvPagerButton:hover {
            background-color: #e9ecef;
        }
        .dxgvPagerButton.dxgvPagerActiveButton {
            background-color: #3498db;
            color: white;
            border-color: #3498db;
        }
        
        /* استایل مخصوص چاپ */
        @media print {
            .no-print {
                display: none !important;
            }
            .page-container {
                background: white;
                padding: 0;
                margin: 0;
            }
            .info-card {
                box-shadow: none;
                border: 1px solid #ddd;
                page-break-inside: avoid;
            }
            .card-header-custom {
                background: #3498db !important;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }
            .stat-badge {
                border: 1px solid #ddd;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }
            .file-item {
                page-break-inside: avoid;

                .file-item {
    display: flex;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #eee;
    cursor: pointer;
    transition: background-color 0.2s;
    direction: rtl; /* راست‌چین */
}

.file-item:hover {
    background-color: #f8f9fa;
}

.file-name {
    flex: 1;
    margin-right: 10px;
    font-size: 14px;
    color: #333;
    white-space: nowrap; /* جلوگیری از شکستن خط */
    overflow: hidden;
    text-overflow: ellipsis; /* نمایش ... در انتها */
    direction: rtl; /* راست‌چین برای فارسی */
    text-align: right; /* تراز راست */
    unicode-bidi: embed; /* برای پشتیبانی بهتر از متن فارسی */
}

/* برای نمایش کامل در هاور */
.file-item:hover .file-name {
    white-space: normal;
    word-wrap: break-word;
    background-color: #f5f5f5;
    padding: 5px;
    border-radius: 4px;
    position: relative;
    z-index: 10;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}

.file-icon {
    font-size: 24px;
    margin-left: 10px;
}

.file-item img {
    width: 40px;
    height: 40px;
    object-fit: cover;
    border-radius: 4px;
    margin-left: 10px;
}
            
/* استایل گالری فایل‌ها - نمایش افقی */
.file-gallery {
    display: flex;
    flex-wrap: wrap;
    gap: 15px;
    padding: 10px 0;
}

.file-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 130px;
    padding: 12px 10px;
    background: #f8f9fa;
    border-radius: 10px;
    border: 2px solid #e9ecef;
    cursor: pointer;
    transition: all 0.3s ease;
    text-align: center;
}

.file-item:hover {
    transform: translateY(-5px);
    box-shadow: 0 8px 25px rgba(0,0,0,0.15);
    border-color: #27ae60;
    background: #ffffff;
}

.file-item img {
    width: 100px;
    height: 100px;
    object-fit: cover;
    border-radius: 8px;
    border: 1px solid #dee2e6;
}

.file-icon {
    width: 100px;
    height: 100px;
    background: #ffffff;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 48px;
    border-radius: 8px;
    border: 1px solid #dee2e6;
}

.file-name {
    width: 100%;
    margin-top: 8px;
    font-size: 12px;
    color: #333;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    direction: rtl;
    text-align: center;
    padding: 2px 5px;
    border-radius: 4px;
    transition: all 0.3s ease;
}

/* نمایش کامل نام فایل در هاور */
.file-item:hover .file-name {
    white-space: normal;
    word-wrap: break-word;
    background: #27ae60;
    color: #ffffff;
    padding: 5px 8px;
    box-shadow: 0 2px 10px rgba(39, 174, 96, 0.3);
    position: relative;
    z-index: 10;
    font-weight: bold;
    font-size: 13px;
}
            body {
                padding: 20px;
            }
        
    </style>
    
    <script type="text/javascript">
        function PrintPanel() {
            // مخفی کردن دکمه‌ها
            var buttons = document.querySelectorAll('.no-print');
            for (var i = 0; i < buttons.length; i++) {
                buttons[i].style.display = 'none';
            }

            // چاپ صفحه
            window.print();

            // نمایش مجدد دکمه‌ها
            setTimeout(function () {
                for (var i = 0; i < buttons.length; i++) {
                    buttons[i].style.display = '';
                }
            }, 1000);

            return false;
        }

        function openFile(url) {
            window.open(url, '_blank');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="page-container" dir="rtl">
        
        <!-- دکمه‌های بالا (عدم نمایش در چاپ) -->
        <div class="mb-3 no-print" style="display: flex; justify-content: space-between; align-items: center; gap: 10px; flex-wrap: wrap;">
            <div style="display: flex; gap: 10px;">
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary btn-custom" 
                    Text="🖨️ چاپ سوابق آموزشی" OnClientClick="return PrintPanel();" />
            </div>
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-warning btn-custom" 
                Text="🏠 بازگشت به صفحه اصلی" OnClick="btnBack_Click" />
        </div>
        
        <!-- پنل اصلی سوابق آموزشی -->
        <asp:Panel ID="Panel2" runat="server">
            
            <!-- هدر گزارش (فقط در چاپ نمایش داده می‌شود) -->
            <div style="text-align: center; margin-bottom: 30px; display: none;" class="print-header">
                <h2 style="color: #2c3e50;">گزارش سوابق آموزشی</h2>
                <p style="color: #7f8c8d;">تاریخ چاپ: <span id="printDate"></span></p>
            </div>
            
            <!-- اطلاعات پایه پرسنل -->
            <div class="info-card">
                <div class="card-header-custom">
                    📋 اطلاعات پایه پرسنل
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="info-row">
                                <div class="info-label">🎓 مدرک تحصیلی</div>
                                <div class="info-value">
                                    <asp:TextBox ID="madrakTxt" runat="server" CssClass="form-control" 
                                        style="border: none; background: #f8f9fa; padding: 8px;" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="info-row">
                                <div class="info-label">📚 رشته تحصیلی</div>
                                <div class="info-value">
                                    <asp:TextBox ID="reshteTxt" runat="server" CssClass="form-control" 
                                        style="border: none; background: #f8f9fa; padding: 8px;" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="info-row">
                                <div class="info-label">💼 سمت سازمانی</div>
                                <div class="info-value">
                                    <asp:TextBox ID="sematTxt" runat="server" CssClass="form-control" 
                                        style="border: none; background: #f8f9fa; padding: 8px;" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- جدول سوابق آموزشی -->
            <div class="info-card">
                <div class="card-header-custom card-header-info">
                    📖 سوابق آموزشی
                </div>
                <div class="card-body" style="overflow-x: auto;">
                    <dx:ASPxGridView ID="trnGrid" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" Width="100%" Font-Size="12px"
                        OnPageIndexChanged="trnGrid_PageIndexChanged"
                        OnDataBinding="trnGrid_DataBinding"
                        EnableCallBacks="True">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="عنوان دوره" FieldName="onvan" VisibleIndex="0" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="dorehayeGozrande" Caption="دوره‌های گذرانده" VisibleIndex="1" Width="20%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" VisibleIndex="2" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra" VisibleIndex="3" Width="12%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" VisibleIndex="4" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="tarixPayan" VisibleIndex="5" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ" VisibleIndex="6" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="saat" Caption="ساعت" VisibleIndex="7" Width="8%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="هماهنگی اجرا" FieldName="hamahangiEjra" VisibleIndex="8" Width="12%">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#3498db" ForeColor="White" />
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <SettingsPager PageSize="10" 
                                       ShowNumericButtons="True"
                                       NumericButtonCount="10"
                                       Mode="ShowPager"
                                       AlwaysShowPager="True">
                            <Summary Text="صفحه {0} از {1} ({2} رکورد)" />
                            <AllButton Text="همه" />
                            <FirstPageButton Text="ابتدا" />
                            <LastPageButton Text="انتها" />
                            <NextPageButton Text="بعدی" />
                            <PrevPageButton Text="قبلی" />
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                        <SettingsText EmptyDataRow="هیچ رکوردی یافت نشد" />
                    </dx:ASPxGridView>
                </div>
            </div>
            
            <!-- مدارک و گواهی‌های آپلود شده (فقط نمایش در صفحه، نه در چاپ) -->
<!-- مدارک و گواهی‌های آپلود شده -->
<!-- مدارک و گواهی‌های آپلود شده -->
<div class="info-card no-print">
    <div class="card-header-custom card-header-success">
        📎 مدارک و گواهی‌های آپلود شده
    </div>
    <div class="card-body">
        <div class="file-gallery">
            <asp:Repeater ID="rptCertificates" runat="server">
                <ItemTemplate>
                    <div class="file-item" onclick='window.open("<%# Eval("Url") %>","_blank")'>
                        <%# If(Eval("IsImage").ToString() = "True", 
                            String.Format("<img src='{0}' alt='مدرک' />", Eval("Url")), 
                            String.Format("<div class='file-icon'>📄</div>")) %>
                        <div class="file-name" data-fullname="<%# Eval("FileName") %>">
                            <%# TruncateFileName(Eval("FileName").ToString(), 20) %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:Panel ID="pnlNoFiles" runat="server" Visible="false">
            <div class="empty-files">
                <span style="font-size: 48px;">📂</span>
                <p style="margin-top: 10px;">هیچ مدرکی برای این پرسنل آپلود نشده است</p>
            </div>
        </asp:Panel>
    </div>
</div>
            
            <!-- آمار دوره‌ها -->
            <div class="info-card">
                <div class="card-header-custom card-header-warning">
                    📊 آمار دوره‌های آموزشی
                </div>
                <div class="card-body">
                    <div style="text-align: center;">
                        <span class="stat-badge">
                            📋 تعداد کل دوره‌ها: <asp:Label ID="lblTotalCount" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </span>
                        <span class="stat-badge">
                            🕒 مجموع ساعت دوره‌ها: <asp:Label ID="lblTotalHours" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </span>
                        <span class="stat-badge">
                            📅 مجموع روزهای دوره: <asp:Label ID="lblTotalDays" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </span>
                        <span class="stat-badge">
                            📎 تعداد مدارک: <asp:Label ID="lblTotalFiles" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
            
        </asp:Panel>
        
    </div>
    
    <script type="text/javascript">
        // تنظیم تاریخ چاپ
        var today = new Date();
        var date = today.toLocaleDateString('fa-IR');
        var printDateSpan = document.getElementById('printDate');
        if (printDateSpan) {
            printDateSpan.innerHTML = date;
        }
    </script>
    
</asp:Content>