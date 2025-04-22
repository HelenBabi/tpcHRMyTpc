<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="turRegisterPersonel.aspx.vb" Inherits="turRegisterPersonel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
      
       <div class="container">
            <div class="alert-success">
           <p>
           <asp:Label ID="lblMsg"  runat="server"></asp:Label></p>
          
</div>
       </div>
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" Visible="True">
                      <div class="row">
                                <div class="col-md-6">
                                    <asp:Panel ID="Panel2" runat="server" BackColor="#FFFF99">
<table class="auto-style4">
        <tr>
            <td>
                <table class="auto-style4" dir="ltr">
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="sahmieLbl" runat="server" text=" 1 "></asp:Label>
                            </td>
                        <td>  :تعداد سهم شما در دوره   </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="estfadeLbl" runat="server" ></asp:Label>
                            </td>
                        <td> </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="bagiLbl" runat="server" text=" 1 "></asp:Label>
                            </td>
                        <td>:تعداد سهم باقی مانده</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                          
                                <asp:Label ID="LblTakafolCount"  runat="server"></asp:Label>
                           <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                            </td>
                        <td>:تعدادنفر مجاز درهر سهم</td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="LblMaxGaza" runat="server" Visible="false" ></asp:Label>
                            </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="text-align: left">
                   
                    
                </td>
            <td>
                    
                    <asp:HiddenField ID="HdnMaxGaza" runat="server" Value="0" />
                    
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    
                    <asp:HiddenField ID="HdnCountGazaSef" runat="server" />
                    
                </td>
        </tr>
    </table>
                                  </asp:Panel>

                                   
                                             
                                                               </div>  
                                
                                 <div class="col-md-6">
 <asp:Label ID="lblPayam" runat="server" Text="" ForeColor="Red"></asp:Label>
                                     </div>
                              
                                </div>
                           
                            <br />
              
                           
                              <div class="row">
              
			<div class="col-md-6">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="انتخاب محل و زمان"></asp:Label>
                      <asp:DropDownList ID="citytToSiteNoRuzkar" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                            
                               </div>
                      <div class="col-md-6">
                         
                           
                           
                              </div>
                      
                      
                      

             	
                      </div>

                          

                          <br />
                                  <div class="row">
              
			<div class="col-md-2">
                <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="تعداد نفرات" ></asp:Label>
                             <asp:TextBox ID="txtTedad" runat="server" CssClass="form-control" placeholder="عدد وارد شود"  TextMode="Number" BackColor="LightYellow" Width="100%"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="تعداد باید وارد شود" ControlToValidate="txtTedad" ForeColor="#CC0000"  ValidationGroup="a" ></asp:RequiredFieldValidator>
                               </div>
                      	<div class="col-md-10">
                               <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="توضیحات" ></asp:Label>
                                <asp:TextBox ID="tozih" runat="server" CssClass="form-control" placeholder="توضیحات مربوطه را در این قسمت بنیوسید، مثلا سابقه و یا تابحال از این محل استفاده نموده اید یا خیر"  TextMode="MultiLine" BackColor="LightYellow" Width="100%" Height="60px"></asp:TextBox>
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
<asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
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
                                            <asp:BoundField DataField="magsad" HeaderText="محل و زمان" />
                                            <asp:BoundField DataField="tedad" HeaderText="تعداد">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                          
                                            <asp:BoundField DataField="tarix" HeaderText="زمان ثبت نام" />
                                          
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

           
       
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>

