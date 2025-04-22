<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnBarnameRiziFrm.aspx.vb" Inherits="trnBarnameRiziFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-8">
              
                 <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
  <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" />
 <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                 <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
                                
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
                   <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
             
        </div>
    <div class="col-md-4">
                <asp:Button ID="Button2" runat="server" Text="رفع مغایرت" class="btn btn-warning btn-sm waves-effect waves-light"   /> 
         </div>
    </div>
      <div class="row">
        
           <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" class="label-control" Text="کد"></asp:Label>
            <asp:TextBox ID="idd" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="idd" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
          <div class="col-md-4">
             <asp:Label ID="Label4" runat="server" class="label-control" Text="عنوان"></asp:Label>
            <asp:TextBox ID="onvan" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="onvan" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
                 <asp:Label ID="Label17" runat="server" class="label-control" Text="حضوری/مجازی"></asp:Label>
                 <asp:DropDownList ID="cmbHozuri" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12" Enabled="False">
                                   <asp:ListItem Value="1">حضوری</asp:ListItem>
                                   <asp:ListItem Value="2">غیر حضوری</asp:ListItem>
                                                                      
                                   </asp:DropDownList>
        </div>

    </div>
    <div class="row">
       <div class="col-md-4">
             <asp:Label ID="Label2" runat="server" class="label-control" Text="واحدهای شرکت کننده"></asp:Label>
            <asp:TextBox ID="vahedha" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="vahedha" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
            <div class="col-md-4">
             <asp:Label ID="Label3" runat="server" class="label-control" Text="تاریخ شروع"></asp:Label>
            <asp:TextBox ID="tarixShuru" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="tarixShuru" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
             <asp:Label ID="Label5" runat="server" class="label-control" Text="تاریخ اتمام"></asp:Label>
            <asp:TextBox ID="tarixEtmam" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="tarixEtmam" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>

    </div>
    <div class="row">
       <div class="col-md-4">
             <asp:Label ID="Label6" runat="server" class="label-control" Text="ساعت برگزاری"></asp:Label>
            <asp:TextBox ID="saat" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="saat" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
            <div class="col-md-4">
             <asp:Label ID="Label7" runat="server" class="label-control" Text="مکان برگزاری"></asp:Label>
            <asp:TextBox ID="makan" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="makan" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>
           <div class="col-md-4">
             <asp:Label ID="Label8" runat="server" class="label-control" Text="مدرس دوره آموزشی"></asp:Label>
            <asp:TextBox ID="modares" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="modares" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
        </div>

    </div>
    <asp:Panel ID="PanelFile" runat="server" GroupingText="ضمایم" Enabled="False" >
      <div class="row">
          
			  <div class="col-md-3">
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" Height="25px" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="400px" Enabled="False" />
                  </div>
            <div class="col-md-3">
                  <asp:TextBox ID="sharhFile" runat="server" class="form-control" ValidationGroup="a" Width="100%" placeholder="توضیحات فایل"></asp:TextBox>
                  </div>
                <div class="col-md-3">
                 
                  </div>
            <div class="col-md-3">
                  </div>
               
        </div>
    <br />
      
     </asp:Panel>
    <div class="row">
        <div class="col-md-4">
                <asp:Label ID="Label12" runat="server" class="label-control" Text="چند روز"></asp:Label>
             <asp:TextBox ID="ruz" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
        </div>
           <div class="col-md-4">
  <asp:Label ID="Label13" runat="server" class="label-control" Text="نوع"></asp:Label>
                <asp:DropDownList ID="faniCmb" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12" Enabled="False">
                                   <asp:ListItem Value="فنی">فنی</asp:ListItem>
                                   <asp:ListItem Value="غیرفنی">غیرفنی</asp:ListItem>
                                   
                                   <asp:ListItem>زبان انگلیسی</asp:ListItem>
                                   <asp:ListItem>HSE</asp:ListItem>
                                   <asp:ListItem>همایش</asp:ListItem>
                                   <asp:ListItem>سیمینار</asp:ListItem>
                                   <asp:ListItem>کنفرنس</asp:ListItem>
                                   <asp:ListItem>نمایشگاه</asp:ListItem>
                                   <asp:ListItem>IT</asp:ListItem>
                                   
                                   </asp:DropDownList>
               <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbDeviceID" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>--%>
        </div>
           <div class="col-md-4">
                  <asp:Label ID="Label14" runat="server" class="label-control" Text="ظرفیت"></asp:Label>
             <asp:TextBox ID="zarfiat" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-4">
                <asp:Label ID="Label9" runat="server" class="label-control" Text="مقدار ساعت مورد نیاز برای این دوره"></asp:Label>
             <asp:TextBox ID="saatNiaz" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
        </div>
           <div class="col-md-4">
  <asp:Label ID="Label10" runat="server" class="label-control" Text="شماره دستگاه ساعت زنی"></asp:Label>
                <asp:DropDownList ID="cmbDeviceID" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12" Enabled="False">
                                   <asp:ListItem Value="0">ندارد</asp:ListItem>
                                   <asp:ListItem Value="90">دستگاه90</asp:ListItem>
                                   <asp:ListItem Value="92">دستگاه92</asp:ListItem>
                                   <asp:ListItem Value="93">دستگاه93</asp:ListItem>
                                   <asp:ListItem Value="95">دستگاه95</asp:ListItem>
                                   </asp:DropDownList>
               <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbDeviceID" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>--%>
        </div>
           <div class="col-md-4">
               <asp:Label ID="Label15" runat="server" class="label-control" Text="وضعیت"></asp:Label>
                <asp:DropDownList ID="statCmb" runat="server"  class="dropdown-header"  Width="100%" Height="30" Font-Size="12" Enabled="False">
                                   <asp:ListItem Value="9">در حال ثبت نام</asp:ListItem>
                                   <asp:ListItem Value="15">درحال برنامه ریزی</asp:ListItem>
                                   <asp:ListItem Value="11">در حال تعیین سطح</asp:ListItem>
                                   <asp:ListItem Value="12">درحال برگزاری</asp:ListItem>
                                   <asp:ListItem Value="13">اتمام دوره</asp:ListItem>
                                
                                   </asp:DropDownList>
        </div>

    </div>
    <div class="row">
          <div class="col-md-8">
                <asp:Label ID="Label16" runat="server" class="label-control" Text="توضیحات"></asp:Label>
               <asp:TextBox ID="tozih" runat="server" 
                    ValidationGroup="a"  class="form-control"  Enabled="false"></asp:TextBox>
                </div>
        <div class="col-md-4">
               <asp:Label ID="Label11" runat="server" class="label-control" Text="فعال/غیرفعال"></asp:Label>
              <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" />
                </div>
         
       
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">

            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="idd"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
 <dx:GridViewDataTextColumn Caption="کد" FieldName="idd" 
                            ShowInCustomizationForm="True" VisibleIndex="1">
     <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("idd")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("idd")%>'></asp:LinkButton>
                            </DataItemTemplate>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   <cellstyle horizontalalign="Center">
     </cellstyle>
                                                   </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="عنوان" FieldName="onvan" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                   </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="واحدهای شرکت کننده" FieldName="vahedha" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                                <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShuru" 
                            ShowInCustomizationForm="True" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="تاریخ اتمام" FieldName="tarixEtmam" 
                            ShowInCustomizationForm="True" VisibleIndex="5">
                              <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="ساعت برگزاری" FieldName="saat" 
                            ShowInCustomizationForm="True" VisibleIndex="6">
                              <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="مکان برگزاری" FieldName="makan" 
                            ShowInCustomizationForm="True" VisibleIndex="7">
                              <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="مدرس" FieldName="modares" 
                            ShowInCustomizationForm="True" VisibleIndex="7">
                               <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساعت موردنیاز" FieldName="saatNiaz" VisibleIndex="9">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="8" Visible="False">
                        </dx:GridViewDataCheckColumn>
                    
                       
                        <dx:GridViewDataTextColumn Caption="شماره دستگاه ساعت زنی" FieldName="deviceID" VisibleIndex="10">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    
                       
                        <dx:GridViewDataTextColumn Caption="تعداد روز" FieldName="ruz" VisibleIndex="11">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نوع" FieldName="fani" VisibleIndex="12">
                        </dx:GridViewDataTextColumn>
                    
                       
                        <dx:GridViewDataTextColumn Caption="ظرفیت" FieldName="zarfiat" VisibleIndex="13">
                        </dx:GridViewDataTextColumn>
                    
                       
                    </Columns>

                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
       </div>
    </div>
    <div class="row">
			  <div class="col-md-12">
                 
                  <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود فایل" BackColor="Yellow"> </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                       <asp:BoundField DataField="tartib" HeaderText="اولویت نمایش">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="content_name" HeaderText="نام فایل" >
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
                          
                            <asp:BoundField DataField="sharh" HeaderText="توضیحات">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="size" HeaderText="حجم فایل/کیلوبایت">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                        </Columns>
                    </asp:GridView>
                          </div>
          
        </div>
      <div class="row">
        <div class="col-md-4">

        </div>
           <div class="col-md-4">
 <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
        </div>
           <div class="col-md-4">
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
        </div>

    </div>
    
</asp:Content>

