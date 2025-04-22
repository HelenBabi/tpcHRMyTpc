<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>
<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.1" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h4>کارتابل من</h4>
     <%-- <h3>1402/01/26 از طریق منوی نظام پیشنهادات می توانید پیشنهادهای فنی و غیرفنی ثبت و از همین سامانه پیگیری نمایید</h3>
      <h4>1401/12/20 از طریق منوی کارهای محوله میتوانید برای همکاران خود کار بفرستید و پیگیر باشید.</h4>
    <h6>1401/10/21 از طریق منوی عمومی گزینه ی پروفایل می توانید اطلاعات تلفن داخلی و منزل و موبایل خود را مشاهده و بروز نمایید</h6>   
         <h6>اضافه شدن درج آگهی لوازم مازاد کارکنان از طریق منوی عمومی قسمت ثبت آگهی</h6>   
           <h6>از این پس میتوانید درخواست های خود را ازطریق این سامانه به واحد فناوری اطلاعات ارسال نمایید</h6>           
            <h6>کارکنانی که از انبار ابزار کارگاه مرکزی ابزار به امانت گرفته اند میتوانند از منوی تعمیرات گزینه ابزارهای در تحویل من کارتکس خود را ببینند و درصورت مغایرت با شماره 2486 تماس بگیرند.</h6>--%>
      <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientIDMode="AutoID" Width="100%" 
        style="margin-top: 0px" TabIndex="3">
             <TabPages>
                 <dx:TabPage Name="trn" Text="آموزش">
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                           
                             
                               <p>سرپرست محترم : خواهشمند است جهت تعيين ميزان تاثيرگذاري دوره مذكور در ارتقاء كيفي انجام وظايف شغلي كاركنان‌، در جدول زير بر روي جزئيات كليك كرده و يكي از گزينه هاي ( بالا - متوسط - پايين ) را انتخاب كنيد .  
                                 
</p>
                        

                             <dx:ASPxGridView ID="trnGridWorkTable" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="idd"   
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">

    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

    <Settings ShowFilterRow="True" />

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

    <Columns>
            <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="5%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkButton4" runat="server" 
                    CommandArgument='<%# Eval("idd")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="جزئیات"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="پرسنل" FieldName="karbar"
                            VisibleIndex="1" ShowInCustomizationForm="True" 
            Width="15%">
        
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="دوره" FieldName="onvan" 
                            VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="از تاریخ" FieldName="tarixShuru" 
                            VisibleIndex="3" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تا تاریخ" FieldName="tarixShuru" 
                            VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="tarix" 
                            VisibleIndex="6" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
      
        
            <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statValue" VisibleIndex="7" Width="5%">
                <HeaderStyle HorizontalAlign="Center" />
                <cellstyle horizontalalign="Center">
                </cellstyle>
            </dx:GridViewDataTextColumn>
      
        
    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

    <SettingsPager PageSize="17">
    </SettingsPager>

<Settings ShowFilterRow="True"></Settings>

</dx:ASPxGridView>

                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>
                 <dx:TabPage Text="پیشنهادات دریافتی" Visible="False">
                     <ContentCollection>
                           <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                               <%--<dx:ASPxGridView ID="sugGridWorkTable" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="sugID"   
                    EnableCallBacks="False" ClientIDMode="AutoID" Width="100%">

<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

    <Columns>
            <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="5%">
            <DataItemTemplate>
                <asp:LinkButton ID="LinkBtnMosh" runat="server" 
                    CommandArgument='<%# Eval("sugID")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="جزئیات"></asp:LinkButton>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="کد پیشنهاد" FieldName="sugID"
                            VisibleIndex="0" ShowInCustomizationForm="True" 
            Width="5%">
        
            <HeaderStyle HorizontalAlign="Center" />
            <cellstyle horizontalalign="Center">
            </cellstyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                            VisibleIndex="1" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="ferestande" VisibleIndex="3" Width="15%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
            </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="جهت" FieldName="runeveshtTypeValue" 
                            VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Right">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ" FieldName="tarix" 
                            VisibleIndex="4" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ساعت" FieldName="saat" 
                            VisibleIndex="5" Width="10%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
      
        
            <dx:GridViewDataTextColumn Caption="متن ارجاع" FieldName="matnErja" VisibleIndex="6" Width="15%">
                <HeaderStyle HorizontalAlign="Center" />
                <cellstyle horizontalalign="Center">
                </cellstyle>
            </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="وضعیت" FieldName="statusValue" VisibleIndex="7" Width="5%">
                <HeaderStyle HorizontalAlign="Center" />
                <cellstyle horizontalalign="Center">
                </cellstyle>
            </dx:GridViewDataTextColumn>
        
    </Columns>

    <SettingsBehavior AllowFocusedRow="True" AutoFilterRowInputDelay="120000" />

    <SettingsPager PageSize="17">
    </SettingsPager>

    <Settings ShowFilterRow="True" />

</dx:ASPxGridView>--%>
                               </dx:ContentControl>
                     </ContentCollection>
                 </dx:TabPage>


                   
                 <dx:TabPage Text="کارهای دریافتی" Visible="False">
                     <contentcollection>
                         <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                             <dx:ASPxGridView ID="jobGrid" runat="server" 
                    AutoGenerateColumns="False" 
                    KeyFieldName="kmID"  
                    EnableCallBacks="False" CssClass="gr_" Width="100%" ClientIDMode="AutoID">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}" />

                    <SettingsBehavior AllowFocusedRow="True" />

                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" />
<ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                          <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
            Width="7%">
            <DataItemTemplate>
             <%--   <asp:LinkButton ID="LinkButton1" runat="server" 
                    CommandArgument='<%# Eval("kmID")%>' CommandName='<%# "moshahede" %>' 
                    style="color: #000000; font-weight: 700;" Text="مشاهده"></asp:LinkButton>--%>
            </DataItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1">
                            <clearfilterbutton visible="True">
                            </clearfilterbutton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="کد" FieldName="kmID" 
                            ShowInCustomizationForm="True" VisibleIndex="2" >
                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="موضوع" FieldName="subject" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="فرستنده" FieldName="karbar" VisibleIndex="4">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="date_start" VisibleIndex="5">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="date_end" VisibleIndex="6">
                            <HeaderStyle HorizontalAlign="Center" />
                            <cellstyle horizontalalign="Center">
                            </cellstyle>
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="الویت" FieldName="valuee" VisibleIndex="7">
                               <HeaderStyle HorizontalAlign="Center" />
                               <cellstyle horizontalalign="Center">
                               </cellstyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="olaviat" Visible="False">
        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="درصد پیشرفت" FieldName="darsad" VisibleIndex="9">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="نوع کار" FieldName="jobTypeValue" VisibleIndex="10">
                              <HeaderStyle HorizontalAlign="Center" />
                              <cellstyle horizontalalign="Center">
                              </cellstyle>
                          </dx:GridViewDataTextColumn>
                    </Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                    <settingspager pagesize="25">
                    </settingspager>

<Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>
                         </dx:ContentControl>
                     </contentcollection>
                 </dx:TabPage>


                   
             </TabPages>
           
    </dx:ASPxPageControl>
     <asp:HiddenField ID="HiddenFieldUserID" runat="server" Value="0" />
</asp:Content>

