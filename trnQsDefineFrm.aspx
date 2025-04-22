<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnQsDefineFrm.aspx.vb" Inherits="trnQsDefineFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
           
                 
                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                          <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                             <div class="row">
                                        <div class="col-md-6">
            
             <asp:Label ID="Label11" runat="server"  class="control-label" Text="دوره"></asp:Label>
            
                                            <asp:DropDownList ID="CmbUnit" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px"></asp:DropDownList>
        
             </div>

<div class="col-md-3">
     <asp:Label ID="Label2" runat="server"  class="control-label" Text="نوع سوال"></asp:Label>
         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px">
        
     </asp:DropDownList>

    </div>

              			<div class="col-md-3">
                              <asp:Button ID="Button1" runat="server" Text="سوال بعدی" />
               
                               </div>
             
                      </div>
                
         
        <br />
        <div class="row">
              
			<div class="col-md-12">
                 <asp:Label ID="Label1" runat="server" class="control-label"  Text="سوال"></asp:Label>
                     
                <asp:TextBox ID="soalTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="soalTxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                               </div>
                  
                   
                      </div>

                  <div class="row">
              
			<div class="col-md-10">
                 <asp:Label ID="lblKala" runat="server" class="control-label"  Text="گزینه"></asp:Label>
                     
                <asp:TextBox ID="qozineTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="qozineTxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                <asp:ImageButton ID="BtnAdd" runat="server" ImageUrl="~/Icons/Add.png" Width="25px" Height="25px" ValidationGroup="a" ToolTip="برای افزودن آیتم کلیک نمایید" />
                               </div>
                  
                       <div class="col-md-2">
                <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="جواب؟"></asp:Label>
                           <asp:CheckBox ID="answerChk" runat="server" CssClass="checkbox-inline" />
            
                
             </div>
                            
                      </div>


                          <br />
           <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
                 
                       <br />
              



                                <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="lblSabege" runat="server" CssClass="control-label" Text="سوالات " Visible="false"></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="dtlID" Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%" ForeColor="Black" GridLines="Vertical">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                          <asp:ButtonField CommandName="sel" Text="انتخاب" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                                        <asp:BoundField DataField="dtlID" HeaderText="#">
                                                        <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="onvan" HeaderText="عنوان دوره" />
                                                          <asp:BoundField DataField="qsTypeValue" HeaderText="نوع سوال" />
                                                        <asp:BoundField DataField="qsValue" HeaderText="سوال" />
                                                    
                                                       
                                                        <asp:BoundField DataField="optionn" HeaderText="گزینه" />
                                                    
                                                       
                                                        <asp:BoundField DataField="answer" HeaderText="جواب؟ " >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                             <asp:ButtonField CommandName="del" Text="حذف" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle Font-Size="Medium" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                </asp:GridView>
                                               </div>
                      </div>
         <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
         </ContentTemplate>
                </asp:UpdatePanel>

                        </asp:Panel>
             
          
           
    <br />
     
          <br />

          
       
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                   
</asp:Content>

