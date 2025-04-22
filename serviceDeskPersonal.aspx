<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="serviceDeskPersonal.aspx.vb" Inherits="serviceDeskPersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
      
       
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="فرم رزرو وقت ملاقات با مدیرعامل">
                           <div class="container"
                              >
                          <div class="alert-success">
                              <h4>روزها و ساعت هایی که ظرفیت پر شده نمایش داده نمی شوند</h4>
                          </div>
                              </div>
                       <br />
                            <div class="row">
                                <div class="col-md-12">
                               
                                   
                                             
                                                               </div>
                                </div>
                            <br />
              
                              <div class="row">
              
			<div class="col-md-12">

               </div>
                                  </div>
                              <div class="row">
              
			<div class="col-md-6">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text=" روزهای قابل رزرو"></asp:Label>
                      <asp:DropDownList ID="citytToSiteNoRuzkar" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                            <asp:Label ID="lblBagi" runat="server" Text="2" BackColor="#FFFF66" Font-Bold="True" ForeColor="Green" Visible="false" ></asp:Label>
                               </div>
                      <div class="col-md-6">
                         
                              <asp:Label ID="lblTedad" runat="server" Text="0" BackColor="#FFFF66" Font-Bold="True" ForeColor="green" Visible="false" ></asp:Label>
                            <asp:Label ID="lblzarf" runat="server"  BackColor="#FFFF66" Font-Bold="True" ForeColor="green" Visible="false"></asp:Label>
                           
                              </div>
                      
                      
                      

             	
                      </div>
                          <br />

                           <div class="row">
              
			<div class="col-md-4">
              <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="ساعت های قابل رزرو"></asp:Label>
                      <asp:DropDownList ID="timeCmb" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                               </div>
                      	<div class="col-md-4">
                                
                              </div>
                      <div class="col-md-4">
                     
                              </div>

             	
                      </div>
                          <br />
                           <div class="row">
              
			<div class="col-md-4">
              <%--<asp:Label ID="Label2" runat="server" CssClass="control-label" Text="توضیحات"></asp:Label>--%>
                             <asp:TextBox ID="tozih" runat="server" CssClass="form-control" placeholder="(علت ملاقات)توضیحات مختصر حداکثر 50 کاراکتر"  TextMode="MultiLine" BackColor="LightYellow" Height="70px" MaxLength="50"></asp:TextBox>
              
                               </div>
                      	
                      <div class="col-md-8">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="توضیحات الزامی می باشد" ValidationGroup="1" ControlToValidate="tozih" ForeColor="Red"></asp:RequiredFieldValidator>
                              </div>

             	
                      </div>
                          <br />
                                  <div class="row">
              
			<div class="col-md-4">
             
                               </div>
                      	<div class="col-md-4">
                                <asp:Button ID="BtnAdd" runat="server" Text="رزرو" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="1"  />
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
                                            <asp:BoundField DataField="magsad" HeaderText=" زمان" />
                                            <asp:BoundField DataField="startTime" HeaderText="ازساعت">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="endTime" HeaderText="تا ساعت">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                          
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ ثبت نام" />
                                          
                                            <asp:BoundField DataField="tozih" HeaderText="توضیحات" />
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت" />
                                          
                                           <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelFile" runat="server"  OnClientClick="return confirm('آیا مطمئن هستید رزرو را ابطلال می کنید ?')"  OnClick="lnkDelFile_Click" style=" Background:#ffd800 "  Text="ابطال"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
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

