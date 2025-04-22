<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnOutTechersDetailFrm.aspx.vb" Inherits="trnOutTechersDetailFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm " Text="برگشت" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
    
      
    <asp:Panel ID="Panel1" runat="server" Enabled="False">
    <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label4" runat="server" class="label-control" Text="نام موسسه"></asp:Label>
            <asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="nam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                   
        </div>
        <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" class="label-control" Text="کد ملی"></asp:Label>
            <asp:TextBox ID="codemeli" runat="server" placeholder="کدملی بدون خط تیره"
                    ValidationGroup="a"  class="form-control" MaxLength="10" TextMode="Number" ></asp:TextBox>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="codemeli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
        
         <div class="col-md-4">
               <asp:Label ID="Label6" runat="server" class="label-control" Text="کداقتصادی"></asp:Label>
            <asp:TextBox ID="codeEgtesadi" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="codeEgtesadi" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
        </div>
     <div class="row">
         <div class="col-md-4">
              <asp:Label ID="Label5" runat="server" class="label-control" Text="شناسه ملی"></asp:Label>
            <asp:TextBox ID="shenasemeli" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="shenasemeli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label8" runat="server" class="label-control" Text="شماره ثبت"></asp:Label>
            <asp:TextBox ID="sabtNo" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="sabtNo" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label9" runat="server" class="label-control" Text="کد پستی"></asp:Label>
            <asp:TextBox ID="codePosti" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="codePosti" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
        </div>
        </div>

    <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label24" runat="server" class="label-control" Text="شماره تماس"></asp:Label>
                            <asp:TextBox ID="tel" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                       
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label25" runat="server" class="label-control" Text="فاکس"></asp:Label>
                      <asp:TextBox ID="fax" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      
        </div>
         <div class="col-md-4">
            <asp:Label ID="nesbatLbl" runat="server" class="label-control" Text="محل اقامت قانونی موسسه" ></asp:Label>
                                     
                 <asp:TextBox ID="mahaleEgamat" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        
                                                                                  
                                         </div> 
      
      
        </div>
  
     <div class="row">
     <div class="col-md-4">
            <asp:Label ID="Label10" runat="server" class="label-control" Text="نوع مالکیت" ></asp:Label>
                                     
                    <asp:DropDownList ID="malekiatType" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                      
                        <asp:ListItem>خصوصی</asp:ListItem>
                        <asp:ListItem>دولتی</asp:ListItem>
                                      
                  </asp:DropDownList>
                     
                                                                                  
                                         </div> 
      
       <div class="col-md-4">
            <asp:Label ID="Label11" runat="server" class="label-control" Text="شماره حساب" ></asp:Label>
                                     
                 <asp:TextBox ID="shHesab" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      
                                                                                  
                                         </div> 
      <div class="col-md-4">
            <asp:Label ID="Label12" runat="server" class="label-control" Text="شماره شبا" ></asp:Label>
                                     
                 <asp:TextBox ID="shShaba" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                     
                                                                                  
                                         </div> 
        </div>

     <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label13" runat="server" class="label-control" Text="بانک"></asp:Label>
            <asp:TextBox ID="bank"   runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
         
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label14" runat="server" class="label-control" Text="شعبه"></asp:Label>
            <asp:TextBox ID="shobe"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label15" runat="server" class="label-control" Text="کد شعبه"></asp:Label>
            <asp:TextBox ID="codeShobe"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
        </div>
        </div>

 
    
     <div class="row">
		<br />
          <div class="col-md-4">
                <asp:Label ID="Label2" runat="server" class="label-control" Text="نام موسسه صاحب حساب"></asp:Label>
            <asp:TextBox ID="namMoseseSahebHesab"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label1" runat="server" class="label-control" Text="نام مدیرعامل"></asp:Label>
                                                
                              <asp:TextBox ID="modirAmel"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                                            
                                         
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label3" runat="server" class="label-control" Text="حسن همکاری"></asp:Label>
                  <asp:TextBox ID="hosnHamkari"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                </div>
         

          </div>
      
     
     <div class="row">
        <div class="col-md-12">
              <asp:Label ID="Label21" runat="server" class="label-control" Text="نتایج حاصل از ارزیابی اولیه تامین کنندگان"></asp:Label>
            <asp:TextBox ID="natayejArzyabiTamin"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         </div>
     <div class="row">
        <div class="col-md-9">
              <asp:Label ID="Label23" runat="server" class="label-control" Text="نتایج حاصل ازارزیابی اثربخشی دوره های آموزشی برگزار شده  قبلی"></asp:Label>
            <asp:TextBox ID="natayejArzyabiGabli"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         
           <div class="col-md-3">
    <asp:CheckBox ID="ChbActiveCode" runat="server" Text="غیرفعال؟" />
               </div>
         </div>
</asp:Panel>
                 <br />

                      
      
       <%--<asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="تخصص ها">
                       
                        
                           <div class="row">
              
			<div class="col-md-6">
              <asp:Label ID="Label29" runat="server" CssClass="control-label" Text="تخصص"></asp:Label>
                             <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  BackColor="LightYellow"  ></asp:TextBox>
              
                               </div>
                      	
                    <div class="col-md-3">
              <asp:Label ID="Label30" runat="server" CssClass="control-label" Text="نمره ارزیابی"></asp:Label>
                             <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"  BackColor="LightYellow"  ></asp:TextBox>
              
                               </div>
                               	<div class="col-md-3">
                                      
                                <asp:Button ID="Button2" runat="server" Text=" ثبت تخصص" class="btn btn-success btn-sm" ValidationGroup="1"  />
                              </div>
             	
                      </div>
                        
                         
      
                           <div class="row">
                                                             	<div class="col-md-12">
                                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="dtlID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="dtlID" HeaderText="#" Visible="False">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dore" HeaderText="تخصص" />
                                            <asp:BoundField DataField="nomre" HeaderText="نمره">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                          
                                           
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
                          <br />
                        </asp:Panel>--%>
             
    <br />
         <asp:Panel ID="PanelAttach" runat="server" GroupingText="ضمایم" BackColor="#FFFFCC"  >
      <div class="row">
         
			  <div class="col-md-3">
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="400px" />
                  </div>
          
                <div class="col-md-6">
               <asp:Label ID="Label20" runat="server" class="label-control" Text="توضیحات فایل"></asp:Label>
             <asp:TextBox ID="txtFileNote" placeholder="نوع فایل" runat="server"  class="form-control" ></asp:TextBox>
                    
          
                   
                  </div>
          <div class="col-md-3">
<asp:Button ID="btnSabtFile" runat="server"  class="btn btn-violet btn-sm " Text="ثبت فایل " />
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
                          
                            <asp:BoundField DataField="sharh" HeaderText="توضیحات فایل">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="karbar" HeaderText="کاربر ثبت کننده فایل" >
                          
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
  <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
 <asp:Button ID="Button1" runat="server" class="btn btn-warning btn-sm " Text="برگشت" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>

</asp:Content>

