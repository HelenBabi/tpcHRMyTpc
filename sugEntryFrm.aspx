<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="sugEntryFrm.aspx.vb" Inherits="sugEntryFrm" %>


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
    <div class="alert alert-success">
 <h5>همکاران محترم لطفا از ثبت پیشنهاد تکراری خوددراری فرمایید</h5>
  
  </div>
    <div class="row">
           
            
             <div class="col-md-8">
                 <asp:Label ID="Label1" runat="server" class="control-label" Text="موضوع پیشنهاد"></asp:Label>
                    <asp:TextBox ID="subject" class="form-control" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا موضوع وارد شود" ValidationGroup="a" ControlToValidate="subject" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label3" runat="server" class="control-label" Text="نوع پیشنهاد"></asp:Label>
                 <asp:DropDownList ID="sugType"  CssClass="form-control" Height="40px" runat="server">
                 </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="نوع پیشنهاد را انتخاب نمایید" ValidationGroup="a" ControlToValidate="sugType" ForeColor="#FF3300" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
    <div class="row">
           
            <div class="col-md-4">
                 <asp:Label ID="Label4" runat="server" class="control-label" Text="پیش بینی هزینه اجرایی"></asp:Label>
                    <asp:TextBox ID="sugIncome" class="form-control" runat="server"  onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()" placeholder="ریال"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="لطفا پیش بینی هزینه اجرایی وارد شود" ValidationGroup="a" ControlToValidate="sugIncome" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label5" runat="server" class="control-label" Text="پیش بینی درآمد اجرایی"></asp:Label>
                    <asp:TextBox ID="realIncom" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()" placeholder="ریال"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="لطفا پیش بینی درآمد اجرایی وارد شود" ValidationGroup="a" ControlToValidate="realIncom" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            </div>
             <div class="col-md-4">
                 <asp:Label ID="Label8" runat="server" class="control-label" Text="منافع حاصل از پیشنهاد" ></asp:Label>
                    <asp:TextBox ID="benefits" class="form-control" runat="server" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus() " placeholder="ریال"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="لطفا منافع حاصل از پیشنهاد وارد شود" ValidationGroup="a" ControlToValidate="benefits" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                 
            </div>
        </div>
     <div class="row">
           
          
        </div>
    
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label6" runat="server" class="control-label" Text="شرح وضعیت موجود"></asp:Label>
                    <asp:TextBox ID="DescStatus" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="لطفا شرح وضعیت موجود را بنویسید" ValidationGroup="a" ControlToValidate="DescStatus" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
                    <asp:Label ID="Label7" runat="server" class="control-label" Text="شرح روش پیشنهادی"></asp:Label>
                    <asp:TextBox ID="sugMethod" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="لطفا شرح روش پیشنهادی را بنویسید" ValidationGroup="a" ControlToValidate="sugMethod" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label9" runat="server" class="control-label" Text="شرح "></asp:Label>
                    <asp:TextBox ID="Description" class="form-control" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
      <div class="row">
                    
                                <div class="col-md-12">
                 <asp:Label ID="Label11" runat="server" class="control-label" Text="سایر همکاران"></asp:Label>
                    <asp:TextBox ID="partners" class="form-control" runat="server" placeholder="نام و نام خانوادگی و کدپرسنلی سایر نفراتی که در این پیشنهاد شریک هستند را بنویسید"></asp:TextBox>
                
            </div>
        </div>
    <asp:Panel ID="PanelFile" runat="server" GroupingText="ضمایم"  >
      <div class="row">
          
			  <div class="col-md-3">
                    <asp:Label ID="Label2" runat="server" class="label-control" Text="فایل سند"></asp:Label>
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;"   />
                  </div>
            <div class="col-md-9">
                
                <asp:Label ID="Label12" runat="server" class="label-control" Text="توضیحات فایل"></asp:Label>
                  <asp:TextBox ID="sharhFile" runat="server" class="form-control" ></asp:TextBox>
                  </div>
               
        </div>
    <br />
      
     </asp:Panel>
    <br />
    <div class="row">
           
            <div class="col-md-4">
                
            </div>
             <div class="col-md-4">
                <asp:Button ID="Button1" runat="server" Text="ثبت" class="btn btn-success btn-sm waves-effect waves-light"  ValidationGroup="a" /> 
            </div>
             <div class="col-md-4">
                 <asp:Button ID="Button2" runat="server" Text="انصراف" class="btn btn-warning btn-sm waves-effect waves-light"   /> 
                 
                 <asp:HiddenField ID="HiddenFieldSugID" runat="server" />
                 
            </div>
        
        </div>
</asp:Content>

