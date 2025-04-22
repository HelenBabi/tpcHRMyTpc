<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="certifiDeskPersonal.aspx.vb" Inherits="certifiDeskPersonal" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
    <div class="container">
          <div class="alert-success">
                              <p>
                                 لطفا در صورت ثبت موفق گواهی و مشاهده پیغام "ثبت شد" به منابع انسانی مراجعه فرمایید و هنگام مشاهده پیغام تکمیل شدن سقف مجاز از تماس و حضور خودداری نمایید.باتشکر</p>
                          </div>
                 <br />
       <div class="alert-danger">
                              <h4>
                                  <asp:Label ID="lblHoshdar" runat="server"></asp:Label></h4>
                          </div>
        </div>
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText="لیست گواهی های من ">
                          
                     
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
                 <br />
         
    
                  
          <%--  </asp:Panel>--%>
  
    <br />

       
                           <div class="row">
              
			<div class="col-md-6">
              <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="نام وام گیرنده"></asp:Label>
                             <asp:TextBox ID="girande" runat="server" CssClass="form-control" placeholder="خودم یا نام و نام خانوادگی وام گیرنده"  TextMode="SingleLine" BackColor="LightYellow"  ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="اطلاعات گیرنده وام ضروری میباشد" ValidationGroup="1" ControlToValidate="girande" ForeColor="Red"></asp:RequiredFieldValidator>
                               </div>
                      	
                     <div class="col-md-6">
              <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="کدملی وام گیرنده"></asp:Label>
                             <asp:TextBox ID="codeMeli" runat="server" CssClass="form-control" placeholder="کدملی وام گیرنده"  TextMode="Number" MaxLength="10" ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="کدملی گیرنده وام ضروری میباشد" ValidationGroup="1" ControlToValidate="codeMeli" ForeColor="Red"></asp:RequiredFieldValidator>
                               </div>

             	
                      </div>
      <div class="row">
                    
                        <div class="col-md-6">
                 <asp:Label ID="Label3" runat="server" class="control-label" Text="نوع گواهی"></asp:Label>
               <asp:DropDownList ID="CmbGavahiType" runat="server" AutoPostBack="True" 
                                            CssClass="form-control" TabIndex="1" 
                    Height="40px">
                                        </asp:DropDownList>
            </div>
                <div class="col-md-6">
                  <asp:Label ID="lblSazman" runat="server" class="control-label" Text="نام سازمان یا موسسه" Visible="False"></asp:Label>
                     <asp:TextBox ID="txtSazman" runat="server"  ValidationGroup="a"
                    class="form-control" Visible="False"></asp:TextBox>
                
            </div>
             
        </div>

             <div class="row">
                    
                        <div class="col-md-4">
                            <asp:Label ID="lblbank" runat="server" class="control-label" Text="بانک"></asp:Label>
                                 
                                            <dx:ASPxComboBox ID="cmbBank" runat="server" ClientIDMode="AutoID" ClientInstanceName="ClientComboBox" IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains" Width="100%" AutoPostBack="True">
                                            </dx:ASPxComboBox>
                                   
                            </div>
                 <div class="col-md-4">
                    
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
     <br />
             <div class="row">
			
                <div class="col-md-4">
                     
                </div>
                  <div class="col-md-4">
               
              
                            <asp:Button ID="btnSave" runat="server" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="1" Text="ثبت" />
                </div>
                <div class="col-md-4">
  <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                
                </div>
       <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />

</asp:Content>

