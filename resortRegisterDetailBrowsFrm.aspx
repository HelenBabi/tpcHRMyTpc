<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="resortRegisterDetailBrowsFrm.aspx.vb" Inherits="resortRegisterDetailBrowsFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="js-persian-cal.min.js" type="text/javascript"></script>
    <link href="js-persian-cal.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function openNewWin(inp1, inp2) {
            var div_ = document.getElementById("pcal1");
            div_.setAttribute('value', inp1);
            var div_ = document.getElementById("pcal2");
            div_.setAttribute('value', inp2);
            //            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');

            //            div_.focus();

        }

</script>
      <div class="row">
			<div class="col-md-12">
               
                </div>
          </div>
<asp:Panel ID="Panel1" runat="server" Enabled="false">
    <div class="row">
			<div class="col-md-3">
  <asp:Label ID="Label16" runat="server" CssClass="control-label" Text="کدملی"></asp:Label>
                 <asp:TextBox ID="codeMelli" runat="server" CssClass="form-control" 
                                        ValidationGroup="a"  ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="کدملی اجباری می باشد" ControlToValidate="codeMelli"  ValidationGroup="a" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            <div class="col-md-3">
                </div>
          <div class="col-md-6">
               <asp:Image ID="Image1" runat="server" Height="150px" Width="200px" />
                </div>
          </div>

    <div class="row">
			<div class="col-md-4">
                 <asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="نام"></asp:Label>
                <asp:TextBox ID="nam" CssClass="form-control"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="nam" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
          <div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="نام خانوادگی"></asp:Label>
                <asp:TextBox ID="lnam" CssClass="form-control"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="lnam" ValidationGroup="a"></asp:RequiredFieldValidator>
              </div>
         <div class="col-md-4">
              <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="جنسیت" ValidationGroup="a"></asp:Label>
             <asp:DropDownList ID="jens" runat="server"   CssClass="btn btn-default dropdown-toggle">
                 <asp:ListItem Value="0">مرد</asp:ListItem>
                 <asp:ListItem Value="1">زن</asp:ListItem>
              </asp:DropDownList>
          
              </div>
         </div>
     <div class="row">
			<div class="col-md-4">
<asp:Label ID="Label6" runat="server" CssClass="control-label" Text="تاریخ مراجعه"></asp:Label>
                 <asp:TextBox ID="tarix" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                             
                </div>
         <div class="col-md-4">
  <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="علت ملاقات"></asp:Label>
             <asp:DropDownList ID="CmbEllat" runat="server" 
                                       CssClass="btn btn-default dropdown-toggle" >
                                    </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="CmbEllat" InitialValue="0" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
         <div class="col-md-4">
             <asp:Label ID="Label10" runat="server" CssClass="control-label" Text="میزبان"></asp:Label>
               <asp:TextBox ID="mizabnTxtt" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                </div>
         </div>
    

      
    
     

     <div class="row">
			<div class="col-md-4">
                 <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="نوع سپرده"></asp:Label>
               <asp:DropDownList ID="CmbSeporedeTyp" runat="server" 
                                       CssClass="btn btn-default dropdown-toggle"> 
                                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="نوع سپرده را انتخاب نمایید" ControlToValidate="CmbSeporedeTyp" InitialValue="0" ValidationGroup="a" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
          <div class="col-md-4">
                
              
              </div>
         <div class="col-md-4">
            
              </div>
         </div>
      <div class="row">
			<div class="col-md-4">
                  <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="شماره کارت"></asp:Label>
              <asp:TextBox ID="txtShomarecart" runat="server" CssClass="form-control" 
                                        ValidationGroup="a" ></asp:TextBox>
                </div>
          	<div class="col-md-4">
                    <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="شماره موبایل"></asp:Label>
                <asp:TextBox ID="txtotag" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                               
                </div>
          	<div class="col-md-4">
                    <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="سریال لب تاپ"></asp:Label>
                <asp:TextBox ID="txtCodemojavez" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                               
                                  </div>
          </div>
    <div class="row">
			<div class="col-md-6">
                 <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="توضیحات"></asp:Label>
                 <asp:TextBox ID="txtNotee" runat="server" CssClass="form-control" ValidationGroup="a"
                                      TextMode="MultiLine" Height="100"></asp:TextBox>
                </div>
        <div class="col-md-6">

            </div>
        </div>
    <div class="row">
			<div class="col-md-3">
                  <asp:Label ID="Label12" runat="server" CssClass="control-label" Text="زمان ورود به شرکت"></asp:Label>
              <asp:TextBox ID="tarixVorud" runat="server" CssClass="form-control" 
                                        ValidationGroup="a" ></asp:TextBox>
                </div>
        <div class="col-md-3">
                  <asp:Label ID="Label15" runat="server" CssClass="control-label" Text="زمان ورود به محل ملاقات"></asp:Label>
              <asp:TextBox ID="vurudUser" runat="server" CssClass="form-control" 
                                        ValidationGroup="a" ></asp:TextBox>
                </div>
          	<div class="col-md-3">
                    <asp:Label ID="Label13" runat="server" CssClass="control-label" Text="زمان خروج از محل ملاقات"></asp:Label>
                <asp:TextBox ID="tarixXuruj1" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                               
                </div>
          	<div class="col-md-3">
                    <asp:Label ID="Label14" runat="server" CssClass="control-label" Text="زمان خروج از شرکت"></asp:Label>
                <asp:TextBox ID="tarixXuruj2" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                               
                                  </div>
          </div>
    <br />
    
</asp:Panel>
         <div class="row">
			  <div class="col-md-6">
                    <asp:Label ID="Label11" runat="server" CssClass="control-label" Text="مدارک"></asp:Label>
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
                          
                            <asp:BoundField DataField="sharh" HeaderText="توضیحات" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="size" HeaderText="حجم فایل/کیلوبایت" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                        </Columns>
                    </asp:GridView>
                                  </div>
               <div class="col-md-6">
                   </div>
          
        </div>
    
      <br />
             <div class="row">
                 <div class="col-md-3">
                  
                </div>
			<div class="col-md-3">
              
                 <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ثبت خروج" ValidationGroup="a" Visible="False" />
                           
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                
                  <div class="col-md-3">
                     
                </div>
                </div>
      
</asp:Content>

