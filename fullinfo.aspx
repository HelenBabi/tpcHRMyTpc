<%@ Page Title="اطلاعات کامل پرسنل" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false"
    CodeFile="fullinfo.aspx.vb" Inherits="fullinfo" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .info-box {
            background: white;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
        }
        .file-name {
    word-wrap: break-word;
    white-space: normal;
    max-width: 150px;
    line-height: 1.4;
    direction: rtl; /* برای زبان فارسی */
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
        .info-title {
            background: #3498db;
            color: white;
            padding: 10px 15px;
            margin: -20px -20px 20px -20px;
            border-radius: 10px 10px 0 0;
            font-weight: bold;
        }
        .info-title-success {
            background: #27ae60;
        }
        .info-title-warning {
            background: #f39c12;
        }
        .info-title-purple {
            background: #9b59b6;
        }
        .info-row {
            display: flex;
            margin-bottom: 10px;
            padding: 5px 0;
            border-bottom: 1px solid #eee;
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
        .file-item {
            display: inline-block;
            margin: 8px;
            text-align: center;
            cursor: pointer;
            vertical-align: top;
        }
        .file-item img {
            width: 100px;
            height: 100px;
            object-fit: cover;
            border-radius: 8px;
            border: 1px solid #ddd;
        }
        .file-icon {
            width: 100px;
            height: 100px;
            background: #f8f9fa;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 40px;
            border-radius: 8px;
            border: 1px solid #ddd;
        }
        .file-name {
            font-size: 11px;
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            margin-top: 5px;
        }
        .stat-box {
            background: #f0f8ff;
            padding: 15px;
            text-align: center;
            border-radius: 8px;
            margin: 5px;
        }
        .stat-number {
            font-size: 24px;
            font-weight: bold;
            color: #3498db;
        }
        .stat-label {
            font-size: 12px;
            color: #666;
            margin-top: 5px;
        }
        .img-person {
            width: 120px;
            height: 120px;
            border-radius: 50%;
            object-fit: cover;
            border: 3px solid #3498db;
        }
        .table-custom {
            width: 100%;
            border-collapse: collapse;
        }
        .table-custom th {
            background: #f8f9fa;
            padding: 10px;
            border: 1px solid #dee2e6;
            text-align: center;
        }
        .table-custom td {
            padding: 8px;
            border: 1px solid #dee2e6;
            text-align: center;
        }
        
        /* استایل‌های صفحه‌بندی DevExpress */
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
        
        .btn-custom {
            padding: 8px 20px;
            border-radius: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div style="padding: 20px;" dir="rtl">
        
        <!-- دکمه‌های بالا -->
        <div class="mb-3">
            <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Icons/deleted.png" 
                ToolTip="برگشت به صفحه قبل" OnClick="btnBack_Click" Height="30px" />
            
            <asp:LinkButton ID="btnBackToRegistrations" runat="server" 
                CssClass="btn btn-primary ms-2 btn-custom" 
                OnClick="btnBackToRegistrations_Click"
                Visible="false">
                برگشت به مدیریت ثبت‌نام‌ها
            </asp:LinkButton>
            
            <asp:LinkButton ID="btnPrint" runat="server" 
                CssClass="btn btn-success ms-2 btn-custom" 
                OnClientClick="window.print(); return false;">
                چاپ اطلاعات
            </asp:LinkButton>
        </div>
        
        <!-- جستجو -->
        <div class="info-box">
            <div class="info-title">جستجوی پرسنل</div>
            <div class="row">
                <div class="col-md-4">
                    <label>کد پرسنلی:</label>
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <br />
                    <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="btn btn-primary" 
                        OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
        
        <!-- اطلاعات پرسنل -->
        <asp:Panel ID="pnlInfo" runat="server" Visible="false">
            
            <!-- اطلاعات پایه -->
            <div class="info-box">
                <div class="info-title">اطلاعات پایه</div>
                <div class="row">
                    <div class="col-md-3 text-center">
                        <asp:Image ID="imgPerson" runat="server" CssClass="img-person" />
                    </div>
                    <div class="col-md-9">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="info-row">
                                    <div class="info-label">کد پرسنلی:</div>
                                    <div class="info-value"><asp:Label ID="lblCode" runat="server"></asp:Label></div>
                                </div>
                                <div class="info-row">
                                    <div class="info-label">نام و نام خانوادگی:</div>
                                    <div class="info-value"><asp:Label ID="lblName" runat="server"></asp:Label></div>
                                </div>
                                <div class="info-row">
                                    <div class="info-label">کد ملی:</div>
                                    <div class="info-value"><asp:Label ID="lblCodeMeli" runat="server"></asp:Label></div>
                                </div>
                                <div class="info-row">
                                    <div class="info-label">شماره بیمه:</div>
                                    <div class="info-value"><asp:Label ID="lblBime" runat="server"></asp:Label></div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="info-row">
                                    <div class="info-label">تاریخ استخدام:</div>
                                    <div class="info-value"><asp:Label ID="lblDateEmploy" runat="server"></asp:Label></div>
                                </div>
                                <div class="info-row">
                                    <div class="info-label">عنوان شغل:</div>
                                    <div class="info-value"><asp:Label ID="lblPost" runat="server"></asp:Label></div>
                                </div>
                                <div class="info-row">
                                    <div class="info-label">تحصیلات:</div>
                                    <div class="info-value"><asp:Label ID="lblMadrak" runat="server"></asp:Label></div>
                                </div>
                                <div class="info-row">
                                    <div class="info-label">رشته تحصیلی:</div>
                                    <div class="info-value"><asp:Label ID="lblReshte" runat="server"></asp:Label></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- سوابق آموزشی با DevExpress GridView -->
            <div class="info-box">
                <div class="info-title info-title-success">سوابق آموزشی</div>
                <dx:ASPxGridView ID="grdTraining" runat="server" AutoGenerateColumns="False" 
                    KeyFieldName="idd" Width="100%" Font-Size="12px"
                    OnPageIndexChanged="grdTraining_PageIndexChanged"
                    OnDataBinding="grdTraining_DataBinding"
                    EnableCallBacks="True">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="عنوان دوره" FieldName="onvan" VisibleIndex="0" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="dorehayeGozrande" Caption="دوره‌های گذرانده" VisibleIndex="1" Width="20%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" VisibleIndex="2" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra" VisibleIndex="3" Width="12%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="tarixPayan" VisibleIndex="5" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ" VisibleIndex="6" Width="8%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="saat" Caption="ساعت" VisibleIndex="7" Width="8%">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#27ae60" ForeColor="White" />
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
                    <SettingsText EmptyDataRow="هیچ سابقه آموزشی ثبت نشده است" />
                </dx:ASPxGridView>
            </div>
            
            <!-- مدارک آپلود شده -->
            <div class="info-box">
                <div class="info-title info-title-warning">مدارک آپلود شده</div>
                <asp:Repeater ID="rptFiles" runat="server">
                    <ItemTemplate>
                        <div class="file-item"  onclick='window.open("<%# Eval("Url") %>","_blank")' >
                            <%# If(Eval("IsImage").ToString() = "True", 
                                String.Format("<img src='{0}' />", Eval("Url")), 
                                "<div class='file-icon'>📄</div>") %>
                            <div class="file-name"><%# CutText(Eval("FileName").ToString(), 50) %></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoFiles" runat="server" Text="هیچ مدرکی آپلود نشده است" 
                    CssClass="text-muted" Visible="false"></asp:Label>
            </div>
            
            <!-- آمار سوابق -->
            <div class="info-box">
                <div class="info-title info-title-purple">آمار سوابق</div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="stat-box">
                            <div class="stat-number"><asp:Label ID="lblSalKar" runat="server" Text="0"></asp:Label></div>
                            <div class="stat-label">سال سابقه کار</div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stat-box">
                            <div class="stat-number"><asp:Label ID="lblMahKar" runat="server" Text="0"></asp:Label></div>
                            <div class="stat-label">ماه سابقه کار</div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stat-box">
                            <div class="stat-number"><asp:Label ID="lblRuzKar" runat="server" Text="0"></asp:Label></div>
                            <div class="stat-label">روز سابقه کار</div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stat-box">
                            <div class="stat-number"><asp:Label ID="lblSalSakhti" runat="server" Text="0"></asp:Label></div>
                            <div class="stat-label">سال سختی کار</div>
                        </div>
                    </div>
                </div>
            </div>
            
        </asp:Panel>
        
    </div>
    
</asp:Content>