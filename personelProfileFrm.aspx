<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="personelProfileFrm.aspx.vb" Inherits="personelProfileFrm" %>
<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container">
                      <div class="alert-success">
		
                <p>لطفا اطلاعات خود را بررسی و در صورت مغایرت اصلاح نمایید و فیلد های خالی را پر و در آخر کلید ذخیره را کلیک کنید
                                  
                </p>
                           <p>تکمیل فرم ذیل صرفا جهت تکمیل بانک اطلاعاتی مدیریت منابع انسانی می باشد</p>
                   </div>   
                          </div>
             
        <br />
    <div class="row">

         <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientIDMode="AutoID" Width="100%" 
        style="margin-top: 0px" TabIndex="3">
             <TabPages>
                 <dx:TabPage Text="اطلاعات شخصی">
                     <activetabstyle font-bold="True">
                     </activetabstyle>
                     <tabstyle width="20%">
                     </tabstyle>
                     <ContentCollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                             <div class="row">
            
                        <div class="col-md-4">
                 <asp:Label ID="Label37" runat="server" class="label-control" Text="نوع پرسنل"></asp:Label>
                      <asp:DropDownList ID="cmbRuzShabKar" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12">
                                   <asp:ListItem Value="0">انتخاب</asp:ListItem>
                                   <asp:ListItem Value="1">عملیاتی</asp:ListItem>
                                   <asp:ListItem Value="2">ستادی</asp:ListItem>
                                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbRuzShabKar" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
        <div class="col-md-4">
             <asp:Label ID="Label12" runat="server" CssClass="control-label" Text="امور "></asp:Label>
       <dx:ASPxComboBox ID="CmbUnit" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name"  runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" class="dropdown-header"  Width="100%" Height="30" Font-Size="12" enabled="False" >
    
                      
        <ItemStyle>
            <SelectedStyle CssClass="selectedItem" />
            <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
                     <dropdownbutton imageposition="Right">
                     </dropdownbutton>
    </dx:ASPxComboBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label13" runat="server" CssClass="control-label" Text="واحد"></asp:Label>
              <dx:ASPxComboBox ID="CmbVahed" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name"  runat="server" ClientIDMode="AutoID" IncrementalFilteringMode="Contains" ValueType="System.String" class="dropdown-header"  Width="100%" Height="30" Font-Size="12" Enabled="False">
    
                      
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
             <asp:Label ID="Label15" runat="server" CssClass="control-label" Text="کدملی "></asp:Label>
              <asp:TextBox ID="codeMelli" runat="server"  
                                            CssClass="form-control" Enabled="False"></asp:TextBox>
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
             <asp:Label ID="Label29" runat="server" CssClass="control-label" Text="سری شناسنامه "></asp:Label>
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
        <div class="col-md-3">
             <asp:Label ID="Label18" runat="server" CssClass="control-label" Text="نوع شناسنامه"></asp:Label>
             <asp:DropDownList ID="vaziatsh" runat="server" 
                                        class="dropdown-header"  Width="100%" Height="30" Font-Size="12"  TabIndex="10" 
                                       >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>اصل</asp:ListItem>
                                        <asp:ListItem>المثنی</asp:ListItem>
                                    </asp:DropDownList>
				        </div>
           <div class="col-md-3">
                  <asp:Label ID="Label19" runat="server" CssClass="control-label" Text="محل صدور شناسنامه"></asp:Label>
               <asp:TextBox ID="mahalesodur" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
        </div>
                   <div class="col-md-3">
               <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="تاریخ صدور"></asp:Label>
               <asp:TextBox ID="tarixSodur" placeholder="روز/ماه/سال" runat="server" 
                    CssClass="form-control"></asp:TextBox>
        </div>
          <div class="col-md-3">
                 <asp:Label ID="Label44" runat="server" CssClass="control-label" Text="وضعیت تاهل"></asp:Label>
          <asp:DropDownList ID="vaziatathol" runat="server" 
                                     class="dropdown-header"  Width="100%" Height="30" Font-Size="12" TabIndex="10" 
                                        >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="متاهل">متاهل</asp:ListItem>
                                        <asp:ListItem Value="مجرد">مجرد</asp:ListItem>
                                        <asp:ListItem Value="مطعلقه">مطعلقه</asp:ListItem>
                                    </asp:DropDownList>
              </div>

    </div>
                             <br />
                               <div class="row">
        <div class="col-md-3">
             <asp:Label ID="Label36" runat="server" class="label-control" Text="وضعیت نظام وظیفه"></asp:Label>
                               <asp:DropDownList ID="xedmat" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12" AutoPostBack="True"  >
                                          <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">پایان خدمت</asp:ListItem>
                      <asp:ListItem Value="2">معافیت</asp:ListItem>
                  </asp:DropDownList>
        </div>
           <div class="col-md-3">
              <asp:Label ID="lblMoafiat" runat="server" class="label-control"  Text="نوع معافیت"></asp:Label>
                          
                     <asp:DropDownList ID="moafitType" runat="server" class="dropdown-header"  Width="100%" Height="30" Font-Size="12"  >
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
              <asp:Label ID="Label27" runat="server"  CssClass="control-label" Text="تاریخ شروع خدمت سربازی"></asp:Label>
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
                                          <asp:DropDownList ID="nesbatRadio" runat="server" class="dropdown-header"  Width="100%" Height="30" Font-Size="12"  AutoPostBack="true" >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">بلی</asp:ListItem>
                      <asp:ListItem Value="2">خیر</asp:ListItem>
                  </asp:DropDownList  
                          >
        </div>
         <div class="col-md-4">
              <asp:Label ID="nesbatLbl" runat="server" class="label-control" Text="نسبت "  ></asp:Label>
                                     
                    <asp:DropDownList ID="nesbatCmb" runat="server" class="dropdown-header"  Width="100%" Height="30" Font-Size="12"  Enabled="False">
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
                    CssClass="form-control" Enabled="False" ></asp:TextBox>
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
                             <br />
                             <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label34" runat="server" class="label-control" Text="وضعیت ایثارگری"></asp:Label>
                            <asp:DropDownList ID="esargariTyp" runat="server" class="dropdown-header"  Width="100%" Height="30" Font-Size="12"  >
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
                                        class="dropdown-header"  Width="100%" Height="30" Font-Size="12"   TabIndex="10" AutoPostBack="True" 
                                         >
                                        <asp:ListItem Value="0">-</asp:ListItem>
                                        <asp:ListItem Value="1">خودم</asp:ListItem>
                                        <asp:ListItem Value="2">فرزند جانباز</asp:ListItem>
                                        <asp:ListItem Value="3">فرزند شهید</asp:ListItem>
                                        <asp:ListItem Value="4">برادر شهید</asp:ListItem>
                                        <asp:ListItem Value="5">خواهر شهید</asp:ListItem>
                                        <asp:ListItem Value="6">فرزند رزمنده</asp:ListItem>
                   <asp:ListItem Value="7">همسر</asp:ListItem>
                                    </asp:DropDownList>
        </div>
         <div class="col-md-4">
          
                                   <asp:Label ID="lblNoeIsargari" runat="server" CssClass="control-label" Text="نوع ایثارگری" ></asp:Label>
             <asp:DropDownList ID="cmbNoeIsargari" runat="server" 
                                        class="dropdown-header"  Width="100%" Height="30" Font-Size="12"   TabIndex="10" 
                                         >
                                        <asp:ListItem>-</asp:ListItem>
                                        <asp:ListItem>رزمنده </asp:ListItem>
                                        <asp:ListItem>جانباز</asp:ListItem>
                                        <asp:ListItem>رزمنده و جانباز</asp:ListItem>
                                        <asp:ListItem>آزاده</asp:ListItem>
                                        <asp:ListItem>جانباز و آزاده</asp:ListItem>
                                        <asp:ListItem>رزمنده و جانبازو آزاده</asp:ListItem>
                                    </asp:DropDownList>        
                                                                                  
                                         </div> 
    
      
        </div>
                             <br />
           <div class="row">
			<div class="col-md-12">

                <asp:Label ID="Label11" runat="server" CssClass="control-label" Text="سوابق نظام و حضور داوطلبانه در جبهه از تاريخ 1359/6/31 لغايت 1367/5/27 (با عنايت به اينكه اگر در زمان جنگ و در جبهه باشد 1.5 برابر احتساب مي گردد)"></asp:Label>

				<asp:TextBox ID="sabegeNezam" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>

			    			</div>
           
            		</div>
                         </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <dx:TabPage Text="اطلاعات تحصیلی">
                     <activetabstyle font-bold="True">
                     </activetabstyle>
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                              <div class="row">
        <div class="col-md-4">
               <asp:Label ID="Label30" runat="server" CssClass="control-label" Text="شهر محل تحصیل"></asp:Label>
               <asp:TextBox ID="shahrTahsil" runat="server"   
                    CssClass="form-control" ></asp:TextBox>
				        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label22" runat="server" CssClass="control-label" Text="مدرک تحصیلی"></asp:Label>
              <asp:DropDownList ID="madrak" runat="server" class="dropdown-header"  Width="100%" Height="30" Font-Size="12"  >
                       <asp:ListItem Value="سیکل"></asp:ListItem>
                   <asp:ListItem Value="ابتدایی"></asp:ListItem>
                      <asp:ListItem Value="سیکل"></asp:ListItem>
                      <asp:ListItem>دیپلم</asp:ListItem>
                         <asp:ListItem>فوق دیپلم</asp:ListItem>
                         <asp:ListItem>لیسانس</asp:ListItem>
                                                   <asp:ListItem>فوق لیسانس</asp:ListItem>
                          <asp:ListItem>دکترا</asp:ListItem>
                       
                  </asp:DropDownList>
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
                     
                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="سوابق آموزشی">
                     <activetabstyle font-bold="True">
                     </activetabstyle>
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            
                                  <div class="row">
        <div class="col-md-12">

                <dx:ASPxGridView ID="trnGrid" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="idd" ClientIDMode="AutoID" Width="930px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="عنوان" FieldName="onvan" VisibleIndex="0" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="dorehayeGozrande" 
                        Caption="دوره های گذرانده" VisibleIndex="1" Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" VisibleIndex="2" Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra"
                        VisibleIndex="3" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ"
                        VisibleIndex="4" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="saat" Caption="ساعت"
                        VisibleIndex="5" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="هماهنگی اجرا" FieldName="hamahangiEjra" VisibleIndex="6" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" VisibleIndex="8" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="tarixPayan" VisibleIndex="9" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="idd" FieldName="idd" Visible="False" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                </dx:ASPxGridView>
                
              </div>
         

    </div>
                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="مدارک" Visible="False">
                     <activetabstyle font-bold="True">
                     </activetabstyle>
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                              <div class="row">
          
			  <div class="col-md-3">
                    <asp:Label ID="Label14" runat="server" class="label-control" Text="فایل گواهی ایثارگری"></asp:Label>
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;"   Width="100%" />
                  </div>
            <div class="col-md-6">
                <asp:Label ID="Label45" runat="server" class="label-control" Text="توضیحات گواهی ایثارگری"></asp:Label>
                  <asp:TextBox ID="sharhFileEsargari" runat="server" class="form-control" ></asp:TextBox>
                
                  </div>
                                   <div class="col-md-3">
                                         <asp:Button ID="btnEsargari" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت فایل گواهی ایثارگری" ValidationGroup="a" />
                                       </div>
               
        </div>
                             <div class="row">
          
			  <div class="col-md-3">
                    <asp:Label ID="Label21" runat="server" class="label-control" Text="فایل تصویر کارت نظام وظیفه"></asp:Label>
                    <asp:FileUpload ID="FileUploadXedmat" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;"   Width="100%" />
                  </div>
            <div class="col-md-6">
                <asp:Label ID="Label26" runat="server" class="label-control" Text="توضیحات کارت نظام وظیفه"></asp:Label>
                  <asp:TextBox ID="sharhFileXedmat" runat="server" class="form-control" ></asp:TextBox>
                
                  </div>
                 <div class="col-md-3">
                       <asp:Button ID="btnXedmat" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت فایل نظام وظیفه" ValidationGroup="a" />
                                       <asp:HiddenField ID="hdnFileTyp" runat="server" Value="0" />
                                       </div>
        </div>
                             <div class="row">
          
			  <div class="col-md-3">
                    <asp:Label ID="Label46" runat="server" class="label-control" Text="فایل تصویر سابقه بیمه"></asp:Label>
                    <asp:FileUpload ID="FileUploadBime" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;"   Width="100%" />
                  </div>
            <div class="col-md-6">
                <asp:Label ID="Label47" runat="server" class="label-control" Text="توضیحات سابقه بیمه"></asp:Label>
                  <asp:TextBox ID="sharhBime" runat="server" class="form-control" ></asp:TextBox>
                
                  </div>
                 <div class="col-md-3">
                       <asp:Button ID="btnBime" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت فایل بیمه" ValidationGroup="a" />
                                       <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                       </div>
        </div>

             <div class="row">
             <div class="col-md-12">

                 </div>
                 </div>
