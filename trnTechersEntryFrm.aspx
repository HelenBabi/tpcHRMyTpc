<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnTechersEntryFrm.aspx.vb" Inherits="trnTechersEntryFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
 </div>
     <div class="row">
        <div class="col-md-12">
 
                      <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
 <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" />
                    <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
                                    
                                  <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
         
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                                                ValidationGroup="b" />
                <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
               <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
        </div>

    </div>
    <br />
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
                     <asp:Label ID="Label26" runat="server" class="label-control" Text="کدپرسنلی"></asp:Label>
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
          <div class="col-md-2">
                  
                 <asp:CheckBox ID="CheckBox1" runat="server" Text="آیا سابقه تدریس شرکتی دارد؟"/>

                 </div>
           <div class="col-md-2">
    <asp:CheckBox ID="ChbActiveCode" runat="server" Text="غیرفعال؟" />
               </div>
         </div>
</asp:Panel>
    <br />
      <div class="row">
            <div class="col-md-12">
              
               <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="techID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="ش پرونده" FieldName="techID" 
                            ShowInCustomizationForm="True" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("techID") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("techID") %>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codemeli" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="7">
                        </dx:GridViewDataCheckColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>
        </div>
             
               
     
       
        </div>
      <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
      <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
</asp:Content>

