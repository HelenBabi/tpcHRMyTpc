<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tpcRegisterStep2.aspx.vb" Inherits="tpcRegisterStep2" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<!DOCTYPE html>

<html dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
     <head runat="server">
       <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Colorlib Templates">
    <meta name="author" content="Colorlib">
    <meta name="keywords" content="Colorlib Templates">
       <title>فرم ثبت درخواست اشتغال در پتروشیمی تبریز</title>
          
     <!-- Vendor CSS-->
    <link href="vendor/mdi-font/css/material-design-iconic-font.min.css" rel="stylesheet" media="all">
    <link href="vendor/font-awesome-4.7/css/font-awesome.min.css" rel="stylesheet" media="all">
    <!-- Font special for pages-->
    

    <!-- Vendor CSS-->
    <link href="vendor/select2/select2.min.css" rel="stylesheet" media="all">
    <link href="vendor/datepicker/daterangepicker.css" rel="stylesheet" media="all">

    <!-- Main CSS-->
    <link href="css/main.css" rel="stylesheet" media="all">
            

          
         <style type="text/css">
             .auto-style1 {
                 width: 100%;
             }
         </style>
            

          
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
   <div class="page-wrapper bg-gra-01 p-t-130 p-b-100 font-poppins">
        <div class="wrapper wrapper--w680">
            <div class="card card-4">
                                 <div class="card-body">
                                     <h6 class="title"> گام دوم - ثبت مشخصات فردی </h6>
                                  <div class="row row-space">
                                       <div class="col-2">
                                <div class="input-group">
                                       <asp:Label ID="Label7" runat="server" class="label"  Text="تاریخ تولد"></asp:Label>
                                <table class="auto-style1">
                                    <tr>
                                         
                                        <td>
                                             <div class="rs-select2 js-select-simple select--no-search">
                                                 <asp:DropDownList ID="ruz" runat="server">
                                                     <asp:ListItem Value="0">روز</asp:ListItem>
                                            <asp:ListItem Value="01">1</asp:ListItem>
                                            <asp:ListItem Value="02">2</asp:ListItem>
                                            <asp:ListItem Value="03">3</asp:ListItem>
                                            <asp:ListItem Value="04">4</asp:ListItem>
                                            <asp:ListItem Value="05">5</asp:ListItem>
                                            <asp:ListItem Value="06">6</asp:ListItem>
                                            <asp:ListItem Value="07">7</asp:ListItem>
                                            <asp:ListItem Value="08">8</asp:ListItem>
                                            <asp:ListItem Value="09">9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            </asp:DropDownList>
                                             <div class="select-dropdown"></div>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" ControlToValidate="ruz" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                         </div>
                                        </td>
                                        <td>
                                             <div class="rs-select2 js-select-simple select--no-search">
                                            <asp:DropDownList ID="mah" runat="server">
                                                <asp:ListItem Value="0">ماه</asp:ListItem>
                                            <asp:ListItem Value="01">1</asp:ListItem>
                                            <asp:ListItem Value="02">2</asp:ListItem>
                                            <asp:ListItem Value="03">3</asp:ListItem>
                                            <asp:ListItem Value="04">4</asp:ListItem>
                                            <asp:ListItem Value="05">5</asp:ListItem>
                                            <asp:ListItem Value="06">6</asp:ListItem>
                                            <asp:ListItem Value="07">7</asp:ListItem>
                                            <asp:ListItem Value="08">8</asp:ListItem>
                                            <asp:ListItem Value="09">9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            </asp:DropDownList>
                                                  <div class="select-dropdown"></div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="mah" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                         </div>
                                                 </td>
                                        <td>
                                              <div class="rs-select2 js-select-simple select--no-search">
                                            <asp:DropDownList ID="sal" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">سال</asp:ListItem>
                                                <asp:ListItem>1368</asp:ListItem>
                                                <asp:ListItem>1369</asp:ListItem>
                                                <asp:ListItem>1370</asp:ListItem>
                                                <asp:ListItem>1371</asp:ListItem>
                                                <asp:ListItem>1372</asp:ListItem>
                                                <asp:ListItem>1373</asp:ListItem>
                                                <asp:ListItem>1374</asp:ListItem>
                                                <asp:ListItem>1375</asp:ListItem>
                                                <asp:ListItem>1376</asp:ListItem>
                                                <asp:ListItem>1377</asp:ListItem>
                                                <asp:ListItem>1378</asp:ListItem>
                                                <asp:ListItem>1379</asp:ListItem>
                                                 <asp:ListItem>1380</asp:ListItem>
                                                <asp:ListItem>1381</asp:ListItem>
                                                <asp:ListItem>1382</asp:ListItem>
                                            </asp:DropDownList>
                                                  <div class="select-dropdown"></div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="sal" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                         </div>
                                           
                                        </td>
                                    </tr>
                                </table>
                                    </div>
                                           </div>
                            
                                      </div>
                                      <div class="row row-space">
