<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="sugDetailConversationFrm.aspx.vb" Inherits="sugDetailConversationFrm" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid">
        <br />
     
          <asp:Panel ID="PanelErja" runat="server" GroupingText="ارجاعات" BackColor="#CCFFCC" >
       
       <div class="row">
			<div class="col-md-12">
                <asp:GridView ID="GridViewErja" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" ForeColor="Black" GridLines="Vertical" CssClass="table" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="ferestande" HeaderText="فرستنده" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="girande" HeaderText="گیرنده" />
                                            <asp:BoundField DataField="matnErja" HeaderText="متن ارجاع">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="runeveshtTypeValue" HeaderText="جهت" />
                                           
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ ارجاع" />
                                            <asp:BoundField DataField="saat" HeaderText="ساعت" />
                                           
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                </div>
              </div>
            
            </asp:Panel>
   
        <asp:Panel ID="Panel2" runat="server" Enabled="False" GroupingText="اطلاعات پیشنهاد">


<div class="fc-view-container">
        <div class="alert-info">
            <div class="row">
                    
                        <div class="col-md-4">
                 <asp:Label ID="Label12" runat="server" class="control-label" Text="پیشنهاد دهنده اصلی"></asp:Label>
                <asp:TextBox ID="pishnahaddahande" class="form-control" runat="server"></asp:TextBox>
            </div>
                <div class="col-md-8">
                 <asp:Label ID="Label11" runat="server" class="control-label" Text="سایر همکاران"></asp:Label>
                    <asp:TextBox ID="partners" class="form-control" runat="server"></asp:TextBox>
                
            </div>
        </div>
