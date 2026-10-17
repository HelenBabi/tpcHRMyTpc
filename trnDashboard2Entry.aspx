<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnDashboard2Entry.aspx.vb" Inherits="trnDashboard2Entry" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .info-panel {
            background-color: #fff8e1;
            border-radius: 8px;
            padding: 15px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            margin-bottom: 20px;
        }
        .info-label {
            font-weight: 600;
            color: #555;
        }
        .info-value {
            font-weight: 700;
            color: #1976d2;
            text-align: left;
            min-width: 50px;
            display: inline-block;
        }
        .form-section {
            background-color: white;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            margin-bottom: 20px;
        }
        .form-title {
            color: #1976d2;
            border-bottom: 1px solid #eee;
            padding-bottom: 10px;
            margin-bottom: 20px;
            font-size: 1.2rem;
        }
        .reservations-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
        }
        .reservations-table th {
            background-color: #1976d2;
            color: white;
            padding: 12px 8px;
            text-align: center;
        }
        .reservations-table td {
            padding: 10px 8px;
            border-bottom: 1px solid #eee;
            text-align: center;
        }
        .reservations-table tr:hover {
            background-color: #f5f5f5;
        }
        .alert-message {
            padding: 12px 15px;
            border-radius: 4px;
            margin-bottom: 15px;
        }
        .btn-custom {
            min-width: 100px;
            padding: 8px 15px;
            border-radius: 4px;
            font-weight: 500;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <!-- Message Alert -->
      <%--  <asp:Panel ID="Panel12" runat="server">
            <div class="alert alert-success alert-message" runat="server" id="divMessage" >
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </asp:Panel>--%>

        <asp:Panel ID="Panel1" runat="server" Visible="True">
            <!-- Information Panel -->
   

            <!-- Reservation Form -->
            <div class="form-section">
                <h5 class="form-title">فرم ثبت نمودار</h5>
                         <div class="row">
       

			<div class="col-md-3">
                                   <div class="col-md-2 form-group">
                        <label for="txtTedad" class="control-label">سال</label>
                        <asp:TextBox ID="sal" runat="server" CssClass="form-control" placeholder="عدد " 
                            TextMode="Number" BackColor="#fffde7" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="سال باید وارد شود" ControlToValidate="sal" 
                            ForeColor="#CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                 
                </div>
                  <div class="col-md-3">
                    <div class="col-md-2 form-group">
                        <label for="txtTedad" class="control-label">ماه</label>
                        <asp:DropDownList ID="mah" runat="server" Height="40px">
                            <asp:ListItem Value="3">3ماهه</asp:ListItem>
                            <asp:ListItem Value="6">6ماهه</asp:ListItem>
                            <asp:ListItem Value="9">9ماهه</asp:ListItem>
                            <asp:ListItem Value="12">یکسال</asp:ListItem>
                                                   </asp:DropDownList>
                    </div>
                                 </div>
                     <div class="col-md-3">
                    <div class="col-md-2 form-group">
                        <label for="txtTedad" class="control-label">ماهیت</label>
                        <asp:DropDownList ID="mahiat" runat="server" Height="40px">
                                                   </asp:DropDownList>
                    </div>
                                 </div>
                   <div class="col-md-3">
                    <div class="col-md-2 form-group">
                        <label for="txtTedad" class="control-label">ساعت</label>
                        <asp:TextBox ID="saat" runat="server" CssClass="form-control" placeholder="عدد " 
                            TextMode="Number" BackColor="#fffde7" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="ساعت باید وارد شود" ControlToValidate="saat" 
                            ForeColor="#CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                 
                </div>
                  
                 
                </div>
                <div class="row mt-4">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnAdd" runat="server" Text="ثبت" 
                            CssClass="btn btn-success btn-sm" ValidationGroup="a" />
                          <asp:Button ID="BtnPrint" runat="server" Text="چاپ نمودار" 
                            CssClass="btn btn-primary btn-sm" />
                        <asp:Button ID="btnBack" runat="server" Text="برگشت" 
                            CssClass="btn btn-danger btn-sm" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Reservations List -->
        <div class="form-section mt-4">
            <h5 class="form-title">لیست </h5>
            <div class="table-responsive">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CssClass="reservations-table" DataKeyNames="idd" BackColor="White" 
                    BorderColor="#F0F0F0" BorderWidth="1px" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="idd" HeaderText="#" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none">
<HeaderStyle CssClass="d-none"></HeaderStyle>

                            <ItemStyle ForeColor="White" CssClass="d-none" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sal" HeaderText="سال" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Title" HeaderText="ماه" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="mahiat" HeaderText="ماهیت" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="saat" HeaderText="ساعت" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelFile" runat="server" 
                                    OnClientClick="return confirm('آیا مطمئن هستید ؟')" 
                                    OnClick="lnkDelFile_Click" 
                                    CssClass="btn btn-sm btn-outline-danger" Text="حذف"></asp:LinkButton>
                            </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="thead-dark" />
                    <RowStyle CssClass="text-center" />
                    <AlternatingRowStyle BackColor="#f9f9f9" />
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>