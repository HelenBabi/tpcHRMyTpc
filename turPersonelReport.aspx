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
                                        <div class="col-md-2">
<asp:ImageButton ID="ImageButton3" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="برگشت به صفحه اصلی" Width="48px" />
                </div>
 <div class="col-md-2">
                
                </div>

          <div class="col-md-8">
                                      
			 	
            
		</div>
        </div>
                             <br />
                                <div class="row">
			<div class="col-md-12">
            <table class="style1">
                <tr>
                    <td style="text-align: center" class="auto-style1">
                        
                        <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" Height="25px" Width="25px"  />
                    </td>
                    <td style="text-align: center" class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                انتخاب:
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
      <div class="row">
			<div class="col-md-12">

          
      
            
            
            
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="idd" ClientIDMode="AutoID"  OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared"  Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" />
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
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" 
                                ShowInCustomizationForm="True" VisibleIndex="1" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="family" 
                                VisibleIndex="2" Width="7%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="کد پرسنل" FieldName="codePersoneli" 
                                VisibleIndex="3" Width="5%">
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
                            <dx:GridViewDataTextColumn Caption="محل و زمان" FieldName="magsad" VisibleIndex="4" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="پذیرش">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "pazir"%>' Text="پذیرش؟" backcolor="lightgreen" ></asp:LinkButton>
                                </dataitemtemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="عدم پذیرش">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "ebtal"%>' Text="عدم پذیرش بعلت کمبود سابقه؟" backcolor="orange" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn 
                                VisibleIndex="0" Width="5%" Caption="عدم پذیرش">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# eval("idd") %>' CommandName='<%# "ebtal2"%>' Text="عدم پذیرش بعلت برنده نشدن در قرعه کشی؟" backcolor="yellow" ></asp:LinkButton>
                                </dataitemtemplate>
                                  <HeaderStyle HorizontalAlign="Center" />
                                  <cellstyle horizontalalign="Center">
                                  </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="stat" Visible="False" VisibleIndex="6" Width="0%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statValue" VisibleIndex="7" Width="3%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد رزرو " FieldName="tedad" ShowInCustomizationForm="True" VisibleIndex="9" Width="2%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="تعداد تحت تکفل" FieldName="tedadTakafol" ShowInCustomizationForm="True" VisibleIndex="10" Width="2%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn VisibleIndex="11" Width="5%" Caption="تلفن داخلی" FieldName="teldaxeli">
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="موبایل" FieldName="mobile" VisibleIndex="12" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="امور" FieldName="UnitsName" VisibleIndex="13" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="واحد" FieldName="omur" VisibleIndex="14" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="16" Width="5%" Caption="سابقه" FieldName="sabege">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="tozih" VisibleIndex="18" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <cellstyle horizontalalign="Right">
                                </cellstyle>
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
                        <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowGroupedColumns="True" ShowGroupPanel="True" />
                        <settingsdetail exportmode="All" showdetailrow="True" />
                        <templates>
                            <detailrow>
                                <dx1:ASPxGridView ID="takafolGRD" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" EnableCallBacks="False" KeyFieldName="ID" Width="80%" OnBeforePerformDataSelect="grdFarei_BeforePerformDataSelect">
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



                </ContentTemplate>
            </asp:UpdatePanel>
            
          <div>
                  </div>
            

       
                

               

        
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

      
    </div>
    </div>

    <br />
          <div class="row">
			<div class="col-md-12">

                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:BoundField DataField="magsad" HeaderText="محل وزمان">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tedad" HeaderText="تعداد پرسنل">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tedadSabti" HeaderText="جمع تعداد رزرو" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle Font-Size="Medium" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <sortedascendingcellstyle backcolor="#F1F1F1" />
                                                    <sortedascendingheaderstyle backcolor="Gray" />
                                                    <sorteddescendingcellstyle backcolor="#CAC9C9" />
                                                    <sorteddescendingheaderstyle backcolor="#383838" />
                                                </asp:GridView>

</div>
              </div>


</asp:Content>