<br />
 <div class="row">

         			  <div class="col-md-12">
           <%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
    <ContentTemplate>--%>
        <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Font-Size="10">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
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
                          
                            <asp:BoundField DataField="typeVlue" HeaderText="نوع فایل">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                                           <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelFile" runat="server"  OnClientClick="return confirm('آیا مطمئن هستید فایل را می خواهید حذف کنید ?')"  OnClick="lnkDelFile_Click" style="color: #FF0000" Text="حذف"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <sortedascendingcellstyle backcolor="#F1F1F1" />
                        <sortedascendingheaderstyle backcolor="Gray" />
                        <sorteddescendingcellstyle backcolor="#CAC9C9" />
                        <sorteddescendingheaderstyle backcolor="#383838" />
                    </asp:GridView>
        <%--<asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="1000" Enabled="False" />--%>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

                  
                          </div>
          
        </div>
               
                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="تحت تکفل">
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                             <div class="row">
			 <div class="col-md-4">
                 <asp:Label ID="Label49" runat="server" class="label-control" Text="نام"></asp:Label>
                  <asp:TextBox ID="nam" runat="server" class="form-control" ></asp:TextBox>
                
                </div>
                <div class="col-md-4">
                     <asp:Label ID="Label50" runat="server" class="label-control" Text="نام خانوادگی"></asp:Label>
                  <asp:TextBox ID="famili" runat="server" class="form-control" ></asp:TextBox>
                </div>
                <div class="col-md-4">
  <asp:Label ID="Label52" runat="server" class="label-control" Text="نسبت"></asp:Label>
                  <asp:DropDownList ID="nesbat" runat="server" class="dropdown-header"   Height="30" Font-Size="12" >
                        </asp:DropDownList>
                </div>
                 
                </div>

  <div class="row">
			 <div class="col-md-4">
                 <asp:Label ID="Label53" runat="server" class="label-control" Text="تاریخ تولد"></asp:Label>
                  <asp:TextBox ID="tarixTavlod" runat="server" class="form-control" ></asp:TextBox>
                
                </div>
                <div class="col-md-4">
                           <asp:Label ID="Label54" runat="server" class="label-control" Text="کدملی"></asp:Label>
                  <asp:TextBox ID="codeMeli" runat="server" class="form-control" ></asp:TextBox>              
                </div>
                <div class="col-md-4">

                </div>
                 
                </div>
  <div class="row">
			 <div class="col-md-4">
               
                
                </div>
                <div class="col-md-4">
                          
                </div>
                <div class="col-md-4">
 <asp:Button runat="server" ID="btnTakafol" class="btn-violet btn-sm waves-effect waves-light"  Text="افزودن"/>
                </div>
                 
                </div>
                             <br />
                             <div class="row">
			 <div class="col-md-12">
              <asp:GridView ID="gridTakafol" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" style="text-align: right" Width="100%" CellPadding="3" Font-Size="10" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                        <Columns>
                           
                            <asp:BoundField DataField="ID" HeaderText="کد" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="fstName" HeaderText="نام ">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="نام خانوادگی">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="Expr1" HeaderText="نسبت" >
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="birthday" HeaderText="تاریخ تولد">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                                           <asp:BoundField DataField="codeMeli" HeaderText="کدملی"></asp:BoundField>
                          
                                           <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdeltakafol" runat="server"  OnClientClick="return confirm('آیا مطمئن هستید رکورد را می خواهید حذف کنید ?')"  OnClick="lnkDelTakafol" style="color: #FF0000" Text="حذف"></asp:LinkButton>
                            </ItemTemplate>
                                               <controlstyle backcolor="#FFFF66" />
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <sortedascendingcellstyle backcolor="#F1F1F1" />
                        <sortedascendingheaderstyle backcolor="#007DBB" />
                        <sorteddescendingcellstyle backcolor="#CAC9C9" />
                        <sorteddescendingheaderstyle backcolor="#00547E" />
                    </asp:GridView>
                
                </div>
                                 </div>
                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="سوابق کار" Visible="False">
                     <activetabstyle font-bold="True">
                     </activetabstyle>
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                             <div class="alert-danger" >
                                 <h5>كاركنان قراردادي‌، سوابق دوران قراردادي با پتروشيمي تبريز را ثبت ننمايند</h5>
                                
                             </div>
 <div class="row">
        <div class="col-md-3">
              <asp:Label ID="Label38" runat="server" class="label-control" Text="سازمان/شرکت محل اشتغال"  ></asp:Label>
              <dx:ASPxComboBox ID="cmbSazman" runat="server" CallbackPageSize="5" class="dropdown-header" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" DropDownRows="5" Font-Size="12pt" ForceDataBinding="true" Height="30px" IncrementalFilteringMode="Contains" TextField="Name" ValueType="System.String" Width="100%">
                  <ItemStyle>
                  <selectedstyle cssclass="selectedItem">
                  </selectedstyle>
                  <hoverstyle cssclass="hoveredItem">
                  </hoverstyle>
                  </ItemStyle>
                  <dropdownbutton imageposition="Right">
                  </dropdownbutton>
              </dx:ASPxComboBox>
            </div>
      <div class="col-md-3">
          <asp:Label ID="Label39" runat="server" class="label-control" Text="عنوان شغل"  ></asp:Label>
           <dx:ASPxComboBox ID="cmbShoghl" runat="server" CallbackPageSize="5" class="dropdown-header" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" DropDownRows="5" Font-Size="12pt" ForceDataBinding="true" Height="30px" IncrementalFilteringMode="Contains" TextField="Name" ValueType="System.String" Width="100%">
               <ItemStyle>
               <selectedstyle cssclass="selectedItem">
               </selectedstyle>
               <hoverstyle cssclass="hoveredItem">
               </hoverstyle>
               </ItemStyle>
               <dropdownbutton imageposition="Right">
               </dropdownbutton>
          </dx:ASPxComboBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cmbShoghl" ErrorMessage="*اجباری*" ForeColor="Red" InitialValue="0" ValidationGroup="b"></asp:RequiredFieldValidator>
            </div>
      <div class="col-md-3">
           <asp:Label ID="Label48" runat="server" class="label-control" Text="نوع سابقه"  ></asp:Label>
                         <asp:DropDownList ID="DropDownListSherkatiTyp" runat="server" AutoPostBack="True" class="dropdown-header" Font-Size="12pt" Height="30px" Width="50%">
                             <asp:ListItem Value="5">انتخاب</asp:ListItem>
                             <asp:ListItem Value="1">داخل پتروشیمی</asp:ListItem>
                             <asp:ListItem Value="0">خارج پتروشیمی</asp:ListItem>
           </asp:DropDownList>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DropDownListSherkatiTyp" ErrorMessage="*اجباری*" ForeColor="Red" InitialValue="5" ValidationGroup="b"></asp:RequiredFieldValidator>
          
            </div>
      <div class="col-md-3">

          <asp:Label ID="lblUnit" runat="server" Text="واحد" CssClass="control-label" Visible="False"  ></asp:Label>
          <dx:ASPxComboBox ID="cmbUnitt" runat="server" CallbackPageSize="5" class="dropdown-header" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" DropDownRows="5" Font-Size="12pt" ForceDataBinding="true" Height="30px" IncrementalFilteringMode="Contains" TextField="Name" ValueType="System.String" Visible="False" Width="100%">
              <ItemStyle>
              <selectedstyle cssclass="selectedItem">
              </selectedstyle>
              <hoverstyle cssclass="hoveredItem">
              </hoverstyle>
              </ItemStyle>
              <dropdownbutton imageposition="Right">
              </dropdownbutton>
          </dx:ASPxComboBox>
                            
             
        
            </div>
     </div>
                 
