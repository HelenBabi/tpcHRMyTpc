<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="jobGroupFrm.aspx.vb" Inherits="jobGroupFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <script src="js-persian-cal.min.js" type="text/javascript"></script>
    <link href="js-persian-cal.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function openNewWin(inp1, inp2) {
            var div_ = document.getElementById("pcal1");
            div_.setAttribute('value', inp1);
            var div_ = document.getElementById("pcal2");
            div_.setAttribute('value', inp2);
            //            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');

            //            div_.focus();

        }

</script>
    <div class="row">
			<div class="col-md-12">
                 <asp:ImageButton ID="ibtnAdd" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Add.png" ToolTip="جدید" Width="48px" 
                        ValidationGroup="b" />
                       <asp:ImageButton ID="ibtnEdit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/edit.png" ToolTip="ویرایش" Width="48px" 
                        ValidationGroup="a" />
                     <asp:ImageButton ID="ibtnSave" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/save2.png" ToolTip="ذخیره" Width="48px" 
                        ValidationGroup="a" />
                    <asp:ImageButton ID="ibtnSaveNew" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/savenew2.png" ToolTip="ذخیره و جدید" Width="48px" 
                        ValidationGroup="a" />
           
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" />
                 
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" />
                 
               	<asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" 
                        ValidationGroup="b" />
                
             			    			</div>
              
      </div>
    <div class="row">
			  <div class="col-md-2">
				<asp:Label ID="lblUserNam" runat="server" CssClass="control-label" Text="کد"></asp:Label>
                <asp:TextBox ID="kmIDTxt" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" Width="100px"></asp:TextBox>
			</div>
         <div class="col-md-6">
	<asp:Label ID="Label1" runat="server" CssClass="control-label" Text="موضوع کار"></asp:Label>
                <asp:TextBox ID="subject" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" Width="100%"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="subject" ErrorMessage="لطفا موضوع کار را بنویسید" ForeColor="#FF33CC" ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
          <div class="col-md-2">
              <asp:Label ID="Label6" runat="server" CssClass="control-label" Text="اهمیت"></asp:Label>
             <asp:DropDownList ID="olvaitCmb" runat="server" Enabled="false" CssClass="form-control" Width="200px">
                 <asp:ListItem Value="0">معمولی</asp:ListItem>
                 <asp:ListItem Value="1">مهم</asp:ListItem>
                 <asp:ListItem Value="2">خیلی مهم</asp:ListItem>
              </asp:DropDownList>
             </div>
          <div class="col-md-2">
				<asp:Label ID="Label10" runat="server" CssClass="control-label" Text="درصد پیشرفت"></asp:Label>
               
                   <asp:TextBox ID="darsad" runat="server" 
                    ValidationGroup="a" Enabled="False" CssClass="form-control" Width="100px">0</asp:TextBox>
			</div>
        	</div>
   
      <div class="row">
			<div class="col-md-12">    
                         <asp:Label ID="Label8" runat="server" CssClass="control-label" Text=" توضیحات"></asp:Label>
                <asp:TextBox ID="noteTxt" runat="server" placeholder="توضیحات کار" Height="88px" 
                                            TextMode="MultiLine" 
                    ValidationGroup="a" CssClass="form-control" Enabled="False"></asp:TextBox>            
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="noteTxt" ErrorMessage="لطفا توضیحات کار را بنویسید" ForeColor="#FF33CC" ValidationGroup="a"></asp:RequiredFieldValidator>
       </div>
           </div>


       <div class="row">
			<div class="col-md-4">        
                	<asp:Label ID="Label3" runat="server" CssClass="control-label" Text="گروه مربوطه"></asp:Label>
                <dx:ASPxComboBox ID="cmbRunevesht" ClientInstanceName="ClientComboBox"
         Width="100%" runat="server" ClientIDMode="AutoID"  CssClass="btn btn-default dropdown-toggle"  IncrementalFilteringDelay="10" IncrementalFilteringMode="Contains" Enabled="False" AutoPostBack="True">
    </dx:ASPxComboBox>
                <asp:Button ID="btnRunevesht" runat="server" class="btn btn-primary btn-sm waves-effect waves-light" Text="افزودن" Enabled="False" />
                </div>
           <div class="col-md-4">    
                             <asp:Label ID="Label11" runat="server" CssClass="control-label" Text=" توضیحات"></asp:Label>
                <asp:TextBox ID="matnErja" runat="server" placeholder="متن ارجاع" Height="88px" 
                                            TextMode="MultiLine" 
                    ValidationGroup="a" CssClass="form-control" Enabled="False"></asp:TextBox>                   
                   </div>
          <div class="col-md-4"> 
              <asp:DataList ID="DataList1" runat="server"  BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        GridLines="Horizontal" Width="100%" ForeColor="Black" 
                                     meta:resourcekey="DataList1Resource1">
                      <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                      <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                      <ItemTemplate>
                          <table class="style17" style="width: 100%">
                              <tr>
                                  <td style="text-align: right; background-color: #007ACC;" class="auto-style1">
                                      &nbsp;&nbsp;<asp:Image ID="Image_pp" runat="server" Height="30px" 
                                          ImageUrl='<%# eval("axurl") %>' meta:resourcekey="Image_ppResource1" 
                                          Width="30px" />
                                      <asp:Label ID="lbl_user" runat="server"  
                                          meta:resourcekey="lbl_userResource1" Text='<%# eval("headd") %>' 
                                          style="color: #FFFFFF"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="text-align: right; " class="style2">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  
                                  <td style="text-align: right; width: 100%; ">
                                      <div style="margin-top:10px; margin-bottom:10px;">
                                          </div>
                                  </td>
                                          </table>
                      </ItemTemplate>
                      <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                  </asp:DataList>
             </div>
        	
        </div>
    <br />
     <div class="row">
