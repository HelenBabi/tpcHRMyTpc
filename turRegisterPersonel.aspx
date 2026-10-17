<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="turRegisterPersonel.aspx.vb" Inherits="turRegisterPersonel" %>
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
        <asp:Panel ID="Panel12" runat="server">
             
            <div class="alert alert-success alert-message" runat="server" id="divMessage" >
<asp:Literal ID="lblMsg" runat="server" Mode="PassThrough"></asp:Literal>
                       <div class="container mt-4">
        <a href="reports/tour guide 2.pdf" class="btn btn-info btn-sm" download>
            <i class="fa fa-file-pdf-o"></i> دانلود فایل اطلاعیه
        </a>
    </div>          
            </div>
       
        </asp:Panel>

        <asp:Panel ID="Panel1" runat="server" Visible="True">
            <!-- Information Panel -->
<%--            <div class="info-panel">
                <div class="row">
                    <div class="col-md-8">
                        <div class="row">
                            <div class="col-md-4">
                                <span class="info-label">تعداد سهم شما در دوره:</span>
                                <span class="info-value"><asp:Label ID="sahmieLbl" runat="server" Text="1"></asp:Label></span>
                            </div>
                            <div class="col-md-4">
                                <span class="info-label">تعداد سهم باقی مانده:</span>
                                <span class="info-value"><asp:Label ID="bagiLbl" runat="server" Text="1"></asp:Label></span>
                            </div>
                            <div class="col-md-4">
                                <span class="info-label">تعداد نفر مجاز در هر سهم:</span>
                                <span class="info-value">
                                    <asp:Label ID="LblTakafolCount" runat="server"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <asp:Label ID="estfadeLbl" runat="server" CssClass="text-info"></asp:Label>
                                <asp:Label ID="lblPayam" runat="server" Text="" CssClass="text-danger"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 text-left">
                        <asp:HiddenField ID="HdnMaxGaza" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                        <asp:HiddenField ID="HdnCountGazaSef" runat="server" />
                        <asp:Label ID="LblMaxGaza" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>--%>

            <!-- Reservation Form -->
            <div class="form-section">
                <h5 class="form-title">فرم رزرو جدید</h5>
                <div class="row">
                    <div class="col-md-8 col-sm-12 col-xl-6 form-group">
                                     <asp:Label ID="lblruneveshtType" runat="server" Text="انتخاب زمان و مکان(لیست را می توانید براساس تاریخ و نام مکان فیلتر نمایید)"></asp:Label>
      <dx:ASPxComboBox ID="CmbTur" ClientInstanceName="ClientComboBox" DropDownRows="5" CallbackPageSize="5" 
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server"
          NullText="انتخاب نمایید..."
           ClientIDMode="AutoID"
        IncrementalFilteringMode="Contains" ValueType="System.String" 
        CssClass="form-control" HorizontalAlign="Right" Font-Bold="True" Font-Names="Vazirmatn FD" Height="50px" >
        <ItemStyle>
          <SelectedStyle CssClass="selectedItem" />
          <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
        <dropdownbutton imageposition="Right"></dropdownbutton>
      </dx:ASPxComboBox>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
    ControlToValidate="CmbTur"
    ErrorMessage="لطفا محل و زمان را انتخاب نمایید"
    ForeColor="Red"
    InitialValue=""
    ValidationGroup="c" />

  
                    </div>
                                <div class="container mt-4">
        <a href="reports/tour list.pdf" class="btn btn-info btn-sm" download>
            <i class="fa fa-file-pdf-o"></i> دانلود فایل لیست
        </a>
    </div> 
                </div>
                
                <div class="row mt-3">
                    <div class="col-md-3 form-group">
                        <label for="txtTedad" class="control-label">تعداد نفرات</label>
                        <asp:TextBox ID="txtTedad" runat="server" CssClass="form-control" placeholder="تعداد" 
                            TextMode="Number" BackColor="#fffde7" Width="100" Font-Bold="True" Font-Names="Vazirmatn FD" Font-Size="12pt" Height="50px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="تعداد باید وارد شود" ControlToValidate="txtTedad" 
                            ForeColor="#CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-9 form-group">
                        <label for="tozih" class="control-label">توضیحات</label>
                        <asp:TextBox ID="tozih" runat="server" CssClass="form-control" 
                            placeholder="توضیحات مربوطه را در این قسمت بنویسید، مثلا سابقه و یا تابحال از این محل استفاده نموده اید یا خیر" 
                            TextMode="MultiLine" BackColor="#fffde7" Width="100%" Rows="3"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row mt-4">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnAdd" runat="server" Text="رزرو" 
                            CssClass="btn btn-success btn-sm" ValidationGroup="a" />
                        <asp:Button ID="btnBack" runat="server" Text="برگشت" 
                            CssClass="btn btn-warning btn-sm" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Reservations List -->
        <div class="form-section mt-4">
            <h5 class="form-title">لیست رزروهای من</h5>
            <div class="table-responsive">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CssClass="reservations-table" DataKeyNames="idd" BackColor="White" 
                    BorderColor="#f0f0f0" BorderWidth="1px" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="idd" HeaderText="#" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none">
                            <ItemStyle ForeColor="White" CssClass="d-none" />
                        </asp:BoundField>
                        <asp:BoundField DataField="magsad" HeaderText="محل و زمان" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="tedad" HeaderText="تعداد" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="statValue" HeaderText="وضعیت" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tarix" HeaderText="زمان ثبت نام" ItemStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="tozih" HeaderText="توضیحات" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelFile" runat="server" 
                                    OnClientClick="return confirm('آیا مطمئن هستید رزرو را ابطلال می کنید؟')" 
                                    OnClick="lnkDelFile_Click" 
                                    CssClass="btn btn-danger btn-sm" Text="ابطال"></asp:LinkButton>
                            </ItemTemplate>
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