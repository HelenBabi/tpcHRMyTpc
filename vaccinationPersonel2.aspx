<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="vaccinationPersonel2.aspx.vb" Inherits="vaccinationPersonel2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
      
       
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                     
                            <div class="row">
                                <div class="col-md-12">
                                    <h5>۱- مطابق نوبت اخذ شده  ، نحوه مراجعه خانواده ها اطلاع رسانی خواهد شد</h5>
                                     <h5>۲- واکسیناسیون فقط شامل افراد تحت تکفل کارکنان که متولد سال  ۱۳۷۵ و قبل از آن هستند (مطابق لیست ارائه شده توسط منابع انسانی)  خواهد شد</h5>
                                      <h5>۳- محل واکسیناسیون  سالن آمفی تاتر مجتمع رفاهی خواهد بود</h5>
                                      <h5>۴- جهت واکسیناسیون همراه داشتن کارت ملی الزامی است</h5>
                                    </div>
                                </div>
                            <br />
                  <div class="row">
              
			<div class="col-md-6">
                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="شماره تلفن همراه"></asp:Label>
                             <asp:TextBox ID="mobileTxt" runat="server" CssClass="form-control" placeholder="091..."  TextMode="Number" BackColor="LightYellow"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="موبایل باید وارد شود" ControlToValidate="mobileTxt" ForeColor="#CC0000"  ValidationGroup="a"></asp:RequiredFieldValidator>
                               </div>
                      	<div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="تعداد نفرات"></asp:Label>
                             <asp:TextBox ID="txtTedad" runat="server" CssClass="form-control" placeholder="عدد"   BackColor="LightYellow" MaxLength="11"></asp:TextBox>
                                 <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="نامعتبر" ControlToValidate="txtTedad" Type="Integer" MaximumValue="3" MinimumValue="1" ValidationGroup="a" ForeColor="Red" SetFocusOnError="True"></asp:RangeValidator>
                              </div>
                      
                      <div class="col-md-3">
                         
                              </div>

             	
                      </div>
                          <br />
                                  <div class="row">
              
			<div class="col-md-6">
              
                               </div>
                      	<div class="col-md-3">
                               <asp:Button ID="BtnAdd" runat="server" Text="ثبت" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a"  />
                              </div>
                      <div class="col-md-3">
                       
                              </div>

             	
                      </div>
                         
            <br />
                            <br />
                        </asp:Panel>
                 <br />
         
    
                  <div class="row">
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="reservID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="reservID" HeaderText="ردیف">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle ForeColor="red" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nam" HeaderText="نام و نام خانوادگی" />
                                            <asp:BoundField DataField="tedad" HeaderText="تعداد">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mobile" HeaderText="موبایل" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="del" Text="ابطال" Visible="False" >
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

