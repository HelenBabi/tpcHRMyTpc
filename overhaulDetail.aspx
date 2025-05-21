<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="overhaulDetail.aspx.vb" Inherits="overhaulDetail" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %><%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

 

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function printCard() {
        var fullName = '<%= fullName %>';
        var personnelCode = '<%= personnelCode %>';
        var photoPath = '<%= photoPath %>';
        var signaturePath = '<%= signaturePath %>';
        var expirationDate = '<%= expirationDate %>';
        var html = `
<html>
<head>
  <meta charset="UTF-8">
  <style>
      body {
            margin: 0;
            padding: 0;
          font-family: Tahoma, sans-serif;
      }
        .container {
            display: flex;
            width: 16cm;
            height: 9cm;
            border: 1px solid black;
            box-sizing: border-box;
            direction: rtl;
        }
        .back, .front {
            width: 50%;
            padding: 0.5cm;
            box-sizing: border-box;
            border-left: 1px solid black;
            position: relative;
        }
        .back {
            font-size: 12px;
            line-height: 1.7;
        }
        .back strong {
            display: inline-block;
            margin-top: 0.3cm;
        }
        .front {
            text-align: center;
        }
        .photo {
            width: 3cm;
            height: 4cm;
            border: 1px solid black;
            margin: 0 auto 0.3cm;
            object-fit: cover;
        }
        .info {
            text-align: right;
            font-size: 12px;
            margin-top: 0.2cm;
            padding: 0 0.2cm;
        }
        .red {
            color: red;
            font-weight: bold;
        }
        .signature {
            margin-top: 0.5cm;
            text-align: right;
        }
        .signature img {
            width: 3cm;
            height: auto;
            display: block;
            margin-top: 0.2cm;
        }
                @media print {
                    body {
                        margin: 0;
                    }
                }
        </style>
    </head>
    <body>
        <div class="container">
            <div class="back">
                <strong>کارت تردد موقت برای تعمیرات کوتاه مدت</strong><br/><br/>
                ۱. این کارت فقط برای تردد در زمان اورهال صادر شده و ارزش دیگری ندارد.<br/>
                ۲. هرگونه سواستفاده احتمالی به عهده گیرنده و دارنده کارت می‌باشد.<br/>
                ۳. در صورت مفقود شدن کارت، مراتب را فوراً به واحد حراست اطلاع دهید.<br/>
                ۴. از یابنده تقاضا می‌شود کارت را به صندوق پستی ۵۱۷۴۵-۳۵۴ ارسال نماید.<br/><br/>
                <strong>تلفن‌های داخلی ضروری مجتمع:</strong><br/>
        حراست: ۱۱۰ - آتش‌نشانی: ۱۱۲ - اورژانس: ۱۱۵
                </div>
                <div class="front">
                    <strong>کارت تردد موقت برای تعمیرات کوتاه مدت</strong>
                    <img src="${photoPath}" alt="عکس" class="photo" />
                    <div class="info">
                        <div><strong>شماره پرسنلی:</strong> ${personnelCode}</div>
                         <br />
                        <div><strong>نام:</strong> ${fullName}</div>
                         <br />
                        <div><strong class="red">تاریخ انقضا:</strong> ${expirationDate}</div>
                        <div class="signature">
                            <strong>امضا:</strong>
                            <img src="${signaturePath}" alt="امضا" />
                        </div>
                    </div>
                </div>
            </div>
            <script>
                window.onload = function () {
                    window.print();
                    setTimeout(() => { window.close(); }, 500);
    }
    <\/script>
</body>
</html>`;

    var win = window.open('', '', 'width=800,height=600');
    win.document.open();
    win.document.write(html);
    win.document.close();
    }
