<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overhaulSecurityAdminBrowsFrm.aspx.vb" Inherits="overhaulSecurityAdminBrowsFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <script type="text/javascript">
    function selectAllRows() {
        if (typeof grid !== "undefined") {
            grid.SelectAllRowsOnPage(); // فقط همین صفحه
        }
    }

    function getSelectedRows() {
        var selectedKeys = grid.GetSelectedKeysOnPage(); // فقط ردیف‌های صفحه جاری

        if (selectedKeys.length === 0) {
            alert("لطفاً حداقل یک ردیف را انتخاب کنید.");
            return;
        }

        // ارسال آی‌دی‌ها به صفحه چاپ
        var query = "?ids=" + selectedKeys.join(",");
        window.open("PrintCards.aspx" + query, "_blank");
    }
</script>--%>

<style>
    @page { size: A4; margin: 1cm; }
    .page {
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-template-rows: repeat(2, auto);
        gap: 0.4cm;
        page-break-after: always;
    }
    .card {
        width: 4.8cm;
        height: 7.6cm;
        border: 1px solid black;
        padding: 0.2cm;
        box-sizing: border-box;
        font-family: Tahoma;
    }
    .photo {
        width: 2.5cm;
        height: 3.2cm;
        object-fit: cover;
        margin: 0.2cm auto;
        border: 1px solid #000;
    }
    .info { font-size: 10px; text-align: right; }
    .signature img { width: 2.5cm; }
    .back { font-size: 10px; direction: rtl; line-height: 1.5; }
</style>

     
 <div class="row">
           
            <div class="col-md-4">
                <asp:Button ID="Button2" runat="server" Text="بازگشت" CssClass="btn btn-warning btn-sm float-end" />
                <asp:Button ID="btnPrintSelected" runat="server" CssClass="btn btn-success" Text="چاپ کارت‌ها"  />
                 <%--<asp:Button ID="Button1" runat="server" Text="چاپ کارت‌ها" OnClientClick="getSelectedRows(); return false;" CssClass="btn btn-primary" />--%>
            </div>
             <div class="col-md-4">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="GridWorkTable">
                </dx:ASPxGridViewExporter>
                 <asp:Button ID="btnSelectAll" runat="server" Text="انتخاب همه در صفحه" CssClass="btn btn-success btn-sm"
    OnClientClick="selectAllRows(); return false;" Visible="False" />


            </div>
             <div class="col-md-4">
                 <asp:Button ID="btnUpdateStatus" runat="server" CssClass="btn btn-danger" Text="ارسال پرونده های انتخاب شده به برنامه ریزی تعمیرات" OnClick="btnUpdateStatus_Click" Visible="False" />
    
                 <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
            </div>
        </div>

   <div class="container mt-3" style="width:100%">
        <div class="row">
            <div class="col-md-12">
                <!-- عنوان یا هر محتویات دیگر داخل صفحه -->
                <h5 class="text-center">لیست چاپ</h5>
            </div>
        </div>

        <!-- دیو برای جدول -->
        <div class="table-responsive shadow-sm p-3 mb-5 bg-white rounded" style="width:100%">
     <dx:ASPxGridView ID="GridWorkTable" runat="server"
    AutoGenerateColumns="False"
    EnableCallBacks="False"
    ClientIDMode="AutoID"
    Width="100%"
    KeyFieldName="Id">
         
    <SettingsPager />
    <SettingsBehavior ProcessSelectionChangedOnServer="False" />
<Settings ShowFilterRow="True" />
    <Columns>
        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" />
      <dx:GridViewDataTextColumn FieldName="Id" Caption="کد پیگیری" VisibleIndex="1" />
        <dx:GridViewDataTextColumn FieldName="Nationality" Caption="کد ملی" VisibleIndex="2" />
        <dx:GridViewDataTextColumn FieldName="FirstName" Caption="نام" VisibleIndex="3" />
        <dx:GridViewDataTextColumn FieldName="LastName" Caption="نام خانوادگی" VisibleIndex="4" />
        <dx:GridViewDataTextColumn FieldName="FatherName" Caption="نام پدر" VisibleIndex="5" />
        <dx:GridViewDataTextColumn FieldName="shSh" Caption="شناسنامه" VisibleIndex="6" />
        <dx:GridViewDataTextColumn FieldName="BirthDay" Caption="تولد" VisibleIndex="7" />
        <dx:GridViewDataTextColumn FieldName="Mobile" Caption="موبایل" VisibleIndex="8" />
        <dx:GridViewDataTextColumn FieldName="Tell" Caption="تلفن" VisibleIndex="9" Visible="False" />
    </Columns>

    <ClientSideEvents Init="function(s,e){
        window.grid = s;
    }" />
</dx:ASPxGridView>

        </div>
    </div>
                   
      
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
</asp:Content>


