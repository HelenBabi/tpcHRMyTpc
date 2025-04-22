<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="ProfileFrm.aspx.vb" Inherits="ProfileFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <asp:Panel ID="Panel1" runat="server" GroupingText="اطلاعات شخصی پرسنل" BorderColor="#9999FF">
      
         
       
        <div class="container">
                      <div class="alert-success">
		
                <p>لطفا اطلاعات خود را بررسی و در صورت مغایرت اصلاح نمایید و فیلد های خالی را پر و در آخر کلید ذخیره را کلیک کنید
                                  
                </p>
                           <p>تکمیل فرم ذیل صرفا جهت تکمیل بانک اطلاعاتی مدیریت منابع انسانی می باشد</p>
                   </div>   
                          </div>
             <div class="row">
        <div class="col-md-4">
               <asp:Label ID="Label38" runat="server" class="label-control" Text="محل اشتغال"  ></asp:Label>
				 <dx:ASPxComboBox ID="cmbSazman" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" CssClass="btn btn-default dropdown-toggle" >
    
                      
        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
            
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label39" runat="server" class="label-control" Text="عنوان شغل"  ></asp:Label>
                    <dx:ASPxComboBox ID="cmbShoghl" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" CssClass="btn btn-default dropdown-toggle" >
                        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
        </div>
                   <div class="col-md-4">
             
        </div>
         </div>
 <div class="row">
        <div class="col-md-3">
              <asp:Label ID="Label41" runat="server" class="label-control" Text="تاریخ شروع"  ></asp:Label>
              <dx:ASPxTextBox ID="tarixshoru" runat="server" CssClass="form-control"
                    HorizontalAlign="Left" placeholder="روز/ماه/سال" >
                            <MaskSettings Mask="HH:mm">
                            </MaskSettings>
                            <masksettings mask="0000/00/00" />
                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
            </div>
      <div class="col-md-3">
          <asp:Label ID="Label42" runat="server" class="label-control" Text="تاریخ پایان"  ></asp:Label>
           <dx:ASPxTextBox ID="tarixpayan" runat="server" CssClass="form-control"
                    HorizontalAlign="Left" placeholder="روز/ماه/سال" >
                            <MaskSettings Mask="HH:mm">
                            </MaskSettings>
                            <masksettings mask="0000/00/00" />
                            <validationsettings display="None">
                            </validationsettings>
                        </dx:ASPxTextBox>
            </div>
      <div class="col-md-3">
           <asp:Label ID="Label40" runat="server" class="label-control" Text="مدت کارکرد"  ></asp:Label>
                         <table class="nav-justified">
            <tr>
                <td>
                    <asp:TextBox ID="karkardDay" runat="server" CssClass="form-control" placeholder="چند روز" TextMode="Number" MaxLength="2"></asp:TextBox>
                  </td>
                <td>
<asp:TextBox ID="karkardMah" runat="server" CssClass="form-control" placeholder="چند ماه" TextMode="Number" MaxLength="2"></asp:TextBox>
                </td>
                <td>
 <asp:TextBox ID="karkardSal" runat="server" CssClass="form-control" placeholder="چند سال" TextMode="Number" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
        </table>
            </div>
      <div class="col-md-3">

          <asp:Label ID="Label43" runat="server" class="label-control" Text="سابقه پرداخت بیمه"  ></asp:Label>
          <table class="nav-justified">
            <tr>
                <td>
                    <asp:TextBox ID="sabegeDay" runat="server" CssClass="form-control" placeholder="چند روز" TextMode="Number" MaxLength="2"></asp:TextBox>
                  </td>
                <td>
<asp:TextBox ID="sabegeMah" runat="server" CssClass="form-control" placeholder="چند ماه" TextMode="Number" MaxLength="2"></asp:TextBox>
                </td>
                <td>
 <asp:TextBox ID="sabegeSal" runat="server" CssClass="form-control" placeholder="چند سال" TextMode="Number" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
        </table>
                              
         
        
            </div>
     </div>