</script>



    <style>
        body {
            font-family: "B Nazanin", Tahoma, sans-serif;
            background: #f2f2f2;
            padding: 20px;
        }

        .print-area {
            background: white;
            padding: 30px;
            max-width: 1000px;
            margin: auto;
            border: 1px solid #ccc;
        }

        .print-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 2px solid #444;
            margin-bottom: 20px;
            padding-bottom: 10px;
        }

        .header-title {
            font-size: 24px;
            font-weight: bold;
            color: #333;
        }

        .photo-box {
            width: 110px;
            height: 140px;
            border: 1px solid #777;
            overflow: hidden;
        }

        .photo-box img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

        .info-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .info-table td {
            border: 1px solid #999;
            padding: 8px 12px;
        }

        .info-table td:nth-child(odd) {
            background-color: #eef3ff;
            font-weight: bold;
            width: 16%;
        }

        .info-table td:nth-child(even) {
            width: 17%;
        }

        .no-print {
            text-align: center;
            margin-top: 30px;
        }

        @media print {
            .no-print {
                display: none !important;
            }

            body {
                background: white;
                margin: 0;
            }

            @page {
                size: A4;
                margin: 2cm;
            }

            .print-area {
                border: none;
                padding: 0;
            }
        }
    </style>

    <div class="card mb-3">
  <div class="card-header bg-primary text-white">ارجاعات</div>
  <div class="card-body">
     <asp:GridView ID="GridViewErja" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" ForeColor="Black" GridLines="Vertical" CssClass="table" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="ferestande" HeaderText="فرستنده" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="girande" HeaderText="گیرنده" />
                                            <asp:BoundField DataField="FormattedNumber" HeaderText="چارت پیشنهادی" />
                                           
                                            <asp:BoundField DataField="matnErja" HeaderText="متن ارجاع">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ ارجاع" />
                                            <asp:BoundField DataField="saat" HeaderText="ساعت" />
                                           
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
  </div>
</div>
         <!-- دکمه چاپ -->
    <%--    <div class="no-print">
            <asp:Button ID="btnPrint" runat="server" Text="چاپ فرم" CssClass="btn btn-success"
                OnClientClick="window.print(); return false;" />
        </div>--%>

    
     <div class="print-area" style="width:100%">
    <div class="print-header">
        <div class="header-title">            
</div>
        <%--<div class="photo-box">
            <asp:Image ID="PersonImage" runat="server" ImageUrl="~/Images/NoImage.png" />
        </div>--%>
        
    </div>

    <table class="info-table" style="width:100%">
        <tr>
            <td>کد پیگیری</td>
            <td><asp:Label ID="Id" runat="server" /></td>
            <td>نام</td>
            <td><asp:Label ID="FirstName" runat="server" /></td>
            <td>نام خانوادگی</td>
            <td><asp:Label ID="LastName" runat="server" /></td>
        </tr>
        <tr>
            <td>نام پدر</td>
            <td><asp:Label ID="FatherName" runat="server" /></td>
            <td>کد ملی</td>
            <td><asp:Label ID="Nationality" runat="server" /></td>
            <td>شماره شناسنامه</td>
            <td><asp:Label ID="ShSh" runat="server" /></td>
        </tr>
        <tr>
            <td>تاریخ تولد</td>
            <td><asp:Label ID="BirthDay" runat="server" /></td>
            <td>شماره کارت</td>
            <td><asp:Label ID="BankCardNo" runat="server" /></td>
            <td>شماره حساب</td>
            <td><asp:Label ID="IBAN" runat="server" /></td>
        </tr>
        <tr>
            <td>نوع بیمه</td>
            <td><asp:Label ID="bime" runat="server" /></td>
            <td>شماره بیمه</td>
            <td><asp:Label ID="SSN" runat="server" /></td>
            <td>موبایل</td>
            <td><asp:Label ID="Mobile" runat="server" /></td>
        </tr>
        <tr>
            <td>تلفن ثابت</td>
            <td><asp:Label ID="Tell" runat="server" /></td>
            <td>کد پستی</td>
            <td><asp:Label ID="PostalCode" runat="server" /></td>
            <td>مدرک</td>
            <td><asp:Label ID="Degree" runat="server" /></td>
        </tr>
        <tr>
            <td>رشته تحصیلی</td>
            <td><asp:Label ID="Major" runat="server" /></td>
            <td>گرایش</td>
            <td><asp:Label ID="EducationalOrientation" runat="server" /></td>
            <td>وضعیت تاهل</td>
            <td><asp:Label ID="tahol" runat="server" /></td>
        </tr>
        <tr>
            <td>وضعیت نظام وظیفه</td>
            <td><asp:Label ID="xedmat" runat="server" /></td>
            <td>سابقه ایثارگری</td>
            <td><asp:Label ID="isargari" runat="server" /></td>
            <td>سابقه اورهال</td>
            <td><asp:Label ID="IsLastOverhaul" runat="server" /></td>
        </tr>
        <tr>
            <td>سمت اورهال قبلی</td>
            <td><asp:Label ID="JobPosition" runat="server" /></td>
            <td>وضعیت</td>
            <td><asp:Label ID="lblStatus" runat="server" BackColor="#FFFF66" /></td>
            <td>شیفت</td>
            <td><asp:Label ID="lblShift" runat="server" BackColor="#FFFF66" /></td>
        </tr>
        <tr>
            <td>استان</td>
            <td><asp:Label ID="province" runat="server" /></td>
                    <td>شهر</td>
            <td><asp:Label ID="city" runat="server" /></td>
                    <td>آدرس</td>
            <td><asp:Label ID="Address" runat="server" /></td>
        </tr>
    </table>
