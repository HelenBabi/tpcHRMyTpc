<%@ Page Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="tpcRegisterStepEdit.aspx.vb" Inherits="tpcRegisterStepEdit" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=Panel1.ClientID%>");
        var printWindow = window.open('', '', 'height=400,width=800');
        printWindow.document.write('<html><head><title>DIV Contents</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(panel.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        setTimeout(function () {
            printWindow.print();
        }, 500);
        return false;
    }
    </script>
     <div class="row">
			<div class="col-md-2">

                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">
<asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت" Width="48px" />
                </div>
          <div class="col-md-2">
                <asp:Button ID="btnPage1" runat="server" cssclass="btn-primary"  Text="چاپ" OnClientClick = "return PrintPanel(); " />
                </div>
          <div class="col-md-2">
               <asp:Button ID="Button2" runat="server" cssclass="btn-success"  Text="ویرایش"  />
                </div>
          <div class="col-md-2">
                     <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                </div>

          </div>
     <asp:Panel ID="Panel1" runat="server" Enabled="false">
         <div class="row">
			<div class="col-md-4">
                                      <asp:Label ID="Label24" runat="server" CssClass="control-label"  Text="شماره پرونده"></asp:Label>

				<asp:TextBox ID="reqIDtxt" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ControlToValidate="reqIDtxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
               <div class="col-md-4">
 <asp:Label ID="Label25" runat="server" CssClass="control-label" Text="تاریخ درخواست"></asp:Label>
                <asp:TextBox ID="zamanTxt" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" ControlToValidate="zamanTxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
               <div class="col-md-4">
 <asp:Label ID="Label27"  runat="server" CssClass="control-label" Text="نوع درخواست"></asp:Label>

				<asp:TextBox ID="reqTypetxt" CssClass="form-control" runat="server" 
                    ValidationGroup="a"   MaxLength="10"  ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" ControlToValidate="reqTypetxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
               </div>
           <div class="row">
			<div class="col-md-4">
                                      <asp:Label ID="Label4" runat="server" CssClass="control-label"  Text="نام"></asp:Label>

				<asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="nam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
               <div class="col-md-4">
 <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="نام خانوادگی"></asp:Label>
                <asp:TextBox ID="lname" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="lname" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
               <div class="col-md-4">
 <asp:Label ID="Label1"  runat="server" CssClass="control-label" Text="کدملی"></asp:Label>

				<asp:TextBox ID="codeMelli" CssClass="form-control" runat="server" 
                    ValidationGroup="a"   MaxLength="10"  ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="codeMelli" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
               </div>

       <div class="row">
			<div class="col-md-4">
 <asp:Label ID="Label7" runat="server" CssClass="control-label"  Text="تاریخ تولد"></asp:Label>
                                  <asp:TextBox ID="tarixtavlodtxt" runat="server"   
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ErrorMessage="*" ControlToValidate="tarixtavlodtxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>        
                </div>
           <div class="col-md-4">
   <asp:Label ID="dds" runat="server" CssClass="control-label" Text="آخرین مدرک تحصیلی"></asp:Label>
                                 <asp:TextBox ID="madrakTxt" runat="server"   
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>                                 
                     </div>
           <div class="col-md-4">
<asp:Label ID="Label6" runat="server" CssClass="control-label" Text="نام پدر"></asp:Label>
                <asp:TextBox ID="fatherName" runat="server"   
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="fatherName" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
           </div>
           <div class="row">
			<div class="col-md-4">
 <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="شماره شناسنامه"></asp:Label>
                <asp:TextBox ID="shsh"  runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="shsh" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-4">
  <asp:Label ID="Label3" runat="server" CssClass="control-label"  Text="تابعیت"></asp:Label>
                                                                  <asp:TextBox ID="tabeiat"  runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                </div>
                          <div class="col-md-4">
