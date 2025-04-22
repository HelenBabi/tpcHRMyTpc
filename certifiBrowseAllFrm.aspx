<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="certifiBrowseAllFrm.aspx.vb" Inherits="certifiBrowseAllFrm" %>


<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <div class="row">
			<div class="col-md-12">         
                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="مشاهده گواهی های درخواستی پرسنل"></asp:Label>
                    
            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False"
                KeyFieldName="codePersoneli" ClientIDMode="AutoID" Width="100%">
                <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                <Columns>
                           <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="10%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("codePersoneli")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="جزئیات"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="0%">
                        <clearfilterbutton visible="True">
                        </clearfilterbutton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="0" ShowInCustomizationForm="True" Width="8%">
                    </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="family" ShowInCustomizationForm="True" Caption="نام خانوادگی" VisibleIndex="1" Width="10%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="codePersoneli" ShowInCustomizationForm="True" Caption="کد پرسنلی" VisibleIndex="2" Width="5%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="value" Caption="نوع استخدام" VisibleIndex="2" Width="7%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="latter_date" ShowInCustomizationForm="True" Caption="تاریخ" VisibleIndex="3" Width="5%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="gavahi_typ" ShowInCustomizationForm="True" 
                        Caption="نوع گواهی" VisibleIndex="4" Width="5%">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
    <cellstyle horizontalalign="Justify">
    </cellstyle>
</dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="برای" FieldName="motafarege_nam" ShowInCustomizationForm="True" VisibleIndex="8" Width="10%">
    <editcellstyle horizontalalign="Justify">
    </editcellstyle>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statt" VisibleIndex="9" ShowInCustomizationForm="True" Width="5%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="stat" FieldName="stat" VisibleIndex="11" Width="0%" Visible="False">
                    </dx:GridViewDataTextColumn>
                                             <dx:GridViewDataTextColumn Caption="شماره نامه" FieldName="new_later_num" 
                            ShowInCustomizationForm="True" VisibleIndex="12" Width="5%">
                           
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="8" Width="5%" FieldName="new_later_num" Caption="ابطال؟">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnEbtal" runat="server" 
                    CommandArgument='<%# Eval("new_later_num")%>' CommandName='<%# "Ebtall" %>' 
                    style="color: #333333; " Text="ابطال؟" 
                    ></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager PageSize="20">
                </SettingsPager>
                <settings showfilterrow="True" showgrouppanel="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
            </dx:ASPxGridView>
              
             </div> 
                  </div>          

   <br />
 <div class="row">
           
            <div class="col-md-4">
                
            </div>
             <div class="col-md-4">
                
            </div>
             <div class="col-md-4">
                 <asp:Button ID="Button2" runat="server" Text="برگشت" class="btn btn-warning btn-sm waves-effect waves-light"   /> 
                 
            </div>
        </div>
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