</div>


     <div class="row">
			<div class="col-md-3">
                <div class="form-check">
            <asp:CheckBox ID="CheckBoxAttach" runat="server" CssClass="form-check-input" AutoPostBack="True" />
            <label class="form-check-label fw-bold text-dark ms-2" for="CheckBoxAttach">
                مشاهده ضمایم
            </label>
        </div>
                </div>
         <br />
         <div class="col-md-3">
              <asp:Button ID="btnOk" runat="server" CssClass="btn btn-success btn-sm w-100" Text="تایید می‌شود" Visible="False" />
                </div>
         <br />
         <div class="col-md-3">
              <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-danger btn-sm w-100" Text="تایید نمی‌شود" Visible="False" />
                </div>
         <div class="col-md-3">
                <asp:Button ID="btnHse" runat="server" CssClass="btn btn-success btn-sm w-100" Text="ارسال به HSE" Visible="false"  />
                 <asp:HiddenField ID="hdn_tmp" runat="server" />
                </div>
         </div>
     <br />
       <asp:Panel ID="PanelMadarek" runat="server" Visible="false" BorderStyle="Solid" GroupingText="کنترل مدارک">
               <div class="row">
                <div class="col-md-3" style="direction:ltr">
                    <asp:CheckBox ID="CheckBoxMadarek" runat="server"  Text="مدارک کامل است" AutoPostBack="True" />
                 	
               </div>
			<div class="col-md-3">
                               	<asp:Label ID="Label11" runat="server"   CssClass="control-label"  Text="کدام مدرک ناقص است؟"></asp:Label>
                <asp:DropDownList ID="cmbmadarek" runat="server"  Width="100%" Height="50px"  >
                    <asp:ListItem Value="1">عکس پرسنلی</asp:ListItem>
                    <asp:ListItem Value="2"> تصویر کارت ملی </asp:ListItem>
                     <asp:ListItem Value="3">صفحه اول شناسنامه</asp:ListItem>
                    <asp:ListItem Value="4"> صفحه توضیحات شناسنامه </asp:ListItem>
                    <asp:ListItem Value="5">آخرین مدرک تحصیلی</asp:ListItem>
                    <asp:ListItem Value="6"> گواهی عدم سوپیشینه </asp:ListItem>
                     <asp:ListItem Value="7">گواهی خدمت یا معافیت یا کارت دانشجویی</asp:ListItem>
                    <asp:ListItem Value="8"> کارت سلامت </asp:ListItem>
                                </asp:DropDownList>
      
 
			</div>
         
              <div class="col-md-6">
                                <div class="mt-2">
        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-sm w-100" Text="افزودن" ValidationGroup="c" />
      </div>
                  </div>
          	</div>
           <div class="row"
               >
                 <div class="col-md-6">
                <div class="card-body">
     <asp:GridView ID="gridMadrak" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
        Width="100%" CssClass="table table-bordered table-hover text-center">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
          <asp:BoundField DataField="idDtl" HeaderText="#" />
          <asp:BoundField DataField="madrakName" HeaderText="نوع مدرک" />
            <asp:BoundField DataField="tarix" HeaderText="تاریخ" />
          <asp:BoundField DataField="saat" HeaderText="ساعت" />
          <asp:ButtonField CommandName="del" Text="حذف" />
        </Columns>
        <HeaderStyle CssClass="table-dark" />
        <RowStyle BackColor="#EFF3FB" />
      </asp:GridView>
  </div>

  </div>
               <div class="col-md-6">
       
