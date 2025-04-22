<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmTicketWorker.aspx.vb" Inherits="frmTicketWorker" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid">
        <br />
     
             <div class="row">
          <div class="col-md-3">
                </div>
          <div class="col-md-3">
                <asp:Label ID="Label12" runat="server" CssClass="control-label" Text=" زمان بروزرسانی اطلاعات"></asp:Label>

				<asp:TextBox ID="tarixUpInfo" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>
                </div>
        
           <div class="col-md-3">
                </div>
          
          <div class="col-md-3">
                </div>
          
		
          </div>
    <div class="row">
			<div class="col-md-6">
                <div class="btn-group" role="group" aria-label="Basic example">
                <asp:CheckBox ID="CheckEgdam" runat="server" class="checkbox circled primary" Text="در دست اقدام" AutoPostBack="True" />
                   <asp:Button ID="btnmojavez" runat="server" class="btn btn-primary btn-sm waves-effect waves-light" Text="نیاز به مجوز" />
                 <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="اطلاعات سیستم" />
                <asp:Button ID="btnSabege" runat="server" class="btn btn-info btn-sm waves-effect waves-light" Text="سوابق سیستم" />
                 <asp:Button ID="btnWork" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="ثبت کار" />
                  <asp:Button ID="btnEtmam" runat="server" class="btn btn-danger btn-sm waves-effect waves-light" Text="اتمام"/>
                    </div>
                 </div>

     <div class="col-md-2">
        
         <asp:TextBox ID="avgTxt" runat="server" ValidationGroup="c" CssClass="form-control" placeholder="میانگین زمان اتمام" Visible="false" TextMode="Number" ></asp:TextBox>
                </div>
         <div class="col-md-4">
             <asp:Button ID="btnping" runat="server" class="btn btn-violet btn-sm" Text="Ping" />
                           <asp:TextBox ID="txtPing" runat="server" CssClass="form-control" style="text-align: center"></asp:TextBox>
           </div>
     
       </div>
 


        <div class="row">
			<div class="col-md-4">

                <asp:Label ID="lblComName" runat="server" CssClass="control-label" Text="نام کامپیوتر"></asp:Label>

				<asp:TextBox ID="txtComNam" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="lblUserNam" runat="server" CssClass="control-label" Text="نام کاربری"></asp:Label>
                <asp:TextBox ID="txtUserNam" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
			</div>

              <div class="col-md-4">
				<asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="رمز کاربر"></asp:Label>
                <asp:TextBox ID="txtRamz" runat="server" 
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
			</div>
            
		</div>

    <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="شماره درخواست"></asp:Label>

				<asp:TextBox ID="txtTicketId" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label8" runat="server" CssClass="control-label" Text="شماره اموال"></asp:Label>
                <asp:TextBox ID="txtSystemCode" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
			</div>

              <div class="col-md-4">
                              <asp:Label ID="Label2" runat="server" CssClass="control-label" Text=" تحويل گيرنده"></asp:Label>
                <asp:TextBox ID="txt_holder" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
							</div>
         
		</div>
    <br />

   
  
     

      <div class="row">
         
          
            <div class="col-md-6">
				 <asp:DropDownList ID="cmbParaf" runat="server"  CssClass="btn btn-default dropdown-toggle" Font-Bold="True" 
                                                 Font-Size="Medium" meta:resourcekey="cmbWorkTypResource1" 
                                                 style="font-size: small" >
                                                
                                             </asp:DropDownList>
                             <asp:Button ID="btnParaf" runat="server"  class="btn btn-success btn-xs"  Text="پاراف" />

			</div>
            
             <div class="col-md-2">
 <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="تلفن"></asp:Label>
                <asp:TextBox ID="txtTelfon" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
                 </div>
             <div class="col-md-4">
                  
                      <asp:DropDownList ID="cmbWorkTyp" runat="server"   CssClass="btn btn-default dropdown-toggle" 
                    Font-Bold="True" Font-Size="Medium" style="font-size: small" 
                    meta:resourcekey="cmbWorkTypResource1" >
                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource1"> ریموت</asp:ListItem>
                    <asp:ListItem Value="12" meta:resourcekey="ListItemResource12">بروزرسانی اطلاعات</asp:ListItem>
                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource2">سخت افزار</asp:ListItem>
                    <asp:ListItem Value="5" meta:resourcekey="ListItemResource3">شبکه</asp:ListItem>
                    <asp:ListItem Value="6">Passive</asp:ListItem>
                    <asp:ListItem Value="7">نرم افزار</asp:ListItem>
                    <asp:ListItem Value="9">serviceDesk</asp:ListItem>
                     <asp:ListItem Value="8">رییس واحدIT </asp:ListItem>
                    <asp:ListItem Value="11">انبار</asp:ListItem>
                          <asp:ListItem Value="13">پرینتر</asp:ListItem>
                </asp:DropDownList>  
                     <asp:Button ID="btnEntegal" runat="server"  class="btn btn-warning btn-xs" Text="ارجاع" />
              
			
			          <asp:HiddenField ID="HiddenFieldUserID" runat="server" />
              
			
			</div>
          </div>
          <br />
          </asp:Panel>



    <br />

   
      <div class="row">
                  
            <div class="col-md-6">
                <asp:TextBox ID="txtNazar" runat="server" class="form-control" placeholder="توضیحات خود را در این قسمت بنویسد "  TextMode="MultiLine" Height="73px" 
                                     meta:resourcekey="txtNazarResource1" 
                                     ToolTip="توضیحات خود را در این قسمت بنویسد و دکمه ثبت نظر را کلیک کنید"></asp:TextBox>
                                
                                 
                </div>
             <div class="col-md-6">
               <asp:Button ID="btnNazar" runat="server"  class="btn btn-success btn-bordered waves-effect waves-light" Text="ثبت توضیحات " />
                      <asp:Button ID="ImageButton1" runat="server" class="btn btn-warning btn-bordered waves-effect waves-light" Text="برگشت" />
                   <asp:Button ID="imgKartabl" runat="server" class="btn btn-danger btn-bordered waves-effect waves-light" Text="برگشت به کارتابل" />
                 
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
                                  <td style="text-align: right; background-color: #007ACC;" class="style2">
                                      &nbsp;&nbsp;<asp:Image ID="Image_pp" runat="server" Height="30px" 
                                          ImageUrl='<%# eval("axurl") %>' meta:resourcekey="Image_ppResource1" 
                                          Width="30px" />
                                      <asp:Label ID="lbl_user" runat="server"  
                                          meta:resourcekey="lbl_userResource1" Text='<%# eval("headd") %>' 
                                          style="color: #FFFFFF"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="text-align: right; " class="style2">
                                      <asp:Label ID="lblUsrTyp"  runat="server" meta:resourcekey="lbl_noteResource1" 
                                          Text='<%# eval("typ") %>' Visible="False"></asp:Label>
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
     <div class="row">
      <div class="col-md-12">
            <asp:Label ID="lblhoshdar" runat="server"></asp:Label>
                                 <asp:HiddenField ID="typ" runat="server" />
                                 <asp:HiddenField ID="dat" runat="server" />
                                 <asp:HiddenField ID="hdnType" runat="server" />
                                 <asp:HiddenField ID="tim" runat="server" />
          </div>
         </div>

        
                                                 
   
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
  
</asp:Content>


