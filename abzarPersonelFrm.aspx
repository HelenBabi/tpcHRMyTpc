<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="abzarPersonelFrm.aspx.vb" Inherits="abzarPersonelFrm" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1" Namespace="DevExpress.Web.ASPxGridView"
    TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type = "text/javascript">
            function PrintPanel() {
                var panel = document.getElementById("<%=Panelmain.ClientID%>");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Expand from 0px to the appropriate size while fading in --%>

    <div class="row">
			<div class="col-md-12">
               
                <asp:Button ID="btnSabege" runat="server" class="btn btn-info btn-sm waves-effect waves-light" Text="سوابق سیستم" Visible="False" />
                  <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="تاریخچه تغییرات" Visible="False" />
                <asp:Button ID="btnPrint" runat="server" class="btn btn-violet btn-sm waves-effect waves-light" Text="چاپ" OnClientClick = "return PrintPanel();" />
                 <asp:Button ID="btnWork" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                
             			    			</div>
              
      </div>

   
       


  <asp:Panel ID="Panelmain" runat="server">
                                 


      
      
  <div class="row">
      <div class="col-md-6"
          >

     

      </div>
        <div class="col-md-6"
          >



      </div>
      </div>
      
      <br />

     <div class="row">
			<div class="col-md-12">
                <dx:ASPxGridView ID="grdFarei" runat="server" AutoGenerateColumns="False" KeyFieldName="IddtlKala" Width="100%" 
                    ClientIDMode="AutoID"
                    >

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <SettingsBehavior AllowFocusedRow="True" />

                    <clientsideevents focusedrowchanged="function(s, e) {

}" />
                    <settingsbehavior allowfocusedrow="True" />
                    <settings showfilterrow="True" />
                    <settingsdetail showdetailbuttons="False" />

                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />
                    <Columns>
                 
                        <dx:GridViewCommandColumn VisibleIndex="2" Width="5%" Visible="False">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد تراکنش" FieldName="IddtlKala" VisibleIndex="2" Width="5%" Visible="False">
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="kodeGate" ShowInCustomizationForm="True" VisibleIndex="3" Caption="کد کالا" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="kodeGate" FieldName="kodeGate" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="انبار" FieldName="nameAnabr" VisibleIndex="8" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="کالا" FieldName="BaseSystemHDtlNmae" VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد " FieldName="dtltedad" VisibleIndex="6" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاريخ" FieldName="WorkDateE" ShowInCustomizationForm="True" VisibleIndex="1" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تعداد استوک" FieldName="dtltedadStok" VisibleIndex="9" Width="5%" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="نوع تراکنش" FieldName="type" VisibleIndex="17" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="وضعیت " FieldName="value" VisibleIndex="18" Width="5%" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="WorkNote" VisibleIndex="5" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره حواله" FieldName="havaleID" VisibleIndex="0" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="شماره حواله قدیم" FieldName="havaleIDOld" VisibleIndex="10" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True" showgrouppanel="True" showheaderfilterbutton="True"></Settings>

                    <settingsdetail showdetailrow="True" />

                    <styles>
                        <detailrow backcolor="#CCFFCC">
                        </detailrow>
                    </styles>
                </dx:ASPxGridView>
                
             			    			</div>
              
      </div>
      <div class="row">
			       <div class="col-md-3">
				<%--<asp:Label ID="Label4" runat="server" CssClass="control-label" Text="باقیمانده"></asp:Label>
                <asp:TextBox ID="bagitxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>--%>
			</div>
    <div class="col-md-3">
			<%--	<asp:Label ID="Label2" runat="server" CssClass="control-label" Text="تعداد صادره"></asp:Label>
                <asp:TextBox ID="sadereTxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>--%>
			</div>
          <div class="col-md-3">
               <%-- <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="تعداد وارده"></asp:Label>

				<asp:TextBox ID="varedeTxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>--%>

			    			</div>
          
                 
            <div class="col-md-3">
                </div>
			</div>
   
          <br />
 
     </asp:Panel>
 

         

    

</asp:Content>