</div>
           </div>
       

    </asp:Panel>
        <br />

     <asp:Panel ID="PanelSms" runat="server" Visible="false">
          <div class="row">
			<div class="col-md-3">

               	<asp:Label ID="Label1" runat="server" CssClass="control-label"  Text="تاریخ مصاحبه"></asp:Label>
              <asp:TextBox ID="mosahebeDate" runat="server" CssClass="form-control" Width="100px"  />
 

			</div>
              	<div class="col-md-2" >
                               	<asp:Label ID="Label5" runat="server" CssClass="control-label"  Text="ساعت مصاحبه"></asp:Label>
                <asp:DropDownList ID="cmbMosahebeDate" runat="server" Width="100px" style="direction:ltr">
                    <asp:ListItem Value="1">8-12</asp:ListItem>
                    <asp:ListItem Value="2">14-18</asp:ListItem>
                                </asp:DropDownList>
      
 
			</div>
               <div class="col-md-2">
                   <asp:Button ID="btnSms" runat="server" class="btn btn-primary btn-sm w-100" Text="ایجاد متن پیامک" Width="150px" />
                  </div>

           <div class="col-md-5">
         <label for="txterja" class="form-label fw-bold text-dark"></label>
      <asp:TextBox ID="txtSms" runat="server" Height="200px"
        CssClass="form-control" placeholder="دکمه ایجاد متن پیامک را کلیک نمایید " ValidationGroup="a" Width="100%" TextMode="MultiLine"  />
        
        </div>
             
          	</div>

    </asp:Panel>
      <asp:Panel ID="PanelDate" runat="server" Visible="false">
          <div class="row">
			<div class="col-md-8">

               	<asp:Label ID="lblDate" runat="server" CssClass="control-label"  Text="تاریخ شروع بکار"></asp:Label>
              <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"  />
    <br />
   
 <asp:Button ID="BtnStartWork" runat="server" class="btn btn-primary btn-sm w-100" Text="ثبت شروع به کار" />
			</div>
           <div class="col-md-4">
                
               </div>
          	</div>

    </asp:Panel>
          <br />


<asp:Panel ID="PanelRunevest" runat="server" GroupingText="ارجاع پرونده" CssClass="border rounded p-4 mt-4 shadow-sm bg-light" BackColor="#F0FFF0">
  <div class="row g-4">
    <div class="col-md-4">
        واحد<label class="form-label fw-bold text-dark" for="cmbRunevesht"> گیرنده</label>
<asp:DropDownList 
    ID="cmbRunevesht" 
    runat="server" 
    CssClass="form-control selectpicker" 
        data-width="100%"
    title="جستجو کنید..." Height="50px" Width="100%">
</asp:DropDownList>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbRunevesht"
        ErrorMessage="لطفا گیرنده را انتخاب نمایید" ForeColor="#FF33CC" InitialValue="0" ValidationGroup="c" />
    </div>
      <div class="col-md-4">
      <label for="txtMatnErja" class="form-label fw-bold text-dark">متن</label>
      <asp:TextBox ID="txtMatnErja" runat="server" Height="50px"
        CssClass="form-control" placeholder="متن ارجاع را در این قسمت بنویسید" ValidationGroup="a" Width="100%" />
         <div class="mt-2">
        <asp:Button ID="btnRunevesht" runat="server" CssClass="btn btn-primary btn-sm w-100" Text="افزودن" ValidationGroup="c" />
      </div>
    </div>
       <div class="col-md-4">
               <asp:Label ID="lblruneveshtType" runat="server" Text="چارت پیشنهادی"></asp:Label>
      <dx:ASPxComboBox ID="CmbruneveshtType" ClientInstanceName="ClientComboBox" DropDownRows="10" CallbackPageSize="5"
        ForceDataBinding="true" TextField="Name" Width="100%" runat="server" ClientIDMode="AutoID"
        IncrementalFilteringMode="Contains" ValueType="System.String"
        CssClass="form-control" HorizontalAlign="Right">
        <ItemStyle>
          <SelectedStyle CssClass="selectedItem" />
          <HoverStyle CssClass="hoveredItem" />
        </ItemStyle>
        <dropdownbutton imageposition="Right"></dropdownbutton>
      </dx:ASPxComboBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CmbruneveshtType"
        ErrorMessage="لطفا جارت پیشنهادی را انتخاب نمایید" ForeColor="#FF33CC" InitialValue="0" ValidationGroup="c" />
    </div>
    <div class="row">
    <div class="col-md-6">
         <label for="txterja" class="form-label fw-bold text-dark"></label>
      <asp:TextBox ID="txtMatnErjaa" runat="server" Height="50px"
        CssClass="form-control" placeholder="متن  را در این قسمت بنویسید" ValidationGroup="a" Width="100%" Visible="False" />
        
        </div>
          <div class="col-md-6">

        </div>
        </div>
  
  </div>

  <hr class="my-4" />

  <div class="row mb-3">
    <div class="col-md-12">
      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Font-Size="10pt"
        Width="100%" CssClass="table table-bordered table-hover text-center">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
          <asp:BoundField DataField="idDtl" HeaderText="#" />
          <asp:BoundField DataField="omur" HeaderText="گیرنده" />
            <asp:BoundField DataField="Title" HeaderText="چارت پیشنهادی" />
          <asp:BoundField DataField="matnErja" HeaderText="متن ارجاع" />
          <asp:ButtonField CommandName="del" Text="حذف" />
        </Columns>
        <HeaderStyle CssClass="table-dark" />
        <RowStyle BackColor="#EFF3FB" />
      </asp:GridView>
    </div>
  </div>

  <div class="row">
    <div class="col-md-12 text-end">
      <asp:Button ID="btnSaveFinal" runat="server" CssClass="btn btn-success btn-sm" Text="ذخیره و ارجاع" ValidationGroup="c" Visible="False" />
    </div>
  </div>