<br />           
        <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="lblSabege" runat="server" CssClass="control-label" Text="سوابق پرسنل " Visible="false"></asp:Label>
                                                <asp:GridView ID="GridViewSavabeg" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="id" Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%" ForeColor="Black" GridLines="Vertical">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="#">
                                                        <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sazmanName" HeaderText="نام شرکت/سازمان" />
                                                        <asp:BoundField DataField="postname" HeaderText="عنوان شغل">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="sherkati" HeaderText="نوع" />
                                                        <asp:BoundField HeaderText="تاریخ شروع" DataField="tarixshoru" />
                                                        <asp:BoundField DataField="tarixpayan" HeaderText="تاریخ پایان" />
                                                             <asp:BoundField DataField="karkardDay" HeaderText="تعداد روز کارکرد" >
                                                        <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="karkardMah" HeaderText="تعداد ماه کارکرد" >
                                                        <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="karkardSal" HeaderText="تعداد سال کارکرد" >
                                                        <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sabegeDay" HeaderText="تعداد روز پرداخت بیمه" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sabegeMah" HeaderText="تعداد ماه پرداخت بیمه" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sabegeSal" HeaderText="تعداد سال پرداخت بیمه" >
                                                             <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                               <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdel" runat="server"  OnClientClick="return confirm('آیا مطمئن هستید رکورد را می خواهید حذف کنید ?')"  OnClick="lnkDel_Click" style="color: #FF0000" Text="حذف"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle Font-Size="Medium" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                </asp:GridView>
                                               </div>
                      </div>
        <br />
            <div class="row">
        <div class="col-md-3">
             <asp:Label ID="Label34" runat="server" class="label-control" Text="وضعیت ایثارگری"></asp:Label>
                            <asp:DropDownList ID="esargariTyp" runat="server" class="btn btn-secondary dropdown-toggle" >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem>جز خانواده ایثارگر نمی باشم</asp:ListItem>
                      <asp:ListItem>خانواده معظم شهدا</asp:ListItem>
                                   <asp:ListItem>خانواده معظم جانبازان</asp:ListItem>
                                    <asp:ListItem>خانواده معظم آزادگان</asp:ListItem>
                                    <asp:ListItem>خانواده معظم رزمندگان</asp:ListItem>
                  </asp:DropDownList>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label28" runat="server" CssClass="control-label" Text="نسبت"></asp:Label>
             <asp:DropDownList ID="nesbatEsargari" runat="server" 
                                        class="btn btn-secondary dropdown-toggle"  TabIndex="10" 
                                        Width="190px" >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>خودم</asp:ListItem>
                                        <asp:ListItem>فرزند جانباز</asp:ListItem>
                                        <asp:ListItem>فرزند شهید</asp:ListItem>
                                        <asp:ListItem>برادر شهید</asp:ListItem>
                                        <asp:ListItem>خواهر شهید</asp:ListItem>
                                        <asp:ListItem>فرزند رزمنده</asp:ListItem>
                                    </asp:DropDownList>
        </div>
         <div class="col-md-2">
          
                                          
                                                                                  
                                         </div> 
          <div class="col-md-3">
                  
              </div>
      
        </div>
     
        <br />
     <div class="row">
        <div class="col-md-3">
             <asp:Label ID="Label36" runat="server" class="label-control" Text="وضعیت نظام وظیفه"></asp:Label>
                               <asp:DropDownList ID="xedmat" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True"  >
                                          <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">پایان خدمت</asp:ListItem>
                      <asp:ListItem Value="2">معافیت</asp:ListItem>
                  </asp:DropDownList>
        </div>
           <div class="col-md-3">
              <asp:Label ID="lblMoafiat" runat="server" class="label-control"  Text="نوع معافیت"></asp:Label>
                          
                     <asp:DropDownList ID="moafitType" runat="server" class="btn btn-secondary dropdown-toggle" >
                      <asp:ListItem Value="0"></asp:ListItem>
                      <asp:ListItem Value="پزشكي دائم"></asp:ListItem>
                      <asp:ListItem>پزشكي موقت</asp:ListItem>
                         <asp:ListItem>کسرخدمت</asp:ListItem>
                         <asp:ListItem>كفالت دائم</asp:ListItem>
                                                   <asp:ListItem>خانواده شهيد/آزاده</asp:ListItem>
                          <asp:ListItem>مازادبرنياز</asp:ListItem>
                         <asp:ListItem>مواردخاص</asp:ListItem>
                  </asp:DropDownList>   
        </div>
         <div class="col-md-3">
              <asp:Label ID="Label27" runat="server"  CssClass="control-label" Text="تاریخ شروع خدlت سربازی"></asp:Label>
               <asp:TextBox ID="tarixSarbaziShuru"  placeholder="روز/ماه/سال مثال1365/12/20" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
         <div class="col-md-3">
              <asp:Label ID="lbltarixsarbazi" runat="server"  CssClass="control-label" Text="تاریخ اتمام خدمت سربازی یا معافیت"></asp:Label>
               <asp:TextBox ID="tarixSarbazi"  placeholder="روز/ماه/سال" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
       
        </div>
        <br />
             <div class="row">

        <div class="col-md-4">
              <asp:Label ID="Label35" runat="server" class="label-control" Text="آیا جزء فرزندان یا بستگان کارکنان صنعت نفت می باشد"></asp:Label>
                                          <asp:DropDownList ID="nesbatRadio" runat="server" class="btn btn-secondary dropdown-toggle"  AutoPostBack="true" >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">بلی</asp:ListItem>
                      <asp:ListItem Value="2">خیر</asp:ListItem>
                  </asp:DropDownList  
                          >
        </div>
         <div class="col-md-4">
              <asp:Label ID="nesbatLbl" runat="server" class="label-control" Text="نسبت "  ></asp:Label>
                                     
                    <asp:DropDownList ID="nesbatCmb" runat="server" class="btn btn-secondary dropdown-toggle" Enabled="False">
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value=" فرزند"></asp:ListItem>
                      <asp:ListItem>سایر بستگان</asp:ListItem>
                  </asp:DropDownList>
        </div>
         <div class="col-md-4">
            
                         <asp:Label ID="perseneliLbl" runat="server" class="label-control" Text="شماره پرسنلی بستگان "  ></asp:Label>
				<asp:TextBox ID="persenliTxt" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"  Enabled="false" placeholder=" کد پرسنلی صنعت نفت" ></asp:TextBox>
        </div>
    
        </div>
               <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label15" runat="server" CssClass="control-label" Text="کدملی "></asp:Label>
              <asp:TextBox ID="codeMelli" runat="server"  
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label16" runat="server" CssClass="control-label" Text="نام پدر"></asp:Label>
               <asp:TextBox ID="father" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
           
  <div class="col-md-4">
               <asp:Label ID="Label20" runat="server" CssClass="control-label" Text="تاریخ تولد"></asp:Label>
               <asp:TextBox ID="tarixtavalod" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
    </div>
         <div class="row">
                      <div class="col-md-3">
                  <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="محل تولد"></asp:Label>
               <asp:TextBox ID="mahaletavalod" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
                <div class="col-md-3">
               <asp:Label ID="Label17" runat="server" CssClass="control-label" Text="شماره شناسنامه"></asp:Label>
               <asp:TextBox ID="shsh" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>      
             <div class="col-md-3">
             <asp:Label ID="Label29" runat="server" CssClass="control-label" Text="شماره مسلسل شناسنامه "></asp:Label>
              <asp:TextBox ID="shMosalsalsh" runat="server"  
                                            CssClass="form-control"></asp:TextBox>
				        </div>
        <div class="col-md-3">
             <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="شماره سریال شناسنامه "></asp:Label>
              <asp:TextBox ID="shserial" runat="server"  
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           
         

    </div>
             <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label18" runat="server" CssClass="control-label" Text="نوع شناسنامه"></asp:Label>
             <asp:DropDownList ID="vaziatsh" runat="server" 
                                        CssClass="dropdown-header" Font-Bold="True" Height="30px" TabIndex="10" 
                                        Width="190px" >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>اصل</asp:ListItem>
                                        <asp:ListItem>المثنی</asp:ListItem>
                                    </asp:DropDownList>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label19" runat="server" CssClass="control-label" Text="محل صدور شناسنامه"></asp:Label>
               <asp:TextBox ID="mahalesodur" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
                   <div class="col-md-4">
               <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="تاریخ صدور"></asp:Label>
               <asp:TextBox ID="tarixSodur" placeholder="روز/ماه/سال" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
         

    </div>
           <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label21" runat="server" CssClass="control-label" Text="وضعیت تاهل"></asp:Label>
          <asp:DropDownList ID="vaziatathol" runat="server" 
                                       CssClass="dropdown-header" Font-Bold="True" Height="30px" TabIndex="10" 
                                        Width="190px" >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="متاهل">متاهل</asp:ListItem>
                                        <asp:ListItem Value="مجرد">مجرد</asp:ListItem>
                                        <asp:ListItem Value="مطعلقه">مطعلقه</asp:ListItem>
                                    </asp:DropDownList>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label22" runat="server" CssClass="control-label" Text="مدرک تحصیلی"></asp:Label>
               <asp:TextBox ID="madrak" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
                   <div class="col-md-4">
               <asp:Label ID="Label23" runat="server" CssClass="control-label" Text="رشته تحصیلی"></asp:Label>
               <asp:TextBox ID="reshte"  runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
         

    </div>
         <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label31" runat="server" CssClass="control-label" Text="نام دانشگاه"></asp:Label>
        <asp:TextBox ID="daneshgah" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
				        </div>
           <div class="col-md-4">
                 <asp:Label ID="Label32" runat="server" CssClass="control-label" Text="معدل"></asp:Label>
               				<dx:ASPxTextBox ID="moadel" runat="server" CssClass="form-control" MaxLength="10"     
                   ><masksettings mask="00/00" /><validationsettings display="None"></validationsettings></dx:ASPxTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="معدل باید وارد شود" ControlToValidate="moadel" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
                   <div class="col-md-4">
               <asp:Label ID="Label33" runat="server" CssClass="control-label" Text="تاریخ اخذ مدرک"></asp:Label>
               <asp:TextBox ID="tarixMadrak" placeholder="روز/ماه/سال" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
         

    </div>
                    <div class="row">
                        <div class="col-md-3">
                  <asp:Label ID="Label30" runat="server" CssClass="control-label" Text="شهر محل تحصیل"></asp:Label>
               <asp:TextBox ID="shahrTahsil" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
                        <div class="col-md-3">
                 <asp:Label ID="Label37" runat="server" class="label-control" Text="نوع پرسنل"></asp:Label>
                      <asp:DropDownList ID="cmbRuzShabKar" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                   <asp:ListItem Value="0">انتخاب</asp:ListItem>
                                   <asp:ListItem Value="1">عملیاتی</asp:ListItem>
                                   <asp:ListItem Value="2">ستادی</asp:ListItem>
                                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbRuzShabKar" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
        <div class="col-md-3">
             <asp:Label ID="Label12" runat="server" CssClass="control-label" Text="امور "></asp:Label>
       <dx:ASPxComboBox ID="CmbUnit" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" CssClass="btn btn-default dropdown-toggle" >
    
                      
        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
				        </div>
           <div class="col-md-3">
                  <asp:Label ID="Label13" runat="server" CssClass="control-label" Text="واحد"></asp:Label>
              <dx:ASPxComboBox ID="CmbVahed" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" CssClass="btn btn-default dropdown-toggle" HorizontalAlign="Right" >
    
                      
        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
        </div>
         

    </div>   
             <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="تلفن داخلی "></asp:Label>
              <asp:TextBox ID="teldaxele" runat="server"  
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="تلفن منزل"></asp:Label>
               <asp:TextBox ID="telmanzel" runat="server"   
                    CssClass="form-control" placeholder="تلفن با کد شهر"></asp:TextBox>
        </div>
           <div class="col-md-4">
               <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="موبایل"></asp:Label>
               <asp:TextBox ID="mobile" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>

    </div>
     <div class="row">
			<div class="col-md-12">

                <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="آدرس"></asp:Label>

				<asp:TextBox ID="adres" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>

			    			</div>
           
            		</div>
                   <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label24" runat="server" CssClass="control-label" Text="کدپستی "></asp:Label>
              <asp:TextBox ID="codePosti" runat="server"  
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label25" runat="server" CssClass="control-label" Text="شماره بیمه"></asp:Label>
               <asp:TextBox ID="shBime" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
           <div class="col-md-4">
              
        </div>

    </div>
     
                   <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="شماره پاسپورت "></asp:Label>
              <asp:TextBox ID="shgozarname" runat="server"  
                                            CssClass="form-control"></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label6" runat="server" CssClass="control-label" Text="شماره حساب"></asp:Label>
               <asp:TextBox ID="shHesab" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
           <div class="col-md-4">
               <asp:Label ID="Label10" runat="server" CssClass="control-label" Text="تاریخ ازدواج"></asp:Label>
               <asp:TextBox ID="tarixEzdevaj" placeholder="روز/ماه/سال" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>

    </div>
           <div class="row">
			<div class="col-md-12">

                <asp:Label ID="Label11" runat="server" CssClass="control-label" Text="سوابق نظام و حضور داوطلبانه در جبهه از تاريخ 1359/6/31 لغايت 1367/5/27 (با عنايت به اينكه اگر در زمان جنگ و در جبهه باشد 1.5 برابر احتساب مي گردد)"></asp:Label>

				<asp:TextBox ID="sabegeNezam" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>

			    			</div>
           
            		</div>
       
      
       
      <div class="row">
          
			  <div class="col-md-6">
                    <asp:Label ID="Label14" runat="server" class="label-control" Text="فایل گواهی ایثارگری"></asp:Label>
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;"   Width="100%" />
                  </div>
            <div class="col-md-6">
                
                
                  </div>
               
        </div>
             <div class="row">
             <div class="col-md-12">
<asp:Label ID="Label26" runat="server" class="label-control" Text="توضیحات گواهی ایثارگری"></asp:Label>
                  <asp:TextBox ID="sharhFile" runat="server" class="form-control" ></asp:TextBox>
                 </div>
                 </div>

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
                            <asp:BoundField DataField="id" HeaderText="کد" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="content_name" HeaderText="نام فایل">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="date" HeaderText="تاریخ ثبت فایل">
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
                          
                            <asp:ButtonField CommandName="del" Text="حذف" />
                        </Columns>
                    </asp:GridView>
        <%--<asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="1000" Enabled="False" />--%>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

                  
                          </div>
          
        </div>
               
   
       
        
     
</asp:Panel>
        
   <div class="row">
			
                <div class="col-md-4">
                     <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره" ValidationGroup="a" />
                </div>
                <div class="col-md-4">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" ValidationGroup="b" />
                </div>
                  <div class="col-md-4">

                </div>
                </div>
            
</asp:Content>

