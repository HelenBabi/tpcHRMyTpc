<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnSessionScoreFrm.aspx.vb" Inherits="trnSessionScoreFrm" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid">
        <br />
     
             <div class="row">

          <div class="col-md-4">
               
        <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="نام"></asp:Label>

				<asp:TextBox ID="nam" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>
         </div>
           <div class="col-md-4">
                <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="نام خانوادگی"></asp:Label>

				<asp:TextBox ID="family" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>
                </div>
          
          <div class="col-md-4">
                
            <asp:Label ID="lblComName" runat="server" CssClass="control-label" Text="کد پرسنلی"></asp:Label>

				<asp:TextBox ID="codePersonel" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>
		</div>
          </div>
 
        <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="نوع دوره"></asp:Label>

				<asp:TextBox ID="typevalue" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="lblUserNam" runat="server" CssClass="control-label" Text="عنوان دوره"></asp:Label>
                <asp:TextBox ID="onvan" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
			</div>

              <div class="col-md-4">
				<asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="مکان"></asp:Label>
                <asp:TextBox ID="makan" runat="server" Enabled="False"
                    ValidationGroup="a" CssClass="form-control"></asp:TextBox>
			</div>
            
		</div>

    <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="تاریخ شروع"></asp:Label>

				<asp:TextBox ID="tarixShuru" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" ></asp:TextBox>

			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label8" runat="server" CssClass="control-label" Text="تاریخ پایان"></asp:Label>
                <asp:TextBox ID="tarixEtmam" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
			</div>

              <div class="col-md-4">
                              <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="مدرس"></asp:Label>
                <asp:TextBox ID="modares" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control"></asp:TextBox>
							</div>
         
		</div>
  

   
          </asp:Panel>
  <br />

    <div class="row">
			<div class="col-md-4">

                <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="تعداد جلسات تشکیل شده"></asp:Label>
                				<asp:TextBox ID="tedadJalseTaskil" runat="server" 
                    ValidationGroup="a" CssClass="form-control" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="tedadJalseTaskil" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
                			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label6" runat="server" CssClass="control-label" Text="تعداد جلسات حضور"></asp:Label>
                <asp:TextBox ID="tedadJalseHozur" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="tedadJalseHozur" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
			</div>

              <div class="col-md-4">
                       <%--       <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="تعداد روز"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>--%>
							</div>
         
		</div>
        
     <div class="row">
			<div class="col-md-4">
                                <asp:Label ID="Label10" runat="server" CssClass="control-label" Text="نمره میان ترم"></asp:Label>
                				<asp:TextBox ID="nomreMianTerm" runat="server" 
                    ValidationGroup="a" CssClass="form-control" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vald_WorkHdrcode" runat="server" 
                    ControlToValidate="nomreMianTerm" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
			    			</div>
            <div class="col-md-4">
				<asp:Label ID="Label11" runat="server" CssClass="control-label" Text="نمره پایان ترم"></asp:Label>
                <asp:TextBox ID="nomrePayanTerm" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="nomrePayanTerm" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
			</div>

              <div class="col-md-4">
                              <asp:Label ID="Label12" runat="server" CssClass="control-label" Text="نمره شفاهی"></asp:Label>
                <asp:TextBox ID="nomreSafahi" runat="server" 
                    ValidationGroup="a"  CssClass="form-control"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="nomreSafahi" ErrorMessage="*" ForeColor="Red" 
                    ValidationGroup="a"></asp:RequiredFieldValidator>
							</div>
         
		</div>
         <br />

                  <div class="row">
              <div class="col-md-4">
              
               
                               </div>
                         <div class="col-md-4">
              
                <asp:Button ID="Button1" runat="server" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="a" Text="ثبت" />
                               </div>
                         <div class="col-md-4">
              
                <asp:Button ID="Button2" runat="server" class="btn btn-warning btn-sm waves-effect waves-light"  Text="برگشت" />
                               </div>
                       
                      </div>
     <div class="row">
      <div class="col-md-12">
            <asp:Label ID="lblhoshdar" runat="server"></asp:Label>
                                 <asp:HiddenField ID="typ" runat="server" />
                                 <asp:HiddenField ID="dat" runat="server" />
                                 <asp:HiddenField ID="hdnType" runat="server" />
                                 <asp:HiddenField ID="tim" runat="server" />
          </div>
         </div>

        
                                                 
   
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
  
</asp:Content>