</asp:Panel>


       <br />
     <asp:Panel ID="PanelAttach" runat="server" GroupingText="ضمایم" CssClass="border rounded p-3 mt-3" BackColor="#FFFFCC" Visible="False">
    <div class="row">
        <asp:Repeater ID="RepeaterImages" runat="server">
            <ItemTemplate>
                <div class="col-md-3 col-sm-6 mb-4">
                    <div class="card shadow-sm text-center">
                        <img src='<%# Eval("Url") %>' class="card-img-top" style="height:150px; object-fit:cover;" />
                        <div class="card-body p-2">
                            <a href='<%# "DownloadImage.aspx?nat=" & Eval("Nat") & "&file=" & Eval("Name") %>' class="btn btn-sm btn-outline-primary">
                                دانلود
                            </a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="row mt-3">
        <div class="col-12 text-end">
            <asp:Button ID="btnDownloadZip" runat="server" Text="دانلود همه" CssClass="btn btn-success btn-sm" OnClick="btnDownloadZip_Click" />
        </div>
    </div>
</asp:Panel>
    <br />
  
        <br />
    <asp:Panel ID="PanelEmeni" runat="server" Visible="false">
          <div class="row">
			<div class="col-md-4">

               	<asp:Label ID="Label4" runat="server" CssClass="control-label"  Text="نمره آزمون"></asp:Label>
              <asp:TextBox ID="txtNomre" runat="server" CssClass="form-control"  TextMode="Number" />
    <br />
   
 <asp:Button ID="btnEmeni" runat="server" class="btn btn-primary btn-sm w-100" Text="ثبت نمره آزمون" />
			</div>
           <div class="col-md-4">
                
               </div>
               <div class="col-md-4">
                
               </div>
          	</div>

    </asp:Panel>
          <br />
    <asp:Panel ID="PanelHR" runat="server" Visible="false">
          <div class="row">
			<div class="col-md-4">

               	<asp:Label ID="Label6" runat="server" CssClass="control-label"  Text="کد پرسنلی"></asp:Label>
              <asp:TextBox ID="codepersoneliTxt" runat="server" CssClass="form-control"  TextMode="Number" MaxLength="6" />
    <br />
   
 <asp:Button ID="BtnHr" runat="server" class="btn btn-primary btn-sm w-100" Text="ثبت کدپرسنلی" />
			</div>
           <div class="col-md-4">
                
               </div>
               <div class="col-md-4">
                
               </div>
          	</div>

    </asp:Panel>
    <br />
   
    <br />
    <asp:Panel ID="PanelHerasat" runat="server" Visible="false">
          <div class="row">
            <div class="col-md-4">
                 	<asp:Label ID="Label7" runat="server" CssClass="control-label"  Text="تاریخ انقضای کارت"></asp:Label>
              <asp:TextBox ID="expiretxt" runat="server" CssClass="form-control"  />
               </div>
              <div class="col-md-4">
                  <asp:Button ID="btnExpire" runat="server" class="btn btn-primary btn-sm w-100" Text="ثبت تاریخ انقضا" />
                  </div>
                 <div class="col-md-4">
                
                         <asp:Button ID="btnPrintCard" runat="server" class="btn btn-warning btn-sm w-100" Text="چاپ کارت" OnClientClick="printCard(); return false;" />

                     <button onclick="printCards()">چاپ کارت‌ها</button>

               </div>
          	</div>
        
    </asp:Panel>
    <br />
       <asp:Panel ID="PanelHse" runat="server" Visible="false" BorderStyle="Solid">
           <div class="row"
               >
                 <div class="col-md-6">
                <div class="card-body">
     <asp:GridView ID="GridHse" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" ForeColor="Black" GridLines="Vertical" CssClass="table" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="shifName" HeaderText="تابم" />
                                                                                      
                                            <asp:BoundField DataField="countt" HeaderText="تعداد">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                         
                                           
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
  </div>

  </div>
               <div class="col-md-6">
