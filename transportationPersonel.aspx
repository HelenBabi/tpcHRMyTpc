<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="transportationPersonel.aspx.vb" Inherits="transportationPersonel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

             <asp:Panel ID="Panel12" runat="server"  >
                  <div class="row">
			<div class="col-md-12">
                <h5 style="color: #FF0000">لطفا سرویس هایی که اغلب استفاده می نمایید را انتخاب و دکمه افزودن را کلیک نمایید</h5>
                               </div>
          </div>
            <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="سرویس هایی که من استفاده می کنم" Visible="false"></asp:Label>
                                               <asp:GridView ID="GridView1" runat="server"  CssClass="frm-submit" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idDtl" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="masir" HeaderText="سرویس" />
                                            <asp:BoundField DataField="typevalue" HeaderText="روزکار/نوبتکار" />
                                            <asp:BoundField DataField="citySiteTypeValue" HeaderText="نوع " />
                                            <asp:ButtonField CommandName="del" Text="حذف" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                   <RowStyle ForeColor="#000066" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                               </div>
                      </div>
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                             <div class="row">
              
			<div class="col-md-12">
                 <asp:Label ID="Label2" runat="server" class="form-control" Text="پرسنل"></asp:Label>
                      <asp:DropDownList ID="cmbRuzShabKar" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   <asp:ListItem Value="0">انتخاب</asp:ListItem>
                                   <asp:ListItem Value="1">روزکار</asp:ListItem>
                                   <asp:ListItem Value="2">نوبتکار</asp:ListItem>
                                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbRuzShabKar" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
             	
                      </div>
                         
                 <br />
                  <div class="row">
              
			<div class="col-md-12">
                 <asp:Label ID="Label1" runat="server" class="form-control" Text=" سرویس روزکار از شهر به پتروشیمی صبح"></asp:Label>
                      <asp:DropDownList ID="citytToSiteNoRuzkar" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                               <asp:Button ID="BtnAdd" runat="server" Text="افزودن" class="btn btn-secondary dropdown-toggle" ValidationGroup="a"  />
                               </div>
             	
                      </div>
                         
                 <br />
                  <div class="row">
                 	<div class="col-md-12">
  <asp:Label ID="Label3" runat="server"  class="form-control" Text="سرویس روزکار از پتروشیمی به شهر عصرساعت 16"></asp:Label>
                    <asp:DropDownList ID="siteToCityNoRuzkar16" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                           <asp:Button ID="Button1" runat="server" Text="افزودن" class="btn btn-secondary dropdown-toggle" ValidationGroup="a" />
                                    </div>
                      </div>
                          
                 <br />
                   
                   <div class="row">
              
			<div class="col-md-12">
                 <asp:Label ID="labelcitytToSiteNo19" runat="server"   class="form-control" Text=" سرویس نوبتکار از شهر به پتروشیمی عصر ساعت19"></asp:Label>
                      <asp:DropDownList ID="citytToSiteNoShift19" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                  <asp:Button ID="Button2" runat="server" Text="افزودن" class="btn btn-secondary dropdown-toggle" ValidationGroup="a" />
                                </div>
                   
                      </div>
                 <br />
                                     <div class="row">
                                       	<div class="col-md-12">
  <asp:Label ID="Label6" runat="server"  class="form-control" Text="سرویس نوبتکار از پتروشیمی به شهر صبح ساعت 8"></asp:Label>
                    <asp:DropDownList ID="siteToCityNoShift8" runat="server" class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                  <asp:Button ID="Button3" runat="server" Text="افزودن" class="btn btn-secondary dropdown-toggle" ValidationGroup="a" />
                </div>

                         </div>
                          <br />
                        </asp:Panel>
                 <br />
                 <asp:Panel ID="Panel4" runat="server" BorderColor="Black" BorderStyle="Dashed">
                   <div class="row">
			<div class="col-md-4">
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text=" از سرویس استفاده نمی کنم " TextAlign="Left" />
                </div>
                       <div class="col-md-4">
                </div>
                       <div class="col-md-4">
                </div>
                       </div>
            </asp:Panel>
               <br />
     <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid">
                                     <div class="row">
                   

                         </div>
            </asp:Panel>
                 <br />
                  <div class="row">
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idDtl" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="masir" HeaderText="سرویس" />
                                            <asp:BoundField DataField="typevalue" HeaderText="روزکار/نوبتکار" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="citySiteTypeValue" HeaderText="نوع " >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
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
            </asp:Panel>
    <br />
     
          <br />

             <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
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

