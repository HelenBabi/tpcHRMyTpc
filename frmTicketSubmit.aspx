<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmTicketSubmit.aspx.vb" Inherits="frmTicketSubmit" %>

<%@ Register namespace="DevExpress.Web.ASPxGridView" tagprefix="ASPxGridView" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

   <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   

               <table class="style1">
                   <tr>
                       <td>
               <asp:Label ID="lblCnt" runat="server" 
         style="color: #666666; font-weight: 700;" 
                               ToolTip="لیست انتظار"></asp:Label>
            

                       </td>
                   </tr>
                   <tr>
                       <td>
                           &nbsp;</td>
                   </tr>
               </table>
            
 
         <div class="div_grid">
                                     <asp:Panel ID="Panel1" runat="server" Direction="RightToLeft" 
                                         GroupingText="کامپیوتر هایی که شما دسترسی دارید" style="color: #3366FF">
                                         <asp:GridView ID="GridUserSystem" 
    runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" CssClass="txt_ r5" 
    Font-Size="10pt" style="color: #000000" 
                            TabIndex="12" Width="900px" ForeColor="#333333" GridLines="None" 
                                             ToolTip="اگر به بیش از یک کامپیوتر دسترسی دارید کامپیوتری را انتخاب کنید که می خواهید سرویس روی آن انجام شود">
                                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                             <Columns>
                                                 <asp:BoundField DataField="SystemCode" HeaderText="شماره اموال" />
                                                 <asp:BoundField DataField="systemcode" HeaderText="شماره اموال" 
                                    Visible="False" />
                                                 <asp:BoundField DataField="BuildingName" HeaderText="ساختمان" />
                                                 <asp:BoundField DataField="UnitsName" HeaderText="واحد" />
                                                 <asp:BoundField DataField="SystemRoom" HeaderText="اتاق" />
                                                 <asp:BoundField DataField="nam" HeaderText="در تحویل" />
                                                 <asp:BoundField DataField="SystemPhone" HeaderText="تلفن" />
                                                 <asp:ButtonField CommandName="dell" Text="انتخاب" />
                                             </Columns>
                                             <EditRowStyle BackColor="#999999" />
                                             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                             <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                             <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                             <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                             <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                             <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                         </asp:GridView>
                                     </asp:Panel>
                  
                
          </div>
                
                <br />
        <div class="div_line">
            <div class="div_nev">
                شماره اموال</div>
            <div class="div_txt">
                        

                <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RequiredFieldValidator ID="vald_WorkHdrcode2" runat="server" 
                            ControlToValidate="txtSystemCode" ErrorMessage="*" ForeColor="Red" 
                            ValidationGroup="a"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtSystemCode" runat="server" CssClass="txt_ r5" 
                            Enabled="False" ToolTip="شماره اموال سیستمی که درخواست روی آن انجام می شود" 
                            Width="100px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        

            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                تلفن 
                داخلی</div>
            <div class="div_txt">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtTelfon" runat="server" CssClass="txt_ r5" 
                            ToolTip="اگر شماره تلفن داخلی تان تغییر کرده اینجا وارد کنید یا از پروفایل تغییر دهید" 
                            Width="170px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="div_val">
                <asp:RequiredFieldValidator ID="vald_WorkHdrcode1" runat="server" 
                    ControlToValidate="txtTelfon" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="div_nev">
                : نوع سرویس
            </div>
            <div class="div_txt">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="CmbWorkeType" runat="server" AutoPostBack="True" 
                        CssClass="txt_ r5" Height="30px" TabIndex="1" Width="170px" 
                        
                        ToolTip="نوع سرویس مشخص می کند سرویس شما را کدام قسمت واحد کامپیوتر انجام دهد " 
                        Font-Bold="True">
                        <asp:ListItem Value="0">نوع سرویس را  انتخاب کنید</asp:ListItem>
                        <asp:ListItem Value="2">نرم افزار</asp:ListItem>
                        <asp:ListItem Value="1">سخت افزار</asp:ListItem>
                        <asp:ListItem Value="5">شبکه</asp:ListItem>
                        <asp:ListItem Value="6">کابل کشی</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                        </asp:UpdatePanel>
            </div>
                    <div class="div_val">
            </div>
        </div>
                
             <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                رمز ورود ویندوز<br />
            </div>
            <div class="div_txt">
                        <asp:TextBox ID="txtRamz" runat="server" CssClass="txt_ r5" 
                            ToolTip="رمزی که با آن وارد ویندوز می شوید را بنویسید" 
                            Width="170px"></asp:TextBox>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>
                <table class="style1">
             
                    <tr>
                        <td style="color: #3366FF">
                            &nbsp;</td>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="color: #3366FF">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                        <td style="text-align: center; color: #000000;">
    
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel2" runat="server" 
                                        GroupingText=":توضیحات را در این قسمت بنویسید">
                                        <asp:TextBox ID="txt_ans" runat="server" CssClass="txt_ r5" Height="42px" 
                                            TextMode="MultiLine" ToolTip="توضیحات خورد را در این قسمت بنویسید" 
                                            Width="900px"></asp:TextBox>
                                    </asp:Panel>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                        <td style="text-align: center; color: #000000;" dir="rtl">
    
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblMohem" runat="server" 
                                        style="font-weight: 700; color: #FF0000" Visible="False"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                        <td style="text-align: center; color: #000000;">
    
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Panel ID="Panel3" runat="server" 
                                        GroupingText=":اطلاعات مهم سیستم را در این قسمت بنویسید" Visible="False">
                                        <asp:TextBox ID="txt_mohem" runat="server" CssClass="txt_ r5" Height="37px" 
                                            TextMode="MultiLine" 
                                            ToolTip="اگر اطلاعات مهم دارید در این قسمت توضیح دهید تا قبل از هر عملیاتی پشتیبان گیری شود" 
                                            Visible="False" Width="900px"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                        <td style="text-align: center; color: #000000;">
    
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server" 
                                        GroupingText=":مشکل سیستم را در این قسمت بنویسید" Visible="False">
                                        <asp:TextBox ID="txt_Iradat" runat="server" CssClass="txt_ r5" Height="37px" 
                                            TextMode="MultiLine" 
                                            ToolTip="لطفا شرح کنید سیستم چه مشکلی دارد که به کارگاه ارسال می کنید" 
                                            Visible="False" Width="900px"></asp:TextBox>
                                    </asp:Panel>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                        <td style="text-align: center; color: #000000;">
    
                            &nbsp;</td>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                        <td style="text-align: center; color: #000000;">
    
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkIAgree" runat="server" 
                                        ToolTip="برای ذخیره و ارسال باید این تعهد نامه را قبول کنید" 
                                        Visible="False" Width="850px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: center">
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                
                
                            &nbsp;</td>
                        <td>
                
                
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="style3">
                                        &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/Icons/bargasht.png" ToolTip="برگشت به کارتابل" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icons/save.png" 
                                            ToolTip="اگر ذخیره را کلیک کنید درخواست شما به واحد کامپیوتر ارسال می شود و در کارتابل خودتان نیز مشاهده می شود" 
                                            ValidationGroup="a" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                
                
                        </td>
                        <td>
                
                
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                
                
                            &nbsp;</td>
                        <td>
                
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                
                        </td>
                        <td>
                
                
                            &nbsp;</td>
                    </tr>
       </table>
                <br />
                <br />
                
                
                <br />
             
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            color: #CC66FF;
        }
        </style>
</asp:Content>


