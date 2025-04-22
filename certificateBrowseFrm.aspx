<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="certificateBrowseFrm.aspx.vb" Inherits="certificateBrowseFrm" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div class="div_page" dir="rtl">
<div class="div_headp" dir="rtl">
                    <asp:ImageButton ID="ibtnExit" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Exit.png" ToolTip="خروج" Width="48px" />
                    <asp:ImageButton ID="ibtnxl" runat="server" Height="48px" 
                        ImageUrl="~/Icons/exel.JPG" ToolTip="استخراج اطلاعات به اکسل" 
                        Width="48px" />
                    <asp:ImageButton ID="ibtnPrint" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Print.png" ToolTip="چاپ" Width="48px" 
                        ValidationGroup="b" />
                    <asp:ImageButton ID="ibtnDeactived" runat="server" 
                        Height="48px" ImageUrl="~/Icons/reload.png" 
                        ToolTip="بازخوانی کدهای غیر فعال" Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnHistory" runat="server" Height="48px" 
                        ImageUrl="~/Icons/history.png" ToolTip="تاریخچه تغییرات" 
                        Width="48px" ValidationGroup="b" Visible="False" />
                    <asp:ImageButton ID="ibtnDelete" runat="server" Height="48px" 
                        ImageUrl="~/Icons/Delete.png" ToolTip="حذف" Width="48px" 
                        ValidationGroup="a" Visible="False" />
                    <asp:ImageButton ID="ibtnCancel" runat="server" Enabled="False" Height="48px" 
                        ImageUrl="~/Icons/Cancel2.png" ToolTip="انصراف" Width="48px" 
                        ValidationGroup="b" Visible="False" />
</div>
<div class="div_line">
            <div class="div_nev">
                از تاريخ</div>
            <div class="div_txt">
            
               <input id="pcal1" type="text"  class="pdate  txt_ r5 h25" name="datepicker" 
                    dir="ltr" style="width: 120px" readonly="readonly"/></div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:Label ID="lbldateE" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDateS" runat="server" Visible="False"></asp:Label>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
            </div>
                    <div class="div_val">
            </div>
        </div>
        <div class="div_line">
            <div class="div_nev">
                تا تاريخ</div>
            <div class="div_txt">
                <input id="pcal2" type="text"  class="pdate txt_ r5 h25" value="" 
                    name="datepicker2" style="width: 120px" readonly="readonly" 
                    __designer:mapid="16d"/><asp:ImageButton ID="ImageButton1" runat="server" Height="25px" 
                    ImageUrl="~/images/design_images/kfilereplace.png" Width="25px" />
            </div>
            <div id="cntt" style=" 
          float:right;
            width:625px;
          height:auto;
          text-align:left;
          vertical-align:middle;

          direction:rtl;">
                <asp:Label ID="lblCnt" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" style="color: #FF6600" Visible="False"></asp:Label>
                      </div>
        </div>

                <div class="div_grid">
            <dx:ASPxGridView ID="ASPxGrvHead" runat="server" AutoGenerateColumns="False"
                KeyFieldName="ID" ClientIDMode="AutoID" Width="100%">
                <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="0%">
                        <clearfilterbutton visible="True">
                        </clearfilterbutton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="نام" FieldName="nam" VisibleIndex="0" ShowInCustomizationForm="True" Width="8%">
                    </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="family" ShowInCustomizationForm="True" Caption="نام خانوادگی" VisibleIndex="1" Width="10%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="codePersoneli" ShowInCustomizationForm="True" Caption="کد پرسنلی" VisibleIndex="2" Width="5%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="value" Caption="نوع استخدام" VisibleIndex="2" Width="7%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="latter_date" ShowInCustomizationForm="True" Caption="تاریخ" VisibleIndex="3" Width="5%">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="gavahi_typ" ShowInCustomizationForm="True" 
                        Caption="نوع گواهی" VisibleIndex="4" Width="5%">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
    <cellstyle horizontalalign="Justify">
    </cellstyle>
</dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="بانک" FieldName="bank_nam" ShowInCustomizationForm="True" VisibleIndex="5" Width="5%">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="شعبه" FieldName="shobe_nam"
                        ShowInCustomizationForm="True" VisibleIndex="6" Width="5%">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="سازمان/موسسه" FieldName="moassese"
                        VisibleIndex="7" Width="5%">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="برای" FieldName="motafarege_nam" ShowInCustomizationForm="True" VisibleIndex="8" Width="10%">
    <editcellstyle horizontalalign="Justify">
    </editcellstyle>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statt" VisibleIndex="9" ShowInCustomizationForm="True" Width="5%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="stat" FieldName="stat" VisibleIndex="11" Width="0%" Visible="False">
                    </dx:GridViewDataTextColumn>
                                             <dx:GridViewDataTextColumn Caption="شماره نامه" FieldName="new_later_num" 
                            ShowInCustomizationForm="True" VisibleIndex="12" Width="5%">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                    CommandArgument='<%# Eval("new_later_num")%>' CommandName='<%# "select" %>' 
                                    Text='<%# Eval("new_later_num")%>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="کاربر" FieldName="karbarr" VisibleIndex="14" Width="8%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <cellstyle horizontalalign="Center">
                        </cellstyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager PageSize="20">
                </SettingsPager>
                <settings showfilterrow="True" showgrouppanel="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
            </dx:ASPxGridView>
        </div>
            <div class="div_line">
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <dx:ASPxGridViewExporter ID="grvExporter1" runat="server" 
                    GridViewID="ASPxGrvHead">
                </dx:ASPxGridViewExporter>
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
            </div>
            <div class="div_txt">
                <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
            </div>
            <div class="div_val">
            </div>
            <div class="div_nev">
                <asp:HiddenField ID="hdn_deactivedcode" runat="server" Value="false" />
            </div>
            <div class="div_txt">
            </div>
                    <div class="div_val">
            </div>
        </div>

        <script type="text/javascript">
            var objCal1 = new AMIB.persianCalendar('objCal1', 'pcal1');
            var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');
            //    	    var objCal2 = new AMIB.persianCalendar('objCal2', 'pcal2');

            //    	    var objCal3 = new AMIB.persianCalendar('objCal3', 'pcal3');
            //    	    var objCal4 = new AMIB.persianCalendar('objCal4', 'pcal4', { oExtraInputID: "extra", ExtraInputFormat: "yyyymmdd" });
	</script>

     </div>
     
</asp:Content>

