<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnTechersDetailFrm.aspx.vb" Inherits="trnTechersDetailFrm" %>
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
             <asp:Label ID="Label4" runat="server" class="label-control" Text="نام"></asp:Label>
            <asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="nam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                   
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label5" runat="server" class="label-control" Text="نام خانوادگی"></asp:Label>
            <asp:TextBox ID="family" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="family" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label6" runat="server" class="label-control" Text="نام پدر"></asp:Label>
            <asp:TextBox ID="father" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="father" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
        </div>
     <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" class="label-control" Text="کد ملی"></asp:Label>
            <asp:TextBox ID="codemeli" runat="server" placeholder="کدملی بدون خط تیره"
                    ValidationGroup="a"  class="form-control" MaxLength="10" TextMode="Number" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="codemeli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label8" runat="server" class="label-control" Text="شماره شناسنامه"></asp:Label>
            <asp:TextBox ID="shsh" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="shsh" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label9" runat="server" class="label-control" Text="جنسیت"></asp:Label>
                               <asp:DropDownList ID="jens" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                      
                  </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="jens" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
        </div>

    <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label24" runat="server" class="label-control" Text="محل اشتغال"></asp:Label>
                            <asp:TextBox ID="mahalEshtegal" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                       
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label25" runat="server" class="label-control" Text="سمت"></asp:Label>
                      <asp:TextBox ID="semat" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      
        </div>
         <div class="col-md-4">
            <asp:Label ID="nesbatLbl" runat="server" class="label-control" Text="تاریخ استخدام" ></asp:Label>
                                     
                 <asp:TextBox ID="tarixStexdam" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        
                                                                                  
                                         </div> 
      
      
        </div>
  
     <div class="row">
     <div class="col-md-4">
            <asp:Label ID="Label10" runat="server" class="label-control" Text="سابقه" ></asp:Label>
                                     
                 <asp:TextBox ID="sabege" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                     
                                                                                  
                                         </div> 
      
       <div class="col-md-4">
            <asp:Label ID="Label11" runat="server" class="label-control" Text="امور" ></asp:Label>
                                     
                 <asp:TextBox ID="omur" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                      
                                                                                  
                                         </div> 
      <div class="col-md-4">
            <asp:Label ID="Label12" runat="server" class="label-control" Text="واحد" ></asp:Label>
                                     
                 <asp:TextBox ID="Unit" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                     
                                                                                  
                                         </div> 
        </div>

     <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label13" runat="server" class="label-control" Text="تلفن همراه"></asp:Label>
            <asp:TextBox ID="mobil"  placeholder="0914900... مثال" runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ControlToValidate="mobil" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label14" runat="server" class="label-control" Text="ایمیل"></asp:Label>
            <asp:TextBox ID="mail"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label15" runat="server" class="label-control" Text="تلفن داخلی"></asp:Label>
            <asp:TextBox ID="daxeli"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
        </div>
        </div>

     <div class="row">
        <div class="col-md-12">
              <asp:Label ID="Label16" runat="server" class="label-control" Text="آدرس"></asp:Label>
             <asp:TextBox ID="address" placeholder=" آدرس کامل:استان-شهر-خیابان-کوچه-پلاک-طبقه-واحد" runat="server" 
                    ValidationGroup="a"  class="form-control" Width="100%"></asp:TextBox>
        </div>
        
        </div>
    
     <div class="row">
		<br />
          <div class="col-md-4">
                <asp:Label ID="Label2" runat="server" class="label-control" Text="تاریخ تولد"></asp:Label>
            <asp:TextBox ID="tarixTavalod"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label1" runat="server" class="label-control" Text="آخرین مدرک تحصیلی"></asp:Label>
                                                
                              <asp:TextBox ID="madrak"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                                            
                                         
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label3" runat="server" class="label-control" Text="رشته تحصیلی"></asp:Label>
                  <asp:TextBox ID="reshte"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                </div>
         

          </div>
      <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label17" runat="server" class="label-control" Text="دانشگاه محل اخذ مدرک"></asp:Label>
             <asp:TextBox ID="uni"  runat="server"  
                    class="form-control"  ></asp:TextBox>
            </div>
            <div class="col-md-4">
                  <asp:Label ID="Label18" runat="server" class="label-control" Text="معدل"></asp:Label>
           <asp:TextBox ID="moadel"  runat="server"  
                    class="form-control"  ></asp:TextBox>
            </div>
            <div class="col-md-4">
                 <asp:Label ID="Label19" runat="server" class="label-control" Text="گرایش تحصیلی"></asp:Label>
                 <asp:TextBox ID="gerayesh"  runat="server"  
                    class="form-control"  ></asp:TextBox>
            </div>

          </div>
     <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label22" runat="server" class="label-control" Text="معرف"></asp:Label>
             <asp:TextBox ID="moaref" placeholder=" نام شخص / سازمان / ارگان" runat="server"  class="form-control" ></asp:TextBox>
            </div>
            
          <div class="col-md-4">
                <asp:Label ID="Label27" runat="server" class="label-control" Text="نوع مدرس"></asp:Label>
                               <asp:DropDownList ID="cmbTechType" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                      
                  </asp:DropDownList>
                 </div>
           <div class="col-md-4">
                     <asp:Label ID="Label31" runat="server" class="label-control" Text="کدپرسنلی"></asp:Label>
                    <asp:TextBox ID="codePersoneli"  runat="server"  
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                 </div>
         </div>
     <div class="row">
        <div class="col-md-12">
              <asp:Label ID="Label21" runat="server" class="label-control" Text="مهارت ها"></asp:Label>
            <asp:TextBox ID="maharatha"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         </div>
     <div class="row">
        <div class="col-md-8">
              <asp:Label ID="Label23" runat="server" class="label-control" Text="توضیحات"></asp:Label>
            <asp:TextBox ID="notee"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
           <div class="col-md-4">
              <asp:Label ID="Label28" runat="server" class="label-control" Text="سابقه تدریس شرکتی"></asp:Label>
               <asp:TextBox ID="sherkati"  runat="server"  
                    class="form-control" ></asp:TextBox>

               </div>
         </div>
        </asp:Panel>
                 <br />

                      <asp:Panel ID="Panel2" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="سوابق تدریس">
                       
                        
                           <div class="row">
              
			<div class="col-md-6">
              <asp:Label ID="Label222" runat="server" CssClass="control-label" Text="دوره های آموزشی تدریس شده"></asp:Label>
                             <asp:TextBox ID="dore" runat="server" CssClass="form-control"  BackColor="LightYellow"  ></asp:TextBox>
              
                               </div>
                      	
                    <div class="col-md-3">
              <asp:Label ID="Label26" runat="server" CssClass="control-label" Text="نمره ارزیابی"></asp:Label>
                             <asp:TextBox ID="nomre" runat="server" CssClass="form-control"  BackColor="LightYellow"  ></asp:TextBox>
              
                               </div>
                               	<div class="col-md-3">
                                      
                                <asp:Button ID="BtnAdd" runat="server" Text=" ثبت سابقه" class="btn btn-success btn-sm" ValidationGroup="1"  />
                              </div>
             	
                      </div>
                        
                         
      
                           <div class="row">
                                                             	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="dtlID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="dtlID" HeaderText="#" Visible="False">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dore" HeaderText="دوره" />
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
                        </asp:Panel>
             
    <br />
       <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="تخصص ها">
                       
                        
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
                        </asp:Panel>
             
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

