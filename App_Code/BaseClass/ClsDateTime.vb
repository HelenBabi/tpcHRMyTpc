Imports Microsoft.VisualBasic
Imports System.Globalization.PersianCalendar
Imports System
Imports System.Globalization


Public Class ClsDateTime
    Dim dd As New Globalization.PersianCalendar

    Public Shared Function ShamsiToMiladi(shamsiDate As String) As DateTime

        Try
            Dim parts() As String = shamsiDate.Split("/"c)

            If parts.Length <> 3 Then
                Throw New FormatException("فرمت تاریخ صحیح نیست. فرمت صحیح: yyyy/MM/dd")
            End If

            Dim year As Integer = Integer.Parse(parts(0))
            Dim month As Integer = Integer.Parse(parts(1))
            Dim day As Integer = Integer.Parse(parts(2))

            Dim pc As New PersianCalendar()
            Return pc.ToDateTime(year, month, day, 0, 0, 0, 0)

        Catch ex As Exception
            Throw New Exception("خطا در تبدیل تاریخ: " & ex.Message)
        End Try
    End Function


    Public Shared Function GetDateNow() As String
        Dim date_p As New Globalization.PersianCalendar
        Dim date_ As String = ""
        Dim mah As String = date_p.GetMonth(Now)
        If date_p.GetMonth(Now) < 10 Then
            mah = "0" & date_p.GetMonth(Now)
        End If
        Dim ruz As String = date_p.GetDayOfMonth(Now)
        If date_p.GetDayOfMonth(Now) < 10 Then
            ruz = "0" & date_p.GetDayOfMonth(Now)
        End If
        date_ = date_p.GetYear(Now) & "/" & mah & "/" & ruz
        Return date_
    End Function

    Public Shared Function GetTimeNow() As String
        Dim time_p As New Globalization.PersianCalendar
        Dim time_ As String = ""
        Dim dagige, saniye, saat As String

        If time_p.GetHour(Now) < 10 Then
            saat = "0" & time_p.GetHour(Now)
        Else
            saat = time_p.GetHour(Now)
        End If

        If time_p.GetMinute(Now) < 10 Then
            dagige = "0" & time_p.GetMinute(Now)
        Else
            dagige = time_p.GetMinute(Now)
        End If

        If time_p.GetSecond(Now) < 10 Then
            saniye = "0" & time_p.GetSecond(Now)
        Else
            saniye = time_p.GetSecond(Now)
        End If
        time_ = saat & ":" & dagige
        Return time_
    End Function
    Public Shared Function setDate(ByVal in_date As String) As String


        Dim date_in As String = in_date
        Dim a As Integer
        Dim sal As String = date_in.Substring(0, 4)
        a = date_in.IndexOf("/", 5)
        Dim brek_sum = a - 5
        Dim month_ As String = date_in.Substring(5, brek_sum)
        Dim day = date_in.Substring(date_in.LastIndexOf("/") + 1)

        Dim date_out As String = ""
        Dim mah As String = ""
        If month_ < 10 Then
            mah = "0" & month_
        Else
            mah = month_
        End If
        Dim ruz As String = ""
        If day < 10 Then
            ruz = "0" & day
        Else
            ruz = day
        End If
        date_out = sal & "/" & mah & "/" & ruz
        Return date_out
    End Function

    Public Function getdatee() As String
        Dim x As String
        x = "امروز" & getrooz() & Str(dd.GetDayOfMonth(Now)) & getmonth() & Str(dd.GetYear(Now))
        Return (x)
    End Function
    Public Function getrooz() As String
        Dim x As String
        Dim w As String = ""
        x = dd.GetDayOfWeek(Now)
        If x = 1 Then
            w = "  دوشنبه "
        ElseIf x = 2 Then
            w = "  سه شنبه "
        ElseIf x = 3 Then
            w = "  چهارشنبه "
        ElseIf x = 4 Then
            w = "  پنج شنبه "
        ElseIf x = 5 Then
            w = "  جمعه "
        ElseIf x = 6 Then
            w = "  شنبه "
        ElseIf x = 0 Then
            w = "  یکشنبه "
        End If
        Return (w)

    End Function
    Public Function getmonth() As String
        Dim x As String
        Dim w As String = ""
        x = Str(dd.GetMonth(Now))
        If x = 1 Then
            w = "  فروردين "
        ElseIf x = 2 Then
            w = "  ارديبهشت "
        ElseIf x = 3 Then
            w = "  خرداد "
        ElseIf x = 4 Then
            w = "  تير "
        ElseIf x = 5 Then
            w = "  مرداد "
        ElseIf x = 6 Then
            w = "  شهريور "
        ElseIf x = 7 Then
            w = "مهر "
        ElseIf x = 8 Then
            w = "  آبان "
        ElseIf x = 9 Then
            w = "  آذر "
        ElseIf x = 10 Then
            w = "  دي "
        ElseIf x = 11 Then
            w = "  بهمن "
        ElseIf x = 12 Then
            w = "  اسفند "
        End If

        Return (w)
    End Function

    Public Shared Function getRoozMah() As Integer
        Dim dd As New Globalization.PersianCalendar
        Dim x As Integer

        x = dd.GetDayOfMonth(Now)

        Return (x)

    End Function
    Public Shared Function getSal() As Integer
        Dim dd As New Globalization.PersianCalendar
        Dim x As Integer
        x = dd.GetYear(Now)
        Return (x)
    End Function

    Public Shared Function getMah() As Integer
        Dim dd As New Globalization.PersianCalendar
        Dim x As Integer
        x = dd.GetMonth(Now)
        Return (x)
    End Function
    Public Function getmonthNow() As String
        Dim x As String

        x = Str(dd.GetMonth(Now))


        Return (x)
    End Function
    Public Shared Function GetSaat() As Integer
        Dim time_p As New Globalization.PersianCalendar
        Dim saat As Integer
        saat = time_p.GetHour(Now)
        Return saat
    End Function
    Public Shared Function GetRoozHafteAdad() As Integer
        Dim dd As New Globalization.PersianCalendar
        Dim x As Integer
        x = dd.GetDayOfWeek(Now)
        ' x = x + 2
        Return (x)
    End Function
End Class

