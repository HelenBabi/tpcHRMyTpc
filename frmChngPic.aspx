<%@ Page Title="تغییر عکس پروفایل" Language="VB" MasterPageFile="~/dabir.master" AutoEventWireup="false" CodeFile="frmChngPic.aspx.vb" Inherits="frmChngPic" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .profile-container {
            min-height: 80vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            padding: 20px;
        }
        .profile-card {
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            max-width: 550px;
            width: 100%;
            overflow: hidden;
            animation: fadeInUp 0.5s ease;
        }
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
        .profile-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px 25px;
            text-align: center;
        }
        .profile-header h3 {
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }
        .profile-header p {
            margin: 10px 0 0;
            opacity: 0.9;
            font-size: 13px;
        }
        .profile-body {
            padding: 35px 30px;
        }
        .avatar-container {
            text-align: center;
            margin-bottom: 30px;
        }
        .avatar-preview {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            object-fit: cover;
            border: 4px solid white;
            box-shadow: 0 5px 20px rgba(0,0,0,0.2);
            background: #f8f9fa;
            transition: all 0.3s ease;
        }
        .avatar-preview:hover {
            transform: scale(1.05);
            box-shadow: 0 8px 30px rgba(0,0,0,0.3);
        }
        .avatar-placeholder {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto;
            border: 4px solid white;
            box-shadow: 0 5px 20px rgba(0,0,0,0.2);
        }
        .avatar-placeholder span {
            font-size: 60px;
            color: white;
        }
        .upload-area {
            border: 2px dashed #e9ecef;
            border-radius: 15px;
            padding: 25px;
            text-align: center;
            background: #f8f9fa;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-bottom: 20px;
        }
        .upload-area:hover {
            border-color: #667eea;
            background: #f0f0ff;
        }
        .upload-icon {
            font-size: 48px;
            color: #667eea;
            margin-bottom: 10px;
        }
        .upload-text {
            color: #666;
            font-size: 14px;
        }
        .upload-browse {
            color: #667eea;
            font-weight: bold;
            cursor: pointer;
        }
        .file-input-hidden {
            display: none;
        }
        .form-group-custom {
            margin-bottom: 20px;
        }
        .btn-custom {
            width: 48%;
            padding: 12px;
            border: none;
            border-radius: 12px;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .btn-save {
            background: linear-gradient(135deg, #28a745 0%, #20c997 100%);
            color: white;
        }
        .btn-save:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(40,167,69,0.4);
        }
        .btn-back {
            background: #6c757d;
            color: white;
        }
        .btn-back:hover {
            background: #5a6268;
            transform: translateY(-2px);
        }
        .btn-group-custom {
            display: flex;
            justify-content: space-between;
            gap: 15px;
            margin-top: 20px;
        }
        .info-message {
            background: #e8f4f8;
            border-radius: 10px;
            padding: 12px;
            margin-bottom: 20px;
            text-align: center;
            font-size: 12px;
            color: #17a2b8;
        }
        .error-message {
            color: #dc3545;
            font-size: 12px;
            margin-top: 8px;
            display: block;
            text-align: center;
        }
        .file-info {
            background: #e8f4f8;
            border-radius: 8px;
            padding: 10px;
            margin-top: 10px;
            font-size: 12px;
            color: #555;
            display: none;
        }
        .file-info.show {
            display: block;
        }
        @media (max-width: 576px) {
            .profile-body {
                padding: 25px 20px;
            }
            .btn-custom {
                width: 100%;
                margin-bottom: 10px;
            }
            .btn-group-custom {
                flex-direction: column;
                gap: 10px;
            }
        }
    </style>
    
    <script type="text/javascript">
        function previewImage(input) {
            var preview = document.getElementById('avatarPreview');
            var placeholder = document.getElementById('avatarPlaceholder');
            var fileInfo = document.getElementById('fileInfo');
            var fileName = document.getElementById('fileName');

            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    preview.src = e.target.result;
                    preview.style.display = 'block';
                    if (placeholder) placeholder.style.display = 'none';
                    preview.style.display = 'block';

                    // نمایش اطلاعات فایل
                    var file = input.files[0];
                    var fileSize = (file.size / 1024).toFixed(2);
                    fileName.innerHTML = file.name + ' (' + fileSize + ' KB)';
                    fileInfo.style.display = 'block';
                }

                reader.readAsDataURL(input.files[0]);
            } else {
                preview.src = '#';
                preview.style.display = 'none';
                if (placeholder) placeholder.style.display = 'flex';
                fileInfo.style.display = 'none';
            }
        }

        function triggerFileUpload() {
            document.getElementById('<%= FileUpload1.ClientID %>').click();
        }

        function validateFile() {
            var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');
            var file = fileUpload.files[0];
            var errorSpan = document.getElementById('fileError');

            if (!file) {
                errorSpan.innerHTML = '❌ لطفاً یک فایل انتخاب کنید';
                return false;
            }

            var allowedExtensions = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'];
            var fileExt = file.name.split('.').pop().toLowerCase();

            if (allowedExtensions.indexOf(fileExt) === -1) {
                errorSpan.innerHTML = '❌ فقط فایل‌های تصویری مجاز هستند (jpg, jpeg, png, gif, bmp, webp)';
                return false;
            }

            var fileSize = file.size / 1024 / 1024;
            if (fileSize > 2) {
                errorSpan.innerHTML = '❌ حجم فایل نباید بیشتر از 2 مگابایت باشد';
                return false;
            }

            errorSpan.innerHTML = '';
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="profile-container" dir="rtl">
        <div class="profile-card">
            
            <!-- هدر کارت -->
            <div class="profile-header">
                <h3>📸 تغییر عکس پروفایل</h3>
                <p>عکس پروفایل خود را آپلود کنید</p>
            </div>
            
            <!-- بدنه فرم -->
            <div class="profile-body">
                
                <!-- پیش‌نمایش عکس -->
                <div class="avatar-container">
                    <asp:Image ID="imgPreview" runat="server" 
                        CssClass="avatar-preview" 
                        ImageUrl="~/images/person_pic/person.ico"
                        style="display: block;" />
                    <div id="avatarPlaceholder" class="avatar-placeholder" style="display: none;">
                        <span>📷</span>
                    </div>
                </div>
                
                <!-- اطلاعات راهنما -->
                <div class="info-message">
                    <span>💡</span> فرمت‌های مجاز: JPG, JPEG, PNG, GIF, BMP, WEBP | حداکثر حجم: 2 مگابایت
                </div>
                
                <!-- ناحیه آپلود -->
                <div class="upload-area" onclick="triggerFileUpload()">
                    <div class="upload-icon">📤</div>
                    <div class="upload-text">
                        برای آپلود عکس کلیک کنید یا <span class="upload-browse">مرور کنید</span>
                    </div>
                    <div class="upload-text" style="font-size: 11px; margin-top: 5px;">عکس خود را اینجا بکشید و رها کنید</div>
                </div>
                
                <!-- فایل آپلود (مخفی) -->
                <asp:FileUpload ID="FileUpload1" runat="server" 
                    CssClass="file-input-hidden" 
                    onchange="previewImage(this);"
                    style="display: none;" />
                
                <!-- اطلاعات فایل انتخاب شده -->
                <div id="fileInfo" class="file-info">
                    <span>📎 فایل انتخاب شده: </span>
                    <span id="fileName"></span>
                </div>
                
                <!-- اعتبارسنجی -->
                <div id="fileError" class="error-message"></div>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ControlToValidate="FileUpload1" 
                    ErrorMessage="❌ فقط فایل عکس می توانید انتخاب کنید" 
                    ForeColor="Red" 
                    ValidationGroup="a"
                    CssClass="error-message"
                    ClientValidationFunction="validateFile"></asp:CustomValidator>
                
                <!-- دکمه‌ها -->
                <div class="btn-group-custom">
                    <asp:Button ID="btnSave" runat="server" 
                        CssClass="btn-custom btn-save" 
                        Text="💾 ذخیره عکس" 
                        ValidationGroup="a"
                        OnClientClick="return validateFile();"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnBack" runat="server" 
                        CssClass="btn-custom btn-back" 
                        Text="🏠 بازگشت به صفحه اصلی" 
                        OnClick="btnBack_Click" />
                </div>
                
            </div>
            
        </div>
    </div>
    
    <script type="text/javascript">
        // بارگذاری عکس فعلی از سرور
        window.onload = function () {
            var img = document.getElementById('<%= imgPreview.ClientID %>');
            if (img && img.src.indexOf('person.ico') === -1) {
                // عکس موجود است
            }
        }
    </script>
    
</asp:Content>