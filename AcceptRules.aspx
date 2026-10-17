<%@ Page Title="" Language="VB" MasterPageFile="~/dabir.master"
    AutoEventWireup="false" CodeFile="AcceptRules.aspx.vb" Inherits="AcceptRules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .spinner-border-sm {
            width: 1rem;
            height: 1rem;
            border-width: 0.15em;
            margin-left: 0.5rem;
            vertical-align: text-bottom;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-5">
    <div class="card shadow-lg p-4 mx-auto" style="max-width:600px;">
        <h3 class="card-title text-center mb-3">فرم پذیرش عضویت در انجمن صنفی</h3>
        <hr />

        <p>لطفاً متن زیر را با دقت مطالعه نموده و سپس موافقت خود را اعلام نمایید:</p>

        <div class="border rounded p-3 bg-light" style="text-align:justify; direction:rtl;">
            اینجانب <strong><%= Session("FullName") %></strong>، مطابق با اساسنامه انجمن صنفی کارکنان قرارداد مستقیم صنعت پتروشیمی تبریز،
            خواهان عضویت در انجمن به‌صورت سالانه می‌باشم.  
            بدین‌وسیله درخواست می‌نمایم مبلغ ماهیانه <strong>۲۵۰,۰۰۰ ریال (بیست و پنج هزار تومان)</strong> به‌عنوان حق عضویت ماهانه
            و مبلغ <strong>۵۰۰,۰۰۰ ریال (پنجاه هزار تومان)</strong> به‌عنوان ورودیه اولیه از حقوق اینجانب کسر گردد.
        </div>

        <div class="form-check mt-3">
            <asp:CheckBox ID="chkAccept" runat="server" CssClass="form-check-input" />
            <label class="form-check-label" for="chkAccept">
                مفاد فوق و کلیه قوانین عضویت را مطالعه کرده و با آن موافقم.
            </label>
        </div>

        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- دکمه تایید با Spinner -->
                <asp:Button ID="btnAccept" runat="server"
                    CssClass="btn btn-primary w-100"
                    Text="تأیید و دریافت کد"
                    OnClick="btnAccept_Click"
                    OnClientClick="return checkAcceptance(this);" />

                <br /><br />

                <!-- پنل وارد کردن کد تایید -->
                <asp:Panel ID="pnlVerify" runat="server" Visible="False">
                    <div class="alert alert-info">کد تأیید برای شماره موبایل شما ارسال شد.</div>

                    <asp:Label ID="lblTimer" runat="server" CssClass="text-warning d-block mb-2"></asp:Label>
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control mb-2" placeholder="کد تأیید"></asp:TextBox>

                    <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-success w-100"
                        Text="ثبت نهایی" OnClick="btnVerify_Click"
                        OnClientClick="return showSpinner(this);" />

                    <br />
                    <asp:Button ID="btnResend" runat="server" CssClass="btn btn-secondary w-100 mt-2"
                        Text="ارسال مجدد کد" OnClick="btnResend_Click" Visible="False"
                        OnClientClick="return showSpinner(this);" />

                    <br />
                    <asp:Label ID="lblInfo" runat="server" CssClass="text-center d-block mt-2"></asp:Label>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</div>

<script type="text/javascript">
    // چک کردن پذیرش قوانین قبل از ارسال فرم
    function checkAcceptance(btn) {
        var chk = document.getElementById('<%= chkAccept.ClientID %>');
        if (!chk.checked) {
            alert('لطفاً پیش از ادامه، قوانین و مقررات را تأیید نمایید.');
            return false;
        }
        return showSpinner(btn);
    }

    function showSpinner(btn) {
        setTimeout(function () {
            btn.disabled = true;
            if (btn.tagName === "INPUT") {
                btn.value = "در حال پردازش...";
            } else {
                btn.innerHTML = "در حال پردازش...";
            }
        }, 10);
        return true;
    }

    var countdownInterval;
    function startTimer(seconds) {
        var lblTimer = document.getElementById('<%= lblTimer.ClientID %>');
        var btnResend = document.getElementById('<%= btnResend.ClientID %>');
        btnResend.style.display = 'none';
        clearInterval(countdownInterval);

        var remaining = seconds;
        lblTimer.innerText = 'زمان باقی‌مانده: ' + remaining + ' ثانیه';

        countdownInterval = setInterval(function () {
            remaining--;
            if (remaining <= 0) {
                clearInterval(countdownInterval);
                lblTimer.innerText = '⏰ زمان اعتبار کد به پایان رسید.';
                btnResend.style.display = 'block';
            } else {
                lblTimer.innerText = 'زمان باقی‌مانده: ' + remaining + ' ثانیه';
            }
        }, 1000);
    }
</script>

</asp:Content>