<asp:Label ID="Label8" runat="server" CssClass="control-label" Text="جنسیت"></asp:Label>
                                            <asp:TextBox ID="jens"  runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>                                              
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="jens" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
           </div>
                                                    
               <div class="row">
			<div class="col-md-4">
 <asp:Label ID="lblNezam" runat="server" CssClass="control-label" Text="وضعیت نظام وظیفه" ></asp:Label>
                                                                               <asp:TextBox ID="xedmat"  runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                                                                           
                                          </div>
           <div class="col-md-4">
 <asp:Label ID="moafiatTypeLbl" runat="server" CssClass="control-label" Text="نوع معافیت" Visible="false"></asp:Label>
                                                 
                 <asp:TextBox ID="moafiatTypeCmb" runat="server" Visible="false"   
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
                </div>
           <div class="col-md-4">

                </div>
           </div>

               <div class="row">
			<div class="col-md-4">
   <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="وضعیت تاهل"></asp:Label>
                                                               <asp:TextBox ID="tahol" runat="server"   
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
                </div>
           <div class="col-md-4">
   <asp:Label ID="Label10" runat="server" CssClass="control-label" Text="وضعیت ایثارگری"></asp:Label>
                 <asp:TextBox ID="shohada"  runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                                                                
                </div>
           <div class="col-md-4">

                </div>
           </div>
 
               <div class="row">
			<div class="col-md-4">
<asp:Label ID="Label12" runat="server" CssClass="control-label" Text="فرزندان یا بستگان کارکنان صنعت نفت"></asp:Label>
                                                          <asp:TextBox ID="nesbatRadio" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"   ></asp:TextBox>
                                 </div>
           <div class="col-md-4">

                                   <asp:Label ID="nesbatLbl" runat="server" CssClass="control-label" Text="نسبت " Visible="false" ></asp:Label>
                                                 <asp:TextBox ID="nesbatCmb" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                </div>
           <div class="col-md-4">
    <asp:Label ID="perseneliLbl" runat="server" CssClass="control-label" Text="شماره پرسنلی " Visible="false" ></asp:Label>
				<asp:TextBox ID="persenliTxt" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"  Visible="false" placeholder=" کد پرسنلی صنعت نفت" ></asp:TextBox>
                </div>
           </div>


               <div class="row">
			<div class="col-md-4">
  <asp:Label ID="Label13" runat="server" CssClass="control-label" Text="تلفن"></asp:Label>
                <asp:TextBox ID="tell"  placeholder="04134282919 مثال" runat ="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="tell" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-4">
 <asp:Label ID="Label15" runat="server" CssClass="control-label" Text="تلفن همراه"></asp:Label>
                <asp:TextBox ID="mobil" runat="server"   
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ControlToValidate="mobil" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
              
           <div class="col-md-4">
  <asp:Label ID="Label14" runat="server" CssClass="control-label" Text="ایمیل"></asp:Label>
                                             <asp:TextBox ID="mail" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                </div>
         </div>

           <div class="row">
			<div class="col-md-4">
  <asp:Label ID="Label16" runat="server" CssClass="control-label" Text="کدپستی"></asp:Label>
                                             <asp:TextBox ID="codePosti" runat="server" placeholder="کدپستی بدون خط تیره "
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ControlToValidate="codePosti" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-8">
   <asp:Label ID="Label17" runat="server" CssClass="control-label" Text="آدرس"></asp:Label>
                <asp:TextBox ID="address" placeholder=" آدرس کامل:استان-شهر-خیابان-کوچه-پلاک-طبقه-واحد" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" Width="100%"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="address" ForeColor="Red" ValidationGroup="a" ></asp:RequiredFieldValidator>
                </div>
         
           </div>

      
     <div class="row">
			<div class="col-md-4">
                  <asp:Label ID="Label20" runat="server" CssClass="control-label" Text="رشته تحصیلی"></asp:Label>
                                             <asp:TextBox ID="reshte" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="reshte" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-4">
                    <asp:Label ID="Label21" runat="server" CssClass="control-label" Text="گرایش تحصیلی"></asp:Label>
                                             <asp:TextBox ID="gerayeshTahsili" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                </div>
           <div class="col-md-4">
                 <asp:Label ID="Label11" runat="server" CssClass="control-label" Text="معدل"></asp:Label>
