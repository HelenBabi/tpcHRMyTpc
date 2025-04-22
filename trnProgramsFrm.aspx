<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnProgramsFrm.aspx.vb" Inherits="trnProgramsFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
           <asp:Panel ID="Panel2" runat="server" GroupingText=" زمان اجرای دوره های آموزشی" BorderColor="#9999FF">
               <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                   <AlternatingRowStyle BackColor="#CCCCCC" />
                   <Columns>
                     
                       <asp:BoundField DataField="idd" HeaderText="کد">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="onvan" HeaderText="عنوان">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="vahedha" HeaderText="واحدهای شرکت کننده">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="tarixShuru" HeaderText="تاریخ شروع">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="tarixEtmam" HeaderText="تاریخ اتمام">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="saat" HeaderText="ساعت برگزاری">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="makan" HeaderText="مکان برگزاری">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="modares" HeaderText="مدرس">
                       <HeaderStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                         <asp:TemplateField>
                           <ItemTemplate>
                               <asp:LinkButton ID="lnkDownload" runat="server"  OnClick="lnkDownload_Click" Text="دانلود فایل آموزشی"> </asp:LinkButton>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:BoundField DataField="statValue" HeaderText="وضعیت">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="typevalue" HeaderText="نوع دوره">
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                   </Columns>
                   <FooterStyle BackColor="#CCCCCC" />
                   <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                   <RowStyle BorderStyle="Solid" />
                   <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                   <SortedAscendingCellStyle BackColor="#F1F1F1" />
                   <SortedAscendingHeaderStyle BackColor="#808080" />
                   <SortedDescendingCellStyle BackColor="#CAC9C9" />
                   <SortedDescendingHeaderStyle BackColor="#383838" />
               </asp:GridView>
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

