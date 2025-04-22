<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="trnSessionOfflinePersonal.aspx.vb" Inherits="trnSessionOfflinePersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

          <%--   <asp:Panel ID="Panel12" runat="server"  >--%>
      
       
                 <br />

                      <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" GroupingText=" درخواست شرکت در دوره آموزشی غیر حضوری">
                          <div class="alert-success">
                                                            <%--<p>بدينوسيله به استحضار مي رساند كه درخواست جنابعالي بعد از ثبت درخواست به كارتابل سرپرست مربوطه ارسال مي گردد تا ايشان ميزان تاثير گذاري دوره مذكور را در ارتقاي كيفيت وظايف شغلي جنابعالي تعيين نمايند.
درصورتي كه ميزان تاثير گذاري بالا يا متوسط باشد‌، پس از شركت در آزمون تعيين سطح و با لحاظ ميزان سابقه (اولويت با همكاراني است كه زير 10 سال سابقه داشته و تاكنون در كلاسهاي آموزشي مذكور شركت ننموده اند) و ميزان تاثير گذاري اعلامي توسط سرپرست مستقيم جنابعالي و با توجه به ظرفيت محدود كلاسها جهت شركت در دوره مذكور دعوت مي گرديد.
لكن در صورتي كه ميزان تاثير گذاري پايين اعلام شده باشد‌، جنابعالي مجاز به شركت در آزمون تعيين سطح و همچنين حضور در دوره هاي آموزشي مذكور نمي باشيد.
 
                                 
</p>--%>
                          </div>
                            <div class="alert-danger"
                                       >
                                       <%--<p>توجه : جهت اطلاع از نتيجه درخواست خود مي توانيد به بخش آموزش/ثبت نام دوره هاي آموزشي/ليست درخواستهاي من/نتيجه مراجعه نماييد</p>--%>

                                   </div>
                       <br />
                            <div class="row">
                                <div class="col-md-12">
                               
                                   
                                             
                                                               </div>
                                </div>
                            <br />
              
                              <div class="row">
              
			<div class="col-md-12">

               </div>
                                  </div>
                              <div class="row">
              
			<div class="col-md-6">
                 <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="دوره های آموزشی"></asp:Label>
                      <asp:DropDownList ID="citytToSiteNoRuzkar" runat="server"  class="btn btn-secondary dropdown-toggle" AutoPostBack="True" >
                                   </asp:DropDownList>
                            <asp:Label ID="lblBagi" runat="server" Text="2" BackColor="#FFFF66" Font-Bold="True" ForeColor="Green" Visible="false" ></asp:Label>
                               </div>
                      <div class="col-md-6">
                         
                              <asp:Label ID="lblTedad" runat="server" Text="0" BackColor="#FFFF66" Font-Bold="True" ForeColor="green" Visible="false" ></asp:Label>
                            <asp:Label ID="lblzarf" runat="server"  BackColor="#FFFF66" Font-Bold="True" ForeColor="green" Visible="false"></asp:Label>
                           
                              </div>
                      
                      
                      

             	
                      </div>
                      

                      
                          <br />
                                  <div class="row">
              
			<div class="col-md-4">
             
                               </div>
                      	<div class="col-md-4">
                                <asp:Button ID="BtnAdd" runat="server" Text="ثبت نام" class="btn btn-success btn-sm waves-effect waves-light" ValidationGroup="1"  />
                              </div>
                      <div class="col-md-4">
                       
                              <asp:HiddenField ID="HiddenFieldSabege" runat="server" />
                       
                              </div>

             	
                      </div>
                         
            <br />
                            <br />
                        </asp:Panel>
                 <br />
         
    
                  <div class="row">
                       <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="لیست دوره های غیرحضوری من"></asp:Label>
                                       	<div class="col-md-12">
                                               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                        Font-Size="10pt" TabIndex="12" Width="100%" style="color: #000000" 
                                        CellPadding="3" DataKeyNames="idd" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical">
                                                   <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="idd" HeaderText="#">
                                            <ItemStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="onvan" HeaderText=" زمان" />
                                            <asp:BoundField DataField="tarixShuru" HeaderText="ازتاریخ">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tarixEtmam" HeaderText="تا تاریخ">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                          
                                            <asp:BoundField DataField="tarix" HeaderText="تاریخ ثبت نام" />
                                          
                                            <asp:BoundField DataField="statValue" HeaderText="وضعیت" />
                                          
                                            <asp:BoundField DataField="sarparast" HeaderText="سرپرست مربوطه" />
                                            <asp:BoundField DataField="arzyabiAdmin" HeaderText="نتیجه" />
                                            <asp:BoundField DataField="scoreval" HeaderText="تعیین سطح" />
                                          
                                            <asp:BoundField DataField="notee" HeaderText="توضیحات آموزش" />
                                          
                                           <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelFile" runat="server"  OnClientClick="return confirm('آیا مطمئن هستید رزرو را ابطلال می کنید ?')"  OnClick="lnkDelFile_Click" style=" Background:#ffd800 "  Text="ابطال"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="Medium" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                               </div>
                      </div>
          <%--  </asp:Panel>--%>
  
    <br />

             <div class="row">
			<div class="col-md-3">
              
               
                           
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
 <asp:Button ID="btnBack" runat="server" class="btn btn-warning btn-sm waves-effect waves-light" Text="برگشت" />
                </div>
                  <div class="col-md-3">

                </div>
                </div>
       
 <asp:HiddenField ID="hdn_Final_key" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_tmp" runat="server" Value="0" />
</asp:Content>

