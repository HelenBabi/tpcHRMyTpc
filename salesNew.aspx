<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="salesNew.aspx.vb" Inherits="salesNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
             <asp:Panel ID="Panel12" runat="server"  >
                  
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                          <br />
                             <div class="row">
              			<div class="col-md-6">
                 <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="گروه آگهی"></asp:Label>
                      <asp:DropDownList ID="DropDownList1" runat="server"   CssClass="btn btn-secondary dropdown-toggle">
                <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                <asp:ListItem Value="1">لوازم خانگی</asp:ListItem>
                <asp:ListItem Value="2">خودرو</asp:ListItem>
                <asp:ListItem Value="3">کامپیوتر</asp:ListItem>
                <asp:ListItem Value="4">مسکن</asp:ListItem>
                <asp:ListItem Value="5">خدمات فنی</asp:ListItem>
                <asp:ListItem Value="6">موبایل</asp:ListItem>
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*اجباری*" ControlToValidate="DropDownList1" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
             		<div class="col-md-6">
            
                         </div>
                      </div>
                         
               
                  <div class="row">
              
			<div class="col-md-6">
                 <asp:Label ID="lblKala" runat="server" CssClass="control-label" Text=" عنوان آگهی"></asp:Label>
                   <asp:TextBox ID="onvan" runat="server"  CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="vald_unitscode" runat="server" 
                    ControlToValidate="onvan" ErrorMessage="عنوان آگهی باید وارد شود" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                               </div>
                     
                         <div class="col-md-6">
                         
                             </div>
                      </div>
                        
                          <br /> 
                 
                  <div class="row">
              <div class="col-md-6">
                      <asp:Label ID="Label1" runat="server"  CssClass="control-label" Text=" تصویرآگهی"></asp:Label>
                   <asp:FileUpload ID="FileUpload1" runat="server" style="text-align: right; background-color: #FFFF66;" Width="400px" Height="25px" />
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="فقط فایل عکس با فرمت jpgمیتوانید بارگزاری نمیایید" ControlToValidate="FileUpload1" ValidationGroup="b" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$" ForeColor="Red"  ></asp:RegularExpressionValidator>
                  </div>
                      
			    <div class="col-md-6">

                  </div>

                      </div>
                          <br />
                             <div class="row">
              <div class="col-md-6">
                   <asp:Label ID="Label7" runat="server"  CssClass="control-label"> توضیحات </asp:Label>
                   <asp:TextBox ID="tozih" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                  
 <asp:RequiredFieldValidator ID="vald_unitscode1" runat="server" 
                    ControlToValidate="tozih" ErrorMessage="توضیحات را باید بنویسید" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                  </div>
                                 
			    <div class="col-md-6">
<asp:CheckBox ID="CheckBox1" runat="server" Text="آیا شماره موبایل در آگهی دیده شود؟" />
                  </div>

                      </div>
                          <br />
                        </asp:Panel>
             
               <br />
            <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="lblSabege" runat="server" CssClass="control-label" Text="سوابق " Visible="false"></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="idDtl" Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="idDtl" HeaderText="#">
                                                        <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="kalaValue" HeaderText="کالا" />
                                                        <asp:BoundField DataField="sizeValue" HeaderText="سایز">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="tarix" HeaderText="تاریخ درخواست" />
                                                        <asp:BoundField HeaderText="تاریخ تحویل" DataField="tarixThavil" Visible="False" />
                                                        <asp:BoundField DataField="statValue" HeaderText="وضعیت" Visible="False" />
                                                             <asp:ButtonField CommandName="del" Text="حذف" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle Font-Size="Medium" ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                </asp:GridView>
                                               </div>
                      </div>

     <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid">
                                     <div class="row">
                   

                         </div>
            </asp:Panel>
                 <br />
                  <div class="row">
                                       	<div class="col-md-12">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                            CellPadding="4" Font-Size="10pt" style="color: #000000; text-align: center;" 
                            TabIndex="12" Width="500px" GridLines="Horizontal">
                            <Columns>
                                <asp:BoundField DataField="idd" HeaderText="#" />
                                <asp:BoundField DataField="mozu" HeaderText="عنوان آگهی" />
                                <asp:ButtonField CommandName="dell" Text="حذف" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#333333" BackColor="White" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#487575" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#275353" />
                        </asp:GridView>
                                               </div>
                      </div>
            </asp:Panel>
    <br />
     
          <br />

             <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                       <asp:Button ID="BtnAdd" runat="server" Text="ثبت" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a"  />
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
     
       
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>

