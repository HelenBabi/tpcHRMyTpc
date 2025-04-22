<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnLearnFilesFrm.aspx.vb" Inherits="trnLearnFilesFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
			<div class="col-md-6">
                <asp:Panel ID="Panel2" runat="server" DefaultButton="ImageButton1" >
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="جستجو در همه موضوعات"></asp:Label>
          <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" placeholder="برای جستجو قسمتی از نام موضوع مورد نظر را بنویسید و Enter بزنید " ></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
                    </asp:Panel>
                </div>
             			<div class="col-md-6">

                </div>
               </div>
    <asp:Panel ID="PanelFiles" runat="server" GroupingText="نتیجه جستجو" Visible="False">
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
                    <asp:BoundField DataField="bazdid" HeaderText="تعداد دانلود">
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload4" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود فایل "></asp:LinkButton>
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
    <div class="row">
        <div class="col-md-12">
              <asp:Panel ID="Panel1" runat="server" GroupingText="فایل های آموزشی">
      <div class="row">
			<div class="col-md-12">
              
                <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Names="A1Tahoma" Font-Overline="False" Font-Size="Small" GridLines="Both" Height="60px" RepeatColumns="8" RepeatDirection="Horizontal" style="text-align: center; margin-left: 1px; background-color: #339966;" Width="100%">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BorderStyle="Outset" ForeColor="#000066" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="96px" ImageUrl="~/Images/folder-close-icon.png" PostBackUrl='<%# String.Format("trnLearnFilesDlFRM.aspx?id={0}", Eval("UnitsCode"))%>' style="background-color: #339966" Width="96px" />
            <br />
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Size="13pt" style="color: #FFFFFF; background-color: #339966;" Text='<%# Eval("UnitsName")%>' Width="110px"></asp:HyperLink>
            <br />
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>

                </div>
         
          </div>
               </asp:Panel>

        </div>

    </div>
    
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

