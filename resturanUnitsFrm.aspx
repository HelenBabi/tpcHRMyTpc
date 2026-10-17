<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="resturanUnitsFrm.aspx.vb" Inherits="resturanUnitsFrm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container mt-4 text-center">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <div class="card shadow-lg border-0 rounded-4">
                    <div class="card-header bg-gradient text-white fw-bold rounded-top-4">
                        🍽 رزرو غذای خوراک
                    </div>

                    <div class="card-body bg-light rounded-bottom-4">
                        <div class="row mb-4">
                            <div class="col-md-4">
                                <asp:Label ID="lblYear" runat="server" Text="سال:" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select" Height="38px"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblMonth" runat="server" Text="ماه:" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select" Height="38px"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 d-flex align-items-end">
                                <asp:Button ID="btnShow" runat="server" Text="نمایش تقویم" CssClass="btn btn-primary w-100 fw-bold shadow-sm" />
                            </div>
                        </div>

                      <!-- 🍽 بخش برنامه غذایی -->
<div id="menuSection" class="mt-5 text-center" runat="server" visible="false">

    <h4 class="fw-bold text-primary mb-4">
        🍽 برنامه غذایی ماه <asp:Label ID="lblMonthName" runat="server"></asp:Label>
    </h4>

    <!-- تصویر منو -->
    <div class="menu-frame shadow-lg rounded-4 mx-auto">
        <asp:ImageButton ID="imgMenuThumb" runat="server"
            CssClass="menu-thumb"
            AlternateText="برنامه غذایی ماه"
            OnClientClick="showMenuModal(); return false;" />
    </div>

   
</div>

<!-- 🌈 Modal بزرگنمایی -->
<div class="modal fade" id="menuModal" tabindex="-1" aria-labelledby="menuModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-xxl">
    <div class="modal-content border-0 rounded-4 shadow-lg">
      <div class="modal-header bg-gradient text-white rounded-top-4">
        <h5 class="modal-title fw-bold" id="menuModalLabel">
            🍽 برنامه غذایی ماه <asp:Label ID="lblModalMonth" runat="server"></asp:Label>
        </h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body bg-light text-center">
        <img id="modalMenuImage" src="" alt="Menu" class="img-fluid rounded-4 shadow-lg border border-3 border-white" />
      </div>
    </div>
  </div>
</div>

                        <!-- 🗓 جدول روزها -->
                        <asp:DataList ID="dlDays" runat="server"
                            RepeatColumns="7"
                            RepeatDirection="Horizontal"
                            CellPadding="5"
                            CssClass="table table-bordered text-center mt-5">
                            <ItemTemplate>
                                <div id="divDayBox" runat="server" class="day-box">
                                    <asp:CheckBox ID="chkDay" runat="server"
                                        CssClass="form-check-input day-selectable"
                                        Text='<%# Eval("DayText") %>'
                                        ToolTip='<%# Eval("FullDate") %>'
                                        Checked='<%# Eval("IsSelected") %>'
                                        Visible='<%# Eval("IsEditable") %>' />

                                    <asp:Label ID="lblDayPastSelected" runat="server"
                                        CssClass="badge bg-success text-white day-past"
                                        Text='<%# Eval("DayText") + " ✔" %>'
                                        Visible='<%# Not Eval("IsEditable") AndAlso Eval("IsSelected") %>' />

                                    <asp:Label ID="lblDayPastNotSelected" runat="server"
                                        CssClass="badge bg-secondary text-white day-past"
                                        Text='<%# Eval("DayText") + " ✖" %>'
                                        Visible='<%# Not Eval("IsEditable") AndAlso Not Eval("IsSelected") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>

                        <div class="text-center mt-3">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm px-4" Text="✅ ثبت انتخاب‌ها" OnClick="btnSave_Click" />
                        </div>
                        <br />
                        <div class="text-center mt-3">
                            <asp:Button ID="Button1" runat="server" class="btn btn-warning btn-sm px-4" Text="⬅ برگشت" />
                        </div>
                        <div class="text-center mt-3">
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-success fw-bold"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
/* فریم اصلی تصویر منو */
.menu-frame {
    width: 70%;              /* اندازه نرمال برای مانیتورهای بزرگ */
    max-width: 700px;        /* حداکثر اندازه */
    min-width: 300px;        /* حداقل اندازه برای موبایل */
    background: linear-gradient(145deg, #f8f9fa, #e9ecef);
    padding: 15px;
    border-radius: 20px;
    transition: all 0.3s ease-in-out;
}

.menu-frame:hover {
    transform: scale(1.02);
    box-shadow: 0 0 25px rgba(13, 110, 253, 0.25);
}

/* خود تصویر منو */
.menu-thumb {
    width: 100%;
    height: auto;
    border-radius: 15px;
    border: 3px solid #fff;
    cursor: pointer;
    box-shadow: 0 0 20px rgba(0,0,0,0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.menu-thumb:hover {
    transform: scale(1.03);
    box-shadow: 0 0 25px rgba(0, 123, 255, 0.4);
}

/* افکت مدال */
.modal-content {
    animation: fadeInScale 0.4s ease;
}

@keyframes fadeInScale {
    from { transform: scale(0.9); opacity: 0; }
    to { transform: scale(1); opacity: 1; }
}

/* هدر گرادیانی مدال */
.bg-gradient {
    background: linear-gradient(90deg, #0d6efd, #6610f2);
}

/* اندازه متعادل‌تر در مانیتورهای خیلی بزرگ */
@media (min-width: 1600px) {
    .menu-frame {
        width: 55%;
        max-width: 800px;
    }
}

/* موبایل */
@media (max-width: 768px) {
    .menu-frame {
        width: 90%;
    }
}
</style>

<!-- 💡 اسکریپت مدال -->
<script>
    function showMenuModal() {
        var img = document.getElementById('<%= imgMenuThumb.ClientID %>');
    if (!img) return;

    var modalImg = document.getElementById('modalMenuImage');
    modalImg.src = img.getAttribute('data-img');

    var myModal = new bootstrap.Modal(document.getElementById('menuModal'));
    myModal.show();
}
</script>

</asp:Content>