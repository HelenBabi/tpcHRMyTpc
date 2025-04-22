<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="refahiReserve.aspx.vb" Inherits="refahiReserve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
      
       
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                            <div class="row">
                                 <div class="col-md-12">
                                      <asp:Label ID="hoshdarLbl" runat="server" style="color: #FF3300; font-weight: 700">.</asp:Label>
                                     </div>
                                </div>
                       <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Panel ID="Panel2" runat="server" BackColor="#FFFF99">
<table class="auto-style4">
        <tr>
            <td>
                <table class="auto-style4" dir="ltr">
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="sahmieLbl" runat="server"></asp:Label>
                            </td>
                        <td>  :تعداد سهم شما در دوره   </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="estfadeLbl" runat="server" Visible="false"></asp:Label>
                            </td>
                        <td> </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="bagiLbl" runat="server"></asp:Label>
                            </td>
                        <td>:تعداد سهم باقی مانده</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="LblTakafolCount" runat="server"></asp:Label>
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
                                
                                 <div class="col-md-3">
                                     </div>
                                 <div class="col-md-3">
                                     </div>
                                 <div class="col-md-3">
                                     </div>
                                </div>
    
                            <br />
                  <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="انتخاب محل"></asp:Label>
                      <asp:DropDownList ID="resturanCmb" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                            
                               </div>
                      <div class="col-md-4">
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="resturanCmb" ErrorMessage="محل باید انتخاب شود" ForeColor="#FF3300" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                              </div>
                      	<div class="col-md-4">
                               
                              </div>
                      
                      </div>
                       <br />
                               <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="زمان"></asp:Label>
                      <asp:DropDownList ID="menuCmb" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                            
                               </div>
                      <div class="col-md-4">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="menuCmb" ErrorMessage="تایم باید انتخاب شود" ForeColor="#FF3300" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                            
                              </div>
                      	<div class="col-md-4">
                              </div>
                      
                      </div>
                           <br />
                            <div class="row">
              
			<div class="col-md-3">
   <asp:DropDownList ID="DropDownList1" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                         <asp:ListItem Value="0">خودم</asp:ListItem>
                                   </asp:DropDownList>
 <asp:Button ID="Button2" runat="server" class="btn-primary" Text="افزودن" ValidationGroup="1"  />
                </div>
                                <div class="col-md-3">
                                   

                </div>
                                  <div class="col-md-3">

                </div>
                                       <div class="col-md-3">

                </div>
                                </div>
                           <br />
                                <div class="row">
              
			<div class="col-md-4">
                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="افراد تحت تکفل"></asp:Label>
                   <asp:DropDownList ID="cmbTakafol" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                    
                                               <asp:Button ID="btnTemp" runat="server" class="btn-primary" Text="افزودن" ValidationGroup="1"  />                
                               </div>
                      <div class="col-md-4">
                       
                        
                              </div>
                      	<div class="col-md-4">
                                <asp:TextBox ID="tedad" runat="server" CssClass="textEntry" MaxLength="3" Width="40px" TextMode="Number" Height="25px" Enabled="False" Visible="False">1</asp:TextBox>
                              </div>
                      
                      </div>
                          <br />
                                  <div class="row">
          
                      
                      <div class="col-md-12">
                       <asp:GridView ID="GridTemp" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="IDD" >
                        <HeaderStyle Width="5px" />
                        <ItemStyle ForeColor="White" Width="5px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="locationValue" HeaderText="محل" />
                        <asp:BoundField DataField="menu" HeaderText="زمان" />
                        <asp:BoundField DataField="takafol" HeaderText="نام و نام خانوادگی" >
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nesbat" HeaderText="نسبت" />
                        <asp:ButtonField CommandName="del" Text="حذف">
                        <ItemStyle ForeColor="#FF3300" />
                        </asp:ButtonField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                              </div>

             	
                      </div>
                         
            <br />
                            <br />
                        </asp:Panel>
                 <br />
         
    
                  
          <%--  </asp:Panel>--%>
  
    <br />

     <div class="row">
			<div class="col-md-12">
        <asp:GridView ID="grdReserve" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="reservID" HeaderText="#">
                        <ItemStyle HorizontalAlign="Center" ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="locationValue" HeaderText="محل" />
                        <asp:BoundField DataField="menu" HeaderText="تایم">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rel" HeaderText="نام و نام خانوادگی">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="نسبت">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tarix" HeaderText="تاریخ رزو">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                </asp:GridView>
                </div>
         </div>

    <br />
             <div class="row">
			<div class="col-md-3">
              
                 <asp:Button ID="btnReserve" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="رزرو" ValidationGroup="a" Visible="False" />
                           
                </div>
                <div class="col-md-3">
                       <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                <div class="col-md-3">
                 
                     <asp:Button ID="Button1" runat="server" class="btn-danger" Text="ابطال" ValidationGroup="a" Visible="False" />
                </div>
                  <div class="col-md-3">
 
                </div>
                </div>
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>

