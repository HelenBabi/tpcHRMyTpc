<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="jobDetailFrm.aspx.vb" Inherits="jobDetailFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
			  <div class="col-md-4">
				<asp:Label ID="lblUserNam" runat="server" CssClass="control-label" Text="کد"></asp:Label>
                <asp:TextBox ID="kmIDTxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
			</div>
         <div class="col-md-4">
            </div>

			       <div class="col-md-4">

            </div>


			</div>
     
     <div class="row">
			<div class="col-md-10">
                	<asp:Label ID="Label1" runat="server" CssClass="control-label" Text="موضوع"></asp:Label>
                <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
         <div class="col-md-2">
           <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="درصد پیشرفت پروژه"></asp:Label>
                <asp:TextBox ID="darsadPishTxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
      <div class="row">
			<div class="col-md-12">
                  <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="توضیحات فرستنده کار"></asp:Label>
                <asp:TextBox ID="tozihatTxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
          </div>
 <br />
  <div class="row">
      <div class="col-md-12">
                 <asp:DataList ID="DataList1" runat="server"  BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        GridLines="Horizontal" Width="100%" ForeColor="Black" 
                                     meta:resourcekey="DataList1Resource1">
                      <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                      <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                      <ItemTemplate>
                          <table class="style17" style="width: 100%">
                              <tr>
                                  <td style="text-align: right; background-color: #7ec0e1;" class="style2">
                                      &nbsp;&nbsp;<asp:Image ID="Image_pp" runat="server" Height="40px" 
                                          ImageUrl='<%# eval("picc") %>' meta:resourcekey="Image_ppResource1" 
                                          Width="30px" />
                                      <asp:Label ID="lbl_user" runat="server"  
                                          meta:resourcekey="lbl_userResource1" Text='<%# eval("headd") %>' 
                                          style="color: #ffffff"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="text-align: right; " class="style2">
                                      <asp:Label ID="lblUsrTyp"  runat="server" meta:resourcekey="lbl_noteResource1" 
                                          Text='<%# Eval("userType")%>' Visible="False"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  
                                  <td style="text-align: right; width: 100%; ">
                                      <div style="margin-top:10px; margin-bottom:10px;">
                                      <asp:Label ID="lbl_note" runat="server"   Font-Bold="False" meta:resourcekey="lbl_noteResource1" 
                                          Text='<%# eval("notee") %>'></asp:Label>
                                          </div>
                                  </td>
                                          </table>
                      </ItemTemplate>
                      <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                  </asp:DataList>
           
                                                     

                </div>
       
          </div>

       <br />
    <div class="row">
                  
            <div class="col-md-10">
                <asp:TextBox ID="txtNazar" runat="server" class="form-control" placeholder="توضیحات خود را در این قسمت بنویسد "  TextMode="MultiLine" Height="73px" 
                                     meta:resourcekey="txtNazarResource1" 
                                     ToolTip="توضیحات خود را در این قسمت بنویسد و دکمه ثبت توضیحات را کلیک کنید" ValidationGroup="a"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNazar" ErrorMessage="لطفا توضیحات کار را بنویسید" ForeColor="#FF33CC" ValidationGroup="a"></asp:RequiredFieldValidator>
                                 
                </div>
           <div class="col-md-2">
               <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="درصد"></asp:Label>
                <asp:TextBox ID="darsadTxt" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" placeholder="%"  ToolTip="توضیحاتی که درج نمودید چند درصد پروژه را شامل می شود"></asp:TextBox>
               </div>
           
          </div>

      <div class="row">
      <div class="col-md-12">
    <asp:Button ID="btnNazar" runat="server"  class="btn btn-success btn-bordered waves-effect waves-light" Text="ثبت توضیحات " ValidationGroup="a" />
                      <asp:Button ID="ImageButton1" runat="server" class="btn btn-warning btn-bordered waves-effect waves-light" Text="برگشت" />
                   <asp:Button ID="imgKartabl" runat="server" class="btn btn-danger btn-bordered waves-effect waves-light" Text="برگشت به کارتابل" />
          </div>
          </div>
    <br />
      <div class="row">
           <asp:Panel ID="Panel1" runat="server" GroupingText="ضمایم">
			  <div class="col-md-3">
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="400px" />
                  </div>
            <div class="col-md-3">
                  <asp:TextBox ID="sharhFile" runat="server" CssClass="txt_ r5" ValidationGroup="a" Width="100%" placeholder="توضیحات فایل"></asp:TextBox>
                  </div>
                <div class="col-md-3">
                  <asp:TextBox ID="tartib" runat="server" CssClass="txt_ r5" ValidationGroup="a" Width="80px" placeholder="اولویت نمایش"></asp:TextBox>
                  </div>
            <div class="col-md-3">
                  <asp:Button ID="btnSabtFile" runat="server"  class="btn btn-success btn-xs waves-effect waves-light" Text="ثبت فایل " />
                  </div>
                </asp:Panel>
        </div>

    <div class="row">
      <div class="col-md-12">
          </div>
        </div>

    

      <div class="row">
			  <div class="col-md-12">
                 
                  <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود فایل" BackColor="Yellow"> </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                       <asp:BoundField DataField="tartib" HeaderText="اولویت نمایش">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="content_name" HeaderText="نام فایل" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="date" HeaderText="تاریخ ثبت فایل" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="id" HeaderText="کد" Visible="False" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="karbar" HeaderText="کاربر ثبت کننده فایل" >
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="sharh" HeaderText="توضیحات">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="size" HeaderText="حجم فایل/کیلوبایت">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                        </Columns>
                    </asp:GridView>
                          </div>
          
        </div>
</asp:Content>

