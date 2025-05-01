<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overhaulReportFrm.aspx.vb" Inherits="overhaulReportFrm" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
			<div class="col-md-2">

                </div>
          <div class="col-md-2">
                </div>
          <div class="col-md-2">
<asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                </div>
          <div class="col-md-2">
              <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="نمایش همه رکوردها" Visible="False" />
                </div>
          <div class="col-md-2">

                </div>
          <div class="col-md-2">
                     <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
                </div>

          </div>

     <div class="row">
			<div class="col-md-12">
            <table class="style1">
                <tr>
                    <td style="text-align: center">
                        <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" Height="25px" Width="25px"  />
                    </td>
                    <td style="text-align: center">
                 
                    </td>
                </tr>
                </table>
                </div>
         </div>
    <br />

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
     <div class="row">
         
			<div class="col-md-12">
            

                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientIDMode="AutoID" Width="100%" 
        style="margin-top: 0px" TabIndex="3">
             <TabPages>
                 <dx:TabPage Name="trn" Text="لیست کلی">
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True"/>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>
     
                        

                             

                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="تایید پیمانکار">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton4" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <%--تایدد نشده توسط پیمانکار--%>
                 <dx:TabPage Text="تایید نشده پیمانکار">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True"/>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton5" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton6" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <%--تایید واحدها--%>
                 <dx:TabPage Text="تایید واحدها">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView4" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True"/>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton7" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton8" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <%--عدم تایید واحدها--%>
                 <dx:TabPage Text="عدم تایید واحدها">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView5" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True"/>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton9" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton10" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <%--عدم تایید برنامه ریزی--%> 
                 <dx:TabPage Text="عدم تایید برنامه ریزی">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView6" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True"/>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton11" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton12" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
                 <%--ارجا به واحدها توسط برنامه ریزی--%>
                 <dx:TabPage Text="ارجا به واحدها توسط برنامه ریزی">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="ASPxGridView7" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="Id" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True"/>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                     <dx:GridViewDataTextColumn Caption="کد پیگیری" FieldName="Id" 
                            UnboundType="Integer" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton13" runat="server" 
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                    Text='<%# Eval("Id")%> ' CssClass="btn btn-light btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                       <dx:GridViewDataTextColumn Caption="کد ملی" FieldName="Nationality" 
                           
                            UnboundType="Integer" VisibleIndex="0" Width="150px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton14" runat="server" 
                                     Text='<%# Eval("Nationality")%> '
                                    CommandArgument='<%# Eval("Id")%>' CommandName='<%# "moshahede"%>' 
                                   CssClass="btn btn-link btn-sm"></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="شناسنامه" FieldName="shSh" VisibleIndex="4" />
                    <dx:GridViewDataTextColumn Caption="تولد" FieldName="BirthDay" VisibleIndex="5" />
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="BankCardNo" VisibleIndex="6" />
                    <dx:GridViewDataTextColumn Caption="شماره حساب" FieldName="IBAN" VisibleIndex="7" />
                    <dx:GridViewDataTextColumn Caption="نوع بیمه" FieldName="bime" VisibleIndex="9" />
                    <dx:GridViewDataTextColumn Caption="شماره بیمه" FieldName="SSN" VisibleIndex="8" />
<dx:GridViewDataTextColumn FieldName="Mobile" ShowInCustomizationForm="True" Caption="موبایل" VisibleIndex="10"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تلفن" FieldName="Tell" VisibleIndex="11" />
                    <dx:GridViewDataTextColumn Caption="کدپستی" FieldName="PostalCode" VisibleIndex="12" />
                    <dx:GridViewDataTextColumn Caption="مدرک" FieldName="madrak" VisibleIndex="13" />
                    <dx:GridViewDataTextColumn Caption="رشته" FieldName="reshte" VisibleIndex="14" />
                     <dx:GridViewDataTextColumn Caption="گرایش" FieldName="EducationalOrientation" VisibleIndex="15">
                     </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="تاهل" FieldName="martialStatus" VisibleIndex="16" />
                    <dx:GridViewDataTextColumn Caption="وظیفه" FieldName="MilitaryName" VisibleIndex="17" />
                    <dx:GridViewDataTextColumn Caption="ایثارگری" FieldName="isargari" VisibleIndex="18" />
                    <dx:GridViewDataTextColumn Caption="سابقه اورهال" FieldName="overhalHistory" VisibleIndex="19" />
                    <dx:GridViewDataTextColumn Caption="سمت اورهال قبلی" FieldName="JobPosition" VisibleIndex="20" />
                    <dx:GridViewDataTextColumn Caption="استان" FieldName="province" VisibleIndex="21" />
                    
                     <dx:GridViewDataTextColumn Caption="شهر" FieldName="city" VisibleIndex="22">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="آدرس" FieldName="Address" ShowInCustomizationForm="True" VisibleIndex="23">
                     </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="Title" ShowInCustomizationForm="True" VisibleIndex="24">
                     </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="State" Visible="False" />
                     <dx:GridViewDataTextColumn Caption="کارتابل" FieldName="omur" VisibleIndex="25">
                     </dx:GridViewDataTextColumn>
                </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>
             </TabPages>
           
    </dx:ASPxPageControl>

      </div>
                  
         </div>

       
  
             

         

         
                    
                </ContentTemplate>
            </asp:UpdatePanel>

         <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGridView1">
                </dx:ASPxGridViewExporter>

        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

