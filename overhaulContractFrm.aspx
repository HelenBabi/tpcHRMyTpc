<%@ Page Language="VB" AutoEventWireup="false" CodeFile="overhaulContractFrm.aspx.vb" Inherits="overhaulContractFrm" MasterPageFile="~/dabir.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
      function toggleAll(source) {
          var checkboxes = document.querySelectorAll('#gridPersonnel input.rowCheck[type="checkbox"]');
          checkboxes.forEach(function (cb) {
              cb.checked = source.checked;
          });
      }
</script>
    <style>
    .pagination-container {
        text-align: center;
        padding: 10px 0;
    }

    .pagination-container table {
        margin: 0 auto;
    }

    .pagination-container td {
        padding: 6px 12px;
        font-size: 16px;
    }

    .pagination-container a, 
    .pagination-container span {
        display: inline-block;
        padding: 6px 12px;
        margin: 0 2px;
        color: #007bff;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        text-decoration: none;
    }

    .pagination-container span {
        background-color: #007bff;
        color: #fff;
    }

    .pagination-container a:hover {
        background-color: #e9ecef;
        color: #0056b3;
    }
</style>

      <div class="row">
			<div class="col-md-6"> 
    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" 
    Placeholder="جستجو بر اساس نام یا کد پرسنلی" Width="100%" />
<asp:Button ID="btnSearch" runat="server" Text="جستجو" 
    CssClass="btn btn-info mt-2 mb-2" OnClick="btnSearch_Click" />
                   </div>
          <div class="col-md-6"> 
              <asp:Label ID="lblTotalCount" runat="server" CssClass="text-info font-weight-bold"></asp:Label>

              </div>
            </div>
        <div class="row">
			<div class="col-md-6">   

<asp:GridView ID="gridPersonnel" runat="server"
    CssClass="table table-striped table-bordered table-hover"
    PagerStyle-CssClass="pagination-container justify-content-center"
    AllowPaging="True"
    PagerSettings-Mode="Numeric"
  OnPageIndexChanging="gridPersonnel_PageIndexChanging"
    DataKeyNames="Address,codePersoneli,BankCardNo,shift,BirthDay,chart,city,FatherName,FirstName,IBAN,LastName,madrak,MilitaryName,Nationality,Price,province,reshte,shSh,SSN,tarixStart"
    AutoGenerateColumns="False"
    ClientIDMode="Static" >

    <Columns>
        <asp:BoundField DataField="fulname" HeaderText="نام و نام خانوادگی" />
        <asp:BoundField DataField="Nationality" HeaderText="کدملی" />
        <asp:BoundField DataField="codePersoneli" HeaderText="کد پرسنلی" />
        <asp:BoundField DataField="shift" HeaderText="شیفت" />
              <asp:BoundField DataField="omur" HeaderText="واحد" />
        <asp:BoundField DataField="tarixStart" HeaderText="تاریخ شروع بکار" />
   <asp:TemplateField HeaderText="عکس">
    <ItemTemplate>
        <asp:Image ID="imgPhoto" runat="server" Width="60px" Height="80px" 
                   Style="border:1px solid #ccc; border-radius:5px;" />
    </ItemTemplate>
</asp:TemplateField>

        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="chkSelectAll" onclick="toggleAll(this)" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" CssClass="rowCheck" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

<PagerStyle CssClass="pagination-container justify-content-center"></PagerStyle>
</asp:GridView>




</div>
            <div class="col-md-6">  
                </div>
            </div>


    <asp:Button ID="btnPrint" runat="server" Text="چاپ کارت‌های انتخاب شده" OnClick="btnPrint_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnMarkPrinted" runat="server" Text="ارسال به برنامه ریزی تعمیرات" 
    OnClick="btnMarkPrinted_Click" CssClass="btn btn-success" Visible="False" 
     />  <!-- غیرفعال تا پس از چاپ فعال شود -->
</asp:Content>
