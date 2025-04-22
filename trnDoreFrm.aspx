<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDoreFrm.aspx.vb" Inherits="trnDoreFrm" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <script type = "text/javascript">
          function PrintPanel() {
              var panel = document.getElementById("<%=Panel2.ClientID%>");
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
       <asp:Button ID="btnPage1" runat="server" cssclass="btn-primary"  Text="چاپ" OnClientClick = "return PrintPanel(); " />
        <asp:Panel ID="Panel2" runat="server" GroupingText="سوابق آموزشی" BorderColor="#9999FF">
      <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="مدرک تحصیلی"></asp:Label>
                    
                <asp:TextBox ID="madrakTxt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>
                      <div class="col-md-4">
                    <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="رشته تحصیلی"></asp:Label>
                    
                <asp:TextBox ID="reshteTxt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                              </div>
                      	<div class="col-md-4">
                                   <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="سمت سازمانی"></asp:Label>
                    
                <asp:TextBox ID="sematTxt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                              </div>
                      
                      </div>
       <br />
                <dx:ASPxGridView ID="trnGrid" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="idd" ClientIDMode="AutoID" Width="930px">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="عنوان" FieldName="onvan" VisibleIndex="0" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="dorehayeGozrande" 
                        Caption="دوره های گذرانده" VisibleIndex="1" Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="مجری دوره" FieldName="mojriDore" VisibleIndex="2" Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="محل اجرا" FieldName="mahalEjra"
                        VisibleIndex="3" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="روز" FieldName="RUZ"
                        VisibleIndex="4" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="saat" Caption="ساعت"
                        VisibleIndex="5" Width="5%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="هماهنگی اجرا" FieldName="hamahangiEjra" VisibleIndex="6" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="tarixShoru" VisibleIndex="8" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="tarixPayan" VisibleIndex="9" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="idd" FieldName="idd" Visible="False" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True">
                    </SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                </dx:ASPxGridView>
                
            </asp:Panel>
             
        <br />
            <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                    
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" ValidationGroup="b" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>

</asp:Content>

