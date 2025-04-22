<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="productPersonel.aspx.vb" Inherits="productPersonel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
             <asp:Panel ID="Panel12" runat="server"  >
                  
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                             <div class="row">
              			<div class="col-md-12">
                 <asp:Label ID="Label2" runat="server" class="label-control" Text="نوع پرسنل"></asp:Label>
                      <asp:DropDownList ID="cmbRuzShabKar" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   <asp:ListItem Value="0">انتخاب</asp:ListItem>
                                   <asp:ListItem Value="1">عملیاتی</asp:ListItem>
                                   <asp:ListItem Value="2">ستادی</asp:ListItem>
                                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*اجباری*" ControlToValidate="cmbRuzShabKar" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                               </div>
             	
                      </div>
                         
                 <br />
                  <div class="row">
              
			<div class="col-md-4">
                 <asp:Label ID="lblKala" runat="server" class="label-control" Text=" انتخاب کالا"></asp:Label>
                      <asp:DropDownList ID="kalaCmb" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                               
                               </div>
                      <div class="col-md-4">
                 <asp:Label ID="lblSize" runat="server" class="label-control" Text=" انتخاب اندازه"></asp:Label>
                      <asp:DropDownList ID="sizeCmb" runat="server"  class="btn btn-secondary dropdown-toggle" >
                                   </asp:DropDownList>
                             
                               </div>
                         <div class="col-md-4">
                             <asp:Button ID="BtnAdd" runat="server" Text="ثبت" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a"  />
                             </div>
                      </div>
                         
                       <br />
                  <div class="row">
              
			

                      </div>
                        </asp:Panel>
             
               <br />
            <div class="row">
                                       	<div class="col-md-12">
                                                <asp:Label ID="lblSabege" runat="server" CssClass="control-label" Text="سوابق " Visible="false"></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="idDtl" Font-Size="10pt" style="color: #000000" TabIndex="12" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="idDtl" HeaderText="#">
                                                        <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="kalaValue" HeaderText="کالا" />
                                                        <asp:BoundField DataField="sizeValue" HeaderText="سایز">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="tarix" HeaderText="تاریخ درخواست" />
                                                        <asp:BoundField HeaderText="تاریخ تحویل" DataField="tarixThavil" Visible="False" />
                                                        <asp:BoundField DataField="statValue" HeaderText="وضعیت" Visible="False" />
                                                             <asp:ButtonField CommandName="del" Text="حذف" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle Font-Size="Medium" ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                </asp:GridView>
                                               </div>
                      </div>

     <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid">
                                     <div class="row">
                   

                         </div>
            </asp:Panel>
                 <br />
                  <div class="row">
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idDtl" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="kalaValue" HeaderText="کالا" />
                                            <asp:BoundField DataField="sizeValue" HeaderText="سایز" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ درخواست" />
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت" />
                                            <asp:ButtonField CommandName="del" Text="ابطال" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle Font-Size="Medium" ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                               </div>
                      </div>
            </asp:Panel>
    <br />
     
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
       
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>

