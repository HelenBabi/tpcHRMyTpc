<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="sugDetailFrm.aspx.vb" Inherits="sugDetailFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
   <%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
   
   <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="row">
			<div class="col-md-12"> 
                     <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Icons/Word-icon.png" ToolTip="ارسال نامه اصلی به word" />
              
             			    			
                
               <asp:Button ID="btnErajaat" runat="server"  class="btn btn-violet btn-sm waves-effect waves-light" Text="مشاهده ارجاعات" Visible="False" />
                    
                 <asp:HiddenField ID="HdnId" runat="server" Value="0" />
                 
                    <asp:Button ID="btnHistory" runat="server"  class="btn btn-primary btn-sm waves-effect waves-light" Text="تاریخچه نامه" />
                 
              
                  <asp:Button ID="btnPayanGardesh" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="پایان گردش" Visible="False" />
                
             			    			
                
                   <asp:Button ID="imgKartabl" runat="server" class="btn btn-danger btn-bordered waves-effect waves-light" Text="بستن نامه" />
                 
                </div>
       </div>
       <div class="row">
			<div class="col-md-12">        
                <asp:GridView ID="GridGirande" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" CssClass="table" Enabled="False">
                                        <Columns>
                                            <asp:BoundField DataField="ferestande" HeaderText="از" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="girandee" HeaderText="به" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="runeveshtTypeValue" HeaderText="نوع نامه">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="zaman" HeaderText="تاریخ نامه">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="actionValue" HeaderText="وضعیت نامه">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                </div>
            
          
        </div>
    <div class="fc-view-container">
        <div class="alert-info">
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
                    <asp:TextBox ID="sugIncome" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label6" runat="server" class="control-label" Text="پیش بینی درآمد اجرایی"></asp:Label>
                    <asp:TextBox ID="realIncom" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label8" runat="server" class="control-label" Text="منافع حاصل از پیشنهاد"></asp:Label>
                    <asp:TextBox ID="benefits" class="form-control" runat="server"></asp:TextBox>
                 
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
                    <asp:Label ID="Label9" runat="server" class="control-label" Text="شرح روش پیشنهادی"></asp:Label>
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
            
    <br />

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
   <asp:Panel ID="PanelRunevest" runat="server" GroupingText="ارجاع نامه" BackColor="#CCFFCC" Visible="False">
       <div class="row">
			<div class="col-md-4">        
                	<asp:Label ID="Label5" runat="server" CssClass="control-label" Text="نام ارجاع گیرنده "></asp:Label>
                <dx:ASPxComboBox ID="cmbRunevesht" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" CssClass="btn btn-default dropdown-toggle" HorizontalAlign="Right">
    
                      
        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbRunevesht" ErrorMessage="لطفا گیرنده ارجاع را انتخاب نمایدد" ForeColor="#FF33CC" InitialValue="0" ValidationGroup="c"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-4"> 
                     <asp:Label ID="Label3" runat="server" CssClass="control-label" Text=" متن "></asp:Label>
                <asp:TextBox ID="txtMatnErja" runat="server" placeholder="متن ارجاع را در این قسمت بنویسید" Height="88px" 
                                            TextMode="MultiLine" 
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>     
               </div>
            <div class="col-md-4">  
                <asp:Button ID="btnRunevesht" runat="server" class="btn btn-primary btn-sm waves-effect waves-light" Text="رونوشت جدید" ValidationGroup="c" />
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
                                            <asp:BoundField DataField="girandee" HeaderText="گیرنده ارجاع " >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="matnErja" HeaderText="متن ارجاع">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
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
			<div class="col-md-12">
               
              
                  <asp:Button ID="btnSaveFinal" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره و ارجاع" ValidationGroup="c" />
                
             			    			</div>
              
      </div>
            </asp:Panel>
       <br />
   
      <div class="row">
           <asp:Panel ID="PanelAttach" runat="server" GroupingText="ضمایم" BackColor="#FFFFCC" Visible="False" >
			  <div class="col-md-4">
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="400px" />
                  </div>
            <div class="col-md-4">
                   <asp:Button ID="btnSabtFile" runat="server"  class="btn btn-violet btn-sm waves-effect waves-light" Text="ثبت فایل " />
                  </div>
                <div class="col-md-4">
              
                    
                  </div>
           
                </asp:Panel>
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
       <br />
        
       
         <div class="row">
			<div class="col-md-12">
  
  
             			    			</div>
              
      </div>
     
                      
                
       
             
</asp:Content>




