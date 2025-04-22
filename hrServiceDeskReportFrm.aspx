<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="hrServiceDeskReportFrm.aspx.vb" Inherits="hrServiceDeskReportFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx1" %>

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
                                   class="btn btn-secondary dropdown-toggle">
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
            
       
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "hozur"%>' Text="حضور؟" backcolor="yellow" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "dell"%>' Text="حذف؟" backcolor="red" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "ebtal"%>' Text="غیبت؟" backcolor="orange" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="5%">
                                
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                                VisibleIndex="1" Width="7%">
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
                            <dx:GridViewDataTextColumn Caption=" زمان" FieldName="magsad" VisibleIndex="3" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="startTime" VisibleIndex="4" Caption="از ساعت">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تا ساعت" FieldName="endTime" VisibleIndex="5">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="6" Width="5%" Caption="نوع قرارداد" FieldName="value" ShowInCustomizationForm="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="تلفن داخلی" FieldName="teldaxeli" 
                                VisibleIndex="7" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="8" Width="5%" Caption="موبایل" FieldName="mobile">
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" VisibleIndex="9" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="tozih" VisibleIndex="10" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Caption="کدملی" FieldName="codeMelli">
                                    
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center" font-bold="False">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statValue" VisibleIndex="12" ShowInCustomizationForm="True">
                            </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Name="photo" ShowInCustomizationForm="True" VisibleIndex="15" Width="5%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image5" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                    </dx:ASPxGridView>

<div>
        <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                </dx:ASPxGridViewExporter>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
            </div>
            <div class="div_val">
            </div>
        </div>
    </div>
                </div>
         
          </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
 <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>


        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     
</asp:Content>

