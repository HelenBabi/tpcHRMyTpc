<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="certifiDetail.aspx.vb" Inherits="certifiDetail" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
       <script language="javascript" type="text/javascript">
           function intFormat(number) {
               var regex = /(\d)((\d{3},?)+)$/;
               number = number.split(',').join('');
               while (regex.test(number)) {
                   number = number.replace(regex, '$1,$2');
               }
               return number;
           }
           function numFormat(number) {
               var pointReg = /([\d,\.]*)\.(\d*)$/, f;
               if (pointReg.test(number)) {
                   f = RegExp.$2;
                   return intFormat(RegExp.$1) + '.' + f;
               }
               return intFormat(number);
           }
    </script>

    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid">
        
        <asp:Panel ID="Panel2" runat="server" Enabled="False" GroupingText="اطلاعات پرسنل">


<div class="fc-view-container">
        <div class="alert-info">
            <div class="row">
                    
                        <div class="col-md-4">
                 <asp:Label ID="Label12" runat="server" class="control-label" Text="نام و نام خانوادگی"></asp:Label>
                <asp:TextBox ID="pishnahaddahande" class="form-control" runat="server"></asp:TextBox>
            </div>
                <div class="col-md-4">
                 <asp:Label ID="Label11" runat="server" class="control-label" Text="کد پرسنلی"></asp:Label>
                    <asp:TextBox ID="person_cod" class="form-control" runat="server"></asp:TextBox>
                
            </div>
                  <div class="col-md-4">
                 <asp:Label ID="Label13" runat="server" class="control-label" Text="نوع قرارداد"></asp:Label>
                   <asp:TextBox ID="contracttyp" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        </div>
        
    
    <br />
    <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="سوابق پرسنل ">
                          
                     
                        <div class="row">
                     
                                       	<div class="col-md-12">
            <dx:ASPxGridView ID="gridSabege" runat="server" AutoGenerateColumns="False" EnableCallBacks="False"
                KeyFieldName="id" ClientIDMode="AutoID" Width="100%">
                <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                <Columns>
                    <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="latter_date" VisibleIndex="0"
                        Width="10%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="gavahi_typ" ShowInCustomizationForm="True" Width="10%" 
                        Caption="نوع گواهی" VisibleIndex="1">
    <editcellstyle horizontalalign="Justify">
    </editcellstyle>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Justify"></CellStyle>
</dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="bank_nam" ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="شعبه" FieldName="shobe_nam"
                        ShowInCustomizationForm="True" VisibleIndex="3" Width="15%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="سازمان/موسسه" FieldName="moassese"
                        VisibleIndex="4" Width="5%">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="برای" FieldName="motafarege_nam" ShowInCustomizationForm="True" VisibleIndex="5" Width="20%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statt" VisibleIndex="6" ShowInCustomizationForm="True" Width="5%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="stat" FieldName="stat" Visible="False" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="کاربر" FieldName="karbarr" VisibleIndex="8" Width="10%" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager PageSize="20">
                </SettingsPager>
            </dx:ASPxGridView>
                                               </div>
                      </div>
                                              
                            
                                              
                            <br />
                        </asp:Panel>
    </div>


           

            </asp:Panel>
    

   <asp:HiddenField ID="HiddenFieldlater_num" runat="server" />

          </asp:Panel>
     <div class="row">
                    
                        <div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" class="control-label" Text="نوع گواهی"></asp:Label>
               <asp:DropDownList ID="CmbGavahiType" runat="server" AutoPostBack="True" 
                                            CssClass="form-control" TabIndex="1" 
                    Height="40px">
                                        </asp:DropDownList>
            </div>
                <div class="col-md-4">
                 <asp:Label ID="Label2" runat="server" class="control-label" Text="میزان دریافتی"></asp:Label>
                    <asp:TextBox ID="txtMozd" class="form-control" onchange="this.value=numFormat(this.value)" onkeyup="this.blur();this.focus()" runat="server"></asp:TextBox>
                
            </div>
                  <div class="col-md-4">
                 <asp:Label ID="Label3" runat="server" class="control-label" Text="نام گیرنده وام"></asp:Label>
                   <asp:TextBox ID="txtGirande" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

             <div class="row">
                    
                        <div class="col-md-4">
                            <asp:Label ID="lblbank" runat="server" class="control-label" Text="بانک"></asp:Label>
                                 
                                            <dx:ASPxComboBox ID="cmbBank" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains" Width="100%" AutoPostBack="True">
                                            </dx:ASPxComboBox>
                                   
                            </div>
                 <div class="col-md-4">
                       <asp:Label ID="lblSazman" runat="server" class="control-label" Text="نام سازمان یا موسسه" Visible="False"></asp:Label>
                     <asp:TextBox ID="txtSazman" runat="server" Width="300px" ValidationGroup="a"
                    class="form-control" Visible="False"></asp:TextBox>
                            </div>
                 <div class="col-md-4">

                            </div>
                 </div>
            <div class="row">
                    
                        <div class="col-md-4">
                            <asp:Label ID="lblShobe" runat="server" class="control-label" Text="شعبه"></asp:Label>
                           
                                            <dx:ASPxComboBox ID="cmbShobe" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains" Width="100%">
                                            </dx:ASPxComboBox>
                                   
                            </div>
                 <div class="col-md-4">

                            </div>
                 <div class="col-md-4">

                            </div>
                 </div>
    <hr />
     <div class="row">
           
            <div class="col-md-4">
                
            </div>
             <div class="col-md-4">
                  <asp:Button ID="btnSave" runat="server" Text="ذخیره" class="btn btn-success btn-sm waves-effect waves-light"   />
                 <asp:Button ID="btnChap" runat="server" Text="چاپ" class="btn btn-info btn-sm waves-effect waves-light"   />
            </div>
             <div class="col-md-4">
                 <asp:Button ID="btnBack" runat="server" Text="برگشت" class="btn btn-warning btn-sm waves-effect waves-light"   /> 
                 
            </div>
        </div>
    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0"/>
                                              
    <asp:HiddenField ID="hdnType" runat="server" Value="0" />
</asp:Content>



