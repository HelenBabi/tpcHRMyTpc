<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="vaccinationPersonel.aspx.vb" Inherits="vaccinationPersonel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
      
       
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                       <br />
                            <div class="row">
                                <div class="col-md-12">
                               
                                   
                                             
                                                               </div>
                                </div>
                            <br />
              
                              <div class="row">
              
			<div class="col-md-12">
<asp:Label ID="lblBagi" runat="server" Text="2" BackColor="#FFFF66" Font-Bold="True" ForeColor="Green" Visible="False" ></asp:Label>
               </div>
                                  </div>
                              <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="انتخاب رشته ورزشی و زمان"></asp:Label>
                      <asp:DropDownList ID="citytToSiteNoRuzkar" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                            
                               </div>
                      <div class="col-md-4">
                         
                              <asp:Label ID="lblTedad" runat="server" Text="2" BackColor="#FFFF66" Font-Bold="True" ForeColor="green" ></asp:Label>
                            <asp:Label ID="lblzarf" runat="server" Text="2" BackColor="#FFFF66" Font-Bold="True" ForeColor="green" Visible="false"></asp:Label>
                           
                              </div>
                      	<div class="col-md-4">
                                <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="تعداد نفرات" Visible="False"></asp:Label>
                             <asp:TextBox ID="txtTedad" runat="server" CssClass="form-control" placeholder="عدد"  TextMode="Number" BackColor="LightYellow" Visible="False">1</asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="تعداد باید وارد شود" ControlToValidate="txtTedad" ForeColor="#CC0000"  ValidationGroup="a" Visible="False">1</asp:RequiredFieldValidator>
                              </div>
                      
                      

             	
                      </div>
                          <br />
                                  <div class="row">
              
			<div class="col-md-4">
             
                               </div>
                      	<div class="col-md-4">
                                <asp:Button ID="BtnAdd" runat="server" Text="رزرو" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a"  />
                              </div>
                      <div class="col-md-4">
                       
                              </div>

             	
                      </div>
                         
            <br />
                            <br />
                        </asp:Panel>
                 <br />
         
    
                  <div class="row">
                       <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="لیست رزروهای من"></asp:Label>
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idd" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="idd" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="magsad" HeaderText="زمان" />
                                            <asp:BoundField DataField="tedad" HeaderText="تعداد" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="del" Text="ابطال" ItemStyle-BackColor="#FF3300">
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
          <%--  </asp:Panel>--%>
  
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

