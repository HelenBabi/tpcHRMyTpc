<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectPersonnel.aspx.vb" Inherits="SelectPersonnel" MasterPageFile="~/dabir.master" %>
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

        <div class="row">
			<div class="col-md-6">   

<asp:GridView ID="gridPersonnel" runat="server"
    CssClass="table table-striped table-bordered table-hover"
    PagerStyle-CssClass="pagination justify-content-center"
    AllowPaging="True"
    OnPageIndexChanging="gridPersonnel_PageIndexChanging"
    DataKeyNames="fulname,codePersoneli,shift,PhotoPath,expireDateCard,omur"
    AutoGenerateColumns="False"
    ClientIDMode="Static">

    <Columns>
        <asp:BoundField DataField="fulname" HeaderText="نام و نام خانوادگی" />
        <asp:BoundField DataField="Nationality" HeaderText="کدملی" />
        <asp:BoundField DataField="codePersoneli" HeaderText="کد پرسنلی" />
        <asp:BoundField DataField="shift" HeaderText="شیفت" />
        <asp:BoundField DataField="PhotoPath" HeaderText="PhotoPath" Visible="False" />
        <asp:BoundField DataField="omur" HeaderText="واحد" />
        <asp:BoundField DataField="expireDateCard" HeaderText="تاریخ انقضا" />

        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="chkSelectAll" onclick="toggleAll(this)" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" CssClass="rowCheck" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>




</div>
            <div class="col-md-6">  
                </div>
            </div>


    <asp:Button ID="btnPrint" runat="server" Text="چاپ کارت‌های انتخاب شده" OnClick="btnPrint_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnMarkPrinted" runat="server" Text="ارسال به برنامه ریزی تعمیرات" 
    OnClick="btnMarkPrinted_Click" CssClass="btn btn-success" 
     />  <!-- غیرفعال تا پس از چاپ فعال شود -->
</asp:Content>
