<%@ Page Title="تایید کارشناس بهداشت" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="HealthExpertApprove.aspx.vb" Inherits="HealthExpertApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .exm-container { max-width: 1000px; margin: 30px auto; padding: 25px 30px; background: #ffffff; border-radius: 12px; box-shadow: 0 2px 15px rgba(0,0,0,0.08); direction: rtl; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .exm-title { font-size: 22px; font-weight: 700; color: #2c3e50; text-align: center; margin-bottom: 25px; padding-bottom: 15px; border-bottom: 2px solid #e9ecef; }
        .exm-table-responsive { overflow-x: auto; }
        .exm-table { width: 100%; border-collapse: collapse; font-size: 13px; text-align: right; }
        .exm-table th { background: #2c3e50; color: #ffffff; padding: 10px 12px; white-space: nowrap; font-weight: 600; }
        .exm-table td { padding: 10px 12px; border-bottom: 1px solid #e9ecef; vertical-align: middle; }
        .exm-table tr:hover { background: #f8f9fa; }
        .exm-status-pending { background: #fff3cd; color: #856404; padding: 3px 12px; border-radius: 12px; display: inline-block; font-weight: 600; }
        .exm-status-approved { background: #d4edda; color: #155724; padding: 3px 12px; border-radius: 12px; display: inline-block; font-weight: 600; }
        .exm-status-rejected { background: #f8d7da; color: #721c24; padding: 3px 12px; border-radius: 12px; display: inline-block; font-weight: 600; }
        .exm-btn { padding: 6px 18px; border: none; border-radius: 20px; font-size: 12px; font-weight: 600; cursor: pointer; transition: all 0.3s; margin: 2px; }
        .exm-btn:hover { transform: scale(1.05); }
        .exm-btn-approve { background: #27ae60; color: #ffffff; }
        .exm-btn-approve:hover { background: #219a52; }
        .exm-btn-reject { background: #e74c3c; color: #ffffff; }
        .exm-btn-reject:hover { background: #c0392b; }
        .exm-btn-back { background: #9b59b6; color: #ffffff; padding: 10px 30px; font-size: 14px; }
        .exm-btn-back:hover { background: #8e44ad; }
        .exm-comment { width: 200px; padding: 8px 12px; border: 1.5px solid #e0e0e0; border-radius: 6px; font-size: 13px; font-family: inherit; min-height: 35px; }
        .exm-alert { padding: 12px 18px; border-radius: 8px; margin: 15px 0; font-size: 14px; display: flex; align-items: center; gap: 10px; }
        .exm-alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .exm-alert-danger { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .exm-alert-warning { background: #fff3cd; color: #856404; border: 1px solid #ffc107; }
        .exm-alert-info { background: #d1ecf1; color: #0c5460; border: 1px solid #bee5eb; }
        .exm-pagination { padding: 10px 0; text-align: center; }
        .exm-pagination a, .exm-pagination span { padding: 5px 12px; border: 1px solid #ddd; border-radius: 5px; margin: 0 2px; text-decoration: none; color: #2c3e50; }
        .exm-pagination a:hover { background: #3498db; color: #ffffff; border-color: #3498db; }
        .exm-pagination span { background: #3498db; color: #ffffff; border-color: #3498db; }
        .exm-actions { display: flex; gap: 5px; flex-wrap: wrap; align-items: center; }
        @media (max-width: 768px) { .exm-container { padding: 15px; margin: 10px; } .exm-table { font-size: 11px; } .exm-table th, .exm-table td { padding: 5px 8px; } .exm-comment { width: 100%; min-width: unset; } .exm-actions { flex-direction: column; } .exm-btn { width: 100%; text-align: center; } }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="exm-container">
        <div class="exm-title">✅ تایید نهایی توسط کارشناس بهداشت</div>
        
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class='exm-alert exm-alert-<%# GetAlertClass() %>'>
                <span class="icon"><asp:Label ID="lblMessageIcon" runat="server" /></span>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
        </asp:Panel>
        
        <div class="exm-table-responsive">
            <asp:GridView ID="gvRequests" runat="server" 
                AutoGenerateColumns="False" 
                CssClass="exm-table" 
                AllowPaging="True" 
                PageSize="15"
                OnPageIndexChanging="gvRequests_PageIndexChanging"
                OnRowCommand="gvRequests_RowCommand"
                OnRowDataBound="gvRequests_RowDataBound"
                EmptyDataText="هیچ درخواستی برای تایید وجود ندارد"
                DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="ردیف" ItemStyle-Width="40" />
                    <asp:BoundField DataField="PersianDate" HeaderText="تاریخ معاینه" ItemStyle-Width="80" />
                    <asp:BoundField DataField="FullName" HeaderText="پرسنل" ItemStyle-Width="120" />
                    <asp:BoundField DataField="UnitName" HeaderText="واحد" ItemStyle-Width="120" />
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-Width="100">
                        <ItemTemplate>
                            <span class="exm-status-pending">
                                <%# Eval("StatusText") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="عملیات" ItemStyle-Width="350">
                        <ItemTemplate>
                            <div class="exm-actions">
                                <asp:TextBox ID="txtComment" runat="server" CssClass="exm-comment" 
                                    placeholder="توضیحات..." TextMode="MultiLine" Rows="2" />
                                <asp:Button ID="btnApprove" runat="server" Text="✅ تایید نهایی" CssClass="exm-btn exm-btn-approve" 
                                    CommandName="Approve" CommandArgument='<%# Container.DisplayIndex %>' />
                                <asp:Button ID="btnReject" runat="server" Text="❌ رد" CssClass="exm-btn exm-btn-reject" 
                                    CommandName="Reject" CommandArgument='<%# Container.DisplayIndex %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="exm-pagination" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        
        <div style="margin-top: 20px; text-align: center;">
            <asp:Button ID="btnBack" runat="server" Text="🏠 بازگشت به صفحه اصلی" CssClass="exm-btn exm-btn-back" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>