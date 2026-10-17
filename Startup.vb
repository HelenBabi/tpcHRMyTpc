'Imports Hangfire
'Imports Hangfire.SqlServer
'Imports Microsoft.Owin
'Imports Owin

'<Assembly: OwinStartup(GetType(Startup))> 

'Public Class Startup
'    Public Sub Configuration(app As IAppBuilder)
'        ' تنظیمات Hangfire
'        GlobalConfiguration.Configuration.UseSqlServerStorage("YourConnectionString")

'        ' (اختیاری) راه‌اندازی داشبورد برای نظارت
'        app.UseHangfireDashboard("/hangfire")

'        ' راه‌اندازی سرور Hangfire
'        app.UseHangfireServer()

'        ' تعریف کارهای تکراری (مثال)
'        RecurringJob.AddOrUpdate(
'            Function() Debug.WriteLine("کار تکراری از OWIN Startup!"),
'            Cron.Daily)
'    End Sub
'End Class