<br />
                                          <div class="row">
                                              	<div class="col-md-3">
                                                      <asp:Label ID="Label41" runat="server" class="label-control" Text="تاریخ شروع"></asp:Label>
                                                      <dx:ASPxTextBox ID="tarixshoru" runat="server" ClientIDMode="AutoID" CssClass="form-control" HorizontalAlign="Left" placeholder="روز/ماه/سال">
                                                          <masksettings mask="0000/00/00" />
                                                          <validationsettings display="None">
                                                          </validationsettings>
                                                      </dx:ASPxTextBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tarixshoru" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                      </div>
                                       	<div class="col-md-3">
                                            <asp:Label ID="Label42" runat="server" class="label-control" Text="تاریخ پایان"></asp:Label>
                                            <dx:ASPxTextBox ID="tarixpayan" runat="server" ClientIDMode="AutoID" CssClass="form-control" HorizontalAlign="Left" placeholder="روز/ماه/سال">
                                                <masksettings mask="0000/00/00" />
                                                <validationsettings display="None">
                                                </validationsettings>
                                            </dx:ASPxTextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label40" runat="server" class="label-control" Text="مدت کارکرد"></asp:Label>
                                                    <table class="nav-justified">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="karkardDay" runat="server" CssClass="form-control" MaxLength="2" placeholder="چند روز" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="karkardDay" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="karkardMah" runat="server" CssClass="form-control" MaxLength="2" placeholder="چند ماه" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="karkardMah" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="karkardSal" runat="server" CssClass="form-control" MaxLength="2" placeholder="چند سال" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="karkardSal" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label43" runat="server" class="label-control" Text="سابقه پرداخت بیمه"></asp:Label>
                                                    <table class="nav-justified">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="sabegeDay" runat="server" CssClass="form-control" MaxLength="2" placeholder="چند روز" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="sabegeDay" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="sabegeMah" runat="server" CssClass="form-control" MaxLength="2" placeholder="چند ماه" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="sabegeMah" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="sabegeSal" runat="server" CssClass="form-control" MaxLength="2" placeholder="چند سال" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="sabegeSal" ErrorMessage="*اجباری*" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
         </div>
                             <br />
                                      <div class="row">
          
			  
            <div class="col-md-12">
                <asp:Label ID="Label51" runat="server" class="label-control" Text="توضیحات سابقه "></asp:Label>
                  <asp:TextBox ID="tozihsabege" runat="server" class="form-control" ></asp:TextBox>
                
                  </div>
                 
        </div>
                             <br />
                             <div class="row">
               
        <div class="col-md-6">
            
				        </div>
           <div class="col-md-6">
                  <asp:Button ID="ButtonAddSabege" runat="server" class="btn btn-danger btn-sm waves-effect waves-light" Text="ثبت سابقه" ValidationGroup="b" Width="50%" />
                  <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
        </div>
         </div>
                 
<br />           
        <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="lblSabege" runat="server" CssClass="control-label" Text="سوابق پرسنل " Visible="false"></asp:Label>
                                                <asp:GridView ID="GridViewSavabeg" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id" Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="#">
                                                        <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sazmanName" HeaderText="نام شرکت/سازمان" />
                                                        <asp:BoundField DataField="postname" HeaderText="عنوان شغل">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="sherkati" HeaderText="نوع" Visible="False" />
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
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle Font-Size="Medium" BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                               </div>
                      </div>
                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
             </TabPages>
             <contentstyle backcolor="#D9E1EC">
             </contentstyle>
    </dx:ASPxPageControl>

    </div>


    <br />
        
   <div class="row">
			 <div class="col-md-4">

                </div>
                <div class="col-md-4">
                     <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره" ValidationGroup="a" />
                </div>
                <div class="col-md-4">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                 
                </div>
            
</asp:Content>

