<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="transportationPersonelMyCar.aspx.vb" Inherits="transportationPersonelMyCar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="اطلاعات خودرو شخصی" BorderColor="#9999FF">
                      <div class="row">
			<div class="col-md-4">
                             <h6>اگر از پارکینگ شرکت استفاده می نمایید لطفا اطلاعات خودرو شخصی خود را ثبت کنید</h6>                            
                </div>
                <div class="col-md-4">
                     <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="مثال"></asp:Label>
 <asp:Image ID="Image1" runat="server" ImageUrl="~/images/design_images/pelak.png" />
                </div>
                <div class="col-md-4">
 
                </div>
                </div>
             <div class="row">
			
                <div class="col-md-4">
                     <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="نوع ماشین"></asp:Label>
                    <asp:TextBox ID="carTypee" runat="server" CssClass="form-control" placeholder="مثال:پراید-پژو..."></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="carTypee" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
                 
                <div class="col-md-4">
                      <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="شماره شهربانی"></asp:Label>
                   

                      <table class="nav-justified">
                          <tr>
                              <td> <asp:TextBox ID="carPelakiran" runat="server" CssClass="form-control" placeholder="ایران" TextMode="Number"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="carPelakiran" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                              </td>
                              <td><asp:TextBox ID="carPelak3" runat="server" CssClass="form-control" placeholder="سه رقم"  TextMode="Number"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="carPelak3" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                              </td>
                              <td><asp:TextBox ID="carPelakHarf" runat="server" CssClass="form-control" placeholder="حرف" ></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="carPelakHarf" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                              </td>
                              <td><asp:TextBox ID="carPelak2" runat="server" CssClass="form-control" placeholder="دو رقم"  TextMode="Number"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="carPelak2" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                              </td>
                          </tr>
                      </table>

                </div>
                   <div class="col-md-4">

                </div>
                </div>
        <br />
            <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                     <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره" ValidationGroup="a" />
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" ValidationGroup="b" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
</asp:Panel>
</asp:Content>

