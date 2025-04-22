<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="hDataEntryFrm.aspx.vb" Inherits="hDataEntryFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label4" runat="server" class="label-control" Text="نام"></asp:Label>
            <asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="nam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                   
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label5" runat="server" class="label-control" Text="نام خانوادگی"></asp:Label>
            <asp:TextBox ID="lname" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="lname" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
         <div class="col-md-4">
               <asp:Label ID="Label6" runat="server" class="label-control" Text="نام پدر"></asp:Label>
            <asp:TextBox ID="fatherName" runat="server" 
                    ValidationGroup="a"  class="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="fatherName" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
        </div>
     <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" class="label-control" Text="کد ملی"></asp:Label>
            <asp:TextBox ID="codeMelli" runat="server" placeholder="کدملی بدون خط تیره"
                    ValidationGroup="a"  class="form-control" MaxLength="10" TextMode="Number" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="codeMelli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
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
                                         <asp:ListItem Value="1">مرد</asp:ListItem>
                      <asp:ListItem Value="2">زن</asp:ListItem>
                  </asp:DropDownList>
        </div>
        </div>

    <div class="row">
        <div class="col-md-3">
             <asp:Label ID="Label24" runat="server" class="label-control" Text="وضعیت ایثارگری"></asp:Label>
                            <asp:DropDownList ID="shohada" runat="server" class="btn btn-secondary dropdown-toggle" >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem>جز خانواده ایثارگر نمی باشم</asp:ListItem>
                      <asp:ListItem>خانواده معظم شهدا</asp:ListItem>
                                   <asp:ListItem>خانواده معظم جانبازان</asp:ListItem>
                                    <asp:ListItem>خانواده معظم آزادگان</asp:ListItem>
                                    <asp:ListItem>خانواده معظم رزمندگان</asp:ListItem>
                  </asp:DropDownList>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label25" runat="server" class="label-control" Text="آیا جزء فرزندان یا بستگان کارکنان صنعت نفت می باشد"></asp:Label>
                                                                            <asp:DropDownList ID="nesbatRadio" runat="server" class="btn btn-secondary dropdown-toggle"  AutoPostBack="true" >
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">بلی</asp:ListItem>
                      <asp:ListItem Value="2">خیر</asp:ListItem>
                  </asp:DropDownList  
                          >
        </div>
         <div class="col-md-2">
            <asp:Label ID="nesbatLbl" runat="server" class="label-control" Text="نسبت " Visible="false" ></asp:Label>
                                     
                    <asp:DropDownList ID="nesbatCmb" runat="server" class="btn btn-secondary dropdown-toggle" Visible="False">
                      <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value=" فرزند"></asp:ListItem>
                      <asp:ListItem>سایر بستگان</asp:ListItem>
                  </asp:DropDownList>
                                          
                                                                                  
                                         </div> 
          <div class="col-md-3">
                    <asp:Label ID="perseneliLbl" runat="server" class="label-control" Text="شماره پرسنلی " Visible="false" ></asp:Label>
				<asp:TextBox ID="persenliTxt" runat="server" 
                    ValidationGroup="a"  class="input--style-4"  Visible="false" placeholder=" کد پرسنلی صنعت نفت" ></asp:TextBox>
              </div>
      
        </div>
    <br />
     <div class="row">
        <div class="col-md-4">
             <asp:Label ID="Label10" runat="server" class="label-control" Text="وضعیت نظام وظیفه"></asp:Label>
                               <asp:DropDownList ID="xedmat" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                          <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="1">پایان خدمت</asp:ListItem>
                      <asp:ListItem Value="2">معافیت</asp:ListItem>
                  </asp:DropDownList>
        </div>
         <div class="col-md-4">
              <asp:Label ID="Label11" runat="server" class="label-control" Text="وضعیت تاهل"></asp:Label>
                               <asp:DropDownList ID="tahol" runat="server"  class="btn btn-secondary dropdown-toggle"  >
                                          <asp:ListItem Value="0">انتخاب کنید</asp:ListItem>
                       <asp:ListItem>مجرد</asp:ListItem>
                      <asp:ListItem>متاهل</asp:ListItem>
                        <asp:ListItem>سایر</asp:ListItem>
                  </asp:DropDownList>
        </div>
         <div class="col-md-4">
             <asp:Label ID="Label12" runat="server" class="label-control" Text="تلفن ثابت"></asp:Label>
            <asp:TextBox ID="tell"  placeholder="04134282919 مثال" runat="server"  
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
               <asp:Label ID="Label15" runat="server" class="label-control" Text="کدپستی"></asp:Label>
            <asp:TextBox ID="codePosti"  runat="server"  
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
              <table class="auto-style1">
                                    <tr>
                                          <asp:Label ID="Label2" runat="server" class="label-control" Text="تاریخ تولد"></asp:Label>
                                        <td>
                                             <div class="rs-select2 js-select-simple select--no-search">
                                                
                                                 <asp:DropDownList ID="ruz" runat="server" class="btn btn-secondary dropdown-toggle">
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
                                            <asp:DropDownList ID="mah" runat="server" class="btn btn-secondary dropdown-toggle">
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
                                            <asp:DropDownList ID="sal" runat="server"  class="btn btn-secondary dropdown-toggle">
                                                <asp:ListItem Value="0">سال</asp:ListItem>
                                                 <asp:ListItem>1360</asp:ListItem>
                                                <asp:ListItem>1361</asp:ListItem>
                                                <asp:ListItem>1362</asp:ListItem>
                                                <asp:ListItem>1363</asp:ListItem>
                                                <asp:ListItem>1364</asp:ListItem>
                                                <asp:ListItem>1365</asp:ListItem>
                                                <asp:ListItem>1366</asp:ListItem>
                                                <asp:ListItem>1367</asp:ListItem>
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
                                                   <asp:ListItem>1383</asp:ListItem>
                                                <asp:ListItem>1384</asp:ListItem>
                                                <asp:ListItem>1385</asp:ListItem>
                                            </asp:DropDownList>
                                                  <div class="select-dropdown"></div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="sal" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                                         </div>
                                           
                                        </td>
                                    </tr>
                                </table>
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label1" runat="server" class="label-control" Text="آخرین مدرک تحصیلی"></asp:Label>
                                                
                              <asp:DropDownList ID="madrak" runat="server" AutoPostBack="True" class="btn btn-secondary dropdown-toggle">
                  </asp:DropDownList>
                                            
                                         
                </div>
          <div class="col-md-4">
               <asp:Label ID="Label3" runat="server" class="label-control" Text="رشته تحصیلی"></asp:Label>
                <asp:DropDownList ID="reshte" runat="server" class="btn btn-secondary dropdown-toggle" >
                  </asp:DropDownList>
                </div>
         

          </div>
      <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label17" runat="server" class="label-control" Text="دانشگاه محل اخذ مدرک"></asp:Label>
             <asp:TextBox ID="madrakuni"  runat="server"  
                    class="form-control"  ></asp:TextBox>
            </div>
            <div class="col-md-4">
                  <asp:Label ID="Label18" runat="server" class="label-control" Text="معدل"></asp:Label>
          <table class="auto-style1">
                                        <tr>
                                            <td> <asp:TextBox ID="moadel1" runat="server" class="form-control" placeholder=" اعشار" 
                    ValidationGroup="a"  MaxLength="2" TextMode="Number" min="1" max="99" Height="30px" Width="100px" ></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="نامعتبر" ControlToValidate="moadel1" Type="Integer" MaximumValue="99" MinimumValue="0" ValidationGroup="a" ForeColor="Red" SetFocusOnError="True"></asp:RangeValidator>
                                              
                                            </td>
                                            <td>
                                                 <asp:Label ID="Label20" runat="server" class="label" Text="/"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="moadel2" runat="server" 
                    ValidationGroup="a"  class="form-control" placeholder=" صحیح" TextMode="Number"  Height="30px" Width="100px" ></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="نامعتبر" ControlToValidate="moadel2" Type="Integer" MaximumValue="20" MinimumValue="1" ValidationGroup="a" ForeColor="Red" SetFocusOnError="True"></asp:RangeValidator>
                                               
                                            </td>
                                        </tr>
                                    </table>
            </div>
            <div class="col-md-4">
                 <asp:Label ID="Label19" runat="server" class="label-control" Text="گرایش تحصیلی"></asp:Label>
                 <asp:TextBox ID="gerayeshTahsili"  runat="server"  
                    class="form-control"  ></asp:TextBox>
            </div>

          </div>
     <div class="row">
        <div class="col-md-4">
              <asp:Label ID="Label22" runat="server" class="label-control" Text="معرف"></asp:Label>
             <asp:TextBox ID="moaref" placeholder=" نام شخص / سازمان / ارگان" runat="server"  class="form-control" ></asp:TextBox>
            </div>
             <div class="col-md-4">
                 </div>
          <div class="col-md-4">
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
        <div class="col-md-12">
              <asp:Label ID="Label23" runat="server" class="label-control" Text="توضیحات"></asp:Label>
            <asp:TextBox ID="notee"  runat="server"  
                    class="form-control" Height="50px" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         </div>
    <br />
      <div class="row">
            <div class="col-md-4">
              
                 <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
        <div class="col-md-8">
 <asp:Button ID="BtnAdd" runat="server" Text="ثبت" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a"  />
            <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
        </div>
       
               
     
       
        </div>
</asp:Content>

