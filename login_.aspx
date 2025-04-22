<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login_.aspx.vb" Inherits="login_" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="login_.css" rel="stylesheet" type="text/css" />
    <link href="scroller/css/scroller.css" rel="stylesheet" type="text/css" />
    <script src="scroller/js/stscode.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-size: large;
            text-align: justify;
        }
        .style2
        {
            font-size: small;
            text-align: justify;
        }
        .style3
        {
            font-size: small;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="TextBox1">
    <div class="main_">
    <div class="head_">

        <asp:Image ID="Image2" runat="server" CssClass="pet_png" 
            ImageUrl="~/images/design_images/petro.png" />
                </div>
    <div class="m_right">
<div class="login_d r5">    
      <asp:Image ID="Image1" runat="server" CssClass="user_img" 
                        ImageUrl="~/images/design_images/user.png" />
                        <br />
                              <br />
         <asp:Label ID="Label2" runat="server" CssClass="lbl_" ForeColor="Black" 
                        Text="نام کاربری" Width="300px"></asp:Label>
                        <br />
            <asp:TextBox ID="TextBox1" runat="server" CssClass="txt_ r5" Height="30px" 
                        Width="300px" EnableTheming="True" TabIndex="1" 
          ToolTip="نام کاربری که وارد ویندوز می شوید را وارد کنید"></asp:TextBox>
                        <br />
                        <br />
                              <br />
      <asp:Label ID="Label1" runat="server" CssClass="lbl_" ForeColor="Black" 
                        Text="کلمه عبور" Width="300px"></asp:Label>
                        <br />
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="txt_ r5" Height="30px" 
                        TextMode="Password" Width="300px" TabIndex="2" 
          ToolTip="رمز عبور"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                    <asp:Button ID="Button1" runat="server" CssClass="btn r5 btn_l" 
            Text="ورود" ValidationGroup="1" TabIndex="3" Font-Bold="True" />

      <br />

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
          ErrorMessage=".لطفا نام کاربری را وارد کنید" ControlToValidate="TextBox1" 
          ValidationGroup="1" CssClass="sum_val" ForeColor="#990000" Width="325px"></asp:RequiredFieldValidator>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
          ErrorMessage=".لطفا کلمه عبور را وارد کنید" ControlToValidate="TextBox2" 
          ValidationGroup="1" CssClass="sum_val" ForeColor="#990000" Width="325px"></asp:RequiredFieldValidator>
</div>
    </div>
    <div class="m_left">
       <div class="scroller r5">
           <span class="style1">
           <br />
           <br />
           </span>
           </div>
    </div>
      
    
    </div>
   
    </form>
</body>
</html>
