<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnQsEntryFrm.aspx.vb" Inherits="trnQsEntryFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
           <div class="container">

           
                  <div class="alert-success">
                             
                               <h3> <asp:Label ID="lblPayam" runat="server" Text="" Visible="False"></asp:Label>
                                 
</h3>
                          </div>
               <div class="alert-info">
                             
                               <h3> <asp:Label ID="lblPayam2" runat="server" Text="" Visible="False"></asp:Label>
                                 
</h3>
                          </div>
               </div>
                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid">
                          <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                             <div class="row">
                                        <div class="col-md-6">
            
             <asp:Label ID="Label11" runat="server"  class="control-label" Text="دوره"></asp:Label>
            
                                            <asp:DropDownList ID="CmbUnit" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px" Enabled="False"></asp:DropDownList>
        
             </div>

<div class="col-md-3">
     <asp:Label ID="Label2" runat="server"  class="control-label" Text="نوع سوال"></asp:Label>
         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px">
             <asp:ListItem Value="1">تایین سطح زبان انگلیسی</asp:ListItem>
             <asp:ListItem Value="2">آزمون زبان انگلیسی</asp:ListItem>
             <asp:ListItem Value="3">HSE</asp:ListItem>
     </asp:DropDownList>

    </div>

              			<div class="col-md-3">
                            
             
                               </div>
             
                      </div>
                
         
        <br />
        <div class="row">
              
			<div class="col-md-12">
                 <asp:Label ID="lblSoal" runat="server" class="control-label"  Text="سوال"></asp:Label>
                     
                <asp:TextBox ID="soalTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="soalTxt" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                               </div>
                  
                   
                      </div>
        	
                  <div class="row">
              
			<div class="col-md-8">
                 <asp:Label ID="lblKala" runat="server" class="control-label"  Text="جواب را انتخاب نمایید:"></asp:Label>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="RadioButtonList1" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>      
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" TextAlign="Left"></asp:RadioButtonList>


                                                 
                               
                               </div>
                  
                    
              
                          <div class="col-md-4">
                              
           
                               </div>
            
                
             </div>
                            
                       <div class="row">
              <div class="col-md-4">
                              
             
                               </div>
			<div class="col-md-4">
                <asp:Button ID="Button1" runat="server" Text="سوال بعدی" CssClass="btn-success" ValidationGroup="a"    />

                                                 
                               
                               </div>
                  
                    
              
                          <div class="col-md-4">
                              
             
                               </div>
            
                
             </div> 
                 
                       <br />
              



                                
         <asp:HiddenField ID="hdn_QsID" runat="server" Value="0" />
         <asp:HiddenField ID="hdn_counter" runat="server" Value="0" />
         </ContentTemplate>
                </asp:UpdatePanel>

                        </asp:Panel>
             
          
           
    <br />
     

      <div class="row">
		
                <div class="col-md-3">
                    <asp:Button ID="btnA" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="مشاهده فایل های جلسه اول سطح A" Visible="False" />
                </div>
                <div class="col-md-3">
                     <asp:Button ID="btnB" runat="server" class="btn btn-info btn-sm waves-effect waves-light" Text="مشاهده فایل های جلسه اول سطح B" Visible="False" />
                </div>
                  <div class="col-md-3">
  <asp:Button ID="btnC" runat="server" class="btn btn-danger btn-sm waves-effect waves-light" Text="مشاهده فایل های جلسه اول سطح C" Visible="False" />
                </div>
          	<div class="col-md-3">
                 
                </div>
                </div>

          <br />

             <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                <div class="col-md-3">
 
                </div>
                  <div class="col-md-3">

                </div>
                </div>
       

                                   
</asp:Content>

