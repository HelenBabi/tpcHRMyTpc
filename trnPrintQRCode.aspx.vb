Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Partial Class trnPrintQRCode
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadCourses()
        End If
    End Sub

#Region "بارگذاری دوره‌ها"

    Private Sub LoadCourses()
        Try
            ' نمایش همه دوره‌ها بدون فیلتر تاریخ
            Dim sqlstr As String = "SELECT idd, onvan, tarixShuru, tarixEtmam, makan, modares, saat " & _
                                  "FROM trnBarnameRiziTbl " & _
                                  "ORDER BY idd DESC"

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            cmbCourses.DataSource = dt
            cmbCourses.DataTextField = "onvan"
            cmbCourses.DataValueField = "idd"
            cmbCourses.DataBind()

            cmbCourses.Items.Insert(0, New ListItem("-- انتخاب دوره --", "0"))

            Dim courseId As String = Request.QueryString("courseId")
            If Not String.IsNullOrEmpty(courseId) Then
                cmbCourses.SelectedValue = courseId
                If cmbCourses.SelectedValue <> "0" Then
                    btnShowQR_Click(Nothing, Nothing)
                End If
            End If

        Catch ex As Exception
            lblMessage.Text = "خطا در بارگذاری دوره‌ها: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

#End Region

#Region "نمایش QR Code"

    Protected Sub btnShowQR_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If cmbCourses.SelectedValue = "0" Then
                lblMessage.Text = "لطفاً یک دوره فعال را انتخاب کنید."
                lblMessage.ForeColor = System.Drawing.Color.Red
                Return
            End If

            Dim courseId As String = cmbCourses.SelectedValue
            hdnCourseId.Value = courseId

            ' دریافت اطلاعات دوره
            Dim sqlstr As String = "SELECT onvan, tarixShuru, tarixEtmam, makan, modares, saat " & _
                                  "FROM trnBarnameRiziTbl WHERE idd = " & courseId

            Dim dt As DataTable = ClsDataTable.GetDataTable(sqlstr)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                lblCourseTitle.InnerText = row("onvan").ToString()
                lblStartDate.InnerText = Convert.ToDateTime(row("tarixShuru")).ToString("yyyy/MM/dd")
                lblEndDate.InnerText = Convert.ToDateTime(row("tarixEtmam")).ToString("yyyy/MM/dd")
                lblLocation.InnerText = row("makan").ToString()
                lblTeacher.InnerText = row("modares").ToString()

                ' بررسی وجود ستون saat
                Try
                    lblTime.InnerText = row("saat").ToString()
                Catch
                    lblTime.InnerText = "-"
                End Try

                lblCourseId.InnerText = courseId
                hdnCourseName.Value = row("onvan").ToString()

                divCourseInfo.Visible = True

                ' ایجاد لینک اسکن
                Dim scanUrl As String = GetScanUrl(courseId)
                lblQRUrl.Text = scanUrl

                ' تنظیم لینک دکمه
                hlScanLink.NavigateUrl = scanUrl
                hlScanLink.ToolTip = "برای ثبت حضور در دوره " & row("onvan").ToString() & " کلیک کنید"

                divLink.Visible = True

                ' تولید QR Code
                GenerateQRCode(scanUrl, courseId)

                divQRCode.Visible = True
                divInstructions.Visible = True
                divButtons.Visible = True

                lblMessage.Text = ""

            Else
                lblMessage.Text = "دوره مورد نظر یافت نشد."
                lblMessage.ForeColor = System.Drawing.Color.Red
            End If

        Catch ex As Exception
            lblMessage.Text = "خطا: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

    Private Function GetScanUrl(ByVal courseId As String) As String
        Try
            Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
            Dim appPath As String = Request.ApplicationPath

            If Not appPath.EndsWith("/") Then
                appPath &= "/"
            End If

            ' لینک به صفحه ثبت حضور
            Dim scanPage As String = baseUrl & appPath & "trnScanAttendance.aspx?courseId=" & courseId

            Return scanPage

        Catch ex As Exception
            Return "http://localhost/trnScanAttendance.aspx?courseId=" & courseId
        End Try
    End Function

    Private Sub GenerateQRCode(ByVal data As String, ByVal courseId As String)
        Try
            ' استفاده از کتابخانه QR Code
            ' برای این کار باید NuGet Package: QRCoder را نصب کنید
            ' Install-Package QRCoder

            Dim qrGenerator As New QRCoder.QRCodeGenerator()
            Dim qrCodeData As QRCoder.QRCodeData = qrGenerator.CreateQrCode(data, QRCoder.QRCodeGenerator.ECCLevel.Q)
            Dim qrCode As New QRCoder.QRCode(qrCodeData)

            Using bitmap As Bitmap = qrCode.GetGraphic(20)
                Using ms As New MemoryStream()
                    bitmap.Save(ms, ImageFormat.Png)
                    Dim base64String As String = Convert.ToBase64String(ms.ToArray())
                    imgQRCode.ImageUrl = "data:image/png;base64," & base64String
                End Using
            End Using

            lblQRText.InnerText = "📱 کد QR برای دوره: " & hdnCourseName.Value

        Catch ex As Exception
            ' اگر کتابخانه QRCoder نصب نیست، از API خارجی استفاده کن
            Try
                imgQRCode.ImageUrl = "https://api.qrserver.com/v1/create-qr-code/?size=300x300&data=" & Server.UrlEncode(data)
                lblQRText.InnerText = "📱 کد QR برای دوره: " & hdnCourseName.Value
            Catch
                lblMessage.Text = "برای تولید QR Code نیاز به نصب کتابخانه QRCoder است. از NuGet Package Manager نصب کنید."
                lblMessage.ForeColor = System.Drawing.Color.Red
            End Try
        End Try
    End Sub

#End Region

#Region "دکمه‌ها"

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadCourses()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Me.GetType(), "Print", "window.print();", True)
    End Sub

    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not String.IsNullOrEmpty(imgQRCode.ImageUrl) Then
                ' استخراج Base64 از ImageUrl
                Dim imageUrl As String = imgQRCode.ImageUrl
                If imageUrl.StartsWith("data:image/png;base64,") Then
                    Dim base64Data As String = imageUrl.Replace("data:image/png;base64,", "")
                    Dim imageBytes As Byte() = Convert.FromBase64String(base64Data)

                    Response.Clear()
                    Response.ContentType = "image/png"
                    Response.AddHeader("Content-Disposition", "attachment; filename=QRCode_Course_" & hdnCourseId.Value & ".png")
                    Response.BinaryWrite(imageBytes)
                    Response.End()
                ElseIf imageUrl.StartsWith("https://api.qrserver.com") Then
                    ' دانلود از API خارجی
                    Response.Redirect(imageUrl)
                End If
            End If
        Catch ex As Exception
            lblMessage.Text = "خطا در دانلود: " & ex.Message
            lblMessage.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

    Protected Sub btnPrintInstructions_Click(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Me.GetType(), "PrintWithInstructions", "window.print();", True)
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/index.aspx")
    End Sub

#End Region

End Class