<div class="col-md-6">        
                	<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table" Enabled="False">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idDtl" HeaderText="#" >
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="matnErja" HeaderText="متن ارجاع">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="del" Text="حذف" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                  
                   </div>
			<div class="col-md-6">
                
                </div>
         
              </div>

       <div class="row">
			<div class="col-md-6">
                
                </div>
            <div class="col-md-6">  
                
                </div>
              </div>
         

               
           <asp:Panel ID="PanelAttach" runat="server" Enabled="False"  >
               <div class="row">
			  <div class="col-md-4">
                    <asp:FileUpload ID="FileUpload" runat="server" ForeColor="Black" style="background-color: #FFFFCC; margin-bottom: 0px;" Width="100%"  />
                  </div>
            <div class="col-md-4">
                 <asp:Button ID="btnSabtFile" runat="server"  class="btn btn-violet btn-sm waves-effect waves-light" Text="ثبت فایل " />
                                    </div>
                <div class="col-md-4">
              
                   
                  </div>
            </div>
               <div class="row">
			  <div class="col-md-12">
       <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <asp:GridView ID="ASPxGrvHead0" runat="server" AutoGenerateColumns="False" DataKeyNames="id" style="text-align: right" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                         
                                            
                            <asp:BoundField DataField="id" HeaderText="کد" Visible="False" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="content_name" HeaderText="نام فایل">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" style="color: #000000" Text="دانلود فایل " > </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                   <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDel_Click" style="color: #000000" Text="حذف فایل " BackColor="Yellow"> </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                          
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
      <%--  <asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="1000" Enabled="False" />
    </ContentTemplate>
</asp:UpdatePanel>   --%>         
                          </div>
          
        </div>
                </asp:Panel>
       
    <br />
     <div class="row">
          
         	<div class="col-md-2">
                	<asp:Label ID="Label4" runat="server" CssClass="control-label" Text="تاریخ شروع"></asp:Label>
               <input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/>
                </div>
         <div class="col-md-2">
                	<asp:Label ID="Label5" runat="server" CssClass="control-label" Text="تاریخ پایان"></asp:Label>
          <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly"/>
                </div>
		 
         
            <div class="col-md-2">
				<asp:Label ID="Label7" runat="server" CssClass="control-label" Text="ثبت در مدیریت دانش"></asp:Label>
               
                     <asp:CheckBox ID="daneshChek" runat="server" Enabled="False" style="color: #000000" />
			</div>
            <div class="col-md-2">
				<asp:Label ID="Label9" runat="server" CssClass="control-label" Text="ارسال sms"></asp:Label>
                 <asp:CheckBox ID="smsChek" runat="server" Enabled="False" style="color: #000000" />
			</div>
              <div class="col-md-2">
				<asp:Label ID="Label2" runat="server" CssClass="control-label" Text="غیرفعال؟"></asp:Label>

                <asp:CheckBox ID="ChbActiveCode" runat="server" Enabled="False" style="color: #000000" />
         </div>
          <div class="col-md-2">
				<asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="نمایش " Visible="False"></asp:Label>
               
                     <asp:CheckBox ID="showAll" runat="server" Enabled="False" style="color: #000000" Visible="False" />
			</div>
          </div>
    <br />
     
     <div class="row">
			<div class="col-md-12">

                		     <dx:ASPxGridView ID="ASPxGrvHead" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="kmID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد" FieldName="kmID" 
                            ShowInCustomizationForm="True" VisibleIndex="1">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# eval("kmID") %>' CommandName='<%# "select" %>' 
                                    Text='<%# eval("kmID") %>'></asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn Caption="غیر فعال" FieldName="CodeClosed" 
                            VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataCheckColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>

                </div>
         </div>
  
 <div class="row">
			<div class="col-md-12">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                </div>
         </div>
       <script type="text/javascript">
           var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
           var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
           //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

           //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
           //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
  <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>