<asp:TextBox ID="moadel1" runat="server"  
                    ValidationGroup="a"  CssClass="form-control"  ></asp:TextBox>
               				          
                </div>
           </div>
      <div class="row">
			<div class="col-md-4">
  <asp:Label ID="Label19" runat="server" CssClass="control-label" Text="سال اخذ مدرک تحصیلی"></asp:Label>

				<asp:TextBox ID="salMadrak" placeholder="مثال1387" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"  MaxLength="4" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="salMadrak" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
           <div class="col-md-4">
                <asp:Label ID="Label22" runat="server" CssClass="control-label" Text="دانشگاه محل اخذ مدرک"></asp:Label>
                                             <asp:TextBox ID="madrakuni" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ControlToValidate="madrakuni" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>    
                </div>
           <div class="col-md-4">

                </div>
           </div>
  
     <div class="row">
			<div class="col-md-12">
 <asp:Label ID="lblSabegeee" runat="server" CssClass="control-label" Text=" سابقه کار" Visible="False"></asp:Label>
                       <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="idDtl" style="text-align: right" Width="100%" >
                        <Columns>
                                                                    
                            <asp:BoundField DataField="idDtl" HeaderText="کد" Visible="False" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                                                      <asp:BoundField DataField="sherkat" HeaderText="سازمان/شرکت">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="semat" HeaderText="سمت">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="modat" HeaderText="مدت/سال">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                                               
                        </Columns>
                    </asp:GridView>                                
                </div>
           
           </div>
     <div class="row">
			<div class="col-md-12">
  <asp:Label ID="lblPetrooo" runat="server" CssClass="control-label" Text="سابقه کار در پتروشیمی تبریز " Visible="False"></asp:Label>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="idDtl" style="text-align: right" Width="100%" >
                        <Columns>
                                                                    
                            <asp:BoundField DataField="idDtl" HeaderText="کد" Visible="False" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                                                      <asp:BoundField DataField="sherkat" HeaderText="بخش/واحد">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="semat" HeaderText="سمت">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="modat" HeaderText="سال/ماه">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                                <asp:TemplateField>
                         
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                          
                        </Columns>
                    </asp:GridView>
                </div>
           
           </div>
     <div class="row">
			<div class="col-md-12">
  <asp:Label ID="Label26" runat="server" CssClass="control-label" Text="سایر مهارتها"></asp:Label>
                <asp:TextBox ID="maharatha" placeholder=" سایر مهارت ها" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" Height="100px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                </div>
          
           </div>
          <div class="row">
        <div class="col-md-12">
              <asp:Label ID="Label18" runat="server" class="label-control" Text="توضیحات"></asp:Label>
            <asp:TextBox ID="notee"  runat="server"  
                    class="form-control" Height="50px" Width="100%" TextMode="MultiLine"  ></asp:TextBox>
            </div>
         </div>
         <br />
         </asp:Panel>
      <div class="row">
			<div class="col-md-12">
                <asp:Label ID="Label23" runat="server" CssClass="control-label" Text="مدارک"></asp:Label>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
 <asp:BoundField DataField="id" HeaderText="کد" Visible="False" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                  <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود" BackColor="Yellow"> </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                                   
                           
                          
                            <asp:BoundField DataField="content_name" HeaderText="نام فایل">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="typeVlue" HeaderText="نوع مدرک">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                 
                          
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
     <br />
      <div class="row">
			<div class="col-md-4">

                </div>
           <div class="col-md-4">
               

                </div>
           <div class="col-md-4">
<asp:Button ID="Button1" runat="server" Text="برگشت" cssclass="btn-primary"/>
                </div>
           </div>
                 
         
     

</asp:Content>

