<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="messageFrm.aspx.vb" Inherits="messageFrm" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        function intFormat(number) {
            var regex = /(\d)((\d{3},?)+)$/;
            number = number.split(',').join('');
            while (regex.test(number)) {
                number = number.replace(regex, '$1,$2');
            }
            return number;
        }
        function numFormat(number) {
            var pointReg = /([\d,\.]*)\.(\d*)$/, f;
            if (pointReg.test(number)) {
                f = RegExp.$2;
                return intFormat(RegExp.$1) + '.' + f;
            }
            return intFormat(number);
        }
    </script>
  
     
          
   
        <asp:Panel ID="Panel2" runat="server" Enabled="False" GroupingText="بینفر">



            </asp:Panel>
    

   
    <br />
     <div class="row">
           <div class="col-md-6">
				 <asp:DropDownList ID="cmbParaf" runat="server"  CssClass="btn btn-default dropdown-toggle" Font-Bold="True" 
                                                 Font-Size="Medium" meta:resourcekey="cmbWorkTypResource1" 
                                                 style="font-size: small" >
                                                
                                             </asp:DropDownList>
                             <asp:Button ID="btnParaf" runat="server" class="btn btn-info btn-sm waves-effect waves-light" Text="پارف" />

			</div>
			<div class="col-md-6">
                
                 </div>


       </div>
    <br />

   
      <div class="row">
                  
            <div class="col-md-6">
                <asp:TextBox ID="txtNazar" runat="server" class="form-control" placeholder="متن خود را در این قسمت بنویسد "  TextMode="MultiLine" Height="73px" 
                                     meta:resourcekey="txtNazarResource1" 
                                     ToolTip="متن خود را در این قسمت بنویسد و دکمه ثبت توضیحات را کلیک کنید"></asp:TextBox>
                                
                                 
                </div>
             <div class="col-md-6">
               <asp:Button ID="btnNazar" runat="server"  class="btn btn-success btn-bordered waves-effect waves-light" Text="ثبت  " />
                      <asp:Button ID="ImageButton1" runat="server" class="btn btn-warning btn-bordered waves-effect waves-light" Text="برگشت" />
                 
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
                                      &nbsp;&nbsp;<asp:Image ID="Image_pp" runat="server" Height="40px" 
                                          ImageUrl='<%# Eval("picc")%>' meta:resourcekey="Image_ppResource1" 
                                          Width="30px" />
                                      <asp:Label ID="lbl_user" runat="server"  
                                          meta:resourcekey="lbl_userResource1" Text='<%# eval("headd") %>' 
                                          style="color: #FFFFFF"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="text-align: right; " class="style2">
                                      <asp:Label ID="lblUsrTyp"  runat="server" meta:resourcekey="lbl_noteResource1" 
                                          Text='<%# eval("ID") %>' Visible="False"></asp:Label>
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
        

   

        
                                                 
   
</asp:Content>