</div>
           </div>
          <div class="row">
                <div class="col-md-3">
                 	<asp:Label ID="Label3" runat="server" CssClass="control-label"  Text="تاریخ کلاس" ></asp:Label>
              <asp:TextBox ID="txtDateKelas" runat="server" CssClass="form-control"  Width="100px" Height="50px"/>
               </div>
			<div class="col-md-3">
                               	<asp:Label ID="Label2" runat="server" CssClass="control-label"  Text="تایم کلاس"></asp:Label>
                <asp:DropDownList ID="cmbKelas" runat="server"  Width="100px" Height="50px" >
                    <asp:ListItem Value="1">ساعت 8</asp:ListItem>
                    <asp:ListItem Value="2"> ساعت 14</asp:ListItem>
                                </asp:DropDownList>
      
 
			</div>
         
              <div class="col-md-6">
                  <asp:Button ID="BtnHseSave" runat="server" class="btn btn-success btn-sm w-100" Text="ثبت و ارجاع به ایمنی" />
                  </div>
          	</div>

    </asp:Panel>
    
    <br />
    <asp:Panel ID="PanelFileUpload" runat="server" Visible="false">

<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card shadow p-4 rounded-4">
                <%--<h4 class="text-center mb-4"> بارگذاری مدارک</h4>--%>

                <label>آپلود کارت سلامت:</label>
                <asp:FileUpload ID="FileUploadHealth" runat="server" CssClass="form-control mb-3" />

                <label>آپلود برگه معاینه:</label>
                <asp:FileUpload ID="FileUploadCheckup" runat="server" CssClass="form-control mb-3" />

                <asp:Button ID="btnUpload" runat="server" Text="ثبت فایل‌" CssClass="btn btn-primary w-100" OnClick="btnUpload_Click" />

                <div class="mt-3">
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
                </div>
            </div>
        </div>
    </div>
</div>


    </asp:Panel>
    <br />
   <div class="card mt-4">
  <div class="card-header bg-light">
          <asp:Label ID="lblNazar" runat="server" Text="ثبت نظر"></asp:Label>
  </div>
  <div class="card-body">
    <div class="mb-3">
      <asp:TextBox ID="txtNazar" runat="server" class="form-control" TextMode="MultiLine" Rows="4"
        placeholder="توضیحات خود را بنویسید..." Height="75px" Width="100%"></asp:TextBox>
    </div>
    <asp:Button ID="btnNazar" runat="server" class="btn btn-primary btn-sm w-100" Text="ثبت توضیحات" />
    <asp:Button ID="ImageButton1" runat="server" class="btn btn-warning btn-sm w-100" Text="برگشت" />
  </div>
</div>
     

    <br />

<div class="row">
    <div class="col-12">
        <asp:DataList ID="DataList1" runat="server" CssClass="w-100" RepeatLayout="Flow">
            <ItemTemplate>
                <div class="card mb-3 shadow border border-primary rounded-3">
                    <div class="card-header bg-primary text-white d-flex align-items-center">
                        <asp:Image ID="Image_pp" runat="server" CssClass="rounded-circle border border-white me-3" Height="50px" Width="50px" ImageUrl='<%# Eval("picc") %>' />
                        <asp:Label ID="lbl_user" runat="server" CssClass="fw-bold fs-5" Text='<%# Eval("headd") %>' />
                    </div>
                    <div class="card-body bg-light">
                        <asp:Label ID="lbl_note" runat="server" CssClass="text-dark fs-6 lh-lg" Text='<%# Eval("notee") %>' />
                        <asp:Label ID="lblUsrTyp" runat="server" Text='<%# Eval("ID") %>' Visible="False" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>


       <br />


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