<div class="row">
           
            
             <div class="col-md-8">
                 <asp:Label ID="Label1" runat="server" class="control-label" Text="موضوع"></asp:Label>
                    <asp:TextBox ID="subject" class="form-control" runat="server"></asp:TextBox>
                
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label2" runat="server" class="control-label" Text="نوع پیشنهاد"></asp:Label>
                 <asp:DropDownList ID="sugType"  CssClass="form-control" Height="40px" runat="server">
                 </asp:DropDownList>
            </div>
        </div>
            <div class="row">
           
            <div class="col-md-4">
                 <asp:Label ID="Label4" runat="server" class="control-label" Text="پیش بینی هزینه اجرایی"></asp:Label>
                    <asp:TextBox ID="sugIncome" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label6" runat="server" class="control-label" Text="پیش بینی درآمد اجرایی"></asp:Label>
                    <asp:TextBox ID="realIncom" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label8" runat="server" class="control-label" Text="منافع حاصل از پیشنهاد"></asp:Label>
                    <asp:TextBox ID="benefits" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()"></asp:TextBox>
                 
            </div>
        </div>
        </div>
        
    
     <div class="row">
           
          
        </div>
    
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label7" runat="server" class="control-label" Text="شرح وضعیت موجود"></asp:Label>
                    <asp:TextBox ID="DescStatus" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
                    <asp:Label ID="Label3" runat="server" class="control-label" Text="شرح روش پیشنهادی"></asp:Label>
                    <asp:TextBox ID="sugMethod" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label10" runat="server" class="control-label" Text="شرح "></asp:Label>
                    <asp:TextBox ID="Description" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    </div>
            </asp:Panel>
    

   

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
                <div class="btn-group" role="group" aria-label="Basic example">
                
                 <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="تایید می شود" />
                 <asp:Button ID="btnWork" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="نیاز به توضیحات بیشتر" />
                  <asp:Button ID="btnEtmam" runat="server" class="btn btn-danger btn-sm waves-effect waves-light" Text="تایید نمی شود"/>
                    </div>
                 </div>


       </div>
    <br />

   
      <div class="row">
                  
            <div class="col-md-6">
                <asp:TextBox ID="txtNazar" runat="server" class="form-control" placeholder="توضیحات خود را در این قسمت بنویسد "  TextMode="MultiLine" Height="73px" 
                                     meta:resourcekey="txtNazarResource1" 
                                     ToolTip="توضیحات خود را در این قسمت بنویسد و دکمه ثبت توضیحات را کلیک کنید"></asp:TextBox>
                                
                                 
                </div>
             <div class="col-md-6">
               <asp:Button ID="btnNazar" runat="server"  class="btn btn-success btn-bordered waves-effect waves-light" Text="ثبت توضیحات " />
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
        <div class="row">
			<div class="col-md-6">   
               
                <asp:CheckBox ID="CheckBoxRunevesht" runat="server" AutoPostBack="True" Text="ارجاع پیشنهاد؟" />
                
                </div>
            <div class="col-md-6">   
 <asp:CheckBox ID="CheckBoxAttach" runat="server" AutoPostBack="True" Text="مشاهده ضمایم" />
                </div>
         </div>

             <br />
   <asp:Panel ID="PanelRunevest" runat="server" GroupingText="ارجاع پیشنهاد" BackColor="#CCFFCC" Visible="False">
       <div class="row">
			<div class="col-md-4">        
                	<asp:Label ID="Label5" runat="server" CssClass="control-label" Text="نام  گیرنده "></asp:Label>
                <dx:ASPxComboBox ID="cmbRunevesht" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" CssClass="btn btn-default dropdown-toggle" HorizontalAlign="Right">
    
                      
        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbRunevesht" ErrorMessage="لطفا گیرنده را انتخاب نمایدد" ForeColor="#FF33CC" InitialValue="0" ValidationGroup="c"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-4"> 
                     <asp:Label ID="Label9" runat="server" CssClass="control-label" Text=" متن "></asp:Label>
                <asp:TextBox ID="txtMatnErja" runat="server" placeholder="متن ارجاع را در این قسمت بنویسید" Height="88px" 
                                            TextMode="MultiLine" 
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>     
               </div>
            <div class="col-md-4">  
                	 <asp:DropDownList ID="CmbruneveshtType" runat="server"  CssClass="btn btn-default dropdown-toggle" Font-Bold="True" 
                                                 Font-Size="Medium" meta:resourcekey="cmbWorkTypResource1" 
                                                 style="font-size: small" >
                                                
                                             </asp:DropDownList>
                <asp:Button ID="btnRunevesht" runat="server" class="btn btn-primary btn-sm waves-effect waves-light" Text="افزودن" ValidationGroup="c" />
                </div>
        </div>
       <div class="row">
			<div class="col-md-12">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#" >
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="girande" HeaderText="گیرنده  " >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="matnErja" HeaderText="متن ارجاع">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="runeveshtTypeValue" HeaderText="جهت" />
                                            <asp:ButtonField CommandName="del" Text="حذف" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                </div>
              </div>
            <div class="row">
                	<div class="col-md-6">
                        </div>
			<div class="col-md-6">
               
              
                  <asp:Button ID="btnSaveFinal" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره و ارجاع" ValidationGroup="c" Visible="False" />
                
             			    			</div>
              
      </div>
            </asp:Panel>
       <br />
     <asp:Panel ID="PanelAttach" runat="server" GroupingText="ضمایم" BackColor="#FFFFCC" Visible="False" >
      <div class="row">
         
			  <div class="col-md-4">
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="400px" />
                  </div>
            <div class="col-md-4">
                   <asp:Button ID="btnSabtFile" runat="server"  class="btn btn-violet btn-sm waves-effect waves-light" Text="ثبت فایل " />
                  </div>
                <div class="col-md-4">
              
                    
                  </div>
           
               
        </div>
        <div class="row">
			  <div class="col-md-6">
                    <asp:Button ID="btnPuyesh" runat="server"  class="btn btn-primary btn-sm waves-effect waves-light" Text="پویش " Visible="False" />
                 
                  </div>
             <div class="col-md-6">
                 <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
                 </div>
            </div>
       <br />
       <div class="row">

         			  <div class="col-md-12">
           <%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
    <ContentTemplate>--%>
        <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود فایل" BackColor="Yellow"> </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                       <asp:BoundField DataField="tartib" HeaderText="اولویت نمایش" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="content_name" HeaderText="نام فایل" Visible="False" >
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
                          
                            <asp:BoundField DataField="size" HeaderText="حجم فایل/کیلوبایت" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                        </Columns>
                    </asp:GridView>
        <%--<asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="1000" Enabled="False" />--%>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

                  
                          </div>
          
        </div>
 </asp:Panel>

       <br />


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



