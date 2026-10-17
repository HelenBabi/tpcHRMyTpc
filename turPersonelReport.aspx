<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="turPersonelReport.aspx.vb" Inherits="turPersonelReport" %>
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
<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web.ASPxPopupControl"
    tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
                                 <div class="row">
                   
 <div class="col-md-4">
     <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="انتخاب همه تورها" TextAlign="Left" Visible="False" />
                </div>

          <div class="col-md-4">
                                      
			 	   <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" Height="25px" Width="25px"  />
            
		</div>
                                                          <div class="col-md-4">
<asp:ImageButton ID="ImageButton3" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                </div>
        </div>
                             <br />
                                <div class="row">
			<div class="col-md-12">
            <table class="style1">
                <tr>
                  <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                    <td style="text-align: center" class="auto-style1">
                        
                       
                    </td>
                    <td style="text-align: center" class="auto-style1">
                       
                                انتخاب:
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                   class="btn btn-secondary dropdown-toggle">
                                </asp:DropDownList>
                              
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                        
                    </td>
                      <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                </tr>
                </table>
                </div>
         </div>
                             <br />
      <div class="row">
			<div class="col-md-12">

                      
        <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" />
                                                  <settingsbehavior allowfocusedrow="True" />
                        <settings showfilterrow="True" ShowHorizontalScrollBar="True" VerticalScrollBarStyle="Virtual"/>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                                ShowInCustomizationForm="True" VisibleIndex="3" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                                VisibleIndex="4" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="5" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نوع قرارداد" FieldName="value" 
                                VisibleIndex="5" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="محل و زمان" FieldName="magsad" VisibleIndex="4" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                        

                            <dx:GridViewDataTextColumn FieldName="stat" Visible="False" VisibleIndex="6" Width="0%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statValue" VisibleIndex="7" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد درخواستی" FieldName="tedad" ShowInCustomizationForm="True" VisibleIndex="9" Width="7%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد تحت تکفل" FieldName="tedadTakafol" ShowInCustomizationForm="True" VisibleIndex="7" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" VisibleIndex="12" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" VisibleIndex="13" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" VisibleIndex="14" Width="5%" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="16" Width="5%" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="tozih" VisibleIndex="15" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="photo" ShowInCustomizationForm="True" VisibleIndex="16" Width="7%">
                                <dataitemtemplate>
                                    <asp:Image ID="Image5" runat="server" Height="96px" Width="64px" />
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle font-bold="False" horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                     
          <dx:GridViewDataTextColumn Caption="سابقه" VisibleIndex="17" Width="10%">
    <DataItemTemplate>
        <asp:Button ID="btnShowHistory" runat="server" Text="سابقه"
            OnClientClick='<%# "ShowPopup(""" & Eval("codePersoneli") & """); return false;" %>'
            CssClass="btn btn-info btn-sm" />
    </DataItemTemplate>
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Center" />
          </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn Caption="پذیرش" VisibleIndex="17" Width="10%">
    <DataItemTemplate>
        <asp:Button ID="btnAccept" runat="server" Text="✔ پذیرش" 
            CommandName="pazir" CommandArgument='<%# Eval("idd") %>'
            CssClass="btn btn-success btn-sm" />
    </DataItemTemplate>
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Center" />
</dx:GridViewDataTextColumn>

<dx:GridViewDataTextColumn Caption="عدم پذیرش (سابقه کم)" VisibleIndex="18" Width="10%">
    <DataItemTemplate>
        <asp:Button ID="btnReject1" runat="server" Text="✘ عدم پذیرش - سابقه"
            CommandName="ebtal" CommandArgument='<%# Eval("idd") %>'
            CssClass="btn btn-danger btn-sm" />
    </DataItemTemplate>
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Center" />
</dx:GridViewDataTextColumn>

<dx:GridViewDataTextColumn Caption="عدم پذیرش (قرعه کشی)" VisibleIndex="19" Width="10%">
    <DataItemTemplate>
        <asp:Button ID="btnReject2" runat="server" Text="✘ عدم پذیرش - قرعه‌کشی"
            CommandName="ebtal2" CommandArgument='<%# Eval("idd") %>'
            CssClass="btn btn-warning btn-sm" />
    </DataItemTemplate>
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Center" />
</dx:GridViewDataTextColumn>

                        </Columns>
                      <settingsbehavior allowfocusedrow="True" AutoFilterRowInputDelay="12000"/>
                        <Settings ShowFilterRow="True" />
                        <settingsdetail exportmode="All" showdetailrow="True" />
                        <templates>
                            <detailrow>
                                <dx1:ASPxGridView ID="takafolGRD" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID"  KeyFieldName="ID" Width="100%" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect">
                                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                                    <Columns>
                                        <dx1:GridViewDataTextColumn Caption="نسبت" FieldName="Expr1" VisibleIndex="2" Width="5%">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="birthday" VisibleIndex="3" Width="10%">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx1:GridViewDataTextColumn>
                                        <dx1:GridViewDataTextColumn Caption="نام" FieldName="fstName" ShowInCustomizationForm="True" VisibleIndex="0" Width="5%">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <cellstyle horizontalalign="Center">
                                            </cellstyle>
                                        </dx1:GridViewDataTextColumn>
                                      
                                    </Columns>
                                    <settingsbehavior allowfocusedrow="True" />
                                    <settingspager pagesize="20">
                                    </settingspager>
                                </dx1:ASPxGridView>
                            </detailrow>
                        </templates>
                    </dx:ASPxGridView>



            <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>
            
          <div>
                  </div>

                      

            <asp:PlaceHolder ID="divPersonelHistory" runat="server" />
                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />


       <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" GridViewID="ASPxGridView1">

</dx:ASPxGridViewExporter>
                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView2">

</dx:ASPxGridViewExporter>


        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

      
    </div>
    </div>

    <br />
    <asp:Panel ID="Panel1" runat="server" GroupingText="آمار کلی">
          <div class="row">
			<div class="col-md-6">
                <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="tedad" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <settingsbehavior allowfocusedrow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <settings showfilterrow="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowGroupedColumns="True" ShowGroupPanel="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <Columns>

   <dx:GridViewDataTextColumn Caption="محل و زمان" FieldName="magsad" VisibleIndex="0" Width="30px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد پرسنل" FieldName="tedad" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="جمع تعداد رزرو" FieldName="tedadSabti" 
                                VisibleIndex="2" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                       
                         
                                                                       
                         
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
                        <settingsdetail exportmode="All" />
                        <templates>
                            <detailrow>
                                
                            </detailrow>
                        </templates>
                    </dx:ASPxGridView>
         

</div>
              <div class="col-md-6">
                                                   
			 	   <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" Height="25px" Width="25px"  />
            	
                  </div>
              </div>
        </asp:Panel>
    <br />
    <dx:ASPxPopupControl ID="PopupHistory" runat="server" ClientInstanceName="popupHistory"
    Width="90%" Height="90%" CloseAction="CloseButton" ShowCloseButton="true"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    HeaderText="سابقه پرسنل" Modal="True">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <asp:Literal ID="litHistory" runat="server" />
            <button type="button" class="btn btn-danger" onclick="popupHistory.Hide(); return false;">بستن</button>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<script type="text/javascript">
    function ShowPopup(codePersoneli) {
        __doPostBack('ShowHistory', codePersoneli);
    }
</script>
  <!-- دکمه بستن درست‌شده -->
<!-- دکمه بستن درست‌شده -->
<%-- <button type="button" class="btn btn-danger" onclick="popupHistory.Hide(); return false;">بستن</button>--%>



</asp:Content>

