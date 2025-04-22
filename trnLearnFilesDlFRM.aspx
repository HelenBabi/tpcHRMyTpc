<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnLearnFilesDlFRM.aspx.vb" Inherits="trnLearnFilesDlFRM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:Panel ID="PanelFiles" runat="server" GroupingText="دانلود فایل های آموزشی">
      <div class="row">
			<div class="col-md-12">
                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="auto-style21" DataKeyNames="Id" style="text-align: center" Width="100%" GridLines="Vertical" ForeColor="Black" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="کد" >
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UnitsName" HeaderText="موضوع" />
                    <asp:BoundField DataField="date" HeaderText="تاریخ ">
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="content_name" HeaderText="نام فایل" />
                    <asp:BoundField DataField="sharh" HeaderText="توضیحات">
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="bazdid" HeaderText="تعداد دانلود" >
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود فایل"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload2" runat="server" OnClick="lnkDownload2_Click" style="color: #000000" Text="دانلود فایل صوتی"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload3" runat="server" OnClick="lnkDownload3_Click" style="color: #000000" Text="دانلود فایل ویدیو"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
                </div>
          </div>
           </asp:Panel>
     <br />
            <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                    
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" ValidationGroup="b" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
</asp:Content>

