<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="registerTpcFrm.aspx.vb" Inherits="registerTpcFrm" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
  
    <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="شماره پرونده"></asp:Label>

				<asp:TextBox ID="idReg" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" defaultbutton="ImageButton1"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/design_images/kfilereplace.png" />

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label5" runat="server" CssClass="control-label" Text="نام"></asp:Label>
                <asp:TextBox ID="nam" runat="server"
                    ValidationGroup="a"  CssClass="form-control" Enabled="false" ></asp:TextBox>
			</div>

              <div class="col-md-4">
				<asp:Label ID="Label6" runat="server" CssClass="control-label" Text=" نام خانوادگی"></asp:Label>
                <asp:TextBox ID="lname" runat="server"   
                    ValidationGroup="a" CssClass="form-control" Enabled="false"></asp:TextBox>
			</div>
            		</div>
      <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="نام پدر"></asp:Label>

				<asp:TextBox ID="fatherName" runat="server" 
                    ValidationGroup="a"  CssClass="form-control" Enabled="false" ></asp:TextBox>

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label2" runat="server" CssClass="control-label" Text="کدملی"></asp:Label>
                <asp:TextBox ID="codeMelli" runat="server"
                    ValidationGroup="a"  CssClass="form-control" Enabled="false"></asp:TextBox>
			</div>

              <div class="col-md-4">
				<asp:Label ID="Label3" runat="server" CssClass="control-label" Text="شماره شناسنامه"></asp:Label>
                <asp:TextBox ID="shsh" runat="server"   
                    ValidationGroup="a" CssClass="form-control" Enabled="false"></asp:TextBox>
			</div>
            		</div>
    <br />
      <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="مصاحبه انجام شد؟ " Enabled="False"></asp:Label>

			   <asp:CheckBox ID="chkMosahebe" runat="server" />

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="lblellat" runat="server" CssClass="control-label" Text="نتیجه مصاحبه" ></asp:Label>
                                <asp:DropDownList ID="cmbNatije" runat="server" 
                                       class="btn btn-secondary dropdown-toggle"  
                                         >
                                    </asp:DropDownList>
			</div>

              <div class="col-md-4">
				<asp:Label ID="lblellattarix" runat="server" CssClass="control-label" Text="تاریخ مصاحبه" ></asp:Label>
                                  <asp:TextBox ID="tarixMosahebe" runat="server" Width="190px" 
                    ValidationGroup="a" CssClass="form-control" ></asp:TextBox>
			</div>
            		</div>
      <div class="row">
			<div class="col-md-12">
                <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="توضیحات"></asp:Label>
                 <asp:TextBox ID="tozihat" runat="server"   
                    ValidationGroup="a" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
                </div>
        </div>
        <br />
            <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                     <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره" ValidationGroup="a" />
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" ValidationGroup="b" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
</asp:Content>

