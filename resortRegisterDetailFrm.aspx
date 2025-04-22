<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="resortRegisterDetailFrm.aspx.vb" Inherits="resortRegisterDetailFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                <h6>لطفا اطلاعات مهمان را کامل وارد نمایید</h6>
                </div>
          </div>
     <div class="row">
			<div class="col-md-4">
                 <asp:Label ID="lblRamz" runat="server" CssClass="control-label" Text="نام"></asp:Label>
                <asp:TextBox ID="nam" CssClass="form-control"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="nam" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
          <div class="col-md-4">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="نام خانوادگی"></asp:Label>
                <asp:TextBox ID="lnam" CssClass="form-control"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="lnam" ValidationGroup="a"></asp:RequiredFieldValidator>
              </div>
         <div class="col-md-4">
              <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="جنسیت" ValidationGroup="a"></asp:Label>
             <asp:DropDownList ID="jens" runat="server"   CssClass="btn btn-default dropdown-toggle">
                 <asp:ListItem Value="0">مرد</asp:ListItem>
                 <asp:ListItem Value="1">زن</asp:ListItem>
              </asp:DropDownList>
          
              </div>
         </div>
     <div class="row">
			<div class="col-md-4">
                 <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="نوع سپرده"></asp:Label>
               <asp:DropDownList ID="CmbSeporedeTyp" runat="server" 
                                       CssClass="btn btn-default dropdown-toggle"> 
                                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="CmbSeporedeTyp" InitialValue="0" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </div>
          <div class="col-md-4">
                 <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="شماره کارت"></asp:Label>
              <asp:TextBox ID="txtShomarecart" runat="server" CssClass="form-control" 
                                        ValidationGroup="a" ></asp:TextBox>
              
              </div>
         <div class="col-md-4">
              <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="علت ملاقات"></asp:Label>
             <asp:DropDownList ID="CmbEllat" runat="server" 
                                       CssClass="btn btn-default dropdown-toggle" >
                                    </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="CmbEllat" InitialValue="0" ValidationGroup="a"></asp:RequiredFieldValidator>
              </div>
         </div>
      <div class="row">
			<div class="col-md-4">
                 <asp:Label ID="Label6" runat="server" CssClass="control-label" Text="تاریخ مراجعه"></asp:Label>
<input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/>
                </div>
          	<div class="col-md-4">
                    <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="شماره موبایل"></asp:Label>
                <asp:TextBox ID="txtotag" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtotag" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
          	<div class="col-md-4">
                    <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="سریال لب تاپ"></asp:Label>
                <asp:TextBox ID="txtCodemojavez" runat="server" CssClass="form-control" ValidationGroup="a" 
                                      ></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtCodemojavez" ValidationGroup="a"></asp:RequiredFieldValidator>
                                  </div>
          </div>
    <div class="row">
			<div class="col-md-6">
                 <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="توضیحات"></asp:Label>
                 <asp:TextBox ID="txtNotee" runat="server" CssClass="form-control" ValidationGroup="a" placeholder="اطلاعات همراه یا همراهان مهمان یا سایر توضیحات"
                                      TextMode="MultiLine" Height="100"></asp:TextBox>
                </div>
        <div class="col-md-6">
            </div>
        </div>
      <br />
             <div class="row">
			<div class="col-md-3">
              
                 <asp:Button ID="btnBrwsSys" runat="server" class="btn btn-success btn-sm waves-effect waves-light" Text="ذخیره" ValidationGroup="a" />
                           
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                <div class="col-md-3">

                </div>
                  <div class="col-md-3">

                </div>
                </div>
      <script type="text/javascript">
          var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
          var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
          //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

          //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
          //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>
</asp:Content>