<div class="col-2">
                                <div class="input-group">
                                      <asp:Label ID="dds" runat="server" class="label" Text="آخرین مدرک تحصیلی"></asp:Label>
                                      <div class="rs-select2 js-select-simple select--no-search">
                              <asp:DropDownList ID="madrak" runat="server" AutoPostBack="True" >
                  </asp:DropDownList>
                                               <div class="select-dropdown"></div>
                                         </div>
                    <asp:RequiredFieldValidator ID="madrakVal" runat="server" ErrorMessage="*" ControlToValidate="madrak" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                     <asp:Label ID="lblMadrak" runat="server" class="label" ></asp:Label>
                                </div>
                            </div>
                                          <div class="col-2">
                                <div class="input-group">
                                     <asp:Label ID="Label11" runat="server" class="label" Text="رشته"></asp:Label>
                                      <div class="rs-select2 js-select-simple select--no-search">
                              <asp:DropDownList ID="reshte" runat="server"  >
                  </asp:DropDownList>
                                               <div class="select-dropdown"></div>
                                         </div>
                                    </div>
                                              </div>
                                          </div>
                                     <asp:Panel ID="Panel1" runat="server">
                        <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                      <asp:Label ID="Label4" runat="server" class="label"  Text="نام"></asp:Label>

				<asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a"  class="input--style-4"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="nam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                   <asp:Label ID="Label5" runat="server" class="label" Text="نام خانوادگی"></asp:Label>
                <asp:TextBox ID="lname" runat="server" 
                    ValidationGroup="a"  class="input--style-4"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="lname" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        
                        <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                       <asp:Label ID="Label6" runat="server" class="label" Text="نام پدر"></asp:Label>
                <asp:TextBox ID="fatherName" runat="server"   
                    ValidationGroup="a" class="input--style-4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="fatherName" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                     <asp:Label ID="Label1"  runat="server" class="label" Text="کدملی"></asp:Label>

				<asp:TextBox ID="codeMelli" placeholder="کدملی بدون خط تیره" runat="server" 
                    ValidationGroup="a"  class="input--style-4" MaxLength="10" TextMode="Number" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="codeMelli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                      <asp:Label ID="Label2" runat="server" class="label" Text="شماره شناسنامه"></asp:Label>
                <asp:TextBox ID="shsh"  runat="server" 
                    ValidationGroup="a"  class="input--style-4" TextMode="Number"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="shsh" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                     <asp:Label ID="Label3" runat="server" class="label"  Text="تابعیت"></asp:Label>
                                     <div class="rs-select2 js-select-simple select--no-search">
                  <asp:DropDownList ID="tabeiat" runat="server"  >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem>ایرانی</asp:ListItem>
                      <asp:ListItem>غیر ایرانی</asp:ListItem>
                  </asp:DropDownList>
                                          <div class="select-dropdown"></div>
                                         </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="tabeiat" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                           </div>
                        </div>
                                  
                <div class="row row-space">
                            <div class="col-2">
                              
                                </div>
       
                            <div class="col-2">
               
                                <div class="input-group">
                      
    
                                   <asp:Label ID="Label8" runat="server" class="label" Text="جنسیت"></asp:Label>
                            <div class="rs-select2 js-select-simple select--no-search">
                  <asp:DropDownList ID="jens" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="OnSelectedIndexChanged" >
                                         <asp:ListItem Value="1">مرد</asp:ListItem>
                      <asp:ListItem Value="2">زن</asp:ListItem>
                  </asp:DropDownList>
                                          <div class="select-dropdown"></div>
                                         </div>
       
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="jens" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
           
                        </div>
                                                                <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional" Visible="true"
    RenderMode="Inline">
    <ContentTemplate>  
               <div class="row row-space">
                                          
                            <div class="col-2">
                                <div class="input-group">
                                    <asp:Label ID="lblNezam" runat="server" class="label" Text="وضعیت نظام وظیفه" ></asp:Label>
                              <div class="rs-select2 js-select-simple select--no-search">
                   
                                                                              <asp:DropDownList ID="xedmat" runat="server" class="input--style-4" AutoPostBack="true"  OnSelectedIndexChanged="OnSelectedIndexChanged" >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">پایان خدمت</asp:ListItem>
                      <asp:ListItem Value="2">معافیت</asp:ListItem>
                  </asp:DropDownList  
                          >
       
             
                                 <div class="select-dropdown"></div>
                                         </div>                                          
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" ControlToValidate="xedmat" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                 <asp:Label ID="moafiatTypeLbl" runat="server" class="label" Text="نوع معافیت" Visible="false"></asp:Label>
                                     <div class="rs-select2 js-select-simple select--no-search">
                   <asp:DropDownList ID="moafiatTypeCmb" runat="server" class="input--style-4" Visible="False">
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value=" پزشکی"></asp:ListItem>
                      <asp:ListItem Value="کفالت"></asp:ListItem>
                       <asp:ListItem>کسر خدمت</asp:ListItem>
                       <asp:ListItem>سایر</asp:ListItem>
                  </asp:DropDownList>
                                                 
                    <div class="select-dropdown"></div>
                                         </div>     
                                </div>
                            </div>
                      
                        </div>   
                              
                     </ContentTemplate>
                                                <Triggers>
        <asp:AsyncPostBackTrigger ControlID="xedmat" EventName="SelectedIndexChanged" />
    </Triggers>
                                      </asp:UpdatePanel>
                    <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                    <asp:Label ID="Label9" runat="server" class="label" Text="وضعیت تاهل"></asp:Label>
                               <div class="rs-select2 js-select-simple select--no-search">
                  <asp:DropDownList ID="tahol" runat="server"  >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem>مجرد</asp:ListItem>
                      <asp:ListItem>متاهل</asp:ListItem>
                        <asp:ListItem>سایر</asp:ListItem>
                  </asp:DropDownList>
                                          <div class="select-dropdown"></div>
                                         </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="tahol" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                      <asp:Label ID="Label10" runat="server" class="label" Text="وضعیت ایثارگری"></asp:Label>
                                     <div class="rs-select2 js-select-simple select--no-search">
                               <asp:DropDownList ID="shohada" runat="server"  >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem>جز خانواده ایثارگر نمی باشم</asp:ListItem>
                      <asp:ListItem>خانواده معظم شهدا</asp:ListItem>
                                   <asp:ListItem>خانواده معظم جانبازان</asp:ListItem>
                                    <asp:ListItem>خانواده معظم آزادگان</asp:ListItem>
                                    <asp:ListItem>خانواده معظم رزمندگان</asp:ListItem>
                  </asp:DropDownList>
                                          <div class="select-dropdown"></div>
                                         </div>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ControlToValidate="shohada" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        </div>

      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" Visible="true"
    RenderMode="Inline">
    <ContentTemplate>
                    
                                <div class="row row-space">
                                                             
                            <div class="col-2">

                                <div class="input-group">
                                     <asp:Label ID="Label12" runat="server" class="label" Text="آیا جزء فرزندان یا بستگان کارکنان صنعت نفت می باشید"></asp:Label>
                                       <div class="rs-select2 js-select-simple select--no-search">
                   
                                                                              <asp:DropDownList ID="nesbatRadio" runat="server" class="input--style-4" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">بلی</asp:ListItem>
                      <asp:ListItem Value="2">خیر</asp:ListItem>
                  </asp:DropDownList  
                          >
                                      <div class="select-dropdown"></div>
                                         </div>                                        
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ControlToValidate="nesbatRadio" ForeColor="Red" ValidationGroup="a" InitialValue="0"  ></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                   <asp:Label ID="nesbatLbl" runat="server" class="label" Text="نسبت " Visible="false" ></asp:Label>
                                      <div class="rs-select2 js-select-simple select--no-search">
                    <asp:DropDownList ID="nesbatCmb" runat="server" class="input--style-4" Visible="False">
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value=" فرزند"></asp:ListItem>
                      <asp:ListItem>سایر بستگان</asp:ListItem>
                  </asp:DropDownList>
                                           <div class="select-dropdown"></div>
                                                                                  
                                         </div>    
                            </div>
                        </div>
                
                        </div>
                    <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                    
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                  <asp:Label ID="perseneliLbl" runat="server" class="label" Text="شماره پرسنلی " Visible="false" ></asp:Label>
				<asp:TextBox ID="persenliTxt" runat="server" 
                    ValidationGroup="a"  class="input--style-4"  Visible="false" placeholder=" کد پرسنلی صنعت نفت" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
         </ContentTemplate>
                                                <Triggers>
        <asp:AsyncPostBackTrigger ControlID="nesbatRadio" EventName="SelectedIndexChanged" />
    </Triggers>
                                      </asp:UpdatePanel>
                    <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                    <asp:Label ID="Label13" runat="server" class="label" Text="تلفن"></asp:Label>
                <asp:TextBox ID="tell"  placeholder="04134282919 مثال" runat ="server" 
                    ValidationGroup="a"  class="input--style-4"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="tell" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                  <asp:Label ID="Label15" runat="server" class="label" Text="تلفن همراه"></asp:Label>
                <asp:TextBox ID="mobil" runat="server"   
                    ValidationGroup="a" class="input--style-4"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ControlToValidate="mobil" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    <div class="row row-space">
                            <div class="col-2">
                                <div class="input-group">
                                    <asp:Label ID="Label14" runat="server" class="label" Text="ایمیل"></asp:Label>
                                             <asp:TextBox ID="mail" runat="server" 
                    ValidationGroup="a"  class="input--style-4" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group">
                                 <asp:Label ID="Label16" runat="server" class="label" Text="کدپستی"></asp:Label>
                                             <asp:TextBox ID="codePosti" runat="server" placeholder="کدپستی بدون خط تیره "
                    ValidationGroup="a"  class="input--style-4" TextMode="Number"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ControlToValidate="codePosti" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                 
                       <div class="p-t-20">
                           <asp:Label ID="Label17" runat="server" class="label" Text="آدرس"></asp:Label>
                <asp:TextBox ID="address" placeholder=" آدرس کامل:استان-شهر-خیابان-کوچه-پلاک-طبقه-واحد" runat="server" 
                    ValidationGroup="a"  class="input--style-4" Width="100%"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="address" ForeColor="Red" ValidationGroup="a" ></asp:RequiredFieldValidator>
                   <asp:HiddenField ID="HiddenField2" runat="server" />
                        </div>

                
                        <div class="p-t-20">
                           <asp:Button ID="btnPage1" runat="server" class="btn btn--radius-2 btn--blue" Text="تایید - گام بعد" ValidationGroup="a"  />
                   <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>
                   </asp:Panel>
              </div>
          </div>
                   </div>
       </div>
          <!-- Jquery JS-->
      <script src="vendor/jquery/jquery.min.js"></script>
    <!-- Vendor JS-->
    <script src="vendor/select2/select2.min.js"></script>
    <script src="vendor/datepicker/moment.min.js"></script>
    <script src="vendor/datepicker/daterangepicker.js"></script>

    <!-- Main JS-->
    <script src="js/global.js"></script>

    <!-- Main JS-->
    </form>
</body>
</html>
