<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnRegisterReportTotalFrm.aspx.vb" Inherits="trnRegisterReportTotalFrm" %>

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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                   class="btn btn-secondary dropdown-toggle" Visible="False">
                                </asp:DropDownList>
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        KeyFieldName="idd" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <settingsbehavior allowfocusedrow="True" />
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
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="ثبت نمرات">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="sabt" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "SabtNomre"%>' Text=" ثبت نمرات" backcolor="lightgreen" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              
                             <dx:GridViewDataTextColumn Caption="دوره" FieldName="doree" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="2" Width="5%" Caption="کد پرسنل" FieldName="codePersoneli">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4" ShowInCustomizationForm="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="6" Width="5%" Caption="نوع قرارداد" FieldName="value">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" 
                                VisibleIndex="9" Width="5%" ShowInCustomizationForm="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="11" Caption="وضعیت" FieldName="statValue">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="12" Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سابقه" FieldName="sabege" VisibleIndex="15" ShowInCustomizationForm="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="14" Width="5%" Name="photo1">
                                <dataitemtemplate>
                                    <asp:Image ID="Image51" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>
     
                        

                             

                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="پذیرش شده ها">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <dx:ASPxGridView ID="gridPaziresh" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="gridPaziresh_HtmlDataCellPrepared"   Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                                                    
                                <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="عدم پذیرش">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "ebtal"%>' Text="عدم پذیرش؟" backcolor="red" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="تعیین سطحS1">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "aa"%>' Text="  S1؟" backcolor="yellow" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="تعیین سطحS2">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "bb"%>' Text="   S2"  backcolor="orange" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="تعیین سطحE1">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "cc"%>' Text="   E1  "  ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="تعیین سطحPI1">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="PI1" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "PI1"%>' Text="   PI1  "  ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                               <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="تعیین سطحUPI1">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="UPI1" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "UPI1"%>' Text="   UPI1  "  ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="تعیین سطحADV">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="ADV" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "ADV"%>' Text="   ADV  "  ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image52" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                              


                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>

                  <dx:TabPage Text="S1">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
                               <dx:ASPxGridView ID="gridA" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"    Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                                                    
                               
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image1" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                                        </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>

                 <dx:TabPage Text="S2">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
                               <dx:ASPxGridView ID="gridB" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"     Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                                                    
                               
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image2" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                                        </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>


                 <dx:TabPage Text="E1">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
                               <dx:ASPxGridView ID="gridC" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"    Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                                                    
                               
                            
                            
                            
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image3" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                                        </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>


                 <dx:TabPage Text="PI1">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
                               <dx:ASPxGridView ID="ASPxGridViewPI1" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"     Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                           
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image4" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                                        </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>




                 <dx:TabPage Text="UP-I1">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
                               <dx:ASPxGridView ID="ASPxGridViewUPI1" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"     Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                    
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image5" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupedColumns="True" ShowGroupPanel="True" />
                    </dx:ASPxGridView>

                                        </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>



                 <dx:TabPage Text="ADV">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
                               <dx:ASPxGridView ID="ASPxGridViewADV" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"   Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                  
                            <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="7%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="2" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="onvan" 
                                VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="tarixEtmam" VisibleIndex="5" Caption="تا تاریخ">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="7" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" 
                                VisibleIndex="8" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="9" Width="5%" Caption="امور" FieldName="UnitsName">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Right">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کدملی" FieldName="codeMelli" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center" font-bold="False">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="وضعیت" FieldName="statValue" ShowInCustomizationForm="True">
                                    
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <cellstyle horizontalalign="Center">
                                     </cellstyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="میزان تاثیرگذاری در وظایف شغلی" FieldName="arzyabiAdmin" VisibleIndex="12">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سطح تعیین شده" FieldName="scoreval" VisibleIndex="13">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="15" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="سرپرست مربوطه" FieldName="sarparast" VisibleIndex="17">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo2" ShowInCustomizationForm="True" VisibleIndex="14" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image6" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <settingsbehavior allowfocusedrow="True" />
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

      <dx:ASPxGridViewExporter ID="ASPxGridViewExporterEtmam" runat="server" 
                    GridViewID="gridPaziresh">
                </dx:ASPxGridViewExporter>

        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

